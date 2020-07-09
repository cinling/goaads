﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager
{
    private Canvas cvs;
    /// <summary>
    /// bar 列表（有序）
    /// </summary>
    private List<Bar> barList = new List<Bar>();
    /// <summary>
    /// 配置
    /// </summary>
    private BarVo barVo = new BarVo();

    /// <summary>
    /// 每个条宽度占比。
    /// 如果是 0.8f
    /// 则 bar 的宽度是 0.8个单位，空白是 0.2 个单位
    /// </summary>
    private static readonly float barWidthRate = 0.8f;
    /// <summary>
    /// 条数
    /// </summary>
    private static int barNum = 200;

    /// <summary>
    /// Obsolete
    /// </summary>
    /// <obsolete>不推荐使用</obsolete>
    public float widthUnit {
        get {
            return (Screen.width - 50) / (float)barNum;
        }
    }

    public float heightUnit {
        get {
            return (Screen.height - 50) / (float)barNum;
        }
    }

    public float barWidth {
        get {
            return this.widthUnit * barWidthRate;
        }
    }

    public float spaceWidth {
        get {
            return this.widthUnit * (1 - barWidthRate);
        }
    }

    public BarManager(Canvas cvs)
    {
        this.cvs = cvs;
    }


    /// <summary>
    /// 初始化方法
    /// </summary>
    public void Init()
    {
        this.initBarList();
        this.cvs.StartCoroutine(disruptionBarList());
        //this.disruptionBarList();
        //this.showBarList();
        //this.printBarList();
    }

    /// <summary>
    /// 设置参数配置
    /// </summary>
    /// <param name="vo"></param>
    public void SetBarVo(BarVo vo)
    {
        this.barVo = vo;
    }

    public void PrintBarList()
    {
        foreach (Bar bar in this.barList)
        {
            Debug.Log(bar.ToString());
        }
    }

    /// <summary>
    /// 初始化barlist的基本数据
    /// </summary>
    private void initBarList()
    {
        // 清空原有的 BarList 以及对应的 gameobject 对象
        Bar[] oldBarList = this.cvs.transform.GetComponentsInChildren<Bar>();
        for (int i = 0; i < oldBarList.Length; i++)
        {
            this.cvs.DestroyGO(oldBarList[i].gameObject);
        }

        this.barList.Clear();

        for (int i = 1; i <= barNum; i++)
        {
            Bar bar = this.createBar(i, this.barWidth, this.heightUnit * i);
            bar.num = i;
            this.barList.Add(bar);
        }
    }

    /// <summary>
    /// 创建一个 Bar GameObject 并返回 Bar
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private Bar createBar(int num, float width, float height)
    {
        GameObject go = new GameObject();
        go.name = "bar_" + num;
        go.transform.parent = this.cvs.transform;
        Bar bar = go.AddComponent<Bar>();
        bar.SetSize(width, height);

        return bar;
    }

    /// <summary>
    /// 打乱列表
    /// </summary>
    private IEnumerator disruptionBarList()
    {
        yield return new WaitForFixedUpdate();

        int disruptionNum = this.barList.Count * 2;
        for (int i = 0; i < disruptionNum; i++)
        {
            int targetIndex = i % this.barList.Count;
            int randomIndex = Random.Range(0, this.barList.Count - 1);
            if (targetIndex == randomIndex)
            {
                continue;
            }

            this.swapBarList(targetIndex, randomIndex, false);
        }

        this.showBarList();
    }

    /// <summary>
    /// 交换连个bar的位置
    /// </summary>
    private void swapBarList(int firstIndex, int secondIndex, bool updateView)
    {
        Bar tmpBar = this.barList[firstIndex];
        this.barList[firstIndex] = this.barList[secondIndex];
        this.barList[secondIndex] = tmpBar;


        // 更新视图数据
        if (updateView)
        {
            // TODO
        }
    }

    /// <summary>
    /// 设置坐标
    /// </summary>
    private void showBarList()
    {
        float wUnit = this.widthUnit;

        for (int i = 0; i < this.barList.Count; i++)
        {
            float x = wUnit * i + this.barWidth + this.spaceWidth;

            Bar bar = this.barList[i];
            bar.SetPosition(x, -25);
        }
    }
}