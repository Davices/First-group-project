using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace ExpressCode.Services
{
    public class BaseService
    {
        private IMapper _mapper;

        public IServiceProvider ServiceProvider { get; set; }

        public IMapper Mapper => LazyGetRequiredService(ref _mapper);

        protected readonly object ServiceProviderLock = new object();

        //单例模式创建一个Mapper对象
        protected TService LazyGetRequiredService<TService>(ref TService reference)
        {
            if (reference == null)//一层判断
            {
                lock (ServiceProviderLock) //加锁
                {
                    if (reference == null)//再次判断
                    {
                        reference = ServiceProvider.GetRequiredService<TService>();
                    }
                }
            }
            return reference;
        }
    }
}
