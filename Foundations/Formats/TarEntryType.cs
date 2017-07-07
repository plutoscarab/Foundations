using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundations.Formats
{
    /// <summary>
    /// 
    /// </summary>
    public enum TarEntryType
    {
        /// <summary>
        /// A regular file.
        /// </summary>
        RegularFile,

        /// <summary>
        /// 
        /// </summary>
        HardLink,

        /// <summary>
        /// 
        /// </summary>
        SymbolicLink,

        /// <summary>
        /// 
        /// </summary>
        CharacterDeviceNode,

        /// <summary>
        /// 
        /// </summary>
        BlockDeviceNode,

        /// <summary>
        /// 
        /// </summary>
        Directory,

        /// <summary>
        /// 
        /// </summary>
        FIFONode,

        /// <summary>
        /// 
        /// </summary>
        Reserved,

        /// <summary>
        /// 
        /// </summary>
        Other,
    }
}
