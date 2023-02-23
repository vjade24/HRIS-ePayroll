//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                     DATE           PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     01/17/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cPayAccountLedger_old
{
    public partial class cPayAccountLedger_old : System.Web.UI.Page
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
        DataTable dtSource_for_dtl
        {
            get
            {
                if ((DataTable)ViewState["dtSource_for_dtl"] == null) return null;
                return (DataTable)ViewState["dtSource_for_dtl"];
            }
            set
            {
                ViewState["dtSource_for_dtl"] = value;
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
                    Session["SortField"] = "ledger_seq_no";
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

                // BEGIN - Pass Value
                // Employee ID      [0]
                // Registry         [1]
                // Year             [2]
                // Employment Type  [3]
                // Department       [4]
                // END   - Pass Value

                if (Session["PreviousValuesonPage_cPayRegistry_RECEJO"] == null)
                    Session["PreviousValuesonPage_cPayRegistry_RECEJO"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry_RECEJO"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry_RECEJO"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    //RetrieveEmploymentType();
                    ddl_year.SelectedValue = prevValues[2].ToString();
                    ddl_empl_type.SelectedValue = prevValues[3].ToString();
                    ddl_department.SelectedValue = prevValues[4].ToString();
                    RetrieveEmpl();
                    ddl_empl_name.SelectedValue = prevValues[0].ToString();
                    
                    RetrieveDataListGrid();
                    lnkbtn_back.Visible = true;
                    btnAdd.Visible = true;
                }
            }

            
        }

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            RetrieveBindingDepartments();
            RetrieveEmpl();
            RetrieveEmploymentType();

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPositionTable"] = "cPositionTable";

            RetrieveDataListGrid();
            RetrieveYear();
            btnAdd.Visible = false;
            lnkbtn_back.Visible = false;
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
            dataListGrid = MyCmn.RetrieveData("sp_payrollemployeeaccountledger_hdr_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_empl_id", ddl_empl_name.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        //private void RetrieveDataListGrid_dtl()
        //{
        //    dtSource_for_dtl = MyCmn.RetrieveData("sp_payrollemployeeaccountledger_paid_list", "par_ledger_seq_no", txtb_lbl_ledger_seq.Text.ToString().Trim());
        //    CommonCode.GridViewBind(ref this.gv_datalistgrid_for_dtl, dtSource_for_dtl);
        //    panel_datalistgrid_dtl.Update();
        //}
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Employment Type
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
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Employee Name
        //*************************************************************************
        private void RetrieveEmpl()
        {
            ddl_empl_name.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_personnelnames_combolist14", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim());

            ddl_empl_name.DataSource = dt;
            ddl_empl_name.DataValueField = "empl_id";
            ddl_empl_name.DataTextField = "employee_name";
            ddl_empl_name.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_name.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Department
        //*************************************************************************
        private void RetrieveBindingDepartments()
        {
            ddl_department.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_departments_tbl_list", "par_include_history", "N");

            ddl_department.DataSource = dt;
            ddl_department.DataValueField = "department_code";
            ddl_department.DataTextField = "department_name1";
            ddl_department.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_department.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Account Code/Description
        //*************************************************************************
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
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Sub-Account Code/Description
        //*************************************************************************
        private void RetrieveBindingSubAccount()
        {
            ddl_subaccount_code.Items.Clear();
            DataTable dt1 = MyCmn.RetrieveData("sp_payroll_subaccount_tbl_list3", "par_account_code", ddl_account_code.SelectedValue.ToString().Trim());

            ddl_subaccount_code.DataSource = dt1;
            ddl_subaccount_code.DataValueField = "account_sub_code";
            ddl_subaccount_code.DataTextField = "account_sub_title";
            ddl_subaccount_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_subaccount_code.Items.Insert(0, li);
        }
        //*************************************************************************
        //  JADE - Jade- 09/28/2018 - Get the Last Control Number
        //*************************************************************************
        private string RetrieveControlNumber()
        {
            // Need SP to Call the last row of the Table
            string sql1 = "SELECT TOP 1 ledger_seq_no from payrollemployeeaccountledger_hdr_tbl where LEFT(ledger_seq_no,12)=LEFT(ledger_seq_no,12) order by ledger_seq_no DESC";
            string last_row = MyCmn.GetLastRow_of_Table(sql1);
            if (last_row.Trim() == "")
            {
                last_row = "0";
            }
            int last_row1 = int.Parse(last_row) + 1;
            txtb_lbl_ledger_seq.Text = last_row1.ToString().PadLeft(12, '0');

            return last_row1.ToString().PadLeft(12, '0'); 
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Retrieve Account code and Sub Account Code
            RetrieveBindingAccount();
            RetrieveBindingSubAccount();
            
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            
            //Retrive Ledger Sequence Number
            RetrieveControlNumber();

            //RetrieveDataListGrid_dtl();
            id_hide_payment_details.Visible = false;

            ddl_account_code.Enabled = true;
            ddl_subaccount_code.Enabled = true;
            txtb_date_from.Visible = true;
            txtb_date_from_disabled.Visible = false;

            txtb_empl_id.Text = ddl_empl_name.SelectedItem.ToString().Trim();

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
            txtb_empl_id.Text               = "";
            txtb_account_amount.Text        = "0.00";
            txtb_date_from.Text             = "";
            txtb_date_to.Text               = "";
            txtb_check_ref_no.Text          = "";

            ddl_ledger_type.SelectedIndex   = 0;
            ddl_ledger_status.SelectedIndex = 0;
            txtb_amount_paid.Text           = "0.00";
            txtb_amount_deduct1.Text        = "0.00";
            txtb_amount_deduct2.Text        = "0.00";
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialized datasource fields/columns - Header
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("ledger_seq_no", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("account_code", typeof(System.String));
            dtSource.Columns.Add("account_sub_code", typeof(System.String));
            dtSource.Columns.Add("date_from", typeof(System.String));
            dtSource.Columns.Add("date_to", typeof(System.String));
            dtSource.Columns.Add("account_amount", typeof(System.String));
            dtSource.Columns.Add("account_amount_paid", typeof(System.String));
            dtSource.Columns.Add("account_amount_deduct1", typeof(System.String));
            dtSource.Columns.Add("account_amount_deduct2", typeof(System.String));
            dtSource.Columns.Add("account_period_no_of_months", typeof(System.String));
            dtSource.Columns.Add("check_ref_no", typeof(System.String));
            dtSource.Columns.Add("ledger_status", typeof(System.String));
            dtSource.Columns.Add("ledger_type", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialized datasource fields/columns - Details
        //*************************************************************************
        private void InitializeTable_dtl()
        {
            dtSource_for_dtl = new DataTable();
            dtSource_for_dtl.Columns.Add("ledger_seq_no", typeof(System.String));
            dtSource_for_dtl.Columns.Add("posted_date", typeof(System.String));
            dtSource_for_dtl.Columns.Add("posted_amount", typeof(System.String));
    }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add Primary Key Field to datasource - Header
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollemployeeaccountledger_hdr_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "ledger_seq_no" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add Primary Key Field to datasource - Details
        //*************************************************************************
        private void AddPrimaryKeys_dtl()
        {
            dtSource_for_dtl.TableName = "payrollemployeeaccountledger_dtl_tbl";
            dtSource_for_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_for_dtl.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "ledger_seq_no", "line_seq_no" };
            dtSource_for_dtl = MyCmn.AddPrimaryKeys(dtSource_for_dtl, col);
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["ledger_seq_no"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["account_code"] = string.Empty;
            nrow["account_sub_code"] = string.Empty;
            nrow["date_from"] = string.Empty;
            nrow["date_to"] = string.Empty;
            nrow["account_amount"] = string.Empty;
            nrow["account_amount_paid"] = string.Empty;
            nrow["account_amount_deduct1"] = string.Empty;
            nrow["account_amount_deduct2"] = string.Empty;
            nrow["account_period_no_of_months"] = string.Empty;
            nrow["check_ref_no"] = string.Empty;
            nrow["ledger_status"] = string.Empty;
            nrow["ledger_type"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString().Trim();
            string seq = commandArgs.ToString().Trim();

            deleteRec1.Text = "Are you sure to delete this Account Code = (" + seq.Trim() + ") ?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();

            string deleteExpression = "ledger_seq_no = '" + svalues.ToString().Trim() + "'";

            MyCmn.DeleteBackEndData("payrollemployeeaccountledger_hdr_tbl", "WHERE " + deleteExpression);
            
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

            string editExpression = "ledger_seq_no = '" + svalues.ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            //This is for HEADER EDIT
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;
            
            //Retrieve Account code and Sub Account Code
            RetrieveBindingAccount();
            RetrieveBindingSubAccount();

            txtb_lbl_ledger_seq.Text = svalues.ToString().Trim();
            ddl_account_code.SelectedValue = row2Edit[0]["account_code"].ToString();

            if (ddl_subaccount_code.SelectedValue.ToString() == "")
            {
                RetrieveBindingSubAccount();
                ddl_subaccount_code.SelectedValue = row2Edit[0]["account_sub_code"].ToString();
            }
            txtb_empl_id.Text                       = ddl_empl_name.SelectedItem.ToString();
            txtb_date_from.Text                     = row2Edit[0]["date_from"].ToString();
            txtb_date_to.Text                       = row2Edit[0]["date_to"].ToString();
            txtb_account_amount.Text                = row2Edit[0]["account_amount"].ToString();
            txtb_check_ref_no.Text                  = row2Edit[0]["check_ref_no"].ToString();

            // New Fields 03/14/2019
            ddl_ledger_type.SelectedValue           = row2Edit[0]["ledger_type"].ToString();
            ddl_ledger_status.SelectedValue         = row2Edit[0]["ledger_status"].ToString();
            txtb_amount_paid.Text                   = row2Edit[0]["account_amount_paid"].ToString();
            txtb_amount_deduct1.Text                 = row2Edit[0]["account_amount_deduct1"].ToString();
            txtb_amount_deduct2.Text                 = row2Edit[0]["account_amount_deduct2"].ToString();
            
            //<<<<--------This SP is for during edit only--------->>>>
            InitializeTable_dtl();
            dtSource_for_dtl = MyCmn.RetrieveData("sp_payrollemployeeaccountledger_paid_list", "par_ledger_seq_no", svalues.ToString().Trim());
            AddPrimaryKeys_dtl();
            //foreach (DataRow nrow in dtSource_for_dtl.Rows)
            //{
            //    nrow["action"] = 1;
            //    nrow["retrieve"] = false;
            //}
            CommonCode.GridViewBind(ref this.gv_datalistgrid_for_dtl, dtSource_for_dtl);
            panel_datalistgrid_dtl.Update();
            //------------END----------------------

            LabelAddEdit.Text = "Edit Record: " + txtb_lbl_ledger_seq.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;

            id_hide_payment_details.Visible = false;
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
                for_account_period_no_of_months();

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource.Rows[0]["ledger_seq_no"]                        = RetrieveControlNumber().ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                              = ddl_empl_name.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_code"]                         = ddl_account_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_sub_code"]                     = ddl_subaccount_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["date_from"]                            = txtb_date_from.Text.ToString().Trim();
                    dtSource.Rows[0]["date_to"]                              = txtb_date_to.Text.ToString().Trim();
                    dtSource.Rows[0]["account_amount"]                       = txtb_account_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["account_amount_paid"]                  = txtb_amount_paid.Text.ToString().Trim();
                    dtSource.Rows[0]["account_amount_deduct1"]                = txtb_amount_deduct1.Text.ToString().Trim();
                    dtSource.Rows[0]["account_amount_deduct2"]                = txtb_amount_deduct2.Text.ToString().Trim();
                    dtSource.Rows[0]["account_period_no_of_months"]          = hidden_account_period_no_of_months.Text;
                    dtSource.Rows[0]["check_ref_no"]                         = txtb_check_ref_no.Text.ToString().Trim();
                    dtSource.Rows[0]["ledger_status"]                        = ddl_ledger_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["ledger_type"]                          = ddl_ledger_type.SelectedValue.ToString().Trim();
                    
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["ledger_seq_no"]                       = txtb_lbl_ledger_seq.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                             = ddl_empl_name.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_code"]                        = ddl_account_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_sub_code"]                    = ddl_subaccount_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["date_from"]                           = txtb_date_from.Text.ToString().Trim();
                    dtSource.Rows[0]["date_to"]                             = txtb_date_to.Text.ToString().Trim();
                    dtSource.Rows[0]["account_amount"]                      = txtb_account_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["account_amount_paid"]                 = txtb_amount_paid.Text.ToString().Trim();
                    dtSource.Rows[0]["account_amount_deduct1"]               = txtb_amount_deduct1.Text.ToString().Trim();
                    dtSource.Rows[0]["account_amount_deduct2"]               = txtb_amount_deduct2.Text.ToString().Trim();
                    dtSource.Rows[0]["account_period_no_of_months"]         = hidden_account_period_no_of_months.Text;
                    dtSource.Rows[0]["check_ref_no"]                        = txtb_check_ref_no.Text.ToString().Trim();
                    dtSource.Rows[0]["ledger_status"]                       = ddl_ledger_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["ledger_type"]                         = ddl_ledger_type.SelectedValue.ToString().Trim();

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
                        nrow["ledger_seq_no"]               = dtSource.Rows[0]["ledger_seq_no"].ToString().Trim();
                        nrow["empl_id"]                     = ddl_empl_name.SelectedValue.ToString().Trim();
                        nrow["account_code"]                = ddl_account_code.SelectedValue.ToString().Trim();
                        nrow["account_sub_code"]            = ddl_subaccount_code.SelectedValue.ToString().Trim();
                        nrow["date_from"]                   = txtb_date_from.Text.ToString().Trim();
                        nrow["date_to"]                     = txtb_date_to.Text.ToString().Trim();
                        nrow["account_amount"]              = txtb_account_amount.Text.ToString().Trim();
                        nrow["account_amount_paid"]         = txtb_amount_paid.Text.ToString().Trim();
                        nrow["account_amount_deduct1"]       = txtb_amount_deduct1.Text.ToString().Trim();
                        nrow["account_amount_deduct2"]       = txtb_amount_deduct2.Text.ToString().Trim();
                        nrow["account_period_no_of_months"]  = hidden_account_period_no_of_months.Text;
                        nrow["check_ref_no"]                = txtb_check_ref_no.Text.ToString().Trim();
                        nrow["ledger_status"]               = ddl_ledger_status.SelectedValue.ToString().Trim();
                        nrow["ledger_type"]                 = ddl_ledger_type.SelectedValue.ToString().Trim();
                        nrow["employee_name"] = ddl_empl_name.SelectedItem.ToString().Trim();
                        
                        if (ddl_subaccount_code.SelectedValue != "" )
                        {
                            nrow["account_title"] = ddl_subaccount_code.SelectedItem.ToString().Trim();
                        }
                        else
                        {
                            nrow["account_title"] = ddl_account_code.SelectedItem.ToString().Trim();
                        }

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        //gv_dataListGrid.SelectRow(gv_dataListGrid.Rows.Count - 1);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "ledger_seq_no = '" + txtb_lbl_ledger_seq.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["ledger_seq_no"]               = txtb_lbl_ledger_seq.Text.ToString().Trim();
                        row2Edit[0]["empl_id"]                     = ddl_empl_name.SelectedValue.ToString().Trim();
                        row2Edit[0]["account_code"]                = ddl_account_code.SelectedValue.ToString().Trim();
                        row2Edit[0]["account_sub_code"]            = ddl_subaccount_code.SelectedValue.ToString().Trim();
                        row2Edit[0]["date_from"]                   = txtb_date_from.Text.ToString().Trim();
                        row2Edit[0]["date_to"]                     = txtb_date_to.Text.ToString().Trim();
                        row2Edit[0]["account_amount"]              = txtb_account_amount.Text.ToString().Trim();
                        row2Edit[0]["account_amount_paid"]         = txtb_amount_paid.Text.ToString().Trim();
                        row2Edit[0]["account_amount_deduct1"]       = txtb_amount_deduct1.Text.ToString().Trim();
                        row2Edit[0]["account_amount_deduct2"]       = txtb_amount_deduct2.Text.ToString().Trim();
                        row2Edit[0]["account_period_no_of_months"]     = hidden_account_period_no_of_months.Text;
                        row2Edit[0]["check_ref_no"]                = txtb_check_ref_no.Text.ToString().Trim();
                        row2Edit[0]["ledger_status"]               = ddl_ledger_status.SelectedValue.ToString().Trim();
                        row2Edit[0]["ledger_type"]                 = ddl_ledger_type.SelectedValue.ToString().Trim();

                        row2Edit[0]["employee_name"] = ddl_empl_name.SelectedItem.ToString().Trim();
                        if (ddl_subaccount_code.SelectedValue != "")
                        {
                            row2Edit[0]["account_title"] = ddl_subaccount_code.SelectedItem.ToString().Trim();
                        }
                        else
                        {
                            row2Edit[0]["account_title"] = ddl_account_code.SelectedItem.ToString().Trim();
                        }
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
            string searchExpression = "ledger_seq_no LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR date_from LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR date_to LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_amount LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_title LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_amount_deduct1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_amount_deduct2 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("ledger_seq_no", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("account_code", typeof(System.String));
            dtSource1.Columns.Add("account_sub_code", typeof(System.String));
            dtSource1.Columns.Add("date_from", typeof(System.String));
            dtSource1.Columns.Add("date_to", typeof(System.String));
            dtSource1.Columns.Add("account_title", typeof(System.String));
            dtSource1.Columns.Add("account_amount", typeof(System.String));
            dtSource1.Columns.Add("account_id_for_empl", typeof(System.String));
            dtSource1.Columns.Add("account_sub_description", typeof(System.String));
            dtSource1.Columns.Add("account_amount_deduct1", typeof(System.String));
            dtSource1.Columns.Add("account_amount_deduct2", typeof(System.String));

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
            
            if (ddl_account_code.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_account_code");
                ddl_account_code.Focus();
                validatedSaved = false;
            }
            if (txtb_date_from.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_date_from");
                txtb_date_from.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(txtb_date_from) == false)
            {
                FieldValidationColorChanged(true, "invalid-date-from");
                txtb_date_from.Focus();
            }
            else if (txtb_date_from.Text != "" && txtb_date_to.Text != "" && (DateTime.Parse(txtb_date_to.Text.Trim()) < DateTime.Parse(txtb_date_from.Text.Trim())))
            {
                FieldValidationColorChanged(true, "dateto-is-greater-than");
                validatedSaved = false;
            }
            if (txtb_date_to.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_date_to");
                txtb_date_to.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(txtb_date_to) == false)
            {
                FieldValidationColorChanged(true, "invalid-date-to");
                txtb_date_to.Focus();
                validatedSaved = false;
            }
            else if (txtb_date_from.Text != "" && txtb_date_to.Text != "" && (DateTime.Parse(txtb_date_to.Text.Trim()) < DateTime.Parse(txtb_date_from.Text.Trim())))
            {
                FieldValidationColorChanged(true, "dateto-is-greater-than");
                validatedSaved = false;
            }
            if (ddl_ledger_type.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_ledger_type");
                ddl_ledger_type.Focus();
                validatedSaved = false;
            }
            if (ddl_ledger_status.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_ledger_status");
                ddl_ledger_status.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_account_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_account_amount");
                txtb_account_amount.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_amount_deduct1) == false)
            {
                FieldValidationColorChanged(true, "txtb_amount_deduct1");
                txtb_amount_deduct1.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_amount_deduct2) == false)
            {
                FieldValidationColorChanged(true, "txtb_amount_deduct2");
                txtb_amount_deduct2.Focus();
                validatedSaved = false;
            }
            UpdatePanel15.Update();
            UpdatePanel14.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
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
                    case "ddl_account_code":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_account_code.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_subaccount_code":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            ddl_subaccount_code.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_date_from":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_date_from.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-date-from":
                        {
                            LblRequired3.Text = "Invalid Date Value";
                            txtb_date_from.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_date_to":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_date_to.BorderColor = Color.Red;
                            txtb_date_to.Focus();
                            break;
                        }
                    case "invalid-date-to":
                        {
                            LblRequired4.Text = "Invalid Date Value";
                            txtb_date_to.BorderColor = Color.Red;
                            txtb_date_to.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "to_required_header();", true);
                            break;
                        }
                    case "txtb_account_amount":
                        {
                            LblRequired8.Text = MyCmn.CONST_RQDFLD;
                            txtb_account_amount.BorderColor = Color.Red;
                            txtb_account_amount.Focus();
                            break;
                        }
                    case "invalid-numeric":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_account_amount.BorderColor = Color.Red;
                            txtb_account_amount.Focus();
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired3.Text = "already exist!";
                            txtb_date_from.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-amount":
                        {
                            lbl_notification.Text = "Account Amount did not match !";
                            LblRequired5.Text = "Account Amount did not match !";
                            txtb_account_amount.BorderColor = Color.Red;
                            txtb_account_amount.Focus();

                            break;
                        }
                    case "dateto-is-greater-than":
                        {
                            LblRequired4.Text = "Must be greater that Date From!";
                            txtb_date_to.BorderColor = Color.Red;
                            txtb_date_to.Focus();
                            break;
                        }
                    case "ddl_ledger_status":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            ddl_ledger_status.BorderColor = Color.Red;
                            ddl_ledger_status.Focus();
                            break;
                        }
                    case "ddl_ledger_type":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            ddl_ledger_type.BorderColor = Color.Red;
                            ddl_ledger_type.Focus();
                            break;
                        }
                    case "txtb_amount_deduct1":
                        {
                            LblRequired9.Text = MyCmn.CONST_RQDFLD;
                            txtb_amount_deduct1.BorderColor = Color.Red;
                            txtb_amount_deduct1.Focus();
                            break;
                        }
                    case "txtb_amount_deduct2":
                        {
                            LblRequired13.Text = MyCmn.CONST_RQDFLD;
                            txtb_amount_deduct2.BorderColor = Color.Red;
                            txtb_amount_deduct2.Focus();
                            break;
                        }
                    case "txtb_check_ref_no":
                        {
                            LblRequired10.Text = MyCmn.CONST_RQDFLD;
                            txtb_check_ref_no.BorderColor = Color.Red;
                            txtb_check_ref_no.Focus();
                            break;
                        }
                    case "invalid-numeric-amount-deduct":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_amount_deduct1.BorderColor = Color.Red;
                            txtb_amount_deduct1.Focus();
                            break;
                        }
                    case "invalid-numeric-amount-deduct2":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_amount_deduct2.BorderColor = Color.Red;
                            txtb_amount_deduct2.Focus();
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
                            LblRequired2.Text = "";
                            LblRequired3.Text = "";
                            LblRequired4.Text = "";
                            LblRequired5.Text = "";
                            LblRequired6.Text = "";
                            LblRequired8.Text = "";
                            LblRequired9.Text = "";
                            LblRequired10.Text = "";
                            LblRequired13.Text = "";
                            ddl_account_code.BorderColor = Color.LightGray;
                            ddl_subaccount_code.BorderColor = Color.LightGray;
                            ddl_ledger_type.BorderColor = Color.LightGray;
                            ddl_ledger_status.BorderColor = Color.LightGray;
                            txtb_account_amount.BorderColor = Color.LightGray;
                            txtb_date_from.BorderColor = Color.LightGray;
                            txtb_date_to.BorderColor = Color.LightGray;
                            ddl_empl_name.BorderColor = Color.LightGray;
                            txtb_amount_deduct1.BorderColor = Color.LightGray;
                            txtb_amount_deduct2.BorderColor = Color.LightGray;
                            txtb_check_ref_no.BorderColor = Color.LightGray;
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                            UpdatePanel15.Update();
                            UpdatePanel14.Update();
                            break;
                        }

                }
            }
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers When Select Department
        //*************************************************************************
        protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_department.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "")
            {
                //RetrieveCombilist();
                RetrieveEmpl();
                btnAdd.Visible = false;
                RetrieveDataListGrid();
            }
            else
            {
                btnAdd.Visible = false;
            }
            UpdatePanel10.Update();
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers When Select Employe Name
        //*************************************************************************
        protected void ddl_empl_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_name.SelectedValue.ToString() != "" &&  ddl_department.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "")
            {
                RetrieveDataListGrid();
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            UpdatePanel10.Update();
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers When Select Account Name
        //*************************************************************************
        protected void ddl_account_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddl_account_code.SelectedValue != "")
            {
                RetrieveBindingSubAccount();
            }
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Calculate and Get the Account Period of month 
        //*************************************************************************
        protected void lnkbtn_breakdown_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            if (IsDataValidated())
            {
                if (txtb_account_amount.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_account_amount");
                    txtb_account_amount.Focus();
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "to_required_header();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "to_required_details();", true);
                    
                    for_account_period_no_of_months();
                }
            }
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Calculagte the Number of Month Based - Date From and To
        //*************************************************************************
        protected void for_account_period_no_of_months()
        {
            DateTime date_from = DateTime.Parse(txtb_date_from.Text.ToString().Trim());
            DateTime date_to = DateTime.Parse(txtb_date_to.Text.ToString().Trim());
            int x = (date_to.Month - date_from.Month + 1);
            if (date_to.Year != date_from.Year && date_from.Year < date_to.Year)
            {
                int year_result = (date_to.Year - date_from.Year) * 12;
                x = x + year_result;
            }
            hidden_account_period_no_of_months.Text = x.ToString();


            //if (dtSource_for_dtl.Rows.Count < 1)
            //{
            //    DateTime date_from = DateTime.Parse(txtb_date_from.Text.ToString().Trim());
            //    DateTime date_to = DateTime.Parse(txtb_date_to.Text.ToString().Trim());
            //    int x = (date_to.Month - date_from.Month + 1);
            //    if (date_to.Year != date_from.Year && date_from.Year < date_to.Year)
            //    {
            //        int year_result = (date_to.Year - date_from.Year) * 12;
            //        x = x + year_result;
            //    }

            //    InitializeTable_dtl();
            //    AddPrimaryKeys_dtl();

            //    double total_amount = Convert.ToDouble(txtb_account_amount.Text) / x;

            //    for (int y = 1; y <= x; y++)
            //    {
            //        DataRow nrow = dtSource_for_dtl.NewRow();
            //        nrow["ledger_seq_no"] = txtb_lbl_ledger_seq.Text;
            //        nrow["line_seq_no"] = y;
            //        nrow["line_date"] = date_from.ToString("yyyy-MM-dd");
            //        nrow["line_amount"] = total_amount;
            //        nrow["action"] = 1;
            //        nrow["retrieve"] = false;
            //        dtSource_for_dtl.Rows.Add(nrow);
            //        date_from = date_from.AddMonths(1);
            //    }
            //}
            //else
            //{
            //    //This is for PURPOSE EDIT

            //}
            ////lbl_account_amount.Text = txtb_account_amount.Text;

            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "show_date();", true);
            //FieldValidationColorChanged(false, "ALL");
            //CommonCode.GridViewBind(ref this.gv_datalistgrid_for_dtl, dtSource_for_dtl);
            //panel_datalistgrid_dtl.Update();
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Select Show Entries - Detials
        //*************************************************************************
        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            gv_datalistgrid_for_dtl.PageSize = Convert.ToInt32(DropDownList1.Text);
            CommonCode.GridViewBind(ref this.gv_datalistgrid_for_dtl, dataListGrid_dtl);
            show_pagesx1.Text = "Page: <b>" + (gv_datalistgrid_for_dtl.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_datalistgrid_for_dtl.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Grid View to Detials
        //*************************************************************************
        protected void gv_datalistgrid_for_dtl_Sorting(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(gv_datalistgrid_for_dtl, dataListGrid_dtl, e.SortExpression, sortingDirection);
            show_pagesx1.Text = "Page: <b>" + (gv_datalistgrid_for_dtl.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_datalistgrid_for_dtl.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Pagination - Details
        //*************************************************************************
        protected void gv_datalistgrid_for_dtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_datalistgrid_for_dtl.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_datalistgrid_for_dtl, dataListGrid_dtl, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx1.Text = "Page: <b>" + (gv_datalistgrid_for_dtl.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_datalistgrid_for_dtl.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Search - Detials
        //*************************************************************************
        protected void txtb_search_details_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "posted_date LIKE '%" + txtb_search_details.Text.Trim().Replace("'","''") + "%' OR posted_amount LIKE '%" + txtb_search_details.Text.Trim().Replace("'", "''") + "%'";
            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("posted_amount", typeof(System.String));
            dtSource1.Columns.Add("posted_date", typeof(System.String));
            dtSource1.Columns.Add("ledger_seq_no", typeof(System.String));

            DataRow[] rows = dtSource_for_dtl.Select(searchExpression);
            dtSource1.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource1.ImportRow(row);
                }
            }
            gv_datalistgrid_for_dtl.DataSource = dtSource1;
            gv_datalistgrid_for_dtl.DataBind();
            panel_datalistgrid_dtl.Update();
            txtb_search_details.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search_details.Focus();
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Visible if the Session is not Empty
        //*************************************************************************
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
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}