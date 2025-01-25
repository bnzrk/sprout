using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sprout.Web.Data;
using Sprout.Web.Data.Entities.Kanji;
using Sprout.Web.Data.Entities.Review;
using Sprout.Web.Data.Entities.Srs;
using Sprout.Web.Mappings;
using Sprout.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IKanjiRepository, KanjiRepository>();
builder.Services.AddScoped<IKanjiService, KanjiService>();
builder.Services.AddScoped<ISrsDataRepository, SrsDataRepository>();
builder.Services.AddScoped<ISrsService, SrsService>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IDeckRepository, DeckRepository>();
builder.Services.AddScoped<IDeckService, DeckService>();
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sprout API", Version = "v1" });
});
builder.Services.AddDbContext<ApplicationContext>(cfg =>
{
    cfg.UseSqlServer();
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
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
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sprout API v1");
    c.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
