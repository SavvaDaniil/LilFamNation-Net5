using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherRate> TeacherRates { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Abonement> Abonements { get; set; }
        public DbSet<AbonementDynamicDateMustBeUsedTo> AbonementDynamicDatesMustBeUsedTo { get; set; }
        public DbSet<DanceGroup> DanceGroups { get; set; }
        public DbSet<DanceGroupDayOfWeek> DanceGroupDayOfWeeks { get; set; }
        public DbSet<ConnectionAbonementToDanceGroup> ConnectionsAbonementToDanceGroup { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ConnectionAbonementToDiscount> ConnectionsAbonementToDiscount { get; set; }
        public DbSet<ConnectionDiscountToUser> ConnectionsDiscountToUser { get; set; }
        public DbSet<ConnectionDanceGroupToUserAdmin> ConnectionsDanceGroupToUserAdmin { get; set; }
        public DbSet<ConnectionAbonementPrivateToUser> ConnectionsAbonementPrivateToUser { get; set; }
        public DbSet<ConnectionUserToDanceGroup> ConnectionsUserToDanceGroup { get; set; }

        public DbSet<PurchaseAbonement> PurchasesAbonement { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<TeacherSalary> TeacherSalaries { get; set; }
        public DbSet<TeacherReplacement> TeacherReplacements { get; set; }
    }
}
