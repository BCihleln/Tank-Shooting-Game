using UnityEngine;


// Tutorial from https://www.bilibili.com/video/BV1FQ4y1d7fG 
public class Singleton<T> : MonoBehaviour where T:Singleton<T>
{
    static T instance;
    public static T Instance{ get { return instance; } }
    public static bool isInitialized{ get { return instance != null; } }

    protected virtual void Awake() {
        if(instance != null) Destroy(this); 
        else instance = (T)this;
    }

    /// <summary>
    /// 當前該實例被銷毀時將instance設置爲空
    /// 完全沒理解……
    /// </summary>
    protected virtual void OnDestroy() {
        if (instance == this) instance = null;
    }
}
