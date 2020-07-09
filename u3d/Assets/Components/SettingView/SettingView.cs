using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingView : MonoBehaviour
{

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
