using System;

public class TimeUtil
{
    /// <summary>
    /// 当前时间戳
    /// </summary>
    /// <returns></returns>
    public static int now()
    {
        return Convert.ToInt32(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
    }

    /// <summary>
    /// 获取当前时间戳（毫秒）
    /// </summary>
    /// <returns></returns>
    public static long nowMS()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
    }
}
