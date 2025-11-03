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

namespace HRIS_ePayroll.View.cPayrollMstAsg
{
    public partial class cPayrollMstAsg : System.Web.UI.Page
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
            if (ddl_year.SelectedValue != "" || ddl_start_with.SelectedValue != "")
            {
                chkIncludeHistory.Enabled = true;
            }
            else
            {
                chkIncludeHistory.Enabled = false;

            }
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            ViewState["chkIncludeHistory"] = "N";

            RetrieveYear();
            RetrieveBindingDepartments();
            RetrieveEmploymentType();
            //
            //RetrieveEmpl();
            RetrieveDataListGrid();
            //
            //// BEGIN - UPDATE : JADE - 05/20/2019 - Add Fields
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveBindingFundcharges();
            RetrieveBindingFunction();
            //// END  - UPDATE : JADE - 05/20/2019 - Add Fields
            //
            //RetrieveDataListGrid_dtl();

            btnAdd.Visible = false;
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollemployeemaster_asg_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_include_history", ViewState["chkIncludeHistory"].ToString().Trim(), "par_letter", ddl_start_with.SelectedValue.ToString().Trim());

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Employment Type
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
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Employee Nmae
        //*************************************************************************
        private void RetrieveEmpl()
        {
            ddl_empl_name.Items.Clear();
            dtSource_for_names = MyCmn.RetrieveData("sp_personnelnames_combolist_asg", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_letter", ddl_start_with.SelectedValue.ToString().Trim());

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
            ddl_dep.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_departments_tbl_list", "par_include_history", "N");

            ddl_dep.DataSource = dt;
            ddl_dep.DataValueField = "department_code";
            ddl_dep.DataTextField = "department_name1";
            ddl_dep.DataBind();
            ListItem li1 = new ListItem("-- Select Here --", "");
            ddl_dep.Items.Insert(0, li1);
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
        //  BEGIN - VJA- 01/17/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            //For Header
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();


            txtb_effective_date.Text = DateTime.Now.ToString("yyyy-MM-dd");

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
            FieldValidationColorChanged(false, "ALL");

            ddl_empl_name.Enabled = true;
            ddl_empl_name.Visible = true;
            txtb_effective_date.Enabled = true;
            txtb_empl_name.Visible = false;
            ddl_upd_master_flag.Enabled = true;
            LabelForMasterDescription();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            ddl_empl_name.SelectedIndex = 0;
            txtb_empl_id.Text = "";

            ddl_dep.SelectedIndex = -1;
            ddl_subdep.SelectedIndex = -1;
            ddl_division.SelectedIndex = -1;
            ddl_section.SelectedIndex = -1;
            ddl_function_code.SelectedIndex = -1;
            ddl_fund_charges.SelectedIndex = -1;
            ddl_upd_master_flag.SelectedValue = "0";
            UpdatePanelEffec.Update();

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
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("department_code", typeof(System.String));
            dtSource.Columns.Add("subdepartment_code", typeof(System.String));
            dtSource.Columns.Add("division_code", typeof(System.String));
            dtSource.Columns.Add("section_code", typeof(System.String));
            dtSource.Columns.Add("function_code", typeof(System.String));
            dtSource.Columns.Add("fund_code", typeof(System.String));
            dtSource.Columns.Add("upd_master_flag", typeof(System.String));
            dtSource.Columns.Add("phic_flag", typeof(System.String));
            dtSource.Columns.Add("ss_appl_flag", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add Primary Key Field to datasource- Insert Table : payrollemployeemaster_asg_tbl
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollemployeemaster_asg_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "employment_type", "effective_date" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["employment_type"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["department_code"] = string.Empty;
            nrow["subdepartment_code"] = string.Empty;
            nrow["division_code"] = string.Empty;
            nrow["section_code"] = string.Empty;
            nrow["function_code"] = string.Empty;
            nrow["fund_code"] = string.Empty;
            nrow["upd_master_flag"] = string.Empty;
            nrow["phic_flag"] = string.Empty;
            nrow["ss_appl_flag"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string editExpression = "empl_id = '" + svalues[0].Trim() + "' AND effective_date = '" + svalues[1].Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;

            txtb_empl_id.Text = row2Edit[0]["empl_id"].ToString();
            txtb_empl_name.Text = row2Edit[0]["employee_name"].ToString();
            txtb_effective_date.Text = row2Edit[0]["effective_date"].ToString();
            ddl_dep.SelectedValue = row2Edit[0]["department_code"].ToString();
            ddl_subdep.SelectedValue = row2Edit[0]["subdepartment_code"].ToString();
            RetrieveBindingDivision();
            ddl_division.SelectedValue = row2Edit[0]["division_code"].ToString();
            RetrieveBindingSection();
            ddl_section.SelectedValue = row2Edit[0]["section_code"].ToString();
            ddl_fund_charges.SelectedValue = row2Edit[0]["fund_code"].ToString();
            ddl_function_code.SelectedValue = row2Edit[0]["function_code"].ToString();
            ddl_upd_master_flag.SelectedValue = row2Edit[0]["upd_master_flag"].ToString() == "True" ? "1" : "0";
            ddl_phic_flag.SelectedValue = row2Edit[0]["phic_flag"].ToString() == "True" ? "1" : "0";
            ddl_ss_appl_flag.SelectedValue = row2Edit[0]["ss_appl_flag"].ToString() == "True" ? "1" : "0";

            if (row2Edit[0]["upd_master_flag"].ToString() == "1" ||
                row2Edit[0]["upd_master_flag"].ToString() == "True")
            {
                ddl_upd_master_flag.Enabled = false;
            }
            else
            {
                ddl_upd_master_flag.Enabled = true;
            }
            LabelForMasterDescription();

            LabelAddEdit.Text = "Edit Record: " + ddl_empl_name.SelectedValue.ToString().Trim();

            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;
            ViewState["old_group_nbr"] = "";

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
            if (IsDataValidated())
            {
                if (saveRecord == MyCmn.CONST_ADD)
                {
                    // BEGIN : Save from Header Table - payrollemployeemaster_asg_tbl
                    dtSource.Rows[0]["empl_id"] = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["employment_type"] = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["effective_date"] = txtb_effective_date.Text.ToString();
                    dtSource.Rows[0]["department_code"] = ddl_dep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["subdepartment_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["division_code"] = ddl_division.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["section_code"] = ddl_section.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["function_code"] = ddl_function_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["fund_code"] = ddl_fund_charges.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["upd_master_flag"] = ddl_upd_master_flag.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["phic_flag"] = ddl_phic_flag.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["ss_appl_flag"] = ddl_ss_appl_flag.SelectedValue.ToString().Trim();
                    // BEGIN : Save from Header Table - payrollemployeemaster_asg_tbl

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    // BEGIN : Save from Header Table - payrollemployeemaster_asg_tbl
                    dtSource.Rows[0]["empl_id"] = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["employment_type"] = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["effective_date"] = txtb_effective_date.Text.ToString();
                    dtSource.Rows[0]["department_code"] = ddl_dep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["subdepartment_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["division_code"] = ddl_division.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["section_code"] = ddl_section.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["function_code"] = ddl_function_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["fund_code"] = ddl_fund_charges.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["upd_master_flag"] = ddl_upd_master_flag.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["phic_flag"] = ddl_phic_flag.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["ss_appl_flag"] = ddl_ss_appl_flag.SelectedValue.ToString().Trim();

                    // BEGIN : Save from Header Table - payrollemployeemaster_asg_tbl


                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    if (scriptInsertUpdate == string.Empty) return;
                    string msg1 = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg1 == "") return;

                    if (msg1.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist");
                        // JVA : 2020-09-30 - Check the Employee Master if Exist 
                        if (msg1.Contains("payrollemployeemaster_hdr_tbl") == true &&
                             msg1.Contains("duplicate") == true)
                        {
                            FieldValidationColorChanged(true, "already-exist-master");

                            return;
                        }
                        return;
                    }

                    // Update On : 2020-09-29 - Ge Comment kay nag buhat ug trigger sir Ariel 
                    // BEGIN : VJA - 11-25-2019 : Update The Table : payrollemployeemaster_hdr_tbl - Assignment Tab
                    // DataTable dt = MyCmn.RetrieveData("sp_payrollemployeemaster_asg_tbl_upd_master_hdr", "par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_dep.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString().Trim(), "par_division_code", ddl_division.SelectedValue.ToString().Trim(), "par_section_code", ddl_section.SelectedValue.ToString().Trim(), "par_function_code", ddl_function_code.SelectedValue.ToString().Trim(), "par_fund_code", ddl_fund_charges.SelectedValue.ToString().Trim());
                    // END : VJA - 11-25-2019 : Update The Table : payrollemployeemaster_hdr_tbl - Assignment Tab


                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        // BEGIN : Save from Header Table - payrollemployeemaster_asg_tbl
                        nrow["empl_id"] = txtb_empl_id.Text.ToString().Trim();
                        nrow["employment_type"] = ddl_empl_type.SelectedValue.ToString().Trim();
                        nrow["effective_date"] = txtb_effective_date.Text.ToString();
                        nrow["department_code"] = ddl_dep.SelectedValue.ToString().Trim();
                        nrow["subdepartment_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                        nrow["division_code"] = ddl_division.SelectedValue.ToString().Trim();
                        nrow["section_code"] = ddl_section.SelectedValue.ToString().Trim();
                        nrow["function_code"] = ddl_function_code.SelectedValue.ToString().Trim();
                        nrow["fund_code"] = ddl_fund_charges.SelectedValue.ToString().Trim();
                        // BEGIN : Save from Header Table - payrollemployeemaster_asg_tbl

                        // BEGIN -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl
                        nrow["empl_id"] = ddl_empl_name.SelectedValue.ToString().Trim();
                        nrow["employment_type"] = ddl_empl_type.SelectedValue.ToString().Trim();
                        nrow["effective_date"] = txtb_effective_date.Text.ToString();
                        nrow["employee_name"] = ddl_empl_name.SelectedItem.ToString().Trim();
                        //nrow["position_code"]       = ddl_position.SelectedValue.ToString();
                        //nrow["hazard_pay_override"] = txtb_hazard_pay_override_hidden.Text.ToString().Trim();
                        // END  -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl
                        nrow["upd_master_flag"]       = ddl_upd_master_flag.SelectedValue.ToString().Trim() == "0" ? "False" : "True";
                        nrow["phic_flag"]         = ddl_phic_flag.SelectedValue.ToString().Trim() == "0" ? "False" : "True";
                        nrow["upd_master_flag_descr"] = ddl_upd_master_flag.SelectedItem.ToString().Trim();
                        nrow["ss_appl_flag"]         = ddl_ss_appl_flag.SelectedValue.ToString().Trim() == "0" ? "False" : "True";
                        nrow["department_name1"]         = ddl_dep.SelectedItem.ToString().Trim() ;


                        // *******************************************************************************************
                        // *** VJA - 2022-07-01 - Insert/Update/Delete - Audit Table for Change of Assignment ********
                        // *******************************************************************************************
                        InsertUpdateDeleteASG(txtb_empl_id.Text.ToString().Trim()
                                             , ddl_empl_type.SelectedValue.ToString().Trim()
                                             , txtb_effective_date.Text.ToString()
                                             , ddl_dep.SelectedValue.ToString().Trim()
                                             , ddl_subdep.SelectedValue.ToString().Trim()
                                             , ddl_division.SelectedValue.ToString().Trim()
                                             , ddl_section.SelectedValue.ToString().Trim()
                                             , ddl_function_code.SelectedValue.ToString().Trim()
                                             , ddl_fund_charges.SelectedValue.ToString().Trim()
                                             , ddl_upd_master_flag.SelectedValue.ToString().Trim()
                                             , ddl_phic_flag.SelectedValue.ToString().Trim()
                                             , ddl_ss_appl_flag.SelectedValue.ToString().Trim()
                                             , "INSERT"
                                             );
                        // *******************************************************************************************
                        // *** VJA - 2022-07-01 - Insert/Update/Delete - Audit Table for Change of Assignment ********
                        // *******************************************************************************************

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND employment_type = '" + ddl_empl_type.SelectedValue.ToString().Trim() + "' AND effective_date = '" + txtb_effective_date.Text.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        // BEGIN : Save from Header Table - payrollemployeemaster_asg_tbl
                        row2Edit[0]["empl_id"] = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employment_type"] = ddl_empl_type.SelectedValue.ToString().Trim();
                        row2Edit[0]["effective_date"] = txtb_effective_date.Text.ToString();
                        row2Edit[0]["department_code"] = ddl_dep.SelectedValue.ToString().Trim();
                        row2Edit[0]["subdepartment_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                        row2Edit[0]["division_code"] = ddl_division.SelectedValue.ToString().Trim();
                        row2Edit[0]["section_code"] = ddl_section.SelectedValue.ToString().Trim();
                        row2Edit[0]["function_code"] = ddl_function_code.SelectedValue.ToString().Trim();
                        row2Edit[0]["fund_code"] = ddl_fund_charges.SelectedValue.ToString().Trim();
                        // BEGIN : Save from Header Table - payrollemployeemaster_asg_tbl

                        // BEGIN -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl
                        row2Edit[0]["empl_id"] = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employment_type"] = ddl_empl_type.SelectedValue.ToString().Trim();
                        row2Edit[0]["effective_date"] = txtb_effective_date.Text.ToString();
                        //row2Edit[0]["position_code"]       = ddl_position.SelectedValue.ToString();
                        //row2Edit[0]["hazard_pay_override"] = txtb_hazard_pay_override_hidden.Text.ToString().Trim();
                        // END  -  Separate Table for Payroll Master Reference - Table Name : payrollemployeemaster_pos_tbl

                        row2Edit[0]["employee_name"] = txtb_empl_name.Text.ToString().Trim();
                        row2Edit[0]["upd_master_flag"] = ddl_upd_master_flag.SelectedValue.ToString().Trim() == "0" ? "False" : "True";
                        row2Edit[0]["phic_flag"] = ddl_phic_flag.SelectedValue.ToString().Trim() == "0" ? "False" : "True";
                        row2Edit[0]["upd_master_flag_descr"] = ddl_upd_master_flag.SelectedItem.ToString().Trim();
                        row2Edit[0]["ss_appl_flag"] = ddl_ss_appl_flag.SelectedValue.ToString().Trim() == "0" ? "False" : "True";
                        row2Edit[0]["department_name1"] = ddl_dep.SelectedItem.ToString().Trim() ;


                        // *******************************************************************************************
                        // *** VJA - 2022-07-01 - Insert/Update/Delete - Audit Table for Change of Assignment ********
                        // *******************************************************************************************
                        InsertUpdateDeleteASG(txtb_empl_id.Text.ToString().Trim()
                                             , ddl_empl_type.SelectedValue.ToString().Trim()
                                             , txtb_effective_date.Text.ToString()
                                             , ddl_dep.SelectedValue.ToString().Trim()
                                             , ddl_subdep.SelectedValue.ToString().Trim()
                                             , ddl_division.SelectedValue.ToString().Trim()
                                             , ddl_section.SelectedValue.ToString().Trim()
                                             , ddl_function_code.SelectedValue.ToString().Trim()
                                             , ddl_fund_charges.SelectedValue.ToString().Trim()
                                             , ddl_upd_master_flag.SelectedValue.ToString().Trim()
                                             , ddl_phic_flag.SelectedValue.ToString().Trim()
                                             , ddl_ss_appl_flag.SelectedValue.ToString().Trim()
                                             , "UPDATE"
                                             );
                        // *******************************************************************************************
                        // *** VJA - 2022-07-01 - Insert/Update/Delete - Audit Table for Change of Assignment ********
                        // *******************************************************************************************

                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
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
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR employment_type LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR upd_master_flag_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR effective_date LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("employment_type", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("department_code", typeof(System.String));
            dtSource1.Columns.Add("subdepartment_code", typeof(System.String));
            dtSource1.Columns.Add("division_code", typeof(System.String));
            dtSource1.Columns.Add("section_code", typeof(System.String));
            dtSource1.Columns.Add("function_code", typeof(System.String));
            dtSource1.Columns.Add("fund_code", typeof(System.String));
            dtSource1.Columns.Add("upd_master_flag_descr", typeof(System.String));
            dtSource1.Columns.Add("department_name1", typeof(System.String));
            dtSource1.Columns.Add("department_short_name", typeof(System.String));

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

            if (ddl_empl_name.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_name");
                validatedSaved = false;
            }
            if (ddl_dep.SelectedValue == "" )
            {
                FieldValidationColorChanged(true, "ddl_dep");
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(txtb_effective_date.Text) == false)
            {
                FieldValidationColorChanged(true, "txtb_effective_date");
                validatedSaved = false;
            }


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

                    case "ddl_empl_name":
                        {
                            LblRequired12.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_name.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "txtb_effective_date":
                        {
                            LblRequired13.Text = MyCmn.CONST_RQDFLD;
                            txtb_effective_date.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired12.Text = "Already Exist";
                            LblRequired13.Text = "Already Exist";
                            txtb_effective_date.BorderColor = Color.Red;
                            ddl_empl_name.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "already-exist-master":
                        {
                            LblRequired12.Text = "This effective date is already exist on Employee Master";
                            ddl_empl_name.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            break;
                        }
                    case "ddl_dep":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_dep.BorderColor = Color.Red;
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
                            LblRequired13.Text = "";
                            LblRequired1.Text = "";
                            ddl_empl_name.BorderColor = Color.LightGray;
                            txtb_effective_date.BorderColor = Color.LightGray;
                            ddl_dep.BorderColor = Color.LightGray;
                            UpdatePanelEffec.Update();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
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
            if (ddl_empl_type.SelectedValue != "" && ddl_year.SelectedValue != "")
            {
                btnAdd.Visible = true;
                chkIncludeHistory.Enabled = true;
            }
            else
            {
                chkIncludeHistory.Enabled = false;
                btnAdd.Visible = false;
            }
            RetrieveDataListGrid();
            UpdatePanel10.Update();
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

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_start_with.SelectedValue != "" && ddl_empl_type.SelectedValue != "")
            {

                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveDataListGrid();
            RetrieveEmpl();
            UpdatePanel10.Update();
        }

        protected void ddl_start_with_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_start_with.SelectedValue != "" && ddl_empl_type.SelectedValue != "")
            {

                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveDataListGrid();
            RetrieveEmpl();
            UpdatePanel10.Update();
        }

        protected void ddl_empl_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_name.SelectedValue != "")
            {
                txtb_empl_id.Text = ddl_empl_name.SelectedValue.ToString();
            }
            else
            {
                ClearEntry();
            }
        }
        //*******************************************************
        //  BEGIN - JADE- 2020-10-12 - Select Update Master
        //********************************************************
        protected void ddl_upd_master_flag_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelForMasterDescription();
        }
        //*******************************************************************************
        //  BEGIN - JADE- 2020-10-12 - Change The Label Description for  Update Master
        //********************************************************************************
        private void LabelForMasterDescription()
        {
            if (ddl_upd_master_flag.SelectedValue == "0" ||
                ddl_upd_master_flag.SelectedValue == "False")
            {
                lbl_upd_master_flag.Text = "";
            }
            else
            {
                lbl_upd_master_flag.Text = "Update Payroll Employee Master Assignment!";
            }
        }
        //********************************************************************************
        //  BEGIN - JADE- 2022-07-21 - Change Assignment Audit Table *********************
        //********************************************************************************
        private void InsertUpdateDeleteASG(string par_empl_id
                                          ,string par_employment_type
                                          ,string par_effective_date
                                          ,string par_department_code
                                          ,string par_subdepartment_code
                                          ,string par_division_code
                                          ,string par_section_code
                                          ,string par_function_code
                                          ,string par_fund_code
                                          ,string par_upd_master_flag
                                          ,string par_phic_flag
                                          ,string par_ss_appl_flag
                                          ,string par_action_descr
                                          ) 
        {
            string insert_update_script = "";
            insert_update_script = "insert into payrollemployeemaster_asg_aud_tbl select "
                                        + " '" + par_empl_id                      + "'"
                                        + ",'" + par_employment_type              + "'"
                                        + ",'" + par_effective_date               + "'"
                                        + ",'" + par_department_code              + "'"
                                        + ",'" + par_subdepartment_code           + "'"
                                        + ",'" + par_division_code                + "'"
                                        + ",'" + par_section_code                 + "'"
                                        + ",'" + par_function_code                + "'"
                                        + ",'" + par_fund_code                    + "'"
                                        + ",'" + par_upd_master_flag              + "'"
                                        + ",'" + par_phic_flag                    + "'"
                                        + ",'" + par_ss_appl_flag                 + "'"
                                        + ",'" + par_action_descr                 + "'"
                                        + ",'" + DateTime.Now.ToString()          + "'"
                                        + ",'" + Session["ep_user_id"].ToString() + "'";

            MyCmn.Update_InsertTable(insert_update_script);

        }
        //*************************************************************************
        //  END OF CODE
        //*************************************************************************
    }
}