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
}
