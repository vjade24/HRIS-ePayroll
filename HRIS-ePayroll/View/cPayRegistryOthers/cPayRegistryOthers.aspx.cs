//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Joseph M. Tombo Jr    02/22/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View
{
    public partial class cPayRegistryOthers : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Data Place holder creation 
        //********************************************************************
        
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

        DataTable dataList_installation_tbl
        {
            get
            {
                if ((DataTable)ViewState["dataList_installation_tbl"] == null) return null;
                return (DataTable)ViewState["dataList_installation_tbl"];
            }
            set
            {
                ViewState["dataList_installation_tbl"] = value;
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

                ViewState["page_allow_add"]             = Session["page_allow_add_from_registry"];
                ViewState["page_allow_delete"]          = Session["page_allow_delete_from_registry"];
                ViewState["page_allow_edit"]            = Session["page_allow_edit_from_registry"];
                ViewState["page_allow_edit_history"]    = Session["page_allow_edit_history_from_registry"];
                ViewState["page_allow_print"]           = Session["page_allow_print_from_registry"];

                //********************************************************************
                //  BEGIN - VJA- 06/20/2019 - This is Session Variable Coming From Login
                //********************************************************************
                if (Session["ep_post_authority"].ToString() == "1")
                {
                    ViewState["page_allow_add"]     = 0;
                    ViewState["page_allow_delete"]  = 1;
                    ViewState["page_allow_edit"]    = 1;
                    ViewState["page_allow_print"]   = 1;
                }
                else
                {
                    ViewState["page_allow_add"]     = 1;
                    ViewState["page_allow_delete"]  = 1;
                    ViewState["page_allow_edit"]    = 1;
                    ViewState["page_allow_print"]   = 0;
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

                    //"2019,01,RE,032,0,10,0003,000081"

                    RetrieveEmployeename();
                    RetrieveDataListGrid();
                    RetrievePayrollInstallation();

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
            Session["cPayRegistryOthers"] = "cPayRegistryOthers";

            RetrieveDataListGrid();
            RetrieveEmploymentType();
            RetrieveEmployeename();
        }
        //*************************************************************************
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrievePayrollInstallation()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollinstallation_tbl_list", "par_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_include_history","N");
            hidden_mone_constant_factor.Value   = dt.Rows[0]["mone_constant_factor"].ToString();
            hidden_hours_in_days.Value          = dt.Rows[0]["hours_in_1day_conv"].ToString();
            hidden_monthly_days.Value           = dt.Rows[0]["monthly_salary_days_conv"].ToString();
        }
        //*************************************************************************
        //  BEGIN - JADE- / Populate Combo list for Payroll Year
        //*************************************************************************
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
        //*************************************************************************
        //  BEGIN - JADE- / Populate Combo list for Employee Name
        //*************************************************************************
        private void RetrieveEmployeename()
        {
            ddl_empl_id.Items.Clear();
            div_generic_notes.Visible = true;

            // ***********************************************************
            // **** New Additioal Column - VJA 2021-12-14 ****************
            // **** For CNA Regular Casual and Honorarium for Job-Order 
            // ***********************************************************
            div_other_amount1.Visible = false;
            div_other_amount2.Visible = false;
            div_other_amount3.Visible = false;
            div_other_amount4.Visible = false;
            div_other_amount5.Visible = false;
            // ***********************************************************
            // ***********************************************************
            
            if (ddl_payroll_template.SelectedValue == "029" || ddl_payroll_template.SelectedValue == "048")
            {
                lbl_gross_pay_descr.Text = "Loyalty Amount :";
                dataList_employee = MyCmn.RetrieveData("sp_employee_with_loyalty_bonus_combolist", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR());
                div_net_pay.Visible = false;
                div_other_amount.Visible = false;
                RetrieveGenericNotes_loyalty();
            }
            else if (ddl_payroll_template.SelectedValue == "028" || ddl_payroll_template.SelectedValue == "047")
            {
                lbl_gross_pay_descr.Text = "Clothing Amount :";
                dataList_employee = MyCmn.RetrieveData("sp_employee_with_benefits_combolist", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_template", ddl_payroll_template.SelectedValue.ToString().Trim());
                ddl_generic_notes.Items.Clear();
                ddl_generic_notes.Items.Insert(0, new ListItem("", ""));
                ddl_generic_notes.SelectedIndex = -1;
                div_generic_notes.Visible = false;
                div_other_amount.Visible = false;
            }
            else if (ddl_payroll_template.SelectedValue == "026" || ddl_payroll_template.SelectedValue == "045")
            {
                lbl_gross_pay_descr.Text = "Mid Year Bonus :";
                dataList_employee = MyCmn.RetrieveData("sp_employee_with_benefits_combolist", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_template", ddl_payroll_template.SelectedValue.ToString().Trim());
                lbl_generic_notes.Text = "Performance Rating :";
                RetrieveGenericNotes_PEI();
            }
            else if (ddl_payroll_template.SelectedValue == "031" || ddl_payroll_template.SelectedValue == "050")
            {
                lbl_gross_pay_descr.Text = "PEI Amount :";
                dataList_employee = MyCmn.RetrieveData("sp_employee_with_benefits_combolist", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_template", ddl_payroll_template.SelectedValue.ToString().Trim());
                lbl_generic_notes.Text = "Performance Rating :";
                RetrieveGenericNotes_PEI();
            }
            else if (ddl_payroll_template.SelectedValue == "043" || ddl_payroll_template.SelectedValue == "024" || ddl_payroll_template.SelectedValue == "063")
            {
                lbl_gross_pay_descr.Text = "CEA Amount :";
                dataList_employee = MyCmn.RetrieveData("sp_employee_with_communication_allowance_combolist1", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim());
                RetrieveGenericNotes_CEA();
            }
            else if (ddl_payroll_template.SelectedValue == "046" || ddl_payroll_template.SelectedValue == "027")
            {
                lbl_gross_pay_descr.Text = "Year End Amount :";
                div_other_amount.Visible = true;
                div_generic_notes.Visible = false;
                div_net_pay.Visible = true;
                lbl_net_pay.Text = "Year-End Net Pay :";
                dataList_employee = MyCmn.RetrieveData("sp_employee_with_benefits_combolist", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_template", ddl_payroll_template.SelectedValue.ToString().Trim());
                lbl_other_amount.Text = "Cash Gift Amount :";
                lbl_generic_notes.Text = "Year End Ratings :";
            }
            else if (ddl_payroll_template.SelectedValue == "030" || ddl_payroll_template.SelectedValue == "049")
            {
                lbl_gross_pay_descr.Text = "Anniversary Bonus :";
                dataList_employee = MyCmn.RetrieveData("sp_employee_with_benefits_combolist", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_template", ddl_payroll_template.SelectedValue.ToString().Trim());
                ddl_generic_notes.Items.Clear();
                ddl_generic_notes.Items.Insert(0, new ListItem("", ""));
                ddl_generic_notes.SelectedIndex = -1;
                div_generic_notes.Visible = false;
                div_other_amount.Visible = false;
            }
            else if (ddl_payroll_template.SelectedValue == "051" || ddl_payroll_template.SelectedValue == "032")
            {
                lbl_gross_pay_descr.Text = "Incentive Amount:";
                div_other_amount.Visible = true;
                div_generic_notes.Visible = true;
                div_net_pay.Visible = true;
                ddl_generic_notes.Visible = false;
                txtb_memo.Visible = true;
                lbl_net_pay.Text = "Net Pay :";
                dataList_employee = MyCmn.RetrieveData("sp_personnelnames_combolist_cna", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_template_code", ddl_payroll_template.SelectedValue.ToString().Trim());
                lbl_other_amount.Text = "Agency Fee :";
                lbl_generic_notes.Text = "Remarks:";

                lbl_other_amount1.Font.Size = 10;
                div_other_amount1.Visible = true;
                lbl_other_amount1.Text = "Mortuary Dues c/o PEUCV:";
            }
            else if (ddl_payroll_template.SelectedValue == "034"
                 || ddl_payroll_template.SelectedValue  == "035")
            {
                lbl_gross_pay_descr.Text    = "Honorarium Amount:";
                div_other_amount.Visible    = false;
                div_generic_notes.Visible   = false;
                div_net_pay.Visible         = false;
                ddl_generic_notes.Visible   = false;
                txtb_memo.Visible           = false;
                lbl_net_pay.Text            = "Net Pay :";
                dataList_employee           = MyCmn.RetrieveData("sp_personnelnames_combolist16", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());
                lbl_other_amount.Text       = "Agency Fee :";
                lbl_generic_notes.Text      = "Remarks:";

                //div_monthly.Visible = true;
            }
            else if (ddl_payroll_template.SelectedValue == "062") //  Honorarium - JO
            {
                lbl_gross_pay_descr.Text    = "Honorarium Amount:";
                div_other_amount.Visible    = false;
                div_generic_notes.Visible   = true;
                div_net_pay.Visible         = true;
                ddl_generic_notes.Visible   = false;
                txtb_memo.Visible           = true;
                lbl_net_pay.Text            = "Net Pay :";
                dataList_employee           = MyCmn.RetrieveData("sp_personnelnames_combolist_honorarium", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());
                lbl_other_amount.Text       = "Agency Fee :";
                lbl_generic_notes.Text      = "No. of Months Rendered:";

                lbl_other_amount1.Font.Size = 10;
                div_other_amount1.Visible = true;
                lbl_other_amount1.Text = "Mortuary Dues c/o PEUCV:";
                //div_monthly.Visible = true;
            }

            else if (ddl_payroll_template.SelectedValue == "025" || ddl_payroll_template.SelectedValue == "044")
            {
                div_gross_pay.Visible           = false;
                lbl_gross_pay_descr.Text        = "Monetization Amount:";
                div_other_amount.Visible        = true;
                div_generic_notes.Visible       = false;
                div_net_pay.Visible             = false;
                ddl_generic_notes.Visible       = false;
                txtb_memo.Visible               = false;
                //div_gross_pay.Visible           = false;
                lbl_net_pay.Text                = "Net Pay :";
                dataList_employee               = MyCmn.RetrieveData("sp_personnelnames_combolist16", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());
                lbl_other_amount.Text           = "No of Days:";
                lbl_generic_notes.Text          = "Remarks:";
                //div_monthly.Visible = true;
            }

            ddl_empl_id.DataSource              = dataList_employee;
            ddl_empl_id.DataValueField          = "empl_id";
            ddl_empl_id.DataTextField           = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - JADE- / Populate Combo list for Employment Type
        //*************************************************************************
        private void RetrieveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            ddl_generic_notes.Visible       = true;
            DataTable dt                    = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");
            ddl_empl_type.DataSource        = dt;
            ddl_empl_type.DataTextField     = "employmenttype_description";
            ddl_empl_type.DataValueField    = "employment_type";
            ddl_empl_type.DataBind();
            ddl_empl_type.Enabled           = false;
        }
        //*************************************************************************
        //  BEGIN - JADE- / Populate Combo list for Note Generic for Year End
        //*************************************************************************
        private void RetrieveGenericNotes_loyalty()
        {
            lbl_generic_notes.Text = "Year's In  Service :";
            
            ddl_generic_notes.Visible           = true;
            txtb_memo.Visible                   = false;
            ddl_generic_notes.Items.Clear();
            DataTable dt                        = MyCmn.RetrieveData("sp_loyaltybonuses_tbl_list");
            ddl_generic_notes.DataSource        = dt;
            ddl_generic_notes.DataTextField     =  "years_of_tenure";
            ddl_generic_notes.DataValueField    = "years_of_tenure";
            ddl_generic_notes.DataBind();
        }
        //*************************************************************************
        //  BEGIN - JADE- / Populate Combo list for Note Generic for CEA
        //*************************************************************************
        private void RetrieveGenericNotes_CEA()
        {
            lbl_generic_notes.Text = "CEA Memo :";
            ddl_generic_notes.Visible = false;
            txtb_memo.Visible = true;
        }
        //*************************************************************************
        //  BEGIN - JADE- / Populate Combo list for Note Generic for PEI
        //*************************************************************************
        private void RetrieveGenericNotes_PEI()
        {
            ddl_generic_notes.Items.Clear();
            ddl_generic_notes.Visible = true;
            txtb_memo.Visible = false;
            ListItem li = new ListItem();
                     li = new ListItem("Select Here", "");
            ddl_generic_notes.Items.Insert(0, li);
                     li = new ListItem("Outstanding", "O");
            ddl_generic_notes.Items.Insert(1, li);
                     li = new ListItem("Very Satisfactory", "VS");
            ddl_generic_notes.Items.Insert(2, li);
                     li = new ListItem("Satisfactory", "S");
            ddl_generic_notes.Items.Insert(3, li);
                     li = new ListItem("Unsatisfactory", "US");
            ddl_generic_notes.Items.Insert(4, li);
                     li = new ListItem("Poor", "P");
            ddl_generic_notes.Items.Insert(5, li);
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

            ddl_payroll_template.DataSource     = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField  = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_dtl_oth_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(),"par_payroll_registry_nbr",ddl_payroll_group.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            reshow_gridview_appearance();

            up_dataListGrid.Update();
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            //initialize table for saving in payrollregistry_jo_dtl_tbl
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();
            
            RetrieveEmployeename();
            
            btnSave.Visible             = true;
            txtb_voucher_nbr.Enabled    = false;
            ddl_empl_id.Visible         = true;
            txtb_employeename.Visible   = false;
            //Get last row of the Column
            LabelAddEdit.Text = "Add Record | Registry No: " + lbl_registry_number.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");
            ToogleTextbox(true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_monthly_rate.Text          = "0.00";
            txtb_hourly_rate.Text           = "0.00";
            txtb_daily_rate.Text            = "0.00";
            txtb_gross_pay.Text             = "0.00";
            txtb_net_pay.Text               = "0.00";
            txtb_other_amount.Text          = "0.00";
            txtb_memo.Text                  = "";
            ddl_empl_id.SelectedIndex       = -1;
            txtb_empl_id.Text               = "";

            txtb_department_descr.Text      = "";
            txtb_voucher_nbr.Text           = "";
            ViewState["created_by_user"]    = "";
            ViewState["updated_by_user"]    = "";
            ViewState["posted_by_user"]     = "";
            ViewState["created_dttm"]       = "";
            ViewState["updated_dttm"]       = "";
            txtb_date_posted.Text           = "";
            txtb_position.Text              = "";
            txtb_status.Text                = "";
            lbl_if_dateposted_yes.Text      = "";
            lbl_post_status.Text            = "";
            lbl_rate_basis_descr.Text       = "Rate Basis:";

            txtb_other_amount1.Text = "0.00";
            txtb_other_amount2.Text = "0.00";
            txtb_other_amount3.Text = "0.00";
            txtb_other_amount4.Text = "0.00";
            txtb_other_amount5.Text = "0.00";
            FieldValidationColorChanged(false, "ALL");
        }
        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Initialized Table
        //*************************************************************************
        private void InitializeTable_dtl()
        {
            dtSource_dtl = new DataTable();
            dtSource_dtl.Columns.Add("payroll_year", typeof(System.String));
            dtSource_dtl.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl.Columns.Add("rate_basis", typeof(System.String));
            dtSource_dtl.Columns.Add("monthly_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("daily_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("hourly_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("gross_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("net_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("other_amount", typeof(System.String));
            dtSource_dtl.Columns.Add("notes_generic", typeof(System.String));
            dtSource_dtl.Columns.Add("post_status", typeof(System.String));
            dtSource_dtl.Columns.Add("date_posted", typeof(System.String));

            dtSource_dtl.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource_dtl.Columns.Add("created_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("posted_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("created_dttm", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_dttm", typeof(System.String));

            dtSource_dtl.Columns.Add("other_amount1", typeof(System.String));
            dtSource_dtl.Columns.Add("other_amount2", typeof(System.String));
            dtSource_dtl.Columns.Add("other_amount3", typeof(System.String));
            dtSource_dtl.Columns.Add("other_amount4", typeof(System.String));
            dtSource_dtl.Columns.Add("other_amount5", typeof(System.String));

        }
        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys_dtl()
        {
            dtSource_dtl.TableName = "payrollregistry_dtl_oth_tbl";
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr", "empl_id" };
            dtSource_dtl = MyCmn.AddPrimaryKeys(dtSource_dtl, col);
            dtSource_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));
        }
        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow_dtl()
        {
            DataRow nrow = dtSource_dtl.NewRow();
            nrow["payroll_year"]            = string.Empty;
            nrow["payroll_registry_nbr"]    = string.Empty;
            nrow["empl_id"]                 = string.Empty;
            nrow["rate_basis"]              = string.Empty;
            nrow["monthly_rate"]            = string.Empty;
            nrow["daily_rate"]              = string.Empty;
            nrow["hourly_rate"]             = string.Empty;
            nrow["gross_pay"]               = string.Empty;
            nrow["net_pay"]                 = string.Empty;
            nrow["other_amount"]            = string.Empty;
            nrow["notes_generic"]           = string.Empty;
            nrow["post_status"]             = string.Empty;
            nrow["date_posted"]             = string.Empty;

            nrow["voucher_nbr"]             = string.Empty;
            nrow["created_by_user"]         = string.Empty;
            nrow["updated_by_user"]         = string.Empty;
            nrow["posted_by_user"]          = string.Empty;
            nrow["created_dttm"]            = string.Empty;
            nrow["updated_dttm"]            = string.Empty;

            nrow["other_amount1"]            = string.Empty;
            nrow["other_amount2"]            = string.Empty;
            nrow["other_amount3"]            = string.Empty;
            nrow["other_amount4"]            = string.Empty;
            nrow["other_amount5"]            = string.Empty;

            nrow["action"]                  = 1;
            nrow["retrieve"]                = false;
            dtSource_dtl.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id              = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year         = commandArgs[2];
            txtb_reason.Text            = "";
            FieldValidationColorChanged(false, "ALL");
            if (Session["ep_post_authority"].ToString() == "0")
            {
                // This is the Normal Delete if the user is Non-accounting user
                deleteRec1.Text         = "Are you sure to delete this Record ?";
                deleteRec0.InnerText    = "Delete this Record ?";
                lbl_unposting.Text      = "";
                txtb_reason.Visible     = false;
                lnkBtnYes.Text          = "Yes, Delete it";
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                // This is Message if the accounting user will unpost the card 
                deleteRec1.Text         = "Are you sure you want to UnPost this Record ?";
                deleteRec0.InnerText    = "UnPost this Record ?";
                lbl_unposting.Text      = "Reason for UnPosting :";
                txtb_reason.Visible     = true;
                lnkBtnYes.Text          = "Yes, UnPost it";
            }
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "'";

            if (Session["ep_post_authority"].ToString() == "0")
            {
                // Non-Accounting Delete 
                MyCmn.DeleteBackEndData("payrollregistry_dtl_oth_tbl", "WHERE " + deleteExpression);
                DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
                dataListGrid.Rows.Remove(row2Delete[0]);
                dataListGrid.AcceptChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                if (txtb_reason.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_reason");
                }
                else
                {
                    // Stored Procedure to Insert to payrollregistry_dtl_unpost_tbl during accounting case
                    DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_unpost_tbl_insert", "par_payroll_year", ddl_year.SelectedValue, "par_payroll_registry_nbr", commandarg[1].Trim(), "par_empl_id", commandarg[0].Trim(), "par_reason", txtb_reason.Text);

                    //4.4.b.Update the following fields: From payrollregistry_dtl_oth_tbl Table
                    //    1.voucher_nbr     =   { blank}
                    //    2.posted_by_user  =   { blank}
                    //    3.post_status     =   "N"   
                    //    4.date_posted     =   { blank}
                    //    5.updated_by_user =   Session User ID
                    //    6.updated_dttm    =   System Date

                    string setparams = "";
                    setparams = "voucher_nbr = '',posted_by_user = '',post_status='N',date_posted='',updated_by_user= '" + Session["ep_user_id"].ToString() + "',updated_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    MyCmn.UpdateTable("payrollregistry_dtl_oth_tbl", setparams, "WHERE " + deleteExpression);
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

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            //initialize table for saving in payrollregistry_jo_dtl_tbl
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();
            dtSource_dtl.Rows[0]["action"]      = 2;
            dtSource_dtl.Rows[0]["retrieve"]    = true;
            
            RetrieveEmployeename();

            //ddl_empl_id.SelectedValue = row2Edit[0]["empl_id"].ToString().Trim();
            txtb_empl_id.Text       = row2Edit[0]["empl_id"].ToString().Trim();
            txtb_employeename.Text  = row2Edit[0]["employee_name"].ToString().Trim();
           
            txtb_net_pay.Text                   = double.Parse(row2Edit[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");
            txtb_gross_pay.Text                 = double.Parse(row2Edit[0]["gross_pay"].ToString().Trim()).ToString("###,##0.00");
            txtb_other_amount.Text              = row2Edit[0]["other_amount"].ToString().Trim();

            dtSource_dtl.Rows[0]["post_status"] = row2Edit[0]["post_status"].ToString().Trim();
            dtSource_dtl.Rows[0]["date_posted"] = row2Edit[0]["date_posted"].ToString().Trim();

            //set_max_amount();

            if (   ddl_payroll_template.SelectedValue == "031" || ddl_payroll_template.SelectedValue == "050"
                || ddl_payroll_template.SelectedValue == "029" || ddl_payroll_template.SelectedValue == "048"
                || ddl_payroll_template.SelectedValue == "026" || ddl_payroll_template.SelectedValue == "045")
            {
                ddl_generic_notes.SelectedValue = row2Edit[0]["notes_generic"].ToString().Trim();
            }

            if (ddl_payroll_template.SelectedValue    == "043"     // Communicaion Expense - RE
                || ddl_payroll_template.SelectedValue == "024"  // Communicaion Expense - CE
                || ddl_payroll_template.SelectedValue == "063"  // Communicaion Expense - JO
                || ddl_payroll_template.SelectedValue == "032"  // C.N.A Regular and Casual
                || ddl_payroll_template.SelectedValue == "051"  // C.N.A Regular and Casual
                || ddl_payroll_template.SelectedValue == "062"  // Honorarium - JO
                )
            {
                txtb_memo.Text = row2Edit[0]["notes_generic"].ToString().Trim();
            }
            
            LabelAddEdit.Text           = "Edit Record | Registry No: " + lbl_registry_number.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ddl_empl_id.Visible         = false;
            txtb_employeename.Visible   = true;
            FieldValidationColorChanged(false, "ALL");

            lbl_rate_basis_descr.Text   = row2Edit[0]["rate_basis_descr"].ToString() + " Rate :";
            txtb_monthly_rate.Text      = row2Edit[0]["monthly_rate"].ToString();
            txtb_hourly_rate.Text       = row2Edit[0]["hourly_rate"].ToString();
            txtb_daily_rate.Text        = row2Edit[0]["daily_rate"].ToString();
            hidden_rate_basis.Value     = row2Edit[0]["rate_basis"].ToString();
                
            switch (row2Edit[0]["rate_basis"].ToString())
            {
                case "M":
                    {
                        div_monthly.Visible = false;
                        div_daily.Visible = false;
                        div_hourly.Visible = false;
                        div_monthly.Visible = true;
                        break;
                    }
                case "D":
                    {
                        div_monthly.Visible = false;
                        div_daily.Visible = false;
                        div_hourly.Visible = false;
                        div_daily.Visible = true;
                        break;
                    }
                case "H":
                    {
                        div_monthly.Visible = false;
                        div_daily.Visible = false;
                        div_hourly.Visible = false;
                        div_hourly.Visible = true;
                        break;
                    }
                default:
                    {
                        div_monthly.Visible = false;
                        div_daily.Visible   = false;
                        div_hourly.Visible  = false;
                        break;
                    }
            }
            
            // Add Field Again - 06/20/2019
            txtb_voucher_nbr.Text           = row2Edit[0]["voucher_nbr"].ToString();
            ViewState["created_by_user"]    = row2Edit[0]["created_by_user"].ToString();
            ViewState["updated_by_user"]    = row2Edit[0]["updated_by_user"].ToString();
            ViewState["posted_by_user"]     = row2Edit[0]["posted_by_user"].ToString();
            ViewState["created_dttm"]       = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            ViewState["updated_dttm"]       = Convert.ToDateTime(row2Edit[0]["updated_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            //ddl_status.SelectedValue        = row2Edit[0]["post_status"].ToString();
            txtb_status.Text                = row2Edit[0]["post_status_descr"].ToString();
            lbl_post_status.Text                = row2Edit[0]["post_status_descr"].ToString();
            txtb_date_posted.Text           = row2Edit[0]["date_posted"].ToString();
            txtb_position.Text              = row2Edit[0]["position_title1"].ToString();
            txtb_department_descr.Text      = row2Edit[0]["department_name1"].ToString();


            txtb_other_amount1.Text = row2Edit[0]["other_amount1"].ToString().Trim();
            txtb_other_amount2.Text = row2Edit[0]["other_amount2"].ToString().Trim();
            txtb_other_amount3.Text = row2Edit[0]["other_amount3"].ToString().Trim();
            txtb_other_amount4.Text = row2Edit[0]["other_amount4"].ToString().Trim();
            txtb_other_amount5.Text = row2Edit[0]["other_amount5"].ToString().Trim();

            // The Save Button Will be Visible false if the 
            if (row2Edit[0]["post_status"].ToString() == "Y" && (Session["ep_post_authority"].ToString() == "1" || Session["ep_post_authority"].ToString() == "0"))
            {
                // For Accounting or Other User With Y Status
                btnSave.Visible = false;
                Linkbtncancel.Text = "Close";
                txtb_voucher_nbr.Enabled = false;
                lbl_if_dateposted_yes.Text = "This Payroll Already Posted, You cannot Edit!";
                btnSave.Text = "Save";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            else if ((row2Edit[0]["post_status"].ToString() == "N" || row2Edit[0]["post_status"].ToString() == "") && Session["ep_post_authority"].ToString() == "1")
            {
                // For Accounting With N Status
                btnSave.Visible = true;
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = true;
                btnSave.Text = "Post to Card";
                txtb_voucher_nbr.Enabled = true;
                txtb_date_posted.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }
            // The Save Button Will be Visible false if the Status is Released
            else if (row2Edit[0]["post_status"].ToString() == "R"
                   // || row2Edit[0]["post_status"].ToString() == "T"
                    || row2Edit[0]["post_status"].ToString() == "X"
                    )
            {
                // For Accounting or Other User With Y Status
                btnSave.Visible = false;
                Linkbtncancel.Text = "Close";
                txtb_voucher_nbr.Enabled = false;
                lbl_if_dateposted_yes.Text = "This Payroll Already " + row2Edit[0]["post_status_descr"].ToString() + ", You cannot Edit!";
                btnSave.Text = "Save";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            else if (Session["ep_post_authority"].ToString() == "0")
            {
                // For Other Users
                btnSave.Visible = true;
                Linkbtncancel.Text = "Cancel";
                txtb_voucher_nbr.Enabled = true;
                lbl_if_dateposted_yes.Text = "";
                btnSave.Text = "Save";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = "";
                ToogleTextbox(true);
            }
            UpdatePanel10.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
            FieldValidationColorChanged(false, "ALL");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;

            if (IsDataValidated())
            {
                calculate_net_pay();
                DataRow[] selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

                /* Template Codes And its Value For Casual And Regular Employment Type
                 * ----------------------------------------------------------------------------------------
                 *  RE TEMPLATE CODES  |   CE TEMPLATE CODES    |   JO TEMPLATE CODES | Description                               
                 *  -------------------+------------------------+------------------------+------------------------------------------
                 *  024                |   043                  |   063                  | Communication Expense Allowance
                 *  -------------------+------------------------+------------------------+------------------------------------------
                 *  026                |   045                  |                        | Mid Year Bunos                           
                 *  -------------------+------------------------+------------------------+------------------------------------------
                 *  027                |   046                  |                        | Year-End And Cash Gift Bunos
                 *  -------------------+------------------------+------------------------+------------------------------------------
                 *  028                |   047                  |                        | Clothing Allowance
                 *  -------------------+------------------------+------------------------+------------------------------------------
                 *  029                |   048                  |                        | Loyalty Bunos
                 *  -------------------+------------------------+------------------------+------------------------------------------
                 *  030                |   049                  |                        | Anniversary Bunos
                 *  -------------------+------------------------+------------------------+------------------------------------------
                 *  031                |   050                  |                        | Productivity Enhancement Incentive Bunos
                 *  -------------------+------------------------+------------------------+------------------------------------------
                 *  032                |   051                  |                        | CNA Incentive Bunos
                 *  -------------------+------------------------+-------------------------------------------------------------------
                 *  034                |   035                  |   062                  | Honorarium
                 *  -------------------+------------------------+-------------------------------------------------------------------
                 *  025                |   044                  |                        | Monetization
                 *  ----------------------------------------------------------------------------------------------------------------
                 */

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource_dtl.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["empl_id"]                 = ddl_empl_id.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["rate_basis"]              = hidden_rate_basis.Value;
                    dtSource_dtl.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["daily_rate"]              = txtb_daily_rate.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hourly_rate"]             = txtb_hourly_rate.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount"]            = txtb_other_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["post_status"]             = "N";

                    if (   ddl_payroll_template.SelectedValue == "024" 
                        || ddl_payroll_template.SelectedValue == "026"
                        || ddl_payroll_template.SelectedValue == "028" 
                        || ddl_payroll_template.SelectedValue == "029"
                        || ddl_payroll_template.SelectedValue == "030" 
                        || ddl_payroll_template.SelectedValue == "031"
                        || ddl_payroll_template.SelectedValue == "047" 
                        || ddl_payroll_template.SelectedValue == "048"
                        || ddl_payroll_template.SelectedValue == "045" 
                        || ddl_payroll_template.SelectedValue == "049"
                        || ddl_payroll_template.SelectedValue == "050" 
                        || ddl_payroll_template.SelectedValue == "043"   
                        || ddl_payroll_template.SelectedValue == "063")  // Communication Expense - JO
                    {
                        dtSource_dtl.Rows[0]["net_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                    }
                    else if (ddl_payroll_template.SelectedValue == "034"
                        || ddl_payroll_template.SelectedValue == "035")
                    {
                        dtSource_dtl.Rows[0]["net_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                    }
                    else if (ddl_payroll_template.SelectedValue == "025" || ddl_payroll_template.SelectedValue == "044")
                    {
                        // For MOnetization Regur and Casual
                        dtSource_dtl.Rows[0]["net_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                    }
                    else
                    {
                        if (ddl_payroll_template.SelectedValue == "027" || ddl_payroll_template.SelectedValue == "046")
                        {
                            txtb_net_pay.Text = (double.Parse(txtb_gross_pay.Text) + double.Parse(txtb_other_amount.Text)).ToString();
                            dtSource_dtl.Rows[0]["net_pay"] = txtb_net_pay.Text.ToString().Trim();
                            //txtb_gross_pay.Text = txtb_monthly_rate.Text.ToString();
                            dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        }
                        else if (ddl_payroll_template.SelectedValue == "032" || ddl_payroll_template.SelectedValue == "051")
                        {
                            // txtb_net_pay.Text = (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_other_amount.Text)).ToString();
                            dtSource_dtl.Rows[0]["net_pay"] = txtb_net_pay.Text.ToString().Trim();
                            dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        }
                        else
                        {
                            dtSource_dtl.Rows[0]["net_pay"] = txtb_net_pay.Text.ToString().Trim();
                            dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        }
                    }

                    if (ddl_payroll_template.SelectedValue.ToString().Trim() == "043" || 
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "024" ||
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "032" || 
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "051" ||
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "062" || // Honorarium - JO
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "063") // Communication Expense - JO
                    {
                        dtSource_dtl.Rows[0]["notes_generic"] = txtb_memo.Text.ToString().Trim();
                    }
                    else
                    {
                        dtSource_dtl.Rows[0]["notes_generic"] = ddl_generic_notes.SelectedValue.ToString().Trim();
                    }

                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource_dtl.Rows[0]["voucher_nbr"]             = txtb_voucher_nbr.Text.ToString();
                    dtSource_dtl.Rows[0]["created_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource_dtl.Rows[0]["updated_by_user"]         = "";

                    dtSource_dtl.Rows[0]["created_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource_dtl.Rows[0]["updated_dttm"]            = "";

                    dtSource_dtl.Rows[0]["posted_by_user"]          = "";
                    dtSource_dtl.Rows[0]["date_posted"]             = "";

                    dtSource_dtl.Rows[0]["other_amount1"] = txtb_other_amount1.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount2"] = txtb_other_amount2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount3"] = txtb_other_amount3.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount4"] = txtb_other_amount4.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount5"] = txtb_other_amount5.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource_dtl);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource_dtl.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["rate_basis"]              = hidden_rate_basis.Value;

                    dtSource_dtl.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["daily_rate"]              = txtb_daily_rate.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hourly_rate"]             = txtb_hourly_rate.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount"]            = txtb_other_amount.Text.ToString().Trim();

                    if (   ddl_payroll_template.SelectedValue == "024" 
                        || ddl_payroll_template.SelectedValue == "026"
                        || ddl_payroll_template.SelectedValue == "028" 
                        || ddl_payroll_template.SelectedValue == "029"
                        || ddl_payroll_template.SelectedValue == "030" 
                        || ddl_payroll_template.SelectedValue == "031"
                        || ddl_payroll_template.SelectedValue == "047" 
                        || ddl_payroll_template.SelectedValue == "048"
                        || ddl_payroll_template.SelectedValue == "045" 
                        || ddl_payroll_template.SelectedValue == "049"
                        || ddl_payroll_template.SelectedValue == "050" 
                        || ddl_payroll_template.SelectedValue == "043"
                        || ddl_payroll_template.SelectedValue == "063")  // Communication Expense - JO
                    {
                        dtSource_dtl.Rows[0]["net_pay"]             = txtb_gross_pay.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["gross_pay"]           = txtb_gross_pay.Text.ToString().Trim();
                    }
                    else if (ddl_payroll_template.SelectedValue == "034"
                        || ddl_payroll_template.SelectedValue == "035")
                    {
                        dtSource_dtl.Rows[0]["net_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                    }
                    else if (ddl_payroll_template.SelectedValue == "025" || ddl_payroll_template.SelectedValue == "044")
                    {
                        // For MOnetization Regur and Casual
                        dtSource_dtl.Rows[0]["net_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                    }
                    else
                    {
                        if (ddl_payroll_template.SelectedValue == "027" || ddl_payroll_template.SelectedValue == "046")
                        {
                            txtb_net_pay.Text = (double.Parse(txtb_gross_pay.Text) + double.Parse(txtb_other_amount.Text)).ToString();
                            dtSource_dtl.Rows[0]["net_pay"] = txtb_net_pay.Text.ToString().Trim();
                           // txtb_gross_pay.Text = txtb_monthly_rate.Text.ToString();
                            dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        }
                        else if (ddl_payroll_template.SelectedValue == "032" || ddl_payroll_template.SelectedValue == "051")
                        {
                            // txtb_net_pay.Text = (double.Parse(txtb_gross_pay.Text) - (double.Parse(txtb_other_amount.Text) + double.Parse(txtb_other_amount1.Text))).ToString();
                            dtSource_dtl.Rows[0]["net_pay"] = txtb_net_pay.Text.ToString().Trim();
                            dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        }
                        else
                        {
                            dtSource_dtl.Rows[0]["net_pay"] = txtb_net_pay.Text.ToString().Trim();
                            dtSource_dtl.Rows[0]["gross_pay"] = txtb_gross_pay.Text.ToString().Trim();
                        }
                    }

                    if (ddl_payroll_template.SelectedValue.ToString().Trim() == "043" || 
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "024" ||
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "032" || 
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "051" ||
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "062" || // Honorarium - JO
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "063") // Communication Expense - JO
                    {
                        dtSource_dtl.Rows[0]["notes_generic"] = txtb_memo.Text.ToString().Trim();
                    }
                    else
                    {
                        dtSource_dtl.Rows[0]["notes_generic"] = ddl_generic_notes.SelectedValue.ToString().Trim();
                    }

                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource_dtl.Rows[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                    dtSource_dtl.Rows[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource_dtl.Rows[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource_dtl.Rows[0]["created_dttm"]            = ViewState["created_dttm"].ToString();

                    if (Session["ep_post_authority"].ToString() == "1")
                    {
                        dtSource_dtl.Rows[0]["posted_by_user"]  = Session["ep_user_id"].ToString();
                        dtSource_dtl.Rows[0]["date_posted"]     = txtb_date_posted.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["post_status"]     = "Y";
                        dtSource_dtl.Rows[0]["voucher_nbr"]     = txtb_voucher_nbr.Text.ToString();
                        dtSource_dtl.Rows[0]["updated_by_user"] = ViewState["updated_by_user"].ToString();
                        dtSource_dtl.Rows[0]["updated_dttm"]    = ViewState["updated_dttm"].ToString();
                    }

                    dtSource_dtl.Rows[0]["other_amount1"] = txtb_other_amount1.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount2"] = txtb_other_amount2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount3"] = txtb_other_amount3.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount4"] = txtb_other_amount4.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_amount5"] = txtb_other_amount5.Text.ToString().Trim();
                    
                    scriptInsertUpdate = MyCmn.updatescript(dtSource_dtl);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    if (scriptInsertUpdate == string.Empty) { reshow_gridview_appearance(); return; }
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") { reshow_gridview_appearance(); return; }

                    if (msg.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist");
                        reshow_gridview_appearance();
                        return;
                    }

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                       
                        nrow["payroll_year"]            = dtSource_dtl.Rows[0]["payroll_year"];
                        nrow["payroll_registry_nbr"]    = dtSource_dtl.Rows[0]["payroll_registry_nbr"];
                        nrow["empl_id"]                 = ddl_empl_id.SelectedValue.ToString().Trim();
                        nrow["employee_name"]           = ddl_empl_id.SelectedItem.Text.ToString().Trim();
                        nrow["rate_basis"]              = dtSource_dtl.Rows[0]["rate_basis"];
                        nrow["monthly_rate"]            = double.Parse(dtSource_dtl.Rows[0]["monthly_rate"].ToString().Trim()).ToString("###,##0.00");
                        nrow["daily_rate"]              = double.Parse(dtSource_dtl.Rows[0]["daily_rate"].ToString().Trim()).ToString("###,##0.00");
                        nrow["net_pay"]                 = double.Parse(dtSource_dtl.Rows[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");
                        nrow["hourly_rate"]             = double.Parse(dtSource_dtl.Rows[0]["hourly_rate"].ToString().Trim()).ToString("###,##0.00");
                        nrow["net_pay"]                 = double.Parse(dtSource_dtl.Rows[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");
                        nrow["gross_pay"]               = double.Parse(dtSource_dtl.Rows[0]["gross_pay"].ToString().Trim()).ToString("###,##0.00");
                        nrow["notes_generic"]           = dtSource_dtl.Rows[0]["notes_generic"];
                        
                        if (ddl_payroll_template.SelectedValue.ToString().Trim() == "025" || ddl_payroll_template.SelectedValue == "044")
                        {
                            nrow["other_amount"] = double.Parse(dtSource_dtl.Rows[0]["other_amount"].ToString().Trim()).ToString("###,##0.000");
                        }
                        else
                        {
                            nrow["other_amount"] = double.Parse(dtSource_dtl.Rows[0]["other_amount"].ToString().Trim()).ToString("###,##0.00");
                        }

                        switch (dtSource_dtl.Rows[0]["rate_basis"].ToString())
                        {
                            case "M":
                                {
                                    nrow["rate_display"] = txtb_monthly_rate.Text.ToString()+ " / <small style='color:green;font-weight:bold;'>Month</small>";
                                    break;
                                }
                            case "D":
                                {
                                    nrow["rate_display"] = txtb_daily_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Day</small>";
                                    break;
                                }
                            case "H":
                                {
                                    nrow["rate_display"] = txtb_daily_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Hour</small>";
                                    break;
                                }
                            default:
                                {
                                    nrow["rate_display"] = "";
                                    break;
                                }
                        }

                        switch (ddl_generic_notes.SelectedValue.ToString().Trim())
                        {
                            case "O":
                                {
                                    nrow["notes_generic_descr"] = "<span class='text-success'> <i class='fa fa-thumbs-up text-primary'></i>Outstading</span>";
                                    break;
                                }
                            case "VS":
                                {
                                    nrow["notes_generic_descr"] = "<span class='text-success'> <i class='fa fa-thumbs-up text-primary'></i>Very Satisfactory</span>";
                                    break;
                                }
                            case "S":
                                {
                                    nrow["notes_generic_descr"] = "<span class='text-success'> <i class='fa fa-thumbs-up text-warning'></i>Satisfactory</span>";
                                    break;
                                }
                            case "US":
                                {
                                    nrow["notes_generic_descr"] = "<span class='text-danger'> <i class='fa fa-thumbs-down text-danger'></i>Unsatisfactory</span>";
                                    break;
                                }
                            case "P":
                                {
                                    nrow["notes_generic_descr"] = "<span class='text-danger'> <i class='fa fa-thumbs-down'></i>Poor</span>";
                                    break;
                                }
                            default:
                                {
                                    if (ddl_payroll_template.SelectedValue.ToString().Trim() == "043" || 
                                        ddl_payroll_template.SelectedValue.ToString().Trim() == "024" ||
                                        ddl_payroll_template.SelectedValue.ToString().Trim() == "062" || // Honorarium - JO
                                        ddl_payroll_template.SelectedValue.ToString().Trim() == "063") // Communication Expense - JO
                                    {
                                        nrow["notes_generic_descr"] = txtb_memo.Text.ToString().Trim();
                                    }
                                    else
                                    {
                                        nrow["notes_generic_descr"] = ddl_generic_notes.SelectedValue.ToString().Trim();
                                    }
                                    break;
                                }
                        }

                        // BEGIN - Add Field Again  - 06/20/2019
                        nrow["post_status"]             = "N";
                        nrow["post_status_descr"]       = "NOT POSTED";
                        nrow["voucher_nbr"]             = txtb_voucher_nbr.Text.ToString();
                        nrow["created_by_user"]         = Session["ep_user_id"].ToString();
                        nrow["updated_by_user"]         = "";
                        nrow["created_dttm"]            = DateTime.Now;
                        nrow["updated_dttm"]            = Convert.ToDateTime("1900-01-01");
                        nrow["posted_by_user"]          = "";
                        nrow["date_posted"]             = "";
                        nrow["position_title1"]         = txtb_position.Text.ToString();
                        nrow["department_name1"]        = txtb_department_descr.Text.ToString();
                        //nrow["rate_basis_descr"]        = lbl_rate_basis_descr.Text.ToString() + " Rate :";

                        nrow["other_amount1"] = double.Parse(dtSource_dtl.Rows[0]["other_amount1"].ToString().Trim()).ToString("###,##0.00");
                        nrow["other_amount2"] = double.Parse(dtSource_dtl.Rows[0]["other_amount2"].ToString().Trim()).ToString("###,##0.00");
                        nrow["other_amount3"] = double.Parse(dtSource_dtl.Rows[0]["other_amount3"].ToString().Trim()).ToString("###,##0.00");
                        nrow["other_amount4"] = double.Parse(dtSource_dtl.Rows[0]["other_amount4"].ToString().Trim()).ToString("###,##0.00");
                        nrow["other_amount5"] = double.Parse(dtSource_dtl.Rows[0]["other_amount5"].ToString().Trim()).ToString("###,##0.00");

                        dataListGrid.Rows.Add(nrow);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND payroll_year='" + ddl_year.SelectedValue.ToString().Trim() + "' AND payroll_registry_nbr ='" + lbl_registry_number.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"]            = dtSource_dtl.Rows[0]["payroll_year"];
                        row2Edit[0]["payroll_registry_nbr"]    = dtSource_dtl.Rows[0]["payroll_registry_nbr"];
                        row2Edit[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employee_name"]           = txtb_employeename.Text.ToString().Trim();
                        row2Edit[0]["rate_basis"]              = dtSource_dtl.Rows[0]["rate_basis"];
                        row2Edit[0]["monthly_rate"]            = double.Parse(dtSource_dtl.Rows[0]["monthly_rate"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["daily_rate"]              = double.Parse(dtSource_dtl.Rows[0]["daily_rate"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["hourly_rate"]             = double.Parse(dtSource_dtl.Rows[0]["hourly_rate"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["net_pay"]                 = double.Parse(dtSource_dtl.Rows[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["gross_pay"]               = double.Parse(dtSource_dtl.Rows[0]["gross_pay"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["notes_generic"]           = dtSource_dtl.Rows[0]["notes_generic"];

                        if (ddl_payroll_template.SelectedValue.ToString().Trim() == "025" || ddl_payroll_template.SelectedValue == "044")
                        {
                            row2Edit[0]["other_amount"] = double.Parse(dtSource_dtl.Rows[0]["other_amount"].ToString().Trim()).ToString("###,##0.000");
                        }
                        else
                        {
                            row2Edit[0]["other_amount"]            = double.Parse(dtSource_dtl.Rows[0]["other_amount"].ToString().Trim()).ToString("###,##0.00");
                        }
                        

                        switch (dtSource_dtl.Rows[0]["rate_basis"].ToString())
                        {
                            case "M":
                                {
                                    row2Edit[0]["rate_display"] = txtb_monthly_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Month</small>";
                                    break;
                                }
                            case "D":
                                {
                                    row2Edit[0]["rate_display"] = txtb_daily_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Day</small>";
                                    break;
                                }
                            case "H":
                                {
                                    row2Edit[0]["rate_display"] = txtb_daily_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Hour</small>";
                                    break;
                                }
                            default:
                                {
                                    row2Edit[0]["rate_display"] = "";
                                    break;
                                }
                        }

                        switch (ddl_generic_notes.SelectedValue.ToString().Trim())
                        {
                            case "O":
                                {
                                    row2Edit[0]["notes_generic_descr"] = "<span class='text-success'> <i class='fa fa-thumbs-up text-primary'></i>Outstading</span>";
                                    break;
                                }
                            case "VS":
                                {
                                    row2Edit[0]["notes_generic_descr"] = "<span class='text-success'> <i class='fa fa-thumbs-up text-primary'></i>Very Satisfactory</span>";
                                    break;
                                }
                            case "S":
                                {
                                    row2Edit[0]["notes_generic_descr"] = "<span class='text-success'> <i class='fa fa-thumbs-up text-warning'></i>Satisfactory</span>";
                                    break;
                                }
                            case "US":
                                {
                                    row2Edit[0]["notes_generic_descr"] = "<span class='text-danger'> <i class='fa fa-thumbs-down text-danger'></i>Unsatisfactory</span>";
                                    break;
                                }
                            case "P":
                                {
                                    row2Edit[0]["notes_generic_descr"] = "<span class='text-danger'> <i class='fa fa-thumbs-down'></i>Poor</span>";
                                    break;
                                }
                            default:
                                {
                                    if (ddl_payroll_template.SelectedValue.ToString().Trim() == "043" ||
                                        ddl_payroll_template.SelectedValue.ToString().Trim() == "024" ||
                                        ddl_payroll_template.SelectedValue.ToString().Trim() == "062" || // Honorarium - JO
                                        ddl_payroll_template.SelectedValue.ToString().Trim() == "063") // Communication Expense - JO
                                    {
                                        row2Edit[0]["notes_generic_descr"]  = txtb_memo.Text.ToString().Trim();
                                    }
                                    else
                                    {
                                        row2Edit[0]["notes_generic_descr"]  = ddl_generic_notes.SelectedValue.ToString().Trim();
                                    }
                                    break;
                                }
                        }

                        // BEGIN - Add Field Again  - 06/20/2019  
                        row2Edit[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                        row2Edit[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                        row2Edit[0]["created_dttm"]            = ViewState["created_dttm"].ToString() == "" ? DateTime.Now : ViewState["created_dttm"] ;
                        row2Edit[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        if (Session["ep_post_authority"].ToString() == "1")
                        {
                            row2Edit[0]["posted_by_user"]       = Session["ep_user_id"].ToString();
                            row2Edit[0]["date_posted"]          = txtb_date_posted.Text.ToString().Trim();
                            row2Edit[0]["post_status"]          = "Y";
                            row2Edit[0]["post_status_descr"]    = "POSTED";
                            row2Edit[0]["voucher_nbr"]          = txtb_voucher_nbr.Text.ToString();
                            row2Edit[0]["updated_by_user"]      = ViewState["updated_by_user"].ToString();
                            row2Edit[0]["updated_dttm"]         = ViewState["updated_dttm"].ToString();
                        }

                        // row2Edit[0]["rate_basis_descr"] = lbl_rate_basis_descr.Text.ToString() + " Rate :";

                        row2Edit[0]["other_amount1"] = double.Parse(dtSource_dtl.Rows[0]["other_amount1"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["other_amount2"] = double.Parse(dtSource_dtl.Rows[0]["other_amount2"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["other_amount3"] = double.Parse(dtSource_dtl.Rows[0]["other_amount3"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["other_amount4"] = double.Parse(dtSource_dtl.Rows[0]["other_amount4"].ToString().Trim()).ToString("###,##0.00");
                        row2Edit[0]["other_amount5"] = double.Parse(dtSource_dtl.Rows[0]["other_amount5"].ToString().Trim()).ToString("###,##0.00");

                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }

                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                }

            }
            reshow_gridview_appearance();
        }

        protected void reshow_gridview_appearance()
        {
            //IF LOYALTY SELECTED
            //if (ddl_payroll_template.SelectedValue == "029" || ddl_payroll_template.SelectedValue == "048")
            //{
            //    gv_dataListGrid.HeaderRow.Cells[3].Text                 = "NO. OF YEARS";
            //    gv_dataListGrid.Columns[3].ItemStyle.HorizontalAlign    = HorizontalAlign.Center;
            //    gv_dataListGrid.Columns[3].Visible                      = true;
            //    gv_dataListGrid.Columns[4].Visible                      = false;
            //    gv_dataListGrid.Columns[5].Visible                      = false;
            //}

            ////IF CLOTHING ALLOWANCE SELECTED
            //if (ddl_payroll_template.SelectedValue == "028" || ddl_payroll_template.SelectedValue == "047")
            //{
            //    gv_dataListGrid.HeaderRow.Cells[4].Text                 = "GROSS PAY";
            //    gv_dataListGrid.Columns[3].Visible                      = false;
            //    gv_dataListGrid.Columns[4].Visible                      = true;
            //    gv_dataListGrid.Columns[5].Visible                      = false;
            //}

            ////IF PEI SELECTED
            //if (ddl_payroll_template.SelectedValue == "031" || ddl_payroll_template.SelectedValue == "050")
            //{
            //    gv_dataListGrid.HeaderRow.Cells[3].Text                 = "PEI RATINGS";
            //    gv_dataListGrid.Columns[3].ItemStyle.HorizontalAlign    = HorizontalAlign.Left;
            //    gv_dataListGrid.Columns[3].Visible                      = true;
            //    gv_dataListGrid.Columns[4].Visible                      = false;
            //    gv_dataListGrid.Columns[5].Visible                      = false;
            //}

            ////IF COMMUNICATION EXPENSE ALLOWANCE SELECTED
            //if (ddl_payroll_template.SelectedValue == "024" || ddl_payroll_template.SelectedValue == "043")
            //{
            //    gv_dataListGrid.Columns[2].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[3].ItemStyle.Width = 20;

            //    gv_dataListGrid.HeaderRow.Cells[3].Text                 = "CEA MEMO";
            //    gv_dataListGrid.Columns[3].ItemStyle.HorizontalAlign    = HorizontalAlign.Left;
            //    gv_dataListGrid.Columns[3].ItemStyle.HorizontalAlign    = HorizontalAlign.Left;
            //    gv_dataListGrid.Columns[3].Visible                      = true;
            //    gv_dataListGrid.Columns[4].Visible                      = false;
            //    gv_dataListGrid.Columns[5].Visible                      = false;
            //}

            ////IF YEAR-END AND CASH GIFT SELECTED
            //if (ddl_payroll_template.SelectedValue == "027" || ddl_payroll_template.SelectedValue == "046")
            //{
            //    gv_dataListGrid.Columns[4].ItemStyle.Width              = 15;
            //    gv_dataListGrid.Columns[5].ItemStyle.Width              = 15;
            //    gv_dataListGrid.Columns[6].ItemStyle.Width              = 15;
            //    gv_dataListGrid.Columns[3].Visible                      = false;
            //    gv_dataListGrid.HeaderRow.Cells[4].Text                 = "GROSS PAY"; 
            //    gv_dataListGrid.Columns[4].Visible                      = true;
            //    gv_dataListGrid.HeaderRow.Cells[5].Text                 = "CASH GIFT";
            //    gv_dataListGrid.Columns[5].Visible                      = true;
            //}

            ////IF MID YEAR BONUS
            //if (ddl_payroll_template.SelectedValue == "026" || ddl_payroll_template.SelectedValue == "045")
            //{

            //    gv_dataListGrid.HeaderRow.Cells[3].Text                 = "MID YEAR RATINGS";
            //    gv_dataListGrid.Columns[3].ItemStyle.HorizontalAlign    = HorizontalAlign.Left;
            //    gv_dataListGrid.Columns[3].Visible                      = true;
            //    gv_dataListGrid.Columns[4].Visible                      = false;
            //    gv_dataListGrid.Columns[5].Visible                      = false;
            //}

            ////IF ANNIVERSARY SELECTED
            //if (ddl_payroll_template.SelectedValue == "030" || ddl_payroll_template.SelectedValue == "049")
            //{
            //    gv_dataListGrid.HeaderRow.Cells[4].Text                 = "GROSS PAY";
            //    gv_dataListGrid.Columns[3].Visible                      = false;
            //    gv_dataListGrid.Columns[4].Visible                      = true;
            //    gv_dataListGrid.Columns[5].Visible                      = false;
            //}

            ////IF CNA SELECTED
            //if (ddl_payroll_template.SelectedValue == "032" || ddl_payroll_template.SelectedValue == "051")
            //{
            //    gv_dataListGrid.Columns[4].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[5].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[6].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[3].Visible = false;
            //    gv_dataListGrid.HeaderRow.Cells[4].Text = "GROSS CNA";
            //    gv_dataListGrid.Columns[4].Visible = true;
            //    gv_dataListGrid.HeaderRow.Cells[5].Text = "AGENCY FEE";
            //    gv_dataListGrid.Columns[5].Visible = true;
            //}
            ////IF HONORARIUM
            //if (ddl_payroll_template.SelectedValue == "062" )
            //{
            //    gv_dataListGrid.Columns[4].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[5].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[6].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[3].Visible = false;
            //    gv_dataListGrid.HeaderRow.Cells[4].Text = "GROSS HONORARIUM";
            //    gv_dataListGrid.Columns[4].Visible = true;
            //    gv_dataListGrid.HeaderRow.Cells[5].Text = "AGENCY FEE";
            //    gv_dataListGrid.Columns[5].Visible = false;
            //}
            ////IF MONETIZATION
            //if (ddl_payroll_template.SelectedValue == "025" || ddl_payroll_template.SelectedValue == "044")
            //{
            //    gv_dataListGrid.Columns[4].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[5].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[6].ItemStyle.Width = 15;
            //    gv_dataListGrid.Columns[3].Visible = false;
            //    gv_dataListGrid.HeaderRow.Cells[4].Text = "MONETIZATION AMOUNT";
            //    gv_dataListGrid.Columns[4].Visible = false;
            //    gv_dataListGrid.HeaderRow.Cells[5].Text = "NO OF DAYS";
            //    gv_dataListGrid.Columns[5].Visible = true;
            //    gv_dataListGrid.Columns[6].Visible = true;
            //}

            //up_dataListGrid.Update();
        }


        //**************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            reshow_gridview_appearance();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            reshow_gridview_appearance();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR position_title1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR department_name1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR post_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR net_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("hourly_rate", typeof(System.String));
            dtSource1.Columns.Add("daily_rate", typeof(System.String));
            dtSource1.Columns.Add("other_amount", typeof(System.String));
            dtSource1.Columns.Add("rate_basis", typeof(System.String));
            dtSource1.Columns.Add("rate_display", typeof(System.String));
            dtSource1.Columns.Add("notes_generic_descr", typeof(System.String));
            dtSource1.Columns.Add("notes_generic", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("department_name1", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));

            dtSource1.Columns.Add("other_amount1", typeof(System.String));
            dtSource1.Columns.Add("other_amount2", typeof(System.String));
            dtSource1.Columns.Add("other_amount3", typeof(System.String));
            dtSource1.Columns.Add("other_amount4", typeof(System.String));
            dtSource1.Columns.Add("other_amount5", typeof(System.String));

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

            reshow_gridview_appearance();
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

        //**************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Check if Object already contains value  
        //*************************************************************************
        protected void CheckInputValue(object sender, EventArgs e)
        {
            TextBox TextBox1    = (TextBox)sender;
            string checkValue   = TextBox1.Text;
            string checkName    = TextBox1.ID;

            if (checkValue.ToString() != "")
            {
                FieldValidationColorChanged(false, checkName);
            }
            TextBox1.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            TextBox1.Focus();
        }

        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            //int target_tab = 1;
            FieldValidationColorChanged(false, "ALL");
            
            if (ddl_empl_id.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }
            

            //>>>>>>>>>>> VALIDATIONS FOR RE AND CE LOYALTY BONUS <<<<<<<<<<<//
            if (ddl_payroll_template.SelectedValue == "029" || ddl_payroll_template.SelectedValue == "048")
            {
                if (ddl_generic_notes.SelectedValue == "")
                {
                    FieldValidationColorChanged(true, "ddl_generic_notes");
                    ddl_generic_notes.Focus();
                    validatedSaved = false;
                }

                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                //else
                //{
                //    if ((double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) > double.Parse(hidden_max_amount.Value)) || (double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) <= 0) && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD )
                //    {
                //        FieldValidationColorChanged(true, "greaterthan-loyalty");
                //        txtb_gross_pay.Focus();
                //        validatedSaved = false;
                //    }
                //}
            }
            //>>>>>>>>> END OF VALIDATIONS FOR RE AND CE LOYALTY BONUS <<<<<<<<<<<<//

            //*>>>>>>>>> VALIDATIONS FOR PEI ENTRY <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*//
            if (ddl_payroll_template.SelectedValue == "031" || ddl_payroll_template.SelectedValue == "050")
            {
                if (ddl_generic_notes.SelectedValue == "")
                {
                    FieldValidationColorChanged(true, "ddl_generic_notes");
                    ddl_generic_notes.Focus();
                    validatedSaved = false;
                }

                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if ((double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) > double.Parse(hidden_max_amount.Value)) || (double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) <= 0))
                    {
                        FieldValidationColorChanged(true, "greaterthan-loyalty");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
            }
            /*>>>>>>>>>>>>>>>>>> END OF  PEI <<<<<<<<<<<<<<<<<<<<<<<*/

            /*>>>>>>>>> VALIDATIONS FOR CLOTHING ENTRY <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
            if (ddl_payroll_template.SelectedValue == "028" || ddl_payroll_template.SelectedValue == "047")
            {
                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if ((double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) > double.Parse(hidden_max_amount.Value)) || (double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) <= 0))
                    {
                        FieldValidationColorChanged(true, "greaterthan-loyalty");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
            }
            /*>>>>>>>>>>>>>>>>>> END OF CLOTHING <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/

            /*>>>>>>>>> VALIDATIONS FOR COMMUNICATION EXPENSE ALLOWANCES ENTRY <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
            if (ddl_payroll_template.SelectedValue == "043" || 
                ddl_payroll_template.SelectedValue == "024" ||
                ddl_payroll_template.SelectedValue == "063") // Communication Expense - JO
            {
                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if ((double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) <= 0))
                    {
                        FieldValidationColorChanged(true, "most-not-zero");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
            }
            /*>>>>>>>>>>>>>>> END OF COMMUNICATION EXPENSE ALLOWANCE <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/

            /*>>>>>>>>> VALIDATIONS FOR YEAR-END AND CASH GIFT <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
            if (ddl_payroll_template.SelectedValue == "046" || ddl_payroll_template.SelectedValue == "027")
            {
                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if ((double.Parse(txtb_gross_pay.Text.ToString().Trim().Replace(",","")) < 0))
                    {
                        FieldValidationColorChanged(true, "less-zero");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else if (double.Parse(txtb_gross_pay.Text.ToString().Trim().Replace(",", "")) > double.Parse(txtb_monthly_rate.Text.ToString().Replace(",","").Trim()))
                    {
                        FieldValidationColorChanged(true, "greater-than-monthly");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }

                if (CommonCode.checkisdecimal(txtb_other_amount) == false)
                {
                    if (txtb_other_amount.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_other_amount");
                        txtb_other_amount.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-otheramount");
                        txtb_other_amount.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if ((double.Parse(txtb_other_amount.Text.ToString().Trim().Replace(",", "")) < 0))
                    {
                        FieldValidationColorChanged(true, "less-zero-other");
                        txtb_other_amount.Focus();
                        validatedSaved = false;
                    }
                    else if (double.Parse(txtb_other_amount.Text.ToString().Trim().Replace(",", "")) > double.Parse(hidden_max_amount.Value.ToString().Replace(",", "").Trim()))
                    {
                        FieldValidationColorChanged(true, "greater-than-cashgift");
                        txtb_other_amount.Focus();
                        validatedSaved = false;
                    }
                }
            }
            /*>>>>>>>>>>>>>>> END OF YECG <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/


            /*>>>>>>>>> VALIDATIONS FOR MID YEAR BONuS ENTRY <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
            if (ddl_payroll_template.SelectedValue == "026" || ddl_payroll_template.SelectedValue == "045")
            {
                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if (double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) < 0)
                    {
                        FieldValidationColorChanged(true, "less-zero");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else if (double.Parse(txtb_gross_pay.Text.ToString().Trim().Replace(",", "")) > double.Parse(txtb_monthly_rate.Text.ToString().Replace(",", "").Trim()))
                    {
                        FieldValidationColorChanged(true, "greater-than-monthly");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
            }
            /*>>>>>>>>>>>>>>>>>> END OF MID YEAR BONUS <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/

            /*>>>>>>>>> VALIDATIONS FOR ANNIVERSARY BONUS ENTRY <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
            if (ddl_payroll_template.SelectedValue == "030" || ddl_payroll_template.SelectedValue == "049")
            {
                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if ((double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) > double.Parse(hidden_max_amount.Value)) || (double.Parse(txtb_gross_pay.Text.ToString().Replace(",", "").Trim()) <= 0))
                    {
                        FieldValidationColorChanged(true, "greaterthan-loyalty");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
            }
            /*>>>>>>>>>>>>>>>>>> END OF ANNIVERSARY BONUS <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/

            /*>>>>>>>>> VALIDATIONS FOR CNA <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
            if (ddl_payroll_template.SelectedValue == "032" || ddl_payroll_template.SelectedValue == "051")
            {
                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if ((double.Parse(txtb_gross_pay.Text.ToString().Trim().Replace(",", "")) < 0))
                    {
                        FieldValidationColorChanged(true, "less-zero");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    //else if (double.Parse(txtb_gross_pay.Text.ToString().Trim().Replace(",", "")) > double.Parse(hidden_max_amount.Value.ToString().Replace(",", "").Trim()))
                    //{
                    //    FieldValidationColorChanged(true, "greater-than-cna-gross");
                    //    txtb_gross_pay.Focus();
                    //    validatedSaved = false;
                    //}
                }

                if (CommonCode.checkisdecimal(txtb_other_amount) == false)
                {
                    if (txtb_other_amount.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_other_amount");
                        txtb_other_amount.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-otheramount");
                        txtb_other_amount.Focus();
                        validatedSaved = false;
                    }
                }
                else
                {
                    if ((double.Parse(txtb_other_amount.Text.ToString().Trim().Replace(",", "")) < 0))
                    {
                        FieldValidationColorChanged(true, "less-zero-other");
                        txtb_other_amount.Focus();
                        validatedSaved = false;
                    }
                    //else if (double.Parse(txtb_other_amount.Text.ToString().Trim().Replace(",", "")) > double.Parse(hidden_max_amount_2.Value.ToString().Replace(",", "").Trim()))
                    //{
                    //    FieldValidationColorChanged(true, "greater-than-agency");
                    //    txtb_other_amount.Focus();
                    //    validatedSaved = false;
                    //}
                }
                if (CommonCode.checkisdecimal(txtb_other_amount1) == false)
                {
                    if (txtb_other_amount1.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_other_amount1");
                        txtb_other_amount1.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-otheramount1");
                        txtb_other_amount1.Focus();
                        validatedSaved = false;
                    }
                }

            }
            /*>>>>>>>>> VALIDATIONS FOR HONORARIUM <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/
            if (ddl_payroll_template.SelectedValue == "062" )
            {
                if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
                {
                    if (txtb_gross_pay.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_gross_pay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-grosspay");
                        txtb_gross_pay.Focus();
                        validatedSaved = false;
                    }
                }
                if (CommonCode.checkisdecimal(txtb_other_amount1) == false)
                {
                    if (txtb_other_amount1.Text.ToString().Trim() == "")
                    {
                        FieldValidationColorChanged(true, "txtb_other_amount1");
                        txtb_other_amount1.Focus();
                        validatedSaved = false;
                    }
                    else
                    {
                        FieldValidationColorChanged(true, "invalid-otheramount1");
                        txtb_other_amount1.Focus();
                        validatedSaved = false;
                    }
                }

            }


            if (txtb_memo.Text.Length > 50)
            {
                FieldValidationColorChanged(true, "txtb_memo-50-max-lenght");
                txtb_memo.Focus();
                validatedSaved = false;
            }

            // Required ang Vouvher number if Status is Y or Blank ug ang Post Authority is equal to 1 
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }
            /*>>>>>>>>>>>>>>> END OF YECG <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*/

            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
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
                    case "invalid-monthlyrate":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_monthly_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_daily_rate":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_daily_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-dailyrate":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_daily_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hourly_rate":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            txtb_hourly_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-hourlyrate":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hourly_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_generic_notes":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            ddl_generic_notes.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gross_pay":
                        {
                            LblRequired7.Text = MyCmn.CONST_RQDFLD;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "greaterthan-loyalty":
                        {
                            LblRequired7.Text = "Should not be greater than "+hidden_max_amount.Value+" nor less than 0";
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "greater-than-monthly":
                        {
                            LblRequired7.Text = "Should not be greater than " + txtb_monthly_rate.Text.Trim();
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "greater-than-cna-gross":
                        {
                            LblRequired7.Text = "Should not be greater than " + hidden_max_amount.Value;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-grosspay":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_pay":
                        {
                            LblRequired8.Text = MyCmn.CONST_RQDFLD;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-netpay":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_other_amount":
                        {
                            LblRequired9.Text = MyCmn.CONST_RQDFLD;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-otheramount":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "less-zero-other":
                        {
                            LblRequired9.Text = "Should not be negative";
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "greater-than-cashgift":
                        {
                            LblRequired9.Text = "Should not be greater than " + hidden_max_amount.Value + " nor less than 0";
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "greater-than-agency":
                        {
                            LblRequired9.Text = "Should not be greater than " + hidden_max_amount_2.Value + " nor less than 0";
                            txtb_other_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "most-not-zero":
                        {
                            LblRequired7.Text = "Should be greater that 0";
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "less-zero":
                        {
                            LblRequired7.Text = "Should not be negative";
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_memo-50-max-lenght":
                        {
                            LblRequired6.Text =  " number of character exceed 50 above!" + " | char. count ('" + txtb_memo.Text.Length + "')";
                            txtb_memo.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_voucher_nbr":
                        {
                            LblRequired200.Text = MyCmn.CONST_RQDFLD;
                            txtb_voucher_nbr.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_reason":
                        {
                            LblRequired201.Text = MyCmn.CONST_RQDFLD;
                            txtb_reason.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_other_amount1":
                        {
                            LblRequired90.Text = MyCmn.CONST_RQDFLD;
                            txtb_other_amount1.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-otheramount1":
                        {
                            LblRequired90.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount1.BorderColor = Color.Red;
                            break;
                        }

                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired1.Text       = "";
                            LblRequired2.Text       = "";
                            LblRequired3.Text       = "";
                            LblRequired4.Text       = "";
                            LblRequired5.Text       = "";
                            LblRequired6.Text       = "";
                            LblRequired7.Text       = "";
                            LblRequired8.Text       = "";
                            LblRequired9.Text       = "";
                            LblRequired200.Text       = "";
                            LblRequired201.Text       = "";
                            LblRequired90.Text = "";
                            LblRequired91.Text = "";
                            LblRequired92.Text = "";
                            LblRequired93.Text = "";
                            LblRequired94.Text = "";

                            ddl_empl_id.BorderColor         = Color.LightGray;
                            ddl_generic_notes.BorderColor   = Color.LightGray;
                            txtb_gross_pay.BorderColor      = Color.LightGray;
                            txtb_net_pay.BorderColor        = Color.LightGray;
                            txtb_other_amount.BorderColor   = Color.LightGray;
                            txtb_daily_rate.BorderColor     = Color.LightGray;
                            txtb_hourly_rate.BorderColor    = Color.LightGray;
                            txtb_monthly_rate.BorderColor   = Color.LightGray;
                            txtb_memo.BorderColor           = Color.LightGray;
                            txtb_voucher_nbr.BorderColor    = Color.LightGray;
                            txtb_reason.BorderColor         = Color.LightGray;
                            txtb_other_amount1.BorderColor  = Color.LightGray;
                            txtb_other_amount2.BorderColor  = Color.LightGray;
                            txtb_other_amount3.BorderColor  = Color.LightGray;
                            txtb_other_amount4.BorderColor  = Color.LightGray;
                            txtb_other_amount5.BorderColor  = Color.LightGray;

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
                DataRow[] selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

                DataTable chk_hdr = new DataTable();
                string query_hdr = "SELECT * FROM payrollregistry_hdr_tbl WHERE payroll_year = '"+ddl_year.SelectedValue.ToString().Trim()+"' AND payroll_registry_nbr = '"+ lbl_registry_number.Text.ToString().Trim() + "'";
                chk_hdr = MyCmn.GetDatatable(query_hdr);

                if (chk_hdr.Rows.Count > 0)
                {
                    string dt_from = chk_hdr.Rows[0]["payroll_period_from"].ToString();
                    string dt_to   = chk_hdr.Rows[0]["payroll_period_to"].ToString();

                    DataTable chk = new DataTable();
                    string query = "SELECT * FROM dbo.payrollemployeegroupings_dtl_excludes_tbl X WHERE X.empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "' AND   X.emp_status	= 0 AND   (CONVERT(date,'" + dt_from + "') BETWEEN CONVERT(date,X.exclude_date_from) AND CONVERT(date,X.exclude_date_to) OR CONVERT(date,'" + dt_to + "') BETWEEN CONVERT(date,X.exclude_date_from) AND CONVERT(date,X.exclude_date_to)) AND X.id	= (SELECT MAX(X1.id) FROM dbo.payrollemployeegroupings_dtl_excludes_tbl X1 WHERE X1.empl_id = X.empl_id)";
                    chk = MyCmn.GetDatatable(query);

                    if (chk.Rows.Count > 0)
                    {
                        msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                        msg_header.InnerText = "YOU EXCLUDE THIS EMPLOYEE (" + chk.Rows[0]["employee_name"].ToString().Trim() + ")";
                        var lbl_descr = "Period covered: <br>" + DateTime.Parse(chk.Rows[0]["exclude_date_from"].ToString().Trim()).ToLongDateString() + " - " + DateTime.Parse(chk.Rows[0]["exclude_date_to"].ToString().Trim()).ToLongDateString() + "<br><br> Reason: <br>" + chk.Rows[0]["exclude_reason"].ToString().Trim();
                        lbl_details.Text = lbl_descr;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop6", "openNotification();", true);
                    }
                }

                hidden_max_amount.Value = "0";
                txtb_empl_id.Text = ddl_empl_id.SelectedValue.ToString().Trim();
                //LOYALTY SETUP
                set_max_amount();
                if (ddl_payroll_template.SelectedValue == "029" || ddl_payroll_template.SelectedValue == "048")
                {
                    txtb_gross_pay.Text                 = selected_employee[0]["loyaltybonus_amount"].ToString();
                    txtb_net_pay.Text                   = selected_employee[0]["loyaltybonus_amount"].ToString();
                    ddl_generic_notes.SelectedValue     = selected_employee[0]["years_of_tenure"].ToString();
                    
                }

                //PEI SETUP
                if (ddl_payroll_template.SelectedValue == "031" || ddl_payroll_template.SelectedValue == "050")
                {
                    txtb_gross_pay.Text                 = selected_employee[0]["productivity_incentives"].ToString();
                    txtb_net_pay.Text                   = selected_employee[0]["productivity_incentives"].ToString();
                    
                }

                //CLOTHING SETUP
                if (ddl_payroll_template.SelectedValue == "028" || ddl_payroll_template.SelectedValue == "047")
                {
                    txtb_gross_pay.Text                 = selected_employee[0]["clothing_allowance"].ToString();
                    txtb_net_pay.Text                   = selected_employee[0]["clothing_allowance"].ToString();
                }

                //COMMUNICATION EXPENSE ALLOWANCE SETUP
                if (ddl_payroll_template.SelectedValue == "043" ||
                    ddl_payroll_template.SelectedValue == "024" ||
                    ddl_payroll_template.SelectedValue == "063") // Communication Expense - JO
                {
                    txtb_memo.Text                      = selected_employee[0]["memo_descr"].ToString();
                    txtb_gross_pay.Text                 = selected_employee[0]["comm_amount"].ToString();
                }

                //YEAR END AND CASH GIFT SETUP
                if (ddl_payroll_template.SelectedValue == "046" || ddl_payroll_template.SelectedValue == "027")
                {
                    txtb_other_amount.Text             = selected_employee[0]["cash_gift"].ToString();
                }

                //ANNIVERSARY BONUS SETUP
                if (ddl_payroll_template.SelectedValue == "030" || ddl_payroll_template.SelectedValue == "049")
                {
                    txtb_gross_pay.Text                = selected_employee[0]["anniversary_bonus"].ToString();
                }

                //CNA INCENTIVES SETUP
                if (ddl_payroll_template.SelectedValue == "032" || ddl_payroll_template.SelectedValue == "051")
                {
                    txtb_gross_pay.Text = selected_employee[0]["incentive_amount"].ToString();
                    txtb_other_amount.Text = selected_employee[0]["agency_fee"].ToString();
                }

                //HONORARIUM - JO
                if (ddl_payroll_template.SelectedValue == "062" )
                {
                    txtb_gross_pay.Text     = selected_employee[0]["honoraria_jo_bonus"].ToString();
                }

                lbl_rate_basis_descr.Text   = selected_employee[0]["rate_basis_descr"].ToString() + " Rate :";
                txtb_monthly_rate.Text      = selected_employee[0]["monthly_rate"].ToString();
                txtb_hourly_rate.Text       = selected_employee[0]["hourly_rate"].ToString();
                txtb_daily_rate.Text        = selected_employee[0]["daily_rate"].ToString();
                hidden_rate_basis.Value     = selected_employee[0]["rate_basis"].ToString();
                
                switch (selected_employee[0]["rate_basis"].ToString())
                {
                    case "M":
                        {
                            div_monthly.Visible = false;
                            div_daily.Visible = false;
                            div_hourly.Visible = false;
                            div_monthly.Visible = true;
                            break;
                        }
                    case "D":
                        {
                            div_monthly.Visible = false;
                            div_daily.Visible = false;
                            div_hourly.Visible = false;
                            div_daily.Visible = true;
                            break;
                        }
                    case "H":
                        {
                            div_monthly.Visible = false;
                            div_daily.Visible = false;
                            div_hourly.Visible = false;
                            div_hourly.Visible = true;
                            break;
                        }
                    default:
                        {
                            div_monthly.Visible = false;
                            div_daily.Visible   = false;
                            div_hourly.Visible  = false;
                            break;
                        }
                }

                // To All Tempalte Type Must Have This Field
                txtb_position.Text          = selected_employee[0]["position_title1"].ToString();
                txtb_department_descr.Text  = selected_employee[0]["department_name1"].ToString();
                txtb_empl_id.Text           = selected_employee[0]["empl_id"].ToString();
                
                calculate_net_pay();
            }
            else
            {
                lbl_rate_basis_descr.Text   = "Rate Basis:";
                //txtb_monthly_rate.Text      = "0.000";
                //txtb_hourly_rate.Text       = "0.000";
                //txtb_daily_rate.Text        = "0.000";
                //txtb_gross_pay.Text         = "0.000";
                //txtb_net_pay.Text           = "0.000";
                //txtb_other_amount.Text      = "0.000";
                //txtb_memo.Text              = "";
                ClearEntry();
                
            }

        }

        private void set_max_amount()
        {
            if (txtb_empl_id.Text.ToString().Trim() != "")
            {
                DataRow[] selected_employee = dataList_employee.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");

                hidden_max_amount.Value = "0";

                //LOYALTY SETUP
                if (ddl_payroll_template.SelectedValue == "029" || ddl_payroll_template.SelectedValue == "048")
                {
                    hidden_max_amount.Value = selected_employee[0]["loyaltybonus_amount"].ToString();
                }

                //PEI SETUP
                if (ddl_payroll_template.SelectedValue == "031" || ddl_payroll_template.SelectedValue == "050")
                {
                    hidden_max_amount.Value = selected_employee[0]["productivity_incentives"].ToString();
                }

                //CLOTHING SETUP
                if (ddl_payroll_template.SelectedValue == "028" || ddl_payroll_template.SelectedValue == "047")
                {
                    hidden_max_amount.Value = selected_employee[0]["clothing_allowance"].ToString();
                }

                //YEAR-END AND CASH GIFT SET UP
                if (ddl_payroll_template.SelectedValue == "046" || ddl_payroll_template.SelectedValue == "027")
                {
                    hidden_max_amount.Value = selected_employee[0]["cash_gift"].ToString();
                }

                //ANNIVERSARY BONUS SET UP
                if (ddl_payroll_template.SelectedValue == "030" || ddl_payroll_template.SelectedValue == "049")
                {
                    hidden_max_amount.Value = selected_employee[0]["anniversary_bonus"].ToString();
                }

                //YEAR-END AND CASH GIFT SET UP
                if (ddl_payroll_template.SelectedValue == "032" || ddl_payroll_template.SelectedValue == "051")
                {
                    hidden_max_amount.Value = selected_employee[0]["incentive_amount"].ToString();
                    hidden_max_amount_2.Value = selected_employee[0]["agency_fee"].ToString();
                }

                //HONORARIUM - JO
                if (ddl_payroll_template.SelectedValue == "062")
                {
                    hidden_max_amount.Value = selected_employee[0]["honoraria_jo_bonus"].ToString();
                }
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Calculate Net Pay
        //**************************************************************************
        protected void calculate_net_pay()
        {
            double net_pay = 0;
            switch (ddl_payroll_template.SelectedValue)
            {
                //YEAR-END AND CASH GIFT
                case "046":
                case "027":

                    string cash_gift = txtb_gross_pay.Text.ToString().Trim() != "" ? txtb_gross_pay.Text.ToString().Trim() : "0";
                    string year_end_bunos = txtb_other_amount.Text.ToString().Trim() != "" ? txtb_other_amount.Text.ToString().Trim() : "0";
                    net_pay = double.Parse(cash_gift) + double.Parse(year_end_bunos);
                    break;
                //CNA INCENTIVES
                case "032":
                case "051":
                    string gross_cna    = txtb_gross_pay.Text.ToString().Trim() != "" ? txtb_gross_pay.Text.ToString().Trim() : "0";
                    string agency_fee   = txtb_other_amount.Text.ToString().Trim() != "" ? txtb_other_amount.Text.ToString().Trim() : "0";
                    string mortuary_fee_cna = txtb_other_amount1.Text.ToString().Trim() != "" ? txtb_other_amount1.Text.ToString().Trim() : "0";
                    net_pay = double.Parse(gross_cna) - (double.Parse(agency_fee) + double.Parse(mortuary_fee_cna));
                    break;
                //MONETIZATION | Monthly Rate (X) Number of Days (X) Monetization Constant Factor (ref. Installation Table) 
                case "025":
                case "044":
                    net_pay = double.Parse(txtb_monthly_rate.Text) * double.Parse(txtb_other_amount.Text);
                    net_pay = net_pay * double.Parse(hidden_mone_constant_factor.Value);
                    txtb_gross_pay.Text = net_pay.ToString("###,##0.00");
                    break;

                // New Additional - 
                case "062":
                    string gross_hon = txtb_gross_pay.Text.ToString().Trim() != "" ? txtb_gross_pay.Text.ToString().Trim() : "0";
                    string mortuary_fee_hon = txtb_other_amount1.Text.ToString().Trim() != "" ? txtb_other_amount1.Text.ToString().Trim() : "0";
                    net_pay = double.Parse(gross_hon) -  double.Parse(mortuary_fee_hon);
                    break;
            }
            txtb_net_pay.Text = net_pay.ToString("###,##0.00");
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            txtb_daily_rate.Enabled       = ifenable;
            txtb_monthly_rate.Enabled     = ifenable;
            txtb_hourly_rate.Enabled      = ifenable;
            txtb_net_pay.Enabled          = ifenable;
            txtb_gross_pay.Enabled        = ifenable;
            txtb_other_amount.Enabled     = ifenable;
            txtb_memo.Enabled             = ifenable;
            ddl_generic_notes.Enabled     = ifenable;
        }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}