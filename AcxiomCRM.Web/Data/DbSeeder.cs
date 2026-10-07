using AcxiomCRM.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AcxiomCRM.Web.Data
{
    public static class DbSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Manager", "Sales Executive" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (await userManager.FindByEmailAsync("admin@acxiom.com") == null)
            {
                var admin = new ApplicationUser { UserName = "admin@acxiom.com", Email = "admin@acxiom.com", FirstName = "Admin", LastName = "User", Team = "Management" };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
            if (await userManager.FindByEmailAsync("priya@acxiom.com") == null)
            {
                var manager = new ApplicationUser { UserName = "priya@acxiom.com", Email = "priya@acxiom.com", FirstName = "Priya", LastName = "Sharma", Team = "Sales Head" };
                await userManager.CreateAsync(manager, "Manager@123");
                await userManager.AddToRoleAsync(manager, "Manager");
            }
            if (await userManager.FindByEmailAsync("rohan@acxiom.com") == null)
            {
                var sales = new ApplicationUser { UserName = "rohan@acxiom.com", Email = "rohan@acxiom.com", FirstName = "Rohan", LastName = "Iyer", Team = "North" };
                await userManager.CreateAsync(sales, "Sales@123");
                await userManager.AddToRoleAsync(sales, "Sales Executive");
            }

            var context = serviceProvider.GetRequiredService<AcxiomCRM.Data.Contexts.ApplicationDbContext>();
            var adminUser = await userManager.FindByEmailAsync("admin@acxiom.com");
            var salesUser = await userManager.FindByEmailAsync("rohan@acxiom.com");
            var managerUser = await userManager.FindByEmailAsync("priya@acxiom.com");

            if (!context.Customers.Any())
            {
                var customers = new List<Customer>();
                string[] companies = { "Acme Corp", "TechNova", "Global Ind", "Stark Ent", "Wayne Tech", "LexCorp", "Umbrella Corp", "Oscorp", "Cyberdyne", "Initech" };
                string[] cities = { "New York", "San Francisco", "London", "Los Angeles", "Chicago", "Tokyo", "Berlin", "Dubai", "Mumbai", "Singapore" };
                for(int i=1; i<=15; i++) {
                    customers.Add(new Customer { Code = $"C-{i:D3}", CustomerName = companies[i%10] + " Ltd", Company = companies[i%10], City = cities[i%10], Status = (i%4==0) ? "Inactive" : "Active", OwnerId = (i%2==0) ? salesUser.Id : managerUser.Id, CreatedAt = DateTime.Now.AddDays(-i*10) });
                }
                context.Customers.AddRange(customers);
                await context.SaveChangesAsync();
            }

            if (!context.Leads.Any())
            {
                var leads = new List<Lead>();
                string[] sources = { "Website", "Referral", "Cold Call", "Conference", "Social Media" };
                string[] statuses = { "New", "Contacted", "Qualified", "Lost" };
                for(int i=1; i<=25; i++) {
                    leads.Add(new Lead { Code = $"L-{i:D3}", LeadName = $"Project Alpha {i}", Source = sources[i%5], Status = statuses[i%4], Value = 50000 + (i*12000), OwnerId = salesUser.Id, CreatedAt = DateTime.Now.AddDays(-i*2) });
                }
                context.Leads.AddRange(leads);
                await context.SaveChangesAsync();
            }

            if (!context.Opportunities.Any())
            {
                var opps = new List<Opportunity>();
                var customers = context.Customers.ToList();
                string[] stages = { "Prospecting", "Qualification", "Proposal", "Negotiation", "Closed Won", "Closed Lost" };
                for(int i=1; i<=20; i++) {
                    opps.Add(new Opportunity { Code = $"OPP-{i:D3}", OpportunityName = $"Enterprise License {i}", CustomerId = customers[i%customers.Count].Id, Stage = stages[i%6], Amount = 100000 + (i*50000), ExpectedCloseDate = DateTime.Now.AddDays(i*5), OwnerId = managerUser.Id, CreatedAt = DateTime.Now.AddDays(-i*3) });
                }
                context.Opportunities.AddRange(opps);
                await context.SaveChangesAsync();
            }

            if (!context.FollowUps.Any())
            {
                var followUps = new List<FollowUp>();
                var leads = context.Leads.ToList();
                string[] methods = { "Call", "Email", "Meeting", "Demo" };
                for(int i=1; i<=15; i++) {
                    followUps.Add(new FollowUp { LeadId = leads[i%leads.Count].Id, Date = DateTime.Now.AddDays(i%5), Type = methods[i%4], Subject = $"Discussed pricing structure {i}", Status = (i%3==0) ? "Completed" : "Pending", OwnerId = salesUser.Id });
                }
                context.FollowUps.AddRange(followUps);
                await context.SaveChangesAsync();
            }

            if (!context.Activities.Any())
            {
                var acts = new List<Activity>();
                string[] actTypes = { "Call Logged", "Email Sent", "Meeting Held", "Note Added" };
                for(int i=1; i<=30; i++) {
                    acts.Add(new Activity { Type = actTypes[i%4], Description = $"Action performed with client {i}", Date = DateTime.Now.AddHours(-i*4), OwnerId = salesUser.Id });
                }
                context.Activities.AddRange(acts);
                await context.SaveChangesAsync();
            }
        }
    }
}
