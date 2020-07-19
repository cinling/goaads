using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// bar 状态枚举
/// </summary>
public enum BarStatus
{
    /// <summary>
    /// 无。静止状态
    /// </summary>
    None,
    /// <summary>
    /// 对比中
    /// 200720 目前被 Active 代替。暂无使用
    /// </summary>
    Compare,
    /// <summary>
    /// 交换中
    /// </summary>
    Swap,
    /// <summary>
    /// 状态激活中（如选择排序法，需要一直点亮）
    /// </summary>
    Active,
}

/// <summary>
/// 交换动画参数
/// </summary>
class SwapAnimotionOption
{

    /// <summary>
    /// 原始位置（锚点坐标）
    /// </summary>
    private Vector2 originVec2;
    /// <summary>
    /// 需要移动到的位置（锚点坐标）
    /// </summary>
    private Vector2 targetVec2;
    /// <summary>
    /// 动画开始时 时间戳
    /// </summary>
    private long startMS;
    /// <summary>
    /// 动画结束时 时间戳
    /// </summary>
    private long endMS;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="vec2">初始化位置</param>
    public SwapAnimotionOption(Vector2 vec2)
    {
        this.originVec2 = vec2;
        this.targetVec2 = vec2;
    }

    /// <summary>
    /// 设置目标位置
    /// </summary>
    /// <param name="vec2">需要移动到的位置</param>
    /// <param name="durationMS">动画时长</param>
    public void SetTargetVec2(Vector2 vec2, int durationMS)
    {
        this.originVec2 = this.targetVec2;
        this.targetVec2 = vec2;
        this.startMS = TimeUtil.nowMS();
        this.endMS = this.startMS + durationMS;
    }

    /// <summary>
    /// 当前需要移动到的坐标
    /// </summary>
    /// <returns></returns>
    public Vector2 NowPosition()
    {
        float rate = (this.endMS - TimeUtil.nowMS()) / (float)(this.endMS - this.startMS);
        if (rate < 0)
        {
            rate = 1;
        }

        float x = (this.targetVec2.x - this.originVec2.x) * rate + this.originVec2.x;
        float y = (this.targetVec2.y - this.originVec2.y) * rate + this.originVec2.y;

        return new Vector2(x, y);
    }

    /// <summary>
    /// 直接获取目标位置
    /// </summary>
    /// <returns></returns>
    public Vector2 GetTargetVec2()
    {
        return this.targetVec2;
    }
}

public class Bar : MonoBehaviour
{
    /// <summary>
    /// 恢复的时间戳
    /// </summary>
    private long recoverMS;
    /// <summary>
    /// bar 状态
    /// </summary>
    private BarStatus status = BarStatus.None;
    /// <summary>
    /// 交换动画参数
    /// </summary>
    private SwapAnimotionOption swapAnimotionOption;


    /// <summary>
    /// 数字大小
    /// </summary>
    public int num;

    // Start is called before the first frame update
    void Awake()
    {
        Image img = this.gameObject.AddComponent<Image>();
        //RectTransform rect = this.gameObject.GetComponent<RectTransform>();
        img.color = Color.black;
        img.rectTransform.sizeDelta = new Vector2(0, 0);
        img.rectTransform.anchorMin = new Vector2(0, 1);
        img.rectTransform.anchorMax = new Vector2(0, 1);
        img.rectTransform.pivot = new Vector2(1, 1);

    }

    void Start()
    {
        Image img = this.gameObject.GetComponent<Image>();
        this.swapAnimotionOption = new SwapAnimotionOption(img.rectTransform.anchoredPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status == BarStatus.Swap)
        {
            Image img = this.gameObject.GetComponent<Image>();
            img.rectTransform.anchoredPosition = this.swapAnimotionOption.NowPosition();
        }
    }

    /// <summary>
    /// 设置大小
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void SetSize(float w, float h)
    {
        this.gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(w, h);
    }

    /// <summary>
    /// 设置需要移动到的位置
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPosition(float x, float y)
    {
        Image img = this.gameObject.GetComponent<Image>();
        Vector2 vec2 = new Vector2(x, y);
        img.rectTransform.anchoredPosition = vec2;
        this.swapAnimotionOption = new SwapAnimotionOption(vec2);
    }

    /// <summary>
    /// 设置位置，并且使用动画播放
    /// </summary>
    /// <param name="vec2"></param>
    public void SetPositionWithAnimotion(Vector2 vec2)
    {
        this.swapAnimotionOption.SetTargetVec2(vec2, 300);
    }

    public override string ToString()
    {
        return "[num = " + this.num + "]";
    }

    /// <summary>
    /// 对比动画
    /// </summary>
    public void CompareAnimation()
    {
        if (!this.CanChangeTo(BarStatus.Compare))
        {
            return;
        }
        StartCoroutine(this.compareAnimation());
        this.status = BarStatus.Compare;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private IEnumerator compareAnimation()
    {
        Image image = this.gameObject.GetComponent<Image>();
        image.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        if (this.recoverMS <= TimeUtil.nowMS())
        {
            image.color = Color.black;
        }

        if (this.status == BarStatus.Compare)
        {
            this.status = BarStatus.None;
        }
    }

    /// <summary>
    /// 交换动画
    /// </summary>
    public void SwapAnimation()
    {
        if (!this.CanChangeTo(BarStatus.Swap))
        {
            return;
        }
        StartCoroutine(this.swapAnimation());
        this.status = BarStatus.Swap;
    }

    private IEnumerator swapAnimation()
    {
        // 变换恢复的时间
        int transTimeoutMS = 300;
        Image image = this.gameObject.GetComponent<Image>();

        this.recoverMS = TimeUtil.nowMS() + transTimeoutMS;

        image.color = Color.red;
        yield return new WaitForSeconds(transTimeoutMS * 0.001f);
        while (this.recoverMS > TimeUtil.nowMS())
        {
            yield return new WaitForFixedUpdate();
        }
        image.color = Color.black;
        if (this.status == BarStatus.Swap)
        {
            this.status = BarStatus.None;
            image.rectTransform.anchoredPosition = this.swapAnimotionOption.GetTargetVec2();
        }
    }

    /// <summary>
    /// 激活动画
    /// </summary>
    public void ActiveAnimation()
    {
        StartCoroutine(this.activeAnimation());
        this.status = BarStatus.Active;
    }

    private IEnumerator activeAnimation()
    {
        yield return new WaitForFixedUpdate();
        this.gameObject.GetComponent<Image>().color = Color.blue;
    }

    /// <summary>
    /// 取消激活
    /// </summary>
    public void InactiveAnimation()
    {
        if (this.status == BarStatus.Active)
        {
            StartCoroutine(this.inactiveAnimation());
            this.status = BarStatus.None;
        }
    }

    private IEnumerator inactiveAnimation()
    {
        yield return new WaitForFixedUpdate();
        this.gameObject.GetComponent<Image>().color = Color.black;
    }


    /// <summary>
    /// 判断当前状态是否可以转变为目标状态
    /// </summary>
    /// <param name="targetStatus">目标状态</param>
    /// <returns></returns>
    public bool CanChangeTo(BarStatus targetStatus)
    {
        switch (this.status)
        {
            case BarStatus.None:
                return true;
            case BarStatus.Compare:
                return targetStatus == BarStatus.Swap || targetStatus == BarStatus.Active;
            case BarStatus.Swap:
                return false;
            case BarStatus.Active:
                return true;
            default:
                return false;
        }
    }
}
