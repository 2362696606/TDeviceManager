using System.Runtime.InteropServices;
// ReSharper disable IdentifierTypo

namespace TDevice.Connection.Zlg;

[StructLayout(LayoutKind.Explicit)]
public struct ZCanChannelInitConfig
{
    [FieldOffset(0)]
    public uint can_type; //type:TYPE_CAN TYPE_CANFD

    [FieldOffset(4)]
    public ZCan can;

    [FieldOffset(4)]
    public ZCanfd canfd;
}