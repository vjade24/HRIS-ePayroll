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
using System.Web.Services;
using Newtonsoft.Json;

namespace HRIS_ePayroll.View.cMortuary
{
    public partial class cMortuary : System.Web.UI.Page
    {
        CommonDB MyCmn = new CommonDB();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "empl_id";
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
                ViewState["page_allow_view"]        = Master.allow_view;
                if (Master.allow_view == "1")
                {
                    ViewState["page_allow_add"]             = 0;
                    ViewState["page_allow_delete"]          = 0;
                    ViewState["page_allow_edit"]            = 0;
                    ViewState["page_allow_edit_history"]    = 0;
                    ViewState["page_allow_print"]           = 0;
                }
                else
                {
                    ViewState["page_allow_add"]             = Master.allow_add;
                    ViewState["page_allow_delete"]          = Master.allow_delete;
                    ViewState["page_allow_edit"]            = Master.allow_edit;
                    ViewState["page_allow_edit_history"]    = Master.allow_edit_history;
                    ViewState["page_allow_print"]           = Master.allow_print;
                }
            }
        }
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
        }

        [WebMethod]
        public static string MortuaryList(string payroll_year,string payroll_month)
        {
            try
            {
                CommonDB MyCmn = new CommonDB();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM mortuary_tbl WHERE payroll_year = '" + payroll_year.ToString().Trim() + "' AND payroll_month = '"+ payroll_month.ToString().Trim() + "'";
                dt = MyCmn.GetDatatable(query);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
                return json;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                throw;
            }
        }
    }
}