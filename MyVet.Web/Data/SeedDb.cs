using MyVet.Web.Data.Entities;
using MyVet.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Data
{
    public class SeedDb
    {
        //Dependency Injection
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }

        //metodo que alimenta la base de datos
        public async Task SeedAsync()
        {
            //el await es para tirar el servicio secuencialmente por diferentes nucleos del procesador
            //asi forzo menos la maquina
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1010", "Raudo", "Moquete", "sanchezz1ero@gmail.com", "8293056303", "m6 #19, El Brisal", "Admin");
            var customer = await CheckUserAsync("2020", "Raudo", "Moquete", "sanchezz09@hotmail.com", "8293056303", "m6 #19, El Brisal", "Customer");
            await CheckPetTypesAsync();
            await CheckServiceTypesAsync();
            await CheckOwnerAsync(customer);
            await CheckManagerAsync(manager);
            await CheckPetsAsync();
            await CheckAgendasAsync();
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task<User> CheckUserAsync(
            string document,
            string fisrtName,
            string lastName,
            string email,
            string phone,
            string Address,
            string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if(user == null)
            {
                user = new User
                {
                    FirstName = fisrtName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = Address,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }
        private async Task CheckPetsAsync()
        {
            var owner = _dataContext.Owners.FirstOrDefault();
            var petType = _dataContext.PetTypes.FirstOrDefault();
            if (!_dataContext.Pets.Any())
            {
                AddPet("Profeta", owner, petType, "Pitbull");
                AddPet("Killer", owner, petType, "Doberman");

                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckServiceTypesAsync()
        {
            if (!_dataContext.ServiceTypes.Any())
            {
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Consulta" });
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Urgencia" });
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Vacuna" });
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckPetTypesAsync()
        {
            if (!_dataContext.PetTypes.Any())
            {
                _dataContext.PetTypes.Add(new Entities.PetType { Name = "Dog" });
                _dataContext.PetTypes.Add(new Entities.PetType { Name = "Cat" });
                _dataContext.PetTypes.Add(new Entities.PetType { Name = "Turtle" });

                // metodo no asincrono: _context.SaveChanges();

                //metodo asincrono
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckOwnerAsync(User user)
        {
            if (!_dataContext.Owners.Any())
            {
                _dataContext.Owners.Add(new Owner { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }
        private async Task CheckManagerAsync(User user)
        {
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new Manager { user = user });
                await _dataContext.SaveChangesAsync();
            }
        }
        private void AddPet(string name, Owner owner, PetType petType, string race)
        {
            _dataContext.Pets.Add(new Pet
            {
                Born = DateTime.Now.AddYears(-2),
                Name = name,
                Owner = owner,
                PetType = petType,
                Race = race
            });
        }

        private async Task CheckAgendasAsync()
        {
            if (!_dataContext.Agendas.Any())
            {
                var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                var finalDate = initialDate.AddYears(1);
                while (initialDate < finalDate)
                {
                    if(initialDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var finalDate2 = initialDate.AddHours(10);
                        while(initialDate < finalDate2)
                        {
                            _dataContext.Agendas.Add(new Agenda
                            {
                                Date = initialDate.ToUniversalTime(),
                                IsAvailable = true
                            });

                            initialDate = initialDate.AddMinutes(30);
                        }

                        initialDate = initialDate.AddHours(14);
                    }
                    else
                    {
                        initialDate = initialDate.AddDays(1);
                    }
                }
            }

            await _dataContext.SaveChangesAsync();
        }
       

       /* private void AddOwner(string document, string firstName, 
            string lastName, string fixedPhone, string cellPhone, string address)
        {
            _dataContext.Owners.Add(new Owner
            {
                Address = address,
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                FixedPhone = fixedPhone,
                LastName = lastName
            });
        } */
    }
}
