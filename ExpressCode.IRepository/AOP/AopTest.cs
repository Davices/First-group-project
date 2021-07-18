using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressCode
{
    public class AopTest : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("方法执行前");
            invocation.Proceed();//在被拦截的方法执行完毕后 继续执行
            Console.WriteLine("方法执行后");
        }
    }
}
