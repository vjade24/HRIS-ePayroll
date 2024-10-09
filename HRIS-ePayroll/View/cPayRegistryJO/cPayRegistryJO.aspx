<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayRegistryJO.aspx.cs" Inherits="HRIS_ePayroll.View.cPayRegistryJO" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>

        <!-- The Modal - Select Report -->
        <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal fade" id="SelectReport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #28a745;color: white;">
                                <h5 class="modal-title" ><asp:Label ID="notify_header" runat="server" Text="Report Options"></asp:Label></h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body with-background">
                                        <div class="row">
                                            <div class="col-12" style="margin-bottom: 5px;">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label  runat="server" Text="Select Report:"></asp:Label>
                                                        <asp:DropDownList ID="ddl_select_report" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_select_report_SelectedIndexChanged">
                                                            <asp:ListItem Value="01" Text="Payslip"></asp:ListItem>
                                                            <asp:ListItem Value="02" Text="Net Take Home Pay"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-8">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label  runat="server" Text="OR Number:" ID="lbl_or_nbr" Visible="false"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtb_or_nbr" CssClass="form-control form-control-sm" Width="100%" MaxLength="25" Visible="false"></asp:TextBox>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-4">
                                                <asp:UpdatePanel runat="server" ID="Update_or_date" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label  runat="server" Text="OR Date:" ID="lbl_or_date" Visible="false"></asp:Label>
                                                        <asp:TextBox runat="server" ID="txtb_or_date" CssClass="form-control form-control-sm my-date text-center" MaxLength="10" Width="100%" Visible="false"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row mt-1" >
                                            <div class="col-12">
                                                <asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkPrint_Click"  OnClientClick="openLoading();"> <i class="fa fa-print"></i> Print </asp:LinkButton>
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
                                        <h2 id="deleteRec0" runat="server">Delete this Record</h2>
                                        <h6><asp:Label ID="deleteRec1" runat="server" Text="Are you sure to delete this Record"></asp:Label></h6>
                                        <asp:Label ID="lbl_unposting" runat="server" CssClass="text-left" Text="Reason for UnPosting :"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtb_reason" CssClass="form-control form-control-sm" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired201"></asp:Label>
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

       <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- Modal Add/EditPage-->
                <div class="modal fade" id="add" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered  modal-2lg" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)" >
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <h5 class="modal-title" id="AddEditPage"><asp:Label ID="LabelAddEdit" runat="server" Text="Add/Edit Page" forecolor="White"></asp:Label>
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_post_status" runat="server" CssClass="badge badge-danger" forecolor="White" Font-Size="Small"></asp:Label>
                                </h5>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        </div>
                        <div class="modal-body with-background" style="padding-top: 5px;" runat="server" >
                            <div class="row" style="margin-top:-15px; display:none">
                                <div class="col-12 text-right" >
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" ></asp:Label>
                                            <asp:Label ID="lbl_registry_number" runat="server" Text="" Font-Underline="true" CssClass="font-weight-bold" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            
                            
                                <div class="col-3">
                                    <div style="float:right;width:65%;margin-left:5px;">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtb_status" CssClass="form-control form-control-sm text-center" Width="100%" Enabled="false" ></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:Label runat="server" style="float:right;padding-top:2px;" Text="Status:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" style="padding-right:0px;">
                                    <asp:Label runat="server" Text="Date Posted:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" style="padding-right:0px;padding-left:0px;">
                                    <asp:UpdatePanel runat="server" >
                                        <ContentTemplate>
                                            <%--<asp:TextBox runat="server" ID="txtb_date_posted" CssClass="form-control form-control-sm my-date" MaxLength="15"  Width="100%"></asp:TextBox>--%>
                                            <asp:TextBox runat="server" ID="txtb_date_posted" CssClass="form-control form-control-sm text-center" MaxLength="15"  Width="100%" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-5">
                                    <div style="float:right;width:45%;margin-left:5px;">
                                        <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_voucher_nbr" CssClass="form-control form-control-sm text-center" MaxLength="15" Width="100%"></asp:TextBox>
                                            <asp:Label runat="server" CssClass="lbl_required" ID="LblRequired200"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                    <asp:Label runat="server" style="float:right;padding-top:2px;" Text="Voucher Nbr :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-3">
                                     <asp:Label ID="Label2" runat="server" Text="Employee's Name:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-6" >
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" Width="100%" OnTextChanged="ddl_empl_id_TextChanged" AutoPostBack="true" ></asp:DropDownList>
                                            <asp:TextBox runat="server" ID="txtb_employeename" CssClass="form-control form-control-sm" Enabled="false" Font-Bold="true"></asp:TextBox> 
                                            <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-1" style="padding-right: 0px;">
                                     <asp:Label ID="Label3" runat="server" Text="ID No:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" >
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server"  ID="txtb_empl_id" CssClass="form-control form-control-sm font-weight-bold text-center"  Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-3" >
                                     <asp:Label ID="Label4" runat="server" Text="Position Title :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-9" >
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server"  ID="txtb_position" CssClass="form-control form-control-sm font-weight-bold "  Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Department:</label>
                                    <%--<a href="#demo" data-toggle="collapse" id="btn_show">Department <small>(Show)</small>:</a>--%>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdateDep" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server"  ID="txtb_department_name1" CssClass="form-control form-control-sm font-weight-bold "  Enabled="false"></asp:TextBox>
                                            <%--<asp:DropDownList ID="ddl_dep" runat="server" Enabled="false" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_SelectedIndexChanged"></asp:DropDownList>--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                            </div>
                            <%--<div class="row collapse" id="demo">
                                <div class="col-3">
                                    <label class="font-weight-bold">Sub-Department:</label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_subdep" runat="server"  Enabled="false" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subdep_SelectedIndexChanged"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Division:</label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_division" runat="server"  Enabled="false" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Section:</label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_section" runat="server" Enabled="false"  CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">Fund Charges :</label>
                                </div>
                                <div class="col-9">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_fund_charges" runat="server" Enabled="false" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>--%>
                            
                            <div class="row"  >
                                
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_rate_basis_descr" CssClass="font-weight-bold" runat="server" Text="Rate:"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" ID="Update_rate_amount" >
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_rate_amount" onInput="search_for_days(event);"  CssClass="form-control form-control-sm text-right" OnTextChanged="txtb_rate_amount_TextChanged"></asp:TextBox>
                                            <asp:Label ID="LblRequired2000" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                            <script type="text/javascript">
                                                function search_for_days(key)
                                                {
                                                    __doPostBack("<%= txtb_rate_amount.ClientID %>", "");
                                                }
                                            </script>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_worked" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_days_worked" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_lates_and_undertime" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_rate_amount" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="Lates and Und. (min.):"></asp:label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" ID="Update_lates_and_undertime" >
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_lates_and_undertime" onInput="search_for_days(event);"  CssClass="form-control form-control-sm text-right" OnTextChanged="txtb_lates_and_undertime_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:Label ID="LblRequired2" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                            <script type="text/javascript">
                                                function search_for_days(key)
                                                {
                                                    __doPostBack("<%= txtb_lates_and_undertime.ClientID %>", "");
                                                }
                                            </script>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_worked" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_days_worked" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_lates_and_undertime" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:3px" />
                                </div>

                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="No. of Working Days:"></asp:label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_working_2nd" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                

                                <div class="col-3">
                                    <label class="font-weight-bold">No. of Days Worked:</label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" ID="Update_days_worked" >
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_no_days_worked" onInput="search_for_days(event);" CssClass="form-control form-control-sm text-right" OnTextChanged="txtb_no_days_worked_TextChanged" AutoPostBack="true"></asp:TextBox>
                                             <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        <script type="text/javascript">
                                            function search_for_days(key) {
                                                    __doPostBack("<%= txtb_no_days_worked.ClientID %>", "");
                                            }
                                        </script>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_worked" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_days_worked" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_lates_and_undertime" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="No. of Days Absent:"></asp:label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_absent_2nd" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                            <asp:Label ID="LblRequired_no_absent_2nd" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <label class="font-weight-bold">No. of Hours Worked:</label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" ID="Update_hours_worked" >
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_no_hours_worked" onInput="search_for_hours(event);" CssClass="form-control form-control-sm text-right" OnTextChanged="txtb_no_hours_worked_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        <script type="text/javascript">
                                            function search_for_hours(key) {
                                                    __doPostBack("<%= txtb_no_hours_worked.ClientID %>", "");
                                            }
                                        </script>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_worked" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_days_worked" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_lates_and_undertime" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_sal" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="No. of Hours (OT):"></asp:label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server"  ChildrenAsTriggers="false" UpdateMode="Conditional" ID="Update_txtb_no_hours_ot">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_hours_ot" CssClass="form-control form-control-sm text-right" OnTextChanged="txtb_no_hours_ot_TextChanged" AutoPostBack="true" onInput="search_txtb_no_hours_ot(event);" ></asp:TextBox>
                                            <asp:Label ID="LblRequired_txtb_no_hours_ot" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        <script type="text/javascript">
                                            function search_txtb_no_hours_ot(key) {
                                                    __doPostBack("<%= txtb_no_hours_ot.ClientID %>", "");
                                            }
                                        </script>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_ot_amt" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_sal_amt" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="Amount (OT):"></asp:label>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_hours_ot_amt" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="No. of Hours (Salary):"></asp:label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" ID="Update_txtb_no_hours_sal">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_hours_sal" CssClass="form-control form-control-sm text-right" OnTextChanged="txtb_no_hours_sal_TextChanged" AutoPostBack="true" onInput="search_txtb_no_hours_sal(event);" ></asp:TextBox>
                                            <asp:Label ID="LblRequired_txtb_no_hours_sal" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                            <script type="text/javascript">
                                            function search_txtb_no_hours_sal(key) {
                                                    __doPostBack("<%= txtb_no_hours_sal.ClientID %>", "");
                                            }
                                        </script>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_ot_amt" />
                                            <asp:AsyncPostBackTrigger ControlID="txtb_no_hours_sal_amt" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="Amount (Salary):"></asp:label>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_hours_sal_amt" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>


                            </div>

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="row" runat="server" id="div_1st_quincena_nbr">
                                        <div class="col-12">
                                            <hr style="margin-top:5px;margin-bottom:3px" />
                                        </div>
                                        <div class="col-12">
                                            <label class="font-weight-bold  text-danger">1st Quincena (No. of Days)</label>
                                        </div>
                                          
                                        <div class="col-3">
                                            <asp:label runat="server" Font-Bold="true" Text="No. of Working Days:"></asp:label>
                                        </div>
                                        <div class="col-3 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate> 
                                                    <asp:TextBox runat="server" ID="txtb_no_working_1st" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                
                                        <div class="col-3">
                                            <asp:label runat="server" Font-Bold="true" Text="No. of Days Worked:"></asp:label>
                                        </div>
                                        <div class="col-3 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate> 
                                                    <asp:TextBox runat="server" ID="txtb_no_worked_1st" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-3">
                                            <asp:label runat="server" Font-Bold="true" Text="No. of Days Absent:"></asp:label>
                                        </div>
                                        <div class="col-3 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate> 
                                                    <asp:TextBox runat="server" ID="txtb_no_absent_1st" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-3 ">
                                            <asp:label runat="server" Font-Bold="true" Text="No. of Hours Worked:"></asp:label>
                                        </div>
                                        <div class="col-3 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate> 
                                                    <asp:TextBox runat="server" ID="txtb_no_hours_worked_1st" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <%--<div class="col-6"></div>
                                        <div class="col-3 ">
                                            <asp:label runat="server" Font-Bold="true" Text="No. of Hours (OT):"></asp:label>
                                        </div>
                                        <div class="col-3 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate> 
                                                    <asp:TextBox runat="server" ID="txtb_no_hours_ot_010" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>--%>
                                        
                                        
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:5px" />
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="No. of Hours (OT):"></asp:label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_hours_ot_010" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="Amount (OT):"></asp:label>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_hours_ot_amt_010" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="No. of Hours (Salary):"></asp:label>
                                </div>
                                <div class="col-3 text-right">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_hours_sal_010" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:label runat="server" Font-Bold="true" Text="Amount (Salary):"></asp:label>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate> 
                                            <asp:TextBox runat="server" ID="txtb_no_hours_sal_amt_010" CssClass="form-control form-control-sm text-right" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row" runat="server">
                                <div class="col-12">
                                    <hr style="margin-top:5px;margin-bottom:3px" />
                                </div>
                                <div class="col-4">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12" style="padding-top:0px;padding-bottom:2px;">
                                                    <strong>Summary Total</strong>
                                                </div>
                                            </div>
                                           <%-- <hr style="margin-top:2px;margin-bottom:2px;" />--%>
                                            <div class="form-group row">
                                                <div class="col-6">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Gross Pay :"></asp:label>
                                                </div>
                                                <div class="col-6">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Width="100%" Font-Bold="true" Font-Size="Small" Enabled="false" Text="0.000" ID="txtb_gross_pay" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired5" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Lates Amount:"></asp:label>
                                                </div>
                                                <div class="col-6 text-right">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" Font-Size="Small" Enabled="false" ID="txtb_less" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired6" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Amount Due:"></asp:label>
                                                </div>
                                                <div class="col-6 text-right">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" Font-Size="Small" Enabled="false" ID="txtb_amount_due" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                            <asp:TextBox runat="server" Font-Bold="true" Font-Size="Small" Enabled="false" ID="txtb_amount_due_hidden" Text="0.000" CssClass="form-control form-control-sm text-right" Visible="false"></asp:TextBox>
                                                                <asp:Label ID="Label5" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6 ">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Mandatory :"></asp:label>
                                                </div>
                                                <div class="col-6 text-right">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" Font-Size="Small" Enabled="false" ID="txtb_total_mandatory" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired7" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Optional :"></asp:label>
                                                </div>
                                                <div class="col-6 text-right">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" Font-Size="Small" Enabled="false" ID="txtb_total_optional" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired8" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Loans :"></asp:label>
                                                </div>
                                                <div class="col-6 text-right">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" Font-Size="Small" Enabled="false" ID="txtb_total_loans" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired9" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Net Pay :"></asp:label>
                                                </div>
                                                <div class="col-6 text-right">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" Font-Size="Small" Enabled="false" ID="txtb_net_pay" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                            <asp:Label ID="LblRequired10" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6" style="display:none;">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Net Pay (1st Half) :"></asp:label>
                                                </div>
                                                <div class="col-6 text-right" style="display:none;">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" ID="txtb_net_pay_1h" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired11" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6" style="display:none;">
                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Net Pay (2nd Half) :"></asp:label>
                                                </div>
                                                <div class="col-6 text-right" style="display:none;">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" ID="txtb_net_pay_2h" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btn_calculate" OnClick="btn_calculate_Click" CssClass="btn btn-block btn-warning btn-sm" runat="server" Text="Calculate" />
                                                    <asp:Label ID="LblRequired1000" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-8">
                                    <div class="row" style="margin-bottom:3px;margin-right: 0px !important;"> 
                                        <div class="col-12">
                                            <ul class="nav nav-tabs" style="border-bottom: 1px solid #dee2e6" id="myTab" role="tablist">
                                                <li class="nav-item">
                                                    <a class="nav-link active" style="padding: .09rem 1rem;" id="id_mandatory" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab" aria-selected="false">MANDATORY</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" style="padding: .09rem 1rem;" id="id_optional" data-toggle="tab" href="#date_config_tab" role="tab" aria-controls="date_config_tab" aria-selected="false">OPTIONAL</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" style="padding: .09rem 1rem;" id="id_loans" data-toggle="tab" href="#loans_tab" role="tab" aria-controls="loans_tab" aria-selected="false">LOANS</a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="tab-content"  style="width:100%" id="myTabContent">

                                            <%--MANDATORY DEDUCTIONS--%>
                                            <div class="tab-pane show fade active" id="mandatory_tab" role="tabpanel" style="border: 1px solid green;min-height:180px;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;" aria-labelledby="name_of_employee_link">
                                                <div class="row" style="margin-top:5px;" >
                                                    <div class="col-6">
                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="HDMF - GS"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_gs" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired12" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="HDMF - PS"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_ps" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired13" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="PHIC - GS"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_phic_gs" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired14" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="PHIC - PS"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_phic_ps" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired15" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true"  Text="BIR Tax(2%)"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_bir_tax_2percent" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired16" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="row form-group">
                                                            <div class="col-6">
                                                                <% if (Convert.ToDateTime(ddl_year.SelectedValue + "-" + ddl_month.SelectedValue + "-" + "01") > Convert.ToDateTime("2021-03-30") &&
                                                                       Convert.ToDateTime(ddl_year.SelectedValue + "-" + ddl_month.SelectedValue + "-" + "01") < Convert.ToDateTime("2023-07-01"))
                                                                    {
                                                                %>
                                                                   <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="BIR Tax(1%)"></asp:label>
                                                                <% 
                                                                    }
                                                                    else
                                                                    {
                                                                %> 
                                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="BIR Tax(3%)"></asp:label>
                                                                <%
                                                                    }
                                                                %>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_bir_tax_3percent" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired17" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="BIR Tax(5%)"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_bir_tax_5percent" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired18" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <% if (Convert.ToDateTime(ddl_year.SelectedValue + "-" + ddl_month.SelectedValue + "-" + "01") > Convert.ToDateTime("2021-03-30") &&
                                                                       Convert.ToDateTime(ddl_year.SelectedValue + "-" + ddl_month.SelectedValue + "-" + "01") < Convert.ToDateTime("2023-07-01"))
                                                                    {
                                                                %>
                                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="BIR Tax(8%)"></asp:label>
                                                                <% 
                                                                    }
                                                                    else
                                                                    {
                                                                %> 
                                                                    <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="BIR Tax(5% VAT)"></asp:label>
                                                                <%
                                                                    }
                                                                %>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_bir_tax_8percent" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired19" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="BIR Tax(10%)"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_bir_tax_10percent" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired20" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="BIR Tax(15%)"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_bir_tax_15percent" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired21" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <%-- BEGIN - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand3"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand4"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand4"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand5"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand5"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand6"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand6" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand6"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand7"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand7" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand8"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand8" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand8"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand9"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand9" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand9"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_mand10"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_mand10" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_mand10"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <%-- END   - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--OPTIONAL DEDUCTIONS--%>
                                            <div class="tab-pane fade" id="date_config_tab" role="tabpanel" style="border: 1px solid green;min-height:180px;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;" aria-labelledby="date_config_link">
                                                <div class="row" style="margin-top:5px;">
                                                    <div class="col-6">
                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="SSS"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_sss" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired22" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="HDMF-Add PS"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate> 
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_addl" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired23" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="HDMF- MP2"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate> 
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_mp2" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired37" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Philam Life"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_philam" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired24" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="Loyalty Card"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_loyalty_card" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired45" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-6">
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group row">
                                                                    <div class="col-6"  runat="server"  id="div_lbl_prem1">
                                                                        <asp:label runat="server" ID="lbl_other_prem1" Font-Size="Small" Font-Bold="true" Text="(Reserved1):"></asp:label>
                                                                    </div>
                                                                    <div class="col-6 text-right"  runat="server"  id="div_txt_prem1"  >
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox runat="server" ID="txtb_otherpremium_no1" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                                <asp:Label ID="LblRequired25" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-6"  runat="server"  id="div_lbl_prem2">
                                                                        <asp:label runat="server" ID="lbl_other_prem2" Font-Size="Small" Font-Bold="true" Text="(Reserved2):"></asp:label>
                                                                    </div>
                                                                    <div class="col-6 text-right"  runat="server"  id="div_txt_prem2">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox runat="server" ID="txtb_otherpremium_no2" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                                <asp:Label ID="LblRequired26" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-6" runat="server" id="div_lbl_prem3">
                                                                        <asp:label runat="server" ID="lbl_other_prem3" Font-Size="Small" Font-Bold="true" Text="(Reserved3):"></asp:label>
                                                                    </div>
                                                                    <div class="col-6 text-right" runat="server"  id="div_txt_prem3">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox runat="server" ID="txtb_otherpremium_no3" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                                <asp:Label ID="LblRequired27" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>

                                                                     <%-- BEGIN   - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem3"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem4"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem4"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem5"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem5"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem6"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem6" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem6"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem7"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem7" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem8"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem8" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem8"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem9"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem9" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem9"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_prem10"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_prem10" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_prem10"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                       <%-- END     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                            
                                                </div>
                                            </div>

                                            <%--LOANS--%>
                                            <div class="tab-pane fade" id="loans_tab" role="tabpanel" style="border: 1px solid green;min-height:180px;padding-bottom:5px;border-radius:0px 0px 5px 5px;padding-left:10px;padding-right:10px;" aria-labelledby="id_loans">
                                                <div class="row" style="padding-top:10px">
                                                    <div class="col-6">
                                                        <div class="form-group row">
                                                            <div class="col-6">
                                                                <asp:label runat="server"  Font-Size="Small" Font-Bold="true" Text="HDMF MPL"></asp:label>
                                                            </div>
                                                            <div class="col-6 text-right">
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox runat="server" ID="txtb_hdmf_mpl_loan" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                        <asp:Label ID="LblRequired28" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server"  Font-Size="Small" Font-Bold="true" Text="HDMF Housing"></asp:label>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_hdmf_house_loan" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired29" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server" Font-Size="Small" Font-Bold="true" Text="HDMF Calamity"></asp:label>
                                                        </div>
                                                        <div class="col-6 text-right">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_hdmf_cal_loan" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired30" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server"  Font-Size="Small" Font-Bold="true" Text="CCMPC Loan"></asp:label>
                                                        </div>
                                                        <div class="col-6 text-right">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_ccmpc_loan" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired31" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:label runat="server"  Font-Size="Small" Font-Bold="true" Text="NICO Loan"></asp:label>
                                                        </div>
                                                        <div class="col-6 text-right">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_nico_loan" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label ID="LblRequired32" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <%-- BEGIN     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan3"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan4"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan4"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan5"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan5"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan6"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan6" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan6"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan7"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan7" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan8"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan8" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan8"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan9"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan9" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan9"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan10"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="padding:0px;padding-right:5px" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan10" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan10"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                       <%-- END     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                    </div>
                                                    </div>

                                                    <div class="col-6">
                                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group row">
                                                                    <div class="col-6">
                                                                        <asp:label runat="server"  Font-Size="Small" Font-Bold="true" Text="Network Bank"></asp:label>
                                                                    </div>
                                                                    <div class="col-6 text-right">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate> 
                                                                                <asp:TextBox runat="server" ID="txtb_networkbank_loan" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                                <asp:Label ID="LblRequired33" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-6">
                                                                        <asp:label runat="server"  Font-Size="Small" Font-Bold="true" Text="Uniform"></asp:label>
                                                                    </div>
                                                                    <div class="col-6 text-right">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate> 
                                                                                <asp:TextBox runat="server" ID="txtb_uniform" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                                <asp:Label ID="LblRequired44" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-6"  runat="server"  id="div_lbl_loan1">
                                                                        <asp:label runat="server" ID="lbl_other_loan1"  Font-Size="Small" Font-Bold="true" Text="(Reserved1):"></asp:label>
                                                                    </div>
                                                                    <div class="col-6 text-right"  runat="server"  id="div_txt_loan1">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox runat="server" ID="txtb_otherloan_no1" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                                <asp:Label ID="LblRequired34" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-6"  runat="server"  id="div_lbl_loan2">
                                                                        <asp:label runat="server" ID="lbl_other_loan2"  Font-Size="Small" Font-Bold="true" Text="(Reserved2):"></asp:label>
                                                                    </div>
                                                                    <div class="col-6 text-right"  runat="server"  id="div_txt_loan2">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox runat="server" ID="txtb_otherloan_no2" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                                <asp:Label ID="LblRequired35" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-6"  runat="server"  id="div_lbl_loan3">
                                                                        <asp:label runat="server" ID="lbl_other_loan3"  Font-Size="Small" Font-Bold="true" Text="(Reserved3):"></asp:label>
                                                                    </div>
                                                                    <div class="col-6 text-right"  runat="server"  id="div_txt_loan3">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox runat="server" ID="txtb_otherloan_no3" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                                <asp:Label ID="LblRequired36" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>

                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                </div>
                                                <%--<div class="row" style="display:none">
                                                    <div class="col-6">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="btn_editloan" runat="server" CssClass="btn btn-primary btn-sm btn-block" Text="" OnClick="btn_editloan_Click">
                                                                    <i class="fa fa-edit"></i> 
                                                                    Edit Loan
                                                                </asp:LinkButton>
                                                                    
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-6">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="btn_contributions" runat="server" CssClass="btn btn-primary btn-sm btn-block" OnClick="btn_contributions_Click">
                                                                    <i class="fa fa-edit"></i> 
                                                                    Edit Contributions
                                                                </asp:LinkButton>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-6" style="padding:0px !important">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_if_dateposted_yes" CssClass="col-form-label-sm text-danger" Font-Bold="true" runat="server" Text="" style="font-size:12px"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6 text-right">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                     <asp:HiddenField ID="hidden_rate_basis" runat="server" />
                                                     <asp:HiddenField ID="hidden_hrs_in1day" runat="server" />
                                                     <asp:HiddenField ID="hidden_monthly_days" runat="server" />
                                                     <asp:HiddenField ID="hidden_daily_rate" runat="server" />
                                                     <asp:HiddenField ID="hidden_hourly_rate" runat="server" />
                                                     <asp:HiddenField ID="hidden_monthly_rate" runat="server" />
                                                     <asp:HiddenField ID="lbl_lastday_hidden" runat="server" />

                                                <%--<asp:Button ID="btn_show_headers" OnClick="btn_show_headers_Click" runat="server" Text="Button" />--%>
                                                <asp:LinkButton ID="Linkbtncancel"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                                <asp:Button ID="btnSave" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    
                                </div>
                               
                                
                           </div>
                            <div class="row">
                                
                                <div class="col-5 text-right">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="notification">
            <!-- The Modal - Message -->
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center">
                <div class="modal-header">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <h5 class="modal-title" ><asp:Label ID="Label6" runat="server" Text=""></asp:Label></h5>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                </div>
                <!-- Modal body -->
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <i id="msg_icon" runat="server" class="fa fa-check-circle-o"></i>
                        <h4 id="msg_header" runat="server">Header Message</h4>
                        <asp:Label ID="lbl_details" runat="server" Text=""></asp:Label>
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
                <div class="col-3">
                    <strong style="font-family:Arial;font-size:18px;color:white;">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <%: ((ddl_payroll_template.SelectedItem.Text.ToString().Trim().Length-3) >22 ?  ddl_payroll_template.SelectedItem.Text.ToString().Substring(0,19)+"...":ddl_payroll_template.SelectedItem.Text.ToString().Trim())%>
                            </ContentTemplate>
                        </asp:UpdatePanel> 
                    </strong>
                </div>
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
                <%--<div class="col-2 text-right">
                   <asp:UpdatePanel runat="server">
                       <ContentTemplate>
                           <asp:Button ID="btn_save_group" runat="server" CssClass="btn btn-success btn-sm add-icon icn"  Text="Save Group"/>
                       </ContentTemplate>
                   </asp:UpdatePanel>
                </div>--%>
            </div>
        </div>
    <div class="row">
        <div class="col-12">
            <table class="table table-bordered  table-scroll">
                <tbody class="my-tbody">
                    <tr>
                        <td>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                        <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm form-control" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" ToolTip="Show entries per page">
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
                                        <div class="col-3">
                                            <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9"></asp:Label>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group row">
                                        <div class="col-6">
                                                <asp:Label runat="server"  Text="Payroll Year:" ></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group row">
                                        <div class="col-4">
                                                <asp:Label runat="server" Text="Month:" ></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control"  AutoPostBack="true">
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
                                <div class="col-lg-4">
                                    <div class="form-group row">
                                        <div class="col-5">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-7">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" CssClass="form-control-sm form-control" ID="ddl_empl_type">
                                                        <asp:ListItem Text="Job-Order Employees" Value="JO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" ></asp:DropDownList>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3"></div>
                                <div class="col-lg-9">
                                    <div class="form-group row">
                                        <div class="col-2" style="padding-right:0px;">
                                            <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_template" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <a ID="lb_back" class="btn btn-info btn-sm font-weight-bold" href="/View/cPayRegistry/cPayRegistry.aspx" ><i class="fa fa-arrow-left"></i> Back To Payroll Registry</a>
                                            <asp:Button ID="btn_edit_hidden" runat="server" Text="Edit Hidden" CssClass="btn btn-secondary btn-sm" OnClick="btn_edit_hidden_Click"  style="display:none"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <asp:Label runat="server" Text="Payroll Group:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_group" runat="server" CssClass="form-control-sm form-control"  AutoPostBack="true" style="float:right;width:97.5%" Enabled="false"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                        <ContentTemplate>
                                            
                                            <% if (ViewState["page_allow_add"].ToString() == "1")
                                                {  %>
                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn btn-block"  Text="Add" OnClick="btnAdd_Click" />
                                            <% }
                                                %>     
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_payroll_group" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_payroll_template" />
                                        </Triggers>
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
                                                <asp:TemplateField HeaderText="ID NO" SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%# Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="POSITION TITLE" SortExpression="position_title1">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%# Eval("position_title1") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("net_pay") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="POST STATUS" SortExpression="post_status">
                                                    <ItemTemplate>
                                                        <%# Eval("post_status_descr")%>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="imgbtn_editrow1" 
                                                                        CssClass="btn btn-primary action" 
                                                                        EnableTheming="true"  
                                                                        runat="server" 
                                                                        ImageUrl='<%# Session["ep_post_authority"].ToString() == "0" ? "~/ResourceImages/final_edit.png" : "~/ResourceImages/card.png" %>'  
                                                                        OnCommand="editRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_registry_nbr") + "," + Eval("payroll_year")%> ' 
                                                                        tooltip='<%# Session["ep_post_authority"].ToString() == "0" ? "Edit Existing Record" : "Post to Card" %>' />
                                                        
                                                                <%  }
                                                                %>
                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton ID="lnkDeleteRow" 
                                                                        CssClass="btn btn-danger action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl='<%# Session["ep_post_authority"].ToString() == "1" ? "~/ResourceImages/unpost.png" : "~/ResourceImages/final_delete.png" %>' 
                                                                        OnCommand="deleteRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_registry_nbr") + "," + Eval("payroll_year")%> ' 
                                                                        Enabled='<%# Eval("post_status").ToString() == "Y" && Session["ep_post_authority"].ToString() == "0" || Eval("post_status").ToString() == "N" && Session["ep_post_authority"].ToString() == "1" ||  Eval("post_status").ToString() == "R" ? false : true %>'
                                                                        ToolTip='<%# Session["ep_post_authority"].ToString() == "0" ? "Delete Existing Record" : "UnPost From Card" %>'/>
                                                                <% }
                                                                %>
                                                                <% if (ViewState["page_allow_print"].ToString() == "1")
                                                                    {
                                                                %>
                                                                     <asp:ImageButton ID="imgbtn_print" 
                                                                         CssClass="btn btn-success action" 
                                                                         EnableTheming="true" runat="server" 
                                                                         ImageUrl="~/ResourceImages/print1.png" 
                                                                         style="padding-left: 0px !important;display:none !important" 
                                                                         onCommand="imgbtn_print_Command"
                                                                         CommandArgument='<%# Eval("empl_id") + "," + Eval("payroll_registry_nbr") + "," + Eval("payroll_year")%> ' 
                                                                         tooltip="Print"/>
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
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
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
        function openModal() {
            $('#add').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
       function show_date()
        {
            $('#<%= txtb_or_date.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
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
        function openNotification() {
            $('#notification').modal({
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
        function openNotification1() {
            $('#notification1').modal({
                keyboard: false,
                backdrop:"static"
            });
       };
    </script>
    <script type="text/javascript">
        function closeNotification1()
        {
            $('#notification1').modal("dispose");
            $(Window).find('.modal-backdrop.fade.show')[1].remove();
        };
    </script>
    <script type="text/javascript">
        function click_tab(target_tab)
    {
        if (target_tab == 1)
        {
            $('#id_mandatory').click()
        }
        else if (target_tab == 2)
        {
            $('#id_optional').click()
        }
        else if (target_tab == 3)
        {
            $('#id_loans').click()
        }
    }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
    <script>
        $( document ).ready(function() {
            $('#<%= btn_edit_hidden.ClientID%>').click();
            $('#id_loans').click();
        });
        function openSelectReport() {
            $('#SelectReport').modal({
                keyboard: false,
                backdrop:"static"
            });
         };
        function openLoading() {

            $('#Loading').modal({
                keyboard: false,
                backdrop: "static"
            });
        }
    </script> 
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

