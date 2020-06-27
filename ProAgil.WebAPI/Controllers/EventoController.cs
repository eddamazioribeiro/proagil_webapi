using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProAgil.Repository;
using ProAgil.Domain;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _repo.GetAllEventosAsync(true);
                
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }            
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> GetById(int EventoId)
        {
            try
            {
                var result = await _repo.GetEventoAsyncById(EventoId, true);
                
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }              
        }

        [HttpGet("{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var result = await _repo.GetAllEventosAsyncByTema(tema, true);
                
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }              
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvento(Evento evento)
        {
            try
            {
                _repo.Add(evento);

                if(await _repo.SaveChangesAsync())
                {
                    // returns the newly created event calling the GetById method
                    return Created($"/api/evento/{evento.Id}", evento);
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvento(int eventoId, Evento evento)
        {
            try
            {
                var eventoAux = await _repo.GetEventoAsyncById(eventoId, false);
                
                if(eventoAux == null)
                {
                    return NotFound();
                }
                
                _repo.Update(evento);

                if(await _repo.SaveChangesAsync())
                {
                    // returns the update event calling the GetById method
                    return Created($"/api/evento/{evento.Id}", evento);
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvento(int eventoId)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(eventoId, false);
                
                if(evento == null)
                {
                    return NotFound();
                }
                
                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }               
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }

            return BadRequest();          
        }                          
    }
}
