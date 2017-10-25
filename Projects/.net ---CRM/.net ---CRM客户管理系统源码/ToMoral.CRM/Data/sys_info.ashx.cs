using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using tomoral.Common;
using System.Security.Cryptography;

namespace tomoral.CRM.Data
{
    /// <summary>
    /// sys_info 的摘要说明
    /// </summary>
    public class sys_info : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            tomoral.BLL.sys_info info = new tomoral.BLL.sys_info();
            tomoral.Model.sys_info model = new tomoral.Model.sys_info();

            if (request["Action"] == "grid")
            {
                DataSet ds = info.GetAllList();
                context.Response.Write(tomoral.Common.GetGridJSON.DataTableToJSON(ds.Tables[0]));
            }

            if (request["Action"] == "getinfo")
            {
                DataSet ds = info.GetList(" id=2 or id=3");
                context.Response.Write(tomoral.Common.GetGridJSON.DataTableToJSON(ds.Tables[0]));
            }
            if (request["Action"] == "up")
            {
                model.sys_value = PageValidate.InputText(request["T_name"], int.MaxValue);
                model.id = 2;

                info.Update(model);
            }

            if (request["Action"] == "logo")
            {
                string fileName = request["filename"];    //文件路径
                fileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                string sExt = fileName.Substring(fileName.LastIndexOf(".")).ToLower();

                DateTime now = DateTime.Now;
                string nowfileName = now.ToString("yyyyMMddHHmmss") + GetRandom(6) + sExt;

                HttpPostedFile uploadFile = request.Files[0];
                uploadFile.SaveAs(context.Server.MapPath(@"~/images/logo/" + nowfileName));

                //context.Response.Write(nowfileName);
                model.sys_value = "images/logo/" + nowfileName;
                model.id = 3;

                info.Update(model);

            }
        }
        #region GetRandom
        private string GetRandom(int length)
        {
            byte[] random = new Byte[length / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(random);

            StringBuilder sb = new StringBuilder(length);
            int i;
            for (i = 0; i < random.Length; i++)
            {
                sb.Append(String.Format("{0:X2}", random[i]));
            }
            return sb.ToString();
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}