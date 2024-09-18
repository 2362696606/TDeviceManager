namespace TConnection.Abstract.Models;
/// <summary>
/// 串口奇偶校验位
/// </summary>
public enum SerialParity
{
    /// <summary>No parity check occurs.</summary>
    None,
    /// <summary>Sets the parity bit so that the count of bits set is an odd number.</summary>
    Odd,
    /// <summary>Sets the parity bit so that the count of bits set is an even number.</summary>
    Even,
    /// <summary>Leaves the parity bit set to 1.</summary>
    Mark,
    /// <summary>Leaves the parity bit set to 0.</summary>
    Space,
}