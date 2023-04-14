//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Print View All
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// JORGE RUSTOM VILLANUEVA     06/03/2019      Code Creation
//**********************************************************************************


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
using System.Management;
using HRIS_Common;
using System.IO;
using System.IO.MemoryMappedFiles;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Diagnostics;



namespace HRIS_ePayroll.prinview
{
    public partial class trypreview1 : System.Web.UI.Page
    {
        ReportDocument cryRpt = new ReportDocument();
        CommonDB MyCmn = new CommonDB();
        static string printfile = "";
        //static string lastpage = "";

        public string obj_url_obj;
        public string obj_url_frame;
        public string obj_url1;

        protected void Page_Init(object sender, EventArgs e)
        {
            // string ls_val;

            if (!IsPostBack)
            {

                hf_printers.Value = "";
                hf_nexpage.Value = "0";
                PrinterSettings settings = new PrinterSettings();
               
            }
            string[] ls_splitvalue;

            //ls_val = Request.QueryString["id"];
            //ls_splitvalue = ls_val.Split(',');
            //ls_val = Request.QueryString["id"];

            if (Session["print_all_variables"] == null || Session["print_all_variables"].ToString() == "")
            {
                Response.Redirect("../View/cDirectToPrinter/cDirectToPrinter.aspx");
            }
            ls_splitvalue = Session["print_all_variables"].ToString().Split(',');
            //ls_val1 = Session["print_all_variables"].ToString();
            //ls_splitvalue1 = ls_val.Split(',');

            loadreport(ls_splitvalue);

        }
        
        private void loadreport(string[] ls_splitvalue)
        {
            //********************************************************************
            //  BEGIN - JRV- 06/03/2019 - Data Place holder creation 
            //********************************************************************

            DataTable dt = null;
            DataTable dtSub = null;
            int pass_val_count = 0;

            //int fixed_parameters     = 8;  // parameters for payroll
            //int dynamic_rpt_location = 8;  // report location for payroll
            //int dynamic_rpt_template = 7;  // template code   for payroll
            //int dynamic_rpt_sp       = 9;  // stored procedure
            //int for_payslip_printing = 0;  // stored procedure


            int fixed_parameters         = 17;  // parameters for payroll
            int dynamic_rpt_location     = 0;   // report location for payroll
            int dynamic_rpt_sp           = 1;   // Stored Procedure for payroll
            int dynamic_rpt_year         = 3;   // Year for payroll
            int dynamic_rpt_month        = 5;   // Month for payroll
            int dynamic_registry_nbr     = 7;   // Registry Number
            int dynamic_template_code    = 9;   // Template Code
            int dynamic_empl_id          = 11;  // Employee Id 
            int dynamic_employment       = 13;  // Employee Employment 
            int dynamic_mother_template  = 16;  // Mother Template
            string multifileslist = string.Empty;
            int x = 0;

            HttpRequest req = System.Web.HttpContext.Current.Request;
            string browserName = req.Browser.Browser;

            string DestinationFile;
            DestinationFile = Server.MapPath("PDFRNew") + Session["ep_user_id"] + "Merge.pdf";
            string DeleteToFile = string.Empty;

            string[] destinationFile1 = DestinationFile.Split('\\');
            string DestinationFilePdf = string.Empty;

            for (int y = 0; y < destinationFile1.Length - 1; y++)
            {
                DestinationFilePdf = DestinationFilePdf + destinationFile1[y] + "\\";

            }

            pass_val_count = ls_splitvalue.Length;

            for (x = 0; x < (pass_val_count / fixed_parameters); x++) //LOOP FOR MULTIPLE REPORTS
            {
                
                dt = MyCmn.RetrieveData(ls_splitvalue[dynamic_rpt_sp], ls_splitvalue[2], ls_splitvalue[dynamic_rpt_year], ls_splitvalue[4], ls_splitvalue[dynamic_rpt_month], ls_splitvalue[6], ls_splitvalue[dynamic_registry_nbr], ls_splitvalue[8], ls_splitvalue[dynamic_template_code], ls_splitvalue[10], ls_splitvalue[dynamic_empl_id], ls_splitvalue[12], ls_splitvalue[dynamic_employment], ls_splitvalue[15], ls_splitvalue[dynamic_mother_template]);

                printfile = ls_splitvalue[dynamic_rpt_location].Trim();
                string[] splitname = ls_splitvalue[dynamic_rpt_location].Split('/');
                string[] fname = splitname[splitname.Length - 1].Split('.');
                string locationpath = printfile;


                if (dt == null)
                {
                    return;
                }
                
                try
                {
                    //GENERATING SELECTED REPORTS

                    ReportDocument cryRpt1 = new ReportDocument();
                    cryRpt1.Load(Server.MapPath(locationpath));
                    cryRpt1.SetDataSource(dt);

                    if (ls_splitvalue[1].ToString().Trim() == "sp_payroll_generate_reports_all")
                    {
                        if (ls_splitvalue[dynamic_template_code].ToString().Trim() == "007" || // RE Monthly Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "008" || // CE Monthly Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "009" || // JO Monthly Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "010" || // JO  1st Quincena Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "011" || // JO  2nd Quincena Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "021" || // RE Hazard, Subsistence and Laundry Pay
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "022" || // RE Overtime Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "023" || // RE RATA Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "024" || // RE Communication Expense Allocation
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "025" || // RE Monetization
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "026" || // RE Mid Year Bonus
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "027" || // RE Year-End and Cash Gift Bonus -Regular
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "028" || // RE Clothing Allowances - Regular
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "029" || // RE Loyalty Bonus
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "030" || // RE Anniversary Bonus
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "031" || // RE Productivity Enhancement Incentive
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "032" || // RE C. N.A.Incentive 2020(Permanent)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "033" || // RE Salary Differential
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "041" || // CE Hazard, Subsistence and Laundry Pay
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "042" || // CE Overtime Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "043" || // CE Communication Expense Allocation
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "044" || // CE Monetization
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "045" || // CE Mid Year Bonus
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "046" || // CE Year-End and Cash Gift Bonus -Casual
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "047" || // CE Clothing Allowances
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "048" || // CE Loyalty Bonus
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "049" || // CE Anniversary Bonus
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "050" || // CE Productivity Enhancement Incentive
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "051" || // CE C. N.A.Incentive 2020(Casual)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "061" || // JO Overtime Payroll
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "062" || // JO Honorarium
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "901" || // JO Adjustments
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "902" || // CE Adjustments
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "903" || // JO Other Pay - Two
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "904" || // JO Other Pay - One
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "905" || // CE Other Pay - Two
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "906" || // CE Other Pay - One
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "907" || // RE Adjustments
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "908" || // RE Other Pay - Two
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "909" || // RE Other Pay - One
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "920" || // RE Peace Keeper's Honorarium
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "921" || // RE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "922" || // RE Special Risk Allowance I
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "923" || // RE Special Risk Allowance II
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "924" || // RE COVID-19 Hazard Pay
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "925" || // RE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "926" || // RE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "927" || // RE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "928" || // RE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "929" || // RE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "930" || // CE Peace Keeper's Honorarium
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "931" || // CE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "932" || // CE Special Risk Allowance I
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "933" || // CE Special Risk Allowance II
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "934" || // CE COVID-19 Hazard Pay
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "935" || // CE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "936" || // CE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "937" || // CE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "938" || // CE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "939" || // CE Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "940" || // JO Peace Keeper's Honorarium
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "941" || // JO PHIC REFUND
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "942" || // JO Special Risk Allowance I
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "943" || // JO Special Risk Allowance II
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "944" || // JO COVID-19 Hazard Pay
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "945" || // JO Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "946" || // JO Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "947" || // JO Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "948" || // JO Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "949" || // JO Other Template(RESERVED)
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "950" || // RE PHIC Share
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "951" || // RE BAC Honorarium
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "052" || // CE Salary Differential
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "063" || // JO Communication Expense Allowance
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "116" || // Monthly Payroll - Sub. Spec.
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "939" || // Performance Based Bonus  - CE
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "929" || // Performance Based Bonus  - RE

                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "232" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "233" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "234" ||

                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "952" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "953" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "954" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "955" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "956" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "957" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "958" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "959" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "960" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "961" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "962" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "963" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "964" ||
                            //ls_splitvalue[dynamic_template_code].ToString().Trim() == "965"
                            
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "240" || //  ONE COVID-19 ALLOWANCE (OCA) - RE - Remittance
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "241" || //  ONE COVID-19 ALLOWANCE (OCA) - CE - Remittance
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "242" || //  ONE COVID-19 ALLOWANCE (OCA) - JO - Remittance

                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "243" || //  Health Emergency Allowance (HEA) - RE - Remittance
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "244" || //  Health Emergency Allowance (HEA) - CE - Remittance
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "245" || //  Health Emergency Allowance (HEA) - JO - Remittance

                            (Convert.ToInt16(ls_splitvalue[dynamic_template_code].ToString().Trim()) >= 920 &&
                            Convert.ToInt16(ls_splitvalue[dynamic_template_code].ToString().Trim()) <= 999)
                            )
                        {
                            DataTable dtSub_insert = null;
                            dtSub_insert = new DataTable();
                            dtSub_insert = MyCmn.RetrieveData("sp_payrollregistry_cafao_rep_new", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[6], ls_splitvalue[7], ls_splitvalue[8], ls_splitvalue[dynamic_template_code]);
                            dtSub = new DataTable();
                            dtSub = MyCmn.RetrieveData("sp_payrollregistry_header_footer_sub_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[6], ls_splitvalue[7]);
                            cryRpt1.Subreports["cryPayrollFooter_A_F.rpt"].SetDataSource(dtSub);
                            cryRpt1.Subreports["cryPayrollHeader.rpt"].SetDataSource(dtSub);
                        }
                        if (ls_splitvalue[dynamic_template_code].ToString().Trim() == "309" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim()  == "310" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim()  == "311" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "133" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "134" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "135"   )
                        {
                            dtSub = new DataTable();
                            dtSub = MyCmn.RetrieveData("sp_payrollregistry_cafao_sub_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[6], ls_splitvalue[7]);
                            cryRpt1.Subreports["cryCAFAO_SubRep.rpt"].SetDataSource(dtSub);
                        }
                        if (ls_splitvalue[dynamic_template_code].ToString().Trim() == "130" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "131" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "132" 
                            )
                        {
                            dtSub = new DataTable();
                            dtSub = MyCmn.RetrieveData("sp_payrollregistry_header_footer_sub_rep", ls_splitvalue[2], ls_splitvalue[3], ls_splitvalue[6], ls_splitvalue[7]);
                        }

                        if (ls_splitvalue[dynamic_template_code].ToString().Trim() == "136" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "137" ||
                            ls_splitvalue[dynamic_template_code].ToString().Trim() == "138"
                            )
                        {
                        }
                    }



                    cryRpt1.ExportToDisk(ExportFormatType.PortableDocFormat, DestinationFilePdf + "pdf" + x.ToString() + Session["ep_user_id"] + ".pdf");
                    multifileslist += "pdf" + x.ToString() + Session["ep_user_id"] + ".pdf,";
                    PrinterSettings settings = new PrinterSettings();
                    cryRpt1.Close();
                    cryRpt1.Dispose();
                }
                catch (Exception)
                {
                }
                
                dynamic_rpt_location = dynamic_rpt_location + fixed_parameters;
                dynamic_rpt_sp = dynamic_rpt_sp + fixed_parameters;
                dynamic_rpt_year = dynamic_rpt_year + fixed_parameters;
                dynamic_rpt_month = dynamic_rpt_month + fixed_parameters;
                dynamic_registry_nbr = dynamic_registry_nbr + fixed_parameters;
                dynamic_template_code = dynamic_template_code + fixed_parameters;
                dynamic_empl_id = dynamic_empl_id + fixed_parameters;
                dynamic_employment = dynamic_employment + fixed_parameters;
                dynamic_mother_template = dynamic_mother_template + fixed_parameters;



            }


            if (multifileslist == "")
            {
                //Response.Redirect("../View/cPayRegistry/cPayRegistry.aspx");
                return;
            }

            multifileslist = multifileslist.Remove(multifileslist.Length - 1, 1);

            string[] multifiles = multifileslist.Split(',');

            MergeFiles(DestinationFile, multifiles); // COMBINING REPORT FILES

            if (browserName == "InternetExplorer" || browserName == "Edge")
            {
                obj_url_obj = "<object id = 'report_obj' data='PDFRNew" + Session["ep_user_id"] + "Merge.pdf' height='0' width='0'  name = 'report_obj' type='application/pdf' ></object>";

            }
            

            else
            {
                obj_url_obj = "<iframe id = 'report_obj' src='PDFRNew" + Session["ep_user_id"] + "Merge.pdf' height='0' width='0'  name = 'report_obj' ></iframe>";
                obj_url1 = "PDFRNew" + Session["ep_user_id"] + "Merge.pdf";
            }

            for (int z = 0; z < 10; z++) //DELETE REPORT FILES
            {
                if (File.Exists(@"" + DestinationFilePdf + "pdf" + z.ToString() + Session["ep_user_id"] + ".pdf"))
                {
                    DeleteToFile = Server.MapPath("pdf") + z.ToString() + Session["ep_user_id"] + ".pdf";
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(DeleteToFile);
                }

            }


        }

        //********************************************************************
        //  BEGIN - JRV- 06/10/2018 - Data Place holder creation 
        //********************************************************************

        private void MergeFiles(string DestinationFile, string[] SourceFile)

        {
            try
            {
                int f = 0;


                string[] destinationFile1 = DestinationFile.Split('\\');
                string DestinationFilePdf = "";
                for (int y = 0; y < destinationFile1.Length - 1; y++)
                {
                    DestinationFilePdf = DestinationFilePdf + destinationFile1[y] + "\\";

                }

                PdfReader reader = new PdfReader(@""+DestinationFilePdf + SourceFile[f]);
                int n = reader.NumberOfPages;

                Document document = new Document(reader.GetPageSizeWithRotation(1));



                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(DestinationFile, FileMode.Create));

                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;

                while (f < SourceFile.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);

                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);

                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                    }

                    

                    f++;
                    if (f < SourceFile.Length)
                    {
                        reader = new PdfReader(@"" + DestinationFilePdf + SourceFile[f]);
                        n = reader.NumberOfPages;

                    }

                }



                document.Close();

            }

            catch (Exception)
            {
            }

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            cryRpt.Close();
            cryRpt.Dispose();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //DELETE EXISTING REPORTS

            string DestinationFile;
            DestinationFile = Server.MapPath("PDFRNew") + Session["ep_user_id"] + "Merge.pdf";

            if (File.Exists(@"" + DestinationFile))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(DestinationFile);
            }
            Response.Redirect("../View/cDirectToPrinter/cDirectToPrinter.aspx");
        }

        //********************************************************************
        //  BEGIN - JRV- 06/15/2018 - Data Place holder creation 
        //********************************************************************

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    //DELETE EXISTING REPORTS

        //    string DestinationFile;
        //    DestinationFile = Server.MapPath("PDFRNew") + Session["ep_user_id"] + "Merge.pdf";

        //    if (File.Exists(@"" + DestinationFile))
        //    {
        //        System.GC.Collect();
        //        System.GC.WaitForPendingFinalizers();
        //        File.Delete(DestinationFile);
        //    }
        //    Response.Redirect("../View/cDirectToPrinter/cDirectToPrinter.aspx");
        //}
    }
}