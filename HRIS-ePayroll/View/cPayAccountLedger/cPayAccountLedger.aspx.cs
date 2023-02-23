//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                     DATE           PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     2019-01-17      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cPayAccountLedger
{
    public partial class cPayAccountLedger : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JVA- 2019-01-17 - Data Place holder creation 
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
        DataTable dtSource_DeductionList
        {
            get
            {
                if ((DataTable)ViewState["dtSource_DeductionList"] == null) return null;
                return (DataTable)ViewState["dtSource_DeductionList"];
            }
            set
            {
                ViewState["dtSource_DeductionList"] = value;
            }
        }
        DataTable dtSource_Deduction_details
        {
            get
            {
                if ((DataTable)ViewState["dtSource_Deduction_details"] == null) return null;
                return (DataTable)ViewState["dtSource_Deduction_details"];
            }
            set
            {
                ViewState["dtSource_Deduction_details"] = value;
            }
        }
        DataTable dtSource_Ledger_info
        {
            get
            {
                if ((DataTable)ViewState["dtSource_Ledger_info"] == null) return null;
                return (DataTable)ViewState["dtSource_Ledger_info"];
            }
            set
            {
                ViewState["dtSource_Ledger_info"] = value;
            }
        }

        DataTable dtSource_brkdwn
        {
            get
            {
                if ((DataTable)ViewState["dtSource_brkdwn"] == null) return null;
                return (DataTable)ViewState["dtSource_brkdwn"];
            }
            set
            {
                ViewState["dtSource_brkdwn"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - JVA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - JVA- 2019-01-17 - Page Load method
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
                    
                    ddl_empl_type.SelectedValue = prevValues[3].ToString();
                    RetrieveEmpl();
                    ddl_empl_name.SelectedValue = prevValues[0].ToString();
                    
                    RetrieveDataListGrid();
                    lnkbtn_back.Visible = true;
                    btnAdd.Visible = true;
                }
            }

            
        }

        //********************************************************************
        //  BEGIN - JVA- 2019-01-17 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            //RetrieveEmpl();
            RetrieveEmploymentType();

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPositionTable"] = "cPositionTable";

            RetrieveDataListGrid();
            RetrieveDeductionList();
            RetrieveAccountType_main();

            btnAdd.Visible = false;
            lnkbtn_back.Visible = false;
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrolldeduc_ledger_tbl_list1",  "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_deduc_code", ddl_loan_account_name.SelectedValue.ToString().Trim(), "p_deduc_status", ddl_load_status.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        private void RetrieveDataListGrid_dtl()
        {
            dtSource_for_dtl = MyCmn.RetrieveData("sp_payrolldeduc_ledger_add_tbl_list", "par_empl_id", txtb_employee_id.Text, "par_deduc_code", txtb_deduc_code.Text, "par_deduc_seq", txtb_deduc_seq_hidden.Text);
            CommonCode.GridViewBind(ref this.gv_datalistgrid_for_dtl, dtSource_for_dtl);
            panel_datalistgrid_dtl.Update();
        }
        private void RetrieveDataListGrid_deduc_dtl()
        {
            dtSource_Deduction_details = MyCmn.RetrieveData("sp_deduct_details_per_empl", "p_empl_id", txtb_employee_id.Text.ToString().Trim(), "p_employment_type", ddl_empl_type.Text.ToString().Trim(), "p_deduc_code", txtb_deduc_code.Text.ToString().Trim(), "p_deduc_seq", txtb_deduc_seq_hidden.Text.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_deduction_details, dtSource_Deduction_details);
            up_deduction_details.Update();
        }
        private void RetrieveDataListGrid_ledger_info_dtl()
        {
            dtSource_Ledger_info = MyCmn.RetrieveData("sp_payrolldeduc_ledger_tbl_list2", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_deduc_code", ddl_loan_account_name.SelectedValue.ToString().Trim(), "p_empl_id", txtb_employee_id.Text.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_up_ledger_info, dtSource_Deduction_details);
            up_ledger_info.Update();
        }
        private void RetrieveDataListGrid_dtl_brkdwn()
        {
            // dtSource_brkdwn = MyCmn.RetrieveData("sp_payrolldeduc_ledger_dtl_tbl_list", "par_empl_id", txtb_employee_id.Text, "par_deduc_code", txtb_deduc_code.Text, "par_deduc_seq", txtb_deduc_seq_hidden.Text);
            // CommonCode.GridViewBind(ref this.gv_ledger_brkdwn, dtSource_brkdwn);
            // up_ledger_brkdwn.Update();

            InitializeTable_brkdwn();
            dtSource_brkdwn = MyCmn.RetrieveData("sp_payrolldeduc_ledger_dtl_tbl_list", "par_empl_id", txtb_employee_id.Text, "par_deduc_code", txtb_deduc_code.Text, "par_deduc_seq", txtb_deduc_seq_hidden.Text);
            AddPrimaryKeys_brkdwn();

            foreach (DataRow nrow1 in dtSource_brkdwn.Rows)
            {
                nrow1["action"] = 1;
                nrow1["retrieve"] = false;
            }

            MyCmn.Sort(gv_ledger_brkdwn, dtSource_brkdwn, "deduc_date_from", Session["SortOrder"].ToString());
            up_ledger_brkdwn.Update();

        }
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
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Loan Account Name
        //*************************************************************************
        private void RetrieveDeductionList()
        {
            ddl_loan_account_name.Items.Clear();
            dtSource_DeductionList = MyCmn.RetrieveData("sp_deduct_accounts_list1", "p_employment_type",ddl_empl_type.SelectedValue.ToString().Trim(), "p_accttype_code", ddl_accttype_main.SelectedValue.ToString().Trim());

            ddl_loan_account_name.DataSource = dtSource_DeductionList;
            ddl_loan_account_name.DataValueField = "deduc_code";
            ddl_loan_account_name.DataTextField = "deduc_descr";
            ddl_loan_account_name.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_loan_account_name.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Employee Name
        //*************************************************************************
        private void RetrieveEmpl()
        {
            ddl_empl_name.Items.Clear();
            dtSource_for_names = MyCmn.RetrieveData("sp_personnelnames_combolist_loan_ledger","p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_empl_name.DataSource = dtSource_for_names;
            ddl_empl_name.DataValueField = "empl_id";
            ddl_empl_name.DataTextField = "employee_name";
            ddl_empl_name.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_name.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            RetrieveEmpl();

            InitializeTable_dtl();
            
            //Retrive Ledger Sequence Number
            RetrieveDataListGrid_dtl();
            RetrieveDataListGrid_dtl_brkdwn();
            AddPrimaryKeys_dtl();
            RetrieveDataListGrid_deduc_dtl();
            RetrieveDataListGrid_ledger_info_dtl();
            
            id_hide_payment_details.Visible = true;
            txtb_empl_name.Visible          = false;
            ddl_empl_name.Visible           = true;
            txtb_date_from.Visible          = true;
            
            txtb_deduc_code.Text              = ddl_loan_account_name.SelectedValue.ToString();
            txtb_loan_account_name_descr.Text = ddl_loan_account_name.SelectedItem.ToString();
            txtb_status.Text                  = ddl_load_status.SelectedItem.ToString();

            // if (ddl_empl_type.SelectedValue == "JO")
            // {
            //     div_amount2.Visible = true;
            //     div_loanamount2.Visible = true;
            //     //Update_amount2.Visible = true;
            //     gv_datalistgrid_for_dtl.Columns[4].Visible = true;
            // }
            // else
            // {
            //     div_amount2.Visible = false;
            //     div_loanamount2.Visible = false;
            //     ///Update_amount2.Visible = false;
            //     gv_datalistgrid_for_dtl.Columns[4].Visible = false;
            // }

            txtb_deduc_source_hidden.Text = "U";
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            Toggle_DeductionDetails();
            Toggle_Others();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopOpenModal", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_account_amount.Text        = "0.00";
            txtb_date_from.Text             = "";
            txtb_date_to.Text               = "";
            txtb_check_ref_no.Text          = "";
            txtb_no_of_months.Text          = "";
            
            txtb_amount_paid.Text           = "0.00";
            txtb_amount_deduct1.Text        = "0.00";
            txtb_amount_deduct2.Text        = "0.00";
            txtb_empl_name.Text = "";
            txtb_employee_id.Text = "";
            ddl_empl_name.SelectedIndex = -1;
            //txtb_deduc_code.Text = "";
            txtb_deduc_seq_hidden.Text = "";
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Initialized datasource fields/columns - Header
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();

            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("deduc_code", typeof(System.String));
            dtSource.Columns.Add("deduc_seq", typeof(System.String));
            dtSource.Columns.Add("deduc_date_from", typeof(System.String));
            dtSource.Columns.Add("deduc_date_to", typeof(System.String));
            dtSource.Columns.Add("deduc_ref_nbr", typeof(System.String));
            dtSource.Columns.Add("deduc_amount1", typeof(System.String));
            dtSource.Columns.Add("deduc_amount2", typeof(System.String));
            dtSource.Columns.Add("deduc_loan_amount", typeof(System.String));
            dtSource.Columns.Add("deduc_nbr_months", typeof(System.String));
            dtSource.Columns.Add("deduc_status", typeof(System.String));
            dtSource.Columns.Add("deduc_source", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));
            
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Initialized datasource fields/columns - Details
        //*************************************************************************
        private void InitializeTable_dtl()
        {
            dtSource_for_dtl = new DataTable();
            dtSource_for_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_for_dtl.Columns.Add("deduc_code", typeof(System.String));
            dtSource_for_dtl.Columns.Add("deduc_seq", typeof(System.String));
            dtSource_for_dtl.Columns.Add("line_seq_no", typeof(System.String));
            dtSource_for_dtl.Columns.Add("monthyear_code_from", typeof(System.String));
            dtSource_for_dtl.Columns.Add("monthyear_code_to", typeof(System.String));
            dtSource_for_dtl.Columns.Add("addon_amount1", typeof(System.String));
            dtSource_for_dtl.Columns.Add("addon_amount2", typeof(System.String));
            dtSource_for_dtl.Columns.Add("created_by_user", typeof(System.String));
            dtSource_for_dtl.Columns.Add("created_dttm", typeof(System.String));
            dtSource_for_dtl.Columns.Add("updated_by_user", typeof(System.String));
            dtSource_for_dtl.Columns.Add("updated_dttm", typeof(System.String));
            // Extra Column
            dtSource_for_dtl.Columns.Add("monthyear_code_from_descr", typeof(System.String));
            dtSource_for_dtl.Columns.Add("monthyear_code_to_descr", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Initialized Deduction Details
        //*************************************************************************
        private void InitializeTable_ledger_info_dtl()
        {
            dtSource_Ledger_info = new DataTable();
            dtSource_Ledger_info.Columns.Add("deduc_date_from", typeof(System.String));
            dtSource_Ledger_info.Columns.Add("deduc_date_to", typeof(System.String));
            dtSource_Ledger_info.Columns.Add("deduc_loan_amount", typeof(System.String));
            dtSource_Ledger_info.Columns.Add("deduc_status_descr", typeof(System.String));
            dtSource_Ledger_info.Columns.Add("deduc_seq", typeof(System.String));
            dtSource_Ledger_info.Columns.Add("empl_id", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Initialized Deduction Details
        //*************************************************************************
        private void InitializeTable_deduc_dtl()
        {
            dtSource_Deduction_details = new DataTable();
            dtSource_Deduction_details.Columns.Add("payroll_yrmonth", typeof(System.String));
            dtSource_Deduction_details.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource_Deduction_details.Columns.Add("payroll_registry_descr", typeof(System.String));
            dtSource_Deduction_details.Columns.Add("deduc_amount_registry", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Add Primary Key Field to datasource - Header
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrolldeduc_ledger_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "deduc_code", "deduc_seq" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Add Primary Key Field to datasource - Details
        //*************************************************************************
        private void AddPrimaryKeys_dtl()
        {
            dtSource_for_dtl.TableName = "payrolldeduc_ledger_add_tbl";
            dtSource_for_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_for_dtl.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "deduc_code", "deduc_seq", "line_seq_no" };
            dtSource_for_dtl = MyCmn.AddPrimaryKeys(dtSource_for_dtl, col);
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"]          = string.Empty;
            nrow["deduc_code"]       = string.Empty;
            nrow["deduc_seq"]        = string.Empty;
            nrow["deduc_date_from"]  = string.Empty;
            nrow["deduc_date_to"]    = string.Empty;
            nrow["deduc_ref_nbr"]    = string.Empty;
            nrow["deduc_amount1"]    = string.Empty;
            nrow["deduc_amount2"]    = string.Empty;
            nrow["deduc_loan_amount"]= string.Empty;
            nrow["deduc_nbr_months"] = string.Empty;
            nrow["deduc_status"]     = string.Empty;
            nrow["deduc_source"]     = string.Empty;
            nrow["created_by_user"]  = string.Empty;
            nrow["created_dttm"]     = string.Empty;
            nrow["updated_by_user"]  = string.Empty;
            nrow["updated_dttm"]     = string.Empty;
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deduc_seq    = commandArgs[0].ToString().Trim();
            string empl_id      = commandArgs[1].ToString().Trim();
            string deduc_code   = commandArgs[2].ToString().Trim();

            deleteRec1.Text = "Are you sure to delete this Record ? </br> " + deduc_seq.ToString() + " | " + empl_id.ToString() + " ";
            lnkBtnYes.CommandArgument = commandArgs[0].ToString().Trim() + "," + commandArgs[1].ToString().Trim() + "," + commandArgs[2].ToString().Trim();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "deduc_seq = '" + svalues[0].ToString().Trim() + "' AND empl_id = '" + svalues[1].ToString().Trim() + "'  AND deduc_code = '" + svalues[2].ToString().Trim() + "' ";

            MyCmn.DeleteBackEndData("payrolldeduc_ledger_tbl", "WHERE " + deleteExpression);
            MyCmn.DeleteBackEndData("payrolldeduc_ledger_add_tbl", "WHERE " + deleteExpression);
            MyCmn.DeleteBackEndData("payrolldeduc_ledger_dtl_tbl", "WHERE " + deleteExpression);
            
            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

            SearchDate(txtb_search.Text.ToString().Trim());
        }

        //**************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string editExpression = "deduc_seq = '" + svalues[0].ToString().Trim() + "' AND empl_id = '" + svalues[1].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            //This is for HEADER EDIT
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;

            txtb_loan_account_name_descr.Text       = ddl_loan_account_name.SelectedItem.ToString();
            txtb_deduc_code.Text                    = ddl_loan_account_name.SelectedValue.ToString();

            txtb_deduc_seq_hidden.Text  = row2Edit[0]["deduc_seq"].ToString();
            txtb_employee_id.Text       = row2Edit[0]["empl_id"].ToString();
            txtb_empl_name.Text         = row2Edit[0]["employee_name"].ToString();
            txtb_date_from.Text         = row2Edit[0]["deduc_date_from"].ToString();
            txtb_date_to.Text           = row2Edit[0]["deduc_date_to"].ToString();
            txtb_account_amount.Text    = row2Edit[0]["deduc_loan_amount"].ToString();
            txtb_check_ref_no.Text      = row2Edit[0]["deduc_ref_nbr"].ToString();
            txtb_amount_deduct1.Text    = row2Edit[0]["deduc_amount1"].ToString();
            txtb_amount_deduct2.Text    = row2Edit[0]["deduc_amount2"].ToString();
            txtb_status.Text            = row2Edit[0]["deduc_status_descr"].ToString();
            txtb_no_of_months.Text      = row2Edit[0]["deduc_nbr_months"].ToString();
            dtSource.Rows[0]["created_by_user"]  = row2Edit[0]["created_by_user"].ToString();
            dtSource.Rows[0]["created_dttm"]     = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");


            // BEGIN : 2019-11-02 - Retrieve Deduction Add-on Tab
            InitializeTable_dtl();
            dtSource_for_dtl = MyCmn.RetrieveData("sp_payrolldeduc_ledger_add_tbl_list", "par_empl_id", txtb_employee_id.Text, "par_deduc_code", txtb_deduc_code.Text, "par_deduc_seq", txtb_deduc_seq_hidden.Text);
            AddPrimaryKeys_dtl();
            foreach (DataRow nrow in dtSource_for_dtl.Rows)
            {
                nrow["action"] = 1;
                nrow["retrieve"] = false;
            }
            CommonCode.GridViewBind(ref this.gv_datalistgrid_for_dtl, dtSource_for_dtl);
            panel_datalistgrid_dtl.Update();
            // END  : 2019-11-02 - Retrieve Deduction Add-on Tab


            // BEGIN : 2019-11-02 - Retrieve Ledger Information on Tab
            InitializeTable_ledger_info_dtl();
            dtSource_Ledger_info = MyCmn.RetrieveData("sp_payrolldeduc_ledger_tbl_list2", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_deduc_code", ddl_loan_account_name.SelectedValue.ToString().Trim(), "p_empl_id", txtb_employee_id.Text.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_up_ledger_info, dtSource_Ledger_info);
            up_ledger_info.Update();
            // END   : 2019-11-02 - Retrieve Ledger Information on Tab

            // BEGIN : 2020-06-02 - Retrieve Ledger Details Information on Tab
            RetrieveDataListGrid_dtl_brkdwn();
            // END  : 2020-06-02 - Retrieve Ledger Details Information on Tab

            txtb_empl_name.Visible = true;
            ddl_empl_name.Visible = false;
            
            LabelAddEdit.Text = "Edit Record " ;
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;

            id_hide_payment_details.Visible = true;
            FieldValidationColorChanged(false, "ALL");
            
            //if (ddl_empl_type.SelectedValue == "JO")
            //{
            //    div_amount2.Visible = true;
            //    div_loanamount2.Visible = true;
            //    //Update_amount2.Visible = true;
            //    gv_datalistgrid_for_dtl.Columns[4].Visible = true;
            //}
            //else
            //{
            //    div_amount2.Visible = false;
            //    div_loanamount2.Visible = false;
            //    ///Update_amount2.Visible = false;
            //    gv_datalistgrid_for_dtl.Columns[4].Visible = false;
            //}

            for_account_period_no_of_months();
            Toggle_DeductionDetails();
            Toggle_Others();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopopenModal", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Change Field Sort mode  
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

            SearchDate(txtb_search.Text.ToString().Trim());
        }

        //**************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Get Grid current sort order 
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
        //  BEGIN - JVA- 2019-01-17 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;

            if (IsDataValidated())
            {
                //for_account_period_no_of_months();

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    
                    dtSource.Rows[0]["empl_id"]               = txtb_employee_id.Text;
                    dtSource.Rows[0]["deduc_code"]            = txtb_deduc_code.Text.ToString().Trim();
                    dtSource.Rows[0]["deduc_seq"]             = txtb_deduc_seq_hidden.Text.ToString();
                    dtSource.Rows[0]["deduc_date_from"]       = txtb_date_from.Text.ToString().Trim();
                    dtSource.Rows[0]["deduc_date_to"]         = txtb_date_to.Text.ToString().Trim();
                    dtSource.Rows[0]["deduc_ref_nbr"]         = txtb_check_ref_no.Text.ToString().Trim();
                    dtSource.Rows[0]["deduc_amount1"]         = float.Parse(txtb_amount_deduct1.Text).ToString("###,##0.00");
                    dtSource.Rows[0]["deduc_amount2"]         = float.Parse(txtb_amount_deduct2.Text).ToString("###,##0.00");
                    dtSource.Rows[0]["deduc_loan_amount"]     = float.Parse(txtb_account_amount.Text).ToString("###,##0.00");
                    dtSource.Rows[0]["deduc_nbr_months"]      = txtb_no_of_months.Text;
                    dtSource.Rows[0]["deduc_status"]          = ddl_load_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["deduc_source"]          = txtb_deduc_source_hidden.Text;
                    dtSource.Rows[0]["created_by_user"]       = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["created_dttm"]          = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["updated_by_user"]       = "";
                    dtSource.Rows[0]["updated_dttm"]          = "";
                    
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["empl_id"]               = txtb_employee_id.Text;
                    dtSource.Rows[0]["deduc_code"]            = txtb_deduc_code.Text.ToString().Trim();
                    dtSource.Rows[0]["deduc_seq"]             = txtb_deduc_seq_hidden.Text.ToString();
                    dtSource.Rows[0]["deduc_date_from"]       = txtb_date_from.Text.ToString().Trim();
                    dtSource.Rows[0]["deduc_date_to"]         = txtb_date_to.Text.ToString().Trim();
                    dtSource.Rows[0]["deduc_ref_nbr"]         = txtb_check_ref_no.Text.ToString().Trim();
                    dtSource.Rows[0]["deduc_amount1"]         = float.Parse(txtb_amount_deduct1.Text).ToString("###,##0.00");
                    dtSource.Rows[0]["deduc_amount2"]         = float.Parse(txtb_amount_deduct2.Text).ToString("###,##0.00");
                    dtSource.Rows[0]["deduc_loan_amount"]     = float.Parse(txtb_account_amount.Text).ToString("###,##0.00");
                    dtSource.Rows[0]["deduc_nbr_months"]      = txtb_no_of_months.Text;
                    dtSource.Rows[0]["deduc_status"]          = ddl_load_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["deduc_source"]          = txtb_deduc_source_hidden.Text;
                    dtSource.Rows[0]["created_by_user"]       = dtSource.Rows[0]["created_by_user"].ToString();
                    dtSource.Rows[0]["created_dttm"]          = dtSource.Rows[0]["created_dttm"].ToString();   
                    dtSource.Rows[0]["updated_by_user"]       = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["updated_dttm"]          = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    if (checkif_match() == false)
                    {
                        FieldValidationColorChanged(true, "did-not-match");
                        return;
                    }


                    //**************************************************************************
                    //  BEGIN - JVA- 2020-10-08 - Gepa Comment ni Sir Ge Trigger lang ni Sir ang Mu 
                    //                          In - Avtive sa Deduction Ledger
                    //**************************************************************************

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataTable dt = MyCmn.RetrieveData("sp_payrolldeduc_ledger_tbl_chck_if_exist", "par_empl_id", txtb_employee_id.Text, "par_deduc_code", ddl_loan_account_name.SelectedValue.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["return_value"].ToString() == "Y")
                            {
                                lbl_notification.Text = "This Loan Account Name is already exist !";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop10", "openNotification();", true);
                                return;
                            }
                        }
                    }

                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;
                    if (msg.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist");
                        return;
                    }

                    dtSource_for_dtl.Columns.Remove("monthyear_code_from_descr");
                    dtSource_for_dtl.Columns.Remove("monthyear_code_to_descr");
                    string[] insert_empl_script = MyCmn.get_insertscript(dtSource_for_dtl).Split(';');
                    MyCmn.DeleteBackEndData(dtSource_for_dtl.TableName.ToString(), "WHERE deduc_seq ='" + txtb_deduc_seq_hidden.Text.ToString().Trim() + "'");
                    for (int x = 0; x < insert_empl_script.Length; x++)
                    {
                        string insert_script = "";
                        insert_script = insert_empl_script[x];
                        MyCmn.insertdata(insert_script);
                    }

                    // ********************************************************************************************************//
                    // ** VJA - 2020-06-02 - This is for Functionality to Delete And Insert to payrolldeducledger_dtl_tbl Table
                    // ********************************************************************************************************//
                    DeleteInsert_BreakDown();
                    // ********************************************************************************************************//
                    // ** VJA - 2020-06-02 - This is for Functionality to Delete And Insert to payrolldeducledger_dtl_tbl Table
                    // ********************************************************************************************************//

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["empl_id"]               = txtb_employee_id.Text;
                        nrow["deduc_code"]            = txtb_deduc_code.Text.ToString().Trim();
                        nrow["deduc_seq"]             = txtb_deduc_seq_hidden.Text.ToString();
                        nrow["deduc_date_from"]       = txtb_date_from.Text.ToString().Trim();
                        nrow["deduc_date_to"]         = txtb_date_to.Text.ToString().Trim();
                        nrow["deduc_ref_nbr"]         = txtb_check_ref_no.Text.ToString().Trim();
                        nrow["deduc_amount1"]         = float.Parse(txtb_amount_deduct1.Text).ToString("###,##0.00");
                        nrow["deduc_amount2"]         = float.Parse(txtb_amount_deduct2.Text).ToString("###,##0.00");
                        nrow["deduc_loan_amount"]     = float.Parse(txtb_account_amount.Text).ToString("###,##0.00");
                        nrow["deduc_nbr_months"]      = txtb_no_of_months.Text == "" ? "0" : txtb_no_of_months.Text;
                        nrow["deduc_status"]          = ddl_load_status.SelectedValue.ToString().Trim();
                        nrow["deduc_source"]          = txtb_deduc_source_hidden.Text;
                        nrow["created_by_user"]       = Session["ep_user_id"].ToString().Trim();
                        nrow["created_dttm"]          = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        nrow["updated_by_user"]       = "";
                        nrow["updated_dttm"]          = "1900-01-01";

                        nrow["employee_name"]         = ddl_empl_name.SelectedItem.ToString().Trim();
                        nrow["deduc_status_descr"]    = "Active";
                        
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                        h2_status.InnerText = "SUCCESSFULLY ADDED";
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "deduc_seq = '" + txtb_deduc_seq_hidden.Text.ToString().Trim() + "'AND deduc_code = '" + txtb_deduc_code.Text.ToString().Trim() + "' AND empl_id = '" + txtb_employee_id.Text.ToString().Trim() + "' ";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        
                        row2Edit[0]["empl_id"]               = txtb_employee_id.Text;
                        row2Edit[0]["deduc_code"]            = txtb_deduc_code.Text.ToString().Trim();
                        row2Edit[0]["deduc_seq"]             = txtb_deduc_seq_hidden.Text.ToString();
                        row2Edit[0]["deduc_date_from"]       = txtb_date_from.Text.ToString().Trim();
                        row2Edit[0]["deduc_date_to"]         = txtb_date_to.Text.ToString().Trim();
                        row2Edit[0]["deduc_ref_nbr"]         = txtb_check_ref_no.Text.ToString().Trim();
                        row2Edit[0]["deduc_amount1"]         = float.Parse(txtb_amount_deduct1.Text).ToString("###,##0.00");
                        row2Edit[0]["deduc_amount2"]         = float.Parse(txtb_amount_deduct2.Text).ToString("###,##0.00");
                        row2Edit[0]["deduc_loan_amount"]     = float.Parse(txtb_account_amount.Text).ToString("###,##0.00");
                        row2Edit[0]["deduc_nbr_months"]      = txtb_no_of_months.Text == "" ? "0" : txtb_no_of_months.Text;
                        row2Edit[0]["deduc_status"]          = ddl_load_status.SelectedValue.ToString().Trim();
                        row2Edit[0]["deduc_source"]          = txtb_deduc_source_hidden.Text;
                        row2Edit[0]["created_by_user"]       = dtSource.Rows[0]["created_by_user"].ToString();
                        row2Edit[0]["created_dttm"]          = dtSource.Rows[0]["created_dttm"].ToString();   
                        row2Edit[0]["updated_by_user"]       = Session["ep_user_id"].ToString().Trim();
                        row2Edit[0]["updated_dttm"]          = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        row2Edit[0]["employee_name"]          = txtb_empl_name.Text;
                        
                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                        i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                        h2_status.InnerText = "SUCCESSFULLY UPDATED";
                    }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "closeModal();", true);
                        ViewState.Remove("AddEdit_Mode");
                        show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

                        SearchDate(txtb_search.Text.ToString().Trim());
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

            SearchDate(txtb_search.Text.ToString().Trim());
        }

        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

            SearchDate(txtb_search.Text.ToString().Trim());
        }

        
        //**************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Define Property for Sort Direction  
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
        //  BEGIN - JVA- 2019-01-17 - Check if Object already contains value  
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
        //  BEGIN - JVA- 2019-01-17 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");

            if (ddl_empl_name.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_name");
                ddl_empl_name.Focus();
                validatedSaved = false;
            }
            if (txtb_date_from.Text == "" &&(
                ddl_accttype_main.SelectedValue == "05" ||
                ddl_accttype_main.SelectedValue == "07" ||
                ddl_accttype_main.SelectedValue == "08"))
            {
                FieldValidationColorChanged(true, "txtb_date_from");
                //txtb_date_from.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(txtb_date_from) == false)
            {
                FieldValidationColorChanged(true, "invalid-date-from");
                //txtb_date_from.Focus();
            }
            else if (txtb_date_from.Text != "" && txtb_date_to.Text != "" && (DateTime.Parse(txtb_date_to.Text.Trim()) < DateTime.Parse(txtb_date_from.Text.Trim())))
            {
                FieldValidationColorChanged(true, "dateto-is-greater-than");
                validatedSaved = false;
            }
            if ((txtb_date_to.Text == "") && (
                ddl_accttype_main.SelectedValue == "05" ||
                ddl_accttype_main.SelectedValue == "07" ||
                ddl_accttype_main.SelectedValue == "08"))
            {
                FieldValidationColorChanged(true, "txtb_date_to");
                //txtb_date_to.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(txtb_date_to) == false )
            {
                FieldValidationColorChanged(true, "invalid-date-to");
                //txtb_date_to.Focus();
                validatedSaved = false;
            }
            else if (txtb_date_from.Text != "" && txtb_date_to.Text != "" && (DateTime.Parse(txtb_date_to.Text.Trim()) < DateTime.Parse(txtb_date_from.Text.Trim())))
            {
                FieldValidationColorChanged(true, "dateto-is-greater-than");
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
        //  BEGIN - JVA- 2019-01-17 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "txtb_date_from":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_date_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "invalid-date-from":
                        {
                            LblRequired3.Text = "Invalid Date Value";
                            txtb_date_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "txtb_date_to":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_date_to.BorderColor = Color.Red;
                            //txtb_date_to.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "invalid-date-to":
                        {
                            LblRequired4.Text = "Invalid Date Value";
                            txtb_date_to.BorderColor = Color.Red;
                            //txtb_date_to.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                            break;
                        }
                    case "txtb_account_amount":
                        {
                            LblRequired8.Text = MyCmn.CONST_RQDFLD;
                            txtb_account_amount.BorderColor = Color.Red;
                            txtb_account_amount.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "invalid-numeric":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_account_amount.BorderColor = Color.Red;
                            txtb_account_amount.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired12.Text = "already exist!";
                            txtb_date_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "dateto-is-greater-than":
                        {
                            LblRequired4.Text = "Must be greater that Date From!";
                            txtb_date_to.BorderColor = Color.Red;
                            txtb_date_to.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    
                    case "txtb_amount_deduct1":
                        {
                            LblRequired9.Text = MyCmn.CONST_RQDFLD;
                            txtb_amount_deduct1.BorderColor = Color.Red;
                            txtb_amount_deduct1.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "txtb_amount_deduct2":
                        {
                            LblRequired13.Text = MyCmn.CONST_RQDFLD;
                            txtb_amount_deduct2.BorderColor = Color.Red;
                            txtb_amount_deduct2.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "txtb_check_ref_no":
                        {
                            LblRequired10.Text = MyCmn.CONST_RQDFLD;
                            txtb_check_ref_no.BorderColor = Color.Red;
                            txtb_check_ref_no.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "invalid-numeric-amount-deduct":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_amount_deduct1.BorderColor = Color.Red;
                            txtb_amount_deduct1.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                            break;
                        }
                    case "invalid-numeric-amount-deduct2":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_amount_deduct2.BorderColor = Color.Red;
                            txtb_amount_deduct2.Focus();
                            break;
                        }
                    case "ddl_empl_name":
                        {
                            LblRequired12.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_name.BorderColor = Color.Red;
                            ddl_empl_name.Focus();
                            break;
                        }
                    case "invalid-txtb_addon_amount1":
                        {
                            LblRequired22.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_addon_amount1.BorderColor = Color.Red;
                            txtb_addon_amount1.Focus();
                            break;
                        }
                    case "invalid-txtb_addon_amount2":
                        {
                            LblRequired23.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_addon_amount2.BorderColor = Color.Red;
                            txtb_addon_amount2.Focus();
                            break;
                        }
                    case "is-greater-than-datefrom":
                        {
                            LblRequired20.Text = "date from is greater than date to!";
                            ddl_addon_date_from.BorderColor = Color.Red;
                            ddl_addon_date_from.Focus();
                            break;
                        }
                    case "date-already-exist":
                        {
                            LblRequired20.Text = "date is already exist";
                            ddl_addon_date_from.BorderColor = Color.Red;
                            ddl_addon_date_from.Focus();
                            break;
                        }
                    case "ddl_addon_date_from":
                        {
                            LblRequired20.Text = MyCmn.CONST_RQDFLD;
                            ddl_addon_date_from.BorderColor = Color.Red;
                            ddl_addon_date_from.Focus();
                            break;
                        }
                    case "ddl_addon_date_to":
                        {
                            LblRequired21.Text = MyCmn.CONST_RQDFLD;
                            ddl_addon_date_to.BorderColor = Color.Red;
                            ddl_addon_date_to.Focus();
                            break;
                        }
                    case "txtb_ref_nbr":
                        {
                            LblRequired30.Text = MyCmn.CONST_RQDFLD;
                            txtb_ref_nbr.BorderColor = Color.Red;
                            txtb_ref_nbr.Focus();
                            break;
                        }
                    case "txtb_deduct_from_brkdwn":
                        {
                            LblRequired31.Text = "Invalid Date Value";
                            txtb_deduct_from_brkdwn.BorderColor = Color.Red;
                            txtb_deduct_from_brkdwn.Focus();
                            break;
                        }
                    case "txtb_deduct_to_brkdwn":
                        {
                            LblRequired32.Text = "Invalid Date Value";
                            txtb_deduct_to_brkdwn.BorderColor = Color.Red;
                            txtb_deduct_to_brkdwn.Focus();
                            break;
                        }
                    case "txtb_deduct_amt_brkdwn":
                        {
                            LblRequired33.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_deduct_amt_brkdwn.BorderColor = Color.Red;
                            txtb_deduct_amt_brkdwn.Focus();
                            break;
                        }
                    case "did-not-match":
                        {
                            i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-warning");
                            h2_status.InnerText = "YOU CANNOT SAVE ! ";
                            SaveAddEdit.Text    = "On Deduction Details Tab - You inputed greater, less-than or did not match the Amount of <b>" + txtb_amount_deduct1.Text +"</b> - Deduction Amount 1";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "openNotif();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4thtb", "click_4thtab();", true);
                            break;
                        }
                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    
                    case "ALL":
                        {
                            LblRequired3.Text = "";
                            LblRequired4.Text = "";
                            LblRequired8.Text = "";
                            LblRequired9.Text = "";
                            LblRequired10.Text = "";
                            LblRequired13.Text = "";
                            LblRequired12.Text = "";
                            LblRequired20.Text = "";
                            LblRequired21.Text = "";
                            LblRequired22.Text = "";
                            LblRequired23.Text = "";

                            LblRequired30.Text = "";
                            LblRequired31.Text = "";
                            LblRequired32.Text = "";
                            LblRequired33.Text = "";

                            txtb_account_amount.BorderColor = Color.LightGray;
                            txtb_date_from.BorderColor = Color.LightGray;
                            txtb_date_to.BorderColor = Color.LightGray;
                            ddl_empl_name.BorderColor = Color.LightGray;
                            txtb_amount_deduct1.BorderColor = Color.LightGray;
                            txtb_amount_deduct2.BorderColor = Color.LightGray;
                            txtb_check_ref_no.BorderColor = Color.LightGray;
                            txtb_addon_amount1.BorderColor = Color.LightGray;
                            txtb_addon_amount2.BorderColor = Color.LightGray;
                            ddl_addon_date_from.BorderColor = Color.LightGray;
                            ddl_addon_date_to.BorderColor = Color.LightGray;

                            txtb_ref_nbr.BorderColor = Color.LightGray;
                            txtb_deduct_from_brkdwn.BorderColor = Color.LightGray;
                            txtb_deduct_to_brkdwn.BorderColor = Color.LightGray;
                            txtb_deduct_amt_brkdwn.BorderColor = Color.LightGray;

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                            UpdatePanel15.Update();
                            UpdatePanel14.Update();
                            UpdatePanel_From.Update();
                            UpdatePanel_To.Update();
                            break;
                        }

                }
            }
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Triggers When Select Employe Name
        //*************************************************************************
        protected void ddl_empl_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ddl_empl_name.SelectedValue.ToString() != "")
            {
                //RetrieveDataListGrid();
                HeaderDetails_Initialized_Add();
                RetrieveNextSeqNumber();
                
                // BEGIN : 2019-11-02 - Retrieve Ledger Information on Tab
                InitializeTable_ledger_info_dtl();
                dtSource_Ledger_info = MyCmn.RetrieveData("sp_payrolldeduc_ledger_tbl_list2", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_deduc_code", ddl_loan_account_name.SelectedValue.ToString().Trim(), "p_empl_id", txtb_employee_id.Text.ToString().Trim());
                CommonCode.GridViewBind(ref this.gv_up_ledger_info, dtSource_Ledger_info);
                up_ledger_info.Update();
                // END   : 2019-11-02 - Retrieve Ledger Information on Tab

            }
            else
            {
                ClearEntry();

                // BEGIN : 2019-11-02 - Clear Add On Data Source
                InitializeTable_dtl();
                AddPrimaryKeys_dtl();
                CommonCode.GridViewBind(ref this.gv_datalistgrid_for_dtl, dtSource_for_dtl);
                panel_datalistgrid_dtl.Update();
                // END : 2019-11-02 - Clear Add On Data Source

                // BEGIN : 2019-11-02 - Clear Ledger Information on TabData Source
                InitializeTable_ledger_info_dtl();
                CommonCode.GridViewBind(ref this.gv_up_ledger_info, dtSource_Ledger_info);
                up_ledger_info.Update();
                // BEGIN : 2019-11-02 - Clear Ledger Information on TabData Source

                FieldValidationColorChanged(false, "ALL");
            }
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Calculate and Get the Account Period of month 
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
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Click1st", "click_1sttab();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Click2nd", "click_2ndtab();", true);
                    
                    for_account_period_no_of_months();
                }
            }
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Calculagte the Number of Month Based - Date From and To
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
            //hidden_account_period_no_of_months.Text = x.ToString();
            txtb_no_of_months.Text = x.ToString();


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
            //        nrow["deduc_seq"] = txtb_lbl_ledger_seq.Text;
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
        //  BEGIN - JVA- 2019-01-17 - Select Show Entries - Detials
        //*************************************************************************
        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            gv_datalistgrid_for_dtl.PageSize = Convert.ToInt32(DropDownList1.Text);
            CommonCode.GridViewBind(ref this.gv_datalistgrid_for_dtl, dtSource_for_dtl);
            show_pagesx1.Text = "Page: <b>" + (gv_datalistgrid_for_dtl.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_datalistgrid_for_dtl.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Grid View to Detials
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

            MyCmn.Sort(gv_datalistgrid_for_dtl, dtSource_for_dtl, e.SortExpression, sortingDirection);
            show_pagesx1.Text = "Page: <b>" + (gv_datalistgrid_for_dtl.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_datalistgrid_for_dtl.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Pagination - Details
        //*************************************************************************
        protected void gv_datalistgrid_for_dtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_datalistgrid_for_dtl.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_datalistgrid_for_dtl, dtSource_for_dtl, "line_seq_no", Session["SortOrder"].ToString());
            show_pagesx1.Text = "Page: <b>" + (gv_datalistgrid_for_dtl.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_datalistgrid_for_dtl.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Search - Detials
        //*************************************************************************
        protected void txtb_search_details_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "deduc_seq LIKE '%" + txtb_search_details.Text.Trim().Replace("'","''") + "%' " +
                "OR monthyear_code_from LIKE '%" + txtb_search_details.Text.Trim().Replace("'", "''") + "%' " +
                "OR monthyear_code_to LIKE '%" + txtb_search_details.Text.Trim().Replace("'", "''") + "%' " +
                "OR addon_amount2 LIKE '%" + txtb_search_details.Text.Trim().Replace("'", "''") + "%' " +
                "OR addon_amount1 LIKE '%" + txtb_search_details.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("deduc_seq", typeof(System.String));
            dtSource1.Columns.Add("line_seq_no", typeof(System.String));
            dtSource1.Columns.Add("monthyear_code_from", typeof(System.String));
            dtSource1.Columns.Add("monthyear_code_to", typeof(System.String));
            dtSource1.Columns.Add("addon_amount1", typeof(System.Decimal));
            dtSource1.Columns.Add("addon_amount2", typeof(System.Decimal));

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
        //  BEGIN - JVA- 2019-01-17 - Visible if the Session is not Empty
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

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_loan_account_name.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "")
            {
                // if (ddl_empl_type.SelectedValue == "JO")
                // {
                //     gv_dataListGrid.Columns[5].Visible = true;
                // }
                // else
                // {
                //     gv_dataListGrid.Columns[5].Visible = false;
                // }
                RetrieveDataListGrid();
                
                btnAdd.Visible = true;
                
            }
            else
            {
                btnAdd.Visible = false;
            }
            
            UpdatePanel10.Update();
            SearchDate(txtb_search.Text.ToString().Trim());
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Initialized During Add Mode
        //**************************************************************************
        private void HeaderDetails_Initialized_Add()
        {
            DataRow[] row2Edit2 = dtSource_for_names.Select("empl_id = '" + ddl_empl_name.SelectedValue.ToString().Trim() + "'");

            if (dtSource_for_names.Rows.Count > 0)
            {
                txtb_employee_id.Text   = row2Edit2[0]["empl_id"].ToString();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/06/2019 - To Update The Table 
        //**************************************************************************
        protected void lnk_btn_continue_Command(object sender, CommandEventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;
            
            dtSource.Rows[0]["empl_id"]               = txtb_employee_id.Text;
            dtSource.Rows[0]["deduc_code"]            = txtb_deduc_code.Text.ToString().Trim();
            dtSource.Rows[0]["deduc_seq"]             = txtb_deduc_seq_hidden.Text.ToString();
            dtSource.Rows[0]["deduc_date_from"]       = txtb_date_from.Text.ToString().Trim();
            dtSource.Rows[0]["deduc_date_to"]         = txtb_date_to.Text.ToString().Trim();
            dtSource.Rows[0]["deduc_ref_nbr"]         = txtb_check_ref_no.Text.ToString().Trim();
            dtSource.Rows[0]["deduc_amount1"]         = txtb_amount_deduct1.Text.ToString().Trim();
            dtSource.Rows[0]["deduc_amount2"]         = txtb_amount_deduct2.Text.ToString().Trim();
            dtSource.Rows[0]["deduc_loan_amount"]     = txtb_account_amount.Text.ToString().Trim();
            dtSource.Rows[0]["deduc_nbr_months"]      = txtb_no_of_months.Text;
            dtSource.Rows[0]["deduc_status"]          = ddl_load_status.SelectedValue.ToString().Trim();
            dtSource.Rows[0]["deduc_source"]          = txtb_deduc_source_hidden.Text;
            dtSource.Rows[0]["created_by_user"]       = Session["ep_user_id"].ToString().Trim();
            dtSource.Rows[0]["created_dttm"]          = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dtSource.Rows[0]["updated_by_user"]       = "";
            dtSource.Rows[0]["updated_dttm"]          = "";

            scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
            // Update All Previous Account Name to In-Active
            //DataTable dt = MyCmn.RetrieveData("sp_update_payrollemployeeaccountledger", "par_empl_id", txtb_employee_id.Text, "par_account_code_with_sub", ddl_loan_account_name.SelectedValue.ToString());
            string tablename = dtSource.TableName.ToString();
            string set_param = "deduc_status = 'I'";
            string whereparam = "WHERE empl_id = '"+ txtb_employee_id.Text.ToString().Trim() + "' AND deduc_code = '" + txtb_deduc_code.Text.ToString().Trim() + "' AND deduc_seq != '" + txtb_deduc_seq_hidden.Text.ToString() + "' ";
            MyCmn.UpdateTable(tablename, set_param, whereparam);

            if (scriptInsertUpdate == string.Empty) return;
            string msg = MyCmn.insertdata(scriptInsertUpdate);
            if (msg == "") return;
            if (msg.Substring(0, 1) == "X") return;

            dtSource_for_dtl.Columns.Remove("monthyear_code_from_descr");
            dtSource_for_dtl.Columns.Remove("monthyear_code_to_descr");
            string[] insert_empl_script = MyCmn.get_insertscript(dtSource_for_dtl).Split(';');
            MyCmn.DeleteBackEndData(dtSource_for_dtl.TableName.ToString(), "WHERE deduc_seq ='" + txtb_deduc_seq_hidden.Text.ToString().Trim() + "'");
            for (int x = 0; x < insert_empl_script.Length; x++)
            {
                string insert_script = "";
                insert_script = insert_empl_script[x];
                MyCmn.insertdata(insert_script);
            }

            RetrieveDataListGrid();
            DeleteInsert_BreakDown();
            SaveAddEdit.Text = MyCmn.CONST_NEWREC;
            i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
            h2_status.InnerText = "SUCCESSFULLY ADDED";
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop33", "closeModal();", true);
            ViewState.Remove("AddEdit_Mode");
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

            SearchDate(txtb_search.Text.ToString().Trim());
        }
        
        //**************************************************************************
        //  BEGIN - VJA- 10/02/2019 - Open Modal for Add ons
        //**************************************************************************
        protected void btn_addon_Click(object sender, EventArgs e)
        {
            
            if (IsDataValidated())
            {
                lbl_hidden_line_seq.Text = "";
                ddl_addon_date_from.SelectedIndex = -1;
                ddl_addon_date_to.SelectedIndex = -1;
                txtb_addon_amount1.Text = "0.00";
                txtb_addon_amount2.Text = "0.00";

                ddl_addon_date_from.Enabled = true;
                ddl_addon_date_to.Enabled = true;
                RetrieveDateMonth();

                //UpdatePanel1.Update();
                //UpdatePanel3.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "openAddOnTabs();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Click2nd", "click_2ndttab();", true);
                ViewState["AddOn_EditAdd"] = MyCmn.CONST_ADD;
            }
            
        }
        //**************************************************************************
        //  BEGIN - VJA- 10/02/2019 - Button that Save or Edit Add on
        //**************************************************************************
        protected void btn_addons_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            if (IsDataValidated2())
            {
                
                    if (ViewState["AddOn_EditAdd"].ToString() == MyCmn.CONST_ADD)
                    {
                        bool if_okay = true;

                        for (int x = 0; x < dtSource_for_dtl.Rows.Count; x++)
                        {
                            for (int y = 0; y < dtSource_for_dtl.Rows.Count; y++)
                            {
                                if (Convert.ToUInt32(dtSource_for_dtl.Rows[y]["monthyear_code_from"].ToString()) >= Convert.ToUInt32(ddl_addon_date_from.SelectedValue.ToString()) || Convert.ToUInt32(dtSource_for_dtl.Rows[x]["monthyear_code_to"].ToString()) >= Convert.ToUInt32(ddl_addon_date_to.SelectedValue.ToString()) || Convert.ToUInt32(dtSource_for_dtl.Rows[x]["monthyear_code_to"].ToString()) >= Convert.ToUInt32(ddl_addon_date_from.SelectedValue.ToString()))
                                {
                                    if_okay = false;
                                }
                            }
                        }
                        if (!if_okay)
                        {
                            FieldValidationColorChanged(true, "date-already-exist");
                            ddl_addon_date_from.Focus();
                            return;
                        }


                        int dtRowCont = dtSource_for_dtl.Rows.Count - 1;
                        string lastCode = "000";

                        if (dtRowCont > -1)
                        {
                            DataRow lastRow = dtSource_for_dtl.Rows[dtRowCont];
                            lastCode = lastRow["line_seq_no"].ToString();
                        }

                        int lastCodeInt = int.Parse(lastCode) + 1;
                        string nextCode = lastCodeInt.ToString();
                        nextCode = nextCode.PadLeft(3, '0');

                        DataRow nrow1 = dtSource_for_dtl.NewRow();
                        
                        nrow1["empl_id"]                    = txtb_employee_id.Text;
                        nrow1["deduc_code"]                 = txtb_deduc_code.Text;
                        nrow1["deduc_seq"]                  = txtb_deduc_seq_hidden.Text;
                        nrow1["line_seq_no"]                = nextCode.ToString();
                        nrow1["monthyear_code_from"]        = ddl_addon_date_from.SelectedValue.ToString().Trim();
                        nrow1["monthyear_code_from_descr"]  = ddl_addon_date_from.SelectedItem.ToString().Trim();
                        nrow1["monthyear_code_to"]          = ddl_addon_date_to.SelectedValue.ToString().Trim();
                        nrow1["monthyear_code_to_descr"]    = ddl_addon_date_to.SelectedItem.ToString().Trim();
                        nrow1["addon_amount1"]              = float.Parse(txtb_addon_amount1.Text).ToString("###,##0.00"); 
                        nrow1["addon_amount2"]              = float.Parse(txtb_addon_amount2.Text).ToString("###,##0.00");
                        nrow1["created_by_user"]            = Session["ep_user_id"].ToString();
                        nrow1["created_dttm"]               = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        nrow1["updated_by_user"]            = "";
                        nrow1["updated_dttm"]               = "1900-01-01";
                        nrow1["action"] = 1;
                        nrow1["retrieve"] = false;
                        dtSource_for_dtl.Rows.Add(nrow1);


                    }
                    else if (ViewState["AddOn_EditAdd"].ToString() == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "line_seq_no = '" + lbl_hidden_line_seq.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dtSource_for_dtl.Select(editExpression);

                        row2Edit[0]["monthyear_code_from"]       = ddl_addon_date_from.SelectedValue.ToString().Trim();
                        row2Edit[0]["monthyear_code_from_descr"] = ddl_addon_date_from.SelectedItem.ToString().Trim();
                        row2Edit[0]["monthyear_code_to"]         = ddl_addon_date_to.SelectedValue.ToString().Trim();
                        row2Edit[0]["monthyear_code_to_descr"]   = ddl_addon_date_to.SelectedItem.ToString().Trim();
                        row2Edit[0]["addon_amount1"]             = float.Parse(txtb_addon_amount1.Text).ToString("###,##0.00");
                        row2Edit[0]["addon_amount2"]             = float.Parse(txtb_addon_amount2.Text).ToString("###,##0.00");
                        row2Edit[0]["updated_by_user"]           = Session["ep_user_id"].ToString();
                        row2Edit[0]["updated_dttm"]              = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                    lbl_hidden_line_seq.Text = "";
                    ddl_addon_date_from.SelectedIndex = -1;
                    ddl_addon_date_to.SelectedIndex = -1;
                    txtb_addon_amount1.Text = "0.00";
                    txtb_addon_amount2.Text = "0.00";

                    MyCmn.Sort(gv_datalistgrid_for_dtl, dtSource_for_dtl, "line_seq_no", "ASC");
                    panel_datalistgrid_dtl.Update();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseAddOn", "closeAddOnTabs();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Click2nd", "click_2ndttab();", true);

                
            }

        }
        //**************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Objects data Validation | For Loan Add Ons
        //*************************************************************************
        private bool IsDataValidated2()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");
            try
            {
                if (ddl_addon_date_from.SelectedValue == "")
                {
                    FieldValidationColorChanged(true, "ddl_addon_date_from");
                    ddl_addon_date_from.Focus();
                    validatedSaved = false;
                }
                if (ddl_addon_date_to.SelectedValue == "")
                {
                    FieldValidationColorChanged(true, "ddl_addon_date_to");
                    ddl_addon_date_to.Focus();
                    validatedSaved = false;
                }
                if (CommonCode.checkisdecimal(txtb_addon_amount1) == false)
                {
                    FieldValidationColorChanged(true, "invalid-txtb_addon_amount1");
                    txtb_addon_amount1.Focus();
                    validatedSaved = false;
                }
                if (CommonCode.checkisdecimal(txtb_addon_amount2) == false)
                {
                    FieldValidationColorChanged(true, "invalid-txtb_addon_amount2");
                    txtb_addon_amount2.Focus();
                    validatedSaved = false;
                }
                if ( Convert.ToInt32(ddl_addon_date_to.SelectedValue) < Convert.ToInt32(ddl_addon_date_from.SelectedValue))
                {
                    FieldValidationColorChanged(true, "is-greater-than-datefrom");
                    ddl_addon_date_from.Focus();
                    validatedSaved = false;
                }
            }
            catch (Exception)
            {

            }
            //UpdatePanel1.Update();
            //UpdatePanel3.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
            return validatedSaved;

        }
        //**************************************************************************
        //  BEGIN - VJA- 10/02/2019 - Delete Add Ons
        //**************************************************************************
        protected void imgbtn_dlt_addon_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string deleteExpression = "line_seq_no = '" + svalues.ToString().Trim() + "'";
            
            DataRow[] row2Delete = dtSource_for_dtl.Select(deleteExpression);
            dtSource_for_dtl.Rows.Remove(row2Delete[0]);
            dtSource_for_dtl.AcceptChanges();
            MyCmn.Sort(gv_datalistgrid_for_dtl, dtSource_for_dtl, "line_seq_no", Session["SortOrder"].ToString());

            panel_datalistgrid_dtl.Update();
            show_pagesx.Text = "Page: <b>" + (gv_datalistgrid_for_dtl.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_datalistgrid_for_dtl.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - VJA- 10/02/2019 - Edit Add Ons
        //**************************************************************************
        protected void imgbtn_edit_addon_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string editExpression = "line_seq_no = '" + svalues.ToString().Trim() + "'";

            lbl_hidden_line_seq.Text = "";
            ddl_addon_date_from.SelectedIndex = -1;
            ddl_addon_date_to.SelectedIndex = -1;
            txtb_addon_amount1.Text = "0.00";
            txtb_addon_amount2.Text = "0.00";

            RetrieveDateMonth();

            ddl_addon_date_from.Enabled = false;
            ddl_addon_date_to.Enabled = false;
            FieldValidationColorChanged(false,"ALL");

            DataRow[] row2Edit = dtSource_for_dtl.Select(editExpression);
            lbl_hidden_line_seq.Text            = row2Edit[0]["line_seq_no"].ToString();
            ddl_addon_date_from.SelectedValue   = row2Edit[0]["monthyear_code_from"].ToString();
            ddl_addon_date_to.SelectedValue     = row2Edit[0]["monthyear_code_to"].ToString();
            txtb_addon_amount1.Text             = row2Edit[0]["addon_amount1"].ToString();
            txtb_addon_amount2.Text             = row2Edit[0]["addon_amount2"].ToString();

            //UpdatePanel1.Update();
            //UpdatePanel3.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "openAddOnTabs();", true);
            ViewState["AddOn_EditAdd"] = MyCmn.CONST_EDIT;
        }
        //**************************************************************************
        //  BEGIN - VJA- 10/02/2019 - Select Employment Type
        //**************************************************************************
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_loan_account_name.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "")
            {
                // if (ddl_empl_type.SelectedValue == "JO")
                // {
                //     gv_dataListGrid.Columns[5].Visible = true;
                // }
                // else
                // {
                //     gv_dataListGrid.Columns[5].Visible = false;
                // }
                RetrieveDataListGrid();
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveDeductionList();
            RetrieveDataListGrid();
            UpdatePanel10.Update();
            SearchDate(txtb_search.Text.ToString().Trim());
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetrieveDateMonth()
        {
            ddl_addon_date_from.Items.Clear();
            ddl_addon_date_to.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_date_month_list", "par_date_fr", txtb_date_from.Text , "par_date_to", txtb_date_to.Text );

            ddl_addon_date_from.DataSource = dt;
            ddl_addon_date_from.DataValueField = "monthyear_code";
            ddl_addon_date_from.DataTextField = "monthyear_descr";
            ddl_addon_date_from.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_addon_date_from.Items.Insert(0, li);

            ddl_addon_date_to.DataSource = dt;
            ddl_addon_date_to.DataValueField = "monthyear_code";
            ddl_addon_date_to.DataTextField = "monthyear_descr";
            ddl_addon_date_to.DataBind();
            ListItem li1 = new ListItem("-- Select Here --", "");
            ddl_addon_date_to.Items.Insert(0, li1);
        }

        protected void ddl_loan_account_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_loan_account_name.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "")
            {
                string select = "deduc_code = '" + ddl_loan_account_name.SelectedValue.ToString().Trim() + "'";
                DataRow[] row2Delete = dtSource_DeductionList.Select(select);
                
                // if (ddl_empl_type.SelectedValue == "JO")
                // {
                //     gv_dataListGrid.Columns[5].Visible = true;
                // }
                // else
                // {
                //     gv_dataListGrid.Columns[5].Visible = false;
                // }
                RetrieveDataListGrid();
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveDataListGrid();
            UpdatePanel10.Update();
            SearchDate(txtb_search.Text.ToString().Trim());
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Retrieve Account Next Seq Number
        //*************************************************************************
        private void RetrieveNextSeqNumber()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrolldeduc_ledger_tbl_get_nxt_seq","par_empl_id",txtb_employee_id.Text.ToString().Trim(), "par_deduc_code",txtb_deduc_code.Text.ToString().Trim());
            txtb_deduc_seq_hidden.Text = dt.Rows[0]["next_deduc_seq"].ToString().Trim();
        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Sorting on Deduction Details
        //*************************************************************************
        protected void gv_deduction_details_Sorting(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(gv_deduction_details, dtSource_Deduction_details, e.SortExpression, sortingDirection);
            up_deduction_details.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClick3rdTab", "click_3rdttab();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Page Index Changing on Deduction Details
        //*************************************************************************
        protected void gv_deduction_details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_deduction_details.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_deduction_details, dtSource_Deduction_details, "payroll_yrmonth", Session["SortOrder"].ToString());
            up_deduction_details.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClick3rdTab", "click_3rdttab();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Show Entries Deduction Details
        //*************************************************************************
        protected void DropDownList2_TextChanged(object sender, EventArgs e)
        {
            gv_deduction_details.PageSize = Convert.ToInt32(DropDownList2.Text);
            CommonCode.GridViewBind(ref this.gv_deduction_details, dtSource_Deduction_details);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClick3rdTab", "click_3rdttab();", true);
        }
        //*************************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Toogle Deduction details and Ledger History
        //  Description : Will only be visible if Deduction Type Is 05 - Personal Loans, 
        //            07 - Other Deductions to GP, and , 08 - Other Personal Share (PS)	
        //*************************************************************************************
        private void Toggle_DeductionDetails()
        {
            if (ddl_accttype_main.SelectedValue == "03" || // Update Date : 2020-07-15 - Epa Enable and Deduction Type nga 03 for SIF and GSIS(PS)
                ddl_accttype_main.SelectedValue == "04" ||
                ddl_accttype_main.SelectedValue == "05" ||
                ddl_accttype_main.SelectedValue == "06" ||
                ddl_accttype_main.SelectedValue == "07" ||
                ddl_accttype_main.SelectedValue == "08")
            {
                gv_up_ledger_info.Columns[4].Visible = true;
                
                txtb_date_from.Enabled  = true;
                txtb_date_to.Enabled    = true;
            }
            else
            {
                gv_up_ledger_info.Columns[4].Visible = false;

                txtb_date_from.Enabled = false;
                txtb_date_to.Enabled = false;
            }
        }
        //*************************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Toogle Deduction details and Ledger History
        //  Description :No of Months, Loan Amount and Loan Amount Paid
        //              Will only be visible if Deduction Type Is 
        //                  04	Personal Share(PS)
        //                  05	Personal Loans
        //                  06	Other Premiums
        //                  07	Other Deductions to GP
        //*************************************************************************************
        private void Toggle_Others()
        {
            if (ddl_accttype_main.SelectedValue == "04" ||
                ddl_accttype_main.SelectedValue == "05" ||
                ddl_accttype_main.SelectedValue == "06" ||
                ddl_accttype_main.SelectedValue == "07")
            {
                txtb_no_of_months.Visible   = true;
                txtb_amount_paid.Visible    = true;
                txtb_account_amount.Visible = true;
                lbl_no_of_months.Visible    = true;
                lbl_load_amount.Visible     = true;
                lbl_amount_paid.Visible     = true;

                //payment_details_hide.Visible = true;
                id_hide_payment_details.Visible = true;
                id_hide_ledger_brkdwn.Visible   = false;
            }
            else
            {
                txtb_no_of_months.Visible   = false;
                txtb_amount_paid.Visible    = false;
                txtb_account_amount.Visible = false;
                lbl_no_of_months.Visible    = false;
                lbl_load_amount.Visible     = false;
                lbl_amount_paid.Visible     = false;

                //payment_details_hide.Visible = false;
                id_hide_payment_details.Visible = false;
                id_hide_ledger_brkdwn.Visible   = false;
            }

            if (txtb_deduc_code.Text == "20201030-09") // HDMF MP2
            {
                id_hide_ledger_brkdwn.Visible = true;
            }

        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Date From and Date to Triggers on Input
        //*************************************************************************
        protected void txtb_date_from_to_TextChanged(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                FieldValidationColorChanged(false, "ALL");
                for_account_period_no_of_months();
            }
            
        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Button that Open Modal for Deduction Details
        //*************************************************************************
        protected void imgbtn_show_payment_details_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string select_deduc_seq = "deduc_seq = '" + svalues[0].ToString().Trim() + "'";
            string select_empl_id   = "empl_id = '" + svalues[1].ToString().Trim() + "'";

            InitializeTable_deduc_dtl();
            dtSource_Deduction_details = MyCmn.RetrieveData("sp_deduct_details_per_empl", "p_empl_id", svalues[1].ToString().Trim(), "p_employment_type", ddl_empl_type.Text.ToString().Trim(), "p_deduc_code", txtb_deduc_code.Text.ToString().Trim(), "p_deduc_seq", svalues[0].ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_deduction_details, dtSource_Deduction_details);
            up_deduction_details.Update();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopOpenDeduction", "openDeduction_details();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClick3rdTab", "click_3rdttab();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Sorting for Ledger Information
        //*************************************************************************
        protected void gv_up_ledger_info_Sorting(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(gv_up_ledger_info, dtSource_Ledger_info, e.SortExpression, sortingDirection);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClick3rdTab", "click_3rdttab();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Pagination for Ledger Information
        //*************************************************************************
        protected void gv_up_ledger_info_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_up_ledger_info.PageIndex = e.NewPageIndex;
            //MyCmn.Sort(gv_up_ledger_info, dtSource_Ledger_info, "payroll_yrmonth", Session["SortOrder"].ToString());
            CommonCode.GridViewBind(ref this.gv_up_ledger_info, dtSource_Ledger_info);
            up_ledger_info.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClick3rdTab", "click_3rdttab();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Show Entries for Ledger Information
        //*************************************************************************
        protected void DropDownList3_TextChanged(object sender, EventArgs e)
        {
            gv_up_ledger_info.PageSize = Convert.ToInt32(DropDownList3.Text);
            CommonCode.GridViewBind(ref this.gv_up_ledger_info, dtSource_Ledger_info);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClick3rdTab", "click_3rdttab();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 11/01/2019 - Select Account Type and Reteive Deduction List
        //*************************************************************************
        protected void ddl_accttype_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDeductionList();
            RetrieveDataListGrid();
            SearchDate(txtb_search.Text.ToString().Trim());
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Account Type
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
        //  BEGIN - JVA- 2019-01-17 - Initialized datasource fields/columns - Header
        //*************************************************************************
        private void InitializeTable_brkdwn()
        {
            dtSource_brkdwn = new DataTable();
            dtSource_brkdwn.Columns.Add("empl_id", typeof(System.String));
            dtSource_brkdwn.Columns.Add("deduc_code", typeof(System.String));
            dtSource_brkdwn.Columns.Add("deduc_seq", typeof(System.String));
            dtSource_brkdwn.Columns.Add("line_seq_no", typeof(System.String));
            dtSource_brkdwn.Columns.Add("deduc_ref_nbr", typeof(System.String));
            dtSource_brkdwn.Columns.Add("deduc_date_from", typeof(System.String));
            dtSource_brkdwn.Columns.Add("deduc_date_to", typeof(System.String));
            dtSource_brkdwn.Columns.Add("deduc_amt", typeof(System.String));
            dtSource_brkdwn.Columns.Add("created_by_user", typeof(System.String));
            dtSource_brkdwn.Columns.Add("created_dttm", typeof(System.String));
            dtSource_brkdwn.Columns.Add("updated_by_user", typeof(System.String));
            dtSource_brkdwn.Columns.Add("updated_dttm", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Add Primary Key Field to datasource - Header
        //*************************************************************************
        private void AddPrimaryKeys_brkdwn()
        {
            dtSource_brkdwn.TableName = "payrolldeduc_ledger_dtl_tbl";
            dtSource_brkdwn.Columns.Add("action", typeof(System.Int32));
            dtSource_brkdwn.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "deduc_code", "deduc_seq","line_seq_no" };
            dtSource_brkdwn = MyCmn.AddPrimaryKeys(dtSource_brkdwn, col);
        }
        //**************************************************************************
        //  BEGIN - JVA- 2019-01-17 - Objects data Validation | For Loan Add Ons
        //*************************************************************************
        private bool IsDataValidated3()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");

            // if (txtb_ref_nbr.Text == "")
            // {
            //     FieldValidationColorChanged(true, "txtb_ref_nbr");
            //     txtb_ref_nbr.Focus();
            //     validatedSaved = false;
            // }
            if (CommonCode.checkisdatetime(txtb_deduct_from_brkdwn) == false)
            {
                FieldValidationColorChanged(true, "txtb_deduct_from_brkdwn");
                txtb_deduct_from_brkdwn.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(txtb_deduct_to_brkdwn) == false)
            {
                FieldValidationColorChanged(true, "txtb_deduct_to_brkdwn");
                txtb_deduct_to_brkdwn.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_deduct_amt_brkdwn) == false)
            {
                FieldValidationColorChanged(true, "txtb_deduct_amt_brkdwn");
                txtb_deduct_amt_brkdwn.Focus();
                validatedSaved = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
            return validatedSaved;

        }

        //*************************************************************************
        //  BEGIN - VJA- 2020-05-30 - Add Button on Breakdown
        //*************************************************************************
        protected void btn_add_brkdwn_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");

            if (ddl_empl_name.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_name");
                ddl_empl_name.Focus();
            }
            else
            {
                txtb_ref_nbr.Text             = "";
                txtb_deduct_from_brkdwn.Text  = "";
                txtb_deduct_to_brkdwn.Text    = "";
                txtb_deduct_amt_brkdwn.Text   = "";
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "openBrkDwn();", true);
                ViewState["BrkDwn_EditAdd"] = MyCmn.CONST_ADD;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4thtb", "click_4thtab();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-05-30 - Button for Save Update and Add
        //*************************************************************************
        protected void btn_save_brkdwn_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            if (IsDataValidated3())
            {
                int dtRowCont = dtSource_brkdwn.Rows.Count - 1;
                string lastCode = "000";

                if (dtRowCont > -1)
                {
                    DataRow lastRow = dtSource_brkdwn.Rows[dtRowCont];
                    lastCode = lastRow["line_seq_no"].ToString();
                }

                int lastCodeInt = int.Parse(lastCode) + 1;
                string nextCode = lastCodeInt.ToString();
                nextCode = nextCode.PadLeft(3, '0');

                if (ViewState["BrkDwn_EditAdd"].ToString() == MyCmn.CONST_ADD)
                {
                    DataRow nrow1 = dtSource_brkdwn.NewRow();

                    nrow1["empl_id"]          = txtb_employee_id.Text;
                    nrow1["deduc_code"]       = txtb_deduc_code.Text;
                    nrow1["deduc_seq"]        = txtb_deduc_seq_hidden.Text;
                    nrow1["line_seq_no"]      = nextCode.ToString();
                    nrow1["deduc_ref_nbr"]    = txtb_ref_nbr.Text.ToString();
                    nrow1["deduc_date_from"]  = txtb_deduct_from_brkdwn.Text.ToString();
                    nrow1["deduc_date_to"]    = txtb_deduct_from_brkdwn.Text.ToString();
                    nrow1["deduc_amt"]        = float.Parse(txtb_deduct_amt_brkdwn.Text).ToString("###,##0.00");
                    nrow1["created_by_user"]  = Session["ep_user_id"].ToString();
                    nrow1["created_dttm"]     = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    nrow1["updated_by_user"]  = "";
                    nrow1["updated_dttm"]     = "1900-01-01";
                    nrow1["action"]           = 1;
                    nrow1["retrieve"]         = false;
                    dtSource_brkdwn.Rows.Add(nrow1);
                }
                else if (ViewState["BrkDwn_EditAdd"].ToString() == MyCmn.CONST_EDIT)
                {
                    string editExpression = btn_save_brkdwn.CommandArgument.ToString();
                    DataRow[] row2Edit = dtSource_brkdwn.Select(editExpression);

                    row2Edit[0]["deduc_ref_nbr"]    = txtb_ref_nbr.Text.ToString().Trim();
                    row2Edit[0]["deduc_date_from"]  = txtb_deduct_from_brkdwn.Text.ToString().Trim();
                    row2Edit[0]["deduc_date_to"]    = txtb_deduct_to_brkdwn.Text.ToString().Trim();
                    row2Edit[0]["deduc_amt"]        = float.Parse(txtb_deduct_amt_brkdwn.Text).ToString("###,##0.00");
                    row2Edit[0]["updated_by_user"]  = Session["ep_user_id"].ToString();
                    row2Edit[0]["updated_dttm"]     = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                // if (ViewState["BrkDwn_EditAdd"].ToString() == MyCmn.CONST_ADD)
                // {
                //     if (checkif_match() == false)
                //     {
                //         FieldValidationColorChanged(true, "did-not-match");
                //         string deleteExpression = "empl_id = '" + txtb_employee_id.Text.ToString().Trim() + "' AND deduc_code = '" + txtb_deduc_code.Text.ToString().Trim() + "' AND deduc_seq = '" + txtb_deduc_seq_hidden.Text.ToString().Trim() + "' AND line_seq_no = '" + nextCode.ToString().Trim() + "'";
                //         DataRow[] row2Delete = dtSource_brkdwn.Select(deleteExpression);
                //         dtSource_brkdwn.Rows.Remove(row2Delete[0]);
                //         dtSource_brkdwn.AcceptChanges();
                //         return;
                //     }
                // }
                // else if (ViewState["BrkDwn_EditAdd"].ToString() == MyCmn.CONST_EDIT)
                // {
                //     if (checkif_match() == false)
                //     {
                //         FieldValidationColorChanged(true, "did-not-match");
                //         return;
                //     }
                // }

                MyCmn.Sort(gv_ledger_brkdwn, dtSource_brkdwn, "line_seq_no", "ASC");
                up_ledger_brkdwn.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseAddOn", "closeBrkDwnTabs();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4thtb", "click_4thtab();", true);
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-05-30 - Edit Breakdown Information
        //*************************************************************************
        protected void img_edit_brkdwn_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id      = svalues[0].ToString().Trim() ;
            string deduc_code   = svalues[1].ToString().Trim() ;
            string deduc_seq    = svalues[2].ToString().Trim() ;
            string line_seq_no  = svalues[3].ToString().Trim() ;

            string editExpression = "empl_id = '" + empl_id.ToString().Trim() + "' AND deduc_code = '" + deduc_code.ToString().Trim() + "' AND deduc_seq = '" + deduc_seq.ToString().Trim() + "' AND line_seq_no = '" + line_seq_no.ToString().Trim() + "'";
            DataRow[] row2Edit = dtSource_brkdwn.Select(editExpression);
            
            txtb_ref_nbr.Text             = row2Edit[0]["deduc_ref_nbr"].ToString();
            txtb_deduct_from_brkdwn.Text  = row2Edit[0]["deduc_date_from"].ToString();
            txtb_deduct_to_brkdwn.Text    = row2Edit[0]["deduc_date_to"].ToString();
            txtb_deduct_amt_brkdwn.Text   = row2Edit[0]["deduc_amt"].ToString();

            UpdatePanel_From.Update();
            UpdatePanel_To.Update();

            btn_save_brkdwn.CommandArgument = editExpression;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "openBrkDwn();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4thtb", "click_4thtab();", true);
            ViewState["BrkDwn_EditAdd"] = MyCmn.CONST_EDIT;
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-05-30 - De;ete Breakdown Information
        //*************************************************************************
        protected void img_delete_brkdwn_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id      = svalues[0].ToString().Trim() ;
            string deduc_code   = svalues[1].ToString().Trim() ;
            string deduc_seq    = svalues[2].ToString().Trim() ;
            string line_seq_no  = svalues[3].ToString().Trim() ;

            string deleteExpression = "empl_id = '" + empl_id.ToString().Trim() + "' AND deduc_code = '" + deduc_code.ToString().Trim() + "' AND deduc_seq = '" + deduc_seq.ToString().Trim() + "' AND line_seq_no = '" + line_seq_no.ToString().Trim() + "'";

            DataRow[] row2Delete = dtSource_brkdwn.Select(deleteExpression);
            dtSource_brkdwn.Rows.Remove(row2Delete[0]);
            dtSource_brkdwn.AcceptChanges();
            MyCmn.Sort(gv_ledger_brkdwn, dtSource_brkdwn, "line_seq_no", Session["SortOrder"].ToString());
            up_ledger_brkdwn.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4thtb", "click_4thtab();", true);
        }
        // *********************************************************************************
        // BEGIN - VJA- 2020-05-21 Check if Allow Save Breakdown  - Specifically MP2 *******
        // *********************************************************************************
        private bool checkif_match()
        {
            bool validatedSaved = true;
            if (txtb_deduc_code.Text == "20201030-09") //  HDMF MP2
            {
                double amount = 0;
                for (int x = 0; x < dtSource_brkdwn.Rows.Count; x++)
                {
                    amount += double.Parse(dtSource_brkdwn.Rows[x]["deduc_amt"].ToString().Trim());
                }
                if (amount > double.Parse(txtb_amount_deduct1.Text) || // Condition : - Not Greater than the Amount 1
                    amount < double.Parse(txtb_amount_deduct1.Text))   // Condition : - Not Less than the Amount 1

                {
                    validatedSaved = false;
                }
            }
            return validatedSaved;
        }

        //******************************************************************************
        //  BEGIN - VJA - 2020-05-21 - Delete and Insert Method - Specifically MP2
        //******************************************************************************
        protected void DeleteInsert_BreakDown()
        {
            string[] insert_empl_script = MyCmn.get_insertscript(dtSource_brkdwn).Split(';');
            MyCmn.DeleteBackEndData(dtSource_brkdwn.TableName.ToString(), "WHERE empl_id ='" + txtb_employee_id.Text.ToString().Trim() + "' AND deduc_seq = '" + txtb_deduc_seq_hidden.Text.ToString().Trim() + "'AND deduc_code = '" + txtb_deduc_code.Text.ToString().Trim() + "'");
            for (int x = 0; x < insert_empl_script.Length; x++)
            {
                string insert_script = "";
                insert_script = insert_empl_script[x];
                MyCmn.insertdata(insert_script);
            }
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            gv_ledger_brkdwn.PageSize = Convert.ToInt32(DropDownList4.Text);
            CommonCode.GridViewBind(ref this.gv_ledger_brkdwn, dtSource_brkdwn);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4thtb", "click_4thtab();", true);
        }
        
        protected void gv_ledger_brkdwn_Sorting(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(gv_ledger_brkdwn, dtSource_brkdwn, e.SortExpression, sortingDirection);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4thtb", "click_4thtab();", true);
        }

        protected void gv_ledger_brkdwn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_ledger_brkdwn.PageIndex = e.NewPageIndex;
            CommonCode.GridViewBind(ref this.gv_ledger_brkdwn, dtSource_brkdwn);
            up_ledger_brkdwn.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4thtb", "click_4thtab();", true);
        }
        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            SearchDate(txtb_search.Text.ToString().Trim());
        }

        //******************************************************************************
        //  BEGIN - VJA - 2020-05-21 - Search Data
        //******************************************************************************
        private void SearchDate(string search)
        {
            string searchExpression = "empl_id LIKE '%" + search.Trim().Replace("'", "''") + "%' " +
                "OR employee_name LIKE '%" +   search.Trim().Replace("'", "''") + "%' " +
                "OR deduc_date_from LIKE '%" + search.Trim().Replace("'", "''") + "%' " +
                "OR deduc_date_to LIKE '%" +   search.Trim().Replace("'", "''") + "%' " +
                "OR deduc_amount1 LIKE '%" +   search.Trim().Replace("'", "''") + "%' " +
                "OR deduc_amount2 LIKE '%" +   search.Trim().Replace("'", "''") + "%' ";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("deduc_code", typeof(System.String));
            dtSource1.Columns.Add("deduc_seq", typeof(System.String));
            dtSource1.Columns.Add("deduc_date_from", typeof(System.String));
            dtSource1.Columns.Add("deduc_date_to", typeof(System.String));
            dtSource1.Columns.Add("deduc_ref_nbr", typeof(System.String));
            dtSource1.Columns.Add("deduc_amount1", typeof(System.String));
            dtSource1.Columns.Add("deduc_amount2", typeof(System.String));
            dtSource1.Columns.Add("deduc_loan_amount", typeof(System.String));
            dtSource1.Columns.Add("deduc_nbr_months", typeof(System.String));
            dtSource1.Columns.Add("deduc_status", typeof(System.String));
            dtSource1.Columns.Add("deduc_source", typeof(System.String));
            dtSource1.Columns.Add("created_by_user", typeof(System.String));
            dtSource1.Columns.Add("created_dttm", typeof(System.String));
            dtSource1.Columns.Add("updated_by_user", typeof(System.String));
            dtSource1.Columns.Add("updated_dttm", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));


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
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}