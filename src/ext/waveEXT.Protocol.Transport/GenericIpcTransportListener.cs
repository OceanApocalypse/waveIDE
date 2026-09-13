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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OceanApocalypse.Wave.Extensibility.Protocol.Transport;

internal class GenericIpcTransportListener(IDisposable baseListener, Func<CancellationToken, Task<Stream>> asyncHandler) : IIpcTransportListener
{
    private bool isDisposed;

    public async Task<Stream> AcceptConnectionAsync(CancellationToken cancellationToken) => await asyncHandler(cancellationToken);

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                baseListener.Dispose();
            }

            isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
