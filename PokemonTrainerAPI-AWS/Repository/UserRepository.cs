using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonTrainerAPI.Domain;
using PokemonTrainerAPI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonTrainerAPI.Repository
{
    public class UserRepository : IUserRepository 
    {
        private PkTrainerContext contexto;
        public UserRepository(PkTrainerContext context)
        {
            contexto  = context;
        }
        public async Task InserirUser(Usuario user)
        {
            await contexto.user.AddAsync(user);
            Commit();
        }

        private void Commit()
        {
            contexto.SaveChanges();
        }

        public async Task MudarNick(string email, string novoNick)
        {
            Usuario user = await GetUserByEmail(email);
            user.SetUsername(novoNick);
            Commit();            
        }
        public void DeleteAll()
        {
            var users = from u in contexto.user select u;
            foreach( var user in users)
            {
                contexto.user.Remove(user);
            }
            Commit();
        }

        public async Task<IList<Usuario>> ListarTreinadores()
        {
            var lista = contexto.user.ToList() ;
            if(lista.Count == 0)
            {
                throw new Exception("Não há treinadores cadastrados");
            }
            return lista;
        }
        public async Task<IList<Usuario>> FindByUsername(string username)
        {
            // IList<Usuario> userDesejado = contexto.user.Where(w => w.username == username).ToList();
            IList<Usuario> userDesejado = await contexto.user.Where(w => w.username == username).ToListAsync();
            if (userDesejado.Count == 0)
            {
                throw new Exception($"Treinador {username} inexistente");
            }
            return userDesejado;
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            Usuario user = await contexto.user.FirstOrDefaultAsync(u => u.email == email);
            return user;
        }
    }
}
