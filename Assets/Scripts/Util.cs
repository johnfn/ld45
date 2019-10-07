using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Microsoft.CSharp;


public static class Util {

  public static void DrawPoint(Vector3 pt, Color c = default(Color)) {
    if (c == default(Color)) c = Color.cyan;

    Debug.DrawLine(
      new Vector3(pt.x - 1, pt.y - 1, pt.z - 1),
      new Vector3(pt.x + 1, pt.y + 1, pt.z + 1),
      c,
      0.1f
    );

    Debug.DrawLine(
      new Vector3(pt.x + 1, pt.y - 1, pt.z - 1),
      new Vector3(pt.x - 1, pt.y + 1, pt.z + 1),
      c,
      0.1f
    );

    Debug.DrawLine(
      new Vector3(pt.x - 1, pt.y + 1, pt.z - 1),
      new Vector3(pt.x + 1, pt.y - 1, pt.z + 1),
      c,
      0.1f
    );
  }

  public static float Distance(GameObject go1, GameObject go2) {
    return (
      Mathf.Sqrt(
        Mathf.Pow(go1.transform.position.x - go2.transform.position.x, 2) +
        Mathf.Pow(go1.transform.position.y - go2.transform.position.y, 2)
      )
    );
  }

  /** If value would exceed zero, set to zero. */
  public static float NonPositive(float val) => Mathf.Min(0, val);

  /** If value would dip below zero, set to zero. */
  public static float NonNegative(float val) => Mathf.Max(0, val);

}