﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MyMono
{
    /// <summary>
    /// bar 管理器
    /// </summary>
    public BarManager barManager = null;
    /// <summary>
    /// 设置管理器
    /// </summary>
    public SettingManager settingManager = null;
    /// <summary>
    /// 声音管理器
    /// </summary>
    public SoundManager soundManager = null;

    void Awake()
    {
        this.barManager = new BarManager(this);
        this.settingManager = new SettingManager(this);
        this.soundManager = new SoundManager(this);
    }

    void Start()
    {
        // 设置
        GameObject.Find(this.GetFullPath() + "/SettingButton").GetComponent<Button>().onClick.AddListener(delegate
        {
            this.settingManager.Toggle();
        });

        // 重置
        GameObject.Find(this.GetFullPath() + "/ResetButton").GetComponent<Button>().onClick.AddListener(delegate
        {
            this.barManager.Init();
        });
    }

    private void Update()
    {
    }

    /// <summary>
    /// 初始化bar管理器
    /// </summary>
    public void InitBarManager()
    {
        this.barManager.Init();
    }

    /// <summary>
    /// 销毁 GameObject
    /// </summary>
    /// <param name="go"></param>
    public void DestroyGO(GameObject go)
    {
        Destroy(go);
    }

    /// <summary>
    /// 实例化 GameObject
    /// </summary>
    public GameObject InstantiateGO(GameObject go)
    {
        return Instantiate(go);
    }
}
