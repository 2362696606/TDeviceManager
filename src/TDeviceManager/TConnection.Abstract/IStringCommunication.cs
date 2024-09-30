using TCommon;

namespace TConnection.Abstract;

public interface IStringCommunication : ICommunication
{

    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="buffer">写入的字符串</param>
    /// <returns>操作结果</returns>
    void Write(string buffer);

    /// <summary>
    /// 读取缓存中的所有数据并立即返回
    /// </summary>
    /// <returns>读取的字符串</returns>
    string ReadString();

    /// <summary>
    /// 从缓存中长度为<see cref="length"/>的数据
    /// </summary>
    /// <param name="length">长度</param>
    /// <param name="timeout">超时时间</param>
    /// <returns></returns>
    string ReadString(int length, int timeout = 2000);

    /// <summary>
    /// 读取字符串知道收到END标志位或终止符
    /// </summary>
    /// <param name="timeout">读取超时时间</param>
    /// <param name="terminationCharacter">终止符</param>
    /// <returns>读取的字符串</returns>
    string ReadStringToTermination(int timeout = 2000, char terminationCharacter = '\n');

    /// <summary>
    /// 发送数据并在<paramref name="interval"/>ms后读取缓存中的所有数据
    /// </summary>
    /// <param name="buffer">待发送数据</param>
    /// <param name="interval">时间间隔</param>
    /// <returns>读取到的数据</returns>
    string SendAndReceivedString(string buffer, int interval);

    /// <summary>
    /// 发送<paramref name="buffer"/>并读取长度<paramref name="length"/>的数据
    /// </summary>
    /// <param name="buffer">待发送数据</param>
    /// <param name="length">读取长度</param>
    /// <param name="timeout">超时时间</param>
    /// <returns>读取到的数据</returns>
    string SendAndReceivedStringByLength(string buffer, int length, int timeout = 2000);

    /// <summary>
    /// 发送字符串并等待收到回复,接收数据到END标志位或终止符
    /// </summary>
    /// <param name="buffer">写入的字符串</param>
    /// <param name="timeout">超时时间</param>
    /// <param name="terminationCharacter"></param>
    /// <returns>读取的字符串</returns>
    string SendAndReceivedStringToTermination(string buffer, int timeout = 2000, char terminationCharacter = '\n');
}