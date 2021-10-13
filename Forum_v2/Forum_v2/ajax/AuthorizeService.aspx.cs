using Forum_v2.ajax.Dtos;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Web.Services;
using System.Web.UI;


namespace Forum_v2.ajax
{
    /// <summary>
    /// 註冊、登入webmethod
    /// </summary>
    public partial class AuthorizeService : Page
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="userName">使用者帳號</param>
        /// <param name="pwd">密碼</param>
        /// <returns>序列化物件{statusCode:0, returnData:{}}</returns>
        [WebMethod]
        public static string Login(string userName, string pwd)
        {
            var user = new LoginDto
            {
                userName = userName,
                pwd = pwd
            };
            var url = ConfigurationManager.AppSettings["wabapiDomain"] + "/Authorize/Login";


            try
            {
                var httpService = new HttpService();            
                return httpService.Post(url, user, "");

            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    statusCode = -500
                });
            }
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="userName">使用者帳號</param>
        /// <param name="nickname">使用者暱稱</param>
        /// <param name="pwd">密碼</param>
        /// <returns>序列化物件{statusCode:0, returnData:{}}</returns>
        [WebMethod]
        public static string Register(string userName, string nickname, string pwd)
        {
            var user = new RegisterDto
            {
                userName = userName,
                nickname = nickname,
                pwd = pwd
            };
            var url = ConfigurationManager.AppSettings["webapiDomain"] + "/Authorize/Register";

            try
            {
                var httpService = new HttpService();
                return httpService.Post(url, user, "");
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