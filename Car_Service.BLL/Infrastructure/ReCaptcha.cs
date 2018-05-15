using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Car_Service.BLL.Infrastructure
{
    public class ReCaptcha
    {
        private static readonly string _sekretKey = "6LfTizUUAAAAAOH-_rnNKMXpi-iUzRLUjJ7adpzn";
        public static bool  Validate(string Response)
        {
            var captchaResponse = JsonConvert.DeserializeObject<ReCaptcha>(Response);

            return bool.Parse(captchaResponse.Success);
        }
        public static async Task<string> GetRespons(string captcha)
        {
            string responce;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.google.com");
                var values = new Dictionary<string, string>
                {
                    { "secret", _sekretKey },
                    { "response", captcha}
                }; 
                var content = new FormUrlEncodedContent(values);
                var result = await client.PostAsync("/recaptcha/api/siteverify", content).ConfigureAwait(false);
                responce = await result.Content.ReadAsStringAsync();
            }
            return responce;
        }

        [JsonProperty("success")]
        public string Success
        {
            get { return _success; }
            set { _success = value; }
        }

        private string _success;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return _errorCodes; }
            set { _errorCodes = value; }
        }
        private List<string> _errorCodes;
    }
}
