using E_Shopping.Models.Entity.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping.Models.DataContext
{
    public class ShopDbContext: IdentityDbContext<ShopUser, ShopRole, int, ShopUserClaim, ShopUserRole,
        ShopUserLogin, ShopRoleClaim, ShopUserToken>
    {
        public ShopDbContext( DbContextOptions options)
            :base(options)
        {
            
        }
    }
}
