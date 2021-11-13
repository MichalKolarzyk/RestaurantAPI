using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    IEnumerable<Role> roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Restaurants.Any())
                {
                    IEnumerable<Restaurant> restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            return new List<Role> 
            {
                new Role(){Name= "User"},
                new Role(){Name= "Manager"},
                new Role(){Name= "Admin"},
            };
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            return new List<Restaurant>
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC fast food restaurant",
                    ContactEmail = "kfc@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish() {Name = "Pizza americana", Price = 10M},
                        new Dish() {Name = "Pizza Costam", Price = 20M},
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Małopolska",
                        PostalCode = "20-630",
                    },
                },

                new Restaurant()
                {
                    Name = "KFC2",
                    Category = "Fast Food2",
                    Description = "KFC fast food restaurant2",
                    ContactEmail = "kfc@kfc2.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish() {Name = "Pizza americana2", Price = 10M},
                        new Dish() {Name = "Pizza Costam2", Price = 20M},
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Małopolska2",
                        PostalCode = "20-630",
                    },
                }
            };
        }
    }
}
