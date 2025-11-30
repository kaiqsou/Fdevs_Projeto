using DrawHub.Data;
using DrawHub.Models;

namespace DrawHub.Seeds
{
    public static class CategoriaSeeder
    {
        public static void SeedCategorias(BancoContext context)
        {
            var categorias = new List<string>
            {
                "Animangá",
                "Horror",
                "Natureza",
                "Realista",
                "Retrato",
                "Abstrato",
                "Minimalista",
                "Colorido",
                "Preto e Branco"
            };

            foreach (var nome in categorias)
            {
                if (!context.Categorias.Any(c => c.Nome == nome))
                {
                    var categoria = new Categoria
                    {
                        Nome = nome,
                        DataCriacao = DateTime.Now
                    };

                    context.Categorias.Add(categoria);
                }
            }

            context.SaveChanges();
        }
    }
}