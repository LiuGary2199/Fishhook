using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 基础UI窗体脚本（父类，其他窗体都继承此脚本）
/// </summary>
public class ShedUIHobby : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("_CurrentUIType")]    //当前（基类）窗口的类型
    public UISick _ReliantUISick= new UISick();
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("close_button")]    public Button Laser_Window;
    private bool m_ArmChemistrySparselyBlade;
    //属性，当前ui窗体类型
    internal UISick ReliantUISick    {
        set
        {
            _ReliantUISick = value;
        }
        get
        {
            return _ReliantUISick;
        }
    }
    protected virtual void Awake()
    {
        CureScrubDewMacdonald(gameObject);
        if (transform.Find("Window/Content/CloseBtn"))
        {
            Laser_Window = transform.Find("Window/Content/CloseBtn").GetComponent<Button>();
            Laser_Window.onClick.AddListener(() => {
                UIAwesome.AgeFletcher().BloodSoSolelyUIHobby(this.GetType().Name);
            });
        }
        if (_ReliantUISick.UIForms_Type == UIFormType.PopUp)
        {
            gameObject.AddComponent<CanvasGroup>();
        }
        gameObject.name = GetType().Name;
    }


    public static void CureScrubDewMacdonald(GameObject goParent)
    {
        Transform parent = goParent.transform;
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform chile = parent.GetChild(i);
            if (chile.GetComponent<Button>())
            {
                chile.GetComponent<Button>().onClick.AddListener(() => {

                    ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
                });
            }
            
            if (chile.childCount > 0)
            {
                CureScrubDewMacdonald(chile.gameObject);
            }
        }
    }

    //页面显示
    public virtual void Display(object uiFormParams)
    {
        //Debug.Log(this.GetType().Name);
        this.gameObject.SetActive(true);
        // 设置模态窗体调用(必须是弹出窗体)
        if (_ReliantUISick.UIForms_Type == UIFormType.PopUp && _ReliantUISick.UIForm_LucencyType != UIFormLucenyType.NoMask)
        {
            UIIronElk.AgeFletcher().WhyIronSubway(this.gameObject, _ReliantUISick.UIForm_LucencyType);
        }
        if (_ReliantUISick.UIForms_Type == UIFormType.PopUp)
        {
            SunBladeSparselyFitSlime();

            //动画添加
            switch (_ReliantUISick.UIForm_animationType)
            {
                case UIFormShowAnimationType.scale:
                    TraditionDemobilize.SeeDale(gameObject, () =>
                    {

                    });
                    break;

            }
            
        }
        //NewUserManager.GetInstance().TriggerEvent(TriggerType.panel_display);
    }
    //页面隐藏（不在栈集合中）
    public virtual void Hidding(System.Action finish = null)
    {
        //if (_CurrentUIType.UIForms_Type == UIFormType.PopUp && _CurrentUIType.UIForm_LucencyType != UIFormLucenyType.NoMask)
        //{
        //    UIIronElk.GetInstance().HideMaskWindow();
        //}

        //取消模态窗体调用

        if (_ReliantUISick.UIForms_Type == UIFormType.PopUp)
        {
            switch (_ReliantUISick.UIForm_animationType)
            {
                case UIFormShowAnimationType.scale:
                    TraditionDemobilize.SeeHone(gameObject, () =>
                    {
                        SunSecureSparselyFitSlime();
                        this.gameObject.SetActive(false);
                        if (_ReliantUISick.UIForms_Type == UIFormType.PopUp && _ReliantUISick.UIForm_LucencyType != UIFormLucenyType.NoMask)
                        {
                            UIIronElk.AgeFletcher().NamelyIronSubway();
                        }
                        UIAwesome.AgeFletcher().DaleWaleSeeAt();
                        finish?.Invoke();
                    });
                    break;
                case UIFormShowAnimationType.none:
                    SunSecureSparselyFitSlime();
                    this.gameObject.SetActive(false);
                    //if (_CurrentUIType.UIForms_Type == UIFormType.PopUp && _CurrentUIType.UIForm_LucencyType != UIFormLucenyType.NoMask)
                    //{
                    //    UIIronElk.GetInstance().CancelMaskWindow();
                    //}
                    UIAwesome.AgeFletcher().DaleWaleSeeAt();
                    finish?.Invoke();
                    break;

            }

        }
        else
        {
            SunSecureSparselyFitSlime();
            this.gameObject.SetActive(false);
            if (_ReliantUISick.UIForms_Type == UIFormType.PopUp && _ReliantUISick.UIForm_LucencyType != UIFormLucenyType.NoMask)
            {
                UIIronElk.AgeFletcher().NamelyIronSubway();
            }
            finish?.Invoke();
        }
    }

    protected virtual bool DebateBladeSparselyCaveSlimeShown()
    {
        return true;
    }

    public void SunBladeSparselyFitSlime()
    {
        if (m_ArmChemistrySparselyBlade) return;
        if (_ReliantUISick.UIForms_Type != UIFormType.PopUp) return;
        if (!DebateBladeSparselyCaveSlimeShown()) return;
        if (ClanAwesome.Instance == null) return;

        ClanAwesome.Instance.BladeSparsely();
        m_ArmChemistrySparselyBlade = true;
    }

    public void SunSecureSparselyFitSlime()
    {
        if (!m_ArmChemistrySparselyBlade) return;
        m_ArmChemistrySparselyBlade = false;
        ClanAwesome.Instance?.SecureSparsely();
    }

    protected virtual void OnDisable()
    {
        // 兜底：避免弹窗被外部直接 SetActive(false) 时遗留暂停深度。
        SunSecureSparselyFitSlime();
    }

    public virtual void Hidding()
    {
        Hidding(null);
    }

    //页面重新显示
    public virtual void Redisplay()
    {
        this.gameObject.SetActive(true);
        if (_ReliantUISick.UIForms_Type == UIFormType.PopUp)
        {
            UIIronElk.AgeFletcher().WhyIronSubway(this.gameObject, _ReliantUISick.UIForm_LucencyType); 
        }
    }
    //页面冻结（还在栈集合中）
    public virtual void Cavern()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 注册按钮事件
    /// </summary>
    /// <param name="buttonName">按钮节点名称</param>
    /// <param name="delHandle">委托，需要注册的方法</param>
    protected void MonarchyManageSubwayCache(string buttonName,CacheSeepageEpisodic.VoidDelegate delHandle)
    {
        GameObject goButton = GuildScarce.CureBeeChildSlit(this.gameObject, buttonName).gameObject;
        //给按钮注册事件方法
        if (goButton != null)
        {
            CacheSeepageEpisodic.Age(goButton).NoThese = delHandle;
        }
    }

    /// <summary>
    /// 打开ui窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    protected GameObject MarkUIJazz(string uiFormName)
    {
        return  UIAwesome.AgeFletcher().DaleUIHobby(uiFormName);
    }

    /// <summary>
    /// 关闭当前ui窗体
    /// </summary>
    protected void BloodUIJazz(string uiFormName)
    {
        //处理后的uiform名称
        UIAwesome.AgeFletcher().BloodSoSolelyUIHobby(uiFormName);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="msgType">消息的类型</param>
    /// <param name="msgName">消息名称</param>
    /// <param name="msgContent">消息内容</param>
    protected void HornExplore(string msgType,string msgName,object msgContent)
    {
        KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
        ExploreGovern.HornExplore(msgType, kvs);
    }

    /// <summary>
    /// 接受消息
    /// </summary>
    /// <param name="messageType">消息分类</param>
    /// <param name="handler">消息委托</param>
    public void BlossomExplore(string messageType,ExploreGovern.DelMessageDelivery handler)
    {
        ExploreGovern.DewMsgEpisodic(messageType, handler);
    }

    /// <summary>
    /// 显示语言
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string Dale(string id)
    {
        string strResult = string.Empty;
        strResult = HesitateElk.AgeFletcher().DaleWelt(id);
        return strResult;
    }
}
