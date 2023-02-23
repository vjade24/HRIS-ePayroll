//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Process Monitor Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade H. Alivio     04/17/2020    Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cPrcMonitor
{
    public partial class cPrcMonitor : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - VJA- 04/17/2020 - Data Place holder creation 
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
        //  BEGIN - VJA- 04/17/2020 - Public Variable used in Add/Edit Mode
        //********************************************************************
        CommonDB MyCmn = new CommonDB();
        //********************************************************************
        //  BEGIN - VJA- 04/17/2020 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "prc_nbr";
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
        //  BEGIN - VJA- 04/17/2020 - Page Load Complete method
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
        //  BEGIN - VJA- 04/17/2020 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            ddl_user_id.Enabled = false;
            ddl_user_id.SelectedValue = Session["ep_user_id"].ToString().Trim();

            var month = DateTime.Now.Month.ToString();
            if (Convert.ToInt32(month) < 10)
            {
                month = "0" + month;
            }
            ddl_month.SelectedValue = month;

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            RetrieveYear();
            RetrieveUser();
            RetrieveStatus();
            RetrieveDataListGrid();
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Populate Combo list for Payroll Year
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
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveUser()
        {
            ddl_user_id.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_userprofile_tbl_list3", "p_module_id", "3");

            ddl_user_id.DataSource = dt;
            ddl_user_id.DataValueField = "user_id";
            ddl_user_id.DataTextField = "user_id";
            ddl_user_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_user_id.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 01/17/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveStatus()
        {
            ddl_status.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_prcstatus_tbl_list");

            ddl_status.DataSource = dt;
            ddl_status.DataValueField = "prc_status";
            ddl_status.DataTextField = "prc_status_descr";
            ddl_status.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_status.Items.Insert(0, li);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            // dataListGrid = MyCmn.RetrieveData("sp_prcmonitor_tbl_list_PAY", "p_year", ddl_year.SelectedValue.ToString().Trim(), "p_month", ddl_month.SelectedValue.ToString().Trim(), "p_user_id", ddl_user_id.SelectedValue.ToString().Trim(), "p_prc_status", ddl_status.SelectedValue.ToString().Trim(),"p_module_id","3");
            dataListGrid = MyCmn.RetrieveData("sp_prcmonitor_tbl_list_PAY", "p_year", ddl_year.SelectedValue.ToString().Trim(), "p_month", ddl_month.SelectedValue.ToString().Trim(), "p_user_id", ddl_user_id.SelectedValue.ToString().Trim(), "p_prc_status", ddl_status.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_process_id.Text        = "";
            txtb_process_nbr.Text       = "";
            txtb_run_start.Text         = "";
            txtb_run_end.Text           = "";
            txtb_status.Text            = "";
            txtb_run_params.Text        = "";
            txtb_prc_name.Text          = "";
            txtb_status_details.Text    = "";
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("prc_nbr", typeof(System.String));
            dtSource.Columns.Add("prc_id", typeof(System.String));
            dtSource.Columns.Add("prc_dttm_begin", typeof(System.String));
            dtSource.Columns.Add("prc_dttm_end", typeof(System.String));
            dtSource.Columns.Add("prc_runby_user_id", typeof(System.String));
            dtSource.Columns.Add("prc_parameters", typeof(System.String));
            dtSource.Columns.Add("prc_status", typeof(System.String));

        }
        //*************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "prcmonitor_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "prc_nbr" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();

            nrow["prc_nbr"] = string.Empty;
            nrow["prc_id"] = string.Empty;
            nrow["prc_dttm_begin"] = string.Empty;
            nrow["prc_dttm_end"] = string.Empty;
            nrow["prc_runby_user_id"] = string.Empty;
            nrow["prc_parameters"] = string.Empty;
            nrow["prc_status"] = string.Empty;
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string prc_nbr = commandArgs[0];
            
            deleteRec1.Text = "Are you sure to delete this Record ?";
            delete_header.InnerText = "Delete this Record";
            lnkBtnYes.Text = " Yes, Delete it ";
            
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);

        }
        //*************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "prc_nbr = '" + commandarg[0].Trim() + "' ";
            
            MyCmn.DeleteBackEndData("prcmonitor_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";


        }
        //**************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });
            string editExpression = "prc_nbr = '" + svalues[0].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["prc_nbr"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);

            txtb_process_id.Text        =  row2Edit[0]["prc_id"].ToString();
            txtb_process_nbr.Text       =  row2Edit[0]["prc_nbr"].ToString();
            txtb_run_start.Text         =  row2Edit[0]["prc_dttm_begin"].ToString();
            txtb_run_end.Text           =  row2Edit[0]["prc_dttm_end"].ToString();
            txtb_status.Text            =  row2Edit[0]["prc_status_short_descr"].ToString();
            txtb_run_params.Text        =  row2Edit[0]["prc_parameters"].ToString();
            txtb_prc_name.Text          =  row2Edit[0]["prc_name"].ToString();
            txtb_status_details.Text    =  row2Edit[0]["prc_error_msg"].ToString();
            
            LabelAddEdit.Text = "View Details";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        //**************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Change Field Sort mode  
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
        //  BEGIN - VJA- 04/17/2020 - Get Grid current sort order 
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
        //  BEGIN - VJA- 04/17/2020 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //**********************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtb_search.Text.ToString().Trim());
        }
        //**********************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void SearchData(string search)
        {
            string searchExpression =
               "prc_nbr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "prc_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "prc_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "prc_dttm_begin LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "prc_dttm_end LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "prc_status LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "prc_parameters LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "prc_status_short_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR " +
               "prc_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";
            
            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("prc_nbr", typeof(System.String));
            dtSource1.Columns.Add("prc_id", typeof(System.String));
            dtSource1.Columns.Add("prc_name", typeof(System.String));
            dtSource1.Columns.Add("prc_dttm_begin", typeof(System.String));
            dtSource1.Columns.Add("prc_dttm_end", typeof(System.String));
            dtSource1.Columns.Add("prc_runby_user_id", typeof(System.String));
            dtSource1.Columns.Add("prc_parameters", typeof(System.String));
            dtSource1.Columns.Add("prc_status_short_descr", typeof(System.String));
            dtSource1.Columns.Add("prc_status_descr", typeof(System.String));
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
        //  BEGIN - VJA- 04/17/2020 - Define Property for Sort Direction  
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
        //*****************************************************************************
        //  BEGIN - VJA- 04/17/2020 - Retrieve When the Dropdown changed
        //*****************************************************************************
        protected void RetrieveGrid(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
    }
}