using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TraditionDemobilize : MonoBehaviour
{
    private static readonly Dictionary<int, Queue<GameObject>> s_LayoutOnMoldJay= new Dictionary<int, Queue<GameObject>>();
    private static readonly Dictionary<GameObject, Tween> s_LayoutOnSolelyWidowJay= new Dictionary<GameObject, Tween>();

    private static GameObject AgeLayoutOnLikeMold(GameObject fxPrefab, Transform parentTransform)
    {
        if (fxPrefab == null) return null;
        int prefabId = fxPrefab.GetInstanceID();
        if (!s_LayoutOnMoldJay.TryGetValue(prefabId, out Queue<GameObject> pool))
        {
            pool = new Queue<GameObject>();
            s_LayoutOnMoldJay[prefabId] = pool;
        }

        while (pool.Count > 0)
        {
            GameObject cached = pool.Dequeue();
            if (cached == null) continue;
            cached.transform.SetParent(parentTransform, false);
            return cached;
        }

        return Instantiate(fxPrefab, parentTransform);
    }

    private static void SolelyLayoutOnIDMold(GameObject fxObj, int prefabId, Transform parentTransform)
    {
        if (fxObj == null) return;
        if (s_LayoutOnSolelyWidowJay.TryGetValue(fxObj, out Tween oldTween) && oldTween != null)
        {
            oldTween.Kill();
        }
        s_LayoutOnSolelyWidowJay.Remove(fxObj);

        fxObj.transform.DOKill();
        fxObj.SetActive(false);
        fxObj.transform.SetParent(parentTransform, false);

        if (!s_LayoutOnMoldJay.TryGetValue(prefabId, out Queue<GameObject> pool))
        {
            pool = new Queue<GameObject>();
            s_LayoutOnMoldJay[prefabId] = pool;
        }
        pool.Enqueue(fxObj);
    }

    private static void ShallowPakistanConcede(GameObject fxObj)
    {
        if (fxObj == null) return;
        ParticleSystem[] systems = fxObj.GetComponentsInChildren<ParticleSystem>(true);
        for (int i = 0; i < systems.Length; i++)
        {
            ParticleSystem ps = systems[i];
            if (ps == null) continue;
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Clear(true);
            ps.Play(true);
        }
    }

    private static float StarlingOnCollapse(GameObject fxObj)
    {
        if (fxObj == null) return 0.8f;
        float maxDuration = 0.8f;
        ParticleSystem[] systems = fxObj.GetComponentsInChildren<ParticleSystem>(true);
        for (int i = 0; i < systems.Length; i++)
        {
            ParticleSystem ps = systems[i];
            if (ps == null) continue;
            var main = ps.main;
            float lifetime = main.startLifetime.constantMax;
            float Industry= main.duration + lifetime;
            if (Industry > maxDuration) maxDuration = Industry;
        }
        return maxDuration;
    }

    void Start()
    {

    }


    void Update()
    {

    }

    /// <summary>
    /// 弹窗出现效果
    /// </summary>
    /// <param name="PopBarUp"></param>
    public static void SeeDale(GameObject PopBarUp, System.Action finish)
    {
        /*-------------------------------------初始化------------------------------------*/
        PopBarUp.GetComponent<CanvasGroup>().alpha = 0;
        PopBarUp.transform.localScale = new Vector3(0, 0, 0);
        /*-------------------------------------动画效果------------------------------------*/
        PopBarUp.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        PopBarUp.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            finish?.Invoke();
        });
    }


    /// <summary>
    /// 弹窗消失效果
    /// </summary>
    /// <param name="PopBarDisapper"></param>
    public static void SeeHone(GameObject PopBarDisapper, System.Action finish)
    {
        /*-------------------------------------初始化------------------------------------*/
        PopBarDisapper.GetComponent<CanvasGroup>().alpha = 1;
        PopBarDisapper.transform.localScale = new Vector3(1, 1, 1);
        /*-------------------------------------动画效果------------------------------------*/
        PopBarDisapper.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        PopBarDisapper.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            finish?.Invoke();
        });
    }


    public static void UISeedLoneBest(GameObject GoldImage, int a, Transform StartTF, Transform EndTF, System.Action finish)//ui界面收现金
    {
        SeedLoneCent(GoldImage, a, StartTF.position, EndTF.position, finish);
    }
    public static void TalkLoneCent(GameObject GoldImage, int a, Transform StartTF, Transform EndTF, System.Action finish)
    {
        UILinkageLoneCent(GoldImage, a, StartTF.position, EndTF.position, finish);
    }

    public static void BorderClause(double startNum, double endNum, float delay, TextMeshProUGUI text, System.Action finish)
    {
        BorderClause(startNum, endNum, delay, text, "", finish);
    }
    public static void BorderClause(double startNum, double endNum, float delay, TextMeshProUGUI text, string prefix, System.Action finish)
    {
        if (text == null)
        {
            finish?.Invoke();
            return;
        }

        DOTween.To(() => startNum, x =>
        {
            if (text != null)
                text.text = prefix + ClauseRide.PhraseIDPer(x);
        }, endNum, 0.5f).SetDelay(delay).OnComplete(() =>
        {
            finish?.Invoke();
        });
    }


    /// <summary>
    /// 收现金   界面 ui    收现金

    public static void SeedLoneCent(GameObject GoldImage, int a, Vector2 StartPosition, Vector2 EndPosition, System.Action finish)
    {
        //如果没有就算了
        if (a == 0)
        {
            finish?.Invoke();
            return;
        }
        //数量不超过15个
        if (a > 15)
        {
            a = 15;
        }
        //每个金币的间隔
        float Delaytime = 0;
        for (int i = 0; i < a; i++)
        {
            int t = i;
            //每次延迟+1
            Delaytime += 0.06f;
            //复制一个图标
            GameObject GoldIcon = Instantiate(GoldImage, GoldImage.transform.parent);
            GoldIcon.SetActive(true);
            //初始化
            GoldIcon.transform.position = StartPosition;
            //GoldIcon.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            //金币弹出随机位置
            float OffsetX = UnityEngine.Random.Range(-0.8f, 0.8f);
            float OffsetY = UnityEngine.Random.Range(-0.8f, 0.8f);
            //创建动画队列
            var s = DOTween.Sequence();
            //金币出现
            s.Append(GoldIcon.transform.DOMove(new Vector3(GoldIcon.transform.position.x + OffsetX, GoldIcon.transform.position.y + OffsetY, GoldIcon.transform.position.z), 0.15f).SetDelay(Delaytime).OnComplete(() =>
            {
                //限制音效播放数量
                //if (Mathf.Sin(t) > 0)
                if (t % 5 == 0)
                {
                    ChileElk.AgeFletcher()?.WifeMisery(ChileSick.UIMusic.Sound_GoldCoin1);
                }
            }));
            //金币移动到最终位置
            s.Append(GoldIcon.transform.DOMove(EndPosition, 0.6f).SetDelay(0.2f));
            s.Join(GoldIcon.transform.DOScale(1.5f, 0.3f).SetEase(Ease.InBack));
            s.Append(GoldIcon.transform.DOScale(0.1f, 0.18f).SetEase(Ease.OutQuad));
            s.AppendCallback(() =>
            {
                GoldIcon.transform.localScale = Vector3.one * 0.1f;
            });
            s.AppendInterval(0.12f);
            s.AppendCallback(() =>
            {
                //收尾
                s.Kill();
                Destroy(GoldIcon);
                if (t == a - 1)
                {
                    finish?.Invoke();
                }
            });
        }
    }

    /// <summary>
    /// UI收 钻石
    /// </summary>
    /// <param name="GoldImage">金币图标</param>
    /// <param name="a">金币数量</param>
    /// <param name="StartPosition">起始位置</param>
    /// <param name="EndPosition">最终位置</param>
    /// <param name="finish">结束回调</param>
    public static void UILinkageLoneCent(GameObject GoldImage, int a, Vector2 StartPosition, Vector2 EndPosition, System.Action finish)
    {
        //如果没有就算了
        if (a == 0)
        {
            finish?.Invoke();
            return;
        }
        //数量不超过15个
        if (a > 15)
        {
            a = 15;
        }

        if (a <= 0)
        {
            return;
        }

        if (ClanAwesome.Instance == null)
        {
            Debug.LogError("UIDiamondMoveBest: ClanAwesome.Instance 为空。");
            finish?.Invoke();
            return;
        }

        if (ClanAwesome.Instance.LetLinkageStarMoldSenior == null)
        {
            Debug.LogError("UIDiamondMoveBest: 请在 ClanAwesome 上配置 flyDiamondItemPoolPrefab。");
            finish?.Invoke();
            return;
        }

        Transform flyDiamondParent = GoldImage.transform.parent;
        //每个金币的间隔
        float Delaytime = 0f;
        for (int i = 0; i < a; i++)
        {
            int t = i;
            //每次延迟+1
            Delaytime += 0.06f;
            //复制一个图标
            GameObject GoldIcon = ClanAwesome.AgeLetLinkageStarMold(flyDiamondParent);
            if (GoldIcon == null)
            {
                Debug.LogError("UIDiamondMoveBest: 无法从 FlyDiamondItem 对象池取得实例。");
                if (t == a - 1)
                {
                    finish?.Invoke();
                }
                continue;
            }
            GoldIcon.SetActive(true);
            //金币弹出随机位置
            float OffsetX = UnityEngine.Random.Range(-1f, 1f);
            float OffsetY = UnityEngine.Random.Range(-1f, 1f);
            //初始化
            GoldIcon.transform.position = new Vector3(StartPosition.x + OffsetX, StartPosition.y + OffsetY);
            GoldIcon.transform.localScale = Vector3.zero;
            //创建动画队列
            var s = DOTween.Sequence();
            //金币出现
            s.Append(GoldIcon.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetDelay(Delaytime).SetEase(Ease.OutBack).OnComplete(() =>
            {
                //限制音效播放数量
                //if (Mathf.Sin(t) > 0)
                if (t % 5 == 0)
                {
                    ChileElk.AgeFletcher()?.WifeMisery(ChileSick.UIMusic.Sound_GoldCoin);
                }
            }));
            s.AppendCallback(() =>
            {
                LetStar fi = GoldIcon != null ? GoldIcon.GetComponent<LetStar>() : null;
                if (fi != null && fi.ToughMisery != null)
                    fi.ToughMisery.gameObject.SetActive(true);
            });
            //s.Append(GoldIcon.transform.DOMove(new Vector3(GoldIcon.transform.position.x + OffsetX, GoldIcon.transform.position.y + OffsetY, GoldIcon.transform.position.z), 0.15f));
            //金币移动到最终位置
            s.Append(GoldIcon.transform.DOMove(EndPosition, 0.5f).SetDelay(0.1f).SetEase(Ease.InCubic));
            s.AppendCallback(() =>
            {
                LetStar fi = GoldIcon != null ? GoldIcon.GetComponent<LetStar>() : null;
                if (fi != null && fi.ToughMisery != null)
                    fi.ToughMisery.gameObject.SetActive(false);
            });
            s.Append(GoldIcon.transform.DOScale(0.1f, 0.18f).SetEase(Ease.OutQuad));
            s.AppendCallback(() =>
            {
                GoldIcon.transform.localScale = Vector3.one * 0.1f;
            });
            s.AppendInterval(0.12f);
            s.AppendCallback(() =>
            {
                //收尾
                s.Kill();
                ClanAwesome.SolelyLetLinkageStarMold(GoldIcon, flyDiamondParent);
                if (t == a - 1)
                {
                    finish?.Invoke();
                }
            });
        }
    }

    // 鱼金币礼花筒飞币（世界空间量级与 GoldMoveBest 一致）
    private const float EaseJuicyVideoX= 0.8f;
    private const float EaseJuicyVideoAtKit= 0.35f;
    private const float EaseJuicyVideoAtRoe= 1.1f;
    private const float EaseJuicyScruffyCollapse= 0.15f;
    private const float EaseJuicyLetCollapse= 0.45f;

    private enum FishFlyPoolKind
    {
        Cash,
        Diamond,
    }

    /// <summary>
    /// 击杀鱼掉金币：使用 <see cref="ClanAwesome.CashPoolPrefab"/> 对象池，贝塞尔飞到 HUD 金币位。
    /// </summary>
    public static void EaseTalkLone(GameObject iconTemplate, int count, Transform startTransform, Transform endTransform, System.Action finish)
    {
        EaseLetLoneGuessMold(FishFlyPoolKind.Cash, iconTemplate, count, startTransform, endTransform, finish);
    }

    /// <summary>
    /// 击杀钻石鱼：使用 <see cref="ClanAwesome.DiamondPoolPrefab"/> 对象池，轨迹与 <see cref="FishGoldMove"/> 一致，落地粒子可用 MoteWould.FX_DiamondCollect（未配置则复用 FX_Collect）。
    /// </summary>
    public static void EaseLinkageLone(GameObject iconTemplate, int count, Transform startTransform, Transform endTransform, System.Action finish)
    {
        EaseLetLoneGuessMold(FishFlyPoolKind.Diamond, iconTemplate, count, startTransform, endTransform, finish);
    }

    /// <summary>鱼击杀飞货币公共实现：Cash / Diamond 仅池子与落地粒子预制不同；钻石飞条目父节点为 endTransform，金币在 FX_Cash 下。</summary>
    private static void EaseLetLoneGuessMold(FishFlyPoolKind kind, GameObject iconTemplate, int count, Transform startTransform, Transform endTransform, System.Action finish)
    {
        if (iconTemplate == null)
        {
            Debug.LogError("FishFlyMove: iconTemplate 不能为空！");
            finish?.Invoke();
            return;
        }

        if (endTransform == null)
        {
            Debug.LogError("FishFlyMove: endTransform 不能为空！");
            finish?.Invoke();
            return;
        }

        Vector3 startPos = startTransform.position;
        Vector3 endPos = endTransform.position;

        if (MoteWould.Instance == null || MoteWould.Instance.FX_Seed == null)
        {
            Debug.LogError("FishFlyMove: MoteWould.FX_Cash 不能为空！");
            finish?.Invoke();
            return;
        }

        GameObject fxCollectPrefab = MoteWould.Instance.FX_Mermaid;
        if (kind == FishFlyPoolKind.Diamond && MoteWould.Instance.FX_LinkageMermaid != null)
        {
            fxCollectPrefab = MoteWould.Instance.FX_LinkageMermaid;
        }

        if (fxCollectPrefab == null)
        {
            Debug.LogError("FishFlyMove: 落地粒子预制（FX_Collect / FX_DiamondCollect）不能为空！");
            finish?.Invoke();
            return;
        }

        count = Mathf.Clamp(count, 0, 15);
        if (count == 0)
        {
            finish?.Invoke();
            return;
        }

        Transform fxCashRoot = MoteWould.Instance.FX_Seed.transform;
        Transform itemParentTransform = kind == FishFlyPoolKind.Cash ? fxCashRoot : endTransform.parent;
        int remaining = count;
        float delayTime = 0f;

        if (ClanAwesome.Instance == null)
        {
            Debug.LogError("FishFlyMove: ClanAwesome.Instance 为空。");
            finish?.Invoke();
            return;
        }

        if (kind == FishFlyPoolKind.Cash && ClanAwesome.Instance.SeedMoldSenior == null)
        {
            Debug.LogError("FishFlyMove: 请在 ClanAwesome 上配置 cashPoolPrefab。");
            finish?.Invoke();
            return;
        }

        if (kind == FishFlyPoolKind.Diamond && ClanAwesome.Instance.LinkageMoldSenior == null)
        {
            Debug.LogError("FishFlyMove: 请在 ClanAwesome 上配置 diamondPoolPrefab。");
            finish?.Invoke();
            return;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject item = kind == FishFlyPoolKind.Cash
                ? ClanAwesome.AgeSeedMold(itemParentTransform)
                : ClanAwesome.AgeLinkageMold(itemParentTransform);

            if (item == null)
            {
                Debug.LogError("FishFlyMove: 无法从对象池获取实例（" + kind + "）。");
                remaining--;
                continue;
            }

            LetStar flyItem = item.GetComponent<LetStar>();
            if (flyItem == null)
            {
                Debug.LogError("FishFlyMove: LetStar 组件不存在！");
                if (kind == FishFlyPoolKind.Cash)
                    ClanAwesome.SolelySeedMold(item, itemParentTransform);
                else
                    ClanAwesome.SolelyLinkageMold(item, itemParentTransform);
                remaining--;
                continue;
            }

            if (flyItem.ToughMisery != null)
                flyItem.ToughMisery.gameObject.SetActive(false);
            item.transform.DOKill();
            item.transform.SetAsFirstSibling();
            item.SetActive(true);
            item.transform.localScale = Vector3.one;
            Image flyVisualImage = item.GetComponentInChildren<Image>(true);
            if (kind == FishFlyPoolKind.Diamond && flyVisualImage != null)
            {
                // FlyFishDiamondItem 的可见图层在子节点，默认缩放为 1.2；取池后先恢复，避免复用残留。
                flyVisualImage.transform.localScale = Vector3.one * 1.2f;
            }

            const float OffsetMin = -1f;
            const float OffsetMax = 1f;
            float offsetX = UnityEngine.Random.Range(OffsetMin, OffsetMax);
            float offsetY = UnityEngine.Random.Range(OffsetMin, OffsetMax);

            Vector3 popPosition = new Vector3(
                startPos.x + offsetX,
                startPos.y + offsetY,
                itemParentTransform.position.z
            );

            Vector3 midPos = new Vector3(
                (popPosition.x + endPos.x) / 2f,
                Mathf.Max(popPosition.y, endPos.y) - UnityEngine.Random.Range(1f, 2f),
                itemParentTransform.position.z
            );

            item.transform.position = popPosition;
            endPos.z = itemParentTransform.position.z;
         //   Vector3[] aniPath = BezierUtils.GetBeizerList(popPosition, midPos, endPos, 24);
            Vector3[] aniPath = BezierUtils.GetBeizerList(popPosition, midPos, endPos, 9);

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(item.transform.DOScale(1.5f, 0.2f).SetDelay(delayTime));
            sequence.Append(item.transform.DOScale(1f, 0.1f));
            sequence.AppendCallback(() =>
            {

                if (flyItem != null && flyItem.ToughMisery != null)
                    flyItem.ToughMisery.gameObject.SetActive(true);
            });
          //  sequence.Append(item.transform.DOPath(aniPath, 0.6f).SetDelay(0.25f).SetEase(Ease.InCubic).SetLookAt(0.001f, Vector3.right));
            sequence.Append(item.transform.DOPath(aniPath, 0.6f).SetDelay(0.25f).SetEase(Ease.InCubic));
            sequence.AppendCallback(() =>
            {
                if (flyItem != null && flyItem.ToughMisery != null)
                    flyItem.ToughMisery.gameObject.SetActive(false);
            });
            sequence.Append(item.transform.DOScale(0.1f, 0.18f).SetEase(Ease.OutQuad));
            sequence.AppendCallback(() =>
            {
                item.transform.localScale = Vector3.one * 0.1f;
                if (kind == FishFlyPoolKind.Diamond && flyVisualImage != null)
                {
                    flyVisualImage.transform.localScale = Vector3.one * 0.1f;
                }
            });
            sequence.AppendInterval(0.12f);
            sequence.AppendCallback(() =>
            {
                GameObject fxTargetEffect = AgeLayoutOnLikeMold(fxCollectPrefab, itemParentTransform);
                if (fxTargetEffect != null)
                {
                    fxTargetEffect.transform.SetAsFirstSibling();
                    fxTargetEffect.SetActive(true);
                    ShallowPakistanConcede(fxTargetEffect);
                    int fxPrefabId = fxCollectPrefab.GetInstanceID();
                    float fxDuration = StarlingOnCollapse(fxTargetEffect);
                    Tween returnTween = DOVirtual.DelayedCall(fxDuration + 0.05f, () =>
                    {
                        SolelyLayoutOnIDMold(fxTargetEffect, fxPrefabId, itemParentTransform);
                    });
                    if (s_LayoutOnSolelyWidowJay.TryGetValue(fxTargetEffect, out Tween oldTween) && oldTween != null)
                    {
                        oldTween.Kill();
                    }
                    s_LayoutOnSolelyWidowJay[fxTargetEffect] = returnTween;
                }

                sequence.Kill();
                if (kind == FishFlyPoolKind.Cash)
                    ClanAwesome.SolelySeedMold(item, itemParentTransform);
                else
                    ClanAwesome.SolelyLinkageMold(item, itemParentTransform);

                remaining--;

                if (remaining == count - 1 && iconTemplate != null)
                {
                    TalkResort(iconTemplate.transform, count);
                }

                if (remaining == 0)
                {
                    finish?.Invoke();
                    if (kind == FishFlyPoolKind.Cash)
                        ChileElk.AgeFletcher()?.WifeMisery(ChileSick.UIMusic.Sound_GoldCoin1);
                    else
                        ChileElk.AgeFletcher()?.WifeMisery(ChileSick.UIMusic.Sound_GoldCoin);
                }
            });

            const float DelayMin = 0.05f;
            const float DelayMax = 0.08f;
            delayTime += UnityEngine.Random.Range(DelayMin, DelayMax);
        }
    }
    /// <summary>
    /// 金币弹跳动画
    /// </summary>
    /// <param name="targetTransform">目标变换组件</param>
    /// <param name="count">金币数量（用于计算循环次数）</param>
    public static void TalkResort(Transform targetTransform, int count)
    {
        // 空值校验
        if (targetTransform == null)
        {
            Debug.LogError("GoldBounce: targetTransform 不能为空！");
            return;
        }

        // 保持原有动画逻辑，优化变量命名和可读性
        DG.Tweening.Sequence goldBounceSequence = DOTween.Sequence();
        goldBounceSequence.Append(targetTransform.DOScale(1.4f, 0.05f));
        goldBounceSequence.Append(targetTransform.DOScale(1f, 0.05f));

        // 计算循环次数（避免负数/0次循环）
        int loopCount = Mathf.Max(1, count * 3 / 5);
        goldBounceSequence.SetLoops(loopCount);
    }

    /// <summary>
    /// 横向滚动
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="addPosition"></param>
    /// <param name="Finish"></param>
    public static void EcologicalScroll(GameObject obj, float addPosition, System.Action Finish)
    {
        float positionX = obj.transform.localPosition.x;
        float endPostion = positionX + addPosition;
        obj.transform.DOLocalMoveX(endPostion, 2f).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            Finish?.Invoke();
        });
    }

    /// <summary>
    /// 小游戏开门：在 <see cref="MoteWould"/> 根下实例化 <paramref name="flyPrefab"/>，从鱼死亡世界坐标飞到<strong>该父节点本地 (0,0,0)</strong>，结束销毁实例。
    /// </summary>
    public static void FirmClanArrayTwinLetIDHankerGovern(
        GameObject flyPrefab,
        Vector3 fishDeathWorldPosition,
        System.Action onComplete,
        float flyDuration = 0.55f)
    {
        if (flyPrefab == null)
        {
            onComplete?.Invoke();
            return;
        }

        if (MoteWould.Instance == null)
        {
            Debug.LogWarning("[TraditionDemobilize] MiniGameIntroIconFly: MoteWould 为空。");
            onComplete?.Invoke();
            return;
        }



        Transform flyParent = MoteWould.Instance.transform;
        GameObject go = UnityEngine.Object.Instantiate(flyPrefab, flyParent);
        Transform t = go.transform;
        t.localScale = Vector3.zero;
        t.localPosition = flyParent.InverseTransformPoint(fishDeathWorldPosition);

        Vector3 midPos = new Vector3(t.localPosition.x / 2f,
                Mathf.Max(t.localPosition.y, 0) - UnityEngine.Random.Range(1f, 2f),
                t.localPosition.z);
        Vector3[] aniPath = BezierUtils.GetBeizerList(t.localPosition, midPos, Vector3.zero, 24);
        t.DOScale(1.5f, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            t.DOScale(3, flyDuration).SetEase(Ease.OutBack);
            t.DOLocalPath(aniPath, flyDuration).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                t.DOScale(0, flyDuration).SetEase(Ease.InBack).SetDelay(0.5f).OnComplete(() =>
                {
                    t.DOKill();
                    UnityEngine.Object.Destroy(go);
                    onComplete?.Invoke();
                });

            });
        });
    }

    public static void DelineateTransmit(Transform item)
    {
        //item.transform.localScale = Vector3.one;
        item.DOKill();
        DOTween.Kill(item);
        item.DOScale(0.7f, 0.1f).OnComplete(() =>
        {
            if (item != null)
                item.DOScale(1, 0.1f);
        });
    }
}
