﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayInstallation.aspx.cs" Inherits="HRIS_ePayroll.View.cPayInstallation.cPayInstallation" %>
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
                                <div class="col-6">
                                        <asp:Label runat="server" Font-Bold="true" Text="Effective Date : "></asp:Label>

                                </div>
                                <div class="col-6">
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="Update_effective_date">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date" ID="txtb_effective_date"></asp:TextBox>
                                            <asp:TextBox runat="server" Visible="false" Enabled="false" CssClass="form-control form-control-sm text-center" ID="txtb_effective_date_edit" Font-Bold="true"></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired1"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-12">
                                    <hr />
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">

                                           <div class="col-6">
                                            <asp:Label runat="server" Font-Bold="true" Text="PERA (no. of days):"></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_pera_days"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired3"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">

                                         <div class="col-6" style="padding-right:0px">
                                            <asp:Label runat="server" Font-Bold="true" Text="Monthly Salary (no. of days):"></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_ms_days"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired2"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                     
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group row">

                                         <div class="col-6">
                                            <asp:Label runat="server" Font-Bold="true" Text="Monthly OT (no. of days):"></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_ot_days"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired4"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                     
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-6">
                                            <asp:Label runat="server" Font-Bold="true" Text="Hours in 1 Day : "></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_hourstoday"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired5"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-6">
                                            <asp:Label runat="server" Font-Bold="true" Text="Minimum Net Pay : "></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_min_pay"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired6"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-6">
                                            <asp:Label runat="server" Font-Bold="true" Text="Monetization Factor: "></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_mone"></asp:TextBox>
                                                    <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired7"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
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


    <asp:UpdatePanel ID="UpdatePanel22" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- Modal Delete -->
            <div class="modal fade" id="notification">
                <div class="modal-dialog modal-dialog-centered modal-md">
                <div class="modal-content text-center">
                <div class="modal-header text-white" style="border-bottom:0px !important">
                <asp:Label runat="server" Text="Invalid Amount"></asp:Label>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                </div>
                <!-- Modal body -->
                    <div class="modal-body">
                        <div class="form-group row">
                            <div class="col-md-3">
                                <i class="fa fa-exclamation-triangle text-danger fa-4x"></i>
                            </div>
                            <div class="col-md-9">
                                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                    <ContentTemplate>
                                            <h4>This Record cannot be Saved!</h4>
                                            <p>
                                                <asp:Label ID="lbl_notification" runat="server"></asp:Label>
                                            </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
                </div>
            </div>  
        </ContentTemplate>
    </asp:UpdatePanel>

     <div class="col-12">
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-4"><strong style="font-family:Arial;font-size:19px;color:white;"><%: Master.page_title %></strong></div>
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
                                            &nbsp;|&nbsp;
                                            <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9"></asp:Label>
                                        </ContentTemplate>  
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <label ID="include_history" runat="server" class="container" style="display:inline !important;font-size:14px !important">Include History
                                                <asp:CheckBox ID="chkIncludeHistory"   OnCheckedChanged="chkIncludeHistory_CheckedChanged" Autopostback="true" runat="server" />
                                                <span class="checkmark"></span>
                                            </label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div> 

                                <div class="col-2" style="margin-left:-30px;">
                                    <div class="form-group row">
                                        <label class="col-8">Payroll Year</label>
                                        <div class="col-4" style="margin-left:-30px;">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" Width="100px" runat="server" style="padding-left:30%" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3" style="margin-bottom:-5px;margin-left:15px;">
                                    <div class="form-group row">
                                        <div class="col-8">
                                            <label style="padding-left:16px">Employment Type</label>
                                        </div>
                                        <div class="col-4" style="margin-left:-30px;">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" Width="200px" runat="server"  CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
 
                                <div class="col-1 text-right" style="margin-left:100px;">
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
                               <div class="col-12">
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

                                                   
                                                    <asp:TemplateField HeaderText="PERA DAYS" SortExpression="pera_days_conv">
                                                        <ItemTemplate>
                                                            <%# Eval("pera_days_conv") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="11%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SAL. DAYS" SortExpression="monthly_salary_days_conv">
                                                        <ItemTemplate>
                                                            <%# Eval("monthly_salary_days_conv") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="11%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="OT DAYS" SortExpression="monthly_ot_days_conv">
                                                        <ItemTemplate>
                                                            <%# Eval("monthly_ot_days_conv") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="HOURS IN 1DAY" SortExpression="hours_in_1day_conv">
                                                        <ItemTemplate>
                                                            <%# Eval("hours_in_1day_conv") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MIN. NET PAY" SortExpression="minimum_net_pay">
                                                        <ItemTemplate>
                                                            <%# Eval("minimum_net_pay") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="MONE. FACTOR" SortExpression="mone_constant_factor">
                                                        <ItemTemplate>
                                                            <%# Eval("mone_constant_factor") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ACTION">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                <ContentTemplate>
                                                                    <% 
                                                                        if (ViewState["page_allow_edit"].ToString() == "1")
                                                                        {
                                                                    %>
                                                                        <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("payroll_year") + "," + Eval("employment_type") + "," + Eval("effective_date")%>'/>
                                                        
                                                                    <%   }
                                                                    %>
                                                                    <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                        {
                                                                    %>
                                                                        <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("payroll_year") + "," + Eval("employment_type") + "," + Eval("effective_date")%>'/>
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
                                        <asp:AsyncPostBackTrigger ControlID="Button2" />
                                        <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                        <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_year" />
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
</form>
        <script type="text/javascript">
        function openModal() {
            $('#add').modal({
                keyboard: false,
                backdrop: "static"
            });
            show_date();
        };
        function show_date() {
            $('#<%= txtb_effective_date.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
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
        function closeModalDelete() {
            $('#deleteRec').modal('hide');
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
</asp:Content>
