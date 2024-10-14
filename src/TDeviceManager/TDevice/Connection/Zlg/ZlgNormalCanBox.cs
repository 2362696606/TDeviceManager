namespace TDevice.Connection.Zlg;

public class ZlgNormalCanBox(ZlgDeviceType deviceType, int deviceIndex) : ZlgCanBoxBase
{
    public override ZlgDeviceType DeviceType { get; } = deviceType;
    public override int DeviceIndex { get; } = deviceIndex;
}