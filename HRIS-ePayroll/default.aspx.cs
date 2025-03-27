//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Barangay Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio.      12/04/2018      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Drawing;
using Newtonsoft.Json;
using System.Web.Services;

namespace HRIS_ePayroll
{
    public partial class defualt : System.Web.UI.Page
    {
        CommonDB MyCmn = new CommonDB();
        //********************************************************************
        //  BEGIN - JMTJR- 09/03/2018 - Data Place holder creation 
        //********************************************************************
        DataTable dtMenuSource
        {
            get
            {
                if ((DataTable)ViewState["dtMenuSource"] == null) return null;
                return (DataTable)ViewState["dtMenuSource"];
            }
            set
            {
                ViewState["dtMenuSource"] = value;
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

        //********************************************************************
        //  BEGIN - JMTJR- 10/08/2018 - Declaration of static variables 
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ep_user_id"] == null || Session["ep_user_id"].ToString().Trim() == "")
                {
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    Session["sortdirection"] = SortDirection.Ascending.ToString();
                }
            }
            Page.LoadComplete += new EventHandler(Page_LoadComplete);
        }

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
        }
        //**************************************************************************
        //  BEGIN - JMTJR- 09/28/2018 - Get Grid current sort order 
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
        //  BEGIN - JMTJR- 09/28/2018 - Define Property for Sort Direction  
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
        [WebMethod]
        public static string NotificationList(string par_year, string par_month)
        {
            string json = "{}";
            if (Boolean.Parse(HttpContext.Current.Session["flag_notif"].ToString()) == true && HttpContext.Current.Session["ep_user_id"].ToString() == "U8314")
            {
                CommonDB MyCmn = new CommonDB();
                DataTable dt = new DataTable();
                dt = MyCmn.RetrieveData("sp_payroll_notification", "par_year", par_year, "par_month", par_month);
                var flag_notif = HttpContext.Current.Session["flag_notif"].ToString();
                json = JsonConvert.SerializeObject(new { dt, flag_notif }, Newtonsoft.Json.Formatting.Indented);
            }
            return json;
        }
        [WebMethod]
        public static string CloseOpenNotif(string action)
        {
            HttpContext.Current.Session["flag_notif"] = false;
            if (action == "OPEN")
            {
                HttpContext.Current.Session["flag_notif"] = true;
            }
            return HttpContext.Current.Session["flag_notif"].ToString();
        }

    }
}