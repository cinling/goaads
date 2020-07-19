using System;
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
    public int barNum = 20;
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
    /// 单词计算的耗时
    /// </summary>
    public float computerDuration = 0.1f;
    /// <summary>
    /// 交换动画时长
    /// </summary>
    public float swapAnimationSeconds = 0.3f;
    /// <summary>
    /// 对比动画时长
    /// </summary>
    public float compareAnimationSeconds = 0.3f;

    /// <summary>
    /// 对比耗时
    /// </summary>
    public float compareSeconds {
        get {
            return this.computerDuration * 1;
        }
    }
    /// <summary>
    /// 交换耗时
    /// </summary>
    public float swapSeconds {
        get {
            return this.computerDuration * 3;
        }
    }

    /// <summary>
    /// 赋值耗时
    /// </summary>
    public float assignSeconds {
        get {
            return this.computerDuration * 1;
        }
    }
}
