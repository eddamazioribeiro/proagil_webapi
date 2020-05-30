using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private static readonly Evento[] Eventos =  {
                new Evento() {
                    EventoId = 1,
                    Tema = "Angular e .NET Core",
                    Local = "Ribeirão Preto",
                    Lote = "1º Lote",
                    QtdPessoas = 250,
                    DataEvento = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy")
                },
                new Evento() {
                    EventoId = 2,
                    Tema = "Node.js",
                    Local = "Belo Horizonte",
                    Lote = "1º Lote",
                    QtdPessoas = 150,
                    DataEvento = DateTime.Now.AddDays(11).ToString("dd/MM/yyyy")
                } 
        };

        private readonly ILogger<EventoController> _logger;

        public EventoController(ILogger<EventoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return Eventos.ToList();
        }

        [HttpGet("{id}")]
        public Evento GetEventoById(string id)
        {
            int.TryParse(id, out int EventoId);
            return Eventos.FirstOrDefault(x => x.EventoId == EventoId);
        } 
    }
}
