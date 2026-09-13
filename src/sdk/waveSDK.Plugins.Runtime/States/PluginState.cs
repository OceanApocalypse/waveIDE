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

namespace OceanApocalypse.Wave.SDK.Plugins.Runtime.States;

/// <summary>
/// Represents the possible states for a plugin.
/// </summary>
public enum PluginState
{
    /// <summary>
    /// The plugin's state is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// The plugin is connected, but not ready.
    /// </summary>
    Connected,

    /// <summary>
    /// The plugin is connected and ready, but hasn't registered everything yet.
    /// </summary>
    Ready,

    /// <summary>
    /// The plugin is connected, ready and has registered everything.
    /// </summary>
    Initialized,

    /// <summary>
    /// The plugin is not connected.
    /// </summary>
    Disconnected
}

