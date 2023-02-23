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

namespace HRIS_ePayroll.View.cPaySubAccounts
{
    public partial class cPaySubAccounts : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - AEC- 09/20/2018 - Data Place holder creation 
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
        //  BEGIN - AEC- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - AEC- 09/20/2018 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "account_sub_code";
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
        //  BEGIN - AEC- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPositionTable"] = "cPositionTable";
            RetrieveDataListGrid();
            RetrieveDataAccount();

            RetrieveAcctClass();
            RetrieveAcctType();

            Button1.Visible = false;
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payroll_subaccount_tbl_list", "par_account_code" ,ddl_account.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list from Salary Grade Table
        //*************************************************************************
        private void RetrieveDataAccount()
        {
            ddl_account.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_payrollaccounts_tbl_list");

            ddl_account.DataSource = dtEmpList;
            ddl_account.DataTextField = "account_title";
            ddl_account.DataValueField = "account_code";
            ddl_account.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_account.Items.Insert(0, li);
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
        //  BEGIN - AEC- 09/20/2018 - Add button to trigger add/edit page
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
            
            txtb_subaccount_code.Focus();
            //Retrieve NExt Sub account
            RetrieveNextSubAccountCode();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_subaccount_code.Text = "";
            txtb_subaccount_description.Text = "";
            txtb_subaccount_short_description.Text = "";

            ddl_acctclass.SelectedIndex = 0;
            ddl_accttype.SelectedIndex = 0;
            
            txtb_subaccount_code.ReadOnly = false;
            txtb_subaccount_code.Focus();
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["account_sub_code"]        = string.Empty;
            nrow["account_code"]            = string.Empty;
            nrow["account_sub_title"]       = string.Empty;
            nrow["account_sub_short_title"] = string.Empty;
            nrow["acctclass_code"]= string.Empty;
            nrow["accttype_code"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
            
        }
        //*************************************************************************
        //  JADE - Jade- 09/28/2018 - Get the Last Control Number
        //*************************************************************************
        private void RetrieveNextSubAccountCode()
        {
            string sql1 = "SELECT TOP 1 account_sub_code from payrollsubaccounts_tbl where LEFT(account_sub_code,2)=LEFT(account_sub_code,2) AND account_sub_code <> '99' AND account_code = '" + ddl_account.SelectedValue.ToString() +"' order by account_sub_code DESC";
            string last_row = MyCmn.GetLastRow_of_Table(sql1);
            if (last_row.Trim() == "")
            {
                last_row = "0";
            }
            int last_row1 = int.Parse(last_row) + 1;
            txtb_subaccount_code.Text = last_row1.ToString().PadLeft(2, '0');
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("account_sub_code", typeof(System.String));
            dtSource.Columns.Add("account_code", typeof(System.String));
            dtSource.Columns.Add("account_sub_title", typeof(System.String));
            dtSource.Columns.Add("account_sub_short_title", typeof(System.String));
            dtSource.Columns.Add("acctclass_code", typeof(System.String));
            dtSource.Columns.Add("accttype_code", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollsubaccounts_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "account_sub_code" ,"account_code"};
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //***************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string appttype = commandArgs[0];
            string appttypedescr = commandArgs[1];

            deleteRec1.Text = "Are you sure to delete this SubAccount = (" + appttype.Trim() + ") - " + appttypedescr.Trim() + " ?";
            lnkBtnYes.CommandArgument = appttype;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string deleteExpression = "account_sub_code = '" + svalues + "'";

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
        //  BEGIN - AEC- 09/20/2018 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string editExpression = "account_sub_code = '" + svalues + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["account_sub_code"] = string.Empty;
            nrow["account_code"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);
            txtb_subaccount_code.Text = svalues;
            txtb_subaccount_description.Text = row2Edit[0]["account_sub_title"].ToString();
            txtb_subaccount_short_description.Text = row2Edit[0]["account_sub_short_title"].ToString();
            ddl_acctclass.SelectedValue = row2Edit[0]["acctclass_code"].ToString();
            ddl_accttype.SelectedValue = row2Edit[0]["accttype_code"].ToString();

            txtb_subaccount_code.ReadOnly = true;
            LabelAddEdit.Text = "Edit Record: " + txtb_subaccount_short_description.Text.Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Change Field Sort mode  
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
        //  BEGIN - AEC- 09/20/2018 - Get Grid current sort order 
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
        //  BEGIN - AEC- 09/20/2018 - Save New Record/Edited Record to back end DB
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
                    dtSource.Rows[0]["account_code"]                 = ddl_account.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_sub_code"]             = txtb_subaccount_code.Text.ToString();
                    dtSource.Rows[0]["account_sub_title"]            = txtb_subaccount_description.Text.ToString();
                    dtSource.Rows[0]["account_sub_short_title"]      = txtb_subaccount_short_description.Text.ToString();
                    dtSource.Rows[0]["acctclass_code"]               = ddl_acctclass.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["accttype_code"]                = ddl_accttype.SelectedValue.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {

                    dtSource.Rows[0]["account_code"]                    = ddl_account.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_sub_code"]                = txtb_subaccount_code.Text.ToString();
                    dtSource.Rows[0]["account_sub_title"]               = txtb_subaccount_description.Text.ToString();
                    dtSource.Rows[0]["account_sub_short_title"]         = txtb_subaccount_short_description.Text.ToString();
                    dtSource.Rows[0]["acctclass_code"]                  = ddl_acctclass.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["accttype_code"]                   = ddl_accttype.SelectedValue.ToString().Trim();

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
                        txtb_subaccount_code.Focus();
                        return;
                    }

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["account_sub_code"] = txtb_subaccount_code.Text.ToString();
                        nrow["account_sub_short_title"] = txtb_subaccount_short_description.Text.ToString();
                        nrow["account_sub_title"] = txtb_subaccount_description.Text.ToString();
                        nrow["acctclass_code"] = ddl_acctclass.SelectedValue.ToString();
                        nrow["accttype_code"] = ddl_accttype.SelectedValue.ToString();

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        //gv_dataListGrid.SelectRow(gv_dataListGrid.Rows.Count - 1);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "account_sub_code = '" + txtb_subaccount_code.Text.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        row2Edit[0]["account_sub_code"] = txtb_subaccount_code.Text.ToString();
                        row2Edit[0]["account_sub_short_title"] = txtb_subaccount_short_description.Text.ToString();
                        row2Edit[0]["account_sub_title"] = txtb_subaccount_description.Text.ToString();
                        row2Edit[0]["acctclass_code"] = ddl_acctclass.SelectedValue.ToString();
                        row2Edit[0]["accttype_code"] = ddl_accttype.SelectedValue.ToString();
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
        //  BEGIN - AEC- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - AEC- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - AEC- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "account_sub_code LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_sub_short_title LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_sub_title LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("account_sub_code", typeof(System.String));
            dtSource1.Columns.Add("account_sub_short_title", typeof(System.String));
            dtSource1.Columns.Add("account_sub_title", typeof(System.String));

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
        //  BEGIN - AEC- 09/20/2018 - Define Property for Sort Direction  
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
        //  BEGIN - AEC- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            
            if(txtb_subaccount_code.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_subaccount_code");
                txtb_subaccount_code.Focus();
                validatedSaved = false;
            }
            else if (txtb_subaccount_short_description.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_subaccount_short_description");
                txtb_subaccount_short_description.Focus();
                validatedSaved = false;
            }
            else if (txtb_subaccount_description.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_subaccount_description");
                txtb_subaccount_description.Focus();
                validatedSaved = false;
            }
            else if (ddl_acctclass.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_acctclass");
                ddl_acctclass.Focus();
                validatedSaved = false;
            }
            else if (ddl_accttype.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_accttype");
                ddl_accttype.Focus();
                validatedSaved = false;
            }
            return validatedSaved;
        }

        //**************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Check if Object already contains value  
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
        //  BEGIN - AEC- 09/20/2018 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "txtb_subaccount_short_description":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_subaccount_short_description.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_subaccount_code":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_subaccount_code.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_subaccount_description":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_subaccount_description.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired3.Text = "Already exist!";
                            txtb_subaccount_code.BorderColor = Color.Red;
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
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
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
                            LblRequired5.Text = "";
                            LblRequired6.Text = "";
                            txtb_subaccount_description.BorderColor = Color.LightGray;
                            txtb_subaccount_short_description.BorderColor = Color.LightGray;
                            txtb_subaccount_code.BorderColor = Color.LightGray;
                            ddl_acctclass.BorderColor = Color.LightGray;
                            ddl_accttype.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }

        protected void ddl_account_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_account.SelectedValue != "")
            {
                RetrieveDataListGrid();
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false;
            }
            UpdatePanel10.Update();
        }
    }
}