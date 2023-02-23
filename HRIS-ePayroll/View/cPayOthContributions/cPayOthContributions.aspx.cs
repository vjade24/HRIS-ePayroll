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


namespace HRIS_ePayroll.View.cPayOthContributions
{
    public partial class cPayOthContributions : System.Web.UI.Page
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
        DataTable dtSource_dtl_upload
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_upload"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_upload"];
            }
            set
            {
                ViewState["dtSource_dtl_upload"] = value;
            }
        }
        DataTable dtSource_dtl_rejected
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_rejected"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_rejected"];
            }
            set
            {
                ViewState["dtSource_dtl_rejected"] = value;
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
                    Session["SortField"] = "employee_name";
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

                if (Session["PreviousValuesonPage_cPayOthCOntributions"] == null)
                    Session["PreviousValuesonPage_cPayOthCOntributions"] = "";
                else if (Session["PreviousValuesonPage_cPayOthCOntributions"].ToString() != string.Empty)
                {
                    RetrieveYear();
                    string[] prevValues = Session["PreviousValuesonPage_cPayOthCOntributions"].ToString().Split(new char[] { ',' });
                    ddl_year.SelectedValue              = prevValues[0].ToString();
                    ddl_month.SelectedValue             = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue         = prevValues[2].ToString();
                    RetrieveAccountType_main();
                    ddl_accttype_main.SelectedValue     = prevValues[3].ToString();
                    ddl_loan_account_name.SelectedValue = prevValues[4].ToString();
                    RetrieveDeductionList(); 
                    ddl_accttype_main.SelectedValue     = prevValues[5].ToString();
                    RetrieveDataListGrid();
                    gv_dataListGrid.PageIndex           = int.Parse(prevValues[6].ToString());
                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                    up_dataListGrid.Update();
                    DropDownListID.SelectedValue = prevValues[7].ToString();
                    txtb_search.Text = prevValues[8].ToString();
                    // SearchData(prevValues[8].ToString());
                    btn_options.Visible = true;
                    Update_btnAdd.Update();
                }
            }
        }

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"]    = SortDirection.Ascending.ToString();
            Session["cPositionTable"]   = "cPositionTable";

            RetrieveYear();
            RetrieveEmploymentType();
            RetrieveDeductionList();
            RetrieveAccountType_main();

            if (ddl_year.SelectedValue.ToString().Trim() != "" 
                && ddl_month.SelectedValue.ToString().Trim() != "" 
                && ddl_empl_type.SelectedValue.ToString().Trim() != ""
                )
            {
                btn_options.Visible = true;
            }
            else
            {
                btn_options.Visible = false;
            }

            RetrieveDataListGrid();
            Update_btnAdd.Update();
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
        //  BEGIN - JVA- 09/20/2018 - Populate Combo list for Employment Type
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
        //  BEGIN - VJA- 2019/11/04 - Populate Combo list for Account Type
        //*************************************************************************
        private void RetrieveAccountType_main()
        {
            ddl_accttype_main.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_deduct_accttype_list1");

            ddl_accttype_main.DataSource = dt;
            ddl_accttype_main.DataValueField = "accttype_code";
            ddl_accttype_main.DataTextField = "accttype_descr";
            ddl_accttype_main.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_accttype_main.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Populate Combo list for Loan Account Name
        //*************************************************************************
        private void RetrieveDeductionList()
        {
            ddl_loan_account_name.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_deduct_accounts_list1", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_accttype_code", ddl_accttype_main.SelectedValue.ToString().Trim());

            ddl_loan_account_name.DataSource = dt;
            ddl_loan_account_name.DataValueField = "deduc_code";
            ddl_loan_account_name.DataTextField = "deduc_descr";
            ddl_loan_account_name.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_loan_account_name.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrolldeduc_ledger_stg_tbl_list1", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_deduc_code", ddl_loan_account_name.SelectedValue.ToString().Trim(), "p_payroll_year",ddl_year.SelectedValue.ToString(), "p_payroll_month", ddl_month.SelectedValue.ToString());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid_dtl_uploaded()
        {
            dtSource_dtl_upload = MyCmn.RetrieveData("sp_payrolldeduc_ledger_stg_tbl_list2", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_deduc_code", ddl_loan_account_name.SelectedValue.ToString().Trim() , "p_empl_id", txtb_empl_id.Text.ToString().Trim(), "p_payroll_year", ddl_year.SelectedValue.ToString(), "p_payroll_month", ddl_month.SelectedValue.ToString());
            MyCmn.Sort(gv_dtl_uploaded, dtSource_dtl_upload, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            update_panel_dtl_uploaded.Update();
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
           

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
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string deleteExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND payroll_year = '" + svalues[1].ToString().Trim() + "' AND payroll_month = '" + svalues[2].ToString().Trim() + "' AND deduc_code = '" + svalues[3].ToString().Trim() + "'";
             MyCmn.DeleteBackEndData("payrolldeduc_ledger_stg_tbl", "WHERE " + deleteExpression);
             MyCmn.DeleteBackEndData("payrolldeduc_ledger_upd_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
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
            
            txtb_empl_id.Text           = row2Edit[0]["empl_id"].ToString();
            txtb_employee_name.Text     = row2Edit[0]["employee_name"].ToString();
            txtb_deduction.Text         = ddl_loan_account_name.SelectedItem.ToString();
            txtb_deduction_code.Text    = ddl_loan_account_name.SelectedValue.ToString();

            RetrieveDataListGrid_dtl_uploaded();

            LabelAddEdit.Text = "Edit Record: ";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                    //dtSource.Rows[0]["payroll_year"]      = ddl_year.SelectedValue.ToString().Trim();
                    //dtSource.Rows[0]["empl_id"]           = ddl_empl_id.SelectedValue.ToString().Trim();
                    //dtSource.Rows[0]["account_code"]      = Get_account_code();
                    //dtSource.Rows[0]["account_sub_code"]  = ddl_sub_account.SelectedValue.ToString().Trim();
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

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
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
                        nrow["account_code"]        = dtSource.Rows[0]["account_code"];
                        nrow["account_sub_code"]    = dtSource.Rows[0]["account_sub_code"];
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

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + dtSource.Rows[0]["empl_id"].ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                       
                        row2Edit[0]["monthly_amount_01"] = double.Parse(dtSource.Rows[0]["monthly_amount_01"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_02"] = double.Parse(dtSource.Rows[0]["monthly_amount_02"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_03"] = double.Parse(dtSource.Rows[0]["monthly_amount_03"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_04"] = double.Parse(dtSource.Rows[0]["monthly_amount_04"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_05"] = double.Parse(dtSource.Rows[0]["monthly_amount_05"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_06"] = double.Parse(dtSource.Rows[0]["monthly_amount_06"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_07"] = double.Parse(dtSource.Rows[0]["monthly_amount_07"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_08"] = double.Parse(dtSource.Rows[0]["monthly_amount_08"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_09"] = double.Parse(dtSource.Rows[0]["monthly_amount_09"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_10"] = double.Parse(dtSource.Rows[0]["monthly_amount_10"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_11"] = double.Parse(dtSource.Rows[0]["monthly_amount_11"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["monthly_amount_12"] = double.Parse(dtSource.Rows[0]["monthly_amount_12"].ToString()).ToString("###,##0.00");
                        row2Edit[0]["recrd_status"]      = dtSource.Rows[0]["recrd_status"].ToString() == "1" ? true:false;

                        
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
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " ;

            DataTable dtSource1 = new DataTable();

            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("deduc_code", typeof(System.String));
            dtSource1.Columns.Add("deduc_seq", typeof(System.String));
            dtSource1.Columns.Add("uploaded_amount", typeof(System.String));
            dtSource1.Columns.Add("uploaded_by_user", typeof(System.String));
            dtSource1.Columns.Add("uploaded_dttm", typeof(System.String));
            dtSource1.Columns.Add("updated_by_user", typeof(System.String));
            dtSource1.Columns.Add("updated_dttm", typeof(System.String));
            dtSource1.Columns.Add("uploaded_loanamount", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_month", typeof(System.String));

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

            //if (double.Parse(txtb_dec_amount.Text) < 0)
            //{
            //    FieldValidationColorChanged(true, "txtb_dec_amount");
            //    txtb_dec_amount.Focus();
            //    validatedSaved = false;
            //}

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

                    case "dec-amount":
                        {
                            //LblRequired17.Text = "should not be less than 0";
                            //txtb_dec_amount.BorderColor = Color.Red;
                            break;
                        }

                }
            else if (!pMode)
            {
                switch (pObjectName)
                {

                    case "ALL":
                        {
                           
                           // LblRequired17.Text  = "";
                           
                            //txtb_sep_amount.BorderColor         = Color.LightGray;
                            break;
                        }

                }
            }
        }
        
        protected void LinkButton5_Command(object sender, CommandEventArgs e)
        {
            DataTable dt = MyCmn.RetrieveData("sp_othercontributionloan_stg_tbl_save", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_account_code", ddl_loan_account_name.SelectedValue.ToString().Trim(), "par_account_sub_code", ddl_loan_account_name.SelectedValue.ToString().Trim());
            if (dt == null)
            {
                lbl_notification.Text = "ERROR ON SAVING TO OTHER CONTRIBUTION";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotification0", "openNotification();", true);
            }
            else if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["run_status"].ToString() == "N")
                {
                    lbl_notification.Text = "Data not Uploaded, Error in SP";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotification1", "openNotification();", true);
                }
                else if (dt.Rows[0]["run_status"].ToString() == "0")
                {
                    lbl_notification.Text = "No Data Uploaded";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotification2", "openNotification();", true);
                }
                else if (dt.Rows[0]["run_status"].ToString() == "Y")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotification2", "closeModal_Upload();", true);
                    RetrieveDataListGrid();
                }
            }
        }
        
        
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Button Upload File
        //*************************************************************************
        protected void lnkbtn_upload_file_Click(object sender, EventArgs e)
        {
            ClearEntry();
            txtb_year_dslp.Text = ddl_year.SelectedItem.ToString();
            txtb_month_dslp.Text = ddl_month.SelectedItem.ToString();
            txtb_account_title_dspl2.Text = ddl_loan_account_name.SelectedItem.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopX", "openModal_Upload();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Button Post to Ledger
        //*************************************************************************
        protected void lnkbtn_post_to_ledger_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopY", "openModal_PosttoLedger();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Button Show Uploaded
        //*************************************************************************
        protected void lnkbtn_show_uploaded_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopY", " openModal_ShowUploaded();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Select Employment Type
        //*************************************************************************
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_month.SelectedValue.ToString().Trim() != ""
                   && ddl_year.SelectedValue.ToString().Trim() != ""
                   && ddl_empl_type.SelectedValue.ToString().Trim() != ""
                   && ddl_accttype_main.SelectedValue.ToString().Trim() != ""
                   && ddl_loan_account_name.SelectedValue.ToString().Trim() != "")
            {
                btn_options.Visible = true;
            }
            else
            {
                btn_options.Visible = false;
            }
            RetrieveDataListGrid();
            RetrieveDeductionList();
            Update_btnAdd.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Select Decution Name
        //*************************************************************************
        protected void ddl_loan_account_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_month.SelectedValue.ToString().Trim() != ""
                   && ddl_year.SelectedValue.ToString().Trim() != ""
                   && ddl_empl_type.SelectedValue.ToString().Trim() != ""
                   && ddl_accttype_main.SelectedValue.ToString().Trim() != ""
                   && ddl_loan_account_name.SelectedValue.ToString().Trim() != "")
            {
                btn_options.Visible = true;
            }
            else
            {
                btn_options.Visible = false;
            }
            RetrieveDataListGrid();
            Update_btnAdd.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Select Account Type
        //*************************************************************************
        protected void ddl_accttype_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_month.SelectedValue.ToString().Trim() != ""
                   && ddl_year.SelectedValue.ToString().Trim() != ""
                   && ddl_empl_type.SelectedValue.ToString().Trim() != ""
                   && ddl_accttype_main.SelectedValue.ToString().Trim() != ""
                   && ddl_loan_account_name.SelectedValue.ToString().Trim() != "")
            {
                btn_options.Visible = true;
            }
            else
            {
                btn_options.Visible = false;
            }
            RetrieveDataListGrid();
            RetrieveDeductionList();
            Update_btnAdd.Update();
        }

        protected void lnkbtn_exec_upload_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopZ", "ExecuteUpload_File();", true);
            InitializeTable();
            RetrieveDataListGrid();

            Session["PreviousValuesonPage_cPayOthCOntributions"] =
                            ddl_year.SelectedValue               + "," +
                            ddl_month.SelectedValue              + "," +
                            ddl_empl_type.SelectedValue          + "," +
                            ddl_accttype_main.SelectedValue      + "," +
                            ddl_loan_account_name.SelectedValue  + "," +
                            ddl_accttype_main.SelectedValue      + "," +
                             gv_dataListGrid.PageIndex           + "," +
                            DropDownListID.SelectedValue         + "," +
                            txtb_search.Text;


            //MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            //gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            //show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            //up_dataListGrid.Update();
        }

        protected void lnkbtn_show_rejected_Click(object sender, EventArgs e)
        {
            // dtSource_dtl_rejected.Columns.Add("empl_id", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("empl_lastname", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("empl_firstname", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("pb_nbr", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("granted_amt", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("monthly_amt", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("date_start", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("date_end", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("remarks", typeof(System.String));
            // dtSource_dtl_rejected.Columns.Add("filler1", typeof(System.String));

            dtSource_dtl_rejected = MyCmn.RetrieveData("sp_payrolldeduc_ledger_rejected_list", "p_deduc_code",ddl_loan_account_name.SelectedValue.ToString(), "p_payroll_year", ddl_year.SelectedValue.ToString(), "p_payroll_month", ddl_month.SelectedValue.ToString());
            MyCmn.Sort(gv_show_rejected, dtSource_dtl_rejected, "empl_id", Session["SortOrder"].ToString());
            update_panel_show_rejected.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopZ", "openModal_RejectedList();", true);
        }

        protected void gv_dtl_uploaded_Sorting(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(gv_dtl_uploaded, dtSource_dtl_upload, e.SortExpression, sortingDirection);
        }

        protected void gv_dtl_uploaded_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dtl_uploaded.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dtl_uploaded, dtSource_dtl_upload, Session["SortField"].ToString(), Session["SortOrder"].ToString());
        }

        protected void ddl_panel_dtl_uploaded_SelectedIndexChanged(object sender, EventArgs e)
        {
            gv_dtl_uploaded.PageSize = Convert.ToInt32(ddl_panel_dtl_uploaded.SelectedValue.ToString());
            MyCmn.Sort(gv_dtl_uploaded, dtSource_dtl_upload, Session["SortField"].ToString(), Session["SortOrder"].ToString());
        }

        protected void lnkbtn_exec_post_to_ledger_Click(object sender, EventArgs e)
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrolldeduc_ledger_stg_posting" , "p_deduc_code", ddl_loan_account_name.SelectedValue.ToString(), "p_payroll_year", ddl_year.SelectedValue.ToString(), "p_payroll_month", ddl_month.SelectedValue.ToString(), "p_user_id", Session["ep_user_id"].ToString(),"p_employment_type",ddl_empl_type.SelectedValue.ToString().Trim());

            if (dt.Rows.Count > 0 || dt.Rows == null)
            {
                if (dt.Rows[0]["run_status"].ToString() == "Y")
                {
                    icon_message.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                    SaveAddEdit.Text = "Successfully Posted";
                    
                }
                else
                {
                    icon_message.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-danger");
                    SaveAddEdit.Text = "Failed to Post";
                }
                header_message.InnerText = dt.Rows[0]["run_message"].ToString();
            }
            RetrieveDataListGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopZ", "closeModal_PosttoLedger();", true);
        }
        
        protected void Dropdown_rejected_SelectedIndexChanged(object sender, EventArgs e)
        {
            gv_show_rejected.PageSize = Convert.ToInt32(Dropdown_rejected.SelectedValue.ToString());
            MyCmn.Sort(gv_show_rejected, dtSource_dtl_rejected, "empl_id", Session["SortOrder"].ToString());
            update_panel_show_rejected.Update();
        }

        protected void gv_show_rejected_Sorting(object sender, GridViewSortEventArgs e)
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
            Session["SortField"] = "empl_id";
            Session["SortOrder"] = sortingDirection;

            MyCmn.Sort(gv_show_rejected, dtSource_dtl_rejected, e.SortExpression, sortingDirection);
            //update_panel_show_rejected.Update();
        }

        protected void gv_show_rejected_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_show_rejected.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_show_rejected, dtSource_dtl_rejected, "empl_id", Session["SortOrder"].ToString());
            //update_panel_show_rejected.Update();
        }

        //********************************************************************
        // END OF THE CODE
        //********************************************************************

    }
}