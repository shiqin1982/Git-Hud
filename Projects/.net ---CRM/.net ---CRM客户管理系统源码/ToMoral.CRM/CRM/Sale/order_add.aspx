<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../CSS/input.css" rel="stylesheet" />
    <link href="../../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />


    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>

    <script src="../../lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>

    <script src="../../lib/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/common.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../lib/jquery.form.js" type="text/javascript"></script>

    <script src="../../lib/json2.js" type="text/javascript"></script>

    <script src="../../JS/tomoral.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.metadata.setType("attr", "validate");
            tomoral.validate($(form1));

            //$("#T_Contract_name").focus();
            $("form").ligerForm();

            $("#T_Customer").ligerComboBox({
                width: 489,
                onBeforeOpen: f_selectCustomer
            })
            $("#f_emp_view").ligerComboBox({
                width: 198,
                onBeforeOpen: f_selectContact
            })
            initcombo();
            if (getparastr("orderid")) {
                loadForm(getparastr("orderid"));
            }

            $("#maingrid4").ligerGrid({
                columns: [
                        //{ display: '序号', width: 30, render: function (item, i) { return i + 1;} },
                        { display: '产品名', name: 'product_name', width: 120 },
                        {
                            display: '单价', name: 'price', width: 80, type: 'float', align: 'right', editor: { type: 'float' }, render: function (item) {
                                return toMoney(item.price);
                            }
                        },
                        { display: '数量', name: 'quantity', width: 100, type: 'int', editor: { type: 'int',isNegative:false, onChange: onChange } },
                        { display: '单位', name: 'unit', width: 40, editor: { type: 'text' } },
                        {
                            display: '总价', name: 'amount', width: 100, type: 'float', align: 'right', render: function (item) {
                                return toMoney(item.amount);
                            }
                        }

                ],
                //selectRowButtonOnly: true,
                onAfterEdit: f_onAfterEdit,
                enabledEdit: true,
                usePager: false,
                checkbox: true,
                url: "../../data/CRM_order_details.ashx?Action=grid&orderid=" + getparastr("orderid"),
                width: '598px', height: '150px',
                heightDiff: -1
            })

        });
        function onChange(rowcell, newValue) {
            var g = $("#maingrid4").ligerGetGridManager();
            var row = $(rowcell).parent();
            var rowid = row.attr("rowid");
            var rowdata = g.getRow(rowid);

            g.updateCell('amount', rowdata.price * rowdata.quantity, rowdata);
            $("#T_amount").val(toMoney(g.getColumnDateByType('amount', 'sum') * 1.0));
        }
        function f_onAfterEdit(e) {
            var manager = $("#maingrid4").ligerGetGridManager();
            manager.updateCell('amount', e.record.price * e.record.quantity, e.rowObj);
            $("#T_amount").val(toMoney(manager.getColumnDateByType('amount', 'sum') * 1.0));
        }
        function initcombo() {
            $('#T_status').ligerComboBox({ width: 182, url: "../../data/Param_SysParam.ashx?Action=combo&parentid=6&rnd=" + Math.random() });
            $('#T_paytype').ligerComboBox({ width: 196, url: "../../data/Param_SysParam.ashx?Action=combo&parentid=5&rnd=" + Math.random() });

            c = $('#T_employee1').ligerComboBox({ width: 96 });
            d = $('#T_department1').ligerComboBox({
                width: 96,
                selectBoxWidth: 240,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                tree: {
                    url: '../../data/hr_department.ashx?Action=tree&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue, newtext, newid) {
                    $.get("../../data/hr_employee.ashx?Action=combo&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        c.setData(eval(data));
                        c.selectValue(newid);
                    });
                }
            });
        }
        function f_selectCustomer() {
            $.ligerDialog.open({
                title: '选择客户', width: 600, height: 350, url: '../../crm/customer/GetCustomer.aspx', buttons: [
                    { text: '确定', onclick: f_selectCustomerOK },
                    { text: '取消', onclick: f_selectContactCancel }
                ]
            });
            return false;
        }
        function f_selectCustomerOK(item, dialog) {
            var data = dialog.frame.f_select();
            if (!data) {
                alert('请选择行!');
                return;
            }
            $("#T_Customer").val(data.Customer);
            $("#T_Customer_val").val(data.id);
            $("#c_emp_view").val("【" + data.Department + "】" + data.Employee);
            $("#c_emp").val(data.Employee);
            $("#c_emp_val").val(data.Employee_id);
            $("#c_dep").val(data.Department);
            $("#c_dep_val").val(data.Department_id);
            dialog.close();
        }
        function f_selectContactCancel(item, dialog) {
            dialog.close();
        }
        function f_selectContact() {
            top.$.ligerDialog.open({
                zindex: 9003,
                title: '选择员工', width: 850, height: 400, url: "../../hr/Getemp_Auth.aspx?auth=3", buttons: [
                    { text: '确定', onclick: f_selectContactOK },
                    { text: '取消', onclick: f_selectContactCancel }
                ]
            });
            return false;
        }
        function f_selectContactOK(item, dialog) {
            var data = dialog.frame.f_select();
            if (!data) {
                alert('请选择员工!');
                return;
            }
            $("#f_emp_view").val("【" + data.dname + "】" + data.name);
            $("#f_emp").val(data.name);
            $("#f_emp_val").val(data.ID);
            $("#f_dep").val(data.dname);
            $("#f_dep_val").val(data.d_id);
            dialog.close();
        }

        function loadForm(oaid) {
            $.ajax({
                type: "GET",
                url: "../../data/CRM_order.ashx", /* 注意后面的名字对应CS的方法名称 */
                data: { Action: 'form', orderid: oaid, rnd: Math.random() }, /* 注意参数的格式和名称 */
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = eval(result);
                    for (var n in obj) {
                        if (obj[n] == "null" || obj[n] == null)
                            obj[n] = "";
                    }
                    //alert(UrlDecode("2013%2F4%2F14%200%3A00%3A00"));
                    //alert(obj.constructor); //String 构造函数
                    $("#T_Customer").val(obj.Customer_name);
                    $("#T_Customer_val").val(obj.Customer_id);

                    $("#f_emp_view").val("【" + obj.F_dep_name + "】" + obj.F_emp_name);
                    $("#f_dep_val").val(obj.F_dep_id);
                    $("#f_dep").val(obj.F_dep_name);
                    $("#f_emp").val(obj.F_emp_name);
                    $("#f_emp_val").val(obj.F_emp_id);

                    $("#T_date").val(formatTimebytype(obj.Order_date, "yyyy-MM-dd"));
                    $("#T_details").val(obj.Order_details);
                    $("#T_amount").val(toMoney(obj.Order_amount));

                    //$("#T_department").val(obj.C_dep_name + " · " + obj.C_emp_name);
                    $("#c_emp_view").val("【" + obj.C_dep_name + "】" + obj.C_emp_name);
                    $("#c_emp").val(obj.C_emp_name);
                    $("#c_emp_val").val(obj.C_emp_id);
                    $("#c_dep").val(obj.C_dep_name);
                    $("#c_dep_val").val(obj.C_dep_id);
                    //$("#T_department1").ligerGetComboBoxManager().selectValue(obj.F_dep_id, obj.F_emp_id);
                    $("#T_status").ligerGetComboBoxManager().selectValue(obj.Order_status_id);
                    $("#T_paytype").ligerGetComboBoxManager().selectValue(obj.pay_type_id);
                }
            });
        }

        var activeDialog = null;
        function f_openWindow(url, title, width, height) {
            var dialogOptions = {
                zindex: 9002,
                width: width, height: height, title: title, url: url, buttons: [
                        {
                            text: '保存', onclick: function (item, dialog) {
                                f_getpost(item, dialog);
                            }
                        },
                        {
                            text: '关闭', onclick: function (item, dialog) {
                                dialog.close();
                            }
                        }
                ], isResize: true, timeParmName: 'a'
            };
            activeDialog = parent.jQuery.ligerDialog.open(dialogOptions);
        }


        function add() {
            f_openWindow("crm/product/GetProduct.aspx", "选择产品", 580, 400);
        }
        function pro_remove() {
            var manager = $("#maingrid4").ligerGetGridManager();
            manager.deleteSelectedRow();
            $("#T_amount").val(toMoney(manager.getColumnDateByType('amount', 'sum') * 1.0));
        }
        function f_getpost(item, dialog) {
            var rows = null;
            if (!dialog.frame.f_select()) {
                alert('请选择产品!');
                return;
            }
            else {
                rows = dialog.frame.f_select();

                //过滤重复
                var manager = $("#maingrid4").ligerGetGridManager();
                var data = manager.getCurrentData();

                for (var i = 0; i < rows.length; i++) {

                    var add = 1;
                    for (var j = 0; j < data.length; j++) {
                        if (rows[i].product_id == data[j].product_id) {
                            add = 0;
                        }
                    }
                    if (add == 1) {
                        //price
                        rows[i].quantity = 1;
                        rows[i]["amount"] = rows[i].price * rows[i].quantity;
                        manager.addRow(rows[i]);
                    }
                }
                dialog.close();
            }
            $("#T_amount").val(toMoney(manager.getColumnDateByType('amount', 'sum') * 1.0));

            f_checkquantity();
        }

        function f_postnum() {
            return $("#maingrid4 td.l-grid-row-cell[columnname=product_name]").length;
        }
        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=save&orderid=" + getparastr("orderid");
                return $("form :input").fieldSerialize() + sendtxt;
            }
        }
        function f_postdata() {
            var manager = $("#maingrid4").ligerGetGridManager();
            var data = manager.getCurrentData();
            return JSON.stringify(data);
        }
        function f_checkquantity() {

            var checked = true;
            if ($("#maingrid4 td.l-grid-row-cell[columnname=quantity]").length > 0) {
                $("#maingrid4 td.l-grid-row-cell[columnname=quantity]").each(function () {
                    if ($(this).text() == "" || $(this).text() == "0")
                        checked = false;
                })
            }
            else {
                checked = true;
            }
            return checked;
        }
        function f_check() {
            var g = $("#maingrid4").ligerGetGridManager().endEdit(true);
        }
    </script>

</head>
<body style="margin: 0">
    <form id="form1" onsubmit="return false">
        <div>
            <table style="width: 600px; margin: 5px;" class='bodytable1'>
                <tr>
                    <td colspan="4" class="table_title1">基本信息</td>
                </tr>

                <tr>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            客户：
                        </div>
                    </td>
                    <td colspan="3">
                        <input type="text" id="T_Customer" name="T_Customer" validate="{required:true}" /></td>
                </tr>
                <tr>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            客户所属：
                        </div>
                    </td>
                    <td>
                        <div style="width: 100px; float: left">
                            <input id="c_emp_view" name="c_emp_view" type="text" ltype="text" ligerui="{width:196,disabled:true}" validate="{required:true}" />
                            <input type="hidden" id="c_dep" name="c_dep" />
                            <input type="hidden" id="c_dep_val" name="c_dep_val" />
                            <input type="hidden" id="c_emp" name="c_emp" />
                            <input type="hidden" id="c_emp_val" name="c_emp_val" />
                        </div>
                    </td>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            成交时间：
                        </div>
                    </td>
                    <td>
                        <input type="text" id="T_date" name="T_date" ltype="date" validate="{required:true}" ligerui="{width:182}" /></td>
                </tr>
                <tr>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            促成人员：
                        </div>
                    </td>
                    <td>
                        <input id="f_emp_view" name="f_emp_view" type="text" validate="{required:true}" />
                        <input type="hidden" id="f_dep" name="f_dep" />
                        <input type="hidden" id="f_dep_val" name="f_dep_val" />
                        <input type="hidden" id="f_emp" name="f_emp" />
                        <input type="hidden" id="f_emp_val" name="f_emp_val" />
                    </td>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            订单状态：
                        </div>
                    </td>
                    <td>

                        <input type="text" id="T_status" name="T_status" validate="{required:true}" /></td>
                </tr>
                <tr>
                    <td>
                        <div align="right" style="width: 62px">订单详情：</div>
                    </td>
                    <td colspan="3">
                        <textarea cols="100" id="T_details" name="T_details" rows="3" class="l-textarea" style="width: 488px"></textarea></td>
                </tr>

                <tr>
                    <td>
                        <div align="right" style="width: 62px">支付方式：</div>
                    </td>
                    <td>
                        <input type="text" id="T_paytype" name="T_paytype" validate="{required:true}" />
                    </td>
                    <td>
                        <div align="right" style="width: 62px">金额：</div>
                    </td>
                    <td>
                        <input type="text" id="T_amount" name="T_amount" ltype="text" style="text-align: right" value="0" validate="{required:true}" ligerui="{width:182,disabled:true}" /></td>
                </tr>
                <tr>
                    <td colspan="4" class="table_title1">订单产品</td>
                </tr>
                <tr>
                    <td>
                        <div align="right" style="width: 62px">产品：</div>
                    </td>
                    <td colspan="3">
                        <input id="Button3" type="button" value="新增" style="height: 21px" onclick="add()" />
                        <input id="Button4" type="button" value="删除" style="height: 21px" onclick="pro_remove()" /></td>


                </tr>
            </table>
        </div>
    </form>
    <form id="form2">
        <div id="maingrid4" style="margin: 5px;"></div>
    </form>

</body>
</html>
