namespace ForumWebApi.Models
{
    public class PostFrontend
    {
        /// <summary>
        /// postId
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 文章內容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public string CreateAt { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public string UpdateAt { get; set; }
    }
}
