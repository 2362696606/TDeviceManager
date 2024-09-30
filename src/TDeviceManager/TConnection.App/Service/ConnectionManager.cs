using System;
using System.Threading;
using TConnection.Abstract;
using TConnection.Abstract.Models;
using TConnection.App.ServiceAbstract;

namespace TConnection.App.Service;

public class ConnectionManager(
    IConnectionRoot connectionRoot,
    IConnectionFactoryRoot factoryRoot,
    IConnectionConfigRecorder recorder)
    : IConnectionManager
{
    private readonly IConnectionRoot _connectionRoot = connectionRoot;
    private readonly IConnectionFactoryRoot _factoryRoot = factoryRoot;
    private readonly IConnectionConfigRecorder _recorder = recorder;

    public IReadOnlyDictionary<string, string> GetConnectionProperty(string connectionName)
    {
        var connection = _connectionRoot.GetConnection(connectionName);
        var property = new Dictionary<string, string>();
        property["ConnectionName"] = connectionName;
        property["ConnectionType"] = connection.GetType().ToString();
        property["IsConnected"] = connection.IsConnected.ToString();
        GetNetConnectionProperty(connection,ref property);
        GetSerialConnectionProperty(connection, ref property);
        return property;
    }

    public void AddConnection(ConnectionConfig config)
    {
        var connection = _factoryRoot.CreateConnection(config.ConnectionType, config.Paras, config.IsAutoConnect);
        _connectionRoot.AddConnection(config.ConnectionName,connection);
        _recorder.AddConfig(config);
        _recorder.SaveConfigs();
    }

    public void RemoveConnection(string connectionName)
    {
        _connectionRoot.RemoveConnection(connectionName);
        _recorder.RemoveConfig(connectionName);
        _recorder.SaveConfigs();
    }

    public void EditConnection(ConnectionConfig config)
    {
        RemoveConnection(config.ConnectionName);
        AddConnection(config);
    }

    #region 代理IByteCommunication

    public void Write(string connectionName, byte[] buffer)
    {
        var communication = GetCommunication<IByteCommunication>(connectionName);
        communication.Write(buffer);
    }

    public byte[] ReadBytes(string connectionName)
    {
        var communication = GetCommunication<IByteCommunication>(connectionName);
        return communication.ReadBytes();
    }

    public byte[] ReadBytes(string connectionName, int length, int timeout = 2000)
    {
        var communication = GetCommunication<IByteCommunication>(connectionName);
        return communication.ReadBytes(length, timeout);
    }

    public byte[] ReadBytesToTermination(string connectionName, int timeout = 2000, byte terminationCharacter = 10)
    {
        var communication = GetCommunication<IByteCommunication>(connectionName);
        return communication.ReadBytesToTermination(timeout, terminationCharacter);
    }

    public byte[] SendAndReceivedBytes(string connectionName, byte[] buffer, int interval)
    {
        var communication = GetCommunication<IByteCommunication>(connectionName);
        return communication.SendAndReceivedBytes(buffer, interval);
    }

    public byte[] SendAndReceivedBytesByLength(string connectionName, byte[] buffer, int length, int timeout = 2000)
    {
        var communication = GetCommunication<IByteCommunication>(connectionName);
        return communication.SendAndReceivedBytesByLength(buffer, length, timeout);
    }

    public byte[] SendAndReceivedBytesToTermination(string connectionName, byte[] buffer, int timeout = 2000,
        byte terminationCharacter = 10)
    {
        var communication = GetCommunication<IByteCommunication>(connectionName);
        return communication.SendAndReceivedBytesToTermination(buffer, timeout, terminationCharacter);
    }

    #endregion

    #region 代理IStringCommunication

    public void Write(string connectionName, string buffer)
    {
        var communication = GetCommunication<IStringCommunication>(connectionName);
        communication.Write(buffer);
    }

    public string ReadString(string connectionName)
    {
        var communication = GetCommunication<IStringCommunication>(connectionName);
        return communication.ReadString();
    }

    public string ReadString(string connectionName, int length, int timeout = 2000)
    {
        var communication = GetCommunication<IStringCommunication>(connectionName);
        return communication.ReadString(length, timeout);
    }

    public string ReadStringToTermination(string connectionName, int timeout = 2000, char terminationCharacter = '\n')
    {
        var communication = GetCommunication<IStringCommunication>(connectionName);
        return communication.ReadStringToTermination(timeout, terminationCharacter);
    }

    public string SendAndReceivedString(string connectionName, string buffer, int interval)
    {
        var communication = GetCommunication<IStringCommunication>(connectionName);
        return communication.SendAndReceivedString(buffer, interval);
    }

    public string SendAndReceivedStringByLength(string connectionName, string buffer, int length, int timeout = 2000)
    {
        var communication = GetCommunication<IStringCommunication>(connectionName);
        return communication.SendAndReceivedStringByLength(buffer, length, timeout);
    }

    public string SendAndReceivedStringToTermination(string connectionName, string buffer, int timeout = 2000,
        char terminationCharacter = '\n')
    {
        var communication = GetCommunication<IStringCommunication>(connectionName);
        return communication.SendAndReceivedStringToTermination(buffer, timeout, terminationCharacter);
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 获取Net连接相关属性
    /// </summary>
    /// <param name="connection">连接对象</param>
    /// <param name="property">属性字典</param>
    private void GetNetConnectionProperty(IConnection connection, ref Dictionary<string, string> property)
    {
        if (connection is INetConnection netConnection)
        {
            property["Ip"] = netConnection.IpAddress;
            property["Port"] = netConnection.Port.ToString();
        }
    }
    /// <summary>
    /// 获取串口连接相关属性
    /// </summary>
    /// <param name="connection">连接对象</param>
    /// <param name="property">属性字典</param>
    private void GetSerialConnectionProperty(IConnection connection, ref Dictionary<string, string> property)
    {
        if (connection is ISerialConnection serialConnection)
        {
            property["PortName"] = serialConnection.PortName;
            property["BaudRate"] = serialConnection.BaudRate.ToString();
        }
    }

    /// <summary>
    /// 通过连接名获取对应通讯类型对象
    /// </summary>
    /// <typeparam name="T">通讯类型</typeparam>
    /// <param name="connectionName">连接名</param>
    /// <returns>通讯对象</returns>
    /// <exception cref="InvalidOperationException">类型不匹配</exception>
    private T GetCommunication<T>(string connectionName) where T : ICommunication
    {
        var connection = _connectionRoot.GetConnection(connectionName);
        if (connection is T convertCommunication)
        {
            return convertCommunication;
        }
        else
        {
            throw new InvalidOperationException($"连接{connectionName}无法以接口{nameof(IByteCommunication)}方式通讯");
        }
    }

    #endregion
}