using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRIS_Common;
using System.Net;
using System.Web.Services;
using System.Web.Security;

namespace HRIS_ePayroll
{
    public partial class logoff : System.Web.UI.Page
    {
        public string login_q = "Y";
        protected void Page_Load(object sender, EventArgs e)
        {
           


        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            try
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.Expires = -1000;
                Response.CacheControl = "no-cache";
                //Response.Redirect("login.aspx", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "reloader();", true);
            //    if (Session["ss_user_id"] != null)
            //    {
            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        Response.Cache.SetExpires(DateTime.Now);
            //        Response.Cache.SetAllowResponseInBrowserHistory(false);
            //        CommonDB.GLOBAL_LOGIN_Q = 0;
            //        Response.Cache.SetNoStore();
            //        Session["ss_user_id"] = null;
            //        Session.Clear();
            //        Session.Abandon();
            //        Session.RemoveAll();
            //        login_q = null;
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "reloader();", true);

            //    }
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Now);
            //Response.Cache.SetAllowResponseInBrowserHistory(false);
            //CommonDB.GLOBAL_LOGIN_Q = 0;
            //Response.Cache.SetNoStore();
            //Session["ss_user_id"] = null;


            //CommonDB.GLOBAL_LOGIN_Q = 0;
            //Response.Cache.SetNoStore();
            //Session.Clear();
            //Session.Abandon();
            //Session.RemoveAll();

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Buffer = true;
            //Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            //Response.Expires = -1000;
            //Response.CacheControl = "no-cache";
        }
    }
}