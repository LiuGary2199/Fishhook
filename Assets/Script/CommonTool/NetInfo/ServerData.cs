using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//登录服务器返回数据
public class RootData
{
    public int code { get; set; }
    public string msg { get; set; }
    public ServerData data { get; set; }
}
//用户登录信息
public class ServerUserData
{
    public int code { get; set; }
    public string msg { get; set; }
    public int data { get; set; }
}
//服务器的数据
public class ServerData
{
    public string init { get; set; }
    public string version { get; set; }

    public string apple_pie { get; set; }
    public string inter_b2f_count { get; set; }
    public string inter_freq { get; set; }
    public string relax_interval { get; set; }
    public string trial_MaxNum { get; set; }
    public string nextlevel_interval { get; set; }
    public string adjust_init_rate_act { get; set; }
    public string adjust_init_act_position { get; set; }
    public string adjust_init_adrevenue { get; set; }
    //public string soho_shop { get; set; }
    public string CashOut_Data { get; set; } //真提现数据
    public string JT_CashOut_Data { get; set; } //假提数据
    public string BlockRule { get; set; } //屏蔽规则
    public string GameData { get; set; } //游戏数据
    public List<List<BaseRewardData>> dailydatelist { get; set; }  //每日签到
    public string fish_config { get; set; } //鱼配置
}

public class GameDatas
{
    public int guide_click_auto { get; set; }
    public List<RewardData> lucky_card_data_list { get; set; }
    public HPConfig HPConfig { get; set; } //恢复生命
    public int lucky_card_win_max_count { get; set; }
    public LittleGameConfig LittleGameconfig { get; set; } //恢复生命
    public List<RewardData> slots_data_list; //小slot奖励
    public FerverTimeConfig FerverTimeConfig { get; set; } //疯狂时间
    public List<HomeWheelRewardData> HomeWheelrewards { get; set; } //主页转盘奖励配置
    public KillInchingConfig KillInchingConfig { get; set; } //疯狂时间
        public List<RewardData> interAdReward; //插屏奖励
    public int[] LevelUpShip; //升级经验值
    public int HookHp; //钩子生命
    public int ComboShow; //连击显示
    public int ComboRot; //连击转盘
    public int LevelUprewards; //升级奖励
    /// <summary>发射后下一发钩子生成间隔（秒），服务器未配置时用默认值</summary>
    public float HookReloadSeconds { get; set; }
    public List<List<RewardData>> dailydatelist { get; set; }
    public List<TaskData> task_list { get; set; }
    /// <summary>普通模式普通鱼“惊喜钻石”替换奖励概率（万分比，0~10000）。</summary>
    public int surprise_diamond_bps { get; set; }
    /// <summary>普通模式普通鱼命中惊喜时发放的钻石数量。</summary>
    public int surprise_diamond_count { get; set; }
    /// <summary>鱼潮刷新间隔（秒）：游戏开始后每隔该时间随机播放一组鱼群。</summary>
    public float fish_shoal_cd { get; set; }

}

public class LittleGameConfig
{
    public int intervalSecond { get; set; }
    public int miniGameCountBeforeBoss { get; set; }
}

public class FerverTimeConfig
{
    public int FerverTimeCount { get; set; }
    public int FerverCountDownTime { get; set; }
}
public class KillInchingConfig
{
    public int KillInchingCount { get; set; }
    public float KillInchingDvalue { get; set; }
    public float KillInchingMAX { get; set; }

}

public class Init
{
    public List<SlotItem> slot_group { get; set; }

    public double[] cash_random { get; set; }
    public MultiGroup[] cash_group { get; set; }
    public MultiGroup[] gold_group { get; set; }
    public MultiGroup[] amazon_group { get; set; }
}
public class HPConfig
{
    public int DefHP { get; set; }
    public int recoveryThreshold { get; set; }
    public int recoverytime { get; set; }
    public int recoveryHP { get; set; }
}
public class SlotItem
{
    public double multi { get; set; }
    public int weight { get; set; }
}

public class MultiGroup
{
    public int max { get; set; }
    public int multi { get; set; }
}

public class UserRootData
{
    public int code { get; set; }
    public string msg { get; set; }
    public string data { get; set; }
}

public class LocationData
{
    public double X;
    public double Y;
    public double Radius;
}

public class UserInfoData
{
    public double lat;
    public double lon;
    public string query; //ip地址
    public string regionName; //地区名称
    public string city; //城市名称
    public bool IsHaveApple; //是否有苹果
}

public class BlockRuleData //屏蔽规则
{
    public LocationData[] LocationList; //屏蔽位置列表
    public string[] CityList; //屏蔽城市列表
    public string[] IPList; //屏蔽IP列表
    public string fall_down; //自然量
    public bool BlockVPN; //屏蔽VPN
    public bool BlockSimulator; //屏蔽模拟器
    public bool BlockRoot; //屏蔽root
    public bool BlockDeveloper; //屏蔽开发者模式
    public bool BlockUsb; //屏蔽USB调试
    public bool BlockSimCard; //屏蔽SIM卡
}

public class CashOutData //提现
{
    public string MoneyName; //货币名称
    public string Description; //玩法描述
    public string convert_goal; //兑换目标
    public List<CashOut_TaskData> TaskList; //任务列表
}

public class CashOut_TaskData
{
    public string Name; //任务名称
    public float NowValue; //当前值
    public double Target; //目标值
    public string Description; //任务描述
    public bool IsDefault; //是否默认（循环）任务
}

public class BaseRewardData
{
    public string type { get; set; }
    public int weight { get; set; }
    public int reward_num { get; set; }
}

public class FishConfigData
{
    public string id { get; set; }
    public string type { get; set; }
    public int unlockLevel { get; set; }
    public int sortOrder { get; set; }
       public int sellPrice { get; set; }
    /// <summary>普通模式钻石鱼：击杀奖励钻石数量；0 表示非钻石鱼（走 sellPrice 金币）。</summary>
    public int diamondReward { get; set; }
    /// <summary>普通模式同 unlockLevel 内相对刷新权重；≤0 或未下发时客户端按 1 处理。</summary>
    public int normalSpawnWeight { get; set; }
}
public class RewardData
{
    public int weight { get; set; }
    public RewardType type { get; set; }
    public double rewardNum { get; set; }
    public int numMax { get; set; }
    public int numMin { get; set; }
}

public class TaskData
{
    public int taskId { get; set; }
    public int target { get; set; }
    public int rewardGold { get; set; }
}

public class LuckyObjData
{
    public RewardType LuckyObjType;
    public double RewardNum;
}


public class HomeWheelRewardData
{
    public string id { get; set; }
    public string name { get; set; }
    public RewardType type { get; set; }
    public int count { get; set; }
    public int probability_bps { get; set; }
}
public enum GameType
{
    None = 0,
    Normal = 1,
    FerverTime = 2,
}
