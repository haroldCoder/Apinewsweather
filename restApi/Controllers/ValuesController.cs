using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using restApi.Context;
using restApi.Models;

namespace restApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ConectionSql context;
        public ValuesController(ConectionSql context)
        {
            this.context = context;
        }
        // GET api/values
        public class News
        {
            public string Status { get; set; }
            public int TotalResults { get; set; }
            public Article[] Articles { get; set; }
        }
        public class Article
        {
            public Source Source { get; set; }
            public string author { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string urlToImage { get; set; }
            public DateTime oublishedAt { get; set; }
        }
        public class Source
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public async Task<object> getWeather(string city)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var _httpClient = httpClient;
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "restApi");
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=a8d56ad88b9efcd3f6da2a86517746ed";
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var weather = JsonConvert.DeserializeObject(json);
                return weather;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<News> getNews(string city)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "restApi");
            var response = await _httpClient.GetAsync($"https://newsapi.org/v2/everything?q={city}&language=en&apiKey=168d3e62b0474fc8a2fe46655dbe2101");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<News>(json);
        }
        [HttpGet]
        public async Task<IActionResult> Get(string city)
        {
            var news = await getNews(city);
            var weather = await getWeather(city);
            var data = new { news, weather };
            return Ok(data);
        }
        [HttpGet("history")]
        public IEnumerable<History> Get()
        {
            return context.History.ToList();
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] History history)
        {
            try
            {
                context.History.Add(history);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
