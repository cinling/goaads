using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{

    /// <summary>
    /// 数字大小
    /// </summary>
    public int num;

    // Start is called before the first frame update
    void Awake()
    {
        Image img = this.gameObject.AddComponent<Image>();
        //RectTransform rect = this.gameObject.GetComponent<RectTransform>();
        img.color = Color.black;
        img.rectTransform.sizeDelta = new Vector2(0, 0);
        img.rectTransform.anchorMin = new Vector2(0, 1);
        img.rectTransform.anchorMax = new Vector2(0, 1);
        img.rectTransform.pivot = new Vector2(1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 设置大小
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void SetSize(float w, float h)
    {
        this.gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(w, h);
    }

    public void SetPosition(float x, float y)
    {
        Image img = this.gameObject.GetComponent<Image>();
        img.rectTransform.anchoredPosition = new Vector3(x, y);
    }

    public override string ToString()
    {
        return "[num = " + this.num + "]";
    }
}
