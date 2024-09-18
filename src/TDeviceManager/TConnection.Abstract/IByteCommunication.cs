using TCommon;

namespace TConnection.Abstract;

public interface IByteCommunication
{
    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="buffer">写入的byte数组</param>
    /// <returns>操作结果</returns>
    void Write(byte[] buffer);

    /// <summary>
    /// 读取缓存中的所有数据
    /// </summary>
    /// <returns>读取的<see cref="byte"/>[]</returns>
    byte[] ReadBytes(int timeout = 2000);

    /// <summary>
    /// 读取数据直到收到END标志位或终止符
    /// </summary>
    /// <param name="timeout">读取超时时间</param>
    /// <param name="terminationCharacter">终止符</param>
    /// <returns>读取的<see cref="byte"/>[]</returns>
    byte[] ReadBytesToTermination(int timeout = 2000, byte terminationCharacter = 0x0A);


    /// <summary>
    /// 发送数据并等待收到回复，接收数据到END标志位或终止符
    /// </summary>
    /// <param name="buffer">数据</param>
    /// <param name="timeout">读取超时时间</param>
    /// <param name="terminationCharacter">终止符</param>
    /// <returns>读取的<see cref="byte"/>[]</returns>
    byte[] SendAndReceivedBytesToTermination(byte[] buffer, int timeout = 2000, byte terminationCharacter = 0x0A);
}