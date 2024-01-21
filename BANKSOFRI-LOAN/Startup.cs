using BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logic;
using BANKSOFRI_LOAN.DATALAYER.DomainObjects.Repos;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Hangfire;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfireServer();
            services.AddControllers();
            services.AddScoped<ILien, LienService>();
            services.AddScoped<IOtherloans, OtherLoanCollectionService>();
            services.AddScoped<ILoanService, LoanService>();           
            services.AddScoped<IBone, BankOneIntegration>();
            services.AddScoped<IBoneLoan, BankOneLoanProccessing>();
            services.AddScoped<ICrlogic, CRLogic>();
            services.AddScoped<ICustomer, CustomerService>();
            services.AddScoped<ILoanApp, LoanApplication>();
            services.AddScoped<ILoanPro, LoanRequestProccessor>();
            services.AddScoped<IMessage, MessagingAPIIntegration>();
            services.AddScoped<INotice, MessagingService>();
            services.AddScoped<IPaystack, PaystackAPIIntegration>();
            services.AddScoped<IProvenir, ProvenirData>();
            services.AddScoped<IUserAuth, UserAuthorization>();
            services.AddScoped<IValidate, ValidateUser>();
            services.AddScoped<IVerify, VerifymeService>();
            services.AddScoped<IVerifyBvn, VerifyMeIntegration>();
            services.AddScoped<IOverDue, OverDueCollectionService>();
            services.AddScoped<ICollect, CollectionService>();
            services.AddScoped<ILogs, AppLogger>();
            services.AddScoped<IRepo, Repo>();
            services.AddScoped<IPromo, PromoManager>();
            services.AddScoped<IOthers, OtherSofriLoan>();
            services.AddScoped<ILiquidate, LiquidationService>();
            services.AddScoped<IRepay, Repayment>();
            services.AddScoped<ICRC, CRCIntegration>();
            services.AddScoped<ICRegistry, CreditRegistryIntegrations>();
            services.AddScoped<ICBScore, CreditBureauService>();
            services.AddScoped<IMono, MonoService>();
            services.AddScoped<IMoint, MonoIntegration>();
            services.AddScoped<IMailer, EmailSender>(i =>
            new EmailSender(
                Configuration["EmailSender:Host"],
                Configuration.GetValue<int>("EmailSender:Port"),
                Configuration["EmailSender:UserName"],
                Configuration["EmailSender:Password"],
                Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                Configuration["EmailSender:AddCardUrl"]
            ));
            services.AddDbContext<PaystackCardsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CardConnection")));
            services.AddDbContext<ApiauthorisationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BANKSOFRI LOAN MIDDLEWARE API", Version = "v1" });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                              new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "basic"
                                    }
                                },
                                new string[] {}
                        }
                    });           
            });
            services.AddCors();
            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("ServiceConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));
            GlobalConfiguration.Configuration.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")).WithJobExpirationTimeout(TimeSpan.FromDays(7));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 );


            app.UseHangfireDashboard("/services", new DashboardOptions()
            {
                AppPath = null,
                DashboardTitle = "SOFRI NANO LOAN SERVICE DASHBOARD",
                Authorization = new[]{
                new HangfireCustomBasicAuthenticationFilter{
                    User = Configuration.GetSection("HangfireCredentials:UserName").Value,
                    Pass = Configuration.GetSection("HangfireCredentials:Password").Value
                }
            },
            });
            //RecurringJob.AddOrUpdate<ILien>(x => x.UnlienAccountService(), Cron.Daily);
            //RecurringJob.AddOrUpdate<IRepay>(x => x.LoanRepayment(), "0 17 * * *");
            //RecurringJob.AddOrUpdate<ICollect>(x => x.SyncLoanStatusWithBankOne(), "*/59 * * * *");
            //RecurringJob.AddOrUpdate<IRepay>(x => x.LoanFailedRepayment(), Cron.Daily);
            //RecurringJob.AddOrUpdate<IRepay>(x => x.OtherSofriLoanFailedRepayment(), Cron.Daily);

            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    #if DEBUG
                      c.SwaggerEndpoint("/swagger/v1/swagger.json", "BANKSOFRI LOAN MIDDLEWARE v1");
                    #else
                       c.SwaggerEndpoint("/NANOLOAN/swagger/v1/swagger.json", "BANKSOFRI LOAN MIDDLEWARE API v1");
                    #endif

                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
