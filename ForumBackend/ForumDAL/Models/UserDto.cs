using System;

namespace ForumDAL.Models
{
    public class UserDto
    {
        /// <summary>
        /// user id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 暱稱
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 等級
        /// </summary>
        public int Level { get; set; }
    }
}
