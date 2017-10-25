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

    <script src="../../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../lib/jquery.form.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../lib/json.js" type="text/javascript"></script>
    <script src="../../JS/tomoral.js" type="text/javascript"></script>

    <script src="../../JS/echarts-all.js" type="text/javascript"></script>
    <script type="text/javascript">
        var manager;
        var manager1;
        $(function () {

            initLayout();
            $(window).resize(function () {
                initLayout();
            });


            //$("#maingrid5").ligerGrid({
            //    columns: [
            //        { display: '编号', width: 40, render: function (item, i) { return i + 1; } },
            //        {
            //            display: '项目', name: 'items', width: 120, render: function (item) {
            //                if (item.items == "")
            //                    item.items = "未分类";
            //                return item.items;
            //            },
            //            totalSummary: { type: 'total' }
            //        },
            //        {
            //            display: '一月', name: 'm1', width: 50, render: function (item) {
            //                if (typeof (item.m1) == "undefined" || (typeof (item.m1) == "number" && item.m1 == "0"))
            //                    return "0";
            //                else
            //                    return item.m1;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '二月', name: 'm2', width: 50, render: function (item) {
            //                if (typeof (item.m2) == "undefined" || (typeof (item.m2) == "number" && item.m2 == "0"))
            //                    return "0";
            //                else
            //                    return item.m2;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '三月', name: 'm3', width: 50, render: function (item) {
            //                if (typeof (item.m3) == "undefined" || (typeof (item.m3) == "number" && item.m3 == "0"))
            //                    return "0";
            //                else
            //                    return item.m3;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '四月', name: 'm4', width: 50, render: function (item) {
            //                if (typeof (item.m4) == "undefined" || (typeof (item.m4) == "number" && item.m4 == "0"))
            //                    return "0";
            //                else
            //                    return item.m4;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '五月', name: 'm5', width: 50, render: function (item) {
            //                if (typeof (item.m5) == "undefined" || (typeof (item.m5) == "number" && item.m5 == "0"))
            //                    return "0";
            //                else
            //                    return item.m5;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '六月', name: 'm6', width: 50, render: function (item) {
            //                if (typeof (item.m6) == "undefined" || (typeof (item.m6) == "number" && item.m6 == "0"))
            //                    return "0";
            //                else
            //                    return item.m6;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '七月', name: 'm7', width: 50, render: function (item) {
            //                if (typeof (item.m7) == "undefined" || (typeof (item.m7) == "number" && item.m7 == "0"))
            //                    return "0";
            //                else
            //                    return item.m7;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '八月', name: 'm8', width: 50, render: function (item) {
            //                if (typeof (item.m8) == "undefined" || (typeof (item.m8) == "number" && item.m8 == "0"))
            //                    return "0";
            //                else
            //                    return item.m8;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '九月', name: 'm9', width: 50, render: function (item) {
            //                if (typeof (item.m9) == "undefined" || (typeof (item.m9) == "number" && item.m9 == "0"))
            //                    return "0";
            //                else
            //                    return item.m9;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '十月', name: 'm10', width: 40, render: function (item) {
            //                if (typeof (item.m10) == "undefined" || (typeof (item.m10) == "number" && item.m10 == "0"))
            //                    return "0";
            //                else
            //                    return item.m10;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '十一月', name: 'm11', width: 50, render: function (item) {
            //                if (typeof (item.m11) == "undefined" || (typeof (item.m11) == "number" && item.m11 == "0"))
            //                    return "0";
            //                else
            //                    return item.m11;
            //            },
            //            totalSummary: { type: 'sum' }
            //        },
            //        {
            //            display: '十二月', name: 'm12', width: 50, render: function (item) {
            //                if (typeof (item.m12) == "undefined" || (typeof (item.m12) == "number" && item.m12 == "0"))
            //                    return "0";
            //                else
            //                    return item.m12;
            //            },
            //            totalSummary: { type: 'sum' }
            //        }
            //    ],
            //    url: '../../data/Reports_CRM.ashx',
            //    usePager: false,
            //    //dataAction: 'local', pageSize: 30, pageSizeOptions: [20, 30, 50, 100],
            //    width: '100%', height: '100%',
            //    title: "年度统计表",
            //    heightDiff: -6
            //});

            $("#toolbar").ligerToolBar({
                items: [
                    { type: 'textbox', id: 'stype', text: '统计项:' },
                    { type: 'textbox', id: "syear", text: "年度：" },
                    { type: 'button', text: '统计', icon: '../../images/search.gif', disable: true, click: function () { doserch(); } },
                    { type: 'button', text: '重置', icon: '../../images/edit.gif', disable: true, click: function () { $("#serchform").each(function () { this.reset(); $(".l-selected").removeClass("l-selected"); }); } }
                ]
                //激活哪个
            });
            initSelect();

            $("#grid").height(document.documentElement.clientHeight - $(".toolbar").height() - 350);


            //$("#maingrid5").ligerGetGridManager().onResize();
            doserch();
        });

        function initSelect() {
            $("#sAction").ligerComboBox({
                width: 100,
                initValue: 'CRM_ADD',
                data: [
                    { 'text': '客户新增', 'id': 'CRM_ADD' },
                    { 'text': '回款金额', 'id': 'Receve_money' },
                    { 'text': '合同数目', 'id': 'CustomerSource' },
                    { 'text': '成交金额', 'id': 'Transaction' },
                    { 'text': '订单数目', 'id': 'CustomerSource' },
                    { 'text': '订单金额', 'id': 'CustomerSource' },
                    { 'text': '跟进数目', 'id': 'CustomerSource' }
                ],
                onSelected: function (newvalue, newtext, newid) {
                    if (newvalue == "CRM_ADD") {
                        $("#stype").parent().parent().show().prev().show().prev().show();
                    }
                    else {
                        $("#stype").parent().parent().hide().prev().hide().prev().hide();
                    }

                }
            })

            $("#stype").ligerComboBox({
                width: 200,
                isMultiSelect: true,
                url: "../../data/param_sysparam.ashx?Action=combo&parentid=1&rnd=" + Math.random()
            })
            var d = new Date();
            var nowYear = +d.getFullYear();
            var syearData = [];
            for (var i = nowYear; i >= nowYear - 20; i--) {
                syearData.push({ 'text': i, 'id': i });
            }
            $("#syear").ligerComboBox({
                width: 100,
                data: syearData,
                initValue: nowYear

            })

        }

        function test(GridData) {
            var series = [], series1 = [];
            var legend = [];

            var total = GridData.Rows.length;
            var count = 0;
            for (var i = 0; i < GridData.Rows.length; i++) {
                var row = GridData.Rows[i];
                count = count + row.cc;
                var CustomerType = GridData.Rows[i].CustomerType;
                series.push({ name: row.CustomerType, value: parseInt((total - i) * 100 / total) });
                legend.push(CustomerType);
            }
            for (var i = 0; i < GridData.Rows.length; i++) {
                var row = GridData.Rows[i]; 
                var CustomerType = GridData.Rows[i].CustomerType;
                series1.push({ name: row.CustomerType, value: parseInt(row.cc * 100 / count) });
            }


            var myChart = echarts.init(document.getElementById('graph'));

            var option = {
                title: {
                    text: '漏斗图'
                },      

                legend: {
                    data: legend
                },
                calculable: true,
                series: [
                    {
                        name: '预期',
                        type: 'funnel',
                        x: '10%',
                        width: '40%',
                        itemStyle: {
                            normal: {
                                label: {
                                    formatter: '{b}'
                                },
                                labelLine: {
                                    show: false
                                }
                            },
                            emphasis: {
                                label: {
                                    position: 'inside',
                                    formatter: '{b} : {c}%'
                                }
                            }
                        },
                        data: series
                    }
                    ,
                    {
                        name: '实际',
                        type: 'funnel',
                        x: '10%',
                        width: '40%',
                        maxSize: '80%',
                        itemStyle: {
                            normal: {
                                borderColor: '#fff',
                                borderWidth: 2,
                                label: {
                                    position: 'inside',
                                    formatter: '{c}%',
                                    textStyle: {
                                        color: '#fff'
                                    }
                                }
                            },
                            emphasis: {
                                label: {
                                    position: 'inside',
                                    formatter: '{b} 实际 : {c}%'
                                }
                            }
                        },
                        data: series1
                    }
                ]
            };

            // 为echarts对象加载数据 
            myChart.setOption(option);
            $("#graph").css({ "filter": "alpha(opacity=100)", "background": "#fff" });
        }
        function doserch() {

            var sendtxt = "&Action=Funnel&rnd=" + Math.random();
            var serchtxt = $("#serchform :input").fieldSerialize() + sendtxt;
            //alert(serchtxt);
            var manager = $("#maingrid5").ligerGetGridManager();

            top.$.ligerDialog.waitting('数据查询中,请稍候...');
            $.ajax({
                url: "../../data/Reports_CRM.ashx", type: "POST",
                data: serchtxt,
                dataType: 'json',
                beforeSend: function () {
                    //manager.showData({ Rows: [], Total: 0 });
                },
                success: function (responseText) {

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

    <form id="form1" onsubmit="return false">
        <div id="griddiv">
            <div id="graph" style="height: 280px; margin: 5px;"></div>

            <div id="maingrid5" style="margin: -1px -1px;"></div>
        </div>
    </form>


</body>
</html>
