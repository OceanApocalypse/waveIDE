using CommunityToolkit.Mvvm.ComponentModel;

namespace OceanApocalypse.Wave.IDE.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
	[ObservableProperty]
	public partial string Greeting { get; set; } = "Welcome to Avalonia!";
}
