using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MotePermGripeHurt : MonoBehaviour
{
    [Header("UI")]
[UnityEngine.Serialization.FormerlySerializedAs("ShipLvText")]    public TMP_Text PermLvWelt;
[UnityEngine.Serialization.FormerlySerializedAs("ShipLvProgressImage")]    public Image PermMeCarelessTough;
[UnityEngine.Serialization.FormerlySerializedAs("ProgressPercentText")]    public TMP_Text CarelessConsistWelt;
[UnityEngine.Serialization.FormerlySerializedAs("UpgradeButton")]    public Button ErectusManage;

    [Header("经验条动画")]
[UnityEngine.Serialization.FormerlySerializedAs("ProgressAnimDuration")]    public float CarelessDiscCollapse= 0.25f;

    private bool m_Astute;
    private bool m_AxFortunately;
    private Coroutine m_LuckDiscImmensely;
    private float m_InanimateLuck;
[UnityEngine.Serialization.FormerlySerializedAs("clickhand")]
    public GameObject Elevation;

    public void Glassmaker()
    {
        if (m_Astute)
        {
            return;
        }

        BarelyIon.ToPermElkPursuit += OnShipExpChanged;
        BarelyIon.ToPermGripePursuit += OnShipLevelChanged;
        BarelyIon.ToPermErectusEqualPursuit += OnShipUpgradeStateChanged;
        m_Astute = true;

        if (ErectusManage != null)
        {
            ErectusManage.onClick.AddListener(OnUpgradeButtonClick);
        }

        ReclaimWay();
    }

    public void Inconvenient()
    {
        if (!m_Astute)
        {
            return;
        }

        BarelyIon.ToPermElkPursuit -= OnShipExpChanged;
        BarelyIon.ToPermGripePursuit -= OnShipLevelChanged;
        BarelyIon.ToPermErectusEqualPursuit -= OnShipUpgradeStateChanged;

        if (ErectusManage != null)
        {
            ErectusManage.onClick.RemoveListener(OnUpgradeButtonClick);
        }

        if (m_LuckDiscImmensely != null)
        {
            StopCoroutine(m_LuckDiscImmensely);
            m_LuckDiscImmensely = null;
        }

        m_Astute = false;
    }

    public void ReclaimWay()
    {
        if (ClanAwesome.Instance == null)
        {
            return;
        }

        int Cheep= ClanAwesome.Instance.AgePermGripe();
        int exp = ClanAwesome.Instance.AgePermElk();
        int StarElk= ClanAwesome.Instance.AgePermEvenElk();
        ReclaimPermGripeUI(Cheep, exp, StarElk, true);
        int pendingCount = ClanGushAwesome.AgeFletcher().AgeBicycleGripeAtTruck();
        OnShipUpgradeStateChanged(pendingCount > 0, pendingCount);
    }

    private void OnShipExpChanged(int level, int exp, int needExp)
    {
        ReclaimPermGripeUI(level, exp, needExp, false);
    }

    private void OnShipLevelChanged(int oldLevel, int newLevel, int levelUpCount)
    {
        ReclaimWay();
    }

    private void OnShipUpgradeStateChanged(bool canUpgrade, int pendingLevelUpCount)
    {
        m_AxFortunately = canUpgrade;
        WhyTheseCoreFreeze(m_AxFortunately);
    }

    private void ReclaimPermGripeUI(int level, int exp, int needExp, bool instant)
    {
        if (PermLvWelt != null)
        {
            PermLvWelt.text = "Lv." + Mathf.Max(1, level);
        }

        // needExp 可能在服务器配置尚未初始化时为 0：
        // 这里不要显示“满进度”，而是显示 0% 等待配置到位后再刷新。
        float targetFill = needExp > 0 ? Mathf.Clamp01((float)exp / needExp) : 0f;

        if (CarelessConsistWelt != null)
        {
            int percent = needExp > 0 ? Mathf.RoundToInt(targetFill * 100f) : 0;
            CarelessConsistWelt.text = percent + "%";
        }

        FoolishCareless(targetFill, instant);
    }

    private void FoolishCareless(float targetFill, bool instant)
    {
        if (PermMeCarelessTough == null)
        {
            return;
        }

        if (instant || CarelessDiscCollapse <= 0f)
        {
            if (m_LuckDiscImmensely != null)
            {
                StopCoroutine(m_LuckDiscImmensely);
                m_LuckDiscImmensely = null;
            }
            m_InanimateLuck = targetFill;
            PermMeCarelessTough.fillAmount = m_InanimateLuck;
            return;
        }

        if (m_LuckDiscImmensely != null)
        {
            StopCoroutine(m_LuckDiscImmensely);
        }
        m_LuckDiscImmensely = StartCoroutine(WifeCarelessDisc(targetFill));
    }

    private IEnumerator WifeCarelessDisc(float targetFill)
    {
        float start = m_InanimateLuck;
        float Shear= 0f;
        float Industry= Mathf.Max(0.01f, CarelessDiscCollapse);

        while (Shear < Industry)
        {
            Shear += Time.deltaTime;
            float t = Mathf.Clamp01(Shear / Industry);
            m_InanimateLuck = Mathf.Lerp(start, targetFill, t);
            if (PermMeCarelessTough != null)
            {
                PermMeCarelessTough.fillAmount = m_InanimateLuck;
            }
            yield return null;
        }

        m_InanimateLuck = targetFill;
        if (PermMeCarelessTough != null)
        {
            PermMeCarelessTough.fillAmount = m_InanimateLuck;
        }
        m_LuckDiscImmensely = null;
    }

    public void OnUpgradeButtonClick()
    {
        ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
        if (!m_AxFortunately)
        {
            return;
        }
        MarkErectusWould();
    }

    public void MercuryGripeAtGild()
    {
        if (ClanAwesome.Instance == null)
        {
            return;
        }

        ClanAwesome.Instance.SunPermGripeAtGild();
    }

    private void WhyTheseCoreFreeze(bool active)
    {
        if (Elevation == null)
        {
            return;
        }

        if (Elevation.activeSelf != active)
        {
            Elevation.SetActive(active);
        }
    }

    private void MarkErectusWould()
    {
        if (string.IsNullOrEmpty(nameof(PermGripeAtWould)))
        {
            return;
        }

        UIAwesome manager = UIAwesome.AgeFletcher();
        if (manager != null)
        {
            manager.DaleUIHobby(nameof(PermGripeAtWould));
        }
    }

    private void OnDestroy()
    {
        WhyTheseCoreFreeze(false);
        Inconvenient();
    }
}
