using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace tomoral.CRM.Data
{
    /// <summary>
    /// sys_menu 的摘要说明
    /// </summary>
    public class sys_menu : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HttpRequest request = context.Request;

            if (request["Action"] == "GetMenu")
            {
                tomoral.BLL.Sys_Menu menu = new tomoral.BLL.Sys_Menu();
                int appid = tomoral.Common.PageValidate.IsNumber(request["appid"]) ? int.Parse(request["appid"]) : 0;

                DataSet ds = menu.GetList(0, "App_id=" + appid, "Menu_order");
                //string dt = Common.GetGridJSON.DataTableToJSON(ds.Tables[0]);
                string dt = "{Rows:[" + GetTasksString(0, ds.Tables[0]) + "]}";
                context.Response.Write(dt);
            }
            //Form JSON
            if (request["Action"] == "form")
            {
                tomoral.BLL.Sys_Menu menu = new tomoral.BLL.Sys_Menu();
                DataSet ds = menu.GetList("Menu_id=" + int.Parse( request["menuid"]));

                string dt = tomoral.Common.DataToJson.DataToJSON(ds);

                context.Response.Write(dt);
            }
            if (request["Action"] == "SysTree")
            {
                tomoral.BLL.Sys_Menu menu = new tomoral.BLL.Sys_Menu();

                int appid = int.Parse(request["appid"]);

                DataSet ds = menu.GetList(0, "parentid=0 and App_id=" + appid, "Menu_order");

                StringBuilder str = new StringBuilder();
                str.Append("[{id:0,pid:0,text:'无',Menu_icon:''},");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{id:" + ds.Tables[0].Rows[i]["menu_id"].ToString() + ",pid:" + ds.Tables[0].Rows[i]["parentid"].ToString() + ",text:'" + ds.Tables[0].Rows[i]["menu_name"] + "',Menu_icon:'" + ds.Tables[0].Rows[i]["Menu_icon"] + "'},");
                }
                str.Replace(",", "", str.Length - 1, 1);
                str.Append("]");
                context.Response.Write(str);
            }
            //save
            if (request["Action"] == "save")
            {
                tomoral.BLL.Sys_Menu menu = new tomoral.BLL.Sys_Menu();
                tomoral.Model.Sys_Menu model = new tomoral.Model.Sys_Menu();

                model.Menu_name = tomoral.Common.PageValidate.InputText(request["T_menu_name"], 255);
                model.Menu_url = tomoral.Common.PageValidate.InputText(request["T_menu_url"], 255);
                model.Menu_icon = tomoral.Common.PageValidate.InputText(request["T_menu_icon"], 255);
                model.Menu_order = int.Parse(request["T_menu_order"]);
                model.Menu_type = "sys";
                model.parentid = int.Parse(request["T_menu_parent_val"]);
                model.parentname = tomoral.Common.PageValidate.InputText(request["T_menu_parent"], 255);
                model.App_id = int.Parse(request["appid"]);

                tomoral.BLL.hr_employee emp = new tomoral.BLL.hr_employee();

                string id = request["menuid"];
                if (!string.IsNullOrEmpty(id) && id != "null")
                {
                    model.Menu_id = int.Parse(id);
                    DataSet ds = menu.GetList("Menu_id=" + model.Menu_id);
                    DataRow dr = ds.Tables[0].Rows[0];

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Menu_type = ds.Tables[0].Rows[0]["Menu_type"].ToString();
                    }
                    menu.Update(model);
                }
                else
                {
                    int mid = menu.Add(model);   
                }
            }
        }
        private static string GetTasksString(int Id, DataTable table)
        {
            DataRow[] rows = table.Select("parentid=" + Id.ToString());

            if (rows.Length == 0) return string.Empty;
            StringBuilder str = new StringBuilder();

            foreach (DataRow row in rows)
            {
                str.Append("{");
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i != 0) str.Append(",");
                    str.Append(row.Table.Columns[i].ColumnName);
                    str.Append(":'");
                    str.Append(row[i].ToString());
                    str.Append("'");
                }
                if (GetTasksString((int)row["menu_id"], table).Length > 0)
                {
                    str.Append(",children:[");
                    str.Append(GetTasksString((int)row["menu_id"], table));
                    str.Append("]},");
                }
                else
                {
                    str.Append("},");
                }
            }
            return str[str.Length - 1] == ',' ? str.ToString(0, str.Length - 1) : str.ToString();
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