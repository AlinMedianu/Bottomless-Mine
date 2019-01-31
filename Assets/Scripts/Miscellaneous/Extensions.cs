using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For extension methods
public static class Extensions
{

    /// <summary>
    /// Returns the renderers of all of the children of that transform
    /// </summary>
    public static List<Renderer> GetBodyParts(this Transform transform)
    {
        Transform[] transformAndChildren = transform.GetComponentsInChildren<Transform>();
        List<Renderer> bodyPart = new List<Renderer>();
        foreach (Transform t in transformAndChildren)
            if (t != transform)
                bodyPart.Add(t.GetComponent<Renderer>());
        return bodyPart;
    }

    /// <summary>
    /// Returns the transforms of all of the children of that transform
    /// </summary>
    public static List<Transform> GetChildren(this Transform transform)
    {
        Transform[] transformAndChildren = transform.GetComponentsInChildren<Transform>();
        List<Transform> children = new List<Transform>();
        foreach (Transform t in transformAndChildren)
            if (t != transform)
                children.Add(t);
        return children;
    }
}
