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

using System.Text.Json.Serialization;

namespace OceanApocalypse.Wave.Extensibility.Protocol.AotCompatibility;

/// <summary>
/// A JSON serializer context that uses source generation to avoid
/// trimming-related problems and maintain full compatibility with
/// NativeAOT.
/// </summary>
/// <remarks>
/// Serializable types are: <see cref="System.Int32"/> and <see cref="System.String"/>.
/// </remarks>
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(string))]
public sealed partial class NativeJsonSourceGenerationContext : JsonSerializerContext;
