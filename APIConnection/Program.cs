using APIConnection.Data;
using APIConnection.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string ConnectionString = ""; //@"Server=localhost\SQLEXPRESS;Initial Catalog=MAUI_Db;MultipleActiveResultSets=true;User ID=DESKTOP-AAMVKH7\zakir;Password=;Integrated Security = True";
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(ConnectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/ConnectionBuild", (ConnectionModel connectionProperties) =>
{
        ConnectionString = $"Server={connectionProperties.ServerName};Initial Catalog={connectionProperties.Catalog};User Id={connectionProperties.Username};Password={connectionProperties.Password};Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security = True";
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(ConnectionString));
    app = builder.Build();
    
})
.WithName("CreateConnectionBuild");

app.MapGet("/Departments", async (ApplicationDbContext db) =>
{
  return await db.Departments.ToListAsync();
})
.WithName("GetDepartments");

app.MapPost("/CreateDepartment", async (Department department, ApplicationDbContext db) =>
{
    await db.Departments.AddAsync(department);
    db.SaveChanges();
    return Results.Created($"/Departments/{department.Id}", department);

})
.WithName("CreateDepartment");

app.MapGet("/Departments/{id}", async (int id, ApplicationDbContext db) =>
{
    return await db.Departments.FirstOrDefaultAsync(x => x.Id == id);
})
.WithName("GetDepartmentsById");

app.MapPut("/UpdateDepartment/{id}", async (int id, Department department, ApplicationDbContext db) =>
{
    var dep = await db.Departments.FirstOrDefaultAsync(x => x.Id == id);
    dep.Name = department.Name;
   await db.SaveChangesAsync();
    return dep;
})
.WithName("UpdateDepartment");

app.Run();
