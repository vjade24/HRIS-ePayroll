//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for cPayCertification Page
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

namespace HRIS_ePayroll.View.cOthrPaySetup
{
    public partial class cOthrPaySetup : System.Web.UI.Page
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
        //  BEGIN - JRV- 03/22/2019  - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cOthrPaySetup"] = "cOthrPaySetup";
            RetrieveEmploymentType();
            RetrieveDataListGrid();
            

        }


        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_othrpaysetup_tbl_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString());
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();

        }
        

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list from Salary Grade Table
        //*************************************************************************
        private void RetrieveDataAccount()
        {
            ddl_account_code.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_payrollaccounts_tbl_list");

            ddl_account_code.DataSource = dtEmpList;
            ddl_account_code.DataTextField = "account_title";
            ddl_account_code.DataValueField = "account_code";
            ddl_account_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_account_code.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list from Salary Grade Table
        //*************************************************************************
        private void RetrieveDataSubAccount()
        {
            ddl_sub_code.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_payroll_subaccount_tbl_list", "par_account_code", ddl_account_code.SelectedValue.ToString());
            ddl_sub_code.DataSource = dtEmpList;
            ddl_sub_code.DataTextField = "account_sub_title";
            ddl_sub_code.DataValueField = "account_sub_code";
            ddl_sub_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_sub_code.Items.Insert(0, li);
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

        private void RetrieveTemplateCode()
        {
            ddl_template_code.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list4", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
            ddl_template_code.DataSource = dt;
            ddl_template_code.DataValueField = "payrolltemplate_code";
            ddl_template_code.DataTextField = "payrolltemplate_descr";
            ddl_template_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_template_code.Items.Insert(0, li);
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

            txtb_template_code_hide.Visible = false;
            ddl_template_code.Visible = true;

            RetrieveTemplateCode();
            RetrieveDataAccount();
            RetrieveDataSubAccount();

            LabelAddEdit.Text = "Add New Record";
            ddl_template_code.Enabled = true;
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {

            txtb_descr_1.Text         = "";
            txtb_descr_2.Text         = "";
            txtb_descr_3.Text         = "";
            txtb_descr_4.Text         = "";
            txtb_descr_5.Text         = "";
            ddl_type_1.SelectedIndex  = 0;
            ddl_type_2.SelectedIndex  = 0;
            ddl_type_3.SelectedIndex  = 0;
            ddl_type_4.SelectedIndex  = 0;
            ddl_type_5.SelectedIndex  = 0;
            txtb_fix_1.Text           = "0.00";
            txtb_fix_2.Text           = "0.00";
            txtb_fix_3.Text           = "0.00";
            txtb_fix_4.Text           = "0.00";
            txtb_fix_5.Text           = "0.00";
            txtb_account_code.Text    = "";
            txtb_sub_account.Text     = "";
            ddl_taxable.SelectedIndex = 0;
            txtb_template_code.Text   = "";
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payrolltemplate_code"] = String.Empty;
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

            dtSource.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource.Columns.Add("fld1_descr", typeof(System.String));
            dtSource.Columns.Add("fld2_descr", typeof(System.String));
            dtSource.Columns.Add("fld3_descr", typeof(System.String));
            dtSource.Columns.Add("fld4_descr", typeof(System.String));
            dtSource.Columns.Add("fld5_descr", typeof(System.String));
            dtSource.Columns.Add("fld1_type", typeof(System.String));
            dtSource.Columns.Add("fld2_type", typeof(System.String));
            dtSource.Columns.Add("fld3_type", typeof(System.String));
            dtSource.Columns.Add("fld4_type", typeof(System.String));
            dtSource.Columns.Add("fld5_type", typeof(System.String));
            dtSource.Columns.Add("fld1_fixed_amt", typeof(System.String));
            dtSource.Columns.Add("fld2_fixed_amt", typeof(System.String));
            dtSource.Columns.Add("fld3_fixed_amt", typeof(System.String));
            dtSource.Columns.Add("fld4_fixed_amt", typeof(System.String));
            dtSource.Columns.Add("fld5_fixed_amt", typeof(System.String));
            dtSource.Columns.Add("account_code", typeof(System.String));
            dtSource.Columns.Add("account_sub_code", typeof(System.String));
            dtSource.Columns.Add("gn_taxable", typeof(System.String));

        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "othrpaysetup_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payrolltemplate_code" };
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

            string deleteExpression = "payrolltemplate_code = '" + code.Trim() + "'";

            MyCmn.DeleteBackEndData("othrpaysetup_tbl", "WHERE " + deleteExpression);

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
            dataListGrid = MyCmn.RetrieveData("sp_othrpaysetup_tbl_list2", "par_payrolltemplate_code", code);

            string editExpression = "payrolltemplate_code = '" + code + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);


            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();

            txtb_template_code_hide.Visible = true;
            ddl_template_code.Visible = false;
            
            RetrieveDataAccount();

            DataRow nrow = dtSource.NewRow();
            nrow["payrolltemplate_code"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);
            ddl_template_code.Enabled = false;
            txtb_template_code_hide.Text    = row2Edit[0]["payrolltemplate_descr"].ToString();
            txtb_template_code.Text         = row2Edit[0]["payrolltemplate_code"].ToString();
            txtb_descr_1.Text               = row2Edit[0]["fld1_descr"].ToString();
            txtb_descr_2.Text               = row2Edit[0]["fld2_descr"].ToString();
            txtb_descr_3.Text               = row2Edit[0]["fld3_descr"].ToString();
            txtb_descr_4.Text               = row2Edit[0]["fld4_descr"].ToString();
            txtb_descr_5.Text               = row2Edit[0]["fld5_descr"].ToString();
            ddl_type_1.SelectedValue        = row2Edit[0]["fld1_type"].ToString();
            ddl_type_2.SelectedValue        = row2Edit[0]["fld2_type"].ToString();
            ddl_type_3.SelectedValue        = row2Edit[0]["fld3_type"].ToString();
            ddl_type_4.SelectedValue        = row2Edit[0]["fld4_type"].ToString();
            ddl_type_5.SelectedValue        = row2Edit[0]["fld5_type"].ToString();
            txtb_fix_1.Text                 = row2Edit[0]["fld1_fixed_amt"].ToString();
            txtb_fix_2.Text                 = row2Edit[0]["fld2_fixed_amt"].ToString();
            txtb_fix_3.Text                 = row2Edit[0]["fld3_fixed_amt"].ToString();
            txtb_fix_4.Text                 = row2Edit[0]["fld4_fixed_amt"].ToString();
            txtb_fix_5.Text                 = row2Edit[0]["fld5_fixed_amt"].ToString();
            ddl_account_code.SelectedValue  = row2Edit[0]["account_code"].ToString();
            txtb_account_code.Text          = row2Edit[0]["account_code"].ToString();

            RetrieveDataSubAccount();

            txtb_sub_account.Text           = row2Edit[0]["account_sub_code"].ToString();
            ddl_sub_code.SelectedValue      = row2Edit[0]["account_sub_code"].ToString();
            ddl_taxable.SelectedValue       = row2Edit[0]["gn_taxable"].ToString();
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
                    dtSource.Rows[0]["payrolltemplate_code"] = ddl_template_code.SelectedValue.ToString();
                    dtSource.Rows[0]["fld1_descr"]           = txtb_descr_1.Text.ToString();
                    dtSource.Rows[0]["fld2_descr"]           = txtb_descr_2.Text.ToString();
                    dtSource.Rows[0]["fld3_descr"]           = txtb_descr_3.Text.ToString();
                    dtSource.Rows[0]["fld4_descr"]           = txtb_descr_4.Text.ToString();
                    dtSource.Rows[0]["fld5_descr"]           = txtb_descr_5.Text.ToString();
                    dtSource.Rows[0]["fld1_type"]            = ddl_type_1.SelectedValue.ToString();
                    dtSource.Rows[0]["fld2_type"]            = ddl_type_2.SelectedValue.ToString();
                    dtSource.Rows[0]["fld3_type"]            = ddl_type_3.SelectedValue.ToString();
                    dtSource.Rows[0]["fld4_type"]            = ddl_type_4.SelectedValue.ToString();
                    dtSource.Rows[0]["fld5_type"]            = ddl_type_5.SelectedValue.ToString();
                    dtSource.Rows[0]["fld1_fixed_amt"]       = txtb_fix_1.Text.ToString();
                    dtSource.Rows[0]["fld2_fixed_amt"]       = txtb_fix_2.Text.ToString();
                    dtSource.Rows[0]["fld3_fixed_amt"]       = txtb_fix_3.Text.ToString();
                    dtSource.Rows[0]["fld4_fixed_amt"]       = txtb_fix_4.Text.ToString();
                    dtSource.Rows[0]["fld5_fixed_amt"]       = txtb_fix_5.Text.ToString();
                    dtSource.Rows[0]["account_code"]         = txtb_account_code.Text.ToString();
                    dtSource.Rows[0]["account_sub_code"]     = txtb_sub_account.Text.ToString();
                    dtSource.Rows[0]["gn_taxable"]           = ddl_taxable.SelectedValue.ToString();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payrolltemplate_code"] = txtb_template_code.Text.ToString();
                    dtSource.Rows[0]["fld1_descr"]      = txtb_descr_1.Text.ToString();
                    dtSource.Rows[0]["fld2_descr"]      = txtb_descr_2.Text.ToString();
                    dtSource.Rows[0]["fld3_descr"]      = txtb_descr_3.Text.ToString();
                    dtSource.Rows[0]["fld4_descr"]      = txtb_descr_4.Text.ToString();
                    dtSource.Rows[0]["fld5_descr"]      = txtb_descr_5.Text.ToString();
                    dtSource.Rows[0]["fld1_type"]       = ddl_type_1.SelectedValue.ToString();
                    dtSource.Rows[0]["fld2_type"]       = ddl_type_2.SelectedValue.ToString();
                    dtSource.Rows[0]["fld3_type"]       = ddl_type_3.SelectedValue.ToString();
                    dtSource.Rows[0]["fld4_type"]       = ddl_type_4.SelectedValue.ToString();
                    dtSource.Rows[0]["fld5_type"]       = ddl_type_5.SelectedValue.ToString();
                    dtSource.Rows[0]["fld1_fixed_amt"]  = txtb_fix_1.Text.ToString();
                    dtSource.Rows[0]["fld2_fixed_amt"]  = txtb_fix_2.Text.ToString();
                    dtSource.Rows[0]["fld3_fixed_amt"]  = txtb_fix_3.Text.ToString();
                    dtSource.Rows[0]["fld4_fixed_amt"]  = txtb_fix_4.Text.ToString();
                    dtSource.Rows[0]["fld5_fixed_amt"]  = txtb_fix_5.Text.ToString();
                    dtSource.Rows[0]["account_code"]    = txtb_account_code.Text.ToString();
                    dtSource.Rows[0]["account_sub_code"]= txtb_sub_account.Text.ToString();
                    dtSource.Rows[0]["gn_taxable"]      = ddl_taxable.SelectedValue.ToString();

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

                        nrow["payrolltemplate_code"]  = ddl_template_code.SelectedValue.ToString();
                        nrow["payrolltemplate_descr"] = ddl_template_code.SelectedItem.Text.ToString();
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {

                        string editExpression = "payrolltemplate_code = '" + txtb_template_code.Text.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        row2Edit[0]["payrolltemplate_code"] = txtb_template_code.Text.ToString();
                        row2Edit[0]["payrolltemplate_descr"] = txtb_template_code_hide.Text.ToString();
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
           "payrolltemplate_code LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
           + "%' OR payrolltemplate_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
           + "%'";



            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_descr", typeof(System.String));

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

            if (ddl_template_code.SelectedIndex == 0 && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_template_code");
                ddl_template_code.Focus();
                validatedSaved = false;
            }

            if (txtb_descr_1.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "txtb_descr_1");
                txtb_descr_1.Focus();
                validatedSaved = false;
            }

            if (ddl_type_1.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_type_1");
                ddl_type_1.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_fix_1) == false)
            {
                if (txtb_fix_1.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_fix_1");
                    txtb_fix_1.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_fix_1");
                    txtb_fix_1.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_fix_2) == false)
            {
                if (txtb_fix_2.Text.ToString().Trim() != "")
                {
                    FieldValidationColorChanged(true, "invalid-txtb_fix_2");
                    txtb_fix_2.Focus();
                    validatedSaved = false;
                }
               
            }

            if (CommonCode.checkisdecimal(txtb_fix_3) == false)
            {
                if (txtb_fix_3.Text.ToString().Trim() != "")
                {
                    FieldValidationColorChanged(true, "invalid-txtb_fix_3");
                    txtb_fix_3.Focus();
                    validatedSaved = false;
                }
                
            }

            if (CommonCode.checkisdecimal(txtb_fix_4) == false)
            {
                if (txtb_fix_4.Text.ToString().Trim() != "")
                {
                    FieldValidationColorChanged(true, "invalid-txtb_fix_4");
                    txtb_fix_4.Focus();
                    validatedSaved = false;
                }
               
            }

            if (CommonCode.checkisdecimal(txtb_fix_5) == false)
            {
                if (txtb_fix_5.Text.ToString().Trim() != "")
                {
                    FieldValidationColorChanged(true, "invalid-txtb_fix_5");
                    txtb_fix_5.Focus();
                    validatedSaved = false;
                }
                
            }   

            if (ddl_account_code.SelectedValue.ToString().Trim() != "")
            {
                if (ddl_sub_code.Items.Count != 1 && ddl_sub_code.SelectedValue.ToString() == "")
                {
                    FieldValidationColorChanged(true, "ddl_sub_code");
                    ddl_sub_code.Focus();
                    validatedSaved = false;
                }
            }

            if (txtb_descr_2.Text.ToString().Trim() != "")
            {

                if (ddl_type_2.SelectedValue.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_fix_2");
                    ddl_type_2.Focus();
                    validatedSaved = false;
                }
                

            }

            if (txtb_descr_3.Text.ToString().Trim() != "")
            {

                if (ddl_type_3.SelectedValue.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_fix_3");
                    ddl_type_3.Focus();
                    validatedSaved = false;
                }

                if (txtb_descr_2.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_descr_2");
                    txtb_descr_2.Focus();
                    validatedSaved = false;
                }

            }

            if (txtb_descr_4.Text.ToString().Trim() != "")
            {

                if (ddl_type_4.SelectedValue.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_fix_4");
                    ddl_type_4.Focus();
                    validatedSaved = false;
                }

                if (txtb_descr_3.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_descr_3");
                    txtb_descr_3.Focus();
                    validatedSaved = false;
                }

            }

            if (txtb_descr_5.Text.ToString().Trim() != "")
            {

                if (ddl_type_5.SelectedValue.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_fix_5");
                    ddl_type_5.Focus();
                    validatedSaved = false;
                }

                if (txtb_descr_4.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_descr_4");
                    txtb_descr_4.Focus();
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
                    case "ddl_template_code":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_template_code.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_descr_1":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_descr_1.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_descr_2":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            txtb_descr_2.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_descr_3":
                        {
                            LblRequired8.Text = MyCmn.CONST_RQDFLD;
                            txtb_descr_3.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_descr_4":
                        {
                            LblRequired11.Text = MyCmn.CONST_RQDFLD;
                            txtb_descr_4.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_descr_5":
                        {
                            LblRequired14.Text = MyCmn.CONST_RQDFLD;
                            txtb_descr_5.BorderColor = Color.Red;
                            break;
                        }

                    case "ddl_type_1":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            ddl_type_1.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_fix_1":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_fix_1.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_fix_1":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_fix_1.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_fix_2":
                        {
                            LblRequired6.Text = MyCmn.CONST_RQDFLD;
                            ddl_type_2.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_fix_2":
                        {
                            LblRequired7.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_fix_2.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_fix_3":
                        {
                            LblRequired9.Text = MyCmn.CONST_RQDFLD;
                            ddl_type_3.BorderColor = Color.Red;
                            break;
                        }


                    case "invalid-txtb_fix_3":
                        {
                            LblRequired10.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_fix_3.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_fix_4":
                        {
                            LblRequired12.Text = MyCmn.CONST_RQDFLD;
                            ddl_type_4.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_fix_4":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_fix_4.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_fix_5":
                        {
                            LblRequired15.Text = MyCmn.CONST_RQDFLD;
                            ddl_type_5.BorderColor = Color.Red;
                            break;
                        }


                    case "invalid-txtb_fix_5":
                        {
                            LblRequired16.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_fix_5.BorderColor = Color.Red;
                            break;
                        }



                    case "ddl_sub_code":
                        {
                            LblRequired17.Text = MyCmn.CONST_RQDFLD;
                            ddl_sub_code.BorderColor = Color.Red;
                            break;
                        }
                        




                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired1.Text  = "";
                            LblRequired2.Text  = "";
                            LblRequired3.Text  = "";
                            LblRequired4.Text  = "";
                            LblRequired5.Text = "";
                            LblRequired7.Text  = "";
                            LblRequired6.Text  = "";
                            LblRequired8.Text = "";
                            LblRequired9.Text  = "";
                            LblRequired11.Text = "";
                            LblRequired12.Text = "";
                            LblRequired14.Text = "";
                            LblRequired15.Text = "";
                            LblRequired10.Text = "";
                            LblRequired13.Text = "";
                            LblRequired16.Text = "";
                            LblRequired17.Text = "";
                            txtb_descr_1.BorderColor= Color.LightGray;
                            txtb_descr_2.BorderColor = Color.LightGray;
                            txtb_descr_3.BorderColor = Color.LightGray;
                            txtb_descr_4.BorderColor = Color.LightGray;
                            txtb_descr_5.BorderColor = Color.LightGray;
                            ddl_type_1.BorderColor = Color.LightGray;
                            ddl_type_2.BorderColor = Color.LightGray;
                            ddl_type_3.BorderColor = Color.LightGray;
                            ddl_type_4.BorderColor = Color.LightGray;
                            ddl_type_5.BorderColor = Color.LightGray;
                            txtb_fix_1.BorderColor = Color.LightGray;
                            txtb_fix_2.BorderColor = Color.LightGray;
                            txtb_fix_3.BorderColor = Color.LightGray;
                            txtb_fix_4.BorderColor = Color.LightGray;
                            txtb_fix_5.BorderColor = Color.LightGray;
                            ddl_sub_code.BorderColor = Color.LightGray;
                            ddl_template_code.BorderColor = Color.LightGray;
                            
                            break;
                        }

                }
            }
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            btnAdd.Visible = true;
         }

        protected void ddl_account_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataSubAccount();
            txtb_account_code.Text = ddl_account_code.SelectedValue.ToString();
            txtb_sub_account.Text = "";
            ddl_sub_code.BorderColor = Color.LightGray;
            LblRequired17.Text = "";
        }

        protected void ddl_sub_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtb_sub_account.Text = ddl_sub_code.SelectedValue.ToString();
        }

        protected void ddl_template_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            txtb_template_code.Text = ddl_template_code.SelectedValue.ToString();
           
        }
    }



}