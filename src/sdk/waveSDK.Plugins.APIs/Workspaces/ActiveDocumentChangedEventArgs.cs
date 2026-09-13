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

namespace OceanApocalypse.Wave.SDK.Plugins.APIs.Workspaces;

/// <summary>
/// Arguments for evens that are triggered when the document focus changes.
/// </summary>
public class ActiveDocumentChangedEventArgs : EventArgs
{
    /// <summary>
    /// The document ID of the previously active document.
    /// </summary>
    public int PreviousDocId { get; set; }

    /// <summary>
    /// The document ID of the currently active document.
    /// </summary>
    public int CurrentDocId { get; set; }

    /// <summary>
    /// Whether the current document is read-only.
    /// </summary>
    public bool IsCurrentReadOnly { get; set; }
}
