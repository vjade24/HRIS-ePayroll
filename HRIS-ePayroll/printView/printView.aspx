<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="printView.aspx.cs" Inherits="HRIS_ePayroll.prinview.trypreview" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
        Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
    <style type="text/css">
        .print-scroll {
            min-width:100% !important;
        }
        .nextback
        {
            margin-left: 905px;
        }
        .marg{
            margin-left:2px;
            margin-right:2px;
        }
        div#MainContainer_crvPrint__UI_mb,div#MainContainer_crvPrint__UI_bc{
                background-color: rgb(136, 136, 136);
                position: fixed;
                opacity: 0.3;
                display: none;
                z-index: 998;
                visibility: hidden;
                width: 0px !important;
                height:0px !important;
                left:0px !important;
                top: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
        
            <asp:HiddenField ID="hf_nexpage" runat="server" />
            <asp:HiddenField ID="hf_printers" runat="server" />

        <%--<div class="col-12"">
            <%
                string urlsX = "";
                if (Session["history_page"] != null) {
                    urlsX = Session["history_page"].ToString();
                    Session.Remove("history_page");
                }
                

            %>
            <div class="form-group row" style="background-image:linear-gradient(to right,blue,white);border-radius:5px;padding-top:10px;padding-bottom:10px;padding-right:10px;margin-top:-10px">
                <div class="col-10">
                    <h5 id="headerpreview" runat="server" class="text-white font-weight-bold">Print Preview</h5>
                </div>
                <div class="col-2 text-right">
                    <a ID="lb_back" class="btn btn-info btn-sm font-weight-bold" href="<%= urlsX.Trim() == "" ? "javascript:history.go(-1)" : urlsX %>" style="width:80px"><i class="fa fa-arrow-left"></i> Back</a>&nbsp;
                </div>
            </div>
        </div>--%>
    <asp:UpdatePanel ID="up_print" runat="server">
        <ContentTemplate>
        <div class="row">
            <div class="col-12">
                <div id="crvHolder" style="overflow:scroll;height:100%;width:100%;">
                    <CR:CrystalReportViewer 
                        ID                       ="crvPrint" 
                        runat                    ="server" 
                        PageZoomFactor           ="100"
                        HasToggleGroupTreeButton ="False" 
                        ToolPanelView            ="None" 
                        HasPrintButton           ="true"
                        HasExportButton          ="true"
                        InteractiveDeviceInfos   ="(Collection)" 
                        WaitMessageFont-Names    ="Verdana"
                        ShowPrintButton          ="true" 
                        ToolPanelWidth           ="200px" 
                        HasGotoPageButton        ="True" 
                        HasSearchButton          ="True"
                        HasRefreshButton         ="True"
                        HasDrilldownTabs         ="False" 
                        DisplayStatusbar         ="False" 
                        HasPageNavigationButtons ="True" 
                        EnableDatabaseLogonPrompt="False" 
                        EnableDrillDown          ="False" 
                        EnableParameterPrompt    ="False" 
                        HasCrystalLogo           ="False" 
                        HasDrillUpButton         ="False"
                        CssClass                 ="print-scroll"
                        PrintMode                ="ActiveX"
                        OnLoad                   ="crvPrint_Load"
                        />
                </div>
            </div>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#sidenavToggler').click()
        });
        function hightlight()
        {
        }
    </script>
</asp:Content>
