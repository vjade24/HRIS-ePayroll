<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cUpdPayrollMaster.aspx.cs" Inherits="HRIS_ePayroll.View.cUpdPayrollMaster" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server" >
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


        <div class="col-12" >
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-6"><strong style="font-family:Arial;font-size:18px;color:white;"><%: Master.page_title %></strong></div>
                <div class="col-6 text-right">
                    
                </div>
            </div>
            <div class="row">
                <table class="table table-bordered  table-scroll">
                    <tbody class="my-tbody">
                        <tr>
                            <td style="padding-left:12px;padding-right:12px;">
                                <div class="row">
                                    <div class="col-3">
                                        <div class="form-group row">
                                            <div class="col-lg-3 mt-2">
                                                 <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true" Text="Show"></asp:Label>
                                            </div>
                                            <div class="col-lg-5">
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                    <ContentTemplate>
                                                            <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control " AppendDataBoundItems="true" AutoPostBack="True"  Width="100%" ToolTip="Show entries per page">
                                                                <asp:ListItem Text="5" Value="5" />
                                                                <asp:ListItem Text="10"  Value="10" />
                                                                <asp:ListItem Text="15" Value="15" />
                                                                <asp:ListItem Text="25" Selected="True" Value="25" />
                                                                <asp:ListItem Text="50" Value="50" />
                                                                <asp:ListItem Text="100" Value="100" />
                                                            </asp:DropDownList>
                                                    </ContentTemplate>  
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-lg-4 mt-2">
                                                    <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9" style="font-size:10px !important"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group row">
                                            <div class="col-6" style="padding-top:6px;">
                                                    <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true"  Text="Year:" ></asp:Label>
                                            </div>
                                            <div class="col-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group row">
                                            <div class="col-3 " style="padding-top:6px;">
                                                    <asp:Label runat="server" CssClass="col-form-label" Font-Bold="true" Text="Month:" ></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged">
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
                                    <div class="col-3">
                                        <div class="form-group row">
                                            <div class="col-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn_generate_report" runat="server" CssClass="btn btn-success btn-md print-icon icn pull-right btn-block" Font-Bold="true" OnClick="btn_generate_report_Click"   Text="Print" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                            <div class="col-6">
                                                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                    <ContentTemplate>
                                            
                                                        <% if (ViewState["page_allow_add"].ToString() == "1")
                                                            {  %>
                                                        <asp:Button ID="btn_create_generate" runat="server" CssClass="btn btn-info btn-md add-icon icn btn-block" Font-Bold="true" OnClick="btn_create_generate_Click" OnClientClick="openLoading()"  Text="Run Update" />
                                
                                                        <% }
                                                            %>     
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3"></div>
                                    <div class="col-9 "  style="margin-top:5px;">
                                        <div class="form-group row">
                                            <div class="col-2" style="padding-top:6px;">
                                                <asp:Label runat="server" Font-Bold="true" Text="Employment Type:" ></asp:Label>
                                            </div>
                                            <div class="col-10">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass=" form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                         <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-2" style="padding-top:6px;">
                                                <asp:Label runat="server" Font-Bold="true" Text="Department:" ></asp:Label>
                                            </div>
                                            <div class="col-10" style="padding-top:6px;">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_department" runat="server" CssClass=" form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                         <asp:Label ID="Label1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2">

                                        <asp:UpdatePanel ID="up_dataListGrid" UpdateMode="Conditional" runat="server" >
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
                                                        pagesize="25"
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
                                                                <ItemStyle HorizontalAlign="CENTER" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                                <ItemTemplate>
                                                                    &nbsp;&nbsp;<%#  Eval("employee_name") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="60%" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="LEFT" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="DEPARTMENT" SortExpression="department_name1">
                                                                <ItemTemplate>
                                                                    &nbsp;&nbsp;<%#  Eval("department_name1") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="25%" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="LEFT" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ACTION">
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                        <ContentTemplate>
                                                                            <% 
                                                                                if (ViewState["page_allow_edit"].ToString() == "1" || ViewState["page_allow_view"].ToString() == "1" )
                                                                                {
                                                                            %>
                                                                                <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png"  CommandArgument='<%# Eval("empl_id") + "," + Eval("effective_date")%>'/>
                                                        
                                                                            <%   }
                                                                            %>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" />
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
                                                <%--<asp:AsyncPostBackTrigger ControlID="Button2" />
                                                <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                                <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_department" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_empl_name" />--%>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                                <asp:AsyncPostBackTrigger ControlID="ddl_department" />
                                            </Triggers>
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
    </script>
</asp:Content>
