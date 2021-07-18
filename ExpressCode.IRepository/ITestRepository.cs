using Autofac.Extras.DynamicProxy;
using ExpressCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressCode.IRepository
{
    [Intercept(typeof(AopTest))]
    public interface ITestRepository
    {
        UserEntity test();
    }
}
