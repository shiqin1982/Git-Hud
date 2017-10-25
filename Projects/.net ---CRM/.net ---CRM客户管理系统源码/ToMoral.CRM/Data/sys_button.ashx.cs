using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using tomoral.Common;

namespace tomoral.CRM.Data
{
    /// <summary>
    /// sys_button 的摘要说明
    /// </summary>
    public class sys_button : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            tomoral.BLL.Sys_Button btn = new tomoral.BLL.Sys_Button();
            tomoral.Model.Sys_Button model = new tomoral.Model.Sys_Button();
            if (request["Action"] == "GetGrid")
            {
                string menuid = request["menuid"];
                if (!string.IsNullOrEmpty(menuid))
                {
                    DataSet ds = btn.GetList(0, "Menu_id=" + (tomoral.Common.PageValidate.IsNumber(menuid) ? menuid : "-1"), "Btn_order");

                    context.Response.Write(tomoral.Common.GetGridJSON.DataTableToJSON(ds.Tables[0]));
                }   
            }
            //Form JSON
            if (request["Action"] == "form")
            {
                string btnid = request["btnid"];
                if (!string.IsNullOrEmpty(btnid))
                {
                    DataSet ds = btn.GetList("Btn_id=" + (tomoral.Common.PageValidate.IsNumber(btnid) ? btnid : "-1"));

                    string dt = tomoral.Common.DataToJson.DataToJSON(ds);

                    context.Response.Write(dt);
                }
            }
            //save
            if (request["Action"] == "save")
            {  
                string Menu_id = request["menuid"];
                if (string.IsNullOrEmpty(Menu_id))
                    Menu_id = "0";
                model.Menu_id = int.Parse(Menu_id);

                tomoral.BLL.Sys_Menu menu = new tomoral.BLL.Sys_Menu();

                model.Menu_name = menu.GetList("Menu_id=" + Menu_id).Tables[0].Rows[0]["Menu_name"].ToString();
                model.Btn_name = tomoral.Common.PageValidate.InputText(request["T_btn_name"], 255);
                model.Btn_icon = tomoral.Common.PageValidate.InputText(request["T_btn_icon"], 255);
                model.Btn_handler = tomoral.Common.PageValidate.InputText(request["T_btn_handler"], 255);
                model.Btn_order = tomoral.Common.PageValidate.InputText(request["T_btn_order"], 255);
               
                string id = request["btnid"];

                if (!string.IsNullOrEmpty(id) && id != "null")
                {
                    model.Btn_id = int.Parse(id);
                    btn.Update(model);
                }
                else
                {
                    int btnid = btn.Add(model);
                }
            }
            //del
            if (request["Action"] == "del")
            {
                int btnid = -1;
                string context_btnid =  context.Request["btnid"];
                if (!string.IsNullOrEmpty(context_btnid))
                {
                    btnid = tomoral.Common.PageValidate.IsNumber(context_btnid) ? int.Parse(context_btnid) : -1;
                }

                DataSet ds = btn.GetList("Btn_id=" + btnid);
                bool isdel = btn.Delete(btnid);
                if (isdel)
                {
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}