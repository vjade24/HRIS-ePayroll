//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Payroll for Hazard, Subsistence and Laundry
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Joseph M. Tombo Jr    03/26/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View
{
    public partial class cPayHazardSubsistenceLaundry : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Data Place holder creation 
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

        DataTable dt_rata_rate_sked_list
        {
            get
            {
                if ((DataTable)ViewState["dt_rata_rate_sked_list"] == null) return null;
                return (DataTable)ViewState["dt_rata_rate_sked_list"];
            }
            set
            {
                ViewState["dt_rata_rate_sked_list"] = value;
            }
        }

        DataTable dt_noluandry_tbl_list
        {
            get
            {
                if ((DataTable)ViewState["dt_noluandry_tbl_list"] == null) return null;
                return (DataTable)ViewState["dt_noluandry_tbl_list"];
            }
            set
            {
                ViewState["dt_noluandry_tbl_list"] = value;
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

                ViewState["page_allow_add"] = Session["page_allow_add_from_registry"];
                ViewState["page_allow_delete"] = Session["page_allow_delete_from_registry"];
                ViewState["page_allow_edit"] = Session["page_allow_edit_from_registry"];
                ViewState["page_allow_edit_history"] = Session["page_allow_edit_history_from_registry"];
                ViewState["page_allow_print"] = Session["page_allow_print_from_registry"];


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
                    RetrieveEmployeename();
                    RetrieveDataListGrid();
                    RetrieveReserveDeduction();
                }

                // BEGIN - VJA : 02/01/2020 - Hide the ADD BUTTON IF THE STATUS ARE ;
                    // 1. ) R = Released
                    // 2. ) Y = Posted
                    // 3. ) T = Return
                //if (Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "R" ||
                //    Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "Y" ||
                //    Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "T"   )
                //{
                //    ViewState["page_allow_add"] = 0;
                //}
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
            Session["cPayReportGrouping"] = "cPayReportGrouping";

            RetrieveDataListGrid();
            RetrieveEmploymentType();

            //Retrieve When Add
            RetrieveEmployeename();
            RetrieveLaundryRef();

        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private float RetrieveInstallation_hrs_in1day()
        {
            string sql1 = "SELECT TOP (1) hours_in_1day_conv FROM payrollinstallation_tbl WHERE payroll_year = '" + ddl_year.SelectedValue + "' AND employment_type = '" + ddl_empl_type.SelectedValue.ToString().Trim() + "' AND effective_date <='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY effective_date DESC";
            string last_row = "";
            last_row = MyCmn.GetLastRow_of_Table(sql1);
            if (last_row.Trim() == "")
            {
                last_row = "0.00";
            }
            hidden_hours_in_days.Value = last_row;
            return float.Parse(last_row);
        }


        //*************************************************************************
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private float RetrieveInstallation_days_month()
        {
            string sql1 = "SELECT TOP (1) monthly_salary_days_conv FROM payrollinstallation_tbl WHERE payroll_year = '" + ddl_year.SelectedValue + "' AND employment_type = '" + ddl_empl_type.SelectedValue.ToString().Trim() + "' AND effective_date <='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY effective_date DESC";
            string last_row = "";
            last_row = MyCmn.GetLastRow_of_Table(sql1);
            if (last_row.Trim() == "")
            {
                last_row = "0";
            }

            hidden_monthly_days.Value = last_row;
            return float.Parse(last_row);
        }

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

        private void RetrieveEmployeename()
        {
            ddl_empl_id.Items.Clear();
            dataList_employee = MyCmn.RetrieveData("sp_personnelnames_combolist_subs", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_templatecode", ddl_payroll_template.SelectedValue.ToString().Trim());
            ddl_empl_id.DataSource = dataList_employee;
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }

        private void RetrieveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");
            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataBind();
            ddl_empl_type.Enabled = false;
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 10/26/2019 This is to get the list of no laundry reference table
        //*************************************************************************
        private void RetrieveLaundryRef()
        {
            dt_noluandry_tbl_list = MyCmn.RetrieveData("sp_nolaundry_ref_tbl_list");
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


        //**************************************************************************
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
        //**************************************************************************
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

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_dtl_subs_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR(), "par_employment_type",ddl_empl_type.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();

            RetrieveEmployeename();
            ddl_empl_id.Enabled = true;
            ddl_empl_id.Visible = true;
            txtb_empl_name.Visible = false;
            btnSave.Visible = true;

            txtb_voucher_nbr.Enabled = false;
            lbl_if_dateposted_yes.Text = "";
            ToogleTextbox(true);

            LabelAddEdit.Text = "Add Record | Registry No: " + lbl_registry_number.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL", "0");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }


        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_monthly_rate.Text          = "0.00";
            txtb_daily_rate.Text            = "0.00";

            txtb_hazard_perc.Text           = "0";
            txtb_no_days_wordked.Text       = "0";
            txtb_no_days_subsistence.Text   = "0";
            txtb_no_days_wordked.Text       = "0";
            txtb_with_laundry.Text          = "0";
            txtb_working_days.Text          = "0";
            txtb_withhel_tax_perc.Text      = "0";
            txtb_remarks.Text               = "";

            txtb_luandry_daily_dspl.Text    = "0.00";
            txtb_subs_daily_dspl.Text       = "0.00";

            txtb_net_hazard_amount.Text     = "0.00";
            txtb_net_laundry_amount.Text    = "0.00";
            txtb_net_subs_amount.Text       = "0.00";

            txtb_gross_pay.Text             = "0.00";
            txtb_net_pay.Text               = "0.00";

            txtb_with_tax_held_amount.Text  = "0.00";
            txtb_ccmpc_loan.Text            = "0.00";
            txtb_nico_loan.Text             = "0.00";
            txtb_network_bank_loan.Text     = "0.00";
            txtb_other_loan.Text            = "0.00";
            txtb_days_not_exposed.Text      = "0";
            txtb_department_descr.Text      = "";
            txtb_department_code.Text       = "";
            txtb_empl_id.Text               = "";
            lbl_post_status.Text            = "";

            //Added by: Jorge
            txtb_voucher_nbr.Text = "";
            ViewState["created_by_user"] = "";
            ViewState["updated_by_user"] = "";
            ViewState["posted_by_user"] = "";
            ViewState["created_dttm"] = "";
            ViewState["updated_dttm"] = "";
            txtb_date_posted.Text = "";
            txtb_department_descr.Text = "";
            txtb_position.Text = "";
            txtb_status.Text = "";

            
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

            ddl_empl_id.SelectedIndex       = -1;
            FieldValidationColorChanged(false, "ALL","");



        }

        private void InitializeTable_dtl()
        {
            dtSource_dtl = new DataTable();
            dtSource_dtl.Columns.Add("payroll_year", typeof(System.String));
            dtSource_dtl.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl.Columns.Add("monthly_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("daily_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("days_working", typeof(System.String));
            dtSource_dtl.Columns.Add("gross_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("net_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("days_plohuh", typeof(System.String));
            dtSource_dtl.Columns.Add("days_subsistence", typeof(System.String));
            dtSource_dtl.Columns.Add("days_laundry", typeof(System.String));
            dtSource_dtl.Columns.Add("hazard_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("hazard_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("subsistence_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("laundry_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("nico_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("network_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("ccmpc_ln", typeof(System.String));
            dtSource_dtl.Columns.Add("other_loan", typeof(System.String));
            dtSource_dtl.Columns.Add("remarks", typeof(System.String));
            dtSource_dtl.Columns.Add("post_status", typeof(System.String));
            dtSource_dtl.Columns.Add("date_posted", typeof(System.String));
            dtSource_dtl.Columns.Add("days_not_exposed", typeof(System.String));
            //dtSource_dtl.Columns.Add("voucher_nbr", typeof(System.String));
            //dtSource_dtl.Columns.Add("posted_by_user", typeof(System.String));
            //Updated   By: Joseph M. Tombo Jr
            //Updated Date: 11/11/2019
            dtSource_dtl.Columns.Add("created_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("created_dttm", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_dttm", typeof(System.String));
        }

        private void AddPrimaryKeys_dtl()
        {
            dtSource_dtl.TableName = "payrollregistry_dtl_subs_tbl";
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr", "empl_id" };
            dtSource_dtl = MyCmn.AddPrimaryKeys(dtSource_dtl, col);
            dtSource_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));
        }

        private void AddNewRow_dtl()
        {
            DataRow nrow = dtSource_dtl.NewRow();
            nrow["payroll_year"]            = string.Empty;
            nrow["payroll_registry_nbr"]    = string.Empty;
            nrow["empl_id"]                 = string.Empty;
            nrow["monthly_rate"]            = string.Empty;
            nrow["daily_rate"]              = string.Empty;
            nrow["days_working"]            = string.Empty;
            nrow["gross_pay"]               = string.Empty;
            nrow["net_pay"]                 = string.Empty;
            nrow["days_plohuh"]             = string.Empty;
            nrow["days_subsistence"]        = string.Empty;
            nrow["days_laundry"]            = string.Empty;
            nrow["hazard_rate"]             = string.Empty;
            nrow["hazard_pay"]              = string.Empty;
            nrow["subsistence_pay"]         = string.Empty;
            nrow["laundry_pay"]             = string.Empty;
            nrow["bir_tax"]                 = string.Empty;
            nrow["bir_tax_rate"]            = string.Empty;
            nrow["nico_ln"]                 = string.Empty;
            nrow["network_ln"]              = string.Empty;
            nrow["ccmpc_ln"]                = string.Empty;
            nrow["other_loan"]              = string.Empty;
            nrow["remarks"]                 = string.Empty;
            nrow["post_status"]             = string.Empty;
            nrow["days_not_exposed"]        = string.Empty;
            nrow["action"]                  = 1;
            nrow["retrieve"]                = false;

            dtSource_dtl.Rows.Add(nrow);
        }
        

        //***************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {

            FieldValidationColorChanged(false, "ALL", "0");
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year = commandArgs[2];
            txtb_reason.Text = "";
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
                MyCmn.DeleteBackEndData("payrollregistry_dtl_subs_tbl", "WHERE " + deleteExpression);
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
                    FieldValidationColorChanged(true, "txtb_reason", "0");
                }
                else
                {
                    // Stored Procedure to Insert to payrollregistry_dtl_unpost_tbl during accounting case
                    DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_unpost_tbl_insert", "par_payroll_year", ddl_year.SelectedValue, "par_payroll_registry_nbr", commandarg[1].Trim(), "par_empl_id", commandarg[0].Trim(), "par_reason", txtb_reason.Text);

                    //4.4.b.Update the following fields: From payrollregistry_dtl_rata_tbl Table
                    //    1.voucher_nbr     =   { blank}
                    //    2.posted_by_user  =   { blank}
                    //    3.post_status     =   "N"   
                    //    4.date_posted     =   { blank}
                    //    5.updated_by_user =   session user_id   
                    //    6.updated_dttm    =   System Date

                    string setparams = "";
                    setparams = "voucher_nbr = '',posted_by_user = '',post_status='N',date_posted='' , updated_by_user='" + Session["ep_user_id"].ToString() + "', updated_dttm='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    MyCmn.UpdateTable("payrollregistry_dtl_subs_tbl", setparams, "WHERE " + deleteExpression);
                    RetrieveDataListGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
                }
            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        ////*************************************************************************
        ////  BEGIN - JOSEPH- 03/20/2019 - Delete Data to back-end Database
        ////*************************************************************************
        //protected void btnDelete_Command(object sender, CommandEventArgs e)
        //{
        //    string[] group_no1 = e.CommandArgument.ToString().Split(new char[] { ',' });
        //    string deleteExpression = "empl_id = '" + group_no1[0].Trim() + "' AND payroll_year='" + group_no1[1].Trim() + "' AND payroll_registry_nbr ='" + group_no1[2].Trim() + "'";

        //    MyCmn.DeleteBackEndData("payrollregistry_dtl_subs_tbl", "WHERE " + deleteExpression);

        //    DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
        //    dataListGrid.Rows.Remove(row2Delete[0]);
        //    dataListGrid.AcceptChanges();
        //    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
        //    up_dataListGrid.Update();
        //    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        //}

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
            dtSource_dtl.Rows[0]["action"] = 2;
            dtSource_dtl.Rows[0]["retrieve"] = true;

            lbl_registry_number.Text            = row2Edit[0]["payroll_registry_nbr"].ToString().Trim();
            ddl_empl_id.Enabled                 = false;
            ddl_empl_id.Visible                 = false;
            txtb_empl_name.Visible              = true;

            txtb_empl_name.Text                 = row2Edit[0]["employee_name"].ToString().Trim();
            txtb_empl_id.Text                   = row2Edit[0]["empl_id"].ToString().Trim();
            txtb_monthly_rate.Text              = row2Edit[0]["monthly_rate"].ToString().Trim();
            txtb_daily_rate.Text                = row2Edit[0]["daily_rate"].ToString().Trim();
            txtb_working_days.Text              = row2Edit[0]["days_working"].ToString().Trim();
            txtb_no_days_subsistence.Text       = row2Edit[0]["days_subsistence"].ToString().Trim();
            txtb_with_laundry.Text              = row2Edit[0]["days_laundry"].ToString().Trim();
            txtb_no_days_wordked.Text           = row2Edit[0]["days_plohuh"].ToString().Trim();
            txtb_hazard_perc.Text               = row2Edit[0]["hazard_rate"].ToString().Trim();
            txtb_withhel_tax_perc.Text          = row2Edit[0]["bir_tax_rate"].ToString().Trim();
            txtb_remarks.Text                   = row2Edit[0]["remarks"].ToString().Trim();

            txtb_department_descr.Text          = row2Edit[0]["department_name1"].ToString().Trim();
            txtb_position.Text                  = row2Edit[0]["position_title1"].ToString().Trim();
            txtb_department_code.Text           = row2Edit[0]["department_code"].ToString().Trim();

            txtb_luandry_daily_dspl.Text        = row2Edit[0]["laundry_daily_rate"].ToString().Trim();
            txtb_subs_daily_dspl.Text           = row2Edit[0]["subsistence_daily_rate"].ToString().Trim();

            txtb_net_hazard_amount.Text         = row2Edit[0]["hazard_pay"].ToString().Trim();
            txtb_net_subs_amount.Text           = row2Edit[0]["subsistence_pay"].ToString().Trim();
            txtb_net_laundry_amount.Text        = row2Edit[0]["laundry_pay"].ToString().Trim();

            txtb_gross_pay.Text                 = double.Parse(row2Edit[0]["gross_pay"].ToString().Trim()).ToString("###,##0.00");

            txtb_with_tax_held_amount.Text      = row2Edit[0]["bir_tax"].ToString().Trim();
            txtb_nico_loan.Text                 = row2Edit[0]["nico_ln"].ToString().Trim();
            txtb_ccmpc_loan.Text                = row2Edit[0]["ccmpc_ln"].ToString().Trim();
            txtb_network_bank_loan.Text         = row2Edit[0]["network_ln"].ToString().Trim();
            txtb_other_loan.Text                = row2Edit[0]["other_loan"].ToString().Trim();

            hidden_laundry_max_amount.Value     = row2Edit[0]["laundry_max_amount"].ToString().Trim();
            hidden_subs_max_amount.Value        = row2Edit[0]["subsistence_max_amount"].ToString().Trim();
            txtb_days_not_exposed.Text          = row2Edit[0]["days_not_exposed"].ToString().Trim();
            
            dtSource_dtl.Rows[0]["created_by_user"] = row2Edit[0]["created_by_user"].ToString().Trim();
            dtSource_dtl.Rows[0]["updated_by_user"] = row2Edit[0]["updated_by_user"].ToString().Trim();
            dtSource_dtl.Rows[0]["created_dttm"]    = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
            dtSource_dtl.Rows[0]["updated_dttm"]    = Convert.ToDateTime(row2Edit[0]["updated_dttm"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");

            txtb_net_pay.Text                   = double.Parse(row2Edit[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");

            dtSource_dtl.Rows[0]["post_status"] = row2Edit[0]["post_status"].ToString().Trim();

            
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


            if (ddl_empl_type.SelectedValue.ToString().Trim() == "RE")
            {
                txtb_working_days.Enabled = false;
            }
            else if (ddl_empl_type.SelectedValue.ToString().Trim() == "CE" || ddl_empl_type.SelectedValue.ToString().Trim() == "JO")
            {
                txtb_working_days.Enabled = true;
            }

            if (row2Edit[0]["post_status"].ToString().ToUpper() == "Y")
            {
                lbl_if_dateposted_yes.Text = "This Payroll is already Posted You connot Edit!";
                btnSave.Visible = false;
            }

            else { lbl_if_dateposted_yes.Text = ""; btnSave.Visible = true; }
            LabelAddEdit.Text = "Edit Record | Registry No: " + lbl_registry_number.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ddl_empl_id.Enabled = false;
            FieldValidationColorChanged(false, "ALL", "0");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            // Add Field Again - 06/20/2019
            //txtb_voucher_nbr.Text = row2Edit[0]["voucher_nbr"].ToString();
            //ViewState["created_by_user"] = row2Edit[0]["created_by_user"].ToString();
            //ViewState["updated_by_user"] = row2Edit[0]["updated_by_user"].ToString();
            //ViewState["posted_by_user"] = row2Edit[0]["posted_by_user"].ToString();
            //ViewState["created_dttm"] = row2Edit[0]["created_dttm"].ToString();
            //ViewState["updated_dttm"] = row2Edit[0]["updated_dttm"].ToString();
            txtb_status.Text        = row2Edit[0]["post_status_descr"].ToString();
            txtb_date_posted.Text   = row2Edit[0]["date_posted"].ToString().Trim();
            lbl_post_status.Text    = row2Edit[0]["post_status_descr"].ToString();

            //FOR POSTED STATUS
            if (row2Edit[0]["post_status"].ToString().ToUpper() == "Y" && (Session["ep_post_authority"].ToString() == "1" || Session["ep_post_authority"].ToString() == "0"))
            {
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                txtb_voucher_nbr.Enabled = false;
                Linkbtncancel.Text = "Close";
                lbl_if_dateposted_yes.Text = "This Payroll is already Posted, You Cannot Edit!";
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }

            //FOR USER'S IN ACCOUNTING
            else if (row2Edit[0]["post_status"].ToString() == "N" && Session["ep_post_authority"].ToString() == "1")
            {
                btnSave.Visible = true;
                btn_calculate.Visible = false;
                btnSave.Text = "Post To Card";
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = true;
                txtb_date_posted.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }
            else if (row2Edit[0]["post_status"].ToString()   == "R"
                    //|| row2Edit[0]["post_status"].ToString() == "T"
                    || row2Edit[0]["post_status"].ToString() == "X"
                    )
            {
                // For Accounting With N Status
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                Linkbtncancel.Text = "Close";
                txtb_voucher_nbr.Enabled = false;
                lbl_if_dateposted_yes.Text = "This Payroll Already "+ row2Edit[0]["post_status_descr"].ToString() + ", You cannot Edit!";
                btnSave.Text = "Save";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }

            //FOR OTHER DEPARTMENTS
            else if (Session["ep_post_authority"].ToString() == "0")
            {
                txtb_voucher_nbr.Enabled = false;
                btnSave.Visible = true;
                btn_calculate.Visible = true;
                btnSave.Text = "Save";
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = "";
                ToogleTextbox(true);
            }
            calculate_gross_only();
            LabelforWorkedDays();
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
            FieldValidationColorChanged(false, "ALL", "0");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;
            calculate_gross_only();
            if (IsDataValidated())
            {
                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource_dtl.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString();
                    dtSource_dtl.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().ToString();
                    dtSource_dtl.Rows[0]["daily_rate"]              = txtb_daily_rate.Text.ToString().ToString();
                    dtSource_dtl.Rows[0]["days_working"]            = txtb_working_days.Text.ToString().ToString();
                    dtSource_dtl.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["days_plohuh"]             = txtb_no_days_wordked.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["days_subsistence"]        = txtb_no_days_subsistence.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["days_laundry"]            = txtb_with_laundry.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hazard_rate"]             = txtb_hazard_perc.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hazard_pay"]              = txtb_net_hazard_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["subsistence_pay"]         = txtb_net_subs_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["laundry_pay"]             = txtb_net_laundry_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax"]                 = txtb_with_tax_held_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax_rate"]            = txtb_withhel_tax_perc.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["network_ln"]              = txtb_network_bank_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_loan"]              = txtb_other_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["remarks"]                 = txtb_remarks.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["post_status"]             = "N";
                    dtSource_dtl.Rows[0]["days_not_exposed"]        = txtb_days_not_exposed.Text.ToString().Trim();

                    // BEGIN - Add Field Again  - 06/20/2019
                    //dtSource_dtl.Rows[0]["voucher_nbr"] = txtb_voucher_nbr.Text.ToString();
                    dtSource_dtl.Rows[0]["created_by_user"] = Session["ep_user_id"].ToString();
                    dtSource_dtl.Rows[0]["updated_by_user"] = "";
                    dtSource_dtl.Rows[0]["created_dttm"]    = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource_dtl.Rows[0]["updated_dttm"]    = "1900-01-01";
                    //dtSource_dtl.Rows[0]["posted_by_user"]  = "";
                    //dtSource_dtl.Rows[0]["date_posted"] = "";
                    // END - Add Field Again  - 06/20/2019


                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource_dtl);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource_dtl.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString();
                    dtSource_dtl.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().ToString();
                    dtSource_dtl.Rows[0]["daily_rate"]              = txtb_daily_rate.Text.ToString().ToString();
                    dtSource_dtl.Rows[0]["days_working"]            = txtb_working_days.Text.ToString().ToString();
                    dtSource_dtl.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["days_plohuh"]             = txtb_no_days_wordked.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["days_subsistence"]        = txtb_no_days_subsistence.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["days_laundry"]            = txtb_with_laundry.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hazard_rate"]             = txtb_hazard_perc.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hazard_pay"]              = txtb_net_hazard_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["subsistence_pay"]         = txtb_net_subs_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["laundry_pay"]             = txtb_net_laundry_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax"]                 = txtb_with_tax_held_amount.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax_rate"]            = txtb_withhel_tax_perc.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["network_ln"]              = txtb_network_bank_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["other_loan"]              = txtb_other_loan.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["remarks"]                 = txtb_remarks.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["days_not_exposed"]        = txtb_days_not_exposed.Text.ToString().Trim();
                    // BEGIN - Add Field Again  - 06/26/2019

                    //dtSource_dtl.Rows[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                    dtSource_dtl.Rows[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource_dtl.Rows[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //dtSource_dtl.Rows[0]["created_dttm"]            = ViewState["created_dttm"].ToString();

                    if (Session["ep_post_authority"].ToString().Trim() == "1")
                    {
                        dtSource_dtl.Rows[0]["posted_by_user"]      = Session["ep_user_id"].ToString();
                        dtSource_dtl.Rows[0]["date_posted"]         = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dtSource_dtl.Rows[0]["post_status"]         = "Y";
                        dtSource_dtl.Rows[0]["voucher_nbr"]         = txtb_voucher_nbr.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["updated_by_user"]     = ViewState["updated_by_user"].ToString();
                        dtSource_dtl.Rows[0]["updated_dttm"]        = ViewState["updated_dttm"].ToString();
                    }
                    // END - Add Field Again  - 06/26/2019

                    scriptInsertUpdate = MyCmn.updatescript(dtSource_dtl);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    if (scriptInsertUpdate == string.Empty) { return; }
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") { return; }


                    if (msg.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist", "0");
                        return;
                    }
                    InsertUpdateOtherDeduction();

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();

                        nrow["payroll_year"]            = dtSource_dtl.Rows[0]["payroll_year"];
                        nrow["payroll_registry_nbr"]    = dtSource_dtl.Rows[0]["payroll_registry_nbr"];
                        nrow["empl_id"]                 = dtSource_dtl.Rows[0]["empl_id"];
                        nrow["employee_name"]           = ddl_empl_id.SelectedItem.Text.ToString().Trim();
                        nrow["monthly_rate"]            = dtSource_dtl.Rows[0]["monthly_rate"];
                        nrow["daily_rate"]              = dtSource_dtl.Rows[0]["daily_rate"];
                        nrow["days_working"]            = txtb_working_days.Text.ToString().ToString();
                        nrow["gross_pay"]               = dtSource_dtl.Rows[0]["gross_pay"];
                        nrow["net_pay"]                 = dtSource_dtl.Rows[0]["net_pay"];
                        nrow["subsistence_max_amount"]  = hidden_subs_max_amount.Value;

                        nrow["subsistence_daily_rate"]  = txtb_subs_daily_dspl.Text.ToString().Trim();
                        nrow["laundry_daily_rate"]      = txtb_luandry_daily_dspl.Text.ToString().Trim();
                     

                        nrow["days_plohuh"]             = dtSource_dtl.Rows[0]["days_plohuh"];
                        nrow["days_subsistence"]        = dtSource_dtl.Rows[0]["days_subsistence"];
                        nrow["days_laundry"]            = dtSource_dtl.Rows[0]["days_laundry"];
                        nrow["hazard_rate"]             = dtSource_dtl.Rows[0]["hazard_rate"];
                        nrow["hazard_pay"]              = dtSource_dtl.Rows[0]["hazard_pay"];
                        nrow["subsistence_pay"]         = dtSource_dtl.Rows[0]["subsistence_pay"];
                        nrow["laundry_pay"]             = dtSource_dtl.Rows[0]["laundry_pay"];
                        nrow["bir_tax"]                 = dtSource_dtl.Rows[0]["bir_tax"];
                        nrow["bir_tax_rate"]            = dtSource_dtl.Rows[0]["bir_tax_rate"];
                        nrow["nico_ln"]                 = dtSource_dtl.Rows[0]["nico_ln"];
                        nrow["network_ln"]              = dtSource_dtl.Rows[0]["network_ln"];
                        nrow["ccmpc_ln"]                = dtSource_dtl.Rows[0]["ccmpc_ln"];
                        nrow["other_loan"]              = dtSource_dtl.Rows[0]["other_loan"];
                        nrow["remarks"]                 = dtSource_dtl.Rows[0]["remarks"];

                        nrow["post_status"]             = "N";
                        nrow["post_status_descr"]       = "NOT POSTED";
                        nrow["days_not_exposed"]         = txtb_days_not_exposed.Text.ToString().Trim();
                        // BEGIN - Add Field Again  - 06/20/2019
                        //nrow["voucher_nbr"] = txtb_voucher_nbr.Text.ToString();
                        nrow["created_by_user"] = Session["ep_user_id"].ToString();
                        nrow["updated_by_user"] = "";
                        nrow["created_dttm"]    = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //nrow["posted_by_user"]  = "";
                        //nrow["date_posted"] = "";
                        nrow["position_title1"] = txtb_position.Text.ToString().Trim();
                        nrow["department_name1"] = txtb_department_descr.Text.ToString().Trim();
                        // END - Add Field Again  - 06/20/2019
                        nrow["updated_dttm"] = "1900-01-01";

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

                        nrow["department_code"]      = txtb_department_code.Text.ToString().Trim();


                        dataListGrid.Rows.Add(nrow);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim()+ "' AND payroll_year='" + ddl_year.SelectedValue.ToString().Trim() + "' AND payroll_registry_nbr ='" + lbl_registry_number.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        //Employee Name or the employee is not change so we dont need to move this values;
                        //row2Edit[0]["payroll_year"]         = dtSource_dtl.Rows[0]["payroll_year"];
                        //row2Edit[0]["payroll_registry_nbr"] = dtSource_dtl.Rows[0]["payroll_registry_nbr"];
                        //row2Edit[0]["empl_id"]              = dtSource_dtl.Rows[0]["empl_id"];
                        //row2Edit[0]["employee_name"]        = txtb_empl_name.Text;

                        row2Edit[0]["monthly_rate"]           = dtSource_dtl.Rows[0]["monthly_rate"];
                        row2Edit[0]["daily_rate"]             = dtSource_dtl.Rows[0]["daily_rate"];
                        row2Edit[0]["days_working"]           = txtb_working_days.Text.ToString().ToString();
                        row2Edit[0]["gross_pay"]              = dtSource_dtl.Rows[0]["gross_pay"];
                        row2Edit[0]["net_pay"]                = dtSource_dtl.Rows[0]["net_pay"];
                        row2Edit[0]["days_plohuh"]            = dtSource_dtl.Rows[0]["days_plohuh"];
                        row2Edit[0]["days_subsistence"]       = dtSource_dtl.Rows[0]["days_subsistence"];
                        row2Edit[0]["days_laundry"]           = dtSource_dtl.Rows[0]["days_laundry"];
                        row2Edit[0]["subsistence_daily_rate"] = txtb_subs_daily_dspl.Text.ToString().Trim();
                        row2Edit[0]["subsistence_max_amount"] = hidden_subs_max_amount.Value;
                        row2Edit[0]["laundry_daily_rate"]     = txtb_luandry_daily_dspl.Text.ToString().Trim();
                        row2Edit[0]["hazard_rate"]            = dtSource_dtl.Rows[0]["hazard_rate"];
                        row2Edit[0]["hazard_pay"]             = dtSource_dtl.Rows[0]["hazard_pay"];
                        row2Edit[0]["subsistence_pay"]        = dtSource_dtl.Rows[0]["subsistence_pay"];
                        row2Edit[0]["laundry_pay"]            = dtSource_dtl.Rows[0]["laundry_pay"];
                        row2Edit[0]["bir_tax"]                = dtSource_dtl.Rows[0]["bir_tax"];
                        row2Edit[0]["bir_tax_rate"]           = dtSource_dtl.Rows[0]["bir_tax_rate"];
                        row2Edit[0]["nico_ln"]                = dtSource_dtl.Rows[0]["nico_ln"];
                        row2Edit[0]["network_ln"]             = dtSource_dtl.Rows[0]["network_ln"];
                        row2Edit[0]["ccmpc_ln"]               = dtSource_dtl.Rows[0]["ccmpc_ln"];
                        row2Edit[0]["other_loan"]             = dtSource_dtl.Rows[0]["other_loan"];
                        row2Edit[0]["remarks"]                = dtSource_dtl.Rows[0]["remarks"];
                        row2Edit[0]["post_status"]            = dtSource_dtl.Rows[0]["post_status"];
                        row2Edit[0]["position_title1"]        = txtb_position.Text.ToString().Trim();
                        row2Edit[0]["department_name1"]       = txtb_department_descr.Text.ToString().Trim();
                        row2Edit[0]["days_not_exposed"]       = txtb_days_not_exposed.Text.ToString().Trim();
                        // BEGIN - Add Field Again - 06/20/2019
                        //row2Edit[0]["created_by_user"]      = ViewState["created_by_user"].ToString();
                        row2Edit[0]["updated_by_user"]        = Session["ep_user_id"].ToString();
                        //row2Edit[0]["created_dttm"]         = ViewState["created_dttm"].ToString() == "" ? DateTime.Now : ViewState["created_dttm"];
                        row2Edit[0]["updated_dttm"]           = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        
                            
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

                        // END - Add Field Again  - 06/20/2019
                        //if (Session["ep_post_authority"].ToString() == "1")
                        //{
                        //    row2Edit[0]["posted_by_user"]   = Session["ep_user_id"].ToString();
                        //    row2Edit[0]["date_posted"]      = txtb_date_posted.Text.ToString().Trim();
                        //    row2Edit[0]["post_status"]      = "Y";
                        //    row2Edit[0]["post_status_descr"]= "POSTED";
                        //    row2Edit[0]["voucher_nbr"]      = txtb_voucher_nbr.Text.ToString();
                        //    row2Edit[0]["updated_by_user"]  = ViewState["updated_by_user"].ToString();
                        //    row2Edit[0]["updated_dttm"]     = ViewState["updated_dttm"].ToString();
                        //}
                        row2Edit[0]["department_code"]       = txtb_department_code.Text.ToString().Trim();

                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }

                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                }

            }
        }

        //**************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "") 
                + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "") 
                + "%' OR position_title1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
                + "%' OR post_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
                + "%' OR net_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
                + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("daily_rate", typeof(System.String));
            dtSource1.Columns.Add("days_working", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("days_plohuh", typeof(System.String));
            dtSource1.Columns.Add("days_subsistence", typeof(System.String));
            dtSource1.Columns.Add("days_laundry", typeof(System.String));
            dtSource1.Columns.Add("hazard_rate", typeof(System.String));
            dtSource1.Columns.Add("hazard_pay", typeof(System.String));
            dtSource1.Columns.Add("subsistence_pay", typeof(System.String));
            dtSource1.Columns.Add("laundry_pay", typeof(System.String));
            dtSource1.Columns.Add("bir_tax", typeof(System.String));
            dtSource1.Columns.Add("nico_ln", typeof(System.String));
            dtSource1.Columns.Add("network_ln", typeof(System.String));
            dtSource1.Columns.Add("ccmpc_ln", typeof(System.String));
            dtSource1.Columns.Add("other_loan", typeof(System.String));
            dtSource1.Columns.Add("remarks", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("date_posted", typeof(System.String));
            dtSource1.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource1.Columns.Add("created_by_user", typeof(System.String));
            dtSource1.Columns.Add("updated_by_user", typeof(System.String));
            dtSource1.Columns.Add("posted_by_user", typeof(System.String));
            dtSource1.Columns.Add("created_dttm", typeof(System.String));
            dtSource1.Columns.Add("updated_dttm", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("days_not_exposed", typeof(System.String));

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

        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL", "0");
            DataRow[] selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

            if (ddl_empl_id.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id", "0");
                ddl_empl_id.Focus();
                validatedSaved = false;
                return validatedSaved;
            }

            if (CommonCode.checkisdecimal(txtb_monthly_rate) == false)
            {
                    FieldValidationColorChanged(true, "txtb_monthly_rate", "0");
                    validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_daily_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_daily_rate", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_no_days_wordked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_wordked", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_no_days_subsistence) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_subsistence", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_with_laundry) == false)
            {
                FieldValidationColorChanged(true, "txtb_with_laundry", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_no_days_wordked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_wordked", "0");
                validatedSaved = false;
            }


            if (CommonCode.checkisdecimal(txtb_hazard_perc) == false)
            {
                FieldValidationColorChanged(true, "txtb_hazard_perc", "0");
                validatedSaved = false;
            }

            if (int.Parse(txtb_hazard_perc.Text) == 0)
            {
                FieldValidationColorChanged(true, "txtb_hazard_perc_zero", "0");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_withhel_tax_perc) == false)
            {
                FieldValidationColorChanged(true, "txtb_withhel_tax_perc", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_subs_daily_dspl) == false)
            {
                FieldValidationColorChanged(true, "txtb_subs_daily_dspl", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_luandry_daily_dspl) == false)
            {
                FieldValidationColorChanged(true, "txtb_luandry_daily_dspl", "0");
                validatedSaved = false;
            }


            if (CommonCode.checkisdecimal(txtb_net_subs_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_subs_amount", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_net_hazard_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_hazard_amount", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_net_laundry_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_laundry_amount", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_gross_pay", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_with_tax_held_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_with_tax_held_amount", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_nico_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_nico_loan", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_ccmpc_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_ccmpc_loan", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_network_bank_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_network_bank_loan", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_other_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_other_loan", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_net_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_pay", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_days_not_exposed) == false)
            {
                FieldValidationColorChanged(true, "txtb_days_not_exposed", "0");
                validatedSaved = false;
            }
            /**END OF GROSS PAY AND NET PAY VALIDATION**/

            //Add validation
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr", "0");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }

            
            // MANDATORY VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_mand1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand1","0");  txtb_other_ded_mand1.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand2","0");  txtb_other_ded_mand2.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand3","0");  txtb_other_ded_mand3.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand4","0");  txtb_other_ded_mand4.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand5","0");  txtb_other_ded_mand5.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand6","0");  txtb_other_ded_mand6.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand7","0");  txtb_other_ded_mand7.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand8","0");  txtb_other_ded_mand8.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand9","0");  txtb_other_ded_mand9.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_mand10", "0"); txtb_other_ded_mand10.Focus() ; validatedSaved  = false; }

            // OPTIONAL VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_prem1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem1","0");  txtb_other_ded_prem1.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem2","0");  txtb_other_ded_prem2.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem3","0");  txtb_other_ded_prem3.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem4","0");  txtb_other_ded_prem4.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem5","0");  txtb_other_ded_prem5.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem6","0");  txtb_other_ded_prem6.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem7","0");  txtb_other_ded_prem7.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem8","0");  txtb_other_ded_prem8.Focus()  ; validatedSaved  = false;  }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem9", "0");  txtb_other_ded_prem9.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_prem10", "0"); txtb_other_ded_prem10.Focus() ; validatedSaved  = false; }
             
            // LOAN VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_loan1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan1","0");  txtb_other_ded_loan1.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan2","0");  txtb_other_ded_loan2.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan3","0");  txtb_other_ded_loan3.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan4","0");  txtb_other_ded_loan4.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan5","0");  txtb_other_ded_loan5.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan6","0");  txtb_other_ded_loan6.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan7","0");  txtb_other_ded_loan7.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan8","0");  txtb_other_ded_loan8.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan9", "0");  txtb_other_ded_loan9.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_loan10", "0"); txtb_other_ded_loan10.Focus() ; validatedSaved  = false; }

            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName, string amount)
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
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_monthly_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-monthly":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_monthly_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_daily_rate":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_daily_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-daily":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_daily_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_working_days":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_working_days.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_days_subsistence":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_days_subsistence.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_with_laundry":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_with_laundry.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_days_wordked":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_days_wordked.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hazard_perc":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hazard_perc.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hazard_perc_zero":
                        {
                            LblRequired8.Text = "Hazard rate is 0%";
                            txtb_hazard_perc.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_withhel_tax_perc":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_withhel_tax_perc.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_remarks":
                        {
                            LblRequired10.Text = MyCmn.CONST_RQDFLD;
                            txtb_remarks.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_subs_daily_dspl":
                        {
                            LblRequired11.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_subs_daily_dspl.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_luandry_daily_dspl":
                        {
                            LblRequired12.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_luandry_daily_dspl.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_subs_amount":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_subs_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_hazard_amount":
                        {
                            LblRequired14.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_hazard_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_laundry_amount":
                        {
                            LblRequired15.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_laundry_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gross_pay":
                        {
                            LblRequired16.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_with_tax_held_amount":
                        {
                            LblRequired17.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_with_tax_held_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_nico_loan":
                        {
                            LblRequired18.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nico_loan.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_ccmpc_loan":
                        {
                            LblRequired19.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_ccmpc_loan.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_network_bank_loan":
                        {
                            LblRequired20.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_network_bank_loan.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_other_loan":
                        {
                            LblRequired21.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_loan.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_pay":
                        {
                            LblRequired22.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_loan.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_voucher_nbr":
                        {
                            LblRequired101.Text = MyCmn.CONST_RQDFLD;
                            txtb_voucher_nbr.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_reason":
                        {
                            LblRequired201.Text = MyCmn.CONST_RQDFLD;
                            txtb_reason.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_days_not_exposed":
                        {
                            LblRequired100.Text = MyCmn.CONST_RQDFLD;
                            txtb_days_not_exposed.BorderColor = Color.Red;
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
                            LblRequired1.Text   = "";
                            LblRequired2.Text   = "";
                            LblRequired3.Text   = "";
                            LblRequired4.Text   = "";
                            LblRequired6.Text   = "";
                            LblRequired5.Text   = "";
                            LblRequired7.Text   = "";
                            LblRequired8.Text   = "";
                            LblRequired9.Text   = "";
                            LblRequired11.Text  = "";
                            LblRequired12.Text  = "";
                            LblRequired13.Text  = "";
                            LblRequired14.Text  = "";
                            LblRequired15.Text  = "";
                            LblRequired16.Text  = "";
                            LblRequired17.Text  = "";
                            LblRequired18.Text  = "";
                            LblRequired19.Text  = "";
                            LblRequired20.Text  = "";
                            LblRequired21.Text  = "";
                            LblRequired22.Text  = "";
                            LblRequired100.Text  = "";
                            LblRequired101.Text = "";
                            LblRequired201.Text = "";

                            txtb_voucher_nbr.BorderColor            = Color.LightGray;
                            txtb_reason.BorderColor                 = Color.LightGray;
                            ddl_empl_id.BorderColor                 = Color.LightGray;
                            txtb_ccmpc_loan.BorderColor             = Color.LightGray;
                            txtb_daily_rate.BorderColor             = Color.LightGray;
                            txtb_gross_pay.BorderColor              = Color.LightGray;
                            txtb_hazard_perc.BorderColor            = Color.LightGray;
                            txtb_luandry_daily_dspl.BorderColor     = Color.LightGray;
                            txtb_monthly_rate.BorderColor           = Color.LightGray;
                            txtb_network_bank_loan.BorderColor      = Color.LightGray;
                            txtb_net_hazard_amount.BorderColor      = Color.LightGray;
                            txtb_net_laundry_amount.BorderColor     = Color.LightGray;
                            txtb_net_pay.BorderColor                = Color.LightGray;
                            txtb_net_subs_amount.BorderColor        = Color.LightGray;
                            txtb_nico_loan.BorderColor              = Color.LightGray;
                            txtb_no_days_subsistence.BorderColor    = Color.LightGray;
                            txtb_no_days_wordked.BorderColor        = Color.LightGray;
                            txtb_other_loan.BorderColor             = Color.LightGray;
                            txtb_remarks.BorderColor                = Color.LightGray;
                            txtb_subs_daily_dspl.BorderColor        = Color.LightGray;
                            txtb_withhel_tax_perc.BorderColor       = Color.LightGray;
                            txtb_with_laundry.BorderColor           = Color.LightGray;
                            txtb_with_tax_held_amount.BorderColor   = Color.LightGray;
                            txtb_working_days.BorderColor           = Color.LightGray;
                            txtb_days_not_exposed.BorderColor           = Color.LightGray;
                            
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
                txtb_empl_id.Text = ddl_empl_id.SelectedValue.ToString().Trim();
                set_empl_hazard_subs_details();
                calculate_all();
                LabelforWorkedDays();
            }
            else
            {
                ClearEntry();

            }

        }

        /*
         * Method Purpose:  Compute Net Hazard
         * Date Created:    06/17/2019
         * Created By:      Joseph M. Tombo Jr.
         */
        protected double compute_net_hazard()
        {
            //set_empl_hazard_subs_details();
            double net_hazard_amount = 0;
            DataRow[] employee_id_ddl = dataList_employee.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");

            string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            string no_days_wordked      = txtb_no_days_wordked.Text.Trim() == "" ? "0" : txtb_no_days_wordked.Text.Trim();
            string actual_days_worked   = txtb_working_days.Text.Trim() == "" ? "0" : txtb_working_days.Text.Trim();
            if (double.Parse(no_days_wordked) >= 11 && txtb_department_code.Text.ToString().Trim() != "12")
            {
                txtb_net_hazard_amount.Text = "0";
                txtb_hazard_perc.Text       = "0";
                net_hazard_amount           = 0;
            }
            else
            {
                //UPDATED BY: JOSEPH M. TOMBO JR.
                //UPDATE PURPOSE: IS TO SET THE Hazard percent when the number of days change
                //add use it in hazard pay calculation
                if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
                {
                    txtb_hazard_perc.Text = double.Parse(employee_id_ddl[0]["hazard_pay_override"].ToString()).ToString();
                }
                else if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_EDIT)
                {
                    txtb_hazard_perc.Text = row2Edit[0]["hazard_rate"].ToString().Trim();
                }

                //This line will evaluate if the department is PSWD then New computation of Hazard will be implemented.
                if (txtb_department_code.Text.ToString().Trim() == "12")
                {
                    int pswd_no_of_days = 0;

                    if (int.Parse(ddl_year.SelectedValue) <= 2019)
                    {
                        pswd_no_of_days = 30; 
                        // VJA : 2020/01/28 - Old Policy : Hazard Pay = Monthly Sal. * 20% / 30 days (effective 2019 year below) * actual no. of dasys of expose
                    }
                    else
                    {
                        pswd_no_of_days = 22; 
                        // VJA : 2020/01/28 - New Policy : Hazard Pay = Monthly Sal. * 20% / 22 days (effective 2020 year up) * actual no. of dasys of expose
                    }

                    if (ddl_empl_type.SelectedValue.ToString().Trim() == "RE")
                    {
                        // OLD POLICY - Formula for Casual Employee ((monthly rate * hazard %) / 30 ) * # of actual days of exposure
                        // NEW POLICY - Formula for Casual Employee ((monthly rate * hazard %) / 22 ) * # of actual days of exposure
                        net_hazard_amount = ((double.Parse(txtb_monthly_rate.Text.ToString()) * (double.Parse(txtb_hazard_perc.Text.ToString().Trim()) / 100)) / pswd_no_of_days) * double.Parse(actual_days_worked.Trim());
                        // OLD - Convert hazard net amount into string and get the 2 digit decimal point and disregard 3rd digit
                        // string str_net_hazard_amount    = net_hazard_amount.ToString("###,##0.0000");
                        // txtb_net_hazard_amount.Text     = str_net_hazard_amount.Split('.')[0] + "." + str_net_hazard_amount.Split('.')[1].Substring(0, 2);
                        // net_hazard_amount               = double.Parse(txtb_net_hazard_amount.Text);

                        // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
                        txtb_net_hazard_amount.Text = net_hazard_amount.ToString("###,##0.00");

                    }
                    else if (ddl_empl_type.SelectedValue.ToString().Trim() == "CE" || ddl_empl_type.SelectedValue.ToString().Trim() == "JO")
                    {
                        // OLD POLICY - Formula for Casual Employee (((daily rate * 22) * hazard %) / 30 ) * # of actual days of exposure
                        // NEW POLICY - Formula for Casual Employee (((daily rate * 22) * hazard %) / 22 ) * # of actual days of exposure
                        net_hazard_amount = (((double.Parse(txtb_daily_rate.Text.ToString()) * 22) * (double.Parse(txtb_hazard_perc.Text.ToString().Trim()) / 100)) / pswd_no_of_days) * double.Parse(actual_days_worked.Trim());
                        //Convert hazard net amount into string and get the 2 digit decimal point and disregard 3rd digit
                        // string str_net_hazard_amount    = net_hazard_amount.ToString("###,##0.0000");
                        // txtb_net_hazard_amount.Text     = str_net_hazard_amount.Split('.')[0] + "." + str_net_hazard_amount.Split('.')[1].Substring(0, 2);
                        // net_hazard_amount               = double.Parse(txtb_net_hazard_amount.Text);

                        // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
                        txtb_net_hazard_amount.Text = net_hazard_amount.ToString("###,##0.00");
                    }
                }
                else
                {
                    if (ddl_empl_type.SelectedValue.ToString().Trim() == "RE")
                    {
                        net_hazard_amount               = double.Parse(txtb_monthly_rate.Text.ToString()) * (double.Parse(txtb_hazard_perc.Text.ToString().Trim()) / 100);
                        //Convert hazard net amount into string and get the 2 digit decimal point and disregard 3rd digit
                        // string str_net_hazard_amount    = net_hazard_amount.ToString("###,##0.0000");
                        // txtb_net_hazard_amount.Text     = str_net_hazard_amount.Split('.')[0] + "." + str_net_hazard_amount.Split('.')[1].Substring(0, 2);
                        // net_hazard_amount               = double.Parse(txtb_net_hazard_amount.Text);

                        // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
                        txtb_net_hazard_amount.Text = net_hazard_amount.ToString("###,##0.00");
                    }
                    else if (ddl_empl_type.SelectedValue.ToString().Trim() == "CE" || ddl_empl_type.SelectedValue.ToString().Trim() == "JO")
                    {
                        net_hazard_amount               = (double.Parse(txtb_daily_rate.Text.ToString().Trim()) * double.Parse(txtb_working_days.Text.ToString().Trim())) * (double.Parse(txtb_hazard_perc.Text.ToString().Trim()) / 100);
                        //Convert hazard net amount into string and get the 2 digit decimal point and disregard 3rd digit
                        // string str_net_hazard_amount    = net_hazard_amount.ToString("###,##0.0000");
                        // txtb_net_hazard_amount.Text     = str_net_hazard_amount.Split('.')[0] + "." + str_net_hazard_amount.Split('.')[1].Substring(0, 2);
                        // net_hazard_amount               = double.Parse(txtb_net_hazard_amount.Text);

                        // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
                        txtb_net_hazard_amount.Text = net_hazard_amount.ToString("###,##0.00");
                    }
                }
            }
            return net_hazard_amount;
        }

        /*
         * Method Purpose:  Compute Net Subsistence
         * Date Created:    06/18/2019
         * Created By:      Joseph M. Tombo Jr.
         */
        protected double compute_net_subsistence()
        {
            double net_subs_amount = 0;

            net_subs_amount = double.Parse(txtb_subs_daily_dspl.Text.ToString().Trim()) * double.Parse(txtb_no_days_subsistence.Text.ToString().Trim());

            if (net_subs_amount > double.Parse(hidden_subs_max_amount.Value.ToString().Trim()))
            {
                net_subs_amount = double.Parse(hidden_subs_max_amount.Value.ToString().Trim());
            }

            //Convert Subsistence net amount into string and get the 2 digit decimal point and disregard 3rd digit
            // string str_net_subs_amount = net_subs_amount.ToString("###,##0.0000");
            // txtb_net_subs_amount.Text = str_net_subs_amount.Split('.')[0].ToString().Trim() + "." + str_net_subs_amount.Split('.')[1].Substring(0, 2).ToString().Trim();
            // net_subs_amount = double.Parse(txtb_net_subs_amount.Text.ToString().Trim());

            // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
            txtb_net_subs_amount.Text = net_subs_amount.ToString("###,##0.00"); 
            return net_subs_amount;
        }

        /*
         * Method Purpose:  Compute Net Laundry
         * Date Created:    06/18/2019
         * Created By:      Joseph M. Tombo Jr.
         */
        protected double compute_net_laundry()
        {
            double net_luandry_amount           = 0;
            string no_days_luandry              = txtb_with_laundry.Text.ToString().Trim() == "" ? "0" : txtb_with_laundry.Text.ToString().Trim();

            if (txtb_department_code.Text.ToString().Trim() == "12")
            {
                no_days_luandry = "22";
            }
            txtb_with_laundry.Text = no_days_luandry;

            string editExpression               =  "days_nolaundry = " + no_days_luandry + "";
            DataRow[] selected_amount_laundry   = dt_noluandry_tbl_list.Select(editExpression);
            if (selected_amount_laundry.Length > 0 && selected_amount_laundry != null)
            {
                net_luandry_amount = double.Parse(selected_amount_laundry[0]["laundry_amt"].ToString());
            }

            //Convert Laundry net amount into string and get the 2 digit decimal point and disregard 3rd digit
            // string str_net_luandry_amount   = net_luandry_amount.ToString("###,##0.0000");
            // txtb_net_laundry_amount.Text    = str_net_luandry_amount.Split('.')[0].ToString().Trim() + "." + str_net_luandry_amount.Split('.')[1].Substring(0, 2).ToString().Trim();
            // net_luandry_amount              = double.Parse(txtb_net_laundry_amount.Text.ToString().Trim());

            // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
            txtb_net_laundry_amount.Text = net_luandry_amount.ToString("###,##0.00");

            // VJA - Ana si maam Mildred , Kung ang mga PSWDO, Zero(0) ang amount sa Laundry Amount
            //if (txtb_department_code.Text.ToString().Trim() == "12")
            //{
            //    net_luandry_amount = 0;
            //}

            return net_luandry_amount;
        }

        /*
        * Method Purpose:  Validate all textboxes value before computation
        * Date Created:    06/18/2019
        * Created By:      Joseph M. Tombo Jr.
        */
        protected bool datavalidation_before_computation()
        {
            bool error_count = true;

            FieldValidationColorChanged(false, "ALL", "0");


            if (ddl_empl_id.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id", "0");
                ddl_empl_id.Focus();
                error_count = false;
            }

            if (CommonCode.checkisdecimal(txtb_monthly_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_monthly_rate", "0");
                error_count = false;
            }

            if (CommonCode.checkisdecimal(txtb_daily_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_daily_rate", "0");
                error_count = false;
            }

            if (CommonCode.checkisdecimal(txtb_no_days_wordked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_wordked", "0");
                error_count = false;
            }
            if (CommonCode.checkisdecimal(txtb_no_days_subsistence) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_subsistence", "0");
                error_count = false;
            }

            if (CommonCode.checkisdecimal(txtb_with_laundry) == false)
            {
                FieldValidationColorChanged(true, "txtb_with_laundry", "0");
                error_count = false;
            }
            if (CommonCode.checkisdecimal(txtb_no_days_wordked) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_days_wordked", "0");
                error_count = false;
            }
            if (CommonCode.checkisdecimal(txtb_hazard_perc) == false)
            {
                FieldValidationColorChanged(true, "txtb_hazard_perc", "0");
                error_count = false;
            }
            if (CommonCode.checkisdecimal(txtb_withhel_tax_perc) == false)
            {
                FieldValidationColorChanged(true, "txtb_withhel_tax_perc", "0");
                error_count = false;
            }

            return error_count;
        }

        /*
        * Method Purpose:  Call all calculation method and validations
        * Date Created:    06/18/2019
        * Created By:      Joseph M. Tombo Jr.
        */
        private void calculate_all()
        {

            double total_gross_pay      = 0;
            double total_tax            = 0;
            double total_net_pay        = 0;
            double total_deduction      = 0;

            if (datavalidation_before_computation() == true)
            {
                compute_net_hazard();
                compute_net_laundry();
                compute_net_subsistence();

                total_gross_pay                 = double.Parse(txtb_net_hazard_amount.Text.ToString().Trim()) + double.Parse(txtb_net_laundry_amount.Text.ToString().Trim()) + double.Parse(txtb_net_subs_amount.Text.ToString().Trim());
                // string str_total_gross_pay      = "";
                // str_total_gross_pay             = total_gross_pay.ToString("###,##0.0000");
                // txtb_gross_pay.Text             = str_total_gross_pay.Split('.')[0].ToString().Trim() + "." + str_total_gross_pay.Split('.')[1].Substring(0, 2).ToString().Trim();

                // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
                txtb_gross_pay.Text = total_gross_pay.ToString("###,##0.00");

                total_tax = double.Parse(txtb_net_hazard_amount.Text.ToString().Trim()) + double.Parse(txtb_net_subs_amount.Text.ToString().Trim());
                total_tax                       = total_tax * (double.Parse(txtb_withhel_tax_perc.Text.ToString().Trim()) / 100);



                string str_total_tax            = "";
                str_total_tax                   = total_tax.ToString("###,##0.00");
                txtb_with_tax_held_amount.Text  = str_total_tax;

                total_deduction                 = double.Parse(txtb_with_tax_held_amount.Text.ToString()) + double.Parse(txtb_nico_loan.Text.ToString()) + double.Parse(txtb_network_bank_loan.Text.ToString()) + double.Parse(txtb_ccmpc_loan.Text.ToString()) + double.Parse(txtb_other_loan.Text.ToString());
                
                // Add Field Again - 2022-05-30
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan1.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan2.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan3.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan4.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan5.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan6.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan7.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan8.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan9.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan10.Text.ToString());

                total_net_pay                   = total_gross_pay - total_deduction;

                // string str_total_net_pay        = "";
                // str_total_net_pay               = total_net_pay.ToString("###,##0.00000");
                // txtb_net_pay.Text               = str_total_net_pay.Split('.')[0].ToString().Trim() + "." + str_total_net_pay.Split('.')[1].Substring(0, 2).ToString().Trim();

                // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
                txtb_net_pay.Text = total_net_pay.ToString("###,##0.00");
            }
        }

        /*
         * Method Purpose:  Use in add mode, when empl_id selected index change the data will be fillup
         * Date Created:    06/17/2019
         * Date Modify:     06/18/2019
         * Created By:      Joseph M. Tombo Jr.
         */
        protected void set_empl_hazard_subs_details()
        {
            DataRow[] selected_employee = dataList_employee.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");

            txtb_monthly_rate.Text          = double.Parse(selected_employee[0]["monthly_rate"].ToString()).ToString("###,##0.00");
            txtb_daily_rate.Text            = double.Parse(selected_employee[0]["daily_rate"].ToString()).ToString("###,##0.00");
            txtb_subs_daily_dspl.Text       = double.Parse(selected_employee[0]["subsistence_daily_rate"].ToString()).ToString("###,##0.00");
            txtb_luandry_daily_dspl.Text    = double.Parse(selected_employee[0]["laundry_daily_rate"].ToString()).ToString("###,##0.00");
            txtb_hazard_perc.Text           = double.Parse(selected_employee[0]["hazard_pay_override"].ToString()).ToString();
            txtb_withhel_tax_perc.Text      = "0";
            txtb_working_days.Text          = selected_employee[0]["days_workings"].ToString();
            txtb_department_code.Text       = selected_employee[0]["department_code"].ToString();
            txtb_department_descr.Text      = selected_employee[0]["department_name1"].ToString();
            txtb_position.Text              = selected_employee[0]["position_title1"].ToString();
            //Modifyication added this row
            hidden_laundry_max_amount.Value = selected_employee[0]["laundry_max_amount"].ToString();
            hidden_subs_max_amount.Value    = selected_employee[0]["subsistence_max_amount"].ToString();
            txtb_withhel_tax_perc.Text      = selected_employee[0]["tax_rate"].ToString();

            if (ddl_empl_type.SelectedValue.ToString().Trim() == "RE")
            {
                txtb_working_days.Enabled = false;
            }
            else if (ddl_empl_type.SelectedValue.ToString().Trim() == "CE" || ddl_empl_type.SelectedValue.ToString().Trim() == "JO")
            {
                txtb_working_days.Enabled = true;
            }
            
        }

        protected void btn_calculate_Click(object sender, EventArgs e)
        {
            calculate_all();
        }

        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            txtb_monthly_rate.Enabled         = ifenable;
            txtb_daily_rate.Enabled           = false;
            txtb_working_days.Enabled         = ifenable;
            txtb_no_days_subsistence.Enabled  = ifenable;
            txtb_with_laundry.Enabled         = ifenable;
            txtb_no_days_wordked.Enabled      = ifenable;
            txtb_hazard_perc.Enabled          = false;
            txtb_withhel_tax_perc.Enabled     = ifenable;
            txtb_remarks.Enabled              = ifenable;
            txtb_luandry_daily_dspl.Enabled   = false;
            txtb_subs_daily_dspl.Enabled      = ifenable;
            txtb_net_hazard_amount.Enabled    = ifenable;
            txtb_net_subs_amount.Enabled      = ifenable;
            txtb_net_laundry_amount.Enabled   = ifenable;
            txtb_gross_pay.Enabled            = ifenable; 
            txtb_with_tax_held_amount.Enabled = ifenable;
            txtb_nico_loan.Enabled            = ifenable;
            txtb_ccmpc_loan.Enabled           = ifenable;
            txtb_network_bank_loan.Enabled    = ifenable;
            txtb_other_loan.Enabled           = ifenable;
            txtb_net_pay.Enabled              = ifenable;
        }
        //***************************************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Calcuate Number of Days
        //***************************************************************************************************
        protected void txtb_no_days_wordked_TextChanged(object sender, EventArgs e)
        {
            calculate_all();
        }
        //***************************************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Calcuate Laundry Allowance
        //***************************************************************************************************
        protected void txtb_with_laundry_TextChanged(object sender, EventArgs e)
        {
            calculate_all();
        }
        //***************************************************************************************************
        //  BEGIN - VJA- 2020-08-15 - Dont Calculate Hazard, Subsistence and Laundry Allowance during Save
        //***************************************************************************************************
        private void calculate_gross_only()
        {
            double total_gross_pay = 0;
            double total_tax = 0;
            double total_net_pay = 0;
            double total_deduction = 0;

            if (datavalidation_before_computation() == true)
            {
                total_gross_pay = double.Parse(txtb_net_hazard_amount.Text.ToString().Trim()) + double.Parse(txtb_net_laundry_amount.Text.ToString().Trim()) + double.Parse(txtb_net_subs_amount.Text.ToString().Trim());
                // string str_total_gross_pay = "";
                // str_total_gross_pay = total_gross_pay.ToString("###,##0.0000");
                // txtb_gross_pay.Text = str_total_gross_pay.Split('.')[0].ToString().Trim() + "." + str_total_gross_pay.Split('.')[1].Substring(0, 2).ToString().Trim();

                // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
                txtb_gross_pay.Text = total_gross_pay.ToString("###,##0.00");

                total_tax = double.Parse(txtb_net_hazard_amount.Text.ToString().Trim()) + double.Parse(txtb_net_subs_amount.Text.ToString().Trim());
                total_tax = total_tax * (double.Parse(txtb_withhel_tax_perc.Text.ToString().Trim()) / 100);
                
                string str_total_tax = "";
                str_total_tax = total_tax.ToString("###,##0.00");
                txtb_with_tax_held_amount.Text = str_total_tax;

                total_deduction = double.Parse(txtb_with_tax_held_amount.Text.ToString()) + double.Parse(txtb_nico_loan.Text.ToString()) + double.Parse(txtb_network_bank_loan.Text.ToString()) + double.Parse(txtb_ccmpc_loan.Text.ToString()) + double.Parse(txtb_other_loan.Text.ToString());
                
                // Add Field Again - 2022-05-30
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan1.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan2.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan3.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan4.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan5.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan6.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan7.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan8.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan9.Text.ToString());
                total_deduction = total_deduction + double.Parse(txtb_other_ded_loan10.Text.ToString());
                
                total_net_pay = total_gross_pay - total_deduction;

                // string str_total_net_pay = "";
                // str_total_net_pay = total_net_pay.ToString("###,##0.00000");
                // txtb_net_pay.Text = str_total_net_pay.Split('.')[0].ToString().Trim() + "." + str_total_net_pay.Split('.')[1].Substring(0, 2).ToString().Trim();

                // VJA - 2020-10-08 - New Computation : Round off to the Nearest 3 digit  - Ana Si Maam Vivian and Maam Meldrid
                txtb_net_pay.Text = total_net_pay.ToString("###,##0.00");

            }
        }
        //***************************************************************************************************
        //  BEGIN - VJA- 2020-08-15 - Change the Label if PSWDO - Department Name
        //***************************************************************************************************
        private void LabelforWorkedDays()
        {
            if (txtb_department_code.Text.ToString().Trim() == "12") // PSWDO
            {
                lbl_worked_days.Text = "No. Days<small> <b style='font-size:9px;color:gray;'>(Qlfd. to Rcvd. Hzd.)</b></small>:";
            }
            else
            {
                lbl_worked_days.Text = "No. Days<small> <b style='font-size:9px;color:gray;'>(PT|LV|OB|HD|UT|Hol)</b></small>:";
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
                    lbl_other_ded_mand1.Text        = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand1_descr"].ToString() + ":";
                    lbl_other_ded_mand2.Text        = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand2_descr"].ToString() + ":";
                    lbl_other_ded_mand3.Text        = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand3_descr"].ToString() + ":";
                    lbl_other_ded_mand4.Text        = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand4_descr"].ToString() + ":";
                    lbl_other_ded_mand5.Text        = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand5_descr"].ToString() + ":";
                    lbl_other_ded_mand6.Text        = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand6_descr"].ToString() + ":";
                    lbl_other_ded_mand7.Text        = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand7_descr"].ToString() + ":";
                    lbl_other_ded_mand8.Text        = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand8_descr"].ToString() + ":";
                    lbl_other_ded_mand9.Text        = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand9_descr"].ToString() + ":";
                    lbl_other_ded_mand10.Text       = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? ""  : dt.Rows[0]["other_ded_mand10_descr"].ToString() + ":";
                    
                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Text        = dt.Rows[0]["other_ded_prem1_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem1_descr"].ToString() + ":";
                    lbl_other_ded_prem2.Text        = dt.Rows[0]["other_ded_prem2_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem2_descr"].ToString() + ":";
                    lbl_other_ded_prem3.Text        = dt.Rows[0]["other_ded_prem3_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem3_descr"].ToString() + ":";
                    lbl_other_ded_prem4.Text        = dt.Rows[0]["other_ded_prem4_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem4_descr"].ToString() + ":";
                    lbl_other_ded_prem5.Text        = dt.Rows[0]["other_ded_prem5_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem5_descr"].ToString() + ":";
                    lbl_other_ded_prem6.Text        = dt.Rows[0]["other_ded_prem6_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem6_descr"].ToString() + ":";
                    lbl_other_ded_prem7.Text        = dt.Rows[0]["other_ded_prem7_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem7_descr"].ToString() + ":";
                    lbl_other_ded_prem8.Text        = dt.Rows[0]["other_ded_prem8_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem8_descr"].ToString() + ":";
                    lbl_other_ded_prem9.Text        = dt.Rows[0]["other_ded_prem9_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem9_descr"].ToString() + ":";
                    lbl_other_ded_prem10.Text       = dt.Rows[0]["other_ded_prem10_descr"].ToString()  == "" ? "" : dt.Rows[0]["other_ded_prem10_descr"].ToString() + ":";
                    
                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Text        = dt.Rows[0]["other_ded_loan1_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan1_descr"].ToString() + ":";
                    lbl_other_ded_loan2.Text        = dt.Rows[0]["other_ded_loan2_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan2_descr"].ToString() + ":";
                    lbl_other_ded_loan3.Text        = dt.Rows[0]["other_ded_loan3_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan3_descr"].ToString() + ":";
                    lbl_other_ded_loan4.Text        = dt.Rows[0]["other_ded_loan4_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan4_descr"].ToString() + ":";
                    lbl_other_ded_loan5.Text        = dt.Rows[0]["other_ded_loan5_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan5_descr"].ToString() + ":";
                    lbl_other_ded_loan6.Text        = dt.Rows[0]["other_ded_loan6_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan6_descr"].ToString() + ":";
                    lbl_other_ded_loan7.Text        = dt.Rows[0]["other_ded_loan7_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan7_descr"].ToString() + ":";
                    lbl_other_ded_loan8.Text        = dt.Rows[0]["other_ded_loan8_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan8_descr"].ToString() + ":";
                    lbl_other_ded_loan9.Text        = dt.Rows[0]["other_ded_loan9_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan9_descr"].ToString() + ":";
                    lbl_other_ded_loan10.Text       = dt.Rows[0]["other_ded_loan10_descr"].ToString()  == "" ? "" : dt.Rows[0]["other_ded_loan10_descr"].ToString() + ":";
                    
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
                
            else
            {
                    // MANDATORY DEDUCTION
                    lbl_other_ded_mand1.Visible        = false;
                    lbl_other_ded_mand2.Visible        = false;
                    lbl_other_ded_mand3.Visible        = false;
                    lbl_other_ded_mand4.Visible        = false;
                    lbl_other_ded_mand5.Visible        = false;
                    lbl_other_ded_mand6.Visible        = false;
                    lbl_other_ded_mand7.Visible        = false;
                    lbl_other_ded_mand8.Visible        = false;
                    lbl_other_ded_mand9.Visible        = false;
                    lbl_other_ded_mand10.Visible       = false;

                    txtb_other_ded_mand1.Visible       = false;
                    txtb_other_ded_mand2.Visible       = false;
                    txtb_other_ded_mand3.Visible       = false;
                    txtb_other_ded_mand4.Visible       = false;
                    txtb_other_ded_mand5.Visible       = false;
                    txtb_other_ded_mand6.Visible       = false;
                    txtb_other_ded_mand7.Visible       = false;
                    txtb_other_ded_mand8.Visible       = false;
                    txtb_other_ded_mand9.Visible       = false;
                    txtb_other_ded_mand10.Visible      = false;

                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Visible        = false;
                    lbl_other_ded_prem2.Visible        = false;
                    lbl_other_ded_prem3.Visible        = false;
                    lbl_other_ded_prem4.Visible        = false;
                    lbl_other_ded_prem5.Visible        = false;
                    lbl_other_ded_prem6.Visible        = false;
                    lbl_other_ded_prem7.Visible        = false;
                    lbl_other_ded_prem8.Visible        = false;
                    lbl_other_ded_prem9.Visible        = false;
                    lbl_other_ded_prem10.Visible       = false;

                    txtb_other_ded_prem1.Visible     = false;
                    txtb_other_ded_prem2.Visible     = false;
                    txtb_other_ded_prem3.Visible     = false;
                    txtb_other_ded_prem4.Visible     = false;
                    txtb_other_ded_prem5.Visible     = false;
                    txtb_other_ded_prem6.Visible     = false;
                    txtb_other_ded_prem7.Visible     = false;
                    txtb_other_ded_prem8.Visible     = false;
                    txtb_other_ded_prem9.Visible     = false;
                    txtb_other_ded_prem10.Visible    = false;

                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Visible      = false;
                    lbl_other_ded_loan2.Visible      = false;
                    lbl_other_ded_loan3.Visible      = false;
                    lbl_other_ded_loan4.Visible      = false;
                    lbl_other_ded_loan5.Visible      = false;
                    lbl_other_ded_loan6.Visible      = false;
                    lbl_other_ded_loan7.Visible      = false;
                    lbl_other_ded_loan8.Visible      = false;
                    lbl_other_ded_loan9.Visible      = false;
                    lbl_other_ded_loan10.Visible     = false;

                    txtb_other_ded_loan1.Visible     = false;
                    txtb_other_ded_loan2.Visible     = false;
                    txtb_other_ded_loan3.Visible     = false;
                    txtb_other_ded_loan4.Visible     = false;
                    txtb_other_ded_loan5.Visible     = false;
                    txtb_other_ded_loan6.Visible     = false;
                    txtb_other_ded_loan7.Visible     = false;
                    txtb_other_ded_loan8.Visible     = false;
                    txtb_other_ded_loan9.Visible     = false;
                    txtb_other_ded_loan10.Visible    = false;
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
                                            + txtb_other_ded_mand1.Text.ToString().Trim().Replace(",","")
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
                    double total_all = 0;
                    total_all =   double.Parse(txtb_other_ded_mand1.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand2.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand3.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand4.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand5.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand6.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand7.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand8.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand9.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand10.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem1.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem2.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem3.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem4.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem5.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem6.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem7.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem8.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem9.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem10.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan1.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan2.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan3.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan4.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan5.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan6.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan7.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan8.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan9.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan10.Text.ToString().Trim().Replace(",", ""));

                    if (total_all > 0)
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
                }
                else
                {
                }
            }
        }
        /* ---------------------- END OF THE CODE------------------------------*/
    }
}