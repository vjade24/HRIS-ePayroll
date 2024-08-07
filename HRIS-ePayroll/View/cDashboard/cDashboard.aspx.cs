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
using Newtonsoft.Json;
using System.Web.Services;


namespace HRIS_ePayroll.View
{
    public partial class cDashboard : System.Web.UI.Page
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
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    
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
                    ViewState["page_allow_add"]          = 0;
                    ViewState["page_allow_delete"]       = 0;
                    ViewState["page_allow_edit"]         = 0;
                    ViewState["page_allow_edit_history"] = 0;
                    ViewState["page_allow_print"]        = 0;
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

                }
            }
        }
        [WebMethod]
        public static string PayrollAppropriation(string year, string type, string function_code, string par_period_from, string par_period_to,string par_payrolltemplate_code)
        {
            try
            {
                function_code = function_code == null ? "" : function_code;
                CommonDB MyCmn  = new CommonDB();
                DataTable dt    = MyCmn.RetrieveData("sp_payroll_dashboard", "par_appropriation_year", year, "par_type", type, "par_function_code", function_code, "par_period_from", par_period_from, "par_period_to", par_period_to, "par_payrolltemplate_code", par_payrolltemplate_code);
                string json     = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
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
        public static string PayrollCharges(string year)
        {
            try
            {
                CommonDB MyCmn  = new CommonDB();
                DataTable dt    = new DataTable();
                string query    = "SELECT DISTINCT appropriation_year,function_code,function_descr,SUM(appropriation_mt) AS appropriation_mt FROM payrollcharges WHERE appropriation_year = '" + year.ToString().Trim() + "' GROUP BY appropriation_year,function_code,function_descr";
                dt              = MyCmn.GetDatatable(query);
                string json     = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
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
        public static string PayrollTemplate()
        {
            try
            {
                CommonDB MyCmn = new CommonDB();
                DataTable dt = new DataTable();
                string query = "SELECT *FROM payrolltemplate_tbl WHERE payrolltemplate_type IN ('01','07','08')";
                dt = MyCmn.GetDatatable(query);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
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
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}