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

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// IDE0046: Convert to conditional expression
[assembly: SuppressMessage("Style", "IDE0046", Justification = "The conditional expression makes the code not obvious.", Scope = "member", Target = "~M:OceanApocalypse.Wave.Extensibility.Operations.Result`2.GetHashCode~System.Int32")]
[assembly: SuppressMessage("Style", "IDE0046", Justification = "The conditional expression makes the code not obvious.", Scope = "member", Target = "~M:OceanApocalypse.Wave.Extensibility.Operations.Result`2.Equals(OceanApocalypse.Wave.Extensibility.Operations.Result{`0,`1})~System.Boolean")]
