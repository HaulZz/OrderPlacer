using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using OrderPlacer.Models;
using OrderPlacer.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
//using System.Net.Http.Json;

namespace OrderPlacer.Services
{
    public static class SendOrderApiService
    {

        public static HttpClient Client { get; set; }

        public static void Init(){
            Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:5000/");
            Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void Send(Order order)
        {
            //var message = System.Text.Json.JsonSerializer.Serialize(order);
            //var buffer = Encoding.UTF8.GetBytes(message);
            //var byteContent = new ByteArrayContent(buffer);
            var result = Client.PostAsJsonAsync("order", order).Result;
            //Console.WriteLine( byteContent);
            //return result.ToString();
        }

        public static async Task<Order> Get(int id)
        {
            string url = $"order/{id}";
            using (HttpResponseMessage response = await SendOrderApiService.Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(data);

                    Order order = JsonSerializer.Deserialize<Order>(data);
                    Console.WriteLine(order.Id);
                    return order;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
    