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

using System.Globalization;

namespace OceanApocalypse.Wave.Extensibility.Plugins.Manifest.Blocks;

/// <summary>
/// A representation of the declaration of a locale.
/// </summary>
/// <remarks>
/// This is an object for JSON serialization and deserialization, thus
/// not being used to define locales in plugins.
/// </remarks>
/// <param name="IsoName">
/// The ISO 639-1 or ISO 639-3 name of the locale.
/// (See <see cref="CultureInfo.TwoLetterISOLanguageName"/>.)
/// </param>
/// <param name="CultureName">
/// The name followed by the associated country code. It's either
/// "{<see cref="IsoName"/>}-{CountryCode}" or the region code.
/// (See <see cref="CultureInfo.Name"/>.)
/// </param>
/// <param name="FilePath">The path to the file that contains the actual locale data.</param>
public record LocaleBlock(
    string IsoName,
    string CultureName,
    string FilePath
);
