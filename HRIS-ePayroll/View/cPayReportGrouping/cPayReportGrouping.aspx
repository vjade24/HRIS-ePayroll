<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayReportGrouping.aspx.cs" Inherits="HRIS_ePayroll.View.cPayReportGrouping.cPayReportGrouping" %>
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
                            <div class="row">
                                <div class="col-3">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Group No.:" CssClass="font-weight-bold" ></asp:Label>
                                            <asp:Label ID="lbl_group_no" runat="server" Text="" Font-Underline="true" CssClass="font-weight-bold" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3" style="padding-right:0px !important">
                                     <asp:Label ID="Label2" runat="server" Text="Group Description:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-6 " style="padding-left:0px !important">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_group_descr"> </asp:TextBox>
                                            <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                            </div>
                            <div class="row" style="padding-right:10px;padding-left:10px">
                                <div class="col-12">
                                    <ul class="nav nav-tabs" style="border-bottom: 1px solid #dee2e6" id="myTab" role="tablist">
                                        
                                        <li class="nav-item">
                                        <a class="nav-link active" id="id_header" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab" aria-selected="false" style="font-weight:bold" aria-expanded="true">Group Information</a>
                                        </li>
                                        <li class="nav-item">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <a runat="server" class="nav-link" id="id_employee" data-toggle="tab" href="#optional_tab" role="tab" aria-controls="optional_tab" aria-selected="false" style="font-weight:bold">Add Employee</a>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </li>
                                    </ul>
                                </div>
                                <div class="tab-content" id="myTabContent" style="width:100%">
                                    <div class="tab-pane fade show active"  id="mandatory_tab" role="tabpanel" style="border: 1px solid whitesmoke;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;" aria-labelledby="id_header">
                                        
                                        <div class="row" style="padding-left:20px;padding-right:20px;padding-top:10px">
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Special Group:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_special_group" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                                        <asp:Label ID="LblRequired11" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Department:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdateDep" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_dep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Sub-Department:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_subdep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Division:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_division" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Section:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                             <div class="col-12">
                                                <hr style="margin-top:5px;margin-bottom:5px" />
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Fund Charges:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_fund_charges" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                        <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Function Code:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_function_code" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                        <asp:Label ID="LblRequired5" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Charge to:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_charge_to" CssClass="form-control form-control-sm" runat="server"></asp:TextBox> 
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold" >Signatory Name<small style="font-size:11px"> (Payroll Override):</small></label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sig_name" CssClass="form-control form-control-sm" runat="server"></asp:TextBox> 
                                                        <asp:Label ID="LblRequired15" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Designation<small style="font-size:11px"> (Payroll Override):</small></label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sig_designation" CssClass="form-control form-control-sm" runat="server"></asp:TextBox> 
                                                        <asp:Label ID="LblRequired16" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold" >Signatory Name<small style="font-size:11px"> (OBR Override):</small></label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sig2_name" CssClass="form-control form-control-sm" runat="server"></asp:TextBox> 
                                                        <asp:Label ID="LblRequired17" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4" style="padding-right:0px">
                                                <label class="font-weight-bold">Designation<small style="font-size:11px"> (OBR Override):</small></label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sig2_designation" CssClass="form-control form-control-sm" runat="server"></asp:TextBox> 
                                                        <asp:Label ID="LblRequired18" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="optional_tab" role="tabpanel" style="border: 1px solid whitesmoke;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;" aria-labelledby="id_employee">
                                        <div class="row" style="padding-top:10px">
                                            <div class="col-3">
                                                <label class="font-weight-bold">Select Employee:</label>
                                            </div>
                                            <div class="col-7" style="padding-right:0px !important">
                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                    <ContentTemplate>
                                                         <asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                        <asp:Label ID="LblRequired2" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        
                                                        <%--<div class="alert alert-success" role="alert">
                                                            <asp:Label ID="lbl_message" runat="server" Text="Successfuly Added !"></asp:Label>
                                                            <button class="close" type="button" data-dismiss="alert" aria-label="Close">
                                                                <span aria-hidden="true"> &times;</span>
                                                            </button>
                                                        </div>--%>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-2 text-right" style="padding-left:0px !important">
                                                <% if (ViewState["page_allow_add"].ToString() == "1")
                                                {  %>
                                                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server" >
                                                        <ContentTemplate>
                                                            <asp:Button ID="btn_add_empl" runat="server" CssClass="btn btn-primary btn-sm " Text="Add to Grid" OnClick="btn_add_empl_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                <% }
                                                %>  
                                            </div>
                                            <div class="col-12">
                                                <hr style="margin-top:5px;margin-bottom:5px" />
                                            </div>
                                
                                        </div>
                                        <div class="row" runat="server">
                                            <%--<div class="col-12 text-left" style="padding-top:5px !important;padding-bottom:5px !important">
                                                <div class="alert alert-primary alert-dismissible fade show small" role="alert" style="padding:5px;margin-bottom:0px;margin-left:0px;opacity:0.8;font-size:12px !important">
                                                    <strong> Note:  </strong> -- If you want to set <b>Active the In-Active status</b>, You have to <b>delete</b> Employee and <b>Add</b> it back.
                                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="padding: 0px; margin-right: 5px;">
                                                    <span aria-hidden="true" class="small">&times;</span>
                                                    </button>
                                                </div>
                                            </div>--%>
                                            <div class="col-3">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label runat="server" Text="Show" Font-Size="Small"></asp:Label>
                                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" Width="40%" ToolTip="Show entries per page" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                                <asp:ListItem Text="5" Value="5" />
                                                                <asp:ListItem Text="10" Selected="True" Value="10" />
                                                                <asp:ListItem Text="15" Value="15" />
                                                                <asp:ListItem Text="25" Value="25" />
                                                                <asp:ListItem Text="50" Value="50" />
                                                                <asp:ListItem Text="100" Value="100" />
                                                            </asp:DropDownList>
                                                        <asp:Label runat="server" Text="Entries" Font-Size="X-Small" ID="show_entries_dtl"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel3" ChildrenAsTriggers="false" UpdateMode="Conditional"  runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_search_details" onInput="search_for(event);" runat="server" class="form-control" placeholder="Search.." Height="30px" 
                                                            Width="100%" OnTextChanged="txtb_search_details_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <script type="text/javascript">
                                                            function search_for(key) {
                                                                    __doPostBack("<%= txtb_search_details.ClientID %>", "");
                                                            }
                                                        </script>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-12" style="margin-top:5px;min-height:200px !important">
                                                <asp:UpdatePanel ID="updatepanel_details" UpdateMode="Conditional" runat="server" >
                                                <ContentTemplate>
                                                    <asp:GridView 
                                                            ID="gv_details" 
                                                            runat="server" 
                                                            allowpaging="True" 
                                                            AllowSorting="True" 
                                                            AutoGenerateColumns="False" 
                                                            EnableSortingAndPagingCallbacks="True"
                                                            OnSorting="gv_details_Sorting" 
                                                            OnPageIndexChanging="gv_details_PageIndexChanging"
                                                            ForeColor="#333333" 
                                                            GridLines="Both" height="100%" 
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
                                                                        &nbsp;<asp:Label ID="lbl_employee_name" runat="server" Text='<%# Eval("employee_name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="50%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:TemplateField>
                                                               
                                                               <asp:TemplateField HeaderText="STATUS" SortExpression="emp_status_descr">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("emp_status_descr") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                               
                                                                <asp:TemplateField HeaderText="ACTION">
                                                                    <ItemTemplate>
                                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                            <ContentTemplate>
                                                                                <%--<% 
                                                                                    if (ViewState["page_allow_delete"].ToString() == "0")
                                                                                    {
                                                                                %>
                                                                                    <asp:ImageButton ID="lnkbtn_delete_details" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="lnkbtn_delete_details_Command" CommandArgument='<%# Eval("empl_id") %>'/>
                                                                                <% 
                                                                                    }
                                                                                %>--%>


                                                                                <% 
                                                                                if (ddl_special_group.SelectedValue == "02" || ddl_special_group.SelectedValue == "03" ) // Communication Expense & RATA and Quarterly Allowance
                                                                                {
                                                                                %>

                                                                                    <asp:ImageButton ID="lbkbtn_group_dtl" OnCommand="lbkbtn_group_dtl_Command" CssClass="btn btn-primary action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_edit.png" CommandArgument='<%# Eval("payroll_group_nbr") +","+Eval("empl_id")+","+Eval("employee_name")%> ' />
<% 
                                                                                }
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
                                                    <asp:AsyncPostBackTrigger ControlID="btn_add_empl" />
                                                    <asp:AsyncPostBackTrigger ControlID="txtb_search_details" />
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownList1" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            </div>
                                       </div>
                                    </div>

                                </div>
                                
                            </div>
                            
                           <div class="row" style="padding-top:5px !important">
                                <div class="col-12 text-right">
                                    <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                       <% 
                                        if (ViewState["page_allow_edit"].ToString() == "1")
                                            {
                                        %> 
                                         <asp:Button ID="Button2" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                                       <%   }
                                        %> 
                                </div>
                           </div>
                        </div>
                        
                    </div>
                        
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
            <!-- Modal Notification 1 Modal -->
                <div class="modal fade" id="notification1">
                    <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content text-center">
                    <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <i class="fa-5x fa fa-question text-danger"></i>
                                        <h3 >You Selected Another Department! </h3>
                                        <small>
                                            <asp:Label runat="server" Text="Remove all employees, Are you sure you want to Continue?"></asp:Label>
                                        </small>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="margin-bottom:50px">
                                    <asp:LinkButton ID="lnk_btn_retrieve" runat="server"  CssClass="btn btn-danger" OnClick="lnk_btn_retrieve_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                    <asp:LinkButton ID="lnk_btn_keepit"  runat="server" CssClass="btn btn-dark" OnCommand="lnk_btn_keepit_Command"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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

        <!-- The Modal - Exclude to Groupings -->
        <asp:UpdatePanel runat="server" ID="UpdatePanel29" ChildrenAsTriggers="false" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="modal fade" id="id_modal_exclude" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content ">
                            <div class="modal-header" style="background-color:#007bff;color:white">
                                <h5 class="modal-title" >Exclude/Include Payroll Groupings</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-9">
                                                <label>Employee Name</label>
                                                <asp:TextBox CssClass="form-control" runat="server" ID="employee_name" Enabled="false" Font-Bold="true"></asp:TextBox>
                                            </div>
                                            <div class="col-3">
                                                <label>ID No</label>
                                                <asp:TextBox CssClass="form-control" runat="server" ID="empl_id" Enabled="false" Font-Bold="true"></asp:TextBox>
                                            </div>
                                            <div class="col-12">
                                                <hr />
                                                <label>Status </label>
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="include_exclude_status">
                                                    <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-6">
                                                <label>Exclude Date from</label>
                                                <asp:TextBox CssClass="form-control my-date" runat="server" ID="exclude_period_from"></asp:TextBox>
                                                <asp:Label ID="exclude_period_from_req" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-6">
                                                <label>Exclude Date to</label>
                                                <asp:TextBox CssClass="form-control my-date" runat="server" ID="exclude_period_to"></asp:TextBox>
                                                <asp:Label ID="exclude_period_to_req" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-12">
                                                <label>Reason</label>
                                                <asp:TextBox CssClass="form-control" runat="server" ID="exclude_reason" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                <asp:Label ID="exclude_reason_req" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="modal-footer">
                                <asp:LinkButton  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> Close </asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-primary" runat="server" ID="btn_save_exclue" OnClick="btn_save_exclue_Click"> <i class="fa fa-save"></i> Save</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- The Modal - Exclude to Groupings -->

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
                                            &nbsp;|&nbsp;
                                            <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9" Font-Size="Smaller"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-6" style="padding-right:0px">
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
                                <div class="col-5">
                                    <div class="form-group row">
                                        <div class="col-4">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
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
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn btn-block"  Text="Add" OnClick="btnAdd_Click" />
                                             <% }
                                                %>     
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3"></div>
                                <div class="col-9">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <asp:Label runat="server" Text="Special Group:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_special_group_main" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_special_group_main_SelectedIndexChanged"  ></asp:DropDownList>
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
                                                <asp:TemplateField HeaderText="GRP #" SortExpression="payroll_group_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("payroll_group_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GROUP DESCRIPTION" SortExpression="grouping_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("grouping_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="32%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DEPARTMENT" SortExpression="department_name1">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("department_name1") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("payroll_group_nbr")%>'/>
                                                        
                                                                

                                                                
                                                                    <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("payroll_group_nbr")%>'/>
                                                                
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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_special_group_main" />
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
                backdrop: "static"
            });
            $('#id_header').click()
       };

       function openModal_Exclude()
       {
            $('#id_modal_exclude').modal({
                keyboard: false,
                backdrop: "static"
           });

           $('#<%= exclude_period_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
           $('#<%= exclude_period_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
       };

       function show_date()
       {
           $('#<%= exclude_period_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
           $('#<%= exclude_period_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
       }

       function closeModal_Exclude()
       {
           $('#id_modal_exclude').modal("hide");
            $('.modal-id_modal_exclude.show').remove();
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
        function click_information()
        {
            $('#id_header').click();
        };
         function click_addemployee()
        {
            $('#id_employee').click();
        };
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
