using System;
using System.Collections.Generic;
using System.Text;

namespace NuvemVulcao.Domain.Entities
{
    public class Mapa : Entity
    {
        public IEnumerable<NuvemCinza> NuvemCinzas { get; set; }

        public IEnumerable<Aeroporto> Aeroportos { get; set; }

    }
}
