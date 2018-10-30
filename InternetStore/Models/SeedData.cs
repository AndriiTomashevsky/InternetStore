using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System;
using InternetStore.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace InternetStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider services)
        {
            using (var serviceScope = services.CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();

                if (!context.Products.Any())
                {
                    Category category1 = new Category() { Name = "Circular duct fans" };
                    Category category2 = new Category() { Name = "Axial fans" };
                    Category category3 = new Category() { Name = "Explosion proof fans" };
                    Category category4 = new Category() { Name = "Smoke extract fans" };

                    for (int i = 1; i <= 25; i++)
                    {
                        context.Products.Add(
                            new Product
                            {
                                Name = $"Product{i}",
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod",
                                Category = category1,
                                Price = Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10,
                                Airflow = Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10,
                                Power = (int)Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10
                            }
                        );
                    }
                    context.Products.AddRange(
                    new Product
                    {
                        Name = $"Product1",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod",
                        Category = category2,
                        Price = Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10,
                        Airflow = Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10,
                        Power = (int)Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10
                    },
                    new Product
                    {
                        Name = $"Product1",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod",
                        Category = category3,
                        Price = Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10,
                        Airflow = Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10,
                        Power = (int)Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10
                    }
,
                    new Product
                    {
                        Name = $"Product1",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod",
                        Category = category4,
                        Price = Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10,
                        Airflow = Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10,
                        Power = (int)Math.Round((decimal)(new Random().Next(200, 500) / 10)) * 10
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
