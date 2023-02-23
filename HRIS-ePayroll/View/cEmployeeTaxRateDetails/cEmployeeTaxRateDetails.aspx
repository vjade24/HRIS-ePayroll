<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cEmployeeTaxRateDetails.aspx.cs" Inherits="HRIS_ePayroll.View.cEmployeeTaxRateDetails.cEmployeeTaxRateDetails" %>
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
                                    <asp:Label ID="SaveAddEdit" runat="server" Text="Generated"></asp:Label></h6>
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
                                        <i ID="icon_delete" runat="server" class="fa-5x fa fa-question text-danger"></i>
                                        <i ID="icon_generate" runat="server" class="fa-5x fa fa-question text-success"></i>

                                        <h2 id="Lbl_delete" runat="server"></h2>
                                        <h6>
                                            <asp:Label ID="deleteRec1" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
                                    </div>
                               
                                </ContentTemplate>
                                </asp:UpdatePanel> 
                            <!-- Modal footer -->
                               <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                            <div style="margin-bottom: 50px">

                                <asp:LinkButton ID="lnkBtnYes" runat="server" CssClass="btn btn-danger" OnCommand="btnDelete_Command">
                                    Yes
                                    <i class="fa fa-check"></i>
                                    
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkBtnYes_gen" runat="server" CssClass="btn btn-success" OnCommand="btnGenerate_Command" >
                                   Yes
                                    <i class="fa fa-check"></i>
                                    
                                </asp:LinkButton>
                                
                                <asp:LinkButton ID="LinkButton3" runat="server" data-dismiss="modal" CssClass="btn btn-dark" >
                                    No
                                    <i class="fa fa-times"></i>
                                </asp:LinkButton>
                               
                               </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" style="overflow:auto" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-lg with-backgrounds" role="document">
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
                                    <div class="col-3">
                                        <asp:Label runat="server" Font-Bold="true" Text="Employee's Name:"></asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                               
                                                <asp:TextBox ID="txtb_empl_name" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                        
                                               
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-1" style="padding-right:0px">
                                        <asp:Label runat="server" Font-Bold="true" Text="ID No:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_empl_id" runat="server" CssClass="form-control form-control-sm text-right text-center" Enabled="false" Style="font-weight: bold;"></asp:TextBox>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-12">
                                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                    </div>
                                   <div class="col-3">
                                        <asp:Label runat="server" Font-Bold="true" Text="Position Title:"></asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                               
                                                <asp:TextBox ID="txtb_position" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                        
                                                <asp:Label ID="Label3" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-1">
                                         <asp:Label runat="server" Font-Bold="true" Text="Basic:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                            <ContentTemplate>
                                                <span style="position:absolute;float:right;right:10%;padding-top:2px; padding-right:5px;color:lightgray;">| %</span>
                                                <asp:TextBox ID="txtb_bus_tax" runat="server" Width="100%" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;padding-right:27px;"></asp:TextBox>
                                                        
                                                <asp:Label ID="Label6" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                     <div class="col-12">
                                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                    </div>
                                   <div class="col-3">
                                        <asp:Label runat="server" Font-Bold="true" Text="BIR Class:"></asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                               
                                                <asp:TextBox ID="txtb_bir_class" runat="server" Width="100%" CssClass="form-control form-control-sm" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;"></asp:TextBox>
                                                        
                                                <asp:Label ID="Label5" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" Visible="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_bir_class" CssClass="form-control form-control-sm"  AutoPostBack="true" runat="server">
                                                        
                                                </asp:DropDownList>

                                                
                                                <asp:TextBox ID="txtb_with_sworn" Enabled ="false" runat="server" CssClass="form-control form-control-sm text-center" Visible="false" Style="font-weight: bold;"></asp:TextBox>

                                                <asp:Label ID="Label1" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-1">
                                         <asp:Label runat="server" Font-Bold="true" Text="Addl.:"></asp:Label>
                                    </div>

                                    <div class="col-2 input-group">
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                 <span style="position:absolute;float:right;right:10%;padding-top:2px; padding-right:5px;color:lightgray;">| %</span>
                                                <asp:TextBox ID="txtb_wheld_tax" runat="server" Width="100%" CssClass="form-control form-control-sm text-right" Enabled="false" Style="font-weight: bold; letter-spacing: 3px;padding-right:27px;"></asp:TextBox>
                                                        
                                                <asp:Label ID="Label4" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>   
                                    <div class="col-12">
                                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                    </div>
                                    <div class="col-3">
                                        <asp:Label runat="server" Font-Bold="true" Text="Payroll Template:"></asp:Label>
                                    </div>
                                    <div class="col-4">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                               <asp:TextBox ID="txtb_payroll_template" runat="server" CssClass="form-control form-control-sm text-center" Enabled="false" Style="font-weight: bold;"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-2">
                                        <asp:Label runat="server" Font-Bold="true" Text="Month: "></asp:Label>
                                    </div>
                                    <div class="col-3">
                                        <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control text-center" AutoPostBack="true">
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
                                                    <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                <asp:TextBox ID="txtb_month" Enabled ="false" runat="server" CssClass="form-control form-control-sm text-center" Visible="false" Style="font-weight: bold;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                     </div>
                                </div>

                                 <div class="col-12">
                                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                    </div>
                                 <div class="row" style="margin-top:3px;" >
                                    <div class="col-3">
                                             <asp:Label runat="server" Font-Bold="true" Text="Remarks:"></asp:Label>
                                    </div>
                                    <div class="col-4">
                                        
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_remarks" TextMode="MultiLine"  runat="server" CssClass="form-control form-control-sm" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired3" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                       
                                    </div>
                                     <div class="col-2">
                                             <asp:Label runat="server" Font-Bold="true" Text="VCH. Nbr.:"></asp:Label>
                                    </div>
                                     <div class="col-3">
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtb_voucher" runat="server" CssClass="form-control form-control-sm text-center" Style="font-weight: bold;"></asp:TextBox>
                                               <asp:Label ID="LblRequired4" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                 <div class="col-12">
                                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                  </div>
                                <div class="row" style="margin-top:3px;" >
                                  <div class="col-3">
                                           <asp:Label runat="server" Font-Bold="true" Text="PERA/CA:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePane20" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txtb_pera_ca" runat="server" CssClass="form-control form-control-sm text-right" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired5" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                    <div class="col-2">
                                           <asp:Label runat="server" Font-Bold="true" Text="GSIS Prem.:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txtb_gsis" runat="server" CssClass="form-control form-control-sm text-right" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired6" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                    <div class="col-1">
                                           <asp:Label runat="server" Font-Bold="true" Text="Gross:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txtb_gross" runat="server" CssClass="form-control form-control-sm text-right"  Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired7" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                  


                                </div>

                                <div class="row" style="margin-top:3px;" >
                                  <div class="col-3">
                                           <asp:Label runat="server" Font-Bold="true" Text="Hazard Pay:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txtb_hazard" runat="server" CssClass="form-control form-control-sm text-right" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired8" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                    <div class="col-2">
                                           <asp:Label runat="server" Font-Bold="true" Text="PHIC Prem.:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txtb_phic" runat="server" CssClass="form-control form-control-sm text-right" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired9" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                    <div class="col-1">
                                    </div>
                                    <div class="col-2">
                                        
                                    </div>

                                  


                                </div>

                                <div class="row" style="margin-top:3px;" >
                                  <div class="col-3">
                                           <asp:Label runat="server" Font-Bold="true" Text="Subsistence Allo.:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txtb_subs_allo" runat="server" CssClass="form-control form-control-sm text-right" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired10" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                    <div class="col-2">
                                           <asp:Label runat="server" Font-Bold="true" Text="HDMF Prem.:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txtb_hdmf" runat="server" CssClass="form-control form-control-sm text-right" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired11" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                    <div class="col-1">
                                    </div>
                                    <div class="col-2">
                                        
                                    </div>

                                  


                                </div>

                                <div class="row" style="margin-top:3px;" >
                                  <div class="col-3">
                                           <asp:Label runat="server" Font-Bold="true" Text="Laundry Allo.:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txtb_laundry" runat="server" CssClass="form-control form-control-sm text-right" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired12" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                    <div class="col-2">
                                           <asp:Label runat="server" Font-Bold="true" Text="W/Held Tax:"></asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                            <ContentTemplate>
                                            
                                            <asp:TextBox ID="txtb_wheld" runat="server" CssClass="form-control form-control-sm text-right" Style="font-weight: bold;"></asp:TextBox>
                                                <asp:Label ID="LblRequired13" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                    </div>

                                    <div class="col-1">
                                    </div>
                                    <div class="col-2">
                                        
                                    </div>

                                  


                                </div>


                               <div class="col-12">
                                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                </div>

                            <div class="modal-footer">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btncancel" runat="server" data-dismiss="modal" Text="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                      
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
                <div class="col-4"><strong style="font-family: Arial; font-size: 19px; color: white;">Employee Tax Rate Details</strong></div>
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
                                            <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9"></asp:Label>
                                        </ContentTemplate>  
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <div class="form-group row">
                                        <label class="col-md-6">Payroll Year : </label>
                                        <div class="col-md-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                   <asp:TextBox ID="txtb_payroll_year" runat="server" CssClass="form-control form-control-sm text-center" Enabled="false" Style="font-weight: bold;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4" style="margin-bottom:-5px">
                                </div>
 
                                <div class="col-2 text-right">
                               </div>
                                 <div class="col-3">

                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-3">
                                            <label>Department: </label>
                                        </div>
                                        <div class="col-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                   <asp:TextBox ID="txtb_department" runat="server" CssClass="form-control form-control-sm text-left" Enabled="false" Style="font-weight: bold;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-5">
                                             <label>Basic Tax:</label>
                                        </div>
                                        <div class="col-7">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <span style="position:absolute;float:right;right:10%;padding-top:2px; padding-right:5px;color:lightgray;">| %</span>
                                                   <asp:TextBox ID="txtb_bus_tax_hdr" runat="server" CssClass="form-control form-control-sm text-center" Enabled="false" Style="font-weight: bold;padding-right:27px;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-1 text-left" style="padding-left:0px;margin-left:-10px;" >
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                              
                                            <% if (ViewState["page_allow_add"].ToString() == "1")
                                                {  %>
                                                      <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success btn-sm save-icon icn btn-block" OnCommand="generateRow_Command" CommandArgument="generate" Style="color:white; width:120%;visibility:hidden" Text="Generate" />
                                            <% }
                                                %>     
                                        </ContentTemplate>
                                       
                                    </asp:UpdatePanel>
                                </div>--%>
                                 <div class="col-3">

                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <a ID="lb_back" class="btn btn-info btn-sm font-weight-bold" href="/View/cEmployeeTaxRate/cEmployeeTaxRate.aspx" ><i class="fa fa-arrow-left"></i> Back To Tax Rate Header</a>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-3">
                                            <label>Name: </label>
                                        </div>
                                        <div class="col-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                   <asp:TextBox ID="txtb_empl_name_hdr" runat="server" CssClass="form-control form-control-sm text-left" Enabled="false" Style="font-weight: bold;"></asp:TextBox>
                                                    <asp:TextBox ID="txtb_empl_id_hdr" runat="server" CssClass="form-control form-control-sm text-center" Visible="false" Enabled="false" Style="font-weight: bold;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-5">
                                             <label>Addl. Tax:</label>
                                        </div>
                                        <div class="col-7">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <span style="position:absolute;float:right;right:10%;padding-top:2px; padding-right:5px;color:lightgray;">| %</span>
                                                   <asp:TextBox ID="txtb_w_tax" runat="server" CssClass="form-control form-control-sm text-center" Enabled="false" Style="font-weight: bold;padding-right:27px;"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-1 text-right" style="padding-left:0px;margin-left:-10px;" >
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                        <ContentTemplate>
                                            
                                            <% if (ViewState["page_allow_add"].ToString() == "1")
                                                {  %>
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm add-icon icn btn-block"  Text="Add" OnClick="btnAdd_Click" style="visibility:hidden" />
                                            <% }
                                                %>     
                                        </ContentTemplate>
                                       
                                    </asp:UpdatePanel>
                                </div>--%>
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
                                                <asp:TemplateField HeaderText="VOUCHER NBR" SortExpression="voucher_nbr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("voucher_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="PERIOD COVERED" SortExpression="payroll_period_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("payroll_period_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DESCRIPTION" SortExpression="payrolltemplate_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("payrolltemplate_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="GROSS PAY" SortExpression="gross_pay">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("gross_pay") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="RIGHT" />
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
                                                                    <asp:ImageButton 
                                                                        ID="imgbtn_editrow1" 
                                                                        CssClass="btn btn-primary action" 
                                                                        EnableTheming="true"  
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/void.png"
                                                                        OnCommand="editRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("effective_date") + "," + Eval("voucher_nbr")%>'
                                                                        tooltip="View"
                                                                        />
                                                                        
                                                        
                                                                <%   }
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
                                       
                                        <%--<asp:AsyncPostBackTrigger ControlID="ddl_subdep" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_division" />
                                        <asp:AsyncPostBackTrigger ControlID="ddl_section" />--%>
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
        };
        
        

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
            $('#AddEditConfirm').modal({
                keyboard: false,
                backdrop: "static"
            });
            setTimeout(function () {
                $('#AddEditConfirm').modal("hide");
                $('.modal-backdrop.show').remove();

            }, 800);

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
