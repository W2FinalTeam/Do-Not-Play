using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseItem : MonoBehaviour
{
    protected Transform transform;

    /// <summary>
    /// 初始化道具 
    /// </summary>
    protected abstract void Init();
    /// <summary>
    /// 摧毁道具
    /// </summary>
    protected abstract void Destory();

}
