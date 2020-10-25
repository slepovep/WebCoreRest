using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCoreRest.Controllers
{
	[Route("api/RedirectGet")]
	[ApiController]
	public class RedirectGetController : ControllerBase
	{
		static string _address = "https://geocode-maps.yandex.ru/1.x/?apikey=8a29c8bd-2c00-4294-8501-90f663b785a4&geocode=%D0%A0%D0%BE%D1%81%D1%81%D0%B8%D1%8F%20%D0%9A%D1%80%D0%B0%D1%81%D0%BD%D0%BE%D0%B4%D0%B0%D1%80%D1%81%D0%BA%D0%B8%D0%B9%20%D0%9A%D1%80%D0%B0%D0%B9%20%D0%9A%D1%80%D0%B0%D1%81%D0%BD%D0%BE%D0%B4%D0%B0%D1%80%20%D0%93%D0%BE%D1%80%D0%BE%D0%B4%20%D0%B8%D0%BC.%20%D0%9B%D0%B5%D0%BD%D0%B8%D0%BD%D0%B0%20%D0%A3%D0%BB%D0%B8%D1%86%D0%B0%20%2092";

		// GET: api/<RedirectGetController>
		/*[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}*/

		// GET api/<RedirectGetController>/5
		[HttpGet]
		public string Get()
		{
			//находим входящий GET-запрос
			string query = this.Request.QueryString.ToString();
			Task <string> content;

			if (query == null | query.Length == 1)
			{
				return "Не введен адреса запроса";
			}

			//отсекаем первый ?
			string address_url = query.Substring(1);
			HttpClient client = new HttpClient();

			try
			{
				content = client.GetStringAsync(address_url);
				return content.Result;  
			}
			catch 
			{
				return "Ошибка запроса" + "\n" + address_url;
			};

		}

		// POST api/<RedirectGetController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<RedirectGetController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<RedirectGetController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

	}
}
