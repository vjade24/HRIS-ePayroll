<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayCertification.aspx.cs" Inherits="HRIS_ePayroll.View.cPayCertification.cPayCertification" %>
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
                    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue);">
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
                        <div class="modal-body with-background"  runat="server">
                            <div class="row" runat="server">
                                <div class ="col-4">
                                    <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold"  Text="Template Code:"></asp:Label>
                                </div>
                                <div class ="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_template_code" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12" >
                                    <hr style="margin-top:5px;margin-bottom:0px;" />
                                </div>
                             </div>

                            <div class="row" style="margin-top:5px;">
                                <div class="col-12">
                                    <ul class="nav nav-tabs" style="font-size: 15px; font-weight: bold;" id="myTab" role="tablist">
                                        <li class="nav-item">
                                            <a id="tab_1" class="nav-link active" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab">Signatory Certification</a>
                                        </li>
                                        <li class="nav-item">
                                            <a id="tab_2" class="nav-link" data-toggle="tab" href="#signatory_name" role="tab" aria-controls="signatory_name">Signatory Name/Designation Override</a>
                                        </li>

                                    </ul>
                                </div>
                                <div class="tab-content" id="myTabContent" style="width: 100%">
                                    <div class="tab-pane fade show active" id="mandatory_tab" role="tabpanel" style="padding-bottom: 5px; border-radius: 0px 0px 5px 5px; padding-left: 20px; padding-right: 10px; padding-top: 20px;" aria-labelledby="id_mandatory">
                                        <div class="row" runat="server" style="margin-top:5px;">
                                            <div class ="col-3">
                                                <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold"  Text="Certification #1:"></asp:Label>
                                            </div>
                                            <div class ="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sig1_cert" TextMode="MultiLine" Height = "60px" runat="server" MaxLength="255"  Width="100%" CssClass="form-control"></asp:TextBox>
                                                        <asp:Label ID="LblRequired2" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row" runat="server" style="margin-top:5px;">
                                            <div class ="col-3">
                                                    <asp:Label ID="Label4" runat="server" CssClass="font-weight-bold"  Text="Certification #2:"></asp:Label>
                                            </div>
                                            <div class ="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sig2_cert" TextMode="MultiLine" Height = "60px" runat="server" MaxLength="255"  Width="100%" CssClass="form-control"></asp:TextBox>
                                                        <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row" runat="server" style="margin-top:5px;">
                                            <div class ="col-3">
                                                <asp:Label ID="Label6" runat="server" CssClass="font-weight-bold"  Text="Certification #3:"></asp:Label>
                                            </div>
                                            <div class ="col-9">
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_sig3_cert" TextMode="MultiLine" Height = "60px" runat="server" MaxLength="255"  Width="100%" CssClass="form-control"></asp:TextBox>
                                                            <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row" runat="server" style="margin-top:5px;">
                                            <div class ="col-3">
                                                    <asp:Label ID="Label8" runat="server" CssClass="font-weight-bold"  Text="Certification #4:"></asp:Label>
                                            </div>
                                            <div class ="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sig4_cert" TextMode="MultiLine" Height = "60px" runat="server" MaxLength="255"  Width="100%" CssClass="form-control"></asp:TextBox>
                                                        <asp:Label ID="LblRequired5" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>   
                                    </div>
                                    <div class="tab-pane fade show" id="signatory_name" role="tabpanel" style="padding-bottom: 5px; border-radius: 0px 0px 5px 5px; padding-left: 20px; padding-right: 10px; padding-top: 20px;" aria-labelledby="id_mandatory">
                                        <div class="row" runat="server" style="margin-top:5px;">
                                             <div class ="col-3">
                                                     <asp:Label ID="Label12" runat="server" CssClass="font-weight-bold"  Text="Signatory #1:"></asp:Label>
                                             </div>
                                             <div class ="col-9">
                                                  <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtb_sig1_name" Visible="true" runat="server" Width="100%" CssClass="form-control form-control-sm" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired6" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                  </asp:UpdatePanel>
                                             </div>
                                        </div>

                                            <div class="row" runat="server" style="margin-top:5px;">
                                                 <div class ="col-3">

                                                         <asp:Label ID="Label14" runat="server" CssClass="font-weight-bold"  Text="Designation #1:"></asp:Label>
                                                  
                                                 </div>

                                                    <div class ="col-9">

                                                          <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                           <ContentTemplate>
                                                               <asp:TextBox ID="txtb_pos1" Visible="true" runat="server" Width="100%" CssClass="form-control form-control-sm" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                               <asp:Label ID="LblRequired7" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                           </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                    
                                                    </div>
                                              </div>

                                         <div class="row">
                                           <div class="col-12" >
                                                  <hr style="margin-top:5px;margin-bottom:5px;" />
                                         </div>
                                        </div>

                                             <div class="row" runat="server" style="margin-top:5px;">
                                             <div class ="col-3">

                                                     <asp:Label ID="Label16" runat="server" CssClass="font-weight-bold"  Text="Signatory #2:"></asp:Label>
                                              
                                             </div>

                                                <div class ="col-9">

                                                     <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                           <ContentTemplate>
                                                               <asp:TextBox ID="txtb_sig2_name" Visible="true" runat="server" Width="100%" CssClass="form-control form-control-sm" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                               <asp:Label ID="LblRequired8" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                           </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                
                                                </div>
                                              </div>

                                             <div class="row" runat="server" style="margin-top:5px;">
                                                <div class ="col-3">
                                                        <asp:Label ID="Label18" runat="server" CssClass="font-weight-bold"  Text="Designation #2:"></asp:Label>
                                                </div>
                                                <div class ="col-9">
                                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtb_pos2" Visible="true" runat="server" Width="100%" CssClass="form-control form-control-sm" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired9" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                               </div>

                                                    <div class="row">
                                                       <div class="col-12" >
                                                              <hr style="margin-top:5px;margin-bottom:5px;" />
                                                     </div>
                                                    </div>

                                        <div class="row" runat="server" style="margin-top:5px;">
                                                <div class ="col-3">

                                                        <asp:Label ID="Label19" runat="server" CssClass="font-weight-bold"  Text="Signatory #3:"></asp:Label>
                                                 
                                                </div>

                                                   <div class ="col-9">

                                                       <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                           <ContentTemplate>
                                                               <asp:TextBox ID="txtb_sig3_name" Visible="true" runat="server" Width="100%" CssClass="form-control form-control-sm" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                               <asp:Label ID="LblRequired10" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                           </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                   
                                                   </div>
                                               </div>

                                        <div class="row" runat="server" style="margin-top:5px;">
                                                <div class ="col-3">

                                                        <asp:Label ID="Label21" runat="server" CssClass="font-weight-bold"  Text="Designation #3:"></asp:Label>
                                                 
                                                </div>

                                                   <div class ="col-9">

                                                       <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                           <ContentTemplate>
                                                               <asp:TextBox ID="txtb_pos3" Visible="true" runat="server" Width="100%" CssClass="form-control form-control-sm" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                               <asp:Label ID="LblRequired11" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                           </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                   
                                                   </div>
                                               </div>

                                         <div class="row">
                                             <div class="col-12" >

                                                           <hr style="margin-top:5px;margin-bottom:5px;" />
                                             </div>
                                          </div>

                                        <div class="row" runat="server" style="margin-top:5px;">
                                                <div class ="col-3">

                                                        <asp:Label ID="Label23" runat="server" CssClass="font-weight-bold"  Text="Signatory #4:"></asp:Label>
                                                 
                                                </div>

                                                   <div class ="col-9">

                                                       <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                           <ContentTemplate>
                                                               <asp:TextBox ID="txtb_sig4_name" Visible="true" runat="server" Width="100%" CssClass="form-control form-control-sm" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                               <asp:Label ID="LblRequired12" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                           </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                   
                                                   </div>
                                               </div>

                                        <div class="row" runat="server" style="margin-top:5px;">
                                                <div class ="col-3">

                                                        <asp:Label ID="Label25" runat="server" CssClass="font-weight-bold"  Text="Designation #4:"></asp:Label>
                                                 
                                                </div>

                                                   <div class ="col-9">

                                                       <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                           <ContentTemplate>
                                                               <asp:TextBox ID="txtb_pos4" Visible="true" runat="server" Width="100%" CssClass="form-control form-control-sm" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                               <asp:Label ID="LblRequired13" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                           </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                   
                                                   </div>
                                               </div>

                                            
                                                
                                        </div>

                                    
                                </div>

                            </div>

                             <div class="row">
                                 <div class="col-12" >
                                    <hr style="margin-top:5px;margin-bottom:0px;" />
                                </div>
                                 <div class="col-12 text-right" style="margin-top:5px">
                                     <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                    <asp:Button ID="Button2" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
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
                <div class="col-4"><strong style="font-family:Arial;font-size:20px;color:white;"><%: Master.page_title %></strong></div>
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
                                                <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="20%" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="5" Value="5" />
                                                    <asp:ListItem Text="10" Selected="True" Value="10" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                            <asp:Label runat="server" Text="Entries"></asp:Label>
                                            &nbsp;&nbsp;|&nbsp;&nbsp;
                                                     <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9"></asp:Label>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                 <div class="col-7">
                                    <asp:Label ID="Label28" style="float:left;padding-top:2px;margin-right:5px;" runat="server" Text="Employment Type:"></asp:Label>
                                     <div style="float:left;width:50%;">
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" ></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                  </div>

                              <div class="col-1" >
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            
                                            <% if (ViewState["page_allow_add"].ToString() == "1")
                                                {  %>
                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn" Visible="false"  Text="Add" OnClick="btnAdd_Click" />
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
                                            AllowPaging="True"
                                            AllowSorting="True"
                                            AutoGenerateColumns="False"
                                            EnableSortingAndPagingCallbacks="True"
                                            ForeColor="#333333"
                                            GridLines="Both" Height="100%"
                                            OnSorting="gv_dataListGrid_Sorting"
                                            OnPageIndexChanging="gridviewbind_PageIndexChanging"
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
                                            EmptyDataRowStyle-CssClass="no-data-found"
                                            >
                                           <Columns>
                                                <asp:TemplateField HeaderText="CODE" SortExpression="payrolltemplate_code">
                                                    <ItemTemplate>
                                                        <%# Eval("payrolltemplate_code") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="2%" />
                                                    <HeaderStyle HorizontalAlign="CENTER" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="DESCRIPTION" SortExpression="payrolltemplate_short_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# (Eval("payrolltemplate_short_descr").ToString().Length > 15) ? Eval("payrolltemplate_short_descr").ToString().Substring(0,15) + "..." : Eval("payrolltemplate_short_descr").ToString() %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="18%" />
                                                    <HeaderStyle HorizontalAlign="LEFT" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HEAD/DESIGNATED" SortExpression="sig1_certification">
                                                    <ItemTemplate>
                                                         &nbsp;<%# (Eval("sig1_certification").ToString().Length > 20) ? Eval("sig1_certification").ToString().Substring(0,20) + "..." : Eval("sig1_certification").ToString() %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="LEFT" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DEPT. HEAD" SortExpression="sig2_certification">
                                                    <ItemTemplate>
                                                        &nbsp;<%# (Eval("sig2_certification").ToString().Length > 20) ? Eval("sig2_certification").ToString().Substring(0,20) + "..." : Eval("sig2_certification").ToString() %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="LEFT" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="PROV. TREASURY" SortExpression="sig3_certification">
                                                    <ItemTemplate>
                                                        &nbsp;<%# (Eval("sig3_certification").ToString().Length > 20) ? Eval("sig3_certification").ToString().Substring(0,20) + "..." : Eval("sig3_certification").ToString() %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="LEFT" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OTHER SIG." SortExpression="sig4_certification">
                                                    <ItemTemplate>
                                                        &nbsp;<%# (Eval("sig4_certification").ToString().Length > 20) ? Eval("sig4_certification").ToString().Substring(0,20) + "..." : Eval("sig4_certification").ToString() %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="LEFT" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                               
                                                                    <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("payrolltemplate_code")%>'/>
                                                        
                                                                    <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server"  ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("payrolltemplate_code")%>'/>
                                                              
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
                backdrop:"static"
            });
       };
       
    </script>
    <script type="text/javascript">
        function openModal1() {
            $('#add1').modal({
                keyboard: false,
                backdrop:"static"
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
