//**********************************************************************************
// PROJECT NAME     :   HRIS - ePayroll
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade Alivio     01/17/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cPayAccounts
{
    public partial class cPayAccounts : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JADE- 01/18/2019 - Data Place holder creation 
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
        //  BEGIN - JADE- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - JADE- 01/18/2019 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "account_code";
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
        //  BEGIN - JADE- 01/18/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPositionTable"] = "cPositionTable";

            RetrieveDataListGrid();
            RetrieveAcctClass();
            RetrieveAcctType();
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollaccounts_tbl_list");
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list from Salary Grade Table
        //*************************************************************************
        private void RetrieveAcctClass()
        {
            ddl_acctclass.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_accountclass_tbl_list");

            ddl_acctclass.DataSource = dtEmpList;
            ddl_acctclass.DataTextField = "acctclass_descr";
            ddl_acctclass.DataValueField = "acctclass_code";
            ddl_acctclass.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_acctclass.Items.Insert(0, li);
        }
        private void RetrieveAcctType()
        {
            ddl_accttype.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_accounttype_tbl_list");

            ddl_accttype.DataSource = dtEmpList;
            ddl_accttype.DataTextField = "accttype_descr";
            ddl_accttype.DataValueField = "accttype_code";
            ddl_accttype.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_accttype.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_code.Text = "";
            txtb_descr.Text = "";
            txtb_short_descr.Text = "";
            ddl_nature_of_account.SelectedIndex = 0;
            ddl_acctclass.SelectedIndex = 0;
            ddl_accttype.SelectedIndex = 0;
            
            txtb_code.ReadOnly = false;
            txtb_code.Focus();
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["account_code"] = string.Empty;
            nrow["account_title"] = string.Empty;
            nrow["account_short_title"] = string.Empty;
            nrow["nature_of_account"] = string.Empty;
            nrow["acctclass_code"] = string.Empty;
            nrow["accttype_code"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);

        }
        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("account_code", typeof(System.String));
            dtSource.Columns.Add("account_short_title", typeof(System.String));
            dtSource.Columns.Add("account_title", typeof(System.String));
            dtSource.Columns.Add("nature_of_account", typeof(System.String));
            dtSource.Columns.Add("acctclass_code", typeof(System.String));
            dtSource.Columns.Add("accttype_code", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollaccounts_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "account_code" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //***************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string appttype = commandArgs[0];
            string appttypedescr = commandArgs[1];

            deleteRec1.Text = "Are you sure to delete this Account Code = (" + appttype.Trim() + ") - " + appttypedescr.Trim() + " ?";
            lnkBtnYes.CommandArgument = appttype;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string deleteExpression = "account_code = '" + svalues + "'";

            MyCmn.DeleteBackEndData("payrollaccounts_tbl", "WHERE " + deleteExpression);
            MyCmn.DeleteBackEndData("payrollsubaccounts_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string editExpression = "account_code = '" + svalues + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["account_code"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_code.Text = svalues;
            txtb_descr.Text = row2Edit[0]["account_title"].ToString();
            txtb_short_descr.Text = row2Edit[0]["account_short_title"].ToString();

            ddl_nature_of_account.SelectedValue = row2Edit[0]["nature_of_account"].ToString();
            ddl_acctclass.SelectedValue = row2Edit[0]["acctclass_code"].ToString();
            ddl_accttype.SelectedValue = row2Edit[0]["accttype_code"].ToString();

            txtb_code.ReadOnly = true;
            LabelAddEdit.Text = "Edit Record: " + txtb_code.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Change Field Sort mode  
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
        //  BEGIN - JADE- 01/18/2019 - Get Grid current sort order 
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
        //  BEGIN - JADE- 01/18/2019 - Save New Record/Edited Record to back end DB
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
                    dtSource.Rows[0]["account_code"]                = txtb_code.Text.ToString();
                    dtSource.Rows[0]["account_short_title"]         = txtb_short_descr.Text.ToString();
                    dtSource.Rows[0]["account_title"]               = txtb_descr.Text.ToString();
                    dtSource.Rows[0]["nature_of_account"]           = ddl_nature_of_account.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["acctclass_code"]              = ddl_acctclass.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["accttype_code"]               = ddl_accttype.SelectedValue.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["account_code"]                = txtb_code.Text.ToString();
                    dtSource.Rows[0]["account_short_title"]         = txtb_short_descr.Text.ToString();
                    dtSource.Rows[0]["account_title"]               = txtb_descr.Text.ToString();
                    dtSource.Rows[0]["nature_of_account"]           = ddl_nature_of_account.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["acctclass_code"]              = ddl_acctclass.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["accttype_code"]               = ddl_accttype.SelectedValue.ToString().Trim();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;
                    if (msg.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist");
                        txtb_code.Focus();
                        return;
                    }


                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["account_code"] = txtb_code.Text.ToString();
                        nrow["account_short_title"] = txtb_short_descr.Text.ToString();
                        nrow["account_title"] = txtb_descr.Text.ToString();
                        nrow["nature_of_account"] = ddl_nature_of_account.SelectedValue.ToString().Trim();
                        nrow["acctclass_code"] = ddl_acctclass.SelectedValue.ToString().Trim();
                        nrow["accttype_code"] = ddl_accttype.SelectedValue.ToString().Trim();

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "account_code = '" + txtb_code.Text.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        row2Edit[0]["account_code"] = txtb_code.Text.ToString();
                        row2Edit[0]["account_short_title"] = txtb_short_descr.Text.ToString();
                        row2Edit[0]["account_title"] = txtb_descr.Text.ToString();

                        row2Edit[0]["nature_of_account"]  = ddl_nature_of_account.SelectedValue.ToString().Trim();
                        row2Edit[0]["acctclass_code"]      = ddl_acctclass.SelectedValue.ToString().Trim();
                        row2Edit[0]["accttype_code"]      = ddl_accttype.SelectedValue.ToString().Trim();
                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                }
                ViewState.Remove("AddEdit_Mode");
                show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            }
        }

        //**************************************************************************
        //  BEGIN - JADE- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JADE- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JADE- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "account_code LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_short_title LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_title LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("account_code", typeof(System.String));
            dtSource1.Columns.Add("account_short_title", typeof(System.String));
            dtSource1.Columns.Add("account_title", typeof(System.String));

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
        //  BEGIN - JADE- 01/18/2019 - Define Property for Sort Direction  
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
        //  BEGIN - JADE- 01/18/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;

            if (txtb_code.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_code");
                txtb_code.Focus();
                validatedSaved = false;
            }
            if (txtb_short_descr.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_short_descr");
                txtb_short_descr.Focus();
                validatedSaved = false;
            }
            if (txtb_descr.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_descr");
                txtb_descr.Focus();
                validatedSaved = false;
            }
            if (ddl_nature_of_account.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_nature_of_account");
                ddl_nature_of_account.Focus();
                validatedSaved = false;
            }
            if (ddl_acctclass.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_acctclass");
                ddl_acctclass.Focus();
                validatedSaved = false;
            }
            if (ddl_accttype.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_accttype");
                ddl_accttype.Focus();
                validatedSaved = false;
            }
            return validatedSaved;
        }

        //**************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Check if Object already contains value  
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

        //**********************************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "txtb_code":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_code.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_short_descr":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_short_descr.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_descr":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_descr.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text = "Already Exist!";
                            txtb_code.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_nature_of_account":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            ddl_nature_of_account.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_acctclass":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            ddl_acctclass.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_accttype":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            ddl_accttype.BorderColor = Color.Red;
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
                            LblRequired6.Text = "";
                            txtb_descr.BorderColor = Color.LightGray;
                            txtb_short_descr.BorderColor = Color.LightGray;
                            txtb_code.BorderColor = Color.LightGray;
                            ddl_nature_of_account.BorderColor = Color.LightGray;
                            ddl_acctclass.BorderColor = Color.LightGray;
                            ddl_accttype.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }
    }
}