<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayOvertime.aspx.cs" Inherits="HRIS_ePayroll.View.cPayOvertime" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
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
                                        <i class="fa-5x fa fa-question text-danger "></i>
                                        <h2 id="deleteRec0" runat="server">Delete this Record</h2>
                                        <h6><asp:Label ID="deleteRec1" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
                                        <asp:Label ID="lbl_unposting" runat="server" CssClass="text-left" Text="Reason for UnPosting :"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtb_reason" CssClass="form-control form-control-sm" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired201"></asp:Label>
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
                                <h5 class="modal-title" id="AddEditPage"><asp:Label ID="LabelAddEdit" runat="server" Text="Add/Edit Page" forecolor="White"></asp:Label>
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_post_status" runat="server" CssClass="badge badge-danger" forecolor="White" Font-Size="Small"></asp:Label>
                                </h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" style="padding-bottom:3px !important;" runat="server" >
                            <div class="row" style="margin-top:-15px;display:none">
                                <div class="col-12 text-right" >
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" ></asp:Label>
                                            <asp:Label ID="lbl_registry_number" runat="server" Text="" Font-Underline="true" CssClass="font-weight-bold" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <div style="float:right;width:65%;margin-left:5px;">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtb_status" CssClass="form-control form-control-sm text-center" Width="100%" Enabled="false" ></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:Label runat="server" style="float:right;padding-top:2px;" Text="Status:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" style="padding-right:0px;">
                                    <asp:Label runat="server" Text="Date Posted:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" style="padding-right:0px;padding-left:0px;">
                                    <asp:UpdatePanel runat="server" >
                                        <ContentTemplate>
                                            <%--<asp:TextBox runat="server" ID="txtb_date_posted" CssClass="form-control form-control-sm my-date" MaxLength="15"  Width="100%"></asp:TextBox>--%>
                                            <asp:TextBox runat="server" ID="txtb_date_posted" CssClass="form-control form-control-sm text-center" MaxLength="15"  Width="100%" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-5">
                                    <div style="float:right;width:45%;margin-left:5px;">
                                        <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_voucher_nbr" CssClass="form-control form-control-sm text-center" MaxLength="15" Width="100%"></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired200"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                    <asp:Label runat="server" style="float:right;padding-top:2px;" Text="Voucher Nbr :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                            </div>
                            <div class="form-group row" style="margin-top:-5px;" >
                                
                                <div class="col-3">
                                     <asp:Label ID="Label2" runat="server" Text="Employee's Name:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-6" >
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" Width="100%" OnTextChanged="ddl_empl_id_TextChanged" AutoPostBack="true" ></asp:DropDownList>
                                            <asp:TextBox runat="server" ID="txtb_employeename" CssClass="form-control form-control-sm font-weight-bold" Enabled="false" ></asp:TextBox> 
                                            <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
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
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-3" >
                                     <asp:Label ID="Label4" runat="server" Text="Position Title :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-9" >
                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server"  ID="txtb_position" CssClass="form-control form-control-sm font-weight-bold"  Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-3" >
                                     <asp:Label ID="Label5" runat="server" Text="Department:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-9" >
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server"  ID="txtb_department_descr" CssClass="form-control form-control-sm font-weight-bold"  Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                 <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                           </div>
                           <div class="form-group row" runat="server" id="div_monthly">
                                <div class="col-3">
                                    <asp:Label CssClass="font-weight-bold" runat="server" Text="Monthly Rate:"></asp:Label>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_monthly_rate" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                            <asp:Label ID="LblRequired202" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                             
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                               <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="Daily Rate:"></asp:label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" Enabled="false"  ID="txtb_daily_rate" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>

                            </div>
                            <%--
                            <div class="row">
                                <div class="col-12" style="padding-top:0px;padding-bottom:2px;">
                                    <strong>Summary Total</strong>
                                </div>
                            </div>
                            --%>
                            <div class="row" runat="server">
                                <div class="col-12">
                                    <ul class="nav nav-tabs" style="border-bottom: 1px solid #dee2e6" id="myTab" role="tablist">
                                        
                                        <li class="nav-item" style="width:33.33% !important">
                                            <a class="nav-link active" id="id_header" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab" aria-selected="false" style="font-weight:bold" aria-expanded="true"><i class="fa fa-info-circle"></i> Overtime Information</a>
                                        </li>
                                        <li class="nav-item" style="width:33.33% !important">
                                            <a class="nav-link" id="id_employee" data-toggle="tab" href="#optional_tab" role="tab" aria-controls="optional_tab" aria-selected="false" style="font-weight:bold"><i class="fa fa-history"></i> Overtime Breakdown</a>
                                        </li>
                                        <li class="nav-item" style="width:33.33% !important">
                                            <a class="nav-link" id="id_employee_oth" data-toggle="tab" href="#employee_oth_tab" role="tab" aria-controls="employee_oth_tab" aria-selected="false" style="font-weight:bold"><i class="fa fa-address-book"></i> Other Information <span class="badge badge-success"> New </span> </a>
                                        </li>
                                    </ul>
                                
                                    <div class="tab-content" id="myTabContent" style="width:100%">
                                        <div class="tab-pane fade show active"  id="mandatory_tab" role="tabpanel" style="border: 1px solid whitesmoke;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;" aria-labelledby="id_header">
                                            <div class="form-group row" style="padding-top:10px">
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                            
                                            
                                                            <div class="form-group row" runat="server" >
                                                                <div class="col-6">
                                                                    <label class="font-weight-bold">Hourly Rate (Actual):</label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Enabled="false"  ID="txtb_hourly_rate_actual" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                            
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" runat="server" id="div_hourly_rate_re_ce">
                                                                <div class="col-6">
                                                                    <label class="font-weight-bold">Hourly Rate (25%):</label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtb_hourly_rate_25" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                            
                                                                <div class="col-6">
                                                                    <label class="font-weight-bold">Hourly Rate (50%):</label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtb_hourly_rate_50" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" runat="server" >

                                                                <div class="col-12">
                                                                <hr />
                                                                    <asp:label runat="server" ID="Label9" Font-Bold="true" Text="Remarks:"></asp:label>
                                                                </div>
                                                                <div class="col-12 ">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server"  Font-Bold="true" ID="txtb_remarks" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                                            <asp:Label ID="LblRequired2" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group row" runat="server" >
                                                                <div class="col-6">
                                                                    <asp:label runat="server" Font-Bold="true" Text="OT Hour (Actual):"></asp:label>
                                                                </div>
                                                                <div class="col-6 ">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_hr_actual" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                            <asp:Label ID="LblRequired3" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" runat="server" id="div_ot_hour_re_ce">
                                                                <div class="col-6" style="margin-bottom:3px" >
                                                                    <asp:label runat="server" Font-Bold="true" Text="OT Hour (25%):"></asp:label>
                                                                </div>
                                                                <div class="col-6" style="margin-bottom:3px" >
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_hr_25" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                            <asp:Label ID="LblRequired4" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                            
                                                                <div class="col-6">
                                                                    <asp:label runat="server" Font-Bold="true" Text="OT Hour (50%):"></asp:label>
                                                                </div>
                                                                <div class="col-6 ">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_hr_50" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                            <asp:Label ID="LblRequired5" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" runat="server" id="div_tax_re_ce">
                                                                <div class="col-6 pb-1">
                                                                    <asp:label runat="server" Font-Bold="true" Text="Tax Rate (%):"></asp:label>
                                                                </div>
                                                                <div class="col-6 pb-1">
                                                                    <asp:UpdatePanel runat="server" >
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Font-Bold="true"  ID="txtb_bir_tax_rate_perc" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                            <asp:Label ID="LblRequired66" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-6">
                                                                    <asp:label runat="server" Font-Bold="true" Text="BIR Tax:"></asp:label>
                                                                </div>
                                                                <div class="col-6 ">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Font-Bold="true"  ID="txtb_bir_tax" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                            <asp:Label ID="LblRequired6" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row" runat="server" id="div_tax_jo">
                                                                <div class="col-6">
                                                                    <label class="font-weight-bold">BIR Tax (2%):</label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtb_tax2" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                             <asp:Label ID="LblRequired7" CssClass="lbl_required" runat="server" Text=""></asp:Label> 
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                            
                                                                <div class="col-6">
                                                                    <% if (Convert.ToDateTime(ddl_year.SelectedValue + "-" + ddl_month.SelectedValue + "-" + "01") > Convert.ToDateTime("2021-03-30"))
                                                                    {
                                                                    %>
                                                                       <label class="font-weight-bold">BIR Tax (1%):</label>
                                                                    <% 
                                                                        }
                                                                        else
                                                                        {
                                                                    %> 
                                                                        <label class="font-weight-bold">BIR Tax (1%):</label>
                                                                    <%
                                                                        }
                                                                    %>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtb_tax3" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                             <asp:Label ID="LblRequired8" CssClass="lbl_required" runat="server" Text=""></asp:Label> 
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-6">
                                                                    <label class="font-weight-bold">BIR Tax (5%):</label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtb_tax5" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                             <asp:Label ID="LblRequired9" CssClass="lbl_required" runat="server" Text=""></asp:Label> 
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-6">
                                                                    <label class="font-weight-bold">BIR Tax (8%):</label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtb_tax8" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                             <asp:Label ID="LblRequired10" CssClass="lbl_required" runat="server" Text=""></asp:Label> 
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-6">
                                                                    <label class="font-weight-bold">BIR Tax (10%):</label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtb_tax10" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                             <asp:Label ID="LblRequired11" CssClass="lbl_required" runat="server" Text=""></asp:Label> 
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-6">
                                                                    <label class="font-weight-bold">BIR Tax (15%):</label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtb_tax15" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                             <asp:Label ID="LblRequired12" CssClass="lbl_required" runat="server" Text=""></asp:Label> 
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row" runat="server" >
                                                                <div class="col-6"></div>
                                                                <div class="col-6" style="margin-top:5px;">
                                                                     <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Button ID="btn_calculate" runat="server"  Text="CALCULATE" CssClass="btn btn-sm btn-block btn-warning" OnClick="btn_calculate_Click"/>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>

                                                            <hr style="margin-top:5px; margin-bottom:5px;">
                                                             <div class="form-group row" runat="server" id="div6" visible="true" >
                                                                <div class="col-6">
                                                                    <asp:label runat="server" ID="Label14" Font-Bold="true" Text="Total Gross:"></asp:label>
                                                                </div>
                                                                <div class="col-6 ">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_gross_pay" Enabled="false" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" runat="server" id="div_net_pay" visible="true">
                                                                <div class="col-6">
                                                                    <asp:label runat="server" ID="lbl_net_pay" Font-Bold="true" Text="Total Net Pay:"></asp:label>
                                                                </div>
                                                                <div class="col-6 text-right">
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_net_pay" Enabled="false" ReadOnly="true" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="optional_tab" role="tabpanel" style="border: 1px solid whitesmoke;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;" aria-labelledby="id_employee">
                                            <div class="form-group row" runat="server" style="padding-top:10px">
                                                
                                                
                                                <div class="col-12 ml-0 pl-0" id="div_for_timer">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div runat="server" id="div_msg" class="alert alert-warning alert-dismissible fade show small" role="alert" style="padding:5px;margin-bottom:0px;margin-left:10px;opacity:0.8" >
                                                                <strong runat="server" id="hdr_msg"> </strong> -- 
                                                                <asp:Label runat="server" ID="dtl_msg" CssClass="alert-link" style="text-decoration:underline !important" ></asp:Label>
                                                                <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="padding: 0px; margin-right: 5px;">
                                                                <span aria-hidden="true" class="small">&times;</span>
                                                                </button>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-3 pt-1">
                                                    <asp:label runat="server" Font-Bold="true" Text="Overtime Date:"></asp:label>
                                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" ID="updatepanel_ot_date">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_ot_date" CssClass="form-control form-control-sm my-date text-center" ></asp:TextBox>
                                                            <asp:Label ID="LblRequired100" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-2">
                                                    <asp:label runat="server" Font-Bold="true" Text="# Hours:" ></asp:label>
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_ot_hours" CssClass="form-control form-control-sm text-center" MaxLength="6"></asp:TextBox>
                                                            <asp:Label ID="LblRequired102" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-4">
                                                    <asp:label runat="server" Font-Bold="true" Text="OT Percentage:"></asp:label>
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_ot_perc">
                                                                <asp:ListItem Value="100" Text="100% - Actual Hours"></asp:ListItem>
                                                                <asp:ListItem Value="125" Text="125% - Percentage"></asp:ListItem>
                                                                <asp:ListItem Value="150" Text="150% - Percentage"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="LblRequired103" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-3" style="padding-top:23px !important"  runat="server">
                                                    <asp:UpdatePanel runat="server" ID="div_gen_add">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" CssClass="btn btn-primary btn-sm btn-block  save-icon icn" Text="Add to Grid" ID="btn_save_brkdwn" OnClick="btn_save_brkdwn_Click"  />
                                                            <div class="btn-group btn-block" role="group" aria-label="Button group with nested dropdown">
                                                  
                                                                <div class="btn-group btn-block " role="group" runat="server" id="id_options">
                                                                    <button id="btnGroupDrop1" type="button" class="btn btn-secondary dropdown-toggle btn-block btn-warning btn-sm" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                        <i class="fa fa-cog"></i> Options
                                                                    </button>
                                                                    <div class="dropdown-menu" aria-labelledby="btn_payroll_per_employee">
                                                                        <asp:Button ID="btn_generate_ovtm" runat="server" CssClass="dropdown-item btn-sm"  Text="Generate OT from DTR" OnClick="btn_generate_ovtm_Click"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="col-12">
                                                    
                                                    <asp:UpdatePanel ID="up_dataListGrid_brkdwn" class="mt10" UpdateMode="Conditional" runat="server" >
                                                        <ContentTemplate>
                                                            <asp:GridView 
                                                                    ID="gv_dataListGrid_brkdwn" 
                                                                    runat="server" 
                                                                    allowpaging="True" 
                                                                    AllowSorting="True" 
                                                                    AutoGenerateColumns="False" 
                                                                    EnableSortingAndPagingCallbacks="True"
                                                                    ForeColor="#333333" 
                                                                    GridLines="Both" height="100%" 
                                                                     OnSorting="gv_dataListGrid_brkdwn_Sorting"
                                                                     OnPageIndexChanging="gv_dataListGrid_brkdwn_PageIndexChanging"
                                                                    PagerStyle-Width="3" 
                                                                    PagerStyle-Wrap="false" 
                                                                    pagesize="30"
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
                                                                        <asp:TemplateField HeaderText="OT DATE" SortExpression="ot_date">
                                                                            <ItemTemplate>
                                                                                <%# Eval("ot_date") %>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="center" />
                                                                        </asp:TemplateField>
                                                                    
                                                                        <asp:TemplateField HeaderText="# OF HRS." SortExpression="hours_rendered">
                                                                            <ItemTemplate>
                                                                                <%# Eval("hours_rendered") %>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="OT PERCENTAGE" SortExpression="ot_perc_descr">
                                                                            <ItemTemplate>
                                                                                &nbsp;&nbsp;<%# Eval("ot_perc_descr")%>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="50%" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ACTION">
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <% 
                                                                                            if (ViewState["page_allow_edit"].ToString() == "1")
                                                                                            {
                                                                                        %>
                                                                                            <asp:ImageButton ID="imgbtn_editrow_brkdwn" 
                                                                                                CssClass="btn btn-primary action btn-sm" 
                                                                                                EnableTheming="true"  
                                                                                                runat="server" 
                                                                                                ImageUrl="~/ResourceImages/final_edit.png" 
                                                                                                OnCommand="editRow_Command_brckdwn" 
                                                                                                CommandArgument='<%# Eval("empl_id") + "," + Eval("ot_date") + "," + Eval("payroll_registry_nbr")%> ' 
                                                                                                 tooltip="Edit Existing Record"/>
                                                        
                                                                                        <%  }
                                                                                        %>
                                                                                        <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                                            {
                                                                                        %>
                                                                                            <asp:ImageButton ID="lnkDeleteRow_brkdwn" 
                                                                                                CssClass="btn btn-danger action btn-sm" 
                                                                                                EnableTheming="true" 
                                                                                                runat="server"  
                                                                                                ImageUrl="~/ResourceImages/final_delete.png"
                                                                                                OnCommand="deleteRow_Command_brckdwn" 
                                                                                                CommandArgument='<%# Eval("empl_id") + "," + Eval("ot_date") + "," + Eval("payroll_registry_nbr")%> ' 
                                                                                                ToolTip="Delete Existing Record"/>
                                                                                        <% }
                                                                                        %>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10%" />
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
                                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Middle" Font-Size="9px" CssClass="td-header" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="btn_save_brkdwn" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="employee_oth_tab" >
                                            <div class="row">
                                                <div class="col-12">

                                                <asp:UpdatePanel ID="up_dataListGrid_oth" class="mt10" UpdateMode="Conditional" runat="server" >
                                                        <ContentTemplate>
                                                            <asp:GridView 
                                                                    ID="gv_dataListGrid_oth" 
                                                                    runat="server" 
                                                                    allowpaging="True" 
                                                                    AllowSorting="True" 
                                                                    AutoGenerateColumns="False" 
                                                                    EnableSortingAndPagingCallbacks="True"
                                                                    ForeColor="#333333" 
                                                                    GridLines="Both" height="100%" 
                                                                    OnSorting="gv_dataListGrid_brkdwn_Sorting"
                                                                    OnPageIndexChanging="gv_dataListGrid_brkdwn_PageIndexChanging"
                                                                    PagerStyle-Width="3" 
                                                                    PagerStyle-Wrap="false" 
                                                                    pagesize="30"
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
                                                                        <asp:TemplateField HeaderText="YEAR MONTH" SortExpression="payroll_year_month">
                                                                            <ItemTemplate>
                                                                                &nbsp;&nbsp;<%# Eval("payroll_year_month") %>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="REG NBR" SortExpression="payroll_registry_nbr">
                                                                            <ItemTemplate>
                                                                                <%# Eval("payroll_registry_nbr")%>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10%" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="OT DATE" SortExpression="ot_date">
                                                                            <ItemTemplate>
                                                                                <%# Eval("ot_date") %>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="STATUS" SortExpression="post_status_descr">
                                                                            <ItemTemplate>
                                                                                <%# Eval("post_status_descr") %>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
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
                                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Middle" Font-Size="9px" CssClass="td-header" />
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
                                                            <asp:AsyncPostBackTrigger ControlID="btn_save_brkdwn" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top:5px;margin-bottom:5px;" />
                            <div class="row" style="margin-bottom:5px;" >
                                <div class="col-6">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_if_dateposted_yes" CssClass="col-form-label-sm text-danger" Font-Bold="true" Font-Size="Smaller" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                   </asp:UpdatePanel>
                                </div>
                                <div class="col-6 text-right">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                             <asp:HiddenField ID="hidden_rate_basis" runat="server" />
                                             <asp:HiddenField ID="hidden_max_amount" Value="0" runat="server" />
                                             <asp:HiddenField ID="hidden_monthly_days" runat="server" />
                                             <asp:HiddenField ID="hidden_hours_in_days" runat="server" />

                                             <asp:HiddenField ID="hidden_monthly_rate" runat="server" />
                                             <asp:HiddenField ID="hidden_daily_rate" runat="server" />
                                             <asp:HiddenField ID="hidden_hourly_rate" runat="server" />
                                             <asp:HiddenField ID="lbl_minimum_netpay_hidden" runat="server" />
                                       
                                            <%--<asp:Button ID="btn_show_headers" OnClick="btn_show_headers_Click" runat="server" Text="Button" />--%>
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
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="notification">
            <!-- The Modal - Message -->
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center">
                <div class="modal-header border-0">
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
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <i id="msg_icon" runat="server" class="fa fa-check-circle-o"></i>
                                <h3 id="msg_header" runat="server" class="font-weight-bold">Header Message</h3>
                                <asp:Label ID="lbl_details" runat="server" Text="" style="padding-right:30px ;padding-left:30px ;"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                <div class="col-3">
                    <strong style="font-family:Arial;font-size:18px;color:white;">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <%: ((ddl_payroll_template.SelectedItem.Text.ToString().Trim().Length-3) >22 ?  ddl_payroll_template.SelectedItem.Text.ToString().Substring(0,19)+"...":ddl_payroll_template.SelectedItem.Text.ToString().Trim())%>
                            </ContentTemplate>
                        </asp:UpdatePanel> 
                    </strong>
                </div>
                <div class="col-9">
                    <asp:UpdatePanel ID="UpdatePanel11" ChildrenAsTriggers="false" UpdateMode="Conditional"  runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtb_search" onInput="search_for(event);" runat="server" class="form-control" placeholder="Search.." Height="30px" 
                                Width="100%" OnTextChanged="txtb_search_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <script type="text/javascript">
                                function search_for(key)
                                {
                                        __doPostBack("<%= txtb_search.ClientID %>", "");
                                }
                            </script>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
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
                                                <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="30%" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="5" Value="5" />
                                                    <asp:ListItem Text="10" Selected="True" Value="10" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                            <asp:Label runat="server" Text="Entries"></asp:Label>
                                            &nbsp;|&nbsp;
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
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
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
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" AutoPostBack="true">
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
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3"></div>
                                <div class="col-9">
                                    <div class="form-group row">
                                        <div class="col-2" style="padding-right:0px;">
                                            <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_template" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-12">
                                            <a ID="lb_back" class="btn btn-info btn-sm font-weight-bold" href="/View/cPayRegistry/cPayRegistry.aspx" ><i class="fa fa-arrow-left"></i> Back To Payroll Registry</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-8">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <asp:Label runat="server" Text="Payroll Group:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_group" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" AutoPostBack="true" style="float:right;width:97.5%" Enabled="false"></asp:DropDownList>
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
                                            <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_payroll_group" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_payroll_template" />
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
                                                <asp:TemplateField HeaderText="ID NO." SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%# Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="POSITION TITLE" SortExpression="position_title1">
                                                    <ItemTemplate>
                                                        <%# Eval("position_title1") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("net_pay") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="POST STATUS" SortExpression="post_status">
                                                    <ItemTemplate>
                                                        <%# Eval("post_status_descr")%>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
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
                                                                        Enabled='<%# Eval("post_status").ToString() == "Y" && Session["ep_post_authority"].ToString() == "0" || Eval("post_status").ToString() == "N" && Session["ep_post_authority"].ToString() == "1" ||  Eval("post_status").ToString() == "R" ? false : true %>'
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
                                                    <ItemStyle Width="9%" />
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
        function openModal() {
            $('#add').modal({
                keyboard: false,
                backdrop:"static"
            });

            $('#id_header').click();
            show_date();
       };
       function show_date()
        {
           $('#<%= txtb_ot_date.ClientID %>').datepicker({format: 'yyyy-mm-dd'});
           if ($('#<%= txtb_ot_date.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_ot_date.ClientID %>').closest("div");
                parent_div.find("i").remove();    
           }
       }
    </script>
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
    </script>

    <script type="text/javascript">
        function closeModal1() {
            $('#add').modal("dispose");
         };
    </script>

    <script type="text/javascript">
        function closeModalDelete() {
            $('#deleteRec').modal('hide');
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
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

