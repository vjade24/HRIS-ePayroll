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

namespace HRIS_ePayroll.View.cPayCertification
{
    public partial class cPayCertification : System.Web.UI.Page
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
            Session["cPayCertification"] = "cPayCertification";
            RetrieveEmploymentType();
            RetrieveDataListGrid();

        }

        
        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollcertification_tbl_list", "par_employment_type",ddl_empl_type.SelectedValue.ToString());
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
            
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
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list","par_employment_type",ddl_empl_type.SelectedValue.ToString().Trim());
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
            RetrieveTemplateCode();
            AddPrimaryKeys();
            AddNewRow();
            LabelAddEdit.Text = "Add New Record";
            ddl_template_code.Enabled = true;
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            
            txtb_sig1_cert.Text = "";
            txtb_sig2_cert.Text = "";
            txtb_sig3_cert.Text = "";
            txtb_sig4_cert.Text = "";
            txtb_sig1_name.Text = "";
            txtb_sig2_name.Text = "";
            txtb_sig3_name.Text = "";
            txtb_sig4_name.Text = "";
            txtb_pos1.Text = "";
            txtb_pos2.Text = "";
            txtb_pos3.Text = "";
            txtb_pos4.Text = "";
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
            dtSource.Columns.Add("sig1_certification", typeof(System.String));
            dtSource.Columns.Add("sig2_certification", typeof(System.String));
            dtSource.Columns.Add("sig3_certification", typeof(System.String));
            dtSource.Columns.Add("sig4_certification", typeof(System.String));
            dtSource.Columns.Add("sig1_name_override", typeof(System.String));
            dtSource.Columns.Add("sig2_name_override", typeof(System.String));
            dtSource.Columns.Add("sig3_name_override", typeof(System.String));
            dtSource.Columns.Add("sig4_name_override", typeof(System.String));
            dtSource.Columns.Add("sig1_position_override", typeof(System.String));
            dtSource.Columns.Add("sig2_position_override", typeof(System.String));
            dtSource.Columns.Add("sig3_position_override", typeof(System.String));
            dtSource.Columns.Add("sig4_position_override", typeof(System.String));

        }

        //*************************************************************************
        //  BEGIN - JRV- 03/22/2019  - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollcertification_tbl";
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

            MyCmn.DeleteBackEndData("payrollcertification_tbl", "WHERE " + deleteExpression);

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
           

            string editExpression = "payrolltemplate_code = '" + code + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            RetrieveTemplateCode();
            DataRow nrow = dtSource.NewRow();
            nrow["payrolltemplate_code"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);



             ddl_template_code.Enabled = false;
             ddl_template_code.SelectedValue = row2Edit[0]["payrolltemplate_code"].ToString();
             txtb_sig1_cert.Text = row2Edit[0]["sig1_certification"].ToString() ;
             txtb_sig2_cert.Text = row2Edit[0]["sig2_certification"].ToString() ;
             txtb_sig3_cert.Text =  row2Edit[0]["sig3_certification"].ToString();
             txtb_sig4_cert.Text =  row2Edit[0]["sig4_certification"].ToString();
             txtb_sig1_name.Text =  row2Edit[0]["sig1_name_override"].ToString();
             txtb_sig2_name.Text =  row2Edit[0]["sig2_name_override"].ToString();
             txtb_sig3_name.Text =  row2Edit[0]["sig3_name_override"].ToString();
             txtb_sig4_name.Text = row2Edit[0]["sig4_name_override"].ToString();
             txtb_pos1.Text      =  row2Edit[0]["sig1_position_override"].ToString();
             txtb_pos2.Text      =  row2Edit[0]["sig2_position_override"].ToString();
             txtb_pos3.Text      =  row2Edit[0]["sig3_position_override"].ToString();
             txtb_pos4.Text      = row2Edit[0]["sig4_position_override"].ToString();

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
                    dtSource.Rows[0]["sig1_certification"] = txtb_sig1_cert.Text.ToString();
                    dtSource.Rows[0]["sig2_certification"] = txtb_sig2_cert.Text.ToString();
                    dtSource.Rows[0]["sig3_certification"] = txtb_sig3_cert.Text.ToString();
                    dtSource.Rows[0]["sig4_certification"] = txtb_sig4_cert.Text.ToString();
                    dtSource.Rows[0]["sig1_name_override"] = txtb_sig1_name.Text.ToString();
                    dtSource.Rows[0]["sig2_name_override"] = txtb_sig2_name.Text.ToString();
                    dtSource.Rows[0]["sig3_name_override"] = txtb_sig3_name.Text.ToString();
                    dtSource.Rows[0]["sig4_name_override"] = txtb_sig4_name.Text.ToString();
                    dtSource.Rows[0]["sig1_position_override"] = txtb_pos1.Text.ToString();
                    dtSource.Rows[0]["sig2_position_override"] = txtb_pos2.Text.ToString();
                    dtSource.Rows[0]["sig3_position_override"] = txtb_pos3.Text.ToString();
                    dtSource.Rows[0]["sig4_position_override"] = txtb_pos4.Text.ToString();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payrolltemplate_code"] = ddl_template_code.SelectedValue.ToString();
                    dtSource.Rows[0]["sig1_certification"] = txtb_sig1_cert.Text.ToString();
                    dtSource.Rows[0]["sig2_certification"] = txtb_sig2_cert.Text.ToString();
                    dtSource.Rows[0]["sig3_certification"] = txtb_sig3_cert.Text.ToString();
                    dtSource.Rows[0]["sig4_certification"] = txtb_sig4_cert.Text.ToString();
                    dtSource.Rows[0]["sig1_name_override"] = txtb_sig1_name.Text.ToString();
                    dtSource.Rows[0]["sig2_name_override"] = txtb_sig2_name.Text.ToString();
                    dtSource.Rows[0]["sig3_name_override"] = txtb_sig3_name.Text.ToString();
                    dtSource.Rows[0]["sig4_name_override"] = txtb_sig4_name.Text.ToString();
                    dtSource.Rows[0]["sig1_position_override"] = txtb_pos1.Text.ToString();
                    dtSource.Rows[0]["sig2_position_override"] = txtb_pos2.Text.ToString();
                    dtSource.Rows[0]["sig3_position_override"] = txtb_pos3.Text.ToString();
                    dtSource.Rows[0]["sig4_position_override"] = txtb_pos4.Text.ToString();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    if (scriptInsertUpdate == string.Empty) return;
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") return;

                    if (msg.Substring(0, 1) == "X")
                    {
                        LblRequired1.Text = "Already Exists!";
                        ddl_template_code.BorderColor = Color.Red;
                        return;
                    }


                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();

                       nrow["payrolltemplate_code"] = ddl_template_code.SelectedValue.ToString();
                       nrow["sig1_certification"] = txtb_sig1_cert.Text.ToString();
                       nrow["sig2_certification"] = txtb_sig2_cert.Text.ToString();
                       nrow["sig3_certification"] = txtb_sig3_cert.Text.ToString();
                       nrow["sig4_certification"] = txtb_sig4_cert.Text.ToString();
                       nrow["sig1_name_override"] = txtb_sig1_name.Text.ToString();
                       nrow["sig2_name_override"] = txtb_sig2_name.Text.ToString();
                       nrow["sig3_name_override"] = txtb_sig3_name.Text.ToString();
                       nrow["sig4_name_override"] = txtb_sig4_name.Text.ToString();
                       nrow["sig1_position_override"] = txtb_pos1.Text.ToString();
                       nrow["sig2_position_override"] = txtb_pos2.Text.ToString();
                       nrow["sig3_position_override"] = txtb_pos3.Text.ToString();
                       nrow["sig4_position_override"] = txtb_pos4.Text.ToString();
                       dataListGrid.Rows.Add(nrow);
                       gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {

                        string editExpression = "payrolltemplate_code = '" + ddl_template_code.SelectedValue.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        
                        row2Edit[0]["payrolltemplate_code"]   = ddl_template_code.SelectedValue.ToString();
                        row2Edit[0]["sig1_certification"]     = txtb_sig1_cert.Text.ToString();
                        row2Edit[0]["sig2_certification"]     = txtb_sig2_cert.Text.ToString();
                        row2Edit[0]["sig3_certification"]     = txtb_sig3_cert.Text.ToString();
                        row2Edit[0]["sig4_certification"]     = txtb_sig4_cert.Text.ToString();
                        row2Edit[0]["sig1_name_override"]     = txtb_sig1_name.Text.ToString();
                        row2Edit[0]["sig2_name_override"]     = txtb_sig2_name.Text.ToString();
                        row2Edit[0]["sig3_name_override"]     = txtb_sig3_name.Text.ToString();
                        row2Edit[0]["sig4_name_override"]     = txtb_sig4_name.Text.ToString();
                        row2Edit[0]["sig1_position_override"] = txtb_pos1.Text.ToString();
                        row2Edit[0]["sig2_position_override"] = txtb_pos2.Text.ToString();
                        row2Edit[0]["sig3_position_override"] = txtb_pos3.Text.ToString();
                        row2Edit[0]["sig4_position_override"] = txtb_pos4.Text.ToString();
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
              + "%' OR payrolltemplate_short_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig1_certification LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig2_certification LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig3_certification LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig4_certification LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig1_name_override LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig2_name_override LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig3_name_override LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig4_name_override LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig1_position_override LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig2_position_override LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig3_position_override LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%' OR sig4_position_override LIKE '%" + txtb_search.Text.Trim().Replace("'", "''").Replace("%", "")
              + "%'";



            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("sig1_certification", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_short_descr", typeof(System.String));
            dtSource1.Columns.Add("sig2_certification", typeof(System.String));
            dtSource1.Columns.Add("sig3_certification", typeof(System.String));
            dtSource1.Columns.Add("sig4_certification", typeof(System.String));
            dtSource1.Columns.Add("sig1_name_override", typeof(System.String));
            dtSource1.Columns.Add("sig2_name_override", typeof(System.String));
            dtSource1.Columns.Add("sig3_name_override", typeof(System.String));
            dtSource1.Columns.Add("sig4_name_override", typeof(System.String));
            dtSource1.Columns.Add("sig1_position_override", typeof(System.String));
            dtSource1.Columns.Add("sig2_position_override", typeof(System.String));
            dtSource1.Columns.Add("sig3_position_override", typeof(System.String));
            dtSource1.Columns.Add("sig4_position_override", typeof(System.String));




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

            if (ddl_template_code.SelectedIndex == 0)
            {
                FieldValidationColorChanged(true, "ddl_template_code");
                ddl_template_code.Focus();
                validatedSaved = false;
            }

            if ((txtb_sig1_cert.Text.ToString().Trim() == ""
                && txtb_sig2_cert.Text.ToString().Trim() == ""
                && txtb_sig3_cert.Text.ToString().Trim() == ""
                && txtb_sig4_cert.Text.ToString().Trim() == ""
                && txtb_sig1_name.Text.ToString().Trim() == ""
                && txtb_pos1.Text.ToString().Trim() == ""
                && txtb_sig2_name.Text.ToString().Trim() == ""
                && txtb_pos2.Text.ToString().Trim() == ""
                && txtb_sig3_name.Text.ToString().Trim() == ""
                && txtb_pos3.Text.ToString().Trim() == ""
                && txtb_sig4_name.Text.ToString().Trim() == ""
                && txtb_pos4.Text.ToString().Trim() == ""
                ))

            {
                FieldValidationColorChanged(true, "txtb_sig1_cert");
                txtb_sig1_cert.Focus();
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
                    case "ddl_template_code":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_template_code.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_sig1_cert":
                        {
                            LblRequired2.Text  = "At least one assignatory required!";
                            LblRequired3.Text  = "At least one assignatory required!";
                            LblRequired4.Text  = "At least one assignatory required!";
                            LblRequired5.Text  = "At least one assignatory required!";
                            LblRequired6.Text  = "At least one assignatory required!";
                            LblRequired7.Text  = "At least one assignatory required!";
                            LblRequired8.Text  = "At least one assignatory required!";
                            LblRequired9.Text  = "At least one assignatory required!";
                            LblRequired10.Text = "At least one assignatory required!";
                            LblRequired11.Text = "At least one assignatory required!";
                            LblRequired12.Text = "At least one assignatory required!";
                            LblRequired13.Text = "At least one assignatory required!";

                            txtb_sig1_name.BorderColor = Color.Red;
                            txtb_pos1.BorderColor = Color.Red;
                            txtb_sig2_name.BorderColor = Color.Red;
                            txtb_pos2.BorderColor = Color.Red;
                            txtb_sig3_name.BorderColor = Color.Red;
                            txtb_pos3.BorderColor = Color.Red;
                            txtb_sig4_name.BorderColor = Color.Red;
                            txtb_pos4.BorderColor = Color.Red;
                            txtb_sig1_cert.BorderColor = Color.Red;
                            txtb_sig2_cert.BorderColor = Color.Red;
                            txtb_sig3_cert.BorderColor = Color.Red;
                            txtb_sig4_cert.BorderColor = Color.Red;
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
                            LblRequired5.Text  = "";
                            LblRequired6.Text  = "";
                            LblRequired7.Text  = "";
                            LblRequired8.Text  = "";
                            LblRequired9.Text  = "";
                            LblRequired10.Text = "";
                            LblRequired11.Text = ""; 
                            LblRequired12.Text = "";
                            LblRequired13.Text = "";
                            ddl_template_code.BorderColor = Color.LightGray;
                            txtb_sig1_name.BorderColor = Color.LightGray;
                            txtb_pos1.BorderColor      = Color.LightGray;
                            txtb_sig2_name.BorderColor = Color.LightGray;
                            txtb_pos2.BorderColor      = Color.LightGray;
                            txtb_sig3_name.BorderColor = Color.LightGray;
                            txtb_pos3.BorderColor      = Color.LightGray;
                            txtb_sig4_name.BorderColor = Color.LightGray;
                            txtb_pos4.BorderColor      = Color.LightGray;
                            txtb_sig1_cert.BorderColor = Color.LightGray;
                            txtb_sig2_cert.BorderColor = Color.LightGray;
                            txtb_sig3_cert.BorderColor = Color.LightGray;
                            txtb_sig4_cert.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue.ToString().Trim() != "")
            {
                RetrieveDataListGrid();
                btnAdd.Visible = true;

            }
        }

    }


   
}