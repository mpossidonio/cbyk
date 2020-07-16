using NuvemVulcao.API.Models;
using NuvemVulcao.Domain.Entities;
using NuvemVulcao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuvemVulcao.API.Interface
{
    public interface IMapaService
    {
        public MapaVM CalculoDias(MapaVM mapaVM);
    }
}
