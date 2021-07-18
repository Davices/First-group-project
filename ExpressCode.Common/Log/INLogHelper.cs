using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressCode.Common
{
    //NLogHelper接口
    public interface INLogHelper
    {
        void LogError(Exception ex);
    }
}
