//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     03/25/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View
{
    public partial class cEmplCommMemo : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 03/25/2019 - Data Place holder creation 
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

        //********************************************************************
        //  BEGIN - VJA- 03/25/2019 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 03/25/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "employee_name";
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
            }
        }

        //********************************************************************
        //  BEGIN - VJA- 03/25/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPayAccountsSubGroup"] = "cPayAccountsSubGroup";
            RetrieveDataListGrid();

            RetrieveBindingDepartments();
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveYear();
            RetriveEmploymentType();

            RetrieveEmpl();

            btnAdd.Visible = false;

        }
        private void RetrieveEmpl()
        {
            ddl_empl_id.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_personnelnames_combolist_commexp", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString().Trim(), "par_division_code", ddl_division.SelectedValue.ToString().Trim(), "par_section_code", ddl_section.SelectedValue.ToString().Trim());

            ddl_empl_id.DataSource = dt;
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }
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
        //  BEGIN - VJA- 03/25/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_communicationmemo_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_department_code", ddl_department.SelectedValue.ToString().Trim(), "par_subdepartment_code", ddl_subdep.SelectedValue.ToString().Trim(), "par_division_code", ddl_division.SelectedValue.ToString().Trim(), "par_section_code", ddl_section.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            RetrieveEmpl();
            ddl_empl_id.Enabled = true;
            LabelAddEdit.Text = "Add New Record";
            txtb_empl_name.Visible = false;
            ddl_empl_id.Visible = true;

            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            //ddl_empl_id.SelectedValue = "";
            ddl_status.SelectedIndex = 0;
            txtb_amount.Text = "";
            txtb_memo_descr.Text = "";
            //txtb_subaccount_code.ReadOnly = false;
            ddl_empl_id.Focus();
        }

        //*************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["memo_descr"] = string.Empty;
            nrow["comm_amount"] = string.Empty;
            nrow["recrd_status"] = string.Empty;
            nrow["created_at"] = string.Empty;
            nrow["created_by"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }

        //*************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("memo_descr", typeof(System.String));
            dtSource.Columns.Add("comm_amount", typeof(System.String));
            dtSource.Columns.Add("recrd_status", typeof(System.String));
            dtSource.Columns.Add("created_at", typeof(System.String));
            dtSource.Columns.Add("created_by", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "communicationmemo_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "Id" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //***************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];

            deleteRec1.Text = "Are you sure to delete this Record ?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            //string svalues = e.CommandArgument.ToString();
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND Id = '"+ svalues[1].ToString().Trim() + "'";

            MyCmn.DeleteBackEndData("communicationmemo_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            //string svalues = e.CommandArgument.ToString();
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "' AND Id = '" + svalues[1].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            RetrieveEmpl();
            DataRow nrow = dtSource.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_empl_id.Text = svalues[0].ToString().Trim();
            txtb_empl_name.Text = row2Edit[0]["employee_name"].ToString();
            txtb_empl_name.Visible = true;
            ddl_empl_id.Visible = false;

            txtb_amount.Text = row2Edit[0]["comm_amount"].ToString();
            ddl_status.Text = row2Edit[0]["recrd_status"].ToString();
            txtb_memo_descr.Text = row2Edit[0]["memo_descr"].ToString();

            ddl_empl_id.Enabled = false;
            LabelAddEdit.Text = "Edit Record: " + ddl_empl_id.SelectedValue.ToString().Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Change Field Sort mode  
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
        //  BEGIN - VJA- 03/25/2019 - Get Grid current sort order 
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
        //  BEGIN - VJA- 03/25/2019 - Save New Record/Edited Record to back end DB
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
                    dtSource.Rows[0]["empl_id"]              = ddl_empl_id.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["memo_descr"]           = txtb_memo_descr.Text.ToString().Trim();
                    dtSource.Rows[0]["comm_amount"]          = float.Parse(txtb_amount.Text).ToString("###,##0.00");
                    dtSource.Rows[0]["recrd_status"]          = ddl_status.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["created_at"]            = DateTime.Now;
                    dtSource.Rows[0]["created_by"]            = Session["ep_user_id"].ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["empl_id"]             = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["memo_descr"]          = txtb_memo_descr.Text.ToString().Trim();
                    dtSource.Rows[0]["comm_amount"]         = float.Parse(txtb_amount.Text).ToString("###,##0.00");
                    dtSource.Rows[0]["recrd_status"]         = ddl_status.SelectedValue.ToString().Trim();
                    //dtSource.Rows[0]["created_at"]            = DateTime.Now;
                    //dtSource.Rows[0]["created_by"]            = Session["ep_user_id"].ToString().Trim();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    //if (msg == "") return;
                    //if (msg.Substring(0, 1) == "X")
                    //{
                    //    FieldValidationColorChanged(true, "already-exist");
                    //    ddl_empl_id.Focus();
                    //    return;
                    //}


                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["empl_id"]               = ddl_empl_id.SelectedValue.ToString().Trim();
                        nrow["employee_name"]         = ddl_empl_id.SelectedItem.ToString().Trim();
                        nrow["memo_descr"]            = txtb_memo_descr.Text.ToString().Trim();
                        nrow["comm_amount"]           = float.Parse(txtb_amount.Text).ToString("###,##0.00");
                        nrow["recrd_status"]          = ddl_status.SelectedValue.ToString().Trim();
                        nrow["created_at"]            = DateTime.Now;
                        nrow["created_by"]            = Session["ep_user_id"].ToString().Trim();

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        //gv_dataListGrid.SelectRow(gv_dataListGrid.Rows.Count - 1);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        
                         row2Edit[0]["empl_id"]             = ddl_empl_id.SelectedValue.ToString().Trim();
                         row2Edit[0]["empl_id"]              = txtb_empl_id.Text.ToString().Trim();
                         row2Edit[0]["employee_name"]        = ddl_empl_id.SelectedItem.ToString().Trim();
                         row2Edit[0]["employee_name"]       = txtb_empl_name.Text.ToString().Trim();
                         row2Edit[0]["memo_descr"]          = txtb_memo_descr.Text.ToString().Trim();
                         row2Edit[0]["comm_amount"]         = float.Parse(txtb_amount.Text).ToString("###,##0.00");
                         row2Edit[0]["recrd_status"]         = ddl_status.SelectedValue.ToString().Trim();
                         //row2Edit[0]["created_at"]          = DateTime.Now;
                         //row2Edit[0]["created_by"]            = Session["ep_user_id"].ToString().Trim();
                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    up_dataListGrid.Update();
                    RetrieveDataListGrid();
                }
                ViewState.Remove("AddEdit_Mode");
                show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            }
        }

        //**************************************************************************
        //  BEGIN - VJA- 03/25/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%","") + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "") + "%' OR comm_amount LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "") + "%' OR memo_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "") + "%'";

            DataTable dtSource1 = new DataTable();

            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("memo_descr", typeof(System.String));
            dtSource1.Columns.Add("comm_amount", typeof(System.String));
            dtSource1.Columns.Add("recrd_status", typeof(System.String));
            dtSource1.Columns.Add("created_at", typeof(System.String));
            dtSource1.Columns.Add("created_by", typeof(System.String));
            dtSource1.Columns.Add("Id", typeof(System.String));

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
        //  BEGIN - VJA- 03/25/2019 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 03/25/2019 - Check if Object already contains value  
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
        //  BEGIN - VJA- 03/25/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;

            if (ddl_empl_id.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }
            if (txtb_amount.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_amount");
                txtb_amount.Focus();
                validatedSaved = false;
            }
            if(CommonCode.checkisdecimal(txtb_amount) == false)
            {
                FieldValidationColorChanged(true, "invalid-txtb_amount");
                txtb_amount.Focus();
                validatedSaved = false;
            }
           //if (txtb_memo_descr.Text == "")
           // {
           //     FieldValidationColorChanged(true, "txtb_memo_descr");
           //     txtb_memo_descr.Focus();
           //     validatedSaved = false;
           // }
            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - VJA- 03/25/2019 - Change/Toggle Mode for Object Appearance during validation  
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
                    case "txtb_amount":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-txtb_amount":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_amount.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_status":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            ddl_status.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_memo_descr":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_memo_descr.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text = "already exist!";
                            ddl_empl_id.BorderColor = Color.Red;
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
                            ddl_empl_id.BorderColor = Color.LightGray;
                            txtb_amount.BorderColor = Color.LightGray;
                            ddl_status.BorderColor = Color.LightGray;
                            txtb_memo_descr.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }

        protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_department.SelectedValue != "")
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveBindingSubDep();
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveDataListGrid();
            UpdatePanel10.Update();
        }
        protected void ddl_subdep_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingDivision();
            RetrieveBindingSection();
            RetrieveDataListGrid();
        }
        protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveBindingSection();
            RetrieveDataListGrid();
        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
        protected void ddl_year_SelectedIndexChanged1(object sender, EventArgs e)
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
            UpdatePanel10.Update();
        }
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_department.SelectedValue != "")
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
    }
}