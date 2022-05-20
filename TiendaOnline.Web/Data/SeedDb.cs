using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaOnline.Web.Data;
using TiendaOnline.Web.Data.Entities;
using TiendaOnline.Web.Enums;
using TiendaOnline.Web.Helpers;
using TiendaOnline.Web.Models;

public class SeedDb
{
    private readonly ApplicationDbContext _context;
    private readonly IUserHelper _userHelper;

    public SeedDb(ApplicationDbContext context, IUserHelper userHelper)
    {
        _context = context;
        _userHelper = userHelper;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountriesAsync();

        await CheckRolesAsync();
        await CheckUserAsync("1", "Orlando", "A", "oralpez@hotmail.com", "3000000000", "Calle Luna Calle Sol", UserType.Admin);

    }
    private async Task CheckRolesAsync()
    {
        await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
        await _userHelper.CheckRoleAsync(UserType.User.ToString());
    }

    private async Task<User> CheckUserAsync(
        string document,
        string firstName,
        string lastName,
        string email,
        string phone,
        string address,
        UserType userType)
    {
        User user = await _userHelper.GetUserAsync(email);
        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                City = _context.Cities.FirstOrDefault(),
                UserType = userType
            };

            await _userHelper.AddUserAsync(user, "123456"); //password debe tener una longitud de 6 caracteres
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }


    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            _context.Countries.Add(new Country
            {
                Name = "Colombia",
                Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "Antioquia",
                        Cities = new List<City>
                        {
                            new City { Name = "Medellín" },
                            new City { Name = "Envigado" },
                            new City { Name = "Itagüí" }
                        }
                    },
                    new Department
                    {
                        Name = "Cundinamarca",
                        Cities = new List<City>
                        {
                            new City { Name = "Bogotá" }
                        }
                    },
                    new Department
                    {
                        Name = "Nariño",
                        Cities = new List<City>
                        {
                            new City { Name = "Pasto" },
                            new City { Name = "Ipiales" },
                            new City { Name = "Pupiales" }
                        }
                    }
                }
            });
            _context.Countries.Add(new Country
            {
                Name = "Argentina",
                Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "Buenos Aires",
                        Cities = new List<City>
                        {
                            new City { Name = "Mar Del Plata" },
                            new City { Name = "Quilmes" },
                            new City { Name = "Lanús" }
                        }
                    },
                    new Department
                    {
                        Name = "Mendoza",
                        Cities = new List<City>
                        {
                            new City { Name = "Godoy Cruz" },
                            new City { Name = "Las Heras" }
                        }
                    }
                }
            });
            await _context.SaveChangesAsync();
        }


    }
}