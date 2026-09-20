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

namespace OceanApocalypse.Wave.Extensibility.Plugins.Manifest.Blocks;

/// <summary>
/// A block that represents the declaration of the necessary permissions for the plugin
/// to function correctly.
/// </summary>
/// <remarks>
/// This is an object for JSON serialization and deserialization, thus
/// not being used to define permissions for plugins.
/// </remarks>
/// <param name="Scopes">A read-only list of the scopes the plugin needs access to.</param>
/// <param name="LoadDynamically">
/// When set to <c>true</c>, the plugin will be allowed to dynamically request intents at runtime.
/// Setting this to <c>true</c> does not mean the intents will be automatically granted, as the host
/// has the ultimate saying in what happens.
/// </param>
public record PermissionBlock(
    IReadOnlyList<string> Scopes,
    bool LoadDynamically
);
