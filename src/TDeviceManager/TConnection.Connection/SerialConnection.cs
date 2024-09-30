using System.IO.Ports;
using System.Text;
using TConnection.Abstract;
using TConnection.Abstract.Models;

namespace TConnection.Connection;

public class SerialConnection : IConnection, ISerialConnection, IByteCommunication, IStringCommunication
{
    #region 字段

    private SerialPort _serialPort = new SerialPort();

    #endregion

    #region IConnection实现

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this._serialPort.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public bool IsConnected => _serialPort.IsOpen;
    public void Connect()
    {
        _serialPort.Open();
    }

    public void DisConnect()
    {
        _serialPort.Close();
    }

    #endregion

    #region ISerialConnection实现

    public string PortName
    {
        get => _serialPort.PortName;
        set => _serialPort.PortName = value;
    }

    public int BaudRate
    {
        get => _serialPort.BaudRate;
        set => _serialPort.BaudRate = value;
    }

    public short DataBits
    {
        get => (short)_serialPort.DataBits;
        set => _serialPort.DataBits = value;
    }

    public SerialStopBits StopBits
    {
        get => (SerialStopBits)_serialPort.StopBits;
        set => _serialPort.StopBits = (StopBits)value;
    }

    public SerialParity Parity
    {
        get => (SerialParity)_serialPort.Parity;
        set => _serialPort.Parity = (Parity)value;
    }

    #endregion

    #region IByteCommunication实现

    public void Write(byte[] buffer)
    {
        _serialPort.Write(buffer, 0, buffer.Length);
    }

    public byte[] ReadBytes()
    {
        var existing = _serialPort.ReadExisting();
        var bytes = !string.IsNullOrEmpty(existing) ? Encoding.ASCII.GetBytes(existing) : Array.Empty<byte>();
        return bytes;
    }

    public byte[] ReadBytes(int length, int timeout)
    {
        _serialPort.ReadTimeout = timeout;
        var buffer = new byte[length];
        var count = _serialPort.Read(buffer, 0, length);
        return buffer;
    }

    public byte[] ReadBytesToTermination(int timeout = 2000, byte terminationCharacter = 10)
    {
        _serialPort.ReadTimeout = timeout;
        var termChar = Encoding.ASCII.GetString(new[] { terminationCharacter });
        var readTo = _serialPort.ReadTo(termChar);
        var bytes = Encoding.ASCII.GetBytes(readTo);
        return bytes;
    }

    public byte[] SendAndReceivedBytes(byte[] buffer, int interval)
    {
        lock (_serialPort)
        {
            Write(buffer);
            Thread.Sleep(interval);
            return ReadBytes();
        }
    }

    public byte[] SendAndReceivedBytesByLength(byte[] buffer, int length, int timeout = 2000)
    {
        lock (_serialPort)
        {
            Write(buffer);
            return ReadBytes(length, timeout);
        }
    }

    public byte[] SendAndReceivedBytesToTermination(byte[] buffer, int timeout = 2000, byte terminationCharacter = 10)
    {
        lock (_serialPort)
        {
            Write(buffer);
            return ReadBytesToTermination(timeout, terminationCharacter);
        }
    }

    #endregion

    #region IStringCommunication实现

    public void Write(string buffer)
    {
        _serialPort.Write(buffer);
    }

    public string ReadString()
    {
        return _serialPort.ReadExisting();
    }

    public string ReadString(int length, int timeout = 2000)
    {
        _serialPort.ReadTimeout = timeout;
        var readBytes = ReadBytes(length, timeout);
        var readString = Encoding.ASCII.GetString(readBytes);
        return readString;
    }

    public string ReadStringToTermination(int timeout = 2000, char terminationCharacter = '\n')
    {
        return _serialPort.ReadTo(new string(new[] { terminationCharacter }));
    }

    public string SendAndReceivedString(string buffer, int interval)
    {
        lock (_serialPort)
        {
            Write(buffer);
            Thread.Sleep(interval);
            return ReadString();
        }
    }

    public string SendAndReceivedStringByLength(string buffer, int length, int timeout = 2000)
    {
        lock (_serialPort)
        {
            Write(buffer);
            return ReadString(length, timeout);
        }
    }

    public string SendAndReceivedStringToTermination(string buffer, int timeout = 2000, char terminationCharacter = '\n')
    {
        lock (_serialPort)
        {
            Write(buffer);
            return ReadStringToTermination(timeout, terminationCharacter);
        }
    }

    #endregion
}