using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Services;
using System.Web.UI;

namespace Forum_v2.ajax
{
    /// <summary>
    /// 討論區功能webmethod
    /// </summary>
    public partial class ForumService : Page
    {
        /// <summary>
        /// 取得指定文章內容
        /// </summary>
        /// <param name="id">文章Id</param>
        /// <param name="index">頁數</param>
        /// <param name="size">每頁大小</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetPostById(int id, int? index, int? size)
        {
            var url = ConfigurationManager.AppSettings["wabapiDomain"] + "/Forum/Reply?" + $"postId={id}&pageIndex={index ?? 0}&pageSize={size ?? 10}";
            var httpService = new HttpService();
            try
            {
                return httpService.Get(url);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -500
                });
            }
        }

        [WebMethod]
        public static string GetPosts(string topic, string nickname, int? index, int? size)
        {
            var url = ConfigurationManager.AppSettings["wabapiDomain"] + "/Forum/Posts?" + $"keyTopic={topic}&keyNickname={nickname}&pageIndex={index ?? 0}&pageSize={size ?? 10}";
            var httpService = new HttpService();
            try
            {
                return httpService.Get(url);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -500
                });
            }
        }

        [WebMethod]
        public static string Addpost(int userId, string topic, string content, string token)
        {
            var validator = new TokenValidator();
            var claims = validator.GetClaimPrincipal(token);

            if (validator.IsExpired(claims.Claims.FirstOrDefault(x => x.Type == "exp").Value))
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -7
                });
            }

            if (!validator.IsLevelTwo(claims.Claims.FirstOrDefault(x => x.Type == "level").Value))
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -8
                });
            }


            var url = ConfigurationManager.AppSettings["webapiDomain"] + "/Forum/Posts";
            var httpService = new HttpService();
            try
            {
                return httpService.Post(url, new { userId = userId, topic = topic, content = content }, token);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -500
                });
            }

        }

        [WebMethod]
        public static string AddReply(int postId , int userId, string content, string token)
        {
            var validator = new TokenValidator();
            var claims = validator.GetClaimPrincipal(token);

            if (validator.IsExpired(claims.Claims.FirstOrDefault(x => x.Type == "exp").Value))
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -7
                });
            }

            if (!validator.IsLevelOne(claims.Claims.FirstOrDefault(x => x.Type == "level").Value))
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -8
                });
            }


            var url = ConfigurationManager.AppSettings["webapiDomain"] + "/Forum/Reply";
            var httpService = new HttpService();
            try
            {
                return httpService.Post(url, new { postId = postId, userId = userId, content = content }, token);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -500
                });
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
    }
}