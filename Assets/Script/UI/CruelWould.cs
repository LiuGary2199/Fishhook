using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


/// <summary> 引导面板 </summary>
public class CruelWould : ShedUIHobby
{
    Image Drape;
    [SerializeField] Image[] IronInsect;
    [Range(0f, 1f)] [SerializeField] float PredateIronProse= 0.78f;
[UnityEngine.Serialization.FormerlySerializedAs("Center")]    public RectTransform Govern;
[UnityEngine.Serialization.FormerlySerializedAs("Hand")]    public Transform Core;
[UnityEngine.Serialization.FormerlySerializedAs("InfoBg")]    public Transform SlumNo;
[UnityEngine.Serialization.FormerlySerializedAs("Info")]    public Text Slum;
    [HideInInspector] [UnityEngine.Serialization.FormerlySerializedAs("GuideIndex")]public int CruelSmile;
[UnityEngine.Serialization.FormerlySerializedAs("NextBtn")]    public Button WaleLad;
    Coroutine SlumLoneIE;


    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        Drape = GetComponent<Image>();
        Hone(true);
    }

    public void DaleIron(Transform Target, float Scale = 1, float? alpha = null)
    {
        RectTransform TargetRect = Target.GetComponent<RectTransform>();
        DaleIron(TargetRect.position, TargetRect.sizeDelta * Target.localScale * Scale, alpha);
    }
    void DaleIron(Vector2 Pos, Vector2 Size, float? alpha = null)
    {
        WhyIronProse(alpha ?? PredateIronProse);
        Drape.raycastTarget = true;
        Govern.DOKill();
        Govern.DOMove(Pos, .5f);
        Govern.DOSizeDelta(Size, .7f).OnComplete(() =>
        {
            Drape.raycastTarget = false;
        });
    }

    public void DaleIron_Cajun(Vector2 Pos, Vector2 Size, float? alpha = null)
    {
        WhyIronProse(alpha ?? PredateIronProse);
        Drape.raycastTarget = true;
        Govern.DOKill();
        Govern.DOLocalMove(Pos, .5f);
        Govern.DOSizeDelta(Size, .7f).OnComplete(() =>
        {
            Drape.raycastTarget = false;
        });
    }

    public void DaleCore(Vector2[] Poss)
    {
        Core.DOKill();
        Core.gameObject.SetActive(true);
        Core.position = Poss[0];
        if (Poss.Length > 1)
            CoreDisc(Poss, 0);
    }
    void CoreDisc(Vector2[] Poss, int Index)
    {
        Core.DOMove(Poss[Index], .4f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (Core.gameObject.activeSelf)
            {
                if (Index < Poss.Length - 1)
                    CoreDisc(Poss, Index + 1);
                else
                    CoreDisc(Poss, 0);
            }
        });
    }

    public void DaleSlum(string Text, int PosY = 0)
    {
        Slum.DOKill();
        SlumNo.DOKill();
        Slum.text = "";
        SlumNo.gameObject.SetActive(true);
        SlumNo.localScale = new Vector2(0, 1);
        SlumNo.DOScale(Vector2.one, .3f);
        Slum.DOText(Text, .3f).SetDelay(.3f);

        if (SlumLoneIE != null)
            StopCoroutine(SlumLoneIE);
        if (SlumNo.localPosition.y != PosY)
            SlumLoneIE = StartCoroutine(nameof(SlumLone), PosY);
    }
    IEnumerator SlumLone(int PosY)
    {
        Vector2 UpdatePos = SlumNo.localPosition;
        Vector2 TargetPos = new Vector2(0, PosY);
        while (true)
        {
            yield return null;
            TargetPos.y = PosY;
            UpdatePos.y = Mathf.Lerp(UpdatePos.y, TargetPos.y, Time.deltaTime * 10);
            SlumNo.localPosition = UpdatePos;
            if (Mathf.Abs(UpdatePos.y - TargetPos.y) < 1)
                break;
        }
    }

    public void DaleWaleLad(Action OnBtnClick)
    {
        WaleLad.onClick.RemoveAllListeners();
        WaleLad.onClick.AddListener(() =>
        {
            WaleLad.onClick.RemoveAllListeners();
            OnBtnClick();
        });
        WaleLad.gameObject.SetActive(false);
        Invoke(nameof(DaleWaleLad), 1f);
    }
    void DaleWaleLad()
    {
        WaleLad.gameObject.SetActive(true);
    }

    void WhyIronProse(float alpha)
    {
        alpha = Mathf.Clamp01(alpha);
        if (IronInsect != null && IronInsect.Length > 0)
        {
            for (int i = 0; i < IronInsect.Length; i++)
            {
                if (IronInsect[i] == null)
                    continue;
                Color color = IronInsect[i].color;
                color.a = alpha;
                IronInsect[i].color = color;
            }
            return;
        }

        if (Drape == null)
            return;
        Color blockColor = Drape.color;
        blockColor.a = alpha;
        Drape.color = blockColor;
    }

    public void Hone(bool IsBlcok)
    {
        Core.DOKill();
        Slum.DOKill();
        SlumNo.DOKill();
        Govern.DOKill();
        Core.gameObject.SetActive(false);
        Slum.text = "";
        SlumNo.gameObject.SetActive(false);
        WaleLad.gameObject.SetActive(false);
        Govern.localPosition = Vector2.zero;
        Govern.sizeDelta = Vector2.one * 3000;
        Drape.raycastTarget = IsBlcok;
    }

}
