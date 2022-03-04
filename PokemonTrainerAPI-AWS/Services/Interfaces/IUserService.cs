using PokemonTrainerAPI.Domain;
using PokemonTrainerAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonTrainerAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task AdicionarUsuario(UserDTO novoUser);
        void MudarNick(string novoNick, string email);
        Task<IList<UserDTO>> ListarTreinadores();
        Task<IList<UserDTO>> GetUserByUsername(string username);
        Task<bool> VerificarExistenciaEmailNoBanco(string email);
    }
}
