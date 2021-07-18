using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressCode.Model
{
    //返回码
    public static class ResultCode
    {
        // 成功状态码
        public const int SUCCESS = 1;

        // -------------------失败状态码----------------------
        // 参数错误
        public const int PARAMS_IS_NULL = 101;// 参数为空
        public const int PARAMS_NOT_COMPLETE = 102; // 参数不全
        public const int PARAMS_TYPE_ERROR = 103; // 参数类型错误
        public const int PARAMS_IS_INVALID = 104; // 参数无效

        // 用户错误
        public const int USER_NOT_EXIST = 201; // 用户不存在
        public const int USER_NOT_LOGGED_IN = 202; // 用户未登陆
        public const int USER_ACCOUNT_ERROR = 203; // 用户名或密码错误
        public const int USER_ACCOUNT_FORBIDDEN = 204; // 用户账户已被禁用
        public const int USER_HAS_EXIST = 205;// 用户已存在

        // 业务错误
        public const int BUSINESS_ERROR = 301;// 系统业务出现问题

        // 系统错误
        public const int SYSTEM_INNER_ERROR = 401; // 系统内部错误

        // 数据错误
        public const int DATA_NOT_FOUND = 501; // 数据未找到
        public const int DATA_IS_WRONG = 502;// 数据有误
        public const int DATA_ALREADY_EXISTED = 503;// 数据已存在

        // 接口错误
        public const int INTERFACE_INNER_INVOKE_ERROR = 601; // 系统内部接口调用异常
        public const int INTERFACE_OUTER_INVOKE_ERROR = 602;// 系统外部接口调用异常
        public const int INTERFACE_FORBIDDEN = 603;// 接口禁止访问
        public const int INTERFACE_ADDRESS_INVALID = 604;// 接口地址无效
        public const int INTERFACE_REQUEST_TIMEOUT = 605;// 接口请求超时
        public const int INTERFACE_EXCEED_LOAD = 606;// 接口负载过高

        // 权限错误
        public const int PERMISSION_NO_ACCESS = 701;// 没有访问权限
    }
}
