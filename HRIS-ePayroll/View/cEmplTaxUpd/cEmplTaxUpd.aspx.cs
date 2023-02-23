//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Update JO Tax
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. ALivio     04/16/2020      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View
{
    public partial class cEmplTaxUpd : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 04/16/2020 - Data Place holder creation 
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
        //  BEGIN - VJA- 04/16/2020 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 04/16/2020 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "";
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
                    

                }
            }


        }

        //********************************************************************
        //  BEGIN - VJA- 04/16/2020 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();

            RetrieveYear();
            ClearEntry();

        }

        //*************************************************************************
        //  BEGIN - VJA- 04/16/2020 - Populate Combo list for Payroll Year
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
        //*************************************************************************
        //  BEGIN - VJA- 04/16/2020 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            ddl_year.SelectedValue                      = DateTime.Now.Year.ToString().Trim();
        }
       
        //**************************************************************************
        //  BEGIN - VJA- 04/16/2020 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false,"ALL");
            if (ddl_year.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_year");
                ddl_year.Focus();
                validatedSaved = false;
            }
            
            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - VJA- 04/16/2020 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
            { 
                switch (pObjectName)
                {
                    case "ddl_year":
                        {
                            ddl_year.BorderColor                = Color.Red;
                            LblRequired1.Text                   = MyCmn.CONST_RQDFLD;
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
                            LblRequired1.Text                   = "";
                            ddl_year.BorderColor                = Color.LightGray;
                            break;
                        }
                }
            }
        }
        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel10.Update();
        }
        
        protected void btn_create_generate_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                /*
                 **********************************************************************************************
                 *| Return Values:                                                                            *
                 *|********************************************************************************************
                 *| I - Employee(s) Successfully Inserted/Updated                                             *
                 *| N - Successfully Run but No Employee was Inserted/Updated                                 *
                 *| S - Error Coming from Stored Procedure Excecuted                                          *
                 *| E - Error Coming from Stored Procedure Excecuted                                          *
                 **********************************************************************************************
                */
                
                dt_result = MyCmn.RetrieveData("sp_generate_payrollemployee_tax_hdr_dtl", "p_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "p_empl_id", "", "p_user_id", Session["ep_user_id"].ToString().Trim());
                     
                if (dt_result != null && dt_result.Rows.Count > 0)
                {
                    switch (dt_result.Rows[0]["result_flag"].ToString())
                    {
                        case "I": // Employee(s) Successfully Inserted/UpdatedUpdated
                        case "N": // Successfully Run but No Employee was Inserted/Updated
                        case "S": // Successfully Run but No Employee was Inserted/Updated
                            {
                                i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                                h2_status.InnerText = "SUCCESSFULLY";
                                lbl_generation_msg.Text = dt_result.Rows[0]["result_flag_message"].ToString();
                                break;
                            }
                        
                        case "E": // Error Coming from Stored Procedure Excecuted (TRY CATCH)
                            {
                                i_icon_display.Attributes.Add("class", "fa-5x fa fa-times-circle text-danger");
                                h2_status.InnerText     = "NOT GENERATED";
                                lbl_generation_msg.Text = dt_result.Rows[0]["result_flag_message"].ToString();
                                break;
                            }
                    }
                }
                else
                {
                    i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                    h2_status.InnerText     = "NOT GENERATED";
                    lbl_generation_msg.Text = "Stored Procedure Error !";
                }
            }
        }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}