//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Approval for Transmittal/ DTR
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     2021-03-04      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace HRIS_ePayroll.View.cApplTransmittal
{
    public partial class cApplTransmittal : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Data Place holder creation 
        //********************************************************************

        DataTable dtSource
        {
            get
            {
                if ((DataTable)ViewState["dtSource"] == null) return null;
                return (DataTable)ViewState["dtSource"];
            }
            set
            {
                ViewState["dtSource"] = value;
            }
        }
        DataTable dataListGrid
        {
            get
            {
                if ((DataTable)ViewState["dataListGrid"] == null) return null;
                return (DataTable)ViewState["dataListGrid"];
            }
            set
            {
                ViewState["dataListGrid"] = value;
            }
        }
        DataTable dtSource_for_names
        {
            get
            {
                if ((DataTable)ViewState["dtSource_for_names"] == null) return null;
                return (DataTable)ViewState["dtSource_for_names"];
            }
            set
            {
                ViewState["dtSource_for_names"] = value;
            }
        }

        DataTable dtSource_History
        {
            get
            {
                if ((DataTable)ViewState["dtSource_History"] == null) return null;
                return (DataTable)ViewState["dtSource_History"];
            }
            set
            {
                ViewState["dtSource_History"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btn_extract_report);
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "transmittal_nbr";
                    if (Session["SortOrder"] == null)
                        Session["SortOrder"] = "ASC";
                    InitializePage();
                }

            }
            else
            {
                Response.Redirect("~/login.aspx");
            }

        }

        void Page_LoadComplete(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["page_allow_view"] = Master.allow_view;
                if (Master.allow_view == "1")
                {
                    ViewState["page_allow_add"] = 0;
                    ViewState["page_allow_delete"] = 0;
                    ViewState["page_allow_edit"] = 0;
                    ViewState["page_allow_edit_history"] = 0;
                    ViewState["page_allow_print"] = 0;
                }
                else
                {
                    ViewState["page_allow_add"] = Master.allow_add;
                    ViewState["page_allow_delete"] = Master.allow_delete;
                    ViewState["page_allow_edit"] = Master.allow_edit;
                    ViewState["page_allow_edit_history"] = Master.allow_edit_history;
                    ViewState["page_allow_print"] = Master.allow_print;
                }


                //if (Session["PreviousValuesonPage_cNTHP_payslip"] == null)
                //    Session["PreviousValuesonPage_cNTHP_payslip"] = "";
                //else if (Session["PreviousValuesonPage_cNTHP_payslip"].ToString() != string.Empty)
                //{
                //    RetrieveYear();
                //    string[] prevValues = Session["PreviousValuesonPage_cNTHP_payslip"].ToString().Split(new char[] { ',' });
                //    ddl_year.SelectedValue = prevValues[0].ToString();
                //    ddl_month.SelectedValue = prevValues[1].ToString();
                //    gv_dataListGrid.PageIndex = int.Parse(prevValues[4].ToString());
                //    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                //    up_dataListGrid.Update();
                //    DropDownListID.SelectedValue = prevValues[5].ToString();
                //    RetrieveDataListGrid();
                //    txtb_search.Text = prevValues[8].ToString();
                //    SearchData(prevValues[8].ToString());
                //}
            }
        }

        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            ViewState["chkIncludeHistory"] = "N";
            RetrieveYear();
            RetrieveDataListGrid();
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_dtr_transmittal_hdr_tbl_list", "p_dtr_year", ddl_year.SelectedValue.ToString().Trim(), "p_dtr_month", ddl_month.SelectedValue.ToString().Trim(), "p_dtr_view", ddl_view_type.SelectedValue.ToString().Trim(), "p_approval_status", ddl_approval_status.SelectedValue.ToString().Trim());

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();

            SearchData(txtb_search.Text.ToString().Trim());
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveYear()
        {
            ddl_year.Items.Clear();
            int years = Convert.ToInt32(DateTime.Now.Year);
            int prev_year = years - 5;
            for (int x = 0; x < 12; x++)
            {
                ListItem li2 = new ListItem(prev_year.ToString(), prev_year.ToString());
                ddl_year.Items.Insert(x, li2);
                if (prev_year == years)
                {
                    ListItem li3 = new ListItem((years + 1).ToString(), (years + 1).ToString());
                    ddl_year.Items.Insert(x + 1, li3);
                    ddl_year.SelectedValue = years.ToString();
                    break;
                }
                prev_year = prev_year + 1;
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_trans_nbr.Text     = "";
            txtb_trans_date.Text    = "";
            txtb_trans_descr.Text   = "";
            txtb_trans_remarks.Text = "";
            txtb_no_employee.Text   = "";

            txtb_submitted_employee_name.Text              = "";
            txtb_hr_rcvd_employee_name.Text                = "";
            txtb_payroll_rcvd_employee_name.Text           = "";
            txtb_payroll_approved_employee_name.Text       = "";
            txtb_payroll_disapproved_employee_name.Text    = "";
            txtb_payroll_created_employee_name.Text        = "";

            txtb_submitted_dttm.Text                       = "";
            txtb_hr_rcvd_dttm.Text                         = "";
            txtb_payroll_rcvd_dttm.Text                    = "";
            txtb_payroll_approved_dttm.Text                = "";
            txtb_payroll_disapproved_dttm.Text             = "";
            txtb_payroll_created_dttm.Text                 = "";
            user_id.Text                                   = Session["ep_user_id"].ToString().Trim();

            FieldValidationColorChanged(false, "ALL");
        }
        
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Change Field Sort mode  
        //**************************************************************************
        protected void gv_dataListGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            sortingDirection = GetCurrentSortDir();

            if (sortingDirection == MyCmn.CONST_SORTASC)
            {
                SortDirectionVal = SortDirection.Descending;
                sortingDirection = MyCmn.CONST_SORTDESC;
            }
            else
            {
                SortDirectionVal = SortDirection.Ascending;
                sortingDirection = MyCmn.CONST_SORTASC;
            }
            Session["SortField"] = e.SortExpression;
            Session["SortOrder"] = sortingDirection;

            MyCmn.Sort(gv_dataListGrid, dataListGrid, e.SortExpression, sortingDirection);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Get Grid current sort order 
        //**************************************************************************
        private string GetCurrentSortDir()
        {
            string sortingDirection = string.Empty;

            if (SortDirectionVal == SortDirection.Ascending)
            {
                sortingDirection = MyCmn.CONST_SORTASC;
            }
            else
            {
                sortingDirection = MyCmn.CONST_SORTDESC;
            }

            return sortingDirection;
        }
        
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtb_search.Text);
        }
        protected void SearchData(string search)
        {
            string searchExpression = "transmittal_nbr LIKE '%" + search.Trim().Replace("'", "''") + "%' " +
                                   "OR transmittal_descr LIKE '%" + search.Trim().Replace("'", "''") + "%' " +
                                   "OR approval_status LIKE '%" + search.Trim().Replace("'", "''") + " %' " +
                                   "OR approval_status_descr LIKE '%" + search.Trim().Replace("'", "''") + " %'" ;

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("transmittal_nbr", typeof(System.String));
            dtSource1.Columns.Add("transmittal_descr", typeof(System.String));
            dtSource1.Columns.Add("no_of_employees", typeof(System.String));
            dtSource1.Columns.Add("approval_status", typeof(System.String));
            dtSource1.Columns.Add("approval_status_descr", typeof(System.String));

            DataRow[] rows = dataListGrid.Select(searchExpression);
            dtSource1.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource1.ImportRow(row);
                }
            }
            gv_dataListGrid.DataSource = dtSource1;
            gv_dataListGrid.DataBind();
            up_dataListGrid.Update();
            txtb_search.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search.Focus();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Define Property for Sort Direction  
        //*************************************************************************
        public SortDirection SortDirectionVal
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }

            set
            {
                ViewState["dirState"] = value;
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Check if Object already contains value  
        //*************************************************************************
        protected void CheckInputValue(object sender, EventArgs e)
        {
            TextBox TextBox1 = (TextBox)sender;
            string checkValue = TextBox1.Text;
            string checkName = TextBox1.ID;

            if (checkValue.ToString() != "")
            {
                FieldValidationColorChanged(false, checkName);
            }
            TextBox1.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            TextBox1.Focus();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");

            //if (ddl_empl_name.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            //{
            //    FieldValidationColorChanged(true, "ddl_empl_name");
            //    validatedSaved = false;
            //}
            //if (CommonCode.checkisdatetime(txtb_effective_date.Text) == false)
            //{
            //    FieldValidationColorChanged(true, "txtb_effective_date");
            //    validatedSaved = false;
            //}
            return validatedSaved;

        }
        //**********************************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {

                    case "txtb_trans_remarks":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_trans_remarks.BorderColor = Color.Red;
                            txtb_trans_remarks.Focus();
                            break;
                        }

                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired1.Text = "";
                            txtb_trans_remarks.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }
        //***********************************************
        //  BEGIN - JADE- 05/21/2019 - Retrieve Year 
        //***********************************************
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
       
        protected void ddl_start_with_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Open the modal Select Report
        //*************************************************************************
        protected void imgbtn_print_Command1(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "transmittal_nbr = '" + svalues[0].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            btn_approve.CommandArgument = e.CommandArgument.ToString();
            btn_disapprove.CommandArgument = e.CommandArgument.ToString();

            ClearEntry();
            
            txtb_trans_nbr.Text     = row2Edit[0]["transmittal_nbr"].ToString();
            txtb_trans_date.Text    = DateTime.Parse(row2Edit[0]["transmittal_date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            txtb_trans_descr.Text   = row2Edit[0]["transmittal_descr"].ToString();
            txtb_trans_remarks.Text = row2Edit[0]["remarks"].ToString();
            txtb_no_employee.Text   = row2Edit[0]["no_of_employees"].ToString();

            lbl_remarks_dis.Text    = row2Edit[0]["remarks"].ToString();

            txtb_submitted_employee_name.Text              = row2Edit[0]["submitted_employee_name"].ToString();
            txtb_hr_rcvd_employee_name.Text                = row2Edit[0]["hr_rcvd_employee_name"].ToString();
            txtb_payroll_rcvd_employee_name.Text           = row2Edit[0]["payroll_rcvd_employee_name"].ToString();
            txtb_payroll_approved_employee_name.Text       = row2Edit[0]["payroll_approved_employee_name"].ToString();
            txtb_payroll_disapproved_employee_name.Text    = row2Edit[0]["payroll_disapproved_employee_name"].ToString();
            txtb_payroll_created_employee_name.Text        = row2Edit[0]["payroll_created_employee_name"].ToString();

            txtb_submitted_dttm.Text                       = row2Edit[0]["submitted_dttm"].ToString();
            txtb_hr_rcvd_dttm.Text                         = row2Edit[0]["hr_rcvd_dttm"].ToString();
            txtb_payroll_rcvd_dttm.Text                    = row2Edit[0]["payroll_rcvd_dttm"].ToString();
            txtb_payroll_approved_dttm.Text                = row2Edit[0]["payroll_approved_dttm"].ToString();
            txtb_payroll_disapproved_dttm.Text             = row2Edit[0]["payroll_disapproved_dttm"].ToString();
            txtb_payroll_created_dttm.Text                 = row2Edit[0]["payroll_created_dttm"].ToString();
            

            ToogleTextboxes(row2Edit[0]["approval_status"].ToString().Trim(), row2Edit[0]["approval_status_descr"].ToString().Trim());
            
            dtSource_History = MyCmn.RetrieveData("sp_dtr_transmittal_dtl_tbl_list", "p_department_code", row2Edit[0]["department_code"].ToString().Trim(), "p_subdepartment_code","", "p_division_code","", "p_section_code","", "p_transmittal_nbr", row2Edit[0]["transmittal_nbr"].ToString());
            user_id.Text = Session["ep_user_id"].ToString().Trim();
            MyCmn.Sort(gv_datagrid_history, dtSource_History, "empl_id", "ASC");
            CommonCode.GridViewBind(ref this.gv_datagrid_history, dtSource_History);
            up_datagrid_history.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "openSelectReport();", true);
        }

        protected void btn_approve_Click(object sender, EventArgs e)
        {
            ApproveDisapprove(txtb_trans_nbr.Text.ToString().Trim(), txtb_trans_remarks.Text, "A");
        }
        protected void btn_disapprove_Click(object sender, EventArgs e)
        {
            ApproveDisapprove(txtb_trans_nbr.Text.ToString().Trim(), txtb_trans_remarks.Text, "D");
        }
        //*****************************************************************************
        //  BEGIN - JRV- 09/07/2020 - Function for Approve and Rejected Status 
        //*****************************************************************************
        private void ApproveDisapprove(string par_transmittal_nbr, string par_remarks, string par_status)
        {
            FieldValidationColorChanged(false, "ALL");
            lbl_remarks.Visible = false;
            txtb_trans_remarks.Visible = false;
            
            string editExpression = "transmittal_nbr = '" + par_transmittal_nbr.Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            if (par_status == "A") // Approve Status
            {
                
                DataTable dt = MyCmn.RetrieveData("sp_edocument_trk_transmit_insert_upd", "par_transmittal_nbr", txtb_trans_nbr.Text.ToString().Trim(), "par_par_user_id", Session["ep_user_id"].ToString(), "par_update_type", "3");
                row2Edit[0]["approval_status"]  = par_status;
                row2Edit[0]["remarks"]          = txtb_trans_remarks.Text;
                SaveAddEdit.Text                = "The Record has been Approved !";
            }
            else if (par_status == "D") // Disaprrove Status
            {

                if (txtb_trans_remarks.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_trans_remarks");
                    lbl_remarks.Visible = true;
                    txtb_trans_remarks.Visible = true;
                    return;
                }
                DataTable dt = MyCmn.RetrieveData("sp_edocument_trk_transmit_insert_upd", "par_transmittal_nbr", txtb_trans_nbr.Text.ToString().Trim(), "par_par_user_id", Session["ep_user_id"].ToString(), "par_update_type", "4");
                DataTable dt2 = MyCmn.RetrieveData("sp_edocument_trk_transmit_insert_upd_dis", "par_transmittal_nbr", txtb_trans_nbr.Text.ToString().Trim(), "par_par_user_id", Session["ep_user_id"].ToString(), "par_tran_disapproved_remarks", txtb_trans_remarks.Text);

                row2Edit[0]["approval_status"]  = par_status;
                row2Edit[0]["remarks"]          = txtb_trans_remarks.Text;
                SaveAddEdit.Text                    = "The Record has been Disapproved !";
            }
            string Table_name       = "dtr_transmittal_hdr_tbl";
            string SetExpression    = "approval_status = '" + par_status + "', approved_by = '" + Session["ep_user_id"].ToString() + "',approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',remarks = '" + txtb_trans_remarks.Text.ToString().Replace("'","''") + "' ";
            string WhereExpression  = "WHERE " + editExpression;
            MyCmn.UpdateTable_to_aats(Table_name, SetExpression, WhereExpression);
            
            string Table_name_dtl           = "dtr_transmittal_dtl_tbl";
            string SetExpression_dtl        = "approval_status = '" + par_status + "'";
            string WhereExpression_dtl      = "WHERE " + editExpression;
            MyCmn.UpdateTable_to_aats(Table_name_dtl, SetExpression_dtl, WhereExpression_dtl);

            //if (row2Edit[0]["view_type"].ToString() == "0")
            //{
            //    string Table_name_dtl       = "dtr_transmittal_dtl_tbl";
            //    string SetExpression_dtl    = "approval_status = '" + par_status + "',frst_qcna_posted_ddtm = '" + DateTime.Now + "',sec_qcna_posted_ddtm = '" + DateTime.Now + "',frst_qcna_posted_by = '" + Session["ep_user_id"].ToString() + "',sec_qcna_posted_by = '" + Session["ep_user_id"].ToString() + "'";
            //    string WhereExpression_dtl  = "WHERE " + editExpression;
            //    MyCmn.UpdateTable_to_aats(Table_name_dtl, SetExpression_dtl, WhereExpression_dtl);
            //}
            //else if (row2Edit[0]["view_type"].ToString() == "1")
            //{
            //    string Table_name_dtl       = "dtr_transmittal_dtl_tbl";
            //    string SetExpression_dtl    = "approval_status = '" + par_status + "',frst_qcna_posted_ddtm = '" + DateTime.Now + "',frst_qcna_posted_by = '" + Session["ep_user_id"].ToString() + "'";
            //    string WhereExpression_dtl  = "WHERE " + editExpression;
            //    MyCmn.UpdateTable_to_aats(Table_name_dtl, SetExpression_dtl, WhereExpression_dtl);
            //}
            //else if (row2Edit[0]["view_type"].ToString() == "2")
            //{
            //    string Table_name_dtl       = "dtr_transmittal_dtl_tbl";
            //    string SetExpression_dtl    = "approval_status = '" + par_status + "',sec_qcna_posted_ddtm = '" + DateTime.Now + "',sec_qcna_posted_by = '" + Session["ep_user_id"].ToString() + "'";
            //    string WhereExpression_dtl  = "WHERE " + editExpression;
            //    MyCmn.UpdateTable_to_aats(Table_name_dtl, SetExpression_dtl, WhereExpression_dtl);
            //}
            //else
            //{
                
            //}

            RetrieveDataListGrid();
            RefreshData(txtb_trans_nbr.Text.ToString().Trim(),par_status);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "closeModal();", true);
            // MyCmn.Sort(gv_dataListGrid, dataListGrid, "transmittal_nbr", "ASC");
            // CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            // up_dataListGrid.Update();
            // show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

        }

        private void ToogleTextboxes(string par_status, string par_status_descr)
        {
            btn_approve.Visible         = false;
            btn_disapprove.Visible      = false;
            lbl_remarks.Visible         = false;
            txtb_trans_remarks.Visible  = false;
            txtb_trans_remarks.Enabled  = true;
            btn_rcvd_payroll.Visible    = false;

            if (par_status == "A")
            {
                btn_approve.Visible         = false;
                btn_disapprove.Visible      = true;
                btn_rcvd_payroll.Visible    = false;
            }
            else if (par_status == "D")
            {
                btn_approve.Visible         = false;
                btn_disapprove.Visible      = false;
                txtb_trans_remarks.Visible  = true;
                lbl_remarks.Visible         = true;
                txtb_trans_remarks.Enabled  = false;
                btn_rcvd_payroll.Visible    = false;
            }

            if (par_status == "V" && par_status_descr == "Received by HR")
            {
                btn_approve.Visible      = false;
                btn_disapprove.Visible   = false;
                btn_rcvd_payroll.Visible = true;
            }
            if (par_status == "V" && par_status_descr == "Received by Payroll Section")
            {
                btn_approve.Visible      = true;
                btn_disapprove.Visible   = true;
                btn_rcvd_payroll.Visible = false;
            }
            
        }

        protected void btn_rcvd_payroll_Click(object sender, EventArgs e)
        {
            DataTable dt = MyCmn.RetrieveData("sp_edocument_trk_transmit_insert_upd", "par_transmittal_nbr", txtb_trans_nbr.Text.ToString().Trim(), "par_par_user_id", Session["ep_user_id"].ToString(), "par_update_type", "2");
            SaveAddEdit.Text = dt.Rows[0]["msg_descr"].ToString().Trim() ;
            
            RetrieveDataListGrid();
            RefreshData(txtb_trans_nbr.Text.ToString().Trim(),"V");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "closeModal();", true);
        }

        protected void btn_extract_report_Click(object sender, EventArgs e)
        {
            var date_extract = DateTime.Now.ToString("yyyy_MM_dd_HHmm");
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Server.MapPath("~/UploadFile/payrollmonitoring_new.xlsx"));
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            object misValue = System.Reflection.Missing.Value;
            Excel.Range xlRange = xlWorkSheet.UsedRange;
            int totalRows = xlRange.Rows.Count;
          
            int totalColumns = xlRange.Columns.Count;
            DataTable dt = MyCmn.RetrieveData("sp_extract_monitoring_rep", "par_transmittal_nbr", txtb_trans_nbr.Text.ToString().Trim(), "par_user_id", Session["ep_user_id"].ToString());

            if (dt.Rows.Count > 0)
            {
                //start_row initial value for the starting row
                int start_row = 18;
                int start_row_original = 18;
                int footer_counter = 20;
                xlWorkSheet.Cells[11, 4] = dt.Rows[0]["transmittal_nbr"]; 
                xlWorkSheet.Cells[11, 9] = dt.Rows[0]["payroll_disapproved_dttm"];
                xlWorkSheet.Cells[12, 4] = dt.Rows[0]["view_type_descr"];
                xlWorkSheet.Cells[12, 9] = dt.Rows[0]["payroll_rcvd_dttm"];
                xlWorkSheet.Cells[13, 4] = dt.Rows[0]["payroll_group_nbr"];
                xlWorkSheet.Cells[13, 9] = dt.Rows[0]["payroll_approved_dttm"];
                xlWorkSheet.Cells[14, 4] = dt.Rows[0]["employment_type_descr"];
                xlWorkSheet.Cells[14, 9] = dt.Rows[0]["submitted_dttm"];
                //xlWorkSheet.Cells[2, 2] = gsis_result.Rows[0]["agency_name"];
                //xlWorkSheet.Cells[3, 2] = gsis_result.Rows[0]["gency_bp_nbr"];
                for (int x = 1; x <= dt.Rows.Count; x++)
                {
                    xlWorkSheet.Rows[start_row].Insert();
                    xlWorkSheet.get_Range("A" + start_row, "R" + start_row).Borders.Color = Color.Black;
                    xlWorkSheet.get_Range("A" + start_row, "R" + start_row).Font.Bold = false;
                    xlWorkSheet.get_Range("A" + start_row, "R" + start_row).Rows.RowHeight = 15;
                    xlWorkSheet.get_Range("C" + start_row, "D" + start_row).Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    xlWorkSheet.get_Range("A" + start_row_original, "R" + start_row_original).Copy(Missing.Value);
                    xlWorkSheet.get_Range("A" + start_row, "R" + start_row).PasteSpecial(Excel.XlPasteType.xlPasteAll,
                        Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                   
                    //xlWorkSheet.Cells[start_row, 18].Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = 1d;

                    xlWorkSheet.Cells[start_row, 1] =  dt.Rows[x - 1]["row_nbr"];
                    xlWorkSheet.Cells[start_row, 2] =  dt.Rows[x - 1]["empl_id"];
                    xlWorkSheet.Cells[start_row, 3] =  dt.Rows[x - 1]["employee_name"];
                    xlWorkSheet.Cells[start_row, 4] =  dt.Rows[x - 1]["position_title1"];
                    xlWorkSheet.Cells[start_row, 5] =  dt.Rows[x - 1]["monthly_rate"];
                    xlWorkSheet.Cells[start_row, 6] =  dt.Rows[x - 1]["transmittal_nbr"];

                    start_row = start_row + 1;
                    footer_counter = footer_counter + 1;
                }

                //xlWorkSheet.Cells[footer_counter, 1] = "sorted";
                xlWorkSheet.Cells[footer_counter + 1, 3] = dt.Rows[0]["payroll_created_dttm"];
                xlWorkSheet.Cells[footer_counter + 4, 3] = dt.Rows[0]["user_name_display"];

                Excel.Range c1 = xlWorkSheet.Cells[3, footer_counter + 3];
                Excel.Range c2 = xlWorkSheet.Cells[4, footer_counter + 3];
                xlWorkSheet.get_Range(c1, c2).Merge();
                //xlWorkSheet.get_Range(xlWorkSheet.Cells[3, footer_counter + 3], xlWorkSheet.Cells[4, footer_counter + 3]).Merge();
                xlWorkSheet.Cells[footer_counter + 5, 3] = dt.Rows[0]["user_position"];



                xlApp.DisplayAlerts = false;
                string filename = "";
                filename = "Extract_" + "payrollmonitoringsheet" + "_" +  Session["ep_user_id"].ToString() + "_" + date_extract + ".xlsx";


                xlWorkBook.SaveAs(Server.MapPath("~/UploadFile/" + filename), Excel.XlFileFormat.xlOpenXMLWorkbook,
                           Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                           Excel.XlSaveConflictResolution.xlLocalSessionChanges, Missing.Value, Missing.Value,
                           Missing.Value, Missing.Value);
                xlWorkBook.Close();
                xlApp.Quit();

                //release the created object from the marshal so that it will be no longer in
                //backend application process..
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                //this line allow client to download the created file in the server down to their
                //computers...
                ScriptManager.RegisterStartupScript(this, GetType(), "PopSelectReportX1XX", "closeLoading();", true);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.WriteFile(Server.MapPath("~/UploadFile/" + filename));
                Response.Write("<script language='javascript'>");
                Response.Write(" $(function() {$('#Loading').modal('hide'); $('#tableX').click();});");
                Response.Write("<" + "/script>");
                Response.Flush();
                Response.End();
            }
            //SaveAddEdit.Text = dt.Rows[0]["msg_descr"].ToString().Trim();

            //RetrieveDataListGrid();
            //RefreshData(txtb_trans_nbr.Text.ToString().Trim(), "V");

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "closeModal();", true);
        }

        private void RefreshData(string par_transmittal_nbr, string par_approval_status)
        {
            dataListGrid = MyCmn.RetrieveData("sp_dtr_transmittal_hdr_tbl_list", "p_dtr_year", ddl_year.SelectedValue.ToString().Trim(), "p_dtr_month", ddl_month.SelectedValue.ToString().Trim(), "p_dtr_view", ddl_view_type.SelectedValue.ToString().Trim(), "p_approval_status", par_approval_status.ToString().Trim());

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();

            string editExpression = "transmittal_nbr = '" + par_transmittal_nbr.ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            ClearEntry();
            
            txtb_trans_nbr.Text                             = row2Edit[0]["transmittal_nbr"].ToString();
            txtb_trans_date.Text                            = DateTime.Parse(row2Edit[0]["transmittal_date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            txtb_trans_descr.Text                           = row2Edit[0]["transmittal_descr"].ToString();
            txtb_trans_remarks.Text                         = row2Edit[0]["remarks"].ToString();
            txtb_no_employee.Text                           = row2Edit[0]["no_of_employees"].ToString();
            lbl_remarks_dis.Text                            = row2Edit[0]["remarks"].ToString();

            txtb_submitted_employee_name.Text              = row2Edit[0]["submitted_employee_name"].ToString();
            txtb_hr_rcvd_employee_name.Text                = row2Edit[0]["hr_rcvd_employee_name"].ToString();
            txtb_payroll_rcvd_employee_name.Text           = row2Edit[0]["payroll_rcvd_employee_name"].ToString();
            txtb_payroll_approved_employee_name.Text       = row2Edit[0]["payroll_approved_employee_name"].ToString();
            txtb_payroll_disapproved_employee_name.Text    = row2Edit[0]["payroll_disapproved_employee_name"].ToString();
            txtb_payroll_created_employee_name.Text        = row2Edit[0]["payroll_created_employee_name"].ToString();

            txtb_submitted_dttm.Text                       = row2Edit[0]["submitted_dttm"].ToString();
            txtb_hr_rcvd_dttm.Text                         = row2Edit[0]["hr_rcvd_dttm"].ToString();
            txtb_payroll_rcvd_dttm.Text                    = row2Edit[0]["payroll_rcvd_dttm"].ToString();
            txtb_payroll_approved_dttm.Text                = row2Edit[0]["payroll_approved_dttm"].ToString();
            txtb_payroll_disapproved_dttm.Text             = row2Edit[0]["payroll_disapproved_dttm"].ToString();
            txtb_payroll_created_dttm.Text                 = row2Edit[0]["payroll_created_dttm"].ToString();
            
            ToogleTextboxes(row2Edit[0]["approval_status"].ToString().Trim(), row2Edit[0]["approval_status_descr"].ToString().Trim());
            ddl_approval_status.SelectedValue = row2Edit[0]["approval_status"].ToString().Trim();

            SearchData(txtb_search.Text.ToString().Trim());
        }


        //*************************************************************************
        //  END OF CODE
        //*************************************************************************
    }
}