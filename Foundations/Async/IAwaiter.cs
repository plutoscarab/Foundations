using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Foundations.Async
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAwaiter : INotifyCompletion
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// 
        /// </summary>
        void GetResult();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IAwaiter<T> : INotifyCompletion
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// 
        /// </summary>
        T GetResult();
    }
}
