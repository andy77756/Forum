using Forum_v2.ajax.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forum_v2.ajax
{
    public partial class AuthorizeService : System.Web.UI.Page
    {
        [WebMethod]
        public static string Login(string userName, string pwd)
        {
            var user = new LoginDto
            {
                userName = userName,
                pwd = pwd
            };
            var url = "https://localhost:44337/api/Authorize/Login";
            var httpService = new HttpService();
            return httpService.Post(url, user,"");
        }

        [WebMethod]
        public static string Register(string userName, string nickname, string pwd)
        {
            var user = new RegisterDto
            {
                userName = userName,
                nickname = nickname,
                pwd = pwd
            };
            var url = "https://localhost:44337/api/Authorize/Register";
            var httpService = new HttpService();
            return httpService.Post(url, user, "");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}