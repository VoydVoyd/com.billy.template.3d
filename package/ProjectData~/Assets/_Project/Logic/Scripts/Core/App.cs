using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class App
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        var app = Object.Instantiate(Resources.Load("App")) as GameObject;
        if (app == null)
            throw new ApplicationException();
        
        Object.DontDestroyOnLoad(app);
    }
}
