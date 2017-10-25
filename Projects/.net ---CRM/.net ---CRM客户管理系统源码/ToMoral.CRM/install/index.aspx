<%@ Page Language="C#" AutoEventWireup="true" %>

<%
    //刷新静态方法缓存  
    tomoral.CRM.Data.install inss = new tomoral.CRM.Data.install();
    string filename = Server.MapPath("/conn.config");
    inss.CheckConfig(filename);
    string filename1 = Server.MapPath("/Web.config");
    inss.CheckConfig(filename1);

    //判断是否已配置
    tomoral.CRM.Data.install ins = new tomoral.CRM.Data.install();
    int configed = ins.configed();

    if (configed == 1)
    {
       Response.Redirect( "remind.aspx");
    }    
   
 %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="IE=edge" http-equiv="X-UA-Compatible">
    <meta http-equiv="content-type" content="text/html; charset=gb2312">
    <title>俊逸CRM-安装</title>   
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>  
    <script type="text/javascript">
          $(function () {
           
            $("#btn_next").click(function () {
                window.location.href = "step2.aspx";
            });             
        })
    </script>
    <style type="text/css">
        img { border: none; }
        .text { border: #d2e2f2 1px solid; height: 19px; }
        body { BACKGROUND: url(../images/login/loginbackground1.jpg) repeat-x; font-size: 12px; }
    </style>
    <script type="text/javascript">
        if (top.location != self.location) top.location = self.location;
    </script>
</head>
<body>
    <form id="form1" name="form1">
        <div style="width: 731px; margin-left: 50px; margin-top: 100px;">
            <table id="__01" width="732" height="358" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>&nbsp;</h2>

                        <table class="auto-style1">
                            <tr>
                                <td style="border-bottom: 1px solid #d2e2f2; width: 400px;">
                                    &nbsp;</td>
                                <td style="border-bottom: 1px solid #d2e2f2;">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="145" style="text-align: center;">
                        <input type="button" value="同意" style="width: 80px; height: 25px;" id="btn_next" />
                    </td>
                </tr>
            </table>
        </div>
    </form>

</body>

</html>
