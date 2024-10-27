var builder = WebApplication.CreateBuilder(args);

// Agrega el HttpClient al contenedor de dependencias
builder.Services.AddHttpClient();

// Agrega los controladores al contenedor de dependencias
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger solo en desarrollo (opcional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
