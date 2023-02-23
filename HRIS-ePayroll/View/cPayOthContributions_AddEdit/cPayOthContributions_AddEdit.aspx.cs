//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Other Contribution and Loans
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
//Joseph M. Tombo Jr     03/28/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View.cPayOthContributions_AddEdit
{
    public partial class cPayOthContributions_AddEdit : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Data Place holder creation 
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

        DataTable dtSourceSTG
        {
            get
            {
                if ((DataTable)ViewState["dtSourceSTG"] == null) return null;
                return (DataTable)ViewState["dtSourceSTG"];
            }
            set
            {
                ViewState["dtSourceSTG"] = value;
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

        DataTable dataListGridSTG
        {
            get
            {
                if ((DataTable)ViewState["dataListGridSTG"] == null) return null;
                return (DataTable)ViewState["dataListGridSTG"];
            }
            set
            {
                ViewState["dataListGridSTG"] = value;
            }
        }


        DataTable dataListAccounts
        {
            get
            {
                if ((DataTable)ViewState["dataListAccounts"] == null) return null;
                return (DataTable)ViewState["dataListAccounts"];
            }
            set
            {
                ViewState["dataListAccounts"] = value;
            }
        }

        DataTable dataListEmployee
        {
            get
            {
                if ((DataTable)ViewState["dataListEmployee"] == null) return null;
                return (DataTable)ViewState["dataListEmployee"];
            }
            set
            {
                ViewState["dataListEmployee"] = value;
            }
        }

        DataTable dataListSTG
        {
            get
            {
                if ((DataTable)ViewState["dataListSTG"] == null) return null;
                return (DataTable)ViewState["dataListSTG"];
            }
            set
            {
                ViewState["dataListSTG"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - JVA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "account_title";
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

                // BEGIN - Pass Value
                // Employee ID      [0]
                // Registry         [1]
                // Year             [2]
                // Employment Type  [3]
                // Department       [4]
                // END  - Pass Value


                if (Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"] == null)
                    Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    hiddel_empl_id.Value            = prevValues[0].ToString();
                    ddl_year.SelectedValue          = prevValues[2].ToString();
                    ddl_empl_type.SelectedValue     = prevValues[3].ToString();
                    ddl_empl_type.SelectedValue     = prevValues[3].ToString();
                    txtb_empl_name.Text             = Session["PreviousValuesonPage_EmployeeName"].ToString();
                    txtb_employee_name.Text         = Session["PreviousValuesonPage_EmployeeName"].ToString();
                    RetrieveDataListGrid();
                    btnAdd.Visible = true;
                }
            }
        }

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            RetrieveYear();
            RetrieveEmploymentType();
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPositionTable"] = "cPositionTable";

            RetrieveBindingAccount();
            RetrieveBindingSubAccount();
            RetrieveDataListGrid();
            
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Payroll Year
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
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {

            dataListGrid = MyCmn.RetrieveData("sp_othercontributionloan_tbl_list_empl", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_empl_id", hiddel_empl_id.Value.ToString());
            
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
            
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");

            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_type.Items.Insert(0, li);
        }
        
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            //For Header
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            
            txtb_empl_id.Text = hiddel_empl_id.Value;
            txtb_empl_name.Text = Session["PreviousValuesonPage_EmployeeName"].ToString();

            ddl_account_sub_code.Enabled = true;
            ddl_account_code.Enabled = true;
            ddl_record_status.SelectedValue = "1";
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        private void RetrieveBindingAccount()
        {
            ddl_account_code.Items.Clear();
            DataTable dt1 = MyCmn.RetrieveData("sp_payrollaccounts_tbl_list3");

            ddl_account_code.DataSource = dt1;
            ddl_account_code.DataValueField = "account_code";
            ddl_account_code.DataTextField = "account_title";
            ddl_account_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_account_code.Items.Insert(0, li);
        }
        private void RetrieveBindingSubAccount()
        {
            ddl_account_sub_code.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payroll_subaccount_tbl_list", "par_account_code", ddl_account_code.SelectedValue.ToString().Trim());

            ddl_account_sub_code.DataSource = dt;
            ddl_account_sub_code.DataValueField = "account_sub_code";
            ddl_account_sub_code.DataTextField = "account_sub_title";
            ddl_account_sub_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_account_sub_code.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_empl_id.Text               = "";
            txtb_empl_name.Text             = "";
            txtb_id_ref_dspl.Text           = "";
            txtb_april_amount.Text          = "0.00";
            txtb_august_amount.Text         = "0.00";
            txtb_feb_amount.Text            = "0.00";
            txtb_dec_amount.Text            = "0.00";
            txtb_jan_amount.Text            = "0.00";
            txtb_jul_amount.Text            = "0.00";
            txtb_june_amount.Text           = "0.00";
            txtb_march_amount.Text          = "0.00";
            txtb_may_amount.Text            = "0.00";
            txtb_nov_amount.Text            = "0.00";
            txtb_oct_amount.Text            = "0.00";
            txtb_sep_amount.Text            = "0.00";
            
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("account_code", typeof(System.String));
            dtSource.Columns.Add("account_sub_code", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_01", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_02", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_03", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_04", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_05", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_06", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_07", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_08", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_09", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_10", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_11", typeof(System.String));
            dtSource.Columns.Add("monthly_amount_12", typeof(System.String));
            dtSource.Columns.Add("recrd_status", typeof(System.String));

        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTableSTG()
        {
            dtSourceSTG = new DataTable();
            dtSourceSTG.Columns.Add("empl_id", typeof(System.String));
            dtSourceSTG.Columns.Add("monthly_amount", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTableSTGDisplay()
        {
            dataListGridSTG = new DataTable();
            dataListGridSTG.Columns.Add("empl_id", typeof(System.String));
            dataListGridSTG.Columns.Add("employee_name", typeof(System.String));
            dataListGridSTG.Columns.Add("account_id_nbr_ref", typeof(System.String));
            dataListGridSTG.Columns.Add("monthly_amount", typeof(System.String));
            dataListGridSTG.Columns.Add("include_status", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "othercontributionloan_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year","empl_id", "account_code","account_sub_code" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeysSTG()
        {
            dtSourceSTG.TableName = "othercontributionloan_stg_tbl";
            dtSourceSTG.Columns.Add("action", typeof(System.Int32));
            dtSourceSTG.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { " empl_id"};
            dtSourceSTG = MyCmn.AddPrimaryKeys(dtSourceSTG, col);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();

            nrow["payroll_year"]        = string.Empty;
            nrow["empl_id"]             = string.Empty;
            nrow["account_code"]        = string.Empty;
            nrow["account_sub_code"]    = string.Empty;
            nrow["monthly_amount_01"]   = string.Empty;
            nrow["monthly_amount_02"]   = string.Empty;
            nrow["monthly_amount_03"]   = string.Empty;
            nrow["monthly_amount_04"]   = string.Empty;
            nrow["monthly_amount_05"]   = string.Empty;
            nrow["monthly_amount_06"]   = string.Empty;
            nrow["monthly_amount_07"]   = string.Empty;
            nrow["monthly_amount_08"]   = string.Empty;
            nrow["monthly_amount_09"]   = string.Empty;
            nrow["monthly_amount_10"]   = string.Empty;
            nrow["monthly_amount_11"]   = string.Empty;
            nrow["monthly_amount_12"]   = string.Empty;
            nrow["recrd_status"]        = string.Empty;

            nrow["action"]      = 1;
            nrow["retrieve"]    = false;
            dtSource.Rows.Add(nrow);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRowSTG()
        {
            DataRow nrow = dtSourceSTG.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["monthly_amount"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSourceSTG.Rows.Add(nrow);
        }

        //***************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString().Trim();
            string seq = commandArgs.ToString().Trim();

            deleteRec1.Text = "Are you sure to delete this Account Code = (" + seq.Trim().Split(',')[0] + ") ?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //***************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command_stg(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString().Trim();
            string empl_id = commandArgs.ToString().Trim();

            lbl_delete_confirm_stg.Text = "Are you want to delete this (" + empl_id + ") in STG?";
            btn_delete_stg.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDeleteSTG();", true);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string deleteExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND account_code = '"+ svalues[1].ToString().Trim() + "' AND account_sub_code = '"+ svalues[2].ToString().Trim() + "' AND payroll_year = '"+ svalues[3].ToString().Trim() + "'";
             MyCmn.DeleteBackEndData("othercontributionloan_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command_STG(object sender, CommandEventArgs e)
        {
            string empl_id = e.CommandArgument.ToString().Trim();
            string deleteExpression = "empl_id = '" + empl_id + "'";
            MyCmn.DeleteBackEndData("othercontributionloan_stg_tbl", "WHERE " + deleteExpression);
            DataRow[] row2Delete = dataListGridSTG.Select(deleteExpression);
            dataListGridSTG.Rows.Remove(row2Delete[0]);
            dataListGridSTG.AcceptChanges();
            CommonCode.GridViewBind(ref this.gv_uploaded_list, dataListGridSTG);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopSTG", "closeModalDeleteSTG();", true);
            
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string editExpression = "empl_id = '" + svalues.ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            //This is for HEADER EDIT
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;
            txtb_empl_name.Visible = true;
            txtb_empl_name.Text = Session["PreviousValuesonPage_EmployeeName"].ToString();
            txtb_empl_id.Text = row2Edit[0]["empl_id"].ToString();

            dtSource.Rows[0]["empl_id"] = row2Edit[0]["empl_id"].ToString();
            dtSource.Rows[0]["account_code"] = row2Edit[0]["account_code"].ToString();
            dtSource.Rows[0]["account_sub_code"] = row2Edit[0]["account_sub_code"].ToString();
            dtSource.Rows[0]["payroll_year"] = row2Edit[0]["payroll_year"].ToString();

            txtb_jan_amount.Text = row2Edit[0]["monthly_amount_01"].ToString();
            txtb_feb_amount.Text = row2Edit[0]["monthly_amount_02"].ToString();
            txtb_march_amount.Text = row2Edit[0]["monthly_amount_03"].ToString();
            txtb_april_amount.Text = row2Edit[0]["monthly_amount_04"].ToString();
            txtb_may_amount.Text = row2Edit[0]["monthly_amount_05"].ToString();
            txtb_june_amount.Text = row2Edit[0]["monthly_amount_06"].ToString();
            txtb_jul_amount.Text = row2Edit[0]["monthly_amount_07"].ToString();
            txtb_august_amount.Text = row2Edit[0]["monthly_amount_08"].ToString();
            txtb_sep_amount.Text = row2Edit[0]["monthly_amount_09"].ToString();
            txtb_oct_amount.Text = row2Edit[0]["monthly_amount_10"].ToString();
            txtb_nov_amount.Text = row2Edit[0]["monthly_amount_11"].ToString();
            txtb_dec_amount.Text = row2Edit[0]["monthly_amount_12"].ToString();
            ddl_record_status.SelectedValue = row2Edit[0]["recrd_status"].ToString().ToUpper() == "TRUE" ? "1":"0";
            RetrieveBindingAccount();
            ddl_account_code.SelectedValue = row2Edit[0]["account_code"].ToString();
            RetrieveBindingSubAccount();
            ddl_account_sub_code.SelectedValue = row2Edit[0]["account_sub_code"].ToString();
            //txtb_code_dspl.Text = row2Edit[0]["account_code"].ToString()+" - "+ row2Edit[0]["account_sub_code"].ToString();

            ddl_account_sub_code.Enabled = false;
            ddl_account_code.Enabled = false;
            LabelAddEdit.Text = "Edit Record: ";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command_stg(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string editExpression = "empl_id = '" + svalues.ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGridSTG.Select(editExpression);

            ClearEntry();

            //This is for HEADER EDIT
            InitializeTableSTG();
            AddPrimaryKeysSTG();
            AddNewRowSTG();

            dtSourceSTG.Rows[0]["action"] = 2;
            dtSourceSTG.Rows[0]["retrieve"] = true;
            FieldValidationColorChanged(false, "ALL");
            txtb_stg_empl_id.Text = row2Edit[0]["empl_id"].ToString();
            txtb_stg_name.Text = row2Edit[0]["employee_name"].ToString();
            txtb_stg_amount.Text = row2Edit[0]["monthly_amount"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopSTG", "openModalSTG();", true);
        }

            //**************************************************************************
            //  BEGIN - JVA- 09/20/2018 - Change Field Sort mode  
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
        //  BEGIN - JVA- 09/20/2018 - Change Field Sort mode  
        //**************************************************************************
        protected void gv_dataListGrid_Sorting_stg(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(gv_uploaded_list, dataListGridSTG, e.SortExpression, sortingDirection);
        }
        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Get Grid current sort order 
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
        //  BEGIN - JVA- 09/20/2018 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");

            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;
            string scriptInsertUpdate1 = string.Empty;

            if (IsDataValidated())
            {

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource.Rows[0]["payroll_year"]      = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]           = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["account_code"]      = ddl_account_code.SelectedValue.ToString();
                    dtSource.Rows[0]["account_sub_code"]  = ddl_account_sub_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_01"] = txtb_jan_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_02"] = txtb_feb_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_03"] = txtb_march_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_04"] = txtb_april_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_05"] = txtb_may_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_06"] = txtb_june_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_07"] = txtb_jul_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_08"] = txtb_august_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_09"] = txtb_sep_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_10"] = txtb_oct_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_11"] = txtb_nov_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_12"] = txtb_dec_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["recrd_status"]      = ddl_record_status.SelectedValue.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"] = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["empl_id"] = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["account_code"] = ddl_account_code.SelectedValue.ToString();
                    dtSource.Rows[0]["account_sub_code"] = ddl_account_sub_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_01"] = txtb_jan_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_02"] = txtb_feb_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_03"] = txtb_march_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_04"] = txtb_april_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_05"] = txtb_may_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_06"] = txtb_june_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_07"] = txtb_jul_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_08"] = txtb_august_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_09"] = txtb_sep_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_10"] = txtb_oct_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_11"] = txtb_nov_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_amount_12"] = txtb_dec_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["recrd_status"] = ddl_record_status.SelectedValue.ToString().Trim();

                    //dtSource.Rows[0]["monthly_amount_01"] = txtb_jan_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_02"] = txtb_feb_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_03"] = txtb_march_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_04"] = txtb_april_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_05"] = txtb_may_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_06"] = txtb_june_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_07"] = txtb_jul_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_08"] = txtb_august_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_09"] = txtb_sep_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_10"] = txtb_oct_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_11"] = txtb_nov_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["monthly_amount_12"] = txtb_dec_amount.Text.ToString().Trim();
                    //dtSource.Rows[0]["recrd_status"]      = ddl_record_status.SelectedValue.ToString().Trim();
                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;
                    if (msg.Substring(0, 1) == "X") return;

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["payroll_year"]        = dtSource.Rows[0]["payroll_year"];
                        nrow["empl_id"]             = dtSource.Rows[0]["empl_id"];
                        //nrow["employee_name"]       = ddl_empl_id.SelectedItem.Text.ToString();
                        
                        nrow["monthly_amount_01"]   = double.Parse(dtSource.Rows[0]["monthly_amount_01"].ToString());
                        nrow["monthly_amount_02"]   = double.Parse(dtSource.Rows[0]["monthly_amount_02"].ToString());
                        nrow["monthly_amount_03"]   = double.Parse(dtSource.Rows[0]["monthly_amount_03"].ToString());
                        nrow["monthly_amount_04"]   = double.Parse(dtSource.Rows[0]["monthly_amount_04"].ToString());
                        nrow["monthly_amount_05"]   = double.Parse(dtSource.Rows[0]["monthly_amount_05"].ToString());
                        nrow["monthly_amount_06"]   = double.Parse(dtSource.Rows[0]["monthly_amount_06"].ToString());
                        nrow["monthly_amount_07"]   = double.Parse(dtSource.Rows[0]["monthly_amount_07"].ToString());
                        nrow["monthly_amount_08"]   = double.Parse(dtSource.Rows[0]["monthly_amount_08"].ToString());
                        nrow["monthly_amount_09"]   = double.Parse(dtSource.Rows[0]["monthly_amount_09"].ToString());
                        nrow["monthly_amount_10"]   = double.Parse(dtSource.Rows[0]["monthly_amount_10"].ToString());
                        nrow["monthly_amount_11"]   = double.Parse(dtSource.Rows[0]["monthly_amount_11"].ToString());
                        nrow["monthly_amount_12"]   = double.Parse(dtSource.Rows[0]["monthly_amount_12"].ToString());
                        nrow["recrd_status"]        = dtSource.Rows[0]["recrd_status"].ToString() == "1" ? true : false;
                        nrow["account_code"]        = ddl_account_code.SelectedValue.ToString();
                        nrow["account_sub_code"]    = ddl_account_sub_code.SelectedValue.ToString().Trim();
                        nrow["account_code_combi"]  = ddl_account_code.SelectedValue.ToString() + " - " + ddl_account_sub_code.SelectedValue.ToString();
                        nrow["account_title"]       = ddl_account_code.SelectedItem.ToString();

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + dtSource.Rows[0]["empl_id"].ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                       
                        row2Edit[0]["monthly_amount_01"]    = double.Parse(dtSource.Rows[0]["monthly_amount_01"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_02"]    = double.Parse(dtSource.Rows[0]["monthly_amount_02"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_03"]    = double.Parse(dtSource.Rows[0]["monthly_amount_03"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_04"]    = double.Parse(dtSource.Rows[0]["monthly_amount_04"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_05"]    = double.Parse(dtSource.Rows[0]["monthly_amount_05"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_06"]    = double.Parse(dtSource.Rows[0]["monthly_amount_06"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_07"]    = double.Parse(dtSource.Rows[0]["monthly_amount_07"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_08"]    = double.Parse(dtSource.Rows[0]["monthly_amount_08"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_09"]    = double.Parse(dtSource.Rows[0]["monthly_amount_09"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_10"]    = double.Parse(dtSource.Rows[0]["monthly_amount_10"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_11"]    = double.Parse(dtSource.Rows[0]["monthly_amount_11"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_12"]    = double.Parse(dtSource.Rows[0]["monthly_amount_12"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["recrd_status"]         = dtSource.Rows[0]["recrd_status"].ToString() == "1" ? true:false;

                        row2Edit[0]["account_code"]         = ddl_account_code.SelectedValue.ToString();
                        row2Edit[0]["account_sub_code"]     = ddl_account_sub_code.SelectedValue.ToString().Trim();
                        row2Edit[0]["account_code_combi"]   = ddl_account_code.SelectedValue.ToString() + "-" + ddl_account_sub_code.SelectedValue.ToString();
                        row2Edit[0]["account_title"]        = ddl_account_code.SelectedItem.ToString();

                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

                }
            }
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging_stg(object sender, GridViewPageEventArgs e)
        {
            gv_uploaded_list.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_uploaded_list, dataListGridSTG, "employee_name", "ASC");
        }

        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string amount_field = "";
            switch(DateTime.Now.Month.ToString())
            {
                case "1":
                    {
                        amount_field = "monthly_amount_01";
                        break;
                    }
                case "2":
                    {
                        amount_field = "monthly_amount_02";
                        break;
                    }
                case "3":
                    {
                        amount_field = "monthly_amount_03";
                        break;
                    }
                case "4":
                    {
                        amount_field = "monthly_amount_04";
                        break;
                    }
                case "5":
                    {
                        amount_field = "monthly_amount_05";
                        break;
                    }
                case "6":
                    {
                        amount_field = "monthly_amount_06";
                        break;
                    }
                case "7":
                    {
                        amount_field = "monthly_amount_07";
                        break;
                    }
                case "8":
                    {
                        amount_field = "monthly_amount_08";
                        break;
                    }
                case "9":
                    {
                        amount_field = "monthly_amount_09";
                        break;
                    }
                case "10":
                    {
                        amount_field = "monthly_amount_10";
                        break;
                    }
                case "11":
                    {
                        amount_field = "monthly_amount_11";
                        break;
                    }
                case "12":
                    {
                        amount_field = "monthly_amount_12";
                        break;
                    }
            }
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR "+ amount_field + " LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' ";

            DataTable dtSource1 = new DataTable();

            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("account_code", typeof(System.String));
            dtSource1.Columns.Add("account_sub_code", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_01", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_02", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_03", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_04", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_05", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_06", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_07", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_08", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_09", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_10", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_11", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount_12", typeof(System.String));
            dtSource1.Columns.Add("recrd_status", typeof(System.String));

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

        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged_stg(object sender, EventArgs e)
        {
            
            string searchExpression = "empl_id LIKE '%" + txtb_search_stg.Text.Trim().Replace("'", "''").Replace("%","") + "%' OR employee_name LIKE '%" + txtb_search_stg.Text.Trim().Replace("'", "''").Replace("%", "") + "%' OR monthly_amount LIKE '%" + txtb_search_stg.Text.Trim().Replace("'", "''").Replace("%", "") + "%' ";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("monthly_amount", typeof(System.String));

            DataRow[] rows = dataListGridSTG.Select(searchExpression);
            dtSource1.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource1.ImportRow(row);
                }
            }
            MyCmn.Sort(gv_uploaded_list, dtSource1, "employee_name", "ASC");
            txtb_search_stg.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search_stg.Focus();
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Define Property for Sort Direction  
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
        //  BEGIN - JVA- 09/20/2018 - Check if Object already contains value  
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
        //  BEGIN - JVA- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");


            if (ddl_account_code.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_account_code");
                ddl_account_code.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_jan_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_jan_amount");
                txtb_jan_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_jan_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "jan-amount");
                txtb_jan_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_feb_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_feb_amount");
                txtb_feb_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_feb_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "feb-amount");
                txtb_feb_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_march_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_march_amount");
                txtb_march_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_march_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "march-amount");
                txtb_march_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_april_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_april_amount");
                txtb_april_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_april_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "april-amount");
                txtb_april_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_may_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_may_amount");
                txtb_may_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_may_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "may-amount");
                txtb_may_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_june_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_june_amount");
                txtb_june_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_june_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "june-amount");
                txtb_june_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_jul_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_jul_amount");
                txtb_jul_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_jul_amount.Text) < 0 )
            {
                FieldValidationColorChanged(true, "jul-amount");
                txtb_jul_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_august_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_august_amount");
                txtb_august_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_august_amount.Text) < 0 )
            {
                FieldValidationColorChanged(true, "august-amount");
                txtb_august_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_sep_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_sep_amount");
                txtb_sep_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_sep_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "sep-amount");
                txtb_sep_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_oct_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_oct_amount");
                txtb_oct_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_oct_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "oct-amount");
                txtb_oct_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_nov_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_nov_amount");
                txtb_nov_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_nov_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "nov-amount");
                txtb_nov_amount.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_dec_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_dec_amount");
                txtb_dec_amount.Focus();
                validatedSaved = false;
            }
            else if (double.Parse(txtb_dec_amount.Text) < 0)
            {
                FieldValidationColorChanged(true, "txtb_dec_amount");
                txtb_dec_amount.Focus();
                validatedSaved = false;
            }

            return validatedSaved;

        }
        //**********************************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "txtb_empl_name":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_empl_name.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_account_code":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_account_code.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_empl_id":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_id_ref_dspl":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            txtb_id_ref_dspl.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_jan_amount":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_jan_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "jan-amount":
                        {
                            LblRequired6.Text = "Should not be less that 0.";
                            txtb_jan_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_feb_amount":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_feb_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "feb-amount":
                        {
                            LblRequired7.Text ="should not be less than 0";
                            txtb_feb_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_march_amount":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_march_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "march-amount":
                        {
                            LblRequired8.Text = "should not bet less than 0 ";
                            txtb_march_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_april_amount":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_april_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "april-amount":
                        {
                            LblRequired9.Text = "Should not be less than 0";
                            txtb_april_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_may_amount":
                        {
                            LblRequired10.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_may_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "may-amount":
                        {
                            LblRequired10.Text = "Should not be less than 0";
                            txtb_may_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_june_amount":
                        {
                            LblRequired11.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_june_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "june-amount":
                        {
                            LblRequired11.Text = "should not be less than 0";
                            txtb_june_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_jul_amount":
                        {
                            LblRequired12.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_jul_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "jul-amount":
                        {
                            LblRequired12.Text = "Should not be less than 0";
                            txtb_jul_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_august_amount":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_august_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "august-amount":
                        {
                            LblRequired13.Text = "should not be less than 0";
                            txtb_august_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_sep_amount":
                        {
                            LblRequired14.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_sep_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "sep-amount":
                        {
                            LblRequired14.Text = "should not be less than 0";
                            txtb_sep_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_oct_amount":
                        {
                            LblRequired15.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_oct_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "oct-amount":
                        {
                            LblRequired15.Text = "should not be less than 0";
                            txtb_oct_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_nov_amount":
                        {
                            LblRequired16.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nov_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "nov-amount":
                        {
                            LblRequired16.Text = "should not be less than 0";
                            txtb_nov_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_dec_amount":
                        {
                            LblRequired17.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_dec_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "dec-amount":
                        {
                            LblRequired17.Text ="should not be less than 0";
                            txtb_dec_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_stg_empl_id":
                        {
                            lbl_stg_empl_id.Text = MyCmn.CONST_RQDFLD;
                            txtb_stg_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_stg_amount":
                        {
                            lbl_stg_amount.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_stg_amount.BorderColor = Color.Red;
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
                            LblRequired5.Text   = "";
                            LblRequired6.Text   = "";
                            LblRequired8.Text   = "";
                            LblRequired7.Text   = "";
                            LblRequired9.Text   = "";
                            LblRequired10.Text  = "";
                            LblRequired11.Text  = "";
                            LblRequired12.Text  = "";
                            LblRequired13.Text  = "";
                            LblRequired14.Text  = "";
                            LblRequired15.Text  = "";
                            LblRequired16.Text  = "";
                            LblRequired17.Text  = "";
                            lbl_stg_amount.Text  = "";
                            lbl_stg_empl_id.Text  = "";

                            txtb_april_amount.BorderColor       = Color.LightGray;
                            txtb_august_amount.BorderColor      = Color.LightGray;
                            txtb_dec_amount.BorderColor         = Color.LightGray;
                            txtb_empl_id.BorderColor            = Color.LightGray;
                            txtb_empl_name.BorderColor          = Color.LightGray;
                            txtb_feb_amount.BorderColor         = Color.LightGray;
                            txtb_id_ref_dspl.BorderColor        = Color.LightGray;
                            txtb_jan_amount.BorderColor         = Color.LightGray;
                            txtb_jul_amount.BorderColor         = Color.LightGray;
                            txtb_june_amount.BorderColor        = Color.LightGray;
                            txtb_march_amount.BorderColor       = Color.LightGray;
                            txtb_may_amount.BorderColor         = Color.LightGray;
                            txtb_nov_amount.BorderColor         = Color.LightGray;
                            txtb_oct_amount.BorderColor         = Color.LightGray;
                            txtb_sep_amount.BorderColor         = Color.LightGray;
                            txtb_stg_empl_id.BorderColor        = Color.LightGray;
                            txtb_stg_amount.BorderColor         = Color.LightGray;
                            ddl_account_code.BorderColor        = Color.LightGray;
                            break;
                        }

                }
            }
        }
        
        protected void btn_back_Click(object sender, EventArgs e)
        {
            string url = "";
            if (ddl_empl_type.SelectedValue == "RE")
            {
                url = "/View/cPayRegistrySalaryRegular/cPayRegistrySalaryRegular.aspx";
            }
            else if (ddl_empl_type.SelectedValue == "CE")
            {
                url = "/View/cPayRegistrySalaryCasual/cPayRegistrySalaryCasual.aspx";
            }
            else
            {
                url = "/View/cPayRegistryJO/cPayRegistryJO.aspx";
            }
            Response.Redirect(url);
        }
        protected void ddl_account_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_account_code.SelectedValue != "")
            {
                RetrieveBindingSubAccount();
            }
        }
        
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}