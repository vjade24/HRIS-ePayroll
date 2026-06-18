<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayAccountLedger.aspx.cs" Inherits="HRIS_ePayroll.View.cPayAccountLedger.cPayAccountLedger" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
    <style>
        .highlight-row td {
            background-color: #fffde7 !important;
            transition: background-color 0.5s ease;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
    
        <%--<div class="modal fade" id="AddEditConfirm">
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
                <div style="margin-bottom:30px">
                </div>
            </div>
            </div>
        </div>--%>


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
                                <div class="col-12" style="margin-top:-10px ">
                                    <div class="row form-group">
                                        <div class="col-3">
                                            <asp:Label runat="server" Font-Bold="true" Text="Deduction:"></asp:Label>
                                        </div>
                                        <div class="col-6" >
                                            <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                <ContentTemplate>
                                                        <asp:TextBox ID="txtb_loan_account_name_descr" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-3" style="margin-bottom:3px;margin-left:0px;padding-left:0px;">
                                            <asp:Label runat="server" Font-Bold="true" Text="Code:"  style="float:left;"></asp:Label>
                                            <div style="float:right;width:70%">
                                                 <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                        <asp:TextBox ID="txtb_deduc_code" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </div>
                                           
                                        </div>
                                        <div class="col-12">
                                            <hr style="margin-top:5px;margin-bottom:5px;" />
                                        </div>
                                        <div class="col-3" style="margin-bottom:3px">
                                             <asp:Label runat="server" Font-Bold="true" Text="Employee's Name:"></asp:Label>
                                        </div>
                                        <div class="col-6" style="margin-bottom:3px;">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_name" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_empl_name_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                                                    <asp:TextBox ID="txtb_empl_name" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;"></asp:TextBox>
                                                    <asp:Label ID="LblRequired12" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                         <div class="col-3" style="margin-bottom:3px;margin-left:0px;padding-left:0px;">
                                             <asp:Label runat="server" Font-Bold="true" Text="ID No.:" style="float:left;"></asp:Label>
                                             <div style="float:right;width:70%">
                                                 <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_employee_id" runat="server" Width="100%" CssClass="form-control form-control-sm font-weight-bold text-center" Enabled="false"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                             </div>
                                             
                                         </div>
                                        <div class="col-12">
                                            <hr style="margin-top:5px;margin-bottom:5px;" />
                                        </div>
                                    </div>
                                 </div>
                                </div>
                                <ul class="nav nav-tabs" id="myTab" role="tablist" style="margin-bottom: 5px;">
                                  <li class="nav-item">
                                    <a class="nav-link active" id="header-id" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true" onclick="unhideBUtton()"><strong>Deduction Info</strong></a>
                                  </li>
                                    <asp:UpdatePanel runat="server" >
                                        <ContentTemplate>
                                          <li class="nav-item" runat="server" id="id_hide_ledger_brkdwn" >
                                            <a class="nav-link" id="details-id-brkdwn" data-toggle="tab" href="#details-brkdwn" role="tab" aria-controls="details-brkdwn" aria-selected="false" onclick="unhideBUtton()"><strong>Deduction Details</strong></a>
                                          </li>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel runat="server" >
                                        <ContentTemplate>
                                          <li class="nav-item" runat="server" id="id_hide_payment_details" visible="false">
                                            <a class="nav-link" id="details-id" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false" onclick="unhideBUtton()"><strong>Deduction Add-ons</strong></a>
                                          </li>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel runat="server" ID="ledger_details_hide">
                                        <ContentTemplate>
                                          <li class="nav-item" runat="server" ID="ledger_details">
                                            <a class="nav-link" id="ledger-details-id" data-toggle="tab" href="#ledger-details" role="tab" aria-controls="ledger-details" aria-selected="false" onclick="hideBUtton()" ><strong><span class="badge badge-success smaller"> Payroll </span> &nbsp;&nbsp;Ledger History</strong></a>
                                          </li>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                            
                                <div class="tab-content" id="myTabContent">
                                  <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab" style="height:210px !important">
                                      <div class="row">
                                          <div class="col-sm-6">
                                              <div class="row form-group">
                                                <div class="col-6">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Deduction Date From:"></asp:Label>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" >
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_date_from" onInput="search_for(event);"  runat="server" Width="100%" CssClass="form-control form-control-sm my-date  text-center" OnTextChanged="txtb_date_from_to_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                            <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                            <script type="text/javascript">
                                                                function search_for(key) {
                                                                        __doPostBack("<%= txtb_date_from.ClientID %>", "");
                                                                }
                                                            </script>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                  <div class="col-6">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Deduction Date To:"></asp:Label>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" >
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_date_to" onInput="search_for(event);"  runat="server" Width="100%" CssClass="form-control form-control-sm my-date  text-center" OnTextChanged="txtb_date_from_to_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                            <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                            <script type="text/javascript">
                                                                function search_for(key) {
                                                                        __doPostBack("<%= txtb_date_to.ClientID %>", "");
                                                                }
                                                            </script>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lbl_no_of_months" runat="server" Font-Bold="true" Text="No. of Months:"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_no_of_months" runat="server" Width="100%" CssClass="form-control form-control-sm text-center"></asp:TextBox>
                                                            <asp:Label ID="Label8" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Deduction Status:"></asp:Label>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_status" runat="server" Width="100%" CssClass="form-control form-control-sm text-center" Enabled="false"></asp:TextBox>
                                                            <asp:Label ID="Label7" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                          </div>

                                        <div class="col-6">
                                            <div class="row form-group">
                                                <div class="col-6">
                                                     <asp:Label runat="server" Font-Bold="true" Text="Deduction Amount 1:"></asp:Label>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_amount_deduct1" runat="server" Width="100%" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                            <asp:Label ID="LblRequired9" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <asp:UpdatePanel runat="server" style="width:100%;padding-bottom:0px;margin-bottom:0px;">
                                                  <ContentTemplate>
                                                      <div class="col-12" id="div_amount2" runat="server">
                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:Label runat="server" Font-Bold="true" Text="Deduction Amount 2:"></asp:Label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtb_amount_deduct2" runat="server" Width="100%" CssClass="form-control form-control-sm text-right" ></asp:TextBox>
                                                                        <asp:Label ID="LblRequired13" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div> 
                                                  </ContentTemplate>
                                              </asp:UpdatePanel>
                                                
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                        <ContentTemplate>
                                                             <asp:Label ID="lbl_load_amount" runat="server" Font-Bold="true" Text="Loan Amount:"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_account_amount" runat="server" Width="100%" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                            <asp:Label ID="LblRequired8" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6" >
                                                    <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lbl_amount_paid" runat="server" Font-Bold="true" Text="Amount Paid:"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_amount_paid" runat="server" Width="100%" CssClass="form-control form-control-sm text-right" Enabled="false" ></asp:TextBox>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>

                                          <div class="col-12">
                                            <hr style="margin-top:5px;margin-bottom:5px;" />
                                        </div>

                                        <div class="col-12">
                                            <div class="form-group row">
                                                <%--<div class="col-3">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Deduction Type"></asp:Label>
                                                </div>
                                                <div class="col-9">
                                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" ID="ddl_deduction_type" CssClass="form-control form-control-sm" Enabled="false"></asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>--%>
                                               <%-- <div class="col-3"></div>--%>
                                                <div class="col-3">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Deduction Ref. No.:"></asp:Label>
                                                </div>
                                                <div class="col-9">
                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_check_ref_no" runat="server" Width="100%" CssClass="form-control form-control-sm" MaxLength="50"></asp:TextBox>
                                                            <asp:Label ID="LblRequired10" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <%--<div class="col-3"></div>--%>
                                            </div>
                                        </div>
                                          
                                      </div>
                                  </div>

                                  <div class="tab-pane fade" id="details-brkdwn" role="tabpanel" aria-labelledby="details-brkdwn"   style="min-height:210px !important">
                                    <div class="row" style="margin-top:10px">
                                        <div class="col-4"  >
                                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Show"></asp:Label>
                                                    <asp:DropDownList ID="DropDownList4" style="width:35%;" CssClass="form-control-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Width="15%" ToolTip="Show entries per page" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                                        <asp:ListItem Text="5" Selected="True" Value="5" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="25" Value="25" />
                                                        <asp:ListItem Text="50" Value="50" />
                                                        <asp:ListItem Text="100" Value="100" />
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" ID="Label12" Text="Entries"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-8">
                                            <asp:Button runat="server" ID="btn_add_brkdwn" CssClass="btn btn-primary btn-sm add-icon icn pull-right" OnClick="btn_add_brkdwn_Click" Text="Add" />
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="up_ledger_brkdwn" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:GridView 
                                                    ID="gv_ledger_brkdwn"
                                                    runat="server" 
                                                    allowpaging="True" 
                                                    AllowSorting="True" 
                                                    AutoGenerateColumns="False" 
                                                    EnableSortingAndPagingCallbacks="True"
                                                     OnSorting="gv_ledger_brkdwn_Sorting"
                                                     OnPageIndexChanging="gv_ledger_brkdwn_PageIndexChanging"
                                                    ForeColor="#333333" 
                                                    GridLines="Both" height="100%" 
                                                    PagerStyle-Width="3" 
                                                    PagerStyle-Wrap="false"
                                                    pagesize="5"
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
                                                        <asp:TemplateField HeaderText="SEQ NO." SortExpression="line_seq_no">
                                                            <ItemTemplate>
                                                                <%# Eval("line_seq_no") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            <HeaderStyle HorizontalAlign="center"/>
                                                            <ItemStyle Font-Size="14px" HorizontalAlign="center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REF. NBR." SortExpression="deduc_ref_nbr">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("deduc_ref_nbr") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="30%" />
                                                            <HeaderStyle HorizontalAlign="center"/>
                                                            <ItemStyle Font-Size="14px" HorizontalAlign="LEFT" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DATE FROM" SortExpression="deduc_date_from">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("deduc_date_from") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20%" />
                                                            <HeaderStyle HorizontalAlign="left"/>
                                                            <ItemStyle Font-Size="14px" HorizontalAlign="CENTER" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DATE TO" SortExpression="deduc_date_to">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("deduc_date_to") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20%" />
                                                            <HeaderStyle HorizontalAlign="left"/>
                                                            <ItemStyle Font-Size="14px" HorizontalAlign="CENTER" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AMOUNT" SortExpression="deduc_amt">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("deduc_amt") %> &nbsp;
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20%" />
                                                            <HeaderStyle HorizontalAlign="Left"/>
                                                            <ItemStyle Font-Size="14px" HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ACTION">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                <ContentTemplate>
                                                                    <% 
                                                                        if (ViewState["page_allow_edit"].ToString() == "1")
                                                                        {
                                                                    %>
                                                                        <asp:ImageButton ID="img_edit_brkdwn" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="img_edit_brkdwn_Command" CommandArgument='<%# Eval("empl_id") + "," + Eval("deduc_code")  + "," + Eval("deduc_seq")  + "," + Eval("line_seq_no") %>'/>
                                                        
                                                                    <%   }
                                                                    %>

                                                                    <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                        {
                                                                    %>
                                                                        <asp:ImageButton ID="img_delete_brkdwn" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="img_delete_brkdwn_Command" CommandArgument='<%#  Eval("empl_id") + "," + Eval("deduc_code")  + "," + Eval("deduc_seq")  + "," + Eval("line_seq_no")  %>'/>
                                                                    <%  }
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
                                                <asp:AsyncPostBackTrigger ControlID="DropDownList4" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        </div>
                                    </div>
                                  </div>

                                  <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab" style="min-height:210px !important">
                                      <div class="row" style="margin-top:10px">
                                          
                                            <div class="col-4"  >
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label runat="server" Text="Show"></asp:Label>
                                                        <asp:DropDownList ID="DropDownList1" style="width:35%;" CssClass="form-control-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Width="15%" ToolTip="Show entries per page" OnTextChanged="DropDownList1_TextChanged">
                                                            <asp:ListItem Text="5" Selected="True" Value="5" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="25" Value="25" />
                                                            <asp:ListItem Text="50" Value="50" />
                                                            <asp:ListItem Text="100" Value="100" />
                                                        </asp:DropDownList>
                                                        <asp:Label runat="server" ID="show_pagesx1" Text="Entries"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-6" runat="server" visible="false">
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
                                          <div class="col-8">
                                              <asp:Button runat="server" ID="btn_addon" CssClass="btn btn-primary btn-sm add-icon icn pull-right" OnClick="btn_addon_Click"  Text="Add" />
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
                                                        pagesize="5"
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
                                                            <asp:TemplateField HeaderText="SEQ NO." SortExpression="line_seq_no">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("line_seq_no") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" />
                                                                <HeaderStyle HorizontalAlign="center"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DATE FROM" SortExpression="monthyear_code_from_descr">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("monthyear_code_from_descr") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="25%" />
                                                                <HeaderStyle HorizontalAlign="left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DATE TO" SortExpression="monthyear_code_to_descr">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("monthyear_code_to_descr") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="25%" />
                                                                <HeaderStyle HorizontalAlign="left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMOUNT 1" SortExpression="addon_amount1">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("addon_amount1") %> &nbsp;
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%" />
                                                                <HeaderStyle HorizontalAlign="Left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMOUNT 2" SortExpression="addon_amount2">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("addon_amount2") %> &nbsp; 
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%" />
                                                                <HeaderStyle HorizontalAlign="Left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ACTION">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                    <ContentTemplate>
                                                                        <% 
                                                                            if (ViewState["page_allow_edit"].ToString() == "1")
                                                                            {
                                                                        %>
                                                                            <asp:ImageButton ID="imgbtn_edit_addon" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="imgbtn_edit_addon_Command" CommandArgument='<%# Eval("line_seq_no") %>'/>
                                                        
                                                                        <%   }
                                                                        %>

                                                                        <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                            {
                                                                        %>
                                                                            <asp:ImageButton ID="imgbtn_dlt_addon" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="imgbtn_dlt_addon_Command" CommandArgument='<%# Eval("line_seq_no") %>'/>
                                                                        <%  }
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
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownList1" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            </div>
                                      </div>
                                  </div>

                                  <div class="tab-pane fade" id="ledger-details" role="tabpanel" aria-labelledby="ledger-details"  style="min-height:210px !important">
                                      <div class="row" style="margin-top:10px">
                                        <div class="col-4"  >
                                            <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Show"></asp:Label>
                                                    <asp:DropDownList ID="DropDownList3" style="width:35%;" CssClass="form-control-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Width="15%" ToolTip="Show entries per page" OnTextChanged="DropDownList3_TextChanged">
                                                        <asp:ListItem Text="5" Selected="True" Value="5" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="25" Value="25" />
                                                        <asp:ListItem Text="50" Value="50" />
                                                        <asp:ListItem Text="100" Value="100" />
                                                    </asp:DropDownList>
                                                    <asp:Label runat="server" ID="Label10" Text="Entries"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                                <asp:UpdatePanel ID="up_ledger_info" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:GridView 
                                                        ID="gv_up_ledger_info"
                                                        runat="server" 
                                                        allowpaging="True" 
                                                        AllowSorting="True" 
                                                        AutoGenerateColumns="False" 
                                                        EnableSortingAndPagingCallbacks="True"
                                                        OnSorting="gv_up_ledger_info_Sorting"
                                                        OnPageIndexChanging="gv_up_ledger_info_PageIndexChanging"
                                                        ForeColor="#333333" 
                                                        GridLines="Both" height="100%" 
                                                        PagerStyle-Width="3" 
                                                        PagerStyle-Wrap="false"
                                                        pagesize="5"
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
                                                            <asp:TemplateField HeaderText="DATE FROM" SortExpression="deduc_date_from">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("deduc_date_from") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20%" />
                                                                <HeaderStyle HorizontalAlign="center"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DATE TO" SortExpression="deduc_date_to">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("deduc_date_to") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20%" />
                                                                <HeaderStyle HorizontalAlign="left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMOUNT" SortExpression="deduc_loan_amount">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("deduc_loan_amount") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20%" />
                                                                <HeaderStyle HorizontalAlign="left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="STATUS" SortExpression="deduc_status_descr">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("deduc_status_descr") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="30%" />
                                                                <HeaderStyle HorizontalAlign="Left"/>
                                                                <ItemStyle Font-Size="14px" HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ACTION">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtn_show_payment_details" CssClass="btn btn-warning action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_select.png" OnCommand="imgbtn_show_payment_details_Command" CommandArgument='<%# Eval("deduc_seq") + "," + Eval("empl_id")%>'  ToolTip="Show Deduction Details"/>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
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
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownList3" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                        <div class="modal-footer" style="padding-top:5px !important;padding-bottom:5px !important; ">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                    <asp:Button ID="Button2" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click"  />
                                    <asp:TextBox runat="server" ID="txtb_deduc_source_hidden" Text="U" Visible="false"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtb_deduc_seq_hidden" Visible="false" ></asp:TextBox>
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
                    <div class="modal-dialog modal-dialog-centered modal-md">
                    <div class="modal-content text-center" style="min-height:200px !important">
                    <div class="modal-header" style="border-bottom:0px !important">
                        <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>--%>
                    </div>
                    <!-- Modal body -->
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                            <ContentTemplate>
                                <div class="modal-body" style="margin-bottom:25px">
                                    <i class="fa fa-warning fa-5x text-warning"></i>
                                    <br />
                                    <h4>
                                        <asp:Label ID="lbl_notification" runat="server"></asp:Label>
                                    </h4>
                                    <asp:Label ID="Label1" Font-Size="Small" runat="server">
                                        Do you want to In-Active the existing record and create this new one ?
                                    </asp:Label>
                                    <br />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="margin-bottom:50px">
                            <asp:LinkButton ID="lnk_btn_continue" runat="server"  CssClass="btn btn-warning" OnCommand="lnk_btn_continue_Command"> <i class="fa fa-check" ></i> Yes, Continue </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton4"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel31" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="addon_modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-sm with-backgrounds" role="document">
                    <div class="modal-content modal-content-lookup" >
                        <div class="modal-header modal-header-lookup" >
                        <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title"><asp:Label ID="Label2" runat="server" Text="Add-On Description" forecolor="White"></asp:Label></h5>
                                <%--<span class="badge badge-secondary"> Notch</span>--%>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label runat="server" Font-Bold="true" Text="Add-On Date From:"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_addon_date_from" runat="server" CssClass="form-control-sm form-control" ></asp:DropDownList>
                                            <asp:Label ID="LblRequired20" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-12">
                                    <asp:Label runat="server" Font-Bold="true" Text="Add-On Date To:"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_addon_date_to" runat="server" CssClass="form-control-sm form-control" ></asp:DropDownList>
                                            <asp:Label ID="LblRequired21" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <asp:Label runat="server" Font-Bold="true" Text="Add-On Amount 1:"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_addon_amount1" runat="server" Width="100%" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                            <asp:Label ID="LblRequired22" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:UpdatePanel runat="server" style="width:100%">
                                    <ContentTemplate>
                                        <div  id="div_loanamount2" runat="server">
                                            <div class="col-12">
                                                <asp:Label runat="server" Font-Bold="true" Text="Add-On Amount 2:"></asp:Label>
                                            </div>
                                            <div class="col-12">
                                                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_addon_amount2" runat="server" Width="100%" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                        <asp:Label ID="LblRequired23" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton1"  runat="server" data-dismiss="modal" Text ="Close" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                        <asp:Button ID="btn_addons" runat="server" Text="Save to Grid" CssClass="btn btn-primary add-icon icn" OnClick="btn_addons_Click" />
                                        <asp:Label ID="lbl_hidden_line_seq" CssClass="lbl_required" runat="server" Visible="false"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel40" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="brkdwn_modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-sm with-backgrounds" role="document">
                    <div class="modal-content modal-content-lookup" >
                        <div class="modal-header modal-header-lookup" >
                        <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title"><asp:Label ID="Label13" runat="server" Text="Deduction Details Info" forecolor="White"></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label runat="server" Font-Bold="true" Text="Account Referrence #:"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_ref_nbr" runat="server" Width="100%" CssClass="form-control form-control-sm" MaxLength="25"></asp:TextBox>
                                            <asp:Label ID="LblRequired30" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row" >
                                    <div class="col-6" style="padding-right:0px !important;padding-left:0px !important;">
                                        
                                        <div class="col-12">
                                            <asp:Label runat="server" Font-Bold="true" Text="Date From:"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="UpdatePanel_From" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_deduct_from_brkdwn" runat="server" Width="100%" CssClass="form-control form-control-sm text-center my-date"></asp:TextBox>
                                                    <asp:Label ID="LblRequired31" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-6" style="padding-right:0px !important;padding-left:0px !important;">
                                        <div class="col-12">
                                            <asp:Label runat="server" Font-Bold="true" Text="Date To:"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="UpdatePanel_To" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_deduct_to_brkdwn" runat="server" Width="100%" CssClass="form-control form-control-sm text-center my-date"></asp:TextBox>
                                                    <asp:Label ID="LblRequired32" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label runat="server" Font-Bold="true" Text="Amount:"></asp:Label>
                                </div>
                                <div class="col-6">
                                    <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_deduct_amt_brkdwn" runat="server" Width="100%" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                            <asp:Label ID="LblRequired33" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton6"  runat="server" data-dismiss="modal" Text ="Close" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                        <asp:Button ID="btn_save_brkdwn" runat="server" Text="Save to Grid" CssClass="btn btn-primary add-icon icn" OnClick="btn_save_brkdwn_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                    <div class="modal-header border-0">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    </div>
                    <div class="modal-body mb-5">
                        <i runat="server" id="i_icon_display" class="fa-5x fa fa-check-circle text-success"></i>
                        <h2 runat="server" id="h2_status" >Successfully</h2>
                        <h6 class="pr-5 pl-5"><asp:Label ID="SaveAddEdit" runat="server" Text="Save"></asp:Label></h6>
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel36" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="deduction-details-modal-id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-lg with-backgrounds" role="document">
                    <div class="modal-content modal-content-lookup" >
                        <div class="modal-header modal-header-lookup" >
                        <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title"><asp:Label ID="Label9" runat="server" Text="Deduction Details History" forecolor="White"></asp:Label></h5>
                                <%--<span class="badge badge-secondary"> Notch</span>--%>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>--%>
                        </div>
                        <div class="modal-body with-background" runat="server" style="min-height:400px">
                            <div class="row" style="margin-top:-10px">
                                <div class="col-6"  >
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                            <asp:DropDownList ID="DropDownList2" CssClass="form-control-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Width="18%" ToolTip="Show entries per page" OnTextChanged="DropDownList2_TextChanged">
                                                <asp:ListItem Text="5" Value="5" />
                                                <asp:ListItem Text="10" Selected="True" Value="10" />
                                                <asp:ListItem Text="25" Value="25" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="100" Value="100" />
                                            </asp:DropDownList>
                                            <asp:Label runat="server" ID="Label6" Text="Entries"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12" >
                                        <asp:UpdatePanel ID="up_deduction_details" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:GridView 
                                                ID="gv_deduction_details"
                                                runat="server" 
                                                allowpaging="True" 
                                                AllowSorting="True" 
                                                AutoGenerateColumns="False" 
                                                EnableSortingAndPagingCallbacks="True"
                                                OnSorting="gv_deduction_details_Sorting"
                                                OnPageIndexChanging="gv_deduction_details_PageIndexChanging"
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
                                                    <asp:TemplateField HeaderText="MONTH" SortExpression="payroll_yrmonth">
                                                        <ItemTemplate>
                                                            &nbsp;<%# Eval("payroll_yrmonth") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                        <HeaderStyle HorizontalAlign="center"/>
                                                        <ItemStyle Font-Size="14px" HorizontalAlign="center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REG. NBR" SortExpression="payroll_registry_nbr">
                                                        <ItemTemplate>
                                                            &nbsp;<%# Eval("payroll_registry_nbr") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                        <HeaderStyle HorizontalAlign="left"/>
                                                        <ItemStyle Font-Size="14px" HorizontalAlign="center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REGISTRY DESCRIPTION" SortExpression="payroll_registry_descr">
                                                        <ItemTemplate>
                                                            &nbsp;<%# Eval("payroll_registry_descr") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60%" />
                                                        <HeaderStyle HorizontalAlign="left"/>
                                                        <ItemStyle Font-Size="14px" HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AMOUNT" SortExpression="deduc_amount_registry">
                                                        <ItemTemplate>
                                                            &nbsp;<%# Eval("deduc_amount_registry") %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                        <HeaderStyle HorizontalAlign="Left"/>
                                                        <ItemStyle Font-Size="14px" HorizontalAlign="right" />
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
                                                <PagerStyle CssClass="pagination-ys col-form-label-sm" BackColor="#2461BF" ForeColor="White" HorizontalAlign="right" VerticalAlign="NotSet" Wrap="True"/>
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                            <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="DropDownList2" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    </div>
                                 
                                </div>
                        </div>
                        <div class="modal-footer text-right" >
                            <asp:LinkButton ID="LinkButton5"  runat="server" data-dismiss="modal" Text ="Close" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                        </div>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    

    <div class="modal fade" id="moratorium_modal" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-xl" role="document" >
            <div class="modal-content  modal-content-add-edit">
                <div class="modal-header bg-success" >
                        <h5 class="modal-title text-white" ><asp:Label runat="server" Text="MORATORIUM"></asp:Label></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <button class="btn btn-primary btn-sm pull-right mr-3" type="button" onclick="btn_moratorium_action('','add')"><i class="fa fa-plus-square"></i> Add New</button>
                            <div class="table-responsive pt-1">
                                <table class="table table-striped table-bordered" id="datalist_grid_moratorium" style="width:100% !important;">
                                    <thead>
                                    <tr>
                                        <th style="width:20% !important">DESCRIPTION</th>
                                        <th style="width:10% !important">ID #</th>
                                        <th style="width:30% !important">EMPLOYEE NAME</th>
                                        <th style="width:20% !important">PERIOD COVERED</th>
                                        <th style="width:10% !important">STATUS</th>
                                        <th style="width:10% !important"></th>
                                    </tr>
                                    </thead>
                                </table>
                            </div>
                                
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="moratorium_modal_details" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document" >
            <div class="modal-content  modal-content-add-edit">
                <div class="modal-header bg-success" >
                        <h5 class="modal-title text-white" ><asp:Label runat="server" Text="MORATORIUM DETAILS"></asp:Label></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                <div class="modal-body">
                    <div class="form-group row">
                        <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label>Description</label>
                                    <input class="form-control" id="deduc_descr" type="text" disabled/>
                                </div>
                                <div class="col-lg-6">
                                    <label>Code</label>
                                    <input class="form-control text-center" id="deduc_code" type="text" disabled/>
                                </div>
                                <div class="col-lg-6" id="employee_select_div">
                                    <label>Employee Name</label>
                                    <select class="form-control" id="ddl_employee_name" > </select>
                                </div>
                                <div class="col-lg-6" id="employee_input_div">
                                    <label>Employee Name</label>
                                    <input class="form-control" id="employee_name" type="text" disabled/>
                                </div>
                                <div class="col-lg-6">
                                    <label>ID #</label>
                                    <input class="form-control text-center" id="empl_id" type="text" disabled/>
                                </div>
                            
                            </div>
                            <hr />
                        </div>
                        <div class="col-lg-6 border-right">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h6 class="text-uppercase"><i class="fa fa-info-circle"></i> Moratorium Information</h6>
                                </div>
                                <div class="col-lg-12">
                                    <label>Period from</label>
                                    <input class="form-control" id="period_from" type="date"/>
                                </div>
                                <div class="col-lg-12">
                                    <label>Period To</label>
                                    <input class="form-control" id="period_to" type="date" />
                                </div>
                                <div class="col-lg-12">
                                    <label>Status</label>
                                    <select class="form-control" id="rcrd_status" >
                                        <option value="1">Active</option>
                                        <option value="0">In-Active</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h6 class="text-uppercase"><i class="fa fa-info-circle"></i> Existing Deduction Details</h6>
                                </div>
                                <div class="col-lg-12">
                                    <label>Period from</label>
                                    <input class="form-control" id="exist_period_from" type="date" disabled/>
                                </div>
                                <div class="col-lg-12">
                                    <label>Period To</label>
                                    <input class="form-control" id="exist_period_to" type="date" disabled/>
                                </div>
                                <div class="col-lg-12">
                                    <label>Remarks</label>
                                    <textarea class="form-control" id="exist_remarks" disabled></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary" type="button" onclick="btn_save()"> Save Changes</button>
                </div>
            </div>
        </div>
    </div>

    <%-- DEDUCTION LEDGER AUDIT MODAL --%>
    <div class="modal fade" id="ledger_audit_modal" tabindex="-1" role="dialog" aria-labelledby="ledgerAuditLabel" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
            <div class="modal-content modal-content-add-edit">
                <div class="modal-header bg-warning">
                    <h5 class="modal-title text-white"><asp:Label runat="server" Text="DEDUCTION LEDGER AUDIT"></asp:Label></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive pt-1">
                                <table class="table table-striped table-bordered" id="datalist_grid_ledger_audit" style="width:100% !important;">
                                    <thead>
                                    <tr>
                                        <th style="width:20% !important" >EMPLOYEE NAME</th>
                                        <th style="width:20% !important" >DESCRIPTION</th>
                                        <th style="width:20% !important" >REF #</th>
                                        <th style="width:10% !important" >AMT 1</th>
                                        <th style="width:10% !important" >AMT 2</th>
                                        <th style="width:20% !important" >DELETED</th>
                                    </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

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
                                            |
                                            <asp:Label ID="show_pagesx" runat="server" style="font-size:10px" Text="Page: 9/9"></asp:Label>
                                        </ContentTemplate>  
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-1"></div>
                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-lg-4">
                                            <asp:Label  ID="Label4" runat="server" Text="Employment Type: "></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-lg-4">
                                            <asp:Label  runat="server" Text="Deduction Type:"></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_accttype_main" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_accttype_main_SelectedIndexChanged" >
                                                    
                                                </asp:DropDownList>  
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-4"></div>
                                <div class="col-8" >
                                    <div class="form-group row mt-1">
                                        <div class="col-lg-2">
                                            <asp:Label  runat="server" Text="Deductions: "></asp:Label>
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_loan_account_name" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_loan_account_name_SelectedIndexChanged"></asp:DropDownList>
                                                    
                                                    <asp:Label ID="LblRequired11" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4"></div>
                                <div class="col-lg-8">
                                    <div class="form-group row mt-1">
                                        <div class="col-lg-2"></div>
                                        <div class="col-lg-2">
                                            <button class="btn btn-success btn-sm btn-block" id="lnkbtn_moraturium" onclick="open_moratorium()" type="button"><i class="fa fa-qrcode"></i> Moratorium <span class="badge badge-danger" id="label_count_moratorium"></span></button>
                                        </div>
                                        <div class="col-lg-2">
                                            <button class="btn btn-warning btn-sm btn-block" id="lnkbtn_ledger_audit" onclick="open_ledger_audit()" type="button"><i class="fa fa-list-alt"></i> Ledger Audit <span class="badge badge-danger" id="label_count_ledger_audit"></span></button>
                                        </div>
                                         <div class="col-6 text-right" >
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
                               
                                
                                <div class="col-2" style="margin-top:3px;display:none">
                                    <asp:Label style="float:left;" ID="Label3" runat="server" Text="Status:"></asp:Label>
                                    
                                    <div style="float:right;width:70%" >
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_load_status" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"  OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" >
                                                    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="In-Active" Value="I"></asp:ListItem>
                                                </asp:DropDownList>  
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                                                <asp:TemplateField HeaderText="ID NO    " SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%--&nbsp;<%# (Eval("employee_name").ToString().Length > 20) ? Eval("employee_name").ToString().Substring(0,20) + "..." : Eval("employee_name").ToString() %>--%>
                                                        &nbsp;&nbsp;<%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        <%--&nbsp;<%# (Eval("employee_name").ToString().Length > 20) ? Eval("employee_name").ToString().Substring(0,20) + "..." : Eval("employee_name").ToString() %>--%>
                                                        &nbsp;&nbsp;<%# Eval("employee_name") + " " + Eval("remarks_descr")  %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="DATE FROM" SortExpression="deduc_date_from">
                                                    <ItemTemplate>
                                                        <%# Eval("deduc_date_from") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="DATE TO" SortExpression="deduc_date_to">
                                                    <ItemTemplate>
                                                        <%# Eval("deduc_date_to") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AMOUNT 1" SortExpression="deduc_amount1">
                                                    <ItemTemplate>
                                                        <%# Eval("deduc_amount1") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="AMOUNT 2" SortExpression="deduc_amount2">
                                                    <ItemTemplate>
                                                        <%# Eval("deduc_amount2") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
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
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("deduc_seq") + "," + Eval("empl_id") %>'/>
                                                        
                                                                <%   }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("deduc_seq") + "," + Eval("empl_id") + "," + Eval("deduc_code")%>'/>
                                                                    <button class="btn btn-sm btn-success" onclick="btn_print(<%# "'"+Eval("empl_id")+ "'" + "," + "'"+ddl_empl_type.SelectedValue.ToString().Trim()+ "'" + "," + "'" +Eval("deduc_code")+ "'" + "," + "'"+Eval("deduc_seq") + "'"%>)"><i class="fa fa-print"></i></button>
                                                                <% }
                                                                %>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
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
                                    <asp:AsyncPostBackTrigger ControlID="Button2" />
                                    <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_loan_account_name" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_loan_account_name" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div class="modal fade" id="modal_print_preview" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document" >
                <div class="modal-content  modal-content-add-edit">
                    <div class="modal-header bg-success" >
                            <h5 class="modal-title text-white" ><asp:Label runat="server" Text="Preview Report"></asp:Label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                    <div class="modal-body with-background" style="padding:0px !important">
                        <div class="row">
                            <div class="col-lg-12">
                                <iframe style="width:100% !important;height:700px !important;border:0px none;" id="iframe_print_preview"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
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
        function show_date()
        {
            $('#<%= txtb_date_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_date_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_deduct_from_brkdwn.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_deduct_to_brkdwn.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });


            if ($('#<%= txtb_date_from.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_date_from.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }

            if ($('#<%= txtb_date_to.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_date_to.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }

            
            if ($('#<%= txtb_deduct_from_brkdwn.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_deduct_from_brkdwn.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }

            if ($('#<%= txtb_deduct_to_brkdwn.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_deduct_to_brkdwn.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }
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

            $('#notification').modal("hide");
             
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
        function openAddOnTabs() {
            $('#addon_modal').modal({
                keyboard: false,
                backdrop: "static"
            });
        };
        function openBrkDwn() {
            $('#brkdwn_modal').modal({
                keyboard: false,
                backdrop: "static"
            });
            show_date();
        };
    </script>

    <script type="text/javascript">
        function closeAddOnTabs() {
            $('#addon_modal').modal("hide");
         };
        function closeBrkDwnTabs() {
            $('#brkdwn_modal').modal("hide");
        }; 
        function openNotif() {
            $('#AddEditConfirm').modal({
                 keyboard: false,
                backdrop:"static"
            });
        }

    </script>
    
    <script type="text/javascript">
        function click_1sttab() 
            {
                $('#header-id').click() 
            }
        function click_2ndttab()
            {
                $('#details-id').click() 
            }
        function click_3rdttab()
            {
                $('#ledger-details-id').click() 
            }
        function click_4thtab()
            {
                $('#details-id-brkdwn').click() 
            }
    </script>

     <script type="text/javascript">
        function openDeduction_details() {
            $('#deduction-details-modal-id').modal({
                keyboard: false,
                backdrop: "static"
            });
         };

         function hideBUtton()
         {
             document.getElementById('<%: Button2.ClientID%>').style.display = "none";
         }
         function unhideBUtton()
         {
             document.getElementById('<%: Button2.ClientID%>').style.display = "";
         }
    </script>

    <script type="text/javascript">

        var datalistgrid;
        var oTable;
        var action_cafoa     = "";
        var selected_row_idx = null;
        //$('#label_count_moratorium').text(0);
        function open_moratorium()
        {
            var deduc_code = $('#<%= ddl_loan_account_name.ClientID %>').val()
            if (!deduc_code)
            {
                alert("PLEASE SELECT DEDUCTIONS DESCRIPTION")
                return;
            }
            RetrieveGrid(deduc_code)
            $('#moratorium_modal').modal({ backdrop: 'static', keyboard: false });
        }

        function RetrieveGrid(par_deduc_code)
        {
            $('#label_count_moratorium').text("0");
            $.ajax({
                type: "POST",
                url: "cPayAccountLedger.aspx/Moratorium",
                data: JSON.stringify({ par_deduc_code: par_deduc_code }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var parsed = JSON.parse(response.d)
                    oTable.fnClearTable();
                    datalistgrid = parsed;
                    if (parsed.length > 0) {
                        oTable.fnAddData(parsed);
                        $('#label_count_moratorium').text(parsed.length);
                    }
                },
                failure: function (response) {
                    alert("Error: " + response.d);
                }
            });

        }

        function btn_moratorium_action(row,action)
        {
            var deduc_code  = $('#<%= ddl_loan_account_name.ClientID %>').val()
            var deduc_descr = $('#<%= ddl_loan_account_name.ClientID %> option:selected').text()

            $('#deduc_code').val("")
            $('#deduc_descr').val("")
            $('#empl_id').val("")
            $('#employee_name').val("")
            $('#period_from').val("")
            $('#period_to').val("")
            $('#rcrd_status').val(1)

            $('#deduc_code').val(deduc_code)
            $('#deduc_descr').val(deduc_descr)

            $('#ddl_employee_name').on('change', function ()
            {
                var var_empl_id = this.options[this.selectedIndex].getAttribute("empl_id")
                $('#empl_id').val(var_empl_id)

                ExistingDed(var_empl_id, deduc_code)

            })

            action_cafoa = action
            var data = datalistgrid[row]
            selected_row_idx = row;

            if (action == "add")
            {
                RetrieveEmpl()

                $('#employee_select_div').show();
                $('#employee_input_div').hide();

                $('#moratorium_modal_details').modal({ backdrop: 'static', keyboard: false });
            }
            else if (action == "update")
            {
                $('#employee_select_div').hide();
                $('#employee_input_div').show();

                $('#empl_id').val(data.empl_id)
                $('#employee_name').val(data.employee_name)
                $('#period_from').val(data.period_from.substring(0, 10))
                $('#period_to').val(data.period_to.substring(0, 10))
                $('#rcrd_status').val(data.rcrd_status == true ? "1" : "0")

                ExistingDed(data.empl_id, deduc_code)

                $('#moratorium_modal_details').modal({ backdrop: 'static', keyboard: false });
            }
            else if (action == "delete")
            {
                if (confirm("Are you sure you want to delete '" + data.employee_name + "' record?"))
                {

                    $.ajax({
                        type: "POST",
                        url: "cPayAccountLedger.aspx/delete_data",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ id: datalistgrid[row].id }),
                        dataType: "json",
                        success: function (response)
                        {
                            var result = response.d;
                            if (result === "success") {
                                oTable.fnDeleteRow(row);
                                $('#moratorium_modal_details').modal("hide");
                            }
                            else {
                                alert("Error: " + result);
                            }
                        },
                        error: function (xhr, status, error) {
                            alert("An error occurred: " + error);
                        }
                    });
                }
            }
            else
            {
                alert("NO ACTION")
            }
        }



        var init_table_data = function (par_data)
        {
            datalistgrid = par_data;
            oTable       = $('#datalist_grid_moratorium').dataTable(
                {
                    data        : datalistgrid,
                    sDom        : 'frtip',
                    pageLength  : 10,
                    paging      : true,
                    columns:
                    [
                        {
                            "mData": "deduc_descr",
                            "mRender": function (data, type, full, row) {
                                return "<span class=' btn-block'>&nbsp;&nbsp;" + data + "</span>"
                            }
                        },
                        {
                            "mData": "empl_id",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-center btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "employee_name",
                            "mRender": function (data, type, full, row) {
                                return "<span class=' btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "period_covered",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-center btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "rcrd_status",
                            "mRender": function (data, type, full, row) {
                                return data == 1 ? "<span class='text-center btn-block'> Active </span>" : "<span class='text-center btn-block'> In-Active </span>"  
                            }
                        },
                        {
                            "mData": "",
                            "mRender": function (data, type, full, row) {
                                return '<button type="button" class="btn text-center btn-info btn-sm" onclick=\'btn_moratorium_action(' + row["row"] + ',"update")\' data-toggle="tooltip" data-placement="top" title="Edit">  <i class="fa fa-edit"></i></button >'
                                     + '<button type="button" class="btn text-center btn-danger btn-sm" onclick=\'btn_moratorium_action(' + row["row"] + ',"delete")\' data-toggle="tooltip" data-placement="top" title="Remove">  <i class="fa fa-trash"></i></button >'
                            }
                        },
                    ],
                });
        }


        function formatPeriodCovered(from, to)
        {
            var options = { year: 'numeric', month: 'short', day: 'numeric' };
            var fromStr = from ? new Date(from).toLocaleDateString('en-US', options) : '';
            var toStr   = to   ? new Date(to).toLocaleDateString('en-US', options)   : '';
            return fromStr + ' - ' + toStr;
        }

        function btn_save()
        {
            var upsert =
            {
                 deduc_code         : $('#<%= ddl_loan_account_name.ClientID %>').val()
                ,empl_id            : $('#empl_id').val()
                ,period_from        : $('#period_from').val()
                ,period_to          : $('#period_to').val()
                ,rcrd_status        : $('#rcrd_status').val()
                ,deduc_descr        : $('#<%= ddl_loan_account_name.ClientID %> option:selected').text()           
                ,employee_name      : $('#ddl_employee_name option:selected').text()           
                ,period_covered     : formatPeriodCovered($('#period_from').val(), $('#period_to').val())
                , id: datalistgrid[selected_row_idx] == undefined ? null :  datalistgrid[selected_row_idx].id
            }

            if (upsert.deduc_code == "")
            {
                alert("deduc_code is Required!")
                return
            } else if (upsert.empl_id == "")
            {
                alert("empl_id is Required!")
                return
            }else if (upsert.period_from == "")
            {
                alert("period_from is Required!")
                return
            }else if (upsert.period_to == "")
            {
                alert("period_to is Required!")
                return
            }else if (upsert.rcrd_status == "")
            {
                alert("rcrd_status is Required!")
                return
            }

            $.ajax({
                type        : "POST",
                url         : "cPayAccountLedger.aspx/save_data",
                contentType : "application/json; charset=utf-8",
                data: JSON.stringify({ action: action_cafoa, data_string: JSON.stringify(upsert) }),
                dataType    : "json",
                success: function (response)
                {
                    var result = response.d;
                    if (result === "success")
                    {
                        var nRow = null;

                        RetrieveGrid(upsert.deduc_code)

                        if (action_cafoa == "update")
                        {
                            oTable.fnUpdate(upsert, selected_row_idx, undefined, false);
                            nRow = oTable.fnGetNodes(selected_row_idx);
                        }
                        else if (action_cafoa == "add")
                        {
                            oTable.fnAddData(upsert);
                            var allNodes = oTable.fnGetNodes();
                            nRow = allNodes[allNodes.length - 1];
                        }
                        if (nRow)
                        {
                            $(nRow).addClass('highlight-row');
                            setTimeout(function () { $(nRow).removeClass('highlight-row'); }, 3000);
                        }
                        $('#moratorium_modal_details').modal("hide");
                    }
                    else if (result.indexOf("duplicate:") === 0)
                    {
                        alert(result.replace("duplicate: ", ""));
                    }
                    else
                    {
                        alert("Error: " + result);
                    }
                },
                error: function (xhr, status, error)
                {
                    alert("An error occurred: " + error);
                }
            });
        }

        function RetrieveEmpl()
        {
            $.ajax({
                type        : "POST",
                url         : "cPayAccountLedger.aspx/RetrieveEmpl",
                contentType : "application/json; charset=utf-8",
                data        : JSON.stringify({ p_employment_type: $('#<%= ddl_empl_type.ClientID %>').val() }),
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    if (parsed.length > 0)
                    {
                        var select = document.getElementById("ddl_employee_name");
                        select.innerHTML = "";
                        var option1      = document.createElement("option");
                        option1.text     = "-- Select Here--"; 
                        option1.value    = "";
                        select.appendChild(option1);
                        // Add options to the select element
                        for (var i = 0; i < parsed.length; i++)
                        {
                            var option              = document.createElement("option");
                            option.text             = parsed[i].employee_name; 
                            option.value            = parsed[i].empl_id; 
                            option.setAttribute("empl_id", parsed[i].empl_id);
                            select.appendChild(option);
                        }
                    } else
                    {
                        var select = document.getElementById("ddl_employee_name");
                        select.innerHTML = "";
                        var option1      = document.createElement("option");
                        option1.text     = "-- Select Here--"; 
                        option1.value    = "";
                        select.appendChild(option1);
                    }
                    
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }

        function ExistingDed(empl_id, deduc_code)
        {
            var deduc_descr = $('#<%= ddl_loan_account_name.ClientID %> option:selected').text()

            $('#exist_period_to').val("")
            $('#exist_period_from').val("")
            $('#exist_remarks').val("")

            $.ajax({
                type: "POST",
                url: "cPayAccountLedger.aspx/ExistingDed",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ empl_id: empl_id, deduc_code: deduc_code }),
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    if (result.length > 0) {
                        console.log(result[0])
                        $('#exist_period_to').val(result[0].deduc_date_from.substring(0, 10))
                        $('#exist_period_from').val(result[0].deduc_date_to.substring(0, 10))
                        $('#exist_remarks').val(result[0].deduc_ref_nbr)
                    }
                    else {
                        alert("No Existing deduction for " + deduc_descr);
                    }
                },
                error: function (xhr, status, error) {
                    alert("An error occurred: " + error);
                }
            });
        }
        var datalistgrid_audit;
        var oTableAudit;

        function open_ledger_audit()
        {
            var deduc_code = $('#<%= ddl_loan_account_name.ClientID %>').val();
            if (!deduc_code)
            {
                alert("PLEASE SELECT DEDUCTIONS DESCRIPTION");
                return;
            }

            RetrieveLedgerAudit(deduc_code);
            $('#ledger_audit_modal').modal({ backdrop: 'static', keyboard: false });
            $('#ledger_audit_modal').one('shown.bs.modal', function () {
                oTableAudit.fnAdjustColumnSizing();
            });
        }

        function RetrieveLedgerAudit(par_deduc_code)
        {
            $('#label_count_ledger_audit').text("0");
            $.ajax({
                type        : "POST",
                url         : "cPayAccountLedger.aspx/DeducLedgerAudit",
                data        : JSON.stringify({ par_deduc_code: par_deduc_code }),
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d);
                    oTableAudit.fnClearTable();
                    datalistgrid_audit = parsed;
                    if (parsed.length > 0)
                    {
                        oTableAudit.fnAddData(parsed);
                        $('#label_count_ledger_audit').text(parsed.length);
                    }
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }

        function formatDateTime(d)
        {
            if (!d) return '';
            var dt = new Date(d);
            return dt.toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' })
                   + ' ' + dt.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit' });
        }

        var init_ledger_audit_table = function ()
        {
            oTableAudit = $('#datalist_grid_ledger_audit').dataTable(
            {
                data       : [],
                sDom       : 'frtip',
                pageLength : 10,
                paging     : true,
                autoWidth  : false,
                columns:
                [
                    {
                        "mData": "employee_name", "mRender": function (d, type, full) { return "<span class='btn-block'>" + (d || '') + ' <br> <small>' + full["empl_id"] + '</small></span>'; }
                    },
                    {
                        "mData": "deduc_descr", "mRender": function (d, type, full)
                        { return "<span class='btn-block'>" + (d || '') + ' <br> <small>' + (full["deduc_date_from"] ? full["deduc_date_from"].substring(0, 10) : '') + ' - ' + (full["deduc_date_to"] ? full["deduc_date_to"].substring(0, 10) : '') + '</small></span>'; }
                    },
                    { "mData": "deduc_ref_nbr",     "mRender": function (d) { return "<span class='btn-block'>" + (d || '') + "</span>"; } },
                    { "mData": "deduc_amount1",     "mRender": function (d) { return "<span class='text-right btn-block'>" + (d || '0.00') + "</span>"; } },
                    { "mData": "deduc_amount2",     "mRender": function (d) { return "<span class='text-right btn-block'>" + (d || '0.00') + "</span>"; } },
                    { "mData": "deleted_dttm",      "mRender": function (d) { return "<span class='text-center btn-block'>" + formatDateTime(d) + "</span>"; } },
                ]
            });
        };

        function btn_print(empl_id, employment_type, deduc_code, deduc_seq)
        {
            var ReportPath      = "~/Reports/cryDeductLedger/cryDeductLedger.rpt";
            var sp                  = ReportPath+","+"sp_deduct_details_per_empl,p_empl_id," + empl_id + ",p_employment_type," + employment_type+ ",p_deduc_code," + deduc_code + ",p_deduc_seq," + deduc_seq;
            previewReport(sp,"")
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
            
            embed_link = "../../printView/CrystalViewer.aspx?Params=" + ""
                + "&ReportName=" + ReportName
                + "&SaveName="   + SaveName
                + "&ReportType=" + ReportType
                + "&ReportPath=" + ReportPath
                + "&id=" + sp // + "," + parameters
            

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
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
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
            init_table_data([]);
            init_ledger_audit_table();
           $('#<%= gv_dataListGrid.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight_on_grid');
           }, function () {
                   $(this).removeClass('highlight_on_grid');
           });
        });
    </script> 
</asp:Content>
