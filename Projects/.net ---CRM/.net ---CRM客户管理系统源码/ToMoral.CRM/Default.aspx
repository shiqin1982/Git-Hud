<%@ Page Language="C#" AutoEventWireup="true" %>

<!--���ҳ��->
<%
    //ˢ�¾�̬��������  
    tomoral.CRM.Data.install inss = new tomoral.CRM.Data.install();
    string filename = Server.MapPath("/conn.config");
    inss.CheckConfig(filename);
    string filename1 = Server.MapPath("/Web.config");
    inss.CheckConfig(filename1);

    //�ж��Ƿ�������
    tomoral.CRM.Data.install ins = new tomoral.CRM.Data.install();
    int configed = ins.configed();

    if (configed == 1)
    {
        //�ж��Ƿ��½
        HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName]; 
        if (Request.IsAuthenticated && null!=cookie)
            Response.Redirect("main.aspx");

        else
            Response.Redirect("login.aspx");
        //Response.Redirect("login.aspx");
    }
    else
        Response.Redirect("install/index.aspx");
 %>
