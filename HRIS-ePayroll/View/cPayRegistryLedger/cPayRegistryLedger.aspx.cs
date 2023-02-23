//**********************************************************************************
// PROJECT NAME     :   HRIS - ePayroll
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for Position Page
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// Vincent Jade Alivio     02/02/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View.cPayRegistryLedger
{
    public partial class cPayRegistryLedger : System.Web.UI.Page
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
        DataTable dtforRelatedTemplate
        {
            get
            {
                if ((DataTable)ViewState["dtforRelatedTemplate"] == null) return null;
                return (DataTable)ViewState["dtforRelatedTemplate"];
            }
            set
            {
                ViewState["dtforRelatedTemplate"] = value;
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
                    Session["SortField"] = "payrolltemplate_code";
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
        //  BEGIN - JADE- 01/18/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            RetrieveEmploymentType();
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPayRegistryLedger"] = "cPayRegistryLedger";
            RetrieveDataListGrid();

            RetrieveRelatedTemplate();
            RetrieveRelatedTemplate1();
            RetrieveYear();

            btn_add.Visible = false;
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
        private void RetrieveRelatedTemplate()
        {
            ddl_payroll_type.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list1" , "par_employment_type" ,ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payroll_type.DataSource = dt;
            ddl_payroll_type.DataValueField = "payrolltemplate_code";
            ddl_payroll_type.DataTextField = "payrolltemplate_descr";
            ddl_payroll_type.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_type.Items.Insert(0, li);
        }
        private void RetrieveRelatedTemplate1()
        {
            ddl_related_no1.Items.Clear();
            ddl_related_no1.DataSource = dtforRelatedTemplate;
            ddl_related_no1.DataValueField = "payrolltemplate_code";
            ddl_related_no1.DataTextField = "payrolltemplate_descr";
            ddl_related_no1.DataBind();
            ListItem li1 = new ListItem("-- Select Here --", "");
            ddl_related_no1.Items.Insert(0, li1);

            ddl_related_no2.Items.Clear();
            ddl_related_no2.DataSource = dtforRelatedTemplate;
            ddl_related_no2.DataValueField = "payrolltemplate_code";
            ddl_related_no2.DataTextField = "payrolltemplate_descr";
            ddl_related_no2.DataBind();
            ListItem li2 = new ListItem("-- Select Here --", "");
            ddl_related_no2.Items.Insert(0, li2);

            ddl_related_no3.Items.Clear();
            ddl_related_no3.DataSource = dtforRelatedTemplate;
            ddl_related_no3.DataValueField = "payrolltemplate_code";
            ddl_related_no3.DataTextField = "payrolltemplate_descr";
            ddl_related_no3.DataBind();
            ListItem li3 = new ListItem("-- Select Here --", "");
            ddl_related_no3.Items.Insert(0, li3);

            ddl_related_no4.Items.Clear();
            ddl_related_no4.DataSource = dtforRelatedTemplate;
            ddl_related_no4.DataValueField = "payrolltemplate_code";
            ddl_related_no4.DataTextField = "payrolltemplate_descr";
            ddl_related_no4.DataBind();
            ListItem li4 = new ListItem("-- Select Here --", "");
            ddl_related_no4.Items.Insert(0, li4);

            ddl_related_no5.Items.Clear();
            ddl_related_no5.DataSource = dtforRelatedTemplate;
            ddl_related_no5.DataValueField = "payrolltemplate_code";
            ddl_related_no5.DataTextField = "payrolltemplate_descr";
            ddl_related_no5.DataBind();
            ListItem li5 = new ListItem("-- Select Here --", "");
            ddl_related_no5.Items.Insert(0, li5);

            ddl_related_no6.Items.Clear();
            ddl_related_no6.DataSource = dtforRelatedTemplate;
            ddl_related_no6.DataValueField = "payrolltemplate_code";
            ddl_related_no6.DataTextField = "payrolltemplate_descr";
            ddl_related_no6.DataBind();
            ListItem li6 = new ListItem("-- Select Here --", "");
            ddl_related_no6.Items.Insert(0, li6);

            ddl_related_no7.Items.Clear();
            ddl_related_no7.DataSource = dtforRelatedTemplate;
            ddl_related_no7.DataValueField = "payrolltemplate_code";
            ddl_related_no7.DataTextField = "payrolltemplate_descr";
            ddl_related_no7.DataBind();
            ListItem li7 = new ListItem("-- Select Here --", "");
            ddl_related_no7.Items.Insert(0, li7);

            ddl_related_no8.Items.Clear();
            ddl_related_no8.DataSource = dtforRelatedTemplate;
            ddl_related_no8.DataValueField = "payrolltemplate_code";
            ddl_related_no8.DataTextField = "payrolltemplate_descr";
            ddl_related_no8.DataBind();
            ListItem li8 = new ListItem("-- Select Here --", "");
            ddl_related_no8.Items.Insert(0, li8);

            ddl_related_no9.Items.Clear();
            ddl_related_no9.DataSource = dtforRelatedTemplate;
            ddl_related_no9.DataValueField = "payrolltemplate_code";
            ddl_related_no9.DataTextField = "payrolltemplate_descr";
            ddl_related_no9.DataBind();
            ListItem li9 = new ListItem("-- Select Here --", "");
            ddl_related_no9.Items.Insert(0, li9);

            ddl_related_no10.Items.Clear();
            ddl_related_no10.DataSource = dtforRelatedTemplate;
            ddl_related_no10.DataValueField = "payrolltemplate_code";
            ddl_related_no10.DataTextField = "payrolltemplate_descr";
            ddl_related_no10.DataBind();
            ListItem li10 = new ListItem("-- Select Here --", "");
            ddl_related_no10.Items.Insert(0, li10);
        }
        //*************************************************************************
        //  JADE - Jade- 09/28/2018 - Get the Last Control Number
        //*************************************************************************
        //private void RetrieveLastRow()
        //{
        //    string sql1 = "SELECT TOP 1 payrolltemplate_code from payrollregistryledger_tbl where LEFT(payrolltemplate_code,3)=LEFT(payrolltemplate_code,3) order by payrolltemplate_code DESC";
        //    string last_row = MyCmn.GetLastRow_of_Table(sql1);
        //    if (last_row.Trim() == "")
        //    {
        //        last_row = "0";
        //    }
        //    int last_row1 = int.Parse(last_row) + 1;
        //    txtb_code.Text = last_row1.ToString().PadLeft(3, '0');
        //}
        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            dtforRelatedTemplate = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list2", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            dataListGrid = MyCmn.RetrieveData("sp_payrollregistryledger_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim() , "par_employment_type",ddl_empl_type.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
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

            RetrieveRelatedTemplate1();

            txtb_empl_type_disabled.Text = ddl_empl_type.SelectedItem.ToString().Trim();
            txtb_payroll_year_disabled.Text = ddl_year.SelectedItem.ToString().Trim();

            ddl_payroll_type.Enabled = true;
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
            txtb_control_seq_no.Text = "";
            txtb_max_control_seq_no.Text = "";
            
            
            ddl_payroll_type.SelectedValue = "";
            //ddl_related_no1.SelectedValue = "";
            //ddl_related_no2.SelectedValue = "";
            //ddl_related_no3.SelectedValue = "";
            //ddl_related_no4.SelectedValue = "";
            //ddl_related_no5.SelectedValue = "";
            //ddl_related_no6.SelectedValue = "";
            //ddl_related_no7.SelectedValue = "";
            //ddl_related_no8.SelectedValue = "";
            //ddl_related_no9.SelectedValue = "";
            //ddl_related_no10.SelectedValue = "";
            
            ddl_payroll_type.Focus();
        }
        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {
            dtSource = new DataTable();
            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("employment_type", typeof(System.String));
            dtSource.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource.Columns.Add("control_seq_no", typeof(System.String));
            dtSource.Columns.Add("max_control_seq_no", typeof(System.String));
            dtSource.Columns.Add("related_template_no1", typeof(System.String));
            dtSource.Columns.Add("related_template_no2", typeof(System.String));
            dtSource.Columns.Add("related_template_no3", typeof(System.String));
            dtSource.Columns.Add("related_template_no4", typeof(System.String));
            dtSource.Columns.Add("related_template_no5", typeof(System.String));
            dtSource.Columns.Add("related_template_no6", typeof(System.String));
            dtSource.Columns.Add("related_template_no7", typeof(System.String));
            dtSource.Columns.Add("related_template_no8", typeof(System.String));
            dtSource.Columns.Add("related_template_no9", typeof(System.String));
            dtSource.Columns.Add("related_template_no10", typeof(System.String));
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollregistryledger_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year" , "employment_type", "payrolltemplate_code" };
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }
        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["employment_type"] = string.Empty;
            nrow["payrolltemplate_code"] = string.Empty;
            nrow["control_seq_no"] = string.Empty;
            nrow["max_control_seq_no"] = string.Empty;
            nrow["related_template_no1"] = string.Empty;
            nrow["related_template_no2"] = string.Empty;
            nrow["related_template_no3"] = string.Empty;
            nrow["related_template_no4"] = string.Empty;
            nrow["related_template_no5"] = string.Empty;
            nrow["related_template_no6"] = string.Empty;
            nrow["related_template_no7"] = string.Empty;
            nrow["related_template_no8"] = string.Empty;
            nrow["related_template_no9"] = string.Empty;
            nrow["related_template_no10"] = string.Empty;
            nrow["action"] = 1;
            nrow["retrieve"] = false;
            dtSource.Rows.Add(nrow);
        }
        //private void GetMaxlenght()
        //{
        //    int seq = int.Parse(txtb_control_seq_no.Text.ToString());
        //    int max_seq = int.Parse(txtb_max_control_seq_no.Text.ToString());

        //    string nextCode = seq.ToString();
        //    nextCode = nextCode.PadLeft(max_seq, '0');

        //    lbl_control_seq_no_hidden.Text = nextCode ;
        //}
        //***************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string payroll_code = commandArgs[0];
            string payroll_year = commandArgs[1];
            string employment_type = commandArgs[2];

            deleteRec1.Text = "Are you sure to delete this Record ?";
            lnkBtnYes.CommandArgument = payroll_code +  "," + payroll_year + "," + employment_type;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandArgs1 = e.CommandArgument.ToString().Split(new char[] { ',' }); ;
            string deleteExpression = "payrolltemplate_code = '" + commandArgs1[0].ToString().Trim() + "' AND payroll_year = '" + commandArgs1[1].ToString().Trim() + "' AND employment_type = '" + commandArgs1[2].ToString().Trim() + "'";

            MyCmn.DeleteBackEndData("payrollregistryledger_tbl", "WHERE " + deleteExpression);

            DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
            dataListGrid.Rows.Remove(row2Delete[0]);
            dataListGrid.AcceptChanges();
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

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
            RetrieveRelatedTemplate1();


            DataRow nrow = dtSource.NewRow();
            nrow["payroll_year"] = string.Empty;
            nrow["employment_type"] = string.Empty;
            nrow["payrolltemplate_code"] = string.Empty;
            nrow["action"] = 2;
            nrow["retrieve"] = true;
            dtSource.Rows.Add(nrow);
            


            ddl_payroll_type.SelectedValue = svalues;
            txtb_control_seq_no.Text = row2Edit[0]["control_seq_no"].ToString();
            txtb_max_control_seq_no.Text = row2Edit[0]["max_control_seq_no"].ToString();
            
            ddl_related_no1.SelectedValue  =  row2Edit[0]["related_template_no1"].ToString();
            ddl_related_no2.SelectedValue  =  row2Edit[0]["related_template_no2"].ToString();
            ddl_related_no3.SelectedValue  =  row2Edit[0]["related_template_no3"].ToString();
            ddl_related_no4.SelectedValue  =  row2Edit[0]["related_template_no4"].ToString();
            ddl_related_no5.SelectedValue  =  row2Edit[0]["related_template_no5"].ToString();
            ddl_related_no6.SelectedValue  =  row2Edit[0]["related_template_no6"].ToString();
            ddl_related_no7.SelectedValue  =  row2Edit[0]["related_template_no7"].ToString();
            ddl_related_no8.SelectedValue  =  row2Edit[0]["related_template_no8"].ToString();
            ddl_related_no9.SelectedValue  =  row2Edit[0]["related_template_no9"].ToString();
            ddl_related_no10.SelectedValue =  row2Edit[0]["related_template_no10"].ToString();

            txtb_empl_type_disabled.Text = ddl_empl_type.SelectedItem.ToString().Trim();
            txtb_payroll_year_disabled.Text = ddl_year.SelectedItem.ToString().Trim();

            ddl_payroll_type.Enabled = false;
            LabelAddEdit.Text = "Edit Record: " + ddl_payroll_type.SelectedItem.ToString().Trim();
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
                    dtSource.Rows[0]["payroll_year"]              = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["employment_type"]           = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payrolltemplate_code"]      = ddl_payroll_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["control_seq_no"]            = txtb_control_seq_no.Text.ToString().Trim();
                    dtSource.Rows[0]["max_control_seq_no"]        = txtb_max_control_seq_no.Text.ToString().Trim();
                    dtSource.Rows[0]["related_template_no1"]      = ddl_related_no1.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no2"]      = ddl_related_no2.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no3"]      = ddl_related_no3.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no4"]      = ddl_related_no4.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no5"]      = ddl_related_no5.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no6"]      = ddl_related_no6.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no7"]      = ddl_related_no7.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no8"]      = ddl_related_no8.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no9"]      = ddl_related_no9.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no10"]     = ddl_related_no10.SelectedValue.ToString().Trim();

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"]              = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["employment_type"]           = ddl_empl_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payrolltemplate_code"]      = ddl_payroll_type.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["control_seq_no"]            = txtb_control_seq_no.Text.ToString().Trim();
                    dtSource.Rows[0]["max_control_seq_no"]        = txtb_max_control_seq_no.Text.ToString().Trim();
                    dtSource.Rows[0]["related_template_no1"]      = ddl_related_no1.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no2"]      = ddl_related_no2.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no3"]      = ddl_related_no3.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no4"]      = ddl_related_no4.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no5"]      = ddl_related_no5.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no6"]      = ddl_related_no6.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no7"]      = ddl_related_no7.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no8"]      = ddl_related_no8.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no9"]      = ddl_related_no9.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["related_template_no10"]     = ddl_related_no10.SelectedValue.ToString().Trim();

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    //
                    //GetMaxlenght();
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

                        nrow["payrolltemplate_code"]   = ddl_payroll_type.SelectedValue.ToString().Trim();
                        //nrow["control_seq_no"]         = txtb_control_seq_no.Text.ToString().Trim();
                        //nrow["max_control_seq_no"]     = txtb_max_control_seq_no.Text.ToString().Trim();
                        nrow["related_template_no1"]   = ddl_related_no1.SelectedValue.ToString().Trim();
                        nrow["related_template_no2"]   = ddl_related_no2.SelectedValue.ToString().Trim();
                        nrow["related_template_no3"]   = ddl_related_no3.SelectedValue.ToString().Trim();
                        nrow["related_template_no4"]   = ddl_related_no4.SelectedValue.ToString().Trim();
                        nrow["related_template_no5"]   = ddl_related_no5.SelectedValue.ToString().Trim();
                        nrow["related_template_no6"]   = ddl_related_no6.SelectedValue.ToString().Trim();
                        nrow["related_template_no7"]   = ddl_related_no7.SelectedValue.ToString().Trim();
                        nrow["related_template_no8"]   = ddl_related_no8.SelectedValue.ToString().Trim();
                        nrow["related_template_no9"]   = ddl_related_no9.SelectedValue.ToString().Trim();
                        nrow["related_template_no10"]  = ddl_related_no10.SelectedValue.ToString().Trim();

                        nrow["payrolltemplate_descr"]     = ddl_payroll_type.SelectedItem.ToString().Trim();
                        
                        dataListGrid.Rows.Add(nrow);
                        gv_dataListGrid.SetPageIndex(gv_dataListGrid.PageCount);
                        //gv_dataListGrid.SelectRow(gv_dataListGrid.Rows.Count - 1);

                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }
                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "payrolltemplate_code = '" + ddl_payroll_type.SelectedValue.ToString().Trim() + "' AND payroll_year = '" + ddl_year.SelectedValue.ToString().Trim() + "' AND employment_type = '" + ddl_empl_type.SelectedValue.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);
                        
                        //row2Edit[0]["control_seq_no"]         = txtb_control_seq_no.Text.ToString().Trim();
                        //row2Edit[0]["max_control_seq_no"]     = txtb_max_control_seq_no.Text.ToString().Trim();
                        row2Edit[0]["related_template_no1"]   = ddl_related_no1.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no2"]   = ddl_related_no2.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no3"]   = ddl_related_no3.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no4"]   = ddl_related_no4.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no5"]   = ddl_related_no5.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no6"]   = ddl_related_no6.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no7"]   = ddl_related_no7.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no8"]   = ddl_related_no8.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no9"]   = ddl_related_no9.SelectedValue.ToString().Trim();
                        row2Edit[0]["related_template_no10"]  = ddl_related_no10.SelectedValue.ToString().Trim();

                        row2Edit[0]["payrolltemplate_descr"]     = ddl_payroll_type.SelectedItem.ToString().Trim();

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
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JADE- 09/12/2018 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "control_seq_no LIKE '%" 
                + txtb_search.Text.Trim().Replace("'","''") 
                + "%' OR payrolltemplate_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("employment_type", typeof(System.String));
            dtSource1.Columns.Add("payrolltemplate_code", typeof(System.String));
            dtSource1.Columns.Add("control_seq_no", typeof(System.String));
            dtSource1.Columns.Add("max_control_seq_no", typeof(System.String));
            dtSource1.Columns.Add("related_template_no1", typeof(System.String));
            dtSource1.Columns.Add("related_template_no2", typeof(System.String));
            dtSource1.Columns.Add("related_template_no3", typeof(System.String));
            dtSource1.Columns.Add("related_template_no4", typeof(System.String));
            dtSource1.Columns.Add("related_template_no5", typeof(System.String));
            dtSource1.Columns.Add("related_template_no6", typeof(System.String));
            dtSource1.Columns.Add("related_template_no7", typeof(System.String));
            dtSource1.Columns.Add("related_template_no8", typeof(System.String));
            dtSource1.Columns.Add("related_template_no9", typeof(System.String));
            dtSource1.Columns.Add("related_template_no10", typeof(System.String));
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
        //**************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Objects data Validation
        //*************************************************************************
        private bool IsDataValidated()
        {
            // Check if the dropdown is Duplicate
            int error = 0;
            for (int x = 1; x <= 10; x++)
            {
                for (int y = 1; y <= 10; y++)
                {
                    DropDownList ddl_x = ddl_containers.FindControl("ddl_related_no" + x) as DropDownList;
                    DropDownList ddl_y = ddl_containers.FindControl("ddl_related_no" + y) as DropDownList;

                    if (x != y)
                    {
                        if(ddl_x.SelectedValue == ddl_y.SelectedValue && (ddl_x.SelectedValue.ToString().Trim() != "" && ddl_y.SelectedValue.ToString().Trim() != ""))
                        {
                            FieldValidationColorChanged(true, "ddl_related_no"+x);
                            FieldValidationColorChanged(true, "required-all");
                            error = error + 1;
                        }
                    }
                }
            }
            if (error > 0)
            {
                return false;
            }

            bool validatedSaved = true;
            if(ddl_payroll_type.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "ddl_payroll_type");
                ddl_payroll_type.Focus();
                validatedSaved = false;
            }
            //else if (txtb_control_seq_no.Text == "")
            //{
            //    FieldValidationColorChanged(true, "txtb_control_seq_no");
            //    txtb_control_seq_no.Focus();
            //    validatedSaved = false;
            //}
            //else if (CommonCode.checkisdecimal(txtb_control_seq_no) == false)
            //{
            //    FieldValidationColorChanged(true, "invalid-seq-no");
            //    txtb_control_seq_no.Focus();
            //    validatedSaved = false;
            //}
            //else if (txtb_max_control_seq_no.Text == "")
            //{
            //    FieldValidationColorChanged(true, "txtb_max_control_seq_no");
            //    txtb_max_control_seq_no.Focus();
            //    validatedSaved = false;
            //}
            //else if (CommonCode.checkisdecimal(txtb_max_control_seq_no) == false)
            //{
            //    FieldValidationColorChanged(true, "invalid-max-seq-no");
            //    txtb_max_control_seq_no.Focus();
            //    validatedSaved = false;
            //}
            else if (ddl_related_no1.SelectedValue == "")
            {
                FieldValidationColorChanged(true, "select-atleast-one");
                ddl_related_no1.Focus();
                validatedSaved = false;
            }
            return validatedSaved;
        }
        //**********************************************************************************************
        //  BEGIN - JADE- 01/18/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_payroll_type":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_payroll_type.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text = "already exist";
                            ddl_payroll_type.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_control_seq_no":
                        {
                            LblRequired2.Text = MyCmn.CONST_RQDFLD;
                            txtb_control_seq_no.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-seq-no":
                        {
                            LblRequired2.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_control_seq_no.BorderColor = Color.Red;
                            break;
                        }
                    case "txtb_max_control_seq_no":
                        {
                            LblRequired3.Text = MyCmn.CONST_RQDFLD;
                            txtb_max_control_seq_no.BorderColor = Color.Red;
                            break;
                        }
                    case "invalid-max-seq-no":
                        {
                            LblRequired3.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_max_control_seq_no.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no1":
                        {
                            //LblRequired4.Text = "select atleast one!";
                            ddl_related_no1.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no2":
                        {
                           // LblRequired5.Text = "select atleast one!";
                            ddl_related_no2.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no3":
                        {
                            //LblRequired6.Text = "select atleast one!";
                            ddl_related_no3.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no4":
                        {
                            //LblRequired7.Text = "select atleast one!";
                            ddl_related_no4.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no5":
                        {
                            //LblRequired8.Text = "select atleast one!";
                            ddl_related_no5.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no6":
                        {
                            //LblRequired9.Text = "select atleast one!";
                            ddl_related_no6.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no7":
                        {
                            //LblRequired10.Text = "select atleast one!";
                            ddl_related_no7.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no8":
                        {
                            //LblRequired11.Text = "select atleast one!";
                            ddl_related_no8.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no9":
                        {
                            //LblRequired12.Text = "select atleast one!";
                            ddl_related_no9.BorderColor = Color.Red;
                            break;
                        }
                    case "ddl_related_no10":
                        {
                            //LblRequired13.Text = "select atleast one!";
                            ddl_related_no10.BorderColor = Color.Red;
                            break;
                        }
                    case "select-atleast-one":
                        { 
                            LblRequired4.Text = MyCmn.CONST_RQDFLD;
                            ddl_related_no1.BorderColor = Color.Red;
                            break;
                        }
                    case "required-all":
                        {
                            LblRequired14.Text = "duplicate related template!";
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
                            LblRequired5.Text = "";
                            LblRequired6.Text = "";
                            LblRequired7.Text = "";
                            LblRequired8.Text = "";
                            LblRequired9.Text = "";
                            LblRequired10.Text = "";
                            LblRequired11.Text = "";
                            LblRequired12.Text = "";
                            LblRequired13.Text = "";
                            LblRequired14.Text = "";
                            ddl_payroll_type.BorderColor = Color.LightGray;
                            txtb_control_seq_no.BorderColor = Color.LightGray;
                            txtb_max_control_seq_no.BorderColor = Color.LightGray;
                            ddl_related_no1.BorderColor = Color.LightGray;
                            ddl_related_no2.BorderColor = Color.LightGray;
                            ddl_related_no3.BorderColor = Color.LightGray;
                            ddl_related_no4.BorderColor = Color.LightGray;
                            ddl_related_no5.BorderColor = Color.LightGray;
                            ddl_related_no6.BorderColor = Color.LightGray;
                            ddl_related_no7.BorderColor = Color.LightGray;
                            ddl_related_no8.BorderColor = Color.LightGray;
                            ddl_related_no9.BorderColor = Color.LightGray;
                            ddl_related_no10.BorderColor = Color.LightGray;
                            break;
                        }
                }
            }
        }
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddl_empl_type.SelectedValue != "")
            {
                RetrieveDataListGrid();
                RetrieveRelatedTemplate();
                btn_add.Visible = true;
            }
            else
            {
                btn_add.Visible = false;
            }
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_year.SelectedValue != "") 
            {
                RetrieveDataListGrid();
            }
        }
    }
}