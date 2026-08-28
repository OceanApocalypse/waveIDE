using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace OceanApocalypse.Wave;

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
public readonly struct Result<TOk, TErr> : IEquatable<Result<TOk, TErr>>
{
	private readonly TOk? value = default;
	private readonly TErr? error = default;
	private readonly bool successful;

	/// <summary>
	/// The result's success status.
	/// </summary>
	/// <remarks>
	/// Make sure to check if this result was successful by using
	/// <see cref="IsSuccessful"/> before blindly using this property.
	/// </remarks>
	public readonly TOk? Value => value;

	/// <summary>
	/// The result's error status.
	/// </summary>
	/// <remarks>
	/// Make sure to check if this result errored out with <see cref="IsError"/>
	/// before blindly using this property.
	/// </remarks>
	public readonly TErr? Error => error;

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

	/// <inheritdoc/>
	/// <remarks>
	/// A result is considered to be equal with:
	/// <list type="bullet">A value of type <typeparamref name="TOk"/> if <see cref="IsSuccessful"/>
	/// is <see langword="true"/> and they're equal.</list>
	/// <list type="bullet">A value of type <typeparamref name="TErr"/> if <see cref="IsError"/>
	/// is <see langword="true"/> and they're equal.</list>
	/// <list type="bullet">Another result if the states match (both are successful, both are empty or both have errors)
	/// and their underlying values are equal under those states (e.g.: if both results are successful, their "ok" values
	/// have to be equal).</list>
	/// </remarks>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj switch
	{
		// ok
		Result<TOk, object> okResult when IsSuccessful && okResult.IsSuccessful => value.Equals(okResult.value),
		TOk ok => Equals(ok),

		// error
		Result<object, TErr> errorResult when IsError && errorResult.IsError => error.Equals(errorResult.error),
		TErr err => Equals(err),

		// empty
		Result<object, object> result when result.IsEmpty => IsEmpty,

		// fallback
		_ => false,
	};

	/// <summary>
	/// Checks if 2 results for the same types are equal.
	/// </summary>
	/// <param name="result">The other result.</param>
	/// <returns>True if they match state and value.</returns>
	public bool Equals(Result<TOk, TErr> result)
	{
		if (IsSuccessful && result.IsSuccessful)
			return value.Equals(result.value);

		if (IsError && result.IsError)
			return error.Equals(result.error);

		return IsEmpty && result.IsEmpty;
	}

	/// <summary>
	/// Checks if this result's success value equals a given value.
	/// For it to happen, <see cref="IsSuccessful"/> must be <see langword="true"/>.
	/// </summary>
	/// <param name="value">The value to check against.</param>
	/// <returns>True if the values are equal.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(TOk value) => IsSuccessful && this.value.Equals(value);

	/// <summary>
	/// Checks if this result's error equals a given error.
	/// For it to happen, <see cref="IsError"/> must be <see langword="true"/>.
	/// </summary>
	/// <param name="error">The error to check against.</param>
	/// <returns>True if the errors are equal.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(TErr error) => IsError && this.error.Equals(error);

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		if (IsSuccessful)
			return value.GetHashCode();

		if (IsError)
			return error.GetHashCode();

		return 0;
	}

	/// <summary>
	/// Checks if 2 results for the same types are equal.
	/// </summary>
	/// <returns>True if they match state and value.</returns>
	public static bool operator ==(Result<TOk, TErr> left, Result<TOk, TErr> right) => left.Equals(right);

	/// <summary>
	/// Checks if 2 results for the same types are different.
	/// </summary>
	/// <returns>True if they don't match state or value.</returns>
	public static bool operator !=(Result<TOk, TErr> left, Result<TOk, TErr> right) => !(left == right);
}
