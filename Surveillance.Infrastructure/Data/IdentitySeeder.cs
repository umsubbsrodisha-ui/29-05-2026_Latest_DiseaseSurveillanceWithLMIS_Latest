

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Surveillance.Domain.Entities;


namespace Surveillance.Infrastructure.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context)
        {
            // ----------------------------------------------------
            // ROLES
            // ----------------------------------------------------

            string[] roles =
            {
                // Existing Roles
                "Admin",
                "Analyst",
                //"UPHCUser",
                //"CHCUser",
                //"UHWCUser",
                "NodalOfficer",
                "AddlnCommissioner",
                "MD",
                "Commissioner",
                "JdAdmin",

                // LMIS Roles
                "DEO",
                "MO",
                "LT",
                "MB"
            };

            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            // ----------------------------------------------------
            // SYSTEM USERS
            // ----------------------------------------------------

            var usersToSeed_central = new List<(string Username,
                                        string Email,
                                        string Password,
                                        string Role)>
{
    ("analyst", "analyst@umsu.com", "Analyst@123!", "Analyst"),

    //("uphcuser", "uphc@umsu.com", "UPHc@123!", "UPHCUser"),

    //("chcuser", "chc@umsu.com", "CHc@123!", "CHCUser"),

    //("uhwcuser", "uhwc@umsu.com", "UHWc@123!", "UHWCUser"),

    ("nodal", "nodal@umsu.com", "Nodal@123!", "NodalOfficer"),

    ("addlncomm", "addln@umsu.com", "Addln@123!", "AddlnCommissioner"),

    ("md", "md@umsu.com", "MD@123!", "MD"),

    ("commissioner", "comm@umsu.com", "Comm@123!", "Commissioner"),

    ("jdadmin", "jd@umsu.com", "JD@123!", "JdAdmin")
};

            foreach (var u in usersToSeed_central)
            {
                var existingUser =
                    await userManager.FindByNameAsync(u.Username);

                if (existingUser == null)
                {
                    int? facilityId = null;

                    //if (u.Role == "CHCUser")
                    //    facilityId = 1;

                    //else if (u.Role == "UPHCUser")
                    //    facilityId = 24;

                    //else if (u.Role == "UHWCUser")
                    //    facilityId = 27;

                    var user = new ApplicationUser
                    {
                        UserName = u.Username,

                        Email = u.Email,

                        EmailConfirmed = true,

                        FacilityId = facilityId
                    };

                    var result =
                        await userManager.CreateAsync(user, u.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, u.Role);
                    }
                }
            }

            // ----------------------------------------------------
            // ADMIN USER
            // ----------------------------------------------------

            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@umsu.com",
                    EmailConfirmed = true,
                    FacilityId = 1
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // ----------------------------------------------------
            // FACILITY BASED USERS
            // ----------------------------------------------------

            var facilities = await context.Facilities.ToListAsync();

            foreach (var facility in facilities)
            {
                var usersToSeed =
                    new List<(string Username,
                              string Email,
                              string Role,
                              string Password)>
                {
                    (
                        $"deo_{facility.Id}",
                        $"deo_{facility.Id}@umsu.com",
                        "DEO",
                        $"Deo@Facility{facility.Id}"
                    ),

                    (
                        $"mo_{facility.Id}",
                        $"mo_{facility.Id}@umsu.com",
                        "MO",
                        $"Mo@Facility{facility.Id}"
                    ),

                    (
                        $"lt_{facility.Id}",
                        $"lt_{facility.Id}@umsu.com",
                        "LT",
                        $"Lt@Facility{facility.Id}"
                    ),

                    (
                        $"mb_{facility.Id}",
                        $"mb_{facility.Id}@umsu.com",
                        "MB",
                        $"Mb@Facility{facility.Id}"
                    )
                };

                foreach (var u in usersToSeed)
                {
                    var existingUser =
                        await userManager.FindByNameAsync(u.Username);

                    if (existingUser == null)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = u.Username,
                            Email = u.Email,
                            EmailConfirmed = true,

                            FacilityId = facility.Id
                        };

                        var result =
                            await userManager.CreateAsync(user, u.Password);

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, u.Role);
                        }
                    }
                }
            }
        }
    }
}
