using NuvemVulcao.API.Interface;
using NuvemVulcao.API.Models;
using NuvemVulcao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;

namespace NuvemVulcao.API.Services
{
    public class MapaService : IMapaService
    {
        public MapaVM CalculoDias(MapaVM mapaVM)
        {
            int dias = 1;
            int qtdAeroportosVisiveis = mapaVM.Aeroportos;
            int dias1Aeroporto = 0;
            int diasTodosAeroportos = 0;
            char[,] grid = new char[mapaVM.Linhas, mapaVM.Colunas];
            bool flAeroportoVisivel = false;
            Mapa mapa = CriaMapa(mapaVM);

            MapaVM retornoGrid = new MapaVM(mapa.Aeroportos.Count(), mapa.NuvemCinzas.Count(), mapaVM.Linhas, mapaVM.Colunas, new List<char[,]>());

            // preenche grid com informações
            foreach (Aeroporto aeroporto in mapa.Aeroportos)
                grid[aeroporto.Linha, aeroporto.Coluna] = 'A';
            foreach (NuvemCinza nuvemCinza in mapa.NuvemCinzas)
                grid[nuvemCinza.Linha, nuvemCinza.Coluna] = '*';

            //Calcula variação dos dias
            while (qtdAeroportosVisiveis > 0)
            {
                flAeroportoVisivel = false;
                // Analisa Aeroportos
                foreach (Aeroporto aeroporto in mapa.Aeroportos)
                {
                    if (grid[aeroporto.Linha, aeroporto.Coluna].Equals('A'))
                    {
                        bool flNuvem = false;
                        if (aeroporto.Linha > 0) if (grid[aeroporto.Linha - 1, aeroporto.Coluna].Equals('*')) flNuvem = true;
                        if (aeroporto.Coluna > 0) if (grid[aeroporto.Linha, aeroporto.Coluna - 1].Equals('*')) flNuvem = true;
                        if (aeroporto.Coluna + 1 < mapaVM.Colunas) if (grid[aeroporto.Linha, aeroporto.Coluna + 1].Equals('*')) flNuvem = true;
                        if (aeroporto.Linha + 1 < mapaVM.Linhas) if (grid[aeroporto.Linha + 1, aeroporto.Coluna].Equals('*')) flNuvem = true;
                        if (flNuvem)
                        {
                            dias1Aeroporto = (dias1Aeroporto == 0 || dias1Aeroporto > dias + 1) ? dias + 1 : dias1Aeroporto;
                            diasTodosAeroportos = (diasTodosAeroportos == 0 || diasTodosAeroportos < dias + 1) ? dias + 1 : diasTodosAeroportos;
                        }
                        flAeroportoVisivel = true;
                    }
                }

                // Amplia nuvens
                retornoGrid.Grids.Add((char[,])grid.Clone());

                char[,] gridClone = (char[,])grid.Clone();
                for (int i = 0; i < mapaVM.Linhas; i++)
                    for (int j = 0; j < mapaVM.Colunas; j++)
                    {
                        if (gridClone[i, j].Equals('*'))
                        {
                            if (i > 0)
                            {
                                if (grid[i - 1, j].Equals('A')) qtdAeroportosVisiveis--;
                                grid[i - 1, j] = '*';
                            }
                            if (i + 1 < mapaVM.Linhas)
                            {
                                if (grid[i + 1, j].Equals('A')) qtdAeroportosVisiveis--;
                                grid[i + 1, j] = '*';
                            }
                            if (j > 0)
                            {
                                if (grid[i, j - 1].Equals('A')) qtdAeroportosVisiveis--;
                                grid[i, j - 1] = '*';
                            }
                            if (j + 1 < mapaVM.Colunas)
                            {
                                if (grid[i, j + 1].Equals('A')) qtdAeroportosVisiveis--;
                                grid[i, j + 1] = '*';
                            }
                        }
                    }

                if (!flAeroportoVisivel || qtdAeroportosVisiveis==0)
                {
                    retornoGrid.Grids.Add((char[,])grid.Clone());
                    break;
                }


                dias++;
            }
            retornoGrid.Dias1Aeroporto = dias1Aeroporto;
            retornoGrid.DiasTodosAeroportos = diasTodosAeroportos;

            return retornoGrid;
        }

        private Mapa CriaMapa(MapaVM mapaVM)
        {
            int Linha = 0;
            int Coluna = 0;
            Mapa mapa = new Mapa();
            Random random = new Random();
            List<Aeroporto> aeroportos = new List<Aeroporto>();
            List<NuvemCinza> nuvemCinzas = new List<NuvemCinza>();


            for (int i = 0; i < mapaVM.Aeroportos; i++)
            {
                do
                {
                    Linha = random.Next(mapaVM.Linhas - 1);
                    Coluna = random.Next(mapaVM.Colunas - 1);
                } while (aeroportos.Where(x => x.Linha == Linha && x.Coluna == Coluna).Any());
                aeroportos.Add(new Aeroporto { Linha = Linha, Coluna = Coluna });
            }

            for (int i = 0; i < mapaVM.Nuvens; i++)
            {
                do
                {
                    Linha = random.Next(mapaVM.Linhas);
                    Coluna = random.Next(mapaVM.Colunas);
                } while (nuvemCinzas.Where(x => x.Linha == Linha && x.Coluna == Coluna).Any() ||
                         aeroportos.Where(x => x.Linha == Linha && x.Coluna == Coluna).Any());
                nuvemCinzas.Add(new NuvemCinza { Linha = Linha, Coluna = Coluna });
            }

            return new Mapa { Aeroportos = aeroportos, NuvemCinzas = nuvemCinzas, Linha = mapaVM.Linhas, Coluna = mapaVM.Colunas };

        }
    }
}
