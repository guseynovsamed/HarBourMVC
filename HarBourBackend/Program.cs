using Domain.Models;
using HarBourBackEnd.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Service;
using Service.Service.Interfaces;
using System;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));



// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
                                                     .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredUniqueChars = 1;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireNonAlphanumeric = true;

    opt.User.RequireUniqueEmail = true;

    opt.SignIn.RequireConfirmedEmail = true;
});


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Smtp"));




builder.Services.AddScoped<ISliderVideoService, SliderVideoService>();
builder.Services.AddScoped<ISliderVideoRepository, SliderVideoRepository>();


builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();


builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();


builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();


builder.Services.AddScoped<IYachtCategoryService, YachtCategoryService>();
builder.Services.AddScoped<IYachtCategoryRepository, YachtCategoryRepository>();


builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IAboutRepository, AboutRepository>();


builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();


builder.Services.AddScoped<IDestinationCountryService, DestinationCountryService>();
builder.Services.AddScoped<IDestinationCountryRepository, DestinationCountryRepository>();


builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();


builder.Services.AddScoped<IComplaintService, ComplaintService>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();


builder.Services.AddScoped<IDestinationCityService, DestinationCityService>();
builder.Services.AddScoped<IDestinationCityRepository, DestinationCityRepository>();


builder.Services.AddScoped<IWaterSportService, WaterSportService>();
builder.Services.AddScoped<IWaterSportRepository, WaterSportRepository>();


builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();


builder.Services.AddScoped<IYachtService, YachtService>();
builder.Services.AddScoped<IYachtRepository, YachtRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();


builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();



var app = builder.Build();




//if (app.Environment.IsProduction())
//{
//    DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
//    developerExceptionPageOptions.SourceCodeLineCount = 1;
//    app.UseDeveloperExceptionPage(developerExceptionPageOptions);

//    //app.UseExceptionHandler("/Home/Error");
//    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    //app.UseHsts();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


//app.UseStatusCodePagesWithReExecute("/StatusCodeError/{0}");

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();