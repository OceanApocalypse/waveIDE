/*
 * waveIDE: the libre code editor
 * Copyright (C) 2026  Ocean Apocalypse
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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
