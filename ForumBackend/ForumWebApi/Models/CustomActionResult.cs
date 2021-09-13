using ForumLib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForumWebApi.Models
{
    /// <summary>
    /// 客製化ActionResult
    /// </summary>
    /// <typeparam name="T">回傳物件型別</typeparam>
    public class CustomActionResult<T> : IActionResult where T : class
    {
        /// <summary>
        /// Result
        /// </summary>
        private readonly Result<T> Result;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="result">回傳結果</param>
        public CustomActionResult(Result<T> result)
        {
            Result = result;
        }
        /// <summary>
        /// 執行回傳結果方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var option = new JsonSerializerOptions();
            option.PropertyNameCaseInsensitive = false;
            var resultString = JsonSerializer.Serialize(new { statusCode = Result.StatusCode, returnData = Result.ReturnData}, option);
            var objectBytes = Encoding.UTF8.GetBytes(resultString);

            await context.HttpContext.Response.Body.WriteAsync(objectBytes.ToArray());
        }
    }

    public class CustomActionResult: CustomActionResult<object>
    {
        public CustomActionResult(Result result):base(result)
        {
        }
    }
}
