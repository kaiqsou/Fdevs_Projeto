using DrawHub.Data;
using DrawHub.Enums;
using DrawHub.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace DrawHub.Seeds
{
    public static class AdminSeeder
    {
        public static void SeedAdmin(BancoContext context)
        {
            const string adminEmail = "admin@drawhub.com";

            if (!context.Usuarios.Any(u => u.Email == adminEmail))
            {
                var admin = new Usuario
                {
                    Nome = "Administrador",
                    Email = adminEmail,
                    Senha = "123456",
                    Tipo = RoleEnum.Administrador
                };

                context.Usuarios.Add(admin);
                context.SaveChanges();

                Console.WriteLine("Usuário admin criado.");
            }
            else
            {
                Console.WriteLine("Admin já existe!");
            }
        }
    }
}
