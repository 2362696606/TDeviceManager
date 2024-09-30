namespace TDevice.Model;

public class Rs485Config:SerialConfig
{
    /// <summary>
    /// 连接名
    /// </summary>
    public string ConnectionName { get; set; } = string.Empty;
}