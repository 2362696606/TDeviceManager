using TDevice.Connection.Zlg;

namespace TDevice.Extensions;

public static class ZlgCanExtension
{
    /// <summary>
    /// 判断当前是否是Net类型Can盒
    /// </summary>
    /// <param name="type"><see cref="ZlgDeviceType"/>值</param>
    /// <returns>
    /// <para>true:是Net类型Can盒;</para>
    /// <para>false:不是Net类型Can盒</para>
    /// </returns>
    public static bool IsNetDevice(this ZlgDeviceType type)
    {
        var isNetDevice = type == ZlgDeviceType.ZCAN_CANETTCP ||
                          type == ZlgDeviceType.ZCAN_CANETUDP ||
                          type == ZlgDeviceType.ZCAN_CANWIFI_TCP ||
                          type == ZlgDeviceType.ZCAN_CANFDNET_400U_TCP ||
                          type == ZlgDeviceType.ZCAN_CANFDNET_400U_UDP ||
                          type == ZlgDeviceType.ZCAN_CANFDNET_200U_TCP ||
                          type == ZlgDeviceType.ZCAN_CANFDNET_200U_UDP ||
                          type == ZlgDeviceType.ZCAN_CANFDNET_800U_TCP ||
                          type == ZlgDeviceType.ZCAN_CANFDNET_800U_UDP;
        return isNetDevice;
    }

    /// <summary>
    /// 判断当前是否是CanFdNet类型Can盒
    /// </summary>
    /// <param name="type"><see cref="ZlgDeviceType"/>值</param>
    /// <returns>
    /// <para>true:是CanFdNet类型Can盒;</para>
    /// <para>false:不是CanFdNet类型Can盒</para>
    /// </returns>
    public static bool IsCanFdNetDevice(this ZlgDeviceType type)
    {
        var isCanFdNetDevice = type == ZlgDeviceType.ZCAN_CANFDNET_400U_TCP ||
                               type == ZlgDeviceType.ZCAN_CANFDNET_400U_UDP ||
                               type == ZlgDeviceType.ZCAN_CANFDNET_200U_TCP ||
                               type == ZlgDeviceType.ZCAN_CANFDNET_200U_UDP ||
                               type == ZlgDeviceType.ZCAN_CANFDNET_800U_TCP ||
                               type == ZlgDeviceType.ZCAN_CANFDNET_800U_UDP;
        return isCanFdNetDevice;
    }

    /// <summary>
    /// 判断当前是否是PcieCanFd类型Can盒
    /// </summary>
    /// <param name="type"><see cref="ZlgDeviceType"/>值</param>
    /// <returns>
    /// <para>true:是PcieCanFd类型Can盒;</para>
    /// <para>false:不是PcieCanFd类型Can盒</para>
    /// </returns>
    public static bool IsPcieCanFdDevice(this ZlgDeviceType type)
    {
        var isPcieCanFdDevice = type == ZlgDeviceType.ZCAN_PCIECANFD_100U ||
                                type == ZlgDeviceType.ZCAN_PCIECANFD_200U ||
                                type == ZlgDeviceType.ZCAN_PCIECANFD_400U ||
                                type == ZlgDeviceType.ZCAN_PCIECANFD_200U_EX;
        return isPcieCanFdDevice;
    }

    /// <summary>
    /// 判断当前是否是UsbCanFd类型Can盒
    /// </summary>
    /// <param name="type"><see cref="ZlgDeviceType"/>值</param>
    /// <returns>
    /// <para>true:是UsbCanFd类型Can盒;</para>
    /// <para>false:不是UsbCanFd类型Can盒</para>
    /// </returns>
    public static bool IsUsbCanFdDevice(this ZlgDeviceType type)
    {
        var isUsbCanFdDevice = type == ZlgDeviceType.ZCAN_USBCANFD_100U ||
                               type == ZlgDeviceType.ZCAN_USBCANFD_200U ||
                               type == ZlgDeviceType.ZCAN_USBCANFD_400U ||
                               type == ZlgDeviceType.ZCAN_USBCANFD_MINI ||
                               type == ZlgDeviceType.ZCAN_USBCANFD_800U;
        return isUsbCanFdDevice;
    }

    /// <summary>
    /// 判断当前是否是CanFd类型Can盒
    /// </summary>
    /// <param name="type"><see cref="ZlgDeviceType"/>值</param>
    /// <returns>
    /// <para>true:是CanFd类型Can盒;</para>
    /// <para>false:不是CanFd类型Can盒</para>
    /// </returns>
    public static bool IsCanFdDevice(this ZlgDeviceType type)
    {
        var isUsbCanFdDevice = type.IsUsbCanFdDevice();
        var isPcieCanFdDevice = type.IsPcieCanFdDevice();
        return isPcieCanFdDevice || isUsbCanFdDevice;
    }

    /// <summary>
    /// 判断当前是否是Cloud类型Can盒
    /// </summary>
    /// <param name="type"><see cref="ZlgDeviceType"/>值</param>
    /// <returns>
    /// <para>true:是Cloud类型Can盒;</para>
    /// <para>false:不是Cloud类型Can盒</para>
    /// </returns>
    public static bool IsCloudDevice(this ZlgDeviceType type)
    {
        var isCloudDevice = type == ZlgDeviceType.ZCAN_CLOUD;
        return isCloudDevice;
    }


    /// <summary>
    /// 判断当前是否是Tcp类型Can盒
    /// </summary>
    /// <param name="type"><see cref="ZlgDeviceType"/>值</param>
    /// <returns>
    /// <para>true:是Tcp类型Can盒;</para>
    /// <para>false:不是Tcp类型Can盒</para>
    /// </returns>
    public static bool IsTcpDevice(this ZlgDeviceType type)
    {
        var isUsbCanFdDevice = type == ZlgDeviceType.ZCAN_CANETTCP ||
                               type == ZlgDeviceType.ZCAN_CANWIFI_TCP ||
                               type == ZlgDeviceType.ZCAN_CANFDNET_400U_TCP ||
                               type == ZlgDeviceType.ZCAN_CANFDNET_200U_TCP ||
                               type == ZlgDeviceType.ZCAN_CANFDNET_800U_TCP;
        return isUsbCanFdDevice;
    }

    /// <summary>
    /// 判断当前是否是Net类型Can盒
    /// </summary>
    /// <param name="canBox"><see cref="ZlgCanBoxBase"/>对象</param>
    /// <returns>
    /// <para>true:是Net类型Can盒;</para>
    /// <para>false:不是Net类型Can盒</para>
    /// </returns>
    public static bool IsNetDevice(this ZlgCanBoxBase canBox)
    {
        var type = canBox.DeviceType;
        return type.IsNetDevice();

    }

    /// <summary>
    /// 判断当前是否是CanFdNet类型Can盒
    /// </summary>
    /// <param name="canBox"><see cref="ZlgCanBoxBase"/>对象</param>
    /// <returns>
    /// <para>true:是CanFdNet类型Can盒;</para>
    /// <para>false:不是CanFdNet类型Can盒</para>
    /// </returns>
    public static bool IsCanFdNetDevice(this ZlgCanBoxBase canBox)
    {
        var type = canBox.DeviceType;
        return type.IsCanFdNetDevice();

    }

    /// <summary>
    /// 判断当前是否是PcieCanFd类型Can盒
    /// </summary>
    /// <param name="canBox"><see cref="ZlgCanBoxBase"/>对象</param>
    /// <returns>
    /// <para>true:是PcieCanFd类型Can盒;</para>
    /// <para>false:不是PcieCanFd类型Can盒</para>
    /// </returns>
    public static bool IsPcieCanFdDevice(this ZlgCanBoxBase canBox)
    {
        var type = canBox.DeviceType;
        return type.IsPcieCanFdDevice();
    }

    /// <summary>
    /// 判断当前是否是UsbCanFd类型Can盒
    /// </summary>
    /// <param name="canBox"><see cref="ZlgCanBoxBase"/>对象</param>
    /// <returns>
    /// <para>true:是UsbCanFd类型Can盒;</para>
    /// <para>false:不是UsbCanFd类型Can盒</para>
    /// </returns>
    public static bool IsUsbCanFdDevice(this ZlgCanBoxBase canBox)
    {
        var type = canBox.DeviceType;
        return type.IsUsbCanFdDevice();
    }

    /// <summary>
    /// 判断当前是否是CanFd类型Can盒
    /// </summary>
    /// <param name="canBox"><see cref="ZlgCanBoxBase"/>对象</param>
    /// <returns>
    /// <para>true:是CanFd类型Can盒;</para>
    /// <para>false:不是CanFd类型Can盒</para>
    /// </returns>
    public static bool IsCanFdDevice(this ZlgCanBoxBase canBox)
    {
        var type = canBox.DeviceType;
        return type.IsCanFdDevice();
    }

    /// <summary>
    /// 判断当前是否是Cloud类型Can盒
    /// </summary>
    /// <param name="canBox"><see cref="ZlgCanBoxBase"/>对象</param>
    /// <returns>
    /// <para>true:是Cloud类型Can盒;</para>
    /// <para>false:不是Cloud类型Can盒</para>
    /// </returns>
    public static bool IsCloudDevice(this ZlgCanBoxBase canBox)
    {
        var type = canBox.DeviceType;
        return type.IsCloudDevice();
    }

    /// <summary>
    /// 判断当前是否是Tcp类型Can盒
    /// </summary>
    /// <param name="canBox"><see cref="ZlgCanBoxBase"/>对象</param>
    /// <returns>
    /// <para>true:是Tcp类型Can盒;</para>
    /// <para>false:不是Tcp类型Can盒</para>
    /// </returns>
    public static bool IsTcpDevice(this ZlgCanBoxBase canBox)
    {
        var type = canBox.DeviceType;
        return type.IsTcpDevice();
    }
}