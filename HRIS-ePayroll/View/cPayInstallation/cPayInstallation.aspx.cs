//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORYbtnAdd_Click
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     02/16/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cPayInstallation
{
    public partial class cPayInstallation : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 09/20/2018 - Data Place holder creation 
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
        public string var_include_history = "N";

        //********************************************************************
        //  BEGIN - VJA- 09/20/2018 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    InitializePage();
                    Session["SortField"] = "effective_date";
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
        //  BEGIN - VJA- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            
            RetrieveYear();
            RetrieveEmploymentType();

            RetrieveDataListGrid();
            btnAdd.Visible = false;
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
        //  BEGIN - VJA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollinstallation_tbl_list", "par_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(),"par_include_history", var_include_history);
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Populate Combo list for Emplyment Type
        //*************************************************************************
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
        //  BEGIN - VJA- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            //For Header
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            txtb_effective_date.Enabled = true;
            txtb_effective_date.Visible = true;
            txtb_effective_date_edit.Visible = false;

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_effective_date.Text = "";
            txtb_ms_days.Text        = "";
            txtb_pera_days.Text      = "";
            txtb_hourstoday.Text     = "";
            txtb_ot_days.Text        = "";
            txtb_min_pay.Text        = "";
            txtb_mone.Text           = "";
            txtb_effective_date_edit.Text = "";
        }

        //*************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("employment_type", typeof(System.String));
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("pera_days_conv", typeof(System.String));
            dtSource.Columns.Add("monthly_salary_days_conv", typeof(System.String));
            dtSource.Columns.Add("monthly_ot_days_conv", typeof(System.String));
            dtSource.Columns.Add("hours_in_1day_conv", typeof(System.String));
            dtSource.Columns.Add("minimum_net_pay", typeof(System.String));
            dtSource.Columns.Add("mone_constant_factor", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollinstallation_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "employment_type","effective_date" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["employment_type"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["pera_days_conv"] = string.Empty;
            nrow["monthly_salary_days_conv"] = string.Empty;
            nrow["monthly_ot_days_conv"] = string.Empty;
            nrow["hours_in_1day_conv"] = string.Empty;
            nrow["minimum_net_pay"] = string.Empty;
            nrow["mone_constant_factor"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Trim().Split(new char[] { ',' });
            string peffective_date = commandArgs[0].ToString().Trim();

            deleteRec1.Text = "Are you sure to delete this information?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string deleteExpression = "payroll_year = '" + svalues[0].Trim() + "' AND employment_type = '" + svalues[1].Trim() + "' AND effective_date = '" + svalues[2].Trim() + "'";

            MyCmn.DeleteBackEndData("payrollinstallation_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string editExpression = "payroll_year = '" + svalues[0].Trim() + "' AND employment_type = '" + svalues[1].Trim() + "' AND effective_date = '" + svalues[2].Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();

            DataRow nrow = dtSource.NewRow();
            nrow["action"]          = 2;
            nrow["retrieve"]        = true;
            nrow["payroll_year"]    = string.Empty;
            nrow["effective_date"]  = string.Empty;
            nrow["employment_type"] = string.Empty;
            dtSource.Rows.Add(nrow);

            txtb_effective_date.Visible      = false;
            txtb_effective_date_edit.Visible = true;
            txtb_effective_date.Text         = row2Edit[0]["effective_date"].ToString().Trim();
            txtb_effective_date_edit.Text    = row2Edit[0]["effective_date"].ToString().Trim();
            txtb_pera_days.Text              = row2Edit[0]["pera_days_conv"].ToString().Trim();
            txtb_ms_days.Text                = row2Edit[0]["monthly_salary_days_conv"].ToString().Trim();
            txtb_hourstoday.Text             = row2Edit[0]["hours_in_1day_conv"].ToString().Trim();
            txtb_ot_days.Text                = row2Edit[0]["monthly_ot_days_conv"].ToString().Trim();
            txtb_min_pay.Text                = row2Edit[0]["minimum_net_pay"].ToString().Trim();
            txtb_mone.Text                   = row2Edit[0]["mone_constant_factor"].ToString().Trim();


            txtb_effective_date.Enabled = false;
            LabelAddEdit.Text = "Edit Record: " + svalues[0].ToString().Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;

            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //**************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Change Field Sort mode  
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
        //  BEGIN - VJA- 09/20/2018 - Get Grid current sort order 
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
        //  BEGIN - VJA- 09/20/2018 - Save New Record/Edited Record to back end DB
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
                   
                    dtSource.Rows[0]["payroll_year"]             = ddl_year.SelectedValue.ToString().Trim(); 
                    dtSource.Rows[0]["employment_type"]          = ddl_empl_type.SelectedValue.ToString().Trim(); 
                    dtSource.Rows[0]["effective_date"]           = txtb_effective_date.Text.ToString().Trim(); 
                    dtSource.Rows[0]["pera_days_conv"]           = txtb_pera_days.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_salary_days_conv"] = txtb_ms_days.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_ot_days_conv"]     = txtb_ot_days.Text.ToString().Trim();
                    dtSource.Rows[0]["hours_in_1day_conv"]       = txtb_hourstoday.Text.ToString().Trim();
                    dtSource.Rows[0]["minimum_net_pay"]          = txtb_min_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["mone_constant_factor"]     = txtb_mone.Text.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"]             = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["employment_type"]          = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["effective_date"]           = txtb_effective_date.Text.ToString().Trim();
                    dtSource.Rows[0]["pera_days_conv"]           = txtb_pera_days.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_salary_days_conv"] = txtb_ms_days.Text.ToString().Trim();
                    dtSource.Rows[0]["monthly_ot_days_conv"]     = txtb_ot_days.Text.ToString().Trim();
                    dtSource.Rows[0]["hours_in_1day_conv"]       = txtb_hourstoday.Text.ToString().Trim();
                    dtSource.Rows[0]["minimum_net_pay"]          = txtb_min_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["mone_constant_factor"]     = txtb_mone.Text.ToString().Trim();
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

                        nrow["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                        nrow["employment_type"]         = ddl_empl_type.SelectedValue.ToString().Trim();
                        nrow["effective_date"]          = txtb_effective_date.Text.ToString().Trim();
                        nrow["pera_days_conv"]          = txtb_pera_days.Text.ToString().Trim();
                        nrow["monthly_salary_days_conv"]= txtb_ms_days.Text.ToString().Trim();
                        nrow["monthly_ot_days_conv"]    = txtb_ot_days.Text.ToString().Trim();
                        nrow["hours_in_1day_conv"]      = txtb_hourstoday.Text.ToString().Trim();
                        nrow["minimum_net_pay"]         = txtb_min_pay.Text.ToString().Trim();
                        nrow["mone_constant_factor"]    = txtb_mone.Text.ToString().Trim();

                        dataListGrid.Rows.Add(nrow);

                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;

                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "effective_date = '" + txtb_effective_date.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"]             = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["employment_type"]          = ddl_empl_type.SelectedValue.ToString().Trim();
                        row2Edit[0]["effective_date"]           = txtb_effective_date.Text.ToString().Trim();
                        row2Edit[0]["pera_days_conv"]           = txtb_pera_days.Text.ToString().Trim();
                        row2Edit[0]["monthly_salary_days_conv"] = txtb_ms_days.Text.ToString().Trim();
                        row2Edit[0]["monthly_ot_days_conv"]     = txtb_ot_days.Text.ToString().Trim();
                        row2Edit[0]["hours_in_1day_conv"]       = txtb_hourstoday.Text.ToString().Trim();
                        row2Edit[0]["minimum_net_pay"]          = txtb_min_pay.Text.ToString().Trim();
                        row2Edit[0]["mone_constant_factor"]     = txtb_mone.Text.ToString().Trim();

                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "closeModal();", true);
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
            string searchExpression = "effective_date LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR payroll_year LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR employment_type LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR pera_days_conv LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR monthly_salary_days_conv LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR monthly_ot_days_conv LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR hours_in_1day_conv LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR minimum_net_pay LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR mone_constant_factor LIKE '%"
                + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("employment_type", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("pera_days_conv", typeof(System.String));
            dtSource1.Columns.Add("monthly_salary_days_conv", typeof(System.String));
            dtSource1.Columns.Add("monthly_ot_days_conv", typeof(System.String));
            dtSource1.Columns.Add("hours_in_1day_conv", typeof(System.String));
            dtSource1.Columns.Add("minimum_net_pay", typeof(System.String));
            dtSource1.Columns.Add("mone_constant_factor", typeof(System.String));



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
        //  BEGIN - VJA- 09/20/2018 - Define Property for Sort Direction  
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
        //  BEGIN - VJA- 09/20/2018 - Check if Object already contains value  
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
        //  BEGIN - VJA- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");
            
             
            if (CommonCode.checkisdatetime(txtb_effective_date.Text) == false)
            {
                if (txtb_effective_date.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_effective_date");
                    txtb_effective_date.Focus();
                    Update_effective_date.Update();
                    validatedSaved = false;
                }
                else if (txtb_effective_date.Text != "")
                {
                    FieldValidationColorChanged(true, "invalid-date");
                    txtb_effective_date.Focus();
                    Update_effective_date.Update();
                    validatedSaved = false;
                }

                Update_effective_date.Update();

            }

            if (txtb_effective_date.Text == "" && ViewState["AddEdit_Mode"].ToString().Trim() == MyCmn.CONST_ADD)
                {
                    FieldValidationColorChanged(true, "txtb_effective_date");
                    txtb_effective_date.Focus();
                    validatedSaved = false;
                }


                if (CommonCode.checkisdecimal(txtb_pera_days) == false)
                {
                    if (txtb_pera_days.Text == "")
                    {
                        FieldValidationColorChanged(true, "txtb_pera_days");
                        txtb_pera_days.Focus();
                        validatedSaved = false;
                    }

                    else
                    {
                        FieldValidationColorChanged(true, "invalid-numeric-1");
                        txtb_pera_days.Focus();
                        validatedSaved = false;
                    }
                }

                if (CommonCode.checkisdecimal(txtb_ms_days) == false)
                {
                    if (txtb_ms_days.Text == "")
                    {
                        FieldValidationColorChanged(true, "txtb_ms_days");
                        txtb_ms_days.Focus();
                        validatedSaved = false;
                    }

                    else
                    {
                        FieldValidationColorChanged(true, "invalid-numeric-2");
                        txtb_ms_days.Focus();
                        validatedSaved = false;
                    }
                }

                if (CommonCode.checkisdecimal(txtb_ot_days) == false)
                {
                    if (txtb_ot_days.Text == "")
                    {
                        FieldValidationColorChanged(true, "txtb_ot_days");
                        txtb_ot_days.Focus();
                        validatedSaved = false;
                    }

                    else
                    {
                        FieldValidationColorChanged(true, "invalid-numeric-3");
                        txtb_ot_days.Focus();
                        validatedSaved = false;
                    }
                }

                if (CommonCode.checkisdecimal(txtb_hourstoday) == false)
                {
                    if (txtb_hourstoday.Text == "")
                    {
                        FieldValidationColorChanged(true, "txtb_hourstoday");
                        txtb_hourstoday.Focus();
                        validatedSaved = false;
                    }

                    else
                    {
                        FieldValidationColorChanged(true, "invalid-numeric-4");
                        txtb_hourstoday.Focus();
                        validatedSaved = false;
                    }
                }

                if (CommonCode.checkisdecimal(txtb_min_pay) == false)
                {
                    if (txtb_min_pay.Text == "")
                    {
                        FieldValidationColorChanged(true, "txtb_min_pay");
                        txtb_min_pay.Focus();
                        validatedSaved = false;
                    }

                    else
                    {
                        FieldValidationColorChanged(true, "invalid-numeric-5");
                        txtb_min_pay.Focus();
                        validatedSaved = false;
                    }
                }

                if (CommonCode.checkisdecimal(txtb_mone) == false)
                {
                    if (txtb_mone.Text == "")
                    {
                        FieldValidationColorChanged(true, "txtb_mone");
                        txtb_mone.Focus();
                        validatedSaved = false;
                    }

                    else
                    {
                        FieldValidationColorChanged(true, "invalid-numeric-6");
                        txtb_mone.Focus();
                        validatedSaved = false;
                    }
                }


            
            return validatedSaved;

        }
        //**********************************************************************************************
        //  BEGIN - VJA- 09/20/2018 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "txtb_effective_date":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_effective_date.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_ms_days":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_ms_days.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_pera_days":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_pera_days.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_ot_days":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_ot_days.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_hourstoday":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            txtb_hourstoday.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_min_pay":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            txtb_min_pay.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_mone":
                        {
                            LblRequired7.Text = MyCmn.CONST_RQDFLD;
                            txtb_mone.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-date":
                        {
                            LblRequired1.Text = "Invalid Date!";
                            txtb_effective_date.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-numeric-1":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_pera_days.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-numeric-2":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_ms_days.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-numeric-3":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_ot_days.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-numeric-4":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_hourstoday.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-numeric-5":
                        {
                            LblRequired6.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_min_pay.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-numeric-6":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_mone.BorderColor = Color.Red;
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
                            LblRequired7.Text = "";

                            txtb_effective_date.BorderColor = Color.LightGray;
                            txtb_ms_days.BorderColor        = Color.LightGray;
                            txtb_pera_days.BorderColor      = Color.LightGray;
                            txtb_hourstoday.BorderColor     = Color.LightGray;
                            txtb_mone.BorderColor           = Color.LightGray;
                            txtb_min_pay.BorderColor        = Color.LightGray;
                            txtb_ot_days.BorderColor        = Color.LightGray;

                            Update_effective_date.Update();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "show_date();", true);
                            break;
                        }

                }
            }
        }
        //*************************************************************************
        //  BEGIN - VJA- 09/09/2018 - Triggers When Select Payroll Year
        //*************************************************************************
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_year.SelectedValue != "")
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
        
    }
}