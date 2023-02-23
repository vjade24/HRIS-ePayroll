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

namespace HRIS_ePayroll.View.cEmplTaxWithHeld
{
    public partial class cEmplTaxWithHeld : System.Web.UI.Page
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

                if (Session["PreviousValuesonPage_cEmployeeTaxRateRECE"] == null)
                    Session["PreviousValuesonPage_cEmployeeTaxRateRECE"] = "";
                else if (Session["PreviousValuesonPage_cEmployeeTaxRateRECE"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cEmployeeTaxRateRECE"].ToString().Split(new char[] { ',' });
                    ddl_empl_type.SelectedValue = prevValues[3].ToString();
                    ddl_year.SelectedValue = prevValues[1].ToString();
                    ddl_department.SelectedValue = prevValues[7].ToString();
                    txtb_search.Text = prevValues[6].ToString().Trim();

                    if (ddl_year.SelectedValue.ToString().Trim() != "" && ddl_department.SelectedValue.ToString().Trim() != "")
                    {
                        btnAdd.Visible = true;
                        //btnGenerate.Visible = true;
                    }
                    else
                    {
                        btnAdd.Visible = false;
                        //btnGenerate.Visible = false;
                    }

                    RetrieveDataListGrid();
                    up_dataListGrid.Update();

                    if (txtb_search.Text.ToString().Trim() != "")
                    {
                        searchFunc();
                    }


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
            RetrieveBindingDepartments();
            RetrieveEmploymentType();
            RetrieveDataListGrid();
            btnAdd.Visible = false;
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_empl_taxwithheld_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_include_history", ViewState["chkIncludeHistory"].ToString().Trim());

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Employment Type
        //*************************************************************************
        private void RetrieveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list3");

            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_type.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Employee Nmae
        //*************************************************************************
        private void RetrieveEmpl()
        {
            ddl_empl_name.Items.Clear();
            dtSource_for_names = MyCmn.RetrieveData("sp_personnelnames_combolist_empl_taxwithheld", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim());

            ddl_empl_name.DataSource = dtSource_for_names;
            ddl_empl_name.DataValueField = "empl_id";
            ddl_empl_name.DataTextField = "employee_name";
            ddl_empl_name.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_name.Items.Insert(0, li);
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
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_department.Items.Insert(0, li);
            
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
        //  BEGIN - VJA- 01/17/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            //For Header
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
            FieldValidationColorChanged(false, "ALL");

            ddl_empl_name.Enabled = true;
            ddl_empl_name.Visible = true;
            txtb_empl_name.Visible = false;

            ddl_status.SelectedValue = "N";
            ddl_status.Enabled = true;
            txtb_effective_date.Enabled = true;
            txtb_tax_rate.Enabled = true;
            txtb_wtax_amt.Enabled = true;


            txtb_effective_date.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            //ddl_empl_name.SelectedIndex = 0;
            txtb_empl_id.Text = "";
            txtb_tax_rate.Text = "0";
            txtb_wtax_amt.Text = "0.00";
            txtb_position.Text = "";
            lbl_rate_descr.Text = "Rate Description :";
            txtb_rate_amount.Text = "0.00";
            
            txtb_effective_date.Text = "";
            // ddl_year.SelectedIndex = 0;

            UpdatePanel16.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popshowdate", "show_date();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("tax_rate", typeof(System.String));
            dtSource.Columns.Add("wtax_amt", typeof(System.String));
            // VJA : 2020-02-21
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("rcrd_status", typeof(System.String));
            dtSource.Columns.Add("user_id_created_by", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("user_id_updated_by", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));

        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add Primary Key Field to datasource- Insert Table : empl_taxwithheld_tbl
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "empl_taxwithheld_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] {"empl_id", "payroll_year","effective_date" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["payroll_year"] = string.Empty;
            nrow["tax_rate"] = string.Empty;
            nrow["wtax_amt"] = string.Empty;
            // VJA : 2020-02-21
            nrow["effective_date"]       = string.Empty;
            nrow["rcrd_status"]          = string.Empty;
            nrow["user_id_created_by"]   = string.Empty;
            nrow["created_dttm"]         = string.Empty;
            nrow["user_id_updated_by"]   = string.Empty;
            nrow["updated_dttm"]         = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
       
        //**************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string editExpression   = "empl_id = '" + svalues[0].Trim() + "' AND payroll_year = '" + svalues[1].Trim() + "' AND effective_date = '" + svalues[2].Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            
            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;
            
            txtb_empl_id.Text        = row2Edit[0]["empl_id"].ToString();
            txtb_empl_name.Text      = row2Edit[0]["employee_name"].ToString();
            txtb_position.Text       = row2Edit[0]["position_title1"].ToString();
            txtb_tax_rate.Text       = row2Edit[0]["tax_rate"].ToString();
            txtb_wtax_amt.Text       = row2Edit[0]["wtax_amt"].ToString();
            lbl_rate_descr.Text      = row2Edit[0]["rate_basis_descr"].ToString().Trim();
            txtb_rate_amount.Text    = row2Edit[0]["rate_amount"].ToString().Trim();
            
            txtb_effective_date.Text                = row2Edit[0]["effective_date"].ToString().Trim();
            ddl_status.SelectedValue                = row2Edit[0]["rcrd_status"].ToString().Trim();
            dtSource.Rows[0]["user_id_created_by"]  = row2Edit[0]["user_id_created_by"].ToString().Trim();
            dtSource.Rows[0]["created_dttm"]        = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
            dtSource.Rows[0]["user_id_updated_by"]  = row2Edit[0]["user_id_updated_by"].ToString().Trim();
            dtSource.Rows[0]["updated_dttm"]        = Convert.ToDateTime(row2Edit[0]["updated_dttm"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");

            //if (ddl_status.SelectedValue == "N" && row2Edit[0]["user_id_created_by"].ToString().Trim() != "sysadmin") // New Tax Override
            //{
            //    txtb_effective_date.Enabled = true;
            //}
            //else
            //{
            txtb_effective_date.Enabled = false;
            //}

            if (ddl_status.SelectedValue == "N")
            {
                ddl_status.Enabled          = true;
                // txtb_effective_date.Enabled = true;
                txtb_tax_rate.Enabled       = false;
                txtb_wtax_amt.Enabled       = false;
                Button2.Visible             = true;
            }
            else
            {
                ddl_status.Enabled          = false;
                // txtb_effective_date.Enabled = false;
                txtb_tax_rate.Enabled       = false;
                txtb_wtax_amt.Enabled       = false;
                Button2.Visible             = false;
            }
            
            LabelAddEdit.Text = "Edit Record: " + ddl_empl_name.SelectedValue.ToString().Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;
            
            ddl_empl_name.Visible = false;
            txtb_empl_name.Visible = true;
            FieldValidationColorChanged(false, "ALL");
            UpdatePanel16.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                     dtSource.Rows[0]["empl_id"]           = txtb_empl_id.Text.ToString().Trim();
                     dtSource.Rows[0]["payroll_year"]      = ddl_year.SelectedValue.ToString().Trim();
                     dtSource.Rows[0]["tax_rate"]          = txtb_tax_rate.Text.ToString().Trim();
                     dtSource.Rows[0]["wtax_amt"]          = txtb_wtax_amt.Text.ToString().Trim();
                    // VJA : 2020-02-21 
                    dtSource.Rows[0]["effective_date"]     = txtb_effective_date.Text.ToString().Trim();
                    dtSource.Rows[0]["rcrd_status"]         = ddl_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["user_id_created_by"]  = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["created_dttm"]        = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ;
                    dtSource.Rows[0]["user_id_updated_by"]  = "";
                    dtSource.Rows[0]["updated_dttm"]        = "";

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["empl_id"]           = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["payroll_year"]      = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["tax_rate"]          = txtb_tax_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["wtax_amt"]          = txtb_wtax_amt.Text.ToString().Trim();
                    // VJA : 2020-02-21 
                    dtSource.Rows[0]["effective_date"]      = txtb_effective_date.Text.ToString().Trim();
                    dtSource.Rows[0]["rcrd_status"]         = ddl_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["user_id_created_by"]  = dtSource.Rows[0]["user_id_created_by"].ToString().Trim();
                    dtSource.Rows[0]["created_dttm"]        = dtSource.Rows[0]["created_dttm"].ToString().Trim();
                    dtSource.Rows[0]["user_id_updated_by"]  = Session["ep_user_id"].ToString().Trim();
                    dtSource.Rows[0]["updated_dttm"]        = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    
                    if (scriptInsertUpdate == string.Empty) return;
                    string msg1 = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg1 == "") return;

                    if (msg1.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist");
                        return;
                    }
                    
                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        
                        nrow["empl_id"]         = txtb_empl_id.Text.ToString().Trim();
                        nrow["payroll_year"]    = ddl_year.SelectedValue.ToString().Trim();
                        nrow["tax_rate"]        = txtb_tax_rate.Text.ToString().Trim();
                        nrow["wtax_amt"]        = txtb_wtax_amt.Text.ToString().Trim();

                        nrow["employee_name"]   = ddl_empl_name.SelectedItem.ToString().Trim();
                        nrow["position_title1"] = txtb_position.Text.ToString().Trim();
                        nrow["rate_basis_descr"] = lbl_rate_descr.Text;
                        nrow["rate_amount"]      = txtb_rate_amount.Text;

                        nrow["effective_date"]      = txtb_effective_date.Text.ToString().Trim();
                        nrow["rcrd_status"]         = ddl_status.SelectedValue.ToString().Trim();
                        nrow["rcrd_status_descr"]   = ddl_status.SelectedItem.ToString().Trim();
                        nrow["user_id_created_by"]  = Session["ep_user_id"].ToString().Trim();
                        nrow["created_dttm"]        = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ;
                        nrow["updated_dttm"]        = Convert.ToDateTime("1900-01-01");
                        //nrow["user_id_updated_by"]  = "";
                        //nrow["updated_dttm"]        = "";


                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND payroll_year = '" + ddl_year.SelectedValue.ToString().Trim() + "' AND effective_date = '" + txtb_effective_date.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        
                        row2Edit[0]["empl_id"]         = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["payroll_year"]    = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["tax_rate"]        = txtb_tax_rate.Text.ToString().Trim();
                        row2Edit[0]["wtax_amt"]        = txtb_wtax_amt.Text.ToString().Trim();
                        
                        row2Edit[0]["employee_name"]    = txtb_empl_name.Text.ToString().Trim();
                        row2Edit[0]["position_title1"]  = txtb_position.Text.ToString().Trim();
                        row2Edit[0]["rate_basis_descr"] = lbl_rate_descr.Text;
                        row2Edit[0]["rate_amount"]      = txtb_rate_amount.Text;
                        // VJA : 2020-02-21 
                        row2Edit[0]["effective_date"]      = txtb_effective_date.Text.ToString().Trim();
                        row2Edit[0]["rcrd_status"]         = ddl_status.SelectedValue.ToString().Trim();
                        row2Edit[0]["rcrd_status_descr"]   = ddl_status.SelectedItem.ToString().Trim();
                        row2Edit[0]["user_id_created_by"]  = dtSource.Rows[0]["user_id_created_by"].ToString().Trim();
                        row2Edit[0]["created_dttm"]        = dtSource.Rows[0]["created_dttm"].ToString().Trim();
                        row2Edit[0]["user_id_updated_by"]  = Session["ep_user_id"].ToString().Trim();
                        row2Edit[0]["updated_dttm"]        = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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
        //  BEGIN - JVA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        private void searchFunc()
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR payroll_year LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR tax_rate LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR effective_date LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR rcrd_status LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR rcrd_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR user_id_created_by LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR user_id_updated_by LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR wtax_amt LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' ";


            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("tax_rate", typeof(System.String));
            dtSource1.Columns.Add("wtax_amt", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("rcrd_status", typeof(System.String));
            dtSource1.Columns.Add("rcrd_status_descr", typeof(System.String));
            dtSource1.Columns.Add("user_id_created_by", typeof(System.String));
            dtSource1.Columns.Add("user_id_updated_by", typeof(System.String));


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
        //  BEGIN - VJA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR payroll_year LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR tax_rate LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR effective_date LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR rcrd_status LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR rcrd_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR user_id_created_by LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR user_id_updated_by LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR wtax_amt LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' ";
            
            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("tax_rate", typeof(System.String));
            dtSource1.Columns.Add("wtax_amt", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("rcrd_status", typeof(System.String));
            dtSource1.Columns.Add("rcrd_status_descr", typeof(System.String));
            dtSource1.Columns.Add("user_id_created_by", typeof(System.String));
            dtSource1.Columns.Add("user_id_updated_by", typeof(System.String));


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
            
            if (ddl_empl_name.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() ==  MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_name");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_tax_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_tax_rate");
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_wtax_amt) == false)
            {
                FieldValidationColorChanged(true, "txtb_wtax_amt");
                validatedSaved = false;
            }
            if (CommonCode.checkisdatetime(txtb_effective_date) == false)
            {
                FieldValidationColorChanged(true, "txtb_effective_date");
                validatedSaved = false;
            }
            // VJA - 2020-06-18 : GE COMMENT KAY NAG BACK TRACK MAN SA DATA
            // if (CommonCode.checkisdatetime(txtb_effective_date) == true)
            // {
            //     if (DateTime.Parse(txtb_effective_date.Text) < DateTime.Now && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            //     {
            //         FieldValidationColorChanged(true, "current-future-date");
            //         validatedSaved = false;
            //     }
            // }

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
                    case "ddl_empl_name":
                        {
                            LblRequired12.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_name.BorderColor = Color.Red;
                            
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired12.Text = "Already Exist";
                            ddl_empl_name.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_tax_rate":
                        {
                            LblRequired1.Text = MyCmn.CONST_INVALID_NUMERIC; ;
                            txtb_tax_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_wtax_amt":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC; ;
                            txtb_wtax_amt.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_effective_date":
                        {
                            LblRequired13.Text = "Invalid Date !";
                            txtb_effective_date.BorderColor = Color.Red;
                            UpdatePanel16.Update();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popshowdate", "show_date();", true);
                            break;
                        }
                    case "current-future-date":
                        {
                            LblRequired13.Text = "Please input future date !" ;
                            txtb_effective_date.BorderColor = Color.Red;
                            UpdatePanel16.Update();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popshowdate", "show_date();", true);
                            break;
                        }
                        
                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired12.Text = "";
                            LblRequired13.Text = "";
                            LblRequired1.Text = "";
                            LblRequired2.Text = "";
                            
                            ddl_empl_name.BorderColor = Color.LightGray;
                            txtb_tax_rate.BorderColor = Color.LightGray;
                            txtb_wtax_amt.BorderColor = Color.LightGray;
                            txtb_effective_date.BorderColor = Color.LightGray;
                            
                            break;
                        }

                }
            }
        }
        //*************************************************************************
        //  BEGIN - JADE- 05/21/2019 - 
        //*************************************************************************
        protected void ddl_empl_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_name.SelectedValue.ToString() != "")
            {
                string editExpression = "empl_id = '" + ddl_empl_name.SelectedValue.ToString().Trim() + "'";
                DataRow[] row2Edit2 = dtSource_for_names.Select(editExpression);
                txtb_empl_id.Text               = row2Edit2[0]["empl_id"].ToString().Trim();
                txtb_position.Text              = row2Edit2[0]["position_title1"].ToString().Trim();
                txtb_tax_rate.Text              = row2Edit2[0]["tax_rate"].ToString().Trim();
                txtb_wtax_amt.Text              = row2Edit2[0]["wtax_amt"].ToString().Trim();
                lbl_rate_descr.Text             = row2Edit2[0]["rate_basis_descr"].ToString().Trim();
                txtb_rate_amount.Text           = row2Edit2[0]["rate_amount"].ToString().Trim();
                
            }
            else
            {
                ClearEntry();
            }
        }
        
        //***********************************************
        //  BEGIN - JADE- 05/21/2019 - Retrieve Year 
        //***********************************************
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_department.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "" && ddl_year.SelectedValue != "")
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

        protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_department.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "" && ddl_year.SelectedValue != "")
            {
                RetrieveEmpl();
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveDataListGrid();
            UpdatePanel10.Update();
        }
        //***************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];
            string payroll_year = commandArgs[1];
            string eff_date = commandArgs[2];

            deleteRec1.Text = "Are you sure to delete this Record ?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_year='" + commandarg[1].Trim() + "' AND effective_date='" + commandarg[2].Trim() + "'";

            MyCmn.DeleteBackEndData("empl_taxwithheld_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //***********************************************
        //  BEGIN - JADE- 05/21/2019 - Include History
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

        //***********************************************
        //  BEGIN - JRV- 05/21/2019 - Include History
        //***********************************************
        protected void imgbtn_add_empl_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string showExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND payroll_year = '" + svalues[1].ToString().Trim() + "' AND effective_date = '" + svalues[2].ToString().Trim() + "'";
            DataRow[] row2Show = dataListGrid.Select(showExpression);
            string url = "../cEmployeeTaxRateRECEDetails/cEmployeeTaxRateRECEDetails.aspx";
          
            Session["PreviousValuesonPage_cEmployeeTaxRateRECE"] = svalues[0].ToString().Trim() + "," + svalues[1].ToString().Trim() + "," + row2Show[0]["tax_rate"].ToString().Trim() + "," + ddl_empl_type.SelectedValue.ToString() + "," + ddl_empl_type.SelectedItem.ToString() + "," + row2Show[0]["position_title1"].ToString().Trim() + "," + txtb_search.Text.ToString() + "," + ddl_department.SelectedValue.ToString();
            Session["PreviousValuesonPage_cEmployeeTaxRateRECE_name"] = row2Show[0]["employee_name"].ToString().Trim();
            Session["PreviousValuesonPage_cEmployeeTaxRateRECE_amt"]  = row2Show[0]["wtax_amt"].ToString().Trim();
            if (url != "")
            {
                Response.Redirect(url);
            }
        }

        //*************************************************************************
        //  END OF CODE
        //*************************************************************************
    }
}