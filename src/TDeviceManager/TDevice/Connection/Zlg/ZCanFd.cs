using System.Runtime.InteropServices;
// ReSharper disable IdentifierTypo

namespace TDevice.Connection.Zlg;

[StructLayout(LayoutKind.Sequential)]
public struct ZCanfd
{
    public uint acc_code;
    public uint acc_mask;
    public uint abit_timing;
    public uint dbit_timing;
    public uint brp;
    public byte filter;
    public byte mode;
    public UInt16 pad;
    public uint reserved;

}