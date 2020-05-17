using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyKitchen.Controllers;
using MyKitchen.Data;
using MyKitchen.Models;
using MyKitchen.Models.FoodItems;
using Xunit;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace MyKitchen.Tests
{
    public class RegistrationTests {


            [Fact]
            public void RegisterNewUser()
            {
                            //dependencies
            // var serviceProvider = new ServiceCollection()
            //     .AddLogging()
            //     .BuildServiceProvider();

            var services = new ServiceCollection();
            services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

   

            var um = services.BuildServiceProvider().GetService<UserManager<IdentityUser>>();
            
            
            //                     var result = await _userManager.CreateAsync(user, Input.Password);




            #region examples that didn't work
            //      var _serviceCollection = new ServiceCollection();
            //          _serviceCollection.AddIdentity<MyKitchen.Data.ApplicationUser, IdentityRole>()
            //         .AddEntityFrameworkStores<ApplicationDbContext<MyKithen.Data.ApplicationUser>()
            //     .AddDefaultTokenProviders();



            // _serviceCollection.AddDbContext<IdentityDbContext<IdentityUser>>(options =>
            // {
            //     options.UseSqlServer(connString);
            // });




            //     var user = new MyKitchen.Data.ApplicationUser { UserName = Input.Email, Email = Input.Email };
            //     var userManager = new UserManager<MyKitchen.Data.ApplicationUser>(

            //         new UserStoreBase<MyKitchen.Data.ApplicationUser,


            //     );


    //public UserManager(IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger);

        //



                // var userManager = new UserManager<MyKitchen.Data.ApplicationUser>(new UserStore<MyKitchen.Data.ApplicationUser,CustomRole,string,CustomUserLogin,CustomuserRole,CustomUserClaim>();

                // new UserStore<MyKitchen.Data.ApplicationUser, CustomRole, string, CustomUserLogin, CustomUserRole, CustomUserClaim>(new ApplicationDbContext()));
                //var userManager = new userManager





#endregion


            }




    }

}
