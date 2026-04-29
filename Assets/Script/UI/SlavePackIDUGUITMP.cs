using UnityEngine;
using TMPro;
using Spine.Unity;
using Spine;
using UnityEngine.UI;


[DisallowMultipleComponent]
public class SlavePackIDUGUITMP : MonoBehaviour
{
    [Header("Spine 骨骼对象（UGUI 版）")]
[UnityEngine.Serialization.FormerlySerializedAs("skeletonGraphic")]    public SkeletonGraphic PermeatePaucity;

    [Header("要跟随的骨骼名字")]
[UnityEngine.Serialization.FormerlySerializedAs("boneName")]    public string YourLust;

    [Header("位置偏移")]
    public Vector2 offset;

    [Header("调试：放大骨骼位移（x/y）")]
    [Tooltip("把骨骼相对 skeletonGraphic.transform 的局部位移 x/y 放大该倍数。\n例如 10=放大10倍，用于修正“只动小数点”的现象。")]
[UnityEngine.Serialization.FormerlySerializedAs("boneWorldXYScale")]    public float YourCivicXYPerch= 10f;

    private RectTransform _Tile;
    private Bone _Your;
    private Canvas _Either;
    private Camera _uiKea;

    void Awake()
    {
        _Tile = GetComponent<RectTransform>();
        _Either = GetComponentInParent<Canvas>();
        if (_Either == null) _Either = FindObjectOfType<Canvas>();
        _uiKea = _Either.renderMode == RenderMode.ScreenSpaceOverlay ? null : _Either.worldCamera;
    }

    void OnEnable()
    {
        CurePack();
    }

    void CurePack()
    {
        _Your = null;
        if (PermeatePaucity == null || string.IsNullOrEmpty(YourLust)) return;
        _Your = PermeatePaucity.Skeleton.FindBone(YourLust);
    }

    void LateUpdate()
    {
        if (_Your == null)
        {
            CurePack();
            return;
        }
        if (_Either == null) return;

        // 1) 获取 bone 世界坐标（Unity 2D 体系）
        Vector3 boneWorld = PermeatePaucity.transform.TransformPoint(new Vector3(_Your.WorldX, _Your.WorldY, 0f));

        // 1.1) 对 bone 的 x/y 位移做放大（相对 skeletonGraphic 自身坐标原点）
        // 避免把世界绝对坐标直接乘导致整体跑飞。
        if (!Mathf.Approximately(YourCivicXYPerch, 1f))
        {
            Vector3 boneLocal = PermeatePaucity.transform.InverseTransformPoint(boneWorld);
            boneLocal.x *= YourCivicXYPerch;
            boneLocal.y *= YourCivicXYPerch;
            boneWorld = PermeatePaucity.transform.TransformPoint(boneLocal);
        }

        // 2) 世界坐标 -> 屏幕坐标
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(_uiKea, boneWorld);

        // 3) 屏幕坐标 -> 当前 RectTransform 父级的局部坐标（更贴合 UI 结构）
        RectTransform refRect = (_Tile.parent as RectTransform) != null ? (_Tile.parent as RectTransform) : _Tile;

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(refRect, screenPos, _uiKea, out localPos);

        // 4) 设置 anchoredPosition + offset（offset 直接按 UI 局部单位叠加）
        _Tile.anchoredPosition = localPos + offset;
    }
}