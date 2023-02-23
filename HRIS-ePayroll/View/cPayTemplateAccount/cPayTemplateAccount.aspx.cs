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

namespace HRIS_ePayroll.View.cPayTemplateAccount
{
    public partial class cPayTemplateAccount : System.Web.UI.Page
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
        DataTable dtsource_for_payrolltemp
        {
            get
            {
                if ((DataTable)ViewState["dtsource_for_payrolltemp"] == null) return null;
                return (DataTable)ViewState["dtsource_for_payrolltemp"];
            }
            set
            {
                ViewState["dtsource_for_payrolltemp"] = value;
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
                    Session["SortField"] = "report_column_order";
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
            RetrieveEmploymentType();

            //Retrieve When Add
            RetrieveDataPayrollTemplate();
            RetrieveDataGroupCode();
            RetrieveDataRefField();
            RetrieveDataRefTable();
            RetrieveDataAccount();
            RetrieveDataSubAccount();
           
            btnAdd.Visible = false;
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list from Salary Grade Table
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

            ddl_related_acct.Items.Clear();
            DataTable dtEmpList1 = MyCmn.RetrieveData("sp_payrollaccounts_tbl_list");

            ddl_related_acct.DataSource = dtEmpList1;
            ddl_related_acct.DataTextField = "account_title";
            ddl_related_acct.DataValueField = "account_code";
            ddl_related_acct.DataBind();
            ListItem li1 = new ListItem("-- Select Here --", "");
            ddl_related_acct.Items.Insert(0, li1);
        }
        private void RetrieveDataSubAccount()
        {
            ddl_subaccount.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_payroll_subaccount_tbl_list", "par_account_code" ,ddl_account.SelectedValue.ToString().Trim());

            ddl_subaccount.DataSource = dtEmpList;
            ddl_subaccount.DataTextField = "account_sub_title";
            ddl_subaccount.DataValueField = "account_sub_code";
            ddl_subaccount.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_subaccount.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_payrollmapping_tbl_list", "par_payrolltemplate_code", ddl_payrolltemplate.SelectedValue.ToString().Trim(),"par_employment_type" , ddl_empl_type.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list from Salary Grade Table
        //*************************************************************************
        private void RetrieveDataPayrollTemplate()
        {
            ddl_payrolltemplate.Items.Clear();

            dtsource_for_payrolltemp = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list","par_employment_type" ,ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payrolltemplate.DataSource = dtsource_for_payrolltemp;
            ddl_payrolltemplate.DataTextField = "payrolltemplate_descr";
            ddl_payrolltemplate.DataValueField = "payrolltemplate_code";
            ddl_payrolltemplate.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payrolltemplate.Items.Insert(0, li);
        }
        private void RetrieveDataRefTable()
        {
            ddl_ref_table.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_fielddescr_tbl_list2");

            ddl_ref_table.DataSource = dtEmpList;
            ddl_ref_table.DataTextField = "field_table";
            ddl_ref_table.DataValueField = "field_table";
            ddl_ref_table.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_ref_table.Items.Insert(0, li);
        }
        private void RetrieveDataRefField()
        {
            ddl_ref_field.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_fielddescr_tbl_list", "par_field_table", ddl_ref_table.SelectedValue.ToString().Trim());

            ddl_ref_field.DataSource = dtEmpList;
            ddl_ref_field.DataTextField = "field_descr"; 
            ddl_ref_field.DataValueField = "field_name";
            ddl_ref_field.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_ref_field.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list from Salary Grade Table
        //*************************************************************************
        private void RetrieveDataGroupCode()
        {
            ddl_account_group_code.Items.Clear();
            DataTable dtEmpList = MyCmn.RetrieveData("sp_payrollaccountgroups_tbl_list");

            ddl_account_group_code.DataSource = dtEmpList;
            ddl_account_group_code.DataTextField = "group_descr"; 
            ddl_account_group_code.DataValueField = "group_code";
            ddl_account_group_code.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_account_group_code.Items.Insert(0, li);
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

            ddl_account.Enabled = true;
            ddl_subaccount.Enabled = true;

            //DisableRefFieldandTable();

            txtb_payroll_template_disabled.Text = ddl_payrolltemplate.SelectedItem.ToString().Trim();
            txtb_empl_type_disabled.Text = ddl_empl_type.SelectedItem.ToString().Trim();
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //private void DisableRefFieldandTable()
        //{
        //    //ddl_payrolltemplate.SelectedValue = ViewState["payrolltemplate_type"].ToString();
        //    ViewState["payrolltemplate_type"] = dtsource_for_payrolltemp.Rows[0]["payrolltemplate_type"].ToString();

        //    string editExpression = "payrolltemplate_type = '" + ddl_payrolltemplate.SelectedValue.ToString().Trim() + "'";
        //    DataRow[] row2Edit = dtsource_for_payrolltemp.Select(editExpression);

        //    //if ()
        //    //{
        //    //    container_of_reference.Visible = false;
        //    //}
        //    //else
        //    //{
        //    //    container_of_reference.Visible = true;
        //    //}
        //}
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_empl_type_disabled.Text = "";
            txtb_payroll_template_disabled.Text = "";
            ddl_account_group_code.SelectedIndex = 0;
            ddl_ref_table.SelectedIndex = 0;
            ddl_ref_field.SelectedIndex = 0;
            ddl_account.SelectedIndex = 0;
            ddl_related_acct.SelectedIndex = 0;
            ddl_ref_field.SelectedIndex = 0;

            txtb_report_column_order.Text = "";
            txtb_report_column_order.Text = "";
            //ddl_subaccount.SelectedIndex = 0;
            
        }

        //*************************************************************************
        //  JADE - Jade- 09/28/2018 - Get the Last Control Number
        //*************************************************************************
        //private void RetrieveNextSubAccountCode()
        //{
        //    string sql1 = "SELECT TOP 1 account_sub_code from payrollmapping_tbl where LEFT(account_sub_code,2)=LEFT(account_sub_code,2) order by account_sub_code DESC";
        //    string last_row = MyCmn.GetLastRow_of_Table(sql1);
        //    if (last_row.Trim() == "")
        //    {
        //        last_row = "0";
        //    }
        //    int last_row1 = int.Parse(last_row) + 1;
        //    txtb_payroll_template.Text = last_row1.ToString().PadLeft(2, '0');
        //}
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payrolltemplate_code",typeof(System.String));
            dtSource.Columns.Add("account_code",typeof(System.String));
            dtSource.Columns.Add("account_sub_code",typeof(System.String));
            dtSource.Columns.Add("group_code",typeof(System.String));
            dtSource.Columns.Add("report_column_order",typeof(System.String));
            dtSource.Columns.Add("reference_table",typeof(System.String));
            dtSource.Columns.Add("reference_field",typeof(System.String));
            dtSource.Columns.Add("related_acct_code",typeof(System.String));
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollmapping_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payrolltemplate_code", "account_code", "account_sub_code" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();

            nrow["payrolltemplate_code"]=string.Empty;
            nrow["account_code"]=string.Empty;
            nrow["account_sub_code"]=string.Empty;
            nrow["group_code"]=string.Empty;
            nrow["report_column_order"]=string.Empty;
            nrow["reference_table"]=string.Empty;
            nrow["reference_field"] = string.Empty;
            nrow["related_acct_code"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string appttype = commandArgs[0];
            string appttypedescr = commandArgs[1];

            deleteRec1.Text = "Are you sure to delete this Record = (" + appttype.Trim() + ") - " + appttypedescr.Trim() + " ?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "payrolltemplate_code = '" + svalues[0].Trim() + "' AND account_code = '" + svalues[1].Trim() + "' AND account_sub_code = '" + svalues[2].Trim() + "'";

            MyCmn.DeleteBackEndData("payrollmapping_tbl", "WHERE " + deleteExpression);

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
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' }); ;
            string editExpression = "payrolltemplate_code = '" + svalues[0].Trim() + "' AND account_code = '" + svalues[1].Trim() + "' AND account_sub_code = '" + svalues[2].Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["payrolltemplate_code"] = string.Empty;
            nrow["account_code"] = string.Empty;
            nrow["account_sub_code"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_empl_type_disabled.Text = ddl_empl_type.SelectedItem.ToString().Trim();
            txtb_payroll_template_disabled.Text = ddl_payrolltemplate.SelectedItem.ToString().Trim();
            
            ddl_account.SelectedValue = row2Edit[0]["account_code"].ToString();
            ddl_related_acct.SelectedValue = row2Edit[0]["related_acct_code"].ToString();

            if (ddl_subaccount.SelectedValue == "")
            {
                RetrieveDataSubAccount();
                ddl_subaccount.SelectedValue = row2Edit[0]["account_sub_code"].ToString();
            }
            ddl_ref_table.SelectedValue = row2Edit[0]["reference_table"].ToString();
            RetrieveDataRefField();
            ddl_ref_field.SelectedValue = row2Edit[0]["reference_field"].ToString();
            txtb_report_column_order.Text = row2Edit[0]["report_column_order"].ToString();
            
            ddl_account_group_code.SelectedValue = row2Edit[0]["group_code"].ToString();
            
            ddl_account.Enabled = false;
            ddl_subaccount.Enabled = false;

            txtb_payroll_template_disabled.ReadOnly = true;
            LabelAddEdit.Text = "Edit Record: " + txtb_payroll_template_disabled.Text.Trim();
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
                    dtSource.Rows[0]["payrolltemplate_code"]        = ddl_payrolltemplate.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_code"]                = ddl_account.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_sub_code"]            = ddl_subaccount.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["group_code"]                  = ddl_account_group_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["report_column_order"]         = txtb_report_column_order.Text.ToString().Trim();
                    dtSource.Rows[0]["reference_table"]             = ddl_ref_table.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["reference_field"]             = ddl_ref_field.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_acct_code"]           = ddl_related_acct.SelectedValue.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payrolltemplate_code"]        = ddl_payrolltemplate.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_code"]                = ddl_account.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["account_sub_code"]            = ddl_subaccount.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["group_code"]                  = ddl_account_group_code.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["report_column_order"]         = txtb_report_column_order.Text.ToString().Trim();
                    dtSource.Rows[0]["reference_table"]             = ddl_ref_table.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["reference_field"]             = ddl_ref_field.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_acct_code"]           = ddl_related_acct.SelectedValue.ToString().Trim();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    FieldValidationColorChanged(false, "ALL");
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

                        nrow["payrolltemplate_code"] = ddl_payrolltemplate.SelectedValue.ToString().Trim();
                        nrow["account_code"] = ddl_account.SelectedValue.ToString().Trim();
                        nrow["account_sub_code"] = ddl_subaccount.SelectedValue.ToString().Trim();
                        nrow["group_code"] = ddl_account_group_code.SelectedValue.ToString().Trim();
                        nrow["report_column_order"]  = txtb_report_column_order.Text.ToString().Trim();
                        nrow["reference_table"] = ddl_ref_table.SelectedValue.ToString().Trim();
                        nrow["reference_field"] = ddl_ref_field.SelectedValue.ToString().Trim();
                        nrow["related_acct_code"] = ddl_related_acct.SelectedValue.ToString().Trim();

                        nrow["account_title"] = ddl_account.SelectedItem.ToString().Trim();
                        nrow["field_descr"] = ddl_ref_field.SelectedItem.ToString().Trim();
                        
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "payrolltemplate_code = '" + ddl_payrolltemplate.SelectedValue.ToString() + "' AND account_code = '" + ddl_account.SelectedValue.ToString() + "' AND account_sub_code = '" + ddl_subaccount.SelectedValue.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payrolltemplate_code"] = ddl_payrolltemplate.SelectedValue.ToString().Trim();
                        row2Edit[0]["account_code"] = ddl_account.SelectedValue.ToString().Trim();
                        row2Edit[0]["account_sub_code"] = ddl_subaccount.SelectedValue.ToString().Trim();
                        row2Edit[0]["group_code"] = ddl_account_group_code.SelectedValue.ToString().Trim();
                        row2Edit[0]["reference_table"] = ddl_ref_table.SelectedValue.ToString().Trim();
                        row2Edit[0]["reference_field"] = ddl_ref_field.SelectedValue.ToString().Trim();
                        row2Edit[0]["report_column_order"] = txtb_report_column_order.Text.ToString().Trim();
                        row2Edit[0]["related_acct_code"] = ddl_related_acct.SelectedValue.ToString().Trim();

                        row2Edit[0]["account_title"] = ddl_account.SelectedItem.ToString().Trim();
                        row2Edit[0]["field_descr"] = ddl_ref_field.SelectedItem.ToString().Trim();
                       
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
            string searchExpression = "account_code LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR account_title LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR report_column_order LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'  OR account_sub_title LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("account_code", typeof(System.String));
            dtSource1.Columns.Add("account_sub_code", typeof(System.String));
            dtSource1.Columns.Add("group_code", typeof(System.String));
            dtSource1.Columns.Add("acctclass", typeof(System.String));
            dtSource1.Columns.Add("report_column_order", typeof(System.String));
            dtSource1.Columns.Add("reference_table", typeof(System.String));
            dtSource1.Columns.Add("reference_field", typeof(System.String));
            dtSource1.Columns.Add("field_descr", typeof(System.String));
            dtSource1.Columns.Add("account_title", typeof(System.String));
            dtSource1.Columns.Add("account_sub_title", typeof(System.String));
            dtSource1.Columns.Add("related_acct_code", typeof(System.String));
            
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
        //**************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;


            if (ddl_account.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_account");
                ddl_account.Focus();
                validatedSaved = false;
            }
            //else if (ddl_subaccount.SelectedValue == "")
            //{
            //    FieldValidationColorChanged(true, "ddl_subaccount");
            //    ddl_subaccount.Focus();
            //    validatedSaved = false;
            //}
            if (ddl_related_acct.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_related_acct");
                ddl_related_acct.Focus();
                validatedSaved = false;
            }
            if (ddl_account_group_code.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_account_group_code");
                ddl_account_group_code.Focus();
                validatedSaved = false;
            }
            
            if (txtb_report_column_order.Text == "")
            {
                FieldValidationColorChanged(true, "txtb_report_column_order");
                txtb_report_column_order.Focus();
                validatedSaved = false;
            }
            if (CommonCode.checkisdecimal(txtb_report_column_order)==false)
            {
                FieldValidationColorChanged(true, "invalid-numeric");
                txtb_report_column_order.Focus();
                validatedSaved = false;
            }
            if (ddl_ref_table.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_ref_table");
                ddl_ref_table.Focus();
                validatedSaved = false;
            }
            if (ddl_ref_field.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_ref_field");
                ddl_ref_field.Focus();
                validatedSaved = false;
            }
            return validatedSaved;
        }

        //**********************************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_account":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_account.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_subaccount":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            ddl_subaccount.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_acct":
                        {
                            LblRequired9.Text = MyCmn.CONST_RQDFLD;
                            ddl_related_acct.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_account_group_code":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            ddl_account_group_code.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_report_column_order":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_report_column_order.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-numeric":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_report_column_order.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_ref_table":
                        {
                            LblRequired7.Text = MyCmn.CONST_RQDFLD;
                            ddl_ref_table.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_ref_field":
                        {
                            LblRequired8.Text = MyCmn.CONST_RQDFLD;
                            ddl_ref_field.BorderColor = Color.Red;
                            break;
                        }

                    case "already-exist":
                        {
                            LblRequired1.Text = "already exist! (change atleast one data)";
                            LblRequired2.Text = "already exist! (change atleast one data)";
                            ddl_account.BorderColor = Color.Red;
                            ddl_subaccount.BorderColor = Color.Red;
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
                            
                            LblRequired7.Text = "";
                            LblRequired8.Text = "";
                            LblRequired9.Text = "";
                            ddl_account.BorderColor = Color.LightGray;
                            ddl_subaccount.BorderColor = Color.LightGray;
                            ddl_account_group_code.BorderColor = Color.LightGray;
                            txtb_report_column_order.BorderColor = Color.LightGray;
                            
                            ddl_ref_table.BorderColor = Color.LightGray;
                            ddl_ref_field.BorderColor = Color.LightGray;
                            ddl_related_acct.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }
        protected void ddl_payrolltemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_payrolltemplate.SelectedValue != "")
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

        protected void ddl_ref_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddl_ref_table.SelectedValue != "")
            {
                RetrieveDataRefField();
            }
        }
        protected void ddl_account_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            //if (IsDataValidated())
            //{
                if (ddl_account.SelectedValue != "")
                {
                    RetrieveDataSubAccount();
                }
            //}
            
        }

        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_payrolltemplate.SelectedValue != "")
            {
                RetrieveDataListGrid();
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            RetrieveDataPayrollTemplate();
            UpdatePanel10.Update();
        }
    }
}