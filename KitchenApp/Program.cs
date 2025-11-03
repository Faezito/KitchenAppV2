using DotNetEnv;
using KitchenApp.Libraries.Login;
using KitchenApp.Libraries.Sessao;
using System.Data.Common;
using Z2.Servicos;
using Z3.DataAccess;
using Z3.DataAccess.Database;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDapper, DapperDatabase>(x =>
{
    string strProv = "Microsoft.Data.SqlClient";
    string strConn = builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer");

    DbProviderFactories.RegisterFactory(strProv, Microsoft.Data.SqlClient.SqlClientFactory.Instance);
    return new DapperDatabase(strProv, strConn);
});

builder.Services.AddScoped<IUsuarioDataAccess, UsuarioDataAccess>();
builder.Services.AddScoped<IUsuarioServicos, UsuarioServicos>();
builder.Services.AddScoped<IProdutoDataAccess, ProdutoDataAccess>();
builder.Services.AddScoped<IProdutoServicos, ProdutoServicos>();
builder.Services.AddScoped<IIngredienteServicos, IngredienteServicos>();
builder.Services.AddScoped<IIngredienteDataAccess, IngredienteDataAccess>();

// Cookies

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});



// Configurações de sessão

builder.Services.AddMemoryCache(); // serve para guardar os dados na memória
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.IOTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddScoped<Sessao>();
builder.Services.AddScoped<LoginUsuario>();

var app = builder.Build();

Env.Load();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
