using System.Collections;
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
    /// Canvas 实例
    /// </summary>
    private Canvas cvs = null;
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
        this.reset();
        this.registerListener();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 注册事件监听器
    /// </summary>
    private void registerListener()
    {
        Slider slider = this.getNumLineSlider();
        InputField inputField = this.getNumLineInputField();

        slider.onValueChanged.AddListener(delegate
        {
            this.barVo.barNum = (int)slider.value;
            if (this.numLineInputFieldValue != this.barVo.barNum)
            {
                this.numLineInputFieldValue = this.barVo.barNum;
            }
        });

        inputField.onValueChanged.AddListener(delegate {
            if (this.numLineInputFieldValue < this.minBarNum)
            {
                this.numLineInputFieldValue = this.minBarNum;
            }
            else if (this.numLineInputFieldValue > this.maxBarNum)
            {
                this.numLineInputFieldValue = this.maxBarNum;
            }
            this.barVo.barNum = this.numLineInputFieldValue;

            if (this.numLineSliderValue != this.barVo.barNum)
            {
                this.numLineSliderValue = this.barVo.barNum;
            }
        });

        GameObject.Find(this.GetFullPath() + "/" + ContextPath + "/OKButton").GetComponent<Button>().onClick.AddListener(delegate {
            StartCoroutine(onOkClick());
        });
        GameObject.Find(this.GetFullPath() + "/" + ContextPath + "/CancleButton").GetComponent<Button>().onClick.AddListener(delegate {
            StartCoroutine(onCancleClick());
        });
    }

    /// <summary>
    /// 设置bar配置参数
    /// </summary>
    /// <param name="barVo"></param>
    public void SetBarVo(BarVo barVo)
    {
        this.barVo = barVo;
        this.reset();
    }

    /// <summary>
    /// 根据参数配置，重置 prefab 的参数
    /// </summary>
    private void reset()
    {
        // 初始化滚动条
        Slider slider = this.getNumLineSlider();
        this.numLineSliderValue = this.barVo.barNum;
        slider.minValue = this.minBarNum;
        slider.maxValue = this.maxBarNum;
        slider.wholeNumbers = true; // 设置步长为整数形式

        // 初始化输入框
        this.numLineInputFieldValue = this.barVo.barNum;
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

    /// <summary>
    /// 确定按钮点击
    /// </summary>
    private IEnumerator onOkClick()
    {
        yield return new WaitForFixedUpdate();

        this.getCanvas().barManager.SetBarVo(this.barVo);
        this.getCanvas().settingManager.Hide();
    }

    /// <summary>
    /// 取消按钮点击
    /// </summary>
    private IEnumerator onCancleClick()
    {
        yield return new WaitForFixedUpdate();

        this.getCanvas().settingManager.Hide();
    }

    private Canvas getCanvas()
    {
        if (this.cvs == null)
        {
            this.cvs = GameObject.Find("Canvas").GetComponent<Canvas>();
        }
        return this.cvs;
    }
}
