using System;

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
