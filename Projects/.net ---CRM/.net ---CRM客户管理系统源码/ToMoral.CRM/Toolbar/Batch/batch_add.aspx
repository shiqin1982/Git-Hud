<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/input.css" rel="stylesheet" type="text/css" />

    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>   

    <script src="../../lib/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/common.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../JS/tomoral.js" type="text/javascript"></script>
    <script src="../../lib/jquery.form.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $.metadata.setType("attr", "validate");
            tomoral.validate($(form1));

            //$("#T_Contract_name").focus();
            //$("form").ligerForm();
            
            $('#T_employee1').ligerComboBox({ width: 196, onBeforeOpen: f_selectContact1 });
            $('#T_employee2').ligerComboBox({ width: 196, onBeforeOpen: f_selectContact2 });
        });
        function f_selectContact1() {
            top.$.ligerDialog.open({
                zindex: 9003,
                title: '选择员工', width: 850, height: 400, url: "../../hr/Getemp_Auth.aspx", buttons: [
                    { text: '确定', onclick: function (item, dialog) { f_selectContactOK(item, dialog, 1) } },
                    { text: '取消', onclick: f_selectContactCancel }
                ]
            });
            return false;
        }
        function f_selectContact2() {
            top.$.ligerDialog.open({
                zindex: 9003,
                title: '选择员工', width: 850, height: 400, url: "../../hr/Getemp_Auth.aspx", buttons: [
                    { text: '确定', onclick: function (item, dialog) { f_selectContactOK(item, dialog, 2) } },
                    { text: '取消', onclick: f_selectContactCancel }
                ]
            });
            return false;
        }
        function f_selectContactOK(item, dialog, type) {
            var data = dialog.frame.f_select();
            if (!data) {
                alert('请选择员工!');
                return;
            }
            switch (type) {
                case 1:
                    $("#T_employee1").val("【" + data.dname + "】" + data.name);
                    $("#T_employee11").val(data.name);
                    $("#T_employee1_val").val(data.ID);
                    $("#T_dep1").val(data.dname);
                    $("#T_dep1_val").val(data.d_id);
                    break;
                case 2:
                    $("#T_employee2").val("【" + data.dname + "】" + data.name);
                    $("#T_employee22").val(data.name);
                    $("#T_employee2_val").val(data.ID);
                    $("#T_dep2").val(data.dname);
                    $("#T_dep2_val").val(data.d_id);
                    break;
            }
            dialog.close();
        }
        function f_selectContactCancel(item, dialog) {
            dialog.close();
        }
        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=save&type=" + getparastr("type");
                return $("form :input").fieldSerialize() + sendtxt;
            }
        }

        
        function f_check()
        {
            if ($(form1).valid()) {
                var emp1 = $("#T_employee1_val").val();
                var emp2 = $("#T_employee2_val").val();

                if (emp1 == emp2)
                    return false;
                else
                    return true;
            }
        }

    </script>
</head>
<body>
    <form id="form1" onsubmit="return false">
        <table align="left" border="0" cellpadding="3" cellspacing="1" class="bodytable2"
            style="background: #fff; width: 320px; margin: 5px;">

            <tr>
                <td width="65px">转出人：</td>
                <td>
                    <input type="text" id="T_employee1" name="T_employee1" validate="{required:true}" />
                    <input id="T_employee11" name="T_employee11" type="hidden" />
                    <input id="T_dep1_val" name="T_dep1_val" type="hidden" />
                    <input id="T_dep1" name="T_dep1" type="hidden" />
                </td>
            </tr>

            <tr>
                <td width="65px">转入人：</td>
                <td>
                    <input type="text" id="T_employee2" name="T_employee2" validate="{required:true}" />
                    <input id="T_employee22" name="T_employee22" type="hidden" />
                    <input id="T_dep2_val" name="T_dep2_val" type="hidden" />
                    <input id="T_dep2" name="T_dep2" type="hidden" />
                </td>

            </tr>
        </table>
    </form>
</body>
</html>
