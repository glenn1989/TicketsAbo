using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tickets.Data;
using Tickets.Domain.Entities;
using Tickets.Repository;
using Tickets.Repository.Interfaces;
using Tickets.Services;
using Tickets.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));

//Dependency Injection classes

builder.Services.AddTransient<Iservices<Wedstrijd>, WedstrijdServices>();
builder.Services.AddTransient<IDAO<Club>, ClubsDAO>();

builder.Services.AddTransient<Iservices<Club>, ClubServices>();
builder.Services.AddTransient<IDAO<Wedstrijd>, WedstrijdDAO>();

builder.Services.AddTransient<Iservices<Vak>, VakServices>();
builder.Services.AddTransient<IDAO<Vak>, VakDAO>();

builder.Services.AddTransient<Iservices<VakStadion>, VakStadionServices>();
builder.Services.AddTransient<IDAO<VakStadion>, VakStadionDAO>();

builder.Services.AddTransient<Iservices<Aankopen>, AankopenServices>();
builder.Services.AddTransient<IDAO<Aankopen>, AankopenDAO>();

builder.Services.AddTransient<Iservices<Ticket>, TicketServices>();
builder.Services.AddTransient<IDAO<Ticket>, TicketDAO>();

builder.Services.AddTransient<Iservices<Plaat>, PlaatsServices>();
builder.Services.AddTransient<IDAO<Plaat>, PlaatsDAO>();

builder.Services.AddTransient<Iservices<Abonnement>, AbonnementServices>();
builder.Services.AddTransient<IDAO<Abonnement>, AbonnementDAO>();

builder.Services.AddSession(options =>
{

    options.Cookie.Name = "be.VIVES.Session";

    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
