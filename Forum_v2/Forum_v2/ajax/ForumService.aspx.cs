using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forum_v2.ajax
{
    public partial class ForumService : System.Web.UI.Page
    {
        [WebMethod]
        public static string GetPostById(int id, int? index, int? size)
        {
            var url = "https://localhost:44337/api/Forum/Reply?" + $"postId={id}&pageIndex={index ?? 0}&pageSize={size ?? 10}";
            var httpService = new HttpService();
            return httpService.Get(url);
        }

        [WebMethod]
        public static string GetPosts(string topic, string nickname, int? index, int? size)
        {
            var url = "https://localhost:44337/api/Forum/Posts?" + $"keyTopic={topic}&keyNickname={nickname}&pageIndex={index ?? 0}&pageSize={size ?? 10}";
            var httpService = new HttpService();
            return httpService.Get(url);
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

            if (validator.IsLevelTwo(claims.Claims.FirstOrDefault(x => x.Type == "level").Value))
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -8
                });
            }


            var url = "https://localhost:44337/api/Forum/Posts";
            var httpService = new HttpService();
            return httpService.Post(url, new { userId = userId, topic = topic, content = content}, token);

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


            var url = "https://localhost:44337/api/Forum/Reply";
            var httpService = new HttpService();
            return httpService.Post(url, new {postId = postId,  userId = userId, content = content }, token);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
    }
}