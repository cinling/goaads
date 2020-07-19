using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoverText : MonoBehaviour
{
    BarManager barManager = null;

    // Start is called before the first frame update
    void Start()
    {
        this.barManager = GameObject.Find("Canvas").GetComponent<Canvas>().barManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.barManager != null && this.barManager.runLock)
        {
            this.GetComponent<Text>().text = this.barManager.GetRecorderVo().showText;
        }
    }
}
