<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayOthContributions_AddEdit.aspx.cs" Inherits="HRIS_ePayroll.View.cPayOthContributions_AddEdit.cPayOthContributions_AddEdit" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
    <!-- The Modal - Add Confirmation -->
            
        
        <asp:UpdatePanel ID="UpdatePanel4" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="modal_upload_file">
                   <div class="modal-dialog modal-dialog-centered modal-lg with-backgrounds" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)">
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="sd"><asp:Label ID="Label14" runat="server" Text="Upload" forecolor="White"></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel13" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-6">
                                                <div class="row form-group">
                                                    <div class="col-6" style="padding-right:0px;">
                                                         <asp:Label runat="server" Font-Bold="true" Text="Contribution Year:"></asp:Label>
                                                    </div>
                                                    <div class="col-6">
                                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtb_year_dslp" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;"></asp:TextBox>
                                                                <asp:Label ID="Label13" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row form-group">
                                                    <div class="col-6 offset-1">
                                                         <asp:Label runat="server" Font-Bold="true" Text="Contribution Month:"></asp:Label>
                                                    </div>
                                                    <div class="col-5">
                                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtb_month_dslp" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;"></asp:TextBox>
                                                                <asp:Label ID="Label12" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <hr style="margin-top:5px;margin-bottom:5px;" />
                                        <div class="row">
                                            <div class="col-7">
                                                <div class="row form-group">
                                                    <div class="col-5">
                                                         <asp:Label runat="server" Font-Bold="true" Text="Account Title:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtb_account_title_dspl2" runat="server" Width="97%" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;float:right;"></asp:TextBox>
                                                                <asp:Label ID="Label15" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5">
                                                <div class="row form-group">
                                                    <div class="col-3">
                                                         <asp:Label runat="server" Font-Bold="true" Text="Code:"></asp:Label>
                                                    </div>
                                                    <div class="col-9">
                                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtb_account_code_dspl2" runat="server" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;"></asp:TextBox>
                                                                <asp:Label ID="Label16" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <hr style="margin-top:5px;margin-bottom:5px;" />
                                        <div class="col-12" style="padding:0px;"> 
                                            <div class="row form-group">
                                                <div class="col-3">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Select File:"></asp:Label>
                                                </div>
                                                <div class="col-9">
                                                    <asp:UpdatePanel ID="UpdatePanel20" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="FileUpload1" onchange="showImage(this);" CssClass="form-control form-control-sm" Width="100%" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="UpdatePanel25" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lbl_msglogger" runat="server" CssClass="lbl_required"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div> 
                                        <hr style="margin-top:5px;margin-bottom:5px;" />
                                        <div class="col-12" style="padding:0px;">
                                            <div class="row form-group">
                                                <div class="col-3">
                                                     <asp:Label runat="server" Font-Bold="true" Text="Search Employee:"></asp:Label>
                                                </div>
                                                <div class="col-9 text-right">
                                                    <asp:UpdatePanel ID="UpdatePanel49" ChildrenAsTriggers="false" UpdateMode="Conditional"  runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_search_stg" onInput="search_for_stg(event);" runat="server" class="form-control" placeholder="Search.." Height="30px" 
                                                                Width="100%" OnTextChanged="txtb_search_TextChanged_stg" AutoPostBack="true"></asp:TextBox>
                                                            <script type="text/javascript">
                                                                function search_for_stg(key) {
                                                                        __doPostBack("<%= txtb_search_stg.ClientID %>", "");
                                                                }
                                                            </script>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12" style="padding:0px;">
                                            <asp:UpdatePanel ID="UpdatePanel24" runat="server" >
                                                <ContentTemplate>
                                                    <asp:GridView 
                                                            ID="gv_uploaded_list" 
                                                            runat="server" 
                                                            allowpaging="True" 
                                                            AllowSorting="True" 
                                                            AutoGenerateColumns="False" 
                                                            EnableSortingAndPagingCallbacks="True"
                                                            ForeColor="#333333" 
                                                            GridLines="Both" height="100%" 
                                                            onsorting="gv_dataListGrid_Sorting_stg"  
                                                            OnPageIndexChanging="gridviewbind_PageIndexChanging_stg"
                                                            PagerStyle-Width="3" 
                                                            PagerStyle-Wrap="false" 
                                                            pagesize="6"
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
                                                                <asp:TemplateField HeaderText=" ID" >
                                                                    <ItemTemplate>
                                                                        <%# Eval("empl_id") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EMPLOYEE NAME" >
                                                                    <ItemTemplate>
                                                                       <%# Eval("employee_name") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="50%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AMOUNT">
                                                                    <ItemTemplate>
                                                                       <%# Eval("monthly_amount") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="32%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ACTION">
                                                                    <ItemTemplate>
                                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                            <ContentTemplate>
                                                                                <%--<asp:ImageButton ID="imgbtn_editrow_stg" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" CommandArgument='<%# Eval("empl_id") %>' OnCommand="editRow_Command_stg"/>--%>
                                                                                <asp:ImageButton ID="lnkDeleteRow_stg" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" CommandArgument='<%# Eval("empl_id") %>' OnCommand="deleteRow_Command_stg" />
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
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <!-- Modal footer -->
                        </div>
                        <div class="modal-footer" style="padding-top:5px;padding-bottom:5px;">
                            
                            <asp:LinkButton ID="LinkButton4"  runat="server" data-dismiss="modal"  CssClass="btn btn-danger"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                            <%--<asp:Button ID="btn_save_contribution" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" OnClick="btn_save_contribution_Click" />--%>
                        </div>
                    </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel26" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="Update_stg" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered with-backgrounds" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)">
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                        <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="AddEditPageX"><asp:Label ID="Label17" runat="server" Text="UPDATE STG AMOUNT" forecolor="White"></asp:Label></h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" runat="server">
                            <div class="row">
                                <div class="col-12">
                                    <div class="row form-group">
                                        <div class="col-4">
                                                <asp:Label runat="server" Font-Bold="true" Text="Employee ID:"></asp:Label>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_stg_empl_id" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                    <asp:Label ID="lbl_stg_empl_id" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12" >
                                    <div class="row form-group">
                                        <div class="col-4" style="padding-right:0px;">
                                            <asp:Label runat="server" Font-Bold="true" Text="Employee's Name:"></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_stg_name" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;"></asp:TextBox>
                                                    <asp:Label ID="Label18" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row form-group">
                                        <div class="col-4">
                                                <asp:Label runat="server" Font-Bold="true" Text="Amount:"></asp:Label>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_stg_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                    <asp:Label ID="lbl_stg_amount" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="padding-bottom:5px;padding-top:5px;">
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
        <asp:UpdatePanel ID="UpdatePanel45" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="deleteRecSTG">
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content text-center">
                        <!-- Modal body -->
                                <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body">
                                            <i class="fa-5x fa fa-question text-danger"></i>
                                            <h2 >Delete this Record</h2>
                                            <h6><asp:Label ID="lbl_delete_confirm_stg" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            <!-- Modal footer -->
                            <div style="margin-bottom:50px">
                                <asp:LinkButton ID="btn_delete_stg" runat="server"  CssClass="btn btn-danger" OnCommand="btnDelete_Command_STG"> <i class="fa fa-check"></i> Yes, Delete it </asp:LinkButton>
                                <asp:LinkButton ID="LinkButton6"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>  
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel47" runat="server">
            <ContentTemplate>
              <!-- Modal Delete -->
                <div class="modal fade" id="otherNotification">
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content text-center">
                        <!-- Modal body -->
                                <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body">
                                            <i class="fa-5x fa fa-warning text-danger"></i>
                                            <h2 >WARNING</h2>
                                            <h6><asp:Label ID="Label19" runat="server" Text="Some data already exist, You still want to continue and Override the existing data?"></asp:Label></h6>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            <!-- Modal footer -->
                            <div style="margin-bottom:50px">
                                <asp:LinkButton ID="LinkButton5" runat="server"  CssClass="btn btn-danger" > <i class="fa fa-check"></i> Yes, Delete it </asp:LinkButton>
                                <asp:LinkButton ID="LinkButton7"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
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
                                <div class="col-9" >
                                    <div class="row form-group">
                                         <div class="col-4">
                                             <asp:Label runat="server" Font-Bold="true" Text="Employee's Name:"></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    
                                                    <asp:TextBox ID="txtb_empl_name" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;"></asp:TextBox>
                                                    <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                 </div> 
                                <div class="col-3">
                                    <div class="row form-group">
                                        <div class="col-3">
                                             <asp:Label runat="server" Font-Bold="true" Text="ID:"></asp:Label>
                                        </div>
                                        <div class="col-9">
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtb_empl_id" runat="server" CssClass="form-control form-control-sm text-center" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                    <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-12">
                                        <hr style="margin-top:5px;margin-bottom:5px;" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-9">
                                        <div class="row form-group">
                                            <div class="col-4">
                                                 <asp:Label runat="server" Font-Bold="true" Text="Account Title:"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_account_code" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_account_code_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="row form-group">
                                            <div class="col-3">
                                                 <asp:Label runat="server" Font-Bold="true" Text="Code:"></asp:Label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_account_sub_code" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" ></asp:DropDownList>
                                                        <asp:Label ID="Label5" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="row form-group">
                                            <div class="col-3">
                                                 <asp:Label runat="server" Font-Bold="true" Text="Account ID Ref:"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_id_ref_dspl" runat="server" CssClass="form-control form-control-sm text-right" Enabled ="false" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired5" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                 <asp:Label runat="server" Font-Bold="true" Text="Record Status :"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_record_status" runat="server" CssClass="form-control form-control-sm">
                                                            <asp:ListItem Value="" Text="--Select Here--"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Label2" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr style="margin-top:5px;margin-bottom:5px;" />
                                <div class="row">
                                    <div class="col-12">
                                        <h5><b>CONTRIBUTIONS AND LOANS AMOUNT</b></h5>
                                        <hr style="margin-top:5px;margin-bottom:5px;" />
                                    </div>
                                    <div class="col-6">
                                         <div class="row form-group">
                                            <div class="col-5">
                                                 <asp:Label runat="server" Font-Bold="true" Text="January"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_jan_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired6" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-5">
                                                 <asp:Label runat="server" Font-Bold="true" Text="February"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_feb_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired7" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-5">
                                                 <asp:Label runat="server" Font-Bold="true" Text="March"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_march_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired8" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-5">
                                                 <asp:Label runat="server" Font-Bold="true" Text="April"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_april_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired9" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-5">
                                                 <asp:Label runat="server" Font-Bold="true" Text="May"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_may_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired10" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-5">
                                                 <asp:Label runat="server" Font-Bold="true" Text="June"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_june_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired11" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                         <div class="row form-group">
                                            <div class="col-6">
                                                 <asp:Label runat="server" Font-Bold="true" Text="July"></asp:Label>
                                            </div>
                                             <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_jul_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired12" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-6">
                                                 <asp:Label runat="server" Font-Bold="true" Text="August"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_august_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired13" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-6">
                                                 <asp:Label runat="server" Font-Bold="true" Text="September"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sep_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired14" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-6">
                                                 <asp:Label runat="server" Font-Bold="true" Text="October"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" CssClass="text-right" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_oct_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired15" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-6">
                                                 <asp:Label runat="server" Font-Bold="true" Text="November"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_nov_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired16" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <div class="col-6">
                                                 <asp:Label runat="server" Font-Bold="true" Text="December"></asp:Label>
                                            </div>
                                            <div class="col-1">
                                                 <asp:Label runat="server" Font-Bold="true" Text=":"></asp:Label>
                                            </div>
                                            <div class="col-5">
                                                <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_dec_amount" runat="server" CssClass="form-control form-control-sm text-right" style="font-weight:bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired17" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hiddel_account_code" />
                                        <asp:HiddenField runat="server" ID="hiddel_empl_id" />
                                        <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                        <asp:Button ID="Button2" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
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
                <div class="col-3"><strong style="font-family:Arial;font-size:16px;color:white;">Other Contributions Add/Edit</strong></div>
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
                                <div class="col-3">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                                <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="25%" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="5"   Value="5" />
                                                    <asp:ListItem Text="10"  Value="10" Selected="True" />
                                                    <asp:ListItem Text="15"  Value="15" />
                                                    <asp:ListItem Text="25"  Value="25" />
                                                    <asp:ListItem Text="50"  Value="50" />
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
                                                    <%--<asp:Label ID="Label3" runat="server" Text="Payroll Year : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                    
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" style="width:95px"  Enabled="false"></asp:DropDownList>
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
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-info btn-sm" Font-Bold="true" OnClick="btn_back_Click">
                                                <i class="fa fa-arrow-left"></i>
                                                Back to Payroll Registry
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-8">
                                    <div class="form-group row">
                                        <div class="col-12">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="Label20" runat="server" Text="Employee's Name: "></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    <asp:TextBox ID="txtb_employee_name" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled ="false" style="font-weight:bold;letter-spacing:3px;width:500px"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" style="width:450px"></asp:DropDownList>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-1 text-right">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            
                                            <% if (ViewState["page_allow_add"].ToString() == "1")
                                                {  %>
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn"  Text="Add" OnClick="btnAdd_Click" />
                                            <% }
                                                %>     
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
                                                <asp:TemplateField HeaderText="ACCOUNT CODE" SortExpression="account_code_combi">
                                                    <ItemTemplate>
                                                        <%# Eval("account_code_combi") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACCOUNT TITLE" SortExpression="account_title">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("account_title") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="ID REF NO" SortExpression="account_id_nbr_ref">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("account_id_nbr_ref") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <%--<asp:TemplateField HeaderText="AMOUNT">
                                                    <ItemTemplate>
                                                         <% if (ddl_month.SelectedValue.ToString() == "01"){%> <%# Eval("monthly_amount_01")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "02"){%> <%# Eval("monthly_amount_02")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "03"){%> <%# Eval("monthly_amount_03")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "04"){%> <%# Eval("monthly_amount_04")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "05"){%> <%# Eval("monthly_amount_05")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "06"){%> <%# Eval("monthly_amount_06")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "07"){%> <%# Eval("monthly_amount_07")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "08"){%> <%# Eval("monthly_amount_08")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "09"){%> <%# Eval("monthly_amount_09")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "10"){%> <%# Eval("monthly_amount_10")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "11"){%> <%# Eval("monthly_amount_11")+"&nbsp;&nbsp;" %><%}%>
                                                         <% if (ddl_month.SelectedValue.ToString() == "12"){%> <%# Eval("monthly_amount_12")+"&nbsp;&nbsp;" %><%}%>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("empl_id") %>'/>
                                                        
                                                                <%   }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("empl_id")+","+Eval("account_code")+","+Eval("account_sub_code")+","+Eval("payroll_year")%>'/>
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
            show_date();
        }

        function openModalSTG()
        {
                $('#Update_stg').modal({
                    keyboard: false,
                    backdrop: "static"
                });
        }
    function closeModalSTG()
        {
            $('#Update_stg').modal("hide");
            $('#AddEditConfirm').modal({
                keyboard: false,
                backdrop: "static"
        });
         setTimeout(function () {
                $('#AddEditConfirm').modal("hide");
                $('.modal-backdrop.show').remove();
                
            }, 800);
        }
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

       function show_date()
        {
           <%-- $('#<%= txtb_date_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_date_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });--%>
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

        function openModalOtherNotification() {
            $('#otherNotification').modal({
                keyboard: false,
                backdrop:"static"
            });
           }

           function openModalDeleteSTG() {
            $('#deleteRecSTG').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>
     
    <script type="text/javascript">
        function closeModalDelete() {
            $('#deleteRec').modal('hide');
        };
         function closeModalDeleteSTG() {
            $('#deleteRecSTG').modal('hide');
        };
    </script>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
</asp:Content>
