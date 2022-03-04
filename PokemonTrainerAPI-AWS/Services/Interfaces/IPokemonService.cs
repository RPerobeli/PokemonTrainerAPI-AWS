using PokemonTrainerAPI.Domain;
using PokemonTrainerAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonTrainerAPI.Services.Interfaces
{
    public interface IPokemonService
    {
        Task AdicionarPokemon(string nome, string email);
        Task<IList<PokemonOutDTO>> ListarPokemonsDoUser(string email);
        Task<IList<PokemonOutDetailedDTO>> ListarPokemonsDoUserDetalhado(string email);
        Task<PokemonOutFullDTO> PokemonDetalhado(string nome);
    }
}
