using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 记录类型
/// </summary>
public enum RecoderType
{
    /// <summary>
    /// 比较次数
    /// </summary>
    Compare,
    /// <summary>
    /// 赋值次数
    /// </summary>
    Assign,
    /// <summary>
    /// 计算次数（简单的加减乘除运算）
    /// </summary>
    Compute,
}

/// <summary>
/// 记录器数据。用于记录计算过程中的数据
/// </summary>
public class RecorderVo
{
    /// <summary>
    /// 其实时间戳（单位：毫秒）
    /// </summary>
    public long startMS;
    /// <summary>
    /// 结束时间戳（单位：毫秒）
    /// </summary>
    public long endMS;
    /// <summary>
    /// 数据记录字典
    /// </summary>
    public Dictionary<RecoderType, int> numDict;

    /// <summary>
    /// 已用毫秒数
    /// </summary>
    public long usedMS {
        get {
            long endMS = this.endMS != -1 ? this.endMS : TimeUtil.nowMS();
            return endMS - this.startMS;
        }
    }

    public int compareNum {
        get {
            return this.numDict[RecoderType.Compare];
        }
    }

    public int assignNum {
        get {
            return this.numDict[RecoderType.Assign];
        }
    }

    public int computeNum {
        get {
            return this.numDict[RecoderType.Compute];
        }
    }

    /// <summary>
    /// 文字显示内容
    /// </summary>
    /// <returns></returns>
    public string showText
    {
        get {
            return "耗时：" + this.usedSeconds(2) + "秒  对比：" + this.compareNum + "次  赋值：" + this.assignNum + "次" + "  计算：" + this.computeNum + "次";
        }
    }

    public RecorderVo()
    {
        this.numDict = new Dictionary<RecoderType, int>();
        this.Reset();
    }

    /// <summary>
    /// 重置计数器
    /// </summary>
    public void Reset()
    {
        this.startMS = TimeUtil.nowMS();
        this.endMS = -1;
        this.resetNumDict();
    }

    /// <summary>
    /// 添加一次计算次数
    /// </summary>
    public void Plus(params RecoderType[] rtList)
    {
        foreach(RecoderType rt in rtList)
        {
            this.numDict[rt]++;
        }
    }

    /// <summary>
    /// 次数校准。（由于算法问题导致的无法记录的问题）
    /// </summary>
    /// <param name="rt">类型</param>
    /// <param name="num">校准数字（可以是负数）</param>
    public void Correct(RecoderType rt, int num)
    {
        this.numDict[rt] += num;
    }

    /// <summary>
    /// 完成
    /// </summary>
    public void Finished()
    {
        this.endMS = TimeUtil.nowMS();
    }

    /// <summary>
    /// 消耗时间的秒数
    /// </summary>
    /// <param name="digit">希望保留多少位小数</param>
    /// <returns></returns>
    private string usedSeconds(int digit)
    {
        float userSeconds = this.usedMS / 1000f;
        return userSeconds.ToString("f" + digit);
    }

    /// <summary>
    /// 重置 数据记录字典
    /// </summary>
    private void resetNumDict()
    {
        foreach (RecoderType rt in Enum.GetValues(typeof(RecoderType)))
        {
            this.numDict[rt] = 0;
        }
    }
}
