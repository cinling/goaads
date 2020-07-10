using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : MyMono
{
    /// <summary>
    /// bar配置
    /// </summary>
    private BarVo barVo = new BarVo();
    /// <summary>
    /// 最小bar条数
    /// </summary>
    private int minBarNum = 10;
    /// <summary>
    /// 最大bar条数
    /// </summary>
    private int maxBarNum = 200;
    private string ContextPath {
        get {
            return "Viewport/Content";
        }
    }
    /// <summary>
    /// 数目滑块实例
    /// </summary>
    private Slider numLineSlider = null;
    /// <summary>
    /// 数目输入框实例
    /// </summary>
    private InputField numLineInputFiled = null;
    /// <summary>
    /// 数目滑块的值
    /// </summary>
    private int numLineSliderValue {
        get {
            Slider slider = this.getNumLineSlider();
            if (slider == null)
            {
                return 0;
            }
            return (int)slider.value;
        }
        set {
            Slider slider = this.getNumLineSlider();
            if (slider == null)
            {
                return;
            }
            slider.value = value;
        }
    }
    /// <summary>
    /// 数目输入框的值
    /// </summary>
    private int numLineInputFieldValue {
        get {
            InputField inputField = this.getNumLineInputField();
            if (inputField == null)
            {
                return 0;
            }
            return inputField.text != "" ? int.Parse(inputField.text) : 0;
        }
        set {
            InputField inputField = this.getNumLineInputField();
            if (inputField == null)
            {
                return;
            }
            inputField.text = "" + value;
        }
    }

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        // 设置位置
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 设置bar配置参数
    /// </summary>
    /// <param name="barVo"></param>
    public void SetBarVo(BarVo barVo)
    {
        this.barVo = barVo;
        this.initSettingViewByConfig();
    }

    /// <summary>
    /// 根据配置参数初始化设置面板
    /// </summary>
    private void initSettingViewByConfig()
    {
        // 获取对象
        InputField inputField = this.getNumLineInputField();
        Slider slider = this.getNumLineSlider();

        // 初始化滚动条
        slider.value = this.barVo.barNum;
        slider.minValue = this.minBarNum;
        slider.maxValue = this.maxBarNum;
        slider.wholeNumbers = true; // 设置步长为整数形式
        slider.onValueChanged.AddListener(delegate {
            this.barVo.barNum = (int)slider.value;
            if (this.numLineInputFieldValue != this.barVo.barNum)
            {
                this.numLineInputFieldValue = this.barVo.barNum;
            }
        });

        // 初始化输入框
        inputField.text = "" + this.barVo.barNum;
        inputField.onValueChanged.AddListener(delegate {
            int num = int.Parse(inputField.text);
            if (num < this.minBarNum)
            {
                num = this.minBarNum;
            } 
            else if (num > this.maxBarNum) 
            {
                num = this.maxBarNum;
            }
            this.numLineInputFieldValue = num;
            this.barVo.barNum = num;

            if (this.numLineSliderValue != this.barVo.barNum)
            {
                this.numLineSliderValue = this.barVo.barNum;
            }
        });
    }

    /// <summary>
    /// 获取数目滑块实例
    /// </summary>
    /// <returns></returns>
    private Slider getNumLineSlider()
    {
        if (this.numLineSlider == null)
        {
            this.numLineSlider = GameObject.Find(this.GetFullPath() + "/" + ContextPath + "/NumLine/Slider").GetComponent<Slider>();
        }
        return this.numLineSlider;
    }

    /// <summary>
    /// 获取数目输入框实例
    /// </summary>
    /// <returns></returns>
    private InputField getNumLineInputField()
    {
        if (this.numLineInputFiled == null)
        {
            this.numLineInputFiled = GameObject.Find(this.GetFullPath() + "/" + ContextPath + "/NumLine/InputField").GetComponent<InputField>();
        }
        return this.numLineInputFiled;
    }
}
