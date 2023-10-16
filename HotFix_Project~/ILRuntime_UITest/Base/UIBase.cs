using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public abstract class UIBase
{
#region Field

    public abstract string panelName { get; }

    public string path => "UI/" + panelName;

    public GameObject gameObj;

    //因为可能要多次调用，所以现在这里获取一次
    private Dictionary<string, UIBase> panelDic = UIMoudle.Instance.PanelDic;

#endregion


#region Init

    public void StartUI()
    {
        InitComponent(gameObj.transform);
    }

    public void ReEnterUI()
    {
        OnEnable();
    }

    public void FixUpdate(float deltaTime)
    {
        OnFixUpdate(deltaTime);
    }

    public void Update(float deltaTime)
    {
        OnUpdate(deltaTime);
    }

    public void PauseUI()
    {
        OnClose();
    }

    public void DestroyUI()
    {
        OnDestroy();
    }

#endregion


#region override

    protected abstract void InitComponent(Transform trans);

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnFixUpdate(float deltaTime)
    {
    }

    protected virtual void OnUpdate(float deltaTime)
    {
    }

    protected virtual void OnClose()
    {
    }

    protected virtual void OnDestroy()
    {
    }

#endregion


#region Method

    public void SetGameObj(GameObject obj)
    {
        gameObj = obj;
    }

    protected T GetUIComponent<T>(Transform transform,string path) where T : Component
    {
        var trans = transform.Find(path);
        if (trans==null)
        {
            Debug.LogError("Transform is missing!");
            return default;
        }

        var t = trans.GetComponent<T>();
        if (t==null)
        {
            Debug.LogError("Component is missing!");
            return default;
        }

        return t;
    }
    
#endregion
}