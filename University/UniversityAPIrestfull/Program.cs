// 1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityAPIrestfull;
using UniversityAPIrestfull.DataAccess;
using UniversityAPIrestfull.Services;

var builder = WebApplication.CreateBuilder(args); // sirve para construir las configuraciones que va a usar nuestra aplicación


// 2. Coneccion with SQL Server Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Add Context to services of builder.
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString)); // Utilizamos UseSqlServer() porque viene del using Microsoft.EntityFrameworkCore, por lo que hay que prestar atención a las dependencias.

// 7. Add Sservice of JWT Autorization
// TODO
builder.Services.AddJwtTokenServices(builder.Configuration);



// Add services to the container.
builder.Services.AddControllers();

// 4. Add custom services from StudentService.cs (folder Services)
builder.Services.AddScoped<IStudentsService, StudentService>();     // After adding this builder, we need to add the controller as an scafolding Entity Framework file in Controllers folder.

// TODO: Add the rest of the services


// 9. Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 8. Config Swagger to take care of Autorization of JWT
builder.Services.AddSwaggerGen(options =>
{
    // Security defining for authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name =  "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
            new string[]{}
        }
    });

}
);




// 5. CORS Configuration --> Controls who can make queries, with which methods and what headers can send.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();   // Queries are allowed from any origin.
        builder.AllowAnyMethod();   // Queries are allowed with any method.
        builder.AllowAnyHeader();   // Queries are allowed with any header.
    });

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // sirve para documentar la aplicación
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 6. Tell app to use CORS.
app.UseCors("CorsPolicy");



app.Run();
