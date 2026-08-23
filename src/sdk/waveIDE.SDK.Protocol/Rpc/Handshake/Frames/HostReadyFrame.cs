using Nerdbank.MessagePack;

namespace OceanApocalypse.Wave.SDK.Protocol.Rpc.Handshake.Frames;

public sealed record HostReadyFrame(
    [property: Key(0)] string HostVersion,
    [property: Key(1)] string ProtocolVersion
);
