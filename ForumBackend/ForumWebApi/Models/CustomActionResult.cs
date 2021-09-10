using ForumLib.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForumWebApi.Models
{
    public class CustomActionResult<T> : IActionResult where T : class
    {
        private readonly Result<T> Result;

        public CustomActionResult(Result<T> result)
        {
            Result = result;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {           
            var resultString = JsonSerializer.Serialize(Result);
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
