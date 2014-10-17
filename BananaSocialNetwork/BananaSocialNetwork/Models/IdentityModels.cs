using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace BananaSocialNetwork.Models
{
    public enum Sexs { Male, Female };
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // Дата регистрации 
        public DateTime RegistrationDate { get; set; }
        // Имя
        public string Firstname { get; set; }
        // Фамилиия
        public string Surname { get; set; }
        // Возраст
        public int Age { get; set; }
        // Пол
        public Sexs Sex { get; set; }
        // Адресс
        public string Adress { get; set; }
        // Путь к аватарке на сервере
        public string AvatatPath { get; set; }

        // Друзья пользователя
        public virtual IEnumerable<ApplicationUser> Friends { get; set; }
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}