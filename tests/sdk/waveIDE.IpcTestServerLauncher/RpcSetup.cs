using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using PolyType;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.SDK.Tests.IpcLauncher;

[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(string))]
internal sealed partial class SourceGenerationContext : JsonSerializerContext;

/// <summary>
/// Helper for JSON formatter.
/// </summary>
[SuppressMessage("Maintainability", "CA1515", Justification = "Used by the minimal sample - will be cleaned up later")]
public static class JsonFormatterHelper
{
	/// <summary>
	/// Creates a JSON source generator.
	/// </summary>
	/// <returns></returns>
	[SuppressMessage("Trimming", "IL2026", Justification = "Using the JSON source generator")]
	[SuppressMessage("AOT", "IL3050", Justification = "Using the JSON source generator")]
	public static SystemTextJsonFormatter CreateFormatter() =>
		new()
		{
			JsonSerializerOptions = { TypeInfoResolver = SourceGenerationContext.Default }
		};
}

/// <summary>
/// Test server shape.
/// </summary>
[JsonRpcContract, GenerateShape(IncludeMethods = MethodShapeFlags.AllPublic)]
#pragma warning disable CA1515 // Consider making public types internal
public partial interface IServer
#pragma warning restore CA1515 // Consider making public types internal
{
	Task<int> Log(string s);
	Task<int> GetLengthOfString(string? s);
	event EventHandler<int> OnLengthObtained;
	event EventHandler<int> OnLogged;
}

internal sealed class Server : IServer
{
	public event EventHandler<int>? OnLogged;
	public event EventHandler<int>? OnLengthObtained;

	public Task<int> Log(string s)
	{
		Console.WriteLine($"Hello from {s} running as a client for the IpcTestServerLauncher.");
		OnLogged?.Invoke(this, 0);
		return Task.FromResult(0);
	}

	public Task<int> GetLengthOfString(string? s)
	{
		if (s is null)
			return (Task<int>)Task.FromException(new ArgumentNullException(nameof(s), "String cannot be null."));

		int len = s.Length;
		OnLengthObtained?.Invoke(this, len);
		return Task.FromResult(len);
	}
}
