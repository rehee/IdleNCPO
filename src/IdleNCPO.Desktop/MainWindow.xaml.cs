using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using IdleNCPO.Core.Services;

namespace IdleNCPO.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  public MainWindow()
  {
    var services = new ServiceCollection();
    services.AddWpfBlazorWebView();
    services.AddSingleton<ProfileService>();
#if DEBUG
    services.AddBlazorWebViewDeveloperTools();
#endif
    Resources.Add("services", services.BuildServiceProvider());
    InitializeComponent();
  }
}