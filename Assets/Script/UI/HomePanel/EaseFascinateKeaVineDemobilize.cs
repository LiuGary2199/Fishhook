using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 仅用于 MagnifierCam + RawImage 的局部特写：
/// - 不切换 Canvas.worldCamera/renderMode
/// - 只通过移动/变焦 MagnifierCam，让目标鱼在相机视野里更靠近
/// - RawImage 负责把 MagnifierCam 的 RenderTexture 显示出来
///
/// 注意：如果你的鱼 UI 是 Canvas(Screen Space-Camera) 且其 Render Camera 不是 zoomCamera，
/// 那么 zoomCamera 再怎么推也看不到鱼（因为鱼不是由 zoomCamera 渲染的）。
/// </summary>
public class EaseFascinateKeaVineDemobilize : MonoBehaviour
{
    [Header("Refs")]
[UnityEngine.Serialization.FormerlySerializedAs("zoomCamera")]    public Camera zoomRumble;          // 你的 MagnifierCam
[UnityEngine.Serialization.FormerlySerializedAs("zoomRawImage")]    public RawImage SortWedTough;     // 显示 RenderTexture 的小 RawImage

    [Tooltip("用于把 RectTransform 坐标换算到屏幕中心（用于居中相机）。如果为空会直接用 zoomCamera 做换算。")]
[UnityEngine.Serialization.FormerlySerializedAs("screenPointCamera")]    public Camera ExportPetalRumble; // 通常就是 UICamera

    [Header("Zoom")]
[UnityEngine.Serialization.FormerlySerializedAs("zoomDuration")]    public float SortCollapse= 0.35f;
[UnityEngine.Serialization.FormerlySerializedAs("zoomInOrthoSizeMultiplier")]    public float SortItBrassFuelNavigation= 0.35f; // orthographic 时：越小越近更大
[UnityEngine.Serialization.FormerlySerializedAs("zoomInFovMultiplier")]    public float SortItSawNavigation= 0.6f;          // perspective 时：FOV 乘系数（越小越近更大）
[UnityEngine.Serialization.FormerlySerializedAs("centerLerp")]    public float JuggleLate= 0.25f; // 相机居中平滑强度
[UnityEngine.Serialization.FormerlySerializedAs("centerOnTarget")]    public bool JuggleToLayout= true;

    [Header("Debug")]
[UnityEngine.Serialization.FormerlySerializedAs("debugLog")]    public bool NovelRod= false;

    private Coroutine m_Coro;

    private float m_WaistBrassFuel;
    private float m_WaistSaw;
    private Vector3 m_WaistShe;
    private Quaternion m_WaistSex;

    public void WaistVine(RectTransform targetFish, float? durationOverride = null)
    {
        if (targetFish == null) return;
        if (zoomRumble == null || SortWedTough == null)
        {
            Debug.LogWarning("EaseFascinateKeaVineDemobilize: refs missing.");
            return;
        }

        float Industry= durationOverride.HasValue ? Mathf.Max(0.01f, durationOverride.Value) : SortCollapse;
        SectVine();

        m_WaistBrassFuel = zoomRumble.orthographic ? zoomRumble.orthographicSize : 0f;
        m_WaistSaw = zoomRumble.fieldOfView;
        m_WaistShe = zoomRumble.transform.position;
        m_WaistSex = zoomRumble.transform.rotation;

        zoomRumble.gameObject.SetActive(true);
        zoomRumble.enabled = true;

        SortWedTough.gameObject.SetActive(true);
        SortWedTough.enabled = true;

        // 确保 RawImage 显示正确纹理
        if (zoomRumble.targetTexture != null && SortWedTough.texture != zoomRumble.targetTexture)
        {
            SortWedTough.texture = zoomRumble.targetTexture;
        }

        if (NovelRod)
        {
            Debug.Log($"EaseFascinateKeaVineDemobilize: StartZoom target={targetFish.name}, camActive={zoomRumble.gameObject.activeSelf}, texSet={(SortWedTough.texture == zoomRumble.targetTexture)}");
        }

        m_Coro = StartCoroutine(VineEdifice(targetFish, Industry));
    }

    public void SectVine()
    {
        if (m_Coro != null)
        {
            StopCoroutine(m_Coro);
            m_Coro = null;
        }

        if (zoomRumble != null)
        {
            zoomRumble.enabled = false;
            zoomRumble.gameObject.SetActive(false);
        }

        if (SortWedTough != null)
        {
            SortWedTough.enabled = false;
            SortWedTough.gameObject.SetActive(false);
        }

        if (NovelRod && zoomRumble != null)
        {
            Debug.Log($"EaseFascinateKeaVineDemobilize: StopZoom camActive={zoomRumble.gameObject.activeSelf}");
        }
    }

    private IEnumerator VineEdifice(RectTransform targetFish, float duration)
    {
        float targetOrthoSize = zoomRumble.orthographic
            ? Mathf.Max(0.01f, m_WaistBrassFuel * SortItBrassFuelNavigation)
            : m_WaistBrassFuel;
        float targetFov = Mathf.Max(1f, m_WaistSaw * SortItSawNavigation);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float p = Mathf.Clamp01(t);

            if (zoomRumble.orthographic)
            {
                zoomRumble.orthographicSize = Mathf.Lerp(m_WaistBrassFuel, targetOrthoSize, p);
            }
            else
            {
                zoomRumble.fieldOfView = Mathf.Lerp(m_WaistSaw, targetFov, p);
            }

            if (JuggleToLayout)
            {
                GovernRumbleIDLayout(targetFish);
            }

            yield return null;
        }
    }

    private void GovernRumbleIDLayout(RectTransform targetFish)
    {
        if (targetFish == null) return;

        Camera cam = ExportPetalRumble != null ? ExportPetalRumble : zoomRumble;

        // 正交：用像素偏移近似把目标拉到屏幕中心
        if (zoomRumble.orthographic)
        {
            Vector3 targetScreen = RectTransformUtility.WorldToScreenPoint(cam, targetFish.position);
            Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
            Vector3 deltaPixels = targetScreen - screenCenter;

            float worldUnitsPerPixel = (2f * zoomRumble.orthographicSize) / Mathf.Max(1f, Screen.height);

            Vector3 offset =
                (-deltaPixels.x * worldUnitsPerPixel) * zoomRumble.transform.right +
                (deltaPixels.y * worldUnitsPerPixel) * zoomRumble.transform.up;

            Vector3 desiredPos = m_WaistShe + offset;
            zoomRumble.transform.position = Vector3.Lerp(zoomRumble.transform.position, desiredPos, JuggleLate);
            zoomRumble.transform.rotation = m_WaistSex;
            return;
        }

        // 透视：深度不易得出单位换算，因此用 LookAt 保证目标在中心附近
        Vector3 toTarget = targetFish.position - zoomRumble.transform.position;
        if (toTarget.sqrMagnitude < 0.0001f) return;
        Quaternion desiredRot = Quaternion.LookRotation(toTarget.normalized, zoomRumble.transform.up);
        zoomRumble.transform.rotation = Quaternion.Lerp(zoomRumble.transform.rotation, desiredRot, JuggleLate);
    }
}

