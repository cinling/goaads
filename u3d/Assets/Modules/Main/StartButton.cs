using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Canvas cvs = null;
    private bool runLock {
        get {
            return this.cvs != null ? this.cvs.barManager.runLock : true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.cvs = FindObjectOfType<Canvas>();
        this.GetComponent<Button>().onClick.AddListener(delegate { 
            if (this.runLock)
            {
                this.OnStop();
            } else
            {
                this.OnStart();
            }
        });
    }

    /// <summary>
    /// 开始
    /// </summary>
    public void OnStart()
    {
        if (this.runLock)
        {
            Debug.LogWarning("已上锁，请解锁后继续");
            return;
        }
        this.renameText("停止");
        this.cvs.barManager.Start();

        Debug.Log("Start");
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnStop()
    {
        this.renameText("开始");
        this.cvs.barManager.Stop();


        Debug.Log("Stop");
    }

    /// <summary>
    /// 重命名文字
    /// </summary>
    /// <param name="text"></param>
    private void renameText(string text)
    {
        this.GetComponentInChildren<Text>().text = text;
    }
}
