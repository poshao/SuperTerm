using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
namespace Spoonson.Apps.SuperTerm.test
{
    class CodeLoaderHelper
    {
        public void run_test()
        {
            //创建应用域
            var appSetup = new AppDomainSetup();
            appSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            var appdomain = AppDomain.CreateDomain("ClientSpace", null, appSetup);

            //创建编译器
            var provider = new CSharpCodeProvider();
            //配置编译参数
            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("RemoteInvoke.dll");
            compilerParameters.GenerateInMemory = false;
            compilerParameters.OutputAssembly = "client.dll";

            //读取代码编译
            string strCode = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\test\\SampleCode.cs");//代码

            CompilerResults compilerResults = provider.CompileAssemblyFromSource(compilerParameters, strCode);

            if (compilerResults.Errors.HasErrors)
            {
                Console.WriteLine("compile failed");
                return;
            }

            //调用
            //var remoteCall =(RemoteLoader)appdomain.CreateInstance("RemoteInvoke", "Spoonson.Common.RemoteInvoke.RemoteLoader").Unwrap();
            //var remoteClass=remoteCall.Create("client.dll", "Spoonson.SampleCode", null);
            //var result=(string)remoteClass.Invoke("GetName", null);
            //System.Diagnostics.Debug.WriteLine("get result: " + result);
            //remoteClass = null;
            //AppDomain.Unload(appdomain);
            //System.IO.File.Delete("client.dll");
        }
    }
}
