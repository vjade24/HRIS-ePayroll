using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using HRIS_Common;

namespace HRIS_ePayroll
{
    /// <summary>
    /// Summary description for MasterPage_Handler
    /// </summary>
    public class MasterPage_Handler : IHttpHandler
    {
        CommonDB MyCmn = new CommonDB();
        public void ProcessRequest(HttpContext context)
        {
            string result       = "";
            string result_msg   = "";
            string result_icon  = "";

            HttpFileCollection files   = context.Request.Files;
            string par_user_id  = context.Request["par_user_id"];
            string p_url_name   = context.Request["p_url_name"];
            string p_mode       = context.Request["p_mode"];

            // MESSAGE DESCRIPTION
            // A - Add Menu to Favorites  
            // Y - Menu Link Succesfully Added to Favorites
            // D - Menu Link Succesfully Remove from Favorites
            // X - Menu Link Not Added/Removed in Favorites
            // N - Favorites Not Inserted, SP fa-5x fa fa-exclamation-triangle text-danger

            DataTable dt = MyCmn.RetrieveData("sp_add_remove_menu_favorites", "p_user_id", par_user_id, "p_url_name", p_url_name, "p_mode", p_mode);
            try
            {
                if (dt.Rows.Count > 0 || dt.Rows != null || dt != null)
            {
                if (dt.Rows[0]["result_flag"].ToString() == "A")
                {
                    result_msg = dt.Rows[0]["result_flag_descr"].ToString();
                    result_icon = "fa-5x fa fa-check-circle text-success";
                    result = dt.Rows[0]["result_flag"].ToString();
                }
                else if (dt.Rows[0]["result_flag"].ToString() == "Y")
                {
                    result_msg = dt.Rows[0]["result_flag_descr"].ToString();
                    result_icon = "fa-5x fa fa-check-circle text-success";
                    result = dt.Rows[0]["result_flag"].ToString();
                }
                else if (dt.Rows[0]["result_flag"].ToString() == "D")
                {
                    result_msg = dt.Rows[0]["result_flag_descr"].ToString();
                    result_icon = "fa-5x fa fa-check-circle text-success";
                    result = dt.Rows[0]["result_flag"].ToString();
                }
                else if (dt.Rows[0]["result_flag"].ToString() == "X")
                {
                    result_msg = dt.Rows[0]["result_flag_descr"].ToString();
                    result_icon = "fa-5x fa fa-exclamation-triangle text-danger";
                    result = dt.Rows[0]["result_flag"].ToString();
                }
                else if (dt.Rows[0]["result_flag"].ToString() == "0")
                {
                    result_msg  = dt.Rows[0]["result_flag_descr"].ToString();
                    result_icon = "fa-5x fa fa-exclamation-triangle text-danger";
                    result      = dt.Rows[0]["result_flag"].ToString();
                }
            }
            else
            {
                result_msg  =  "ERRO BACKEND, Null when Calling the Stored Procedure";
                result_icon =  "fa-5x fa fa-exclamation-triangle text-danger";
                result      =  "ERRO BACKEND";
            }
            }catch(Exception e)
            {
                result_msg  =  e.Message;
                result_icon =  "fa-5x fa fa-exclamation-triangle text-danger";
                result      =  "ERRO BACKEND";
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(result + "*" + result_msg + "*" + result_icon);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}