using Microsoft.AspNetCore.Identity;
using TaskManagementApplication.Data;
using TaskManagementDomain.Entity;
using TaskManagementDomain.Helper;

namespace TaskManagementSystemApi.Seeds
{
    public class SeedDatas
    {
        public async static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var createScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = createScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                var userManager = createScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var userRole = createScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await userRole.RoleExistsAsync(UserRoles.CustomerRole))
                {
                    await userRole.CreateAsync(new IdentityRole(UserRoles.CustomerRole));
                }

                if (!context.Users.Any())
                {
                    AppUser user = new AppUser();
                    context.Users.AddRange(new List<AppUser>
                    {
                         new AppUser()
                        {
                            Id = "2d16c229-9d00-ce9b-5a0d-d7f47513a9f3",
                            UserName = "Vic123",
                            Email = "vic@gmail.com",
                            PasswordHash = "Vic12345@",
                            FullName = "Victory Lem",
                            PhoneNumber = "1234567890",
                           
                        }
                    });
                  await context.SaveChangesAsync();
                };
            
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(new List<Project>()
                    {
                        new Project()
                        {
                         Id = Guid.NewGuid(),
                         Name= "Writing",
                         Description = "to write"
                        },
                        new Project()
                        {
                         Id = Guid.NewGuid(),
                         Name= "Singing",
                         Description = "to write",
                     }
                    });
                    await context.SaveChangesAsync();
                }

                if (!context.MyTasks.Any())
                {
                    context.MyTasks.AddRange(new List<MyTask>()
                    {
                        new MyTask()
                        {
                         Id = Guid.NewGuid(),
                         Title= "Writing",
                         Description = "to write",
                         Priority = "high",
                         Status = "ongoing",
                         DueDate = new DateTime(2023, 09, 25, 17, 32, 18, 204),
                         UserId = context.Users.First(b => b.Email == "vic@gmail.com").Id,
                          ProjectId = context.Projects.First(b => b.Name == "Singing").Id,
                        },

                         new MyTask()
                        {
                         Id = Guid.NewGuid(),
                         Title= "Singing",
                         Description = "to write",
                         Priority = "Low",
                         Status = "ongoing",
                         DueDate = new DateTime(2023, 09, 29, 17, 32, 18, 204),
                         UserId = context.Users.First(b => b.Email == "vic@gmail.com").Id,
                         ProjectId = context.Projects.First(b => b.Name == "Singing").Id,
                        }
                    });

                    await context.SaveChangesAsync();
                }

                if (!context.Notifications.Any())
                {
                    context.Notifications.AddRange(new List<Notification>()
                    {
                        new Notification()
                        {
                         Id = Guid.NewGuid(),
                         Type= "Writing",
                         Message = "to write",
                         Timestamp = "high",
                         Status = "not read",
                         UserId = context.Users.First(b => b.Email == "vic@gmail.com").Id
                        },
                        new Notification()
                        {
                         Id = Guid.NewGuid(),
                         Type= "Writing",
                         Message = "to write",
                         Timestamp = "high",
                         Status = "read",
                         UserId = context.Users.First(b => b.Email == "vic@gmail.com").Id
                        }
                    });
                      await context.SaveChangesAsync();
                }
            }  
        }
    }
}
