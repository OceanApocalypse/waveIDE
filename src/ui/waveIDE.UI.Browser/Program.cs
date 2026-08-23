using System.Runtime.Versioning;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Browser;

using OceanApocalypse.Wave.IDE.UI;

namespace OceanApocalypse.Wave.IDE.UI.Browser;

internal sealed partial class Program
{
	private Program() { }

	private static Task Main(string[] args) =>
		BuildAvaloniaApp()
			.WithInterFont()
			.StartBrowserAppAsync("out");

	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>();
}