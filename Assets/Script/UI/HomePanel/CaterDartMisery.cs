using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 全屏点击水波纹效果（基于 UI/WaterWave Shader，扭曲 RawImage 自身贴图）。
/// 挂在一个铺满屏幕的 RawImage 上即可。
/// </summary>
[RequireComponent(typeof(RawImage))]
public class CaterDartMisery : MonoBehaviour, IPointerDownHandler
{
    [Header("水波纹参数")]
    [Tooltip("波纹扩散速度（半径每秒增加量）")]
[UnityEngine.Serialization.FormerlySerializedAs("waveSpeed")]    public float PagePreen= 1.5f;

    [Tooltip("波纹宽度")]
    [Range(0.0f, 0.2f)]
[UnityEngine.Serialization.FormerlySerializedAs("waveWidth")]    public float PageEnure= 0.05f;

    [Tooltip("波纹强度")]
    [Range(0.0f, 0.1f)]
[UnityEngine.Serialization.FormerlySerializedAs("waveStrength")]    public float PageForester= 0.02f;

    [Tooltip("波纹最大半径（0~1，按UV计算）")]
    [Range(0.0f, 1.5f)]
[UnityEngine.Serialization.FormerlySerializedAs("maxRadius")]    public float WitExpend= 1.0f;

    private Material _Get;
    private Vector2 _JuggleUV;
    private float _Hunger;
    private bool _Assess;

    // Shader 属性ID
    private static readonly int ID_DartGovern= Shader.PropertyToID("_WaveCenter");
    private static readonly int ID_DartExpend= Shader.PropertyToID("_WaveRadius");
    private static readonly int ID_DartEnure= Shader.PropertyToID("_WaveWidth");
    private static readonly int ID_DartForester= Shader.PropertyToID("_WaveStrength");

    private void Awake()
    {
        var img = GetComponent<RawImage>();

        // 为每个实例拷贝一份材质，避免修改到共享材质
        if (img.material != null)
        {
            _Get = Instantiate(img.material);
        }
        else
        {
            _Get = new Material(Shader.Find("UI/WaterWave"));
        }
        img.material = _Get;

        // 默认拉满全屏（如果你已经在 Inspector 里设好了，可以删掉这一段）
        var rt = img.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        img.uvRect = new Rect(0, 0, 1, 1);

        // 初始化材质参数
        _Get.SetFloat(ID_DartExpend, 0f);
        _Get.SetFloat(ID_DartEnure, PageEnure);
        _Get.SetFloat(ID_DartForester, PageForester);
    }

    private void Update()
    {
        if (!_Assess) return;

        _Hunger += PagePreen * Time.deltaTime;
        _Get.SetFloat(ID_DartExpend, _Hunger);

        if (_Hunger >= WitExpend)
        {
            _Assess = false;
            _Hunger = 0f;
            _Get.SetFloat(ID_DartExpend, 0f);
        }
    }

    /// <summary>
    /// 点击屏幕时触发一次波纹（需要 Canvas + GraphicRaycaster）。
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform Tile= transform as RectTransform;
        if (Tile == null || _Get == null) return;

        // 屏幕点 → Rect 本地坐标
        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Tile,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint))
        {
            return;
        }

        // 本地坐标 → 0~1 UV（左下为 0,0）
        Rect r = Tile.rect;
        _JuggleUV.x = Mathf.Clamp01((localPoint.x - r.xMin) / r.width);
        _JuggleUV.y = Mathf.Clamp01((localPoint.y - r.yMin) / r.height);

        _Get.SetVector(ID_DartGovern, _JuggleUV);
        _Get.SetFloat(ID_DartEnure, PageEnure);
        _Get.SetFloat(ID_DartForester, PageForester);

        _Hunger = 0f;
        _Get.SetFloat(ID_DartExpend, 0f);
        _Assess = true;
    }

    /// <summary>
    /// 通过代码触发，参数是屏幕坐标（像素）。
    /// </summary>
    public void SeepageDart(Vector2 screenPos, Camera uiCamera = null)
    {
        RectTransform Tile= transform as RectTransform;
        if (Tile == null || _Get == null) return;

        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Tile,
                screenPos,
                uiCamera,
                out localPoint))
        {
            return;
        }

        Rect r = Tile.rect;
        _JuggleUV.x = Mathf.Clamp01((localPoint.x - r.xMin) / r.width);
        _JuggleUV.y = Mathf.Clamp01((localPoint.y - r.yMin) / r.height);

        _Get.SetVector(ID_DartGovern, _JuggleUV);
        _Get.SetFloat(ID_DartEnure, PageEnure);
        _Get.SetFloat(ID_DartForester, PageForester);

        _Hunger = 0f;
        _Get.SetFloat(ID_DartExpend, 0f);
        _Assess = true;
    }

    private void OnDestroy()
    {
        if (_Get != null)
        {
            Destroy(_Get);
            _Get = null;
        }
    }
}
