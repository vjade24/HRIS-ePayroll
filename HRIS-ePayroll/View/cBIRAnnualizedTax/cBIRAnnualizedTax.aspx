<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cBIRAnnualizedTax.aspx.cs" Inherits="HRIS_ePayroll.View.cBIRAnnualizedTax"%>

<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

        <asp:ScriptManager ID="sm_Script" runat="server"></asp:ScriptManager>
        <!-- The Modal - Add Confirmation -->
        <!-- The Modal - Add Confirmation -->
        <div class="modal fade" id="AddEditConfirm">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content text-center">
                    <!-- Modal body -->
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>

                            <div class="modal-body">
                                <i class="fa-5x fa fa-check-circle text-success"></i>
                                <h2>Successfully</h2>
                                <h6>
                                    <asp:Label ID="SaveAddEdit" runat="server" Text="Save"></asp:Label></h6>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!-- Modal footer -->
                    <div style="margin-bottom: 30px">
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
                                        <h2>Delete this Record</h2>
                                        <h6>
                                            <asp:Label ID="deleteRec1" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- Modal footer -->
                            <div style="margin-bottom: 50px">
                                <asp:LinkButton ID="lnkBtnYes" runat="server" CssClass="btn btn-danger" OnCommand="btnDelete_Command"> <i class="fa fa-check"></i> Yes, Delete it </asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" data-dismiss="modal" CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" style="overflow:auto" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-lg with-backgrounds" role="document">
                        <div class="modal-content" style="background-image: linear-gradient(white, lightblue)">
                            <div class="modal-header" style="background-image: linear-gradient(green, yellow); padding: 8px!important;">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <h5 class="modal-title" id="AddEditPage">
                                            <asp:Label ID="LabelAddEdit" runat="server" Text="Add/Edit Page" ForeColor="White"></asp:Label></h5>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body with-background" runat="server">
                                <div class="row" runat="server">
                                    <div class="col-9">
                                        <div class="row form-group">
                                            <div class="col-4">
                                                <asp:Label runat="server" Font-Bold="true" Text="Employee Name:"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_empl_id_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:TextBox ID="txtb_empl_name" Visible="false" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                        <asp:TextBox ID="txtb_max_amt" Visible="false" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtb_empl_id" runat="server" CssClass="form-control form-control-sm text-right text-center" Enabled="false" Style="font-weight: bold;"></asp:TextBox>
                                                        <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-12">
                                        <ul class="nav nav-tabs" style="font-size: 15px; font-weight: bold;" id="myTab" role="tablist">
                                            <li class="nav-item">
                                                <a id="tab_1" class="nav-link active" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab">TAXABLE INCOME</a>
                                            </li>
                                            <li class="nav-item">
                                                <a id="tab_2" class="nav-link" data-toggle="tab" href="#non_taxbl_tab" role="tab" aria-controls="non_taxbl_tab">NON-TAXABLE INCOME</a>
                                            </li>
                                            <li class="nav-item">
                                                <a id="tab_3" class="nav-link" data-toggle="tab" href="#summary_tab" role="tab" aria-controls="summary_tab">SUMMARY</a>
                                            </li>

                                            <li class="nav-item">
                                                <a id="tab_4" class="nav-link" data-toggle="tab" href="#monthly_tax_tab" role="tab" aria-controls="monthly_tax_tab">MONTHLY TAX DUE</a>
                                            </li>

                                        </ul>
                                    </div>
                                    <div class="tab-content" id="myTabContent" style="width: 100%">

                                        <div class="tab-pane fade show active" id="mandatory_tab" role="tabpanel" style="padding-bottom: 5px; border-radius: 0px 0px 5px 5px; padding-left: 20px; padding-right: 10px; padding-top: 20px;" aria-labelledby="id_mandatory">
                                            <div class="row" style="margin-top: -20px; margin-left: 0px">
                                                <h4>
                                                    <asp:UpdatePanel ID="UpdatePanel118" runat="server">
                                                        <ContentTemplate>
                                                    <asp:Label ID="lbl_regular" runat="server" Font-Bold="true" Text="Regular"></asp:Label>
                                                            </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                            </h4>
                                            </div>
                                            <div class="row">
                                                <div style="margin-left: 15px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Salary:"></asp:Label></div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_sal" runat="server" CssClass="form-control form-control-sm text-right text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired35" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div style="margin-left: 15px">
                                                      <asp:UpdatePanel ID="UpdatePanel104" runat="server">
                                                       <ContentTemplate>
                                                    <asp:Label ID="lbl_cola" runat="server" Font-Bold="true" Text="COLA:"></asp:Label></div>
                                                      </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>

                                                            <asp:TextBox ID="tbx_cola" runat="server" CssClass="form-control form-control-sm text-right text-right" Enabled="true" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired34" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div>

                                                    
                                                    <asp:UpdatePanel ID="UpdatePanel105" runat="server">
                                                        <ContentTemplate>
                                                    <asp:Label ID="lbl_fha" runat="server" Font-Bold="true" Text="FHA:"></asp:Label></div>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                <div class="col-3" style="margin-left: 17px">
                                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                        <ContentTemplate>

                                                            <asp:TextBox ID="tbx_FHA" runat="server" CssClass="form-control form-control-sm text-right text-right" Enabled="true" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired33" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                           
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>


                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 16px">
                                                        <asp:UpdatePanel ID="UpdatePanel103" runat="server">
                                                        <ContentTemplate>
                                                             <asp:Label id ="lbl_oth_a" runat="server" Visible= "true" Font-Bold="true">Oth A:</asp:Label>
                                                            
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                </div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>

                                                            <asp:TextBox ID="tbx_oth_a" runat="server" CssClass="form-control form-control-sm text-right text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            
                                                            
                                                            <asp:Label ID="LblRequired32" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div style="margin-left: 15px">
                                                    <asp:UpdatePanel ID="UpdatePanel106" runat="server">
                                                        <ContentTemplate>
                                                    <asp:Label ID="lbl_oth_b" runat="server" Font-Bold="true" Text="Oth B:"></asp:Label></div>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_oth_b" runat="server" CssClass="form-control form-control-sm text-right text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                           
                                                            <asp:Label ID="LblRequired31" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-left: 5px; margin-top: 5px">
                                              
                                                    <asp:UpdatePanel ID="UpdatePanel117" runat="server">
                                                        <ContentTemplate>
                                                                <h4><asp:Label ID="lbl_supplmtary" runat="server" Font-Bold="true" Text="Supplementary"></asp:Label></h4>
                                                            
                                                            
                                                          </ContentTemplate>
                                                    </asp:UpdatePanel>

                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 19px">
                                                     <asp:UpdatePanel ID="UpdatePanel107" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lbl_prft_s" runat="server" Font-Bold="true" Text="Prft S:"></asp:Label>
                                                          </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                            </div>
                                                      
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                        <ContentTemplate>

                                                            <asp:TextBox ID="tbx_prft_s" runat="server" CssClass="form-control form-control-sm text-right text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired30" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel109" runat="server">
                                                        <ContentTemplate>
                                                              <asp:Label ID="lbl_13th_oth" runat="server" Font-Bold="true" Text="13th/Oth:"></asp:Label>
                                                       </ContentTemplate>
                                                    </asp:UpdatePanel>      
                                                            </div>
                                                <div class="col-3" style="margin-left: -10px;">
                                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_13_oth" Width="165px" runat="server" CssClass="form-control form-control-sm text-right text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired29" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel110" runat="server">
                                                        <ContentTemplate>
                                                                 <asp:Label ID="lbl_fid" runat="server" Font-Bold="true" Text="FID:"></asp:Label>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>   
                                                            </div>
                                                <div class="col-3" style="margin-left: 20px">
                                                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                        <ContentTemplate>

                                                            <asp:TextBox ID="tbx_fid" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                          
                                                            <asp:Label ID="LblRequired28" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>


                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 17px">
                                                    <asp:UpdatePanel ID="UpdatePanel111" runat="server">
                                                        <ContentTemplate>
                                                             <asp:Label ID="lbl_hzrd" runat="server" Font-Bold="true" Text="Hzrd:"></asp:Label>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                   </div>
                                                <div class="col-3" style="margin-left: 7px">
                                                    <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                                        <ContentTemplate>

                                                            <asp:TextBox ID="tbx_hzrd" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>

                                                            <asp:Label ID="LblRequired27" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div style="margin-left: 15px">
                                                    <asp:UpdatePanel ID="UpdatePanel112" runat="server">
                                                        <ContentTemplate>
                                                    <asp:Label ID="lbl_ot" runat="server" Font-Bold="true" Text="OT:"></asp:Label>
                                                          </ContentTemplate>
                                                    </asp:UpdatePanel>  
                                                            </div>
                                                           
                                                <div class="col-3" style="margin-left: 24px">
                                                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_ot" runat="server" CssClass="form-control form-control-sm text-right" Width="166px" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired26" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div style="margin-left: 3px">
                                                     <asp:UpdatePanel ID="UpdatePanel113" runat="server">
                                                        <ContentTemplate>
                                                              <asp:Label ID="lbl_com" runat="server" Font-Bold="true" Text="Com:"></asp:Label>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>   
                                                            </div>
                                                <div class="col-3" style="margin-left: 8px">
                                                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_com" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                           
                                                            <asp:Label ID="LblRequired25" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 21px">
                                                    <asp:UpdatePanel ID="UpdatePanel114" runat="server">
                                                        <ContentTemplate>
                                                                <asp:Label ID="lbl_hon" runat="server" Font-Bold="true" Text="Hon:"></asp:Label>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>
                                                <div class="col-3" style="margin-left: 7px">
                                                    <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                                        <ContentTemplate>

                                                            <asp:TextBox ID="tbx_hon" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                           
                                                            <asp:Label ID="LblRequired24" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div style="margin-left: 15px">
                                                    <asp:UpdatePanel ID="UpdatePanel115" runat="server">
                                                          <ContentTemplate>
                                                                <asp:Label ID="lbl_sub" runat="server" Font-Bold="true" Text="Sub:"></asp:Label>
                                                           </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>
                                                <div class="col-3" style="margin-left: 17px">
                                                    <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_sub" runat="server" CssClass="form-control form-control-sm text-right" Width="166px" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired23" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div style="margin-left: -8px">
                                                    <asp:UpdatePanel ID="UpdatePanel116" runat="server">
                                                        <ContentTemplate>
                                                    <asp:Label ID="lbl_total_txbl" runat="server" Font-Bold="true" Text="TOTAL:"></asp:Label>
                                                       </ContentTemplate>
                                                    </asp:UpdatePanel>     
                                                            </div>
                                                <div class="col-3" style="margin-left: 5px">
                                                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_total" runat="server" autopostback="true" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired22" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>

                                        </div>
                                        <div class="tab-pane fade show" id="non_taxbl_tab" role="tabpanel" style="padding-bottom: 5px; border-radius: 0px 0px 5px 5px; padding-left: 10px; padding-right: 10px;" aria-labelledby="non_taxbl_tab">
                                            <div class="row" style="margin-left: 10px">
                                                <h4>
                                                    <asp:UpdatePanel ID="UpdatePanel126" runat="server">
                                                        <ContentTemplate>
                                                             <asp:Label ID="lbl_exemp" runat="server" Font-Bold="true" Text="Exemption Comp. Income"></asp:Label>
                                                       </ContentTemplate>
                                                    </asp:UpdatePanel>     
                                                            </h4>
                                            </div>
                                            <div class="row">
                                                <div style="margin-left: 25px">
                                                    <asp:UpdatePanel ID="UpdatePanel108" runat="server">
                                                        <ContentTemplate>
                                                              <asp:Label ID="lbl_13th_non" runat="server" Font-Bold="true" Text="13th Month Pay/Other Forms Comp. :"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                            </div>
                                                <div class="col-3" style="margin-left: 30px">
                                                    <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_13th_oth_non" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired21" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 25px">
                                                     <asp:UpdatePanel ID="UpdatePanel120" runat="server">
                                                        <ContentTemplate>
                                                    <asp:Label ID="lbl_de_min" runat="server" Font-Bold="true" Text="De minimis Benefits :"></asp:Label>
                                                         </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-3" style="margin-left: 155px">
                                                    <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_de_min" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired20" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 25px">
                                                    <asp:UpdatePanel ID="UpdatePanel121" runat="server">
                                                        <ContentTemplate>
                                                                 <asp:Label ID="lbl_slrs_oth" runat="server" Font-Bold="true" Text="Slrs & Oth forms Of Comp.:"></asp:Label>
                                                           </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-3" style="margin-left: 108px">
                                                    <asp:UpdatePanel ID="UpdatePanel64" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_slrs_oth_comp" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired19" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 25px">
                                                    <asp:UpdatePanel ID="UpdatePanel122" runat="server">
                                                        <ContentTemplate>
                                                        <asp:Label ID="lbl_ra" runat="server" Font-Bold="true" Text="Representation Allowance:"></asp:Label>
                                                       </ContentTemplate>
                                                    </asp:UpdatePanel>     
                                                            </div>

                                                <div class="col-3" style="margin-left: 115px">
                                                    <asp:UpdatePanel ID="UpdatePanel62" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_RA" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired18" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 25px">
                                                     <asp:UpdatePanel ID="UpdatePanel123" runat="server">
                                                        <ContentTemplate>
                                                              <asp:Label ID="lbl_ta" runat="server" Font-Bold="true" Text="Travel Allowance:"></asp:Label>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>    
                                                            </div>
                                                <div class="col-3" style="margin-left: 183px">
                                                    <asp:UpdatePanel ID="UpdatePanel63" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_TA" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired17" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>



                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 25px">
                                                    <asp:UpdatePanel ID="UpdatePanel124" runat="server">
                                                        <ContentTemplate>
                                                                 <asp:Label ID="lbl_contri" runat="server" Font-Bold="true" Text="SSS,GSIS,PHIC,HDMF Contri.,Union Dues :"></asp:Label>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>    
                                                            </div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_contri" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired16" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                <div style="margin-left: 25px">
                                                    <asp:UpdatePanel ID="UpdatePanel125" runat="server">
                                                        <ContentTemplate>
                                                              <asp:Label ID="lbl_total_non" runat="server" Font-Bold="true" Text="TOTAL :"></asp:Label>
                                                         </ContentTemplate>
                                                    </asp:UpdatePanel>    
                                                            </div>
                                                <div class="col-3" style="margin-left: 257px">
                                                    <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_total_non" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired15" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>


                                        </div>

                                        <div class="tab-pane fade show" id="summary_tab" role="tabpanel" style="padding-bottom: 5px; border-radius: 0px 0px 5px 5px; padding-left: 10px; padding-right: 10px;" aria-labelledby="summary_tab">

                                            <div class="row" style="margin-left: 10px">
                                                <h4>
                                                    <asp:Label runat="server" Font-Bold="true" Text="Summary"></asp:Label></h4>
                                            </div>

                                            <div class="row" style="margin-top: 8px">

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Gross Comp. :"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 120px">
                                                    <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_gross_comp" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired14" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Net Txbl Inc. :"></asp:Label></div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel57" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_net_txbl" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired13" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 8px">

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Less: Total Non-Txbl :"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 60px">
                                                    <asp:UpdatePanel ID="UpdatePanel51" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_ttl_non_tx" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired12" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Tax Due :"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 35px">
                                                    <asp:UpdatePanel ID="UpdatePanel58" runat="server">
                                                        <ContentTemplate>
                                                           
                                                            
                                                            <asp:TextBox ID="tbx_tax_due" runat="server" CssClass="form-control form-control-sm text-right" AutoPostBack="true" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                           
                                                            <asp:Label ID="LblRequired11" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 8px">

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Txbl Comp. Inc :"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 100px">
                                                    <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_Txbl_inc" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired10" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Percentage"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 22px">
                                                    <asp:UpdatePanel ID="UpdatePanel76" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_per" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            
                                                            <asp:Label ID="LblRequired99" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>


                                                <%-- <h4 style="margin-left:25px"> <asp:Label runat="server" Font-Bold="true" Text="Amt. Of Taxes Withheld"></asp:Label></h4>--%>
                                            </div>

                                            <div class="row" style="margin-top: 8px">

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Add: Txbl Comp. Prv. Emplyr."></asp:Label></div>
                                                <div class="col-3">
                                                    <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="add_tbx_Txbl_com_prev" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            
                                                            <asp:Label ID="LblRequired9" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Amt of Taxes Withheld"></asp:Label></div>
                                                </br>

                                                

                                            </div>


                                            <div class="row" style="margin-top: 8px">

                                                

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Gross Txbl Comp. :"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 80px">
                                                    <asp:UpdatePanel ID="UpdatePanel56" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_gross_txbl" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired7" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Prsnt Emplyr"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 11px">
                                                    <asp:UpdatePanel ID="UpdatePanel59" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_prst_empl" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            
                                                            <asp:Label ID="LblRequired8" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>


                                            </div>

                                            <div class="row" style="margin-top: 8px">

                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Less: Total Exempt."></asp:Label></div>
                                                <div class="col-3" style="margin-left: 78px">
                                                    <asp:UpdatePanel ID="UpdatePanel54" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_ls_ttl_expt" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            
                                                            <asp:Label ID="LblRequired5" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                
                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Prev. Emplyr"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 12px">
                                                    <asp:UpdatePanel ID="UpdatePanel60" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_prev_empl" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            
                                                            <asp:Label ID="LblRequired6" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 8px">


                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Less: Prm on Hlth:"></asp:Label></div>
                                                <div class="col-3" style="margin-left: 85px">
                                                    <asp:UpdatePanel ID="UpdatePanel55" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_ls_prm_hlth" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                           
                                                            <asp:Label ID="LblRequired4" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                
                                                
                                                <div style="margin-left: 25px">
                                                    <asp:Label runat="server" Font-Bold="true" Text="Total Amt. Of Txs As Adjstd."></asp:Label></div>


                                            </div>

                                            <div class="row" style="margin-top: 8px">
                                                
                                                <div class="col-3" style="margin-left: 582px; margin-top: -15px">
                                                    <asp:UpdatePanel ID="UpdatePanel61" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="tbx_ttl_adjstd" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                            <asp:Label ID="LblRequired3" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>









                                        </div>
                                        <div class="tab-pane fade show" id="monthly_tax_tab" role="tabpanel" style="padding-bottom: 5px; border-radius: 0px 0px 5px 5px; padding-left: 10px; padding-right: 10px;" aria-labelledby="monthly_tax_tab">


                                            <div class="row" style="margin-left: 10px">
                                                <h4>
                                                    <asp:Label runat="server" Font-Bold="true" Text="Monthly Tax Due"></asp:Label></h4>
                                            </div>

                                            <div class="row">
                                                <div class="col-4" style="margin-left:13px;">
                                                    <asp:Label runat="server" Font-Bold="true" Text="TOTAL AMT."></asp:Label>
                                                 </div>
                                                <div class="col-4" style="margin-left:-147px;">

                                                            <asp:UpdatePanel ID="UpdatePanel101" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_ttl_amt_due" runat="server" Autopostback="true" CssClass="form-control form-control-sm text-right" width="223px" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="Label40" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                 </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-5">
                                                        </div>
                                                        <div class="col-3" style="margin-left: -3px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="Actual"></asp:Label>
                                                        </div>
                                                        <div class="col-4">
                                                            <asp:Label runat="server" Font-Bold="true" Text="Projected"></asp:Label>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-5">
                                                        </div>
                                                        <div class="col-3" style="margin-left: -3px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="Actual"></asp:Label>
                                                        </div>
                                                        <div class="col-4">
                                                            <asp:Label runat="server" Font-Bold="true" Text="Projected"></asp:Label>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                            
                                            <div class="row">

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="January"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel77" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_jan_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired70" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel78" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_jan_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                   
                                                                    <asp:Label ID="LblRequired71" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="July"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel79" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_jul_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired72" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel80"  runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_jul_due_prj"  runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                      
                                                                    <asp:Label ID="LblRequired73" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 3px">

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="February"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel81" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_feb_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired74" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel82" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_feb_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                   
                                                                    <asp:Label ID="LblRequired75" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="August"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel83" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_aug_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired76" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel84" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_aug_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                   
                                                                    <asp:Label ID="LblRequired77" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 3px">

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="March"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel85" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_mar_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired78" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel86" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_mar_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                  
                                                                    <asp:Label ID="LblRequired79" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="September"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel87" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_sep_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired80" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel88" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_sep_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                   
                                                                    <asp:Label ID="LblRequired81" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 3px">

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="April"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel89" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_apr_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired82" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel90"  runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_apr_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    
                                                                    <asp:Label ID="LblRequired83" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="October"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel91" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_oct_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired84" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel92" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_oct_due_prj"  runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                     
                                                                    <asp:Label ID="LblRequired85" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 3px">

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="May"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel93" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_may_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired86" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel94" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_may_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                   
                                                                    <asp:Label ID="LblRequired87" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="November"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel95" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_nov_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired88" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel96" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_nov_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                      
                                                                    <asp:Label ID="LblRequired89" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row" style="margin-top: 3px">

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="June"></asp:Label>
                                                        </div>
                                                         <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel97" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_jun_due_act1" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired90" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel98" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_jun_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    
                                                                    <asp:Label ID="LblRequired91" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-6">
                                                    <div class="row">

                                                        <div class="col-4" style="margin-left: 15px">
                                                            <asp:Label runat="server" Font-Bold="true" Text="December"></asp:Label>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel99" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_dec_due_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired92" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-4" style="margin-left: -15px">
                                                            <asp:UpdatePanel ID="UpdatePanel100" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="tbx_dec_due_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="False" Style="font-weight: bold;"></asp:TextBox>
                                                                     
                                                                    <asp:Label ID="LblRequired93" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                    </div>


                                </div>
                             
                               
                                    
                                           <div class="row">
                                            
                                               <div class="col-12">
                                               <asp:UpdatePanel ID="UpdatePanel102" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
                                                   <ContentTemplate>
                                                       <asp:TextBox ID="txtb_search1" onInput="search_for(event);" runat="server" class="form-control" placeholder="Search.." Height="30px"
                                                           Width="100%" OnTextChanged="txtb_search_TextChanged_dtl" AutoPostBack="true"></asp:TextBox>
                                                       <script type="text/javascript">
                                                           function search_for(key) {
                                                               __doPostBack("<%= txtb_search1.ClientID %>", "");
                                                           }
                                                       </script>
                                                   </ContentTemplate>
                                               </asp:UpdatePanel>
                                           </div>
                                       </div>
                                

                                <div class="row">
                                </div>
                                <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                <div class="row">
                                    <div class="col-12">
                                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView
                                                    ID="GridView1"
                                                    runat="server"
                                                    AllowPaging="True"
                                                    AllowSorting="True"
                                                    AutoGenerateColumns="False"
                                                    EnableSortingAndPagingCallbacks="True"
                                                    ForeColor="#333333"
                                                    GridLines="Both" Height="100%"
                                                    OnSorting="gv_dataListGrid_Sorting_dtl"
                                                    OnPageIndexChanging="gridviewbind_PageIndexChanging_dtl"
                                                    PagerStyle-Width="3"
                                                    PagerStyle-Wrap="false"
                                                    PageSize="5"
                                                    Width="100%"
                                                    Font-Names="Century gothic"
                                                    Font-Size="small"
                                                    RowStyle-Width="5%"
                                                    AlternatingRowStyle-Width="10%"
                                                    CellPadding="2"
                                                    ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="NO DATA FOUND"
                                                    EmptyDataRowStyle-ForeColor="Red"
                                                    EmptyDataRowStyle-CssClass="no-data-found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText=" ID" SortExpression="empl_id">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("empl_id") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="CENTER" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ACCT. TITLE" SortExpression="account_title">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("account_title") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="40%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="LEFT" />
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="ACCT. CLASS CODE" SortExpression="account_title" Visible ="false">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("acctclass_code") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="40%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="LEFT" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ACCT. CLASS" SortExpression="acctclass_descr">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("acctclass_descr") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="LEFT" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ACCT. AMT" SortExpression="acct_total_amount">
                                                            <ItemTemplate>
                                                                &nbsp;<%# Eval("acct_total_amount") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="AMOUNT">
                                                            <ItemTemplate>
                                                                 <% if (DateTime.Now.Month == 01){%> <%# Eval("monthly_amount_01")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 02){%> <%# Eval("monthly_amount_02")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 03){%> <%# Eval("monthly_amount_03")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 04){%> <%# Eval("monthly_amount_04")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 05){%> <%# Eval("monthly_amount_05")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 06){%> <%# Eval("monthly_amount_06")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 07){%> <%# Eval("monthly_amount_07")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 08){%> <%# Eval("monthly_amount_08")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 09){%> <%# Eval("monthly_amount_09")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 10){%> <%# Eval("monthly_amount_10")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 11){%> <%# Eval("monthly_amount_11")+"&nbsp;&nbsp;" %><%}%>
                                                                 <% if (DateTime.Now.Month == 12){%> <%# Eval("monthly_amount_12")+"&nbsp;&nbsp;" %><%}%>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="22%" />
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
                                                                        <asp:ImageButton ID="imgbtn_editrow1_dtl" CssClass="btn btn-primary action" EnableTheming="true" runat="server" OnCommand="editRow1_Command" ImageUrl="~/ResourceImages/final_edit.png" CommandArgument='<%# Eval("empl_id") + "," + Eval("account_code") + "," + Eval("account_sub_code") + "," + Eval("payroll_year") %>' />

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
                                            <%--<asp:AsyncPostBackTrigger ControlID="Button2" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                            <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_department" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>


                            </div>
                            <div class="modal-footer">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btncancel" runat="server" data-dismiss="modal" Text="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                        <asp:Button ID="btncalculate" runat="server" style="color:white;" Text="Calculate" CssClass="btn btn-warning save-icon icn" OnClick="btncalculate_Click"/>
                                        <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-primary save-icon icn" OnClick="btnSave_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="edit_dtl" style="overflow:auto" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-md with-backgrounds" role="document">
                        <div class="modal-content" style="background-image: linear-gradient(white, lightblue)">
                            <div class="modal-header" style="background-image: linear-gradient(green, yellow); padding: 8px!important;">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <h5 class="modal-title" id="">
                                            <asp:Label ID="Label6" runat="server" Text="Add/Edit Page" ForeColor="White"></asp:Label></h5>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body with-background" runat="server">

                                <div class="row">

                                    <div class="col-3">
                                        <asp:Label runat="server" Font-Bold="true" Text="Acct. Title"></asp:Label>
                                    </div>
                                    <div class="col-9">
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="tbx_acc_title" runat="server" CssClass="form-control form-control-sm" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:TextBox ID="tbx_acc_code" runat="server" CssClass="form-control form-control-sm" Enabled="false" Visible="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:TextBox ID="tbx_acc_sub_code" runat="server" CssClass="form-control form-control-sm" Enabled="false" Visible="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="Label13" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                </div>
                                <div class="row" style="margin-top: 3px">

                                    <div class="col-3">
                                        <asp:Label runat="server" Font-Bold="true" Text="Acct. Class"></asp:Label>
                                    </div>
                                    <div class="col-9">
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="tbx_acct_class" runat="server" CssClass="form-control form-control-sm" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="Label14" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                <asp:Label ID="acct_class_code" runat="server" Visible ="false" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                </div>

                                <div class="row" style="margin-top: 3px">

                                    <div class="col-3">
                                        <asp:Label runat="server" Font-Bold="true" Text="Acct. Total"></asp:Label>
                                    </div>
                                    <div class="col-9">
                                        <asp:UpdatePanel ID="UpdatePanel75" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="tbx_acct_ttl" runat="server" CssClass="form-control form-control-sm" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="Label70" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                </div>


                                <hr style="margin-top: 5px; margin-bottom: 5px;" />


                                <div class="row">
                                    <div class="col-4">
                                    </div>
                                    <div class="col-4" style="margin-left: -20px;">
                                        <asp:Label runat="server" Font-Bold="true" Text="Actual"></asp:Label>
                                    </div>

                                    <div class="col-4" style="margin-left: 20px;">

                                        <asp:Label runat="server" Font-Bold="true" Text="Projected"></asp:Label>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="January"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_jan_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired100" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_jan_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired101" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="February"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_feb_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired102" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_feb_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired103" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="March"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_mar_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired104" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_mar_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired105" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="April"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_apr_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired106" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_apr_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired107" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="May"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_may_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired108" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_may_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired109" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="June"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_jun_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired110" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_jun_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired111" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="July"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_jul_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired112" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_jul_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired113" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="August"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel65" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_aug_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired114" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel66" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_aug_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired115" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="September"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel67" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_sep_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired116" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel68" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_sep_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired117" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="October"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel69" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_oct_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired118" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel70" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_oct_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired119" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="November"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel71" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_nov_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired120" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel72" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_nov_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired121" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 1px">
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="December"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel73" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_dec_amount_act" runat="server" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired122" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-5">
                                        <asp:UpdatePanel ID="UpdatePanel74" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_dec_amount_prj" runat="server" CssClass="form-control form-control-sm text-right" Enabled="true" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                <asp:Label ID="LblRequired123" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>



                                <div class="modal-footer">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" data-dismiss="modal" Text="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                            <asp:Button ID="btn_update" runat="server" OnCommand="btn_update_Command" Text="Save" CssClass="btn btn-primary save-icon icn" />
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
                            <div class="modal-header text-white" style="background-color: #dc3545;">
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
                <div class="col-4"><strong style="font-family: Arial; font-size: 19px; color: white;"><%: Master.page_title %></strong></div>
                <div class="col-8">
                    <asp:UpdatePanel ID="UpdatePanel11" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
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
                                <div class="row" style="margin-bottom: 10px; padding-left: 10px;">
                                    <div class="col-3">
                                         <div class="form-group row">
                                            <div class="col-12">
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
                                          </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group row">
                                            <div class="col-6">
                                                <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Payroll Year : "></asp:Label>
                                            </div>
                                            <div class="col-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-4">
                                                <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="Employment Type: "></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-4">
                                                <asp:Label ID="Label5" Font-Bold="true" runat="server" Text="Department: "></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm text-right" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="Label1" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-4">
                                                <asp:Label ID="Label7" Font-Bold="true" runat="server" Text="Sub-Department: "></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_subdep" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm text-right" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-4">
                                                <asp:Label ID="Label8" Font-Bold="true" runat="server" Text="Division : "></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_division" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm text-right" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="Label9" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group row">
                                            <div class="col-4">
                                                <asp:Label ID="Label10" Font-Bold="true" runat="server" Text="Section : "></asp:Label>
                                            </div>
                                            <div class="col-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_section" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm text-right" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="Label11" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-2 text-right">
                                                <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                    <ContentTemplate>

                                                        <% if (ViewState["page_allow_add"].ToString() == "1")
                                                            {  %>
                                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn" Text="Add" Visible="false" OnClick="btnAdd_Click" />
                                                        <% }
                                                        %>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <asp:UpdatePanel ID="up_dataListGrid" UpdateMode="Conditional" runat="server">
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
                                            EmptyDataRowStyle-CssClass="no-data-found">
                                            <Columns>
                                                <asp:TemplateField HeaderText=" ID" SortExpression="empl_id">
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
                                                    <ItemStyle Width="30%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GROSS INCOME" SortExpression="total_gross_pay">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Convert.ToDecimal(Eval("total_gross_pay")).ToString("#,##0.00") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="TAXABLE INCOME" SortExpression="total_taxable_income">
                                                    <ItemTemplate>
                                                        &nbsp;<%#  Convert.ToDecimal(Eval("total_taxable_income")).ToString("#,##0.00") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MONTHLY TAXABLE INCOME" SortExpression="total_taxable_income">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Convert.ToDecimal(Convert.ToDecimal((Eval("total_taxable_income")))/12).ToString("#,##0.00") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="27%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                <asp:ImageButton ID="imgbtn_editrow1" CssClass="btn btn-primary action" EnableTheming="true" runat="server" ImageUrl="~/ResourceImages/final_edit.png" OnCommand="editRow_Command" CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_year")%>' />

                                                                <%  }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                <asp:ImageButton ID="lnkDeleteRow" CssClass="btn btn-danger action" EnableTheming="true" runat="server" ImageUrl="~/ResourceImages/final_delete.png" OnCommand="deleteRow_Command" CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_year")%>' />
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
                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                        <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                        <asp:AsyncPostBackTrigger ControlID="txtb_search1" />
                                        <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_department" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_subdep" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_division" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_section" />
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
           <%-- $('#<%= txtb_date_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_date_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });--%>
        }
        function to_required_header() {
            $("#header-id").click();
        }
        function to_required_details() {
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
        function closeDateModal() {
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
        function closeModal1() {
            $('#add').modal("dispose");
        };
    </script>



    <script type="text/javascript">
        function openModalDelete() {
            $('#deleteRec').modal({
                keyboard: false,
                backdrop: "static"
            });
        };
    </script>

    <script type="text/javascript">
        function closeModalDelete() {
            $('#deleteRec').modal('hide');
        };
    </script>

    <script type="text/javascript">
        function openEditDetail() {
         
            $('#edit_dtl').modal("show");
            $('#edit_dtl').modal({
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
                backdrop: "static"
            });
            setTimeout(function () {
                $('#AddEditConfirm').modal("hide");
                $('.modal-backdrop.show').remove();

            }, 800);

        };

        function closeModal12() {
            $('#edit_dtl').modal("hide");
           

        };

        function select_tab(tab_nbr)
        {
            if (tab_nbr == 1)
            {
                $("#tab_1").click();
            }
             if (tab_nbr == 2)
            {
                $("#tab_2").click();
            }
             if (tab_nbr == 3)
            {
                $("#tab_3").click();
            }

            if (tab_nbr == 4)
            {
                $("#tab_4").click();
            }
        }
       
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
</asp:Content>
