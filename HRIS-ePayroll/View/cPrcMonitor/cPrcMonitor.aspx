<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPrcMonitor.aspx.cs" Inherits="HRIS_ePayroll.View.cPrcMonitor.cPrcMonitor" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

        <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
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
                                    <i class="fa-5x fa fa-question text-danger" ID="delete_icon" runat="server" ></i>
                                    <h2 runat="server" id="delete_header">Delete this Record</h2>
                                    <h6><asp:Label ID="deleteRec1" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
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
                        <div class="modal-body with-background" runat="server" >
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: -10px;">
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Process Nbr.:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_process_nbr" CssClass="form-control form-control-sm text-left" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Process ID:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_process_id" CssClass="form-control form-control-sm text-left" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Run Start:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_run_start" CssClass="form-control form-control-sm text-left" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired3" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                         <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Run End:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_run_end" CssClass="form-control form-control-sm text-left" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired4" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                            <hr style="padding:0px;margin: 5px;"/>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Process Name:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_prc_name" CssClass="form-control form-control-sm" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired5" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Run Parameter(s):" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_run_params" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3" Enabled="false"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired6" runat="server" CssClass="lbl_required" ></asp:Label>
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Process Status:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_status" CssClass="form-control form-control-sm" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired7" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Process Status Details:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_status_details" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3" Enabled="false"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired8" runat="server" CssClass="lbl_required" ></asp:Label>
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                            <div class="modal-footer" style="padding-top:5px;padding-bottom:5px;" >
                            
                                <asp:UpdatePanel runat="server" >
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Close" CssClass="btn btn-danger cancel-icon icn" ></asp:LinkButton>
                                        <%--<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="col-12">
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-4"><strong style="font-family:Arial;font-size:18px;color:white;"><%: Master.page_title %></strong></div>
                <div class="col-8">
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
                                            <asp:Label runat="server" Text="Entries" Font-Size="X-Small"></asp:Label>
                                            |
                                            <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9"  Font-Size="X-Small"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-1"></div>
                                
                                <div class="col-2">
                                    <div class="form-group row">
                                        <div class="col-4 mt-1">
                                                <asp:Label runat="server"  Text="Year:" CssClass="font-weight-bold" ></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="RetrieveGrid"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-3 mt-1">
                                                <asp:Label runat="server" Text="Month:" CssClass="font-weight-bold" ></asp:Label>
                                        </div>
                                        <div class="col-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="RetrieveGrid">
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
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-4 mt-1" style="padding-right:0px">
                                            <asp:Label runat="server" Text="User ID:" CssClass="font-weight-bold" ></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_user_id" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true"  OnSelectedIndexChanged="RetrieveGrid" Enabled="false"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4"></div>
                                <div class="col-8">
                                    <div class="row">
                                        <div class="col-12"  >
                                            <asp:Label runat="server" Text="Status:" CssClass="font-weight-bold"  style="float:left;margin-top:3px" ></asp:Label>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_status"  runat="server" CssClass="form-control-sm form-control" AutoPostBack="true"  OnSelectedIndexChanged="RetrieveGrid" style="float:right;width:91.3%"></asp:DropDownList>
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
                                                <asp:TemplateField HeaderText="PRC.#" SortExpression="prc_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("prc_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="PROCESS NAME" SortExpression="prc_name">
                                                    <ItemTemplate>
                                                        <%# "&nbsp; "+Eval("prc_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="38%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="RUN START" SortExpression="prc_dttm_begin">
                                                    <ItemTemplate>
                                                        <%# Eval("prc_dttm_begin") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="17%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RUN END" SortExpression="prc_dttm_end">
                                                    <ItemTemplate>
                                                        <%# Eval("prc_dttm_end") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="17%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RUN STATUS" SortExpression="prc_status_short_descr">
                                                    <ItemTemplate>
                                                        <%# Eval("prc_status_short_descr") %> 
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
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
                                                                        <asp:ImageButton 
                                                                        ID="imgbtn_editrow1" 
                                                                        runat="server" 
                                                                        EnableTheming="true"  
                                                                        CssClass="btn btn-warning action" 
                                                                        ImageUrl="~/ResourceImages/add.png" 
                                                                        style="padding-left: 0px !important;padding-right: 3px !important;" 
                                                                        OnCommand="editRow_Command" 
                                                                        CommandArgument='<%# Eval("prc_nbr")%> ' 
                                                                        tooltip="View Details"
                                                                        />
                                                        
                                                                <%   }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton 
                                                                        ID="lnkDeleteRow" 
                                                                        CssClass="btn btn-danger action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/final_delete.png"
                                                                        style="padding-left: 6px !important;padding-right: 6px !important;" 
                                                                        OnCommand="deleteRow_Command" 
                                                                        CommandArgument='<%# Eval("prc_nbr")%> '   
                                                                        tooltip="Delete" 
                                                                        />
                                                                <% }
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
                                    <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_status" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_user_id" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="specific_scripts" runat="server">
      <script type="text/javascript">
        function openModal() {
            $('#add').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
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
    </script>
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
