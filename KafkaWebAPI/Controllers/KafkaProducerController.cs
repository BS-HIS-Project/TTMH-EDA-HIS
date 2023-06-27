using KafkaWebAPI.ViewModels;
using KafkaWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KafkaWebAPI.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class KafkaProducerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("HAHAHAHAH");
        }

        [HttpPost]
        public IActionResult KafkaProducer([FromBody] KafkaProducerViewModel value)
        {
            KafkaProducer controller = new KafkaProducer("server.nicklu89.com:9092");
            controller.Produce(value.Topic ?? "no-topic", value.Key ?? "no-key", value.Message ?? "no-message"); // topic, message
            return Ok();
        }

        [HttpPost]
        public IActionResult KafkaProducerDoctor([FromBody] KafkaProducerDoctorViewModel value)
        {
            KafkaProducer controller = new KafkaProducer("server.nicklu89.com:9092");
            controller.Produce(value.Topic ?? "no-topic", value.Key ?? "no-key", System.Text.Json.JsonSerializer.Serialize(value.Message) ?? "no-message"); // topic, message
            return Ok();
        }
    }
}
