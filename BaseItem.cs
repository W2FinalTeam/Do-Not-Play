using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseItem : MonoBehaviour
{
    public Transform myTransform;

    /// <summary>
    /// 初始化道具 
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// 摧毁道具
    /// </summary>
    public abstract void Destory();
}
