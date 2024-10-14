namespace TDevice.Connection.Zlg;

public class ChannelCountAttribute:Attribute
{
    public int ChannelCount { get; set; } = 1;

    public ChannelCountAttribute()
    {
        
    }

    public ChannelCountAttribute(int channelCount)
    {
        ChannelCount = channelCount;
    }
}