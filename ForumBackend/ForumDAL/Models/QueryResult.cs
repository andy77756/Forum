namespace ForumDAL.Models
{
    public class QueryResult<T> where T : class
    {
        /// <summary>
        /// 狀態碼
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 回傳結果
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 全部資料數量
        /// </summary>
        public int? Length { get; set; }
    }

}
