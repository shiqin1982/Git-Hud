using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using tomoral.Common;

namespace tomoral.CRM.Data
{
    /// <summary>
    /// Reports_CRM 的摘要说明
    /// </summary>
    public class Reports_CRM : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            if (request["Action"] == "CRM_Reports_year")
            {
                tomoral.BLL.CRM_Customer ccc = new tomoral.BLL.CRM_Customer();

                string items = PageValidate.InputText(context.Request["stype_val"], 255);
                int year = int.Parse(context.Request["syear_val"]);

                DataSet ds = ccc.Reports_year(items, year, "");

                string dt = tomoral.Common.GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }
            if (request["Action"] == "Follow_Reports_year")
            {
                tomoral.BLL.CRM_Follow follow = new tomoral.BLL.CRM_Follow();

                string items = "Follow_Type";
                int year = int.Parse(context.Request["syear_val"]);

                DataSet ds = follow.Reports_year(items, year, "");

                string dt = tomoral.Common.GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }

            //if (request["Action"] == "Funnel")
            //{
            //    string whereStr = " 1=1 ";
            //    int year = int.Parse(context.Request["syear_val"]);
            //    if (year > 0)
            //        whereStr += string.Format(" and datediff(year,b.Create_date,'{0}-01-01')=0", year);

            //    string items = PageValidate.InputText(context.Request["stype_val"], 255);
            //    if (!string.IsNullOrEmpty(items) && items != "null")
            //        whereStr += string.Format(" and a.id in({0})", items.Replace(';',','));

            //    //context.Response.Write(whereStr);

            //    BLL.CRM_Customer ccc = new BLL.CRM_Customer();
            //    DataSet ds = ccc.Funnel(whereStr);

            //    string dt = Common.GetGridJSON.DataTableToJSON(ds.Tables[0]);
            //    context.Response.Write(dt);
            //}
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