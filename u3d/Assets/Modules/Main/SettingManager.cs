using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingManager
{
    /// <summary>
    /// 画布实例
    /// </summary>
    private Canvas cvs;
    /// <summary>
    /// bar 配置参数
    /// </summary>
    private BarVo barVo = new BarVo();

    public SettingManager(Canvas cvs)
    {
        this.cvs = cvs;
    }

    public void Show()
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/ScrollView");
        GameObject go = this.cvs.InstantiateGO(prefab);
        go.transform.parent = this.cvs.transform;
    }

    public void Hide()
    {

    }

    /// <summary>
    /// 重置配置参数
    /// </summary>
    public void ResetConfig()
    {
        this.barVo = new BarVo();
        this.cvs.barManager.SetBarVo(this.barVo);
    }
}
