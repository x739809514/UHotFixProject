using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

/// <summary>
/// ILRuntime入口类
/// </summary>
public class UIEntrance : MonoBehaviour
{
    private AppDomain appdomain = AppDomainSingle.Instance.appDomain;
    private IType type;
    private ILTypeInstance obj;
    public static Transform canvasTrans;

#region override

    private void Awake()
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        
        obj = appdomain.Instantiate("HotFix_Project.ILRuntime_UITest.InitializationIlLRuntime",null);
        type = appdomain.LoadedTypes["HotFix_Project.ILRuntime_UITest.InitializationIlLRuntime"];
        var method = type.GetMethod("OnAwake",0);
        using (var ctx = appdomain.BeginInvoke(method))
        {
            ctx.PushObject(obj);
            ctx.Invoke();
        }
    }

    private void OnEnable()
    {
        var method = type.GetMethod("OnEnable",0);
        using (var ctx = appdomain.BeginInvoke(method))
        {
            ctx.PushObject(obj);
            ctx.Invoke();
        }
    }

    private void Start()
    {
        var method = type.GetMethod("OnStart",0);
        using (var ctx = appdomain.BeginInvoke(method))
        {
            ctx.PushObject(obj);
            ctx.Invoke();
        }
    }

    private void FixedUpdate()
    {
        var method = type.GetMethod("OnFixUpdate",1);
        using (var ctx = appdomain.BeginInvoke(method))
        {
            ctx.PushObject(obj);
            ctx.PushObject(Time.deltaTime);
            ctx.Invoke();
        }
        
    }

    private void Update()
    {
        var method = type.GetMethod("OnUpdate",1);
        using (var ctx = appdomain.BeginInvoke(method))
        {
            ctx.PushObject(obj);
            ctx.PushObject(Time.deltaTime);
            ctx.Invoke();
        }
    }

    private void OnDestroy()
    {
        var method = type.GetMethod("OnDestroy",0);
        using (var ctx = appdomain.BeginInvoke(method))
        {
            ctx.PushObject(obj);
            ctx.Invoke();
        }
    }

    private void OnDisable()
    {
        var method = type.GetMethod("OnDisable",0);
        using (var ctx = appdomain.BeginInvoke(method))
        {
            ctx.PushObject(obj);
            ctx.Invoke();
        }
    }

#endregion
}