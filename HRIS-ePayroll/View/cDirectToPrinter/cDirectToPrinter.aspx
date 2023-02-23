<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cDirectToPrinter.aspx.cs" Inherits="HRIS_ePayroll.View.cDirectToPrinter" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
    <style type="text/css">
        .table-bordered td, .table-bordered th 
        {
            border-left: 0px solid white; 
            border-right: 0px solid white; 
            border-top: 0px solid white;
        }
    </style>

        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server" style="padding: 12% 15% 0%;">
    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>


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
                                        <i class="fa-5x fa fas fa-exclamation-circle text-warning"></i>
                                        <h2 >Please Select At Least One.</h2>
                                        <h6><asp:Label ID="deleteRec1" runat="server" Text=""></asp:Label></h6>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <div style="margin-bottom:50px">
                             <asp:LinkButton ID="LinkButton3"  runat="server" data-dismiss="modal"  CssClass="btn btn-warning"> <i class="fa fa-check"></i> OK</asp:LinkButton>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel1" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="deductions">
                    <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content text-center">
                    <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <i class="fa-5x fa fas fa-exclamation-circle text-warning"></i>
                                        <h2 >No deduction/s to be printed!</h2>
                                        <h6><asp:Label ID="Label1" runat="server" Text=""></asp:Label></h6>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <div style="margin-bottom:50px">
                             <asp:LinkButton ID="LinkButton1"  runat="server" data-dismiss="modal"  CssClass="btn btn-warning"> <i class="fa fa-check"></i> OK</asp:LinkButton>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- The Modal - Generating Report -->
                <div class="modal fade" id="Loading">
                    <div class="modal-dialog modal-dialog-centered modal-lg">
                        <div class="modal-content text-center">
                            <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                
                                                     <div class="modal-body">
                                                <div class="col-12 text-center">
                                                    <img src="/ResourceImages/loadingwithlogo.gif" style="width: 100%;" />
                                                </div>
                                                         
                                                     </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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


        <div class="col-12" style="margin-top:-110px;margin-bottom:-2px;">
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-3">
                     <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                            <ContentTemplate>
                                    <a ID="lb_back" style="padding-top:7px;padding-bottom:8px;font-size:15px;" class="btn btn-success btn-sm font-weight-bold" href="/View/cPayRegistry/cPayRegistry.aspx" ><i class="fa fa-arrow-left"></i> Back To Registry</a>
                               </ContentTemplate>
                        </asp:UpdatePanel>
                </div>
                <div class="col-6 text-center"><strong style="font-family:Arial;font-size:25px;color:white;">Generate Reports </strong></div>
                <div class="col-3 text-right">
                    
                        
                            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                            <ContentTemplate>
                                   <asp:Button ID="btn_create_generate" runat="server" CssClass="btn btn-success btn-md print-icon icn" Font-Bold="true" OnClick="btn_print_all_Click" Text="Print All" />
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-info btn-md add-icon icn" Font-Bold="true" OnClientClick="openLoading();" Visible="false" Text="Print" />
                                     
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
        <div class="row">
            <div class="col-12">
                <table class="table table-bordered  table-scroll">
                    <tbody class="my-tbody">
                        <tr>
                            <td style="padding-left:12px;padding-right:12px;">
                                <div class="row">
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-6" >
                                                    <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true"  Text="Payroll Year:" ></asp:Label>
                                            </div>
                                            <div class="col-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        
                                                        <asp:TextBox ID="txtb_payroll_year" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm text-center"></asp:TextBox>
                                                        <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-5 text-right">
                                                    <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true" Text="Payroll Month:" ></asp:Label>
                                            </div>
                                            <div class="col-7">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>

                                                        <asp:TextBox ID="txtb_payroll_month" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm text-center"></asp:TextBox>

                                                        <asp:Label ID="LblRequired2" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 " >
                                        <div class="form-group row">
                                            <div class="col-3" style="padding-top:6px;">
                                                <asp:Label runat="server" Font-Bold="true" Text="Employment Type:" ></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <%--<asp:DropDownList ID="ddl_empl_type" runat="server" CssClass=" form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtb_empl_type" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtb_empl_type_display" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>
                                                         <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:UpdatePanel runat="server" Visible="false">
                                              <ContentTemplate>
                                    <div class="col-12" >
                                        <div class="form-group row">
                                            <div class="col-3" style="padding-right:0px !important;padding-top:6px;" >
                                                <asp:Label runat="server" Font-Bold="true" Text="Payroll Template:" ></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_payroll_template" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtb_payroll_template_display" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lbl_registry_number" Visible="false" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                                         </ContentTemplate>
                                                </asp:UpdatePanel>

                                     <div class="col-12" >
                                        <div class="form-group row">
                                            <div class="col-3" style="padding-top:6px;">
                                                <asp:Label runat="server" Font-Bold="true" Text="Payroll Group Descr.:" ></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>

                                                        <%--<asp:DropDownList ID="ddl_payroll_group" runat="server" CssClass=" form-control" AutoPostBack="true" style="float:right;width:100%" OnSelectedIndexChanged="ddl_payroll_group_SelectedIndexChanged" ></asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtb_payroll_group" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtb_payroll_group_display" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12" >
                                        <div class="form-group row">
                                            <div class="col-3" style="padding-top:6px;">
                                                <asp:Label runat="server" Font-Bold="true" Text="Payroll Description:" ></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_payroll_descr" runat="server" Enabled="false" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                  
                                    <div class="col-12">
                                         <hr style="margin-top:15px;margin-bottom:5px;" />
                                        <div class="form-group row">
                                            <div class="col-3" style="padding-top:3px;">
                                                <h5><b>Print Options:</b></h5>
                                            </div>
                                            <div class="col-1">
                                                 <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                              <ContentTemplate>

                                                  <label ID="lbl_print_all" runat="server" class="container" style="display:inline !important;font-size:14px !important;">
                                                   <asp:CheckBox ID="chkbox_print_all" OnCheckedChanged="chkbox_print_all_CheckedChanged" runat="server" AutoPostBack="True" />
                                                   <span class="checkmark"></span>
                                                   </label>
                                                  
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3" style="padding-top:3px;padding-left:0px;margin-left:-10px;">
                                                <asp:Label runat="server" Font-Bold="true" Text="Select All Reports" ></asp:Label>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    
                                </div>
                                    <asp:UpdatePanel runat="server" class="col-12">
                                        <ContentTemplate>
                                        <div class="row">
                                        <div class="col-12" style="margin-top:5px;" id="regdiv" runat="server">
                                            <div class="form-group row">
                                                <div class="col-3" style="padding-top:6px;">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Payroll Registry Nbr:" ></asp:Label>
                                                </div>
                                                <div class="col-4">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddl_registrynbr" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" style="float:left;width:100%"></asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                         </div>
                                        </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                               

                           <hr style="margin-top:10px;margin-bottom:10px;" />
                            
                                      
                    <div class="col-12" style="margin-top:-20px;">
                        <asp:UpdatePanel ID="rCasualApptPrint_panel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView 
                                ID="gv_dataListGrid"
                                runat="server" 
                                AllowSorting="True" 
                                AutoGenerateColumns="False" 
                                EnableSortingAndPagingCallbacks="True"
                                GridLines="None"
                                PagerStyle-Width="3" 
                                PagerStyle-Wrap="false" 
                                pagesize="15"
                                Width="100%" 
                                BorderWidth="0px"
                                BorderColor="Red"
                                BorderStyle="None"
                                showheader="false"
                                Font-Names="Century gothic"
                                Font-Size="Medium" 
                                RowStyle-Width="5%" 
                                AlternatingRowStyle-Width="10%" CellPadding="2" ShowHeaderWhenEmpty="false"
                                EmptyDataText="NO DATA FOUND" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-CssClass="no-data-found">
                                <Columns>

                                    <asp:TemplateField HeaderText="PAYROLL Code" Visible="false" SortExpression="payrolltemplate_code">
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lbltemplate_code" runat="server" Text='<%# Eval("payrolltemplate_code") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                        <ItemStyle Width="0%" />
                                        <HeaderStyle HorizontalAlign="LEFT" />
                                        <ItemStyle HorizontalAlign="LEFT" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PRINT OPTIONS" ControlStyle-CssClass="pull-left">
                                        <ItemTemplate>

                                            <div style="margin-top:15px;">
                                                 <asp:Label ID="lbltemplate_descr" runat="server" Text='<%# Eval("payrolltemplate_descr") %>'></asp:Label>
                                           </div>

                                        </ItemTemplate>
                                        <ItemStyle Width="97%" />
                                        <HeaderStyle  CssClass="text-center" />
                                        <ItemStyle HorizontalAlign="LEFT" Font-Bold="true" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="SELECT">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel14" style="margin-top:15px; margin-top:8px;" runat="server">
                                                <ContentTemplate>

                                                    <label ID="lbl_print" runat="server" class="container" style="display:inline !important;font-size:14px !important;">
                                                     <asp:CheckBox ID="chkbox_print" runat="server" />
                                                     <span class="checkmark"></span>
                                                     </label>

                                                    <%--<asp:CheckBox ID="chkbx_allow_delete" Enabled='<%# (Eval("group_id").ToString().Trim() == "" )? true:false %>'  runat="server" />--%>
                                                    <%--<asp:CheckBox ID="chkbox_print" Enabled="true" runat="server" />--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
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
                                <%--<AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="10%" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" CssClass="header-style"/>
                                <PagerStyle CssClass="pagination-ys" BackColor="#2461BF" ForeColor="White" HorizontalAlign="right" VerticalAlign="NotSet" Wrap="True" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
<%--                            <asp:AsyncPostBackTrigger ControlID="btn_print" />
                            <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                            <asp:AsyncPostBackTrigger ControlID="txt_search" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_department" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_subdep" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_division" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_section" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_fund" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_budget_code" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_group_id" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_semester" />
                            <asp:AsyncPostBackTrigger ControlID="btn_viewselected" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>



                                    

                                   <%-- <asp:UpdatePanel ID ="UpdatePanel2" class="col-12" style="margin-top:5px;" autopostback="true" runat="server">
                                        <ContentTemplate> 
                                        <asp:UpdatePanel ID ="UpdatePanel4" class="form-group row" autopostback="true" runat="server">
                                            <ContentTemplate> 
                                         <asp:UpdatePanel ID ="Lbl_panel" class="col-3" style="margin-top:5px;" autopostback="true" runat="server">

                                        <ContentTemplate> 
                                             <asp:Label runat="server" Font-Bold="true" Text="TEXT" ></asp:Label>
                                        </ContentTemplate> 

                                     </asp:UpdatePanel>

                                     <asp:UpdatePanel ID ="UpdatePanel1" class="col-9" style="margin-top:5px;" autopostback="true" runat="server">

                                       <ContentTemplate> 
                                           <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                       </ContentTemplate> 

                                     </asp:UpdatePanel>
                                     </ContentTemplate> 
                                     </asp:UpdatePanel>

                                     </ContentTemplate> 
                                   </asp:UpdatePanel>--%>



                                    
                                       

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
  
    <script type="text/javascript">
       $(function() {
           $("#chkbox_print_all").click(function () {
           $('CheckBox').not(this).prop('checked', this.checked);
           });
        });
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
        function openModalDelete() {
            $('#deleteRec').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>

    <script type="text/javascript">
        function openModalNoDeductions() {
            $('#deductions').modal({
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
            

        };
 </script>



</asp:Content>
