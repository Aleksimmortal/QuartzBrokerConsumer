using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BrokerMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        //// GET: api/<BrokerController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<BrokerController>/5
        [HttpGet]
        public string Get()
        {
            return Message.GetMessage();
        }

        // POST api/<BrokerController>
        [HttpPost]
        public void Push([FromBody] BrokerMessage value)
        {
            Console.WriteLine($"Get message {value.Message}");
            Message.PushMessage(value.Message);
        }


        // DELETE api/<BrokerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
