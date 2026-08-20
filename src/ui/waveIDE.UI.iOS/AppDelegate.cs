using Foundation;

using UIKit;

using Avalonia;
using Avalonia.Controls;
using Avalonia.iOS;
using Avalonia.Media;

using OceanApocalypse.Wave.IDE.UI;

namespace OceanApocalypse.Wave.IDE.UI.iOS;

// The UIApplicationDelegate for the application. This class is responsible for launching the 
// User Interface of the application, as well as listening (and optionally responding) to 
// application events from iOS.
[Register("AppDelegate")]
internal sealed partial class AppDelegate : AvaloniaAppDelegate<App>
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
{
	protected override AppBuilder CustomizeAppBuilder(AppBuilder builder) => base.CustomizeAppBuilder(builder).WithInterFont();
}
