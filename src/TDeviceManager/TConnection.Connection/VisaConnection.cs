using NationalInstruments.Visa;
using TConnection.Abstract;

namespace TConnection.Connection;
/// <summary>
/// Visa连接
/// </summary>
public abstract class VisaConnection:IConnection,IByteCommunication, IStringCommunication
{
    /// <summary>
    /// 构造函数
    /// </summary>
    protected VisaConnection()
    {
    }

    #region 字段

    /// <summary>
    /// Visa会话资源
    /// </summary>
    protected MessageBasedSession? Session;

    #endregion


    #region 属性

    /// <summary>
    /// 资源名
    /// </summary>
    public string ResourceName { get; set; } = string.Empty;

    #endregion

    #region 释放资源
    /// <summary>
    /// 用于子类重写Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Session?.Dispose();
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    public virtual bool IsConnected => Session != null;
    public virtual void Connect()
    {
        using var resourceManager = new ResourceManager();
        var visaSession = resourceManager.Open(ResourceName);
        if (visaSession is MessageBasedSession messageBasedSession)
        {
            Session = messageBasedSession;
        }
        else
        {
            visaSession.Dispose();
            throw new InvalidOperationException($"资源{ResourceName}无法创建{nameof(MessageBasedSession)}");
        }
    }

    public virtual void DisConnect()
    {
        Session?.Dispose();
    }

    public virtual void Write(byte[] buffer)
    {
        ArgumentNullException.ThrowIfNull(Session, nameof(Session));
        Session.RawIO.Write(buffer);
    }

    public virtual byte[] ReadBytes(int timeout = 2000)
    {
        ArgumentNullException.ThrowIfNull(Session,nameof(Session));
        Session.TimeoutMilliseconds = timeout;
        var readBufferSize = Session.FormattedIO.ReadBufferSize;
        var bytes = Session.RawIO.Read(readBufferSize);
        return bytes;
    }

    public virtual byte[] ReadBytesToTermination(int timeout = 2000, byte terminationCharacter = 10)
    {
        ArgumentNullException.ThrowIfNull(Session, nameof(Session));
        Session.TimeoutMilliseconds = timeout;
        Session.TerminationCharacter = terminationCharacter;
        var bytes = Session.RawIO.Read();
        return bytes;
    }

    public virtual byte[] SendAndReceivedBytesToTermination(byte[] buffer, int timeout = 2000, byte terminationCharacter = 10)
    {
        Write(buffer);
        return ReadBytesToTermination(timeout, terminationCharacter);
    }

    public virtual void Write(string buffer)
    {
        ArgumentNullException.ThrowIfNull(Session, nameof(Session));
        Session.RawIO.Write(buffer);
    }

    public virtual string ReadString(int timeout = 2000)
    {
        ArgumentNullException.ThrowIfNull(Session, nameof(Session));
        Session.TimeoutMilliseconds = timeout;
        var readBufferSize = Session.FormattedIO.ReadBufferSize;
        var readString = Session.RawIO.ReadString(readBufferSize);
        return readString;
    }

    public virtual string ReadStringToTermination(int timeout = 2000, char terminationCharacter = '\n')
    {
        ArgumentNullException.ThrowIfNull(Session, nameof(Session));
        Session.TimeoutMilliseconds = timeout;
        Session.TerminationCharacter = (byte)terminationCharacter;
        var readString = Session.RawIO.ReadString();
        return readString;
    }

    public virtual string SendAndReceivedStringToTermination(string buffer, int timeout = 2000, char terminationCharacter = '\n')
    {
        Write(buffer);
        return ReadStringToTermination(timeout, terminationCharacter);
    }
}