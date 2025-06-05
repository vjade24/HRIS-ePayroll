<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cApplTransmittal.aspx.cs" Inherits="HRIS_ePayroll.View.cApplTransmittal.cApplTransmittal" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
    

    
    <!-- The Modal - Select Report -->
    <asp:UpdatePanel ID="edit_delete_notify" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="modal fade" id="SelectReport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="overflow-y:auto !important">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content" style="background-image:linear-gradient(white, lightblue)">
                    <div class="modal-header bg-success" >
                        <h5 class="modal-title" ><asp:Label ID="notify_header" runat="server" Text="Transmittal Approval" forecolor="White"></asp:Label></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                            <div class="modal-body with-background">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <ul class="nav nav-tabs" id="myTab" role="tablist">
                                          <li class="nav-item">
                                            <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Transmittal Description</a>
                                          </li>
                                          <li class="nav-item">
                                            <a class="nav-link" id="profile-tab" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false">Transmittal History</a>
                                          </li>
                                          
                                        </ul>
                                        <div class="tab-content" id="myTabContent">
                                          <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                                              
                                                <div class="row pr-3 pl-3">
                                                    <div class="col-md-12">
                                                        <hr style="padding:0px;margin: 5px;"/>
                                                        <h6 class="m-t-none m-b text-success">TRANSMITTAL DESCRIPTION</h6>
                                                    </div>
                                                    <div class="col-3" >
                                                        <asp:Label runat="server" Text="Transmittal Nbr.:" Font-Bold="true"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtb_trans_nbr" CssClass="form-control form-control-sm text-center" Enabled="false" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-3" >
                                                        <asp:Label runat="server" Text="Transmittal Date:" Font-Bold="true"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtb_trans_date" CssClass="form-control form-control-sm text-center" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2" >
                                                        <asp:Label runat="server" Text="Count.:" Font-Bold="true"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtb_no_employee" CssClass="form-control form-control-sm text-center" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2" >
                                                        <asp:Label runat="server" Text="User ID:" Font-Bold="true"></asp:Label>
                                                        <asp:TextBox runat="server" ID="user_id" CssClass="form-control form-control-sm text-center" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-12" >
                                                        <asp:Label runat="server" Text="Transmittal Description:" Font-Bold="true"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtb_trans_descr" TextMode="MultiLine" Rows="2"  CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-12" >
                                                        <asp:Label runat="server" Text="Disapproved Remarks:" Visible="false" ID="lbl_remarks" Font-Bold="true"></asp:Label>
                                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="2" ID="txtb_trans_remarks" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                                        <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" ></asp:Label>
                                                    </div>
                                                </div>
                                                    
                                          </div>
                                          <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                                              <div class="row pr-3 pl-3">
                                                  <div class="col-md-12">   
                                                        <hr style="padding:0px;margin: 5px;"/>
                                                        <h6 class="m-t-none m-b text-success">TRANSMITTAL HISTORY</h6>
                                                  </div>
                                                  <div class="col-4" >
                                                    <asp:Label runat="server" Text="Submitted Date/Time:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_submitted_dttm" CssClass="form-control form-control-sm text-center" Enabled="false" ></asp:TextBox>
                                                  </div>
                                                  <div class="col-8" >
                                                    <asp:Label runat="server" Text="Submitted Name:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_submitted_employee_name" CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
                                                  </div>
                                                  <div class="col-4" >
                                                    <asp:Label runat="server" Text="Received HR Date/Time:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_hr_rcvd_dttm" CssClass="form-control form-control-sm text-center" Enabled="false" ></asp:TextBox>
                                                 </div>
                                                  <div class="col-8" >
                                                    <asp:Label runat="server" Text="Received HR Name:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_hr_rcvd_employee_name" CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
                                                  </div>
                                                  
                                                  <div class="col-4" >
                                                    <asp:Label runat="server" Text="Payroll Received Date/Time:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_rcvd_dttm" CssClass="form-control form-control-sm text-center" Enabled="false" ></asp:TextBox>
                                                 </div>
                                                  <div class="col-8" >
                                                    <asp:Label runat="server" Text="Payroll Received Name:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_rcvd_employee_name" CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
                                                  </div>

                                                  <div class="col-4" >
                                                    <asp:Label runat="server" Text="Approved Date/Time:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_approved_dttm" CssClass="form-control form-control-sm text-center" Enabled="false" ></asp:TextBox>
                                                 </div>
                                                  <div class="col-8" >
                                                    <asp:Label runat="server" Text="Approved Name:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_approved_employee_name" CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
                                                  </div>

                                                  <div class="col-4" >
                                                    <asp:Label runat="server" Text="Disapproved Date/Time:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_disapproved_dttm" CssClass="form-control form-control-sm text-center" Enabled="false" ></asp:TextBox>
                                                 </div>
                                                  <div class="col-8" >
                                                    <asp:Label runat="server" Text="Disapproved Name:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_disapproved_employee_name" CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
                                                      <asp:Label ID="lbl_remarks_dis" runat="server" CssClass="lbl_required" ></asp:Label>
                                                  </div>

                                                  <div class="col-4" >
                                                    <asp:Label runat="server" Text="Payroll Create Date/Time:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_created_dttm" CssClass="form-control form-control-sm text-center" Enabled="false" ></asp:TextBox>
                                                 </div>
                                                  <div class="col-8" >
                                                    <asp:Label runat="server" Text="Payroll Created Name:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_payroll_created_employee_name" CssClass="form-control form-control-sm" Enabled="false" ></asp:TextBox>
                                                  </div>
                                              </div>
                                          </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row">
                                    <div class="col-12">
                                        <hr style="margin-top:5px !important;margin-bottom:5px !important;" />
                                    </div>
                                    <div class="col-12">
                                          <a class="btn btn-primary btn-sm mb-2" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample" style="display:none">
                                            <i class="fa fa-eye"></i> View Details | Employee List
                                          </a>
                                        <div class="collapsed collapse show" id="collapseExample">
                                            <asp:UpdatePanel ID="up_datagrid_history" UpdateMode="Conditional" runat="server" >
                                            <ContentTemplate>
                                                <asp:GridView 
                                                        ID="gv_datagrid_history" 
                                                        runat="server" 
                                                        allowpaging="false" 
                                                        AllowSorting="false" 
                                                        AutoGenerateColumns="False" 
                                                        EnableSortingAndPagingCallbacks="True"
                                                        ForeColor="#333333" 
                                                        GridLines="Both" height="100%" 
                                                        PagerStyle-Width="3" 
                                                        PagerStyle-Wrap="false" 
                                                        pagesize="5"
                                                        Width="100%" 
                                                        Font-Names="Century gothic"
                                                        Font-Size="Small" 
                                                        RowStyle-Width="5%" 
                                                        AlternatingRowStyle-Width="10%"
                                                        CellPadding="2"
                                                        ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="NO DATA FOUND"
                                                        EmptyDataRowStyle-ForeColor="Red"
                                                        EmptyDataRowStyle-CssClass="no-data-found">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="STATUS" SortExpression="included" >
                                                                <ItemTemplate>
                                                                    <span class="<%# Eval("included").ToString() == "Y" ? "badge badge-success" : "badge badge-danger" %>">
                                                                        <i class="<%# Eval("included").ToString() == "Y" ? "fa fa-check" : "fa fa-times" %> "></i>
                                                                        <%# Eval("included").ToString() == "Y" ? "Included" : "Excluded" %>
                                                                    </span>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="CENTER" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID" SortExpression="empl_id" >
                                                                <ItemTemplate>
                                                                    <%# Eval("empl_id") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="CENTER" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("employee_name") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="45%" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="LEFT" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="POSITION" SortExpression="position_title1">
                                                                <ItemTemplate>
                                                                    &nbsp;<%# Eval("position_title1") %>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40%" />
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
                                        </asp:UpdatePanel>
                                        </div>


                                    </div>
                                    <div class="col-12">
                                        <hr style="margin-top:5px !important;margin-bottom:5px !important;" />
                                    </div>
                                </div>

                            </div>
                       <asp:UpdatePanel runat="server">
                           <ContentTemplate>
                               <div class=" text-center " >
                                    <asp:UpdatePanel runat="server" >
                                        <ContentTemplate>
                                            <div class="col-lg-12 text-center mb-4" >
                                                <button class="btn btn-success" id="id_payroll" onclick="print('MONITORING')"><i class="fa fa-print"></i> Preview </button>
                                                <asp:LinkButton ID="btn_extract_report" runat="server" Onclick="btn_extract_report_Click" CssClass="btn btn-warning pt-2 text-white" Width="220" Height="40"><i class="fa fa-file-excel-o "></i> Extract Monitoring Sheet</asp:LinkButton>
                                                <asp:LinkButton ID="btn_rcvd_payroll" runat="server" CssClass="btn btn-primary pt-2" OnClick="btn_rcvd_payroll_Click" Width="200" Height="40"><i class="fa fa-qrcode"></i> Receive to Payroll</asp:LinkButton>
                                                <asp:LinkButton ID="btn_approve" runat="server" CssClass="btn btn-success pt-2" OnClick="btn_approve_Click" Width="200" Height="40"><i class="fa fa-thumbs-up"></i> Approve</asp:LinkButton>
                                                <asp:LinkButton ID="btn_disapprove" runat="server"  CssClass="btn btn-danger pt-2" OnClick="btn_disapprove_Click" Width="200" Height="40" ><i class="fa fa-thumbs-down"></i> Disapprove</asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                           </ContentTemplate>
                       </asp:UpdatePanel> 
                    
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

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
                                            <%--<img src="../../ResourceImages/loadingwithlogo.gif" style="width: 100%;" />--%>
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
    
    <!-- The Modal - Add Confirmation -->
    <div class="modal fade" id="AddEditConfirm">
        <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content text-center">
            <!-- Modal body -->
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
 
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <i  class="fa-5x fa fa-check-circle text-success"></i>
                        <h2 id="lbl_message_descr">Successfully</h2>
                        <h6><asp:Label ID="SaveAddEdit" runat="server" Text="Save"></asp:Label></h6>

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
                                <div class="col-4">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                                <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="25%" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="5" Value="5" />
                                                    <asp:ListItem Text="10" Selected="True" Value="10" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                            <asp:Label runat="server" Text="Entries"></asp:Label>
                                            |
                                            <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9" style="font-size:10px !important"></asp:Label>
                                        </ContentTemplate>  
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-md-6">
                                            <label >Transmittal Year: </label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group row">
                                        <div class="col-md-6">
                                            <label >Transmittal Month: </label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true" >
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
                                <div class="col-4" style="display:none"></div>
                                
                                <div class="col-8" style="display:none">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label >View Type: </label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_view_type" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true" >
                                                        <asp:ListItem Value="0" Text="Whole Month"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="1st Quincena"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="2nd Quincena"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4"></div>
                                <div class="col-8">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label >Approval Status: </label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_approval_status" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true" >
                                                        <asp:ListItem Value="V" Text="Not Yet Receive on HR and Received by HR"></asp:ListItem>
                                                        <asp:ListItem Value="N" Text="New"></asp:ListItem>
                                                        <asp:ListItem Value="S" Text="Submitted"></asp:ListItem>
                                                        <asp:ListItem Value="A" Text="Approved"></asp:ListItem>
                                                        <asp:ListItem Value="D" Text="Disapproved"></asp:ListItem>
                                                        <asp:ListItem Value="T" Text="For Return"></asp:ListItem>
                                                    </asp:DropDownList>
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
                                                <asp:TemplateField HeaderText="TRANS. NBR." SortExpression="transmittal_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("transmittal_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="TRANSMITTAL DESCRPTION" SortExpression="transmittal_descr">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%#  Eval("transmittal_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="TYPE" SortExpression="transmittal_type_descr">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%#  Eval("transmittal_type_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="TYPE" SortExpression="view_type_descr">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%#  Eval("view_type_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="STATUS" SortExpression="approval_status_descr">
                                                    <ItemTemplate>
                                                        &nbsp;
                                                        <span class="<%# Eval("approval_status").ToString()  != "D" && Eval("approval_status").ToString()  != "T" ? "badge badge-success" : "badge badge-danger" %>">
                                                            <i class="<%# Eval("approval_status").ToString() != "D" && Eval("approval_status").ToString()  != "T" ? "fa fa-check" : "fa fa-times" %> "></i>
                                                            <%#  Eval("approval_status_descr") %>
                                                        </span>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CNT." SortExpression="no_of_employees">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%#  Eval("no_of_employees") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                               <%-- <% if (ViewState["page_allow_print"].ToString() == "1")
                                                                    {
                                                                %>--%>
                                                                     <asp:ImageButton ID="imgbtn_print" 
                                                                         CssClass="btn btn-warning action" 
                                                                         EnableTheming="true" 
                                                                         runat="server" 
                                                                         ImageUrl="~/ResourceImages/add.png"  
                                                                         style="padding-left: 0px !important;padding-right: 3px !important;" 
                                                                         OnCommand="imgbtn_print_Command1" 
                                                                         CommandArgument='<%# Eval("transmittal_nbr")%> ' 
                                                                         tooltip="Action Transmittal"/>
                                                               <%-- <%  }
                                                                %>--%>

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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="modal fade" id="modal_print_preview" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true">
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
        function openSelectReport() {
            $('#SelectReport').modal({
                keyboard: false,
                backdrop:"static"
            });
         };
          function openLoading() {
           
            $('#Loading').modal({
                keyboard: false,
            backdrop:"static"
            });
            setTimeout(function () {
                $('#Loading').modal("hide");
                $('.modal-backdrop.show').remove();
            }, 5000);
            setTimeout(function () {
                openMessage();
            }, 5000);
             
         };
    </script> 
    
    <script type="text/javascript">
        function closeModal() {
             //$('#SelectReport').modal("hide");
             $('#AddEditConfirm').modal({
                 keyboard: false,
                backdrop:"static"
            });
            setTimeout(function () {
                $('#AddEditConfirm').modal("hide");
                //$('.modal-backdrop.show').remove();
                
            }, 2000);
        };
    </script>
    <script type="text/javascript">
        function print(report_type)
        {
            var sp                  = ""
            var transmittal_nbr     = $('#<%= txtb_trans_nbr.ClientID %>').val()
            var user_id             = $('#<%= user_id.ClientID %>').val()
            ReportPath  = "~/Reports/cryPayrollMonitoring/cryPayrollMonitoring.rpt";
            sp          = ReportPath+","+"sp_extract_monitoring_rep,par_transmittal_nbr," + transmittal_nbr + ",par_user_id," + user_id;
            previewReport(sp,report_type)
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
            if (report_type == "PAYROLL")
            {
                embed_link = sp;
            }
            else
            {
                embed_link = "../../printView/CrystalViewer.aspx?Params=" + ""
                    + "&ReportName=" + ReportName
                    + "&SaveName="   + SaveName
                    + "&ReportType=" + ReportType
                    + "&ReportPath=" + ReportPath
                    + "&id=" + sp // + "," + parameters
            }

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
                   $(this).addClass('highlight');
           }, function () {
                   $(this).removeClass('highlight');
           });
        }

        $(document).ready(function ()
        {
           $('#<%= gv_dataListGrid.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight');
           }, function () {
                   $(this).removeClass('highlight');
           });
        });
    </script>
    
</asp:Content>
