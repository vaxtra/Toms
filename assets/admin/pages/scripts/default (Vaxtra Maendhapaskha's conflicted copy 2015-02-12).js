var REST = new OF({
    'url': '/api/services.asmx/request'
});

$(document).ready(function () {
    Preload();
});

function Preload() {
    REST.onSuccess = function (result) {
        $.each(result.d.data, function (indexInArray, valueOfElement) {
            $("." + indexInArray).html(valueOfElement);
        });

        var RecentOrder = result.d.data.RecentOrder;
        if (RecentOrder != null)
            LoadRecentOrder(RecentOrder);

        var BestSeller = result.d.data.BestSeller;
        if (BestSeller != null)
            LoadBestSeller(BestSeller);

        var NewCustomer = result.d.data.NewCustomers;
        if (NewCustomer != null)
            LoadNewCustomer(NewCustomer);

        $(".money-format-average").formatCurrency({
            symbol: ' IDR',
            positiveFormat: '%n%s',
            negativeFormat: ' - %n%s',
            decimalSymbol: ',',
            digitGroupSymbol: '.',
            groupDigits: true,
            roundToDecimalPlace: 2
        });

        $(".money-format").formatCurrency({ region: 'id-ID' });

        var ChartRevenue = result.d.data.ChartRevenue;
        LoadChart(ChartRevenue);
    };
    REST.sendRequest({
        'c': 'admindashboard',
        'm': 'preload',
        'data': {
            'RequestData': ["BestSeller", "RecentOrder", "NewCustomers", "TotalCustomer", "TotalOrder", "AverageOrder", "TotalSales", 'ChartRevenue']
        }
    });
}

function showTooltip(x, y, labelX, labelY) {
    $('<div id="tooltip" class="chart-tooltip">' + (labelY.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,')) + ' IDR<\/div>').css({
        position: 'absolute',
        display: 'none',
        top: y - 40,
        left: x - 60,
        border: '0px solid #ccc',
        padding: '2px 6px',
        'background-color': '#fff',
    }).appendTo("body").fadeIn(200);
}

function LoadChart(data) {
    var chartData = [];

    for (var i = 0; i < data.length; i++) {
        chartData.push([moment(data[i].Date).format("DD/MM/YYYY"), data[i].Amount]);
        //chartData.push([i, data[i].Amount]);
    }
    console.log(chartData);

    //var options = {
    //    series: {
    //        lines: { show: true },
    //        points: { show: true }
    //    }
    //};
    //$("#statistics_1").plot(chartData, options);

    var initChart1 = function () {

        var data = [
            ['01/2013', 4],
            ['02/2013', 8],
            ['03/2013', 10],
            ['04/2013', 12],
            ['05/2013', 2125],
            ['06/2013', 324],
            ['07/2013', 1223],
            ['08/2013', 1365],
            ['09/2013', 250],
            ['10/2013', 999],
            ['11/2013', 390],
        ];
        data = chartData;

        var plot_statistics = $.plot(
            $("#statistics_1"),
            [
                {
                    data: data,
                    lines: {
                        fill: 0.6,
                        lineWidth: 0,
                    },
                    color: ['#f89f9f']
                },
                {
                    data: data,
                    points: {
                        show: true,
                        fill: true,
                        radius: 5,
                        fillColor: "#f89f9f",
                        lineWidth: 3
                    },
                    color: '#fff',
                    shadowSize: 0
                },
            ],
            {

                xaxis: {
                    tickLength: 0,
                    tickDecimals: 0,
                    mode: "categories",
                    font: {
                        lineHeight: 15,
                        style: "normal",
                        variant: "small-caps",
                        color: "#6F7B8A"
                    }
                },
                yaxis: {
                    ticks: 3,
                    tickDecimals: 0,
                    tickColor: "#f0f0f0",
                    font: {
                        lineHeight: 15,
                        style: "normal",
                        variant: "small-caps",
                        color: "#6F7B8A"
                    }
                },
                grid: {
                    backgroundColor: {
                        colors: ["#fff", "#fff"]
                    },
                    borderWidth: 1,
                    borderColor: "#f0f0f0",
                    margin: 0,
                    minBorderMargin: 0,
                    labelMargin: 20,
                    hoverable: true,
                    clickable: true,
                    mouseActiveRadius: 6
                },
                legend: {
                    show: false
                }
            }
        );

        var previousPoint = null;

        $("#statistics_1").bind("plothover", function (event, pos, item) {
            $("#x").text(pos.x.toFixed(2));
            $("#y").text(pos.y.toFixed(2));
            if (item) {
                if (previousPoint != item.dataIndex) {
                    previousPoint = item.dataIndex;

                    $("#tooltip").remove();
                    var x = item.datapoint[0].toFixed(2),
                        y = item.datapoint[1].toFixed(2);

                    showTooltip(item.pageX, item.pageY, item.datapoint[0], item.datapoint[1]);
                }
            } else {
                $("#tooltip").remove();
                previousPoint = null;
            }
        });

    }
    initChart1();
}

function LoadRecentOrder(data) {
    var item = '';

    for (var i = 0; i < data.length; i++) {
        item += '<tr>';
        item += '<td>';
        item += '<a target="_blank" href="orders/orders/detail.aspx?id=' + data[i].IDOrder + '">' + data[i].CustomerName + '</a>';
        item += '</td>';
        item += '<td>' + data[i].Quantity;
        item += '</td>';
        item += '<td class="money-format">' + data[i].Total;
        item == '</td>';
        item += '<td>';
        item += '<a target="_blank" href="orders/orders/detail.aspx?id=' + data[i].IDOrder + '" class="btn default btn-xs green-stripe">View </a>';
        item += '</td>';
        item += '</tr>';
    }
    $("#tableRecentOrder tbody").html(item);
}

function LoadBestSeller(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<tr>';
        item += '<td>';
        item += '<a target="_blank" href="catalog/product/update.aspx?id=' + data[i].IDProduct + '">' + data[i].Name + '</a>';
        item += '</td>';
        item += '<td>' + data[i].Quantity;
        item += '</td>';
        item += '<td class="money-format">' + data[i].Total;
        item += '</td>';
        item += '<td>';
        item += '<a target="_blank" href="catalog/product/update.aspx?id=' + data[i].IDProduct + '" class="btn default btn-xs green-stripe">View </a>';
        item += '</td>';
        item += '</tr>';
    }
    $("#tableBestSeller tbody").html(item);
}

function LoadNewCustomer(data) {
    var item = '';
    for (var i = 0; i < data.length; i++) {
        item += '<tr>';
        item += '<td>';
        item += data[i].FirstName + ' ' + data[i].LastName;
        item += '</td>';
        item += '<td>' + data[i].Email;
        item += '</td>';
        item += '<td>' + data[i].PhoneNumber;
        item += '</td>';
        item += '</tr>';
    }
    $("#tableNewCustomer tbody").html(item);
}