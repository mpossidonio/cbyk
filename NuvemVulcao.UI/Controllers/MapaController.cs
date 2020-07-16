using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuvemVulcao.API.Models;
using NuvemVulcao.Domain.Entities;
using NuvemVulcao.Domain.Repositories;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;

namespace NuvemVulcao.UI.Controllers
{
    public class MapaController : Controller
    {
        private readonly IMapaRepository _mapaRepository;

        public MapaController(IMapaRepository mapaRepository)
        {
            _mapaRepository = mapaRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MapaGrid(object obj)
        {
            return View(obj);
        }

        [HttpPost]
        public IActionResult MapaGrid([FromForm] MapaVM vm)
        {
            if (ModelState.IsValid)
            {
                HttpClient c = new HttpClient();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44308/api/Mapa");
                var reqBody = JsonConvert.SerializeObject(vm);
                req.Content = new StringContent(reqBody, Encoding.UTF8, "application/json");
                var resp = c.SendAsync(req).Result;

                var x = resp.Content.ReadAsStringAsync().Result;
                MapaVM obj = JsonConvert.DeserializeObject<MapaVM>(x);
                return View("MapaGrid", obj);
            }
            //return View(vm);

            return BadRequest();
        }

    }
}

