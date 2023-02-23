//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Other Contribution and Loans
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
//Joseph M. Tombo Jr     03/28/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;


namespace HRIS_ePayroll.View.cPayOthContributionsGSIS
{
    public partial class cPayOthContributionsGSIS : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Data Place holder creation 
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
        DataTable dtSource_dtl_upload
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_upload"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_upload"];
            }
            set
            {
                ViewState["dtSource_dtl_upload"] = value;
            }
        }
        DataTable dtSource_dtl_rejected
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_rejected"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_rejected"];
            }
            set
            {
                ViewState["dtSource_dtl_rejected"] = value;
            }
        }
        //********************************************************************
        //  BEGIN - JVA- 09/12/2018 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Page Load method
        //********************************************************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ep_user_id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["SortField"] = "employee_name";
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

                if (Session["PreviousValuesonPage_cPayOthCOntributionsGSIS"] == null)
                    Session["PreviousValuesonPage_cPayOthCOntributionsGSIS"] = "";
                else if (Session["PreviousValuesonPage_cPayOthCOntributionsGSIS"].ToString() != string.Empty)
                {
                    RetrieveYear();
                    string[] prevValues = Session["PreviousValuesonPage_cPayOthCOntributionsGSIS"].ToString().Split(new char[] { ',' });
                    ddl_year.SelectedValue              = prevValues[0].ToString();
                    ddl_month.SelectedValue             = prevValues[1].ToString();
                    ddl_report_list.SelectedValue       = prevValues[2].ToString();
                    
                    RetrieveDataListGrid();
                    gv_dataListGrid.PageIndex           = int.Parse(prevValues[3].ToString());
                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                    up_dataListGrid.Update();
                    DropDownListID.SelectedValue = prevValues[4].ToString();
                    txtb_search.Text = prevValues[5].ToString();
                    // SearchData(prevValues[8].ToString());
                    btn_options.Visible = true;
                    Update_btnAdd.Update();
                }
            }
        }

        //********************************************************************
        //  BEGIN - JVA- 09/20/2018 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"]    = SortDirection.Ascending.ToString();
            Session["cPositionTable"]   = "cPositionTable";

            RetrieveYear();
            if (ddl_year.SelectedValue.ToString().Trim() != "" 
                && ddl_month.SelectedValue.ToString().Trim() != "" 
                && ddl_report_list.SelectedValue.ToString().Trim() != ""
                )
            {
                btn_options.Visible = true;
            }
            else
            {
                btn_options.Visible = false;
            }

            RetrieveDataListGrid();
            Update_btnAdd.Update();
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Payroll Year
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
        //  BEGIN - JVA- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_gsis_ded_ledger_uploaded_current_list", "p_uploaded_year", ddl_year.SelectedValue.ToString(), "p_uploaded_month", ddl_month.SelectedValue.ToString(), "p_uploaded_by_user", Session["ep_user_id"].ToString(), "p_report_list", ddl_report_list.SelectedValue.ToString());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
           txtb_last_name.Text         = "";
           txtb_first_name.Text        = "";
           txtb_middle_name.Text       = "";
           txtb_prefix_name.Text       = "";
           txtb_appellation.Text       = "";
           txtb_birth_date.Text        = "";
           txtb_gsis_crn.Text          = "";
           txtb_monthly_salary.Text    = "";
           txtb_effectivity_date.Text  = "";

        }
        
        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string editExpression = "empl_id = '" + svalues.ToString().Trim() + "'";
            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            
            txtb_last_name.Text         = row2Edit[0]["last_name"].ToString();
            txtb_first_name.Text        = row2Edit[0]["first_name"].ToString();
            txtb_middle_name.Text       = row2Edit[0]["middle_name"].ToString();
            txtb_prefix_name.Text       = row2Edit[0]["prefix_name"].ToString();
            txtb_appellation.Text       = row2Edit[0]["appellation"].ToString();
            txtb_birth_date.Text        = row2Edit[0]["birth_date"].ToString();
            txtb_gsis_crn.Text          = row2Edit[0]["gsis_crn"].ToString();
            txtb_monthly_salary.Text    = row2Edit[0]["monthly_salary"].ToString();
            txtb_effectivity_date.Text  = row2Edit[0]["effectivity_date"].ToString();
            
            LabelAddEdit.Text = "View Uploaded Information";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        
        //**************************************************************************
        //  BEGIN - JVA- 09/20/2018 - Change Field Sort mode  
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
        //  BEGIN - JVA- 09/20/2018 - Get Grid current sort order 
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
        //  BEGIN - JVA- 09/12/2018 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        
        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JVA- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                                   "OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                                   "OR deduc_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                                   "OR uploaded_amt LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " +
                                   "OR deduc_amount1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' " ;

            DataTable dtSource1 = new DataTable();
            
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("account_id_nbr_ref", typeof(System.String));
            dtSource1.Columns.Add("deduc_code", typeof(System.String));
            dtSource1.Columns.Add("deduc_descr", typeof(System.String));
            dtSource1.Columns.Add("deduc_amount1", typeof(System.String));
            dtSource1.Columns.Add("uploaded_amt", typeof(System.String));
            
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
        //  BEGIN - JVA- 09/20/2018 - Define Property for Sort Direction  
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
        //  BEGIN - JVA- 09/20/2018 - Check if Object already contains value  
        //*************************************************************************
        protected void CheckInputValue(object sender, EventArgs e)
        {
            TextBox TextBox1 = (TextBox)sender;
            string checkValue = TextBox1.Text;
            string checkName = TextBox1.ID;
            
            TextBox1.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
            TextBox1.Focus();
        }
        
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Button Upload File
        //*************************************************************************
        protected void lnkbtn_upload_file_Click(object sender, EventArgs e)
        {
            ClearEntry();
            txtb_year_dslp.Text = ddl_year.SelectedItem.ToString();
            txtb_month_dslp.Text = ddl_month.SelectedItem.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopX", "openModal_Upload();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Button Show Uploaded
        //*************************************************************************
        protected void lnkbtn_show_uploaded_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopY", " openModal_ShowUploaded();", true);
        }
        //*************************************************************************
        //  BEGIN - VJA- 2019/11/04 - Select Employment Type
        //*************************************************************************
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_month.SelectedValue.ToString().Trim() != ""
                   && ddl_year.SelectedValue.ToString().Trim() != ""
                   && ddl_report_list.SelectedValue.ToString().Trim() != "")
            {
                btn_options.Visible = true;
            }
            else
            {
                btn_options.Visible = false;
            }
            RetrieveDataListGrid();
            Update_btnAdd.Update();
        }

        protected void lnkbtn_exec_upload_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopZ", "ExecuteUpload_FileGSIS();", true);
            RetrieveDataListGrid();

            Session["PreviousValuesonPage_cPayOthCOntributionsGSIS"] =
                                                                        ddl_year.SelectedValue + "," +
                                                                        ddl_month.SelectedValue + "," +
                                                                        ddl_report_list.SelectedValue + "," +
                                                                        gv_dataListGrid.PageIndex + "," +
                                                                        DropDownListID.SelectedValue + "," +
                                                                        txtb_search.Text;


        }
        
        
        protected void Dropdown_rejected_SelectedIndexChanged(object sender, EventArgs e)
        {
            gv_show_rejected.PageSize = Convert.ToInt32(Dropdown_rejected.SelectedValue.ToString());
            MyCmn.Sort(gv_show_rejected, dtSource_dtl_rejected, "empl_id", Session["SortOrder"].ToString());
            update_panel_show_rejected.Update();
        }

        protected void gv_show_rejected_Sorting(object sender, GridViewSortEventArgs e)
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
            Session["SortField"] = "empl_id";
            Session["SortOrder"] = sortingDirection;

            MyCmn.Sort(gv_show_rejected, dtSource_dtl_rejected, e.SortExpression, sortingDirection);
        }

        protected void gv_show_rejected_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_show_rejected.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_show_rejected, dtSource_dtl_rejected, "empl_id", Session["SortOrder"].ToString());
        }

        protected void lnk_btn_print_Click(object sender, EventArgs e)
        {
            string printreport;
            string procedure;
            string url = "";
            Session["history_page"] = Request.Url.AbsolutePath;
            printreport = "/cryGSISUploadedList/cryGSISUploadedList.rpt";
            procedure = "sp_gsis_ded_ledger_uploaded_current_list";
            url = "/printView/printView.aspx?id=~/Reports/" + printreport + "," + procedure + ",p_uploaded_year," + ddl_year.SelectedValue.ToString().Trim() + ",p_uploaded_month," + ddl_month.SelectedValue.ToString() + ",p_uploaded_by_user," + Session["ep_user_id"].ToString() + ",p_report_list," + ddl_report_list.SelectedValue.ToString();

            if (url != "")
            {
                Response.Redirect(url);
            }
        }

        //********************************************************************
        // END OF THE CODE
        //********************************************************************

    }
}