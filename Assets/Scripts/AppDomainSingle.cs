using System;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

public class AppDomainSingle
{
    private static AppDomainSingle instance;

    public AppDomain appDomain = new AppDomain();

    public static AppDomainSingle Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new AppDomainSingle();
            }

            return instance;
        }
    }

    
}