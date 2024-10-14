namespace TDevice.Connection.Zlg;

public class ZlgCloudCanBox:ZlgCanBoxBase
{
    public ZlgCloudCanBox(string httpAddress, int httpPort, string mqttAddress, int mqttPort, string userName,
        string password, int deviceIndex)
    {
        HttpAddress = httpAddress;
        HttpPort = httpPort;
        MqttAddress = mqttAddress;
        MqttPort = mqttPort;
        UserName = userName;
        Password = password;
        DeviceIndex = deviceIndex;
    }

    public override ZlgDeviceType DeviceType { get; } = ZlgDeviceType.ZCAN_CLOUD;
    public override int DeviceIndex { get; }
    /// <summary>
    /// http地址
    /// </summary>
    public string HttpAddress { get; }
    /// <summary>
    /// Http端口
    /// </summary>
    public int HttpPort { get; }
    /// <summary>
    /// mqtt地址
    /// </summary>
    public string MqttAddress { get; }
    /// <summary>
    /// Mqtt端口
    /// </summary>
    public int MqttPort { get; }
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; }
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; }

    public override void OpenDevice()
    {
        ZlgMethod.ZCLOUD_SetServerInfo(HttpAddress, (ushort)HttpPort, MqttAddress, (ushort)MqttPort);
        var connectRet = ZlgMethod.ZCLOUD_ConnectServer(UserName, Password);
        switch (connectRet)
        {
            case 1:
                throw new ArgumentException("连接服务器失败");
            case 2:
                throw new ArgumentException("Http错误");
            case 3:
                throw new ArgumentException("登录信息错误");
            case 4:
                throw new ArgumentException("Mqtt连接错误");
        }
        base.OpenDevice();
    }

    public override void CloseDevice()
    {
        ZlgMethod.ZCLOUD_DisconnectServer();
        base.CloseDevice();
    }
}