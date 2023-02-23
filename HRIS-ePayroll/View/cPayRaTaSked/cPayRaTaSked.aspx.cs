//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for RA and TA Sked
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Joseph M. Tombo Jr    03/22/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View
{
    public partial class cPayRaTaSked : System.Web.UI.Page
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
        //  BEGIN - AEC- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {

            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPayRaTaSked"] = "cPayRaTaSked";
            RetrieveDataListGrid();
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            string include_history = "N";
            if (chkIncludeHistory.Checked)
            {
                include_history = "Y";
            }
            dataListGrid = MyCmn.RetrieveData("sp_rata_rate_sked_tbl_list", "par_include_history",include_history);
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ddl_rata_type.Enabled = true;
            txtb_max_no_days.Enabled = true;
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            lbl_effective_date.Text = "";
            txtb_max_no_days.Text = "";
            txtb_rate_percent.Text = "";

            ddl_rata_type.SelectedIndex = -1;
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["rate_type"] = string.Empty;
            nrow["no_of_days_max"] = string.Empty;
            nrow["effective_date"] = string.Empty;
            nrow["rate_percentage"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
            lbl_effective_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("rate_type", typeof(System.String));
            dtSource.Columns.Add("no_of_days_max", typeof(System.String));
            dtSource.Columns.Add("effective_date", typeof(System.String));
            dtSource.Columns.Add("rate_percentage", typeof(System.String));
            
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "rata_rate_sked_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "rate_type", "no_of_days_max", "effective_date" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //***************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            deleteRec1.Text = "Are you sure to delete this Record ?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string deleteExpression = "effective_date = '" + svalues[0] + "' AND rate_type='"+ svalues[1] + "' AND no_of_days_max='" + svalues[2] + "'";

            MyCmn.DeleteBackEndData("rata_rate_sked_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "effective_date = '" + svalues[0] + "' AND rate_type='"+ svalues[1] + "' AND no_of_days_max ='"+svalues[2]+ "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["effective_date"]  = svalues[0];
            nrow["rate_type"]       = svalues[1];
            nrow["no_of_days_max"]  = svalues[2];

            ddl_rata_type.SelectedValue = svalues[1];
            lbl_effective_date.Text = svalues[0];
            txtb_max_no_days.Text = row2Edit[0]["no_of_days_max"].ToString();
            txtb_rate_percent.Text = row2Edit[0]["rate_percentage"].ToString();
            ddl_rata_type.Enabled = false;
            txtb_max_no_days.Enabled = false;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);
            LabelAddEdit.Text = "Edit Record: ";
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
                    dtSource.Rows[0]["rate_type"]       = ddl_rata_type.SelectedValue.ToString().ToString();
                    dtSource.Rows[0]["no_of_days_max"]  = txtb_max_no_days.Text.ToString().Trim();
                    dtSource.Rows[0]["effective_date"]  = DateTime.Now.ToString("yyyy-MM-dd");
                    dtSource.Rows[0]["rate_percentage"] = txtb_rate_percent.Text.ToString().Trim();
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["rate_percentage"] = txtb_rate_percent.Text.ToString().Trim();
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
                        ddl_rata_type.Focus();
                        return;
                    }


                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();

                        nrow["rate_type"]               = dtSource.Rows[0]["rate_type"];
                        nrow["rate_type_descr"]         = ddl_rata_type.SelectedItem.Text.ToString().Trim();
                        nrow["no_of_days_max"]          = dtSource.Rows[0]["no_of_days_max"];
                        nrow["effective_date"]          = dtSource.Rows[0]["effective_date"];
                        nrow["rate_percentage"]         = dtSource.Rows[0]["rate_percentage"];
                        nrow["rate_percentage_descr"]   = dtSource.Rows[0]["rate_percentage"]+"%";

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "rate_type = '" + dtSource.Rows[0]["rate_type"] + "' AND no_of_days_max ='"+ dtSource.Rows[0]["no_of_days_max"] + "' AND effective_date = '"+ dtSource.Rows[0]["effective_date"] + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        row2Edit[0]["rate_type"]                = dtSource.Rows[0]["rate_type"];
                        row2Edit[0]["rate_type_descr"]          = ddl_rata_type.SelectedItem.Text.ToString().Trim();
                        row2Edit[0]["no_of_days_max"]           = dtSource.Rows[0]["no_of_days_max"];
                        row2Edit[0]["effective_date"]           = dtSource.Rows[0]["effective_date"];
                        row2Edit[0]["rate_percentage"]          = dtSource.Rows[0]["rate_percentage"];
                        row2Edit[0]["rate_percentage_descr"]    = dtSource.Rows[0]["rate_percentage"] + "%";
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
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - AEC- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "effective_date LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR no_of_days_max LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR rate_percentage_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR rate_type_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("rate_type", typeof(System.String));
            dtSource1.Columns.Add("no_of_days_max", typeof(System.String));
            dtSource1.Columns.Add("effective_date", typeof(System.String));
            dtSource1.Columns.Add("rate_percentage", typeof(System.String));
            dtSource1.Columns.Add("rate_type_descr", typeof(System.String));
            dtSource1.Columns.Add("rate_percentage_descr", typeof(System.String));

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
        //  BEGIN - AEC- 09/20/2018 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            bool validatedSaved = true;

            if (ddl_rata_type.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_rata_type");
                ddl_rata_type.Focus();
                validatedSaved = false;
            }

            if (CommonCode.checkisdecimal(txtb_max_no_days) == false)
            {
                if (txtb_max_no_days.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_max_no_days");
                    txtb_max_no_days.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-max");
                    txtb_max_no_days.Focus();
                    validatedSaved = false;
                }
            }
            else
            {
                if (double.Parse(txtb_max_no_days.Text) < 0)
                {
                    FieldValidationColorChanged(true, "max-less-zero");
                    txtb_max_no_days.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_rate_percent) == false)
            {
                if (txtb_rate_percent.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_rate_percent");
                    txtb_rate_percent.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-percent");
                    txtb_rate_percent.Focus();
                    validatedSaved = false;
                }
            }
            else
            {
                if (double.Parse(txtb_rate_percent.Text) < 0)
                {
                    FieldValidationColorChanged(true, "percent-less");
                    txtb_rate_percent.Focus();
                    validatedSaved = false;
                }
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
                    case "ddl_rata_type":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_rata_type.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text = "Already Added with the same keys.";
                            ddl_rata_type.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_max_no_days":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_max_no_days.BorderColor = Color.Red;
                            break;
                        }
                    case "max-less-zero":
                        {
                            LblRequired2.Text = "Should not be less than zero.";
                            txtb_max_no_days.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-max":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_max_no_days.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_rate_percent":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_rate_percent.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-percent":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_rate_percent.BorderColor = Color.Red;
                            break;
                        }
                    case "percent-less":
                        {
                            LblRequired3.Text = "Should not be less that zero";
                            txtb_rate_percent.BorderColor = Color.Red;
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
                            ddl_rata_type.BorderColor = Color.LightGray;
                            txtb_max_no_days.BorderColor = Color.LightGray;
                            txtb_rate_percent.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }

        protected void chkIncludeHistory_CheckedChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }
    }
}