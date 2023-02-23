//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     01/17/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View
{
    public partial class cPaySalaryDifferential : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 05/23/2019 - Data Place holder creation 
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

        //********************************************************************
        //  BEGIN - VJA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 05/23/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "payroll_registry_nbr";
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
                ViewState["page_allow_add"]             = Session["page_allow_add_from_registry"];
                ViewState["page_allow_delete"]          = Session["page_allow_delete_from_registry"];
                ViewState["page_allow_edit"]            = Session["page_allow_edit_from_registry"];
                ViewState["page_allow_edit_history"]    = Session["page_allow_edit_history_from_registry"];
                ViewState["page_allow_print"]           = Session["page_allow_print_from_registry"];


                //********************************************************************
                //  BEGIN - VJA- 06/20/2019 - This is Session Variable Coming From Login
                //********************************************************************
                if (Session["ep_post_authority"].ToString() == "1")
                {
                    ViewState["page_allow_add"] = 0;
                    ViewState["page_allow_delete"] = 1;
                    ViewState["page_allow_edit"] = 1;
                    ViewState["page_allow_print"] = 1;
                }
                else
                {
                    ViewState["page_allow_add"] = 1;
                    ViewState["page_allow_delete"] = 1;
                    ViewState["page_allow_edit"] = 1;
                    ViewState["page_allow_print"] = 0;
                }


                if (Session["PreviousValuesonPage_cPayRegistry"] == null)
                    Session["PreviousValuesonPage_cPayRegistry"] = "";
                else if (Session["PreviousValuesonPage_cPayRegistry"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    //RetrieveEmploymentType();
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_month.SelectedValue = prevValues[1].ToString();
                    getTheLastdayoftheMonth();
                    ddl_empl_type.SelectedValue = prevValues[2].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue = prevValues[3].ToString();
                    RetriveGroupings();
                    ddl_payroll_group.SelectedValue = prevValues[7].ToString();
                    lbl_registry_number.Text = prevValues[7].ToString();
                    RetrieveDataListGrid();
                    RetrieveEmployeename();

                    RetrieveInstallation();
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
        //  BEGIN - VJA- 05/23/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            
            Session["sortdirection"] = SortDirection.Ascending.ToString();

            RetrieveYear();
            RetriveTemplate();
            RetriveGroupings();
            RetrieveDataListGrid();

            //Retrieve When Add
            RetrieveBindingDep();
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveEmployeename();
            RetrieveBindingFundcharges();
            
            btnAdd.Visible = true;

        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Get The Last Day of The Selected Month
        //*************************************************************************
        private void getTheLastdayoftheMonth()
        {
            DateTime last_Date = new DateTime(Convert.ToInt32(ddl_year.SelectedValue.ToString()), Convert.ToInt32(ddl_month.SelectedValue.ToString()), 1).AddMonths(1).AddDays(-1);
            int lastday = Convert.ToInt32(last_Date.Day.ToString());
            lbl_lastday_hidden.Text = lastday.ToString();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Retrieve From Installation Table
        //*************************************************************************
        private void RetrieveInstallation()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollinstallation_tbl_list", "par_year",ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_include_history","N");
            lbl_installation_monthly_conv_hidden.Text = dt.Rows[0]["monthly_salary_days_conv"].ToString();
            lbl_minimum_netpay_hidden.Text = dt.Rows[0]["minimum_net_pay"].ToString();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveEmployeename()
        {
            ddl_empl_id.Items.Clear();
            dtSource_for_names = MyCmn.RetrieveData("sp_personnelnames_combolist_diff", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR());

            ddl_empl_id.DataSource = dtSource_for_names;

            ddl_empl_id.DataValueField  = "empl_id";
            ddl_empl_id.DataTextField   = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }

        private void RetrieveBindingDep()
        {
            ddl_dep.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_departments_tbl_list", "par_include_history", "N");

            ddl_dep.DataSource = dt;
            ddl_dep.DataValueField = "department_code";
            ddl_dep.DataTextField = "department_name1";
            ddl_dep.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_dep.Items.Insert(0, li);
        }
        private void RetrieveBindingSubDep()
        {
            ddl_subdep.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_subdepartments_tbl_list");

            ddl_subdep.DataSource = dt;
            ddl_subdep.DataValueField = "subdepartment_code";
            ddl_subdep.DataTextField = "subdepartment_short_name";
            ddl_subdep.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_subdep.Items.Insert(0, li);
        }
        private void RetrieveBindingDivision()
        {
            ddl_division.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_divisions_tbl_combolist", "par_department_code", ddl_dep.SelectedValue.ToString(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString());

            ddl_division.DataSource = dt;
            ddl_division.DataValueField = "division_code";
            ddl_division.DataTextField = "division_name1";
            ddl_division.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_division.Items.Insert(0, li);
        }
        private void RetrieveBindingSection()
        {
            ddl_section.Items.Clear();
            DataTable dt1 = MyCmn.RetrieveData("sp_sections_tbl_combolist", "par_department_code", ddl_dep.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString(), "par_division_code", ddl_division.SelectedValue.ToString().Trim());

            ddl_section.DataSource = dt1;
            ddl_section.DataValueField = "section_code";
            ddl_section.DataTextField = "section_name1";
            ddl_section.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_section.Items.Insert(0, li);
        }

        private void RetrieveBindingFundcharges()
        {
            ddl_fund_charges.Items.Clear();
            DataTable dt1 = MyCmn.RetrieveData("sp_fundcharges_tbl_list");

            ddl_fund_charges.DataSource = dt1;
            ddl_fund_charges.DataValueField = "fund_code";
            ddl_fund_charges.DataTextField = "fund_description";
            ddl_fund_charges.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_fund_charges.Items.Insert(0, li);
        }
        
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
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
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveGroupings()
        {
            ddl_payroll_group.Items.Clear();
            dtGroup = MyCmn.RetrieveData("sp_payrollregistry_hdr_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month",ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

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
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
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
        //  BEGIN - VJA- 05/23/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            GetRegistry_NBR();
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_dtl_diff_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", lbl_registry_number.Text.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        
        //*************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();

            // For Detials Initialized, Add Primary Keys, Add new Row
            GetRegistry_NBR();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            RetrieveEmployeename();

            txtb_voucher_nbr.Enabled = false;
            lbl_if_dateposted_yes.Text = "";
            ToogleTextbox(true);

            btnSave.Visible = true;
            ddl_empl_id.Enabled = true;
            txtb_employeename.Visible = false;
            ddl_empl_id.Visible = true;
            btn_calculate.Visible = true;

            LabelAddEdit.Text = "Add New Record | Registry No: " + lbl_registry_number.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
            FieldValidationColorChanged(false, "ALL");
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            
        }
        private string GetRegistry_NBR()
        {
            string group_nbr = "";
            if (ddl_payroll_group.SelectedValue.ToString().Trim() !="")
            {
                lbl_registry_number.Text = ddl_payroll_group.SelectedValue.ToString();
                if (dtGroup != null && dtGroup.Rows.Count > 0)
                {
                    DataRow[] registry_nbr = dtGroup.Select("payroll_registry_nbr='"+ddl_payroll_group.SelectedValue.ToString()+"'");
                    group_nbr = registry_nbr[0]["payroll_group_nbr"].ToString().Trim();
                }
            }
            return group_nbr;
        }

        //*************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            //Header 
            //lbl_registry_no.Text = "";
            
            txtb_empl_id.Text               = "";

            ddl_empl_id.SelectedIndex       = -1;
            ddl_dep.SelectedIndex           = -1;
            ddl_subdep.SelectedIndex        = -1;
            ddl_division.SelectedIndex      = -1;
            ddl_section.SelectedIndex       = -1;

            ddl_fund_charges.SelectedValue  = "";
            txtb_month_covered.Text         = "";
            txtb_daily_rate_new.Text        = "0.00";
            txtb_monthly_rate_new.Text      = "0.00";
            txtb_net_pay.Text               = "0.00";
            txtb_monthly_rate_old.Text      = "0.00";
            txtb_daily_rate_old.Text        = "0.00";
            txtb_total_deductions.Text      = "0.00";
            txtb_gross_pay.Text             = "0.00";
            txtb_gsis_gs.Text               = "0.00";
            txtb_gsis_ps.Text               = "0.00";
            txtb_hdmf_gs.Text               = "0.00";
            txtb_hdmf_ps.Text               = "0.00";
            txtb_phic_gs.Text               = "0.00";
            txtb_phic_ps.Text               = "0.00";
            txtb_bir_tax.Text               = "0.00";
            txtb_salary_diff_amt.Text       = "0.00";
            txtb_no_of_months.Text          = "0";

            txtb_days_worked.Text           = "0.00";
            txtb_sal_diff_per_day.Text      = "0.00";
            txtb_leave_earned.Text          = "0.00";

            txtb_remarks.Text               = "";
            txtb_lates_amt.Text             = "0.00";

            //Added by: Jorge
            txtb_voucher_nbr.Text = "";
            ViewState["created_by_user"] = "";
            ViewState["updated_by_user"] = "";
            ViewState["posted_by_user"] = "";
            ViewState["created_dttm"] = "";
            ViewState["updated_dttm"] = "";
            ViewState["wtax_rate"] = "0";
            txtb_date_posted.Text = "";
            txtb_position.Text = "";
            txtb_status.Text = "";
            FieldValidationColorChanged(false, "ALL");
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("rate_basis", typeof(System.String));
            dtSource.Columns.Add("monthly_rate", typeof(System.String));
            dtSource.Columns.Add("daily_rate", typeof(System.String));
            dtSource.Columns.Add("gross_pay", typeof(System.String));
            dtSource.Columns.Add("net_pay", typeof(System.String));
            dtSource.Columns.Add("month_covered", typeof(System.String));
            dtSource.Columns.Add("no_of_months", typeof(System.String));
            dtSource.Columns.Add("monthly_rate_old", typeof(System.String));
            dtSource.Columns.Add("daily_rate_old", typeof(System.String));
            dtSource.Columns.Add("salary_diff_amount", typeof(System.String));
            dtSource.Columns.Add("lowp_amount", typeof(System.String));
            dtSource.Columns.Add("wtax", typeof(System.String));
            dtSource.Columns.Add("gsis_gs",typeof(System.String));
            dtSource.Columns.Add("gsis_ps",typeof(System.String));
            dtSource.Columns.Add("hdmf_gs",typeof(System.String));
            dtSource.Columns.Add("hdmf_ps",typeof(System.String));
            dtSource.Columns.Add("phic_gs",typeof(System.String));
            dtSource.Columns.Add("phic_ps", typeof(System.String));
            dtSource.Columns.Add("post_status", typeof(System.String));
            dtSource.Columns.Add("date_posted", typeof(System.String));
            dtSource.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            dtSource.Columns.Add("posted_by_user", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));
            dtSource.Columns.Add("days_worked", typeof(System.String));
            dtSource.Columns.Add("salary_diff_amt_per_day", typeof(System.String));
            dtSource.Columns.Add("leave_earned", typeof(System.String));

            dtSource.Columns.Add("remarks", typeof(System.String));
            dtSource.Columns.Add("lates_amount", typeof(System.String));

        }
        //*************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollregistry_dtl_diff_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr", "empl_id" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["rate_basis"] = string.Empty;
            nrow["monthly_rate"] = string.Empty;
            nrow["daily_rate"] = string.Empty;
            nrow["gross_pay"] = string.Empty;
            nrow["net_pay"] = string.Empty;
            nrow["month_covered"] = string.Empty;
            nrow["no_of_months"] = string.Empty;
            nrow["monthly_rate_old"] = string.Empty;
            nrow["daily_rate_old"] = string.Empty;
            nrow["salary_diff_amount"] = string.Empty;
            nrow["lowp_amount"] = string.Empty;
            nrow["wtax"] = string.Empty;
            nrow["gsis_gs"]=string.Empty;
            nrow["gsis_ps"]=string.Empty;
            nrow["hdmf_gs"]=string.Empty;
            nrow["hdmf_ps"]=string.Empty;
            nrow["phic_gs"]=string.Empty;
            nrow["phic_ps"] = string.Empty;
            nrow["post_status"] = string.Empty;
            nrow["date_posted"] = string.Empty;

            nrow["days_worked"] = string.Empty;
            nrow["salary_diff_amt_per_day"] = string.Empty;
            nrow["leave_earned"] = string.Empty;

            nrow["remarks"] = string.Empty;
            nrow["lates_amount"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {

            FieldValidationColorChanged(false, "ALL");
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year = commandArgs[2];
            txtb_reason.Text = "";
            if (Session["ep_post_authority"].ToString() == "0")
            {
                // This is the Normal Delete if the user is Non-accounting user
                deleteRec1.Text = "Are you sure to delete this Record ?";
                deleteRec0.InnerText = "Delete this Record ?";
                lbl_unposting.Text = "";
                txtb_reason.Visible = false;
                lnkBtnYes.Text = "Yes, Delete it";
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                // This is Message if the accounting user will unpost the card 
                deleteRec1.Text = "Are you sure you want to UnPost this Record ?";
                deleteRec0.InnerText = "UnPost this Record ?";
                lbl_unposting.Text = "Reason for UnPosting :";
                txtb_reason.Visible = true;
                lnkBtnYes.Text = "Yes, UnPost it";
            }

            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "'";

            if (Session["ep_post_authority"].ToString() == "0")
            {
                // Non-Accounting Delete 
                MyCmn.DeleteBackEndData("payrollregistry_dtl_diff_tbl", "WHERE " + deleteExpression);
                DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
                dataListGrid.Rows.Remove(row2Delete[0]);
                dataListGrid.AcceptChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                if (txtb_reason.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_reason");
                }
                else
                {
                    // Stored Procedure to Insert to payrollregistry_dtl_unpost_tbl during accounting case
                    DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_unpost_tbl_insert", "par_payroll_year", ddl_year.SelectedValue, "par_payroll_registry_nbr", commandarg[1].Trim(), "par_empl_id", commandarg[0].Trim(), "par_reason", txtb_reason.Text);

                    //4.4.b.Update the following fields: From payrollregistry_dtl_rata_tbl Table
                    //    1.voucher_nbr     =   { blank}
                    //    2.posted_by_user  =   { blank}
                    //    3.post_status     =   "N"   
                    //    4.date_posted     =   { blank}
                    //    5.updated_by_user =   session user_id   
                    //    6.updated_dttm    =   System Date

                    string setparams = "";
                    setparams = "voucher_nbr = '',posted_by_user = '',post_status='N',date_posted='' , updated_by_user='" + Session["ep_user_id"].ToString() + "', updated_dttm='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    MyCmn.UpdateTable("payrollregistry_dtl_diff_tbl", setparams, "WHERE " + deleteExpression);
                    RetrieveDataListGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
                }
            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        
        //**************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            editaddmodal(e.CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private void editaddmodal(string session_val)
        {
            // BEGIN - Pass Value
            // Employee ID      [0]
            // Registry         [1]
            // Year             [2]
            // Employment Type  [3]
            // Department       [4]
            // END   - Pass Value

            string[] svalues = session_val.ToString().Split(new char[] { ',' });
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND payroll_registry_nbr = '" + svalues[1].ToString().Trim() + "' AND payroll_year = '" + svalues[2].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            DataRow[] row2Edit2 = dtSource_for_names.Select("empl_id = '" + svalues[0].ToString().Trim() + "'");

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            lbl_registry_number.Text = svalues[1].ToString().Trim();
            txtb_employeename.Text = row2Edit[0]["employee_name"].ToString();
            //ddl_empl_id.SelectedValue      = row2Edit[0]["empl_id"].ToString();
            txtb_empl_id.Text = svalues[0].ToString().Trim();

            txtb_employeename.Visible = true;
            ddl_empl_id.Visible = false;
           
            if (row2Edit[0]["department_code"].ToString() != string.Empty)
            {
                ddl_dep.SelectedValue = row2Edit[0]["department_code"].ToString();
            }
            if (row2Edit[0]["subdepartment_code"].ToString() != string.Empty)
            {
                ddl_subdep.SelectedValue = row2Edit[0]["subdepartment_code"].ToString();
            }
            else
            {
                ddl_subdep.SelectedIndex = -1;
            }
            RetrieveBindingDivision();

            if (row2Edit[0]["division_code"].ToString() != string.Empty && row2Edit[0]["division_code"].ToString() != "")
            {
                ddl_division.SelectedValue = row2Edit[0]["division_code"].ToString();
            }
            else
            {
                ddl_division.SelectedIndex = -1;
            }
            RetrieveBindingSection();

            if (row2Edit[0]["section_code"].ToString() != string.Empty)
            {
                ddl_section.SelectedValue   = row2Edit[0]["section_code"].ToString();
            }                               
                                            
            ddl_fund_charges.SelectedValue  = row2Edit[0]["fund_code"].ToString();
                                            
            lbl_rate_basis_hidden.Text      = row2Edit[0]["rate_basis"].ToString();
            txtb_monthly_rate_new.Text      = row2Edit[0]["monthly_rate"].ToString();
            txtb_monthly_rate_old.Text      = row2Edit[0]["monthly_rate_old"].ToString();
            txtb_daily_rate_new.Text        = row2Edit[0]["daily_rate"].ToString();
            txtb_daily_rate_old.Text        = row2Edit[0]["daily_rate_old"].ToString();
            txtb_net_pay.Text               = row2Edit[0]["net_pay"].ToString();

            txtb_gsis_gs.Text               = row2Edit[0]["gsis_gs"].ToString();
            txtb_gsis_ps.Text               = row2Edit[0]["gsis_ps"].ToString();
            txtb_hdmf_gs.Text               = row2Edit[0]["hdmf_gs"].ToString();
            txtb_hdmf_ps.Text               = row2Edit[0]["hdmf_ps"].ToString();
            txtb_phic_gs.Text               = row2Edit[0]["phic_gs"].ToString();
            txtb_phic_ps.Text               = row2Edit[0]["phic_ps"].ToString();
            txtb_bir_tax.Text               = row2Edit[0]["wtax"].ToString();
            txtb_gross_pay.Text             = row2Edit[0]["gross_pay"].ToString();
            txtb_month_covered.Text         = row2Edit[0]["month_covered"].ToString();
            txtb_no_of_months.Text          = row2Edit[0]["no_of_months"].ToString();
            txtb_monthly_rate_old.Text       = row2Edit[0]["monthly_rate_old"].ToString();
            txtb_lwop_amount.Text           = row2Edit[0]["lowp_amount"].ToString();

            txtb_days_worked.Text           = row2Edit[0]["days_worked"].ToString();
            txtb_leave_earned.Text          = row2Edit[0]["leave_earned"].ToString();
            txtb_sal_diff_per_day.Text      = row2Edit[0]["salary_diff_amt_per_day"].ToString();

            txtb_remarks.Text           = row2Edit[0]["remarks"].ToString();
            txtb_lates_amt.Text         = row2Edit[0]["lates_amount"].ToString();
            ViewState["wtax_rate"]      = row2Edit[0]["wtax_rate"].ToString();

            // The Save Button Will be Visible false if the 

            //if (row2Edit[0]["post_status"].ToString() == "Y")
            //{
            //    btnSave.Visible = false;
            //    lbl_if_dateposted_yes.Text = "This Payroll Already Posted, You cannot Edit!";
            //}
            //else
            //{
            //    btnSave.Visible = true;
            //    lbl_if_dateposted_yes.Text = "";
            //}

            // During The Employee Name Change 
            calculate_summary_deductions();
            calculate_gross_salarydiff();
            calculate_netpays();
            //calculate_leave_earned();

            ddl_empl_id.Enabled = false;
            LabelAddEdit.Text = "Edit Record | Registry No : " + lbl_registry_number.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            lbl_addeditmode_hidden.Text = MyCmn.CONST_EDIT;

            FieldValidationColorChanged(false, "ALL");

            txtb_position.Text = row2Edit[0]["position_title1"].ToString();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


            // Add Field Again - 06/20/2019
            txtb_voucher_nbr.Text = row2Edit[0]["voucher_nbr"].ToString();
            ViewState["created_by_user"]     = row2Edit[0]["created_by_user"].ToString();
            ViewState["updated_by_user"]     = row2Edit[0]["updated_by_user"].ToString();
            ViewState["posted_by_user"]      = row2Edit[0]["posted_by_user"].ToString();
            ViewState["created_dttm"]        = row2Edit[0]["created_dttm"].ToString();
            ViewState["updated_dttm"]        = row2Edit[0]["updated_dttm"].ToString();
            txtb_status.Text                 = row2Edit[0]["post_status_descr"].ToString();
            dtSource.Rows[0]["date_posted"]  = row2Edit[0]["date_posted"].ToString().Trim();

            //FOR POSTED STATUS
            if (row2Edit[0]["post_status"].ToString().ToUpper() == "Y" && (Session["ep_post_authority"].ToString() == "1" || Session["ep_post_authority"].ToString() == "0"))
            {
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                txtb_voucher_nbr.Enabled = false;
                Linkbtncancel.Text = "Close";
                lbl_if_dateposted_yes.Text = "This Payroll is already Posted, You Cannot Edit!";
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }

            //FOR USER'S IN ACCOUNTING
            else if (row2Edit[0]["post_status"].ToString() == "N" && Session["ep_post_authority"].ToString() == "1")
            {
                btnSave.Visible = true;
                btn_calculate.Visible = false;
                btnSave.Text = "Post To Card";
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = true;
                txtb_date_posted.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }
            //FOR RELEASED STATUS
            else if (row2Edit[0]["post_status"].ToString() == "R"
                 //   || row2Edit[0]["post_status"].ToString() == "T"
                    || row2Edit[0]["post_status"].ToString() == "X"
                    )
            {
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                txtb_voucher_nbr.Enabled = false;
                Linkbtncancel.Text = "Close";
                lbl_if_dateposted_yes.Text = "This Payroll Already " + row2Edit[0]["post_status_descr"].ToString() + ", You cannot Edit!";
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            //FOR OTHER DEPARTMENTS
            else if (Session["ep_post_authority"].ToString() == "0")
            {
                txtb_voucher_nbr.Enabled = false;
                btnSave.Visible = true;
                btn_calculate.Visible = true;
                btnSave.Text = "Save";
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = "";
                ToogleTextbox(true);

            }

        }
        //**************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Change Field Sort mode  
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
        //  BEGIN - VJA- 05/23/2019 - Get Grid current sort order 
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
        //  BEGIN - VJA- 05/23/2019 - Save New Record/Edited Record to back end DB
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
                    dtSource.Rows[0]["payroll_year"]             = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"]     = lbl_registry_number.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                  = ddl_empl_id.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]               = lbl_rate_basis_hidden.Text.ToString();
                    dtSource.Rows[0]["monthly_rate"]             = txtb_monthly_rate_new.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]               = txtb_daily_rate_new.Text.ToString().Trim();
                    dtSource.Rows[0]["gross_pay"]                = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                  = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["month_covered"]            = txtb_month_covered.Text.ToString().Trim();
                    dtSource.Rows[0]["no_of_months"]             = txtb_no_of_months.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_rate_old"]         = txtb_monthly_rate_old.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate_old"]           = txtb_daily_rate_old.Text.ToString().Trim();
                    dtSource.Rows[0]["salary_diff_amount"]       = txtb_salary_diff_amt.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount"]              = txtb_lwop_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["wtax"]                     = txtb_bir_tax.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_gs"]                  = txtb_gsis_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ps"]                  = txtb_gsis_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_gs"]                  = txtb_hdmf_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps"]                  = txtb_hdmf_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_gs"]                  = txtb_phic_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_ps"]                  = txtb_phic_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["post_status"]              = "N";


                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource.Rows[0]["voucher_nbr"]              = txtb_voucher_nbr.Text.ToString();
                    dtSource.Rows[0]["created_by_user"]          = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_by_user"]          = "";
                    dtSource.Rows[0]["created_dttm"]             = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["updated_dttm"]             = "";
                    dtSource.Rows[0]["posted_by_user"]           = "";
                    dtSource.Rows[0]["date_posted"]              = "";
                    // END - Add Field Again  - 06/20/2019

                    dtSource.Rows[0]["days_worked"]              = txtb_days_worked.Text;
                    dtSource.Rows[0]["salary_diff_amt_per_day"]  = txtb_sal_diff_per_day.Text;
                    dtSource.Rows[0]["leave_earned"]             = txtb_leave_earned.Text;

                    dtSource.Rows[0]["remarks"]                 = txtb_remarks.Text;
                    dtSource.Rows[0]["lates_amount"]             = txtb_lates_amt.Text;

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"]             = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"]     = lbl_registry_number.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                  = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]               = lbl_rate_basis_hidden.Text.ToString();
                    dtSource.Rows[0]["monthly_rate"]             = txtb_monthly_rate_new.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]               = txtb_daily_rate_new.Text.ToString().Trim();
                    dtSource.Rows[0]["gross_pay"]                = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                  = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["month_covered"]            = txtb_month_covered.Text.ToString().Trim();
                    dtSource.Rows[0]["no_of_months"]             = txtb_no_of_months.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_rate_old"]         = txtb_monthly_rate_old.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate_old"]           = txtb_daily_rate_old.Text.ToString().Trim();
                    dtSource.Rows[0]["salary_diff_amount"]       = txtb_salary_diff_amt.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount"]              = txtb_lwop_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["wtax"]                     = txtb_bir_tax.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_gs"]                  = txtb_gsis_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ps"]                  = txtb_gsis_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_gs"]                  = txtb_hdmf_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps"]                  = txtb_hdmf_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_gs"]                  = txtb_phic_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_ps"]                  = txtb_phic_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["post_status"]              = "N";

                    // BEGIN - Add Field Again  - 06/26/2019

                    dtSource.Rows[0]["created_by_user"]          = ViewState["created_by_user"].ToString();
                    dtSource.Rows[0]["updated_by_user"]          = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_dttm"]             = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["created_dttm"]             = ViewState["created_dttm"].ToString();

                    if (Session["ep_post_authority"].ToString().Trim() == "1")
                    {
                        dtSource.Rows[0]["posted_by_user"]       = Session["ep_user_id"].ToString();
                        dtSource.Rows[0]["date_posted"]          = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dtSource.Rows[0]["post_status"]          = "Y";
                        dtSource.Rows[0]["voucher_nbr"]          = txtb_voucher_nbr.Text.ToString().Trim();
                        dtSource.Rows[0]["updated_by_user"]      = ViewState["updated_by_user"].ToString();
                        dtSource.Rows[0]["updated_dttm"]         = ViewState["updated_dttm"].ToString();
                    }
                    // END - Add Field Again  - 06/26/2019

                    
                    dtSource.Rows[0]["days_worked"]              = txtb_days_worked.Text;
                    dtSource.Rows[0]["salary_diff_amt_per_day"]  = txtb_sal_diff_per_day.Text;
                    dtSource.Rows[0]["leave_earned"]             = txtb_leave_earned.Text;

                    
                    dtSource.Rows[0]["remarks"]                 = txtb_remarks.Text;
                    dtSource.Rows[0]["lates_amount"]             = txtb_lates_amt.Text;

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                        //Calculate All Fields and Insert to the Haader
                        calculate_summary_deductions();
                        calculate_gross_salarydiff();
                        calculate_netpays();
                        //calculate_leave_earned();

                        if (scriptInsertUpdate == string.Empty) return;
                        string msg = MyCmn.insertdata(scriptInsertUpdate);
                        if (msg == "") return;

                        if (msg.Substring(0, 1) == "X")
                        {
                            FieldValidationColorChanged(true, "already-exist");
                            return;
                        }

                        if (saveRecord == MyCmn.CONST_ADD)
                        {
                            DataRow nrow = dataListGrid.NewRow();

                            nrow["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                            nrow["payroll_registry_nbr"] = lbl_registry_number.Text.ToString().Trim();
                            nrow["empl_id"]              = ddl_empl_id.SelectedValue.ToString().Trim();
                            nrow["rate_basis"]           = lbl_rate_basis_hidden.Text.ToString();
                            nrow["monthly_rate"]         = txtb_monthly_rate_new.Text.ToString().Trim();
                            nrow["daily_rate"]           = txtb_daily_rate_new.Text.ToString().Trim();
                            nrow["gross_pay"]            = txtb_gross_pay.Text.ToString().Trim();
                            nrow["net_pay"]              = txtb_net_pay.Text.ToString().Trim();
                            nrow["month_covered"]        = txtb_month_covered.Text.ToString().Trim();
                            nrow["no_of_months"]         = txtb_no_of_months.Text.ToString().Trim();
                            nrow["monthly_rate_old"]     = txtb_monthly_rate_old.Text.ToString().Trim();
                            nrow["daily_rate_old"]       = txtb_daily_rate_old.Text.ToString().Trim();
                            nrow["salary_diff_amount"]   = txtb_salary_diff_amt.Text.ToString().Trim();
                            nrow["lowp_amount"]          = txtb_lwop_amount.Text.ToString().Trim();
                            nrow["wtax"]                 = txtb_bir_tax.Text.ToString().Trim();
                            nrow["gsis_gs"]              = txtb_gsis_gs.Text.ToString().Trim();
                            nrow["gsis_ps"]              = txtb_gsis_ps.Text.ToString().Trim();
                            nrow["hdmf_gs"]              = txtb_hdmf_gs.Text.ToString().Trim();
                            nrow["hdmf_ps"]              = txtb_hdmf_ps.Text.ToString().Trim();
                            nrow["phic_gs"]              = txtb_phic_gs.Text.ToString().Trim();
                            nrow["phic_ps"]              = txtb_phic_ps.Text.ToString().Trim();
                            nrow["post_status"]          = "N";
                            nrow["date_posted"]          = "";
                            nrow["employee_name"]        = ddl_empl_id.SelectedItem.ToString().Trim();
                            nrow["post_status_descr"]    = "NOT POSTED";

                            // BEGIN - Add Field Again  - 06/20/2019
                            nrow["voucher_nbr"] = txtb_voucher_nbr.Text.ToString();
                            nrow["created_by_user"] = Session["ep_user_id"].ToString();
                            nrow["updated_by_user"] = "";
                            nrow["created_dttm"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        nrow["updated_dttm"]        = Convert.ToDateTime("1900-01-01");
                            nrow["posted_by_user"] = "";
                            nrow["date_posted"] = "";
                            nrow["position_title1"] = txtb_position.Text.ToString().Trim();
                            // END - Add Field Again  - 06/20/2019
                            
                            nrow["days_worked"]              = txtb_days_worked.Text;
                            nrow["salary_diff_amt_per_day"]  = txtb_sal_diff_per_day.Text;
                            nrow["leave_earned"]             = txtb_leave_earned.Text;
                        
                            nrow["remarks"]                 = txtb_remarks.Text;
                            nrow["lates_amount"]             = txtb_lates_amt.Text;

                            dataListGrid.Rows.Add(nrow);
                            gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                            SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        }
                        if (saveRecord == MyCmn.CONST_EDIT)
                        {
                            string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "'";
                            DataRow[] row2Edit = dataListGrid.Select(editExpression);

                            row2Edit[0]["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                            row2Edit[0]["payroll_registry_nbr"] = lbl_registry_number.Text.ToString().Trim();
                            row2Edit[0]["empl_id"]              = txtb_empl_id.Text.ToString().Trim();
                            row2Edit[0]["rate_basis"]           = lbl_rate_basis_hidden.Text.ToString();
                            row2Edit[0]["monthly_rate"]         = txtb_monthly_rate_new.Text.ToString().Trim();
                            row2Edit[0]["daily_rate"]           = txtb_daily_rate_new.Text.ToString().Trim();
                            row2Edit[0]["gross_pay"]            = txtb_gross_pay.Text.ToString().Trim();
                            row2Edit[0]["net_pay"]              = txtb_net_pay.Text.ToString().Trim();
                            row2Edit[0]["month_covered"]        = txtb_month_covered.Text.ToString().Trim();
                            row2Edit[0]["no_of_months"]         = txtb_no_of_months.Text.ToString().Trim();
                            row2Edit[0]["monthly_rate_old"]     = txtb_monthly_rate_old.Text.ToString().Trim();
                            row2Edit[0]["daily_rate_old"]       = txtb_daily_rate_old.Text.ToString().Trim();
                            row2Edit[0]["salary_diff_amount"]   = txtb_salary_diff_amt.Text.ToString().Trim();
                            row2Edit[0]["lowp_amount"]          = txtb_lwop_amount.Text.ToString().Trim();
                            row2Edit[0]["wtax"]                 = txtb_bir_tax.Text.ToString().Trim();
                            row2Edit[0]["gsis_gs"]              = txtb_gsis_gs.Text.ToString().Trim();
                            row2Edit[0]["gsis_ps"]              = txtb_gsis_ps.Text.ToString().Trim();
                            row2Edit[0]["hdmf_gs"]              = txtb_hdmf_gs.Text.ToString().Trim();
                            row2Edit[0]["hdmf_ps"]              = txtb_hdmf_ps.Text.ToString().Trim();
                            row2Edit[0]["phic_gs"]              = txtb_phic_gs.Text.ToString().Trim();
                            row2Edit[0]["phic_ps"]              = txtb_phic_ps.Text.ToString().Trim();
                            row2Edit[0]["post_status"]          = "N";
                            row2Edit[0]["date_posted"]          = "";
                            row2Edit[0]["employee_name"]        = txtb_employeename.Text.ToString().Trim();

                             // BEGIN - Add Field Again - 06/20/2019
                             row2Edit[0]["created_by_user"]     = ViewState["created_by_user"].ToString();
                             row2Edit[0]["updated_by_user"]     = Session["ep_user_id"].ToString();
                             row2Edit[0]["created_dttm"]        = ViewState["created_dttm"].ToString() == "" ? DateTime.Now : ViewState["created_dttm"];
                             row2Edit[0]["updated_dttm"]        = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                             // END - Add Field Again  - 06/20/2019
                             if (Session["ep_post_authority"].ToString() == "1")
                             {
                                 row2Edit[0]["posted_by_user"]  = Session["ep_user_id"].ToString();
                                 row2Edit[0]["date_posted"]     = txtb_date_posted.Text.ToString().Trim();
                                 row2Edit[0]["post_status"]     = "Y";
                                 row2Edit[0]["post_status_descr"] = "POSTED";
                                 row2Edit[0]["voucher_nbr"]     = txtb_voucher_nbr.Text.ToString();
                                 row2Edit[0]["updated_by_user"] = ViewState["updated_by_user"].ToString();
                                 row2Edit[0]["updated_dttm"]    = ViewState["updated_dttm"].ToString();
                             }

                             row2Edit[0]["days_worked"]              = txtb_days_worked.Text;
                             row2Edit[0]["salary_diff_amt_per_day"]  = txtb_sal_diff_per_day.Text;
                             row2Edit[0]["leave_earned"]             = txtb_leave_earned.Text;


                            row2Edit[0]["remarks"] = txtb_remarks.Text;
                            row2Edit[0]["lates_amount"] = txtb_lates_amt.Text;

                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                            SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                        ViewState.Remove("AddEdit_Mode");
                        show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                    }
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
        }
        //**********************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR position_title1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR post_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR net_pay LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("rate_basis", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("daily_rate", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("month_covered", typeof(System.String));
            dtSource1.Columns.Add("no_of_months", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate_old", typeof(System.String));
            dtSource1.Columns.Add("daily_rate_old", typeof(System.String));
            dtSource1.Columns.Add("salary_diff_amount", typeof(System.String));
            dtSource1.Columns.Add("lowp_amount", typeof(System.String));
            dtSource1.Columns.Add("wtax", typeof(System.String));
            dtSource1.Columns.Add("gsis_gs", typeof(System.String));
            dtSource1.Columns.Add("gsis_ps", typeof(System.String));
            dtSource1.Columns.Add("hdmf_gs", typeof(System.String));
            dtSource1.Columns.Add("hdmf_ps", typeof(System.String));
            dtSource1.Columns.Add("phic_gs", typeof(System.String));
            dtSource1.Columns.Add("phic_ps", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("date_posted", typeof(System.String));

            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
           
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
        //  BEGIN - VJA- 05/23/2019 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 05/23/2019 - Check if Object already contains value  
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
        //  BEGIN - VJA- 05/23/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            FieldValidationColorChanged(false, "ALL");
            bool validatedSaved = true;

            int target_tab = 1;

            if (ddl_empl_id.SelectedValue == "" && lbl_addeditmode_hidden.Text == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }
            
            if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_gross_pay");
                txtb_gross_pay.Focus();
                validatedSaved = false;
            }
            
            if (CommonCode.checkisdecimal(txtb_net_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_pay");
                txtb_net_pay.Focus();
                validatedSaved = false;
            }
            
            if (CommonCode.checkisdecimal(txtb_gsis_gs) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_gs");
                txtb_gsis_gs.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ps) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ps");
                txtb_gsis_ps.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_gs) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_gs");
                txtb_hdmf_gs.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_ps) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_ps");
                txtb_hdmf_ps.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_phic_gs) == false)
            {
                FieldValidationColorChanged(true, "txtb_phic_gs");
                txtb_phic_gs.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_phic_ps) == false)
            {
                FieldValidationColorChanged(true, "txtb_phic_ps");
                txtb_phic_ps.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_bir_tax) == false)
            {
                FieldValidationColorChanged(true, "txtb_bir_tax");
                txtb_bir_tax.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisnumber(txtb_no_of_months) == false)
            {
                FieldValidationColorChanged(true, "txtb_no_of_months");
                txtb_no_of_months.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_lwop_amount) == false)
            {
                FieldValidationColorChanged(true, "txtb_lwop_amount");
                txtb_lwop_amount.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_monthly_rate_new) == false)
            {
                FieldValidationColorChanged(true, "txtb_monthly_rate_new");
                txtb_monthly_rate_new.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_daily_rate_new) == false)
            {
                FieldValidationColorChanged(true, "txtb_daily_rate_new");
                txtb_daily_rate_new.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_monthly_rate_old) == false)
            {
                FieldValidationColorChanged(true, "txtb_monthly_rate_old");
                txtb_monthly_rate_old.Focus();
                validatedSaved = false;
                target_tab = 1;
            }
            if (CommonCode.checkisdecimal(txtb_daily_rate_old) == false)
            {
                FieldValidationColorChanged(true, "txtb_daily_rate_old");
                txtb_daily_rate_old.Focus();
                validatedSaved = false;
                target_tab = 1;
            }

            //Add validation
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_days_worked) == false)
            {
                FieldValidationColorChanged(true, "txtb_days_worked");
                txtb_days_worked.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_sal_diff_per_day) == false)
            {
                FieldValidationColorChanged(true, "txtb_sal_diff_per_day");
                txtb_sal_diff_per_day.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_leave_earned) == false)
            {
                FieldValidationColorChanged(true, "txtb_leave_earned");
                txtb_leave_earned.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_lates_amt) == false)
            {
                FieldValidationColorChanged(true, "txtb_lates_amt");
                txtb_lates_amt.Focus();
                validatedSaved = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClickTab", "click_tab("+ target_tab + ")", true);
            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_empl_id":
                        {
                            LblRequired60.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    
                    case "txtb_gross_pay":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_pay":
                        {
                            LblRequired10.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_gs":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_gs.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_ps":
                        {
                            LblRequired15.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ps.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_gs":
                        {
                            LblRequired17.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_gs.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_ps":
                        {
                            LblRequired18.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_ps.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_phic_gs":
                        {
                            LblRequired19.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_phic_gs.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_phic_ps":
                        {
                            LblRequired20.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_phic_ps.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_bir_tax":
                        {
                            LblRequired21.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_bir_tax.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired60.Text = "already exist!";
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_no_of_months":
                        {
                            LblRequired1.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_no_of_months.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_lwop_amount":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lwop_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_monthly_rate_old":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_monthly_rate_old.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_daily_rate_old":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_daily_rate_old.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_monthly_rate_new":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_monthly_rate_new.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_daily_rate_new":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_daily_rate_new.BorderColor = Color.Red;
                            break;
                        }
                    case "less-than-minimum":
                        {
                            LblRequired10.Text = "Net Pay is Below Minimum! ";
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_voucher_nbr":
                        {
                            LblRequired101.Text = MyCmn.CONST_RQDFLD;
                            txtb_voucher_nbr.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_reason":
                        {
                            LblRequired201.Text = MyCmn.CONST_RQDFLD;
                            txtb_reason.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_days_worked":
                        {
                            LblRequired300.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_days_worked.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_sal_diff_per_day":
                        {
                            LblRequired301.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_sal_diff_per_day.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_lates_amt":
                        {
                            lbl_txtb_lates_amt.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lates_amt.BorderColor = Color.Red;
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
                            LblRequired4.Text = "";
                            LblRequired5.Text = "";
                            LblRequired10.Text = "";
                            LblRequired13.Text = "";
                            LblRequired15.Text = "";
                            LblRequired17.Text = "";
                            LblRequired18.Text = "";
                            LblRequired19.Text = "";
                            LblRequired20.Text = "";
                            LblRequired21.Text = "";
                            LblRequired60.Text = "";
                            LblRequired101.Text = "";
                            LblRequired201.Text = "";
                            LblRequired300.Text = "";
                            LblRequired301.Text = "";
                            lbl_txtb_lates_amt.Text = "";

                            txtb_voucher_nbr.BorderColor = Color.LightGray;
                            txtb_reason.BorderColor = Color.LightGray;
                            txtb_gross_pay.BorderColor = Color.LightGray;
                            txtb_net_pay.BorderColor = Color.LightGray;
                            txtb_gsis_gs.BorderColor = Color.LightGray;
                            txtb_gsis_ps.BorderColor = Color.LightGray;
                            txtb_hdmf_gs.BorderColor = Color.LightGray;
                            txtb_hdmf_ps.BorderColor = Color.LightGray;
                            txtb_phic_gs.BorderColor = Color.LightGray;
                            txtb_phic_ps.BorderColor = Color.LightGray;
                            txtb_bir_tax.BorderColor = Color.LightGray;
                            ddl_empl_id.BorderColor = Color.LightGray;
                            txtb_lwop_amount.BorderColor = Color.LightGray;
                            txtb_no_of_months.BorderColor = Color.LightGray;
                            txtb_monthly_rate_new.BorderColor = Color.LightGray;
                            txtb_monthly_rate_old.BorderColor = Color.LightGray;

                            txtb_days_worked.BorderColor = Color.LightGray;
                            txtb_sal_diff_per_day.BorderColor = Color.LightGray;
                            txtb_lates_amt.BorderColor = Color.LightGray;
                            
                            break;
                        }
                }
            }
        }
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "")
            {
                RetrieveDataListGrid();
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetriveGroupings();
            RetriveTemplate();
            RetrieveEmployeename();
            UpdatePanel10.Update();
        }
        protected void ddl_dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_dep.SelectedValue != "" )
            {
                RetrieveBindingSubDep();
                RetrieveBindingDivision();
                RetrieveBindingSection();
                RetrieveEmployeename();
                
            }
            else
            {
                ClearEntry();
            }
        }
        protected void ddl_subdep_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveEmployeename();
            ddl_subdep.Focus();
        }
        protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingSection();
            RetrieveEmployeename();
            ddl_division.Focus();
        }
        
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveEmployeename();
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ddl_dep.SelectedValue = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }

        protected void ddl_year_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "")
            {
                RetrieveYear();
            }
        }
        protected void ddl_payroll_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            RetrieveEmployeename();
        }

        protected void ddl_payroll_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        protected void ddl_empl_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false,"ALL");
            if (ddl_empl_id.SelectedValue != "")
            {
                HeaderDetails_Initialized_Add();
                calculate_summary_deductions();
                calculate_gross_salarydiff();
                calculate_netpays();
            }
            else
            {
                ClearEntry();
            }
            
        }
        private void HeaderDetails_Initialized_Add()
        {
            string editExpression = "empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'";
            DataRow[] row2Edit2 = dtSource_for_names.Select("empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

            txtb_empl_id.Text = ddl_empl_id.SelectedValue.ToString().Trim();
            txtb_position.Text = row2Edit2[0]["position_title1"].ToString();

            if (row2Edit2[0]["department_code"].ToString() != string.Empty)
            {
                ddl_dep.SelectedValue = row2Edit2[0]["department_code"].ToString();
            }
            if (row2Edit2[0]["subdepartment_code"].ToString() != string.Empty)
            {
                ddl_subdep.SelectedValue = row2Edit2[0]["subdepartment_code"].ToString();
            }
            else
            {
                ddl_subdep.SelectedIndex = -1;
            }
            RetrieveBindingDivision();

            if (row2Edit2[0]["division_code"].ToString() != string.Empty && row2Edit2[0]["division_code"].ToString() != "")
            {
                ddl_division.SelectedValue = row2Edit2[0]["division_code"].ToString();
            }
            else
            {
                ddl_division.SelectedIndex = -1;
            }
            RetrieveBindingSection();

            if (row2Edit2[0]["section_code"].ToString() != string.Empty)
            {
                ddl_section.SelectedValue = row2Edit2[0]["section_code"].ToString();
            }
            ddl_fund_charges.SelectedValue = row2Edit2[0]["fund_code"].ToString();
            
            lbl_rate_basis_hidden.Text                  = row2Edit2[0]["rate_basis"].ToString();
            txtb_monthly_rate_new.Text                  = row2Edit2[0]["monthly_rate"].ToString();
            txtb_daily_rate_new.Text                    = row2Edit2[0]["daily_rate"].ToString();
            
            txtb_monthly_rate_new.Text  = row2Edit2[0]["monthly_rate"].ToString();
            txtb_monthly_rate_old.Text  = row2Edit2[0]["monthly_rate_old"].ToString();
            txtb_daily_rate_new.Text    = row2Edit2[0]["daily_rate"].ToString();
            txtb_daily_rate_old.Text    = row2Edit2[0]["daily_rate_old"].ToString();

            switch (row2Edit2[0]["rate_basis"].ToString())
            {
                case "M":
                    {
                        lbl_rate_descr.Text       = "New Mthly. Rate:";
                        lbl_rate_descr_old.Text   = "Old Mthly. Rate:";
                        txtb_salary_diff_amt.Text = row2Edit2[0]["salary_diff_amount_monthly"].ToString();
                        calculate_gross_salarydiff();
                        break;
                    }
                case "D":
                    {
                        lbl_rate_descr.Text       = "New Daily Rate:";
                        lbl_rate_descr_old.Text   = "Old Daily Rate:";
                        txtb_salary_diff_amt.Text = row2Edit2[0]["salary_diff_amount_daily"].ToString();
                        break;
                    }
            }
            
            // UPDATE - VJA : 05/22/2019 - Data Will Coming From Employee Master
            txtb_gsis_gs.Text               = row2Edit2[0]["gsis_gs"].ToString();
            txtb_gsis_ps.Text               = row2Edit2[0]["gsis_ps"].ToString();
            txtb_hdmf_gs.Text               = row2Edit2[0]["hdmf_gs"].ToString();
            txtb_hdmf_ps.Text               = row2Edit2[0]["hdmf_ps"].ToString();
            txtb_phic_gs.Text               = row2Edit2[0]["phic_gs"].ToString();
            txtb_phic_ps.Text               = row2Edit2[0]["phic_ps"].ToString();
            ViewState["wtax_rate"]          = row2Edit2[0]["wtax_rate"].ToString();
            txtb_bir_tax.Text               = row2Edit2[0]["wtax"].ToString();

            double total_wtax = 0;
            total_wtax = (double.Parse(txtb_salary_diff_amt.Text) - (double.Parse(txtb_gsis_ps.Text) + double.Parse(txtb_hdmf_ps.Text) + double.Parse(txtb_phic_ps.Text))) * (double.Parse(ViewState["wtax_rate"].ToString()) / 100);
            txtb_bir_tax.Text = total_wtax.ToString("###,##0.00").Trim();

        }
        //**********************************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Calculate All When Click Button Calculate  
        //**********************************************************************************************
        protected void btn_calculate_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                calculate_summary_deductions();
                calculate_gross_salarydiff();
                calculate_netpays();
                //calculate_leave_earned();
            }
        }
        //**********************************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Calculate Gross Pay AND Salary Differential   
        //**********************************************************************************************
        private void calculate_gross_salarydiff()
        {
            double total_salary_diff = 0;
            double total_gross = 0;
            double total_salary_diff_per_day = 0;

            
            if (lbl_rate_basis_hidden.Text == "M" || ddl_empl_type.SelectedValue == "RE")
            {
                total_salary_diff = double.Parse(txtb_monthly_rate_new.Text) - double.Parse(txtb_monthly_rate_old.Text);
                total_gross = double.Parse(txtb_monthly_rate_new.Text) - double.Parse(txtb_monthly_rate_old.Text);
                total_gross = total_salary_diff - (double.Parse(txtb_lwop_amount.Text) + double.Parse(txtb_lates_amt.Text));
            }
            else if(ddl_empl_type.SelectedValue == "CE")
            {
                total_salary_diff_per_day = double.Parse(txtb_daily_rate_new.Text) - double.Parse(txtb_daily_rate_old.Text);
                total_salary_diff = total_salary_diff_per_day * double.Parse(txtb_days_worked.Text);
                total_gross = total_salary_diff - (double.Parse(txtb_lwop_amount.Text) + double.Parse(txtb_lates_amt.Text));
            }
            
            txtb_sal_diff_per_day.Text = total_salary_diff_per_day.ToString("###,##0.00").Trim();
            txtb_salary_diff_amt.Text = total_salary_diff.ToString("###,##0.00").Trim();
            txtb_gross_pay.Text = total_gross.ToString("###,##0.00").Trim();
        }
        
        //**********************************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Calculate Net Pay
        //**********************************************************************************************
        private void calculate_netpays()
        {
            double total_netpay = 0;
            total_netpay            = double.Parse(txtb_gross_pay.Text) - double.Parse(txtb_total_deductions.Text);
            txtb_net_pay.Text       = total_netpay.ToString("###,##0.00").Trim();

            //if (total_netpay < float.Parse(lbl_minimum_netpay_hidden.Text))
            //{
            //    FieldValidationColorChanged(true,"less-than-minimum");
            //}
        }
        //**********************************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Calculate Summary Details
        //**********************************************************************************************
        private void calculate_summary_deductions()
        {
            double total_deductions = 0;

            //total_deductions = total_deductions + float.Parse(txtb_gsis_gs.Text.ToString().Trim());
            total_deductions = total_deductions + double.Parse(txtb_gsis_ps.Text.ToString().Trim());
            //total_deductions = total_deductions + float.Parse(txtb_hdmf_gs.Text.ToString().Trim());
            total_deductions = total_deductions + double.Parse(txtb_hdmf_ps.Text.ToString().Trim());
            //total_deductions = total_deductions + float.Parse(txtb_phic_gs.Text.ToString().Trim());
            total_deductions = total_deductions + double.Parse(txtb_phic_ps.Text.ToString().Trim());
            total_deductions = total_deductions + double.Parse(txtb_bir_tax.Text.ToString().Trim());

            txtb_total_deductions.Text = total_deductions.ToString("###,##0.00");
        }


        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            lbl_rate_basis_hidden.Enabled   = ifenable;
            txtb_monthly_rate_new.Enabled        = ifenable;

            txtb_gsis_gs.Enabled            = ifenable;
            txtb_gsis_ps.Enabled            = ifenable;
            txtb_hdmf_gs.Enabled            = ifenable;
            txtb_hdmf_ps.Enabled            = ifenable;
            txtb_phic_gs.Enabled            = ifenable;
            txtb_phic_ps.Enabled            = ifenable;
            txtb_bir_tax.Enabled            = ifenable;
            txtb_month_covered.Enabled      = ifenable;
            txtb_no_of_months.Enabled       = ifenable;
            txtb_monthly_rate_old.Enabled    = ifenable;
            txtb_lwop_amount.Enabled        = ifenable;

            txtb_days_worked.Enabled        = ifenable;
            txtb_leave_earned.Enabled       = ifenable;

            txtb_remarks.Enabled       = ifenable;
            txtb_lates_amt.Enabled       = ifenable;
            // txtb_sal_diff_per_day.Enabled   = ifenable;

            if (ddl_empl_type.SelectedValue == "CE")
            {
                div_days_worked.Visible        = true;
                div_leave_earned.Visible       = true;
                div_sal_diff_per_day.Visible   = true;

                txtb_daily_rate_new.Visible     = true;
                txtb_daily_rate_old.Visible     = true;
                txtb_monthly_rate_new.Visible   = false;
                txtb_monthly_rate_old.Visible   = false;

            }
            else
            {
                div_days_worked.Visible        = false;
                div_leave_earned.Visible       = false;
                div_sal_diff_per_day.Visible   = false;
                
                txtb_daily_rate_new.Visible     = false;
                txtb_daily_rate_old.Visible     = false;
                txtb_monthly_rate_new.Visible   = true;
                txtb_monthly_rate_old.Visible   = true;
            }

        }
        //**********************************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Calculate Leave Earned for Casual
        //**********************************************************************************************
        private void calculate_leave_earned()
        {
            double total_leave_earned = 0;
            total_leave_earned      = double.Parse(txtb_sal_diff_per_day.Text) * double.Parse(txtb_days_worked.Text) / 8;
            txtb_leave_earned.Text  = total_leave_earned.ToString("###,##0.00");
        }
        //**********************************************************************************************
        //  BEGIN - VJA- 05/23/2019 - Calculate Leave Earned for Casual
        //**********************************************************************************************
        //private void calculate_wtax()
        //{
        //    double total_wtax = 0;
        //    total_wtax = double.Parse(txtb_sal_diff_per_day.Text) * double.Parse(txtb_days_worked.Text) / 8;
        //    txtb_leave_earned.Text = total_wtax.ToString("###,##0.00");
        //}
        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}