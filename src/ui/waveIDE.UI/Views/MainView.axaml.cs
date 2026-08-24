
using System;

using Avalonia;
using Avalonia.Controls;

using OceanApocalypse.Wave.IDE.UI.ViewModels;

namespace OceanApocalypse.Wave.IDE.UI.Views;

public partial class MainView : DrawerPage
{
	public MainView() => InitializeComponent();

	protected override async void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
	{
		base.OnAttachedToVisualTree(e);

		UpdatePage(DrawerList.SelectedIndex);
	}

	private async void DrawerList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		if (ContentPage != null && sender is ListBox listbox)
		{
			var index = listbox.SelectedIndex;
			UpdatePage(index);
		}
	}

	private void UpdatePage(int index)
	{
		ViewModelBase page = index switch
		{
			0 => new HomeViewModel(),
			1 => new SettingsViewModel(),
			_ => throw new NotImplementedException()
		};

		ContentPage.Content = page;
		IsOpen = false;
	}
}
