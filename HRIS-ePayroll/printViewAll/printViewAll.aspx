<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="printViewAll.aspx.cs" Inherits="HRIS_ePayroll.prinview.trypreview1" validateRequest="false"%>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
        Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
   <meta content="text/html; charset=utf-8" http-equiv="Content-Type">

    
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
    <asp:ScriptManager ID="sm_Script" runat="server" >
    </asp:ScriptManager>


        <!-- The Modal - Add Confirmation -->
            <div class="modal fade" id="AddEditConfirm">
              <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content text-center">
                  <!-- Modal body -->
                       
                     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                     <ContentTemplate>
 
                    <div class="modal-body">
                      <i class="fa-5x fa fa-check-circle text-success"></i>
                      <h2>Successfully Printed</h2>
                        </br>
                          <h6 ><asp:Button ID="Button2" runat="server"  Text="OK" CssClass="btn btn-success" OnClientClick="openLoading();" OnClick="Button2_Click"/></h6>
                         </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>       
                     
                      

                    
                  <!-- Modal footer -->
                  <div style="margin-bottom:30px">
                  </div>
                </div>
              </div>
            </div>


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

        <asp:UpdatePanel ID="up_print" runat="server" style="display:none;" autopostback="true">
            <ContentTemplate>
            <asp:HiddenField ID="hf_nexpage" runat="server" />
            <asp:HiddenField ID="hf_printers" runat="server" />
                <div class="col-12"">
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        
        <div class="row" id="div_cont">
            <%= obj_url_obj %>
        </div>

                    
     </form>

    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server" >
    
  

    <script type="text/javascript">
     
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args){
                if (args.get_error() != undefined){
                    args.set_errorHandled(true);
                }
        }

        (function () {
                 
           var ua = window.navigator.userAgent;
            var isIE = /MSIE|Trident/.test(ua);
            var edge = ua.indexOf('Edge/');

            printMe(); // Call print method.

            if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1 ||
                isIE ||
                edge > 0)
            {
                openModalDelete();
            }
            else
            {
                openModalDelete_Google()
            }

        })();
        

        function printMe()

        {
            
            var report_obj_parse  = document.getElementById('report_obj');


            if (!window.location.hash)
            {
                window.location = window.location + '#loaded';
                window.location.reload();
            }

            

            else
            {

                var ua = window.navigator.userAgent;
                var isIE = /MSIE|Trident/.test(ua);
                var edge = ua.indexOf('Edge/');

                if (isIE)
                {
                    //IE specific code goes here

                    try
                    {
                        setTimeout(function () {
                            report_obj_parse.focus();
                            report_obj_parse.print();
                        }, 1000);
                    }
                    catch (e)
                    {
                        setTimeout(function () {
                        report_obj_parse.contentWindow.document.execCommand('print', false, null);
                        }, 1000);
                    }

                }



                else if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1)
                {

                    //FF specific code goes here
                   window.open('<%= obj_url1 %>');
                    

                 }

                else if (edge > 0) {

                   //Edge specific code goes here
                    try
                    {

                        setTimeout(function () {
                        report_obj_parse.contentWindow.document.execCommand('print', false, null);
                        }, 1000);
                    }

                     catch (e)
                        {
                              try
                              {
                                 setTimeout(function () {
                                   $("#report_obj").get(0).contentWindow.print();
                                 }, 1000);
                              }

                              catch (e)
                              {
                                  setTimeout(function () {
                                  window.frames["report_obj"].focus();
                                  window.frames["report_obj"].print(); 
                                  }, 1000);
                              }
                            
                       }  

                 }


                else  // For other browser
                {
                    setTimeout(function () {
                        window.frames["report_obj"].focus();
                        //window.frames["report_obj"].print(); 
                        $("#report_obj").get(0).contentWindow.print();
                    }, 1000);
                }

            }
            
        }


        function openModalDelete_Google()
        {
            $('#AddEditConfirm').modal({
                keyboard: false,
                backdrop:"static"
            });
        }    
        function openModalDelete()
        {

            setTimeout(function () {
                $('#AddEditConfirm').modal({
                keyboard: false,
                backdrop:"static"
                });
            }, 2300);

            setTimeout(function () {
               $('#<%= Button2.ClientID %>').click()
               openLoading()
              }, 2000);
           
            
        }

        
        function openLoading()
        {

            $('#Loading').modal({
                keyboard: false,
                backdrop: "static"
            });
        }

    </script>





</asp:Content>
