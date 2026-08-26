using System;
using System.Diagnostics.CodeAnalysis;

namespace OceanApocalypse.Wave.SDK;

/// <summary>
/// Factory class for <see cref="Result{TOk, TErr}"/> instances.
/// </summary>
public static class Result
{
	/// <summary>
	/// Creates a new failed result.
	/// </summary>
	/// <typeparam name="TOk">The type of the expected success value.</typeparam>
	/// <typeparam name="TErr">The type of the actual error value.</typeparam>
	/// <param name="error">The error value.</param>
	/// <returns>The result, wrapping the given error.</returns>
	public static Result<TOk, TErr> Fail<TOk, TErr>(TErr error) => new(error);

	/// <summary>
	/// Creates a new failed result from an <see cref="Exception"/>.
	/// </summary>
	/// <typeparam name="TOk">The type of the expected success value.</typeparam>
	/// <param name="exception">The exception itself.</param>
	/// <returns>The result, wrapping the given exception.</returns>
	public static Result<TOk, Exception> Fail<TOk>(Exception exception) => new(exception);

	/// <summary>
	/// Creates a new empty result.
	/// </summary>
	/// <typeparam name="TOk">The type of the expected success value.</typeparam>
	/// <typeparam name="TErr">The type of the expected error value.</typeparam>
	/// <returns>The empty result.</returns>
	public static Result<TOk, TErr> Empty<TOk, TErr>() => new();

	/// <summary>
	/// Creates a new successful result.
	/// </summary>
	/// <typeparam name="TOk">The type of the actual success value.</typeparam>
	/// <typeparam name="TErr">The type of the error value that could happen.</typeparam>
	/// <param name="value">The success value.</param>
	/// <returns>The result, wrapping the given value.</returns>
	public static Result<TOk, TErr> Success<TOk, TErr>(TOk value) => new(value);
}

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
