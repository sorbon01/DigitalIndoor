﻿using DigitalIndoor.DTOs.Request;
using DigitalIndoor.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace DigitalIndoor.Services.Implementations
{
    public class DemoService : IDemoService
    {
        readonly Context context;
        readonly IUserService userService;
        readonly IRoleService roleService;

        public DemoService(
            Context context,
            IUserService userService,
            IRoleService roleService
            )
        {
            this.context = context;
            this.userService = userService;
            this.roleService = roleService;
        }
        public async Task CreateDemoAsync()
        {
            if (await context.Users.AnyAsync())
                return;

            // admin role
            await roleService.AddAsync(new RoleCreateDto()
            {
                Name = "Admin",
                Functionals = new string[] { "allFunctionals" }
            });
            // admin user 
            await userService.AddAsync(new UserCreateDto()
            {
                FullName = "Administrator",
                Username = "admin",
                Password = "admin",
                RoleId = 1
            });

        }
    }
}
