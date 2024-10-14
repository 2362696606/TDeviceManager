using System.Runtime.InteropServices;

namespace TDevice.Connection.Zlg;

[StructLayout(LayoutKind.Sequential)]
public struct ZCan
{
    public uint acc_code;
    public uint acc_mask;
    public uint reserved;
    public byte filter;
    public byte timing0;
    public byte timing1;
    public byte mode;
};