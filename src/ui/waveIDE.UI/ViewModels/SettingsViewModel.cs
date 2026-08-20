using CommunityToolkit.Mvvm.ComponentModel;

using OceanApocalypse.Wave.IDE.UI.ViewModels;

namespace OceanApocalypse.Wave.IDE.UI.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
	[ObservableProperty]
	public partial string Greeting { get; set; } = "This is Settings";
}
