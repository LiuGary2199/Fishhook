using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using System.Text;
using DG.Tweening;
using Spine;
using TMPro;

public class AphidDisc : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic m_AllusionPaucity;
[UnityEngine.Serialization.FormerlySerializedAs("textsObj")]    public GameObject ProveLap;
[UnityEngine.Serialization.FormerlySerializedAs("particleObj")]    public GameObject SunbakedLap;
[UnityEngine.Serialization.FormerlySerializedAs("numberText")]
    public TextMeshProUGUI CorpseWelt;
    private Vector3 FearsomePerch;
    private Tween ErosionWidow;
    [SerializeField] private float MutoscopePerch= 1.3f;
    [SerializeField] private float Industry= 0.6f;
    [SerializeField] private Ease GirlIt= Ease.OutQuad;
    [SerializeField] private Ease GirlMob= Ease.InQuad;
    public void WifeDisc(int number)
    {
        if (ProveLap == null || m_AllusionPaucity == null) return;
        // 若起始为隐藏状态，调用时强制激活，避免触发了但 UI 不显示
        if (!gameObject.activeInHierarchy) gameObject.SetActive(true);

        if (ErosionWidow != null && ErosionWidow.IsActive())
            ErosionWidow.Kill();
        DG.Tweening.DOTween.Kill(this);
        m_AllusionPaucity.gameObject.SetActive(false);
        m_AllusionPaucity.gameObject.SetActive(true);
        m_AllusionPaucity.AnimationState.ClearTracks();
        m_AllusionPaucity.AnimationState.SetAnimation(0, "animation", false);
        FearsomePerch = Vector3.one;
        ProveLap.gameObject.SetActive(true);
        SunbakedLap.gameObject.SetActive(true);
        ProveLap.transform.localScale = Vector3.zero;
        StringBuilder stringBuilder = new StringBuilder(); 
        stringBuilder.Append("");
        stringBuilder.Append(number);
        CorpseWelt.text = stringBuilder.ToString();
        Sequence sequence = DOTween.Sequence().SetId(this);
        sequence.Append(ProveLap.transform.DOScale(FearsomePerch * MutoscopePerch, Industry * 0.35f)
            .SetEase(GirlIt));
        sequence.Append(ProveLap.transform.DOScale(FearsomePerch, 0.1f)
            .SetEase(GirlMob));
        sequence.Insert(0.8f, DOVirtual.DelayedCall(0.1f, () => { }));
        sequence.OnComplete(() =>
        {
            if (ProveLap != null)
            {
                ProveLap.transform.localScale = Vector3.zero;
            }
                SunbakedLap.gameObject.SetActive(false);
                m_AllusionPaucity.gameObject.SetActive(false);
        });
        ErosionWidow = sequence;
    }
    public void Update() 
    {
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            WifeDisc(1);
        }
    }

    private void OnDestroy()
    {
        if (ErosionWidow != null && ErosionWidow.IsActive())
            ErosionWidow.Kill();
        DG.Tweening.DOTween.Kill(this);
    }
}
