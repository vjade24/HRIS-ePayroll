<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPHICSetup.aspx.cs" Inherits="HRIS_ePayroll.View.cPHICSetup.cPHICSetup" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

        <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
         


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
                                    <i class="fa-5x fa fa-question text-danger" ID="delete_icon" runat="server" ></i>
                                    <h2 runat="server" id="delete_header">Delete this Record</h2>
                                    <h6><asp:Label ID="deleteRec1" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!-- Modal footer -->
                        <div style="margin-bottom:50px">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkBtnYes" runat="server"  CssClass="btn btn-danger" OnCommand="btnDelete_Command"> <i class="fa fa-check"></i> Yes, Delete it </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton3"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>

        
        <asp:UpdatePanel ID="UpdatePanel1" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="generate">
                    <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content text-center">
                    <!-- Modal body -->
                        <div class="modal-header bg-success text-white">
                                <h5 class="modal-title" ><asp:Label ID="Label6" runat="server" Text="Generate PHIC Contribution"></asp:Label></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background">
                            <%--<i class="fa-5x fa fa-question text-success" runat="server" ></i>
                            <h2 runat="server" id="H1">Generate Contribution</h2>
                            <h6><asp:Label ID="Label1" runat="server" Text="Are you sure you want to Generate PHIC contribution for All Employees with that Filters ?"></asp:Label></h6>--%>
                            
                            <div class="row text-left">
                                
                                <div class="col-md-4 pb-1">
                                    <asp:Label runat="server" Text="Effective Date:" CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-md-4 pb-1">
                                    <asp:UpdatePanel ID="UpdateEffective_generate" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_effective_date_generate" CssClass="form-control form-control-sm my-date text-center" ></asp:TextBox>
                                            <asp:Label ID="LblRequired6" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-4 pb-1"></div>
                                <div class="col-4 pb-1 pr-0">
                                    <asp:Label runat="server" Text="Employment Type:"  CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-8 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_empl_type"></asp:DropDownList>
                                            <asp:Label ID="LblRequired7" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-4 pr-0">
                                    <asp:Label runat="server" Text="Fix Rate Amount:"  CssClass="font-weight-bold"></asp:Label>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_fix_rate" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                            <asp:Label ID="LblRequired8" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-4 pb-1"></div>
                            </div>
                        </div>
                        <div class="modal-footer" >
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton4"  runat="server" data-dismiss="modal"  CssClass="btn btn-danger"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtn_generate" runat="server"  CssClass="btn btn-success" OnClick="lnkbtn_generate_Click" > <i class="fa fa-check"></i> Generate </asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                       
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

        <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-md" role="document">
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
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: -10px;">
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdateEffective" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Effective Date:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_effective_date" CssClass="form-control form-control-sm text-center my-date"  Width="100%" ></asp:TextBox>
                                                    <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                         <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Contribution Rate (%):" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_contribution_rate" CssClass="form-control form-control-sm text-right"  Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired4" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Flooring Amount:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_salary_flooring_amt" CssClass="form-control form-control-sm text-right"  Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Ceiling Amount:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_salary_ceiling_amt" CssClass="form-control form-control-sm text-right"  Width="100%"></asp:TextBox>
                                                    <asp:Label ID="LblRequired3" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Text="Remarks:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_philhealth_remarks" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3"  MaxLength="255"> </asp:TextBox>
                                                    <asp:Label ID="LblRequired5" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                            <div class="modal-footer" style="padding-top:5px;padding-bottom:5px;" >
                            
                                <asp:UpdatePanel runat="server" >
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Close" CssClass="btn btn-danger cancel-icon icn" ></asp:LinkButton>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
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
                                <div class="col-9 text-right">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            
                                            <% if (ViewState["page_allow_add"].ToString() == "1")
                                                {  %>
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm add-icon icn"  Text="Add" AutoPostBack="true" OnClick="btnAdd_Click" />
                                            <% }
                                             %>
                                                                     
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
                                                <asp:TemplateField HeaderText="EFF. DATE" SortExpression="effective_date">
                                                    <ItemTemplate>
                                                        <%# Eval("effective_date") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="FLOORING AMT." SortExpression="salary_flooring_amt">
                                                    <ItemTemplate>
                                                        <%# Eval("salary_flooring_amt") %> &nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="CEILING AMT." SortExpression="salary_ceiling_amt">
                                                    <ItemTemplate>
                                                        <%# Eval("salary_ceiling_amt") %> &nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="PHIC %" SortExpression="contribution_rate">
                                                    <ItemTemplate>
                                                        <%# Eval("contribution_rate") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REMARKS" SortExpression="philhealth_remarks">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("philhealth_remarks") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="42%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                    
                                                                    
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                        <asp:ImageButton 
                                                                        ID="imgbtn_editrow1" 
                                                                        runat="server" 
                                                                        EnableTheming="true"  
                                                                        CssClass="btn btn-primary action" 
                                                                        ImageUrl="~/ResourceImages/final_edit.png" 
                                                                        OnCommand="editRow_Command" 
                                                                        CommandArgument='<%# Eval("effective_date")%> ' 
                                                                        tooltip="View/Edit Details"
                                                                        />
                                                        
                                                                <%   }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton 
                                                                        ID="lnkDeleteRow" 
                                                                        CssClass="btn btn-danger action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/final_delete.png"
                                                                        style="padding-left: 6px !important;padding-right: 6px !important;" 
                                                                        OnCommand="deleteRow_Command" 
                                                                        CommandArgument='<%# Eval("effective_date")%> '   
                                                                        tooltip="Delete" 
                                                                        />
                                                                <% }
                                                                %>

                                                                    <asp:ImageButton 
                                                                        ID="img_generate" 
                                                                        CssClass="btn btn-success action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/post.png"
                                                                        style="padding-left: 0px !important;padding-right: 3px !important;" 
                                                                        OnCommand="generate_Command" 
                                                                        CommandArgument='<%# Eval("effective_date")%> '   
                                                                        tooltip="Generate" 
                                                                        />
                                                               
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
            show_date()
          };
        function show_date()
        {
            $('#<%= txtb_effective_date.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
          
           if ($('#<%= txtb_effective_date.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_effective_date.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }
            
            $('#<%= txtb_effective_date_generate.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
          
           if ($('#<%= txtb_effective_date_generate.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_effective_date_generate.ClientID %>').closest("div");
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
        function openLoading() {

            $('#Loading').modal({
                keyboard: false,
                backdrop: "static"
            });
        }
        function openGenerate() {

            $('#generate').modal({
                keyboard: false,
                backdrop: "static"
            });
            show_date()
        }

        function openLoadingAndClose() {
             $('#generate').modal("hide");
             $('#Loading').modal({
                 keyboard: false,
                backdrop:"static"
            });
            setTimeout(function () {
                $('#Loading').modal("hide");
                $('.modal-backdrop.show').remove();
                $('#AddEditConfirm').modal({keyboard: false,backdrop:"static"});
            }, 5000);
        };
    </script>
</asp:Content>
