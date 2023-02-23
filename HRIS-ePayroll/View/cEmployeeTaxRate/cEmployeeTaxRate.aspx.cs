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

namespace HRIS_ePayroll.View.cEmployeeTaxRate
{
    public partial class cEmployeeTaxRate : System.Web.UI.Page
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

        DataTable dataTaxratePercentage
        {
            get
            {
                if ((DataTable)ViewState["dataTaxratePercentage"] == null) return null;
                return (DataTable)ViewState["dataTaxratePercentage"];
            }
            set
            {
                ViewState["dataTaxratePercentage"] = value;
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

                if (Session["PreviousValuesonPage_cEmployeeTaxRate"] == null)
                    Session["PreviousValuesonPage_cEmployeeTaxRate"] = "";
                else if (Session["PreviousValuesonPage_cEmployeeTaxRate"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cEmployeeTaxRate"].ToString().Split(new char[] { ',' });
                    ddl_department.SelectedValue = prevValues[9].ToString();
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    txtb_search.Text = prevValues[13].ToString().Trim();
                    if (prevValues[12].ToString().Trim() == "Y")
                    {
                        ViewState["chkIncludeHistory"] = "Y";
                        chkIncludeHistory.Checked = true;
                    }

                    else if (prevValues[12].ToString().Trim() == "N")
                    {
                        ViewState["chkIncludeHistory"] = "N";
                        chkIncludeHistory.Checked = false;
                    }

                   
                    //if (ddl_year.SelectedValue.ToString().Trim() != "" && ddl_department.SelectedValue.ToString().Trim() != "")
                    //{
                    //    btnAdd.Visible      = true;
                    //    //btnGenerate.Visible = true;
                    //}
                    //else
                    //{
                    //    btnAdd.Visible      = false;
                    //    //btnGenerate.Visible = false;
                    //}

                    RetrieveDataListGrid();
                    up_dataListGrid.Update();
                   
                    if (txtb_search.Text.ToString().Trim() != "")
                    {
                        searchFunc();
                    }
                    
                   
                }

                ViewState["total_gross_pay"] = "0.00";
            }
        }

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            RetrieveYear();
            RetrieveBindingDepartments();
            //RetrieveWithHeldTax();
           
            ViewState["chkIncludeHistory"] = "N";

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cBIRAnnualizedTax"] = "cBIRAnnualizedTax";
            ViewState["empl_id_tax_rate"] = "";
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

            dataListGrid = MyCmn.RetrieveData("sp_payrollemployee_tax_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_include_history", ViewState["chkIncludeHistory"].ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void GenerateTaxRate(string year, string empl_id)
        {

            dataListGridGenerate = MyCmn.RetrieveData("sp_generate_payrollemployee_tax_hdr_dtl", "p_payroll_year", year, "p_empl_id", empl_id, "p_user_id", Session["ep_user_id"].ToString());
          
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private string GetTaxRate()
        {
            string bir_class = "";
            DataTable dt = MyCmn.RetrieveData("sp_getemployee_tax_rate", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            bir_class = dt.Rows[0]["bir_class"].ToString();
            return bir_class;
        }


        private void RetrieveBIRclass()
        {
            ddl_bir_class.Items.Clear();
            dataListBirTax = MyCmn.RetrieveData("sp_jo_tax_tbl_list");

            ddl_bir_class.DataSource = dataListBirTax;
            ddl_bir_class.DataValueField = "bir_class";
            ddl_bir_class.DataTextField = "bir_class_descr";
            ddl_bir_class.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_bir_class.Items.Insert(0, li);

        }

        //private void RetrieveWithHeldTax()
        //{
        //    ddl_w_held.Items.Clear();
        //    dataTaxratePercentage = MyCmn.RetrieveData("sp_taxrate_percentage_tbl_list");

        //    ddl_w_held.DataSource = dataTaxratePercentage;
        //    ddl_w_held.DataValueField = "taxrate_percentage";
        //    ddl_w_held.DataTextField = "taxrate_description";
        //    ddl_w_held.DataBind();
        //    ListItem li = new ListItem("0 Percent", "0");
        //    ddl_w_held.Items.Insert(0, li);

        //    ddl_business_tax.Items.Clear();

        //    ddl_business_tax.DataSource = dataTaxratePercentage;
        //    ddl_business_tax.DataValueField = "taxrate_percentage";
        //    ddl_business_tax.DataTextField = "taxrate_description";
        //    ddl_business_tax.DataBind();
        //    ListItem li1 = new ListItem("0 Percent", "0");
        //    ddl_business_tax.Items.Insert(0, li);

        //    ddl_vat.Items.Clear();

        //    ddl_vat.DataSource = dataTaxratePercentage;
        //    ddl_vat.DataValueField = "taxrate_percentage";
        //    ddl_vat.DataTextField = "taxrate_description";
        //    ddl_vat.DataBind();
        //    ListItem li2 = new ListItem("0 Percent", "0");
        //    ddl_vat.Items.Insert(0, li);
        //}

        

        private void RetrieveEmpl()
        {
            ddl_empl_id.Items.Clear();
            dataListEmployee = MyCmn.RetrieveData("sp_personnelnames_combolist_tax_jo", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim());

            ddl_empl_id.DataSource = dataListEmployee;
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
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






        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            //For Header
            RetrieveEmpl();
            RetrieveBIRclass();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            ClearEntry();
            ddl_empl_id.Visible = true;
            txtb_empl_name.Visible = false;

            txtb_effective_date.Visible = true;
            txtb_effective_date_hid.Visible = false;


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
            txtb_effective_date.Text            = "";
            UpdatePanel2.Update();
            txtb_empl_id.Text                   = "";
            ddl_bir_class.Enabled               = false;
            ddl_bir_class.SelectedValue         = "";
            ddl_fixed_rate.SelectedValue        = "";
            ddl_with_sworn.SelectedValue        = "";
            txtb_gross.Text                     = "0.00";
            ddl_deduction_status.SelectedValue  = "";
            ddl_status.SelectedValue            = "N";
            //ddl_w_held.SelectedValue            = "0";
            txtb_basic_tax.Text                 = "";
            //ddl_vat.SelectedValue               = "0";
            txtb_vat.Text                       = "0";
            //ddl_business_tax.SelectedValue      = "0";
            txtb_addl_tax.Text                  = "0";
            txtb_exmp_amt.Text                  = "0.00";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop100", "show_date();", true);
            UpdatePanel2.Update();
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("bir_class", typeof(System.String));
            dtSource.Columns.Add("with_sworn", typeof(System.String));
            dtSource.Columns.Add("fixed_rate", typeof(System.String));
            dtSource.Columns.Add("total_gross_pay", typeof(System.String));
            dtSource.Columns.Add("dedct_status", typeof(System.String));
            
            // JRV : 2020-02-21
            dtSource.Columns.Add("rcrd_status", typeof(System.String));
            dtSource.Columns.Add("user_id_created_by", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("user_id_updated_by", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));
            
            // JRV : 2020-07-27

            dtSource.Columns.Add("w_tax_perc", typeof(System.String));
            dtSource.Columns.Add("bus_tax_perc", typeof(System.String));
            dtSource.Columns.Add("vat_perc", typeof(System.String));
            dtSource.Columns.Add("exmpt_amt", typeof(System.String));
        }



        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollemployee_tax_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "effective_date" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

       


        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();

            nrow["empl_id"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["bir_class"] = string.Empty;
            nrow["with_sworn"] = string.Empty;
            nrow["fixed_rate"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }



        ////***************************************************************************
        ////  BEGIN - JVA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        ////***************************************************************************
        //protected void deleteRow_Command(object sender, CommandEventArgs e)
        //{
        //    string commandArgs = e.CommandArgument.ToString().Trim();
        //    string seq = commandArgs.ToString().Trim();
        //    Lbl_delete.InnerText = "Delete this Record?";
        //    lnkBtnYes.Visible = true;
        //    lnkBtnYes_gen.Visible = false;
        //    icon_delete.Visible = true;
        //    icon_generate.Visible = false;
        //    lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        //}


        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        //protected void btnDelete_Command(object sender, CommandEventArgs e)
        //{

        //    string[] svalues = e.CommandArgument.ToString().Split(',');
        //    string deleteExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND effective_date = '" + svalues[1].ToString().Trim() + "'";
        //    MyCmn.DeleteBackEndData("payrollemployee_tax_tbl", "WHERE " + deleteExpression);

        //    DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
        //    dataListGrid.Rows.Remove(row2Delete[0]);
        //    dataListGrid.AcceptChanges();
        //    CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
        //    up_dataListGrid.Update();
        //    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

        //}


        //***************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void generateRow_Command(object sender, CommandEventArgs e)
        {
            //string seq = commandArgs.ToString().Trim();
            Lbl_delete.InnerText = "Generate this Record";
            deleteRec1.Text = "Once generated, data will be updated automatically!";
            //lnkBtnYes.Visible = false;
            lnkBtnYes_gen.Visible = true;
            icon_generate.Visible = true;
            //icon_delete.Visible = false;
            lnkBtnYes_gen.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);



        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnGenerate_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            dataListGridGenerate = MyCmn.RetrieveData("sp_generate_payrollemployee_tax_hdr_dtl", "p_payroll_year", ddl_year.SelectedValue.ToString().Trim(),"p_empl_id", svalues[0], "p_user_id", Session["ep_user_id"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "closeModal();", true);
            RetrieveDataListGrid();
        }


        //*************************************************************************
        //  BEGIN - JVA- 07/20/2020 - Toogle Disable All for Approve
        //*************************************************************************

        private void ToogleApproveDisable()
        {
            ddl_fixed_rate.Enabled       = false;
            txtb_basic_tax.Enabled       = false;
            //ddl_w_held.Enabled           = false;
            ddl_with_sworn.Enabled       = false;
            //ddl_business_tax.Enabled     = false;
            txtb_addl_tax.Enabled        = false;
            ddl_bir_class.Enabled        = false;
            ddl_deduction_status.Enabled = false;
            //ddl_vat.Enabled              = false;
            txtb_vat.Enabled             = false;
            txtb_exmp_amt.Enabled        = false;
            ddl_status.Enabled           = false;
            btnsave.Visible              = false;
        }

        //*************************************************************************
        //  BEGIN - JVA- 07/20/2020 - Toogle Disable All for Approve
        //*************************************************************************

        private void ToogleApproveEnable()
        {
            ddl_fixed_rate.Enabled          = false;
            //ddl_w_held.Enabled              = false;
            txtb_basic_tax.Enabled          = false;
            ddl_with_sworn.Enabled          = false;
            //ddl_business_tax.Enabled        = false;
            txtb_addl_tax.Enabled           = false;
            ddl_bir_class.Enabled           = false;
            ddl_deduction_status.Enabled    = false;
            //ddl_vat.Enabled                 = false;
            txtb_vat.Enabled                = false;
            txtb_exmp_amt.Enabled           = false;
            btnsave.Visible                 = true;
            ddl_status.Enabled              = true;
        }




        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {


            ToogleApproveEnable();
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim()+ "' AND effective_date ='" + svalues[1].ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
           

            InitializeTable();
            AddPrimaryKeys();
            RetrieveBIRclass();
            DataRow nrow            = dtSource.NewRow();
            nrow["action"]          = 2;
            nrow["retrieve"]        = true;
            nrow["empl_id"]         = String.Empty;
            nrow["effective_date"]  = String.Empty;
            dtSource.Rows.Add(nrow);

            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;

            ddl_empl_id.Visible = false;
            txtb_empl_name.Visible = true;
            txtb_empl_id.Visible = true;
            txtb_effective_date_hid.Visible = true;
            txtb_effective_date.Visible = false;
            ClearEntry();
            txtb_empl_id.Text               = row2Edit[0]["empl_id"].ToString();
            ddl_status.SelectedValue        = row2Edit[0]["rcrd_status"].ToString();
            txtb_effective_date_hid.Text    = row2Edit[0]["effective_date"].ToString();
            txtb_empl_name.Text             = row2Edit[0]["employee_name"].ToString();
            ddl_bir_class.SelectedValue     = row2Edit[0]["bir_class"].ToString();
            ddl_fixed_rate.SelectedValue    = row2Edit[0]["fixed_rate"].ToString();
            ddl_with_sworn.SelectedValue    = row2Edit[0]["with_sworn"].ToString();
            ddl_deduction_status.SelectedValue = row2Edit[0]["dedct_status"].ToString();
            ViewState["total_gross_pay"]    = double.Parse(row2Edit[0]["total_gross_pay"].ToString().Trim()).ToString("###,##0.00");
            ViewState["bir_class"]          = row2Edit[0]["bir_class"].ToString().Trim();
            txtb_gross.Text                 = double.Parse(row2Edit[0]["total_gross_pay"].ToString().Trim()).ToString("###,##0.00");
            txtb_exmp_amt.Text              = double.Parse(row2Edit[0]["exmpt_amt"].ToString().Trim()).ToString("###,##0.00");
            //ddl_w_held.SelectedValue      = row2Edit[0]["w_tax_perc"].ToString().Trim();
            txtb_basic_tax.Text             = double.Parse(row2Edit[0]["w_tax_perc"].ToString().Trim()).ToString("###,##0.00");
            //ddl_business_tax.SelectedValue = row2Edit[0]["bus_tax_perc"].ToString().Trim();
            txtb_addl_tax.Text              = double.Parse(row2Edit[0]["bus_tax_perc"].ToString().Trim()).ToString("###,##0.00");
            //ddl_vat.SelectedValue           = row2Edit[0]["vat_perc"].ToString().Trim();
            txtb_vat.Text                   = double.Parse(row2Edit[0]["vat_perc"].ToString().Trim()).ToString("###,##0.00");

            if (row2Edit[0]["fixed_rate"].ToString().ToUpper() == "TRUE" || row2Edit[0]["fixed_rate"].ToString().ToUpper() == "1")
            {
                ddl_bir_class.Enabled = false;
            }

            else
            {
                ddl_bir_class.Enabled = false;
            }

            if (row2Edit[0]["rcrd_status"].ToString() == "A" || row2Edit[0]["rcrd_status"].ToString() == "R")
            {
                ToogleApproveDisable();
            }

            UpdatePanel2.Update();
            LabelAddEdit.Text = "Edit Record: ";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;

            ViewState["user_id_created_by"] = row2Edit[0]["user_id_created_by"].ToString().Trim();
            ViewState["created_dttm"]       = row2Edit[0]["created_dttm"].ToString().Trim();

            FieldValidationColorChanged(false, "ALL");



            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        public static string DoFormat(decimal myNumber)
        {
            var s = myNumber.ToString("###,##0.00").Trim();
            return s;

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




        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Save New Record/Edited Record to back end DB
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
                    double empt_amt;

                    if (txtb_exmp_amt.Text.ToString().Trim() == "")
                    {
                        empt_amt = 0;
                    }

                    else
                    {
                        empt_amt = double.Parse(txtb_exmp_amt.Text.ToString().Trim());
                    }
                    
                    
                    dtSource.Rows[0]["empl_id"]         = txtb_empl_id.Text.Trim();
                    dtSource.Rows[0]["effective_date"]  = txtb_effective_date.Text.Trim();

                    if (ddl_fixed_rate.SelectedValue.ToString().Trim().ToUpper() == "TRUE")
                    {
                        dtSource.Rows[0]["bir_class"] = ddl_bir_class.SelectedValue.Trim().ToString();
                    }

                    else if (ddl_fixed_rate.SelectedValue.ToString().Trim().ToUpper() == "FALSE")
                    {
                        dtSource.Rows[0]["bir_class"] = GetTaxRate();
                    }

                    dtSource.Rows[0]["with_sworn"]             = Convert.ToBoolean(ddl_with_sworn.SelectedValue.Trim().ToString());
                    dtSource.Rows[0]["fixed_rate"]             = Convert.ToBoolean(ddl_fixed_rate.SelectedValue.Trim().ToString());
                    dtSource.Rows[0]["total_gross_pay"]        = double.Parse(txtb_gross.Text.ToString().Trim());
                    dtSource.Rows[0]["dedct_status"]           = Convert.ToBoolean(ddl_deduction_status.SelectedValue.Trim().ToString());
                    dtSource.Rows[0]["rcrd_status"]            = ddl_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["user_id_created_by"]     = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["created_dttm"]           = DateTime.Now;
                    dtSource.Rows[0]["user_id_updated_by"]     = "";
                    dtSource.Rows[0]["updated_dttm"]           = Convert.ToDateTime("1900-01-01");
                    //dtSource.Rows[0]["w_tax_perc"]             = ddl_w_held.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["w_tax_perc"]             = txtb_basic_tax.Text.ToString().Trim();
                    //dtSource.Rows[0]["bus_tax_perc"]           = ddl_business_tax.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["bus_tax_perc"]           = txtb_addl_tax.Text.ToString().Trim();
                    //dtSource.Rows[0]["vat_perc"]               = ddl_vat.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["vat_perc"]               = txtb_vat.Text.ToString().Trim();
                    dtSource.Rows[0]["exmpt_amt"]              = empt_amt;
                    scriptInsertUpdate                         = MyCmn.get_insertscript(dtSource);

                }

                else if (saveRecord == MyCmn.CONST_EDIT)
                {


                    double empt_amt;

                    if (txtb_exmp_amt.Text.ToString().Trim() == "")
                    {
                        empt_amt = 0;
                    }

                    else
                    {
                        empt_amt = double.Parse(txtb_exmp_amt.Text.ToString().Trim());
                    }

                    dtSource.Rows[0]["empl_id"]         = txtb_empl_id.Text.Trim();
                    dtSource.Rows[0]["effective_date"]  = txtb_effective_date_hid.Text.Trim();

                    if (ddl_fixed_rate.SelectedValue.ToString().Trim().ToUpper() == "TRUE")
                    {
                        dtSource.Rows[0]["bir_class"] = ddl_bir_class.SelectedValue.Trim().ToString();
                    }

                    else if (ddl_fixed_rate.SelectedValue.ToString().Trim().ToUpper() == "FALSE")
                    {
                        dtSource.Rows[0]["bir_class"] = GetTaxRate();
                    }

                   
                    dtSource.Rows[0]["with_sworn"]      = Convert.ToBoolean(ddl_with_sworn.SelectedValue.Trim().ToString());
                    dtSource.Rows[0]["fixed_rate"]      = Convert.ToBoolean(ddl_fixed_rate.SelectedValue.Trim().ToString());
                    dtSource.Rows[0]["total_gross_pay"] = double.Parse(txtb_gross.Text.ToString().Trim());
                    dtSource.Rows[0]["dedct_status"]    = Convert.ToBoolean(ddl_deduction_status.SelectedValue.Trim().ToString());

                    dtSource.Rows[0]["rcrd_status"]     = ddl_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["user_id_updated_by"] = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_dttm"]    = DateTime.Now;

                    dtSource.Rows[0]["user_id_created_by"] = ViewState["user_id_created_by"].ToString();
                    dtSource.Rows[0]["created_dttm"]    = Convert.ToDateTime(ViewState["created_dttm"].ToString());


                    //dtSource.Rows[0]["w_tax_perc"]      = ddl_w_held.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["w_tax_perc"]      = txtb_basic_tax.Text.ToString().Trim();
                    //dtSource.Rows[0]["bus_tax_perc"]    = ddl_business_tax.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["bus_tax_perc"]    = txtb_addl_tax.Text.ToString().Trim();
                    //dtSource.Rows[0]["vat_perc"]        = ddl_vat.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["vat_perc"]        = txtb_vat.Text.ToString().Trim();
                    dtSource.Rows[0]["exmpt_amt"]       = empt_amt;




                    scriptInsertUpdate = MyCmn.updatescript(dtSource);


                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {

                    double empt_amt;

                    if (txtb_exmp_amt.Text.ToString().Trim() == "")
                    {
                        empt_amt = 0;
                    }

                    else
                    {
                        empt_amt = double.Parse(txtb_exmp_amt.Text.ToString().Trim());
                    }

                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;
                    if (msg.Substring(0, 1) == "X")
                    {
                        LblRequired1.Text = "Already Exists!";
                        ddl_empl_id.BorderColor = Color.Red;
                        return;
                    }
                    

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();

                     

                        nrow["employee_name"]         = ddl_empl_id.SelectedItem.ToString().Trim();
                        nrow["empl_id"]               = txtb_empl_id.Text.Trim();

                        if (ddl_fixed_rate.SelectedValue.ToString().Trim().ToUpper() == "TRUE")
                        {
                            nrow["bir_class"] = ddl_bir_class.SelectedValue.Trim().ToString();
                        }

                        else if (ddl_fixed_rate.SelectedValue.ToString().Trim().ToUpper() == "FALSE")
                        {
                            nrow["bir_class"] = GetTaxRate();
                            ddl_bir_class.SelectedValue = nrow["bir_class"].ToString();

                        }

                        //if (ddl_with_sworn.SelectedValue.ToString().Trim().ToUpper() == "TRUE")
                        //{
                        //    nrow["wi_sworn_perc"] = txtb_business_tax.Text;
                        //}

                        //else if (ddl_with_sworn.SelectedValue.ToString().Trim().ToUpper() == "FALSE")
                        //{
                        //    nrow["wo_sworn_perc"] = txtb_business_tax.Text;
                        //}



                        //nrow["tax_perc"]              = txtb_wheld_tax.Text.ToString(); 
                        nrow["bir_class_descr"]       = ddl_bir_class.SelectedItem.Text.Trim().ToString();
                        nrow["with_sworn"]            = Convert.ToBoolean(ddl_with_sworn.SelectedValue.Trim().ToString());
                        nrow["fixed_rate"]            = Convert.ToBoolean(ddl_fixed_rate.SelectedValue.Trim().ToString());
                        nrow["with_sworn_descr"]      = ddl_with_sworn.SelectedItem.Text.Trim().ToString();
                        nrow["fixed_rate_descr"]      = ddl_fixed_rate.SelectedItem.Text.Trim().ToString();
                        nrow["total_gross_pay"]       = DoFormat(Convert.ToDecimal(txtb_gross.Text.ToString().Trim()));
                        nrow["effective_date"]        = txtb_effective_date.Text.ToString().Trim();
                        nrow["dedct_status"]          = Convert.ToBoolean(ddl_deduction_status.SelectedValue.Trim().ToString());
                        nrow["rcrd_status"]           = ddl_status.SelectedValue.ToString().Trim();
                        nrow["rcrd_status_descr"]     = ddl_status.SelectedItem.Text.ToString().Trim();

                        nrow["user_id_created_by"]    = Session["ep_user_id"].ToString();
                        nrow["created_dttm"]          = DateTime.Now;

                        nrow["user_id_updated_by"]    = "";
                        nrow["updated_dttm"]          = Convert.ToDateTime("1900-01-01");

                        //nrow["w_tax_perc"]            = ddl_w_held.SelectedValue.ToString().Trim();
                        nrow["w_tax_perc"]            = txtb_basic_tax.Text.ToString().Trim();
                        //nrow["bus_tax_perc"]          = ddl_business_tax.SelectedValue.ToString().Trim();
                        nrow["bus_tax_perc"]          = txtb_addl_tax.Text.ToString().Trim();
                        //nrow["vat_perc"]              = ddl_vat.SelectedValue.ToString().Trim();
                        nrow["vat_perc"]              = txtb_vat.Text.ToString().Trim();
                        nrow["exmpt_amt"]             = empt_amt;

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        up_dataListGrid.Update();


                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;

                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "'AND effective_date = '" + txtb_effective_date_hid.Text.ToString().Trim() + "'";

                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                      
                      

                        row2Edit[0]["employee_name"]    = txtb_empl_name.Text.Trim();
                        row2Edit[0]["empl_id"]          = txtb_empl_id.Text.Trim();

                        if (ddl_fixed_rate.SelectedValue.ToString().Trim().ToUpper() == "TRUE")
                        {
                            row2Edit[0]["bir_class"] = ddl_bir_class.SelectedValue.Trim().ToString();
                        }

                        else if (ddl_fixed_rate.SelectedValue.ToString().Trim().ToUpper() == "FALSE")
                        {
                            row2Edit[0]["bir_class"] = GetTaxRate();
                            ddl_bir_class.SelectedValue = row2Edit[0]["bir_class"].ToString();

                        }

                        //if (ddl_with_sworn.SelectedValue.ToString().Trim().ToUpper() == "TRUE")
                        //{
                        //    row2Edit[0]["wi_sworn_perc"] = txtb_business_tax.Text;
                        //}

                        //else if (ddl_with_sworn.SelectedValue.ToString().Trim().ToUpper() == "FALSE")
                        //{
                        //    row2Edit[0]["wo_sworn_perc"] = txtb_business_tax.Text;
                        //}

                        //row2Edit[0]["tax_perc"]         = txtb_wheld_tax.Text.ToString();
                        row2Edit[0]["bir_class_descr"]  = ddl_bir_class.SelectedItem.Text.Trim().ToString();
                        row2Edit[0]["with_sworn"]       = Convert.ToBoolean(ddl_with_sworn.SelectedValue.Trim().ToString());
                        row2Edit[0]["fixed_rate"]       = Convert.ToBoolean(ddl_fixed_rate.SelectedValue.Trim().ToString());
                        row2Edit[0]["with_sworn_descr"] = ddl_with_sworn.SelectedItem.Text.Trim().ToString();
                        row2Edit[0]["fixed_rate_descr"] = ddl_fixed_rate.SelectedItem.Text.Trim().ToString();
                        row2Edit[0]["total_gross_pay"]  = DoFormat(Convert.ToDecimal(txtb_gross.Text.ToString().Trim()));
                        row2Edit[0]["effective_date"]   = txtb_effective_date_hid.Text.ToString().Trim();
                        row2Edit[0]["dedct_status"]     = Convert.ToBoolean(ddl_deduction_status.SelectedValue.Trim().ToString());
                        row2Edit[0]["rcrd_status"]      = ddl_status.SelectedValue.ToString().Trim();
                        row2Edit[0]["rcrd_status_descr"] = ddl_status.SelectedItem.Text.ToString().Trim();
                        row2Edit[0]["user_id_updated_by"] = Session["ep_user_id"].ToString();
                        row2Edit[0]["updated_dttm"]     = DateTime.Now;

                        row2Edit[0]["user_id_created_by"] = ViewState["user_id_created_by"].ToString();
                        row2Edit[0]["created_dttm"]     = Convert.ToDateTime(ViewState["created_dttm"].ToString());

                        //row2Edit[0]["w_tax_perc"]       = ddl_w_held.SelectedValue.ToString().Trim();
                        row2Edit[0]["w_tax_perc"]       = txtb_basic_tax.Text.ToString().Trim();
                        //row2Edit[0]["bus_tax_perc"]     = ddl_business_tax.SelectedValue.ToString().Trim();
                        row2Edit[0]["bus_tax_perc"]     = txtb_addl_tax.Text.ToString().Trim();
                        //row2Edit[0]["vat_perc"]         = ddl_vat.SelectedValue.ToString().Trim();
                        row2Edit[0]["vat_perc"]         = txtb_vat.Text.ToString().Trim();
                        row2Edit[0]["exmpt_amt"]        = empt_amt;

                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                        up_dataListGrid.Update();

                    }
                    GenerateTaxRate(ddl_year.SelectedValue.ToString().Trim(),txtb_empl_id.Text.Trim());
                    
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
        private void searchFunc()
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR employee_name LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR bir_class LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR effective_date LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR bir_class_descr LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR with_sworn_descr LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR fixed_rate_descr LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR rcrd_status_descr LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR total_gross_pay LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%'";



            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("bir_class", typeof(System.String));
            dtSource1.Columns.Add("bir_class_descr", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("with_sworn", typeof(System.String));
            dtSource1.Columns.Add("fixed_rate", typeof(System.String));
            dtSource1.Columns.Add("with_sworn_descr", typeof(System.String));
            dtSource1.Columns.Add("fixed_rate_descr", typeof(System.String));
            dtSource1.Columns.Add("wo_sworn_perc", typeof(System.String));
            dtSource1.Columns.Add("tax_perc", typeof(System.String));
            dtSource1.Columns.Add("rcrd_status_descr", typeof(System.String));
            dtSource1.Columns.Add("total_gross_pay", typeof(System.String));
            dtSource1.Columns.Add("user_id_created_by", typeof(System.String));
            dtSource1.Columns.Add("created_dttm", typeof(System.String));
            dtSource1.Columns.Add("user_id_updated_by", typeof(System.String));
            dtSource1.Columns.Add("updated_dttm", typeof(System.String));

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
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {

            searchFunc();

            
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
        
        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");

            if (ddl_empl_id.SelectedValue.ToString().Trim() == "" && ViewState["AddEdit_Mode"].ToString() == "ADD")
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdatetime(txtb_effective_date.Text) == false && ViewState["AddEdit_Mode"].ToString() == "ADD")
            {
                if (txtb_effective_date.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_effective_date");
                    txtb_effective_date.Focus();
                    validatedSaved = false;
                }
                else if (txtb_effective_date.Text != "")
                {
                    FieldValidationColorChanged(true, "invalid-date");
                    txtb_effective_date.Focus();
                    validatedSaved = false;
                }


            }

          

            if (ddl_bir_class.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_bir_class");
                ddl_bir_class.Focus();
                validatedSaved = false;
            }

            if (ddl_fixed_rate.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_fixed_rate");
                ddl_fixed_rate.Focus();
                validatedSaved = false;
            }

            if (ddl_with_sworn.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_with_sworn");
                ddl_with_sworn.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_gross) == false)
            {
                if (txtb_gross.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_gross");
                    txtb_gross.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_gross");
                    txtb_gross.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_exmp_amt) == false)
            {
                if (txtb_exmp_amt.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_exmp_amt");
                    txtb_exmp_amt.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_exmp_amt");
                    txtb_exmp_amt.Focus();
                    validatedSaved = false;
                }
            }



            if (ddl_deduction_status.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_deduction_status");
                ddl_deduction_status.Focus();
                validatedSaved = false;
            }



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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop0", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "txtb_effective_date":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_effective_date.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "ddl_bir_class":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            ddl_bir_class.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "ddl_fixed_rate":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            ddl_fixed_rate.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "ddl_with_sworn":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            ddl_with_sworn.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "invalid-date":
                        {
                            LblRequired3.Text = "Invalid Date!";
                            txtb_effective_date.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop5", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "txtb_gross":
                        {
                            LblRequired7.Text = MyCmn.CONST_RQDFLD;
                            txtb_gross.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop6", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "invalid-txtb_gross":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_gross.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop7", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "txtb_exmp_amt":
                        {
                            LblRequired9.Text = MyCmn.CONST_RQDFLD;
                            txtb_exmp_amt.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop9", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "invalid-txtb_exmp_amt":
                        {
                            LblRequired9.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_exmp_amt.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop10", "show_date();", true);
                            UpdatePanel2.Update();
                            break;
                        }

                    case "ddl_deduction_status":
                        {
                            LblRequired8.Text = MyCmn.CONST_RQDFLD;
                            ddl_deduction_status.BorderColor = Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop8", "show_date();", true);
                            UpdatePanel2.Update();
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
                            LblRequired3.Text = "";
                            LblRequired4.Text = "";
                            LblRequired5.Text = "";
                            LblRequired6.Text = "";
                            LblRequired7.Text = "";
                            LblRequired8.Text = "";
                            LblRequired9.Text = "";
                            ddl_with_sworn.BorderColor = Color.LightGray;
                            ddl_fixed_rate.BorderColor = Color.LightGray;
                            ddl_bir_class.BorderColor = Color.LightGray;
                            txtb_effective_date.BorderColor = Color.LightGray;
                            ddl_empl_id.BorderColor = Color.LightGray;
                            txtb_gross.BorderColor = Color.LightGray;
                            txtb_exmp_amt.BorderColor = Color.LightGray;
                            ddl_deduction_status.BorderColor = Color.LightGray;
                            UpdatePanel2.Update();
                            break;

                        }

                }
            }
        }

        protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_year.SelectedValue.ToString().Trim() != "" && ddl_department.SelectedValue.ToString().Trim() != "")
            //{
            //    btnAdd.Visible = true;
            //    //btnGenerate.Visible = true;
            //}
            //else
            //{
            //    btnAdd.Visible = false;
            //    //btnGenerate.Visible = false;
            //}
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_year.SelectedValue.ToString().Trim() != "" && ddl_department.SelectedValue.ToString().Trim() != "")
            //{
            //    btnAdd.Visible = true;
            //    //btnGenerate.Visible = true;
            //}
            //else
            //{
            //    btnAdd.Visible = false;
            //    //btnGenerate.Visible = false;
            //}
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (ddl_year.SelectedValue.ToString().Trim() != "" && ddl_department.SelectedValue.ToString().Trim() != "")
            //{
            //    btnAdd.Visible = true;
            //    //btnGenerate.Visible = true;
            //}
            //else
            //{
            //    btnAdd.Visible = false;
            //    //btnGenerate.Visible = false;
            //}
            RetrieveDataListGrid();
            up_dataListGrid.Update();

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
        protected void ddl_empl_id_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_empl_id.SelectedValue.ToString().Trim() != "")
            {
                txtb_empl_id.Text = ddl_empl_id.SelectedValue.ToString().Trim();
                string addExpression            = "empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'";
                DataRow[] row2Add               = dataListEmployee.Select(addExpression);
                txtb_gross.Text                 = double.Parse(row2Add[0]["total_gross_pay"].ToString().Trim()).ToString("###,##0.00");
                ViewState["total_gross_pay"]    = double.Parse(row2Add[0]["total_gross_pay"].ToString().Trim()).ToString("###,##0.00");
                ViewState["bir_class"]          = row2Add[0]["bir_class"].ToString().Trim();
                ddl_bir_class.SelectedValue     = row2Add[0]["bir_class"].ToString().Trim();
            }
            else
            {
                ClearEntry();
                ViewState["total_gross_pay"] = "0.00";
                ViewState["bir_class"] = "";
            }
        }

        protected void ddl_fixed_rate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_fixed_rate.SelectedValue.ToString().ToUpper() == "TRUE")
            {
                txtb_gross.Enabled = false;
                ddl_bir_class.Enabled = false;
                //ddl_w_held.Enabled          = false;
                txtb_basic_tax.Enabled = false;
                //ddl_business_tax.Enabled    = false;
                txtb_addl_tax.Enabled = false;
                //ddl_vat.Enabled = false;
            }

            else
            {
                txtb_gross.Enabled = false;
                ddl_bir_class.Enabled = false;
                ddl_bir_class.Enabled = false;
                //ddl_w_held.Enabled          = false;
                txtb_basic_tax.Enabled = false;
                //ddl_business_tax.Enabled    = false;
                txtb_addl_tax.Enabled = false;
                //ddl_vat.Enabled = false;
                txtb_gross.Text = ViewState["total_gross_pay"].ToString();
                ddl_bir_class.SelectedValue = GetTaxRate();

                string showTaxRate = "bir_class = '" + ddl_bir_class.SelectedValue.ToString().Trim() + "'";

                DataRow[] birTaxRow = dataListBirTax.Select(showTaxRate);

                if (birTaxRow.Length > 0)
                {
                    if (ddl_with_sworn.SelectedValue.ToString().ToUpper() == "TRUE")
                    {
                        //ddl_w_held.SelectedValue        = birTaxRow[0]["wi_sworn_perc"].ToString().Trim();
                        txtb_basic_tax.Text = birTaxRow[0]["wi_sworn_perc"].ToString().Trim();
                        //ddl_business_tax.SelectedValue  = birTaxRow[0]["tax_perc"].ToString().Trim();
                        //ddl_business_tax.SelectedValue = birTaxRow[0]["tax_perc"].ToString().Trim();
                        //ddl_vat.SelectedValue = birTaxRow[0]["vat_perc"].ToString().Trim();
                    }

                    else
                    {
                        //ddl_w_held.SelectedValue = birTaxRow[0]["wo_sworn_perc"].ToString().Trim(); ;
                        txtb_basic_tax.Text = birTaxRow[0]["wo_sworn_perc"].ToString().Trim();
                        //ddl_business_tax.SelectedValue = birTaxRow[0]["tax_perc"].ToString().Trim();
                        //ddl_vat.SelectedValue = birTaxRow[0]["vat_perc"].ToString().Trim();

                    }

                }

                else
                {
                    //ddl_w_held.SelectedValue        = "0";
                    txtb_basic_tax.Text = "0";
                    //ddl_business_tax.SelectedValue = "0";
                    //ddl_vat.SelectedValue = "0";
                }

            }
        }

        protected void ddl_bir_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            string showTaxRate = "bir_class = '" + ddl_bir_class.SelectedValue.ToString().Trim() + "'";

            DataRow[] birTaxRow = dataListBirTax.Select(showTaxRate);

            if (birTaxRow.Length > 0)
            {
                if (ddl_with_sworn.SelectedValue.ToString().ToUpper() == "TRUE")
                {
                    //ddl_w_held.SelectedValue        = birTaxRow[0]["wi_sworn_perc"].ToString().Trim();
                    txtb_basic_tax.Text = birTaxRow[0]["wi_sworn_perc"].ToString().Trim();
                    //ddl_business_tax.SelectedValue = birTaxRow[0]["tax_perc"].ToString().Trim();
                    //ddl_vat.SelectedValue = birTaxRow[0]["vat_perc"].ToString().Trim();
                }

                else
                {
                    //ddl_w_held.SelectedValue        = birTaxRow[0]["wo_sworn_perc"].ToString().Trim();
                    txtb_basic_tax.Text = birTaxRow[0]["wo_sworn_perc"].ToString().Trim();
                    //ddl_business_tax.SelectedValue = birTaxRow[0]["tax_perc"].ToString().Trim();
                    //ddl_vat.SelectedValue = birTaxRow[0]["vat_perc"].ToString().Trim();
                }

            }

            else
            {
                //ddl_w_held.SelectedValue        = "0";
                txtb_basic_tax.Text = "0";
                //ddl_business_tax.SelectedValue = "0";
                //ddl_vat.SelectedValue = "0";
            }

        }

        protected void ddl_with_sworn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string showTaxRate = "bir_class = '" + ddl_bir_class.SelectedValue.ToString().Trim() + "'";

            DataRow[] birTaxRow = dataListBirTax.Select(showTaxRate);
            if (ddl_with_sworn.SelectedValue.ToString() != "")
            {
                if (birTaxRow.Length > 0)
                {
                    if (ddl_with_sworn.SelectedValue.ToString().ToUpper() == "TRUE")
                    {
                        //ddl_w_held.SelectedValue        = birTaxRow[0]["wi_sworn_perc"].ToString().Trim();
                        txtb_basic_tax.Text = birTaxRow[0]["wi_sworn_perc"].ToString().Trim();
                        //ddl_business_tax.SelectedValue = birTaxRow[0]["tax_perc"].ToString().Trim();
                        //ddl_vat.SelectedValue = birTaxRow[0]["vat_perc"].ToString().Trim();
                    }

                    else
                    {
                        //ddl_w_held.SelectedValue = birTaxRow[0]["wo_sworn_perc"].ToString().Trim();
                        txtb_basic_tax.Text = birTaxRow[0]["wo_sworn_perc"].ToString().Trim();
                        //ddl_business_tax.SelectedValue = birTaxRow[0]["tax_perc"].ToString().Trim();
                        //ddl_vat.SelectedValue = birTaxRow[0]["vat_perc"].ToString().Trim();
                    }

                }

                else
                {
                    //ddl_w_held.SelectedValue        = "0";
                    txtb_basic_tax.Text = "0";
                    //ddl_business_tax.SelectedValue = "0";
                    //ddl_vat.SelectedValue = "0";
                }
            }

            else
            {
                txtb_basic_tax.Text = "0";
                //ddl_business_tax.SelectedValue = "0";
                //ddl_vat.SelectedValue = "0";
            }
        }

        protected void imgbtn_add_empl_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string showExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND effective_date = '" + svalues[1].ToString().Trim() + "'";
            DataRow[] row2Show = dataListGrid.Select(showExpression);
            string url = "../cEmployeeTaxRateDetails/cEmployeeTaxRateDetails.aspx";
            

            Session["PreviousValuesonPage_cEmployeeTaxRate"] = ddl_year.SelectedValue.ToString() + "," + ddl_department.SelectedItem.Text.ToString().Trim() + "," + row2Show[0]["empl_id"].ToString().Trim() + ","  + double.Parse(row2Show[0]["w_tax_perc"].ToString().Trim()).ToString("###,##0.00") + "," + double.Parse(row2Show[0]["bus_tax_perc"].ToString().Trim()).ToString("###,##0.00") + "," + row2Show[0]["effective_date"].ToString().Trim() + "," + row2Show[0]["position_title1"].ToString().Trim() + "," + row2Show[0]["empl_id"].ToString().Trim() + "," + row2Show[0]["bir_class_descr"].ToString().Trim() + "," + ddl_department.SelectedValue.ToString() + "," + row2Show[0]["with_sworn"].ToString().Trim().ToUpper() + "," + row2Show[0]["fixed_rate"].ToString().Trim().ToUpper() + "," + ViewState["chkIncludeHistory"].ToString().Trim() + "," + txtb_search.Text.ToString().Trim();
            Session["PreviousValuesonPage_cEmployeeTaxRate_name"] = row2Show[0]["employee_name"].ToString().Trim();
            if (url != "")
            {
                Response.Redirect(url);
            }
        }


        //***************************************************************************
        //  BEGIN - JRV- 04/06/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void imgbtn_print_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];
            string effective_date = commandArgs[1];
        }

        //***********************************************
        //  BEGIN - JRV- 05/21/2019 - Include History
        //***********************************************
        protected void chkIncludeHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncludeHistory.Checked)
            {
                ViewState["chkIncludeHistory"] = "Y";
            }
            else
            {
                ViewState["chkIncludeHistory"] = "N";
            }
            RetrieveDataListGrid();
        }

    }
}