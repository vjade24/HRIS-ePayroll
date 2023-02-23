//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Other Contribution and Loans
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
//JORGE RUSTOM VILLANUEVA    05/18/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cEmployeeTaxRateRECEDetails
{

    
    public partial class cEmployeeTaxRateRECEDetails : System.Web.UI.Page
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

        DataTable dataListGridGenerate
        {
            get
            {
                if ((DataTable)ViewState["dataListGridGenerate"] == null) return null;
                return (DataTable)ViewState["dataListGridGenerate"];
            }
            set
            {
                ViewState["dataListGridGenerate"] = value;
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

        DataTable dataListBirTax
        {
            get
            {
                if ((DataTable)ViewState["dataListBirTax"] == null) return null;
                return (DataTable)ViewState["dataListBirTax"];
            }
            set
            {
                ViewState["dataListBirTax"] = value;
            }
        }


        //********************************************************************
        //  BEGIN - JVA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();
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
                    ViewState["page_allow_add"]             = 0;
                    ViewState["page_allow_delete"]          = 0;
                    ViewState["page_allow_edit"]            = 0;
                    ViewState["page_allow_edit_history"]    = 0;
                    ViewState["page_allow_print"]           = 0;
                }
                else
                {
                    ViewState["page_allow_add"]             = 1;
                    ViewState["page_allow_delete"]          = 1;
                    ViewState["page_allow_edit"]            = 1;
                    ViewState["page_allow_edit_history"]    = 1;
                    ViewState["page_allow_print"]           = 1;
                }

                if (Session["PreviousValuesonPage_cEmployeeTaxRateRECE"] == null)
                    Session["PreviousValuesonPage_cEmployeeTaxRateRECE"] = "";
                else if (Session["PreviousValuesonPage_cEmployeeTaxRateRECE"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cEmployeeTaxRateRECE"].ToString().Split(new char[] { ',' });
                    //Session["PreviousValuesonPage_cEmployeeTaxRate_name"];
                    txtb_payroll_year.Text      = prevValues[1].ToString();
                    txtb_empl_id_hdr.Text       = prevValues[0].ToString();
                    txtb_tax_due.Text           = prevValues[3].ToString();
                    txtb_tax_due.Text           = Session["PreviousValuesonPage_cEmployeeTaxRateRECE_amt"].ToString().Trim();
                    txtb_tax_rate.Text          = prevValues[2].ToString();
                    txtb_employment_type.Text   = prevValues[4].ToString();
                    //txtb_position.Text      = prevValues[6].ToString();
                    //txtb_empl_id.Text       = prevValues[7].ToString();
                    //txtb_bir_class.Text     = prevValues[8].ToString();
                    //txtb_with_sworn.Text    = prevValues[10].ToString();
                    txtb_tax_rate_dtl.Text     = prevValues[2].ToString();
                    txtb_tax_due_dtl.Text     = Session["PreviousValuesonPage_cEmployeeTaxRateRECE_amt"].ToString().Trim();
                    txtb_position.Text        = prevValues[5].ToString();
                    txtb_empl_name_hdr.Text   = Session["PreviousValuesonPage_cEmployeeTaxRateRECE_name"].ToString();
                    txtb_empl_id.Text         = prevValues[0].ToString();
                    txtb_empl_name.Text       = Session["PreviousValuesonPage_cEmployeeTaxRateRECE_name"].ToString();
                   
                    RetrieveDataListGrid();
                }
            }
        }

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            //RetrieveBindingSubDep();
            //RetrieveBindingDivision();
            //RetrieveBindingSection();


            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cEmployeeTaxRateRECEDetails"] = "cEmployeeTaxRateRECEDetails";

            //RetrieveDataListGrid();

        }


        //*************************************************************************
        //  BEGIN - JRV- 09/09/2018 - Populate Combo list for Payroll Template for Job Order
        //*************************************************************************

        //private void RetriveTemplate()
        //{
        //    ddl_payroll_template.Items.Clear();
        //    DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list7");

        //    ddl_payroll_template.DataSource = dt;
        //    ddl_payroll_template.DataValueField = "payrolltemplate_code";
        //    ddl_payroll_template.DataTextField = "payrolltemplate_descr";
        //    ddl_payroll_template.DataBind();
        //    ListItem li = new ListItem("-- Select Here --", "");
        //    ddl_payroll_template.Items.Insert(0, li);
        //}

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            
            dataListGrid = MyCmn.RetrieveData("sp_annualtax_dtl_tbl_list", "p_payroll_year", txtb_payroll_year.Text.ToString().Trim(), "p_empl_id", txtb_empl_id_hdr.Text.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }





        //private void RetrieveEmploymentType()
        //{
        //    ddl_empl_type.Items.Clear();
        //    DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");

        //    ddl_empl_type.DataSource = dt;
        //    ddl_empl_type.DataValueField = "employment_type";
        //    ddl_empl_type.DataTextField = "employmenttype_description";
        //    ddl_empl_type.DataBind();
        //    ListItem li = new ListItem("-- Select Here --", "");
        //    ddl_empl_type.Items.Insert(0, li);
        //}


        //private string get_empl_details()
        //{
        //    string id_ref = "";
        //    DataRow[] selected_employee = dataListEmployee.Select("empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

        //    if (selected_employee.Length > 0)
        //    {
        //        id_ref = selected_employee[0]["account_id_nbr_ref"].ToString();
        //    }

        //    return id_ref;
        //}


        //private void RetrieveBindingSubDep()
        //{
        //    ddl_subdep.Items.Clear();
        //    DataTable dt = MyCmn.RetrieveData("sp_subdepartments_tbl_list");

        //    ddl_subdep.DataSource = dt;
        //    ddl_subdep.DataValueField = "subdepartment_code";
        //    ddl_subdep.DataTextField = "subdepartment_short_name";
        //    ddl_subdep.DataBind();
        //    ListItem li = new ListItem("-- Select Here --", "");
        //    ddl_subdep.Items.Insert(0, li);
        //}

        //private void RetrieveBindingDivision()
        //{
        //    ddl_division.Items.Clear();
        //    DataTable dt = MyCmn.RetrieveData("sp_divisions_tbl_combolist", "par_department_code", ddl_department.SelectedValue.ToString(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString());

        //    ddl_division.DataSource = dt;
        //    ddl_division.DataValueField = "division_code";
        //    ddl_division.DataTextField = "division_name1";
        //    ddl_division.DataBind();
        //    ListItem li = new ListItem("-- Select Here --", "");
        //    ddl_division.Items.Insert(0, li);
        //}

        //private void RetrieveBindingSection()
        //{
        //    ddl_section.Items.Clear();
        //    DataTable dt1 = MyCmn.RetrieveData("sp_sections_tbl_combolist", "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString(), "par_division_code", ddl_division.SelectedValue.ToString().Trim());

        //    ddl_section.DataSource = dt1;
        //    ddl_section.DataValueField = "section_code";
        //    ddl_section.DataTextField = "section_name1";
        //    ddl_section.DataBind();
        //    ListItem li = new ListItem("-- Select Here --", "");
        //    ddl_section.Items.Insert(0, li);
        //}










        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
            //For Header
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            ClearEntry();
            txtb_month.Visible = false;
            isDisable(MyCmn.CONST_ADD,"2");
            //txtb_payroll_template.Visible   = false;

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            
            
            //txtb_payroll_template.Text = "";
            txtb_remarks.Text   = "";
            txtb_voucher.Text   = "";
            txtb_pera_ca.Text   = "0.00";
            txtb_gsis.Text      = "0.00";
            txtb_gross.Text     = "0.00";
            txtb_hazard.Text    = "0.00";
            txtb_phic.Text      = "0.00";
            txtb_subs_allo.Text = "0.00";
            txtb_hdmf.Text      = "0.00";
            txtb_laundry.Text   = "0.00";
            txtb_wheld.Text     = "0.00";
            //txtb_payroll_template.Text = "";

        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("payroll_month", typeof(System.String));
            dtSource.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource.Columns.Add("pera_ca_amt", typeof(System.String));
            dtSource.Columns.Add("gsis_ps", typeof(System.String));
            dtSource.Columns.Add("phic_ps", typeof(System.String));
            dtSource.Columns.Add("hdmf_ps", typeof(System.String));
            dtSource.Columns.Add("hazard_pay", typeof(System.String));
            dtSource.Columns.Add("subsistence_allowance", typeof(System.String));
            dtSource.Columns.Add("laundry_allowance", typeof(System.String));
            dtSource.Columns.Add("gross_pay", typeof(System.String));
            dtSource.Columns.Add("wtax_amt", typeof(System.String));
            dtSource.Columns.Add("remarks", typeof(System.String));
            dtSource.Columns.Add("rcrd_status", typeof(System.String));
            dtSource.Columns.Add("acctclass_code", typeof(System.String));
        }



     

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollemployee_tax_dtl_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "effective_date", "empl_id", "voucher_nbr"};
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

       


        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();

            nrow["effective_date"]          = string.Empty;
            nrow["empl_id"]                 = string.Empty;
            nrow["voucher_nbr"]             = string.Empty;
            nrow["payroll_year"]            = string.Empty;
            nrow["payroll_month"]           = string.Empty;
            nrow["payrolltemplate_code"]    = string.Empty;
            nrow["pera_ca_amt"]             = string.Empty;
            nrow["gsis_ps"]                 = string.Empty;
            nrow["phic_ps"]                 = string.Empty;
            nrow["hdmf_ps"]                 = string.Empty;
            nrow["hazard_pay"]              = string.Empty;
            nrow["subsistence_allowance"]   = string.Empty;
            nrow["laundry_allowance"]       = string.Empty;
            nrow["gross_pay"]               = string.Empty;
            nrow["wtax_amt"]                = string.Empty;
            nrow["remarks"]                 = string.Empty;
            nrow["rcrd_status"]             = string.Empty;


            nrow["action"]                  = 1;
            nrow["retrieve"]                = false;
            dtSource.Rows.Add(nrow);
        }

        

        //***************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString().Trim();
            string seq = commandArgs.ToString().Trim();
            Lbl_delete.InnerText = "Delete this Record?";
            lnkBtnYes.Visible = true;
            lnkBtnYes_gen.Visible = false;
            icon_delete.Visible = true;
            icon_generate.Visible = false;
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        private void isDisable(string action,string rcrd_status)

        {

          txtb_remarks.Enabled        = false;
          txtb_pera_ca.Enabled        = false;
          txtb_gsis.Enabled           = false;
          txtb_gross.Enabled          = false;
          txtb_hazard.Enabled         = false;
          txtb_phic.Enabled           = false;
          txtb_subs_allo.Enabled      = false;
          txtb_hdmf.Enabled           = false;
          txtb_laundry.Enabled        = false;
          txtb_wheld.Enabled          = false;
          txtb_voucher.Enabled        = false;
            

        }


        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {

            string[] svalues = e.CommandArgument.ToString().Split(',');
            string deleteExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND effective_date = '" + svalues[1].ToString().Trim() + "' AND voucher_nbr = '" + svalues[2].ToString().Trim() + "'";
            MyCmn.DeleteBackEndData("payrollemployee_tax_dtl_tbl", "WHERE " + deleteExpression);
        
            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            GenerateEmployeeTax();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

        }


        //***************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void generateRow_Command(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString().Trim();
            string seq = commandArgs.ToString().Trim();
            Lbl_delete.InnerText = "Generate this Record";
            deleteRec1.Text = "Once generated, data will be updated automatically!";
            lnkBtnYes.Visible = false;
            lnkBtnYes_gen.Visible = true;
            icon_delete.Visible = false;
            icon_generate.Visible = true;
            lnkBtnYes_gen.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);



        }

        private void GenerateEmployeeTax()
        {

            if (Session["PreviousValuesonPage_cEmployeeTaxRate"] == null)
                Session["PreviousValuesonPage_cEmployeeTaxRate"] = "";
            else if (Session["PreviousValuesonPage_cEmployeeTaxRate"].ToString() != string.Empty)
            {
                string[] prevValues = Session["PreviousValuesonPage_cEmployeeTaxRate"].ToString().Split(new char[] { ',' });
                txtb_empl_name_hdr.Text = Session["PreviousValuesonPage_cEmployeeTaxRate_name"].ToString();
                txtb_empl_name.Text = Session["PreviousValuesonPage_cEmployeeTaxRate_name"].ToString();

                dataListGridGenerate = MyCmn.RetrieveData("sp_generate_payrollemployee_tax_hdr_dtl", "p_payroll_year", prevValues[0].ToString(), "p_empl_id", prevValues[7].ToString(), "p_user_id", Session["ep_user_id"].ToString());

                //if (txtb_fixed_rate.Text.ToUpper() == "FALSE")
                //{
                //    ddl_bir_class.SelectedValue = GetTaxRate();

                //    txtb_bir_class.Text = ddl_bir_class.SelectedItem.Text.ToString();





                //    string getTaxRate = "bir_class = '" + ddl_bir_class.SelectedValue.ToString().Trim() + "'";
                //    DataRow[] row2Tax = dataListBirTax.Select(getTaxRate);
                //    txtb_w_tax.Text = row2Tax[0]["tax_perc"].ToString();
                //    txtb_wheld_tax.Text = row2Tax[0]["tax_perc"].ToString();
                //    if (txtb_with_sworn.Text.ToUpper() == "TRUE")
                //    {
                //        txtb_bus_tax_hdr.Text = row2Tax[0]["wi_sworn_perc"].ToString();
                //        txtb_bus_tax.Text = row2Tax[0]["wi_sworn_perc"].ToString();
                //    }

                //    else if (txtb_with_sworn.Text.ToUpper() == "FALSE")
                //    {
                //        txtb_bus_tax_hdr.Text = row2Tax[0]["wo_sworn_perc"].ToString();
                //        txtb_bus_tax.Text = row2Tax[0]["wi_sworn_perc"].ToString();
                //    }

                //}


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "closeModal();", true);

                RetrieveDataListGrid();
            }

        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnGenerate_Command(object sender, CommandEventArgs e)
        {


            GenerateEmployeeTax();

        }



        private string GetTaxRate()
        {
            string bir_class = "";
            DataTable dt = MyCmn.RetrieveData("sp_getemployee_tax_rate", "par_payroll_year", txtb_payroll_year.Text.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            bir_class = dt.Rows[0]["bir_class"].ToString();
            return bir_class;
        }








        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {



            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim()+ "' AND voucher_nbr ='" + svalues[1].ToString().Trim() + "' AND payroll_year ='" + svalues[2].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            isDisable(MyCmn.CONST_EDIT, row2Edit[0]["rcrd_status"].ToString());


            InitializeTable();
            AddPrimaryKeys();
            
            txtb_empl_id.Visible                = true;
            ClearEntry();
            txtb_voucher.Text                   = row2Edit[0]["voucher_nbr"].ToString();
            //txtb_payroll_template.Text          = row2Edit[0]["payrolltemplate_code"].ToString();
            txtb_pera_ca.Text                   = DoFormat(Convert.ToDecimal(row2Edit[0]["pera_ca_amt"].ToString()));
            txtb_gsis.Text                      = DoFormat(Convert.ToDecimal(row2Edit[0]["gsis_ps"].ToString()));
            txtb_phic.Text                      = DoFormat(Convert.ToDecimal(row2Edit[0]["phic_ps"].ToString()));
            txtb_hdmf.Text                      = DoFormat(Convert.ToDecimal(row2Edit[0]["hdmf_ps"].ToString()));
            txtb_hazard.Text                    = DoFormat(Convert.ToDecimal(row2Edit[0]["hazard_pay"].ToString()));
            txtb_subs_allo.Text                 = DoFormat(Convert.ToDecimal(row2Edit[0]["subsistence_allowance"].ToString()));
            txtb_laundry.Text                   = DoFormat(Convert.ToDecimal(row2Edit[0]["laundry_allowance"].ToString()));
            txtb_gross.Text                     = DoFormat(Convert.ToDecimal(row2Edit[0]["gross_pay"].ToString()));
            txtb_wheld.Text                     = DoFormat(Convert.ToDecimal(row2Edit[0]["wtax_amt"].ToString()));
            txtb_remarks.Text                   = row2Edit[0]["remarks"].ToString();
            //txtb_payroll_template.Text          = row2Edit[0]["remarks"].ToString();
            //txtb_payroll_template.Visible       = false;
            //txtb_payroll_template.Visible       = true;
            txtb_month.Visible                  = true;
            LabelAddEdit.Text = "View Record: ";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;



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


        

        public static string DoFormat(decimal myNumber)
        {
            var s = myNumber.ToString("###,##0.00").Trim();
            return s;
           
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
                + "%' OR voucher_nbr LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR payroll_period_descr LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR payrolltemplate_descr LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR gross_pay LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%'";



            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("effective_date", typeof(System.String));

            dtSource1.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_month", typeof(System.String));
            dtSource1.Columns.Add("payroll_period_descr", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_descr", typeof(System.String));
            dtSource1.Columns.Add("pera_ca_amt", typeof(System.String));
            dtSource1.Columns.Add("gsis_ps", typeof(System.String));
            dtSource1.Columns.Add("phic_ps", typeof(System.String));
            dtSource1.Columns.Add("hdmf_ps", typeof(System.String));
            dtSource1.Columns.Add("hazard_pay", typeof(System.String));
            dtSource1.Columns.Add("subsistence_allowance", typeof(System.String));
            dtSource1.Columns.Add("laundry_allowance", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("wtax_amt", typeof(System.String));
            dtSource1.Columns.Add("remarks", typeof(System.String));
            dtSource1.Columns.Add("rcrd_status", typeof(System.String));
            dtSource1.Columns.Add("acctclass_code", typeof(System.String));




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
        

        protected void ddl_empl_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }
       

        

       


    }
}