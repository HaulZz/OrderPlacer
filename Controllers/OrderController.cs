using Microsoft.AspNetCore.Mvc;
using OrderPlacer.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using OrderPlacer.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderPlacer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        // POST api/<Class>
        [HttpPost]
        public IActionResult Post(Order order)
        {
            //enviar o pedido para a outra aplicação por api
            SendOrderApiService.Send(order);
            return Accepted(order);
        }

        // Get api/<Class>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            return await SendOrderApiService.Get(id);
        }
    }
}
