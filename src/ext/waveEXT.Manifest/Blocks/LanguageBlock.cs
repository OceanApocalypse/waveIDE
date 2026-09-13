/*
 * Licensed to Ocean Apocalypse under one or more contributor
 * license agreements. See the NOTICE file distributed
 * with this work for additional information regarding
 * copyright ownership. Ocean Apocalypse licenses this file to
 * you under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance
 * with the License. You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations
 * under the License.    
*/

using System.Collections.Generic;

namespace OceanApocalypse.Wave.Extensibility.Manifest.Blocks;

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
