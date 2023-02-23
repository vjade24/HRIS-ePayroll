//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Payroll for RATA
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Joseph M. Tombo Jr    03/28/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View
{
    public partial class cPayRaTa : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Data Place holder creation 
        //********************************************************************

        DataTable dtSource
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl"];
            }
            set
            {
                ViewState["dtSource_dtl"] = value;
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

        DataTable dataList_employee
        {
            get
            {
                if ((DataTable)ViewState["dataList_employee"] == null) return null;
                return (DataTable)ViewState["dataList_employee"];
            }
            set
            {
                ViewState["dataList_employee"] = value;
            }
        }

       DataTable dtGroup
        {
            get
            {
                if ((DataTable)ViewState["dtGroup"] == null) return null;
                return (DataTable)ViewState["dtGroup"];
            }
            set
            {
                ViewState["dtGroup"] = value;
            }
        }

        DataTable dt_rata_rate_sked_list
        {
            get
            {
                if ((DataTable)ViewState["dt_rata_rate_sked_list"] == null) return null;
                return (DataTable)ViewState["dt_rata_rate_sked_list"];
            }
            set
            {
                ViewState["dt_rata_rate_sked_list"] = value;
            }
        }

        //********************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "employee_name";
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

                ViewState["page_allow_add"] = Session["page_allow_add_from_registry"];
                ViewState["page_allow_delete"] = Session["page_allow_delete_from_registry"];
                ViewState["page_allow_edit"] = Session["page_allow_edit_from_registry"];
                ViewState["page_allow_edit_history"] = Session["page_allow_edit_history_from_registry"];
                ViewState["page_allow_print"] = Session["page_allow_print_from_registry"];

                 //********************************************************************
                //  BEGIN - VJA- 06/20/2019 - This is Session Variable Coming From Login
                //********************************************************************
                if (Session["ep_post_authority"].ToString() == "1")
                {
                    ViewState["page_allow_add"]             = 0;
                    ViewState["page_allow_delete"]          = 1;
                    ViewState["page_allow_edit"]            = 1;
                    ViewState["page_allow_print"]           = 1;
                }
                else
                {
                    ViewState["page_allow_add"]             = 1;
                    ViewState["page_allow_delete"]          = 1;
                    ViewState["page_allow_edit"]            = 1;
                    ViewState["page_allow_print"]           = 0;
                }

                if (Session["PreviousValuesonPage_cPayRegistry"] == null)
                    Session["PreviousValuesonPage_cPayRegistry"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    RetrieveEmploymentType();
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_month.SelectedValue = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue = prevValues[2].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue = prevValues[3].ToString();
                    RetriveGroupings();
                    ddl_payroll_group.SelectedValue = prevValues[7].ToString();
                    lbl_registry_number.Text = prevValues[7].ToString();
                    ddl_payroll_template.Enabled = false;
                    ddl_month.Enabled = false;
                    ddl_year.Enabled = false;
                    ddl_empl_type.Enabled = false;

                    RetrieveEmployeename();
                    RetrieveDataListGrid();
                    RetrieveRataRateSked();

                }

                // BEGIN - VJA : 02/01/2020 - Hide the ADD BUTTON IF THE STATUS ARE ;
                // 1. ) R = Released
                // 2. ) Y = Posted
                // 3. ) T = Return
                if (Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "R" ||
                    Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "Y")
                {
                    ViewState["page_allow_add"] = 0;
                }
                // END   - VJA : 02/01/2020 - Hide the ADD BUTTON IF THE STATUS ARE ;
            }
        }

        //********************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPayReportGrouping"] = "cPayReportGrouping";

            RetrieveDataListGrid();
            RetrieveEmploymentType();

            //Retrieve When Add
            RetrieveEmployeename();

        }
        
        private string GetRegistry_NBR()
        {
            string group_nbr = "";
            if (ddl_payroll_group.SelectedValue.ToString().Trim() != "")
            {
                lbl_registry_number.Text = ddl_payroll_group.SelectedValue.ToString();
                if (dtGroup != null && dtGroup.Rows.Count > 0)
                {
                    DataRow[] registry_nbr = dtGroup.Select("payroll_registry_nbr='" + ddl_payroll_group.SelectedValue.ToString() + "'");
                    group_nbr = registry_nbr[0]["payroll_group_nbr"].ToString().Trim();
                }
            }
            return group_nbr;
        }

        private void RetrieveEmployeename()
        {
            ddl_empl_id.Items.Clear();
            dataList_employee = MyCmn.RetrieveData("sp_personnelnames_combolist_rata", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_templatecode", ddl_payroll_template.SelectedValue.ToString().Trim());
            ddl_empl_id.DataSource = dataList_employee;
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }

        private void RetrieveRataRateSked()
        {
            dt_rata_rate_sked_list = MyCmn.RetrieveData("sp_rata_rate_sked_tbl_list");
        }
            private void RetrieveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");
            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataBind();
            ddl_empl_type.Enabled = false;
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
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
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveGroupings()
        {
            ddl_payroll_group.Items.Clear();
            dtGroup = MyCmn.RetrieveData("sp_payrollregistry_hdr_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            dtGroup.DefaultView.Sort = "grouping_descr";
            dtGroup.AcceptChanges();
            ddl_payroll_group.DataSource = dtGroup;
            ddl_payroll_group.DataValueField = "payroll_registry_nbr";
            ddl_payroll_group.DataTextField = "grouping_descr";
            ddl_payroll_group.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_group.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveTemplate()
        {
            ddl_payroll_template.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_dtl_rata_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_payrolltemplate_code",ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();

            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
            //initialize table for saving in payrollregistry_jo_dtl_tbl
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            RetrieveEmployeename();

            ddl_empl_id.Enabled = true;
            ddl_empl_id.Visible = true;
            txtb_empl_name.Visible = false;

            btnSave.Visible = true;
            txtb_empl_name.Visible = false;
            txtb_voucher_nbr.Enabled = false;
            btn_calculate.Visible = true;
            lbl_if_dateposted_yes.Text = "";
            ToogleTextbox(true);


            //Get last row of the Column
            LabelAddEdit.Text = "Add New Record | Registry No: " + lbl_registry_number.Text.Trim();
            FieldValidationColorChanged(false, "ALL","0");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            //txtb_monthly_rate.Text      = "0.00";
            //txtb_gross_pay.Text         = "0.00";
            //txtb_monthly_rate.Text      = "0.00";

            //txtb_net_qa_amount.Text     = "0.00";
            //txtb_net_ra_amount.Text     = "0.00";
            //txtb_net_ta_amount.Text     = "0.00";
            //txtb_net_pay.Text           = "0.00";


            //txtb_no_days_leave.Text     = "0";
            //txtb_no_days_wordked.Text   = "0";
            //txtb_wo_vehicle.Text        = "0";

            //txtb_qa_amount_dspl.Text    = "0.00";
            //txtb_ra_amount_dspl.Text    = "0.00";
            //txtb_ta_amount_dspl.Text    = "0.00";
            
            txtb_monthly_rate.Attributes.Add("placeholder", "0.00");
            txtb_gross_pay.Attributes.Add("placeholder", "0.00");
            txtb_monthly_rate.Attributes.Add("placeholder", "0.00");

            txtb_net_qa_amount.Attributes.Add("placeholder", "0.00");
            txtb_net_ra_amount.Attributes.Add("placeholder", "0.00");
            txtb_net_ta_amount.Attributes.Add("placeholder", "0.00");
            txtb_net_pay.Attributes.Add("placeholder", "0.00");


            txtb_no_days_leave.Attributes.Add("placeholder", "0.00");
            txtb_no_days_wordked.Attributes.Add("placeholder", "0.00");
            txtb_wo_vehicle.Attributes.Add("placeholder", "0.00");

            txtb_qa_amount_dspl.Attributes.Add("placeholder", "0.00");
            txtb_ra_amount_dspl.Attributes.Add("placeholder", "0.00");
            txtb_ta_amount_dspl.Attributes.Add("placeholder", "0.00");

            txtb_department_descr.Text  = "";
            txtb_position.Text          = "";
            ddl_empl_id.SelectedIndex   = -1;
            txtb_empl_id.Text           = "" ;
            
            txtb_voucher_nbr.Text        = "";
            ViewState["created_by_user"] = "";
            ViewState["updated_by_user"] = "";
            ViewState["posted_by_user"]  = "";
            ViewState["created_dttm"]    = "";
            ViewState["updated_dttm"]    = "";
            txtb_date_posted.Text        = "";
            txtb_department_descr.Text   = "";
            txtb_position.Text           = "";
            txtb_status.Text             = "";
            lbl_post_status.Text             = "";

            LabelAddEdit.Text = "Add Record: " + lbl_registry_number.Text.Trim();
            FieldValidationColorChanged(false, "ALL","");
        }

        

        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("monthly_rate", typeof(System.String));
            dtSource.Columns.Add("gross_pay", typeof(System.String));
            dtSource.Columns.Add("net_pay", typeof(System.String));
            dtSource.Columns.Add("days_worked", typeof(System.String));
            dtSource.Columns.Add("days_leaved", typeof(System.String));
            dtSource.Columns.Add("days_wo_vehicle", typeof(System.String));
            dtSource.Columns.Add("rate_percentage", typeof(System.String));
            dtSource.Columns.Add("ra_amount", typeof(System.String));
            dtSource.Columns.Add("ta_amount", typeof(System.String));
            dtSource.Columns.Add("qa_amount", typeof(System.String));
            dtSource.Columns.Add("post_status", typeof(System.String));
            dtSource.Columns.Add("date_posted", typeof(System.String));
            dtSource.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            dtSource.Columns.Add("posted_by_user", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));
         }

        

        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollregistry_dtl_rata_tbl";
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr", "empl_id" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
        }

        

        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();

            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["monthly_rate"] = string.Empty;
            nrow["gross_pay"] = string.Empty;
            nrow["net_pay"] = string.Empty;
            nrow["days_worked"] = string.Empty;
            nrow["days_leaved"] = string.Empty;
            nrow["days_wo_vehicle"] = string.Empty;
            nrow["rate_percentage"] = string.Empty;
            nrow["ra_amount"] = string.Empty;
            nrow["ta_amount"] = string.Empty;
            nrow["qa_amount"] = string.Empty;
            nrow["post_status"] = string.Empty;
            nrow["date_posted"] = string.Empty;
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;

            dtSource.Rows.Add(nrow);
           
        }


        //***************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {

            FieldValidationColorChanged(false, "ALL", "0");
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year = commandArgs[2];
            txtb_reason.Text = "";
            if (Session["ep_post_authority"].ToString() == "0")
            {
                // This is the Normal Delete if the user is Non-accounting user
                deleteRec1.Text = "Are you sure to delete this Record ?";
                deleteRec0.InnerText = "Delete this Record ?";
                lbl_unposting.Text = "";
                txtb_reason.Visible = false;
                lnkBtnYes.Text = "Yes, Delete it";
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                // This is Message if the accounting user will unpost the card 
                deleteRec1.Text = "Are you sure you want to UnPost this Record ?";
                deleteRec0.InnerText = "UnPost this Record ?";
                lbl_unposting.Text = "Reason for UnPosting :";
                txtb_reason.Visible = true;
                lnkBtnYes.Text = "Yes, UnPost it";
            }

            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "'";

            if (Session["ep_post_authority"].ToString() == "0")
            {
                // Non-Accounting Delete 
                MyCmn.DeleteBackEndData("payrollregistry_dtl_rata_tbl", "WHERE " + deleteExpression);
                DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
                dataListGrid.Rows.Remove(row2Delete[0]);
                dataListGrid.AcceptChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                if (txtb_reason.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_reason","0");
                }
                else
                {
                    // Stored Procedure to Insert to payrollregistry_dtl_unpost_tbl during accounting case
                    DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_unpost_tbl_insert", "par_payroll_year", ddl_year.SelectedValue, "par_payroll_registry_nbr", commandarg[1].Trim(), "par_empl_id", commandarg[0].Trim(), "par_reason", txtb_reason.Text);

                    //4.4.b.Update the following fields: From payrollregistry_dtl_rata_tbl Table
                    //    1.voucher_nbr     =   { blank}
                    //    2.posted_by_user  =   { blank}
                    //    3.post_status     =   "N"   
                    //    4.date_posted     =   { blank}
                    //    5.updated_by_user =   session user_id   
                    //    6.updated_dttm    =   System Date

                    string setparams = "";
                    setparams = "voucher_nbr = '',posted_by_user = '',post_status='N',date_posted='' , updated_by_user='" + Session["ep_user_id"].ToString() + "', updated_dttm='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    MyCmn.UpdateTable("payrollregistry_dtl_rata_tbl", setparams, "WHERE " + deleteExpression);
                    RetrieveDataListGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
                }
            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "'";

            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;

            //ddl_empl_id.SelectedValue   = row2Edit[0]["empl_id"].ToString().Trim();
            txtb_empl_id.Text           = row2Edit[0]["empl_id"].ToString().Trim();
            txtb_department_descr.Text  = row2Edit[0]["department_name1"].ToString().Trim();
            txtb_position.Text          = row2Edit[0]["position_title1"].ToString().Trim();
            lbl_registry_number.Text    = row2Edit[0]["payroll_registry_nbr"].ToString().Trim();
            set_empl_ratadetail();

            ddl_empl_id.Enabled         = false;
            ddl_empl_id.Visible         = false;
            txtb_empl_name.Visible      = true;
            txtb_empl_name.Text         = row2Edit[0]["employee_name"].ToString().Trim();

            txtb_no_days_wordked.Text   = row2Edit[0]["days_worked"].ToString().Trim();
            txtb_no_days_leave.Text     = row2Edit[0]["days_leaved"].ToString().Trim();
            txtb_wo_vehicle.Text        = row2Edit[0]["days_wo_vehicle"].ToString().Trim();
            txtb_net_qa_amount.Text     = row2Edit[0]["qa_amount"].ToString().Trim();
            header_details();

            txtb_monthly_rate.Text      = row2Edit[0]["monthly_rate"].ToString();

            txtb_net_pay.Text           = double.Parse(row2Edit[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");
            txtb_gross_pay.Text         = double.Parse(row2Edit[0]["gross_pay"].ToString().Trim()).ToString("###,##0.00");

            dtSource.Rows[0]["post_status"] = row2Edit[0]["post_status"].ToString().Trim();

            LabelAddEdit.Text = "Edit Record | Registry No: " + lbl_registry_number.Text.Trim();
            ddl_empl_id.Enabled = false;
            FieldValidationColorChanged(false, "ALL","0");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


            // Add Field Again - 06/20/2019
            txtb_voucher_nbr.Text = row2Edit[0]["voucher_nbr"].ToString();
            ViewState["created_by_user"] = row2Edit[0]["created_by_user"].ToString();
            ViewState["updated_by_user"] = row2Edit[0]["updated_by_user"].ToString();
            ViewState["posted_by_user"] = row2Edit[0]["posted_by_user"].ToString();
            ViewState["created_dttm"]   = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            ViewState["updated_dttm"]   = Convert.ToDateTime(row2Edit[0]["updated_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            txtb_status.Text = row2Edit[0]["post_status_descr"].ToString();
            lbl_post_status.Text = row2Edit[0]["post_status_descr"].ToString();
            dtSource.Rows[0]["date_posted"] = row2Edit[0]["date_posted"].ToString().Trim();

            //FOR POSTED STATUS
            if (row2Edit[0]["post_status"].ToString().ToUpper() == "Y" && (Session["ep_post_authority"].ToString() == "1" || Session["ep_post_authority"].ToString() == "0"))
            {
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                txtb_voucher_nbr.Enabled = false;
                txtb_wo_vehicle.Enabled = false;
                Linkbtncancel.Text = "Close";
                lbl_if_dateposted_yes.Text = "This Payroll is already Posted, You Cannot Edit!";
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }

            //FOR USER'S IN ACCOUNTING
            else if (row2Edit[0]["post_status"].ToString() == "N" && Session["ep_post_authority"].ToString() == "1")
            {
                btnSave.Visible = true;
                btn_calculate.Visible = false;
                btnSave.Text = "Post To Card";
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = true;
                txtb_wo_vehicle.Enabled = false;
                txtb_date_posted.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }
            //FOR RELEASED
            else if (row2Edit[0]["post_status"].ToString() == "R"
                    //|| row2Edit[0]["post_status"].ToString() == "T"
                    || row2Edit[0]["post_status"].ToString() == "X"
                    )
            {
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                txtb_voucher_nbr.Enabled = false;
                txtb_wo_vehicle.Enabled = false;
                Linkbtncancel.Text = "Close";
                lbl_if_dateposted_yes.Text = "This Payroll Already " + row2Edit[0]["post_status_descr"].ToString() + ", You cannot Edit!";
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            //FOR OTHER DEPARTMENTS
            else if (Session["ep_post_authority"].ToString() == "0")
            {
                txtb_voucher_nbr.Enabled = false;
                btnSave.Visible = true;
                btn_calculate.Visible = true;
                btnSave.Text = "Save";
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = "";
                ToogleTextbox(true);

            }



        }

        //**************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Change Field Sort mode  
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
        //  BEGIN - JOSEPH- 03/20/2019 - Get Grid current sort order 
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
        //  BEGIN - JOSEPH- 03/20/2019 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL","0");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;
            header_details();

            if (IsDataValidated())
            {
                
                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString();
                    dtSource.Rows[0]["empl_id"]                 = ddl_empl_id.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().ToString();
                    dtSource.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["days_worked"]             = txtb_no_days_wordked.Text.ToString().Trim();
                    dtSource.Rows[0]["days_leaved"]             = txtb_no_days_leave.Text.ToString().Trim();
                    dtSource.Rows[0]["days_wo_vehicle"]         = txtb_wo_vehicle.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_percentage"]         = txtb_rata_sked_perc.Text.ToString().Trim();
                    dtSource.Rows[0]["ra_amount"]               = txtb_net_ra_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["ta_amount"]               = txtb_net_ta_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["qa_amount"]               = txtb_net_qa_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["post_status"]             = "N";


                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource.Rows[0]["voucher_nbr"]              = txtb_voucher_nbr.Text.ToString();
                    dtSource.Rows[0]["created_by_user"]          = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_by_user"]          = "";

                    dtSource.Rows[0]["created_dttm"]             = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["updated_dttm"]             = "";

                    dtSource.Rows[0]["posted_by_user"]           = "";
                    dtSource.Rows[0]["date_posted"]              = "";
                    // END - Add Field Again  - 06/20/2019

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString();
                    dtSource.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().ToString();
                    dtSource.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["days_worked"]             = txtb_no_days_wordked.Text.ToString().Trim();
                    dtSource.Rows[0]["days_leaved"]             = txtb_no_days_leave.Text.ToString().Trim();
                    dtSource.Rows[0]["days_wo_vehicle"]         = txtb_wo_vehicle.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_percentage"]         = txtb_rata_sked_perc.Text.ToString().Trim();
                    dtSource.Rows[0]["ra_amount"]               = txtb_net_ra_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["ta_amount"]               = txtb_net_ta_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["qa_amount"]               = txtb_net_qa_amount.Text.ToString().Trim();
                   
                    // BEGIN - Add Field Again  - 06/20/2019
                  
                    dtSource.Rows[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                    dtSource.Rows[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["created_dttm"]            = ViewState["created_dttm"].ToString();

                    if (Session["ep_post_authority"].ToString().Trim() == "1")
                    {
                        dtSource.Rows[0]["posted_by_user"]      = Session["ep_user_id"].ToString();
                        dtSource.Rows[0]["date_posted"]         = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dtSource.Rows[0]["post_status"]         = "Y";
                        dtSource.Rows[0]["voucher_nbr"]         = txtb_voucher_nbr.Text.ToString().Trim();
                        dtSource.Rows[0]["updated_by_user"]     = ViewState["updated_by_user"].ToString();
                        dtSource.Rows[0]["updated_dttm"]        = ViewState["updated_dttm"].ToString();
                    }
                    // END - Add Field Again  - 06/20/2019

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    if (scriptInsertUpdate == string.Empty) {return; }
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") { return; }

                    if (msg.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist","0");
                        return;
                    }

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["payroll_year"]            = dtSource.Rows[0]["payroll_year"];
                        nrow["payroll_registry_nbr"]    = dtSource.Rows[0]["payroll_registry_nbr"];
                        nrow["empl_id"]                 = dtSource.Rows[0]["empl_id"];
                        nrow["employee_name"]           = ddl_empl_id.SelectedItem.Text.ToString().Trim();
                        nrow["monthly_rate"]            = dtSource.Rows[0]["monthly_rate"];
                        nrow["gross_pay"]               = dtSource.Rows[0]["gross_pay"];
                        nrow["net_pay"]                 = dtSource.Rows[0]["net_pay"];
                        nrow["days_worked"]             = dtSource.Rows[0]["days_worked"];
                        nrow["days_leaved"]             = dtSource.Rows[0]["days_leaved"];
                        nrow["days_wo_vehicle"]         = dtSource.Rows[0]["days_wo_vehicle"];
                        nrow["rate_percentage"]         = dtSource.Rows[0]["rate_percentage"];
                        nrow["rate_percentage_descr"]   = dtSource.Rows[0]["rate_percentage"].ToString()+"%";
                        nrow["ra_amount"]               = dtSource.Rows[0]["ra_amount"];
                        nrow["ta_amount"]               = dtSource.Rows[0]["ta_amount"];
                        nrow["qa_amount"]               = double.Parse(dtSource.Rows[0]["qa_amount"].ToString()).ToString("###,##0.00");
                        nrow["post_status"]             = "N";
                        nrow["post_status_descr"]       = "NOT POSTED";


                        // BEGIN - Add Field Again  - 06/20/2019
                        nrow["voucher_nbr"]             = txtb_voucher_nbr.Text.ToString();
                        nrow["created_by_user"]         = Session["ep_user_id"].ToString();
                        nrow["updated_by_user"]         = "";
                        nrow["created_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        nrow["updated_dttm"]            = Convert.ToDateTime("1900-01-01"); 
                        nrow["posted_by_user"]          = "";
                        nrow["date_posted"]             = "";
                        nrow["position_title1"]         = txtb_position.Text.ToString().Trim();
                        // END - Add Field Again  - 06/20/2019

                        dataListGrid.Rows.Add(nrow);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND payroll_year='" + ddl_year.SelectedValue.ToString().Trim() + "' AND payroll_registry_nbr ='" + lbl_registry_number.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"]             = dtSource.Rows[0]["payroll_year"];
                        row2Edit[0]["payroll_registry_nbr"]     = dtSource.Rows[0]["payroll_registry_nbr"];
                        row2Edit[0]["empl_id"]                  = dtSource.Rows[0]["empl_id"];
                        row2Edit[0]["employee_name"]            = txtb_empl_name.Text.ToString().Trim();
                        row2Edit[0]["monthly_rate"]             = dtSource.Rows[0]["monthly_rate"];
                        row2Edit[0]["gross_pay"]                = dtSource.Rows[0]["gross_pay"];
                        row2Edit[0]["net_pay"]                  = dtSource.Rows[0]["net_pay"];
                        row2Edit[0]["days_worked"]              = dtSource.Rows[0]["days_worked"];
                        row2Edit[0]["days_leaved"]              = dtSource.Rows[0]["days_leaved"];
                        row2Edit[0]["days_wo_vehicle"]          = dtSource.Rows[0]["days_wo_vehicle"];
                        row2Edit[0]["rate_percentage"]          = dtSource.Rows[0]["rate_percentage"];
                        row2Edit[0]["rate_percentage_descr"]    = dtSource.Rows[0]["rate_percentage"].ToString() + "%";
                        row2Edit[0]["ra_amount"]                = dtSource.Rows[0]["ra_amount"];
                        row2Edit[0]["ta_amount"]                = dtSource.Rows[0]["ta_amount"];
                        row2Edit[0]["qa_amount"]                = double.Parse(dtSource.Rows[0]["qa_amount"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["position_title1"]          = txtb_position.Text.ToString().Trim();
                        row2Edit[0]["post_status"]              = dtSource.Rows[0]["post_status"];
                        

                        // BEGIN - Add Field Again - 06/20/2019
                        row2Edit[0]["created_by_user"]          = ViewState["created_by_user"].ToString();
                        row2Edit[0]["updated_by_user"]          = Session["ep_user_id"].ToString();
                        row2Edit[0]["created_dttm"]             = ViewState["created_dttm"].ToString() == "" ? DateTime.Now : ViewState["created_dttm"];
                        row2Edit[0]["updated_dttm"]             = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                          
                        // END - Add Field Again  - 06/20/2019
                        if (Session["ep_post_authority"].ToString() == "1")
                        {
                            row2Edit[0]["posted_by_user"]        = Session["ep_user_id"].ToString();
                            row2Edit[0]["date_posted"]           = txtb_date_posted.Text.ToString().Trim();
                            row2Edit[0]["post_status"]           = "Y";
                            row2Edit[0]["post_status_descr"]     = "POSTED";
                            row2Edit[0]["voucher_nbr"]           = txtb_voucher_nbr.Text.ToString();
                            row2Edit[0]["updated_by_user"]       = ViewState["updated_by_user"].ToString();
                            row2Edit[0]["updated_dttm"]          = ViewState["updated_dttm"].ToString();
                        }

                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }
                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                }

            }
        }

        //**************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" 
                + txtb_search.Text.Trim().Replace("'", "''").Replace("%","") 
                + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "") 
                + "%' OR net_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "") 
                + "%' OR position_title1 LIKE '%" + txtb_search.Text.ToString().Replace("'", "''").Replace("%", "")
                + "%' OR post_status_descr LIKE '%" + txtb_search.Text.ToString().Replace("'", "''").Replace("%", "") + "%'";

            DataTable dtSource1 = new DataTable();

            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("days_worked", typeof(System.String));
            dtSource1.Columns.Add("days_leaved", typeof(System.String));
            dtSource1.Columns.Add("days_wo_vehicle", typeof(System.String));
            dtSource1.Columns.Add("rate_percentage", typeof(System.String));
            dtSource1.Columns.Add("rate_percentage_descr", typeof(System.String));
            dtSource1.Columns.Add("ra_amount", typeof(System.String));
            dtSource1.Columns.Add("ta_amount", typeof(System.String));
            dtSource1.Columns.Add("qa_amount", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("date_posted", typeof(System.String));

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
        //  BEGIN - JOSEPH- 03/20/2019 - Define Property for Sort Direction  
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

        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL","0");
            DataRow[] selected_employee = dataList_employee.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");

            if (ddl_empl_id.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id", "0");
                ddl_empl_id.Focus();
                validatedSaved = false;
                return validatedSaved;
            }

            if (CommonCode.checkisdecimal(txtb_no_days_wordked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_wordked", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_no_days_leave) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_leave", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_wo_vehicle) == false)
            {
                FieldValidationColorChanged(true, "txtb_wo_vehicle", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_monthly_rate) == false)
            {
                if (txtb_monthly_rate.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_monthly_rate", "0");
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-monthly", "0");
                    validatedSaved = false;
                }
            }

            /** GROSS PAY AND NET PAY VALIDATION **/
                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    FieldValidationColorChanged(true, "txtb_gross_pay", "0");
                    validatedSaved = false;
                }
                if (CommonCode.checkisdecimal(txtb_net_pay) == false)
                {
                    FieldValidationColorChanged(true, "txtb_net_pay", "0");
                    validatedSaved = false;
                }
            /**END OF GROSS PAY AND NET PAY VALIDATION**/


            /**VALIDATION OF SUB NET AMOUNTS**/
            if (CommonCode.checkisdecimal(txtb_net_qa_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_qa_amount", "0");
                validatedSaved = false;
            }
            else if (double.Parse(selected_employee[0]["qa_amount"].ToString()) < double.Parse(txtb_net_qa_amount.Text.ToString()))
            {
                FieldValidationColorChanged(true, "invalid-qa", selected_employee[0]["qa_amount"].ToString());
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_net_ra_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_ra_amount","0");
                validatedSaved = false;
            }
            else if (double.Parse(selected_employee[0]["ra_amount"].ToString()) < double.Parse(txtb_net_ra_amount.Text.ToString()))
            {
                FieldValidationColorChanged(true, "invalid-ra",selected_employee[0]["ra_amount"].ToString());
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_net_ta_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_ta_amount","0");
                validatedSaved = false;
            }
            else if (double.Parse(selected_employee[0]["ta_amount"].ToString()) < double.Parse(txtb_net_ta_amount.Text.ToString()))
            {
                FieldValidationColorChanged(true, "invalid-ta", selected_employee[0]["ta_amount"].ToString());
                validatedSaved = false;
            }
            //Add validation
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr", "0");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }

            return validatedSaved;
        }


        //**********************************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName, string amount)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_empl_id":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text = "already-exist";
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_monthly_rate":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_monthly_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-monthly":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_monthly_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gross_pay":
                        {
                            LblRequired14.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-grosspay":
                        {
                            LblRequired14.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_pay":
                        {
                            LblRequired15.Text = MyCmn.CONST_RQDFLD;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-netpay":
                        {
                            LblRequired15.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_days_wordked":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_days_wordked.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_days_leave":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_days_leave.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_wo_vehicle":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_wo_vehicle.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_ra_amount":
                        {
                            LblRequired11.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_ra_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-ra":
                        {
                            LblRequired11.Text = "Should not be greater than "+amount;
                            txtb_net_ra_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_ta_amount":
                        {
                            LblRequired12.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_ta_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-ta":
                        {
                            LblRequired12.Text = "Should not be greater than " + amount;
                            txtb_net_ta_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_qa_amount":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_qa_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-qa":
                        {
                            LblRequired13.Text = "Should not be greater than " + amount;
                            txtb_net_qa_amount.BorderColor = Color.Red;
                            break;
                        }
                        

                    case "txtb_voucher_nbr":
                        {
                            LblRequired22.Text = MyCmn.CONST_RQDFLD;
                            txtb_voucher_nbr.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_reason":
                        {
                            LblRequired201.Text = MyCmn.CONST_RQDFLD;
                            txtb_reason.BorderColor = Color.Red;
                            break;
                        }
                        

                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired1.Text   = "";
                            LblRequired2.Text   = "";
                            LblRequired3.Text   = "";
                            LblRequired4.Text   = "";
                            LblRequired6.Text   = "";
                            LblRequired5.Text   = "";
                            LblRequired7.Text   = "";
                            LblRequired8.Text   = "";
                            LblRequired9.Text   = "";
                            LblRequired10.Text  = "";
                            LblRequired11.Text  = "";
                            LblRequired12.Text  = "";
                            LblRequired13.Text  = "";
                            LblRequired14.Text  = "";
                            LblRequired15.Text  = "";
                            LblRequired21.Text  = "";
                            LblRequired22.Text  = "";
                            LblRequired201.Text = "";

                            
                            txtb_voucher_nbr.BorderColor        = Color.LightGray;
                            ddl_empl_id.BorderColor             = Color.LightGray;
                            txtb_gross_pay.BorderColor          = Color.LightGray;
                            txtb_net_pay.BorderColor            = Color.LightGray;
                            txtb_no_days_leave.BorderColor      = Color.LightGray;
                            txtb_no_days_wordked.BorderColor    = Color.LightGray;
                            txtb_wo_vehicle.BorderColor         = Color.LightGray;
                            txtb_monthly_rate.BorderColor       = Color.LightGray;
                            txtb_qa_amount_dspl.BorderColor     = Color.LightGray;
                            txtb_ra_amount_dspl.BorderColor     = Color.LightGray;
                            txtb_ta_amount_dspl.BorderColor     = Color.LightGray;
                            txtb_net_qa_amount.BorderColor      = Color.LightGray;
                            txtb_net_ra_amount.BorderColor      = Color.LightGray;
                            txtb_net_ta_amount.BorderColor      = Color.LightGray;
                            txtb_reason.BorderColor             = Color.LightGray;


                            break;
                        }

                }
            }
        }
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_payroll_group.SelectedValue.ToString().Trim() != "" && ddl_year.SelectedValue.ToString().Trim() != "" && ddl_month.SelectedValue != "" && ddl_payroll_template.SelectedValue != "")
            {

                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }

            RetrieveDataListGrid();
            RetrieveEmployeename();
            UpdatePanel10.Update();
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveEmployeename();
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }

        protected void ddl_empl_id_TextChanged(object sender, EventArgs e)
        {
            if (ddl_empl_id.SelectedValue.ToString().Trim() != "")
            {
                //rate_basis = selected_employee[0]["rate_basis"].ToString();
                //lbl_rate_basis_descr.Text = selected_employee[0]["rate_basis_descr"].ToString() + " Rate :";
               
                set_empl_ratadetail();
                header_details();


            }
            else
            {
                ClearEntry();
            }

        }

        private string header_details()
        {
            double total_gross  = 0;
            double percent_ta   = 0;
            double percent_rata = 0;
            double net_pay      = 0;

            int error_occured   = 0;
            string rate_basis   = "";
            
            FieldValidationColorChanged(false, "ALL","0");

            if (txtb_empl_id.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_empl_id","0");
                error_occured = error_occured + 1;
            }

            if (CommonCode.checkisdecimal(txtb_no_days_wordked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_wordked","0");
                error_occured = error_occured + 1;
            }
            double net_ta       = 0;
            double net_ra       = 0;

            if (CommonCode.checkisdecimal(txtb_no_days_leave) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_leave","0");
                error_occured = error_occured + 1;
            }

            if (CommonCode.checkisdecimal(txtb_wo_vehicle) == false)
            {
                FieldValidationColorChanged(true, "txtb_wo_vehicle","0");
                error_occured = error_occured + 1;
            }

            if (error_occured > 0)
            {
                return "";
            }

            // 2023-01-06 - VJA
            double no_days_worked = 0;
            no_days_worked = double.Parse(txtb_no_days_wordked.Text.ToString().Trim()) - double.Parse(txtb_no_days_leave.Text.ToString().Trim());
            // 2023-01-06 - VJA

            DataRow[] selected_employee = dataList_employee.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");

            //DataTable select_rataratesked_a = MyCmn.RetrieveData("sp_rata_rate_sked_tbl_list2", "par_no_of_days",txtb_no_days_wordked.Text.ToString().Trim(), "par_rate_type","A");
            DataTable select_rataratesked_a = MyCmn.RetrieveData("sp_rata_rate_sked_tbl_list2", "par_no_of_days", no_days_worked.ToString().Trim(), "par_rate_type","A");
            DataTable select_rataratesked_v = MyCmn.RetrieveData("sp_rata_rate_sked_tbl_list2", "par_no_of_days", txtb_wo_vehicle.Text.ToString().Trim(), "par_rate_type", "V");

            /*
             * To get the percentage of RA and TA rate based on No. Actual Performance days of the employee
            */
            if (select_rataratesked_a.Rows.Count > 0)
                {
                    if (double.Parse(txtb_no_days_wordked.Text.ToString()) > 0)
                    {
                        txtb_rata_sked_perc.Text = select_rataratesked_a.Rows[0]["rate_percentage"].ToString();
                    }
                    else
                    {
                        txtb_rata_sked_perc.Text = "0";
                    }
                }
            else
                {
                    if (double.Parse(txtb_no_days_wordked.Text.ToString()) > 0)
                    {
                        txtb_rata_sked_perc.Text = "100";
                    }
                    else
                    {
                        txtb_rata_sked_perc.Text = "0";
                    }
                }

                percent_rata = double.Parse(txtb_rata_sked_perc.Text.ToString()) / 100;
                net_ra = percent_rata * double.Parse(selected_employee[0]["ra_amount"].ToString());
                txtb_net_ra_amount.Text = net_ra.ToString("###,##0.00");
            /*
             * VALIDATION OF PERCENTAGE END HERE
            */


            /*
             * IF T.A Amount = 0 That means official have vehicle 
             * Validate if there is No. days Without car(Repaired, Exchange) to calculate the Net T.A amount based on number of days
            */
                if (selected_employee[0]["vehicle_flag"].ToString() == "Y")
                {
                
                    txtb_wo_vehicle.Enabled = true;
                   
                    if (select_rataratesked_v.Rows.Count > 0 && double.Parse(txtb_wo_vehicle.Text.ToString().Trim()) > 0)
                    {
                        percent_ta = double.Parse(select_rataratesked_v.Rows[0]["rate_percentage"].ToString()) / 100;
                        txtb_rata_sked_perc_v.Text = select_rataratesked_v.Rows[0]["rate_percentage"].ToString();
                        net_ta = percent_ta * double.Parse(selected_employee[0]["ta_amount"].ToString());
                        txtb_net_ta_amount.Text = net_ta.ToString("###,##0.00");
                    }
                    else
                    {
                        if (double.Parse(txtb_wo_vehicle.Text.ToString().Trim()) <= 0)
                        {
                            txtb_rata_sked_perc_v.Text = "0";
                            txtb_net_ta_amount.Text = "0.00";
                        }
                        else
                        {
                            txtb_rata_sked_perc_v.Text = "100";
                            txtb_net_ta_amount.Text = double.Parse(selected_employee[0]["ta_amount"].ToString()).ToString("###,##0.00");
                        }
                    }
                }
                else
                {
                    txtb_wo_vehicle.Text    = "0";
                    txtb_wo_vehicle.Enabled = false;
                    txtb_rata_sked_perc_v.Text = "0";
                    percent_rata = double.Parse(txtb_rata_sked_perc.Text.ToString()) / 100;
                    net_ta = percent_rata * double.Parse(selected_employee[0]["ta_amount"].ToString());
                    txtb_net_ta_amount.Text = net_ta.ToString("###,##0.00");
                }
            /*
             * END T.A VALIDATION AND COMPUTATION
             */

            //hidden_rate_basis.Value = selected_employee[0]["rate_basis"].ToString();
            total_gross = double.Parse(selected_employee[0]["qa_amount"].ToString()) + double.Parse(selected_employee[0]["ta_amount"].ToString()) + double.Parse(selected_employee[0]["ra_amount"].ToString());
            txtb_gross_pay.Text = total_gross.ToString("###,##0.00");
            net_pay = double.Parse(txtb_net_qa_amount.Text.ToString() != "" ? txtb_net_qa_amount.Text.ToString() : "0") + double.Parse(txtb_net_ra_amount.Text.ToString() != "" ? txtb_net_ra_amount.Text.ToString() : "0") + double.Parse(txtb_net_ta_amount.Text.ToString() != "" ? txtb_net_ta_amount.Text.ToString() : "0");
            txtb_net_pay.Text = net_pay.ToString("###,##0.00");
            return rate_basis;
        }

        protected void set_empl_ratadetail()
        {
            DataRow[] selected_employee = null;
            if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
               selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");
            }

            else if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_EDIT)
            {
               selected_employee = dataList_employee.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");
            }
            

            txtb_monthly_rate.Text      = selected_employee[0]["monthly_rate"].ToString();
            txtb_empl_id.Text           = selected_employee[0]["empl_id"].ToString();
            txtb_qa_amount_dspl.Text    = double.Parse(selected_employee[0]["qa_amount"].ToString()).ToString("###,##0.00");
            txtb_ta_amount_dspl.Text    = double.Parse(selected_employee[0]["ta_amount"].ToString()).ToString("###,##0.00");
            txtb_ra_amount_dspl.Text    = double.Parse(selected_employee[0]["ra_amount"].ToString()).ToString("###,##0.00");
            txtb_net_qa_amount.Text     = double.Parse(selected_employee[0]["qa_amount"].ToString()).ToString("###,##0.00");
            //Added by Jorge
            txtb_department_descr.Text  = selected_employee[0]["department_name1"].ToString().Trim();
            txtb_position.Text          = selected_employee[0]["position_title1"].ToString().Trim();

            txtb_no_days_wordked.Text   = selected_employee[0]["time_days_equi"].ToString();
            txtb_wo_vehicle.Text        = "0";
            if (selected_employee[0]["vehicle_flag"].ToString() == "N")
            {
                txtb_wo_vehicle.Enabled = false;
            }
            else txtb_wo_vehicle.Enabled = true;
        }

        protected void btn_calculate_Click(object sender, EventArgs e)
        {
            header_details();
        }

        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            txtb_no_days_wordked.Enabled   = ifenable;
            txtb_no_days_leave.Enabled     = ifenable;
            txtb_net_qa_amount.Enabled     = ifenable;

        }
        
        /* ---------------------- END OF THE CODE------------------------------*/
    }
}