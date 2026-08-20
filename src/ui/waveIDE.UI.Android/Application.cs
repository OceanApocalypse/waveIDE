using Android.App;
using Android.Runtime;

using Avalonia;
using Avalonia.Android;

using OceanApocalypse.Wave.IDE.UI;

namespace OceanApocalypse.Wave.IDE.UI.Android;

[Application]
public class Application : AvaloniaAndroidApplication<App>
{
	protected Application(nint javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
	{
	}

	protected override AppBuilder CustomizeAppBuilder(AppBuilder builder) => base.CustomizeAppBuilder(builder).WithInterFont();
}
