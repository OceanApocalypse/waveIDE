using System.Collections.Generic;

namespace OceanApocalypse.Wave.SDK.Infrastructure.Manifests.Blocks;

/// <summary>
/// A block that represents the declaration of support for a programming language.
/// </summary>
/// <remarks>
/// This is an object for TOML serialization and deserialization, thus
/// not being used to define language support in plugins.
/// </remarks>
/// <param name="Id">The language ID.</param>
/// <param name="DisplayName">The language's friendly/display name.</param>
/// <param name="FileExtensions">
/// A read-only list of file extensions that match this language.
/// It's possible to also define complex extensions such as <c>foo.bar</c>,
/// which is great for when enhanced support for a specific use case of the file extension
/// is necessary.
/// </param>
/// <param name="MimeType">The MIME type of the language. Usually, it's <c>application/{Id}</c>.</param>
/// <param name="LspCapability">When set to <c>true</c>, marks the language as supporting LSP.</param>
/// <param name="FormattingCapability">When set to <c>true</c>, marks the language as supporting formatters.</param>
/// <param name="DebuggingCapability">When set to <c>true</c>, marks the language as supporting debuggers.</param>
public record LanguageBlock(
	string Id,
	string DisplayName,
	IReadOnlyList<string> FileExtensions,
	string MimeType,
	bool LspCapability,
	bool FormattingCapability,
	bool DebuggingCapability
);
