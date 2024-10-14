// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using System.ComponentModel;
// ReSharper disable CommentTypo

namespace TDevice.Connection.Zlg;

public enum ZlgDeviceType
{
    /// <summary>
    /// USBCAN1
    /// </summary>
    [Description("USBCAN1")]
    [ChannelCount]
    ZCAN_USBCAN1 = 3,
    /// <summary>
    /// USBCAN2
    /// </summary>
    [Description("USBCAN2")]
    [ChannelCount(2)]
    ZCAN_USBCAN2 = 4,
    /// <summary>
    /// PCI_9820I
    /// </summary>
    [Description("PCI_9820I")]
    [ChannelCount(2)]
    ZCAN_PCI9820I = 16,
    /// <summary>
    /// CANET-UDP
    /// </summary>
    [Description("CANET-UDP")]
    [ChannelCount]
    ZCAN_CANETUDP = 12,
    /// <summary>
    /// CANET-TCP
    /// </summary>
    [Description("CANET-TCP")]
    [ChannelCount]
    ZCAN_CANETTCP = 17,
    /// <summary>
    /// CANWIFI-TCP
    /// </summary>
    [Description("CANWIFI-TCP")]
    [ChannelCount]
    ZCAN_CANWIFI_TCP = 25,
    /// <summary>
    /// USBCAN_E_U
    /// </summary>
    [Description("USBCAN_E_U")]
    [ChannelCount]
    ZCAN_USBCAN_E_U = 20,
    /// <summary>
    /// USBCAN_2E_U
    /// </summary>
    [Description("USBCAN_2E_U")]
    [ChannelCount(2)]
    ZCAN_USBCAN_2E_U = 21,
    /// <summary>
    /// USBCAN_4E_U
    /// </summary>
    [Description("USBCAN_4E_U")]
    [ChannelCount(4)]
    ZCAN_USBCAN_4E_U = 31,
    /// <summary>
    /// PCIECANFD-100U
    /// </summary>
    [Description("PCIECANFD-100U")]
    [ChannelCount]
    ZCAN_PCIECANFD_100U = 38,
    /// <summary>
    /// PCIECANFD-200U
    /// </summary>
    [Description("PCIECANFD-200U")]
    [ChannelCount(2)]
    ZCAN_PCIECANFD_200U = 39,
    /// <summary>
    /// PCIECANFD-200U-EX
    /// </summary>
    [Description("PCIECANFD-200U-EX")]
    [ChannelCount(2)]
    ZCAN_PCIECANFD_200U_EX = 62,
    /// <summary>
    /// PCIECANFD-400U
    /// </summary>
    [Description("PCIECANFD-400U")]
    [ChannelCount(4)]
    ZCAN_PCIECANFD_400U = 61,
    /// <summary>
    /// USBCANFD-200U
    /// </summary>
    [Description("USBCANFD-200U")]
    [ChannelCount(2)]
    ZCAN_USBCANFD_200U = 41,
    /// <summary>
    /// USBCANFD-400U
    /// </summary>
    [Description("USBCANFD-400U")]
    [ChannelCount(4)]
    ZCAN_USBCANFD_400U = 76,
    /// <summary>
    /// USBCANFD-100U
    /// </summary>
    [Description("USBCANFD-100U")]
    [ChannelCount]
    ZCAN_USBCANFD_100U = 42,
    /// <summary>
    /// USBCANFD-MNI
    /// </summary>
    [Description("USBCANFD-MNI")]
    [ChannelCount]
    ZCAN_USBCANFD_MINI = 43,
    /// <summary>
    /// USBCANFD-800U
    /// </summary>
    [Description("USBCANFD-800U")]
    [ChannelCount(8)]
    ZCAN_USBCANFD_800U = 59,
    /// <summary>
    /// CLOUD
    /// </summary>
    [Description("CLOUD")]
    [ChannelCount(1)]
    ZCAN_CLOUD = 46,
    /// <summary>
    /// CANFDNET-200U-TCP
    /// </summary>
    [Description("CANFDNET-200U-TCP")]
    [ChannelCount(2)]
    ZCAN_CANFDNET_200U_TCP = 48,
    /// <summary>
    /// CANFDNET-200U-UDP
    /// </summary>
    [Description("CANFDNET-200U-UDP")]
    [ChannelCount(2)]
    ZCAN_CANFDNET_200U_UDP = 49,
    /// <summary>
    /// CANFDNET-400U-TCP
    /// </summary>
    [Description("CANFDNET-400U-TCP")]
    [ChannelCount(4)]
    ZCAN_CANFDNET_400U_TCP = 52,
    /// <summary>
    /// CANFDNET-400U-UDP
    /// </summary>
    [Description("CANFDNET-400U-UDP")]
    [ChannelCount(4)]
    ZCAN_CANFDNET_400U_UDP = 53,
    /// <summary>
    /// CANFDNET-800U-TCP
    /// </summary>
    [Description("CANFDNET-800U-TCP")]
    [ChannelCount(8)]
    ZCAN_CANFDNET_800U_TCP = 57,
    /// <summary>
    /// CANFDNET-800U-UDP
    /// </summary>
    [Description("CANFDNET-800U-UDP")]
    [ChannelCount(8)]
    ZCAN_CANFDNET_800U_UDP = 58
}