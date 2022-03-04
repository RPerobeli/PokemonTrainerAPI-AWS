using PokemonTrainerAPI.Domain;
using PokemonTrainerAPI.DTO;
using PokemonTrainerAPI.Map.Interfaces;
using PokemonTrainerAPI.Repository.Interfaces;
using PokemonTrainerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonTrainerAPI.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private IPokemonRepository pkRepository;
        private IMapper mapper;

        public UserService(IUserRepository _userRepository, IMapper _mapper, IPokemonRepository _pkRepository)
        {
            userRepository = _userRepository;
            mapper = _mapper;
            pkRepository = _pkRepository;
        }

        public async Task<bool> AdicionarPokemon(string nome, string email)
        {
            if (await VerificarExistenciaEmailNoBanco(email))
            {
                Pokemon pokemon = new Pokemon();
                pokemon.nome = nome;
                Usuario user = await userRepository.GetUserByEmail(email);
                isUserValido(user);
                pokemon.idTrainer = user.id;
                pkRepository.InserirPokemon(pokemon);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task AdicionarUsuario(UserDTO novoUser)
        {
            Usuario user = new Usuario();
            var isEmailValid = await VerificarExistenciaEmailNoBanco(novoUser.email);
            if (!isEmailValid)
            {
                user.SetEmail(novoUser.email);
                user.SetUsername(novoUser.username);
                await userRepository.InserirUser(user);
            }
        }
        public async Task<IList<UserDTO>> GetUserByUsername(string username)
        {
            IList<Usuario> user = await userRepository.FindByUsername(username);
            IList<UserDTO> userDto = mapper.Usuario2UserDTO(user);
            return userDto;
        }
        public async Task<IList<UserDTO>> ListarTreinadores()
        {
            IList<Usuario> lista = await userRepository.ListarTreinadores();
            IList<UserDTO> listaSaida = mapper.Usuario2UserDTO(lista);
            return listaSaida;
        }

        public void MudarNick(string novoNick, string email)
        {
            userRepository.MudarNick(email, novoNick);
        }
        public async Task<bool> VerificarExistenciaEmailNoBanco(string email)
        {
            Usuario user = await userRepository.GetUserByEmail(email);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void isUserValido(Usuario user)
        {
            if (user == null)
            {
                throw new Exception($"treinador não encontrado no banco de treinadores");
            }
        }
    }
}
