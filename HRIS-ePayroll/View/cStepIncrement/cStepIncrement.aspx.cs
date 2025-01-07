using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRIS_Common;

namespace HRIS_ePayroll.View
{
    public partial class cStepIncrement : System.Web.UI.Page
    {
        //********************************************************************
        // BEGIN Jade- 10/11/18 - Data Place holder creation 
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
        DataTable budgetcodelist
        {
            get
            {
                if ((DataTable)ViewState["budgetcodelist"] == null) return null;
                return (DataTable)ViewState["budgetcodelist"];
            }
            set
            {
                ViewState["budgetcodelist"] = value;
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
        DataTable dtEmpList
        {
            get
            {
                if ((DataTable)ViewState["dtEmpList"] == null) return null;
                return (DataTable)ViewState["dtEmpList"];
            }
            set
            {
                ViewState["dtEmpList"] = value;
            }
        }
        CommonDB MyCmn = new CommonDB();
        //********************************************************************
        //  BEGIN Jade- 10/11/18 - Public Constant Variable used in Sorting
        //********************************************************************

        const string CONST_TRANS_CODE = "103";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            if (!IsPostBack)
            {
                    Session["SortField"] = "employee_name";
                if (Session["SortOrder"] == null)
                    Session["SortOrder"] = "ASC";
                InitializePage();

            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["page_allow_view"] = Master.allow_view;
                if (Master.allow_view == "1")
                {
                    ViewState["page_allow_add"]             = 0;
                    ViewState["page_allow_delete"]          = 0;
                    ViewState["page_allow_edit"]            = 0;
                    ViewState["page_allow_edit_history"]    = 0;
                    ViewState["page_allow_print"]           = 0;
                }
                else
                {
                    ViewState["page_allow_add"]             = Master.allow_add;
                    ViewState["page_allow_delete"]          = Master.allow_delete;
                    ViewState["page_allow_edit"]            = Master.allow_edit;
                    ViewState["page_allow_edit_history"]    = Master.allow_edit_history;
                    ViewState["page_allow_print"]           = Master.allow_print;
                }
                if (Session["PreviousValuesonPage_NOSI"] == null)
                    Session["PreviousValuesonPage_NOSI"] = "";
                else if (Session["PreviousValuesonPage_NOSI"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_NOSI"].ToString().Split(new char[] { ',' });
                    ddl_budget_code.SelectedValue = prevValues[0].ToString();
                    if (ddl_budget_code.SelectedValue.ToString().Trim() == "")
                    {
                        btn_add.Visible = false;
                        btn_extract.Visible = false;
                    }
                    else
                    {
                        RetrieveBindingEmplNameList();
                        btn_add.Visible = true;
                        btn_extract.Visible = true;
                    }
                    RetrieveDataListGrid();
                }
            }
        }
        //********************************************************************
        //  BEGIN Jade- 10/11/18 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cStepIncrement"] = "cStepIncrement";


            
            RetrieveBindingBudgetYear();
            if (Session["ep_budget_code"].ToString() != string.Empty)
            {
                ddl_budget_code.SelectedValue = Session["ep_budget_code"].ToString();
                
            }
            RetrieveBindingEmplNameList();
            RetrieveDataListGrid();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";

            btn_add.Visible = false;
            btn_extract.Visible = false;
        }

        //*************************************************************************
        // BEGIN Jade- 10/11/18 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_newstepincrement_tbl_list", "par_budget_code", ddl_budget_code.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //*************************************************************************
        //  BEGIN - Jade- 10/11/18 - Populate Combo List from Employees with TA
        //*************************************************************************
        private void RetrieveBindingEmplNameList()
        {
            ddl_empl_name.Items.Clear();
            dtEmpList                       = MyCmn.RetrieveData("sp_employee_for_nosi_list", "p_budget_code", ddl_budget_code.SelectedValue.ToString().Trim());

            ddl_empl_name.DataSource        = dtEmpList;
            ddl_empl_name.DataTextField     = "employee_name";
            ddl_empl_name.DataValueField    = "empl_id";
            ddl_empl_name.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_name.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN Jade- 10/11/18 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_date_generated.Text        = DateTime.Now.ToString("yyyy-MM-dd");
            ddl_empl_name.SelectedIndex     = 0;
            txtb_date_of_effectivity.Text   = "";
            txtb_position_title.Text        = "";
            txtb_salary_grade.Text          = "";
            txtb_item_no.Text               = "";
            txtb_new_step.Text              = "";
            txtb_new_salary.Text            = "";
            ddl_new_step.SelectedValue      = "";
            ddl_status.SelectedValue        = "";
            ddl_step_type.SelectedValue     = "";
        }

        //*************************************************************************
        //  BEGIN Jade- 10/11/18 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("date_generated", typeof(System.String));
            dtSource.Columns.Add("date_of_effectivity", typeof(System.String));
            dtSource.Columns.Add("item_no", typeof(System.String));
            dtSource.Columns.Add("step_increment_new", typeof(System.String));
            dtSource.Columns.Add("approval_status", typeof(System.String));
            dtSource.Columns.Add("posting_status", typeof(System.String));
            dtSource.Columns.Add("approval_id", typeof(System.String));
            dtSource.Columns.Add("budget_code", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));
            dtSource.Columns.Add("step_type", typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN Jade- 10/11/18 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "newstepincrement_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "date_generated" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //*************************************************************************
        // BEGIN Jade- 10/11/18 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow                = dtSource.NewRow();
            nrow["empl_id"]             = string.Empty;
            nrow["date_generated"]      = string.Empty;
            nrow["date_of_effectivity"] = string.Empty;
            nrow["item_no"]             = string.Empty;
            nrow["step_increment_new"]  = string.Empty;
            nrow["approval_status"]     = string.Empty;
            nrow["posting_status"]      = string.Empty;
            nrow["approval_id"]         = string.Empty;
            nrow["budget_code"]         = string.Empty;
            nrow["created_by_user"]     = string.Empty;
            nrow["updated_by_user"]     = string.Empty;
            nrow["created_dttm"]        = string.Empty;
            nrow["updated_dttm"]        = string.Empty;
            nrow["step_type"]           = string.Empty;
            nrow["action"]              = 1;
            nrow["retrieve"]            = false;
            dtSource.Rows.Add(nrow);
        }

        //*************************************************************************
        // BEGIN Ariel - 10/07/18 - Initialize datatable object for Row to Edit
        //*************************************************************************
        private void InitRow2Edit()
        {
            DataRow nrow                = dtSource.NewRow();
            nrow["empl_id"]             = string.Empty;
            nrow["date_generated"]      = string.Empty;
            nrow["date_of_effectivity"] = string.Empty;
            nrow["item_no"]             = string.Empty;
            nrow["step_increment_new"]  = string.Empty;
            nrow["approval_status"]     = string.Empty;
            nrow["posting_status"]      = string.Empty;
            nrow["approval_id"]         = string.Empty;
            nrow["budget_code"]         = string.Empty;
            nrow["created_by_user"]     = string.Empty;
            nrow["updated_by_user"]     = string.Empty;
            nrow["created_dttm"]        = string.Empty;
            nrow["updated_dttm"]        = string.Empty;
            nrow["step_type"]           = string.Empty;
            nrow["action"]              = 2;
            nrow["retrieve"]            = true;
            dtSource.Rows.Add(nrow);
        }

        //*************************************************************************
        //  BEGIN Jade- 10/11/18 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btn_Add_Click(object sender, EventArgs e)
        {

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            btn_submit.Visible      = true;
            btn_cancel.Visible      = true;
            //btn_save.Visible = true;

            ddl_empl_name.Visible   = true;
            txtb_empl_name.Visible  = false;


            txtb_date_of_effectivity_disable.Visible    = false;
            txtb_date_of_effectivity.Visible            = true;
            ddl_new_step.Enabled = true;

            LabelAddEdit.Text           = "Add New Record";
            ViewState["AddEdit_Mode"]   = MyCmn.CONST_ADD;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopAdd", "openModal();", true);
            UpdatePanel7.Update();
        }

        //**************************************************************************
        //  BEGIN Jade- 10/11/18 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {

            string[] commandArgs    = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id0         = commandArgs[0].ToString();
            string dt_filed         = commandArgs[1].ToString();
            string approval_status  = commandArgs[2].ToString();

            string editExpression   = "empl_id = '" + empl_id0 + "' AND date_generated = '" + dt_filed.ToString().Trim() + "'";

            DataRow[] row2Edit      = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            InitRow2Edit();
            txtb_empl_name.Text                     = row2Edit[0]["employee_name"].ToString();
            
            txtb_date_generated.Text                = row2Edit[0]["date_generated"].ToString();
            txtb_date_of_effectivity.Text           = row2Edit[0]["date_of_effectivity"].ToString();
            txtb_date_of_effectivity_disable.Text   = row2Edit[0]["date_of_effectivity"].ToString();
            
            txtb_item_no.Text                       = row2Edit[0]["item_no"].ToString();
            txtb_new_salary.Text                    = row2Edit[0]["new_salary"].ToString();
            txtb_salary_grade.Text                  = row2Edit[0]["salary_grade"].ToString();
            txtb_position_title.Text                = row2Edit[0]["position_long_title"].ToString();
            txtb_new_step.Text                      = row2Edit[0]["step_increment_new"].ToString();
            ddl_new_step.SelectedValue              = row2Edit[0]["step_increment_new"].ToString();
            ddl_step_type.SelectedValue             = row2Edit[0]["step_type"].ToString();
            ddl_status.SelectedValue                = row2Edit[0]["approval_status"].ToString();
            lbl_hidden_approval_id.Text             = row2Edit[0]["approval_id"].ToString();
            approval_status                         = row2Edit[0]["approval_status"].ToString();
            lbl_hidden_appr_status.Text             = approval_status;

            dtSource.Rows[0]["empl_id"]             = row2Edit[0]["empl_id"];
            dtSource.Rows[0]["created_by_user"]     = row2Edit[0]["created_by_user"];
            dtSource.Rows[0]["updated_by_user"]     = row2Edit[0]["updated_by_user"];
            dtSource.Rows[0]["created_dttm"]        = row2Edit[0]["created_dttm"];
            dtSource.Rows[0]["updated_dttm"]        = row2Edit[0]["updated_dttm"];
            if ((approval_status.Trim() == CommonDB.CONST_APPR_STAT_NEW) || (approval_status.Trim() == CommonDB.CONST_APPR_STAT_PENDING))
            {
                txtb_empl_name.Visible  = true;
                ddl_empl_name.Visible   = false;

                btn_submit.Visible      = true;
                btn_cancel.Visible      = true;
                ddl_new_step.Enabled    = true;
                //btn_save.Visible = true;


                txtb_date_of_effectivity_disable.Visible    = false;
                txtb_date_of_effectivity.Visible            = true;

                LabelAddEdit.Text = "Edit Record: " + txtb_empl_name.Text.ToString();

            }
            else
            {
                txtb_empl_name.Visible  = true;
                ddl_empl_name.Visible   = false;
                ddl_new_step.Enabled    = false;
                btn_submit.Visible      = true;
                btn_cancel.Visible      = true;
                //btn_save.Visible = false;

                txtb_date_of_effectivity_disable.Visible    = true;
                txtb_date_of_effectivity.Visible            = false;

                LabelAddEdit.Text = "Edit Record: " + txtb_empl_name.Text.ToString();

            }

            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;
            UpdatePanel7.Update();

            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopEdit", "openModal();", true);

        }

        //***************************************************************************
        // BEGIN Jade- 10/11/18 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs    = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id0         = commandArgs[0].ToString();
            string dt_filed         = commandArgs[1].ToString();
            string approval_status  = commandArgs[2].ToString();
            string approval_id      = commandArgs[3].ToString();
            if ((approval_status.Trim() == CommonDB.CONST_APPR_STAT_DISAPPROVED) || (approval_status.Trim() == CommonDB.CONST_APPR_STAT_APPROVED))
            {
                notify_header.Text          = "Unable to Delete";
                lbl_editdeletenotify.Text   = " - This Record is Approved/Disapprove, it can't be Deleted";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopNotif", "openModalNotify();", true);
            }
            else
            {
                ddl_empl_name.SelectedValue = empl_id0.Trim();
                deleteRec1.Text             = "Are you sure you want to delete this record = (" + empl_id0.Trim() + ") - " + ddl_empl_name.SelectedItem.Text.ToString().Trim() + " ?";
                lnkBtnYes.CommandArgument   = empl_id0 + ", " + dt_filed + ", " + approval_status.Trim() + ", " + approval_id.Trim();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopD", "openModalDelete();", true);
            }
        }


        //*************************************************************************
        //  BEGIN Jade- 10/11/18 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs    = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id1         = commandArgs[0].ToString();
            string dt_filed1        = commandArgs[1].ToString();
            string appr_status1     = commandArgs[2].ToString();
            string approval_id1     = commandArgs[3].ToString();

            string deleteExpression     = "empl_id = '" + empl_id1 + "' AND CONVERT(date,date_generated) = CONVERT(date,'" + dt_filed1.ToString().Trim() + "')";
            string deleteExpression1    = "empl_id = '" + empl_id1 + "' AND date_generated = '" + dt_filed1.ToString().Trim() + "'";

            MyCmn.DeleteBackEndData("newstepincrement_tbl", "WHERE " + deleteExpression);
            //MyCmn.DeleteBackEndData("approvalworkflow_tbl", "WHERE approval_id = '" + approval_id1.Trim() + "'");
            //if (appr_status1.Trim() != CommonDB.CONST_APPR_STAT_NEW)
            //{
            //    MyCmn.SaveUpdateApprovalWorkFlow(approval_id1.Trim(), Session["ep_user_id"].ToString().Trim(), "L", "Cancelled Application");
            //}

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression1);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            //CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopDx", "closeModalDelete();", true);
            resignation_panel.Update();
        }

        //***************************************************************************
        // BEGIN Jade- 10/11/18 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void printRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs    = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id0         = commandArgs[0].ToString();
            string dt_generated     = commandArgs[1].ToString();
            string approval_status  = commandArgs[2].ToString();
            string printreport;
            string procedure;
            string url;

            Session["PreviousValuesonPage_NOSI"] = ddl_budget_code.SelectedValue.ToString();
            Session["history_page"] = Request.Url.AbsolutePath;
            
            printreport     = "cryNosi/cryNosi.rpt";
                procedure   = "sp_nosi_report";
                url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",par_empl_id," + empl_id0.Trim() + ",par_date_generated," + dt_generated.Trim();
                Response.Redirect(url);
        }


        //**************************************************************************
        //  BEGIN Jade- 10/11/18 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN Jade- 10/11/18 - Get Grid current sort order 
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
        //  BEGIN Jade- 10/11/18 - Change Field Sort mode  
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
        //**********************************************************************************
        //  BEGIN Jade- 10/11/18 - Change on Page Size (no. of row per page) on Gridview  
        //**********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            if (DropDownListID.SelectedItem.Text == "ALL")
            {
                gv_dataListGrid.PageSize = gv_dataListGrid.PageCount;
            }
            else
            {
                gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.SelectedItem.Value);
            }
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN Jade- 10/11/18 - Define Property for Sort Direction  
        //**************************************************************************
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

        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txt_search.Text.Trim() + "%' OR employee_name LIKE '%" + txt_search.Text.Trim() + "%' OR approval_status_descr LIKE '%" + txt_search.Text.Trim() + "%'";

            DataTable dtSource = new DataTable();
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("date_generated", typeof(System.String));
            dtSource.Columns.Add("date_of_effectivity", typeof(System.String));
            dtSource.Columns.Add("item_no", typeof(System.String));
            dtSource.Columns.Add("step_increment_new", typeof(System.String));
            dtSource.Columns.Add("approval_status", typeof(System.String));
            dtSource.Columns.Add("posting_status", typeof(System.String));
            dtSource.Columns.Add("approval_id", typeof(System.String));
            dtSource.Columns.Add("employee_name", typeof(System.String));
            dtSource.Columns.Add("approval_status_descr", typeof(System.String));
            dtSource.Columns.Add("budget_code", typeof(System.String));
            dtSource.Columns.Add("step_type_desc", typeof(System.String));
            dtSource.Columns.Add("step_type", typeof(System.String));

            DataRow[] rows = dataListGrid.Select(searchExpression);
            dtSource.Clear();
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    dtSource.ImportRow(row);
                }
            }

            gv_dataListGrid.DataSource = dtSource;
            gv_dataListGrid.DataBind();
            resignation_panel.Update();
            txt_search.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            txt_search.Focus();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        

        //**************************************************************************
        //  BEGIN Jade- 10/11/18 - Populate Combo list from Departments table
        //**************************************************************************
        private void RetrieveBindingBudgetYear()
        {

            budgetcodelist                  = MyCmn.RetrieveData("sp_plantillapayroll_tbl_list2");
            ddl_budget_code.DataSource      = budgetcodelist;
            ddl_budget_code.DataValueField  = "budget_code";
            ddl_budget_code.DataTextField   = "payroll_year";
            ddl_budget_code.DataBind();

            ListItem li = new ListItem("-- Select Here --", "");
            ddl_budget_code.Items.Insert(0, li);
        }

        //**********************************************************************************
        //  BEGIN - Jade- 10/11/18 - Validate Dropdownlist for Departments 
        //*********************************************************************************
        protected void ddl_budget_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_budget_code.SelectedValue.ToString() != "")
            {
                btn_add.Visible = true;
                btn_extract.Visible = true;
            }
            else
            {
                btn_add.Visible = false;
                btn_extract.Visible = false;

            }
            RetrieveBindingEmplNameList();
            RetrieveDataListGrid();
        }

        //**********************************************************************************
        //  BEGIN - Jade- 10/11/18 - Validate Dropdownlist for Reasons 
        //*********************************************************************************
        protected void ddl_empl_name_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_empl_name.SelectedValue.ToString() == "")
            {
                FieldValidationColorChanged(true, "ddl_empl_name");
                txtb_item_no.Text = "";
            }
            else
            {
                FieldValidationColorChanged(false, "ddl_empl_name");
                PopulateAddEditFields();
            }
            ddl_empl_name.Focus();
        }
        //**********************************************************************************
        //  BEGIN - Jade- 10/11/18 - Populate Data from Textbox 
        //*********************************************************************************
        private void PopulateAddEditFields()
        {
            string editExpression = "empl_id = '" + ddl_empl_name.SelectedValue.ToString() + "'";

            DataRow[] row2Sel = dtEmpList.Select(editExpression);

            txtb_item_no.Text        = row2Sel[0]["item_no"].ToString();
            txtb_new_step.Text       = row2Sel[0]["step_increment_new"].ToString();
            txtb_salary_grade.Text   = row2Sel[0]["salary_grade"].ToString();
            txtb_new_salary.Text     = row2Sel[0]["new_salary"].ToString();
            txtb_position_title.Text = row2Sel[0]["position_long_title"].ToString();
        }

        //*************************************************************************
        //  BEGIN - Jade- 09/25/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;
            FieldValidationColorChanged(false, "ALL");
            //try
            //{
            //    DateTime.Parse(txtb_date_of_effectivity.Text.Trim());
            //    FieldValidationColorChanged(false, "txtb_date_of_effectivity");
            //    UpdatePanel7.Update();
                
                if (ddl_empl_name.SelectedValue.Trim() == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
                {
                    FieldValidationColorChanged(true, "ddl_empl_name");
                    ddl_empl_name.Focus();
                    validatedSaved = false;
                }
                if (txtb_date_of_effectivity.Text == "")
                {

                    FieldValidationColorChanged(true, "txtb_date_of_effectivity");
                    txtb_date_of_effectivity.Focus();
                    UpdatePanel7.Update();
                    validatedSaved = false;
                }
                else if (CommonCode.checkisdatetime(txtb_date_of_effectivity) == false)
                {
                    FieldValidationColorChanged(true, "invalid-date");
                    validatedSaved = false;
                }

                if (ddl_new_step.SelectedValue.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "ddl_new_step");
                    validatedSaved = false;
                }

                if (ddl_status.SelectedValue.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "ddl_status");
                    validatedSaved = false;
                }
            if (ddl_step_type.SelectedValue.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "ddl_step_type");
                validatedSaved = false;
            }

            //    else
            //    {

            //    }
            //}
            //catch (Exception b)
            //{
            //    if (txtb_date_of_effectivity.Text == "")
            //    {
            //        FieldValidationColorChanged(true, "txtb_date_of_effectivity");
            //    }
            //    else
            //    {
            //        FieldValidationColorChanged(true, "invalid-date");
            //    }
            //    UpdatePanel7.Focus();
            //    return false;
            //}

            return validatedSaved;
        }

        //*************************************************************************
        //  BEGIN - Jade - 10/11/18 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
            {
                switch (pObjectName)
                {
                    case "ddl_empl_name":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_name.CssClass = "form-control form-control-sm required";
                            break;
                        }
                    case "txtb_date_of_effectivity":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_date_of_effectivity.CssClass = "form-control form-control-sm required my-date";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                            UpdatePanel7.Update();
                            break;
                        }
                    case "invalid-date":
                        {
                            LblRequired2.Text = "Invalid Date Value";
                            txtb_date_of_effectivity.CssClass = "form-control form-control-sm required my-date";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "show_date();", true);
                            UpdatePanel7.Update();
                            break;
                        }
                    case "ddl_new_step":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            ddl_new_step.CssClass = "form-control form-control-sm required";
                            break;
                        }
                    case "ddl_step_type":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            ddl_step_type.CssClass = "form-control form-control-sm required";
                            break;
                        }
                    case "ddl_status":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            ddl_status.CssClass = "form-control form-control-sm required";
                            break;
                        }
                }
            }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ddl_empl_name":
                        {
                            LblRequired1.Text = "";
                            ddl_empl_name.CssClass = "form-control form-control-sm";
                            break;
                        }
                    case "txtb_date_of_effectivity":
                        {
                            LblRequired2.Text = "";
                            txtb_date_of_effectivity.CssClass = "form-control form-control-sm my-date";
                            UpdatePanel7.Update();
                            break;
                        }
                    case "ALL":
                        {
                            LblRequired1.Text = "";
                            LblRequired2.Text = "";
                            LblRequired3.Text = "";
                            LblRequired4.Text = "";
                            LblRequired5.Text = "";
                            ddl_empl_name.CssClass  = "form-control form-control-sm";
                            ddl_new_step.CssClass   = "form-control form-control-sm";
                            ddl_status.CssClass     = "form-control form-control-sm";
                            ddl_step_type.CssClass     = "form-control form-control-sm";
                            txtb_date_of_effectivity.CssClass = "form-control form-control-sm my-date";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popx", "show_date();", true);
                            UpdatePanel7.Update();
                            break;
                        }

                }
            }
        }

        //**************************************************************************
        //  BEGIN - Jade- 10/11/18 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            string l_AddEdit_Mode = ViewState["AddEdit_Mode"].ToString();
            if (IsDataValidated())
            {
                if (l_AddEdit_Mode.ToString() == MyCmn.CONST_ADD)
                {
                    Save_AddEditSubmit(CommonDB.CONST_APPR_STAT_NEW,"");
                }
                else if (l_AddEdit_Mode.ToString() == MyCmn.CONST_EDIT)
                    Save_AddEditSubmit(lbl_hidden_appr_status.Text, lbl_hidden_approval_id.Text);
            }
        }

        //*****************************************************************************************
        //  BEGIN - Jade- 10/11/18 - Save New Record/Edited/Submitted Record to back end DB
        //*****************************************************************************************
        private void Save_AddEditSubmit(string par_Approval_Status, string par_Approval_Id)
        {
            string saveRecord           = ViewState["AddEdit_Mode"].ToString();
            string approval_id          = par_Approval_Id;
            string scriptInsertUpdate   = string.Empty;

            if (saveRecord == MyCmn.CONST_ADD)
            {
                dtSource.Rows[0]["empl_id"]             = ddl_empl_name.SelectedValue.ToString();
                dtSource.Rows[0]["date_generated"]      = txtb_date_generated.Text.ToString();
                dtSource.Rows[0]["date_of_effectivity"] = txtb_date_of_effectivity.Text.ToString();
                dtSource.Rows[0]["approval_status"]     = ddl_status.SelectedValue.ToString();
                dtSource.Rows[0]["posting_status"]      = "0";
                dtSource.Rows[0]["approval_id"]         = approval_id;
                dtSource.Rows[0]["budget_code"]         = ddl_budget_code.SelectedValue.ToString();
                dtSource.Rows[0]["item_no"]             = txtb_item_no.Text.ToString();
                dtSource.Rows[0]["step_increment_new"]  = ddl_new_step.SelectedValue.ToString();
                dtSource.Rows[0]["created_by_user"]     = Session["ep_user_id"].ToString();
                dtSource.Rows[0]["updated_by_user"]     = "";
                dtSource.Rows[0]["created_dttm"]        = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dtSource.Rows[0]["updated_dttm"]        = "";
                dtSource.Rows[0]["step_type"]           = ddl_step_type.SelectedValue.ToString();
                scriptInsertUpdate                      = MyCmn.get_insertscript(dtSource);
            }
            else if (saveRecord == MyCmn.CONST_EDIT)
            {
                //dtSource.Rows[0]["empl_id"]             = ddl_empl_name.SelectedValue.ToString();
                dtSource.Rows[0]["date_generated"]      = txtb_date_generated.Text.ToString();
                dtSource.Rows[0]["date_of_effectivity"] = txtb_date_of_effectivity.Text.ToString();
                dtSource.Rows[0]["approval_status"]     = ddl_status.SelectedValue.ToString();
                dtSource.Rows[0]["posting_status"]      = "0";
                dtSource.Rows[0]["approval_id"]         = approval_id;
                dtSource.Rows[0]["budget_code"]         = ddl_budget_code.SelectedValue.ToString();
                dtSource.Rows[0]["item_no"]             = txtb_item_no.Text.ToString();
                dtSource.Rows[0]["step_increment_new"]  = ddl_new_step.SelectedValue.ToString();
               // dtSource.Rows[0]["created_by_user"] = row2Edit[0]["created_by_user"];
                dtSource.Rows[0]["updated_by_user"]     = Session["ep_user_id"];
                //dtSource.Rows[0]["created_dttm"] = row2Edit[0]["created_dttm"];
                dtSource.Rows[0]["updated_dttm"]        = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dtSource.Rows[0]["step_type"]           = ddl_step_type.SelectedValue.ToString();
                scriptInsertUpdate                      = MyCmn.updatescript(dtSource);

            }

            if (saveRecord == MyCmn.CONST_ADD|| saveRecord == MyCmn.CONST_EDIT)
            {
                if (scriptInsertUpdate == string.Empty) return;
                string msg = MyCmn.insertdata(scriptInsertUpdate);
                if (msg == "") return;
                if (msg.Substring(0, 1) == "X") return;

                if (saveRecord == MyCmn.CONST_ADD)
                {
                    DataRow nrow = dataListGrid.NewRow();
                    nrow["empl_id"]             = ddl_empl_name.SelectedValue.ToString();
                    nrow["date_generated"]      = txtb_date_generated.Text.ToString();
                    nrow["date_of_effectivity"] = txtb_date_of_effectivity.Text.ToString();
                    nrow["approval_status"]     = ddl_status.SelectedValue.ToString();
                    nrow["posting_status"]      = 0;
                    nrow["approval_id"]         = approval_id;
                    nrow["employee_name"]       = ddl_empl_name.SelectedItem.Text.ToString();
                    nrow["budget_code"]         = ddl_budget_code.SelectedItem.Text.ToString();
                    nrow["item_no"]             = txtb_item_no.Text.ToString();
                    nrow["step_increment_new"]  = ddl_new_step.SelectedValue.ToString();

                    nrow["new_salary"]          = txtb_new_salary.Text.ToString();
                    nrow["salary_grade"]        = txtb_salary_grade.Text.ToString();
                    nrow["position_long_title"] = txtb_position_title.Text.ToString();
                    nrow["step_type"]           = ddl_step_type.SelectedValue.ToString();
                    nrow["step_type_desc"]      = ddl_step_type.SelectedItem.Text.ToString();

                    switch (ddl_status.SelectedValue.ToString())
                    {
                        case CommonDB.CONST_APPR_STAT_NEW:
                            nrow["approval_status_descr"] = MyCmn.CONST_APPR_STAT_NEW_DESCR;
                            break;
                        case CommonDB.CONST_APPR_STAT_SUBMITTED:
                            nrow["approval_status_descr"] = MyCmn.CONST_APPR_STAT_SUBMITTED_DESCR;
                            break;
                        case CommonDB.CONST_APPR_STAT_APPROVED:
                            nrow["approval_status_descr"] = "Final Approved";
                            break;
                        default:
                            break;
                    }

                    dataListGrid.Rows.Add(nrow);
                    gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                    //gv_dataListGrid.SelectRow(gv_dataListGrid.Rows.Count - 1);
                    SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                }

                if (saveRecord == MyCmn.CONST_EDIT)
                {
                    string editExpression = "empl_id = '" + dtSource.Rows[0]["empl_id"].ToString().Trim() + "' AND date_generated = '" + txtb_date_generated.Text.Trim() + "'";
                    DataRow[] row2Edit = dataListGrid.Select(editExpression);
                    row2Edit[0]["date_of_effectivity"]  = txtb_date_of_effectivity.Text.ToString();
                    row2Edit[0]["approval_id"]          = approval_id;
                    row2Edit[0]["approval_status"]      = ddl_status.SelectedValue.ToString();
                    row2Edit[0]["step_increment_new"]   = ddl_new_step.SelectedValue.ToString();
                    row2Edit[0]["item_no"]              = txtb_item_no.Text.ToString();
                    row2Edit[0]["new_salary"]           = txtb_new_salary.Text.ToString();
                    row2Edit[0]["salary_grade"]         = txtb_salary_grade.Text.ToString();
                    row2Edit[0]["position_long_title"]  = txtb_position_title.Text.ToString();
                    row2Edit[0]["step_type"]            = ddl_step_type.SelectedValue.ToString();
                    row2Edit[0]["step_type_desc"]       = ddl_step_type.SelectedItem.Text.ToString();
                    switch (ddl_status.SelectedValue.ToString())
                    {
                        case CommonDB.CONST_APPR_STAT_NEW:
                            row2Edit[0]["approval_status_descr"] = MyCmn.CONST_APPR_STAT_NEW_DESCR;
                            break;
                        case CommonDB.CONST_APPR_STAT_PENDING:
                            row2Edit[0]["approval_status_descr"] = MyCmn.CONST_APPR_STAT_PENDING_DESCR;
                            break;
                        case CommonDB.CONST_APPR_STAT_SUBMITTED:
                            row2Edit[0]["approval_status_descr"] = MyCmn.CONST_APPR_STAT_SUBMITTED_DESCR;
                            break;
                        case CommonDB.CONST_APPR_STAT_APPROVED:
                            row2Edit[0]["approval_status_descr"] = "Final Approved";
                            break;
                        default:
                            break;
                    }

                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                    SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                }

                ViewState.Remove("AddEdit_Mode");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                resignation_panel.Update();
                int page_size   = gv_dataListGrid.PageSize;
                int counter     = 0;
                DataRow[] templ_tbl = dataListGrid.Select("", Session["SortField"].ToString() + " " + Session["SortOrder"].ToString());
                foreach (DataRow row in templ_tbl)
                {
                    string empl_id = saveRecord == MyCmn.CONST_ADD ? ddl_empl_name.SelectedValue.ToString() : dtSource.Rows[0]["empl_id"].ToString();
                    if (row["empl_id"].ToString() == empl_id.ToString().Trim())
                    {
                        if ((counter / page_size) > 0)
                        {
                            gv_dataListGrid.PageIndex = int.Parse((counter / page_size).ToString());
                        }
                        else
                        {
                            gv_dataListGrid.PageIndex = 0;
                        }

                        break;
                    }
                    counter = counter + 1;

                }
                MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                resignation_panel.Update();
                foreach (GridViewRow row2 in gv_dataListGrid.Rows)
                {
                    ImageButton imgbtn = row2.FindControl("imgbtn_editrow1") as ImageButton;
                    string empl_id = "";
                    empl_id         = imgbtn.CommandArgument.Split(',')[0];
                    string empl_idX = saveRecord == MyCmn.CONST_ADD ? ddl_empl_name.SelectedValue.ToString() : dtSource.Rows[0]["empl_id"].ToString();
                    if (empl_id.Trim() == empl_idX.ToString().Trim())
                    {
                        gv_dataListGrid.SelectRow(row2.RowIndex);
                        break;
                    }
                }

                String remove_empl = "empl_id = '"+ dtSource.Rows[0]["empl_id"].ToString() + "'";
                DataRow[] row2Delete = dtEmpList.Select(remove_empl);
                if (row2Delete != null && row2Delete.Length > 0)
                {
                    dtEmpList.Rows.Remove(row2Delete[0]);
                    dtEmpList.AcceptChanges();
                    ddl_empl_name.Items.Clear();
                    ddl_empl_name.DataSource = dtEmpList;
                    ddl_empl_name.DataBind();
                    ListItem li = new ListItem("-- Select Here --", "");
                    ddl_empl_name.Items.Insert(0, li);
                }
               
            }
        }

        //*****************************************************************************************
        //  BEGIN - Jade- 10/11/18 - Submitted Record for Approval
        //*****************************************************************************************
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string l_AddEdit_Mode   = ViewState["AddEdit_Mode"].ToString();
            string approval_id      = "";
            if (IsDataValidated())
            {
                //if (l_AddEdit_Mode == MyCmn.CONST_ADD)
                //{
                //    approval_id = MyCmn.SaveGetApprovalWorkFlow(Session["ep_user_id"].ToString(), Session["ep_empl_id"].ToString(), CONST_TRANS_CODE);
                //}
                //else if (l_AddEdit_Mode == MyCmn.CONST_EDIT)
                //{
                //    if (lbl_hidden_appr_status.Text == "C")
                //    {
                //        MyCmn.SaveUpdateApprovalWorkFlow(approval_id, Session["ep_user_id"].ToString(), CommonDB.CONST_APPR_STAT_SUBMITTED, "Re-Submit");
                //    }
                //    else
                //    {
                //        approval_id = MyCmn.SaveGetApprovalWorkFlow(Session["ep_user_id"].ToString(), Session["ep_empl_id"].ToString(), CONST_TRANS_CODE);
                //    }
                //}

                Save_AddEditSubmit(CommonDB.CONST_APPR_STAT_SUBMITTED,approval_id);
            }
        }

        protected void ddl_new_step_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_new_step.SelectedValue.ToString().Trim() != "")
            {
                DataTable dt = MyCmn.RetrieveData("sp_get_salaries_monthly_per_step", "par_budget_code", ddl_budget_code.SelectedValue.ToString(), "par_step_increment", ddl_new_step.SelectedValue.ToString(), "par_salary_grade", txtb_salary_grade.Text.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtb_new_salary.Text = dt.Rows[0]["new_monthly_rate"].ToString();
                }
                else
                {
                    txtb_new_salary.Text = "";
                }
            }
            else
            {
                txtb_new_salary.Text = "";
                FieldValidationColorChanged(true, "ddl_new_step");
            }
        }

        protected void btn_extract_Click(object sender, EventArgs e)
        {
            DataTable dt        = new DataTable();
            string yes_overrid  = "";
            yes_overrid         = (sender as Button).AccessKey;

            dt = MyCmn.RetrieveData("sp_extract_employee_with_step_increment", "par_budget_code", ddl_budget_code.SelectedValue.ToString().Trim(), "par_user_id", Session["ep_user_id"].ToString().Trim(), "par_flag_override_existing", yes_overrid);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopX", "openExtractNotifiation('"+dt.Rows[0]["result_msg"]+"');", true);
            RetrieveDataListGrid();
        }
        //**********************************************************************************
        //  END OF CODE - Jade- 10/02/2018 
        //*********************************************************************************


    }
}