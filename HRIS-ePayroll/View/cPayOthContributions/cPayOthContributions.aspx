<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayOthContributions.aspx.cs" Inherits="HRIS_ePayroll.View.cPayOthContributions.cPayOthContributions" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
    <style type="text/css">
        .datalistgrid-scrollable
        {
            overflow:scroll !important;
            width:150% !important;
            height:50% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">
    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
    
        <!-- Modal Page For Upload File -->
        <asp:UpdatePanel ID="UpdatePanel4" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="modal_upload_file">
                   <div class="modal-dialog modal-dialog-centered modal-md with-backgrounds" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)">
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="sd"><asp:Label ID="Label14" runat="server" Text="Upload File" forecolor="White"></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel13" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                        <div class="row">
                                            <div class="col-6">
                                                <asp:Label runat="server" Text="Payroll Year:" style="float:left;"></asp:Label>
                                                <div style="float:right;width:50%">
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_year_dslp" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <asp:Label runat="server" Text="Payroll Month:" style="float:left;"></asp:Label>
                                                <div style="float:right;width:50%">
                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_month_dslp" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="col-12" style="margin-top:3px">
                                                <asp:Label runat="server" style="float:left;" Text="Deduction:"></asp:Label>
                                                <div style="float:right;width:76.5%">
                                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_account_title_dspl2" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;"></asp:TextBox>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                 <hr style="margin-top:5px;margin-bottom:5px;" />
                                            </div>

                                            <div class="col-12" style="margin-top:3px">
                                                <asp:Label runat="server" style="float:left;margin-right:3px" Text="Delete Existing:"></asp:Label>
                                                <div style="float:left;width:20%">
                                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_delete_existing">
                                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="col-12" style="margin-top:3px">
                                                <asp:Label runat="server" style="float:left;" Text="Select File:"></asp:Label>
                                                <div style="float:right;width:76.5%">
                                                    <asp:UpdatePanel ID="UpdatePanel20" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="FileUpload1" CssClass="form-control form-control-sm" runat="server" />
                                                            <asp:Label ID="lbl_FileUpload1" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    
                                                </div>
                                            </div>

                                        </div>
                                        
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        </div>
                        <div class="modal-footer" style="padding-top:5px;padding-bottom:5px;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton4"  runat="server" data-dismiss="modal"  CssClass="btn btn-danger"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtn_exec_upload" OnClick="lnkbtn_exec_upload_Click" runat="server" CssClass="btn btn-warning"> <i class="fa fa-upload"></i> Upload </asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>


        <!-- Modal Page For Show Uploaded -->
        <asp:UpdatePanel ID="UpdatePanel32" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="modal_show_uploaded">
                   <div class="modal-dialog modal-dialog-centered modal-md with-backgrounds" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)">
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                        <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" ><asp:Label ID="Label5" runat="server" Text="Upload File" forecolor="White"></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            <div class="row">
                                
                            </div>
                        <!-- Modal footer -->
                        </div>
                        <div class="modal-footer" style="padding-top:5px;padding-bottom:5px;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton6"  runat="server" data-dismiss="modal"  CssClass="btn btn-danger"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton7" OnClick="lnkbtn_exec_upload_Click" runat="server" CssClass="btn btn-warning"> <i class="fa fa-upload"></i> Upload </asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="modal_upload_file_notif">
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center" style="padding-top:20px;padding-bottom:20px">
                
                <!-- Modal body -->
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
 
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12">
                            <i class="fa-5x fa fa-check-circle text-success" id="icon_modal_upload_file_notif"></i>
                            <h3 id="header_modal_upload_file_notif"></h3>
                        </div>
                        <div class="col-12 text-center" style="margin-top:5px">
                            <asp:UpdatePanel ID="UpdatePanel25" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_msglogger" runat="server" ></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-12" style="margin-top:10px">
                            <asp:LinkButton ID="LinkButton5"  runat="server" data-dismiss="modal"  CssClass="btn btn-danger"> <i class="fa fa-times"></i> Close </asp:LinkButton>
                        </div>
                    </div>
                    
                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal For Rejected Modal-->
                <div class="modal fade" id="show_rejected_modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-lg with-backgrounds" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)">
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" ><asp:Label ID="Label2" runat="server" Text="Rejected List" forecolor="White" ></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            
                            <div class="row">
                                <div class="col-4" style="margin-bottom:3px">
                                    <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                                <asp:DropDownList ID="Dropdown_rejected" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" Width="30%" OnSelectedIndexChanged="Dropdown_rejected_SelectedIndexChanged" ToolTip="Show entries per page">
                                                   <asp:ListItem Text="5" Value="5" selected="true" />
                                                   <asp:ListItem Text="10" Value="10" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                </asp:DropDownList>
                                            <asp:Label runat="server" Text="Entries"></asp:Label>
                                        </ContentTemplate>  
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="row table-responsive">
                                <div class="col-12"  style="width:100%">
                                    <asp:UpdatePanel ID="update_panel_show_rejected" UpdateMode="Conditional" runat="server" >
                                        <ContentTemplate>
                                            <asp:GridView 
                                                    ID="gv_show_rejected" 
                                                    runat="server" 
                                                    AllowPaging="true"
                                                    AllowSorting="True" 
                                                    AutoGenerateColumns="False" 
                                                    EnableSortingAndPagingCallbacks="True"
                                                    ForeColor="#333333" 
                                                    GridLines="Both" height="100%" 
                                                    OnSorting ="gv_show_rejected_Sorting"
                                                    OnPageIndexChanging="gv_show_rejected_PageIndexChanging"
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
                                                     CssClass="datalistgrid-scrollable" >
                                                   <Columns>
                                                        <asp:TemplateField HeaderText=" ID NO" SortExpression="empl_id">
                                                            <ItemTemplate>
                                                                <%# Eval("empl_id") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="CENTER" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="LAST NAME" SortExpression="empl_lastname">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("empl_lastname") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="LEFT" />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="FIRST NAME" SortExpression="empl_firstname">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("empl_firstname") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="LEFT" />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="REF NBR." SortExpression="pb_nbr">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("filler1") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="LEFT" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GRANTED AMT." SortExpression="granted_amt">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("granted_amt") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MONTHLY AMT." SortExpression="monthly_amt">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("monthly_amt") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DATE START" SortExpression="date_start">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("date_start") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="DATE END" SortExpression="date_end">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("date_end") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="REMARKS" SortExpression="remarks">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("remarks") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="30%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="LEFT" />
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
                                                    Visible="True" 
                                                    />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="10%" />
                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" VerticalAlign="Middle" Font-Size="14px" CssClass="td-header" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="#2461BF" ForeColor="White" HorizontalAlign="right" VerticalAlign="NotSet" Wrap="True" />
                                                    <RowStyle BackColor="#EFF3FB"  Font-Size="14px"/>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton1"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanel10" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="post_to_ledger_modal">
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content text-center">
                        <!-- Modal body -->
                                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body">
                                            <i class="fa-5x fa fa-question text-success"></i>
                                            <h2 >Post to Ledger ? </h2>
                                            <h6><asp:Label ID="Label1" runat="server" Text="Are you sure you want Post to Ledger "></asp:Label></h6>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            <!-- Modal footer -->
                            <div style="margin-bottom:50px">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lnkbtn_exec_post_to_ledger" runat="server"  CssClass="btn btn-success" OnClick="lnkbtn_exec_post_to_ledger_Click" > <i class="fa fa-check"></i> Yes, Post it </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton9"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>


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

        <div class="modal fade" id="AddEditConfirm">
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center">
                <!-- Modal body -->
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <i runat="server" id="icon_message" ></i>
                            <h2 runat="server" id="header_message">Successfully</h2>
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

       <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-lg with-backgrounds" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)">
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="AddEditPage"><asp:Label ID="LabelAddEdit" runat="server" Text="Add/Edit Page" forecolor="White" ></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            <div class="row" runat="server">
                                <div class="col-9">
                                    <asp:Label runat="server" Text="Deduction:" style="float:left;"></asp:Label>
                                    <div style="float:right;width:75%">
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_deduction" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <asp:Label runat="server" Text="Code:" style="float:left;"></asp:Label>
                                    <div style="float:right;width:65%">
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_deduction_code" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-9" style="margin-top:3px">
                                    <asp:Label runat="server" Text="Employee's Name:" style="float:left;"></asp:Label>
                                    <div style="float:right;width:75%">
                                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_employee_name" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-3" style="margin-top:3px">
                                    <asp:Label runat="server" Text="ID No. :" style="float:left;"></asp:Label>
                                    <div style="float:right;width:65%">
                                        <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_empl_id" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-12">
                                     <hr style="margin-top:5px;margin-bottom:5px;" />
                                </div>



                                <div class="col-4" style="margin-bottom:3px">
                                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                                <asp:DropDownList ID="ddl_panel_dtl_uploaded" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" Width="30%" OnSelectedIndexChanged="ddl_panel_dtl_uploaded_SelectedIndexChanged" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="3" Value="3" />
                                                    <asp:ListItem  Selected="True" Text="5" Value="5" />
                                                    <asp:ListItem Text="10" Value="10" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                            <asp:Label runat="server" Text="Entries"></asp:Label>
                                        </ContentTemplate>  
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                   <asp:UpdatePanel ID="update_panel_dtl_uploaded" UpdateMode="Conditional" runat="server" >
                                        <ContentTemplate>
                                            <asp:GridView 
                                                    ID="gv_dtl_uploaded" 
                                                    runat="server" 
                                                    allowpaging="True" 
                                                    AllowSorting="True" 
                                                    AutoGenerateColumns="False" 
                                                    EnableSortingAndPagingCallbacks="True"
                                                    ForeColor="#333333" 
                                                    GridLines="Both" height="100%" 
                                                    OnSorting="gv_dtl_uploaded_Sorting"
                                                    OnPageIndexChanging="gv_dtl_uploaded_PageIndexChanging"
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
                                                    >
                                                   <Columns>
                                                        <asp:TemplateField HeaderText="YEAR" SortExpression="payroll_year">
                                                            <ItemTemplate>
                                                                <%# Eval("payroll_year") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="CENTER" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MONTH" SortExpression="payroll_month">
                                                            <ItemTemplate>
                                                                <%# Eval("payroll_month").ToString() == "01" ? "&nbsp; January"   : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "02" ? "&nbsp; February"  : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "03" ? "&nbsp; March"     : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "04" ? "&nbsp; April"     : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "05" ? "&nbsp; May"       : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "06" ? "&nbsp; June"      : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "07" ? "&nbsp; July"      : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "08" ? "&nbsp; August"    : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "09" ? "&nbsp; September" : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "20" ? "&nbsp; October"   : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "11" ? "&nbsp; November"  : ""  %>
                                                                <%# Eval("payroll_month").ToString() == "12" ? "&nbsp; December"  : ""  %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="AMOUNT" SortExpression="uploaded_amount">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("uploaded_amount") %> &nbsp;
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                       <%--<asp:TemplateField HeaderText="STATUS" SortExpression="uploaded_status_descr">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("uploaded_status_descr") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>--%>
                                                       <%--<asp:TemplateField HeaderText="DATE UPLOADED" SortExpression="uploaded_dttm">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("uploaded_dttm") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                       <%--<asp:TemplateField HeaderText="UPLOADED BY" SortExpression="uploaded_by_user">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("uploaded_by_user") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>--%>
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
                                                    <RowStyle BackColor="#EFF3FB" Font-Size="14px" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                        </ContentTemplate>
                                       <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_panel_dtl_uploaded" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="padding-top:5px;padding-bottom:5px;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Close" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                        
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="notification">
                    <div class="modal-dialog modal-dialog-centered modal-sm">
                    <div class="modal-content">
                    <div class="modal-header text-white" style="background-color:#dc3545;">
                        <i class="fa fa-exclamation-triangle fa-1x text-white" style="margin-top:5px;" runat="server" id="notification_icon"></i>&nbsp;
                        <asp:Label runat="server" ID="notification_header" Text="Invalid Amount"></asp:Label>
                         <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <!-- Modal body -->
                        <div class="modal-body text-center">
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                        <asp:Label ID="lbl_notification" runat="server"></asp:Label>
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
                            <div class="row" >
                                <div class="col-lg-4">
                                    <div class="form-group row">
                                        <div class="col-lg-2">
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                        <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="100%" ToolTip="Show entries per page">
                                                            <asp:ListItem Text="5" Value="5" />
                                                            <asp:ListItem Text="10" Selected="True" Value="10" />
                                                            <asp:ListItem Text="15" Value="15" />
                                                            <asp:ListItem Text="25" Value="25" />
                                                            <asp:ListItem Text="50" Value="50" />
                                                            <asp:ListItem Text="100" Value="100" />
                                                        </asp:DropDownList>
                                                </ContentTemplate>  
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Entries" style="font-size:11px"></asp:Label>
                                            <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9" style="font-size:11px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4" >
                                    <div class="form-group row">
                                        <div class="col-lg-4">
                                            <asp:Label ID="Label3" runat="server" Text="Payroll Year:" Font-Size="Small" ></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_loan_account_name_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group row">
                                        <div class="col-lg-4" >
                                            <asp:Label ID="Label11" runat="server" Text="Payroll Month:" Font-Size="Small" ></asp:Label>
                                        </div>
                                        <div class="col-lg-8" >
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_loan_account_name_SelectedIndexChanged">
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
                                <div class="col-lg-4"></div>
                                <div class="col-lg-4" >
                                    <div class="form-group row">
                                        <div class="col-lg-4">
                                            <asp:Label ID="Label4" runat="server" Text="Employment:" Font-Size="Small" ></asp:Label>
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
                                <div class="col-lg-4" >
                                    <div class="form-group row">
                                        <div class="col-lg-4">
                                            <asp:Label  ID="Label6" runat="server" Text="Deduction Type:" Font-Size="Small" ></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_accttype_main" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_accttype_main_SelectedIndexChanged">
                                                    
                                                    </asp:DropDownList>  
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4"></div>
                                <div class="col-lg-8" >
                                    <div class="form-group row">
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label21" runat="server" Text="Deductions: " Font-Size="Small" ></asp:Label>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_loan_account_name" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_loan_account_name_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:Label ID="Label22" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-lg-2"></div>
                                        <div class="col-lg-4">
                                            <asp:UpdatePanel runat="server" ID="Update_btnAdd" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <%--<button class="btn btn-primary btn-sm"> <i class="fa fa-upload"> </i> Upload File</button>--%>
                                                    <div class="btn-group pull-right btn-block " role="group" aria-label="Button group with nested dropdown" runat="server" id="btn_options">
                                                        <div class="btn-group btn-block" role="group">
                                                            <button id="btnGroupDrop1" type="button" class="btn btn-primary dropdown-toggle btn-sm btn-block" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                               <i class="fa fa-bars"></i> Other Actions
                                                            </button>
                                                            <div class="dropdown-menu" aria-labelledby="btnGroupDrop1">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="lnkbtn_upload_file" runat="server" class="dropdown-item btn-warning" Text="Upload File" OnClick="lnkbtn_upload_file_Click"></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkbtn_post_to_ledger" runat="server" class="dropdown-item btn-success" Text="Post to Ledger" OnClick="lnkbtn_post_to_ledger_Click"></asp:LinkButton>
                                                                        <%--<asp:LinkButton ID="lnkbtn_show_uploaded" runat="server" class="dropdown-item btn-info"  Text="Show Uploaded" OnClick="lnkbtn_show_uploaded_Click"></asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="lnkbtn_show_rejected" runat="server" class="dropdown-item btn-danger"  Text="Show Rejected" OnClick="lnkbtn_show_rejected_Click"></asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <%--<Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btn_options" />
                                                </Triggers>--%>
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
                                                <asp:TemplateField HeaderText=" ID NO" SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="52%"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="UPLD AMT." SortExpression="uploaded_amount">
                                                    <ItemTemplate>
                                                       <%# Eval("uploaded_amount") %> &nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="UPLD LOAN AMT." SortExpression="uploaded_loanamount">
                                                    <ItemTemplate>
                                                        <%# Eval("uploaded_loanamount") %> &nbsp;
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
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("empl_id") %>' Visible="false"/>
                                                        
                                                                <%   }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_year") + "," + Eval("payroll_month") + "," + Eval("deduc_code")%>'/>
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
                                    <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_accttype_main" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_loan_account_name" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                    <asp:AsyncPostBackTrigger ControlID="lnkbtn_exec_upload" />
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
        function openModal()
        {
            $('#add').modal({
                keyboard: false,
                backdrop: "static"
            });
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
        
        function ExecuteUpload_File() 
        {
            if ($('#<%=FileUpload1.ClientID%>').val() == "")
            {
                $('#<%=FileUpload1.ClientID%>').css("border","0.5px solid red");
                $('#<%=lbl_msglogger.ClientID%>').css("color", "red");
                $('#<%=lbl_FileUpload1.ClientID%>').text("required field !");
                 //openModal_Upload_Notif();
                return;
            }

            $(function() {  
                var fileUpload = $('#<%=FileUpload1.ClientID%>').get(0);
                var files = fileUpload.files;  
                var test = new FormData();  
                for (var i = 0; i < files.length; i++)
                {  
                    test.append(files[i].name, files[i]);  
                }
                test.append("par_year", $("#<%= ddl_year.ClientID%>").val());
                test.append("par_month", $("#<%= ddl_month.ClientID%>").val());
                test.append("par_account",$("#<%= ddl_loan_account_name.ClientID%>").val());
                test.append("par_delete_existing",$("#<%= ddl_delete_existing.ClientID%>").val());
                test.append("par_user_id","<%:Session["ep_user_id"].ToString()%>");
                
                $.ajax({  
                    url: "FileUploader.ashx",  
                    type: "POST",  
                    contentType: false,  
                    processData: false,  
                    data: test,  
                    success: function (result)
                    {
                        if (result.split('*')[0] == "Y")
                        {
                             $('#<%= lbl_msglogger.ClientID %>').text(result.split('*')[1]);
                             $('#<%=FileUpload1.ClientID%>').css("border","0.5px solid green");
                             $('#<%=lbl_msglogger.ClientID%>').css("color", "green");

                             $('#icon_modal_upload_file_notif').removeClass();
                             $('#icon_modal_upload_file_notif').addClass("fa-5x fa fa-check-circle text-success");

                             $('#header_modal_upload_file_notif').text("Successfully Uploaded");


                        }
                        else
                        {
                            $('#<%= lbl_msglogger.ClientID %>').text(result.split('*')[1]);
                            $('#<%=FileUpload1.ClientID%>').css("border","0.5px solid red");
                            $('#<%=lbl_msglogger.ClientID%>').css("color", "red");

                            $('#icon_modal_upload_file_notif').removeClass();
                            $('#icon_modal_upload_file_notif').addClass("fa-5x fa fa-exclamation-circle text-danger");

                            $('#header_modal_upload_file_notif').text("Failed to Upload!");
                                   
                        }
                        openModal_Upload_Notif();
                    },  
                    error: function (err) {
                        alert("2.)"+err.statusText);  
                    }  
                });  
            })  
         }
        
    </script>

    <script type="text/javascript">
        function openModal_Upload()
        {
            $('#modal_upload_file').modal({
                keyboard: false,
                backdrop: "static"
            });
        }

        function closeModal_Upload()
        {
            $('#modal_upload_file').modal("hide");
            $('#AddEditConfirm').modal({
                keyboard: false,
                backdrop: "static"
                });
         setTimeout(function () {
                $('#AddEditConfirm').modal("hide");
                $('.modal-backdrop.show').remove();
                
            }, 800);
        }

        function openModal_Upload_Notif()
        {
            $('#modal_upload_file_notif').modal({
                keyboard: false,
                backdrop: "static"
            });

            setTimeout(function () {
                $('#modal_upload_file').modal("hide");
                $('#modal_upload_file_notif').modal("hide");
                $('.modal-backdrop.show').remove();
                 window.location.href = "../cPayOthContributions/cPayOthContributions.aspx"
            }, 2000);
        }

    </script>

    <script type="text/javascript">
         function openModal_PosttoLedger()
        {
            $('#post_to_ledger_modal').modal({
                keyboard: false,
                backdrop: "static"
             });
        }

        function closeModal_PosttoLedger()
        {
            $('#post_to_ledger_modal').modal("hide");
            $('#AddEditConfirm').modal({
                keyboard: false,
                backdrop: "static"
                });
         setTimeout(function () {
            $('#AddEditConfirm').modal("hide");
             $('.modal-backdrop.show').remove();
             
            }, 2000);
        }

    </script>


    <script type="text/javascript">
         function openModal_RejectedList()
        {
            $('#show_rejected_modal').modal({
                keyboard: false,
                backdrop: "static"
            });
        }
    </script>
    

    <script type="text/javascript">
         function openModal_ShowUploaded()
        {
            $('#modal_show_uploaded').modal({
                keyboard: false,
                backdrop: "static"
            });
        }
    </script>

    <script type="text/javascript">
        function openModalDelete() {
            $('#deleteRec').modal({
                keyboard: false,
                backdrop:"static"
            });
        };
        function closeModalDelete() {
            $('#deleteRec').modal('hide');
         };
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
           $('#<%= gv_dataListGrid.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight_on_grid');
           }, function () {
                   $(this).removeClass('highlight_on_grid');
           });
        });
    </script> 
</asp:Content>
