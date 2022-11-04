using customerapi.CasosDeUso;
using customerapi.Repositories;
using Microsoft.EntityFrameworkCore;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("*")
                                              .AllowAnyMethod().AllowAnyHeader();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(rouing=>rouing.LowercaseUrls=true); 

builder.Services.AddDbContext<CustomerDatabaseContext>(mysqlbuilder =>{
   // builder.UseMySQL("Server=localhost;Port=3306;Database=systemapi;Uid=root;");
   mysqlbuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection1"));
    
});

builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/*app.MapGet("/customer/{id}", (long id) =>{
    return "net 6";
});*/
//app.UseCors(MyAllowSpecificOrigins);
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin 
    .AllowCredentials());
    
app.MapControllers();

app.Run();
