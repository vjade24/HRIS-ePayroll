<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayRegistrySalaryCasual.aspx.cs" Inherits="HRIS_ePayroll.View.cPayRegistrySalaryCasual" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">

    <style type="text/css">
        .lbl_required {
            font-size: 8px !important;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server" >

        <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>


        <!-- The Modal - Select Report -->
        <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
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
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body with-background">
                                        <div class="row">
                                            <div class="col-12" style="margin-bottom: 5px;">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label  runat="server" Text="Select Report:"></asp:Label>
                                                        <asp:DropDownList ID="ddl_select_report" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_select_report_SelectedIndexChanged">
                                                            <asp:ListItem Value="01" Text="Payslip"></asp:ListItem>
                                                            <asp:ListItem Value="02" Text="Net Take Home Pay"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-8">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label  runat="server" Text="OR Number:" ID="lbl_or_nbr" Visible="false"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtb_or_nbr" CssClass="form-control form-control-sm" Width="100%" MaxLength="25" Visible="false"></asp:TextBox>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4">
                                                <asp:UpdatePanel runat="server" ID="Update_or_date" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label  runat="server" Text="OR Date:" ID="lbl_or_date" Visible="false"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtb_or_date" CssClass="form-control form-control-sm my-date text-center" MaxLength="10" Width="100%" Visible="false"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-1" >
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
            </asp:UpdatePanel>


            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <!-- The Modal - Generating Report -->
                    <div class="modal fade" id="Loading">
                        <div class="modal-dialog modal-dialog-centered modal-lg">
                            <div class="modal-content text-center">
                                <!-- Modal body -->
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
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
                    <div class="modal-dialog modal-dialog-centered  modal-2lg" role="document">
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
                        <div class="modal-body with-background" runat="server" style="padding-top: 5px;">
                            <div class="row">
                                    <div class="form-group row" style="display:none">
                                        <div class="col-12 text-right" style="margin-top:-15px;">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" ></asp:Label>
                                                <asp:Label ID="lbl_registry_no" runat="server" Text="" Font-Underline="true" CssClass="font-weight-bold" ></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-3" style="display:none">
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
                                

                                <div class="col-3">
                                     <asp:Label ID="Label2" runat="server" Text="Employee's Name:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-6" style="padding-left:0px">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                             <asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_id_SelectedIndexChanged"  ></asp:DropDownList>
                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_employeename" CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
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
                                            <asp:TextBox runat="server" ID="txtb_empl_id" Font-Bold="true" CssClass="form-control form-control-sm font-weight-bold text-center"  Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>

                                <div class="col-3" >
                                     <asp:Label ID="Label4" runat="server" Text="Position Title :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-9"  style="padding-left:0px">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_position" CssClass="form-control form-control-sm"  Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>

                            </div>
                                
                            <div class="row">
                                <div class="col-3">
                                    <a href="#demo" data-toggle="collapse" id="btn_show">Department <small>(Show)</small>:</a>
                                    <%--<label class="font-weight-bold">Department:</label>--%>
                                </div>
                                <div class="col-9" style="padding-left:0px">
                                    <asp:UpdatePanel ID="UpdateDep" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_dep" Font-Bold="true" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row collapse" id="demo">
                                <div class="col-3">
                                    <label class="font-weight-bold">Sub-Department:</label>
                                </div>
                                <div class="col-9" style="padding-left:0px">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_subdep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Division:</label>
                                </div>
                                <div class="col-9" style="padding-left:0px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_division" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Section:</label>
                                </div>
                                <div class="col-9" style="padding-left:0px">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Fund Charges :</label>
                                </div>
                                <div class="col-9" style="padding-left:0px">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_fund_charges" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            
                            <div class="row" >
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" class="font-weight-bold" ID="lbl_rate_descr" Text="M/D/H Rate"  Font-Size="Small" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2" style="padding-left:0px">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_rate_amount" CssClass="form-control form-control-sm text-right" Text="0.00" ></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired01"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                    <asp:label runat="server" Font-Bold="true" Text="Days Worked:"  Font-Size="Small"></asp:label>
                                </div>
                                <div class="col-1" style="padding-left:0px">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_no_days_worked" CssClass="form-control form-control-sm text-right text-right"></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired2"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                    <asp:label runat="server" Font-Bold="true" Text="LWP Days(Ord):"  Font-Size="Small"></asp:label>
                                </div>
                                <div class="col-2" style="padding-left:0px">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_lwp" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired100"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                            </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row" runat="server" ID="id_days_hours">
                                            <div class="col-2" style="display:none">
                                                <asp:label runat="server" Font-Bold="true" Text="Hours Worked:" Visible="false"></asp:label>
                                            </div>
                                            <div class="col-2" style="display:none">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" ID="txtb_no_hours_worked" CssClass="form-control form-control-sm text-right" Visible="false"></asp:TextBox>
                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired3"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                <asp:label runat="server" Font-Bold="true" Text="Lates(min.):"  Font-Size="Small"></asp:label>
                                            </div>
                                            <div class="col-2" style="padding-left:0px">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" ID="txtb_lates_undertime_in_minute" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired33"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-2">
                                                <asp:label runat="server" Font-Bold="true" Text="LWOP Days:"  Font-Size="Small"></asp:label>
                                            </div>
                                            <div class="col-1" style="padding-left:0px">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" ID="txtb_no_days_lwopay" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired1"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            
                                            <div class="col-2">
                                                <asp:label runat="server" Font-Bold="true" Text="LWP Days (SPL):"  Font-Size="Small"></asp:label>
                                            </div>
                                            <div class="col-2" style="padding-left:0px">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" ID="txtb_lwp_spl" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired202"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                <asp:label runat="server" Font-Bold="true" Text="LWOP Remarks:"  Font-Size="Small"></asp:label>
                                            </div>
                                            <div class="col-9" style="padding-left:0px">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" ID="txtb_remarks" CssClass="form-control form-control-sm" MaxLength="150" ></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                
                                


                            <div class="row">
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <%--<div class="col-12">
                                    <asp:label runat="server" Font-Bold="true" Text="SUMMARY TOTALS :"></asp:label>
                                    &nbsp;&nbsp;&nbsp;&nbsp ;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:label runat="server" Font-Bold="true" CssClass="text-right" Text="DEDUCTIONS :"></asp:label>
                                     <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>--%>
                                <div class="col-4">
                                    <div class="tabs-container">
                                        <ul class="nav nav-tabs">
                                            <li class="nav-item"><a data-toggle="tab" id="id_salary_tab" href="#tab-1" class="nav-link active" style="padding: .09rem 1rem;font-weight:bold"> SALARY </a></li>
                                            <li class="nav-item"><a data-toggle="tab" id="id_pera_tab"  href="#tab-2" class="nav-link" style="padding: .09rem 1rem;font-weight:bold">PERA </a></li>
                                        </ul>
                                        <div class="tab-content">
                                            <div id="tab-1" class="tab-pane fade show active">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Bold="true" Text="Wages Amount:"  Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" Font-Bold="true" ID="txtb_wages_amt" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:label runat="server" Font-Bold="true" Text="Less" Font-Size="Smaller" class="text-danger" ></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="LWOP Amt:" Font-Size="Small"></asp:label>
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
                                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="Lates Amt. :" Font-Size="Small"></asp:label>
                                                        </div>
                                                    <div class="col-6">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" ID="txtb_lates_undertime_in_minute_in_amt" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                                <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired101"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6" style="padding-right:0px !important">
                                                        <asp:label runat="server" Font-Bold="true" Text="Net - Wages Amt:"  Font-Size="Small"></asp:label>
                                                    </div>
                                                    <div class="col-6">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" Font-Bold="true" ID="txtb_wages_amt_net" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="tab-2" class="tab-pane fade">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Bold="true" Text="PERA :" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_pera_amount" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired61"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:label runat="server" Font-Bold="true" Text="Less" Font-Size="Smaller" class="text-danger" ></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="LWOP Amt:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_lwop_amount_pera" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                    
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div> 
                                                        <div class="col-6">
                                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="Lates Amt. :" Font-Size="Small"></asp:label>
                                                            </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_lates_undertime_in_minute_in_amt_pera" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired300"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding-right:0px !important">
                                                            <asp:label runat="server" Font-Bold="true" Text="Net - PERA Amt:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_pera_amount_net" CssClass="form-control form-control-sm text-right" Font-Bold="true" Enabled="false"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="Label7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="form-group row">
                                        <div class="col-12">
                                            <hr style="margin-top:5px;margin-bottom:5px" />
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
                                        
                                        <%--<div class="col-6">
                                            <asp:label runat="server" Font-Bold="true" Text="Less :"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_less" CssClass="form-control form-control-sm text-right" Text="0.00"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired5"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>--%>
                                         <div class="col-12">
                                            <asp:label runat="server" Font-Bold="true" Text="Less" Font-Size="Smaller" class="text-danger" ></asp:label>
                                        </div>  
                                        <div class="col-6">
                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="Mandatory :" Font-Size="Small"></asp:label>
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
                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="Optional :" Font-Size="Small"></asp:label>
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
                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="Loans :" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_total_loans" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired9"></asp:Label>
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
                                        <div class="col-6" style="padding-right: 0px !important">
                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="Net (1st Half) :" Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_net_pay_1h" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired11"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6" style="padding-right: 0px !important">
                                            &nbsp&nbsp&nbsp&nbsp<asp:label runat="server" Font-Bold="true" Text="Net (2nd Half) :"  Font-Size="Small"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                   <asp:TextBox runat="server" ID="txtb_net_pay_2h" CssClass="form-control form-control-sm text-right" Enabled="false" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired12"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6" style="padding-right: 0px !important">
                                                <asp:label runat="server" Font-Bold="true" Text="LEAVE EARNED :" Font-Size="Small"></asp:label>
                                            </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_leave_earned" CssClass="form-control form-control-sm text-right" Font-Bold="true"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired203"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12 text-right" style="margin-top:5px">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Button runat="server" ID="btn_calculate" CssClass="btn btn-warning btn-block btn-sm" Text="Calculate" OnClick="btn_calculate_Click"  />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-8" style="padding-left:20px">
                                    <div class="form-group row">
                                    <div class="col-12">
                                        <ul class="nav nav-tabs" style="border-bottom: 1px solid #dee2e6" id="myTab" role="tablist">
                                        
                                            <li class="nav-item">
                                            <a class="nav-link " id="id_mandatory" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab" aria-selected="false" style="padding: .09rem 1rem;font-weight:bold" aria-expanded="true">MANDATORY</a>
                                            </li>
                                            <li class="nav-item">
                                            <a class="nav-link active" id="id_optional" data-toggle="tab" href="#optional_tab" role="tab" aria-controls="optional_tab" aria-selected="false" style="padding: .09rem 1rem;font-weight:bold">OPTIONAL</a>
                                            </li>
                                            <li class="nav-item">
                                            <a class="nav-link"  id="id_loans" data-toggle="tab" href="#loans_tab" role="tab" aria-controls="loans_tab" aria-selected="false" style="padding: .09rem 1rem;font-weight:bold">LOANS</a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="tab-content" id="myTabContent" style="width:100%">
                                        <%--MANDATORY DEDUCTIONS--%>
                                        <div class="tab-pane fade show active"  id="mandatory_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px; margin-right:15px;" aria-labelledby="id_mandatory">
                                            <div class="row" style="padding-left:30px;padding-right:30px;padding-top:10px;height: 370px;">
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
                                                <div class="col-6">
                                                    <div class="form-group row">
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    
                                        <%--OPTIONAL DEDUCTIONS--%>
                                        <div class="tab-pane fade " id="optional_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px; margin-right:15px;" aria-labelledby="id_optional">
                                            <div class="row" style="padding-left:30px;padding-right:30px;padding-top:10px;height: 370px;">
                                                <div class="col-4">
                                                    <div class="form-group row">
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true"  Text="GSIS-UOLI:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server"  ID="txtb_gsis_ouli" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired22"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="UOLI-45 :" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_ouli45" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired23"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="UOLI-50 :" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server"  ID="txtb_gsis_ouli50" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired24"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="UOLI-55 :" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_ouli55" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired25"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="UOLI-60 :" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_ouli60" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired26"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="UOLI-65 :" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_ouli65" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired27"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
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
                                                            <asp:label runat="server" Font-Bold="true" Text="HDMFLCD:" Font-Size="Small"></asp:label>
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
                                                            <asp:label runat="server" Font-Bold="true" Text="PHIL LF:" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_philam" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired30"></asp:Label>
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


                                                <div class="col-4">
                                                    <div class="form-group row">
                                                        
                                                        
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:label runat="server" Font-Bold="true" Text="CEAP :" Font-Size="Small"></asp:label>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_gsis_ceap" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired333"></asp:Label>
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
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="form-group row">
                                                        
                                                        
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" ID="lbl_other_prem4"  Font-Bold="true" Text="(Reserved4):" Font-Size="Small"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_otherpremium_no4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired38"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
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

                                                    </div>
                                                </div>
                                            </div> 
                                        </div>

                                        <%--LOANS--%>
                                        <div class="tab-pane fade" id="loans_tab" role="tabpanel" style="border: 1px solid green;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px; margin-right:15px;" aria-labelledby="id_loans">
                                            <div class="row" style="padding-left:30px;padding-right:30px;padding-top:10px;height: 370px;">
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
                                                        <asp:label runat="server" Font-Bold="true" Text="UOLI Loan:" Font-Size="Small"></asp:label>
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
                                                        
                                                    </div>
                                                </div>

                                                <div class="col-4">
                                                    <div class="form-group row">
                                                            
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
                                                                <asp:label runat="server" Font-Bold="true" Text="GSISHELP :" Font-Size="Small"></asp:label>
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
                                                                <asp:label runat="server" Font-Bold="true" Text="GSISHOUS:" Font-Size="Small"></asp:label>
                                                            </div>
                                                            <div class="col-6" style="padding:0px;padding-right:5px">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_gsis_housing_loan" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired65"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-4">
                                                        <div class="form-group row">
                                                        
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
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
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
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
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
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
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
                                                       <%-- END     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                            <div class="col-12" style="padding:0px;padding-right:5px;display:none">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="btn_editloan" runat="server" CssClass="btn btn-primary btn-sm btn-block" OnClick="btn_editloan_Click">
                                                                            <i class="fa fa-edit"></i> 
                                                                            Edit Loans
                                                                        </asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-12" style="padding:0px;padding-right:5px;margin-top:5px;display:none">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="btn_contributions" runat="server" CssClass="btn btn-primary btn-sm btn-block" OnClick="btn_contributions_Click">
                                                                            <i class="fa fa-edit"></i> 
                                                                            Edit Contributions
                                                                        </asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="form-group row" style="margin-top:15px">

                                        <div class="col-6" style="padding:0px">
                                             <asp:UpdatePanel runat="server">
                                                 <ContentTemplate>
                                                      <asp:Label runat="server" ID="lbl_if_dateposted_yes" CssClass="col-form-label-sm text-danger" Font-Bold="true" Font-Size="12px" Text=""></asp:Label>

                                                     <asp:Label runat="server" ID="lbl_lastday_hidden" Visible="false"></asp:Label>
                                                     <asp:Label runat="server" ID="lbl_installation_monthly_conv_hidden" Visible="false"></asp:Label>
                                                     <asp:Label runat="server" ID="lbl_installation_PERA_conv_hidden" Visible="false"></asp:Label>
                                                     <asp:Label runat="server" ID="lbl_rate_basis_hidden" Visible="false"></asp:Label>
                                                     <%--<asp:Label runat="server" ID="lbl_lastday_monthly_rate_hidden" Visible="false"></asp:Label>--%>
                                                     <asp:Label runat="server" ID="lbl_lastday_daily_rate_hidden" Visible="false"></asp:Label>
                                                     <asp:Label runat="server" ID="lbl_lastday_hourly_rate_hidden" Visible="false"></asp:Label>
                                                     <%--<asp:Label runat="server" ID="lbl_lwop_pera_hidden" Visible="false"></asp:Label>--%>
                                                     <asp:Label runat="server" ID="lbl_lwop_monthly_hidden" Visible="false"></asp:Label>
                                                     <asp:Label runat="server" ID="lbl_minimum_netpay_hidden" Visible="false"></asp:Label>
                                                     <asp:Label runat="server" ID="lbl_addeditmode_hidden" Visible="false" ></asp:Label>
                                                     <asp:Label runat="server" ID="lbl_wages_amt_hidden" Visible="false" ></asp:Label>
                                                     <%--<asp:Label runat="server" ID="lbl_leave_earned_amount_hidden" Visible="false" ></asp:Label>--%>
                                                     <asp:Label runat="server" ID="hidden_hoursin1day" Visible="false" ></asp:Label>
                                                 </ContentTemplate>
                                             </asp:UpdatePanel>
                                         </div>

                                        <div class="col-6 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="Linkbtncancel"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                                    <asp:Button ID="btnSave" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click"   />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                        
                                </div>
                                
                            </div>
                        </div>
                        <%--<div class="row" style="padding-right:10px;padding-left:10px;padding-bottom:5px">
                            <div class="col-12" style="padding:0px">
                                <hr style="margin-top:5px;margin-bottom:5px;" />
                            </div>
                            
                        </div>--%>
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
                <div class="col-3"><strong style="font-family:Arial;font-size:18px;color:white;">Monthly Payroll (Casual)</strong></div>
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
                                            <asp:Label runat="server" Text="Entries"></asp:Label>
                                            |
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
                                            <asp:Button ID="btn_edit_hidden" runat="server" Text="Edit Hidden" CssClass="btn btn-secondary btn-sm" OnClick="btn_edit_hidden_Click"  style="display:none"/>
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
                                                    <asp:DropDownList ID="ddl_payroll_group" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" style="float:right;width:97.5%" OnSelectedIndexChanged="ddl_payroll_group_SelectedIndexChanged" Enabled="false" ></asp:DropDownList>
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
                                                        &nbsp;&nbsp;<%# Eval("position_title1") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="GROSS PAY" SortExpression="gross_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("gross_pay") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
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
                                                                        Enabled='<%# Eval("post_status").ToString() == "Y" && Session["ep_post_authority"].ToString() == "0" || Eval("post_status").ToString() == "N" && Session["ep_post_authority"].ToString() == "1" || Eval("post_status").ToString() == "R"? false : true %>'
                                                                        ToolTip='<%# Session["ep_post_authority"].ToString() == "0" ? "Delete Existing Record" : "UnPost From Card" %>'/>
                                                                <% }
                                                                %>
                                                                <% if (ViewState["page_allow_print"].ToString() == "1")
                                                                    {
                                                                %>
                                                                     <asp:ImageButton ID="imgbtn_print"  
                                                                         CssClass="btn btn-success action" 
                                                                         EnableTheming="true" 
                                                                         runat="server" 
                                                                         ImageUrl="~/ResourceImages/print1.png" 
                                                                          OnCommand="imgbtn_print_Command"
                                                                         style="padding-left: 0px !important;" 
                                                                         CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_registry_nbr") + "," + Eval("payroll_year")%> ' 
                                                                         tooltip="Print"/>
                                                                <%  }
                                                                %>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
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
    function openModal() {
        $('#add').modal({
            keyboard: false,
            backdrop:"static"
        });
    };
    function show_date()
        {
            $('#<%= txtb_or_date.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
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
    <script type="text/javascript">
        function click_salary_tab()
        {
            $('#id_salary_tab').click()
        }
        function click_pera_tab()
        {
            $('#id_pera_tab').click()
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
    <script>
        $( document ).ready(function() {
            $('#<%= btn_edit_hidden.ClientID%>').click();
            $('#id_loans').click();
        });
        
    </script>  

    <script type="text/javascript">
        function openSelectReport() {
            $('#SelectReport').modal({
                keyboard: false,
                backdrop:"static"
            });
         };

         function openLoading() {

            $('#Loading').modal({
                keyboard: false,
                backdrop: "static"
            });
        }
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
