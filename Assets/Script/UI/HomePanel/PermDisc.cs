using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PermDisc : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("m_ShipSkeleton")]
    public SkeletonGraphic m_PermAllusion;
    private string PermRomeDiscLust= "idle_blank";
    private string PermPastDiscLust= "idle_fire";
[UnityEngine.Serialization.FormerlySerializedAs("m_GunGraphic")]    public SkeletonGraphic m_LopPaucity;
    private string LopPastMelt= "idle_blank";
    private string LopMyGonging= "idle_cock";
    private string LopPast= "idle_fire";
    private string LopLMyHalfMelt= "idle_fully";



    public void Cape()
    {
        m_PermAllusion.AnimationState.Complete += OnShipAnimComplete;
        m_LopPaucity.AnimationState.Complete += OnGunAnimComplete;
        m_PermAllusion.AnimationState.SetAnimation(0, PermRomeDiscLust, true);
        m_LopPaucity.AnimationState.SetAnimation(0, LopMyGonging, false);

    }
    public void WifePermPast()
    {
        m_PermAllusion.AnimationState.SetAnimation(0, PermPastDiscLust, false);
    }
    public void WifeLopPast()
    {
        m_LopPaucity.Skeleton.SetToSetupPose();
        m_LopPaucity.AnimationState.ClearTracks();
        var entry =  m_LopPaucity.AnimationState.SetAnimation(0, LopPast, false);
        if (entry != null) entry.TimeScale = 5;
    }
     public void WifeLopMyStinger()
    {
         m_LopPaucity.Skeleton.SetToSetupPose();
        m_LopPaucity.AnimationState.ClearTracks();
        var entry = m_LopPaucity.AnimationState.SetAnimation(0, LopMyGonging, false);
        if (entry != null)
        {
            bool isFerverTime = ClanAwesome.Instance != null && ClanAwesome.Instance.ClanSick == GameType.FerverTime;
            entry.TimeScale = isFerverTime ? 8f : 0.4f;
        }
    }

    public void OnShipAnimComplete(TrackEntry trackEntry)
    {
        if (trackEntry == null || trackEntry.Animation == null) return;
        if (string.IsNullOrEmpty(PermPastDiscLust)) return;

        if (trackEntry.Animation.Name == PermPastDiscLust)
        {
            m_PermAllusion.AnimationState.SetAnimation(0, PermRomeDiscLust, true);
        }
    }
    public void OnGunAnimComplete(TrackEntry trackEntry)
    {
        if (trackEntry == null || trackEntry.Animation == null) return;
        if (string.IsNullOrEmpty(LopPastMelt)) return;

        if (trackEntry.Animation.Name == LopPastMelt)
        {
            m_LopPaucity.AnimationState.SetAnimation(0, LopPast, true);
        }
    }
}
