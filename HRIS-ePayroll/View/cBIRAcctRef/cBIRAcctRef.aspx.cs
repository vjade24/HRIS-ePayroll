//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for cBIRAcctRef Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// JORGE RUSTOM VILLANUEVA    03/22/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cBIRAcctRef
{
    public partial class cBIRAcctRef : System.Web.UI.Page
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

        //********************************************************************
        //  BEGIN - JRV- 03/22/2019  - Public Variable used in Add/Edit Mode
        //********************************************************************

        public string var_include_history = "N";
        public int percentage;
        public float amt;
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
                    Session["SortField"] = "account_code";
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
            Session["cBIRAcctRef"] = "cBIRAcctRef";
            RetrieveDataListGrid();
            
        }

        //**********************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Set Include History Variable
        //*********************************************************************************
        public void chkIncludeHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncludeHistory.Checked)
            {
                var_include_history = "Y";
                
            }
            else
            {
                var_include_history = "N";
            }
            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }



        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_bir_excempt_tbl_list", "par_include_history", var_include_history);
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }


        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Populate Combo list from Salary Grade Table
        //*************************************************************************
        private void RetrieveBindingAccount()
        {
            ddl_acct_code.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_payrollaccounts_tbl_list2");

            ddl_acct_code.DataSource = dtEmpList;
            ddl_acct_code.DataTextField = "account_title";
            ddl_acct_code.DataValueField = "account_code";
            ddl_acct_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_acct_code.Items.Insert(0, li);
            //UpdatePanel1.Update();
        }

        private void RetrieveBindingSub_account()
        {
            ddl_sub_acct_code.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_payroll_subaccount_tbl_list2", "par_account_code", ddl_acct_code.SelectedValue.ToString());

            ddl_sub_acct_code.DataSource = dtEmpList;
            ddl_sub_acct_code.DataTextField = "account_sub_title";
            ddl_sub_acct_code.DataValueField = "account_sub_code";
            ddl_sub_acct_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_sub_acct_code.Items.Insert(0, li);
          
        }

        //**********************************************************************************
        //  BEGIN - RETRIEVE SUB ACCOUNTS LIST
        //*********************************************************************************
        public void ddl_acct_code_txt_changed(object sender, EventArgs e)
        {
            RetrieveBindingSub_account();
          
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            RetrieveBindingAccount();
            RetrieveBindingSub_account();
            AddPrimaryKeys();
            AddNewRow();

            ddl_acct_code.Enabled = true;
            ddl_sub_acct_code.Enabled = true;
            tbx_effective_date.Enabled = true;
            tbx_effective_date.Visible = true;
            tbx_effective_date1.Enabled = true;
            tbx_effective_date1.Visible = false;
            UpdatePanel3.Update();
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
            tbx_effective_date.Text = "";
            UpdatePanel3.Update();
            tbx_monthly_amt_per.Text = "";
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["account_code"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["account_sub_code"] = string.Empty;
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
            dtSource.Columns.Add("account_code", typeof(System.String));
            dtSource.Columns.Add("account_sub_code", typeof(System.String));
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("monthly_max_amount", typeof(System.String));
            dtSource.Columns.Add("monthly_max_percentage", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "bir_excempt_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "account_code","effective_date","account_sub_code" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //***************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string code = commandArgs[0];
            string sub_code = commandArgs[1];
            string descr = commandArgs[2];
            string effective_date = commandArgs[3];

            deleteRec1.Text = "Are you sure to delete this Record = (" + code.Trim() + ") - " + descr.Trim() + " ?";
            lnkBtnYes.CommandArgument = code + ", " + sub_code + ", " + effective_date;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string code = commandArgs[0];
            string sub_code = commandArgs[1];
            string effective_date = commandArgs[2];

            string deleteExpression = "account_code = '" + code.Trim() + "' AND effective_date = '" + effective_date.Trim() + "' AND account_sub_code = '" + sub_code.Trim() + "'";

            MyCmn.DeleteBackEndData("bir_excempt_tbl", "WHERE " + deleteExpression);

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
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string code = commandArgs[0];
            string sub_code = commandArgs[1];
            string effective_date = commandArgs[2];

            string editExpression = "account_code = '" + code.Trim() + "' AND effective_date = '" + effective_date.Trim() + "' AND account_sub_code = '" + sub_code.Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            RetrieveBindingAccount();
            DataRow nrow = dtSource.NewRow();
            nrow["account_code"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["account_sub_code"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);
           
            ddl_acct_code.SelectedValue = row2Edit[0]["account_code"].ToString();
            ddl_acct_code.Enabled = false;
            ddl_sub_acct_code.Enabled = false;
            tbx_effective_date.Enabled = false;
            RetrieveBindingSub_account();
            ddl_sub_acct_code.SelectedValue = row2Edit[0]["account_sub_code"].ToString();
            tbx_effective_date.Visible = false;
            tbx_effective_date1.Visible = true;
            tbx_effective_date1.Enabled = false;
            tbx_effective_date1.Text = effective_date.ToString().Trim();
            tbx_effective_date.Text = effective_date.ToString().Trim();

            if (row2Edit[0]["monthly_max_amount_percentage"].ToString().Trim().Substring(row2Edit[0]["monthly_max_amount_percentage"].ToString().Trim().Length - 1) == "%")
            {
                ddl_per_amt.SelectedIndex = 1;
                tbx_monthly_amt_per.Text = row2Edit[0]["monthly_max_amount_percentage"].ToString().Trim().Replace("%","");
            }
            else
            {
                ddl_per_amt.SelectedIndex = 2;
                tbx_monthly_amt_per.Text = row2Edit[0]["monthly_max_amount_percentage"].ToString().Trim();
            }



            UpdatePanel3.Update();

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
                if (ddl_per_amt.SelectedItem.ToString() == "%")
                {
                    if (tbx_monthly_amt_per.Text == "")
                    {
                        percentage = 0;
                    }
                    else
                    {
                        percentage = int.Parse(tbx_monthly_amt_per.Text.ToString());

                    }

                    amt = 0;

                }

                else if (ddl_per_amt.SelectedItem.ToString() == ".00")
                {
                    if (tbx_monthly_amt_per.Text == "")
                    {
                        amt = 0;
                    }

                    else
                    {
                        amt = float.Parse(tbx_monthly_amt_per.Text.ToString());
                    }

                    percentage = 0;

                }

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    dtSource.Rows[0]["account_code"] = ddl_acct_code.SelectedValue.ToString();
                    dtSource.Rows[0]["account_sub_code"] = ddl_sub_acct_code.SelectedValue.ToString();
                    dtSource.Rows[0]["effective_date"] = tbx_effective_date.Text.ToString();
                    dtSource.Rows[0]["monthly_max_amount"] = amt;
                    dtSource.Rows[0]["monthly_max_percentage"] = percentage;
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["account_code"] = ddl_acct_code.SelectedValue.ToString();
                    dtSource.Rows[0]["account_sub_code"] = ddl_sub_acct_code.SelectedValue.ToString();
                    dtSource.Rows[0]["effective_date"] = tbx_effective_date.Text.ToString();
                    dtSource.Rows[0]["monthly_max_amount"] = amt;
                    dtSource.Rows[0]["monthly_max_percentage"] = percentage;
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

                        if (amt == 0)
                        {
                            nrow["monthly_max_amount_percentage"] = percentage;
                        }

                        else
                        {
                            nrow["monthly_max_amount_percentage"] = amt;
                        }
                        nrow["account_code"] = ddl_acct_code.SelectedValue.ToString();
                        nrow["account_sub_code"] = ddl_sub_acct_code.SelectedValue.ToString();
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {

                        string editExpression = "account_code = '" + ddl_acct_code.SelectedValue.ToString() + " ' AND effective_date = '" + tbx_effective_date.Text.ToString() + "' AND account_sub_code = '" + ddl_sub_acct_code.SelectedValue.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        if (amt == 0)
                        {
                            row2Edit[0]["monthly_max_amount_percentage"] = percentage;
                        }

                        else
                        {
                            row2Edit[0]["monthly_max_amount_percentage"] = amt;
                        }
                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    RetrieveDataListGrid();
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
            //int loopstring = 0;
            //string searchtext = "";
            //if (txtb_search.Text.Trim().Length != 0)

            //    {

            //    loopstring = txtb_search.Text.Trim().Length;
            //    while (loopstring != 0)
            //        {
            //        searchtext = txtb_search.Text.Trim().Replace("%", "");
            //        loopstring = loopstring - 1;
            //        }

            //    }
            string searchExpression =
              "account_code LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR effective_date LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR account_sub_code LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR monthly_max_amount_percentage LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR account_title LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%'";



            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("account_code", typeof(System.String));
            dtSource1.Columns.Add("account_title", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("account_sub_code", typeof(System.String));
            dtSource1.Columns.Add("monthly_max_amount_percentage", typeof(System.String));
           
           
          

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

            if (ddl_acct_code.SelectedIndex == 0)
            {
                FieldValidationColorChanged(true, "ddl_acct_code");
                ddl_acct_code.Focus();
                validatedSaved = false;
            }

            //if (ddl_sub_acct_code.SelectedIndex == 0)
            //{
              
            //     FieldValidationColorChanged(true, "ddl_sub_acct_code");
            //     ddl_sub_acct_code.Focus();
            //     validatedSaved = false;
                
            //}


            if (ddl_per_amt.SelectedIndex == 0)
            {
                FieldValidationColorChanged(true, "ddl_per_amt");
                ddl_per_amt.Focus();
                validatedSaved = false;
            }

            if (tbx_effective_date.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "tbx_effective_date");
                tbx_effective_date.Focus();
                validatedSaved = false;
            }


            if (tbx_monthly_amt_per.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "tbx_monthly_amt_per");
                tbx_monthly_amt_per.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(tbx_monthly_amt_per) == false)
            {
                FieldValidationColorChanged(true, "invalid-amt-no");
                tbx_monthly_amt_per.Focus();
                validatedSaved = false;
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
                    case "ddl_acct_code":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_acct_code.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_monthly_amt_per":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            tbx_monthly_amt_per.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-amt-no":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            tbx_monthly_amt_per.BorderColor = Color.Red;
                            break;
                        }

                    case "ddl_per_amt":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            ddl_per_amt.BorderColor = Color.Red;
                            break;
                        }

                    case "tbx_effective_date":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            tbx_effective_date.BorderColor = Color.Red;
                            UpdatePanel3.Update();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal1();", true);
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

                            ddl_acct_code.BorderColor = Color.LightGray;
                            //ddl_sub_acct_code.BorderColor = Color.LightGray;
                            tbx_monthly_amt_per.BorderColor = Color.LightGray;
                            ddl_per_amt.BorderColor = Color.LightGray;
                            tbx_effective_date.BorderColor = Color.LightGray;
                            UpdatePanel3.Update();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "openModal1();", true);

                            break;
                        }

                }
            }
        }
    }
}