namespace TConnection.Abstract.EventArgs;

public class ByteReceivedArgs:System.EventArgs
{
    /// <summary>
    /// 接收到的数据
    /// </summary>
    public byte[] ReceivedData { get; set; } = [];

}