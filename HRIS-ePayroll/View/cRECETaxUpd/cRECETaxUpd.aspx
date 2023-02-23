<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cRECETaxUpd.aspx.cs" Inherits="HRIS_ePayroll.View.cRECETaxUpd.cRECETaxUpd" %>
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

        <asp:UpdatePanel ID="approval_confirm_popup_header" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="approval_confirm_popup">
                    <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content text-center">
                    <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <i class="fa-5x fa fas fa-exclamation-circle text-warning"></i>
                                        <h2><asp:Label ID="approval_hdr" runat="server" Text="Approve this record"></asp:Label></h2>
                                        <h6><asp:Label ID="approval_dtl" runat="server" Text="Are you sure to Approve this Record"></asp:Label></h6>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <div style="margin-bottom:50px">
                            <asp:LinkButton ID="lnkBtnYes" OnCommand="btn_approval_Click" runat="server"  CssClass="btn btn-warning"> <i class="fa fa-check"></i> <asp:Label ID="approval_footer" runat="server" Text="Yes"></asp:Label></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- The Modal - Add Confirmation -->
        <div class="modal fade" id="NotificationAlert">
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center">
                <!-- Modal body -->
                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                    <ContentTemplate>
                    <div class="modal-body">
                        <i runat="server" id="i_icon_display" class="fa-5x fa fa-check-circle text-success"></i>
                        <h2 runat="server" id="h2_status" ></h2>
                        <h6><asp:Label ID="Label8" runat="server"></asp:Label></h6>
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


        <asp:UpdatePanel ID="UpdatePanel22" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="approve_all_confirm_popup">
                    <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content text-center">
                    <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <i class="fa-5x fa fas fa-thumbs-up text-success"></i>
                                        <h2><asp:Label ID="Label4" runat="server" Text="Approve All record?"></asp:Label></h2>
                                        <h6><asp:Label ID="Label5" runat="server" Text="Are you sure you want to Approve All"></asp:Label></h6>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <div style="margin-bottom:50px">
                            <asp:LinkButton ID="lnkbtn_approve_all" OnCommand="lnkbtn_approve_all_Command" runat="server"  CssClass="btn btn-success"> <i runat="server" id="id_approveall_icn" class="fa fa-check"></i> <asp:Label ID="Label6" runat="server" Text="Yes, Approve All"></asp:Label></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No </asp:LinkButton>
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
                        <div class="modal-header text-center" style="background-image:linear-gradient(green, yellow);padding:8px!important;">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title text-center" id="AddEditPage"><asp:Label ID="LabelAddEdit" runat="server" Text="Add/Edit Page" forecolor="White"></asp:Label></h5>
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
                                        <div class="col-9">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Employee's Name:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_employee_name" CssClass="form-control form-control-sm font-weight-bold" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-3">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="ID No:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_empl_id" CssClass="form-control form-control-sm text-center font-weight-bold" Enabled="false" ></asp:TextBox>
                                                    <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                            <hr style="padding:0px;margin: 5px;"/>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Tax Rate :" CssClass="font-weight-bold text-left" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_basic_tax_rate" CssClass="form-control form-control-sm text-center font-weight-bold" Enabled="false" style="float:right;width:40%" ></asp:TextBox>
                                                    <asp:Label ID="LblRequired3" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                         <div class="col-8">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="WTax Amt.:" CssClass="font-weight-bold text-left" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_wtax_amt" CssClass="form-control form-control-sm text-right font-weight-bold" Enabled="false" style="float:right;width:60%" ></asp:TextBox>
                                                    <asp:Label ID="LblRequired4" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        
                                        <div class="col-12">
                                            <hr style="padding:0px;margin: 5px;"/>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Effective Date :" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_effective_date" CssClass="form-control form-control-sm text-center font-weight-bold" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="Label2" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Record Status:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_status" CssClass="form-control form-control-sm font-weight-bold" Enabled="false" Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired7" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Payroll Year :" CssClass="font-weight-bold text-left" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_year" CssClass="form-control form-control-sm text-center font-weight-bold" Enabled="false"></asp:TextBox>
                                                    <asp:Label ID="Label1" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                            <div class="modal-footer" >
                            
                                <asp:UpdatePanel runat="server" >
                                    <ContentTemplate>
                                        <div class="col-lg-12 text-center">
                                            <asp:Label ID="lbl_status_descr" runat="server" CssClass="badge badge-danger" ></asp:Label>
                                        </div>
                                        <div class="col-lg-12" style="padding-left:25px;padding-right:25px">
                                            <asp:LinkButton ID="btn_approve" runat="server" OnClick="btn_approve_Click" CssClass="btn btn-success pt-2" Width="200" Height="40"><i class="fa fa-thumbs-up"></i> Approve</asp:LinkButton>
                                            <asp:LinkButton ID="btn_reject" runat="server"  OnClick="btn_reject_Click" CssClass="btn btn-danger pt-2" Width="200" Height="40" ><i class="fa fa-thumbs-down"></i> Reject</asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server"  ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal fade" id="modal_details" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-lg" role="document">
                        <div class="modal-content" style="background-image:linear-gradient(white, lightblue)" >
                            <div class="modal-header text-center" style="background-image:linear-gradient(green, yellow);padding:8px!important;">
                                <h5 class="modal-title text-center" ><asp:Label ID="Label3" runat="server" Text="Show Details" forecolor="White"></asp:Label></h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body with-background" runat="server" >
                                <div class="row">
                                    <div class="col-9">
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <asp:Label runat="server" Text="Employee's Name:" CssClass="font-weight-bold text-lef" ></asp:Label>
                                                <asp:TextBox runat="server" ID="txtb_employee_name_dtl" CssClass="form-control form-control-sm font-weight-bold" Enabled="false" style="float:right;width:70%"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-3">
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <asp:Label runat="server" Text="ID No:" CssClass="font-weight-bold text-lef" ></asp:Label>
                                                <asp:TextBox runat="server" ID="txtb_empl_id_dtl" CssClass="form-control form-control-sm text-center font-weight-bold" Enabled="false" style="float:right;width:55%"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-12">
                                        <hr style="padding:0px;margin: 5px;"/>
                                    </div>
                                    <div class="col-3">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:Label runat="server" Text="Show"></asp:Label>
                                                    <asp:DropDownList ID="ddl_show_dtl" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="ddl_show_dtl_TextChanged" Width="40%" ToolTip="Show entries per page">
                                                        <asp:ListItem Text="5" Value="5" />
                                                        <asp:ListItem Text="10" Selected="True" Value="10" />
                                                        <asp:ListItem Text="15" Value="15" />
                                                        <asp:ListItem Text="25" Value="25" />
                                                        <asp:ListItem Text="50" Value="50" />
                                                        <asp:ListItem Text="100" Value="100" />
                                                    </asp:DropDownList>
                                                <asp:Label runat="server" Text="Entries" ></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-1"></div>
                                    <div class="col-8 mb-2">
                                    <asp:UpdatePanel ID="UpdatePanel17" ChildrenAsTriggers="false" UpdateMode="Conditional"  runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_search_dtl" onInput="search_for(event);" runat="server" class="form-control" placeholder="Search.." Height="30px" 
                                                Width="100%" OnTextChanged="txtb_search_TextChanged_dtl" AutoPostBack="true"></asp:TextBox>
                                            <script type="text/javascript">
                                                function search_for(key) {
                                                        __doPostBack("<%= txtb_search_dtl.ClientID %>", "");
                                                }
                                            </script>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="up_dataListGrid_dtl" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView
                                            ID="gv_dataListGrid_dtl"
                                            runat="server"
                                            AllowPaging="True"
                                            AllowSorting="True"
                                            AutoGenerateColumns="False"
                                            EnableSortingAndPagingCallbacks="True"
                                            ForeColor="#333333"
                                            GridLines="Both" Height="100%"
                                            onsorting="gv_dataListGrid_Sorting_dtl"  
                                            OnPageIndexChanging="gridviewbind_PageIndexChanging_dtl"
                                            PagerStyle-Width="3"
                                            PagerStyle-Wrap="false"
                                            PageSize="10"
                                            Width="100%"
                                            Font-Names="Century gothic"
                                            Font-Size="Medium"
                                            RowStyle-Width="5%"
                                            AlternatingRowStyle-Width="10%"
                                            CellPadding="2"
                                            ShowHeaderWhenEmpty="True"
                                            EmptyDataText="NO DATA FOUND"
                                            EmptyDataRowStyle-ForeColor="Red"
                                            EmptyDataRowStyle-CssClass="no-data-found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="VOUCHER NBR" SortExpression="voucher_nbr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("voucher_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="PERIOD COVERED" SortExpression="payroll_period_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("payroll_period_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="REMARKS" SortExpression="remarks">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("remarks") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="GROSS PAY" SortExpression="gross_pay">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("gross_pay") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
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
                                        <asp:AsyncPostBackTrigger ControlID="txtb_search_dtl" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_show_dtl" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div class="col-12">
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-6"><strong style="font-family:Arial;font-size:18px;color:white;padding-top:5px">For Review/Approval -> Tax Update for Regular/Casual</strong></div>
                <div class="col-6">
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
                                <div class="col-3 text-right" style="padding-top:5px" >
                                     <asp:Label runat="server" Font-Bold="true" Text="Employment Type:" ></asp:Label>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                           
                                            <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass=" form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" ></asp:DropDownList>
                                               
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-2">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>

                                            <asp:LinkButton ID="lnbtn_approve_all"  runat="server" OnCommand="lnbtn_approve_all_Command" CssClass="btn btn-success btn-sm btn-block"> <i class="fa fa-thumbs-up"></i> <asp:Label ID="Label7" runat="server" CssClass="font-weight-bold" Text="Approve All"> </asp:Label></asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                                <asp:TemplateField HeaderText="ID NO" SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="08%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%#  Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               
                                               <asp:TemplateField HeaderText="TAX RATE" SortExpression="tax_rate">
                                                    <ItemTemplate>
                                                        <%# Eval("tax_rate") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TAX AMT." SortExpression="wtax_amt">
                                                    <ItemTemplate>
                                                        <%# Eval("wtax_amt") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="YEAR" SortExpression="payroll_year">
                                                    <ItemTemplate>
                                                        <%# Eval("payroll_year") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="EFF. DATE" SortExpression="effective_date">
                                                    <ItemTemplate>
                                                        <%# Eval("effective_date") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS" SortExpression="rcrd_status_descr">
                                                    <ItemTemplate>
                                                        <%# Eval("rcrd_status_descr") %> 
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                    
                                                                    
                                                               
                                                                        <asp:ImageButton 
                                                                        ID="imgbtn_editrow1" 
                                                                        runat="server" 
                                                                        EnableTheming="true"  
                                                                        CssClass="btn btn-warning action" 
                                                                        ImageUrl="~/ResourceImages/add.png" 
                                                                        style="padding-left: 0px !important;padding-right: 3px !important;" 
                                                                        OnCommand="imgbtn_add_empl_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("effective_date") + "," + Eval("payroll_year") %> ' 
                                                                        tooltip="View Details"
                                                                        />
                                                        
                                                               
                                                                    <asp:ImageButton 
                                                                        ID="lnkDeleteRow" 
                                                                        CssClass="btn btn-primary action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/review.png"
                                                                        style="padding-left: 6px !important;padding-right: 6px !important;" 
                                                                        OnCommand="approve_reject_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("effective_date") + "," + Eval("payroll_year") %> ' 
                                                                        tooltip="Approve or Reject" 
                                                                        />
                                                                <asp:ImageButton 
                                                                        ID="ImageButton1" 
                                                                        CssClass="btn btn-success action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/approve.png"
                                                                        style="padding-left: 6px !important;padding-right: 6px !important;" 
                                                                        Oncommand ="approve_Command"
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("effective_date") + "," + Eval("payroll_year") %> ' 
                                                                        tooltip="Approve" 
                                                                        />
                                                                <asp:ImageButton 
                                                                        ID="ImageButton2" 
                                                                        CssClass="btn btn-danger action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/cancel.png"
                                                                        style="padding-left: 6px !important;padding-right: 6px !important;" 
                                                                        Oncommand ="reject_Command"
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("effective_date") + "," + Eval("payroll_year") %> ' 
                                                                        tooltip="Reject" 
                                                                        />
                                                              
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="17%" />
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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
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
          function openModal_dtl() {
              $('#modal_details').modal({
                keyboard: false,
                backdrop:"static"
            });
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

    <script type="text/javascript">
        function openModalApproval() {
            $('#approval_confirm_popup').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>
     
    <script type="text/javascript">
        function closeModalApproval() {
            $('#approval_confirm_popup').modal('hide');
         };
    </script>

    <script type="text/javascript">
        function openModalApprove_All() {
            $('#approve_all_confirm_popup').modal({
                keyboard: false,
                backdrop:"static"
            });
        };
        function closeModalApprove_All() {
            $('#approve_all_confirm_popup').modal('hide');
        };
        function closeModalApproval_1() {
            $('#NotificationAlert').modal({
                keyboard: false,
                backdrop:"static"
            });
         };
    </script>

</asp:Content>
