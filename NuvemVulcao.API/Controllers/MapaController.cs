using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuvemVulcao.API.Interface;
using NuvemVulcao.API.Models;
using NuvemVulcao.Domain.Entities;
using NuvemVulcao.Domain.Repositories;

namespace NuvemVulcao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapaController : ControllerBase
    {
        private readonly IMapaService _mapaService;

        public MapaController(IMapaService mapaService)
        {
            _mapaService = mapaService;
        }


        [HttpPost]
        public IActionResult CalculaDias([FromBody] MapaVM mapaVM)
        {
            if (ModelState.IsValid)
            {
                return Ok(_mapaService.CalculoDias(mapaVM));
            }

            //retorna totais
            return BadRequest(ModelState);
        }

    }
}
