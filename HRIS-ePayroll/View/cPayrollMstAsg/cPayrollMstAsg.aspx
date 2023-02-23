<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayrollMstAsg.aspx.cs" Inherits="HRIS_ePayroll.View.cPayrollMstAsg.cPayrollMstAsg" %>
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

       <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-lg" role="document">
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
                        <div class="modal-body with-background" runat="server" >
                            <div class="row" runat="server" style="margin-top:-10px">
                                <div class="col-9" >
                                    <div class="row form-group">
                                        <div class="col-4">
                                            <Label ID="Label1" ><strong>Employee's Name:</strong></Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList  ID="ddl_empl_name" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_name_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:TextBox runat="server" ID="txtb_empl_name" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                                    <asp:Label ID="LblRequired12" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                 </div> 
                                <div class="col-3" >
                                    <div class="row form-group">
                                        <div class="col-4" style="padding-right:0px">
                                            <Label ><strong>ID No:</strong></Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_empl_id" CssClass="form-control form-control-sm text-center" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                 </div> 
                                 <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3 ">
                                            <strong><asp:Label runat="server" Text="Effective Date:"></asp:Label></strong>
                                        </div>
                                        <div class="col-3">
                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanelEffec">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_effective_date" CssClass="form-control form-control-sm my-date text-center"></asp:TextBox>
                                                    <asp:Label ID="LblRequired13" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-12">
                                            <hr style="margin-bottom:5px;margin-top:5px"/>
                                        </div>

                                        <div class="col-3">
                                                <label class="font-weight-bold">Department:</label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdateDep" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_dep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">Sub-Department:</label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_subdep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">Division:</label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_division" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">Section:</label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">Fund Charges:</label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_fund_charges" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                        <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">Function Code:</label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_function_code" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                        <asp:Label ID="LblRequired5" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-12">
                                                <hr style="margin-bottom:5px;margin-top:5px"/>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">Update Master:</label>
                                            </div>
                                            <div class="col-3">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_upd_master_flag" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_upd_master_flag_SelectedIndexChanged">
                                                            <asp:ListItem Text="Yes"  Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LblRequired6" CssClass="lbl_required" runat="server" Text="" style="width:100%"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label CssClass=" smaller badge badge-warning"  runat="server" ID="lbl_upd_master_flag" ></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-12">
                                                <hr style="margin-bottom:5px;margin-top:5px"/>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">PHIC Share Employee:</label>
                                            </div>
                                            <div class="col-3">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_phic_flag" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_upd_master_flag_SelectedIndexChanged">
                                                            <asp:ListItem Text="Yes"  Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <%--<div class="col-12">
                                                <hr style="margin-bottom:5px;margin-top:5px"/>
                                            </div>--%>
                                            <div class="col-3" style="padding-right:0px !important">
                                                <label class="font-weight-bold">Self-Service AO Approv.:</label>
                                            </div>
                                            <div class="col-3">
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_ss_appl_flag" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true">
                                                            <asp:ListItem Text="Yes"  Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                             <div class="col-12 text-right" >
                                                <hr style="margin-bottom:5px;margin-top:5px"/>
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                                        <% if (ViewState["page_allow_add"].ToString() == "1" || ViewState["page_allow_edit"].ToString() == "1")
                                                        {  %>
                                                            <asp:Button ID="Button2" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                                                        <% }
                                                        %> 
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                    </div>
                                </div>
                                
                               

                           </div>
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
                <div class="modal-content text-center">
                <div class="modal-header text-white" style="border-bottom:0px !important">
                <asp:Label runat="server" Text="Invalid Amount"></asp:Label>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                </div>
                <!-- Modal body -->
                    <div class="modal-body">
                        <div class="form-group row">
                            <div class="col-md-3">
                                <i class="fa fa-exclamation-triangle text-danger fa-4x"></i>
                            </div>
                            <div class="col-md-9">
                                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                    <ContentTemplate>
                                            <h4>This Record cannot be Saved!</h4>
                                            <p>
                                                <asp:Label ID="lbl_notification" runat="server"></asp:Label>
                                            </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

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
                                        <div class="col-md-6">
                                            <label >Payroll Year : </label>
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
                                <div class="col-3" >
                                    <div class="form-group row">
                                        
                                            <div class="col-md-7" style="padding-right:0px">
                                                <label >Last Name Starts W/: </label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_start_with" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_start_with_SelectedIndexChanged" >
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
                                <div class="col-2 text-right">
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
                                
                                <div class="col-4">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <% if ((ViewState["page_allow_edit_history"].ToString() == "1") && (ViewState["page_allow_view"].ToString()== "0")){ %>
                                            <label ID="include_history" runat="server" class="container" style="display:inline !important;font-size:14px !important">Include History
                                                <asp:CheckBox ID="chkIncludeHistory" OnCheckedChanged="chkIncludeHistory_CheckedChanged" Autopostback="true" runat="server" />
                                                <span class="checkmark"></span>
                                            </label>
                                            <%} %>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-8" style="margin-bottom:-5px">
                                    <label style="float:left" >Employment Type: </label>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_empl_type" style="width:80.5%;float:right" runat="server"  CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
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
                                                <asp:TemplateField HeaderText="ID NO" SortExpression="account_code">
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
                                               <asp:TemplateField HeaderText="EFF. DATE" SortExpression="effective_date" >
                                                    <ItemTemplate>
                                                        <%# Eval("effective_date") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="UPD. MASTER" SortExpression="upd_master_flag_descr" >
                                                    <ItemTemplate>
                                                        <%# Eval("upd_master_flag_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1" || ViewState["page_allow_view"].ToString() == "1" )
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("empl_id") + "," + Eval("effective_date")%>'/>
                                                        
                                                                <%   }
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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_start_with" />
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
            $('#<%= txtb_effective_date.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });

            
            if ($('#<%= txtb_effective_date.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_effective_date.ClientID %>').closest("div");
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
        function openNotification() {
            $('#notification').modal({
                keyboard: false,
                backdrop:"static"
            });  
        };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
</asp:Content>
