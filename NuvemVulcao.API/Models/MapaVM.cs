using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NuvemVulcao.API.Models
{
    public class MapaVM
    {
        public MapaVM()
        {
        }
        public MapaVM(int aeroportos, int nuvens, int linhas, int colunas, List<char[,]> grids)
        {
            Aeroportos = aeroportos;
            Nuvens = nuvens;
            Linhas = linhas;
            Colunas = colunas;
            Grids = grids;
        }

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [Range(3, int.MaxValue, ErrorMessage = "A quantidade minima de {0} deve ser {1}.")]
        public int Aeroportos { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(4, int.MaxValue, ErrorMessage = "A quantidade minima de {0} deve ser {1}.")]
        public int Nuvens { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(10, int.MaxValue, ErrorMessage = "A quantidade minima de {0} deve ser {1}.")]
        public int Linhas { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(10, int.MaxValue, ErrorMessage = "A quantidade minima de {0} deve ser {1}.")]
        public int Colunas { get; set; }
        
        public int Dias1Aeroporto { get; set; }
        public int DiasTodosAeroportos { get; set; }
        public List<char[,]> Grids { get; set; }
    }
}
