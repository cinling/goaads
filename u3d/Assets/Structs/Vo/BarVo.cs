using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// bar 配置参数
/// </summary>
public class BarVo
{
    /// <summary>
    /// bar 图宽度占比。剩余的部分留有空隙
    /// </summary>
    public float barWidthRate = 0.8f;
    /// <summary>
    /// 需要的 bar 条数
    /// </summary>
    public int barNum = 100;
    /// <summary>
    /// 左边距
    /// </summary>
    public float marginLeft = 15;
    /// <summary>
    /// 右边距
    /// </summary>
    public float marginRight = 15;
    /// <summary>
    /// 上边距
    /// </summary>
    public float marginTop = 50;
    /// <summary>
    /// 下边距
    /// </summary>
    public float marginBottom = 15;
    /// <summary>
    /// 每次运算的时间间隔。
    /// 包括对比、位置互换.
    /// 对比是一次操作。互换是三次（一次赋值就是一次计算）
    /// </summary>
    public float intervalSeconds = 0.01f;
}
