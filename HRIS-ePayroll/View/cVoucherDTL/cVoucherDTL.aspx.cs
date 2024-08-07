//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     06/06/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View
{
    public partial class cVoucherDTL : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 06/06/2019 - Data Place holder creation 
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
        DataTable dtSource_oth_claims
        {
            get
            {
                if ((DataTable)ViewState["dtSource_oth_claims"] == null) return null;
                return (DataTable)ViewState["dtSource_oth_claims"];
            }
            set
            {
                ViewState["dtSource_oth_claims"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 06/06/2019 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 06/06/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "voucher_ctrl_nbr";
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

                // Year
                // Month
                // Employment Type
                // Payroll Template
                // Page Index
                // Show Entries
                // Registry No
                // Department

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
                    ViewState["page_allow_print"] = 1;
                }

                if (Session["PreviousValuesonPage_cVoucherHdr"] == null)
                    Session["PreviousValuesonPage_cVoucherHdr"] = "";
                else if (Session["PreviousValuesonPage_cVoucherHdr"].ToString() != string.Empty)
                {
                    RetrieveYear();
                    string[] prevValues = Session["PreviousValuesonPage_cVoucherHdr"].ToString().Split(new char[] { ',' });
                    ddl_year.SelectedValue              = prevValues[0].ToString();
                    ddl_month.SelectedValue             = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue         = prevValues[2].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue  = prevValues[3].ToString();
                    DropDownListID.SelectedValue        = prevValues[5].ToString();
                    ddl_dep.SelectedValue               = Session["PreviousValuesonPage_cVoucherHdr_department"].ToString().Trim();
                    //RetrieveHeader();
                    //ddl_header.SelectedValue            = prevValues[6].ToString();
                    voucher_nbr.Text                    = prevValues[6].ToString();
                    txtb_empl_id.Text                   = prevValues[7].ToString();
                    RetrieveDataListGrid();
                    gv_dataListGrid.PageIndex           = int.Parse(prevValues[4].ToString());
                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                    up_dataListGrid.Update();
                    btnAdd.Visible = true;
                    lbl_rate_basis_hidden.Text      = Session["PreviousValuesonPage_cVoucherHdr_rate_basis"].ToString().Trim();
                    lbl_monthly_rate_hidden.Text    = Session["PreviousValuesonPage_cVoucherHdr_monthly_rate"].ToString().Trim();
                    lbl_daily_rate_hidden.Text      = Session["PreviousValuesonPage_cVoucherHdr_daily_rate"].ToString().Trim();
                    lbl_hourly_rate_hidden.Text     = Session["PreviousValuesonPage_cVoucherHdr_hourly_rate"].ToString().Trim();

                    if (Session["PreviousValuesonPage_cVoucherHdr_post_status"].ToString() == "Y" ||
                        Session["PreviousValuesonPage_cVoucherHdr_post_status"].ToString() == "R")
                    {
                        btnAdd.Visible = false;
                    }

                    RetrieveLoanPremiums_Visible();
                    RetrieveReserveDeduction();
                }
            }
        }

        /*  Remarks : Template Code and Description for Voucher
         * 
             CODE   |           DESCRIPTION                    EMPLOYMENT TYPE
            601     | Refund to Employee                     | RE
            602     | Refund to Employeer - Salary           | RE
            603     | Terminal Leave                         | RE
            604     | Honorarium                             | RE
            605     | Other Salaries                         | RE
            606     | Refund to Employeer - MidYear Bonus    | RE
            607     | Refund to Employeer - YearEnd Bonus    | RE

            801     | Refund to Employee                     | JO
            802     | Refund to Employeer - Salary           | JO
            803     | Terminal Leave                         | JO
            804     | Honorarium                             | JO
            805     | Other Salaries                         | JO

            701     | Refund to Employee                     | CE
            702     | Refund to Employeer - Salary           | CE
            703     | Terminal Leave                         | CE
            704     | Honorarium                             | CE
            705     | Other Salaries                         | CE
            706     | Refund to Employeer - MidYear Bonus    | CE
            707     | Refund to Employeer - YearEnd Bonus    | CE
            
        */

        //********************************************************************
        //  BEGIN - VJA- 06/06/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {

            Session["sortdirection"] = SortDirection.Ascending.ToString();

            RetrieveYear();
            RetriveTemplate();
            RetriveEmploymentType();
            //RetrieveHeader();
            RetrieveDataListGrid();

            //Retrieve When Add
            RetrieveBindingDep();
            //RetrieveBindingSubDep();
            //RetrieveBindingDivision();
            //RetrieveBindingSection();
            //RetrieveEmployeename();
            //RetrieveBindingFundcharges();
            //RetrieveLastVoucherNumber();
            //RetrieveHeader();

            btnAdd.Visible = false;
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Populate Combo list for Employment Type
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
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Populate Combo list for Department
        //*************************************************************************
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
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Populate Combo list for Payroll Year
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
        //  BEGIN - VJA- 06/06/2019 - Populate Combo list for Template
        //*************************************************************************
        private void RetriveTemplate()
        {
            ddl_payroll_template.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list5", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);

            payrolltemplate_code.Items.Clear();
            DataTable chk = new DataTable();
            //string query = "SELECT * FROM payrolltemplate_tbl WHERE payrolltemplate_type IN ('01','07','08') AND employment_type = '"+ ddl_empl_type.SelectedValue.ToString().Trim() + "'";
            string query = "SELECT * FROM payrolltemplate_tbl WHERE payrolltemplate_type IN ('01','08') AND employment_type = '"+ ddl_empl_type.SelectedValue.ToString().Trim() + "'";
            chk = MyCmn.GetDatatable(query);
            payrolltemplate_code.DataSource = chk;
            payrolltemplate_code.DataValueField = "payrolltemplate_code";
            payrolltemplate_code.DataTextField = "payrolltemplate_descr";
            payrolltemplate_code.DataBind();
            payrolltemplate_code.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        //private void RetrieveHeader()
        //{
        //    ddl_header.Items.Clear();
        //    DataTable dt = MyCmn.RetrieveData("sp_voucher_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", Session["PreviousValuesonPage_cVoucherHdr_department_grid"].ToString().Trim());
        //    ddl_header.DataSource = dt;
        //    ddl_header.DataValueField = "voucher_ctrl_nbr";
        //    ddl_header.DataTextField  = "voucher_ctrl_nbr";
        //    ddl_header.DataBind();
        //    ListItem li = new ListItem("-- Select Here --", "");
        //    ddl_header.Items.Insert(0, li);

        //}
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_voucher_dtl_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_voucher_ctrl_nbr", voucher_nbr.Text.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            
            string editExpression = "voucher_ctrl_nbr = '" + voucher_nbr.Text.ToString().Trim() + "' ";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            
            // txtb_empl_id.Text               = dataListGrid.Rows[0]["empl_id"].ToString();
            txtb_employeename.Text          = Session["PreviousValuesonPage_cVoucherHdr_employee_name"].ToString();
            txtb_main_employee_name.Text    = Session["PreviousValuesonPage_cVoucherHdr_employee_name"].ToString();

        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            InitializeTable_oth_claims();
            AddPrimaryKeys_oth_claims();
            AddNewRow_oth_claims();

            lbl_if_dateposted_yes.Text = "";
            btnSave.Visible = true;

            txtb_employeename.Visible = true;
            RetrieveSeqNbr();

            switch (lbl_rate_basis_hidden.Text)
            {
                case "M":
                    {
                        txtb_rate_amount.Text = lbl_monthly_rate_hidden.Text.ToString();
                        lbl_rate_descr.Text = "Monthly Rate:";
                        break;
                    }
                case "D":
                    {
                        txtb_rate_amount.Text = lbl_daily_rate_hidden.Text.ToString();
                        lbl_rate_descr.Text = "Daily Rate:";
                        break;
                    }
                case "H":
                    {
                        txtb_rate_amount.Text = lbl_hourly_rate_hidden.Text.ToString();
                        lbl_rate_descr.Text = "Hourly Rate:";
                        break;
                    }
            }
            // END   - VJA : 05/29/2019 - Details


            btnSave.Visible = true;
            txtb_voucher_nbr.Enabled = false;
            btn_calculate.Visible = true;
            lbl_if_dateposted_yes.Text = "";
            ToogleTextbox(true);

            LabelAddEdit.Text = "Add Record | Voucher Ctrl No: " + voucher_nbr.Text.ToString().Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
            ViewState.Add("AddEdit_Mode_BrkDwn", "BRK_ADD");
            lbl_addeditmode_hidden.Text = MyCmn.CONST_ADD;
            FieldValidationColorChanged(false, "ALL");

            // This is To Toogle Modal When Add or Edit Based from Template Code
            ToogleModal();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Toogle Modal During Add Or Edit
        //*************************************************************************
        private void ToogleModal()
        {
            FieldValidationColorChanged(false, "ALL");

            id_oth_claims_refund.Visible = false;
            payrolltemplate_code.Enabled = false;
            is_differential.Enabled      = false;

            switch (ddl_payroll_template.SelectedValue)
            {
                // Template Code for : Refund To Employee 
                case "601":
                case "701":
                case "801":
                    id_mandatory.Visible            = true;
                    id_optional.Visible             = true;
                    id_loans.Visible                = true;
                    div_lwop.Visible                = true;
                    div_deductions.Visible          = true;
                    div_tax.Visible                 = false;
                    div_gross.Visible               = false;
                    lbl_netpay_descr.Text           = "Refund Total :";
                    div_no_of_days.Visible          = false;
                    txtb_gross_pay.Enabled          = true;
                    div_amount1.Visible             = false;
                    lbl_other_amount1_descr.Text    = "";
                    div_amount2.Visible             = false;
                    lbl_other_amount2_descr.Text    = "";
                    div_amount3.Visible             = false;
                    lbl_other_amount3_descr.Text    = "";
                    div_netpay.Visible              = true;
                    div_amount4.Visible             = false;
                    lbl_refund_sal_amt.Text         = "";
                    break;

                // Template Code for : Honorarium
                case "604":
                case "704":
                case "804":
                    id_mandatory.Visible            = false;
                    id_optional.Visible             = false;
                    id_loans.Visible                = false;
                    div_lwop.Visible                = false;
                    div_deductions.Visible          = false;
                    div_tax.Visible                 = true;
                    div_gross.Visible               = true;
                    lbl_netpay_descr.Text           = "Net Pay (Hon.):";
                    div_no_of_days.Visible          = false;
                    txtb_gross_pay.Enabled          = true;
                    div_amount1.Visible             = false;
                    lbl_other_amount1_descr.Text    = "";
                    div_amount2.Visible             = false;
                    lbl_other_amount2_descr.Text    = "";
                    div_amount3.Visible             = false;
                    lbl_other_amount3_descr.Text    = "";
                    div_netpay.Visible              = true;
                    div_amount4.Visible             = false;
                    lbl_refund_sal_amt.Text         = "";
                    break;

                // Template Code for : Terminal Leave
                case "603":
                case "703":
                case "803":
                    id_mandatory.Visible            = true;
                    id_optional.Visible             = true;
                    id_loans.Visible                = true;
                    div_lwop.Visible                = false;
                    div_deductions.Visible          = true;
                    div_tax.Visible                 = false;
                    div_gross.Visible               = true;
                    lbl_netpay_descr.Text           = "Net Pay (Term.):";
                    div_no_of_days.Visible          = true;
                    txtb_gross_pay.Enabled          = false;
                    div_amount1.Visible             = false;
                    lbl_other_amount1_descr.Text    = "";
                    div_amount2.Visible             = false;
                    lbl_other_amount2_descr.Text    = "";
                    div_amount3.Visible             = false;
                    lbl_other_amount3_descr.Text    = "";
                    div_netpay.Visible              = true;
                    div_amount4.Visible             = true;
                    lbl_refund_sal_amt.Text         = "Refund:";
                    break;

                // Template Code for : Other Salaries
                case "605":
                case "705":
                case "805":
                    id_mandatory.Visible            = true;
                    id_optional.Visible             = true;
                    id_loans.Visible                = true;
                    div_deductions.Visible          = true;
                    div_tax.Visible                 = false;
                    div_gross.Visible               = true;
                    lbl_netpay_descr.Text           = "Net Pay:";
                    div_no_of_days.Visible          = false;
                    txtb_gross_pay.Enabled          = true;
                    
                    if (Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "1")      // Other Salaries - First Claim (Promotion)
                    {
                        
                        div_lwop.Visible                = false;
                        div_amount1.Visible             = true;
                        lbl_other_amount1_descr.Text    = "PERA Amt.";
                        div_amount2.Visible             = true;
                        lbl_other_amount2_descr.Text    = "Authorized Sal. :";
                        div_amount3.Visible             = true;
                        lbl_other_amount3_descr.Text    = "Received Sal. :";
                        div_netpay.Visible              = true;
                    }
                    if (Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "2")      // Other Salaries - Sal. Diff (Multiple Months)
                    {

                        div_lwop.Visible                = true;
                        div_amount1.Visible             = true;
                        lbl_other_amount1_descr.Text    = "PERA Amt.";
                        div_amount2.Visible             = true;
                        lbl_other_amount2_descr.Text    = "Authorized Sal. :";
                        div_amount3.Visible             = true;
                        lbl_other_amount3_descr.Text    = "Received Sal. :";
                        div_netpay.Visible              = true;
                    }
                    
                    if (Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "3")      // Other Salaries - Other Sal. (Multiple Months)
                    {

                        id_mandatory.Visible            = true;
                        id_optional.Visible             = true;
                        id_loans.Visible                = true;
                        div_lwop.Visible                = true;
                        div_deductions.Visible          = true;
                        div_tax.Visible                 = false;
                        div_gross.Visible               = true;
                        lbl_netpay_descr.Text           = "Net Pay:";

                        if (ddl_empl_type.SelectedValue == "RE")
                        {
                            div_no_of_days.Visible      = false;
                        }
                        else
                        {
                            div_no_of_days.Visible      = true;
                        }

                        txtb_gross_pay.Enabled          = false;
                        div_amount1.Visible             = true;
                        lbl_other_amount1_descr.Text    = "PERA Amt.";
                        div_amount2.Visible             = false;
                        lbl_other_amount2_descr.Text    = "";
                        div_amount3.Visible             = false;
                        lbl_other_amount3_descr.Text    = "";
                        div_netpay.Visible              = true;
                        txtb_other_amount1.Enabled      = true;
                        btn_calculate.Visible           = true;
                    
                        div_amount4.Visible = true;
                        lbl_refund_sal_amt.Text = "Refund:";
                    }
                    break;

                // Template Code for : Refund to Employeer - Mid Year Bonus
                case "606":
                case "706":
                case "806":
                    id_mandatory.Visible            = false;
                    id_optional.Visible             = false;
                    id_loans.Visible                = false;
                    div_lwop.Visible                = false;
                    div_deductions.Visible          = false;
                    div_tax.Visible                 = false;
                    div_gross.Visible               = false;
                    lbl_netpay_descr.Text           = "Net Pay:";
                    div_no_of_days.Visible          = false;
                    txtb_gross_pay.Enabled          = true;
                    div_amount1.Visible             = true;
                    lbl_other_amount1_descr.Text    = "Mid Year Amt.";
                    div_amount2.Visible             = false;
                    lbl_other_amount2_descr.Text    = "";
                    div_amount3.Visible             = false;
                    lbl_other_amount3_descr.Text    = "";
                    div_netpay.Visible              = false;
                    div_amount4.Visible             = false;
                    lbl_refund_sal_amt.Text         = "";
                    break;

                // Template Code for : Refund to Employeer - Year End Bonus
                case "607":
                case "707":
                case "807":
                    id_mandatory.Visible            = false;
                    id_optional.Visible             = false;
                    id_loans.Visible                = false;
                    div_lwop.Visible                = false;
                    div_deductions.Visible          = false;
                    div_tax.Visible                 = false;
                    div_gross.Visible               = false;
                    lbl_netpay_descr.Text           = "Net Pay:";
                    div_no_of_days.Visible          = false;
                    txtb_gross_pay.Enabled          = true;
                    div_amount1.Visible             = true;
                    lbl_other_amount1_descr.Text    = "Year-End Amt.";
                    div_amount2.Visible             = true;
                    lbl_other_amount2_descr.Text    = "Cash Gift Amt.";
                    div_amount3.Visible             = false;
                    lbl_other_amount3_descr.Text    = "";
                    div_netpay.Visible              = true;
                    div_amount4.Visible             = false;
                    lbl_refund_sal_amt.Text         = "";
                    break;

                // Template Code for : Maternity
                case "608":
                case "708":
                case "808":
                    id_mandatory.Visible            = true;
                    id_optional.Visible             = true;
                    id_loans.Visible                = true;
                    div_lwop.Visible                = true;
                    div_deductions.Visible          = true;
                    div_tax.Visible                 = false;
                    div_gross.Visible               = true;
                    lbl_netpay_descr.Text           = "Net Pay (Mat.) :";

                    if (ddl_empl_type.SelectedValue == "RE")
                    {
                        div_no_of_days.Visible = false;
                    }
                    else
                    {
                        div_no_of_days.Visible = true;
                    }

                    txtb_gross_pay.Enabled          = false;
                    div_amount1.Visible             = true;
                    lbl_other_amount1_descr.Text    = "PERA Amt.";
                    div_amount2.Visible             = false;
                    lbl_other_amount2_descr.Text    = "";
                    div_amount3.Visible             = false;
                    lbl_other_amount3_descr.Text    = "";
                    div_netpay.Visible              = true;
                    div_amount4.Visible             = true;
                    lbl_refund_sal_amt.Text         = "Refund:";
                    break;

                // Template Code for : Other Claims/Refund
                case "609":
                case "709":
                case "809":
                    id_mandatory.Visible            = false;
                    id_optional.Visible             = false;
                    id_loans.Visible                = false;
                    div_lwop.Visible                = false;
                    div_deductions.Visible          = false;
                    div_tax.Visible                 = false;
                    div_gross.Visible               = true;
                    lbl_netpay_descr.Text           = "Net Pay:";

                    if (ddl_empl_type.SelectedValue == "RE")
                    {
                        div_no_of_days.Visible      = false;
                    }
                    else
                    {
                        div_no_of_days.Visible      = true;
                    }

                    txtb_gross_pay.Enabled          = false;
                    div_amount1.Visible             = true;
                    lbl_other_amount1_descr.Text    = "Total Claims:";
                    div_amount2.Visible             = true;
                    lbl_other_amount2_descr.Text    = "Deduct to Gross:";
                    div_amount3.Visible             = true;
                    lbl_other_amount3_descr.Text    = "Total Refund:";
                    div_netpay.Visible              = true;
                    txtb_other_amount1.Enabled      = false;
                    txtb_other_amount2.Enabled      = false;
                    txtb_other_amount3.Enabled      = false;
                    btn_calculate.Visible           = true;
                    id_oth_claims_refund.Visible    = true;
                    div_amount4.Visible             = false;
                    lbl_refund_sal_amt.Text         = "";
                    break;

                case "610": // Other Claims - v2
                case "611": // Other Claims - v2
                case "612": // Other Claims - v2

                    lbl_netpay_descr.Text               = "Net Pay:";
                    lbl_other_amount1_descr.Text        = "PERA Amt.";
                    div_amount1.Visible                 = true;
                    payrolltemplate_code.Enabled        = true;
                    is_differential.Enabled             = true;
                    if (is_differential.Checked == true)
                    {
                        div_amount2.Visible             = true;
                        lbl_other_amount2_descr.Text    = "Authorized";
                        div_amount3.Visible             = true;
                        lbl_other_amount3_descr.Text    = "Received:";
                    }
                    else
                    {
                        div_amount2.Visible             = false;
                        lbl_other_amount2_descr.Text    = "Amount 2:";
                        div_amount3.Visible             = false;
                        lbl_other_amount3_descr.Text    = "Amount 3:";
                    }

                    break;

            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            hidden_seq_no.Text          = "";
            txtb_rate_amount.Text       = "0.00";
            txtb_no_of_days.Text        = "0.00";
            txtb_birtax_summary.Text    = "0.00";

            //Summary Tab                 
            txtb_gross_pay.Text         = "0.00";
            txtb_lwo_pay.Text           = "0.00";
            txtb_total_mandatory.Text   = "0.00";
            txtb_total_optional.Text    = "0.00";
            txtb_total_loans.Text       = "0.00";
            txtb_net_pay.Text           = "0.00";
            txtb_lwop_amount_pera.Text  = "0.00";

            //Mandatory Deductions Tab
            txtb_gsis_gs.Text           = "0.00";
            txtb_gsis_ps.Text           = "0.00";
            txtb_gsis_sif.Text          = "0.00";
            txtb_hdmf_gs.Text           = "0.00";
            txtb_hdmf_ps.Text           = "0.00";
            txtb_phic_gs.Text           = "0.00";
            txtb_phic_ps.Text           = "0.00";
            txtb_bir_tax.Text           = "0.00";

            //Optional Deduction Tab
            txtb_sss.Text               = "0.00";
            txtb_hdmf_addl.Text         = "0.00";
            txtb_philam.Text            = "0.00";
            txtb_gsis_ehp.Text          = "0.00";
            txtb_gsis_hip.Text          = "0.00";
            txtb_gsis_ceap.Text         = "0.00";
            txtb_gsis_add.Text          = "0.00";
            txtb_hdmf_mp2.Text          = "0.00";
            txtb_hdmf_loyalty_card.Text = "0.00";

            //Loans Tab
            txtb_gsis_consolidated.Text     = "0.00";
            txtb_gsis_policy_regular.Text   = "0.00";
            txtb_gsis_policy_optional.Text  = "0.00";
            txtb_gsis_ouli_loan.Text        = "0.00";
            txtb_gsis_emer_loan.Text        = "0.00";
            txtb_gsis_ecard_loan.Text       = "0.00";
            txtb_gsis_educ_loan.Text        = "0.00";
            txtb_gsis_real_loan.Text        = "0.00";
            txtb_gsis_sos_loan.Text         = "0.00";
            txtb_hdmf_mpl_loan.Text         = "0.00";
            txtb_hdmf_house_loan.Text       = "0.00";
            txtb_hdmf_cal_loan.Text         = "0.00";
            txtb_ccmpc_loan.Text            = "0.00";
            txtb_nico_loan.Text             = "0.00";
            txtb_networkbank_loan.Text      = "0.00";

            // Add Field 03/12/2019
            txtb_nhmfc_hsng.Text            = "0.00";
            txtb_nafc.Text                  = "0.00";

            // Add Field Again 03/14/2019
            txtb_gsis_help.Text             = "0.00";
            txtb_gsis_housing_loan.Text     = "0.00";

            txtb_other_amount1.Text         = "0.00";
            txtb_other_amount2.Text         = "0.00";
            txtb_other_amount3.Text         = "0.00";

            txtb_period_from.Text           = "";
            txtb_period_to.Text             = "";

            //Added by Jorge: 07/01/2019
            txtb_voucher_nbr.Text           = "";
            ViewState["created_by_user"]    = "";
            ViewState["updated_by_user"]    = "";
            ViewState["posted_by_user"]     = "";
            ViewState["created_dttm"]       = "";
            ViewState["updated_dttm"]       = "";
            txtb_date_posted.Text           = "";
            txtb_status.Text                = "";
            lbl_if_dateposted_yes.Text      = "";

            txtb_otherloan_no1.Text         = "0.00";
            txtb_otherloan_no2.Text         = "0.00";
            txtb_otherloan_no3.Text         = "0.00";
            txtb_otherloan_no4.Text         = "0.00";
            txtb_otherloan_no5.Text         = "0.00";
            txtb_otherpremium_no1.Text      = "0.00";
            txtb_otherpremium_no2.Text      = "0.00";
            txtb_otherpremium_no3.Text      = "0.00";
            txtb_otherpremium_no4.Text      = "0.00";
            txtb_otherpremium_no5.Text      = "0.00";

            txtb_payroll_descr1.Text         = "";
            txtb_payroll_descr2.Text         = "";
            txtb_payroll_descr3.Text         = "";
            txtb_payroll_descr4.Text         = "";
            txtb_payroll_descr5.Text         = "";
            ddl_payroll_type_1.SelectedValue = "01";
            ddl_payroll_type_2.SelectedValue = "01";
            ddl_payroll_type_3.SelectedValue = "01";
            ddl_payroll_type_4.SelectedValue = "01";
            ddl_payroll_type_5.SelectedValue = "01";
            txtb_payroll_amt1.Text           = "0.00";
            txtb_payroll_amt2.Text           = "0.00";
            txtb_payroll_amt3.Text           = "0.00";
            txtb_payroll_amt4.Text           = "0.00";
            txtb_payroll_amt5.Text           = "0.00";
            
            // Add Field Again 2022-05-30
            txtb_other_ded_mand1.Text   =  "0.00";
            txtb_other_ded_mand2.Text   =  "0.00";
            txtb_other_ded_mand3.Text   =  "0.00";
            txtb_other_ded_mand4.Text   =  "0.00";
            txtb_other_ded_mand5.Text   =  "0.00";
            txtb_other_ded_mand6.Text   =  "0.00";
            txtb_other_ded_mand7.Text   =  "0.00";
            txtb_other_ded_mand8.Text   =  "0.00";
            txtb_other_ded_mand9.Text   =  "0.00";
            txtb_other_ded_mand10.Text  =  "0.00";
            txtb_other_ded_prem1.Text   =  "0.00";
            txtb_other_ded_prem2.Text   =  "0.00";
            txtb_other_ded_prem3.Text   =  "0.00";
            txtb_other_ded_prem4.Text   =  "0.00";
            txtb_other_ded_prem5.Text   =  "0.00";
            txtb_other_ded_prem6.Text   =  "0.00";
            txtb_other_ded_prem7.Text   =  "0.00";
            txtb_other_ded_prem8.Text   =  "0.00";
            txtb_other_ded_prem9.Text   =  "0.00";
            txtb_other_ded_prem10.Text  =  "0.00";
            txtb_other_ded_loan1.Text   =  "0.00";
            txtb_other_ded_loan2.Text   =  "0.00";
            txtb_other_ded_loan3.Text   =  "0.00";
            txtb_other_ded_loan4.Text   =  "0.00";
            txtb_other_ded_loan5.Text   =  "0.00";
            txtb_other_ded_loan6.Text   =  "0.00";
            txtb_other_ded_loan7.Text   =  "0.00";
            txtb_other_ded_loan8.Text   =  "0.00";
            txtb_other_ded_loan9.Text   =  "0.00";
            txtb_other_ded_loan10.Text  =  "0.00";

            txtb_lates_min.Text         = "0.00";
            txtb_lates_amount.Text      = "0.00";
            refund_sal_amt.Text     = "0.00";

            payrolltemplate_code.SelectedValue  = "";
            voucher_dtl_descr.Text              = "";
            is_differential.Checked             = false;

            FieldValidationColorChanged(false, "ALL");
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("voucher_ctrl_nbr", typeof(System.String));
            dtSource.Columns.Add("seq_no", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("voucher_period_from", typeof(System.String));
            dtSource.Columns.Add("voucher_period_to", typeof(System.String));
            dtSource.Columns.Add("gross_pay", typeof(System.String));
            dtSource.Columns.Add("net_pay", typeof(System.String));
            dtSource.Columns.Add("rate_basis", typeof(System.String));
            dtSource.Columns.Add("monthly_rate", typeof(System.String));
            dtSource.Columns.Add("daily_rate", typeof(System.String));
            dtSource.Columns.Add("hourly_rate", typeof(System.String));
            dtSource.Columns.Add("no_of_days", typeof(System.String));
            dtSource.Columns.Add("other_amt1", typeof(System.String));
            dtSource.Columns.Add("other_amt2", typeof(System.String));
            dtSource.Columns.Add("other_amt3", typeof(System.String));
            dtSource.Columns.Add("wtax", typeof(System.String));
            dtSource.Columns.Add("lowp_amount_salary", typeof(System.String));
            dtSource.Columns.Add("lowp_amount_pera", typeof(System.String));
            dtSource.Columns.Add("gsis_gs", typeof(System.String));
            dtSource.Columns.Add("gsis_ps", typeof(System.String));
            dtSource.Columns.Add("sif_gs", typeof(System.String));
            dtSource.Columns.Add("hdmf_gs", typeof(System.String));
            dtSource.Columns.Add("hdmf_ps", typeof(System.String));
            dtSource.Columns.Add("phic_gs", typeof(System.String));
            dtSource.Columns.Add("phic_ps", typeof(System.String));
            dtSource.Columns.Add("nhmfc_hsing", typeof(System.String));
            dtSource.Columns.Add("nafc_svlf", typeof(System.String));
            dtSource.Columns.Add("sss_ps", typeof(System.String));
            dtSource.Columns.Add("hdmf_ps2", typeof(System.String));
            dtSource.Columns.Add("hdmf_mp2", typeof(System.String));
            dtSource.Columns.Add("philamlife_ps", typeof(System.String));
            dtSource.Columns.Add("gsis_ehp", typeof(System.String));
            dtSource.Columns.Add("gsis_hip", typeof(System.String));
            dtSource.Columns.Add("gsis_ceap", typeof(System.String));
            dtSource.Columns.Add("gsis_addl_ins", typeof(System.String));
            dtSource.Columns.Add("gsis_conso_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_policy_reg_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_policy_opt_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_uoli_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_emergency_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_ecard_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_educ_asst_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_real_state_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_sos_ln", typeof(System.String));
            dtSource.Columns.Add("gsis_help", typeof(System.String));
            dtSource.Columns.Add("gsis_housing_ln", typeof(System.String));
            dtSource.Columns.Add("hdmf_mpl_ln", typeof(System.String));
            dtSource.Columns.Add("hdmf_hse_ln", typeof(System.String));
            dtSource.Columns.Add("hdmf_cal_ln", typeof(System.String));
            dtSource.Columns.Add("hdmf_loyalty_card", typeof(System.String));
            dtSource.Columns.Add("nico_ln", typeof(System.String));
            dtSource.Columns.Add("network_ln", typeof(System.String));
            dtSource.Columns.Add("ccmpc_ln", typeof(System.String));
            dtSource.Columns.Add("total_mandatory", typeof(System.String));
            dtSource.Columns.Add("total_loans", typeof(System.String));
            dtSource.Columns.Add("total_optional", typeof(System.String));
            dtSource.Columns.Add("other_loan1", typeof(System.String));
            dtSource.Columns.Add("other_loan2", typeof(System.String));
            dtSource.Columns.Add("other_loan3", typeof(System.String));
            dtSource.Columns.Add("other_loan4", typeof(System.String));
            dtSource.Columns.Add("other_loan5", typeof(System.String));
            dtSource.Columns.Add("other_premium1", typeof(System.String));
            dtSource.Columns.Add("other_premium2", typeof(System.String));
            dtSource.Columns.Add("other_premium3", typeof(System.String));
            dtSource.Columns.Add("other_premium4", typeof(System.String));
            dtSource.Columns.Add("other_premium5", typeof(System.String));

            dtSource.Columns.Add("lates_mins_hrs", typeof(System.String));
            dtSource.Columns.Add("lates_amount", typeof(System.String));

            dtSource.Columns.Add("refund_sal_amt", typeof(System.String)); // For Terminal Leave

            dtSource.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource.Columns.Add("voucher_dtl_descr", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.DateTime));
            dtSource.Columns.Add("updated_dttm", typeof(System.DateTime));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("is_differential", typeof(System.Boolean));
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "voucher_dtl_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "voucher_ctrl_nbr", "seq_no", "empl_id" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"]        = string.Empty;
            nrow["voucher_ctrl_nbr"]    = string.Empty;
            nrow["seq_no"]              = string.Empty;
            nrow["empl_id"]             = string.Empty;
            nrow["voucher_period_from"] = string.Empty;
            nrow["voucher_period_to"]   = string.Empty;
            nrow["gross_pay"]           = string.Empty;
            nrow["net_pay"]             = string.Empty;
            nrow["rate_basis"]          = string.Empty;
            nrow["monthly_rate"]        = string.Empty;
            nrow["daily_rate"]          = string.Empty;
            nrow["hourly_rate"]         = string.Empty;
            nrow["no_of_days"]          = string.Empty;
            nrow["other_amt1"]          = string.Empty;
            nrow["other_amt2"]          = string.Empty;
            nrow["other_amt3"]          = string.Empty;
            nrow["wtax"]                = string.Empty;
            nrow["lowp_amount_salary"]  = string.Empty;
            nrow["lowp_amount_pera"]    = string.Empty;
            nrow["gsis_gs"]             = string.Empty;
            nrow["gsis_ps"]             = string.Empty;
            nrow["sif_gs"]              = string.Empty;
            nrow["hdmf_gs"]             = string.Empty;
            nrow["hdmf_ps"]             = string.Empty;
            nrow["phic_gs"]             = string.Empty;
            nrow["phic_ps"]             = string.Empty;
            nrow["nhmfc_hsing"]         = string.Empty;
            nrow["nafc_svlf"]           = string.Empty;
            nrow["sss_ps"]              = string.Empty;
            nrow["hdmf_ps2"]            = string.Empty;
            nrow["hdmf_mp2"]            = string.Empty;
            nrow["philamlife_ps"]       = string.Empty;
            nrow["gsis_ehp"]            = string.Empty;
            nrow["gsis_hip"]            = string.Empty;
            nrow["gsis_ceap"]           = string.Empty;
            nrow["gsis_addl_ins"]       = string.Empty;
            nrow["gsis_conso_ln"]       = string.Empty;
            nrow["gsis_policy_reg_ln"]  = string.Empty;
            nrow["gsis_policy_opt_ln"]  = string.Empty;
            nrow["gsis_uoli_ln"]        = string.Empty;
            nrow["gsis_emergency_ln"]   = string.Empty;
            nrow["gsis_ecard_ln"]       = string.Empty;
            nrow["gsis_educ_asst_ln"]   = string.Empty;
            nrow["gsis_real_state_ln"]  = string.Empty;
            nrow["gsis_sos_ln"]         = string.Empty;
            nrow["gsis_help"]           = string.Empty;
            nrow["gsis_housing_ln"]     = string.Empty;
            nrow["hdmf_mpl_ln"]         = string.Empty;
            nrow["hdmf_hse_ln"]         = string.Empty;
            nrow["hdmf_cal_ln"]         = string.Empty;
            nrow["hdmf_loyalty_card"]   = string.Empty;
            nrow["nico_ln"]             = string.Empty;
            nrow["network_ln"]          = string.Empty;
            nrow["ccmpc_ln"]            = string.Empty;
            nrow["total_mandatory"]     = string.Empty;
            nrow["total_loans"]         = string.Empty;
            nrow["total_optional"]      = string.Empty;
            nrow["other_loan1"]         = string.Empty;
            nrow["other_loan2"]         = string.Empty;
            nrow["other_loan3"]         = string.Empty;
            nrow["other_loan4"]         = string.Empty;
            nrow["other_loan5"]         = string.Empty;
            nrow["other_premium1"]      = string.Empty;
            nrow["other_premium2"]      = string.Empty;
            nrow["other_premium3"]      = string.Empty;
            nrow["other_premium4"]      = string.Empty;
            nrow["other_premium5"]      = string.Empty;
            nrow["lates_mins_hrs"]      = string.Empty;
            nrow["lates_amount"]        = string.Empty;
            nrow["refund_sal_amt"]      = string.Empty;
            nrow["payrolltemplate_code"]= string.Empty;
            nrow["voucher_dtl_descr"]   = string.Empty;
            nrow["updated_by_user"]     = string.Empty;
            nrow["created_by_user"]     = string.Empty;
            nrow["action"]      = 1;
            nrow["retrieve"]    = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {

            FieldValidationColorChanged(false, "ALL");
            string[] commandArgs    = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id          = commandArgs[0];
            string voucher_ctrl_nbr = commandArgs[1];
            string payroll_year     = commandArgs[2];
            string seq_no           = commandArgs[3];
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
            
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression             = "empl_id = '" + commandarg[0].Trim() + "' AND voucher_ctrl_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "' AND seq_no = '" + commandarg[3].Trim() + "'";
            string deleteExpression_oth_claims  = "empl_id = '" + commandarg[0].Trim() + "' AND voucher_ctrl_nbr = '" + commandarg[1].Trim() + "' AND seq_no = '" + commandarg[3].Trim() + "'";
            string deleteExpression_othded      = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "' AND seq_no = '" + commandarg[3].Trim() + "'";

            if (Session["ep_post_authority"].ToString() == "0")
            {
                // Non-Accounting Delete 
                MyCmn.DeleteBackEndData("voucher_dtl_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("voucher_dtl_oth_claims_tbl", "WHERE " + deleteExpression_oth_claims);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_othded_tbl", "WHERE " + deleteExpression_othded);
                DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
                dataListGrid.Rows.Remove(row2Delete[0]);
                dataListGrid.AcceptChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - VJA : 06/06/2019 - Edit Row selection that will trigger edit page
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            editaddmodal(e.CommandArgument.ToString());
            ToogleModal();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA : 06/06/2019 - Edit Row selection that will trigger edit page
        //**************************************************************************
        private void editaddmodal(string session_val)
        {
            // BEGIN            - Pass Value
            // Employee ID      [0]
            // Registry         [1]
            // Year             [2]
            // Employment Type  [3]
            // Department       [4]
            // END              - Pass Value

            string[] svalues = session_val.ToString().Split(new char[] { ',' });
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND voucher_ctrl_nbr = '" + svalues[1].ToString().Trim() + "' AND payroll_year = '" + svalues[2].ToString().Trim() + "' AND seq_no = '" + svalues[3].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow1 = dtSource.NewRow();
            nrow1["payroll_year"] = string.Empty;
            nrow1["voucher_ctrl_nbr"] = string.Empty;
            nrow1["seq_no"] = string.Empty;
            nrow1["empl_id"] = string.Empty;
            nrow1["action"] = 2;
            nrow1["retrieve"] = true;
            dtSource.Rows.Add(nrow1);

            voucher_nbr.Text            = svalues[1].ToString().Trim();
            txtb_employeename.Text      = row2Edit[0]["employee_name"].ToString();
            txtb_empl_id.Text           = svalues[0].ToString().Trim();
            
            txtb_employeename.Visible = true;

            lbl_rate_basis_hidden.Text      = row2Edit[0]["rate_basis"].ToString();
            ViewState["daily_rate"]         = row2Edit[0]["daily_rate"].ToString();
            txtb_net_pay.Text               = row2Edit[0]["net_pay"].ToString();
            txtb_no_of_days.Text            = row2Edit[0]["no_of_days"].ToString();
            txtb_period_from.Text           = row2Edit[0]["voucher_period_from"].ToString();
            txtb_period_to.Text             = row2Edit[0]["voucher_period_to"].ToString();
            txtb_gross_pay.Text             = row2Edit[0]["gross_pay"].ToString();
            txtb_net_pay.Text               = row2Edit[0]["net_pay"].ToString();
            hidden_seq_no.Text              = row2Edit[0]["seq_no"].ToString();
            lbl_rate_basis_hidden.Text      = row2Edit[0]["rate_basis"].ToString();
            lbl_monthly_rate_hidden.Text    = row2Edit[0]["monthly_rate"].ToString();
            lbl_daily_rate_hidden.Text      = row2Edit[0]["daily_rate"].ToString();
            lbl_hourly_rate_hidden.Text     = row2Edit[0]["hourly_rate"].ToString();
            txtb_no_of_days.Text            = row2Edit[0]["no_of_days"].ToString();
            txtb_other_amount1.Text         = row2Edit[0]["other_amt1"].ToString();
            txtb_other_amount2.Text         = row2Edit[0]["other_amt2"].ToString();
            txtb_other_amount3.Text         = row2Edit[0]["other_amt3"].ToString();
            txtb_bir_tax.Text               = row2Edit[0]["wtax"].ToString();
            txtb_birtax_summary.Text        = row2Edit[0]["wtax"].ToString();
            txtb_lwo_pay.Text               = row2Edit[0]["lowp_amount_salary"].ToString();
            txtb_lwop_amount_pera.Text      = row2Edit[0]["lowp_amount_pera"].ToString();
            txtb_gsis_gs.Text               = row2Edit[0]["gsis_gs"].ToString();
            txtb_gsis_ps.Text               = row2Edit[0]["gsis_ps"].ToString();
            txtb_gsis_sif.Text              = row2Edit[0]["sif_gs"].ToString();
            txtb_hdmf_gs.Text               = row2Edit[0]["hdmf_gs"].ToString();
            txtb_hdmf_ps.Text               = row2Edit[0]["hdmf_ps"].ToString();
            txtb_phic_gs.Text               = row2Edit[0]["phic_gs"].ToString();
            txtb_phic_ps.Text               = row2Edit[0]["phic_ps"].ToString();
            txtb_nhmfc_hsng.Text            = row2Edit[0]["nhmfc_hsing"].ToString();
            txtb_nafc.Text                  = row2Edit[0]["nafc_svlf"].ToString();
            txtb_sss.Text                   = row2Edit[0]["sss_ps"].ToString();
            txtb_hdmf_addl.Text             = row2Edit[0]["hdmf_ps2"].ToString();
            txtb_hdmf_mp2.Text              = row2Edit[0]["hdmf_mp2"].ToString();
            txtb_philam.Text                = row2Edit[0]["philamlife_ps"].ToString();
            txtb_gsis_ehp.Text              = row2Edit[0]["gsis_ehp"].ToString();
            txtb_gsis_hip.Text              = row2Edit[0]["gsis_hip"].ToString();
            txtb_gsis_ceap.Text             = row2Edit[0]["gsis_ceap"].ToString();
            txtb_gsis_add.Text              = row2Edit[0]["gsis_addl_ins"].ToString();
            txtb_gsis_consolidated.Text     = row2Edit[0]["gsis_conso_ln"].ToString();
            txtb_gsis_policy_regular.Text   = row2Edit[0]["gsis_policy_reg_ln"].ToString();
            txtb_gsis_policy_optional.Text  = row2Edit[0]["gsis_policy_opt_ln"].ToString();
            txtb_gsis_ouli_loan.Text        = row2Edit[0]["gsis_uoli_ln"].ToString();
            txtb_gsis_emer_loan.Text        = row2Edit[0]["gsis_emergency_ln"].ToString();
            txtb_gsis_ecard_loan.Text       = row2Edit[0]["gsis_ecard_ln"].ToString();
            txtb_gsis_educ_loan.Text        = row2Edit[0]["gsis_educ_asst_ln"].ToString();
            txtb_gsis_real_loan.Text        = row2Edit[0]["gsis_real_state_ln"].ToString();
            txtb_gsis_sos_loan.Text         = row2Edit[0]["gsis_sos_ln"].ToString();
            txtb_gsis_help.Text             = row2Edit[0]["gsis_help"].ToString();
            txtb_gsis_housing_loan.Text     = row2Edit[0]["gsis_housing_ln"].ToString();
            txtb_hdmf_mpl_loan.Text         = row2Edit[0]["hdmf_mpl_ln"].ToString();
            txtb_hdmf_house_loan.Text       = row2Edit[0]["hdmf_hse_ln"].ToString();
            txtb_hdmf_cal_loan.Text         = row2Edit[0]["hdmf_cal_ln"].ToString();
            txtb_hdmf_loyalty_card.Text     = row2Edit[0]["hdmf_loyalty_card"].ToString();
            txtb_nico_loan.Text             = row2Edit[0]["nico_ln"].ToString();
            txtb_networkbank_loan.Text      = row2Edit[0]["network_ln"].ToString();
            txtb_ccmpc_loan.Text            = row2Edit[0]["ccmpc_ln"].ToString();
            
            txtb_otherpremium_no1.Text      = row2Edit[0]["other_premium1"].ToString();
            txtb_otherpremium_no2.Text      = row2Edit[0]["other_premium2"].ToString();
            txtb_otherpremium_no3.Text      = row2Edit[0]["other_premium3"].ToString();
            txtb_otherpremium_no4.Text      = row2Edit[0]["other_premium4"].ToString();
            txtb_otherpremium_no5.Text      = row2Edit[0]["other_premium5"].ToString();
            txtb_otherloan_no1.Text         = row2Edit[0]["other_loan1"].ToString();
            txtb_otherloan_no2.Text         = row2Edit[0]["other_loan2"].ToString();
            txtb_otherloan_no3.Text         = row2Edit[0]["other_loan3"].ToString();
            txtb_otherloan_no4.Text         = row2Edit[0]["other_loan4"].ToString();
            txtb_otherloan_no5.Text         = row2Edit[0]["other_loan5"].ToString();
            
            // Add Field Again - VJA - 2022-05-26 - Other Deduction 
            txtb_other_ded_mand1.Text   =  row2Edit[0]["other_ded_mand1"].ToString();
            txtb_other_ded_mand2.Text   =  row2Edit[0]["other_ded_mand2"].ToString();
            txtb_other_ded_mand3.Text   =  row2Edit[0]["other_ded_mand3"].ToString();
            txtb_other_ded_mand4.Text   =  row2Edit[0]["other_ded_mand4"].ToString();
            txtb_other_ded_mand5.Text   =  row2Edit[0]["other_ded_mand5"].ToString();
            txtb_other_ded_mand6.Text   =  row2Edit[0]["other_ded_mand6"].ToString();
            txtb_other_ded_mand7.Text   =  row2Edit[0]["other_ded_mand7"].ToString();
            txtb_other_ded_mand8.Text   =  row2Edit[0]["other_ded_mand8"].ToString();
            txtb_other_ded_mand9.Text   =  row2Edit[0]["other_ded_mand9"].ToString();
            txtb_other_ded_mand10.Text  =  row2Edit[0]["other_ded_mand10"].ToString();
            txtb_other_ded_prem1.Text   =  row2Edit[0]["other_ded_prem1"].ToString();
            txtb_other_ded_prem2.Text   =  row2Edit[0]["other_ded_prem2"].ToString();
            txtb_other_ded_prem3.Text   =  row2Edit[0]["other_ded_prem3"].ToString();
            txtb_other_ded_prem4.Text   =  row2Edit[0]["other_ded_prem4"].ToString();
            txtb_other_ded_prem5.Text   =  row2Edit[0]["other_ded_prem5"].ToString();
            txtb_other_ded_prem6.Text   =  row2Edit[0]["other_ded_prem6"].ToString();
            txtb_other_ded_prem7.Text   =  row2Edit[0]["other_ded_prem7"].ToString();
            txtb_other_ded_prem8.Text   =  row2Edit[0]["other_ded_prem8"].ToString();
            txtb_other_ded_prem9.Text   =  row2Edit[0]["other_ded_prem9"].ToString();
            txtb_other_ded_prem10.Text  =  row2Edit[0]["other_ded_prem10"].ToString();
            txtb_other_ded_loan1.Text   =  row2Edit[0]["other_ded_loan1"].ToString();
            txtb_other_ded_loan2.Text   =  row2Edit[0]["other_ded_loan2"].ToString();
            txtb_other_ded_loan3.Text   =  row2Edit[0]["other_ded_loan3"].ToString();
            txtb_other_ded_loan4.Text   =  row2Edit[0]["other_ded_loan4"].ToString();
            txtb_other_ded_loan5.Text   =  row2Edit[0]["other_ded_loan5"].ToString();
            txtb_other_ded_loan6.Text   =  row2Edit[0]["other_ded_loan6"].ToString();
            txtb_other_ded_loan7.Text   =  row2Edit[0]["other_ded_loan7"].ToString();
            txtb_other_ded_loan8.Text   =  row2Edit[0]["other_ded_loan8"].ToString();
            txtb_other_ded_loan9.Text   =  row2Edit[0]["other_ded_loan9"].ToString();
            txtb_other_ded_loan10.Text  =  row2Edit[0]["other_ded_loan10"].ToString();

            txtb_lates_min.Text         = row2Edit[0]["lates_mins_hrs"].ToString();
            txtb_lates_amount.Text      = row2Edit[0]["lates_amount"].ToString();
            refund_sal_amt.Text         = row2Edit[0]["refund_sal_amt"].ToString();
            
            payrolltemplate_code.SelectedValue      = row2Edit[0]["payrolltemplate_code"].ToString();
            voucher_dtl_descr.Text                  = row2Edit[0]["voucher_dtl_descr"].ToString();
            is_differential.Checked                 = bool.Parse(row2Edit[0]["is_differential"].ToString());

            switch (row2Edit[0]["rate_basis"].ToString())
            {
                case "M":
                    {
                        txtb_rate_amount.Text = row2Edit[0]["monthly_rate"].ToString();
                        lbl_rate_descr.Text = "Monthly Rate:";
                        break;
                    }
                case "D":
                    {
                        txtb_rate_amount.Text = row2Edit[0]["daily_rate"].ToString();
                        lbl_rate_descr.Text = "Daily Rate:";
                        break;
                    }
                case "H":
                    {
                        txtb_rate_amount.Text = row2Edit[0]["hourly_rate"].ToString();
                        lbl_rate_descr.Text = "Hourly Rate:";
                        break;
                    }
            }
            // END   - VJA : 05/29/2019 - Details


            // ***************BEGIN*******************************************************
            //                OTHER CLAIMS/REFUND
            // ***************************************************************************
            dtSource_oth_claims              = MyCmn.RetrieveData("voucher_dtl_oth_claims_tbl_list", "par_voucher_ctrl_nbr", voucher_nbr.Text.ToString().Trim());
            string editExpression_oth_claims = "empl_id = '" + svalues[0].ToString().Trim() + "' AND voucher_ctrl_nbr = '" + svalues[1].ToString().Trim() + "' AND seq_no = '" + svalues[3].ToString().Trim() + "'";
            DataRow[] row2Edit2              = dtSource_oth_claims.Select(editExpression_oth_claims);
            
            InitializeTable_oth_claims();
            AddPrimaryKeys_oth_claims();
            DataRow nrow2 = dtSource_oth_claims.NewRow();
            nrow2["voucher_ctrl_nbr"] = string.Empty;
            nrow2["seq_no"]           = string.Empty;
            nrow2["empl_id"]          = string.Empty;
            nrow2["action"]           = 2;
            nrow2["retrieve"] = true;
            dtSource_oth_claims.Rows.Add(nrow2);
            
            txtb_payroll_descr1.Text         = row2Edit2[0]["payroll_descr_amt1"].ToString();
            txtb_payroll_descr2.Text         = row2Edit2[0]["payroll_descr_amt2"].ToString();
            txtb_payroll_descr3.Text         = row2Edit2[0]["payroll_descr_amt3"].ToString();
            txtb_payroll_descr4.Text         = row2Edit2[0]["payroll_descr_amt4"].ToString();
            txtb_payroll_descr5.Text         = row2Edit2[0]["payroll_descr_amt5"].ToString();
            ddl_payroll_type_1.SelectedValue = row2Edit2[0]["payroll_type_amt1"].ToString();
            ddl_payroll_type_2.SelectedValue = row2Edit2[0]["payroll_type_amt2"].ToString();
            ddl_payroll_type_3.SelectedValue = row2Edit2[0]["payroll_type_amt3"].ToString();
            ddl_payroll_type_4.SelectedValue = row2Edit2[0]["payroll_type_amt4"].ToString();
            ddl_payroll_type_5.SelectedValue = row2Edit2[0]["payroll_type_amt5"].ToString();
            txtb_payroll_amt1.Text           = row2Edit2[0]["other_amt1"].ToString();
            txtb_payroll_amt2.Text           = row2Edit2[0]["other_amt2"].ToString();
            txtb_payroll_amt3.Text           = row2Edit2[0]["other_amt3"].ToString();
            txtb_payroll_amt4.Text           = row2Edit2[0]["other_amt4"].ToString();
            txtb_payroll_amt5.Text           = row2Edit2[0]["other_amt5"].ToString();

            // ***************END*******************************************************
            //                OTHER CLAIMS/REFUND
            // ***************************************************************************


            // The Save Button Will be Visible false if the Post Status is equal to Y
            if (row2Edit[0]["post_status"].ToString() == "Y")
            {
                btnSave.Visible = false;
                lbl_if_dateposted_yes.Text = "This Payroll Already Posted, You cannot Edit!";
            }
            else
            {
                btnSave.Visible = true;
                lbl_if_dateposted_yes.Text = "";
            }

            // During The Employee Name Change 
            calculate_mandatory();
            calculate_optional();
            calculate_loans();
            calculate_maternity();
            calculate_grosspay();
            calculate_netpays();

            //ddl_empl_id.Enabled = false;
            LabelAddEdit.Text = "Edit Record | Voucher Ctrl No : " + voucher_nbr.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState.Add("AddEdit_Mode_BrkDwn", "BRK_EDIT");
            lbl_addeditmode_hidden.Text = MyCmn.CONST_EDIT;

            FieldValidationColorChanged(false, "ALL");
            
            txtb_status.Text                    = row2Edit[0]["post_status_descr"].ToString();

            //FOR POSTED STATUS
            if (row2Edit[0]["post_status"].ToString().ToUpper() == "Y" && (Session["ep_post_authority"].ToString() == "1" || Session["ep_post_authority"].ToString() == "0"))
            {
                btnSave.Visible             = false;
                btn_calculate.Visible       = false;
                txtb_voucher_nbr.Enabled    = false;
                Linkbtncancel.Text          = "Close";
                lbl_if_dateposted_yes.Text  = "This Payroll is already Posted, You Cannot Edit!";
                txtb_date_posted.Text       = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }

            //FOR USER'S IN ACCOUNTING
            else if (row2Edit[0]["post_status"].ToString() == "N" && Session["ep_post_authority"].ToString() == "1")
            {
                btnSave.Visible             = true;
                btn_calculate.Visible       = false;
                btnSave.Text                = "Post To Card";
                Linkbtncancel.Text          = "Cancel";
                lbl_if_dateposted_yes.Text  = "";
                txtb_voucher_nbr.Enabled    = true;
                txtb_date_posted.Text       = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }

            //FOR OTHER DEPARTMENTS
            else if (Session["ep_post_authority"].ToString() == "0")
            {
                txtb_voucher_nbr.Enabled    = false;
                btnSave.Visible             = true;
                btn_calculate.Visible       = true;
                btnSave.Text                = "Save";
                Linkbtncancel.Text          = "Cancel";
                lbl_if_dateposted_yes.Text  = "";
                txtb_voucher_nbr.Enabled    = false;
                txtb_date_posted.Text       = "";
                ToogleTextbox(true);

            }

            if (row2Edit[0]["post_status"].ToString().ToUpper() == "R")
            {
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                txtb_voucher_nbr.Enabled = false;
                Linkbtncancel.Text = "Close";
                lbl_if_dateposted_yes.Text = "This Payroll is already Released, You Cannot Edit!";
                //txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                txtb_rate_amount.Enabled = false;
                ToogleTextbox(false);
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Change Field Sort mode  
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
        //  BEGIN - VJA- 06/06/2019 - Get Grid current sort order 
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
        //  BEGIN - VJA- 06/06/2019 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate1 = string.Empty;
            string scriptInsertUpdate_oth_claims = string.Empty;

            if (IsDataValidated())
            {
                //Calculate All Fields and Insert to the Haader
                calculate_mandatory();
                calculate_optional();
                calculate_loans();
                calculate_maternity();
                calculate_grosspay();
                calculate_netpays();
                
                if (saveRecord == MyCmn.CONST_ADD)
                {
                    // BEGIN - VJA : 05/29/2019 - Header
                    dtSource.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["voucher_ctrl_nbr"]        = voucher_nbr.Text.ToString().Trim();
                    dtSource.Rows[0]["seq_no"]                  = hidden_seq_no.Text;
                    dtSource.Rows[0]["voucher_period_from"]     = txtb_period_from.Text.ToString().Trim();
                    dtSource.Rows[0]["voucher_period_to"]       = txtb_period_to.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]              = lbl_rate_basis_hidden.Text.ToString();
                    dtSource.Rows[0]["monthly_rate"]            = lbl_monthly_rate_hidden.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]              = lbl_daily_rate_hidden.Text.ToString().Trim();
                    dtSource.Rows[0]["hourly_rate"]             = lbl_hourly_rate_hidden.Text;
                    dtSource.Rows[0]["no_of_days"]              = txtb_no_of_days.Text;
                    dtSource.Rows[0]["other_amt1"]              = txtb_other_amount1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amt2"]              = txtb_other_amount2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amt3"]              = txtb_other_amount3.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount_salary"]      = txtb_lwo_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount_pera"]        = txtb_lwop_amount_pera.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_gs"]                 = txtb_gsis_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ps"]                 = txtb_gsis_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["sif_gs"]                  = txtb_gsis_sif.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_gs"]                 = txtb_hdmf_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps"]                 = txtb_hdmf_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_gs"]                 = txtb_phic_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_ps"]                 = txtb_phic_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["nhmfc_hsing"]             = txtb_nhmfc_hsng.Text.ToString().Trim();
                    dtSource.Rows[0]["nafc_svlf"]               = txtb_nafc.Text.ToString().Trim();
                    dtSource.Rows[0]["sss_ps"]                  = txtb_sss.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps2"]                = txtb_hdmf_addl.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_mp2"]                = txtb_hdmf_mp2.Text.ToString().Trim();
                    dtSource.Rows[0]["philamlife_ps"]           = txtb_philam.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ehp"]                = txtb_gsis_ehp.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_hip"]                = txtb_gsis_hip.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ceap"]               = txtb_gsis_ceap.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_addl_ins"]           = txtb_gsis_add.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_conso_ln"]           = txtb_gsis_consolidated.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_policy_reg_ln"]      = txtb_gsis_policy_regular.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_policy_opt_ln"]      = txtb_gsis_policy_optional.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli_ln"]            = txtb_gsis_ouli_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_emergency_ln"]       = txtb_gsis_emer_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ecard_ln"]           = txtb_gsis_ecard_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_educ_asst_ln"]       = txtb_gsis_educ_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_real_state_ln"]      = txtb_gsis_real_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_sos_ln"]             = txtb_gsis_sos_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_help"]               = txtb_gsis_help.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_housing_ln"]         = txtb_gsis_housing_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_mpl_ln"]             = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_hse_ln"]             = txtb_hdmf_house_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_cal_ln"]             = txtb_hdmf_cal_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_loyalty_card"]       = txtb_hdmf_loyalty_card.Text.ToString().Trim();
                    dtSource.Rows[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["network_ln"]              = txtb_networkbank_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["total_mandatory"]         = txtb_total_mandatory.Text.ToString().Trim();
                    dtSource.Rows[0]["total_loans"]             = txtb_total_loans.Text.ToString().Trim();
                    dtSource.Rows[0]["total_optional"]          = txtb_total_optional.Text.ToString().Trim();
                    //dtSource.Rows[0]["deduc_amt"]               = calculate_deduc_amt().ToString("###,##0.00");

                    dtSource.Rows[0]["other_loan1"]             = txtb_otherloan_no1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan2"]             = txtb_otherloan_no2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan3"]             = txtb_otherloan_no3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan4"]             = txtb_otherloan_no4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan5"]             = txtb_otherloan_no5.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium1"]          = txtb_otherpremium_no1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium2"]          = txtb_otherpremium_no2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium3"]          = txtb_otherpremium_no3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium4"]          = txtb_otherpremium_no4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium5"]          = txtb_otherpremium_no5.Text.ToString().Trim();

                    dtSource.Rows[0]["lates_mins_hrs"]          = txtb_lates_min.Text.ToString().Trim();
                    dtSource.Rows[0]["lates_amount"]            = txtb_lates_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["refund_sal_amt"]          = refund_sal_amt.Text.ToString().Trim();

                    dtSource.Rows[0]["payrolltemplate_code"]    = payrolltemplate_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["voucher_dtl_descr"]       = voucher_dtl_descr.Text.ToString().Trim();
                    dtSource.Rows[0]["created_dttm"]            = DateTime.Now;
                    dtSource.Rows[0]["created_by_user"]         = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["is_differential"]         = is_differential.Checked;

                    // If the Template Code is Hononrarium - Hiddent TextBox
                    if (ddl_payroll_template.SelectedValue == "604" ||
                        ddl_payroll_template.SelectedValue == "704" ||
                        ddl_payroll_template.SelectedValue == "804")
                    {
                        dtSource.Rows[0]["wtax"] = txtb_birtax_summary.Text.ToString().Trim();
                    }
                    else
                    {
                        dtSource.Rows[0]["wtax"] = txtb_bir_tax.Text.ToString().Trim();
                    }
                    // END - Add Field Again  - 06/20/2019


                    // ***************BEGIN*******************************************************
                    //                OTHER CLAIMS/REFUND
                    // ***************************************************************************
                    dtSource_oth_claims.Rows[0]["voucher_ctrl_nbr"]     = voucher_nbr.Text.ToString().Trim();
                    dtSource_oth_claims.Rows[0]["seq_no"]               = hidden_seq_no.Text;
                    dtSource_oth_claims.Rows[0]["empl_id"]              = txtb_empl_id.Text.ToString().Trim();
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt1"]   = txtb_payroll_descr1.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt2"]   = txtb_payroll_descr2.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt3"]   = txtb_payroll_descr3.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt4"]   = txtb_payroll_descr4.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt5"]   = txtb_payroll_descr5.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_type_amt1"]    = ddl_payroll_type_1.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["payroll_type_amt2"]    = ddl_payroll_type_2.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["payroll_type_amt3"]    = ddl_payroll_type_3.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["payroll_type_amt4"]    = ddl_payroll_type_4.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["payroll_type_amt5"]    = ddl_payroll_type_5.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["other_amt1"]           = txtb_payroll_amt1.Text;           
                    dtSource_oth_claims.Rows[0]["other_amt2"]           = txtb_payroll_amt2.Text;           
                    dtSource_oth_claims.Rows[0]["other_amt3"]           = txtb_payroll_amt3.Text;           
                    dtSource_oth_claims.Rows[0]["other_amt4"]           = txtb_payroll_amt4.Text;           
                    dtSource_oth_claims.Rows[0]["other_amt5"]           = txtb_payroll_amt5.Text;
                    // ***************END*******************************************************
                    //                OTHER CLAIMS/REFUND
                    // ***************************************************************************
                    
                    scriptInsertUpdate1 = MyCmn.get_insertscript(dtSource);
                    scriptInsertUpdate_oth_claims   = MyCmn.get_insertscript(dtSource_oth_claims);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    // BEGIN - VJA : 05/29/2019 - Header
                    dtSource.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["voucher_ctrl_nbr"]        = voucher_nbr.Text.ToString().Trim();
                    dtSource.Rows[0]["seq_no"]                  = hidden_seq_no.Text;
                    dtSource.Rows[0]["voucher_period_from"]     = txtb_period_from.Text.ToString().Trim();
                    dtSource.Rows[0]["voucher_period_to"]       = txtb_period_to.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]              = lbl_rate_basis_hidden.Text.ToString();
                    dtSource.Rows[0]["monthly_rate"]            = lbl_monthly_rate_hidden.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]              = lbl_daily_rate_hidden.Text.ToString().Trim();
                    dtSource.Rows[0]["hourly_rate"]             = lbl_hourly_rate_hidden.Text;
                    dtSource.Rows[0]["no_of_days"]              = txtb_no_of_days.Text;
                    dtSource.Rows[0]["other_amt1"]              = txtb_other_amount1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amt2"]              = txtb_other_amount2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amt3"]              = txtb_other_amount3.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount_salary"]      = txtb_lwo_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["lowp_amount_pera"]        = txtb_lwop_amount_pera.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_gs"]                 = txtb_gsis_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ps"]                 = txtb_gsis_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["sif_gs"]                  = txtb_gsis_sif.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_gs"]                 = txtb_hdmf_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps"]                 = txtb_hdmf_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_gs"]                 = txtb_phic_gs.Text.ToString().Trim();
                    dtSource.Rows[0]["phic_ps"]                 = txtb_phic_ps.Text.ToString().Trim();
                    dtSource.Rows[0]["nhmfc_hsing"]             = txtb_nhmfc_hsng.Text.ToString().Trim();
                    dtSource.Rows[0]["nafc_svlf"]               = txtb_nafc.Text.ToString().Trim();
                    dtSource.Rows[0]["sss_ps"]                  = txtb_sss.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_ps2"]                = txtb_hdmf_addl.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_mp2"]                = txtb_hdmf_mp2.Text.ToString().Trim();
                    dtSource.Rows[0]["philamlife_ps"]           = txtb_philam.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ehp"]                = txtb_gsis_ehp.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_hip"]                = txtb_gsis_hip.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ceap"]               = txtb_gsis_ceap.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_addl_ins"]           = txtb_gsis_add.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_conso_ln"]           = txtb_gsis_consolidated.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_policy_reg_ln"]      = txtb_gsis_policy_regular.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_policy_opt_ln"]      = txtb_gsis_policy_optional.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_uoli_ln"]            = txtb_gsis_ouli_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_emergency_ln"]       = txtb_gsis_emer_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_ecard_ln"]           = txtb_gsis_ecard_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_educ_asst_ln"]       = txtb_gsis_educ_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_real_state_ln"]      = txtb_gsis_real_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_sos_ln"]             = txtb_gsis_sos_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_help"]               = txtb_gsis_help.Text.ToString().Trim();
                    dtSource.Rows[0]["gsis_housing_ln"]         = txtb_gsis_housing_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_mpl_ln"]             = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_hse_ln"]             = txtb_hdmf_house_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_cal_ln"]             = txtb_hdmf_cal_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["hdmf_loyalty_card"]       = txtb_hdmf_loyalty_card.Text.ToString().Trim();
                    dtSource.Rows[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["network_ln"]              = txtb_networkbank_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                    dtSource.Rows[0]["total_mandatory"]         = txtb_total_mandatory.Text.ToString().Trim();
                    dtSource.Rows[0]["total_loans"]             = txtb_total_loans.Text.ToString().Trim();
                    dtSource.Rows[0]["total_optional"]          = txtb_total_optional.Text.ToString().Trim();
                    //dtSource.Rows[0]["deduc_amt"]               = calculate_deduc_amt().ToString("###,##0.00");
                    
                    dtSource.Rows[0]["other_loan1"]             = txtb_otherloan_no1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan2"]             = txtb_otherloan_no2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan3"]             = txtb_otherloan_no3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan4"]             = txtb_otherloan_no4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_loan5"]             = txtb_otherloan_no5.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium1"]          = txtb_otherpremium_no1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium2"]          = txtb_otherpremium_no2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium3"]          = txtb_otherpremium_no3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium4"]          = txtb_otherpremium_no4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_premium5"]          = txtb_otherpremium_no5.Text.ToString().Trim();

                    dtSource.Rows[0]["lates_mins_hrs"]          = txtb_lates_min.Text.ToString().Trim();
                    dtSource.Rows[0]["lates_amount"]            = txtb_lates_amount.Text.ToString().Trim();
                    dtSource.Rows[0]["refund_sal_amt"]          = refund_sal_amt.Text.ToString().Trim();

                    dtSource.Rows[0]["payrolltemplate_code"]    = payrolltemplate_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["voucher_dtl_descr"]       = voucher_dtl_descr.Text.ToString().Trim();
                    dtSource.Rows[0]["updated_dttm"]            = DateTime.Now;
                    dtSource.Rows[0]["updated_by_user"]         = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["is_differential"]         = is_differential.Checked;

                    // If the Template Code is Hononrarium - Hiddent TextBox
                    if (ddl_payroll_template.SelectedValue == "604" ||
                        ddl_payroll_template.SelectedValue == "704" ||
                        ddl_payroll_template.SelectedValue == "804")
                    {
                        dtSource.Rows[0]["wtax"] = txtb_birtax_summary.Text.ToString().Trim();
                    }
                    else
                    {
                        dtSource.Rows[0]["wtax"] = txtb_bir_tax.Text.ToString().Trim();
                    }
                    // END - Add Field Again  - 06/20/2019

                    // ***************BEGIN*******************************************************
                    //                OTHER CLAIMS/REFUND
                    // ***************************************************************************
                    dtSource_oth_claims.Rows[0]["voucher_ctrl_nbr"]     = voucher_nbr.Text.ToString().Trim();
                    dtSource_oth_claims.Rows[0]["seq_no"]               = hidden_seq_no.Text;
                    dtSource_oth_claims.Rows[0]["empl_id"]              = txtb_empl_id.Text.ToString().Trim();
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt1"]   = txtb_payroll_descr1.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt2"]   = txtb_payroll_descr2.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt3"]   = txtb_payroll_descr3.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt4"]   = txtb_payroll_descr4.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_descr_amt5"]   = txtb_payroll_descr5.Text;         
                    dtSource_oth_claims.Rows[0]["payroll_type_amt1"]    = ddl_payroll_type_1.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["payroll_type_amt2"]    = ddl_payroll_type_2.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["payroll_type_amt3"]    = ddl_payroll_type_3.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["payroll_type_amt4"]    = ddl_payroll_type_4.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["payroll_type_amt5"]    = ddl_payroll_type_5.SelectedValue.ToString(); 
                    dtSource_oth_claims.Rows[0]["other_amt1"]           = txtb_payroll_amt1.Text;           
                    dtSource_oth_claims.Rows[0]["other_amt2"]           = txtb_payroll_amt2.Text;           
                    dtSource_oth_claims.Rows[0]["other_amt3"]           = txtb_payroll_amt3.Text;           
                    dtSource_oth_claims.Rows[0]["other_amt4"]           = txtb_payroll_amt4.Text;           
                    dtSource_oth_claims.Rows[0]["other_amt5"]           = txtb_payroll_amt5.Text;
                    // ***************END*******************************************************
                    //                OTHER CLAIMS/REFUND
                    // ***************************************************************************
                    

                    scriptInsertUpdate1 = MyCmn.updatescript(dtSource);
                    scriptInsertUpdate_oth_claims = MyCmn.updatescript(dtSource_oth_claims);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    if (scriptInsertUpdate1 == string.Empty) return;
                    if (scriptInsertUpdate_oth_claims == string.Empty) return;
                    string msg1 = MyCmn.insertdata(scriptInsertUpdate1);
                    string msg_oth_claims = MyCmn.insertdata(scriptInsertUpdate_oth_claims);
                    if (msg1 == "") return;
                    if (msg_oth_claims == "") return;
                    if (msg1.Substring(0, 1) == "X" || msg_oth_claims.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist");
                        LblRequired60.Text              = msg1 + msg_oth_claims;
                        txtb_employeename.BorderColor   = Color.Red;
                        return;
                    }

                    InsertUpdateOtherDeduction();
                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        // BEGIN - VJA : 05/29/2019 - Header
                        nrow["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                        nrow["voucher_ctrl_nbr"]        = voucher_nbr.Text.ToString().Trim();
                        nrow["seq_no"]                  = hidden_seq_no.Text;
                        nrow["voucher_period_from"]     = txtb_period_from.Text.ToString().Trim();
                        nrow["voucher_period_to"]       = txtb_period_to.Text.ToString().Trim();
                        nrow["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                        nrow["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                        nrow["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                        nrow["rate_basis"]              = lbl_rate_basis_hidden.Text.ToString();
                        nrow["monthly_rate"]            = lbl_monthly_rate_hidden.Text.ToString().Trim();
                        nrow["daily_rate"]              = lbl_daily_rate_hidden.Text.ToString().Trim();
                        nrow["hourly_rate"]             = lbl_hourly_rate_hidden.Text;
                        nrow["no_of_days"]              = txtb_no_of_days.Text;
                        nrow["other_amt1"]              = txtb_other_amount1.Text.ToString().Trim();
                        nrow["other_amt2"]              = txtb_other_amount2.Text.ToString().Trim();
                        nrow["other_amt3"]              = txtb_other_amount3.Text.ToString().Trim();
                        nrow["lowp_amount_salary"]      = txtb_lwo_pay.Text.ToString().Trim();
                        nrow["lowp_amount_pera"]        = txtb_lwop_amount_pera.Text.ToString().Trim();
                        nrow["gsis_gs"]                 = txtb_gsis_gs.Text.ToString().Trim();
                        nrow["gsis_ps"]                 = txtb_gsis_ps.Text.ToString().Trim();
                        nrow["sif_gs"]                  = txtb_gsis_sif.Text.ToString().Trim();
                        nrow["hdmf_gs"]                 = txtb_hdmf_gs.Text.ToString().Trim();
                        nrow["hdmf_ps"]                 = txtb_hdmf_ps.Text.ToString().Trim();
                        nrow["phic_gs"]                 = txtb_phic_gs.Text.ToString().Trim();
                        nrow["phic_ps"]                 = txtb_phic_ps.Text.ToString().Trim();
                        nrow["nhmfc_hsing"]             = txtb_nhmfc_hsng.Text.ToString().Trim();
                        nrow["nafc_svlf"]               = txtb_nafc.Text.ToString().Trim();
                        nrow["sss_ps"]                  = txtb_sss.Text.ToString().Trim();
                        nrow["hdmf_ps2"]                = txtb_hdmf_addl.Text.ToString().Trim();
                        nrow["hdmf_mp2"]                = txtb_hdmf_mp2.Text.ToString().Trim();
                        nrow["philamlife_ps"]           = txtb_philam.Text.ToString().Trim();
                        nrow["gsis_ehp"]                = txtb_gsis_ehp.Text.ToString().Trim();
                        nrow["gsis_hip"]                = txtb_gsis_hip.Text.ToString().Trim();
                        nrow["gsis_ceap"]               = txtb_gsis_ceap.Text.ToString().Trim();
                        nrow["gsis_addl_ins"]           = txtb_gsis_add.Text.ToString().Trim();
                        nrow["gsis_conso_ln"]           = txtb_gsis_consolidated.Text.ToString().Trim();
                        nrow["gsis_policy_reg_ln"]      = txtb_gsis_policy_regular.Text.ToString().Trim();
                        nrow["gsis_policy_opt_ln"]      = txtb_gsis_policy_optional.Text.ToString().Trim();
                        nrow["gsis_uoli_ln"]            = txtb_gsis_ouli_loan.Text.ToString().Trim();
                        nrow["gsis_emergency_ln"]       = txtb_gsis_emer_loan.Text.ToString().Trim();
                        nrow["gsis_ecard_ln"]           = txtb_gsis_ecard_loan.Text.ToString().Trim();
                        nrow["gsis_educ_asst_ln"]       = txtb_gsis_educ_loan.Text.ToString().Trim();
                        nrow["gsis_real_state_ln"]      = txtb_gsis_real_loan.Text.ToString().Trim();
                        nrow["gsis_sos_ln"]             = txtb_gsis_sos_loan.Text.ToString().Trim();
                        nrow["gsis_help"]               = txtb_gsis_help.Text.ToString().Trim();
                        nrow["gsis_housing_ln"]         = txtb_gsis_housing_loan.Text.ToString().Trim();
                        nrow["hdmf_mpl_ln"]             = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                        nrow["hdmf_hse_ln"]             = txtb_hdmf_house_loan.Text.ToString().Trim();
                        nrow["hdmf_cal_ln"]             = txtb_hdmf_cal_loan.Text.ToString().Trim();
                        nrow["hdmf_loyalty_card"]       = txtb_hdmf_loyalty_card.Text.ToString().Trim();
                        nrow["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                        nrow["network_ln"]              = txtb_networkbank_loan.Text.ToString().Trim();
                        nrow["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                        nrow["total_mandatory"]         = txtb_total_mandatory.Text.ToString().Trim();
                        nrow["total_loans"]             = txtb_total_loans.Text.ToString().Trim();
                        nrow["total_optional"]          = txtb_total_optional.Text.ToString().Trim();
                        nrow["deduc_amt"]               = calculate_deduc_amt().ToString("###,##0.00");
                        
                        nrow["other_loan1"]             = txtb_otherloan_no1.Text.ToString().Trim();
                        nrow["other_loan2"]             = txtb_otherloan_no2.Text.ToString().Trim();
                        nrow["other_loan3"]             = txtb_otherloan_no3.Text.ToString().Trim();
                        nrow["other_loan4"]             = txtb_otherloan_no4.Text.ToString().Trim();
                        nrow["other_loan5"]             = txtb_otherloan_no5.Text.ToString().Trim();
                        nrow["other_premium1"]          = txtb_otherpremium_no1.Text.ToString().Trim();
                        nrow["other_premium2"]          = txtb_otherpremium_no2.Text.ToString().Trim();
                        nrow["other_premium3"]          = txtb_otherpremium_no3.Text.ToString().Trim();
                        nrow["other_premium4"]          = txtb_otherpremium_no4.Text.ToString().Trim();
                        nrow["other_premium5"]          = txtb_otherpremium_no5.Text.ToString().Trim();

                        nrow["lates_mins_hrs"] = txtb_lates_min.Text.ToString().Trim();
                        nrow["lates_amount"] = txtb_lates_amount.Text.ToString().Trim();
                        nrow["refund_sal_amt"] = refund_sal_amt.Text.ToString().Trim();

                        // Add Field  2022-05-30
                        nrow["other_ded_mand1"]       = txtb_other_ded_mand1.Text.ToString().Trim();
                        nrow["other_ded_mand2"]       = txtb_other_ded_mand2.Text.ToString().Trim();
                        nrow["other_ded_mand3"]       = txtb_other_ded_mand3.Text.ToString().Trim();
                        nrow["other_ded_mand4"]       = txtb_other_ded_mand4.Text.ToString().Trim();
                        nrow["other_ded_mand5"]       = txtb_other_ded_mand5.Text.ToString().Trim();
                        nrow["other_ded_mand6"]       = txtb_other_ded_mand6.Text.ToString().Trim();
                        nrow["other_ded_mand7"]       = txtb_other_ded_mand7.Text.ToString().Trim();
                        nrow["other_ded_mand8"]       = txtb_other_ded_mand8.Text.ToString().Trim();
                        nrow["other_ded_mand9"]       = txtb_other_ded_mand9.Text.ToString().Trim();
                        nrow["other_ded_mand10"]      = txtb_other_ded_mand10.Text.ToString().Trim();
                        nrow["other_ded_prem1"]       = txtb_other_ded_prem1.Text.ToString().Trim();
                        nrow["other_ded_prem2"]       = txtb_other_ded_prem2.Text.ToString().Trim();
                        nrow["other_ded_prem3"]       = txtb_other_ded_prem3.Text.ToString().Trim();
                        nrow["other_ded_prem4"]       = txtb_other_ded_prem4.Text.ToString().Trim();
                        nrow["other_ded_prem5"]       = txtb_other_ded_prem5.Text.ToString().Trim();
                        nrow["other_ded_prem6"]       = txtb_other_ded_prem6.Text.ToString().Trim();
                        nrow["other_ded_prem7"]       = txtb_other_ded_prem7.Text.ToString().Trim();
                        nrow["other_ded_prem8"]       = txtb_other_ded_prem8.Text.ToString().Trim();
                        nrow["other_ded_prem9"]       = txtb_other_ded_prem9.Text.ToString().Trim();
                        nrow["other_ded_prem10"]      = txtb_other_ded_prem10.Text.ToString().Trim();
                        nrow["other_ded_loan1"]       = txtb_other_ded_loan1.Text.ToString().Trim();
                        nrow["other_ded_loan2"]       = txtb_other_ded_loan2.Text.ToString().Trim();
                        nrow["other_ded_loan3"]       = txtb_other_ded_loan3.Text.ToString().Trim();
                        nrow["other_ded_loan4"]       = txtb_other_ded_loan4.Text.ToString().Trim();
                        nrow["other_ded_loan5"]       = txtb_other_ded_loan5.Text.ToString().Trim();
                        nrow["other_ded_loan6"]       = txtb_other_ded_loan6.Text.ToString().Trim();
                        nrow["other_ded_loan7"]       = txtb_other_ded_loan7.Text.ToString().Trim();
                        nrow["other_ded_loan8"]       = txtb_other_ded_loan8.Text.ToString().Trim();
                        nrow["other_ded_loan9"]       = txtb_other_ded_loan9.Text.ToString().Trim();
                        nrow["other_ded_loan10"]      = txtb_other_ded_loan10.Text.ToString().Trim();

                        nrow["payrolltemplate_code"]    = payrolltemplate_code.SelectedValue.ToString().Trim();
                        nrow["payrolltemplate_descr"]   = payrolltemplate_code.SelectedItem.ToString().Trim();
                        nrow["voucher_dtl_descr"]       = voucher_dtl_descr.Text.ToString().Trim();
                        nrow["is_differential"]         = is_differential.Checked;

                        // If the Template Code is Hononrarium - Hiddent TextBox
                        if (ddl_payroll_template.SelectedValue == "604" ||
                            ddl_payroll_template.SelectedValue == "704" ||
                            ddl_payroll_template.SelectedValue == "804")
                        {
                                nrow["wtax"] = txtb_birtax_summary.Text.ToString().Trim();
                        }
                        else
                        {
                                nrow["wtax"] = txtb_bir_tax.Text.ToString().Trim();
                        }
                        // END - Add Field Again  - 06/20/2019
                        nrow["post_status_descr"] = "NOT POSTED";
                        nrow["employee_name"] = txtb_employeename.Text.ToString().Trim();
                        
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND voucher_ctrl_nbr = '" + voucher_nbr.Text.ToString().Trim() + "' AND seq_no = '" + hidden_seq_no.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        
                        // BEGIN - VJA : 05/29/2019 - Header
                        row2Edit[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["voucher_ctrl_nbr"]        = voucher_nbr.Text.ToString().Trim();
                        row2Edit[0]["seq_no"]                  = hidden_seq_no.Text;
                        row2Edit[0]["voucher_period_from"]     = txtb_period_from.Text.ToString().Trim();
                        row2Edit[0]["voucher_period_to"]       = txtb_period_to.Text.ToString().Trim();
                        row2Edit[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                        row2Edit[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                        row2Edit[0]["rate_basis"]              = lbl_rate_basis_hidden.Text.ToString();
                        row2Edit[0]["monthly_rate"]            = lbl_monthly_rate_hidden.Text.ToString().Trim();
                        row2Edit[0]["daily_rate"]              = lbl_daily_rate_hidden.Text.ToString().Trim();
                        row2Edit[0]["hourly_rate"]             = lbl_hourly_rate_hidden.Text;
                        row2Edit[0]["no_of_days"]              = txtb_no_of_days.Text;
                        row2Edit[0]["other_amt1"]              = txtb_other_amount1.Text.ToString().Trim();
                        row2Edit[0]["other_amt2"]              = txtb_other_amount2.Text.ToString().Trim();
                        row2Edit[0]["other_amt3"]              = txtb_other_amount3.Text.ToString().Trim();
                        row2Edit[0]["lowp_amount_salary"]      = txtb_lwo_pay.Text.ToString().Trim();
                        row2Edit[0]["lowp_amount_pera"]        = txtb_lwop_amount_pera.Text.ToString().Trim();
                        row2Edit[0]["gsis_gs"]                 = txtb_gsis_gs.Text.ToString().Trim();
                        row2Edit[0]["gsis_ps"]                 = txtb_gsis_ps.Text.ToString().Trim();
                        row2Edit[0]["sif_gs"]                  = txtb_gsis_sif.Text.ToString().Trim();
                        row2Edit[0]["hdmf_gs"]                 = txtb_hdmf_gs.Text.ToString().Trim();
                        row2Edit[0]["hdmf_ps"]                 = txtb_hdmf_ps.Text.ToString().Trim();
                        row2Edit[0]["phic_gs"]                 = txtb_phic_gs.Text.ToString().Trim();
                        row2Edit[0]["phic_ps"]                 = txtb_phic_ps.Text.ToString().Trim();
                        row2Edit[0]["nhmfc_hsing"]             = txtb_nhmfc_hsng.Text.ToString().Trim();
                        row2Edit[0]["nafc_svlf"]               = txtb_nafc.Text.ToString().Trim();
                        row2Edit[0]["sss_ps"]                  = txtb_sss.Text.ToString().Trim();
                        row2Edit[0]["hdmf_ps2"]                = txtb_hdmf_addl.Text.ToString().Trim();
                        row2Edit[0]["hdmf_mp2"]                = txtb_hdmf_mp2.Text.ToString().Trim();
                        row2Edit[0]["philamlife_ps"]           = txtb_philam.Text.ToString().Trim();
                        row2Edit[0]["gsis_ehp"]                = txtb_gsis_ehp.Text.ToString().Trim();
                        row2Edit[0]["gsis_hip"]                = txtb_gsis_hip.Text.ToString().Trim();
                        row2Edit[0]["gsis_ceap"]               = txtb_gsis_ceap.Text.ToString().Trim();
                        row2Edit[0]["gsis_addl_ins"]           = txtb_gsis_add.Text.ToString().Trim();
                        row2Edit[0]["gsis_conso_ln"]           = txtb_gsis_consolidated.Text.ToString().Trim();
                        row2Edit[0]["gsis_policy_reg_ln"]      = txtb_gsis_policy_regular.Text.ToString().Trim();
                        row2Edit[0]["gsis_policy_opt_ln"]      = txtb_gsis_policy_optional.Text.ToString().Trim();
                        row2Edit[0]["gsis_uoli_ln"]            = txtb_gsis_ouli_loan.Text.ToString().Trim();
                        row2Edit[0]["gsis_emergency_ln"]       = txtb_gsis_emer_loan.Text.ToString().Trim();
                        row2Edit[0]["gsis_ecard_ln"]           = txtb_gsis_ecard_loan.Text.ToString().Trim();
                        row2Edit[0]["gsis_educ_asst_ln"]       = txtb_gsis_educ_loan.Text.ToString().Trim();
                        row2Edit[0]["gsis_real_state_ln"]      = txtb_gsis_real_loan.Text.ToString().Trim();
                        row2Edit[0]["gsis_sos_ln"]             = txtb_gsis_sos_loan.Text.ToString().Trim();
                        row2Edit[0]["gsis_help"]               = txtb_gsis_help.Text.ToString().Trim();
                        row2Edit[0]["gsis_housing_ln"]         = txtb_gsis_housing_loan.Text.ToString().Trim();
                        row2Edit[0]["hdmf_mpl_ln"]             = txtb_hdmf_mpl_loan.Text.ToString().Trim();
                        row2Edit[0]["hdmf_hse_ln"]             = txtb_hdmf_house_loan.Text.ToString().Trim();
                        row2Edit[0]["hdmf_cal_ln"]             = txtb_hdmf_cal_loan.Text.ToString().Trim();
                        row2Edit[0]["hdmf_loyalty_card"]       = txtb_hdmf_loyalty_card.Text.ToString().Trim();
                        row2Edit[0]["nico_ln"]                 = txtb_nico_loan.Text.ToString().Trim();
                        row2Edit[0]["network_ln"]              = txtb_networkbank_loan.Text.ToString().Trim();
                        row2Edit[0]["ccmpc_ln"]                = txtb_ccmpc_loan.Text.ToString().Trim();
                        row2Edit[0]["total_mandatory"]         = txtb_total_mandatory.Text.ToString().Trim();
                        row2Edit[0]["total_loans"]             = txtb_total_loans.Text.ToString().Trim();
                        row2Edit[0]["total_optional"]          = txtb_total_optional.Text.ToString().Trim();
                        row2Edit[0]["deduc_amt"]               = calculate_deduc_amt().ToString("###,##0.00");
                        
                        row2Edit[0]["other_loan1"]             = txtb_otherloan_no1.Text.ToString().Trim();
                        row2Edit[0]["other_loan2"]             = txtb_otherloan_no2.Text.ToString().Trim();
                        row2Edit[0]["other_loan3"]             = txtb_otherloan_no3.Text.ToString().Trim();
                        row2Edit[0]["other_loan4"]             = txtb_otherloan_no4.Text.ToString().Trim();
                        row2Edit[0]["other_loan5"]             = txtb_otherloan_no5.Text.ToString().Trim();
                        row2Edit[0]["other_premium1"]          = txtb_otherpremium_no1.Text.ToString().Trim();
                        row2Edit[0]["other_premium2"]          = txtb_otherpremium_no2.Text.ToString().Trim();
                        row2Edit[0]["other_premium3"]          = txtb_otherpremium_no3.Text.ToString().Trim();
                        row2Edit[0]["other_premium4"]          = txtb_otherpremium_no4.Text.ToString().Trim();
                        row2Edit[0]["other_premium5"]          = txtb_otherpremium_no5.Text.ToString().Trim();

                        row2Edit[0]["lates_mins_hrs"] = txtb_lates_min.Text.ToString().Trim();
                        row2Edit[0]["lates_amount"] = txtb_lates_amount.Text.ToString().Trim();
                        row2Edit[0]["refund_sal_amt"] = refund_sal_amt.Text.ToString().Trim();

                        // If the Template Code is Hononrarium - Hiddent TextBox
                        if (ddl_payroll_template.SelectedValue == "604" ||
                            ddl_payroll_template.SelectedValue == "704" ||
                            ddl_payroll_template.SelectedValue == "804")
                        {
                            row2Edit[0]["wtax"] = txtb_birtax_summary.Text.ToString().Trim();
                        }
                        else
                        {
                            row2Edit[0]["wtax"] = txtb_bir_tax.Text.ToString().Trim();
                        }
                        // END - Add Field Again  - 06/20/2019

                        if (row2Edit[0]["post_status"].ToString() == "R")
                        {
                            row2Edit[0]["post_status_descr"] = "RELEASED";
                        } 
                        else if (row2Edit[0]["post_status"].ToString() == "T") 
                        {
                            row2Edit[0]["post_status_descr"] = "RETURNED";
                        }
                        else 
                        { 
                            row2Edit[0]["post_status_descr"] = "NOT POSTED";
                        }

                        row2Edit[0]["employee_name"] = txtb_employeename.Text.ToString().Trim();
                        
                        // Add Field  2022-05-30
                        row2Edit[0]["other_ded_mand1"]       = txtb_other_ded_mand1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand2"]       = txtb_other_ded_mand2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand3"]       = txtb_other_ded_mand3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand4"]       = txtb_other_ded_mand4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand5"]       = txtb_other_ded_mand5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand6"]       = txtb_other_ded_mand6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand7"]       = txtb_other_ded_mand7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand8"]       = txtb_other_ded_mand8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand9"]       = txtb_other_ded_mand9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand10"]      = txtb_other_ded_mand10.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem1"]       = txtb_other_ded_prem1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem2"]       = txtb_other_ded_prem2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem3"]       = txtb_other_ded_prem3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem4"]       = txtb_other_ded_prem4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem5"]       = txtb_other_ded_prem5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem6"]       = txtb_other_ded_prem6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem7"]       = txtb_other_ded_prem7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem8"]       = txtb_other_ded_prem8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem9"]       = txtb_other_ded_prem9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem10"]      = txtb_other_ded_prem10.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan1"]       = txtb_other_ded_loan1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan2"]       = txtb_other_ded_loan2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan3"]       = txtb_other_ded_loan3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan4"]       = txtb_other_ded_loan4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan5"]       = txtb_other_ded_loan5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan6"]       = txtb_other_ded_loan6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan7"]       = txtb_other_ded_loan7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan8"]       = txtb_other_ded_loan8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan9"]       = txtb_other_ded_loan9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan10"]      = txtb_other_ded_loan10.Text.ToString().Trim();
                        
                        row2Edit[0]["payrolltemplate_code"]  = payrolltemplate_code.SelectedValue.ToString().Trim();
                        row2Edit[0]["payrolltemplate_descr"] = payrolltemplate_code.SelectedItem.ToString().Trim();
                        row2Edit[0]["voucher_dtl_descr"]     = voucher_dtl_descr.Text.ToString().Trim();
                        row2Edit[0]["is_differential"]       = is_differential.Checked;

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
        //  BEGIN - VJA- 06/06/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR voucher_ctrl_nbr LIKE '%" +    txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR employee_name LIKE '%" +       txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR monthly_rate LIKE '%" +        txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR payrolltemplate_descr LIKE '%" +     txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR post_status_descr LIKE '%" +   txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR net_pay LIKE '%" +             txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("voucher_ctrl_nbr", typeof(System.String));
            dtSource1.Columns.Add("payroll_month", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("voucher_period_from", typeof(System.String));
            dtSource1.Columns.Add("voucher_period_to", typeof(System.String));
            dtSource1.Columns.Add("voucher_descr1", typeof(System.String));
            dtSource1.Columns.Add("voucher_descr2", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("date_posted", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("rate_basis", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("daily_rate", typeof(System.String));
            dtSource1.Columns.Add("hourly_rate", typeof(System.String));
            dtSource1.Columns.Add("no_of_days", typeof(System.String));
            dtSource1.Columns.Add("other_amt1", typeof(System.String));
            dtSource1.Columns.Add("other_amt2", typeof(System.String));
            dtSource1.Columns.Add("other_amt3", typeof(System.String));
            dtSource1.Columns.Add("wtax", typeof(System.String));
            dtSource1.Columns.Add("lowp_amount_salary", typeof(System.String));
            dtSource1.Columns.Add("lowp_amount_pera", typeof(System.String));
            dtSource1.Columns.Add("gsis_gs", typeof(System.String));
            dtSource1.Columns.Add("gsis_ps", typeof(System.String));
            dtSource1.Columns.Add("sif_gs", typeof(System.String));
            dtSource1.Columns.Add("hdmf_gs", typeof(System.String));
            dtSource1.Columns.Add("hdmf_ps", typeof(System.String));
            dtSource1.Columns.Add("phic_gs", typeof(System.String));
            dtSource1.Columns.Add("phic_ps", typeof(System.String));
            dtSource1.Columns.Add("nhmfc_hsing", typeof(System.String));
            dtSource1.Columns.Add("nafc_svlf", typeof(System.String));
            dtSource1.Columns.Add("sss_ps", typeof(System.String));
            dtSource1.Columns.Add("hdmf_ps2", typeof(System.String));
            dtSource1.Columns.Add("hdmf_mp2", typeof(System.String));
            dtSource1.Columns.Add("philamlife_ps", typeof(System.String));
            dtSource1.Columns.Add("gsis_ehp", typeof(System.String));
            dtSource1.Columns.Add("gsis_hip", typeof(System.String));
            dtSource1.Columns.Add("gsis_ceap", typeof(System.String));
            dtSource1.Columns.Add("gsis_addl_ins", typeof(System.String));
            dtSource1.Columns.Add("gsis_conso_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_policy_reg_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_policy_opt_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_uoli_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_emergency_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_ecard_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_educ_asst_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_real_state_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_sos_ln", typeof(System.String));
            dtSource1.Columns.Add("gsis_help", typeof(System.String));
            dtSource1.Columns.Add("gsis_housing_ln", typeof(System.String));
            dtSource1.Columns.Add("hdmf_mpl_ln", typeof(System.String));
            dtSource1.Columns.Add("hdmf_hse_ln", typeof(System.String));
            dtSource1.Columns.Add("hdmf_cal_ln", typeof(System.String));
            dtSource1.Columns.Add("hdmf_loyalty_card", typeof(System.String));
            dtSource1.Columns.Add("nico_ln", typeof(System.String));
            dtSource1.Columns.Add("network_ln", typeof(System.String));
            dtSource1.Columns.Add("ccmpc_ln", typeof(System.String));
            dtSource1.Columns.Add("preparedby_name", typeof(System.String));
            dtSource1.Columns.Add("preparedby_designation", typeof(System.String));

            dtSource1.Columns.Add("employee_name", typeof(System.String));
            //dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_descr", typeof(System.String));

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
        //  BEGIN - VJA- 06/06/2019 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 06/06/2019 - Check if Object already contains value  
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
        //  BEGIN - VJA- 06/06/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            FieldValidationColorChanged(false, "ALL");
            bool validatedSaved = true;

            int target_tab = 6;

            //if (ddl_empl_id.SelectedValue == "" && lbl_addeditmode_hidden.Text == MyCmn.CONST_ADD)
            //{
            //    FieldValidationColorChanged(true, "ddl_empl_id");
            //    ddl_empl_id.Focus();
            //    validatedSaved = false;
            //}
            if (CommonCode.checkisdatetime(txtb_period_from) == false)
            {
                FieldValidationColorChanged(true, "invalid-date-txtb_period_from");
                txtb_period_from.Focus();
                validatedSaved = false;
                target_tab = 4;
            }
            if (CommonCode.checkisdatetime(txtb_period_to) == false)
            {
                FieldValidationColorChanged(true, "invalid-date-txtb_period_to");
                txtb_period_to.Focus();
                validatedSaved = false;
                target_tab = 4;
            }
            if (payrolltemplate_code.SelectedValue.ToString().Trim() == "" && 
                (ddl_payroll_template.SelectedValue.ToString().Trim() == "610" || ddl_payroll_template.SelectedValue.ToString().Trim() == "611" || ddl_payroll_template.SelectedValue.ToString().Trim() == "612" )
                )
            {
                lbl_payrolltemplate_code.Text    = MyCmn.CONST_RQDFLD;
                payrolltemplate_code.BorderColor = Color.Red;
                payrolltemplate_code.Focus();
                validatedSaved = false;
                target_tab = 4;
            }
            //if (voucher_dtl_descr.Text.ToString().Trim() == "")
            //{
            //    lbl_voucher_dtl_descr.Text = MyCmn.CONST_RQDFLD;
            //    voucher_dtl_descr.BorderColor = Color.Red;
            //    voucher_dtl_descr.Focus();
            //    validatedSaved = false;
            //    target_tab = 4;
            //}
            //if (txtb_voucher_descr1.Text == "")
            //{
            //    FieldValidationColorChanged(true, "txtb_voucher_descr1");
            //    txtb_voucher_descr1.Focus();
            //    validatedSaved = false;
            //    target_tab = 4;
            //}
            //if (txtb_voucher_descr2.Text == "")
            //{
            //    FieldValidationColorChanged(true, "txtb_voucher_descr2");
            //    txtb_voucher_descr2.Focus();
            //    validatedSaved = false;
            //    target_tab = 4;
            //}
            if (CommonCode.checkisdecimal(txtb_other_amount1) == false)
            {
                FieldValidationColorChanged(true, "txtb_other_amount1");
                txtb_other_amount1.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_other_amount2) == false)
            {
                FieldValidationColorChanged(true, "txtb_other_amount2");
                txtb_other_amount2.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_other_amount3) == false)
            {
                FieldValidationColorChanged(true, "txtb_other_amount3");
                txtb_other_amount3.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_lwop_amount_pera) == false)
            {
                FieldValidationColorChanged(true, "txtb_lwop_amount_pera");
                txtb_lwop_amount_pera.Focus();
                validatedSaved = false;
            }
            //if (CommonCode.checkisdecimal(txtb_no_of_days) == false)
            //{
            //    FieldValidationColorChanged(true, "txtb_no_of_days");
            //    txtb_no_of_days.Focus();
            //    validatedSaved = false;
            //}
            //if (CommonCode.checkisdecimal(txtb_no_days_worked) == false)
            //{
            //    FieldValidationColorChanged(true, "txtb_no_days_worked");
            //    txtb_no_days_worked.Focus();
            //    validatedSaved = false;
            //}
            //if (CommonCode.checkisdecimal(txtb_no_hours_worked) == false)
            //{
            //    FieldValidationColorChanged(true, "txtb_no_hours_worked");
            //    txtb_no_hours_worked.Focus();
            //    validatedSaved = false;
            //}
            if (CommonCode.checkisdecimal(txtb_gross_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_gross_pay");
                txtb_gross_pay.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_lwo_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_lwo_pay");
                txtb_lwo_pay.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_total_mandatory) == false)
            {
                FieldValidationColorChanged(true, "txtb_total_mandatory");
                txtb_total_mandatory.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_total_optional) == false)
            {
                FieldValidationColorChanged(true, "txtb_total_optional");
                txtb_total_optional.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_total_loans) == false)
            {
                FieldValidationColorChanged(true, "txtb_total_loans");
                txtb_total_loans.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_net_pay) == false)
            {
                FieldValidationColorChanged(true, "txtb_net_pay");
                txtb_net_pay.Focus();
                validatedSaved = false;
            }

            //Mandatory
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
            if (CommonCode.checkisdecimal(txtb_gsis_sif) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_sif");
                txtb_gsis_sif.Focus();
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
            //OPTIONAL DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_sss) == false)
            {
                FieldValidationColorChanged(true, "txtb_sss");
                txtb_sss.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_addl) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_addl");
                txtb_hdmf_addl.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_philam) == false)
            {
                FieldValidationColorChanged(true, "txtb_philam");
                txtb_philam.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ehp) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ehp");
                txtb_gsis_ehp.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_hip) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_hip");
                txtb_gsis_hip.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ceap) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ceap");
                txtb_gsis_ceap.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_add) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_add");
                txtb_gsis_add.Focus();
                validatedSaved = false;
                target_tab = 2;
            }

            if (CommonCode.checkisdecimal(txtb_hdmf_mp2) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_mp2");
                txtb_hdmf_mp2.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_loyalty_card) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_loyalty_card");
                txtb_hdmf_loyalty_card.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            //LOANS
            if (CommonCode.checkisdecimal(txtb_gsis_consolidated) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_consolidated");
                txtb_gsis_consolidated.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_policy_regular) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_policy_regular");
                txtb_gsis_policy_regular.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_policy_optional) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_policy_optional");
                txtb_gsis_policy_optional.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ouli_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ouli_loan");
                txtb_gsis_ouli_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_emer_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_emer_loan");
                txtb_gsis_emer_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_ecard_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_ecard_loan");
                txtb_gsis_ecard_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_educ_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_educ_loan");
                txtb_gsis_educ_loan.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_real_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_real_loan");
                txtb_gsis_real_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_sos_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_sos_loan");
                txtb_gsis_sos_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_mpl_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_mpl_loan");
                txtb_hdmf_mpl_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_house_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_house_loan");
                txtb_hdmf_house_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_hdmf_cal_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_hdmf_cal_loan");
                txtb_hdmf_cal_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_ccmpc_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_ccmpc_loan");
                txtb_ccmpc_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_nico_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_nico_loan");
                txtb_nico_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_networkbank_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_networkbank_loan");
                txtb_networkbank_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_nhmfc_hsng) == false)
            {
                FieldValidationColorChanged(true, "txtb_nhmfc_hsng");
                txtb_nhmfc_hsng.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_nafc) == false)
            {
                FieldValidationColorChanged(true, "txtb_nafc");
                txtb_nafc.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_help) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_help");
                txtb_gsis_help.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_gsis_housing_loan) == false)
            {
                FieldValidationColorChanged(true, "txtb_gsis_housing_loan");
                txtb_gsis_housing_loan.Focus();
                validatedSaved = false;
                target_tab = 3;
            }

            //Add validation
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr");
                txtb_voucher_nbr.Focus();
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
            if ((txtb_period_from.Text != "" && txtb_period_to.Text != "") && 
                (DateTime.Parse(txtb_period_from.Text) > DateTime.Parse(txtb_period_to.Text)))
            {
                FieldValidationColorChanged(true, "greater-than-period-from");
                txtb_period_from.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_otherpremium_no1) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no1");
                txtb_otherpremium_no1.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no2) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no2");
                txtb_otherpremium_no2.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no3) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no3");
                txtb_otherpremium_no3.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no4) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no4");
                txtb_otherpremium_no4.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherpremium_no5) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherpremium_no5");
                txtb_otherpremium_no5.Focus();
                validatedSaved = false;
                target_tab = 2;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no1) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no1");
                txtb_otherloan_no1.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no2) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no2");
                txtb_otherloan_no2.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no3) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no3");
                txtb_otherloan_no3.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no4) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no4");
                txtb_otherloan_no4.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_otherloan_no5) == false)
            {
                FieldValidationColorChanged(true, "txtb_otherloan_no5");
                txtb_otherloan_no5.Focus();
                validatedSaved = false;
                target_tab = 3;
            }
            if (CommonCode.checkisdecimal(txtb_payroll_amt1) == false)
            {
                FieldValidationColorChanged(true, "txtb_payroll_amt1");
                txtb_payroll_amt1.Focus();
                validatedSaved = false;
                target_tab = 5;
            }
            if (CommonCode.checkisdecimal(txtb_payroll_amt2) == false)
            {
                FieldValidationColorChanged(true, "txtb_payroll_amt2");
                txtb_payroll_amt2.Focus();
                validatedSaved = false;
                target_tab = 5;
            }
            if (CommonCode.checkisdecimal(txtb_payroll_amt3) == false)
            {
                FieldValidationColorChanged(true, "txtb_payroll_amt3");
                txtb_payroll_amt3.Focus();
                validatedSaved = false;
                target_tab = 5;
            }
            if (CommonCode.checkisdecimal(txtb_payroll_amt4) == false)
            {
                FieldValidationColorChanged(true, "txtb_payroll_amt4");
                txtb_payroll_amt4.Focus();
                validatedSaved = false;
                target_tab = 5;
            }
            if (CommonCode.checkisdecimal(txtb_payroll_amt5) == false)
            {
                FieldValidationColorChanged(true, "txtb_payroll_amt5");
                txtb_payroll_amt5.Focus();
                validatedSaved = false;
                target_tab = 5;
            }

            if (CommonCode.checkisdecimal(txtb_lates_min) == false)
            {
                FieldValidationColorChanged(true, "txtb_lates_min");
                txtb_lates_min.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(refund_sal_amt) == false)
            {
                FieldValidationColorChanged(true, "txtb_other_amount4");
                refund_sal_amt.Focus();
                validatedSaved = false;
            }
            // MANDATORY VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_mand1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand1");  txtb_other_ded_mand1.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand2");  txtb_other_ded_mand2.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand3");  txtb_other_ded_mand3.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand4");  txtb_other_ded_mand4.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand5");  txtb_other_ded_mand5.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand6");  txtb_other_ded_mand6.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand7");  txtb_other_ded_mand7.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand8");  txtb_other_ded_mand8.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand9");  txtb_other_ded_mand9.Focus()  ; validatedSaved  = false; target_tab = 1; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_mand10"); txtb_other_ded_mand10.Focus() ; validatedSaved  = false; target_tab = 1; }

            // OPTIONAL VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_prem1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem1");  txtb_other_ded_prem1.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem2");  txtb_other_ded_prem2.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem3");  txtb_other_ded_prem3.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem4");  txtb_other_ded_prem4.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem5");  txtb_other_ded_prem5.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem6");  txtb_other_ded_prem6.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem7");  txtb_other_ded_prem7.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem8");  txtb_other_ded_prem8.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem9");  txtb_other_ded_prem9.Focus()  ; validatedSaved  = false; target_tab = 2; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_prem10"); txtb_other_ded_prem10.Focus() ; validatedSaved  = false; target_tab = 2; }
             
            // LOAN VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_loan1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan1");  txtb_other_ded_loan1.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan2");  txtb_other_ded_loan2.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan3");  txtb_other_ded_loan3.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan4");  txtb_other_ded_loan4.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan5");  txtb_other_ded_loan5.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan6");  txtb_other_ded_loan6.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan7");  txtb_other_ded_loan7.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan8");  txtb_other_ded_loan8.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan9");  txtb_other_ded_loan9.Focus()  ; validatedSaved  = false; target_tab = 3; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_loan10"); txtb_other_ded_loan10.Focus() ; validatedSaved  = false; target_tab = 3; }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopClickTab1", "click_tab(" + target_tab + ")", true);
            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {

                    //case "txtb_no_of_days":
                    //    {
                    //        LblRequired1.Text = MyCmn.CONST_INVALID_NUMERIC;
                    //        txtb_no_of_days.BorderColor = Color.Red;
                    //        break;
                    //    }
                    //case "txtb_no_days_worked":
                    //    {
                    //        LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                    //        txtb_no_days_worked.BorderColor = Color.Red;
                    //        break;
                    //    }
                    //case "txtb_no_hours_worked":
                    //    {
                    //        LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                    //        txtb_no_hours_worked.BorderColor = Color.Red;
                    //        break;
                    //    }
                    case "txtb_gross_pay":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_lwo_pay":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lwo_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_total_mandatory":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_mandatory.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_total_optional":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_optional.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_total_loans":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_total_loans.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_net_pay":
                        {
                            LblRequired10.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_other_amount1":
                        {
                            LblRequired68.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount1.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_other_amount2":
                        {
                            LblRequired69.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount2.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_other_amount3":
                        {
                            LblRequired70.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount3.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_lwop_amount_pera":
                        {
                            LblRequired71.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lwop_amount_pera.BorderColor = Color.Red;
                            break;
                        }
                    //Mandatory
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
                    case "txtb_gsis_sif":
                        {
                            LblRequired16.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_sif.BorderColor = Color.Red;
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

                    //OPTIONAL DEDUCTIONS

                    case "txtb_sss":
                        {
                            LblRequired28.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_sss.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_hdmf_addl":
                        {
                            LblRequired29.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_addl.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_philam":
                        {
                            LblRequired30.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_philam.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_ehp":
                        {
                            LblRequired31.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ehp.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_hip":
                        {
                            LblRequired32.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_hip.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_ceap":
                        {
                            LblRequired33.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ceap.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_add":
                        {
                            LblRequired34.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_add.BorderColor = Color.Red;

                            break;
                        }

                    case "txtb_hdmf_mp2":
                        {
                            LblRequired66.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_mp2.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_hdmf_loyalty_card":
                        {
                            LblRequired67.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_loyalty_card.BorderColor = Color.Red;

                            break;
                        }

                    //LOANS
                    case "txtb_gsis_consolidated":
                        {
                            LblRequired40.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_consolidated.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_policy_regular":
                        {
                            LblRequired41.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_policy_regular.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_policy_optional":
                        {
                            LblRequired42.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_policy_optional.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_ouli_loan":
                        {
                            LblRequired43.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ouli_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_emer_loan":
                        {
                            LblRequired44.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_emer_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_ecard_loan":
                        {
                            LblRequired45.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_ecard_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_educ_loan":
                        {
                            LblRequired46.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_educ_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_real_loan":
                        {
                            LblRequired47.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_real_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_gsis_sos_loan":
                        {
                            LblRequired48.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_sos_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_hdmf_mpl_loan":
                        {
                            LblRequired49.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_mpl_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_hdmf_house_loan":
                        {
                            LblRequired50.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_house_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_hdmf_cal_loan":
                        {
                            LblRequired51.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hdmf_cal_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_ccmpc_loan":
                        {
                            LblRequired52.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_ccmpc_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_nico_loan":
                        {
                            LblRequired53.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nico_loan.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_networkbank_loan":
                        {
                            LblRequired54.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_networkbank_loan.BorderColor = Color.Red;

                            break;
                        }

                    case "below-minumum-net":
                        {
                            LblRequired10.Text = "Net Pay is Below Minimum! ";
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    //case "already-exist":
                    //    {
                    //        LblRequired60.Text = "already exist!";
                    //        ddl_empl_id.BorderColor = Color.Red;
                    //        break;
                    //    }
                    //case "ddl_empl_id":
                    //    {
                    //        LblRequired60.Text = MyCmn.CONST_RQDFLD;
                    //        ddl_empl_id.BorderColor = Color.Red;
                    //        break;
                    //    }
                    case "txtb_nhmfc_hsng":
                        {
                            LblRequired62.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nhmfc_hsng.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_nafc":
                        {
                            LblRequired63.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nafc.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_help":
                        {
                            LblRequired64.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_help.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_gsis_housing_loan":
                        {
                            LblRequired65.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gsis_housing_loan.BorderColor = Color.Red;
                            break;
                        }

                    // For Header Validation

                    //case "txtb_voucher_descr1":
                    //    {
                    //        LblRequired201.Text = MyCmn.CONST_RQDFLD;
                    //        txtb_voucher_descr1.BorderColor = Color.Red;
                    //        break;
                    //    }
                    //case "txtb_voucher_descr2":
                    //    {
                    //        LblRequired202.Text = MyCmn.CONST_RQDFLD;
                    //        txtb_voucher_descr2.BorderColor = Color.Red;
                    //        break;
                    //    }
                    case "invalid-date-txtb_period_from":
                        {
                            LblRequired203.Text = "Invalid Date !";
                            txtb_period_from.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-date-txtb_period_to":
                        {
                            LblRequired204.Text = "Invalid Date !";
                            txtb_period_to.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_voucher_nbr":
                        {
                            LblRequired22.Text = MyCmn.CONST_RQDFLD;
                            txtb_voucher_nbr.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_reason":
                        {
                            LblRequired205.Text = MyCmn.CONST_RQDFLD;
                            txtb_reason.BorderColor = Color.Red;
                            break;
                        }
                    case "greater-than-period-from":
                        {
                            LblRequired203.Text = "greater than period from!";
                            txtb_period_from.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_period_from":
                        {
                            LblRequired203.Text = MyCmn.CONST_RQDFLD;
                            txtb_period_from.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_period_to":
                        {
                            LblRequired204.Text = MyCmn.CONST_RQDFLD;
                            txtb_period_to.BorderColor = Color.Red;
                            break;
                        }
                    //case "ddl_select_report":
                    //    {
                    //        LblRequired0.Text = MyCmn.CONST_RQDFLD;
                    //        ddl_select_report.BorderColor = Color.Red;
                    //        break;
                    //    }

                    case "txtb_otherpremium_no1":
                        {
                            LblRequired35.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no1.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherpremium_no2":
                        {
                            LblRequired36.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no2.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherpremium_no3":
                        {
                            LblRequired37.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no3.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherpremium_no4":
                        {
                            LblRequired38.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no4.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherpremium_no5":
                        {
                            LblRequired39.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherpremium_no5.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherloan_no1":
                        {
                            LblRequired55.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no1.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherloan_no2":
                        {
                            LblRequired56.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no2.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherloan_no3":
                        {
                            LblRequired57.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no3.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherloan_no4":
                        {
                            LblRequired58.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no4.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_otherloan_no5":
                        {
                            LblRequired59.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_otherloan_no5.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_payroll_amt1":
                        {
                            LblRequired301.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_payroll_amt1.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_payroll_amt2":
                        {
                            LblRequired303.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_payroll_amt2.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_payroll_amt3":
                        {
                            LblRequired305.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_payroll_amt3.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_payroll_amt4":
                        {
                            LblRequired307.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_payroll_amt4.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_payroll_amt5":
                        {
                            LblRequired309.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_payroll_amt5.BorderColor = Color.Red;

                            break;
                        }

                    case "txtb_lates_min":
                        {
                            LblRequired100.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lates_min.BorderColor = Color.Red;

                            break;
                        }
                    case "txtb_other_amount4":
                        {
                            LblRequired102.Text = MyCmn.CONST_INVALID_NUMERIC;
                            refund_sal_amt.BorderColor = Color.Red;

                            break;
                        }
                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_mand1":  { req_other_ded_mand1.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand1.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand2":  { req_other_ded_mand2.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand2.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand3":  { req_other_ded_mand3.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand3.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand4":  { req_other_ded_mand4.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand4.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand5":  { req_other_ded_mand5.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand5.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand6":  { req_other_ded_mand6.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand6.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand7":  { req_other_ded_mand7.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand7.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand8":  { req_other_ded_mand8.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand8.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand9":  { req_other_ded_mand9.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand9.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_mand10": { req_other_ded_mand10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand10.BorderColor = Color.Red; break;}

                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_prem1":  { req_other_ded_prem1.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem1.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem2":  { req_other_ded_prem2.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem2.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem3":  { req_other_ded_prem3.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem3.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem4":  { req_other_ded_prem4.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem4.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem5":  { req_other_ded_prem5.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem5.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem6":  { req_other_ded_prem6.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem6.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem7":  { req_other_ded_prem7.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem7.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem8":  { req_other_ded_prem8.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem8.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem9":  { req_other_ded_prem9.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem9.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_prem10": { req_other_ded_prem10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem10.BorderColor = Color.Red; break;}

                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_loan1":  { req_other_ded_loan1.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan1.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan2":  { req_other_ded_loan2.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan2.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan3":  { req_other_ded_loan3.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan3.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan4":  { req_other_ded_loan4.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan4.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan5":  { req_other_ded_loan5.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan5.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan6":  { req_other_ded_loan6.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan6.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan7":  { req_other_ded_loan7.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan7.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan8":  { req_other_ded_loan8.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan8.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan9":  { req_other_ded_loan9.Text  = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan9.BorderColor  = Color.Red; break;}
                    case "txtb_other_ded_loan10": { req_other_ded_loan10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan10.BorderColor = Color.Red; break;}


                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            //LblRequired0.Text = "";
                            //LblRequired1.Text = "";
                            LblRequired4.Text = "";
                            LblRequired6.Text = "";
                            LblRequired7.Text = "";
                            LblRequired8.Text = "";
                            LblRequired9.Text = "";
                            LblRequired10.Text = "";
                            LblRequired13.Text = "";
                            LblRequired15.Text = "";
                            LblRequired16.Text = "";
                            LblRequired17.Text = "";
                            LblRequired18.Text = "";
                            LblRequired19.Text = "";
                            LblRequired20.Text = "";
                            LblRequired21.Text = "";
                            LblRequired28.Text = "";
                            LblRequired29.Text = "";
                            LblRequired30.Text = "";
                            LblRequired31.Text = "";
                            LblRequired32.Text = "";
                            LblRequired33.Text = "";
                            LblRequired34.Text = "";
                            LblRequired40.Text = "";
                            LblRequired41.Text = "";
                            LblRequired42.Text = "";
                            LblRequired43.Text = "";
                            LblRequired44.Text = "";
                            LblRequired45.Text = "";
                            LblRequired46.Text = "";
                            LblRequired47.Text = "";
                            LblRequired48.Text = "";
                            LblRequired49.Text = "";
                            LblRequired50.Text = "";
                            LblRequired51.Text = "";
                            LblRequired52.Text = "";
                            LblRequired53.Text = "";
                            LblRequired54.Text = "";
                            LblRequired60.Text = "";
                            LblRequired62.Text = "";
                            LblRequired63.Text = "";
                            LblRequired64.Text = "";
                            LblRequired65.Text = "";
                            LblRequired66.Text = "";
                            LblRequired67.Text = "";
                            LblRequired68.Text = "";
                            LblRequired69.Text = "";
                            LblRequired70.Text = "";
                            LblRequired71.Text = "";
                            //LblRequired201.Text = "";
                            //LblRequired202.Text = "";
                            LblRequired203.Text = "";
                            LblRequired204.Text = "";
                            LblRequired205.Text = "";
                            LblRequired22.Text = "";
                            LblRequired35.Text = "";
                            LblRequired36.Text = "";
                            LblRequired37.Text = "";
                            LblRequired38.Text = "";
                            LblRequired39.Text = "";
                            LblRequired55.Text = "";
                            LblRequired56.Text = "";
                            LblRequired57.Text = "";
                            LblRequired58.Text = "";
                            LblRequired59.Text = "";

                            LblRequired301.Text = "";
                            LblRequired303.Text = "";
                            LblRequired305.Text = "";
                            LblRequired307.Text = "";
                            LblRequired309.Text = "";

                            //ddl_select_report.BorderColor = Color.LightGray;
                            txtb_voucher_nbr.BorderColor = Color.LightGray;
                            txtb_reason.BorderColor = Color.LightGray;
                            txtb_other_amount1.BorderColor = Color.LightGray;
                            txtb_other_amount2.BorderColor = Color.LightGray;
                            txtb_other_amount3.BorderColor = Color.LightGray;
                            txtb_lwop_amount_pera.BorderColor = Color.LightGray;
                            //txtb_no_of_days.BorderColor = Color.LightGray;
                            //txtb_no_days_worked.BorderColor = Color.LightGray;
                            //txtb_no_hours_worked.BorderColor = Color.LightGray;
                            txtb_gross_pay.BorderColor = Color.LightGray;
                            txtb_lwo_pay.BorderColor = Color.LightGray;
                            txtb_total_mandatory.BorderColor = Color.LightGray;
                            txtb_total_optional.BorderColor = Color.LightGray;
                            txtb_total_loans.BorderColor = Color.LightGray;
                            txtb_net_pay.BorderColor = Color.LightGray;
                            txtb_gsis_gs.BorderColor = Color.LightGray;
                            txtb_gsis_ps.BorderColor = Color.LightGray;
                            txtb_gsis_sif.BorderColor = Color.LightGray;
                            txtb_hdmf_gs.BorderColor = Color.LightGray;
                            txtb_hdmf_ps.BorderColor = Color.LightGray;
                            txtb_phic_gs.BorderColor = Color.LightGray;
                            txtb_phic_ps.BorderColor = Color.LightGray;
                            txtb_bir_tax.BorderColor = Color.LightGray;

                            txtb_sss.BorderColor = Color.LightGray;
                            txtb_hdmf_addl.BorderColor = Color.LightGray;
                            txtb_philam.BorderColor = Color.LightGray;
                            txtb_gsis_ehp.BorderColor = Color.LightGray;
                            txtb_gsis_hip.BorderColor = Color.LightGray;
                            txtb_gsis_ceap.BorderColor = Color.LightGray;
                            txtb_gsis_add.BorderColor = Color.LightGray;
                            txtb_hdmf_mp2.BorderColor = Color.LightGray;

                            txtb_gsis_consolidated.BorderColor = Color.LightGray;
                            txtb_gsis_policy_regular.BorderColor = Color.LightGray;
                            txtb_gsis_policy_optional.BorderColor = Color.LightGray;
                            txtb_gsis_ouli_loan.BorderColor = Color.LightGray;
                            txtb_gsis_emer_loan.BorderColor = Color.LightGray;
                            txtb_gsis_ecard_loan.BorderColor = Color.LightGray;
                            txtb_gsis_educ_loan.BorderColor = Color.LightGray;
                            txtb_gsis_real_loan.BorderColor = Color.LightGray;
                            txtb_gsis_sos_loan.BorderColor = Color.LightGray;
                            txtb_hdmf_mpl_loan.BorderColor = Color.LightGray;
                            txtb_hdmf_house_loan.BorderColor = Color.LightGray;
                            txtb_hdmf_cal_loan.BorderColor = Color.LightGray;
                            txtb_ccmpc_loan.BorderColor = Color.LightGray;
                            txtb_nico_loan.BorderColor = Color.LightGray;
                            txtb_networkbank_loan.BorderColor = Color.LightGray;
                            txtb_nhmfc_hsng.BorderColor = Color.LightGray;
                            txtb_nafc.BorderColor = Color.LightGray;
                            txtb_gsis_help.BorderColor = Color.LightGray;
                            txtb_gsis_housing_loan.BorderColor = Color.LightGray;
                            txtb_hdmf_loyalty_card.BorderColor = Color.LightGray;

                            txtb_otherloan_no1.BorderColor = Color.LightGray;
                            txtb_otherloan_no2.BorderColor = Color.LightGray;
                            txtb_otherloan_no3.BorderColor = Color.LightGray;
                            txtb_otherloan_no4.BorderColor = Color.LightGray;
                            txtb_otherloan_no5.BorderColor = Color.LightGray;
                            txtb_otherpremium_no1.BorderColor = Color.LightGray;
                            txtb_otherpremium_no2.BorderColor = Color.LightGray;
                            txtb_otherpremium_no3.BorderColor = Color.LightGray;
                            txtb_otherpremium_no4.BorderColor = Color.LightGray;
                            txtb_otherpremium_no5.BorderColor = Color.LightGray;

                            txtb_payroll_amt1.BorderColor = Color.LightGray;
                            txtb_payroll_amt2.BorderColor = Color.LightGray;
                            txtb_payroll_amt3.BorderColor = Color.LightGray;
                            txtb_payroll_amt4.BorderColor = Color.LightGray;
                            txtb_payroll_amt5.BorderColor = Color.LightGray;

                            //ddl_empl_id.BorderColor = Color.LightGray;

                            // BEGIN - Header
                            //txtb_voucher_descr1.BorderColor = Color.LightGray;
                            //txtb_voucher_descr2.BorderColor = Color.LightGray;
                            txtb_period_from.BorderColor = Color.LightGray;
                            txtb_period_to.BorderColor = Color.LightGray;
                            // END   - Header
                            txtb_lates_min.BorderColor = Color.LightGray;
                            refund_sal_amt.BorderColor = Color.LightGray;

                            req_other_ded_mand1.Text = "";
                            req_other_ded_mand2.Text = "";
                            req_other_ded_mand3.Text = "";
                            req_other_ded_mand4.Text = "";
                            req_other_ded_mand5.Text = "";
                            req_other_ded_mand6.Text = "";
                            req_other_ded_mand7.Text = "";
                            req_other_ded_mand8.Text = "";
                            req_other_ded_mand9.Text = "";
                            req_other_ded_mand10.Text = "";

                            req_other_ded_prem1.Text = "";
                            req_other_ded_prem2.Text = "";
                            req_other_ded_prem3.Text = "";
                            req_other_ded_prem4.Text = "";
                            req_other_ded_prem5.Text = "";
                            req_other_ded_prem6.Text = "";
                            req_other_ded_prem7.Text = "";
                            req_other_ded_prem8.Text = "";
                            req_other_ded_prem9.Text = "";
                            req_other_ded_prem10.Text = "";

                            req_other_ded_loan1.Text = "";
                            req_other_ded_loan2.Text = "";
                            req_other_ded_loan3.Text = "";
                            req_other_ded_loan4.Text = "";
                            req_other_ded_loan5.Text = "";
                            req_other_ded_loan6.Text = "";
                            req_other_ded_loan7.Text = "";
                            req_other_ded_loan8.Text = "";
                            req_other_ded_loan9.Text = "";
                            req_other_ded_loan10.Text = "";
                            lbl_voucher_dtl_descr.Text = "";
                            lbl_payrolltemplate_code.Text = "";

                            txtb_other_ded_mand1.BorderColor = Color.LightGray;
                            txtb_other_ded_mand2.BorderColor = Color.LightGray;
                            txtb_other_ded_mand3.BorderColor = Color.LightGray;
                            txtb_other_ded_mand4.BorderColor = Color.LightGray;
                            txtb_other_ded_mand5.BorderColor = Color.LightGray;
                            txtb_other_ded_mand6.BorderColor = Color.LightGray;
                            txtb_other_ded_mand7.BorderColor = Color.LightGray;
                            txtb_other_ded_mand8.BorderColor = Color.LightGray;
                            txtb_other_ded_mand9.BorderColor = Color.LightGray;
                            txtb_other_ded_mand10.BorderColor = Color.LightGray;

                            txtb_other_ded_prem1.BorderColor = Color.LightGray;
                            txtb_other_ded_prem2.BorderColor = Color.LightGray;
                            txtb_other_ded_prem3.BorderColor = Color.LightGray;
                            txtb_other_ded_prem4.BorderColor = Color.LightGray;
                            txtb_other_ded_prem5.BorderColor = Color.LightGray;
                            txtb_other_ded_prem6.BorderColor = Color.LightGray;
                            txtb_other_ded_prem7.BorderColor = Color.LightGray;
                            txtb_other_ded_prem8.BorderColor = Color.LightGray;
                            txtb_other_ded_prem9.BorderColor = Color.LightGray;
                            txtb_other_ded_prem10.BorderColor = Color.LightGray;

                            txtb_other_ded_loan1.BorderColor = Color.LightGray;
                            txtb_other_ded_loan2.BorderColor = Color.LightGray;
                            txtb_other_ded_loan3.BorderColor = Color.LightGray;
                            txtb_other_ded_loan4.BorderColor = Color.LightGray;
                            txtb_other_ded_loan5.BorderColor = Color.LightGray;
                            txtb_other_ded_loan6.BorderColor = Color.LightGray;
                            txtb_other_ded_loan7.BorderColor = Color.LightGray;
                            txtb_other_ded_loan8.BorderColor = Color.LightGray;
                            txtb_other_ded_loan9.BorderColor = Color.LightGray;
                            txtb_other_ded_loan10.BorderColor = Color.LightGray;

                            payrolltemplate_code.BorderColor = Color.LightGray;
                            voucher_dtl_descr.BorderColor = Color.LightGray;
                            
                            UpdateDateFrom.Update();
                            UpdateDateTo.Update();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop_showdate", "show_date();", true);
                            break;
                        }
                }
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Triggers when Select Employment Type
        //*************************************************************************
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "" && ddl_month.SelectedValue != "" && ddl_empl_type.SelectedValue != "" && ddl_payroll_template.SelectedValue != "" && ddl_dep.SelectedValue != "")
            {
                RetrieveDataListGrid();
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetriveTemplate();
            UpdatePanel10.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Triggers when Select Department
        //*************************************************************************
        protected void ddl_dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "" && ddl_month.SelectedValue != "" && ddl_empl_type.SelectedValue != "" && ddl_payroll_template.SelectedValue != "" && ddl_dep.SelectedValue != "")
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
                FieldValidationColorChanged(true, "ddl_dep");
            }
            UpdatePanel10.Update();
            RetrieveDataListGrid();
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Triggers when Click No Keep It
        //*************************************************************************
        protected void lnk_btn_keepit_Command(object sender, CommandEventArgs e)
        {
            ddl_dep.SelectedValue = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "closeNotification1();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Triggers when Select Payroll Year
        //*************************************************************************
        protected void ddl_year_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "" && ddl_month.SelectedValue != "" && ddl_empl_type.SelectedValue != "" && ddl_payroll_template.SelectedValue != "" && ddl_dep.SelectedValue != "")
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;

            }
            RetrieveDataListGrid();
            UpdatePanel10.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Triggers when Select Payroll Template Type
        //*************************************************************************
        protected void ddl_payroll_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "" && ddl_month.SelectedValue != "" && ddl_empl_type.SelectedValue != "" && ddl_payroll_template.SelectedValue != "" && ddl_dep.SelectedValue != "")
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;

            }
            RetrieveDataListGrid();
            UpdatePanel10.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Triggers when Select Month
        //*************************************************************************
        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "" && ddl_month.SelectedValue != "" && ddl_empl_type.SelectedValue != "" && ddl_payroll_template.SelectedValue != "" && ddl_dep.SelectedValue != "")
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveDataListGrid();
            UpdatePanel10.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Triggers when Click Calculate 
        //*************************************************************************
        protected void btn_calculate_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                Calculate_MDH();
                calculate_mandatory();
                calculate_optional();
                calculate_loans();
                calculate_maternity();
                Calculate_LatesAmount();
                calculate_grosspay();
                calculate_netpays();
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Calculate Gross Pay Amount
        //*************************************************************************
        private void calculate_grosspay()
        {
            double total_gross = 0;
            string total_str_grosspay = "";

            switch (ddl_payroll_template.SelectedValue)
            {
                //Template Code for Refund To Employee
                case "601":
                case "701":
                case "801":
                    total_gross = 0;
                    total_str_grosspay = total_gross.ToString("###,##0.0000");
                    txtb_gross_pay.Text = total_str_grosspay.Split('.')[0] + "." + total_str_grosspay.Split('.')[1].Substring(0, 2);
                    break;

                // Template Code for Honorarium
                case "604":
                case "704":
                case "804":
                    total_gross = double.Parse(txtb_gross_pay.Text.ToString()) - double.Parse(txtb_lates_amount.Text.ToString());
                    total_str_grosspay = total_gross.ToString("###,##0.0000");
                    txtb_gross_pay.Text = total_str_grosspay.Split('.')[0] + "." + total_str_grosspay.Split('.')[1].Substring(0, 2);
                    break;

                // Template Code for Terminal Leave - Calculate Gross = (MR * no of Days * constant factor)
                case "603":
                case "703":
                case "803":

                    // Switch For Employment Type
                    switch (ddl_empl_type.SelectedValue)
                    {
                        case "RE": // If Regular = Monthly Rate * No of Days * Constatnt Factor for  Mone/Terminal Leave
                            total_gross = double.Parse(txtb_rate_amount.Text.ToString()) * double.Parse(txtb_no_of_days.Text.ToString()) * double.Parse(lbl_mone_contant_hidden.Text.ToString());
                            break;
                        case "CE": // If Casual = Daily Rate * Conversion Factor of Monthly Rate * No of Days * Constatnt Factor for Mone/Terminal Leave
                            total_gross = double.Parse(txtb_rate_amount.Text) * double.Parse(lbl_installation_monthly_conv_hidden.Text) * double.Parse(txtb_no_of_days.Text.ToString()) * double.Parse(lbl_mone_contant_hidden.Text.ToString());
                            break;
                        case "JO": // Thier is no Terminal Leave for Job-Order
                            total_gross = 0;
                            break;
                    }
                    total_gross = total_gross - double.Parse(txtb_lates_amount.Text.ToString());
                    total_str_grosspay = total_gross.ToString("###,##0.0000");
                    txtb_gross_pay.Text = total_str_grosspay.Split('.')[0] + "." + total_str_grosspay.Split('.')[1].Substring(0, 2);


                    txtb_gross_pay.Text = total_gross.ToString("###,##0.00");
                    break;

                // Template Code for Other Salaries - Calculate Gross
                case "605":
                case "705":
                case "805":
                    if (Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "1")       // Other Salaries - First Claim (Promotion)
                    {
                        total_gross = double.Parse(txtb_other_amount2.Text.ToString()) - double.Parse(txtb_other_amount3.Text.ToString());
                        total_gross = total_gross - double.Parse(txtb_lates_amount.Text.ToString());
                    }
                    if (Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "2")       // Other Salaries - Sal. Diff (Multiple Months)
                    {
                        total_gross = double.Parse(txtb_other_amount2.Text.ToString()) - double.Parse(txtb_other_amount3.Text.ToString());
                        total_gross = total_gross - double.Parse(txtb_lates_amount.Text.ToString());
                    }
                    else
                    {
                        total_gross = double.Parse(txtb_gross_pay.Text.ToString());
                    }

                   
                    // Wala na syay gamit ang calculation sa Gross pay kay User Inputed ang Gross pay
                    // Switch For Employment Type
                    total_str_grosspay = total_gross.ToString("###,##0.0000");
                    txtb_gross_pay.Text = total_str_grosspay.Split('.')[0] + "." + total_str_grosspay.Split('.')[1].Substring(0, 2);
                    break;

                // Template Code for : Refund to Employeer - MidYear Bonus - Calculate Gross
                case "606":
                case "706":
                case "806":
                    total_gross = 0;
                    total_str_grosspay = total_gross.ToString("###,##0.0000");
                    txtb_gross_pay.Text = total_str_grosspay.Split('.')[0] + "." + total_str_grosspay.Split('.')[1].Substring(0, 2);
                    break;

                // Template Code for : Refund to Employeer - YearEnd Bonus - Calculate Gross
                case "607":
                case "707":
                case "807":
                    total_gross = 0;
                    total_str_grosspay = total_gross.ToString("###,##0.0000");
                    txtb_gross_pay.Text = total_str_grosspay.Split('.')[0] + "." + total_str_grosspay.Split('.')[1].Substring(0, 2);
                    break;

                //Template Code for : Maternity for Casual and Regular
                case "608":
                case "708":
                case "808":
                    calculate_maternity();
                    // total_str_grosspay = total_gross.ToString("###,##0.0000");
                    // txtb_gross_pay.Text = total_str_grosspay.Split('.')[0] + "." + total_str_grosspay.Split('.')[1].Substring(0, 2);
                    break;
                //Template Code for : Other Claims/Refund
                case "609":
                case "709":
                case "809":
                    calculate_oth_claims_refund();
                    break;
                case "610": // Other Claims - v2
                case "611": // Other Claims - v2
                case "612": // Other Claims - v2
                    if (is_differential.Checked == true) 
                    {
                        total_gross = (double.Parse(txtb_other_amount1.Text.ToString()) + double.Parse(txtb_other_amount2.Text.ToString())) - double.Parse(txtb_other_amount3.Text.ToString());
                        total_gross = total_gross - double.Parse(txtb_lates_amount.Text.ToString());
                    }
                    else
                    {
                        total_gross = double.Parse(txtb_gross_pay.Text.ToString());
                    }
                    total_str_grosspay = total_gross.ToString("###,##0.0000");
                    txtb_gross_pay.Text = total_str_grosspay.Split('.')[0] + "." + total_str_grosspay.Split('.')[1].Substring(0, 2);
                    break;

            }
            
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Calculate Net Pays
        //*************************************************************************
        private void calculate_netpays()
        {
            double total_netpay = 0;
            string total_str_netpay = "";

            switch (ddl_payroll_template.SelectedValue)
            {
                // Template Code for Refund To Employee - Lwop's amount + Deductions
                case "601":
                case "701":
                case "801":
                    total_netpay = total_netpay + double.Parse(txtb_lwo_pay.Text.ToString().Trim());
                    total_netpay = total_netpay + double.Parse(txtb_lwop_amount_pera.Text.ToString().Trim());
                    total_netpay = total_netpay + double.Parse(txtb_total_mandatory.Text.ToString().Trim());
                    total_netpay = total_netpay + double.Parse(txtb_total_optional.Text.ToString().Trim());
                    total_netpay = total_netpay + double.Parse(txtb_total_loans.Text.ToString().Trim());
                    break;

                // Template Code for Honorarium - Gross pay - Tax Hidden
                case "604":
                case "704":
                case "804":
                    total_netpay = double.Parse(txtb_gross_pay.Text.ToString()) - double.Parse(txtb_birtax_summary.Text.ToString());
                    break;

                // Template Code for Terminal Leave - Gross Pay - Deductions
                case "603":
                case "703":
                case "803":
                    total_netpay = double.Parse(txtb_gross_pay.Text.ToString()) - (double.Parse(txtb_total_mandatory.Text) + double.Parse(txtb_total_optional.Text) + double.Parse(txtb_total_loans.Text) + double.Parse(refund_sal_amt.Text) + double.Parse(txtb_lates_amount.Text));
                    break;

                // Template Code for Other Salaries - ( Gross Pay + PERA (amount1) ) - (Deductions + LWOP Amount)
                case "605":
                case "705":
                case "805":
                    total_netpay = double.Parse(txtb_gross_pay.Text.ToString()) + double.Parse(txtb_other_amount1.Text.ToString());
                    total_netpay = total_netpay - (double.Parse(txtb_total_mandatory.Text) + double.Parse(txtb_total_optional.Text) + double.Parse(txtb_total_loans.Text) + double.Parse(txtb_lwo_pay.Text.ToString().Trim()) + double.Parse(txtb_lwop_amount_pera.Text.ToString().Trim()) + double.Parse(txtb_lates_amount.Text.ToString().Trim()) + double.Parse(refund_sal_amt.Text));
                    break;

                // Template Code for Refund to Employeer - MidYear Bonus (Amount1 = Net Pay)
                case "606":
                case "706":
                case "806":
                    total_netpay = double.Parse(txtb_other_amount1.Text.ToString());
                    break;

                // Template Code for Refund to Employeer - YearEnd Bonus (Amount1 + Amount2 = Net Pay)
                case "607":
                case "707":
                case "807":
                    total_netpay = double.Parse(txtb_other_amount1.Text.ToString()) + double.Parse(txtb_other_amount2.Text.ToString());
                    break;

                // Template Code for Maternity
                case "608":
                case "708":
                case "808":
                    total_netpay = double.Parse(txtb_gross_pay.Text.ToString()) + double.Parse(txtb_other_amount1.Text.ToString());
                    total_netpay = total_netpay - double.Parse(txtb_lwo_pay.Text.ToString().Trim());
                    total_netpay = total_netpay - double.Parse(txtb_lwop_amount_pera.Text.ToString().Trim());
                    total_netpay = total_netpay - (double.Parse(txtb_total_mandatory.Text) + double.Parse(txtb_total_optional.Text) + double.Parse(txtb_total_loans.Text));
                    break;

                // Template Code for Other Claims/Refund
                case "609":
                case "709":
                case "809":
                    total_netpay = double.Parse(txtb_other_amount1.Text.ToString());
                    total_netpay = total_netpay - (double.Parse(txtb_other_amount2.Text.ToString()) + double.Parse(txtb_other_amount3.Text.ToString()));
                    break;
                case "610": // Other Claims - v2
                case "611": // Other Claims - v2
                case "612": // Other Claims - v2
                    if (is_differential.Checked == true)
                    {
                        total_netpay = (double.Parse(txtb_other_amount1.Text.ToString()) + double.Parse(txtb_other_amount2.Text.ToString())) - double.Parse(txtb_other_amount3.Text.ToString());
                        total_netpay = total_netpay - (double.Parse(txtb_total_mandatory.Text) + double.Parse(txtb_total_optional.Text) + double.Parse(txtb_total_loans.Text) + double.Parse(txtb_lwo_pay.Text.ToString().Trim()) + double.Parse(txtb_lwop_amount_pera.Text.ToString().Trim()) + double.Parse(txtb_lates_amount.Text.ToString().Trim()) + double.Parse(refund_sal_amt.Text));
                    }
                    else
                    {
                        total_netpay = double.Parse(txtb_gross_pay.Text.ToString()) + double.Parse(txtb_other_amount1.Text.ToString());
                        total_netpay = total_netpay - (double.Parse(txtb_total_mandatory.Text) + double.Parse(txtb_total_optional.Text) + double.Parse(txtb_total_loans.Text) + double.Parse(txtb_lwo_pay.Text.ToString().Trim()) + double.Parse(txtb_lwop_amount_pera.Text.ToString().Trim()) + double.Parse(txtb_lates_amount.Text.ToString().Trim()) + double.Parse(refund_sal_amt.Text));
                    }
                    break;
            }

            total_str_netpay  = total_netpay.ToString("###,##0.0000");
            txtb_net_pay.Text = total_str_netpay.Split('.')[0] + "." + total_str_netpay.Split('.')[1].Substring(0, 2);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Calculate Mandatory Amount
        //*************************************************************************
        private void calculate_mandatory()
        {
            double total_mandatory = 0;

            // This Calculate Mandatory If Tempalate Type is For Terminal Leave Deductions : VJA - 06/13/2019
            if (ddl_payroll_template.SelectedValue == "603" ||
                ddl_payroll_template.SelectedValue == "703" ||
                ddl_payroll_template.SelectedValue == "803" ||
                ddl_payroll_template.SelectedValue == "608" || // Maternity 
                ddl_payroll_template.SelectedValue == "708" || // Maternity 
                ddl_payroll_template.SelectedValue == "808" || // Maternity 
                ddl_payroll_template.SelectedValue == "610" || // Other Claims - v2
                ddl_payroll_template.SelectedValue == "611" || // Other Claims - v2
                ddl_payroll_template.SelectedValue == "612"    // Other Claims - v2
                )    
                
            {
                total_mandatory = total_mandatory + double.Parse(txtb_gsis_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_hdmf_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_phic_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_bir_tax.Text.ToString());
            }
            else if ((ddl_payroll_template.SelectedValue == "605" ||    // Other Salaries
                      ddl_payroll_template.SelectedValue == "705" ||    // Other Salaries
                      ddl_payroll_template.SelectedValue == "805"       // Other Salaries

                      
                      ) &&  
                      Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "1")            // First Claim (Promotion)
            {
                total_mandatory = total_mandatory + double.Parse(txtb_gsis_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_hdmf_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_phic_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_bir_tax.Text.ToString());
            }
            else if ((ddl_payroll_template.SelectedValue == "605" ||    // Other Salaries
                      ddl_payroll_template.SelectedValue == "705" ||    // Other Salaries
                      ddl_payroll_template.SelectedValue == "805"      // Other Salaries
                      ) &&   
                      Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "2" ||
                      Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "3")            // Other Salaries - Sal. Diff (Multiple Months)
            {
                total_mandatory = total_mandatory + double.Parse(txtb_gsis_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_hdmf_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_phic_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_bir_tax.Text.ToString());
            }
            else
            {
                total_mandatory = total_mandatory + double.Parse(txtb_gsis_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_gsis_gs.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_gsis_sif.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_hdmf_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_hdmf_gs.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_phic_ps.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_phic_gs.Text.ToString());
                total_mandatory = total_mandatory + double.Parse(txtb_bir_tax.Text.ToString());
            }
            // Add Field Again - 2022-05-30
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand2.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand3.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand4.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand5.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand6.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand7.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand8.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand9.Text.ToString());
            total_mandatory = total_mandatory + double.Parse(txtb_other_ded_mand10.Text.ToString());

            txtb_total_mandatory.Text = total_mandatory.ToString("###,##0.00");
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Calculate Optional Amount
        //*************************************************************************
        private void calculate_optional()
        {
            double total_optional = 0;
            total_optional = total_optional + double.Parse(txtb_sss.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_hdmf_addl.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_philam.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ehp.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_hip.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_ceap.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_gsis_add.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_hdmf_mp2.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_hdmf_loyalty_card.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no1.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no2.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no3.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no4.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_otherpremium_no5.Text.ToString());


            // Add Field Again - 2022-05-30
            total_optional = total_optional + double.Parse(txtb_other_ded_prem1.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem2.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem3.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem4.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem5.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem6.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem7.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem8.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem9.Text.ToString());
            total_optional = total_optional + double.Parse(txtb_other_ded_prem10.Text.ToString());

            txtb_total_optional.Text = total_optional.ToString("###,##0.00");
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Calculate Loan Amount
        //*************************************************************************
        private void calculate_loans()
        {
            double total_loans = 0;
            total_loans = total_loans + double.Parse(txtb_gsis_consolidated.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_policy_regular.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_policy_optional.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_ouli_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_emer_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_ecard_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_educ_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_real_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_sos_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_hdmf_mpl_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_hdmf_house_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_hdmf_cal_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_ccmpc_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_nico_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_networkbank_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_nhmfc_hsng.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_nafc.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_help.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_gsis_housing_loan.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no1.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no2.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no3.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no4.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_otherloan_no5.Text.ToString());

            // Add Field Again - 2022-05-30
            total_loans = total_loans + double.Parse(txtb_other_ded_loan1.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan2.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan3.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan4.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan5.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan6.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan7.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan8.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan9.Text.ToString());
            total_loans = total_loans + double.Parse(txtb_other_ded_loan10.Text.ToString());

            txtb_total_loans.Text = total_loans.ToString("###,##0.00");
        }
        
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            txtb_period_from.Enabled                = ifenable;
            txtb_period_to.Enabled                  = ifenable;
            lbl_rate_basis_hidden.Enabled           = ifenable;
            lbl_monthly_rate_hidden.Enabled         = ifenable;
            lbl_daily_rate_hidden.Enabled           = ifenable;
            lbl_hourly_rate_hidden.Enabled          = ifenable;
            txtb_other_amount1.Enabled              = ifenable;
            txtb_other_amount2.Enabled              = ifenable;
            txtb_other_amount3.Enabled              = ifenable;
            txtb_bir_tax.Enabled                    = ifenable;
            txtb_birtax_summary.Enabled             = ifenable;
            txtb_lwo_pay.Enabled                    = ifenable;
            txtb_lwop_amount_pera.Enabled           = ifenable;
            txtb_gsis_gs.Enabled                    = ifenable;
            txtb_gsis_ps.Enabled                    = ifenable;
            txtb_gsis_sif.Enabled                   = ifenable;
            txtb_hdmf_gs.Enabled                    = ifenable;
            txtb_hdmf_ps.Enabled                    = ifenable;
            txtb_phic_gs.Enabled                    = ifenable;
            txtb_phic_ps.Enabled                    = ifenable;
            txtb_nhmfc_hsng.Enabled                 = ifenable;
            txtb_nafc.Enabled                       = ifenable;
            txtb_sss.Enabled                        = ifenable;
            txtb_hdmf_addl.Enabled                  = ifenable;
            txtb_hdmf_mp2.Enabled                   = ifenable;
            txtb_philam.Enabled                     = ifenable;
            txtb_gsis_ehp.Enabled                   = ifenable;
            txtb_gsis_hip.Enabled                   = ifenable;
            txtb_gsis_ceap.Enabled                  = ifenable;
            txtb_gsis_add.Enabled                   = ifenable;
            txtb_gsis_consolidated.Enabled          = ifenable;
            txtb_gsis_policy_regular.Enabled        = ifenable;
            txtb_gsis_policy_optional.Enabled       = ifenable;
            txtb_gsis_ouli_loan.Enabled             = ifenable;
            txtb_gsis_emer_loan.Enabled             = ifenable;
            txtb_gsis_ecard_loan.Enabled            = ifenable;
            txtb_gsis_educ_loan.Enabled             = ifenable;
            txtb_gsis_real_loan.Enabled             = ifenable;
            txtb_gsis_sos_loan.Enabled              = ifenable;
            txtb_gsis_help.Enabled                  = ifenable;
            txtb_gsis_housing_loan.Enabled          = ifenable;
            txtb_hdmf_mpl_loan.Enabled              = ifenable;
            txtb_hdmf_house_loan.Enabled            = ifenable;
            txtb_hdmf_cal_loan.Enabled              = ifenable;
            txtb_hdmf_loyalty_card.Enabled          = ifenable;
            txtb_nico_loan.Enabled                  = ifenable;
            txtb_networkbank_loan.Enabled           = ifenable;
            txtb_ccmpc_loan.Enabled                 = ifenable;
            txtb_otherloan_no1.Enabled              = ifenable;
            txtb_otherloan_no2.Enabled              = ifenable;
            txtb_otherloan_no3.Enabled              = ifenable;
            txtb_otherloan_no4.Enabled              = ifenable;
            txtb_otherloan_no5.Enabled              = ifenable;
            txtb_otherpremium_no1.Enabled           = ifenable;
            txtb_otherpremium_no2.Enabled           = ifenable;
            txtb_otherpremium_no3.Enabled           = ifenable;
            txtb_otherpremium_no4.Enabled           = ifenable;
            txtb_otherpremium_no5.Enabled           = ifenable;
            lbl_mone_contant_hidden.Enabled              = ifenable;
            lbl_installation_monthly_conv_hidden.Enabled = ifenable;
            txtb_no_of_days.Enabled                 = ifenable;
            txtb_lates_amount.Enabled               = ifenable;
            refund_sal_amt.Enabled                  = ifenable;
            txtb_other_amount1.Enabled              = ifenable;
            txtb_lates_min.Enabled                  = ifenable;
            txtb_other_ded_mand1.Enabled            = ifenable;
            txtb_other_ded_mand2.Enabled            = ifenable;
            txtb_other_ded_mand3.Enabled            = ifenable;
            txtb_other_ded_mand4.Enabled            = ifenable;
            txtb_other_ded_mand5.Enabled            = ifenable;
            txtb_other_ded_mand6.Enabled            = ifenable;
            txtb_other_ded_mand7.Enabled            = ifenable;
            txtb_other_ded_mand8.Enabled            = ifenable;
            txtb_other_ded_mand9.Enabled            = ifenable;
            txtb_other_ded_mand10.Enabled           = ifenable;
            txtb_other_ded_prem1.Enabled            = ifenable;
            txtb_other_ded_prem2.Enabled            = ifenable;
            txtb_other_ded_prem3.Enabled            = ifenable;
            txtb_other_ded_prem4.Enabled            = ifenable;
            txtb_other_ded_prem5.Enabled            = ifenable;
            txtb_other_ded_prem6.Enabled            = ifenable;
            txtb_other_ded_prem7.Enabled            = ifenable;
            txtb_other_ded_prem8.Enabled            = ifenable;
            txtb_other_ded_prem9.Enabled            = ifenable;
            txtb_other_ded_prem10.Enabled           = ifenable;
            txtb_other_ded_loan1.Enabled            = ifenable;
            txtb_other_ded_loan2.Enabled            = ifenable;
            txtb_other_ded_loan3.Enabled            = ifenable;
            txtb_other_ded_loan4.Enabled            = ifenable;
            txtb_other_ded_loan5.Enabled            = ifenable;
            txtb_other_ded_loan6.Enabled            = ifenable;
            txtb_other_ded_loan7.Enabled            = ifenable;
            txtb_other_ded_loan8.Enabled            = ifenable;
            txtb_other_ded_loan9.Enabled            = ifenable;
            txtb_other_ded_loan10.Enabled           = ifenable;
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void calculate_maternity()
        {
            // Computation - Casual
            // Daily Rate * Number of Days (Textbox)

            // Computation - Regular
            // Monthly Rate / Last Day of the Month * Number of days every Month

            // 1.) Get The Number of Months
            // 2.) Get the Last Day of the month each Number of months
            // 3.) Count the number of days Every Month

            double total_gross = 0;

            if (IsDataValidated())
            {
                if ((ddl_empl_type.SelectedValue         == "RE"  &&
                     ddl_payroll_template.SelectedValue  == "608")  // Regular - Maternity 
                     || 
                     ((ddl_empl_type.SelectedValue       == "RE"  &&
                     ddl_payroll_template.SelectedValue  == "605" &&
                     Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "3")) // Regular - Other Salaries (Multiple Months)
                    ) 
                {
                    DateTime date_from = Convert.ToDateTime(txtb_period_from.Text);
                    DateTime date_to = Convert.ToDateTime(txtb_period_to.Text);
                    int first_day = date_from.Day;

                    int count_month = (date_to.Month - date_from.Month + 1);
                    if (date_to.Year != date_from.Year && date_from.Year < date_to.Year)
                    {
                        int year_result = (date_to.Year - date_from.Year) * 12;
                        count_month = count_month + year_result;
                    }

                    DateTime first_month = DateTime.Parse(date_from.Year + "-" + date_from.Month + "-01");
                    int lastDayOfMonth = 0;
                    int year = date_from.Year;

                    for (int x = 0; x < count_month; x++)
                    {
                        lastDayOfMonth = DateTime.DaysInMonth(year, first_month.Month);

                        if (first_month == DateTime.Parse(date_from.Year + "-" + date_from.Month + "-01") && (first_month != DateTime.Parse(date_to.Year + "-" + date_to.Month + "-01")))
                        {
                            total_gross += (Convert.ToDouble(txtb_rate_amount.Text) / lastDayOfMonth) * ((lastDayOfMonth - date_from.Day) + 1);
                        }
                        else if (first_month == DateTime.Parse(date_from.Year + "-" + date_from.Month + "-01") && (first_month == DateTime.Parse(date_to.Year + "-" + date_to.Month + "-01")))
                        {
                            total_gross += (Convert.ToDouble(txtb_rate_amount.Text) / lastDayOfMonth) * ((date_to.Day - date_from.Day) + 1);
                        }
                        else if (first_month == DateTime.Parse(date_to.Year + "-" + date_to.Month + "-01"))
                        {
                            total_gross += (Convert.ToDouble(txtb_rate_amount.Text) / lastDayOfMonth) * date_to.Day;
                        }
                        else
                        {
                            total_gross += (Convert.ToDouble(txtb_rate_amount.Text) / lastDayOfMonth) * lastDayOfMonth;
                        }

                        if (first_month.AddMonths(1).Year != year)
                        {
                            year = first_month.AddMonths(1).Year;
                        }
                        first_month = first_month.AddMonths(1);
                    }

                    txtb_gross_pay.Text = total_gross.ToString("###,##0.00");
                }

                else if ((ddl_payroll_template.SelectedValue == "708" || // Casual    - Maternity
                         ddl_payroll_template.SelectedValue == "808")    // Job-Order - Maternity

                         ||

                         (ddl_payroll_template.SelectedValue   == "605"
                         || ddl_payroll_template.SelectedValue == "705"
                         || ddl_payroll_template.SelectedValue == "805"
                         && Session["PreviousValuesonPage_cVoucherHdr_voucher_type"].ToString() == "3"
                         )
                         )   
                {
                    total_gross = double.Parse(txtb_no_of_days.Text) * double.Parse(txtb_rate_amount.Text);
                    txtb_gross_pay.Text = total_gross.ToString("###,##0.00");
                }

            }

            //total_gross = total_gross  + double.Parse(txtb_other_amount1.Text);
        }
        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        protected void ddl_voucher_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToogleModal();
        }
        //********************************************************************************
        //  BEGIN - VJA- 2020-07-08 - Calculate Hidden Monthly, Daily and Hourly Rate
        //********************************************************************************
        private void Calculate_MDH()
        {
            double monthly      = double.Parse(lbl_monthly_rate_hidden.Text);
            double daily        = double.Parse(lbl_daily_rate_hidden.Text);
            double hourly       = double.Parse(lbl_hourly_rate_hidden.Text);
            double rate_basis   = double.Parse(txtb_rate_amount.Text);
            string str_monthly  = "";
            string str_daily    = "";
            string str_hourly   = "";


            switch (lbl_rate_basis_hidden.Text.ToString().Trim())
            {
                case "M":
                    {
                        monthly = rate_basis;
                        daily   = rate_basis / 22;
                        hourly  = rate_basis / 22 / 8;
                        break;
                    }
                case "D":
                    {
                        monthly = rate_basis * 22; 
                        daily   = rate_basis ;
                        hourly  = rate_basis / 8;
                        break;
                    }
                case "H":
                    {
                        monthly = rate_basis * 8 * 22;
                        daily   = rate_basis * 8;
                        hourly  = rate_basis;
                        break;
                    }
            }
            str_monthly   = monthly.ToString("###,##0.0000");
            str_daily     = daily.ToString("###,##0.0000");
            str_hourly    = hourly.ToString("###,##.0000");
            
            lbl_monthly_rate_hidden.Text = str_monthly.Split('.')[0] + "." + str_monthly.Split('.')[1].Substring(0, 2);
            lbl_daily_rate_hidden.Text   = str_daily.Split('.')[0] + "." + str_daily.Split('.')[1].Substring(0, 2);
            lbl_hourly_rate_hidden.Text  = str_hourly.Split('.')[0] + "." + str_hourly.Split('.')[1].Substring(0, 2);
        }
        //********************************************************************************
        //  BEGIN - VJA- 2020-07-08 - Retrieve Sequence Number 
        //********************************************************************************
        private void RetrieveSeqNbr()
        {
            int dtRowCont = dataListGrid.Rows.Count - 1;
            string lastCode = "00";

            if (dtRowCont > -1)
            {
                DataRow lastRow = dataListGrid.Rows[dtRowCont];
                lastCode = lastRow["seq_no"].ToString();
                if (lastCode == "")
                {
                    lastCode = "0";
                }
            }
            int lastCodeInt = int.Parse(lastCode) + 1;
            string nextCode = lastCodeInt.ToString();
            nextCode = nextCode.PadLeft(2, '0');

            hidden_seq_no.Text = nextCode.Trim();
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Calculate Deduction Amount
        //*************************************************************************
        private double calculate_deduc_amt()
        {
            double total_deduc_amt = 0;
            total_deduc_amt = total_deduc_amt + double.Parse(txtb_total_mandatory.Text.ToString());
            total_deduc_amt = total_deduc_amt + double.Parse(txtb_total_optional.Text.ToString());
            total_deduc_amt = total_deduc_amt + double.Parse(txtb_total_loans.Text.ToString());

            if (ddl_payroll_template.SelectedValue == "609" ||
                ddl_payroll_template.SelectedValue == "709" ||
                ddl_payroll_template.SelectedValue == "809" 
                )
            {
                total_deduc_amt = total_deduc_amt + (double.Parse(txtb_other_amount2.Text.ToString()) + double.Parse(txtb_other_amount3.Text.ToString()));
            }

            return total_deduc_amt;
        }

        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - The Description of Label Of Loan and Optional Deduction Toogle
        //**************************************************************************
        private void RetrieveLoanPremiums_Visible()
        {
            string payrolltemplate = "";

            if (ddl_empl_type.SelectedValue == "RE")
            {
                payrolltemplate = "007";
            }
            else if (ddl_empl_type.SelectedValue == "CE")
            {
                payrolltemplate = "008";
            }
            else 
            {
                payrolltemplate = "009";
            }

            DataTable dt = MyCmn.RetrieveData("sp_payrollmapping_others_flag_list", "par_payrolltemplate_code", payrolltemplate.Trim());

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    //OTHER PREMIUM
                    lbl_other_prem1.Text            = dt.Rows[0]["descr_other_premium1"].ToString() == "" ? lbl_other_prem1.Text : dt.Rows[0]["descr_other_premium1"].ToString() + ":";
                    txtb_otherpremium_no1.Enabled   = dt.Rows[0]["other_premium1"].ToString()       == "Y" ? true : false;
                                                      
                    lbl_other_prem2.Text            = dt.Rows[0]["descr_other_premium2"].ToString() == "" ? lbl_other_prem2.Text : dt.Rows[0]["descr_other_premium2"].ToString() + ":";
                    txtb_otherpremium_no2.Enabled   = dt.Rows[0]["other_premium2"].ToString()       == "Y" ? true : false;
                                                      
                    lbl_other_prem3.Text            = dt.Rows[0]["descr_other_premium3"].ToString() == "" ? lbl_other_prem3.Text : dt.Rows[0]["descr_other_premium3"].ToString() + ":";
                    txtb_otherpremium_no3.Enabled   = dt.Rows[0]["other_premium3"].ToString()       == "Y" ? true : false;
                                                      
                    lbl_other_prem4.Text            = dt.Rows[0]["descr_other_premium4"].ToString() == "" ? lbl_other_prem4.Text : dt.Rows[0]["descr_other_premium4"].ToString() + ":";
                    txtb_otherpremium_no4.Enabled   = dt.Rows[0]["other_premium4"].ToString()       == "Y" ? true : false;
                                                      
                    lbl_other_prem5.Text            = dt.Rows[0]["descr_other_premium5"].ToString() == "" ? lbl_other_prem5.Text : dt.Rows[0]["descr_other_premium5"].ToString() + ":";
                    txtb_otherpremium_no5.Enabled   = dt.Rows[0]["other_premium5"].ToString()       == "Y" ? true : false;
                                                      
                    //OTHER LOAN                      
                    lbl_loan1.Text                  = dt.Rows[0]["descr_other_loan1"].ToString()    == "" ? lbl_loan1.Text : dt.Rows[0]["descr_other_loan1"].ToString() + ":";
                    txtb_otherloan_no1.Enabled      = dt.Rows[0]["other_loan1"].ToString()          == "Y" ? true : false;
                                                  
                    lbl_loan2.Text                  = dt.Rows[0]["descr_other_loan2"].ToString()    == "" ? lbl_loan2.Text : dt.Rows[0]["descr_other_loan2"].ToString() + ":";
                    txtb_otherloan_no2.Enabled      = dt.Rows[0]["other_loan2"].ToString()          == "Y" ? true : false;
                                                  
                    lbl_loan3.Text                  = dt.Rows[0]["descr_other_loan3"].ToString()    == "" ? lbl_loan3.Text : dt.Rows[0]["descr_other_loan3"].ToString() + ":";
                    txtb_otherloan_no3.Enabled      = dt.Rows[0]["other_loan3"].ToString()          == "Y" ? true : false;
                                                  
                    lbl_loan4.Text                  = dt.Rows[0]["descr_other_loan4"].ToString()    == "" ? lbl_loan4.Text : dt.Rows[0]["descr_other_loan4"].ToString() + ":";
                    txtb_otherloan_no4.Enabled      = dt.Rows[0]["other_loan4"].ToString()          == "Y" ? true : false;
                                                  
                    lbl_loan5.Text                  = dt.Rows[0]["descr_other_loan5"].ToString()    == "" ? lbl_loan5.Text : dt.Rows[0]["descr_other_loan5"].ToString() + ":";
                    txtb_otherloan_no5.Enabled      = dt.Rows[0]["other_loan5"].ToString()          == "Y" ? true : false;
                }
            }
        }

        private void calculate_oth_claims_refund()
        {
            // ddl_payroll_type_1
            // 01 - Add to Gross
            // 02 - Deduct to Gross
            // 03 - Refund

            double total_claims = 0;
            double total_refund = 0;
            double total_deduc_to_gross = 0;
            double total_gross_pay = 0;
            
            if (ddl_payroll_type_1.SelectedValue == "01")
            {
                total_claims += double.Parse(txtb_payroll_amt1.Text);
            }
            else if (ddl_payroll_type_1.SelectedValue == "02")
            {
                total_deduc_to_gross += double.Parse(txtb_payroll_amt1.Text);
            }
            else if (ddl_payroll_type_1.SelectedValue == "03")
            {
                total_refund += double.Parse(txtb_payroll_amt1.Text);
            }
            
            if (ddl_payroll_type_2.SelectedValue == "01")
            {
                total_claims += double.Parse(txtb_payroll_amt2.Text);
            }
            else if (ddl_payroll_type_2.SelectedValue == "02")
            {
                total_deduc_to_gross += double.Parse(txtb_payroll_amt2.Text);
            }
            else if (ddl_payroll_type_2.SelectedValue == "03")
            {
                total_refund += double.Parse(txtb_payroll_amt2.Text);
            }

            if (ddl_payroll_type_3.SelectedValue == "01")
            {
                total_claims += double.Parse(txtb_payroll_amt3.Text);
            }
            else if (ddl_payroll_type_3.SelectedValue == "02")
            {
                total_deduc_to_gross += double.Parse(txtb_payroll_amt3.Text);
            }
            else if (ddl_payroll_type_3.SelectedValue == "03")
            {
                total_refund += double.Parse(txtb_payroll_amt3.Text);
            }

            if (ddl_payroll_type_4.SelectedValue == "01")
            {
                total_claims += double.Parse(txtb_payroll_amt4.Text);
            }
            else if (ddl_payroll_type_4.SelectedValue == "02")
            {
                total_deduc_to_gross += double.Parse(txtb_payroll_amt4.Text);
            }
            else if (ddl_payroll_type_4.SelectedValue == "03")
            {
                total_refund += double.Parse(txtb_payroll_amt4.Text);
            }

            if (ddl_payroll_type_5.SelectedValue == "01")
            {
                total_claims += double.Parse(txtb_payroll_amt5.Text);
            }
            else if (ddl_payroll_type_5.SelectedValue == "02")
            {
                total_deduc_to_gross += double.Parse(txtb_payroll_amt5.Text);
            }
            else if (ddl_payroll_type_5.SelectedValue == "03")
            {
                total_refund += double.Parse(txtb_payroll_amt5.Text);
            }

            txtb_other_amount1.Text = total_claims.ToString("###,##0.00");
            txtb_other_amount2.Text = total_deduc_to_gross.ToString("###,##0.00");
            txtb_other_amount3.Text = total_refund.ToString("###,##0.00");
            total_gross_pay         = total_claims - (total_deduc_to_gross + total_refund);
            txtb_gross_pay.Text     = total_gross_pay.ToString("###,##0.00");

        }

        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Initialized datasource fields/columns - Other Claims Refund
        //*************************************************************************
        private void InitializeTable_oth_claims()
        {
            dtSource_oth_claims = new DataTable();
            dtSource_oth_claims.Columns.Add("voucher_ctrl_nbr", typeof(System.String));
            dtSource_oth_claims.Columns.Add("seq_no", typeof(System.String));
            dtSource_oth_claims.Columns.Add("empl_id", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_descr_amt1", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_descr_amt2", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_descr_amt3", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_descr_amt4", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_descr_amt5", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_type_amt1", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_type_amt2", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_type_amt3", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_type_amt4", typeof(System.String));
            dtSource_oth_claims.Columns.Add("payroll_type_amt5", typeof(System.String));
            dtSource_oth_claims.Columns.Add("other_amt1", typeof(System.String));
            dtSource_oth_claims.Columns.Add("other_amt2", typeof(System.String));
            dtSource_oth_claims.Columns.Add("other_amt3", typeof(System.String));
            dtSource_oth_claims.Columns.Add("other_amt4", typeof(System.String));
            dtSource_oth_claims.Columns.Add("other_amt5", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Add Primary Key Field to datasource - Other Claims Refund
        //*************************************************************************
        private void AddPrimaryKeys_oth_claims()
        {
            dtSource_oth_claims.TableName = "voucher_dtl_oth_claims_tbl";
            dtSource_oth_claims.Columns.Add("action", typeof(System.Int32));
            dtSource_oth_claims.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "voucher_ctrl_nbr", "seq_no", "empl_id" };
            dtSource_oth_claims = MyCmn.AddPrimaryKeys(dtSource_oth_claims, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 06/06/2019 - Add new row to datatable object - Other Claims Refund
        //*************************************************************************
        private void AddNewRow_oth_claims()
        {
            DataRow nrow = dtSource_oth_claims.NewRow();
            nrow["voucher_ctrl_nbr"] = string.Empty;
            nrow["seq_no"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["payroll_descr_amt1"] = string.Empty;
            nrow["payroll_descr_amt2"] = string.Empty;
            nrow["payroll_descr_amt3"] = string.Empty;
            nrow["payroll_descr_amt4"] = string.Empty;
            nrow["payroll_descr_amt5"] = string.Empty;
            nrow["payroll_type_amt1"] = string.Empty;
            nrow["payroll_type_amt2"] = string.Empty;
            nrow["payroll_type_amt3"] = string.Empty;
            nrow["payroll_type_amt4"] = string.Empty;
            nrow["payroll_type_amt5"] = string.Empty;
            nrow["other_amt1"] = string.Empty;
            nrow["other_amt2"] = string.Empty;
            nrow["other_amt3"] = string.Empty;
            nrow["other_amt4"] = string.Empty;
            nrow["other_amt5"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource_oth_claims.Rows.Add(nrow);
        }
        
        //*****************************************************************
        //  BEGIN - VJA- 2022-05-30 - Reserved Deductions Description
        //*****************************************************************
        private void RetrieveReserveDeduction()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_othded_setup_tbl_list", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    // MANDATORY DEDUCTION
                    lbl_other_ded_mand1.Text        = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand1_descr"].ToString() + ":";
                    lbl_other_ded_mand2.Text        = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand2_descr"].ToString() + ":";
                    lbl_other_ded_mand3.Text        = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand3_descr"].ToString() + ":";
                    lbl_other_ded_mand4.Text        = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand4_descr"].ToString() + ":";
                    lbl_other_ded_mand5.Text        = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand5_descr"].ToString() + ":";
                    lbl_other_ded_mand6.Text        = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand6_descr"].ToString() + ":";
                    lbl_other_ded_mand7.Text        = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand7_descr"].ToString() + ":";
                    lbl_other_ded_mand8.Text        = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand8_descr"].ToString() + ":";
                    lbl_other_ded_mand9.Text        = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand9_descr"].ToString() + ":";
                    lbl_other_ded_mand10.Text       = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_mand10_descr"].ToString() + ":";
                    
                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Text        = dt.Rows[0]["other_ded_prem1_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem1_descr"].ToString() + ":";
                    lbl_other_ded_prem2.Text        = dt.Rows[0]["other_ded_prem2_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem2_descr"].ToString() + ":";
                    lbl_other_ded_prem3.Text        = dt.Rows[0]["other_ded_prem3_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem3_descr"].ToString() + ":";
                    lbl_other_ded_prem4.Text        = dt.Rows[0]["other_ded_prem4_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem4_descr"].ToString() + ":";
                    lbl_other_ded_prem5.Text        = dt.Rows[0]["other_ded_prem5_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem5_descr"].ToString() + ":";
                    lbl_other_ded_prem6.Text        = dt.Rows[0]["other_ded_prem6_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem6_descr"].ToString() + ":";
                    lbl_other_ded_prem7.Text        = dt.Rows[0]["other_ded_prem7_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem7_descr"].ToString() + ":";
                    lbl_other_ded_prem8.Text        = dt.Rows[0]["other_ded_prem8_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem8_descr"].ToString() + ":";
                    lbl_other_ded_prem9.Text        = dt.Rows[0]["other_ded_prem9_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem9_descr"].ToString() + ":";
                    lbl_other_ded_prem10.Text       = dt.Rows[0]["other_ded_prem10_descr"].ToString()  == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_prem10_descr"].ToString() + ":";
                    
                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Text        = dt.Rows[0]["other_ded_loan1_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan1_descr"].ToString() + ":";
                    lbl_other_ded_loan2.Text        = dt.Rows[0]["other_ded_loan2_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan2_descr"].ToString() + ":";
                    lbl_other_ded_loan3.Text        = dt.Rows[0]["other_ded_loan3_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan3_descr"].ToString() + ":";
                    lbl_other_ded_loan4.Text        = dt.Rows[0]["other_ded_loan4_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan4_descr"].ToString() + ":";
                    lbl_other_ded_loan5.Text        = dt.Rows[0]["other_ded_loan5_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan5_descr"].ToString() + ":";
                    lbl_other_ded_loan6.Text        = dt.Rows[0]["other_ded_loan6_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan6_descr"].ToString() + ":";
                    lbl_other_ded_loan7.Text        = dt.Rows[0]["other_ded_loan7_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan7_descr"].ToString() + ":";
                    lbl_other_ded_loan8.Text        = dt.Rows[0]["other_ded_loan8_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan8_descr"].ToString() + ":";
                    lbl_other_ded_loan9.Text        = dt.Rows[0]["other_ded_loan9_descr"].ToString()   == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan9_descr"].ToString() + ":";
                    lbl_other_ded_loan10.Text       = dt.Rows[0]["other_ded_loan10_descr"].ToString()  == "" ? lbl_other_prem1.Text : dt.Rows[0]["other_ded_loan10_descr"].ToString() + ":";
                    
                    // ***********************************************************************************************************
                    // ***** DO NOT DISPLAY THE TEXTBOXES IF NO DESCRIPTION ******************************************************
                    // ***********************************************************************************************************

                    // MANDATORY DEDUCTION
                    lbl_other_ded_mand1.Visible        = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand2.Visible        = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand3.Visible        = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand4.Visible        = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand5.Visible        = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand6.Visible        = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand7.Visible        = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand8.Visible        = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand9.Visible        = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand10.Visible       = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? false : true;

                    txtb_other_ded_mand1.Visible       = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand2.Visible       = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand3.Visible       = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand4.Visible       = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand5.Visible       = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand6.Visible       = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand7.Visible       = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand8.Visible       = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand9.Visible       = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand10.Visible      = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? false : true;

                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Visible        = dt.Rows[0]["other_ded_prem1_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem2.Visible        = dt.Rows[0]["other_ded_prem2_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem3.Visible        = dt.Rows[0]["other_ded_prem3_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem4.Visible        = dt.Rows[0]["other_ded_prem4_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem5.Visible        = dt.Rows[0]["other_ded_prem5_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem6.Visible        = dt.Rows[0]["other_ded_prem6_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem7.Visible        = dt.Rows[0]["other_ded_prem7_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem8.Visible        = dt.Rows[0]["other_ded_prem8_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem9.Visible        = dt.Rows[0]["other_ded_prem9_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem10.Visible        = dt.Rows[0]["other_ded_prem10_descr"].ToString()  == "" ? false : true;

                    txtb_other_ded_prem1.Visible     = dt.Rows[0]["other_ded_prem1_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem2.Visible     = dt.Rows[0]["other_ded_prem2_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem3.Visible     = dt.Rows[0]["other_ded_prem3_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem4.Visible     = dt.Rows[0]["other_ded_prem4_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem5.Visible     = dt.Rows[0]["other_ded_prem5_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem6.Visible     = dt.Rows[0]["other_ded_prem6_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem7.Visible     = dt.Rows[0]["other_ded_prem7_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem8.Visible     = dt.Rows[0]["other_ded_prem8_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem9.Visible     = dt.Rows[0]["other_ded_prem9_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem10.Visible    = dt.Rows[0]["other_ded_prem10_descr"].ToString()      == "" ? false : true;

                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Visible        = dt.Rows[0]["other_ded_loan1_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan2.Visible        = dt.Rows[0]["other_ded_loan2_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan3.Visible        = dt.Rows[0]["other_ded_loan3_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan4.Visible        = dt.Rows[0]["other_ded_loan4_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan5.Visible        = dt.Rows[0]["other_ded_loan5_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan6.Visible        = dt.Rows[0]["other_ded_loan6_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan7.Visible        = dt.Rows[0]["other_ded_loan7_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan8.Visible        = dt.Rows[0]["other_ded_loan8_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan9.Visible        = dt.Rows[0]["other_ded_loan9_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan10.Visible       = dt.Rows[0]["other_ded_loan10_descr"].ToString()    == "" ? false : true;

                    txtb_other_ded_loan1.Visible     = dt.Rows[0]["other_ded_loan1_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan2.Visible     = dt.Rows[0]["other_ded_loan2_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan3.Visible     = dt.Rows[0]["other_ded_loan3_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan4.Visible     = dt.Rows[0]["other_ded_loan4_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan5.Visible     = dt.Rows[0]["other_ded_loan5_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan6.Visible     = dt.Rows[0]["other_ded_loan6_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan7.Visible     = dt.Rows[0]["other_ded_loan7_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan8.Visible     = dt.Rows[0]["other_ded_loan8_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan9.Visible     = dt.Rows[0]["other_ded_loan9_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan10.Visible    = dt.Rows[0]["other_ded_loan10_descr"].ToString()      == "" ? false : true;
                }
            }
        }
        //************************************************************************************************
        //  BEGIN - VJA- 2022-05-30 - Check, Insert and Update Additional Table for Reserved Dedutions
        //************************************************************************************************
        private void InsertUpdateOtherDeduction()
        {

            DataTable dt = MyCmn.RetrieveData("payrollregistry_dtl_othded_chk", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString(), "par_payroll_year", ddl_year.SelectedValue.ToString() , "par_payroll_month", ddl_month.SelectedValue.ToString(), "par_payroll_registry_nbr", voucher_nbr.Text.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString(), "par_seq_no", hidden_seq_no.Text.ToString().Trim());
            if (dt != null)
            {
                string insert_update_script = "";

                // UPDATE DATA FROM OTHER DEDUCTIONS
                if (dt.Rows[0]["flag_return"].ToString() == "U")
                {
                    insert_update_script = "update payrollregistry_dtl_othded_tbl set "
                                            + "other_ded_mand1 ="
                                            + txtb_other_ded_mand1.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand2 ="
                                            + txtb_other_ded_mand2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand3 ="
                                            + txtb_other_ded_mand3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand4 ="
                                            + txtb_other_ded_mand4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand5 ="
                                            + txtb_other_ded_mand5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand6 ="
                                            + txtb_other_ded_mand6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand7 ="
                                            + txtb_other_ded_mand7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand8 ="
                                            + txtb_other_ded_mand8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand9 ="
                                            + txtb_other_ded_mand9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand10 ="
                                            + txtb_other_ded_mand10.Text.ToString().Trim().Replace(",", "")

                                            + ",other_ded_prem1 ="
                                            + txtb_other_ded_prem1.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem2 ="
                                            + txtb_other_ded_prem2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem3 ="
                                            + txtb_other_ded_prem3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem4 ="
                                            + txtb_other_ded_prem4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem5 ="
                                            + txtb_other_ded_prem5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem6="
                                            + txtb_other_ded_prem6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem7 ="
                                            + txtb_other_ded_prem7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem8 ="
                                            + txtb_other_ded_prem8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem9 ="
                                            + txtb_other_ded_prem9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem10 ="
                                            + txtb_other_ded_prem10.Text.ToString().Trim().Replace(",", "")

                                            + ",other_ded_loan1 ="
                                            + txtb_other_ded_loan1.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan2 ="
                                            + txtb_other_ded_loan2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan3 ="
                                            + txtb_other_ded_loan3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan4 ="
                                            + txtb_other_ded_loan4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan5 ="
                                            + txtb_other_ded_loan5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan6 ="
                                            + txtb_other_ded_loan6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan7 ="
                                            + txtb_other_ded_loan7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan8 ="
                                            + txtb_other_ded_loan8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan9 ="
                                            + txtb_other_ded_loan9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan10 ="
                                            + txtb_other_ded_loan10.Text.ToString().Trim().Replace(",", "")

                                            + "where "
                                            +         "payrolltemplate_code = '" + ddl_payroll_template.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_year= '"          + ddl_year.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_month= '"         + ddl_month.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_registry_nbr= '"  + voucher_nbr.Text.ToString().Trim() + "'"
                                            + "AND " + "empl_id= '"               + txtb_empl_id.Text + "'"
                                            + "AND " + "seq_no= '"                + hidden_seq_no.Text.ToString().Trim() + "'";

                    MyCmn.Update_InsertTable(insert_update_script);
                }
                // INSERT DATA TO OTHER DEDUCTIONS
                else if (dt.Rows[0]["flag_return"].ToString() == "I")
                {
                    insert_update_script = "insert into payrollregistry_dtl_othded_tbl select "
                                         +       "'" + ddl_payroll_template.SelectedValue.ToString() + "'"
                                         + "," + "'" + ddl_year.SelectedValue.ToString()             + "'"
                                         + "," + "'" + ddl_month.SelectedValue.ToString()            + "'"
                                         + "," + "'" + voucher_nbr.Text.ToString().Trim()    + "'"
                                         + "," + "'" + txtb_empl_id.Text                             + "'"
                                         + "," + txtb_other_ded_mand1.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand2.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand3.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand4.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand5.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand6.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand7.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand8.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_mand9.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_mand10.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_prem1.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem2.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem3.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem4.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem5.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem6.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem7.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem8.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_prem9.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_prem10.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_loan1.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan2.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan3.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan4.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan5.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan6.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan7.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan8.Text.ToString().Trim().Replace(",","")
                                         + "," + txtb_other_ded_loan9.Text.ToString().Trim().Replace(",", "")
                                         + "," + txtb_other_ded_loan10.Text.ToString().Trim().Replace(",", "")
                                         + "," + "'" + hidden_seq_no.Text.ToString().Trim() + "'";

                    MyCmn.Update_InsertTable(insert_update_script);
                }
                else
                {
                }
            }
         }


        //********************************************************************************
        //  BEGIN - VJA- 2020-07-08 - Calculate Lates Amount
        //********************************************************************************
        private void Calculate_LatesAmount()
        {
            double lates_time = 0;
            double lates_amount = 0;
            lates_time = double.Parse((txtb_lates_min.Text.ToString().Trim() != "" ? txtb_lates_min.Text.ToString().Trim() : "0"));
            lates_time = lates_time / 60;

            if (lates_time.ToString().Split('.').Length > 1)
            {
                lates_time = double.Parse(lates_time.ToString().Split('.')[0].Trim() + "." + (double.Parse("." + lates_time.ToString().Split('.')[1].Trim()) / 60).ToString().Split('.')[1]);
            }

            switch (lbl_rate_basis_hidden.Text.ToString())
            {
                case "M":
                    {
                        lates_amount = double.Parse(txtb_rate_amount.Text.ToString()) / 22 / 8 / 60 * double.Parse(txtb_lates_min.Text.ToString());
                        txtb_lates_amount.Text = lates_amount.ToString("###,##0.00");
                        break;
                    }
                case "D":
                    {
                        lates_amount = double.Parse(txtb_rate_amount.Text.ToString()) / 8 / 60 * double.Parse(txtb_lates_min.Text.ToString());
                        txtb_lates_amount.Text = lates_amount.ToString("###,##0.00");
                        break;
                    }
                case "H":
                    {
                        lates_amount = double.Parse(txtb_rate_amount.Text.ToString()) / 8 / 60 * double.Parse(txtb_lates_min.Text.ToString());
                        txtb_lates_amount.Text = lates_amount.ToString("###,##0.00");
                        break;
                    }
            }


        }

        protected void is_differential_CheckedChanged(object sender, EventArgs e)
        {
            ToogleModal();
        }

        //********************************************************************
        // END OF THE CODE
        //********************************************************************
    }
}