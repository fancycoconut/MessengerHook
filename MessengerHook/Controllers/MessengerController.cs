using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessengerHook.Models;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace MessengerHook.Controllers
{
    [Route("api/messenger")]
    public class MessengerController : Controller
    {
        private static HttpClient _client = new HttpClient();

        [HttpGet]
        [Route("receive")]
        public IActionResult Receive()
        {
            var query = HttpContext.Request.Query;
            if (query["hub.mode"] == "subscribe" &&
                query["hub.verify_token"] == "token...")
            {
                var returnValue = query["hub.challenge"];
                return Json(int.Parse(returnValue));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("receive")]
        public async Task<IActionResult> Receive(MessengerCallbackModel model)
        {
            await Task.Factory.StartNew(async () =>
            {
                foreach (var entry in model.Entry)
                {
                    foreach (var message in entry.Messaging)
                    {
                        if (String.IsNullOrEmpty(message?.Message.Text)) continue;

                        var msg = "You said: " + message.Message.Text;
                        var json = $@" {{recipient: {{ id: {message.Sender.Id}}},message: {{text: ""{msg}"" }}}}";
                        await PostRaw("https://graph.facebook.com/v2.6/me/messages?access_token=YOUR_TOKEN", json);
                    }
                }
            });

            return Ok();
        }

        private async Task<string> PostRaw(string url, string data)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, stringContent);

            return await response.Content.ReadAsStringAsync();
        }
    
    }
}
