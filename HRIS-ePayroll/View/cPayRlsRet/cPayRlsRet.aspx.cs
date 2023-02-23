//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Payroll Release, Return and Void Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     11/12/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View.cPayRlsRet
{
    public partial class cPayRlsRet : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Data Place holder creation 
        //********************************************************************

        DataTable dtSource
        {
            get
            {
                if ((DataTable)ViewState["dtSource"] == null) return null;
                return (DataTable)ViewState["dtSource"];
            }
            set
            {
                ViewState["dtSource"] = value;
            }
        }
        DataTable dtSource_hdr
        {
            get
            {
                if ((DataTable)ViewState["dtSource_hdr"] == null) return null;
                return (DataTable)ViewState["dtSource_hdr"];
            }
            set
            {
                ViewState["dtSource_hdr"] = value;
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

        DataTable dtSource_for_names
        {
            get
            {
                if ((DataTable)ViewState["dtSource_for_names"] == null) return null;
                return (DataTable)ViewState["dtSource_for_names"];
            }
            set
            {
                ViewState["dtSource_for_names"] = value;
            }
        }

        DataTable dtSource_defult_setup
        {
            get
            {
                if ((DataTable)ViewState["dtSource_defult_setup"] == null) return null;
                return (DataTable)ViewState["dtSource_defult_setup"];
            }
            set
            {
                ViewState["dtSource_defult_setup"] = value;
            }
        }

        DataTable dtSourse_for_template // For Report File
        {
            get
            {
                if ((DataTable)ViewState["dtSourse_for_template"] == null) return null;
                return (DataTable)ViewState["dtSourse_for_template"];
            }
            set
            {
                ViewState["dtSourse_for_template"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "payroll_registry_descr";
                    if (Session["SortOrder"] == null)
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

                    ViewState["page_allow_receive"] = 0;
                    ViewState["page_allow_audit"] = 0;
                    ViewState["page_allow_post"] = 0;
                }
                else
                {
                    ViewState["page_allow_add"] = Master.allow_add;
                    ViewState["page_allow_delete"] = Master.allow_delete;
                    ViewState["page_allow_edit"] = Master.allow_edit;
                    ViewState["page_allow_edit_history"] = Master.allow_edit_history;
                    ViewState["page_allow_print"] = Master.allow_print;

                    Session["page_allow_print_from_registry"] = Master.allow_print;
                    Session["page_allow_edit_history_from_registry"] = Master.allow_edit_history;
                    Session["page_allow_edit_from_registry"] = Master.allow_edit;
                    Session["page_allow_add_from_registry"] = Master.allow_add;
                    Session["page_allow_delete_from_registry"] = Master.allow_delete;

                    ViewState["page_allow_receive"] = 1;
                    ViewState["page_allow_audit"] = 1;
                    ViewState["page_allow_post"] = 1;

                }
                if (Session["PreviousValuesonPage_cPayRlsRet"] == null)
                    Session["PreviousValuesonPage_cPayRlsRet"] = "";
                else if (Session["PreviousValuesonPage_cPayRlsRet"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRlsRet"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    //RetrieveEmploymentType();
                    ddl_year.SelectedValue              = prevValues[0].ToString();
                    ddl_month.SelectedValue             = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue         = prevValues[2].ToString();
                    txtb_registry_no.Text               = prevValues[9].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue  = prevValues[3].ToString();
                    RetriveGroupings();
                    RetrieveDataListGrid();
                    //RetrieveEmployeename();
                    //RetrieveLoanPremiums_Visible();
                    //RetrievePayrollInstallation();
                }
                txtb_search_scan.Focus();
            }

        }

        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();

            RetrieveYear();
            RetrieveDataListGrid();

            RetriveGroupings();
            RetriveTemplate();
            RetriveEmploymentType();
            RetrieveBindingDep();

           
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
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
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveGroupings()
        {
            /*    01  -  Common Groupings
                  02  -  Communication Expense
                  03  -  RATA and Quarterly Allowance
                  04  -  Monetization
                  05  -  Hazard, Subsistence and Laundry Pay
                  06  -  Overtime Pay
                  07  -  Loyalty Bonus
            */
            string special_group = "";
            ddl_payroll_group.Items.Clear();
            if (ddl_payroll_template.SelectedValue == "043" || ddl_payroll_template.SelectedValue == "024")
            {
                special_group = "02";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "023" || ddl_payroll_template.SelectedValue.ToString().Trim() == "043")
            {
                special_group = "03";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "025" || ddl_payroll_template.SelectedValue == "044")
            {
                special_group = "04";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "021" || ddl_payroll_template.SelectedValue == "041")
            {
                special_group = "05";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "022" ||
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "042" ||
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "061")
            {
                special_group = "06";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() == "029" ||
                     ddl_payroll_template.SelectedValue.ToString().Trim() == "048")
            {
                special_group = "07";
            }
            else if (ddl_payroll_template.SelectedValue.ToString().Trim() != "")
            {
                special_group = "01";
            }

            DataTable dt = MyCmn.RetrieveData("sp_payrollemployeegroupings_hdr_tbl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_special_group", special_group);
            ddl_payroll_group.DataSource = dt;
            ddl_payroll_group.DataValueField = "payroll_group_nbr";
            ddl_payroll_group.DataTextField = "grouping_descr";
            ddl_payroll_group.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_group.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveTemplate()
        {
            //ddl_payroll_template.Items.Clear();  ytrerhycc
            //DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list6", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            //ddl_payroll_template.DataSource = dt;
            //ddl_payroll_template.DataValueField = "payrolltemplate_code";
            //ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            //ddl_payroll_template.DataBind();
            //ListItem li = new ListItem("-- Select Here --", "");
            //ddl_payroll_template.Items.Insert(0, li);

            DataTable dt = new DataTable();
            if (ddl_payrolltype.SelectedValue == "01")
            {
                ddl_payroll_template.Items.Clear();
                dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list6", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            }
            else if (ddl_payrolltype.SelectedValue == "02")
            {
                ddl_payroll_template.Items.Clear();
                dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list5", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
            }
            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetriveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");

            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_type.Items.Insert(0, li);
        }

        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve Related Tempate COde/Type
        //*************************************************************************
        private void RetrieveRelatedTemplate()
        {
            ddl_select_report.Items.Clear();
            dtSourse_for_template = MyCmn.RetrieveData("sp_payrollregistry_template_combolist", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            ddl_select_report.DataSource = dtSourse_for_template;
            ddl_select_report.DataValueField = "payrolltemplate_code";
            ddl_select_report.DataTextField = "payrolltemplate_descr";
            ddl_select_report.DataBind();
            ListItem li = new ListItem("Generate Report to Printer", "");
            ddl_select_report.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_hdr_rls_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Get The Default from SP sp_payrollregistry_get_dates
        //*************************************************************************
        protected void get_default()
        {
            dtSource_defult_setup = MyCmn.RetrieveData("sp_payrollregistry_get_dates", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_year", ddl_year.SelectedValue.ToString(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim());
            if (dtSource_defult_setup != null && dtSource_defult_setup.Rows.Count > 0)
            {
                txtb_period_from.Text = dtSource_defult_setup.Rows[0]["payroll_period_from"].ToString();
                txtb_period_to.Text = dtSource_defult_setup.Rows[0]["payroll_period_to"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                UpdateDateFrom.Update();
                UpdateDateTo.Update();

                /*>>>>>>>>> IF TEMPLATE BELONGS HERE <<<<<<<<<*/
                if (ddl_payroll_template.SelectedValue.ToString().Trim() == "008" ||
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "009" ||
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "010" ||
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "023" ||  // For Separate Registry and Separate Group
                    ddl_payroll_template.SelectedValue.ToString().Trim() == "011")
                {
                    txtb_no_works.Text = dtSource_defult_setup.Rows[0]["no_of_workdays"].ToString();
                    div_no_works_days.Visible = true;
                }
                else
                {
                    txtb_no_works.Text = "0";
                    div_no_works_days.Visible = false;
                }
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_registry_no.Text = "";
            txtb_period_from.Text = "";
            txtb_period_to.Text = "";
            txtb_registry_descr.Text = "";

            ddl_payroll_group.SelectedIndex = -1;
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource.Columns.Add("department_code", typeof(System.String));
            dtSource.Columns.Add("remarks", typeof(System.String));
            dtSource.Columns.Add("user_id_released_by", typeof(System.String));
            dtSource.Columns.Add("user_id_returned_by", typeof(System.String));
            dtSource.Columns.Add("user_id_void_by", typeof(System.String));
            dtSource.Columns.Add("released_dttm", typeof(System.String));
            dtSource.Columns.Add("returned_dttm", typeof(System.String));
            dtSource.Columns.Add("void_dttm", typeof(System.String));
            
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollregistry_hdr_rls_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["department_code"] = string.Empty;
            nrow["remarks"] = string.Empty;
            nrow["user_id_released_by"] = string.Empty;
            nrow["user_id_returned_by"] = string.Empty;
            nrow["user_id_void_by"] = string.Empty;
            nrow["released_dttm"] = string.Empty;
            nrow["returned_dttm"] = string.Empty;
            nrow["void_dttm"] = string.Empty;
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
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
        //  BEGIN - VJA- 01/17/2019 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;

            if (IsDataValidated())
            {
                
                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource.Rows[0]["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"] = txtb_registry_no.Text.ToString().Trim();
                    dtSource.Rows[0]["department_code"]      = ddl_department.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["remarks"]              = txtb_remarks.Text.ToString().Trim();
                    dtSource.Rows[0]["user_id_released_by"]  = txtb_released_by.Text.ToString().Trim();
                    dtSource.Rows[0]["user_id_returned_by"]  = txtb_returned_by.Text.ToString().Trim();
                    dtSource.Rows[0]["user_id_void_by"]      = txtb_voided_by.Text.ToString().Trim();
                    dtSource.Rows[0]["released_dttm"]        = txtb_date_released.Text.ToString().Trim();
                    dtSource.Rows[0]["returned_dttm"]        = txtb_date_returned.Text.ToString().Trim();
                    dtSource.Rows[0]["void_dttm"]            = txtb_date_voided.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"] = txtb_registry_no.Text.ToString().Trim();
                    dtSource.Rows[0]["department_code"]      = ddl_department.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["remarks"]              = txtb_remarks.Text.ToString().Trim();
                    dtSource.Rows[0]["user_id_released_by"]  = txtb_released_by.Text.ToString().Trim();
                    dtSource.Rows[0]["user_id_returned_by"]  = txtb_returned_by.Text.ToString().Trim();
                    dtSource.Rows[0]["user_id_void_by"]      = txtb_voided_by.Text.ToString().Trim();
                    dtSource.Rows[0]["released_dttm"]        = txtb_date_released.Text.ToString().Trim();
                    dtSource.Rows[0]["returned_dttm"]        = txtb_date_returned.Text.ToString().Trim();
                    dtSource.Rows[0]["void_dttm"]            = txtb_date_voided.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    
                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;

                    if (msg.Substring(0, 1) == "X")
                    {

                        if (lbl_behaviour_mode_hidden.Text.ToString() == "RLS")
                        {
                            msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                            msg_header.InnerText = "Unable to Release, Data has been released by other User/s";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);

                        }
                        else if (lbl_behaviour_mode_hidden.Text == "RTN")
                        {
                            msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                            msg_header.InnerText = "Unable to Return, Data has been returned by other User/s";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
                        }
                        else if (lbl_behaviour_mode_hidden.Text == "VOI")
                        {
                            msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                            msg_header.InnerText = "Unable to Void, Data has been voided by other User/s";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
                        }

                        return;
                    }

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        string whereclause = "WHERE payroll_registry_nbr = '" + txtb_registry_no.Text.ToString().Trim() + "' AND payroll_year = '" + ddl_year.SelectedValue.ToString().Trim() + "'";
                        MyCmn.UpdateTable("payrollregistry_hdr_tbl", "post_status = 'R',date_posted = '" + txtb_date_released.Text.ToString().Trim() + "'  ", whereclause);
                        //Update_Detail_Tables("R");
                    }
                    else if(saveRecord == MyCmn.CONST_EDIT)
                    {
                        if (lbl_behaviour_mode_hidden.Text == "RTN")
                        {
                            // If the Behavior is RTN : Update the Post Status on Heder will Update into = N 
                            string whereclause = "WHERE payroll_registry_nbr = '" + txtb_registry_no.Text.ToString().Trim() + "' AND payroll_year = '"+ ddl_year.SelectedValue.ToString().Trim() + "'";
                            MyCmn.UpdateTable("payrollregistry_hdr_tbl", "post_status = 'N',date_posted = '" + txtb_date_returned.Text.ToString().Trim() + "' ", whereclause);
                            //Update_Detail_Tables("N");
                        }
                        else if (lbl_behaviour_mode_hidden.Text == "VOI")
                        {
                            // If the Behavior is VOI : Update the Post Status on Heder will Update into = V
                            string whereclause = "WHERE payroll_registry_nbr = '" + txtb_registry_no.Text.ToString().Trim() + "' AND payroll_year = '" + ddl_year.SelectedValue.ToString().Trim() + "'";
                            MyCmn.UpdateTable("payrollregistry_hdr_tbl", "post_status = 'V',date_posted = '"+ txtb_date_voided.Text.ToString().Trim() + "'", whereclause);
                            //Update_Detail_Tables("V");
                        }
                    }
                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        string editExpression = "payroll_registry_nbr = '" + txtb_registry_no.Text.ToString().Trim() + "' AND payroll_year = '" + ddl_year.SelectedValue.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"] = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_month"] = ddl_month.SelectedValue.ToString().Trim();
                        row2Edit[0]["payrolltemplate_code"] = ddl_payroll_template.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_group_nbr"] = ddl_payroll_group.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_period_from"] = txtb_period_from.Text.ToString().Trim();
                        row2Edit[0]["payroll_period_to"] = txtb_period_to.Text.ToString().Trim();
                        row2Edit[0]["payroll_registry_descr"] = txtb_registry_descr.Text.ToString().Trim();
                        
                        row2Edit[0]["post_status"]                     = "R";
                        row2Edit[0]["post_status_descr"]               = "RELEASED";

                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "payroll_registry_nbr = '" + txtb_registry_no.Text.ToString().Trim() + "' AND payroll_year = '" + ddl_year.SelectedValue.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"]             = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_month"]            = ddl_month.SelectedValue.ToString().Trim();
                        row2Edit[0]["payrolltemplate_code"]     = ddl_payroll_template.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_group_nbr"]        = ddl_payroll_group.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_period_from"]      = txtb_period_from.Text.ToString().Trim();
                        row2Edit[0]["payroll_period_to"]        = txtb_period_to.Text.ToString().Trim();
                        row2Edit[0]["payroll_registry_descr"]   = txtb_registry_descr.Text.ToString().Trim();


                        if (lbl_behaviour_mode_hidden.Text == "RTN")
                        {
                            // If the Behavior is RTN : Update the Post Status on Heder will Update into = N 
                            row2Edit[0]["post_status"]       = "N";
                            row2Edit[0]["post_status_descr"] = "NOT POSTED";
                        }
                        else if (lbl_behaviour_mode_hidden.Text == "VOI")
                        {
                            // If the Behavior is VOI : Update the Post Status on Heder will Update into = V
                            row2Edit[0]["post_status"]       = "V";
                            row2Edit[0]["post_status_descr"] = "VOIDED";
                        }
                        
                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                    RetrieveDataListGrid();

                    txtb_search_scan.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                    txtb_search_scan.Focus();
                }
                   
            }

        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtb_search.Text.ToString().Trim());

            txtb_search.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search.Focus();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

        }
        //**********************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void SearchData(string search)
        {
            string searchExpression = "payroll_registry_nbr LIKE '%" + search.Trim().Replace("'", "''") + "%' OR " +
                "payroll_registry_descr LIKE '%" + search.Trim().Replace("'", "''") + "%' OR " +
                "payroll_period_from LIKE '%" + search.Trim().Replace("'", "''") + "%' OR " +
                "payroll_group_nbr LIKE '%" + search.Trim().Replace("'", "''") + "%' OR " +
                "gross_pay LIKE '%" + search.Trim().Replace("'", "''") + "%' OR " +
                "net_pay LIKE '%" + search.Trim().Replace("'", "''") + "%' OR " +
                "post_status_descr LIKE '%" + search.Trim().Replace("'", "''") + "%' OR " +
                "payroll_period_descr LIKE '%" + search.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_month", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("payroll_group_nbr", typeof(System.String));
            dtSource1.Columns.Add("payroll_period_from", typeof(System.String));
            dtSource1.Columns.Add("payroll_period_to", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_descr", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("payroll_period_descr", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("date_posted", typeof(System.String));
            DataRow[] rows = dataListGrid.Select(searchExpression);
            dtSource1.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource1.ImportRow(row);
                }
            }
            gv_dataListGrid.DataSource = dtSource1;
            gv_dataListGrid.DataBind();
            up_dataListGrid.Update();
            
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
        //  BEGIN - VJA- 01/17/2019 - Check if Object already contains value  
        //*************************************************************************
        protected void CheckInputValue(object sender, EventArgs e)
        {
            TextBox TextBox1 = (TextBox)sender;
            string checkValue = TextBox1.Text;
            string checkName = TextBox1.ID;

            if (checkValue.ToString() != "")
            {
                FieldValidationColorChanged(false, checkName);
            }
            TextBox1.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            TextBox1.Focus();
        }

        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            
            try
            {
                DateTime.Parse(txtb_period_from.Text);
                UpdateDateFrom.Update();
                DateTime.Parse(txtb_period_to.Text);
                UpdateDateTo.Update();

                if (txtb_registry_descr.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_registry_descr");
                    txtb_registry_descr.Focus();
                    validatedSaved = false;
                }
                if (ddl_payroll_group.SelectedValue == "")
                {
                    FieldValidationColorChanged(true, "ddl_payroll_group");
                    ddl_payroll_group.Focus();
                    validatedSaved = false;
                }
                if (txtb_period_from.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_period_from");
                    txtb_period_from.Focus();
                    validatedSaved = false;
                }
                if (txtb_period_to.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_period_to");
                    txtb_period_to.Focus();
                    validatedSaved = false;
                }
                if (ddl_department.SelectedValue == "" && lbl_behaviour_mode_hidden.Text == "RTN")
                {
                    FieldValidationColorChanged(true, "ddl_department");
                    ddl_department.Focus();
                    validatedSaved = false;
                }
            }
            catch (Exception)
            {
                if (txtb_registry_descr.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_registry_descr");
                    txtb_registry_descr.Focus();
                    validatedSaved = false;
                }
                if (txtb_period_from.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_period_from");
                    txtb_period_from.Focus();
                    validatedSaved = false;
                }
                else if(txtb_period_from.Text != "")
                {
                    FieldValidationColorChanged(true, "invalid-date-1");
                    txtb_period_from.Focus();
                    validatedSaved = false;
                }
                if (txtb_period_to.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_period_to");
                    txtb_period_to.Focus();
                    validatedSaved = false;
                }
                else if (txtb_period_to.Text != "")
                {
                    FieldValidationColorChanged(true, "invalid-date-2");
                    txtb_period_to.Focus();
                    validatedSaved = false;
                }
                //if (CommonCode.checkisdatetime(txtb_date_release.Text) == false && ddl_post_status.SelectedValue == "R")
                //{
                //    FieldValidationColorChanged(true, "txtb_date_release");
                //    txtb_date_release.Focus();
                //    validatedSaved = false;
                //}
                validatedSaved = false;
            }
            
            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "txtb_registry_descr":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_registry_descr.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_period_from":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_period_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "ddl_payroll_group":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            ddl_payroll_group.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "txtb_period_to":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_period_to.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "invalid-date-1":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_period_from.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "invalid-date-2":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_period_to.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                    case "ddl_department":
                        {
                            LblRequired7.Text = MyCmn.CONST_RQDFLD;
                            ddl_department.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            break;
                        }
                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired1.Text = "";
                            LblRequired2.Text = "";
                            LblRequired3.Text = "";
                            LblRequired5.Text = "";
                            LblRequired7.Text = "";

                            txtb_registry_descr.BorderColor = Color.LightGray;
                            txtb_period_from.BorderColor = Color.LightGray;
                            txtb_period_to.BorderColor = Color.LightGray;
                            ddl_payroll_group.BorderColor = Color.LightGray;
                            ddl_department.BorderColor = Color.LightGray;
                            
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            UpdateDateFrom.Update();
                            UpdateDateTo.Update();
                            UpdateDateTo.Update();
                            break;
                        }
                }
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Employment Type
        //*************************************************************************
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetriveTemplate();
            RetriveGroupings();
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Close Notification
        //*************************************************************************
        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Payroll Year
        //*************************************************************************
        protected void ddl_year_SelectedIndexChanged1(object sender, EventArgs e)
        {
            RetriveGroupings();
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Payroll Group
        //*************************************************************************
        protected void ddl_payroll_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtb_registry_descr.Text =  ddl_payroll_group.SelectedItem.Text.ToString();
            get_default();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Template Type/Code
        //*************************************************************************
        protected void ddl_payroll_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetriveGroupings();
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers when Select Payroll Month
        //*************************************************************************
        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetriveGroupings();
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Open the modal Select Report
        //*************************************************************************
        protected void imgbtn_print_Command1(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string payroll_registry = "payroll_registry_nbr = '" + commandarg[0].Trim() + "'";

            RetrieveRelatedTemplate();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport", "openSelectReport();", true);
            lnkPrint.CommandArgument = e.CommandArgument.ToString();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - 
        //*************************************************************************
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string printreport;
            string procedure;
            string url = "";
            Session["history_page"] = Request.Url.AbsolutePath;
            //Session["PreviousValuesonPage_cPayRlsRet"] = "";
            Session["PreviousValuesonPage_cPayRlsRet"] = ddl_year.SelectedValue.ToString() + "," 
                                                        + ddl_month.SelectedValue.ToString().Trim() + "," 
                                                        + ddl_empl_type.SelectedValue.ToString() + "," 
                                                        + ddl_payroll_template.SelectedValue.ToString() + ","
                                                        + gv_dataListGrid.PageIndex + "," 
                                                        + DropDownListID.SelectedValue.ToString() + ","
                                                        + lnkPrint.CommandArgument.Split(',')[1].ToString().Trim() + ","
                                                        + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + "," 
                                                        + txtb_search.Text.ToString().Trim() + "," 
                                                        + ddl_payrolltype.SelectedValue.ToString().Trim();

            try
            {
                switch (ddl_select_report.SelectedValue)
                {
                    case "105": // Obligation Request (OBR) - For Regular 
                    case "205": // Obligation Request (OBR) - For Casual
                    case "305": // Obligation Request (OBR) - For Job-Order
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_obr_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim();

                        break;

                    //---- START OF REGULAR REPORTS

                    case "007": // Summary Monthly Salary  - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "101": // Mandatory Deduction  - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "102": // Optional Deduction Page 1 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "106": // Optional Deduction Page 2 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "103": // Loan Deduction Page 1 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "107": // Loan Deduction Page 2 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "104": // Attachment - For Monthly Salary
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_attach_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                        break;

                    case "033": // Salary Differential - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_diff_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    //---- END OF REGULAR REPORTS

                    //---- START OF CASUAL REPORTS

                    case "008": // Summary Monthly Salary  - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "206": // Mandatory Deduction  -  For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "207": // Optional Deduction Page 1 - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "208": // Optional Deduction Page 2 - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "209": // Loan Deduction Page 1 - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "210": // Loan Deduction Page 2 - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "211": // Attachment - For Monthly Salary - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_ce_attach_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                        break;

                    case "044": // Monetization Payroll - For Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    //---- END OF CASUAL REPORTS

                    //---- START OF JOB-ORDER REPORTS

                    case "009": // Summary Salary Monthly - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "010": // Summary Salary 1st Quincemna - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "011": // Summary Salary 2nd Quincemna - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "306": // Contributions/Deductions Page 1 - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "307": // Contributions/Deductions Page 1 - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "308": // Attachment - For Monthly Salary
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_re_attach_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                        break;

                    case "061": // Overtime Payroll - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_ovtm_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "062": // Honorarium Payroll - For Job-Order 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;


                    //---- END OF JOB-ORDER REPORTS
                    //---- START OF OTHER PAYROLL REPORTS

                    case "024": // Communication Expense Allowance - Regular
                    case "043": // Communication Expense Allowance - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "026": // Mid Year Bonus  - Regular        
                    case "045": // Mid Year Bonus  - Casual       
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "027": // Year-End And Cash Gift Bonus - Regular
                    case "046": // Year-End And Cash Gift Bonus - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "028": // Clothing Allowance - Regular
                    case "047": // Clothing Allowance - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "029": // Loyalty Bonus        - Regular
                    case "048": // Loyalty Bonus        - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "030": // Anniversary Bonus    - Regular
                    case "049": // Anniversary Bonus    - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "031": // Productivity Enhancement Incentive Bonus  - Regular
                    case "050": // Productivity Enhancement Incentive Bonus  - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "023": // RATA 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_rata_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "108": // RATA - OBR Breakdown
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_obr_rata_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();

                        break;

                    case "021": // Subsistence, HA and LA      - Regular
                    case "041": // Subsistence, HA and LA      - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_subs_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "022": // Overtime - Regular
                    case "042": // Overtime - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_ovtm_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "032": // CNA INCENTIVE - Regular
                    case "051": // CNA INCENTIVE - Casual
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "025": // Monetization Payroll - For Regular
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_oth1_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "901": // Other Payroll 1 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "902": // Other Payroll 2 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "903": // Other Payroll 3 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "904": // Other Payroll 4 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "905": // Other Payroll 5 - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_othpay_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim();

                        break;

                    case "109": // Communicatio Expense - OBR Breakdown
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_obr_commx_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();

                        break;

                    case "": // Direct Print to Printer
                        url = "/View/cDirectToPrinter/cDirectToPrinter.aspx";
                        break;

                    case "111": // Attachment - FOR RATA PAYROLL
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_RATA_attach_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                        break;

                    case "212": // PaySLip  - For Regular 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_payslip_re_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim() + ",par_empl_id," + "";

                        break;

                    case "214": // PaySLip  - For Casual 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_payrollregistry_salary_payslip_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_select_report.SelectedValue.ToString().Trim() + ",par_empl_id," + "";
                        break;

                    case "604":    // RE - Honorarium Voucher 
                    case "704":    // CE - Honorarium Voucher 
                    case "804":    // JO - Honorarium Voucher 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_voucher_tbl_repo";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_voucher_ctrl_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();
                        break;

                    case "601":    // RE - Refund To Employee Voucher 
                    case "701":    // CE - Refund To Employee Voucher 
                    case "801":    // JO - Refund To Employee Voucher 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_voucher_tbl_repo";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_voucher_ctrl_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();
                        break;

                    case "603":    // RE - Terminal Leave 
                    case "703":    // CE - Terminal Leave 
                    case "803":    // JO - Terminal Leave 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_voucher_tbl_repo2";     // -- SP for Terminal is separate from other Voucher
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_voucher_ctrl_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();
                        break;

                    case "605":    // RE - Other Salaries 
                    case "705":    // CE - Other Salaries 
                    case "805":    // JO - Other Salaries 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_voucher_tbl_repo2";     // -- SP for Terminal is separate from other Voucher
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_voucher_ctrl_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();
                        break;

                    case "606":    // RE - Mid Year Bonus 
                    case "706":    // CE - Mid Year Bonus 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_voucher_tbl_repo2";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_voucher_ctrl_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();
                        break;

                    case "607":    // RE - Year End and Cash Gift 
                    case "707":    // CE - Year End and Cash Gift 
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_voucher_tbl_repo2";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_voucher_ctrl_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim();
                        break;

                    case "110":    // RE - OBR for Voucher
                        printreport = hidden_report_filename.Text;
                        procedure = "sp_voucher_obr_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_voucher_ctrl_nbr," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim();
                        break;

                }

                if (url != "")
                {
                    Response.Redirect(url);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Trigger When Select From Related Template
        //*************************************************************************
        protected void ddl_select_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetReportFile();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Get Report Filename
        //*************************************************************************
        private void GetReportFile()
        {
            //RetrieveRelatedTemplate();l
            string searchExpression = "payrolltemplate_code = '" + ddl_select_report.SelectedValue.ToString().Trim() + "'";
            DataRow[] row2Search = dtSourse_for_template.Select(searchExpression);

            hidden_report_filename.Text = row2Search[0]["report_filename"].ToString();
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetrieveBindingDep()
        {
            ddl_department.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_departments_tbl_list", "par_include_history", "N");

            ddl_department.DataSource = dt;
            ddl_department.DataValueField = "department_code";
            ddl_department.DataTextField = "department_name1";
            ddl_department.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_department.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Release , Return and Void Payroll
        //*************************************************************************
        protected void imgbtn_released_return_void_Command(object sender, CommandEventArgs e)
        {
            editaddmodal(e.CommandArgument.ToString());
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        private void editaddmodal(string payroll_registry_nbr_year)
        {

            string[] svalues = payroll_registry_nbr_year.ToString().Split(new char[] { ',' });
            string editExpression = "payroll_registry_nbr = '" + svalues[0].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            RetriveGroupings();
            ClearEntry();

            txtb_registry_no.Text           = row2Edit[0]["payroll_registry_nbr"].ToString();
            txtb_registry_descr.Text        = row2Edit[0]["payroll_registry_descr"].ToString();
            txtb_period_from.Text           = row2Edit[0]["payroll_period_from"].ToString();
            txtb_period_to.Text             = row2Edit[0]["payroll_period_to"].ToString();
            ddl_payroll_group.SelectedValue = row2Edit[0]["payroll_group_nbr"].ToString();
            txtb_registry_descr.Text        = row2Edit[0]["payroll_registry_descr"].ToString();

            //txtb_no_works.Text              = row2Edit[0]["nod_work_1st"].ToString();

            ddl_department.SelectedValue    = row2Edit[0]["department_code"].ToString();
            txtb_remarks.Text               = row2Edit[0]["remarks"].ToString();
            txtb_released_by.Text           = row2Edit[0]["user_id_released_by"].ToString();
            txtb_returned_by.Text           = row2Edit[0]["user_id_returned_by"].ToString();
            txtb_voided_by.Text             = row2Edit[0]["user_id_void_by"].ToString();
            txtb_date_released.Text         = row2Edit[0]["released_dttm_1"].ToString();
            txtb_date_returned.Text         = row2Edit[0]["returned_dttm_1"].ToString();
            txtb_date_voided.Text           = row2Edit[0]["void_dttm_1"].ToString();

            if (svalues[1].ToString() == "RLS")
            {
                LabelAddEdit.Text = "Release Payroll";
                btnSave.Text = "Release";

                lbl_date_released.Visible = true;
                txtb_date_released.Visible = true;
                lbl_date_returned.Visible = false;
                txtb_date_returned.Visible = false;
                lbl_department.Visible = false;
                ddl_department.Visible = false;
                lbl_remarks.Visible = false;
                txtb_remarks.Visible = false;
                lbl_date_voided.Visible = false;
                txtb_date_voided.Visible = false;

                txtb_released_by.Visible = true;
                lbl_released_by.Visible = true;
                txtb_returned_by.Visible = false;
                lbl_returned_by.Visible = false;
                txtb_voided_by.Visible = false;
                lbl_voided_by.Visible = false;

                if (txtb_date_returned.Text != "" || txtb_released_by.Text != "")
                {
                    lbl_department.Visible = true;
                    ddl_department.Visible = true;
                    lbl_remarks.Visible = true;
                    txtb_remarks.Visible = true;

                    txtb_remarks.Enabled = false;
                    ddl_department.Enabled = false;
                }

                InitializeTable();
                AddPrimaryKeys();
                AddNewRow();

                txtb_released_by.Text = Session["ep_user_id"].ToString();
                txtb_date_released.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
                lbl_behaviour_mode_hidden.Text = "RLS";

            }
            else if (svalues[1].ToString() == "RTN")
            {
                LabelAddEdit.Text = "Return Payroll";
                btnSave.Text = "Return";

                lbl_date_released.Visible = false;
                txtb_date_released.Visible = false;
                lbl_date_returned.Visible = true;
                txtb_date_returned.Visible = true;
                lbl_department.Visible = true;
                ddl_department.Visible = true;
                lbl_remarks.Visible = true;
                txtb_remarks.Visible = true;
                lbl_date_voided.Visible = false;
                txtb_date_voided.Visible = false;

                txtb_released_by.Visible = false;
                lbl_released_by.Visible = false;
                txtb_returned_by.Visible = true;
                lbl_returned_by.Visible = true;
                txtb_voided_by.Visible = false;
                lbl_voided_by.Visible = false;

                InitializeTable();
                AddPrimaryKeys();
                DataRow nrow = dtSource.NewRow();
                nrow["payroll_year"] = string.Empty;
                nrow["payroll_registry_nbr"] = string.Empty;
                nrow["action"] = 2;
                nrow["retrieve"] = true;
                dtSource.Rows.Add(nrow);

                txtb_returned_by.Text = Session["ep_user_id"].ToString();
                txtb_date_returned.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
                lbl_behaviour_mode_hidden.Text = "RTN";
            }
            else if (svalues[1].ToString() == "VOI")
            {
                LabelAddEdit.Text = "Void Payroll";
                btnSave.Text = "Void";

                lbl_date_released.Visible = false;
                txtb_date_released.Visible = false;
                lbl_date_returned.Visible = false;
                txtb_date_returned.Visible = false;
                lbl_department.Visible = false;
                ddl_department.Visible = false;
                lbl_remarks.Visible = false;
                txtb_remarks.Visible = false;
                lbl_date_voided.Visible = true;
                txtb_date_voided.Visible = true;

                txtb_released_by.Visible = false;
                lbl_released_by.Visible = false;
                txtb_returned_by.Visible = false;
                lbl_returned_by.Visible = false;
                txtb_voided_by.Visible = true;
                lbl_voided_by.Visible = true;

                InitializeTable();
                AddPrimaryKeys();
                DataRow nrow = dtSource.NewRow();
                nrow["payroll_year"] = string.Empty;
                nrow["payroll_registry_nbr"] = string.Empty;
                nrow["action"] = 2;
                nrow["retrieve"] = true;
                dtSource.Rows.Add(nrow);

                txtb_voided_by.Text = Session["ep_user_id"].ToString();
                txtb_date_voided.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
                lbl_behaviour_mode_hidden.Text = "VOI";
            }

            ddl_payroll_group.Enabled = false;
            FieldValidationColorChanged(false, "ALL");
            
            CheckIfChanged();
        }
        //**************************************************************************
        //  BEGIN - VJA- 11/22/2019 - Method for Check if the Record is Updated,Delete 
        //                          and Insert by another user
        //*************************************************************************
        private void CheckIfChanged()
        {
            RetrieveDataListGrid();
            
            DataTable datasource_chk = MyCmn.RetrieveData("sp_payrollregistry_hdr_rls_tbl_chk", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", txtb_registry_no.Text.ToString().Trim());
            
            if (datasource_chk.Rows.Count < 0)
            {
                msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-danger");
                msg_header.InnerText = "Error Stored Procedure";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
            }
            if (datasource_chk.Rows[0]["post_status"].ToString() == "R" && lbl_behaviour_mode_hidden.Text.ToString() == "RLS")
            {
                msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                msg_header.InnerText = "Data Released by other User/s";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
                
            }
            else if (datasource_chk.Rows[0]["post_status"].ToString() == "N" && lbl_behaviour_mode_hidden.Text == "RTN")
            {
                msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                msg_header.InnerText = "Data Returned by other User/s";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
            }
            else if (datasource_chk.Rows[0]["post_status"].ToString() == "V" && lbl_behaviour_mode_hidden.Text == "VOI")
            {
                msg_icon.Attributes.Add("class", "fa-5x fa fa-exclamation-triangle text-warning");
                msg_header.InnerText = "Data Voided by other User/s";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            RetrieveDataListGrid();
            return;
        }
        //*************************************************************************
        //  BEGIN - VJA- 12/12/2019 - Scan and Input 
        //*************************************************************************
        protected void txtb_search_scan_TextChanged(object sender, EventArgs e)
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_hdr_tbl_list_payroll_registry_nbr", "par_payroll_registry_nbr", txtb_search_scan.Text.ToString().Trim());

            if (dataListGrid.Rows.Count > 0 || dataListGrid.Rows == null)
            {
                ddl_year.SelectedValue              = dataListGrid.Rows[0]["payroll_year"].ToString();
                ddl_month.SelectedValue             = dataListGrid.Rows[0]["payroll_month"].ToString();
                ddl_empl_type.SelectedValue         = dataListGrid.Rows[0]["employment_type"].ToString();
                ddl_payrolltype.SelectedValue       = dataListGrid.Rows[0]["payrolltemplate_type"].ToString();
                RetriveTemplate();
                ddl_payroll_template.SelectedValue  = dataListGrid.Rows[0]["payrolltemplate_code"].ToString();
                
                editaddmodal(dataListGrid.Rows[0]["payroll_registry_nbr"].ToString() + "," + "RLS");
                SearchData(txtb_search_scan.Text.ToString().Trim());
                txtb_search_scan.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                txtb_search_scan.Focus();
                show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";


                txtb_search_scan.Text = "";
                UpdatePanel10.Update();
             }

        }
        protected void ddl_payrolltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DataTable dt = new DataTable();
            if (ddl_payrolltype.SelectedValue == "01")
            {
                ddl_payroll_template.Items.Clear();
                dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list6", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
                
            }
            else if (ddl_payrolltype.SelectedValue == "02")
            {
                ddl_payroll_template.Items.Clear();
                dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list5", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
            }
            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
            
            RetrieveDataListGrid();
        }
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}