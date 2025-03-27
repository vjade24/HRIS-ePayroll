using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRIS_Common;

namespace HRIS_ePayroll
{
    public partial class login : System.Web.UI.Page
    {
        DataTable dataLogin
        {
            get
            {
                if ((DataTable)ViewState["dataLogin"] == null) return null;
                return (DataTable)ViewState["dataLogin"];
            }
            set
            {
                ViewState["dataLogin"] = value;
            }
        }

        CommonDB MyCmn = new CommonDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            appName.InnerText = appName.InnerText.Trim() + " (" + MyCmn.ConnectDB().DataSource.ToString().Split('\\')[MyCmn.ConnectDB().DataSource.ToString().Split('\\').Length -1] + ")";
            if (appName.InnerText.ToString().Trim() == "(HRIS_PRD)")
            {
              //  welcome_message.Visible = false;
                appName.InnerText = "";
            }
            if (!IsPostBack)
            {
                Session.RemoveAll();

            }
        }

        protected void txtb_username_TextChanged(object sender, EventArgs e)
        {
            if (txtb_username.Text.Trim() == "")
            {
                msg_logre.ForeColor = System.Drawing.Color.Red;
                msg_logre.Text = "User Name required.";
            }
            else
            {
                msg_logre.Text = "";
                msg_logre.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void txtb_password_TextChanged(object sender, EventArgs e)
        {

            if (txtb_password.Text.Trim() == "")
            {
                msg_logre.ForeColor = System.Drawing.Color.Red;
                msg_logre.Text = "Password required.";
            }
            else
            {
                msg_logre.Text = "";
                msg_logre.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btn_login_Command(object sender, CommandEventArgs e)
        {
            if (txtb_password.Text.Trim() == "" || txtb_username.Text.Trim() == "")
            {
                msg_logre.ForeColor = System.Drawing.Color.Red;
                msg_logre.Text = "User Name and/or Password required.";
            }
            else
            {
                Session.RemoveAll();
                dataLogin = MyCmn.RetrieveData("sp_user_login_PAY", "par_user_id", txtb_username.Text.Trim(), "par_user_password", MyCmn.EncryptString(txtb_password.Text.Trim(), MyCmn.CONST_WORDENCRYPTOR));
                if (dataLogin.Rows.Count == 1)
                {
                    if (dataLogin.Rows[0]["change_password"].ToString() == "True")
                    {
                        msg_logre.ForeColor = System.Drawing.Color.Red;
                        msg_logre.Text = "<strong>FIRST LOGIN!</strong><br/>Login first to self service and changed your password.";
                    }
                    // VJA : 2020-02-22 : 
                        // Y - Authorized
                        // N - Not Authorized for this Application  
                        // X - Incorrect Password     
                        // I - In-Active User    
                        // 0 - Invalid Log-in, User Id Not Existing   
                        // No Return - Invalid User

                    else if (dataLogin.Rows[0]["log_in_flag"].ToString() == "N"  ||
                             dataLogin.Rows[0]["log_in_flag"].ToString() == "X"  ||
                             dataLogin.Rows[0]["log_in_flag"].ToString() == "I"  ||
                             dataLogin.Rows[0]["log_in_flag"].ToString() == "0"  )
                    {
                        msg_logre.Text = dataLogin.Rows[0]["log_in_flag_descr"].ToString();
                        return;
                    }
                    else if (dataLogin.Rows[0]["log_in_flag"].ToString() == "Y")
                    {
                           Session["ep_user_id"]           = dataLogin.Rows[0]["user_id"].ToString();
                           Session["ep_user_profile"]      = dataLogin.Rows[0]["empl_photo"].ToString().Trim();
                           Session["ep_empl_id"]           = dataLogin.Rows[0]["empl_id"].ToString().Trim();
                           Session["ep_first_name"]        = dataLogin.Rows[0]["first_name"].ToString().Trim();
                           Session["ep_last_name"]         = dataLogin.Rows[0]["last_name"].ToString().Trim();
                           Session["ep_middle_name"]       = dataLogin.Rows[0]["middle_name"].ToString().Trim();
                           Session["ep_suffix_name"]       = dataLogin.Rows[0]["suffix_name"].ToString().Trim();
                           Session["ep_photo"]             = dataLogin.Rows[0]["empl_photo"].ToString().Trim();
                           Session["ep_owner_fullname"]    = dataLogin.Rows[0]["employee_name"].ToString().Trim();
                           Session["ep_department_code"]   = dataLogin.Rows[0]["department_code"].ToString().Trim();
                           Session["ep_budget_code"]       = dataLogin.Rows[0]["budget_code"].ToString().Trim();
                           Session["ep_post_authority"]    = "0";

                            // 0 - HR Authority
                            // 1 - Posting (For Accounting)
                            // 2 - Receiving (For Accounting)
                            // 3 - Audit (For Accounting)
                            Session["flag_notif"]       = true;
                            //Session["session_timeout_expired"] = 1; // 1 Minute
                            Response.Redirect("~/");
                    }
                }
                else
                {
                    msg_logre.ForeColor = System.Drawing.Color.Red;
                    msg_logre.Text = "Invalid User !";
                }

            }
        }
    }
}