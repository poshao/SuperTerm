using System;
using Spoonson.Common.RemoteInvoke;

namespace Spoonson
{
    class SampleCode : MarshalByRefObject, IRemoteCall
    {
        public object Invoke(string MethodFullName, object[] Parameters)
        {
            return this.GetType().InvokeMember(MethodFullName, System.Reflection.BindingFlags.InvokeMethod, null, this, Parameters);
        }

        public string GetName()
        {
            return "hello, I am client!";
        }
    }
}
