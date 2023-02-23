<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cExtractToExcel.aspx.cs" Inherits="HRIS_ePayroll.View.cExtractToExcel" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server" style="padding:10%;">
    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>

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
                                            <%--<img src="../../ResourceImages/loadingwithlogo.gif" style="width: 100%;" />--%>
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

    <!-- The Modal - Add Confirmation -->
            <div class="modal fade" id="AddEditConfirm">
              <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content text-center">
                  <!-- Modal body -->
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                        <div class="modal-header border-0">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body mb-5">
                            <i runat="server" id="i_icon_display" class="fa-5x fa fa-check-circle text-success"></i>
                            <h2 runat="server" id="h2_status" >Successfully</h2>
                            <h6 class="pr-5 pl-5"><asp:Label ID="lbl_generation_msg" runat="server" Text="Save"></asp:Label></h6>
                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  <!-- Modal footer -->
                  <%--<div style="margin-bottom:30px">
                      <button type="button" class="btn btn-primary btn-lg" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true"> <i class="fa fa-check"></i> OK</span>
                        </button>
                  </div>--%>
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
                <div class="col-8"><strong style="font-family:Arial;font-size:18px;color:white;"><%: Master.page_title %></strong></div>
                <div class="col-4 text-right">
                    
                        <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                            <ContentTemplate>
                                            
                                <% if (ViewState["page_allow_add"].ToString() == "1")
                                    {  %>
                                <asp:Button ID="btn_create_generate" runat="server" CssClass="btn btn-info btn-md add-icon icn" Font-Bold="true" OnClick="btn_create_generate_Click"  Text="Create/Download Excel" />
                                <% }
                                    %>     
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
                            <td style="padding-left:12px;padding-right:12px;">
                                <div class="row">
                                    <div class="col-12"  style="margin-top:5px;">
                                        <div class="form-group row">
                                            <div class="col-3" style="padding-right:0px !important;padding-top:6px;" >
                                                <asp:Label runat="server" Font-Bold="true" Text="Report Template:" ></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_report_type" runat="server" CssClass=" form-control" AutoPostBack="true" >
                                                            <asp:ListItem Value="CAFOA" Text="CAFOA Amount"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <div class="col-12 "  style="margin-top:5px;">
                                        <div class="form-group row">
                                            <div class="col-3" style="padding-top:6px;">
                                                <asp:Label runat="server" Font-Bold="true" Text="Status:" ></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_post_status" runat="server" CssClass=" form-control" AutoPostBack="true" >
                                                             <asp:ListItem Value="" Text="All"></asp:ListItem>
                                                             <asp:ListItem Value="Y" Text="Posted"></asp:ListItem>
                                                             <asp:ListItem Value="N" Text="Not Posted"></asp:ListItem>
                                                             <asp:ListItem Value="T" Text="Returned"></asp:ListItem>
															 <asp:ListItem Value="R" Text="Released"></asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:Label ID="Label1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 "  style="margin-top:5px;">
                                        <div class="form-group row">
                                            <div class="col-3" style="padding-top:6px;">
                                                <asp:Label runat="server" Font-Bold="true" Text="Employment Type:" ></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass=" form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                         <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top:5px;">
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-6" style="padding-top:6px;">
                                                    <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true"  Text="Payroll Year:" ></asp:Label>
                                            </div>
                                            <div class="col-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-5 text-right" style="padding-top:6px;">
                                                    <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true" Text="Payroll Month:" ></asp:Label>
                                            </div>
                                            <div class="col-7">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_month_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Text="All"></asp:ListItem>
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
                                                        <asp:Label ID="LblRequired2" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
   <script type="text/javascript">
       function openLoading() {
           
            $('#Loading').modal({
                keyboard: false,
            backdrop:"static"
            });
            setTimeout(function () {
                $('#Loading').modal("hide");
                $('.modal-backdrop.show').remove();
            }, 5000);
            setTimeout(function () {
                openMessage();
            }, 5000);
             
         };
    </script>
     <script type="text/javascript">
         function openMessage() {
             
            $('#AddEditConfirm').modal({
                 keyboard: false,
                backdrop:"static"
             });
         };

         function hightlight()
        {
        }
    </script>
</asp:Content>
