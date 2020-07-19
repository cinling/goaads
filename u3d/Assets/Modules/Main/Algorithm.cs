using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

/// <summary>
/// 算法
/// </summary>
public class Algorithm
{
    private BarManager barManager;

    public Algorithm(BarManager barManager)
    {
        this.barManager = barManager;
    }

    /// <summary>
    /// 冒泡排序法
    /// </summary>
    public IEnumerator BubbleSort()
    {
        yield return new WaitForEndOfFrame();
        BarVo barVo = this.barManager.GetBarVo();
        RecorderVo recorderVo = this.barManager.GetRecorderVo();

        int maxI = this.barManager.barList.Count - 1;
        bool finished;
        recorderVo.Plus(
            RecoderType.Compute     // -1 运算
            , RecoderType.Assign    // mixI 赋值
            ); 
        do
        {
            finished = true;
            recorderVo.Plus(
                RecoderType.Compare     // do while 判断（含多记录的一条）
                , RecoderType.Assign    // finished 赋值
                , RecoderType.Assign    // 下面 i 初始化赋值
                );
            for (int i = 0; i < maxI; i++)
            {
                int j = i + 1;
                bool isIBigger = this.barManager.CompareBars(i, j);
                recorderVo.Plus(
                    RecoderType.Compare, RecoderType.Compute, RecoderType.Assign     // i 对比、i 运算、i赋值
                    , RecoderType.Compute, RecoderType.Assign   // j 计算、j 赋值
                    , RecoderType.Compare   // 比较大小
                    ); 
                yield return new WaitForSeconds(barVo.compareSeconds);
                if (isIBigger)
                {
                    this.barManager.SwapBars(i, j, true);
                    finished = false;
                    recorderVo.Plus(
                        RecoderType.Assign, RecoderType.Assign, RecoderType.Assign  // 交换三次赋值
                        , RecoderType.Assign                                        // finished 赋值
                        );
                    yield return new WaitForSeconds(barVo.swapSeconds);
                }
            }
        } while (!finished);
        recorderVo.Correct(RecoderType.Compare, -1); // 因 do while 导致多记录一次 对比

        this.barManager.Finished();
    }

    /// <summary>
    /// 选择排序
    /// </summary>
    /// <returns></returns>
    public IEnumerator SelectSort()
    {
        yield return new WaitForEndOfFrame();
        BarVo barVo = this.barManager.GetBarVo();
        RecorderVo recorderVo = this.barManager.GetRecorderVo();

        int len = this.barManager.barList.Count;
        int maxI = len - 1;
        recorderVo.Plus(
            RecoderType.Assign                          // len 赋值
            , RecoderType.Compute, RecoderType.Assign   // j计算和赋值
            , RecoderType.Assign                        // for 循环 i 初始化
            );
        
        for (int i = 0; i < maxI; i++)
        {
            int minIndex = i;
            recorderVo.Plus(
                RecoderType.Compare, RecoderType.Compute, RecoderType.Assign    // i 对比、自增
                , RecoderType.Assign                                            // minIndex 初始化
                , RecoderType.Compute, RecoderType.Assign                       // 下面 j 初始化（计算和赋值）
                );

            for (int j = i + 1;  j < len; j++)
            {
                bool isBigger = this.barManager.CompareBars(j, minIndex);
                yield return new WaitForSeconds(barVo.compareSeconds);
                recorderVo.Plus(
                    RecoderType.Compare, RecoderType.Compute, RecoderType.Assign    // j 对比、自增
                    , RecoderType.Compare                                           // 对比大小
                    );
                if (!isBigger)
                {
                    minIndex = j;
                    recorderVo.Plus(RecoderType.Assign);    // 赋值
                }
            }

            recorderVo.Plus(RecoderType.Compare);    // 对比
            if (minIndex != i)
            {
                recorderVo.Plus(RecoderType.Assign, RecoderType.Assign, RecoderType.Assign); // 交换三次赋值
                this.barManager.SwapBars(minIndex, i, true);
                yield return new WaitForSeconds(barVo.swapSeconds);
            }
        }

        this.barManager.Finished();
    }
}
