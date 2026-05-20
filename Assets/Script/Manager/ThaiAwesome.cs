using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThaiAwesome : MonoBehaviour
{
    public static ThaiAwesome instance;

    private bool Habit= false;

    private void Awake()
    {
        instance = this;
    }

    public void PairCape()
    {
        bool isNewPlayer = !PlayerPrefs.HasKey(CMillet.If_AxEarSailor + "Bool") || SpotGushAwesome.GetBool(CMillet.If_AxEarSailor);

        ArouseCapeAwesome.Instance.CapeArouseGush(isNewPlayer);

        if (isNewPlayer)
        {
            // 新用户
            SpotGushAwesome.SetBool(CMillet.If_AxEarSailor, false);
        }


        ChileElk.AgeFletcher().WifeNo(ChileSick.SceneMusic.Sound_BGM);
        if (PlayerPrefs.GetInt("da2prx") > 0)
        {
            AIGamePlusManager.AgeFletcher().SendEvent("da2prx");
        }
        if (PlayerPrefs.GetInt("2cgw82") > 0)
        {
            AIGamePlusManager.AgeFletcher().SendEvent("2cgw82");
        }
        if (PlayerPrefs.GetInt("7sfuth") > 0)
        {
            AIGamePlusManager.AgeFletcher().SendEvent("7sfuth");
        }


        UIAwesome.AgeFletcher().DaleUIHobby(nameof(MoteWould));
        QuitCacheCandle.AgeFletcher().HornCache("1001");
        Habit = true;
    }

    //切前后台也需要检测屏蔽 防止游戏中途更改手机状态
    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
            PotionUtil.SeveralDrapePhase();
    }
}
