//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Salary Increase Table
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     2020-07-29      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cSalIncreaseEff
{
    public partial class cSalIncreaseEff : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 2020-07-29 - Data Place holder creation 
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
        //  BEGIN - VJA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - VJA- 2020-07-29 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    InitializePage();
                    Session["SortField"] = "payroll_year";
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
        //  BEGIN - VJA- 2020-07-29 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            RetrieveYear();
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cSalIncreaseEff"] = "cSalIncreaseEff";
            RetrieveDataListGrid();
        }

        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveYear()
        {
            ddl_payroll_year.Items.Clear();
            int years = Convert.ToInt32(DateTime.Now.Year);
            int prev_year = years - 5;
            for (int x = 0; x < 12; x++)
            {
                ListItem li2 = new ListItem(prev_year.ToString(), prev_year.ToString());
                ddl_payroll_year.Items.Insert(x, li2);
                if (prev_year == years)
                {
                    ListItem li3 = new ListItem((years + 1).ToString(), (years + 1).ToString());
                    ddl_payroll_year.Items.Insert(x + 1, li3);
                    ddl_payroll_year.SelectedValue = years.ToString();
                    break;
                }
                prev_year = prev_year + 1;
            }
        }
        
        //*************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_salary_increase_tbl_list");
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            txtb_increase_date.Enabled  = true;
            ddl_payroll_year.Enabled    = true;
            UpdateEff.Update();

            div_footer.Visible = true;
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_increase_date.Text="";
            //ddl_payroll_year.SelectedValue ="";
        }

        //*************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["increase_date"] = string.Empty;
            nrow["created_by_user"] = string.Empty;
            nrow["updated_by_user"] = string.Empty;
            nrow["created_dttm"] = string.Empty;
            nrow["updated_dttm"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("increase_date", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "salary_increase_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "increase_date" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //***************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string payroll_year     = commandArgs[0];
            string effective_date   = commandArgs[1];

            deleteRec1.Text = "Are you sure to delete this Record ?";
            lnkBtnYes.CommandArgument = payroll_year + "," + effective_date;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "payroll_year = '" + svalues[0].Trim() + "' AND increase_date = '" + svalues[1].Trim() + "'";

            MyCmn.DeleteBackEndData("salary_increase_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "payroll_year = '" + svalues[0].Trim() + "' AND increase_date = '" + svalues[1].Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["increase_date"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            ddl_payroll_year.SelectedValue = row2Edit[0]["payroll_year"].ToString();
            txtb_increase_date.Text        = row2Edit[0]["increase_date"].ToString();
            
            txtb_increase_date.Enabled  = false;
            ddl_payroll_year.Enabled    = false;
            LabelAddEdit.Text = "Edit Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            FieldValidationColorChanged(false, "ALL");

            div_footer.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Change Field Sort mode  
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
        //  BEGIN - VJA- 2020-07-29 - Get Grid current sort order 
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
        //  BEGIN - VJA- 2020-07-29 - Save New Record/Edited Record to back end DB
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
                    dtSource.Rows[0]["payroll_year"]     = ddl_payroll_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["increase_date"]    = txtb_increase_date.Text.ToString().Trim();
                    dtSource.Rows[0]["created_by_user"]  = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_by_user"]  = "";
                    dtSource.Rows[0]["created_dttm"]     = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["updated_dttm"]     = "";
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"]     = ddl_payroll_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["increase_date"]    = txtb_increase_date.Text.ToString().Trim();
                    dtSource.Rows[0]["created_by_user"]  = dtSource.Rows[0]["created_by_user"].ToString(); 
                    dtSource.Rows[0]["updated_by_user"]  = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["created_dttm"]     = dtSource.Rows[0]["created_dttm"].ToString();
                    dtSource.Rows[0]["updated_dttm"]     = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
                        return;
                    } 


                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();
                        nrow["payroll_year"]     = ddl_payroll_year.SelectedValue.ToString().Trim();
                        nrow["increase_date"]    = txtb_increase_date.Text.ToString().Trim();
                        nrow["created_by_user"]  = Session["ep_user_id"].ToString();
                        nrow["updated_by_user"]  = "";
                        nrow["created_dttm"]     = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        nrow["updated_dttm"]     = Convert.ToDateTime("1900-01-01");
                        //nrow["updated_dttm"]     = "";
                       
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        h2_status.InnerText = "SUCCESSFULLY ADDED!";
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "payroll_year = '" + ddl_payroll_year.SelectedValue.ToString().Trim() + "' AND increase_date = '" + txtb_increase_date.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        row2Edit[0]["payroll_year"]     = ddl_payroll_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["increase_date"]    = txtb_increase_date.Text.ToString().Trim();
                        row2Edit[0]["created_by_user"]  = dtSource.Rows[0]["created_by_user"].ToString(); 
                        row2Edit[0]["updated_by_user"]  = Session["ep_user_id"].ToString();
                        row2Edit[0]["created_dttm"]     = dtSource.Rows[0]["created_dttm"].ToString();
                        row2Edit[0]["updated_dttm"]     = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                       
                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        h2_status.InnerText = "SUCCESSFULLY UPDATED!";
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "closeModal();", true);
                }
                ViewState.Remove("AddEdit_Mode");
                show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
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
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "payroll_year LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                "OR increase_date LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' ";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("increase_date", typeof(System.String));
            
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
        //  BEGIN - VJA- 2020-07-29 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 2020-07-29 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");
            if (ddl_payroll_year.SelectedValue == "" )
            {
                FieldValidationColorChanged(true, "ddl_payroll_year");
                ddl_payroll_year.Focus();
                validatedSaved = false;
            }
            else if (CommonCode.checkisdatetime(txtb_increase_date) == false)
            {
                FieldValidationColorChanged(true, "txtb_increase_date");
                txtb_increase_date.Focus();
                validatedSaved = false;
            }
            else
            {
                FieldValidationColorChanged(false, "ALL");
            }
            
            UpdateEff.Update();
            return validatedSaved;
        }

        //**************************************************************************
        //  BEGIN - VJA- 2020-07-29 - Check if Object already contains value  
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
        //  BEGIN - VJA- 2020-07-29 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_payroll_year":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_payroll_year.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_increase_date":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_increase_date.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            //LblRequired1.Text = "already exist!";
                            LblRequired2.Text = "already exist!";
                            //ddl_payroll_year.BorderColor = Color.Red;
                            txtb_increase_date.BorderColor = Color.Red;
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
                            txtb_increase_date.BorderColor = Color.LightGray;
                            ddl_payroll_year.BorderColor = Color.LightGray;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            UpdateEff.Update();
                            break;
                        }

                }
            }
        }

    }
}