<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="../../CSS/core.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Toolbar.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/input.css" rel="stylesheet" />

    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>

    <script src="../../lib/jquery.form.js" type="text/javascript"></script>
    <script src="../../JS/Toolbar.js" type="text/javascript"></script>
    <script src="../../lib/json.js" type="text/javascript"></script>
    <script src="../../JS/XHD.js" type="text/javascript"></script>

    <script src="../../JS/flotr2.js" type="text/javascript"></script>
    <script src="../../JS/flotr2.ie.min.js" type="text/javascript"></script>
    <%--<script src="../../JS/excanvas.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        var manager;
        var manager1;
        $(function () {

            initLayout();
            $(window).resize(function () {
                initLayout();
            });


            $("#maingrid5").ligerGrid({
                columns: [
                    { display: '编号', width: 40, render: function (item, i) { return i + 1; } },
                    {
                        display: '项目', name: 'items', width: 120, render: function (item) {
                            if (item.items == "")
                                item.items = "未分类";
                            return item.items;
                        },
                        totalSummary: { type: 'total' }
                    },
                    {
                        display: '一月', name: 'moon1', width: 50, render: function (item) {
                            if (typeof (item.moon1) == "undefined" || (typeof (item.moon1) == "number" && item.moon1 == "0"))
                                return "0";
                            else
                                return item.moon1;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '二月', name: 'moon2', width: 50, render: function (item) {
                            if (typeof (item.moon2) == "undefined" || (typeof (item.moon2) == "number" && item.moon2 == "0"))
                                return "0";
                            else
                                return item.moon2;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '三月', name: 'moon3', width: 50, render: function (item) {
                            if (typeof (item.moon3) == "undefined" || (typeof (item.moon3) == "number" && item.moon3 == "0"))
                                return "0";
                            else
                                return item.moon3;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '四月', name: 'moon4', width: 50, render: function (item) {
                            if (typeof (item.moon4) == "undefined" || (typeof (item.moon4) == "number" && item.moon4 == "0"))
                                return "0";
                            else
                                return item.moon4;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '五月', name: 'moon5', width: 50, render: function (item) {
                            if (typeof (item.moon5) == "undefined" || (typeof (item.moon5) == "number" && item.moon5 == "0"))
                                return "0";
                            else
                                return item.moon5;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '六月', name: 'moon6', width: 50, render: function (item) {
                            if (typeof (item.moon6) == "undefined" || (typeof (item.moon6) == "number" && item.moon6 == "0"))
                                return "0";
                            else
                                return item.moon6;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '七月', name: 'moon7', width: 50, render: function (item) {
                            if (typeof (item.moon7) == "undefined" || (typeof (item.moon7) == "number" && item.moon7 == "0"))
                                return "0";
                            else
                                return item.moon7;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '八月', name: 'moon8', width: 50, render: function (item) {
                            if (typeof (item.moon8) == "undefined" || (typeof (item.moon8) == "number" && item.moon8 == "0"))
                                return "0";
                            else
                                return item.moon8;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '九月', name: 'moon9', width: 50, render: function (item) {
                            if (typeof (item.moon9) == "undefined" || (typeof (item.moon9) == "number" && item.moon9 == "0"))
                                return "0";
                            else
                                return item.moon9;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '十月', name: 'moon10', width: 40, render: function (item) {
                            if (typeof (item.moon10) == "undefined" || (typeof (item.moon10) == "number" && item.moon10 == "0"))
                                return "0";
                            else
                                return item.moon10;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '十一月', name: 'moon11', width: 50, render: function (item) {
                            if (typeof (item.moon11) == "undefined" || (typeof (item.moon11) == "number" && item.moon11 == "0"))
                                return "0";
                            else
                                return item.moon11;
                        },
                        totalSummary: { type: 'sum' }
                    },
                    {
                        display: '十二月', name: 'moon12', width: 50, render: function (item) {
                            if (typeof (item.moon12) == "undefined" || (typeof (item.moon12) == "number" && item.moon12 == "0"))
                                return "0";
                            else
                                return item.moon12;
                        },
                        totalSummary: { type: 'sum' }
                    }
                ],
                url: '../../data/CRM_Customer.ashx',
                usePager: false,
                //dataAction: 'local', pageSize: 30, pageSizeOptions: [20, 30, 50, 100],
                width: '100%', height: '100%',
                title: "年度统计表",
                heightDiff: -6
            });

            var toolbar1 = new Toolbar({
                renderTo: 'toolbar',
                //border: 'top',
                items: [{
                    type: 'textfield',
                    id: "stype",
                    text: "类型：",
                    useable: 'enabled',
                    handler: function () {
                        //EditButton();
                    }
                }, '-', {
                    type: 'textfield',
                    id: "syear",
                    text: "年度：",
                    useable: 'enabled',
                    handler: function () {

                    }
                }, '-', {
                    type: 'textfield',
                    id: "stext",
                    text: "关键字：",
                    useable: 'enabled',
                    handler: function () {

                    }
                }, '-', {
                    type: 'button',
                    text: '统计',
                    bodyStyle: 'search',
                    useable: 'enabled',
                    handler: function () {
                        doserch();

                    }
                }, {
                    type: 'button',
                    text: '重置',
                    bodyStyle: 'edit',
                    useable: 'enabled',
                    handler: function () {
                        $("#serchform").each(function () {
                            this.reset();
                        });
                    }
                }
                ],
                active: 'ALL'//激活哪个
            });
            toolbar1.render();

            $("#stype").ligerComboBox({
                width: 100,
                data: [
                    { 'text': '省份', 'id': 'Provinces' },
                    { 'text': '城市', 'id': 'City' },
                    { 'text': '客户类型', 'id': 'CustomerType' },
                    { 'text': '客户级别', 'id': 'CustomerLevel' },
                    { 'text': '客户来源', 'id': 'CustomerSource' }
                ],
                initValue: 'CustomerType'
            })
            var d = new Date();
            var nowYear = +d.getFullYear();
            var syearData = [];
            for (var i = nowYear; i >= nowYear - 20; i--)
            {
                syearData.push({ 'text': i, 'id': i });
            }
            $("#syear").ligerComboBox({
                width: 100,
                data: syearData,
                initValue: nowYear,

            })
            $("#stext").ligerTextBox({ width: 200 })

            $("#grid").height(document.documentElement.clientHeight - $(".toolbar").height() - 350);
            $("#pageloading").hide();

            $("#maingrid5").ligerGetGridManager().onResize();
            doserch();
        });
        function test(GridData) {            
            //alert(JSON.stringify(data));

            var data = GridData.Rows;

            var colors = ['blue', 'red', 'green', 'yellow', 'black', 'gray'];
            var flotData = [];
            for (var i = 0; i < data.length; i++) {
                var m1 = typeof (data[i].moon1) == "undefined" ? "0" : data[i].moon1;
                var m2 = typeof (data[i].moon2) == "undefined" ? "0" : data[i].moon2;
                var m3 = typeof (data[i].moon3) == "undefined" ? "0" : data[i].moon3;
                var m4 = typeof (data[i].moon4) == "undefined" ? "0" : data[i].moon4;
                var m5 = typeof (data[i].moon5) == "undefined" ? "0" : data[i].moon5;
                var m6 = typeof (data[i].moon6) == "undefined" ? "0" : data[i].moon6;
                var m7 = typeof (data[i].moon7) == "undefined" ? "0" : data[i].moon7;
                var m8 = typeof (data[i].moon8) == "undefined" ? "0" : data[i].moon8;
                var m9 = typeof (data[i].moon9) == "undefined" ? "0" : data[i].moon9;
                var m10 = typeof (data[i].moon10) == "undefined" ? "0" : data[i].moon10;
                var m11 = typeof (data[i].moon11) == "undefined" ? "0" : data[i].moon11;
                var m12 = typeof (data[i].moon12) == "undefined" ? "0" : data[i].moon12;
                //alert([[1, m1], [2, m2], [3, m3], [4, m4], [5, m5], [6, m6], [7, m7], [8, m8], [9, m9], [10, m10], [11, m11], [12, m12]]);

                flotData.push({
                    label: data[i].items,
                    data: [[1, m1 * 1],[2, m2 * 1],[3, m3 * 1],[4, m4 * 1],[5, m5 * 1],[6, m6 * 1],[7, m7 * 1],[8, m8 * 1],[9, m9 * 1],[10, m10 * 1],[11, m11 * 1],[12, m12 * 1]],
                    lines: { show: true },
                    points: { show: true }
                });
            }
            // This function prepend each label with 'y = '
            function labelFn(label) {
                return label;
            }
            var container = document.getElementById('graph');
            //function ticksFn(n) { return '' + n + '月'; }
            // Draw graph
            graph1 = Flotr.draw(container, flotData, {
                legend: {
                    position: 'ne',            // Position the legend 'south-east'.
                    labelFormatter: labelFn,   // Format the labels.
                    backgroundColor: '#D2E2F2' // A light blue background color.
                },
                xaxis: {
                    noTicks: 12,              // Display 7 ticks.
                    //tickFormatter: ticksFn,  // Displays tick values between brackets.
                    min: 1,                  // Part of the series is not displayed.
                    max: 12
                },
                HtmlText: false,
                mouse: {
                    track: true, // Enable mouse tracking
                    lineColor: 'purple',
                    relative: true,
                    position: 'ne',
                    sensibility: 1,
                    trackDecimals: 2,
                    trackFormatter: function (o) { return 'x='+ o.x + ', y = ' + o.y; }
                },
                crosshair: {
                    //mode: 'xy'
                }
            });
        }
        function doserch() {

            var sendtxt = "&Action=year_reports&rnd=" + Math.random();
            var serchtxt = $("#serchform :input").fieldSerialize() + sendtxt;
            //alert(serchtxt);
            var manager = $("#maingrid5").ligerGetGridManager();            

            top.$.ligerDialog.waitting('数据查询中,请稍候...');
            $.ajax({
                url: "../../data/CRM_Customer.ashx", type: "POST",
                data: serchtxt,
                dataType: 'json',
                beforeSend: function () {
                    manager.showData({ Rows: [], Total: 0 });
                },
                success: function (responseText) {
                    //alert("../data/crm_customer.ashx" + serchtxt);
                    manager.setURL("../../data/crm_customer.ashx?" + serchtxt);
                    manager.showData(responseText);
                    //manager.loadData(true);
                    top.$.ligerDialog.closeWaitting();
                    test(responseText);
                },
                error: function () {
                    top.$.ligerDialog.closeWaitting();
                    top.$.ligerDialog.error('查询失败！请检查查询项。');
                }
            });
            //test();
        }
        function f_reload() {
            var manager = $("#maingrid5").ligerGetGridManager();
            manager.loadData(true);
        };


    </script>
</head>
<body>
    <div style="position: relative; z-index: 9999">
        <form id="serchform">
            <div id="toolbar"></div>
        </form>
    </div>

    <form id="form1">
        <div id="griddiv">
            <div id="graph" style="height: 280px; margin: 5px;"></div>
            <div class="l-loading" style="display: block" id="pageloading"></div>
            <div id="maingrid5" style="margin: -1px -1px;"></div>
        </div>
    </form>


</body>
</html>
