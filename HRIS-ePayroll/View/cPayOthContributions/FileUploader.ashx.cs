using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using HRIS_Common;

namespace HRIS_ePayroll.View.cPayOthContributions
{
    /// <summary>
    /// Summary description for FileUploader
    /// </summary>
    public class FileUploader : IHttpHandler
    {
        CommonDB MyCmn = new CommonDB();
        public void ProcessRequest(HttpContext context)
        {
            string filedata = string.Empty;
            string result = "";
            string result_msg = "Successfully Uploaded";
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                string par_year = context.Request["par_year"];
                string par_month = context.Request["par_month"];
                string par_account = context.Request["par_account"];
                string par_delete_existing = context.Request["par_delete_existing"];
                string par_user_id = context.Request["par_user_id"];
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    if (Path.GetExtension(file.FileName).ToLower() != ".csv")
                    {
                        result_msg = "Only .csv file type is allowed";
                        result = "N";
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(result + "*" + result_msg);
                        return;
                    }
                    decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);
                    if (size > 2048)
                    {
                       
                        result_msg = "File size should not exceed 2 MB.!";
                        result = "N";
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(result + "*" + result_msg);
                        return;
                    }
                    string fname;
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] {
                        '\\'
                    });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = Path.GetExtension(file.FileName);
                    }

                    ////here UploadFile is define my folder name, where files will be store.  
                    //string uploaddir = System.Configuration.ConfigurationManager.AppSettings["Upload"];
                    //filedata = "U_"+ par_user_id+"_" +par_year+"_"+par_month+""+fname;
                    filedata = "U_"+ par_user_id+"_"+ par_account + "_"+ fname;
                    fname = Path.Combine(context.Server.MapPath("~/UploadFile/"),filedata);
                    file.SaveAs(fname);
                    //string tr_paramfilename = "D:\\FILE_REPO\\feb_for_upd.csv";
                    DataTable dt = MyCmn.RetrieveData("sp_payrolldeduc_ledger_stg_upload", "p_deduc_code", par_account , "p_payroll_year", par_year, "p_payroll_month", par_month, "p_uploaded_by_user", par_user_id, "p_deleteexisting", par_delete_existing, "par_filename", fname);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["run_status"].ToString();
                        result_msg = dt.Rows[0]["run_message"].ToString();
                    }
                    else
                    {
                        result = "N";
                        result_msg = "ERROR ON UPLOADING FILE";
                    }
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(result+"*" + result_msg);
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