# UHotFixProject
This is a hotload project which is based on ILRuntime. I implemented an UI framework in this project. ILRuntime is a hotload solution for mobile games, which can let developers code in c#. ILRuntime's performance is great, it runs 10 times faster than xLua in mobile phones.
If you want to know more details about how to use ILRuntime, please check the official document: https://ourpalm.github.io/ILRuntime/public/v1/guide/index.html

# How to use this framework?
 1. open the project
 2. you can see a `canvas` in the scene, and create a panel
    ![image](https://github.com/x739809514/UHotFixProject/assets/53636082/bf2a89c9-d5cb-4764-9243-1b1fba92b46b)
 3. after you edit your panel, drag the panel to the `Resources/UI` directory
 4. modify hotfix_project, find `PanelName` and add your panel, please keep the string same as your panel's name
    ![image](https://github.com/x739809514/UHotFixProject/assets/53636082/63c248cb-1889-45dd-9e2b-a172f0d36763)
 5. create a script for your panel, and inheritance `UIBase`, then set the property `panelName`
 6. you can use UIMoudle to open your panel, like `UIMoudle.Instance.OpenPanel<Panel_Test>()`
 7. finally generate the hotload project

# What's in this project?

## Main project

In main project, there are two main scripts: `ILRuntimeInitialize` and `UIEntrance`, in `ILRuntimeInitialize` I initialize the ILRuntime, you need to notice the method `InitializeILRuntime`:
```C#
private void InitializeILRuntime()
{
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
    appdomain.UnityMainThreadID = Thread.CurrentThread.ManagedThreadId;
#endif
    //register delegate, coroutine and monobehaviour, since cross-domain inheritance
    HotFixRegister.RegisterDelegate(appdomain);
    HotFixRegister.RegisterCoroutine(appdomain);
    HotFixRegister.ResiterMono(appdomain);
    
    //CLR Redirection
    var mi = typeof(Debug).GetMethod("Log", new System.Type[] { typeof(object) });
    unsafe //please check unsafe option in Unity
    {
        appdomain.RegisterCLRMethodRedirection(mi, Log_11);
    }
    appdomain.InitializeBindings();
    ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
}
```
## Hotload Project
In hotload proj, there is an UI framework, `UIBase` is a base class for every panel, `UIMoudle` is a manager, you can it to open, pop close your panel.

# notice
Please don't use mono in hot-load proj, if you must need to do so, you can inheritance it in one script once, and make other scripts inheritance that script.
