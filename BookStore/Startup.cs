using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BookStore.Models;
using BookStore.Modules.AutoMapper;
using BookStore.Seed_Data;
using BookStore.Services;
using BookStore.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BookStore
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
            //dotnet ef dbcontext scaffold -o Models -f -d "Data Source=bookstore.db" "Microsoft.EntityFrameworkCore.Sqlite"
            //Configuration
            AuthenConfig authenConfig = new AuthenConfig();
            Configuration.Bind("JWT", authenConfig);
            services.AddSingleton(x => {
                return authenConfig;
            });

            // Services
            services.AddControllers();
            services.AddSingleton<TokenGenerator>();
            services.AddSingleton<AccessToken>();
            services.AddSingleton<RefreshToken>();
            services.AddScoped<UserServices>();
            services.AddScoped<BookServices>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ImageServices>();
            services.AddScoped<BookCommentServices>();
            services.AddScoped<CartServices>();
            services.AddScoped<InvoiceService>();
            services.AddScoped<InvoiceDetailsService>();
            services.AddScoped<CityServices>();
            services.AddScoped<AuthorServices>();
            services.AddScoped<UserAddressService>();
            services.AddCors(c => {
                c.AddPolicy("TCAPolicy", builder => {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            services.AddEntityFrameworkSqlite().AddDbContext<bookstoreContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using Bearer Scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenConfig.AccessTokenSecret)),
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);



            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            try
            {
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration.GetConnectionString("RedisServer");
                    options.InstanceName = "BookstoreRedis";
                });
            }
            catch
            {
                throw new Exception("Co loi trong qua trinh ket noi Redis");
            }


            //services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("TCAPolicy");

            app.UseRouting();
            
            app.UseAuthentication(); 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.WebRootPath, "images")),
                RequestPath = "/images"
            });

            AppDbInitializer.Seed(app);
        }
    }
}
