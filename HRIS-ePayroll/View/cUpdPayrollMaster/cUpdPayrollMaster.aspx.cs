//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Update Payroll Employee Master
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
    public partial class cUpdPayrollMaster : System.Web.UI.Page
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
                    Session["SortField"] = "department_code";
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

                ddl_month.SelectedValue = DateTime.Now.Month.ToString().Trim().PadLeft(2,'0');
                ddl_empl_type.SelectedValue = "RE";
                
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
            RetrieveBindingDepartments();
            RetrieveYear();
            RetriveEmploymentType();
            RetrieveDataListGrid();
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

        private void RetriveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");

            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataBind();
            //ListItem li = new ListItem("-- Select Here --", "");
            //ddl_empl_type.Items.Insert(0, li);
        }


        //*************************************************************************
        //  BEGIN - VJA- 04/16/2020 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            ddl_year.SelectedValue                      = DateTime.Now.Year.ToString().Trim();
            ddl_month.SelectedIndex                     = -1;
            ddl_empl_type.SelectedValue                 = "RE";
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

            if (ddl_month.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_month");
                ddl_month.Focus();
                validatedSaved = false;
            }

            if (ddl_empl_type.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_empl_type");
                ddl_empl_type.Focus();
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
                    case "ddl_month":
                        {
                            ddl_month.BorderColor               = Color.Red;
                            LblRequired2.Text                   = MyCmn.CONST_RQDFLD;
                            break;
                        }
                    case "ddl_empl_type":
                        {
                            ddl_empl_type.BorderColor           = Color.Red;
                            LblRequired3.Text                   = MyCmn.CONST_RQDFLD;
                            break;
                        }
                    default:
                        {
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
                            LblRequired2.Text                   = "";
                            LblRequired3.Text                   = "";

                            ddl_empl_type.BorderColor           = Color.LightGray;
                            ddl_month.BorderColor               = Color.LightGray;
                            ddl_year.BorderColor                = Color.LightGray;
                            break;
                        }
                }
            }
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            UpdatePanel10.Update();
        }

        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel10.Update();
        }

        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
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

                switch (ddl_empl_type.SelectedValue.ToString().Trim())
                {
                    case "RE":
                        dt_result = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_insert_RE", "p_year", ddl_year.SelectedValue.ToString().Trim(), "p_month", ddl_month.SelectedValue.ToString().Trim(), "p_user_id", Session["ep_user_id"].ToString().Trim());
                        break;
                    case "CE":
                    case "JO":
                        {
                            dt_result = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_insert_CE_JO", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_year", ddl_year.SelectedValue.ToString().Trim(), "p_month", ddl_month.SelectedValue.ToString().Trim(), "p_user_id", Session["ep_user_id"].ToString().Trim());
                            break;
                        }
                }

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


        protected void btn_generate_report_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                string printreport;
                string procedure;
                string url = "";

                Session["history_page"] = Request.Url.AbsolutePath;
                switch (ddl_empl_type.SelectedValue.ToString().Trim())
                {
                    case "RE":
                        printreport = "/cryMasterInserted/cryMasterInserted_RE.rpt";
                        procedure = "sp_payrollemployeemaster_tbl_insert_RE_list";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",p_year," + ddl_year.SelectedValue.ToString().Trim() + ",p_month," + ddl_month.SelectedValue.ToString().Trim();
                        break;
                    case "CE":
                    case "JO":
                        printreport = "/cryMasterInserted/cryMasterInserted_CEJO.rpt";
                        procedure = "sp_payrollemployeemaster_tbl_insert_CE_JO_list";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",p_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",p_year," + ddl_year.SelectedValue.ToString().Trim() + ",p_month," + ddl_month.SelectedValue.ToString().Trim();
                        break;
                }

                if (url != "")
                {
                    Response.Redirect(url);
                }
            }
        }


        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            if (ddl_empl_type.SelectedValue.ToString() == "CE" || ddl_empl_type.SelectedValue.ToString() == "JO")
            {
                dataListGrid = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_insert_CE_JO_list1", "p_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "p_year", ddl_year.SelectedValue.ToString().Trim(), "p_month", ddl_month.SelectedValue.ToString().Trim(), "p_department_code", ddl_department.SelectedValue.ToString().Trim());
            }
            else 
            {
                dataListGrid = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_insert_RE_list1","p_year", ddl_year.SelectedValue.ToString().Trim(), "p_month", ddl_month.SelectedValue.ToString().Trim(), "p_department_code", ddl_department.SelectedValue.ToString().Trim());
            }
            
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }

        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Change Field Sort mode  
        //**************************************************************************
        protected void gv_dataListGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            sortingDirection = GetCurrentSortDir();

            if (sortingDirection == MyCmn.CONST_SORTASC)
            {
                SortDirectionVal = SortDirection.Descending;
                sortingDirection = MyCmn.CONST_SORTDESC;
            }
            else
            {
                SortDirectionVal = SortDirection.Ascending;
                sortingDirection = MyCmn.CONST_SORTASC;
            }
            Session["SortField"] = e.SortExpression;
            Session["SortOrder"] = sortingDirection;

            MyCmn.Sort(gv_dataListGrid, dataListGrid, e.SortExpression, sortingDirection);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            //SearchData(txtb_search.Text.ToString().Trim());
        }

        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Get Grid current sort order 
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
        //  BEGIN - VJA- 01/17/2019 - Define Property for Sort Direction  
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
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            //SearchData(txtb_search.Text.ToString().Trim());
        }

        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Department
        //*************************************************************************
        private void RetrieveBindingDepartments()
        {
            ddl_department.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_departments_tbl_list", "par_include_history", "N");

            ddl_department.DataSource = dt;
            ddl_department.DataValueField = "department_code";
            ddl_department.DataTextField = "department_name1";
            ddl_department.DataBind();
            ListItem li = new ListItem("-- Select All --", "");
            ddl_department.Items.Insert(0, li);
            
        }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}