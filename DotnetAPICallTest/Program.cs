using DotnetAPIStartupTesting.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//add api support
builder.Services.AddHttpClient("JsonHTTPClient", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
});
builder.Services.AddSingleton<APIDataSets>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var apiData = app.Services.GetRequiredService<APIDataSets>();
var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("JsonHTTPClient");
var response = await httpClient.GetAsync("todos/3");
if (response.IsSuccessStatusCode)
{
    var responseData = await response.Content.ReadFromJsonAsync<ToDo>();
    apiData.ToDo = responseData;
}

app.Run();
