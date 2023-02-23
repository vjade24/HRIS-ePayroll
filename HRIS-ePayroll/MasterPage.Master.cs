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
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        CommonDB MyCmn = new CommonDB();
        //********************************************************************
        //  BEGIN - JMTJR- 09/03/2018 - Data Place holder creation 
        //********************************************************************
        public string menu_active_level;
        public string current_ruet;
        public string active_menu_id;
        public string active_parent_id;
        public string page_title;
        public string budget_code;

        public string allow_add;
        public string allow_edit;
        public string allow_delete;
        public string allow_print;
        public string allow_view;
        public string allow_edit_history;
        public string third_url;
        public string user_defaults;

        public int session_timeout_expired;


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
        DataTable userdefaults
        {
            get
            {
                if ((DataTable)ViewState["userdefaults"] == null) return null;
                return (DataTable)ViewState["userdefaults"];
            }
            set
            {
                ViewState["userdefaults"] = value;
            }
        }
        DataTable dtNotificationSource
        {
            get
            {
                if ((DataTable)ViewState["dtNotificationSource"] == null) return null;
                return (DataTable)ViewState["dtNotificationSource"];
            }
            set
            {
                ViewState["dtNotificationSource"] = value;
            }
        }
        DataTable dtFavorites
        {
            get
            {
                if ((DataTable)ViewState["dtFavorites"] == null) return null;
                return (DataTable)ViewState["dtFavorites"];
            }
            set
            {
                ViewState["dtFavorites"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - JMTJR- 09/03/2018 - Menu List Variable Initialization 
        //********************************************************************
        public class page_menus
        {
            public int id;
            public string menu_name;
            public int menu_id_link;
            public string url_name;
            public string page_title;
            public string menu_icon;
            public int menu_level;
        }
        public List<page_menus> menus = new List<page_menus>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //appName.Text = appName.Text.Trim() + " (" + MyCmn.ConnectDB().Database.Remove(0, 5) + ")";
            appName.Text = appName.Text.Trim() + " (" + MyCmn.ConnectDB().DataSource.ToString().Split('\\')[MyCmn.ConnectDB().DataSource.ToString().Split('\\').Length - 1] + ")";

            if (appName.Text.ToString().Trim() == "(HRIS_PRD)")
            {
                appName.Text = "";
            }

            if (!IsPostBack)
                if (!IsPostBack)
            {
                if (Session["ep_user_id"] == null)
                {
                    Response.Redirect("~/logoff.aspx");
                }
                initialize();
            }

            Page.Unload += Page_Unload;
            Page.LoadComplete += Page_LoadComplete;
        }

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            try
            {

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.Expires = -1000;
                Response.CacheControl = "no-cache";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            // VJA : 2020-04-24 - Time Out Expired Session
            //Session.Timeout = Convert.ToInt32(Session["session_timeout_expired"].ToString());
            //session_timeout_expired = Convert.ToInt32(Session["session_timeout_expired"].ToString());
            // VJA : 2020-04-24 - Time Out Expired Session
        }

        private void Page_Unload(object sender, EventArgs e)
        {

        }

        protected void initialize()
        {
            if (Request.Url.AbsolutePath.Split('/')[Request.Url.AbsolutePath.Split('/').Length - 1] != "printView.aspx")
            {
                Session["history_page"] = "";
            }
            //dtMenuSource = CommonDB.RetrieveData("sp_menus_tbl_list", "module_id", 9);
            dtMenuSource = MyCmn.RetrieveData("sp_user_menu_access_role_list_PAY", "par_user_id", Session["ep_user_id"].ToString().Trim());
            // VJA : 2020-02-22 - From sp_user_menu_access_role_list TO sp_user_menu_access_role_list_PAY
            menus.Clear();
            current_ruet = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            string[] extentions = Request.QueryString.AllKeys;
            string url_params = "";

            if (extentions.Length > 0)
            {
                foreach (string keys in extentions)
                {
                    url_params = url_params + "&" + keys + "=" + Request.QueryString[keys].ToString();
                }
            }
            url_params = url_params.Trim('&');
            url_params = url_params.Trim() != "" ? "?" + url_params : "";
            if (url_params != "" && url_params != null)
            {
                current_ruet = current_ruet + "" + url_params;
            }
            DataRow[] MenuRows = dtMenuSource.Select();
            foreach (DataRow row in MenuRows)
            {
                if (row["url_name"].ToString() == current_ruet || row["url_name"].ToString() + url_params == current_ruet)
                {
                    page_title = row["page_title"].ToString();
                    active_menu_id = row["id"].ToString();
                    active_parent_id = row["menu_id_link"].ToString();
                    menu_active_level = row["menu_level"].ToString();
                    allow_add = row["allow_add"].ToString();
                    allow_edit = row["allow_edit"].ToString();
                    allow_delete = row["allow_delete"].ToString();
                    allow_print = row["allow_print"].ToString();
                    allow_view = row["allow_view"].ToString();
                    allow_edit_history = row["allow_edit_history"].ToString();
                }

                page_menus getMenusFromDB = new page_menus();
                getMenusFromDB.id = Convert.ToInt32(row["id"]);
                getMenusFromDB.menu_name = row["menu_name"].ToString();
                getMenusFromDB.menu_icon = WebUtility.HtmlDecode(row["menu_icon"].ToString());
                getMenusFromDB.url_name = row["url_name"].ToString();
                getMenusFromDB.page_title = row["page_title"].ToString();
                getMenusFromDB.menu_id_link = Convert.ToInt32(row["menu_id_link"]);
                getMenusFromDB.menu_level = Convert.ToInt32(row["menu_level"]);
                menus.Add(getMenusFromDB);

            }
            initialize_notification();
        }

        //********************************************************************
        //  BEGIN - VJA- 04/21/2020 - Public List
        //********************************************************************
        public List<notification_lst> notif = new List<notification_lst>();
        //********************************************************************
        //  BEGIN - VJA- 04/21/2020 - Notification List Variable Initialization 
        //********************************************************************
        public class notification_lst
        {
            public string row_nbr;
            public string notify_cntr;
            public string notify_code;
            public string notify_short_msg;
            public string notify_long_msg;
            public string notify_url_name;
            public string user_id;
        }
        //********************************************************************
        //  BEGIN - VJA- 04/21/2020 - Initialization for Notification
        //********************************************************************
        protected void initialize_notification()
        {
            dtNotificationSource = MyCmn.RetrieveData("sp_notification_trn_tbl_list_PAY","p_user_id", Session["ep_user_id"].ToString().Trim(), "p_date", DateTime.Now.ToString("yyyy-MM-dd"));
            notif.Clear();

            if (dtNotificationSource != null)
            {
                DataRow[] NotifRows = dtNotificationSource.Select();
                foreach (DataRow row in NotifRows)
                {
                    notification_lst getNotifFromDB = new notification_lst();

                    getNotifFromDB.row_nbr = row["row_nbr"].ToString();
                    getNotifFromDB.notify_cntr = row["notify_cntr"].ToString();
                    getNotifFromDB.notify_code = row["notify_code"].ToString();
                    getNotifFromDB.notify_short_msg = row["notify_short_msg"].ToString();
                    getNotifFromDB.notify_long_msg = row["notify_long_msg"].ToString();
                    getNotifFromDB.notify_url_name = row["notify_url_name"].ToString();
                    getNotifFromDB.user_id = row["user_id"].ToString();
                    notif.Add(getNotifFromDB);
                }
            }
            
        }
        //********************************************************************
        //  BEGIN - VJA- 04/25/2020 - For Favorites Add or Remove
        //********************************************************************
        //protected void AddRemoveFavorites(string url_name, string addorremove)
        //{
        //    // MESSAGE DESCRIPTION
        //    // A - Add Menu to Favorites  
        //    // Y - Menu Link Succesfully Added to Favorites
        //    // D - Menu Link Succesfully Remove from Favorites
        //    // X - Menu Link Not Added/Removed in Favorites
        //    // N - Favorites Not Inserted, SP Error

        //    string result_flag_descr = "";
        //    string result_icon       = "";
        //    dtFavorites = MyCmn.RetrieveData("sp_add_remove_menu_favorites", "p_user_id", Session["ep_user_id"].ToString(), "p_url_name", url_name, "p_mode",addorremove);
            
        //    if (dtFavorites.Rows[0]["result_flag"].ToString() == "A")
        //    {
        //        result_flag_descr = dtFavorites.Rows[0]["result_flag_descr"].ToString();
        //        result_icon       = "success";
        //    }
        //    else if (dtFavorites.Rows[0]["result_flag"].ToString() == "Y")
        //    {
        //        result_flag_descr = dtFavorites.Rows[0]["result_flag_descr"].ToString();
        //        result_icon      = "success";
        //    }
        //    else if (dtFavorites.Rows[0]["result_flag"].ToString() == "D")
        //    {
        //        result_flag_descr = dtFavorites.Rows[0]["result_flag_descr"].ToString();
        //        result_icon     = "success";
        //    }
        //    else if (dtFavorites.Rows[0]["result_flag"].ToString() == "X")
        //    {
        //        result_flag_descr = dtFavorites.Rows[0]["result_flag_descr"].ToString();
        //        result_icon = "error";
        //    }
        //    else if (dtFavorites.Rows[0]["result_flag"].ToString() == "0")
        //    {
        //        result_flag_descr = dtFavorites.Rows[0]["result_flag_descr"].ToString();
        //        result_icon = "error";
        //    }

        //}
    }
}