using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Text;
using System.Windows;

namespace Gui;


public partial class MainWindow : Window
{
    public IServiceProvider ServiceProvider { get; private set; }

    public MainWindow()
    {
        ServiceCollection services = new();
        services.AddTransient<HttpService>();
        services.AddTransient<MissionService>();

        services.AddScoped<DialogService>();
        services.AddSingleton<MainWindowViewModel>();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        ServiceProvider = services.BuildServiceProvider();

        services.AddScoped<ServiceProvider>();
        MainWindowViewModel viewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
        DataContext = viewModel;
    }
}
