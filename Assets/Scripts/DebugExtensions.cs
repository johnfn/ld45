

#if !UNITY_EDITOR
#define DEBUG_LOG_OVERWRAP
#endif
using UnityEngine;

#if DEBUG_LOG_OVERWRAP
public static class Debug
{
    static public void Break ()
    {
        if (IsEnable ())
        {
            UnityEngine.Debug.Break ();
        }
    }
 
    static public void Log (object message)
    {
        if (IsEnable ()) {
            UnityEngine.Debug.Log (message);
        }
    }
 
    static public void Log (object message, Object context)
    {
        if (IsEnable ()) {
            UnityEngine.Debug.Log (message, context);
        }
    }
 
    static public void LogWarning (object message)
    {
        if (IsEnable ()) {
            UnityEngine.Debug.LogWarning (message);
        }
    }
 
    static public void LogWarning (object message, Object context)
    {
        if (IsEnable ()) {
            UnityEngine.Debug.LogWarning (message, context);
        }
    }
 
    static public void LogError (object message)
    {
        if (IsEnable ()) {
            UnityEngine.Debug.LogError (message);
        }
    }
 
    static public void LogError (object message, Object context)
    {
        if (IsEnable ()) {
            UnityEngine.Debug.LogError (message, context);
        }
    }
 
    static public void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.0F, bool depthTest = true) {
        UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
    }
 
    static bool IsEnable ()
    {
        return UnityEngine.Debug.isDebugBuild;
    }
}
#endif
