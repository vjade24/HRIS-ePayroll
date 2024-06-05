<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cVoucherDTL.aspx.cs" Inherits="HRIS_ePayroll.View.cVoucherDTL" %>
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
        <!-- The Modal - Select Report -->
        <%--<asp:UpdatePanel ID="edit_delete_notify" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
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
                                        <div class="col-12" style="margin-bottom: 5px;">
                                            
                                            <asp:DropDownList ID="ddl_select_report" CssClass="form-control form-control-sm" runat="server" Width="100%"  OnSelectedIndexChanged="ddl_select_report_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:Label ID="LblRequired0" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="col-12">
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
        </asp:UpdatePanel>--%>
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

        <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
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
                            
                            <div class="row">
                                <%--<div class="col-12 text-right" style="margin-top:-15px;display:none">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_registry_no" runat="server" Text="" Font-Underline="true" CssClass="font-weight-bold" Visible="false"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>--%>

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
                                    <asp:Label runat="server" style="float:left;padding-top:2px; margin-right:5px;" MaxLength="15" Text="Voucher Nbr :" CssClass="font-weight-bold" ></asp:Label>
                                </div>


                                <div class="col-12"  style="display:none">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>

                                <div class="col-3">
                                     <asp:Label ID="Label2" runat="server" Text="Employee's Name:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-6" >
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                             <%--<asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_id_SelectedIndexChanged"></asp:DropDownList>--%>
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


                            <%--<div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>

                                <div class="col-3">
                                     <asp:Label ID="Label11" runat="server" Text="Position Title :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_position" Font-Bold="true" CssClass="form-control form-control-sm" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>--%>

                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                

                            </div>
                            <%--<div class="row" >
                                <div class="col-3">
                                    <a href="#demo" data-toggle="collapse" id="btn_show">Department <small>(Show)</small>:</a>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_department" CssClass="form-control form-control-sm"  Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row collapse" id="demo">
                                <div class="col-3">
                                    <label class="font-weight-bold">Sub-Department:</label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_subdep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Division:</label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_division" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Section:</label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Fund Charges :</label>
                                </div>
                                <div class="col-9">
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
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="Prepared By (Name):" ></asp:label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_prepared_name" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="Prepared By (Desig.):"></asp:label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_prepared_design" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12" runat="server" >
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                            </div>--%>


                            <div class="row" >
                                <div class="col-6">
                                    <div class="form-group row">
                                            
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" class="font-weight-bold" ID="lbl_rate_descr" Text="Monthly Rate:" ></asp:Label>
                                             
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_rate_amount" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-6">
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
                                                                <asp:ListItem Value="1" Text="First Claim (Promotion)"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                </div>--%>

                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-4">
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
                            </div>

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                        <div class="row" runat="server" id="div_no_of_days">
                                            <div class="col-3" >
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
                                                    <asp:label runat="server" Font-Bold="true" Text="Gross Pay :" Font-Size="Small"></asp:label>
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
                                            <asp:label runat="server" Font-Bold="true" Text="Lwop (MR) :" Font-Size="Small"></asp:label>
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
                                            <asp:label runat="server" Font-Bold="true" Text="Lwop (PERA) :" Font-Size="Small"></asp:label>
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
                                                    <asp:label runat="server" Font-Bold="true" Text="BIR Tax :" Font-Size="Small"></asp:label>
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
                                            <asp:label runat="server" Font-Bold="true" Text="Mandatory :" Font-Size="Small"></asp:label>
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
                                            <asp:label runat="server" Font-Bold="true" Text="Optional :" Font-Size="Small"></asp:label>
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
                                            <asp:label runat="server" Font-Bold="true" Text="Loans :" Font-Size="Small"></asp:label>
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
                                                    <asp:label runat="server" Font-Bold="true" Text="Refund Total :"  Font-Size="Small" ID="lbl_netpay_descr"></asp:label>
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
                                            <a class="nav-link active" id="id_header" data-toggle="tab" href="#header_tab" role="tab" aria-controls="header_tab" aria-selected="false" style="font-weight:bold;padding: .09rem 1rem;" aria-expanded="true" runat="server">DATE PERIOD</a>
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
                                            <li class="nav-item">
                                            <a class="nav-link" id="id_oth_claims_refund" data-toggle="tab" href="#oth_claims_refund" role="tab" aria-controls="oth_claims_refund" aria-selected="false" style="font-weight:bold;padding: .09rem 1rem;" runat="server" visible="false">OTHER CLAIMS/REFUND</a>
                                            </li>
                                           <%-- <li class="nav-item">
                                            <a class="nav-link" id="id_details" data-toggle="tab" href="#details_tab" role="tab" aria-controls="details_tab" aria-selected="false" style="font-weight:bold;padding: .09rem 1rem;" runat="server">DETAILS</a>
                                            </li>--%>
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
                                                <%--<div class="col-12">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:label runat="server" Font-Bold="true" Text="Voucher Details #1:" ></asp:label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_voucher_descr1" TextMode="MultiLine" Rows="2"> </asp:TextBox>
                                                            <asp:Label ID="LblRequired201" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-12">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:label runat="server" Font-Bold="true" Text="Voucher Details #2:" ></asp:label>
                                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_voucher_descr2" TextMode="MultiLine" Rows="2"> </asp:TextBox>
                                                            <asp:Label ID="LblRequired202" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>--%>
                                            </div>
                                        </div>

                                        <%--MANDATORY DEDUCTIONS--%>
                                        <div class="tab-pane fade"  id="mandatory_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;min-height: 200px" aria-labelledby="id_mandatory" >
                                            <div class="row" style="padding-left:15px;padding-right:15px;padding-top:10px">
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
                                                        <div class="col-6" >
                                                            <asp:label runat="server" Font-Bold="true" Text="SIF :" Font-Size="Small"></asp:label>
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
                                                </div>
                                                <div class="col-6">
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
                                                <div class="col-6">
                                                    <div class="form-group row">
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="SSS :" Font-Size="Small"></asp:label>
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
                                                            <asp:label runat="server" Font-Bold="true" Text="HDMF-PS2 :" Font-Size="Small"></asp:label>
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
                                                            <asp:label runat="server" Font-Bold="true" Text="EHP :" Font-Size="Small"></asp:label>
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
                                                            <asp:label runat="server" Font-Bold="true" Text="HIP :" Font-Size="Small"></asp:label>
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


                                                <div class="col-6">
                                                    <div class="form-group row">
                                                        
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="CEAP :" Font-Size="Small"></asp:label>
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
                                                            <asp:label runat="server" Font-Bold="true" Text="GSIS A.I :" Font-Size="Small"></asp:label>
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px"">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_otherpremium_no5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired39"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <%-- BEGIN   - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem3"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem4"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem4"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem5"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem5"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem6"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem6" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem6"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem7"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem7" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem8"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem8" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem8"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem9"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem9" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem9"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem10"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
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
                                                <div class="col-4">
                                                    <div class="form-group row">
                                                    <div class="col-6" style="padding:0px;padding-right:5px">
                                                        <asp:label runat="server" Font-Bold="true" Text="Consol :"  Font-Size="Small"></asp:label>
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
                                                        <asp:label runat="server" Font-Bold="true" Text="Pol. Reg :" Font-Size="Small"></asp:label>
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

                                                <div class="col-4">
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
                                                                <asp:label runat="server" Font-Bold="true" Text="CCMPC :" Font-Size="Small"></asp:label>
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
                                                                    <asp:label runat="server" Font-Bold="true" Text="NHMFC :" Font-Size="Small"></asp:label>
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
                                                    <div class="col-4">
                                                        <div class="form-group row">
                                                            
                                                            
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:label runat="server" Font-Bold="true" Text="NAFC :" Font-Size="Small"></asp:label>
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
                                                                <asp:label runat="server" Font-Bold="true" Text="GSIS HELP :" Font-Size="Small"></asp:label>
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan10" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan10"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                       <%-- END     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--OTHER CLAIMS/REFUND--%>
                                        <div class="tab-pane fade show" id="oth_claims_refund" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;min-height: 200px;" aria-labelledby="id_oth_claims_refund" >
                                            <div class="row" style="padding-left:15px;padding-right:15px;padding-top:10px">
                                                
                                                <div class="col-5" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Payroll Descr. 1:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="txtb_payroll_descr1"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired300" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-4" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Type:" ></asp:label>
                                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_payroll_type_1">
                                                        <asp:ListItem Value="01" Text="Add to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="Ded. to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="03" Text="Refund"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-3" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount 1:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right " ID="txtb_payroll_amt1" Text="0.00"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired301" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-5" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Payroll Descr. 2:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="txtb_payroll_descr2"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired302" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-4" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Type:" ></asp:label>
                                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_payroll_type_2">
                                                        <asp:ListItem Value="01" Text="Add to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="Ded. to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="03" Text="Refund"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-3" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount 2:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right " ID="txtb_payroll_amt2" Text="0.00"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired303" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-5" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Payroll Descr. 3:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="txtb_payroll_descr3"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired304" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-4" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Type:" ></asp:label>
                                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_payroll_type_3">
                                                        <asp:ListItem Value="01" Text="Add to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="Ded. to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="03" Text="Refund"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-3" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount 3:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right " ID="txtb_payroll_amt3" Text="0.00"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired305" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-5" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Payroll Descr. 4:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="txtb_payroll_descr4"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired306" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-4" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Type:" ></asp:label>
                                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_payroll_type_4">
                                                        <asp:ListItem Value="01" Text="Add to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="Ded. to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="03" Text="Refund"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-3" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount 4:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right " ID="txtb_payroll_amt4" Text="0.00"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired307" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-5" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Payroll Descr. 5:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm " ID="txtb_payroll_descr5"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired308" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </div>
                                                <div class="col-4" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Type:" ></asp:label>
                                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_payroll_type_5">
                                                        <asp:ListItem Value="01" Text="Add to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="Ded. to Gross"></asp:ListItem>
                                                        <asp:ListItem Value="03" Text="Refund"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-3" >
                                                    <asp:label runat="server" Font-Bold="true" Text="Amount 5:" ></asp:label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right" ID="txtb_payroll_amt5" Text="0.00"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired309" runat="server" CssClass="lbl_required" ></asp:Label>
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

                                            <asp:Label ID="hidden_seq_no" runat="server" Visible="false"></asp:Label>
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
            <div class="col-3"><strong style="font-family:Arial;font-size:18px;color:white;"><%= ddl_payroll_template.SelectedItem.ToString().Trim() %> (Details)</strong></div>
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
                                                <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="30%" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="5" Value="5" />
                                                    <asp:ListItem Text="10" Selected="True" Value="10" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                            <asp:Label runat="server" Text="Entries"></asp:Label>|
                                                     <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-9">
                                    <div class="form-group row">
                                        <div class="col-2" style="padding-right:0px !important" >
                                            <a href="#demo" data-toggle="collapse" id="btn_show">Employee Name:</a>
                                        </div>
                                        <div class="col-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_main_employee_name" Enabled="false" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-1">
                                            <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                <ContentTemplate>
                                            
                                                    <% if (ViewState["page_allow_add"].ToString() == "1")
                                                        {  %>
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn pull-right"  Text="Add" OnClick="btnAdd_Click" />
                                                    <% }
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
                                <div class="row ">
                                    <div class="col-3"></div>
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
                                                        <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_month_SelectedIndexChanged"  Enabled="false">
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
                                                        <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"  Enabled="false"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row collapse" id="demo">
                                    <div class="col-3"></div>
                                    <div class="col-9">
                                        <div class="form-group row">
                                            <div class="col-2" style="padding-right:0px !important" >
                                                <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                            </div>
                                            <div class="col-10">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_payroll_template" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_payroll_template_SelectedIndexChanged"  Enabled="false"></asp:DropDownList>
                                                    
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-3">

                                    </div>
                                    <div class="col-9">
                                        <div class="form-group row">
                                            <div class="col-2" style="padding-right:0px !important;">
                                                <asp:Label runat="server" Text="Department :" ></asp:Label>
                                            </div>
                                            <div class="col-10">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                            <asp:DropDownList ID="ddl_dep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_SelectedIndexChanged"   Enabled="false"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" >
                                    <div class="col-3">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <a ID="lb_back" class="btn btn-info btn-sm font-weight-bold" href="/View/cVoucherHdr/cVoucherHdr.aspx" ><i class="fa fa-arrow-left"></i> Back To Voucher Header</a>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-9">
                                        <div class="form-group row">
                                            <div class="col-2" style="padding-right:0px !important;">
                                                <asp:Label runat="server" Text="Voucher Ctrl. No.:" ></asp:Label>
                                            </div>
                                            <div class="col-10">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" ID="voucher_nbr" CssClass="form-control form-control-sm" Width="100%" Enabled="false"></asp:TextBox>
                                                        <%--<asp:DropDownList ID="ddl_header" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true"  Enabled="false"></asp:DropDownList>--%>
                                                    </ContentTemplate>
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
                                               <asp:TemplateField HeaderText="DATE FROM" SortExpression="voucher_period_from">
                                                    <ItemTemplate>
                                                        <%# Eval("voucher_period_from") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="12%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="DATE TO" SortExpression="voucher_period_to">
                                                    <ItemTemplate>
                                                        <%# Eval("voucher_period_to") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="12%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="GROSS PAY" SortExpression="gross_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("gross_pay") %>&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="DED. AMT." SortExpression="deduction_amount">
                                                    <ItemTemplate>
                                                        <%# Eval("deduc_amt") %>&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("net_pay") %>&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="STATUS" SortExpression="post_status_descr">
                                                    <ItemTemplate>
                                                        <%# Eval("post_status_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="11%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <%--<% 
                                                                if (
                                                                    ddl_payroll_template.SelectedValue == "608" || // Template Code for : Maternity
                                                                    ddl_payroll_template.SelectedValue == "708" || // Template Code for : Maternity
                                                                    ddl_payroll_template.SelectedValue == "808"    // Template Code for : Maternity
                                                                    )
                                                                {
                                                                %>
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
                                                                    />
                                                                <%   }

                                                                %>--%>

                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="imgbtn_editrow1" 
                                                                        CssClass="btn btn-primary action" 
                                                                        EnableTheming="true"  
                                                                        runat="server" 
                                                                        ImageUrl="~/ResourceImages/final_edit.png"
                                                                        OnCommand="editRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("voucher_ctrl_nbr") + "," + Eval("payroll_year") + "," + Eval("seq_no")%> ' 
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
                                                                        ImageUrl="~/ResourceImages/final_delete.png"
                                                                        OnCommand="deleteRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("voucher_ctrl_nbr") + "," + Eval("payroll_year") + "," + Eval("seq_no")%> '
                                                                        Enabled='<%# Eval("post_status").ToString() == "Y" || Eval("post_status").ToString() == "R" ? false: true %>'
                                                                        ToolTip="Delete Existing Record"/>
                                                                <% }
                                                                %>
                                                                <%--<% if (ViewState["page_allow_print"].ToString() == "1")
                                                                    {
                                                                %>
                                                                     <asp:ImageButton ID="imgbtn_print" 
                                                                         CssClass="btn btn-success action" 
                                                                         EnableTheming="true" runat="server" 
                                                                         ImageUrl="~/ResourceImages/print1.png" 
                                                                         style="padding-left: 0px !important;" 
                                                                         OnCommand="imgbtn_print_Command1" 
                                                                         CommandArgument='<%# Eval("voucher_ctrl_nbr")%> ' 
                                                                         tooltip='<%# Session["ep_post_authority"].ToString() == "0" ? "Show Report Option" : "Print Card" %>'/>
                                                                <%  }
                                                                %>--%>
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
            else if (target_tab == 5)
            {
                $('#<%= id_oth_claims_refund.ClientID %>').click()
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
        function openModal_Header() {
            $('#add-header').modal({
                keyboard: false,
                backdrop:"static"
            });
            show_date();
            };
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

        $(document).ready(function () {
           $('#<%= gv_dataListGrid.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight_on_grid');
           }, function () {
                   $(this).removeClass('highlight_on_grid');
           });
        });
    </script> 
</asp:Content>
