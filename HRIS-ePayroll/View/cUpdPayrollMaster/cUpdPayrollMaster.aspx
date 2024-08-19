<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cUpdPayrollMaster.aspx.cs" Inherits="HRIS_ePayroll.View.cUpdPayrollMaster" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server" style="padding:10%;">
    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>

        <div class="modal fade" id="modal_print_preview" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document" >
                <div class="modal-content  modal-content-add-edit">
                    <div class="modal-header bg-success" >
                        <h5 class="modal-title text-white" ><asp:Label runat="server" Text="Preview Report"></asp:Label></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body with-background" style="padding:0px !important">
                        <div class="row">
                            <div class="col-lg-12 text-center m-2">
                                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                    <ContentTemplate>
                                            
                                        <% if (ViewState["page_allow_add"].ToString() == "1")
                                            {  %>
                                        <asp:Button ID="btn_create_generate" runat="server" CssClass="btn btn-info btn-md add-icon icn" Font-Bold="true" OnClick="btn_create_generate_Click" OnClientClick="openLoading()"  Text="Run Update" />
                                
                                        <% }
                                            %>     
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-lg-12">
                                <iframe style="width:100% !important;height:700px !important;border:0px none;" id="iframe_print_preview"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
                        <div class="modal-body">
                            <i runat="server" id="i_icon_display" class="fa-5x fa fa-check-circle text-success"></i>
                            <h2 runat="server" id="h2_status" >Successfully</h2>
                            <h6><asp:Label ID="lbl_generation_msg" runat="server" Text="Save"></asp:Label></h6>
                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  <!-- Modal footer -->
                  <div style="margin-bottom:30px">
                      <button type="button" class="btn btn-primary btn-lg" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true"> <i class="fa fa-check"></i> OK</span>
                        </button>
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


        <div class="col-12" style="padding:5% 10% 0% 10%;">
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-6"><strong style="font-family:Arial;font-size:18px;color:white;"><%: Master.page_title %></strong></div>
                <div class="col-6 text-right">
                    
                        
                </div>
                <%--<div class="col-2 text-right">
                   <asp:UpdatePanel runat="server">
                       <ContentTemplate>
                           <asp:Button ID="btn_save_group" runat="server" CssClass="btn btn-success btn-sm add-icon icn"  Text="Save Group"/>
                       </ContentTemplate>
                   </asp:UpdatePanel>
                </div>--%>
            </div>
            <div class="row">
                <table class="table table-bordered  table-scroll">
                    <tbody class="my-tbody">
                        <tr>
                            <td style="padding-left:12px;padding-right:12px;">
                                <div class="row">
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-6" style="padding-top:6px;">
                                                    <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true"  Text="Year:" ></asp:Label>
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
                                                    <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true" Text="Month:" ></asp:Label>
                                            </div>
                                            <div class="col-7">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_month_SelectedIndexChanged">
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
                                        <div class="col-12 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <button class="btn btn-success" id="id_payroll" onclick="print('MONITORING')"><i class="fa fa-print"></i> Preview </button>
                                                    <asp:Button ID="btn_generate_report" runat="server" CssClass="btn btn-success btn-md print-icon icn pull-right" Visible="false" Font-Bold="true" OnClick="btn_generate_report_Click"   Text="Print" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            
        </div>
        <%--<div class="row" >
            <div class="col-12" style="padding:0% 10% 1% 10%">
                
            </div>
        </div>--%>
        
</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
   <script type="text/javascript">
       function hightlight()
        {
        }
        function openLoading() {
             $('#Loading').modal({
                 keyboard: false,
                backdrop:"static"
            });
            setTimeout(function () {
                $('#Loading').modal("hide");
                $('#modal_print_preview').modal("hide");
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
    </script>
    <script type="text/javascript">
        function print(report_type)
        {
            var sp                  = ""
            var p_employment_type   = $('#<%= ddl_empl_type.ClientID %>').val()
            var p_year              = $('#<%= ddl_year.ClientID %>').val()
            var p_month             = $('#<%= ddl_month.ClientID %>').val()
            if (p_employment_type == "RE")
            {
                ReportPath      = "~/Reports/cryMasterInserted/cryMasterInserted_RE.rpt";
                sp              = ReportPath + "," + "sp_payrollemployeemaster_tbl_insert_RE_list,p_year," + p_year + ",p_month," + p_month;
            }
            else
            {
                ReportPath      = "~/Reports/cryMasterInserted/cryMasterInserted_CEJO.rpt";
                sp              = ReportPath + "," + "sp_payrollemployeemaster_tbl_insert_CE_JO_list,p_employment_type," + p_employment_type + ",p_year," + p_year + ",p_month," + p_month;
            }
            previewReport(sp,report_type)
        }
        function previewReport(sp,report_type)
        {
            // *******************************************************
            // *** VJA : 2021-07-14 - Validation and Loading hide ****
            // *******************************************************
            var ReportName      = "CrystalReport"
            var SaveName        = "Crystal_Report"
            var ReportType      = "inline"
            var ReportPath      = ""
            var iframe          = document.getElementById('iframe_print_preview');
            var iframe_page     = $("#iframe_print_preview")[0];
            var embed_link;
            iframe.style.visibility = "hidden";
            if (report_type == "PAYROLL")
            {
                embed_link = sp;
            }
            else
            {
                embed_link = "../../printView/CrystalViewer.aspx?Params=" + ""
                    + "&ReportName=" + ReportName
                    + "&SaveName="   + SaveName
                    + "&ReportType=" + ReportType
                    + "&ReportPath=" + ReportPath
                    + "&id=" + sp // + "," + parameters
            }

            if (!/*@cc_on!@*/0) { //if not IE
                iframe.onload = function () {
                    iframe.style.visibility = "visible";
                };
            }
            else if (iframe_page.innerHTML()) {
                // get and check the Title (and H tags if you want)
                var ifTitle = iframe_page.contentDocument.title;
                if (ifTitle.indexOf("404") >= 0)
                {
                    swal("You cannot Preview this Report", "There something wrong!", { icon: "warning" });
                    iframe.src = "";
                }
                else if (ifTitle != "")
                {
                    swal("You cannot Preview this Report", "There something wrong!", { icon: "warning" });
                    iframe.src = "";
                }
            }
            else {
                iframe.onreadystatechange = function ()
                {
                    if (iframe.readyState == "complete")
                    {
                        iframe.style.visibility = "visible";
                    }
                };
            }
            iframe.src = embed_link;
            $('#modal_print_preview').modal({ backdrop: 'static', keyboard: false });
            // *******************************************************
            // *******************************************************
        }
    </script>
</asp:Content>
