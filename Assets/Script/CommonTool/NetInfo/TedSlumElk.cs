/***
 * 
 * 
 * 网络信息控制
 * 
 * **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using LitJson;
using UnityEngine;
using UnityEngine.Playables;
//using MoreMountains.NiceVibrations;

public class TedSlumElk : MonoBehaviour
{
    public static TedSlumElk instance;
    //请求超时时间
    private static float TIMEOUT= 3f;
[UnityEngine.Serialization.FormerlySerializedAs("BaseUrl")]    //base
    public string ShedPeg;
[UnityEngine.Serialization.FormerlySerializedAs("BaseLoginUrl")]    //登录url
    public string ShedImplyPeg;
[UnityEngine.Serialization.FormerlySerializedAs("BaseConfigUrl")]    //配置url
    public string ShedMilletPeg;
[UnityEngine.Serialization.FormerlySerializedAs("BaseTimeUrl")]    //时间戳url
    public string ShedDutyPeg;
[UnityEngine.Serialization.FormerlySerializedAs("BaseAdjustUrl")]    //更新AdjustId url
    public string ShedArousePeg;
[UnityEngine.Serialization.FormerlySerializedAs("GameCode")]    //后台gamecode
    public string ClanCode= "20000";
[UnityEngine.Serialization.FormerlySerializedAs("Lifeway")]
    //channel渠道平台
#if UNITY_IOS
    public string Lifeway = "AppStore";
#elif UNITY_ANDROID
    public string Lifeway= "GooglePlay";
#else
    public string Lifeway = "Other";
#endif
    //工程包名
    private string RapidlyLust{ get { return Application.identifier; } }
    //登录url
    private string ImplyPeg= "";
    //配置url
    private string MilletPeg= "";
    //更新AdjustId url
    private string ArousePeg= "";
[UnityEngine.Serialization.FormerlySerializedAs("country")]    //国家
    public string Endless= "";
[UnityEngine.Serialization.FormerlySerializedAs("ConfigData")]    //服务器Config数据
    public ServerData MilletGush;
[UnityEngine.Serialization.FormerlySerializedAs("GameData")]    //提现相关后台数据
#if ZT
    public CashOutData CashOut_Data;
#endif
#if JT
    public JT_CashOutData JT_CashOut_Data;
#endif
    //服务器Config数据
    public GameDatas ClanGush;
[UnityEngine.Serialization.FormerlySerializedAs("InitData")]    //游戏内数据
    public Init CapeGush;
[UnityEngine.Serialization.FormerlySerializedAs("adManager")]    //ADAwesome
    public GameObject OrAwesome;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("gaid")]    public string Chic;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("aid")]    public string Dig;
    [HideInInspector]
[UnityEngine.Serialization.FormerlySerializedAs("idfa")]    public string Milk;
    int Habit_React= 0;
[UnityEngine.Serialization.FormerlySerializedAs("ready")]    
    public bool Habit= false;

[UnityEngine.Serialization.FormerlySerializedAs("BlockRule")]    //ios 获取idfa函数声明
    public BlockRuleData DrapeWith;
#if UNITY_IOS
    [DllImport("__Internal")]
    internal extern static void getIDFA();
#endif

    [HideInInspector] [UnityEngine.Serialization.FormerlySerializedAs("DataFrom")]public string GushLike; //数据来源 打点用
[UnityEngine.Serialization.FormerlySerializedAs("List_SignInData")]
    //签到奖励
    public List<List<RewardData>> Ploy_FoldItGush= new List<List<RewardData>>();
    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        ImplyPeg = ShedImplyPeg + ClanCode + "&channel=" + Lifeway + "&version=" + Application.version;
        MilletPeg = ShedMilletPeg + ClanCode + "&channel=" + Lifeway + "&version=" + Application.version;
        ArousePeg = ShedArousePeg + ClanCode;
    }
    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass aj = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject p = aj.GetStatic<AndroidJavaObject>("currentActivity");
            p.Call("getGaid");
            p.Call("getAid");
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
#if UNITY_IOS
            getIDFA();
            string idfv = UnityEngine.iOS.Device.vendorIdentifier;
            SpotGushAwesome.SetString("idfv", idfv);
#endif
        }
        else
        {
            Imply();           //编辑器登录
        }
        //获取config数据
        AgeMilletGush();

        //提现登录
        ZJT_Manager.AgeFletcher().Login();
    }

    /// <summary>
    /// 获取gaid回调
    /// </summary>
    /// <param name="gaid_str"></param>
    public void gaidAction(string gaid_str)
    {
        Debug.Log("unity收到gaid：" + gaid_str);
        Chic = gaid_str;
        if (Chic == null || Chic == "")
        {
            Chic = SpotGushAwesome.GetString("gaid");
        }
        else
        {
            SpotGushAwesome.SetString("gaid", Chic);
        }
        Habit_React++;
        if (Habit_React == 2)
        {
            Imply();
        }
    }
    /// <summary>
    /// 获取aid回调
    /// </summary>
    /// <param name="aid_str"></param>
    public void aidAction(string aid_str)
    {
        Debug.Log("unity收到aid：" + aid_str);
        Dig = aid_str;
        if (Dig == null || Dig == "")
        {
            Dig = SpotGushAwesome.GetString("aid");
        }
        else
        {
            SpotGushAwesome.SetString("aid", Dig);
        }
        Habit_React++;
        if (Habit_React == 2)
        {
            Imply();
        }
    }
    /// <summary>
    /// 获取idfa成功
    /// </summary>
    /// <param name="message"></param>
    public void idfaSuccess(string message)
    {
        Debug.Log("idfa success:" + message);
        Milk = message;
        SpotGushAwesome.SetString("idfa", Milk);
        Imply();
    }
    /// <summary>
    /// 获取idfa失败
    /// </summary>
    /// <param name="message"></param>
    public void idfaFail(string message)
    {
        Debug.Log("idfa fail");
        Milk = SpotGushAwesome.GetString("idfa");
        Imply();
    }
    /// <summary>
    /// 登录
    /// </summary>
    public void Imply()
    {
        //获取本地缓存的Local用户ID
        string localId = SpotGushAwesome.GetString(CMillet.If_CajunFootWe);

        //没有用户ID，视为新用户，生成用户ID
        if (localId == "" || localId.Length == 0)
        {
            //生成用户随机id
            TimeSpan st = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            string timeStr = Convert.ToInt64(st.TotalSeconds).ToString() + UnityEngine.Random.Range(0, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(1, 10).ToString();
            localId = timeStr;
            SpotGushAwesome.SetString(CMillet.If_CajunFootWe, localId);
        }

        //拼接登录接口参数
        string url = "";
        if (Application.platform == RuntimePlatform.IPhonePlayer)       //一个参数 - iOS
        {
            url = ImplyPeg + "&" + "randomKey" + "=" + localId + "&idfa=" + Milk + "&packageName=" + RapidlyLust;
        }
        else if (Application.platform == RuntimePlatform.Android)  //两个参数 - Android
        {
            url = ImplyPeg + "&" + "randomKey" + "=" + localId + "&gaid=" + Chic + "&androidId=" + Dig + "&packageName=" + RapidlyLust;
        }
        else //编辑器
        {
            url = ImplyPeg + "&" + "randomKey" + "=" + localId + "&packageName=" + RapidlyLust;
        }

        //获取国家信息
        RunPerplex(() =>
        {
            url += "&country=" + Endless;
            //登录请求
            TedYearAwesome.AgeFletcher().TestAge(url,
                (data) =>
                {
                    Debug.Log("Login 成功" + data.downloadHandler.text);
                    SpotGushAwesome.SetString("init_time", DateTime.Now.ToString());
                    ServerUserData serverUserData = JsonMapper.ToObject<ServerUserData>(data.downloadHandler.text);
                    SpotGushAwesome.SetString(CMillet.If_CajunBottomWe, serverUserData.data.ToString());

                    HornArouseAlga();

                    if (PlayerPrefs.GetInt("SendedEvent") != 1 && !String.IsNullOrEmpty(PotionUtil.ChopRod))
                        PotionUtil.HornCache();
                },
                () =>
                {
                    Debug.Log("Login 失败");
                });
        });
    }
    /// <summary>
    /// 获取国家
    /// </summary>
    /// <param name="cb"></param>
    private void RunPerplex(Action cb)
    {
        bool callBackReady = false;
        if (String.IsNullOrEmpty(Endless))
        {
            TedYearAwesome.AgeFletcher().TestAge("https://a.mafiagameglobal.com/event/country/", (data) =>
            {
                Endless = JsonMapper.ToObject<Dictionary<string, string>>(data.downloadHandler.text)["country"];
                Debug.Log("获取国家 成功:" + Endless);
                if (!callBackReady)
                {
                    callBackReady = true;
                    cb?.Invoke();
                }
            },
            () =>
            {
                Debug.Log("获取国家 失败");
                if (!callBackReady)
                {
                    Endless = "";
                    callBackReady = true;
                    cb?.Invoke();
                }
            });
        }
        else
        {
            if (!callBackReady)
            {
                callBackReady = true;
                cb?.Invoke();
            }
        }
    }

    /// <summary>
    /// 获取服务器Config数据
    /// </summary>
    private void AgeMilletGush()
    {
        Debug.Log("GetConfigData:" + MilletPeg);
        //获取并存入Config
        TedYearAwesome.AgeFletcher().TestAge(MilletPeg,
        (data) =>
        {
            GushLike = "OnlineData";
            Debug.Log("ConfigData 成功" + data.downloadHandler.text);
            SpotGushAwesome.SetString("OnlineData", data.downloadHandler.text);
            WhyMilletGush(data.downloadHandler.text);
        },
        () =>
        {
            Debug.Log("ConfigData 失败");
            AgeReversalGush();
        });
    }

    /// <summary>
    /// 获取本地Config数据
    /// </summary>
    private void AgeReversalGush()
    {
        //是否有缓存
        if (SpotGushAwesome.GetString("OnlineData") == "" || SpotGushAwesome.GetString("OnlineData").Length == 0)
        {
            GushLike = "LocalData_Updated"; //已联网更新过的数据
            Debug.Log("本地数据");
            TextAsset json = Resources.Load<TextAsset>("LocationJson/LocationData");
            WhyMilletGush(json.text);
        }
        else
        {
            GushLike = "LocalData_Original"; //原始数据
            Debug.Log("服务器缓存数据");
            WhyMilletGush(SpotGushAwesome.GetString("OnlineData"));
        }
    }

    /// <summary>
    /// 解析config数据
    /// </summary>
    /// <param name="configJson"></param>
    void WhyMilletGush(string configJson)
    {
        //如果已经获得了数据则不再处理
        if (MilletGush == null)
        {
            RootData rootData = JsonMapper.ToObject<RootData>(configJson);
            MilletGush = rootData.data;
            string GameDataStr = SeduceJsonGush(MilletGush.GameData); //处理json数据中，枚举和字符串转换问题
            ClanGush = JsonMapper.ToObject<GameDatas>(GameDataStr);
            CapeGush = JsonMapper.ToObject<Init>(MilletGush.init);

            if (!string.IsNullOrEmpty(MilletGush.BlockRule))
                DrapeWith = JsonMapper.ToObject<BlockRuleData>(MilletGush.BlockRule);
#if ZT
            if (!string.IsNullOrEmpty(MilletGush.CashOut_Data))
                CashOut_Data = JsonMapper.ToObject<CashOutData>(MilletGush.CashOut_Data);
#endif

#if JT
            if (!string.IsNullOrEmpty(ConfigData.JT_CashOut_Data))
            {
                JT_CashOut_Data = JsonMapper.ToObject<JT_CashOutData>(ConfigData.JT_CashOut_Data);
                ZJT_Manager.GetInstance().Init();
            }
#endif

            //GameReady();
            
            AgeFootSlum();
        }
    }
    /// <summary>
    /// 进入游戏
    /// </summary>
    void ClanFrost()
    {
        //打开admanager
        OrAwesome.SetActive(true);
        //进度条可以继续
        Habit = true;
        ClanGushAwesome.AgeFletcher().CapeMilletGush(ClanGush, MilletGush.fish_config);
    }

    /// <summary>
    /// 向后台发送adjustId
    /// </summary>
    public void HornArouseAlga()
    {
        string serverId = SpotGushAwesome.GetString(CMillet.If_CajunBottomWe);
        string adjustId = ArouseCapeAwesome.Instance.AgeArouseAlga();
        if (string.IsNullOrEmpty(serverId) || string.IsNullOrEmpty(adjustId))
        {
            return;
        }

        string url = ArousePeg + "&serverId=" + serverId + "&adid=" + adjustId;
        TedYearAwesome.AgeFletcher().TestAge(url,
            (data) =>
            {
                Debug.Log("服务器更新adjust adid 成功" + data.downloadHandler.text);
            },
            () =>
            {
                Debug.Log("服务器更新adjust adid 失败");
            });
    }
[UnityEngine.Serialization.FormerlySerializedAs("UserDataStr")]

    //获取用户信息
    public string FootGushPer= "";
[UnityEngine.Serialization.FormerlySerializedAs("UserData")]    public UserInfoData FootGush;
    int AgeFootSlumTruck= 0;
    void AgeFootSlum()
    {
        //还有进入正常模式的可能
        if (PlayerPrefs.HasKey("OtherChance") && PlayerPrefs.GetString("OtherChance") == "YES")
            PlayerPrefs.DeleteKey("Save_AP");
        //已经记录过用户信息 跳过检查
        if (PlayerPrefs.HasKey("OtherChance") && PlayerPrefs.GetString("OtherChance") == "NO")
        {
            ClanFrost();
            return;
        }

        //检查归因渠道信息
        //CheckAdjustNetwork();
        //获取用户信息
        string CheckUrl = ShedPeg + "/api/client/user/checkUser";
        TedYearAwesome.AgeFletcher().TestAge(CheckUrl,
        (data) =>
        {
            FootGushPer = data.downloadHandler.text;
            print("+++++ 获取用户数据 成功" + FootGushPer);
            UserRootData rootData = JsonMapper.ToObject<UserRootData>(FootGushPer);
            FootGush = JsonMapper.ToObject<UserInfoData>(rootData.data);
            if (FootGushPer.Contains("apple")
            || FootGushPer.Contains("Apple")
            || FootGushPer.Contains("APPLE"))
                FootGush.IsHaveApple = true;
            ClanFrost();
        }, () => { });
        Invoke(nameof(ReAgeFootSlum), 1);
    }
    void ReAgeFootSlum()
    {
        if (!Habit)
        {
            AgeFootSlumTruck++;
            if (AgeFootSlumTruck < 10)
            {
                print("+++++ 获取用户数据失败 重试： " + AgeFootSlumTruck);
                AgeFootSlum();
            }
            else
            {
                print("+++++ 获取用户数据 失败次数过多，放弃");
                ClanFrost();
            }
        }
    }
  
       public string SeduceJsonGush(string jsonData)
   {
       jsonData = jsonData.Replace("\"type\": \"diamond\"", "\"type\":2"); 
       jsonData = jsonData.Replace("\"type\": \"cash\"", "\"type\":1"); 
       return jsonData;
   }
    public void CapeFoldItGush()
    {
        Ploy_FoldItGush.Clear();
        for (int i = 0; i < ClanGush.dailydatelist.Count; i++)
        {
            List<RewardData> list = new List<RewardData>();
            for (int j = 0; j < ClanGush.dailydatelist[i].Count; j++)
            {
                double num = ClanGush.dailydatelist[i][j].rewardNum;
                //num *= (int)InitData.gold_group[0].multi;
                var data = new RewardData();
                data.rewardNum = num;
                data.type = RewardType.Diamond;
                list.Add(data);
            }
            Ploy_FoldItGush.Add(list);
        }
    }
}
