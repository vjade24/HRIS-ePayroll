//**********************************************************************************
// PROJECT NAME     :   HRIS - eComval
// VERSION/RELEASE  :   HRIS Release #1
// PURPOSE          :   Code Behind for cPayRegistryOthPay
//**********************************************************************************
// REVISION HISTORY
//**********************************************************************************
// AUTHOR                    DATE            PURPOSE
//----------------------------------------------------------------------------------
// JORGE RUSTOM VILLANUEVA    05/24/2019      Code Creation
//**********************************************************************************
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using HRIS_Common;
using System.Drawing;

namespace HRIS_ePayroll.View
{
    public partial class cPayRegistryOthPay : System.Web.UI.Page
    {
        //********************************************************************
        //  BEGIN - JORGE- 03/20/2019 - Data Place holder creation 
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

        DataTable dtSource_fundcharges
        {
            get
            {
                if ((DataTable)ViewState["dtSource_fundcharges"] == null) return null;
                return (DataTable)ViewState["dtSource_fundcharges"];
            }
            set
            {
                ViewState["dtSource_fundcharges"] = value;
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

        DataTable dataList_employee
        {
            get
            {
                if ((DataTable)ViewState["dataList_employee"] == null) return null;
                return (DataTable)ViewState["dataList_employee"];
            }
            set
            {
                ViewState["dataList_employee"] = value;
            }
        }

        DataTable dataList_installation_tbl
        {
            get
            {
                if ((DataTable)ViewState["dataList_installation_tbl"] == null) return null;
                return (DataTable)ViewState["dataList_installation_tbl"];
            }
            set
            {
                ViewState["dataList_installation_tbl"] = value;
            }
        }

        DataTable dtGroup
        {
            get
            {
                if ((DataTable)ViewState["dtGroup"] == null) return null;
                return (DataTable)ViewState["dtGroup"];
            }
            set
            {
                ViewState["dtGroup"] = value;
            }
        }

        DataTable dtListField
        {
            get
            {
                if ((DataTable)ViewState["dtListField"] == null) return null;
                return (DataTable)ViewState["dtListField"];
            }
            set
            {
                ViewState["dtListField"] = value;
            }
        }



        //********************************************************************
        //  BEGIN - JORGE- 03/12/2019 - Public Variable used in Add/Edit Mode
        //********************************************************************

        CommonDB MyCmn = new CommonDB();

        //********************************************************************
        //  BEGIN - JORGE- 03/20/2019 - Page Load method
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

                ViewState["page_allow_add"]             = Session["page_allow_add_from_registry"];
                ViewState["page_allow_delete"]          = Session["page_allow_delete_from_registry"];
                ViewState["page_allow_edit"]            = Session["page_allow_edit_from_registry"];
                ViewState["page_allow_edit_history"]    = Session["page_allow_edit_history_from_registry"];
                ViewState["page_allow_print"]           = Session["page_allow_print_from_registry"];

                //********************************************************************
                //  BEGIN - VJA- 06/20/2019 - This is Session Variable Coming From Login
                //********************************************************************
                if (Session["ep_post_authority"].ToString() == "1")
                {
                    ViewState["page_allow_add"]     = 0;
                    ViewState["page_allow_delete"]  = 1;
                    ViewState["page_allow_edit"]    = 1;
                    ViewState["page_allow_print"]   = 1;
                }
                else
                {
                    ViewState["page_allow_add"]     = 1;
                    ViewState["page_allow_delete"]  = 1;
                    ViewState["page_allow_edit"]    = 1;
                    ViewState["page_allow_print"]   = 0;
                }


                if (Session["PreviousValuesonPage_cPayRegistry"] == null || Session["PreviousValuesonPage_cPayRegistry"].ToString() == "")
                {
                    Response.Redirect("~/View/cPayRegistry/cPayRegistry.aspx");
                    return;
                }
                else if (Session["PreviousValuesonPage_cPayRegistry"].ToString() != string.Empty)
                {
                    string[] prevValues = Session["PreviousValuesonPage_cPayRegistry"].ToString().Split(new char[] { ',' });
                    RetrieveYear();
                    RetrieveEmploymentType();
                    ddl_year.SelectedValue = prevValues[0].ToString();
                    ddl_month.SelectedValue = prevValues[1].ToString();
                    ddl_empl_type.SelectedValue = prevValues[2].ToString();
                    RetriveTemplate();
                    ddl_payroll_template.SelectedValue = prevValues[3].ToString();
                    RetriveGroupings();
                    ddl_payroll_group.SelectedValue = prevValues[7].ToString();
                    lbl_registry_number.Text = prevValues[7].ToString();
                    ddl_payroll_template.Enabled = false;
                    ddl_month.Enabled = false;
                    ddl_year.Enabled = false;
                    ddl_empl_type.Enabled = false;

                    //"2019,01,RE,032,0,10,0003,000081"
                    RetrieveReserveDeduction();
                    RetrieveEmployeename();
                    RetrieveDataListGrid();
                    RetrievePayrollInstallation();

                }

                // BEGIN - VJA : 02/01/2020 - Hide the ADD BUTTON IF THE STATUS ARE ;
                // 1. ) R = Released
                // 2. ) Y = Posted
                // 3. ) T = Return
                if (Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "R" ||
                    Session["PreviousValuesonPage_cPayRegistry_post_status"].ToString().Trim() == "Y")
                {
                    ViewState["page_allow_add"] = 0;
                }
                // END   - VJA : 02/01/2020 - Hide the ADD BUTTON IF THE STATUS ARE ;
            }
        }

        //********************************************************************
        //  BEGIN - JORGE- 03/20/2019 - Initialiazed Page 
        //********************************************************************
        private void InitializePage()
        {
            Session["sortdirection"] = SortDirection.Ascending.ToString();
            Session["cPayRegistryOthPay"] = "cPayRegistryOthPay";

            RetrieveDataListGrid();
            RetrieveEmploymentType();

        }
        //*************************************************************************
        //  BEGIN - JORGE- 03/03/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetrievePayrollInstallation()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollinstallation_tbl_list", "par_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_include_history", "N");
            hidden_mone_constant_factor.Value = dt.Rows[0]["mone_constant_factor"].ToString();
        }
        //*************************************************************************
        //  BEGIN - JADE- / Populate Combo list for Payroll Year
        //*************************************************************************
        private string GetRegistry_NBR()
        {
            string group_nbr = "";
            if (ddl_payroll_group.SelectedValue.ToString().Trim() != "")
            {
                lbl_registry_number.Text = ddl_payroll_group.SelectedValue.ToString();
                if (dtGroup != null && dtGroup.Rows.Count > 0)
                {
                    DataRow[] registry_nbr = dtGroup.Select("payroll_registry_nbr='" + ddl_payroll_group.SelectedValue.ToString() + "'");
                    group_nbr = registry_nbr[0]["payroll_group_nbr"].ToString().Trim();
                }
            }
            return group_nbr;
        }

        private void RetrieveDataFields()
        {
            dtListField = MyCmn.RetrieveData("sp_othrpaysetup_tbl_list2", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            if (dtListField.Rows.Count == 0)
            {
                div_field_1.Visible = false;
                div_field_2.Visible = false;
                div_field_3.Visible = false;
                div_field_4.Visible = false;
                div_field_5.Visible = false;
            }

            else
            {

                string ded_gross_text = "DEDUCTION TO GROSS";
                string add_gross_text = "ADD TO GROSS";

                if (dtListField.Rows[0]["fld1_descr"].ToString().Trim() != "")
                {
                    Lbl_field_1.Text      = dtListField.Rows[0]["fld1_descr"].ToString() + ":";
                    lbl_field_descr1.Text = dtListField.Rows[0]["fld1_type"].ToString().Trim() == "A" ? add_gross_text : ded_gross_text ;
                    txtb_other_amount1.Visible = true;

                    if (Convert.ToDecimal(dtListField.Rows[0]["fld1_fixed_amt"].ToString().Trim()) != 0)
                    {

                        txtb_other_amount1.Text = Convert.ToDecimal(dtListField.Rows[0]["fld1_fixed_amt"].ToString().Trim()).ToString("#,##0.00");
                    }

                }

                else
                {
                    div_field_1.Visible = false;
                    Lbl_field_1.Text = "";
                    txtb_other_amount1.Visible = false;
                }

                if (dtListField.Rows[0]["fld2_descr"].ToString().Trim() != "")
                {
                    Lbl_field_2.Text = dtListField.Rows[0]["fld2_descr"].ToString() + ":";
                    lbl_field_descr2.Text = dtListField.Rows[0]["fld2_type"].ToString().Trim() == "A" ? add_gross_text : ded_gross_text ;
                    txtb_other_amount2.Visible = true;

                    if (Convert.ToDecimal(dtListField.Rows[0]["fld2_fixed_amt"].ToString().Trim()) != 0)
                    {

                        txtb_other_amount2.Text = Convert.ToDecimal(dtListField.Rows[0]["fld2_fixed_amt"].ToString().Trim()).ToString("#,##0.00");
                    }

                }

                else
                {
                    div_field_2.Visible = false;
                    Lbl_field_2.Text = "";
                    txtb_other_amount2.Visible = false;
                }

                if (dtListField.Rows[0]["fld3_descr"].ToString().Trim() != "")
                {
                    Lbl_field_3.Text = dtListField.Rows[0]["fld3_descr"].ToString() + ":";
                    lbl_field_descr3.Text = dtListField.Rows[0]["fld3_type"].ToString().Trim() == "A" ? add_gross_text : ded_gross_text ;
                    txtb_other_amount3.Visible = true;

                    if (Convert.ToDecimal(dtListField.Rows[0]["fld3_fixed_amt"].ToString().Trim()) != 0)
                    {

                        txtb_other_amount3.Text = Convert.ToDecimal(dtListField.Rows[0]["fld3_fixed_amt"].ToString().Trim()).ToString("#,##0.00");
                    }
                }

                else
                {
                    div_field_3.Visible = false;
                    Lbl_field_3.Text = "";
                    txtb_other_amount3.Visible = false;
                }

                if (dtListField.Rows[0]["fld4_descr"].ToString().Trim() != "")
                {
                    Lbl_field_4.Text = dtListField.Rows[0]["fld4_descr"].ToString() + ":";
                    lbl_field_descr4.Text = dtListField.Rows[0]["fld4_type"].ToString().Trim() == "A" ? add_gross_text : ded_gross_text ;
                    txtb_other_amount4.Visible = true;

                    if (Convert.ToDecimal(dtListField.Rows[0]["fld4_fixed_amt"].ToString().Trim()) != 0)
                    {

                        txtb_other_amount4.Text = Convert.ToDecimal(dtListField.Rows[0]["fld4_fixed_amt"].ToString().Trim()).ToString("#,##0.00");
                    }
                }

                else
                {
                    div_field_4.Visible = false;
                    Lbl_field_4.Text = "";
                    txtb_other_amount4.Visible = false;
                }

                if (dtListField.Rows[0]["fld5_descr"].ToString().Trim() != "")
                {
                    Lbl_field_5.Text = dtListField.Rows[0]["fld5_descr"].ToString() + ":";
                    lbl_field_descr5.Text = dtListField.Rows[0]["fld5_type"].ToString().Trim() == "A" ? add_gross_text : ded_gross_text ;
                    txtb_other_amount5.Visible = true;

                    if (Convert.ToDecimal(dtListField.Rows[0]["fld5_fixed_amt"].ToString().Trim()) != 0)
                    {

                        txtb_other_amount5.Text = Convert.ToDecimal(dtListField.Rows[0]["fld5_fixed_amt"].ToString().Trim()).ToString("#,##0.00");
                    }
                }

                else
                {
                    div_field_5.Visible = false;
                    Lbl_field_5.Text = "";
                    txtb_other_amount5.Visible = false;
                }


            }

            lbl_generic_notes.Text = "Remarks:";
            PanelRowDiv.Update();
        }

        private void RetrieveEmployeename()
        {
            ddl_empl_id.Items.Clear();
            div_generic_notes.Visible = true;
            
            div_gross_pay.Visible = true;
            lbl_gross_pay_descr.Text = "Gross Pay:";
            div_generic_notes.Visible = true;
            div_net_pay.Visible = true;
            ddl_generic_notes.Visible = false;
            txtb_memo.Visible = true;
            div_remarks.Visible = false;
            lbl_net_pay.Text = "Net Pay :";
            dataList_employee = MyCmn.RetrieveData("sp_personnelnames_combolist_othpay", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrol_group_nbr", GetRegistry_NBR(), "par_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            ddl_empl_id.DataSource = dataList_employee;
            ddl_empl_id.DataValueField = "empl_id";
            ddl_empl_id.DataTextField = "employee_name";
            ddl_empl_id.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_empl_id.Items.Insert(0, li);
        }


        private void RetrieveEmploymentType()
        {
            ddl_empl_type.Items.Clear();
            ddl_generic_notes.Visible = true;
            DataTable dt = MyCmn.RetrieveData("sp_employmenttypes_tbl_list");
            ddl_empl_type.DataSource = dt;
            ddl_empl_type.DataTextField = "employmenttype_description";
            ddl_empl_type.DataValueField = "employment_type";
            ddl_empl_type.DataBind();
            ddl_empl_type.Enabled = false;
        }
        

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
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
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveGroupings()
        {
            ddl_payroll_group.Items.Clear();
            dtGroup = MyCmn.RetrieveData("sp_payrollregistry_hdr_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            dtGroup.DefaultView.Sort = "grouping_descr";
            dtGroup.AcceptChanges();
            ddl_payroll_group.DataSource = dtGroup;
            ddl_payroll_group.DataValueField = "payroll_registry_nbr";
            ddl_payroll_group.DataTextField = "grouping_descr";
            ddl_payroll_group.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_group.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/03/2019 - Populate Combo list for Payroll Year
        //*************************************************************************
        private void RetriveTemplate()
        {
            ddl_payroll_template.Items.Clear();
            DataTable dt = MyCmn.RetrieveData("sp_payrolltemplate_tbl_list6", "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());

            ddl_payroll_template.DataSource = dt;
            ddl_payroll_template.DataValueField = "payrolltemplate_code";
            ddl_payroll_template.DataTextField = "payrolltemplate_descr";
            ddl_payroll_template.DataBind();
            ListItem li = new ListItem("-- Select Here --", "");
            ddl_payroll_template.Items.Insert(0, li);
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Retrieve back end data and load to GridView
        //*************************************************************************
        private void RetrieveDataListGrid()
        {
            GetRegistry_NBR();
            //dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_dtl_othpay_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", ddl_payroll_group.SelectedValue.ToString().Trim());
            dataListGrid = MyCmn.RetrieveData("sp_payrollregistry_dtl_othpay_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_payroll_registry_nbr", lbl_registry_number.Text.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim());
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
            up_dataListGrid.Update();
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Add button to trigger add/edit page
        //*************************************************************************
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();

            ViewState.Add("AddEdit_Mode", MyCmn.CONST_ADD);

            RetrieveEmployeename();
            RetrieveDataFields();
            ddl_empl_id.Enabled = true;
            ddl_empl_id.Visible = true;
            btnSave.Visible = true;
            txtb_empl_name.Visible = false;
            txtb_voucher_nbr.Enabled = false;
            lbl_if_dateposted_yes.Text = "";
            ToogleTextbox(true);
            ToogleByTemplate();

            LabelAddEdit.Text = "Add New Record | Registry No: " + lbl_registry_number.Text.Trim();
            FieldValidationColorChanged(false, "ALL");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        //*************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Clear Add/Edit Page Fields
        //*************************************************************************
        private void ClearEntry()
        {

            FieldValidationColorChanged(false, "ALL");

            txtb_monthly_rate.Text = "0.00";
            txtb_hourly_rate.Text = "0.00";
            txtb_daily_rate.Text = "0.00";
            txtb_gross_pay.Text = "0.00";
            txtb_net_pay.Text = "0.00";
            txtb_other_amount.Text = "0.00";
            txtb_other_amount1.Text = "0.00";
            txtb_other_amount2.Text = "0.00";
            txtb_other_amount3.Text = "0.00";
            txtb_other_amount4.Text = "0.00";
            txtb_other_amount5.Text = "0.00";
            txtb_memo.Text = "";
            ddl_empl_id.SelectedIndex = -1;
            txtb_empl_id.Text = "";
            txtb_remarks.Text = "";

            txtb_voucher_nbr.Text        = "";
            ViewState["created_by_user"] = "";
            ViewState["updated_by_user"] = "";
            ViewState["posted_by_user"]  = "";
            ViewState["created_dttm"] = "";
            ViewState["updated_dttm"] = "";
            txtb_date_posted.Text = "";
            txtb_department_descr.Text = "";
            txtb_position.Text = "";
            txtb_status.Text = "";
            
            // Add Field Again 2022-05-30
            txtb_other_ded_mand1.Text   =  "0.00";
            txtb_other_ded_mand2.Text   =  "0.00";
            txtb_other_ded_mand3.Text   =  "0.00";
            txtb_other_ded_mand4.Text   =  "0.00";
            txtb_other_ded_mand5.Text   =  "0.00";
            txtb_other_ded_mand6.Text   =  "0.00";
            txtb_other_ded_mand7.Text   =  "0.00";
            txtb_other_ded_mand8.Text   =  "0.00";
            txtb_other_ded_mand9.Text   =  "0.00";
            txtb_other_ded_mand10.Text  =  "0.00";
            txtb_other_ded_prem1.Text   =  "0.00";
            txtb_other_ded_prem2.Text   =  "0.00";
            txtb_other_ded_prem3.Text   =  "0.00";
            txtb_other_ded_prem4.Text   =  "0.00";
            txtb_other_ded_prem5.Text   =  "0.00";
            txtb_other_ded_prem6.Text   =  "0.00";
            txtb_other_ded_prem7.Text   =  "0.00";
            txtb_other_ded_prem8.Text   =  "0.00";
            txtb_other_ded_prem9.Text   =  "0.00";
            txtb_other_ded_prem10.Text  =  "0.00";
            txtb_other_ded_loan1.Text   =  "0.00";
            txtb_other_ded_loan2.Text   =  "0.00";
            txtb_other_ded_loan3.Text   =  "0.00";
            txtb_other_ded_loan4.Text   =  "0.00";
            txtb_other_ded_loan5.Text   =  "0.00";
            txtb_other_ded_loan6.Text   =  "0.00";
            txtb_other_ded_loan7.Text   =  "0.00";
            txtb_other_ded_loan8.Text   =  "0.00";
            txtb_other_ded_loan9.Text   =  "0.00";
            txtb_other_ded_loan10.Text  =  "0.00";
        }


        //*************************************************************************
        //  BEGIN - JORGE- 03/20/2019 - Initialized datasource fields/columns
        //*************************************************************************
        private void InitializeTable()
        {

            dtSource = new DataTable();

            dtSource.Columns.Add("payroll_year", typeof(System.String));
            dtSource.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource.Columns.Add("empl_id", typeof(System.String));
            dtSource.Columns.Add("rate_basis", typeof(System.String));
            dtSource.Columns.Add("monthly_rate", typeof(System.String));
            dtSource.Columns.Add("daily_rate", typeof(System.String));
            dtSource.Columns.Add("hourly_rate", typeof(System.String));
            dtSource.Columns.Add("gross_pay", typeof(System.String));
            dtSource.Columns.Add("net_pay", typeof(System.String));
            dtSource.Columns.Add("other_amount1", typeof(System.String));
            dtSource.Columns.Add("other_amount2", typeof(System.String));
            dtSource.Columns.Add("other_amount3", typeof(System.String));
            dtSource.Columns.Add("other_amount4", typeof(System.String));
            dtSource.Columns.Add("other_amount5", typeof(System.String));
            dtSource.Columns.Add("notes_generic", typeof(System.String));
            dtSource.Columns.Add("post_status", typeof(System.String));
            dtSource.Columns.Add("date_posted", typeof(System.String));
            dtSource.Columns.Add("voucher_nbr", typeof(System.String));
            dtSource.Columns.Add("created_by_user", typeof(System.String));
            dtSource.Columns.Add("updated_by_user", typeof(System.String));
            dtSource.Columns.Add("posted_by_user", typeof(System.String));
            dtSource.Columns.Add("created_dttm", typeof(System.String));
            dtSource.Columns.Add("updated_dttm", typeof(System.String));
            dtSource.Columns.Add("remarks", typeof(System.String));

        }



        //*************************************************************************
        //  BEGIN - JORGE- 03/20/2019 - Add Primary Key Field to datasource
        //*************************************************************************
        private void AddPrimaryKeys()
        {
            dtSource.TableName = "payrollregistry_dtl_othpay_tbl";
            dtSource.Columns.Add("action", typeof(System.Int32));
            dtSource.Columns.Add("retrieve", typeof(System.Boolean));
            string[] col = new string[] { "payroll_year", "payroll_registry_nbr","empl_id"};
            dtSource = MyCmn.AddPrimaryKeys(dtSource, col);
        }


        //*************************************************************************
        //  BEGIN - JORGE- 03/20/2019 - Add new row to datatable object
        //*************************************************************************
        private void AddNewRow()
        {
            DataRow nrow = dtSource.NewRow();

            nrow["payroll_year"] = string.Empty;
            nrow["payroll_registry_nbr"] = string.Empty;
            nrow["empl_id"] = string.Empty;
            nrow["rate_basis"] = string.Empty;
            nrow["monthly_rate"] = string.Empty;
            nrow["daily_rate"] = string.Empty;
            nrow["hourly_rate"] = string.Empty;
            nrow["gross_pay"] = string.Empty;
            nrow["net_pay"] = string.Empty;
            nrow["other_amount1"] = string.Empty;
            nrow["other_amount2"] = string.Empty;
            nrow["other_amount3"] = string.Empty;
            nrow["other_amount4"] = string.Empty;
            nrow["other_amount5"] = string.Empty;
            nrow["notes_generic"] = string.Empty;
            nrow["post_status"] = string.Empty;
            nrow["date_posted"] = string.Empty;
            nrow["remarks"] = string.Empty;

            nrow["action"] = 1;
            nrow["retrieve"] = false;

            dtSource.Rows.Add(nrow);
        }



        //***************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Triggers Delete Confirmation Pop-up Dialog Box
        //***************************************************************************
        protected void deleteRow_Command(object sender, CommandEventArgs e)
        {

            FieldValidationColorChanged(false, "ALL");
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string empl_id = commandArgs[0];
            string payroll_registry_nbr = commandArgs[1];
            string payroll_year = commandArgs[2];
            txtb_reason.Text = "";
            if (Session["ep_post_authority"].ToString() == "0")
            {
                // This is the Normal Delete if the user is Non-accounting user
                deleteRec1.Text = "Are you sure to delete this Record ?";
                deleteRec0.InnerText = "Delete this Record ?";
                lbl_unposting.Text = "";
                txtb_reason.Visible = false;
                lnkBtnYes.Text = "Yes, Delete it";
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                // This is Message if the accounting user will unpost the card 
                deleteRec1.Text = "Are you sure you want to UnPost this Record ?";
                deleteRec0.InnerText = "UnPost this Record ?";
                lbl_unposting.Text = "Reason for UnPosting :";
                txtb_reason.Visible = true;
                lnkBtnYes.Text = "Yes, UnPost it";
            }

            lnkBtnYes.CommandArgument = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }

        //*************************************************************************
        //  BEGIN - VJA- 04/06/2019 - Delete Data to back-end Database
        //*************************************************************************
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string[] commandarg = e.CommandArgument.ToString().Split(new char[] { ',' });
            string deleteExpression = "empl_id = '" + commandarg[0].Trim() + "' AND payroll_registry_nbr = '" + commandarg[1].Trim() + "' AND payroll_year = '" + commandarg[2].Trim() + "'";

            if (Session["ep_post_authority"].ToString() == "0")
            {
                // Non-Accounting Delete 
                MyCmn.DeleteBackEndData("payrollregistry_dtl_othpay_tbl", "WHERE " + deleteExpression);
                MyCmn.DeleteBackEndData("payrollregistry_dtl_othded_tbl", "WHERE " + deleteExpression);
                DataRow[] row2Delete = dataListGrid.Select(deleteExpression);
                dataListGrid.Rows.Remove(row2Delete[0]);
                dataListGrid.AcceptChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
            }
            else if (Session["ep_post_authority"].ToString() == "1")
            {
                if (txtb_reason.Text == "")
                {
                    FieldValidationColorChanged(true, "txtb_reason");
                }
                else
                {
                    // Stored Procedure to Insert to payrollregistry_dtl_unpost_tbl during accounting case
                    DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_unpost_tbl_insert", "par_payroll_year", ddl_year.SelectedValue, "par_payroll_registry_nbr", commandarg[1].Trim(), "par_empl_id", commandarg[0].Trim(), "par_reason", txtb_reason.Text);

                    //4.4.b.Update the following fields: From payrollregistry_dtl_rata_tbl Table
                    //    1.voucher_nbr     =   { blank}
                    //    2.posted_by_user  =   { blank}
                    //    3.post_status     =   "N"   
                    //    4.date_posted     =   { blank}
                    //    5.updated_by_user =   session user_id   
                    //    6.updated_dttm    =   System Date

                    string setparams = "";
                    setparams = "voucher_nbr = '',posted_by_user = '',post_status='N',date_posted='' , updated_by_user='" + Session["ep_user_id"].ToString() + "', updated_dttm='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    MyCmn.UpdateTable("payrollregistry_dtl_othpay_tbl", setparams, "WHERE " + deleteExpression);
                    RetrieveDataListGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalDelete();", true);
                }
            }

            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            up_dataListGrid.Update();
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Edit Row selection that will trigger edit page 
        //**************************************************************************
        protected void editRow_Command(object sender, CommandEventArgs e)
        {
            lbl_rate_basis_descr.Text = "";
            string[] svalues = e.CommandArgument.ToString().Split(',');
            string editExpression = "empl_id = '" + svalues[0].ToString().Trim() + "'";

            DataRow[] row2Edit = dataListGrid.Select(editExpression);

            ClearEntry();
            InitializeTable();
            AddPrimaryKeys();
            AddNewRow();
            //RetrieveLastRow();
            ViewState.Add("AddEdit_Mode", MyCmn.CONST_EDIT);
            dtSource.Rows[0]["action"] = 2;
            dtSource.Rows[0]["retrieve"] = true;
            
            //RetrieveEmployeename();
            RetrieveDataFields();
            //ddl_empl_id.SelectedValue = row2Edit[0]["empl_id"].ToString().Trim();
            txtb_empl_id.Text = row2Edit[0]["empl_id"].ToString().Trim();
            txtb_empl_name.Text = row2Edit[0]["employee_name"].ToString().Trim();
            
            txtb_monthly_rate.Text      = row2Edit[0]["monthly_rate"].ToString();
            txtb_daily_rate.Text        = row2Edit[0]["daily_rate"].ToString();
            txtb_hourly_rate.Text       = row2Edit[0]["hourly_rate"].ToString();
            lbl_rate_basis_descr.Text   = row2Edit[0]["rate_basis_descr"].ToString();
            txtb_empl_id.Text           = row2Edit[0]["empl_id"].ToString();
            txtb_position.Text          = row2Edit[0]["position_title1"].ToString();
            txtb_department_descr.Text  = row2Edit[0]["department_name1"].ToString();
            hidden_rate_basis.Value     = row2Edit[0]["rate_basis"].ToString();
            txtb_department_code.Text   = row2Edit[0]["department_code"].ToString();


            //if (ddl_payroll_template.SelectedValue != "922" &&  // Special Risk Allowance - RE
            //    ddl_payroll_template.SelectedValue != "932" &&  // Special Risk Allowance - CE
            //    ddl_payroll_template.SelectedValue != "942")    // Special Risk Allowance - JO
            //{
            //    header_details();
            //}

            ddl_empl_id.Enabled = false;
            ddl_empl_id.Visible = false;
            txtb_empl_name.Visible = true;

            txtb_net_pay.Text = double.Parse(row2Edit[0]["net_pay"].ToString().Trim()).ToString("###,##0.00");
            txtb_gross_pay.Text = double.Parse(row2Edit[0]["gross_pay"].ToString().Trim()).ToString("###,##0.00");
            dtSource.Rows[0]["post_status"] = row2Edit[0]["post_status"].ToString().Trim();
            dtSource.Rows[0]["date_posted"] = row2Edit[0]["date_posted"].ToString().Trim();
            txtb_memo.Text = row2Edit[0]["notes_generic"].ToString().Trim();

            txtb_other_amount1.Text = row2Edit[0]["other_amount1"].ToString().Trim();
            txtb_other_amount2.Text = row2Edit[0]["other_amount2"].ToString().Trim();
            txtb_other_amount3.Text = row2Edit[0]["other_amount3"].ToString().Trim();
            txtb_other_amount4.Text = row2Edit[0]["other_amount4"].ToString().Trim();
            txtb_other_amount5.Text = row2Edit[0]["other_amount5"].ToString().Trim();

            txtb_remarks.Text = row2Edit[0]["remarks"].ToString().Trim();

            // Add Field Again - VJA - 2022-05-26 - Other Deduction 
            txtb_other_ded_mand1.Text   =  row2Edit[0]["other_ded_mand1"].ToString();
            txtb_other_ded_mand2.Text   =  row2Edit[0]["other_ded_mand2"].ToString();
            txtb_other_ded_mand3.Text   =  row2Edit[0]["other_ded_mand3"].ToString();
            txtb_other_ded_mand4.Text   =  row2Edit[0]["other_ded_mand4"].ToString();
            txtb_other_ded_mand5.Text   =  row2Edit[0]["other_ded_mand5"].ToString();
            txtb_other_ded_mand6.Text   =  row2Edit[0]["other_ded_mand6"].ToString();
            txtb_other_ded_mand7.Text   =  row2Edit[0]["other_ded_mand7"].ToString();
            txtb_other_ded_mand8.Text   =  row2Edit[0]["other_ded_mand8"].ToString();
            txtb_other_ded_mand9.Text   =  row2Edit[0]["other_ded_mand9"].ToString();
            txtb_other_ded_mand10.Text  =  row2Edit[0]["other_ded_mand10"].ToString();
            txtb_other_ded_prem1.Text   =  row2Edit[0]["other_ded_prem1"].ToString();
            txtb_other_ded_prem2.Text   =  row2Edit[0]["other_ded_prem2"].ToString();
            txtb_other_ded_prem3.Text   =  row2Edit[0]["other_ded_prem3"].ToString();
            txtb_other_ded_prem4.Text   =  row2Edit[0]["other_ded_prem4"].ToString();
            txtb_other_ded_prem5.Text   =  row2Edit[0]["other_ded_prem5"].ToString();
            txtb_other_ded_prem6.Text   =  row2Edit[0]["other_ded_prem6"].ToString();
            txtb_other_ded_prem7.Text   =  row2Edit[0]["other_ded_prem7"].ToString();
            txtb_other_ded_prem8.Text   =  row2Edit[0]["other_ded_prem8"].ToString();
            txtb_other_ded_prem9.Text   =  row2Edit[0]["other_ded_prem9"].ToString();
            txtb_other_ded_prem10.Text  =  row2Edit[0]["other_ded_prem10"].ToString();
            txtb_other_ded_loan1.Text   =  row2Edit[0]["other_ded_loan1"].ToString();
            txtb_other_ded_loan2.Text   =  row2Edit[0]["other_ded_loan2"].ToString();
            txtb_other_ded_loan3.Text   =  row2Edit[0]["other_ded_loan3"].ToString();
            txtb_other_ded_loan4.Text   =  row2Edit[0]["other_ded_loan4"].ToString();
            txtb_other_ded_loan5.Text   =  row2Edit[0]["other_ded_loan5"].ToString();
            txtb_other_ded_loan6.Text   =  row2Edit[0]["other_ded_loan6"].ToString();
            txtb_other_ded_loan7.Text   =  row2Edit[0]["other_ded_loan7"].ToString();
            txtb_other_ded_loan8.Text   =  row2Edit[0]["other_ded_loan8"].ToString();
            txtb_other_ded_loan9.Text   =  row2Edit[0]["other_ded_loan9"].ToString();
            txtb_other_ded_loan10.Text  =  row2Edit[0]["other_ded_loan10"].ToString();


            if (row2Edit[0]["post_status"].ToString().ToUpper() == "Y")
            {
                lbl_if_dateposted_yes.Text = "This Payroll is already Posted You connot Edit!";
                btnSave.Visible = false;
            }

            else
            {
                lbl_if_dateposted_yes.Text = "";
                btnSave.Visible = true;
            }

            LabelAddEdit.Text = "Edit Record | Registry No : " + lbl_registry_number.Text.Trim();
            //lbl_addeditmode_hidden.Text = MyCmn.CONST_EDIT;

            ddl_empl_id.Enabled = false;
            FieldValidationColorChanged(false, "ALL");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            // Add Field Again - 06/20/2019
            txtb_voucher_nbr.Text        = row2Edit[0]["voucher_nbr"].ToString();
            ViewState["created_by_user"] = row2Edit[0]["created_by_user"].ToString();
            ViewState["updated_by_user"] = row2Edit[0]["updated_by_user"].ToString();
            ViewState["posted_by_user"]  = row2Edit[0]["posted_by_user"].ToString();
            ViewState["created_dttm"]    = Convert.ToDateTime(row2Edit[0]["created_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            ViewState["updated_dttm"]    = Convert.ToDateTime(row2Edit[0]["updated_dttm"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            txtb_status.Text = row2Edit[0]["post_status_descr"].ToString();
            dtSource.Rows[0]["date_posted"] = row2Edit[0]["date_posted"].ToString().Trim();

            //FOR POSTED STATUS
            if (row2Edit[0]["post_status"].ToString().ToUpper() == "Y" && (Session["ep_post_authority"].ToString() == "1" || Session["ep_post_authority"].ToString() == "0"))
            {
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                txtb_voucher_nbr.Enabled = false;
                Linkbtncancel.Text = "Close";
                lbl_if_dateposted_yes.Text = "This Payroll is already Posted, You Cannot Edit!";
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }

            //FOR USER'S IN ACCOUNTING
            else if (row2Edit[0]["post_status"].ToString() == "N" && Session["ep_post_authority"].ToString() == "1")
            {
                btnSave.Visible = true;
                btn_calculate.Visible = false;
                btnSave.Text = "Post To Card";
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = true;
                txtb_date_posted.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ToogleTextbox(false);
            }
            //FOR RELEASED STATUS
            else if (row2Edit[0]["post_status"].ToString()   == "R"
                  //  || row2Edit[0]["post_status"].ToString() == "T"
                    || row2Edit[0]["post_status"].ToString() == "X"
                    )
            {
                btnSave.Visible = false;
                btn_calculate.Visible = false;
                txtb_voucher_nbr.Enabled = false;
                Linkbtncancel.Text = "Close";
                lbl_if_dateposted_yes.Text = "This Payroll Already " + row2Edit[0]["post_status_descr"].ToString() + ", You cannot Edit!";
                txtb_date_posted.Text = row2Edit[0]["date_posted"].ToString();
                ToogleTextbox(false);
            }
            //FOR OTHER DEPARTMENTS
            else if (Session["ep_post_authority"].ToString() == "0")
            {
                txtb_voucher_nbr.Enabled = false;
                btnSave.Visible = true;
                btn_calculate.Visible = true;
                btnSave.Text = "Save";
                Linkbtncancel.Text = "Cancel";
                lbl_if_dateposted_yes.Text = "";
                txtb_voucher_nbr.Enabled = false;
                txtb_date_posted.Text = "";
                ToogleTextbox(true);

            }
            ToogleByTemplate();
        }

        //**************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Change Field Sort mode  
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
        //  BEGIN - JOSEPH- 03/20/2019 - Get Grid current sort order 
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
        //  BEGIN - JOSEPH- 03/20/2019 - Save New Record/Edited Record to back end DB
        //**************************************************************************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidationColorChanged(false, "ALL");
            string saveRecord = ViewState["AddEdit_Mode"].ToString();
            string scriptInsertUpdate = string.Empty;

            if (IsDataValidated())
            {

                //if (ddl_payroll_template.SelectedValue != "922" &&  // Special Risk Allowance - RE
                //    ddl_payroll_template.SelectedValue != "932" &&  // Special Risk Allowance - CE
                //    ddl_payroll_template.SelectedValue != "942")    // Special Risk Allowance - JO
                //{
                //    header_details();
                //}
                calculate_gross_net();
                CalculateGrossNetByTemplate();
                //DataRow[] selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");


                if (saveRecord == MyCmn.CONST_ADD)
                {

                    dtSource.Rows[0]["payroll_year"]          = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"]  = lbl_registry_number.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]               = ddl_empl_id.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]            = hidden_rate_basis.Value;
                    dtSource.Rows[0]["monthly_rate"]          = txtb_monthly_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]            = txtb_daily_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["hourly_rate"]           = txtb_hourly_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount1"]         = txtb_other_amount1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount2"]         = txtb_other_amount2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount3"]         = txtb_other_amount3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount4"]         = txtb_other_amount4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount5"]         = txtb_other_amount5.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]               = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["gross_pay"]             = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["post_status"]           = "N";
                    dtSource.Rows[0]["notes_generic"]         = txtb_memo.Text.ToString().Trim();

                    // BEGIN - Add Field Again  - 06/20/2019
                    dtSource.Rows[0]["voucher_nbr"] = txtb_voucher_nbr.Text.ToString();
                    dtSource.Rows[0]["created_by_user"] = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_by_user"] = "";

                    dtSource.Rows[0]["created_dttm"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["updated_dttm"] = "";

                    dtSource.Rows[0]["posted_by_user"] = "";
                    dtSource.Rows[0]["date_posted"] = "";
                    dtSource.Rows[0]["remarks"] = txtb_remarks.Text.ToString().Trim();
                    // END - Add Field Again  - 06/20/2019

                    scriptInsertUpdate = MyCmn.get_insertscript(dtSource);

                }
                else if (saveRecord == MyCmn.CONST_EDIT)
                {
                    dtSource.Rows[0]["payroll_year"]            = ddl_year.SelectedValue.ToString().Trim();
                    dtSource.Rows[0]["payroll_registry_nbr"]    = lbl_registry_number.Text.ToString().Trim();
                    dtSource.Rows[0]["empl_id"]                 = txtb_empl_id.Text.ToString().Trim();
                    dtSource.Rows[0]["rate_basis"]              = hidden_rate_basis.Value;
                    dtSource.Rows[0]["monthly_rate"]            = txtb_monthly_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["daily_rate"]              = txtb_daily_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["hourly_rate"]             = txtb_hourly_rate.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount1"]           = txtb_other_amount1.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount2"]           = txtb_other_amount2.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount3"]           = txtb_other_amount3.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount4"]           = txtb_other_amount4.Text.ToString().Trim();
                    dtSource.Rows[0]["other_amount5"]           = txtb_other_amount5.Text.ToString().Trim();
                    dtSource.Rows[0]["net_pay"]                 = txtb_net_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["gross_pay"]               = txtb_gross_pay.Text.ToString().Trim();
                    dtSource.Rows[0]["post_status"]             = "N";
                    dtSource.Rows[0]["notes_generic"]           = txtb_memo.Text.ToString().Trim();

                    //BEGIN - Add Field Again  - 06/20/2019

                    dtSource.Rows[0]["created_by_user"]         = ViewState["created_by_user"].ToString();
                    dtSource.Rows[0]["updated_by_user"]         = Session["ep_user_id"].ToString();
                    dtSource.Rows[0]["updated_dttm"]            = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtSource.Rows[0]["created_dttm"]            = ViewState["created_dttm"].ToString();

                    if (Session["ep_post_authority"].ToString().Trim() == "1")
                    {
                        dtSource.Rows[0]["posted_by_user"]      = Session["ep_user_id"].ToString();
                        dtSource.Rows[0]["date_posted"]         = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dtSource.Rows[0]["post_status"]         = "Y";
                        dtSource.Rows[0]["voucher_nbr"]         = txtb_voucher_nbr.Text.ToString().Trim();
                        dtSource.Rows[0]["updated_by_user"]     = ViewState["updated_by_user"].ToString();
                        dtSource.Rows[0]["updated_dttm"]        = ViewState["updated_dttm"].ToString();
                    }
                    dtSource.Rows[0]["remarks"] = txtb_remarks.Text.ToString().Trim();
                    // END - Add Field Again  - 06/20/2019

                    scriptInsertUpdate = MyCmn.updatescript(dtSource);
                }

                if (saveRecord == MyCmn.CONST_ADD || saveRecord == MyCmn.CONST_EDIT)
                {
                    if (double.Parse(txtb_net_pay.Text) < 0)
                    {
                        FieldValidationColorChanged(true, "txtb_net_pay-negative");
                        txtb_net_pay.Focus();
                        return;
                    }

                    if (scriptInsertUpdate == string.Empty) { return; }
                    string msg = MyCmn.insertdata(scriptInsertUpdate);
                    if (msg == "") { return; }

                    if (msg.Substring(0, 1) == "X")
                    {
                        FieldValidationColorChanged(true, "already-exist");
                        return;
                    }
                    InsertUpdateOtherDeduction();
                    if (saveRecord == MyCmn.CONST_ADD)
                    {
                        DataRow nrow = dataListGrid.NewRow();

                        nrow["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                        nrow["payroll_registry_nbr"] = lbl_registry_number.Text.ToString().Trim();
                        nrow["empl_id"]              = ddl_empl_id.SelectedValue.ToString().Trim();
                        nrow["employee_name"]        = ddl_empl_id.SelectedItem.Text.ToString().Trim();
                        nrow["rate_basis"]           = hidden_rate_basis.Value;
                        nrow["monthly_rate"]         = Convert.ToDecimal(txtb_monthly_rate.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["daily_rate"]           = Convert.ToDecimal(txtb_daily_rate.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["hourly_rate"]          = Convert.ToDecimal(txtb_hourly_rate.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["net_pay"]              = Convert.ToDecimal(txtb_net_pay.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["gross_pay"]            = Convert.ToDecimal(txtb_gross_pay.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["other_amount1"]        = Convert.ToDecimal(txtb_other_amount1.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["other_amount2"]        = Convert.ToDecimal(txtb_other_amount2.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["other_amount3"]        = Convert.ToDecimal(txtb_other_amount3.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["other_amount4"]        = Convert.ToDecimal(txtb_other_amount4.Text.ToString().Trim()).ToString("#,##0.00");
                        nrow["other_amount5"]        = Convert.ToDecimal(txtb_other_amount5.Text.ToString().Trim()).ToString("#,##0.00"); 
                        nrow["notes_generic"]        = txtb_memo.Text.ToString().Trim();

                        nrow["post_status"]          = "N";
                        nrow["post_status_descr"]    = "NOT POSTED";

                        
                        // Add Field  2022-05-30
                        nrow["other_ded_mand1"]       = txtb_other_ded_mand1.Text.ToString().Trim();
                        nrow["other_ded_mand2"]       = txtb_other_ded_mand2.Text.ToString().Trim();
                        nrow["other_ded_mand3"]       = txtb_other_ded_mand3.Text.ToString().Trim();
                        nrow["other_ded_mand4"]       = txtb_other_ded_mand4.Text.ToString().Trim();
                        nrow["other_ded_mand5"]       = txtb_other_ded_mand5.Text.ToString().Trim();
                        nrow["other_ded_mand6"]       = txtb_other_ded_mand6.Text.ToString().Trim();
                        nrow["other_ded_mand7"]       = txtb_other_ded_mand7.Text.ToString().Trim();
                        nrow["other_ded_mand8"]       = txtb_other_ded_mand8.Text.ToString().Trim();
                        nrow["other_ded_mand9"]       = txtb_other_ded_mand9.Text.ToString().Trim();
                        nrow["other_ded_mand10"]      = txtb_other_ded_mand10.Text.ToString().Trim();
                        nrow["other_ded_prem1"]       = txtb_other_ded_prem1.Text.ToString().Trim();
                        nrow["other_ded_prem2"]       = txtb_other_ded_prem2.Text.ToString().Trim();
                        nrow["other_ded_prem3"]       = txtb_other_ded_prem3.Text.ToString().Trim();
                        nrow["other_ded_prem4"]       = txtb_other_ded_prem4.Text.ToString().Trim();
                        nrow["other_ded_prem5"]       = txtb_other_ded_prem5.Text.ToString().Trim();
                        nrow["other_ded_prem6"]       = txtb_other_ded_prem6.Text.ToString().Trim();
                        nrow["other_ded_prem7"]       = txtb_other_ded_prem7.Text.ToString().Trim();
                        nrow["other_ded_prem8"]       = txtb_other_ded_prem8.Text.ToString().Trim();
                        nrow["other_ded_prem9"]       = txtb_other_ded_prem9.Text.ToString().Trim();
                        nrow["other_ded_prem10"]      = txtb_other_ded_prem10.Text.ToString().Trim();
                        nrow["other_ded_loan1"]       = txtb_other_ded_loan1.Text.ToString().Trim();
                        nrow["other_ded_loan2"]       = txtb_other_ded_loan2.Text.ToString().Trim();
                        nrow["other_ded_loan3"]       = txtb_other_ded_loan3.Text.ToString().Trim();
                        nrow["other_ded_loan4"]       = txtb_other_ded_loan4.Text.ToString().Trim();
                        nrow["other_ded_loan5"]       = txtb_other_ded_loan5.Text.ToString().Trim();
                        nrow["other_ded_loan6"]       = txtb_other_ded_loan6.Text.ToString().Trim();
                        nrow["other_ded_loan7"]       = txtb_other_ded_loan7.Text.ToString().Trim();
                        nrow["other_ded_loan8"]       = txtb_other_ded_loan8.Text.ToString().Trim();
                        nrow["other_ded_loan9"]       = txtb_other_ded_loan9.Text.ToString().Trim();
                        nrow["other_ded_loan10"]      = txtb_other_ded_loan10.Text.ToString().Trim();


                        switch (dtSource.Rows[0]["rate_basis"].ToString())
                        {
                            case "M":
                                {
                                    nrow["rate_display"] = txtb_monthly_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Month</small>";
                                    break;
                                }
                            case "D":
                                {
                                    nrow["rate_display"] = txtb_daily_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Day</small>";
                                    break;
                                }
                            case "H":
                                {
                                    nrow["rate_display"] = txtb_daily_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Hour</small>";
                                    break;
                                }
                            default:
                                {
                                    nrow["rate_display"] = "";
                                    break;
                                }
                        }



                        // BEGIN - Add Field Again  - 06/20/2019
                        nrow["voucher_nbr"] = txtb_voucher_nbr.Text.ToString();
                        nrow["created_by_user"] = Session["ep_user_id"].ToString();
                        nrow["updated_by_user"] = "";
                        nrow["created_dttm"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        nrow["updated_dttm"] = Convert.ToDateTime("1900-01-01");
                        nrow["posted_by_user"] = "";
                        nrow["date_posted"] = "";
                        nrow["position_title1"] = txtb_position.Text.ToString().Trim();
                        // END - Add Field Again  - 06/20/2019
                        nrow["department_name1"] = txtb_department_descr.Text;
                        nrow["rate_basis"] = hidden_rate_basis.Value;
                        nrow["rate_basis_descr"] = lbl_rate_basis_descr.Text;
                        nrow["remarks"] = txtb_remarks.Text.ToString().Trim();

                        dataListGrid.Rows.Add(nrow);
                        SaveAddEdit.Text = MyCmn.CONST_NEWREC;
                    }

                    if (saveRecord == MyCmn.CONST_EDIT)
                    {
                        string editExpression = "empl_id = '" + txtb_empl_id.Text.ToString().Trim() + "' AND payroll_year='" + ddl_year.SelectedValue.ToString().Trim() + "' AND payroll_registry_nbr ='" + lbl_registry_number.Text.ToString().Trim() + "'";
                        DataRow[] row2Edit = dataListGrid.Select(editExpression);

                        row2Edit[0]["payroll_year"]         = ddl_year.SelectedValue.ToString().Trim();
                        row2Edit[0]["payroll_registry_nbr"] = lbl_registry_number.Text.ToString().Trim();
                        row2Edit[0]["empl_id"]              = txtb_empl_id.Text.ToString().Trim();
                        row2Edit[0]["employee_name"]        = txtb_empl_name.Text.ToString().Trim();
                        row2Edit[0]["rate_basis"]           = hidden_rate_basis.Value;
                        row2Edit[0]["monthly_rate"]         = Convert.ToDecimal(txtb_monthly_rate.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["daily_rate"]           = Convert.ToDecimal(txtb_daily_rate.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["hourly_rate"]          = Convert.ToDecimal(txtb_hourly_rate.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["net_pay"]              = Convert.ToDecimal(txtb_net_pay.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["gross_pay"]            = Convert.ToDecimal(txtb_gross_pay.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["other_amount1"]        = Convert.ToDecimal(txtb_other_amount1.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["other_amount2"]        = Convert.ToDecimal(txtb_other_amount2.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["other_amount3"]        = Convert.ToDecimal(txtb_other_amount3.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["other_amount4"]        = Convert.ToDecimal(txtb_other_amount4.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["other_amount5"]        = Convert.ToDecimal(txtb_other_amount5.Text.ToString().Trim()).ToString("#,##0.00");
                        row2Edit[0]["post_status"]          = dtSource.Rows[0]["post_status"].ToString().Trim();
                        row2Edit[0]["notes_generic"]        = txtb_memo.Text.ToString().Trim();
                        row2Edit[0]["date_posted"]          = dtSource.Rows[0]["date_posted"].ToString().Trim();

                        switch (dtSource.Rows[0]["rate_basis"].ToString())
                        {
                            case "M":
                                {
                                    row2Edit[0]["rate_display"] = txtb_monthly_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Month</small>";
                                    break;
                                }
                            case "D":
                                {
                                    row2Edit[0]["rate_display"] = txtb_daily_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Day</small>";
                                    break;
                                }
                            case "H":
                                {
                                    row2Edit[0]["rate_display"] = txtb_daily_rate.Text.ToString() + " / <small style='color:green;font-weight:bold;'>Hour</small>";
                                    break;
                                }
                            default:
                                {
                                    row2Edit[0]["rate_display"] = "";
                                    break;
                                }
                        }

                        // BEGIN - Add Field Again - 06/20/2019
                        row2Edit[0]["created_by_user"] = ViewState["created_by_user"].ToString();
                        row2Edit[0]["updated_by_user"] = Session["ep_user_id"].ToString();
                        row2Edit[0]["created_dttm"] = ViewState["created_dttm"].ToString() == "" ? DateTime.Now : ViewState["created_dttm"];
                        row2Edit[0]["updated_dttm"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        // END - Add Field Again  - 06/20/2019
                        if (Session["ep_post_authority"].ToString() == "1")
                        {
                            row2Edit[0]["posted_by_user"] = Session["ep_user_id"].ToString();
                            row2Edit[0]["date_posted"] = txtb_date_posted.Text.ToString().Trim();
                            row2Edit[0]["post_status"] = "Y";
                            row2Edit[0]["post_status_descr"] = "POSTED";
                            row2Edit[0]["voucher_nbr"] = txtb_voucher_nbr.Text.ToString();
                            row2Edit[0]["updated_by_user"] = ViewState["updated_by_user"].ToString();
                            row2Edit[0]["updated_dttm"] = ViewState["updated_dttm"].ToString();
                        }
                        row2Edit[0]["department_name1"] = txtb_department_descr.Text;
                        row2Edit[0]["rate_basis"] = hidden_rate_basis.Value;
                        row2Edit[0]["rate_basis_descr"] = lbl_rate_basis_descr.Text;
                        row2Edit[0]["remarks"] = txtb_remarks.Text.ToString().Trim();

                         // Add Field  2022-05-30
                        row2Edit[0]["other_ded_mand1"]       = txtb_other_ded_mand1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand2"]       = txtb_other_ded_mand2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand3"]       = txtb_other_ded_mand3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand4"]       = txtb_other_ded_mand4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand5"]       = txtb_other_ded_mand5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand6"]       = txtb_other_ded_mand6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand7"]       = txtb_other_ded_mand7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand8"]       = txtb_other_ded_mand8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand9"]       = txtb_other_ded_mand9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_mand10"]      = txtb_other_ded_mand10.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem1"]       = txtb_other_ded_prem1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem2"]       = txtb_other_ded_prem2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem3"]       = txtb_other_ded_prem3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem4"]       = txtb_other_ded_prem4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem5"]       = txtb_other_ded_prem5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem6"]       = txtb_other_ded_prem6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem7"]       = txtb_other_ded_prem7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem8"]       = txtb_other_ded_prem8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem9"]       = txtb_other_ded_prem9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_prem10"]      = txtb_other_ded_prem10.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan1"]       = txtb_other_ded_loan1.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan2"]       = txtb_other_ded_loan2.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan3"]       = txtb_other_ded_loan3.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan4"]       = txtb_other_ded_loan4.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan5"]       = txtb_other_ded_loan5.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan6"]       = txtb_other_ded_loan6.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan7"]       = txtb_other_ded_loan7.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan8"]       = txtb_other_ded_loan8.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan9"]       = txtb_other_ded_loan9.Text.ToString().Trim();
                        row2Edit[0]["other_ded_loan10"]      = txtb_other_ded_loan10.Text.ToString().Trim();


                        CommonCode.GridViewBind(ref this.gv_dataListGrid, dataListGrid);
                        SaveAddEdit.Text = MyCmn.CONST_EDITREC;
                    }



                    MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                    ViewState.Remove("AddEdit_Mode");
                    show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
                }

            }
        }

        


        //**************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - GridView Change Page Number
        //**************************************************************************
        protected void gridviewbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_dataListGrid.PageIndex = e.NewPageIndex;
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Change on Page Size (no. of row per page) on Gridview  
        //*********************************************************************************
        protected void DropDownListID_TextChanged(object sender, EventArgs e)
        {
            gv_dataListGrid.PageSize = Convert.ToInt32(DropDownListID.Text);
            MyCmn.Sort(gv_dataListGrid, dataListGrid, Session["SortField"].ToString(), Session["SortOrder"].ToString());
            show_pagesx.Text = "Page: <b>" + (gv_dataListGrid.PageIndex + 1) + "</b>/<strong style='color:#B7B7B7;'>" + gv_dataListGrid.PageCount + "</strong>";
        }

        //**********************************************************************************
        //  BEGIN - JOSEPH- 03/12/2019 - Search Data Bind to Grid View on every KeyInput  
        //*********************************************************************************
        protected void txtb_search_TextChanged(object sender, EventArgs e)
        {
            string searchExpression = "empl_id LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR employee_name LIKE '%" + txtb_search.Text.Trim().Replace("'", "''") 
                + "%' OR position_title1 LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR post_status_descr LIKE '%" + txtb_search.Text.Trim().Replace("'", "''")
                + "%' OR net_pay LIKE '%" + txtb_search.Text.ToString().Replace("'", "''") + "%'";

            DataTable dtSource1 = new DataTable();
            dtSource1.Columns.Add("empl_id", typeof(System.String));
            dtSource1.Columns.Add("employee_name", typeof(System.String));
            dtSource1.Columns.Add("gross_pay", typeof(System.String));
            dtSource1.Columns.Add("net_pay", typeof(System.String));
            dtSource1.Columns.Add("monthly_rate", typeof(System.String));
            dtSource1.Columns.Add("hourly_rate", typeof(System.String));
            dtSource1.Columns.Add("daily_rate", typeof(System.String));
            dtSource1.Columns.Add("other_amount", typeof(System.String));
            dtSource1.Columns.Add("rate_basis", typeof(System.String));
            dtSource1.Columns.Add("rate_display", typeof(System.String));
            dtSource1.Columns.Add("notes_generic_descr", typeof(System.String));
            dtSource1.Columns.Add("notes_generic", typeof(System.String));
            dtSource1.Columns.Add("payroll_year", typeof(System.String));
            dtSource1.Columns.Add("payroll_registry_nbr", typeof(System.String));
            dtSource1.Columns.Add("post_status", typeof(System.String));
            dtSource1.Columns.Add("position_title1", typeof(System.String));
            dtSource1.Columns.Add("post_status_descr", typeof(System.String));
            dtSource1.Columns.Add("remarks", typeof(System.String));

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
        //  BEGIN - JOSEPH- 03/20/2019 - Define Property for Sort Direction  
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
        //  BEGIN - JOSEPH- 03/20/2019 - Check if Object already contains value  
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

        private bool IsDataValidated()
        {
            bool validatedSaved = true;

            FieldValidationColorChanged(false, "ALL");

            if (ddl_empl_id.SelectedValue == "" && ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                FieldValidationColorChanged(true, "ddl_empl_id");
                ddl_empl_id.Focus();
                validatedSaved = false;
            }

           
            if (CommonCode.checkisdecimal(txtb_other_amount1) == false)
            {
                if (txtb_other_amount1.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_other_amount1");
                    txtb_other_amount1.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_other_amount1");
                    txtb_other_amount1.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_other_amount2) == false)
            {
                if (txtb_other_amount2.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_other_amount2");
                    txtb_other_amount2.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_other_amount2");
                    txtb_other_amount2.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_other_amount3) == false)
            {
                if (txtb_other_amount3.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_other_amount3");
                    txtb_other_amount3.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_other_amount3");
                    txtb_other_amount3.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_other_amount4) == false)
            {
                if (txtb_other_amount4.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_other_amount4");
                    txtb_other_amount4.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_other_amount4");
                    txtb_other_amount4.Focus();
                    validatedSaved = false;
                }
            }

            if (CommonCode.checkisdecimal(txtb_other_amount5) == false)
            {
                if (txtb_other_amount1.Text.ToString().Trim() == "")
                {
                    FieldValidationColorChanged(true, "txtb_other_amount5");
                    txtb_other_amount5.Focus();
                    validatedSaved = false;
                }
                else
                {
                    FieldValidationColorChanged(true, "invalid-txtb_other_amount5");
                    txtb_other_amount5.Focus();
                    validatedSaved = false;
                }
            }
            
            //Add validation
            if (Session["ep_post_authority"].ToString() == "1" && txtb_voucher_nbr.Text.ToString().Trim() == "")
            {
                FieldValidationColorChanged(true, "txtb_voucher_nbr");
                txtb_voucher_nbr.Focus();
                validatedSaved = false;
            }
            // MANDATORY VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_mand1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand1");  txtb_other_ded_mand1.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand2");  txtb_other_ded_mand2.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand3");  txtb_other_ded_mand3.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand4");  txtb_other_ded_mand4.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand5");  txtb_other_ded_mand5.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand6");  txtb_other_ded_mand6.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand7");  txtb_other_ded_mand7.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand8");  txtb_other_ded_mand8.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_mand9");  txtb_other_ded_mand9.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_mand10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_mand10"); txtb_other_ded_mand10.Focus() ; validatedSaved  = false; }

            // OPTIONAL VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_prem1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem1");  txtb_other_ded_prem1.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem2");  txtb_other_ded_prem2.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem3");  txtb_other_ded_prem3.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem4");  txtb_other_ded_prem4.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem5");  txtb_other_ded_prem5.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem6");  txtb_other_ded_prem6.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem7");  txtb_other_ded_prem7.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem8");  txtb_other_ded_prem8.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_prem9");  txtb_other_ded_prem9.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_prem10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_prem10"); txtb_other_ded_prem10.Focus() ; validatedSaved  = false; }
             
            // LOAN VALIDATION FOR OTHER DEDUCTIONS
            if (CommonCode.checkisdecimal(txtb_other_ded_loan1) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan1");  txtb_other_ded_loan1.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan2) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan2");  txtb_other_ded_loan2.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan3) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan3");  txtb_other_ded_loan3.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan4) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan4");  txtb_other_ded_loan4.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan5) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan5");  txtb_other_ded_loan5.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan6) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan6");  txtb_other_ded_loan6.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan7) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan7");  txtb_other_ded_loan7.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan8) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan8");  txtb_other_ded_loan8.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan9) == false) { FieldValidationColorChanged(true, "txtb_other_ded_loan9");  txtb_other_ded_loan9.Focus()  ; validatedSaved  = false; }
            if (CommonCode.checkisdecimal(txtb_other_ded_loan10)== false) { FieldValidationColorChanged(true, "txtb_other_ded_loan10"); txtb_other_ded_loan10.Focus() ; validatedSaved  = false; }


            return validatedSaved;
        }


        //**********************************************************************************************
        //  BEGIN - JOSEPH- 03/20/2019 - Change/Toggle Mode for Object Appearance during validation  
        //**********************************************************************************************
        protected void FieldValidationColorChanged(bool pMode, string pObjectName)
        {
            if (pMode)
                switch (pObjectName)
                {
                    case "ddl_empl_id":
                        {
                            LblRequired1.Text = MyCmn.CONST_RQDFLD;
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }
                    case "already-exist":
                        {
                            LblRequired1.Text = "already-exist";
                            ddl_empl_id.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_other_amount1":
                        {
                            LblRequired10.Text = MyCmn.CONST_RQDFLD;
                            txtb_other_amount1.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_other_amount1":
                        {
                            LblRequired10.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount1.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_other_amount2":
                        {
                            LblRequired11.Text = MyCmn.CONST_RQDFLD;
                            txtb_other_amount2.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_other_amount2":
                        {
                            LblRequired11.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount2.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_other_amount3":
                        {
                            LblRequired12.Text = MyCmn.CONST_RQDFLD;
                            txtb_other_amount3.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_other_amount3":
                        {
                            LblRequired12.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount3.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_other_amount4":
                        {
                            LblRequired13.Text = MyCmn.CONST_RQDFLD;
                            txtb_other_amount4.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_other_amount4":
                        {
                            LblRequired13.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount4.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_other_amount5":
                        {
                            LblRequired14.Text = MyCmn.CONST_RQDFLD;
                            txtb_other_amount5.BorderColor = Color.Red;
                            break;
                        }

                    case "invalid-txtb_other_amount5":
                        {
                            LblRequired14.Text = MyCmn.CONST_INVALID_NUMERIC;
                            txtb_other_amount5.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_voucher_nbr":
                        {
                            LblRequired22.Text = MyCmn.CONST_RQDFLD;
                            txtb_voucher_nbr.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_reason":
                        {
                            LblRequired201.Text = MyCmn.CONST_RQDFLD;
                            txtb_reason.BorderColor = Color.Red;
                            break;
                        }

                    case "txtb_net_pay-negative":
                        {
                            LblRequired8.Text = "Negative Value !";
                            txtb_net_pay.BorderColor = Color.Red;
                            break;
                        }
                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_mand1": { req_other_ded_mand1.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand1.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand2": { req_other_ded_mand2.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand2.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand3": { req_other_ded_mand3.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand3.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand4": { req_other_ded_mand4.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand4.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand5": { req_other_ded_mand5.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand5.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand6": { req_other_ded_mand6.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand6.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand7": { req_other_ded_mand7.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand7.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand8": { req_other_ded_mand8.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand8.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand9": { req_other_ded_mand9.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand9.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_mand10": { req_other_ded_mand10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_mand10.BorderColor = Color.Red; break; }

                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_prem1": { req_other_ded_prem1.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem1.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem2": { req_other_ded_prem2.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem2.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem3": { req_other_ded_prem3.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem3.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem4": { req_other_ded_prem4.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem4.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem5": { req_other_ded_prem5.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem5.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem6": { req_other_ded_prem6.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem6.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem7": { req_other_ded_prem7.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem7.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem8": { req_other_ded_prem8.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem8.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem9": { req_other_ded_prem9.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem9.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_prem10": { req_other_ded_prem10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_prem10.BorderColor = Color.Red; break; }

                    // MANDATORY DEDUCTION VALIDATIONS
                    case "txtb_other_ded_loan1": { req_other_ded_loan1.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan1.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan2": { req_other_ded_loan2.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan2.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan3": { req_other_ded_loan3.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan3.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan4": { req_other_ded_loan4.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan4.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan5": { req_other_ded_loan5.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan5.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan6": { req_other_ded_loan6.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan6.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan7": { req_other_ded_loan7.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan7.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan8": { req_other_ded_loan8.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan8.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan9": { req_other_ded_loan9.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan9.BorderColor = Color.Red; break; }
                    case "txtb_other_ded_loan10": { req_other_ded_loan10.Text = MyCmn.CONST_INVALID_NUMERIC; txtb_other_ded_loan10.BorderColor = Color.Red; break; }

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

                            LblRequired22.Text = "";
                            LblRequired201.Text = "";

                            txtb_voucher_nbr.BorderColor = Color.LightGray;
                            txtb_reason.BorderColor = Color.LightGray;

                            txtb_other_amount1.BorderColor = Color.LightGray;
                            txtb_other_amount2.BorderColor = Color.LightGray;
                            txtb_other_amount3.BorderColor = Color.LightGray;
                            txtb_other_amount4.BorderColor = Color.LightGray;
                            txtb_other_amount5.BorderColor = Color.LightGray;

                            ddl_empl_id.BorderColor = Color.LightGray;
                            ddl_generic_notes.BorderColor = Color.LightGray;
                            txtb_gross_pay.BorderColor = Color.LightGray;
                            txtb_net_pay.BorderColor = Color.LightGray;
                            txtb_other_amount.BorderColor = Color.LightGray;
                            txtb_daily_rate.BorderColor = Color.LightGray;
                            txtb_hourly_rate.BorderColor = Color.LightGray;
                            txtb_monthly_rate.BorderColor = Color.LightGray;
                            
                            txtb_other_ded_mand1.BorderColor = Color.LightGray;
                            txtb_other_ded_mand2.BorderColor = Color.LightGray;
                            txtb_other_ded_mand3.BorderColor = Color.LightGray;
                            txtb_other_ded_mand4.BorderColor = Color.LightGray;
                            txtb_other_ded_mand5.BorderColor = Color.LightGray;
                            txtb_other_ded_mand6.BorderColor = Color.LightGray;
                            txtb_other_ded_mand7.BorderColor = Color.LightGray;
                            txtb_other_ded_mand8.BorderColor = Color.LightGray;
                            txtb_other_ded_mand9.BorderColor = Color.LightGray;
                            txtb_other_ded_mand10.BorderColor = Color.LightGray;

                            txtb_other_ded_prem1.BorderColor = Color.LightGray;
                            txtb_other_ded_prem2.BorderColor = Color.LightGray;
                            txtb_other_ded_prem3.BorderColor = Color.LightGray;
                            txtb_other_ded_prem4.BorderColor = Color.LightGray;
                            txtb_other_ded_prem5.BorderColor = Color.LightGray;
                            txtb_other_ded_prem6.BorderColor = Color.LightGray;
                            txtb_other_ded_prem7.BorderColor = Color.LightGray;
                            txtb_other_ded_prem8.BorderColor = Color.LightGray;
                            txtb_other_ded_prem9.BorderColor = Color.LightGray;
                            txtb_other_ded_prem10.BorderColor = Color.LightGray;

                            txtb_other_ded_loan1.BorderColor = Color.LightGray;
                            txtb_other_ded_loan2.BorderColor = Color.LightGray;
                            txtb_other_ded_loan3.BorderColor = Color.LightGray;
                            txtb_other_ded_loan4.BorderColor = Color.LightGray;
                            txtb_other_ded_loan5.BorderColor = Color.LightGray;
                            txtb_other_ded_loan6.BorderColor = Color.LightGray;
                            txtb_other_ded_loan7.BorderColor = Color.LightGray;
                            txtb_other_ded_loan8.BorderColor = Color.LightGray;
                            txtb_other_ded_loan9.BorderColor = Color.LightGray;
                            txtb_other_ded_loan10.BorderColor = Color.LightGray;

                            break;
                        }

                }
            }
        }
        protected void ddl_empl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_empl_type.SelectedValue != "" && ddl_payroll_group.SelectedValue.ToString().Trim() != "" && ddl_year.SelectedValue.ToString().Trim() != "" && ddl_month.SelectedValue != "" && ddl_payroll_template.SelectedValue != "")
            {

                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }

            RetrieveDataListGrid();
            RetrieveEmployeename();
            UpdatePanel10.Update();
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveEmployeename();
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            RetrieveDataListGrid();
        }

        protected void ddl_empl_id_TextChanged(object sender, EventArgs e)
        {
            if (ddl_empl_id.SelectedValue.ToString().Trim() != "")
            {
                DataRow[] selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");
                header_details();
                ToogleByTemplate();
                calculate_gross_net();
                CalculateGrossNetByTemplate();
            }
            else
            {
                ClearEntry();

            }

        }

       

        private string header_details()
        {

            string rate_basis = "";

            DataRow[] selected_employee = null;
            if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");
            }

            else if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_EDIT)
            {
                selected_employee = dataList_employee.Select("empl_id='" + txtb_empl_id.Text.ToString().Trim() + "'");
            }


            rate_basis                  = selected_employee[0]["rate_basis"].ToString();
            lbl_rate_basis_descr.Text   = selected_employee[0]["rate_basis_descr"].ToString() + " Rate :";
            txtb_empl_id.Text           = selected_employee[0]["empl_id"].ToString();
            txtb_position.Text          = selected_employee[0]["position_title1"].ToString();
            txtb_department_descr.Text  = selected_employee[0]["department_name1"].ToString();
            txtb_monthly_rate.Text      = selected_employee[0]["monthly_rate"].ToString();
            txtb_hourly_rate.Text       = selected_employee[0]["hourly_rate"].ToString();
            txtb_daily_rate.Text        = selected_employee[0]["daily_rate"].ToString();
            hidden_rate_basis.Value     = selected_employee[0]["rate_basis"].ToString();
            
                switch (selected_employee[0]["rate_basis"].ToString())
            {
                case "M":
                    {
                        div_monthly.Visible = false;
                        div_daily.Visible = false;
                        div_hourly.Visible = false;

                        div_monthly.Visible = true;
                        break;
                    }
                case "D":
                    {
                        div_monthly.Visible = false;
                        div_daily.Visible = false;
                        div_hourly.Visible = false;

                        div_daily.Visible = true;


                        break;
                    }
                case "H":
                    {
                        div_monthly.Visible = false;
                        div_daily.Visible = false;
                        div_hourly.Visible = false;

                        div_hourly.Visible = true;
                        break;
                    }
                default:
                    {
                        div_monthly.Visible = false;
                        div_daily.Visible = false;
                        div_hourly.Visible = false;
                        break;
                    }
            }
            return rate_basis;
        }

       

       

        private void calculate_gross_net()
        {
            decimal net_pay     = 0;
            //decimal gross_pay   = 0;
            decimal field_1_amt = 0;
            decimal field_2_amt = 0;
            decimal field_3_amt = 0;
            decimal field_4_amt = 0;
            decimal field_5_amt = 0;
            decimal deductions  = 0;
            if (IsDataValidated())
            {
                if (txtb_other_amount1.Visible == true)
                {
                    field_1_amt = Convert.ToDecimal(txtb_other_amount1.Text);



                    if (dtListField.Rows[0]["fld1_type"].ToString().Trim() == "A")
                    {
                        net_pay += field_1_amt;
                    }

                    else if (dtListField.Rows[0]["fld1_type"].ToString().Trim() == "D")
                    {
                        deductions += field_1_amt;
                    }



                }

                if (txtb_other_amount2.Visible == true)
                {
                    field_2_amt = Convert.ToDecimal(txtb_other_amount2.Text);

                    if (dtListField.Rows[0]["fld2_type"].ToString().Trim() == "A")
                    {
                        net_pay += field_2_amt;
                    }

                    else if (dtListField.Rows[0]["fld2_type"].ToString().Trim() == "D")
                    {
                        deductions += field_2_amt;
                    }


                }

                if (txtb_other_amount3.Visible == true)
                {
                    field_3_amt = Convert.ToDecimal(txtb_other_amount3.Text);

                    if (dtListField.Rows[0]["fld3_type"].ToString().Trim() == "A")
                    {
                        net_pay += field_3_amt;
                    }

                    else if (dtListField.Rows[0]["fld3_type"].ToString().Trim() == "D")
                    {
                        deductions += field_3_amt;
                    }


                }

                if (txtb_other_amount4.Visible == true)
                {
                    field_4_amt = Convert.ToDecimal(txtb_other_amount4.Text);

                    if (dtListField.Rows[0]["fld4_type"].ToString().Trim() == "A")
                    {
                        net_pay += field_4_amt;
                    }

                    else if (dtListField.Rows[0]["fld4_type"].ToString().Trim() == "D")
                    {
                        deductions += field_4_amt;
                    }


                }

                if (txtb_other_amount5.Visible == true)
                {
                    field_5_amt = Convert.ToDecimal(txtb_other_amount5.Text);

                    if (dtListField.Rows[0]["fld5_type"].ToString().Trim() == "A")
                    {
                        net_pay += field_5_amt;
                    }

                    else if (dtListField.Rows[0]["fld5_type"].ToString().Trim() == "D")
                    {
                        deductions += field_5_amt;
                    }


                }

                txtb_gross_pay.Text = (net_pay).ToString("#,##0.00");
                txtb_net_pay.Text = (net_pay - deductions).ToString("#,##0.00");

                if (ddl_payroll_template.SelectedValue == "968" ||  // ONE COVID-19 ALLOWANCE (OCA) - RE
                    ddl_payroll_template.SelectedValue == "969" ||  // ONE COVID-19 ALLOWANCE (OCA) - CE
                    ddl_payroll_template.SelectedValue == "970" ||  // ONE COVID-19 ALLOWANCE (OCA) - JO

                    ddl_payroll_template.SelectedValue == "974" ||  // Health Emergency Allowance (HEA) - RE
                    ddl_payroll_template.SelectedValue == "975" ||  // Health Emergency Allowance (HEA) - CE
                    ddl_payroll_template.SelectedValue == "976"     // Health Emergency Allowance (HEA) - JO
                )
                {
                    txtb_net_pay.Text = "0";
                }
                
                if (double.Parse(txtb_net_pay.Text) < 0)
                {
                    FieldValidationColorChanged(true, "txtb_net_pay-negative");
                    txtb_net_pay.Focus();
                    return;
                }
            }
        }

        protected void btnCal_Click(object sender, EventArgs e)
        {
            if (IsDataValidated())
            {
                calculate_gross_net();
                CalculateGrossNetByTemplate();
            }
        }

        //**************************************************************************
        //  BEGIN - VJA- 09/12/2018 - Toogle All Textbox 
        //**************************************************************************
        private void ToogleTextbox(bool ifenable)
        {
            txtb_other_amount1.Enabled = ifenable;
            txtb_other_amount2.Enabled = ifenable;
            txtb_other_amount3.Enabled = ifenable;
            txtb_other_amount4.Enabled = ifenable;
            txtb_other_amount5.Enabled = ifenable;
            txtb_memo.Enabled          = ifenable;
            
            txtb_other_ded_mand1.Enabled      = ifenable;
            txtb_other_ded_mand2.Enabled      = ifenable;
            txtb_other_ded_mand3.Enabled      = ifenable;
            txtb_other_ded_mand4.Enabled      = ifenable;
            txtb_other_ded_mand5.Enabled      = ifenable;
            txtb_other_ded_mand6.Enabled      = ifenable;
            txtb_other_ded_mand7.Enabled      = ifenable;
            txtb_other_ded_mand8.Enabled      = ifenable;
            txtb_other_ded_mand9.Enabled      = ifenable;
            txtb_other_ded_mand10.Enabled     = ifenable;
            txtb_other_ded_prem1.Enabled      = ifenable;
            txtb_other_ded_prem2.Enabled      = ifenable;
            txtb_other_ded_prem3.Enabled      = ifenable;
            txtb_other_ded_prem4.Enabled      = ifenable;
            txtb_other_ded_prem5.Enabled      = ifenable;
            txtb_other_ded_prem6.Enabled      = ifenable;
            txtb_other_ded_prem7.Enabled      = ifenable;
            txtb_other_ded_prem8.Enabled      = ifenable;
            txtb_other_ded_prem9.Enabled      = ifenable;
            txtb_other_ded_prem10.Enabled     = ifenable;
            txtb_other_ded_loan1.Enabled      = ifenable;
            txtb_other_ded_loan2.Enabled      = ifenable;
            txtb_other_ded_loan3.Enabled      = ifenable;
            txtb_other_ded_loan4.Enabled      = ifenable;
            txtb_other_ded_loan5.Enabled      = ifenable;
            txtb_other_ded_loan6.Enabled      = ifenable;
            txtb_other_ded_loan7.Enabled      = ifenable;
            txtb_other_ded_loan8.Enabled      = ifenable;
            txtb_other_ded_loan9.Enabled      = ifenable;
            txtb_other_ded_loan10.Enabled     = ifenable;

        }
        //**************************************************************************
        //  BEGIN - VJA- 2020-12-28 - Toogle Textboxes and Labels by Template
        //                          1.) Special Risk Allowance 
        //**************************************************************************
        private void ToogleByTemplate()
        {
            if (Lbl_field_1.Text.Length >= 20)
            {
                Lbl_field_1.Font.Size = 9;
                Lbl_field_1.Font.Bold = true;
            }
            if (Lbl_field_2.Text.Length >= 20)
            {
                Lbl_field_2.Font.Size = 9;
                Lbl_field_2.Font.Bold = true;
            }
            if (Lbl_field_3.Text.Length >= 20)
            {
                Lbl_field_3.Font.Size = 9;
                Lbl_field_3.Font.Bold = true;
            }
            if (Lbl_field_4.Text.Length >= 20)
            {
                Lbl_field_4.Font.Size = 9;
                Lbl_field_4.Font.Bold = true;
            }
            if (Lbl_field_5.Text.Length >= 20)
            {
                Lbl_field_5.Font.Size = 9;
                Lbl_field_5.Font.Bold = true;
            }

            if (ddl_payroll_template.SelectedValue == "922" ||  // Special Risk Allowance - RE
                ddl_payroll_template.SelectedValue == "932" ||  // Special Risk Allowance - CE
                ddl_payroll_template.SelectedValue == "942")    // Special Risk Allowance - JO
            {


                div_monthly.Visible = true;
                div_daily.Visible = true;
                div_hourly.Visible = true;
                lbl_rate_basis_descr.Text = "Monthly Rate:";

                txtb_hourly_rate.Enabled = true;
                txtb_daily_rate.Enabled = true;
                txtb_monthly_rate.Enabled = true;
            }
            else if (ddl_payroll_template.SelectedValue == "939" ||   // Performance Based Bonus 2020 - CE
                     ddl_payroll_template.SelectedValue == "929" ||   // Performance Based Bonus 2020 - RE
                     ddl_payroll_template.SelectedValue == "971" ||   // Performance Based Bonus 2021 - RE
                     ddl_payroll_template.SelectedValue == "972"      // Performance Based Bonus 2021 - CE
                     )
            {
                div_monthly.Visible = true;
                div_daily.Visible   = false;
                div_hourly.Visible  = false;
                lbl_rate_basis_descr.Text = "Monthly Rate:";
            }
            else if (ddl_payroll_template.SelectedValue == "962" ||  // Service Recognition Incentives (SRI) - RE
                     ddl_payroll_template.SelectedValue == "957")    // Service Recognition Incentives (SRI) - CE
            {
                div_remarks.Visible = true;
                lbl_remarks.Text    = "Performance Rating:";
            }
            else if (ddl_payroll_template.SelectedValue == "981")
            {
                div_generic_notes.Visible = false;
                div_remarks.Visible       = true;
                lbl_remarks.Text          = "Rate Percentage:";
            }
        }
        //**************************************************************************
        //  BEGIN - VJA- 2020-12-28 - Calculate Gross and Net for Special Risk Allowance 
        //**************************************************************************
        private void CalculateGrossNetByTemplate()
        {
            DataRow[] selected_employee = null;

            double tax_rate = 0;
            //double tax1 = 0;
            //double tax2 = 0;
            //double tax5 = 0;
            //double tax8 = 0;
            //double tax10 = 0;
            //double tax15 = 0;

            //double txtb_tax1  = 0;
            //double txtb_tax2  = 0;
            //double txtb_tax5  = 0;
            //double txtb_tax8  = 0;
            //double txtb_tax10 = 0;
            //double txtb_tax15 = 0;

            if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
            {
                selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");
                tax_rate = double.Parse(selected_employee[0]["tax_rate"].ToString());

                //tax1     = double.Parse(selected_employee[0]["wtax_1perc"].ToString());
                //tax2     = double.Parse(selected_employee[0]["wtax_2perc"].ToString());
                //tax5     = double.Parse(selected_employee[0]["wtax_5perc"].ToString());
                //tax8     = double.Parse(selected_employee[0]["wtax_8perc"].ToString());
                //tax10    = double.Parse(selected_employee[0]["wtax_10perc"].ToString());
                //tax15    = double.Parse(selected_employee[0]["wtax_15perc"].ToString());
            }

            if (ddl_payroll_template.SelectedValue == "922" ||  // Special Risk Allowance I - RE
                ddl_payroll_template.SelectedValue == "932" ||  // Special Risk Allowance I - CE
                ddl_payroll_template.SelectedValue == "942")    // Special Risk Allowance I - JO
            {
                double amount_1 = 0;
                double gross_pay = 0;
                double net_pay = 0;
                
                DataTable dt_setup  = MyCmn.RetrieveData("sp_payrollinstallation_SRA_tbl_list", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim());
               
                for (int i = 0; i < dt_setup.Rows.Count; i++)
                {
                    if (double.Parse(txtb_other_amount2.Text) >= double.Parse(dt_setup.Rows[i]["lower_limit"].ToString().Trim()) &&
                        double.Parse(txtb_other_amount2.Text) <= double.Parse(dt_setup.Rows[i]["upper_limit"].ToString().Trim())
                        )
                    {
                        txtb_other_amount3.Text = dt_setup.Rows[i]["incentive_perc"].ToString().Trim();
                        break;
                    }
                }
                amount_1    = double.Parse(txtb_monthly_rate.Text) * (double.Parse(dt_setup.Rows[0]["perc_monthly_rate"].ToString().Trim()) / 100);
                gross_pay   = double.Parse(txtb_other_amount1.Text) * (double.Parse(txtb_other_amount3.Text) / 100);
                net_pay     = double.Parse(txtb_other_amount1.Text) * (double.Parse(txtb_other_amount3.Text) / 100);
                
                txtb_other_amount1.Text = amount_1.ToString("###,##0.00");
                txtb_gross_pay.Text     = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text       = net_pay.ToString("###,##0.00");
            }

            if (ddl_payroll_template.SelectedValue == "923" ||  // Special Risk Allowance II - RE
                ddl_payroll_template.SelectedValue == "933" ||  // Special Risk Allowance II - CE
                ddl_payroll_template.SelectedValue == "943")    // Special Risk Allowance II - JO
            {
                double amount_1     = 0;
                double gross_pay    = 0;
                double net_pay      = 0;
                
                double sra_common_amount    = 5000;
                double sra_common_days      = 22;

                amount_1    = sra_common_amount / sra_common_days;
                gross_pay   = amount_1 * (double.Parse(txtb_other_amount2.Text));
                net_pay     = amount_1 * (double.Parse(txtb_other_amount2.Text));

                if (net_pay >= sra_common_amount)
                {
                    gross_pay   = sra_common_amount;
                    net_pay     = sra_common_amount;
                }

                txtb_other_amount1.Text = amount_1.ToString("###,##0.00");
                txtb_gross_pay.Text     = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text       = net_pay.ToString("###,##0.00");
            }


            if (ddl_payroll_template.SelectedValue == "924" ||  // COVID-19 Hazard Pay - RE
                ddl_payroll_template.SelectedValue == "934" ||  // COVID-19 Hazard Pay - CE
                ddl_payroll_template.SelectedValue == "944")    // COVID-19 Hazard Pay - JO
            {
                double amount_1     = 0;
                double gross_pay    = 0;
                double net_pay      = 0;
                
                double sra_common_amount    = 3000;
                double sra_common_days      = 22;

                amount_1    = sra_common_amount / sra_common_days;
                gross_pay   = amount_1 * (double.Parse(txtb_other_amount2.Text));
                net_pay     = amount_1 * (double.Parse(txtb_other_amount2.Text));
                
                if (net_pay >= sra_common_amount)
                {
                    gross_pay   = sra_common_amount;
                    net_pay     = sra_common_amount;
                }

                txtb_other_amount1.Text = amount_1.ToString("###,##0.00");
                txtb_gross_pay.Text     = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text       = net_pay.ToString("###,##0.00");
            }

            if (ddl_payroll_template.SelectedValue == "939" ||  // Performance Based Bonus 2020 - CE
                ddl_payroll_template.SelectedValue == "929" )   // Performance Based Bonus 2020 - RE
            {
                double gross_pay    = 0;
                double net_pay      = 0;
                double amount_3     = 0;
                double amount_4     = 0;
                double amount_5     = 0;

                amount_3 = double.Parse(txtb_monthly_rate.Text)  * double.Parse(txtb_other_amount1.Text) * (double.Parse(txtb_other_amount2.Text) / 100);
                amount_4 = amount_3 / 2;
                amount_4 = amount_4 - (amount_4 % 1);
                amount_5 = amount_3 / 2;
                amount_5 = amount_5 + (amount_5 % 1); 

                txtb_other_amount3.Text = amount_3.ToString("###,##0.00");
                txtb_other_amount4.Text = amount_4.ToString("###,##0.00");
                txtb_other_amount5.Text = amount_5.ToString("###,##0.00");

                gross_pay  = amount_5;
                net_pay    = amount_5;

                if (ddl_year.SelectedValue.ToString()  == "2020" && 
                    ddl_month.SelectedValue.ToString() == "12")
                {
                    gross_pay  = amount_4;
                    net_pay    = amount_4;
                }

                txtb_gross_pay.Text     = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text       = net_pay.ToString("###,##0.00");
            }
            
            if (ddl_payroll_template.SelectedValue == "963" ||  // Hazard, Subsistence and Laundry Differential
                ddl_payroll_template.SelectedValue == "958")    // Hazard, Subsistence and Laundry Differential
            {
                double gross_pay    = 0;
                double net_pay      = 0;

                double amount_1     = double.Parse(txtb_other_amount1.Text);
                double amount_2     = double.Parse(txtb_other_amount2.Text);
                double amount_3     = double.Parse(txtb_other_amount3.Text);
                double amount_4     = double.Parse(txtb_other_amount4.Text);
                double amount_5     = double.Parse(txtb_other_amount5.Text);

                gross_pay       = (amount_1 - amount_2) * (amount_3 / 100);
                // amount_5        = gross_pay * (amount_5 / 100);

                if (txtb_department_code.Text == "12" ||
                    txtb_department_descr.Text == "PROVINCIAL SOCIAL, WELFARE & DEV'T. OFFICE") // -- PSWDO
                {
                    gross_pay   = (amount_1 - amount_2) * (amount_3 / 100) / 22 * amount_4;
                }

                net_pay                 = gross_pay - amount_5;

                txtb_gross_pay.Text     = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text       = net_pay.ToString("###,##0.00");
            }
            if (ddl_payroll_template.SelectedValue == "964" ||  // Meals, Allowance and Tranportation Benefits - re
                ddl_payroll_template.SelectedValue == "959" ||  // Meals, Allowance and Tranportation Benefits - ce
                ddl_payroll_template.SelectedValue == "947"     // Meals, Allowance and Tranportation Benefits - jo
                )    
            {
                double gross_pay    = 0;
                double net_pay      = 0;

                double amount_1     = double.Parse(txtb_other_amount1.Text);
                double amount_2     = double.Parse(txtb_other_amount2.Text);
                double amount_3     = double.Parse(txtb_other_amount3.Text);
                double amount_4     = double.Parse(txtb_other_amount4.Text);
                double amount_5     = double.Parse(txtb_other_amount5.Text);

                gross_pay           = amount_2;
                net_pay             = gross_pay - amount_5;

                txtb_gross_pay.Text     = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text       = net_pay.ToString("###,##0.00");
            }

            if (ddl_payroll_template.SelectedValue == "968" ||  // ONE COVID-19 ALLOWANCE (OCA) - RE
                ddl_payroll_template.SelectedValue == "969" ||  // ONE COVID-19 ALLOWANCE (OCA) - CE
                ddl_payroll_template.SelectedValue == "970" ||  // ONE COVID-19 ALLOWANCE (OCA) - JO

                ddl_payroll_template.SelectedValue == "974" ||  // Health Emergency Allowance (HEA) - RE
                ddl_payroll_template.SelectedValue == "975" ||  // Health Emergency Allowance (HEA) - CE
                ddl_payroll_template.SelectedValue == "976"     // Health Emergency Allowance (HEA) - JO
                )    
            {
                double gross_pay    = 0;
                double net_pay      = 0;

                double amount_1 = 0;
                double amount_2 = 0;
                double amount_3 = 0;
                double amount_4 = 0;

                // amount_1     = ((double.Parse(txtb_other_amount1.Text) > 96 ? 96 : double.Parse(txtb_other_amount1.Text)) / 96) * 9000;
                // amount_2     = ((double.Parse(txtb_other_amount2.Text) > 96 ? 96 : double.Parse(txtb_other_amount2.Text)) / 96) * 6000;
                // amount_3     = ((double.Parse(txtb_other_amount3.Text) > 96 ? 96 : double.Parse(txtb_other_amount3.Text)) / 96) * 3000;

                var high        = CommonCode.Truncate(double.Parse(txtb_other_amount1.Text) / 96, 11) * 9000;
                var moderate    = CommonCode.Truncate(double.Parse(txtb_other_amount2.Text) / 96, 11) * 6000;
                var low         = CommonCode.Truncate(double.Parse(txtb_other_amount3.Text) / 96, 11) * 3000;

                amount_1 = (double.Parse(txtb_other_amount1.Text) > 96 ? 9000 : high)     ;
                amount_2 = (double.Parse(txtb_other_amount2.Text) > 96 ? 6000 : moderate) ;
                amount_3 = (double.Parse(txtb_other_amount3.Text) > 96 ? 3000 : low)      ;

                gross_pay = (amount_1 + amount_2 + amount_3);

                if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
                {
                    if (ddl_empl_type.SelectedValue.ToString().Trim() == "RE" ||
                        ddl_empl_type.SelectedValue.ToString().Trim() == "CE")
                    {
                        amount_4 = gross_pay * (tax_rate / 100);
                        txtb_other_amount4.Text = amount_4.ToString("###,##0.00");
                    }
                    else if (ddl_empl_type.SelectedValue.ToString().Trim() == "JO")
                    {
                        txtb_gross_pay.Text = gross_pay.ToString("###,##0.00");
                        Calculate_Taxes();
                        amount_4 = double.Parse(txtb_other_ded_mand1.Text) 
                                 + double.Parse(txtb_other_ded_mand2.Text) 
                                 + double.Parse(txtb_other_ded_mand3.Text) 
                                 + double.Parse(txtb_other_ded_mand4.Text) 
                                 + double.Parse(txtb_other_ded_mand5.Text) 
                                 + double.Parse(txtb_other_ded_mand6.Text);
                        txtb_other_amount4.Text = amount_4.ToString("###,##0.00");
                    }
                }
                else
                {
                    if (ddl_empl_type.SelectedValue.ToString().Trim() == "RE" ||
                        ddl_empl_type.SelectedValue.ToString().Trim() == "CE")
                    {

                    }
                    else if (ddl_empl_type.SelectedValue.ToString().Trim() == "JO")
                    {
                        txtb_other_amount4.Text =  ( double.Parse(txtb_other_ded_mand1.Text)
                                                   + double.Parse(txtb_other_ded_mand2.Text)
                                                   + double.Parse(txtb_other_ded_mand3.Text)
                                                   + double.Parse(txtb_other_ded_mand4.Text)
                                                   + double.Parse(txtb_other_ded_mand5.Text)
                                                   + double.Parse(txtb_other_ded_mand6.Text)).ToString("###,##0.00");
                    }

                }

                net_pay                 = gross_pay - double.Parse(txtb_other_amount4.Text);
                txtb_gross_pay.Text     = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text       = net_pay.ToString("###,##0.00");
            }

            if (ddl_payroll_template.SelectedValue  == "971" ||  // Performance Based Bonus 2021 - RE
                 ddl_payroll_template.SelectedValue == "972")    // Performance Based Bonus 2021 - CE
            {
                double gross_pay = 0;
                double net_pay = 0;
                double amount_3 = 0;
               
                amount_3 = double.Parse(txtb_monthly_rate.Text) * double.Parse(txtb_other_amount1.Text) * (double.Parse(txtb_other_amount2.Text) / 100);
                
                txtb_other_amount3.Text = amount_3.ToString("###,##0.00");
                
                gross_pay   = amount_3;
                net_pay     = amount_3;
                
                txtb_gross_pay.Text = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text = net_pay.ToString("###,##0.00");
            }
            if (ddl_payroll_template.SelectedValue == "981")
            {
                double gross_pay = 0;
                double net_pay   = 0;

                if (ViewState["AddEdit_Mode"].ToString() == MyCmn.CONST_ADD)
                {
                    DataTable dt_rata_dif = new DataTable();
                    DataRow[] selected = null;
                    dt_rata_dif = MyCmn.RetrieveData("sp_personnelnames_rata_diff", "par_payroll_year", ddl_year.SelectedValue.ToString().Trim(), "par_payroll_month", ddl_month.SelectedValue.ToString().Trim(), "par_employment_type", ddl_empl_type.SelectedValue.ToString().Trim(), "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim(), "par_payroll_group_nbr", GetRegistry_NBR());
                    selected    = dt_rata_dif.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

                    txtb_other_amount1.Text = double.Parse(selected[0]["fld1_fixed_amt"].ToString()).ToString("###,##0.00");
                    txtb_other_amount2.Text = double.Parse(selected[0]["fld2_fixed_amt"].ToString()).ToString("###,##0.00");
                    txtb_other_amount3.Text = double.Parse(selected[0]["fld3_fixed_amt"].ToString()).ToString("###,##0.00");
                    txtb_other_amount4.Text = double.Parse(selected[0]["fld4_fixed_amt"].ToString()).ToString("###,##0.00");
                    txtb_other_amount5.Text = double.Parse(selected[0]["fld5_fixed_amt"].ToString()).ToString("###,##0.00");
                    txtb_remarks.Text       = selected[0]["remarks"].ToString();
                    gross_pay               = (double.Parse(txtb_other_amount2.Text) - double.Parse(txtb_other_amount3.Text)) + (double.Parse(txtb_other_amount4.Text) - double.Parse(txtb_other_amount5.Text));
                    net_pay                 = gross_pay;
                }
                else
                {
                    gross_pay        = (double.Parse(txtb_other_amount2.Text) - double.Parse(txtb_other_amount3.Text)) + (double.Parse(txtb_other_amount4.Text) - double.Parse(txtb_other_amount5.Text));
                    net_pay          = gross_pay;
                }

                
                txtb_gross_pay.Text = gross_pay.ToString("###,##0.00");
                txtb_net_pay.Text   = net_pay.ToString("###,##0.00");
            }

        }
         //*****************************************************************
        //  BEGIN - VJA- 2022-05-30 - Reserved Deductions Description
        //*****************************************************************
        private void RetrieveReserveDeduction()
        {
            DataTable dt = MyCmn.RetrieveData("sp_payrollregistry_dtl_othded_setup_tbl_list", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString().Trim());

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    // MANDATORY DEDUCTION
                    lbl_other_ded_mand1.Text        = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand1_descr"].ToString() + ":";
                    lbl_other_ded_mand2.Text        = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand2_descr"].ToString() + ":";
                    lbl_other_ded_mand3.Text        = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand3_descr"].ToString() + ":";
                    lbl_other_ded_mand4.Text        = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand4_descr"].ToString() + ":";
                    lbl_other_ded_mand5.Text        = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand5_descr"].ToString() + ":";
                    lbl_other_ded_mand6.Text        = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand6_descr"].ToString() + ":";
                    lbl_other_ded_mand7.Text        = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand7_descr"].ToString() + ":";
                    lbl_other_ded_mand8.Text        = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand8_descr"].ToString() + ":";
                    lbl_other_ded_mand9.Text        = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? ""  : dt.Rows[0]["other_ded_mand9_descr"].ToString() + ":";
                    lbl_other_ded_mand10.Text       = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? ""  : dt.Rows[0]["other_ded_mand10_descr"].ToString() + ":";
                    
                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Text        = dt.Rows[0]["other_ded_prem1_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem1_descr"].ToString() + ":";
                    lbl_other_ded_prem2.Text        = dt.Rows[0]["other_ded_prem2_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem2_descr"].ToString() + ":";
                    lbl_other_ded_prem3.Text        = dt.Rows[0]["other_ded_prem3_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem3_descr"].ToString() + ":";
                    lbl_other_ded_prem4.Text        = dt.Rows[0]["other_ded_prem4_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem4_descr"].ToString() + ":";
                    lbl_other_ded_prem5.Text        = dt.Rows[0]["other_ded_prem5_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem5_descr"].ToString() + ":";
                    lbl_other_ded_prem6.Text        = dt.Rows[0]["other_ded_prem6_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem6_descr"].ToString() + ":";
                    lbl_other_ded_prem7.Text        = dt.Rows[0]["other_ded_prem7_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem7_descr"].ToString() + ":";
                    lbl_other_ded_prem8.Text        = dt.Rows[0]["other_ded_prem8_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem8_descr"].ToString() + ":";
                    lbl_other_ded_prem9.Text        = dt.Rows[0]["other_ded_prem9_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_prem9_descr"].ToString() + ":";
                    lbl_other_ded_prem10.Text       = dt.Rows[0]["other_ded_prem10_descr"].ToString()  == "" ? "" : dt.Rows[0]["other_ded_prem10_descr"].ToString() + ":";
                    
                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Text        = dt.Rows[0]["other_ded_loan1_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan1_descr"].ToString() + ":";
                    lbl_other_ded_loan2.Text        = dt.Rows[0]["other_ded_loan2_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan2_descr"].ToString() + ":";
                    lbl_other_ded_loan3.Text        = dt.Rows[0]["other_ded_loan3_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan3_descr"].ToString() + ":";
                    lbl_other_ded_loan4.Text        = dt.Rows[0]["other_ded_loan4_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan4_descr"].ToString() + ":";
                    lbl_other_ded_loan5.Text        = dt.Rows[0]["other_ded_loan5_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan5_descr"].ToString() + ":";
                    lbl_other_ded_loan6.Text        = dt.Rows[0]["other_ded_loan6_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan6_descr"].ToString() + ":";
                    lbl_other_ded_loan7.Text        = dt.Rows[0]["other_ded_loan7_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan7_descr"].ToString() + ":";
                    lbl_other_ded_loan8.Text        = dt.Rows[0]["other_ded_loan8_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan8_descr"].ToString() + ":";
                    lbl_other_ded_loan9.Text        = dt.Rows[0]["other_ded_loan9_descr"].ToString()   == "" ? "" : dt.Rows[0]["other_ded_loan9_descr"].ToString() + ":";
                    lbl_other_ded_loan10.Text       = dt.Rows[0]["other_ded_loan10_descr"].ToString()  == "" ? "" : dt.Rows[0]["other_ded_loan10_descr"].ToString() + ":";
                    
                    // ***********************************************************************************************************
                    // ***** DO NOT DISPLAY THE TEXTBOXES IF NO DESCRIPTION ******************************************************
                    // ***********************************************************************************************************

                    // MANDATORY DEDUCTION
                    lbl_other_ded_mand1.Visible        = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand2.Visible        = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand3.Visible        = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand4.Visible        = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand5.Visible        = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand6.Visible        = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand7.Visible        = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand8.Visible        = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand9.Visible        = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? false : true;
                    lbl_other_ded_mand10.Visible       = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? false : true;

                    txtb_other_ded_mand1.Visible       = dt.Rows[0]["other_ded_mand1_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand2.Visible       = dt.Rows[0]["other_ded_mand2_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand3.Visible       = dt.Rows[0]["other_ded_mand3_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand4.Visible       = dt.Rows[0]["other_ded_mand4_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand5.Visible       = dt.Rows[0]["other_ded_mand5_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand6.Visible       = dt.Rows[0]["other_ded_mand6_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand7.Visible       = dt.Rows[0]["other_ded_mand7_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand8.Visible       = dt.Rows[0]["other_ded_mand8_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand9.Visible       = dt.Rows[0]["other_ded_mand9_descr"].ToString()   == "" ? false : true;
                    txtb_other_ded_mand10.Visible      = dt.Rows[0]["other_ded_mand10_descr"].ToString()  == "" ? false : true;

                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Visible        = dt.Rows[0]["other_ded_prem1_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem2.Visible        = dt.Rows[0]["other_ded_prem2_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem3.Visible        = dt.Rows[0]["other_ded_prem3_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem4.Visible        = dt.Rows[0]["other_ded_prem4_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem5.Visible        = dt.Rows[0]["other_ded_prem5_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem6.Visible        = dt.Rows[0]["other_ded_prem6_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem7.Visible        = dt.Rows[0]["other_ded_prem7_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem8.Visible        = dt.Rows[0]["other_ded_prem8_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem9.Visible        = dt.Rows[0]["other_ded_prem9_descr"].ToString()    == "" ? false : true;
                    lbl_other_ded_prem10.Visible        = dt.Rows[0]["other_ded_prem10_descr"].ToString()  == "" ? false : true;

                    txtb_other_ded_prem1.Visible     = dt.Rows[0]["other_ded_prem1_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem2.Visible     = dt.Rows[0]["other_ded_prem2_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem3.Visible     = dt.Rows[0]["other_ded_prem3_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem4.Visible     = dt.Rows[0]["other_ded_prem4_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem5.Visible     = dt.Rows[0]["other_ded_prem5_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem6.Visible     = dt.Rows[0]["other_ded_prem6_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem7.Visible     = dt.Rows[0]["other_ded_prem7_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem8.Visible     = dt.Rows[0]["other_ded_prem8_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem9.Visible     = dt.Rows[0]["other_ded_prem9_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_prem10.Visible    = dt.Rows[0]["other_ded_prem10_descr"].ToString()      == "" ? false : true;

                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Visible        = dt.Rows[0]["other_ded_loan1_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan2.Visible        = dt.Rows[0]["other_ded_loan2_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan3.Visible        = dt.Rows[0]["other_ded_loan3_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan4.Visible        = dt.Rows[0]["other_ded_loan4_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan5.Visible        = dt.Rows[0]["other_ded_loan5_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan6.Visible        = dt.Rows[0]["other_ded_loan6_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan7.Visible        = dt.Rows[0]["other_ded_loan7_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan8.Visible        = dt.Rows[0]["other_ded_loan8_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan9.Visible        = dt.Rows[0]["other_ded_loan9_descr"].ToString()     == "" ? false : true;
                    lbl_other_ded_loan10.Visible       = dt.Rows[0]["other_ded_loan10_descr"].ToString()    == "" ? false : true;

                    txtb_other_ded_loan1.Visible     = dt.Rows[0]["other_ded_loan1_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan2.Visible     = dt.Rows[0]["other_ded_loan2_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan3.Visible     = dt.Rows[0]["other_ded_loan3_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan4.Visible     = dt.Rows[0]["other_ded_loan4_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan5.Visible     = dt.Rows[0]["other_ded_loan5_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan6.Visible     = dt.Rows[0]["other_ded_loan6_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan7.Visible     = dt.Rows[0]["other_ded_loan7_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan8.Visible     = dt.Rows[0]["other_ded_loan8_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan9.Visible     = dt.Rows[0]["other_ded_loan9_descr"].ToString()       == "" ? false : true;
                    txtb_other_ded_loan10.Visible    = dt.Rows[0]["other_ded_loan10_descr"].ToString()      == "" ? false : true;
                }
                
            else
            {
                    // MANDATORY DEDUCTION
                    lbl_other_ded_mand1.Visible        = false;
                    lbl_other_ded_mand2.Visible        = false;
                    lbl_other_ded_mand3.Visible        = false;
                    lbl_other_ded_mand4.Visible        = false;
                    lbl_other_ded_mand5.Visible        = false;
                    lbl_other_ded_mand6.Visible        = false;
                    lbl_other_ded_mand7.Visible        = false;
                    lbl_other_ded_mand8.Visible        = false;
                    lbl_other_ded_mand9.Visible        = false;
                    lbl_other_ded_mand10.Visible       = false;

                    txtb_other_ded_mand1.Visible       = false;
                    txtb_other_ded_mand2.Visible       = false;
                    txtb_other_ded_mand3.Visible       = false;
                    txtb_other_ded_mand4.Visible       = false;
                    txtb_other_ded_mand5.Visible       = false;
                    txtb_other_ded_mand6.Visible       = false;
                    txtb_other_ded_mand7.Visible       = false;
                    txtb_other_ded_mand8.Visible       = false;
                    txtb_other_ded_mand9.Visible       = false;
                    txtb_other_ded_mand10.Visible      = false;

                    // OPTIONAL DEDUCTION
                    lbl_other_ded_prem1.Visible        = false;
                    lbl_other_ded_prem2.Visible        = false;
                    lbl_other_ded_prem3.Visible        = false;
                    lbl_other_ded_prem4.Visible        = false;
                    lbl_other_ded_prem5.Visible        = false;
                    lbl_other_ded_prem6.Visible        = false;
                    lbl_other_ded_prem7.Visible        = false;
                    lbl_other_ded_prem8.Visible        = false;
                    lbl_other_ded_prem9.Visible        = false;
                    lbl_other_ded_prem10.Visible       = false;

                    txtb_other_ded_prem1.Visible     = false;
                    txtb_other_ded_prem2.Visible     = false;
                    txtb_other_ded_prem3.Visible     = false;
                    txtb_other_ded_prem4.Visible     = false;
                    txtb_other_ded_prem5.Visible     = false;
                    txtb_other_ded_prem6.Visible     = false;
                    txtb_other_ded_prem7.Visible     = false;
                    txtb_other_ded_prem8.Visible     = false;
                    txtb_other_ded_prem9.Visible     = false;
                    txtb_other_ded_prem10.Visible    = false;

                    // LOAN DEDUCTION
                    lbl_other_ded_loan1.Visible      = false;
                    lbl_other_ded_loan2.Visible      = false;
                    lbl_other_ded_loan3.Visible      = false;
                    lbl_other_ded_loan4.Visible      = false;
                    lbl_other_ded_loan5.Visible      = false;
                    lbl_other_ded_loan6.Visible      = false;
                    lbl_other_ded_loan7.Visible      = false;
                    lbl_other_ded_loan8.Visible      = false;
                    lbl_other_ded_loan9.Visible      = false;
                    lbl_other_ded_loan10.Visible     = false;

                    txtb_other_ded_loan1.Visible     = false;
                    txtb_other_ded_loan2.Visible     = false;
                    txtb_other_ded_loan3.Visible     = false;
                    txtb_other_ded_loan4.Visible     = false;
                    txtb_other_ded_loan5.Visible     = false;
                    txtb_other_ded_loan6.Visible     = false;
                    txtb_other_ded_loan7.Visible     = false;
                    txtb_other_ded_loan8.Visible     = false;
                    txtb_other_ded_loan9.Visible     = false;
                    txtb_other_ded_loan10.Visible    = false;
            }
            }
        }
        
        //************************************************************************************************
        //  BEGIN - VJA- 2022-05-30 - Check, Insert and Update Additional Table for Reserved Dedutions
        //************************************************************************************************
        private void InsertUpdateOtherDeduction()
        {

            DataTable dt = MyCmn.RetrieveData("payrollregistry_dtl_othded_chk", "par_payrolltemplate_code", ddl_payroll_template.SelectedValue.ToString(), "par_payroll_year", ddl_year.SelectedValue.ToString(), "par_payroll_month", ddl_month.SelectedValue.ToString(), "par_payroll_registry_nbr", lbl_registry_number.Text.ToString(), "par_empl_id", txtb_empl_id.Text.ToString(), "par_seq_no", "");
            if (dt != null)
            {
                string insert_update_script = "";

                // UPDATE DATA FROM OTHER DEDUCTIONS
                if (dt.Rows[0]["flag_return"].ToString() == "U")
                {
                    insert_update_script = "update payrollregistry_dtl_othded_tbl set "
                                            + "other_ded_mand1 ="
                                            + txtb_other_ded_mand1.Text.ToString().Trim().Replace(",","")
                                            + ",other_ded_mand2 ="
                                            + txtb_other_ded_mand2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand3 ="
                                            + txtb_other_ded_mand3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand4 ="
                                            + txtb_other_ded_mand4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand5 ="
                                            + txtb_other_ded_mand5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand6 ="
                                            + txtb_other_ded_mand6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand7 ="
                                            + txtb_other_ded_mand7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand8 ="
                                            + txtb_other_ded_mand8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand9 ="
                                            + txtb_other_ded_mand9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_mand10 ="
                                            + txtb_other_ded_mand10.Text.ToString().Trim().Replace(",", "")

                                            + ",other_ded_prem1 ="
                                            + txtb_other_ded_prem1.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem2 ="
                                            + txtb_other_ded_prem2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem3 ="
                                            + txtb_other_ded_prem3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem4 ="
                                            + txtb_other_ded_prem4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem5 ="
                                            + txtb_other_ded_prem5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem6="
                                            + txtb_other_ded_prem6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem7 ="
                                            + txtb_other_ded_prem7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem8 ="
                                            + txtb_other_ded_prem8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem9 ="
                                            + txtb_other_ded_prem9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_prem10 ="
                                            + txtb_other_ded_prem10.Text.ToString().Trim().Replace(",", "")

                                            + ",other_ded_loan1 ="
                                            + txtb_other_ded_loan1.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan2 ="
                                            + txtb_other_ded_loan2.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan3 ="
                                            + txtb_other_ded_loan3.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan4 ="
                                            + txtb_other_ded_loan4.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan5 ="
                                            + txtb_other_ded_loan5.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan6 ="
                                            + txtb_other_ded_loan6.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan7 ="
                                            + txtb_other_ded_loan7.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan8 ="
                                            + txtb_other_ded_loan8.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan9 ="
                                            + txtb_other_ded_loan9.Text.ToString().Trim().Replace(",", "")
                                            + ",other_ded_loan10 ="
                                            + txtb_other_ded_loan10.Text.ToString().Trim().Replace(",", "")

                                            + "where "
                                            + "payrolltemplate_code = '" + ddl_payroll_template.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_year= '" + ddl_year.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_month= '" + ddl_month.SelectedValue.ToString() + "'"
                                            + "AND " + "payroll_registry_nbr= '" + lbl_registry_number.Text.ToString() + "'"
                                            + "AND " + "empl_id= '" + txtb_empl_id.Text + "'";

                    MyCmn.Update_InsertTable(insert_update_script);
                }
                // INSERT DATA TO OTHER DEDUCTIONS
                else if (dt.Rows[0]["flag_return"].ToString() == "I")
                {
                    double total_all = 0;
                    total_all =   double.Parse(txtb_other_ded_mand1.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand2.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand3.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand4.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand5.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand6.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand7.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand8.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand9.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_mand10.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem1.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem2.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem3.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem4.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem5.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem6.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem7.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem8.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem9.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_prem10.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan1.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan2.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan3.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan4.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan5.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan6.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan7.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan8.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan9.Text.ToString().Trim().Replace(",", ""))
                                + double.Parse(txtb_other_ded_loan10.Text.ToString().Trim().Replace(",", ""));

                    if (total_all > 0)
                    {
                        insert_update_script = "insert into payrollregistry_dtl_othded_tbl select "
                                             + "'" + ddl_payroll_template.SelectedValue.ToString() + "'"
                                             + "," + "'" + ddl_year.SelectedValue.ToString() + "'"
                                             + "," + "'" + ddl_month.SelectedValue.ToString() + "'"
                                             + "," + "'" + lbl_registry_number.Text.ToString() + "'"
                                             + "," + "'" + txtb_empl_id.Text + "'"
                                             + "," + txtb_other_ded_mand1.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_mand2.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_mand3.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_mand4.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_mand5.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_mand6.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_mand7.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_mand8.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_mand9.Text.ToString().Trim().Replace(",", "")
                                             + "," + txtb_other_ded_mand10.Text.ToString().Trim().Replace(",", "")
                                             + "," + txtb_other_ded_prem1.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_prem2.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_prem3.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_prem4.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_prem5.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_prem6.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_prem7.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_prem8.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_prem9.Text.ToString().Trim().Replace(",", "")
                                             + "," + txtb_other_ded_prem10.Text.ToString().Trim().Replace(",", "")
                                             + "," + txtb_other_ded_loan1.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_loan2.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_loan3.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_loan4.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_loan5.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_loan6.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_loan7.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_loan8.Text.ToString().Trim().Replace(",","")
                                             + "," + txtb_other_ded_loan9.Text.ToString().Trim().Replace(",", "")
                                             + "," + txtb_other_ded_loan10.Text.ToString().Trim().Replace(",", "")
                                             + "," + "''";

                        MyCmn.Update_InsertTable(insert_update_script);
                    }
                }
                else
                {
                }
            }
        }
        //***********************************************************************************
        //  BEGIN - VJA- 2020-09-26 - Calculate Taxes During Add and Calculate Individually
        //***********************************************************************************
        private void Calculate_Taxes()
        {
            double tax2     = 0;
            double tax3     = 0;
            double tax5     = 0;
            double tax8     = 0;
            double tax10    = 0;
            double tax15    = 0;

            DataRow[] selected_employee = dataList_employee.Select("empl_id='" + ddl_empl_id.SelectedValue.ToString().Trim() + "'");

            if (selected_employee.Length > 0)
            {
                // **************************************************************
                // *** VJA - Compute VAT
                // **************************************************************
                switch (selected_employee[0]["tax_perc"].ToString())
                {
                    case "2":
                        tax2 = tax2 + (double.Parse(txtb_gross_pay.Text)) * .02;
                        break;
                    case "1":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .01;
                        break;
                    case "3":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .03;
                        break;
                    case "5":
                        tax5 = tax5 + (double.Parse(txtb_gross_pay.Text)) * .05;
                        break;
                    case "8":
                        tax8 = tax8 + (double.Parse(txtb_gross_pay.Text)) * .08;
                        break;
                    case "10":
                        tax10 = tax10 + (double.Parse(txtb_gross_pay.Text)) * .10;
                        break;
                    case "15":
                        tax15 = tax15 + (double.Parse(txtb_gross_pay.Text)) * .15;
                        break;
                }

                // **************************************************************
                // *** VJA - Compute VAT
                // **************************************************************

                switch (selected_employee[0]["vat_perc"].ToString())
                {
                    case "2":
                        tax2 = tax2 + (double.Parse(txtb_gross_pay.Text)) * .02;
                        break;
                    case "1":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .01;
                        break;
                    case "3":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .03;
                        break;
                    case "5":
                        tax5 = tax5 + (double.Parse(txtb_gross_pay.Text)) * .05;
                        break;
                    case "8":
                        tax8 = tax8 + (double.Parse(txtb_gross_pay.Text)) * .08;
                        break;
                    case "10":
                        tax10 = tax10 + (double.Parse(txtb_gross_pay.Text)) * .10;
                        break;
                    case "15":
                        tax15 = tax15 + (double.Parse(txtb_gross_pay.Text)) * .15;
                        break;
                }
                // **************************************************************
                // *** VJA - 2022-04-05 Additional Computation for 3 Percentage - Special Case without Sworn       
                // **************************************************************

                switch (selected_employee[0]["additional_vat_perc"].ToString())
                {
                    case "2":
                        tax2 = tax2 + (double.Parse(txtb_gross_pay.Text) ) * .02;
                        break;
                    case "1":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .01;
                        break;
                    case "3":
                        tax3 = tax3 + (double.Parse(txtb_gross_pay.Text)) * .03;
                        break;
                    case "5":
                        tax5 = tax5 + (double.Parse(txtb_gross_pay.Text)) * .05;
                        break;
                    case "8":
                        tax8 = tax8 + (double.Parse(txtb_gross_pay.Text)) * .08;
                        break;
                    case "10":
                        tax10 = tax10 + (double.Parse(txtb_gross_pay.Text)) * .10;
                        break;
                    case "15":
                        tax15 = tax15 + (double.Parse(txtb_gross_pay.Text)) * .15;
                        break;
                }

            }

            txtb_other_ded_mand1.Text = tax3.ToString("###,##0.00");
            txtb_other_ded_mand2.Text = tax2.ToString("###,##0.00");
            txtb_other_ded_mand3.Text = tax5.ToString("###,##0.00");
            txtb_other_ded_mand4.Text = tax8.ToString("###,##0.00");
            txtb_other_ded_mand5.Text = tax10.ToString("###,##0.00");
            txtb_other_ded_mand6.Text = tax15.ToString("###,##0.00");
            
        }
        /* ---------------------- END OF THE CODE------------------------------*/
    }
}