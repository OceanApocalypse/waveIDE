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
/// Arguments for events that are triggered when documents are edited.
/// </summary>
public class DocumentEditedEventArgs : EventArgs
{
    /// <summary>
    /// The ID of the edited document.
    /// </summary>
    public Guid DocId { get; set; }

    // todo: add actual args for what was edited and where
}
