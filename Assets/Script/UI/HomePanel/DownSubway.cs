using UnityEngine;

public class DownSubway : MonoBehaviour
{
    private const string WallDon= "Wall";
    private const string EaseDon= "Fish";

    private bool m_KierGazellePopulateWindBeam;
    private bool m_KierHisLiquidFadPopulateWindDownBeam;

    private void OnEnable()
    {
        m_KierGazellePopulateWindBeam = false;
        m_KierHisLiquidFadPopulateWindDownBeam = false;
        BarelyIon.ToDownBeamCompile -= ChartKierBeamOvert;
        BarelyIon.ToDownBeamCompile += ChartKierBeamOvert;
    }

    private void OnDisable()
    {
        BarelyIon.ToDownBeamCompile -= ChartKierBeamOvert;
    }

    void ChartKierBeamOvert()
    {
        m_KierGazellePopulateWindBeam = false;
        m_KierHisLiquidFadPopulateWindDownBeam = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (!ClanAwesome.Instance.AxDownTonal)
        {
            return;
        }
        if (ClanAwesome.Instance.AgeJoyDownHP() <= 0)
        {
            return;
        }
        if (other.CompareTag(WallDon))
        {
            BarelyIon.ToEtchEven?.Invoke();
            Debug.Log($"DownSubway 碰到墙体(Trigger): {other.name}");
        }

        if (ClanAwesome.Instance.AgeJoyDownHP () > 0)
        {
            // 允许弱点/子碰撞器不带 Tag=Fish：只要它隶属 UIEaseDeluge 就算命中。
            UIEaseDeluge uIFishEntity = other.GetComponentInParent<UIEaseDeluge>();
            if (uIFishEntity != null)
            {
                BarelyIon.ToEtchEase?.Invoke();
                UIEaseDeluge.SunAccuseKierGazelleSeepageGildBoxBeam(uIFishEntity, other, ref m_KierGazellePopulateWindBeam);
                uIFishEntity.WifeFadMeCondense(other, ref m_KierHisLiquidFadPopulateWindDownBeam);
                Debug.Log($"DownSubway 碰到鱼(Trigger): {other.name}");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag(WallDon)) return;
        Debug.Log($"DownSubway 碰到墙体(Collision): {collision.collider.name}");
    }
}
