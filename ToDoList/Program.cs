using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories.Abstract;
using ToDoList.Domain.Repositories.EntityFramework;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// configure identity
builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 3;   // ����������� �����
    opts.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
    opts.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
    opts.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
    opts.Password.RequireDigit = false; // ��������� �� �����
})
    .AddEntityFrameworkStores<ApplicationContext>();

//Configure repository
builder.Services.AddTransient<ITaskListRepository, TaskListRepository>();
builder.Services.AddTransient<ITaskItemRepository, TaskItemRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
