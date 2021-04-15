using System;

namespace BlazorComponent
{
    public interface IRendered<TItem>
    {
        /// <summary>
        /// 渲染完成后
        /// </summary>
        Action OnRendered { get; set; }

        /// <summary>
        /// 新节点数据，用于展开并选择新节点
        /// </summary>
        TItem NewChildData { get; set; }
    }
}
