﻿using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioUsuarios
    {
        Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(Usuario usuario);
    }

    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly string connectionString;
        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Método para crear usuario
        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Usuarios (Email,EmailNormalizado,PasswordHash)
                                                        VALUES(@Email,@EmailNormalizado,@PasswordHash); 
                                                        SELECT SCOPE_IDENTITY();", usuario);
            return id;
        }

        //Método para buscar un usuario mediante email
        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QuerySingleOrDefaultAsync<Usuario>("SELECT * FROM Usuarios Where EmailNormalizado = @emailNormalizado", new { emailNormalizado });
        }
    }
}
