using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager
{
    private Canvas cvs;
    /// <summary>
    /// 配置
    /// </summary>
    private BarVo barVo = new BarVo();
    /// <summary>
    /// 运行锁
    /// 运行时，除了可以执行 Init() 进行解锁外，不能进行任何操作
    /// </summary>
    private bool _runLock = false;
    /// <summary>
    /// 排序算法的协程
    /// </summary>
    private Coroutine sortCoroutine = null;
    /// <summary>
    /// 运行锁
    /// </summary>
    public bool runLock {
        get {
            return this._runLock;
        }
    }
    /// <summary>
    /// bar 列表（无序）
    /// </summary>
    public List<Bar> barList = new List<Bar>();

    /// <summary>
    /// Obsolete
    /// </summary>
    /// <obsolete>不推荐使用</obsolete>
    public float widthUnit {
        get {
            return (Screen.width - this.barVo.marginLeft - this.barVo.marginRight - 50) / this.barVo.barNum;
        }
    }

    public float heightUnit {
        get {
            return (Screen.height - this.barVo.marginTop - this.barVo.marginBottom - 50) / this.barVo.barNum;
        }
    }

    public float barWidth {
        get {
            return this.widthUnit * this.barVo.barWidthRate;
        }
    }

    public float spaceWidth {
        get {
            return this.widthUnit * (1 - this.barVo.barWidthRate);
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
        this.runLockDown(); // 解锁
    }

    public void Start()
    {
        this.checkRunLock();
        this.runLockUp(); // 上锁

        Algorithm alg = new Algorithm(this);
        this.sortCoroutine = this.cvs.StartCoroutine(alg.BubbleSort());
    }

    /// <summary>
    /// 停止继续运算
    /// </summary>
    public void Stop()
    {
        if (this.sortCoroutine != null)
        {
            this.cvs.StopCoroutine(this.sortCoroutine);
            this.sortCoroutine = null;
            this.runLockDown();
        }
    }

    /// <summary>
    /// 设置参数配置
    /// </summary>
    /// <param name="vo"></param>
    public void SetBarVo(BarVo vo)
    {
        this.checkRunLock();
        this.barVo = vo;
        this.Init();
    }

    public BarVo GetBarVo()
    {
        return this.barVo;
    }

    public void PrintBarList()
    {
        this.checkRunLock();
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

        for (int i = 1; i <= this.barVo.barNum; i++)
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

            this.SwapBarList(targetIndex, randomIndex, false);
        }

        this.ResetBarListView();
    }

    /// <summary>
    /// 交换连个bar的位置
    /// </summary>
    public void SwapBarList(int firstIndex, int secondIndex, bool updateView)
    {
        Bar tmpBar = this.barList[firstIndex];
        this.barList[firstIndex] = this.barList[secondIndex];
        this.barList[secondIndex] = tmpBar;


        // 更新视图数据
        if (updateView)
        {
            Bar bar1 = this.barList[firstIndex];
            Bar bar2 = this.barList[secondIndex];

            // 重新计算坐标
            float wUnit = this.widthUnit;
            float x1 = wUnit * firstIndex + this.barWidth + this.spaceWidth + this.barVo.marginLeft;
            float x2 = wUnit * secondIndex + this.barWidth + this.spaceWidth + this.barVo.marginLeft;
            float y = -this.barVo.marginTop;
            bar1.SetPosition(x1, y);
            bar2.SetPosition(x2, y);

            // 交换动画
            bar1.SwapAnimation();
            bar2.SwapAnimation();

            // 交换音效
            this.PlaySwapSound();
        }
    }

    /// <summary>
    /// 根据列表的位置，重新设置bar的信息
    /// </summary>
    public void ResetBarListView()
    {
        float wUnit = this.widthUnit;

        for (int i = 0; i < this.barList.Count; i++)
        {
            float x = wUnit * i + this.barWidth + this.spaceWidth + this.barVo.marginLeft;
            float y = -this.barVo.marginTop;

            Bar bar = this.barList[i];
            bar.SetPosition(x, y);
        }
    }

    /// <summary>
    /// 播放对比的声音
    /// </summary>
    public void PlayCompareSound()
    {
        this.cvs.soundManager.PlayPiano(33);
    }

    /// <summary>
    /// 播放交换的声音
    /// </summary>
    public void PlaySwapSound()
    {
        this.cvs.soundManager.PlayPiano(77);
    }

    /// <summary>
    /// 上锁
    /// </summary>
    private void runLockUp()
    {
        this._runLock = true;
    }

    /// <summary>
    /// 解锁
    /// </summary>
    private void runLockDown()
    {
        this._runLock = false;
    }

    private void checkRunLock()
    {
        if (this.runLock)
        {
            throw new UnityException("BarManager 已锁定。请初始化以解锁");
        }
    }
}
