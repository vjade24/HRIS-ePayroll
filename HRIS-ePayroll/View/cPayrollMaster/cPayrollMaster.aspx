<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayrollMaster.aspx.cs" Inherits="HRIS_ePayroll.View.cPayrollMaster.cPayrollMaster" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">

    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
        

    <!-- The Modal - Select Report -->
    <asp:UpdatePanel ID="edit_delete_notify" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
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
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                        <ContentTemplate>
                            <div class="modal-body with-background">
                                <div class="row">
                                    <div class="col-12">
                                        <Label class="small"><strong>List of Employee:</strong></Label>
                                        <asp:DropDownList ID="ddl_select_report" CssClass="form-control form-control-sm" runat="server" Width="100%" OnSelectedIndexChanged="ddl_select_report_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="1" Text="List of Employee - Payroll Master"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="List of Employee - Plantilla"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="List of Employee - Age"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="List of Employee - Resigned and Retired"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-12" runat="server" id="div_budget_year" >
                                        <Label class="small"><strong>Budget Year:</strong></Label>
                                        <asp:DropDownList ID="ddl_budget_year" CssClass="form-control form-control-sm" runat="server" Width="100%" ></asp:DropDownList>
                                    </div>
                                    <div class="col-6" runat="server" id="div_payroll_year" style="width:100% !important">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <Label runat="server" id="lbl_year" class="small"><strong>Year:</strong></Label>
                                                <asp:DropDownList ID="ddl_payroll_year" CssClass="form-control form-control-sm" runat="server" Width="100%" ></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-6" runat="server" id="div_payroll_month" style="width:100% !important">
                                        <Label class="small"><strong>Month:</strong></Label>
                                        <asp:DropDownList ID="ddl_payroll_month" CssClass="form-control form-control-sm" runat="server" Width="100%" >
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
                                    </div>
                                    
                                    <div class="col-6" runat="server" id="div_employment_type" style="width:100% !important">
                                        <Label class="small"><strong>Employment Type:</strong></Label>
                                        <asp:DropDownList ID="ddl_employment_type" CssClass="form-control form-control-sm" runat="server" Width="100%" ></asp:DropDownList>
                                    </div>
                                    <div class="col-6" runat="server" id="div_status" style="width:100% !important">
                                        <Label class="small"><strong>Status:</strong></Label>
                                        <asp:DropDownList ID="ddl_status" CssClass="form-control form-control-sm" runat="server" Width="100%" >
                                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-12" runat="server" id="div_department" style="width:100% !important">
                                        <Label class="small"><strong>Department:</strong></Label>
                                        <asp:DropDownList ID="ddl_department_report" CssClass="form-control form-control-sm" runat="server" Width="100%" ></asp:DropDownList>
                                    </div>
                                    <div class="col-6" runat="server" id="div_age" style="width:100% !important">
                                        <Label class="small"><strong>Age: <span class="text-danger"> >=</span></strong></Label>
                                        <asp:TextBox runat="server" ID="txtb_age" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="col-12" style="margin-top:5px !important">
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
                            <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
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

        <asp:UpdatePanel ID="delete_confirm_popup" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <!-- Modal Delete -->
                <div class="modal fade" id="deleteRec">
                    <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content text-center">
                    <!-- Modal body -->
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
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
                                        <div class="col-3">
                                            <strong><asp:Label runat="server" Text="Payroll Group :"></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_group_nbr" CssClass="form-control form-control-sm" Width="100%" runat="server"></asp:DropDownList>
                                                    <%--<span class="text-primary smaller font-weight-bold" style="padding-bottom:0px !important;padding-bottom:0px !important;margin-bottom:0px !important;margin-bottom:0px !important;"> <i class="fa fa-info-circle"></i> </span>--%>
                                                    <i class="fa fa-info-circle text-primary lbl_required pull-left text-left p-5" style="padding:2px !important"></i><asp:Label ID="Label2" CssClass="lbl_required text-primary" runat="server" Text=" &nbsp;Group number list is filtered by Department."></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-3">
                                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                            <ContentTemplate>
                                                <strong><asp:Label runat="server" style="width:30%;float:left">Status:</asp:Label></strong>
                                                <asp:DropDownList runat="server" CssClass="form-control form-control-sm" style="width:60%;float:right" ID="ddl_emp_status" OnSelectedIndexChanged="ddl_emp_status_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In-Active"></asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<label runat="server" class="container" style="display:inline !important;font-size:14px !important;font-weight:bold;display:none"> Status 
                                                    <asp:CheckBox ID="chkbx_emp_status" Autopostback="true" runat="server" OnCheckedChanged="chkbx_emp_status_CheckedChanged" Visible="false"/>
                                                    <span class="checkmark"></span>
                                                </label>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                        <div class="col-12">
                                            <hr style="margin-bottom:5px;margin-top:5px"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6 ">
                                             <strong><asp:Label runat="server" Text="Monthly Rate:" ></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server"  >
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_monthly_rate" CssClass="form-control form-control-sm text-right" Font-Bold="true" ></asp:TextBox>
                                                    <asp:Label ID="LblRequired20" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6">
                                            <strong><asp:Label runat="server" Text="Period From :"></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanelFrom">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_period_from" CssClass="form-control form-control-sm my-date text-center"></asp:TextBox>
                                                    <asp:Label ID="LblRequired14" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6">
                                             <strong><asp:Label runat="server" Text="Daily Rate:" ></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server"  >
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_daily_rate" CssClass="form-control form-control-sm text-right" Font-Bold="true" ></asp:TextBox>
                                                    <asp:Label ID="LblRequired21" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6">
                                            <strong><asp:Label runat="server" Text="Period To :"></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server"  UpdateMode="Conditional" ID="UpdatePanelTo">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_period_to" CssClass="form-control form-control-sm my-date text-center"></asp:TextBox>
                                                    <asp:Label ID="LblRequired15" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                               
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6">
                                             <strong><asp:Label runat="server" Text="Hourly Rate:" ></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server"  >
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_hourly_rate" CssClass="form-control form-control-sm text-right" Font-Bold="true" ></asp:TextBox>
                                                    <asp:Label ID="LblRequired22" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                 
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6 ">
                                            <strong><asp:Label runat="server" Text="Effective Date :"></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanelEffec">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_effective_date" CssClass="form-control form-control-sm my-date text-center"></asp:TextBox>
                                                    </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-bottom:3px;margin-top:0px"/>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6">
                                            <strong><asp:Label runat="server" Text="Date of Assumption:"></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel34">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="txtb_date_of_assumption" CssClass="form-control form-control-sm my-date text-center"></asp:TextBox>
                                                    <asp:Label ID="LblRequired1000" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-6">
                                            <strong><asp:Label runat="server" Text="Birth Date:"></asp:Label></strong>
                                        </div>
                                        <div class="col-6">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date text-center" BorderColor="LightGrey"  ID="txtb_birth_date" Enabled="false"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-bottom:3px;margin-top:0px"/>
                                </div>
                                

                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                                <ContentTemplate>
                                                    <label ID="Label4" runat="server" class="container" style="font-size:14px !important;font-weight:bold">
                                                        GSIS <asp:CheckBox ID="chckbox_flag_gsis"  Autopostback="true" runat="server" />
                                                        <span class="checkmark" ></span>
                                                    </label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                                <ContentTemplate>
                                            
                                                    <label ID="Label5" runat="server" class="container" style="font-size:14px !important;font-weight:bold">
                                                        PHIC <asp:CheckBox ID="chckbox_flag_phic"  Autopostback="true" runat="server" />
                                                        <span class="checkmark"></span>
                                                    </label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                                <ContentTemplate>
                                           
                                                    <label ID="Label6" runat="server" class="container" style="font-size:14px !important;font-weight:bold">
                                                         HDMF<asp:CheckBox ID="chckbox_flag_hdmf"  Autopostback="true" runat="server" />
                                                        <span class="checkmark"></span>
                                                    </label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-2" style="padding-right:0px !important">
                                    <strong><asp:Label runat="server" Text="(PS) FIX RATE:" style="font-size:13px !important"></asp:Label></strong>
                                </div>      
                                <div class="col-3">
                                    <strong><asp:Label runat="server" Text="Exempted Deduction:"></asp:Label></strong>
                                </div>
                                <div class="col-3 ">
                                    <asp:UpdatePanel runat="server"  >
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_hdmf_fix_rate" CssClass="form-control form-control-sm text-right" Font-Bold="true" Text="0.00"></asp:TextBox>
                                            <asp:Label ID="LblRequired300" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                                <div class="col-12">
                                    <hr style="margin-bottom:3px;margin-top:0px"/>
                                </div>
                                

                                <div class="col-12">
                                    <ul class="nav nav-tabs" style="border-bottom: 1px solid #dee2e6" id="myTab" role="tablist">
                                        
                                        <li class="nav-item">
                                        <a class="nav-link active" id="id_mandatory" data-toggle="tab" href="#mandatory_tab" role="tab" aria-controls="mandatory_tab" aria-selected="false" style="font-weight:bold" aria-expanded="true"> Assignment</a>
                                        </li>
                                        <li class="nav-item">
                                        <a class="nav-link" id="id_optional" data-toggle="tab" href="#optional_tab" role="tab" aria-controls="optional_tab" aria-selected="false" style="font-weight:bold"> Master Details</a>
                                        </li>
                                        <li class="nav-item">
                                        <a class="nav-link" id="id_empl_history" data-toggle="tab" href="#empl_history_tab" role="tab" aria-controls="empl_history_tab" aria-selected="false" style="font-weight:bold">Employment History</a>
                                        </li>
                                        <li class="nav-item">
                                        <a class="nav-link" id="id_empl_deduction" data-toggle="tab" href="#id_empl_deduction_tab" role="tab" aria-controls="id_empl_deduction" aria-selected="false" style="font-weight:bold"><span class="badge badge-success smaller"> Active</span> &nbsp;&nbsp;Deduction Ledger </a> 
                                        </li>
                                    </ul>
                                </div>
                                <div class="tab-content" id="myTabContent" style="width:100%;padding-top:10px">
                                    <div class="tab-pane fade show active"  id="mandatory_tab" role="tabpanel" aria-labelledby="id_mandatory" style="padding-right: 20px;padding-left: 20px;">
                                        <div class="row">
                                            <div class="col-3">
                                                <label class="font-weight-bold">Department</label>
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
                                                <label class="font-weight-bold">Fund Charges :</label>
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
                                                <label class="font-weight-bold">Function Code :</label>
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
                                                <hr style="margin-bottom:3px;margin-top:3px"/>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">Position Title :</label>
                                            </div>
                                            <div class="col-9">
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_position" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                        <asp:Label ID="LblRequired6" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            
                                            <div class="col-3">
                                                <label class="font-weight-bold">Salary Grade:</label>
                                            </div>
                                            <div class="col-2">
                                                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm text-center" ID="txtb_salary_grade" Enabled="false"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-3">
                                                <label class="font-weight-bold">Hazard Override % :</label>
                                            </div>
                                            <div class="col-2">
                                                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm text-right" ID="txtb_hazard_pay_override_hidden"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="optional_tab" role="tabpanel" aria-labelledby="id_optional" style="padding-right: 20px;padding-left: 20px;">
                                        <div class="row">
                                            <div class="col-12" >
                                                <asp:UpdatePanel ID="update_details" UpdateMode="Conditional" runat="server" style="overflow-y:auto !important;height:265px !important">
                                                    <ContentTemplate>
                                                        <asp:GridView 
                                                                ID="gv_details" 
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
                                                                EmptyDataRowStyle-CssClass="no-data-found"
                                                    
                                                            >
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="CODE" SortExpression="account_code" >
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="gv_lbl_account_code" Text='<%# Eval("account_code") %>'></asp:Label>
                                                                            
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SB CODE" SortExpression="account_sub_code">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="gv_lbl_account_sub_code" Text='<%# Eval("account_sub_code") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ACCOUNT TITLE" SortExpression="account_description">
                                                                        <ItemTemplate>
                                                                            <%# Eval("account_title") %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="35%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="LEFT" />
                                                                    </asp:TemplateField>
                                                        
                                                                    <asp:TemplateField HeaderText="AMOUNT" SortExpression="account_amount">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtb_account_amount" Text='<%# Eval("account_amount") %>' CssClass="form-control form-control-sm text-right" Enabled='<%# Eval("effective_row").ToString() == "C"  ? true:false %>'></asp:TextBox>
                                                                                    <asp:Label ID="LblRequired13" runat="server" CssClass="lbl_required" Text=""></asp:Label>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%" />
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
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_empl_name" />
                                                        <%--<asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                                        <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_department" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_empl_name" />--%>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="empl_history_tab" role="tabpanel" aria-labelledby="id_empl_history" style="padding-right: 20px;padding-left: 20px;">
                                        <div class="row">
                                            <div class="col-12" >
                                                <asp:UpdatePanel ID="up_datagrid_history" UpdateMode="Conditional" runat="server" style="overflow-y:auto !important;height:265px !important">
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
                                                                    <asp:TemplateField HeaderText="EFF. DATE" SortExpression="effective_date" >
                                                                        <ItemTemplate>
                                                                            <%# Eval("effective_date") %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="CENTER" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DEPARTMENT" SortExpression="department_name1">
                                                                        <ItemTemplate>
                                                                            &nbsp;<%# Eval("department_name1") %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="35%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="LEFT" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="POSITION" SortExpression="position_long_title">
                                                                        <ItemTemplate>
                                                                            &nbsp;<%# Eval("position_long_title") %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="LEFT" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="RATE" SortExpression="monthly_rate">
                                                                        <ItemTemplate>
                                                                            <%# Eval("monthly_rate") %>&nbsp;
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="RIGHT" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ACTION">
                                                                    <ItemTemplate>
                                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                                            <ContentTemplate>
                                                                                
                                                                                    <asp:ImageButton ID="img_edit_history"  CssClass="btn btn-warning action" EnableTheming="true"  runat="server"  ImageUrl="~/ResourceImages/add.png" style="padding-left: 0px !important;" OnCommand="img_edit_history_Command" CommandArgument='<%# Eval("empl_id") + "," + Eval("effective_date")%>'/>
                                                        

                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_empl_name" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="id_empl_deduction_tab" role="tabpanel" aria-labelledby="id_empl_deduction" style="padding-right: 20px;padding-left: 20px;">
                                        <div class="row">
                                            <div class="col-12" >
                                                <asp:UpdatePanel ID="up_deduction_list" UpdateMode="Conditional" runat="server" style="overflow-y:auto !important;height:265px !important">
                                                    <ContentTemplate>
                                                        <asp:GridView 
                                                                ID="gv_deduction_list" 
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
                                                                    <asp:TemplateField HeaderText="DEDUCTION DESCRIPTION" SortExpression="deduc_descr" >
                                                                        <ItemTemplate>
                                                                            &nbsp;<%# Eval("deduc_descr") %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="41%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="LEFT" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DATE FROM" SortExpression="deduc_date_from">
                                                                        <ItemTemplate>
                                                                            <%# Eval("deduc_date_from") %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="12%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DATE TO" SortExpression="deduc_date_to">
                                                                        <ItemTemplate>
                                                                            <%# Eval("deduc_date_to") %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="12%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="AMOUNT 1" SortExpression="deduc_amount1">
                                                                        <ItemTemplate>
                                                                            <%# Eval("deduc_amount1") %>&nbsp;
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="RIGHT" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="AMOUNT 2" SortExpression="deduc_amount2">
                                                                        <ItemTemplate>
                                                                            <%# Eval("deduc_amount2") %>&nbsp;
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="RIGHT" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ACTION" >
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton 
                                                                                ID="img_btn_view_history" 
                                                                                CssClass="btn btn-warning action" 
                                                                                EnableTheming="true" 
                                                                                runat="server"  
                                                                                ImageUrl="~/ResourceImages/final_select.png"
                                                                                OnCommand="img_btn_view_history_Command"
                                                                                CommandArgument='<%# Eval("deduc_code")%> '   
                                                                                tooltip="View Deduction Ledger History Details" 
                                                                                />

                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_empl_name" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-bottom:5px;margin-top:5px"/>
                                </div>
                                <div class="col-6 text-left">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <% if (ViewState["page_allow_delete"].ToString() == "1")
                                            {  %>
                                                <asp:Button ID="btn_reprocess" runat="server"  Text="Reprocess from Plantilla" CssClass="btn btn-success submit-icon icn" OnClick="btn_reprocess_Click"/>
                                            <% }
                                            %> 
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-6 text-right" >
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

                                <div class="row" style="display:none">
                                    <div class="col-3">
                                        <label class="font-weight-bold">Rate Basis :</label>
                                    </div>
                                    <div class="col-9">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_rate_basis_hidden"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                           </div>
                        </div>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel20" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- Modal Delete -->
            <div class="modal fade" id="modal-employment-history" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-md">
                <div class="modal-content">
                    <div class="modal-header bg-warning text-white font-weight-bold" >
                        <h5><i class="fa fa-info-circle"></i> Payroll Employment History Info </h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                        <div class="modal-body with-background">
                            <div class="form-group row p-0">
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Effective Date:</strong></Label>
                                </div>
                                <div class="col-3 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_effective" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                                <div class="col-3">
                                    <Label class="small"><strong>Status:</strong></Label>
                                </div>
                                <div class="col-3 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_status" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Emplymnt. Type:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_employment" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Monthly Rate:</strong></Label>
                                </div>
                                <div class="col-3 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_monthly" CssClass="form-control form-control-sm text-right" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Daily Rate:</strong></Label>
                                </div>
                                <div class="col-3 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_daily" CssClass="form-control form-control-sm text-right" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Hourly Rate:</strong></Label>
                                </div>
                                <div class="col-3 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_hourly" CssClass="form-control form-control-sm text-right" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Hazard %:</strong></Label>
                                </div>
                                <div class="col-3 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_hzrd_perc" CssClass="form-control form-control-sm text-center" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Position Title:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_position" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Grouping Descr.:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_groupings" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Department:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel  runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_department" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Sub-Department:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_subdepartment" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Division:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_division" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Section:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_section" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Function:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_function" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-3 pr-0">
                                    <Label class="small"><strong>Fund Charges:</strong></Label>
                                </div>
                                <div class="col-9 pb-1">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_history_fund" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
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

    <asp:UpdatePanel ID="UpdatePanel27" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- Modal Delete -->
            <div class="modal fade" id="modal-ledger-audit-table" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-md">
                <div class="modal-content">
                    <div class="modal-header text-white font-weight-bold" style="background-color: #507CD1;color:white !important">
                        <h5><i class="fa fa-bookmark-o"></i> - Deduction Ledger History Details</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                        <div class="modal-body with-background">    
                            <div class="form-group row p-0">
                                
                                <div class="col-8">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <Label class="small"><strong>Deduction Description:</strong></Label>
                                            <asp:TextBox runat="server" ID="txtb_deduc_descr" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-4">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <Label class="small"><strong>Code:</strong></Label>
                                            <asp:TextBox runat="server" ID="txtb_deduc_code" CssClass="form-control form-control-sm" Font-Bold="true" Enabled="false" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-bottom:5px;margin-top:5px"/>
                                </div>
                                <div class="col-12" >
                                    <asp:UpdatePanel ID="up_deduction_audit_history" UpdateMode="Conditional" runat="server" style="overflow-y:auto !important;height:265px !important">
                                        <ContentTemplate>
                                            <asp:GridView 
                                                    ID="gv_deduction_audit_history" 
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
                                                        
                                                        <asp:TemplateField HeaderText="DATE FROM" SortExpression="deduc_date_from">
                                                            <ItemTemplate>
                                                                <%# Eval("deduc_date_from") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="12%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DATE TO" SortExpression="deduc_date_to">
                                                            <ItemTemplate>
                                                                <%# Eval("deduc_date_to") %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="12%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AMOUNT 1" SortExpression="deduc_amount1">
                                                            <ItemTemplate>
                                                                <%# Eval("deduc_amount1") %>&nbsp;
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="RIGHT" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AMOUNT 2" SortExpression="deduc_amount2">
                                                            <ItemTemplate>
                                                                <%# Eval("deduc_amount2") %>&nbsp;
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="RIGHT" />
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
                                            <asp:AsyncPostBackTrigger ControlID="ddl_empl_name" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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

    
        <!-- The Modal - Add Confirmation -->
        <div class="modal fade" id="AddEditConfirm">
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content text-center">
                <!-- Modal body -->
                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                    <ContentTemplate>
                    <div class="modal-body">
                        <i runat="server" id="i_icon_display" class="fa-5x fa fa-check-circle text-success"></i>
                        <h2 runat="server" id="h2_status" ></h2>
                        <h6><asp:Label ID="SaveAddEdit" runat="server"></asp:Label></h6>
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- Modal footer -->
                <div style="margin-bottom:30px">
                    <button type="button" class="btn btn-primary btn-lg" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true"> <i class="fa fa-check"></i> OK</span>
                    </button>
                </div>
            </div>
            </div>
        </div>
    
    <!-- The Modal - Pop-Up Modal for Reprocess to Plantilla -->
    <asp:UpdatePanel ID="UpdatePanel8" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="modal fade" id="modal_reprocess">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content text-center">
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <i class="fa-5x fa fa-question text-success" ID="I1" runat="server" ></i>
                                    <br />
                                    <h2 runat="server" id="H1">Delete and Reprocess </h2>
                                    <div class="row text-left">
                                        <div class="col-2"></div>
                                        
                                        <div class="col-4">
                                            <asp:Label runat="server" CssClass="font-weight-bold" Text="Year:"></asp:Label>
                                            <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" ></asp:DropDownList>
                                        </div>
                                        <div class="col-4">
                                            <asp:Label runat="server" CssClass="font-weight-bold" Text="Month:"></asp:Label>
                                            <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" >
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
                                        </div>
                                        <div class="col-2"></div>
                                    </div>
                                    <br />
                                    <h6><asp:Label ID="Label3" runat="server" Text="Are you sure you want to delete this Employee's Record for this specific Year, Month and Reprocess it?"></asp:Label></h6>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="margin-bottom:50px">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkbtn_reprocess" runat="server"  CssClass="btn btn-success" OnClick="lnkbtn_reprocess_Click" > <i class="fa fa-check" id="icn_reprocess"></i> Yes, Delete and Reprocess </asp:LinkButton>
                                    
                                    <asp:LinkButton ID="LinkButton4"  runat="server" data-dismiss="modal"  CssClass="btn btn-dark"> <i class="fa fa-times"></i> No, Keep it! </asp:LinkButton>
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
                            <div class="row" >
                                <div class="col-lg-3">
                                    <div class="form-group row">
                                        <div class="col-lg-3">
                                             <asp:Label runat="server" Text="Show"></asp:Label>
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                        <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm" AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="100%" ToolTip="Show entries per page">
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
                                        <div class="col-lg-4">
                                                <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9" style="font-size:10px !important"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                        <label >Employment Type: </label>
                                </div>
                                <div class="col-lg-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_empl_type" runat="server"  CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" ></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-4 text-right">
                                    <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group row">
                                                <div class="col-lg-8">
                                                    <% if (ViewState["page_allow_print"].ToString() == "1")
                                                        {  %>
                                                    <asp:Button ID="btn_print" runat="server" CssClass="btn btn-success btn-sm print-icon icn  btn-block"  Text="Print List of Employee" OnClick="btn_print_Click" />
                                                    <% }
                                                    %>  

                                                </div>
                                                <div class="col-lg-4" style="visibility:hidden">
                                                    <%--<% if (ViewState["page_allow_add"].ToString() == "1")
                                                        {  %>--%>
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm add-icon icn btn-block"  Text="Add" OnClick="btnAdd_Click" />
                                                    <%--<% }
                                                        %>     --%>

                                                </div>
                                            </div>
                                            
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                               
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group row">
                                        <div class="col-lg-3">
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
                                        <div class="col-lg-2" >
                                            <label >Department: </label>
                                        </div>
                                        <div class="col-lg-7" >
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList  ID="ddl_department" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" ></asp:DropDownList>
                                                    <asp:Label ID="LblRequired11" runat="server" CssClass="lbl_required" Text=""></asp:Label>
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
                                                    <ItemStyle Width="08%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="EMPLOYEE NAME" SortExpression="employee_name">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;<%#  Eval("employee_name") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="47%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="RATE BASIS" SortExpression="rate_basis_descr" >
                                                    <ItemTemplate>
                                                        <%# Eval("rate_basis_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="RATE" SortExpression="daily_rate" >
                                                    <ItemTemplate>
                                                        <%# Eval("rate_basis").ToString() == "M" ? Eval("monthly_rate").ToString() : Eval("rate_basis").ToString() == "D" ? Eval("daily_rate").ToString() : Eval("hourly_rate").ToString()%>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="EFF. DATE" SortExpression="effective_date" >
                                                            <ItemTemplate>
                                                                <%# Eval("effective_date") %>
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

                                                                <% if (ViewState["page_allow_delete"].ToString() == "0")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton 
                                                                        ID="lnkDeleteRow" 
                                                                        CssClass="btn btn-danger action" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/final_delete.png"
                                                                        OnCommand="deleteRow_Command" 
                                                                        CommandArgument='<%# Eval("empl_id")+","+Eval("effective_date")+","+Eval("employment_type")%> '   
                                                                        tooltip="Delete" 
                                                                        />

                                                                    
                                                                <% }
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
                                    <asp:AsyncPostBackTrigger ControlID="Button2" />
                                    <asp:AsyncPostBackTrigger ControlID="txtb_search" />
                                    <asp:AsyncPostBackTrigger ControlID="DropDownListID" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_empl_type" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_department" />
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
            $('#<%= txtb_period_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_period_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_effective_date.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
            $('#<%= txtb_date_of_assumption.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });


            if ($('#<%= txtb_period_from.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_period_from.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }

            if ($('#<%= txtb_period_to.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_period_to.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }
            if ($('#<%= txtb_effective_date.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_effective_date.ClientID %>').closest("div");
                parent_div.find("i").remove();    
            }
            if ($('#<%= txtb_date_of_assumption.ClientID %>').prop('disabled') == true)
            {
                var parent_div = $('#<%= txtb_date_of_assumption.ClientID %>').closest("div");
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

        function CloseNotification() {
            $('#deleteRec').modal("hide");
            $('#AddEditConfirm').modal({
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

         function openEmplHistory() {
            $('#modal-employment-history').modal({
                keyboard: false,
                backdrop:"static"
            });  
        };
        
    </script>

    <script type="text/javascript">
        function openReprocess()
        {
            $('#modal_reprocess').modal({
                keyboard: false,
                backdrop:"static"
            }); 
        }
        
        function closeReprocess()
        {
            $('#icn_reprocess').removeClass('fa fa-check');
            $('#icn_reprocess').addClass('fa fa-spinner fa-spin');
             setTimeout(function () {
                
                $('#modal_reprocess').modal("hide");
                $('#add').modal("hide");
                $('#icn_reprocess').removeClass('fa fa-spinner fa-spin');
                $('#icn_reprocess').addClass('fa fa-check');
                $('.modal-backdrop.show').remove();
                $('#AddEditConfirm').modal({
                    keyboard: false,
                    backdrop:"static"
                }); 
            }, 2000);
        }

        function openModal_Ledger_Audit()
        {
            $('#modal-ledger-audit-table').modal({
                keyboard: false,
                backdrop:"static"
            }); 
        }

        function open_Dynamic_Notification() {
            $('#AddEditConfirm').modal({
                keyboard: false,
                backdrop:"static"
            });  
        };

        function open_SelectReport() {
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
            
            $('#<%= gv_deduction_list.ClientID%> tr').hover(function () {
                   $(this).addClass('highlight_on_grid');
           }, function () {
                   $(this).removeClass('highlight_on_grid');
                });

            $('#<%= gv_datagrid_history.ClientID%> tr').hover(function () {
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
