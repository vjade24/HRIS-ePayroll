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

namespace HRIS_ePayroll.View.cPayReportGrouping
{
    public partial class cPayReportGrouping : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Data Place holder creation 
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

        DataTable dtSource_fundcharges
        {
            get
            {
                if ((DataTable)ViewState["dtSource_fundcharges"] == null) return null;
                return (DataTable)ViewState["dtSource_fundcharges"];
            }
            set
            {
                ViewState["dtSource_fundcharges"] = value;
            }
        }

        DataTable dtSource_function
        {
            get
            {
                if ((DataTable)ViewState["dtSource_function"] == null) return null;
                return (DataTable)ViewState["dtSource_function"];
            }
            set
            {
                ViewState["dtSource_function"] = value;
            }
        }

        //DataTable dtSource_dtl_for_display
        //{
        //    get
        //    {
        //        if ((DataTable)ViewState["dtSource_dtl_for_display"] == null) return null;
        //        return (DataTable)ViewState["dtSource_dtl_for_display"];
        //    }
        //    set
        //    {
        //        ViewState["dtSource_dtl_for_display"] = value;
        //    }
        //}

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

        //DataTable dtSource_for_employeegrouping_details
        //{
        //    get
        //    {
        //        if ((DataTable)ViewState["dtSource_for_employeegrouping_details"] == null) return null;
        //        return (DataTable)ViewState["dtSource_for_employeegrouping_details"];
        //    }
        //    set
        //    {
        //        ViewState["dtSource_for_employeegrouping_details"] = value;
        //    }
        //}

        DataTable dt_result
        {
            get
            {
                if ((DataTable)ViewState["dt_result"] == null) return null;
                return (DataTable)ViewState["dt_result"];
            }
            set
            {
                ViewState["dt_result"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************
                
        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "grouping_descr";
                    Session["SortOrder"] = "ASC";
                    InitializePage();
                    //if (Session["SortField"].ToString() == "" || Session["SortField"].ToString() == null)
                    //{
                    //}
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
        //  BEGIN - VJA- 06/11/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPayReportGrouping"] = "cPayReportGrouping";
            RetrieveDataListGrid();
            RetrieveEmploymentType();

            //Retrieve When Add
            RetrieveBindingDep();
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveEmployeename();
            
            RetrieveBindingFundcharges();
            RetrieveBindingFunction();

            RetrieveYear();
            RetrieveSpecialGroup();

            btnAdd.Visible = false;
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private string RetrieveGroupCode()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_next_nbr", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_special_group", ddl_special_group_main.SelectedValue.ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                lbl_group_no.Text =  dt.Rows[0]["next_payroll_group_nbr"].ToString();
            }
            return dt.Rows[0]["next_payroll_group_nbr"].ToString();
        }
        private void RetrieveEmployeename()
        {
            /*   01  -  Common Groupings
                 02  -  Communication Expense
                 03  -  RATA and Quarterly Allowance
                 04  -  Monetization
                 05  -  Hazard, Subsistence and Laundry Pay
                 06  -  Overtime Pay
                 07  -  Loyalty Bonus
           */

            ddl_empl_id.Items.Clear();
            if (ddl_special_group_main.SelectedValue    == "01"     // Common Groupings
                || ddl_special_group_main.SelectedValue == "06"     // Overtime Pay 
                || ddl_special_group_main.SelectedValue == "99")     // Other Payroll

            {
                // This Combolist is for Common Groupings
                DataTable dt = MyCmn.RetrieveData("sp_personnelnames_combolist15", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_dep.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString(), "par_division_code", ddl_division.SelectedValue.ToString().Trim(), "par_section_code", ddl_section.SelectedValue.ToString().Trim());
                ddl_empl_id.DataSource = dt;
            }
            else if (ddl_special_group_main.SelectedValue == "02")
            {
                // This Combolist is for Communication Expense

                DataTable dt = MyCmn.RetrieveData("sp_personnelnames_combolist17", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
                ddl_empl_id.DataSource = dt;
            }
            else if (ddl_special_group_main.SelectedValue == "03")
            {
                // This Combolist is for RATA and Quarterly Allowance

                DataTable dt = MyCmn.RetrieveData("sp_personnelnames_combolist18", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
                ddl_empl_id.DataSource = dt;
            }
            else if (ddl_special_group_main.SelectedValue == "04")
            {
                // This Combolist is for Monetization
                ddl_empl_id.Items.Clear();
            }
            else if (ddl_special_group_main.SelectedValue == "05")
            {
                // This Combolist is for Hazard, Subsistence and Laundry Pay

                DataTable dt = MyCmn.RetrieveData("sp_personnelnames_combolist_group_subs", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
                ddl_empl_id.DataSource = dt;
            }
            else if (ddl_special_group_main.SelectedValue == "07")
            {
                // This Combolist is for Loyalty Bonus
                DataTable dt = MyCmn.RetrieveData("sp_personnelnames_combolist_group_loyalty", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_year", ddl_year.SelectedValue.ToString().Trim());
                ddl_empl_id.DataSource = dt;
            }
            
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Populate Department
        //********************************************************************
        private void RetrieveBindingDep()
        {
            ddl_dep.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_departments_tbl_list", "par_include_history", "N");

            ddl_dep.DataSource = dt;
            ddl_dep.DataValueField = "department_code";
            ddl_dep.DataTextField = "department_name1";
            ddl_dep.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_dep.Items.Insert(0, li);
        }
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Populate Sub-Department
        //********************************************************************
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
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Populate Division
        //********************************************************************
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
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Populate Section
        //********************************************************************
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
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Populate Fund Charges
        //********************************************************************
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
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Populate Function
        //********************************************************************
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
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Populate Employment Type
        //********************************************************************
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
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Populate Special Group
        //********************************************************************
        private void RetrieveSpecialGroup()
        {
            ddl_special_group.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payspecialgroups_tbl_list");
            ddl_special_group.DataSource = dt;
            ddl_special_group.DataValueField = "special_group";
            ddl_special_group.DataTextField = "special_group_descr";
            ddl_special_group.DataBind();

            ddl_special_group_main.Items.Clear();
            ddl_special_group_main.DataSource = dt;
            ddl_special_group_main.DataValueField = "special_group";
            ddl_special_group_main.DataTextField = "special_group_descr";
            ddl_special_group_main.DataBind();
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Retrieve back end data and load to GridView - Header
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollemployeegroupings_hdr_tbl_list", "par_employment_type" , ddl_empl_type.SelectedValue.ToString().Trim(), "par_special_group",ddl_special_group_main.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //********************************************************************
        //  BEGIN - VJA- 06/11/2019 - Retrieve back end data and load to GridView - Detials
        //********************************************************************
        private void RetrieveDataListGrid_dtl()
        {
            //InitializeTable_dtl_for_display();
            //AddPrimaryKeys_dtl_for_display();
            InitializeTable_dtl();
            dtSource_dtl             = MyCmn.RetrieveData("sp_payrollemployeegroupings_dtl_tbl_list","par_payroll_group_nbr", lbl_group_no.Text.ToString().Trim());
            AddPrimaryKeys_dtl();
            //dtSource_dtl_for_display = MyCmn.RetrieveData("sp_payrollemployeegroupings_dtl_tbl_list", "par_payroll_group_nbr", lbl_group_no.Text.ToString().Trim());

            foreach (DataRow nrow in dtSource_dtl.Rows)
            {
               // nrow["emp_status"] = 1;
                nrow["action"] = 1;
                nrow["retrieve"] = false;
            }

            CommonCode.GridViewBind(ref this.gv_details, dtSource_dtl);
            updatepanel_details.Update();
            gv_details.PageSize = Convert.ToInt32(DropDownList1.Text);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            RetrieveGroupCode();
            RetrieveDataListGrid_dtl();
            RetrieveEmployeename();

            // For Details
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
           
            //InitializeTable_dtl_for_display();
            //AddPrimaryKeys_dtl_for_display();
            
            //Get last row of the Column
            if (ddl_empl_type.SelectedValue == "RE")
            {
                ddl_fund_charges.Enabled = false;
            }
            else {
                ddl_fund_charges.Enabled = true;
            }
            ddl_special_group.SelectedValue = ddl_special_group_main.SelectedValue.ToString().Trim();

            ddl_dep.Enabled = true;
            ddl_subdep.Enabled = true;
            ddl_division.Enabled = true;
            ddl_section.Enabled = true;
            ToogleSpecialGroup();
            
            //ddl_special_group.Enabled = true;
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopInformation", "click_information();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            lbl_group_no.Text               = "";
            txtb_group_descr.Text           = "";
            txtb_charge_to.Text             = "";

            ddl_dep.SelectedIndex           = -1;
            ddl_subdep.SelectedIndex        = -1;
            ddl_division.SelectedIndex      = -1;
            ddl_section.SelectedIndex       = -1;
            ddl_empl_id.SelectedIndex       = -1;
            ddl_function_code.SelectedIndex = -1;
            ddl_fund_charges.SelectedIndex  = -1;
            ddl_special_group.SelectedIndex = -1;
            
            txtb_charge_to.Text = "";
            txtb_sig_name.Text = "";
            txtb_sig_designation.Text = "";

            txtb_sig2_name.Text        = "";
            txtb_sig2_designation.Text   = "";
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Initialized datasource fields/columns - Header
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_group_nbr",typeof(System.String));
            dtSource.Columns.Add("employment_type",typeof(System.String));
            dtSource.Columns.Add("grouping_descr",typeof(System.String));
            dtSource.Columns.Add("department_code",typeof(System.String));
            dtSource.Columns.Add("sub_department_code",typeof(System.String));
            dtSource.Columns.Add("division_code",typeof(System.String));
            dtSource.Columns.Add("section_code", typeof(System.String));
            dtSource.Columns.Add("fund_code", typeof(System.String));
            dtSource.Columns.Add("function_code", typeof(System.String));
            dtSource.Columns.Add("special_group", typeof(System.String));
            dtSource.Columns.Add("charge_to", typeof(System.String));
            dtSource.Columns.Add("sig_name", typeof(System.String));
            dtSource.Columns.Add("sig_designation", typeof(System.String));
            dtSource.Columns.Add("dttm_created", typeof(System.String));
            dtSource.Columns.Add("dttm_updated", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            // VJA : 2020-02-18 
            dtSource.Columns.Add("sig_name2", typeof(System.String));
            dtSource.Columns.Add("sig_designation2", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Initialized datasource fields/columns - Details
        //*************************************************************************
        private void InitializeTable_dtl()
        {
            dtSource_dtl = new DataTable();
            dtSource_dtl.Columns.Add("payroll_group_nbr", typeof(System.String));
            dtSource_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl.Columns.Add("employee_name", typeof(System.String));
            dtSource_dtl.Columns.Add("flag_update_master", typeof(System.String));
            dtSource_dtl.Columns.Add("emp_status", typeof(System.String));
            dtSource_dtl.Columns.Add("emp_status_descr", typeof(System.String));
            
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Initialized datasource fields/columns - Display 
        //*************************************************************************
        //private void InitializeTable_dtl_for_display()
        //{
        //    dtSource_dtl_for_display = new DataTable();
        //    dtSource_dtl_for_display.Columns.Add("payroll_group_nbr", typeof(System.String));
        //    dtSource_dtl_for_display.Columns.Add("empl_id", typeof(System.String));
        //    dtSource_dtl_for_display.Columns.Add("employee_name", typeof(System.String));
        //    dtSource_dtl_for_display.Columns.Add("flag_update_master", typeof(System.String));
        //    dtSource_dtl_for_display.Columns.Add("emp_status", typeof(System.String));
        //    dtSource_dtl_for_display.Columns.Add("emp_status_descr", typeof(System.String));
        //}
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollemployeegroupings_hdr_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_group_nbr", "employment_type" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add Primary Key Field to datasource - Details
        //*************************************************************************
        private void AddPrimaryKeys_dtl()
        {
            dtSource_dtl.TableName = "payrollemployeegroupings_dtl_tbl";
            dtSource_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_group_nbr", "empl_id"};
            dtSource_dtl = MyCmn.AddPrimaryKeys(dtSource_dtl, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add Primary Key Field to datasource - For Display
        //*************************************************************************
        //private void AddPrimaryKeys_dtl_for_display()
        //{
        //    dtSource_dtl_for_display.TableName = "payrollemployeegroupings_dtl_tbl";
        //    string[] col = new string[] { "payroll_group_nbr", "empl_id" };
        //    dtSource_dtl_for_display = MyCmn.AddPrimaryKeys(dtSource_dtl_for_display, col);
        //}
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_group_nbr"]=string.Empty;
            nrow["employment_type"]=string.Empty;
            nrow["grouping_descr"]=string.Empty;
            nrow["department_code"]=string.Empty;
            nrow["sub_department_code"]=string.Empty;
            nrow["division_code"]=string.Empty;
            nrow["section_code"] = string.Empty;
            nrow["fund_code"] = string.Empty;
            nrow["function_code"] = string.Empty;
            nrow["special_group"]= string.Empty;
            nrow["charge_to"]= string.Empty;
            nrow["sig_name"] = string.Empty;
            nrow["sig_designation"] = string.Empty;

            nrow["dttm_created"] = string.Empty;
            nrow["dttm_updated"] = string.Empty;
            nrow["created_by_user"] = string.Empty;
            nrow["updated_by_user"] = string.Empty;

            nrow["sig_name2"] = string.Empty;
            nrow["sig_designation2"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add new row to datatable object - Details
        //*************************************************************************
        private void AddNewRow_dtl()
        {
            DataRow nrow = dtSource_dtl.NewRow();
            nrow["payroll_group_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add new row to datatable object - For Display
        //*************************************************************************
        //private void AddNewRow_dtl_for_display()
        //{
        //    DataRow nrow = dtSource_dtl_for_display.NewRow();
        //    nrow["payroll_group_nbr"] = string.Empty;
        //    nrow["empl_id"] = string.Empty;
        //    nrow["employee_name"] = string.Empty;
        //    nrow["action"] = 1;
        //    nrow["retrieve"] = false;
        //}
        //***************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString().Trim();
            
            DataTable dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_chk_in_payroll", "par_payroll_group_nbr", commandArgs);
            
            if (dt.Rows.Count > 0)
            {
                msg_icon.Attributes.Remove("class");
                msg_icon.Attributes.Add("class", "fa fa-exclamation-circle fa-5x text-danger");
                msg_header.InnerText = "You cannot Delete this Group";
                lbl_details.Text     = "This Payroll Group is already used in Payroll!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openNotification();", true);
            }
            else
            {
                deleteRec1.Text = "Are you sure to delete this Record ?";
                lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
            }

        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] group_no1 = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "payroll_group_nbr = '" + group_no1[0].Trim() + "'";

            MyCmn.DeleteBackEndData("payrollemployeegroupings_dtl_tbl", "WHERE " + deleteExpression);
            MyCmn.DeleteBackEndData("payrollemployeegroupings_hdr_tbl", "WHERE " + deleteExpression);
            dt_result = MyCmn.RetrieveData("payrollemployeemaster_hdr_tbl_update", "par_empl_id", "", "par_employment_type", ddl_empl_type.SelectedValue.ToString(), "par_payroll_group_nbr", "", "par_update_all", "1", "par_payroll_group_nbr1", group_no1[0].Trim());
            
            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string editExpression = "payroll_group_nbr = '" + svalues.Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow3 = dtSource.NewRow();
            nrow3["payroll_group_nbr"] = string.Empty;
            nrow3["employment_type"] = string.Empty;
            nrow3["action"] = 2;
            nrow3["retrieve"] = true;
            dtSource.Rows.Add(nrow3);
            
            lbl_group_no.Text = svalues.Trim();
            txtb_group_descr.Text = row2Edit[0]["grouping_descr"].ToString();
            if (row2Edit[0]["department_code"].ToString() != string.Empty)
            {
                ddl_dep.SelectedValue = row2Edit[0]["department_code"].ToString();
            }
            if (row2Edit[0]["sub_department_code"].ToString() != string.Empty)
            {
                ddl_subdep.SelectedValue = row2Edit[0]["sub_department_code"].ToString();
            }
            else
            {
                ddl_subdep.SelectedIndex = -1;
            }
            RetrieveBindingDivision();

            if (row2Edit[0]["division_code"].ToString() != string.Empty && row2Edit[0]["division_code"].ToString() != "")
            {
                ddl_division.SelectedValue = row2Edit[0]["division_code"].ToString();
            }
            else
            {
                ddl_division.SelectedIndex = -1;
            }

            RetrieveBindingSection();

            if (row2Edit[0]["section_code"].ToString() != string.Empty)
            {
                ddl_section.SelectedValue   = row2Edit[0]["section_code"].ToString();
            }
            ddl_fund_charges.SelectedValue  = row2Edit[0]["fund_code"].ToString();
            ddl_function_code.SelectedValue = row2Edit[0]["function_code"].ToString();

            ddl_special_group.SelectedValue = row2Edit[0]["special_group"].ToString();
            txtb_charge_to.Text             = row2Edit[0]["charge_to"].ToString();
            txtb_sig_name.Text              = row2Edit[0]["sig_name"].ToString();
            txtb_sig_designation.Text       = row2Edit[0]["sig_designation"].ToString();
            
            dtSource.Rows[0]["dttm_created"]      = Convert.ToDateTime(row2Edit[0]["dttm_created"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            dtSource.Rows[0]["dttm_updated"]      = Convert.ToDateTime(row2Edit[0]["dttm_updated"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            dtSource.Rows[0]["created_by_user"]   = row2Edit[0]["created_by_user"].ToString();
            dtSource.Rows[0]["updated_by_user"]   = row2Edit[0]["updated_by_user"].ToString();

            txtb_sig2_name.Text             = row2Edit[0]["sig_name2"].ToString();
            txtb_sig2_designation.Text       = row2Edit[0]["sig_designation2"].ToString();

            RetrieveDataListGrid_dtl();
            ToogleSpecialGroup();
            RetrieveEmployeename();

            ddl_dep.Enabled = false;
            if (ddl_special_group.SelectedValue == "99" || // Other Payroll
                ddl_special_group.SelectedValue == "06")   // Overtime Payroll
            {
                ddl_dep.Enabled = true;
            }

            ddl_special_group.Enabled = false;
            LabelAddEdit.Text = "Edit Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            
            FieldValidationColorChanged(false, "ALL");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Change Field Sort mode  
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
        //  BEGIN - VJA- 06/11/2019 - Get Grid current sort order 
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
        //  BEGIN - VJA- 06/11/2019 - Save New Record/Edited Record to back end DB
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
                    dtSource.Rows[0]["payroll_group_nbr"]       = RetrieveGroupCode().ToString();
                    dtSource.Rows[0]["employment_type"]         = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["grouping_descr"]          = txtb_group_descr.Text.ToString().Trim();
                    dtSource.Rows[0]["department_code"]         = ddl_dep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["sub_department_code"]     = ddl_subdep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["division_code"]           = ddl_division.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["section_code"]            = ddl_section.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["fund_code"]               = ddl_fund_charges.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["function_code"]           = ddl_function_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["special_group"]           = ddl_special_group.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["charge_to"]               = txtb_charge_to.Text.ToString().Trim();
                    dtSource.Rows[0]["sig_name"]                = txtb_sig_name.Text.ToString().Trim();
                    dtSource.Rows[0]["sig_designation"]         = txtb_sig_designation.Text.ToString().Trim();

                    dtSource.Rows[0]["dttm_created"]           = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["dttm_updated"]           = "1900-01-01";
                    dtSource.Rows[0]["created_by_user"]        = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["updated_by_user"]        = "";

                    dtSource.Rows[0]["sig_name2"]              = txtb_sig2_name.Text;   
                    dtSource.Rows[0]["sig_designation2"]       = txtb_sig2_designation.Text   ;   
                    
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_group_nbr"]       = lbl_group_no.Text.ToString().Trim();
                    dtSource.Rows[0]["employment_type"]         = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["grouping_descr"]          = txtb_group_descr.Text.ToString().Trim();
                    dtSource.Rows[0]["department_code"]         = ddl_dep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["sub_department_code"]     = ddl_subdep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["division_code"]           = ddl_division.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["section_code"]            = ddl_section.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["fund_code"]               = ddl_fund_charges.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["function_code"]           = ddl_function_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["special_group"]           = ddl_special_group.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["charge_to"]               = txtb_charge_to.Text.ToString().Trim();
                    dtSource.Rows[0]["sig_name"]                = txtb_sig_name.Text.ToString().Trim();
                    dtSource.Rows[0]["sig_designation"]         = txtb_sig_designation.Text.ToString().Trim();

                    dtSource.Rows[0]["dttm_created"]           = dtSource.Rows[0]["dttm_created"].ToString();
                    dtSource.Rows[0]["dttm_updated"]           = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["created_by_user"]        = dtSource.Rows[0]["created_by_user"].ToString();
                    dtSource.Rows[0]["updated_by_user"]        = Session["ep_user_id"].ToString().Trim();

                    dtSource.Rows[0]["sig_name2"]              = txtb_sig2_name.Text;   
                    dtSource.Rows[0]["sig_designation2"]       = txtb_sig2_designation.Text   ;   

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;

                    if (msg.Substring(0, 1) == "X")
                    {
                        // FieldValidationColorChanged(true, "already-exist");
                        LblRequired3.Text = msg;
                        txtb_group_descr.BorderColor = Color.Red;
                        return;
                    }

                    if (dtSource_dtl.Rows.Count < 1 && 
                        ddl_special_group_main.SelectedValue != "99") // Other Custom Groups
                    {
                        FieldValidationColorChanged(true, "notification-message");
                    }
                    else if (txtb_group_descr.Text  == "")
                    {
                        FieldValidationColorChanged(true, "txtb_group_descr");
                        txtb_group_descr.Focus();
                    }
                    else
                    {

                        //************************************************************************************************
                        //  BEGIN - VJA - 10/19/2019 - Update Previous Grouping of the Specific Employee during Add Mode
                        //************************************************************************************************
                        
                        for (int x = 0; x < dtSource_dtl.Rows.Count; x++)
                        {
                            // Status 
                            // 0 = In-Active
                            // 1 - Active

                            if (dtSource_dtl.Rows[x]["emp_status"].ToString() == "1" || dtSource_dtl.Rows[x]["emp_status"].ToString() == "True")
                            {
                                // Ma In-Active and Specific nga Employee pag naa na syay Previous Group
                                // Dle nani niya agian nga Stored Procedure kung False iyang emp_status
                                // Update : 10/24/2019 - Add Params Special Group
                                DataTable dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_dtl_tbl_upd", "par_payroll_group_nbr", lbl_group_no.Text.ToString().Trim(), "par_empl_id", dtSource_dtl.Rows[x]["empl_id"].ToString(), "par_special_group", ddl_special_group_main.SelectedValue.ToString().Trim());
                            }

                        }
                        
                        //******************************************************************************
                        //  BEGIN - VJA - 10/19/2019 - Delete and Insert Method from Grouping Details
                        //******************************************************************************
                        dtSource_dtl.Columns.Remove("employee_name");
                        dtSource_dtl.Columns.Remove("flag_update_master");
                        dtSource_dtl.Columns.Remove("emp_status_descr");

                        if (saveRecord == MyCmn.CONST_ADD)
                        {
                            for (int x = 0; x < dtSource_dtl.Rows.Count; x++)
                            {
                                dtSource_dtl.Rows[x]["payroll_group_nbr"] = dtSource.Rows[0]["payroll_group_nbr"];
                            }
                            //RetrieveDataListGrid_dtl();
                        }
                        
                        string[] insert_empl_script = MyCmn.get_insertscript(dtSource_dtl).Split(';');
                        MyCmn.DeleteBackEndData(dtSource_dtl.TableName.ToString(), "WHERE payroll_group_nbr ='" + dtSource.Rows[0]["payroll_group_nbr"] + "'");
                        for (int x = 0; x < insert_empl_script.Length; x++)
                        {
                            string insert_script = "";
                            insert_script = insert_empl_script[x];
                            MyCmn.insertdata(insert_script);
                        }
                        //******************************************************************************
                        //  BEGIN - VJA- 06/18/2019 - SP for Update for Employee Master During Add/Edit
                        //******************************************************************************
                        if (ddl_special_group.SelectedValue == "01")
                        {
                            for (int x = 0; x < dtSource_dtl.Rows.Count; x++)
                            {
                                if (dtSource_dtl.Rows[x]["emp_status"].ToString() == "1" || dtSource_dtl.Rows[x]["emp_status"].ToString() == "True")
                                {
                                    // E Update ang Group Number sa Master to the Specific Employeee
                                    dt_result = MyCmn.RetrieveData("payrollemployeemaster_hdr_tbl_update", "par_empl_id", dtSource_dtl.Rows[x]["empl_id"].ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", lbl_group_no.Text.ToString(), "par_update_all", "", "par_payroll_group_nbr1", "");
                                }
                                // Ge Comment naku kay kung in-active . dle lang e update ang Master
                                //else
                                //{
                                //    // E Empty ang Group Number sa Master to the Specific Employeee
                                //    dt_result = MyCmn.RetrieveData("payrollemployeemaster_hdr_tbl_update", "par_empl_id", dtSource_dtl.Rows[x]["empl_id"].ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", "", "par_update_all", "", "par_payroll_group_nbr1", "");
                                //}
                            }
                        }

                        if (saveRecord == MyCmn.CONST_ADD)
                        {
                            DataRow nrow = dataListGrid.NewRow();
                            nrow["payroll_group_nbr"]   = dtSource.Rows[0]["payroll_group_nbr"];
                            nrow["employment_type"]     = ddl_empl_type.SelectedValue.ToString().Trim();
                            nrow["grouping_descr"]      = txtb_group_descr.Text.ToString().Trim();
                            nrow["department_code"]     = ddl_dep.SelectedValue.ToString().Trim();
                            nrow["sub_department_code"] = ddl_subdep.SelectedValue.ToString().Trim();
                            nrow["division_code"]       = ddl_division.SelectedValue.ToString().Trim();
                            nrow["section_code"]        = ddl_section.SelectedValue.ToString().Trim();
                            nrow["fund_code"]           = ddl_fund_charges.SelectedValue.ToString().Trim();
                            nrow["function_code"]       = ddl_function_code.SelectedValue.ToString().Trim();
                            nrow["special_group"]       = ddl_special_group.SelectedValue.ToString().Trim();
                            nrow["charge_to"]           = txtb_charge_to.Text.ToString().Trim();
                            nrow["sig_name"]            = txtb_sig_name.Text.ToString().Trim();
                            nrow["sig_designation"]     = txtb_sig_designation.Text.ToString().Trim();

                            nrow["dttm_created"]           = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            nrow["dttm_updated"]           = "1900-01-01";
                            nrow["created_by_user"]        = Session["ep_user_id"].ToString().Trim();
                            nrow["updated_by_user"]        = "";

                            nrow["sig_name2"]              = txtb_sig2_name.Text;
                            nrow["sig_designation2"]       = txtb_sig2_designation.Text   ;

                            if (ddl_special_group.SelectedValue == "01" ||
                                ddl_special_group.SelectedValue == "07" ||
                                ddl_special_group.SelectedValue == "05" ||       // Hazard, Subsistence and Laundry Pay
                                ddl_special_group.SelectedValue == "06")
                            {
                                nrow["department_name1"] = ddl_dep.SelectedItem.ToString().Trim();
                            }
                            else if (ddl_dep.SelectedValue == "")
                            {
                                nrow["department_name1"] = "";
                            }
                            else
                            {
                                nrow["department_name1"] = "";
                            }

                            dataListGrid.Rows.Add(nrow);
                            gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                            SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        }
                        if (saveRecord == MyCmn.CONST_EDIT)
                        {
                            string editExpression = "payroll_group_nbr = '" + lbl_group_no.Text.ToString().Trim() + "'";
                            DataRow[] row2Edit = dataListGrid.Select(editExpression);

                            row2Edit[0]["payroll_group_nbr"]        = lbl_group_no.Text.ToString().Trim();
                            row2Edit[0]["employment_type"]          = ddl_empl_type.SelectedValue.ToString().Trim();
                            row2Edit[0]["grouping_descr"]           = txtb_group_descr.Text.ToString().Trim();
                            row2Edit[0]["department_code"]          = ddl_dep.SelectedValue.ToString().Trim();
                            row2Edit[0]["sub_department_code"]      = ddl_subdep.SelectedValue.ToString().Trim();
                            row2Edit[0]["division_code"]            = ddl_division.SelectedValue.ToString().Trim();
                            row2Edit[0]["section_code"]             = ddl_section.SelectedValue.ToString().Trim();
                            row2Edit[0]["fund_code"]                = ddl_fund_charges.SelectedValue.ToString().Trim();
                            row2Edit[0]["function_code"]            = ddl_function_code.SelectedValue.ToString().Trim();
                            row2Edit[0]["special_group"]            = ddl_special_group.SelectedValue.ToString().Trim();
                            row2Edit[0]["charge_to"]                = txtb_charge_to.Text.ToString().Trim();
                            row2Edit[0]["sig_name"]                 = txtb_sig_name.Text.ToString().Trim();
                            row2Edit[0]["sig_designation"]          = txtb_sig_designation.Text.ToString().Trim();
                            
                            row2Edit[0]["dttm_created"]           = dtSource.Rows[0]["dttm_created"].ToString();
                            row2Edit[0]["dttm_updated"]           = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row2Edit[0]["created_by_user"]        = dtSource.Rows[0]["created_by_user"].ToString();
                            row2Edit[0]["updated_by_user"]        = Session["ep_user_id"].ToString().Trim();

                            row2Edit[0]["sig_name2"]              = txtb_sig2_name.Text;
                            row2Edit[0]["sig_designation2"]       = txtb_sig2_designation.Text   ;   

                            if (ddl_special_group.SelectedValue == "01" ||
                                ddl_special_group.SelectedValue == "07" ||
                                ddl_special_group.SelectedValue == "05" ||       // Hazard, Subsistence and Laundry Pay
                                ddl_special_group.SelectedValue == "06")
                            {
                                row2Edit[0]["department_name1"] = ddl_dep.SelectedItem.ToString().Trim();
                            }
                            else if (ddl_dep.SelectedValue == "")
                            {
                                row2Edit[0]["department_name1"] = "";
                            }
                            else
                            {
                                row2Edit[0]["department_name1"] = "";
                            }

                            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                            SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                        ViewState.Remove("AddEdit_Mode");
                        show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                    }
                    
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
            string searchExpression = "payroll_group_nbr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR grouping_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR department_name1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";
            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_group_nbr", typeof(System.String));
            //dtSource1.Columns.Add("payroll_group_nbr", typeof(System.String));
            dtSource1.Columns.Add("employment_type", typeof(System.String));
            dtSource1.Columns.Add("grouping_descr", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("department_code", typeof(System.String));
            dtSource1.Columns.Add("department_name1", typeof(System.String));
            dtSource1.Columns.Add("sub_department_code", typeof(System.String));
            dtSource1.Columns.Add("division_code", typeof(System.String));
            dtSource1.Columns.Add("section_code", typeof(System.String));
            dtSource1.Columns.Add("special_group", typeof(System.String));
            dtSource1.Columns.Add("charge_to", typeof(System.String));
            dtSource1.Columns.Add("sig_name", typeof(System.String));
            dtSource1.Columns.Add("sig_designation", typeof(System.String));
            dtSource1.Columns.Add("sig_name2", typeof(System.String));
            dtSource1.Columns.Add("sig_designation2", typeof(System.String));
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
        //  BEGIN - VJA- 06/11/2019 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 06/11/2019 - Check if Object already contains value  
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
        //  BEGIN - VJA- 06/11/2019 - Objects data Validation - For Details
        //*************************************************************************
        private bool IsDataValidated2() {
            bool validatedSaved = true;
            if (ddl_dep.SelectedValue == "" && ddl_special_group.SelectedValue == "01" && ddl_special_group.SelectedValue == "03")
            {
                FieldValidationColorChanged(true, "ddl_dep");
                ddl_dep.Focus();
                validatedSaved = false;
            }
            else if (ddl_empl_id.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }

            return validatedSaved;
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Objects data Validation  - For HEader
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            if (ddl_special_group.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_special_group");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopTab1", "click_information();", true);
                ddl_special_group.Focus();
                validatedSaved = false;
            }
            else if (ddl_dep.SelectedValue == "" && 
                     (ddl_special_group.SelectedValue == "01" || // Common Groupings
                     //ddl_special_group.SelectedValue == "06" || // Overtime
                     ddl_special_group.SelectedValue == "07")) // Loyalty Bonus
            {
                FieldValidationColorChanged(true, "ddl_dep");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopTab1", "click_information();", true);

                ddl_dep.Focus();
                validatedSaved = false;
            }
            
            else if (ddl_empl_type.SelectedValue == "CE" || ddl_empl_type.SelectedValue == "JO" ) {
                if (ddl_fund_charges.SelectedValue.ToString().Trim() == "" &
                    ddl_special_group.SelectedValue == "01" ) // Common Groupings
                    
                {
                    FieldValidationColorChanged(true, "ddl_fund_charges");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopTab1", "click_information();", true);
                    validatedSaved = false;
                }

                if (ddl_function_code.SelectedValue.ToString().Trim() == "" &&
                    ddl_special_group.SelectedValue == "01" ) // Common Groupings
                    
                {
                    FieldValidationColorChanged(true, "ddl_function_code");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopTab1", "click_information();", true);
                    validatedSaved = false;
                }
            }
            //else if (txtb_sig_name.Text == "" && ddl_special_group.SelectedValue != "01")
            //{
            //    FieldValidationColorChanged(true, "txtb_sig_name");
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopTab1", "click_information();", true);
            //    txtb_sig_name.Focus();
            //    validatedSaved = false;
            //}
            //else if (txtb_sig_designation.Text == "" && ddl_special_group.SelectedValue != "01")
            //{
            //    FieldValidationColorChanged(true, "txtb_sig_designation");
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopTab1", "click_information();", true);
            //    txtb_sig_designation.Focus();
            //    validatedSaved = false;
            //}
            else if (ddl_empl_id.SelectedValue == "" && 
                ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD &&
                ddl_special_group_main.SelectedValue != "99") // Other Custom Group
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopTab2", "click_addemployee();", true);

                ddl_empl_id.Focus();
                validatedSaved = false;
            }
            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_dep":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_dep.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_empl_id":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired2.Text = "already exist!";
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "notification-message":
                        {
                            msg_header.InnerText = "ADD ATLEAST ONE EMPLOYEE!";
                            msg_icon.Attributes.Add("class", "fa fa-exclamation-circle fa-5x text-danger");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openNotification();", true);
                            break;
                        }
                    case "txtb_group_descr":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_group_descr.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_fund_charges":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            ddl_fund_charges.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_function_code":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            ddl_function_code.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_special_group":
                        {
                            LblRequired11.Text = MyCmn.CONST_RQDFLD;
                            ddl_special_group.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_sig_name":
                        {
                            LblRequired15.Text = MyCmn.CONST_RQDFLD;
                            txtb_sig_name.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_sig_designation":
                        {
                            LblRequired16.Text = MyCmn.CONST_RQDFLD;
                            txtb_sig_designation.BorderColor = Color.Red;
                            break;
                        }
                    case "exclude_period_from":
                        {
                            exclude_period_from_req.Text = MyCmn.CONST_RQDFLD;
                            exclude_period_from.BorderColor = Color.Red;
                            break;
                        }
                    case "exclude_period_to":
                        {
                            exclude_period_to_req.Text = MyCmn.CONST_RQDFLD;
                            exclude_period_to.BorderColor = Color.Red;
                            break;
                        }
                    case "exclude_reason":
                        {
                            exclude_reason_req.Text = MyCmn.CONST_RQDFLD;
                            exclude_reason.BorderColor = Color.Red;
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
                            LblRequired11.Text = "";
                            LblRequired15.Text = "";
                            LblRequired16.Text = "";
                            exclude_period_from_req.Text = "";
                            exclude_period_to_req.Text = "";
                            exclude_reason_req.Text = "";

                            ddl_empl_id.BorderColor = Color.LightGray;
                            ddl_dep.BorderColor = Color.LightGray;
                            txtb_group_descr.BorderColor = Color.LightGray;
                            ddl_function_code.BorderColor = Color.LightGray;
                            ddl_fund_charges.BorderColor = Color.LightGray;
                            ddl_special_group.BorderColor = Color.LightGray;
                            txtb_sig_name.BorderColor = Color.LightGray;
                            txtb_sig_designation.BorderColor = Color.LightGray;

                            exclude_period_from.BorderColor = Color.LightGray;
                            exclude_period_to.BorderColor = Color.LightGray;
                            exclude_reason.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Trigger When Select Employment Type
        //*************************************************************************
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "")
            {
                RetrieveDataListGrid();
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveEmployeename();
            UpdatePanel10.Update();
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Trigger When Select Department
        //*************************************************************************
        protected void ddl_dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_dep.SelectedValue != "" )
            {
                if (dtSource_dtl.Rows.Count > 0 && ddl_special_group.SelectedValue != "99")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop6", "openNotification1();", true);
                    
                }
            }
            else
            {
                // FieldValidationColorChanged(true, "ddl_dep");
            }
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveEmployeename();
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Trigger When Select Sub-Department
        //*************************************************************************
        protected void ddl_subdep_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveEmployeename();
            ddl_subdep.Focus();
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Trigger When Select Division
        //*************************************************************************
        protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingSection();
            RetrieveEmployeename();
            ddl_division.Focus();
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Trigger When Select Section
        //*************************************************************************
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveEmployeename();
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Trigger When Select Payroll Year
        //*************************************************************************
        protected void ddl_year_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "")
            {
                RetrieveDataListGrid();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Trigger When Click Add From Select Employee
        //*************************************************************************
        protected void btn_add_empl_Click(object sender, EventArgs e)
        {
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            FieldValidationColorChanged(false, "ALL");
            if (IsDataValidated2())
            {
                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    try
                    {
                        //DataRow nrow2 = dtSource_for_employeegrouping_details.NewRow();
                        //nrow2["payroll_group_nbr"]  = lbl_group_no.Text.ToString().Trim();
                        //nrow2["empl_id"]            = ddl_empl_id.SelectedValue.ToString().Trim();
                        //nrow2["emp_status"]         = "1";
                        //nrow2["action"] = 1;
                        //nrow2["retrieve"] = false;
                        //dtSource_for_employeegrouping_details.Rows.Add(nrow2);
                        
                        DataRow nrow1 = dtSource_dtl.NewRow();
                        nrow1["payroll_group_nbr"]  = lbl_group_no.Text.ToString().Trim();
                        nrow1["empl_id"]            = ddl_empl_id.SelectedValue.ToString().Trim();
                        nrow1["employee_name"]      = ddl_empl_id.SelectedItem.ToString().Trim();
                        nrow1["emp_status"]         = 1;
                        nrow1["emp_status_descr"]   = "Active";
                        nrow1["flag_update_master"] = "Y";
                        nrow1["action"] = 1;
                        nrow1["retrieve"] = false;
                        dtSource_dtl.Rows.Add(nrow1);

                        MyCmn.Sort(gv_details, dtSource_dtl, "employee_name", "ASC");
                        updatepanel_details.Update();
                        GetTheGroupDescription();
                    }
                    catch (Exception)
                    {
                        string editExpression = "payroll_group_nbr = '" + lbl_group_no.Text.ToString().Trim() + "' AND empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'";
                        DataRow[] row2Edit = dtSource_dtl.Select(editExpression);

                        if (row2Edit[0]["emp_status"].ToString() == "1" ||
                            row2Edit[0]["emp_status"].ToString() == "True" ||
                            row2Edit[0]["emp_status"].ToString() == "true" ||
                            row2Edit[0]["flag_update_master"].ToString() == "Y")
                        {
                            FieldValidationColorChanged(true, "already-exist");
                        }
                        else
                        {
                            row2Edit[0]["emp_status"] = 1;
                            row2Edit[0]["emp_status_descr"] = "Active";
                            row2Edit[0]["flag_update_master"] = "Y";

                            MyCmn.Sort(gv_details, dtSource_dtl, "employee_name", "ASC");
                            show_entries_dtl.Text = "Entries: <b>" + (gv_details.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_details.PageCount + "</strong>";
                            updatepanel_details.Update();
                            GetTheGroupDescription();
                            
                        }
                    }
                }
            }


            //string saveRecord = ViewState["AddEdit_Mode"].ToString();
            //FieldValidationColorChanged(false, "ALL");
            //if (IsDataValidated2())
            //{
            //    if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
            //    {
            //        try
            //        {
            //            //lnk_btn_keepit.CommandArgument = ddl_dep.SelectedValue.ToString().Trim();
            //            DataRow[] select_row = dtSource_dtl.Select("empl_id='"+ ddl_empl_id.SelectedValue.ToString().Trim() + "' AND flag_update_master='N'");
            //            if (select_row.Length > 0)
            //            {
            //                select_row[0]["flag_update_master"] = "Y";
            //            }
            //            else if (dtSource_dtl.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "' AND flag_update_master='Y'").Length < 1)
            //            {
            //                DataRow nrow1 = dtSource_dtl.NewRow();
            //                nrow1["payroll_group_nbr"]      = lbl_group_no.Text.ToString().Trim();
            //                nrow1["empl_id"]                = ddl_empl_id.SelectedValue.ToString().Trim();
            //                nrow1["employee_name"]          = ddl_empl_id.SelectedItem.Text.ToString().Trim();
            //                nrow1["flag_update_master"]     = "Y";
            //                dtSource_dtl.Rows.Add(nrow1);

            //                if (ddl_special_group.SelectedValue != "01")
            //                {
            //                    DataRow nrow2 = dtSource_for_employeegrouping_details.NewRow();
            //                    nrow2["payroll_group_nbr"]  = lbl_group_no.Text.ToString().Trim();
            //                    nrow2["empl_id"]            = ddl_empl_id.SelectedValue.ToString().Trim();
            //                    nrow2["action"]             = 1;
            //                    nrow2["retrieve"]           = false;
            //                    dtSource_for_employeegrouping_details.Rows.Add(nrow2);
            //                }
            //            }
            //            else
            //            {
            //                FieldValidationColorChanged(true, "already-exist");
            //                return;
            //            }

            //            DataRow[] rows = dtSource_dtl.Select();
            //            dtSource_dtl_for_display.Clear();
            //            if (rows.Length > 0)
            //            {
            //                foreach (DataRow row in rows)
            //                {   // IF flag = "N" = Mao ning New Data            // IF flag = "S" = Mao ning Karaan nga sa SP  |  Update Date : 06/27/2019
            //                    if (row["flag_update_master"].ToString() == "Y" || row["flag_update_master"].ToString() == "S")
            //                    {
            //                        dtSource_dtl_for_display.ImportRow(row);
            //                    }
            //                }
            //            }

            //            MyCmn.Sort(gv_details, dtSource_dtl_for_display, "employee_name", "ASC");
            //            updatepanel_details.Update();
            //            GetTheGroupDescription();
            //        }
            //        catch (Exception)
            //        {
            //            FieldValidationColorChanged(true, "already-exist");
            //        }
            //    }
            //}

        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Trigger When Click Delete From Details
        //*************************************************************************
        protected void lnkbtn_delete_details_Command(object sender, CommandEventArgs e)
        {

            string commandArgs = e.CommandArgument.ToString();
            string empl_id1 = commandArgs.ToString();
            string deleteExpression = "empl_id = '" + empl_id1 + "' AND payroll_group_nbr = '" + lbl_group_no.Text.ToString().Trim() + "'";

            string editExpression = "payroll_group_nbr = '" + lbl_group_no.Text.ToString().Trim() + "'";
            DataRow[] row2Edit = dtSource_dtl.Select(editExpression);


            //foreach (GridViewRow row in gv_details.Rows)
            //{
            //    ImageButton DropStatus = row.FindControl("lnkbtn_delete_details") as ImageButton;
                
            //    if (row2Edit[0]["emp_status"].ToString().Trim() == "True" ||
            //        row2Edit[0]["emp_status"].ToString().Trim() == "1")
            //    {
            //        row2Edit[0]["emp_status"] = "False";
            //        row2Edit[0]["emp_status_descr"] = "In-Active";
            //        DropStatus.CssClass = "btn btn-danger action";
            //    }
            //    else
            //    {
            //        row2Edit[0]["emp_status"] = "True";
            //        row2Edit[0]["emp_status_descr"] = "Active";
            //        DropStatus.CssClass = "btn btn-success action";
            //    }
            //}

            DataRow[] rowDelete2 = dtSource_dtl.Select(deleteExpression);
            dtSource_dtl.Rows.Remove(rowDelete2[0]);
            dtSource_dtl.AcceptChanges();


            //if (ddl_special_group.SelectedValue != "01")
            //{
            //    DataRow[] rowDelete = dtSource_for_employeegrouping_details.Select(deleteExpression);
            //    dtSource_for_employeegrouping_details.Rows.Remove(rowDelete[0]);
            //    dtSource_for_employeegrouping_details.AcceptChanges();

            //    DataRow[] rowDelete2 = dtSource_dtl.Select(deleteExpression);
            //    dtSource_dtl.Rows.Remove(rowDelete2[0]);
            //    dtSource_dtl.AcceptChanges();

            //    DataRow[] rowDelete3 = dtSource_dtl_for_display.Select(deleteExpression);
            //    dtSource_dtl_for_display.Rows.Remove(rowDelete3[0]);
            //    dtSource_dtl_for_display.AcceptChanges();
            //}
            //else
            //{
            //    DataRow[] row2Delete = dtSource_dtl.Select(deleteExpression);
            //    row2Delete[0]["flag_update_master"] = "N";

            //    DataRow[] rows = dtSource_dtl.Select("flag_update_master = 'Y'");
            //    InitializeTable_dtl_for_display();
            //    AddPrimaryKeys_dtl_for_display();

            //    dtSource_dtl_for_display.Clear();
            //    if (rows.Length > 0)
            //    {
            //        foreach (DataRow row in rows)
            //        {
            //            dtSource_dtl_for_display.ImportRow(row);
            //        }
            //    }
            //}

            MyCmn.Sort(gv_details, dtSource_dtl, "employee_name","ASC");
            updatepanel_details.Update();

            //Get the First row of the Grid
            GetTheGroupDescription();

            updatepanel_details.Update();
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Get The Group Description 
        //*************************************************************************
        private void GetTheGroupDescription()
        {
            gv_details.PageIndex = 0;
            MyCmn.Sort(gv_details, dtSource_dtl, "employee_name", Session["SortOrder"].ToString());

            // VJA - If one Employee - No ET. AL. on Description
            if (dtSource_dtl.Rows.Count == 1)
            {
                Label label = gv_details.Rows[0].Cells[1].FindControl("lbl_employee_name") as Label;
                txtb_group_descr.Text = label.Text;
            }
            else if (dtSource_dtl.Rows.Count > 0)
            {
                Label label = gv_details.Rows[0].Cells[1].FindControl("lbl_employee_name") as Label;
                txtb_group_descr.Text = label.Text+ " ET, AL";
            }
            else
            {
                txtb_group_descr.Text = "";
            }
            
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 -Trigger When Select Payroll Year
        //*************************************************************************
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Search Expresion on Detials
        //*************************************************************************
        protected void txtb_search_details_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search_details.Text.Trim() + "%' " +
                "OR employee_name LIKE '%" + txtb_search_details.Text.Trim() + "%'" +
                "OR emp_status_descr LIKE '%" + txtb_search_details.Text.Trim() + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("emp_status", typeof(System.String));
            dtSource1.Columns.Add("emp_status_descr", typeof(System.String));
            DataRow[] rows = dtSource_dtl.Select(searchExpression);
            dtSource1.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource1.ImportRow(row);
                }
            }

            gv_details.DataSource = dtSource1;
            gv_details.DataBind();
            updatepanel_details.Update();
            txtb_search_details.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search_details.Focus();
            
            show_entries_dtl.Text = "Entries: <b>" + (gv_details.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_details.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - DropDown Show Number of Detail on Grid
        //*************************************************************************
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gv_details.PageSize = Convert.ToInt32(DropDownList1.Text);
            CommonCode.GridViewBind(ref this.gv_details, dtSource_dtl);
            show_entries_dtl.Text = "Entries: <b>" + (gv_details.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_details.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Sort when Click the Header of the Column
        //*************************************************************************
        protected void gv_details_Sorting(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(gv_details, dtSource_dtl, e.SortExpression, sortingDirection);
            show_entries_dtl.Text = "Entries: <b>" + (gv_details.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_details.PageCount + "</strong>";

        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Pagination of The Detials
        //*************************************************************************
        protected void gv_details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_details.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_details, dtSource_dtl, "employee_name", Session["SortOrder"].ToString());
            show_entries_dtl.Text = "Entries: <b>" + (gv_details.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_details.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - IF YES -  Triggers Click Yes/No When Change the Department
        //*************************************************************************
        protected void lnk_btn_retrieve_Click(object sender, EventArgs e)
        {
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            
           //InitializeTable_dtl_for_display();
           //AddPrimaryKeys_dtl_for_display();
            
            txtb_group_descr.Text = "";

            CommonCode.GridViewBind(ref this.gv_details, dtSource_dtl);
            updatepanel_details.Update();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - IF NO, Keep It! -  Triggers Click Yes/No When Change the Department
        //*************************************************************************
        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ddl_dep.SelectedValue = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Triggers When Select Special Group on Main Page
        //*************************************************************************
        protected void ddl_special_group_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "")
            {

                RetrieveDataListGrid();
                RetrieveEmployeename();
                ToogleSpecialGroup();
                btnAdd.Visible = true;
                
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveGroupCode();
            UpdatePanel10.Update();
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Toogle The Fields when Select Special Groupings
        //*************************************************************************
        private void ToogleSpecialGroup()
        {
            if (ddl_special_group_main.SelectedValue == "01" ||       // Common Groupings
                ddl_special_group_main.SelectedValue == "02" ||       // Communication Expense
                ddl_special_group_main.SelectedValue == "03" ||       // RATA and Quarterly Allowance
                ddl_special_group_main.SelectedValue == "05" ||       // Hazard, Subsistence and Laundry Pay
                ddl_special_group_main.SelectedValue == "06" ||       // Overtime Pay
                ddl_special_group_main.SelectedValue == "07" ||       // Loyalty Bonus
                ddl_special_group_main.SelectedValue == "99" )        // Other Payroll

            {
                ddl_dep.Enabled                 = true;
                ddl_subdep.Enabled              = true;
                ddl_division.Enabled            = true;
                ddl_section.Enabled             = true;
                ddl_function_code.Enabled       = true;
                ddl_fund_charges.Enabled        = true;
                id_employee.Visible             = true;
            }
            else
            {
                ddl_dep.Enabled                 = false;
                ddl_subdep.Enabled              = false;
                ddl_division.Enabled            = false;
                ddl_section.Enabled             = false;
                ddl_function_code.Enabled       = false;
                ddl_fund_charges.Enabled        = false;
                id_employee.Visible             = false;

                ddl_dep.SelectedValue           = "";
                ddl_subdep.SelectedValue        = "";
                ddl_division.SelectedValue      = "";
                ddl_section.SelectedValue       = "";
                // ddl_function_code.SelectedValue = "";
                // ddl_fund_charges.SelectedValue  = "";

            }
        }

        protected void lbkbtn_group_dtl_Command(object sender, CommandEventArgs e)
        {
            string[] grp_data = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "payroll_group_nbr = '" + grp_data[0].ToString().Trim() + "' AND empl_id = '" + grp_data[1].ToString().Trim() + "'";
            DataRow[] row2Edit = dtSource_dtl.Select(editExpression);

            DataTable chk = new DataTable();
            string query = "SELECT * FROM payrollemployeegroupings_dtl_excludes_tbl WHERE payroll_group_nbr = '" + grp_data[0].ToString().Trim() + "' AND empl_id = '" + grp_data[1].ToString().Trim() + "' ORDER BY created_dttm DESC";
            chk = MyCmn.GetDatatable(query);

            employee_name.Text                      = row2Edit[0]["employee_name"].ToString().Trim();
            empl_id.Text                            = row2Edit[0]["empl_id"].ToString().Trim();
            include_exclude_status.SelectedValue    = (row2Edit[0]["emp_status"].ToString().Trim() == "True" || row2Edit[0]["emp_status"].ToString().Trim() == "true" ?  "1":"0");
            exclude_period_from.Text                = "";
            exclude_period_to.Text                  = "";
            exclude_reason.Text                     = "";

            if (chk.Rows.Count > 0)
            {
                employee_name.Text                      = chk.Rows[0]["employee_name"].ToString().Trim();
                empl_id.Text                            = chk.Rows[0]["empl_id"].ToString().Trim();
                exclude_period_from.Text                = DateTime.Parse(chk.Rows[0]["exclude_date_from"].ToString().Trim()).ToString("yyyy-MM-dd");
                exclude_period_to.Text                  = DateTime.Parse(chk.Rows[0]["exclude_date_to"].ToString().Trim()).ToString("yyyy-MM-dd");
                exclude_reason.Text                     = chk.Rows[0]["exclude_reason"].ToString().Trim();
                include_exclude_status.SelectedValue    = chk.Rows[0]["emp_status"].ToString().Trim();
            }
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal_Exclude", "openModal_Exclude();", true);
        }
        
        protected void btn_save_exclue_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            if (IsDataValidated_Exclude())
            {
                string insert_script = "INSERT INTO payrollemployeegroupings_dtl_excludes_tbl VALUES('" + lbl_group_no.Text.ToString().Trim() + "','"+ empl_id.Text.Replace("'", "''") + "','"+ employee_name.Text.Replace("'", "''") + "','"+ exclude_period_from.Text.Replace("'", "''") + "','"+ exclude_period_to.Text.Replace("'", "''") + "','"+ exclude_reason.Text.Replace("'", "''") + "','"+ include_exclude_status.SelectedValue.ToString().Replace("'", "''") + "','"+DateTime.Now.ToString()+"','"+ Session["ep_user_id"] .ToString().Trim()+ "')";
                MyCmn.InsertToTable(insert_script);

                string update_script = "UPDATE payrollemployeegroupings_dtl_tbl SET emp_status = '"+ include_exclude_status.SelectedValue.ToString().Trim() + "' WHERE payroll_group_nbr = '" + lbl_group_no.Text.ToString().Trim() + "' AND empl_id = '" + empl_id.Text.ToString().Trim() + "'";
                MyCmn.UpdateToTable(update_script);

                string editExpression = "payroll_group_nbr = '" + lbl_group_no.Text.ToString().Trim() + "' AND empl_id = '"+ empl_id.Text .ToString().Trim()+ "'";
                DataRow[] row2Edit = dtSource_dtl.Select(editExpression);
            
                row2Edit[0]["emp_status"]         = (include_exclude_status.SelectedValue.ToString().Trim() == "1"? 1:0 );
                row2Edit[0]["emp_status_descr"]   = include_exclude_status.SelectedItem.ToString().Trim();

                MyCmn.Sort(gv_details, dtSource_dtl, "employee_name", "ASC");
                updatepanel_details.Update();

                MyCmn.Sort(gv_details, dtSource_dtl, "employee_name", "ASC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal_asd", "closeModal_Exclude();", true);
            }
        }

        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Objects data Validation  - For HEader
        //*************************************************************************
        private bool IsDataValidated_Exclude()  
        {
            bool validatedSaved = true;
            if (exclude_period_from.Text == "")
            {
                FieldValidationColorChanged(true, "exclude_period_from");
                exclude_period_from.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(exclude_period_from.Text) == false)
            {
                exclude_period_from_req.Text    = "Invalid Date!";
                exclude_period_from.BorderColor = Color.Red;
                exclude_period_from.Focus();
                validatedSaved = false;
            }
            if (exclude_period_to.Text == "")
            {
                FieldValidationColorChanged(true, "exclude_period_to");
                exclude_period_to.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(exclude_period_to.Text) == false)
            {
                exclude_period_to_req.Text    = "Invalid Date!";
                exclude_period_to.BorderColor = Color.Red;
                exclude_period_to.Focus();
                validatedSaved = false;
            }
            if (exclude_reason.Text == "")
            {
                FieldValidationColorChanged(true, "exclude_reason");
                exclude_reason.Focus();
                validatedSaved = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
            return validatedSaved;
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/11/2019 - END OF CODE
        //*************************************************************************
    }
}