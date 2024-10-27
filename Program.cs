var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFront", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500") // Cambia esto por el origen de tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Agrega el HttpClient y los controladores
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usa CORS con la política definida
app.UseCors("PermitirFront");

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
