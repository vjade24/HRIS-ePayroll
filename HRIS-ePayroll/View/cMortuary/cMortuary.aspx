<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cMortuary.aspx.cs" Inherits="HRIS_ePayroll.View.cMortuary.cMortuary" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server">
    <asp:ScriptManager ID="sm_Script" runat="server"> </asp:ScriptManager>
        <div class="row">
            <div class="col-lg-12">
                <div class="card mb-2">
                    <div class="card-header" style="background-color:#507cd1 !important;color:white">
                       
                        <div class="form-group row">
                            <div class="col-lg-6">
                                 <i class="fa fa-table me-1"></i>
                                 Mortuary
                            </div>
                            
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive smaller" >
                        <div class="form-group row pr-4 pl-3">
                            <div class="col-lg-2">
                                <label>Show</label>
                                <select id="show_filter" class="form-control form-control-sm">
                                    <option value="10">10</option>
                                    <option value="25">25</option>
                                    <option value="50">50</option>
                                    <option value="100">100</option>
                                </select>
                            </div>
                            <div></div>
                            <div class="col-lg-2">
                                <label>Payroll Year</label>
                                <select id="year_filter" class="form-control form-control-sm">
                                    <option value="2025">2025</option>
                                    <option value="2024">2024</option>
                                </select>
                            </div>
                            <div class="col-lg-2">
                                <label>Payroll Month</label>
                                <select id="month_filter" class="form-control form-control-sm">
                                    <option value="01">January</option>
                                    <option value="02">February</option>
                                    <option value="03">March</option>
                                    <option value="04">April</option>
                                    <option value="05">May</option>
                                    <option value="06">June</option>
                                    <option value="07">July</option>
                                    <option value="08">August</option>
                                    <option value="09">September</option>
                                    <option value="10">October</option>
                                    <option value="11">November</option>
                                    <option value="12">December</option>
                                </select>
                            </div>
                        </div>
                            <table class="table table-hover table-bordered" id="datalist_grid" style="padding:0px !important">
                                <thead >
                                    <tr>
                                        <th scope="col" style="background-color:#507cd1 !important;color:white !important;width:10% !important">ID</th>
                                        <th scope="col" style="background-color:#507cd1 !important;color:white !important;width:50% !important">NAME</th>
                                        <th scope="col" style="background-color:#507cd1 !important;color:white !important;width:10% !important">AMOUNT 1</th>
                                        <th scope="col" style="background-color:#507cd1 !important;color:white !important;width:10% !important">AMOUNT 2</th>
                                        <th scope="col" style="background-color:#507cd1 !important;color:white !important;width:10% !important"></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="Loading_master">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-dialog modal-sm">
                    <span class="fa fa-spinner fa-spin fa-3x text-white"></span>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
   
    <script type="text/javascript">
        function hightlight() { }
        
        var datalistgrid;
        var oTable;
        
        init()
        function init()
        {
            datatable()

            $('#year_filter').on('change', function (e)
            {
                datatable()
            });
            $('#month_filter').on('change', function (e)
            {
                datatable()
            });
        }
        // Datatable
        function datatable()
        {
            var year          = $('#year_filter').val()
            var month         = $('#month_filter').val()
            //$('#Loading_master').modal({ keyboard: false, backdrop: "static" });
            $.ajax({
                type        : "POST",
                url         : "cMortuary.aspx/MortuaryList",
                data        : JSON.stringify({ payroll_year: year,payroll_month:month }),
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    if (!datalistgrid)
                    {
                        init_table_data([]);
                    }
                    
                    var parsed = JSON.parse(response.d)
                    oTable.fnClearTable();
                    datalistgrid = parsed;

                    if (parsed)
                    {
                        if (parsed.length > 0)
                        {
                            oTable.fnAddData(parsed);
                        }
                        else
                        {
                            //alert("No Data Found!");
                            $('#Loading_master').modal("hide");
                            oTable.fnClearTable();
                        }
                    }
                    $('#Loading_master').modal("hide");
                    
                },
                failure: function (response)
                {
                    $('#Loading_master').modal("hide");
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
                    pageLength  : 10,
                    sDom        : 'rt<"bottom"p>',
                    columns:
                    [
                        {
                            "mData": "empl_id",
                            "mRender": function (data, type, full, row)
                            {
                                 return "<span class='text-center   btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "employee_name",
                            "mRender": function (data, type, full, row) {
                                 return "<span class='   btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "deduct_amt1",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-right   btn-block'>" + currency(data) + "</span>"
                            }
                        },
                        {
                            "mData": "deduct_amt2",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-right   btn-block'>" + currency(data) + "</span>"
                            }
                        },
                        {
                            "mRender": function (data, type, full, row) {
                                return ""
                            }
                        },
                    ],
                });
        }
        function currency(d)
        {
            var retdata = ""
            if (d == null || d == "" || d == undefined) {
                return retdata = "0.00"
            }
            else {
                retdata = d.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
                return retdata
            }
        }
    </script>
</asp:Content>
