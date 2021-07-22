using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace PassionCentre.Services
{
    public class ReCaptcha
    {
        private readonly HttpClient captchaClient;
        private readonly IConfiguration _configuration;

        public ReCaptcha(HttpClient captchaClient, IConfiguration configuration)
        {
            this.captchaClient = captchaClient;
            _configuration = configuration;
        }

        public async Task<bool> IsValid(string captcha)
        {
            try
            {
                var secretKey = _configuration["ReCaptcha:SecretKey"];
                var postTask = await captchaClient.PostAsync($"?secret={secretKey}&response={captcha}", new StringContent(""));
                var result = await postTask.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(result);
                dynamic success = resultObject["success"];
                return (bool)success;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
