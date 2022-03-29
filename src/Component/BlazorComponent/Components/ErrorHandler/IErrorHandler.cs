using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IErrorHandler
    {
        /// <summary>
        /// 自定义 Error 处理方法
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        Task HandlerExceptionAsync(Exception ex);
    }
}
