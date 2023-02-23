<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayOthContributionsGSIS.aspx.cs" Inherits="HRIS_ePayroll.View.cPayOthContributionsGSIS.cPayOthContributionsGSIS" %>
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
                    <div class="modal-content" >
                        <div class="modal-header" >
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="sd"><asp:Label ID="Label14" runat="server" Text="Upload File" ></asp:Label></h5>
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
                                            <div class="col-lg-6">
                                                <asp:Label runat="server" Text="Payroll Year:" style="float:left;"></asp:Label>
                                                <div style="float:right;width:50%">
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_year_dslp" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label runat="server" Text="Payroll Month:" style="float:left;"></asp:Label>
                                                <div style="float:right;width:50%">
                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_month_dslp" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            
                                            <div class="col-lg-12">
                                                 <hr style="margin-top:5px;margin-bottom:5px;" />
                                            </div>

                                            <div class="col-lg-12" style="margin-top:3px">
                                                <asp:Label runat="server" style="float:left;margin-right:3px" Text="Delete Existing:"></asp:Label>
                                                <div style="float:left;width:20%">
                                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_delete_existing">
                                                                <asp:ListItem Value="1" Selected="True" Text="Yes"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="col-lg-12" style="margin-top:3px">
                                                <asp:Label runat="server" style="float:left;" Text="Select File:"></asp:Label>
                                                <div style="float:right;width:76.5%">
                                                    <asp:UpdatePanel ID="UpdatePanel20" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="FileUploadGSIS" CssClass="form-control form-control-sm" runat="server" />
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
                        <div class="modal-footer text-center" >
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
                    <div class="modal-content" >
                        <div class="modal-header bg-primary" >
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="AddEditPage"><asp:Label ID="LabelAddEdit" runat="server" Text="View Uploaded Information" ForeColor="White"></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            <div class="row" >
                                    <div class="col-lg-4">
                                        <div class="form-group row">
                                            <div class="col-lg-12"><asp:Label runat="server" Text="Last Name:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_last_name" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                            <div class="col-lg-12"><asp:Label runat="server" Text="First Name:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_first_name" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                            <div class="col-lg-12"><asp:Label runat="server" Text="Middle Name:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_middle_name" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                        </div>        
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group row">

                                            <div class="col-lg-12"><asp:Label runat="server" Text="Prefix Name:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_prefix_name" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                            <div class="col-lg-12"><asp:Label runat="server" Text="Appellation:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_appellation" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                            <div class="col-lg-12"><asp:Label runat="server" Text="Birth Date:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_birth_date" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                        </div>

                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group row">
                                            <div class="col-lg-12"><asp:Label runat="server" Text="GSIS CRN:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_gsis_crn" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                            <div class="col-lg-12"><asp:Label runat="server" Text="Montly Salary:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_monthly_salary" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                            <div class="col-lg-12"><asp:Label runat="server" Text="Effectivity Date:" ></asp:Label><asp:UpdatePanel runat="server"> <ContentTemplate><asp:TextBox ID="txtb_effectivity_date" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;"></asp:TextBox></ContentTemplate></asp:UpdatePanel></div>
                                        </div>

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
                <div class="col-3"><strong style="font-family:Arial;font-size:15px;color:white;"><%: Master.page_title %></strong></div>
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
                            <div class="row" style="margin-bottom:10px;">
                                <div class="col-lg-3">
                                    <div class="form-group row">
                                        <div class="col-lg-2">
                                            <asp:Label runat="server" Text="Show"  style="padding-top:10px;"></asp:Label>
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" ToolTip="Show entries per page">
                                                        <asp:ListItem Text="5" Value="5" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="15" Selected="True"  Value="15" />
                                                        <asp:ListItem Text="25" Value="25" />
                                                        <asp:ListItem Text="50" Value="50" />
                                                        <asp:ListItem Text="100" Value="100" />
                                                    </asp:DropDownList>
                                                </ContentTemplate>  
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9" style="font-size:11px;margin-top:10px;"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-1" >
                                    <asp:Label ID="Label3" runat="server" Text="Year: " ></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-1" >
                                    <asp:Label ID="Label11" runat="server" Text="Month: "></asp:Label>
                                </div>
                                <div class="col-lg-3" >
                                     <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged">
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
                                <div class="col-lg-2">
                                    <asp:UpdatePanel runat="server" ID="Update_btnAdd" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="btn-group pull-right " role="group" aria-label="Button group with nested dropdown" runat="server" id="btn_options">
                                                <div class="btn-group" role="group">
                                                <button id="btnGroupDrop1" type="button" class="btn btn-primary dropdown-toggle btn-sm btn-block" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                   <i class="fa fa-bars"></i> Other Actions
                                                </button>
                                                <div class="dropdown-menu" aria-labelledby="btnGroupDrop1">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="lnkbtn_upload_file" runat="server" class="dropdown-item btn-warning" Text="Upload File" OnClick="lnkbtn_upload_file_Click"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_btn_print" runat="server" class="dropdown-item btn-success" Text="Print" OnClick="lnk_btn_print_Click"></asp:LinkButton>
                                                            <%--<asp:LinkButton ID="lnkbtn_post_to_ledger" runat="server" class="dropdown-item btn-success" Text="Post to Ledger" OnClick="lnkbtn_post_to_ledger_Click"></asp:LinkButton>--%>
                                                            <%--<asp:LinkButton ID="lnkbtn_show_uploaded" runat="server" class="dropdown-item btn-info"  Text="Show Uploaded" OnClick="lnkbtn_show_uploaded_Click"></asp:LinkButton>--%>
                                                            <%--<asp:LinkButton ID="lnkbtn_show_rejected" runat="server" class="dropdown-item btn-danger"  Text="Show Rejected" OnClick="lnkbtn_show_rejected_Click"></asp:LinkButton>--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-3"></div>
                                <div class="col-lg-1">
                                    <asp:Label ID="Label7" runat="server" Text="View by:" ></asp:Label>
                                </div>
                                <div class="col-lg-8">
                                    <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_report_list" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" >
                                                    <asp:ListItem Value="01" Selected="True" Text="NOT EQUAL TO UPLOADED"></asp:ListItem>
                                                    <asp:ListItem Value="02" Text="UPLOADED LIST"></asp:ListItem>
                                                    <asp:ListItem Value="03" Text="ACTIVE DEDUCTION ON LEDGER"></asp:ListItem>
                                                    <asp:ListItem Value="04" Text="NO BP # BASED ON PAYROLL RECORD"></asp:ListItem>
                                                </asp:DropDownList>
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
                                            pagesize="15"
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
                                                    <ItemStyle Width="30%"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="DEDUCTION" SortExpression="deduc_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("deduc_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30%"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="UPLD AMT." SortExpression="uploaded_amt">
                                                    <ItemTemplate>
                                                       <%# Eval("uploaded_amt") %> &nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="ACT. AMT." SortExpression="deduc_amount1">
                                                    <ItemTemplate>
                                                        <%# Eval("deduc_amount1") %> &nbsp;
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
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_select.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("empl_id") %>' ToolTip="View Other Information" />
                                                        
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
                                    <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
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
        
        function ExecuteUpload_FileGSIS() 
        {
            if ($('#<%=FileUploadGSIS.ClientID%>').val() == "")
            {
                $('#<%=FileUploadGSIS.ClientID%>').css("border","0.5px solid red");
                $('#<%=lbl_msglogger.ClientID%>').css("color", "red");
                $('#<%=FileUploadGSIS.ClientID%>').text("required field !");
                 //openModal_Upload_Notif();
                return;
            }

            $(function() {  
                var fileUpload = $('#<%=FileUploadGSIS.ClientID%>').get(0);
                var files = fileUpload.files;  
                var test = new FormData();  
                for (var i = 0; i < files.length; i++)
                {  
                    test.append(files[i].name, files[i]);  
                }
                test.append("par_year", $("#<%= ddl_year.ClientID%>").val());
                test.append("par_month", $("#<%= ddl_month.ClientID%>").val());
                test.append("par_delete_existing",$("#<%= ddl_delete_existing.ClientID%>").val());
                test.append("par_user_id","<%:Session["ep_user_id"].ToString()%>");

                $.ajax({  
                    url: "FileUploader_GSIS.ashx",  
                    type: "POST",  
                    contentType: false,  
                    processData: false,  
                    data: test,  
                    success: function (result)
                    {
                        if (result.split('*')[0] == "Y")
                        {
                             $('#<%= lbl_msglogger.ClientID %>').text(result.split('*')[1]);
                             $('#<%=FileUploadGSIS.ClientID%>').css("border","0.5px solid green");
                             $('#<%=lbl_msglogger.ClientID%>').css("color", "green");

                             $('#icon_modal_upload_file_notif').removeClass();
                             $('#icon_modal_upload_file_notif').addClass("fa-5x fa fa-check-circle text-success");

                             $('#header_modal_upload_file_notif').text("Successfully Uploaded");


                        }
                        else
                        {
                            $('#<%= lbl_msglogger.ClientID %>').text(result.split('*')[1]);
                            $('#<%=FileUploadGSIS.ClientID%>').css("border","0.5px solid red");
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
                 window.location.href = "../cPayOthContributionsGSIS/cPayOthContributionsGSIS.aspx"
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
