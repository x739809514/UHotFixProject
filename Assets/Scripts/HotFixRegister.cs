using System;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

public static class HotFixRegister
{
    /// <summary>
    /// register delegate
    /// </summary>
    /// <param name="appdomain"></param>
    public static void RegisterDelegate(AppDomain appdomain)
    {
        //TestDelegateMethod, 这个委托类型为有个参数为int的方法，注册仅需要注册不同的参数搭配即可
        appdomain.DelegateManager.RegisterMethodDelegate<int>();
        //带返回值的委托的话需要用RegisterFunctionDelegate，返回类型为最后一个
        appdomain.DelegateManager.RegisterFunctionDelegate<int, string>();
        appdomain.DelegateManager.RegisterMethodDelegate<string>();
        appdomain.DelegateManager.RegisterMethodDelegate<float>();
        
        //if there are any other types of delegate, register it
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
        {
            return new UnityEngine.Events.UnityAction(() =>
            {
                ((Action)act)();
            });
        });

    }

    /// <summary>
    /// register coroutine
    /// </summary>
    /// <param name="appdomain"></param>
    public static void RegisterCoroutine(AppDomain appdomain)
    {
        //这里做一些ILRuntime的注册
        //使用Couroutine时，C#编译器会自动生成一个实现了IEnumerator，IEnumerator<object>，IDisposable接口的类，因为这是跨域继承，所以需要写CrossBindAdapter（详细请看04_Inheritance教程），Demo已经直接写好，直接注册即可
        appdomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        appdomain.DebugService.StartDebugService(56000);
    }

    public static void ResiterMono(AppDomain appdomain)
    {
        //这里做一些ILRuntime的注册
        appdomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        appdomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
    }
}