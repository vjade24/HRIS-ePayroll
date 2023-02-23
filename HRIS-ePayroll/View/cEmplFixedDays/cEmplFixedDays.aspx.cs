//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for cEmplFixedDays Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// JORGE RUSTOM VILLANUEVA    05/20/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cEmplFixedDays
{
    public partial class cEmplFixedDays : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JRV- 03/22/2019  - Data Place holder creation 
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
        //  BEGIN - JRV- 03/22/2019  - Public Variable used in Add/Edit Mode
        //****************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - JRV- 09/20/2018 - Page Load method
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
        //  BEGIN - JRV- 03/22/2019  - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cEmplFixedDays"] = "cEmplFixedDays";
            RetrieveEmploymentType();
            RetrieveDataListGrid();
            RetrieveYear();
            RetrieveBindingDepartments();

        }


        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollemployee_fixed_days_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString() ,"par_employment_type", ddl_empl_type.SelectedValue.ToString() , "par_department_code", ddl_department.SelectedValue.ToString());
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();

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
            dataListEmployee = MyCmn.RetrieveData("sp_personnelnames_combolist_fixed_days", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim());

            ddl_empl_id.DataSource = dataListEmployee;
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }



        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            RetrieveEmpl();

            txtb_empl_name_edit.Visible = false;
            ddl_empl_id.Visible = true;

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_empl_id.Text = "";
            txtb_empl_name_edit.Text = "";
            txtb_fixed_days.Text = "";
            txtb_position.Text = "";
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = String.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);

        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {

            dtSource = new DataTable();
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("fixed_days", typeof(System.String));
           

        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollemployee_fixed_days_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //***************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString();
            string code = commandArgs;

            deleteRec1.Text = "Are you sure to delete this Record = (" + code.Trim() + ") ?";
            lnkBtnYes.CommandArgument = code;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string commandArgs = e.CommandArgument.ToString();
            string code = commandArgs;

            string deleteExpression = "empl_id = '" + code.Trim() + "'";

            MyCmn.DeleteBackEndData("payrollemployee_fixed_days_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string code = e.CommandArgument.ToString();


            string editExpression = "empl_id = '" + code + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_empl_name_edit.Visible = true;
            ddl_empl_id.Visible = false;

            txtb_empl_name_edit.Text  = row2Edit[0]["employee_name"].ToString();
            txtb_empl_id.Text       = row2Edit[0]["empl_id"].ToString();
            txtb_fixed_days.Text    = row2Edit[0]["fixed_days"].ToString();
            txtb_position.Text    = row2Edit[0]["position_title1"].ToString();

            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);


            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Change Field Sort mode  
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
        //  BEGIN - JRV- 03/22/2019  - Get Grid current sort order 
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
        //  BEGIN - JRV- 03/22/2019  - Save New Record/Edited Record to back end DB
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
                    dtSource.Rows[0]["empl_id"]     = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["fixed_days"]  = txtb_fixed_days.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["empl_id"]    = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["fixed_days"] = txtb_fixed_days.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;
                    if (msg.Substring(0, 1) == "X") return;


                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();

                        nrow["empl_id"]         = txtb_empl_id.Text.ToString().Trim();
                        nrow["employee_name"]   = ddl_empl_id.SelectedItem.Text.ToString().Trim();
                        nrow["fixed_days"]      = txtb_fixed_days.Text.ToString().Trim();
                        nrow["position_title1"] = txtb_position.Text.ToString().Trim();
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {

                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["empl_id"]          = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employee_name"]    = txtb_empl_name_edit.Text.ToString().Trim();
                        row2Edit[0]["fixed_days"]       = txtb_fixed_days.Text.ToString().Trim();
                        row2Edit[0]["position_title1"]  = txtb_position.Text.ToString().Trim();

                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;

                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                }
                ViewState.Remove("AddEdit_Mode");
                show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            }
        }

        //**************************************************************************
        //  BEGIN - JRV- 03/22/2019  - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression =
              "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR fixed_days LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR position_title1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%'";



            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("fixed_days", typeof(System.String));
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
        //  BEGIN - JRV- 03/22/2019  - Define Property for Sort Direction  
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
        //  BEGIN - JRV- 03/22/2019  - Check if Object already contains value  
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
        //  BEGIN - JRV- 03/22/2019  - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;

            if (ddl_empl_id.SelectedIndex == 0 && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_fixed_days) == false)
            {
                if (txtb_fixed_days.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_fixed_days");
                    txtb_fixed_days.Focus();
                    validatedSaved = false;
                }

                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_fixed_days");
                    txtb_fixed_days.Focus();
                    validatedSaved = false;
                }
            }

            else
            {

                if (Convert.ToDecimal(txtb_fixed_days.Text) <= 0)
                {
                    FieldValidationColorChanged(true, "greater-zero-txtb_fixed_days");
                    txtb_fixed_days.Focus();
                    validatedSaved = false;
                }

            }

            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Change/Toggle Mode for Object Appearance during validation  
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

                    case "txtb_fixed_days":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_fixed_days.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_fixed_days":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_fixed_days.BorderColor = Color.Red;
                            break;
                        }

                    case "greater-zero-txtb_fixed_days":
                        {
                            LblRequired3.Text = "At least greater than zero";
                            txtb_fixed_days.BorderColor = Color.Red;
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
                            ddl_empl_id.BorderColor = Color.LightGray;
                            txtb_fixed_days.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
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

            RetrieveDataListGrid();
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
            RetrieveDataListGrid();
            up_dataListGrid.Update();
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
            up_dataListGrid.Update();
        }

        protected void ddl_empl_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_id.SelectedValue != "")
            {
                string editExpression = "empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'";
                DataRow[] row2Edit2 = dataListEmployee.Select("empl_id = '" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

                txtb_empl_id.Text = row2Edit2[0]["empl_id"].ToString();
                txtb_position.Text = row2Edit2[0]["position_title1"].ToString();
            }
            else
            {
                ClearEntry();
            }
            
        }
    }



}