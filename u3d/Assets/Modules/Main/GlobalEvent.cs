using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局事件
/// </summary>
public class GlobalEvent : MonoBehaviour
{
    private bool isDebug = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isDebug || !Input.anyKey)
        {
            return;
        }

        this.EventHandler();
    }

    private void EventHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(mouseLeftClick());
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(tClick());
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(nClick());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(escapeClick());
        }
    }

    IEnumerator mouseLeftClick()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("mouse left");
    }

    /// <summary>
    /// 一般用于测试
    /// </summary>
    IEnumerator tClick()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("t");
    }
    /// <summary>
    /// 初始化一个bar列表
    /// </summary>
    /// <returns></returns>
    IEnumerator nClick()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("n");

        Canvas cvs = FindObjectOfType<Canvas>();
        if (cvs == null)
        {
            Debug.LogError("cvs 不存在");
        }

        cvs.InitBarManager();
    }

    IEnumerator escapeClick()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("esc");
        Canvas cvs = FindObjectOfType<Canvas>();
        if (cvs == null)
        {
            Debug.LogError("cvs 不存在");
        }

        cvs.settingManager.Show();
    }
}
