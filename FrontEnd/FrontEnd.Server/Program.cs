using HarnishBalanceSheet.BusinessLogic;
using HarnishBalanceSheet.DataAccess;
using HarnishBalanceSheet.PreciousMetalsService;
using HarnishBalanceSheet.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<BalanceSheetContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBalanceSheetRepository, BalanceSheetRepository>();
builder.Services.AddScoped<IPreciousMetalsService, PreciousMetalsService>();
builder.Services.AddScoped<IBalanceSheetBL, BalanceSheetBL>();
builder.Services.AddScoped<AuthenticateFilter>();
builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy
            .SetIsOriginAllowed(origin =>
            {
                if (origin.Contains("localhost"))
                {
                    return true;
                }

                return false;
            })
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    
});

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Authority = builder.Configuration["AzureAd:Instance"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, 
        ValidIssuer = builder.Configuration["AzureAd:Instance"], 
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AzureAd:Audience"],
        ValidateIssuerSigningKey = false
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            // Log the exception for detailed error information
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            // Log details about why the challenge is occurring
            Console.WriteLine($"OnChallenge: {context}, {context.Request.Path}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
