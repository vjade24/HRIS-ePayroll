<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cTakeHomePay.aspx.cs" Inherits="HRIS_ePayroll.View.cTakeHomePay.cTakeHomePay" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
        <style type="text/css">
        
        .highlight
            {
                background-color: #507cd1 !important;
                color:white !important;
                cursor: pointer;
            }
        
        
        /*@media screen and (max-width: 900px) {
          tbody {
            background-color:red !important;
            font-size:1px !important;
          }
        }*/

    </style>
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

    
    <!-- The Modal - Select Report -->
    <asp:UpdatePanel ID="edit_delete_notify" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="modal fade" id="SelectReport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #28a745;color: white;">
                        <h5 class="modal-title" ><asp:Label ID="notify_header" runat="server" Text="Report Option"></asp:Label></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                            <div class="modal-body with-background">
                                <div class="row">
                                    <div class="col-9" >
                                        <asp:Label runat="server" Text="Employee Name:"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtb_employee_name" CssClass="form-control form-control-sm" Enabled="false" Font-Bold="true"></asp:TextBox>
                                    </div>
                                    <div class="col-3" >
                                        <asp:Label runat="server" Text="ID No.:"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtb_empl_id" CssClass="form-control form-control-sm" Enabled="false" Font-Bold="true"></asp:TextBox>
                                    </div>
                                    <div class="col-12">
                                        <hr />
                                    </div>
                                    <div class="col-6" >
                                        <asp:Label runat="server" Text="Payroll Year:"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtb_payroll_year" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-6" >
                                        <asp:Label runat="server" Text="Payroll Month:"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtb_payroll_month" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-12" >
                                        <asp:Label runat="server" Text="Select Report:"></asp:Label>
                                        <asp:DropDownList ID="ddl_select_report" CssClass="form-control form-control-sm" runat="server" Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="ddl_select_report_SelectedIndexChanged">
                                            <asp:ListItem Value="01" Text="Net Take Home Pay"></asp:ListItem>
                                            <asp:ListItem Value="02" Text="Pay Slip"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-12">
                                        <hr />
                                    </div>
                                    <div class="col-12" >
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Label runat="server" Text="Select Purpose:" ID="lbl_purpose"></asp:Label> 
                                                <asp:DropDownList ID="ddl_purpose" CssClass="form-control form-control-sm" runat="server" Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="ddl_purpose_SelectedIndexChanged" >
                                                    <asp:ListItem Text=""></asp:ListItem>
                                                    <asp:ListItem Value="CCMPC Loan" Text="CCMPC Loan"></asp:ListItem>
                                                    <asp:ListItem Value="NICO Loan" Text="NICO Loan"></asp:ListItem>
                                                    <asp:ListItem Value="BDO Loan" Text="BDO Loan"></asp:ListItem>
                                                    <asp:ListItem Value="LBP eSL" Text="LBP eSL"></asp:ListItem>
                                                    <asp:ListItem Value="Tagum Coop." Text="Tagum Coop."></asp:ListItem>
                                                    <asp:ListItem Value="" Text="Other Loan"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-12">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Label  runat="server" Text="Purpose Override:" ID="lbl_purpose_override" ></asp:Label> 
                                                <asp:TextBox runat="server" ID="txtb_purpose_override" CssClass="form-control form-control-sm"  ></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-8">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Label  runat="server" Text="OR Number:" ID="lbl_or_nbr" ></asp:Label>
                                                <asp:TextBox runat="server" ID="txtb_or_nbr" CssClass="form-control form-control-sm" Width="100%" MaxLength="25" ></asp:TextBox>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-4">
                                        <asp:UpdatePanel runat="server" ID="Update_or_date" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Label  runat="server" Text="OR Date:" ID="lbl_or_date" ></asp:Label>
                                                <asp:TextBox runat="server" ID="txtb_or_date" CssClass="form-control form-control-sm my-date text-center" MaxLength="10" Width="100%" ></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                        
                                    <div class="col-12" >
                                        <asp:Label runat="server" ID="lbl_payroll_template" Text="Payroll Template:" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddl_payroll_template" CssClass="form-control form-control-sm" runat="server" Width="100%"  AutoPostBack="true" Visible="false"></asp:DropDownList>
                                        
                                    </div>
                                    <hr />
                                    <div class="col-9" style="text-align:center !important">
                                        <asp:Label runat="server" CssClass="badge badge-danger" ForeColor="White" ID="LblRequired1"></asp:Label>
                                    </div>
                                    <%--<div class="col-12 text-left" style="padding-top:5px !important" runat="server" id="col_message">
                                        <div class="alert alert-danger alert-dismissible fade show small" role="alert" style="padding:5px;margin-bottom:0px;margin-left:0px;opacity:0.8;font-size:12px !important">
                                            <strong> Message:  </strong> -- Cannot Print Report <b> No Data Found</b>
                                            <asp:Label runat="server" ID="lbl_field_descr3" CssClass="bold alert-link" style="text-decoration:underline !important" ></asp:Label>
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="padding: 0px; margin-right: 5px;">
                                            <span aria-hidden="true" class="small">&times;</span>
                                            </button>
                                        </div>
                                    </div>--%>
                                    <div class="col-3" style="margin-top:5px">
                                        <asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkPrint_Click"  > <i class="fa fa-print"></i> Print </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <!-- The Modal - Generating Report -->
            <div class="modal fade" id="Loading">
                <div class="modal-dialog modal-dialog-centered modal-lg">
                    <div class="modal-content text-center">
                        <!-- Modal body -->
                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
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
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-md-7">
                                            <label >Payroll Year : </label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true"  onchange="openLoading()"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group row">
                                        <div class="col-md-4">
                                            <label >Payroll Month : </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true" onchange="openLoading()">
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
                                <div class="col-4"></div>
                                <div class="col-3" >
                                    <div class="form-group row">
                                        
                                            <div class="col-md-7" style="padding-right:0px !important">
                                                <label >Last Name Starts W/: </label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_start_with" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_start_with_SelectedIndexChanged"  onchange="openLoading()">
                                                            <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                            <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                            <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                            <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                            <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                            <asp:ListItem Text="F" Value="F"></asp:ListItem>
                                                            <asp:ListItem Text="G" Value="G"></asp:ListItem>
                                                            <asp:ListItem Text="H" Value="H"></asp:ListItem>
                                                            <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                                            <asp:ListItem Text="J" Value="J"></asp:ListItem>
                                                            <asp:ListItem Text="K" Value="K"></asp:ListItem>
                                                            <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                                            <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                                            <asp:ListItem Text="O" Value="O"></asp:ListItem>
                                                            <asp:ListItem Text="P" Value="P"></asp:ListItem>
                                                            <asp:ListItem Text="Q" Value="Q"></asp:ListItem>
                                                            <asp:ListItem Text="R" Value="R"></asp:ListItem>
                                                            <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                                            <asp:ListItem Text="T" Value="T"></asp:ListItem>
                                                            <asp:ListItem Text="U" Value="U"></asp:ListItem>
                                                            <asp:ListItem Text="V" Value="V"></asp:ListItem>
                                                            <asp:ListItem Text="W" Value="W"></asp:ListItem>
                                                            <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                </div>
                                <div class="col-md-5" >
                                    <div class="form-group row">
                                        <div class="col-4">
                                            <label >Employment Type: </label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_empl_type" runat="server"  CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" onchange="openLoading()"></asp:DropDownList>
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
                                                <asp:TemplateField HeaderText="ID NO" SortExpression="account_code">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%#  Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <% if (ViewState["page_allow_print"].ToString() == "1")
                                                                    {
                                                                %>
                                                                     <asp:ImageButton ID="imgbtn_print" 
                                                                         CssClass="btn btn-success action" 
                                                                         EnableTheming="true" 
                                                                         runat="server" 
                                                                         ImageUrl="~/ResourceImages/print1.png" 
                                                                         style="padding-left: 0px !important;" 
                                                                         OnCommand="imgbtn_print_Command1" 
                                                                         CommandArgument='<%# Eval("empl_id")%> ' 
                                                                         tooltip="Print"/>
                                                                <%  }
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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_start_with" />
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
        function openSelectReport() {
            $('#SelectReport').modal({
                keyboard: false,
                backdrop:"static"
            });
            show_date();
         };
        function show_date()
        {
            $('#<%= txtb_or_date.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
        }
         function openLoading()
         {
             $('#Loading').modal({ keyboard: false, backdrop: "static" });

            //$('#Loading').modal({
            //    keyboard: false,
            //    backdrop: "static"
            //});
         }
         function closeLoading()
         {
             $('#Loading').modal('hide');
             $('div.modal-backdrop').remove();
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
