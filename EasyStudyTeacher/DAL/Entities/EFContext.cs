﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStudy.DAL.Entities
{
    public class EFContext : IdentityDbContext<DbUser, DbRole, long, IdentityUserClaim<long>,
         DbUserRole, IdentityUserLogin<long>,
         IdentityRoleClaim<long>, IdentityUserToken<long>>
    {        
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
          
            builder.Entity<Teacher>()
                  .HasOne(a => a.Group)
                  .WithOne(b => b.Teacher)
                  .HasForeignKey<Group>(b => b.TeacherId);
            
            builder.Entity<Student>()
                .HasOne<Group>(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);
            
        }
    }
}