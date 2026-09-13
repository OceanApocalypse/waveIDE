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
/// A block that represents the declaration of supported and unsupported editors.
/// </summary>
/// <remarks>
/// This is an object for TOML serialization and deserialization, thus
/// not being used to define editor support in plugins.
/// </remarks>
/// <param name="CompatibleWith">
/// A read-only list of editors the plugin has been tested against with a positive outcome.
/// Do not include editors that are "probably" compatible - only the ones who have undergone actual testing.
/// </param>
public record EditorSupportBlock(
    IReadOnlyList<string> CompatibleWith
)
{
    /// <summary>
    /// A read-only list of editors the plugin has been tested against with a negative outcome.
    /// Do not include editors that were not tested against - only the ones who have undergone actual testing.
    /// Default is an empty list.
    /// </summary>
    public IReadOnlyList<string> IncompatibleWith { get; init; } = [];

    /// <summary>
    /// When set to <c>true</c>, any editor not defined in <see cref="CompatibleWith"/> will be considered fully incompatible.
    /// Default is <c>false</c>.
    /// </summary>
    public bool UnspecifiedAreStrictlyIncompatible { get; init; }
}
