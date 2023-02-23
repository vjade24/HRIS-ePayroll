//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for PHIC Setup
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     05/27/2020    Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cPHICSetup
{
    public partial class cPHICSetup : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 05/27/2020 - Data Place holder creation 
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
        //  BEGIN - VJA- 05/27/2020 - Public Variable used in Add/Edit Mode
        //********************************************************************
        CommonDB MyCmn = new CommonDB();
        //********************************************************************
        //  BEGIN - VJA- 05/27/2020 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "effective_date";
                    if (Session["SortOrder"] == null)
                        Session["SortOrder"] = "DESC";
                    InitializePage();
                }
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 05/27/2020 - Page Load Complete method
        //********************************************************************
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    ViewState["page_allow_add"]          = Master.allow_add;
                    ViewState["page_allow_delete"]       = Master.allow_delete;
                    ViewState["page_allow_edit"]         = Master.allow_edit;
                    ViewState["page_allow_edit_history"] = Master.allow_edit_history;
                    ViewState["page_allow_print"]        = Master.allow_print;
            }
        }
        //********************************************************************
        //  BEGIN - VJA- 05/27/2020 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            RetrieveDataListGrid();
            RetriveEmploymentType();
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_philhealth_tbl_list");
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            txtb_effective_date.Enabled = true;
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_effective_date.Text        = "";
            txtb_salary_flooring_amt.Text   = "";
            txtb_salary_ceiling_amt.Text    = "";
            txtb_contribution_rate.Text     = "";
            txtb_philhealth_remarks.Text    = "";
            
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("salary_flooring_amt", typeof(System.String));
            dtSource.Columns.Add("salary_ceiling_amt", typeof(System.String));
            dtSource.Columns.Add("contribution_rate", typeof(System.String));
            dtSource.Columns.Add("philhealth_remarks", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "philhealth_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "effective_date" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["effective_date"] = string.Empty;
            nrow["salary_flooring_amt"] = string.Empty;
            nrow["salary_ceiling_amt"] = string.Empty;
            nrow["contribution_rate"] = string.Empty;
            nrow["philhealth_remarks"] = string.Empty;
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string eff_date = commandArgs[0];
            
            deleteRec1.Text         = "Are you sure to delete this Record ?";
            delete_header.InnerText = "Delete this Record";
            lnkBtnYes.Text          = " Yes, Delete it ";
            
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);

        }
        //*************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "effective_date = '" + commandarg[0].Trim() + "' ";
            
            MyCmn.DeleteBackEndData("philhealth_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "effective_date = '" + svalues[0].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["effective_date"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_effective_date.Text      = row2Edit[0]["effective_date"].ToString();
            txtb_salary_flooring_amt.Text =  row2Edit[0]["salary_flooring_amt"].ToString();
            txtb_salary_ceiling_amt.Text  =  row2Edit[0]["salary_ceiling_amt"].ToString();
            txtb_contribution_rate.Text   =  row2Edit[0]["contribution_rate"].ToString();
            txtb_philhealth_remarks.Text =   row2Edit[0]["philhealth_remarks"].ToString();

            txtb_effective_date.Enabled = false;
            LabelAddEdit.Text = "Edit Existing Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Change Field Sort mode  
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
        //  BEGIN - VJA- 05/27/2020 - Get Grid current sort order 
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
        //  BEGIN - VJA- 05/27/2020 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**********************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void SearchData(string search)
        {
            string searchExpression =
               "effective_date LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "salary_flooring_amt LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "salary_ceiling_amt LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "contribution_rate LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "philhealth_remarks LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";
            
            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("salary_flooring_amt", typeof(System.String));
            dtSource1.Columns.Add("salary_ceiling_amt", typeof(System.String));
            dtSource1.Columns.Add("contribution_rate", typeof(System.String));
            dtSource1.Columns.Add("philhealth_remarks", typeof(System.String));
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
        //  BEGIN - VJA- 05/27/2020 - Define Property for Sort Direction  
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
                    dtSource.Rows[0]["effective_date"]       = txtb_effective_date.Text.ToString().Trim();
                    dtSource.Rows[0]["salary_flooring_amt"]  = txtb_salary_flooring_amt.Text.ToString().Trim();
                    dtSource.Rows[0]["salary_ceiling_amt"]   = txtb_salary_ceiling_amt.Text.ToString().Trim();
                    dtSource.Rows[0]["contribution_rate"]    = txtb_contribution_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["philhealth_remarks"]   = txtb_philhealth_remarks.Text.ToString().Trim();
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);
                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["effective_date"]       = txtb_effective_date.Text.ToString().Trim();
                    dtSource.Rows[0]["salary_flooring_amt"]  = txtb_salary_flooring_amt.Text.ToString().Trim();
                    dtSource.Rows[0]["salary_ceiling_amt"]   = txtb_salary_ceiling_amt.Text.ToString().Trim();
                    dtSource.Rows[0]["contribution_rate"]    = txtb_contribution_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["philhealth_remarks"]   = txtb_philhealth_remarks.Text.ToString().Trim();
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

                        nrow["effective_date"]       = txtb_effective_date.Text.ToString().Trim();
                        nrow["salary_flooring_amt"]  = txtb_salary_flooring_amt.Text.ToString().Trim();
                        nrow["salary_ceiling_amt"]   = txtb_salary_ceiling_amt.Text.ToString().Trim();
                        nrow["contribution_rate"]    = txtb_contribution_rate.Text.ToString().Trim();
                        nrow["philhealth_remarks"]   = txtb_philhealth_remarks.Text.ToString().Trim();
                        
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                        h2_status.InnerText = "SUCCESSFULY SAVED";
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "effective_date = '" + txtb_effective_date.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        row2Edit[0]["effective_date"]       = txtb_effective_date.Text.ToString().Trim();
                        row2Edit[0]["salary_flooring_amt"]  = txtb_salary_flooring_amt.Text.ToString().Trim();
                        row2Edit[0]["salary_ceiling_amt"]   = txtb_salary_ceiling_amt.Text.ToString().Trim();
                        row2Edit[0]["contribution_rate"]    = txtb_contribution_rate.Text.ToString().Trim();
                        row2Edit[0]["philhealth_remarks"]   = txtb_philhealth_remarks.Text.ToString().Trim();
                        
                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                        h2_status.InnerText = "SUCCESSFULY UPDATED";
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                }
                ViewState.Remove("AddEdit_Mode");
                show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                up_dataListGrid.Update();
            }
        }
        //**************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            
            if (CommonCode.checkisdatetime(txtb_effective_date) == false)
            {
                FieldValidationColorChanged(true, "txtb_effective_date");
                txtb_effective_date.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_salary_flooring_amt) == false)
            {
                FieldValidationColorChanged(true, "txtb_salary_flooring_amt");
                txtb_salary_flooring_amt.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_salary_ceiling_amt) == false)
            {
                FieldValidationColorChanged(true, "txtb_salary_ceiling_amt");
                txtb_salary_ceiling_amt.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_contribution_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_contribution_rate");
                txtb_contribution_rate.Focus();
                validatedSaved = false;
            }
            if (txtb_philhealth_remarks.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_philhealth_remarks");
                txtb_philhealth_remarks.Focus();
                validatedSaved = false;
            }
            return validatedSaved;
        }
        //**************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated_Oth()
        {
            bool validatedSaved = true;
            if (CommonCode.checkisdatetime(txtb_effective_date_generate) == false)
            {
                FieldValidationColorChanged(true, "txtb_effective_date_generate");
                ddl_empl_type.Focus();
                validatedSaved = false;
            }
            else if (Convert.ToDateTime(txtb_effective_date_generate.Text) < Convert.ToDateTime(ViewState["txtb_effective_date_generate"].ToString()))
            {
                LblRequired6.Text = "date is less-than " + ViewState["txtb_effective_date_generate"].ToString();
                txtb_effective_date_generate.BorderColor = Color.Red;
                UpdateEffective_generate.Update();
                validatedSaved = false;
            }
            if (ddl_empl_type.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_empl_type");
                ddl_empl_type.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_fix_rate) == false)
            {
                FieldValidationColorChanged(true, "txtb_fix_rate");
                txtb_fix_rate.Focus();
                validatedSaved = false;
            }

            return validatedSaved;
        }
        //**********************************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "txtb_effective_date":
                        {
                            LblRequired1.Text = "Invalid Date!";
                            txtb_effective_date.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_salary_flooring_amt":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_salary_flooring_amt.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_salary_ceiling_amt":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_salary_ceiling_amt.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_contribution_rate":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_contribution_rate.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_philhealth_remarks":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            txtb_philhealth_remarks.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text = "already exist!";
                            txtb_effective_date.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_effective_date_generate":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            txtb_effective_date_generate.BorderColor = Color.Red;
                            UpdateEffective_generate.Update();
                            break;
                        }
                    case "ddl_empl_type":
                        {
                            LblRequired7.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_type.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_fix_rate":
                        {
                            LblRequired8.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_fix_rate.BorderColor = Color.Red;
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
                            LblRequired8.Text = "";
                            txtb_effective_date.BorderColor          = Color.LightGray;
                            txtb_salary_flooring_amt.BorderColor     = Color.LightGray;
                            txtb_salary_ceiling_amt.BorderColor      = Color.LightGray;
                            txtb_contribution_rate.BorderColor       = Color.LightGray;
                            txtb_philhealth_remarks.BorderColor      = Color.LightGray;
                            txtb_effective_date_generate.BorderColor = Color.LightGray;
                            ddl_empl_type.BorderColor                = Color.LightGray;
                            txtb_fix_rate.BorderColor                = Color.LightGray;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDate", "show_date();", true);
                            UpdateEffective.Update();
                            UpdateEffective_generate.Update();
                            break;
                        }

                }
            }
        }


        //***************************************************************************
        //  BEGIN - VJA- 05/27/2020 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void generate_Command(object sender, CommandEventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string eff_date = commandArgs[0].ToString().Trim();

            txtb_effective_date_generate.Text = "";
            ddl_empl_type.SelectedValue = "";
            txtb_fix_rate.Text = "0.00";

            txtb_effective_date_generate.Text           = eff_date;
            ViewState["txtb_effective_date_generate"]   = eff_date;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openGenerate();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Employment Type
        //*************************************************************************
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
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Employment Type
        //*************************************************************************
        protected void lnkbtn_generate_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");

            if (IsDataValidated_Oth())
            {
                DataTable dt_result = MyCmn.RetrieveData("sp_phic_auto_update", "p_effective_date",txtb_effective_date_generate.Text.ToString().Trim(), "p_deduc_date_from", ViewState["txtb_effective_date_generate"].ToString().Trim(), "p_employment_type",ddl_empl_type.SelectedValue.ToString().Trim(), "p_fixed_rate",txtb_fix_rate.Text.ToString().Trim(), "p_user_id", Session["ep_user_id"].ToString().Trim());
                
                if (dt_result != null && dt_result.Rows.Count > 0)
                {
                    switch (dt_result.Rows[0]["result_flag"].ToString().Trim())
                    {
                        case "E":
                            i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                            h2_status.InnerText = "NOT GENERATED";
                            SaveAddEdit.Text = dt_result.Rows[0]["result_flag_message"].ToString();
                            break;

                        default:
                            i_icon_display.Attributes.Add("class", "fa-5x fa fa-check-circle text-success");
                            h2_status.InnerText = "SUCCESSFULLY GENERATED";
                            SaveAddEdit.Text = dt_result.Rows[0]["result_flag_message"].ToString();
                            break;
                    }
                }
                else
                {
                    i_icon_display.Attributes.Add("class", "fa-5x fa fa-warning text-danger");
                    h2_status.InnerText = "NOT GENERATED";
                    SaveAddEdit.Text = "Stored Procedure Error !";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openLoadingAndClose();", true);
            }
            
        }
    }
}