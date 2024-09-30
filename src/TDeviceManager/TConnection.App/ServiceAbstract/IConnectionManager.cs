using TConnection.Abstract.Models;
using TConnection.App.Models;

namespace TConnection.App.ServiceAbstract;

public interface IConnectionManager
{
    /// <summary>
    /// 获取连接属性
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <returns>连接对象的属性</returns>
    IReadOnlyDictionary<string,string> GetConnectionProperty(string connectionName);
    /// <summary>
    /// 添加连接
    /// </summary>
    /// <param name="config">连接配置</param>
    void AddConnection(ConnectionConfig config);
    /// <summary>
    /// 删除连接
    /// </summary>
    /// <param name="connectionName">连接名</param>
    void RemoveConnection(string connectionName);
    /// <summary>
    /// 修改连接
    /// </summary>
    /// <param name="config"></param>
    void EditConnection(ConnectionConfig config);

    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="buffer">写入的byte数组</param>
    /// <returns>操作结果</returns>
    void Write(string connectionName, byte[] buffer);

    /// <summary>
    /// 读取缓存中的所有数据并立即返回
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="timeout">超时时间</param>
    /// <returns>读取的<see cref="byte"/>[]</returns>
    byte[] ReadBytes(string connectionName);

    /// <summary>
    /// 从缓存中长度为<see cref="length"/>的数据
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="length">长度</param>
    /// <param name="timeout">超时时间</param>
    /// <returns>读取的<see cref="byte"/>[]</returns>
    byte[] ReadBytes(string connectionName, int length, int timeout = 2000);

    /// <summary>
    /// 读取数据直到收到END标志位或终止符
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="timeout">读取超时时间</param>
    /// <param name="terminationCharacter">终止符</param>
    /// <returns>读取的<see cref="byte"/>[]</returns>
    byte[] ReadBytesToTermination(string connectionName, int timeout = 2000, byte terminationCharacter = 0x0A);

    /// <summary>
    /// 发送数据并在<paramref name="interval"/>ms后读取缓存中的所有数据
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="buffer">待发送数据</param>
    /// <param name="interval">时间间隔</param>
    /// <returns>读取到的数据</returns>
    byte[] SendAndReceivedBytes(string connectionName, byte[] buffer, int interval);

    /// <summary>
    /// 发送<paramref name="buffer"/>并读取长度<paramref name="length"/>的数据
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="buffer">待发送数据</param>
    /// <param name="length">读取长度</param>
    /// <param name="timeout">超时时间</param>
    /// <returns>读取到的数据</returns>
    byte[] SendAndReceivedBytesByLength(string connectionName, byte[] buffer, int length, int timeout = 2000);


    /// <summary>
    /// 发送数据并等待收到回复，接收数据到END标志位或终止符
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="buffer">数据</param>
    /// <param name="timeout">读取超时时间</param>
    /// <param name="terminationCharacter">终止符</param>
    /// <returns>读取的<see cref="byte"/>[]</returns>
    byte[] SendAndReceivedBytesToTermination(string connectionName, byte[] buffer, int timeout = 2000, byte terminationCharacter = 0x0A);

    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="buffer">写入的字符串</param>
    /// <returns>操作结果</returns>
    void Write(string connectionName, string buffer);

    /// <summary>
    /// 读取缓存中的所有数据
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <returns>读取的字符串</returns>
    string ReadString(string connectionName);

    /// <summary>
    /// 从缓存中长度为<see cref="length"/>的数据
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="length">长度</param>
    /// <param name="timeout">超时时间</param>
    /// <returns></returns>
    string ReadString(string connectionName, int length, int timeout = 2000);

    /// <summary>
    /// 读取字符串知道收到END标志位或终止符
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="timeout">读取超时时间</param>
    /// <param name="terminationCharacter">终止符</param>
    /// <returns>读取的字符串</returns>
    string ReadStringToTermination(string connectionName, int timeout = 2000, char terminationCharacter = '\n');

    /// <summary>
    /// 发送数据并在<paramref name="interval"/>ms后读取缓存中的所有数据
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="buffer">待发送数据</param>
    /// <param name="interval">时间间隔</param>
    /// <returns>读取到的数据</returns>
    string SendAndReceivedString(string connectionName, string buffer, int interval);

    /// <summary>
    /// 发送<paramref name="buffer"/>并读取长度<paramref name="length"/>的数据
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="buffer">待发送数据</param>
    /// <param name="length">读取长度</param>
    /// <param name="timeout">超时时间</param>
    /// <returns>读取到的数据</returns>
    string SendAndReceivedStringByLength(string connectionName, string buffer, int length, int timeout = 2000);

    /// <summary>
    /// 发送字符串并等待收到回复,接收数据到END标志位或终止符
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="buffer">写入的字符串</param>
    /// <param name="timeout">超时时间</param>
    /// <param name="terminationCharacter"></param>
    /// <returns>读取的字符串</returns>
    string SendAndReceivedStringToTermination(string connectionName, string buffer, int timeout = 2000, char terminationCharacter = '\n');
}