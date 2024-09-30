namespace TDevice.Connection;

public class ZlgCanChannel(ZlgCanBox canBox)
{
    public ZlgCanBox CanBox { get; set; } = canBox;
}