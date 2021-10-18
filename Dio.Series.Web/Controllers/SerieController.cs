﻿using DIO.Series;
using DIO.Series.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dio.Series.Web.Controllers
{
    [Route("[controller]")]
    public class SerieController : Controller
    {

        private readonly IRepositorio<Serie> _repositorioSerie;

        public SerieController(IRepositorio<Serie> repositorioSerie) // injeção de dependencias - tiramos do controller a responsabilidade de conhecer a classe concreta.
        {
            _repositorioSerie = repositorioSerie;
        }



        [HttpGet("")]
        public IActionResult Lista()
        {
            return Ok(_repositorioSerie.Lista().Select(s => new SerieModel(s))); //para cada serieModel ele retornará uma lista.
           
        }


        [HttpPut("{id}")]
        public IActionResult Atualiza(int id, [FromBody] SerieModel model)
        {
            _repositorioSerie.Atualiza(id, model.ToSerie());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Exclui(int id)
        {
            _repositorioSerie.Exclui(id);
            return NoContent();
        }

        [HttpPost("{id}")]
        public IActionResult Insere([FromBody] SerieModel model)
        {
            model.Id = _repositorioSerie.ProximoId();

            Serie serie = model.ToSerie();

            _repositorioSerie.Insere(serie);
            return Created("", serie);
        }

        [HttpGet("{id}")]
        public IActionResult Consulta(int id)
        {
            return Ok(new SerieModel(_repositorioSerie.Lista().FirstOrDefault(s => s.Id == id)));
        }
    }

    /*
     * GET = Retorna algo
     * POST = Inserir algo
     * PUT = Altera algo
     * DELETE = Excluir algo 
     * 
     */
}
