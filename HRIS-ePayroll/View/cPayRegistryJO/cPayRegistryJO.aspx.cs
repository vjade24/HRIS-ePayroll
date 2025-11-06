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
    public partial class cPayRegistryJO : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 05/25/2019 - Data Place holder creation 
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

        DataTable dtSource_dtl_for_display
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_for_display"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_for_display"];
            }
            set
            {
                ViewState["dtSource_dtl_for_display"] = value;
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

        DataTable dataList_employee_tax
        {
            get
            {
                if ((DataTable)ViewState["dataList_employee_tax"] == null) return null;
                return (DataTable)ViewState["dataList_employee_tax"];
            }
            set
            {
                ViewState["dataList_employee_tax"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 05/25/2019 - Page Load method
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
                    ViewState["page_allow_print"]   = 1;
                }

                if (Session["PreviousValuesonPage_cPayRegistry"] == null)
                    Session["PreviousValuesonPage_cPayRegistry"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_month.SelectedValue = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue = prevValues[2].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue = prevValues[3].ToString();
                    RetriveGroupings();
                    ddl_payroll_group.SelectedValue = prevValues[7].ToString();
                    lbl_registry_number.Text = prevValues[7].ToString();
                    RetrieveReserveDeduction();
                    ddl_payroll_template.Enabled            = false;
                    ddl_month.Enabled                       = false;
                    ddl_year.Enabled                        = false;
                    ddl_empl_type.Enabled                   = false;
                    RetrieveDataListGrid();
                    ViewState["payroll_group_nbr"] = prevValues[6].ToString().Trim();
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

                RetrievePayrollInstallation();
                Toogle_1st_Quincena();
            }
        }

        //********************************************************************
        //  BEGIN - VJA- 05/25/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPayRegistryJO"] = "cPayRegistryJO";
            
            RetrieveDataListGrid();
            RetriveGroupings();
            RetriveTemplate();

            chkbox_continue.Visible = false;
            //upd_continue.Visible = false;
            //upd_continue.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Retrieve From Installation Table 
        //*************************************************************************
        private void RetrievePayrollInstallation()
        {
            //  This is to Get Last Day of The Month
            DateTime last_Date = new DateTime(Convert.ToInt32(ddl_year.SelectedValue.ToString()), Convert.ToInt32(ddl_month.SelectedValue.ToString()), 1).AddMonths(1).AddDays(-1);
            int lastday = Convert.ToInt32(last_Date.Day.ToString());
            lbl_lastday_hidden.Value = lastday.ToString();

            DataTable dt = MyCmn.RetrieveData("sp_payrollinstallation_tbl_list", "par_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_include_history", "N");
            if (dt.Rows.Count > 0)
            {
                hidden_monthly_days.Value      = dt.Rows[0]["monthly_salary_days_conv"].ToString();
                hidden_hrs_in1day.Value        = dt.Rows[0]["hours_in_1day_conv"].ToString();
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 05/25/2019 - Retrieve Employee Name From Stored Procedure
        //********************************************************************
        private void RetrieveEmployeename()
        {
            string group_nbr = "";
            //if (dataListGrid.Rows.Count > 0)
            //{
            //    group_nbr = dataListGrid.Rows[0]["payroll_group_nbr"].ToString().Trim();
            //}
            //else
            //{
            //    group_nbr = GetRegistry_NBR();
            //}
            group_nbr = ViewState["payroll_group_nbr"].ToString().Trim();
            ddl_empl_id.Items.Clear();
            dataList_employee = MyCmn.RetrieveData("sp_personnelnames_combolist_preg_jo_payroll", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", group_nbr);

            ddl_empl_id.DataSource = dataList_employee;
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
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

            ddl_payroll_template.DataSource     = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField  = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li                         = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }
        //********************************************************************
        //  BEGIN - VJA- 05/25/2019 - Retrieve Registry Number
        //********************************************************************
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
        //  BEGIN - VJA- 05/25/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            GetRegistry_NBR();
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_hdr_jo_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month",ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr",lbl_registry_number.Text, "par_payrolltemplate_code",ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR().ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize    = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text            = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }

        //*************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            ClearEntry();

            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();
            
            RetrieveEmployeename();
            
            ddl_empl_id.Enabled = true;
            txtb_employeename.Visible   = false;
            ddl_empl_id.Visible         = true;
            txtb_voucher_nbr.Enabled    = false;

            btn_calculate.Visible       = true;
            LabelAddEdit.Text   = "Add New Record | Registry No : " + lbl_registry_number.Text;

            RetrieveGetPremAndOther_flag();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            chkbox_continue.Visible     = false;
            chkbox_continue.Checked     = false;
            txtb_employeename.Visible   = false;
            ddl_empl_id.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Toogle Loans and Optional Deductions RESERVED 
        //*************************************************************************
        private void RetrieveGetPremAndOther_flag()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollmapping_others_flag_list", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

                    div_lbl_loan1.Visible = true;
                    div_txt_loan1.Visible = true;
                    div_lbl_loan2.Visible = true;
                    div_txt_loan2.Visible = true;
                    div_lbl_loan3.Visible = true;
                    div_txt_loan3.Visible = true;
                    div_txt_prem1.Visible = true;
                    div_lbl_prem1.Visible = true;
                    div_txt_prem2.Visible = true;
                    div_lbl_prem2.Visible = true;
                    div_txt_prem3.Visible = true;
                    div_lbl_prem3.Visible = true;

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["other_loan1"].ToString() == "Y")
                    {
                        txtb_otherloan_no1.Enabled = true;
                        lbl_other_loan1.Text = dt.Rows[0]["descr_other_loan1"].ToString();
                    }
                    else {
                        txtb_otherloan_no1.Enabled = false;
                        lbl_other_loan1.Text = "(Reserved1):";
                    }


                    if (dt.Rows[0]["other_loan2"].ToString() == "Y")
                    {
                        txtb_otherloan_no2.Enabled = true;
                        lbl_other_loan2.Text = dt.Rows[0]["descr_other_loan2"].ToString();
                    }
                    else
                    {
                        txtb_otherloan_no2.Enabled = false;
                        lbl_other_loan2.Text = "(Reserved2):";
                    }


                    if (dt.Rows[0]["other_loan3"].ToString() == "Y")
                    {
                        txtb_otherloan_no3.Enabled = true;
                        lbl_other_loan3.Text = dt.Rows[0]["descr_other_loan3"].ToString();
                    }
                    else
                    {
                        txtb_otherloan_no3.Enabled = false;
                        lbl_other_loan3.Text = "(Reserved3):";
                    }

                    if (dt.Rows[0]["other_premium1"].ToString() == "Y")
                    {
                        txtb_otherpremium_no1.Enabled = true;
                        lbl_other_prem1.Text = dt.Rows[0]["descr_other_premium1"].ToString();
                    }
                    else
                    {
                        txtb_otherpremium_no1.Enabled = false;
                        lbl_other_prem1.Text = "(Reserved1):";
                    }

                    if (dt.Rows[0]["other_premium2"].ToString() == "Y")
                    {
                        txtb_otherpremium_no2.Enabled = true;
                        lbl_other_prem2.Text = dt.Rows[0]["descr_other_premium2"].ToString();
                    }
                    else
                    {
                        txtb_otherpremium_no2.Enabled = false;
                        lbl_other_prem2.Text = "(Reserved2):";
                    }

                    if (dt.Rows[0]["other_premium3"].ToString() == "Y")
                    {
                        txtb_otherpremium_no3.Enabled = true;
                        lbl_other_prem3.Text = dt.Rows[0]["descr_other_premium3"].ToString();
                    }
                    else
                    {
                        txtb_otherpremium_no3.Enabled = false;
                        lbl_other_prem3.Text = "(Reserved3):";
                    }
                }
            }

        }

        //*************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_employeename.Text = "";
            txtb_empl_id.Text = "";
            txtb_rate_amount.Text           = "0.00";
            txtb_bir_tax_10percent.Text     = "0.00";
            txtb_bir_tax_15percent.Text     = "0.00";
            txtb_bir_tax_2percent.Text      = "0.00";
            txtb_bir_tax_3percent.Text      = "0.00";
            txtb_bir_tax_5percent.Text      = "0.00";
            txtb_bir_tax_8percent.Text      = "0.00";
            txtb_ccmpc_loan.Text            = "0.00";
            txtb_gross_pay.Text             = "0.00";
            txtb_hdmf_addl.Text             = "0.00";
            txtb_hdmf_mp2.Text              = "0.00";
            txtb_hdmf_cal_loan.Text         = "0.00";
            txtb_hdmf_gs.Text               = "0.00";
            txtb_hdmf_house_loan.Text       = "0.00";
            txtb_hdmf_mpl_loan.Text         = "0.00";
            txtb_hdmf_ps.Text               = "0.00";
            txtb_less.Text                  = "0.00";
            txtb_lates_and_undertime.Text   = "0.00";
            txtb_networkbank_loan.Text      = "0.00";
            txtb_net_pay.Text               = "0.00";
            txtb_net_pay_1h.Text            = "0.00";
            txtb_net_pay_2h.Text            = "0.00";
            txtb_nico_loan.Text             = "0.00";
            txtb_no_days_worked.Text        = "0.00";
            txtb_no_hours_worked.Text       = "0.00";
            txtb_otherloan_no1.Text         = "0.00";
            txtb_otherloan_no2.Text         = "0.00";
            txtb_otherloan_no3.Text         = "0.00";
            txtb_otherpremium_no1.Text      = "0.00";
            txtb_otherpremium_no2.Text      = "0.00";
            txtb_otherpremium_no3.Text      = "0.00";
            txtb_phic_gs.Text               = "0.00";
            txtb_phic_ps.Text               = "0.00";
            txtb_philam.Text                = "0.00";
            txtb_rate_amount.Text           = "0.00";
            txtb_sss.Text                   = "0.00";
            txtb_total_loans.Text           = "0.00";
            txtb_total_mandatory.Text       = "0.00";
            txtb_total_optional.Text        = "0.00";

            txtb_uniform.Text               = "0.00";
            txtb_loyalty_card.Text          = "0.00";
            
            ddl_empl_id.SelectedIndex       = -1;
            txtb_department_name1.Text      = "";

            lbl_rate_basis_descr.Text   = "Rate Basis:";

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
            hidden_rate_basis.Value         = "";
            
            hidden_monthly_rate.Value    = "";
            hidden_daily_rate.Value      = "";
            hidden_hourly_rate.Value     = "";

            Update_days_worked.Update();
            Update_hours_worked.Update();

            // VJA : 2020-10-07 - New Added Textboxes
            txtb_no_absent_2nd.Text         = "0.00";
            txtb_no_absent_1st.Text         = "0.00";
            txtb_no_worked_1st.Text         = "0.00";
            txtb_no_hours_worked_1st.Text   = "0.00";
            txtb_amount_due.Text            = "0.00";
            txtb_amount_due_hidden.Text     = "0.00";

            txtb_no_hours_ot.Text           = "0.00";
            txtb_no_hours_ot_amt.Text       = "0.00";
            txtb_no_hours_sal.Text          = "0.00";
            txtb_no_hours_sal_amt.Text      = "0.00";
            
            txtb_no_hours_ot_010.Text           = "0.00";
            txtb_no_hours_ot_amt_010.Text       = "0.00";
            txtb_no_hours_sal_010.Text          = "0.00";
            txtb_no_hours_sal_amt_010.Text      = "0.00";

            
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

            Update_txtb_no_hours_ot.Update();
            Update_txtb_no_hours_sal.Update();

            Update_lates_and_undertime.Update();
            Update_rate_amount.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable_dtl()
        {
            dtSource_dtl = new DataTable();
            dtSource_dtl.Columns.Add("payroll_year", typeof(System.String));
            dtSource_dtl.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl.Columns.Add("rate_basis", typeof(System.String));
            dtSource_dtl.Columns.Add("days_worked", typeof(System.String));
            dtSource_dtl.Columns.Add("hours_worked", typeof(System.String));
            dtSource_dtl.Columns.Add("monthly_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("daily_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("hourly_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("net_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("lates_mh_amount", typeof(System.String));
            dtSource_dtl.Columns.Add("lates_mins_hrs", typeof(System.String));
            dtSource_dtl.Columns.Add("hdmf_gs", typeof(System.String));
            dtSource_dtl.Columns.Add("hdmf_ps", typeof(System.String));
            dtSource_dtl.Columns.Add("phic_gs", typeof(System.String));
            dtSource_dtl.Columns.Add("phic_ps", typeof(System.String));
            dtSource_dtl.Columns.Add("wtax_2perc", typeof(System.String));
            dtSource_dtl.Columns.Add("wtax_3perc", typeof(System.String));
            dtSource_dtl.Columns.Add("wtax_5perc", typeof(System.String));
            dtSource_dtl.Columns.Add("wtax_8perc", typeof(System.String));
            dtSource_dtl.Columns.Add("wtax_10perc", typeof(System.String));
            dtSource_dtl.Columns.Add("wtax_15perc", typeof(System.String));
            dtSource_dtl.Columns.Add("sss_ps", typeof(System.String));
            dtSource_dtl.Columns.Add("hdmf_ps2", typeof(System.String));
            dtSource_dtl.Columns.Add("hdmf_mp2", typeof(System.String));
            dtSource_dtl.Columns.Add("philamlife_ps", typeof(System.String));
            dtSource_dtl.Columns.Add("other_premium1", typeof(System.String));
            dtSource_dtl.Columns.Add("other_premium2", typeof(System.String));
            dtSource_dtl.Columns.Add("other_premium3", typeof(System.String));
            dtSource_dtl.Columns.Add("hdmf_mpl_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("hdmf_hse_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("hdmf_cal_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("nico_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("network_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("ccmpc_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("other_loan1", typeof(System.String));
            dtSource_dtl.Columns.Add("other_loan2", typeof(System.String));
            dtSource_dtl.Columns.Add("other_loan3", typeof(System.String));
            dtSource_dtl.Columns.Add("post_status", typeof(System.String));
            dtSource_dtl.Columns.Add("gross_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("date_posted", typeof(System.String));
            dtSource_dtl.Columns.Add("uniform_amt", typeof(System.String));
            dtSource_dtl.Columns.Add("hdmf_loyalty_card", typeof(System.String));

            //Add Field Again   -- 06/20/2019
            dtSource_dtl.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource_dtl.Columns.Add("created_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("posted_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("created_dttm", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_dttm", typeof(System.String));

            //Add Field Again   -- 2020-10-07
            dtSource_dtl.Columns.Add("nbr_days_absent", typeof(System.String));
            dtSource_dtl.Columns.Add("hours_ot", typeof(System.String));
            dtSource_dtl.Columns.Add("hours_sal", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys_dtl()
        {
            dtSource_dtl.TableName = "payrollregistry_dtl_jo_tbl";
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr", "empl_id" };
            dtSource_dtl = MyCmn.AddPrimaryKeys(dtSource_dtl, col);
            dtSource_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow_dtl()
        {
            DataRow nrow = dtSource_dtl.NewRow();
            nrow["payroll_year"]            = string.Empty;
            nrow["payroll_registry_nbr"]    = string.Empty;
            nrow["empl_id"]                 = string.Empty;
            nrow["rate_basis"]              = string.Empty;
            nrow["days_worked"]             = string.Empty;
            nrow["hours_worked"]            = string.Empty;
            nrow["monthly_rate"]            = string.Empty;
            nrow["daily_rate"]              = string.Empty;
            nrow["hourly_rate"]             = string.Empty;
            nrow["net_pay"]                 = string.Empty;
            nrow["lates_mh_amount"]         = string.Empty;
            nrow["lates_mins_hrs"]          = string.Empty;
            nrow["hdmf_gs"]                 = string.Empty;
            nrow["hdmf_ps"]                 = string.Empty;
            nrow["phic_gs"]                 = string.Empty;
            nrow["phic_ps"]                 = string.Empty;
            nrow["wtax_2perc"]              = string.Empty;
            nrow["wtax_3perc"]              = string.Empty;
            nrow["wtax_5perc"]              = string.Empty;
            nrow["wtax_8perc"]              = string.Empty;
            nrow["wtax_10perc"]             = string.Empty;
            nrow["wtax_15perc"]             = string.Empty;
            nrow["sss_ps"]                  = string.Empty;
            nrow["hdmf_ps2"]                = string.Empty;
            nrow["hdmf_mp2"]                = string.Empty;
            nrow["philamlife_ps"]           = string.Empty;
            nrow["other_premium1"]          = string.Empty;
            nrow["other_premium2"]          = string.Empty;
            nrow["other_premium3"]          = string.Empty;
            nrow["hdmf_mpl_ln"]             = string.Empty;
            nrow["hdmf_hse_ln"]             = string.Empty;
            nrow["hdmf_cal_ln"]             = string.Empty;
            nrow["nico_ln"]                 = string.Empty;
            nrow["network_ln"]              = string.Empty;
            nrow["ccmpc_ln"]                = string.Empty;
            nrow["other_loan1"]             = string.Empty;
            nrow["other_loan2"]             = string.Empty;
            nrow["other_loan3"]             = string.Empty;
            nrow["post_status"]             = string.Empty;
            nrow["gross_pay"]               = string.Empty;
            nrow["date_posted"]             = string.Empty;
            nrow["uniform_amt"]             = string.Empty;
            nrow["hdmf_loyalty_card"]       = string.Empty;

            //Add Field Again   -- 06/20/2019
            nrow["voucher_nbr"]             = string.Empty;
            nrow["created_by_user"]         = string.Empty;
            nrow["updated_by_user"]         = string.Empty;
            nrow["posted_by_user"]          = string.Empty;
            nrow["created_dttm"]            = string.Empty;
            nrow["updated_dttm"]            = string.Empty;

            //Add Field Again   -- 2020-10-07
            nrow["nbr_days_absent"]         = string.Empty;
            nrow["hours_ot"]                = string.Empty;
            nrow["hours_sal"]                = string.Empty;

            nrow["action"]                  = 1;
            nrow["retrieve"]                = false;
            dtSource_dtl.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year = commandArgs[2];
            txtb_reason.Text = "";
            FieldValidationColorChanged(false, "ALL");
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
        //  BEGIN - VJA- 05/25/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "'";

            if (Session["ep_post_authority"].ToString() == "0")
            {
                // Non-Accounting Delete 
                MyCmn.DeleteBackEndData("payrollregistry_dtl_jo_tbl", "WHERE " + deleteExpression);
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

                    //4.4.b.Update the following fields: From payrollregistry_dtl_jo_tbl Table
                    //    1.voucher_nbr     =   { blank}
                    //    2.posted_by_user  =   { blank}
                    //    3.post_status     =   "N"   
                    //    4.date_posted     =   { blank}
                    //    5.updated_by_user =   Session User ID
                    //    6.updated_dttm    =   System Date

                    string setparams = "";
                    setparams = "voucher_nbr = '',posted_by_user = '',post_status='N',date_posted='',updated_by_user= '" + Session["ep_user_id"].ToString() + "',updated_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    MyCmn.UpdateTable("payrollregistry_dtl_jo_tbl", setparams, "WHERE " + deleteExpression);
                    RetrieveDataListGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
                }
            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            editaddmodal(e.CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Edit Row selection that will trigger edit page 
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

            ClearEntry();
            //initialize table for saving in payrollregistry_jo_dtl_tbl
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();
            dtSource_dtl.Rows[0]["action"] = 2;
            dtSource_dtl.Rows[0]["retrieve"] = true;
            
            RetrieveGetPremAndOther_flag();
            
            txtb_department_name1.Text  = row2Edit[0]["department_name1"].ToString();
            txtb_empl_id.Text           = svalues[0].ToString().Trim();
            txtb_employeename.Text      = row2Edit[0]["employee_name"].ToString();

            txtb_employeename.Visible   = true;
            ddl_empl_id.Visible         = false;
            chkbox_continue.Visible     = false;
            chkbox_continue.Checked     = false;

            txtb_bir_tax_10percent.Text         = row2Edit[0]["wtax_10perc"].ToString().Trim();
            txtb_bir_tax_15percent.Text         = row2Edit[0]["wtax_15perc"].ToString().Trim();
            txtb_bir_tax_2percent.Text          = row2Edit[0]["wtax_2perc"].ToString().Trim();
            txtb_bir_tax_3percent.Text          = row2Edit[0]["wtax_3perc"].ToString().Trim();
            txtb_bir_tax_5percent.Text          = row2Edit[0]["wtax_5perc"].ToString().Trim();
            txtb_bir_tax_8percent.Text          = row2Edit[0]["wtax_8perc"].ToString().Trim();
            txtb_ccmpc_loan.Text                = row2Edit[0]["ccmpc_ln"].ToString().Trim();
            txtb_hdmf_addl.Text                 = row2Edit[0]["hdmf_ps2"].ToString().Trim();
            txtb_hdmf_mp2.Text                  = row2Edit[0]["hdmf_mp2"].ToString().Trim();
            txtb_hdmf_cal_loan.Text             = row2Edit[0]["hdmf_cal_ln"].ToString().Trim();
            txtb_hdmf_gs.Text                   = row2Edit[0]["hdmf_gs"].ToString().Trim();
            txtb_hdmf_house_loan.Text           = row2Edit[0]["hdmf_hse_ln"].ToString().Trim();
            txtb_hdmf_mpl_loan.Text             = row2Edit[0]["hdmf_mpl_ln"].ToString().Trim();
            txtb_hdmf_ps.Text                   = row2Edit[0]["hdmf_ps"].ToString().Trim();
            txtb_less.Text                      = double.Parse(row2Edit[0]["lates_mh_amount"].ToString().Trim()).ToString("###,##0.00");
            txtb_lates_and_undertime.Text       = row2Edit[0]["lates_mins_hrs"].ToString().Trim();
            txtb_networkbank_loan.Text          = row2Edit[0]["network_ln"].ToString().Trim();
            txtb_net_pay.Text                   = double.Parse(row2Edit[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");
            txtb_nico_loan.Text                 = row2Edit[0]["nico_ln"].ToString().Trim();
            txtb_no_days_worked.Text            = row2Edit[0]["days_worked"].ToString().Trim();
            txtb_no_hours_worked.Text           = row2Edit[0]["hours_worked"].ToString().Trim();
            Update_days_worked.Update();
            Update_hours_worked.Update();
            txtb_otherloan_no1.Text             = row2Edit[0]["other_loan1"].ToString().Trim();
            txtb_otherloan_no2.Text             = row2Edit[0]["other_loan2"].ToString().Trim();
            txtb_otherloan_no3.Text             = row2Edit[0]["other_loan3"].ToString().Trim();

            txtb_otherpremium_no1.Text          = row2Edit[0]["other_premium1"].ToString().Trim();
            txtb_otherpremium_no2.Text          = row2Edit[0]["other_premium2"].ToString().Trim();
            txtb_otherpremium_no3.Text          = row2Edit[0]["other_premium3"].ToString().Trim();

            txtb_phic_gs.Text                   = row2Edit[0]["phic_gs"].ToString().Trim();
            txtb_phic_ps.Text                   = row2Edit[0]["phic_ps"].ToString().Trim();

            txtb_philam.Text                    = row2Edit[0]["philamlife_ps"].ToString().Trim();

            txtb_sss.Text                       = row2Edit[0]["sss_ps"].ToString().Trim();
            txtb_uniform.Text                   = row2Edit[0]["uniform_amt"].ToString().Trim();
            txtb_loyalty_card.Text              = row2Edit[0]["hdmf_loyalty_card"].ToString().Trim();

            dtSource_dtl.Rows[0]["post_status"] = row2Edit[0]["post_status"].ToString().Trim();
            dtSource_dtl.Rows[0]["date_posted"] = row2Edit[0]["date_posted"].ToString().Trim();


            hidden_monthly_rate.Value          = row2Edit[0]["monthly_rate"].ToString().Trim();
            hidden_daily_rate.Value            = row2Edit[0]["daily_rate"].ToString().Trim();
            hidden_hourly_rate.Value           = row2Edit[0]["hourly_rate"].ToString().Trim();

            // Date Update : 01-09-202
            // Date By     : JADE

            hidden_rate_basis.Value = row2Edit[0]["rate_basis"].ToString().Trim();

            switch (row2Edit[0]["rate_basis"].ToString().Trim())
            {
                case "H":
                    txtb_rate_amount.Text = row2Edit[0]["hourly_rate"].ToString().Trim();
                    lbl_rate_basis_descr.Text = "Hourly Rate:";
                    break;
                case "D":
                    txtb_rate_amount.Text = row2Edit[0]["daily_rate"].ToString().Trim();
                    lbl_rate_basis_descr.Text = "Daily Rate:";
                    break;
                case "M":
                    txtb_rate_amount.Text = row2Edit[0]["monthly_rate"].ToString().Trim();
                    lbl_rate_basis_descr.Text = "Monthly Rate:";
                    break;

            }
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

            CheckIfNotEqualto_AmountDue();
            calculate_total_mandatory();
            calculate_total_loans();
            calculate_total_optional();
            if (row2Edit[0]["post_status"].ToString().ToUpper() == "Y")
            {
                lbl_if_dateposted_yes.Text = "This Payroll is already Posted You connot Edit!";
                btnSave.Visible = false;
            }
            else { lbl_if_dateposted_yes.Text = ""; btnSave.Visible = true; }
            txtb_gross_pay.Text = double.Parse(row2Edit[0]["gross_pay"].ToString().Trim()).ToString("###,##0.00");
            Calculate_AmountDue();

            LabelAddEdit.Text = "Edit Record | Registry No : " + lbl_registry_number.Text;
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ddl_empl_id.Enabled = false;
            FieldValidationColorChanged(false, "ALL");
            
            // Add Field Again - 06/20/2019
            txtb_voucher_nbr.Text           = row2Edit[0]["voucher_nbr"].ToString();
            ViewState["created_by_user"]    = row2Edit[0]["created_by_user"].ToString();
            ViewState["updated_by_user"]    = row2Edit[0]["updated_by_user"].ToString();
            ViewState["posted_by_user"]     = row2Edit[0]["posted_by_user"].ToString();
            ViewState["created_dttm"]       = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            ViewState["updated_dttm"]       = Convert.ToDateTime(row2Edit[0]["updated_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            //ddl_status.SelectedValue        = row2Edit[0]["post_status"].ToString();
            txtb_status.Text                = row2Edit[0]["post_status_descr"].ToString();
            txtb_date_posted.Text           = row2Edit[0]["date_posted"].ToString();
            txtb_position.Text              = row2Edit[0]["position_title1"].ToString();

            // VJA : BEGIN - 2020-10-07 - New Added Textboxes
            txtb_no_absent_2nd.Text         = row2Edit[0]["nbr_days_absent"].ToString();
            txtb_no_absent_1st.Text         = row2Edit[0]["nbr_days_absent_010"].ToString();
            txtb_no_worked_1st.Text         = row2Edit[0]["days_worked_010"].ToString();
            txtb_no_hours_worked_1st.Text   = row2Edit[0]["hours_worked_010"].ToString();
            // VJA : END -  2020-10-07 - New Added Textboxes
            
            txtb_no_hours_ot.Text           = row2Edit[0]["hours_ot"].ToString();
            txtb_no_hours_sal.Text          = row2Edit[0]["hours_sal"].ToString();

            txtb_no_hours_ot_010.Text       = row2Edit[0]["hours_ot_010"].ToString();
            txtb_no_hours_sal_010.Text      = row2Edit[0]["hours_sal_010"].ToString();

            Calculate_Hours_OT();

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
                btnSave.Text = "Post to Card";
                txtb_voucher_nbr.Enabled = true;
                txtb_date_posted.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }
            // IF the Post Status is Released 
            else if (row2Edit[0]["post_status"].ToString()   == "R"
                    || row2Edit[0]["post_status"].ToString() == "X"
                    )
            {
                // For Accounting or Other User With Y Status
                btnSave.Visible = false;
                btn_calculate.Visible = false;
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
                btn_calculate.Visible = true;
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
        //  BEGIN - VJA- 05/25/2019 - Change Field Sort mode  
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
        //  BEGIN - VJA- 05/25/2019 - Get Grid current sort order 
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
        //  BEGIN - VJA- 05/25/2019 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            string saveRecord           = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate   = string.Empty;
            
            if (IsDataValidated())
            {
                // **************************************************************************
                // ******2021-07-01 -  Not equal to Hidden Gross - Lates Amount *************
                // **************************************************************************
                if (double.Parse(txtb_amount_due.Text) != double.Parse(txtb_amount_due_hidden.Text))
                {
                    FieldValidationColorChanged(true, "not-equal-to-amount-due");
                    return;
                }
                
                // Calculate ALL Computation
                calculate_grossamount();
                Calculate_Absent();
                calculate_total_loans();
                calculate_total_mandatory();
                calculate_total_optional();
                calculate_netpay();
                // Calculate ALL Computation

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    
                    dtSource_dtl.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["empl_id"]                 = ddl_empl_id.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["rate_basis"]              = hidden_rate_basis.Value;
                    dtSource_dtl.Rows[0]["days_worked"]             = txtb_no_days_worked.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hours_worked"]            = txtb_no_hours_worked.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["monthly_rate"]            =  hidden_monthly_rate.Value; 
                    dtSource_dtl.Rows[0]["daily_rate"]              =  hidden_daily_rate.Value;
                    dtSource_dtl.Rows[0]["hourly_rate"]             =  hidden_hourly_rate.Value;
                    dtSource_dtl.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["lates_mins_hrs"]          = txtb_lates_and_undertime.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["lates_mh_amount"]         = txtb_less.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_gs"]                 = txtb_hdmf_gs.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_ps"]                 = txtb_hdmf_ps.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["phic_gs"]                 = txtb_phic_gs.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["phic_ps"]                 = txtb_phic_ps.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_2perc"]              = txtb_bir_tax_2percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_3perc"]              = txtb_bir_tax_3percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_5perc"]              = txtb_bir_tax_5percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_8perc"]              = txtb_bir_tax_8percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_10perc"]             = txtb_bir_tax_10percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_15perc"]             = txtb_bir_tax_15percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["sss_ps"]                  = txtb_sss.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_ps2"]                = txtb_hdmf_addl.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_mp2"]                = txtb_hdmf_mp2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["philamlife_ps"]           = txtb_philam.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_premium1"]          = txtb_otherpremium_no1.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_premium2"]          = txtb_otherpremium_no2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_premium3"]          = txtb_otherpremium_no3.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_mpl_ln"]             = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_hse_ln"]             = txtb_hdmf_house_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_cal_ln"]             = txtb_hdmf_cal_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["network_ln"]              = txtb_networkbank_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_loan1"]             = txtb_otherloan_no1.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_loan2"]             = txtb_otherloan_no2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_loan3"]             = txtb_otherloan_no3.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["uniform_amt"]             = txtb_uniform.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_loyalty_card"]       = txtb_loyalty_card.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["post_status"]             = "N";
                    dtSource_dtl.Rows[0]["voucher_nbr"]             = txtb_voucher_nbr.Text.ToString();
                    dtSource_dtl.Rows[0]["created_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource_dtl.Rows[0]["updated_by_user"]         = "";
                    dtSource_dtl.Rows[0]["created_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource_dtl.Rows[0]["updated_dttm"]            = "";
                    dtSource_dtl.Rows[0]["posted_by_user"]          = "";
                    dtSource_dtl.Rows[0]["date_posted"]             = "";
                    dtSource_dtl.Rows[0]["nbr_days_absent"]         = txtb_no_absent_2nd.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hours_ot"]                = txtb_no_hours_ot.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hours_sal"]               = txtb_no_hours_sal.Text.ToString().Trim();
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource_dtl);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource_dtl.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["rate_basis"]              = hidden_rate_basis.Value;
                    dtSource_dtl.Rows[0]["days_worked"]             = txtb_no_days_worked.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hours_worked"]            = txtb_no_hours_worked.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["monthly_rate"]            = hidden_monthly_rate.Value;  
                    dtSource_dtl.Rows[0]["daily_rate"]              = hidden_daily_rate.Value;
                    dtSource_dtl.Rows[0]["hourly_rate"]             = hidden_hourly_rate.Value;
                    dtSource_dtl.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["lates_mins_hrs"]          = txtb_lates_and_undertime.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["lates_mh_amount"]         = txtb_less.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_gs"]                 = txtb_hdmf_gs.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_ps"]                 = txtb_hdmf_ps.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["phic_gs"]                 = txtb_phic_gs.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["phic_ps"]                 = txtb_phic_ps.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_2perc"]              = txtb_bir_tax_2percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_3perc"]              = txtb_bir_tax_3percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_5perc"]              = txtb_bir_tax_5percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_8perc"]              = txtb_bir_tax_8percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_10perc"]             = txtb_bir_tax_10percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["wtax_15perc"]             = txtb_bir_tax_15percent.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["sss_ps"]                  = txtb_sss.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_ps2"]                = txtb_hdmf_addl.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_mp2"]                = txtb_hdmf_mp2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["philamlife_ps"]           = txtb_philam.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_premium1"]          = txtb_otherpremium_no1.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_premium2"]          = txtb_otherpremium_no2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_premium3"]          = txtb_otherpremium_no3.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_mpl_ln"]             = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_hse_ln"]             = txtb_hdmf_house_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_cal_ln"]             = txtb_hdmf_cal_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["network_ln"]              = txtb_networkbank_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_loan1"]             = txtb_otherloan_no1.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_loan2"]             = txtb_otherloan_no2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_loan3"]             = txtb_otherloan_no3.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["uniform_amt"]             = txtb_uniform.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hdmf_loyalty_card"]       = txtb_loyalty_card.Text.ToString().Trim();
                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource_dtl.Rows[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                    dtSource_dtl.Rows[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource_dtl.Rows[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource_dtl.Rows[0]["created_dttm"]            = ViewState["created_dttm"].ToString();
                    dtSource_dtl.Rows[0]["nbr_days_absent"]         = txtb_no_absent_2nd.Text.ToString().Trim();

                    if (Session["ep_post_authority"].ToString() == "1")
                    {
                        dtSource_dtl.Rows[0]["posted_by_user"]  = Session["ep_user_id"].ToString();
                        dtSource_dtl.Rows[0]["date_posted"]     = txtb_date_posted.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["post_status"]     = "Y";
                        dtSource_dtl.Rows[0]["voucher_nbr"]     = txtb_voucher_nbr.Text.ToString();
                        dtSource_dtl.Rows[0]["updated_by_user"] = ViewState["updated_by_user"].ToString();
                        dtSource_dtl.Rows[0]["updated_dttm"]    = ViewState["updated_dttm"].ToString();
                    }
                    dtSource_dtl.Rows[0]["hours_ot"]    = txtb_no_hours_ot.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hours_sal"]   = txtb_no_hours_sal.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource_dtl);
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
                        nrow["payroll_registry_nbr"]        = lbl_registry_number.Text.ToString().Trim();
                        nrow["empl_id"]                     = ddl_empl_id.SelectedValue.ToString().Trim();
                        nrow["employee_name"]               = ddl_empl_id.SelectedItem.Text.ToString().Trim();
                        nrow["rate_basis"]                  = hidden_rate_basis.Value;
                        nrow["days_worked"]                 = txtb_no_days_worked.Text.ToString().Trim();
                        nrow["hours_worked"]                = txtb_no_hours_worked.Text.ToString().Trim();
                        nrow["monthly_rate"]                = hidden_monthly_rate.Value; 
                        nrow["daily_rate"]                  = hidden_daily_rate.Value;
                        nrow["hourly_rate"]                 = hidden_hourly_rate.Value;
                        nrow["net_pay"]                     = txtb_net_pay.Text.ToString().Trim();
                        nrow["lates_mins_hrs"]              = txtb_lates_and_undertime.Text.ToString().Trim();
                        nrow["lates_mh_amount"]             = txtb_less.Text.ToString().Trim();
                        nrow["hdmf_gs"]                     = txtb_hdmf_gs.Text.ToString().Trim();
                        nrow["hdmf_ps"]                     = txtb_hdmf_ps.Text.ToString().Trim();
                        nrow["phic_gs"]                     = txtb_phic_gs.Text.ToString().Trim();
                        nrow["phic_ps"]                     = txtb_phic_ps.Text.ToString().Trim();
                        nrow["wtax_2perc"]                  = txtb_bir_tax_2percent.Text.ToString().Trim();
                        nrow["wtax_3perc"]                  = txtb_bir_tax_3percent.Text.ToString().Trim();
                        nrow["wtax_5perc"]                  = txtb_bir_tax_5percent.Text.ToString().Trim();
                        nrow["wtax_8perc"]                  = txtb_bir_tax_8percent.Text.ToString().Trim();
                        nrow["wtax_10perc"]                 = txtb_bir_tax_10percent.Text.ToString().Trim();
                        nrow["wtax_15perc"]                 = txtb_bir_tax_15percent.Text.ToString().Trim();
                        nrow["sss_ps"]                      = txtb_sss.Text.ToString().Trim();
                        nrow["hdmf_ps2"]                    = txtb_hdmf_addl.Text.ToString().Trim();
                        nrow["hdmf_mp2"]                    = txtb_hdmf_mp2.Text.ToString().Trim();
                        nrow["philamlife_ps"]               = txtb_philam.Text.ToString().Trim();
                        nrow["other_premium1"]              = txtb_otherpremium_no1.Text.ToString().Trim();
                        nrow["other_premium2"]              = txtb_otherpremium_no2.Text.ToString().Trim();
                        nrow["other_premium3"]              = txtb_otherpremium_no3.Text.ToString().Trim();
                        nrow["hdmf_mpl_ln"]                 = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                        nrow["hdmf_hse_ln"]                 = txtb_hdmf_house_loan.Text.ToString().Trim();
                        nrow["hdmf_cal_ln"]                 = txtb_hdmf_cal_loan.Text.ToString().Trim();
                        nrow["nico_ln"]                     = txtb_nico_loan.Text.ToString().Trim();
                        nrow["network_ln"]                  = txtb_networkbank_loan.Text.ToString().Trim();
                        nrow["ccmpc_ln"]                    = txtb_ccmpc_loan.Text.ToString().Trim();
                        nrow["other_loan1"]                 = txtb_otherloan_no1.Text.ToString().Trim();
                        nrow["other_loan2"]                 = txtb_otherloan_no2.Text.ToString().Trim();
                        nrow["other_loan3"]                 = txtb_otherloan_no3.Text.ToString().Trim();
                        nrow["gross_pay"]                   = txtb_gross_pay.Text.ToString().Trim();
                        nrow["uniform_amt"]                 = txtb_uniform.Text.ToString().Trim();
                        nrow["hdmf_loyalty_card"]           = txtb_loyalty_card.Text.ToString().Trim();
                        nrow["department_name1"]            = txtb_department_name1.Text.ToString().Trim();
                        nrow["post_status"]                 = "N";
                        nrow["post_status_descr"]           = "NOT POSTED";
                        nrow["voucher_nbr"]                 = txtb_voucher_nbr.Text.ToString();
                        nrow["created_by_user"]             = Session["ep_user_id"].ToString();
                        nrow["updated_by_user"]             = "";
                        nrow["created_dttm"]                = DateTime.Now;
                        nrow["updated_dttm"]                = Convert.ToDateTime("1900-01-01");
                        nrow["posted_by_user"]              = "";
                        nrow["date_posted"]                 = "";
                        nrow["position_title1"]             = txtb_position.Text.ToString();
                        nrow["hours_ot"]                    = txtb_no_hours_ot.Text.ToString().Trim();
                        nrow["hours_sal"]                   = txtb_no_hours_sal.Text.ToString().Trim();
                        nrow["nbr_days_absent"]             = txtb_no_absent_2nd.Text.ToString().Trim();
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
                        nrow["hours_ot_010"]          = txtb_no_hours_ot_010.Text;
                        nrow["hours_sal_010"]         = txtb_no_hours_sal_010.Text;

                        dataListGrid.Rows.Add(nrow);
                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND payroll_year='" + ddl_year.SelectedValue.ToString().Trim() + "' AND payroll_registry_nbr ='" + lbl_registry_number.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_registry_nbr"] = lbl_registry_number.Text.ToString().Trim();
                        row2Edit[0]["empl_id"]              = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employee_name"]        = txtb_employeename.Text.ToString().Trim();
                        row2Edit[0]["rate_basis"]           = hidden_rate_basis.Value;
                        row2Edit[0]["days_worked"]          = txtb_no_days_worked.Text.ToString().Trim();
                        row2Edit[0]["hours_worked"]         = txtb_no_hours_worked.Text.ToString().Trim();
                        row2Edit[0]["monthly_rate"]         = hidden_monthly_rate.Value;  
                        row2Edit[0]["daily_rate"]           = hidden_daily_rate.Value;
                        row2Edit[0]["hourly_rate"]          = hidden_hourly_rate.Value;
                        row2Edit[0]["net_pay"]              = txtb_net_pay.Text.ToString().Trim();
                        row2Edit[0]["lates_mins_hrs"]       = txtb_lates_and_undertime.Text.ToString().Trim();
                        row2Edit[0]["lates_mh_amount"]      = txtb_less.Text.ToString().Trim();
                        row2Edit[0]["hdmf_gs"]              = txtb_hdmf_gs.Text.ToString().Trim();
                        row2Edit[0]["hdmf_ps"]              = txtb_hdmf_ps.Text.ToString().Trim();
                        row2Edit[0]["phic_gs"]              = txtb_phic_gs.Text.ToString().Trim();
                        row2Edit[0]["phic_ps"]              = txtb_phic_ps.Text.ToString().Trim();
                        row2Edit[0]["wtax_2perc"]           = txtb_bir_tax_2percent.Text.ToString().Trim();
                        row2Edit[0]["wtax_3perc"]           = txtb_bir_tax_3percent.Text.ToString().Trim();
                        row2Edit[0]["wtax_5perc"]           = txtb_bir_tax_5percent.Text.ToString().Trim();
                        row2Edit[0]["wtax_8perc"]           = txtb_bir_tax_8percent.Text.ToString().Trim();
                        row2Edit[0]["wtax_10perc"]          = txtb_bir_tax_10percent.Text.ToString().Trim();
                        row2Edit[0]["wtax_15perc"]          = txtb_bir_tax_15percent.Text.ToString().Trim();
                        row2Edit[0]["sss_ps"]               = txtb_sss.Text.ToString().Trim();
                        row2Edit[0]["hdmf_ps2"]             = txtb_hdmf_addl.Text.ToString().Trim();
                        row2Edit[0]["hdmf_mp2"]             = txtb_hdmf_mp2.Text.ToString().Trim();
                        row2Edit[0]["philamlife_ps"]        = txtb_philam.Text.ToString().Trim();
                        row2Edit[0]["other_premium1"]       = txtb_otherpremium_no1.Text.ToString().Trim();
                        row2Edit[0]["other_premium2"]       = txtb_otherpremium_no2.Text.ToString().Trim();
                        row2Edit[0]["other_premium3"]       = txtb_otherpremium_no3.Text.ToString().Trim();
                        row2Edit[0]["hdmf_mpl_ln"]          = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                        row2Edit[0]["hdmf_hse_ln"]          = txtb_hdmf_house_loan.Text.ToString().Trim();
                        row2Edit[0]["hdmf_cal_ln"]          = txtb_hdmf_cal_loan.Text.ToString().Trim();
                        row2Edit[0]["nico_ln"]              = txtb_nico_loan.Text.ToString().Trim();
                        row2Edit[0]["network_ln"]           = txtb_networkbank_loan.Text.ToString().Trim();
                        row2Edit[0]["ccmpc_ln"]             = txtb_ccmpc_loan.Text.ToString().Trim();
                        row2Edit[0]["other_loan1"]          = txtb_otherloan_no1.Text.ToString().Trim();
                        row2Edit[0]["other_loan2"]          = txtb_otherloan_no2.Text.ToString().Trim();
                        row2Edit[0]["other_loan3"]          = txtb_otherloan_no3.Text.ToString().Trim();
                        row2Edit[0]["gross_pay"]            = txtb_gross_pay.Text.ToString().Trim();
                        row2Edit[0]["uniform_amt"]          = txtb_uniform.Text.ToString().Trim();
                        row2Edit[0]["hdmf_loyalty_card"]    = txtb_loyalty_card.Text.ToString().Trim();
                        row2Edit[0]["department_name1"]     = txtb_department_name1.Text.ToString().Trim();
                        // BEGIN - Add Field Again  - 06/20/2019
                        row2Edit[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                        row2Edit[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                        row2Edit[0]["created_dttm"]            = ViewState["created_dttm"].ToString() == "" ? DateTime.Now : ViewState["created_dttm"] ;
                        row2Edit[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        row2Edit[0]["nbr_days_absent"]         = txtb_no_absent_2nd.Text.ToString().Trim();
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
                        row2Edit[0]["hours_ot"]                 = txtb_no_hours_ot.Text.ToString().Trim();
                        row2Edit[0]["hours_sal"]                 = txtb_no_hours_sal.Text.ToString().Trim();

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
                        row2Edit[0]["hours_ot_010"]          = txtb_no_hours_ot_010.Text;
                        row2Edit[0]["hours_sal_010"]         = txtb_no_hours_sal_010.Text;

                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text                    = MyCmn.CONST_EDITREC;
                    }
                    //RetrieveDataListGrid();
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
                + "%' OR position_title1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR post_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR payroll_year LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR payroll_registry_nbr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR net_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
           
            DataRow[] rows = dataListGrid.Select(searchExpression);
            dtSource1.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource1.ImportRow(row);
                }
            }

            gv_dataListGrid.DataSource          = dtSource1;
            gv_dataListGrid.DataBind();
            up_dataListGrid.Update();
            txtb_search.Attributes["onfocus"]   = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search.Focus();
            show_pagesx.Text                    = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 05/25/2019 - Check if Object already contains value  
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
        //  BEGIN - VJA- 05/25/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated2()
        {
            bool validatedSaved = true;
            if (ddl_empl_id.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }

            return validatedSaved;
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            int target_tab = 1;
            FieldValidationColorChanged(false, "ALL");
            if (ddl_empl_id.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_lates_and_undertime) == false)
            {
                if (txtb_lates_and_undertime.Text.ToString().Trim() == "")
                {
                    txtb_lates_and_undertime.Text = "0";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_lates_and_undertime");
                    txtb_lates_and_undertime.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_no_days_worked) == false)
            {
                if (txtb_no_days_worked.Text.ToString().Trim() == "")
                {
                    txtb_no_days_worked.Text = "0";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_no_days_worked");
                    txtb_no_days_worked.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_no_hours_worked) == false)
            {
                if (txtb_no_hours_worked.Text.ToString().Trim() == "")
                {
                    txtb_no_hours_worked.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_no_hours_worked");
                    txtb_no_hours_worked.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_no_hours_ot) == false)
            {
                if (txtb_no_hours_ot.Text.ToString().Trim() == "")
                {
                    txtb_no_hours_ot.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_no_hours_ot");
                    txtb_no_hours_ot.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_no_hours_sal) == false)
            {
                if (txtb_no_hours_sal.Text.ToString().Trim() == "")
                {
                    txtb_no_hours_sal.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_no_hours_sal");
                    txtb_no_hours_sal.Focus();
                    validatedSaved = false;
                }
            }
            //TARGET TAB = 1
            if (CommonCode.checkisdecimal(txtb_hdmf_gs) == false)
            {
                if (txtb_hdmf_gs.Text.ToString().Trim() == "")
                {
                    txtb_hdmf_gs.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_hdmf_gs");
                    txtb_hdmf_gs.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_hdmf_ps) == false)
            {
                if (txtb_hdmf_ps.Text.ToString().Trim() == "")
                {
                    txtb_hdmf_ps.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_hdmf_ps");
                    txtb_hdmf_ps.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_phic_gs) == false)
            {
                if (txtb_phic_gs.Text.ToString().Trim() == "")
                {
                    txtb_phic_gs.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_phic_gs");
                    txtb_phic_gs.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_phic_ps) == false)
            {
                if (txtb_phic_ps.Text.ToString().Trim() == "")
                {
                    txtb_phic_ps.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_phic_ps");
                    txtb_phic_ps.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_bir_tax_2percent) == false)
            {
                if (txtb_bir_tax_2percent.Text.ToString().Trim() == "")
                {
                    txtb_bir_tax_2percent.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_bir_tax_2percent");
                    txtb_bir_tax_2percent.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_bir_tax_3percent) == false)
            {
                if (txtb_bir_tax_3percent.Text.ToString().Trim() == "")
                {
                    txtb_bir_tax_3percent.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_bir_tax_3percent");
                    txtb_bir_tax_3percent.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_bir_tax_5percent) == false)
            {
                if (txtb_bir_tax_5percent.Text.ToString().Trim() == "")
                {
                    txtb_bir_tax_5percent.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_bir_tax_5percent");
                    txtb_bir_tax_5percent.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_bir_tax_8percent) == false)
            {
                if (txtb_bir_tax_8percent.Text.ToString().Trim() == "")
                {
                    txtb_bir_tax_8percent.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_bir_tax_8percent");
                    txtb_bir_tax_8percent.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_bir_tax_10percent) == false)
            {
                if (txtb_bir_tax_10percent.Text.ToString().Trim() == "")
                {
                    txtb_bir_tax_10percent.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_bir_tax_10percent");
                    txtb_bir_tax_10percent.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(txtb_bir_tax_15percent) == false)
            {
                if (txtb_bir_tax_15percent.Text.ToString().Trim() == "")
                {
                    txtb_bir_tax_15percent.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_bir_tax_15percent");
                    txtb_bir_tax_15percent.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            //TARGET TAB = 2
            if (CommonCode.checkisdecimal(txtb_sss) == false)
            {
                if (txtb_sss.Text.ToString().Trim() == "")
                {
                    txtb_sss.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_sss");
                    txtb_sss.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(txtb_hdmf_addl) == false)
            {
                if (txtb_hdmf_addl.Text.ToString().Trim() == "")
                {
                    txtb_hdmf_addl.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_hdmf_addl");
                    txtb_hdmf_addl.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(txtb_hdmf_mp2) == false)
            {
                if (txtb_hdmf_mp2.Text.ToString().Trim() == "")
                {
                    txtb_hdmf_mp2.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_hdmf_mp2");
                    txtb_hdmf_mp2.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(txtb_philam) == false)
            {
                if (txtb_philam.Text.ToString().Trim() == "")
                {
                    txtb_philam.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_philam");
                    txtb_philam.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(txtb_philam) == false)
            {
                if (txtb_philam.Text.ToString().Trim() == "")
                {
                    txtb_philam.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_philam");
                    txtb_philam.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(txtb_otherpremium_no1) == false)
            {
                if (txtb_otherpremium_no1.Text.ToString().Trim() == "")
                {
                    txtb_otherpremium_no1.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_otherpremium_no1");
                    txtb_otherpremium_no1.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(txtb_otherpremium_no2) == false)
            {
                if (txtb_otherpremium_no2.Text.ToString().Trim() == "")
                {
                    txtb_otherpremium_no2.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_otherpremium_no2");
                    txtb_otherpremium_no2.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(txtb_otherpremium_no3) == false)
            {
                if (txtb_otherpremium_no3.Text.ToString().Trim() == "")
                {
                    txtb_otherpremium_no3.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_otherpremium_no3");
                    txtb_otherpremium_no3.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            //TARGET TAB = 3
            if (CommonCode.checkisdecimal(txtb_hdmf_mpl_loan) == false)
            {
                if (txtb_hdmf_mpl_loan.Text.ToString().Trim() == "")
                {
                    txtb_hdmf_mpl_loan.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_hdmf_mpl_loan");
                    txtb_hdmf_mpl_loan.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_hdmf_house_loan) == false)
            {
                if (txtb_hdmf_house_loan.Text.ToString().Trim() == "")
                {
                    txtb_hdmf_house_loan.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_hdmf_house_loan");
                    txtb_hdmf_house_loan.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_hdmf_cal_loan) == false)
            {
                if (txtb_hdmf_cal_loan.Text.ToString().Trim() == "")
                {
                    txtb_hdmf_cal_loan.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_hdmf_cal_loan");
                    txtb_hdmf_cal_loan.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_ccmpc_loan) == false)
            {
                if (txtb_ccmpc_loan.Text.ToString().Trim() == "")
                {
                    txtb_ccmpc_loan.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_ccmpc_loan");
                    txtb_ccmpc_loan.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_nico_loan) == false)
            {
                if (txtb_nico_loan.Text.ToString().Trim() == "")
                {
                    txtb_nico_loan.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_nico_loan");
                    txtb_nico_loan.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_networkbank_loan) == false)
            {
                if (txtb_networkbank_loan.Text.ToString().Trim() == "")
                {
                    txtb_networkbank_loan.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_networkbank_loan");
                    txtb_networkbank_loan.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_otherloan_no1) == false)
            {
                if (txtb_otherloan_no1.Text.ToString().Trim() == "")
                {
                    txtb_otherloan_no1.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_otherloan_no1");
                    txtb_otherloan_no1.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_otherloan_no2) == false)
            {
                if (txtb_otherloan_no2.Text.ToString().Trim() == "")
                {
                    txtb_otherloan_no2.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_otherloan_no2");
                    txtb_otherloan_no2.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }


            if (CommonCode.checkisdecimal(txtb_otherloan_no3) == false)
            {
                if (txtb_otherloan_no3.Text.ToString().Trim() == "")
                {
                    txtb_otherloan_no3.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_otherloan_no3");
                    txtb_otherloan_no3.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_uniform) == false)
            {
                if (txtb_uniform.Text.ToString().Trim() == "")
                {
                    txtb_uniform.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_uniform");
                    txtb_uniform.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(txtb_loyalty_card) == false)
            {
                if (txtb_loyalty_card.Text.ToString().Trim() == "")
                {
                    txtb_loyalty_card.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_loyalty_card");
                    txtb_loyalty_card.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(txtb_rate_amount) == false)
            {
                if (txtb_rate_amount.Text.ToString().Trim() == "")
                {
                    txtb_rate_amount.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_rate_amount");
                    txtb_rate_amount.Focus();
                    validatedSaved = false;
                }
            }

            // Required ang Vouvher number if Status is Y or Blank ug ang Post Authority is equal to 1 
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_no_absent_2nd) == false)
            {
                if (txtb_no_absent_2nd.Text.ToString().Trim() == "")
                {
                    txtb_no_absent_2nd.Text = "0.000";
                }
                else
                {
                    FieldValidationColorChanged(true, "txtb_no_absent_2nd");
                    txtb_no_absent_2nd.Focus();
                    validatedSaved = false;
                }
            }
            
            if (double.Parse((double.Parse(txtb_no_hours_ot.Text.ToString().Trim()) + double.Parse(txtb_no_hours_sal.Text.ToString().Trim())).ToString("###,##0.00")) != double.Parse(txtb_no_hours_worked.Text.ToString().Trim()))
            {
                FieldValidationColorChanged(true, "not-equal-to-no-of-hours");
                validatedSaved = false;

                Update_hours_worked.Update();
                txtb_no_hours_worked.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                txtb_no_hours_worked.Focus();
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

            if ((int.Parse(ddl_year.SelectedValue) >= 2025 && int.Parse(ddl_month.SelectedValue) >=10) 
              || int.Parse(ddl_year.SelectedValue) >  2025
              )
            {
                if (double.Parse(txtb_no_worked_1st.Text) + double.Parse(txtb_no_days_worked.Text) > 22 && chkbox_continue.Checked == false && ddl_payroll_template.SelectedValue == "011")
                {
                    LblRequired1.Text = "Number of worked days exceeds 22 days (1st =" + txtb_no_worked_1st.Text.ToString() + " day/s, 2nd =" + txtb_no_days_worked.Text.ToString() + "day/s) TOTALS = " + (double.Parse(txtb_no_worked_1st.Text) + double.Parse(txtb_no_days_worked.Text));
                    LblRequired1.Text.ToUpper();
                    ddl_empl_id.BorderColor = Color.Red;
                    validatedSaved = false;
                    chkbox_continue.Visible = true;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClickTab", "click_tab(" + target_tab + ")", true);
            return validatedSaved;
        }


        //**********************************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_empl_id":
                        {
                            LblRequired1.Text                       = MyCmn.CONST_RQDFLD;
                            ddl_empl_id.BorderColor                 = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text                       = "already-exist";
                            ddl_empl_id.BorderColor                 = Color.Red;
                            break;
                        }
                    case "txtb_lates_and_undertime":
                        {
                            LblRequired2.Text                       = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lates_and_undertime.BorderColor    = Color.Red;
                            break;
                        }
                    case "txtb_no_days_worked":
                        {
                            LblRequired3.Text                       = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_days_worked.BorderColor         = Color.Red;
                            break;
                        }
                    case "txtb_no_hours_worked":
                        {
                            LblRequired4.Text                       = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_hours_worked.BorderColor        = Color.Red;
                            break;
                        }
                    case "txtb_gross_pay":
                        {
                            LblRequired5.Text                       = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross_pay.BorderColor              = Color.Red;
                            break;
                        }
                    case "txtb_less":
                        {
                            LblRequired6.Text                       = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_less.BorderColor                   = Color.Red;
                            break;
                        }
                    case "txtb_total_mandatory":
                        {
                            LblRequired7.Text                       = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_mandatory.BorderColor        = Color.Red;
                            break;
                        }
                    case "txtb_total_optional":
                        {
                            LblRequired8.Text                       = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_optional.BorderColor         = Color.Red;
                            break;
                        }
                    case "txtb_total_loans":
                        {
                            LblRequired9.Text                       = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_loans.BorderColor            = Color.Red;
                            break;
                        }
                    case "txtb_net_pay":
                        {
                            LblRequired10.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay.BorderColor                = Color.Red;
                            break;
                        }
                    case "txtb_net_pay_1h":
                        {
                            LblRequired11.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay_1h.BorderColor             = Color.Red;
                            break;
                        }
                    case "txtb_net_pay_2h":
                        {
                            LblRequired12.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay_2h.BorderColor             = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_gs":
                        {
                            LblRequired12.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_gs.BorderColor                = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_ps":
                        {
                            LblRequired13.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_ps.BorderColor                = Color.Red;
                            break;
                        }
                    case "txtb_phic_gs":
                        {
                            LblRequired14.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_phic_gs.BorderColor                = Color.Red;
                            break;
                        }
                    case "txtb_phic_ps":
                        {
                            LblRequired15.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_phic_ps.BorderColor                = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax_2percent":
                        {
                            LblRequired16.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax_2percent.BorderColor       = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax_3percent":
                        {
                            LblRequired17.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax_3percent.BorderColor       = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax_5percent":
                        {
                            LblRequired18.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax_5percent.BorderColor       = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax_8percent":
                        {
                            LblRequired19.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax_8percent.BorderColor       = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax_10percent":
                        {
                            LblRequired20.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax_10percent.BorderColor      = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax_15percent":
                        {
                            LblRequired21.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax_15percent.BorderColor      = Color.Red;
                            break;
                        }
                    case "txtb_sss":
                        {
                            LblRequired22.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_sss.BorderColor                    = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_addl":
                        {
                            LblRequired23.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_addl.BorderColor              = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_mp2":
                        {
                            LblRequired37.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_mp2.BorderColor               = Color.Red;
                            break;
                        }
                    case "txtb_philam":
                        {
                            LblRequired24.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_philam.BorderColor                 = Color.Red;
                            break;
                        }
                    case "txtb_otherpremium_no1":
                        {
                            LblRequired25.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no1.BorderColor       = Color.Red;
                            break;
                        }
                    case "txtb_otherpremium_no2":
                        {
                            LblRequired26.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no2.BorderColor       = Color.Red;
                            break;
                        }
                    case "txtb_otherpremium_no3":
                        {
                            LblRequired27.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no3.BorderColor       = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_mpl_loan":
                        {
                            LblRequired28.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_mpl_loan.BorderColor          = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_house_loan":
                        {
                            LblRequired29.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_house_loan.BorderColor        = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_cal_loan":
                        {
                            LblRequired30.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_cal_loan.BorderColor          = Color.Red;
                            break;
                        }
                    case "txtb_ccmpc_loan":
                        {
                            LblRequired31.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_ccmpc_loan.BorderColor             = Color.Red;
                            break;
                        }
                    case "txtb_nico_loan":
                        {
                            LblRequired32.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nico_loan.BorderColor              = Color.Red;
                            break;
                        }
                    case "txtb_networkbank_loan":
                        {
                            LblRequired33.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_networkbank_loan.BorderColor       = Color.Red;
                            break;
                        }
                    case "txtb_otherloan_no1":
                        {
                            LblRequired34.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no1.BorderColor          = Color.Red;
                            break;
                        }
                    case "txtb_otherloan_no2":
                        {
                            LblRequired35.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no2.BorderColor          = Color.Red;
                            break;
                        }
                    case "txtb_otherloan_no3":
                        {
                            LblRequired36.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no3.BorderColor          = Color.Red;
                            break;
                        }
                    case "txtb_uniform":
                        {
                            LblRequired44.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_uniform.BorderColor          = Color.Red;
                            break;
                        }
                    case "txtb_loyalty_card":
                        {
                            LblRequired45.Text                      = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_loyalty_card.BorderColor          = Color.Red;
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
                    case "txtb_rate_amount":
                        {
                            LblRequired2000.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_rate_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_absent_2nd":
                        {
                            LblRequired_no_absent_2nd.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_absent_2nd.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_hours_ot":
                        {
                            LblRequired_txtb_no_hours_ot.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_hours_ot.BorderColor = Color.Red;
                            Update_txtb_no_hours_ot.Update();
                            break;
                        }
                    case "txtb_no_hours_sal":
                        {
                            LblRequired_txtb_no_hours_sal.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_hours_sal.BorderColor = Color.Red;
                            Update_txtb_no_hours_sal.Update();
                            break;
                        }
                    case "not-equal-to-amount-due":
                        {
                            LblRequired1000.Text = "Please click the Calculate button before you save, Expected Amount due is " + txtb_amount_due_hidden.Text.ToString();
                            break;
                        }
                    case "not-equal-to-no-of-hours":
                        {
                            LblRequired4.Text                   = "No.of Hours(OT) and No.of Hours(Salary) is not equal to No. of Hours Worked: " + txtb_no_hours_worked.Text.ToString();
                            LblRequired_txtb_no_hours_ot.Text   = "No.of Hours(OT) and No.of Hours(Salary) is not equal to No. of Hours Worked: " + txtb_no_hours_worked.Text.ToString();
                            LblRequired_txtb_no_hours_sal.Text  = "No.of Hours(OT) and No.of Hours(Salary) is not equal to No. of Hours Worked: " + txtb_no_hours_worked.Text.ToString();

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
                            LblRequired1000.Text                    = "";
                            LblRequired1.Text                       = "";
                            LblRequired2.Text                       = "";
                            LblRequired3.Text                       = "";
                            LblRequired4.Text                       = "";
                            LblRequired5.Text                       = "";
                            LblRequired6.Text                       = "";
                            LblRequired7.Text                       = "";
                            LblRequired8.Text                       = "";
                            LblRequired9.Text                       = "";
                            LblRequired10.Text                      = "";
                            LblRequired11.Text                      = "";
                            LblRequired12.Text                      = "";
                            LblRequired13.Text                      = "";
                            LblRequired14.Text                      = "";
                            LblRequired15.Text                      = "";
                            LblRequired16.Text                      = "";
                            LblRequired17.Text                      = "";
                            LblRequired18.Text                      = "";
                            LblRequired19.Text                      = "";
                            LblRequired20.Text                      = "";
                            LblRequired21.Text                      = "";
                            LblRequired22.Text                      = "";
                            LblRequired23.Text                      = "";
                            LblRequired24.Text                      = "";
                            LblRequired25.Text                      = "";
                            LblRequired26.Text                      = "";
                            LblRequired27.Text                      = "";
                            LblRequired28.Text                      = "";
                            LblRequired29.Text                      = "";
                            LblRequired30.Text                      = "";
                            LblRequired31.Text                      = "";
                            LblRequired32.Text                      = "";
                            LblRequired33.Text                      = "";
                            LblRequired34.Text                      = "";
                            LblRequired35.Text                      = "";
                            LblRequired36.Text                      = "";
                            LblRequired37.Text                      = "";
                            LblRequired44.Text                      = "";
                            LblRequired45.Text                      = "";
                            LblRequired200.Text                     = "";
                            LblRequired201.Text                     = "";
                            LblRequired2000.Text                     = "";
                            LblRequired_no_absent_2nd.Text                     = "";
                            LblRequired_txtb_no_hours_ot.Text                     = "";
                            LblRequired_txtb_no_hours_sal.Text                     = "";
                            ddl_empl_id.BorderColor                 = Color.LightGray;
                            txtb_bir_tax_10percent.BorderColor      = Color.LightGray;
                            txtb_bir_tax_15percent.BorderColor      = Color.LightGray;
                            txtb_bir_tax_2percent.BorderColor       = Color.LightGray;
                            txtb_bir_tax_3percent.BorderColor       = Color.LightGray;
                            txtb_bir_tax_5percent.BorderColor       = Color.LightGray;
                            txtb_bir_tax_8percent.BorderColor       = Color.LightGray;
                            txtb_ccmpc_loan.BorderColor             = Color.LightGray;
                            txtb_gross_pay.BorderColor              = Color.LightGray;
                            txtb_hdmf_addl.BorderColor              = Color.LightGray;
                            txtb_hdmf_mp2.BorderColor               = Color.LightGray;
                            txtb_hdmf_cal_loan.BorderColor          = Color.LightGray;
                            txtb_hdmf_gs.BorderColor                = Color.LightGray;
                            txtb_hdmf_house_loan.BorderColor        = Color.LightGray;
                            txtb_hdmf_mpl_loan.BorderColor          = Color.LightGray;
                            txtb_hdmf_ps.BorderColor                = Color.LightGray;
                            txtb_lates_and_undertime.BorderColor    = Color.LightGray;
                            txtb_less.BorderColor                   = Color.LightGray;
                            txtb_networkbank_loan.BorderColor       = Color.LightGray;
                            txtb_net_pay.BorderColor                = Color.LightGray;
                            txtb_net_pay_1h.BorderColor             = Color.LightGray;
                            txtb_net_pay_2h.BorderColor             = Color.LightGray;
                            txtb_nico_loan.BorderColor              = Color.LightGray;
                            txtb_no_days_worked.BorderColor         = Color.LightGray;
                            txtb_no_hours_worked.BorderColor        = Color.LightGray;
                            txtb_otherloan_no1.BorderColor          = Color.LightGray;
                            txtb_otherloan_no2.BorderColor          = Color.LightGray;
                            txtb_otherloan_no3.BorderColor          = Color.LightGray;
                            txtb_otherpremium_no1.BorderColor       = Color.LightGray;
                            txtb_otherpremium_no2.BorderColor       = Color.LightGray;
                            txtb_otherpremium_no3.BorderColor       = Color.LightGray;
                            txtb_phic_gs.BorderColor                = Color.LightGray;
                            txtb_phic_ps.BorderColor                = Color.LightGray;
                            txtb_philam.BorderColor                 = Color.LightGray;
                            txtb_rate_amount.BorderColor            = Color.LightGray;
                            txtb_sss.BorderColor                    = Color.LightGray;
                            txtb_total_loans.BorderColor            = Color.LightGray;
                            txtb_total_mandatory.BorderColor        = Color.LightGray;
                            txtb_total_optional.BorderColor         = Color.LightGray;
                            txtb_uniform.BorderColor                = Color.LightGray;
                            txtb_loyalty_card.BorderColor           = Color.LightGray;
                            txtb_voucher_nbr.BorderColor            = Color.LightGray;
                            txtb_reason.BorderColor                 = Color.LightGray;
                            txtb_rate_amount.BorderColor            = Color.LightGray;
                            txtb_no_absent_2nd.BorderColor          = Color.LightGray;
                            txtb_no_hours_ot.BorderColor            = Color.LightGray;
                            txtb_no_hours_sal.BorderColor            = Color.LightGray;
                            
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

                            Update_txtb_no_hours_ot.Update();
                            Update_txtb_no_hours_sal.Update();
                            Update_hours_worked.Update();
                            break;
                        }

                }
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 -Triggers When Select Payroll Year
        //*************************************************************************
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 -Triggers When Select Employee Name
        //*************************************************************************
        protected void ddl_empl_id_TextChanged(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");

            if (ddl_empl_id.SelectedValue.ToString().Trim() != "")
            {
                // *************************************************************************************************************
                // ******* BEGIN : 2022-09-23 - Check the Payroll Validations **************************************************
                // *************************************************************************************************************
                DataTable dt = MyCmn.RetrieveData("sp_chk_payroll", "par_transmittal_nbr","", "par_payroll_registry_nbr", lbl_registry_number.Text.ToString().Trim(), "par_payroll_year",ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month",ddl_month.SelectedValue.ToString().Trim(),"par_empl_id",ddl_empl_id.SelectedValue.ToString().Trim());
                if (dt.Rows.Count > 0) 
                {
                    msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                    msg_header.InnerText        = "YOU SELECT " + dt.Rows[0]["employee_name"].ToString().Trim();
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

                header_details();
                Update_days_worked.Update();
                Update_hours_worked.Update();
                Calculate_Days_worked_Ded_Absent();
                calculate_grossamount();
                Calculate_Absent();
                CheckIfNotEqualto_AmountDue();
                calculate_total_loans();
                calculate_total_mandatory();
                calculate_total_optional();
                calculate_netpay();
            }
            else
            {
                ClearEntry();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Initialized Value During Add Mode
        //*************************************************************************
        public string header_details()
        {
            string rate_basis = "";
            double lates_time = 0;
            double gross_pay = 0;
            double lates_amount = 0;
            DataRow[] selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");
            
            if (selected_employee.Length > 0)
            {
                txtb_department_name1.Text          = selected_employee[0]["department_name1"].ToString();
                rate_basis                          = selected_employee[0]["rate_basis"].ToString();
                lbl_rate_basis_descr.Text           = selected_employee[0]["rate_basis_descr"].ToString() + " Rate :";
                hidden_monthly_rate.Value           = selected_employee[0]["monthly_rate"].ToString();
                hidden_hourly_rate.Value            = selected_employee[0]["hourly_rate"].ToString();
                hidden_daily_rate.Value             = selected_employee[0]["daily_rate"].ToString();
                hidden_rate_basis.Value             = selected_employee[0]["rate_basis"].ToString();
                txtb_lates_and_undertime.Text       = selected_employee[0]["under_time"].ToString();
                txtb_no_days_worked.Text            = selected_employee[0]["no_of_workdays"].ToString();
                calculate_hours_worked();

                txtb_empl_id.Text = selected_employee[0]["empl_id"].ToString();

                lates_time = double.Parse((txtb_lates_and_undertime.Text.ToString().Trim() != "" ? txtb_lates_and_undertime.Text.ToString().Trim() : "0"));
                lates_time = lates_time / 60;

                if (lates_time.ToString().Split('.').Length > 1)
                {
                    lates_time = double.Parse(lates_time.ToString().Split('.')[0].Trim() + "." + (double.Parse("." + lates_time.ToString().Split('.')[1].Trim()) / 60).ToString().Split('.')[1]);
                }
                switch (selected_employee[0]["rate_basis"].ToString())
                {
                    case "M":
                        {
                            if (txtb_no_days_worked.Text.ToString().Trim() != "" && txtb_no_days_worked.Text.ToString().Trim() != "0" && txtb_no_days_worked.Text.ToString().Trim('0') != "")
                            {
                                gross_pay = double.Parse(selected_employee[0]["daily_rate"].ToString()) * double.Parse(txtb_no_days_worked.Text.ToString().Trim());
                            }
                            else
                            {
                                gross_pay = double.Parse(selected_employee[0]["monthly_rate"].ToString());
                            }
                            
                            lates_amount = double.Parse(selected_employee[0]["daily_rate"].ToString()) * (lates_time / double.Parse(hidden_hrs_in1day.Value));
                            txtb_rate_amount.Text = selected_employee[0]["monthly_rate"].ToString();

                            break;
                        }
                    case "D":
                        {

                            lates_amount = double.Parse(selected_employee[0]["daily_rate"].ToString()) * (lates_time / double.Parse(hidden_hrs_in1day.Value));

                            gross_pay = double.Parse(selected_employee[0]["daily_rate"].ToString()) * double.Parse(txtb_no_days_worked.Text.ToString().Trim() != "" ? txtb_no_days_worked.Text.ToString().Trim() : "0");

                            txtb_rate_amount.Text = selected_employee[0]["daily_rate"].ToString();
                            txtb_no_hours_worked.Text = "0";
                            break;
                        }
                    case "H":
                        {
                            double dr = 0;
                            dr = double.Parse(selected_employee[0]["hourly_rate"].ToString()) * double.Parse(hidden_hrs_in1day.Value);
                            gross_pay = dr * double.Parse(txtb_no_days_worked.Text.ToString() != "" ? txtb_no_days_worked.Text.ToString() : "0");
                            lates_amount = dr * lates_time;
                            txtb_rate_amount.Text = selected_employee[0]["hourly_rate"].ToString();

                            break;
                        }
                    default:
                        {
                            txtb_rate_amount.Text = "0.000";
                            break;
                        }
                }

            // BEGIN - VJA : 05/22/2019         - Populate Details During Add Mode
            txtb_ccmpc_loan.Text                = selected_employee[0]["ccmpc_ln"].ToString().Trim();
            txtb_hdmf_addl.Text                 = selected_employee[0]["hdmf_ps2"].ToString().Trim();
            txtb_hdmf_mp2.Text                  = selected_employee[0]["hdmf_mp2"].ToString().Trim();
            txtb_hdmf_cal_loan.Text             = selected_employee[0]["hdmf_cal_ln"].ToString().Trim();
            txtb_hdmf_gs.Text                   = selected_employee[0]["hdmf_gs"].ToString().Trim();
            txtb_hdmf_house_loan.Text           = selected_employee[0]["hdmf_hse_ln"].ToString().Trim();
            txtb_hdmf_mpl_loan.Text             = selected_employee[0]["hdmf_mpl_ln"].ToString().Trim();
            txtb_hdmf_ps.Text                   = selected_employee[0]["hdmf_ps"].ToString().Trim();
            txtb_networkbank_loan.Text          = selected_employee[0]["network_ln"].ToString().Trim();
            txtb_nico_loan.Text                 = selected_employee[0]["nico_ln"].ToString().Trim();
            txtb_otherloan_no1.Text             = selected_employee[0]["other_loan1"].ToString().Trim();
            txtb_otherloan_no2.Text             = selected_employee[0]["other_loan2"].ToString().Trim();
            txtb_otherloan_no3.Text             = selected_employee[0]["other_loan3"].ToString().Trim();
            txtb_otherpremium_no1.Text          = selected_employee[0]["other_premium1"].ToString().Trim();
            txtb_otherpremium_no2.Text          = selected_employee[0]["other_premium2"].ToString().Trim();
            txtb_otherpremium_no3.Text          = selected_employee[0]["other_premium3"].ToString().Trim();
            txtb_phic_gs.Text                   = selected_employee[0]["phic_gs"].ToString().Trim();
            txtb_phic_ps.Text                   = selected_employee[0]["phic_ps"].ToString().Trim();
            txtb_philam.Text                    = selected_employee[0]["philamlife_ps"].ToString().Trim();
            txtb_sss.Text                       = selected_employee[0]["sss_ps"].ToString().Trim();
            txtb_uniform.Text                   = selected_employee[0]["uniform_amt"].ToString().Trim();
            txtb_loyalty_card.Text              = selected_employee[0]["hdmf_loyalty_card"].ToString().Trim();
            txtb_position.Text                  = selected_employee[0]["position_title1"].ToString().Trim();
            txtb_no_absent_1st.Text             = selected_employee[0]["nbr_days_absent_010"].ToString();
            txtb_no_worked_1st.Text             = selected_employee[0]["days_worked_010"].ToString();
            txtb_no_hours_worked_1st.Text       = selected_employee[0]["hours_worked_010"].ToString();
            txtb_no_absent_2nd.Text             = selected_employee[0]["nbr_days_absent"].ToString();
                
            txtb_other_ded_mand1.Text           = selected_employee[0]["other_ded_mand1"].ToString();
            txtb_other_ded_mand2.Text           = selected_employee[0]["other_ded_mand2"].ToString();
            txtb_other_ded_mand3.Text           = selected_employee[0]["other_ded_mand3"].ToString();
            txtb_other_ded_mand4.Text           = selected_employee[0]["other_ded_mand4"].ToString();
            txtb_other_ded_mand5.Text           = selected_employee[0]["other_ded_mand5"].ToString();
            txtb_other_ded_mand6.Text           = selected_employee[0]["other_ded_mand6"].ToString();
            txtb_other_ded_mand7.Text           = selected_employee[0]["other_ded_mand7"].ToString();
            txtb_other_ded_mand8.Text           = selected_employee[0]["other_ded_mand8"].ToString();
            txtb_other_ded_mand9.Text           = selected_employee[0]["other_ded_mand9"].ToString();
            txtb_other_ded_mand10.Text          = selected_employee[0]["other_ded_mand10"].ToString();
            txtb_other_ded_prem1.Text           = selected_employee[0]["other_ded_prem1"].ToString();
            txtb_other_ded_prem2.Text           = selected_employee[0]["other_ded_prem2"].ToString();
            txtb_other_ded_prem3.Text           = selected_employee[0]["other_ded_prem3"].ToString();
            txtb_other_ded_prem4.Text           = selected_employee[0]["other_ded_prem4"].ToString();
            txtb_other_ded_prem5.Text           = selected_employee[0]["other_ded_prem5"].ToString();
            txtb_other_ded_prem6.Text           = selected_employee[0]["other_ded_prem6"].ToString();
            txtb_other_ded_prem7.Text           = selected_employee[0]["other_ded_prem7"].ToString();
            txtb_other_ded_prem8.Text           = selected_employee[0]["other_ded_prem8"].ToString();
            txtb_other_ded_prem9.Text           = selected_employee[0]["other_ded_prem9"].ToString();
            txtb_other_ded_prem10.Text          = selected_employee[0]["other_ded_prem10"].ToString();
            txtb_other_ded_loan1.Text           = selected_employee[0]["other_ded_loan1"].ToString();
            txtb_other_ded_loan2.Text           = selected_employee[0]["other_ded_loan2"].ToString();
            txtb_other_ded_loan3.Text           = selected_employee[0]["other_ded_loan3"].ToString();
            txtb_other_ded_loan4.Text           = selected_employee[0]["other_ded_loan4"].ToString();
            txtb_other_ded_loan5.Text           = selected_employee[0]["other_ded_loan5"].ToString();
            txtb_other_ded_loan6.Text           = selected_employee[0]["other_ded_loan6"].ToString();
            txtb_other_ded_loan7.Text           = selected_employee[0]["other_ded_loan7"].ToString();
            txtb_other_ded_loan8.Text           = selected_employee[0]["other_ded_loan8"].ToString();
            txtb_other_ded_loan9.Text           = selected_employee[0]["other_ded_loan9"].ToString();
            txtb_other_ded_loan10.Text          = selected_employee[0]["other_ded_loan10"].ToString();

            Update_lates_and_undertime.Update();
            Update_rate_amount.Update();

            calculate_grossamount();
            Calculate_Absent();
            Calculate_AmountDue();
            Calculate_PHIC();
            CalculateManExemption();
            Calculate_Taxes();
            calculate_total_loans();
            calculate_total_mandatory();
            calculate_total_optional();
            calculate_netpay();

            }
            return rate_basis;
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Calculate Gross Pay
        //*************************************************************************
        private void calculate_grossamount()
        {
            decimal lates_time       = 0;
            double gross_pay         = 0;
            decimal lates_amount     = 0;
            string str_lates_amount  = "";
                lates_time = decimal.Parse((txtb_lates_and_undertime.Text.ToString().Trim() != "" ? txtb_lates_and_undertime.Text.ToString().Trim() : "0"));
                lates_time = lates_time / 60;

                if (lates_time.ToString().Split('.').Length > 1)
                {
                    lates_time = decimal.Parse(lates_time.ToString().Split('.')[0].Trim() + "." + (decimal.Parse("." + lates_time.ToString().Split('.')[1].Trim()) / 60).ToString().Split('.')[1]);
                }

                switch (hidden_rate_basis.Value.ToString().Trim())
                {
                    case "M": // Rate basis = Monthly Rate is Daily Rate * No of days Worked
                        {
                            gross_pay                   = double.Parse(txtb_rate_amount.Text.ToString());
                            lates_amount                = decimal.Parse(hidden_daily_rate.Value.ToString()) * (lates_time / decimal.Parse(hidden_hrs_in1day.Value));
                            decimal late_amount1        = decimal.Parse(txtb_rate_amount.Text.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60; 
                            string str_lates_amount_1   = "";
                            str_lates_amount_1          = late_amount1.ToString("###,##0.000000000000");
                            str_lates_amount_1          = str_lates_amount_1.Split('.')[0] + "." + str_lates_amount_1.Split('.')[1].Substring(0,11);
                            lates_amount                = decimal.Parse(str_lates_amount_1) * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                            hidden_monthly_rate.Value   = txtb_rate_amount.Text.ToString();
                            break;
                        }
                    case "D": // Rate basis = Daily Rate is Daily Rate * No of days Worked
                        {
                            lates_amount                = decimal.Parse(txtb_rate_amount.Text.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60 * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                            decimal late_amount1        = decimal.Parse(txtb_rate_amount.Text.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60; 
                            string str_lates_amount_1   = "";
                            str_lates_amount_1          = late_amount1.ToString("###,##0.000000000000");
                            str_lates_amount_1          = str_lates_amount_1.Split('.')[0] + "." + str_lates_amount_1.Split('.')[1].Substring(0,11);
                            lates_amount                = decimal.Parse(str_lates_amount_1) * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                            gross_pay                   = double.Parse(txtb_rate_amount.Text.ToString()) * double.Parse(txtb_no_days_worked.Text.ToString().Trim() != "" ? txtb_no_days_worked.Text.ToString().Trim() : "0");
                            txtb_rate_amount.Text       = txtb_rate_amount.Text;
                            hidden_daily_rate.Value     = txtb_rate_amount.Text.ToString(); 
                            break;
                        }
                    case "H":// Rate basis = Hourlky Rate is Hourly Rate * No of Hours Worked
                        {
                            lates_amount                = decimal.Parse(hidden_daily_rate.Value.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60 * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                            decimal late_amount1        = decimal.Parse(txtb_rate_amount.Text.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60; 
                            string str_lates_amount_1   = "";
                            str_lates_amount_1          = late_amount1.ToString("###,##0.000000000000");
                            str_lates_amount_1          = str_lates_amount_1.Split('.')[0] + "." + str_lates_amount_1.Split('.')[1].Substring(0,11);
                            lates_amount                = decimal.Parse(str_lates_amount_1) * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                            gross_pay                   = double.Parse(txtb_rate_amount.Text) * double.Parse(txtb_no_hours_worked.Text.ToString());
                            hidden_hourly_rate.Value = txtb_rate_amount.Text.ToString();
                            break;
                        }
                    default:
                        {
                            txtb_rate_amount.Text = "0.000";
                            break;
                        }
                }
            // NEW COMPUTATION **************************************
            txtb_gross_pay.Text = gross_pay.ToString("###,##0.00");
            str_lates_amount    = lates_amount.ToString("###,##0.00");
            str_lates_amount    = str_lates_amount.Split('.')[0] + "." + str_lates_amount.Split('.')[1].Substring(0, 2);
            txtb_less.Text      = str_lates_amount;
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Triggers When Click Calculate Button
        //*************************************************************************
        protected void btn_calculate_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                calculate_grossamount();
                Calculate_Absent();
                Calculate_AmountDue();
                Calculate_Hours_OT();
                Calculate_PHIC();
                CalculateManExemption();
                Calculate_Taxes();
                calculate_total_loans();
                calculate_total_mandatory();
                calculate_total_optional();
                calculate_netpay();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Calculate Mandatory
        //*************************************************************************
        private double calculate_total_mandatory()
        {
            double total_mandatory      = 0;
            string str_total_mandatory  = "";
            /*! COMPUTATION NOTE COMMENT !*/
            /***(double.Parse (check if textbox contains more than 3 decimal ? true(splite wholeNo+'.'+decimal subs to tw0 degit): false (textbox value itself)) )***/
            total_mandatory             = total_mandatory + double.Parse((txtb_hdmf_ps.Text.ToString().Trim().Split('.').Length             > 1 ? txtb_hdmf_ps.Text.ToString().Trim().Split('.')[0]             + "." + txtb_hdmf_ps.Text.ToString().Trim().Split('.')[1].Substring(0, 2)           : txtb_hdmf_ps.Text.ToString().Trim()));
            total_mandatory             = total_mandatory + double.Parse((txtb_phic_ps.Text.ToString().Trim().Split('.').Length             > 1 ? txtb_phic_ps.Text.ToString().Trim().Split('.')[0]             + "." + txtb_phic_ps.Text.ToString().Trim().Split('.')[1].Substring(0, 2)           : txtb_phic_ps.Text.ToString().Trim()));
            total_mandatory             = total_mandatory + double.Parse((txtb_bir_tax_10percent.Text.ToString().Trim().Split('.').Length   > 1 ? txtb_bir_tax_10percent.Text.ToString().Trim().Split('.')[0]   + "." + txtb_bir_tax_10percent.Text.ToString().Trim().Split('.')[1].Substring(0, 2) : txtb_bir_tax_10percent.Text.ToString().Trim()));
            total_mandatory             = total_mandatory + double.Parse((txtb_bir_tax_15percent.Text.ToString().Trim().Split('.').Length   > 1 ? txtb_bir_tax_15percent.Text.ToString().Trim().Split('.')[0]   + "." + txtb_bir_tax_15percent.Text.ToString().Trim().Split('.')[1].Substring(0, 2) : txtb_bir_tax_15percent.Text.ToString().Trim()));
            total_mandatory             = total_mandatory + double.Parse((txtb_bir_tax_2percent.Text.ToString().Trim().Split('.').Length    > 1 ? txtb_bir_tax_2percent.Text.ToString().Trim().Split('.')[0]    + "." + txtb_bir_tax_2percent.Text.ToString().Trim().Split('.')[1].Substring(0, 2)  : txtb_bir_tax_2percent.Text.ToString().Trim()));
            total_mandatory             = total_mandatory + double.Parse((txtb_bir_tax_3percent.Text.ToString().Trim().Split('.').Length    > 1 ? txtb_bir_tax_3percent.Text.ToString().Trim().Split('.')[0]    + "." + txtb_bir_tax_3percent.Text.ToString().Trim().Split('.')[1].Substring(0, 2)  : txtb_bir_tax_3percent.Text.ToString().Trim()));
            total_mandatory             = total_mandatory + double.Parse((txtb_bir_tax_5percent.Text.ToString().Trim().Split('.').Length    > 1 ? txtb_bir_tax_5percent.Text.ToString().Trim().Split('.')[0]    + "." + txtb_bir_tax_5percent.Text.ToString().Trim().Split('.')[1].Substring(0, 2)  : txtb_bir_tax_5percent.Text.ToString().Trim()));
            total_mandatory             = total_mandatory + double.Parse((txtb_bir_tax_8percent.Text.ToString().Trim().Split('.').Length    > 1 ? txtb_bir_tax_8percent.Text.ToString().Trim().Split('.')[0]    + "." + txtb_bir_tax_8percent.Text.ToString().Trim().Split('.')[1].Substring(0, 2)  : txtb_bir_tax_8percent.Text.ToString().Trim()));
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
            
            str_total_mandatory         = total_mandatory.ToString("###,##0.00");
            str_total_mandatory         = str_total_mandatory.Split('.')[0] + "." + str_total_mandatory.Split('.')[1].Substring(0,2);

            txtb_total_mandatory.Text   = str_total_mandatory;
            return total_mandatory;
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Calculate Loans
        //*************************************************************************
        private double calculate_total_loans()
        {
            double total_loans          = 0;
            string str_total_loans      = "";
            /*! COMPUTATION NOTE COMMENT !*/
            /***(double.Parse (check if textbox contains more than 3 decimal ? true(splite wholeNo+'.'+decimal subs to tw0 degit): false (textbox value itself)) )***/
            total_loans                 = total_loans + double.Parse((txtb_hdmf_mpl_loan.Text.ToString().Trim().Split('.').Length           > 1 ? txtb_hdmf_mpl_loan.Text.ToString().Trim().Split('.')[0]       + "." + txtb_hdmf_mpl_loan.Text.ToString().Trim().Split('.')[1].Substring(0, 2)     : txtb_hdmf_mpl_loan.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_hdmf_house_loan.Text.ToString().Trim().Split('.').Length         > 1 ? txtb_hdmf_house_loan.Text.ToString().Trim().Split('.')[0]     + "." + txtb_hdmf_house_loan.Text.ToString().Trim().Split('.')[1].Substring(0, 2)   : txtb_hdmf_house_loan.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_hdmf_cal_loan.Text.ToString().Trim().Split('.').Length           > 1 ? txtb_hdmf_cal_loan.Text.ToString().Trim().Split('.')[0]       + "." + txtb_hdmf_cal_loan.Text.ToString().Trim().Split('.')[1].Substring(0, 2)     : txtb_hdmf_cal_loan.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_ccmpc_loan.Text.ToString().Trim().Split('.').Length              > 1 ? txtb_ccmpc_loan.Text.ToString().Trim().Split('.')[0]          + "." + txtb_ccmpc_loan.Text.ToString().Trim().Split('.')[1].Substring(0, 2)        : txtb_ccmpc_loan.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_nico_loan.Text.ToString().Trim().Split('.').Length               > 1 ? txtb_nico_loan.Text.ToString().Trim().Split('.')[0]           + "." + txtb_nico_loan.Text.ToString().Trim().Split('.')[1].Substring(0, 2)         : txtb_nico_loan.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_networkbank_loan.Text.ToString().Trim().Split('.').Length        > 1 ? txtb_networkbank_loan.Text.ToString().Trim().Split('.')[0]    + "." + txtb_networkbank_loan.Text.ToString().Trim().Split('.')[1].Substring(0, 2)  : txtb_networkbank_loan.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_otherloan_no1.Text.ToString().Trim().Split('.').Length           > 1 ? txtb_otherloan_no1.Text.ToString().Trim().Split('.')[0]       + "." + txtb_otherloan_no1.Text.ToString().Trim().Split('.')[1].Substring(0, 2)     : txtb_otherloan_no1.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_otherloan_no3.Text.ToString().Trim().Split('.').Length           > 1 ? txtb_otherloan_no3.Text.ToString().Trim().Split('.')[0]       + "." + txtb_otherloan_no3.Text.ToString().Trim().Split('.')[1].Substring(0, 2)     : txtb_otherloan_no3.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_otherloan_no3.Text.ToString().Trim().Split('.').Length           > 1 ? txtb_otherloan_no3.Text.ToString().Trim().Split('.')[0]       + "." + txtb_otherloan_no3.Text.ToString().Trim().Split('.')[1].Substring(0, 2)     : txtb_otherloan_no3.Text.ToString().Trim()));
            total_loans                 = total_loans + double.Parse((txtb_uniform.Text.ToString().Trim().Split('.').Length            > 1 ? txtb_uniform.Text.ToString().Trim().Split('.')[0]        + "." + txtb_uniform.Text.ToString().Trim().Split('.')[1].Substring(0, 2)      : txtb_uniform.Text.ToString().Trim()));

            
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
            

            str_total_loans = total_loans.ToString("###,##0.00");
            str_total_loans             = str_total_loans.Split('.')[0] + "." + str_total_loans.Split('.')[1].Substring(0,2);
            txtb_total_loans.Text       = str_total_loans;
            return total_loans;
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Calculate Optional 
        //*************************************************************************
        private double calculate_total_optional()
        {
            double total_optional       = 0;
            string str_total_optional   = "";
            /*! COMPUTATION NOTE COMMENT !*/
            /***(double.Parse (check if textbox contains more than 3 decimal ? true(splite wholeNo+'.'+decimal subs to tw0 degit): false (textbox value itself)) )***/
            total_optional              = total_optional + double.Parse((txtb_sss.Text.ToString().Trim().Split('.').Length                  > 1 ? txtb_sss.Text.ToString().Trim().Split('.')[0]                 + "." + txtb_sss.Text.ToString().Trim().Split('.')[1].Substring(0,2)                : txtb_sss.Text.ToString().Trim()));
            total_optional              = total_optional + double.Parse((txtb_hdmf_addl.Text.ToString().Trim().Split('.').Length            > 1 ? txtb_hdmf_addl.Text.ToString().Trim().Split('.')[0]           + "." + txtb_hdmf_addl.Text.ToString().Trim().Split('.')[1].Substring(0, 2)         : txtb_hdmf_addl.Text.ToString().Trim()));
            total_optional              = total_optional + double.Parse((txtb_hdmf_mp2.Text.ToString().Trim().Split('.').Length             > 1 ? txtb_hdmf_mp2.Text.ToString().Trim().Split('.')[0]            + "." + txtb_hdmf_mp2.Text.ToString().Trim().Split('.')[1].Substring(0, 2)          : txtb_hdmf_mp2.Text.ToString().Trim()));
            total_optional              = total_optional + double.Parse((txtb_philam.Text.ToString().Trim().Split('.').Length               > 1 ? txtb_philam.Text.ToString().Trim().Split('.')[0]              + "." + txtb_philam.Text.ToString().Trim().Split('.')[1].Substring(0, 2)            : txtb_philam.Text.ToString().Trim()));
            total_optional              = total_optional + double.Parse((txtb_otherpremium_no1.Text.ToString().Trim().Split('.').Length     > 1 ? txtb_otherpremium_no1.Text.ToString().Trim().Split('.')[0]    + "." + txtb_otherpremium_no1.Text.ToString().Trim().Split('.')[1].Substring(0, 2)  : txtb_otherpremium_no1.Text.ToString().Trim()));
            total_optional              = total_optional + double.Parse((txtb_otherpremium_no2.Text.ToString().Trim().Split('.').Length     > 1 ? txtb_otherpremium_no2.Text.ToString().Trim().Split('.')[0]    + "." + txtb_otherpremium_no2.Text.ToString().Trim().Split('.')[1].Substring(0, 2)  : txtb_otherpremium_no2.Text.ToString().Trim()));
            total_optional              = total_optional + double.Parse((txtb_otherpremium_no3.Text.ToString().Trim().Split('.').Length     > 1 ? txtb_otherpremium_no3.Text.ToString().Trim().Split('.')[0]    + "." + txtb_otherpremium_no3.Text.ToString().Trim().Split('.')[1].Substring(0, 2)  : txtb_otherpremium_no3.Text.ToString().Trim()));
            //total_optional              = total_optional + double.Parse((txtb_uniform.Text.ToString().Trim().Split('.').Length > 1 ? txtb_uniform.Text.ToString().Trim().Split('.')[0] + "." + txtb_uniform.Text.ToString().Trim().Split('.')[1].Substring(0, 2) : txtb_uniform.Text.ToString().Trim()));
            total_optional              = total_optional + double.Parse((txtb_loyalty_card.Text.ToString().Trim().Split('.').Length > 1 ? txtb_loyalty_card.Text.ToString().Trim().Split('.')[0] + "." + txtb_loyalty_card.Text.ToString().Trim().Split('.')[1].Substring(0, 2) : txtb_loyalty_card.Text.ToString().Trim()));


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


            str_total_optional = total_optional.ToString("###,##0.00");
            txtb_total_optional.Text    = str_total_optional.Split('.')[0] + "." + str_total_optional.Split('.')[1].Substring(0, 2);
            txtb_total_optional.Text    = str_total_optional;
            return total_optional;
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Calculate Net Pay
        //*************************************************************************
        private void calculate_netpay()
        {
            string str_net_pay = "";
            double total_deductions = 0;
            total_deductions = double.Parse(txtb_total_loans.Text.ToString().Trim()) + double.Parse(txtb_total_mandatory.Text.ToString().Trim()) + double.Parse(txtb_total_optional.Text.ToString().Trim()) + double.Parse(txtb_less.Text.ToString());
            str_net_pay = (double.Parse(txtb_gross_pay.Text.ToString()) - total_deductions).ToString("###,##0.00");
            str_net_pay = str_net_pay.Split('.')[0] + "." + str_net_pay.Split('.')[1].Substring(0, 2);
            txtb_net_pay.Text = str_net_pay;
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Triggers When Put Value on Textbox  : txtb_no_hours_worked
        //*************************************************************************
        protected void txtb_no_hours_worked_TextChanged(object sender, EventArgs e)
        {
            calculate_days_worked();
            CheckIfNotEqualto_AmountDue();

            if (ddl_payroll_template.SelectedValue == "009")
            {
                calculate_grossamount();
                Calculate_Absent();
                Calculate_AmountDue();
                Calculate_PHIC();
                Calculate_Taxes();
                calculate_total_loans();
                calculate_total_mandatory();
                calculate_total_optional();
                calculate_netpay();
            }

            txtb_no_hours_sal.Text = txtb_no_hours_worked.Text;
            Calculate_Hours_OT();
            Update_txtb_no_hours_sal.Update();

            Update_days_worked.Update();
            txtb_no_hours_worked.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_no_hours_worked.Focus();
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Triggers When Put Value on Textbox  : txtb_no_days_worked
        //*************************************************************************
        protected void txtb_no_days_worked_TextChanged(object sender, EventArgs e)
        {
            calculate_hours_worked();
            CheckIfNotEqualto_AmountDue();

            if (ddl_payroll_template.SelectedValue == "009")
            {
                calculate_grossamount();
                Calculate_Absent();
                Calculate_AmountDue();
                Calculate_PHIC();
                CalculateManExemption();
                Calculate_Taxes();
                calculate_total_loans();
                calculate_total_mandatory();
                calculate_total_optional();
                calculate_netpay();
            }

            Update_hours_worked.Update();
            txtb_no_days_worked.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_no_days_worked.Focus();
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Calculate Days Worked
        //*************************************************************************
        private void calculate_days_worked()
        {
            double total_days_worked = 0;
            FieldValidationColorChanged(false, "ALL");
            if (CommonCode.checkisdecimal(txtb_no_hours_worked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_hours_worked");
                Update_hours_worked.Update();
                txtb_no_days_worked.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                txtb_no_days_worked.Focus();
            }
            else
            {
                total_days_worked = double.Parse(txtb_no_hours_worked.Text) / double.Parse(hidden_hrs_in1day.Value);
                txtb_no_days_worked.Text = total_days_worked.ToString("###,##0.00");
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/25/2019 - Calculate Hours Worked
        //*************************************************************************
        private void calculate_hours_worked()
        {
            double total_hours_worked = 0;

            FieldValidationColorChanged(false, "ALL");

            if (CommonCode.checkisdecimal(txtb_no_days_worked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_worked");
                Update_days_worked.Update();
                txtb_no_hours_worked.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                txtb_no_hours_worked.Focus();
            }
            else
            {
                total_hours_worked = double.Parse(txtb_no_days_worked.Text) * double.Parse(hidden_hrs_in1day.Value);
                txtb_no_hours_worked.Text = total_hours_worked.ToString("###,##0.00");
                txtb_no_hours_sal.Text    = total_hours_worked.ToString("###,##0.00");
            }
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
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            txtb_bir_tax_10percent.Enabled    = ifenable;
            txtb_bir_tax_15percent.Enabled    = ifenable;
            txtb_bir_tax_2percent.Enabled     = ifenable;
            txtb_bir_tax_3percent.Enabled     = ifenable;
            txtb_bir_tax_5percent.Enabled     = ifenable;
            txtb_bir_tax_8percent.Enabled     = ifenable;
            txtb_ccmpc_loan.Enabled           = ifenable;
            txtb_hdmf_addl.Enabled            = ifenable;
            txtb_hdmf_mp2.Enabled             = ifenable;
            txtb_hdmf_cal_loan.Enabled        = ifenable;
            txtb_hdmf_gs.Enabled              = ifenable;
            txtb_hdmf_house_loan.Enabled      = ifenable;
            txtb_hdmf_mpl_loan.Enabled        = ifenable;
            txtb_hdmf_ps.Enabled              = ifenable;
            txtb_less.Enabled                 = ifenable;
            txtb_lates_and_undertime.Enabled  = ifenable;
            txtb_networkbank_loan.Enabled     = ifenable;
            txtb_nico_loan.Enabled            = ifenable;
            txtb_no_days_worked.Enabled       = ifenable;
            txtb_no_hours_worked.Enabled      = ifenable;
            txtb_otherloan_no1.Enabled        = ifenable;
            txtb_otherloan_no2.Enabled        = ifenable;
            txtb_otherloan_no3.Enabled        = ifenable;
            txtb_otherpremium_no1.Enabled     = ifenable;
            txtb_otherpremium_no2.Enabled     = ifenable;
            txtb_otherpremium_no3.Enabled     = ifenable;
            txtb_phic_gs.Enabled              = ifenable;
            txtb_phic_ps.Enabled              = ifenable;
            txtb_philam.Enabled               = ifenable;
            txtb_sss.Enabled                  = ifenable;
            txtb_uniform.Enabled              = ifenable;
            txtb_loyalty_card.Enabled         = ifenable;
            txtb_rate_amount.Enabled          = ifenable;
            txtb_no_absent_2nd.Enabled        = ifenable;
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
        //****************************************************************************************
        //  BEGIN - VJA-  2020-06-02 - Calculate PHIC PS specifically 1st and 2nd Quincena Payroll
        //****************************************************************************************
        private void Calculate_PHIC()
        {
            double phic_ps_amt = 0;
            var effective_date = ddl_year.SelectedValue.ToString().Trim() + "-" + ddl_month.SelectedValue.ToString().Trim() + "-01";
            DataTable dt_perc = MyCmn.RetrieveData("sp_philhealth_tbl_list");
            string selectExpression = "effective_date = '" + effective_date + "'";
            DataRow[] row2Select = dt_perc.Select(selectExpression);

            // COMPUTATION FOR PHIC PS on 1st Quincena and 2nd Quincena = GROSS PAY - LATES AMOUNT * PHIC PERCENTAGE
            if (ddl_payroll_template.SelectedValue == "010")
            {
                var date_selected = ddl_year.SelectedValue.ToString().Trim() + '-' + ddl_month.SelectedValue.ToString().Trim() + "-01";

                if (DateTime.Parse(date_selected).Year >= 2022)
                {
                    double gross_ded_lates = 0;
                    gross_ded_lates = double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text);
                    DataTable dt = MyCmn.RetrieveData("sp_phic_compute_indv_jo", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "p_empl_id", txtb_empl_id.Text.ToString().Trim(), "p_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "p_gross_pay", gross_ded_lates.ToString("###,##0.00"));
                    if (dt.Rows != null || dt != null)
                    {
                        phic_ps_amt = double.Parse(dt.Rows[0]["phic_ps"].ToString().Trim());
                    }
                }
                else
                {
                    // OLD COMPUTATION FOR PHIC PS on 1st Quincena and 2nd Quincena = GROSS PAY - LATES AMOUNT * PHIC PERCENTAGE
                     phic_ps_amt = (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * (double.Parse(row2Select[0]["contribution_rate"].ToString()) / 100);
                }

                txtb_phic_ps.Text = phic_ps_amt.ToString("###,##0.00");
            }
            else if (ddl_payroll_template.SelectedValue == "011")
            {
                double gross_ded_lates = 0;
                gross_ded_lates = double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text);
                DataTable dt = MyCmn.RetrieveData("sp_phic_compute_indv_jo", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "p_empl_id", txtb_empl_id.Text.ToString().Trim(), "p_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "p_gross_pay", gross_ded_lates.ToString("###,##0.00"));
                if (dt.Rows != null || dt != null)
                {
                    txtb_phic_ps.Text = dt.Rows[0]["phic_ps"].ToString().Trim();
                }
            }
            else 
            {
                DataTable chk = new DataTable();
                string query = "SELECT * FROM payrollemployeemaster_hdr_tbl A WHERE A.empl_id = '"+ txtb_empl_id.Text.ToString().Trim() + "' AND   A.emp_rcrd_status = 1 AND   A.effective_date = (SELECT MAX(A1.effective_date) FROM payrollemployeemaster_hdr_tbl A1 WHERE A1.empl_id = A.empl_id AND A1.emp_rcrd_status = A.emp_rcrd_status AND A1.effective_date  <= CONVERT(date,'"+ effective_date + "'))";
                chk = MyCmn.GetDatatable(query);
                if (chk.Rows.Count > 0)
                {
                    if (Boolean.Parse(chk.Rows[0]["flag_expt_phic"].ToString()) == false)
                    {
                        double gross_ded_lates = 0;
                        gross_ded_lates = double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text);
                        DataTable dt = MyCmn.RetrieveData("sp_phic_compute_indv_jo", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "p_empl_id", txtb_empl_id.Text.ToString().Trim(), "p_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "p_gross_pay", gross_ded_lates.ToString("###,##0.00"));
                        if (dt.Rows != null || dt != null)
                        {
                            txtb_phic_ps.Text = dt.Rows[0]["phic_ps"].ToString().Trim();
                        }
                    }
                }
                else
                {
                    txtb_phic_ps.Text = "0.00";
                }
            }
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopXSelectReport", "openSelectReport();", true);
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

            switch (ddl_select_report.SelectedValue)
            {
                case "01": // PaySLip 
                    printreport = "/cryJobOrderReports/cryPayslip/cryPaySlip.rpt";
                    procedure = "sp_payrollregistry_salary_payslip_jo_rep";
                    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[1].ToString().Trim() + ",par_payrolltemplate_code," + "213" + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
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
        //***********************************************************************************
        //  BEGIN - VJA- 2020-09-26 - Calculate Taxes During Add and Calculate Individually
        //***********************************************************************************
        private void Calculate_Taxes()
        {
            double tax2     = 0;
            double tax3     = 0;
            double tax5     = 0;
            double tax8     = 0;
            double tax10    = 0;
            double tax15    = 0;

            //dataList_employee_tax       = MyCmn.RetrieveData("sp_personnelnames_combolist_preg_jo_for_tax", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", ViewState["payroll_group_nbr"].ToString().Trim());
            dataList_employee_tax       = MyCmn.RetrieveData("sp_jo_for_tax", "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            DataRow[] selected_employee = dataList_employee_tax.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");

            if (selected_employee.Length > 0)
            {
                // **************************************************************
                // *** VJA - Compute VAT
                // **************************************************************
                switch (selected_employee[0]["tax_perc"].ToString())
                {
                    case "2":
                        tax2 = tax2 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .02;
                        break;
                    case "1":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .01;
                        break;
                    case "3":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .03;
                        break;
                    case "5":
                        tax5 = tax5 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .05;
                        break;
                    case "8":
                        tax8 = tax8 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .08;
                        break;
                    case "10":
                        tax10 = tax10 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .10;
                        break;
                    case "15":
                        tax15 = tax15 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .15;
                        break;
                }

                // **************************************************************
                // *** VJA - Compute VAT
                // **************************************************************

                switch (selected_employee[0]["vat_perc"].ToString())
                {
                    case "2":
                        tax2 = tax2 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .02;
                        break;
                    case "1":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .01;
                        break;
                    case "3":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .03;
                        break;
                    case "5":
                        tax5 = tax5 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .05;
                        break;
                    case "8":
                        tax8 = tax8 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .08;
                        break;
                    case "10":
                        tax10 = tax10 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .10;
                        break;
                    case "15":
                        tax15 = tax15 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .15;
                        break;
                }
                // **************************************************************
                // *** VJA - 2022-04-05 Additional Computation for 3 Percentage - Special Case without Sworn       
                // **************************************************************

                switch (selected_employee[0]["additional_vat_perc"].ToString())
                {
                    case "2":
                        tax2 = tax2 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .02;
                        break;
                    case "1":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .01;
                        break;
                    case "3":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .03;
                        break;
                    case "5":
                        tax5 = tax5 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .05;
                        break;
                    case "8":
                        tax8 = tax8 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .08;
                        break;
                    case "10":
                        tax10 = tax10 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .10;
                        break;
                    case "15":
                        tax15 = tax15 + (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text)) * .15;
                        break;
                }

            }

            txtb_bir_tax_2percent.Text   = tax2.ToString("###,##0.00").Trim();
            txtb_bir_tax_3percent.Text   = tax3.ToString("###,##0.00").Trim();
            txtb_bir_tax_5percent.Text   = tax5.ToString("###,##0.00").Trim();
            txtb_bir_tax_8percent.Text   = tax8.ToString("###,##0.00").Trim();
            txtb_bir_tax_10percent.Text  = tax10.ToString("###,##0.00").Trim();
            txtb_bir_tax_15percent.Text  = tax15.ToString("###,##0.00").Trim();
        }
        //******************************************************************************************
        //  BEGIN - VJA- 2020-09-26 - Hide Div Number of Days if 2nd QUincena Tempalte Description
        //  BEGIN - VJA : 2020-10-07 - Session Number of Days Coming from Payroll Registry Header
        //******************************************************************************************
        private void Toogle_1st_Quincena()
        {
            if (ddl_payroll_template.SelectedValue == "011")
            {
                txtb_no_working_1st.Text = Session  ["PreviousValuesonPage_cPayRegistry_nod_work_1st"].ToString();
                txtb_no_working_2nd.Text = Session  ["PreviousValuesonPage_cPayRegistry_nod_work_2nd"].ToString();
                div_1st_quincena_nbr.Visible = true;
            }
            else
            {
                txtb_no_working_2nd.Text = Session["PreviousValuesonPage_cPayRegistry_nod_work_1st"].ToString();
                div_1st_quincena_nbr.Visible = false;
            }
        }
        //******************************************************************************************
        //  BEGIN - VJA- 2020-09-26 - Calculate Worked days and Hours Worked Deducted by Absent
        //******************************************************************************************
        private void Calculate_Days_worked_Ded_Absent()
        {
            double days_worked = 0;
            double hours_worked = 0;

            switch (hidden_rate_basis.Value.ToString().Trim())
            {
                case "D":
                case "M":
                    days_worked = double.Parse(txtb_no_working_2nd.Text) - double.Parse(txtb_no_absent_2nd.Text);
                    hours_worked = days_worked * double.Parse(hidden_hrs_in1day.Value);

                    txtb_no_days_worked.Text  = days_worked.ToString("###,##0.00");
                    Update_days_worked.Update();

                    txtb_no_hours_worked.Text = hours_worked.ToString("###,##0.00");
                    Update_hours_worked.Update();

                break;
            }
        }
        //******************************************************************************************
        //  BEGIN - VJA- 2020-11-12 - Calculate Absent - Monthly Payroll - JO
        //******************************************************************************************
        private void Calculate_Absent()
        {
            double absent_amt = 0;
            double gross_amt  = 0;

            if (ddl_payroll_template.SelectedValue == "009") // Monthly Payroll - JO
            {
                switch (hidden_rate_basis.Value.ToString().Trim())
                {
                    case "H":
                        absent_amt = double.Parse(txtb_no_absent_2nd.Text) * (double.Parse(txtb_rate_amount.Text) * 8);
                        break;
                    case "D":
                        absent_amt = double.Parse(txtb_no_absent_2nd.Text) * double.Parse(txtb_rate_amount.Text);
                        break;
                    case "M":
                        absent_amt = double.Parse(txtb_no_absent_2nd.Text) * (double.Parse(txtb_rate_amount.Text) / 22);
                        break;
                }
                gross_amt = double.Parse(txtb_gross_pay.Text) - absent_amt;
                txtb_gross_pay.Text = gross_amt.ToString("###,##0.00");
            }
        }

        //******************************************************************************************
        //  BEGIN - VJA- 2020-03-18 - Calculate Amount Due
        //******************************************************************************************
        private void Calculate_AmountDue()
        {
            double amount_due = 0;
            amount_due = (double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_less.Text.ToString()));
            txtb_amount_due.Text = amount_due.ToString("###,##0.00");

        }
        //******************************************************************************************
        //  BEGIN - VJA- 2021-04-12 - Calculate Exempted Deduction
        //******************************************************************************************
        private void CalculateManExemption()
        {
            DataTable dataList_employee_flag = new DataTable();
            dataList_employee_flag = MyCmn.RetrieveData("sp_personnelnames_combolist_flag_expt", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", ViewState["payroll_group_nbr"].ToString().Trim());
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

                if ((selected_employee[0]["flag_expt_hdmf"].ToString() == "1" ||
                selected_employee[0]["flag_expt_hdmf"].ToString() == "True") && double.Parse(selected_employee[0]["hdmf_fix_rate"].ToString()) == 0)
                {
                    txtb_hdmf_ps.Text = "0.00";
                    txtb_hdmf_gs.Text = "0.00";
                }
                else
                {
                    txtb_hdmf_ps.Text = "0.00";
                    // Fix Same as Generation and Add Manually
                    if (ddl_payroll_template.SelectedValue == "010" ||
                        ddl_payroll_template.SelectedValue == "009")
                    {
                        txtb_hdmf_ps.Text = "400.00";
                    }
                }

                if (double.Parse(selected_employee[0]["hdmf_fix_rate"].ToString()) != 0)
                {
                    txtb_hdmf_ps.Text = selected_employee[0]["hdmf_fix_rate"].ToString();
                }
                // END - VJA : 2021-04-12 - Exempted Mandatory Deduction
            }
        }
        //******************************************************************************************
        //  BEGIN - VJA- 2020-03-18 - Check if not Equal on Amount Due and on the Fly Gross Pay
        //******************************************************************************************
        private void CheckIfNotEqualto_AmountDue()
        {
            double amount_due_hidden = 0;
            decimal lates_time       = 0;
            decimal lates_amount     = 0;

            decimal late_amount1 = 0;
            string str_lates_amount_1 = "";

            lates_time = decimal.Parse((txtb_lates_and_undertime.Text.ToString().Trim() != "" ? txtb_lates_and_undertime.Text.ToString().Trim() : "0"));
            lates_time = lates_time / 60;
            
            if (lates_time.ToString().Split('.').Length > 1)
            {
                lates_time = decimal.Parse(lates_time.ToString().Split('.')[0].Trim() + "." + (decimal.Parse("." + lates_time.ToString().Split('.')[1].Trim()) / 60).ToString().Split('.')[1]);
            }

            switch (hidden_rate_basis.Value.ToString().Trim())
            {
                case "H":
                    lates_amount      = decimal.Parse(hidden_daily_rate.Value.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60 * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                    
                    late_amount1                = decimal.Parse(txtb_rate_amount.Text.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60;
                    str_lates_amount_1          = "";
                    str_lates_amount_1          = late_amount1.ToString("###,##0.000000000000");
                    str_lates_amount_1          = str_lates_amount_1.Split('.')[0] + "." + str_lates_amount_1.Split('.')[1].Substring(0, 11);
                    lates_amount                = decimal.Parse(str_lates_amount_1) * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                    
                    amount_due_hidden = (double.Parse(txtb_rate_amount.Text) * double.Parse(txtb_no_hours_worked.Text.ToString()));
                    amount_due_hidden = amount_due_hidden - double.Parse(lates_amount.ToString("###,##0.00"));

                    break;
                case "D":
                    lates_amount      = decimal.Parse(txtb_rate_amount.Text.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60 * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                    
                    late_amount1                = decimal.Parse(txtb_rate_amount.Text.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60;
                    str_lates_amount_1          = "";
                    str_lates_amount_1          = late_amount1.ToString("###,##0.000000000000");
                    str_lates_amount_1          = str_lates_amount_1.Split('.')[0] + "." + str_lates_amount_1.Split('.')[1].Substring(0, 11);
                    lates_amount                = decimal.Parse(str_lates_amount_1) * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                    
                    amount_due_hidden = (double.Parse(txtb_rate_amount.Text) * double.Parse(txtb_no_days_worked.Text.ToString())) ;
                    amount_due_hidden = amount_due_hidden - double.Parse(lates_amount.ToString("###,##0.00"));
                    break;
                case "M":
                    lates_amount      = decimal.Parse(hidden_daily_rate.Value.ToString()) * (lates_time / decimal.Parse(hidden_hrs_in1day.Value));
                    
                    late_amount1                = decimal.Parse(txtb_rate_amount.Text.ToString()) / decimal.Parse(hidden_hrs_in1day.Value) / 60;
                    str_lates_amount_1          = "";
                    str_lates_amount_1          = late_amount1.ToString("###,##0.000000000000");
                    str_lates_amount_1          = str_lates_amount_1.Split('.')[0] + "." + str_lates_amount_1.Split('.')[1].Substring(0, 11);
                    lates_amount                = decimal.Parse(str_lates_amount_1) * decimal.Parse(txtb_lates_and_undertime.Text.ToString());
                    
                    amount_due_hidden = (double.Parse(txtb_rate_amount.Text)) ;
                    amount_due_hidden = amount_due_hidden - double.Parse(lates_amount.ToString("###,##0.00"));
                    break;
            }
            txtb_amount_due_hidden.Text = amount_due_hidden.ToString("###,##0.00");
        }

        protected void txtb_lates_and_undertime_TextChanged(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                CheckIfNotEqualto_AmountDue();
                
                Update_lates_and_undertime.Update();
                txtb_lates_and_undertime.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                txtb_lates_and_undertime.Focus();
            }
        }

        protected void txtb_rate_amount_TextChanged(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                CheckIfNotEqualto_AmountDue();

                Update_rate_amount.Update();
                txtb_rate_amount.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                txtb_rate_amount.Focus();
            }
        }

        private void Calculate_Hours_OT()
        {
            double ot_amt       = 0;
            double ot_sal       = 0;
            double ot_amt_010   = 0;
            double ot_sal_010   = 0;

            switch (hidden_rate_basis.Value.ToString().Trim())
            {
                case "H":
                    ot_amt       = double.Parse(txtb_no_hours_ot.Text)      * double.Parse(txtb_rate_amount.Text.ToString());
                    ot_sal       = double.Parse(txtb_no_hours_sal.Text)     * double.Parse(txtb_rate_amount.Text.ToString());
                    ot_amt_010   = double.Parse(txtb_no_hours_ot_010.Text)  * double.Parse(txtb_rate_amount.Text.ToString());
                    ot_sal_010   = double.Parse(txtb_no_hours_sal_010.Text) * double.Parse(txtb_rate_amount.Text.ToString());
                    break;
                case "D":
                   
                    ot_amt       = (double.Parse(txtb_no_hours_ot.Text).ToString()      == "" ? 0 :double.Parse(txtb_no_hours_ot.Text)     ) * (double.Parse(txtb_rate_amount.Text.ToString()) / double.Parse(hidden_hrs_in1day.Value));
                    ot_sal       = (double.Parse(txtb_no_hours_sal.Text).ToString()     == "" ? 0 :double.Parse(txtb_no_hours_sal.Text)    ) * (double.Parse(txtb_rate_amount.Text.ToString()) / double.Parse(hidden_hrs_in1day.Value));
                    ot_amt_010   = (double.Parse(txtb_no_hours_ot_010.Text).ToString()  == "" ? 0 :double.Parse(txtb_no_hours_ot_010.Text) ) * (double.Parse(txtb_rate_amount.Text.ToString()) / double.Parse(hidden_hrs_in1day.Value));
                    ot_sal_010   = (double.Parse(txtb_no_hours_sal_010.Text).ToString() == "" ? 0 :double.Parse(txtb_no_hours_sal_010.Text)) * (double.Parse(txtb_rate_amount.Text.ToString()) / double.Parse(hidden_hrs_in1day.Value));

                    if (ot_amt == 0)
                    {
                        ot_sal = double.Parse(txtb_rate_amount.Text.ToString()) * double.Parse(txtb_no_days_worked.Text);
                    }

                    if (ot_amt_010 == 0)
                    {
                        ot_sal_010 = double.Parse(txtb_rate_amount.Text.ToString()) * double.Parse(txtb_no_days_worked.Text);
                    }
                    
                    break;
                case "M":
                    ot_amt       = double.Parse(txtb_no_hours_ot.Text)      * double.Parse(hidden_hourly_rate.Value.ToString());
                    ot_sal       = double.Parse(txtb_no_hours_sal.Text)     * double.Parse(hidden_hourly_rate.Value.ToString());
                    ot_amt_010   = double.Parse(txtb_no_hours_ot_010.Text)  * double.Parse(hidden_hourly_rate.Value.ToString());
                    ot_sal_010   = double.Parse(txtb_no_hours_sal_010.Text) * double.Parse(hidden_hourly_rate.Value.ToString());
                    break;
            }

            txtb_no_hours_ot_amt.Text       = ot_amt.ToString("###,##0.00");
            txtb_no_hours_sal_amt.Text      = ot_sal.ToString("###,##0.00");
            txtb_no_hours_ot_amt_010.Text   = ot_amt_010.ToString("###,##0.00");
            txtb_no_hours_sal_amt_010.Text  = ot_sal_010.ToString("###,##0.00");
        }

        protected void txtb_no_hours_ot_TextChanged(object sender, EventArgs e)
        {
            Calculate_Hours_OT();
            Update_txtb_no_hours_ot.Update();
            txtb_no_hours_ot.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_no_hours_ot.Focus();
        }

        protected void txtb_no_hours_sal_TextChanged(object sender, EventArgs e)
        {
            Calculate_Hours_OT();
            Update_txtb_no_hours_sal.Update();
            txtb_no_hours_sal.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_no_hours_sal.Focus();
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

            DataTable dt = MyCmn.RetrieveData("payrollregistry_dtl_othded_chk", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString(), "par_payroll_year", ddl_year.SelectedValue.ToString(), "par_payroll_month", ddl_month.SelectedValue.ToString(), "par_payroll_registry_nbr", lbl_registry_number.Text.ToString(), "par_empl_id", txtb_empl_id.Text.ToString(), "par_seq_no", "");
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
                                            + "payrolltemplate_code = '" + ddl_payroll_template.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_year= '" + ddl_year.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_month= '" + ddl_month.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_registry_nbr= '" + lbl_registry_number.Text.ToString() + "'"
                                            + "AND " + "empl_id= '" + txtb_empl_id.Text + "'";

                    MyCmn.Update_InsertTable(insert_update_script);
                }
                // INSERT DATA TO OTHER DEDUCTIONS
                else if (dt.Rows[0]["flag_return"].ToString() == "I")
                {
                    insert_update_script = "insert into payrollregistry_dtl_othded_tbl select "
                                         + "'" + ddl_payroll_template.SelectedValue.ToString() + "'"
                                         + "," + "'" + ddl_year.SelectedValue.ToString() + "'"
                                         + "," + "'" + ddl_month.SelectedValue.ToString() + "'"
                                         + "," + "'" + lbl_registry_number.Text.ToString() + "'"
                                         + "," + "'" + txtb_empl_id.Text + "'"
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