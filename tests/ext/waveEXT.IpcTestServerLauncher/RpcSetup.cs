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
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using PolyType;

using StreamJsonRpc;

namespace OceanApocalypse.Wave.Extensibility.Tests.IpcLauncher.Servers;

/// <summary>
/// Test server shape.
/// </summary>
[JsonRpcContract, GenerateShape(IncludeMethods = MethodShapeFlags.AllPublic)]
[SuppressMessage("Maintainability", "CA1515", Justification = "Used for testing purposes.")]
[SuppressMessage("Design", "CA1003", Justification = "Used for testing purposes.")]
public partial interface IServer
{
    /// <summary>
    /// Logs a message.
    /// </summary>
    /// <param name="s">The message.</param>
    Task Log(string s);

    /// <summary>
    /// Returns the length of a string.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns>The string's length.</returns>
    Task<int> GetLengthOfString(string? s);

    /// <summary>
    /// Event triggered when somebody calls <see cref="GetLengthOfString(String?)"/>.
    /// </summary>
    event EventHandler<int> OnLengthObtained;

    /// <summary>
    /// Event triggered when somebody calls <see cref="Log(String)"/>.
    /// </summary>
    event EventHandler<int> OnLogged;
}

internal sealed class Server : IServer
{
    public event EventHandler<int>? OnLogged;
    public event EventHandler<int>? OnLengthObtained;

    public Task Log(string s)
    {
        Console.WriteLine($"Hello from {s} running as a client for the IpcTestServerLauncher.");
        OnLogged?.Invoke(this, 0);
        return Task.FromResult(0);
    }

    public Task<int> GetLengthOfString(string? s)
    {
        if (s is null)
            return (Task<int>)Task.FromException(new ArgumentNullException(nameof(s), "String cannot be null."));

        int len = s.Length;
        OnLengthObtained?.Invoke(this, len);
        return Task.FromResult(len);
    }
}
