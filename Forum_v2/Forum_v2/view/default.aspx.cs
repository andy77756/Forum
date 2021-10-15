using Newtonsoft.Json;
using System;
using System.IO;
using System.Web.UI;

namespace Forum_v2.view
{
    public partial class _default : Page
    {
        //目前版本號
        public string CurrentVersion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //取得Config.json版本號
            StreamReader reader = new StreamReader("./Config.json");
            string jsonString = reader.ReadToEnd();
            var config = JsonConvert.DeserializeObject<Config>(jsonString);
            this.CurrentVersion = config.CurrentVersion;
        }
    }
}