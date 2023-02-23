<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayAccountLedger_old.aspx.cs" Inherits="HRIS_ePayroll.View.cPayAccountLedger_old.cPayAccountLedger_old" %>
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
                    <div class="modal-dialog modal-dialog-centered  modal-lg with-backgrounds" role="document">
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
                        <div class="modal-body with-background" runat="server">
                            <div class="row" runat="server">
                                <div class="col-12" >
                                    <div class="row form-group">
                                         <div class="col-3">
                                             <asp:Label runat="server" Font-Bold="true" Text="Employee's Name:"></asp:Label>
                                        </div>
                                        <div class="col-9">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_empl_id" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                 </div> 
                                <div class="col-12" style="display:none">
                                    <div class="row form-group">
                                        <div class="col-3">
                                             <asp:Label runat="server" Font-Bold="true" Text="Ledger Sequence No:"></asp:Label>
                                        </div>
                                        <div class="col-3">
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_lbl_ledger_seq" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px;" />
                                </div>
                                </div>
                                <ul class="nav nav-tabs" id="myTab" role="tablist" style="margin-bottom: 5px;">
                                  <li class="nav-item">
                                    <a class="nav-link active" id="header-id" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true"><strong>Loan Details</strong></a>
                                  </li>
                                    <asp:UpdatePanel runat="server" ID="payment_details_hide">
                                        <ContentTemplate>
                                          <li class="nav-item" runat="server" id="id_hide_payment_details">
                                            <a class="nav-link" id="details-id" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false"><strong>Payment Details</strong></a>
                                          </li>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                                <div class="tab-content" id="myTabContent">
                                  <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                                      <div class="row">
                                          <div class="col-12" >
                                            <div class="row form-group">
                                                <div class="col-3">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Account Name:"></asp:Label>
                                                </div>
                                                <div class="col-9">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddl_account_code" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_account_code_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                         </div> 
                                        <div class="col-12">
                                            <div class="row form-group">
                                                 <div class="col-3">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Sub-Account Name:"></asp:Label>
                                                </div>
                                                <div class="col-9">
                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddl_subaccount_code" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                            <asp:Label ID="LblRequired2" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                         </div> 
                                        <div class="col-6">
                                            <div class="row form-group">
                                                <div class="col-6">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Period Date From:"></asp:Label>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_date_from" runat="server" Width="100%" CssClass="form-control form-control-sm my-date" placeholder="yyyy-mm-dd"></asp:TextBox>
                                                            <asp:TextBox ID="txtb_date_from_disabled" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" Visible="false"></asp:TextBox>
                                                            <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                         </div>
                                        <div class="col-6">
                                            <div class="row form-group">
                                                <div class="col-6">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Period Date To:"></asp:Label>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_date_to" runat="server" Width="100%" CssClass="form-control form-control-sm my-date" placeholder="yyyy-mm-dd"></asp:TextBox>
                                                            <asp:TextBox ID="txtb_date_to_disabled" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" Visible="false"></asp:TextBox>
                                                            <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <hr style="margin:5px" / >
                                              <div class="form-group row">
                                                  <div class="col-3">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Ledger Type:"></asp:Label>
                                                  </div>
                                                  <div class="col-3">
                                                      <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                          <ContentTemplate>
                                                              <asp:DropDownList ID="ddl_ledger_type" runat="server" CssClass="form-control form-control-sm" >
                                                                  <asp:ListItem Value="" Text="-- Select Here -- "></asp:ListItem>
                                                                  <asp:ListItem Value="L" Text="Loan"></asp:ListItem>
                                                                  <asp:ListItem Value="C" Text="Contribution and Loan"></asp:ListItem>
                                                              </asp:DropDownList>
                                                            <asp:Label ID="LblRequired5" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                          </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                  </div>
                                                  <div class="col-3">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Ledger Status:"></asp:Label>
                                                  </div>
                                                  <div class="col-3">
                                                      <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                          <ContentTemplate>
                                                              <asp:DropDownList ID="ddl_ledger_status" runat="server" CssClass="form-control form-control-sm" >
                                                                  <asp:ListItem Value="" Text="-- Select Here -- "></asp:ListItem>
                                                                  <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                                                  <asp:ListItem Value="I" Text="In-Active"></asp:ListItem>
                                                                  <asp:ListItem Value="F" Text="Fully Paid"></asp:ListItem>
                                                              </asp:DropDownList>
                                                              <asp:Label ID="LblRequired6" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                          </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                  </div>
                                              </div>
                                          </div>
                                        <div class="col-12">
                                            <div class="form-group row">
                                                
                                                <div class="col-3" >
                                                     <asp:Label runat="server" Font-Bold="true" Text="Amount Paid:"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_amount_paid" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Loan Amount:"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_account_amount" runat="server" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            <asp:Label ID="LblRequired8" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="col-12">
                                             <div class="form-group row">
                                                <div class="col-3">
                                                     <asp:Label runat="server" Font-Bold="true" Text="Amount Deduct 1:"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_amount_deduct1" runat="server" Width="100%" CssClass="form-control form-control-sm" ></asp:TextBox>
                                                            <asp:Label ID="LblRequired9" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-3">
                                                     <asp:Label runat="server" Font-Bold="true" Text="Amount Deduct 2:"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_amount_deduct2" runat="server" Width="100%" CssClass="form-control form-control-sm" ></asp:TextBox>
                                                            <asp:Label ID="LblRequired13" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                             </div>
                                         </div>
                                        <div class="col-6"></div> 
                                         <div class="col-12" >
                                            <div class="row form-group">
                                                 <Label class="col-3 col-form-label" ><strong>Check Reference No:</strong></Label>
                                                <div class="col-9">
                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_check_ref_no" runat="server" Width="100%" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:Label ID="LblRequired10" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                         </div>
                                         
                                      </div>
                                  </div>
                                  <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                                      <div class="row" style="margin-top:10px">
                                            <div class="col-4"  >
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label runat="server" Text="Show"></asp:Label>
                                                        <asp:DropDownList ID="DropDownList1" style="width:35%;" CssClass="form-control-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Width="15%" ToolTip="Show entries per page" OnTextChanged="DropDownList1_TextChanged">
                                                            <asp:ListItem Text="5" Value="5" />
                                                            <asp:ListItem Text="10" Selected="True" Value="10" />
                                                            <asp:ListItem Text="25" Value="25" />
                                                            <asp:ListItem Text="50" Value="50" />
                                                            <asp:ListItem Text="100" Value="100" />
                                                        </asp:DropDownList>
                                                        <asp:Label runat="server" ID="show_pagesx1" Text="Entries"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel17" ChildrenAsTriggers="false" UpdateMode="Conditional"  runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_search_details" onInput="search_for(event);" runat="server" class="form-control" placeholder="Search.." Height="30px" Width="100%" AutoPostBack="true" OnTextChanged="txtb_search_details_TextChanged"></asp:TextBox>
                                                        <script type="text/javascript">
                                                            function search_for(key)
                                                            {
                                                                    __doPostBack("<%= txtb_search_details.ClientID %>", "");
                                                            }
                                                        </script>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-12">
                                                <asp:UpdatePanel ID="panel_datalistgrid_dtl" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:GridView 
                                                        ID="gv_datalistgrid_for_dtl"
                                                        runat="server" 
                                                        allowpaging="True" 
                                                        AllowSorting="True" 
                                                        AutoGenerateColumns="False" 
                                                        EnableSortingAndPagingCallbacks="True"
                                                        OnSorting="gv_datalistgrid_for_dtl_Sorting"
                                                        OnPageIndexChanging="gv_datalistgrid_for_dtl_PageIndexChanging"
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
                                                        style="margin-top:10px;margin-bottom:10px;"
                                                        >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="POSTED AMOUNT" SortExpression="posted_amount">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("posted_amount") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="30%" />
                                                                <HeaderStyle HorizontalAlign="Left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="POSTED DATE" SortExpression="posted_date">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("posted_date") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="30%" />
                                                                <HeaderStyle HorizontalAlign="Left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="center" />
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
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownList1" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            </div>
                                      </div>
                                  </div>
                                </div>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                        <asp:Button ID="Button2" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                                        
                                        <asp:Label ID="hidden_account_period_no_of_months" runat="server" Text="" Visible="false"></asp:Label>

                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                    <div class="modal-dialog modal-dialog-centered modal-sm">
                    <div class="modal-content text-center">
                    <div class="modal-header text-white" style="background-color:#dc3545;">
                    <i class="fa fa-exclamation-triangle text-white"></i>
                    <asp:Label runat="server" Text="Invalid Amount"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    </div>
                    <!-- Modal body -->
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <asp:Label ID="lbl_notification" runat="server"></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>

     <div class="col-12">
            <div class="row breadcrumb my-breadcrumb">
                <div class="col-3"><strong style="font-family:Arial;font-size:19px;color:white;"><%: Master.page_title %></strong></div>
                <div class="col-9">
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
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-6">
                                            <asp:Label ID="Label3" runat="server" Text="Payroll Year : "></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-4">
                                            <asp:Label ID="Label4" runat="server" Text="Employment Type: "></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">

                                    
                                </div>
                                <div class="col-9">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <asp:Label ID="Label5" runat="server" Text="Department: "></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:Label ID="LblRequired11" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lnkbtn_back" runat="server" CssClass="btn btn-info btn-sm" Font-Bold="true" OnClick="btn_back_Click">
                                                <i class="fa fa-arrow-left"></i>
                                                Back to Payroll Registry
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-9">
                                    <div class="form-group row">
                                        <div class="col-2" style="padding-right:0px !important">
                                            <asp:Label ID="Label6" runat="server" Text="Employee's Name:"></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_name" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_name_SelectedIndexChanged" ></asp:DropDownList>
                                                    <asp:Label ID="LblRequired12" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-2 text-right" >
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
                                                <asp:TemplateField HeaderText="SEQ NO" SortExpression="ledger_seq_no">
                                                    <ItemTemplate>
                                                        <%# Eval("ledger_seq_no") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACCOUNT DESCRIPTION" SortExpression="account_title">
                                                    <ItemTemplate>
                                                        <%--<%# Eval("account_title") %>--%>
                                                        &nbsp;<%# (Eval("account_title").ToString().Length > 20) ? Eval("account_title").ToString().Substring(0,20) + "..." : Eval("account_title").ToString() %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="DATE FROM" SortExpression="date_from">
                                                    <ItemTemplate>
                                                        <%# Eval("date_from") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="DATE TO" SortExpression="date_to">
                                                    <ItemTemplate>
                                                        <%# Eval("date_to") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AMT. DEDUCT 1" SortExpression="account_amount_deduct1">
                                                    <ItemTemplate>
                                                        <%# Eval("account_amount_deduct1") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="AMT. DEDUCT 2" SortExpression="account_amount_deduct2">
                                                    <ItemTemplate>
                                                        <%# Eval("account_amount_deduct2") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("ledger_seq_no") %>'/>
                                                        
                                                                <%   }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("ledger_seq_no")%>'/>
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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_department" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_name" />
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
            show_date();
        };
        function show_date() {
            $('#<%= txtb_date_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_date_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            }
        function to_required_header()
        {
            $("#header-id").click();
        }
        function to_required_details()
        {
            $("#details-id").click();
        }
        
    </script>
    <script type="text/javascript">
        function openDateModal() {
            $('#edit-date').modal({
                keyboard: false,
                backdrop: "static"
            });
        };
        function closeDateModal()
        {
             $('#edit-date').modal("hide");
        }
    </script>
    <script type="text/javascript">
        function openNotification() {
            $('#notification').modal({
                keyboard: false,
                backdrop: "static"
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
