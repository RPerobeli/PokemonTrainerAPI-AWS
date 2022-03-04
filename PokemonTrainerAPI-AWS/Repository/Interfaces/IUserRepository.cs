using PokemonTrainerAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonTrainerAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task InserirUser(Usuario user);
        Task<IList<Usuario>> ListarTreinadores();
        Task MudarNick(string email, string NovoNick);
        void DeleteAll();
        Task<IList<Usuario>> FindByUsername(string username);
        Task<Usuario> GetUserByEmail(string email);
    }
}
