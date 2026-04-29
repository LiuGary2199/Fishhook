using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

/// <summary>
/// 调试/编辑器快捷键：按键生成「鱼群形状」编队。
/// 仅保留 FishSchoolShape 方案；旧 V/双环鱼潮入口已移除。
/// </summary>
public class UIEaseFootPrepayDepress : MonoBehaviour
{
    [Header("Target")]
[UnityEngine.Serialization.FormerlySerializedAs("fishSwimSystem")]    public UIEaseBergBureau VaseBergBureau;

    [Header("鱼群形状（FishSchoolShape）")]
    [Tooltip("可配置多个鱼群形状，自动刷新时会随机抽一个播放")]
[UnityEngine.Serialization.FormerlySerializedAs("fishSchoolShapes")]    public List<FishSchoolShape> VaseRetardShapes= new List<FishSchoolShape>();
    [Tooltip("手动热键播放使用的索引（越界自动回 0）")]
[UnityEngine.Serialization.FormerlySerializedAs("manualShapeIndex")]    public int LonelyShapeSmile= 0;
[UnityEngine.Serialization.FormerlySerializedAs("fishSchoolSpawnKey")]    public KeyCode VaseRetardAlikeLet= KeyCode.Alpha3;

    [Header("自动鱼潮刷新")]
    [Tooltip("是否按服务器 GameData.fish_shoal_cd 定时刷新鱼潮")]
[UnityEngine.Serialization.FormerlySerializedAs("autoPlayFishShoal")]    public bool FortWifeEaseWater= true;
    [Tooltip("服务器未下发 fish_shoal_cd 时的本地兜底（秒）")]
    [Min(0.1f)]
[UnityEngine.Serialization.FormerlySerializedAs("fallbackFishShoalCd")]    public float EuropeanEaseWaterOn= 12f;

    private float m_EaseWaterTexas;
    private float m_EaseWaterCd;
    private int m_MeanPropelBingeSmile= -1;

    /// <summary>应用处于后台/失焦：鱼群 CD 必须完全不计时（含 PC 仍跑 Update 的情况）。</summary>
    private bool m_AppInBackground;

    private void Awake()
    {
        if (VaseBergBureau == null)
        {
            VaseBergBureau = FindFirstObjectByType<UIEaseBergBureau>();
        }

        ReclaimEaseWaterOn();
        m_AppInBackground = false;
    }

    private void OnEnable()
    {
        m_EaseWaterTexas = 0f;
        ReclaimEaseWaterOn();
        m_AppInBackground = false;
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            OnEnterBackground();
        }
        else
        {
            OnLeaveBackground();
        }
    }

    /// <summary>Standalone / Editor 常靠失焦判断，不能只依赖 OnApplicationPause。</summary>
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            OnEnterBackground();
        }
        else
        {
            OnLeaveBackground();
        }
    }

    private void OnEnterBackground()
    {
        if (m_AppInBackground)
        {
            return;
        }

        m_AppInBackground = true;
    }

    private void OnLeaveBackground()
    {
        if (!m_AppInBackground)
        {
            return;
        }

        m_AppInBackground = false;
    }

    private void Update()
    {
        if (VaseBergBureau == null) return;

        //  if (Input.GetKeyDown(VaseRetardAlikeLet))
        bool isFerverTime = IsInFerverTime();
        if (!isFerverTime && Input.GetKeyDown(VaseRetardAlikeLet))
        {
            AlikeEaseRetardLikeBingeHairy(AgeSuburbBinge());
        }

        if (FortWifeEaseWater)
        {
            //BaskPikeEaseWater(Time.deltaTime);
           // BaskPikeEaseWater(Time.deltaTime, isFerverTime);
           // float dt = m_AppInBackground ? 0f : Time.deltaTime;
          //  bool freezeCountdown = m_AppInBackground || (ClanAwesome.Instance != null && ClanAwesome.Instance.AxSparselyPaused);
            bool freezeCountdown = m_AppInBackground  || (ClanAwesome.Instance != null && ClanAwesome.Instance.AxSparselyPaused)
         || (ClanAwesome.Instance != null && ClanAwesome.Instance.IsFerverProximityStagingBlock());
            float dt = freezeCountdown ? 0f : Time.deltaTime;
            BaskPikeEaseWater(dt, isFerverTime);
        }
    }

    /// <summary>
    /// 按形状资源参数生成一整队（同速、同缩放、无个体弧线）。
    /// </summary>
    public void AlikeEaseRetardLikeBingeHairy(FishSchoolShape shape)
    {
        if (VaseBergBureau == null)
        {
            VaseBergBureau = FindFirstObjectByType<UIEaseBergBureau>();
        }
        if (VaseBergBureau == null || shape == null) return;

        int resolvedDir = shape.ResolveSpawnDir(shape.fallbackDir);
        bool mirrorX = FishSchoolShape.ResolveMirrorShapeX(shape.mirrorMode, resolvedDir);

        float cx = shape.manualCenterX;
        if (shape.autoCenterX)
        {
            cx = VaseBergBureau.AgeLaboriousEaseRetardGovernFolkloreX(
                shape,
                resolvedDir,
                shape.cellSpacingX,
                mirrorX);
        }

        if (shape.spawnStaggerSeconds > 0f)
        {
            VaseBergBureau.AlikeEaseRetardLikeBingeSupremely(
                shape,
                cx,
                shape.centerY,
                resolvedDir,
                shape.speed,
                shape.cellSpacingX,
                shape.cellSpacingY,
                mirrorX,
                shape.spawnStaggerSeconds);
        }
        else
        {
            VaseBergBureau.AlikeEaseRetardLikeBinge(
                shape,
                cx,
                shape.centerY,
                resolvedDir,
                shape.speed,
                shape.cellSpacingX,
                shape.cellSpacingY,
                mirrorX);
        }
    }

    private void BaskPikeEaseWater(float deltaTime,bool isFerverTime)
    {
        if (VaseRetardShapes == null || VaseRetardShapes.Count == 0)
        {
            return;
        }

        // Ferver 期间暂停鱼潮倒计时，退出后继续剩余倒计时。
        if (isFerverTime)
        {
            return;
        }
        // 广告播放期间冻结鱼潮倒计时；广告结束后继续累计剩余时间。
        if (IsAdPlaying())
        {
            return;
        }

        if (m_EaseWaterCd <= 0f)
        {
            ReclaimEaseWaterOn();
            if (m_EaseWaterCd <= 0f)
            {
                return;
            }
        }

        m_EaseWaterTexas += Mathf.Max(0f, deltaTime);
        if (m_EaseWaterTexas < m_EaseWaterCd)
        {
            return;
        }

        m_EaseWaterTexas = 0f;
        FishSchoolShape randomShape = AgePropelBingeHingeKernel();
        if (randomShape != null)
        {
            AlikeEaseRetardLikeBingeHairy(randomShape);
        }
    }
    private static bool IsInFerverTime()
    {
        return ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;
    }

    private static bool IsAdPlaying()
    {
        return ADAwesome.Fletcher != null && ADAwesome.Fletcher.IsShowingAd;
    }
    private void ReclaimEaseWaterOn()
    {
        float serverCd = ClanGushAwesome.AgeFletcher() != null ? ClanGushAwesome.AgeFletcher().AgeEaseWaterOn() : 0f;
        m_EaseWaterCd = serverCd > 0f ? serverCd : Mathf.Max(0.1f, EuropeanEaseWaterOn);
    }

    private FishSchoolShape AgeSuburbBinge()
    {
        if (VaseRetardShapes == null || VaseRetardShapes.Count == 0)
        {
            return null;
        }

        int safeIndex = Mathf.Clamp(LonelyShapeSmile, 0, VaseRetardShapes.Count - 1);
        return VaseRetardShapes[safeIndex];
    }

    private FishSchoolShape AgePropelBingeHingeKernel()
    {
        if (VaseRetardShapes == null || VaseRetardShapes.Count == 0)
        {
            return null;
        }

        int count = VaseRetardShapes.Count;
        List<int> validIndices = new List<int>(count);
        for (int i = 0; i < count; i++)
        {
            if (VaseRetardShapes[i] != null)
            {
                validIndices.Add(i);
            }
        }

        if (validIndices.Count == 0)
        {
            return null;
        }

        // 可选项大于 1 时，避免与上一次相同。
        if (validIndices.Count > 1 && m_MeanPropelBingeSmile >= 0)
        {
            for (int i = validIndices.Count - 1; i >= 0; i--)
            {
                if (validIndices[i] == m_MeanPropelBingeSmile)
                {
                    validIndices.RemoveAt(i);
                    break;
                }
            }
        }

        int chosen = validIndices[Random.Range(0, validIndices.Count)];
        m_MeanPropelBingeSmile = chosen;
        return VaseRetardShapes[chosen];
    }

    public void AlikePavlovaFoot()
    {
        FishSchoolShape shape = AgePropelBingeHingeKernel();
        if (shape != null)
        {
            AlikeEaseRetardLikeBingeHairy(shape);
        }
    }
}

