using UnityEngine;
using UnityEngine.UI;

/// <summary> 屏蔽界面 阻止玩家操作 退出游戏 </summary>
public class DrapeWould : ShedUIHobby
{
[UnityEngine.Serialization.FormerlySerializedAs("InfoText")]    public Text InfoWelt;
[UnityEngine.Serialization.FormerlySerializedAs("QuitBtn")]    public Button BookLad;

    private void Start()
    {
        BookLad.onClick.AddListener(Application.Quit);
    }

    public void DaleSlum(string info)
    {
        InfoWelt.text = info;
    }
}
