<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cDashboard.aspx.cs" Inherits="HRIS_ePayroll.View.cDashboard" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="specific_css" runat="server">
    <style type="text/css">
        .dataTables_wrapper
        {
            padding:10px !important;
        }
        span.details-control {
            background: url('../../ResourceImages/show.jpg') no-repeat center center;
            background-size: 20px;
            width: 20px;
            height: 20px;
            cursor: pointer;
        }

        tr.shown span.details-control 
        {
            background: url('../../ResourceImages/unshow.jpg') no-repeat center center;
            background-size: 20px;
            width: 20px;
            height: 20px;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="server">
    <form runat="server" >
        <asp:ScriptManager ID="sm_Script" runat="server" AsyncPostBackTimeOut="36000"> </asp:ScriptManager>
        <div class="container-fluid px-4">
            <h2 class="">Dashboard</h2>
            <%--<ol class="breadcrumb mb-2">
                <li class="breadcrumb-item active">Dashboard</li>
            </ol>--%>
            <div class="row">
                <div class="col-lg-12">
                    <ul class="nav nav-pills nav-justified" id="myTab" role="tablist">
                      <li class="nav-item" role="presentation">
                        <button class="nav-link active btn btn-block" id="home-tab" data-toggle="tab" data-target="#home" type="button" role="tab" aria-controls="home" aria-selected="true">Appropriation</button>
                      </li>
                      <li class="nav-item" role="presentation">
                        <button class="nav-link btn btn-block" id="profile-tab" data-toggle="tab" data-target="#profile" type="button" role="tab" aria-controls="profile" aria-selected="false">Payrolls</button>
                      </li>
                    </ul>
                    <div class="tab-content" id="myTabContent">
                      <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                        <div class="row mb-2 mt-2">
                            <div class="col-lg-3">
                                <label>Appropriation Year</label>
                                <select class="form-control" id="appropriation_year">
                                    <option value="2025" selected>2025</option>
                                    <option value="2024" >2024</option>
                                    <option value="2023">2023</option>
                                    <option value="2022">2022</option>
                                    <option value="2021">2021</option>
                                </select>
                            </div>
                            <div class="col-lg-6">
                                <label>Function </label>
                                <select class="form-control" id="function_descr">
                                </select>
                            </div>
                            <div class="col-lg-3">
                                <label>Action </label>
                                <button type="button" class="btn btn-secondary btn-block" id="btn_generate" ><i class="fa fa-qrcode"></i> Generate Report</button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-primary text-white mb-2">
                                    <div class="card-body" > 
                                        <h2 id="overall_appropriation">...</h2>
                                        <%--<small class="smaller text-white stretched-link" >Total Appropriation</small>--%>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="#">Total Appropriation</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-danger text-white mb-2">
                                    <div class="card-body" > 
                                        <h2 id="overall_actual">...</h2>
                                        <%--<small class="smaller text-white stretched-link" >Total Actual</small>--%>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="#">Total Actual</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-warning text-white mb-2">
                                    <div class="card-body" > 
                                        <h2 >...</h2>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="#">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-success text-white mb-2">
                                    <div class="card-body" > 
                                        <h2 >...</h2>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="#">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                
                        </div>
                        <div class="row">
                            <div class="col-xl-9">
                                <div class="card mb-2">
                                    <div class="card-header">
                                        <i class="fa fa-chart-bar me-1"></i>
                                        Bar Chart
                                    </div>
                                    <div class="card-body"><canvas id="BarChart" width="100%" height="29"></canvas></div>
                                </div>
                            </div>
                            <div class="col-xl-3">
                                <div class="card mb-2">
                                    <div class="card-header">
                                        <i class="fa fa-chart-bar me-1"></i>
                                        Pie Chart 
                                    </div>
                                    <div class="card-body"><canvas id="myPieChart" width="100%" ></canvas></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card mb-2">
                                    <div class="card-header">
                                        <i class="fa fa-table me-1"></i>
                                        DataTable 
                                    </div>
                                    <div class="card-body">
                                        <div class="table-responsive  smaller" >
                                            <table class="table table-hover table-bordered" id="datalist_grid">
                                                <thead >
                                                    <tr>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:30% !important">DESCRIPTION</th>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:20% !important">OOE</th>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:10% !important">ACCOUNT</th>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:10% !important">AMOUNT</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="col-xl-12">
                                <div class="card mb-2">
                                    <div class="card-header">
                                        <i class="fa fa-chart-area me-1"></i>
                                        Area Chart Example
                                    </div>
                                    <div class="card-body"><canvas id="AreaChart" width="100%" height="20"></canvas></div>
                                </div>
                            </div>--%>
                        </div>
                      </div>
                      <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                          <div class="row mb-2 mt-2">
                            <div class="col-lg-3">
                                <div class="form-group row">
                                    <div class="col-lg-6">
                                        <label>Period From</label>
                                        <input class="form-control" type="date" id="period_from" />
                                    </div>
                                    <div class="col-lg-6">
                                        <label>Period To</label>
                                        <input class="form-control" type="date" id="period_to" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-9">
                                <label>Payroll Type </label>
                                <select class="form-control" id="payrolltype"> </select>
                            </div>
                            <div class="col-lg-3">
                                <%--<label>Action </label>--%>
                                <%--<button type="button" class="btn btn-secondary btn-block"  ><i class="fa fa-qrcode"></i> Generate Report</button>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-primary text-white mb-2">
                                    <div class="card-body" > 
                                        <h2 id="ttl_posted">...</h2>
                                        <%--<small class="smaller text-white stretched-link" >Total Appropriation</small>--%>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="#">TOTAL # OF POSTED</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-success text-white mb-2">
                                    <div class="card-body" > 
                                        <h2 id="ttl_released">...</h2>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="#">TOTAL # OF RELEASED</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-warning text-white mb-2">
                                    <div class="card-body" > 
                                        <h2 id="ttl_not_posted">...</h2>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="#">TOTAL # OF NOT POSTED</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-danger text-white mb-2">
                                    <div class="card-body" > 
                                        <h2 id="ttl_returned">...</h2>
                                        <%--<small class="smaller text-white stretched-link" >Total Actual</small>--%>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="#">TOTAL # OF RETURNED</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-9">
                                <div class="card mb-2">
                                    <div class="card-header">
                                        <i class="fa fa-table me-1"></i>
                                        DataTable 
                                    </div>
                                    <div class="card-body">
                                        <div class="table-responsive  smaller" >
                                            <table class="table table-hover table-bordered" id="datalist_grid_payrolls" style="width:100% !important">
                                                <thead >
                                                    <tr>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:5% !important"></th>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:10% !important">REG</th>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:35% !important">PAYROLL</th>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:40% !important">DESCRIPTION</th>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:5% !important">CAFOA AMOUNT</th>
                                                        <th scope="col" style="background-color:#007bff !important;color:white !important;width:5% !important">STATUS</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3">
                                <div class="card mb-2">
                                    <div class="card-header">
                                        <i class="fa fa-chart-bar me-1"></i>
                                        Pie Chart 
                                    </div>
                                    <div class="card-body"><canvas id="myPieChartPayroll" width="100%" ></canvas></div>
                                </div>
                            </div>
                            
                            <%--<div class="col-xl-12">
                                <div class="card mb-2">
                                    <div class="card-header">
                                        <i class="fa fa-chart-bar me-1"></i>
                                        Bar Chart
                                    </div>
                                    <div class="card-body"><canvas id="BarChartPayroll" width="100%" height="29"></canvas></div>
                                </div>
                            </div>--%>
                        </div>
                      </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
    <div class="modal fade" id="Loading_master">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-dialog modal-sm">
                <span class="fa fa-spinner fa-spin fa-3x text-white"></span>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="specific_scripts" runat="server">
    <script src="../../Scripts/sheetJS.js"></script>
    <!-- Include Chart.js -->
    <script src="../../Scripts/chart.js"></script>

    <!-- Include chartjs-plugin-datalabels -->
    <script src="../../Scripts/chartjs-plugin-datalabels.js"></script>

    <script  type="text/javascript">
        function hightlight()
        {
        }
        var date = new Date()
        var d_fm = date.getFullYear() + "-" + ((date.getMonth()+1) > 9 ?(date.getMonth()+1):"0"+(date.getMonth()+1)) + "-01"
        var d_to = date.toISOString().slice(0, 10)
        $('#period_from').val(d_fm)
        $('#period_to').val(d_to)

        var datalistgrid;
        var oTable;

        var datalistgrid_payrolls;
        var oTable_payrolls;

        var myBarChart;
        var myPieChart;
        var myPieChartPayroll;
        var data_to_excel;

        var open_show = false;
        // Initialize
        init()
        payrolllist()
        function init()
        {
            // Payroll Charges
            PayrollTemplateList()
            // BarGraph Data
            barChart('BAR')
            // AreaChart Data
            //areaChart()
            // Initialized Datatable
            datatable()
            // Payroll Type
            PayrollTemplate()
        
            $('#appropriation_year').on('change', function (e)
            {
                PayrollTemplateList()
                barChart('MAIN')
                datatable()
            });
            $('#function_descr').on('change', function (e)
            {
                if ($('#function_descr').val() == "")
                {
                    barChart('BAR')
                }
                else
                {
                    barChart('MAIN')
                }
                datatable()
            });

            $('#btn_generate').on('click', function (e)
            {
                fnExcelReport()
            })
            $('#period_from').on('change', function (e)
            {
                payrolllist()
            });
            $('#period_to').on('change', function (e)
            {
                payrolllist()
            });
            $('#payrolltype').on('change', function (e)
            {
                payrolllist()
            });
        }
        // Datatable
        function datatable()
        {
            var year          = $('#appropriation_year').val()
            var function_code = $('#function_descr').val()
            $('#Loading_master').modal({ keyboard: false, backdrop: "static" });
            $.ajax({
                type        : "POST",
                url         : "cDashboard.aspx/PayrollAppropriation",
                data        : JSON.stringify({ year: year,type:'MAIN',function_code:function_code,par_period_from:'',par_period_to:'',par_payrolltemplate_code:'' }),
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

                    data_to_excel = [];
                    data_to_excel = parsed

                    if (parsed)
                    {
                        var ttl_appr = 0
                        var ttl_actl = 0
                        if (parsed.length > 0)
                        {
                            oTable.fnAddData(parsed);
                            for (var i = 0; i < parsed.length; i++)
                            {
                                ttl_actl += parsed[i]['total_actual']
                                ttl_appr += parsed[i]['total_appropriation']
                            }
                            $('#overall_actual').text("P" + currency(ttl_actl))
                            $('#overall_appropriation').text("P" + currency(ttl_appr))
                            // Pie Chart
                            pieChart(ttl_appr,ttl_actl)
                        }
                        else
                        {
                            alert("No Data Found!");
                            $('#Loading_master').modal("hide");
                            oTable.fnClearTable();
                            pieChart(ttl_appr,ttl_actl)
                            $('#overall_actual').text("P" + currency(ttl_actl))
                            $('#overall_appropriation').text("P" + currency(ttl_appr))
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
        // Bar Chart
        function barChart(type)
        {
            
            var ctx           = document.getElementById('BarChart');
            var year          = $('#appropriation_year').val()
            var function_code = $('#function_descr').val()
            $.ajax({
                type        : "POST",
                url         : "cDashboard.aspx/PayrollAppropriation",
                data        : JSON.stringify({ year: year,type:type,function_code:function_code,par_period_from:'',par_period_to:'',par_payrolltemplate_code:''}),
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var barGraphData =  JSON.parse(response.d)
                    
                    if (barGraphData.length > 0)
                    {
                        let delayed;
                        var labels = "";
                        var valuesA = "";
                        var valuesB = "";
                        if (type == "MAIN")
                        {
                            labels  = barGraphData.map(a=>a.object_of_expenditure);
                            valuesA = barGraphData.map(a=>a.total_appropriation)
                            valuesB = barGraphData.map(a=>a.total_actual)
                        } else
                        {
                            labels  = barGraphData.map(a=>a.function_descr);
                            valuesA = barGraphData.map(a=>a.total_appropriation)
                            valuesB = barGraphData.map(a=>a.total_actual)
                        }

                        if (myBarChart)
                        {
                            myBarChart.destroy();
                        }
                        myBarChart = new Chart(ctx,
                        {
                            type    : 'bar',
                            data    : 
                            {
                                labels          : labels,
                                datasets        : [
                                                    {
                                                        label          : 'Appropriation',
                                                        data           : valuesA,
                                                        borderColor    : '#9BD0F5',
                                                        backgroundColor: '#9BD0F5',
                                                    },
                                                    {
                                                        label          : 'Actual',
                                                        data           : valuesB,
                                                        borderColor    : '#FFB1C1',
                                                        backgroundColor: '#FFB1C1',
                                                    }
                                                    ]
                            },
                            //plugins: [ChartDataLabels],
                            //options: {
                            //    plugins: {
                            //        datalabels: {
                            //            color: 'black',
                            //            anchor: 'end',
                            //            align: 'top',
                            //            formatter: function (value, context)
                            //            {
                            //                return currency(value);
                            //            }
                            //        }
                            //    }
                            //}
                            options: {
                                    animation: {
                                      onComplete: () => {
                                        delayed = true;
                                      },
                                      delay: (context) => {
                                        let delay = 0;
                                        if (context.type === 'data' && context.mode === 'default' && !delayed) {
                                          delay = context.dataIndex * 300 + context.datasetIndex * 100;
                                        }
                                        return delay;
                                      },
                                    },
                                    indexAxis: 'x',
                                    elements: {
                                      bar: {
                                        borderWidth: 2,
                                      }
                                    },
                                    responsive: true,
                                    plugins: {
                                      legend: {
                                        position: 'top'
                                      },
                                      //title: {
                                      //  display: true,
                                      //  text: 'Chart.js Horizontal Bar Chart'
                                      //}
                                    }
                                  },

                        });

                        

                    } else
                    {
                        if (myBarChart)
                        {
                            myBarChart.destroy();
                        }
                    }
                },
                failure: function (response)
                {
                    alert("Error: " + response.d);
                    
                }
            });

            
        }
        // Pie Chart
        function pieChart(ttl_appr, ttl_actl)
        {
            if (myPieChart)
            {
                myPieChart.destroy();
            }
            var ctx = document.getElementById('myPieChart').getContext('2d');
            myPieChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['Total Appropriation', 'Total Actual'],
                    datasets: [{
                        //label: 'Total',
                        data: [ttl_appr, ttl_actl],
                        backgroundColor: [
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 99, 132, 0.2)'
                        ],
                        borderColor: [
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 99, 132, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                plugins: [ChartDataLabels],
                options: {
                    plugins: {
                        datalabels: {
                            color: '#212529', // Color of the labels (percentage)
                            formatter: (value, ctx) => {
                                let sum = 0;
                                let dataArr = ctx.chart.data.datasets[0].data;
                                dataArr.map(data => {
                                    sum += data;
                                });
                                let percentage = (value * 100 / sum).toFixed(2) + "%";
                                return percentage;
                            },
                            font: {
                                size    : 25, // Font size
                                weight  : 'bold' // Font weight
                            },
                        },
                        
                    }
                }

            });
        }
        // Currency
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
        var init_table_data = function (par_data)
        {
            datalistgrid = par_data;
            oTable       = $('#datalist_grid').dataTable(
                {
                    data        : datalistgrid,
                    pageLength  : 10,
                    columns:
                    [
                        //{
                        //    "mData": "appropriation_year",
                        //    "mRender": function (data, type, full, row) {
                        //        return "<span class='text-center   btn-block'>" + data + "</span>"
                        //    }
                        //},
                        {
                            "mData": "function_descr",
                            "mRender": function (data, type, full, row)
                            {
                                return "<span>" + full["function_code"] + ' - '+ data + "</span>"
                            }
                        },
                        //{
                        //    "mData": "raao_descr",
                        //    "mRender": function (data, type, full, row) {
                        //        return "<span>" + full["raao_code"] + ' - '+ data + "</span>"
                        //    }
                        //},
                        {
                            "mData": "object_of_expenditure",
                            "mRender": function (data, type, full, row) {
                                return "<span>" + full["ooe_code"] + ' - '+ data + "</span>"
                            }
                        },
                        {
                            "mData": "account_code",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-center   btn-block'>" + data + "</span>"
                            }
                        },
                        {
                            "mData": "total_actual",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-right   btn-block'>" + currency(data) + "&nbsp;&nbsp;</span>"
                            }
                        },
                    ],
                });
        }
        const options123 = { year: 'numeric', month: 'short', day: 'numeric' };
        var init_table_data_payrolls = function (par_data)
        {
            datalistgrid_payrolls = par_data;
            oTable_payrolls       = $('#datalist_grid_payrolls').dataTable(
                {
                    data        : datalistgrid_payrolls,
                    pageLength  : 10,
                    responsive  : true,
                    columns:
                    [
                        {
                        "mData": null,
                        "mRender": function (data, type, full, row) {
                            return "<center><span class='details-control' style='display:block;' ></center>"
                        }
                        },
                        {
                            "mData": "payroll_registry_nbr",
                            "mRender": function (data, type, full, row)
                            {
                                return "<span class='text-center   btn-block'>" + full[0].payroll_registry_nbr + "</span>"
                            }
                        },
                        {
                            "mData": "payrolltemplate_descr",
                            "mRender": function (data, type, full, row) {
                                var date_from = new Date(full[0].payroll_period_from).toLocaleDateString('en-US', options123)
                                var date_to = new Date(full[0].payroll_period_to).toLocaleDateString('en-US', options123)
                                return "<span>" + full[0].payrolltemplate_descr + "&nbsp;&nbsp;<small class='badge badge-secondary'> " + date_from + "-" + date_to+ "</small></span>"
                            }
                        },
                        {
                            "mData": "payroll_registry_descr",
                            "mRender": function (data, type, full, row) {
                                return "<span>" + full[0].payroll_registry_descr + "</span>"
                            }
                        },
                        {
                            "mData": "total_cafoa",
                            "mRender": function (data, type, full, row) {
                                return "<span class='text-right btn-block'>" + currency(full[0].total_cafoa) + "&nbsp;&nbsp;</span>"
                            }
                        },
                        {
                            "mData": "post_status_descr",
                            "mRender": function (data, type, full, row)
                            {
                                return "<span class='text-center btn-block'>" + full[0].post_status_descr + "</span>"
                            }
                        },
                    ],
                });
        }
        function PayrollTemplateList()
        {
            var year = $('#appropriation_year').val()
            $.ajax({
                type        : "POST",
                url         : "cDashboard.aspx/PayrollCharges",
                data        : JSON.stringify({ year: year }),
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    var select = document.getElementById("function_descr");
                    if (parsed.length > 0)
                    {
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
                            option.text     = parsed[i].function_descr; 
                            option.value = parsed[i].function_code; 
                            select.appendChild(option);
                        }
                    } else
                    {
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
        function fnExcelReport() 
        {
            var data = data_to_excel
            // Convert object array to worksheet
            const ws = XLSX.utils.json_to_sheet(data);

            // Create a new workbook
            const wb = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(wb, ws, "Sheet1");

            // Export the workbook
            var date = new Date()
            var filename = "appropriation_" + formatDateTime(date)+".xlsx";
            XLSX.writeFile(wb, filename);
        }
        function formatDateTime(date)
        {
            // Extract date components
            let year    = date.getFullYear();
            let month   = String(date.getMonth() + 1).padStart(2, '0'); // Month is zero-indexed, so we add 1
            let day     = String(date.getDate()).padStart(2, '0');
            let hours   = String(date.getHours()).padStart(2, '0');
            let minutes = String(date.getMinutes()).padStart(2, '0');
            let seconds = String(date.getSeconds()).padStart(2, '0');

            // Format the date as YYYYMMDD_hhmmss
            return `${year}${month}${day}_${hours}${minutes}${seconds}`;
        }
        // Area Chart
        //function areaChart()
        //{
        //    const ctx = document.getElementById('AreaChart');
        //    new Chart(ctx,
        //    {
        //        type    : 'line',
        //        data    : 
        //        {
        //            labels          : ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        //            datasets        : [
        //                                {
        //                                    label           : '# of Votes',
        //                                    data            : [12, 19, 3, 5, 2, 3],
        //                                    borderWidth     : 1,
        //                                    backgroundColor : '#FFB1C1',
        //                                }
        //                              ]
        //        },
        //        options: {
        //                scales:
        //                    {
        //                        y:
        //                        {
        //                            beginAtZero: true
        //                        }
        //                    }
        //                }
        //    });
        //}

        function payrolllist()
        {
            var period_from                 = $('#period_from').val()
            var period_to                   = $('#period_to').val()
            var par_payrolltemplate_code    = $('#payrolltype').val() == null ? "" : $('#payrolltype').val()
            if (period_from != "" && period_to != "")
            {
                $('#Loading_master').modal({ keyboard: false, backdrop: "static" });
                $.ajax({
                    type        : "POST",
                    url         : "cDashboard.aspx/PayrollAppropriation",
                    data        : JSON.stringify({ year: '',type:'PAYROLLS',function_code:'',par_period_from:period_from,par_period_to:period_to,par_payrolltemplate_code:par_payrolltemplate_code }),
                    contentType : "application/json; charset=utf-8",
                    dataType    : "json",
                    success: function (response)
                    {
                        var parsed = JSON.parse(response.d)
                        var ttl_posted      = 0
                        var ttl_not_posted  = 0
                        var ttl_released    = 0
                        var ttl_returned = 0
                        if (!datalistgrid_payrolls)
                        {
                            init_table_data_payrolls([]);
                        }
                        oTable_payrolls.fnClearTable();
                        datalistgrid_payrolls = parsed;

                        if (!open_show)
                        {
                            $('#datalist_grid_payrolls tbody').on('click', 'span.details-control', function ()
                            {
                                open_show = true
                                var tr  = $(this).closest('tr');
                                var row = $('#datalist_grid_payrolls').DataTable().row(tr);

                                //console.log(row.data())
                                if (row.child.isShown())
                                {
                                    row.child.hide();
                                    tr.removeClass('shown');
                                }
                                else
                                {
                                    row.child(format(row.data())).show();
                                    //$compile(row)($scope); 
                                    tr.addClass('shown');
                                    //$compile(tr)($scope); 

                                }
                            });

                        }

                        // Grouping by 'post_status_descr' property
                        //let grouped = parsed.reduce((acc, obj) => {
                        //    let key = obj.post_status_descr;
                        //    if (!acc[key]) {
                        //        acc[key] = [];
                        //    }
                        //    acc[key].push(obj);
                        //    return acc;
                        //}, {});

                        // Grouping by 'payroll_registry_nbr' property
                        let groupedByReg = parsed.reduce((acc, obj) => {
                            let key = obj.payroll_registry_nbr;
                            if (!acc[key]) {
                                acc[key] = [];
                            }
                            acc[key].push(obj);
                            return acc;
                        }, {});

                        // Step 2: Group and Count Distinct payroll_registry_nbr
                        const result = {};

                        parsed.forEach(item => {
                          const groupKey = item.post_status_descr;
                          if (!result[groupKey]) result[groupKey] = new Set();
                          result[groupKey].add(item.payroll_registry_nbr);
                        });

                        // Step 3: Format the result
                        const finalOutput = Object.entries(result).map(([status, regSet]) => ({
                          post_status_descr: status,
                          distinct_count: regSet.size
                        }));
                        
                        if (parsed.length > 0)
                        {
                            if (finalOutput.length > 0)
                            {
                                for (var i = 0; i < finalOutput.length; i++)
                                {
                                    if (finalOutput[i].post_status_descr == "POSTED")
                                    {
                                        ttl_posted = finalOutput[i].distinct_count
                                    }
                                    if (finalOutput[i].post_status_descr == "NOT POSTED")
                                    {
                                        ttl_not_posted = finalOutput[i].distinct_count
                                    }
                                    if (finalOutput[i].post_status_descr == "RELEASED")
                                    {
                                        ttl_released = finalOutput[i].distinct_count
                                    }
                                    if (finalOutput[i].post_status_descr == "RETURNED")
                                    {
                                        ttl_returned = finalOutput[i].distinct_count
                                    }
                                }
                            }
                            //ttl_posted     = (grouped['POSTED']     == null ? 0 : grouped['POSTED'].length);
                            //ttl_not_posted = (grouped['NOT POSTED'] == null ? 0 : grouped['NOT POSTED'].length);
                            //ttl_released   = (grouped['RELEASED']   == null ? 0 : grouped['RELEASED'].length);
                            //ttl_returned   = (grouped['RETURNED']   == null ? 0 : grouped['RETURNED'].length);

                            pieChartPayroll(ttl_posted,ttl_not_posted,ttl_released,ttl_returned)
                            let payrolls = [];
                            // Iterate over each key-value pair in groupedData
                            Object.keys(groupedByReg).forEach((a,b) =>
                            {
                                  payrolls.push(groupedByReg[a])
                            });
                            
                            oTable_payrolls.fnAddData(payrolls);
                            $('#Loading_master').modal("hide");
                            
                        } else
                        {
                            pieChartPayroll(ttl_posted,ttl_not_posted,ttl_released,ttl_returned)
                            $('#Loading_master').modal("hide");
                        }
                        $('#ttl_posted').text(ttl_posted)
                        $('#ttl_not_posted').text(ttl_not_posted)
                        $('#ttl_released').text(ttl_released)
                        $('#ttl_returned').text(ttl_returned)
                    },
                    failure: function (response)
                    {
                        alert("Error: " + response.d);
                        $('#Loading_master').modal("hide");
                    }
                });
            }
        }
        function format(d)
        {
            var table = "";
            for (var i = 0; i < d.length; i++)
            {
                table += '<tr>  <td scope="row" class="text-right">' + (i + 1) + '.</td>  <td scope="row" class="text-center">' + d[i].empl_id + '</td> <td>' + d[i].employee_name + '</td> <td class="text-right">' + currency(d[i].gross_pay) + '</td>'+ '<td class="text-right">' + currency(d[i].net_pay) + '</td>' + '</tr>';
            }
            var var_table = '<div style="padding:5px 10% 5px 10% !important" class="table-responsive"><table class="table"> <thead><tr> <th style="background-image: linear-gradient(#18a689, #18a689) !important;" scope="col">#</th> <th  style="background-image: linear-gradient(#18a689, #18a689) !important;" scope="col">ID NO</th>  <th  style="background-image: linear-gradient(#18a689, #18a689) !important;" scope="col">EMPLOYEE NAME</th> <th  style="background-image: linear-gradient(#18a689, #18a689) !important;"  scope="col">GROSS PAY</th><th  style="background-image: linear-gradient(#18a689, #18a689) !important;"  scope="col">NET PAY</th>   </tr></thead><tbody> ' + table +'</tbody></table></div>';
            return var_table;
        }
        // Pie Chart Payroll
        function pieChartPayroll(posted, released,not_posted,returned)
        {
            if (myPieChartPayroll)
            {
                myPieChartPayroll.destroy();
            }
            var ctx = document.getElementById('myPieChartPayroll').getContext('2d');
            myPieChartPayroll = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['Total Posted', 'Total Released', 'Total Not Posted', 'Total Returned'],
                    datasets: [{
                        //label: 'Total',
                        data: [posted, released,not_posted,returned],
                        backgroundColor: [
                            'rgba(54, 162, 235, 0.6)', // Blue with transparency
                            'rgba(75, 192, 192, 0.6)', // Green with transparency
                            'rgba(255, 206, 86, 0.6)', // Yellow with transparency
                            'rgba(255, 99, 132, 0.6)', // Red with transparency
                        ],
                        borderColor: [
                            'rgba(54, 162, 235, 0.6)', // Blue with transparency
                            'rgba(75, 192, 192, 0.6)', // Green with transparency
                            'rgba(255, 206, 86, 0.6)', // Yellow with transparency
                            'rgba(255, 99, 132, 0.6)', // Red with transparency
                        ],
                        borderWidth: 1
                    }]
                },
                plugins: [ChartDataLabels],
                options: {
                    plugins: {
                        datalabels: {
                            color: '#212529', // Color of the labels (percentage)
                            formatter: (value, ctx) => {
                                let sum = 0;
                                let dataArr = ctx.chart.data.datasets[0].data;
                                dataArr.map(data => {
                                    sum += data;
                                });
                                let percentage = ((value * 100 / sum) == 0 ? "" :(value * 100 / sum).toFixed(2) + "%") ;
                                return percentage;
                            },
                            font: {
                                size    : 25, // Font size
                                weight  : 'bold' // Font weight
                            }
                        },
                        legend: {
                            position: 'top',
                            labels: {
                                // Generate labels with custom text (label + count)
                                generateLabels: function(chart) {
                                    var data = chart.data;
                                    if (data.labels.length && data.datasets.length) {
                                        return data.labels.map(function(label, i) {
                                            var dataset = data.datasets[0];
                                            var value = dataset.data[i];
                                            var percent = ((value / dataset.data.reduce((a, b) => a + b, 0)) * 100).toFixed(2) + '%'; // Calculate percentage
                                            return {
                                                text: `${label} (${value} - ${percent})`,
                                                fillStyle: dataset.backgroundColor[i],
                                                hidden: isNaN(dataset.data[i]), // Hide labels for missing data points
                                                index: i
                                            };
                                        });
                                    }
                                    return [];
                                }
                            }
                        }
                    }
                }

            });
        }
        function PayrollTemplate()
        {
            $.ajax({
                type        : "POST",
                url         : "cDashboard.aspx/PayrollTemplate",
                contentType : "application/json; charset=utf-8",
                dataType    : "json",
                success: function (response)
                {
                    var parsed = JSON.parse(response.d)
                    var select = document.getElementById("payrolltype");
                    if (parsed.length > 0)
                    {
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
    </script>
</asp:Content>
