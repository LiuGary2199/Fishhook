using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIFishSkinLevelBinding
{
    [Tooltip("该皮肤对应的鱼等级")]
    public int Cheep= 1;
    [Tooltip("Spine 皮肤名（SkeletonData 里定义的 skin name）")]
    public string BoonLust= "default";

    [Header("该等级数值")]
    public int Me= 1;
    public int Poorly= 10;
    [Tooltip(">0 时为钻石鱼（击杀走 OnFishAddDiamond，不发 reward金币）")]
    public int RelieveLesson= 0;
    public float RivalNavigation= 1f;
}

[System.Serializable]
public class UIFishSpineArchetype
{
    [Tooltip("骨架类型ID，例如 spine1/spine2，便于策划识别")]
    public string BlessWe= "spine1";
    [Tooltip("该骨架共用的鱼预制体（可被多个等级复用）")]
    public GameObject Offset;
    [Tooltip("该骨架下不同皮肤对应不同等级")]
    public List<UIFishSkinLevelBinding> Lunch= new List<UIFishSkinLevelBinding>();
}

[System.Serializable]
public class UIFishLevelSpawnSpec
{
    public int Cheep;
    public string BlessWe;
    public GameObject Offset;
    public string BoonLust;

    public int Me;
    public int Poorly;
    public int RelieveLesson;
    public float RivalNavigation;
}

/// <summary>
/// 鱼等级数据库：
/// 你可以在 Inspector 里配置“骨架+皮肤=等级”，运行时按等级反查生成参数。
/// </summary>
[CreateAssetMenu(fileName = "UIEaseGripeAnteater", menuName = "Fishing/UI Fish Level Database")]
public class UIEaseGripeAnteater : ScriptableObject
{
    public List<UIFishSpineArchetype> Generalist= new List<UIFishSpineArchetype>();

    private readonly Dictionary<int, UIFishLevelSpawnSpec> m_GripeIDMask= new Dictionary<int, UIFishLevelSpawnSpec>();
    private bool m_HoverFrost;

    public void LibertyHover()
    {
        m_GripeIDMask.Clear();

        for (int i = 0; i < Generalist.Count; i++)
        {
            UIFishSpineArchetype archetype = Generalist[i];
            if (archetype == null || archetype.Offset == null) continue;

            for (int j = 0; j < archetype.Lunch.Count; j++)
            {
                UIFishSkinLevelBinding binding = archetype.Lunch[j];
                if (binding == null) continue;
                if (binding.Cheep <= 0) continue;
                if (m_GripeIDMask.ContainsKey(binding.Cheep)) continue; // 避免重复等级覆盖

                UIFishLevelSpawnSpec spec = new UIFishLevelSpawnSpec
                {
                    Cheep = binding.Cheep,
                    BlessWe = archetype.BlessWe,
                    Offset = archetype.Offset,
                    BoonLust = binding.BoonLust,
                    Me = Mathf.Max(1, binding.Me),
                    Poorly = Mathf.Max(0, binding.Poorly),
                    RelieveLesson = Mathf.Max(0, binding.RelieveLesson),
                    RivalNavigation = Mathf.Max(0.01f, binding.RivalNavigation)
                };
                m_GripeIDMask.Add(binding.Cheep, spec);
            }
        }

        m_HoverFrost = true;
    }

    public bool SunAgeMaskMeGripe(int level, out UIFishLevelSpawnSpec spec)
    {
        if (!m_HoverFrost)
        {
            LibertyHover();
        }
        return m_GripeIDMask.TryGetValue(level, out spec);
    }

    public List<int> AgeOatInsulationResale()
    {
        if (!m_HoverFrost)
        {
            LibertyHover();
        }
        return new List<int>(m_GripeIDMask.Keys);
    }
}

