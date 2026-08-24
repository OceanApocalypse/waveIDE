using CommunityToolkit.Mvvm.ComponentModel;

namespace OceanApocalypse.Wave.IDE.UI.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
	[ObservableProperty]
	public partial string Greeting { get; set; } = "This is Settings";
}
