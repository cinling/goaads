using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMono : MonoBehaviour
{
    /// <summary>
    /// 获取完整的路径
    /// </summary>
    /// <returns></returns>
    public string GetFullPath()
    {
        return this.getParentName(this.transform, this.gameObject.name);
    }

    private string getParentName(Transform tf, string myName)
    {
        if (tf.parent != null)
        {
            return tf.parent.name + "/" + myName;
        }
        return name;
    }
}
