﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cEmplCommMemo.aspx.cs" Inherits="HRIS_ePayroll.View.cEmplCommMemo" %>
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
                                        <i class="fa-5x fa fa-question text-danger"></i>
                                        <h2 >Delete this Record</h2>
                                        <h6><asp:Label ID="deleteRec1" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <div style="margin-bottom:50px">
                            <asp:LinkButton ID="lnkBtnYes" runat="server"  CssClass="btn btn-danger" OnCommand="btnDelete_Command"> <i class="fa fa-check"></i> Yes, Delete it </asp:LinkButton>
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
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)">
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
                        <div class="modal-body  with-background" runat="server">
                        <div class="row" runat="server">
                                <div class="col-12">
                                    <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold"  Text="Employee Name:"></asp:Label>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList runat="server" ID="ddl_empl_id" CssClass="form-control form-control-sm"></asp:DropDownList>
                                            <asp:TextBox ID="txtb_empl_name" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="txtb_empl_id" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                            <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-12">
                                    <div class="row form-group">
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    
                                                    <asp:Label runat="server" CssClass="font-weight-bold"  Text="Amount:" ></asp:Label>
                                                    <asp:TextBox ID="txtb_amount" runat="server" CssClass="form-control form-control-sm" placeholder="00.00"></asp:TextBox>
                                                    <asp:Label ID="LblRequired2" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" CssClass="font-weight-bold"  Text="Status:"></asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddl_status" CssClass="form-control form-control-sm">
                                                        <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="In-Active" Value="I"></asp:ListItem>
                                                        <asp:ListItem Text="On-Hold" Value="O"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                 </div> 
                                
                                <div class="form-group col-md-12">
                                    <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold"  Text="Memo Description:"></asp:Label>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_memo_descr" TextMode="MultiLine" Height = "80px" runat="server" MaxLength="255"  Width="100%" CssClass="form-control"></asp:TextBox>
                                            <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                           </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                            <asp:Button ID="Button2" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                        </div>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

     <div class="col-12">
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-4"><strong style="font-family:Arial;font-size:20px;color:white;"><%: Master.page_title %></strong></div>
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
                            <div class="row" style="margin-bottom:10px">
                                <div class="col-4">
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

                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-4" >
                                                <asp:Label runat="server"  Text="Payroll Year:" ></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged1"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-4">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-2 text-right">
                                            <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                <ContentTemplate>
                                            
                                                    <% if (ViewState["page_allow_add"].ToString() == "1")
                                                        {  %>
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn"  Text="Add" OnClick="btnAdd_Click" />
                                                    <% }
                                                     %>
                                                                     
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4"></div>
                                <div class="col-lg-8" >
                                    <div class="form-group row">
                                         <div class="col-md-2">
                                         <label>Department:</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6" style="display:none">
                                    <div class="form-group row">
                                        <div class="col-md-4">
                                            <label>Sub-Department:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_subdep" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6" style="display:none">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label>Division:</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_division" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6" style="display:none" >
                                    <div class="form-group row">
                                        <div class="col-md-4">
                                         <label>Section:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList  ID="ddl_section" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                
                            </div>
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
                                                <%--<asp:TemplateField HeaderText="#" SortExpression="Id">
                                                    <ItemTemplate>
                                                        <%# Eval("Id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="ID" SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        &nbsp;<%# " "+Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="AMOUNT" SortExpression="comm__amount">
                                                    <ItemTemplate>
                                                        <%# Eval("comm_amount") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="MEMO" SortExpression="memo_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("memo_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="CREATED" SortExpression="created_at">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("created_at") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("empl_id") + ","+ Eval("Id") %>'/>
                                                        
                                                                <%   }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("empl_id")  + ","+ Eval("Id")%>'/>
                                                                <% }
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
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                    <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_department" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_subdep" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_division" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_section" />

                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
        <%--<div class="row">
            <div class="col-12">
                <table class="table">
                    <tr onclick="getrow(this)">
                        <td>Jade </td>
                    </tr>
                    <tr onclick="getrow(this)">
                        <td> Joseph </td>
                    </tr >
                    <tr onclick="getrow(this)">
                        <td>Jorge</td>
                    </tr>
                </table>
            </div>
        </div>--%>
</form>
    <%--<script type="text/javascript">
        function getrow(x)
        {
            alert(x.rowIndex);
            //alert(x.find("td").text());
            //alert($('#tr_' + x + ' > td').text());
        }
    </script>--%>
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
        function closeModalDelete() {
            $('#deleteRec').modal('hide');
         };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
</asp:Content>
