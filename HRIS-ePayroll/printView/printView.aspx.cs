using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data;
using HRIS_Common;
namespace HRIS_ePayroll.prinview
{
    public partial class trypreview : System.Web.UI.Page
    {
        ReportDocument cryRpt = new ReportDocument();
        CommonDB MyCmn = new CommonDB();
        static string printfile = "";
        //static string lastpage = "";
        //bool firstload = true;
        protected void Page_Init(object sender, EventArgs e)
        {
            string ls_val;
            
            if (!IsPostBack)
            {

                hf_printers.Value = "";
                hf_nexpage.Value = "0";
                PrinterSettings settings = new PrinterSettings();
                //firstload = true;
            }
            else
            {
                //firstload = false;
            }
            string[] ls_splitvalue;
            ls_val = Request.QueryString["id"];
            ls_splitvalue = ls_val.Split(',');
            loadreport(ls_splitvalue);

            //lastpage = cryRpt.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext()).ToString();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setPageDisplay();", true);
            
        }
        

        protected void Page_Unload(object sender, EventArgs e)
        {
            cryRpt.Close();
            cryRpt.Dispose();
        }
        private void loadreport(string[] ls_splitvalue)
        {
            if (Session["can_print_on_preview"] != null  &&
                Session["can_print_on_preview"].ToString() == "false")
            {
                crvPrint.HasExportButton = false;
                crvPrint.HasPrintButton  = false;
            }

            
            DataTable dt = null;
            DataTable dtSub = null;
            printfile = ls_splitvalue[0].Trim();
            string[] splitname = ls_splitvalue[0].Split('/');
            string[] fname = splitname[splitname.Length - 1].Split('.');


            string locationpath = printfile;
            cryRpt.Load(Server.MapPath(locationpath));
            if (ls_splitvalue.Length == 2)
            {

                dt = MyCmn.RetrieveData(ls_splitvalue[1]);
                //dt = customerdb.get_data(ls_splitvalue[1], Session["cust_account_no"].ToString(), Convert.ToInt32(Session["countryid"].ToString()), Session["comp_code"].ToString(), Session["branch_code"].ToString(), Convert.ToInt32(Session["franchise"].ToString()));
            }
            if (ls_splitvalue.Length == 4)
            {
                dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3]);
                //dt = customerdb.get_data(ls_splitvalue[1], Session["cust_account_no"].ToString(), Convert.ToInt32(Session["countryid"].ToString()), Session["comp_code"].ToString(), Session["branch_code"].ToString(), Convert.ToInt32(Session["franchise"].ToString()), );
            }
            if (ls_splitvalue.Length == 6)
            {
                dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5]);
                // dt = customerdb.get_data(ls_splitvalue[1], Session["cust_account_no"].ToString(), Convert.ToInt32(Session["countryid"].ToString()), Session["comp_code"].ToString(), Session["branch_code"].ToString(), Convert.ToInt32(Session["franchise"].ToString()), ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5]);
            }
            if (ls_splitvalue.Length == 7)
            {
                dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7]);
                //if (ls_splitvalue[5] == "N")
                //{
                //dt = MyCmn.RetrieveData(ls_splitvalue[1], Session["cust_account_no"].ToString(), Convert.ToInt32(Session["countryid"].ToString()), Session["comp_code"].ToString(), Session["branch_code"].ToString(), Convert.ToInt32(Session["franchise"].ToString()), ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], Convert.ToInt32(ls_splitvalue[6]));
                //}
                //else
                //{
                //    dt = customerdb.get_data(ls_splitvalue[1], Session["cust_account_no"].ToString(), Convert.ToInt32(Session["countryid"].ToString()), Session["comp_code"].ToString(), Session["branch_code"].ToString(), Convert.ToInt32(Session["franchise"].ToString()), ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[6]);
                //}
            }
            if (ls_splitvalue.Length == 8)
            {
                dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7]);
                //  dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8]);
                //                dt = customerdb.get_data(ls_splitvalue[1], Session["cust_account_no"].ToString(), Convert.ToInt32(Session["countryid"].ToString()), Session["comp_code"].ToString(), Session["branch_code"].ToString(), Convert.ToInt32(Session["franchise"].ToString()), ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7]);
            }
            if (ls_splitvalue.Length == 10)
            {
                //dt = customerdb.get_data(ls_splitvalue[1], Session["cust_account_no"].ToString(), Convert.ToInt32(Session["countryid"].ToString()), Session["comp_code"].ToString(), Session["branch_code"].ToString(), Convert.ToInt32(Session["franchise"].ToString()), ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9]);
                 dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9]);

            }
            if (ls_splitvalue.Length == 12) 
                // This is for the Voucher Report
            {
                dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9], ls_splitvalue[10], ls_splitvalue[11]);
            }

            if (ls_splitvalue.Length == 14)
            // This is for the Voucher Report
            {
                dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9], ls_splitvalue[10], ls_splitvalue[11], ls_splitvalue[12], ls_splitvalue[13]);
            }
            if (ls_splitvalue.Length == 16)
            // This is for the Voucher Report
            {
                dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9], ls_splitvalue[10], ls_splitvalue[11], ls_splitvalue[12], ls_splitvalue[13], ls_splitvalue[14], ls_splitvalue[15]);
            }
            //if (ls_splitvalue.Length == 12 & (Session["BODY_LINE"].ToString() == "" || Session["BODY_LINE"].ToString() != "")) 
            //    // 12 Length and Not or Equal to blank the BODY LINE for eRsp Module
            //{
            //    dt = MyCmn.RetrieveData(ls_splitvalue[1], ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9], ls_splitvalue[10], ls_splitvalue[11], "par_letter_body_line1", Session["BODY_LINE"].ToString().Trim());
            //}
            else
            {
                //Label1.Text = ls_splitvalue.Length.ToString();
            }
            if (dt == null)
            {
                return;
            }
            
            try
            {
                cryRpt.SetDataSource(dt);


                // VJA : 2020-09-04 - For Sub Report Organization Table
                if (ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_takehome" ||
                    ls_splitvalue[1].ToString().Trim() == "sp_list_of_employees_report" ||
                    ls_splitvalue[1].ToString().Trim() == "sp_payrollemployeemaster_rep" ||
                    ls_splitvalue[1].ToString().Trim() == "sp_employee_list_age"
                    )
                {
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_organizations_tbl", "organization_code", "1");
                    cryRpt.Subreports["cryOrganization.rpt"].SetDataSource(dtSub);
                }
                // VJA : 2020-10-24 - For Sub Report Footer - New Payroll - Regular
                else if (ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_salary_re_ce_rep" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_salary_ce_rep" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_salary_jo_rep" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_subs_rep" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_ovtm_rep" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_rata_rep" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_oth1_rep" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_salary_diff_rep" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_othpay_rep")
                {
                    if (ls_splitvalue[7].ToString().Trim() == "007" || // RE Monthly Payroll
                        ls_splitvalue[7].ToString().Trim() == "008" || // CE Monthly Payroll
                        ls_splitvalue[7].ToString().Trim() == "009" || // JO Monthly Payroll
                        ls_splitvalue[7].ToString().Trim() == "010" || // JO  1st Quincena Payroll
                        ls_splitvalue[7].ToString().Trim() == "011" || // JO  2nd Quincena Payroll
                        ls_splitvalue[7].ToString().Trim() == "021" || // RE Hazard, Subsistence and Laundry Pay
                        ls_splitvalue[7].ToString().Trim() == "022" || // RE Overtime Payroll
                        ls_splitvalue[7].ToString().Trim() == "023" || // RE RATA Payroll
                        ls_splitvalue[7].ToString().Trim() == "024" || // RE Communication Expense Allocation
                        ls_splitvalue[7].ToString().Trim() == "025" || // RE Monetization
                        ls_splitvalue[7].ToString().Trim() == "026" || // RE Mid Year Bonus
                        ls_splitvalue[7].ToString().Trim() == "027" || // RE Year-End and Cash Gift Bonus -Regular
                        ls_splitvalue[7].ToString().Trim() == "028" || // RE Clothing Allowances - Regular
                        ls_splitvalue[7].ToString().Trim() == "029" || // RE Loyalty Bonus
                        ls_splitvalue[7].ToString().Trim() == "030" || // RE Anniversary Bonus
                        ls_splitvalue[7].ToString().Trim() == "031" || // RE Productivity Enhancement Incentive
                        ls_splitvalue[7].ToString().Trim() == "032" || // RE C. N.A.Incentive 2020(Permanent)
                        ls_splitvalue[7].ToString().Trim() == "033" || // RE Salary Differential
                        ls_splitvalue[7].ToString().Trim() == "041" || // CE Hazard, Subsistence and Laundry Pay
                        ls_splitvalue[7].ToString().Trim() == "042" || // CE Overtime Payroll
                        ls_splitvalue[7].ToString().Trim() == "043" || // CE Communication Expense Allocation
                        ls_splitvalue[7].ToString().Trim() == "044" || // CE Monetization
                        ls_splitvalue[7].ToString().Trim() == "045" || // CE Mid Year Bonus
                        ls_splitvalue[7].ToString().Trim() == "046" || // CE Year-End and Cash Gift Bonus -Casual
                        ls_splitvalue[7].ToString().Trim() == "047" || // CE Clothing Allowances
                        ls_splitvalue[7].ToString().Trim() == "048" || // CE Loyalty Bonus
                        ls_splitvalue[7].ToString().Trim() == "049" || // CE Anniversary Bonus
                        ls_splitvalue[7].ToString().Trim() == "050" || // CE Productivity Enhancement Incentive
                        ls_splitvalue[7].ToString().Trim() == "051" || // CE C. N.A.Incentive 2020(Casual)
                        ls_splitvalue[7].ToString().Trim() == "061" || // JO Overtime Payroll
                        ls_splitvalue[7].ToString().Trim() == "062" || // JO Honorarium
                        ls_splitvalue[7].ToString().Trim() == "901" || // JO Adjustments
                        ls_splitvalue[7].ToString().Trim() == "902" || // CE Adjustments
                        ls_splitvalue[7].ToString().Trim() == "903" || // JO Other Pay - Two
                        ls_splitvalue[7].ToString().Trim() == "904" || // JO Other Pay - One
                        ls_splitvalue[7].ToString().Trim() == "905" || // CE Other Pay - Two
                        ls_splitvalue[7].ToString().Trim() == "906" || // CE Other Pay - One
                        ls_splitvalue[7].ToString().Trim() == "907" || // RE Adjustments
                        ls_splitvalue[7].ToString().Trim() == "908" || // RE Other Pay - Two
                        ls_splitvalue[7].ToString().Trim() == "909" || // RE Other Pay - One
                        ls_splitvalue[7].ToString().Trim() == "920" || // RE Peace Keeper's Honorarium
                        ls_splitvalue[7].ToString().Trim() == "921" || // RE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "922" || // RE Special Risk Allowance I
                        ls_splitvalue[7].ToString().Trim() == "923" || // RE Special Risk Allowance II
                        ls_splitvalue[7].ToString().Trim() == "924" || // RE COVID-19 Hazard Pay
                        ls_splitvalue[7].ToString().Trim() == "925" || // RE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "926" || // RE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "927" || // RE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "928" || // RE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "929" || // RE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "930" || // CE Peace Keeper's Honorarium
                        ls_splitvalue[7].ToString().Trim() == "931" || // CE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "932" || // CE Special Risk Allowance I
                        ls_splitvalue[7].ToString().Trim() == "933" || // CE Special Risk Allowance II
                        ls_splitvalue[7].ToString().Trim() == "934" || // CE COVID-19 Hazard Pay
                        ls_splitvalue[7].ToString().Trim() == "935" || // CE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "936" || // CE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "937" || // CE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "938" || // CE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "939" || // CE Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "940" || // JO Peace Keeper's Honorarium
                        ls_splitvalue[7].ToString().Trim() == "941" || // JO PHIC REFUND
                        ls_splitvalue[7].ToString().Trim() == "942" || // JO Special Risk Allowance I
                        ls_splitvalue[7].ToString().Trim() == "943" || // JO Special Risk Allowance II
                        ls_splitvalue[7].ToString().Trim() == "944" || // JO COVID-19 Hazard Pay
                        ls_splitvalue[7].ToString().Trim() == "945" || // JO Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "946" || // JO Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "947" || // JO Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "948" || // JO Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "949" || // JO Other Template(RESERVED)
                        ls_splitvalue[7].ToString().Trim() == "950" || // RE PHIC Share
                        ls_splitvalue[7].ToString().Trim() == "951" || // RE BAC Honorarium
                        ls_splitvalue[7].ToString().Trim() == "052" || // CE Salary Differential
                        ls_splitvalue[7].ToString().Trim() == "063" || // JO Communication Expense Allowance
                        ls_splitvalue[7].ToString().Trim() == "116" ||   // Monthly Payroll - Sub. Spec.
                        ls_splitvalue[7].ToString().Trim() == "232" ||  
                        ls_splitvalue[7].ToString().Trim() == "233" ||  
                        ls_splitvalue[7].ToString().Trim() == "234" ||

                        //ls_splitvalue[7].ToString().Trim() == "952" ||
                        //ls_splitvalue[7].ToString().Trim() == "953" ||
                        //ls_splitvalue[7].ToString().Trim() == "954" ||
                        //ls_splitvalue[7].ToString().Trim() == "955" ||
                        //ls_splitvalue[7].ToString().Trim() == "956" ||
                        //ls_splitvalue[7].ToString().Trim() == "957" ||
                        //ls_splitvalue[7].ToString().Trim() == "958" ||
                        //ls_splitvalue[7].ToString().Trim() == "959" ||
                        //ls_splitvalue[7].ToString().Trim() == "960" ||
                        //ls_splitvalue[7].ToString().Trim() == "961" ||
                        //ls_splitvalue[7].ToString().Trim() == "962" ||
                        //ls_splitvalue[7].ToString().Trim() == "963" ||
                        //ls_splitvalue[7].ToString().Trim() == "964" ||
                        //ls_splitvalue[7].ToString().Trim() == "965" 

                        (Convert.ToInt16(ls_splitvalue[7].ToString().Trim()) >= 920 &&
                         Convert.ToInt16(ls_splitvalue[7].ToString().Trim()) <= 999)
                        )   
                    {
                        dtSub = new DataTable();
                        dtSub = MyCmn.RetrieveData("sp_payrollregistry_header_footer_sub_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5]);
                        cryRpt.Subreports["cryPayrollFooter_A_F.rpt"].SetDataSource(dtSub);
                        cryRpt.Subreports["cryPayrollHeader.rpt"].SetDataSource(dtSub);

                    }

                }

                // VJA : 2020-11-12 - For Sub Report Obligation Request (CAFOA) 
                else if (ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_cafao_rep_new" ||
                         ls_splitvalue[1].ToString().Trim() == "sp_voucher_obr_rep")
                {
                    var par_descr = ls_splitvalue[4];
                    if (ls_splitvalue[1].ToString().Trim() == "sp_voucher_obr_rep")
                    {
                        par_descr = "par_payroll_registry_nbr";
                    }
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_payrollregistry_cafao_sub_rep", ls_splitvalue[2], ls_splitvalue[3], par_descr, ls_splitvalue[5]);
                    cryRpt.Subreports["cryCAFAO_SubRep.rpt"].SetDataSource(dtSub);
                }
                // VJA : 2021-01-16 - Sub Report for Maternity Remittance
                else if (ls_splitvalue[1].ToString().Trim() == "sp_voucher_dtl_tbl_rep")
                {
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_voucher_dtl_tbl_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9]);
                    cryRpt.Subreports["cryMaternityRemit_Mandatory.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryMaternityRemit_Optional.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryMaternityRemit_Loan1.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryMaternityRemit_Loan2.rpt"].SetDataSource(dtSub);
                }

                // VJA : 2021-01-19 - Sub Report for Voucher Remittance
                else if (ls_splitvalue[1].ToString().Trim() == "sp_voucher_tbl_repo" &&
                        (ls_splitvalue[9].ToString() == "229" ||
                         ls_splitvalue[9].ToString() == "230" ||
                         ls_splitvalue[9].ToString() == "231" ||
                         ls_splitvalue[9].ToString() == "603" || // Terminal Leave
                         ls_splitvalue[9].ToString() == "703" || // Terminal Leave
                         ls_splitvalue[9].ToString() == "803" || // Terminal Leave
                         ls_splitvalue[9].ToString() == "601" || // Refund to Employee  - Regular     
                         ls_splitvalue[9].ToString() == "701" || // Refund to Employee  - Casual      
                         ls_splitvalue[9].ToString() == "801" || // Refund to Employee  - Job-Order                                        
                         ls_splitvalue[9].ToString() == "605" ||
                         ls_splitvalue[9].ToString() == "705" ||
                         ls_splitvalue[9].ToString() == "805"))
                {
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_voucher_tbl_repo", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9], ls_splitvalue[10], ls_splitvalue[11]);
                    cryRpt.Subreports["cryVoucherRemit_Mandatory.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryVoucherRemit_Optional.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryVoucherRemit_Loan1.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryVoucherRemit_Loan2.rpt"].SetDataSource(dtSub);
                }
                // VJA : 2020-10-24 - For Sub Report Footer - New Payroll - Regular
                else if (ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_bac_rep")
                {
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_payrollregistry_header_footer_sub_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5]);
                    cryRpt.Subreports["cryPayrollFooter_A_F.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryPayrollHeader.rpt"].SetDataSource(dtSub);
                }
                if ((ls_splitvalue[1].ToString().Trim() == "sp_voucher_tbl_repo2" &&
                    (ls_splitvalue[9].ToString() == "603" || // Terminal Leave
                    ls_splitvalue[9].ToString()  == "703" || // Terminal Leave
                    ls_splitvalue[9].ToString()  == "803" || // Terminal Leave
                    ls_splitvalue[9].ToString()  == "605" || // Other Salries
                    ls_splitvalue[9].ToString()  == "705" || // Other Salries
                    ls_splitvalue[9].ToString()  == "805"    // Other Salries
                    ))
                    ||
                    ls_splitvalue[9].ToString() == "610" || // Other Claims v2
                    ls_splitvalue[9].ToString() == "611" || // Other Claims v2
                    ls_splitvalue[9].ToString() == "612"    // Other Claims v2
                    )  
                {
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_voucher_header_footer_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9]);
                    cryRpt.Subreports["cryVoucherFooter.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryVoucherHeader.rpt"].SetDataSource(dtSub);
                }
                if (ls_splitvalue[1].ToString().Trim() == "voucher_dtl_oth_claims_tbl_rep")
                {
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_voucher_header_footer_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9]);
                    cryRpt.Subreports["cryVoucherFooter.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryVoucherHeader.rpt"].SetDataSource(dtSub);
                }
                if (ls_splitvalue[1].ToString().Trim() == "sp_payrollregistry_phic_share_rep")
                {
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_payrollregistry_header_footer_sub_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[4], ls_splitvalue[5]);
                    cryRpt.Subreports["cryPayrollFooter_A_F.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryPayrollHeader.rpt"].SetDataSource(dtSub);
                }
            }
            catch (Exception)
            {
                // VJA : 2021-01-19 - Sub Report for Voucher Remittance
                if (ls_splitvalue[1].ToString().Trim() == "sp_voucher_tbl_repo2" || 
                    ls_splitvalue[1].ToString().Trim() == "sp_voucher_tbl_repo"  ||
                    ls_splitvalue[1].ToString().Trim() == "voucher_dtl_oth_claims_tbl_rep")
                {
                    dtSub = new DataTable();
                    dtSub = MyCmn.RetrieveData("sp_voucher_header_footer_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[9]);
                    cryRpt.Subreports["cryVoucherFooter.rpt"].SetDataSource(dtSub);
                    cryRpt.Subreports["cryVoucherHeader.rpt"].SetDataSource(dtSub);
                }
            }

            crvPrint.ReportSource = cryRpt;
            crvPrint.DataBind();
            PrinterSettings settings = new PrinterSettings();
        }

        private void BindReport(ReportDocument ReportPath)
        {
            crvPrint.ReportSource = ReportPath;
            crvPrint.DataBind();

        }
        private void shownextpage(int pageno)
        {
            crvPrint.ShowNthPage(pageno);
            hf_nexpage.Value = "0";
            
        }
        private void shoprevpage()
        {
            crvPrint.ShowPreviousPage();
           
        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            
            try
            {
                cryRpt.Refresh();

                switch (printfile)
                {
                    case "~/Reports/cryPlantilla/cryPlantilla.rpt":
                        cryRpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                        cryRpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal;
                break;
                    case "~/Reports/cryPlantillaCSC/cryPlantillaCSC.rpt":
                        cryRpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                        cryRpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal;
                break;
                    case "~/Reports/cryPlantillaHR/cryPlantillaHR.rpt":
                        cryRpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                        cryRpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal;
                break;
                    case "~/Reports/cryPSSalariesWages/cryPSSalariesWages.rpt":
                        cryRpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                        cryRpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter;
                break;
                    case "~/Reports/cryVacantItems/cryVacantItems.rpt":
                        cryRpt.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                        cryRpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter;
                break;
                    case "~/Reports/cryListOfEmployees/cryListOfEmployees.rpt":
                        cryRpt.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                        cryRpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter;
                break;
                    default:
                    
                break;
                }
            }
            catch (Exception)
            {
            }
          
        }

        private string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                {
                    return printer;
                }
            }
            return string.Empty;
        }

        protected void btn_close_Click(object sender, EventArgs e)
        {
            closepage();
        }
        private void closepage()
        {
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        }

        protected void img_nextpage_Click(object sender, ImageClickEventArgs e)
        {
            crvPrint.ShowNextPage();

        }
        protected void lbtn_pdf_Click(object sender, ImageClickEventArgs e)
        {
            converttopdf();
           
        }
        private void converttopdf()
        {
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = @"c:\\pdf\Plantilla.pdf";
                CrExportOptions = cryRpt.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                cryRpt.Export();
               
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void lbtn_pdf_Click(object sender, EventArgs e)
        {
            converttopdf();
        }
        
        protected void btn_save_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "Clickprint();", true);
        }

        protected void crvPrint_Load(object sender, EventArgs e)
        {
        //    if (Session["first_load"].ToString() == "true")
        //     {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setPageDisplay();", true);
        //     }
        }
    }
}