//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Payroll Auto Creation
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// JORGE RUSTOM VILLANUEVA     06/03/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View
{
    public partial class cDirectToPrinter : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JRV- 06/03/2019 - Data Place holder creation 
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

        DataTable dataListGridField
        {
            get
            {
                if ((DataTable)ViewState["dataListGridField"] == null) return null;
                return (DataTable)ViewState["dataListGridField"];
            }
            set
            {
                ViewState["dataListGridField"] = value;
            }
        }

        DataTable dataListGridCheck
        {
            get
            {
                if ((DataTable)ViewState["dataListGridCheck"] == null) return null;
                return (DataTable)ViewState["dataListGridCheck"];
            }
            set
            {
                ViewState["dataListGridCheck"] = value;
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

        //********************************************************************
        //  BEGIN - JRV- 06/03/2019 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - JRV- 06/03/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    ViewState["page_allow_add"] = Session["page_allow_add_from_registry"];
                    ViewState["page_allow_delete"] = Session["page_allow_delete_from_registry"];
                    ViewState["page_allow_edit"] = Session["page_allow_edit_from_registry"];
                    ViewState["page_allow_edit_history"] = Session["page_allow_edit_history_from_registry"];
                    ViewState["page_allow_print"] = Session["page_allow_print_from_registry"];


                    if (Session["PreviousValuesonPage_cPayRegistry"] == null)
                        Session["PreviousValuesonPage_cPayRegistry"] = "";
                    else if (Session["PreviousValuesonPage_cPayRegistry"].ToString() != string.Empty)
                    {
                        string[] prevValues = Session["PreviousValuesonPage_cPayRegistry"].ToString().Split(new char[] { ',' });
                      
                        txtb_payroll_year.Text = prevValues[0].ToString();
                        ViewState["payroll_month"] = prevValues[1].ToString().Trim();
                        if (prevValues[1].ToString().Trim() == "01")
                        {
                            txtb_payroll_month.Text = "January";
                        }

                        else if (prevValues[1].ToString().Trim() == "02")
                        {
                            txtb_payroll_month.Text = "February";
                        }

                        else if (prevValues[1].ToString().Trim() == "03")
                        {
                            txtb_payroll_month.Text = "March";
                        }

                        else if (prevValues[1].ToString().Trim() == "04")
                        {
                            txtb_payroll_month.Text = "April";
                        }

                        else if (prevValues[1].ToString().Trim() == "05")
                        {
                            txtb_payroll_month.Text = "May";
                        }

                        else if (prevValues[1].ToString().Trim() == "06")
                        {
                            txtb_payroll_month.Text = "June";
                        }

                        else if (prevValues[1].ToString().Trim() == "07")
                        {
                            txtb_payroll_month.Text = "July";
                        }

                        else if (prevValues[1].ToString().Trim() == "08")
                        {
                            txtb_payroll_month.Text = "August";
                        }

                        else if (prevValues[1].ToString().Trim() == "09")
                        {
                            txtb_payroll_month.Text = "September";
                        }

                        else if (prevValues[1].ToString().Trim() == "10")
                        {
                            txtb_payroll_month.Text = "October";
                        }

                        else if (prevValues[1].ToString().Trim() == "11")
                        {
                            txtb_payroll_month.Text = "November";
                        }

                        else if (prevValues[1].ToString().Trim() == "12")
                        {
                            txtb_payroll_month.Text = "December";
                        }
                        


                        txtb_empl_type.Text = prevValues[2].ToString();
                        txtb_payroll_template.Text = prevValues[3].ToString();
                        txtb_payroll_group.Text = prevValues[6].ToString();
                        lbl_registry_number.Text = prevValues[7].ToString();

                        DataTable dt1 = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");
                        
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            DataRow[] dt1_row = dt1.Select("employment_type = '" + prevValues[2].ToString() + "'");
                            txtb_empl_type_display.Text = dt1_row[0]["employmenttype_description"].ToString();
                        }

                        DataTable dt2 = MyCmn.RetrieveData("sp_payrollregistry_template_combolist", "par_payrolltemplate_code", prevValues[3].ToString().Trim());
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            txtb_payroll_template_display.Text = dt2.Rows[0]["payrolltemplate_descr"].ToString();
                        }
                        
                        DataTable dt3 = MyCmn.RetrieveData("sp_payrollregistry_hdr_tbl_list", "par_payroll_year", prevValues[0].ToString().Trim(), "par_payroll_month", prevValues[1].ToString().Trim(), "par_payrolltemplate_code", prevValues[3].ToString().Trim());
                        if (dt3 != null && dt3.Rows.Count > 0)
                        {
                            
                            DataRow[] row2Edit = dt3.Select("payroll_registry_nbr = '"+ prevValues[7].ToString() + "'");
                            txtb_payroll_group_display.Text = row2Edit[0]["grouping_descr"].ToString();
                            txtb_payroll_descr.Text = row2Edit[0]["payroll_registry_descr"].ToString();
                        }
                        
                        RetrieveDataListGrid();
                        

                    }


                }

                
            }
            
        }
        

        private void CheckDeductions()
        {
            dataListGridCheck = MyCmn.RetrieveData("sp_check_total_deductions", "par_payroll_year", txtb_payroll_year.Text.ToString().Trim(), "par_payroll_registry_nbr", lbl_registry_number.Text.ToString().Trim(), "par_payrolltemplate_code", txtb_payroll_template.Text.ToString().Trim());
        }

        private void RetrieveDataListGrid()
        {

            dataListGridField = MyCmn.RetrieveData("sp_payrollregistry_template_combolist", "par_payrolltemplate_code", txtb_payroll_template.Text.ToString().Trim());
            dataListGridField.Columns.Add("checkbox_checked", typeof(System.String));
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGridField);
            
        }

        public void pass_value()
        {
            int x = 0;

            foreach (GridViewRow row in gv_dataListGrid.Rows)
            {
                CheckBox temp_chkbox_print = row.FindControl("chkbox_print") as CheckBox;
                Label lbltemplate_code = row.FindControl("lbltemplate_code") as Label;
                string code = lbltemplate_code.Text.ToString();
                

                if (temp_chkbox_print.Checked)
                {
                    dataListGridField.Rows[x]["checkbox_checked"] = true;
                }
                else
                {
                    dataListGridField.Rows[x]["checkbox_checked"] = false;
                }


                x = x + 1;
            }

        }



        //********************************************************************
        //  BEGIN - JRV- 06/03/2019- Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            regdiv.Visible = false;
        }




        //**************************************************************************
        //  BEGIN - JRV- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = false;
            FieldValidationColorChanged(false, "ALL");

            for (int x = 0; x < dataListGridField.Rows.Count; x++)
            {
                if (dataListGridField.Rows[x]["checkbox_checked"].ToString() == "True")
                {
                    validatedSaved = true;
                }
            }

            if (validatedSaved == false)
            {
                FieldValidationColorChanged(true, "ALL");
            }
            

            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - JRV- 09/20/2018 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
            {
                switch (pObjectName)
                {

                    case "ALL":
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
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
                            gv_dataListGrid.BorderColor = Color.LightGray;
                            break;
                        }
                }
            }
        }
        


        protected void btn_print_all_Click(object sender, EventArgs e)
        {

            pass_value();

            string printreport = string.Empty;
            string procedure = "sp_payroll_generate_reports_all";
            string url = string.Empty;
            Boolean print_included = true;
            //url = "/printViewAll/printViewAll.aspx?id=";
            url = "";
            if (IsDataValidated())
            {
                CheckDeductions();

                

                ///ScriptManager.RegisterStartupScript(this, this.GetType(), "trigger", "alert('asdasd');", true);

                for (int x = 0; x < dataListGridField.Rows.Count; x++)
                {
                    
                    if (dataListGridField.Rows[x]["checkbox_checked"].ToString() == "True")
                    {
                        print_included = true;

                        // ********************START*********************************************************
                        // ******* This is for the Other Custom Payroll Setup ********05/07/2020*************
                        // **********************************************************************************
                        if (dataListGridField.Rows[x]["payrolltemplate_code"].ToString().Trim().Substring(0, 1) == "9")
                        {
                            DataTable dt = MyCmn.RetrieveData("sp_othrpaysetup_tbl_fld_cnt", "p_payrolltemplate_code", dataListGridField.Rows[x]["payrolltemplate_code"].ToString().Trim());


                            if (dt.Rows.Count > 0)
                            {
                                DataTable dt2 = MyCmn.RetrieveData("sp_payrollregistry_template_combolist", "par_payrolltemplate_code", dataListGridField.Rows[x]["payrolltemplate_code"].ToString().Trim());
                                string report_filename = "";

                                if (dt2 != null && dt2.Rows.Count > 0)
                                {
                                    report_filename = dt2.Rows[0]["report_filename"].ToString();
                                }

                                if (report_filename == "")
                                {
                                    printreport = "/cryOtherPayroll/cryOthPay/cryOthPay" + dt.Rows[0]["no_of_fields"].ToString().Trim() + ".rpt";
                                    //procedure = "sp_payrollregistry_othpay_rep";
                                    url = url + "~/Reports/";
                                }
                                else
                                {
                                    printreport = report_filename;
                                    //procedure = "sp_payrollregistry_othpay_rep";
                                    url = url + "~/Reports/";
                                }
                            }



                        }

                        switch (dataListGridField.Rows[x]["payrolltemplate_code"].ToString().Trim())
                        {
                            case "105": // Obligation Request (OBR) - For Regular 
                            case "205": // Obligation Request (OBR) - For Casual
                            case "305": // Obligation Request (OBR) - For Job-Order
                                        //dataListGridField.Rows[x]["payrolltemplate_code"] = txtb_payroll_template.Text.ToString().Trim();
                                        //printreport = "cryOtherPayroll/cryOBR/cryOBR.rpt";
                               
                                    printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                    //procedure = "sp_payrollregistry_obr_rep";
                                    url = url + "~/Reports/";
                              

                                    /*<= 5 ? title : ;*/
                               
                                break;

                            //---- START OF REGULAR REPORTS

                            case "007": // Summary Monthly Salary  - For Regular 
                                //printreport = "cryRegularReports/crySalary/crySalarySummary.rpt";
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_re_ce_rep";
                                url = url + "~/Reports/";
                                break;

                            case "101": // Mandatory Deduction  - For Regular 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_re_ce_rep";
                                url = url + "~/Reports/";
                                break;

                            case "102": // Optional Deduction Page 1 - For Regular 

                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_optional_page1"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_re_ce_rep";
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }


                                break;

                            case "106": // Optional Deduction Page 2 - For Regular 

                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_optional_page2"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_re_ce_rep";
                                        url = url + "~/Reports/";
                                    }
                                    else
                                    {
                                        print_included = false;
                                    }
                                }

                                break;

                            case "103": // Loan Deduction Page 1 - For Regular 

                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page1"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_re_ce_rep";
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }


                                break;

                            case "107": // Loan Deduction Page 2 - For Regular 

                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page2"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_re_ce_rep";
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }


                                break;

                            case "104": // Attachment - For Monthly Salary
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_re_attach_rep";
                                url = url + "~/Reports/";
                                break;

                            case "033": // Salary Differential - For Regular 
                            case "052": // Salary Differential - For Casual 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_diff_rep";
                                url = url + "~/Reports/";
                                break;

                            //---- END OF REGULAR REPORTS

                            //---- START OF CASUAL REPORTS

                            case "008": // Summary Monthly Salary  - For Casual 
                                
                                // *************************************************************************************
                                // *** Special Case for Casual Monthly Payroll - Change Report File name if Year 2020
                                // ****************** 2021-01-13 *******************************************************
                                // *************************************************************************************
                                if (double.Parse(txtb_payroll_year.Text) <= 2020)
                                {
                                    printreport = "/cryCasualReports/crySalary/crySalarySummary_2020.rpt";
                                    url = url + "~/Reports/";
                                }
                                // *************************************************************************************
                                // *** Special Case for Casual Monthly Payroll - Change Report File name if Year 2020
                                // ****************** 2021-01-13 *******************************************************
                                // *************************************************************************************
                                else
                                {
                                    printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                    //procedure = "sp_payrollregistry_salary_ce_rep";
                                    url = url + "~/Reports/";
                                }
                                break;

                            //case "309": // Test Print All
                            //    printreport = "cryCasualReports/crySalary/crySalarySummary.rpt";
                            //    procedure = "sp_payrollregistry_salary_ce_rep";
                            //    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();
                            //    Response.Redirect(url);
                            //    break;

                            case "206": // Mandatory Deduction  -  For Casual 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_ce_rep";
                                url = url + "~/Reports/";
                                break;

                            case "207": // Optional Deduction Page 1 - For Casual 

                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_optional_page1"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_ce_rep";
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }


                                break;

                            case "208": // Optional Deduction Page 2 - For Casual 

                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_optional_page2"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_ce_rep";
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }

                                break;

                            case "209": // Loan Deduction Page 1 - For Casual 
                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page1"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_ce_rep";
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }


                                break;

                            case "210": // Loan Deduction Page 2 - For Casual 

                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page2"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_ce_rep";
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }

                                break;

                            case "211": // Attachment - For Monthly Salary - For Casual 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_ce_attach_rep";
                                url = url + "~/Reports/";
                                break;

                            case "044": // Monetization Payroll - For Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            //---- END OF CASUAL REPORTS

                            //---- START OF JOB-ORDER REPORTS

                            case "009": // Summary Salary Monthly - For Job-Order 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_jo_rep";
                                url = url + "~/Reports/";
                                break;

                            case "010": // Summary Salary 1st Quincemna - For Job-Order 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_jo_rep";
                                url = url + "~/Reports/";
                                break;

                            case "011": // Summary Salary 2nd Quincemna - For Job-Order 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_jo_rep";
                                url = url + "~/Reports/";
                                break;

                            case "306": // Contributions/Deductions Page 1 - For Job-Order 
                               
                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page1"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        //procedure = "sp_payrollregistry_salary_ce_rep";
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }
                                
                                break;

                            case "307": // Contributions/Deductions Page 1 - For Job-Order 
                                //ADDED BY JORGE: 2020-08-19
                                if (dataListGridCheck != null)
                                {

                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page2"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        url = url + "~/Reports/";
                                    }

                                    else
                                    {
                                        print_included = false;
                                    }
                                }

                                break;

                            case "308": // Attachment - For Monthly Salary
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_re_attach_rep";
                                url = url + "~/Reports/";
                                break;

                            case "061": // Overtime Payroll - For Job-Order 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_ovtm_rep";
                                url = url + "~/Reports/";
                                break;

                            case "062": // Honorarium Payroll - For Job-Order 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;


                            //---- END OF JOB-ORDER REPORTS
                            //---- START OF OTHER PAYROLL REPORTS

                            case "024": // Communication Expense Allowance - Regular
                            case "043": // Communication Expense Allowance - Casual
                            case "063": // Communication Expense Allowance - Job-Order
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "026": // Mid Year Bonus  - Regular        
                            case "045": // Mid Year Bonus  - Casual       
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "027": // Year-End And Cash Gift Bonus - Regular
                            case "046": // Year-End And Cash Gift Bonus - Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "028": // Clothing Allowance - Regular
                            case "047": // Clothing Allowance - Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "029": // Loyalty Bonus        - Regular
                            case "048": // Loyalty Bonus        - Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "030": // Anniversary Bonus    - Regular
                            case "049": // Anniversary Bonus    - Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "031": // Productivity Enhancement Incentive Bonus  - Regular
                            case "050": // Productivity Enhancement Incentive Bonus  - Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "023": // RATA 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_rata_rep";
                                url = url + "~/Reports/";
                                break;

                            case "108": // RATA - OBR Breakdown
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_obr_rata_rep";
                                url = url + "~/Reports/";
                                break;

                            case "021": // Subsistence, HA and LA      - Regular
                            case "041": // Subsistence, HA and LA      - Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_subs_rep";
                                url = url + "~/Reports/";
                                break;

                            case "022": // Overtime - Regular
                            case "042": // Overtime - Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_ovtm_rep";
                                url = url + "~/Reports/";
                                break;

                            case "032": // CNA INCENTIVE - Regular
                            case "051": // CNA INCENTIVE - Casual
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "025": // Monetization Payroll - For Regular
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "109": // Communication Expense - OBR Breakdown - RE
                            case "120": // Communication Expense - OBR Breakdown - JO
                            case "121": // Communication Expense - OBR Breakdown - CE
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                url = url + "~/Reports/";
                                //procedure = "sp_payrollregistry_obr_commx_rep";

                                break;
                            case "111": // Attachment - FOR RATA PAYROLL
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_RATA_attach_rep";
                                url = url + "~/Reports/";

                                break;
                           
                            case "212": // Other Payroll Five - For Regular
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_payslip_re_rep";
                                url = url + "~/Reports/";
                                break;

                            case "213": // PaySLip  - For Job_Order 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_payslip_re_rep";
                                url = url + "~/Reports/";
                                break;

                            case "214": // Other Payroll Five - For Regular
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_salary_payslip_ce_rep";
                                url = url + "~/Reports/";
                                break;
                            
                            
                            case "034": // Honorarium  - For Regular 
                            case "035": // Honorarium  - For Casual 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            case "112": // Other Payroll Attachment  - For Regular 
                            case "113": // Other Payroll Attachment  - For Casual 
                            case "114": // Other Payroll Attachment  - For JO 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth_attach_rep";
                                url = url + "~/Reports/";
                                break;

                            case "950": // Other Payroll - PHIC Share -  RE
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_phic_share_rep";
                                url = url + "~/Reports/";
                                break;

                            case "951": // Other Payroll - BAC Honorarium -  RE
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_bac_rep";
                                url = url + "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2020-09-03 ***********************
                            // ****************** R E M I T T A N C E S ************
                            //******************************************************

                            case "215": // W/Tax Remittance for Subsistence - CE
                            case "216": // W/Tax Remittance for Subsistence - RE
                            case "217": // W/Tax Remittance for Subsistence - JO
                                //printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //url = url + "~/Reports/";

                                //ADDED BY JADE: 2021-03-02
                                if (dataListGridCheck != null)
                                {
                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page2"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        url = url + "~/Reports/";
                                    }
                                    else
                                    {
                                        print_included = false;
                                    }
                                }

                                break;

                            case "223": // Overtime W/Tax Remittance - CE
                            case "224": // Overtime W/Tax Remittance - RE
                            case "225": // Overtime W/Tax Remittance - JO
                                //printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //url = url + "~/Reports/";

                                //ADDED BY JADE: 2021-03-02
                                if (dataListGridCheck != null)
                                {
                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page2"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        url = url + "~/Reports/";
                                    }
                                    else
                                    {
                                        print_included = false;
                                    }
                                }

                                break;

                            case "219": // Subsistence Loan Remittance -  CE
                                //printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //url = url + "~/Reports/";

                                //ADDED BY JADE: 2021-03-02
                                if (dataListGridCheck != null)
                                {
                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page1"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        url = url + "~/Reports/";
                                    }
                                    else
                                    {
                                        print_included = false;
                                    }
                                }
                                break;


                            case "218": // Subsistence Loan Remittance -  RE
                                //printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //url = url + "~/Reports/";

                                //ADDED BY JADE: 2021-03-02
                                if (dataListGridCheck != null)
                                {
                                    if (Convert.ToDecimal(dataListGridCheck.Rows[0]["total_loan_page1"].ToString().Trim()) > 0)
                                    {
                                        printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                        url = url + "~/Reports/";
                                    }
                                    else
                                    {
                                        print_included = false;
                                    }
                                }
                                break;

                            case "220": // Generic Pay Slip - CE
                            case "221": // Generic Pay Slip - RE
                            case "222": // Generic Pay Slip - JO

                                //printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //url = url + "~/Reports/";

                                if (txtb_payroll_template.Text.ToString().Trim() == "041" ||  // Subsistence - RE
                                    txtb_payroll_template.Text.ToString().Trim() == "021")   // Subsistence - CE
                                {
                                    printreport = "/cryOtherPayroll/cryPayslip/cryPS_Subsistence.rpt";
                                    url = url + "~/Reports/";

                                }
                                else if (txtb_payroll_template.Text.ToString().Trim() == "023")  // RATA - RE
                                {
                                    printreport = "/cryOtherPayroll/cryPayslip/cryPS_RATA.rpt";
                                    url = url + "~/Reports/";
                                }
                                else if (txtb_payroll_template.Text.ToString().Trim() == "022" ||   // Overtime Payroll - RE
                                         txtb_payroll_template.Text.ToString().Trim() == "042" ||   // Overtime Payroll - CE
                                         txtb_payroll_template.Text.ToString().Trim() == "061")  // Overtime Payroll - JO
                                {
                                    printreport = "/cryOtherPayroll/cryPayslip/cryPS_Ovtm.rpt";
                                    url = url + "~/Reports/";
                                }
                                else
                                {
                                    printreport = "/cryOtherPayroll/cryPayslip/cryPS_OtherSal.rpt";
                                    url = url + "~/Reports/";
                                }

                                break;
                            // *****************************************************
                            // ****************** N E W  A T T A C H M E N T *******
                            // ****************** 2020-09-14 ***********************
                            //******************************************************

                            case "130": // New Attachment -  RE
                            case "131": // New Attachment -  CE
                            case "132": // New Attachment -  JO
                                //cryPayrollSubReport/cryPayrollFooter_G.rpt
                                printreport = "/cryPayrollSubReport/cryPayrollFooter_G.rpt";
                                url = url + "~/Reports/";
                                break;
                                
                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2020-11-12 *********************
                            //******************************************************

                            case "116": // Monthly Payroll -  Sub Specialist
                                printreport = "/cryJobOrderReports/crySalary/crySalaryMonthly1.rpt";
                                url = url + "~/Reports/";
                                break;
                                
                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2020-11-14 ***********************
                            //******************************************************
                            case "309":  // Obligation Request - Details coming from CAFOA - RE
                            case "310":  // Obligation Request - Details coming from CAFOA - CE
                            case "311":  // Obligation Request - Details coming from CAFOA - JO
                                printreport = "/cryOtherPayroll/cryOBR/cryCAFAO.rpt";
                                url = url + "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2020-11-14 ***********************
                            //******************************************************
                            case "923":  // Special Risk Allowance II - RE
                            case "933":  // Special Risk Allowance II - CE
                            case "943":  // Special Risk Allowance II - JO
                                printreport = "/cryOtherPayroll/cryOthpay/cryOthPay_SRA.rpt";
                                url = "~/Reports/";
                                break;

                            case "924":  // COVID-19 Hazard Pay - RE
                            case "934":  // COVID-19 Hazard Pay - CE
                            case "944":  // COVID-19 Hazard Pay - JO
                                printreport = "/cryOtherPayroll/cryOthpay/cryOthPay_HZD.rpt";
                                url = "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2021-02-12 ***********************
                            //******************************************************
                            case "133":  // Fund Utilization Request and Status (FURS) - RE
                            case "134":  // Fund Utilization Request and Status (FURS) - CE
                            case "135":  // Fund Utilization Request and Status (FURS) - JO
                                printreport = "/cryOtherPayroll/cryOBR/cryFURS.rpt";
                                url = url + "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2021-10-27 ***********************
                            //******************************************************
                            case "939":  // Performance Based Bonus  - CE
                            case "929":  // Performance Based Bonus  - RE
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_PBB.rpt";
                                url = "~/Reports/";
                                break;

                            case "232": 
                            case "233": 
                            case "234": 
                                printreport = dataListGridField.Rows[x]["report_filename"].ToString().Trim();
                                //procedure = "sp_payrollregistry_oth1_rep";
                                url = url + "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2021-10-27 ***********************
                            //******************************************************
                            case "962":  // Service Recognition Incentives (SRI) - RE
                            case "957":  // Service Recognition Incentives (SRI) - CE
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_SRI.rpt";
                                url = "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2022-03-22 ***********************
                            //******************************************************
                            case "963":  // Hazard, Subsistence and Laundry (Differential)
                            case "958":  // Hazard, Subsistence and Laundry (Differential)
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HZD_DIFF.rpt";
                                url = "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2022-05-17 ***********************
                            //******************************************************
                            case "136": // Document Tracking History - RE'
                            case "137": // Document Tracking History - CE'
                            case "138": // Document Tracking History - JO'
                                printreport = "/cryDocTracking/cryDocsHistory.rpt";
                                //url = "~/Reports/";
                                url = url + "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2022-10-24 ***********************
                            //******************************************************
                            case "968":  //  ONE COVID-19 ALLOWANCE (OCA) - RE
                            case "969":  //  ONE COVID-19 ALLOWANCE (OCA) - CE
                            case "970":  //  ONE COVID-19 ALLOWANCE (OCA) - JO
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA.rpt";
                                url =  "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2022-10-24 ***********************
                            //******************************************************
                            case "240":  //  ONE COVID-19 ALLOWANCE (OCA) - RE - Remittance
                            case "241":  //  ONE COVID-19 ALLOWANCE (OCA) - CE - Remittance
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA_Remit_RECE.rpt";
                                url = url + "~/Reports/";
                                break;

                            case "242":  //  ONE COVID-19 ALLOWANCE (OCA) - JO - Remittance
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_OCA_Remit_JO.rpt";
                                url = url + "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2022-10-25 ***********************
                            //******************************************************
                            case "971":  //  PERFORMANCE BASED BONUS FY 2021 - REGULAR
                            case "972":  //  PERFORMANCE BASED BONUS FY 2021 - CASUAL
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_PBB2021.rpt";
                                url = "~/Reports/";
                                break;

                            // *****************************************************
                            // ****************** N E W L Y    A D D E D ***********
                            // ****************** 2023-05-13 ***********************
                            //******************************************************
                            case "974":  // Health Emergency Allowance (HEA) - 'RE'
                            case "975":  // Health Emergency Allowance (HEA) - 'CE'
                            case "976":  // Health Emergency Allowance (HEA) - 'JO'
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA.rpt";
                                url = "~/Reports/";
                                break;

                            case "243":  //  Health Emergency Allowance (HEA) - RE - Remittance
                            case "244":  //  Health Emergency Allowance (HEA) - CE - Remittance
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA_Remit_RECE.rpt";
                                url = url + "~/Reports/";
                                break;

                            case "245":  //  Health Emergency Allowance (HEA) - JO - Remittance
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay_HEA_Remit_JO.rpt";
                                url = url + "~/Reports/";
                                break;

                            case "981":  //  RATA Differential
                                printreport = "/cryOtherPayroll/cryOthPay/cryOthPay5_RATADiff.rpt";
                                url = "~/Reports/";
                                break;
                        }



                        if (print_included == true)
                        {
                            url = url + printreport + "," + procedure + ",par_payroll_year," + txtb_payroll_year.Text.Trim() + ",par_payroll_month," + ViewState["payroll_month"].ToString() + ",par_payroll_registry_nbr," + lbl_registry_number.Text.ToString().Trim() + ",par_payrolltemplate_code," + dataListGridField.Rows[x]["payrolltemplate_code"].ToString().Trim() + ",par_empl_id," + "" + ",par_employment_type," + txtb_empl_type.Text.ToString().Trim() + "," + ",par_mother_template_code," + txtb_payroll_template.Text.ToString().Trim() + ",";
                        }
                    }

                }

                if (printreport != "")
                {
                    url = url.Remove(url.Length - 1, 1);
                    Session["print_all_variables"] = "";
                    Session["print_all_variables"] = url;
                    Response.Redirect("/printViewAll/printViewAll.aspx");
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openModalNoDeductions();", true);
                }
                

            }
        }
        //ADDED BY JORGE : 08-21-2020
        protected void chkbox_print_all_CheckedChanged(object sender, EventArgs e)
        {
            
            foreach (GridViewRow row in gv_dataListGrid.Rows)
            {
                CheckBox temp_chkbox_print = row.FindControl("chkbox_print") as CheckBox;
                Label lbltemplate_code = row.FindControl("lbltemplate_code") as Label;
                string code = lbltemplate_code.Text.ToString();

                if (chkbox_print_all.Checked)
                {
                    temp_chkbox_print.Checked = true;
                }

                else
                {
                    temp_chkbox_print.Checked = false;
                }
                
                

                
            }
        }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}