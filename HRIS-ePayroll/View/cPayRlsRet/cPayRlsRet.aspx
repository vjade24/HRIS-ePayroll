<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayRlsRet.aspx.cs" Inherits="HRIS_ePayroll.View.cPayRlsRet.cPayRlsRet" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
    <style type="text/css">
        
        .highlight
            {
                background-color: #507cd1 !important;
                color:white !important;
                cursor: pointer;
            } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
    
        <!-- The Modal - Select Report -->
        <asp:UpdatePanel ID="UpdatePanel_id_receive_audit_post" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="id_receive_audit_post" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)" >
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <h5 class="modal-title" ><asp:Label ID="Label3" runat="server" Text="Receive" forecolor="White"></asp:Label></h5>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body with-background">
                            <div class="row">
                                <div class="col-12">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_hidden_registry_nbr" CssClass="form-control form-control-sm" Width="100%" Visible="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-6">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label5" runat="server" Text="Voucher Nbr:" CssClass="font-weight-bold" ></asp:Label>
                                            <asp:TextBox runat="server" ID="txtb_voucher_nbr" CssClass="form-control form-control-sm" Width="100%" MaxLength="15"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-6">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_date" runat="server" Text="Date Posted:" CssClass="font-weight-bold" ></asp:Label>
                                            <asp:TextBox runat="server" ID="txtb_date_posted" CssClass="form-control form-control-sm" Width="100%" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--<div class="col-12">
                                    <hr style="padding:0px;margin: 5px;"/>
                                </div>--%>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton4"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                    
                                    <asp:Label ID="Label4" runat="server" Visible="false"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                     </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


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
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <div class="modal-body with-background">
                                    <div class="row">
                                        <div class="col-12" style="margin-bottom: 5px;">
                                            <asp:DropDownList ID="ddl_select_report" CssClass="form-control form-control-sm" runat="server" Width="100%" OnSelectedIndexChanged="ddl_select_report_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <!-- The Modal - Generating Report -->
                <div class="modal fade" id="Loading">
                    <div class="modal-dialog modal-dialog-centered modal-lg">
                        <div class="modal-content text-center">
                            <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
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
                                        <h2 >Delete this Record</h2>
                                        <h6><asp:Label ID="deleteRec1" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <div style="margin-bottom:50px">
                            <asp:LinkButton ID="lnkBtnYes" runat="server"  CssClass="btn btn-danger" > <i class="fa fa-check"></i> Yes, Delete it </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
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
                    <div class="modal-dialog modal-dialog-centered  modal-md" role="document">
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
                        <div class="modal-body with-background" runat="server" >
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: -10px;">
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_registry_no" CssClass="form-control form-control-sm text-center" Enabled="false" Width="100%"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Font-Bold="true" Text="&nbsp; Payroll Group:" ></asp:Label>
                                                    <asp:DropDownList ID="ddl_payroll_group" runat="server" CssClass=" form-control form-control-sm" AutoPostBack="true" style="float:right;width:97.5%" OnSelectedIndexChanged="ddl_payroll_group_SelectedIndexChanged" Enabled="false" ></asp:DropDownList>
                                                    <asp:Label ID="LblRequired5" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                            <hr style="padding:0px;margin: 5px;"/>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Registry Description:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_registry_descr" TextMode="MultiLine" Rows="3" Enabled="false" > </asp:TextBox>
                                                    <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6" >
                                            <asp:UpdatePanel ID="UpdateDateFrom" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text="Date From:" CssClass="font-weight-bold" ></asp:Label>
                                                    
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date text-center" ID="txtb_period_from" Enabled="false" > </asp:TextBox>
                                                    <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6" >
                                            <asp:UpdatePanel ID="UpdateDateTo" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text="Date To:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date text-center" ID="txtb_period_to" Enabled="false" > </asp:TextBox>
                                                    <asp:Label ID="LblRequired3" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6" style="margin-top:5px;" runat="server" id="div_no_works_days" visible="false" >
                                             <asp:Label ID="lbl_dynamic" runat="server" Text="No. Days Work :" CssClass="font-weight-bold" ></asp:Label>
                                             <asp:TextBox runat="server" CssClass="form-control form-control-sm" Width="55%" ID="txtb_no_works" Enabled="false" > </asp:TextBox>
                                             <asp:Label ID="LblRequired4" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </div>

                                        <div class="col-12">
                                            <hr style="padding:0px;margin: 5px;"/>
                                        </div>

                                        <div class="col-12">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_remarks" runat="server" Text="Remarks :" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_remarks" TextMode="MultiLine" Rows="2"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired6" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-12">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_department" runat="server" Text="Department :" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_department"></asp:DropDownList>
                                                    <asp:Label ID="LblRequired7" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                         <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_date_released" runat="server" Text="Date Released :" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date text-center" ID="txtb_date_released" > </asp:TextBox>
                                                    <asp:Label ID="LblRequired8" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_released_by" runat="server" Text="Released By:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-center" ID="txtb_released_by" Enabled="false" > </asp:TextBox>
                                                    <asp:Label ID="LblRequired9" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_date_returned" runat="server" Text="Date Returned :" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date text-center" ID="txtb_date_returned" > </asp:TextBox>
                                                    <asp:Label ID="LblRequired10" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_returned_by" runat="server" Text="Returned By:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-center" ID="txtb_returned_by" Enabled="false"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired11" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_date_voided" runat="server" Text="Date Voided :" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date text-center" ID="txtb_date_voided" > </asp:TextBox>
                                                    <asp:Label ID="LblRequired12" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_voided_by" runat="server" Text="Voided By:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm text-center" ID="txtb_voided_by" Enabled="false"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired13" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer" style="padding-top:5px;padding-bottom:5px;" >
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                    <asp:Button ID="btnSave" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                                
                                    <asp:Label ID="lbl_behaviour_mode_hidden" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="hidden_report_filename" runat="server" Visible="false"></asp:Label>
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
                            <h5 class="modal-title" ><asp:Label runat="server" Text=""></asp:Label></h5>
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
                        <i id="msg_icon" runat="server" class="fa-5x fa fa-exclamation-triangle text-danger"></i>
                        <h4 id="msg_header" runat="server">Report File is Blank !</h4>
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
                <div class="col-3"><strong style="font-family:Arial;font-size:17px;color:white;"><%: Master.page_title %></strong></div>
                <div class="col-7">
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
                <div class="col-2">
                    <div class="input-group">
                        <asp:UpdatePanel ID="UpdatePanel10" ChildrenAsTriggers="false" UpdateMode="Conditional"  runat="server">
                            <ContentTemplate>
                                <%--<button class="btn btn-success btn-sm" style="float:right;width:30%;padding-top:2px;border-radius:0px 5px 5px 0px;"><i class="fa fa-qrcode"></i></button>--%>
                                <%--<span style="position:absolute;float:right;right:20%;color:white;border:1px solid"><i class="fa fa-qrcode"></i></span>--%>
                                <span style="position:absolute;float:right;right:10%;padding-top:2px;color:dimgrey;"> <i class="fa fa-barcode"></i> <i class="fa fa-qrcode"></i></span>
                                <asp:TextBox ID="txtb_search_scan" onInput="search_for_scan(event);" runat="server" class="form-control form-control-sm" placeholder="Registry Nbr..."
                                     style="float:left;width:100%;" OnTextChanged="txtb_search_scan_TextChanged" AutoPostBack="true"></asp:TextBox>
                                
                                <script type="text/javascript">
                                    function search_for_scan(key) {
                                            __doPostBack("<%= txtb_search_scan.ClientID %>", "");
                                    }
                                </script>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
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
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged1"></asp:DropDownList>
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
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_month_SelectedIndexChanged">
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
                                        <div class="col-5 text-right">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-7">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3"></div>
                                <div class="col-3">
                                    <div class="row">
                                        <div class="col-6" style="padding-right:0px !important" >
                                            <asp:Label runat="server" Text="Payroll Type:" ></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                       
                                                    <asp:DropDownList ID="ddl_payrolltype" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_payrolltype_SelectedIndexChanged">
                                                        <asp:ListItem Text="Payroll" Value="01"></asp:ListItem>
                                                        <asp:ListItem Text="Voucher" Value="02"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="row">
                                        <div class="col-3" style="padding-right:0px !important" >
                                            <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                        </div>
                                        <div class="col-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                       
                                                    <asp:DropDownList ID="ddl_payroll_template" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_payroll_template_SelectedIndexChanged"></asp:DropDownList>
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
                                               <%--<asp:TemplateField HeaderText="VOUCHER NO" SortExpression="voucher_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("voucher_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="REG NO" SortExpression="payroll_registry_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("payroll_registry_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="REGISTRY DESCRIPTION" SortExpression="payroll_registry_descr">
                                                    <ItemTemplate>
                                                        <%# "&nbsp; "+Eval("payroll_registry_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="GRP NO" SortExpression="payroll_group_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("payroll_group_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERIOD" SortExpression="payroll_period_descr">
                                                    <ItemTemplate>
                                                        <%# Eval("payroll_period_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GROSS" SortExpression="gross_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("gross_pay") %> &nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("net_pay") %> &nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="STATUS" SortExpression="post_status_descr">
                                                    <ItemTemplate>
                                                        <%# Eval("post_status_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>

                                                                   
                                                                <asp:ImageButton 
                                                                    ID="imgbtn_released" 
                                                                    runat="server" 
                                                                    EnableTheming="true"  
                                                                    CssClass="btn btn-info action" 
                                                                    ImageUrl="~/ResourceImages/check.png" 
                                                                    CommandArgument='<%# Eval("payroll_registry_nbr") + "," + "RLS"%> ' 
                                                                    tooltip="Release" 
                                                                    OnCommand="imgbtn_released_return_void_Command"
                                                                    Enabled='<%# Eval("post_status").ToString().Trim() == "R" ||
                                                                                 Eval("post_status").ToString().Trim() == "V" ||
                                                                                 Eval("post_status").ToString().Trim() == "Y"
                                                                                ? false : true
                                                                             %>' 
                                                                    />
                                                                    
                                                                 
                                                                    <asp:ImageButton 
                                                                        ID="imgbtn_return" 
                                                                        CssClass="btn btn-warning action" 
                                                                        EnableTheming="true"  
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/return.png" 
                                                                        CommandArgument='<%# Eval("payroll_registry_nbr") + "," + "RTN"%> ' 
                                                                        tooltip="Return"
                                                                        OnCommand="imgbtn_released_return_void_Command"
                                                                        Enabled='<%# Eval("post_status").ToString().Trim() == "V" ||
                                                                                 Eval("post_status").ToString().Trim() == "Y"
                                                                                ? false : true
                                                                                %>'  
                                                                        />
                                                                
                                                                  
                                                                    <asp:ImageButton 
                                                                        ID="imgbtn_void" 
                                                                        CssClass="btn btn-danger action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/void.png"
                                                                        CommandArgument='<%# Eval("payroll_registry_nbr") + "," + "VOI"%> '   
                                                                        tooltip="Void" 
                                                                        OnCommand="imgbtn_released_return_void_Command"
                                                                        Enabled='<%# Eval("post_status").ToString().Trim() == "V" ||
                                                                                 Eval("post_status").ToString().Trim() == "Y"
                                                                                ? false : true
                                                                                %>'  
                                                                        />
                                                               
                                                                <% if (ViewState["page_allow_print"].ToString() == "1")
                                                                    {
                                                                %>
                                                                     <asp:ImageButton ID="imgbtn_print" 
                                                                         CssClass="btn btn-success action" 
                                                                         EnableTheming="true" 
                                                                         runat="server" 
                                                                         ImageUrl="~/ResourceImages/print1.png" 
                                                                         style="padding-left: 0px !important;" 
                                                                         OnCommand="imgbtn_print_Command1" 
                                                                         CommandArgument='<%# Eval("payroll_registry_nbr")+","+Eval("payroll_group_nbr")%> ' 
                                                                         tooltip="Print"/>
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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_payroll_group" />
                                    <asp:AsyncPostBackTrigger ControlID="txtb_search_scan" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_payrolltype" />
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
            show_date();
       };
       function show_date()
        {
           $('#<%= txtb_period_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
           $('#<%= txtb_period_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
           $('#<%= txtb_date_released.ClientID %>').datetimepicker({ format: 'yyyy-mm-dd HH:MM' });
           $('#<%= txtb_date_returned.ClientID %>').datetimepicker({ format: 'yyyy-mm-dd HH:MM' });
           $('#<%= txtb_date_voided.ClientID %>').datetimepicker({ format: 'yyyy-mm-dd HH:MM' });


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

           if ($('#<%= txtb_date_released.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_date_released.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }

            if ($('#<%= txtb_date_returned.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_date_returned.ClientID %>').closest("div");
                parent_div.find("i").remove();    
           }

           
            if ($('#<%= txtb_date_voided.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_date_voided.ClientID %>').closest("div");
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
        function openSelectReport() {
            $('#SelectReport').modal({
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
        function OpenRAP() {

            $('#id_receive_audit_post').modal({
                keyboard: false,
                backdrop: "static"
            });
        }
    </script>
    <script type="text/javascript">
        function closeModal_RAP() {
            $('#id_receive_audit_post').modal("hide");
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
      <%--<script>
    $( document ).ready(function() 
    {
        if ('<%= Session["PreviousValuesonPage_cPayRegistry_toprint"] %>' != "")
        {
            $('#<%= btn_edit_hidden.ClientID%>').click();
        }
    });
 
    </script> --%> 
    <script type="text/javascript">
    $(document).ready(function () {
           $('#<%= gv_dataListGrid.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight');
           }, function () {
                   $(this).removeClass('highlight');
           });
    });
    </script> 
</asp:Content>
