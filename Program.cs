using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PodCastPipocaAgilApi.Context;
using PodCastPipocaAgilApi.Interfaces;
using PodCastPipocaAgilApi.Models;
using PodCastPipocaAgilApi.Repository;
using PodCastPipocaAgilApi.SendEmail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PodCastPipocaAgilApiContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"))
);

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "PodCastPipocaWebApi.",
            Version = "v1.",
            Description =
                "Funcionalidade 'Trilha de Conhecimento' no Podcast Pipoca Ágil. Explore vídeos sobre Metodologias Ágeis, Inovação, Gerenciamento de Projetos, e mais. Acesse trilhas personalizadas, e receba recomendações gratuitamente.",
            TermsOfService = new Uri("https://meusite.com"),
            Contact = new OpenApiContact
            {
                Name = "Victor Sérgio",
                Url = new Uri("https://meusite.com")
            },
            License = new OpenApiLicense
            {
                Name = "PodCast Pipoca Agil",
                Url = new Uri("https://meusite.com")
            }
        }
    );
    //Add Configuracoes extras da documentaçao, para ler os XMLs
    var xmlArquivo = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlArquivo));
});

//Config JWT

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
    })
    .AddJwtBearer(
        "JwtBearer",
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes("PodCastPipocaAgilDb-chave-autenticacao")
                ),
                ClockSkew = TimeSpan.FromMinutes(30),
                ValidIssuer = "PodCastPipocaAgilDbAPI.Web",
                ValidAudience = "PodCastPipocaAgilDbAPI.Web"
            };
        }
    );

builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<PodCastPipocaAgilApiContext>();
builder.Services.AddTransient<ICadastroRepository, CadastroRepository>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<ILiveRepository, LiveRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.UseStaticFiles();
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")
        ),
        RequestPath = "/Files"
    }
);

app.MapControllers();

app.Run();
