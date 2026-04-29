using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 事件渗透
/// </summary>
public class SeashoreCacheEstuarine : MonoBehaviour, ICanvasRaycastFilter
{
    private Image UplandTough;
    public void WhyLayoutTough(Image target)
    {
        UplandTough = target;
    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (UplandTough == null)
        {
            return true;
        }
        return !RectTransformUtility.RectangleContainsScreenPoint(UplandTough.rectTransform, sp, eventCamera);
    }
}