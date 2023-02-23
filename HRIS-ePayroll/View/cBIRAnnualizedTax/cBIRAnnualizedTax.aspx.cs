//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Other Contribution and Loans
//**********************************************************************************
// REVISION HISTORYbtnAdd_Click
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
//JORGE RUSTOM VILLANUEVA    04/03/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View
{
    public partial class cBIRAnnualizedTax : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Data Place holder creation 
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


        DataTable dataListGrid_dtl
        {
            get
            {
                if ((DataTable)ViewState["dataListGrid_dtl"] == null) return null;
                return (DataTable)ViewState["dataListGrid_dtl"];
            }
            set
            {
                ViewState["dataListGrid_dtl"] = value;
            }
        }

        DataTable dtSource_for_update_delete
        {
            get
            {
                if ((DataTable)ViewState["dtSource_for_update_delete"] == null) return null;
                return (DataTable)ViewState["dtSource_for_update_delete"];
            }
            set
            {
                ViewState["dtSource_for_update_delete"] = value;
            }
        }


        DataTable dataListGrid_dtl_update_delete
        {
            get
            {
                if ((DataTable)ViewState["dataListGrid_dtl_update_delete"] == null) return null;
                return (DataTable)ViewState["dataListGrid_dtl_update_delete"];
            }
            set
            {
                ViewState["dataListGrid_dtl_update_delete"] = value;
            }
        }

        DataTable dataListEmployee
        {
            get
            {
                if ((DataTable)ViewState["dataListEmployee"] == null) return null;
                return (DataTable)ViewState["dataListEmployee"];
            }
            set
            {
                ViewState["dataListEmployee"] = value;
            }
        }

        //********************************************************************
        //  BEGIN - JVA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();
        int target_tab = 1;
        decimal salary = 0;
        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    InitializePage();
                    Session["SortField"] = "empl_id";
                    Session["SortOrder"] = "ASC";

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
            }
        }

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            RetrieveYear();
            RetrieveEmploymentType();
            RetrieveBindingDepartments();
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
           

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cBIRAnnualizedTax"] = "cBIRAnnualizedTax";

            RetrieveDataListGrid();
           
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Payroll Year
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
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {

            dataListGrid = MyCmn.RetrieveData("sp_annualtax_hdr_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString().Trim(), "par_division_code", ddl_division.SelectedValue.ToString().Trim(), "par_section_code", ddl_section.SelectedValue.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }

        private void RetrieveDataListGrid_dtl()
        {

            dataListGrid_dtl = MyCmn.RetrieveData("sp_annualtax_dtl_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            CommonCode.GridViewBind(ref this.GridView1, dataListGrid_dtl);
            UpdatePanel3.Update();
            
        }

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

        private void RetrieveEmpl()
        {
            ddl_empl_id.Items.Clear();
            dataListEmployee = MyCmn.RetrieveData("sp_personnelnames_combolist_tax", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString().Trim(), "par_division_code", ddl_division.SelectedValue.ToString().Trim(), "par_section_code", ddl_section.SelectedValue.ToString().Trim());

            ddl_empl_id.DataSource = dataListEmployee;
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }

        private string get_empl_details()
        {
            string id_ref = "";
            DataRow[] selected_employee = dataListEmployee.Select("empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

            if (selected_employee.Length > 0)
            {
                id_ref = selected_employee[0]["account_id_nbr_ref"].ToString();
            }

            return id_ref;
        }

        private void RetrieveBindingDepartments()
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
            DataTable dt = MyCmn.RetrieveData("sp_divisions_tbl_combolist", "par_department_code", ddl_department.SelectedValue.ToString(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString());

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
            DataTable dt1 = MyCmn.RetrieveData("sp_sections_tbl_combolist", "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString(), "par_division_code", ddl_division.SelectedValue.ToString().Trim());

            ddl_section.DataSource = dt1;
            ddl_section.DataValueField = "section_code";
            ddl_section.DataTextField = "section_name1";
            ddl_section.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_section.Items.Insert(0, li);
        }

        

          

       

       


        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            //For Header
            RetrieveEmpl();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            UpdatePanel102.Update();
            
            GridView1.Visible = false;
            UpdatePanel3.Update();
            txtb_search1.Visible = false;
            UpdatePanel102.Update();

            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();

            InitializeTable_dtl_delete_update();
            AddPrimaryKeys_delete_update();
            AddNewRow_delete_update();
            
            ddl_empl_id.Visible = true;
            txtb_empl_id.Visible = true;
            

            
            tbx_cola.Enabled = true;
            tbx_FHA.Enabled = true;
            tbx_oth_a.Enabled = true;
            tbx_oth_b.Enabled = true;
            tbx_com.Enabled = true;
            tbx_fid.Enabled = true;
            tbx_13_oth.Enabled = true;
            tbx_prft_s.Enabled = true;
            tbx_hon.Enabled = true;
            tbx_ls_ttl_expt.Enabled = true;
            add_tbx_Txbl_com_prev.Enabled = true;
            tbx_prst_empl.Enabled = false;
            tbx_prev_empl.Enabled = true;
            tbx_per.Enabled = false;


            txtb_empl_name.Visible = false;
            tbx_hzrd.Enabled = false;
            tbx_ot.Enabled = false;
            tbx_sub.Enabled = false;
            tbx_13th_oth_non.Enabled = false;
            tbx_de_min.Enabled = false;
            tbx_contri.Enabled = false;
            tbx_RA.Enabled = false;
            tbx_TA.Enabled = false;

            tbx_slrs_oth_comp.Enabled = false;
            tbx_gross_comp.Enabled = false;
            tbx_Txbl_inc.Enabled = false;
            tbx_net_txbl.Enabled = false;
            tbx_ttl_non_tx.Enabled = false;
            tbx_tax_due.Enabled = false;
            tbx_ttl_adjstd.Enabled = false;

            tbx_jan_due_act.Enabled = true;
            tbx_feb_due_act.Enabled = true;
            tbx_mar_due_act.Enabled = true;
            tbx_apr_due_act.Enabled = true;
            tbx_may_due_act.Enabled = true;
            tbx_jun_due_act1.Enabled = true;
            tbx_jul_due_act.Enabled = true;
            tbx_aug_due_act.Enabled = true;
            tbx_sep_due_act.Enabled = true;
            tbx_oct_due_act.Enabled = true;
            tbx_nov_due_act.Enabled = true;
            tbx_dec_due_act.Enabled = true;

            tbx_jan_due_prj.Enabled = true;
            tbx_feb_due_prj.Enabled = true;
            tbx_mar_due_prj.Enabled = true;
            tbx_apr_due_prj.Enabled = true;
            tbx_may_due_prj.Enabled = true;
            tbx_jun_due_prj.Enabled = true;
            tbx_jul_due_prj.Enabled = true;
            tbx_aug_due_prj.Enabled = true;
            tbx_sep_due_prj.Enabled = true;
            tbx_oct_due_prj.Enabled = true;
            tbx_nov_due_prj.Enabled = true;
            tbx_dec_due_prj.Enabled = true;

            tbx_ttl_amt_due.Enabled = false;


            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            tbx_total_non.Text  = "0.00";
            tbx_total.Text      = "0.00";
            txtb_empl_id.Text   = "0.00";
            tbx_sal.Text        = "0.00";
            txtb_empl_name.Text = "";
            tbx_per.Text        = "0.00";
            tbx_cola.Text       = "0.00";
            tbx_FHA.Text        = "0.00";
            tbx_oth_a.Text      = "0.00";
            tbx_oth_b.Text      = "0.00";
            tbx_prft_s.Text     = "0.00";
            tbx_13_oth.Text     = "0.00";
            tbx_fid.Text        = "0.00";
            tbx_hzrd.Text       = "0.00";
            tbx_ot.Text         = "0.00";
            tbx_com.Text        = "0.00";
            tbx_hon.Text        = "0.00";
            tbx_sub.Text        = "0.00";

            tbx_13th_oth_non.Text = "0.00";
            tbx_de_min.Text     = "0.00";
            tbx_contri.Text     = "0.00";
            tbx_RA.Text         = "0.00";
            tbx_TA.Text         = "0.00";
            tbx_slrs_oth_comp.Text = "0.00";

            tbx_gross_comp.Text = "0.00";
            tbx_net_txbl.Text   = "0.00";
            tbx_ttl_non_tx.Text = "0.00";
            tbx_tax_due.Text    = "0.00";
            tbx_Txbl_inc.Text   = "0.00";
            tbx_prst_empl.Text  = "0.00";
            tbx_gross_txbl.Text = "0.00";
            tbx_prev_empl.Text  = "0.00";
            tbx_ls_ttl_expt.Text = "0.00";
            tbx_ls_prm_hlth.Text = "0.00";
            tbx_ttl_adjstd.Text = "0.00";
            add_tbx_Txbl_com_prev.Text = "0.00";

            tbx_jan_due_act.Text = "0.00";
            tbx_feb_due_act.Text = "0.00";
            tbx_mar_due_act.Text = "0.00";
            tbx_apr_due_act.Text = "0.00";
            tbx_may_due_act.Text = "0.00";
            tbx_jun_due_act1.Text = "0.00";
            tbx_jul_due_act.Text = "0.00";
            tbx_aug_due_act.Text = "0.00";
            tbx_sep_due_act.Text = "0.00";
            tbx_oct_due_act.Text = "0.00";
            tbx_nov_due_act.Text = "0.00";
            tbx_dec_due_act.Text = "0.00";

            tbx_jan_due_prj.Text = "0.00";
            tbx_feb_due_prj.Text = "0.00";
            tbx_mar_due_prj.Text = "0.00";
            tbx_apr_due_prj.Text = "0.00";
            tbx_may_due_prj.Text = "0.00";
            tbx_jun_due_prj.Text = "0.00";
            tbx_jul_due_prj.Text = "0.00";
            tbx_aug_due_prj.Text = "0.00";
            tbx_sep_due_prj.Text = "0.00";
            tbx_oct_due_prj.Text = "0.00";
            tbx_nov_due_prj.Text = "0.00";
            tbx_dec_due_prj.Text = "0.00";
            tbx_ttl_amt_due.Text = "0.00";


            tbx_per.Enabled = false;
          
            tbx_cola.Enabled = true;
            tbx_FHA.Enabled = true;
            tbx_oth_a.Enabled = true;
            tbx_oth_b.Enabled = true;
            tbx_prft_s.Enabled = true;
            tbx_13_oth.Enabled = true;
            tbx_fid.Enabled = true;
            tbx_hzrd.Enabled = true;
            tbx_ot.Enabled = true;
            tbx_com.Enabled = true;
            tbx_hon.Enabled = true;
            tbx_sub.Enabled = true;

            tbx_13th_oth_non.Enabled = true;
            tbx_de_min.Enabled = true;
            tbx_contri.Enabled = true;
            tbx_RA.Enabled = true;
            tbx_TA.Enabled = true;
            tbx_slrs_oth_comp.Enabled = true;

            tbx_gross_comp.Enabled = true;
            tbx_net_txbl.Enabled = true;
            tbx_ttl_non_tx.Enabled = true;
            tbx_tax_due.Enabled = true;
            tbx_Txbl_inc.Enabled = true;
            tbx_prst_empl.Enabled = true;
            tbx_prev_empl.Enabled = true;
            tbx_ls_ttl_expt.Enabled = true;
            tbx_ls_prm_hlth.Enabled = true;
            tbx_ttl_adjstd.Enabled = true;
            add_tbx_Txbl_com_prev.Enabled = true;

            ddl_empl_id.SelectedIndex = -1;
            //UpdatePanel2.Update();
            //UpdatePanel24.Update();
            //UpdatePanel15.Update();
            //UpdatePanel21.Update();
            //UpdatePanel26.Update();
            //UpdatePanel28.Update();
            //UpdatePanel25.Update();
            //UpdatePanel53.Update();
            //UpdatePanel60.Update();
            //UpdatePanel54.Update();
            //UpdatePanel55.Update();

        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("ntx_13th_14th_37", typeof(System.String));
            dtSource.Columns.Add("ntx_de_minimis_38", typeof(System.String));
            dtSource.Columns.Add("ntx_gsis_phic_hdmf_39", typeof(System.String));
            dtSource.Columns.Add("ntx_salaries_oth_40", typeof(System.String));
            dtSource.Columns.Add("basicsalary_42", typeof(System.String));
            dtSource.Columns.Add("representation_43", typeof(System.String));
            dtSource.Columns.Add("transportation_44", typeof(System.String));
            dtSource.Columns.Add("cola_45", typeof(System.String));
            dtSource.Columns.Add("fh_allowance_46", typeof(System.String));
            dtSource.Columns.Add("others_47a", typeof(System.String));
            dtSource.Columns.Add("others_47b", typeof(System.String));
            dtSource.Columns.Add("commision_48", typeof(System.String));
            dtSource.Columns.Add("prof_sharing_49", typeof(System.String));
            dtSource.Columns.Add("fi_drctr_fees_50", typeof(System.String));
            dtSource.Columns.Add("txbl_13th_14th_51", typeof(System.String));
            dtSource.Columns.Add("hazard_pay_52", typeof(System.String));
            dtSource.Columns.Add("ot_pay_53", typeof(System.String));
            dtSource.Columns.Add("others_54a", typeof(System.String));
            dtSource.Columns.Add("others_54b", typeof(System.String));
            dtSource.Columns.Add("txbl_comm_inc_oth_24", typeof(System.String));
            dtSource.Columns.Add("total_exemptions_26", typeof(System.String));
            dtSource.Columns.Add("prm_paid_health_27", typeof(System.String));
            dtSource.Columns.Add("annual_tax_due_29", typeof(System.String));
            dtSource.Columns.Add("total_tax_due_pres_30a", typeof(System.String));
            dtSource.Columns.Add("total_tax_due_prev_30b", typeof(System.String));
            dtSource.Columns.Add("monthly_tax_due_sal_99", typeof(System.String));
            dtSource.Columns.Add("monthly_tax_due_hp_99", typeof(System.String));
            dtSource.Columns.Add("monthly_tax_rate_hp_ot", typeof(System.String));

        }

        private void InitializeTable_dtl()
        {
            dtSource_dtl = new DataTable();
            dtSource_dtl.Columns.Add("payroll_year", typeof(System.String));
            dtSource_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl.Columns.Add("account_code", typeof(System.String));
            dtSource_dtl.Columns.Add("account_sub_code", typeof(System.String));
            dtSource_dtl.Columns.Add("month_01_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_02_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_03_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_04_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_05_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_06_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_07_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_08_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_09_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_10_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_11_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_12_act", typeof(System.String));
            dtSource_dtl.Columns.Add("month_01_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_02_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_03_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_04_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_05_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_06_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_07_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_08_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_09_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_10_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_11_prj", typeof(System.String));
            dtSource_dtl.Columns.Add("month_12_prj", typeof(System.String));


        }

        private void InitializeTable_dtl_delete_update()
        {

            dtSource_for_update_delete = new DataTable();
            dtSource_for_update_delete.Columns.Add("payroll_year", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("empl_id", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("account_code", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("account_sub_code", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_01_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_02_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_03_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_04_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_05_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_06_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_07_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_08_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_09_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_10_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_11_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_12_act", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_01_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_02_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_03_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_04_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_05_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_06_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_07_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_08_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_09_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_10_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_11_prj", typeof(System.String));
            dtSource_for_update_delete.Columns.Add("month_12_prj", typeof(System.String));
        }



            //*************************************************************************
            //  BEGIN - JVA- 09/20/2018 - Add Primary Key Field to datasource
            //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "annualtax_hdr_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "empl_id" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        private void AddPrimaryKeys_dtl()
        {
            dtSource_dtl.TableName = "annualtax_dtl_tbl";
            dtSource_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "empl_id", "account_code", "account_sub_code" };
            dtSource_dtl = MyCmn.AddPrimaryKeys(dtSource_dtl, col);

        }

        private void AddPrimaryKeys_delete_update()
        {
            dtSource_for_update_delete.TableName = "annualtax_dtl_tbl";
            dtSource_for_update_delete.Columns.Add("action", typeof(System.Int32));
            dtSource_for_update_delete.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col1 = new string[] { "payroll_year", "empl_id", "account_code", "account_sub_code" };
            dtSource_for_update_delete = MyCmn.AddPrimaryKeys(dtSource_for_update_delete, col1);
         }


        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();

            nrow["payroll_year"] = string.Empty;
            nrow["empl_id"] = string.Empty;

            nrow["ntx_13th_14th_37"] = string.Empty;
            nrow["ntx_de_minimis_38"] = string.Empty;
            nrow["ntx_gsis_phic_hdmf_39"] = string.Empty;
            nrow["ntx_salaries_oth_40"] = string.Empty;
            nrow["basicsalary_42"] = string.Empty;
            nrow["representation_43"] = string.Empty;
            nrow["transportation_44"] = string.Empty;
            nrow["cola_45"] = string.Empty;
            nrow["fh_allowance_46"] = string.Empty;
            nrow["others_47a"] = string.Empty;
            nrow["others_47b"] = string.Empty;
            nrow["commision_48"] = string.Empty;
            nrow["prof_sharing_49"] = string.Empty;
            nrow["fi_drctr_fees_50"] = string.Empty;
            nrow["txbl_13th_14th_51"] = string.Empty;
            nrow["hazard_pay_52"] = string.Empty;
            nrow["ot_pay_53"] = string.Empty;
            nrow["others_54a"] = string.Empty;
            nrow["others_54b"] = string.Empty;
            nrow["txbl_comm_inc_oth_24"] = string.Empty;
            nrow["total_exemptions_26"] = string.Empty;
            nrow["prm_paid_health_27"] = string.Empty;
            nrow["annual_tax_due_29"] = string.Empty;
            nrow["total_tax_due_pres_30a"] = string.Empty;
            nrow["total_tax_due_prev_30b"] = string.Empty;
            nrow["monthly_tax_due_sal_99"] = string.Empty;
            nrow["monthly_tax_due_hp_99"] = string.Empty;
            nrow["monthly_tax_rate_hp_ot"] = string.Empty;


            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }

        private void AddNewRow_dtl()
        {
            DataRow nrow = dtSource_dtl.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["account_code"] = string.Empty;
            nrow["account_sub_code"] = string.Empty;
            nrow["month_01_act"] = string.Empty;
            nrow["month_02_act"] = string.Empty;
            nrow["month_03_act"] = string.Empty;
            nrow["month_04_act"] = string.Empty;
            nrow["month_05_act"] = string.Empty;
            nrow["month_06_act"] = string.Empty;
            nrow["month_07_act"] = string.Empty;
            nrow["month_08_act"] = string.Empty;
            nrow["month_09_act"] = string.Empty;
            nrow["month_10_act"] = string.Empty;
            nrow["month_11_act"] = string.Empty;
            nrow["month_12_act"] = string.Empty;
            nrow["month_01_prj"] = string.Empty;
            nrow["month_02_prj"] = string.Empty;
            nrow["month_03_prj"] = string.Empty;
            nrow["month_04_prj"] = string.Empty;
            nrow["month_05_prj"] = string.Empty;
            nrow["month_06_prj"] = string.Empty;
            nrow["month_07_prj"] = string.Empty;
            nrow["month_08_prj"] = string.Empty;
            nrow["month_09_prj"] = string.Empty;
            nrow["month_10_prj"] = string.Empty;
            nrow["month_11_prj"] = string.Empty;
            nrow["month_12_prj"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource_dtl.Rows.Add(nrow);

        }

        private void AddNewRow_delete_update()
        {

            DataRow nrow1 = dtSource_for_update_delete.NewRow();
            nrow1["payroll_year"] = string.Empty;
            nrow1["empl_id"] = string.Empty;
            nrow1["account_code"] = string.Empty;
            nrow1["account_sub_code"] = string.Empty;
            nrow1["month_01_act"] = string.Empty;
            nrow1["month_02_act"] = string.Empty;
            nrow1["month_03_act"] = string.Empty;
            nrow1["month_04_act"] = string.Empty;
            nrow1["month_05_act"] = string.Empty;
            nrow1["month_06_act"] = string.Empty;
            nrow1["month_07_act"] = string.Empty;
            nrow1["month_08_act"] = string.Empty;
            nrow1["month_09_act"] = string.Empty;
            nrow1["month_10_act"] = string.Empty;
            nrow1["month_11_act"] = string.Empty;
            nrow1["month_12_act"] = string.Empty;
            nrow1["month_01_prj"] = string.Empty;
            nrow1["month_02_prj"] = string.Empty;
            nrow1["month_03_prj"] = string.Empty;
            nrow1["month_04_prj"] = string.Empty;
            nrow1["month_05_prj"] = string.Empty;
            nrow1["month_06_prj"] = string.Empty;
            nrow1["month_07_prj"] = string.Empty;
            nrow1["month_08_prj"] = string.Empty;
            nrow1["month_09_prj"] = string.Empty;
            nrow1["month_10_prj"] = string.Empty;
            nrow1["month_11_prj"] = string.Empty;
            nrow1["month_12_prj"] = string.Empty;

            nrow1["action"] = 1;
            nrow1["retrieve"] = false;
            dtSource_for_update_delete.Rows.Add(nrow1);

        }


            //***************************************************************************
            //  BEGIN - JVA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
            //***************************************************************************
            protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString().Trim();
            string seq = commandArgs.ToString().Trim();

            deleteRec1.Text = "Are you sure to delete this Account Code = (" + seq.Trim().Split(',')[0] + ") ?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string deleteExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND payroll_year = '" + svalues[1].ToString().Trim() + "'";
            MyCmn.DeleteBackEndData("annualtax_hdr_tbl", "WHERE " + deleteExpression);
            MyCmn.DeleteBackEndData("annualtax_dtl_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        

         protected void editRow1_Command(object sender, CommandEventArgs e)
         {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND account_code = '" + svalues[1].ToString().Trim() + "' AND account_sub_code = '" + svalues[2].ToString().Trim() + "' AND payroll_year = '" + svalues[3].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid_dtl.Select(editExpression);
            btn_update.CommandArgument = e.CommandArgument.ToString();

            tbx_acc_title.Text       = row2Edit[0]["account_title"].ToString();
            tbx_acc_code.Text        = row2Edit[0]["account_code"].ToString();
            tbx_acc_sub_code.Text    = row2Edit[0]["account_sub_code"].ToString();
            tbx_acct_class.Text      = row2Edit[0]["acctclass_descr"].ToString();
            acct_class_code.Text     = row2Edit[0]["acctclass_code"].ToString();
            tbx_acct_ttl.Text        = row2Edit[0]["acct_total_amount"].ToString();
            txtb_jan_amount_act.Text = row2Edit[0]["month_01_act"].ToString();
            txtb_feb_amount_act.Text = row2Edit[0]["month_02_act"].ToString();
            txtb_mar_amount_act.Text = row2Edit[0]["month_03_act"].ToString();
            txtb_apr_amount_act.Text = row2Edit[0]["month_04_act"].ToString();
            txtb_may_amount_act.Text = row2Edit[0]["month_05_act"].ToString();
            txtb_jun_amount_act.Text = row2Edit[0]["month_06_act"].ToString();
            txtb_jul_amount_act.Text = row2Edit[0]["month_07_act"].ToString();
            txtb_aug_amount_act.Text = row2Edit[0]["month_08_act"].ToString();
            txtb_sep_amount_act.Text = row2Edit[0]["month_09_act"].ToString();
            txtb_oct_amount_act.Text = row2Edit[0]["month_10_act"].ToString();
            txtb_nov_amount_act.Text = row2Edit[0]["month_11_act"].ToString();
            txtb_dec_amount_act.Text = row2Edit[0]["month_12_act"].ToString();
            txtb_jan_amount_prj.Text = row2Edit[0]["month_01_prj"].ToString();
            txtb_feb_amount_prj.Text = row2Edit[0]["month_02_prj"].ToString();
            txtb_mar_amount_prj.Text = row2Edit[0]["month_03_prj"].ToString();
            txtb_apr_amount_prj.Text = row2Edit[0]["month_04_prj"].ToString();
            txtb_may_amount_prj.Text = row2Edit[0]["month_05_prj"].ToString();
            txtb_jun_amount_prj.Text = row2Edit[0]["month_06_prj"].ToString();
            txtb_jul_amount_prj.Text = row2Edit[0]["month_07_prj"].ToString();
            txtb_aug_amount_prj.Text = row2Edit[0]["month_08_prj"].ToString();
            txtb_sep_amount_prj.Text = row2Edit[0]["month_09_prj"].ToString();
            txtb_oct_amount_prj.Text = row2Edit[0]["month_10_prj"].ToString();
            txtb_nov_amount_prj.Text = row2Edit[0]["month_11_prj"].ToString();
            txtb_dec_amount_prj.Text = row2Edit[0]["month_12_prj"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDetails", "openEditDetail();", true);

         }


            //**************************************************************************
            //  BEGIN - JVA- 09/20/2018 - Edit Row selection that will trigger edit page 
            //**************************************************************************
         protected void editRow_Command(object sender, CommandEventArgs e)
         {

              //RetrieveDataListGrid();

              string[] svalues = e.CommandArgument.ToString().Split(',');
              string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND payroll_year = '" + svalues[1].ToString().Trim() + "'";
              DataRow[] row2Edit = dataListGrid.Select(editExpression);
              ClearEntry();

              GridView1.Visible = true;
              UpdatePanel3.Update();
            
              txtb_search1.Visible = true;
              UpdatePanel102.Update();
              //TAXABLE TAB
              tbx_sal.Enabled = false;
              tbx_cola.Enabled = true;
              tbx_FHA.Enabled = true;
              tbx_oth_a.Enabled = true;
              tbx_oth_b.Enabled = true;
              tbx_prft_s.Enabled = true;
              tbx_fid.Enabled = true;
              tbx_com.Enabled = true;
              tbx_13_oth.Enabled = false;
              tbx_hzrd.Enabled = false;
              tbx_ot.Enabled = false;
              tbx_sub.Enabled = false;

              //NON TAXABLE TAB
              tbx_13th_oth_non.Enabled = false;
              tbx_de_min.Enabled = false;
              tbx_contri.Enabled = false;
              tbx_RA.Enabled = false;
              tbx_TA.Enabled = false;
              tbx_slrs_oth_comp.Enabled = false;

              //SUMMARY TAB
              tbx_gross_comp.Enabled = false;
              tbx_net_txbl.Enabled = false;
              tbx_ttl_non_tx.Enabled = false;
              tbx_tax_due.Enabled = false;
              tbx_Txbl_inc.Enabled = false;
              tbx_prst_empl.Enabled = false;


            //tbx_prev_empl.Enabled = true;
            //tbx_ls_ttl_expt.Enabled = true;
            //tbx_ls_prm_hlth.Enabled = true;
            //add_tbx_Txbl_com_prev.Enabled = true;
            //tbx_per.Enabled = true;
            //tbx_hon.Enabled = false;

              tbx_per.Enabled = false;
              tbx_gross_txbl.Enabled = false;
              tbx_ttl_adjstd.Enabled = false;

              //TAX DUE TAB
              tbx_jan_due_act.Enabled = false;
              tbx_feb_due_act.Enabled = false;
              tbx_mar_due_act.Enabled = false;
              tbx_apr_due_act.Enabled = false;
              tbx_may_due_act.Enabled = false;
              tbx_jun_due_act1.Enabled = false;
              tbx_jul_due_act.Enabled = false;
              tbx_aug_due_act.Enabled = false;
              tbx_sep_due_act.Enabled = false;
              tbx_oct_due_act.Enabled = false;
              tbx_nov_due_act.Enabled = false;
              tbx_dec_due_act.Enabled = false;

              tbx_jan_due_prj.Enabled = true;
              tbx_feb_due_prj.Enabled = true;
              tbx_mar_due_prj.Enabled = true;
              tbx_apr_due_prj.Enabled = true;
              tbx_may_due_prj.Enabled = true;
              tbx_jun_due_prj.Enabled = true;
              tbx_jul_due_prj.Enabled = true;
              tbx_aug_due_prj.Enabled = true;
              tbx_sep_due_prj.Enabled = true;
              tbx_oct_due_prj.Enabled = true;
              tbx_nov_due_prj.Enabled = true;
              tbx_dec_due_prj.Enabled = true;
              tbx_ttl_amt_due.Enabled = false;

              InitializeTable();
              AddPrimaryKeys();
              AddNewRow();

              InitializeTable_dtl();
              AddPrimaryKeys_dtl();
              AddNewRow_dtl();

              InitializeTable_dtl_delete_update();
              AddPrimaryKeys_delete_update();
              AddNewRow_delete_update();

              dtSource.Rows[0]["action"] = 2;
              dtSource.Rows[0]["retrieve"] = true;
              dtSource_dtl.Rows[0]["action"] = 2;
              dtSource_dtl.Rows[0]["retrieve"] = true;

              ddl_empl_id.Visible = false;
              txtb_empl_name.Visible = true;


            

              tbx_per.Text            = Convert.ToDecimal(row2Edit[0]["monthly_tax_rate_hp_ot"]).ToString("#,##0.00");
              txtb_empl_name.Text     = row2Edit[0]["employee_name"].ToString().Trim().ToString(); 
              txtb_empl_id.Text       = row2Edit[0]["empl_id"].ToString().Trim().ToString();
              tbx_sal.Text            = Convert.ToDecimal(row2Edit[0]["basicsalary_42"]).ToString("#,##0.00");
              tbx_cola.Text           = Convert.ToDecimal(row2Edit[0]["cola_45"]).ToString("#,##0.00");
              tbx_FHA.Text            = Convert.ToDecimal(row2Edit[0]["fh_allowance_46"]).ToString("#,##0.00");
              tbx_oth_a.Text          = Convert.ToDecimal(row2Edit[0]["others_47a"]).ToString("#,##0.00");
              tbx_oth_b.Text          = Convert.ToDecimal(row2Edit[0]["others_47b"]).ToString("#,##0.00");
              tbx_prft_s.Text         = Convert.ToDecimal(row2Edit[0]["prof_sharing_49"]).ToString("#,##0.00");
              tbx_13_oth.Text         = Convert.ToDecimal(row2Edit[0]["txbl_13th_14th_51"]).ToString("#,##0.00");
              tbx_fid.Text            = Convert.ToDecimal(row2Edit[0]["fi_drctr_fees_50"]).ToString("#,##0.00");
              tbx_hzrd.Text           = Convert.ToDecimal(row2Edit[0]["hazard_pay_52"]).ToString("#,##0.00");
              tbx_ot.Text             = Convert.ToDecimal(row2Edit[0]["ot_pay_53"]).ToString("#,##0.00");
              tbx_com.Text            = Convert.ToDecimal(row2Edit[0]["commision_48"]).ToString("#,##0.00");
              tbx_hon.Text            = Convert.ToDecimal(row2Edit[0]["others_54a"]).ToString("#,##0.00");
              tbx_sub.Text            = Convert.ToDecimal(row2Edit[0]["others_54b"]).ToString("#,##0.00");
              tbx_13th_oth_non.Text   = Convert.ToDecimal(row2Edit[0]["ntx_13th_14th_37"]).ToString("#,##0.00");
              tbx_de_min.Text         = Convert.ToDecimal(row2Edit[0]["ntx_de_minimis_38"]).ToString("#,##0.00");
              tbx_contri.Text         = Convert.ToDecimal(row2Edit[0]["ntx_gsis_phic_hdmf_39"]).ToString("#,##0.00");
              tbx_RA.Text             = Convert.ToDecimal(row2Edit[0]["representation_43"]).ToString("#,##0.00");
              tbx_TA.Text             = Convert.ToDecimal(row2Edit[0]["transportation_44"]).ToString("#,##0.00");
              tbx_slrs_oth_comp.Text  = Convert.ToDecimal(row2Edit[0]["ntx_salaries_oth_40"]).ToString("#,##0.00");
              tbx_tax_due.Text        = Convert.ToDecimal(row2Edit[0]["annual_tax_due_29"]).ToString("#,##0.00");
              add_tbx_Txbl_com_prev.Text = Convert.ToDecimal(row2Edit[0]["txbl_comm_inc_oth_24"]).ToString("#,##0.00");
              tbx_prst_empl.Text      = Convert.ToDecimal(row2Edit[0]["total_tax_due_pres_30a"]).ToString("#,##0.00");
              tbx_prev_empl.Text      = Convert.ToDecimal(row2Edit[0]["total_tax_due_prev_30b"]).ToString("#,##0.00");
              tbx_ls_ttl_expt.Text    = Convert.ToDecimal(row2Edit[0]["total_exemptions_26"]).ToString("#,##0.00");
              tbx_ls_prm_hlth.Text    = Convert.ToDecimal(row2Edit[0]["prm_paid_health_27"]).ToString("#,##0.00");
            

              tbx_jan_due_act.Text = row2Edit[0]["month_01_act"].ToString().Trim();
              tbx_feb_due_act.Text = row2Edit[0]["month_02_act"].ToString().Trim();
              tbx_mar_due_act.Text = row2Edit[0]["month_03_act"].ToString().Trim();
              tbx_apr_due_act.Text = row2Edit[0]["month_04_act"].ToString().Trim();
              tbx_may_due_act.Text = row2Edit[0]["month_05_act"].ToString().Trim();
              tbx_jun_due_act1.Text =row2Edit[0]["month_06_act"].ToString().Trim();
              tbx_jul_due_act.Text = row2Edit[0]["month_07_act"].ToString().Trim();
              tbx_aug_due_act.Text = row2Edit[0]["month_08_act"].ToString().Trim();
              tbx_sep_due_act.Text = row2Edit[0]["month_09_act"].ToString().Trim();
              tbx_oct_due_act.Text = row2Edit[0]["month_10_act"].ToString().Trim();
              tbx_nov_due_act.Text = row2Edit[0]["month_11_act"].ToString().Trim();
              tbx_dec_due_act.Text = row2Edit[0]["month_12_act"].ToString().Trim();

              tbx_jan_due_prj.Text = row2Edit[0]["month_01_prj"].ToString().Trim();
              tbx_feb_due_prj.Text = row2Edit[0]["month_02_prj"].ToString().Trim();
              tbx_mar_due_prj.Text = row2Edit[0]["month_03_prj"].ToString().Trim();
              tbx_apr_due_prj.Text = row2Edit[0]["month_04_prj"].ToString().Trim();
              tbx_may_due_prj.Text = row2Edit[0]["month_05_prj"].ToString().Trim();
              tbx_jun_due_prj.Text = row2Edit[0]["month_06_prj"].ToString().Trim();
              tbx_jul_due_prj.Text = row2Edit[0]["month_07_prj"].ToString().Trim();
              tbx_aug_due_prj.Text = row2Edit[0]["month_08_prj"].ToString().Trim();
              tbx_sep_due_prj.Text = row2Edit[0]["month_09_prj"].ToString().Trim();
              tbx_oct_due_prj.Text = row2Edit[0]["month_10_prj"].ToString().Trim();
              tbx_nov_due_prj.Text = row2Edit[0]["month_11_prj"].ToString().Trim();
              tbx_dec_due_prj.Text = row2Edit[0]["month_12_prj"].ToString().Trim();
              tbx_ttl_amt_due.Text = row2Edit[0]["acct_total_amount"].ToString().Trim();

              calculate_act_prj();
              calculate_tax_due();

              RetrieveDataListGrid_dtl();
              

              LabelAddEdit.Text = "Edit Record: ";
              ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
              ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;
              FieldValidationColorChanged(false, "ALL");

              

              ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Change Field Sort mode  
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
        //  BEGIN - JVA- 09/20/2018 - Change Field Sort mode  
        //**************************************************************************
        protected void gv_dataListGrid_Sorting_dtl(object sender, GridViewSortEventArgs e)
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

            MyCmn.Sort(GridView1, dataListGrid_dtl, e.SortExpression, sortingDirection);
            show_pagesx.Text = "Page: <b>" + (GridView1.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + GridView1.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Get Grid current sort order 
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
        //  BEGIN - JVA- 09/20/2018 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");

            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;
            string scriptInsertUpdate1 = string.Empty;



            transfer_datalist();
            calculate_act_prj();
            calculate_tax_due();

            if (IsDataValidated())
            {

                if (saveRecord == MyCmn.CONST_ADD)
                {

                

                string[] insert_empl_script = MyCmn.get_insertscript(dtSource_for_update_delete).Split(';');

                    MyCmn.DeleteBackEndData(dtSource_for_update_delete.TableName.ToString(), "WHERE payroll_year ='" + ddl_year.SelectedValue.ToString().Trim() + "' AND empl_id ='" + txtb_empl_id.Text.ToString().Trim() + "' AND account_code !='20201010'");

                    for (int x = 0; x < insert_empl_script.Length; x++)
                    {

                        string insert_script = "";
                        insert_script = insert_empl_script[x];
                        MyCmn.insertdata(insert_script);

                    }

                    



                        dtSource.Rows[0]["payroll_year"]             = ddl_year.SelectedValue.ToString().Trim();
                        dtSource.Rows[0]["empl_id"]                  = ddl_empl_id.SelectedValue.ToString().Trim();
                        dtSource.Rows[0]["ntx_13th_14th_37"]         = tbx_13th_oth_non.Text.ToString();
                        dtSource.Rows[0]["ntx_de_minimis_38"]        = tbx_de_min.Text.ToString();
                        dtSource.Rows[0]["ntx_gsis_phic_hdmf_39"]    = tbx_contri.Text.ToString();
                        dtSource.Rows[0]["ntx_salaries_oth_40"]      = tbx_slrs_oth_comp.Text.ToString();
                        dtSource.Rows[0]["basicsalary_42"]           = tbx_sal.Text.ToString();
                        dtSource.Rows[0]["representation_43"]        = tbx_RA.Text.ToString();
                        dtSource.Rows[0]["transportation_44"]        = tbx_TA.Text.ToString();
                        dtSource.Rows[0]["cola_45"]                  = tbx_cola.Text.ToString();
                        dtSource.Rows[0]["fh_allowance_46"]          = tbx_FHA.Text.ToString();
                        dtSource.Rows[0]["others_47a"]               = tbx_oth_a.Text.ToString();
                        dtSource.Rows[0]["others_47b"]               = tbx_oth_b.Text.ToString();
                        dtSource.Rows[0]["commision_48"]             = tbx_com.Text.ToString();
                        dtSource.Rows[0]["prof_sharing_49"]          = tbx_prft_s.Text.ToString();
                        dtSource.Rows[0]["fi_drctr_fees_50"]         = tbx_fid.Text.ToString();
                        dtSource.Rows[0]["txbl_13th_14th_51"]        = tbx_13_oth.Text.ToString();
                        dtSource.Rows[0]["hazard_pay_52"]            = tbx_hzrd.Text.ToString();
                        dtSource.Rows[0]["ot_pay_53"]                = tbx_ot.Text.ToString();
                        dtSource.Rows[0]["others_54a"]               = tbx_hon.Text.ToString();
                        dtSource.Rows[0]["others_54b"]               = tbx_sub.Text.ToString();
                        dtSource.Rows[0]["txbl_comm_inc_oth_24"]     = add_tbx_Txbl_com_prev.Text.ToString();
                        dtSource.Rows[0]["total_exemptions_26"]      = tbx_ls_ttl_expt.Text.ToString();
                        dtSource.Rows[0]["prm_paid_health_27"]       = tbx_ls_prm_hlth.Text.ToString();
                        dtSource.Rows[0]["annual_tax_due_29"]        = tbx_tax_due.Text.ToString();
                        dtSource.Rows[0]["total_tax_due_pres_30a"]   = tbx_prst_empl.Text.ToString();
                        dtSource.Rows[0]["total_tax_due_prev_30b"]   = tbx_prev_empl.Text.ToString();
                        dtSource.Rows[0]["monthly_tax_rate_hp_ot"]   = tbx_per.Text.ToString();
                        dtSource.Rows[0]["monthly_tax_due_sal_99"]   = (Convert.ToDecimal(tbx_prst_empl.Text.ToString())/12);
                        dtSource.Rows[0]["monthly_tax_due_hp_99"]    = (Convert.ToDecimal(tbx_hzrd.Text.ToString()) * (Convert.ToDecimal(tbx_per.Text.ToString()) / 100)) / 12;

                    if (ddl_empl_type.SelectedValue.ToString().Trim() == "RE")
                    {

                        dtSource_dtl.Rows[0]["account_code"] = "20201010";
                        dtSource_dtl.Rows[0]["account_sub_code"] = "01";

                    }

                    else if (ddl_empl_type.SelectedValue.ToString().Trim() == "CE")
                    {

                        dtSource_dtl.Rows[0]["account_code"] = "20201010";
                        dtSource_dtl.Rows[0]["account_sub_code"] = "02";

                    }

                    else
                    {
                        dtSource_dtl.Rows[0]["account_code"] = "20201010";
                        dtSource_dtl.Rows[0]["account_sub_code"] = "03";
                    }

                        
                        dtSource_dtl.Rows[0]["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                        dtSource_dtl.Rows[0]["empl_id"]              = ddl_empl_id.SelectedValue.ToString().Trim();
                        dtSource_dtl.Rows[0]["month_01_act"]         = tbx_jan_due_act.Text;
                        dtSource_dtl.Rows[0]["month_02_act"]         = tbx_feb_due_act.Text;
                        dtSource_dtl.Rows[0]["month_03_act"]         = tbx_mar_due_act.Text;
                        dtSource_dtl.Rows[0]["month_04_act"]         = tbx_apr_due_act.Text;
                        dtSource_dtl.Rows[0]["month_05_act"]         = tbx_may_due_act.Text;
                        dtSource_dtl.Rows[0]["month_06_act"]         = tbx_jun_due_act1.Text;
                        dtSource_dtl.Rows[0]["month_07_act"]         = tbx_jul_due_act.Text;
                        dtSource_dtl.Rows[0]["month_08_act"]         = tbx_aug_due_act.Text;
                        dtSource_dtl.Rows[0]["month_09_act"]         = tbx_sep_due_act.Text;
                        dtSource_dtl.Rows[0]["month_10_act"]         = tbx_oct_due_act.Text;
                        dtSource_dtl.Rows[0]["month_11_act"]         = tbx_nov_due_act.Text;
                        dtSource_dtl.Rows[0]["month_12_act"]         = tbx_dec_due_act.Text;
                        dtSource_dtl.Rows[0]["month_01_prj"]         = tbx_jan_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_02_prj"]         = tbx_feb_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_03_prj"]         = tbx_mar_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_04_prj"]         = tbx_apr_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_05_prj"]         = tbx_may_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_06_prj"]         = tbx_jun_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_07_prj"]         = tbx_jul_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_08_prj"]         = tbx_aug_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_09_prj"]         = tbx_sep_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_10_prj"]         = tbx_oct_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_11_prj"]         = tbx_nov_due_prj.Text;
                        dtSource_dtl.Rows[0]["month_12_prj"]         = tbx_dec_due_prj.Text;

              
                        scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                        scriptInsertUpdate1 = MyCmn.get_insertscript(dtSource_dtl);

                }

                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    
                        string[] insert_empl_script = MyCmn.get_insertscript(dtSource_for_update_delete).Split(';');

                        MyCmn.DeleteBackEndData(dtSource_for_update_delete.TableName.ToString(), "WHERE payroll_year ='" + ddl_year.SelectedValue.ToString().Trim() + "' AND empl_id ='" + txtb_empl_id.Text.ToString().Trim() + "' AND account_code !='20201010'");

                        for (int x = 0; x < insert_empl_script.Length; x++)
                        {

                            string insert_script = "";
                            insert_script = insert_empl_script[x];
                            MyCmn.insertdata(insert_script);
                             
                        }

                    dtSource.Rows[0]["payroll_year"] = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["empl_id"] = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["ntx_13th_14th_37"] = tbx_13th_oth_non.Text.ToString();
                    dtSource.Rows[0]["ntx_de_minimis_38"] = tbx_de_min.Text.ToString();
                    dtSource.Rows[0]["ntx_gsis_phic_hdmf_39"] = tbx_contri.Text.ToString();
                    dtSource.Rows[0]["ntx_salaries_oth_40"] = tbx_slrs_oth_comp.Text.ToString();
                    dtSource.Rows[0]["basicsalary_42"] = tbx_sal.Text.ToString();
                    dtSource.Rows[0]["representation_43"] = tbx_RA.Text.ToString();
                    dtSource.Rows[0]["transportation_44"] = tbx_TA.Text.ToString();
                    dtSource.Rows[0]["cola_45"] = tbx_cola.Text.ToString();
                    dtSource.Rows[0]["fh_allowance_46"] = tbx_FHA.Text.ToString();
                    dtSource.Rows[0]["others_47a"] = tbx_oth_a.Text.ToString();
                    dtSource.Rows[0]["others_47b"] = tbx_oth_b.Text.ToString();
                    dtSource.Rows[0]["commision_48"] = tbx_com.Text.ToString();
                    dtSource.Rows[0]["prof_sharing_49"] = tbx_prft_s.Text.ToString();
                    dtSource.Rows[0]["fi_drctr_fees_50"] = tbx_fid.Text.ToString();
                    dtSource.Rows[0]["txbl_13th_14th_51"] = tbx_13_oth.Text.ToString();
                    dtSource.Rows[0]["hazard_pay_52"] = tbx_hzrd.Text.ToString();
                    dtSource.Rows[0]["ot_pay_53"] = tbx_ot.Text.ToString();
                    dtSource.Rows[0]["others_54a"] = tbx_hon.Text.ToString();
                    dtSource.Rows[0]["others_54b"] = tbx_sub.Text.ToString();
                    dtSource.Rows[0]["txbl_comm_inc_oth_24"] = add_tbx_Txbl_com_prev.Text.ToString();
                    dtSource.Rows[0]["total_exemptions_26"] = tbx_ls_ttl_expt.Text.ToString();
                    dtSource.Rows[0]["prm_paid_health_27"] = tbx_ls_prm_hlth.Text.ToString();
                    dtSource.Rows[0]["annual_tax_due_29"] = tbx_tax_due.Text.ToString();
                    dtSource.Rows[0]["total_tax_due_pres_30a"] = tbx_prst_empl.Text.ToString();
                    dtSource.Rows[0]["total_tax_due_prev_30b"] = tbx_prev_empl.Text.ToString();
                    dtSource.Rows[0]["monthly_tax_rate_hp_ot"] = tbx_per.Text.ToString();
                    dtSource.Rows[0]["monthly_tax_due_sal_99"] = (Convert.ToDecimal(tbx_prst_empl.Text.ToString()) / 12);
                    dtSource.Rows[0]["monthly_tax_due_hp_99"]  = (Convert.ToDecimal(tbx_hzrd.Text.ToString()) * (Convert.ToDecimal(tbx_per.Text.ToString())/100)) / 12;


                    if (ddl_empl_type.SelectedValue.ToString().Trim() == "RE")
                    {

                        dtSource_dtl.Rows[0]["account_code"] = "20201010";
                        dtSource_dtl.Rows[0]["account_sub_code"] = "01";

                    }

                    else if (ddl_empl_type.SelectedValue.ToString().Trim() == "CE")
                    {

                        dtSource_dtl.Rows[0]["account_code"] = "20201010";
                        dtSource_dtl.Rows[0]["account_sub_code"] = "02";

                    }

                    else
                    {
                        dtSource_dtl.Rows[0]["account_code"] = "20201010";
                        dtSource_dtl.Rows[0]["account_sub_code"] = "03";
                    }


                    dtSource_dtl.Rows[0]["payroll_year"] = ddl_year.SelectedValue.ToString().Trim();
                    dtSource_dtl.Rows[0]["empl_id"]      = txtb_empl_id.Text.ToString().Trim();
                    dtSource_dtl.Rows[0]["month_01_act"] = tbx_jan_due_act.Text;
                    dtSource_dtl.Rows[0]["month_02_act"] = tbx_feb_due_act.Text;
                    dtSource_dtl.Rows[0]["month_03_act"] = tbx_mar_due_act.Text;
                    dtSource_dtl.Rows[0]["month_04_act"] = tbx_apr_due_act.Text;
                    dtSource_dtl.Rows[0]["month_05_act"] = tbx_may_due_act.Text;
                    dtSource_dtl.Rows[0]["month_06_act"] = tbx_jun_due_act1.Text;
                    dtSource_dtl.Rows[0]["month_07_act"] = tbx_jul_due_act.Text;
                    dtSource_dtl.Rows[0]["month_08_act"] = tbx_aug_due_act.Text;
                    dtSource_dtl.Rows[0]["month_09_act"] = tbx_sep_due_act.Text;
                    dtSource_dtl.Rows[0]["month_10_act"] = tbx_oct_due_act.Text;
                    dtSource_dtl.Rows[0]["month_11_act"] = tbx_nov_due_act.Text;
                    dtSource_dtl.Rows[0]["month_12_act"] = tbx_dec_due_act.Text;
                    dtSource_dtl.Rows[0]["month_01_prj"] = tbx_jan_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_02_prj"] = tbx_feb_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_03_prj"] = tbx_mar_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_04_prj"] = tbx_apr_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_05_prj"] = tbx_may_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_06_prj"] = tbx_jun_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_07_prj"] = tbx_jul_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_08_prj"] = tbx_aug_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_09_prj"] = tbx_sep_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_10_prj"] = tbx_oct_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_11_prj"] = tbx_nov_due_prj.Text;
                    dtSource_dtl.Rows[0]["month_12_prj"] = tbx_dec_due_prj.Text;
                    

                    scriptInsertUpdate  = MyCmn.updatescript(dtSource);
                    scriptInsertUpdate1 = MyCmn.updatescript(dtSource_dtl);


                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;
                    if (msg.Substring(0, 1) == "X") return;

                    if (scriptInsertUpdate1 == string.Empty) return;
                    string msg1 = MyCmn.insertdata(scriptInsertUpdate1);
                    if (msg1 == "") return;
                    if (msg1.Substring(0, 1) == "X") return;

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                       DataRow nrow = dataListGrid.NewRow();
                        nrow["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                        nrow["empl_id"]                 = ddl_empl_id.SelectedValue.ToString().Trim();
                        nrow["ntx_13th_14th_37"]        = tbx_13th_oth_non.Text.ToString();
                        nrow["ntx_de_minimis_38"]       = tbx_de_min.Text.ToString();
                        nrow["ntx_gsis_phic_hdmf_39"]   = tbx_contri.Text.ToString();
                        nrow["ntx_salaries_oth_40"]     = tbx_slrs_oth_comp.Text.ToString();
                        nrow["basicsalary_42"]          = tbx_sal.Text.ToString();
                        nrow["representation_43"]       = tbx_RA.Text.ToString();
                        nrow["transportation_44"]       = tbx_TA.Text.ToString();
                        nrow["cola_45"]                 = tbx_cola.Text.ToString();
                        nrow["fh_allowance_46"]         = tbx_FHA.Text.ToString();
                        nrow["others_47a"]              = tbx_oth_a.Text.ToString();
                        nrow["others_47b"]              = tbx_oth_b.Text.ToString();
                        nrow["commision_48"]            = tbx_com.Text.ToString();
                        nrow["prof_sharing_49"]         = tbx_prft_s.Text.ToString();
                        nrow["fi_drctr_fees_50"]        = tbx_fid.Text.ToString();
                        nrow["txbl_13th_14th_51"]       = tbx_13_oth.Text.ToString();
                        nrow["hazard_pay_52"]           = tbx_hzrd.Text.ToString();
                        nrow["ot_pay_53"]               = tbx_ot.Text.ToString();
                        nrow["others_54a"]              = tbx_hon.Text.ToString();
                        nrow["others_54b"]              = tbx_sub.Text.ToString();
                        nrow["txbl_comm_inc_oth_24"]    = add_tbx_Txbl_com_prev.Text.ToString();
                        nrow["total_exemptions_26"]     = tbx_ls_ttl_expt.Text.ToString();
                        nrow["prm_paid_health_27"]      = tbx_ls_prm_hlth.Text.ToString();
                        nrow["annual_tax_due_29"]       = tbx_tax_due.Text.ToString();
                        nrow["total_tax_due_pres_30a"]  = tbx_prst_empl.Text.ToString();
                        nrow["total_tax_due_prev_30b"]  = tbx_prev_empl.Text.ToString();
                        nrow["monthly_tax_rate_hp_ot"]  = tbx_per.Text.ToString();
                        nrow["monthly_tax_due_sal_99"]  = (Convert.ToDecimal(tbx_prst_empl.Text.ToString()) / 12);
                        nrow["monthly_tax_due_hp_99"]   = (Convert.ToDecimal(tbx_hzrd.Text.ToString()) * (Convert.ToDecimal(tbx_per.Text.ToString()) / 100)) / 12;
                        nrow["employee_name"]           = ddl_empl_id.SelectedItem.Text.ToString()  ;
                        nrow["total_gross_pay"]         = (Convert.ToDecimal(tbx_gross_comp.Text.ToString()) + Convert.ToDecimal(add_tbx_Txbl_com_prev.Text.ToString())).ToString("#,##0.00");
                        nrow["total_taxable_income"]    = tbx_net_txbl.Text.ToString();
                        
                        nrow["month_01_act"]            = tbx_jan_due_act.Text;
                        nrow["month_02_act"]            = tbx_feb_due_act.Text;
                        nrow["month_03_act"]            = tbx_mar_due_act.Text;
                        nrow["month_04_act"]            = tbx_apr_due_act.Text;
                        nrow["month_05_act"]            = tbx_may_due_act.Text;
                        nrow["month_06_act"]            = tbx_jun_due_act1.Text;
                        nrow["month_07_act"]            = tbx_jul_due_act.Text;
                        nrow["month_08_act"]            = tbx_aug_due_act.Text;
                        nrow["month_09_act"]            = tbx_sep_due_act.Text;
                        nrow["month_10_act"]            = tbx_oct_due_act.Text;
                        nrow["month_11_act"]            = tbx_nov_due_act.Text;
                        nrow["month_12_act"]            = tbx_dec_due_act.Text;
                        nrow["month_01_prj"]            = tbx_jan_due_prj.Text;
                        nrow["month_02_prj"]            = tbx_feb_due_prj.Text;
                        nrow["month_03_prj"]            = tbx_mar_due_prj.Text;
                        nrow["month_04_prj"]            = tbx_apr_due_prj.Text;
                        nrow["month_05_prj"]            = tbx_may_due_prj.Text;
                        nrow["month_06_prj"]            = tbx_jun_due_prj.Text;
                        nrow["month_07_prj"]            = tbx_jul_due_prj.Text;
                        nrow["month_08_prj"]            = tbx_aug_due_prj.Text;
                        nrow["month_09_prj"]            = tbx_sep_due_prj.Text;
                        nrow["month_10_prj"]            = tbx_oct_due_prj.Text;
                        nrow["month_11_prj"]            = tbx_nov_due_prj.Text;
                        nrow["month_12_prj"]            = tbx_dec_due_prj.Text;
                        nrow["acct_total_amount"]       = tbx_ttl_amt_due.Text.ToString();

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        up_dataListGrid.Update();
                        

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"]             = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["empl_id"]                  = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["ntx_13th_14th_37"]         = tbx_13th_oth_non.Text.ToString();
                        row2Edit[0]["ntx_de_minimis_38"]        = tbx_de_min.Text.ToString();
                        row2Edit[0]["ntx_gsis_phic_hdmf_39"]    = tbx_contri.Text.ToString();
                        row2Edit[0]["ntx_salaries_oth_40"]      = tbx_slrs_oth_comp.Text.ToString();
                        row2Edit[0]["basicsalary_42"]           = tbx_sal.Text.ToString();
                        row2Edit[0]["representation_43"]        = tbx_RA.Text.ToString();
                        row2Edit[0]["transportation_44"]        = tbx_TA.Text.ToString();
                        row2Edit[0]["cola_45"]                  = tbx_cola.Text.ToString();
                        row2Edit[0]["fh_allowance_46"]          = tbx_FHA.Text.ToString();
                        row2Edit[0]["others_47a"]               = tbx_oth_a.Text.ToString();
                        row2Edit[0]["others_47b"]               = tbx_oth_b.Text.ToString();
                        row2Edit[0]["commision_48"]             = tbx_com.Text.ToString();
                        row2Edit[0]["prof_sharing_49"]          = tbx_prft_s.Text.ToString();
                        row2Edit[0]["fi_drctr_fees_50"]         = tbx_fid.Text.ToString();
                        row2Edit[0]["txbl_13th_14th_51"]        = tbx_13_oth.Text.ToString();
                        row2Edit[0]["hazard_pay_52"]            = tbx_hzrd.Text.ToString();
                        row2Edit[0]["ot_pay_53"]                = tbx_ot.Text.ToString();
                        row2Edit[0]["others_54a"]               = tbx_hon.Text.ToString();
                        row2Edit[0]["others_54b"]               = tbx_sub.Text.ToString();
                        row2Edit[0]["txbl_comm_inc_oth_24"]     = add_tbx_Txbl_com_prev.Text.ToString();
                        row2Edit[0]["total_exemptions_26"]      = tbx_ls_ttl_expt.Text.ToString();
                        row2Edit[0]["prm_paid_health_27"]       = tbx_ls_prm_hlth.Text.ToString();
                        row2Edit[0]["annual_tax_due_29"]        = tbx_tax_due.Text.ToString();
                        row2Edit[0]["total_tax_due_pres_30a"]   = tbx_prst_empl.Text.ToString();
                        row2Edit[0]["total_tax_due_prev_30b"]   = tbx_prev_empl.Text.ToString();
                        row2Edit[0]["monthly_tax_rate_hp_ot"]   = tbx_per.Text.ToString();
                        row2Edit[0]["monthly_tax_due_sal_99"]   = (Convert.ToDecimal(tbx_prst_empl.Text.ToString()) / 12);
                        row2Edit[0]["monthly_tax_due_hp_99"]    = (Convert.ToDecimal(tbx_hzrd.Text.ToString()) * (Convert.ToDecimal(tbx_per.Text.ToString()) / 100)) / 12;
                        row2Edit[0]["employee_name"]            = txtb_empl_name.Text.ToString();
                        row2Edit[0]["total_gross_pay"]          = (Convert.ToDecimal(tbx_gross_comp.Text.ToString()) + Convert.ToDecimal(add_tbx_Txbl_com_prev.Text.ToString())).ToString("#,##0.00");
                        row2Edit[0]["total_taxable_income"]     = tbx_net_txbl.Text.ToString();
                      
                        row2Edit[0]["month_01_act"]             = tbx_jan_due_act.Text;
                        row2Edit[0]["month_02_act"]             = tbx_feb_due_act.Text;
                        row2Edit[0]["month_03_act"]             = tbx_mar_due_act.Text;
                        row2Edit[0]["month_04_act"]             = tbx_apr_due_act.Text;
                        row2Edit[0]["month_05_act"]             = tbx_may_due_act.Text;
                        row2Edit[0]["month_06_act"]             = tbx_jun_due_act1.Text;
                        row2Edit[0]["month_07_act"]             = tbx_jul_due_act.Text;
                        row2Edit[0]["month_08_act"]             = tbx_aug_due_act.Text;
                        row2Edit[0]["month_09_act"]             = tbx_sep_due_act.Text;
                        row2Edit[0]["month_10_act"]             = tbx_oct_due_act.Text;
                        row2Edit[0]["month_11_act"]             = tbx_nov_due_act.Text;
                        row2Edit[0]["month_12_act"]             = tbx_dec_due_act.Text;
                        row2Edit[0]["month_01_prj"]             = tbx_jan_due_prj.Text;
                        row2Edit[0]["month_02_prj"]             = tbx_feb_due_prj.Text;
                        row2Edit[0]["month_03_prj"]             = tbx_mar_due_prj.Text;
                        row2Edit[0]["month_04_prj"]             = tbx_apr_due_prj.Text;
                        row2Edit[0]["month_05_prj"]             = tbx_may_due_prj.Text;
                        row2Edit[0]["month_06_prj"]             = tbx_jun_due_prj.Text;
                        row2Edit[0]["month_07_prj"]             = tbx_jul_due_prj.Text;
                        row2Edit[0]["month_08_prj"]             = tbx_aug_due_prj.Text;
                        row2Edit[0]["month_09_prj"]             = tbx_sep_due_prj.Text;
                        row2Edit[0]["month_10_prj"]             = tbx_oct_due_prj.Text;
                        row2Edit[0]["month_11_prj"]             = tbx_nov_due_prj.Text;
                        row2Edit[0]["month_12_prj"]             = tbx_dec_due_prj.Text;
                        row2Edit[0]["acct_total_amount"]        = tbx_ttl_amt_due.Text.ToString();

                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                        up_dataListGrid.Update();


                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

                }
            }
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging_dtl(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            MyCmn.Sort(GridView1, dataListGrid_dtl, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (GridView1.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + GridView1.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {

            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR employee_name LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR total_gross_pay LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR total_taxable_income LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%'";



            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("total_gross_pay", typeof(System.String));
            dtSource1.Columns.Add("total_taxable_income", typeof(System.String));

            


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

        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged_dtl(object sender, EventArgs e)
        {

            string searchExpression = "empl_id LIKE '%" + txtb_search1.Text.Trim().Replace("'", "''")
                + "%' OR account_title LIKE '%"
                + txtb_search1.Text.Trim().Replace("'", "''")
                + "%' OR account_code LIKE '%"
                + txtb_search1.Text.Trim().Replace("'", "''")
                + "%' OR acctclass_descr LIKE '%"
                + txtb_search1.Text.Trim().Replace("'", "''")
                + "%' OR acctclass_code LIKE '%"
                + txtb_search1.Text.Trim().Replace("'", "''")
                + "%' OR acct_total_amount LIKE '%"
                + txtb_search1.Text.Trim().Replace("'", "''") + "%'";



            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("account_title", typeof(System.String));
            dtSource1.Columns.Add("account_code", typeof(System.String));
            dtSource1.Columns.Add("account_sub_code", typeof(System.String));
            dtSource1.Columns.Add("acctclass_descr", typeof(System.String));
            dtSource1.Columns.Add("acctclass_code", typeof(System.String));
            dtSource1.Columns.Add("acct_total_amount", typeof(System.String));




            DataRow[] rows = dataListGrid_dtl.Select(searchExpression);
            dtSource1.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource1.ImportRow(row);
                }
            }
            GridView1.DataSource = dtSource1;
            GridView1.DataBind();
            UpdatePanel3.Update();
            txtb_search1.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txtb_search1.Focus();
            show_pagesx.Text = "Page: <b>" + (GridView1.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + GridView1.PageCount + "</strong>";
        }

        protected void retain_search()
        {
            if (txtb_search1.Text.ToString().Trim() != "")
            {
                string searchExpression = "empl_id LIKE '%" + txtb_search1.Text.Trim().Replace("'", "''")
                    + "%' OR account_title LIKE '%"
                    + txtb_search1.Text.Trim().Replace("'", "''")
                    + "%' OR account_code LIKE '%"
                    + txtb_search1.Text.Trim().Replace("'", "''")
                    + "%' OR acctclass_descr LIKE '%"
                    + txtb_search1.Text.Trim().Replace("'", "''")
                    + "%' OR acctclass_code LIKE '%"
                    + txtb_search1.Text.Trim().Replace("'", "''")
                    + "%' OR acct_total_amount LIKE '%"
                    + txtb_search1.Text.Trim().Replace("'", "''") + "%'";



                DataTable dtSource1 = new DataTable();
                dtSource1.Columns.Add("empl_id", typeof(System.String));
                dtSource1.Columns.Add("payroll_year", typeof(System.String));
                dtSource1.Columns.Add("account_title", typeof(System.String));
                dtSource1.Columns.Add("account_code", typeof(System.String));
                dtSource1.Columns.Add("account_sub_code", typeof(System.String));
                dtSource1.Columns.Add("acctclass_descr", typeof(System.String));
                dtSource1.Columns.Add("acctclass_code", typeof(System.String));
                dtSource1.Columns.Add("acct_total_amount", typeof(System.String));




                DataRow[] rows = dataListGrid_dtl.Select(searchExpression);
                dtSource1.Clear();
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        dtSource1.ImportRow(row);
                    }
                }
                GridView1.DataSource = dtSource1;
                GridView1.DataBind();
                UpdatePanel3.Update();
                txtb_search1.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                txtb_search1.Focus();
                show_pagesx.Text = "Page: <b>" + (GridView1.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + GridView1.PageCount + "</strong>";
            }

            txtb_search1.Text = "";
            UpdatePanel102.Update();
        }

        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Define Property for Sort Direction  
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
        //  BEGIN - JVA- 09/20/2018 - Check if Object already contains value  
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

        private bool IsDataValidated1()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");


            if (CommonCode.checkisdecimal(txtb_jan_amount_act) == false)
            {

                if (txtb_jan_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_jan_amount_act");
                    txtb_jan_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_jan_amount_act");
                    txtb_jan_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_jan_amount_prj) == false)
            {

                if (txtb_jan_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_jan_amount_prj");
                    txtb_jan_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_jan_amount_prj");
                    txtb_jan_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_feb_amount_act) == false)
            {

                if (txtb_feb_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_feb_amount_act");
                    txtb_feb_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_feb_amount_act");
                    txtb_feb_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_feb_amount_prj) == false)
            {

                if (txtb_feb_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_feb_amount_prj");
                    txtb_feb_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_feb_amount_prj");
                    txtb_feb_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_mar_amount_act) == false)
            {

                if (txtb_mar_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_mar_amount_act");
                    txtb_mar_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_mar_amount_act");
                    txtb_mar_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_mar_amount_prj) == false)
            {

                if (txtb_mar_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_mar_amount_prj");
                    txtb_mar_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_mar_amount_prj");
                    txtb_mar_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_apr_amount_act) == false)
            {

                if (txtb_apr_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_apr_amount_act");
                    txtb_apr_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_apr_amount_act");
                    txtb_apr_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_apr_amount_prj) == false)
            {

                if (txtb_apr_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_apr_amount_prj");
                    txtb_apr_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_apr_amount_prj");
                    txtb_apr_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_may_amount_act) == false)
            {

                if (txtb_apr_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_may_amount_act");
                    txtb_may_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_may_amount_act");
                    txtb_may_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_may_amount_prj) == false)
            {

                if (txtb_may_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_may_amount_prj");
                    txtb_may_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_may_amount_prj");
                    txtb_may_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_jun_amount_act) == false)
            {

                if (txtb_jun_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_jun_amount_act");
                    txtb_jun_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_jun_amount_act");
                    txtb_jun_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_jun_amount_prj) == false)
            {

                if (txtb_jun_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_jun_amount_prj");
                    txtb_jun_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_jun_amount_prj");
                    txtb_jun_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_jul_amount_act) == false)
            {

                if (txtb_jul_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_jul_amount_act");
                    txtb_jul_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_jul_amount_act");
                    txtb_jul_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_jul_amount_prj) == false)
            {

                if (txtb_jul_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_jul_amount_prj");
                    txtb_jul_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_jul_amount_prj");
                    txtb_jul_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_aug_amount_act) == false)
            {

                if (txtb_aug_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_aug_amount_act");
                    txtb_aug_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_aug_amount_act");
                    txtb_aug_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_aug_amount_prj) == false)
            {

                if (txtb_aug_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_aug_amount_prj");
                    txtb_aug_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_aug_amount_prj");
                    txtb_aug_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_sep_amount_act) == false)
            {

                if (txtb_sep_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_sep_amount_act");
                    txtb_sep_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_sep_amount_act");
                    txtb_sep_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_sep_amount_prj) == false)
            {

                if (txtb_sep_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_sep_amount_prj");
                    txtb_sep_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_sep_amount_prj");
                    txtb_sep_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_oct_amount_act) == false)
            {

                if (txtb_oct_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_oct_amount_act");
                    txtb_oct_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_oct_amount_act");
                    txtb_oct_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_oct_amount_prj) == false)
            {

                if (txtb_oct_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_oct_amount_prj");
                    txtb_oct_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_oct_amount_prj");
                    txtb_oct_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_nov_amount_act) == false)
            {

                if (txtb_nov_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_nov_amount_act");
                    txtb_nov_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_nov_amount_act");
                    txtb_nov_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_nov_amount_prj) == false)
            {

                if (txtb_nov_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_nov_amount_prj");
                    txtb_nov_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_nov_amount_prj");
                    txtb_nov_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_dec_amount_act) == false)
            {

                if (txtb_dec_amount_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_dec_amount_act");
                    txtb_dec_amount_act.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_dec_amount_act");
                    txtb_dec_amount_act.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_dec_amount_prj) == false)
            {

                if (txtb_dec_amount_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_dec_amount_prj");
                    txtb_dec_amount_prj.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_dec_amount_prj");
                    txtb_dec_amount_prj.Focus();
                    validatedSaved = false;
                }
            }

            return validatedSaved;

        }
            //**************************************************************************
            //  BEGIN - JVA- 09/20/2018 - Objects data Validation
            //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");
           
            if (ddl_empl_id.SelectedValue.ToString().Trim() == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(tbx_total) == false)
            {
                if (tbx_total.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_total");
                    tbx_total.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_total");
                    tbx_total.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_total_non) == false)
            {
                if (tbx_total_non.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_total_non");
                    tbx_total_non.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_total_non");
                    tbx_total_non.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(tbx_gross_comp) == false)
            {
                if (tbx_gross_comp.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_gross_comp");
                    tbx_gross_comp.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_gross_comp");
                    tbx_gross_comp.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(tbx_ttl_non_tx) == false)
            {
                if (tbx_ttl_non_tx.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_ttl_non_tx");
                    tbx_ttl_non_tx.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_ttl_non_tx");
                    tbx_ttl_non_tx.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(tbx_Txbl_inc) == false)
            {
                if (tbx_Txbl_inc.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_Txbl_inc");
                    tbx_Txbl_inc.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_Txbl_inc");
                    tbx_Txbl_inc.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(tbx_gross_txbl) == false)
            {
                if (tbx_Txbl_inc.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_gross_txbl");
                    tbx_Txbl_inc.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_gross_txbl");
                    tbx_gross_txbl.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(tbx_net_txbl) == false)
            {
                if (tbx_net_txbl.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_net_txbl");
                    tbx_net_txbl.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_net_txbl");
                    tbx_net_txbl.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            







            if (CommonCode.checkisdecimal(tbx_sal) == false)
            {
                if (tbx_sal.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_sal");
                    tbx_sal.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_sal");
                    tbx_sal.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }



            if (CommonCode.checkisdecimal(tbx_cola) == false)
            {
                if (tbx_cola.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_cola");
                    tbx_cola.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_cola");
                    tbx_cola.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

            }

            if (CommonCode.checkisdecimal(tbx_FHA) == false)
            {

                if (tbx_FHA.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_FHA");
                    tbx_FHA.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_FHA");
                    tbx_FHA.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

            }

            if (CommonCode.checkisdecimal(tbx_oth_a) == false)
            {

                if (tbx_oth_a.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_oth_a");
                    tbx_oth_a.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_oth_a");
                    tbx_oth_a.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_oth_b) == false)
            {

                if (tbx_oth_b.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_oth_b");
                    tbx_oth_b.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_oth_b");
                    tbx_oth_b.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_prft_s) == false)
            {

                if (tbx_prft_s.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_prft_s");
                    tbx_prft_s.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_prft_s");
                    tbx_prft_s.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_13_oth) == false)
            {
                if (tbx_13_oth.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_13_oth");
                    tbx_13_oth.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_13_oth");
                    tbx_13_oth.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_fid) == false)
            {

                if (tbx_fid.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_fid");
                    tbx_fid.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_fid");
                    tbx_fid.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_hzrd) == false)
            {

                if (tbx_hzrd.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_hzrd");
                    tbx_hzrd.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_hzrd");
                    tbx_hzrd.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_ot) == false)
            {

                if (tbx_ot.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_ot");
                    tbx_ot.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_ot");
                    tbx_ot.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_com) == false)
            {

                if (tbx_com.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_com");
                    tbx_com.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_com");
                    tbx_com.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            if (CommonCode.checkisdecimal(tbx_hon) == false)
            {

                if (tbx_hon.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_hon");
                    tbx_hon.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_hon");
                    tbx_hon.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }

            }

            if (CommonCode.checkisdecimal(tbx_sub) == false)
            {

                if (tbx_sub.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_sub");
                    tbx_sub.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_sub");
                    tbx_sub.Focus();
                    validatedSaved = false;
                    target_tab = 1;
                }
            }

            

            if (CommonCode.checkisdecimal(tbx_13th_oth_non) == false)
            {

                if (tbx_13th_oth_non.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_13th_oth_non");
                    tbx_13th_oth_non.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_13th_oth_non");
                    tbx_13th_oth_non.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }

            }

            if (CommonCode.checkisdecimal(tbx_de_min) == false)
            {


                if (tbx_de_min.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_de_min");
                    tbx_de_min.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_de_min");
                    tbx_de_min.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(tbx_slrs_oth_comp) == false)
            {
                if (tbx_slrs_oth_comp.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_slrs_oth_comp");
                    tbx_slrs_oth_comp.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_slrs_oth_comp");
                    tbx_slrs_oth_comp.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(tbx_RA) == false)
            {

                if (tbx_RA.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_RA");
                    tbx_RA.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_RA");
                    tbx_RA.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }

            }

            if (CommonCode.checkisdecimal(tbx_TA) == false)
            {

                if (tbx_TA.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_TA");
                    tbx_TA.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_TA");
                    tbx_TA.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(tbx_contri) == false)
            {
                if (tbx_contri.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_contri");
                    tbx_contri.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_contri");
                    tbx_contri.Focus();
                    validatedSaved = false;
                    target_tab = 2;
                }
            }

            if (CommonCode.checkisdecimal(tbx_per) == false)
            {
                if (tbx_per.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_per");
                    tbx_contri.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_per");
                    tbx_contri.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }
            

            if (CommonCode.checkisdecimal(tbx_tax_due) == false)
            {

                if (tbx_tax_due.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_tax_due");
                    tbx_tax_due.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_tax_due");
                    tbx_tax_due.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }
           
            if (CommonCode.checkisdecimal(add_tbx_Txbl_com_prev) == false)
            {


                if (add_tbx_Txbl_com_prev.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_Txbl_com");
                    add_tbx_Txbl_com_prev.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_Txbl_com");
                    add_tbx_Txbl_com_prev.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(tbx_prst_empl) == false)
            {

                if (tbx_prst_empl.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_prst_empl");
                    tbx_prst_empl.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_prst_empl");
                    tbx_prst_empl.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }
           
            if (CommonCode.checkisdecimal(tbx_prev_empl) == false)
            {

                if (tbx_prev_empl.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_prev_empl");
                    tbx_prev_empl.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_prev_empl");
                    tbx_prev_empl.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }
            if (CommonCode.checkisdecimal(tbx_ls_ttl_expt) == false)
            {

                if (tbx_ls_ttl_expt.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_ls_ttl_expt");
                    tbx_ls_ttl_expt.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_ls_ttl_expt");
                    tbx_ls_ttl_expt.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }
            if (CommonCode.checkisdecimal(tbx_ls_prm_hlth) == false)
            {

                if (tbx_ls_prm_hlth.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_ls_prm_hlth");
                    tbx_ls_prm_hlth.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_ls_prm_hlth");
                    tbx_ls_prm_hlth.Focus();
                    validatedSaved = false;
                    target_tab = 3;
                }
            }

            if (CommonCode.checkisdecimal(tbx_jan_due_act) == false)
            {

                if (tbx_jan_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_jan_due_act");
                    tbx_jan_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_jan_due_act");
                    tbx_jan_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_jan_due_prj) == false)
            {

                if (tbx_jan_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_jan_due_prj");
                    tbx_jan_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_jan_due_prj");
                    tbx_jan_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_jul_due_act) == false)
            {

                if (tbx_jul_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_jul_due_act");
                    tbx_jul_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_jul_due_act");
                    tbx_jul_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_jul_due_prj) == false)
            {

                if (tbx_jul_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_jul_due_prj");
                    tbx_jul_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_jul_due_prj");
                    tbx_jul_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_feb_due_act) == false)
            {

                if (tbx_feb_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_feb_due_act");
                    tbx_feb_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_feb_due_act");
                    tbx_feb_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_feb_due_prj) == false)
            {

                if (tbx_feb_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_feb_due_prj");
                    tbx_feb_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_feb_due_prj");
                    tbx_feb_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_aug_due_act) == false)
            {

                if (tbx_aug_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_aug_due_act");
                    tbx_aug_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_aug_due_act");
                    tbx_aug_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_jan_due_act) == false)
            {

                if (tbx_jan_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_jan_due_act");
                    tbx_jan_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_jan_due_act");
                    tbx_jan_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_aug_due_prj) == false)
            {

                if (tbx_aug_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_aug_due_prj");
                    tbx_aug_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_aug_due_prj");
                    tbx_aug_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_mar_due_act) == false)
            {

                if (tbx_mar_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_mar_due_act");
                    tbx_mar_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_mar_due_act");
                    tbx_mar_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_mar_due_prj) == false)
            {

                if (tbx_mar_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_mar_due_prj");
                    tbx_mar_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_mar_due_prj");
                    tbx_mar_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_sep_due_act) == false)
            {

                if (tbx_sep_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_sep_due_act");
                    tbx_sep_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_sep_due_act");
                    tbx_sep_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_apr_due_act) == false)
            {

                if (tbx_apr_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_apr_due_act");
                    tbx_apr_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_apr_due_act");
                    tbx_apr_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }


            if (CommonCode.checkisdecimal(tbx_apr_due_prj) == false)
            {

                if (tbx_apr_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_apr_due_prj");
                    tbx_apr_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_apr_due_prj");
                    tbx_apr_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }


            if (CommonCode.checkisdecimal(tbx_oct_due_act) == false)
            {

                if (tbx_oct_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_oct_due_act");
                    tbx_oct_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_oct_due_act");
                    tbx_oct_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_oct_due_prj) == false)
            {

                if (tbx_oct_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_oct_due_prj");
                    tbx_oct_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_oct_due_prj");
                    tbx_oct_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_may_due_act) == false)
            {

                if (tbx_may_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_may_due_act");
                    tbx_may_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_may_due_act");
                    tbx_may_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_may_due_prj) == false)
            {

                if (tbx_may_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_may_due_prj");
                    tbx_may_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_may_due_prj");
                    tbx_may_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_nov_due_act) == false)
            {

                if (tbx_nov_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_nov_due_act");
                    tbx_nov_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_nov_due_act");
                    tbx_nov_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_nov_due_prj) == false)
            {

                if (tbx_nov_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_nov_due_prj");
                    tbx_nov_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_nov_due_prj");
                    tbx_nov_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_jun_due_act1) == false)
            {

                if (tbx_jun_due_act1.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_jun_due_act1");
                    tbx_jun_due_act1.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_jun_due_act1");
                    tbx_jun_due_act1.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_jun_due_prj) == false)
            {

                if (tbx_jun_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_jun_due_prj");
                    tbx_jun_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_jun_due_prj");
                    tbx_jun_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_dec_due_act) == false)
            {

                if (tbx_dec_due_act.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_dec_due_act");
                    tbx_dec_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_dec_due_act");
                    tbx_dec_due_act.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }

            if (CommonCode.checkisdecimal(tbx_dec_due_prj) == false)
            {

                if (tbx_dec_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_dec_due_prj");
                    tbx_dec_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_dec_due_prj");
                    tbx_dec_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
            }


            if (CommonCode.checkisdecimal(tbx_sep_due_prj) == false)
            {

                if (tbx_sep_due_prj.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "tbx_sep_due_prj");
                    tbx_sep_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-tbx_sep_due_prj");
                    tbx_sep_due_prj.Focus();
                    validatedSaved = false;
                    target_tab = 4;
                }

            }





            ScriptManager.RegisterStartupScript(this, this.GetType(), "SelectTab", "select_tab("+target_tab+");", true);
            return validatedSaved;

        }
        //**********************************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
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
                    case "txtb_empl_name":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_empl_name.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_per":
                        {
                            LblRequired99.Text = MyCmn.CONST_RQDFLD;
                            tbx_per.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_ls_prm_hlth":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            tbx_ls_prm_hlth.BorderColor = Color.Red;
                            //UpdatePanel55.Update();
                            break;
                        }
                    case "tbx_ls_ttl_expt":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            tbx_ls_ttl_expt.BorderColor = Color.Red;
                            //UpdatePanel54.Update();
                            break;
                        }

                    case "tbx_prev_empl":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            tbx_prev_empl.BorderColor = Color.Red;
                            //UpdatePanel60.Update();
                            break;
                        }
                 

                    case "tbx_prst_empl":
                        {
                            LblRequired8.Text = MyCmn.CONST_RQDFLD;
                            tbx_prst_empl.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_Txbl_com":
                        {
                            LblRequired9.Text = MyCmn.CONST_RQDFLD;
                            add_tbx_Txbl_com_prev.BorderColor = Color.Red;
                            //UpdatePanel53.Update();
                            break;
                        }
                    //case "tbx_Txbl_inc":
                    //    {
                    //        LblRequired10.Text = MyCmn.CONST_RQDFLD;
                    //        tbx_Txbl_inc.BorderColor = Color.Red;
                    //        break;
                    //    }

                    case "tbx_tax_due":
                        {
                            LblRequired11.Text = MyCmn.CONST_RQDFLD;
                            tbx_tax_due.BorderColor = Color.Red;
                            break;
                        }
                    

                    case "tbx_contri":
                        {
                            LblRequired16.Text = MyCmn.CONST_RQDFLD;
                            tbx_contri.BorderColor = Color.Red;
                            break;
                        }
                        
                    case "tbx_TA":
                        {
                            LblRequired17.Text = MyCmn.CONST_RQDFLD;
                            tbx_TA.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_RA":
                        {
                            LblRequired18.Text = MyCmn.CONST_RQDFLD;
                            tbx_RA.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_slrs_oth_comp":
                        {
                            LblRequired19.Text = MyCmn.CONST_RQDFLD;
                            tbx_slrs_oth_comp.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_de_min":
                        {
                            LblRequired20.Text = MyCmn.CONST_RQDFLD;
                            tbx_de_min.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_13th_oth_non":
                        {
                            LblRequired21.Text = MyCmn.CONST_RQDFLD;
                            tbx_13th_oth_non.BorderColor = Color.Red;
                            break;
                        }
                   

                    case "tbx_sub":
                        {
                            LblRequired23.Text = MyCmn.CONST_RQDFLD;
                            tbx_sub.BorderColor = Color.Red;
                            //UpdatePanel43.Update();
                            break;
                        }
                    case "tbx_hon":
                        {
                            LblRequired24.Text = MyCmn.CONST_RQDFLD;
                            tbx_hon.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_com":
                        {
                            LblRequired25.Text = MyCmn.CONST_RQDFLD;
                            tbx_com.BorderColor = Color.Red;
                            //UpdatePanel25.Update();
                            break;
                        }

                    case "tbx_ot":
                        {
                            LblRequired26.Text = MyCmn.CONST_RQDFLD;
                            tbx_ot.BorderColor = Color.Red;
                            break;
                        }


                       
                    case "tbx_hzrd":
                        {
                            LblRequired27.Text = MyCmn.CONST_RQDFLD;
                            tbx_hzrd.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_fid":
                        {
                            LblRequired28.Text = MyCmn.CONST_RQDFLD;
                            tbx_fid.BorderColor = Color.Red;
                            //UpdatePanel28.Update();
                            break;
                        }
                    case "tbx_13_oth":
                        {
                            LblRequired29.Text = MyCmn.CONST_RQDFLD;
                            tbx_13_oth.BorderColor = Color.Red;
                            break;
                        }
                    case "tbx_prft_s":
                        {
                            LblRequired30.Text = MyCmn.CONST_RQDFLD;
                            tbx_prft_s.BorderColor = Color.Red;
                            //UpdatePanel26.Update();
                            break;
                        }

                    case "tbx_oth_b":
                        {
                            LblRequired31.Text = MyCmn.CONST_RQDFLD;
                            tbx_oth_b.BorderColor = Color.Red;
                            //UpdatePanel21.Update();
                            break;
                        }
                        
                    case "tbx_oth_a":
                        {
                            LblRequired32.Text = MyCmn.CONST_RQDFLD;
                            tbx_oth_a.BorderColor = Color.Red;
                            //UpdatePanel15.Update();
                            break;
                        }

                    case "tbx_FHA":
                        {
                            LblRequired33.Text = MyCmn.CONST_RQDFLD;
                            tbx_FHA.BorderColor = Color.Red;
                            //UpdatePanel24.Update();
                            break;
                        }
                    case "tbx_cola":
                        {
                            LblRequired34.Text = MyCmn.CONST_RQDFLD;
                            tbx_cola.BorderColor = Color.Red;
                            //UpdatePanel2.Update();
                            break;
                        }
                    case "tbx_sal":
                        {
                            LblRequired35.Text = MyCmn.CONST_RQDFLD;
                            tbx_sal.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_per":
                        {
                            LblRequired99.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_per.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_ls_prm_hlth":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_ls_prm_hlth.BorderColor = Color.Red;
                            //UpdatePanel55.Update();
                            break;
                        }
                    case "invalid-tbx_ls_ttl_expt":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_ls_ttl_expt.BorderColor = Color.Red;
                            //UpdatePanel54.Update();
                            break;
                        }

                    case "invalid-tbx_prev_empl":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_prev_empl.BorderColor = Color.Red;
                            //UpdatePanel60.Update();
                            break;
                        }
                  

                    case "invalid-tbx_prst_empl":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_prst_empl.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_Txbl_com":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            add_tbx_Txbl_com_prev.BorderColor = Color.Red;
                            //UpdatePanel53.Update();
                            break;
                        }
                   
                    case "invalid-tbx_tax_due":
                        {
                            LblRequired11.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_tax_due.BorderColor = Color.Red;
                            break;
                        }
                    
                   
                    case "invalid-tbx_contri":
                        {
                            LblRequired16.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_contri.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_TA":
                        {
                            LblRequired17.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_TA.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_RA":
                        {
                            LblRequired18.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_RA.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_slrs_oth_comp":
                        {
                            LblRequired19.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_slrs_oth_comp.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_de_min":
                        {
                            LblRequired20.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_de_min.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_13th_oth_non":
                        {
                            LblRequired21.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_13th_oth_non.BorderColor = Color.Red;
                            break;
                        }
                   

                    case "invalid-tbx_sub":
                        {
                            LblRequired23.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_sub.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_hon":
                        {
                            LblRequired24.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_hon.BorderColor = Color.Red;
                            UpdatePanel43.Update();
                            break;
                        }
                    case "invalid-tbx_com":
                        {
                            LblRequired25.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_com.BorderColor = Color.Red;
                            //UpdatePanel25.Update();
                            break;
                        }

                    case "invalid-tbx_ot":
                        {
                            LblRequired26.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_ot.BorderColor = Color.Red;
                            break;
                        }



                    case "invalid-tbx_hzrd":
                        {
                            LblRequired27.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_hzrd.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_fid":
                        {
                            LblRequired28.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_fid.BorderColor = Color.Red;
                            //UpdatePanel28.Update();
                            break;
                        }
                    case "invalid-tbx_13_oth":
                        {
                            LblRequired29.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_13_oth.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-tbx_prft_s":
                        {
                            LblRequired30.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_prft_s.BorderColor = Color.Red;
                            //UpdatePanel26.Update();
                            break;
                        }

                    case "invalid-tbx_oth_b":
                        {
                            LblRequired31.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_oth_b.BorderColor = Color.Red;
                            //UpdatePanel21.Update();
                            break;
                        }

                    case "invalid-tbx_oth_a":
                        {
                            LblRequired32.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_oth_a.BorderColor = Color.Red;
                            //UpdatePanel15.Update();
                            break;
                        }

                    case "invalid-tbx_FHA":
                        {
                            LblRequired33.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_FHA.BorderColor = Color.Red;
                            //UpdatePanel24.Update();
                            break;
                        }
                    case "invalid-tbx_cola":
                        {
                            LblRequired34.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_cola.BorderColor = Color.Red;
                            //UpdatePanel2.Update();
                            break;
                        }
                    case "invalid-tbx_sal":
                        {
                            LblRequired35.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_sal.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_dec_due_prj":
                        {
                            LblRequired93.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_dec_due_prj.BorderColor = Color.Red;
                            //UpdatePanel100.Update();
                            break;
                        }

                    case "tbx_dec_due_prj":
                        {
                            LblRequired93.Text = MyCmn.CONST_RQDFLD;
                            tbx_dec_due_prj.BorderColor = Color.Red;
                            //UpdatePanel100.Update();
                            break;
                        }

                    case "tbx_dec_due_act":
                        {
                            LblRequired92.Text = MyCmn.CONST_RQDFLD;
                            tbx_dec_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_jun_due_prj":
                        {
                            LblRequired91.Text = MyCmn.CONST_RQDFLD;
                            tbx_jun_due_prj.BorderColor = Color.Red;
                            //UpdatePanel98.Update();
                            break;
                        }

                    case "tbx_jun_due_act1":
                        {
                            LblRequired90.Text = MyCmn.CONST_RQDFLD;
                            tbx_jun_due_act1.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_nov_due_prj":
                        {
                            LblRequired89.Text = MyCmn.CONST_RQDFLD;
                            tbx_nov_due_prj.BorderColor = Color.Red;
                            //UpdatePanel96.Update();
                            break;
                        }

                    case "tbx_nov_due_act":
                        {
                            LblRequired88.Text = MyCmn.CONST_RQDFLD;
                            tbx_nov_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_may_due_prj":
                        {
                            LblRequired87.Text = MyCmn.CONST_RQDFLD;
                            tbx_may_due_prj.BorderColor = Color.Red;
                            //UpdatePanel94.Update();
                            break;
                        }

                    case "tbx_may_due_act":
                        {
                            LblRequired86.Text = MyCmn.CONST_RQDFLD;
                            tbx_may_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_oct_due_prj":
                        {
                            LblRequired85.Text = MyCmn.CONST_RQDFLD;
                            tbx_oct_due_prj.BorderColor = Color.Red;
                            //UpdatePanel92.Update();
                            break;
                        }

                    case "tbx_oct_due_act":
                        {
                            LblRequired84.Text = MyCmn.CONST_RQDFLD;
                            tbx_oct_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_apr_due_prj":
                        {
                            LblRequired83.Text = MyCmn.CONST_RQDFLD;
                            tbx_apr_due_prj.BorderColor = Color.Red;
                            //UpdatePanel90.Update();
                            break;
                        }

                    case "tbx_apr_due_act":
                        {
                            LblRequired82.Text = MyCmn.CONST_RQDFLD;
                            tbx_apr_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_sep_due_prj":
                        {
                            LblRequired81.Text = MyCmn.CONST_RQDFLD;
                            tbx_sep_due_prj.BorderColor = Color.Red;
                            //UpdatePanel88.Update();
                            break;
                        }

                    case "tbx_sep_due_act":
                        {
                            LblRequired80.Text = MyCmn.CONST_RQDFLD;
                            tbx_sep_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_mar_due_prj":
                        {
                            LblRequired79.Text = MyCmn.CONST_RQDFLD;
                            tbx_mar_due_prj.BorderColor = Color.Red;
                            //UpdatePanel86.Update();
                            break;
                        }

                    case "tbx_mar_due_act":
                        {
                            LblRequired78.Text = MyCmn.CONST_RQDFLD;
                            tbx_mar_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_aug_due_prj":
                        {
                            LblRequired77.Text = MyCmn.CONST_RQDFLD;
                            tbx_aug_due_prj.BorderColor = Color.Red;
                            //UpdatePanel84.Update();
                            break;
                        }

                    case "tbx_aug_due_act":
                        {
                            LblRequired76.Text = MyCmn.CONST_RQDFLD;
                            tbx_aug_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_feb_due_prj":
                        {
                            LblRequired75.Text = MyCmn.CONST_RQDFLD;
                            tbx_feb_due_prj.BorderColor = Color.Red;
                            //UpdatePanel82.Update();
                            break;
                        }

                    case "tbx_feb_due_act":
                        {
                            LblRequired74.Text = MyCmn.CONST_RQDFLD;
                            tbx_feb_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_jul_due_prj":
                        {
                            LblRequired73.Text = MyCmn.CONST_RQDFLD;
                            tbx_jul_due_prj.BorderColor = Color.Red;
                            //UpdatePanel80.Update();
                            break;
                        }

                    case "tbx_jul_due_act":
                        {
                            LblRequired72.Text = MyCmn.CONST_RQDFLD;
                            tbx_jul_due_act.BorderColor = Color.Red;
                            //UpdatePanel78.Update();
                            break;
                        }

                    case "tbx_jan_due_prj":
                        {
                            LblRequired71.Text = MyCmn.CONST_RQDFLD;
                            tbx_jan_due_prj.BorderColor = Color.Red;
                            //UpdatePanel78.Update();
                            break;
                        }

                    case "tbx_jan_due_act":
                        {
                            LblRequired70.Text = MyCmn.CONST_RQDFLD;
                            tbx_jan_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_dec_due_act":
                        {
                            LblRequired92.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_dec_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_jun_due_prj":
                        {
                            LblRequired91.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_jun_due_prj.BorderColor = Color.Red;
                            //UpdatePanel98.Update();
                            break;
                        }

                    case "invalid-tbx_jun_due_act1":
                        {
                            LblRequired90.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_jun_due_act1.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_nov_due_prj":
                        {
                            LblRequired89.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_nov_due_prj.BorderColor = Color.Red;
                            //UpdatePanel96.Update();
                            break;
                        }

                    case "invalid-tbx_nov_due_act":
                        {
                            LblRequired88.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_nov_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_may_due_prj":
                        {
                            LblRequired87.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_may_due_prj.BorderColor = Color.Red;
                            //UpdatePanel94.Update();
                            break;
                        }

                    case "invalid-tbx_may_due_act":
                        {
                            LblRequired86.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_may_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_oct_due_prj":
                        {
                            LblRequired85.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_oct_due_prj.BorderColor = Color.Red;
                            //UpdatePanel92.Update();
                            break;
                        }

                    case "invalid-tbx_oct_due_act":
                        {
                            LblRequired84.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_oct_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_apr_due_prj":
                        {
                            LblRequired83.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_apr_due_prj.BorderColor = Color.Red;
                            //UpdatePanel90.Update();
                            break;
                        }

                    case "invalid-tbx_apr_due_act":
                        {
                            LblRequired82.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_apr_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_sep_due_prj":
                        {
                            LblRequired81.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_sep_due_prj.BorderColor = Color.Red;
                            //UpdatePanel88.Update();
                            break;
                        }

                    case "invalid-tbx_sep_due_act":
                        {
                            LblRequired80.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_sep_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_mar_due_prj":
                        {
                            LblRequired79.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_mar_due_prj.BorderColor = Color.Red;
                            //UpdatePanel86.Update();
                            break;
                        }

                    case "invalid-tbx_mar_due_act":
                        {
                            LblRequired78.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_mar_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_aug_due_prj":
                        {
                            LblRequired77.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_aug_due_prj.BorderColor = Color.Red;
                            //UpdatePanel84.Update();
                            break;
                        }

                    case "invalid-tbx_aug_due_act":
                        {
                            LblRequired76.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_aug_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_feb_due_prj":
                        {
                            LblRequired75.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_feb_due_prj.BorderColor = Color.Red;
                            //UpdatePanel82.Update();
                            break;
                        }

                    case "invalid-tbx_feb_due_act":
                        {
                            LblRequired74.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_feb_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_jul_due_prj":
                        {
                            LblRequired73.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_jul_due_prj.BorderColor = Color.Red;
                            //UpdatePanel80.Update();
                            break;
                        }

                    case "invalid-tbx_jul_due_act":
                        {
                            LblRequired72.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_jul_due_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_jan_due_prj":
                        {
                            LblRequired71.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_jan_due_prj.BorderColor = Color.Red;
                            //UpdatePanel78.Update();
                            break;
                        }

                    case "invalid-tbx_jan_due_act":
                        {
                            LblRequired70.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_jan_due_act.BorderColor = Color.Red;
                            break;
                        }


                    case "txtb_jan_amount_act":
                        {
                            LblRequired100.Text = MyCmn.CONST_RQDFLD;
                            txtb_jan_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_jan_amount_prj":
                        {
                            LblRequired101.Text = MyCmn.CONST_RQDFLD;
                            txtb_jan_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_feb_amount_act":
                        {
                            LblRequired102.Text = MyCmn.CONST_RQDFLD;
                            txtb_feb_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_feb_amount_prj":
                        {
                            LblRequired103.Text = MyCmn.CONST_RQDFLD;
                            txtb_feb_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_mar_amount_act":
                        {
                            LblRequired104.Text = MyCmn.CONST_RQDFLD;
                            txtb_mar_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_mar_amount_prj":
                        {
                            LblRequired105.Text = MyCmn.CONST_RQDFLD;
                            txtb_mar_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_apr_amount_act":
                        {
                            LblRequired106.Text = MyCmn.CONST_RQDFLD;
                            txtb_apr_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_apr_amount_prj":
                        {
                            LblRequired107.Text = MyCmn.CONST_RQDFLD;
                            txtb_apr_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_may_amount_act":
                        {
                            LblRequired108.Text = MyCmn.CONST_RQDFLD;
                            txtb_may_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_may_amount_prj":
                        {
                            LblRequired109.Text = MyCmn.CONST_RQDFLD;
                            txtb_may_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_jun_amount_act":
                        {
                            LblRequired110.Text = MyCmn.CONST_RQDFLD;
                            txtb_jun_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_jun_amount_prj":
                        {
                            LblRequired111.Text = MyCmn.CONST_RQDFLD;
                            txtb_jun_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_jul_amount_act":
                        {
                            LblRequired112.Text = MyCmn.CONST_RQDFLD;
                            txtb_jul_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_jul_amount_prj":
                        {
                            LblRequired113.Text = MyCmn.CONST_RQDFLD;
                            txtb_jul_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_aug_amount_act":
                        {
                            LblRequired114.Text = MyCmn.CONST_RQDFLD;
                            txtb_aug_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_aug_amount_prj":
                        {
                            LblRequired115.Text = MyCmn.CONST_RQDFLD;
                            txtb_aug_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_sep_amount_act":
                        {
                            LblRequired116.Text = MyCmn.CONST_RQDFLD;
                            txtb_sep_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_sep_amount_prj":
                        {
                            LblRequired117.Text = MyCmn.CONST_RQDFLD;
                            txtb_sep_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_oct_amount_act":
                        {
                            LblRequired118.Text = MyCmn.CONST_RQDFLD;
                            txtb_oct_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_oct_amount_prj":
                        {
                            LblRequired119.Text = MyCmn.CONST_RQDFLD;
                            txtb_oct_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_nov_amount_act":
                        {
                            LblRequired120.Text = MyCmn.CONST_RQDFLD;
                            txtb_nov_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_nov_amount_prj":
                        {
                            LblRequired121.Text = MyCmn.CONST_RQDFLD;
                            txtb_nov_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_dec_amount_act":
                        {
                            LblRequired122.Text = MyCmn.CONST_RQDFLD;
                            txtb_dec_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_dec_amount_prj":
                        {
                            LblRequired123.Text = MyCmn.CONST_RQDFLD;
                            txtb_dec_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_jan_amount_act":
                        {
                            LblRequired100.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_jan_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_jan_amount_prj":
                        {
                            LblRequired101.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_jan_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_feb_amount_act":
                        {
                            LblRequired102.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_feb_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_feb_amount_prj":
                        {
                            LblRequired103.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_feb_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_mar_amount_act":
                        {
                            LblRequired104.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_mar_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_mar_amount_prj":
                        {
                            LblRequired105.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_mar_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_apr_amount_act":
                        {
                            LblRequired106.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_apr_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_apr_amount_prj":
                        {
                            LblRequired107.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_apr_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_may_amount_act":
                        {
                            LblRequired108.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_may_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_may_amount_prj":
                        {
                            LblRequired109.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_may_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_jun_amount_act":
                        {
                            LblRequired110.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_jun_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_jun_amount_prj":
                        {
                            LblRequired111.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_jun_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_jul_amount_act":
                        {
                            LblRequired112.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_jul_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_jul_amount_prj":
                        {
                            LblRequired113.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_jul_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_aug_amount_act":
                        {
                            LblRequired114.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_aug_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_aug_amount_prj":
                        {
                            LblRequired115.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_aug_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_sep_amount_act":
                        {
                            LblRequired116.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_sep_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_sep_amount_prj":
                        {
                            LblRequired117.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_sep_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_oct_amount_act":
                        {
                            LblRequired118.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_oct_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_oct_amount_prj":
                        {
                            LblRequired119.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_oct_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_nov_amount_act":
                        {
                            LblRequired120.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nov_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_nov_amount_prj":
                        {
                            LblRequired121.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_nov_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_dec_amount_act":
                        {
                            LblRequired122.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_dec_amount_act.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_dec_amount_prj":
                        {
                            LblRequired123.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_dec_amount_prj.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_total":
                        {
                            LblRequired22.Text = MyCmn.CONST_RQDFLD;
                            tbx_total.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_total_non":
                        {
                            LblRequired15.Text = MyCmn.CONST_RQDFLD;
                            tbx_total_non.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_gross_comp":
                        {
                            LblRequired14.Text = MyCmn.CONST_RQDFLD;
                            tbx_gross_comp.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_ttl_non_tx":
                        {
                            LblRequired12.Text = MyCmn.CONST_RQDFLD;
                            tbx_ttl_non_tx.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_Txbl_inc":
                        { 
                            LblRequired10.Text = MyCmn.CONST_RQDFLD;
                            tbx_Txbl_inc.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_gross_txbl":
                        {
                            LblRequired7.Text = MyCmn.CONST_RQDFLD;
                            tbx_gross_txbl.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_net_txbl":
                        {
                            LblRequired13.Text = MyCmn.CONST_RQDFLD;
                            tbx_net_txbl.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_total":
                        {
                            LblRequired22.Text = MyCmn.CONST_RQDFLD;
                            tbx_total.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_total_non":
                        {
                            LblRequired15.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_total_non.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_gross_comp":
                        {
                            LblRequired14.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_gross_comp.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_ttl_non_tx":
                        {
                            LblRequired12.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_ttl_non_tx.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_Txbl_inc":
                        {
                            LblRequired10.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_Txbl_inc.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_gross_txbl":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_gross_txbl.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-tbx_net_txbl":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_net_txbl.BorderColor = Color.Red;
                            break;
                        }







                }
            else if (!pMode)
            {
                switch (pObjectName)
                {

                    case "ALL":
                        {
                            LblRequired13.Text = "";
                            LblRequired7.Text = "";
                            LblRequired10.Text = "";
                            LblRequired12.Text = "";
                            LblRequired14.Text = "";
                            LblRequired15.Text = "";
                            LblRequired22.Text = "";
                            LblRequired1.Text = "";
                            LblRequired2.Text = "";
                            LblRequired3.Text = "";
                            LblRequired4.Text = "";
                            LblRequired5.Text = "";
                            LblRequired6.Text = "";
                            LblRequired8.Text = "";
                            LblRequired7.Text = "";
                            LblRequired9.Text = "";
                            LblRequired10.Text = "";
                            LblRequired11.Text = "";
                            LblRequired12.Text = "";
                            LblRequired13.Text = "";
                            LblRequired14.Text = "";
                            LblRequired15.Text = "";
                            LblRequired16.Text = "";
                            LblRequired17.Text = "";
                            LblRequired18.Text = "";
                            LblRequired19.Text = "";
                            LblRequired20.Text = "";
                            LblRequired21.Text = "";
                            LblRequired22.Text = "";
                            LblRequired23.Text = "";
                            LblRequired24.Text = "";
                            LblRequired25.Text = "";
                            LblRequired26.Text = "";
                            LblRequired27.Text = "";
                            LblRequired28.Text = "";
                            LblRequired29.Text = "";
                            LblRequired30.Text = "";
                            LblRequired31.Text = "";
                            LblRequired32.Text = "";
                            LblRequired33.Text = "";
                            LblRequired34.Text = "";
                            LblRequired35.Text = "";
                            LblRequired99.Text = "";
                            LblRequired70.Text = "";
                            LblRequired71.Text = "";
                            LblRequired72.Text = "";
                            LblRequired73.Text = "";
                            LblRequired74.Text = "";
                            LblRequired75.Text = "";
                            LblRequired76.Text = "";
                            LblRequired77.Text = "";
                            LblRequired78.Text = "";
                            LblRequired79.Text = "";
                            LblRequired80.Text = "";
                            LblRequired81.Text = "";
                            LblRequired82.Text = "";
                            LblRequired83.Text = "";
                            LblRequired84.Text = "";
                            LblRequired85.Text = "";
                            LblRequired86.Text = "";
                            LblRequired87.Text = "";
                            LblRequired88.Text = "";
                            LblRequired89.Text = "";
                            LblRequired90.Text = "";
                            LblRequired91.Text = "";
                            LblRequired92.Text = "";
                            LblRequired93.Text = "";
                            LblRequired120.Text = "";
                            LblRequired119.Text = "";
                            LblRequired118.Text = "";
                            LblRequired117.Text = "";
                            LblRequired116.Text = "";
                            LblRequired115.Text = "";
                            LblRequired114.Text = "";
                            LblRequired113.Text = "";
                            LblRequired112.Text = "";
                            LblRequired111.Text = "";
                            LblRequired110.Text = "";
                            LblRequired109.Text = "";
                            LblRequired108.Text = "";
                            LblRequired107.Text = "";
                            LblRequired106.Text = "";
                            LblRequired105.Text = "";
                            LblRequired104.Text = "";
                            LblRequired103.Text = "";
                            LblRequired102.Text = "";
                            LblRequired101.Text = "";
                            LblRequired100.Text = "";

                            tbx_gross_txbl.BorderColor = Color.LightGray;
                            tbx_net_txbl.BorderColor = Color.LightGray;
                            tbx_Txbl_inc.BorderColor = Color.LightGray;
                            tbx_ttl_non_tx.BorderColor = Color.LightGray;
                            tbx_gross_comp.BorderColor = Color.LightGray;
                            tbx_total_non.BorderColor = Color.LightGray;
                            tbx_total.BorderColor = Color.LightGray;

                            txtb_jan_amount_act.BorderColor = Color.LightGray;
                            txtb_jan_amount_prj.BorderColor = Color.LightGray;
                            txtb_feb_amount_act.BorderColor = Color.LightGray;
                            txtb_feb_amount_prj.BorderColor = Color.LightGray;
                            txtb_mar_amount_act.BorderColor = Color.LightGray;
                            txtb_mar_amount_prj.BorderColor = Color.LightGray;
                            txtb_apr_amount_act.BorderColor = Color.LightGray;
                            txtb_apr_amount_prj.BorderColor = Color.LightGray;
                            txtb_may_amount_act.BorderColor = Color.LightGray;
                            txtb_may_amount_prj.BorderColor = Color.LightGray;
                            txtb_jun_amount_act.BorderColor = Color.LightGray;
                            txtb_jun_amount_prj.BorderColor = Color.LightGray;
                            txtb_jul_amount_act.BorderColor = Color.LightGray;
                            txtb_jul_amount_prj.BorderColor = Color.LightGray;
                            txtb_aug_amount_act.BorderColor = Color.LightGray;
                            txtb_aug_amount_prj.BorderColor = Color.LightGray;
                            txtb_sep_amount_act.BorderColor = Color.LightGray;
                            txtb_sep_amount_prj.BorderColor = Color.LightGray;
                            txtb_oct_amount_act.BorderColor = Color.LightGray;
                            txtb_oct_amount_prj.BorderColor = Color.LightGray;
                            txtb_nov_amount_act.BorderColor = Color.LightGray;
                            LblRequired121.Text = "";
                            txtb_nov_amount_prj.BorderColor = Color.LightGray;
                            LblRequired122.Text = "";
                            txtb_dec_amount_act.BorderColor = Color.LightGray;
                            LblRequired123.Text = "";
                            txtb_dec_amount_prj.BorderColor = Color.LightGray;

                            tbx_dec_due_prj.BorderColor = Color.LightGray;
                            tbx_dec_due_act.BorderColor = Color.LightGray;
                            tbx_jun_due_prj.BorderColor = Color.LightGray;
                            tbx_jun_due_act1.BorderColor = Color.LightGray;
                            tbx_nov_due_prj.BorderColor = Color.LightGray;
                            tbx_nov_due_act.BorderColor = Color.LightGray;
                            tbx_may_due_prj.BorderColor = Color.LightGray;
                            tbx_may_due_act.BorderColor = Color.LightGray;
                            tbx_oct_due_prj.BorderColor = Color.LightGray;
                            tbx_oct_due_act.BorderColor = Color.LightGray;
                            tbx_apr_due_prj.BorderColor = Color.LightGray;
                            tbx_apr_due_act.BorderColor = Color.LightGray;
                            tbx_sep_due_prj.BorderColor = Color.LightGray;
                            tbx_sep_due_act.BorderColor = Color.LightGray;
                            tbx_mar_due_prj.BorderColor = Color.LightGray;
                            tbx_mar_due_act.BorderColor = Color.LightGray;
                            tbx_aug_due_prj.BorderColor = Color.LightGray;
                            tbx_aug_due_act.BorderColor = Color.LightGray;
                            tbx_feb_due_prj.BorderColor = Color.LightGray;
                            tbx_feb_due_act.BorderColor = Color.LightGray;
                            tbx_jul_due_prj.BorderColor = Color.LightGray;
                            tbx_jul_due_act.BorderColor = Color.LightGray;
                            tbx_jan_due_prj.BorderColor = Color.LightGray;
                            tbx_jan_due_act.BorderColor = Color.LightGray;
                            tbx_per.BorderColor = Color.LightGray;
                            ddl_empl_id.BorderColor = Color.LightGray;
                            tbx_sal.BorderColor = Color.LightGray;
                            tbx_cola.BorderColor = Color.LightGray;
                            tbx_FHA.BorderColor = Color.LightGray;
                            tbx_oth_a.BorderColor = Color.LightGray;
                            tbx_oth_b.BorderColor = Color.LightGray;
                            tbx_prft_s.BorderColor = Color.LightGray;
                            tbx_13_oth.BorderColor = Color.LightGray;
                            tbx_fid.BorderColor = Color.LightGray;
                            tbx_hzrd.BorderColor = Color.LightGray;
                            tbx_ot.BorderColor = Color.LightGray;
                            tbx_com.BorderColor = Color.LightGray;
                            tbx_hon.BorderColor = Color.LightGray;
                            tbx_sub.BorderColor = Color.LightGray;
                            tbx_13th_oth_non.BorderColor = Color.LightGray;
                            tbx_de_min.BorderColor = Color.LightGray;
                            tbx_contri.BorderColor = Color.LightGray;
                            tbx_RA.BorderColor = Color.LightGray;
                            tbx_TA.BorderColor = Color.LightGray;
                            tbx_slrs_oth_comp.BorderColor = Color.LightGray;
                            tbx_tax_due.BorderColor = Color.LightGray;
                            tbx_prst_empl.BorderColor = Color.LightGray;
                            tbx_prev_empl.BorderColor = Color.LightGray;
                            tbx_ls_ttl_expt.BorderColor = Color.LightGray;
                            tbx_ls_prm_hlth.BorderColor = Color.LightGray;
                            add_tbx_Txbl_com_prev.BorderColor = Color.LightGray;

                            //UpdatePanel2.Update();
                            //UpdatePanel24.Update();
                            //UpdatePanel15.Update();
                            //UpdatePanel21.Update();
                            //UpdatePanel26.Update();
                            //UpdatePanel28.Update();
                            //UpdatePanel25.Update();
                            //UpdatePanel55.Update();
                            //UpdatePanel53.Update();
                            //UpdatePanel60.Update();
                            //UpdatePanel54.Update();
                            //UpdatePanel43.Update();
                           
                            ////MONTHLY DUE
                            //UpdatePanel78.Update();
                            //UpdatePanel80.Update();
                            //UpdatePanel82.Update();
                            //UpdatePanel84.Update();
                            //UpdatePanel86.Update();
                            //UpdatePanel88.Update();
                            //UpdatePanel90.Update();
                            //UpdatePanel92.Update();
                            //UpdatePanel94.Update();
                            //UpdatePanel96.Update();
                            //UpdatePanel98.Update();
                            //UpdatePanel100.Update();
                            break;

                        }

                }
            }
        }

        protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue.ToString().Trim() != "" && ddl_empl_type.SelectedValue.ToString().Trim() != "" && ddl_department.SelectedValue.ToString().Trim() != "")
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveDataListGrid();
            UpdatePanel10.Update();
            up_dataListGrid.Update();
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue.ToString().Trim() != "" && ddl_empl_type.SelectedValue.ToString().Trim() != "" && ddl_department.SelectedValue.ToString().Trim() != "")
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            UpdatePanel10.Update();
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_year.SelectedValue.ToString().Trim() != "" && ddl_empl_type.SelectedValue.ToString().Trim() != "" && ddl_department.SelectedValue.ToString().Trim() != "")
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            UpdatePanel10.Update();
            RetrieveDataListGrid();
            up_dataListGrid.Update();

        }

        protected void ddl_empl_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }
        protected void ddl_subdep_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }

        protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingSection();
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }

        protected void ddl_empl_id_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (ddl_empl_id.SelectedValue.ToString().Trim() != "")
            {
                txtb_empl_id.Text = ddl_empl_id.SelectedValue.ToString().Trim();

                RetrieveDataListGrid_dtl();
                GridView1.Visible = true;
                UpdatePanel3.Update();
                txtb_search1.Visible = true;
                UpdatePanel102.Update();
                transfer_datalist();
                
            }

            else
            {
                GridView1.Visible = false;
                UpdatePanel3.Update();
                txtb_search1.Visible = false;
                UpdatePanel102.Update();
            }


           
        }

        protected void calculate_act_prj()
        {
           
                decimal calculate_txb = 0;
                decimal calculate1_txb = 0;



            
                salary = Convert.ToDecimal(tbx_sal.Text);
            



             calculate_txb =
                    salary
                    + Convert.ToDecimal(tbx_cola.Text)
                    + Convert.ToDecimal(tbx_FHA.Text)
                    + Convert.ToDecimal(tbx_oth_a.Text)
                    + Convert.ToDecimal(tbx_oth_b.Text)
                    + Convert.ToDecimal(tbx_prft_s.Text)
                    + Convert.ToDecimal(tbx_13_oth.Text)
                    + Convert.ToDecimal(tbx_fid.Text)
                    + Convert.ToDecimal(tbx_hzrd.Text)
                    + Convert.ToDecimal(tbx_ot.Text)
                    + Convert.ToDecimal(tbx_com.Text)
                    + Convert.ToDecimal(tbx_hon.Text)
                    + Convert.ToDecimal(tbx_sub.Text);

                tbx_total.Text = calculate_txb.ToString("#,##0.00");
                tbx_sal.Text = salary.ToString("#,##0.00");

                calculate1_txb =
                Convert.ToDecimal(tbx_13th_oth_non.Text)
                + Convert.ToDecimal(tbx_de_min.Text)
                + Convert.ToDecimal(tbx_slrs_oth_comp.Text)
                + Convert.ToDecimal(tbx_RA.Text)
                + Convert.ToDecimal(tbx_TA.Text)
                + Convert.ToDecimal(tbx_contri.Text);

                tbx_total_non.Text = calculate1_txb.ToString("#,##0.00");
                tbx_ttl_non_tx.Text = calculate1_txb.ToString("#,##0.00");
                tbx_gross_comp.Text = (calculate_txb + calculate1_txb).ToString("#,##0.00");
                tbx_Txbl_inc.Text = (Convert.ToDecimal(tbx_gross_comp.Text) - Convert.ToDecimal(tbx_ttl_non_tx.Text)).ToString("#,##0.00");


                tbx_gross_txbl.Text = (Convert.ToDecimal(tbx_Txbl_inc.Text) + Convert.ToDecimal(add_tbx_Txbl_com_prev.Text)).ToString("#,##0.00");
                tbx_net_txbl.Text = ((Convert.ToDecimal(tbx_gross_txbl.Text)) - (Convert.ToDecimal(tbx_ls_ttl_expt.Text) + Convert.ToDecimal(tbx_ls_prm_hlth.Text))).ToString("#,##0.00").Trim();
                tbx_ttl_adjstd.Text = (Convert.ToDecimal(tbx_prst_empl.Text) + Convert.ToDecimal(tbx_prev_empl.Text)).ToString("#,##0.00");

                decimal ttl_act_prj = 0;
                if (Convert.ToDecimal(tbx_jan_due_act.Text) != 0)
                {
                    ttl_act_prj = Convert.ToDecimal(tbx_jan_due_act.Text);
                }
                else
                {
                    ttl_act_prj = Convert.ToDecimal(tbx_jan_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_feb_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_feb_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_feb_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_mar_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_mar_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_mar_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_apr_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_apr_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_apr_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_may_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_may_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_may_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_jun_due_act1.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_jun_due_act1.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_jun_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_jul_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_jul_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_jul_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_aug_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_aug_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_aug_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_sep_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_sep_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_sep_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_oct_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_oct_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_oct_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_nov_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_nov_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_nov_due_prj.Text);
                }

                if (Convert.ToDecimal(tbx_dec_due_act.Text) != 0)
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_dec_due_act.Text);
                }
                else
                {
                    ttl_act_prj = ttl_act_prj + Convert.ToDecimal(tbx_dec_due_prj.Text);
                }





            
               tbx_ttl_amt_due.Text = ttl_act_prj.ToString("#,##0.00");
               tbx_prst_empl.Text   = ttl_act_prj.ToString("#,##0.00");
               tbx_ttl_adjstd.Text  = (Convert.ToDecimal(tbx_prst_empl.Text) + Convert.ToDecimal(tbx_prev_empl.Text)).ToString("#,##0.00");




        }
        

        protected void calculate_act_prj1()
        {

            decimal ttl_act_prj = 0;
            if (Convert.ToDecimal(txtb_jan_amount_act.Text) != 0)
            {
                ttl_act_prj = Convert.ToDecimal(txtb_jan_amount_act.Text);
            }
            else
            {
                ttl_act_prj = Convert.ToDecimal(txtb_jan_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_feb_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_feb_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_feb_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_mar_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_mar_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_mar_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_apr_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_apr_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_apr_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_may_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_may_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_may_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_jun_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_jun_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_jun_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_jul_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_jul_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_jul_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_aug_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_aug_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_aug_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_sep_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_sep_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_sep_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_oct_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_oct_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_oct_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_nov_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_nov_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_nov_amount_prj.Text);
            }

            if (Convert.ToDecimal(txtb_dec_amount_act.Text) != 0)
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_dec_amount_act.Text);
            }
            else
            {
                ttl_act_prj = ttl_act_prj + Convert.ToDecimal(txtb_dec_amount_prj.Text);
            }

            tbx_acct_ttl.Text = ttl_act_prj.ToString("#,##0.00");
            tbx_ttl_adjstd.Text = (Convert.ToDecimal(tbx_prst_empl.Text) + Convert.ToDecimal(tbx_prev_empl.Text)).ToString("#,##0.00");


        }

        protected void calculate_tax_due()
        {

            decimal total_txbl_income = 0;
            decimal total_tax_due = 0;
            decimal lower_limit = 0;
            decimal upper_limit = 0;
            decimal tax_on_lower_limit = 0;
            decimal tax_on_excess_over_lower_in_percent = 0;
            decimal percentage = 0;

            total_txbl_income = Convert.ToDecimal(tbx_net_txbl.Text.ToString().Trim());

            DataTable tax_table = MyCmn.RetrieveData("sp_calculate_taxtable_tbl_list", "par_payroll_year",ddl_year.SelectedValue.ToString().Trim(), "par_amt_per", total_txbl_income.ToString("#,##0.00"));

                if (tax_table.Rows.Count != 0)
                {


                    lower_limit = Convert.ToDecimal(tax_table.Rows[0]["lower_limit"].ToString());
                    upper_limit = Convert.ToDecimal(tax_table.Rows[0]["upper_limit"].ToString());
                    tax_on_lower_limit = Convert.ToDecimal(tax_table.Rows[0]["tax_on_lower_limit"].ToString());
                    tax_on_excess_over_lower_in_percent = Convert.ToDecimal(tax_table.Rows[0]["tax_on_excess"].ToString());
                    percentage = Convert.ToDecimal(tax_table.Rows[0]["tax_on_excess"].ToString());
                    tbx_per.Text = percentage.ToString("#,##0.00");
                    total_tax_due = total_txbl_income - lower_limit;
                    total_tax_due = total_tax_due * (percentage / 100);
                    total_tax_due = total_tax_due + tax_on_lower_limit;
                    tbx_tax_due.Text = total_tax_due.ToString("#,##0.00");

                }

            

                else
                {
                    tbx_tax_due.Text = total_tax_due.ToString("#,##0.00");
                }
            }

            
        

            private void transfer_datalist()
            {
            int x = 0;
            decimal max_13th_14th = 0;
            decimal total_dem1 = 0;
            decimal excess_dem1 = 0;
            decimal max_dem1 = 0;
            decimal total_13th_14th_oth1 = 0;
            decimal excess_13th_14th_oth1 = 0;
            decimal total_deduct = 0;

            decimal sal_amt = 0;
            decimal pera_amt = 0;
            decimal ra_amt = 0;
            decimal ta_amt = 0;
            decimal hzrd_amt = 0;
            decimal ot_amt = 0;
            decimal sub_amt = 0;

            //decimal max_deduct_gsis = 0;
            //decimal max_deduct_phic = 0;
            //decimal max_deduct_hdmf = 0;
            decimal max_deductions  = 0;

            dtSource_for_update_delete.Clear();
            foreach (DataRow nrow in dataListGrid_dtl.Rows)
            {
                DataRow nrow1 = dtSource_for_update_delete.NewRow();
                nrow1["payroll_year"] = nrow["payroll_year"];
                nrow1["empl_id"] = nrow["empl_id"];
                nrow1["account_code"] = nrow["account_code"];
                nrow1["account_sub_code"] = nrow["account_sub_code"];
                nrow1["month_01_act"] = nrow["month_01_act"];
                nrow1["month_02_act"] = nrow["month_02_act"];
                nrow1["month_03_act"] = nrow["month_03_act"];
                nrow1["month_04_act"] = nrow["month_04_act"];
                nrow1["month_05_act"] = nrow["month_05_act"];
                nrow1["month_06_act"] = nrow["month_06_act"];
                nrow1["month_07_act"] = nrow["month_07_act"];
                nrow1["month_08_act"] = nrow["month_08_act"];
                nrow1["month_09_act"] = nrow["month_09_act"];
                nrow1["month_10_act"] = nrow["month_10_act"];
                nrow1["month_11_act"] = nrow["month_11_act"];
                nrow1["month_12_act"] = nrow["month_12_act"];

                nrow1["month_01_prj"] = nrow["month_01_prj"];
                nrow1["month_02_prj"] = nrow["month_02_prj"];
                nrow1["month_03_prj"] = nrow["month_03_prj"];
                nrow1["month_04_prj"] = nrow["month_04_prj"];
                nrow1["month_05_prj"] = nrow["month_05_prj"];
                nrow1["month_06_prj"] = nrow["month_06_prj"];
                nrow1["month_07_prj"] = nrow["month_07_prj"];
                nrow1["month_08_prj"] = nrow["month_08_prj"];
                nrow1["month_09_prj"] = nrow["month_09_prj"];
                nrow1["month_10_prj"] = nrow["month_10_prj"];
                nrow1["month_11_prj"] = nrow["month_11_prj"];
                nrow1["month_12_prj"] = nrow["month_12_prj"];
                nrow1["action"] = 1;
                nrow1["retrieve"] = false;
                dtSource_for_update_delete.Rows.Add(nrow1);

                if (max_13th_14th == 0)
                {
                    if (dataListGrid_dtl.Rows[x]["acctclass_code"].ToString() == "02")
                    {
                        DataTable max_amount = MyCmn.RetrieveData("sp_bir_excempt_max_tbl_list", "par_account_code", dataListGrid_dtl.Rows[x]["account_code"].ToString().Trim());
                        max_13th_14th = (Convert.ToDecimal(max_amount.Rows[0]["monthly_max_amount_percentage"].ToString()) * 12);
                    }
                }
                
                    

                if (dataListGrid_dtl.Rows[x]["acctclass_code"].ToString() == "02")
                {
                    

                    total_13th_14th_oth1 = total_13th_14th_oth1 + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"].ToString());

                    if (total_13th_14th_oth1 > max_13th_14th)
                    {
                        excess_13th_14th_oth1 = total_13th_14th_oth1 - max_13th_14th;

                    }



                }

                else if (dataListGrid_dtl.Rows[x]["acctclass_code"].ToString() == "03")
                {



                    DataTable max_amount = MyCmn.RetrieveData("sp_bir_excempt_max_tbl_list", "par_account_code", dataListGrid_dtl.Rows[x]["account_code"].ToString().Trim());

                    if (max_amount.Rows.Count != 0)
                    {
                        txtb_max_amt.Text = max_amount.Rows[0]["monthly_max_amount_percentage"].ToString();
                        max_dem1 = Convert.ToDecimal(txtb_max_amt.Text) * 12;

                        if (Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"].ToString()) > max_dem1)
                        {
                            excess_dem1 = Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"].ToString()) - max_dem1;
                            total_dem1 = total_dem1 + max_dem1;

                            if ((Convert.ToDecimal(tbx_13th_oth_non.Text) + excess_dem1) > max_13th_14th)
                            {
                                excess_13th_14th_oth1 = excess_13th_14th_oth1 + excess_dem1;
                                total_13th_14th_oth1 = total_13th_14th_oth1 + excess_dem1;
                            }

                            else
                            {
                                total_13th_14th_oth1 = total_13th_14th_oth1 + excess_dem1;
                            }


                        }

                        else
                        {
                            total_dem1 = total_dem1 + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"].ToString());
                        }
                       
                    }

                    else
                    {
                        total_dem1 = total_dem1 + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"].ToString());
                    }

                   




                }

                else if (dataListGrid_dtl.Rows[x]["acctclass_code"].ToString() == "04")
                {


                    total_deduct = total_deduct + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"].ToString());

                    DataTable max_amount = MyCmn.RetrieveData("sp_bir_excempt_annual_tbl_list", "par_account_code", dataListGrid_dtl.Rows[x]["account_code"].ToString().Trim());

                    if (max_amount.Rows.Count != 0)
                    {
                        if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "20201020")
                        {
                            max_deductions = max_deductions + ((sal_amt * Convert.ToDecimal(max_amount.Rows[0]["monthly_max_amount_percentage"])/100)/12);
                        }

                        else if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "20201030")
                        {
                            max_deductions = max_deductions + Convert.ToDecimal(max_amount.Rows[0]["monthly_max_amount_percentage"].ToString());
                        }


                        else if(dataListGrid_dtl.Rows[x]["account_code"].ToString() == "20201040")
                        {
                            max_deductions = max_deductions + Convert.ToDecimal(max_amount.Rows[0]["monthly_max_amount_percentage"].ToString());
                        }
                        
                    }

                }

               
            

                else if (dataListGrid_dtl.Rows[x]["acctclass_code"].ToString() == "05")
                {


                }

                //DEFINING MANDATORY ACCOUNT CODE

                //PERA FIELD
                if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "50102010")
                {
                    pera_amt = pera_amt + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"]);
                   
                }

                //END PERA FIELD

                //GROSS PAY ASSUME SALARY FOR THE MOMENT
                else if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "99999991")
                {
                    sal_amt = sal_amt + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"]);
                    
                }
                

                //REPRESENTATION FIELD
                else if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "50102020")
                {
                    ra_amt = ra_amt + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"]);
                }
                //END REPRESENTATION FIELD

                //TRANSPORTATION FIELD
                else if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "50102030")
                {
                    ta_amt = ta_amt + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"]);
                }
                //END TRANSPORTATION FIELD


                //hazard_pay_52 FIELD
                else if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "50102110")
                {
                    hzrd_amt = hzrd_amt + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"]);
                    
                }
                //hazard_pay_52 FIELD

                //ot_pay_53 FIELD
                else if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "50102130")
                {
                    ot_amt = ot_amt + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"]);
                  
                }
                //ot_pay_53 FIELD

                //others_54B/Subsistence Allowance
                else if (dataListGrid_dtl.Rows[x]["account_code"].ToString() == "50102050")
                {
                    sub_amt = sub_amt + Convert.ToDecimal(dataListGrid_dtl.Rows[x]["acct_total_amount"]);
                    
                }


                x = x + 1;
               

            }

            max_deductions = max_deductions * 12;

            if (total_deduct > max_deductions)
            {
                target_tab = 2;
                tbx_contri.BorderColor = Color.Red;
                LblRequired16.Text = "TOTAL DEDUCTIONS EXCEEDED!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SelectTab", "select_tab(" + target_tab + ");", true);
            }

            else
            {
                tbx_contri.BorderColor = Color.LightGray;
                LblRequired16.Text = "";
            }

            if (total_13th_14th_oth1 > max_13th_14th)
            {
                excess_13th_14th_oth1 = total_13th_14th_oth1 - max_13th_14th;
                total_13th_14th_oth1 = max_13th_14th;
            }

            else
            {
                excess_13th_14th_oth1 = 0;
            }

            //MANDATORY ACCOUNT CLASS
            
            tbx_de_min.Text = total_dem1.ToString("#,##0.00");
            tbx_13_oth.Text = excess_13th_14th_oth1.ToString("#,##0.00");
            

            tbx_13th_oth_non.Text = total_13th_14th_oth1.ToString("#,##0.00");
            tbx_contri.Text = total_deduct.ToString("#,##0.00");

            //MANDATORY ACCOUNT CODES
            tbx_slrs_oth_comp.Text = pera_amt.ToString("#,##0.00");
            tbx_RA.Text = ra_amt.ToString("#,##0.00");
            tbx_TA.Text = ta_amt.ToString("#,##0.00");
            tbx_sal.Text = (sal_amt - Convert.ToDecimal(tbx_contri.Text)).ToString("#,##0.00");
            tbx_hzrd.Text = hzrd_amt.ToString("#,##0.00");
            tbx_ot.Text = ot_amt.ToString("#,##0.00");
            tbx_sub.Text = sub_amt.ToString("#,##0.00");



        }

        protected void btn_update_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND account_code = '" + svalues[1].ToString().Trim() + "' AND account_sub_code = '" + svalues[2].ToString().Trim() + "' AND payroll_year = '" + svalues[3].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid_dtl.Select(editExpression);
          
            if (IsDataValidated1())
            {
                calculate_act_prj1();

               
                row2Edit[0]["acct_total_amount"] = tbx_acct_ttl.Text.ToString();
                row2Edit[0]["month_01_act"] = txtb_jan_amount_act.Text.ToString();
                row2Edit[0]["month_02_act"] = txtb_feb_amount_act.Text.ToString();
                row2Edit[0]["month_03_act"] = txtb_mar_amount_act.Text.ToString();
                row2Edit[0]["month_04_act"] = txtb_apr_amount_act.Text.ToString();
                row2Edit[0]["month_05_act"] = txtb_may_amount_act.Text.ToString();
                row2Edit[0]["month_06_act"] = txtb_jun_amount_act.Text.ToString();
                row2Edit[0]["month_07_act"] = txtb_jul_amount_act.Text.ToString();
                row2Edit[0]["month_08_act"] = txtb_aug_amount_act.Text.ToString();
                row2Edit[0]["month_09_act"] = txtb_sep_amount_act.Text.ToString();
                row2Edit[0]["month_10_act"] = txtb_oct_amount_act.Text.ToString();
                row2Edit[0]["month_11_act"] = txtb_nov_amount_act.Text.ToString();
                row2Edit[0]["month_12_act"] = txtb_dec_amount_act.Text.ToString();
                row2Edit[0]["month_01_prj"] = txtb_jan_amount_prj.Text.ToString();
                row2Edit[0]["month_02_prj"] = txtb_feb_amount_prj.Text.ToString();
                row2Edit[0]["month_03_prj"] = txtb_mar_amount_prj.Text.ToString();
                row2Edit[0]["month_04_prj"] = txtb_apr_amount_prj.Text.ToString();
                row2Edit[0]["month_05_prj"] = txtb_may_amount_prj.Text.ToString();
                row2Edit[0]["month_06_prj"] = txtb_jun_amount_prj.Text.ToString();
                row2Edit[0]["month_07_prj"] = txtb_jul_amount_prj.Text.ToString();
                row2Edit[0]["month_08_prj"] = txtb_aug_amount_prj.Text.ToString();
                row2Edit[0]["month_09_prj"] = txtb_sep_amount_prj.Text.ToString();
                row2Edit[0]["month_10_prj"] = txtb_oct_amount_prj.Text.ToString();
                row2Edit[0]["month_11_prj"] = txtb_nov_amount_prj.Text.ToString();
                row2Edit[0]["month_12_prj"] = txtb_dec_amount_prj.Text.ToString();
                CommonCode.GridViewBind(ref this.GridView1, dataListGrid_dtl);
                UpdatePanel3.Update();

                transfer_datalist();

                calculate_act_prj();
                calculate_tax_due();

                retain_search();
                

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal12();", true);
            }
        }

       

        protected void btncalculate_Click(object sender, EventArgs e)
        {
            target_tab = 3;
            

            if (IsDataValidated())
            {

                calculate_act_prj();
                calculate_tax_due();
            }

            
        }
    }
    }

