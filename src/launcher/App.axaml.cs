using System;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using OceanApocalypse.Wave.IDE.UI.ViewModels;
using OceanApocalypse.Wave.IDE.UI.Views;

namespace OceanApocalypse.Wave.IDE;

public partial class App : Application
{
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
#if DEBUG
		this.AttachDeveloperTools();
#endif
	}

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.MainWindow = new MainWindow
			{
				DataContext = new MainViewModel()
			};
		}
		else if (ApplicationLifetime is IActivityApplicationLifetime singleViewFactoryApplicationLifetime)
		{
			singleViewFactoryApplicationLifetime.MainViewFactory = () => new PageNavigationHost()
			{
				Page = new MainView { DataContext = new MainViewModel() }
			};
		}
		else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
		{
			singleViewPlatform.MainView = new PageNavigationHost()
			{
				Page = new MainView { DataContext = new MainViewModel() }
			};
		}
		else
		{
			throw new InvalidOperationException("waveIDE is not supported in this platform.");
		}

		base.OnFrameworkInitializationCompleted();
	}
}
