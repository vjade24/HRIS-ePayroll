//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Payroll for Overtime
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade Alivio       04/02/2019      Code Creation

// Update Date : 06/14/2019 By: VJA
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View
{
    public partial class cPayOvertime : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 04/02/2019 - Data Place holder creation 
        //********************************************************************
        DataTable dtSource_dtl
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl"];
            }
            set
            {
                ViewState["dtSource_dtl"] = value;
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

        DataTable dataList_employee
        {
            get
            {
                if ((DataTable)ViewState["dataList_employee"] == null) return null;
                return (DataTable)ViewState["dataList_employee"];
            }
            set
            {
                ViewState["dataList_employee"] = value;
            }
        }

        DataTable dtGroup
        {
            get
            {
                if ((DataTable)ViewState["dtGroup"] == null) return null;
                return (DataTable)ViewState["dtGroup"];
            }
            set
            {
                ViewState["dtGroup"] = value;
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
        DataTable dtSource_dtl_brkdwn
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_brkdwn"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_brkdwn"];
            }
            set
            {
                ViewState["dtSource_dtl_brkdwn"] = value;
            }
        }
        DataTable dataList_employee_tax
        {
            get
            {
                if ((DataTable)ViewState["dataList_employee_tax"] == null) return null;
                return (DataTable)ViewState["dataList_employee_tax"];
            }
            set
            {
                ViewState["dataList_employee_tax"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 03/12/2019 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 04/02/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "employee_name";
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

        void Page_LoadComplete(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ViewState["page_allow_add"] = Session["page_allow_add_from_registry"];
                ViewState["page_allow_delete"] = Session["page_allow_delete_from_registry"];
                ViewState["page_allow_edit"] = Session["page_allow_edit_from_registry"];
                ViewState["page_allow_edit_history"] = Session["page_allow_edit_history_from_registry"];
                ViewState["page_allow_print"] = Session["page_allow_print_from_registry"];

                //********************************************************************
                //  BEGIN - VJA- 06/20/2019 - This is Session Variable Coming From Login
                //********************************************************************
                if (Session["ep_post_authority"].ToString() == "1")
                {
                    ViewState["page_allow_add"]     = 0;
                    ViewState["page_allow_delete"]  = 1;
                    ViewState["page_allow_edit"]    = 1;
                    ViewState["page_allow_print"]   = 1;
                }
                else
                {
                    ViewState["page_allow_add"]     = 1;
                    ViewState["page_allow_delete"]  = 1;
                    ViewState["page_allow_edit"]    = 1;
                    ViewState["page_allow_print"]   = 0;
                }

                if (Session["PreviousValuesonPage_cPayRegistry"] == null)
                    Session["PreviousValuesonPage_cPayRegistry"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    RetrieveEmploymentType();
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_month.SelectedValue = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue = prevValues[2].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue = prevValues[3].ToString();
                    RetriveGroupings();
                    ddl_payroll_group.SelectedValue = prevValues[7].ToString();
                    lbl_registry_number.Text = prevValues[7].ToString();
                    ddl_payroll_template.Enabled = false;
                    ddl_month.Enabled = false;
                    ddl_year.Enabled = false;
                    ddl_empl_type.Enabled = false;

                    RetrieveEmployeename();
                    RetrieveDataListGrid();
                    RetrievePayrollInstallation();

                }

                // BEGIN - VJA : 02/01/2020 - Hide the ADD BUTTON IF THE STATUS ARE ;
                // 1. ) R = Released
                // 2. ) Y = Posted
                // 3. ) T = Return
                if (Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "R" ||
                    Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "Y" )
                {
                    ViewState["page_allow_add"] = 0;
                }
                // END   - VJA : 02/01/2020 - Hide the ADD BUTTON IF THE STATUS ARE ;
            }
        }

        //********************************************************************
        //  BEGIN - VJA- 04/02/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPayOvertime"] = "cPayOvertime";
            RetrieveDataListGrid();
            RetrieveEmploymentType();
            RetrieveEmployeename();
            RetrievePayrollInstallation();
            RetrieveDataListGrid_Oth();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Retrieve From Installation Table 
        //*************************************************************************
        private void RetrievePayrollInstallation()
        {
            //  This is to Get Last Day of The Month
            //DateTime last_Date = new DateTime(Convert.ToInt32(ddl_year.SelectedValue.ToString()), Convert.ToInt32(ddl_month.SelectedValue.ToString()), 1).AddMonths(1).AddDays(-1);
            //int lastday = Convert.ToInt32(last_Date.Day.ToString());
            //hidden_monthly_days.Value = lastday.ToString();

            DataTable dt = MyCmn.RetrieveData("sp_payrollinstallation_tbl_list", "par_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_include_history", "N");
            if (dt.Rows.Count > 0)
            {
                hidden_monthly_days.Value       = dt.Rows[0]["monthly_salary_days_conv"].ToString();
                lbl_minimum_netpay_hidden.Value = dt.Rows[0]["minimum_net_pay"].ToString();
                hidden_hours_in_days.Value      = dt.Rows[0]["hours_in_1day_conv"].ToString();
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private string GetRegistry_NBR()
        {
            string group_nbr = "";
            if (ddl_payroll_group.SelectedValue.ToString().Trim() != "")
            {
                lbl_registry_number.Text = ddl_payroll_group.SelectedValue.ToString();
                if (dtGroup != null && dtGroup.Rows.Count > 0)
                {
                    DataRow[] registry_nbr = dtGroup.Select("payroll_registry_nbr='" + ddl_payroll_group.SelectedValue.ToString() + "'");
                    group_nbr = registry_nbr[0]["payroll_group_nbr"].ToString().Trim();
                }
            }
            return group_nbr;
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Populate Combo list for Employee Name
        //*************************************************************************
        private void RetrieveEmployeename()
        {
            ddl_empl_id.Items.Clear();
            // dtSource_for_names = MyCmn.RetrieveData("sp_personnelnames_combolist_ovtm", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR());
            dtSource_for_names = MyCmn.RetrieveData("sp_personnelnames_combolist_ovtm1", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim());

            ddl_empl_id.DataSource = dtSource_for_names;

            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetrieveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");
            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataBind();
            ddl_empl_type.Enabled = false;
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Populate Combo list for Payroll Year
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
        //  BEGIN - VJA- 04/02/2019 - Populate Combo list for Groupings
        //*************************************************************************
        private void RetriveGroupings()
        {
            ddl_payroll_group.Items.Clear();
            dtGroup = MyCmn.RetrieveData("sp_payrollregistry_hdr_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            dtGroup.DefaultView.Sort = "grouping_descr";
            dtGroup.AcceptChanges();
            ddl_payroll_group.DataSource = dtGroup;
            ddl_payroll_group.DataValueField = "payroll_registry_nbr";
            ddl_payroll_group.DataTextField = "grouping_descr";
            ddl_payroll_group.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_group.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveTemplate()
        {
            ddl_payroll_template.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_dtl_ovtm_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_payrolltemplate_code",ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid_Brkdwn()
        {
            InitializeTable_brkdwn();
            dtSource_dtl_brkdwn = MyCmn.RetrieveData("sp_payrollregistry_dtl_ovtm_brkdwn_tbl_list", "par_payroll_registry_nbr", ddl_payroll_group.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_ot_year", ddl_year.SelectedValue.ToString().Trim());
            AddPrimaryKeys_brkdwn();

            foreach (DataRow nrow1 in dtSource_dtl_brkdwn.Rows)
            {
                nrow1["action"] = 1;
                nrow1["retrieve"] = false;
            }

            MyCmn.Sort(gv_dataListGrid_brkdwn, dtSource_dtl_brkdwn, "ot_date", Session["SortOrder"].ToString());
            up_dataListGrid_brkdwn.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();
            RetrieveEmployeename();

            //btnSave.Visible             = true;
            txtb_voucher_nbr.Enabled    = false;
            //Get last row of the Column
            LabelAddEdit.Text = "Add Record | Registry No: " + lbl_registry_number.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL","0");

            ddl_empl_id.Visible         = true;
            ddl_empl_id.Visible         = true;
            txtb_employeename.Visible   = false;
            txtb_employeename.Visible   = false;
            ToogleTextbox(true);
            ToogleDisplay_AddEdit_RECE_JO();

            // ****************************************
            // **** BEGIN : BREAKDOWN OVERTIME ********
            // ****************************************
            RetrieveDataListGrid_Brkdwn();
            btn_save_brkdwn.Text = "Save/Add";
            btn_save_brkdwn.Attributes.Add("class", "btn btn-primary btn-sm btn-block  save-icon icn");
            ViewState.Add("AddEdit_Mode_BrkDwn", "BRK_ADD");
            div_msg.Visible     = false;
            hdr_msg.InnerText   = "";
            dtl_msg.Text        = "";
            // ****************************************
            // **** END   : BREAKDOWN OVERTIME ********
            // ****************************************

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_monthly_rate.Text          = "0.00";
            txtb_daily_rate.Text            = "0.00";
            txtb_hourly_rate_actual.Text    = "0.00";
            txtb_hourly_rate_25.Text        = "0.00";
            txtb_hourly_rate_50.Text        = "0.00";
            txtb_hr_actual.Text             = "0.00";
            txtb_hr_25.Text                 = "0.00";
            txtb_hr_50.Text                 = "0.00";
            txtb_bir_tax.Text               = "0.00";
            txtb_remarks.Text               = "";
            txtb_net_pay.Text               = "0.00";
            txtb_gross_pay.Text             = "0.00";
            //ddl_empl_id.SelectedIndex       = -1;
            txtb_empl_id.Text               = "";
            txtb_employeename.Text          = "";

            txtb_tax2.Text                  = "0.00";
            txtb_tax3.Text                  = "0.00";
            txtb_tax5.Text                  = "0.00";
            txtb_tax8.Text                  = "0.00";
            txtb_tax10.Text                 = "0.00";
            txtb_tax15.Text                 = "0.00";

            txtb_department_descr.Text      = "";
            txtb_voucher_nbr.Text           = "";
            ViewState["created_by_user"]    = "";
            ViewState["updated_by_user"]    = "";
            ViewState["posted_by_user"]     = "";
            ViewState["created_dttm"]       = "";
            ViewState["updated_dttm"]       = "";
            txtb_date_posted.Text           = "";
            txtb_position.Text              = "";
            txtb_status.Text                = "";
            lbl_if_dateposted_yes.Text      = "";
            lbl_post_status.Text      = "";
            txtb_ot_date.Text      = "";
            FieldValidationColorChanged(false, "ALL", "0");
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable_dtl()
        {
            dtSource_dtl = new DataTable();
            dtSource_dtl.Columns.Add("payroll_year", typeof(System.String));
            dtSource_dtl.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl.Columns.Add("monthly_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("daily_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("hourly_rate", typeof(System.String));
            dtSource_dtl.Columns.Add("hourly_rate_25", typeof(System.String));
            dtSource_dtl.Columns.Add("hourly_rate_50", typeof(System.String));
            dtSource_dtl.Columns.Add("gross_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("net_pay", typeof(System.String));
            dtSource_dtl.Columns.Add("ot_hour_reg", typeof(System.String));
            dtSource_dtl.Columns.Add("ot_hour_25", typeof(System.String));
            dtSource_dtl.Columns.Add("ot_hour_50", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax2", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax3", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax5", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax8", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax10", typeof(System.String));
            dtSource_dtl.Columns.Add("bir_tax15", typeof(System.String));
            dtSource_dtl.Columns.Add("remarks", typeof(System.String));
            dtSource_dtl.Columns.Add("post_status", typeof(System.String));
            dtSource_dtl.Columns.Add("date_posted", typeof(System.String));

            dtSource_dtl.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource_dtl.Columns.Add("created_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("posted_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("created_dttm", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_dttm", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Add Primary Key and Defining What Table Will Data Saved
        //*************************************************************************
        private void AddPrimaryKeys_dtl()
        {
            dtSource_dtl.TableName = "payrollregistry_dtl_ovtm_tbl";
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr", "empl_id" };
            dtSource_dtl = MyCmn.AddPrimaryKeys(dtSource_dtl, col);
            dtSource_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow_dtl()
        {
            DataRow nrow = dtSource_dtl.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["monthly_rate"] = string.Empty;
            nrow["daily_rate"] = string.Empty;
            nrow["hourly_rate"] = string.Empty;
            nrow["hourly_rate_25"] = string.Empty;
            nrow["hourly_rate_50"] = string.Empty;
            nrow["gross_pay"] = string.Empty;
            nrow["net_pay"] = string.Empty;
            nrow["ot_hour_reg"] = string.Empty;
            nrow["ot_hour_25"] = string.Empty;
            nrow["ot_hour_50"] = string.Empty;
            nrow["bir_tax"] = string.Empty;
            nrow["bir_tax2"] = string.Empty;
            nrow["bir_tax3"] = string.Empty;
            nrow["bir_tax5"] = string.Empty;
            nrow["bir_tax8"] = string.Empty;
            nrow["bir_tax10"] = string.Empty;
            nrow["bir_tax15"] = string.Empty;
            nrow["remarks"] = string.Empty;
            nrow["post_status"] = string.Empty;
            nrow["date_posted"] = string.Empty;

            
            nrow["voucher_nbr"]             = string.Empty;
            nrow["created_by_user"]         = string.Empty;
            nrow["updated_by_user"]         = string.Empty;
            nrow["posted_by_user"]          = string.Empty;
            nrow["created_dttm"]            = string.Empty;
            nrow["updated_dttm"]            = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource_dtl.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id              = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year         = commandArgs[2];
            txtb_reason.Text            = "";
            FieldValidationColorChanged(false, "ALL","");
            if (Session["ep_post_authority"].ToString() == "0")
            {
                // This is the Normal Delete if the user is Non-accounting user
                deleteRec1.Text         = "Are you sure to delete this Record ?";
                deleteRec0.InnerText    = "Delete this Record ?";
                lbl_unposting.Text      = "";
                txtb_reason.Visible     = false;
                lnkBtnYes.Text          = "Yes, Delete it";
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                // This is Message if the accounting user will unpost the card 
                deleteRec1.Text         = "Are you sure you want to UnPost this Record ?";
                deleteRec0.InnerText    = "UnPost this Record ?";
                lbl_unposting.Text      = "Reason for UnPosting :";
                txtb_reason.Visible     = true;
                lnkBtnYes.Text          = "Yes, UnPost it";
            }
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "'";
            string deleteExpression_brkdwn = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "'";

            if (Session["ep_post_authority"].ToString() == "0")
            {
                // Non-Accounting Delete 
                MyCmn.DeleteBackEndData("payrollregistry_dtl_ovtm_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_ovtm_brkdwn_tbl", "WHERE " + deleteExpression_brkdwn);
                DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
                dataListGrid.Rows.Remove(row2Delete[0]);
                dataListGrid.AcceptChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                if (txtb_reason.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_reason","");
                }
                else
                {
                    // Stored Procedure to Insert to payrollregistry_dtl_unpost_tbl during accounting case
                    DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_unpost_tbl_insert", "par_payroll_year", ddl_year.SelectedValue, "par_payroll_registry_nbr", commandarg[1].Trim(), "par_empl_id", commandarg[0].Trim(), "par_reason", txtb_reason.Text);

                    //4.4.b.Update the following fields: From payrollregistry_dtl_ovtm_tbl Table
                    //    1.voucher_nbr     =   { blank}
                    //    2.posted_by_user  =   { blank}
                    //    3.post_status     =   "N"   
                    //    4.date_posted     =   { blank}
                    //    5.updated_by_user =   Session User ID
                    //    6.updated_dttm    =   System Date

                    string setparams = "";
                    setparams = "voucher_nbr = '',posted_by_user = '',post_status='N',date_posted='',updated_by_user= '" + Session["ep_user_id"].ToString() + "',updated_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    MyCmn.UpdateTable("payrollregistry_dtl_ovtm_tbl", setparams, "WHERE " + deleteExpression);
                    RetrieveDataListGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
                }
            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
           
            //initialize table for saving in payrollregistry_jo_dtl_tbl
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();
            dtSource_dtl.Rows[0]["action"] = 2;
            dtSource_dtl.Rows[0]["retrieve"] = true;

            txtb_empl_id.Text                   = row2Edit[0]["empl_id"].ToString().Trim();
            txtb_employeename.Text              = row2Edit[0]["employee_name"].ToString().Trim();
            lbl_registry_number.Text            = row2Edit[0]["payroll_registry_nbr"].ToString().Trim();

            txtb_monthly_rate.Text              = row2Edit[0]["monthly_rate"].ToString().Trim();
            txtb_daily_rate.Text                = row2Edit[0]["daily_rate"].ToString().Trim();
            txtb_hourly_rate_actual.Text        = row2Edit[0]["hourly_rate"].ToString().Trim();
            txtb_hourly_rate_25.Text            = row2Edit[0]["hourly_rate_25"].ToString().Trim();
            txtb_hourly_rate_50.Text            = row2Edit[0]["hourly_rate_50"].ToString().Trim();

            txtb_hr_actual.Text                 = row2Edit[0]["ot_hour_reg"].ToString().Trim();
            txtb_hr_25.Text                     = row2Edit[0]["ot_hour_25"].ToString().Trim();
            txtb_hr_50.Text                     = row2Edit[0]["ot_hour_50"].ToString().Trim();
            txtb_bir_tax.Text                   = row2Edit[0]["bir_tax"].ToString().Trim();
            txtb_remarks.Text                   = row2Edit[0]["remarks"].ToString().Trim();
            txtb_net_pay.Text                   = double.Parse(row2Edit[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");
            txtb_gross_pay.Text                 = double.Parse(row2Edit[0]["gross_pay"].ToString().Trim()).ToString("###,##0.00");
            txtb_tax2.Text                      = row2Edit[0]["bir_tax2"].ToString().Trim();
            txtb_tax3.Text                      = row2Edit[0]["bir_tax3"].ToString().Trim();
            txtb_tax5.Text                      = row2Edit[0]["bir_tax5"].ToString().Trim();
            txtb_tax8.Text                      = row2Edit[0]["bir_tax8"].ToString().Trim();
            txtb_tax10.Text                     = row2Edit[0]["bir_tax10"].ToString().Trim();
            txtb_tax15.Text                     = row2Edit[0]["bir_tax15"].ToString().Trim();

                    
            ViewState["department_code"]        = row2Edit[0]["department_code"].ToString().Trim();

            LabelAddEdit.Text = "Edit Record | Registry No: " + lbl_registry_number.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            FieldValidationColorChanged(false, "ALL","0");

            ddl_empl_id.Visible         = false;
            ddl_empl_id.Visible         = false;
            txtb_employeename.Visible   = true;
            txtb_employeename.Visible   = true;

            // calculate_grosspays();
            // calculate_netpays();

            ToogleDisplay_AddEdit_RECE_JO();

            // Add Field Again - 06/20/2019
            txtb_voucher_nbr.Text           = row2Edit[0]["voucher_nbr"].ToString();
            ViewState["created_by_user"]    = row2Edit[0]["created_by_user"].ToString();
            ViewState["updated_by_user"]    = row2Edit[0]["updated_by_user"].ToString();
            ViewState["posted_by_user"]     = row2Edit[0]["posted_by_user"].ToString();
            ViewState["created_dttm"]       = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            ViewState["updated_dttm"]       = Convert.ToDateTime(row2Edit[0]["updated_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            //ddl_status.SelectedValue        = row2Edit[0]["post_status"].ToString();
            txtb_status.Text                = row2Edit[0]["post_status_descr"].ToString();
            lbl_post_status.Text                = row2Edit[0]["post_status_descr"].ToString();
            txtb_date_posted.Text           = row2Edit[0]["date_posted"].ToString();
            txtb_position.Text              = row2Edit[0]["position_title1"].ToString();
            txtb_department_descr.Text      = row2Edit[0]["department_name1"].ToString();
            txtb_bir_tax_rate_perc.Text     = row2Edit[0]["tax_rate"].ToString();

             // The Save Button Will be Visible false if the 
            if (row2Edit[0]["post_status"].ToString() == "Y" && (Session["ep_post_authority"].ToString() == "1" || Session["ep_post_authority"].ToString() == "0"))
            {
                // For Accounting or Other User With Y Status
                btnSave.Visible             = false;
                btn_calculate.Visible       = false;
                Linkbtncancel.Text          = "Close";
                txtb_voucher_nbr.Enabled    = false;
                lbl_if_dateposted_yes.Text  = "This Payroll Already Posted, You cannot Edit!";
                btnSave.Text                = "Save";
                txtb_voucher_nbr.Enabled    = false;
                txtb_date_posted.Text       = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            else if ((row2Edit[0]["post_status"].ToString() == "N" || row2Edit[0]["post_status"].ToString() == "") && Session["ep_post_authority"].ToString() == "1")
            {
                // For Accounting With N Status
                btnSave.Visible             = true;
                btn_calculate.Visible       = false;
                Linkbtncancel.Text          = "Cancel";
                lbl_if_dateposted_yes.Text  = "";
                txtb_voucher_nbr.Enabled    = true;
                btnSave.Text                = "Post to Card";
                txtb_voucher_nbr.Enabled    = true;
                txtb_date_posted.Text       = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }
            // THis is for Released Status
            else if (row2Edit[0]["post_status"].ToString() == "R"
                    //|| row2Edit[0]["post_status"].ToString() == "T"
                    || row2Edit[0]["post_status"].ToString() == "X"
                    )
            {
                // For Accounting or Other User With Y Status
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                Linkbtncancel.Text = "Close";
                txtb_voucher_nbr.Enabled = false;
                lbl_if_dateposted_yes.Text = "This Payroll Already " + row2Edit[0]["post_status_descr"].ToString() + ", You cannot Edit!";
                btnSave.Text = "Save";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            else if (Session["ep_post_authority"].ToString() == "0")
            {
                // For Other Users
                btnSave.Visible             = true;
                btn_calculate.Visible       = true;
                Linkbtncancel.Text          = "Cancel";
                txtb_voucher_nbr.Enabled    = true;
                lbl_if_dateposted_yes.Text  = "";
                btnSave.Text                = "Save";
                txtb_voucher_nbr.Enabled    = false;
                txtb_date_posted.Text =     "";
                ToogleTextbox(true);
            }
            UpdatePanel10.Update();
            
            RetrieveDataListGrid_Brkdwn();
            RetrieveDataListGrid_Oth();

            btn_save_brkdwn.Text = "Save/Add";
            btn_save_brkdwn.Attributes.Add("class", "btn btn-primary btn-sm btn-block  save-icon icn");
            ViewState.Add("AddEdit_Mode_BrkDwn", "BRK_ADD");
            div_msg.Visible     = false;
            hdr_msg.InnerText   = "";
            dtl_msg.Text        = "";

            // Override Tax Rate Base on The Gross pay and Amount of the Tax Amount (Vice Versa)
            double bir_tax_rate_perc = 0;
            bir_tax_rate_perc = double.Parse(txtb_bir_tax.Text) / double.Parse(txtb_gross_pay.Text) * 100;
            txtb_bir_tax_rate_perc.Text = bir_tax_rate_perc.ToString("###,##0.00");

            //calculate_hourly25_50();
            // calculate_grosspays();
            // calculate_netpays();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Change Field Sort mode  
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
        //  BEGIN - VJA- 04/02/2019 - Get Grid current sort order 
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
        //  BEGIN - VJA- 04/02/2019 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL","0");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;
            if (IsDataValidated())
            {
                calculate_grosspays();
                calculate_netpays();

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource_dtl.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString();
                    dtSource_dtl.Rows[0]["empl_id"]                 = ddl_empl_id.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().ToString();
                    dtSource_dtl.Rows[0]["daily_rate"]              = txtb_daily_rate.Text.ToString().Trim(); 
                    dtSource_dtl.Rows[0]["hourly_rate"]             = txtb_hourly_rate_actual.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hourly_rate_25"]          = txtb_hourly_rate_25.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hourly_rate_50"]          = txtb_hourly_rate_50.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ot_hour_reg"]             = txtb_hr_actual.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ot_hour_25"]              = txtb_hr_25.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ot_hour_50"]              = txtb_hr_50.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax"]                 = txtb_bir_tax.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax2"]                = txtb_tax2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax3"]                = txtb_tax3.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax5"]                = txtb_tax5.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax8"]                = txtb_tax8.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax10"]               = txtb_tax10.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax15"]               = txtb_tax15.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["remarks"]                 = txtb_remarks.Text.ToString().Trim();
                    
                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource_dtl.Rows[0]["voucher_nbr"]             = txtb_voucher_nbr.Text.ToString();
                    dtSource_dtl.Rows[0]["created_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource_dtl.Rows[0]["updated_by_user"]         = "";

                    dtSource_dtl.Rows[0]["created_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource_dtl.Rows[0]["updated_dttm"]            = "";

                    dtSource_dtl.Rows[0]["posted_by_user"]          = "";
                    dtSource_dtl.Rows[0]["date_posted"]             = "";

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource_dtl);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {

                    dtSource_dtl.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString();
                    dtSource_dtl.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().ToString();
                    dtSource_dtl.Rows[0]["daily_rate"]              = txtb_daily_rate.Text.ToString().Trim(); 
                    dtSource_dtl.Rows[0]["hourly_rate"]             = txtb_hourly_rate_actual.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hourly_rate_25"]          = txtb_hourly_rate_25.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["hourly_rate_50"]          = txtb_hourly_rate_50.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ot_hour_reg"]             = txtb_hr_actual.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ot_hour_25"]              = txtb_hr_25.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["ot_hour_50"]              = txtb_hr_50.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax"]                 = txtb_bir_tax.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax2"]                = txtb_tax2.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax3"]                = txtb_tax3.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax5"]                = txtb_tax5.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax8"]                = txtb_tax8.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax10"]               = txtb_tax10.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["bir_tax15"]               = txtb_tax15.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["remarks"]                 = txtb_remarks.Text.ToString().Trim();
                    
                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource_dtl.Rows[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                    dtSource_dtl.Rows[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource_dtl.Rows[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource_dtl.Rows[0]["created_dttm"]            = ViewState["created_dttm"].ToString();

                    if (Session["ep_post_authority"].ToString() == "1")
                    {
                        dtSource_dtl.Rows[0]["posted_by_user"]      = Session["ep_user_id"].ToString();
                        dtSource_dtl.Rows[0]["date_posted"]         = txtb_date_posted.Text.ToString().Trim();
                        dtSource_dtl.Rows[0]["post_status"]         = "Y";
                        dtSource_dtl.Rows[0]["voucher_nbr"]         = txtb_voucher_nbr.Text.ToString();
                        dtSource_dtl.Rows[0]["updated_by_user"]     = ViewState["updated_by_user"].ToString();
                        dtSource_dtl.Rows[0]["updated_dttm"]        = ViewState["updated_dttm"].ToString();
                    }
                    
                    scriptInsertUpdate = MyCmn.updatescript(dtSource_dtl);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    if (dtSource_dtl_brkdwn.Rows.Count <= 0)
                    {
                        txtb_employeename.BorderColor = Color.Red;
                        LblRequired1.Text = "You cannot save, input atleast one(1) Overtime Breakdown!";
                        return;
                    }

                    if (scriptInsertUpdate == string.Empty) {return; }
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") { return; }

                    
                    if (msg.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist","0");
                        return;
                    }


                    // *******************************************
                    // ** Delete Insert Function to Detail Table *
                    // *******************************************
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        dtSource_dtl_brkdwn.Columns.Remove("ot_perc_descr");
                        dtSource_dtl_brkdwn.Columns.Remove("post_status");
                        dtSource_dtl_brkdwn.Columns.Remove("post_status_descr");
                        dtSource_dtl_brkdwn.Columns.Remove("payroll_year_month");
                        dtSource_dtl_brkdwn.Columns.Remove("payroll_year");
                        dtSource_dtl_brkdwn.Columns.Remove("payroll_month");
                        string[] insert_empl_script = MyCmn.get_insertscript(dtSource_dtl_brkdwn).Split(';');
                        MyCmn.DeleteBackEndData(dtSource_dtl_brkdwn.TableName.ToString(), "WHERE empl_id ='" + txtb_empl_id.Text.ToString().Trim() + "' AND payroll_registry_nbr = '" + lbl_registry_number.Text.ToString().Trim() + "'");
                        for (int x = 0; x < insert_empl_script.Length; x++)
                        {
                            string insert_script = "";
                            insert_script = insert_empl_script[x];
                            MyCmn.insertdata(insert_script);
                        }
                    }
                    else if(saveRecord == MyCmn.CONST_ADD)
                    {
                        DeleteInsert_BreakDown();
                    }
                    // *******************************************
                    // ** Delete Insert Function to Detail Table *
                    // *******************************************

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                        nrow["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString();
                        nrow["empl_id"]                 = ddl_empl_id.SelectedValue.ToString().Trim();
                        nrow["monthly_rate"]            = txtb_monthly_rate.Text.ToString().ToString();
                        nrow["daily_rate"]              = txtb_daily_rate.Text.ToString().Trim();
                        nrow["hourly_rate"]             = txtb_hourly_rate_actual.Text.ToString().Trim();
                        nrow["hourly_rate_25"]          = txtb_hourly_rate_25.Text.ToString().Trim();
                        nrow["hourly_rate_50"]          = txtb_hourly_rate_50.Text.ToString().Trim();
                        nrow["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                        nrow["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                        nrow["ot_hour_reg"]             = txtb_hr_actual.Text.ToString().Trim();
                        nrow["ot_hour_25"]              = txtb_hr_25.Text.ToString().Trim();
                        nrow["ot_hour_50"]              = txtb_hr_50.Text.ToString().Trim();
                        nrow["bir_tax"]                 = txtb_bir_tax.Text.ToString().Trim();
                        nrow["remarks"]                 = txtb_remarks.Text.ToString().Trim();
                        nrow["bir_tax2"]                = txtb_tax2.Text.ToString().Trim();
                        nrow["bir_tax3"]                = txtb_tax3.Text.ToString().Trim();
                        nrow["bir_tax5"]                = txtb_tax5.Text.ToString().Trim();
                        nrow["bir_tax8"]                = txtb_tax8.Text.ToString().Trim();
                        nrow["bir_tax10"]               = txtb_tax10.Text.ToString().Trim();
                        nrow["bir_tax15"]               = txtb_tax15.Text.ToString().Trim();
                        nrow["employee_name"]           = ddl_empl_id.SelectedItem.ToString().Trim();
                        
                        // BEGIN - Add Field Again  - 06/20/2019
                        nrow["post_status"]             = "N";
                        nrow["post_status_descr"]       = "NOT POSTED";
                        nrow["voucher_nbr"]             = txtb_voucher_nbr.Text.ToString();
                        nrow["created_by_user"]         = Session["ep_user_id"].ToString();
                        nrow["updated_by_user"]         = "";
                        nrow["created_dttm"]            = DateTime.Now;
                        nrow["posted_by_user"]          = "";
                        nrow["date_posted"]             = "";
                        nrow["position_title1"]         = txtb_position.Text.ToString();
                        nrow["department_name1"]        = txtb_department_descr.Text.ToString();
                        nrow["updated_dttm"]            = Convert.ToDateTime("1900-01-01");
                        dataListGrid.Rows.Add(nrow);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND payroll_year='" + ddl_year.SelectedValue.ToString().Trim() + "' AND payroll_registry_nbr ='" + lbl_registry_number.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        
                        row2Edit[0]["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_registry_nbr"] = lbl_registry_number.Text.ToString();
                        row2Edit[0]["empl_id"]              = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["monthly_rate"]         = txtb_monthly_rate.Text.ToString().ToString();
                        row2Edit[0]["daily_rate"]           = txtb_daily_rate.Text.ToString().Trim();
                        row2Edit[0]["hourly_rate"]          = txtb_hourly_rate_actual.Text.ToString().Trim();
                        row2Edit[0]["hourly_rate_25"]       = txtb_hourly_rate_25.Text.ToString().Trim();
                        row2Edit[0]["hourly_rate_50"]       = txtb_hourly_rate_50.Text.ToString().Trim();
                        row2Edit[0]["gross_pay"]            = txtb_gross_pay.Text.ToString().Trim();
                        row2Edit[0]["net_pay"]              = txtb_net_pay.Text.ToString().Trim();
                        row2Edit[0]["ot_hour_reg"]          = txtb_hr_actual.Text.ToString().Trim();
                        row2Edit[0]["ot_hour_25"]           = txtb_hr_25.Text.ToString().Trim();
                        row2Edit[0]["ot_hour_50"]           = txtb_hr_50.Text.ToString().Trim();
                        row2Edit[0]["bir_tax"]              = txtb_bir_tax.Text.ToString().Trim();
                        row2Edit[0]["bir_tax2"]             = txtb_tax2.Text.ToString().Trim();
                        row2Edit[0]["bir_tax3"]             = txtb_tax3.Text.ToString().Trim();
                        row2Edit[0]["bir_tax5"]             = txtb_tax5.Text.ToString().Trim();
                        row2Edit[0]["bir_tax8"]             = txtb_tax8.Text.ToString().Trim();
                        row2Edit[0]["bir_tax10"]            = txtb_tax10.Text.ToString().Trim();
                        row2Edit[0]["bir_tax15"]            = txtb_tax15.Text.ToString().Trim();
                        row2Edit[0]["remarks"]              = txtb_remarks.Text.ToString().Trim();
                        row2Edit[0]["employee_name"]        = txtb_employeename.Text.ToString().Trim();

                        // BEGIN - Add Field Again  - 06/20/2019  
                        row2Edit[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                        row2Edit[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                        row2Edit[0]["created_dttm"]            = ViewState["created_dttm"].ToString() == "" ? DateTime.Now : ViewState["created_dttm"] ;
                        row2Edit[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        if (Session["ep_post_authority"].ToString() == "1")
                        {
                            row2Edit[0]["posted_by_user"]       = Session["ep_user_id"].ToString();
                            row2Edit[0]["date_posted"]          = txtb_date_posted.Text.ToString().Trim();
                            row2Edit[0]["post_status"]          = "Y";
                            row2Edit[0]["post_status_descr"]    = "POSTED";
                            row2Edit[0]["voucher_nbr"]          = txtb_voucher_nbr.Text.ToString();
                            row2Edit[0]["updated_by_user"]      = ViewState["updated_by_user"].ToString();
                            row2Edit[0]["updated_dttm"]         = ViewState["updated_dttm"].ToString();
                        }
                        
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                        }
                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                }

            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 03/12/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 03/12/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 03/12/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR position_title1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR department_name1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR post_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR net_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("daily_rate", typeof(System.String));
            dtSource1.Columns.Add("hourly_rate", typeof(System.String));
            dtSource1.Columns.Add("hourly_rate_25", typeof(System.String));
            dtSource1.Columns.Add("hourly_rate_50", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("ot_hour_reg", typeof(System.String));
            dtSource1.Columns.Add("ot_hour_25", typeof(System.String));
            dtSource1.Columns.Add("ot_hour_50", typeof(System.String));
            dtSource1.Columns.Add("bir_tax", typeof(System.String));
            dtSource1.Columns.Add("remarks", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("date_posted", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("department_name1", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));

            dtSource1.Columns.Add("employee_name", typeof(System.String));

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
            txtb_search.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search.Focus();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 04/02/2019 - Validation 
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL","0");
            if (ddl_empl_id.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id", "0");
                ddl_empl_id.Focus();
                validatedSaved = false;
                return validatedSaved;
            }
            if (CommonCode.checkisdecimal(txtb_hr_actual) == false)
            {
                FieldValidationColorChanged(true, "txtb_hr_actual", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_hr_25) == false)
            {
                FieldValidationColorChanged(true, "txtb_hr_25", "0");
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_hr_50) == false)
            {
                FieldValidationColorChanged(true, "txtb_hr_50", "0");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_bir_tax) == false)
            {
                FieldValidationColorChanged(true, "txtb_bir_tax", "0");
                validatedSaved = false;
            }
            /** GROSS PAY AND NET PAY VALIDATION **/
            if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_gross_pay", "0");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_net_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_pay", "0");
                validatedSaved = false;
            }
            // Required ang Vouvher number if Status is Y or Blank ug ang Post Authority is equal to 1 
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr","0");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }
            // Required ang Vouvher number if Status is Y or Blank ug ang Post Authority is equal to 1 
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr","");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_monthly_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_monthly_rate", "");
                txtb_monthly_rate.Focus();
                validatedSaved = false;
            }


            /**END OF GROSS PAY AND NET PAY VALIDATION**/

            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName, string amount)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_empl_id":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text = "already-exist";
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hr_actual":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hr_actual.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hr_25":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hr_25.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hr_50":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hr_50.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_voucher_nbr":
                        {
                            LblRequired200.Text = MyCmn.CONST_RQDFLD;
                            txtb_voucher_nbr.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_reason":
                        {
                            LblRequired201.Text = MyCmn.CONST_RQDFLD;
                            txtb_reason.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_monthly_rate":
                        {
                            LblRequired202.Text = MyCmn.CONST_RQDFLD;
                            txtb_monthly_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_ot_date":
                        {
                            LblRequired100.Text = "Invalid date format!";
                            txtb_ot_date.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_ot_date_not-equal-to-this-month":
                        {
                            LblRequired100.Text = "Must be " + ddl_month.SelectedItem + " " + ddl_year.SelectedValue;
                            txtb_ot_date.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_ot_hours":
                        {
                            LblRequired102.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_ot_hours.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_ot_perc":
                        {
                            LblRequired103.Text = MyCmn.CONST_RQDFLD;
                            ddl_ot_perc.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_ot_date-exist":
                        {
                            msg_icon.Attributes.Add("class", "fa fa-exclamation-triangle fa-5x text-warning");
                            msg_header.InnerText = "DATA NOT SAVED!";
                            lbl_details.Text = "You cannot save this date " + txtb_ot_date.Text + " from this employee, because this is already exist!";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
                            break;
                        }

                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired1.Text   = "";
                            LblRequired3.Text   = "";
                            LblRequired4.Text   = "";
                            LblRequired5.Text   = "";
                            LblRequired6.Text   = "";
                            LblRequired200.Text   = "";
                            LblRequired201.Text   = "";
                            LblRequired202.Text   = "";
                            LblRequired100.Text   = "";
                            LblRequired102.Text   = "";
                            LblRequired103.Text   = "";

                            txtb_employeename.BorderColor   = Color.LightGray;
                            ddl_empl_id.BorderColor         = Color.LightGray;
                            txtb_hr_actual.BorderColor      = Color.LightGray;
                            txtb_hr_25.BorderColor          = Color.LightGray;
                            txtb_hr_50.BorderColor          = Color.LightGray;
                            txtb_bir_tax.BorderColor        = Color.LightGray;
                            txtb_gross_pay.BorderColor      = Color.LightGray;
                            txtb_net_pay.BorderColor        = Color.LightGray;
                            txtb_voucher_nbr.BorderColor    = Color.LightGray;
                            txtb_reason.BorderColor    = Color.LightGray;
                            txtb_monthly_rate.BorderColor    = Color.LightGray;
                            txtb_ot_date.BorderColor    = Color.LightGray;
                            ddl_ot_perc.BorderColor    = Color.LightGray;
                            txtb_ot_hours.BorderColor    = Color.LightGray;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            updatepanel_ot_date.Update();
                            break;
                        }

                }
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Trigger When Select Employment Type
        //*************************************************************************
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_payroll_group.SelectedValue.ToString().Trim() != "" && ddl_year.SelectedValue.ToString().Trim() != "" && ddl_month.SelectedValue != "" && ddl_payroll_template.SelectedValue != "")
            {

                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }

            RetrieveDataListGrid();
            RetrieveEmployeename();
            UpdatePanel10.Update();
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Trigger When Select Payroll Year
        //*************************************************************************
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Trigger When Select Employee Name
        //*************************************************************************
        protected void ddl_empl_id_TextChanged(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL", "0");

            div_msg.Visible   = false;
            div_msg.Attributes.Add("class", "");
            hdr_msg.InnerText = "";
            dtl_msg.Text      = "";

            InitializeTable_brkdwn();
            dtSource_dtl_brkdwn.Columns.Add("ot_perc_descr", typeof(System.String));
            AddPrimaryKeys_brkdwn();

            foreach (DataRow nrow1 in dtSource_dtl_brkdwn.Rows)
            {
                nrow1["action"] = 1;
                nrow1["retrieve"] = false;
            }

            MyCmn.Sort(gv_dataListGrid_brkdwn, dtSource_dtl_brkdwn, "ot_date", Session["SortOrder"].ToString());
            up_dataListGrid_brkdwn.Update();
            ClearEntry();
            if (ddl_empl_id.SelectedValue.ToString().Trim() != "")
            {
                initialized_header();
               calculate_hourly25_50();
               calculate_grosspays();
                // calculate_tax_re_ce();
                Calculate_Taxes_JO();
               calculate_netpays();
                RetrieveDataListGrid_Oth();
            }
            else
            {
                ClearEntry();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Trigger When Select Payroll Year
        //*************************************************************************
        protected void initialized_header()
        {
            //double hours_25 = 0;
            //double hours_50 = 0;

            DataRow[] selected_employee = dtSource_for_names.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");
            
            txtb_monthly_rate.Text       = selected_employee[0]["monthly_rate"].ToString();
            txtb_daily_rate.Text         = double.Parse(selected_employee[0]["daily_rate"].ToString()).ToString("###,##0.00");
            txtb_empl_id.Text            = selected_employee[0]["empl_id"].ToString();

            // txtb_hourly_rate_actual.Text = double.Parse(selected_employee[0]["hourly_rate"].ToString()).ToString("###,##0.00");
            // switch (ddl_empl_type.SelectedValue)
            // {
            //     case "CE":
            //         hours_25 = (double.Parse(txtb_hourly_rate_actual.Text.ToString()) * .25) + double.Parse(txtb_hourly_rate_actual.Text.ToString());
            //         txtb_hourly_rate_25.Text = hours_25.ToString("###,##0.000");
            //         hours_50 = (double.Parse(txtb_hourly_rate_actual.Text.ToString()) * .50) + double.Parse(txtb_hourly_rate_actual.Text.ToString());
            //         txtb_hourly_rate_50.Text = hours_50.ToString("###,##0.000");
            //         break;
            //     case "RE":
            //         hours_25 = (double.Parse(txtb_hourly_rate_actual.Text.ToString()) * .25) + double.Parse(txtb_hourly_rate_actual.Text.ToString());
            //         txtb_hourly_rate_25.Text = hours_25.ToString("###,##0.00");
            //         hours_50 = (double.Parse(txtb_hourly_rate_actual.Text.ToString()) * .50) + double.Parse(txtb_hourly_rate_actual.Text.ToString());
            //         txtb_hourly_rate_50.Text = hours_50.ToString("###,##0.00");
            //         break;
            // }
            
            txtb_position.Text            = selected_employee[0]["position_title1"].ToString();
            txtb_department_descr.Text    = selected_employee[0]["department_name1"].ToString();
            txtb_bir_tax_rate_perc.Text   = selected_employee[0]["tax_rate"].ToString();

            ViewState["department_code"]  = selected_employee[0]["department_code"].ToString();
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Trigger When Click Calcukate
        //*************************************************************************
        protected void btn_calculate_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                calculate_hourly25_50();
                calculate_grosspays();
                calculate_tax_re_ce();
                Calculate_Taxes_JO();
                calculate_netpays();
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Calculate Gross Pay
        //*************************************************************************
        private void calculate_grosspays()
        {
            double total_hr_actual  = 0;
            double total_hr_25      = 0;
            double total_hr_50      = 0;

            double gross_pay        = 0;

            switch (ddl_empl_type.SelectedValue)
            {
                
                //case "CE":
                //    total_hr_actual = double.Parse(txtb_hourly_rate_actual.Text.ToString()) * double.Parse(txtb_hr_actual.Text.ToString());
                //    total_hr_25     = double.Parse((double.Parse(txtb_hourly_rate_25.Text.ToString()) * double.Parse(txtb_hr_25.Text.ToString())).ToString("###,##0.00"));
                //    total_hr_50     = double.Parse((double.Parse(txtb_hourly_rate_50.Text.ToString()) * double.Parse(txtb_hr_50.Text.ToString())).ToString("###,##0.00"));
                //    gross_pay       = total_hr_actual + total_hr_25 + total_hr_50;

                //    // OLD COMPUTATION : Update : VJA - 11/19/2019
                //    string total_str_gross_pay = "";
                //    total_str_gross_pay = gross_pay.ToString("###,##0.0000");
                //    txtb_gross_pay.Text = total_str_gross_pay.Split('.')[0] + "." + total_str_gross_pay.Split('.')[1].Substring(0, 2);

                //    // NEW COMPUTATION 
                //    //txtb_gross_pay.Text = gross_pay.ToString("###,##0.000");
                //    break;

                case "RE":
                case "CE":
                  
                    total_hr_actual = double.Parse(txtb_hourly_rate_actual.Text.ToString()) * double.Parse(txtb_hr_actual.Text.ToString());
                    total_hr_25 = double.Parse((double.Parse(txtb_hourly_rate_25.Text.ToString()) * double.Parse(txtb_hr_25.Text.ToString())).ToString("###,##0.00"));
                    total_hr_50 = double.Parse((double.Parse(txtb_hourly_rate_50.Text.ToString()) * double.Parse(txtb_hr_50.Text.ToString())).ToString("###,##0.00"));
                    gross_pay = total_hr_actual + total_hr_25 + total_hr_50;
                    txtb_gross_pay.Text = gross_pay.ToString("###,##0.00");
                    break;

                case "JO":
                    // Computation for JO - Is (Daily Rate / 8) * Actual Rendered hour  - Ana Si Maam Meldrid - 2020-11-14
                    gross_pay = (double.Parse(txtb_daily_rate.Text.ToString()) / 8) * double.Parse(txtb_hr_actual.Text.ToString());
                    txtb_gross_pay.Text = gross_pay.ToString("###,##0.00");
                    break;
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Calculate Net Pays
        //*************************************************************************
        private void calculate_netpays()
        {
            double net_pay = 0;
            double bir_taxes = 0;

            switch (ddl_empl_type.SelectedValue)
            {
                case "RE":
                case "CE":
                    bir_taxes = double.Parse(txtb_bir_tax.Text);
                    net_pay = double.Parse(txtb_gross_pay.Text) - bir_taxes;
                    break;

                case "JO":
                    bir_taxes = bir_taxes +  double.Parse(txtb_tax2.Text);
                    bir_taxes = bir_taxes +  double.Parse(txtb_tax3.Text);
                    bir_taxes = bir_taxes +  double.Parse(txtb_tax5.Text);
                    bir_taxes = bir_taxes +  double.Parse(txtb_tax8.Text);
                    bir_taxes = bir_taxes +  double.Parse(txtb_tax10.Text);
                    bir_taxes = bir_taxes +  double.Parse(txtb_tax15.Text);
                    net_pay = double.Parse(txtb_gross_pay.Text) - bir_taxes;
                    break;
            }
            txtb_net_pay.Text = net_pay.ToString("###,##0.00");
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            txtb_monthly_rate.Enabled = ifenable;
            txtb_daily_rate.Enabled = ifenable;
            txtb_hourly_rate_actual.Enabled = ifenable;
            txtb_gross_pay.Enabled = ifenable;
            // txtb_hr_actual.Enabled           =  ifenable;
            // txtb_hr_25.Enabled               =  ifenable;
            // txtb_hr_50.Enabled               =  ifenable;
            txtb_bir_tax.Enabled             =  ifenable;
            txtb_tax2.Enabled                =  ifenable;
            txtb_tax3.Enabled                =  ifenable;
            txtb_tax5.Enabled                =  ifenable;
            txtb_tax8.Enabled                =  ifenable;
            txtb_tax10.Enabled               =  ifenable;
            txtb_tax15.Enabled               =  ifenable;
            txtb_remarks.Enabled             =  ifenable;

            btn_save_brkdwn.Visible             = ifenable;
            id_options.Visible                  = ifenable;
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleDisplay_AddEdit_RECE_JO()
        {
            if (ddl_empl_type.SelectedValue == "CE" || ddl_empl_type.SelectedValue == "RE")
            {
                div_hourly_rate_re_ce.Visible   = true;
                div_ot_hour_re_ce.Visible       = true;
                div_tax_re_ce.Visible           = true;
                div_tax_jo.Visible              = false;
                ddl_ot_perc.Enabled             = true;
            }
            else
            {
                div_hourly_rate_re_ce.Visible   = false;
                div_ot_hour_re_ce.Visible       = false;
                div_tax_re_ce.Visible           = false;
                div_tax_jo.Visible              = true;
                ddl_ot_perc.Enabled             = false;
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Calculate Net Pays
        //*************************************************************************
        private void calculate_hourly25_50()
        {
            double daily_rate = 0;
            double hourly_rate = 0;
            double hours_25 = 0;
            double hours_50 = 0;


            //if (ddl_empl_type.SelectedValue == "CE")
            //{
            //    daily_rate = double.Parse(txtb_monthly_rate.Text) / 22;
            //    hourly_rate = daily_rate / double.Parse(hidden_hours_in_days.Value);
            //    // Casual Computation : Dle E Round of and Daily Rate : Get the 3 Decimal Point and Drop the 3rd
            //    // Date Updated       : 07/30/2019

            //    // Ge Drop Lang Ang Ika Duha nga Decimal Point
            //    string total_str_daily_rate = "";
            //    total_str_daily_rate = daily_rate.ToString("###,##0.0000");
            //    txtb_daily_rate.Text = total_str_daily_rate.Split('.')[0] + "." + total_str_daily_rate.Split('.')[1].Substring(0, 2);

            //    // Ge Drop Lang Ang Ika tulo nga Decimal Point
            //    string total_str_hourly_rate_actual = "";
            //    total_str_hourly_rate_actual = hourly_rate.ToString("###,##0.00000");
            //    txtb_hourly_rate_actual.Text = total_str_hourly_rate_actual.Split('.')[0] + "." + total_str_hourly_rate_actual.Split('.')[1].Substring(0, 3);

            //    // Ge Drop Lang Ang Ika tulo nga Decimal Point
            //    hours_25 = (hourly_rate * .25) + hourly_rate;
            //    string total_str_hourly_rate_25 = "";
            //    total_str_hourly_rate_25 = hours_25.ToString("###,##0.00000");
            //    txtb_hourly_rate_25.Text = total_str_hourly_rate_25.Split('.')[0] + "." + total_str_hourly_rate_25.Split('.')[1].Substring(0, 3);

            //    // Update : VJA - 11/22/2019 - Ge Round Off Nila ang 50% sa Printed
            //     hours_50 = (hourly_rate * .50) + hourly_rate;
            //     txtb_hourly_rate_50.Text = hours_50.ToString("###,##0.000");


            //    // Ge Drop Lang Ang Ika tulo nga Decimal Point
            //    // hours_50 = (hourly_rate * .50) + hourly_rate;
            //    // string total_str_hourly_rate_50 = "";
            //    // total_str_hourly_rate_50 = hours_50.ToString("###,##0.00000");
            //    // txtb_hourly_rate_50.Text = total_str_hourly_rate_50.Split('.')[0] + "." + total_str_hourly_rate_50.Split('.')[1].Substring(0, 3);


            //}
            //else
            //{
                daily_rate = (ddl_empl_type.SelectedValue == "JO" || ddl_empl_type.SelectedValue == "CE" ? double.Parse(txtb_daily_rate.Text) : double.Parse(txtb_monthly_rate.Text) / 22);
                txtb_daily_rate.Text = daily_rate.ToString("###,##0.00");

                hourly_rate = double.Parse(txtb_daily_rate.Text) / double.Parse(hidden_hours_in_days.Value);
                txtb_hourly_rate_actual.Text = hourly_rate.ToString("###,##0.00");

                hours_25 = (double.Parse(hourly_rate.ToString()) * .25) + double.Parse(hourly_rate.ToString());
                txtb_hourly_rate_25.Text = hours_25.ToString("###,##0.00");

                hours_50 = (double.Parse(hourly_rate.ToString()) * .50) + double.Parse(hourly_rate.ToString());
                txtb_hourly_rate_50.Text = hours_50.ToString("###,##0.00");
            //}
            
        }
        
        

        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Initialized datasource fields/columns - Details
        //*************************************************************************
        private void InitializeTable_brkdwn()
        {
            dtSource_dtl_brkdwn = new DataTable();
            dtSource_dtl_brkdwn.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource_dtl_brkdwn.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl_brkdwn.Columns.Add("ot_date", typeof(System.String));
            dtSource_dtl_brkdwn.Columns.Add("hours_rendered", typeof(System.String));
            dtSource_dtl_brkdwn.Columns.Add("ot_perc", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add Primary Key Field to datasource - Details
        //*************************************************************************
        private void AddPrimaryKeys_brkdwn()
        {
            dtSource_dtl_brkdwn.TableName = "payrollregistry_dtl_ovtm_brkdwn_tbl";
            dtSource_dtl_brkdwn.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl_brkdwn.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id","ot_date" };
            dtSource_dtl_brkdwn = MyCmn.AddPrimaryKeys(dtSource_dtl_brkdwn, col);
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow_brkdwn()
        {
            DataRow nrow = dtSource_dtl_brkdwn.NewRow();

            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["ot_date"] = string.Empty;
            nrow["hours_rendered"] = string.Empty;
            nrow["ot_perc"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource_dtl_brkdwn.Rows.Add(nrow);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/11/2019 - Add Primary Key Field to datasource - Details
        //*************************************************************************
        protected void btn_save_brkdwn_Click(object sender, EventArgs e)
        {
            string saveRecord = ViewState["AddEdit_Mode_BrkDwn"].ToString();
            FieldValidationColorChanged(false, "ALL","");
            if (IsDataValidated2())
            {
                //*************************************************************************
                //  BEGIN - VJA- 2020/05/21 - Check the Date if already exist
                //*************************************************************************
                if (saveRecord == "BRK_ADD")
                {
                    try
                    {
                        DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_ovtm_brkdwn_chck", "par_empl_id", txtb_empl_id.Text.ToString(), "par_ot_date", txtb_ot_date.Text.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["return_flag"].ToString().Trim() == "Y")
                            {
                                msg_icon.Attributes.Add("class", "fa fa-exclamation-triangle fa-5x text-warning");
                                msg_header.InnerText = "DATA NOT SAVED!";
                                lbl_details.Text = "You cannot save this date " + txtb_ot_date.Text + " from this employee ("+ dt.Rows[0]["employee_name"].ToString().Trim() + "), because this is already exist ! --> (" + dt.Rows[0]["payroll_year_month"].ToString().Trim() + ") - " + dt.Rows[0]["payroll_registry_nbr"].ToString().Trim() + " - " + dt.Rows[0]["payroll_registry_descr"].ToString().Trim();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
                                return;
                            }  
                        }
                    }
                        catch (Exception)
                    {
                        msg_icon.Attributes.Add("class", "fa fa-exclamation-triangle fa-5x text-warning");
                        msg_header.InnerText = "SP NOT FOUND";
                        lbl_details.Text = "Stored Procedure not found, please contact to the programmer";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openNotification();", true);
                        return;
                    }
                }
                //*************************************************************************
                //  END - VJA- 2020/05/21 - Check the Date if already exist
                //*************************************************************************

                try
                {

                    if (saveRecord == "BRK_ADD")
                    {
                        DataRow nrow1 = dtSource_dtl_brkdwn.NewRow();
                        nrow1["empl_id"]                = txtb_empl_id.Text.ToString().Trim();
                        nrow1["payroll_registry_nbr"]   = lbl_registry_number.Text.ToString().Trim();
                        nrow1["ot_date"]                = txtb_ot_date.Text.ToString().Trim();
                        nrow1["hours_rendered"]         = txtb_ot_hours.Text.ToString().Trim();
                        nrow1["ot_perc"]                = ddl_ot_perc.SelectedValue.ToString().Trim();
                        nrow1["ot_perc_descr"]          = ddl_ot_perc.SelectedItem.ToString().Trim();
                        nrow1["action"] = 1;
                        nrow1["retrieve"] = false;
                        dtSource_dtl_brkdwn.Rows.Add(nrow1);
                        // gv_dataListGrid_brkdwn.Rows.Add(nrow1);

                        txtb_ot_date.Enabled = true;
                        ViewState.Add("AddEdit_Mode_BrkDwn", "BRK_ADD");

                        div_msg.Visible     = true;
                        div_msg.Attributes.Add("class", "alert alert-primary alert-dismissible fade show small");
                        hdr_msg.InnerText   = "Successfully Added!";
                        dtl_msg.Text        = " The Record successfully saved/added ";
                    }
                    else if (saveRecord == "BRK_EDIT")
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text + "' AND payroll_registry_nbr = '" + lbl_registry_number.Text + "'  AND ot_date = '" + txtb_ot_date.Text + "'";
                        DataRow[] row2Edit = dtSource_dtl_brkdwn.Select(editExpression);

                        row2Edit[0]["hours_rendered"]   = txtb_ot_hours.Text.ToString().Trim();
                        row2Edit[0]["ot_perc"]          = ddl_ot_perc.SelectedValue.ToString().Trim();
                        row2Edit[0]["ot_perc_descr"]    = ddl_ot_perc.SelectedItem.ToString().Trim();
                        
                        txtb_ot_date.Enabled = true;
                        ViewState.Add("AddEdit_Mode_BrkDwn", "BRK_ADD");

                        div_msg.Visible     = true;
                        div_msg.Attributes.Add("class", "alert alert-success alert-dismissible fade show small");
                        hdr_msg.InnerText   = "Successfully Updated!";
                        dtl_msg.Text        = " The Record successfully saved/updated ";
                    }

                    MyCmn.Sort(gv_dataListGrid_brkdwn, dtSource_dtl_brkdwn, "ot_date", "ASC");
                    up_dataListGrid_brkdwn.Update();


                    calculate_breakdown();

                }
                catch (Exception)
                {
                    FieldValidationColorChanged(true, "txtb_ot_date-exist", "");
                }
            }
            
        }

        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Validation 
        //*************************************************************************
        private bool IsDataValidated2()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL", "0");
            
            if (CommonCode.checkisdatetime(txtb_ot_date) == false)
            {
                FieldValidationColorChanged(true, "txtb_ot_date", "0");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_ot_hours) == false)
            {
                FieldValidationColorChanged(true, "txtb_ot_hours", "0");
                validatedSaved = false;
            }
            if (ddl_ot_perc.SelectedValue.ToString() == "")
            {
                FieldValidationColorChanged(true, "ddl_ot_perc", "0");
                validatedSaved = false;
            }

            if (txtb_ot_date.Text.ToString().Trim() != "") 
            { 
                if (int.Parse(ddl_year.SelectedValue) != DateTime.Parse(txtb_ot_date.Text).Year ||
                    int.Parse(ddl_month.SelectedValue) != DateTime.Parse(txtb_ot_date.Text).Month)
                {
                    FieldValidationColorChanged(true, "txtb_ot_date_not-equal-to-this-month", "");
                    txtb_ot_date.Focus();
                    validatedSaved = false;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
            updatepanel_ot_date.Update();
            return validatedSaved;
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command_brckdwn(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND ot_date = '" + svalues[1].ToString().Trim() + "' AND payroll_registry_nbr = '"+ svalues[2].ToString().Trim() + "'";
            DataRow[] row2Edit = dtSource_dtl_brkdwn.Select(editExpression);
            
            txtb_ot_date.Text = "";
            txtb_ot_hours.Text = "";

            txtb_ot_date.Text           = row2Edit[0]["ot_date"].ToString().Trim();
            txtb_ot_hours.Text          = row2Edit[0]["hours_rendered"].ToString().Trim();
            ddl_ot_perc.SelectedValue   = row2Edit[0]["ot_perc"].ToString().Trim();
            
            txtb_ot_date.Enabled = false;
            updatepanel_ot_date.Update();

            btn_save_brkdwn.Text = "Save/Update";
            btn_save_brkdwn.Attributes.Add("class", "btn btn-success btn-sm btn-block  save-icon icn text-danger");
            ViewState.Add("AddEdit_Mode_BrkDwn", "BRK_EDIT");
            
            FieldValidationColorChanged(false, "ALL", "0");
            
        }
        //******************************************************************************
        //  BEGIN - VJA - 10/19/2019 - Delete and Insert Method 
        //******************************************************************************
        protected void DeleteInsert_BreakDown()
        {
            dtSource_dtl_brkdwn.Columns.Remove("ot_perc_descr");
            // dtSource_dtl_brkdwn.Columns.Remove("post_status");
            // dtSource_dtl_brkdwn.Columns.Remove("post_status_descr");
            // dtSource_dtl_brkdwn.Columns.Remove("payroll_year_month");
            // dtSource_dtl_brkdwn.Columns.Remove("payroll_year");
            // dtSource_dtl_brkdwn.Columns.Remove("payroll_month");
            string[] insert_empl_script = MyCmn.get_insertscript(dtSource_dtl_brkdwn).Split(';');
            MyCmn.DeleteBackEndData(dtSource_dtl_brkdwn.TableName.ToString(), "WHERE empl_id ='" + txtb_empl_id.Text.ToString().Trim() + "' AND payroll_registry_nbr = '"+lbl_registry_number.Text.ToString().Trim()+"'");
            for (int x = 0; x < insert_empl_script.Length; x++)
            {
                string insert_script = "";
                insert_script = insert_empl_script[x];
                MyCmn.insertdata(insert_script);
            }
        }
        //***************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command_brckdwn(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string deleteExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND ot_date = '" + svalues[1].ToString().Trim() + "' AND payroll_registry_nbr = '" + svalues[2].ToString().Trim() + "'";

            div_msg.Visible = true;
            div_msg.Attributes.Add("class", "alert alert-danger alert-dismissible fade show small");
            hdr_msg.InnerText = "Successfully Deleted!";
            dtl_msg.Text = " The Record successfully deleted ";

            DataRow[] row2Delete = dtSource_dtl_brkdwn.Select(deleteExpression);
            dtSource_dtl_brkdwn.Rows.Remove(row2Delete[0]);
            dtSource_dtl_brkdwn.AcceptChanges();

            MyCmn.Sort(gv_dataListGrid_brkdwn, dtSource_dtl_brkdwn, "ot_date", "ASC");
            up_dataListGrid_brkdwn.Update();

            calculate_breakdown();
        }
        // *********************************************************************************
        // BEGIN - VJA- 2020/05/21 CALCULATE THE ACTUAL , 25 % AND 50 % HOURS **************
        // *********************************************************************************
        private void calculate_breakdown()
        {
            string whereclause = "empl_id = '" + txtb_empl_id.Text + "' AND payroll_registry_nbr = '" + lbl_registry_number.Text + "'";
            DataRow[] rowtoEditBrkDwn = dtSource_dtl_brkdwn.Select(whereclause);

            double actual = 0;
            double hours_25 = 0;
            double hours_50 = 0;
            for (int x = 0; x < dtSource_dtl_brkdwn.Rows.Count; x++)
            {
                if (double.Parse(dtSource_dtl_brkdwn.Rows[x]["ot_perc"].ToString().Trim()) == 100)
                {
                    actual += double.Parse(dtSource_dtl_brkdwn.Rows[x]["hours_rendered"].ToString().Trim());
                }
                if (double.Parse(dtSource_dtl_brkdwn.Rows[x]["ot_perc"].ToString().Trim()) == 125)
                {
                    hours_25 += double.Parse(dtSource_dtl_brkdwn.Rows[x]["hours_rendered"].ToString().Trim());
                }
                if (double.Parse(dtSource_dtl_brkdwn.Rows[x]["ot_perc"].ToString().Trim()) == 150)
                {
                    hours_50 += double.Parse(dtSource_dtl_brkdwn.Rows[x]["hours_rendered"].ToString().Trim());
                }

                // txtb_hr_actual.Text = actual.ToString().Trim();
                // txtb_hr_25.Text = hours_25.ToString().Trim();
                // txtb_hr_50.Text = hours_50.ToString().Trim();
            }

            txtb_hr_actual.Text = actual.ToString("###,##0.00").Trim();
            txtb_hr_25.Text     = hours_25.ToString("###,##0.00").Trim();
            txtb_hr_50.Text     = hours_50.ToString("###,##0.00").Trim();

            // This code is to check if the Breackdown Datatable is Blank (Kay iyang e zero niya ang amount sa tanan.
            //if (dtSource_dtl_brkdwn         == null || 
            //    dtSource_dtl_brkdwn.Rows    == null || 
            //    dtSource_dtl_brkdwn.DataSet == null)
            //{
            //    txtb_hr_actual.Text = actual.ToString().Trim();
            //    txtb_hr_25.Text     = hours_25.ToString().Trim();
            //    txtb_hr_50.Text     = hours_50.ToString().Trim();
            //}

            btn_save_brkdwn.Text = "Save/Add";
            btn_save_brkdwn.Attributes.Add("class", "btn btn-primary btn-sm btn-block  save-icon icn");
            txtb_ot_date.Text = "";
            txtb_ot_hours.Text = "";

            // calculate_hourly25_50();
            calculate_grosspays();
            calculate_tax_re_ce();
            Calculate_Taxes_JO();
            calculate_netpays();

            // calculate_hourly25_50();
            // calculate_grosspays();
            // calculate_tax_re_ce();
            // calculate_netpays();
        }
        // *********************************************************************************
        // BEGIN - VJA- 2020/05/21 Calculate Tax Rate for Regular and Casual  **************
        // *********************************************************************************
        private void calculate_tax_re_ce()
        {
            double bir_tax             = 0;
            // double bir_tax_rate_perc   = 0;
            
            switch (ddl_empl_type.SelectedValue.ToString())
            {
                case "RE":
                case "CE":

                    bir_tax           = double.Parse(txtb_gross_pay.Text) * (double.Parse(txtb_bir_tax_rate_perc.Text) / 100);
                    // bir_tax_rate_perc = double.Parse(txtb_bir_tax.Text) / double.Parse(txtb_gross_pay.Text) * 100;
                    break;

                case "JO":
                    bir_tax           = 0;
                    // bir_tax_rate_perc = 0;
                    break;
            }
            
            txtb_bir_tax.Text           = bir_tax.ToString("###,##0.00");
            // txtb_bir_tax_rate_perc.Text = bir_tax_rate_perc.ToString("###,##0.00");
        }

        protected void gv_dataListGrid_brkdwn_Sorting(object sender, GridViewSortEventArgs e)
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
            Session["SortField"] = "employee_name";
            Session["SortOrder"] = sortingDirection;

            MyCmn.Sort(gv_dataListGrid_brkdwn, dtSource_dtl_brkdwn, e.SortExpression, sortingDirection);
            up_dataListGrid_brkdwn.Update();
        }

        protected void gv_dataListGrid_brkdwn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid_brkdwn.PageIndex = e.NewPageIndex;

            MyCmn.Sort(gv_dataListGrid_brkdwn, dtSource_dtl_brkdwn, "ot_date", "ASC");
            up_dataListGrid_brkdwn.Update();
        }
        //***********************************************************************************
        //  BEGIN - VJA- 2020-09-26 - Calculate Taxes During Add and Calculate Individually
        //***********************************************************************************
        private void Calculate_Taxes_JO()
        {
            if (ddl_empl_type.SelectedValue.ToString() == "JO")
            {
                double tax2     = 0;
                double tax3     = 0;
                double tax5     = 0;
                double tax8     = 0;
                double tax10    = 0;
                double tax15    = 0;

                dataList_employee_tax       = MyCmn.RetrieveData("sp_payrollemployee_tax_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_department_code", ViewState["department_code"].ToString().Trim(), "par_include_history", "N");
                DataRow[] selected_employee = dataList_employee_tax.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");

                if (selected_employee.Length > 0)
                {
                    if (selected_employee[0]["rcrd_status"].ToString() == "A")
                    {
                        //**************************************************************
                        //*** VJA - Tax Computation for With and Without Sworn 
                        //**************************************************************

                        if (selected_employee[0]["with_sworn"].ToString() == "1" ||
                            selected_employee[0]["with_sworn"].ToString() == "True")
                        {
                            switch (selected_employee[0]["wi_sworn_perc"].ToString()) // With Sworn
                            {
                                case "2":
                                    tax2 = tax2 + (double.Parse(txtb_gross_pay.Text)) * .02;
                                    break;
                                //case "1":
                                //    tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .01;
                                //    break;
                                case "3":
                                    tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .03;
                                    break;
                                case "5":
                                    tax5 = tax5 + (double.Parse(txtb_gross_pay.Text)) * .05;
                                    break;
                                case "8":
                                    tax8 = tax8 + (double.Parse(txtb_gross_pay.Text)) * .08;
                                    break;
                                case "10":
                                    tax10 = tax10 + (double.Parse(txtb_gross_pay.Text)) * .10;
                                    break;
                                case "15":
                                    tax15 = tax15 + (double.Parse(txtb_gross_pay.Text)) * .15;
                                    break;
                            }
                        }
                        else if (selected_employee[0]["with_sworn"].ToString() == "0" ||
                                selected_employee[0]["with_sworn"].ToString() == "False")
                        {
                            switch (selected_employee[0]["wo_sworn_perc"].ToString()) // Without Sworn
                            {
                                case "2":
                                    tax2 = tax2 + (double.Parse(txtb_gross_pay.Text)) * .02;
                                    break;
                                //case "1":
                                //    tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .01;
                                //    break;
                                case "3":
                                    tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .03;
                                    break;
                                case "5":
                                    tax5 = tax5 + (double.Parse(txtb_gross_pay.Text)) * .05;
                                    break;
                                case "8":
                                    tax8 = tax8 + (double.Parse(txtb_gross_pay.Text)) * .08;
                                    break;
                                case "10":
                                    tax10 = tax10 + (double.Parse(txtb_gross_pay.Text)) * .10;
                                    break;
                                case "15":
                                    tax15 = tax15 + (double.Parse(txtb_gross_pay.Text)) * .15;
                                    break;
                            }
                        }
                        // **************************************************************
                        // *** VJA - Compute VAT
                        // **************************************************************
                        switch (selected_employee[0]["tax_perc"].ToString())
                        {
                            case "2":
                                tax2 = tax2 + (double.Parse(txtb_gross_pay.Text)) * .02;
                                break;
                            //case "1":
                            //    tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .01;
                            //    break;
                            case "3":
                                tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .03;
                                break;
                            case "5":
                                tax5 = tax5 + (double.Parse(txtb_gross_pay.Text)) * .05;
                                break;
                            case "8":
                                tax8 = tax8 + (double.Parse(txtb_gross_pay.Text)) * .08;
                                break;
                            case "10":
                                tax10 = tax10 + (double.Parse(txtb_gross_pay.Text)) * .10;
                                break;
                            case "15":
                                tax15 = tax15 + (double.Parse(txtb_gross_pay.Text)) * .15;
                                break;
                        }

                        // **************************************************************
                        // *** VJA - Compute VAT
                        // **************************************************************

                        switch (selected_employee[0]["vat_perc"].ToString())
                        {
                            case "2":
                                tax2 = tax2 + (double.Parse(txtb_gross_pay.Text)) * .02;
                                break;
                            //case "1":
                            //    tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .01;
                            //    break;
                            case "3":
                                tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .03;
                                break;
                            case "5":
                                tax5 = tax5 + (double.Parse(txtb_gross_pay.Text)) * .05;
                                break;
                            case "8":
                                tax8 = tax8 + (double.Parse(txtb_gross_pay.Text)) * .08;
                                break;
                            case "10":
                                tax10 = tax10 + (double.Parse(txtb_gross_pay.Text)) * .10;
                                break;
                            case "15":
                                tax15 = tax15 + (double.Parse(txtb_gross_pay.Text)) * .15;
                                break;
                        }
                    }
                }

                txtb_tax2.Text   = tax2.ToString("###,##0.00").Trim();
                txtb_tax3.Text   = tax3.ToString("###,##0.00").Trim();
                txtb_tax5.Text   = tax5.ToString("###,##0.00").Trim();
                txtb_tax8.Text   = tax8.ToString("###,##0.00").Trim();
                txtb_tax10.Text  = tax10.ToString("###,##0.00").Trim();
                txtb_tax15.Text  = tax15.ToString("###,##0.00").Trim();
            }

        }
        //***********************************************************************************
        //** BEGIN - VJA- 2022-08-08 - Calculate OT and Generate OT coming from DTR  ********
        //***********************************************************************************
        protected void btn_generate_ovtm_Click(object sender, EventArgs e)
        {
            if (dtSource_dtl_brkdwn.Rows.Count <= 0) 
            {
                Generate_OTfromDTR();
            }
            else 
            {
                // ************************************
                // ** Already Have Date Breakdown *****
                // ************************************
                div_msg.Visible   = true;
                div_msg.Attributes.Add("class", "alert alert-danger alert-dismissible fade show small");
                hdr_msg.InnerText = "You cannot generate this Employee!";
                dtl_msg.Text      = " Already have an Date breakdown, delete the date and add manually instead!";
            }
        }
        //***********************************************************************************
        //** BEGIN - VJA- 2022-08-08 -  Generate OT coming from DTR  ************************
        //***********************************************************************************
        private void Generate_OTfromDTR() 
        { 
            DataTable dt = MyCmn.RetrieveData_to_aats("sp_generate_ovtm", "p_year", ddl_year.SelectedValue.ToString().Trim(), "p_month", ddl_month.SelectedValue.ToString().Trim(), "p_empl_id", txtb_empl_id.Text.ToString().Trim());

            if (dt.Rows.Count > 0) 
            { 
                var ot_perc       = "";
                var ot_perc_descr = "";
                switch (ddl_empl_type.SelectedValue.ToString().Trim())
                {
                    case "RE":
                        ot_perc       = "150";
                        ot_perc_descr = "150% - Actual Hours";
                        break;
                    case "CE":
                        ot_perc       = "150";
                        ot_perc_descr = "150% - Actual Hours";
                        break;
                    case "JO":
                        ot_perc       = "100";
                        ot_perc_descr = "100% - Actual Hours";
                        break;
                }

                InitializeTable_brkdwn();
                dtSource_dtl_brkdwn.Columns.Add("ot_perc_descr", typeof(System.String));
                AddPrimaryKeys_brkdwn();

                foreach (DataRow nrow1 in dtSource_dtl_brkdwn.Rows)
                {
                    nrow1["action"] = 1;
                    nrow1["retrieve"] = false;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow nrow1                   = dtSource_dtl_brkdwn.NewRow();
                    nrow1["empl_id"]                = dt.Rows[i]["empl_id"].ToString();
                    nrow1["payroll_registry_nbr"]   = lbl_registry_number.Text.ToString().Trim();
                    nrow1["ot_date"]                = dt.Rows[i]["dtr_date"].ToString();
                    nrow1["hours_rendered"]         = dt.Rows[i]["time_ot_payable"].ToString();
                    nrow1["ot_perc"]                = ot_perc;
                    nrow1["ot_perc_descr"]          = ot_perc_descr;
                    nrow1["action"]                 = 1;
                    nrow1["retrieve"]               = false;
                    dtSource_dtl_brkdwn.Rows.Add(nrow1);
                }

                //txtb_hr_actual.Text = dt.Rows[0]["sum_time_ot_payable"].ToString();
                txtb_remarks.Text   = dt.Rows[0]["dtr_date_concat"].ToString();

                div_msg.Visible     = true;
                div_msg.Attributes.Add("class", "alert alert-primary alert-dismissible fade show small");
                hdr_msg.InnerText   = "Successfully Generated!";
                dtl_msg.Text        = " The Record successfully generated OT from DTR";

                calculate_breakdown();

                MyCmn.Sort(gv_dataListGrid_brkdwn, dtSource_dtl_brkdwn, "ot_date", "ASC");
                up_dataListGrid_brkdwn.Update();
            }
            else 
            {
                div_msg.Visible   = true;
                div_msg.Attributes.Add("class", "alert alert-danger alert-dismissible fade show small");
                hdr_msg.InnerText = "Not Generated!";
                dtl_msg.Text      = " No data found from DTR or this date/s is already payrolled";
            }
        }

        //*************************************************************************
        //  BEGIN - VJA- 04/02/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid_Oth()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_ovtm_brkdwn_tbl_list", "par_payroll_registry_nbr", "", "par_empl_id", txtb_empl_id.Text.ToString().Trim(), "par_ot_year",ddl_year.SelectedValue.ToString().Trim());
            
            MyCmn.Sort(gv_dataListGrid_oth, dt, "ot_date", Session["SortOrder"].ToString());
            up_dataListGrid_oth.Update();
        }
        //**************************************************************************
        //  END OF THE CODE
        //*************************************************************************
    }
}