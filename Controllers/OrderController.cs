using Microsoft.AspNetCore.Mvc;
using OrderPlacer.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderPlacer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        // POST api/<Class>
        [HttpPost]
        public string Post(Order order)
        {
            //enviar o pedido para a outra aplicação por api
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));


            var message = System.Text.Json.JsonSerializer.Serialize(order);
            var buffer = Encoding.UTF8.GetBytes(message);
            var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("order", byteContent).Result;

            return result.ToString();
        }
    }
}
