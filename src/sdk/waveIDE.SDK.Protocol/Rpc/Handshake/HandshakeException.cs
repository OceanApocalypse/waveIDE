using System;

namespace OceanApocalypse.Wave.SDK.Protocol.Rpc.Handshake;

public class HandshakeException : Exception
{
    public HandshakeException() : base() { }

    public HandshakeException(string message) : base(message) { }

    public HandshakeException(string message, Exception inner) : base(message, inner) { }
}
