using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole{Name = "User", NormalizedName = "USER"}
            }; 
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(login => new { login.LoginProvider, login.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(role => new { role.UserId, role.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
    
            modelBuilder.UseCollation("utf8mb4_general_ci");
            modelBuilder.Entity<IdentityRole>().HasData(roles); 
        }

        public DbSet<Stock> Stocks {get; set;}
        public DbSet<Comment> Comments {get; set;}  
        
    }
}