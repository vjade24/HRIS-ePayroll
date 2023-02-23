<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPaySalaryDifferential.aspx.cs" Inherits="HRIS_ePayroll.View.cPaySalaryDifferential" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">

    <style type="text/css">
        .lbl_required {
            font-size: 8px !important;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

        <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>

        <!-- The Modal - Add Confirmation -->
        <div class="modal fade" id="AddEditConfirm">
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center">
                <!-- Modal body -->
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <i class="fa-5x fa fa-check-circle text-success"></i>
                            <h2 >Successfully</h2>
                            <h6><asp:Label ID="SaveAddEdit" runat="server" Text="Save"></asp:Label></h6>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- Modal footer -->
                <div style="margin-bottom:30px">
                </div>
            </div>
            </div>
        </div>

        <asp:UpdatePanel ID="delete_confirm_popup" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="deleteRec">
                    <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content text-center">
                    <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <i class="fa-5x fa fa-question text-danger"></i>
                                        <h2 id="deleteRec0" runat="server">Delete this Record</h2>
                                        <h6><asp:Label ID="deleteRec1" runat="server" Text=""></asp:Label></h6>
                                        <asp:Label ID="lbl_unposting" runat="server" Text=""></asp:Label>
                                        <asp:TextBox runat="server" ID="txtb_reason"  visible="false" CssClass="form-control form-control-sm text-center" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                            <asp:Label ID="LblRequired201" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <div style="margin-bottom:50px">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkBtnYes" runat="server"  CssClass="btn btn-danger" OnCommand="btnDelete_Command"> <i class="fa fa-check"></i> Yes, Delete it </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton3"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>


       <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-lg" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)" >
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="AddEditPage"><asp:Label ID="LabelAddEdit" runat="server" Text="Add/Edit Page" forecolor="White"></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server"  style="padding-top: 5px;">

                             <div class="row" style="display:none">
                                <div class="col-3">
                                    <div style="float:right;width:65%;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_status"  visible="true" CssClass="form-control form-control-sm text-center" Width="100%" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label17" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div> 
                                    <asp:Label runat="server" style="float:left;padding-top:2px;" Text="Status:" CssClass="font-weight-bold" ></asp:Label>
                               </div>
                                <div class="col-2" style="padding-right:0px;padding-top:5px;margin-left:10px;">
                                    <asp:Label runat="server" Text="Date Posted:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" style="padding-right:0px;padding-left:0px; margin-left:-10px;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                           <asp:TextBox runat="server" ID="txtb_date_posted"  CssClass="form-control form-control-sm text-center" MaxLength="15"  Width="100%" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-5">
                                    <div style="float:right;width:55%;">
                                        <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_voucher_nbr" CssClass="form-control form-control-sm text-center" MaxLength="15" Width="100%"></asp:TextBox>
                                            <asp:Label ID="LblRequired101" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                    <asp:Label runat="server" style="float:left;padding-top:2px; margin-right:5px;" MaxLength="15" Text="Voucher Nbr :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                 <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                             </div>

                           
                            <div class="row">
                                <div class="col-12 text-right" style="margin-top:-15px">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_registry_number" runat="server" Text="" Font-Underline="true" CssClass="font-weight-bold" Visible="false"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                     <asp:Label ID="Label2" runat="server" Text="Employee's Name:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-6" style="padding-left:0px;">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                             <asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_id_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:TextBox runat="server" Visible="false" ID="txtb_employeename" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired60"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-1" style="padding-right: 0px;">
                                     <asp:Label ID="Label3" runat="server" Text="ID No:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" >
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server"  ID="txtb_empl_id" CssClass="form-control form-control-sm font-weight-bold text-center"  Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <hr style="margin-top:5px;margin-bottom:5px" />
                            <div class="form-group row">
                                <div class="col-3">
                                     <asp:Label ID="Label21" runat="server" Text="Position Title:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-9" style="padding-left:0px;">
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_position"  CssClass="form-control form-control-sm" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3" style="display:none;">
                                    
                                </div>
                           </div>
                           <hr style="margin-top:5px;margin-bottom:5px" />

                            <div class="row">
                                <div class="col-3">
                                    <a href="#demo" data-toggle="collapse" id="btn_show">Department <small>(Show)</small>:</a>
                                </div>
                                <div class="col-9" style="padding-left:0px;">
                                    <asp:UpdatePanel ID="UpdateDep" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_dep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                                
                            <div class="row collapse" id="demo">
                                
                                <div class="col-3">
                                    <label class="font-weight-bold">Sub-Department:</label>
                                </div>
                                <div class="col-9" style="padding-left:0px;">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_subdep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Division:</label>
                                </div>
                                <div class="col-9" style="padding-left:0px;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_division" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Section:</label>
                                </div>
                                <div class="col-9" style="padding-left:0px;">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3" style="display:none">
                                    <label class="font-weight-bold">Fund Charges :</label>
                                </div>
                                <div class="col-9" style="display:none">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_fund_charges" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row" >

                                <div class="col-12" >
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" class="font-weight-bold" ID="Label5" Text="No. of Months:" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-1" style="padding-left:0px;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_no_of_months" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired1"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-4" style="margin-top:3px;">
                                    <asp:label runat="server" Font-Bold="true" Text="Month Covered:" style="float:left;"></asp:label>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_month_covered" CssClass="form-control form-control-sm text-center" style="float:right;width:45%;margin-top:-3px;"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--<div class="col-1" style="padding-left:0px; margin-top:3px;">
                                </div>--%>

                                <div class="col-2">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" class="font-weight-bold" ID="Label7" Text="LWOP Amt.:" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2" style="padding-left:0px;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_lwop_amount" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired2"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                     <asp:Label runat="server" class="font-weight-bold" ID="Label4" Text="Remarks:" ></asp:Label>
                                </div>
                                <div class="col-5" style="margin-top:3px;padding-left:0px;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_remarks" CssClass="form-control form-control-sm" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                     <asp:Label runat="server" class="font-weight-bold" ID="Label8" Text="Lates Amt.:" ></asp:Label>
                                </div>
                                <div class="col-2" style="margin-top:3px;padding-left:0px;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_lates_amt" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="lbl_txtb_lates_amt"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                               
                            <div class="row">
                                
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-4">
                                    <asp:label runat="server" Font-Bold="true" Text="Summary Totals:"></asp:label>
                                    <div class="form-group row">
                                        
                                        
                                        <div class="col-6" style="padding-right:0px">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" class="font-weight-bold" ID="lbl_rate_descr" Text="New Rate" Font-Size="Small"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_monthly_rate_new" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtb_daily_rate_new" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired3"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-6" style="padding-right:0px">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" class="font-weight-bold" ID="lbl_rate_descr_old" Text="Old Rate:" Font-Size="Small"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_monthly_rate_old" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtb_daily_rate_old" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired5"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                       
                                        
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="form-group row" runat="server" id="div_sal_diff_per_day" style="padding-right:15px;padding-left:15px;margin-bottom: 0px;">
                                                     <div class="col-6">
                                                        <asp:label runat="server" Font-Bold="true" Text="Diff.Amt./Day:"  Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_sal_diff_per_day" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired301"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="form-group row" runat="server" id="div_days_worked"  style="padding-right:15px;padding-left:15px;margin-bottom: 0px;">
                                                    <div class="col-6" >
                                                        <asp:label runat="server" Font-Bold="true" Text="Days Worked:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" >
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_days_worked" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired300"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                        
                                        <div class="col-6"  style="padding-right:0px">
                                            <asp:label runat="server" Font-Bold="true" Text="Differential Amt :"  Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_salary_diff_amt" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Gross Pay :" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_gross_pay" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired4"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-6" style="padding-right:0px">
                                            <asp:label runat="server" Font-Bold="true" Text="Total Deductions :" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_total_deductions" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Net Pay :"  Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_net_pay" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired10"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="form-group row" runat="server" id="div_leave_earned" style="padding-right:15px;padding-left:15px;margin-bottom: 0px;">
                                                    <div class="col-6">
                                                        <asp:label runat="server" Font-Bold="true" Text="Leave Earned:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_leave_earned" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired302"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        

                                        <div class="col-12 text-right" style="margin-top:5px">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Button runat="server" ID="btn_calculate" CssClass="btn btn-warning btn-block btn-sm" Text="Calculate" OnClick="btn_calculate_Click"/>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-8">
                                    <div class="form-group row">
                                        <div class="col-12">
                                            <ul class="nav nav-tabs" style="border-bottom: 1px solid #dee2e6" id="myTab" role="tablist">
                                                <li class="nav-item">
                                                    <a class="nav-link active" style="padding:.09rem 1rem;" id="name_of_employee_link" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab" aria-selected="false">SUMMARY DEDUCTIONS</a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="tab-content" id="myTabContent" style="width:100%;margin-right:15px">
                                            <div class="tab-pane show fade active" id="mandatory_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;" aria-labelledby="name_of_employee_link">
                                                <div class="row" style="margin-top:5px;" >
                                                    <div class="col-6">
                                                        <div class="form-group row">
                                                            <div class="col-6" >
                                                                <asp:label runat="server" Font-Bold="true" Text="GSIS - GS :" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" >
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_gsis_gs" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired13"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                         </div>
                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Bold="true" Text="GSIS - PS :" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_gsis_ps" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired15"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Bold="true" Text="HDMF-GS :" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" >
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_gs" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired17"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Bold="true" Text="HDMF-PS :" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_ps" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired18"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Bold="true" Text="PHIC - GS :" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_phic_gs" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired19"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="form-group row">
                                                            
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Bold="true" Text="PHIC - PS :" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_phic_ps" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired20"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        
                                                        </div>

                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Bold="true"  Text="BIR Tax :" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" >
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_bir_tax" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired21"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 text-right" style="margin-top:5px;"> 
                                           <asp:UpdatePanel runat="server">
                                               <ContentTemplate>
                                                     <asp:Label runat="server" style="float:left; margin-left:-15px;" ID="lbl_if_dateposted_yes" CssClass="col-form-label-sm text-danger" Font-Bold="true" Font-Size="Smaller" Text=""></asp:Label>
                                                    <asp:Label runat="server" ID="lbl_lastday_hidden" Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lbl_installation_monthly_conv_hidden" Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lbl_rate_basis_hidden" Visible="false"></asp:Label>
                                                    <%--<asp:Label runat="server" ID="lbl_lastday_daily_rate_hidden" Visible="false"></asp:Label>--%>
                                                    <asp:Label runat="server" ID="lbl_lastday_hourly_rate_hidden" Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lbl_lwop_pera_hidden" Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lbl_lwop_monthly_hidden" Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lbl_minimum_netpay_hidden" Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lbl_addeditmode_hidden" Visible="false" ></asp:Label>

                                                   <%-- <asp:Label runat="server" ID="lbl_monthly_rate_old_hidden" Visible="false" ></asp:Label>
                                                    <asp:Label runat="server" ID="lbl_daily_rate_old_hidden" Visible="false" ></asp:Label>--%>
                                                            
                                                      
                                                    <asp:LinkButton ID="Linkbtncancel"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                                   <asp:Button ID="btnSave" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                                            </ContentTemplate>
                                           </asp:UpdatePanel>
                                        </div>

                                   </div>
                                </div>
                              </div>
                            </div>
                         </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="notification">
            <!-- The Modal - Message -->
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center">
                <div class="modal-header">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <h5 class="modal-title" ><asp:Label ID="Label6" runat="server" Text=""></asp:Label></h5>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                </div>
                <!-- Modal body -->
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <i id="msg_icon" runat="server" class="fa fa-check-circle-o"></i>
                        <h4 id="msg_header" runat="server">Header Message</h4>
                        <asp:Label ID="lbl_details" runat="server" Text=""></asp:Label>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                <!-- Modal footer -->
                <div style="margin-bottom:30px">
                </div>
            </div>
            </div>
        </div>

     <div class="col-12">
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-3" style="padding-right:0px !important"><strong style="font-family:Arial;font-size:18px;color:white;"><%= ddl_payroll_template.SelectedItem.ToString() %></strong></div>
                <div class="col-9">
                    <asp:UpdatePanel ID="UpdatePanel11" ChildrenAsTriggers="false" UpdateMode="Conditional"  runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtb_search" onInput="search_for(event);" runat="server" class="form-control" placeholder="Search.." Height="30px" 
                                Width="100%" OnTextChanged="txtb_search_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <script type="text/javascript">
                                function search_for(key) {
                                        __doPostBack("<%= txtb_search.ClientID %>", "");
                                }
                            </script>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%--<div class="col-2 text-right">
                   <asp:UpdatePanel runat="server">
                       <ContentTemplate>
                           <asp:Button ID="btn_save_group" runat="server" CssClass="btn btn-success btn-sm add-icon icn"  Text="Save Group"/>
                       </ContentTemplate>
                   </asp:UpdatePanel>
                </div>--%>
            </div>
        </div>
    <div class="row">
        <div class="col-12">
            <table class="table table-bordered  table-scroll">
                <tbody class="my-tbody">
                    <tr>
                        <td>
                            <div class="row">
                                <div class="col-3">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                                <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="25%" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="5" Value="5" />
                                                    <asp:ListItem Text="10" Selected="True" Value="10" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                            <asp:Label runat="server" Text="Entries"></asp:Label>
                                            &nbsp;&nbsp;|&nbsp;&nbsp;
                                                     <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                                
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-6">
                                                <asp:Label runat="server"  Text="Payroll Year:" ></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged1" Enabled="false"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group row">
                                        <div class="col-4">
                                                <asp:Label runat="server" Text="Month:" ></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_month_SelectedIndexChanged" Enabled="false">
                                                        <asp:ListItem Value="01" Text="January"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="February"></asp:ListItem>
                                                        <asp:ListItem Value="03" Text="March"></asp:ListItem>
                                                        <asp:ListItem Value="04" Text="April"></asp:ListItem>
                                                        <asp:ListItem Value="05" Text="May"></asp:ListItem>
                                                        <asp:ListItem Value="06" Text="June"></asp:ListItem>
                                                        <asp:ListItem Value="07" Text="July"></asp:ListItem>
                                                        <asp:ListItem Value="08" Text="August"></asp:ListItem>
                                                        <asp:ListItem Value="09" Text="September"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-5">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-7">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" Enabled="false">
                                                        <asp:ListItem Value="" Text="-- Select Here --"></asp:ListItem>
                                                        <asp:ListItem Value="RE" Text="Regular Employees"></asp:ListItem>
                                                        <asp:ListItem Value="CE" Text="Casual Employees"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3"></div>
                                <div class="col-9">
                                    <div class="form-group row">
                                        <div class="col-2" style="padding-right:0px !important" >
                                            <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_template" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_payroll_template_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">

                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <a ID="lb_back" class="btn btn-info btn-sm font-weight-bold" href="/View/cPayRegistry/cPayRegistry.aspx" ><i class="fa fa-arrow-left"></i> Back To Payroll Registry</a>
                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-8">
                                    <div class="form-group row">
                                        <div class="col-2" style="padding-right:0px !important;">
                                            <asp:Label runat="server" Text="Payroll Group:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_group" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" style="float:right;width:97.5%" OnSelectedIndexChanged="ddl_payroll_group_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-1">
                                    <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                        <ContentTemplate>
                                            
                                            <% if (ViewState["page_allow_add"].ToString() == "1")
                                                {  %>
                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn"  Text="Add" OnClick="btnAdd_Click" />
                                            <% }
                                                %>     
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="up_dataListGrid" class="mt10" UpdateMode="Conditional" runat="server" >
                                <ContentTemplate>
                                    <asp:GridView 
                                            ID="gv_dataListGrid" 
                                            runat="server" 
                                            allowpaging="True" 
                                            AllowSorting="True" 
                                            AutoGenerateColumns="False" 
                                            EnableSortingAndPagingCallbacks="True"
                                            ForeColor="#333333" 
                                            GridLines="Both" height="100%" 
                                            onsorting="gv_dataListGrid_Sorting"  
                                            OnPageIndexChanging="gridviewbind_PageIndexChanging"
                                            PagerStyle-Width="3" 
                                            PagerStyle-Wrap="false" 
                                            pagesize="10"
                                            Width="100%" 
                                            Font-Names="Century gothic"
                                            Font-Size="Medium" 
                                            RowStyle-Width="5%" 
                                            AlternatingRowStyle-Width="10%"
                                            CellPadding="2"
                                            ShowHeaderWhenEmpty="True"
                                            EmptyDataText="NO DATA FOUND"
                                            EmptyDataRowStyle-ForeColor="Red"
                                            EmptyDataRowStyle-CssClass="no-data-found"
                                            >
                                           <Columns>
                                                <asp:TemplateField HeaderText="ID NO" SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        <%# Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="POSITION" SortExpression="position_title1">
                                                    <ItemTemplate>
                                                        <%# Eval("position_title1") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("net_pay") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="STATUS" SortExpression="post_status_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("post_status_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="12%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="imgbtn_editrow1" 
                                                                        CssClass="btn btn-primary action" 
                                                                        EnableTheming="true"  
                                                                        runat="server" 
                                                                        ImageUrl='<%# Session["ep_post_authority"].ToString() == "0" ? "~/ResourceImages/final_edit.png" : "~/ResourceImages/card.png" %>'  
                                                                        OnCommand="editRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_registry_nbr") + "," + Eval("payroll_year")%> ' 
                                                                        tooltip='<%# Session["ep_post_authority"].ToString() == "0" ? "Edit Existing Record" : "Post to Card" %>' />
                                                        
                                                                <%  }
                                                                %>
                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="lnkDeleteRow" 
                                                                        CssClass="btn btn-danger action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl='<%# Session["ep_post_authority"].ToString() == "1" ? "~/ResourceImages/unpost.png" : "~/ResourceImages/final_delete.png" %>' 
                                                                        OnCommand="deleteRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_registry_nbr") + "," + Eval("payroll_year")%> ' 
                                                                        Enabled='<%# Eval("post_status").ToString() == "Y" && Session["ep_post_authority"].ToString() == "0" || Eval("post_status").ToString() == "N" && Session["ep_post_authority"].ToString() == "1" || Eval("post_status").ToString() == "R" ? false : true %>'
                                                                        ToolTip='<%# Session["ep_post_authority"].ToString() == "0" ? "Delete Existing Record" : "UnPost From Card" %>'/>
                                                                <% }
                                                                %>
                                                                <% if (ViewState["page_allow_print"].ToString() == "1")
                                                                    {
                                                                %>
                                                                     <asp:ImageButton ID="imgbtn_print" 
                                                                         CssClass="btn btn-success action" 
                                                                         EnableTheming="true" runat="server" 
                                                                         ImageUrl="~/ResourceImages/print1.png" 
                                                                         style="padding-left: 0px !important;" 
                                                                         CommandArgument='<%# Eval("payroll_registry_nbr")%> ' 
                                                                         tooltip="Print Card"/>
                                                                <%  }
                                                                %>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="12%" />
                                                    <ItemStyle CssClass="text-center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings  
                                            Mode="NumericFirstLast" 
                                            FirstPageText="First" 
                                            PreviousPageText="Previous" 
                                            NextPageText="Next" 
                                            LastPageText="Last" 
                                            PageButtonCount="1" 
                                            Position="Bottom" 
                                            Visible="True" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="10%" />
                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Middle" Font-Size="14px" CssClass="td-header" />
                                            <PagerStyle CssClass="pagination-ys" BackColor="#2461BF" ForeColor="White" HorizontalAlign="right" VerticalAlign="NotSet" Wrap="True" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                    <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_payroll_template" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_payroll_group" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</form>
   
    <script type="text/javascript">
        function closeModal() {
            $('#add').modal("hide");
             $('#AddEditConfirm').modal({
                 keyboard: false,
                backdrop:"static"
            });
            setTimeout(function () {
                $('#AddEditConfirm').modal("hide");
                $('.modal-backdrop.show').remove();
                
            }, 800);
        };

        function click_tab(target_tab)
        {
            if (target_tab == 1)
            {
                $('#id_mandatory').click()
            }
            else if (target_tab == 2)
            {
                $('#id_optional').click()
            }
            else if (target_tab == 3)
            {
                $('#id_loans').click()
            }
        }
    </script>

    <script type="text/javascript">
        function closeModal1() {
            $('#add').modal("dispose");
         };
    </script>

    <script type="text/javascript">
        function openModalDelete() {
            $('#deleteRec').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>

    <script type="text/javascript">
        function openNotification() {
            $('#notification').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>
     
    <script type="text/javascript">
        function closeModalDelete() {
            $('#deleteRec').modal('hide');
         };
    </script>

    <script type="text/javascript">
        function openNotification1() {
            $('#notification1').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>
    <script type="text/javascript">
        function closeNotification1()
        {
            $('#notification1').modal("dispose");
            $(Window).find('.modal-backdrop.fade.show')[1].remove();
         };
    </script>

    <script type="text/javascript">
        function openAddHeader() {
            $('#header').modal({
                keyboard: false,
                backdrop:"static"
            });
        };
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
   
    <script type="text/javascript">
        function openModal() {
            $('#add').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>

    <script type="text/javascript">
        function hightlight()
        {
            $('#<%= gv_dataListGrid.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight_on_grid');
           }, function () {
                   $(this).removeClass('highlight_on_grid');
           });
        }

        $(document).ready(function () {
           $('#<%= gv_dataListGrid.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight_on_grid');
           }, function () {
                   $(this).removeClass('highlight_on_grid');
           });
        });
    </script> 
</asp:Content>
