//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for JO Tax Approval
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     04/22/2020    Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cJOTaxUpd
{
    public partial class cJOTaxUpd : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 04/22/2020 - Data Place holder creation 
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
        DataTable dataListGrid_dtl
        {
            get
            {
                if ((DataTable)ViewState["dataListGrid_dtl"] == null) return null;
                return (DataTable)ViewState["dataListGrid_dtl"];
            }
            set
            {
                ViewState["dataListGrid_dtl"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 04/22/2020 - Public Variable used in Add/Edit Mode
        //********************************************************************
        CommonDB MyCmn = new CommonDB();
        //********************************************************************
        //  BEGIN - VJA- 04/22/2020 - Page Load method
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
        //********************************************************************
        //  BEGIN - VJA- 04/22/2020 - Page Load Complete method
        //********************************************************************
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    ViewState["page_allow_add"]          = Master.allow_add;
                    ViewState["page_allow_delete"]       = Master.allow_delete;
                    ViewState["page_allow_edit"]         = Master.allow_edit;
                    ViewState["page_allow_edit_history"] = Master.allow_edit_history;
                    ViewState["page_allow_print"]        = Master.allow_print;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 04/22/2020 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            RetrieveYear();
            RetrieveDataListGrid();
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollemployee_tax_tbl_for_apprvl", "par_year",ddl_year.SelectedValue.ToString().Trim(), "par_rcrd_status", ddl_rcrd_status.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_empl_id.Text        = "" ;
            txtb_employee_name.Text  = "" ;
            txtb_basic_tax_rate.Text = "" ;
            txtb_addl_tax_rate.Text  = "" ;
            txtb_vat_perc.Text       = "" ;
            txtb_status.Text         = "" ;
            txtb_effective_date.Text = "" ;

        }
        //*************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();

            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("bir_class", typeof(System.String));
            dtSource.Columns.Add("with_sworn", typeof(System.String));
            dtSource.Columns.Add("fixed_rate", typeof(System.String));
            dtSource.Columns.Add("total_gross_pay", typeof(System.String));
            dtSource.Columns.Add("dedct_status", typeof(System.String));
            dtSource.Columns.Add("rcrd_status", typeof(System.String));
            dtSource.Columns.Add("user_id_created_by", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("user_id_updated_by", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));

        }
        //*************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollemployee_tax_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "effective_date" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["bir_class"] = string.Empty;
            nrow["with_sworn"] = string.Empty;
            nrow["fixed_rate"] = string.Empty;
            nrow["total_gross_pay"] = string.Empty;
            nrow["dedct_status"] = string.Empty;
            nrow["rcrd_status"] = string.Empty;
            nrow["user_id_created_by"] = string.Empty;
            nrow["created_dttm"] = string.Empty;
            nrow["user_id_updated_by"] = string.Empty;
            nrow["updated_dttm"] = string.Empty;
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void approve_reject_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND effective_date = '"+ svalues[1].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_empl_id.Text        =  row2Edit[0]["empl_id"].ToString();
            txtb_employee_name.Text  =  row2Edit[0]["employee_name"].ToString();
            txtb_basic_tax_rate.Text =  row2Edit[0]["basic_tax_rate"].ToString();
            txtb_addl_tax_rate.Text  =  row2Edit[0]["tax_perc"].ToString();
            txtb_vat_perc.Text       =  row2Edit[0]["vat_perc"].ToString();
            txtb_status.Text         =  row2Edit[0]["rcrd_status_descr"].ToString();
            txtb_effective_date.Text =  row2Edit[0]["effective_date"].ToString();
            
            LabelAddEdit.Text = "Approve and Reject";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            if (row2Edit[0]["rcrd_status"].ToString() == "A" ||  // Approved
                row2Edit[0]["rcrd_status"].ToString() == "R" )   // Rejected
            {
                btn_approve.Visible = false;
                btn_reject.Visible  = false;
                btn_set_new.Visible  = true;
                lbl_status_descr.Text = "The Record Status is Already " + row2Edit[0]["rcrd_status_descr"].ToString() + ".";
            }
            else
            {
                btn_approve.Visible = true;
                btn_reject.Visible  = true;
                btn_set_new.Visible = false;
                lbl_status_descr.Text = "";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopAddEdit", "openModal();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void imgbtn_add_empl_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string showExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND effective_date = '" + svalues[1].ToString().Trim() + "'";
            DataRow[] row2Show = dataListGrid.Select(showExpression);

            txtb_employee_name_dtl.Text  = "";
            txtb_empl_id_dtl.Text        = "";

            txtb_employee_name_dtl.Text  = row2Show[0]["employee_name"].ToString().Trim();
            txtb_empl_id_dtl.Text        = row2Show[0]["empl_id"].ToString().Trim();

            dataListGrid_dtl = MyCmn.RetrieveData("sp_payrollemployee_tax_dtl_tbl_list_ACT", "par_payroll_year", row2Show[0]["payroll_year"].ToString().Trim(), "par_empl_id", row2Show[0]["empl_id"].ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_dataListGrid_dtl, dataListGrid_dtl);
            gv_dataListGrid_dtl.PageSize = Convert.ToInt32(ddl_show_dtl.Text);
            up_dataListGrid_dtl.Update();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDtl", "openModal_dtl();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Change Field Sort mode  
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
        //  BEGIN - VJA- 04/22/2020 - Get Grid current sort order 
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
        //  BEGIN - VJA- 04/22/2020 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**********************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void SearchData(string search)
        {
            string searchExpression =
               "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "rcrd_status LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "rcrd_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "effective_date LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";
            
            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("rcrd_status", typeof(System.String));
            dtSource1.Columns.Add("rcrd_status_descr", typeof(System.String));
            dtSource1.Columns.Add("basic_tax_rate", typeof(System.String));
            dtSource1.Columns.Add("tax_perc", typeof(System.String));
            dtSource1.Columns.Add("vat_perc", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
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
        //  BEGIN - VJA- 04/22/2020 - Define Property for Sort Direction  
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

        //*****************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Function for Approve and Rejected Status 
        //*****************************************************************************
        private void ApproveReject(string var_status)
        {
            string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND effective_date = '" + txtb_effective_date.Text.ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);


            if (var_status == "A") // Approve Status
            {
                row2Edit[0]["rcrd_status"]       = var_status;
                row2Edit[0]["rcrd_status_descr"] = "Approved";
                SaveAddEdit.Text                 = "The Record has been Approved !";
            }
            else if (var_status == "R") // Rejected Status
            {
                row2Edit[0]["rcrd_status"]       = var_status;
                row2Edit[0]["rcrd_status_descr"] = "Rejected";
                SaveAddEdit.Text                 = "The Record has been Rejected !";
            }
            else if (var_status == "N") // Set to New - 2023-07-19
            {
                row2Edit[0]["rcrd_status"]       = var_status;
                row2Edit[0]["rcrd_status_descr"] = "New";
                SaveAddEdit.Text                 = "The Record has set to New !";
            }
            string Table_name       = "payrollemployee_tax_tbl";
            string SetExpression    = "rcrd_status = '" + var_status + "', user_id_updated_by = '" + Session["ep_user_id"].ToString() + "',updated_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string WhereExpression = "WHERE " + editExpression;
            MyCmn.UpdateTable(Table_name, SetExpression, WhereExpression);
            up_dataListGrid.Update();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
            ViewState.Remove("AddEdit_Mode");
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            // ***************************************************************************************************
            // *** VJA - 2023-07-21 - Update the payrollemployee_tax_hdr_tbl on HRIS_ACT db - Kuya Marvin  *******
            // ***************************************************************************************************
            DataTable upd_act = new DataTable();
            upd_act = MyCmn.RetrieveData("sp_payrollemployee_tax_hdr_tbl_update", "par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_effective_date", txtb_effective_date.Text.ToString().Trim(), "par_rcrd_status", var_status, "par_user_id_updated_by", Session["ep_user_id"].ToString());
        }
        //*****************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Button to Approve 
        //*****************************************************************************
        protected void btn_approve_Click(object sender, EventArgs e)
        {
            ApproveReject("A");
        }
        //*****************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Button to Reject
        //*****************************************************************************
        protected void btn_reject_Click(object sender, EventArgs e)
        {
            ApproveReject("R");
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Change Field Sort mode  
        //**************************************************************************
        protected void gv_dataListGrid_Sorting_dtl(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(gv_dataListGrid_dtl, dataListGrid_dtl, e.SortExpression, sortingDirection);
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/22/2020 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging_dtl(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid_dtl.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid_dtl, dataListGrid_dtl, "payroll_month", Session["SortOrder"].ToString());
        }
        //**********************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged_dtl(object sender, EventArgs e)
        {
            string searchExpression =
               "voucher_nbr LIKE '%" + txtb_search_dtl.Text.Trim().Replace("'", "''") + "%' OR " +
               "payroll_period_descr LIKE '%" + txtb_search_dtl.Text.Trim().Replace("'", "''") + "%' OR " +
               "payrolltemplate_descr LIKE '%" + txtb_search_dtl.Text.Trim().Replace("'", "''") + "%' OR " +
               "gross_pay LIKE '%" + txtb_search_dtl.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource1.Columns.Add("payroll_period_descr", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_descr", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            
            DataRow[] rows = dataListGrid_dtl.Select(searchExpression);
            dtSource1.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource1.ImportRow(row);
                }
            }
            gv_dataListGrid_dtl.DataSource = dtSource1;
            gv_dataListGrid_dtl.DataBind();
            up_dataListGrid_dtl.Update();
            txtb_search_dtl.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search_dtl.Focus();
            
        }

        //**********************************************************************************
        //  BEGIN - VJA- 04/22/2020 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void ddl_show_dtl_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid_dtl.PageSize = Convert.ToInt32(ddl_show_dtl.Text);
            MyCmn.Sort(gv_dataListGrid_dtl, dataListGrid_dtl, "payroll_month", Session["SortOrder"].ToString());
        }

        //*****************************************************************************
        //  BEGIN - JRV- 09/07/2020 - Button to Approve 
        //*****************************************************************************
        protected void btn_approval_Click(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            ApproveRejectNew(svalues[0], svalues[1], svalues[2], svalues[3]);

        }

        //*****************************************************************************
        //  BEGIN - JRV- 09/07/2020 - Function for Approve and Rejected Status 
        //*****************************************************************************
        private void ApproveRejectNew(string par_empl_id, string par_effective_date, string par_year, string par_status)
        {
            string editExpression = "empl_id = '" + par_empl_id.Trim() + "' AND effective_date = '" + par_effective_date.Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            if (par_status == "A") // Approve Status
            {
                row2Edit[0]["rcrd_status"] = par_status;
                row2Edit[0]["rcrd_status_descr"] = "Approved";
                SaveAddEdit.Text = "The Record has been Approved !";
            }
            else if (par_status == "R") // Rejected Status
            {
                row2Edit[0]["rcrd_status"] = par_status;
                row2Edit[0]["rcrd_status_descr"] = "Rejected";
                SaveAddEdit.Text = "The Record has been Rejected !";
            }
            string Table_name = "payrollemployee_tax_tbl";
            string SetExpression = "rcrd_status = '" + par_status + "', user_id_updated_by = '" + Session["ep_user_id"].ToString() + "',updated_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            string WhereExpression = "WHERE " + editExpression;
            MyCmn.UpdateTable(Table_name, SetExpression, WhereExpression);

            up_dataListGrid.Update();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalApproval();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "closeModal();", true);
            ViewState.Remove("AddEdit_Mode");
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            // ***************************************************************************************************
            // *** VJA - 2023-07-21 - Update the payrollemployee_tax_hdr_tbl on HRIS_ACT db - Kuya Marvin  *******
            // ***************************************************************************************************
            DataTable upd_act = new DataTable();
            upd_act = MyCmn.RetrieveData("sp_payrollemployee_tax_hdr_tbl_update", "par_empl_id", par_empl_id.Trim(), "par_effective_date", par_effective_date.Trim(), "par_rcrd_status", par_status, "par_user_id_updated_by", Session["ep_user_id"].ToString());
        }

        //**********************************************************************************
        //  BEGIN - JRV- 04/22/2020 - Additional Action Button for Approval
        //*********************************************************************************
        protected void approve_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND effective_date = '" + svalues[1].ToString().Trim() + "' AND payroll_year = '" + svalues[2].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            approval_hdr.Text = "Approve this Record?";
            approval_dtl.Text = "Are you sure to approve this record?";
            approval_footer.Text = "Yes, Approve it";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString() + ",A";
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_empl_id.Text = row2Edit[0]["empl_id"].ToString();
            txtb_employee_name.Text = row2Edit[0]["employee_name"].ToString();
            txtb_basic_tax_rate.Text = row2Edit[0]["basic_tax_rate"].ToString();
            txtb_addl_tax_rate.Text = row2Edit[0]["tax_perc"].ToString();
            txtb_vat_perc.Text = row2Edit[0]["vat_perc"].ToString();
            txtb_status.Text = row2Edit[0]["rcrd_status_descr"].ToString();
            txtb_effective_date.Text = row2Edit[0]["effective_date"].ToString();

            LabelAddEdit.Text = "Approve and Reject";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            

            LabelAddEdit.Text = "Approve and Reject";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            if (row2Edit[0]["rcrd_status"].ToString() == "A" ||  // Approved
                row2Edit[0]["rcrd_status"].ToString() == "R")   // Rejected
            {
                btn_approve.Visible = false;
                btn_reject.Visible = false;
                btn_set_new.Visible = true;
                lbl_status_descr.Text = "The Record Status is Already " + row2Edit[0]["rcrd_status_descr"].ToString() + ".";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopAddEdit", "openModal();", true);

            }
            else
            {
                btn_approve.Visible = true;
                btn_reject.Visible = true;
                btn_set_new.Visible = false;
                lbl_status_descr.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopModalApproval", "openModalApproval();", true);
            }


        }

        //**********************************************************************************
        //  BEGIN - JRV- 04/22/2020 - Additional Action Button for Approval
        //*********************************************************************************
        protected void reject_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND effective_date = '" + svalues[1].ToString().Trim() + "' AND payroll_year = '" + svalues[2].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            approval_hdr.Text = "Disapprove this Record?";
            approval_dtl.Text = "Are you sure to disapprove this record?";
            approval_footer.Text = "Yes, Disapprove it";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString() + ",R";

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_empl_id.Text = row2Edit[0]["empl_id"].ToString();
            txtb_employee_name.Text = row2Edit[0]["employee_name"].ToString();
            txtb_basic_tax_rate.Text = row2Edit[0]["basic_tax_rate"].ToString();
            txtb_addl_tax_rate.Text = row2Edit[0]["tax_perc"].ToString();
            txtb_vat_perc.Text = row2Edit[0]["vat_perc"].ToString();
            txtb_status.Text = row2Edit[0]["rcrd_status_descr"].ToString();
            txtb_effective_date.Text = row2Edit[0]["effective_date"].ToString();

            LabelAddEdit.Text = "Approve and Reject";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);


            LabelAddEdit.Text = "Approve and Reject";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            if (row2Edit[0]["rcrd_status"].ToString() == "A" ||  // Approved
                row2Edit[0]["rcrd_status"].ToString() == "R")   // Rejected
            {
                btn_approve.Visible = false;
                btn_reject.Visible = false;
                btn_set_new.Visible = true;
                lbl_status_descr.Text = "The Record Status is Already " + row2Edit[0]["rcrd_status_descr"].ToString() + ".";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopAddEdit", "openModal();", true);

            }
            else
            {
                btn_approve.Visible = true;
                btn_reject.Visible = true;
                btn_set_new.Visible = false;
                lbl_status_descr.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopModalApproval", "openModalApproval();", true);
            }
            

        }

        //**********************************************************************************
        //  BEGIN - VJA- 2021-04-14 - Button on Modal -Approve All
        //*********************************************************************************
        protected void lnbtn_approve_all_Command(object sender, CommandEventArgs e)
        {
            if (dataListGrid.Rows.Count == 0 || dataListGrid == null)
            {
                Label8.Text         = "No data found to Approve data";
                h2_status.InnerText = "NO DATA FOUND!";
                i_icon_display.Attributes.Add("class", "fa-5x fa fa-times-circle text-danger");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopcloseModalApproval", "closeModalApproval_1();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalApprove_All();", true);
            }
        }
        //**********************************************************************************
        //  BEGIN - VJA- 2021-04-14 - Button on Modal -Approve All
        //*********************************************************************************
        protected void lnkbtn_approve_all_Command(object sender, CommandEventArgs e)
        {
            id_approveall_icn.Attributes.Add("class", "fa fa-spinner fa-spin");
            for (int x = 0; x < dataListGrid.Rows.Count; x++)
            {
                if (dataListGrid.Rows[x]["rcrd_status"].ToString() == "N")
                {
                    string editExpression = "empl_id = '" + dataListGrid.Rows[x]["empl_id"].ToString() + "' AND effective_date = '" + dataListGrid.Rows[x]["effective_date"].ToString() + "'";
                    string par_status                   = "A";
                    dataListGrid.Rows[x]["rcrd_status"] = "A";
                    dataListGrid.Rows[x]["rcrd_status_descr"] = "Approved";
                    SaveAddEdit.Text = "The Record has been Approved !";

                    string Table_name = "payrollemployee_tax_tbl";
                    string SetExpression = "rcrd_status = '" + par_status + "', user_id_updated_by = '" + Session["ep_user_id"].ToString() + "',updated_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                    string WhereExpression = "WHERE " + editExpression;
                    MyCmn.UpdateTable(Table_name, SetExpression, WhereExpression);
                    // ***************************************************************************************************
                    // *** VJA - 2023-07-21 - Update the payrollemployee_tax_hdr_tbl on HRIS_ACT db - Kuya Marvin  *******
                    // ***************************************************************************************************
                    DataTable upd_act = new DataTable();
                    upd_act = MyCmn.RetrieveData("sp_payrollemployee_tax_hdr_tbl_update", "par_empl_id", dataListGrid.Rows[x]["empl_id"].ToString(), "par_effective_date", dataListGrid.Rows[x]["effective_date"].ToString(), "par_rcrd_status","A", "par_user_id_updated_by", Session["ep_user_id"].ToString());
                }
            }

            up_dataListGrid.Update();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalApprove_All();", true);

            Label8.Text = dataListGrid.Rows.Count + "  Employee count has been Approved";
            h2_status.InnerText = "THE RECORD HAS BEEN APPROVED!";
            i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopcloseModalApproval", "closeModalApproval_1();", true);
            id_approveall_icn.Attributes.Add("class", "fa fa-check");
            ViewState.Remove("AddEdit_Mode");
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
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

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }

        protected void btn_set_new_Click(object sender, EventArgs e)
        {
            ApproveReject("N");
        }
    }
}