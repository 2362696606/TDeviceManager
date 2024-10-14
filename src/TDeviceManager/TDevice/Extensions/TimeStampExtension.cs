using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace TDevice.Extensions;

public static class TimeStampExtension
{
    /// <summary>
    /// 当前时间的时间戳
    /// </summary>
    public static long NowTimeStamp => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    /// <summary>
    /// 从时间戳转换为Datetime，时间戳应为Utc
    /// </summary>
    /// <param name="timeStamp">时间戳</param>
    /// <returns>时间戳所表示的<see cref="DateTime"/>对象</returns>
    public static DateTime ToDateTime(this long timeStamp)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(timeStamp).LocalDateTime;
    }
    /// <summary>
    /// 根据Datetime获取时间戳
    /// </summary>
    /// <param name="time"><see cref="DateTime"/>对象</param>
    /// <returns>时间戳</returns>
    public static long ToTimeStamp(this DateTime time)
    {
        var dateTimeOffset = new DateTimeOffset(time);
        return dateTimeOffset.ToUnixTimeMilliseconds();
    }
}