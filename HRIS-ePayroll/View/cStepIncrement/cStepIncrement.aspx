<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cStepIncrement.aspx.cs" Inherits="HRIS_ePayroll.View.cStepIncrement" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server" >
        <asp:ScriptManager ID="sm_Script" runat="server"></asp:ScriptManager>
        <!-- The Modal - Generating Report -->
                <div class="modal fade" id="LoadingX">
                  <div class="modal-dialog modal-dialog-centered modal-lg">
                    <div class="modal-content text-center">
                      <!-- Modal body -->
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                  <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                   <ContentTemplate>
                                           <div class="col-12 text-center">
                                                <img src="/ResourceImages/loadingwithlogo.gif" style="width:100%;"/>
                                            </div>
                                    </ContentTemplate>
                               </asp:UpdatePanel>
                              </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                      <!-- Modal footer -->
                      <div style="margin-bottom:30px">
                      </div>
                    </div>
                  </div>
                </div>

         <!-- The Modal - Add Confirmation -->
            <div class="modal fade" id="AddEditConfirm">
              <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content text-center">
                  <!-- Modal body -->
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
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

        <!-- The Modal - Add Confirmation -->
            <div class="modal fade" id="ConfirmOverride">
              <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content text-center">
                  <!-- Modal body -->
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                 <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <i class="fa-5x fa fa-info-circle text-info"></i>
                                <h6 >Do you want to override existing "NEW" record of each employee that already exists for this Payroll year?</h6>
                                <div class="row mt-3">
                                    <div class="col-6 text-right">
                                         <%--data-dismiss="modal" aria-label="Close" --%>
                                        <asp:Button ID="btn_yes_override" runat="server" AccessKey="1"  autoPostback="true" CssClass="btn btn-success" Text="Yes, Override Existing!" OnClick="btn_extract_Click" OnClientClick="openLoading();"/>
                                        <%--<button runat="server" class="btn btn-success"><i class="fa fa-thumbs-up"></i> Yes Override!</button>--%>
                                    </div>
                                    <div class="col-6 text-left">
                                        <asp:Button ID="btn_no_override" runat="server" AccessKey="0" CommandArgument="0" autoPostback="true" CssClass="btn btn-dark" Text=" No, Don't Override! "  OnClick="btn_extract_Click" OnClientClick="openLoading();" />
                                        <%--<button runat="server" class="btn btn-dark"><i class="fa fa-times"></i> No !</button>--%>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   <!-- Modal footer -->
                  <div style="margin-bottom:30px">
                  </div>
                </div>
              </div>
            </div>
            <div class="modal fade" id="extractNotif">
              <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content text-center">
                  <!-- Modal body -->
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                         <div class="modal-body" id="notifBody">
                                <i class="fa-5x fa fa-check-circle text-success"></i>
                                 <h2 >Successfully</h2>
                                <h6><asp:Label ID="Label1" runat="server" Text="Save"></asp:Label></h6>
                                <button type="button" class="btn btn-success" > OK </button>
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
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
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
       
         <asp:UpdatePanel ID="edit_delete_notify" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="editdeletenotify" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #dc3545;color: white;">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" ><asp:Label ID="notify_header" runat="server" Text=""></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body text-center">
                                        <h6><i class="fa fa-times-circle text-danger">  </i><asp:Label ID="lbl_editdeletenotify" runat="server"></asp:Label></h6>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                     </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="add_edit_panel" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                    <div class="modal-content modal-content-add-edit">
                        <div class="modal-header modal-header-add-edit">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="AddEditPage"><asp:Label ID="LabelAddEdit" runat="server" Text="Add/Edit Page"></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-12">
                                  <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                          <label class="font-weight-bold">Employee Name:</label>
                                          <asp:TextBox ID="txtb_empl_name" MaxLength="100" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" Visible="false"></asp:TextBox>
                                           <asp:DropDownList ID="ddl_empl_name" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" Width="100%" OnSelectedIndexChanged="ddl_empl_name_SelectedIndexChanged"> </asp:DropDownList>   
                                         </ContentTemplate>
                                  </asp:UpdatePanel>
                                  <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                            <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                    </ContentTemplate>
                                  </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row" style="margin-top:10px;margin-bottom:10px">
                                <div class="col-6">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate> 
                                        <label class="font-weight-bold">Effectivity Date:</label>
                                        <asp:TextBox ID="txtb_date_of_effectivity_disable" MaxLength="100" runat="server" Width="100%" CssClass="form-control form-control-sm text-center" Enabled="false" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtb_date_of_effectivity" runat="server"  MaxLength="10" Width="100%" CssClass="form-control-sm my-date text-center" placeholder="yyyy-mm-dd"></asp:TextBox>
                                        <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                    </ContentTemplate>
                                  </asp:UpdatePanel>
                                </div>
                                <div class="col-6">
                                  <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate> 
                                        <label class="font-weight-bold">Date Generated:</label>
                                        <asp:TextBox ID="txtb_date_generated" runat="server" MaxLength="10" Width="100%" CssClass="form-control form-control-sm text-center" Enabled="false"></asp:TextBox>
                                    </ContentTemplate>
                                  </asp:UpdatePanel>
                                </div>
                            </div>
                           <div class="row" style="margin-top:10px;margin-bottom:10px">
                               <div class="col-6">
                                   <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                          <label class="font-weight-bold">Item No:</label>
                                        <asp:TextBox ID="txtb_item_no" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </ContentTemplate>
                                  </asp:UpdatePanel>
                               </div>
                               <div class="col-6">
                                   <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                          <label class="font-weight-bold">New Step Increment:</label>
                                          <asp:DropDownList ID="ddl_new_step" runat="server" Width="100%" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_new_step_SelectedIndexChanged" AutoPostBack="true">
                                              <asp:ListItem Text="--Select Here--" Value=""></asp:ListItem>
                                              <asp:ListItem Text="Step 1" Value="1"></asp:ListItem>
                                              <asp:ListItem Text="Step 2" Value="2"></asp:ListItem>
                                              <asp:ListItem Text="Step 3" Value="3"></asp:ListItem>
                                              <asp:ListItem Text="Step 4" Value="4"></asp:ListItem>
                                              <asp:ListItem Text="Step 5" Value="5"></asp:ListItem>
                                              <asp:ListItem Text="Step 6" Value="6"></asp:ListItem>
                                              <asp:ListItem Text="Step 7" Value="7"></asp:ListItem>
                                              <asp:ListItem Text="Step 8" Value="8"></asp:ListItem>
                                          </asp:DropDownList>
                                          <asp:TextBox ID="txtb_new_step" runat="server" Width="100%" Visible="false" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                          <asp:Label ID="LblRequired3" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                    </ContentTemplate>
                                  </asp:UpdatePanel>
                               </div>
                           </div>
                            <hr style="margin-top:2px;margin-bottom:2px;" />
                           <div class="row" >
                                <div class="col-4">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <label class="font-weight-bold">Salary Grade:</label>
                                        <asp:TextBox ID="txtb_salary_grade" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <label class="font-weight-bold">New Salary:</label>
                                        <asp:TextBox ID="txtb_new_salary" runat="server" Width="100%" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                        <ContentTemplate>
                                              <label class="font-weight-bold">Status:</label>
                                              <asp:DropDownList ID="ddl_status" runat="server" Width="100%" CssClass="form-control form-control-sm">
                                                  <asp:ListItem Text="--Select Here--" Value=""></asp:ListItem>
                                                  <asp:ListItem Text="New" Value="N"></asp:ListItem>
                                                  <asp:ListItem Text="Approved" Value="F"></asp:ListItem>
                                              </asp:DropDownList>
                                              <asp:Label ID="LblRequired4" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                        </ContentTemplate>
                                      </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row" style="margin-top:10px;margin-bottom:10px;">
                                <div class="col-12"> 
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                    <label class="font-weight-bold">Position Title:</label>
                                    <asp:TextBox ID="txtb_position_title" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lbl_hidden_approval_id" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_hidden_appr_status" runat="server" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                          <ContentTemplate>
                              <div class="modal-footer" style="padding:5px;">
                                  <asp:Button ID="btn_cancel"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:Button>
                                  <asp:Button ID="btn_submit"  runat="server" Text="Save" CssClass="btn btn-primary save-icon icn" OnClick="btn_submit_Click" />
                                  <%--<asp:Button ID="btn_save"    runat="server" Text="Save"   CssClass="btn btn-primary" OnClick="Btn_Save_Click" />--%>
                              </div>
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
                <div class="col-12 col-md-4"><strong style="font-family:Arial;font-size:20px;color:white;"><%: (Master.page_title.Length <= 27 ? Master.page_title:Master.page_title.Substring(0,27)+"...")%></strong></div>
                <div class="col-8">
                    <asp:UpdatePanel ID="UpdatePanel39" ChildrenAsTriggers="false" UpdateMode="Conditional"  runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txt_search" onInput="search_for(event);" runat="server" class="form-control" placeholder="Search.." Height="30px" 
                        Width="100%" OnTextChanged="txt_search_TextChanged" AutoPostBack="true"></asp:TextBox>
                           <script type="text/javascript">
                                function search_for(key)
                                {
                                        __doPostBack("<%= txt_search.ClientID %>", "");
                                }
                            </script>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <%-- This Part Update Panel is For Gridview Display --%>
                <table class="table table-bordered  table-scroll">
                    <tbody class="my-tbody">
                        <tr>
                            <td>
                                <div class="row" style="margin-bottom:10px">
                                    <div class="col-4">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Label runat="server" style="float:left;margin-top:2px;" Text="Show"></asp:Label>&nbsp;
                                                <asp:DropDownList ID="DropDownListID" OnTextChanged="DropDownListID_TextChanged" style="float:left;margin-left:2px;margin-right:2px;" CssClass="form-control form-control-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Width="28%" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="5" Value="5" />
                                                    <asp:ListItem Text="10" Selected="True" Value="10" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                                <asp:Label runat="server" style="float:left;margin-top:2px;" Text="Entries &nbsp;&nbsp;|&nbsp;&nbsp;"></asp:Label>
                                                <asp:Label ID="show_pagesx" style="float:left;margin-top:2px;" runat="server" Text="Page: 0/0"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                   <div class="col-3">
                                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                            <ContentTemplate>
                                                    <asp:Label runat="server" style="float:left;margin-top:3px;" Text="Payroll Year:"></asp:Label>
                                                    <asp:DropDownList ID="ddl_budget_code" style="float:right;" Width="60%" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm text-center" OnSelectedIndexChanged="ddl_budget_code_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                     <%--<div class="col-sm-4 text-right">
                                          <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <% if (ViewState["page_allow_add"].ToString() == "1") {%>
                                                <asp:Button ID="btn_extract"  runat="server" autoPostback="true" CssClass="btn btn-success btn-sm" Text="EXTRACT"/>
                                                <%} %>
                                            </ContentTemplate>
                                            <Triggers> 
                                                <asp:AsyncPostBackTrigger ControlID="ddl_budget_code" />
                                            </Triggers>
                                        </asp:UpdatePanel> 
                                     </div>--%>
                                    <div class="col-sm-5 text-right">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <% if (ViewState["page_allow_add"].ToString() == "1") {%>
                                                 <asp:Button ID="btn_extract"  runat="server" autoPostback="true" CssClass="btn btn-success btn-sm" Text="EXTRACT" OnClientClick="openConfirOverride();" />
                                                <asp:Button ID="btn_add"  runat="server" autoPostback="true" CssClass="btn btn-primary btn-sm add-icon icn" Text="Add" OnClick="btn_Add_Click"/>
                                                <%} %>
                                            </ContentTemplate>
                                            <Triggers> 
                                                <asp:AsyncPostBackTrigger ControlID="ddl_budget_code" />
                                            </Triggers>
                                        </asp:UpdatePanel>                       
                                    </div>
                                    
                                </div>
                                <asp:UpdatePanel ID="resignation_panel" runat="server" UpdateMode="Conditional">
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
                                        AlternatingRowStyle-Width="10%" CellPadding="2" ShowHeaderWhenEmpty="true"
                                     EmptyDataText="NO DATA FOUND" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-CssClass="no-data-found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" SortExpression="empl_id">
                                                <ItemTemplate>
                                                    <%# Eval("empl_id") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="06%" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                <ItemTemplate>
                                                    &nbsp;<%# Eval("employee_name") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="54%" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DATE GENERATED" SortExpression="date_generated">
                                                <ItemTemplate>
                                                    <%# Eval("date_generated") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STATUS" SortExpression="approval_status_descr">
                                                <ItemTemplate>
                                                    <%# Eval("approval_status_descr") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="CENTER" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ACTION">
                                                <ItemTemplate>
                                                    <% if(ViewState["page_allow_edit"].ToString() == "1"){ %>
                                                     <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("empl_id") + ", " + Eval("date_generated") + ", " + Eval("approval_status")  %>' ImageAlign="Middle" />
                                                    <%} %>
                                                    <%if(ViewState["page_allow_print"].ToString() == "1"){ %>
                                                    <asp:ImageButton ID="imgbtn_print1" CssClass="btn btn-success action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/print.png" OnClientClick="openLoading();" OnCommand="printRow_Command" CommandArgument='<%# Eval("empl_id") + ", " + Eval("date_generated") + ", " + Eval("approval_status") %> ' />
                                                    <%} %>
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
                                    <asp:AsyncPostBackTrigger ControlID="btn_add" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                    <asp:AsyncPostBackTrigger ControlID="txt_search" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_budget_code" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#add').modal({
                keyboard: false,
                backdrop:"static"
            });
            show_date();
        };
        function show_date() {
            $('#<%= txtb_date_of_effectivity.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            if ($('#<%= txtb_date_of_effectivity.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_date_of_effectivity.ClientID %>').closest("div");
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
        function openModalNotify() {
            $('#editdeletenotify').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>
    <script type="text/javascript">
        function closeModalNotify() {
            $('#editdeletenotify').modal('hide');
         };
    </script>
     <script type="text/javascript">
        function openLoading() {
           
            $('#LoadingX').modal({
                keyboard: false,
                backdrop:"static"
            });
         }

         function openConfirOverride()
         {
             $('#ConfirmOverride').modal({
                keyboard: false,
                backdrop:"static"
            });
         }

         function openExtractNotifiation(par_value)
         {
             $('#LoadingX').modal("hide");
             $('#LoadingX').removeClass("show");
             $('#LoadingX').css("display", "none");
             $('div.modal-backdrop.fade.show').remove();
             $('#ConfirmOverride').modal("hide");
             if (par_value > 0)
                     {
                         $("#notifBody").html(''
                         +'<i class="fa-5x fa fa-check-circle text-success"></i>'
                         + '<h2 >Successfully Extracted ' + par_value + ' entrie/s.!</h2>' 
                         +'<button type="button" class="btn btn-success" data-dismiss="modal" aria-label="Close"> &nbsp; OK &nbsp; </button>'+
                         '');  
                     }
                     if (par_value == 0)
                     {
                         $("#notifBody").html(''
                         +'<i class="fa-5x fa fa-warning text-danger"></i>'
                         + '<h2 >' + par_value + ' Additional Extracted from plantilla!</h2>' 
                         +'<button type="button" class="btn btn-danger" data-dismiss="modal" aria-label="Close"> &nbsp; OK &nbsp; </button>'+
                         '');  
                     }
           
                     $('#extractNotif').modal({
                        keyboard: false,
                        backdrop:"static"
                 });

             //setTimeout(function () {
             //    if (par_value > 0)
             //        {
             //            $("#notifBody").html(''
             //            +'<i class="fa-5x fa fa-check-circle text-success"></i>'
             //            + '<h2 >Successfully Extracted ' + par_value + ' entrie/s.!</h2>' 
             //            +'<button type="button" class="btn btn-success" data-dismiss="modal" aria-label="Close"> &nbsp; OK &nbsp; </button>'+
             //            '');  
             //        }
             //        if (par_value == 0)
             //        {
             //            $("#notifBody").html(''
             //            +'<i class="fa-5x fa fa-warning text-danger"></i>'
             //            + '<h2 >' + par_value + ' Additional Extracted from plantilla!</h2>' 
             //            +'<button type="button" class="btn btn-danger" data-dismiss="modal" aria-label="Close"> &nbsp; OK &nbsp; </button>'+
             //            '');  
             //        }
           
             //        $('#extractNotif').modal({
             //           keyboard: false,
             //           backdrop:"static"
             //    });

             //}, 500);
             
        }
    </script>
</asp:Content>

