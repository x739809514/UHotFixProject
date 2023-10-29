using System;
using System.Collections.Generic;
using UnityEngine;
using HotFix_Project.ILRuntime_UITest;
using Object = UnityEngine.Object;

public class UIMoudle
{
    #region Filed
    public Dictionary<string, UIBase> PanelDic = new Dictionary<string, UIBase>();
    public Stack<UIBase> UIStack;
    #endregion


    #region singleton

    private static UIMoudle _instance;

    public static UIMoudle Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIMoudle();
            }

            return _instance;
        }
    }

    #endregion


    #region Method
    /// <summary>
    /// 加载一个新页面
    /// </summary>
    /// <param name="panelName"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    public void OpenPanel<T>(Action<T> action = null) where T : UIBase, new()
    {
        //正常应该是从AssetBundle中接入，这里先暂且从resource中读取
        var t = new T();
        var obj = Resources.Load<GameObject>(t.path);
        if (obj == null) return;
        var go = Object.Instantiate(obj, UIEntrance.canvasTrans);
        t.SetGameObj(go);
        OpenUI(t.path, t);
        action?.Invoke(t);
    }

    /// <summary>
    /// 弹出一个界面
    /// </summary>
    /// <param name="panelName"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    public void PopPanel<T>(Action<T> action = null) where T : UIBase, new()
    {
        var t = new T();
        var obj = Resources.Load<GameObject>(t.path);
        if (obj == null) return;
        var go = Object.Instantiate(obj, UIEntrance.canvasTrans);
        t.SetGameObj(go);
        PopUI(t.path, t);
        action?.Invoke(t);
    }

    /// <summary>
    /// 直接关掉最上层的UI
    /// </summary>
    public void CloseUI()
    {
        var ui = UIStack.Pop();
        ui.PauseUI();
        ui.gameObj.SetActive(false);
        if (UIStack.Count == 0) return;
        var top = UIStack.Peek();
        top.gameObj.SetActive(true);
        top.ReEnterUI();
    }

    private void PopUI(string path, UIBase ui)
    {
        if (UIStack == null)
        {
            UIStack = new Stack<UIBase>();
        }
        if (UIStack.Count != 0)
        {
            var topUI = UIStack.Peek();
            //如果页面已经打开，则无视
            if (topUI == PanelDic[path]) return;
        }
        if (PanelDic.ContainsKey(path))
        {
            //把界面压栈
            UIStack.Push(PanelDic[path]);
            PanelDic[path].gameObj.SetActive(true);
            PanelDic[path].ReEnterUI();
        }
        else
        {
            PanelDic.Add(path, ui);
            UIStack.Push(ui);
            ui.gameObj.SetActive(true);
            ui.StartUI();
        }
    }

    private void OpenUI(string path, UIBase ui)
    {
        if (UIStack == null)
        {
            UIStack = new Stack<UIBase>();
        }

        if (UIStack.Count != 0)
        {
            var topUI = UIStack.Peek();
            //如果页面已经打开，则无视
            if (topUI == PanelDic[path]) return;
            //停掉当前页面
            topUI.PauseUI();
            topUI.gameObj.SetActive(false);
        }

        if (PanelDic.ContainsKey(path))
        {
            //把界面压栈
            UIStack.Push(PanelDic[path]);
            PanelDic[path].gameObj.SetActive(true);
            PanelDic[path].ReEnterUI();
        }
        else
        {
            PanelDic.Add(path, ui);
            UIStack.Push(ui);
            ui.gameObj.SetActive(true);
            ui.StartUI();
        }
    }

    #endregion
}