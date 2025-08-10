using Foodify.Data;
using Microsoft.EntityFrameworkCore;
using Foodify.Service.Interfaces;
using Foodify.Service.Implementations;
using AutoMapper;
using Foodify.Data.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IDeliveryPartnerService, DeliveryPartnerService>();
builder.Services.AddScoped<IDeliveryAssignmentService, DeliveryAssignmentService>();
builder.Services.AddAutoMapper(typeof(UserProfile), typeof(RestaurantProfile), typeof(OrderProfile), typeof(OrderItemProfile), typeof(MenuProfile), typeof(DeliveryPartnerProfile), typeof(DeliveryAssignmentProfile));
builder.Services.AddControllers(); // Add this if missing

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Add this if missing

app.Run();