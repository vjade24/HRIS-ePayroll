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
        
    }
}