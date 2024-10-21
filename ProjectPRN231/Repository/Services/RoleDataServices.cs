using BussinessObject.DTOS.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
 
   public class RoleDataServices : IDataServices
    {
        private readonly IServiceProvider _serviceProvider;

        public RoleDataServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task AddData()
        {
            using var scope = _serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in UserRole.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
