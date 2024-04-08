//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     01/17/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cPayrollMaster
{
    public partial class cPayrollMaster : System.Web.UI.Page
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
        DataTable dtSource_dtl
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
        
        DataTable dtSource_pos
        {
            get
            {
                if ((DataTable)ViewState["dtSource_pos"] == null) return null;
                return (DataTable)ViewState["dtSource_pos"];
            }
            set
            {
                ViewState["dtSource_pos"] = value;
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
        DataTable dtSource_reProcess
        {
            get
            {
                if ((DataTable)ViewState["dtSource_reProcess"] == null) return null;
                return (DataTable)ViewState["dtSource_reProcess"];
            }
            set
            {
                ViewState["dtSource_reProcess"] = value;
            }
        }
        DataTable dtSource_deduction_ledger
        {
            get
            {
                if ((DataTable)ViewState["dtSource_deduction_ledger"] == null) return null;
                return (DataTable)ViewState["dtSource_deduction_ledger"];
            }
            set
            {
                ViewState["dtSource_deduction_ledger"] = value;
            }
        }
        DataTable dtSource_deduction_ledger_audit
        {
            get
            {
                if ((DataTable)ViewState["dtSource_deduction_ledger_audit"] == null) return null;
                return (DataTable)ViewState["dtSource_deduction_ledger_audit"];
            }
            set
            {
                ViewState["dtSource_deduction_ledger_audit"] = value;
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
            }
        }

        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            if (ddl_dep.SelectedValue != "" || ddl_empl_type.SelectedValue != "")
            {
                chkIncludeHistory.Enabled = true;
            }
            else
            {
                chkIncludeHistory.Enabled = false;

            }
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            ViewState["chkIncludeHistory"] = "N";
            
            RetrieveBindingDepartments();
            RetrieveEmploymentType();
            //
            //RetrieveEmpl();
            RetrieveDataListGrid();
            RetrieveBindingGroupings();
            //
            //// BEGIN - UPDATE : JADE - 05/20/2019 - Add Fields
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveBindingFundcharges();
            RetrieveBindingFunction();
            RetrievePosition();
            //// END  - UPDATE : JADE - 05/20/2019 - Add Fields
            //
            //RetrieveDataListGrid_dtl();
            RetrieveYear();
            RetrieveBindingBudgetYear();
            btnAdd.Visible = false;

            ViewState["prev_chckbox_flag_gsis"]   = false;
            ViewState["prev_chckbox_flag_phic"]   = false;
            ViewState["prev_chckbox_flag_hdmf"]   = false;
            ViewState["prev_txtb_hdmf_fix_rate"]  = 0;
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_include_history", ViewState["chkIncludeHistory"].ToString().Trim());

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView - Details
        //*************************************************************************
        private void RetrieveDataListGrid_dtl()
        {
            dtSource_dtl = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_dtl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_rate_basis", txtb_rate_basis_hidden.Text.ToString().Trim(), "par_effective_date", txtb_effective_date.Text.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_details, dtSource_dtl);
            update_details.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView - Details
        //*************************************************************************
        private void RetrieveDataListGrid_empl_history()
        {
            dtSource_History = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_empl_history_list", "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            MyCmn.Sort(gv_datagrid_history, dtSource_History, "effective_date", "DESC");
            CommonCode.GridViewBind(ref this.gv_datagrid_history, dtSource_History);
            up_datagrid_history.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetrieveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            ddl_employment_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");

            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_type.Items.Insert(0, li);

            ddl_employment_type.DataSource = dt;
            ddl_employment_type.DataValueField = "employment_type";
            ddl_employment_type.DataTextField = "employmenttype_description";
            ddl_employment_type.DataBind();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Employee Nmae
        //*************************************************************************
        private void RetrieveEmpl()
        {
            ddl_empl_name.Items.Clear();
            dtSource_for_names = MyCmn.RetrieveData("sp_personnelnames_combolist_paymaster", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim());

            ddl_empl_name.DataSource = dtSource_for_names;
            ddl_empl_name.DataValueField = "empl_id";
            ddl_empl_name.DataTextField = "employee_name";
            ddl_empl_name.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_name.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Department
        //*************************************************************************
        private void RetrieveBindingDepartments()
        {
            ddl_department.Items.Clear();
            ddl_dep.Items.Clear();
            ddl_department_report.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_departments_tbl_list", "par_include_history", "N");

            ddl_department.DataSource = dt;
            ddl_department.DataValueField = "department_code";
            ddl_department.DataTextField = "department_name1";
            ddl_department.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_department.Items.Insert(0, li);

            ddl_dep.DataSource = dt;
            ddl_dep.DataValueField = "department_code";
            ddl_dep.DataTextField = "department_name1";
            ddl_dep.DataBind();
            ListItem li1 = new ListItem("-- Select Here --", "");
            ddl_dep.Items.Insert(0, li1);

            ddl_department_report.DataSource = dt;
            ddl_department_report.DataValueField = "department_code";
            ddl_department_report.DataTextField = "department_name1";
            ddl_department_report.DataBind();
            ListItem li2 = new ListItem("All Department", "");
            ddl_department_report.Items.Insert(0, li2);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Grouping
        //*************************************************************************
        private void RetrieveBindingGroupings()
        {
            ddl_group_nbr.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_combolist", "par_employment_type", ddl_empl_type.SelectedValue.ToString(),"par_special_group","01","par_department_code",ddl_dep.SelectedValue.ToString().Trim());
            dt.Columns.Add("payrollgroup_with_descr", typeof(string), "grouping_descr+' - '+payroll_group_nbr");
            ddl_group_nbr.DataSource = dt;
            ddl_group_nbr.DataValueField = "payroll_group_nbr";
            ddl_group_nbr.DataTextField = "payrollgroup_with_descr";
            //ddl_group_nbr.DataTextFormatString = string.Format("{0} - {1}", "grouping_descr", "payroll_group_nbr");
            ddl_group_nbr.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_group_nbr.Items.Insert(0, li);
        }
        
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            //For Header
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();

            InitializeTable_pos();
            AddPrimaryKeys_pos();
            AddNewRow_pos();
            
            RetrievePosition();
            RetrieveDataListGrid_dtl();
            RetriveDeductionLedger();
            RetrieveBindingGroupings();
            RetrieveDataListGrid_empl_history();

            txtb_effective_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ViewState["payroll_group_nbr"] = "";

            ViewState["prev_chckbox_flag_gsis"]   = false;
            ViewState["prev_chckbox_flag_phic"]   = false;
            ViewState["prev_chckbox_flag_hdmf"]   = false;
            ViewState["prev_txtb_hdmf_fix_rate"]  = 0;
            
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
            FieldValidationColorChanged(false, "ALL");

            ddl_empl_name.Enabled       = true;
            ddl_empl_name.Visible       = true;
            txtb_effective_date.Enabled = true;
            ddl_group_nbr.Enabled       = true;
            txtb_empl_name.Visible      = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            ddl_empl_name.SelectedIndex = 0;
            txtb_period_from.Text = "";
            txtb_period_to.Text = "";
            txtb_monthly_rate.Text = "";
            txtb_daily_rate.Text = "";
            txtb_hourly_rate.Text = "";
            txtb_empl_id.Text = "";
            
            ddl_dep.SelectedIndex           = -1;
            ddl_subdep.SelectedIndex        = -1;
            ddl_division.SelectedIndex      = -1;
            ddl_section.SelectedIndex       = -1;
            ddl_function_code.SelectedIndex = -1;
            ddl_fund_charges.SelectedIndex  = -1;
            ddl_position.SelectedIndex      = -1;
            ddl_emp_status.SelectedValue    = "1";

            txtb_hazard_pay_override_hidden.Text = "";
            txtb_rate_basis_hidden.Text          = "";

            chckbox_flag_gsis.Checked = false;
            chckbox_flag_phic.Checked = false;
            chckbox_flag_hdmf.Checked = false;
            txtb_hdmf_fix_rate.Text = "0.00";
            txtb_date_of_assumption.Text = "";
            txtb_salary_grade.Text = "";
            txtb_birth_date.Text = "";
            txtb_remarks.Text = "";
            
            UpdatePanelTo.Update();
            UpdatePanelFrom.Update();
            UpdatePanelEffec.Update();
            UpdatePanel34.Update();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popshowdate", "show_date();", true);

        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("employment_type", typeof(System.String));
            dtSource.Columns.Add("monthly_rate", typeof(System.String));
            dtSource.Columns.Add("daily_rate", typeof(System.String));
            dtSource.Columns.Add("hourly_rate", typeof(System.String));
            dtSource.Columns.Add("rate_basis", typeof(System.String));
            dtSource.Columns.Add("emp_rcrd_status", typeof(System.String));
            dtSource.Columns.Add("period_from", typeof(System.String));
            dtSource.Columns.Add("period_to", typeof(System.String));
            dtSource.Columns.Add("payroll_group_nbr", typeof(System.String));
            //dtSource.Columns.Add("with_cna", typeof(System.String));
            dtSource.Columns.Add("effective_date", typeof(System.String));

            // BEGIN  - Update : JADE  05/20/2019 -- Add Fields by Sir
            dtSource.Columns.Add("department_code", typeof(System.String));
            dtSource.Columns.Add("subdepartment_code", typeof(System.String));
            dtSource.Columns.Add("division_code", typeof(System.String));
            dtSource.Columns.Add("section_code", typeof(System.String));
            dtSource.Columns.Add("function_code", typeof(System.String));
            dtSource.Columns.Add("fund_code", typeof(System.String));
            // END  - Update : JADE  05/20/2019 -- Add Fields by Sir

            // BEGIN - Update : JADE  2021-03-26 -- Add Fields by Sir
            dtSource.Columns.Add("flag_expt_gsis", typeof(System.String));
            dtSource.Columns.Add("flag_expt_hdmf", typeof(System.String));
            dtSource.Columns.Add("flag_expt_phic", typeof(System.String));
            dtSource.Columns.Add("hdmf_fix_rate", typeof(System.String));
            dtSource.Columns.Add("date_of_assumption", typeof(System.String));
            // END   - Update : JADE  2021-03-26 -- Add Fields by Sir
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialized datasource fields/columns - Details
        //*************************************************************************
        private void InitializeTable_dtl()
        {
            dtSource_dtl = new DataTable();
            dtSource_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl.Columns.Add("employment_type", typeof(System.String));
            dtSource_dtl.Columns.Add("account_code", typeof(System.String));
            dtSource_dtl.Columns.Add("account_sub_code", typeof(System.String));
            dtSource_dtl.Columns.Add("effective_date", typeof(System.String));
            dtSource_dtl.Columns.Add("account_amount", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialized datasource fields/columns - to POS Table
        //*************************************************************************
        private void InitializeTable_pos()
        {
            dtSource_pos = new DataTable();
            dtSource_pos.Columns.Add("empl_id", typeof(System.String));
            dtSource_pos.Columns.Add("employment_type", typeof(System.String));
            dtSource_pos.Columns.Add("effective_date", typeof(System.String));
            dtSource_pos.Columns.Add("position_code", typeof(System.String));
            dtSource_pos.Columns.Add("hazard_pay_override", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add Primary Key Field to datasource- Insert Table : payrollemployeemaster_hdr_tbl
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollemployeemaster_hdr_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] {"empl_id", "employment_type", "effective_date"};
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add Primary Key Field to datasource - Insert Table : payrollemployeemaster_dtl_tbl
        //*************************************************************************
        private void AddPrimaryKeys_dtl()
        {
            dtSource_dtl.TableName = "payrollemployeemaster_dtl_tbl";
            dtSource_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "employment_type", "account_code", "account_sub_code", "effective_date" };
            dtSource_dtl = MyCmn.AddPrimaryKeys(dtSource_dtl, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add Primary Key Field to datasource - Insert Table : payrollemployeemaster_pos_tbl
        //*************************************************************************
        private void AddPrimaryKeys_pos()
        {
            dtSource_pos.TableName = "payrollemployeemaster_pos_tbl";
            dtSource_pos.Columns.Add("action", typeof(System.Int32));
            dtSource_pos.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "employment_type", "effective_date" };
            dtSource_pos = MyCmn.AddPrimaryKeys(dtSource_pos, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["employment_type"] = string.Empty;
            nrow["monthly_rate"] = string.Empty;
            nrow["daily_rate"] = string.Empty;
            nrow["hourly_rate"] = string.Empty;
            nrow["rate_basis"] = string.Empty;
            nrow["emp_rcrd_status"] = string.Empty;
            nrow["period_from"] = string.Empty;
            nrow["period_to"] = string.Empty;
            nrow["effective_date"] = string.Empty;

            // BEGIN  - Update : JADE  05/20/2019 -- Add Fields by Sir
            nrow["department_code"] = string.Empty;
            nrow["subdepartment_code"] = string.Empty;
            nrow["division_code"] = string.Empty;
            nrow["section_code"] = string.Empty;
            nrow["function_code"] = string.Empty;
            nrow["fund_code"] = string.Empty;
            // END   - Update : JADE  05/20/2019 -- Add Fields by Sir

            // BEGIN  - Update : JADE  05/20/2019 -- Add Fields by Sir
            nrow["flag_expt_gsis"] = string.Empty;
            nrow["flag_expt_hdmf"] = string.Empty;
            nrow["flag_expt_phic"] = string.Empty;
            nrow["hdmf_fix_rate"] = string.Empty;
            nrow["date_of_assumption"] = string.Empty;
            // END   - Update : JADE  05/20/2019 -- Add Fields by Sir
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        private void AddNewRow_dtl()
        {
            DataRow nrow = dtSource_dtl.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["employment_type"] = string.Empty;
            nrow["account_code"] = string.Empty;
            nrow["account_sub_code"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["account_amount"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            //dtSource_dtl.Rows.Add(nrow);
        }
        private void AddNewRow_pos()
        {
            DataRow nrow = dtSource_pos.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["employment_type"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["position_code"] = string.Empty;
            nrow["hazard_pay_override"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource_pos.Rows.Add(nrow);
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string editExpression   = "empl_id = '" + svalues[0].Trim() + "' AND effective_date = '" + svalues[1].Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            
            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;

            InitializeTable_pos();
            AddPrimaryKeys_pos();
            AddNewRow_pos();
            dtSource_pos.Rows[0]["action"] = 2;
            dtSource_pos.Rows[0]["retrieve"] = true;

            // InitializeTable_dtl();
            // AddPrimaryKeys_dtl();
            ///Add
            ///dtSource_dtl.Rows[0]["action"] = 2;
            ///dtSource_dtl.Rows[0]["retrieve"] = true;

            txtb_empl_id.Text                   = row2Edit[0]["empl_id"].ToString();
            txtb_empl_name.Text                 = row2Edit[0]["employee_name"].ToString();
            txtb_effective_date.Text            = row2Edit[0]["effective_date"].ToString();
            
            txtb_period_from.Text               = row2Edit[0]["period_from"].ToString();
            txtb_period_to.Text                 = row2Edit[0]["period_to"].ToString();
            txtb_monthly_rate.Text              = row2Edit[0]["monthly_rate"].ToString();
            txtb_daily_rate.Text                = row2Edit[0]["daily_rate"].ToString();
            txtb_hourly_rate.Text               = row2Edit[0]["hourly_rate"].ToString();
            ddl_emp_status.SelectedValue        = row2Edit[0]["emp_rcrd_status"].ToString();
            ddl_dep.SelectedValue               = row2Edit[0]["department_code"].ToString();
            ddl_subdep.SelectedValue            = row2Edit[0]["subdepartment_code"].ToString();
            RetrieveBindingDivision();
            ddl_division.SelectedValue          = row2Edit[0]["division_code"].ToString();
            RetrieveBindingSection();
            ddl_section.SelectedValue           = row2Edit[0]["section_code"].ToString();
            ddl_fund_charges.SelectedValue      = row2Edit[0]["fund_code"].ToString();
            ddl_function_code.SelectedValue     = row2Edit[0]["function_code"].ToString();
            txtb_rate_basis_hidden.Text         = row2Edit[0]["rate_basis"].ToString();

            if (ddl_emp_status.SelectedValue == "0")
            {
                ddl_group_nbr.Enabled = false;
            }
            else
            {
                ddl_group_nbr.Enabled = true;

            }

            RetrieveBindingGroupings();
            try
            {
                ddl_group_nbr.SelectedValue        = row2Edit[0]["payroll_group_nbr"].ToString();
                ViewState["payroll_group_nbr"]      = row2Edit[0]["payroll_group_nbr"].ToString();

            }catch(Exception)
            {
                ddl_group_nbr.SelectedValue = "";
                ViewState["payroll_group_nbr"] = row2Edit[0]["payroll_group_nbr"].ToString();
            }

            if (row2Edit[0]["flag_expt_gsis"].ToString() == "1" || row2Edit[0]["flag_expt_gsis"].ToString() == "True")
            {
                chckbox_flag_gsis.Checked = true;
            }
            if (row2Edit[0]["flag_expt_hdmf"].ToString() == "1" || row2Edit[0]["flag_expt_hdmf"].ToString() == "True")
            {
                chckbox_flag_hdmf.Checked = true;
            }
            if (row2Edit[0]["flag_expt_phic"].ToString() == "1" || row2Edit[0]["flag_expt_phic"].ToString() == "True")
            {
                chckbox_flag_phic.Checked = true;
            }
            txtb_hdmf_fix_rate.Text = row2Edit[0]["hdmf_fix_rate"].ToString();

            txtb_date_of_assumption.Text = row2Edit[0]["date_of_assumption"].ToString();
            txtb_salary_grade.Text       = row2Edit[0]["salary_grade"].ToString();
            txtb_birth_date.Text         = row2Edit[0]["birth_date"].ToString();
            
            Retrieve_FromMaster();
            RetrieveDataListGrid_dtl();
            RetriveDeductionLedger();
            RetrieveDataListGrid_empl_history();
            
            LabelAddEdit.Text = "Edit Record: " + ddl_empl_name.SelectedValue.ToString().Trim();

            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;
            ViewState["old_group_nbr"] = "";

            ViewState["prev_chckbox_flag_gsis"]   = chckbox_flag_gsis.Checked;
            ViewState["prev_chckbox_flag_phic"]   = chckbox_flag_phic.Checked;
            ViewState["prev_chckbox_flag_hdmf"]   = chckbox_flag_hdmf.Checked;
            ViewState["prev_txtb_hdmf_fix_rate"]  = txtb_hdmf_fix_rate.Text;
            
            txtb_effective_date.Enabled = false;
            ddl_empl_name.Visible = false;
            txtb_empl_name.Visible = true;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
            SearchData(txtb_search.Text.ToString().Trim());
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
        //  BEGIN - VJA- 01/17/2019 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;
            string scriptInsertUpdate_pos = string.Empty;

            if (IsDataValidated())
            {
                if (saveRecord == MyCmn.CONST_ADD)
                {
                    // BEGIN : Save from Header Table - payrollemployeemaster_hdr_tbl
                    dtSource.Rows[0]["empl_id"]            = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["employment_type"]    = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["effective_date"]     = txtb_effective_date.Text.ToString();
                    dtSource.Rows[0]["monthly_rate"]       = txtb_monthly_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]         = txtb_daily_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["hourly_rate"]        = txtb_hourly_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]         = txtb_rate_basis_hidden.Text.ToString().Trim();
                    dtSource.Rows[0]["period_from"]        = txtb_period_from.Text.ToString().Trim();
                    dtSource.Rows[0]["period_to"]          = txtb_period_to.Text.ToString().Trim();
                    dtSource.Rows[0]["payroll_group_nbr"]  = ddl_group_nbr.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["emp_rcrd_status"]    = ddl_emp_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["department_code"]    = ddl_dep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["subdepartment_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["division_code"]      = ddl_division.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["section_code"]       = ddl_section.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["function_code"]      = ddl_function_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["fund_code"]          = ddl_fund_charges.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["flag_expt_gsis"]     = chckbox_flag_gsis.Checked == true ? "1" : "0";
                    dtSource.Rows[0]["flag_expt_hdmf"]     = chckbox_flag_hdmf.Checked == true ? "1" : "0";
                    dtSource.Rows[0]["flag_expt_phic"]     = chckbox_flag_phic.Checked == true ? "1" : "0";
                    dtSource.Rows[0]["hdmf_fix_rate"]      = txtb_hdmf_fix_rate.Text;
                    dtSource.Rows[0]["date_of_assumption"] = txtb_date_of_assumption.Text;
                   
                    // BEGIN : Save from Header Table - payrollemployeemaster_hdr_tbl

                    // BEGIN -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl
                    dtSource_pos.Rows[0]["empl_id"]             = txtb_empl_id.Text.ToString().Trim();
                    dtSource_pos.Rows[0]["employment_type"]     = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource_pos.Rows[0]["effective_date"]      = txtb_effective_date.Text.ToString();
                    dtSource_pos.Rows[0]["position_code"]       = ddl_position.SelectedValue.ToString();        
                    dtSource_pos.Rows[0]["hazard_pay_override"] = txtb_hazard_pay_override_hidden.Text.ToString().Trim();
                    // END  -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                    scriptInsertUpdate_pos = MyCmn.get_insertscript(dtSource_pos);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    // BEGIN : Save from Header Table - payrollemployeemaster_hdr_tbl
                    dtSource.Rows[0]["empl_id"]            = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["employment_type"]    = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["effective_date"]     = txtb_effective_date.Text.ToString();
                    dtSource.Rows[0]["monthly_rate"]       = txtb_monthly_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]         = txtb_daily_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["hourly_rate"]        = txtb_hourly_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]         = txtb_rate_basis_hidden.Text.ToString().Trim();
                    dtSource.Rows[0]["period_from"]        = txtb_period_from.Text.ToString().Trim();
                    dtSource.Rows[0]["period_to"]          = txtb_period_to.Text.ToString().Trim();
                    dtSource.Rows[0]["payroll_group_nbr"]  = ddl_group_nbr.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["emp_rcrd_status"]    = ddl_emp_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["department_code"]    = ddl_dep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["subdepartment_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["division_code"]      = ddl_division.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["section_code"]       = ddl_section.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["function_code"]      = ddl_function_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["fund_code"]          = ddl_fund_charges.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["flag_expt_gsis"]     = chckbox_flag_gsis.Checked == true ? "1" : "0";
                    dtSource.Rows[0]["flag_expt_hdmf"]     = chckbox_flag_hdmf.Checked == true ? "1" : "0";
                    dtSource.Rows[0]["flag_expt_phic"]     = chckbox_flag_phic.Checked == true ? "1" : "0";
                    dtSource.Rows[0]["hdmf_fix_rate"]      = txtb_hdmf_fix_rate.Text;
                    dtSource.Rows[0]["date_of_assumption"] = txtb_date_of_assumption.Text;
                    // BEGIN : Save from Header Table - payrollemployeemaster_hdr_tbl
                    
                    // BEGIN -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl
                    dtSource_pos.Rows[0]["empl_id"]             = txtb_empl_id.Text.ToString().Trim();
                    dtSource_pos.Rows[0]["employment_type"]     = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource_pos.Rows[0]["effective_date"]      = txtb_effective_date.Text.ToString();
                    dtSource_pos.Rows[0]["position_code"]       = ddl_position.SelectedValue.ToString();        
                    dtSource_pos.Rows[0]["hazard_pay_override"] = txtb_hazard_pay_override_hidden.Text.ToString().Trim();
                    // END  -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                    scriptInsertUpdate_pos = MyCmn.updatescript(dtSource_pos);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    //************************************************************************************************
                    //  BEGIN - VJA - 10/19/2019 - Update Previous Grouping of the Specific Employee during Add Mode
                    //************************************************************************************************

                    if (ddl_emp_status.SelectedValue == "0" || ViewState["payroll_group_nbr"].ToString() != ddl_group_nbr.SelectedValue.ToString().Trim())
                    {
                        DataTable dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_dtl_tbl_upd", "par_payroll_group_nbr", ddl_group_nbr.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_special_group", "01");
                    }
                    //************************************************************************************************
                    //  END - VJA - 10/19/2019 - Update Previous Grouping of the Specific Employee during Add Mode
                    //************************************************************************************************

                    // BEGIN : Date : 10/30/2019 - In-Active Grouping Across Special Groups
                    if (ddl_group_nbr.SelectedValue == "" || ddl_emp_status.SelectedValue == "0" )
                    {
                        string table_update = "payrollemployeegroupings_dtl_tbl";
                        string set_update   = "emp_status = '0' ";
                        string where_update = "WHERE empl_id = '"+ txtb_empl_id.Text.ToString().Trim() +"'";
                        MyCmn.UpdateTable(table_update, set_update, where_update);
                    }
                    // END  : Date : 10/30/2019 - In-Active Grouping Across Special Groups

                    // Insert From Database : Table Name : payrollemployeemaster_pos_tbl
                    if (scriptInsertUpdate_pos == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate_pos);
                    if (msg == "") return;
                    
                    if (scriptInsertUpdate == string.Empty) return;
                    string msg1 = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg1 == "") return;

                    SaveRemarks("");

                    if (msg1.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist");
                        return;
                    }

                    InitializeTable_dtl();
                    AddPrimaryKeys_dtl();
                    foreach (GridViewRow nrow in gv_details.Rows)
                    {
                        Label gv_lbl_account_code       = nrow.FindControl("gv_lbl_account_code") as Label;
                        Label gv_lbl_account_sub_code   = nrow.FindControl("gv_lbl_account_sub_code") as Label;
                        
                        DataRow nrow1               = dtSource_dtl.NewRow();
                        nrow1["empl_id"]            = txtb_empl_id.Text.ToString().Trim();
                        nrow1["employment_type"]    = ddl_empl_type.SelectedValue.ToString().Trim();
                        nrow1["account_code"]       = gv_lbl_account_code.Text;
                        nrow1["account_sub_code"]   = gv_lbl_account_sub_code.Text;
                        nrow1["effective_date"]     = txtb_effective_date.Text.ToString();
                        nrow1["action"] = 1;
                        nrow1["retrieve"] = false;
                        dtSource_dtl.Rows.Add(nrow1);
                    }

                    int error = 0;
                    foreach (GridViewRow row in gv_details.Rows)
                    {
                        TextBox txtb_account_amount = row.FindControl("txtb_account_amount") as TextBox;

                        dtSource_dtl.Rows[row.RowIndex]["account_amount"] = txtb_account_amount.Text.ToString().Trim();
                        

                        if (CommonCode.checkisdecimal(txtb_account_amount) == false)
                        {
                            txtb_account_amount.BorderColor = Color.Red;
                            txtb_account_amount.BorderWidth = 1;
                            FieldValidationColorChanged(true, "invalid-numeric");
                            error = error + 1;
                        }
                        else
                        {
                            if (Convert.ToDouble(txtb_account_amount.Text) <= 0)
                            {
                                dtSource_dtl.Rows[row.RowIndex]["action"] = 0;
                            }
                            else {
                                dtSource_dtl.Rows[row.RowIndex]["account_amount"] = txtb_account_amount.Text.ToString().Trim();
                                txtb_account_amount.BorderColor = Color.Gray;
                                txtb_account_amount.BorderWidth = 1;
                            }
                        }
                    }

                    if (error > 0)
                    {
                        update_details.Update();
                        return;
                    }
                    
                    string[] insert_empl_script = MyCmn.get_insertscript(dtSource_dtl).Split(';');
                    MyCmn.DeleteBackEndData(dtSource_dtl.TableName.ToString(), "WHERE empl_id ='" + txtb_empl_id.Text.ToString().Trim() + "' AND  employment_type ='" + ddl_empl_type.SelectedValue.ToString().Trim() + "' AND  effective_date ='" + txtb_effective_date.Text.ToString().Trim() + "'");
                    for (int x = 0; x < insert_empl_script.Length; x++)
                    {
                        string insert_script = "";
                        insert_script = insert_empl_script[x];
                        MyCmn.insertdata(insert_script);
                    }
                    
                    

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        // BEGIN : Save from Header Table - payrollemployeemaster_hdr_tbl
                        nrow["empl_id"]            = txtb_empl_id.Text.ToString().Trim();
                        nrow["employment_type"]    = ddl_empl_type.SelectedValue.ToString().Trim();
                        nrow["effective_date"]     = txtb_effective_date.Text.ToString();
                        nrow["monthly_rate"]       = txtb_monthly_rate.Text.ToString().Trim();
                        nrow["daily_rate"]         = txtb_daily_rate.Text.ToString().Trim();
                        nrow["hourly_rate"]        = txtb_hourly_rate.Text.ToString().Trim();
                        nrow["rate_basis"]         = txtb_rate_basis_hidden.Text.ToString().Trim();
                        nrow["period_from"]        = txtb_period_from.Text.ToString().Trim();
                        nrow["period_to"]          = txtb_period_to.Text.ToString().Trim();
                        nrow["payroll_group_nbr"]  = ddl_group_nbr.SelectedValue.ToString().Trim();
                        nrow["emp_rcrd_status"]    = ddl_emp_status.SelectedValue.ToString().Trim();
                        nrow["department_code"]    = ddl_dep.SelectedValue.ToString().Trim();
                        nrow["subdepartment_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                        nrow["division_code"]      = ddl_division.SelectedValue.ToString().Trim();
                        nrow["section_code"]       = ddl_section.SelectedValue.ToString().Trim();
                        nrow["function_code"]      = ddl_function_code.SelectedValue.ToString().Trim();
                        nrow["fund_code"]          = ddl_fund_charges.SelectedValue.ToString().Trim();
                        nrow["flag_expt_gsis"]     = chckbox_flag_gsis.Checked == true ? "1" : "0";
                        nrow["flag_expt_hdmf"]     = chckbox_flag_hdmf.Checked == true ? "1" : "0";
                        nrow["flag_expt_phic"]     = chckbox_flag_phic.Checked == true ? "1" : "0";
                        nrow["hdmf_fix_rate"]      = txtb_hdmf_fix_rate.Text;
                        nrow["date_of_assumption"]   = txtb_date_of_assumption.Text;
                        // BEGIN : Save from Header Table - payrollemployeemaster_hdr_tbl
                    
                        // BEGIN -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl
                        nrow["empl_id"]             = ddl_empl_name.SelectedValue.ToString().Trim();
                        nrow["employment_type"]     = ddl_empl_type.SelectedValue.ToString().Trim();
                        nrow["effective_date"]      = txtb_effective_date.Text.ToString();
                        nrow["employee_name"]       = ddl_empl_name.SelectedItem.ToString().Trim();
                        //nrow["position_code"]       = ddl_position.SelectedValue.ToString();
                        //nrow["hazard_pay_override"] = txtb_hazard_pay_override_hidden.Text.ToString().Trim();
                        // END  -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        h2_status.InnerText = "SUCCESSFULLY ADDED!";
                        i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND employment_type = '" + ddl_empl_type.SelectedValue.ToString().Trim() + "' AND effective_date = '" + txtb_effective_date.Text.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        
                        // BEGIN : Save from Header Table - payrollemployeemaster_hdr_tbl
                        row2Edit[0]["empl_id"]            = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employment_type"]    = ddl_empl_type.SelectedValue.ToString().Trim();
                        row2Edit[0]["effective_date"]     = txtb_effective_date.Text.ToString();
                        row2Edit[0]["monthly_rate"]       = txtb_monthly_rate.Text.ToString().Trim();
                        row2Edit[0]["daily_rate"]         = txtb_daily_rate.Text.ToString().Trim();
                        row2Edit[0]["hourly_rate"]        = txtb_hourly_rate.Text.ToString().Trim();
                        row2Edit[0]["rate_basis"]         = txtb_rate_basis_hidden.Text.ToString().Trim();
                        row2Edit[0]["period_from"]        = txtb_period_from.Text.ToString().Trim();
                        row2Edit[0]["period_to"]          = txtb_period_to.Text.ToString().Trim();
                        row2Edit[0]["payroll_group_nbr"]  = ddl_group_nbr.SelectedValue.ToString().Trim();
                        row2Edit[0]["emp_rcrd_status"]    = ddl_emp_status.SelectedValue.ToString().Trim();
                        row2Edit[0]["department_code"]    = ddl_dep.SelectedValue.ToString().Trim();
                        row2Edit[0]["subdepartment_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                        row2Edit[0]["division_code"]      = ddl_division.SelectedValue.ToString().Trim();
                        row2Edit[0]["section_code"]       = ddl_section.SelectedValue.ToString().Trim();
                        row2Edit[0]["function_code"]      = ddl_function_code.SelectedValue.ToString().Trim();
                        row2Edit[0]["fund_code"]          = ddl_fund_charges.SelectedValue.ToString().Trim();

                        row2Edit[0]["flag_expt_gsis"]   = chckbox_flag_gsis.Checked == true ? "1" : "0";
                        row2Edit[0]["flag_expt_hdmf"]   = chckbox_flag_hdmf.Checked == true ? "1" : "0";
                        row2Edit[0]["flag_expt_phic"]   = chckbox_flag_phic.Checked == true ? "1" : "0";
                        row2Edit[0]["hdmf_fix_rate"]    = txtb_hdmf_fix_rate.Text;
                        row2Edit[0]["date_of_assumption"]    = txtb_date_of_assumption.Text;
                        // BEGIN : Save from Header Table - payrollemployeemaster_hdr_tbl

                        // BEGIN -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl
                        row2Edit[0]["empl_id"]             = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employment_type"]     = ddl_empl_type.SelectedValue.ToString().Trim();
                        row2Edit[0]["effective_date"]      = txtb_effective_date.Text.ToString();
                        //row2Edit[0]["position_code"]       = ddl_position.SelectedValue.ToString();
                        //row2Edit[0]["hazard_pay_override"] = txtb_hazard_pay_override_hidden.Text.ToString().Trim();
                        // END  -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl
                        
                        row2Edit[0]["employee_name"]        = txtb_empl_name.Text.ToString().Trim();

                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text    = MyCmn.CONST_EDITREC;
                        h2_status.InnerText = "SUCCESSFULLY UPDATED!";
                        i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                    }
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                    SearchData(txtb_search.Text.ToString().Trim());
                }
               
            }
        }

        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            SearchData(txtb_search.Text.ToString().Trim());
        }

        //**********************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            SearchData(txtb_search.Text.ToString().Trim());
        }

        //**********************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtb_search.Text.ToString().Trim());
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
            
            if (ddl_empl_name.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() ==  MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_name");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_monthly_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_monthly_rate");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_daily_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_daily_rate");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_hourly_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_hourly_rate");
                validatedSaved = false;
            }
            if (txtb_period_from.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_period_from");
                validatedSaved = false;
            }
            if (txtb_period_to.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_period_to");
                validatedSaved = false;
            }
            if (ddl_position.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_position");
                validatedSaved = false;
            }

            if (CommonCode.checkisdatetime(txtb_period_from) == false)
            {
                FieldValidationColorChanged(true, "invalid-date-1");
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(txtb_period_to) == false)
            {
                FieldValidationColorChanged(true, "invalid-date-2");
                validatedSaved = false;
            }
            if (ddl_position.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_position");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_fix_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_fix_rate");
                validatedSaved = false;
            }
            else
            {
                if (chckbox_flag_hdmf.Checked == true && (double.Parse(txtb_hdmf_fix_rate.Text) > 0  && double.Parse(txtb_hdmf_fix_rate.Text) < 200 ))
                {
                    LblRequired300.Text = "Input zero (0) for HDMF exemption or greater than 200!";
                    txtb_hdmf_fix_rate.BorderColor = Color.Red;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                    txtb_hdmf_fix_rate.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                    txtb_hdmf_fix_rate.Focus();
                    validatedSaved = false;
                }
                if (chckbox_flag_hdmf.Checked == false && double.Parse(txtb_hdmf_fix_rate.Text) > 0)
                {
                    LblRequired300.Text = "Please check the hdmf!";
                    txtb_hdmf_fix_rate.BorderColor = Color.Red;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                    txtb_hdmf_fix_rate.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                    txtb_hdmf_fix_rate.Focus();
                    validatedSaved = false;
                }
                //if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_EDIT)
                //{
                //    if (Boolean.Parse(ViewState["prev_chckbox_flag_gsis"].ToString())   != chckbox_flag_gsis.Checked
                //     || Boolean.Parse(ViewState["prev_chckbox_flag_phic"].ToString())   != chckbox_flag_phic.Checked
                //     || Boolean.Parse(ViewState["prev_chckbox_flag_hdmf"].ToString())   != chckbox_flag_hdmf.Checked
                //     || double.Parse(ViewState["prev_txtb_hdmf_fix_rate"].ToString())   != double.Parse(txtb_hdmf_fix_rate.Text)
                //    )
                //    {
                //        SaveAddEdit.Text = "Please Input Remarks to continue!";
                //        h2_status.InnerText = "YOU CANNOT CONTINUE!";
                //        i_icon_display.Attributes.Add("class", "fa-5x fa fa-times-circle text-danger");
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popopen_Dynamic_Notification", "open_Dynamic_Notification();", true);
                //        validatedSaved = false;
                //    }
                //}
            }
            // if (CommonCode.checkisdatetime(txtb_date_of_assumption) == false)
            // {
            //     FieldValidationColorChanged(true, "invalid-date-3");
            //     validatedSaved = false;
            // }
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

                    case "atleast-one":
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "openNotification();", true);
                            lbl_notification.Text = "The Table must be atleast one Detail!";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "ddl_empl_name":
                        {
                            LblRequired12.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_name.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired12.Text = "Already Exist";
                            ddl_empl_name.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "txtb_period_from":
                        {
                            LblRequired14.Text = MyCmn.CONST_RQDFLD;
                            txtb_period_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            UpdatePanelFrom.Update();
                            break;
                        }
                    case "txtb_period_to":
                        {
                            LblRequired15.Text = MyCmn.CONST_RQDFLD;
                            txtb_period_to.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            UpdatePanelTo.Update();
                            break;
                        }
                    case "invalid-date-1":
                        {
                            LblRequired14.Text = "Invalid Date!";
                            txtb_period_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            UpdatePanelFrom.Update();
                            break;
                        }
                    case "invalid-date-2":
                        {
                            LblRequired15.Text = "Invalid Date!";
                            txtb_period_to.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            UpdatePanelTo.Update();
                            break;
                        }
                    case "invalid-date-3":
                        {
                            LblRequired1000.Text = "Invalid Date!";
                            txtb_date_of_assumption.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            UpdatePanel34.Update();
                            break;
                        }
                    case "txtb_monthly_rate":
                        {
                            LblRequired20.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_monthly_rate.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "txtb_daily_rate":
                        {
                            LblRequired21.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_daily_rate.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "txtb_hourly_rate":
                        {
                            LblRequired22.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hourly_rate.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "ddl_position":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            ddl_position.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "txtb_hdmf_fix_rate":
                        {
                            LblRequired300.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_fix_rate.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                        
                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired12.Text = "";
                            LblRequired14.Text = "";
                            LblRequired15.Text = "";
                            LblRequired20.Text = "";
                            LblRequired21.Text = "";
                            LblRequired22.Text = "";
                            LblRequired6.Text = "";
                            LblRequired300.Text = "";
                            LblRequired1000.Text = "";
                            ddl_empl_name.BorderColor = Color.LightGray;
                            txtb_period_from.BorderColor = Color.LightGray;
                            txtb_period_to.BorderColor = Color.LightGray;
                            txtb_effective_date.BorderColor = Color.LightGray;
                            txtb_monthly_rate.BorderColor = Color.LightGray;
                            txtb_daily_rate.BorderColor = Color.LightGray;
                            txtb_hourly_rate.BorderColor = Color.LightGray;
                            ddl_position.BorderColor = Color.LightGray;
                            txtb_hdmf_fix_rate.BorderColor = Color.LightGray;
                            txtb_date_of_assumption.BorderColor = Color.LightGray;

                            UpdatePanelFrom.Update();
                            UpdatePanelTo.Update();
                            UpdatePanelEffec.Update();
                            UpdatePanel34.Update();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }

                }
            }
        }
        protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_department.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "" )
            {
                RetrieveEmpl();
                chkIncludeHistory.Enabled = true;
                btnAdd.Visible = true;
            }
            else
            {
                chkIncludeHistory.Enabled = false;
                btnAdd.Visible = false;
            }
            RetrieveDataListGrid();
            UpdatePanel10.Update();
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //*************************************************************************
        //  BEGIN - JADE- 05/21/2019 - 
        //*************************************************************************
        protected void ddl_empl_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_name.SelectedValue.ToString() != "")
            {
                string editExpression = "empl_id = '" + ddl_empl_name.SelectedValue.ToString().Trim() + "'";
                DataRow[] row2Edit2 = dtSource_for_names.Select(editExpression);
                
                txtb_rate_basis_hidden.Text     = row2Edit2[0]["rate_basis"].ToString().Trim();
                txtb_monthly_rate.Text          = row2Edit2[0]["monthly_rate"].ToString();
                txtb_daily_rate.Text            = row2Edit2[0]["daily_rate"].ToString();
                txtb_hourly_rate.Text           = row2Edit2[0]["hourly_rate"].ToString();
                txtb_empl_id.Text               = row2Edit2[0]["empl_id"].ToString().Trim();
                
                Retrieve_FromMaster();
                RetrieveDataListGrid_dtl();
                

            }
            else
            {
                ClearEntry();
            }
            RetriveDeductionLedger();
            RetrieveDataListGrid_empl_history();
        }
        
        //*************************************************************************
        //  BEGIN - JADE- 05/21/2019 - Retrieve back end data from sp_payrollemployeemaster_tbl_empl
        //*************************************************************************
        private void Retrieve_FromMaster()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_empl", "par_employment_type", ddl_empl_type.SelectedValue.ToString(), "par_department_code", ddl_department.SelectedValue.ToString(), "par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_effective_date", txtb_effective_date.Text.ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                RetrievePosition();
                ddl_position.SelectedValue              = dt.Rows[0]["position_code"].ToString().Trim();
                txtb_hazard_pay_override_hidden.Text    = dt.Rows[0]["hazard_pay_override"].ToString();

                FieldValidationColorChanged(false, "ALL");
            }
        }
        //***********************************************
        //  BEGIN - JADE- 05/21/2019 - Include History
        //***********************************************
        protected void chkIncludeHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncludeHistory.Checked)
            {
                ViewState["chkIncludeHistory"] = "Y";
            }
            else
            {
                ViewState["chkIncludeHistory"] = "N";
            }
            RetrieveDataListGrid();
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**********************************************************************************************
        //  UPDATE - JADE - 05/20/2019 - Add Field Department, Sub-department, Division, Section, Function Code
        //                               and Fund Charges
        //**********************************************************************************************
        private void RetrieveBindingSubDep()
        {
            ddl_subdep.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_subdepartments_tbl_list");

            ddl_subdep.DataSource = dt;
            ddl_subdep.DataValueField = "subdepartment_code";
            ddl_subdep.DataTextField = "subdepartment_short_name";
            ddl_subdep.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_subdep.Items.Insert(0, li);
        }
        //***************************************************
        //  BEGIN - JADE- 05/21/2019 -Retrieve Division
        //***************************************************
        private void RetrieveBindingDivision()
        {
            ddl_division.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_divisions_tbl_combolist", "par_department_code", ddl_dep.SelectedValue.ToString(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString());

            ddl_division.DataSource = dt;
            ddl_division.DataValueField = "division_code";
            ddl_division.DataTextField = "division_name1";
            ddl_division.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_division.Items.Insert(0, li);
        }
        //***************************************************
        //  BEGIN - JADE- 05/21/2019 -Retrieve Section
        //***************************************************
        private void RetrieveBindingSection()
        {
            ddl_section.Items.Clear();
            // ddl_section.ClearSelection();
            DataTable dt1 = MyCmn.RetrieveData("sp_sections_tbl_combolist", "par_department_code", ddl_dep.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString(), "par_division_code", ddl_division.SelectedValue.ToString().Trim());

            ddl_section.DataSource = dt1;
            ddl_section.DataValueField = "section_code";
            ddl_section.DataTextField = "section_name1";
            ddl_section.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_section.Items.Insert(0, li);
        }
        private void RetrieveBindingFundcharges()
        {
            ddl_fund_charges.Items.Clear();
            // ddl_section.ClearSelection();
            DataTable dt1 = MyCmn.RetrieveData("sp_fundcharges_tbl_list");

            ddl_fund_charges.DataSource = dt1;
            ddl_fund_charges.DataValueField = "fund_code";
            ddl_fund_charges.DataTextField = "fund_description";
            ddl_fund_charges.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_fund_charges.Items.Insert(0, li);
        }
        private void RetrieveBindingFunction()
        {
            ddl_function_code.Items.Clear();
            // ddl_section.ClearSelection();
            DataTable dt1 = MyCmn.RetrieveData("sp_functions_tbl_list");

            ddl_function_code.DataSource = dt1;
            ddl_function_code.DataValueField = "function_code";
            ddl_function_code.DataTextField = "function_name";
            ddl_function_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_function_code.Items.Insert(0, li);
        }
        //*******************************************************
        //  BEGIN - JADE- 05/21/2019 - Select Department on Modal
        //********************************************************
        protected void ddl_dep_SelectedIndexChanged(object sender, EventArgs e)

        {
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveBindingGroupings();
          
        }
        //*******************************************************
        //  BEGIN - JADE- 05/21/2019 - Select Sub-Department on Modal
        //********************************************************
        protected void ddl_subdep_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingDivision();
            RetrieveBindingSection();
            ddl_subdep.Focus();
        }
        //*******************************************************
        //  BEGIN - JADE- 05/21/2019 - Select Division on Modal
        //********************************************************
        protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingSection();
            ddl_division.Focus();
        }
        //*************************************************************************
        //  BEGIN - VJA- 07//25/2019 - Populate Combo list for Position Title
        //*************************************************************************
        private void RetrievePosition()
        {
            ddl_position.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_positions_tbl_list_paymaster_per_empl", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim());

            ddl_position.DataSource = dt;
            ddl_position.DataValueField = "position_code";
            ddl_position.DataTextField = "position_long_title";
            ddl_position.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_position.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 10/23/2019 - To In-Active or ctive Employees
        //*************************************************************************
        protected void ddl_emp_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_emp_status.SelectedValue == "1")
            {
                ddl_group_nbr.SelectedValue = ViewState["old_group_nbr"].ToString();
                ddl_group_nbr.Enabled = true;
            }
            else
            {
                ViewState["old_group_nbr"] = ddl_group_nbr.SelectedValue.ToString();
                ddl_group_nbr.SelectedValue = "";
                ddl_group_nbr.Enabled = false;
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 10/23/2019 - To In-Active or ctive Employees
        //*************************************************************************
        protected void img_edit_history_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string editExpression = "empl_id = '" + svalues[0].Trim() + "' AND effective_date = '" + svalues[1].Trim() + "'";
            DataRow[] row2Edit = dtSource_History.Select(editExpression);

            txtb_history_effective.Text     = "";
            txtb_history_monthly.Text       = "";
            txtb_history_daily.Text         = "";
            txtb_history_hourly.Text        = "";
            txtb_history_employment.Text    = "";
            txtb_history_department.Text    = "";
            txtb_history_subdepartment.Text = "";
            txtb_history_division.Text      = "";
            txtb_history_section.Text       = "";
            txtb_history_function.Text      = "";
            txtb_history_fund.Text          = "";
            txtb_history_status.Text        = "";
            txtb_history_hzrd_perc.Text     = "";
            txtb_history_position.Text      = "";
            txtb_history_groupings.Text     = "";
            
            txtb_history_effective.Text     = row2Edit[0]["effective_date"].ToString();
            txtb_history_monthly.Text       = row2Edit[0]["monthly_rate"].ToString();
            txtb_history_daily.Text         = row2Edit[0]["daily_rate"].ToString();
            txtb_history_hourly.Text        = row2Edit[0]["hourly_rate"].ToString();
            txtb_history_employment.Text    = row2Edit[0]["employmenttype_description"].ToString();
            txtb_history_department.Text    = row2Edit[0]["department_name1"].ToString();
            txtb_history_subdepartment.Text = row2Edit[0]["subdepartment_name1"].ToString();
            txtb_history_division.Text      = row2Edit[0]["division_name1"].ToString();
            txtb_history_section.Text       = row2Edit[0]["section_name1"].ToString();
            txtb_history_function.Text      = row2Edit[0]["function_name"].ToString();
            txtb_history_fund.Text          = row2Edit[0]["fund_description"].ToString();
            txtb_history_status.Text        = row2Edit[0]["emp_rcrd_status_descr"].ToString();
            txtb_history_hzrd_perc.Text     = row2Edit[0]["hazard_pay_override"].ToString();
            txtb_history_position.Text      = row2Edit[0]["position_long_title"].ToString();
            txtb_history_groupings.Text     = row2Edit[0]["grouping_descr"].ToString();
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "openEmplHistory();", true);
        }


        //***************************************************************************
        //  BEGIN - VJA- 2020-07-28 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id          = commandArgs[0];
            string effective_date   = commandArgs[1];
            string employment_type  = commandArgs[2];
            
            deleteRec1.Text = "Are you sure to delete this Record ?";
            delete_header.InnerText = "Delete this Record";
            lnkBtnYes.Text = " Yes, Delete it ";
            
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-07-28 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND effective_date='" + commandarg[1].Trim() + "' AND employment_type='" + commandarg[2].Trim() + "'";

            DataTable dt = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_delete", "p_employment_type", commandarg[2].Trim(), "p_empl_id", commandarg[0].Trim(), "p_effective_date", commandarg[1].Trim());
            
            if (dt.Rows[0]["result_flag"].ToString() == "Y")
            {
                h2_status.InnerText         = "SUCCESSFULLY DELETED";
                i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
            }
            else
            {
                h2_status.InnerText         = "DATA NOT DELETED";
                i_icon_display.Attributes.Add("class", "fa-5x fa fa-times-circle text-danger");
            }
            SaveAddEdit.Text            =  dt.Rows[0]["result_flag_descr"].ToString();
            
            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseNotification();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-07-28 - Search Data
        //*************************************************************************
        private void SearchData(string search)
        {
            string searchExpression = "empl_id LIKE '%" + search.Trim().Replace("'", "''") + "%' OR employee_name LIKE '%" + search.Trim().Replace("'", "''") + "%' OR monthly_rate LIKE '%" + search.Trim().Replace("'", "''") + "%' OR daily_rate LIKE '%" + search.Trim().Replace("'", "''") + "%' OR hourly_rate LIKE '%" + search.Trim().Replace("'", "''") + "%' OR rate_basis LIKE '%" + search.Trim().Replace("'", "''") + "%' OR rate_basis_descr LIKE '%" + search.Trim().Replace("'", "''") + "%' OR employment_type LIKE '%" + search.Trim().Replace("'", "''") + "%' OR effective_date LIKE '%" + search.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("daily_rate", typeof(System.String));
            dtSource1.Columns.Add("hourly_rate", typeof(System.String));
            dtSource1.Columns.Add("rate_basis", typeof(System.String));
            dtSource1.Columns.Add("rate_basis_descr", typeof(System.String));
            dtSource1.Columns.Add("employment_type", typeof(System.String));
            dtSource1.Columns.Add("period_from", typeof(System.String));
            dtSource1.Columns.Add("period_to", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("department_code", typeof(System.String));
            dtSource1.Columns.Add("subdepartment_code", typeof(System.String));
            dtSource1.Columns.Add("division_code", typeof(System.String));
            dtSource1.Columns.Add("section_code", typeof(System.String));
            dtSource1.Columns.Add("function_code", typeof(System.String));
            dtSource1.Columns.Add("fund_code", typeof(System.String));
            dtSource1.Columns.Add("flag_expt_gsis", typeof(System.String));
            dtSource1.Columns.Add("flag_expt_hdmf", typeof(System.String));
            dtSource1.Columns.Add("flag_expt_phic", typeof(System.String));
            dtSource1.Columns.Add("hdmf_fix_rate", typeof(System.String));
            dtSource1.Columns.Add("emp_rcrd_status", typeof(System.String));

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
        //*************************************************************************
        //  BEGIN - VJA- 2020-07-30 -  Button for ReProcess to Plantilla - Open Modal
        //*************************************************************************
        protected void btn_reprocess_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReprocess", "openReprocess();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-07-30 -  Execution Button for ReProcess to Plantilla
        //*************************************************************************
        protected void lnkbtn_reprocess_Click(object sender, EventArgs e)
        {
            //string effe_year    = DateTime.Parse(txtb_effective_date.Text).Year.ToString();
            //string effe_month  = DateTime.Parse(txtb_effective_date.Text).Month.ToString();

            //if (Convert.ToInt32(effe_month) < 10)
            //{
            //    effe_month = "0" + effe_month;
            //}

            string effe_year        = ddl_year.SelectedValue.ToString().Trim();
            string effe_month       = ddl_month.SelectedValue.ToString().Trim();
            // string effe_for_delete  = ddl_year.SelectedValue.ToString().Trim() + "-" + ddl_month.SelectedValue.ToString().Trim() + "-01";
            string effe_for_delete = txtb_effective_date.Text.ToString().Trim();


            if (ddl_empl_type.SelectedValue == "RE")
            {
                // This Stored Procedure is for Delete Current Employee Master Header, Details, Position and Deduction Ledger (Active) 
                DataTable dt = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_delete", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_empl_id", txtb_empl_id.Text.ToString().Trim(), "p_effective_date", effe_for_delete.ToString().Trim());
                
                // This Stored Procedure is for Insert for Master Header, Details and Position
                dtSource_reProcess = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_insert_RE_empl_id", "p_year", effe_year, "p_month", effe_month, "p_user_id", Session["ep_user_id"].ToString(), "p_empl_id", txtb_empl_id.Text.ToString().Trim());
            }
            else if (ddl_empl_type.SelectedValue == "CE" || ddl_empl_type.SelectedValue == "JO")
            {
                // This Stored Procedure is for Delete Current Employee Master Header, Details, Position and Deduction Ledger (Active) 
                DataTable dt = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_delete", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_empl_id", txtb_empl_id.Text.ToString().Trim(), "p_effective_date", effe_for_delete.ToString().Trim());

                // This Stored Procedure is for Insert for Master Header, Details and Position
                dtSource_reProcess = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_insert_CEJO_empl_id", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_year", effe_year, "p_month", effe_month, "p_user_id", Session["ep_user_id"].ToString(), "p_empl_id", txtb_empl_id.Text.ToString().Trim());
            }
            try
            {
                switch (dtSource_reProcess.Rows[0]["result_flag"].ToString())
                {
                    case "I":
                    case "N":
                    case "S":
                        h2_status.InnerText = "SUCCESSFULLY REPROCESSED!";
                        i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                        SaveAddEdit.Text = dtSource_reProcess.Rows[0]["result_flag_message"].ToString();


                        break;
                    default:
                        h2_status.InnerText = "SOMETHING ERROR!";
                        i_icon_display.Attributes.Add("class", "fa-5x fa fa-times-circle text-danger");
                        SaveAddEdit.Text = dtSource_reProcess.Rows[0]["result_flag_message"].ToString();
                        break;
                }
                RetrieveDataListGrid();
                SearchData(txtb_search.Text.ToString().Trim());
                
            }
            catch (Exception a)
            {
                h2_status.InnerText = "SOMETHING ERROR! ";
                i_icon_display.Attributes.Add("class", "fa-5x fa fa-times-circle text-danger");
                SaveAddEdit.Text = "<br /> <small> <b>" + a.Message.ToString() + "</b></ small>";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReprocess_execute", "closeReprocess();", true);

        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveYear()
        {
            ddl_year.Items.Clear();
            ddl_payroll_year.Items.Clear();
            int years = Convert.ToInt32(DateTime.Now.Year);
            int prev_year = years - 5;
            for (int x = 0; x < 12; x++)
            {
                ListItem li2 = new ListItem(prev_year.ToString(), prev_year.ToString());
                ddl_year.Items.Insert(x, li2);
                ddl_payroll_year.Items.Insert(x, li2);
                if (prev_year == years)
                {
                    ListItem li3 = new ListItem((years + 1).ToString(), (years + 1).ToString());
                    ddl_year.Items.Insert(x + 1, li3);
                    ddl_year.SelectedValue = years.ToString();
                    ddl_payroll_year.Items.Insert(x + 1, li3);
                    ddl_payroll_year.SelectedValue = years.ToString();
                    break;
                }
                prev_year = prev_year + 1;
            }
        }

        //*************************************************************************
        //  BEGIN - VJA- 2020-09-29 - Deduction Ledger Tab
        //*************************************************************************
        private void RetriveDeductionLedger()
        {
            dtSource_deduction_ledger = MyCmn.RetrieveData("sp_payrollemployeemaster_ledger_list","par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_deduction_list, dtSource_deduction_ledger);
            up_deduction_list.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-10-09 - Deduction Ledger Tab - Audit Table/History
        //*************************************************************************
        protected void img_btn_view_history_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            dtSource_deduction_ledger_audit = MyCmn.RetrieveData("sp_payrollemployeemaster_ledger_aud_list", "par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_deduc_code", svalues[0].ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_deduction_audit_history, dtSource_deduction_ledger_audit);
            up_deduction_audit_history.Update();

            txtb_deduc_descr.Text = "";
            txtb_deduc_code.Text  = "";

            if (dtSource_deduction_ledger_audit.Rows.Count > 0)
            {
                string select_Expression = "deduc_code = '" + svalues[0].Trim() + "'";
                DataRow[] row2Edit = dtSource_deduction_ledger_audit.Select(select_Expression);
            
                txtb_deduc_descr.Text = row2Edit[0]["deduc_descr"].ToString().Trim();
                txtb_deduc_code.Text  = row2Edit[0]["deduc_code"].ToString().Trim();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopLedger_Audit", "openModal_Ledger_Audit();", true);
            }
            else
            {
                SaveAddEdit.Text        = "No Deduction Ledger History!";
                h2_status.InnerText     = "NO DATA FOUND";
                i_icon_display.Attributes.Add("class", "fa-5x fa fa-times-circle text-danger");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popopen_Dynamic_Notification", "open_Dynamic_Notification();", true);
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-11-27 - List of Employee - Coming from Employee Master
        //*************************************************************************
        protected void btn_print_Click(object sender, EventArgs e)
        {
            ToggleReportOptions();
            ddl_department_report.SelectedValue = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "open_SelectReport();", true);
        }
        //**************************************************************************
        //  BEGIN Jade- 09/17/18 - Populate Combo list from Province table
        //*************************************************************************
        private void RetrieveBindingBudgetYear()
        {
            ddl_budget_year.Items.Clear();
            DataTable budgetyear_tbl = MyCmn.RetrieveData("sp_budgetyears_tbl_list2");
            ddl_budget_year.DataSource = budgetyear_tbl;
            ddl_budget_year.DataValueField = "budget_code";
            ddl_budget_year.DataTextField = "budget_description";
            ddl_budget_year.DataBind();
        }
        //**************************************************************************
        //  BEGIN Jade- 09/17/18 - Populate Combo list from Province table
        //*************************************************************************
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string printreport;
            string procedure;
            string url = "";

            Session["history_page"] = Request.Url.AbsolutePath;

            switch (ddl_select_report.SelectedValue)
            {
                case "1": // List of Employee - Payroll Master
                    printreport = "cryListOfEmployees/cryListOfEmployees_Payroll.rpt";
                    procedure   = "sp_payrollemployeemaster_rep";
                    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_payroll_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_payroll_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_employment_type.SelectedValue.ToString().Trim() + ",par_department_code," + ddl_department_report.SelectedValue.ToString().Trim() + ",par_emp_rcrd_status," + ddl_status.SelectedValue.ToString().Trim();
                    break;

                case "2": // List of Employee - Plantilla
                    printreport = "cryListOfEmployees/cryListOfEmployees_Plantilla.rpt";
                    procedure = "sp_list_of_employees_report";
                    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_emp_status," + ddl_status.SelectedValue.ToString().Trim() + ",par_budget_code," + ddl_budget_year.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_employment_type.SelectedValue.ToString().Trim() + ",par_dept_code," + ddl_department_report.SelectedValue.ToString().Trim();
                    break;

                case "3": // List of Employee - Age
                    printreport = "cryListOfEmployees/cryListOfEmployees_Age.rpt";
                    procedure = "sp_employee_list_age";
                    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",p_year," + ddl_payroll_year.SelectedValue.ToString().Trim() + ",p_month," + ddl_payroll_month.SelectedValue.ToString().Trim() + ",p_age," + txtb_age.Text.ToString().Trim();
                    break;

                case "4": // List of Employee - Resigned and Retired
                    printreport = "cryListOfEmployees/cryListofEmployees_Retired_Resigned.rpt";
                    procedure = "sp_resignations_retirement_rep";
                    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_year_of_effectivity," + ddl_payroll_year.SelectedValue.ToString().Trim();
                    break;
            }
            if (url != "")
            {
                Response.Redirect(url);
            }
        }
        //**************************************************************************
        //  BEGIN Jade- 09/17/18 - Populate Combo list from Province table
        //*************************************************************************
        protected void ddl_select_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleReportOptions();
        }
        //**************************************************************************
        //  BEGIN Jade- 09/17/18 - Populate Combo list from Province table
        //*************************************************************************
        private void ToggleReportOptions()
        {

            lbl_year.InnerText = "Year:";
            if (ddl_select_report.SelectedValue == "1")
            {
                div_budget_year.Visible     = false;
                div_payroll_year.Visible    = true;
                div_payroll_month.Visible   = true;
                div_age.Visible             = false;
                div_employment_type.Visible = true;
                div_status.Visible          = true;
                div_department.Visible      = true;
            }

            else if (ddl_select_report.SelectedValue == "2")
            {
                div_budget_year.Visible     = true;
                div_payroll_year.Visible    = false;
                div_payroll_month.Visible   = false;
                div_age.Visible             = false;
                div_employment_type.Visible = true;
                div_status.Visible          = true;
                div_department.Visible      = true;
            }
            else if (ddl_select_report.SelectedValue == "3")
            {
                div_budget_year.Visible     = false;
                div_payroll_year.Visible    = true;
                div_payroll_month.Visible   = true;
                div_age.Visible             = true;
                div_employment_type.Visible = false;
                div_status.Visible          = false;
                div_department.Visible      = false;
            }
            else if (ddl_select_report.SelectedValue == "4")
            {
                div_budget_year.Visible     = false;
                div_payroll_year.Visible    = true;
                div_payroll_month.Visible   = false;
                div_age.Visible             = false;
                div_employment_type.Visible = false;
                div_status.Visible          = false;
                div_department.Visible      = false;
                lbl_year.InnerText          = "Effective Year:";
            }
        }
        
        protected void lnkbtn_view_remarks_Click1(object sender, EventArgs e)
        {
            DataTable dt_payrollremarks = new DataTable();
            string query = "SELECT * FROM payrollemployeemaster_remarks_tbl WHERE empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' ORDER BY created_dttm DESC";
            dt_payrollremarks = MyCmn.GetDatatable(query);
            CommonCode.GridViewBind(ref this.GridView1, dt_payrollremarks);
            UpdatePanel36.Update();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReportModalViewRemarks", "openModalViewRemarks()", true);
        }
        
        protected void btn_add_remarks1_Click(object sender, EventArgs e)
        {
            txtb_remarks_descr.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReportModalAddRemarks123", "openModalAddRemarks()", true);
        }

        protected void chckbox_flag_hdmf_CheckedChanged(object sender, EventArgs e)
        {
            payrollmasterremarks();
        }
        protected void payrollmasterremarks()
        {
                var remarks = "";
            if (IsDataValidated())
            {
                if (Boolean.Parse(ViewState["prev_chckbox_flag_gsis"].ToString())  != chckbox_flag_gsis.Checked
                || Boolean.Parse(ViewState["prev_chckbox_flag_phic"].ToString())   != chckbox_flag_phic.Checked
                || Boolean.Parse(ViewState["prev_chckbox_flag_hdmf"].ToString())   != chckbox_flag_hdmf.Checked
                || double.Parse(ViewState["prev_txtb_hdmf_fix_rate"].ToString())   != double.Parse(txtb_hdmf_fix_rate.Text)
                )
                {
                    remarks += Boolean.Parse(ViewState["prev_chckbox_flag_gsis"].ToString())  != chckbox_flag_gsis.Checked             ? "GSIS      changed from " + (Boolean.Parse(ViewState["prev_chckbox_flag_gsis"].ToString()) == true ? " checked " : " unchecked ") + " to "+ (chckbox_flag_gsis.Checked == true ? " checked " : " unchecked ") + ", "              : "";
                    remarks += Boolean.Parse(ViewState["prev_chckbox_flag_phic"].ToString())  != chckbox_flag_phic.Checked             ? "PHIC      changed from " + (Boolean.Parse(ViewState["prev_chckbox_flag_phic"].ToString()) == true ? " checked " : " unchecked ") + " to "+ (chckbox_flag_phic.Checked == true ? " checked " : " unchecked ") + ", "              : "";
                    remarks += Boolean.Parse(ViewState["prev_chckbox_flag_hdmf"].ToString())  != chckbox_flag_hdmf.Checked             ? "HDMF      changed from " + (Boolean.Parse(ViewState["prev_chckbox_flag_hdmf"].ToString()) == true ? " checked " : " unchecked ") + " to "+ (chckbox_flag_hdmf.Checked == true ? " checked " : " unchecked ") + ", "              : "";
                    remarks += double.Parse(ViewState["prev_txtb_hdmf_fix_rate"].ToString())  != double.Parse(txtb_hdmf_fix_rate.Text) ? "HDMF RATE changed from " + double.Parse(ViewState["prev_txtb_hdmf_fix_rate"].ToString()) + " to " + double.Parse(txtb_hdmf_fix_rate.Text) + ") ," : "";
                }
            }
            txtb_remarks.Text = remarks;
        }
        
        protected void txtb_hdmf_fix_rate_TextChanged(object sender, EventArgs e)
        {
            payrollmasterremarks();
            txtb_hdmf_fix_rate.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_hdmf_fix_rate.Focus();
        }

        protected void SaveRemarks(string action_type)
        {
            if (action_type == "SAVE-INDIVIDUAL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopocloseModalAddRemarks", "closeModalAddRemarks();", true);
                string insert_script    = "INSERT INTO payrollemployeemaster_remarks_tbl VALUES('" + txtb_empl_id.Text.ToString().Trim() + "','" + txtb_effective_date.Text.ToString().Trim() + "','" + ddl_remarks_type.SelectedValue.Replace("'", "''") + "','" + txtb_remarks_descr.Text.Replace("'", "''") + "','" + DateTime.Now.ToString() + "','" + Session["ep_user_id"].ToString().Replace("'", "''") + "','" + Session["ep_owner_fullname"].ToString().Replace("'", "''") + "')";
                SaveAddEdit.Text        = MyCmn.InsertToTable(insert_script);
                h2_status.InnerText     = "SUCESSFULLY ADDED!";
                i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popopen_Dynamic_Notification", "open_Dynamic_Notification();", true);
            }
            else
            {
                if (txtb_remarks.Text.ToString() != "")
                {
                    string insert_script = "INSERT INTO payrollemployeemaster_remarks_tbl VALUES('" + txtb_empl_id.Text.ToString().Trim() + "','" + txtb_effective_date.Text.ToString().Trim() + "','" + "" + "','" + txtb_remarks.Text.Replace("'", "''") + "','" + DateTime.Now.ToString() + "','" + Session["ep_user_id"].ToString().Replace("'", "''") + "','" + Session["ep_owner_fullname"].ToString().Replace("'", "''") + "')";
                    SaveAddEdit.Text = MyCmn.InsertToTable(insert_script);
                }
            }
        }

        protected void btnSaveRemarks_Click(object sender, EventArgs e)
        {
            SaveRemarks("SAVE-INDIVIDUAL");
        }
        //*************************************************************************
        //  END OF CODE
        //*************************************************************************
    }
}