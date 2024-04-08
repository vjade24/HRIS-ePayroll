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

namespace HRIS_ePayroll.View.cPayTemplate
{
    public partial class cPayTemplate : System.Web.UI.Page
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
                    InitializePage();
                    Session["SortField"] = "payrolltemplate_code";
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
        //  BEGIN - JADE- 01/18/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            RetrieveEmploymentType();
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPositionTable"] = "cPositionTable";
            RetrieveDataListGrid();
            RetrieveTemplateType();

            Button1.Visible = false;
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

        private void RetrieveTemplateType()
        {
            ddl_template_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplatetype_tbl_list1");

            ddl_template_type.DataSource = dt;
            ddl_template_type.DataValueField = "templatetype_code";
            ddl_template_type.DataTextField = "templatetype_descr";
            ddl_template_type.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_template_type.Items.Insert(0, li);
        }

        private void RetrieveTemplateType1()
        {
            ddl_payroll_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplatetype_tbl_list1");

            ddl_payroll_type.DataSource = dt;
            ddl_payroll_type.DataValueField = "templatetype_code";
            ddl_payroll_type.DataTextField = "templatetype_descr";
            ddl_payroll_type.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_type.Items.Insert(0, li);
        }

        //*************************************************************************
        //  JADE - Jade- 09/28/2018 - Get the Last Control Number
        //*************************************************************************
        private void RetrieveLastRow()
        {
            string sql1 = "SELECT TOP 1 payrolltemplate_code from payrolltemplate_tbl where LEFT(payrolltemplate_code,3)=LEFT(payrolltemplate_code,3) order by payrolltemplate_code DESC";
            string last_row = MyCmn.GetLastRow_of_Table(sql1);
            if (last_row.Trim() == "")
            {
                last_row = "0";
            }
            int last_row1 = int.Parse(last_row) + 1;
            txtb_code.Text = last_row1.ToString().PadLeft(3, '0');
        }
        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list0", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_type", ddl_template_type.SelectedValue.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
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
            RetrieveLastRow();

            RetrieveTemplateType1();
            ddl_payroll_type.Enabled = false;
            ddl_payroll_type.SelectedValue = ddl_template_type.SelectedValue.ToString();

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
            txtb_code.ReadOnly = false;
            ddl_payment_mode.SelectedValue = "01";
            txtb_code.Focus();
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payrolltemplate_code"] = string.Empty;
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
            dtSource.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource.Columns.Add("payrolltemplate_short_descr", typeof(System.String));
            dtSource.Columns.Add("payrolltemplate_descr", typeof(System.String));
            dtSource.Columns.Add("payrolltemplate_type", typeof(System.String));
            dtSource.Columns.Add("employment_type", typeof(System.String));
            dtSource.Columns.Add("report_filename", typeof(System.String));
            dtSource.Columns.Add("crud_name", typeof(System.String));
            dtSource.Columns.Add("payment_mode", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrolltemplate_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payrolltemplate_code" };
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

            deleteRec1.Text = "Are you sure to delete this Payroll Template Code = (" + appttype.Trim() + ") - " + appttypedescr.Trim() + " ?";
            lnkBtnYes.CommandArgument = appttype;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string deleteExpression = "payrolltemplate_code = '" + svalues + "'";

            MyCmn.DeleteBackEndData("payrolltemplate_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);

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
            string editExpression = "payrolltemplate_code = '" + svalues + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            RetrieveTemplateType1();

            DataRow nrow = dtSource.NewRow();
            nrow["payrolltemplate_code"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);
            txtb_code.Text = svalues;
            txtb_descr.Text                       = row2Edit[0]["payrolltemplate_descr"].ToString();
            txtb_short_descr.Text                 = row2Edit[0]["payrolltemplate_short_descr"].ToString();
            ddl_payroll_type.SelectedValue        = ddl_template_type.SelectedValue.ToString();       
            ddl_payroll_type.Enabled              = false;
            txtb_report_filename.Text             = row2Edit[0]["report_filename"].ToString();
            txtb_crud_name.Text                   = row2Edit[0]["crud_name"].ToString();
            ddl_payment_mode.SelectedValue        = row2Edit[0]["payment_mode"].ToString();

            txtb_code.ReadOnly = true;
            LabelAddEdit.Text = "Edit Record: " + txtb_short_descr.Text.Trim();
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
                    dtSource.Rows[0]["payrolltemplate_code"]        = txtb_code.Text.ToString();
                    dtSource.Rows[0]["payrolltemplate_short_descr"] = txtb_short_descr.Text.ToString();
                    dtSource.Rows[0]["payrolltemplate_descr"]       = txtb_descr.Text.ToString();
                    dtSource.Rows[0]["payrolltemplate_type"]        = ddl_payroll_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["employment_type"]             = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["report_filename"]             = txtb_report_filename.Text.ToString().Trim();
                    dtSource.Rows[0]["crud_name"]                   = txtb_crud_name.Text.ToString().Trim();
                    dtSource.Rows[0]["payment_mode"]                = ddl_payment_mode.SelectedValue.ToString().Trim();
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payrolltemplate_code"]        = txtb_code.Text.ToString();
                    dtSource.Rows[0]["payrolltemplate_short_descr"] = txtb_short_descr.Text.ToString();
                    dtSource.Rows[0]["payrolltemplate_descr"]       = txtb_descr.Text.ToString();
                    dtSource.Rows[0]["payrolltemplate_type"]        = ddl_payroll_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["employment_type"]             = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["report_filename"]             = txtb_report_filename.Text.ToString().Trim();
                    dtSource.Rows[0]["crud_name"]                   = txtb_crud_name.Text.ToString().Trim();
                    dtSource.Rows[0]["payment_mode"]                = ddl_payment_mode.SelectedValue.ToString().Trim();
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
                        nrow["payrolltemplate_code"]        = txtb_code.Text.ToString();
                        nrow["payrolltemplate_short_descr"] = txtb_short_descr.Text.ToString();
                        nrow["payrolltemplate_descr"]       = txtb_descr.Text.ToString();
                        nrow["payrolltemplate_type"]        = ddl_payroll_type.SelectedValue.ToString().Trim();
                        nrow["report_filename"]             = txtb_report_filename.Text.ToString().Trim();
                        nrow["crud_name"]                   = txtb_crud_name.Text.ToString().Trim();
                        nrow["payment_mode"]                = ddl_payment_mode.SelectedValue.ToString().Trim();
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        //gv_dataListGrid.SelectRow(gv_dataListGrid.Rows.Count - 1);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "payrolltemplate_code = '" + txtb_code.Text.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        row2Edit[0]["payrolltemplate_code"]         = txtb_code.Text.ToString();
                        row2Edit[0]["payrolltemplate_short_descr"]  = txtb_short_descr.Text.ToString();
                        row2Edit[0]["payrolltemplate_descr"]        = txtb_descr.Text.ToString();
                        row2Edit[0]["payrolltemplate_type"]         = ddl_payroll_type.SelectedValue.ToString().Trim();
                        row2Edit[0]["report_filename"]              = txtb_report_filename.Text.ToString().Trim();
                        row2Edit[0]["crud_name"]                    = txtb_crud_name.Text.ToString().Trim();
                        row2Edit[0]["payment_mode"]                 = ddl_payment_mode.SelectedValue.ToString().Trim();
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
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JADE- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "payrolltemplate_code LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR payrolltemplate_short_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR payrolltemplate_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR report_filename LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR templatetype_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_short_descr", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_descr", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_type", typeof(System.String));
            dtSource1.Columns.Add("report_filename", typeof(System.String));
            dtSource1.Columns.Add("templatetype_descr", typeof(System.String));
            dtSource1.Columns.Add("crud_name", typeof(System.String));
            dtSource1.Columns.Add("payment_mode", typeof(System.String));

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

            if(ddl_payroll_type.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_payroll_type");
                ddl_payroll_type.Focus();
                validatedSaved = false;
            }
            //if (txtb_report_filename.Text == "")
            //{
            //    FieldValidationColorChanged(true, "txtb_report_filename");
            //    txtb_report_filename.Focus();
            //    validatedSaved = false;
            //}
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
            if (ddl_payment_mode.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_payment_mode");
                ddl_payment_mode.Focus();
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
                    case "txtb_short_descr":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_short_descr.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_payroll_type":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            ddl_payroll_type.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_descr":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_descr.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_report_filename":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_report_filename.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_payment_mode":
                        {
                            LblRequired300.Text = MyCmn.CONST_RQDFLD;
                            ddl_payment_mode.BorderColor = Color.Red;
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
                            LblRequired300.Text = "";
                            txtb_descr.BorderColor = Color.LightGray;
                            txtb_short_descr.BorderColor = Color.LightGray;
                            ddl_payroll_type.BorderColor = Color.LightGray;
                            txtb_report_filename.BorderColor = Color.LightGray;
                            ddl_payment_mode.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddl_empl_type.SelectedValue != "" && ddl_template_type.SelectedValue != "")
            {
                
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false;
            }

            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }

        protected void ddl_template_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_template_type.SelectedValue != "")
            {
                Button1.Visible = true;
            }
            else
            {
                Button1.Visible = false;
            }

            RetrieveDataListGrid();
            up_dataListGrid.Update();
        }
    }
}