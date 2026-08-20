using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;

namespace OceanApocalypse.Wave.IDE.UI.Android;

[Activity(
	Label = "waveIDE.UI.Android",
	Theme = "@style/MyTheme.NoActionBar",
	Icon = "@drawable/icon",
	MainLauncher = true,
	ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity
{
}
