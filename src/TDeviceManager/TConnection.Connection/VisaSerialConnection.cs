using System.Text.RegularExpressions;
using Ivi.Visa;
using NationalInstruments.Visa;
using TConnection.Abstract;
using TConnection.Abstract.Models;
using SerialParity = TConnection.Abstract.Models.SerialParity;

namespace TConnection.Connection;
/// <summary>
/// Visa串口连接
/// </summary>
public class VisaSerialConnection : VisaConnection,ISerialConnection
{
    #region 属性

    /// <summary>
    /// 串口连接对象
    /// </summary>
    private SerialSession? _serialSession;

    public string PortName
    {
        get
        {
            var match = Regex.Match(ResourceName, "ASRL([0-9]+)::INSTR");
            if (match.Success)
            {
                var value = match.Groups[1].Value;
                return $"COM{value}";
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            var upper = value.ToUpper();
            var match = Regex.Match(upper, "COM([0-9]+)");
            if (match.Success)
            {
                var comValue = match.Groups[1].Value;
                ResourceName = $"ASRL{comValue}::INSTR";
            }
        }
    }

    private int _baudRate = 9600;
    private short _dataBits = 8;
    private SerialStopBits _stopBits = SerialStopBits.One;
    private SerialParity _parity = SerialParity.None;

    public int BaudRate
    {
        get => _baudRate;
        set
        {
            _baudRate = value;
            if (_serialSession != null)
            {
                _serialSession.BaudRate = value;
            }
        }
    }

    public short DataBits
    {
        get => _dataBits;
        set
        {
            _dataBits = value;
            if (_serialSession != null)
            {
                _serialSession.DataBits = value;
            }
        }
    }

    public SerialStopBits StopBits
    {
        get => _stopBits;
        set
        {
            _stopBits = value;
            if (_serialSession != null)
            {
                _serialSession.StopBits = (SerialStopBitsMode)value;
            }
        }
    }

    public SerialParity Parity
    {
        get => _parity;
        set
        {
            _parity = value;
            if (_serialSession != null)
            {
                _serialSession.Parity = (Ivi.Visa.SerialParity)value;
            }
        }
    }

    #endregion

    #region 重写

    public override bool IsConnected => _serialSession != null;

    public override void Connect()
    {
        base.Connect();
        if (Session is SerialSession serialSession)
        {
            _serialSession = serialSession;
            _serialSession.BaudRate = BaudRate;
            _serialSession.DataBits = DataBits;
            _serialSession.StopBits = (SerialStopBitsMode)StopBits;
            _serialSession.Parity = (Ivi.Visa.SerialParity)Parity;
        }
        else
        {
            Session?.Dispose();
            throw new InvalidOperationException($"资源{ResourceName}无法创建{nameof(SerialSession)}");
        }
    }
    

    #endregion
}