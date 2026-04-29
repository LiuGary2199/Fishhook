using UnityEngine;
using UnityEngine.UI;

public class CaterNevadaDemobilize : MonoBehaviour
{
    [Header("波纹参数")]
    [Tooltip("波纹最大扩散半径（UV坐标，0-1）")]
[UnityEngine.Serialization.FormerlySerializedAs("maxRippleRadius")]    public float WitNevadaExpend= 0.5f;
    [Tooltip("波纹扩散速度")]
[UnityEngine.Serialization.FormerlySerializedAs("rippleSpeed")]    public float WarmthPreen= 0.1f;
    [Tooltip("波纹强度")]
[UnityEngine.Serialization.FormerlySerializedAs("rippleStrength")]    public float WarmthForester= 0.1f;
    [Tooltip("波纹衰减速度")]
[UnityEngine.Serialization.FormerlySerializedAs("rippleFalloff")]    public float WarmthFiction= 5f;

    private RawImage _DeterTough; // 水面UI
    private Material _DeterEntrench; // 水面材质
    private bool _ToRippling= false; // 是否正在扩散波纹
    private float _ErosionExpend= 0f; // 当前波纹半径
    private Vector2 _WarmthGovernUV; // 波纹中心（UV坐标）

    void Start()
    {
        // 获取RawImage组件（确保挂载对象是RawImage）
        _DeterTough = GetComponent<RawImage>();
        if (_DeterTough == null)
        {
            Debug.LogError("请将脚本挂载到RawImage对象上！");
            return;
        }

        // 创建材质实例（避免修改共享材质）
        _DeterEntrench = new Material(_DeterTough.material);
        _DeterTough.material = _DeterEntrench;

        // 初始化材质参数
        ChartNevadaAssist();
    }

    void Update()
    {
        // 如果正在扩散波纹，更新参数
        if (_ToRippling)
        {
            _ErosionExpend += WarmthPreen * Time.deltaTime;
            // 更新材质参数
            _DeterEntrench.SetVector("_RippleCenter", _WarmthGovernUV);
            _DeterEntrench.SetFloat("_RippleRadius", _ErosionExpend);
            _DeterEntrench.SetFloat("_RippleStrength", WarmthForester);
            _DeterEntrench.SetFloat("_RippleFalloff", WarmthFiction);

            // 当波纹扩散到最大半径，停止扩散
            if (_ErosionExpend >= WitNevadaExpend)
            {
                _ToRippling = false;
                ChartNevadaAssist(); // 重置参数
            }
        }
    }

    /// <summary>
    /// 触发水波（箭入水时调用）
    /// </summary>
    /// <param name="worldPos">箭入水的世界坐标</param>
    public void SeepageNevada(Vector3 worldPos)
    {
        // 将世界坐标转换为UI的UV坐标
        RectTransform rectTrans = _DeterTough.rectTransform;
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTrans,
            Camera.main.WorldToScreenPoint(worldPos),
            Camera.main,
            out localPos
        );

        // 转换为UV坐标（0-1范围）
        _WarmthGovernUV.x = (localPos.x + rectTrans.rect.width / 2) / rectTrans.rect.width;
        _WarmthGovernUV.y = (localPos.y + rectTrans.rect.height / 2) / rectTrans.rect.height;

        // 初始化波纹参数
        _ErosionExpend = 0.01f;
        _ToRippling = true;
    }

    /// <summary>
    /// 重置波纹参数
    /// </summary>
    private void ChartNevadaAssist()
    {
        _DeterEntrench.SetVector("_RippleCenter", Vector2.zero);
        _DeterEntrench.SetFloat("_RippleRadius", 0f);
        _DeterEntrench.SetFloat("_RippleStrength", 0f);
    }

    void OnDestroy()
    {
        // 销毁材质，避免内存泄漏
        if (_DeterEntrench != null)
        {
            Destroy(_DeterEntrench);
        }
    }
}