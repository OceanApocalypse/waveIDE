using Nerdbank.MessagePack;

namespace OceanApocalypse.Wave.SDK.Protocol.Rpc.Handshake.Frames;

public sealed record PluginReadyFrame(
    [property: Key(0)] string PluginId,
    [property: Key(1)] string PluginVersion
);

public sealed record PluginReadyAck(
    [property: Key(0)] bool WasAccepted,
    [property: Key(1)] string? RejectionReason
);
