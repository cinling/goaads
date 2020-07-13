using System.Collections;
using System.Collections.Generic;
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

        float intervalSeconds = this.barManager.GetBarVo().intervalSeconds;

        bool finished = false;
        while (!finished)
        {
            finished = true;

            for (int i = 0; i < this.barManager.barList.Count - 1; i++)
            {
                Bar bar1 = this.barManager.barList[i];
                Bar bar2 = this.barManager.barList[i + 1];
                yield return new WaitForSeconds(1 * intervalSeconds);
                bar1.CompareAnimation();
                bar2.CompareAnimation();
                if (bar1.num > bar2.num)
                {
                    yield return new WaitForSeconds(3 * intervalSeconds);
                    this.barManager.SwapBarList(i, i+1, true);
                    finished = false;
                }
            }
        }
    }

}
