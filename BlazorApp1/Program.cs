using BlazorApp1.Components;






//Om denna sätts på 0: URL till API:et i denna solution används och filer cachas ej
//Om denna sätts på 1: Valfri URL till API används
int isProduction = 1;




Uri uri = new("https://localhost:7208");




if(isProduction == 1)
{
    //Skriv URL till din deployade API
    uri = new("");
}










var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();













builder.Services.AddBlazorBootstrap();
builder.Services.AddSingleton(new AlertComponent());
builder.Services.AddSingleton(new ConfirmDialogComponent());

builder.Services.AddScoped(sp =>
    new HttpClient {BaseAddress = uri});









var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();











if (isProduction == 1)
{
    app.UseStaticFiles();
}


else
{
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = staticFileResponseContext =>
        {
            staticFileResponseContext.Context.Response.Headers.Append(
                    "Cache-Control", "no-store");
        }
    });
}








app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();