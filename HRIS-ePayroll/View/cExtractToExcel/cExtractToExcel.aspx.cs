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
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace HRIS_ePayroll.View
{
    public partial class cExtractToExcel : System.Web.UI.Page
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
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btn_create_generate);

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

                //ddl_month.SelectedValue     = DateTime.Now.Month.ToString().Trim().PadLeft(2,'0');
                ddl_month.SelectedValue         = "";
                ddl_empl_type.SelectedValue     = "JO";

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
            RetriveEmploymentType();
            ClearEntry();

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
           
        }
       
        //**************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false,"ALL");
            
            if (ddl_empl_type.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_empl_type");
                ddl_empl_type.Focus();
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
                    case "ddl_empl_type":
                        {
                            ddl_empl_type.BorderColor           = Color.Red;
                            LblRequired3.Text                   = MyCmn.CONST_RQDFLD;
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
                            LblRequired3.Text                   = "";
                            
                            ddl_empl_type.BorderColor           = Color.LightGray;
                            break;
                        }
                }
            }
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel10.Update();
        }

        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel10.Update();
        }

        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel10.Update();
        }

        protected void btn_create_generate_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                var user_id         = Session["ep_user_id"].ToString().Trim();
                var date_extract    = DateTime.Now.ToString("yyyy_MM_dd_HHmm");

                string extract_type         = ddl_report_type.SelectedValue.ToString().Trim();
                Excel.Application xlApp     = new Excel.Application();
                    
                Excel.Workbook xlWorkBook   = xlApp.Workbooks.Open(Server.MapPath("~/UploadFile/Extract_"+ddl_empl_type.SelectedValue.ToString().Trim()+"_CAFOA.xlsx"));
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                object misValue             = System.Reflection.Missing.Value;

                Excel.Range xlRange = xlWorkSheet.UsedRange;
                int totalRows       = xlRange.Rows.Count;
                int totalColumns    = xlRange.Columns.Count;

                DataTable gsis_result = new DataTable();
                if (extract_type == "CAFOA")
                {
                    gsis_result = MyCmn.RetrieveData("sp_extract_cafoa", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_post_status", ddl_post_status.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

                    if (gsis_result.Rows.Count > 0)
                    {
                        //start_row initial value for the starting row
                        int start_row = 2;
                        //xlWorkSheet.Cells[2, 2] = gsis_result.Rows[0]["agency_name"];
                        //xlWorkSheet.Cells[3, 2] = gsis_result.Rows[0]["gency_bp_nbr"];
                        for (int x = 1; x <= gsis_result.Rows.Count; x++)
                        {

                            xlWorkSheet.Cells[start_row, 1]  = gsis_result.Rows[x - 1]["department_code"];
                            xlWorkSheet.Cells[start_row, 2]  = gsis_result.Rows[x - 1]["department_short_name"];
                            xlWorkSheet.Cells[start_row, 3]  = gsis_result.Rows[x - 1]["function_code"];
                            xlWorkSheet.Cells[start_row, 4]  = gsis_result.Rows[x - 1]["payroll_year"];
                            xlWorkSheet.Cells[start_row, 5]  = gsis_result.Rows[x - 1]["payroll_month"];
                            xlWorkSheet.Cells[start_row, 6]  = gsis_result.Rows[x - 1]["payroll_registry_nbr"];
                            xlWorkSheet.Cells[start_row, 7]  = gsis_result.Rows[x - 1]["payroll_registry_descr"];
                            xlWorkSheet.Cells[start_row, 8]  = gsis_result.Rows[x - 1]["payrolltemplate_descr"];
                            xlWorkSheet.Cells[start_row, 9]  = gsis_result.Rows[x - 1]["post_status_descr"];
                            xlWorkSheet.Cells[start_row, 10] = gsis_result.Rows[x - 1]["cafoa_amt"];

                            start_row = start_row + 1;
                        }

                        //DisplayAlert so that the excel will not popup
                        xlApp.DisplayAlerts = false;
                        string filename = "";
                        filename = "Extract_" + ddl_empl_type.SelectedValue.ToString().Trim()+"_" + extract_type + "_"+ user_id + "_" + date_extract + ".xlsx";

                        xlWorkBook.SaveAs(Server.MapPath("~/UploadFile/" + filename), Excel.XlFileFormat.xlOpenXMLWorkbook,
                            Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                            Excel.XlSaveConflictResolution.xlLocalSessionChanges, Missing.Value, Missing.Value,
                            Missing.Value, Missing.Value);
                        xlWorkBook.Close();
                        xlApp.Quit();

                        //release the created object from the marshal so that it will be no longer in
                        //backend application process..
                        Marshal.ReleaseComObject(xlWorkSheet);
                        Marshal.ReleaseComObject(xlWorkBook);
                        Marshal.ReleaseComObject(xlApp);

                        //this line allow client to download the created file in the server down to their
                        //computers...
                        ScriptManager.RegisterStartupScript(this, GetType(), "PopSelectReportX1XX", "closeLoading();", true);
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                        Response.WriteFile(Server.MapPath("~/UploadFile/" + filename));
                        Response.Write("<script language='javascript'>");
                        Response.Write(" $(function() {$('#Loading').modal('hide'); $('#tableX').click();});");
                        Response.Write("<" + "/script>");
                        Response.Flush();
                        Response.End();
                    }
                }

            }
        }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}