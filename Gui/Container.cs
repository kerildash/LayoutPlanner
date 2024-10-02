using Database.Repositories;
using Database;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Gui;

public class Container
{
    public IServiceProvider ServiceProvider { get; private set; }


    public Container()
    {
        ServiceCollection services = new();
        services.AddTransient<HttpService>();
        services.AddTransient<MissionService>();

        services.AddScoped<DialogService>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                ConfigurationManager.AppSettings["DefaultConnection"]);
        });

        services.AddScoped<IRepository<Pallet>, PalletRepository>();
        services.AddScoped<IRepository<Box>, BoxRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        ServiceProvider = services.BuildServiceProvider();

        services.AddScoped<ServiceProvider>();

        MainWindowViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
    }

    public MainWindowViewModel MainWindowViewModel { get; private set; }
}