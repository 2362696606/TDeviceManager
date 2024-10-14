using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace TDevice.Connection.Zlg;

public static partial class ZlgMethod
{
    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[]{typeof(CallConvStdcall)})]
    internal static partial IntPtr ZCAN_OpenDevice(uint device_type, uint device_index, uint reserved);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_CloseDevice(IntPtr device_handle);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    // pInitConfig -> ZCAN_CHANNEL_INIT_CONFIG
    internal static partial IntPtr ZCAN_InitCAN(IntPtr device_handle, uint can_index, IntPtr pInitConfig);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_SetValue(IntPtr device_handle, string path, byte[] value);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_SetValue(IntPtr device_handle, string path, IntPtr value);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial IntPtr ZCAN_GetValue(IntPtr device_handle, string path);


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_StartCAN(IntPtr channel_handle);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_ResetCAN(IntPtr channel_handle);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_ClearBuffer(IntPtr channel_handle);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    // pTransmit -> ZCAN_Transmit_Data
    internal static partial uint ZCAN_Transmit(IntPtr channel_handle, IntPtr pTransmit, uint len);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    // pTransmit -> ZCAN_TransmitFD_Data
    internal static partial uint ZCAN_TransmitFD(IntPtr channel_handle, IntPtr pTransmit, uint len);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    // pTransmit -> ZCAN_TransmitFD_Data
    internal static partial uint ZCAN_TransmitData(IntPtr device_handle, IntPtr pTransmit, uint len);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_GetReceiveNum(IntPtr channel_handle, byte type);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_Receive(IntPtr channel_handle, IntPtr data, uint len, int wait_time = -1);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_ReceiveFD(IntPtr channel_handle, IntPtr data, uint len, int wait_time = -1);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_ReceiveData(IntPtr device_handle, IntPtr data, uint len, int wait_time = -1);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    // pErrInfo -> ZCAN_CHANNEL_ERROR_INFO
    internal static partial uint ZCAN_ReadChannelErrInfo(IntPtr channel_handle, IntPtr pErrInfo);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial IntPtr GetIProperty(IntPtr device_handle);

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ZCLOUD_IsConnected();

    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial void ZCLOUD_SetServerInfo(string httpAddr, ushort httpPort,
        string mqttAddr, ushort mqttPort);


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCLOUD_ConnectServer(string username, string password);


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCLOUD_DisconnectServer();


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial IntPtr ZCLOUD_GetUserData(int updata);

    ///////////LIN相关接口函数////////


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial IntPtr ZCAN_InitLIN(IntPtr device_handle, uint lin_index, IntPtr pLINinitConfig);


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_StartLIN(IntPtr channel_handle);


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_ResetLIN(IntPtr channel_handle);


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_TransmitLIN(IntPtr channel_handle, IntPtr pTransmit, uint len);


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_ReceiveLIN(IntPtr channel_handle, IntPtr data, uint len, int wait_time);


    [LibraryImport("zlgcan.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    internal static partial uint ZCAN_SetLINPublish(IntPtr channel_handle, IntPtr data, uint nPublishCount);  //注册响应报文
}