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

using System;

using OceanApocalypse.Wave.Extensibility.Plugins.SDK.Runtime.States;

using PolyType;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.Extensibility.Plugins.SDK.Runtime;

/// <summary>
/// Represents a proxy for an instance of a plugin.
/// Via said proxy, plugin authors can operate on themselves and make calls to the host.
/// Plugin disconnects via disposal.
/// </summary>
[JsonRpcContract]
[GenerateShape(IncludeMethods = MethodShapeFlags.AllPublic)]
#pragma warning disable CS3027
// Type is not CLS-compliant because base interface is not CLS-compliant
// Justification for disabling:
// Nobody is to actually use polymorphism here regarding the relation between IPluginProxy and IShapeable
// if somebody does that, it's a misuse of waveSDK
public partial interface IPluginProxy : IAsyncDisposable
#pragma warning restore CS3027
{
}
