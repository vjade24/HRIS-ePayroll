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
using System.Web.Services;
using System.Xml;
using Newtonsoft.Json;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HRIS_ePayroll.View.cPayRegistry
{
    public partial class cPayRegistry : System.Web.UI.Page
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
        DataTable dtSource_hdr
        {
            get
            {
                if ((DataTable)ViewState["dtSource_hdr"] == null) return null;
                return (DataTable)ViewState["dtSource_hdr"];
            }
            set
            {
                ViewState["dtSource_hdr"] = value;
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

        DataTable dtSource_defult_setup
        {
            get
            {
                if ((DataTable)ViewState["dtSource_defult_setup"] == null) return null;
                return (DataTable)ViewState["dtSource_defult_setup"];
            }
            set
            {
                ViewState["dtSource_defult_setup"] = value;
            }
        }

        DataTable dtSourse_for_template // For Report File
        {
            get
            {
                if ((DataTable)ViewState["dtSourse_for_template"] == null) return null;
                return (DataTable)ViewState["dtSourse_for_template"];
            }
            set
            {
                ViewState["dtSourse_for_template"] = value;
            }
        }

        DataTable dt_payroll
        {
            get
            {
                if ((DataTable)ViewState["dt_payroll"] == null) return null;
                return (DataTable)ViewState["dt_payroll"];
            }
            set
            {
                ViewState["dt_payroll"] = value;
            }
        }
        
        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Public Variable used in Add/Edit Mode
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
                    Session["SortField"] = "payroll_registry_descr";
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

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["page_allow_view"] = Master.allow_view;
                if (Master.allow_view == "1")
                {
                    ViewState["page_allow_add"]             = 0;
                    ViewState["page_allow_delete"]          = 0;
                    ViewState["page_allow_edit"]            = 0;
                    ViewState["page_allow_edit_history"]    = 0;
                    ViewState["page_allow_print"]           = 0;

                    ViewState["page_allow_receive"]         = 0;
                    ViewState["page_allow_audit"]           = 0;
                    ViewState["page_allow_post"]            = 0;
                }   
                else
                {
                    ViewState["page_allow_add"]             = Master.allow_add;
                    ViewState["page_allow_delete"]          = Master.allow_delete;
                    ViewState["page_allow_edit"]            = Master.allow_edit;
                    ViewState["page_allow_edit_history"]    = Master.allow_edit_history;
                    ViewState["page_allow_print"]           = Master.allow_print;

                    Session["page_allow_print_from_registry"]           = Master.allow_print;
                    Session["page_allow_edit_history_from_registry"]    = Master.allow_edit_history;
                    Session["page_allow_edit_from_registry"]            = Master.allow_edit;
                    Session["page_allow_add_from_registry"]             = Master.allow_add;
                    Session["page_allow_delete_from_registry"]          = Master.allow_delete;

                    ViewState["page_allow_receive"]                     = 1;
                    ViewState["page_allow_audit"]                       = 1;
                    ViewState["page_allow_post"]                        = 1;

                }

                if (Session["PreviousValuesonPage_cPayRegistry"] == null)
                    Session["PreviousValuesonPage_cPayRegistry"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry"].ToString() != string.Empty)
                {
                    RetrieveYear();
                    string[] prevValues                 = Session["PreviousValuesonPage_cPayRegistry"].ToString().Split(new char[] { ',' });
                    ddl_year.SelectedValue              = prevValues[0].ToString();
                    ddl_month.SelectedValue             = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue         = prevValues[2].ToString();
                    //RetriveGroupings();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue  = prevValues[3].ToString();
                    RetrieveDataListGrid();
                    gv_dataListGrid.PageIndex           = int.Parse(prevValues[4].ToString());
                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                    up_dataListGrid.Update();
                    btnAdd.Visible                      = true;
                    DropDownListID.SelectedValue        = prevValues[5].ToString();
                    txtb_search.Text                    = prevValues[8].ToString();
                    SearchData(prevValues[8].ToString());
                }
            }
        }

        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            // ddl_month.SelectedValue = DateTime.Now.Month.ToString().Trim().PadLeft(2, '0');
            RetrieveYear();
            RetrieveDataListGrid();

            //RetriveGroupings();
            RetriveTemplate();
            RetriveEmploymentType();
            RetrieveBindingDep();
            RetrieveBindingFunction();
            //RetrieveEmployee();

            //Retrieve the last row of the Group when Add Header
            //RetrieveLastRegistryNumber();

            btnAdd.Visible = false;
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveYear()
        {
            ddl_year.Items.Clear();
            //ddl_year_modal.Items.Clear();
            int years = Convert.ToInt32(DateTime.Now.Year);
            int prev_year = years - 5;
            for (int x = 0; x < 12; x++)
            {
                ListItem li2 = new ListItem(prev_year.ToString(), prev_year.ToString());
                ddl_year.Items.Insert(x, li2);
                //ddl_year_modal.Items.Insert(x, li2);
                if (prev_year == years)
                {
                    ListItem li3 = new ListItem((years + 1).ToString(), (years + 1).ToString());
                    ddl_year.Items.Insert(x + 1, li3);
                    //ddl_year_modal.Items.Insert(x + 1, li3);
                    ddl_year.SelectedValue = years.ToString();
                    //ddl_year_modal.SelectedValue = years.ToString();
                    break;
                }
                prev_year = prev_year + 1;
            }
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveGroupings()
        {
            /*    
                  01  -  Common Groupings
                  02  -  Communication Expense
                  03  -  RATA and Quarterly Allowance
                  04  -  Monetization
                  05  -  Hazard, Subsistence and Laundry Pay
                  06  -  Overtime Pay
                  07  -  Loyalty Bonus
                  99  -  Other Custom Groups
            */
            string special_group = "";
            ddl_payroll_group.Items.Clear();
            if (ddl_payroll_template.SelectedValue == "043" ||   // Communication Expense Allowance - RE
                ddl_payroll_template.SelectedValue == "024" ||   // Communication Expense Allowance - CE
                ddl_payroll_template.SelectedValue == "063")     // Communication Expense Allowance - JO - Newly Added 2020-10-08
            {
                special_group = "02";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "023" || // RATA Payroll 
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "981")
            {
                special_group = "03";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "025" || // Monetization - RE
                    ddl_payroll_template.SelectedValue.ToString().Trim()  == "044")   // Monetization - CE
            {
                special_group = "04";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "021" || // Hazard, Subsistence and Laundry Pay - RE
                    ddl_payroll_template.SelectedValue.ToString().Trim()  == "041")   // Hazard, Subsistence and Laundry Pay - CE
            {
                special_group = "05";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "022" || // Overtime Payroll - RE
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "042" || // Overtime Payroll - CE
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "061")   // Overtime Payroll - JO
            {
                special_group = "06";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "029" || // Loyalty Bonus - RE
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "048")   // Loyalty Bonus - CE
            {
                special_group = "07";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "" || 
                    ddl_payroll_template.SelectedValue.ToString().Trim().Substring(0,1) == "9") // Other Payroll Template Starts With 9 (Other Payroll)
            {
                special_group = "99";
            }
            else
            {
                special_group = "01";
            }

            DataTable dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_hdr_tbl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_special_group", special_group);

            // *************************************************************************************************************
            // ***** VJA : 05/08/2020 : Override the Stored Procedure if the Template Code are Other Custom Setup Payroll
            // *************************************************************************************************************
            if (ddl_payroll_template.SelectedValue.ToString().Trim() == "981")
            {

            }
            else
            {
                if (ddl_payroll_template.SelectedValue.ToString().Trim() == "" ||
                    ddl_payroll_template.SelectedValue.ToString().Trim().Substring(0, 1) == "9")
                {
                    dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_hdr_tbl_list1", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
                }
            }

            ddl_payroll_group.DataSource = dt;
            ddl_payroll_group.DataValueField    = "payroll_group_nbr";
            ddl_payroll_group.DataTextField     = "grouping_descr";
            ddl_payroll_group.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_group.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveTemplate()
        {
            ddl_payroll_template.Items.Clear();
            //ddl_payrolltemplate_report.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list6", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
            
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        //private void RetriveTemplate_Modal()
        //{
            //ddl_payrolltemplate_report.Items.Clear();
            //DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list6", "par_employment_type", ddl_empl_type_modal.SelectedValue.ToString().Trim());
            
            //ddl_payrolltemplate_report.DataSource = dt;
            //ddl_payrolltemplate_report.DataValueField = "payrolltemplate_code";
            //ddl_payrolltemplate_report.DataTextField = "payrolltemplate_descr";
            //ddl_payrolltemplate_report.DataBind();
            //ListItem li2 = new ListItem("-- All --", "");
            //ddl_payrolltemplate_report.Items.Insert(0, li2);
        //}
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetriveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            //ddl_empl_type_modal.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");

            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_type.Items.Insert(0, li);

            //ddl_empl_type_modal.DataSource = dt;
            //ddl_empl_type_modal.DataValueField = "employment_type";
            //ddl_empl_type_modal.DataTextField = "employmenttype_description";
            //ddl_empl_type_modal.DataBind();
            //ListItem li2 = new ListItem("-- Select Here --", "");
            //ddl_empl_type_modal.Items.Insert(0, li2);
        }

        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve Related Tempate COde/Type
        //*************************************************************************
        private void RetrieveRelatedTemplate()
        {
            ddl_select_report.Items.Clear();
            dtSourse_for_template = MyCmn.RetrieveData("sp_payrollregistry_template_combolist", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            ddl_select_report.DataSource = dtSourse_for_template;
            ddl_select_report.DataValueField = "payrolltemplate_code";
            ddl_select_report.DataTextField = "payrolltemplate_descr";
            ddl_select_report.DataBind();
            ListItem li = new ListItem("Generate Report to Printer", "X");
            ddl_select_report.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve Registry Number
        //*************************************************************************
        private void RetrieveLastRegistryNumber()
        {
            DataTable dt = MyCmn.RetrieveData("sp_get_next_registry_no", "par_payroll_year", ddl_year.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                txtb_registry_no.Text = dt.Rows[0]["next_registry_no"].ToString();
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_hdr_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();

            // For Detials Initialized, Add Primary Keys, Add new Row
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            RetriveGroupings();

            RetrieveLastRegistryNumber();
            lbl_if_dateposted_yes.Text = "";
            btnSave.Visible = true;
            txtb_allotment_code.Text = "100";

            txtb_registry_descr.Text = ddl_payroll_group.SelectedItem.ToString().Trim();
            ddl_post_status.SelectedValue = "N"; // Not Posted
            ddl_payroll_group.Enabled = true;
            //ddl_empl_id.Enabled = true;
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");
            get_default();
            ToogleButtonTextboxes(true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Get The Default from SP sp_payrollregistry_get_dates
        //*************************************************************************
        protected void get_default()
        {
            dtSource_defult_setup = MyCmn.RetrieveData("sp_payrollregistry_get_dates", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_year", ddl_year.SelectedValue.ToString(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim());
            if (dtSource_defult_setup != null && dtSource_defult_setup.Rows.Count > 0)
            {
                txtb_period_from.Text = dtSource_defult_setup.Rows[0]["payroll_period_from"].ToString();
                txtb_period_to.Text = dtSource_defult_setup.Rows[0]["payroll_period_to"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                UpdateDateFrom.Update();
                UpdateDateTo.Update();

                /*>>>>>>>>> IF TEMPLATE BELONGS HERE <<<<<<<<<*/
                if (ddl_payroll_template.SelectedValue.ToString().Trim() == "008" || // Casual - Monthly Payroll  
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "023" || // Regular - RATA Payroll
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "009" || // JO - Monthly Payroll
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "010" || // JO - 1st Quincena Payroll
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "011")   // JO - 2nd Quincena Payroll
                {
                    txtb_no_works_1st.Text = dtSource_defult_setup.Rows[0]["no_of_workdays"].ToString();
                    txtb_no_works_2nd.Text = dtSource_defult_setup.Rows[0]["no_of_workdays"].ToString();
                    div_no_works_days_1st.Visible = true;
                    div_no_works_days_2nd.Visible = true;

                    if (ddl_payroll_template.SelectedValue.ToString().Trim() == "011")
                    {
                        DataTable dt            = new DataTable();
                        dt                      = MyCmn.RetrieveData("sp_payrollregistry_get_dates", "par_payrolltemplate_code", "010", "par_payroll_year", ddl_year.SelectedValue.ToString(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim());
                        txtb_no_works_1st.Text  = dt.Rows[0]["no_of_workdays"].ToString();
                    }
                }
                else
                {
                    txtb_no_works_1st.Text = "0";
                    txtb_no_works_2nd.Text = "0";
                    div_no_works_days_1st.Visible = false;
                    div_no_works_days_2nd.Visible = false;
                }

                if (ddl_payroll_template.SelectedValue.ToString().Trim() == "010")
                {
                    div_no_works_days_2nd.Visible = false;
                }
                else
                {
                    div_no_works_days_2nd.Visible = true;
                }
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_registry_no.Text = "";
            txtb_period_from.Text = "";
            txtb_period_to.Text = "";
            txtb_registry_descr.Text = "";
            txtb_date_release.Text = "";
            ddl_payment_mode.SelectedValue = "01";
            ddl_payroll_group.SelectedIndex = -1;

            ddl_dep.SelectedValue           = "";
            //ddl_dep_modal.SelectedValue = "";
            ddl_function_code.SelectedValue = "";
            txtb_allotment_code.Text        = "100";

            txtb_transmittal_nbr.Text = "";
            txtb_remarks.Text         = "";
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("payroll_month", typeof(System.String));
            dtSource.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource.Columns.Add("payroll_group_nbr", typeof(System.String));
            dtSource.Columns.Add("payroll_period_from", typeof(System.String));
            dtSource.Columns.Add("payroll_period_to", typeof(System.String));
            dtSource.Columns.Add("payroll_registry_descr", typeof(System.String));
            dtSource.Columns.Add("nod_work_1st", typeof(System.String));
            dtSource.Columns.Add("nod_work_2nd", typeof(System.String));
            dtSource.Columns.Add("post_status", typeof(System.String));
            dtSource.Columns.Add("payroll_dttm_created", typeof(System.String));
            dtSource.Columns.Add("payroll_dttm_updated", typeof(System.String));
            dtSource.Columns.Add("user_id_created_by", typeof(System.String));
            dtSource.Columns.Add("user_id_updated_by", typeof(System.String));
            dtSource.Columns.Add("date_posted", typeof(System.String));
            dtSource.Columns.Add("payment_mode", typeof(System.String));
            dtSource.Columns.Add("department_code", typeof(System.String));
            dtSource.Columns.Add("function_code", typeof(System.String));
            dtSource.Columns.Add("allotment_code", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollregistry_hdr_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["payroll_month"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["payrolltemplate_code"] = string.Empty;
            nrow["payroll_group_nbr"] = string.Empty;
            nrow["payroll_period_from"] = string.Empty;
            nrow["payroll_period_to"] = string.Empty;
            nrow["payroll_registry_descr"] = string.Empty;
            nrow["nod_work_1st"] = "0";
            nrow["nod_work_2nd"] = "0";
            nrow["post_status"] = string.Empty;
            nrow["payroll_dttm_created"] = string.Empty;
            nrow["payroll_dttm_updated"] = string.Empty;
            nrow["user_id_created_by"] = string.Empty;
            nrow["user_id_updated_by"] = string.Empty;
            nrow["date_posted"] = string.Empty;
            nrow["payment_mode"] = string.Empty;
            nrow["department_code"] = string.Empty;
            nrow["function_code"] = string.Empty;
            nrow["allotment_code"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string payroll_registry_nbr = commandArgs[0];
            string payroll_year = commandArgs[1];
            string post_status = commandArgs[2];

            if (post_status.ToString().Trim() == "T") // This will trigger if the Status is "T = RETURNED"
            {
                deleteRec1.Text = "Are you sure to Void this Record ?";
                delete_header.InnerText = "Void this Record";
                lnkBtnYes.Text = " Yes, Void it ";
            }
            else
            {
                deleteRec1.Text = "Are you sure to delete this Record ?";
                delete_header.InnerText = "Delete this Record";
                lnkBtnYes.Text = " Yes, Delete it ";
            }
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);

        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "payroll_registry_nbr = '" + commandarg[0].Trim() + "' AND payroll_year='" + commandarg[1].Trim() + "'";
            string deleteExpression_ovtm_brkdn = "payroll_registry_nbr = '" + commandarg[0].Trim() + "'";

            if (commandarg[2].Trim() == "T") // This will trigger if the Status is "T = RETURNED"
            {
                // VJA : 02/01/2020 - Update the Header Table
                // 1.) User ID Update By = User ID Updated
                // 2.) Post Status       = V (Voided)
                // 3.) Updated Date Time = Current Date

                string tbl_name = "payrollregistry_hdr_tbl";
                string set_param = "user_id_updated_by = '" + Session["ep_user_id"].ToString() + "' , post_status = 'X' , payroll_dttm_updated = '" + DateTime.Now + "'";
                string where_param = "WHERE " + deleteExpression + " ";

                MyCmn.UpdateTable(tbl_name, set_param, where_param);

                DataRow[] row2Edit = dataListGrid.Select(deleteExpression);
                row2Edit[0]["post_status"] = "X";
                row2Edit[0]["post_status_descr"] = "VOIDED";

            }
            else
            {
                MyCmn.DeleteBackEndData("payrollregistry_hdr_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_ce_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_jo_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_oth_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_rata_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_subs_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_diff_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_othpay_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_ovtm_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_ovtm_brkdwn_tbl", "WHERE " + deleteExpression_ovtm_brkdn);

                MyCmn.DeleteBackEndData("payrollregistry_hdr_oth_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_othded_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_hdr_coaching_tbl", "WHERE " + deleteExpression);

                MyCmn.DeleteBackEndData_to_trk("cafao_hdr_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData_to_trk("cafao_dtl_tbl", "WHERE " + deleteExpression);
                
                DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
                dataListGrid.Rows.Remove(row2Delete[0]);
                dataListGrid.AcceptChanges();

            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "payroll_registry_nbr = '" + svalues[0].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            RetriveGroupings();
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_registry_no.Text           = svalues[0].ToString().Trim();
            txtb_registry_descr.Text        = row2Edit[0]["payroll_registry_descr"].ToString();
            txtb_period_from.Text           = row2Edit[0]["payroll_period_from"].ToString();
            txtb_period_to.Text             = row2Edit[0]["payroll_period_to"].ToString();
            ddl_payroll_group.Enabled       = false;
            txtb_registry_descr.Text        = row2Edit[0]["payroll_registry_descr"].ToString();
            nrow["payroll_dttm_created"]    = Convert.ToDateTime(row2Edit[0]["payroll_dttm_created"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            nrow["payroll_dttm_updated"]    = Convert.ToDateTime(row2Edit[0]["payroll_dttm_updated"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            nrow["user_id_created_by"]      = row2Edit[0]["user_id_created_by"].ToString();
            nrow["user_id_updated_by"]      = row2Edit[0]["user_id_updated_by"].ToString();
            txtb_date_release.Text          = row2Edit[0]["date_posted"].ToString();
            ddl_post_status.SelectedValue   = row2Edit[0]["post_status"].ToString();
            ddl_payment_mode.SelectedValue  = row2Edit[0]["payment_mode"].ToString();
            txtb_no_works_1st.Text          = row2Edit[0]["nod_work_1st"].ToString();
            txtb_no_works_2nd.Text          = row2Edit[0]["nod_work_2nd"].ToString();
            
            ddl_dep.SelectedValue           = row2Edit[0]["department_code"].ToString();
            ddl_function_code.SelectedValue = row2Edit[0]["function_code"].ToString();
            txtb_allotment_code.Text        = row2Edit[0]["allotment_code"].ToString();

            ViewState["gross_pay"]  = row2Edit[0]["gross_pay"].ToString();
            ViewState["net_pay"]    = row2Edit[0]["net_pay"].ToString();
            
            txtb_transmittal_nbr.Text = row2Edit[0]["transmittal_nbr"].ToString();
            txtb_remarks.Text         = row2Edit[0]["remarks"].ToString();

            /*>>>>>>>>> IF TEMPLATE BELONGS HERE <<<<<<<<<*/
            if (ddl_payroll_template.SelectedValue.ToString().Trim() == "008" || // Casual - Monthly Payroll  
                ddl_payroll_template.SelectedValue.ToString().Trim() == "023" || // Regular - RATA Payroll
                ddl_payroll_template.SelectedValue.ToString().Trim() == "009" || // JO - Monthly Payroll
                ddl_payroll_template.SelectedValue.ToString().Trim() == "010" || // JO - 1st Quincena Payroll
                ddl_payroll_template.SelectedValue.ToString().Trim() == "011")   // JO - 2nd Quincena Payroll
            {
                div_no_works_days_1st.Visible = true;
                div_no_works_days_2nd.Visible = true;
            }
            else
            {
                div_no_works_days_1st.Visible = false;
                div_no_works_days_2nd.Visible = false;
            }
            if (ddl_payroll_template.SelectedValue.ToString().Trim() == "010")
            {
                div_no_works_days_2nd.Visible = false;
            }
            else
            {
                div_no_works_days_2nd.Visible = true;
            }

            if (row2Edit[0]["post_status"].ToString()    == "R" // Release
                || row2Edit[0]["post_status"].ToString() == "X" // Bad Data
                || row2Edit[0]["post_status"].ToString() == "Y" // Release
                )
            {
                lbl_if_dateposted_yes.Text = "This Payroll Already " + row2Edit[0]["post_status_descr"].ToString() + "";
                btnSave.Visible = false;
                ToogleButtonTextboxes(false);
            }
            // else if (row2Edit[0]["post_status"].ToString() == "T") // Return
            // {
            //     lbl_if_dateposted_yes.Text = "This Payroll Already " + row2Edit[0]["post_status_descr"].ToString() + "";
            //     btnSave.Visible = true;
            //     ToogleButtonTextboxes(true);
            // }
            else
            {
                btnSave.Visible = true;
                ToogleButtonTextboxes(true);
                lbl_if_dateposted_yes.Text = "";
            }

            // VJA : 2020-04-04
            // Ge Try Catch ni Sya kay for the Purpose of PHIC Share
            // Mu Error man ang List kay wala sa Dropdown
            try
            {
                ddl_payroll_group.SelectedValue = row2Edit[0]["payroll_group_nbr"].ToString();

            }
            catch (Exception)
            {
                ddl_payroll_group.SelectedValue = "";
            }

            if (ddl_payroll_template.SelectedValue == "950" || // PHIC Share
                ddl_payroll_template.SelectedValue == "951"    // BAC Honorarium
                )
            {
                ToogleButtonTextboxes(false);
                //btnSave.Visible = false;
                ddl_dep.Enabled             = true;
                ddl_function_code.Enabled   = true;
                txtb_allotment_code.Enabled = true;
            }
            
            LabelAddEdit.Text = "Edit Record: " + txtb_registry_no.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
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

            if (IsDataValidated())
            {
                DataTable dt_upd_ins_hdr_oth = MyCmn.RetrieveData("sp_payrollregistry_hdr_oth_tbl_insert_upd", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", txtb_registry_no.Text.ToString(), "par_transmittal_nbr", txtb_transmittal_nbr.Text.ToString().Trim(), "par_remarks", txtb_remarks.Text.ToString().Trim(), "par_user_id", Session["ep_user_id"].ToString().Trim());
                
                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource.Rows[0]["payroll_year"] = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_month"] = ddl_month.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"] = RetrieveLastRegistryNumber_Save().ToString().Trim();
                    dtSource.Rows[0]["payrolltemplate_code"] = ddl_payroll_template.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_group_nbr"] = ddl_payroll_group.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_period_from"] = txtb_period_from.Text.ToString().Trim();
                    dtSource.Rows[0]["payroll_period_to"] = txtb_period_to.Text.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_descr"] = txtb_registry_descr.Text.ToString().Trim();
                    dtSource.Rows[0]["post_status"] = ddl_post_status.SelectedValue.ToString();
                    dtSource.Rows[0]["payroll_dttm_created"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["user_id_created_by"] = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["date_posted"] = txtb_date_release.Text == "" ? "1900-01-01" : txtb_date_release.Text;
                    dtSource.Rows[0]["payment_mode"] = ddl_payment_mode.SelectedValue.ToString().Trim();
                    
                    /*>>>>>>>>> IF TEMPLATE BELONGS HERE <<<<<<<<<*/
                    if (ddl_payroll_template.SelectedValue.ToString().Trim() == "008" || // Casual - Monthly Payroll  
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "023" || // Regular - RATA Payroll
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "009" || // JO - Monthly Payroll
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "010" || // JO - 1st Quincena Payroll
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "011")   // JO - 2nd Quincena Payroll
                    {
                        dtSource.Rows[0]["nod_work_1st"] = txtb_no_works_1st.Text.ToString().Trim();
                        dtSource.Rows[0]["nod_work_2nd"] = txtb_no_works_2nd.Text.ToString().Trim();
                    }
                    dtSource.Rows[0]["department_code"] =  ddl_dep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["function_code"]   =  ddl_function_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["allotment_code"]  =  txtb_allotment_code.Text.ToString().Trim();
                    
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"] = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_month"] = ddl_month.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"] = txtb_registry_no.Text.ToString().Trim();
                    dtSource.Rows[0]["payrolltemplate_code"] = ddl_payroll_template.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_group_nbr"] = ddl_payroll_group.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_period_from"] = txtb_period_from.Text.ToString().Trim();
                    dtSource.Rows[0]["payroll_period_to"] = txtb_period_to.Text.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_descr"] = txtb_registry_descr.Text.ToString().Trim();
                    dtSource.Rows[0]["post_status"] = ddl_post_status.SelectedValue.ToString();
                    dtSource.Rows[0]["payroll_dttm_updated"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["user_id_updated_by"] = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["date_posted"] = txtb_date_release.Text == "" ? "1900-01-01" : txtb_date_release.Text;
                    dtSource.Rows[0]["payment_mode"] = ddl_payment_mode.SelectedValue.ToString().Trim();

                    /*>>>>>>>>> IF TEMPLATE BELONGS HERE <<<<<<<<<*/
                    if (ddl_payroll_template.SelectedValue.ToString().Trim() == "008" || // Casual - Monthly Payroll  
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "023" || // Regular - RATA Payroll
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "009" || // JO - Monthly Payroll
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "010" || // JO - 1st Quincena Payroll
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "011")   // JO - 2nd Quincena Payroll
                    {
                        dtSource.Rows[0]["nod_work_1st"] = txtb_no_works_1st.Text.ToString().Trim();
                        dtSource.Rows[0]["nod_work_2nd"] = txtb_no_works_2nd.Text.ToString().Trim();
                    }
                    dtSource.Rows[0]["department_code"] =  ddl_dep.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["function_code"]   =  ddl_function_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["allotment_code"]  =  txtb_allotment_code.Text.ToString().Trim();
                    
                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    // if (msg == "") return;

                    if (msg.Substring(0, 1) == "X")
                    {
                        LblRequired1.Text = msg;
                        txtb_registry_descr.BorderColor = Color.Red;

                        FieldValidationColorChanged(true, "already-exist");
                        return;
                    }

                    /*>>>>>>>>>  Method to Update Details Coming From Header Post Status <<<<<<<<<*/
                    Update_Detail_Tables();

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["payroll_year"] = ddl_year.SelectedValue.ToString().Trim();
                        nrow["payroll_month"] = ddl_month.SelectedValue.ToString().Trim();
                        nrow["payroll_registry_nbr"] = txtb_registry_no.Text.ToString().Trim();
                        nrow["payrolltemplate_code"] = ddl_payroll_template.SelectedValue.ToString().Trim();
                        nrow["payroll_group_nbr"] = ddl_payroll_group.SelectedValue.ToString().Trim();
                        nrow["payroll_period_from"] = txtb_period_from.Text.ToString().Trim();
                        nrow["payroll_period_to"] = txtb_period_to.Text.ToString().Trim();
                        nrow["payroll_registry_descr"] = txtb_registry_descr.Text.ToString().Trim();
                        nrow["post_status"] = ddl_post_status.SelectedValue.ToString();
                        nrow["post_status_descr"] = ddl_post_status.SelectedItem.ToString();
                        nrow["payroll_period_descr"] = DateTime.Parse(txtb_period_from.Text).ToString("MM/dd") + " - " + DateTime.Parse(txtb_period_to.Text).ToString("MM/dd/yyyy");
                        nrow["date_posted"] = txtb_date_release.Text == "" ? "1900-01-01" : txtb_date_release.Text;
                        nrow["gross_pay"]   = "0.00";
                        nrow["net_pay"]     = "0.00";
                        nrow["payment_mode"] = ddl_payment_mode.SelectedValue.ToString().Trim();

                        /*>>>>>>>>> IF TEMPLATE BELONGS HERE <<<<<<<<<*/
                        if (ddl_payroll_template.SelectedValue.ToString().Trim() == "008" || // Casual - Monthly Payroll  
                            ddl_payroll_template.SelectedValue.ToString().Trim() == "023" || // Regular - RATA Payroll
                            ddl_payroll_template.SelectedValue.ToString().Trim() == "009" || // JO - Monthly Payroll
                            ddl_payroll_template.SelectedValue.ToString().Trim() == "010" || // JO - 1st Quincena Payroll
                            ddl_payroll_template.SelectedValue.ToString().Trim() == "011")   // JO - 2nd Quincena Payroll
                        {
                            nrow["nod_work_1st"] = txtb_no_works_1st.Text.ToString().Trim();
                            nrow["nod_work_2nd"] = txtb_no_works_2nd.Text.ToString().Trim();
                        }
                        nrow["department_code"] =  ddl_dep.SelectedValue.ToString().Trim();
                        nrow["function_code"]   =  ddl_function_code.SelectedValue.ToString().Trim();
                        nrow["allotment_code"]  =  txtb_allotment_code.Text.ToString().Trim();

                        nrow["transmittal_nbr"] = txtb_transmittal_nbr.Text  ;
                        nrow["remarks"]         = txtb_remarks.Text          ;

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        RetrieveDataListGrid();
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        id_header.InnerHtml   = "Successfully";
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "payroll_registry_nbr = '" + txtb_registry_no.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"] = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_month"] = ddl_month.SelectedValue.ToString().Trim();
                        row2Edit[0]["payrolltemplate_code"] = ddl_payroll_template.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_group_nbr"] = ddl_payroll_group.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_period_from"] = txtb_period_from.Text.ToString().Trim();
                        row2Edit[0]["payroll_period_to"] = txtb_period_to.Text.ToString().Trim();
                        row2Edit[0]["payroll_registry_descr"] = txtb_registry_descr.Text.ToString().Trim();
                        row2Edit[0]["post_status"] = ddl_post_status.SelectedValue.ToString();
                        row2Edit[0]["post_status_descr"] = ddl_post_status.SelectedItem.ToString();
                        row2Edit[0]["payroll_period_descr"] = DateTime.Parse(txtb_period_from.Text).ToString("MM/dd") + " - " + DateTime.Parse(txtb_period_to.Text).ToString("MM/dd/yyyy");
                        row2Edit[0]["date_posted"] = txtb_date_release.Text == "" ? "1900-01-01" : txtb_date_release.Text;
                        row2Edit[0]["gross_pay"] = ViewState["gross_pay"].ToString(); 
                        row2Edit[0]["net_pay"]   = ViewState["net_pay"].ToString();
                        row2Edit[0]["payment_mode"] = ddl_payment_mode.SelectedValue.ToString().Trim();

                        /*>>>>>>>>> IF TEMPLATE BELONGS HERE <<<<<<<<<*/
                        if (ddl_payroll_template.SelectedValue.ToString().Trim() == "008" ||  // Casual - Monthly Payroll  
                            ddl_payroll_template.SelectedValue.ToString().Trim() == "023" ||  // Regular - RATA Payroll
                            ddl_payroll_template.SelectedValue.ToString().Trim() == "009" ||  // JO - Monthly Payroll
                            ddl_payroll_template.SelectedValue.ToString().Trim() == "010" ||  // JO - 1st Quincena Payroll
                            ddl_payroll_template.SelectedValue.ToString().Trim() == "011")    // JO - 2nd Quincena Payroll
                        {
                            row2Edit[0]["nod_work_1st"] = txtb_no_works_1st.Text.ToString().Trim();
                            row2Edit[0]["nod_work_2nd"] = txtb_no_works_2nd.Text.ToString().Trim();
                        }
                        row2Edit[0]["department_code"] =  ddl_dep.SelectedValue.ToString().Trim();
                        row2Edit[0]["function_code"]   =  ddl_function_code.SelectedValue.ToString().Trim();
                        row2Edit[0]["allotment_code"]  =  txtb_allotment_code.Text.ToString().Trim();

                        row2Edit[0]["transmittal_nbr"] = txtb_transmittal_nbr.Text;
                        row2Edit[0]["remarks"]          = txtb_remarks.Text;

                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                        id_header.InnerHtml = "Successfully";

                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                    SearchData(txtb_search.Text.ToString().Trim());
                }

            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**********************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**********************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**********************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void SearchData(string search)
        {
            string searchExpression = "payroll_registry_nbr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
                "payroll_registry_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
                "payroll_period_from LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
                "payroll_group_nbr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
                "gross_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
                "net_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
                "post_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
                "payroll_period_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_month", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("payroll_group_nbr", typeof(System.String));
            dtSource1.Columns.Add("payroll_period_from", typeof(System.String));
            dtSource1.Columns.Add("payroll_period_to", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_descr", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("payroll_period_descr", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("date_posted", typeof(System.String));
            dtSource1.Columns.Add("crud_name", typeof(System.String));
            dtSource1.Columns.Add("payment_mode", typeof(System.String));
            dtSource1.Columns.Add("coaching_status", typeof(System.String));
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

            try
            {
                DateTime.Parse(txtb_period_from.Text);
                UpdateDateFrom.Update();
                DateTime.Parse(txtb_period_to.Text);
                UpdateDateTo.Update();

                if (txtb_registry_descr.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_registry_descr");
                    txtb_registry_descr.Focus();
                    validatedSaved = false;
                }
                if (ddl_payroll_group.SelectedValue == "")
                {
                    FieldValidationColorChanged(true, "ddl_payroll_group");
                    ddl_payroll_group.Focus();
                    validatedSaved = false;
                }
                if (txtb_period_from.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_period_from");
                    txtb_period_from.Focus();
                    validatedSaved = false;
                }
                if (txtb_period_to.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_period_to");
                    txtb_period_to.Focus();
                    validatedSaved = false;
                }
                if (CommonCode.checkisdatetime(txtb_date_release.Text) == false && ddl_post_status.SelectedValue == "R")
                {
                    FieldValidationColorChanged(true, "txtb_date_release");
                    txtb_date_release.Focus();
                    validatedSaved = false;
                }
                if (ddl_payment_mode.SelectedValue == "")
                {
                    FieldValidationColorChanged(true, "ddl_payment_mode");
                    ddl_payment_mode.Focus();
                    validatedSaved = false;
                }
            }
            catch (Exception)
            {
                if (txtb_registry_descr.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_registry_descr");
                    txtb_registry_descr.Focus();
                    validatedSaved = false;
                }
                if (txtb_period_from.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_period_from");
                    txtb_period_from.Focus();
                    validatedSaved = false;
                }
                else if (txtb_period_from.Text != "")
                {
                    FieldValidationColorChanged(true, "invalid-date-1");
                    txtb_period_from.Focus();
                    validatedSaved = false;
                }
                if (txtb_period_to.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_period_to");
                    txtb_period_to.Focus();
                    validatedSaved = false;
                }
                else if (txtb_period_to.Text != "")
                {
                    FieldValidationColorChanged(true, "invalid-date-2");
                    txtb_period_to.Focus();
                    validatedSaved = false;
                }
                if (CommonCode.checkisdatetime(txtb_date_release.Text) == false && ddl_post_status.SelectedValue == "R")
                {
                    FieldValidationColorChanged(true, "txtb_date_release");
                    txtb_date_release.Focus();
                    validatedSaved = false;
                }
                if (ddl_payment_mode.SelectedValue == "")
                {
                    FieldValidationColorChanged(true, "ddl_payment_mode");
                    ddl_payment_mode.Focus();
                    validatedSaved = false;
                }
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
                    case "txtb_registry_descr":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_registry_descr.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_period_from":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_period_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "ddl_payroll_group":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            ddl_payroll_group.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "txtb_period_to":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_period_to.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "invalid-date-1":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_period_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "invalid-date-2":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_period_to.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "txtb_date_release":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_date_release.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "ddl_payment_mode":
                        {
                            LblRequired300.Text = MyCmn.CONST_RQDFLD;
                            ddl_payment_mode.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }

                    case "date_of_coaching":
                        {
                            date_of_coaching_req.Text    = MyCmn.CONST_RQDFLD;
                            date_of_coaching.BorderColor = Color.Red;
                            break;
                        }
                    case "subject":
                        {
                            subject_req.Text = MyCmn.CONST_RQDFLD;
                            subject.BorderColor = Color.Red;
                            break;
                        }
                    case "particulars":
                        {
                            particulars_req.Text = MyCmn.CONST_RQDFLD;
                            particulars.BorderColor = Color.Red;
                            break;
                        }
                    case "name_of_incharge":
                        {
                            name_of_incharge_req.Text = MyCmn.CONST_RQDFLD;
                            name_of_incharge.BorderColor = Color.Red;
                            break;
                        }
                    case "name_of_supervisor":
                        {
                            name_of_supervisor_req.Text = MyCmn.CONST_RQDFLD;
                            name_of_supervisor.BorderColor = Color.Red;
                            break;
                        }
                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired1.Text           = "";
                            LblRequired2.Text           = "";
                            LblRequired3.Text           = "";
                            LblRequired5.Text           = "";
                            LblRequired6.Text           = "";
                            LblRequired300.Text         = "";
                            date_of_coaching_req.Text   = "";
                            subject_req.Text            = "";
                            particulars_req.Text        = "";
                            name_of_incharge_req.Text   = "";
                            name_of_supervisor_req.Text = "";

                            txtb_registry_descr.BorderColor = Color.LightGray;
                            txtb_period_from.BorderColor    = Color.LightGray;
                            txtb_period_to.BorderColor      = Color.LightGray;
                            ddl_payroll_group.BorderColor   = Color.LightGray;
                            txtb_date_release.BorderColor   = Color.LightGray;
                            ddl_payment_mode.BorderColor    = Color.LightGray;
                            date_of_coaching.BorderColor    = Color.LightGray;
                            subject.BorderColor             = Color.LightGray;
                            particulars.BorderColor         = Color.LightGray;
                            name_of_incharge.BorderColor    = Color.LightGray;
                            name_of_supervisor.BorderColor  = Color.LightGray;
                            
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            UpdateDateFrom.Update();
                            UpdateDateTo.Update();
                            UpdateDateTo.Update();
                            UpdatePanel16.Update();
                            break;
                        }
                }
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Employment Type
        //*************************************************************************
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue        != "" &&
                ddl_year.SelectedValue             != "" &&
                ddl_month.SelectedValue            != "" &&
                ddl_payroll_template.SelectedValue != "" &&
                ddl_payroll_template.SelectedValue != "950" && // Hide ADD BUTTON - PHIC Share
                ddl_payroll_template.SelectedValue != "951"    // Hide ADD BUTTON - BAC Honorarium
                )
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetriveTemplate();
            //RetriveGroupings();
            RetrieveDataListGrid();
            UpdatePanel10.Update();

            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Close Notification
        //*************************************************************************
        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Payroll Year
        //*************************************************************************
        protected void ddl_year_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue        != "" && 
                ddl_year.SelectedValue             != "" && 
                ddl_month.SelectedValue            != "" && 
                ddl_payroll_template.SelectedValue != "" &&
                ddl_payroll_template.SelectedValue != "950" && // Hide ADD BUTTON - PHIC Share
                ddl_payroll_template.SelectedValue != "951"    // Hide ADD BUTTON - BAC Honorarium
                )
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            //RetriveGroupings();
            RetrieveDataListGrid();
            UpdatePanel10.Update();
            updatepanel_printall.Update();

            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Payroll Group
        //*************************************************************************
        protected void ddl_payroll_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtb_registry_descr.Text = ddl_payroll_group.SelectedItem.Text.ToString();
            get_default();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Template Type/Code
        //*************************************************************************
        protected void ddl_payroll_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue        != "" && 
                ddl_year.SelectedValue             != "" && 
                ddl_month.SelectedValue            != "" && 
                ddl_payroll_template.SelectedValue != "" &&
                ddl_payroll_template.SelectedValue != "950" && // Hide ADD BUTTON - PHIC Share
                ddl_payroll_template.SelectedValue != "951"    // Hide ADD BUTTON - BAC Honorarium
                )
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
           // RetriveGroupings();
            RetrieveDataListGrid();
            UpdatePanel10.Update();
            updatepanel_printall.Update();

            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Payroll Month
        //*************************************************************************
        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue        != "" && 
                ddl_year.SelectedValue             != "" && 
                ddl_month.SelectedValue            != "" && 
                ddl_payroll_template.SelectedValue != "" &&
                ddl_payroll_template.SelectedValue != "950" && // Hide ADD BUTTON - PHIC Share
                ddl_payroll_template.SelectedValue != "951"    // Hide ADD BUTTON - BAC Honorarium
                )
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            //RetriveGroupings();
            RetrieveDataListGrid();
            UpdatePanel10.Update();
            updatepanel_printall.Update();

            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Go to the URL When Click Based on Tempalte Code
        //*************************************************************************
        protected void imgbtn_add_empl_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string selectExpression = "payroll_registry_nbr = '" + commandarg[0].Trim() + "'";

            string url = "";
            Session["PreviousValuesonPage_cPayRegistry"] = ddl_year.SelectedValue.ToString() + "," + ddl_month.SelectedValue.ToString().Trim() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_payroll_template.SelectedValue.ToString() + "," + gv_dataListGrid.PageIndex + "," + DropDownListID.SelectedValue.ToString() + "," + commandarg[1].ToString().Trim() + "," + commandarg[0].ToString().Trim() + "," + txtb_search.Text.ToString().Trim();

            // VJA : 02/01/2020 - Seprate Session Variable because the positble value of Search 
            DataRow[] row2Edit = dataListGrid.Select(selectExpression);
            Session["PreviousValuesonPage_cPayRegistry_post_status"]  = row2Edit[0]["post_status"].ToString();
            Session["PreviousValuesonPage_cPayRegistry_nod_work_1st"] = row2Edit[0]["nod_work_1st"].ToString();
            Session["PreviousValuesonPage_cPayRegistry_nod_work_2nd"] = row2Edit[0]["nod_work_2nd"].ToString();
            // VJA : 02/01/2020 - Seprate Session Variable because the positble value of Search  

            url = row2Edit[0]["crud_name"].ToString().Trim();

            if (url != "")
            {
                Response.Redirect(url);
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Open the modal Select Report
        //*************************************************************************
        protected void imgbtn_print_Command1(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string payroll_registry = "payroll_registry_nbr = '" + commandarg[0].Trim() + "'";
            lbl_payrollregistry_nbr_print.Text = "";
            lbl_payrollregistry_nbr_print.Text = commandarg[0].ToString().Trim();
            string can_print = "true";

            // ************************************************************************************************************
            // ** BEGIN : VJA - 2022-11-29 - Prompt the User does not coincide the Payroll Registry Description ***********
            // ************************************************************************************************************
            DataTable chk_reg_hdr = MyCmn.RetrieveData("sp_chk_payroll_registry_hdr", "p_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "p_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "p_payroll_registry_nbr", commandarg[0].ToString().Trim());
            if (chk_reg_hdr.Rows.Count > 0)
            {
                if (ddl_select_report.SelectedValue != "")
                {
                    ddl_select_report.Items.RemoveAt(0);
                }
                GetReportFile();
                can_print = "false";
                btn_show_print_option.Visible = false;
                msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                msg_header.InnerText = "CANNOT PROCEED IN PRINTING";
                lbl_details.Text = chk_reg_hdr.Rows[0]["validation_msg"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);

                //RetrieveRelatedTemplate();
                //lnkPrint.CommandArgument = e.CommandArgument.ToString();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "openSelectReport();", true);
                return;
            }
            // ************************************************************************************************************
            // ** END : VJA - 2022-11-29 - Prompt the User does not coincide the Payroll Registry Description ***********
            // ************************************************************************************************************

            DataTable dt_check_atm = MyCmn.RetrieveData("p_check_atm_printing", "p_payroll_registry_nbr", commandarg[0].ToString().Trim());
            DataRow[] row2Edit = dataListGrid.Select(payroll_registry);

            // ***************************************************************************************
            // VJA - 2022-05-18 - Condition to Required the Transmittal Number During Printing for***
            //  ************ Monthly and QuinenaPayroll, Overtime and Hazard and Subsistince
            // ***************************************************************************************
            if ((ddl_payroll_template.SelectedValue.ToString() == "007" ||
                ddl_payroll_template.SelectedValue.ToString() == "008" ||
                ddl_payroll_template.SelectedValue.ToString() == "009" ||
                ddl_payroll_template.SelectedValue.ToString() == "010" ||
                ddl_payroll_template.SelectedValue.ToString() == "011" ||
                ddl_payroll_template.SelectedValue.ToString() == "022" || // Overtime
                ddl_payroll_template.SelectedValue.ToString() == "042" || // Overtime
                ddl_payroll_template.SelectedValue.ToString() == "061" || // Overtime
                ddl_payroll_template.SelectedValue.ToString() == "021" || // Hazard
                ddl_payroll_template.SelectedValue.ToString() == "041"    // Hazard
                )
                &&
                row2Edit[0]["transmittal_nbr"].ToString() == "")
            {
                if (ddl_select_report.SelectedValue != "")
                {
                    ddl_select_report.Items.RemoveAt(0);
                }
                GetReportFile();
                can_print = "false";
                btn_show_print_option.Visible = true;
                msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                msg_header.InnerText = "DO YOU WANT TO CONTINUE PRINTING?";
                lbl_details.Text = "This Payroll Registry is no Transmittal number!";

                

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
            }
            // ***************************************************************************************
            // ***************************************************************************************

            RetrieveRelatedTemplate();
            lnkPrint.CommandArgument = e.CommandArgument.ToString();

            if (commandarg[2].ToString().Trim() == "01" || commandarg[2].ToString().Trim() == "")
            {
                if (dt_check_atm.Rows[0]["result_flag"].ToString() == "Y")
                {
                    can_print = "true";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "openSelectReport();", true);
                }
                else
                {
                    if (ddl_select_report.SelectedValue != "")
                    {
                        ddl_select_report.Items.RemoveAt(0);
                    }
                    GetReportFile();
                    can_print = "false";
                    btn_show_print_option.Visible = true;
                    msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                    msg_header.InnerText = "PRINT PREVIEW ONLY!";
                    lbl_details.Text = dt_check_atm.Rows[0]["result_flag_descr"].ToString() + "<br><br>";
                    
                    for (int i = 0; i < dt_check_atm.Rows.Count; i++)
                    {
                        lbl_details.Text += dt_check_atm.Rows[i]["empl_id"].ToString() + " - " + dt_check_atm.Rows[i]["employee_name"].ToString();
                    }
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
                }
            }
            else if (commandarg[2].ToString().Trim() == "02")
            {
                can_print = "true";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "openSelectReport();", true);
            }
            
            Session["can_print_on_preview"] = can_print;
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - 
        //*************************************************************************
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string printreport;
            string procedure;
            string url = "";
            Session["history_page"] = Request.Url.AbsolutePath;
            Session["PreviousValuesonPage_cPayRegistry"] = ddl_year.SelectedValue.ToString() + "," + ddl_month.SelectedValue.ToString().Trim() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_payroll_template.SelectedValue.ToString() + "," + gv_dataListGrid.PageIndex + "," + DropDownListID.SelectedValue.ToString() + "," + lnkPrint.CommandArgument.Split(',')[1].ToString().Trim() + "," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + "," + txtb_search.Text.ToString().Trim();

            // ********************START*********************************************************
            // ******* This is for the Other Custom Payroll Setup ********05/07/2020*************
            // **********************************************************************************
            if (ddl_select_report.SelectedValue.ToString().Trim().Substring(0, 1) == "9")
            {
                DataTable dt = MyCmn.RetrieveData("sp_othrpaysetup_tbl_fld_cnt", "p_payrolltemplate_code", ddl_select_report.SelectedValue.ToString().Trim());
                if (dt.Rows.Count > 0)
                {
                    if (hidden_report_filename.Text == "")
                    {
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay" + dt.Rows[0]["no_of_fields"].ToString().Trim() + ".rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                    }
                    else
                    {
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                    }
                }

            }
            // ******************END*************************************************************

            try
            {
                switch (ddl_select_report.SelectedValue)
                {
                    case "105": // Obligation Request (OBR) - For Regular 
                    case "205": // Obligation Request (OBR) - For Casual
                    case "305": // Obligation Request (OBR) - For Job-Order
                         printreport = hidden_report_filename.Text;
                         procedure = "sp_payrollregistry_obr_rep";
                         url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim();
                        
                         //// Temporary - Details are coming from table cafao_dtl_tbl
                         // printreport = "/cryOtherPayroll/cryOBR/cryOBR_CAFOA.rpt";
                         // procedure = "sp_payrollregistry_cafao_rep_new"; 
                         // url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim();

                        break;

                    //---- START OF REGULAR REPORTS

                    case "007": // Summary Monthly Salary  - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "101": // Mandatory Deduction  - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "102": // Optional Deduction Page 1 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "106": // Optional Deduction Page 2 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "103": // Loan Deduction Page 1 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "107": // Loan Deduction Page 2 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "104": // Attachment - For Monthly Salary
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_attach_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                        break;

                    case "033": // Salary Differential - For Regular 
                    case "052": // Salary Differential - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_diff_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    //---- END OF REGULAR REPORTS

                    //---- START OF CASUAL REPORTS

                    case "008": // Summary Monthly Salary  - For Casual 

                        // *************************************************************************************
                        // *** Special Case for Casual Monthly Payroll - Change Report File name if Year 2020
                        // ****************** 2021-01-13 *******************************************************
                        // *************************************************************************************
                        if (ddl_select_report.SelectedValue == "008" && double.Parse(ddl_year.SelectedValue) <= 2020)
                        {
                            printreport = "/cryCasualReports/crySalary/crySalarySummary_2020.rpt";
                            procedure   = "sp_payrollregistry_salary_ce_rep";
                            url         = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        }
                        // *************************************************************************************
                        // *** Special Case for Casual Monthly Payroll - Change Report File name if Year 2020
                        // ****************** 2021-01-13 *******************************************************
                        // *************************************************************************************
                        else
                        {
                            printreport = hidden_report_filename.Text;
                            procedure = "sp_payrollregistry_salary_ce_rep";
                            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        }
                        
                        break;

                    case "206": // Mandatory Deduction  -  For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "207": // Optional Deduction Page 1 - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "208": // Optional Deduction Page 2 - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "209": // Loan Deduction Page 1 - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "210": // Loan Deduction Page 2 - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "211": // Attachment - For Monthly Salary - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_attach_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                        break;

                    case "044": // Monetization Payroll - For Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    //---- END OF CASUAL REPORTS

                    //---- START OF JOB-ORDER REPORTS

                    case "009": // Summary Salary Monthly - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "010": // Summary Salary 1st Quincemna - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "011": // Summary Salary 2nd Quincemna - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "306": // Contributions/Deductions Page 1 - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "307": // Contributions/Deductions Page 1 - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "308": // Attachment - For Monthly Salary - Job-Order
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_attach_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                        break;

                    case "061": // Overtime Payroll - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_ovtm_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "062": // Honorarium Payroll - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;


                    //---- END OF JOB-ORDER REPORTS
                    //---- START OF OTHER PAYROLL REPORTS

                    case "024": // Communication Expense Allowance - Regular
                    case "043": // Communication Expense Allowance - Casual
                    case "063": // Communication Expense Allowance - Job-Order
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "026": // Mid Year Bonus  - Regular        
                    case "045": // Mid Year Bonus  - Casual       
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "027": // Year-End And Cash Gift Bonus - Regular
                    case "046": // Year-End And Cash Gift Bonus - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "028": // Clothing Allowance - Regular
                    case "047": // Clothing Allowance - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "029": // Loyalty Bonus        - Regular
                    case "048": // Loyalty Bonus        - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "030": // Anniversary Bonus    - Regular
                    case "049": // Anniversary Bonus    - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "031": // Productivity Enhancement Incentive Bonus  - Regular
                    case "050": // Productivity Enhancement Incentive Bonus  - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "023": // RATA 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_rata_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "108": // RATA - OBR Breakdown
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_obr_rata_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();

                        break;

                    case "021": // Subsistence, HA and LA      - Regular
                    case "041": // Subsistence, HA and LA      - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "022": // Overtime - Regular
                    case "042": // Overtime - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_ovtm_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "032": // CNA INCENTIVE - Regular
                    case "051": // CNA INCENTIVE - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "025": // Monetization Payroll - For Regular
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    //case "901": // Other Payroll 1 - For Regular 
                    //    printreport = hidden_report_filename.Text;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                    //    break;

                    //case "902": // Other Payroll 2 - For Regular 
                    //    printreport = hidden_report_filename.Text;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                    //    break;

                    //case "903": // Other Payroll 3 - For Regular 
                    //    printreport = hidden_report_filename.Text;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                    //    break;

                    //case "904": // Other Payroll 4 - For Regular 
                    //    printreport = hidden_report_filename.Text;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                    //    break;

                    //case "905": // Other Payroll 5 - For Regular 
                    //    printreport = hidden_report_filename.Text;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                    //    break;

                    case "109": // Communication Expense - OBR Breakdown - RE
                    case "120": // Communication Expense - OBR Breakdown - JO
                    case "121": // Communication Expense - OBR Breakdown - CE
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_obr_commx_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();

                        break;

                    case "X": // Direct Print to Printer
                        url = "/View/cDirectToPrinter/cDirectToPrinter.aspx";
                        break;

                    //case "111": // Attachment - FOR RATA PAYROLL
                    //    printreport = hidden_report_filename.Text;
                    //    procedure = "sp_payrollregistry_RATA_attach_rep";
                    //    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                    //    break;

                    case "212": // PaySLip  - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_payslip_re_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim() + ",par_empl_id," + "";

                        break;

                    case "213": // PaySLip  - For Job_Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_payslip_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim() + ",par_empl_id," + "";

                        break;

                    case "214": // PaySLip  - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_payslip_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim() + ",par_empl_id," + "";

                        break;

                    case "034": // Honorarium  - For Regular 
                    case "035": // Honorarium  - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "112": // Other Payroll Attachment  - For Regular 
                    case "113": // Other Payroll Attachment  - For Casual 
                    case "114": // Other Payroll Attachment  - For JO 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth_attach_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "950": // Other Payroll - PHIC Share -  RE
                        printreport = hidden_report_filename.Text;
                        procedure   = "sp_payrollregistry_phic_share_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                        break;

                    case "951": // Other Payroll - BAC Honorarium -  RE
                        printreport = hidden_report_filename.Text;
                        procedure   = "sp_payrollregistry_bac_rep";
                        url         = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-09-03 ***********************
                    // ****************** R E M I T T A N C E S ************
                    //******************************************************

                    case "215": // W/Tax Remittance for Subsistence - CE
                    case "216": // W/Tax Remittance for Subsistence - RE
                    case "217": // W/Tax Remittance for Subsistence - JO
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "223": // W/Tax Remittance for Subsistence - CE
                    case "224": // W/Tax Remittance for Subsistence - RE
                    case "225": // W/Tax Remittance for Subsistence - JO
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_ovtm_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        
                        break;

                    case "219": // Subsistence Loan Remittance -  CE
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                        
                    case "218": // Subsistence Loan Remittance -  RE
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-09-14 ***********************
                    // ****************** G E N E R I C  P A Y   S L I P ***
                    //******************************************************

                    case "220": // Generic Pay Slip - CE
                    case "221": // Generic Pay Slip - RE
                    case "222": // Generic Pay Slip - JO
                        procedure = "sp_payrollregistry_payslip";

                        if (ddl_payroll_template.SelectedValue == "041" ||  // Subsistence - RE
                            ddl_payroll_template.SelectedValue == "021" )   // Subsistence - CE
                        {
                            printreport = "/cryOtherPayroll/cryPayslip/cryPS_Subsistence.rpt";
                            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + "";
                            
                        }
                        else if(ddl_payroll_template.SelectedValue == "023")  // RATA - RE
                        {
                            printreport = "/cryOtherPayroll/cryPayslip/cryPS_RATA.rpt";
                            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + "";
                        }
                        else if (ddl_payroll_template.SelectedValue == "022" ||   // Overtime Payroll - RE
                                 ddl_payroll_template.SelectedValue == "042" ||   // Overtime Payroll - CE
                                 ddl_payroll_template.SelectedValue == "061"   )  // Overtime Payroll - JO
                        {
                            printreport = "/cryOtherPayroll/cryPayslip/cryPS_Ovtm.rpt";
                            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + "";
                        }
                        else 
                        {
                            printreport = "/cryOtherPayroll/cryPayslip/cryPS_OtherSal.rpt";
                            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + "";
                        }

                        break;
                    // *****************************************************
                    // ****************** N E W  A T T A C H M E N T *******
                    // ****************** 2020-09-14 ***********************
                    //******************************************************

                    case "130": // New Attachment -  RE
                    case "131": // New Attachment -  CE
                    case "132": // New Attachment -  JO
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_header_footer_sub_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                        break;
                        
                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-11-12 ***********************
                    //******************************************************

                    case "116": // Monthly Payroll -  Sub Specialist
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-11-14 ***********************
                    //******************************************************
                    case "309":  // Obligation Request - Details coming from CAFOA - RE
                    case "310":  // Obligation Request - Details coming from CAFOA - CE
                    case "311":  // Obligation Request - Details coming from CAFOA - JO
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_cafao_rep_new";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-11-14 ***********************
                    //******************************************************
                    case "923":  // Special Risk Allowance II - RE
                    case "933":  // Special Risk Allowance II - CE
                    case "943":  // Special Risk Allowance II - JO
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_SRA.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "924":  // COVID-19 Hazard Pay - RE
                    case "934":  // COVID-19 Hazard Pay - CE
                    case "944":  // COVID-19 Hazard Pay - JO
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HZD.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2021-02-12 ***********************
                    //******************************************************
                    case "133":  // Fund Utilization Request and Status (FURS) - RE
                    case "134":  // Fund Utilization Request and Status (FURS) - CE
                    case "135":  // Fund Utilization Request and Status (FURS) - JO
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_cafao_rep_new";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim();
                        break;

                    case "939":  // Performance Based Bonus  - CE
                    case "929":  // Performance Based Bonus  - RE
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_PBB.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2021-12-17 ***********************
                    //******************************************************
                    case "232":  // Honorarium Remittance - JO
                    case "233":  // CNA Remittance        - RE
                    case "234":  // CNA Remittance        - CE
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "962":  // Service Recognition Incentives (SRI) - RE
                    case "957":  // Service Recognition Incentives (SRI) - CE
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_SRI.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;
                        
                    case "963":  // Hazard, Subsistence and Laundry (Differential)
                    case "958":  // Hazard, Subsistence and Laundry (Differential)
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HZD_DIFF.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "136": // Document Tracking History - RE'
                    case "137": // Document Tracking History - CE'
                    case "138": // Document Tracking History - JO'
                        printreport = "/cryDocTracking/cryDocsHistory.rpt";
                        procedure = "sp_edocument_trk_tbl_history";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",p_doc_ctrl_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",p_docmnt_type," + "01-P";
                        break;

                    case "968":  // ONE COVID-19 ALLOWANCE (OCA) - 'RE'
                    case "969":  // ONE COVID-19 ALLOWANCE (OCA) - 'CE'
                    case "970":  // ONE COVID-19 ALLOWANCE (OCA) - 'JO'
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "240":  //  ONE COVID-19 ALLOWANCE (OCA) - RE - Remittance
                    case "241":  //  ONE COVID-19 ALLOWANCE (OCA) - CE - Remittance
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA_Remit_RECE.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "242":  //  ONE COVID-19 ALLOWANCE (OCA) - JO - Remittance
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA_Remit_JO.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "971":  // PERFORMANCE BASED BONUS FY 2021 - REGULAR
                    case "972":  // PERFORMANCE BASED BONUS FY 2021 - CASUAL
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_PBB2021.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;
                        
                    case "974":  // Health Emergency Allowance (HEA) - 'RE'
                    case "975":  // Health Emergency Allowance (HEA) - 'CE'
                    case "976":  // Health Emergency Allowance (HEA) - 'JO'
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "243":  //  Health Emergency Allowance (HEA) - RE - Remittance
                    case "244":  //  Health Emergency Allowance (HEA) - CE - Remittance
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA_Remit_RECE.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "245":  //  Health Emergency Allowance (HEA) - JO - Remittance
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA_Remit_JO.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;

                    case "981":  //  RATA Differential
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay5_RATADiff.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                        break;
                }


                if (url != "")
                {
                    Response.Redirect(url);
                }
            }
            catch (Exception)
            {
                btn_show_print_option.Visible = false;
                msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-danger");
                msg_header.InnerText    = "CANNOT PRINT!";
                lbl_details.Text        = "Report File is Blank !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Trigger When Select From Related Template
        //*************************************************************************
        protected void ddl_select_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_select_report.SelectedValue != "")
            {
                GetReportFile();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Get Report Filename
        //*************************************************************************
        private void GetReportFile()
        {
            if (ddl_select_report.SelectedValue != "X" 
                && ddl_select_report.SelectedValue != "") 
            {
                string searchExpression = "payrolltemplate_code = '" + ddl_select_report.SelectedValue.ToString().Trim() + "'";
                DataRow[] row2Search = dtSourse_for_template.Select(searchExpression);
            
                hidden_report_filename.Text = row2Search[0]["report_filename"].ToString();
            }
        }

        protected void ddl_post_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            if (ddl_post_status.SelectedValue == "R")
            {
                txtb_date_release.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                txtb_date_release.Text = "";
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Method to Update Details Coming From Header Post Status
        //*************************************************************************
        private void Update_Detail_Tables()
        {
            string WhereExpression = "WHERE payroll_registry_nbr = '" + txtb_registry_no.Text.Trim() + "' AND payroll_year='" + ddl_year.SelectedValue.ToString() + "'";
            string Table_name = "";
            switch (ddl_payroll_template.SelectedValue)
            {
                case "007":  // Monthly Salary                      - For Regular
                    Table_name = "payrollregistry_dtl_tbl";
                    break;

                case "008": // Monthly Salary                        - For Casual
                    Table_name = "payrollregistry_dtl_ce_tbl";
                    break;

                case "009": // Monthly Salary                       - For Job-Order
                case "010": // 1st Quicena Salary                   - For Job-Order
                case "011": // 2nd Quicena Salary                   - For Job-Order
                    Table_name = "payrollregistry_dtl_jo_tbl";
                    break;

                case "022":  // Overtime Payroll                    - For Regular
                case "042":  // Overtime Payroll                    - For Casual
                case "061":  // Overtime Payroll                     - For Job-Order
                    Table_name = "payrollregistry_dtl_ovtm_tbl";
                    break;

                case "062": // Honorarium                           - For Job-Order
                    Table_name = "payrollregistry_dtl_oth_tbl";
                    break;

                case "901": //  Additional Bonus For Regular
                case "902": //  Additional Bonus For Regular
                case "903": //  Additional Bonus For Regular
                case "904": //  Additional Bonus For Regular
                case "905": //  Additional Bonus For Regular
                    Table_name = "payrollregistry_dtl_othpay_tbl";
                    break;

                case "024": // Communication Expense Allowance     - For Regular
                case "025": // Monetization                        - For Regular
                case "026": // Mid Year Bonus                      - For Regular
                case "027": // Year-End and Cash Gift Bonus        - For Regular
                case "028": // Clothing Allowances                 - For Regular
                case "029": // Loyalty Bonus                       - For Regular
                case "030": // Anniversary Bonus                   - For Regular
                case "031": // Productivity Enhancement Incentive  - For Regular
                case "032": // C. N. A. Incentive                  - For Regular

                case "043":  // Communication Expense Allowance    - For Casual
                case "044":  // Monetization                       - For Casual
                case "045":  // Mid Year Bonus                     - For Casual
                case "046":  // Year - End and Cash Gift Bonus     - For Casual
                case "047":  // Clothing Allowances                - For Casual
                case "048":  // Loyalty Bonus                      - For Casual
                case "049":  // Anniversary Bonus                  - For Casual
                case "050":  // Productivity Enhancement Incentive - For Casual
                case "051":  // C.N.A.Incentive                    - For Casual
                    Table_name = "payrollregistry_dtl_oth_tbl";
                    break;

                case "033": // Salary Differential                   - For Regular
                case "052": // Salary Differential                   - For Casual
                    Table_name = "payrollregistry_dtl_diff_tbl";
                    break;

                case "023": // RATA                                  - For Regular
                    Table_name = "payrollregistry_dtl_rata_tbl";
                    break;

                case "021": // Subsistence, HA and LA      - Regular
                case "041": // Subsistence, HA and LA      - Casual
                    Table_name = "payrollregistry_dtl_subs_tbl";
                    break;

            }

            MyCmn.UpdateTable(Table_name, "post_status = '" + ddl_post_status.SelectedValue.ToString() + "'", WhereExpression);
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Toogle Textboxes to Enable
        //*************************************************************************
        private void ToogleButtonTextboxes(bool var_TrueFalse)
        {
            // txtb_registry_descr.Enabled = var_TrueFalse;
            // txtb_period_from.Enabled    = var_TrueFalse;
            // txtb_period_to.Enabled      = var_TrueFalse;
            // ddl_payment_mode.Enabled    = var_TrueFalse;

            //txtb_registry_no.Enabled = var_TrueFalse;
            txtb_period_from.Enabled = var_TrueFalse;
            txtb_period_to.Enabled = var_TrueFalse;
            txtb_registry_descr.Enabled = var_TrueFalse;
            txtb_date_release.Enabled = var_TrueFalse;
            ddl_payment_mode.Enabled = var_TrueFalse;
            ddl_payroll_group.Enabled = var_TrueFalse;

            ddl_dep.Enabled = var_TrueFalse;
            ddl_function_code.Enabled = var_TrueFalse;
            txtb_allotment_code.Enabled = var_TrueFalse;

            txtb_no_works_1st.Enabled = var_TrueFalse;
            txtb_no_works_2nd.Enabled = var_TrueFalse;


            if (ddl_post_status.SelectedValue == "Y" ||
                ddl_post_status.SelectedValue == "R")
            {
                ddl_dep.Enabled             = true;
                ddl_function_code.Enabled   = true;
                txtb_allotment_code.Enabled = true;
                btnSave.Visible             = true;
            }
        }

        protected void btn_show_print_option_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "closeNotification();", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "openSelectReport();", true);
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

            //ddl_dep_modal.DataSource = dt;
            //ddl_dep_modal.DataValueField = "department_code";
            //ddl_dep_modal.DataTextField = "department_name1";
            //ddl_dep_modal.DataBind();
            //ListItem li2 = new ListItem("-- Select Here --", "");
            //ddl_dep_modal.Items.Insert(0, li2);
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
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve Registry Number
        //*************************************************************************
        private string RetrieveLastRegistryNumber_Save()
        {
            string registry_nbr = "";
            DataTable dt = MyCmn.RetrieveData("sp_get_next_registry_no", "par_payroll_year", ddl_year.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                registry_nbr = dt.Rows[0]["next_registry_no"].ToString();
            }
            return registry_nbr;
        }


        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetrieveEmployee()
        {
            //ddl_employee_name.Items.Clear();
            ////ddl_employee_id.Items.Clear();
            //DataTable dt = MyCmn.RetrieveData("sp_personnelnames_combolist_per_dep", "par_department_code", ddl_dep_modal.SelectedValue.ToString());

            //ddl_employee_name.DataSource = dt;
            //ddl_employee_name.DataValueField = "empl_id";
            //ddl_employee_name.DataTextField = "employee_name";
            //ddl_employee_name.DataBind();
            //ListItem li = new ListItem("-- Select Here --", "");
            //ddl_employee_name.Items.Insert(0, li);

            //ddl_employee_id.DataSource = dt;
            //ddl_employee_id.DataValueField = "empl_id";
            //ddl_employee_id.DataTextField = "empl_id";
            //ddl_employee_id.DataBind();
            //ListItem li2 = new ListItem("----", "");
            //ddl_employee_id.Items.Insert(0, li2);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve Registry Number
        //*************************************************************************
        //public void RetrievePayrollPerEmployee(string empl_id)
        //{
        //    try
        //    {
        //        dt_payroll = new DataTable();
                
        //        dt_payroll = MyCmn.RetrieveData("sp_payroll_per_empl_list", "par_empl_id", empl_id, "par_payroll_year", ddl_year_modal.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month_modal.SelectedValue.ToString().Trim());
        //        MyCmn.Sort(grid_payroll_list, dt_payroll, Session["SortField"].ToString(), Session["SortOrder"].ToString());

        //        ddl_dep_modal.SelectedValue = "";

        //        if (dt_payroll.Rows.Count > 0)
        //        {
        //            ddl_dep_modal.SelectedValue = dt_payroll.Rows[0]["department_code"].ToString().Trim();
        //        }
        //    }
        //    catch (Exception)
        //    {
                
        //    }
        //}
        
        protected void btn_payroll_per_employee_Click(object sender, EventArgs e)
        {
            lbl_select_option.Text = "Payroll Per Employee List";
            div_name.Visible        = true;
            div_yr_mth.Visible      = true;
            div_dep.Visible         = true;
            div_pyroll_lst.Visible  = true;
            div_empl_type.Visible   = false;
            //lnk_generate_rep.Visible = false;
            //ddl_dep_modal.Enabled = false;
            //lnk_print_rep.Visible = false;
            div_payrolltemplate.Visible = false;

            //ddl_month_modal.SelectedValue = ddl_month.SelectedValue.ToString();
            //ddl_year_modal.SelectedValue = ddl_year.SelectedValue.ToString();
            RetrieveEmployee();
            //RetrievePayrollPerEmployee();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop_Payroll", "openModalPayroll();", true);
        }
        
        //protected void ddl_employee_name_SelectedIndexChanged(object sender, EventArgs e)
        //{
            //ddl_employee_id.SelectedValue = ddl_employee_name.SelectedValue;
            //RetrievePayrollPerEmployee();
        //}
        //protected void ddl_employee_id_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddl_employee_name.SelectedValue = ddl_employee_id.SelectedValue;
        //    RetrievePayrollPerEmployee();
        //}
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Change Field Sort mode  
        //**************************************************************************
        protected void gv_dataListGrid_per_empl_Sorting(object sender, GridViewSortEventArgs e)
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

            //MyCmn.Sort(grid_payroll_list, dt_payroll, e.SortExpression, sortingDirection);
        }

        //protected void ddl_dep_modal_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RetrieveEmployee();
        //}

        protected void btn_annual_ovtm_rep_Click(object sender, EventArgs e)
        {
            
            lbl_select_option.Text = "Annual Report for Overtime Payroll";
            div_name.Visible        = false;
            div_yr_mth.Visible      = true;
            div_dep.Visible         = true;
            div_pyroll_lst.Visible  = false;
            div_empl_type.Visible   = true;
            //lnk_generate_rep.Visible= true;
           // ddl_dep_modal.Enabled   = true;
            //lnk_print_rep.Visible   = false;
            div_payrolltemplate.Visible = false;

            //ddl_month_modal.SelectedValue = "12";
            //ddl_year_modal.SelectedValue  = ddl_year.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop_Payroll", "openModalPayroll();", true);
            

        }

        //protected void lnk_generate_rep_Click(object sender, EventArgs e)
        //{
        //    string year  = "";
        //    string month = "";
        //    string empl_type = "";
        //    string dep = "";
        //    string printreport;
        //    string procedure;
        //    string url  = "";
        //    printreport = "/cryOvertimeAnnual/cryOvertimeAnnual.rpt";
        //    procedure   = "sp_payrollregistry_ovtm_annual_rep";
        //    url         = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_year," + year + ",par_month," + month + ",par_employment_type," + empl_type + ",par_department_code," + dep;
            
        //    if (url != "")
        //    {
        //        Response.Redirect(url);
        //    }
        //}

        protected void imgbtn_coaching_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "payroll_year = '" + commandarg[0].ToString().Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            payroll_year.Text           = "";
            payroll_registry_nbr.Text   = "";
            date_of_coaching.Text       = "";
            subject.Text                = "";
            particulars.Text            = "";
            name_of_incharge.Text       = "";
            name_of_supervisor.Text     = "";
            lbl_coaching_msg.Text       = "";
            
            DataTable chk = new DataTable();
            string query = "SELECT * FROM payrollregistry_hdr_coaching_tbl WHERE payroll_year = '" + row2Edit[0]["payroll_year"].ToString() + "' AND payroll_registry_nbr = '" + row2Edit[0]["payroll_registry_nbr"].ToString() + "'";
            chk = MyCmn.GetDatatable(query);

            payroll_year.Text           = row2Edit[0]["payroll_year"].ToString();
            payroll_registry_nbr.Text   = row2Edit[0]["payroll_registry_nbr"].ToString();
            if (chk.Rows.Count > 0)
            {
                name_of_incharge.Text       = chk.Rows[0]["name_of_incharge"].ToString();
                name_of_supervisor.Text     = chk.Rows[0]["name_of_supervisor"].ToString();
                date_of_coaching.Text       = DateTime.Parse(chk.Rows[0]["date_of_coaching"].ToString()).ToString("yyyy-MM-dd");
                subject.Text                = chk.Rows[0]["subject"].ToString();
                particulars.Text            = chk.Rows[0]["particulars"].ToString();
            }
            else
            {

                DataTable name = new DataTable();
                string query_name = "SELECT * FROM payrollregistry_hdr_oth_tbl A INNER JOIN vw_personnelnames_PAY B ON B.empl_id = REPLACE(ISNULL(A.user_id_created_by,''),'U','') WHERE A.payroll_registry_nbr = '" + row2Edit[0]["payroll_registry_nbr"].ToString() + "' AND A.payroll_year = '" + row2Edit[0]["payroll_year"].ToString() + "'";
                name = MyCmn.GetDatatable(query_name);
                if (name.Rows.Count > 0)
                {
                    name_of_incharge.Text   = (name.Rows[0]["first_name"].ToString() + " " + (name.Rows[0]["middle_name"].ToString() == "" ? "" : name.Rows[0]["middle_name"].ToString().Substring(0,1) + ". ") + name.Rows[0]["last_name"].ToString()).ToUpper();
                }
                else
                {
                    name_of_incharge.Text   = "NO TRANSMITTAL YET";
                }

                name_of_supervisor.Text = "VIVIAN S. ESPARAGOZA";
            }

            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "openCoaching", "openCoaching();", true);
        }

        protected void lnkbtn_save_coach_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            if (IsDataValidated_Coaching())
            {
                DataTable chk   = new DataTable();
                string query    = "SELECT * FROM payrollregistry_hdr_coaching_tbl WHERE payroll_year = '"+payroll_year.Text.ToString().Trim()+"' AND payroll_registry_nbr = '"+payroll_registry_nbr.Text.ToString().Trim()+"'";
                chk             = MyCmn.GetDatatable(query);

                if (chk.Rows.Count > 0)
                {
                    string update_script = "UPDATE payrollregistry_hdr_coaching_tbl SET date_of_coaching = '"+ date_of_coaching.Text + "',subject = '"+ subject.Text.Replace("'","''") + "',particulars = '"+particulars.Text.Replace("'", "''") + "',name_of_incharge='"+name_of_incharge.Text.Replace("'", "''") + "',name_of_supervisor = '"+name_of_supervisor.Text.Replace("'", "''") + "' WHERE payroll_year = '" + payroll_year.Text.ToString().Trim() + "' AND payroll_registry_nbr = '" + payroll_registry_nbr.Text.ToString().Trim() + "'";
                    SaveAddEdit.Text = MyCmn.UpdateToTable(update_script);
                }
                else
                {
                    string insert_script = "INSERT INTO payrollregistry_hdr_coaching_tbl VALUES('"+ payroll_year.Text.ToString().Trim() + "','"+ payroll_registry_nbr.Text.ToString().Trim() + "','"+ date_of_coaching.Text.Replace("'", "''") + "','"+ subject.Text.Replace("'", "''") + "','"+ particulars.Text.Replace("'", "''") + "','"+ name_of_incharge.Text.Replace("'", "''") + "','"+ name_of_supervisor.Text.Replace("'", "''") + "')";
                    SaveAddEdit.Text = MyCmn.InsertToTable(insert_script);
                }

                string editExpression = "payroll_year = '" + payroll_year.Text + "' AND payroll_registry_nbr = '" + payroll_registry_nbr.Text + "'";
                DataRow[] row2Edit = dataListGrid.Select(editExpression);

                row2Edit[0]["payroll_year"]           = payroll_year.Text          ; 
                row2Edit[0]["payroll_registry_nbr"]   = payroll_registry_nbr.Text  ; 
                row2Edit[0]["date_of_coaching"]       = date_of_coaching.Text      ; 
                row2Edit[0]["subject"]                = subject.Text               ; 
                row2Edit[0]["particulars"]            = particulars.Text           ; 
                row2Edit[0]["name_of_incharge"]       = name_of_incharge.Text      ; 
                row2Edit[0]["name_of_supervisor"]     = name_of_supervisor.Text;
                row2Edit[0]["coaching_status"]        = payroll_registry_nbr.Text;
                lbl_coaching_msg.Text                 = "";
                up_dataListGrid.Update();
                MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeCoaching", "closeCoaching();", true);
            }
        }
        
        protected void lnkbtn_print_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            if (IsDataValidated_Coaching())
            {
                DataTable chk   = new DataTable();
                string query    = "SELECT * FROM payrollregistry_hdr_coaching_tbl WHERE payroll_year = '"+payroll_year.Text.ToString().Trim()+"' AND payroll_registry_nbr = '"+payroll_registry_nbr.Text.ToString().Trim()+"'";
                chk             = MyCmn.GetDatatable(query);

                if (chk.Rows.Count > 0)
                {
                    string update_script = "UPDATE payrollregistry_hdr_coaching_tbl SET date_of_coaching = '"+ date_of_coaching.Text + "',subject = '"+ subject.Text.Replace("'","''") + "',particulars = '"+particulars.Text.Replace("'", "''") + "',name_of_incharge='"+name_of_incharge.Text.Replace("'", "''") + "',name_of_supervisor = '"+name_of_supervisor.Text.Replace("'", "''") + "' WHERE payroll_year = '" + payroll_year.Text.ToString().Trim() + "' AND payroll_registry_nbr = '" + payroll_registry_nbr.Text.ToString().Trim() + "'";
                    SaveAddEdit.Text = MyCmn.UpdateToTable(update_script);
                }
                else
                {
                    string insert_script = "INSERT INTO payrollregistry_hdr_coaching_tbl VALUES('"+ payroll_year.Text.ToString().Trim() + "','"+ payroll_registry_nbr.Text.ToString().Trim() + "','"+ date_of_coaching.Text.Replace("'", "''") + "','"+ subject.Text.Replace("'", "''") + "','"+ particulars.Text.Replace("'", "''") + "','"+ name_of_incharge.Text.Replace("'", "''") + "','"+ name_of_supervisor.Text.Replace("'", "''") + "')";
                    SaveAddEdit.Text = MyCmn.InsertToTable(insert_script);
                }

                string editExpression = "payroll_year = '" + payroll_year.Text + "' AND payroll_registry_nbr = '" + payroll_registry_nbr.Text + "'";
                DataRow[] row2Edit = dataListGrid.Select(editExpression);

                row2Edit[0]["payroll_year"]           = payroll_year.Text          ; 
                row2Edit[0]["payroll_registry_nbr"]   = payroll_registry_nbr.Text  ; 
                row2Edit[0]["date_of_coaching"]       = date_of_coaching.Text      ; 
                row2Edit[0]["subject"]                = subject.Text               ; 
                row2Edit[0]["particulars"]            = particulars.Text           ; 
                row2Edit[0]["name_of_incharge"]       = name_of_incharge.Text      ; 
                row2Edit[0]["name_of_supervisor"]     = name_of_supervisor.Text;
                lbl_coaching_msg.Text                 = "";

                Session["history_page"] = Request.Url.AbsolutePath;
                Session["PreviousValuesonPage_cPayRegistry"] = ddl_year.SelectedValue.ToString() + "," + ddl_month.SelectedValue.ToString().Trim() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_payroll_template.SelectedValue.ToString() + "," + gv_dataListGrid.PageIndex + "," + DropDownListID.SelectedValue.ToString() + "," + "" + "," + "" + "," + txtb_search.Text.ToString().Trim();

                string printreport;
                string procedure;
                string url  = "";
                printreport = "/cryCoaching/cryCoaching.rpt";
                procedure   = "sp_payrollregistry_hdr_coaching_tbl_rep";
                url         = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",p_payroll_year," + payroll_year.Text.ToString().Trim() + ",p_payroll_month," + "" + ",p_payroll_registry_nbr," + payroll_registry_nbr.Text.ToString().Trim() + ",p_payrolltemplate_code,";

                if (url != "")
                {
                    Response.Redirect(url);
                }
            }

        }

        protected void btn_coaching_list_Click(object sender, EventArgs e)
        {
            lbl_select_option.Text = "Coaching & Mentoring List";
            div_name.Visible        = false;
            div_yr_mth.Visible      = true;
            div_dep.Visible         = false;
            div_pyroll_lst.Visible  = false;
            div_empl_type.Visible   = true;
            //lnk_generate_rep.Visible= false;
            //ddl_dep_modal.Enabled   = false;
            //lnk_print_rep.Visible   = true;
            div_payrolltemplate.Visible = true;

            //ddl_empl_type_modal.SelectedValue = "";
            //RetriveTemplate_Modal();
            //ddl_month_modal.SelectedValue = ddl_month.SelectedValue.ToString();
            //ddl_year_modal.SelectedValue  = ddl_year.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop_Payroll", "openModalPayroll();", true);
        }

        //protected void lnk_print_rep_Click(object sender, EventArgs e)
        //{
        //    string year = "";
        //    string month = "";
        //    string template = "";
        //    DataTable chk =  MyCmn.RetrieveData("sp_payrollregistry_hdr_coaching_tbl_rep", "p_payroll_year", year, "p_payroll_month", month, "p_payroll_registry_nbr", "", "p_payrolltemplate_code", template);
        //    if (chk.Rows.Count > 0)
        //    {
        //        Session["history_page"] = Request.Url.AbsolutePath;
        //        Session["PreviousValuesonPage_cPayRegistry"] = ddl_year.SelectedValue.ToString() + "," + ddl_month.SelectedValue.ToString().Trim() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_payroll_template.SelectedValue.ToString() + "," + gv_dataListGrid.PageIndex + "," + DropDownListID.SelectedValue.ToString() + "," + "" + "," + "" + "," + txtb_search.Text.ToString().Trim();

        //        string printreport;
        //        string procedure;
        //        string url = "";
        //        printreport = "/cryCoachingList/cryCoachingList.rpt";
        //        procedure = "sp_payrollregistry_hdr_coaching_tbl_rep";
        //        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",p_payroll_year," + year + ",p_payroll_month," + month + ",p_payroll_registry_nbr," + "" + ",p_payrolltemplate_code," + template;

        //        if (url != "")
        //        {
        //            Response.Redirect(url);
        //        }
        //    }
        //    else
        //    {
        //        btn_show_print_option.Visible = false;
        //        msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
        //        msg_header.InnerText = "NO DATA FOUND!";
        //        lbl_details.Text = "Coaching and Mentoring information not found!";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
        //    }

        //}

        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated_Coaching()
        {
            bool validatedSaved = true;
            try
            {
                if (date_of_coaching.Text == "")
                {
                    FieldValidationColorChanged(true, "date_of_coaching");
                    date_of_coaching.Focus();
                    validatedSaved = false;
                }
                if (CommonCode.checkisdatetime(date_of_coaching.Text) == false)
                {
                    date_of_coaching_req.Text           = "Invalid date!";
                    date_of_coaching.BorderColor        = Color.Red;
                    date_of_coaching.Focus();
                    validatedSaved = false;
                }
                if (subject.Text == "")
                {
                    FieldValidationColorChanged(true, "subject");
                    subject.Focus();
                    validatedSaved = false;
                }
                if (particulars.Text == "")
                {
                    FieldValidationColorChanged(true, "particulars");
                    particulars.Focus();
                    validatedSaved = false;
                }
                if (name_of_incharge.Text == "")
                {
                    FieldValidationColorChanged(true, "name_of_incharge");
                    name_of_incharge.Focus();
                    validatedSaved = false;
                }
                if (name_of_supervisor.Text == "")
                {
                    FieldValidationColorChanged(true, "name_of_supervisor");
                    name_of_supervisor.Focus();
                    validatedSaved = false;
                }
            }
            catch (Exception e)
            {
                lbl_coaching_msg.Text   = e.Message.ToString();
                validatedSaved = false;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
            return validatedSaved;
        }

        protected void lnkbtn_delete_coach_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            DataTable chk = new DataTable();
            string query = "SELECT * FROM payrollregistry_hdr_coaching_tbl WHERE payroll_year = '" + payroll_year.Text.ToString().Trim() + "' AND payroll_registry_nbr = '" + payroll_registry_nbr.Text.ToString().Trim() + "'";
            chk = MyCmn.GetDatatable(query);

            if (chk.Rows.Count > 0)
            {
                string delete_script = "DELETE payrollregistry_hdr_coaching_tbl WHERE payroll_year = '" + payroll_year.Text.ToString().Trim() + "' AND payroll_registry_nbr = '" + payroll_registry_nbr.Text.ToString().Trim() + "'";
                SaveAddEdit.Text = MyCmn.DeleteToTable(delete_script);
                
                string editExpression = "payroll_year = '" + payroll_year.Text + "' AND payroll_registry_nbr = '" + payroll_registry_nbr.Text + "'";
                DataRow[] row2Edit = dataListGrid.Select(editExpression);

                row2Edit[0]["coaching_status"] = "";
                up_dataListGrid.Update();
                MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeCoaching", "closeCoaching();", true);
            }
            else
            {
                btn_show_print_option.Visible = false;
                msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                msg_header.InnerText = "NO DATA FOUND!";
                lbl_details.Text = "Coaching and Mentoring information not found!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
            }
        }

        //protected void ddl_empl_type_modal_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RetriveTemplate_Modal();
        //}

        [WebMethod]
        public static string DepartmentList()
        {
            CommonDB MyCmn  = new CommonDB();
            DataTable dt    = MyCmn.RetrieveData("sp_departments_tbl_list", "par_include_history", "N");
            string json     = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return json;
        }
        [WebMethod]
        public static string RetrieveGrid(string empl_id, string year, string month)
        {
            DataTable dt_payroll = new DataTable();
            CommonDB MyCmn       = new CommonDB();
            dt_payroll           = MyCmn.RetrieveData("sp_payroll_per_empl_list", "par_empl_id", empl_id, "par_payroll_year", year, "par_payroll_month", month);
            string json          = JsonConvert.SerializeObject(dt_payroll, Newtonsoft.Json.Formatting.Indented);
            return json;
        }
        [WebMethod]
        public static string PayrollTemplateList(string empl_type)
        {
            CommonDB MyCmn  = new CommonDB();
            DataTable dt    = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list6", "par_employment_type", empl_type);
            string json     = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return json;
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - 
        //*************************************************************************
        [WebMethod]
        public static string PayrollPrintPreview(string ddl_select_report, string ddl_year, string ddl_month, string payroll_registry_nbr, string ddl_payroll_template, string ddl_empl_type)
        {
            CommonDB MyCmn = new CommonDB();
            string report_filename;
            string printreport;
            string procedure;
            string url              = "";
            string gv_dataListGrid  = "1";
            string DropDownListID   = "5";
            string txtb_search      = "";
            HttpContext.Current.Session["print_all_variables"] = "";
            HttpContext.Current.Session["history_pagex"]       = "/View/cPayRegistry/cPayRegistry.aspx";
            HttpContext.Current.Session["PreviousValuesonPage_cPayRegistry"]       = ddl_year.ToString() + "," + ddl_month.ToString().Trim() + "," + ddl_empl_type.ToString() + "," + ddl_payroll_template.ToString() + "," + gv_dataListGrid + "," + DropDownListID.ToString() + "," + "" + "," + payroll_registry_nbr.ToString().Trim() + "," + txtb_search.ToString().Trim();


            DataTable payroll = new DataTable();
            string query    = "SELECT *FROM payrolltemplate_tbl WHERE payrolltemplate_code = '"+ ddl_select_report.ToString().Trim() + "'";
            payroll         = MyCmn.GetDatatable(query);
            report_filename = payroll.Rows[0]["report_filename"].ToString();
            // ********************START*********************************************************
            // ******* This is for the Other Custom Payroll Setup ********05/07/2020*************
            // **********************************************************************************
            if (ddl_select_report.ToString().Trim().Substring(0, 1) == "9")
            {
                DataTable dt = MyCmn.RetrieveData("sp_othrpaysetup_tbl_fld_cnt", "p_payrolltemplate_code", ddl_select_report.ToString().Trim());
                if (dt.Rows.Count > 0)
                {
                    if (report_filename == "")
                    {
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay" + dt.Rows[0]["no_of_fields"].ToString().Trim() + ".rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                    }
                    else
                    {
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                    }
                }
            }
            // ******************END*************************************************************

            try
            {
                switch (ddl_select_report)
                {
                    case "105": // Obligation Request (OBR) - For Regular 
                    case "205": // Obligation Request (OBR) - For Casual
                    case "305": // Obligation Request (OBR) - For Job-Order
                         printreport = report_filename;
                         procedure = "sp_payrollregistry_obr_rep";
                         url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim();
                        
                         //// Temporary - Details are coming from table cafao_dtl_tbl
                         // printreport = "/cryOtherPayroll/cryOBR/cryOBR_CAFOA.rpt";
                         // procedure = "sp_payrollregistry_cafao_rep_new"; 
                         // url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim();

                        break;

                    //---- START OF REGULAR REPORTS

                    case "007": // Summary Monthly Salary  - For Regular 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "101": // Mandatory Deduction  - For Regular 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "102": // Optional Deduction Page 1 - For Regular 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "106": // Optional Deduction Page 2 - For Regular 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "103": // Loan Deduction Page 1 - For Regular 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "107": // Loan Deduction Page 2 - For Regular 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "104": // Attachment - For Monthly Salary
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_re_attach_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr;

                        break;

                    case "033": // Salary Differential - For Regular 
                    case "052": // Salary Differential - For Casual 
                        //printreport = report_filename;
                        //procedure = "sp_payrollregistry_salary_diff_rep";
                        //url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        var reg = payroll_registry_nbr.Remove(payroll_registry_nbr.Length - 1, 1).Split('-');
                        for (int i = 0; i < reg.Length; i++)
                        {
                            url += "~/Reports//" + report_filename + ",sp_payroll_generate_reports_all,par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_payroll_registry_nbr," + reg[i] + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim() + ",par_empl_id,,par_employment_type," + ddl_empl_type.ToString().Trim() + ",,par_mother_template_code," + ddl_payroll_template.ToString().Trim() + ",";
                            var template = "";
                            var cafoa_rpt = "cryOtherPayroll/cryOBR/cryCAFAO.rpt";
                            if (ddl_select_report.ToString().Trim() == "033")
                            {
                                template = "309";
                                url += "~/Reports//" + cafoa_rpt + ",sp_payroll_generate_reports_all,par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_payroll_registry_nbr," + reg[i] + ",par_payrolltemplate_code," + template + ",par_empl_id,,par_employment_type," + ddl_empl_type.ToString().Trim() + ",,par_mother_template_code," + ddl_payroll_template.ToString().Trim() + ",";
                            }
                            else
                            {
                                template = "310";
                                url += "~/Reports//" + cafoa_rpt + ",sp_payroll_generate_reports_all,par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_payroll_registry_nbr," + reg[i] + ",par_payrolltemplate_code," + template + ",par_empl_id,,par_employment_type," + ddl_empl_type.ToString().Trim() + ",,par_mother_template_code," + ddl_payroll_template.ToString().Trim() + ",";
                            }
                        }
                        url = url.Remove(url.Length - 1, 1);
                        HttpContext.Current.Session["print_all_variables"] = url;
                        break;

                    //---- END OF REGULAR REPORTS

                    //---- START OF CASUAL REPORTS

                    case "008": // Summary Monthly Salary  - For Casual 

                        // *************************************************************************************
                        // *** Special Case for Casual Monthly Payroll - Change Report File name if Year 2020
                        // ****************** 2021-01-13 *******************************************************
                        // *************************************************************************************
                        if (ddl_select_report == "008" && double.Parse(ddl_year) <= 2020)
                        {
                            printreport = "/cryCasualReports/crySalary/crySalarySummary_2020.rpt";
                            procedure   = "sp_payrollregistry_salary_ce_rep";
                            url         = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        }
                        // *************************************************************************************
                        // *** Special Case for Casual Monthly Payroll - Change Report File name if Year 2020
                        // ****************** 2021-01-13 *******************************************************
                        // *************************************************************************************
                        else
                        {
                            printreport = report_filename;
                            procedure = "sp_payrollregistry_salary_ce_rep";
                            url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        }
                        
                        break;

                    case "206": // Mandatory Deduction  -  For Casual 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "207": // Optional Deduction Page 1 - For Casual 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "208": // Optional Deduction Page 2 - For Casual 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "209": // Loan Deduction Page 1 - For Casual 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "210": // Loan Deduction Page 2 - For Casual 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "211": // Attachment - For Monthly Salary - For Casual 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_ce_attach_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr;

                        break;

                    case "044": // Monetization Payroll - For Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    //---- END OF CASUAL REPORTS

                    //---- START OF JOB-ORDER REPORTS

                    case "009": // Summary Salary Monthly - For Job-Order 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "010": // Summary Salary 1st Quincemna - For Job-Order 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "011": // Summary Salary 2nd Quincemna - For Job-Order 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "306": // Contributions/Deductions Page 1 - For Job-Order 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "307": // Contributions/Deductions Page 1 - For Job-Order 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "308": // Attachment - For Monthly Salary - Job-Order
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_jo_attach_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr;

                        break;

                    case "061": // Overtime Payroll - For Job-Order 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_ovtm_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "062": // Honorarium Payroll - For Job-Order 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;


                    //---- END OF JOB-ORDER REPORTS
                    //---- START OF OTHER PAYROLL REPORTS

                    case "024": // Communication Expense Allowance - Regular
                    case "043": // Communication Expense Allowance - Casual
                    case "063": // Communication Expense Allowance - Job-Order
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "026": // Mid Year Bonus  - Regular        
                    case "045": // Mid Year Bonus  - Casual       
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "027": // Year-End And Cash Gift Bonus - Regular
                    case "046": // Year-End And Cash Gift Bonus - Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "028": // Clothing Allowance - Regular
                    case "047": // Clothing Allowance - Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "029": // Loyalty Bonus        - Regular
                    case "048": // Loyalty Bonus        - Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "030": // Anniversary Bonus    - Regular
                    case "049": // Anniversary Bonus    - Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "031": // Productivity Enhancement Incentive Bonus  - Regular
                    case "050": // Productivity Enhancement Incentive Bonus  - Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "023": // RATA 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_rata_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "108": // RATA - OBR Breakdown
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_obr_rata_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim() + ",par_employment_type," + ddl_empl_type.ToString().Trim();

                        break;

                    case "021": // Subsistence, HA and LA      - Regular
                    case "041": // Subsistence, HA and LA      - Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "022": // Overtime - Regular
                    case "042": // Overtime - Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_ovtm_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "032": // CNA INCENTIVE - Regular
                    case "051": // CNA INCENTIVE - Casual
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    case "025": // Monetization Payroll - For Regular
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                        break;

                    //case "901": // Other Payroll 1 - For Regular 
                    //    printreport = report_filename;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                    //    break;

                    //case "902": // Other Payroll 2 - For Regular 
                    //    printreport = report_filename;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                    //    break;

                    //case "903": // Other Payroll 3 - For Regular 
                    //    printreport = report_filename;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                    //    break;

                    //case "904": // Other Payroll 4 - For Regular 
                    //    printreport = report_filename;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                    //    break;

                    //case "905": // Other Payroll 5 - For Regular 
                    //    printreport = report_filename;
                    //    procedure = "sp_payrollregistry_othpay_rep";
                    //    url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();

                    //    break;

                    case "109": // Communication Expense - OBR Breakdown - RE
                    case "120": // Communication Expense - OBR Breakdown - JO
                    case "121": // Communication Expense - OBR Breakdown - CE
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_obr_commx_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim() + ",par_employment_type," + ddl_empl_type.ToString().Trim();

                        break;

                    case "X": // Direct Print to Printer
                        url = "/View/cDirectToPrinter/cDirectToPrinter.aspx";
                        break;

                    //case "111": // Attachment - FOR RATA PAYROLL
                    //    printreport = report_filename;
                    //    procedure = "sp_payrollregistry_RATA_attach_rep";
                    //    url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr;

                    //    break;

                    case "212": // PaySLip  - For Regular 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_payslip_re_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim() + ",par_empl_id," + "";

                        break;

                    case "213": // PaySLip  - For Job_Order 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_payslip_jo_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim() + ",par_empl_id," + "";

                        break;

                    case "214": // PaySLip  - For Casual 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_payslip_ce_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim() + ",par_empl_id," + "";

                        break;

                    case "034": // Honorarium  - For Regular 
                    case "035": // Honorarium  - For Casual 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "112": // Other Payroll Attachment  - For Regular 
                    case "113": // Other Payroll Attachment  - For Casual 
                    case "114": // Other Payroll Attachment  - For JO 
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth_attach_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "950": // Other Payroll - PHIC Share -  RE
                        printreport = report_filename;
                        procedure   = "sp_payrollregistry_phic_share_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr;
                        break;

                    case "951": // Other Payroll - BAC Honorarium -  RE
                        printreport = report_filename;
                        procedure   = "sp_payrollregistry_bac_rep";
                        url         = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr;
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-09-03 ***********************
                    // ****************** R E M I T T A N C E S ************
                    //******************************************************

                    case "215": // W/Tax Remittance for Subsistence - CE
                    case "216": // W/Tax Remittance for Subsistence - RE
                    case "217": // W/Tax Remittance for Subsistence - JO
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "223": // W/Tax Remittance for Subsistence - CE
                    case "224": // W/Tax Remittance for Subsistence - RE
                    case "225": // W/Tax Remittance for Subsistence - JO
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_ovtm_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        
                        break;

                    case "219": // Subsistence Loan Remittance -  CE
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                        
                    case "218": // Subsistence Loan Remittance -  RE
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-09-14 ***********************
                    // ****************** G E N E R I C  P A Y   S L I P ***
                    //******************************************************

                    case "220": // Generic Pay Slip - CE
                    case "221": // Generic Pay Slip - RE
                    case "222": // Generic Pay Slip - JO
                        procedure = "sp_payrollregistry_payslip";

                        if (ddl_payroll_template == "041" ||  // Subsistence - RE
                            ddl_payroll_template == "021" )   // Subsistence - CE
                        {
                            printreport = "/cryOtherPayroll/cryPayslip/cryPS_Subsistence.rpt";
                            url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_employment_type," + ddl_empl_type.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim() + ",par_empl_id," + "";
                            
                        }
                        else if(ddl_payroll_template == "023")  // RATA - RE
                        {
                            printreport = "/cryOtherPayroll/cryPayslip/cryPS_RATA.rpt";
                            url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_employment_type," + ddl_empl_type.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim() + ",par_empl_id," + "";
                        }
                        else if (ddl_payroll_template == "022" ||   // Overtime Payroll - RE
                                 ddl_payroll_template == "042" ||   // Overtime Payroll - CE
                                 ddl_payroll_template == "061"   )  // Overtime Payroll - JO
                        {
                            printreport = "/cryOtherPayroll/cryPayslip/cryPS_Ovtm.rpt";
                            url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_employment_type," + ddl_empl_type.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim() + ",par_empl_id," + "";
                        }
                        else 
                        {
                            printreport = "/cryOtherPayroll/cryPayslip/cryPS_OtherSal.rpt";
                            url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month," + ddl_month.ToString().Trim() + ",par_employment_type," + ddl_empl_type.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim() + ",par_empl_id," + "";
                        }

                        break;
                    // *****************************************************
                    // ****************** N E W  A T T A C H M E N T *******
                    // ****************** 2020-09-14 ***********************
                    //******************************************************

                    case "130": // New Attachment -  RE
                    case "131": // New Attachment -  CE
                    case "132": // New Attachment -  JO
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_header_footer_sub_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr;
                        break;
                        
                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-11-12 ***********************
                    //******************************************************

                    case "116": // Monthly Payroll -  Sub Specialist
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-11-14 ***********************
                    //******************************************************
                    case "309":  // Obligation Request - Details coming from CAFOA - RE
                    case "310":  // Obligation Request - Details coming from CAFOA - CE
                    case "311":  // Obligation Request - Details coming from CAFOA - JO
                        // printreport = report_filename;
                        // procedure = "sp_payrollregistry_cafao_rep_new";
                        // url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim();
                        if (payroll_registry_nbr.Contains("-") == true)
                        {
                            reg = payroll_registry_nbr.Remove(payroll_registry_nbr.Length - 1, 1).Split('-');
                            for (int i = 0; i < reg.Length; i++)
                            {
                                url += "~/Reports//" + report_filename + ",sp_payroll_generate_reports_all,par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month,"+ddl_month.ToString().Trim()+",par_payroll_registry_nbr,"+ reg[i] + ",par_payrolltemplate_code,"+ ddl_select_report.ToString().Trim() + ",par_empl_id,,par_employment_type,"+ddl_empl_type.ToString().Trim()+",,par_mother_template_code,"+ ddl_payroll_template.ToString().Trim() + ",";
                            }
                        }
                        else
                        {
                                url += "~/Reports//" + report_filename + ",sp_payroll_generate_reports_all,par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_month,"+ddl_month.ToString().Trim()+",par_payroll_registry_nbr,"+ payroll_registry_nbr + ",par_payrolltemplate_code,"+ ddl_select_report.ToString().Trim() + ",par_empl_id,,par_employment_type,"+ddl_empl_type.ToString().Trim()+",,par_mother_template_code,"+ ddl_payroll_template.ToString().Trim() + ",";
                        }
                        url = url.Remove(url.Length - 1, 1);
                        HttpContext.Current.Session["print_all_variables"]  = url;
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2020-11-14 ***********************
                    //******************************************************
                    case "923":  // Special Risk Allowance II - RE
                    case "933":  // Special Risk Allowance II - CE
                    case "943":  // Special Risk Allowance II - JO
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_SRA.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "924":  // COVID-19 Hazard Pay - RE
                    case "934":  // COVID-19 Hazard Pay - CE
                    case "944":  // COVID-19 Hazard Pay - JO
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HZD.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2021-02-12 ***********************
                    //******************************************************
                    case "133":  // Fund Utilization Request and Status (FURS) - RE
                    case "134":  // Fund Utilization Request and Status (FURS) - CE
                    case "135":  // Fund Utilization Request and Status (FURS) - JO
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_cafao_rep_new";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_payroll_template.ToString().Trim();
                        break;

                    case "939":  // Performance Based Bonus  - CE
                    case "929":  // Performance Based Bonus  - RE
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_PBB.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    // *****************************************************
                    // ****************** N E W L Y    A D D E D ***********
                    // ****************** 2021-12-17 ***********************
                    //******************************************************
                    case "232":  // Honorarium Remittance - JO
                    case "233":  // CNA Remittance        - RE
                    case "234":  // CNA Remittance        - CE
                        printreport = report_filename;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "962":  // Service Recognition Incentives (SRI) - RE
                    case "957":  // Service Recognition Incentives (SRI) - CE
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_SRI.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;
                        
                    case "963":  // Hazard, Subsistence and Laundry (Differential)
                    case "958":  // Hazard, Subsistence and Laundry (Differential)
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HZD_DIFF.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "136": // Document Tracking History - RE'
                    case "137": // Document Tracking History - CE'
                    case "138": // Document Tracking History - JO'
                        printreport = "/cryDocTracking/cryDocsHistory.rpt";
                        procedure = "sp_edocument_trk_tbl_history";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",p_doc_ctrl_nbr," + payroll_registry_nbr + ",p_docmnt_type," + "01-P";
                        break;

                    case "968":  // ONE COVID-19 ALLOWANCE (OCA) - 'RE'
                    case "969":  // ONE COVID-19 ALLOWANCE (OCA) - 'CE'
                    case "970":  // ONE COVID-19 ALLOWANCE (OCA) - 'JO'
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "240":  //  ONE COVID-19 ALLOWANCE (OCA) - RE - Remittance
                    case "241":  //  ONE COVID-19 ALLOWANCE (OCA) - CE - Remittance
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA_Remit_RECE.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "242":  //  ONE COVID-19 ALLOWANCE (OCA) - JO - Remittance
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA_Remit_JO.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "971":  // PERFORMANCE BASED BONUS FY 2021 - REGULAR
                    case "972":  // PERFORMANCE BASED BONUS FY 2021 - CASUAL
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_PBB2021.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;
                        
                    case "974":  // Health Emergency Allowance (HEA) - 'RE'
                    case "975":  // Health Emergency Allowance (HEA) - 'CE'
                    case "976":  // Health Emergency Allowance (HEA) - 'JO'
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "243":  //  Health Emergency Allowance (HEA) - RE - Remittance
                    case "244":  //  Health Emergency Allowance (HEA) - CE - Remittance
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA_Remit_RECE.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "245":  //  Health Emergency Allowance (HEA) - JO - Remittance
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA_Remit_JO.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;

                    case "981":  //  RATA Differential
                        printreport = "/cryOtherPayroll/cryOthPay/cryOthPay5_RATADiff.rpt";
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "../../printView/CrystalViewer.aspx?ReportType=&ReportPath=&id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.ToString().Trim() + ",par_payroll_registry_nbr," + payroll_registry_nbr + ",par_payrolltemplate_code," + ddl_select_report.ToString().Trim();
                        break;
                }
                
                return url;
            }
            catch (Exception)
            {
                return url;
            }
        }

        protected void check_reg_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btn_printall_Click(object sender, EventArgs e)
        {
            RetrieveRelatedTemplate();
            GetReportFile();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "openSelectReport();", true);
        }
        [WebMethod]
        public static string CafoaList(string payroll_year, string payroll_registry_nbr, string payrolltemplate_code, string par_cafoa_type)
        {
            try
            {
                CommonDB MyCmn = new CommonDB();
                DataTable dt        = new DataTable();
                DataTable dt_header = new DataTable();
                string query = "SELECT A.payroll_year ,A.payroll_registry_nbr ,A.seq_nbr ,A.function_code ,A.allotment_code ,A.account_code ,A.account_short_title ,A.account_amt,A.created_by_user,A.updated_by_user,A.created_dttm,A.updated_dttm,A.raao_code,A.ooe_code,ISNULL(B.function_shortname, '') AS function_shortname,ISNULL(B.function_name, '') AS function_name,ISNULL(B.function_detail, '') AS function_detail,ISNULL(B.function_program, '') AS function_program FROM HRIS_TRK.dbo.cafao_dtl_tbl A LEFT JOIN HRIS_PAY.dbo.functions_tbl B ON B.function_code = A.function_code WHERE payroll_year = '" + payroll_year + "' AND payroll_registry_nbr = '" + payroll_registry_nbr + "'";
                dt = MyCmn.GetDatatable(query);
                if (par_cafoa_type == "VOUCHER")
                {
                    dt_header = MyCmn.RetrieveData("sp_voucher_obr_rep", "par_payroll_year", payroll_year, "par_voucher_ctrl_nbr", payroll_registry_nbr, "par_payrolltemplate_code", payrolltemplate_code);
                }
                else
                {
                    dt_header = MyCmn.RetrieveData("sp_payrollregistry_cafao_rep_new", "par_payroll_year", payroll_year, "par_payroll_registry_nbr", payroll_registry_nbr, "par_payrolltemplate_code", payrolltemplate_code);
                }
                string json = JsonConvert.SerializeObject(new { dt ,dt_header}, Newtonsoft.Json.Formatting.Indented);
                return json;
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Trace.TraceError(ex.ToString());
                // Optionally, rethrow or handle the error
                throw;
            }
        }
        [WebMethod]
        public static string DepartmentSignatories()
        {
            CommonDB MyCmn = new CommonDB();
            DataTable dt = new DataTable();
            string query = "SELECT A.department_code,A.department_short_name,A.empl_id,UPPER(B.employee_name_format3) AS employee_name_format2,UPPER(A.designation_head1) AS designation_head1,UPPER(A.designation_head2) AS designation_head2,UPPER(A.function_code) AS function_code FROM departments_tbl A INNER JOIN vw_personnelnames2_tbl B ON B.empl_id = A.empl_id";
            query        = query + " UNION SELECT B.department_code,B.department_short_name,A.empl_id,UPPER(A.employee_name) AS employee_name_format2,UPPER(A.position_long_title) AS designation_head1,UPPER(A.position_long_title) AS designation_head2,A.function_code AS function_code FROM vw_payrollemployeemaster_hdr_pos_tbl A INNER JOIN departments_tbl B ON B.department_code = A.department_code WHERE A.empl_id IN ('10631')";
            query        = query + " UNION SELECT B.department_code,B.department_short_name,A.empl_id,UPPER(A.employee_name) AS employee_name_format2,UPPER(A.position_long_title) AS designation_head1,UPPER(A.position_long_title) AS designation_head2,UPPER(A.function_code) AS function_code FROM vw_payrollemployeemaster_hdr_pos_tbl A INNER JOIN departments_tbl B ON B.department_code = A.department_code WHERE A.position_long_title LIKE '%Executive Assistant%' AND A.employment_type = 'RE' AND emp_rcrd_status = 1";
            dt = MyCmn.GetDatatable(query);
            string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return json;
        }
        [WebMethod]
        public static string RetrieveFunctions()
        {
            CommonDB MyCmn = new CommonDB();
            DataTable dt = new DataTable();
            string query = "SELECT *FROM functions_tbl ORDER BY function_name";
            dt = MyCmn.GetDatatable(query);
            string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            return json;
        }

        public class cafao_dtl_tbl
        {
            public string payroll_year           { get; set; }
            public string payroll_registry_nbr   { get; set; }
            public int seq_nbr                   { get; set; }
            public string function_code          { get; set; }
            public string allotment_code         { get; set; }
            public string account_code           { get; set; }
            public string account_short_title    { get; set; }
            public double account_amt            { get; set; }
            public string created_by_user        { get; set; }
            public string updated_by_user        { get; set; }
            public DateTime? created_dttm         { get; set; }
            public DateTime? updated_dttm         { get; set; }
            public string raao_code              { get; set; }
            public string ooe_code               { get; set; }
            public string payrolltemplate_code   { get; set; }
        }
        public class cafao_hdr_tbl
        {
            public string payroll_year           { get; set; }
            public string payroll_registry_nbr   { get; set; }
            public string payee                  { get; set; }
            public string obr_nbr                { get; set; }
            public double? approved_amt           { get; set; }
            public string dv_nbr                 { get; set; }
            public string request_descr          { get; set; }
            public string charged_to             { get; set; }
            public string req_empl_id            { get; set; }
            public string req_name               { get; set; }
            public string req_desig              { get; set; }
            public string budget_empl_id         { get; set; }
            public string budget_name            { get; set; }
            public string budget_desig           { get; set; }
            public string treasurer_empl_id      { get; set; }
            public string treasurer_name         { get; set; }
            public string treasurer_desig        { get; set; }
            public string pacco_empl_id          { get; set; }
            public string pacco_name             { get; set; }
            public string pacco_desig            { get; set; }
            public string created_by_user        { get; set; }
            public string updated_by_user        { get; set; }
            public DateTime? created_dttm        { get; set; }
            public DateTime? updated_dttm        { get; set; }
        }
        [WebMethod]
        public static string saveall_cafoa(string data_dtl, string data_hdr)
        {
            try
            {
                var message = "";

                List<cafao_dtl_tbl> cafoa = JsonConvert.DeserializeObject<List<cafao_dtl_tbl>>(data_dtl);
                cafao_hdr_tbl cafoa_hdr   = JsonConvert.DeserializeObject<cafao_hdr_tbl>(data_hdr);
                if (cafoa.Count > 0 && cafoa_hdr != null)
                {
                    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["hrisConn_trk"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();
                        // DELETE existing records
                        string delete_dtl = "DELETE FROM cafao_dtl_tbl WHERE payroll_year = @payroll_year AND payroll_registry_nbr = @payroll_registry_nbr";
                        using (SqlCommand deleteCmd = new SqlCommand(delete_dtl, con))
                        {
                            deleteCmd.Parameters.AddWithValue("@payroll_year", cafoa_hdr.payroll_year);
                            deleteCmd.Parameters.AddWithValue("@payroll_registry_nbr", cafoa_hdr.payroll_registry_nbr);
                            deleteCmd.ExecuteNonQuery();
                        }
                        string delete_hdr = "DELETE FROM cafao_hdr_tbl WHERE payroll_year = @payroll_year AND payroll_registry_nbr = @payroll_registry_nbr";
                        using (SqlCommand delete_hdr_cmd = new SqlCommand(delete_hdr, con))
                        {
                            delete_hdr_cmd.Parameters.AddWithValue("@payroll_year", cafoa_hdr.payroll_year);
                            delete_hdr_cmd.Parameters.AddWithValue("@payroll_registry_nbr", cafoa_hdr.payroll_registry_nbr);
                            delete_hdr_cmd.ExecuteNonQuery();
                        }
                        
                        string query_insert_hdr = "INSERT INTO cafao_hdr_tbl(payroll_year, payroll_registry_nbr, payee, obr_nbr, approved_amt, dv_nbr, request_descr, charged_to, req_empl_id, req_name, req_desig, budget_empl_id, budget_name, budget_desig, treasurer_empl_id, treasurer_name, treasurer_desig, pacco_empl_id, pacco_name, pacco_desig, created_by_user, updated_by_user, created_dttm, updated_dttm) " +
                                                  "VALUES (@payroll_year, @payroll_registry_nbr, @payee, @obr_nbr, @approved_amt, @dv_nbr, @request_descr, @charged_to, @req_empl_id, @req_name, @req_desig, @budget_empl_id, @budget_name, @budget_desig, @treasurer_empl_id, @treasurer_name, @treasurer_desig, @pacco_empl_id, @pacco_name, @pacco_desig, @created_by_user, @updated_by_user, @created_dttm, @updated_dttm)";
                        using (SqlCommand insert_cmd_hdr = new SqlCommand(query_insert_hdr, con))
                        {
                            insert_cmd_hdr.Parameters.AddWithValue("@payroll_year", cafoa_hdr.payroll_year);
                            insert_cmd_hdr.Parameters.AddWithValue("@payroll_registry_nbr", cafoa_hdr.payroll_registry_nbr);
                            insert_cmd_hdr.Parameters.AddWithValue("@payee", cafoa_hdr.payee);
                            insert_cmd_hdr.Parameters.AddWithValue("@obr_nbr", cafoa_hdr.obr_nbr);
                            insert_cmd_hdr.Parameters.AddWithValue("@approved_amt", cafoa_hdr.approved_amt);
                            insert_cmd_hdr.Parameters.AddWithValue("@dv_nbr", cafoa_hdr.dv_nbr);
                            insert_cmd_hdr.Parameters.AddWithValue("@request_descr", cafoa_hdr.request_descr);
                            insert_cmd_hdr.Parameters.AddWithValue("@charged_to", cafoa_hdr.charged_to);
                            insert_cmd_hdr.Parameters.AddWithValue("@req_empl_id", cafoa_hdr.req_empl_id);
                            insert_cmd_hdr.Parameters.AddWithValue("@req_name", cafoa_hdr.req_name);
                            insert_cmd_hdr.Parameters.AddWithValue("@req_desig", cafoa_hdr.req_desig);
                            insert_cmd_hdr.Parameters.AddWithValue("@budget_empl_id", cafoa_hdr.budget_empl_id);
                            insert_cmd_hdr.Parameters.AddWithValue("@budget_name", cafoa_hdr.budget_name);
                            insert_cmd_hdr.Parameters.AddWithValue("@budget_desig", cafoa_hdr.budget_desig);
                            insert_cmd_hdr.Parameters.AddWithValue("@treasurer_empl_id", cafoa_hdr.treasurer_empl_id);
                            insert_cmd_hdr.Parameters.AddWithValue("@treasurer_name", cafoa_hdr.treasurer_name);
                            insert_cmd_hdr.Parameters.AddWithValue("@treasurer_desig", cafoa_hdr.treasurer_desig);
                            insert_cmd_hdr.Parameters.AddWithValue("@pacco_empl_id", cafoa_hdr.pacco_empl_id);
                            insert_cmd_hdr.Parameters.AddWithValue("@pacco_name", cafoa_hdr.pacco_name);
                            insert_cmd_hdr.Parameters.AddWithValue("@pacco_desig", cafoa_hdr.pacco_desig);
                            insert_cmd_hdr.Parameters.AddWithValue("@created_by_user", HttpContext.Current.Session["ep_user_id"]?.ToString().Trim());
                            insert_cmd_hdr.Parameters.AddWithValue("@updated_by_user", "");
                            insert_cmd_hdr.Parameters.AddWithValue("@created_dttm", DateTime.Now);
                            insert_cmd_hdr.Parameters.AddWithValue("@updated_dttm", "");
                            insert_cmd_hdr.ExecuteNonQuery();
                        }

                        // INSERT records
                        foreach (var item in cafoa)
                        {
                            string query = "INSERT INTO cafao_dtl_tbl (payroll_year, payroll_registry_nbr,seq_nbr,function_code,allotment_code,account_code,account_short_title,account_amt,created_by_user,updated_by_user,created_dttm,updated_dttm,raao_code,ooe_code,payrolltemplate_code) VALUES (@payroll_year, @payroll_registry_nbr,@seq_nbr,@function_code,@allotment_code,@account_code,@account_short_title,@account_amt,@created_by_user,@updated_by_user,@created_dttm,@updated_dttm,@raao_code,@ooe_code,@payrolltemplate_code)";
                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@payroll_year", item.payroll_year);
                                cmd.Parameters.AddWithValue("@payroll_registry_nbr", item.payroll_registry_nbr);
                                cmd.Parameters.AddWithValue("@seq_nbr", item.seq_nbr);
                                cmd.Parameters.AddWithValue("@function_code", item.function_code);
                                cmd.Parameters.AddWithValue("@allotment_code", item.allotment_code);
                                cmd.Parameters.AddWithValue("@account_code", item.account_code);
                                cmd.Parameters.AddWithValue("@account_short_title", item.account_short_title);
                                cmd.Parameters.AddWithValue("@account_amt", item.account_amt);
                                cmd.Parameters.AddWithValue("@created_by_user", HttpContext.Current.Session["ep_user_id"]?.ToString().Trim());
                                cmd.Parameters.AddWithValue("@updated_by_user", "");
                                cmd.Parameters.AddWithValue("@created_dttm", DateTime.Now);
                                cmd.Parameters.AddWithValue("@updated_dttm", "");
                                cmd.Parameters.AddWithValue("@raao_code", item.raao_code == null ? "" : item.raao_code);
                                cmd.Parameters.AddWithValue("@ooe_code", item.ooe_code == null ? "" : item.ooe_code);
                                cmd.Parameters.AddWithValue("@payrolltemplate_code", item.payrolltemplate_code == null ? "" : item.payrolltemplate_code);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    message = "success";
                }
                else
                {
                    message = "no data found!";
                }
                return message;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}