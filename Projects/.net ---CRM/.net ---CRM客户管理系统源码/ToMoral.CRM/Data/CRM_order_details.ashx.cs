using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using tomoral.Common;
using System.Web.Security;

namespace tomoral.CRM.Data
{
    /// <summary>
    /// CRM_order_details 的摘要说明
    /// </summary>
    public class CRM_order_details : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string CoockiesID = ticket.UserData;

            tomoral.BLL.hr_employee emp = new tomoral.BLL.hr_employee();
            int emp_id = int.Parse(CoockiesID);
            DataSet dsemp = emp.GetList("id=" + emp_id);
            string empname = dsemp.Tables[0].Rows[0]["name"].ToString();
            string uid = dsemp.Tables[0].Rows[0]["uid"].ToString();
           
            if (request["Action"] == "grid")
            {
                tomoral.BLL.CRM_order_details cod = new tomoral.BLL.CRM_order_details();
                string orderid = request["orderid"];               

                DataSet ds = cod.GetList(" order_id=" + int.Parse( orderid));
                context.Response.Write(tomoral.Common.GetGridJSON.DataTableToJSON(ds.Tables[0]));
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