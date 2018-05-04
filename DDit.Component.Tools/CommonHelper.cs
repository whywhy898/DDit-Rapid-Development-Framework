using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DDit.Component.Tools
{
   public class CommonHelper
    {

       /// <summary>
       /// 得到HTTP参数
       /// </summary>
       /// <param name="context"></param>
       /// <returns></returns>
       public static string GetStreamInfo(ControllerContext context) {

           context.HttpContext.Request.InputStream.Position = 0;
           using (StreamReader inputStream = new StreamReader(context.HttpContext.Request.InputStream))
           {
               var cc = inputStream.CurrentEncoding;
               byte[] byts = new byte[context.HttpContext.Request.InputStream.Length];
               context.HttpContext.Request.InputStream.Read(byts, 0, byts.Length);
               string req = System.Text.Encoding.UTF8.GetString(byts);
               req = HttpContext.Current.Server.UrlDecode(req);

               return req;
           }
       
       }

       /// <summary> 
       /// 将字符串中的运算符按正常计算 例如按四则运算 
       /// </summary> 
       /// <param name="expression">标准表达式如 1+15*0.5-200</param> 
       /// <returns>返回计算的值，可以为任意合法的数据类型</returns> 
       public static object MathCalculate(string expression)
       {
           object retvar = null;
           Microsoft.CSharp.CSharpCodeProvider provider = new Microsoft.CSharp.CSharpCodeProvider();
           System.CodeDom.Compiler.CompilerParameters cp = new System.CodeDom.Compiler.CompilerParameters(
           new string[] { @"System.dll" });
           StringBuilder builder = new StringBuilder("using System;class CalcExp{public static object Calc(){ return \"expression\";}}");
           builder.Replace("\"expression\"", expression);
           string code = builder.ToString();
           System.CodeDom.Compiler.CompilerResults results;
           results = provider.CompileAssemblyFromSource(cp, new string[] { code });
           if (results.Errors.HasErrors)
           {
               retvar = null;
           }
           else
           {
               System.Reflection.Assembly a = results.CompiledAssembly;
               Type t = a.GetType("CalcExp");
               retvar = t.InvokeMember("Calc", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.InvokeMethod
                   , System.Type.DefaultBinder, null, null);
           }
           return retvar;
       }

       /// <summary>
       /// EF返回DataTable
       /// </summary>
       /// <param name="database"></param>
       /// <param name="sql"></param>
       /// <returns></returns>
       public static DataTable SqlQueryForDataTatable(string str, string sql)
       {

           SqlConnection conn = new System.Data.SqlClient.SqlConnection();
           conn.ConnectionString = str;
           if (conn.State != ConnectionState.Open)
           {
               conn.Open();
           }

           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandText = sql;

           SqlDataAdapter adapter = new SqlDataAdapter(cmd);
           DataTable table = new DataTable();
           adapter.Fill(table);

           conn.Close();//连接需要关闭  
           conn.Dispose();
           return table;
       }

       /// <summary>
       /// 得到排序的列名称（针对关联字段）
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="dataName"></param>
       /// <returns></returns>
       public static string GetOrderName<T>(dynamic dataName) where T: class
       {
           Type type = typeof(T);
           object obj = Activator.CreateInstance(type);
           var isExit = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(a => a.Name == dataName.data).FirstOrDefault();
           return isExit == null ? dataName.name : dataName.data;
       }
    }

   public enum FlowState {
       /// <summary>
       /// 条件失败
       /// </summary>
       Failure = 0,
       /// <summary>
       /// 进行中
       /// </summary>
       Underway = 1,
       /// <summary>
       /// 结束
       /// </summary>
       Finish = 2,
       /// <summary>
       /// 驳回
       /// </summary>
       Reject=3
      
   }
}
