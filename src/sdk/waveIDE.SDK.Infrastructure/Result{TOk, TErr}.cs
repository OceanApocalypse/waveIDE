using System.Diagnostics.CodeAnalysis;

namespace OceanApocalypse.Wave.SDK;

/// <summary>
/// A result is a 3-state data structure that wraps operations where regular
/// exception handling would be too expensive to handle.
/// The allowed states are: empty (default), successful and failed.
/// </summary>
/// <typeparam name="TOk">The type of value when the result is successful.</typeparam>
/// <typeparam name="TErr">The type of error when the result is not successful.</typeparam>
public readonly struct Result<TOk, TErr>
{
	private readonly TOk? value = default;
	private readonly TErr? error = default;
	private readonly bool successful;

	/// <summary>
	/// Indicates whether the value is not null and actually matches a successful operation.
	/// </summary>
	[MemberNotNullWhen(true, nameof(value))]
	public readonly bool IsSuccessful => !IsEmpty && successful;

	/// <summary>
	/// Indicates whether the error is not null and actually matches a failed operation.
	/// </summary>
	[MemberNotNullWhen(true, nameof(error))]
	public readonly bool IsError => !IsEmpty && !successful;

	/// <summary>
	/// Indicates whether the result is empty.
	/// </summary>
	public readonly bool IsEmpty => value is null && error is null;

	/// <summary>
	/// Initializes an empty <see cref="Result{TOk, TErr}"/> instance.
	/// This instance cannot be modified.
	/// </summary>
	public Result() => successful = false;

	internal Result(TOk value)
	{
		this.value = value;
		successful = true;
	}

	internal Result(TErr error)
	{
		this.error = error;
		successful = false;
	}
}
