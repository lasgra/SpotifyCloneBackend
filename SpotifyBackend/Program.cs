using SpotifyBackend.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAntiforgery();
builder.Services.AddSingleton<UserService>();

var app = builder.Build();
app.UseAntiforgery();
app.UseCors(
  options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Tracks")),
    RequestPath = "/Tracks"
});

app.MapGet("/SongName", (UserService service, string Name) =>
{
    var Found = service.FindSongName(Name);
    return Found;
});

app.MapGet("/SongAutor", (UserService service, string Autor) => {
    var Found = service.FindSongAutor(Autor);
    return Found;
});

app.MapGet("/SongSearch", (UserService service, string Input) =>
{
    var Found = service.SearchSongs(Input);
    return Found;
});

app.MapGet("/SongRandom", (UserService service) => {
    return Results.Ok(service.GetRandomSongList());
});


app.Run();