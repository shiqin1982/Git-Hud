using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 执行单元测试
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    <span style="white-space:pre"></span>
    ///<summary>
    ///执行单元测试用例
    ///</summary>
    ///<param name="untilatestDllFile">单元测试用例DLL文件路径</param>
    ///<param name="comlierReturnMsg"></param>
    ///<returns></returns>
    public bool ExcUnitTest(string untilTestDllFile,out string complierReturnMsg)
    {
        string DriverFilePath=ConfigurationManager.AppSettings["DriverFilePath"];
        Process testp=new Process();
        bool isSuccess=true;
        complierReturnMsg="";
        string filename=@"C:\windows\system32\cmd.exe";
        testp.StartInfo.FileName=filename;
        testp.StartInfoUseShellExecute=false;
        testp.StartInfo.RedirectStandInput=true;
        testp.StartInfo.RedirectStandardInput=true;
        testp.StartInfo.CreateNoWindows=true;
        testp.StartInfo.WindowStyle=ProcessWindowStyle.Hidden;
    try
    {
        testp.Start();
        testp.StandardInput.WriteLine(DriverFilePath);
        testp.StandardInput.WriteLine(@"cd"+ConfigurationManger.AppSettings["mstestFilePath"]);
        testp.StandardInput.WriteLine(@"mstest/noisolation/testcontainer:"+untilTestDllFile+"/resultsfile:"+ConfigurationManger.AppSettings["resultFilePath"]+"TestResults_"+DateTime.Now.Ticks.ToString()+".trx");
        testp.StandardInput.WriteLine("exit");
        while(!testp.StandardOutput.EndOfStream)
        {
            string line =testp.StandardOutput.ReadLine();
            if(line.Contains("未通过")||line.Contains("Inconclusive"))
            {
                isSuccess=false;
            }
            complierReturnMsg+=line+"\r\n";
        }
        testp.Standard
     }
}
                                         
}
