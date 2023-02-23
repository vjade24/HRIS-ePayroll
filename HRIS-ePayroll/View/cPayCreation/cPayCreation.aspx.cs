//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Payroll Auto Creation
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Joseph M. Tombo Jr     03/20/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View
{
    public partial class cPayCreation : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - AEC- 09/20/2018 - Data Place holder creation 
        //********************************************************************

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
        //  BEGIN - AEC- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - AEC- 09/20/2018 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            // ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            // scriptManager.RegisterPostBackControl(this.btn_create_generate);


            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "payroll_registry_nbr";
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

                ddl_month.SelectedValue = DateTime.Now.Month.ToString().Trim().PadLeft(2,'0');
                ddl_empl_type.SelectedValue = "RE";
                RetriveTemplate();

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

                    Session["page_allow_print_from_registry"] = Master.allow_print;
                    Session["page_allow_edit_history_from_registry"] = Master.allow_edit_history;
                    Session["page_allow_edit_from_registry"] = Master.allow_edit;
                    Session["page_allow_add_from_registry"] = Master.allow_add;
                    Session["page_allow_delete_from_registry"] = Master.allow_delete;

                }
            }


        }

        //********************************************************************
        //  BEGIN - AEC- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();

            RetrieveYear();
            RetriveGroupings();
            RetriveTemplate();
            RetriveEmploymentType();
            ClearEntry();
            regdiv.Visible = false;
            diffDiv1.Visible = false;
            diffDiv2.Visible = false;

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
            ddl_year.SelectedValue = years.ToString().Trim();
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Payroll Year
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
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "023") // RATA Payroll  
            {
                special_group = "03";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "025" || // Monetization - RE
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "044")   // Monetization - CE
            {
                special_group = "04";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "021" || // Hazard, Subsistence and Laundry Pay - RE
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "041")   // Hazard, Subsistence and Laundry Pay - CE
            {
                special_group = "05";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "022" || // Overtime Payroll - RE
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "042" || // Overtime Payroll - CE
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "061")   // Overtime Payroll - JO
            {
                special_group = "06";
            }
            //else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "029" || // Loyalty Bonus - RE
            //         ddl_payroll_template.SelectedValue.ToString().Trim() == "048")   // Loyalty Bonus - CE
            //{
            //    special_group = "07";
            //}
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "" ||
                    ddl_payroll_template.SelectedValue.ToString().Trim().Substring(0, 1) == "9") // Other Payroll Template Starts With 9 (Other Payroll)
            {
                special_group = "99";
            }
            else
            {
                special_group = "01";
            }

            DataTable dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_hdr_tbl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_special_group", special_group);

            ddl_payroll_group.DataSource = dt;
            ddl_payroll_group.DataValueField = "payroll_group_nbr";
            ddl_payroll_group.DataTextField = "grouping_descr";
            ddl_payroll_group.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_group.Items.Insert(0, li);
            chckbx_delete_created_payroll_group.Enabled = false;
            chckbx_delete_created_payroll_group.Checked = false;
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveTemplate()
        {
            ddl_payroll_template.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list0", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_type","01");

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }

        private void RetriveEmploymentType()
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
        //  BEGIN - AEC- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            ddl_year.SelectedValue                      = DateTime.Now.Year.ToString().Trim();
            ddl_month.SelectedIndex                     = -1;
            ddl_empl_type.SelectedIndex                 = -1;
            ddl_payroll_group.SelectedIndex             = -1;
            ddl_payroll_template.SelectedIndex          = -1;

            chckbx_delete_created_payroll_group.Checked = false;
        }
       
        //**************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false,"ALL");
            if (ddl_year.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_year");
                ddl_year.Focus();
                validatedSaved = false;
            }

            if (ddl_month.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_month");
                ddl_month.Focus();
                validatedSaved = false;
            }

            if (ddl_empl_type.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_empl_type");
                ddl_empl_type.Focus();
                validatedSaved = false;
            }

            if (ddl_payroll_template.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_payroll_template");
                ddl_payroll_template.Focus();
                validatedSaved = false;
            }

            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
            { 
                switch (pObjectName)
                {
                    case "ddl_year":
                        {
                            ddl_year.BorderColor                = Color.Red;
                            LblRequired1.Text                   = MyCmn.CONST_RQDFLD;
                            break;
                        }
                    case "ddl_month":
                        {
                            ddl_month.BorderColor               = Color.Red;
                            LblRequired2.Text                   = MyCmn.CONST_RQDFLD;
                            break;
                        }
                    case "ddl_empl_type":
                        {
                            ddl_empl_type.BorderColor           = Color.Red;
                            LblRequired3.Text                   = MyCmn.CONST_RQDFLD;
                            break;
                        }
                    case "ddl_payroll_template":
                        {
                            ddl_payroll_template.BorderColor    = Color.Red;
                            LblRequired4.Text                   = MyCmn.CONST_RQDFLD;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired1.Text                   = "";
                            LblRequired2.Text                   = "";
                            LblRequired3.Text                   = "";
                            LblRequired4.Text                   = "";

                            ddl_empl_type.BorderColor           = Color.LightGray;
                            ddl_month.BorderColor               = Color.LightGray;
                            ddl_payroll_group.BorderColor       = Color.LightGray;
                            ddl_payroll_template.BorderColor    = Color.LightGray;
                            ddl_year.BorderColor                = Color.LightGray;
                            break;
                        }
                }
            }
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetriveTemplate();
            RetriveGroupings();
            RetriveRegistryNbr();
            UpdatePanel10.Update();
        }

        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }
        protected void ddl_payroll_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetriveGroupings();
            RetriveRegistryNbr();

            if (ddl_payroll_template.SelectedValue.ToString().Trim() == "033" ||
                ddl_payroll_template.SelectedValue.ToString().Trim() == "052")
            {
                diffDiv1.Visible = true;
                diffDiv2.Visible = true;
            }
            else
            {
                diffDiv1.Visible = false;
                diffDiv2.Visible = false;
            }
            UpdatePanel10.Update();
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetriveRegistryNbr();
            UpdatePanel10.Update();
        }

        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetriveRegistryNbr();
            UpdatePanel10.Update();
        }

        protected void ddl_payroll_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_payroll_group.SelectedValue.ToString().Trim() != "")
            {
                chckbx_delete_created_payroll_group.Enabled = true;
                RetriveRegistryNbr();
            }
            else {
                chckbx_delete_created_payroll_group.Checked = false;
                chckbx_delete_created_payroll_group.Enabled = false;

            }
            RetriveRegistryNbr();
        }

        //*************************************************************************
        //  BEGIN - AEC- 05/07/2019 - Populate Combo list for Registry Nbr
        //*************************************************************************
        private void RetriveRegistryNbr()
        {
            ddl_registrynbr.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_get_registry_nbrs", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim());

            ddl_registrynbr.DataSource = dt;
            ddl_registrynbr.DataValueField = "payroll_registry_nbr";
            ddl_registrynbr.DataTextField = "payroll_registry_nbr";
            ddl_registrynbr.DataBind();
            ddl_registrynbr.SelectedIndex = -1;
            if (ddl_registrynbr.Items.Count >= 1)
            {
                ddl_registrynbr.SelectedIndex = 0;
                regdiv.Visible = true;
            }
            else
            {
                regdiv.Visible = false;
            };
        }


        protected void btn_create_generate_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                // **********************************************************************************************
                // ****BEGIN******** O L D   A P P R O A C H   A S   O F   0 5 / 0 9 / 2 0 2 0 ******************
                // **********************************************************************************************

                //string checkbox_checked = "";
                ///*
                // **********************************************************************************************
                // *| Return Values:                                                                            *
                // *|********************************************************************************************
                // *| Y - Payroll Registry Successfully Generated                                               *
                // *| N - Payroll Registry Not Generated / SP Error                                             *
                // *| 0 - No Data found, PayReg not Generated                                                   *
                // *| X - PayReg not Generated -No Pay Group Selected to be Deleted                             *
                // *| D - PayReg not Generated -Delete Existing Payroll Group not Allowed for Multiple Groups   *
                // *| P - Already Posted - PayReg not Generated                                                 *
                // *| A - PayReg already existing for selected Payroll Template                                 *
                // *| R - No Selected Payroll Registry to be Regenerated                                        *
                // *| G - PayReg already existing for selected Payroll Group                                    *
                // **********************************************************************************************
                //*/
                ///* Template Codes And its Value For Casual And Regular Employment Type
                // * ----------------------------------------------------------------------------------------
                // *  RE TEMPLATE CODES  |   CE TEMPLATE CODES    | Description                               
                // *  -------------------+------------------------+------------------------------------------
                // *  023                |   043                  | Communication Expense Allowance
                // *  -------------------+------------------------+------------------------------------------
                // *  026                |   045                  | Mid Year Bunos                           
                // *  -------------------+------------------------+------------------------------------------
                // *  027                |   046                  | Year-End And Cash Gift Bunos
                // *  -------------------+------------------------+------------------------------------------
                // *  028                |   047                  | Clothing Allowance
                // *  -------------------+------------------------+------------------------------------------
                // *  029                |   048                  | Loyalty Bunos
                // *  -------------------+------------------------+------------------------------------------
                // *  030                |   049                  | Anniversary Bunos
                // *  -------------------+------------------------+------------------------------------------
                // *  031                |   050                  | Productivity Enhancement Incentive Bunos
                // *  -------------------+------------------------+------------------------------------------
                // *  032                |   051                  | CNA Incentives
                // *  -------------------+------------------------+------------------------------------------
                // *  062                |                        | Honoraria - JO - Bonus
                // *  ---------------------------------------------------------------------------------------
                // */
                //checkbox_checked = chckbx_delete_created_payroll_group.Checked ? "1" : "0";
                //switch (ddl_payroll_template.SelectedValue.ToString().Trim())
                //{
                //    case "007":
                //        dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_salary_re", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim());
                //        break;
                //    case "008":
                //        dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_salary_ce", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim());
                //        break;
                //    case "009":
                //    case "010":
                //    case "011":
                //        {
                //            dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_salary_jo", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim());
                //            break;
                //        }
                //    case "033":    // Salary Differential
                //        {
                //            dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_salary_diff", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim(), "par_no_of_months", ddl_no_months.SelectedValue.ToString().Trim(), "par_month_covered", txt_month_covered.Text.ToString().Trim());
                //            break;
                //        }
                //    case "024":
                //    case "026":
                //    case "027":
                //    case "030":
                //    case "043":
                //    case "045":
                //    case "046":
                //    case "048":
                //    case "049":
                //    case "050":
                //    case "032":
                //    case "051":
                //    case "062":
                //    case "047":
                //    case "028": 
                //    case "029": 
                //    case "031": 
                //        {
                //            dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_others", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim());
                //            break;
                //        }
                //    case "023":
                //        {
                //            dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_rata", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim());
                //            break;
                //        }
                //    case "021":
                //    case "041":
                //        {
                //            dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_subs", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim());
                //            break;
                //        }
                //}

                //if (dt_result != null && dt_result.Rows.Count > 0)
                //{
                //    switch (dt_result.Rows[0]["return_flag"].ToString())
                //    {
                //        case "Y":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                //                h2_status.InnerText = "SUCCESSFULLY";
                //                lbl_generation_msg.Text = "GENERATED";
                //                break;
                //            }
                //        case "N":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-times-circle text-danger");
                //                h2_status.InnerText = "SP ERROR";
                //                lbl_generation_msg.Text = "Payroll Registry Not Generated";
                //                break;
                //            }
                //        case "0":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-info-circle text-danger");
                //                h2_status.InnerText = "NO DATA FOUND";
                //                lbl_generation_msg.Text = "Payroll Registry Not Generated";
                //                break;
                //            }
                //        case "X":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                //                h2_status.InnerText = "NOT GENERATED";
                //                lbl_generation_msg.Text = "No Pay Group Selected to be Deleted ";
                //                break;
                //            }
                //        case "D":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                //                h2_status.InnerText = "NOT GENERATED";
                //                lbl_generation_msg.Text = "Delete Existing Payroll Group not Allowed for Multiple Groups";
                //                break;
                //            }
                //        case "P":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                //                h2_status.InnerText = "NOT GENERATED";
                //                lbl_generation_msg.Text = "Already Posted";
                //                break;
                //            }
                //        case "A":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                //                h2_status.InnerText = "NOT GENERATED";
                //                lbl_generation_msg.Text = "PayReg already existing for selected Payroll Template";
                //                break;
                //            }
                //        case "R":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                //                h2_status.InnerText = "NOT GENERATED";
                //                lbl_generation_msg.Text = "Invalid or No Payroll Registry Nbr. Selected to be Regenerated";
                //                break;
                //            }
                //        case "G":
                //            {
                //                i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                //                h2_status.InnerText = "NOT GENERATED";
                //                lbl_generation_msg.Text = "PayReg already existing for selected Payroll Group";
                //                break;
                //            }
                //    }
                //}
                //else
                //{
                //    i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                //    h2_status.InnerText = "NOT GENERATED";
                //    lbl_generation_msg.Text = "Stored Procedure Error !";
                //}

                // **********************************************************************************************
                // ****END******** O L D   A P P R O A C H   A S   O F   0 5 / 0 9 / 2 0 2 0 ********************
                // **********************************************************************************************



                // **********************************************************************************************
                // *****BEGIN******* N E W   A P P R O A C H   A S   O F   0 5 / 0 9 / 2 0 2 0  B Y  - V J A ****
                // **********************************************************************************************

                //                  Note : If the Template Code is 033 - Salary Differetial
                //                       1.) E Zero(0) ang par_no_of_months (No of Months)
                //                       2.) E Blank ang Parameter nga par_month_covered ( Month Covered)

                try
                {
                    i_icon_display.Attributes.Add("class", "fa fa-5x fa fa-spinner fa-spin text-success");
                    h2_status.InnerText     = "Please Wait...";
                    lbl_generation_msg.Text = "";
                    
                    string checkbox_checked = "";
                    checkbox_checked = chckbx_delete_created_payroll_group.Checked ? "1" : "0";

                    if (ddl_payroll_template.SelectedValue.ToString().Trim() == "033" || // Regular - Salary Differential
                        ddl_payroll_template.SelectedValue.ToString().Trim() == "052")   // Casual  - Salary Differential
                    {
                        dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_all", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim(), "par_no_of_months", ddl_no_months.SelectedValue.ToString().Trim(), "par_month_covered", txt_month_covered.Text.ToString().Trim());
                    }
                    else
                    {
                        dt_result = MyCmn.RetrieveData("sp_payrollregistry_generate_all", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_delete_existing_group", checkbox_checked, "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_payroll_registry_nbr", ddl_registrynbr.SelectedValue.ToString().Trim(), "par_no_of_months", "0", "par_month_covered", "");
                    }

                    if (dt_result != null && dt_result.Rows.Count > 0)
                    {
                        switch (dt_result.Rows[0]["result_flag"].ToString().Trim())
                        {
                            case "E":
                                i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                                h2_status.InnerText     = "NOT GENERATED";
                                lbl_generation_msg.Text = dt_result.Rows[0]["result_flag_message"].ToString();
                                break;

                            default:
                                i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                                h2_status.InnerText     = "SUCCESSFULLY GENERATED";
                                lbl_generation_msg.Text = dt_result.Rows[0]["result_flag_message"].ToString();
                                break;
                        }
                    }
                    else
                    {
                        i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                        h2_status.InnerText     = "NOT GENERATED";
                        lbl_generation_msg.Text = "Stored Procedure Error !";
                    }
                }
                catch (Exception x)
                {
                    i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                    h2_status.InnerText = "SOMETHING WENT WRONG";
                    lbl_generation_msg.Text = x.Message.ToString().Trim();
                }

            }
        }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}