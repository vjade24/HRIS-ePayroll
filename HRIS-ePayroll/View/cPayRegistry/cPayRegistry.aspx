<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cPayRegistry.aspx.cs" Inherits="HRIS_ePayroll.View.cPayRegistry.cPayRegistry" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
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
    <form runat="server" >

    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
    
        
        <!-- The Modal - Select Report -->
        <asp:UpdatePanel ID="UpdatePanel23" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="modal_payroll_list" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #ffc107;color: black;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <h5 class="modal-title" ><asp:Label ID="lbl_select_option" runat="server" Text=""></asp:Label></h5>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    
                                    <div class="row pb-1" id="div_name" runat="server">
                                        
                                        <div class="col-3">
                                            <asp:Label runat="server" Text="Employee Name:" ></asp:Label>
                                        </div>
                                        <div class="col-9">
                                            <select class="js-example-basic-single" style="width:100% !important" id="ddl_employee_name"></select>
                                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <%--<asp:DropDownList runat="server" ID="ddl_employee_name" CssClass="js-example-basic-single" Width="100%"   OnSelectedIndexChanged="ddl_employee_name_SelectedIndexChanged" AutoPostBack="false"   ></asp:DropDownList>--%>
                                                    <%--<asp:DropDownList ID="ddl_employee_name"  runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_employee_name_SelectedIndexChanged"></asp:DropDownList>--%>
                                                 
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <%--<div class="col-3">
                                            <div class="form-group row">
                                                <div class="col-5">
                                                    <asp:Label runat="server" Text="ID No:" ></asp:Label>
                                                </div>
                                                <div class="col-7">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddl_employee_id"  runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_employee_id_SelectedIndexChanged"></asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>--%>
                                        
                                    </div>
                                    <div class="row" id="div_yr_mth" runat="server">
                                        <div class="col-3">
                                            <asp:Label runat="server" Text="Payroll Year:" ></asp:Label>
                                        </div>
                                        <div class="col-3">
                                            <%--<asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_year_modal" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_employee_name_SelectedIndexChanged" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                            <select class="form-control-sm form-control" id="year_filter">
                                                <option value="2020">2020</option>
                                                <option value="2021">2021</option>
                                                <option value="2022">2022</option>
                                                <option value="2023">2023</option>
                                                <option value="2024" selected>2024</option>
                                                <option value="2025">2025</option>
                                            </select>
                                        </div>

                                        <div class="col-2">
                                            <asp:Label runat="server" Text="Payroll Month:" ></asp:Label>
                                        </div>
                                        <div class="col-4">
                                            <select class="form-control-sm form-control" id="month_filter">
                                                <option value="01">January  </option>
                                                <option value="02">February </option>
                                                <option value="03">March    </option>
                                                <option value="04">April    </option>
                                                <option value="05">May      </option>
                                                <option value="06">June     </option>
                                                <option value="07" selected>July     </option>
                                                <option value="08">August   </option>
                                                <option value="09">September</option>
                                                <option value="10">October  </option>
                                                <option value="11">November </option>
                                                <option value="12">December </option>
                                            </select>
                                            <%--<asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_month_modal" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_employee_name_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Text="All Month"></asp:ListItem>
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
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                    <div class="row" id="div_empl_type" runat="server">
                                        <div class="col-3 mt-1">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-9 mt-1">
                                            <select class="form-control-sm form-control" id="empl_type_filter"> 
                                                <option value="">-- Select All --</option>
                                                <option value="RE">Regular</option>
                                                <option value="CE">Casual</option>
                                                <option value="JO">Job-Order</option>
                                            </select>
                                            <%--<asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type_modal" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_modal_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>

                                    <div class="row" id="div_dep" runat="server">

                                        <div class="col-3 mt-1" >
                                            <asp:Label runat="server" Text="Department:" ></asp:Label>
                                        </div>
                                        <div class="col-9 mt-1">
                                            <select class="form-control-sm form-control" id="department_filter"></select>
                                            <%--<asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_dep_modal" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_dep_modal_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>

                                    
                                    <div class="row" id="div_payrolltemplate" runat="server">

                                        <div class="col-3 mt-1" >
                                            <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                        </div>
                                        <div class="col-9 mt-1">
                                            <select class="form-control-sm form-control" id="payrolltemplate_filter"></select>
                                            <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_payrolltemplate_report" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                    
                                    <div class="row" id="div_pyroll_lst" runat="server">
                                        
                                        <div class="col-12 mt-3">
                                            <div class="table-responsive table-bordered" style="border:1px solid;border-radius:10px;padding:10px">
                                                <table class="table table-hover" id="datalist_grid">
                                                  <thead>
                                                    <tr>
                                                      <th scope="col" style="background-color:#507CD1 !important;color:white !important">REG</th>
                                                      <th scope="col" style="background-color:#507CD1 !important;color:white !important">DESCRIPTION</th>
                                                      <th scope="col" style="background-color:#507CD1 !important;color:white !important">GRP</th>
                                                      <th scope="col" style="background-color:#507CD1 !important;color:white !important">PAYROLL DESCR.</th>
                                                      <th scope="col" style="background-color:#507CD1 !important;color:white !important">GROSS</th>
                                                      <th scope="col" style="background-color:#507CD1 !important;color:white !important">NET PAY</th>
                                                      <th scope="col" style="background-color:#507CD1 !important;color:white !important">STATUS</th>
                                                    </tr>
                                                  </thead>
                                                </table>
                                            </div>
                                            <%--<asp:UpdatePanel ID="UpdatePanel25" class="mt10" UpdateMode="Conditional" runat="server" >
                                                <ContentTemplate>
                                                    <asp:GridView 
                                                            ID="grid_payroll_list" 
                                                            runat="server" 
                                                            allowpaging="True" 
                                                            AllowSorting="True" 
                                                            AutoGenerateColumns="False" 
                                                            EnableSortingAndPagingCallbacks="True"
                                                            ForeColor="#333333" 
                                                            GridLines="Both" height="100%" 
                                                            onsorting="gv_dataListGrid_per_empl_Sorting"  
                                                            PagerStyle-Width="3" 
                                                            PagerStyle-Wrap="false" 
                                                            pagesize="10"
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
                                                                <asp:TemplateField HeaderText="REG" SortExpression="payroll_registry_nbr">
                                                                    <ItemTemplate>
                                                                        <%# Eval("payroll_registry_nbr") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="center" />
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="REGISTRY DESCRIPTION" SortExpression="payroll_registry_descr">
                                                                    <ItemTemplate>
                                                                        <%# "&nbsp; "+Eval("payroll_registry_descr") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="30%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="GRP" SortExpression="payroll_group_nbr">
                                                                    <ItemTemplate>
                                                                        <%# Eval("payroll_group_nbr") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PAYROLL TEMPLATE" SortExpression="payrolltemplate_descr">
                                                                    <ItemTemplate>
                                                                        <%# Eval("payrolltemplate_descr") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="30%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GROSS" SortExpression="gross_pay">
                                                                    <ItemTemplate>
                                                                        <%# Eval("gross_pay") %> 
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                                    <ItemTemplate>
                                                                        <%# Eval("net_pay") %> 
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="STATUS" SortExpression="post_status_descr">
                                                                    <ItemTemplate>
                                                                        <%# Eval("post_status_descr") %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
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
                                                            <AlternatingRowStyle BackColor="" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <HeaderStyle BackColor="#ffc107" ForeColor="Black" VerticalAlign="Middle"  Font-Size="Small"  CssClass="td-header" />
                                                            <PagerStyle CssClass="pagination-ys" BackColor="#2461BF" ForeColor="White" HorizontalAlign="right" VerticalAlign="NotSet" Wrap="True" />
                                                            <RowStyle BackColor="#EFF3FB" Font-Size="Small" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                        
                                                        </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer" >
                            <button class="btn btn-success btn-sm" id="id_ot" onclick="print('OT')"><i class="fa fa-print"></i> Preview Annual Overtime</button>
                            <button class="btn btn-success btn-sm" id="id_cm"  onclick="print('CM')"><i class="fa fa-print"></i> Preview Coaching & Mentoring</button>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <%--<asp:LinkButton ID="lnk_print_rep" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnk_print_rep_Click"> <i class="fa fa-print"></i> Print </asp:LinkButton>--%>
                                    <%--<asp:LinkButton ID="lnk_generate_rep" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnk_generate_rep_Click"> <i class="fa fa-qrcode"></i> Generate </asp:LinkButton>--%>
                                    <asp:LinkButton ID="LinkButton1"  runat="server" data-dismiss="modal" Text ="Close" CssClass="btn btn-danger cancel-icon icn btn-sm" ></asp:LinkButton>  

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                     </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- The Modal - Coaching and Mentoring -->
        <asp:UpdatePanel ID="UpdatePanel29" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="modal_coaching_mentoring" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color:#17a2b8;color:white">
                            <h5 class="modal-title" ><asp:Label runat="server" Text="Coaching & Mentoring"></asp:Label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                            <ContentTemplate>
                                <div class="modal-body with-background">
                                    <div class="row">
                                        <div class="col-6" >
                                            <label>Payroll Year</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="payroll_year" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-6" >
                                            <label>Payroll Registry No</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="payroll_registry_nbr" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-12">
                                            <hr />
                                        </div>
                                        <div class="col-6" >
                                            <label>Date of Coaching</label>
                                            <asp:TextBox runat="server" CssClass="form-control my-date" ID="date_of_coaching" ></asp:TextBox>
                                            <asp:Label ID="date_of_coaching_req" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </div>
                                        <div class="col-12" >
                                            <label>Subject</label>
                                            <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ID="subject"></asp:TextBox>
                                            <asp:Label ID="subject_req" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </div>
                                        <div class="col-12" >
                                            <label>Particulars</label>
                                            <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" ID="particulars"></asp:TextBox>
                                            <asp:Label ID="particulars_req" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </div>
                                        <div class="col-12" >
                                            <label>Name of Incharge</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="name_of_incharge" ></asp:TextBox>
                                            <asp:Label ID="name_of_incharge_req" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </div>
                                        <div class="col-12" >
                                            <label>Name of Supervisor</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="name_of_supervisor"></asp:TextBox>
                                            <asp:Label ID="name_of_supervisor_req" runat="server" CssClass="lbl_required" ></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="modal-footer">
                                    <span class="label label-danger">
                                        <asp:Label ID="lbl_coaching_msg" runat="server" CssClass="smaller lbl_required" ></asp:Label>
                                    </span>
                                    <asp:LinkButton runat="server" CssClass="btn btn-danger pull-left" ID="lnkbtn_delete_coach" OnClick="lnkbtn_delete_coach_Click"><i class="fa fa-trash"></i> Delete Coaching</asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-success pull-left" ID="lnkbtn_print" OnClick="lnkbtn_print_Click"><i class="fa fa-print"></i> Save and Print</asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtn_save_coach" runat="server"  CssClass="btn btn-primary"  OnClick="lnkbtn_save_coach_Click"> <i class="fa fa-save"></i> Save Only</asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                     </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- The Modal - Select Report -->
        <%--<asp:UpdatePanel ID="UpdatePanel_id_receive_audit_post" ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="id_receive_audit_post" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                    <div class="modal-content" style="background-image:linear-gradient(white, lightblue)" >
                        <div class="modal-header" style="background-image:linear-gradient(green, yellow);padding:8px!important;" >

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <h5 class="modal-title" ><asp:Label ID="Label3" runat="server" Text="Receive" forecolor="White"></asp:Label></h5>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body with-background">
                            <div class="row">
                                <div class="col-12">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtb_hidden_registry_nbr" CssClass="form-control form-control-sm" Width="100%" Visible="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-6">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label5" runat="server" Text="Voucher Nbr:" CssClass="font-weight-bold" ></asp:Label>
                                            <asp:TextBox runat="server" ID="txtb_voucher_nbr" CssClass="form-control form-control-sm" Width="100%" MaxLength="15"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-6">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_date" runat="server" Text="Date Posted:" CssClass="font-weight-bold" ></asp:Label>
                                            <asp:TextBox runat="server" ID="txtb_date_posted" CssClass="form-control form-control-sm" Width="100%" Enabled="false"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="LinkButton4"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn"></asp:LinkButton>
                                    
                                    <asp:Label ID="Label4" runat="server" Visible="false"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                     </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>


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
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <div class="modal-body with-background">
                                    <div class="row">
                                        <div class="col-12" >
                                            <label>Registry No.</label>
                                            <asp:TextBox runat="server" ID="lbl_payrollregistry_nbr_print" Enabled="false" CssClass="form-control  form-control-sm"></asp:TextBox>
                                        </div>
                                        <div class="col-12" style="margin-bottom: 10px;">
                                            <label>Choose Report</label>
                                            <asp:DropDownList ID="ddl_select_report" CssClass="form-control form-control-sm" runat="server" Width="100%" OnSelectedIndexChanged="ddl_select_report_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="col-12" style="margin-bottom: 10px;">
                                            <button class="btn btn-success" id="id_payroll" onclick="print('PAYROLL')"><i class="fa fa-print"></i> Preview </button>
                                            <asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkPrint_Click"  OnClientClick="openLoading();" > <i class="fa fa-print"></i> Print </asp:LinkButton>
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

        <!-- The Modal - Add Confirmation -->
        <div class="modal fade" id="AddEditConfirm">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content text-center">
                    <!-- Modal body -->
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <i runat="server" id="id_icon" class="fa-5x fa fa-check-circle text-success"></i>
                                <h2 runat="server" id="id_header">Successfully</h2>
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
                                        <div class="col-4">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="Registry No:" CssClass="font-weight-bold" ></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtb_registry_no" CssClass="form-control form-control-sm" Enabled="false" Width="100%"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-8">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" Font-Bold="true" Text="Payroll Group:" ></asp:Label>
                                                    <asp:DropDownList ID="ddl_payroll_group" runat="server" CssClass=" form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_payroll_group_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:Label ID="LblRequired5" runat="server" CssClass="lbl_required" ></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                       
                                        

                                        <div class="col-12">
                                            <hr style="padding:0px;margin: 5px;"/>
                                            <ul class="nav nav-tabs" id="myTab" role="tablist">
                                              <li class="nav-item">
                                                <a class="nav-link active" id="home-tab-reg" data-toggle="tab" href="#homereg" role="tab" aria-controls="homereg" aria-selected="true">REGISTRY</a>
                                              </li>
                                              <li class="nav-item">
                                                <a class="nav-link " id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">OVERRIDES</a>
                                              </li>
                                              <li class="nav-item">
                                                <a class="nav-link" id="profile-tab" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false">OTHERS</a>
                                              </li>
                                            </ul>
                                            <div class="tab-content" id="myTabContent">
                                                <div class="tab-pane fade show active" id="homereg" role="tabpanel" aria-labelledby="home-reg">
                                                    <div class="form-group row">
                                                        <div class="col-md-12">
                                                            <hr style="padding:0px;margin: 5px;"/>
                                                            <h6 class="m-t-none m-b text-success">REGISTRY DESCRIPTION</h6>
                                                        </div>
                                                        
                                                        <div class="col-12">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text="Registry Description:" CssClass="font-weight-bold" ></asp:Label>
                                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_registry_descr" TextMode="MultiLine" Rows="3"> </asp:TextBox>
                                                                    <asp:Label ID="LblRequired1" runat="server" CssClass="lbl_required" ></asp:Label>
                                                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" >
                                                            <asp:UpdatePanel ID="UpdateDateFrom" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="Label8" runat="server" Text="Date From:" CssClass="font-weight-bold" ></asp:Label>
                                                    
                                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date" ID="txtb_period_from"> </asp:TextBox>
                                                                    <asp:Label ID="LblRequired2" runat="server" CssClass="lbl_required" ></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6">
                                                            <asp:UpdatePanel ID="UpdateDateTo" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="Label9" runat="server" Text="Date To:" CssClass="font-weight-bold" ></asp:Label>
                                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date" ID="txtb_period_to"> </asp:TextBox>
                                                                    <asp:Label ID="LblRequired3" runat="server" CssClass="lbl_required" ></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-6" style="margin-top:5px;" runat="server" id="div_no_works_days_1st" >
                                                             <asp:Label ID="lbl_dynamic" runat="server" Text="No. Days Work (1st):" CssClass="font-weight-bold" ></asp:Label>
                                                             <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_no_works_1st"> </asp:TextBox>
                                                             <asp:Label ID="LblRequired4" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </div>
                                                        <div class="col-6" style="margin-top:5px;" runat="server" id="div_no_works_days_2nd" >
                                                             <asp:Label ID="Label10" runat="server" Text="No. Days Work (2nd):" CssClass="font-weight-bold" ></asp:Label>
                                                             <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtb_no_works_2nd"> </asp:TextBox>
                                                             <asp:Label ID="LblRequired400" runat="server" CssClass="lbl_required" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label7" runat="server" CssClass="font-weight-bold"  Text="Payment Mode:"></asp:Label>
                                                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_payment_mode">
                                                                        <asp:ListItem Value="" Text="-- Select Here --"></asp:ListItem>
                                                                        <asp:ListItem Value="01" Text="To be Credited to ATM" Selected="true"></asp:ListItem>
                                                                        <asp:ListItem Value="02" Text="Over the Counter"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="LblRequired300" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                              <div class="tab-pane fade  " id="home" role="tabpanel" aria-labelledby="home-tab">
                                                  <div class="form-group row">

                                                      <div class="col-md-12">
                                                            <hr style="padding:0px;margin: 5px;"/>
                                                            <h6 class="m-t-none m-b text-success">OVERRIDES</h6>
                                                      </div>
                                        
                                                        <div class="col-md-12">
                                                        <asp:Label ID="Label11" runat="server" CssClass="font-weight-bold"  Text="Department:"></asp:Label>
                                                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddl_dep" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                                    <asp:Label ID="LblRequired301" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label13" runat="server" CssClass="font-weight-bold"  Text="Function code:"></asp:Label>
                                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddl_function_code" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoPostBack="true" ></asp:DropDownList>
                                                                    <asp:Label ID="LblRequired302" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label15" runat="server" CssClass="font-weight-bold"  Text="Allotment code:"></asp:Label>
                                                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtb_allotment_code" MaxLength="8"> </asp:TextBox>
                                                                    <asp:Label ID="Label16" CssClass="lbl_required" runat="server" Text=""></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-12" style="display:none">
                                                            <hr style="padding:0px;margin: 5px;"/>
                                                        </div>

                                                            <div class="col-6" style="display:none">
                                                            <asp:Label runat="server"  CssClass="font-weight-bold" Text="Status"></asp:Label>
                                                            <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddl_post_status" AutoPostBack="true" OnSelectedIndexChanged="ddl_post_status_SelectedIndexChanged">
                                                                <asp:ListItem Value="N" Text="NOT POSTED"></asp:ListItem>
                                                                <asp:ListItem Value="R" Text="RELEASED"></asp:ListItem>
                                                                <asp:ListItem Value="Y" Text="POSTED"></asp:ListItem>
                                                                <asp:ListItem Value="X" Text="VOIDED"></asp:ListItem>
                                                                <asp:ListItem Value="T" Text="RETURNED"></asp:ListItem>
                                                                <asp:ListItem Value="" Text="BAD DATA"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-6" style="display:none">
                                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Label runat="server"  CssClass="font-weight-bold" Text="Date Release"></asp:Label>
                                                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm my-date"  ID="txtb_date_release"> </asp:TextBox>
                                                                    <asp:Label ID="LblRequired6" runat="server" CssClass="lbl_required" ></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>       
                                                        </div>
                                                  </div>
                                              </div>
                                              <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                                                  <div class="form-group row">

                                                    <div class="col-md-12">
                                                        <hr style="padding:0px;margin: 5px;"/>
                                                        <h6 class="m-t-none m-b text-success">OTHERS</h6>
                                                    </div>

                                                    <div class="col-6">
                                                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label ID="Label18" runat="server" Text="Transmittal Nbr.:" CssClass="font-weight-bold" ></asp:Label>
                                                                <asp:TextBox runat="server" ID="txtb_transmittal_nbr" CssClass="form-control form-control-sm" Width="100%" MaxLength="10" ></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-12">
                                                        <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label ID="Label14" runat="server" Text="Remarks:" CssClass="font-weight-bold" ></asp:Label>
                                                                <asp:TextBox runat="server" ID="txtb_remarks" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                  </div>
                                              </div>
                                                
                                            </div>
                                        </div>

                                         <div class="col-12">
                                            <hr style="padding:0px;margin: 5px;"/>
                                        </div>
                                        <div class="col-12 text-center" >
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                        <asp:Label runat="server" ID="lbl_if_dateposted_yes" style="color:red;"></asp:Label>
                                                    </button>
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
                                        <asp:LinkButton ID="LinkButton2"  runat="server" data-dismiss="modal" Text ="Cancel" CssClass="btn btn-danger cancel-icon icn" ></asp:LinkButton>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary save-icon icn" onClick="btnSave_Click" />
                                        <asp:Label ID="hidden_report_filename" runat="server" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

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
                <div class="modal-header" style="border-bottom:0px !important">
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
                        <i id="msg_icon" runat="server" class="fa-5x fa fa-exclamation-triangle text-danger"></i>
                        <h4 id="msg_header" runat="server">Report File is Blank !</h4>
                        <asp:Label ID="lbl_details" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Button ID="btn_show_print_option" runat="server" CssClass="btn btn-primary" Text="OK, Show Report Options" OnClick="btn_show_print_option_Click" />
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
                <div class="col-3"><strong style="font-family:Arial;font-size:18px;color:white;"><%: Master.page_title %></strong></div>
                <div class="col-7">
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
               
                <div class="col-2 text-right">
                   <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="btn-group btn-block" role="group" aria-label="Button group with nested dropdown">
                                                  
                                <div class="btn-group btn-block  " role="group">
                                <button id="btnGroupDrop1" type="button" class="btn btn-secondary dropdown-toggle btn-block btn-warning btn-sm" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fa fa-cog"></i> Options
                                </button>
                                <div class="dropdown-menu" aria-labelledby="btn_payroll_per_employee">
                                    <asp:Button ID="btn_payroll_per_employee" runat="server" CssClass="dropdown-item btn-sm" OnClick="btn_payroll_per_employee_Click"  Text="Payroll Per Employee"/>
                                    <asp:Button ID="btn_annual_ovtm_rep" runat="server" CssClass="dropdown-item btn-sm" OnClick="btn_annual_ovtm_rep_Click"  Text="Annual Report for Overtime"/>
                                    <asp:Button ID="btn_coaching_list" runat="server" CssClass="dropdown-item btn-sm" OnClick="btn_coaching_list_Click"  Text="Coaching & Mentoring List"/>

                                </div>
                                </div>
                            </div>
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
                                                <asp:DropDownList ID="DropDownListID" runat="server" CssClass="form-control-sm " AppendDataBoundItems="true" AutoPostBack="True" OnTextChanged="DropDownListID_TextChanged" Width="30%" ToolTip="Show entries per page">
                                                    <asp:ListItem Text="5" Value="5" />
                                                    <asp:ListItem Text="10" Selected="True" Value="10" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                    <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="50" Value="50" />
                                                    <asp:ListItem Text="100" Value="100" />
                                                </asp:DropDownList>
                                            <asp:Label runat="server" Text="Entries" Font-Size="XX-Small"></asp:Label>
                                            |
                                            <asp:Label ID="show_pagesx" runat="server" Text="Page: 9/9"  Font-Size="XX-Small"></asp:Label>
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
                                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged1"></asp:DropDownList>
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
                                                    <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_month_SelectedIndexChanged">
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
                                <div class="col-4">
                                    <div class="form-group row">
                                        <div class="col-5 text-right">
                                            <asp:Label runat="server" Text="Employment Type:" ></asp:Label>
                                        </div>
                                        <div class="col-7">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_empl_type" runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_empl_type_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                        <asp:UpdatePanel ID="updatepanel_printall" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <% if (ddl_payroll_template.SelectedValue == "033" || ddl_payroll_template.SelectedValue == "052" )
                                                {%>
                                                    <asp:LinkButton runat="server" id="btn_printall" OnClick="btn_printall_Click" OnClientClick="checkSelectedRows()" CssClass="btn btn-success btn-sm btn-block"><i class="fa fa-print"></i> Print Selected</asp:LinkButton>
                                                <%
                                                } %>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                </div>
                                <div class="col-9">
                                    <div class="row">
                                        <div class="col-2" style="padding-right:0px !important" >
                                            <asp:Label runat="server" Text="Payroll Template:" ></asp:Label>
                                        </div>
                                        <div class="col-9">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                       
                                                    <asp:DropDownList ID="ddl_payroll_template"  runat="server" CssClass="form-control-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_payroll_template_SelectedIndexChanged"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-1">
                                            <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                <ContentTemplate>
                                            
                                                     <% if (ViewState["page_allow_add"].ToString() == "1" || 
                                                           ddl_payroll_template.SelectedValue    == "950" || // PHIC Share
                                                           ddl_payroll_template.SelectedValue    == "951"    // BAC Honorarium
                                                             )
                                                        {  %>
                                                    <asp:Button ID="btnAdd" runat="server" style="float:right;"  CssClass="btn btn-primary btn-sm add-icon icn"  Text="Add" OnClick="btnAdd_Click"  />
                                                    
                                                    <% }
                                                        %> 
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
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
                                               <%--<asp:TemplateField HeaderText="VOUCHER NO" SortExpression="voucher_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("voucher_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="13%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="REG #" SortExpression="payroll_registry_nbr">
                                                    <ItemTemplate>
                                                        <%if (ddl_payroll_template.SelectedValue == "033" || ddl_payroll_template.SelectedValue == "052") {%>
                                                        <asp:CheckBox 
                                                            runat="server" 
                                                            ID="check_reg" 
                                                            OnCheckedChanged="check_reg_CheckedChanged"
                                                            />
                                                        &nbsp;
                                                        <% } %>

                                                        <%# Eval("payroll_registry_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="REGISTRY DESCRIPTION" SortExpression="payroll_registry_descr">
                                                    <ItemTemplate>
                                                        <%# "&nbsp; "+Eval("payroll_registry_descr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="LEFT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="GRP #" SortExpression="payroll_group_nbr">
                                                    <ItemTemplate>
                                                        <%# Eval("payroll_group_nbr") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERIOD" SortExpression="payroll_period_descr">
                                                    <ItemTemplate>
                                                        <small>
                                                             <%# Eval("payroll_period_descr") %>
                                                        </small>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="CENTER" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GROSS" SortExpression="gross_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("gross_pay") %> &nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="NET PAY" SortExpression="net_pay">
                                                    <ItemTemplate>
                                                        <%# Eval("net_pay") %> &nbsp;
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="RIGHT" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="STATUS" SortExpression="post_status_descr">
                                                    <ItemTemplate>
                                                        <span class="badge <%# Eval("post_status").ToString().Trim() == "T" || Eval("post_status").ToString().Trim() == "N" ? "badge-danger" : "badge-primary" %>">
                                                            <small>
                                                                &nbsp;<%# Eval("post_status_descr") %>
                                                            </small>
                                                        </spa>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                                            <ContentTemplate>
                                                                    
                                                                    <% 
                                                                        
                                                                    %>
                                                                    <asp:ImageButton 
                                                                        ID="imgbtn_add_empl" 
                                                                        runat="server" 
                                                                        EnableTheming="true"  
                                                                        CssClass="btn btn-warning action btn-sm" 
                                                                        ImageUrl="~/ResourceImages/add.png" 
                                                                        style="padding-left: 0px !important;" 
                                                                        OnCommand="imgbtn_add_empl_Command" 
                                                                        CommandArgument='<%# Eval("payroll_registry_nbr") +","+Eval("payroll_group_nbr")%> ' 
                                                                        tooltip="Registry Details"
                                                                         Visible='<%# Eval("crud_name").ToString().Trim() == "" ? false : true %>'
                                                                        />
                                                                    <%   

                                                                    %>
                                                                    
                                                                <% 
                                                                    if (ViewState["page_allow_edit"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton 
                                                                        ID="imgbtn_editrow1" 
                                                                        CssClass="btn btn-primary action btn-sm" 
                                                                        EnableTheming="true"  
                                                                        runat="server"  
                                                                        ImageUrl="~/ResourceImages/final_edit.png" 
                                                                        OnCommand="editRow_Command" 
                                                                        CommandArgument='<%# Eval("payroll_registry_nbr")%> ' 
                                                                        tooltip="Edit"
                                                                        />
                                                                        
                                                        
                                                                <%   }
                                                                %>

                                                                <% if (ViewState["page_allow_delete"].ToString() == "1")
                                                                    {
                                                                %>
                                                                    <asp:ImageButton 
                                                                        ID="lnkDeleteRow" 
                                                                        CssClass="btn btn-danger action btn-sm" 
                                                                        EnableTheming="true" 
                                                                        runat="server"  
                                                                        ImageUrl='<%# Eval("post_status").ToString().Trim() == "T" ? "~/ResourceImages/void.png" : "~/ResourceImages/final_delete.png" %>'
                                                                        OnCommand="deleteRow_Command" 
                                                                        CommandArgument='<%# Eval("payroll_registry_nbr")+","+Eval("payroll_year")+","+Eval("post_status")%> '   
                                                                        tooltip='<%# Eval("post_status").ToString().Trim() == "T" ? "Void" : "Delete" %>'
                                                                        Enabled='<%# Eval("post_status").ToString().Trim() == "N" || Eval("post_status").ToString().Trim() == "T" ? true  : false  %>'
                                                                        Visible='<%#  Eval("crud_name").ToString().Trim() == "" ? false : true %>'
                                                                        />
                                                                <% }
                                                                %>
                                                                <% if (ViewState["page_allow_print"].ToString() == "1")
                                                                    {
                                                                %>
                                                                     <asp:ImageButton ID="imgbtn_print" 
                                                                         CssClass="btn btn-success action btn-sm" 
                                                                         EnableTheming="true" 
                                                                         runat="server" 
                                                                         ImageUrl="~/ResourceImages/print1.png" 
                                                                         style="padding-left: 0px !important;" 
                                                                         OnCommand="imgbtn_print_Command1" 
                                                                         CommandArgument='<%# Eval("payroll_registry_nbr")+","+Eval("payroll_group_nbr")+","+Eval("payment_mode")%> ' 
                                                                         tooltip="Print"/>
                                                                <%  }
                                                                %>

                                                                <asp:ImageButton runat="server" ID="imgbtn_coaching" 
                                                                    tooltip="Coaching & Mentoring"
                                                                    OnCommand="imgbtn_coaching_Command"
                                                                    ImageUrl="~/ResourceImages/final_select.png" 
                                                                    style="padding-left: 5px !important;padding-right: 5px !important;" 
                                                                    CommandArgument='<%# Eval("payroll_year")+","+Eval("payroll_registry_nbr")%> ' 
                                                                    CssClass='<%# Eval("coaching_status").ToString() == "" ? "btn btn-info action btn-sm" : "btn btn-danger action btn-sm" %>' />

                                                              
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
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
                                    <asp:AsyncPostBackTrigger ControlID="ddl_year" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_payroll_template" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_payroll_group" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        </div>

        <div class="modal fade" id="modal_print_preview" tabindex="-1" role="dialog" aria-labelledby="modalLabelSmall" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document" >
                <div class="modal-content  modal-content-add-edit">
                    <div class="modal-header bg-success" >
                            <h5 class="modal-title text-white" ><asp:Label runat="server" Text="Preview Report"></asp:Label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                    <div class="modal-body with-background" style="padding:0px !important">
                        <div class="row">
                            <div class="col-lg-12">
                                <iframe style="width:100% !important;height:700px !important;border:0px none;" id="iframe_print_preview"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</form>

    <script type="text/javascript">
       function openModal()
       {
            $('#add').modal({
                keyboard: false,
                backdrop:"static"
            });
            show_date();
            hightlight();
       };
       function show_date()
       {
           $('#<%= txtb_period_from.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
           $('#<%= txtb_period_to.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
           $('#<%= txtb_date_release.ClientID %>').datepicker({ format: 'yyyy-mm-dd HH:mm:ss' });
           $('#<%= date_of_coaching.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });

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
        }
        function closeModal()
        {
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
        function closeModal1()
        {
            $('#add').modal("dispose");
        };
        function openModalDelete()
        {
            $('#deleteRec').modal({
                keyboard: false,
                backdrop:"static"
            });
        };
        function openNotification()
        {
            $('#notification').modal({
                keyboard: false,
                backdrop:"static"
            });
        };

        function closeModalDelete()
        {
            $('#deleteRec').modal('hide');
        };
        function openSelectReport()
         {
            $('#SelectReport').modal({
                keyboard: false,
                backdrop:"static"
            });
        };
        function openLoading()
        {

            $('#Loading').modal({
                keyboard: false,
                backdrop: "static"
            });
        }
        function closeNotification() {

            $('#notification').modal("hide");
        }
        function openCoaching()
        {
            $('#modal_coaching_mentoring').modal({
                keyboard: false,
                backdrop:"static"
            });

            $('#<%= date_of_coaching.ClientID %>').datepicker({ format: 'yyyy-mm-dd' });
        }

        function closeCoaching()
        {
            $('#modal_coaching_mentoring').modal("hide");
             $('#AddEditConfirm').modal({
                 keyboard: false,
                backdrop:"static"
            });
            setTimeout(function ()
            {
                $('#AddEditConfirm').modal("hide");
                $('.modal-backdrop.show').remove();
            }, 800);
        };
    </script>

    <script type="text/javascript">

        function formatState(state)
        {
            var image_link = "http://192.168.5.218/storage/images/photo/thumb/";
            if (!state.id) {
                return state.text;
            }
            var baseUrl = (state.empl_photo == "" ? "../../ResourceImages/upload_profile.png" : image_link + state.id) ;
            var $state = $(
                            '<span><img alt="image" class="img-circle" width="50" height="50" src="' + baseUrl + '" class="img-flag" /> ' + state.text + '</span>'
                          );
            return $state;
        }
        var reg_nbrs= "";
        var datalistgrid;
        var oTable;
        var domain   = window.location.hostname;
        var api_link = "http://hris.dvodeoro.local:90/api/ListOfEmployee";
        if (domain == "hris.dvodeoro.ph")
        {
            api_link = "https://hris.dvodeoro.ph:450/api/ListOfEmployee"
        }
        console.log(domain)
        console.log(api_link)
        function openModalPayroll()
        {
            var text = $('#<%= lbl_select_option.ClientID %>').text()
            var button_id_ot = document.getElementById('id_ot');
            var button_id_cm = document.getElementById('id_cm');
            button_id_ot.style.display = 'none';
            button_id_cm.style.display = 'none';
            if (text == "Annual Report for Overtime Payroll")
            {
                button_id_ot.style.display = 'flex';
                button_id_cm.style.display = 'none';
            }
            if (text == "Coaching & Mentoring List")
            {
                button_id_ot.style.display = 'none';
                button_id_cm.style.display = 'flex';
                // Retrieve Payroll Template
                PayrollTemplateList($('#empl_type_filter').val());
            }

            // Retrieve Department
            DepartmentList()
            // Retrieve Employee
            RetrieveEmployee()
            
            // Initialized Datatable
            init_table_data([]);
            
            var year    = $('#year_filter').val();
            var month   = $('#month_filter').val();
            $('#year_filter').on('change', function (e)
            {
                RetrieveGrid($('#ddl_employee_name option:selected').val(),year,month);
            });
            $('#month_filter').on('change', function (e)
            {
                RetrieveGrid($('#ddl_employee_name option:selected').val(),year,month);
            });
            $('#empl_type_filter').on('change', function (e)
            {
                PayrollTemplateList($('#empl_type_filter').val());
            });

            $('#modal_payroll_list').modal({keyboard: false,backdrop:"static"});
        };

        function RetrieveGrid(empl_id,year,month)
        {
            var empl_id = empl_id;
            var year    = $('#year_filter').val();
            var month   = $('#month_filter').val();
            $.ajax({
                type        : "POST",
                url         : "cPayRegistry.aspx/RetrieveGrid",
                data        : JSON.stringify({ empl_id: empl_id,year: year ,month:month }),
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    oTable.fnClearTable();
                    datalistgrid = parsed;
                    if (parsed)
                    {
                        if (parsed.length > 0)
                        {
                            oTable.fnAddData(parsed);
                            $('#department_filter').val(parsed[0].department_code)
                        }
                        else
                        {
                            $('#department_filter').val('')
                            alert("No Data Found!");
                        }
                    }
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }
        
        function DepartmentList()
        {
            $.ajax({
                type        : "POST",
                url         : "cPayRegistry.aspx/DepartmentList",
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    if (parsed.length > 0)
                    {
                        var select = document.getElementById("department_filter");
                        // Clear existing options if any
                        select.innerHTML = "";
                        // Add Empty
                        var option1      = document.createElement("option");
                        option1.text     = "-- Select Here--"; 
                        option1.value    = "";
                        select.appendChild(option1);
                        // Add options to the select element
                        for (var i = 0; i < parsed.length; i++)
                        {
                            var option      = document.createElement("option");
                            option.text     = parsed[i].department_name1; 
                            option.value    = parsed[i].department_code; 
                            select.appendChild(option);
                        }
                    } else
                    {
                        alert("No Data Found!");
                    }
                    
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }

        function RetrieveEmployee()
        {
            var ddl = $('.js-example-basic-single').select2(
             {
                dropdownParent      : $('#modal_payroll_list'),
                templateResult      : formatState,
                minimumInputLength  : 3,
                placeholder         : "Select Employee",
                ajax:
                    {
                    url         : api_link,
                    dataType    : 'json',
                    data        : (params) =>
                    {
                        return {
                            term: params.term,
                        }
                    },
                    processResults: (data, params) =>
                    {
                        const results = data.map(item => {
                            return {
                                id              : item.empl_id,
                                text            : item.empl_id + " - " + item.employee_name,
                                empl_photo      : item.empl_photo
                            };
                        });
                        return{
                            results: results,
                        }
                    },
                },
            });

            ddl.on('select2:select', function (e)
            {
                var selectedValue = e.params.data.id;
                RetrieveGrid(selectedValue,$('#year_filter').val(),$('#month_filter').val())
            })
        }

        function PayrollTemplateList()
        {
            var empl_type = $('#empl_type_filter').val()
            $.ajax({
                type        : "POST",
                url         : "cPayRegistry.aspx/PayrollTemplateList",
                data        : JSON.stringify({ empl_type: empl_type }),
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    if (parsed.length > 0)
                    {
                        var select = document.getElementById("payrolltemplate_filter");
                        // Clear existing options if any
                        select.innerHTML = "";
                        // Add Empty
                        var option1      = document.createElement("option");
                        option1.text     = "-- Select Here--"; 
                        option1.value    = "";
                        select.appendChild(option1);
                        // Add options to the select element
                        for (var i = 0; i < parsed.length; i++)
                        {
                            var option      = document.createElement("option");
                            option.text     = parsed[i].payrolltemplate_descr; 
                            option.value    = parsed[i].payrolltemplate_code; 
                            select.appendChild(option);
                        }
                    } else
                    {
                        var select = document.getElementById("payrolltemplate_filter");
                        // Clear existing options if any
                        select.innerHTML = "";
                        // Add Empty
                        var option1      = document.createElement("option");
                        option1.text     = "-- Select Here--"; 
                        option1.value    = "";
                        select.appendChild(option1);
                    }
                    
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                }
            });
        }

        var init_table_data = function (par_data)
        {
            datalistgrid = par_data;
            oTable       = $('#datalist_grid').dataTable(
                {
                    data        : datalistgrid,
                    sDom        : 'rt<"bottom"ip>',
                    pageLength  : 10,
                    columns:
                    [
                        {
                            "mData": "payroll_registry_nbr",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-center btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "payroll_registry_descr",
                            "mRender": function (data, type, full, row) {
                                return "<span>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "payroll_group_nbr",
                            "mRender": function (data, type, full, row) {
                                 return "<span>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "payrolltemplate_descr",
                            "mRender": function (data, type, full, row) {
                                return "<span >" + data + "</span>"
                            }
                        },
                        {
                            "mData": "gross_pay",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-center   btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "net_pay",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-center   btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "post_status_descr",
                            "mRender": function (data, type, full, row)
                            {
                                if (full["post_status"] == "N" ||full["post_status"] == "T")
                                {
                                    return "<span class='text-center badge badge-danger'>" + data + "</span>"
                                } else
                                {
                                    return "<span class='text-center badge badge-primary'>" + data + "</span>"
                                }
                            }
                        },
                    ],
                });
        }
        
        function print(report_type)
        {
            // *******************************************************
            // *******************************************************
            var sp          = ""
            var year        = $('#year_filter').val();
            var month       = $('#month_filter').val();
            var emp_type    = $('#empl_type_filter').val();
            var dep         = $('#department_filter').val();
            var template    = $('#payrolltemplate_filter').val();
            
            if (report_type == 'OT')
            {
                ReportPath = "~/Reports/cryOvertimeAnnual/cryOvertimeAnnual.rpt";
                sp          = ReportPath+","+"sp_payrollregistry_ovtm_annual_rep,par_year," + year + ",par_month," + month+ ",par_employment_type," + emp_type + ",par_department_code," + dep;
                previewReport(sp,report_type)
            }
            else if (report_type == 'CM')
            {
                ReportPath  = "~/Reports/cryCoachingList/cryCoachingList.rpt";
                sp          = ReportPath+","+"sp_payrollregistry_hdr_coaching_tbl_rep,p_payroll_year," + year + ",p_payroll_month," + month + ",p_payroll_registry_nbr," + "" + ",p_payrolltemplate_code," + template;
                
                previewReport(sp,report_type)
            }
            else 
            {
                if ($('#<%= ddl_select_report.ClientID %>').val() == "" || $('#<%= ddl_select_report.ClientID %>').val() == "X")
                {
                    alert('Please Select Report')
                    return;
                }
                else if ($('#<%= lbl_payrollregistry_nbr_print.ClientID %>').val() == "")
                {
                    alert('Please check atleast 1 (one) Registry')
                    return;
                }
                else
                {
                    var data = JSON.stringify({
                                             ddl_select_report     : $('#<%= ddl_select_report.ClientID %>').val()
                                            , ddl_year             : $('#<%= ddl_year.ClientID %>').val()
                                            , ddl_month            : $('#<%= ddl_month.ClientID %>').val()
                                            , payroll_registry_nbr : $('#<%= lbl_payrollregistry_nbr_print.ClientID %>').val()
                                            , ddl_payroll_template : $('#<%= ddl_payroll_template.ClientID %>').val()
                                            , ddl_empl_type        : $('#<%= ddl_empl_type.ClientID %>').val()
                                            });
                    $.ajax({
                        type        : "POST",
                        url         : "cPayRegistry.aspx/PayrollPrintPreview",
                        data        : data,
                        contentType : "application/json; charset=utf-8",
                        dataType    : "json",
                        success: function (response)
                        {
                            // Direct to Preview/Print All
                            if ($('#<%= ddl_select_report.ClientID %>').val() == "309"
                             || $('#<%= ddl_select_report.ClientID %>').val() == "310"
                             || $('#<%= ddl_select_report.ClientID %>').val() == "311"
                             || $('#<%= ddl_select_report.ClientID %>').val() == "033"
                             || $('#<%= ddl_select_report.ClientID %>').val() == "052"
                                )
                            {
                                location.href = "/printViewAll/printViewAll.aspx";
                            }
                            else
                            {
                                sp = response.d
                                previewReport(sp,report_type)

                            }
                        },
                        failure: function (response)
                        {
                            alert("Error: " + response.d);
                        }
                    });
                }
            }
        }

        function previewReport(sp,report_type)
        {
            // *******************************************************
            // *** VJA : 2021-07-14 - Validation and Loading hide ****
            // *******************************************************
            var ReportName      = "CrystalReport"
            var SaveName        = "Crystal_Report"
            var ReportType      = "inline"
            var ReportPath      = ""
            var iframe          = document.getElementById('iframe_print_preview');
            var iframe_page     = $("#iframe_print_preview")[0];
            var embed_link;
            iframe.style.visibility = "hidden";
            if (report_type == "PAYROLL")
            {
                embed_link = sp;
            }
            else
            {
                embed_link = "../../printView/CrystalViewer.aspx?Params=" + ""
                    + "&ReportName=" + ReportName
                    + "&SaveName="   + SaveName
                    + "&ReportType=" + ReportType
                    + "&ReportPath=" + ReportPath
                    + "&id=" + sp // + "," + parameters
            }

            if (!/*@cc_on!@*/0) { //if not IE
                iframe.onload = function () {
                    iframe.style.visibility = "visible";
                };
            }
            else if (iframe_page.innerHTML()) {
                // get and check the Title (and H tags if you want)
                var ifTitle = iframe_page.contentDocument.title;
                if (ifTitle.indexOf("404") >= 0)
                {
                    swal("You cannot Preview this Report", "There something wrong!", { icon: "warning" });
                    iframe.src = "";
                }
                else if (ifTitle != "")
                {
                    swal("You cannot Preview this Report", "There something wrong!", { icon: "warning" });
                    iframe.src = "";
                }
            }
            else {
                iframe.onreadystatechange = function ()
                {
                    if (iframe.readyState == "complete")
                    {
                        iframe.style.visibility = "visible";
                    }
                };
            }
            console.log(embed_link)
            iframe.src = embed_link;
            $('#modal_print_preview').modal({ backdrop: 'static', keyboard: false });
            // *******************************************************
            // *******************************************************
        }
        
        
        <%--function onCheckboxChange(chkBox)
        {
            if (chkBox.checked)
            {
                var row = chkBox.parentNode.parentNode;
                var cells = row.getElementsByTagName("td");
                var data = [];
                for (var i = 0; i < cells.length; i++)
                {
                    data.push(cells[i].innerText);
                }
                reg_nbrs += (data[0] + "-").replace(" ","")
            }
            $('#<%= lbl_payrollregistry_nbr_print.ClientID %>').val(reg_nbrs)
        }--%>
        function checkSelectedRows()
        {
            reg_nbrs = "";
            var grid = document.getElementById("<%= gv_dataListGrid.ClientID %>");
            var checkboxes = grid.getElementsByTagName("input");
            var selectedRows = [];

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type == "checkbox" && checkboxes[i].checked) {
                    var row = checkboxes[i].parentNode.parentNode;
                    var cells = row.getElementsByTagName("td");
                    var data = [];
                    for (var j = 0; j < cells.length; j++) {
                        data.push(cells[j].innerText);
                    }
                    reg_nbrs += (data[0] + "-").trim().replace(" ","")
                }
            }

            $('#<%= lbl_payrollregistry_nbr_print.ClientID %>').val(reg_nbrs)
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
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
</asp:Content>
