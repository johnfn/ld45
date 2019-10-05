

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

  public static string ConvertToString(object obj, int depth) {
    depth++;

    if (depth > 6) {
      return "[too deep!]";
    }

    if (obj == null) {
      return "null";
    }

    if (obj is Vector3 || obj is Vector3Int) {
      return $"Vec3 { obj.ToString() }";
    }

    if (obj is Vector2 || obj is Vector2Int) {
      return $"Vec2 { obj.ToString() }";
    }

    if (obj is Vector4) {
      return $"Vec4 { obj.ToString() }";
    }

    if (obj is Color) {
      Color c = (Color) obj;

      return $"Color [{ c.r }, { c.g }, { c.b }, { c.a }]";
    }

    var objType = obj.GetType();
    var typeString = objType.ToString();

    if (objType.IsArray) {
      var rank = objType.GetArrayRank();

      if (rank == 1) {
        dynamic arr = obj;
        var result = "[";

        for (var i = 0; i < arr.Length; i++) {
          result += D.ConvertToString(arr[i], depth) + ", ";
        }

        return result + "]";
      }

      if (rank == 3) {
        dynamic arr = obj;
        var result = "[";

        for (var i = 0; i < 20; i++) {
          for (var j = 0; j < 20; j++) {
            result += "[";
            for (var k = 0; k < 20; k++) {
              result += D.ConvertToString(arr[i,j,k], depth) + ", ";
            }
            result += "]";
          }
        }

        return result + "]";
      }
    }

    if (objType.IsEnum) {
      return obj.ToString();
    }

    if (
        typeString.StartsWith("System.Int")    ||
        typeString.StartsWith("System.Single") ||
        typeString.StartsWith("System.Double") ||
        typeString.StartsWith("System.Bool")   ||
        typeString.StartsWith("System.String") ||
        typeString.StartsWith("System.Char")   ||
        typeString.StartsWith("System.Date")   ||
        typeString.StartsWith("System.Double")
      ) {
      return obj.ToString();
    }

    if (
      typeString.StartsWith("System.Collections.Generic.Dict") ||
      typeString.StartsWith("SerDict")
    ) {
      dynamic dict = obj;
      var result = "{";

      foreach (var k in dict) {
        result += k.Key + "=" + k.Value + ", ";
      }

      return result + "}";
    }

    if (
      typeString.StartsWith("System.Collections.Generic.List") ||
      typeString.StartsWith("System.Linq.Enumerable")
      ) {

      var listStr = "";

      foreach (var x in (dynamic) obj) {
        listStr += D.ConvertToString(x, depth) + ", ";
      }

      return listStr;
    }

    if (typeString.StartsWith("System.Tuple`2")) {
      dynamic dobj = (dynamic) obj;

      return $"({ ConvertToString(dobj.Item1, depth) }, { ConvertToString(dobj.Item2, depth) })";
    }

    if (typeString.StartsWith("System.Tuple`3")) {
      dynamic dobj = (dynamic) obj;

      return $"({ ConvertToString(dobj.Item1, depth) }, { ConvertToString(dobj.Item2, depth) }, { ConvertToString(dobj.Item3, depth) })";
    }

    if (typeString.StartsWith("UnityEngine.Bounds")) {
      Bounds b = (Bounds) obj;

      return $"Bounds center { ConvertToString(b.center, depth) } ext { ConvertToString(b.extents, depth) } ";
    }

    Debug.Log($"Unknown type { typeString }");

    // Log generic object. Could be bad.

    // Debug.Log($"Unkn type { objType } ");

    var objectStr = "{";

    objectStr += String.Join(", ",
      objType.GetFields()
        .Select(prop => {
          if (prop.Name == "palette") {
            return "[palette]";
          }

          object propValue = prop.GetValue(obj);

          return $"{ prop.Name }: { D.ConvertToString(prop.GetValue(obj), depth) }";
        })
    );

    objectStr += "}";

    return objectStr;
  }
}
#endif
