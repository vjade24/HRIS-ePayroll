//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Net Take Home Pay and Payslip
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     2021-02-20      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cTakeHomePay
{
    public partial class cTakeHomePay : System.Web.UI.Page
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
        //********************************************************************
        //  BEGIN - VJA- 09/12/2018 - Public Variable used in Add/Edit Mode
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


                if (Session["PreviousValuesonPage_cNTHP_payslip"] == null)
                    Session["PreviousValuesonPage_cNTHP_payslip"] = "";
                else if (Session["PreviousValuesonPage_cNTHP_payslip"].ToString() != string.Empty)
                {
                    RetrieveYear();
                    string[] prevValues = Session["PreviousValuesonPage_cNTHP_payslip"].ToString().Split(new char[] { ',' });
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_month.SelectedValue = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue = prevValues[2].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue = prevValues[3].ToString();
                    gv_dataListGrid.PageIndex = int.Parse(prevValues[4].ToString());
                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                    up_dataListGrid.Update();
                    DropDownListID.SelectedValue = prevValues[5].ToString();
                    ddl_start_with.Text = prevValues[7].ToString();
                    RetrieveDataListGrid();
                    txtb_search.Text = prevValues[8].ToString();
                    SearchData(prevValues[8].ToString());
                }
            }
        }

        //********************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            ViewState["chkIncludeHistory"] = "N";

            RetrieveYear();
            RetrieveEmploymentType();
            RetriveTemplate();
            //RetrieveEmpl();
            RetrieveDataListGrid();
            
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopLoading", "openLoading();", true);
            //System.Threading.Thread.Sleep(5000);
            
            up_dataListGrid.Update();
            dataListGrid = MyCmn.RetrieveData("sp_payrollemployeemaster_NTHP_paslip", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_letter", ddl_start_with.SelectedValue.ToString().Trim(), "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim());
            
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();

            SearchData(txtb_search.Text);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopLoadingClose", "closeLoading();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetrieveEmploymentType()
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
        //  BEGIN - VJA- 01/17/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_employee_name.Text  = "";
            txtb_empl_id.Text        = "";
            txtb_payroll_year.Text   = "";
            txtb_payroll_month.Text  = "";
            txtb_or_nbr.Text         = "";
            txtb_or_date.Text        = "";
            LblRequired1.Text        = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popshowdate", "show_date();", true);
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
            SearchData(txtb_search.Text);
        }
        protected void SearchData(string search)
        {
            string searchExpression = "empl_id LIKE '%" + search.Trim().Replace("'", "''") + "%' " +
                                      "OR employee_name LIKE '%" + search.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
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
            FieldValidationColorChanged(false, "ALL");

            //if (ddl_empl_name.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            //{
            //    FieldValidationColorChanged(true, "ddl_empl_name");
            //    validatedSaved = false;
            //}
            //if (CommonCode.checkisdatetime(txtb_effective_date.Text) == false)
            //{
            //    FieldValidationColorChanged(true, "txtb_effective_date");
            //    validatedSaved = false;
            //}


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

                    default: break;
                        //case "ddl_empl_name":
                        //    {
                        //        LblRequired12.Text = MyCmn.CONST_RQDFLD;
                        //        ddl_empl_name.BorderColor = Color.Red;
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                        //        break;
                        //    }
                        //case "txtb_effective_date":
                        //    {
                        //        LblRequired13.Text = MyCmn.CONST_RQDFLD;
                        //        txtb_effective_date.BorderColor = Color.Red;
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                        //        break;
                        //    }
                        //case "already-exist":
                        //    {
                        //        LblRequired12.Text = "Already Exist";
                        //        LblRequired13.Text = "Already Exist";
                        //        txtb_effective_date.BorderColor = Color.Red;
                        //        ddl_empl_name.BorderColor = Color.Red;
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                        //        break;
                        //    }
                        //case "already-exist-master":
                        //    {
                        //        LblRequired12.Text = "This effective date is already exist on Employee Master";
                        //        ddl_empl_name.BorderColor = Color.Red;
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                        //        break;
                        //    }

                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    default: break;
                    //case "ALL":
                    //    {
                    //        LblRequired12.Text = "";
                    //        LblRequired13.Text = "";
                    //        ddl_empl_name.BorderColor = Color.LightGray;
                    //        txtb_effective_date.BorderColor = Color.LightGray;
                    //        UpdatePanelEffec.Update();
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                    //        break;
                    //    }

                }
            }
        }
        //***********************************************
        //  BEGIN - JADE- 05/21/2019 - Retrieve Year 
        //***********************************************
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
       
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            //RetriveTemplate();
        }

        protected void ddl_start_with_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Open the modal Select Report
        //*************************************************************************
        protected void imgbtn_print_Command1(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            lnkPrint.CommandArgument = e.CommandArgument.ToString();
            ClearEntry();
            
            txtb_employee_name.Text = row2Edit[0]["employee_name"].ToString();
            txtb_empl_id.Text       = row2Edit[0]["empl_id"].ToString();
            txtb_payroll_year.Text  = ddl_year.SelectedItem.ToString();
            txtb_payroll_month.Text = ddl_month.SelectedItem.ToString();
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopReport_Show", "openSelectReport();", true);
            
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveTemplate()
        {
            ddl_payroll_template.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list6", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }

        protected void ddl_select_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetriveTemplate();
            LblRequired1.Text = "";
            lbl_or_nbr.Visible = false;
            txtb_or_nbr.Visible = false;
            lbl_or_date.Visible = false;
            txtb_or_date.Visible = false;
            lbl_payroll_template.Visible = true;
            ddl_payroll_template.Visible = true;


            lbl_purpose.Visible             = false;
            ddl_purpose.Visible             = false;
            lbl_purpose_override.Visible    = false;
            txtb_purpose_override.Visible   = false;

            if (ddl_select_report.SelectedValue == "01") // Net Take Home Pay
            {
                lbl_payroll_template.Visible = false;
                ddl_payroll_template.Visible = false;
                lbl_or_nbr.Visible = true;
                txtb_or_nbr.Visible = true;
                lbl_or_date.Visible = true;
                txtb_or_date.Visible = true;
                
                lbl_purpose.Visible             = true;
                ddl_purpose.Visible             = true;
                lbl_purpose_override.Visible    = true;
                txtb_purpose_override.Visible   = true;
            }

            Update_or_date.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopSHowDate", "show_date();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - 
        //*************************************************************************
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            DataTable dt_check = new DataTable();
            DataTable dt_NTHP_CHK = new DataTable();
            string procedure;
            string url = "";
            string printreport;
            bool can_print = false;
            Session["history_page"] = Request.Url.AbsolutePath;
            Session["PreviousValuesonPage_cNTHP_payslip"] = ddl_year.SelectedValue.ToString() + "," + ddl_month.SelectedValue.ToString().Trim() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_payroll_template.SelectedValue.ToString() + "," + gv_dataListGrid.PageIndex + "," + DropDownListID.SelectedValue.ToString() + "," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + "," + ddl_start_with.SelectedValue.ToString().Trim() + "," + txtb_search.Text.ToString().Trim();
            Update_or_date.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopSHowDate", "show_date();", true);
            LblRequired1.Text = "";

            var message_descr = "";
            if (ddl_select_report.SelectedValue == "01")
            {
                switch (ddl_empl_type.SelectedValue)
                {
                    case "RE":
                    case "CE":
                        dt_NTHP_CHK = MyCmn.RetrieveData("sp_payrollregistry_takehome_chk", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());
                        // if (dt_NTHP_CHK.Rows != null || dt_NTHP_CHK != null)
                        if (dt_NTHP_CHK.Rows.Count > 0)
                        {
                            double total_net_pay = 0;
                            for (int x = 0; x < dt_NTHP_CHK.Rows.Count; x++)
                            {
                                total_net_pay += total_net_pay + double.Parse(dt_NTHP_CHK.Rows[x]["net_pay"].ToString());
                                if (dt_NTHP_CHK.Rows[x]["post_status"].ToString() == "Y" && total_net_pay >= 5000)
                                {
                                    printreport = "/cryOtherPayroll/cryNTHP/cryNTHP.rpt";
                                    procedure = "sp_payrollregistry_takehome";
                                    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_orno," + txtb_or_nbr.Text.ToString().Trim() + ",par_ordate," + txtb_or_date.Text.ToString().Trim() + ",par_purpose," + txtb_purpose_override.Text.ToString().Trim();
                                    can_print = true;
                                }
                                else if (total_net_pay < 5000)
                                {
                                    message_descr     += dt_NTHP_CHK.Rows[x]["payrolltemplate_descr"].ToString() + "-" + dt_NTHP_CHK.Rows[x]["net_pay"].ToString() + " <br>";
                                    LblRequired1.Text = message_descr + " Net Pay is Below 5k";
                                    can_print = false;
                                    return;
                                }
                                else
                                {
                                    message_descr += dt_NTHP_CHK.Rows[x]["payrolltemplate_descr"].ToString() + "-" + dt_NTHP_CHK.Rows[x]["post_status_descr"].ToString() + " <br>";
                                    LblRequired1.Text = message_descr;
                                    can_print = false;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            can_print = false;
                            LblRequired1.Text = "No Data Found!";
                            return;
                        }
                        
                        break;

                    case "JO":
                        dt_NTHP_CHK = MyCmn.RetrieveData("sp_payrollregistry_takehome_chk", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());
                        // if (dt_NTHP_CHK.Rows != null || dt_NTHP_CHK != null)
                        if (dt_NTHP_CHK.Rows.Count > 0)
                        {
                            for (int x = 0; x < dt_NTHP_CHK.Rows.Count; x++)
                            {
                                if (dt_NTHP_CHK.Rows.Count == 1 && dt_NTHP_CHK.Rows[0]["payrolltemplate_descr"].ToString() == "1st Quincena Payroll")
                                {
                                    message_descr = "No Payroll for 2nd Quincena";
                                    LblRequired1.Text = message_descr;
                                    can_print = false;
                                    return;
                                }
                                else if (dt_NTHP_CHK.Rows[x]["post_status"].ToString() != "Y")
                                {
                                    message_descr += dt_NTHP_CHK.Rows[x]["payrolltemplate_descr"].ToString() + "-" + dt_NTHP_CHK.Rows[x]["post_status_descr"].ToString() + " <br>";
                                    LblRequired1.Text = message_descr;
                                    can_print = false;
                                    return;
                                }
                                else
                                {
                                    printreport = "/cryOtherPayroll/cryNTHP/cryNTHP.rpt";
                                    procedure = "sp_payrollregistry_takehome";
                                    url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_orno," + txtb_or_nbr.Text.ToString().Trim() + ",par_ordate," + txtb_or_date.Text.ToString().Trim() + ",par_purpose," + txtb_purpose_override.Text.ToString().Trim();
                                    can_print = true;
                                }
                            }
                        }
                        else
                        {
                            can_print = false;
                            LblRequired1.Text = "No Data Found!";
                            return;
                        }
                        break;
                }

                if (url != "" && can_print == true)
                {
                    Response.Redirect(url);
                }
            }
            else if (ddl_select_report.SelectedValue == "02")
            {
                if (ddl_payroll_template.SelectedValue == "009" ||  // JO - Monthly payroll
                        ddl_payroll_template.SelectedValue == "010" ||  // JO - 1st Quincena
                        ddl_payroll_template.SelectedValue == "011"     // JO - 2nd Quincena
                        )   
                    {
                        printreport = "/cryJobOrderReports/cryPayslip/cryPaySlip.rpt";
                        procedure = "sp_payrollregistry_salary_payslip_jo_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + "213" + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                    }
                    else if (ddl_payroll_template.SelectedValue == "008")
                    {
                        printreport = "/cryCasualReports/cryPayslip/cryPaySlip.rpt";
                        procedure = "sp_payrollregistry_salary_payslip_ce_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + "214" + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                    }
                    else if (ddl_payroll_template.SelectedValue == "007")
                    {
                        printreport = "/cryRegularReports/cryPayslip/cryPaySlip.rpt";
                        procedure = "sp_payrollregistry_salary_payslip_re_rep";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + "212" + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                    }
                    else if (ddl_payroll_template.SelectedValue == "041" ||  // Subsistence - RE
                            ddl_payroll_template.SelectedValue  == "021")   // Subsistence - CE
                    {
                        procedure = "sp_payrollregistry_payslip";
                        printreport = "/cryOtherPayroll/cryPayslip/cryPS_Subsistence.rpt";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

                    }
                    else if (ddl_payroll_template.SelectedValue == "023")  // RATA - RE
                    {
                        procedure = "sp_payrollregistry_payslip";
                        printreport = "/cryOtherPayroll/cryPayslip/cryPS_RATA.rpt";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                    }
                    else if (ddl_payroll_template.SelectedValue == "022" ||   // Overtime Payroll - RE
                             ddl_payroll_template.SelectedValue == "042" ||   // Overtime Payroll - CE
                             ddl_payroll_template.SelectedValue == "061")  // Overtime Payroll - JO
                    {
                        procedure = "sp_payrollregistry_payslip";
                        printreport = "/cryOtherPayroll/cryPayslip/cryPS_Ovtm.rpt";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                    }
                    else
                    {
                        procedure = "sp_payrollregistry_payslip";
                        printreport = "/cryOtherPayroll/cryPayslip/cryPS_OtherSal.rpt";
                        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
                    }

                    LblRequired1.Text = "";
                    dt_check = new DataTable();
                    if (procedure == "sp_payrollregistry_takehome")
                    {
                        dt_check = MyCmn.RetrieveData(procedure, "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim(), "par_orno", txtb_or_nbr.Text.ToString().Trim(), "par_ordate", txtb_or_date.Text, "par_purpose", txtb_purpose_override.Text.ToString().Trim());
                        if (dt_check.Rows.Count > 0)
                        {
                            can_print = true;
                        }
                        else
                        {
                            can_print = false;
                            LblRequired1.Text = "No Data Found!";
                        }
                    }
                    else if (procedure == "sp_payrollregistry_payslip")
                    {
                        dt_check = MyCmn.RetrieveData(procedure, "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", "", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());
                        if (dt_check.Rows.Count > 0)
                        {
                            can_print = true;
                        }
                        else
                        {
                            can_print = false;
                            LblRequired1.Text = "No Data Found!";
                        }
                    }
                    else if (procedure == "sp_payrollregistry_salary_payslip_jo_rep" ||
                             procedure == "sp_payrollregistry_salary_payslip_ce_rep" ||
                             procedure == "sp_payrollregistry_salary_payslip_re_rep")
                    {
                        dt_check = MyCmn.RetrieveData(procedure, "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", "", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());
                        if (dt_check.Rows.Count > 0)
                        {
                            can_print = true;
                        }
                        else
                        {
                            can_print = false;
                            LblRequired1.Text = "No Data Found!";
                        }
                    }

                if (url != "" && can_print == true)
                {
                    Response.Redirect(url);
                }
            }

            //switch (ddl_select_report.SelectedValue)
            //{
            //    case "01": // Net Take Home Pay 
            //        printreport = "/cryOtherPayroll/cryNTHP/cryNTHP.rpt";
            //        procedure = "sp_payrollregistry_takehome";
            //        url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim() + ",par_orno," + txtb_or_nbr.Text.ToString().Trim() + ",par_ordate," + txtb_or_date.Text.ToString().Trim() + ",par_purpose," + txtb_purpose_override.Text.ToString().Trim();

            //        LblRequired1.Text = "";
            //        dt_check = new DataTable();
            //        if (procedure == "sp_payrollregistry_takehome")
            //        {
            //            if (ddl_empl_type.SelectedValue == "JO")
            //            {
            //                DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_salary_payslip_jo_rep", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", "", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());

            //                for (int x = 0; x < dt.Rows.Count; x++)
            //                {
            //                    if (dt.Rows[x]["post_status"].ToString() == "N")
            //                    {
            //                        can_print = false;
            //                        LblRequired1.Text = dt.Rows[x]["payrolltemplate_descr"].ToString() + " - Not Posted";
            //                    }
            //                    else if (dt == null || dt.Rows.Count < 0)
            //                    {
            //                        can_print = false;
            //                        LblRequired1.Text = "No Data Found!";
            //                    }
            //                    else
            //                    {
            //                        can_print = true;
            //                        LblRequired1.Text = "";
            //                    }
            //                }
            //            }
            //            else if (ddl_empl_type.SelectedValue == "RE")
            //            {
            //                DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_salary_payslip_re_rep", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", "", "par_payrolltemplate_code", "007", "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());
                           
            //                double net_pay = 0;
            //                net_pay = net_pay + double.Parse(dt.Rows[0]["net_pay"].ToString());
                           
            //                if (net_pay < 5000)
            //                {
            //                    can_print = false;
            //                    LblRequired1.Text = dt.Rows[0]["payrolltemplate_descr"].ToString() + " - Net Pay is Below 5k";
            //                }
            //                else if (dt == null || dt.Rows.Count < 0)
            //                {
            //                    can_print = false;
            //                    LblRequired1.Text = "No Data Found!";
            //                }
            //                else
            //                {
            //                    can_print = true;
            //                    LblRequired1.Text = "No Data Found!";
            //                }

            //            }
            //            else if (ddl_empl_type.SelectedValue == "CE")
            //            {
            //                DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_salary_payslip_ce_rep", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", "", "par_payrolltemplate_code", "008", "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());
                            
            //                double net_pay = 0;
            //                net_pay = net_pay + double.Parse(dt.Rows[0]["net_pay"].ToString());
                            
            //                if (net_pay < 5000)
            //                {
            //                    can_print = false;
            //                    LblRequired1.Text = dt.Rows[0]["payrolltemplate_descr"].ToString() + " - Net Pay is Below 5k";
            //                }
            //                else if (dt == null || dt.Rows.Count < 0)
            //                {
            //                    can_print = false;
            //                    LblRequired1.Text = "No Data Found!";
            //                }
            //                else
            //                {
            //                    can_print = true;
            //                    LblRequired1.Text = "No Data Found!";
            //                }
            //            }
            //            else
            //            {
            //                dt_check = MyCmn.RetrieveData(procedure, "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim(), "par_orno", txtb_or_nbr.Text.ToString().Trim(), "par_ordate", txtb_or_date.Text);
            //                if (dt_check.Rows.Count > 0)
            //                {
            //                    can_print = true;
            //                }
            //                else
            //                {
            //                    can_print = false;
            //                    LblRequired1.Text = "No Data Found!";
            //                }
            //            }
            //        }

            //        break;

            //    case "02": // PaySLip 
            //        if (ddl_payroll_template.SelectedValue == "009" ||  // JO - Monthly payroll
            //            ddl_payroll_template.SelectedValue == "010" ||  // JO - 1st Quincena
            //            ddl_payroll_template.SelectedValue == "011"     // JO - 2nd Quincena
            //            )   
            //        {
            //            printreport = "/cryJobOrderReports/cryPayslip/cryPaySlip.rpt";
            //            procedure = "sp_payrollregistry_salary_payslip_jo_rep";
            //            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + "213" + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
            //        }
            //        else if (ddl_payroll_template.SelectedValue == "008")
            //        {
            //            printreport = "/cryCasualReports/cryPayslip/cryPaySlip.rpt";
            //            procedure = "sp_payrollregistry_salary_payslip_ce_rep";
            //            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + "214" + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
            //        }
            //        else if (ddl_payroll_template.SelectedValue == "007")
            //        {
            //            printreport = "/cryRegularReports/cryPayslip/cryPaySlip.rpt";
            //            procedure = "sp_payrollregistry_salary_payslip_re_rep";
            //            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + "212" + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
            //        }
            //        else if (ddl_payroll_template.SelectedValue == "041" ||  // Subsistence - RE
            //                ddl_payroll_template.SelectedValue  == "021")   // Subsistence - CE
            //        {
            //            procedure = "sp_payrollregistry_payslip";
            //            printreport = "/cryOtherPayroll/cryPayslip/cryPS_Subsistence.rpt";
            //            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();

            //        }
            //        else if (ddl_payroll_template.SelectedValue == "023")  // RATA - RE
            //        {
            //            procedure = "sp_payrollregistry_payslip";
            //            printreport = "/cryOtherPayroll/cryPayslip/cryPS_RATA.rpt";
            //            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
            //        }
            //        else if (ddl_payroll_template.SelectedValue == "022" ||   // Overtime Payroll - RE
            //                 ddl_payroll_template.SelectedValue == "042" ||   // Overtime Payroll - CE
            //                 ddl_payroll_template.SelectedValue == "061")  // Overtime Payroll - JO
            //        {
            //            procedure = "sp_payrollregistry_payslip";
            //            printreport = "/cryOtherPayroll/cryPayslip/cryPS_Ovtm.rpt";
            //            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
            //        }
            //        else
            //        {
            //            procedure = "sp_payrollregistry_payslip";
            //            printreport = "/cryOtherPayroll/cryPayslip/cryPS_OtherSal.rpt";
            //            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_payroll_year," + ddl_year.SelectedValue.ToString().Trim() + ",par_payroll_month," + ddl_month.SelectedValue.ToString().Trim() + ",par_employment_type," + ddl_empl_type.SelectedValue.ToString().Trim() + ",par_payroll_registry_nbr," + "" + ",par_payrolltemplate_code," + ddl_payroll_template.SelectedValue.ToString().Trim() + ",par_empl_id," + lnkPrint.CommandArgument.Split(',')[0].ToString().Trim();
            //        }

            //        LblRequired1.Text = "";
            //        dt_check = new DataTable();
            //        if (procedure == "sp_payrollregistry_takehome")
            //        {
            //            dt_check = MyCmn.RetrieveData(procedure, "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim(), "par_orno", txtb_or_nbr.Text.ToString().Trim(), "par_ordate", txtb_or_date.Text, "par_purpose", txtb_purpose_override.Text.ToString().Trim());
            //            if (dt_check.Rows.Count > 0)
            //            {
            //                can_print = true;
            //            }
            //            else
            //            {
            //                can_print = false;
            //                LblRequired1.Text = "No Data Found!";
            //            }
            //        }
            //        else if (procedure == "sp_payrollregistry_payslip")
            //        {
            //            dt_check = MyCmn.RetrieveData(procedure, "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", "", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());
            //            if (dt_check.Rows.Count > 0)
            //            {
            //                can_print = true;
            //            }
            //            else
            //            {
            //                can_print = false;
            //                LblRequired1.Text = "No Data Found!";
            //            }
            //        }
            //        else if (procedure == "sp_payrollregistry_salary_payslip_jo_rep" ||
            //                 procedure == "sp_payrollregistry_salary_payslip_ce_rep" ||
            //                 procedure == "sp_payrollregistry_salary_payslip_re_rep")
            //        {
            //            dt_check = MyCmn.RetrieveData(procedure, "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", "", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_empl_id", lnkPrint.CommandArgument.Split(',')[0].ToString().Trim());
            //            if (dt_check.Rows.Count > 0)
            //            {
            //                can_print = true;
            //            }
            //            else
            //            {
            //                can_print = false;
            //                LblRequired1.Text = "No Data Found!";
            //            }
            //        }
            //        break;
            //}


            //if (url != "" && can_print == true)
            //{
            //    Response.Redirect(url);
            //}

        }

        protected void ddl_purpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtb_purpose_override.Text = "";
            txtb_purpose_override.Text = ddl_purpose.SelectedValue.ToString();

            Update_or_date.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopSHowDate", "show_date();", true);
        }
        //*************************************************************************
        //  END OF CODE
        //*************************************************************************
    }
}