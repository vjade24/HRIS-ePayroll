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

namespace HRIS_ePayroll.View.cEmplRefIds
{
    public partial class cEmplRefIds : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - AEC- 09/20/2018 - Data Place holder creation 
        //********************************************************************

        //DataTable dtSource
        //{
        //    get
        //    {
        //        if ((DataTable)ViewState["dtSource"] == null) return null;
        //        return (DataTable)ViewState["dtSource"];
        //    }
        //    set
        //    {
        //        ViewState["dtSource"] = value;
        //    }
        //}
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
        DataTable dtSource_dtl
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl"];
            }
            set
            {
                ViewState["dtSource_dtl"] = value;
            }
        }
        DataTable dtSource_dtl_for_display
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_for_display"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_for_display"];
            }
            set
            {
                ViewState["dtSource_dtl_for_display"] = value;
            }
        }
        DataTable dtSource_for_names
        {
            get
            {
                if ((DataTable)ViewState["dtSource_for_names"] == null) return null;
                return (DataTable)ViewState["dtSource_for_names"];
            }
            set
            {
                ViewState["dtSource_for_names"] = value;
            }
        }
        DataTable dtSource_dtl_for_loop
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_for_loop"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_for_loop"];
            }
            set
            {
                ViewState["dtSource_dtl_for_loop"] = value;
            }
        }

        DataTable dtSource_dtl_ATM_nbr
        {
            get
            {
                if ((DataTable)ViewState["dtSource_dtl_ATM_nbr"] == null) return null;
                return (DataTable)ViewState["dtSource_dtl_ATM_nbr"];
            }
            set
            {
                ViewState["dtSource_dtl_ATM_nbr"] = value;
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
                    Session["SortField"] = "empl_id";
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
            ViewState["chkIncludeHistory"] = "N";
            
            RetrieveEmploymentType();
            RetrieveDataListGrid();
            RetrieveDataListGrid_dtl();
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_personnelnames_combolist_refnbr", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_lastname_st", ddl_lastname_starts.SelectedValue.ToString().Trim());
           
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }
        private void RetrieveDataListGrid_dtl()
        {
            dtSource_dtl_for_display = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_ref_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            dtSource_dtl_for_loop    = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_ref_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            dtSource_dtl_ATM_nbr     = MyCmn.RetrieveData("sp_payrollemployeemaster_tbl_ref_list", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_empl_id", txtb_empl_id.Text.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_details, dtSource_dtl_for_display);
            update_details.Update();
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
        
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable_dtl();
            AddPrimaryKeys_dtl();
            AddNewRow_dtl();

            InitializeTable_dtl_for_display();
            AddPrimaryKeys_dtl_for_display();
            AddNewRow_dtl_for_display();

            //for Details
           // ViewState["rate_basis"] = dtSource_for_names.Rows[0]["rate_basis"].ToString().Trim();
            RetrieveDataListGrid_dtl();
            //ddl_empl_name.Enabled = true;

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
            txtb_empl_id.Text   = "";
            txtb_empl_name.Text= "";
        }
        
        private void InitializeTable_dtl()
        {
            dtSource_dtl = new DataTable();
            dtSource_dtl.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl.Columns.Add("account_code", typeof(System.String));
            dtSource_dtl.Columns.Add("account_sub_code", typeof(System.String));
            dtSource_dtl.Columns.Add("account_id_nbr_ref", typeof(System.String));
            dtSource_dtl.Columns.Add("created_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("created_dttm", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_by_user", typeof(System.String));
            dtSource_dtl.Columns.Add("updated_dttm", typeof(System.String));
        }
        private void InitializeTable_dtl_for_display()
        {
            dtSource_dtl_for_display = new DataTable();
            dtSource_dtl_for_display.Columns.Add("empl_id", typeof(System.String));
            dtSource_dtl_for_display.Columns.Add("account_code", typeof(System.String));
            dtSource_dtl_for_display.Columns.Add("account_sub_code", typeof(System.String));
            dtSource_dtl_for_display.Columns.Add("account_title", typeof(System.String));
            dtSource_dtl_for_display.Columns.Add("account_id_nbr_ref", typeof(System.String));
            dtSource_dtl_for_display.Columns.Add("created_by_user", typeof(System.String));
            dtSource_dtl_for_display.Columns.Add("created_dttm", typeof(System.String));
            dtSource_dtl_for_display.Columns.Add("updated_by_user", typeof(System.String));
            dtSource_dtl_for_display.Columns.Add("updated_dttm", typeof(System.String));
        }
        private void AddPrimaryKeys_dtl()
        {
            dtSource_dtl.TableName = "payrollemployeemaster_ref_tbl";
            dtSource_dtl.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "account_code", "account_sub_code"};
            dtSource_dtl = MyCmn.AddPrimaryKeys(dtSource_dtl, col);
        }
        private void AddPrimaryKeys_dtl_for_display()
        {
            dtSource_dtl_for_display.TableName = "payrollemployeemaster_ref_tbl";
            dtSource_dtl_for_display.Columns.Add("action", typeof(System.Int32));
            dtSource_dtl_for_display.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "empl_id", "account_code", "account_sub_code" };
            dtSource_dtl_for_display = MyCmn.AddPrimaryKeys(dtSource_dtl_for_display, col);
        }
        private void AddNewRow_dtl()
        {
            DataRow nrow = dtSource_dtl.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["account_code"] = string.Empty;
            nrow["account_sub_code"] = string.Empty;
            nrow["account_id_nbr_ref"] = string.Empty;
            nrow["created_by_user"] = string.Empty;
            nrow["created_dttm"] = string.Empty;
            nrow["updated_by_user"] = string.Empty;
            nrow["updated_dttm"] = string.Empty;
            
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            //dtSource_dtl.Rows.Add(nrow);
        }
        private void AddNewRow_dtl_for_display()
        {
            DataRow nrow = dtSource_dtl_for_display.NewRow();
            nrow["empl_id"] = string.Empty;
            nrow["account_code"] = string.Empty;
            nrow["account_sub_code"] = string.Empty;
            nrow["account_title"] = string.Empty;
            nrow["account_id_nbr_ref"] = string.Empty;
            nrow["created_by_user"] = string.Empty;
            nrow["created_dttm"] = string.Empty;
            nrow["updated_by_user"] = string.Empty;
            nrow["updated_dttm"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;
            //dtSource_dtl_for_display.Rows.Add(nrow);
        }
        //***************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Trim().Split(new char[] { ',' });
            string pempl_id = commandArgs[0].ToString().Trim();
            string paccount_code = commandArgs[1].ToString().Trim();
            string paccount_sub_code = commandArgs[2].ToString().Trim();
            string peffectivity_date = commandArgs[3].ToString().Trim();

            deleteRec1.Text = "Are you sure to delete this: Account Code = (" + paccount_code.Trim() + ") ?";
            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string deleteExpression = "AND empl_id = '" + svalues[0].Trim() + "' AND account_code = '" + svalues[1].Trim() + "' AND account_sub_code = '" + svalues[2].Trim() + "' AND effectivity_date = '" + svalues[3].Trim() + "'";

            MyCmn.DeleteBackEndData("payrollemployeemaster_ref_tbl", "WHERE " + deleteExpression);

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
            string[] svalues = e.CommandArgument.ToString().Split(new char[] { ',' });

            string editExpression = "empl_id = '" + svalues[0].Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();

            txtb_empl_id.Text = row2Edit[0]["empl_id"].ToString();
            txtb_empl_name.Text = row2Edit[0]["employee_name"].ToString();
            RetrieveDataListGrid_dtl();
            RetriveATM_PACCO();
            Retain_ROW_ATM();

            LabelAddEdit.Text = "Edit Record: " + txtb_empl_id.Text.ToString().Trim();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            ViewState["AddEdit_Mode"] = MyCmn.CONST_EDIT;

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

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    //dtSource_dtl.TableName = "payrollemployeemaster_ref_tbl";
                    //dtSource_dtl.Columns.Add("action", typeof(System.Int32));
                    //dtSource_dtl.Columns.Add("retrieve", typeof(System.Boolean));

                    //dtSource_dtl.Columns.RemoveAt(0);

                    //for (int x = 0; x < dtSource_dtl.Rows.Count; x++)
                    //{
                    //    dtSource_dtl.Rows[x]["action"] = 1;
                    //    dtSource_dtl.Rows[x]["retrieve"] = false;
                    //}

                    // FindControl 
                    InitializeTable_dtl();
                    AddPrimaryKeys_dtl();
                    foreach (DataRow nrow in dtSource_dtl_for_display.Rows)
                    {
                        DataRow nrow1               = dtSource_dtl.NewRow();
                        nrow1["empl_id"]            = txtb_empl_id.Text.ToString().Trim();
                        nrow1["account_code"]       = nrow["account_code"];
                        nrow1["account_sub_code"]   = nrow["account_sub_code"];
                        nrow1["updated_dttm"]       = nrow["updated_dttm"];
                        nrow1["updated_by_user"]    = nrow["updated_by_user"];
                        nrow1["created_dttm"]       = nrow["created_dttm"];
                        nrow1["created_by_user"]    = nrow["created_by_user"];
                        nrow1["action"] = 1;
                        nrow1["retrieve"] = false;

                        dtSource_dtl.Rows.Add(nrow1);
                    }


                    foreach (GridViewRow row in gv_details.Rows)
                    {
                        TextBox txtb_ref_id = row.FindControl("txtb_ref_id") as TextBox;
                        dtSource_dtl.Rows[row.RowIndex]["account_id_nbr_ref"] = txtb_ref_id.Text.ToString().Trim();

                        // Trigger that Only TextBox inputted will be saved!
                        if (txtb_ref_id.Text == "")
                        {
                            dtSource_dtl.Rows[row.RowIndex]["action"] = 0;
                        }
                        else
                        {
                            dtSource_dtl.Rows[row.RowIndex]["account_id_nbr_ref"] = txtb_ref_id.Text.ToString().Trim();

                            //*******************************************************************************************************
                            //  BEGIN - VJA- 2020-12-01 - Update Field created_dttm, update_dttm, created_by_user and updated_by_user
                            //*******************************************************************************************************
                            
                            // Check the Reference Number if not Equal to the existing value - then Update the column updated_dttm and updated_by_user
                            if (dtSource_dtl.Rows[row.RowIndex]["account_id_nbr_ref"].ToString().Trim() != 
                                dtSource_dtl_for_loop.Rows[row.RowIndex]["account_id_nbr_ref"].ToString().Trim())
                            {
                                dtSource_dtl.Rows[row.RowIndex]["updated_dttm"]    = DateTime.Now;
                                dtSource_dtl.Rows[row.RowIndex]["updated_by_user"] = Session["ep_user_id"].ToString();
                            }
                            // Check the Reference Number if empty or no value - then Update the column created_dttm and created_by_user
                            if (dtSource_dtl.Rows[row.RowIndex]["account_id_nbr_ref"].ToString().Trim() != "" &&
                                dtSource_dtl_for_loop.Rows[row.RowIndex]["account_id_nbr_ref"].ToString().Trim() == "")
                            {
                                dtSource_dtl.Rows[row.RowIndex]["created_dttm"]    = DateTime.Now;
                                dtSource_dtl.Rows[row.RowIndex]["created_by_user"] = Session["ep_user_id"].ToString();
                            }
                            //*******************************************************************************************************
                            //  END - VJA- 2020-12-01 - Update Field created_dttm, update_dttm, created_by_user and updated_by_user
                            //*******************************************************************************************************
                        }
                    }



                    //*******************************************************************************************
                    //  BEGIN - VJA- 2020-12-05 - Retain Row for ATM Number and Reference if PACCO Department
                    //*******************************************************************************************
                    if (Session["ep_department_code"].ToString().Trim() == "07") // PACCO 
                    {
                        DataRow nrow1               = dtSource_dtl.NewRow();
                        nrow1["empl_id"]            = txtb_empl_id.Text.ToString().Trim();
                        nrow1["account_code"]       = dtSource_dtl_ATM_nbr.Rows[0]["account_code"];
                        nrow1["account_sub_code"]   = dtSource_dtl_ATM_nbr.Rows[0]["account_sub_code"];
                        nrow1["account_id_nbr_ref"] = dtSource_dtl_ATM_nbr.Rows[0]["account_id_nbr_ref"];
                        nrow1["updated_dttm"]       = dtSource_dtl_ATM_nbr.Rows[0]["updated_dttm"];
                        nrow1["updated_by_user"]    = dtSource_dtl_ATM_nbr.Rows[0]["updated_by_user"];
                        nrow1["created_dttm"]       = dtSource_dtl_ATM_nbr.Rows[0]["created_dttm"];
                        nrow1["created_by_user"]    = dtSource_dtl_ATM_nbr.Rows[0]["created_by_user"];
                        nrow1["action"] = 1;
                        nrow1["retrieve"] = false;
                        dtSource_dtl.Rows.Add(nrow1);
                    }
                    //*******************************************************************************************
                    //  END   - VJA- 2020-12-05 - Retain Row for ATM Number and Reference if PACCO Department
                    //*******************************************************************************************

                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();

                        nrow["empl_id"]         = txtb_empl_id.Text.ToString().Trim();
                        nrow["employee_name"]   = txtb_empl_name.Text.ToString().Trim();
                        
                        dataListGrid.Rows.Add(nrow);

                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["empl_id"]  = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employee_name"] = txtb_empl_name.Text.ToString().Trim();

                        MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }

                    string[] insert_empl_script = MyCmn.get_insertscript(dtSource_dtl).Split(';');
                    MyCmn.DeleteBackEndData(dtSource_dtl.TableName.ToString(), "WHERE empl_id ='" + txtb_empl_id.Text.ToString().Trim() + "'");
                    for (int x = 0; x < insert_empl_script.Length; x++)
                    {
                        string insert_script = "";
                        insert_script = insert_empl_script[x];
                        MyCmn.insertdata(insert_script);
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop3", "closeModal();", true);
                }
                SearchData(txtb_search.Text.ToString().Trim());
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
            SearchData(txtb_search.Text.ToString().Trim());
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
            FieldValidationColorChanged(false, "ALL");
            try
            {
                if (txtb_empl_id.Text == "")
                {
                    FieldValidationColorChanged(true, "ddl_empl_name");
                    validatedSaved = false;
                }
                else if (dtSource_dtl_for_display.Rows.Count <= 0)
                {
                    FieldValidationColorChanged(true, "atleast-one");
                    validatedSaved = false;
                }
            }
            catch (Exception)
            {
                
                return false;
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

                    case "atleast-one":
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop4", "openNotification();", true);
                            lbl_notification.Text = "This Record cannot be saved";
                            break;
                        }
                        
                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "ALL":
                        {
                            LblRequired12.Text = "";
                            break;
                        }

                }
            }
        }
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_lastname_starts.SelectedValue.ToString() != "" && ddl_empl_type.SelectedValue != "")
            {
                RetrieveDataListGrid();
            }
        }
        //**********************************************************************************
        //  BEGIN - VJA- 2020-12-01 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void SearchData(string search)
        {
            string searchExpression = "empl_id LIKE '%" + search.Trim().Replace("'", "''") + "%' OR employee_name LIKE '%" + search.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));

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
        //**********************************************************************************
        //  BEGIN - VJA- 2020-12-05 - Retain Row for ATM Number and Reference if PACCO 
        //*********************************************************************************
        private void Retain_ROW_ATM()
        {
            string retain_row = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND account_code = '" + "10305030" + "' AND account_sub_code = '" + "" + "'";
            DataRow[] toInsert = dtSource_dtl_ATM_nbr.Select(retain_row);
        }
        //**********************************************************************************
        //  BEGIN - VJA- 2020-12-05 - DO not display the account code for ATM if PACCO department
        //*********************************************************************************
        private void RetriveATM_PACCO()
        {
            if (Session["ep_department_code"].ToString().Trim() == "07") // PACCO 
            {
                string deleteExpression = "account_code = '" + "10305030" + "' AND account_sub_code = '" + "" + "'";
                DataRow[] row2Delete = dtSource_dtl_for_display.Select(deleteExpression);
                dtSource_dtl_for_display.Rows.Remove(row2Delete[0]);
                dtSource_dtl_for_display.AcceptChanges();
                MyCmn.Sort(gv_details, dtSource_dtl_for_display, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            }
        }
    }
}