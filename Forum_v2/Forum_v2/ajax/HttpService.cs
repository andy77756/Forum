using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace Forum_v2.ajax
{
    public class HttpService
    {
        /// <summary>
        /// Get方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Get(string url)
        {
            var result = "";
            var request = HttpWebRequest.Create(url);
            request.Method = "Get";
            request.Timeout = 30000;

            using (var response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }

            return result;
        }

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string Post(string url, object postData, string token)
        {
            var result = "";
            var request = WebRequest.Create(url);
            request.Method = "Post";
            request.ContentType = "application/json; charset=utf-8";

            if (!String.IsNullOrEmpty(token))
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
            }
            //將需 post 的資料內容轉為 stream 
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(postData);
                streamWriter.Write(json);
                streamWriter.Flush();
            }
            //使用 GetResponse 方法將 request 送出，如果不是用 using 包覆，請記得手動 close WebResponse 物件，避免連線持續被佔用而無法送出新的 request
            using (var httpResponse = request.GetResponse())
            //使用 GetResponseStream 方法從 server 回應中取得資料，stream 必需被關閉
            //使用 stream.close 就可以直接關閉 WebResponse 及 stream，但同時使用 using 或是關閉兩者並不會造成錯誤，養成習慣遇到其他情境時就比較不會出錯
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }
    }
}