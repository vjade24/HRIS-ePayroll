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

namespace HRIS_ePayroll.View.cTaxTable
{
    public partial class cTaxTable : System.Web.UI.Page
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
                    Session["SortField"] = "seq_no";
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
            RetrieveYear();
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPositionTable"] = "cPositionTable";
            RetrieveDataListGrid();
            //RetrieveDataAccount();

            Button1.Visible = false;
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveYear()
        {
            ddl_tax_year.Items.Clear();
            int years = Convert.ToInt32(DateTime.Now.Year);
            int prev_year = years - 5;
            for (int x = 0; x < 12; x++)
            {
                ListItem li2 = new ListItem(prev_year.ToString(), prev_year.ToString());
                ddl_tax_year.Items.Insert(x, li2);
                if (prev_year == years)
                {
                    ListItem li3 = new ListItem((years + 1).ToString(), (years + 1).ToString());
                    ddl_tax_year.Items.Insert(x + 1, li3);
                    ddl_tax_year.SelectedValue = years.ToString();
                    break;
                }
                prev_year = prev_year + 1;
            }
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrieveYear1()
        {
            ddl_tax_year_add.Items.Clear();
            int years = Convert.ToInt32(DateTime.Now.Year);
            int prev_year = years - 5;
            for (int x = 0; x < 12; x++)
            {
                ListItem li2 = new ListItem(prev_year.ToString(), prev_year.ToString());
                ddl_tax_year_add.Items.Insert(x, li2);
                if (prev_year == years)
                {
                    ListItem li3 = new ListItem((years + 1).ToString(), (years + 1).ToString());
                    ddl_tax_year_add.Items.Insert(x + 1, li3);
                    ddl_tax_year_add.SelectedValue = years.ToString();
                    break;
                }
                prev_year = prev_year + 1;
            }
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dataListGrid = MyCmn.RetrieveData("sp_tax_tbl_list", "par_tax_year", ddl_tax_year.SelectedValue.ToString().Trim());
            CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/09/2018 - Populate Combo list from Salary Grade Table
        //*************************************************************************
        //private void RetrieveDataAccount()
        //{
        //    ddl_empl_type.Items.Clear();
        //    DataTable dtEmpList = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");

        //    ddl_empl_type.DataSource = dtEmpList;
        //    ddl_empl_type.DataTextField = "employmenttype_description";
        //    ddl_empl_type.DataValueField = "employment_type";
        //    ddl_empl_type.DataBind();
        //    ListItem li = new ListItem("-- Select Here --", "");
        //    ddl_empl_type.Items.Insert(0, li);
        //}
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            RetrieveYear1();
            

            LabelAddEdit.Text = "Add New Record";
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD); ;
            FieldValidationColorChanged(false, "ALL");

            //Retrieve NExt Sub account
            RetrieveNextSeq();
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {
            txtb_seq.Text="";
            txtb_lowerlimit.Text ="";
            txtb_upperlimit.Text ="";
            txtb_taxlower.Text ="";
            txtb_taxupper.Text = "";

            txtb_seq.ReadOnly = false;
            txtb_seq.Focus();
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["seq_no"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);

            //int dtRowCont = dataListGrid.Rows.Count - 1;
            //string lastCode = "00";

            //if (dtRowCont > -1)
            //{
            //    DataRow lastRow = dataListGrid.Rows[dtRowCont];
            //    lastCode = lastRow["seq_no"].ToString();
            //}

            //int lastCodeInt = int.Parse(lastCode) + 1;
            //string nextCode = lastCodeInt.ToString();
            //nextCode = nextCode.PadLeft(2, '0');

            //txtb_seq.Text = nextCode;
        }
        //*************************************************************************
        //  JADE - Jade- 09/28/2018 - Get the Last Control Number
        //*************************************************************************
        private void RetrieveNextSeq()
        {
            string sql1 = "SELECT TOP 1 seq_no from taxtable_tbl where LEFT(seq_no,2)=LEFT(seq_no,2) order by seq_no DESC";
            string last_row = MyCmn.GetLastRow_of_Table(sql1);
            if (last_row.Trim() == "")
            {
                last_row = "0";
            }
            int last_row1 = int.Parse(last_row) + 1;
            txtb_seq.Text = last_row1.ToString().PadLeft(2, '0');
        }
        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("seq_no", typeof(System.String));
            dtSource.Columns.Add("lower_limit", typeof(System.String));
            dtSource.Columns.Add("upper_limit", typeof(System.String));
            dtSource.Columns.Add("tax_on_lower_limit", typeof(System.String));
            dtSource.Columns.Add("tax_on_excess", typeof(System.String));
            dtSource.Columns.Add("tax_year", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "tax_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "seq_no" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }

        //***************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string appttype = commandArgs[0];
            string appttypedescr = commandArgs[1];

            deleteRec1.Text = "Are you sure to delete this Sequence Number = (" + appttype.Trim() + ") - " + appttypedescr.Trim() + " ?";
            lnkBtnYes.CommandArgument = appttype;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string svalues = e.CommandArgument.ToString();
            string deleteExpression = "seq_no = '" + svalues + "'";

            MyCmn.DeleteBackEndData("tax_tbl", "WHERE " + deleteExpression);

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
            string svalues = e.CommandArgument.ToString();
            string editExpression = "seq_no = '" + svalues + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            RetrieveYear1();

            InitializeTable();
            AddPrimaryKeys();
            DataRow nrow = dtSource.NewRow();
            nrow["seq_no"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);
            txtb_seq.Text = svalues;
            ddl_tax_year_add.SelectedValue = row2Edit[0]["tax_year"].ToString();
            txtb_seq.Text                  = row2Edit[0]["seq_no"].ToString();
            txtb_lowerlimit.Text           = row2Edit[0]["lower_limit"].ToString();
            txtb_upperlimit.Text           = row2Edit[0]["upper_limit"].ToString();    
            txtb_taxlower.Text             = row2Edit[0]["tax_on_lower_limit"].ToString();
            txtb_taxupper.Text             = row2Edit[0]["tax_on_excess"].ToString().Trim('%', '%');

            //txtb_taxlower.Text = Convert.ToDecimal(txtb_taxlower.ToString()).ToString("#,##0.00");

            txtb_seq.ReadOnly = true;
            LabelAddEdit.Text = "Edit Record: " + txtb_upperlimit.Text.Trim();
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
                    dtSource.Rows[0]["seq_no"] = txtb_seq.Text.ToString();
                    dtSource.Rows[0]["tax_year"] = ddl_tax_year_add.SelectedValue.ToString();
                    dtSource.Rows[0]["lower_limit"] = Convert.ToDecimal(txtb_lowerlimit.Text.ToString());
                    dtSource.Rows[0]["upper_limit"] = Convert.ToDecimal(txtb_upperlimit.Text.ToString());
                    dtSource.Rows[0]["tax_on_lower_limit"] = Convert.ToDecimal(txtb_taxlower.Text.ToString());
                    dtSource.Rows[0]["tax_on_excess"] = Convert.ToDecimal(txtb_taxupper.Text.ToString());
                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["seq_no"] = txtb_seq.Text.ToString();
                    dtSource.Rows[0]["lower_limit"] = Convert.ToDecimal(txtb_lowerlimit.Text.ToString());
                    dtSource.Rows[0]["upper_limit"] = Convert.ToDecimal(txtb_upperlimit.Text.ToString());
                    dtSource.Rows[0]["tax_year"]    = ddl_tax_year_add.SelectedValue.ToString();
                    dtSource.Rows[0]["tax_on_lower_limit"] = Convert.ToDecimal(txtb_taxlower.Text.ToString());
                    dtSource.Rows[0]["tax_on_excess"] = Convert.ToDecimal(txtb_taxupper.Text.ToString());
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
                        nrow["seq_no"] = txtb_seq.Text.ToString();
                        nrow["tax_year"] = ddl_tax_year_add.SelectedValue.ToString();
                        nrow["lower_limit"] = txtb_lowerlimit.Text.ToString();
                        nrow["upper_limit"] = txtb_upperlimit.Text.ToString();
                        nrow["tax_on_lower_limit"] = txtb_taxlower.Text.ToString();
                        nrow["tax_on_excess"] = txtb_taxupper.Text.ToString() + '%';

                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "seq_no = '" + txtb_seq.Text.ToString() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        row2Edit[0]["seq_no"] = txtb_seq.Text.ToString();
                        row2Edit[0]["tax_year"] = ddl_tax_year_add.SelectedValue.ToString();
                        row2Edit[0]["lower_limit"] = txtb_lowerlimit.Text.ToString();
                        row2Edit[0]["upper_limit"] = txtb_upperlimit.Text.ToString();
                        row2Edit[0]["tax_on_lower_limit"] = txtb_taxlower.Text.ToString();
                        row2Edit[0]["tax_on_excess"] = txtb_taxupper.Text.ToString() + '%';
                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "closeModal();", true);
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
            string searchExpression = "seq_no LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR lower_limit LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR upper_limit LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR tax_on_lower_limit LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR tax_on_excess LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%' OR tax_year LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("seq_no", typeof(System.String));
            dtSource1.Columns.Add("lower_limit", typeof(System.String));
            dtSource1.Columns.Add("upper_limit", typeof(System.String));
            dtSource1.Columns.Add("tax_on_lower_limit", typeof(System.String));
            dtSource1.Columns.Add("tax_on_excess", typeof(System.String));
            dtSource1.Columns.Add("tax_year", typeof(System.String));

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
            FieldValidationColorChanged(false, "ALL");
         
                

               

                if (txtb_lowerlimit.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_lowerlimit");
                    txtb_lowerlimit.Focus();
                    validatedSaved = false;
                }
                else if (CommonCode.checkisdecimal(txtb_lowerlimit) == false)
                {
                    FieldValidationColorChanged(true, "invalid-numeric1");
                    txtb_lowerlimit.Focus();
                    validatedSaved = false;
                }
                else if (txtb_upperlimit.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_upperlimit");
                    txtb_upperlimit.Focus();
                    validatedSaved = false;
                }
                else if (CommonCode.checkisdecimal(txtb_upperlimit) == false)
                {
                    FieldValidationColorChanged(true, "invalid-numeric2");
                    txtb_upperlimit.Focus();
                    validatedSaved = false;
                }
                else if (txtb_taxlower.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_taxlower");
                    txtb_taxlower.Focus();
                    validatedSaved = false;
                }
                else if (CommonCode.checkisdecimal(txtb_taxlower) == false)
                {
                    FieldValidationColorChanged(true, "invalid-numeric3");
                    txtb_taxlower.Focus();
                    validatedSaved = false;
                }
                else if (txtb_taxupper.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_taxupper");
                    txtb_taxupper.Focus();
                    validatedSaved = false;
                }
                else if (CommonCode.checkisdecimal(txtb_taxupper) == false)
                {
                    FieldValidationColorChanged(true, "invalid-numeric4");
                    txtb_taxupper.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(false, "ALL");
                }
            
            
            return validatedSaved;
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

        //**********************************************************************************************
        //  BEGIN - AEC- 09/20/2018 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "txtb_lowerlimit":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            txtb_lowerlimit.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_upperlimit":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_upperlimit.BorderColor = Color.Red;
                            break;
                        }
                   
                   
                    case "txtb_taxlower":
                        {
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            txtb_taxlower.CssClass = "form-control form-control-sm required";
                            txtb_taxlower.Focus();
                            break;
                        }
                    case "txtb_taxupper":
                        {
                            LblRequired5.Text = MyCmn.CONST_RQDFLD;
                            txtb_taxupper.CssClass = "form-control form-control-sm required";
                            txtb_taxupper.Focus();
                            break;
                        }
                    case "invalid-numeric1":
                        {
                            LblRequired1.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_lowerlimit.CssClass = "form-control form-control-sm required";
                            txtb_lowerlimit.Focus();
                            break;
                        }
                    case "invalid-numeric2":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_upperlimit.CssClass = "form-control form-control-sm required";
                            txtb_upperlimit.Focus();
                            break;
                        }
                    case "invalid-numeric3":
                        {
                            LblRequired4.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_taxlower.CssClass = "form-control form-control-sm required";
                            txtb_taxlower.Focus();
                            break;
                        }
                    case "invalid-numeric4":
                        {
                            LblRequired5.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_taxupper.CssClass = "form-control form-control-sm required";
                            txtb_taxupper.Focus();
                            break;
                        }
                }
            else if (!pMode)
            {
                switch (pObjectName)
                {
                    case "txtb_lowerlimit":
                        {
                            LblRequired1.Text = "";
                            txtb_lowerlimit.BorderColor = Color.LightGray;
                            break;
                        }
                    case "txtb_upperlimit":
                        {
                            LblRequired2.Text = "";
                            txtb_upperlimit.BorderColor = Color.LightGray;
                            break;
                        }
                    
                    case "txtb_taxlower":
                        {
                            LblRequired4.Text = "";
                            txtb_taxlower.CssClass = "form-control form-control-sm";
                            break;
                        }
                    case "txtb_taxupper":
                        {
                            LblRequired5.Text = "";
                            txtb_taxupper.CssClass = "form-control form-control-sm";
                            break;
                        }
                    case "ALL":
                        {
                            LblRequired1.Text = "";
                            LblRequired2.Text = "";
                            LblRequired3.Text = "";
                            LblRequired4.Text = "";
                            LblRequired5.Text = "";
                            txtb_lowerlimit.BorderColor = Color.LightGray;
                            txtb_upperlimit.BorderColor = Color.LightGray;
                            txtb_taxlower.BorderColor = Color.LightGray;
                            txtb_taxupper.BorderColor = Color.LightGray;
                            break;
                        }

                }
            }
        }

        protected void ddl_tax_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
            up_dataListGrid.Update();
            Button1.Visible = true;
            UpdatePanel10.Update();

        }
    }
}