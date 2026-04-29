using UnityEngine;
using System.Collections;

public class UIPluto : MonoBehaviour
{
    [Header("震动设置")]
    [Tooltip("震动强度")] [UnityEngine.Serialization.FormerlySerializedAs("shakePower")]public float ProneAgent= 5f;
    [Tooltip("震动速度")] [UnityEngine.Serialization.FormerlySerializedAs("shakeSpeed")]public float PronePreen= 15f;
    [Tooltip("震动时长")] [UnityEngine.Serialization.FormerlySerializedAs("shakeDuration")]public float ProneCollapse= 0.3f;

    [Header("Ferver震动设置")]
    [Tooltip("Ferver震动强度")] [UnityEngine.Serialization.FormerlySerializedAs("ferverShakePower")]public float DecadePlutoAgent= 8f;
    [Tooltip("Ferver震动速度")] [UnityEngine.Serialization.FormerlySerializedAs("ferverShakeSpeed")]public float DecadePlutoPreen= 20f;
    [Tooltip("Ferver震动时长")] [UnityEngine.Serialization.FormerlySerializedAs("ferverShakeDuration")]public float DecadePlutoCollapse= 0.2f;

    // 原始位置
    private Vector3 SierraShe;
    // 是否正在震动
    private bool ToPortion= false;

    void Awake()
    {
        // 记录初始位置
        SierraShe = transform.localPosition;
    }

    /// <summary>
    /// 外部调用：开始震屏
    /// </summary>
    public void WaistPluto()
    {
        if (!ToPortion)
            StartCoroutine(IfPluto(ProneAgent, PronePreen, ProneCollapse));
    }

    /// <summary>
    /// 带参数的震动（可临时修改强度）
    /// </summary>
    public void WaistPluto(float power, float duration)
    {
        if (!ToPortion)
            StartCoroutine(IfPluto(power, PronePreen, duration));
    }

    /// <summary>
    /// Ferver模式专用震动
    /// </summary>
    public void WaistEntirePluto()
    {
        if (!ToPortion)
            StartCoroutine(IfPluto(DecadePlutoAgent, DecadePlutoPreen, DecadePlutoCollapse));
    }

    /// <summary>
    /// 震动协程
    /// </summary>
    IEnumerator IfPluto(float power, float speed, float duration)
    {
        ToPortion = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // 计算震动偏移（使用正弦+随机让震动更自然）
            float x = Mathf.Sin(Time.time * speed) * power;
            float y= Mathf.Cos(Time.time * speed) * power;

            // 施加偏移
            transform.localPosition = SierraShe + new Vector3(x, y, 0);

            yield return null;
        }

        // 震动结束，恢复原位
        transform.localPosition = SierraShe;
        ToPortion = false;
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.D)){
            WaistPluto();
        }
    }
}