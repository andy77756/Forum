namespace ForumLib.Models
{
    public class Result<T> where T : class
    {
        public int StatusCode { get; set; }
        public T ReturnData { get; set; }

        public Result(int statusCode, T data = null)
        {
            StatusCode = statusCode;
            ReturnData = data;
        }
    }

    public class Result : Result<object>
    {
        public Result(int statusCode):base(statusCode)
        {
        }
    }

    
}
