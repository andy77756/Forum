using System;

namespace ForumLib.Models
{
    /// <summary>
    /// token map info
    /// </summary>
    public class TokenMapInfo
    {
        /// <summary>
        /// expired datetime
        /// </summary>
        public DateTime ExpiredDateTime { get; set; }

        /// <summary>
        /// permession
        /// </summary>
        public int Permission { get; set; }
    }
}
