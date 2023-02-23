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

namespace HRIS_ePayroll.View
{
    public partial class cPayRegistrySalaryRegular : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 04/06/2019 - Data Place holder creation 
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
        //  BEGIN - VJA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 04/06/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "payroll_registry_nbr";
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

                ViewState["page_allow_add"]          = Session["page_allow_add_from_registry"];
                ViewState["page_allow_delete"]       = Session["page_allow_delete_from_registry"];
                ViewState["page_allow_edit"]         = Session["page_allow_edit_from_registry"];
                ViewState["page_allow_edit_history"] = Session["page_allow_edit_history_from_registry"];
                ViewState["page_allow_print"]        = Session["page_allow_print_from_registry"];


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
                    ViewState["page_allow_print"]   = 1;
                }

                if (Session["PreviousValuesonPage_cPayRegistry"] == null)
                    Session["PreviousValuesonPage_cPayRegistry"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    //RetrieveEmploymentType();
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_month.SelectedValue = prevValues[1].ToString();

                    ddl_empl_type.SelectedValue = prevValues[2].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue = prevValues[3].ToString();
                    RetriveGroupings();
                    ddl_payroll_group.SelectedValue = prevValues[7].ToString();
                    lbl_registry_no.Text = prevValues[7].ToString();
                    RetrieveDataListGrid();
                    RetrieveEmployeename();
                    RetrieveLoanPremiums_Visible();
                    RetrieveReserveDeduction();
                    RetrievePayrollInstallation();
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
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_empl_type.SelectedValue = prevValues[1].ToString();
                    RetrieveDataListGrid();
                }

                if (Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"] == null)
                    Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_empl_type.SelectedValue = prevValues[1].ToString();
                    RetrieveDataListGrid();
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
            RetrievePayrollInstallation();
        }

        //********************************************************************
        //  BEGIN - VJA- 04/06/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            RetrieveYear();
            RetriveTemplate();
            RetriveGroupings();
            RetrieveDataListGrid();

            //Retrieve When Add
            RetrieveBindingDep();
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveEmployeename();
            RetrieveBindingFundcharges();

            btnAdd.Visible = true;
            id_days_hours.Visible = false;

            //txtb_date_posted.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Retrieve From Installation Table 
        //*************************************************************************
        private void RetrievePayrollInstallation()
        {
            //  This is to Get Last Day of The Month
            DateTime last_Date = new DateTime(Convert.ToInt32(ddl_year.SelectedValue.ToString()), Convert.ToInt32(ddl_month.SelectedValue.ToString()), 1).AddMonths(1).AddDays(-1);
            int lastday = Convert.ToInt32(last_Date.Day.ToString());
            lbl_lastday_hidden.Text = lastday.ToString();

            DataTable dt = MyCmn.RetrieveData("sp_payrollinstallation_tbl_list", "par_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_include_history", "N");
            if (dt.Rows.Count > 0)
            {
                lbl_installation_monthly_conv_hidden.Text = dt.Rows[0]["monthly_salary_days_conv"].ToString();
                lbl_minimum_netpay_hidden.Text = dt.Rows[0]["minimum_net_pay"].ToString();
                lbl_hours_in_1day.Text = dt.Rows[0]["hours_in_1day_conv"].ToString();
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveEmployeename()
        {
            ddl_empl_id.Items.Clear();
            dtSource_for_names = MyCmn.RetrieveData("sp_personnelnames_combolist_preg_re_ce_payroll", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR());

            ddl_empl_id.DataSource = dtSource_for_names;

            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Department 
        //*************************************************************************
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
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Sub-Department
        //*************************************************************************
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
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Division
        //*************************************************************************
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
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Section
        //*************************************************************************
        private void RetrieveBindingSection()
        {
            ddl_section.Items.Clear();
            DataTable dt1 = MyCmn.RetrieveData("sp_sections_tbl_combolist", "par_department_code", ddl_dep.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString(), "par_division_code", ddl_division.SelectedValue.ToString().Trim());

            ddl_section.DataSource = dt1;
            ddl_section.DataValueField = "section_code";
            ddl_section.DataTextField = "section_name1";
            ddl_section.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_section.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Fund Charges
        //*************************************************************************
        private void RetrieveBindingFundcharges()
        {
            ddl_fund_charges.Items.Clear();
            DataTable dt1 = MyCmn.RetrieveData("sp_fundcharges_tbl_list");

            ddl_fund_charges.DataSource = dt1;
            ddl_fund_charges.DataValueField = "fund_code";
            ddl_fund_charges.DataTextField = "fund_description";
            ddl_fund_charges.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_fund_charges.Items.Insert(0, li);
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
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
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
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveTemplate()
        {
            ddl_payroll_template.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            GetRegistry_NBR();
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_dtl_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", lbl_registry_no.Text.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR(),"par_employment_type",ddl_empl_type.SelectedValue.ToString());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();

            // For Detials Initialized, Add Primary Keys, Add new Row
            GetRegistry_NBR();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            RetrieveEmployeename();

            lbl_if_dateposted_yes.Text = "";
            btnSave.Visible = true;

            ddl_empl_id.Enabled = true;

            txtb_employeename.Visible   = false;
            ddl_empl_id.Visible         = true;
            txtb_voucher_nbr.Enabled    = false;

            btn_calculate.Visible       = true;
            btn_contributions.Visible   = true;
            btn_editloan.Visible        = true;

            LabelAddEdit.Text = "Addd Record | Registry No: " + lbl_registry_no.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
            lbl_addeditmode_hidden.Text = MyCmn.CONST_ADD;
            FieldValidationColorChanged(false, "ALL");
            ToogleTextbox(true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Get The Registry Number
        //*************************************************************************
        private string GetRegistry_NBR()
        {
            string group_nbr = "";
            if (ddl_payroll_group.SelectedValue.ToString().Trim() != "")
            {
                lbl_registry_no.Text = ddl_payroll_group.SelectedValue.ToString();
                if (dtGroup != null && dtGroup.Rows.Count > 0)
                {
                    DataRow[] registry_nbr = dtGroup.Select("payroll_registry_nbr='" + ddl_payroll_group.SelectedValue.ToString() + "'");
                    group_nbr = registry_nbr[0]["payroll_group_nbr"].ToString().Trim();
                }
            }
            return group_nbr;
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Clear Add/Edit Page Fields                  |
        //*************************************************************************
        private void ClearEntry()
        {
            //Header 
            //lbl_registry_no.Text = "";
            // ddl_empl_id.SelectedValue = "";
            txtb_empl_id.Text = "";
            ddl_dep.SelectedIndex = -1;
            ddl_subdep.SelectedIndex = -1;
            ddl_division.SelectedIndex = -1;
            ddl_section.SelectedIndex = -1;
            txtb_remarks.Text = "";

            txtb_rate_amount.Text = "0.00";
            txtb_no_days_lwopay.Text = "0.00";
            txtb_no_days_worked.Text = "0.00";
            txtb_lates_in_min.Text = "0";
            txtb_no_hours_worked.Text = "0.00";
            txtb_pera_amount.Text = "0.00";

            //Summary Tab                 
            txtb_gross_pay.Text = "0.00";
            txtb_lwo_pay.Text = "0.00";
            txtb_total_mandatory.Text = "0.00";
            txtb_total_optional.Text = "0.00";
            txtb_total_loans.Text = "0.00";
            txtb_net_pay.Text = "0.00";
            txtb_net_pay_1h.Text = "0.00";
            txtb_net_pay_2h.Text = "0.00";
            txtb_lwop_amount_pera.Text = "0.00";
            txtb_late_in_amount.Text = "0.00";

            //Mandatory Deductions Tab
            txtb_gsis_gs.Text               = "0.00";
            txtb_gsis_ps.Text               = "0.00";
            txtb_gsis_sif.Text              = "0.00";
            txtb_hdmf_gs.Text               = "0.00";
            txtb_hdmf_ps.Text               = "0.00";
            txtb_phic_gs.Text               = "0.00";
            txtb_phic_ps.Text               = "0.00";
            txtb_bir_tax.Text               = "0.00";

            //Optional Deduction Tab
            txtb_gsis_ouli.Text             = "0.00";
            txtb_gsis_ouli45.Text           = "0.00";
            txtb_gsis_ouli50.Text           = "0.00";
            txtb_gsis_ouli55.Text           = "0.00";
            txtb_gsis_ouli60.Text           = "0.00";
            txtb_gsis_ouli65.Text           = "0.00";
            txtb_sss.Text                   = "0.00";
            txtb_hdmf_addl.Text             = "0.00";
            txtb_philam.Text                = "0.00";
            txtb_gsis_ehp.Text              = "0.00";
            txtb_gsis_hip.Text              = "0.00";
            txtb_gsis_ceap.Text             = "0.00";
            txtb_gsis_add.Text              = "0.00";
            txtb_otherpremium_no1.Text      = "0.00";
            txtb_otherpremium_no2.Text      = "0.00";
            txtb_otherpremium_no3.Text      = "0.00";
            txtb_otherpremium_no4.Text      = "0.00";
            txtb_otherpremium_no5.Text      = "0.00";
            txtb_hdmf_mp2.Text              = "0.00";
            txtb_hdmf_loyalty_card.Text     = "0.00";

            //Loans Tab
            txtb_gsis_consolidated.Text     = "0.00";
            txtb_gsis_policy_regular.Text   = "0.00";
            txtb_gsis_policy_optional.Text  = "0.00";
            txtb_gsis_ouli_loan.Text        = "0.00";
            txtb_gsis_emer_loan.Text        = "0.00";
            txtb_gsis_ecard_loan.Text       = "0.00";
            txtb_gsis_educ_loan.Text        = "0.00";
            txtb_gsis_real_loan.Text        = "0.00";
            txtb_gsis_sos_loan.Text         = "0.00";
            txtb_hdmf_mpl_loan.Text         = "0.00";
            txtb_hdmf_house_loan.Text       = "0.00";
            txtb_hdmf_cal_loan.Text         = "0.00";
            txtb_ccmpc_loan.Text            = "0.00";
            txtb_nico_loan.Text             = "0.00";
            txtb_networkbank_loan.Text      = "0.00";
            txtb_otherloan_no1.Text         = "0.00";
            txtb_otherloan_no2.Text         = "0.00";
            txtb_otherloan_no3.Text         = "0.00";
            txtb_otherloan_no4.Text         = "0.00";
            txtb_otherloan_no5.Text         = "0.00";

            // Add Field 03/12/2019
            txtb_nhmfc_hsng.Text            = "0.00";
            txtb_nafc.Text                  = "0.00";

            // Add Field Again 03/14/2019
            txtb_gsis_help.Text             = "0.00";
            txtb_gsis_housing_loan.Text     = "0.00";

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
            lbl_post_status.Text      = "";

            // Add Field Again 2022-05-30
            txtb_other_ded_mand1.Text   =  "0.00";
            txtb_other_ded_mand2.Text   =  "0.00";
            txtb_other_ded_mand3.Text   =  "0.00";
            txtb_other_ded_mand4.Text   =  "0.00";
            txtb_other_ded_mand5.Text   =  "0.00";
            txtb_other_ded_mand6.Text   =  "0.00";
            txtb_other_ded_mand7.Text   =  "0.00";
            txtb_other_ded_mand8.Text   =  "0.00";
            txtb_other_ded_mand9.Text   =  "0.00";
            txtb_other_ded_mand10.Text  =  "0.00";
            txtb_other_ded_prem1.Text   =  "0.00";
            txtb_other_ded_prem2.Text   =  "0.00";
            txtb_other_ded_prem3.Text   =  "0.00";
            txtb_other_ded_prem4.Text   =  "0.00";
            txtb_other_ded_prem5.Text   =  "0.00";
            txtb_other_ded_prem6.Text   =  "0.00";
            txtb_other_ded_prem7.Text   =  "0.00";
            txtb_other_ded_prem8.Text   =  "0.00";
            txtb_other_ded_prem9.Text   =  "0.00";
            txtb_other_ded_prem10.Text  =  "0.00";
            txtb_other_ded_loan1.Text   =  "0.00";
            txtb_other_ded_loan2.Text   =  "0.00";
            txtb_other_ded_loan3.Text   =  "0.00";
            txtb_other_ded_loan4.Text   =  "0.00";
            txtb_other_ded_loan5.Text   =  "0.00";
            txtb_other_ded_loan6.Text   =  "0.00";
            txtb_other_ded_loan7.Text   =  "0.00";
            txtb_other_ded_loan8.Text   =  "0.00";
            txtb_other_ded_loan9.Text   =  "0.00";
            txtb_other_ded_loan10.Text  =  "0.00";

            txtb_late_in_amount_pera.Text      = "0.00";
            FieldValidationColorChanged(false, "ALL");
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("rate_basis", typeof(System.String));
            dtSource.Columns.Add("monthly_rate", typeof(System.String));
            dtSource.Columns.Add("daily_rate", typeof(System.String));
            dtSource.Columns.Add("pera_amt", typeof(System.String));
            dtSource.Columns.Add("net_pay", typeof(System.String));
            dtSource.Columns.Add("net_pay1", typeof(System.String));
            dtSource.Columns.Add("net_pay2", typeof(System.String));
            dtSource.Columns.Add("lowpay_day", typeof(System.String));
            dtSource.Columns.Add("lowp_amount", typeof(System.String));
            dtSource.Columns.Add("gsis_gs", typeof(System.String));
            dtSource.Columns.Add("gsis_ps", typeof(System.String));
            dtSource.Columns.Add("sif_gs", typeof(System.String));
            dtSource.Columns.Add("hdmf_gs", typeof(System.String));
            dtSource.Columns.Add("hdmf_ps", typeof(System.String));
            dtSource.Columns.Add("phic_gs", typeof(System.String));
            dtSource.Columns.Add("phic_ps", typeof(System.String));
            dtSource.Columns.Add("wtax", typeof(System.String));
            dtSource.Columns.Add("gsis_uoli", typeof(System.String));
            dtSource.Columns.Add("gsis_uoli45", typeof(System.String));
            dtSource.Columns.Add("gsis_uoli50", typeof(System.String));
            dtSource.Columns.Add("gsis_uoli55", typeof(System.String));
            dtSource.Columns.Add("gsis_uoli60", typeof(System.String));
            dtSource.Columns.Add("gsis_uoli65", typeof(System.String));
            dtSource.Columns.Add("sss_ps", typeof(System.String));
            dtSource.Columns.Add("hdmf_ps2", typeof(System.String));
            dtSource.Columns.Add("philamlife_ps", typeof(System.String));
            dtSource.Columns.Add("gsis_ehp", typeof(System.String));
            dtSource.Columns.Add("gsis_hip", typeof(System.String));
            dtSource.Columns.Add("gsis_ceap", typeof(System.String));
            dtSource.Columns.Add("gsis_addl_ins", typeof(System.String));
            dtSource.Columns.Add("other_premium1", typeof(System.String));
            dtSource.Columns.Add("other_premium2", typeof(System.String));
            dtSource.Columns.Add("other_premium3", typeof(System.String));
            dtSource.Columns.Add("other_premium4", typeof(System.String));
            dtSource.Columns.Add("other_premium5", typeof(System.String));
            dtSource.Columns.Add("gsis_conso_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_policy_reg_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_policy_opt_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_uoli_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_emergency_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_ecard_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_educ_asst_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_real_state_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_sos_ln", typeof(System.String));
            dtSource.Columns.Add("hdmf_mpl_ln", typeof(System.String));
            dtSource.Columns.Add("hdmf_hse_ln", typeof(System.String));
            dtSource.Columns.Add("hdmf_cal_ln", typeof(System.String));
            dtSource.Columns.Add("nico_ln", typeof(System.String));
            dtSource.Columns.Add("network_ln", typeof(System.String));
            dtSource.Columns.Add("ccmpc_ln", typeof(System.String));
            dtSource.Columns.Add("other_loan1", typeof(System.String));
            dtSource.Columns.Add("other_loan2", typeof(System.String));
            dtSource.Columns.Add("other_loan3", typeof(System.String));
            dtSource.Columns.Add("other_loan4", typeof(System.String));
            dtSource.Columns.Add("other_loan5", typeof(System.String));
            dtSource.Columns.Add("post_status", typeof(System.String));
            dtSource.Columns.Add("days_worked", typeof(System.String));
            dtSource.Columns.Add("hours_worked", typeof(System.String));
            dtSource.Columns.Add("gross_pay", typeof(System.String));
            dtSource.Columns.Add("lowp_amount_pera", typeof(System.String));

            //Add Field         -- 03/12/2019
            dtSource.Columns.Add("nhmfc_hsing", typeof(System.String));
            dtSource.Columns.Add("nafc_svlf", typeof(System.String));

            //Add Field Again   -- 03/14/2019
            dtSource.Columns.Add("gsis_help", typeof(System.String));
            dtSource.Columns.Add("gsis_housing_ln", typeof(System.String));
            dtSource.Columns.Add("date_posted", typeof(System.String));

            //Add Field Again   -- 03/18/2019
            dtSource.Columns.Add("hdmf_mp2", typeof(System.String));

            //Add Field Again   -- 03/20/2019
            dtSource.Columns.Add("hdmf_loyalty_card", typeof(System.String));

            //Add Field Again   -- 05/06/2019
            dtSource.Columns.Add("lates_mins_hrs", typeof(System.String));
            dtSource.Columns.Add("lates_amount", typeof(System.String));
            dtSource.Columns.Add("remarks", typeof(System.String));

            //Add Field Again   -- 06/20/2019
            dtSource.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            dtSource.Columns.Add("posted_by_user", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));
            dtSource.Columns.Add("lates_amount_pera", typeof(System.String));
            
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollregistry_dtl_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr", "empl_id" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["rate_basis"] = string.Empty;
            nrow["monthly_rate"] = string.Empty;
            nrow["daily_rate"] = string.Empty;
            nrow["pera_amt"] = string.Empty;
            nrow["net_pay"] = string.Empty;
            nrow["net_pay1"] = string.Empty;
            nrow["net_pay2"] = string.Empty;
            nrow["lowpay_day"] = string.Empty;
            nrow["lowp_amount"] = string.Empty;
            nrow["gsis_gs"] = string.Empty;
            nrow["gsis_ps"] = string.Empty;
            nrow["sif_gs"] = string.Empty;
            nrow["hdmf_gs"] = string.Empty;
            nrow["hdmf_ps"] = string.Empty;
            nrow["phic_gs"] = string.Empty;
            nrow["phic_ps"] = string.Empty;
            nrow["wtax"] = string.Empty;
            nrow["gsis_uoli"] = string.Empty;
            nrow["gsis_uoli45"] = string.Empty;
            nrow["gsis_uoli50"] = string.Empty;
            nrow["gsis_uoli55"] = string.Empty;
            nrow["gsis_uoli60"] = string.Empty;
            nrow["gsis_uoli65"] = string.Empty;
            nrow["sss_ps"] = string.Empty;
            nrow["hdmf_ps2"] = string.Empty;
            nrow["philamlife_ps"] = string.Empty;
            nrow["gsis_ehp"] = string.Empty;
            nrow["gsis_hip"] = string.Empty;
            nrow["gsis_ceap"] = string.Empty;
            nrow["gsis_addl_ins"] = string.Empty;
            nrow["other_premium1"] = string.Empty;
            nrow["other_premium2"] = string.Empty;
            nrow["other_premium3"] = string.Empty;
            nrow["other_premium4"] = string.Empty;
            nrow["other_premium5"] = string.Empty;
            nrow["gsis_conso_ln"] = string.Empty;
            nrow["gsis_policy_reg_ln"] = string.Empty;
            nrow["gsis_policy_opt_ln"] = string.Empty;
            nrow["gsis_uoli_ln"] = string.Empty;
            nrow["gsis_emergency_ln"] = string.Empty;
            nrow["gsis_ecard_ln"] = string.Empty;
            nrow["gsis_educ_asst_ln"] = string.Empty;
            nrow["gsis_real_state_ln"] = string.Empty;
            nrow["gsis_sos_ln"] = string.Empty;
            nrow["hdmf_mpl_ln"] = string.Empty;
            nrow["hdmf_hse_ln"] = string.Empty;
            nrow["hdmf_cal_ln"] = string.Empty;
            nrow["nico_ln"] = string.Empty;
            nrow["network_ln"] = string.Empty;
            nrow["ccmpc_ln"] = string.Empty;
            nrow["other_loan1"] = string.Empty;
            nrow["other_loan2"] = string.Empty;
            nrow["other_loan3"] = string.Empty;
            nrow["other_loan4"] = string.Empty;
            nrow["other_loan5"] = string.Empty;
            nrow["post_status"] = string.Empty;

            nrow["days_worked"] = string.Empty;
            nrow["hours_worked"] = string.Empty;
            nrow["gross_pay"] = string.Empty;
            nrow["lowp_amount_pera"] = string.Empty;

            // Add Field        - 03/12/2019
            nrow["nhmfc_hsing"] = string.Empty;
            nrow["nafc_svlf"] = string.Empty;

            // Add Field Again - 03/14/2019
            nrow["gsis_help"] = string.Empty;
            nrow["gsis_housing_ln"] = string.Empty;
            nrow["date_posted"] = string.Empty;

            //Add Field Again   -- 03/18/2019
            nrow["hdmf_mp2"] = string.Empty;

            //Add Field Again   -- 03/20/2019
            nrow["hdmf_loyalty_card"] = string.Empty;

            //Add Field Again   -- 05/06/2019
            nrow["lates_mins_hrs"] = string.Empty;
            nrow["lates_amount"] = string.Empty;
            nrow["remarks"] = string.Empty;

            //Add Field Again   -- 06/20/2019
            nrow["voucher_nbr"] = string.Empty;
            nrow["created_by_user"] = string.Empty;
            nrow["updated_by_user"] = string.Empty;
            nrow["posted_by_user"] = string.Empty;
            nrow["created_dttm"] = string.Empty;
            nrow["updated_dttm"] = string.Empty;

            nrow["lates_amount_pera"] = string.Empty;
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year = commandArgs[2];
            txtb_reason.Text = "";
            FieldValidationColorChanged(false,"ALL");
            if (Session["ep_post_authority"].ToString() == "0")
            {
                // This is the Normal Delete if the user is Non-accounting user
                deleteRec1.Text = "Are you sure to delete this Record ?";
                deleteRec0.InnerText = "Delete this Record ?";
                lbl_unposting.Text = "";
                txtb_reason.Visible = false;
                lnkBtnYes.Text = "Yes, Delete it";
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                // This is Message if the accounting user will unpost the card 
                deleteRec1.Text = "Are you sure you want to UnPost this Record ?";
                deleteRec0.InnerText = "UnPost this Record ?";
                lbl_unposting.Text = "Reason for UnPosting :";
                txtb_reason.Visible = true;
                lnkBtnYes.Text = "Yes, UnPost it";
            }

            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "'";

            if (Session["ep_post_authority"].ToString() == "0")
            {
                // Non-Accounting Delete 
                MyCmn.DeleteBackEndData("payrollregistry_dtl_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_othded_tbl", "WHERE " + deleteExpression);
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

                    //4.4.b.Update the following fields: From payrollregistry_dtl_tbl Table
                    //    1.voucher_nbr     =   { blank}
                    //    2.posted_by_user  =   { blank}
                    //    3.post_status     =   "N"   
                    //    4.date_posted     =   { blank}
                    //    5.updated_by_user =   Session User ID
                    //    6.updated_dttm    =   System Date

                    string setparams = "";
                    setparams = "voucher_nbr = '',posted_by_user = '',post_status='N',date_posted='',updated_by_user= '"+ Session["ep_user_id"].ToString() + "',updated_dttm = '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"'";
                    MyCmn.UpdateTable("payrollregistry_dtl_tbl", setparams, "WHERE "+ deleteExpression);
                    RetrieveDataListGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
                }
            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            editaddmodal(e.CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        private void editaddmodal(string session_val)
        {
            // BEGIN - Pass Value
            // Employee ID      [0]
            // Registry         [1]
            // Year             [2]
            // Employment Type  [3]
            // Department       [4]
            // END   - Pass Value

            string[] svalues = session_val.ToString().Split(new char[] { ',' });
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND payroll_registry_nbr = '" + svalues[1].ToString().Trim() + "' AND payroll_year = '" + svalues[2].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            //DataRow[] row2Edit2 = dtSource_for_names.Select("empl_id = '" + svalues[0].ToString().Trim() + "'");

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            lbl_registry_no.Text = svalues[1].ToString().Trim();
            txtb_employeename.Text = row2Edit[0]["employee_name"].ToString();
            //ddl_empl_id.SelectedValue      = row2Edit[0]["empl_id"].ToString();
            txtb_empl_id.Text = svalues[0].ToString().Trim();

            txtb_employeename.Visible = true;
            ddl_empl_id.Visible = false;

            if (row2Edit[0]["department_code"].ToString() != string.Empty)
            {
                ddl_dep.SelectedValue       = row2Edit[0]["department_code"].ToString();
            }
            if (row2Edit[0]["subdepartment_code"].ToString() != string.Empty)
            {
                ddl_subdep.SelectedValue    = row2Edit[0]["subdepartment_code"].ToString();
            }
            else
            {
                ddl_subdep.SelectedIndex    = -1;
            }
            RetrieveBindingDivision();

            if (row2Edit[0]["division_code"].ToString() != string.Empty && row2Edit[0]["division_code"].ToString() != "")
            {
                ddl_division.SelectedValue  = row2Edit[0]["division_code"].ToString();
            }
            else
            {
                ddl_division.SelectedIndex  = -1;
            }
            RetrieveBindingSection();

            if (row2Edit[0]["section_code"].ToString() != string.Empty)
            {
                ddl_section.SelectedValue   = row2Edit[0]["section_code"].ToString();
            }

            ddl_fund_charges.SelectedValue = row2Edit[0]["fund_code"].ToString();

            lbl_rate_basis_hidden.Text      = row2Edit[0]["rate_basis"].ToString();
            txtb_rate_amount.Text           = row2Edit[0]["monthly_rate"].ToString();
            ViewState["daily_rate"]         = row2Edit[0]["daily_rate"].ToString();
            txtb_pera_amount.Text           = row2Edit[0]["pera_amt"].ToString();
            txtb_net_pay.Text               = row2Edit[0]["net_pay"].ToString();
            txtb_net_pay_1h.Text            = row2Edit[0]["net_pay1"].ToString();
            txtb_net_pay_2h.Text            = row2Edit[0]["net_pay2"].ToString();
            txtb_no_days_lwopay.Text        = row2Edit[0]["lowpay_day"].ToString();
            txtb_lwo_pay.Text               = row2Edit[0]["lowp_amount"].ToString();

            txtb_gsis_gs.Text               = row2Edit[0]["gsis_gs"].ToString();
            txtb_gsis_ps.Text               = row2Edit[0]["gsis_ps"].ToString();
            txtb_gsis_sif.Text              = row2Edit[0]["sif_gs"].ToString();
            txtb_hdmf_gs.Text               = row2Edit[0]["hdmf_gs"].ToString();
            txtb_hdmf_ps.Text               = row2Edit[0]["hdmf_ps"].ToString();
            txtb_phic_gs.Text               = row2Edit[0]["phic_gs"].ToString();
            txtb_phic_ps.Text               = row2Edit[0]["phic_ps"].ToString();
            txtb_bir_tax.Text               = row2Edit[0]["wtax"].ToString();

            txtb_gsis_ouli.Text             = row2Edit[0]["gsis_uoli"].ToString();
            txtb_gsis_ouli45.Text           = row2Edit[0]["gsis_uoli45"].ToString();
            txtb_gsis_ouli50.Text           = row2Edit[0]["gsis_uoli50"].ToString();
            txtb_gsis_ouli55.Text           = row2Edit[0]["gsis_uoli55"].ToString();
            txtb_gsis_ouli60.Text           = row2Edit[0]["gsis_uoli60"].ToString();
            txtb_gsis_ouli65.Text           = row2Edit[0]["gsis_uoli65"].ToString();
            txtb_sss.Text                   = row2Edit[0]["sss_ps"].ToString();
            txtb_hdmf_addl.Text             = row2Edit[0]["hdmf_ps2"].ToString();
            txtb_philam.Text                = row2Edit[0]["philamlife_ps"].ToString();
            txtb_gsis_ehp.Text              = row2Edit[0]["gsis_ehp"].ToString();
            txtb_gsis_hip.Text              = row2Edit[0]["gsis_hip"].ToString();
            txtb_gsis_ceap.Text             = row2Edit[0]["gsis_ceap"].ToString();
            txtb_gsis_add.Text              = row2Edit[0]["gsis_addl_ins"].ToString();
            txtb_otherpremium_no1.Text      = row2Edit[0]["other_premium1"].ToString();
            txtb_otherpremium_no2.Text      = row2Edit[0]["other_premium2"].ToString();
            txtb_otherpremium_no3.Text      = row2Edit[0]["other_premium3"].ToString();
            txtb_otherpremium_no4.Text      = row2Edit[0]["other_premium4"].ToString();
            txtb_otherpremium_no5.Text      = row2Edit[0]["other_premium5"].ToString();
            txtb_gsis_consolidated.Text     = row2Edit[0]["gsis_conso_ln"].ToString();
            txtb_gsis_policy_regular.Text   = row2Edit[0]["gsis_policy_reg_ln"].ToString();
            txtb_gsis_policy_optional.Text  = row2Edit[0]["gsis_policy_opt_ln"].ToString();
            txtb_gsis_ouli_loan.Text        = row2Edit[0]["gsis_uoli_ln"].ToString();
            txtb_gsis_emer_loan.Text        = row2Edit[0]["gsis_emergency_ln"].ToString();
            txtb_gsis_ecard_loan.Text       = row2Edit[0]["gsis_ecard_ln"].ToString();
            txtb_gsis_educ_loan.Text        = row2Edit[0]["gsis_educ_asst_ln"].ToString();
            txtb_gsis_real_loan.Text        = row2Edit[0]["gsis_real_state_ln"].ToString();
            txtb_gsis_sos_loan.Text         = row2Edit[0]["gsis_sos_ln"].ToString();
            txtb_hdmf_mpl_loan.Text         = row2Edit[0]["hdmf_mpl_ln"].ToString();
            txtb_hdmf_house_loan.Text       = row2Edit[0]["hdmf_hse_ln"].ToString();
            txtb_hdmf_cal_loan.Text         = row2Edit[0]["hdmf_cal_ln"].ToString();
            txtb_nico_loan.Text             = row2Edit[0]["nico_ln"].ToString();
            txtb_networkbank_loan.Text      = row2Edit[0]["network_ln"].ToString();
            txtb_ccmpc_loan.Text            = row2Edit[0]["ccmpc_ln"].ToString();
            txtb_otherloan_no1.Text         = row2Edit[0]["other_loan1"].ToString();
            txtb_otherloan_no2.Text         = row2Edit[0]["other_loan2"].ToString();
            txtb_otherloan_no3.Text         = row2Edit[0]["other_loan3"].ToString();
            txtb_otherloan_no4.Text         = row2Edit[0]["other_loan4"].ToString();
            txtb_otherloan_no5.Text         = row2Edit[0]["other_loan5"].ToString();

            txtb_no_days_worked.Text        = row2Edit[0]["days_worked"].ToString();
            txtb_no_hours_worked.Text       = row2Edit[0]["hours_worked"].ToString();
            txtb_gross_pay.Text             = row2Edit[0]["gross_pay"].ToString();
            txtb_lwop_amount_pera.Text      = row2Edit[0]["lowp_amount_pera"].ToString();

            // Add Field - 03/12/2019
            txtb_nhmfc_hsng.Text            = row2Edit[0]["nhmfc_hsing"].ToString();
            txtb_nafc.Text                  = row2Edit[0]["nafc_svlf"].ToString();

            // Add Field Again - 03/14/2019
            txtb_gsis_help.Text             = row2Edit[0]["gsis_help"].ToString();
            txtb_gsis_housing_loan.Text     = row2Edit[0]["gsis_housing_ln"].ToString();

            // Add Field Again - 03/18/2019
            txtb_hdmf_mp2.Text              = row2Edit[0]["hdmf_mp2"].ToString();

            // Add Field Again - 03/18/2019
            txtb_hdmf_loyalty_card.Text     = row2Edit[0]["hdmf_loyalty_card"].ToString();

            // Add Field Again - 05/06/2019
            txtb_lates_in_min.Text          = row2Edit[0]["lates_mins_hrs"].ToString();
            txtb_late_in_amount.Text        = row2Edit[0]["lates_amount"].ToString();
            txtb_remarks.Text               = row2Edit[0]["remarks"].ToString();
            txtb_late_in_amount_pera.Text   = row2Edit[0]["lates_amount_pera"].ToString();


            // Add Field Again - VJA - 2022-05-26 - Other Deduction 
            txtb_other_ded_mand1.Text   =  row2Edit[0]["other_ded_mand1"].ToString();
            txtb_other_ded_mand2.Text   =  row2Edit[0]["other_ded_mand2"].ToString();
            txtb_other_ded_mand3.Text   =  row2Edit[0]["other_ded_mand3"].ToString();
            txtb_other_ded_mand4.Text   =  row2Edit[0]["other_ded_mand4"].ToString();
            txtb_other_ded_mand5.Text   =  row2Edit[0]["other_ded_mand5"].ToString();
            txtb_other_ded_mand6.Text   =  row2Edit[0]["other_ded_mand6"].ToString();
            txtb_other_ded_mand7.Text   =  row2Edit[0]["other_ded_mand7"].ToString();
            txtb_other_ded_mand8.Text   =  row2Edit[0]["other_ded_mand8"].ToString();
            txtb_other_ded_mand9.Text   =  row2Edit[0]["other_ded_mand9"].ToString();
            txtb_other_ded_mand10.Text  =  row2Edit[0]["other_ded_mand10"].ToString();
            txtb_other_ded_prem1.Text   =  row2Edit[0]["other_ded_prem1"].ToString();
            txtb_other_ded_prem2.Text   =  row2Edit[0]["other_ded_prem2"].ToString();
            txtb_other_ded_prem3.Text   =  row2Edit[0]["other_ded_prem3"].ToString();
            txtb_other_ded_prem4.Text   =  row2Edit[0]["other_ded_prem4"].ToString();
            txtb_other_ded_prem5.Text   =  row2Edit[0]["other_ded_prem5"].ToString();
            txtb_other_ded_prem6.Text   =  row2Edit[0]["other_ded_prem6"].ToString();
            txtb_other_ded_prem7.Text   =  row2Edit[0]["other_ded_prem7"].ToString();
            txtb_other_ded_prem8.Text   =  row2Edit[0]["other_ded_prem8"].ToString();
            txtb_other_ded_prem9.Text   =  row2Edit[0]["other_ded_prem9"].ToString();
            txtb_other_ded_prem10.Text  =  row2Edit[0]["other_ded_prem10"].ToString();
            txtb_other_ded_loan1.Text   =  row2Edit[0]["other_ded_loan1"].ToString();
            txtb_other_ded_loan2.Text   =  row2Edit[0]["other_ded_loan2"].ToString();
            txtb_other_ded_loan3.Text   =  row2Edit[0]["other_ded_loan3"].ToString();
            txtb_other_ded_loan4.Text   =  row2Edit[0]["other_ded_loan4"].ToString();
            txtb_other_ded_loan5.Text   =  row2Edit[0]["other_ded_loan5"].ToString();
            txtb_other_ded_loan6.Text   =  row2Edit[0]["other_ded_loan6"].ToString();
            txtb_other_ded_loan7.Text   =  row2Edit[0]["other_ded_loan7"].ToString();
            txtb_other_ded_loan8.Text   =  row2Edit[0]["other_ded_loan8"].ToString();
            txtb_other_ded_loan9.Text   =  row2Edit[0]["other_ded_loan9"].ToString();
            txtb_other_ded_loan10.Text  =  row2Edit[0]["other_ded_loan10"].ToString();

            // During The Employee Name Change 
            calculate_salary_summary_tab();
            calculate_pera_summary_tab();
            calculate_mandatory();
            calculate_optional();
            calculate_loans();
            //calculate_gross_regular();
            //HeaderDetails_Initialized_Add();
            //calculate_netpays();
            
            ddl_empl_id.Enabled = false;
            LabelAddEdit.Text = "Edit Record | Registry No : " + lbl_registry_no.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            lbl_addeditmode_hidden.Text = MyCmn.CONST_EDIT;


            // Add Field Again - 06/20/2019
            txtb_voucher_nbr.Text           = row2Edit[0]["voucher_nbr"].ToString();
            ViewState["created_by_user"]    = row2Edit[0]["created_by_user"].ToString();
            ViewState["updated_by_user"]    = row2Edit[0]["updated_by_user"].ToString();
            ViewState["posted_by_user"]     = row2Edit[0]["posted_by_user"].ToString();
            ViewState["created_dttm"]       = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            ViewState["updated_dttm"]       = Convert.ToDateTime(row2Edit[0]["updated_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            //ddl_status.SelectedValue        = row2Edit[0]["post_status"].ToString();
            lbl_post_status.Text                = row2Edit[0]["post_status_descr"].ToString();
            txtb_status.Text                = row2Edit[0]["post_status_descr"].ToString();
            txtb_date_posted.Text           = row2Edit[0]["date_posted"].ToString();
            txtb_position.Text              = row2Edit[0]["position_title1"].ToString();

            FieldValidationColorChanged(false, "ALL");

            // The Save Button Will be Visible false if the 
            if (row2Edit[0]["post_status"].ToString() == "Y" && (Session["ep_post_authority"].ToString() == "1" || Session["ep_post_authority"].ToString() == "0"))
            {
                // For Accounting or Other User With Y Status
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                Linkbtncancel.Text = "Close";
                txtb_voucher_nbr.Enabled = false;
                lbl_if_dateposted_yes.Text = "This Payroll Already Posted, You cannot Edit!";
                btn_contributions.Visible = false;
                btn_editloan.Visible = false;
                btnSave.Text = "Save";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            else if ((row2Edit[0]["post_status"].ToString() == "N" || row2Edit[0]["post_status"].ToString() == "") && Session["ep_post_authority"].ToString() == "1")
            {
                // For Accounting With N Status
                btnSave.Visible = true;
                btn_calculate.Visible = false;
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = true;
                btn_contributions.Visible = false;
                btn_editloan.Visible = false;
                btnSave.Text = "Post to Card";
                txtb_voucher_nbr.Enabled = true;
                txtb_date_posted.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }
            else if (row2Edit[0]["post_status"].ToString()   == "R"
                    // || row2Edit[0]["post_status"].ToString() == "T"
                    || row2Edit[0]["post_status"].ToString() == "X"
                    )
            {
                // For Accounting With N Status
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                Linkbtncancel.Text = "Close";
                txtb_voucher_nbr.Enabled = false;
                lbl_if_dateposted_yes.Text = "This Payroll Already "+ row2Edit[0]["post_status_descr"].ToString() + ", You cannot Edit!";
                btn_contributions.Visible = false;
                btn_editloan.Visible = false;
                btnSave.Text = "Save";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            else if (Session["ep_post_authority"].ToString() == "0")
            {
                // For Other Users
                btnSave.Visible = true;
                btn_calculate.Visible = true;
                Linkbtncancel.Text = "Cancel";
                txtb_voucher_nbr.Enabled = true;
                lbl_if_dateposted_yes.Text = "";
                btn_contributions.Visible = true;
                btn_editloan.Visible = true;
                btnSave.Text = "Save";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = "";
                ToogleTextbox(true);
            }
            UpdatePanel10.Update();
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Change Field Sort mode  
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
        //  BEGIN - VJA- 04/06/2019 - Get Grid current sort order 
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
        //  BEGIN - VJA- 04/06/2019 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;

            if (IsDataValidated())
            {
                //Calculate All Fields and Insert to the Haader
                calculate_salary_summary_tab();
                calculate_pera_summary_tab();
                calculate_mandatory();
                calculate_optional();
                calculate_loans();
                calculate_gross_regular();
                //HeaderDetails_Initialized_Add();
                calculate_netpays();

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"]    = lbl_registry_no.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                 = ddl_empl_id.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]              = lbl_rate_basis_hidden.Text.ToString();
                    dtSource.Rows[0]["monthly_rate"]            = txtb_rate_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]              = lbl_lastday_daily_rate_hidden.Text.ToString().Trim();
                    dtSource.Rows[0]["pera_amt"]                = txtb_pera_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay1"]                = txtb_net_pay_1h.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay2"]                = txtb_net_pay_2h.Text.ToString().Trim();
                    dtSource.Rows[0]["lowpay_day"]              = txtb_no_days_lwopay.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount"]             = txtb_lwo_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_gs"]                 = txtb_gsis_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ps"]                 = txtb_gsis_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["sif_gs"]                  = txtb_gsis_sif.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_gs"]                 = txtb_hdmf_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps"]                 = txtb_hdmf_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_gs"]                 = txtb_phic_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_ps"]                 = txtb_phic_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["wtax"]                    = txtb_bir_tax.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli"]               = txtb_gsis_ouli.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli45"]             = txtb_gsis_ouli45.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli50"]             = txtb_gsis_ouli50.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli55"]             = txtb_gsis_ouli55.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli60"]             = txtb_gsis_ouli60.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli65"]             = txtb_gsis_ouli65.Text.ToString().Trim();
                    dtSource.Rows[0]["sss_ps"]                  = txtb_sss.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps2"]                = txtb_hdmf_addl.Text.ToString().Trim();
                    dtSource.Rows[0]["philamlife_ps"]           = txtb_philam.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ehp"]                = txtb_gsis_ehp.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_hip"]                = txtb_gsis_hip.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ceap"]               = txtb_gsis_ceap.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_addl_ins"]           = txtb_gsis_add.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium1"]          = txtb_otherpremium_no1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium2"]          = txtb_otherpremium_no2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium3"]          = txtb_otherpremium_no3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium4"]          = txtb_otherpremium_no4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium5"]          = txtb_otherpremium_no5.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_conso_ln"]           = txtb_gsis_consolidated.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_policy_reg_ln"]      = txtb_gsis_policy_regular.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_policy_opt_ln"]      = txtb_gsis_policy_optional.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli_ln"]            = txtb_gsis_ouli_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_emergency_ln"]       = txtb_gsis_emer_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ecard_ln"]           = txtb_gsis_ecard_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_educ_asst_ln"]       = txtb_gsis_educ_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_real_state_ln"]      = txtb_gsis_real_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_sos_ln"]             = txtb_gsis_sos_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_mpl_ln"]             = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_hse_ln"]             = txtb_hdmf_house_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_cal_ln"]             = txtb_hdmf_cal_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["network_ln"]              = txtb_networkbank_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan1"]             = txtb_otherloan_no1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan2"]             = txtb_otherloan_no2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan3"]             = txtb_otherloan_no3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan4"]             = txtb_otherloan_no4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan5"]             = txtb_otherloan_no5.Text.ToString().Trim();
                    dtSource.Rows[0]["post_status"]             = "N";

                    dtSource.Rows[0]["days_worked"]             = txtb_no_days_worked.Text.ToString().Trim();
                    dtSource.Rows[0]["hours_worked"]            = txtb_no_hours_worked.Text.ToString().Trim();
                    dtSource.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount_pera"]        = txtb_lwop_amount_pera.Text.ToString().Trim();
                    
                    // Add Field - 03/12/2019
                    dtSource.Rows[0]["nhmfc_hsing"]             = txtb_nhmfc_hsng.Text.ToString().Trim();
                    dtSource.Rows[0]["nafc_svlf"]               = txtb_nafc.Text.ToString().Trim();

                    // Add Field Again  - 03/14/2019
                    dtSource.Rows[0]["gsis_help"]               = txtb_gsis_help.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_housing_ln"]         = txtb_gsis_housing_loan.Text.ToString().Trim();

                    // Add Field Again  - 03/18/2019
                    dtSource.Rows[0]["hdmf_mp2"]                = txtb_hdmf_mp2.Text.ToString().Trim();
                    
                    // Add Field Again  - 03/20/2019
                    dtSource.Rows[0]["hdmf_loyalty_card"]       = txtb_hdmf_loyalty_card.Text.ToString().Trim();

                    // Add Field Again  - 05/06/2019
                    dtSource.Rows[0]["lates_mins_hrs"]          = txtb_lates_in_min.Text.ToString().Trim();
                    dtSource.Rows[0]["lates_amount"]            = txtb_late_in_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["remarks"]                 = txtb_remarks.Text.ToString().Trim();

                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource.Rows[0]["voucher_nbr"]             = txtb_voucher_nbr.Text.ToString();
                    dtSource.Rows[0]["created_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_by_user"]         = "";
                    
                    dtSource.Rows[0]["created_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["updated_dttm"]            = "";

                    dtSource.Rows[0]["posted_by_user"]          = "";
                    dtSource.Rows[0]["date_posted"]             = "";

                    dtSource.Rows[0]["lates_amount_pera"]       = txtb_late_in_amount_pera.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"]    = lbl_registry_no.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]              = lbl_rate_basis_hidden.Text.ToString();
                    dtSource.Rows[0]["monthly_rate"]            = txtb_rate_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]              = lbl_lastday_daily_rate_hidden.Text.ToString().Trim();
                    dtSource.Rows[0]["pera_amt"]                = txtb_pera_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay1"]                = txtb_net_pay_1h.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay2"]                = txtb_net_pay_2h.Text.ToString().Trim();
                    dtSource.Rows[0]["lowpay_day"]              = txtb_no_days_lwopay.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount"]             = txtb_lwo_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_gs"]                 = txtb_gsis_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ps"]                 = txtb_gsis_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["sif_gs"]                  = txtb_gsis_sif.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_gs"]                 = txtb_hdmf_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps"]                 = txtb_hdmf_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_gs"]                 = txtb_phic_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_ps"]                 = txtb_phic_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["wtax"]                    = txtb_bir_tax.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli"]               = txtb_gsis_ouli.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli45"]             = txtb_gsis_ouli45.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli50"]             = txtb_gsis_ouli50.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli55"]             = txtb_gsis_ouli55.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli60"]             = txtb_gsis_ouli60.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli65"]             = txtb_gsis_ouli65.Text.ToString().Trim();
                    dtSource.Rows[0]["sss_ps"]                  = txtb_sss.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps2"]                = txtb_hdmf_addl.Text.ToString().Trim();
                    dtSource.Rows[0]["philamlife_ps"]           = txtb_philam.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ehp"]                = txtb_gsis_ehp.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_hip"]                = txtb_gsis_hip.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ceap"]               = txtb_gsis_ceap.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_addl_ins"]           = txtb_gsis_add.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium1"]          = txtb_otherpremium_no1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium2"]          = txtb_otherpremium_no2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium3"]          = txtb_otherpremium_no3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium4"]          = txtb_otherpremium_no4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium5"]          = txtb_otherpremium_no5.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_conso_ln"]           = txtb_gsis_consolidated.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_policy_reg_ln"]      = txtb_gsis_policy_regular.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_policy_opt_ln"]      = txtb_gsis_policy_optional.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli_ln"]            = txtb_gsis_ouli_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_emergency_ln"]       = txtb_gsis_emer_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ecard_ln"]           = txtb_gsis_ecard_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_educ_asst_ln"]       = txtb_gsis_educ_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_real_state_ln"]      = txtb_gsis_real_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_sos_ln"]             = txtb_gsis_sos_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_mpl_ln"]             = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_hse_ln"]             = txtb_hdmf_house_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_cal_ln"]             = txtb_hdmf_cal_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["network_ln"]              = txtb_networkbank_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan1"]             = txtb_otherloan_no1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan2"]             = txtb_otherloan_no2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan3"]             = txtb_otherloan_no3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan4"]             = txtb_otherloan_no4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan5"]             = txtb_otherloan_no5.Text.ToString().Trim();
                    
                    dtSource.Rows[0]["days_worked"]             = txtb_no_days_worked.Text.ToString().Trim();
                    dtSource.Rows[0]["hours_worked"]            = txtb_no_hours_worked.Text.ToString().Trim();
                    dtSource.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount_pera"]        = txtb_lwop_amount_pera.Text.ToString().Trim();
                    
                    // Add Field - 03/12/2019
                    dtSource.Rows[0]["nhmfc_hsing"]             = txtb_nhmfc_hsng.Text.ToString().Trim();
                    dtSource.Rows[0]["nafc_svlf"]               = txtb_nafc.Text.ToString().Trim();

                    // Add Field Again  - 03/14/2019
                    dtSource.Rows[0]["gsis_help"]               = txtb_gsis_help.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_housing_ln"]         = txtb_gsis_housing_loan.Text.ToString().Trim();

                    // Add Field Again  - 03/18/2019
                    dtSource.Rows[0]["hdmf_mp2"]                = txtb_hdmf_mp2.Text.ToString().Trim();

                    // Add Field Again  - 03/20/2019
                    dtSource.Rows[0]["hdmf_loyalty_card"]       = txtb_hdmf_loyalty_card.Text.ToString().Trim();

                    // Add Field Again  - 05/06/2019
                    dtSource.Rows[0]["lates_mins_hrs"]          = txtb_lates_in_min.Text.ToString().Trim();
                    dtSource.Rows[0]["lates_amount"]            = txtb_late_in_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["remarks"]                 = txtb_remarks.Text.ToString().Trim();

                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource.Rows[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                    dtSource.Rows[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["created_dttm"]            = ViewState["created_dttm"].ToString();

                    if (Session["ep_post_authority"].ToString() == "1")
                    {
                        dtSource.Rows[0]["posted_by_user"]  = Session["ep_user_id"].ToString();
                        dtSource.Rows[0]["date_posted"]     = txtb_date_posted.Text.ToString().Trim();
                        dtSource.Rows[0]["post_status"]     = "Y";
                        dtSource.Rows[0]["voucher_nbr"]     = txtb_voucher_nbr.Text.ToString();
                        dtSource.Rows[0]["updated_by_user"] = ViewState["updated_by_user"].ToString();
                        dtSource.Rows[0]["updated_dttm"]    = ViewState["updated_dttm"].ToString();
                    }

                    dtSource.Rows[0]["lates_amount_pera"] = txtb_late_in_amount_pera.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                        

                        if (scriptInsertUpdate == string.Empty) return;
                        string msg = MyCmn.insertdata(scriptInsertUpdate);
                        if (msg == "") return;

                        if (msg.Substring(0, 1) == "X")
                        {
                            FieldValidationColorChanged(true, "already-exist");
                            return;
                        }

                        InsertUpdateOtherDeduction();

                        if (saveRecord == MyCmn.CONST_ADD)
                        {
                            DataRow nrow = dataListGrid.NewRow();
                            nrow["payroll_year"]                = ddl_year.SelectedValue.ToString().Trim();
                            nrow["payroll_registry_nbr"]        = lbl_registry_no.Text.ToString().Trim();
                            nrow["empl_id"]                     = ddl_empl_id.SelectedValue.ToString().Trim();
                            nrow["rate_basis"]                  = lbl_rate_basis_hidden.Text.ToString();
                            nrow["monthly_rate"]                = txtb_rate_amount.Text.ToString().Trim();
                            nrow["daily_rate"]                  = lbl_lastday_daily_rate_hidden.Text.ToString().Trim();
                            nrow["pera_amt"]                    = txtb_pera_amount.Text.ToString().Trim();
                            nrow["net_pay"]                     = txtb_net_pay.Text.ToString().Trim();
                            nrow["net_pay1"]                    = txtb_net_pay_1h.Text.ToString().Trim();
                            nrow["net_pay2"]                    = txtb_net_pay_2h.Text.ToString().Trim();
                            nrow["lowpay_day"]                  = txtb_no_days_lwopay.Text.ToString().Trim();
                            nrow["lowp_amount"]                 = txtb_lwo_pay.Text.ToString().Trim();
                            nrow["gsis_gs"]                     = txtb_gsis_gs.Text.ToString().Trim();
                            nrow["gsis_ps"]                     = txtb_gsis_ps.Text.ToString().Trim();
                            nrow["sif_gs"]                      = txtb_gsis_sif.Text.ToString().Trim();
                            nrow["hdmf_gs"]                     = txtb_hdmf_gs.Text.ToString().Trim();
                            nrow["hdmf_ps"]                     = txtb_hdmf_ps.Text.ToString().Trim();
                            nrow["phic_gs"]                     = txtb_phic_gs.Text.ToString().Trim();
                            nrow["phic_ps"]                     = txtb_phic_ps.Text.ToString().Trim();
                            nrow["wtax"]                        = txtb_bir_tax.Text.ToString().Trim();
                            nrow["gsis_uoli"]                   = txtb_gsis_ouli.Text.ToString().Trim();
                            nrow["gsis_uoli45"]                 = txtb_gsis_ouli45.Text.ToString().Trim();
                            nrow["gsis_uoli50"]                 = txtb_gsis_ouli50.Text.ToString().Trim();
                            nrow["gsis_uoli55"]                 = txtb_gsis_ouli55.Text.ToString().Trim();
                            nrow["gsis_uoli60"]                 = txtb_gsis_ouli60.Text.ToString().Trim();
                            nrow["gsis_uoli65"]                 = txtb_gsis_ouli65.Text.ToString().Trim();
                            nrow["sss_ps"]                      = txtb_sss.Text.ToString().Trim();
                            nrow["hdmf_ps2"]                    = txtb_hdmf_addl.Text.ToString().Trim();
                            nrow["philamlife_ps"]               = txtb_philam.Text.ToString().Trim();
                            nrow["gsis_ehp"]                    = txtb_gsis_ehp.Text.ToString().Trim();
                            nrow["gsis_hip"]                    = txtb_gsis_hip.Text.ToString().Trim();
                            nrow["gsis_ceap"]                   = txtb_gsis_ceap.Text.ToString().Trim();
                            nrow["gsis_addl_ins"]               = txtb_gsis_add.Text.ToString().Trim();
                            nrow["other_premium1"]              = txtb_otherpremium_no1.Text.ToString().Trim();
                            nrow["other_premium2"]              = txtb_otherpremium_no2.Text.ToString().Trim();
                            nrow["other_premium3"]              = txtb_otherpremium_no3.Text.ToString().Trim();
                            nrow["other_premium4"]              = txtb_otherpremium_no4.Text.ToString().Trim();
                            nrow["other_premium5"]              = txtb_otherpremium_no5.Text.ToString().Trim();
                            nrow["gsis_conso_ln"]               = txtb_gsis_consolidated.Text.ToString().Trim();
                            nrow["gsis_policy_reg_ln"]          = txtb_gsis_policy_regular.Text.ToString().Trim();
                            nrow["gsis_policy_opt_ln"]          = txtb_gsis_policy_optional.Text.ToString().Trim();
                            nrow["gsis_uoli_ln"]                = txtb_gsis_ouli_loan.Text.ToString().Trim();
                            nrow["gsis_emergency_ln"]           = txtb_gsis_emer_loan.Text.ToString().Trim();
                            nrow["gsis_ecard_ln"]               = txtb_gsis_ecard_loan.Text.ToString().Trim();
                            nrow["gsis_educ_asst_ln"]           = txtb_gsis_educ_loan.Text.ToString().Trim();
                            nrow["gsis_real_state_ln"]          = txtb_gsis_real_loan.Text.ToString().Trim();
                            nrow["gsis_sos_ln"]                 = txtb_gsis_sos_loan.Text.ToString().Trim();
                            nrow["hdmf_mpl_ln"]                 = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                            nrow["hdmf_hse_ln"]                 = txtb_hdmf_house_loan.Text.ToString().Trim();
                            nrow["hdmf_cal_ln"]                 = txtb_hdmf_cal_loan.Text.ToString().Trim();
                            nrow["nico_ln"]                     = txtb_nico_loan.Text.ToString().Trim();
                            nrow["network_ln"]                  = txtb_networkbank_loan.Text.ToString().Trim();
                            nrow["ccmpc_ln"]                    = txtb_ccmpc_loan.Text.ToString().Trim();
                            nrow["other_loan1"]                 = txtb_otherloan_no1.Text.ToString().Trim();
                            nrow["other_loan2"]                 = txtb_otherloan_no2.Text.ToString().Trim();
                            nrow["other_loan3"]                 = txtb_otherloan_no3.Text.ToString().Trim();
                            nrow["other_loan4"]                 = txtb_otherloan_no4.Text.ToString().Trim();
                            nrow["other_loan5"]                 = txtb_otherloan_no5.Text.ToString().Trim();
                            

                            nrow["days_worked"]                 = txtb_no_days_worked.Text.ToString().Trim();
                            nrow["hours_worked"]                = txtb_no_hours_worked.Text.ToString().Trim();
                            nrow["gross_pay"]                   = txtb_gross_pay.Text.ToString().Trim();
                            nrow["lowp_amount_pera"]            = txtb_lwop_amount_pera.Text.ToString().Trim();

                            // Add Field - 03/12/2019
                            nrow["nhmfc_hsing"]                 = txtb_nhmfc_hsng.Text.ToString().Trim();
                            nrow["nafc_svlf"]                   = txtb_nafc.Text.ToString().Trim();

                            // Add Field Again  - 03/14/2019
                            nrow["gsis_help"]                   = txtb_gsis_help.Text.ToString().Trim();
                            nrow["gsis_housing_ln"]             = txtb_gsis_housing_loan.Text.ToString().Trim();

                            // Add Field Again  - 03/18/2019
                            nrow["hdmf_mp2"]                    = txtb_hdmf_mp2.Text.ToString().Trim(); 
                        
                            // Add Field Again  - 03/20/2019
                            nrow["hdmf_loyalty_card"]           = txtb_hdmf_loyalty_card.Text.ToString().Trim();

                            nrow["employee_name"]               = ddl_empl_id.SelectedItem.ToString().Trim();

                            // Add Field Again  - 05/06/2019
                            nrow["lates_mins_hrs"]              = txtb_lates_in_min.Text.ToString().Trim();
                            nrow["lates_amount"]                = txtb_late_in_amount.Text.ToString().Trim();
                            nrow["remarks"]                     = txtb_remarks.Text.ToString().Trim();
                        
                            nrow["department_code"]             = ddl_dep.SelectedValue.ToString().Trim();
                            nrow["subdepartment_code"]          = ddl_subdep.SelectedValue.ToString().Trim();
                            nrow["division_code"]               = ddl_division.SelectedValue.ToString().Trim();
                            nrow["section_code"]                = ddl_section.SelectedValue.ToString().Trim();
                            nrow["fund_code"]                   = ddl_fund_charges.SelectedValue.ToString().Trim();

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
                            nrow["lates_amount_pera"]       = txtb_late_in_amount_pera.Text.ToString().Trim();

                        // Add Field  2022-05-30
                        nrow["other_ded_mand1"]       = txtb_other_ded_mand1.Text.ToString().Trim();
                        nrow["other_ded_mand2"]       = txtb_other_ded_mand2.Text.ToString().Trim();
                        nrow["other_ded_mand3"]       = txtb_other_ded_mand3.Text.ToString().Trim();
                        nrow["other_ded_mand4"]       = txtb_other_ded_mand4.Text.ToString().Trim();
                        nrow["other_ded_mand5"]       = txtb_other_ded_mand5.Text.ToString().Trim();
                        nrow["other_ded_mand6"]       = txtb_other_ded_mand6.Text.ToString().Trim();
                        nrow["other_ded_mand7"]       = txtb_other_ded_mand7.Text.ToString().Trim();
                        nrow["other_ded_mand8"]       = txtb_other_ded_mand8.Text.ToString().Trim();
                        nrow["other_ded_mand9"]       = txtb_other_ded_mand9.Text.ToString().Trim();
                        nrow["other_ded_mand10"]      = txtb_other_ded_mand10.Text.ToString().Trim();
                        nrow["other_ded_prem1"]       = txtb_other_ded_prem1.Text.ToString().Trim();
                        nrow["other_ded_prem2"]       = txtb_other_ded_prem2.Text.ToString().Trim();
                        nrow["other_ded_prem3"]       = txtb_other_ded_prem3.Text.ToString().Trim();
                        nrow["other_ded_prem4"]       = txtb_other_ded_prem4.Text.ToString().Trim();
                        nrow["other_ded_prem5"]       = txtb_other_ded_prem5.Text.ToString().Trim();
                        nrow["other_ded_prem6"]       = txtb_other_ded_prem6.Text.ToString().Trim();
                        nrow["other_ded_prem7"]       = txtb_other_ded_prem7.Text.ToString().Trim();
                        nrow["other_ded_prem8"]       = txtb_other_ded_prem8.Text.ToString().Trim();
                        nrow["other_ded_prem9"]       = txtb_other_ded_prem9.Text.ToString().Trim();
                        nrow["other_ded_prem10"]      = txtb_other_ded_prem10.Text.ToString().Trim();
                        nrow["other_ded_loan1"]       = txtb_other_ded_loan1.Text.ToString().Trim();
                        nrow["other_ded_loan2"]       = txtb_other_ded_loan2.Text.ToString().Trim();
                        nrow["other_ded_loan3"]       = txtb_other_ded_loan3.Text.ToString().Trim();
                        nrow["other_ded_loan4"]       = txtb_other_ded_loan4.Text.ToString().Trim();
                        nrow["other_ded_loan5"]       = txtb_other_ded_loan5.Text.ToString().Trim();
                        nrow["other_ded_loan6"]       = txtb_other_ded_loan6.Text.ToString().Trim();
                        nrow["other_ded_loan7"]       = txtb_other_ded_loan7.Text.ToString().Trim();
                        nrow["other_ded_loan8"]       = txtb_other_ded_loan8.Text.ToString().Trim();
                        nrow["other_ded_loan9"]       = txtb_other_ded_loan9.Text.ToString().Trim();
                        nrow["other_ded_loan10"]      = txtb_other_ded_loan10.Text.ToString().Trim();


                        dataListGrid.Rows.Add(nrow);
                            gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                            SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        }
                        if (saveRecord == MyCmn.CONST_EDIT)
                        {
                            string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "'";
                            DataRow[] row2Edit = dataListGrid.Select(editExpression);

                            row2Edit[0]["payroll_year"]               = ddl_year.SelectedValue.ToString().Trim();  
                            row2Edit[0]["payroll_registry_nbr"]       = lbl_registry_no.Text.ToString().Trim();
                            row2Edit[0]["empl_id"]                    = txtb_empl_id.Text.ToString().Trim();
                            row2Edit[0]["rate_basis"]                 = lbl_rate_basis_hidden.Text.ToString();
                            row2Edit[0]["monthly_rate"]               = txtb_rate_amount.Text.ToString().Trim();
                            row2Edit[0]["daily_rate"]                 = lbl_lastday_daily_rate_hidden.Text.ToString().Trim();
                            row2Edit[0]["pera_amt"]                   = txtb_pera_amount.Text.ToString().Trim();
                            row2Edit[0]["net_pay"]                    = txtb_net_pay.Text.ToString().Trim();
                            row2Edit[0]["net_pay1"]                   = txtb_net_pay_1h.Text.ToString().Trim();
                            row2Edit[0]["net_pay2"]                   = txtb_net_pay_2h.Text.ToString().Trim();
                            row2Edit[0]["lowpay_day"]                 = txtb_no_days_lwopay.Text.ToString().Trim();
                            row2Edit[0]["lowp_amount"]                = txtb_lwo_pay.Text.ToString().Trim();
                            row2Edit[0]["gsis_gs"]                    = txtb_gsis_gs.Text.ToString().Trim();
                            row2Edit[0]["gsis_ps"]                    = txtb_gsis_ps.Text.ToString().Trim();
                            row2Edit[0]["sif_gs"]                     = txtb_gsis_sif.Text.ToString().Trim();
                            row2Edit[0]["hdmf_gs"]                    = txtb_hdmf_gs.Text.ToString().Trim();
                            row2Edit[0]["hdmf_ps"]                    = txtb_hdmf_ps.Text.ToString().Trim();
                            row2Edit[0]["phic_gs"]                    = txtb_phic_gs.Text.ToString().Trim();
                            row2Edit[0]["phic_ps"]                    = txtb_phic_ps.Text.ToString().Trim();
                            row2Edit[0]["wtax"]                       = txtb_bir_tax.Text.ToString().Trim();
                            row2Edit[0]["gsis_uoli"]                  = txtb_gsis_ouli.Text.ToString().Trim();
                            row2Edit[0]["gsis_uoli45"]                = txtb_gsis_ouli45.Text.ToString().Trim();
                            row2Edit[0]["gsis_uoli50"]                = txtb_gsis_ouli50.Text.ToString().Trim();
                            row2Edit[0]["gsis_uoli55"]                = txtb_gsis_ouli55.Text.ToString().Trim();
                            row2Edit[0]["gsis_uoli60"]                = txtb_gsis_ouli60.Text.ToString().Trim();
                            row2Edit[0]["gsis_uoli65"]                = txtb_gsis_ouli65.Text.ToString().Trim();
                            row2Edit[0]["sss_ps"]                     = txtb_sss.Text.ToString().Trim();
                            row2Edit[0]["hdmf_ps2"]                   = txtb_hdmf_addl.Text.ToString().Trim();
                            row2Edit[0]["philamlife_ps"]              = txtb_philam.Text.ToString().Trim();
                            row2Edit[0]["gsis_ehp"]                   = txtb_gsis_ehp.Text.ToString().Trim();
                            row2Edit[0]["gsis_hip"]                   = txtb_gsis_hip.Text.ToString().Trim();
                            row2Edit[0]["gsis_ceap"]                  = txtb_gsis_ceap.Text.ToString().Trim();
                            row2Edit[0]["gsis_addl_ins"]              = txtb_gsis_add.Text.ToString().Trim();
                            row2Edit[0]["other_premium1"]             = txtb_otherpremium_no1.Text.ToString().Trim();
                            row2Edit[0]["other_premium2"]             = txtb_otherpremium_no2.Text.ToString().Trim();
                            row2Edit[0]["other_premium3"]             = txtb_otherpremium_no3.Text.ToString().Trim();
                            row2Edit[0]["other_premium4"]             = txtb_otherpremium_no4.Text.ToString().Trim();
                            row2Edit[0]["other_premium5"]             = txtb_otherpremium_no5.Text.ToString().Trim();
                            row2Edit[0]["gsis_conso_ln"]              = txtb_gsis_consolidated.Text.ToString().Trim();
                            row2Edit[0]["gsis_policy_reg_ln"]         = txtb_gsis_policy_regular.Text.ToString().Trim();
                            row2Edit[0]["gsis_policy_opt_ln"]         = txtb_gsis_policy_optional.Text.ToString().Trim();
                            row2Edit[0]["gsis_uoli_ln"]               = txtb_gsis_ouli_loan.Text.ToString().Trim();
                            row2Edit[0]["gsis_emergency_ln"]          = txtb_gsis_emer_loan.Text.ToString().Trim();
                            row2Edit[0]["gsis_ecard_ln"]              = txtb_gsis_ecard_loan.Text.ToString().Trim();
                            row2Edit[0]["gsis_educ_asst_ln"]          = txtb_gsis_educ_loan.Text.ToString().Trim();
                            row2Edit[0]["gsis_real_state_ln"]         = txtb_gsis_real_loan.Text.ToString().Trim();
                            row2Edit[0]["gsis_sos_ln"]                = txtb_gsis_sos_loan.Text.ToString().Trim();
                            row2Edit[0]["hdmf_mpl_ln"]                = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                            row2Edit[0]["hdmf_hse_ln"]                = txtb_hdmf_house_loan.Text.ToString().Trim();
                            row2Edit[0]["hdmf_cal_ln"]                = txtb_hdmf_cal_loan.Text.ToString().Trim();
                            row2Edit[0]["nico_ln"]                    = txtb_nico_loan.Text.ToString().Trim();
                            row2Edit[0]["network_ln"]                 = txtb_networkbank_loan.Text.ToString().Trim();
                            row2Edit[0]["ccmpc_ln"]                   = txtb_ccmpc_loan.Text.ToString().Trim();
                            row2Edit[0]["other_loan1"]                = txtb_otherloan_no1.Text.ToString().Trim();
                            row2Edit[0]["other_loan2"]                = txtb_otherloan_no2.Text.ToString().Trim();
                            row2Edit[0]["other_loan3"]                = txtb_otherloan_no3.Text.ToString().Trim();
                            row2Edit[0]["other_loan4"]                = txtb_otherloan_no4.Text.ToString().Trim();
                            row2Edit[0]["other_loan5"]                = txtb_otherloan_no5.Text.ToString().Trim();
                            
                            row2Edit[0]["days_worked"]                = txtb_no_days_worked.Text.ToString().Trim();
                            row2Edit[0]["hours_worked"]               = txtb_no_hours_worked.Text.ToString().Trim();
                            row2Edit[0]["gross_pay"]                  = txtb_gross_pay.Text.ToString().Trim();
                            row2Edit[0]["lowp_amount_pera"]           = txtb_lwop_amount_pera.Text.ToString().Trim();

                            // Add Field - 03/12/2019
                            row2Edit[0]["nhmfc_hsing"]                = txtb_nhmfc_hsng.Text.ToString().Trim();
                            row2Edit[0]["nafc_svlf"]                  = txtb_nafc.Text.ToString().Trim();

                            // Add Field Again  - 03/14/2019
                            row2Edit[0]["gsis_help"]                  = txtb_gsis_help.Text.ToString().Trim();
                            row2Edit[0]["gsis_housing_ln"]            = txtb_gsis_housing_loan.Text.ToString().Trim();

                            // Add Field Again  - 03/18/2019
                            row2Edit[0]["hdmf_mp2"]                   = txtb_hdmf_mp2.Text.ToString().Trim();

                            // Add Field Again  - 03/20/2019
                            row2Edit[0]["hdmf_loyalty_card"]          = txtb_hdmf_loyalty_card.Text.ToString().Trim();

                            row2Edit[0]["employee_name"]              = txtb_employeename.Text.ToString().Trim();

                            // Add Field Again  - 05/06/2019
                            row2Edit[0]["lates_mins_hrs"]               = txtb_lates_in_min.Text.ToString().Trim();
                            row2Edit[0]["lates_amount"]                 = txtb_late_in_amount.Text.ToString().Trim();
                            row2Edit[0]["remarks"]                      = txtb_remarks.Text.ToString().Trim();

                            row2Edit[0]["department_code"]          = ddl_dep.SelectedValue.ToString().Trim();
                            row2Edit[0]["subdepartment_code"]       = ddl_subdep.SelectedValue.ToString().Trim();
                            row2Edit[0]["division_code"]            = ddl_division.SelectedValue.ToString().Trim();
                            row2Edit[0]["section_code"]             = ddl_section.SelectedValue.ToString().Trim();
                            row2Edit[0]["fund_code"]                = ddl_fund_charges.SelectedValue.ToString().Trim();

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

                            row2Edit[0]["lates_amount_pera"] = txtb_late_in_amount_pera.Text.ToString().Trim();

                            
                        // Add Field  2022-05-30
                        row2Edit[0]["other_ded_mand1"]       = txtb_other_ded_mand1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand2"]       = txtb_other_ded_mand2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand3"]       = txtb_other_ded_mand3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand4"]       = txtb_other_ded_mand4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand5"]       = txtb_other_ded_mand5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand6"]       = txtb_other_ded_mand6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand7"]       = txtb_other_ded_mand7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand8"]       = txtb_other_ded_mand8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand9"]       = txtb_other_ded_mand9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand10"]      = txtb_other_ded_mand10.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem1"]       = txtb_other_ded_prem1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem2"]       = txtb_other_ded_prem2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem3"]       = txtb_other_ded_prem3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem4"]       = txtb_other_ded_prem4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem5"]       = txtb_other_ded_prem5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem6"]       = txtb_other_ded_prem6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem7"]       = txtb_other_ded_prem7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem8"]       = txtb_other_ded_prem8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem9"]       = txtb_other_ded_prem9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem10"]      = txtb_other_ded_prem10.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan1"]       = txtb_other_ded_loan1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan2"]       = txtb_other_ded_loan2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan3"]       = txtb_other_ded_loan3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan4"]       = txtb_other_ded_loan4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan5"]       = txtb_other_ded_loan5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan6"]       = txtb_other_ded_loan6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan7"]       = txtb_other_ded_loan7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan8"]       = txtb_other_ded_loan8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan9"]       = txtb_other_ded_loan9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan10"]      = txtb_other_ded_loan10.Text.ToString().Trim();
                        
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
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR monthly_rate LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR position_title1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR post_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR net_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("rate_basis", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("daily_rate", typeof(System.String));
            dtSource1.Columns.Add("pera_amt", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay1", typeof(System.String));
            dtSource1.Columns.Add("net_pay2", typeof(System.String));
            dtSource1.Columns.Add("lowpay_day", typeof(System.String));
            dtSource1.Columns.Add("lowp_amount", typeof(System.String));
            dtSource1.Columns.Add("gsis_gs", typeof(System.String));
            dtSource1.Columns.Add("gsis_ps", typeof(System.String));
            dtSource1.Columns.Add("sif_gs", typeof(System.String));
            dtSource1.Columns.Add("hdmf_gs", typeof(System.String));
            dtSource1.Columns.Add("hdmf_ps", typeof(System.String));
            dtSource1.Columns.Add("phic_gs", typeof(System.String));
            dtSource1.Columns.Add("phic_ps", typeof(System.String));
            dtSource1.Columns.Add("wtax", typeof(System.String));
            dtSource1.Columns.Add("gsis_uoli", typeof(System.String));
            dtSource1.Columns.Add("gsis_uoli45", typeof(System.String));
            dtSource1.Columns.Add("gsis_uoli50", typeof(System.String));
            dtSource1.Columns.Add("gsis_uoli55", typeof(System.String));
            dtSource1.Columns.Add("gsis_uoli60", typeof(System.String));
            dtSource1.Columns.Add("gsis_uoli65", typeof(System.String));
            dtSource1.Columns.Add("sss_ps", typeof(System.String));
            dtSource1.Columns.Add("hdmf_ps2", typeof(System.String));
            dtSource1.Columns.Add("philamlife_ps", typeof(System.String));
            dtSource1.Columns.Add("gsis_ehp", typeof(System.String));
            dtSource1.Columns.Add("gsis_hip", typeof(System.String));
            dtSource1.Columns.Add("gsis_ceap", typeof(System.String));
            dtSource1.Columns.Add("gsis_addl_ins", typeof(System.String));
            dtSource1.Columns.Add("other_premium1", typeof(System.String));
            dtSource1.Columns.Add("other_premium2", typeof(System.String));
            dtSource1.Columns.Add("other_premium3", typeof(System.String));
            dtSource1.Columns.Add("other_premium4", typeof(System.String));
            dtSource1.Columns.Add("other_premium5", typeof(System.String));
            dtSource1.Columns.Add("gsis_conso_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_policy_reg_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_policy_opt_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_uoli_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_emergency_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_ecard_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_educ_asst_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_real_state_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_sos_ln", typeof(System.String));
            dtSource1.Columns.Add("hdmf_mpl_ln", typeof(System.String));
            dtSource1.Columns.Add("hdmf_hse_ln", typeof(System.String));
            dtSource1.Columns.Add("hdmf_cal_ln", typeof(System.String));
            dtSource1.Columns.Add("nico_ln", typeof(System.String));
            dtSource1.Columns.Add("network_ln", typeof(System.String));
            dtSource1.Columns.Add("ccmpc_ln", typeof(System.String));
            dtSource1.Columns.Add("other_loan1", typeof(System.String));
            dtSource1.Columns.Add("other_loan2", typeof(System.String));
            dtSource1.Columns.Add("other_loan3", typeof(System.String));
            dtSource1.Columns.Add("other_loan4", typeof(System.String));
            dtSource1.Columns.Add("other_loan5", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("days_worked", typeof(System.String));
            dtSource1.Columns.Add("hours_worked", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("lowp_amount_pera", typeof(System.String));
            dtSource1.Columns.Add("nhmfc_hsing", typeof(System.String));
            dtSource1.Columns.Add("nafc_svlf", typeof(System.String));
            dtSource1.Columns.Add("gsis_help", typeof(System.String));
            dtSource1.Columns.Add("gsis_housing_ln", typeof(System.String));
            dtSource1.Columns.Add("hdmf_mp2", typeof(System.String));
            dtSource1.Columns.Add("hdmf_loyalty_card", typeof(System.String));

            dtSource1.Columns.Add("remarks", typeof(System.String));
            dtSource1.Columns.Add("lates_mins_hrs", typeof(System.String));
            dtSource1.Columns.Add("lates_amount", typeof(System.String));


            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("lates_amount_pera", typeof(System.String));
           
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
        //  BEGIN - VJA- 04/06/2019 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 04/06/2019 - Check if Object already contains value  
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
        //  BEGIN - VJA- 04/06/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            FieldValidationColorChanged(false, "ALL");
            bool validatedSaved = true;

            int target_tab = 1;

            if (ddl_empl_id.SelectedValue == "" && lbl_addeditmode_hidden.Text == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_no_days_lwopay) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_lwopay");
                txtb_no_days_lwopay.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_no_days_worked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_worked");
                txtb_no_days_worked.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisnumber(txtb_lates_in_min) == false)
            {
                FieldValidationColorChanged(true, "txtb_lates_in_min");
                txtb_lates_in_min.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_no_hours_worked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_hours_worked");
                txtb_no_hours_worked.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_pera_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_pera_amount");
                txtb_pera_amount.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_gross_pay");
                txtb_gross_pay.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_lwo_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_lwo_pay");
                txtb_lwo_pay.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_lwop_amount_pera) == false)
            {
                FieldValidationColorChanged(true, "txtb_lwop_amount_pera");
                txtb_lwop_amount_pera.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_total_mandatory) == false)
            {
                FieldValidationColorChanged(true, "txtb_total_mandatory");
                txtb_total_mandatory.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_total_optional) == false)
            {
                FieldValidationColorChanged(true, "txtb_total_optional");
                txtb_total_optional.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_total_loans) == false)
            {
                FieldValidationColorChanged(true, "txtb_total_loans");
                txtb_total_loans.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_net_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_pay");
                txtb_net_pay.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_net_pay_1h) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_pay_1h");
                txtb_net_pay_1h.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_net_pay_2h) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_pay_2h");
                txtb_net_pay_2h.Focus();
                validatedSaved = false;
            }
            //Mandatory
            if (CommonCode.checkisdecimal(txtb_gsis_gs) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_gs");
                txtb_gsis_gs.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ps) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ps");
                txtb_gsis_ps.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_sif) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_sif");
                txtb_gsis_sif.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_gs) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_gs");
                txtb_hdmf_gs.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_ps) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_ps");
                txtb_hdmf_ps.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_phic_gs) == false)
            {
                FieldValidationColorChanged(true, "txtb_phic_gs");
                txtb_phic_gs.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_phic_ps) == false)
            {
                FieldValidationColorChanged(true, "txtb_phic_ps");
                txtb_phic_ps.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_bir_tax) == false)
            {
                FieldValidationColorChanged(true, "txtb_bir_tax");
                txtb_bir_tax.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            //OPTIONAL DEDUCTIONS

            if (CommonCode.checkisdecimal(txtb_gsis_ouli) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ouli");
                txtb_gsis_ouli.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ouli45) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ouli45");
                txtb_gsis_ouli45.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ouli50) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ouli50");
                txtb_gsis_ouli50.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ouli55) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ouli55");
                txtb_gsis_ouli55.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ouli60) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ouli60");
                txtb_gsis_ouli60.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ouli65) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ouli65");
                txtb_gsis_ouli65.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_sss) == false)
            {
                FieldValidationColorChanged(true, "txtb_sss");
                txtb_sss.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_addl) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_addl");
                txtb_hdmf_addl.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_philam) == false)
            {
                FieldValidationColorChanged(true, "txtb_philam");
                txtb_philam.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ehp) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ehp");
                txtb_gsis_ehp.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_hip) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_hip");
                txtb_gsis_hip.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ceap) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ceap");
                txtb_gsis_ceap.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_add) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_add");
                txtb_gsis_add.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no1) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no1");
                txtb_otherpremium_no1.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no2) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no2");
                txtb_otherpremium_no2.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no3) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no3");
                txtb_otherpremium_no3.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no4) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no4");
                txtb_otherpremium_no4.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no5) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no5");
                txtb_otherpremium_no5.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_mp2) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_mp2");
                txtb_hdmf_mp2.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_loyalty_card) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_loyalty_card");
                txtb_hdmf_loyalty_card.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            //LOANS
            if (CommonCode.checkisdecimal(txtb_gsis_consolidated) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_consolidated");
                txtb_gsis_consolidated.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_policy_regular) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_policy_regular");
                txtb_gsis_policy_regular.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_policy_optional) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_policy_optional");
                txtb_gsis_policy_optional.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ouli_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ouli_loan");
                txtb_gsis_ouli_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_emer_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_emer_loan");
                txtb_gsis_emer_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ecard_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ecard_loan");
                txtb_gsis_ecard_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_educ_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_educ_loan");
                txtb_gsis_educ_loan.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_real_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_real_loan");
                txtb_gsis_real_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_sos_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_sos_loan");
                txtb_gsis_sos_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_mpl_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_mpl_loan");
                txtb_hdmf_mpl_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_house_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_house_loan");
                txtb_hdmf_house_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_cal_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_cal_loan");
                txtb_hdmf_cal_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_ccmpc_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_ccmpc_loan");
                txtb_ccmpc_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_nico_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_nico_loan");
                txtb_nico_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_networkbank_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_networkbank_loan");
                txtb_networkbank_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no1) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no1");
                txtb_otherloan_no1.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no2) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no2");
                txtb_otherloan_no2.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no3) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no3");
                txtb_otherloan_no3.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no4) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no4");
                txtb_otherloan_no4.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no5) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no5");
                txtb_otherloan_no5.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_nhmfc_hsng) == false)
            {
                FieldValidationColorChanged(true, "txtb_nhmfc_hsng");
                txtb_nhmfc_hsng.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_nafc) == false)
            {
                FieldValidationColorChanged(true, "txtb_nafc");
                txtb_nafc.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_help) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_help");
                txtb_gsis_help.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_housing_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_housing_loan");
                txtb_gsis_housing_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            // Required ang Vouvher number if Status is Y or Blank ug ang Post Authority is equal to 1 
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }

            // MANDATORY VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_mand1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand1");  txtb_other_ded_mand1.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand2");  txtb_other_ded_mand2.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand3");  txtb_other_ded_mand3.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand4");  txtb_other_ded_mand4.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand5");  txtb_other_ded_mand5.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand6");  txtb_other_ded_mand6.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand7");  txtb_other_ded_mand7.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand8");  txtb_other_ded_mand8.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand9");  txtb_other_ded_mand9.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_mand10"); txtb_other_ded_mand10.Focus() ; validatedSaved  = false; target_tab = 1; }

            // OPTIONAL VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_prem1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem1");  txtb_other_ded_prem1.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem2");  txtb_other_ded_prem2.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem3");  txtb_other_ded_prem3.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem4");  txtb_other_ded_prem4.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem5");  txtb_other_ded_prem5.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem6");  txtb_other_ded_prem6.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem7");  txtb_other_ded_prem7.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem8");  txtb_other_ded_prem8.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem9");  txtb_other_ded_prem9.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_prem10"); txtb_other_ded_prem10.Focus() ; validatedSaved  = false; target_tab = 2; }
             
            // LOAN VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_loan1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan1");  txtb_other_ded_loan1.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan2");  txtb_other_ded_loan2.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan3");  txtb_other_ded_loan3.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan4");  txtb_other_ded_loan4.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan5");  txtb_other_ded_loan5.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan6");  txtb_other_ded_loan6.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan7");  txtb_other_ded_loan7.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan8");  txtb_other_ded_loan8.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan9");  txtb_other_ded_loan9.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_loan10"); txtb_other_ded_loan10.Focus() ; validatedSaved  = false; target_tab = 3; }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClickTab", "click_tab("+ target_tab + ")", true);
            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_empl_id":
                        {
                            LblRequired60.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_days_lwopay":
                        {
                            LblRequired1.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_days_lwopay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_days_worked":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_days_worked.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_lates_in_min":
                        {
                            LblRequired100.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lates_in_min.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_hours_worked":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_hours_worked.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gross_pay":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_pera_amount":
                        {
                            LblRequired61.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_pera_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_lwo_pay":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lwo_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_lwop_amount_pera":
                        {
                            LblRequired600.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lwop_amount_pera.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_total_mandatory":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_mandatory.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_total_optional":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_optional.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_total_loans":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_loans.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_pay":
                        {
                            LblRequired10.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_pay_1h":
                        {
                            LblRequired11.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay_1h.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_pay_2h":
                        {
                            LblRequired12.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay_2h.BorderColor = Color.Red;
                            break;
                        }
                        //Mandatory
                    case "txtb_gsis_gs":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_gs.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_ps":
                        {
                            LblRequired15.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ps.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_sif":
                        {
                            LblRequired16.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_sif.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_gs":
                        {
                            LblRequired17.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_gs.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_ps":
                        {
                            LblRequired18.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_ps.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_phic_gs":
                        {
                            LblRequired19.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_phic_gs.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_phic_ps":
                        {
                            LblRequired20.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_phic_ps.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax":
                        {
                            LblRequired21.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax.BorderColor = Color.Red;
                            break;
                        }

                        //OPTIONAL DEDUCTIONS
                    case "txtb_gsis_ouli":
                        {
                            LblRequired22.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ouli.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_ouli45":
                        {
                            LblRequired23.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ouli45.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_ouli50":
                        {
                            LblRequired24.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ouli50.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_ouli55":
                        {
                            LblRequired25.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ouli55.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_ouli60":
                        {
                            LblRequired26.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ouli60.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_ouli65":
                        {
                            LblRequired27.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ouli65.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_sss":
                        {
                            LblRequired28.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_sss.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_addl":
                        {
                            LblRequired29.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_addl.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_philam":
                        {
                            LblRequired30.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_philam.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_ehp":
                        {
                            LblRequired31.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ehp.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_hip":
                        {
                            LblRequired32.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_hip.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_ceap":
                        {
                            LblRequired33.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ceap.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_add":
                        {
                            LblRequired34.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_add.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherpremium_no1":
                        {
                            LblRequired35.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no1.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherpremium_no2":
                        {
                            LblRequired36.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no2.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherpremium_no3":
                        {
                            LblRequired37.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no3.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherpremium_no4":
                        {
                            LblRequired38.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no4.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherpremium_no5":
                        {
                            LblRequired39.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no5.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_hdmf_mp2":
                        {
                            LblRequired66.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_mp2.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_hdmf_loyalty_card":
                        {
                            LblRequired67.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_loyalty_card.BorderColor = Color.Red;
                            
                            break;
                        }

                    //LOANS
                    case "txtb_gsis_consolidated":
                        {
                            LblRequired40.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_consolidated.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_policy_regular":
                        {
                            LblRequired41.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_policy_regular.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_policy_optional":
                        {
                            LblRequired42.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_policy_optional.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_ouli_loan":
                        {
                            LblRequired43.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ouli_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_emer_loan":
                        {
                            LblRequired44.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_emer_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_ecard_loan":
                        {
                            LblRequired45.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ecard_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_educ_loan":
                        {
                            LblRequired46.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_educ_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_real_loan":
                        {
                            LblRequired47.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_real_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_gsis_sos_loan":
                        {
                            LblRequired48.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_sos_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_hdmf_mpl_loan":
                        {
                            LblRequired49.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_mpl_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_hdmf_house_loan":
                        {
                            LblRequired50.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_house_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_hdmf_cal_loan":
                        {
                            LblRequired51.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_cal_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_ccmpc_loan":
                        {
                            LblRequired52.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_ccmpc_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_nico_loan":
                        {
                            LblRequired53.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nico_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_networkbank_loan":
                        {
                            LblRequired54.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_networkbank_loan.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherloan_no1":
                        {
                            LblRequired55.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no1.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherloan_no2":
                        {
                            LblRequired56.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no2.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherloan_no3":
                        {
                            LblRequired57.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no3.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherloan_no4":
                        {
                            LblRequired58.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no4.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "txtb_otherloan_no5":
                        {
                            LblRequired59.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no5.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "below-minumum-net":
                        {
                            LblRequired10.Text = "Net Pay is Below Minimum! ";
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired60.Text = "already exist!";
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_nhmfc_hsng":
                        {
                            LblRequired62.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nhmfc_hsng.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_nafc":
                        {
                            LblRequired63.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nafc.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_help":
                        {
                            LblRequired64.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_help.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_housing_loan":
                        {
                            LblRequired65.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_housing_loan.BorderColor = Color.Red;
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

                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_mand1":  { req_other_ded_mand1.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand1.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand2":  { req_other_ded_mand2.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand2.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand3":  { req_other_ded_mand3.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand3.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand4":  { req_other_ded_mand4.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand4.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand5":  { req_other_ded_mand5.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand5.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand6":  { req_other_ded_mand6.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand6.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand7":  { req_other_ded_mand7.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand7.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand8":  { req_other_ded_mand8.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand8.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand9":  { req_other_ded_mand9.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand9.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand10": { req_other_ded_mand10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand10.BorderColor = Color.Red; break;}

                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_prem1":  { req_other_ded_prem1.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem1.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem2":  { req_other_ded_prem2.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem2.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem3":  { req_other_ded_prem3.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem3.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem4":  { req_other_ded_prem4.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem4.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem5":  { req_other_ded_prem5.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem5.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem6":  { req_other_ded_prem6.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem6.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem7":  { req_other_ded_prem7.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem7.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem8":  { req_other_ded_prem8.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem8.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem9":  { req_other_ded_prem9.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem9.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem10": { req_other_ded_prem10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem10.BorderColor = Color.Red; break;}

                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_loan1":  { req_other_ded_loan1.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan1.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan2":  { req_other_ded_loan2.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan2.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan3":  { req_other_ded_loan3.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan3.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan4":  { req_other_ded_loan4.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan4.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan5":  { req_other_ded_loan5.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan5.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan6":  { req_other_ded_loan6.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan6.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan7":  { req_other_ded_loan7.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan7.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan8":  { req_other_ded_loan8.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan8.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan9":  { req_other_ded_loan9.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan9.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan10": { req_other_ded_loan10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan10.BorderColor = Color.Red; break;}
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
                            LblRequired6.Text = "";
                            LblRequired7.Text = "";
                            LblRequired8.Text = "";
                            LblRequired9.Text = "";
                            LblRequired10.Text = "";
                            LblRequired11.Text = "";
                            LblRequired12.Text = "";
                            LblRequired13.Text = "";
                            LblRequired15.Text = "";
                            LblRequired16.Text = "";
                            LblRequired17.Text = "";
                            LblRequired18.Text = "";
                            LblRequired19.Text = "";
                            LblRequired20.Text = "";
                            LblRequired21.Text = "";
                            LblRequired22.Text = "";
                            LblRequired23.Text = "";
                            LblRequired24.Text = "";
                            LblRequired25.Text = "";
                            LblRequired26.Text = "";
                            LblRequired27.Text = "";
                            LblRequired28.Text = "";
                            LblRequired29.Text = "";
                            LblRequired30.Text = "";
                            LblRequired31.Text = "";
                            LblRequired32.Text = "";
                            LblRequired33.Text = "";
                            LblRequired34.Text = "";
                            LblRequired35.Text = "";
                            LblRequired36.Text = "";
                            LblRequired37.Text = "";
                            LblRequired38.Text = "";
                            LblRequired39.Text = "";
                            LblRequired40.Text = "";
                            LblRequired41.Text = "";
                            LblRequired42.Text = "";
                            LblRequired43.Text = "";
                            LblRequired44.Text = "";
                            LblRequired45.Text = "";
                            LblRequired46.Text = "";
                            LblRequired47.Text = "";
                            LblRequired48.Text = "";
                            LblRequired49.Text = "";
                            LblRequired50.Text = "";
                            LblRequired51.Text = "";
                            LblRequired52.Text = "";
                            LblRequired53.Text = "";
                            LblRequired54.Text = "";
                            LblRequired55.Text = "";
                            LblRequired56.Text = "";
                            LblRequired57.Text = "";
                            LblRequired58.Text = "";
                            LblRequired59.Text = "";
                            LblRequired60.Text = "";
                            LblRequired61.Text = "";
                            LblRequired62.Text = "";
                            LblRequired63.Text = "";
                            LblRequired64.Text = "";
                            LblRequired65.Text = "";
                            LblRequired66.Text = "";
                            LblRequired67.Text = "";
                            LblRequired100.Text = "";
                            LblRequired200.Text = "";
                            LblRequired201.Text = "";

                            txtb_no_days_lwopay.BorderColor = Color.LightGray;
                            txtb_no_days_worked.BorderColor = Color.LightGray;
                            txtb_no_hours_worked.BorderColor = Color.LightGray;
                            txtb_gross_pay.BorderColor = Color.LightGray;
                            txtb_pera_amount.BorderColor = Color.LightGray;
                            txtb_lwo_pay.BorderColor = Color.LightGray;
                            txtb_total_mandatory.BorderColor = Color.LightGray;
                            txtb_total_optional.BorderColor = Color.LightGray;
                            txtb_total_loans.BorderColor = Color.LightGray;
                            txtb_net_pay.BorderColor = Color.LightGray;
                            txtb_net_pay_1h.BorderColor = Color.LightGray;
                            txtb_net_pay_2h.BorderColor = Color.LightGray;
                            txtb_gsis_gs.BorderColor = Color.LightGray;
                            txtb_gsis_ps.BorderColor = Color.LightGray;
                            txtb_gsis_sif.BorderColor = Color.LightGray;
                            txtb_hdmf_gs.BorderColor = Color.LightGray;
                            txtb_hdmf_ps.BorderColor = Color.LightGray;
                            txtb_phic_gs.BorderColor = Color.LightGray;
                            txtb_phic_ps.BorderColor = Color.LightGray;
                            txtb_bir_tax.BorderColor = Color.LightGray;

                            txtb_gsis_ouli.BorderColor = Color.LightGray;
                            txtb_gsis_ouli45.BorderColor = Color.LightGray;
                            txtb_gsis_ouli50.BorderColor = Color.LightGray;
                            txtb_gsis_ouli55.BorderColor = Color.LightGray;
                            txtb_gsis_ouli60.BorderColor = Color.LightGray;
                            txtb_gsis_ouli65.BorderColor = Color.LightGray;
                            txtb_sss.BorderColor = Color.LightGray;
                            txtb_hdmf_addl.BorderColor = Color.LightGray;
                            txtb_philam.BorderColor = Color.LightGray;
                            txtb_gsis_ehp.BorderColor = Color.LightGray;
                            txtb_gsis_hip.BorderColor = Color.LightGray;
                            txtb_gsis_ceap.BorderColor = Color.LightGray;
                            txtb_gsis_add.BorderColor = Color.LightGray;
                            txtb_otherpremium_no1.BorderColor = Color.LightGray;
                            txtb_otherpremium_no2.BorderColor = Color.LightGray;
                            txtb_otherpremium_no3.BorderColor = Color.LightGray;
                            txtb_otherpremium_no4.BorderColor = Color.LightGray;
                            txtb_otherpremium_no5.BorderColor = Color.LightGray;
                            txtb_hdmf_mp2.BorderColor = Color.LightGray;

                            txtb_gsis_consolidated.BorderColor = Color.LightGray;
                            txtb_gsis_policy_regular.BorderColor = Color.LightGray;
                            txtb_gsis_policy_optional.BorderColor = Color.LightGray;
                            txtb_gsis_ouli_loan.BorderColor = Color.LightGray;
                            txtb_gsis_emer_loan.BorderColor = Color.LightGray;
                            txtb_gsis_ecard_loan.BorderColor = Color.LightGray;
                            txtb_gsis_educ_loan.BorderColor = Color.LightGray;
                            txtb_gsis_real_loan.BorderColor = Color.LightGray;
                            txtb_gsis_sos_loan.BorderColor = Color.LightGray;
                            txtb_hdmf_mpl_loan.BorderColor = Color.LightGray;
                            txtb_hdmf_house_loan.BorderColor = Color.LightGray;
                            txtb_hdmf_cal_loan.BorderColor = Color.LightGray;
                            txtb_ccmpc_loan.BorderColor = Color.LightGray;
                            txtb_nico_loan.BorderColor = Color.LightGray;
                            txtb_networkbank_loan.BorderColor = Color.LightGray;
                            txtb_otherloan_no1.BorderColor = Color.LightGray;
                            txtb_otherloan_no2.BorderColor = Color.LightGray;
                            txtb_otherloan_no3.BorderColor = Color.LightGray;
                            txtb_otherloan_no4.BorderColor = Color.LightGray;
                            txtb_otherloan_no5.BorderColor = Color.LightGray;
                            txtb_nhmfc_hsng.BorderColor = Color.LightGray;
                            txtb_nafc.BorderColor = Color.LightGray;
                            txtb_gsis_help.BorderColor = Color.LightGray;
                            txtb_gsis_housing_loan.BorderColor = Color.LightGray;
                            txtb_hdmf_loyalty_card.BorderColor = Color.LightGray;

                            ddl_empl_id.BorderColor = Color.LightGray;
                            txtb_lates_in_min.BorderColor = Color.LightGray;
                            txtb_voucher_nbr.BorderColor = Color.LightGray;
                            txtb_reason.BorderColor = Color.LightGray;

                            req_other_ded_mand1.Text = "";
                            req_other_ded_mand2.Text = "";
                            req_other_ded_mand3.Text = "";
                            req_other_ded_mand4.Text = "";
                            req_other_ded_mand5.Text = "";
                            req_other_ded_mand6.Text = "";
                            req_other_ded_mand7.Text = "";
                            req_other_ded_mand8.Text = "";
                            req_other_ded_mand9.Text = "";
                            req_other_ded_mand10.Text = "";

                            req_other_ded_prem1.Text = "";
                            req_other_ded_prem2.Text = "";
                            req_other_ded_prem3.Text = "";
                            req_other_ded_prem4.Text = "";
                            req_other_ded_prem5.Text = "";
                            req_other_ded_prem6.Text = "";
                            req_other_ded_prem7.Text = "";
                            req_other_ded_prem8.Text = "";
                            req_other_ded_prem9.Text = "";
                            req_other_ded_prem10.Text = "";

                            req_other_ded_loan1.Text = "";
                            req_other_ded_loan2.Text = "";
                            req_other_ded_loan3.Text = "";
                            req_other_ded_loan4.Text = "";
                            req_other_ded_loan5.Text = "";
                            req_other_ded_loan6.Text = "";
                            req_other_ded_loan7.Text = "";
                            req_other_ded_loan8.Text = "";
                            req_other_ded_loan9.Text = "";
                            req_other_ded_loan10.Text = "";

                            txtb_other_ded_mand1.BorderColor = Color.LightGray;
                            txtb_other_ded_mand2.BorderColor = Color.LightGray;
                            txtb_other_ded_mand3.BorderColor = Color.LightGray;
                            txtb_other_ded_mand4.BorderColor = Color.LightGray;
                            txtb_other_ded_mand5.BorderColor = Color.LightGray;
                            txtb_other_ded_mand6.BorderColor = Color.LightGray;
                            txtb_other_ded_mand7.BorderColor = Color.LightGray;
                            txtb_other_ded_mand8.BorderColor = Color.LightGray;
                            txtb_other_ded_mand9.BorderColor = Color.LightGray;
                            txtb_other_ded_mand10.BorderColor = Color.LightGray;

                            txtb_other_ded_prem1.BorderColor = Color.LightGray;
                            txtb_other_ded_prem2.BorderColor = Color.LightGray;
                            txtb_other_ded_prem3.BorderColor = Color.LightGray;
                            txtb_other_ded_prem4.BorderColor = Color.LightGray;
                            txtb_other_ded_prem5.BorderColor = Color.LightGray;
                            txtb_other_ded_prem6.BorderColor = Color.LightGray;
                            txtb_other_ded_prem7.BorderColor = Color.LightGray;
                            txtb_other_ded_prem8.BorderColor = Color.LightGray;
                            txtb_other_ded_prem9.BorderColor = Color.LightGray;
                            txtb_other_ded_prem10.BorderColor = Color.LightGray;

                            txtb_other_ded_loan1.BorderColor = Color.LightGray;
                            txtb_other_ded_loan2.BorderColor = Color.LightGray;
                            txtb_other_ded_loan3.BorderColor = Color.LightGray;
                            txtb_other_ded_loan4.BorderColor = Color.LightGray;
                            txtb_other_ded_loan5.BorderColor = Color.LightGray;
                            txtb_other_ded_loan6.BorderColor = Color.LightGray;
                            txtb_other_ded_loan7.BorderColor = Color.LightGray;
                            txtb_other_ded_loan8.BorderColor = Color.LightGray;
                            txtb_other_ded_loan9.BorderColor = Color.LightGray;
                            txtb_other_ded_loan10.BorderColor = Color.LightGray;
                            break;
                        }
                }
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Employmebt Type
        //**************************************************************************
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
            RetriveGroupings();
            RetriveTemplate();
            RetrieveEmployeename();
            UpdatePanel10.Update();
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Department
        //**************************************************************************
        protected void ddl_dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_dep.SelectedValue != "" )
            {
                RetrieveBindingSubDep();
                RetrieveBindingDivision();
                RetrieveBindingSection();
                RetrieveEmployeename();
                
            }
            else
            {
                FieldValidationColorChanged(true, "ddl_dep");
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Sub-Department
        //**************************************************************************
        protected void ddl_subdep_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveEmployeename();
            ddl_subdep.Focus();
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Division
        //**************************************************************************
        protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingSection();
            RetrieveEmployeename();
            ddl_division.Focus();
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Section
        //**************************************************************************
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveEmployeename();
        }
        
        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ddl_dep.SelectedValue = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Payroll Year
        //**************************************************************************
        protected void ddl_year_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "")
            {
                RetrieveYear();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Payroll Group
        //**************************************************************************
        protected void ddl_payroll_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            RetrieveEmployeename();
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Payroll Template
        //**************************************************************************
        protected void ddl_payroll_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Payroll Month
        //**************************************************************************
        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Select Employe Name
        //**************************************************************************
        protected void ddl_empl_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddl_empl_id.SelectedValue != "")
            {

                // *************************************************************************************************************
                // ******* BEGIN : 2022-09-23 - Check the Payroll Validations **************************************************
                // *************************************************************************************************************
                DataTable dt = MyCmn.RetrieveData("sp_chk_payroll", "par_transmittal_nbr", "", "par_payroll_registry_nbr", lbl_registry_no.Text.ToString().Trim(), "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_empl_id", ddl_empl_id.SelectedValue.ToString().Trim());
                if (dt.Rows.Count > 0)
                {
                    msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                    msg_header.InnerText = "YOU SELECT " + dt.Rows[0]["employee_name"].ToString().Trim();
                    var lbl_descr = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lbl_descr += "*" + dt.Rows[i]["validation_msg"].ToString() + "<br>";
                    }
                    lbl_details.Text = lbl_descr;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop6", "openNotification();", true);
                }
                // *************************************************************************************************************
                // ******* BEGIN : 2022-09-23 - Check the Payroll Validations **************************************************
                // *************************************************************************************************************

                HeaderDetails_Initialized_Add();
                calculate_salary_summary_tab();
                calculate_pera_summary_tab();
                CalculateManExemption();
                calculate_mandatory();
                calculate_optional();
                calculate_loans();
                calculate_gross_regular();
                calculate_netpays();
            }
            else
            {
                ClearEntry();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Initialized Value During Add
        //**************************************************************************
        private void HeaderDetails_Initialized_Add()
        {
            string editExpression = "empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'";
            DataRow[] row2Edit2 = dtSource_for_names.Select("empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

            txtb_empl_id.Text = ddl_empl_id.SelectedValue.ToString().Trim();

            if (row2Edit2[0]["department_code"].ToString() != string.Empty)
            {
                ddl_dep.SelectedValue = row2Edit2[0]["department_code"].ToString();
            }
            if (row2Edit2[0]["subdepartment_code"].ToString() != string.Empty)
            {
                ddl_subdep.SelectedValue = row2Edit2[0]["subdepartment_code"].ToString();
            }
            else
            {
                ddl_subdep.SelectedIndex = -1;
            }
            RetrieveBindingDivision();

            if (row2Edit2[0]["division_code"].ToString() != string.Empty && row2Edit2[0]["division_code"].ToString() != "")
            {
                ddl_division.SelectedValue = row2Edit2[0]["division_code"].ToString();
            }
            else
            {
                ddl_division.SelectedIndex = -1;
            }
            RetrieveBindingSection();

            if (row2Edit2[0]["section_code"].ToString() != string.Empty)
            {
                ddl_section.SelectedValue = row2Edit2[0]["section_code"].ToString();
            }

            ddl_fund_charges.SelectedValue = row2Edit2[0]["fund_code"].ToString();
            
            if (lbl_addeditmode_hidden.Text == MyCmn.CONST_ADD)
            {
                txtb_no_days_worked.Text = row2Edit2[0]["time_days_equi"].ToString();
                txtb_no_hours_worked.Text = row2Edit2[0]["time_hours_equi"].ToString();
            }
            
            lbl_rate_basis_hidden.Text                  = row2Edit2[0]["rate_basis"].ToString();
            txtb_rate_amount.Text                       = row2Edit2[0]["monthly_rate"].ToString();
            lbl_lastday_daily_rate_hidden.Text          = row2Edit2[0]["daily_rate"].ToString();
            lbl_lastday_hourly_rate_hidden.Text         = row2Edit2[0]["hourly_rate"].ToString();
            lbl_rate_descr.Text                         = row2Edit2[0]["rate_basis_descr"].ToString() + " Rate :";
            
            
            switch (row2Edit2[0]["rate_basis"].ToString())
            {
                case "M":
                    {
                        txtb_rate_amount.Text = row2Edit2[0]["monthly_rate"].ToString();
                        //calculate_gross_regular();
                        break;
                    }
                case "D":
                    {
                        txtb_rate_amount.Text = row2Edit2[0]["daily_rate"].ToString();
                        break;
                    }
                case "H":
                    {
                        txtb_rate_amount.Text = row2Edit2[0]["hourly_rate"].ToString();
                        break;
                    }
            }
            
            // UPDATE - VJA : 05/22/2019 - Data Will Coming From Employee Master

            txtb_pera_amount.Text           = row2Edit2[0]["pera_amt"].ToString();
            txtb_gsis_gs.Text               = row2Edit2[0]["gsis_gs"].ToString();
            txtb_gsis_ps.Text               = row2Edit2[0]["gsis_ps"].ToString();
            txtb_gsis_sif.Text              = row2Edit2[0]["sif_gs"].ToString();
            txtb_hdmf_gs.Text               = row2Edit2[0]["hdmf_gs"].ToString();
            txtb_hdmf_ps.Text               = row2Edit2[0]["hdmf_ps"].ToString();
            txtb_phic_gs.Text               = row2Edit2[0]["phic_gs"].ToString();
            txtb_phic_ps.Text               = row2Edit2[0]["phic_ps"].ToString();
            txtb_bir_tax.Text               = row2Edit2[0]["wtax"].ToString();
                                                      
            txtb_gsis_ouli.Text             = row2Edit2[0]["gsis_uoli"].ToString();
            txtb_gsis_ouli45.Text           = row2Edit2[0]["gsis_uoli45"].ToString();
            txtb_gsis_ouli50.Text           = row2Edit2[0]["gsis_uoli50"].ToString();
            txtb_gsis_ouli55.Text           = row2Edit2[0]["gsis_uoli55"].ToString();
            txtb_gsis_ouli60.Text           = row2Edit2[0]["gsis_uoli60"].ToString();
            txtb_gsis_ouli65.Text           = row2Edit2[0]["gsis_uoli65"].ToString();
            txtb_sss.Text                   = row2Edit2[0]["sss_ps"].ToString();
            txtb_hdmf_addl.Text             = row2Edit2[0]["hdmf_ps2"].ToString();
            txtb_philam.Text                = row2Edit2[0]["philamlife_ps"].ToString();
            txtb_gsis_ehp.Text              = row2Edit2[0]["gsis_ehp"].ToString();
            txtb_gsis_hip.Text              = row2Edit2[0]["gsis_hip"].ToString();
            txtb_gsis_ceap.Text             = row2Edit2[0]["gsis_ceap"].ToString();
            txtb_gsis_add.Text              = row2Edit2[0]["gsis_addl_ins"].ToString();
            txtb_otherpremium_no1.Text      = row2Edit2[0]["other_premium1"].ToString();
            txtb_otherpremium_no2.Text      = row2Edit2[0]["other_premium2"].ToString();
            txtb_otherpremium_no3.Text      = row2Edit2[0]["other_premium3"].ToString();
            txtb_otherpremium_no4.Text      = row2Edit2[0]["other_premium4"].ToString();
            txtb_otherpremium_no5.Text      = row2Edit2[0]["other_premium5"].ToString();
            txtb_gsis_consolidated.Text     = row2Edit2[0]["gsis_conso_ln"].ToString();
            txtb_gsis_policy_regular.Text   = row2Edit2[0]["gsis_policy_reg_ln"].ToString();
            txtb_gsis_policy_optional.Text  = row2Edit2[0]["gsis_policy_opt_ln"].ToString();
            txtb_gsis_ouli_loan.Text        = row2Edit2[0]["gsis_uoli_ln"].ToString();
            txtb_gsis_emer_loan.Text        = row2Edit2[0]["gsis_emergency_ln"].ToString();
            txtb_gsis_ecard_loan.Text       = row2Edit2[0]["gsis_ecard_ln"].ToString();
            txtb_gsis_educ_loan.Text        = row2Edit2[0]["gsis_educ_asst_ln"].ToString();
            txtb_gsis_real_loan.Text        = row2Edit2[0]["gsis_real_state_ln"].ToString();
            txtb_gsis_sos_loan.Text         = row2Edit2[0]["gsis_sos_ln"].ToString();
            txtb_hdmf_mpl_loan.Text         = row2Edit2[0]["hdmf_mpl_ln"].ToString();
            txtb_hdmf_house_loan.Text       = row2Edit2[0]["hdmf_hse_ln"].ToString();
            txtb_hdmf_cal_loan.Text         = row2Edit2[0]["hdmf_cal_ln"].ToString();
            txtb_nico_loan.Text             = row2Edit2[0]["nico_ln"].ToString();
            txtb_networkbank_loan.Text      = row2Edit2[0]["network_ln"].ToString();
            txtb_ccmpc_loan.Text            = row2Edit2[0]["ccmpc_ln"].ToString();
            txtb_otherloan_no1.Text         = row2Edit2[0]["other_loan1"].ToString();
            txtb_otherloan_no2.Text         = row2Edit2[0]["other_loan2"].ToString();
            txtb_otherloan_no3.Text         = row2Edit2[0]["other_loan3"].ToString();
            txtb_otherloan_no4.Text         = row2Edit2[0]["other_loan4"].ToString();
            txtb_otherloan_no5.Text         = row2Edit2[0]["other_loan5"].ToString();
            
            txtb_nhmfc_hsng.Text            = row2Edit2[0]["nhmfc_hsing"].ToString();
            txtb_nafc.Text                  = row2Edit2[0]["nafc_svlf"].ToString();
            txtb_gsis_help.Text             = row2Edit2[0]["gsis_help"].ToString();
            txtb_gsis_housing_loan.Text     = row2Edit2[0]["gsis_housing_ln"].ToString();
            txtb_hdmf_mp2.Text              = row2Edit2[0]["hdmf_mp2"].ToString();
            txtb_hdmf_loyalty_card.Text     = row2Edit2[0]["hdmf_loyalty_card"].ToString();
            txtb_position.Text              = row2Edit2[0]["position_title1"].ToString();
            
            txtb_other_ded_mand1.Text    = row2Edit2[0]["other_ded_mand1"].ToString();
            txtb_other_ded_mand2.Text    = row2Edit2[0]["other_ded_mand2"].ToString();
            txtb_other_ded_mand3.Text    = row2Edit2[0]["other_ded_mand3"].ToString();
            txtb_other_ded_mand4.Text    = row2Edit2[0]["other_ded_mand4"].ToString();
            txtb_other_ded_mand5.Text    = row2Edit2[0]["other_ded_mand5"].ToString();
            txtb_other_ded_mand6.Text    = row2Edit2[0]["other_ded_mand6"].ToString();
            txtb_other_ded_mand7.Text    = row2Edit2[0]["other_ded_mand7"].ToString();
            txtb_other_ded_mand8.Text    = row2Edit2[0]["other_ded_mand8"].ToString();
            txtb_other_ded_mand9.Text    = row2Edit2[0]["other_ded_mand9"].ToString();
            txtb_other_ded_mand10.Text   = row2Edit2[0]["other_ded_mand10"].ToString();
            txtb_other_ded_prem1.Text    = row2Edit2[0]["other_ded_prem1"].ToString(); 
            txtb_other_ded_prem2.Text    = row2Edit2[0]["other_ded_prem2"].ToString(); 
            txtb_other_ded_prem3.Text    = row2Edit2[0]["other_ded_prem3"].ToString(); 
            txtb_other_ded_prem4.Text    = row2Edit2[0]["other_ded_prem4"].ToString(); 
            txtb_other_ded_prem5.Text    = row2Edit2[0]["other_ded_prem5"].ToString(); 
            txtb_other_ded_prem6.Text    = row2Edit2[0]["other_ded_prem6"].ToString(); 
            txtb_other_ded_prem7.Text    = row2Edit2[0]["other_ded_prem7"].ToString(); 
            txtb_other_ded_prem8.Text    = row2Edit2[0]["other_ded_prem8"].ToString(); 
            txtb_other_ded_prem9.Text    = row2Edit2[0]["other_ded_prem9"].ToString();
            txtb_other_ded_prem10.Text   = row2Edit2[0]["other_ded_prem10"].ToString();
            txtb_other_ded_loan1.Text    = row2Edit2[0]["other_ded_loan1"].ToString(); 
            txtb_other_ded_loan2.Text    = row2Edit2[0]["other_ded_loan2"].ToString(); 
            txtb_other_ded_loan3.Text    = row2Edit2[0]["other_ded_loan3"].ToString(); 
            txtb_other_ded_loan4.Text    = row2Edit2[0]["other_ded_loan4"].ToString(); 
            txtb_other_ded_loan5.Text    = row2Edit2[0]["other_ded_loan5"].ToString(); 
            txtb_other_ded_loan6.Text    = row2Edit2[0]["other_ded_loan6"].ToString(); 
            txtb_other_ded_loan7.Text    = row2Edit2[0]["other_ded_loan7"].ToString(); 
            txtb_other_ded_loan8.Text    = row2Edit2[0]["other_ded_loan8"].ToString(); 
            txtb_other_ded_loan9.Text    = row2Edit2[0]["other_ded_loan9"].ToString();
            txtb_other_ded_loan10.Text   = row2Edit2[0]["other_ded_loan10"].ToString();
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Triggers When Click Calculate Button
        //**************************************************************************
        protected void btn_calculate_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                calculate_salary_summary_tab();
                calculate_pera_summary_tab();
                CalculateManExemption();
                calculate_mandatory();
                calculate_optional();
                calculate_loans();
                calculate_gross_regular();
                //HeaderDetails_Initialized_Add();
                calculate_netpays();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Calculate Salary Summary Tab
        //**************************************************************************
        private void calculate_salary_summary_tab()
        {
            double lates_time = 0;
            double lates_amount = 0;

            lates_time = double.Parse((txtb_lates_in_min.Text.ToString().Trim() != "" ? txtb_lates_in_min.Text.ToString().Trim() : "0"));
            lates_time = lates_time / 60;

            lates_amount = double.Parse(txtb_rate_amount.Text.ToString()) / float.Parse(lbl_lastday_hidden.Text.ToString()) *  (lates_time / float.Parse(lbl_hours_in_1day.Text));

            float lates_amount_override = 0;
            lates_amount_override = float.Parse(txtb_late_in_amount.Text);
            if ((lates_amount_override == lates_amount) || lates_amount_override == 0 )
            {
                txtb_late_in_amount.Text = lates_amount.ToString("###,##0.00").Trim();
            }
            
            float lwo_amount_monthly = 0;
            lwo_amount_monthly = float.Parse(txtb_rate_amount.Text.ToString().Trim()) / float.Parse(lbl_lastday_hidden.Text.ToString()) * float.Parse(txtb_no_days_lwopay.Text.ToString().Trim());

            float lwo_amount_monthly_override = 0;
            lwo_amount_monthly_override = float.Parse(txtb_lwo_pay.Text);
            if ((lwo_amount_monthly_override == lwo_amount_monthly) || lwo_amount_monthly_override == 0)
            {
                txtb_lwo_pay.Text = lwo_amount_monthly.ToString("###,##0.00").Trim();
            }

            double wages_net = 0;
            wages_net = double.Parse(txtb_rate_amount.Text.ToString()) - (double.Parse(txtb_late_in_amount.Text.ToString()) + double.Parse(txtb_lwo_pay.Text.ToString()));
            txtb_wages_amt_net.Text = wages_net.ToString("###,##0.00").Trim();
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Calculate PERA Summary Tab
        //**************************************************************************
        private void calculate_pera_summary_tab()
        {
            double lates_time = 0;
            double lates_amount = 0;
            
            lates_time = double.Parse((txtb_lates_in_min.Text.ToString().Trim() != "" ? txtb_lates_in_min.Text.ToString().Trim() : "0"));
            lates_time = lates_time / 60;
            
            lates_amount = (double.Parse(txtb_pera_amount.Text.ToString()) / float.Parse(lbl_lastday_hidden.Text.ToString())) * (lates_time / float.Parse(lbl_hours_in_1day.Text));

            //float lates_amount_override = 0;
            //lates_amount_override = float.Parse(txtb_late_in_amount_pera.Text);
            //if ((lates_amount_override == lates_amount) || lates_amount_override == 0)
            //{
            //    txtb_late_in_amount_pera.Text = lates_amount.ToString("###,##0.00").Trim();
            //}

            // Update Computation By : Barro 09-26-2019
            double lwo_amount_pera = 0;
            lwo_amount_pera = double.Parse(txtb_pera_amount.Text.ToString()) / float.Parse(lbl_lastday_hidden.Text.ToString()) * float.Parse(txtb_no_days_lwopay.Text.ToString().Trim());

            float lwo_amount_monthly_override = 0;
            lwo_amount_monthly_override = float.Parse(txtb_lwop_amount_pera.Text);
            if ((lwo_amount_monthly_override == lwo_amount_pera) || lwo_amount_monthly_override == 0)
            {
                txtb_lwop_amount_pera.Text = lwo_amount_pera.ToString("###,##0.00").Trim();
            }
            
            double pera_net = 0;
            pera_net = double.Parse(txtb_pera_amount.Text.ToString()) - (double.Parse(txtb_late_in_amount_pera.Text.ToString()) + double.Parse(txtb_lwop_amount_pera.Text.ToString()));
            txtb_pera_amount_net.Text = pera_net.ToString("###,##0.00").Trim();

        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Calculate Gross Amount
        //**************************************************************************
        private void calculate_gross_regular()
        {
            double total_gross           = 0;
            //float lwo_amount_pera       = 0;
            //float lwo_amount_monthly    = 0;
            //double lates_amount         = 0;

            //**************************************************************************
            //  UPDATE - VJA- 07/24/2019 - Gepa Editable ni Sir Ariel ang LWOP Monthly 
            //                           and PERA Kay mag discuss pa sila sa taga HR
            //**************************************************************************

            //lwo_amount_pera             = float.Parse(txtb_pera_amount.Text.ToString().Trim()) / float.Parse(lbl_lastday_hidden.Text.ToString()) * float.Parse(txtb_no_days_lwopay.Text.ToString().Trim());
            //lwo_amount_monthly          = float.Parse(txtb_rate_amount.Text.ToString().Trim()) / float.Parse(lbl_lastday_hidden.Text.ToString()) * float.Parse(txtb_no_days_lwopay.Text.ToString().Trim());
            //lates_amount = double.Parse(txtb_lates_in_min.Text.ToString().Trim())  * (double.Parse(txtb_rate_amount.Text.ToString().Trim()) / float.Parse(lbl_lastday_hidden.Text.ToString())  / float.Parse(lbl_hours_in_1day.Text) / 60);

            //txtb_lwop_amount_pera.Text  = lwo_amount_pera.ToString("###,##0.00").Trim();
            //txtb_lwo_pay.Text           = lwo_amount_monthly.ToString("###,##0.00").Trim();
            //txtb_late_in_amount.Text    = lates_amount.ToString("###,##0.00").Trim();
            
            //Total GROSS PAY PLUS(+) PERA MINUS(-) LWO-Pay
            total_gross                 = double.Parse(txtb_wages_amt_net.Text.ToString().Trim()) + double.Parse(txtb_pera_amount_net.Text.ToString().Trim());
            txtb_gross_pay.Text         = total_gross.ToString("###,##0.00").Trim();

            id_days_hours.Visible = false;
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Calculate Net Pays
        //**************************************************************************
        private void calculate_netpays()
        {
            double total_netpay         = 0;
            double total_netpay1        = 0;
            double total_netpay2        = 0;
            double total_pera_amount    = 0;

            //Net Pay Minus The Loans, Deduction and LWO - Pay Amount
            total_netpay = double.Parse(txtb_gross_pay.Text.ToString()) - (double.Parse(txtb_total_mandatory.Text) + double.Parse(txtb_total_optional.Text) + double.Parse(txtb_total_loans.Text));
            string total_str_netpay = "";
            total_str_netpay        = total_netpay.ToString("###,##0.0000");
            txtb_net_pay.Text       = total_str_netpay.Split('.')[0] + "." + total_str_netpay.Split('.')[1].Substring(0, 2);

            //Net Pay for 1st Half (OLD COMPUTATION 09/30/2019)
            //total_pera_amount       = double.Parse(txtb_pera_amount.Text.ToString()) - double.Parse(txtb_lwop_amount_pera.Text.ToString());
            //total_netpay1           = ((total_netpay - total_pera_amount) / 2 ) + total_pera_amount;
            //txtb_net_pay_1h.Text    = total_netpay1.ToString("###,##0").Trim() + ".00";

            //Net Pay for 1st Half (NEW COMPUTATION 10/16/2019)
                // Special Cases (Update on : 10/22/2019)
                    // If PERA Amount is less-than 2000 or Pro-Rate iyang PERA
                    // or If the LWOP Amount 
                    // or If the Lates in Minutes have amount
            if (double.Parse(txtb_pera_amount.Text.ToString()) < 2000 || double.Parse(txtb_lwop_amount_pera.Text.ToString()) > 0)
            {
                total_pera_amount = double.Parse(txtb_pera_amount.Text.ToString()) - (double.Parse(txtb_lwop_amount_pera.Text.ToString()));
                total_netpay1 = (total_netpay - total_pera_amount) / 2;
                total_netpay1 = total_netpay1 - (total_netpay1 % 1);
                total_netpay1 = total_netpay1 + total_pera_amount;
                txtb_net_pay_1h.Text = total_netpay1.ToString("###,##0.00");
            }
            else
            {
                string total_str_netpay1 = "";
                total_pera_amount = double.Parse(txtb_pera_amount.Text.ToString()) - double.Parse(txtb_lwop_amount_pera.Text.ToString());
                total_netpay1 = ((total_netpay - total_pera_amount) / 2) + total_pera_amount;
                total_str_netpay1 = total_netpay1.ToString("###,##0.0000");
                txtb_net_pay_1h.Text = total_str_netpay1.Split('.')[0] + ".00";
            }

            //Net Pay for 2nd Half (OLD COMPUTATION 09/30/2019)
            //total_netpay2 = total_netpay - total_netpay1;
            //txtb_net_pay_2h.Text = total_netpay2.ToString("###,##0.00").Split('.')[0] + "." + txtb_net_pay.Text.Split('.')[1].Substring(0, 2);

            //Net Pay for 2nd Half (NEW COMPUTATION 10/16/2019) - OLD 
            total_netpay2 = (double.Parse(txtb_net_pay.Text.ToString()) - total_pera_amount) / 2;
            total_netpay2 = total_netpay2 + (total_netpay2 % 1);
            txtb_net_pay_2h.Text = total_netpay2.ToString("###,#00.00");
            
            if (double.Parse(txtb_net_pay.Text) < double.Parse(lbl_minimum_netpay_hidden.Text))
            {
                FieldValidationColorChanged(true, "below-minumum-net");
                txtb_net_pay_2h.Focus();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - The Description of Label Of Loan and Optional Deduction Toogle
        //**************************************************************************
        private void RetrieveLoanPremiums_Visible()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollmapping_others_flag_list", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    //OTHER PREMIUM
                    lbl_other_prem1.Text            = dt.Rows[0]["descr_other_premium1"].ToString() == "" ? lbl_other_prem1.Text : dt.Rows[0]["descr_other_premium1"].ToString() + ":";
                    txtb_otherpremium_no1.Enabled   = dt.Rows[0]["other_premium1"].ToString()       == "Y" ? true : false;
                                                      
                    lbl_other_prem2.Text            = dt.Rows[0]["descr_other_premium2"].ToString() == "" ? lbl_other_prem2.Text : dt.Rows[0]["descr_other_premium2"].ToString() + ":";
                    txtb_otherpremium_no2.Enabled   = dt.Rows[0]["other_premium2"].ToString()       == "Y" ? true : false;
                                                      
                    lbl_other_prem3.Text            = dt.Rows[0]["descr_other_premium3"].ToString() == "" ? lbl_other_prem3.Text : dt.Rows[0]["descr_other_premium3"].ToString() + ":";
                    txtb_otherpremium_no3.Enabled   = dt.Rows[0]["other_premium3"].ToString()       == "Y" ? true : false;
                                                      
                    lbl_other_prem4.Text            = dt.Rows[0]["descr_other_premium4"].ToString() == "" ? lbl_other_prem4.Text : dt.Rows[0]["descr_other_premium4"].ToString() + ":";
                    txtb_otherpremium_no4.Enabled   = dt.Rows[0]["other_premium4"].ToString()       == "Y" ? true : false;
                                                      
                    lbl_other_prem5.Text            = dt.Rows[0]["descr_other_premium5"].ToString() == "" ? lbl_other_prem5.Text : dt.Rows[0]["descr_other_premium5"].ToString() + ":";
                    txtb_otherpremium_no5.Enabled   = dt.Rows[0]["other_premium5"].ToString()       == "Y" ? true : false;
                                                      
                    //OTHER LOAN                      
                    lbl_loan1.Text                  = dt.Rows[0]["descr_other_loan1"].ToString()    == "" ? lbl_loan1.Text : dt.Rows[0]["descr_other_loan1"].ToString() + ":";
                    txtb_otherloan_no1.Enabled      = dt.Rows[0]["other_loan1"].ToString()          == "Y" ? true : false;
                                                  
                    lbl_loan2.Text                  = dt.Rows[0]["descr_other_loan2"].ToString()    == "" ? lbl_loan2.Text : dt.Rows[0]["descr_other_loan2"].ToString() + ":";
                    txtb_otherloan_no2.Enabled      = dt.Rows[0]["other_loan2"].ToString()          == "Y" ? true : false;
                                                  
                    lbl_loan3.Text                  = dt.Rows[0]["descr_other_loan3"].ToString()    == "" ? lbl_loan3.Text : dt.Rows[0]["descr_other_loan3"].ToString() + ":";
                    txtb_otherloan_no3.Enabled      = dt.Rows[0]["other_loan3"].ToString()          == "Y" ? true : false;
                                                  
                    lbl_loan4.Text                  = dt.Rows[0]["descr_other_loan4"].ToString()    == "" ? lbl_loan4.Text : dt.Rows[0]["descr_other_loan4"].ToString() + ":";
                    txtb_otherloan_no4.Enabled      = dt.Rows[0]["other_loan4"].ToString()          == "Y" ? true : false;
                                                  
                    lbl_loan5.Text                  = dt.Rows[0]["descr_other_loan5"].ToString()    == "" ? lbl_loan5.Text : dt.Rows[0]["descr_other_loan5"].ToString() + ":";
                    txtb_otherloan_no5.Enabled      = dt.Rows[0]["other_loan5"].ToString()          == "Y" ? true : false;
                }
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Calculate Mandatory Tab
        //**************************************************************************
        private void calculate_mandatory()
        {
            double total_mandatory = 0;
            total_mandatory = total_mandatory + double.Parse(txtb_gsis_ps.Text.ToString());
            //total_mandatory = total_mandatory + float.Parse(txtb_gsis_sif.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_hdmf_ps.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_phic_ps.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_bir_tax.Text.ToString());

            // Add Field Again - 2022-05-30
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand2.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand3.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand4.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand5.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand6.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand7.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand8.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand9.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand10.Text.ToString());
            
            txtb_total_mandatory.Text = total_mandatory.ToString("###,##0.00");
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Calculate Optional Tab
        //**************************************************************************
        private void calculate_optional()
        {
            double total_optional = 0;
            total_optional = total_optional + double.Parse(txtb_gsis_ouli.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ouli45.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ouli50.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ouli55.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ouli60.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ouli65.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_sss.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_hdmf_addl.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_philam.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ehp.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_hip.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ceap.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_add.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no1.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no2.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no3.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no4.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no5.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_hdmf_mp2.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_hdmf_loyalty_card.Text.ToString());

            // Add Field Again - 2022-05-30
            total_optional = total_optional + double.Parse(txtb_other_ded_prem1.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem2.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem3.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem4.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem5.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem6.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem7.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem8.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem9.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem10.Text.ToString());
            
            txtb_total_optional.Text = total_optional.ToString("###,##0.00");
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Calculate Loan Tab
        //**************************************************************************
        private void calculate_loans()
        {
            double total_loans = 0;
            total_loans = total_loans + double.Parse(txtb_gsis_consolidated.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_policy_regular.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_policy_optional.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_ouli_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_emer_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_ecard_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_educ_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_real_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_sos_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_hdmf_mpl_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_hdmf_house_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_hdmf_cal_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_ccmpc_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_nico_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_networkbank_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no1.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no2.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no3.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no4.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no5.Text.ToString());

            // Add Field - 03/12/2019
            total_loans = total_loans + double.Parse(txtb_nhmfc_hsng.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_nafc.Text.ToString());

            // Add Field Again - 03/14/2019
            total_loans = total_loans + double.Parse(txtb_gsis_help.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_housing_loan.Text.ToString());

            // Add Field Again - 2022-05-30
            total_loans = total_loans + double.Parse(txtb_other_ded_loan1.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan2.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan3.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan4.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan5.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan6.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan7.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan8.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan9.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan10.Text.ToString());
            
            txtb_total_loans.Text = total_loans.ToString("###,##0.00");
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Redirect to Account Ledger Page
        //**************************************************************************
        protected void btn_editloan_Click(object sender, EventArgs e)
        {
            // BEGIN - Pass Value
            // Employee ID      [0]
            // Registry         [1]
            // Year             [2]
            // Employment Type  [3]
            // Department       [4]
            // END   - Pass Value

            string url = "";
            Session["PreviousValuesonPage_cPayRegistry_RECEJO"] = txtb_empl_id.Text.ToString() + "," + lbl_registry_no.Text.ToString() + "," + ddl_year.SelectedValue.ToString() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_dep.SelectedValue.ToString();
            url = "/View/cPayAccountLedger/cPayAccountLedger.aspx";
            Response.Redirect(url);
            
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Hidden Button Trriggers When Back To this Page
        //**************************************************************************
        protected void btn_edit_hidden_Click(object sender, EventArgs e)
        {
            if (Session["PreviousValuesonPage_cPayRegistry_RECEJO"] == null)
                Session["PreviousValuesonPage_cPayRegistry_RECEJO"] = "";
            else if (Session["PreviousValuesonPage_cPayRegistry_RECEJO"].ToString() != string.Empty)
            {
                editaddmodal(Session["PreviousValuesonPage_cPayRegistry_RECEJO"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openModal();", true);
                Session["PreviousValuesonPage_cPayRegistry_RECEJO"] = "";
            }
            else if (Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"].ToString() != string.Empty)
            {
                editaddmodal(Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openModal();", true);
                Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"] = "";
                Session["PreviousValuesonPage_EmployeeName"] = "";
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Redirect to Other Contribution Page
        //**************************************************************************
        protected void btn_contributions_Click(object sender, EventArgs e)
        {
            // BEGIN - Pass Value
            // Employee ID      [0]
            // Registry         [1]
            // Year             [2]
            // Employment Type  [3]
            // Department       [4]
            // Employee Name    [5]
            // END  - Pass Value

            string url = "";
            Session["PreviousValuesonPage_cPayRegistry_RECEJO_OthContributions"] = txtb_empl_id.Text.ToString() + "," + lbl_registry_no.Text.ToString() + "," + ddl_year.SelectedValue.ToString() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_dep.SelectedValue.ToString() + "," + txtb_employeename.Text.ToString(); ;
            Session["PreviousValuesonPage_EmployeeName"] = txtb_employeename.Text.ToString();
            url = "/View/cPayOthContributions_AddEdit/cPayOthContributions_AddEdit.aspx";
            Response.Redirect(url);
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            txtb_pera_amount.Enabled            = ifenable;
            txtb_no_days_lwopay.Enabled         = ifenable;
            txtb_lwo_pay.Enabled                = ifenable;
            txtb_gsis_gs.Enabled                = ifenable;
            txtb_gsis_ps.Enabled                = ifenable;
            txtb_gsis_sif.Enabled               = ifenable;
            txtb_hdmf_gs.Enabled                = ifenable;
            txtb_hdmf_ps.Enabled                = ifenable;
            txtb_phic_gs.Enabled                = ifenable;
            txtb_phic_ps.Enabled                = ifenable;
            txtb_bir_tax.Enabled                = ifenable;
            txtb_gsis_ouli.Enabled              = ifenable;
            txtb_gsis_ouli45.Enabled            = ifenable;
            txtb_gsis_ouli50.Enabled            = ifenable;
            txtb_gsis_ouli55.Enabled            = ifenable;
            txtb_gsis_ouli60.Enabled            = ifenable;
            txtb_gsis_ouli65.Enabled            = ifenable;
            txtb_sss.Enabled                    = ifenable;
            txtb_hdmf_addl.Enabled              = ifenable;
            txtb_philam.Enabled                 = ifenable;
            txtb_gsis_ehp.Enabled               = ifenable;
            txtb_gsis_hip.Enabled               = ifenable;
            txtb_gsis_ceap.Enabled              = ifenable;
            txtb_gsis_add.Enabled               = ifenable;
            txtb_otherpremium_no1.Enabled       = ifenable;
            txtb_otherpremium_no2.Enabled       = ifenable;
            txtb_otherpremium_no3.Enabled       = ifenable;
            txtb_otherpremium_no4.Enabled       = ifenable;
            txtb_otherpremium_no5.Enabled       = ifenable;
            txtb_gsis_consolidated.Enabled      = ifenable;
            txtb_gsis_policy_regular.Enabled    = ifenable;
            txtb_gsis_policy_optional.Enabled   = ifenable;
            txtb_gsis_ouli_loan.Enabled         = ifenable;
            txtb_gsis_emer_loan.Enabled         = ifenable;
            txtb_gsis_ecard_loan.Enabled        = ifenable;
            txtb_gsis_educ_loan.Enabled         = ifenable;
            txtb_gsis_real_loan.Enabled         = ifenable;
            txtb_gsis_sos_loan.Enabled          = ifenable;
            txtb_hdmf_mpl_loan.Enabled          = ifenable;
            txtb_hdmf_house_loan.Enabled        = ifenable;
            txtb_hdmf_cal_loan.Enabled          = ifenable;
            txtb_nico_loan.Enabled              = ifenable;
            txtb_networkbank_loan.Enabled       = ifenable;
            txtb_ccmpc_loan.Enabled             = ifenable;
            txtb_otherloan_no1.Enabled          = ifenable;
            txtb_otherloan_no2.Enabled          = ifenable;
            txtb_otherloan_no3.Enabled          = ifenable;
            txtb_otherloan_no4.Enabled          = ifenable;
            txtb_otherloan_no5.Enabled          = ifenable;
            txtb_no_days_worked.Enabled         = ifenable;
            txtb_no_hours_worked.Enabled        = ifenable;
            txtb_lwop_amount_pera.Enabled       = ifenable;
            txtb_nhmfc_hsng.Enabled             = ifenable;
            txtb_nafc.Enabled                   = ifenable;
            txtb_gsis_help.Enabled              = ifenable;
            txtb_gsis_housing_loan.Enabled      = ifenable;
            txtb_hdmf_mp2.Enabled               = ifenable;
            txtb_hdmf_loyalty_card.Enabled      = ifenable;
            txtb_lates_in_min.Enabled           = ifenable;
            txtb_late_in_amount.Enabled         = ifenable;
            txtb_remarks.Enabled                = ifenable;
            txtb_rate_amount.Enabled            = ifenable;
            txtb_late_in_amount_pera.Enabled    = ifenable;

            txtb_other_ded_mand1.Enabled      = ifenable;
            txtb_other_ded_mand2.Enabled      = ifenable;
            txtb_other_ded_mand3.Enabled      = ifenable;
            txtb_other_ded_mand4.Enabled      = ifenable;
            txtb_other_ded_mand5.Enabled      = ifenable;
            txtb_other_ded_mand6.Enabled      = ifenable;
            txtb_other_ded_mand7.Enabled      = ifenable;
            txtb_other_ded_mand8.Enabled      = ifenable;
            txtb_other_ded_mand9.Enabled      = ifenable;
            txtb_other_ded_mand10.Enabled     = ifenable;
            txtb_other_ded_prem1.Enabled      = ifenable;
            txtb_other_ded_prem2.Enabled      = ifenable;
            txtb_other_ded_prem3.Enabled      = ifenable;
            txtb_other_ded_prem4.Enabled      = ifenable;
            txtb_other_ded_prem5.Enabled      = ifenable;
            txtb_other_ded_prem6.Enabled      = ifenable;
            txtb_other_ded_prem7.Enabled      = ifenable;
            txtb_other_ded_prem8.Enabled      = ifenable;
            txtb_other_ded_prem9.Enabled      = ifenable;
            txtb_other_ded_prem10.Enabled     = ifenable;
            txtb_other_ded_loan1.Enabled      = ifenable;
            txtb_other_ded_loan2.Enabled      = ifenable;
            txtb_other_ded_loan3.Enabled      = ifenable;
            txtb_other_ded_loan4.Enabled      = ifenable;
            txtb_other_ded_loan5.Enabled      = ifenable;
            txtb_other_ded_loan6.Enabled      = ifenable;
            txtb_other_ded_loan7.Enabled      = ifenable;
            txtb_other_ded_loan8.Enabled      = ifenable;
            txtb_other_ded_loan9.Enabled      = ifenable;
            txtb_other_ded_loan10.Enabled     = ifenable;

        }

        //***************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void imgbtn_print_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id              = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year         = commandArgs[2];

            lnkPrint.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopX", "openSelectReport();", true);
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
            //Session["PreviousValuesonPage_cPayRegistry"] = ddl_year.SelectedValue.ToString() + "," + ddl_month.SelectedValue.ToString().Trim() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_payroll_template.SelectedValue.ToString() + "," + gv_dataListGrid.PageIndex + "," + DropDownListID.SelectedValue.ToString() + "," + lnkPrint.CommandArgument.Split(',')[1].ToString().Trim() + "," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + "," + txtb_search.Text.ToString().Trim();
            
                switch (ddl_select_report.SelectedValue)
                {
                    case "01": // PaySLip  - For Regular 
                        printreport = "/cryRegularReports/cryPayslip/cryPaySlip.rpt";
                        procedure = "sp_payrollregistry_salary_payslip_re_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[1].ToString().Trim() + ",par_payrolltemplate_code," + "212" + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                        break;
                
                    case "02": // Net Take Home Pay 
                        printreport = "/cryOtherPayroll/cryNTHP/cryNTHP.rpt";
                        procedure = "sp_payrollregistry_takehome";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_orno," + txtb_or_nbr.Text.ToString().Trim() + ",par_ordate," + txtb_or_date.Text.ToString().Trim();
                        break;
                
                }

            if (url != "")
                {
                    Response.Redirect(url);
                }
            
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Trigger when Select Report
        //*************************************************************************
        protected void ddl_select_report_SelectedIndexChanged(object sender, EventArgs e)
        {
             lbl_or_nbr.Visible      = false;
             txtb_or_nbr.Visible     = false;
             lbl_or_date.Visible     = false;
             txtb_or_date.Visible    = false;
             
             if (ddl_select_report.SelectedValue == "02")
             {
                 lbl_or_nbr.Visible      = true;
                 txtb_or_nbr.Visible     = true;
                 lbl_or_date.Visible     = true;
                 txtb_or_date.Visible    = true;
             }

            Update_or_date.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopSHowDate", "show_date();", true);
        }
        //******************************************************************************************
        //  BEGIN - VJA- 2021-04-12 - Calculate Exempted Deduction
        //******************************************************************************************
        private void CalculateManExemption()
        {
            DataTable dataList_employee_flag = new DataTable();
            dataList_employee_flag = MyCmn.RetrieveData("sp_personnelnames_combolist_flag_expt", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR().ToString().Trim());
            DataRow[] selected_employee = dataList_employee_flag.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");

            if (selected_employee.Length > 0)
            {
                // BEGIN - VJA : 2021-04-12 - Exempted Mandatory Deduction
                if (selected_employee[0]["flag_expt_phic"].ToString() == "1" ||
                selected_employee[0]["flag_expt_phic"].ToString() == "True")
                {
                    txtb_phic_ps.Text = "0.00";
                    txtb_phic_gs.Text = "0.00";
                }

                if (selected_employee[0]["flag_expt_hdmf"].ToString() == "1" ||
                selected_employee[0]["flag_expt_hdmf"].ToString() == "True")
                {
                    txtb_hdmf_ps.Text = "0.00";
                    txtb_hdmf_gs.Text = "0.00";
                }

                if (double.Parse(selected_employee[0]["hdmf_fix_rate"].ToString()) != 0)
                {
                    txtb_hdmf_ps.Text = selected_employee[0]["hdmf_fix_rate"].ToString();
                }

                if (selected_employee[0]["flag_expt_gsis"].ToString() == "1" ||
                selected_employee[0]["flag_expt_gsis"].ToString() == "True")
                {
                    txtb_gsis_ps.Text = "0.00";
                    txtb_gsis_gs.Text = "0.00";
                }
                // END - VJA : 2021-04-12 - Exempted Mandatory Deduction
            }
        }
        //*****************************************************************
        //  BEGIN - VJA- 2022-05-30 - Reserved Deductions Description
        //*****************************************************************
        private void RetrieveReserveDeduction()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_othded_setup_tbl_list", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    // MANDATORY DEDUCTION
                    lbl_other_ded_mand1.Text        = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand1_descr"].ToString() + ":";
                    lbl_other_ded_mand2.Text        = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand2_descr"].ToString() + ":";
                    lbl_other_ded_mand3.Text        = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand3_descr"].ToString() + ":";
                    lbl_other_ded_mand4.Text        = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand4_descr"].ToString() + ":";
                    lbl_other_ded_mand5.Text        = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand5_descr"].ToString() + ":";
                    lbl_other_ded_mand6.Text        = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand6_descr"].ToString() + ":";
                    lbl_other_ded_mand7.Text        = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand7_descr"].ToString() + ":";
                    lbl_other_ded_mand8.Text        = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand8_descr"].ToString() + ":";
                    lbl_other_ded_mand9.Text        = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand9_descr"].ToString() + ":";
                    lbl_other_ded_mand10.Text       = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand10_descr"].ToString() + ":";
                    
                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Text        = dt.Rows[0]["other_ded_prem1_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem1_descr"].ToString() + ":";
                    lbl_other_ded_prem2.Text        = dt.Rows[0]["other_ded_prem2_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem2_descr"].ToString() + ":";
                    lbl_other_ded_prem3.Text        = dt.Rows[0]["other_ded_prem3_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem3_descr"].ToString() + ":";
                    lbl_other_ded_prem4.Text        = dt.Rows[0]["other_ded_prem4_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem4_descr"].ToString() + ":";
                    lbl_other_ded_prem5.Text        = dt.Rows[0]["other_ded_prem5_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem5_descr"].ToString() + ":";
                    lbl_other_ded_prem6.Text        = dt.Rows[0]["other_ded_prem6_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem6_descr"].ToString() + ":";
                    lbl_other_ded_prem7.Text        = dt.Rows[0]["other_ded_prem7_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem7_descr"].ToString() + ":";
                    lbl_other_ded_prem8.Text        = dt.Rows[0]["other_ded_prem8_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem8_descr"].ToString() + ":";
                    lbl_other_ded_prem9.Text        = dt.Rows[0]["other_ded_prem9_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem9_descr"].ToString() + ":";
                    lbl_other_ded_prem10.Text       = dt.Rows[0]["other_ded_prem10_descr"].ToString()  == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem10_descr"].ToString() + ":";
                    
                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Text        = dt.Rows[0]["other_ded_loan1_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan1_descr"].ToString() + ":";
                    lbl_other_ded_loan2.Text        = dt.Rows[0]["other_ded_loan2_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan2_descr"].ToString() + ":";
                    lbl_other_ded_loan3.Text        = dt.Rows[0]["other_ded_loan3_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan3_descr"].ToString() + ":";
                    lbl_other_ded_loan4.Text        = dt.Rows[0]["other_ded_loan4_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan4_descr"].ToString() + ":";
                    lbl_other_ded_loan5.Text        = dt.Rows[0]["other_ded_loan5_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan5_descr"].ToString() + ":";
                    lbl_other_ded_loan6.Text        = dt.Rows[0]["other_ded_loan6_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan6_descr"].ToString() + ":";
                    lbl_other_ded_loan7.Text        = dt.Rows[0]["other_ded_loan7_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan7_descr"].ToString() + ":";
                    lbl_other_ded_loan8.Text        = dt.Rows[0]["other_ded_loan8_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan8_descr"].ToString() + ":";
                    lbl_other_ded_loan9.Text        = dt.Rows[0]["other_ded_loan9_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan9_descr"].ToString() + ":";
                    lbl_other_ded_loan10.Text       = dt.Rows[0]["other_ded_loan10_descr"].ToString()  == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan10_descr"].ToString() + ":";
                    
                    // ***********************************************************************************************************
                    // ***** DO NOT DISPLAY THE TEXTBOXES IF NO DESCRIPTION ******************************************************
                    // ***********************************************************************************************************

                    // MANDATORY DEDUCTION
                    lbl_other_ded_mand1.Visible        = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand2.Visible        = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand3.Visible        = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand4.Visible        = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand5.Visible        = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand6.Visible        = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand7.Visible        = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand8.Visible        = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand9.Visible        = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand10.Visible       = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? false : true;

                    txtb_other_ded_mand1.Visible       = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand2.Visible       = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand3.Visible       = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand4.Visible       = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand5.Visible       = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand6.Visible       = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand7.Visible       = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand8.Visible       = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand9.Visible       = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand10.Visible      = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? false : true;

                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Visible        = dt.Rows[0]["other_ded_prem1_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem2.Visible        = dt.Rows[0]["other_ded_prem2_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem3.Visible        = dt.Rows[0]["other_ded_prem3_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem4.Visible        = dt.Rows[0]["other_ded_prem4_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem5.Visible        = dt.Rows[0]["other_ded_prem5_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem6.Visible        = dt.Rows[0]["other_ded_prem6_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem7.Visible        = dt.Rows[0]["other_ded_prem7_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem8.Visible        = dt.Rows[0]["other_ded_prem8_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem9.Visible        = dt.Rows[0]["other_ded_prem9_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem10.Visible        = dt.Rows[0]["other_ded_prem10_descr"].ToString()  == "" ? false : true;

                    txtb_other_ded_prem1.Visible     = dt.Rows[0]["other_ded_prem1_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem2.Visible     = dt.Rows[0]["other_ded_prem2_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem3.Visible     = dt.Rows[0]["other_ded_prem3_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem4.Visible     = dt.Rows[0]["other_ded_prem4_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem5.Visible     = dt.Rows[0]["other_ded_prem5_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem6.Visible     = dt.Rows[0]["other_ded_prem6_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem7.Visible     = dt.Rows[0]["other_ded_prem7_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem8.Visible     = dt.Rows[0]["other_ded_prem8_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem9.Visible     = dt.Rows[0]["other_ded_prem9_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem10.Visible    = dt.Rows[0]["other_ded_prem10_descr"].ToString()      == "" ? false : true;

                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Visible        = dt.Rows[0]["other_ded_loan1_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan2.Visible        = dt.Rows[0]["other_ded_loan2_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan3.Visible        = dt.Rows[0]["other_ded_loan3_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan4.Visible        = dt.Rows[0]["other_ded_loan4_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan5.Visible        = dt.Rows[0]["other_ded_loan5_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan6.Visible        = dt.Rows[0]["other_ded_loan6_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan7.Visible        = dt.Rows[0]["other_ded_loan7_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan8.Visible        = dt.Rows[0]["other_ded_loan8_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan9.Visible        = dt.Rows[0]["other_ded_loan9_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan10.Visible       = dt.Rows[0]["other_ded_loan10_descr"].ToString()    == "" ? false : true;

                    txtb_other_ded_loan1.Visible     = dt.Rows[0]["other_ded_loan1_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan2.Visible     = dt.Rows[0]["other_ded_loan2_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan3.Visible     = dt.Rows[0]["other_ded_loan3_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan4.Visible     = dt.Rows[0]["other_ded_loan4_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan5.Visible     = dt.Rows[0]["other_ded_loan5_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan6.Visible     = dt.Rows[0]["other_ded_loan6_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan7.Visible     = dt.Rows[0]["other_ded_loan7_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan8.Visible     = dt.Rows[0]["other_ded_loan8_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan9.Visible     = dt.Rows[0]["other_ded_loan9_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan10.Visible    = dt.Rows[0]["other_ded_loan10_descr"].ToString()      == "" ? false : true;
                }
            }
        }
        //************************************************************************************************
        //  BEGIN - VJA- 2022-05-30 - Check, Insert and Update Additional Table for Reserved Dedutions
        //************************************************************************************************
        private void InsertUpdateOtherDeduction()
        {

            DataTable dt = MyCmn.RetrieveData("payrollregistry_dtl_othded_chk", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString(), "par_payroll_year", ddl_year.SelectedValue.ToString() , "par_payroll_month", ddl_month.SelectedValue.ToString(), "par_payroll_registry_nbr", lbl_registry_no.Text.ToString(), "par_empl_id", txtb_empl_id.Text.ToString(), "par_seq_no", "");
            if (dt != null)
            {
                string insert_update_script = "";

                // UPDATE DATA FROM OTHER DEDUCTIONS
                if (dt.Rows[0]["flag_return"].ToString() == "U")
                {
                    insert_update_script = "update payrollregistry_dtl_othded_tbl set "
                                            + "other_ded_mand1 ="
                                            + txtb_other_ded_mand1.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand2 ="
                                            + txtb_other_ded_mand2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand3 ="
                                            + txtb_other_ded_mand3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand4 ="
                                            + txtb_other_ded_mand4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand5 ="
                                            + txtb_other_ded_mand5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand6 ="
                                            + txtb_other_ded_mand6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand7 ="
                                            + txtb_other_ded_mand7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand8 ="
                                            + txtb_other_ded_mand8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand9 ="
                                            + txtb_other_ded_mand9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand10 ="
                                            + txtb_other_ded_mand10.Text.ToString().Trim().Replace(",", "")

                                            + ",other_ded_prem1 ="
                                            + txtb_other_ded_prem1.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem2 ="
                                            + txtb_other_ded_prem2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem3 ="
                                            + txtb_other_ded_prem3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem4 ="
                                            + txtb_other_ded_prem4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem5 ="
                                            + txtb_other_ded_prem5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem6="
                                            + txtb_other_ded_prem6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem7 ="
                                            + txtb_other_ded_prem7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem8 ="
                                            + txtb_other_ded_prem8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem9 ="
                                            + txtb_other_ded_prem9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem10 ="
                                            + txtb_other_ded_prem10.Text.ToString().Trim().Replace(",", "")

                                            + ",other_ded_loan1 ="
                                            + txtb_other_ded_loan1.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan2 ="
                                            + txtb_other_ded_loan2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan3 ="
                                            + txtb_other_ded_loan3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan4 ="
                                            + txtb_other_ded_loan4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan5 ="
                                            + txtb_other_ded_loan5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan6 ="
                                            + txtb_other_ded_loan6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan7 ="
                                            + txtb_other_ded_loan7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan8 ="
                                            + txtb_other_ded_loan8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan9 ="
                                            + txtb_other_ded_loan9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan10 ="
                                            + txtb_other_ded_loan10.Text.ToString().Trim().Replace(",", "")

                                            + "where "
                                            +         "payrolltemplate_code = '" + ddl_payroll_template.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_year= '"          + ddl_year.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_month= '"         + ddl_month.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_registry_nbr= '"  + lbl_registry_no.Text.ToString() + "'"
                                            + "AND " + "empl_id= '"               + txtb_empl_id.Text + "'";

                    MyCmn.Update_InsertTable(insert_update_script);
                }
                // INSERT DATA TO OTHER DEDUCTIONS
                else if (dt.Rows[0]["flag_return"].ToString() == "I")
                {
                    insert_update_script = "insert into payrollregistry_dtl_othded_tbl select "
                                         +       "'" + ddl_payroll_template.SelectedValue.ToString() + "'"
                                         + "," + "'" + ddl_year.SelectedValue.ToString()             + "'"
                                         + "," + "'" + ddl_month.SelectedValue.ToString()            + "'"
                                         + "," + "'" + lbl_registry_no.Text.ToString()               + "'"
                                         + "," + "'" + txtb_empl_id.Text                             + "'"
                                         + "," + txtb_other_ded_mand1.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand2.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand3.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand4.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand5.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand6.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand7.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand8.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand9.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_mand10.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_prem1.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem2.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem3.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem4.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem5.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem6.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem7.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem8.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem9.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_prem10.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_loan1.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan2.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan3.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan4.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan5.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan6.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan7.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan8.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan9.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_loan10.Text.ToString().Trim().Replace(",", "")
                                         + "," + "''";

                    MyCmn.Update_InsertTable(insert_update_script);
                }
                else
                {
                }
            }
         }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}