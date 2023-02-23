<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayHazardSubsistenceLaundry.aspx.cs" Inherits="HRIS_ePayroll.View.cPayHazardSubsistenceLaundry" %>
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
                                        <h2 id="deleteRec0" runat="server">Delete this Record</h2>
                                        <h6><asp:Label ID="deleteRec1" runat="server" Text=""></asp:Label></h6>
                                        <asp:Label ID="lbl_unposting" runat="server" Text=""></asp:Label>
                                        <asp:TextBox runat="server" ID="txtb_reason"  visible="false" CssClass="form-control form-control-sm text-center" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                            <asp:Label ID="LblRequired201" CssClass="lbl_required" runat="server" Text=""></asp:Label>
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
                    <div class="modal-dialog modal-dialog-centered  modal-lg" role="document">
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
                        <div class="modal-body with-background" style="padding-bottom:3px !important;padding-top:3px;" runat="server" >
                            <div class="row" style="margin-top:-15px;display:none;">
                                <div class="col-12 text-right" >
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" ></asp:Label>
                                            <asp:Label ID="lbl_registry_number" runat="server" Text="" Font-Underline="true" CssClass="font-weight-bold" ></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                             
                            <div class="row" style="display:none !important">
                                <div class="col-3">
                                    <div style="float:right;width:65%;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_status"  visible="true" CssClass="form-control form-control-sm text-center" Width="100%" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label17" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div> 
                                    <asp:Label runat="server" style="float:left;padding-top:2px;" Text="Status:" CssClass="font-weight-bold" ></asp:Label>
                               </div>
                                <div class="col-2" style="padding-right:0px;padding-top:5px;margin-left:10px;">
                                    <asp:Label runat="server" Text="Date Posted:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-2" style="padding-right:0px;padding-left:0px; margin-left:-10px;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                           <asp:TextBox runat="server" ID="txtb_date_posted"  CssClass="form-control form-control-sm text-center" MaxLength="15"  Width="100%" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-5">
                                    <div style="float:right;width:55%;">
                                        <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_voucher_nbr" CssClass="form-control form-control-sm text-center" MaxLength="15" Width="100%"></asp:TextBox>
                                            <asp:Label ID="LblRequired101" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                    <asp:Label runat="server" style="float:left;padding-top:2px; margin-right:5px;" MaxLength="15" Text="Voucher Nbr :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                             </div>
                            <div class="form-group row">
                                <div class="col-3">
                                     <asp:Label ID="Label2" runat="server" Text="Employee's Name:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-6" >
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_empl_id" runat="server" CssClass="form-control form-control-sm" Width="100%" OnTextChanged="ddl_empl_id_TextChanged" AutoPostBack="true" ></asp:DropDownList>
                                            <asp:TextBox ID="txtb_empl_name" Font-Bold="true"  CssClass="form-control form-control-sm" Width="100%" Enabled="false" Visible="false" runat="server"></asp:TextBox>
                                            <asp:Label ID="LblRequired1" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                             <asp:Label ID="Label11" runat="server" Text="ID:" CssClass="font-weight-bold" ></asp:Label>
                                             <asp:TextBox ID="txtb_empl_id" style="float:right;" Font-Bold="true"  CssClass="form-control form-control-sm text-center" Width="70%" Enabled="false" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                           </div>

                            <hr style="margin-top:5px;margin-bottom:5px" />
                            <div class="form-group row">
                                <div class="col-3">
                                     <asp:Label ID="Label21" runat="server" Text="Position Title:" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-9" >
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_position" Font-Bold="true"  CssClass="form-control form-control-sm" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3" style="display:none;">
                                    
                                </div>
                           </div>
                           <hr style="margin-top:5px;margin-bottom:5px" />
                            <div class="form-group row">
                                <div class="col-3">
                                     <asp:Label ID="Label15" runat="server" Text="Department :" CssClass="font-weight-bold" ></asp:Label>
                                </div>
                                <div class="col-9" >
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtb_department_descr" Font-Bold="true"  CssClass="form-control form-control-sm" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3" style="display:none;">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                             <asp:Label ID="Label18" runat="server" Text="Code:" CssClass="font-weight-bold" ></asp:Label>
                                             <asp:TextBox ID="txtb_department_code" style="float:right;" Font-Bold="true"  CssClass="form-control form-control-sm text-center" Width="70%" Enabled="false" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                           </div>
                           
                             <%--COMPENSATION SECTION--%>
                            <hr style="margin-top:5px;margin-bottom:5px" />
                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div_monthly">
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbl_rate_basis_descr" CssClass="font-weight-bold" runat="server" Text="Monthly Salary:"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Enabled="true" ID="txtb_monthly_rate" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                        <asp:Label ID="LblRequired2" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div12">
                                        <div class="col-6">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="Label19" CssClass="font-weight-bold" runat="server" Text="Daily Salary:"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtb_daily_rate" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                        <asp:Label ID="LblRequired3" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <%--END OF COMPENSATION SECTION--%>
                             
                            <%--DAYSWORKING SECTION--%>
                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div13"  visible="true">
                                        <div class="col-6" style="padding-right:0px;">
                                            <asp:label runat="server" Font-Bold="true" Text="Worked Days<small> <b style='font-size:9px;color:gray;'>(W/ S.hol,S3)</b></small>:"></asp:label>
                                        </div>
                                        <div class="col-6 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate> 
                                                    <asp:TextBox runat="server" ID="txtb_working_days" OnTextChanged="txtb_no_days_wordked_TextChanged" onKeydown="postBack_no_days(event)" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired4" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div_daily"  visible="true">
                                        <div class="col-6" style="padding-right:0px;">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                        <asp:label runat="server" Font-Bold="true" ID="lbl_worked_days" Text=""></asp:label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-6 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate> 
                                                    <asp:TextBox runat="server" ID="txtb_no_days_wordked" OnTextChanged="txtb_no_days_wordked_TextChanged" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                        <%--<script type="text/javascript">
                                                            function postBack_no_days(event)
                                                            {
                                                                if (event.keyCode == 13)
                                                                {
                                                                    __doPostBack("<%= txtb_no_days_wordked.ClientID %>", "");
                                                                }
                                                                }

                                                                function postBack_calculate()
                                                            {
                                                                    __doPostBack("<%= txtb_no_days_wordked.ClientID %>", "");
                                                            }
                                                        </script>--%>
                                                    <asp:Label ID="LblRequired7" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--END OF DAYS SETUP SECTION--%>
                            <hr style="margin-top:3px;margin-bottom:3px;" />
                            <%--HAZARD SECTION--%>
                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div8" visible="true" >
                                        <div class="col-6">
                                            <asp:label runat="server" ID="Label4" Font-Bold="true" Text="Hazard Rate Percent :"></asp:label>
                                        </div>
                                        <div class="col-6 ">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <span style="position:absolute;float:right;right:10%;padding-top:2px;color:lightgray;">| %</span>
                                                    <asp:TextBox runat="server" style="padding-right:27px;" Font-Bold="true" Enabled="false" ID="txtb_hazard_perc" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired8" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div4" visible="true">
                                        <div class="col-6">
                                            <asp:label runat="server" ID="Label10" Font-Bold="true" Text="Net Hazard:" ></asp:label>
                                        </div>
                                        <div class="col-6 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Font-Bold="true" ID="txtb_net_hazard_amount" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired14" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--END HAZARD SECTION--%>
                            <%--SUBSISTINCE SECTION--%>
                            <hr style="margin-top:3px;margin-bottom:3px;" />
                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6" style="padding-right:0px;">
                                            <asp:label runat="server" ID="lbl_gross_pay_descr"  style="letter-spacing:0.5px;font-size:15px;" Font-Size="Small" Font-Bold="true" Text="Subsistence Daily Rate:"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Width="100%" Font-Bold="true" Text="0.000" Enabled="false" ID="txtb_subs_daily_dspl" OnTextChanged="txtb_no_days_wordked_TextChanged"  CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired11" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group row" runat="server" id="div_hourly"  visible="true">
                                        <div class="col-6">
                                            <label class="font-weight-bold">No. Days Subsistence:</label>
                                        </div>
                                        <div class="col-6 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_no_days_subsistence" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired5" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div17" visible="true" >
                                        <div class="col-12" style="padding-top:9px;">&nbsp;</div>
                                    </div>
                                    <div class="form-group row" runat="server" id="div1" visible="true" >
                                        <div class="col-6 " style="padding-right:0px;">
                                            <asp:label runat="server" ID="Label3" Font-Bold="true"  Text="Net Subsistence:"></asp:label>
                                        </div>
                                        <div class="col-6 ">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Font-Bold="true" ID="txtb_net_subs_amount" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired13" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top:3px;margin-bottom:3px;" />
                            <%--END SUBSISTINCE SECTION--%>

                            <%--LAUNDRY SECTION--%>
                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div_other_amount" visible="true" >
                                        <div class="col-6" style="padding-right:0px;">
                                            <asp:label runat="server" ID="lbl_other_amount" style="letter-spacing:0.5px;font-size:15px;" Font-Size="Small" Font-Bold="true" Text="Laundry Daily Rate:"></asp:label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Font-Bold="true" ID="txtb_luandry_daily_dspl" Enabled="false" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired12" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group row" runat="server" id="div7"  visible="true">
                                        <div class="col-6" style="padding-right:0px;">
                                            <label class="font-weight-bold">No. Days W/ Laundry:</label>
                                        </div>
                                        <div class="col-6 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_with_laundry" OnTextChanged="txtb_with_laundry_TextChanged" onKeydown="postBack_no_laundry_days(event)" onblur="postBack_no_laundry_calculate()" Font-Bold="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <%--<script type="text/javascript">
                                                        function postBack_no_laundry_days(event)
                                                        {
                                                            if (event.keyCode == 13)
                                                            {
                                                                __doPostBack("<%= txtb_with_laundry.ClientID %>", "");
                                                            }
                                                        }

                                                        function postBack_no_laundry_calculate()
                                                        {
                                                                __doPostBack("<%= txtb_with_laundry.ClientID %>", "");
                                                        }
                                                    </script>--%>
                                                    <asp:Label ID="LblRequired6" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div18" visible="true">
                                        <div class="col-6" style="padding-top:9px;">
                                            <asp:label runat="server" ID="Label22" Font-Bold="true" Text="">&nbsp;</asp:label>
                                        </div>
                                    </div>
                                    <div class="form-group row" runat="server" id="div3" visible="true">
                                        <div class="col-6">
                                            <asp:label runat="server" ID="Label8" Font-Bold="true" Text="Net Laundry:"></asp:label>
                                        </div>
                                        <div class="col-6 text-right">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Font-Bold="true" ID="txtb_net_laundry_amount" Enabled="false" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired15" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top:3px;margin-bottom:3px;" />
                            <%--END LAUNDRY SECTION--%>
                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div14" visible="true" >
                                        <div class="col-6">
                                            <asp:label runat="server" ID="Label23" Font-Bold="true" Text="W/Held Tax Percent :"></asp:label>
                                        </div>
                                        <div class="col-6 ">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <span style="position:absolute;float:right;right:10%;padding-top:2px;color:lightgray;">| %</span>
                                                    <asp:TextBox runat="server" style="padding-right:27px;" Font-Bold="true" Enabled="true" ID="txtb_withhel_tax_perc" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired9" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row" runat="server" id="div9" visible="true" >
                                        <div class="col-6">
                                            <asp:label runat="server" ID="Label9" Font-Bold="true" Text="Tax With Held:"></asp:label>
                                        </div>
                                        <div class="col-6 ">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Font-Bold="true" ID="txtb_with_tax_held_amount" Enabled="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                    <asp:Label ID="LblRequired17" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top:3px; margin-bottom:3px;">
                            <div class="row" runat="server">
                                <div class="col-6">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group row" runat="server" id="div16" visible="false" >
                                                <div class="col-6">
                                                    <asp:label runat="server" ID="Label20" Font-Bold="true" Text="No. Days Leave :"></asp:label>
                                                </div>
                                                <div class="col-6 ">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <%--<span style="position:absolute;float:right;right:10%;padding-top:2px;color:lightgray;">| %</span>--%>
                                                            <asp:TextBox runat="server" Font-Bold="true" Enabled="true" ID="txtb_days_not_exposed" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                            <asp:Label ID="LblRequired100" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="form-group row" runat="server" id="div5" visible="true" >
                                                <div class="col-12">
                                                    <asp:label runat="server" ID="Label12" Font-Bold="true" Text="Hazard Remarks:"></asp:label>
                                                </div>
                                                <div class="col-12">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_remarks" Enabled="true" CssClass="form-control"></asp:TextBox>
                                                            <asp:Label ID="LblRequired10" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-12 text-left" style="padding-top:5px !important">
                                                    <div class="alert alert-success alert-dismissible fade show small" role="alert" style="padding:5px;margin-bottom:0px;margin-left:0px;opacity:0.8;font-size:12px !important">
                                                        <strong> Note:  </strong> -- Do not click Calculate Button to <b>Override the Net Hazard, Net Subsistence and Net Laundry Amount</b>, Click the Save Button instead.
                                                        <asp:Label runat="server" ID="lbl_field_descr3" CssClass="bold alert-link" style="text-decoration:underline !important" ></asp:Label>
                                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="padding: 0px; margin-right: 5px;">
                                                        <span aria-hidden="true" class="small">&times;</span>
                                                        </button>
                                                    </div>

                                                    <%--<i class="fa fa-info-circle text-primary lbl_required pull-left text-left p-5" style="padding:2px !important;"></i><asp:Label ID="Label24" CssClass="lbl_required text-primary" runat="server" Text=" &nbsp;Do not click Calculate Button to Override the Net Hazard, Net Subsistence and Net Laundry Amount, Click the Save Button instead. " ></asp:Label>--%>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-6">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                             <div class="form-group row" runat="server" id="div6" visible="true" >
                                                <div class="col-6">
                                                    <asp:label runat="server" ID="Label14" Font-Bold="true" Text="Total Gross :"></asp:label>
                                                </div>
                                                <div class="col-6 ">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_gross_pay" Enabled="false" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                            <asp:Label ID="LblRequired16" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <hr style="margin-top:5px; margin-bottom:5px;">
                                            <a href="#demo" data-toggle="collapse" id="btn_show">Show Loan Deduction(s)</a>
                                            <div id="demo" class="collapse">
                                                <div class="form-group row" runat="server" id="div10" visible="true" >
                                                    <div class="col-6">
                                                        <asp:label runat="server" ID="Label13" Font-Bold="true" Text="Nico Loan:"></asp:label>
                                                    </div>
                                                    <div class="col-6 ">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" Font-Bold="true" ID="txtb_nico_loan" Enabled="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired18" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="form-group row" runat="server" id="div2" visible="true" >
                                                    <div class="col-6">
                                                        <asp:label runat="server" ID="Label5" Font-Bold="true" Text="CCMPC Loan:"></asp:label>
                                                    </div>
                                                    <div class="col-6 ">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" Font-Bold="true" ID="txtb_ccmpc_loan" Enabled="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired19" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="form-group row" runat="server" id="div11" visible="true" >
                                                    <div class="col-6">
                                                        <asp:label runat="server" ID="Label16" Font-Bold="true" Text="Network Bank Loan:"></asp:label>
                                                    </div>
                                                    <div class="col-6 ">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" Font-Bold="true" ID="txtb_network_bank_loan" Enabled="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired20" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div class="form-group row" runat="server" id="div15" visible="true" >
                                                    <div class="col-6">
                                                        <asp:label runat="server" ID="Label7" Font-Bold="true" Text="LBP eSL:"></asp:label>
                                                    </div>
                                                    <div class="col-6 ">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox runat="server" Font-Bold="true" ID="txtb_other_loan" Enabled="true" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                <asp:Label ID="LblRequired21" CssClass="lbl_required text-left" runat="server" Text=""></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <%-- BEGIN - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                <%--<div class="form-group row" style="margin-bottom:0px !important"></div>--%>
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                        <%-- END   - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>

                                                <%-- BEGIN   - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

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
                                                </div>
                                                       <%-- END     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>


                                                <%-- BEGIN     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6 " >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan1"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan1" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan1"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan2"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan2" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan2"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan3"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan3" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan3"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan4"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan4" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan4"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan5"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan5" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan5"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan6"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan6" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan6"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan7"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan7" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan7"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan8"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan8" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan8"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" style="margin-bottom:0px !important">

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan9"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan9" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan9"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                
                                                <div class="form-group row" >

                                                        <div class="col-6" >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:label runat="server" Font-Bold="true" Font-Size="Small" ID="lbl_other_ded_loan10"></asp:label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6"  >
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtb_other_ded_loan10" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                                    <asp:Label runat="server" CssClass="lbl_required" ID="req_other_ded_loan10"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                </div>
                                                       <%-- END     - VJA - 2022-05-27 - RESERVED DEDUCTIONS--%>
                                            </div>
                                            
                                            <hr style="margin-top:5px; margin-bottom:5px;">
                                            <div class="form-group row" runat="server" id="div_net_pay" visible="true">
                                                <div class="col-6">
                                                    <asp:label runat="server" ID="lbl_net_pay" Font-Bold="true" Text="Total Net Pay :"></asp:label>
                                                </div>
                                                <div class="col-6 text-right">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox runat="server" Font-Bold="true" ID="txtb_net_pay" Enabled="false" ReadOnly="true" Text="0.000" CssClass="form-control form-control-sm text-right"></asp:TextBox>
                                                            <asp:Label ID="LblRequired22" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6" style="margin-top:5px;">
                                                   <asp:UpdatePanel runat="server" ID="Update_asdasd">
                                                       <ContentTemplate>
                                                           <asp:LinkButton ID="Linkbtncancel"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn btn-sm"></asp:LinkButton>
                                                            <asp:Button ID="btnSave" runat="server"  Text="Save" CssClass="btn btn-primary save-icon icn btn-sm" onClick="btnSave_Click" Width="85" />
                                                       </ContentTemplate>
                                                   </asp:UpdatePanel>
                                                </div>
                                                <div class="col-6" style="margin-top:5px;">
                                                     <asp:Button ID="btn_calculate" runat="server" OnClientClick="btn_show()" Text="CALCULATE" CssClass="btn btn-sm btn-block btn-warning" OnClick="btn_calculate_Click"/>
                                                </div>
                                                
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <%--<hr style="margin-top:5px;margin-bottom:5px;" />--%>
                            <div class="row">
                                <div class="col-6">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_if_dateposted_yes" CssClass="col-form-label-sm text-danger" Font-Bold="true" Font-Size="Smaller" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                   </asp:UpdatePanel>
                                </div>
                                <div class="col-6 text-right">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                             <asp:HiddenField ID="hidden_rate_basis" runat="server" />
                                             <asp:HiddenField ID="hidden_max_amount" Value="0" runat="server" />
                                             <asp:HiddenField ID="hidden_subs_max_amount" Value="0" runat="server" />
                                             <asp:HiddenField ID="hidden_laundry_max_amount" Value="0" runat="server" />
                                             <asp:HiddenField ID="hidden_monthly_days" runat="server" />
                                             <asp:HiddenField ID="hidden_hours_in_days" runat="server" />
                                             <asp:HiddenField ID="hidden_daily_rate" runat="server" />
                                             <asp:HiddenField ID="hidden_hourly_rate" runat="server" />
                                             <asp:HiddenField ID="hidden_monthly_rate" runat="server" />
                                       
                                   
                                    <%--<asp:Button ID="btn_show_headers" OnClick="btn_show_headers_Click" runat="server" Text="Button" />--%>
                                            
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
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
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
                                function search_for(key)
                                {
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
                                <div class="col-3">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:Label runat="server" Text="Show"></asp:Label>
                                                <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="28%" ToolTip="Show entries per page">
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
                                
                                
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-6">
                                                <asp:Label runat="server"  Text="Payroll Year:" ></asp:Label>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group row">
                                        <div class="col-4">
                                                <asp:Label runat="server" Text="Month:" ></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="01" Text="Jan"></asp:ListItem>
                                                        <asp:ListItem Value="02" Text="Feb"></asp:ListItem>
                                                        <asp:ListItem Value="03" Text="Mar"></asp:ListItem>
                                                        <asp:ListItem Value="04" Text="Apr"></asp:ListItem>
                                                        <asp:ListItem Value="05" Text="May"></asp:ListItem>
                                                        <asp:ListItem Value="06" Text="Jun"></asp:ListItem>
                                                        <asp:ListItem Value="07" Text="Jul"></asp:ListItem>
                                                        <asp:ListItem Value="08" Text="Aug"></asp:ListItem>
                                                        <asp:ListItem Value="09" Text="Sep"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-5">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-7">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3"></div>
                                <div class="col-9">
                                    <div class="form-group row">
                                        <div class="col-2" style="padding-right:0px;">
                                            <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_template" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group row">
                                        <div class="col-12">
                                            <a ID="lb_back" class="btn btn-info btn-sm font-weight-bold" href="/View/cPayRegistry/cPayRegistry.aspx" ><i class="fa fa-arrow-left"></i> Back To Payroll Registry</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-8">
                                    <div class="form-group row">
                                        <div class="col-2" style="padding-right:0px;">
                                            <asp:Label runat="server" Text="Payroll Group:" ></asp:Label>
                                        </div>
                                        <div class="col-10">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payroll_group" runat="server" CssClass="form-control-sm form-control" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged" AutoPostBack="true" style="float:right;width:97.5%"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-1">
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
                                                <asp:TemplateField HeaderText="ID NO." SortExpression="empl_id">
                                                    <ItemTemplate>
                                                        <%# Eval("empl_id") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="9%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="POSITION" SortExpression="position_title1">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%# Eval("position_title1") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("net_pay") %>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                               

                                               <asp:TemplateField HeaderText="STATUS" SortExpression="post_status_descr">
                                                    <ItemTemplate>
                                                        &nbsp;<%# Eval("post_status_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="12%" />
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
                                                                        Enabled='<%# Eval("post_status").ToString() == "Y" && Session["ep_post_authority"].ToString() == "0" || Eval("post_status").ToString() == "N" && Session["ep_post_authority"].ToString() == "1"  ||  Eval("post_status").ToString() == "R" ? false : true %>'
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
                                                                         style="padding-left: 0px !important;" 
                                                                         CommandArgument='<%# Eval("payroll_registry_nbr")%> ' 
                                                                         tooltip="Print Card"/>
                                                                <%  }
                                                                %>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="12%" />
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

        function select_tab(tab_number)
        {
            if (tab_number == "1")
            {
                $('#name_of_employee_link').click();
            }
            else if (tab_number == "2") {
                 $('#date_config_link').click();
            }
            else if (tab_number == "3")
            {
                 $('#id_loans').click();
            }
        }
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
        function btn_show()
        {
            if ($('#demo').hasClass('collapse show') && !$('#btn_show').hasClass('collapsed'))
            {
                $('#demo').removeAttr('class');
                $('#demo').removeClass('collapse show')
                $('#demo').addClass('collapse show')
                setTimeout(function ()
                {
                    $('#demo').addClass('collapse show')
                }, 200);
            }
            
        }
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