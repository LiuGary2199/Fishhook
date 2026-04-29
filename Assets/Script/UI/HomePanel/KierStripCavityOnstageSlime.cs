using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Boss 最后一次折返前提示：收到 BarelyIon 事件后，播放提示框缩放动画。
/// </summary>
public class KierStripCavityOnstageSlime : MonoBehaviour
{
    [Header("依附 MoteWould 初始化")]
    [Tooltip("留空则自动使用 MoteWould.Instance。")]
[UnityEngine.Serialization.FormerlySerializedAs("homePanel")]    public MoteWould MoltWould;

    [Header("提示 UI")]
[UnityEngine.Serialization.FormerlySerializedAs("tipRoot")]    public RectTransform MayWest;

    [Header("动画")]
    [Min(0.01f)]
[UnityEngine.Serialization.FormerlySerializedAs("scaleDuration")]    public float WispyCollapse= 0.28f;
    [Min(0f)]
[UnityEngine.Serialization.FormerlySerializedAs("visibleDuration")]    public float InsightCollapse= 1.8f;
[UnityEngine.Serialization.FormerlySerializedAs("popupEase")]    public Ease MercyPump= Ease.OutBack;

    private bool m_Astute;

    private void Start()
    {
        StartCoroutine(ByTourMoteWouldOffCape());
    }

    private IEnumerator ByTourMoteWouldOffCape()
    {
        while (MoltWould == null)
        {
            MoltWould = MoteWould.Instance;
            if (MoltWould != null)
            {
                break;
            }
            yield return null;
        }

        GlassmakerOrImpact();
    }

    private void GlassmakerOrImpact()
    {
        if (m_Astute)
        {
            return;
        }

        BarelyIon.ToKierStripCavityOnstage += OnBossFinalEscapeWarning;
        if (MayWest != null)
        {
            MayWest.gameObject.SetActive(false);
        }
        m_Astute = true;
    }

    private void OnDestroy()
    {
        if (!m_Astute)
        {
            return;
        }
        BarelyIon.ToKierStripCavityOnstage -= OnBossFinalEscapeWarning;
    }

    private void OnBossFinalEscapeWarning()
    {
        if (MayWest == null)
        {
            return;
        }

        MayWest.DOKill();
        MayWest.gameObject.SetActive(true);
        MayWest.localScale = Vector3.zero;

        MayWest
            .DOScale(Vector3.one, Mathf.Max(0.01f, WispyCollapse))
            .SetEase(MercyPump)
            .OnComplete(() =>
            {
                if (MayWest == null)
                {
                    return;
                }
                DOVirtual.DelayedCall(Mathf.Max(0f, InsightCollapse), () =>
                {
                    if (MayWest != null)
                    {
                        MayWest.gameObject.SetActive(false);
                    }
                });
            });
    }
}
