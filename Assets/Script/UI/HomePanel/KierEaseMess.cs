using System.Collections;
using UnityEngine;
using Spine.Unity;

public class KierEaseMess : MonoBehaviour
{
    [Header("引用")]
[UnityEngine.Serialization.FormerlySerializedAs("arrowRect")]    public RectTransform arrowLady;
[UnityEngine.Serialization.FormerlySerializedAs("arrowTipRect")]    public RectTransform AlikeRimLady;
[UnityEngine.Serialization.FormerlySerializedAs("rootRect")]    public RectTransform PinkLady;
[UnityEngine.Serialization.FormerlySerializedAs("swimSystem")]    public UIEaseBergBureau FleeBureau;
[UnityEngine.Serialization.FormerlySerializedAs("warnSpine")]    public SkeletonGraphic WideSlave;
[UnityEngine.Serialization.FormerlySerializedAs("warnSpine2")]    public SkeletonGraphic WideSlave2;
[UnityEngine.Serialization.FormerlySerializedAs("particleRoot")]    public GameObject SunbakedWest;


    [Header("Spine 设置")]
    [Tooltip("预告动画名（播放一次）")]
[UnityEngine.Serialization.FormerlySerializedAs("warnAnimName")]    public string WideDiscLust= "warn";
    [Tooltip("找不到动画时，是否回退到第一条动画")]
[UnityEngine.Serialization.FormerlySerializedAs("fallbackToFirstAnim")]    public bool EuropeanIDFloodDisc= true;

    [Header("边缘偏移")]
[UnityEngine.Serialization.FormerlySerializedAs("edgeInsetX")]    public float TownInputX= 40f;
    [Tooltip("两个警告 Spine 关闭后，箭头额外停留时长（秒）")]
[UnityEngine.Serialization.FormerlySerializedAs("arrowCloseDelayAfterSpine")]    public float AlikeBloodNomadAmongSlave= 0.5f;

    private Coroutine m_MessEdifice;

    private void OnEnable()
    {
        BarelyIon.ToKierAlikeNotation -= OnBossSpawnPrepared;
        BarelyIon.ToKierAlikeNotation += OnBossSpawnPrepared;
    }

    private void OnDisable()
    {
        BarelyIon.ToKierAlikeNotation -= OnBossSpawnPrepared;
        if (m_MessEdifice != null)
        {
            StopCoroutine(m_MessEdifice);
            m_MessEdifice = null;
        }
        if (arrowLady != null)
        {
            arrowLady.gameObject.SetActive(false);
        }
        if (WideSlave != null)
        {
            WideSlave.gameObject.SetActive(false);
            WideSlave2.gameObject.SetActive(false);
        }
        SunbakedWest.SetActive(false);
    }

    private void OnBossSpawnPrepared(int dir, float spawnX, float spawnY, float warnSpineDuration, float warnPostDelay)
    {
        if (FleeBureau == null)
        {
            FleeBureau = FindFirstObjectByType<UIEaseBergBureau>();
        }
        if (PinkLady == null)
        {
            PinkLady = transform as RectTransform;
        }

        WifeMessSlave();
        DaleRefer(dir, spawnX, spawnY);
        if (m_MessEdifice != null)
        {
            StopCoroutine(m_MessEdifice);
        }
        m_MessEdifice = StartCoroutine(ByHoneReferAmongNomad(Mathf.Max(0f, warnSpineDuration), Mathf.Max(0f, AlikeBloodNomadAmongSlave)));
    }

    private IEnumerator ByHoneReferAmongNomad(float spineDelay, float arrowDelay)
    {
        if (spineDelay > 0f)
        {
            yield return new WaitForSeconds(spineDelay);
        }
        if (WideSlave != null)
        {
            WideSlave.gameObject.SetActive(false);
            WideSlave2.gameObject.SetActive(false);
             SunbakedWest.SetActive(false);
        }
        if (arrowDelay > 0f)
        {
            yield return new WaitForSeconds(arrowDelay);
        }
        if (arrowLady != null)
        {
            arrowLady.gameObject.SetActive(false);
        }
        BarelyIon.ToKierMessDormancy?.Invoke();
        m_MessEdifice = null;
    }

    private void DaleRefer(int dir, float spawnX, float spawnY)
    {
        if (arrowLady == null || PinkLady == null || FleeBureau == null || FleeBureau.FleeTill == null)
        {
            return;
        }

        Rect area = FleeBureau.FleeTill.rect;
        float y= Mathf.Clamp(spawnY, area.yMin, area.yMax);
        float x = dir > 0 ? (area.xMin + TownInputX) : (area.xMax - TownInputX);
        arrowLady.anchoredPosition = new Vector2(x, y);
        Vector3 Wispy= arrowLady.localScale;
        Wispy.x = Mathf.Abs(Wispy.x);
        arrowLady.localScale = Wispy;
        if (AlikeRimLady != null)
        {
            Vector3 tipEuler = AlikeRimLady.localEulerAngles;
            tipEuler.z = dir > 0 ? -90f : 90f;
            AlikeRimLady.localEulerAngles = tipEuler;
        }
        arrowLady.gameObject.SetActive(true);
    }

    private void WifeMessSlave()
    {
        if (WideSlave == null)
        {
            return;
        }
 SunbakedWest.SetActive(true);
        WideSlave.gameObject.SetActive(true);
        WideSlave2.gameObject.SetActive(true);
        if (WideSlave.AnimationState == null || WideSlave.Skeleton == null)
        {
            return;
        }

        string anim = WideDiscLust;
        if (string.IsNullOrEmpty(anim) && EuropeanIDFloodDisc)
        {
            if (WideSlave.Skeleton.Data != null && WideSlave.Skeleton.Data.Animations != null && WideSlave.Skeleton.Data.Animations.Count > 0)
            {
                anim = WideSlave.Skeleton.Data.Animations.Items[0].Name;
            }
        }
        if (string.IsNullOrEmpty(anim))
        {
            return;
        }

        WideSlave.AnimationState.SetAnimation(0, anim, false);
    }
}
