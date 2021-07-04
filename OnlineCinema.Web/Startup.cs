using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineCinema.Repository;
using OnlineCinema.Domain.Identity;
using OnlineCinema.Repository.Interface;
using OnlineCinema.Repository.Implementation;
using OnlineCinema.Services.Interface;
using OnlineCinema.Services.Implementation;
using OnlineCinema.Domain;
using OnlineCinema.Services;
using Stripe;

namespace OnlineCinema.Web
{
    public class Startup
    {
        private MailSettings mailSettings;
        public Startup(IConfiguration configuration)
        {
            mailSettings = new MailSettings();
            Configuration = configuration;
            Configuration.GetSection("MailSettings").Bind(mailSettings);

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDefaultIdentity<OnlineCinemaTicketUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));

            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<IOrderService, Services.Implementation.OrderService>();

            /*  services.AddScoped<MailSettings>(es => mailSettings);
              services.AddScoped<IMailService, MailService>(email => new MailService(mailSettings));
              services.AddScoped<IBackgroundSender, BackgroundSender>();
              services.AddHostedService<ConsumeScopedHostedService>();*/


            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));


            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKeyHere"]);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
