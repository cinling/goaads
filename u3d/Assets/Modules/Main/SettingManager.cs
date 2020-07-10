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
    /// <summary>
    /// 当前是否显示了菜单
    /// </summary>
    private bool isShow = false;

    public SettingManager(Canvas cvs)
    {
        this.cvs = cvs;
    }

    /// <summary>
    /// 显示或隐藏菜单
    /// </summary>
    public void Toggle()
    {
        if (isShow)
        {
            this.hide();
        }
        else
        {
            this.show();
        }
        isShow = !isShow;
    }

    private void show()
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/SettingView");
        GameObject go = this.cvs.InstantiateGO(prefab);
        go.transform.SetParent(this.cvs.transform);

        SettingView sv = go.GetComponent<SettingView>();
        sv.SetBarVo(this.barVo);
    }

    private void hide()
    {
        SettingView sv = this.cvs.GetComponentInChildren<SettingView>();
        if (sv != null)
        {
            this.cvs.DestroyGO(sv.gameObject);
        }
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
