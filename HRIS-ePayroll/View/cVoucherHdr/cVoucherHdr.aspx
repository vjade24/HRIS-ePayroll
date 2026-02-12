<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cVoucherHdr.aspx.cs" Inherits="HRIS_ePayroll.View.cVoucherHdr" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">

    <style type="text/css">
        .lbl_required {
            font-size: 8px !important;
        }
         .highlight
            {
                background-color: #507cd1 !important;
                color:white !important;
                cursor: pointer;
            }
        
        @keyframes blink {
            0% { color: red; }
            50% { color: transparent; }
            100% { color: red; }
        }

        .blink-red {
            animation: blink 1s infinite;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">
        <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
        <!-- The Modal - Select Report -->
        <asp:UpdatePanel ID="edit_delete_notify" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="SelectReport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #28a745;color: white;">
                            <h5 class="modal-title" ><asp:Label ID="notify_header" runat="server" Text="Report Options"></asp:Label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <div class="modal-body with-background">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <label>Year</label>
                                            <asp:TextBox runat="server" ID="selected_year" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Ctrl. No</label>
                                            <asp:TextBox runat="server" ID="selected_voucher_ctrl_nbr" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-12" style="margin-bottom: 5px;">
                                            <label>Select Report</label>
                                            <asp:DropDownList ID="ddl_select_report" CssClass="form-control form-control-sm" runat="server" Width="100%"  OnSelectedIndexChanged="ddl_select_report_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:Label ID="LblRequired0" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <%if (ddl_select_report.SelectedValue.Trim() == "110" ||
                                                  ddl_select_report.SelectedValue.Trim() == "111" ||
                                                  ddl_select_report.SelectedValue.Trim() == "115" )
                                            {
                                            %>
                                            <button class="btn btn-warning" id="id_cafoa_btn" onclick="btn_cafoa()"><i class="fa fa-briefcase"></i> CAFOA </button>
                                            <%--<button class="btn btn-warning" id="btn_cafoa" onclick="show_cafoa()"> CAFOA Override</button>--%>
                                            <%
                                            }
                                            %>
                                            <asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkPrint_Click"  OnClientClick="openLoading();"> <i class="fa fa-print"></i> Print </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                     </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <!-- The Modal - Generating Report -->
                <div class="modal fade" id="Loading">
                    <div class="modal-dialog modal-dialog-centered modal-lg">
                        <div class="modal-content text-center">
                            <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 text-center">
                                                    <img src="/ResourceImages/loadingwithlogo.gif" style="width: 100%;" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- Modal footer -->
                            <div style="margin-bottom: 30px">
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                                        <asp:TextBox runat="server" ID="txtb_reason"  visible="false" CssClass="form-control form-control-sm" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        <asp:Label ID="LblRequired205" CssClass="lbl_required" runat="server" Text=""></asp:Label>
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

        <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="add-header" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered  modal-lg" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)" >
                    <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <h5 class="modal-title"><asp:Label ID="Label4" runat="server" Text="Add/Edit" forecolor="White"></asp:Label></h5>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    </div>
                    <div class="modal-body with-background" runat="server"  style="padding-top: 5px;">
                        
                    </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>--%>


       <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-xl" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)" >
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow)" >
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
                            
                            <div class="row">
                                <div class="col-12 text-right" style="margin-top:-15px;display:none">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_registry_no" runat="server" Text="" Font-Underline="true" CssClass="font-weight-bold" Visible="false"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-3" style="display:none">
                                    <div style="float:right;width:65%;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_status"  visible="true" CssClass="form-control form-control-sm text-center" Width="100%" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label7" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div> 
                                    <asp:Label runat="server" style="float:left;padding-top:2px;" Text="Status:" CssClass="font-weight-bold" ></asp:Label>
                               </div>
                                <div class="col-2" style="padding-right:0px;padding-top:5px;margin-left:10px;display:none">
                                    <asp:Label runat="server" Text="Date Posted:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" style="padding-right:0px;padding-left:0px; margin-left:-10px;display:none">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <%--<asp:TextBox runat="server" ID="txtb_date_posted" CssClass="form-control form-control-sm my-date" MaxLength="15"  Width="100%"></asp:TextBox>--%>
                                            <asp:TextBox runat="server" ID="txtb_date_posted"  CssClass="form-control form-control-sm text-center" MaxLength="15"  Width="100%" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-5"  style="display:none">
                                    <div style="float:right;width:55%;">
                                        <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_voucher_nbr" CssClass="form-control form-control-sm text-center" MaxLength="15" Width="100%"></asp:TextBox>
                                            <asp:Label ID="LblRequired22" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                    <asp:Label runat="server" style="float:left;padding-top:2px; margin-right:5px;" MaxLength="15" Text="Voucher Nbr:" CssClass="font-weight-bold" ></asp:Label>
                                </div>


                                <div class="col-12"  style="display:none">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>

                                <div class="col-2">
                                     <asp:Label ID="Label2" runat="server" Text="Employee's Name:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-7" >
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                             <asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_id_SelectedIndexChanged" onchange="check_payroll()"></asp:DropDownList>
                                            <asp:TextBox runat="server" Font-Bold="true" Visible="false" ID="txtb_employeename" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
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


                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>

                                <div class="col-2">
                                     <asp:Label ID="Label11" runat="server" Text="Position Title:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-10">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_position" Font-Bold="true" CssClass="form-control form-control-sm" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                

                            </div>
                            <div class="row" >
                                <div class="col-2">
                                    <a href="#demo" data-toggle="collapse" id="btn_show">Department <small>(Show)</small>:</a>
                                    <%--<label class="font-weight-bold">Department:</label>--%>
                                </div>
                                <div class="col-10">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                                <asp:DropDownList ID="ddl_dep_modal" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_department" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row collapse" id="demo">
                                <div class="col-2">
                                    <label class="font-weight-bold">Sub-Department:</label>
                                </div>
                                <div class="col-10">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_subdep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                    <label class="font-weight-bold">Division:</label>
                                </div>
                                <div class="col-10">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_division" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                    <label class="font-weight-bold">Section:</label>
                                </div>
                                <div class="col-10">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                    <label class="font-weight-bold">Fund Charges:</label>
                                </div>
                                <div class="col-10">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_fund_charges" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row" >
                                <div class="col-12" runat="server" >
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-2">
                                    <asp:label runat="server" Font-Bold="true" Text="Prepared By (Name):" ></asp:label>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_prepared_name" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                    <asp:label runat="server" Font-Bold="true" Text="Designation:" ></asp:label>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_prepared_design" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12" runat="server" >
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                            </div>
                            <div class="row" >
                                
                                <div class="col-2">
                                    <asp:label runat="server" Font-Bold="true" Text="Claimant Name:" ></asp:label>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_claimant_name" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                    <asp:label runat="server" Font-Bold="true" Text="Relationship:" ></asp:label>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_claimant_rel" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12" runat="server" >
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                            </div>

                            <div class="row" >
                                <div class="col-6">
                                    <div class="form-group row">
                                            
                                        <div class="col-4">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" class="font-weight-bold" ID="lbl_rate_descr" Text="Monthly Rate:" ></asp:Label>
                                             
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-3">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_rate_amount" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-2">
                                            <asp:label runat="server" Font-Bold="true" Text="Lates Min:" ></asp:label>
                                        </div>
                                        <div class="col-3">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_lates_min" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired100"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="row" runat="server" id="div_voucher_type">
                                                <div class="col-4" style="padding-right:0px !important">
                                                    <asp:Label runat="server" class="font-weight-bold" ID="Label8" Text="Voucher Type:" ></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" ID="ddl_voucher_type" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_voucher_type_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value=""  Text="Default Voucher"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Diff./First Claim (Promotion)"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Sal. Diff (Multiple Months)"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="Other Sal. (Multiple Months)"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                </div>
                                
                            </div>

                            <div class="row mt-1" >
                                <div class="col-lg-6">

                                    <div class="form-group row">
                                        <div class="col-4">
                                            <asp:label runat="server" Font-Bold="true" Text="Remarks:" ></asp:label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_voucher_remarks" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired103"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    
                                    </div>
                                </div>
                            </div>

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                        <div class="row mt-1" runat="server" id="div_no_of_days">
                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <div class="col-4" >
                                                        <asp:label runat="server" Font-Bold="true" Text="No of Days:" ></asp:label>
                                                    </div>
                                                    <div class="col-3">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_no_of_days" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired1"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                            <div class="row">
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                            </div>
                            <div class="row" runat="server" id="div_summary_and_tab">
                                <div class="col-4">
                                    <asp:label runat="server" Font-Bold="true" Text="Summary Totals:"></asp:label>
                                    <div class="form-group row" runat="server" id="div_amount1" style="margin-bottom:0px !important">
                                        <div class="col-6" style="padding-right:0px !important">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount #1:" Font-Size="Small" ID="lbl_other_amount1_descr"></asp:label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right"  ID="txtb_other_amount1" Font-Bold="true"> </asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired68"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="form-group row"  runat="server" id="div_amount2" style="margin-bottom:0px !important">
                                        <div class="col-6" style="padding-right:0px !important">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount #2:" Font-Size="Small" ID="lbl_other_amount2_descr"></asp:label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right" ID="txtb_other_amount2" Font-Bold="true"> </asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired69"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="form-group row"  runat="server" id="div_amount3" style="margin-bottom:0px !important">
                                        <div class="col-6" style="padding-right:0px !important">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount #3:" Font-Size="Small" ID="lbl_other_amount3_descr"></asp:label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right" ID="txtb_other_amount3" Font-Bold="true"> </asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired70"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                        

                                    <div class="form-group row" runat="server" id="div_gross" style="margin-bottom:0px !important">
                                        <div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Lates Amount:" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_lates_amount" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired101"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:label runat="server" Font-Bold="true" Text="Gross Pay:" Font-Size="Small"></asp:label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_gross_pay" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired4"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="form-group row"  runat="server" id="div_amount4" style="margin-bottom:0px !important">
                                        <div class="col-6" style="padding-right:0px !important">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount #4:" Font-Size="Small" ID="lbl_other_amount4_descr"></asp:label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right" ID="txtb_other_amount4" Font-Bold="true"> </asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired102"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="form-group row" runat="server" id="div_lwop" style="margin-bottom:0px !important">
                                        <div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Lwop (MR):" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_lwo_pay" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired6"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div> 
                                        <div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Lwop (PERA):" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_lwop_amount_pera" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired71"></asp:Label>
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div> 
                                    </div>

                                    <div class="form-group row" runat="server" id="div_tax" style="margin-bottom:0px !important">
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:label runat="server" Font-Bold="true" Text="BIR Tax:" Font-Size="Small"></asp:label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_birtax_summary" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="Label5"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                        
                                    <div class="form-group row" runat="server" id="div_deductions" style="margin-bottom:0px !important">
                                        <div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Mandatory:" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_total_mandatory" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired7"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Optional:" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_total_optional" CssClass="form-control form-control-sm text-right"  Enabled="false" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired8"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Loans:" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_total_loans" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired9"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group row" style="padding:0px;" runat="server" id="div_netpay">
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:label runat="server" Font-Bold="true" Text="Refund Total:"  Font-Size="Small" ID="lbl_netpay_descr"></asp:label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_net_pay" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired10"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-8" style="padding-left:20px" id="div_alltab" runat="server">
                                    <div class="form-group row">
                                    <div class="col-12">
                                        <ul class="nav nav-tabs" style="border-bottom: 1px solid #dee2e6" id="myTab" role="tablist">
                                            <li class="nav-item">
                                            <a class="nav-link active" id="id_header" data-toggle="tab" href="#header_tab" role="tab" aria-controls="header_tab" aria-selected="false" style="font-weight:bold;padding: .09rem 1rem;" aria-expanded="true" runat="server">HEADER</a>
                                            </li>
                                        
                                            <li class="nav-item">
                                            <a class="nav-link " id="id_mandatory" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab" aria-selected="false" style="font-weight:bold;padding: .09rem 1rem;" runat="server">MANDATORY</a>
                                            </li>
                                            <li class="nav-item">
                                            <a class="nav-link" id="id_optional" data-toggle="tab" href="#optional_tab" role="tab" aria-controls="optional_tab" aria-selected="false" style="font-weight:bold;padding: .09rem 1rem;" runat="server">OPTIONAL</a>
                                            </li>
                                            <li class="nav-item">
                                            <a class="nav-link" id="id_loans" data-toggle="tab" href="#loans_tab" role="tab" aria-controls="loans_tab" aria-selected="false" style="font-weight:bold;padding: .09rem 1rem;" runat="server">LOANS</a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="tab-content" id="myTabContent" style="width:100%;margin-right:15px">
                                        <%--HEADER--%>
                                        <div class="tab-pane fade show active" id="header_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;min-height: 200px;" aria-labelledby="id_header" >
                                            <div class="row" style="padding-left:15px;padding-right:15px;padding-top:10px">
                                                
                                                <div class="col-6" >
                                                    <asp:UpdatePanel ID="UpdateDateFrom" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:label runat="server" Font-Bold="true" Text="Period From:" ></asp:label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date" ID="txtb_period_from"> </asp:TextBox>
                                                            <asp:Label ID="LblRequired203" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6" >
                                                    <asp:UpdatePanel ID="UpdateDateTo" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:label runat="server" Font-Bold="true" Text="Period To:" ></asp:label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date" ID="txtb_period_to"> </asp:TextBox>
                                                            <asp:Label ID="LblRequired204" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:label runat="server" Font-Bold="true" Text="Voucher Details #1:" ></asp:label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_voucher_descr1" TextMode="MultiLine" Rows="2"> </asp:TextBox>
                                                            <asp:Label ID="LblRequired201" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:label runat="server" Font-Bold="true" Text="Voucher Details #2:" ID="lbl_voucher_descr2" ></asp:label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_voucher_descr2" TextMode="MultiLine" Rows="2"> </asp:TextBox>
                                                            <asp:Label ID="LblRequired202" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                
                                                <div class="col-md-12">
                                                    <hr style="padding:0px;margin: 5px;"/>
                                                    <h6 class="m-t-none m-b text-success">DOCUMENT TRACKING OVERRIDES</h6>
                                                </div>
                                        
                                                <div class="col-md-6">
                                                    <asp:Label ID="Label9" runat="server" CssClass="font-weight-bold"  Text="Department:"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddl_department_code_trk" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                            <asp:Label ID="LblRequired301" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="col-md-4">
                                                    <asp:Label ID="Label13" runat="server" CssClass="font-weight-bold"  Text="Function code:"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddl_function_code_trk" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                            <asp:Label ID="LblRequired302" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label15" runat="server" CssClass="font-weight-bold"  Text="Allotment code:"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtb_allotment_code_trk" MaxLength="8"> </asp:TextBox>
                                                            <asp:Label ID="Label16" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>

                                        <%--MANDATORY DEDUCTIONS--%>
                                        <div class="tab-pane fade"  id="mandatory_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;min-height: 200px" aria-labelledby="id_mandatory" >
                                            <div class="row" style="padding-left:15px;padding-right:15px;padding-top:10px">
                                                <div class="col-3">
                                                    <div class="form-group row">
                                                        <div class="col-6" >
                                                            <asp:label runat="server" Font-Bold="true" Text="GSIS - GS:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_gs" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired13"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Bold="true" Text="GSIS - PS:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_ps" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired15"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:label runat="server" Font-Bold="true" Text="SIF:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server"  ID="txtb_gsis_sif" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired16"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Bold="true" Text="HDMF-GS:" Font-Size="Small"></asp:label>
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
                                                </div>
                                                <div class="col-3">
                                                    <div class="form-group row">
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Bold="true" Text="HDMF-PS:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_hdmf_ps" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired18"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Bold="true" Text="PHIC - GS:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_phic_gs" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired19"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Bold="true" Text="PHIC - PS:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_phic_ps" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired20"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Bold="true"  Text="BIR Tax:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_bir_tax" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired21"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <%-- BEGIN - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand3"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand4"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand4"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand5"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand5"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand6"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand6" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand6"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand7"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand7" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand8"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand8" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand8"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand9"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand9" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand9"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand10"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand10" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand10"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <%-- END   - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    
                                        <%--OPTIONAL DEDUCTIONS--%>
                                        <div class="tab-pane fade" id="optional_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;min-height: 200px;" aria-labelledby="id_optional">
                                            <div class="row" style="padding-left:15px;padding-right:15px;padding-top:10px">
                                                <div class="col-3">
                                                    <div class="form-group row">
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="SSS:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_sss" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired28"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;">
                                                            <asp:label runat="server" Font-Bold="true" Text="HDMF-PS2:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_hdmf_addl" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired29"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;">
                                                            <asp:label runat="server" Font-Bold="true" Text="HDMFMP2:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_hdmf_mp2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired66"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="HDMF-LCD:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_hdmf_loyalty_card" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired67"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="PHILAM LF:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_philam" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired30"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="EHP:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_ehp" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired31"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="HIP:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server"  ID="txtb_gsis_hip" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired32"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="col-3">
                                                    <div class="form-group row">
                                                        
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="CEAP:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_ceap" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired33"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="GSIS A.I:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_add" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired34"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" ID="lbl_other_prem1" Font-Bold="true" Text="(Reserved1):" Font-Size="Small"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_otherpremium_no1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired35"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                        <asp:label runat="server" ID="lbl_other_prem2"  Font-Bold="true" Text="(Reserved2):" Font-Size="Small"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server"  ID="txtb_otherpremium_no2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired36"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" ID="lbl_other_prem3"  Font-Bold="true" Text="(Reserved3):" Font-Size="Small"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server"  ID="txtb_otherpremium_no3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired37"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px"">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" ID="lbl_other_prem4"  Font-Bold="true" Text="(Reserved4):" Font-Size="Small"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px"">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_otherpremium_no4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired38"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px"">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" ID="lbl_other_prem5"  Font-Bold="true" Text="(Reserved5):" Font-Size="Small"></asp:label>
                                                                 </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_otherpremium_no5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired39"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <%-- BEGIN   - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem3"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem4"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem4"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem5"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem5"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem6"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem6" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem6"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem7"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem7" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem8"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem8" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem8"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem9"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem9" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem9"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem10"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem10" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem10"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                       <%-- END     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>

                                                    </div>
                                                </div>
                                            </div> 
                                        </div>

                                        <%--LOANS--%>
                                        <div class="tab-pane fade" id="loans_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;min-height: 200px" aria-labelledby="id_loans" >
                                            <div class="row" style="padding-left:15px;padding-right:15px;padding-top:10px">
                                                <div class="col-3">
                                                    <div class="form-group row">
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true" Text="Consol:"  Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_gsis_consolidated" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired40"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true" Text="Pol. Reg:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_gsis_policy_regular" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired41"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true" Text="Pol. Optnl:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_gsis_policy_optional" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired42"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true" Text="OULI Loan:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_gsis_ouli_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired43"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true" Text="Emergency:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_gsis_emer_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired44"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true" Text="E-Card:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_gsis_ecard_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired45"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true"  Text="Educ. Asst:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_gsis_educ_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired46"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true" Text="Real State:" Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_gsis_real_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired47"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    
                                                    </div>
                                                </div>

                                                <div class="col-3">
                                                    <div class="form-group row">
                                                            

                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="SOS:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server"  ID="txtb_gsis_sos_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired48"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="MPL:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_mpl_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired49"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="Housing:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_house_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired50"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="Calamity:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_cal_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired51"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="CCMPC:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_ccmpc_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired52"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="NICO:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_nico_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired53"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="Net. Bnk:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_networkbank_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired54"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="NHMFC:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_nhmfc_hsng" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired62"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group row">
                                                            
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="NAFC:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_nafc" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired63"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="GSIS HELP:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_gsis_help" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired64"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="GSIS HOUS:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_gsis_housing_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                         <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired65"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                            <asp:label runat="server" ID="lbl_loan1" Font-Bold="true" Text="(Reserved1):" Font-Size="Small"></asp:label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_otherloan_no1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired55"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                            <asp:label runat="server" ID="lbl_loan2" Font-Bold="true" Text="(Reserved2):" Font-Size="Small"></asp:label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_otherloan_no2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired56"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                            <asp:label runat="server" ID="lbl_loan3" Font-Bold="true" Text="(Reserved3):" Font-Size="Small"></asp:label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_otherloan_no3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired57"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                            <asp:label runat="server" ID="lbl_loan4" Font-Bold="true" Text="(Reserved4):" Font-Size="Small"></asp:label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_otherloan_no4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired58"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                            <asp:label runat="server" ID="lbl_loan5" Font-Bold="true" Text="(Reserved5):" Font-Size="Small"></asp:label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_otherloan_no5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired59"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>

                                                            <%-- BEGIN     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                       <%-- END     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        </div>
                                                </div>
                                                <div class="col-3">
                                                    <div class="form-group row">
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan3"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan4"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan4"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan5"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan5"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan6"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan6" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan6"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan7"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan7" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan8"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan8" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan8"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan9"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan9" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan9"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan10"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan10" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan10"></asp:Label>
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
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btn_calculate" CssClass="btn btn-warning btn-block" Text="Calculate" OnClick="btn_calculate_Click"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-4" style="padding:0px !important">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" ID="lbl_if_dateposted_yes" CssClass="text-danger" Font-Bold="true" Text="" style="font-size:11px"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-4 text-right">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                             <asp:LinkButton ID="Linkbtncancel"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                            <asp:Button ID="btnSave" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-12 text-center">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                                
                                            <asp:Label runat="server" ID="lbl_lastday_hidden" Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_rate_basis_hidden" Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_monthly_rate_hidden"  Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_daily_rate_hidden"    Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_hourly_rate_hidden"   Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_lwop_pera_hidden" Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_lwop_monthly_hidden" Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_minimum_netpay_hidden" Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_addeditmode_hidden" Visible="false" ></asp:Label>

                                            <asp:Label runat="server" ID="lbl_mone_contant_hidden" Visible="false" ></asp:Label>
                                            <asp:Label runat="server" ID="lbl_installation_monthly_conv_hidden" Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbl_installation_hours1day_hidden" Visible="false"></asp:Label>
                                            
                                            <asp:Label ID="hidden_report_filename" runat="server" Visible="false"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="modal_payroll_exists" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document" >
                <div class="modal-content ">
                    <div class="modal-header" style="background-image: linear-gradient(360deg, #ff2500, #106aff);color: white;">
                        <h6 id="id_hdr_payroll_check"></h6>
                        <button type="button" class="close collapse-button"  data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body " >
                        <div class="row">
                            <div class="col-lg-12" >
                                <ul style="max-height:500px !important;overflow-y:scroll !important" class="list-group" id="dataList"></ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
            <div class="col-3"><strong style="font-family:Arial;font-size:18px;color:white;"><%: Master.page_title %></strong></div>
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
                                    <div class="form-group row">
                                        <div class="col-lg-2">
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                        <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm form-control" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" ToolTip="Show entries per page">
                                                            <asp:ListItem Text="5" Value="5" />
                                                            <asp:ListItem Text="10" Selected="True" Value="10" />
                                                            <asp:ListItem Text="15" Value="15" />
                                                            <asp:ListItem Text="25" Value="25" />
                                                            <asp:ListItem Text="50" Value="50" />
                                                            <asp:ListItem Text="100" Value="100" />
                                                        </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="show_pagesx" CssClass="small" runat="server" Text="Page: 9/9"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-lg-3">
                                    <div class="form-group row">
                                        <div class="col-lg-6">
                                                <asp:Label runat="server"  Text="Payroll Year:" ></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged1" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group row">
                                        <div class="col-lg-4">
                                                <asp:Label runat="server" Text="Month:" ></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_month_SelectedIndexChanged" >
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
                                <div class="col-lg-4">
                                    <div class="form-group row">
                                        <div class="col-lg-5">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-lg-7">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3"></div>
                                <div class="col-lg-9">
                                    <div class="form-group row">
                                        <div class="col-lg-2" style="padding-right:0px !important" >
                                            <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_template" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_payroll_template_SelectedIndexChanged" ></asp:DropDownList>
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">

                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-9">
                                    <div class="form-group row">
                                        <div class="col-lg-2" >
                                            <asp:Label runat="server" Text="Department:" ></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                     <asp:DropDownList ID="ddl_dep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_SelectedIndexChanged" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                <ContentTemplate>
                                            
                                                    <% if (ViewState["page_allow_add"].ToString() == "1" && ((ddl_payroll_template.SelectedValue.ToString() != "609" && ddl_payroll_template.SelectedValue.ToString() != "709" && ddl_payroll_template.SelectedValue.ToString() != "809") &&
                                                                                                             (ddl_payroll_template.SelectedValue.ToString() != "605" && ddl_payroll_template.SelectedValue.ToString() != "705" && ddl_payroll_template.SelectedValue.ToString() != "805")))
                                                        {
                                                    %>
                                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn btn-block"  Text="Add" OnClick="btnAdd_Click" />
                                                    <% 
                                                        }
                                                    %>   

                                                    <%
                                                        else
                                                        {
                                                    %>
                                                            <label  style="font-size:xx-small;font-weight:bold;color:red;text-align:center"> YOU CANNOT ADD AT THIS TIME!</label>
                                                    <%
                                                        }
                                                    %>
                                            
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
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
                                               <asp:TemplateField HeaderText="VCHR #" SortExpression="voucher_ctrl_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("voucher_ctrl_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID #" SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="6%" />
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
                                                        &nbsp;<%# Eval("position_title1") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="PERIOD COVERED" SortExpression="voucher_period_from">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("voucher_period_from") + " - " + Eval("voucher_period_to") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("net_pay") %>&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="STATUS" SortExpression="post_status_descr">
                                                    <ItemTemplate>
                                                        <span class="badge <%# Eval("post_status").ToString().Trim() == "T" || Eval("post_status").ToString().Trim() == "N" ? "badge-danger" : "badge-primary" %>">
                                                            <small>
                                                                &nbsp;<%# Eval("post_status_descr") %>
                                                            </small>
                                                        </spa>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <%--<% 
                                                                if (
                                                                    ddl_payroll_template.SelectedValue == "608" || // Template Code for: Maternity
                                                                    ddl_payroll_template.SelectedValue == "708" || // Template Code for: Maternity
                                                                    ddl_payroll_template.SelectedValue == "808"    // Template Code for: Maternity
                                                                    || 
                                                                    (
                                                                    ddl_payroll_template.SelectedValue == "609" || // Template Code for: Other Claims/Refund
                                                                    ddl_payroll_template.SelectedValue == "709" || // Template Code for: Other Claims/Refund
                                                                    ddl_payroll_template.SelectedValue == "809"    // Template Code for: Other Claims/Refund
                                                                    )
                                                                    || 
                                                                    (
                                                                    ddl_payroll_template.SelectedValue == "605" || // Template Code for: Other Salaries
                                                                    ddl_payroll_template.SelectedValue == "705" || // Template Code for: Other Salaries
                                                                    ddl_payroll_template.SelectedValue == "805"    // Template Code for: Other Salaries
                                                                    )
                                                                    )
                                                                {
                                                                %>--%>
                                                                <asp:ImageButton 
                                                                    ID="imgbtn_add_empl" 
                                                                    runat="server" 
                                                                    EnableTheming="true"  
                                                                    CssClass="btn btn-warning action" 
                                                                    ImageUrl="~/ResourceImages/add.png" 
                                                                    style="padding-left: 0px !important;" 
                                                                    OnCommand="imgbtn_add_empl_Command" 
                                                                    CommandArgument='<%# Eval("empl_id") + "," + Eval("voucher_ctrl_nbr") + "," + Eval("payroll_year")%> ' 
                                                                    tooltip="Voucher Details"
                                                                    Visible='<%# 
                                                                                 Eval("payrolltemplate_code").ToString() == "608" || // Template Code for: Maternity
                                                                                 Eval("payrolltemplate_code").ToString() == "708" || // Template Code for: Maternity
                                                                                 Eval("payrolltemplate_code").ToString() == "808" || // Template Code for: Maternity

                                                                                 Eval("payrolltemplate_code").ToString() == "609" || // Template Code for: Other Claims/Refund
                                                                                 Eval("payrolltemplate_code").ToString() == "709" || // Template Code for: Other Claims/Refund
                                                                                 Eval("payrolltemplate_code").ToString() == "809" || // Template Code for: Other Claims/Refund
                                                                                 
                                                                                 Eval("payrolltemplate_code").ToString() == "610" ||  // Other Claims - v2
                                                                                 Eval("payrolltemplate_code").ToString() == "611" ||  // Other Claims - v2
                                                                                 Eval("payrolltemplate_code").ToString() == "612" ||  // Other Claims - v2
                                                                                 
                                                                                (Eval("payrolltemplate_code").ToString() == "605" || // Template Code for: Other Salaries
                                                                                 Eval("payrolltemplate_code").ToString() == "705" || // Template Code for: Other Salaries
                                                                                 Eval("payrolltemplate_code").ToString() == "805"    // Template Code for: Other Salaries
                                                                                 
                                                                                 ) && 
                                                                                 Eval("voucher_type").ToString() == "2" ||      // Template Code for: Other Salaries - Sal. Diff (Multiple Months)
                                                                                 Eval("voucher_type").ToString() == "3" 
                                                                        ? true : false %>'
                                                                    />
                                                                <%--<%   }

                                                                %>--%>

                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="imgbtn_editrow1" 
                                                                        CssClass="btn btn-primary action" 
                                                                        EnableTheming="true"  
                                                                        runat="server" 
                                                                        ImageUrl='<%# Session["ep_post_authority"].ToString() == "0" ? "~/ResourceImages/final_edit.png": "~/ResourceImages/card.png" %>'  
                                                                        OnCommand="editRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("voucher_ctrl_nbr") + "," + Eval("payroll_year")%> ' 
                                                                        tooltip='<%# Session["ep_post_authority"].ToString() == "0" ? "Edit Existing Record": "Post to Card" %>' />
                                                        
                                                                <%  }
                                                                %>
                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="lnkDeleteRow" 
                                                                        CssClass="btn btn-danger action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl='<%# Session["ep_post_authority"].ToString() == "1" ? "~/ResourceImages/unpost.png": "~/ResourceImages/final_delete.png" %>' 
                                                                        OnCommand="deleteRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("voucher_ctrl_nbr") + "," + Eval("payroll_year")%> ' 
                                                                        Enabled='<%# Eval("post_status").ToString() == "Y" || Eval("post_status").ToString() == "R" ? false: true %>'
                                                                        ToolTip='<%# Session["ep_post_authority"].ToString() == "0" ? "Delete Existing Record": "UnPost From Card" %>'/>
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
                                                                         OnCommand="imgbtn_print_Command1" 
                                                                         CommandArgument='<%# Eval("voucher_ctrl_nbr") + "," + Eval("payroll_year")%> ' 
                                                                         tooltip='<%# Session["ep_post_authority"].ToString() == "0" ? "Show Report Option": "Print Card" %>'/>
                                                                <%  }
                                                                %>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_dep" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</form>

    <div class="modal fade" id="modal_cafoa" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document" >
                <div class="modal-content  modal-content-add-edit">
                    <div class="modal-header bg-warning" >
                            <h5 class="modal-title text-white" ><asp:Label runat="server" Text="Certification On Appropriations, Fund and Obligation of Allotment"></asp:Label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-6" style="padding-right:30px !important">
                                <button class="btn btn-primary btn-sm pull-right" type="button" onclick="btn_cafoa_action('','add')"><i class="fa fa-plus-square"></i> Add New</button>
                            </div>
                            <div class="col-lg-6"></div>
                            <div class="col-lg-6">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered" id="datalist_grid_cafoa" style="width:100% !important;">
                                        <thead>
                                        <tr>
                                            <th style="width:10% !important">Code</th>
                                            <th style="width:50% !important">Description</th>
                                            <th style="width:10% !important">Amount</th>
                                            <th style="width:10% !important"></th>
                                        </tr>
                                        </thead>
                                    </table>
                                </div>
                                <div class="form-group row text-right">
                                    <div class="col-lg-12">
                                        <b><small id="total_amount_cafoa_old" class="pr-3"></small></b>
                                        <h2 id="total_amount_cafoa" class="pr-3" style="margin-bottom:0px !important"></h2>
                                        <b><small id="total_amount_cafoa_grand" class="pr-3"></small></b>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group row">
                                    <div class="col-lg-12">
                                        <label>Payee</label>
                                        <input class="form-control form-control-sm" id="id_payee"/>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group row">
                                            <div class="col-lg-4">
                                                <label>Obligation No</label>
                                                <input class="form-control form-control-sm" id="id_obr_nbr" />
                                            </div>
                                            <div class="col-lg-4">
                                                <label>Approved Amount</label>
                                                <input class="form-control form-control-sm text-right" id="id_approved_amt" />
                                            </div>
                                            <div class="col-lg-4">
                                                <label>DV No</label>
                                                <input class="form-control form-control-sm" id="id_dv_nbr" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-12">
                                        <label>Request</label>
                                        <input class="form-control form-control-sm" id="id_request"/>
                                    </div>
                                    <div class="col-lg-12">
                                        <label>Charged to</label>
                                        <input class="form-control form-control-sm" id="id_charged_to"/>
                                    </div>
                                </div>
                                <div class="form-group row">
                                     <div class="col-lg-12">
                                        <hr style="margin-bottom:5px;margin-top:5px" />
                                    </div>
                                    <div class="col-lg-12">
                                        <h4>Signatories</h4>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Requesting Official | <small class="text-danger"> Override</small></label>
                                        <select class="form-control-sm form-control" id="req_name" ></select>
                                        <i class="fa fa-info-circle text-success"></i><small class="badge text-success" id="req_name_lbl"></small>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Requesting Official Designation | <small class="text-danger"> Override</small></label>
                                        <input class="form-control-sm form-control" id="req_desig"/>
                                        <i class="fa fa-info-circle text-success"></i><small class="badge text-success" id="req_desig_lbl"></small>
                                    </div>
                                    <div class="col-lg-12">
                                        <hr style="margin-bottom:5px;margin-top:5px" />
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Budget Officer</label>
                                        <select class="form-control-sm form-control" id="budget_name"></select>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Budget Officer Designation</label>
                                        <input class="form-control-sm form-control" id="budget_desig"/>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Treasurer</label>
                                        <select class="form-control-sm form-control" id="treasurer_name"></select>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Treasurer Designation</label>
                                        <input class="form-control-sm form-control" id="treasurer_desig"/>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Accountant</label>
                                        <select class="form-control-sm form-control" id="pacco_name"></select>
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Accountant Designation</label>
                                        <input class="form-control-sm form-control" id="pacco_desig"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-primary" type="button" onclick="saveall_cafoa()"><i class="fa fa-plus-circle"></i> Save All</button>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="modal_cafoa_details" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-md" role="document" >
                <div class="modal-content  modal-content-add-edit">
                    <div class="modal-header bg-success" >
                            <h5 class="modal-title text-white" ><asp:Label runat="server" Text="CAFOA DETAILS"></asp:Label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body with-background" >
                            <div class="row">
                                
                                <div class="col-lg-12">
                                    <label>Function</label>
                                    <select class="form-control form-control-sm" id="function_code"></select>
                                </div>
                                <div class="col-lg-12">
                                    <label>Function Name</label>
                                    <input class="form-control form-control-sm" id="function_name" disabled/>
                                </div>
                                <div class="col-lg-4">
                                    <label>Allotment Code</label>
                                    <input class="form-control form-control-sm text-center" id="allotment_code" />
                                </div>
                                <div class="col-lg-4">
                                    <label>Account Code</label>
                                    <input class="form-control form-control-sm text-center" id="account_code"/>
                                </div>
                                <div class="col-lg-4">
                                    <label>Amount</label>
                                    <input class="form-control form-control-sm text-right" id="account_amt"/>
                                </div>
                            </div>
                            <div class="row" style="display:none">
                                <div class="col-lg-4">
                                    <label>Row Id</label>
                                    <input class="form-control form-control-sm" disabled id="row_id" />
                                </div>
                                <div class="col-lg-4">
                                    <label>Seq. No</label>
                                    <input class="form-control form-control-sm" disabled id="seq_nbr" />
                                </div>
                                <div class="col-lg-12">
                                    <label>RAAO</label>
                                    <input class="form-control form-control-sm" id="raao_code"/>
                                </div>
                                <div class="col-lg-12">
                                    <label>OOE</label>
                                    <input class="form-control form-control-sm" id="ooe_code"/>
                                </div>
                            </div>
                        </div>
                    <div class="modal-footer">
                        <button data-dismiss="modal" class="btn btn-danger text-left">Close</button>
                        <button class="btn btn-primary" type="button" onclick="btn_save_cafoa_dtl()"><i class="fa fa-plus-circle"></i> Save</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modal_print_preview" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document" >
                <div class="modal-content  modal-content-add-edit">
                    <div class="modal-header bg-success" >
                            <h5 class="modal-title text-white" ><asp:Label runat="server" Text="Preview Report"></asp:Label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                    <div class="modal-body with-background" style="padding:0px !important">
                        <div class="row">
                            <div class="col-lg-12">
                                <iframe style="width:100% !important;height:700px !important;border:0px none;" id="iframe_print_preview"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   
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
                $('#<%= id_header.ClientID %>').click()
            }
            else if (target_tab == 2)
            {
                $('#<%= id_optional.ClientID %>').click()
            }
            else if (target_tab == 3)
            {
                $('#<%= id_loans.ClientID %>').click()
            }
            else if (target_tab == 4)
            {
                $('#<%= id_header.ClientID %>').click()
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
    <script type="text/javascript">
        function openLoading() {

            $('#Loading').modal({
                keyboard: false,
                backdrop: "static"
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
     <script type="text/javascript">
        //function openModal_Header() {
        //    $('#add-header').modal({
        //        keyboard: false,
        //        backdrop:"static"
        //    });
        //    show_date();
        //    };
            function show_date()
            {
                $('#<%= txtb_period_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
                $('#<%= txtb_period_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });

                if ($('#<%= txtb_period_from.ClientID %>').prop('disabled') == true)
                 {
                        var parent_div = $('#<%= txtb_period_from.ClientID %>').closest("div");
                        parent_div.find("i").remove();    
                 }

                if ($('#<%= txtb_period_to.ClientID %>').prop('disabled') == true)
                 {
                        var parent_div = $('#<%= txtb_period_to.ClientID %>').closest("div");
                        parent_div.find("i").remove();    
                 }

            }
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#add').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>
    <script type="text/javascript">
        function openSelectReport() {
            $('#SelectReport').modal({
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

        $(document).on('hidden.bs.modal', '.modal', function () {
            if ($('.modal.show').length > 0) {
                $('body').addClass('modal-open'); // Re-add modal-open if another modal is still open
            }
        });
        $(document).ready(function () {
            init_table_data_cafoa([]);
           $('#<%= gv_dataListGrid.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight_on_grid');
           }, function () {
                   $(this).removeClass('highlight_on_grid');
           });
        });
    </script> 

    <script type="text/javascript">
        function btn_cafoa()
        {
            $('#req_name').on('change', function ()
            {
                $('#req_desig').val(this.options[this.selectedIndex].getAttribute("designation_head2"))
            })
            $('#budget_name').on('change', function ()
            {
                $('#budget_desig').val(this.options[this.selectedIndex].getAttribute("designation_head2"))
            })
            $('#treasurer_name').on('change', function ()
            {
                $('#treasurer_desig').val(this.options[this.selectedIndex].getAttribute("designation_head2"))
            })
            $('#pacco_name').on('change', function ()
            {
                $('#pacco_desig').val(this.options[this.selectedIndex].getAttribute("designation_head2"))
            })
            

            DepartmentSignatories()
            RetrieveFunctions('function_code')

            $('#function_code').on('change', function ()
            {
                console.log(this.options[this.selectedIndex])
                $('#function_name').val(this.options[this.selectedIndex].getAttribute("function_name"))
            })
            var year        = $('#<%= ddl_year.ClientID %>').val()
            var ctrl_nbr    = $('#<%= selected_voucher_ctrl_nbr.ClientID %>').val()
            var template    = $('#<%= ddl_payroll_template.ClientID %>').val()
            
            Cafoa(year,ctrl_nbr,template,"VOUCHER")
            $('#modal_cafoa').modal({ backdrop: 'static', keyboard: false });
        }
        function Cafoa(payroll_year,payroll_registry_nbr,payrolltemplate_code,par_cafoa_type)
        {
            $.ajax({
                type        : "POST",
                url         : "../cPayRegistry/cPayRegistry.aspx/CafoaList",
                data        : JSON.stringify({ payroll_year: payroll_year,payroll_registry_nbr:payroll_registry_nbr,payrolltemplate_code:payrolltemplate_code,par_cafoa_type:par_cafoa_type }),
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    var parsed_hdr = JSON.parse(response.d)
                    parsed = parsed.dt
                    for (var i = 0; i < parsed.length; i++)
                    {
                        parsed[i]["payrolltemplate_code"] = payrolltemplate_code
                    }
                    parsed_hdr = parsed_hdr.dt_header
                    oTable_cafoa.fnClearTable();
                    datalistgrid_cafoa = parsed;
                    if (parsed)
                    {
                        if (parsed.length > 0)
                        {
                            if (parsed_hdr.length > 0)
                            {
                                var header = parsed_hdr[0]
                                $('#id_payee').val(header.employee_name)
                                $('#id_obr_nbr').val("")
                                $('#id_approved_amt').val("")
                                $('#id_dv_nbr').val("")
                                $('#id_request').val(header.voucher_descr1)
                                $('#id_charged_to').val(header.charge_to)
                                
                                $('#req_name').val("")
                                $('#req_desig').val("")	
                                $('#req_name_lbl').text(header.repsig_dept_name_grp)
                                $('#req_desig_lbl').text(header.repsig_dept_desig_grp)	
                                $('#budget_name').val(header.repsig_pbo_empl_id)
                                $('#budget_desig').val(header.repsig_pbo_desig)	
                                $('#treasurer_name').val(header.repsig_pto_empl_id)
                                $('#treasurer_desig').val(header.repsig_pto_desig)
                                $('#pacco_name').val(header.repsig_pacco_empl_id)
                                $('#pacco_desig').val(header.repsig_pacco_desig)     
                            }
                           
                            oTable_cafoa.fnAddData(parsed);
                            var allRows = oTable_cafoa.fnGetData();
                            var total  = 0;
                            for (var i = 0; i < allRows.length; i++)
                            {
                                total =+total  + parseFloat(allRows[i]["account_amt"])
                            }
                            $('#total_amount_cafoa_old').text(currency(total)); 
                            $('#id_approved_amt').val(currency(total)); 
                            total_cafoa()
                        }
                        else
                        {
                            alert("No Data Found!");
                        }
                    }
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }
        var init_table_data_cafoa = function (par_data)
        {
            datalistgrid_cafoa = par_data;
            oTable_cafoa       = $('#datalist_grid_cafoa').dataTable(
                {
                    data        : datalistgrid_cafoa,
                    sDom        : 'rt<"bottom">',
                    //pageLength  : 2,
                    paging      : false,
                    columns:
                    [
                        {
                            "mData": "function_code",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-center btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "function_name",
                            "mRender": function (data, type, full, row)
                            {
                                return "<span class='btn-block'>" + data + " <br> <small class='badge'> "+full["account_code"]+" </span></span>"
                            }
                        },
                        {
                            "mData": "account_amt",
                            "mRender": function (data, type, full, row) {
                                return "<span class='btn-block text-right'>" + currency(data) + "</span>"
                            }
                        },
                        {
                            "mData": "",
                            "mRender": function (data, type, full, row) {
                                return '<button type="button" class="btn text-center btn-info btn-sm" onclick=\'btn_cafoa_action(' + row["row"] + ',"update")\' data-toggle="tooltip" data-placement="top" title="Edit">  <i class="fa fa-edit"></i></button >'
                                     + '<button type="button" class="btn text-center btn-danger btn-sm" onclick=\'btn_cafoa_action(' + row["row"] + ',"delete")\' data-toggle="tooltip" data-placement="top" title="Remove">  <i class="fa fa-trash"></i></button >'
                            }
                        },
                    ],
                });
            
            
            
        }
        function btn_cafoa_action(row,action)
        {
            action_cafoa = action
            $('#row_id').val("")
            $('#seq_nbr').val("")
            $('#function_code').val("")
            $('#function_name').val("")
            $('#allotment_code').val("")
            $('#account_code').val("")
            $('#account_amt').val("")
            $('#raao_code').val("")
            $('#ooe_code').val("")

            var template    = $('#<%= ddl_payroll_template.ClientID %>').val()
            if (action == "update")
            {
                var data = oTable_cafoa.fnGetData(row)
                $('#row_id').val(row)
                $('#seq_nbr').val(parseInt(data.seq_nbr))
                $('#function_code').val(data.function_code)
                $('#function_name').val(data.function_name)
                $('#allotment_code').val(data.allotment_code)
                $('#account_code').val(data.account_code)
                $('#account_amt').val(currency(data.account_amt))
                $('#raao_code').val(data.raao_code)
                $('#ooe_code').val(data.ooe_code)
                $('#payrolltemplate_code').val(template)

                $('#modal_cafoa_details').modal({ backdrop: 'static', keyboard: false });

            } else if (action == "add")
            {
                $('#seq_nbr').val(getLastRowPlusOne_Cafoa())
                
                $('#modal_cafoa_details').modal({ backdrop: 'static', keyboard: false });
            }
            else if (action == "delete")
            {
                if (confirm("Are you sure you want to delete this row?"))
                {
                    oTable_cafoa.fnDeleteRow(row);
                    total_cafoa()
                }
            }
        }
        function DepartmentSignatories()
        {
            $.ajax({
                type        : "POST",
                url         : "../cPayRegistry/cPayRegistry.aspx/DepartmentSignatories",
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    if (parsed.length > 0)
                    {
                        var select_req_name         = document.getElementById('req_name');
                        var select_budget_name      = document.getElementById('budget_name');
                        var select_treasurer_name   = document.getElementById('treasurer_name');
                        var select_pacco_name       = document.getElementById('pacco_name');
                        // Clear existing options if any
                        select_req_name.innerHTML       = "";
                        select_budget_name.innerHTML    = "";
                        select_treasurer_name.innerHTML = "";
                        select_pacco_name.innerHTML     = "";
                        // Add Empty
                        var option1      = document.createElement("option");
                        option1.text     = "-- Select Here--"; 
                        option1.value = "";

                        select_budget_name.appendChild(option1);
                        select_treasurer_name.appendChild(option1);
                        select_pacco_name.appendChild(option1);
                        select_req_name.appendChild(option1);

                        // Add options to the select element
                        
                        for (var i = 0; i < parsed.length; i++)
                        {
                            var option      = document.createElement("option");
                            option.text     = parsed[i].employee_name_format2 
                            option.value    = parsed[i].empl_id;
                            option.setAttribute("designation_head2", parsed[i].designation_head2);
                            select_budget_name.appendChild(option);
                        }
                        for (var i = 0; i < parsed.length; i++)
                        {
                            var option      = document.createElement("option");
                            option.text     = parsed[i].employee_name_format2 
                            option.value    = parsed[i].empl_id;
                            option.setAttribute("designation_head2", parsed[i].designation_head2);
                            select_treasurer_name.appendChild(option);
                        }
                        for (var i = 0; i < parsed.length; i++)
                        {
                            var option      = document.createElement("option");
                            option.text     = parsed[i].employee_name_format2 
                            option.value    = parsed[i].empl_id;
                            option.setAttribute("designation_head2", parsed[i].designation_head2);
                            select_pacco_name.appendChild(option);
                        }
                        for (var i = 0; i < parsed.length; i++)
                        {
                            var option              = document.createElement("option");
                            option.text             = parsed[i].employee_name_format2 
                            option.value            = parsed[i].empl_id;
                            option.setAttribute("designation_head2", parsed[i].designation_head2);
                            select_req_name.appendChild(option);
                        }
                        
                    } else
                    {
                        alert("No Data Found!");
                    }
                    
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }

        function RetrieveFunctions(select_id)
        {
            $.ajax({
                type        : "POST",
                url         : "../cPayRegistry/cPayRegistry.aspx/RetrieveFunctions",
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    if (parsed.length > 0)
                    {
                        var select = document.getElementById(select_id);
                        // Clear existing options if any
                        select.innerHTML = "";
                        // Add Empty
                        var option1      = document.createElement("option");
                        option1.text     = "-- Select Here--"; 
                        option1.value    = "";
                        select.appendChild(option1);
                        // Add options to the select element
                        for (var i = 0; i < parsed.length; i++)
                        {
                            var option              = document.createElement("option");
                            option.text             = parsed[i].function_code + " - " + parsed[i].function_name ; 
                            option.value            = parsed[i].function_code; 
                            option.setAttribute("function_name", parsed[i].function_name);
                            select.appendChild(option);
                        }
                    } else
                    {
                        alert("No Data Found!");
                    }
                    
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }
        

        function btn_save_cafoa_dtl()
        {
            var upsert =
            {
                 payroll_year           : $('#<%= ddl_year.ClientID %>').val()
                ,payroll_registry_nbr   : $('#<%= selected_voucher_ctrl_nbr.ClientID %>').val()
                ,seq_nbr                : parseInt($('#seq_nbr').val())
                ,function_code          : $('#function_code').val()
                ,allotment_code         : $('#allotment_code').val()
                ,function_name          : $('#function_name').val()
                ,account_code           : $('#account_code').val()
                ,account_amt            : parseFloat($('#account_amt').val().replace(",","").replace(",",""))
                ,raao_code              : $('#raao_code').val()
                ,ooe_code               : $('#ooe_code').val()
                ,account_short_title    : $('#function_code option:selected').text()
                ,payrolltemplate_code   : $('#<%= ddl_payroll_template.ClientID %>').val()
            };

            if (upsert.function_code == "")
            {
                alert("Function is Required!")
                return
            } else if (upsert.allotment_code == "")
            {
                alert("Allotment Code is Required!")
                return
            }else if (upsert.account_code == "")
            {
                alert("Account Code is Required!")
                return
            }
            else if (upsert.account_amt == "" || upsert.account_amt == NaN)
            {
                alert("Amount is Required!")
                return
            }
            
            var rowIndex = $('#row_id').val()
            if (action_cafoa == "update")
            {
                oTable_cafoa.fnUpdate(upsert, rowIndex, undefined, false);
            }
            else if (action_cafoa == "add")
            {
                oTable_cafoa.fnAddData(upsert);
            } 
            else
            {
                alert("No Action Selected")
                return;
            }
            total_cafoa()
            
            $('#modal_cafoa_details').modal("hide");
        }
        function getLastRowPlusOne_Cafoa()
        {
            var rowCount = oTable_cafoa.fnGetData().length; // Get total row count
            if (rowCount === 0) return 1; // If no rows, start from 1

            var lastRowData = oTable_cafoa.fnGetData(rowCount - 1); // Get last row data
            var lastValue = parseInt(lastRowData.seq_nbr, 10) || 0; // Convert to number
            return lastValue + 1;
        }

        function saveall_cafoa()
        {
            if (big_cafoa_lbl != small_cafoa_lbl)
            {
                alert("CAFOA/OBR Amount is not equal!")
                return;
            }


            var cafoa_hdr = {
                                payroll_year		 : $('#<%= ddl_year.ClientID %>').val()
                               ,payroll_registry_nbr : $('#<%= selected_voucher_ctrl_nbr.ClientID %>').val()
                               ,payee				 : $('#id_payee').val()	
                               ,obr_nbr				 : $('#id_obr_nbr').val()
                               ,approved_amt		 : $('#id_approved_amt').val()	
                               ,dv_nbr				 : $('#id_dv_nbr').val()	
                               ,request_descr		 : $('#id_request').val()	
                               ,charged_to			 : $('#id_charged_to').val()	
                               ,req_empl_id			 : $('#req_name').val()
                               ,req_name			 : $('#req_name option:selected').text()
                               ,req_desig			 : $('#req_desig').val()	
                               ,budget_empl_id		 : $('#budget_name').val()
                               ,budget_name			 : $('#budget_name option:selected').text()
                               ,budget_desig		 : $('#budget_desig').val()	
                               ,treasurer_empl_id	 : $('#treasurer_name').val()
                               ,treasurer_name		 : $('#treasurer_name option:selected').text()
                               ,treasurer_desig		 : $('#treasurer_desig').val()
                               ,pacco_empl_id		 : $('#pacco_name').val()
                               ,pacco_name			 : $('#pacco_name option:selected').text()
                               ,pacco_desig			 : $('#pacco_desig').val()     
                            }

            $.ajax({
                type        : "POST",
                url         : "../cPayRegistry/cPayRegistry.aspx/saveall_cafoa",
                data        : JSON.stringify({
                                                 data_dtl : JSON.stringify(oTable_cafoa.fnGetData())
                                                ,data_hdr : JSON.stringify(cafoa_hdr)
                                            }),
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = response.d
                    if (parsed == "success")
                    {
                        alert(parsed);
                        $('#modal_cafoa').modal("hide");
                    } else
                    {
                        alert(parsed);
                    }
                    
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }

        function total_cafoa()
        {
            var allRows = oTable_cafoa.fnGetData();
            var total  = 0;
            for (var i = 0; i < allRows.length; i++)
            {
                total =+total  + parseFloat(allRows[i]["account_amt"])
            }
            $('#total_amount_cafoa').text(currency(total)); 

            big_cafoa_lbl   = $('#total_amount_cafoa').text().replace(",","").replace(",","")
            small_cafoa_lbl = $('#total_amount_cafoa_old').text().replace(",","").replace(",","")
            grand_cafoa_lbl = (parseFloat(big_cafoa_lbl) - parseFloat(small_cafoa_lbl)).toString()
            $('#total_amount_cafoa_grand').text(currency(grand_cafoa_lbl)); 

            $('#total_amount_cafoa').removeClass('blink-red')
            if (big_cafoa_lbl != small_cafoa_lbl)
            {
                $('#total_amount_cafoa').addClass('blink-red')
            } 
        }

        function currency(d)
        {
            var retdata = ""
            if (d == null || d == "" || d == undefined) {
                return retdata = "0.00"
            }
            else
            {
                retdata = parseFloat(d).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,')
                return retdata
            }
        }
    </script>

    <script type="text/javascript">
        function check_payroll()
        {
            var ctrlno          = $('#<%= lbl_registry_no.ClientID %>').text()
            var empl_id         = $('#<%= ddl_empl_id.ClientID %>').val()
            var period_from     = $('#<%= txtb_period_from.ClientID %>').val()
            var period_to       = $('#<%= txtb_period_to.ClientID %>').val()

            if (period_from == "" || period_to == "" )
            {
                alert("Period from and Period to is Required!")
                return
            }

            if (empl_id == "")
            {
                alert("Employee ID is Required!")
                return
            }

            $.ajax({
                type: "POST",
                url: "../../Default.aspx/CheckPayrollExists",  // Make sure this URL is correct!
                data: JSON.stringify({
                     par_payroll_registry_nbr    : ctrlno
                    ,par_empl_id                 : empl_id
                    ,par_period_from             : period_from
                    ,par_period_to               : period_to
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    if (parsed.length > 0)
                    {
                        var period_covered = new Date(period_from).toLocaleDateString("en-US", { year: "numeric", month: "long", day: "numeric" }).toUpperCase()
                                     +" - "+ new Date(period_to).toLocaleDateString("en-US", { year: "numeric", month: "long", day: "numeric" }).toUpperCase()
                        $('#id_hdr_payroll_check').text("PAYROLL EXIST FOR THE PERIOD "+ period_covered + " ("+ parsed[0].employee_name + ")")
                        let list = $("#dataList");
                        list.empty(); 
                        
                        $.each(parsed, function (index, item)
                        {
                            let post_status = item.post_status_descr == "NOT POSTED" ? "danger" : "success";
                            let listItem = `<li class="list-group-item">
                                            <div class="form-group row">
                                                <div class="col-lg-12 font-weight-bold">
                                                    ${item.payrolltemplate_descr}
                                                </div>
                                                <div class="col-lg-8">
                                                    <small>${item.payroll_registry_descr}</small><br />
                                                    <small>${item.period_covered}</small>
                                                </div>
                                                <div class="col-lg-4 text-center">
                                                    <span class="badge badge-${post_status}">${item.post_status_descr}</span>
                                                </div>
                                            </div>
                                        </li>`;

                            list.append(listItem);
                        });
                        $('#modal_payroll_exists').modal({ backdrop: 'static', keyboard: false });
                        
                    }
                },
                error: function (xhr)
                {
                    alert("AJAX Error")
                    console.error("AJAX Error:", xhr.status, xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>
