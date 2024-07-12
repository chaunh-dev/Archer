using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils 
{
    public static float ToAngle(Vector2 p_vector2)
    {
        var angle = Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;

        //if (angle < 0)
        //{
        //    angle *= -1;
        //}
        //else
        //{
        //    if (angle < 360)
        //    {
        //        angle = 360 - angle;
        //    }
        //}
        return angle;
    }

    public static float ToAngle(Vector3 p_vector3)
    {
        var angle = Mathf.Atan2(p_vector3.x, p_vector3.z) * Mathf.Rad2Deg;

        //if (angle < 0)
        //{
        //    angle *= -1;
        //}
        //else
        //{
        //    if (angle < 360)
        //    {
        //        angle = 360 - angle;
        //    }
        //}
        return angle;
    }

    public static string CalTime(double timeInSecond)
    {
        var h = (int)timeInSecond / 3600;
        var m = ((int)timeInSecond % 3600) / 60;
        var s = ((int)timeInSecond % 60);

        return (h > 0 ? (h < 10 ? "0" + h : h + "") + ":" : "") + (m > 0 ? (m < 10 ? "0" + m : m + "") + ":" : "") + ((s < 10 && m > 0) ? "0" + s : s + "");
    }

    public static void ResetTransform(this Transform transfrom)
    {
        transfrom.localPosition = Vector3.zero;
        transfrom.localScale = Vector3.one;
        transfrom.localEulerAngles = Vector3.zero;
    }

    public static void CloneTransform(this Transform mTransform, Transform targetTransform)
    {
        mTransform.localPosition = targetTransform.localPosition;
        mTransform.localScale = targetTransform.localScale;
        mTransform.localEulerAngles = targetTransform.localEulerAngles;
    }
    public static List<Transform> GetAllChilds(this Transform _t)
    {
        List<Transform> ts = new List<Transform>();

        foreach (Transform t in _t)
        {
            ts.Add(t);
            if (t.childCount > 0)
                ts.AddRange(GetAllChilds(t));
        }

        return ts;
    }

    public static void SetLayerAllChild(this GameObject go, string layerName, bool alls = true) {
        
        go.layer = LayerMask.NameToLayer(layerName);

        if(alls) {
            var spriteRenderer = go.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null) spriteRenderer.sortingLayerName = layerName;

            var particle = go.GetComponent<ParticleSystemRenderer>();
            if(particle != null) particle.sortingLayerName = layerName;

            var skeletion = go.GetComponent<MeshRenderer>();
            if(skeletion != null) skeletion.sortingLayerName = layerName;
        }

        var childCount = go.transform.childCount;
        for (int i = 0; i < childCount; i++) {
            var child = go.transform.GetChild(i);
            if(child != null) {
                SetLayerAllChild(child.gameObject, layerName, alls);
            }
        }
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static GameObject FindInChildren(this GameObject go, string name)
    {
        return (from x in go.GetComponentsInChildren<Transform>()
                where x.gameObject.name == name
                select x.gameObject).First();
    }

    public static T RandomElement<T>(this List<T> self)
    {
        return self[Random.Range(0, self.Count)];
    }
}
