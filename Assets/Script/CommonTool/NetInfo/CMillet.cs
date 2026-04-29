/**
 * 
 * 常量配置
 * 
 * 
 * **/
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMillet
{
    #region 常量字段
    //登录url
    public const string ImplyPeg= "/api/client/user/getId?gameCode=";
    //配置url
    public const string MilletPeg= "/api/client/config?gameCode=";
    //时间戳url
    public const string DutyPeg= "/api/client/common/current_timestamp?gameCode=";
    //更新AdjustId url
    public const string ArousePeg= "/api/client/user/setAdjustId?gameCode=";
    #endregion

    #region 本地存储的字符串
    /// <summary>
    /// 本地用户id (string)
    /// </summary>
    public const string If_CajunFootWe= "sv_LocalUserId";
    /// <summary>
    /// 本地服务器id (string)
    /// </summary>
    public const string If_CajunBottomWe= "sv_LocalServerId";
    /// <summary>
    /// 是否是新用户玩家 (bool)
    /// </summary>
    public const string If_AxEarSailor= "sv_IsNewPlayer";

    /// <summary>
    /// 签到次数 (int)
    /// </summary>
    public const string If_MaserChunkAgeTruck= "sv_DailyBounsGetCount";
    /// <summary>
    /// 签到最后日期 (int)
    /// </summary>
    public const string If_MaserChunkWild= "sv_DailyBounsDate";
    /// <summary>
    /// 新手引导完成的步数
    /// </summary>
    public const string If_EarFootChop= "sv_NewUserStep";
    /// <summary>
    /// 金币余额
    /// </summary>
    public const string If_SeedVice= "sv_GoldCoin";
    /// <summary>
    /// 累计金币总数
    /// </summary>
    public const string If_LegitimateTalkVice= "sv_CumulativeGoldCoin";
    /// <summary>
    /// 钻石/现金余额
    /// </summary>
    public const string If_Farce= "sv_Token";
    /// <summary>
    /// 累计钻石/现金总数
    /// </summary>
    public const string If_LegitimateFarce= "sv_CumulativeToken";
    /// <summary>
    /// 钻石Amazon
    /// </summary>
    public const string If_Writer= "sv_Amazon";
    /// <summary>
    /// 累计Amazon总数
    /// </summary>
    public const string If_LegitimateWriter= "sv_CumulativeAmazon";
    /// <summary>
    /// 游戏总时长
    /// </summary>
    public const string If_RigidClanDuty= "sv_TotalGameTime";
    /// <summary>
    /// 第一次获得钻石奖励
    /// </summary>
    public const string If_FloodAgeFarce= "sv_FirstGetToken";
    /// <summary>
    /// 是否已显示评级弹框
    /// </summary>
    public const string If_ArmDaleTrapWould= "sv_HasShowRatePanel";
    /// <summary>
    /// 累计Roblox奖券总数
    /// </summary>
    public const string If_LegitimateOutside= "sv_CumulativeLottery";
    /// <summary>
    /// 已经通过一次的关卡(int array)
    /// </summary>
    public const string If_OutlineBowlResale= "sv_AlreadyPassLevels";
    /// <summary>
    /// 新手引导
    /// </summary>
    public const string If_EarFootChopWalker= "sv_NewUserStepFinish";
    public const string If_Mote_Cheep_React= "sv_task_level_count";
    // 是否第一次使用过slot
    public const string If_FloodEmit= "sv_FirstSlot";

    // 是否第一次使用过slot
    public const string If_FloodGripeUPEmit= "sv_FirstLevelUPSlot";
    /// <summary>
    /// adjust adid
    /// </summary>
    public const string If_ArouseAlga= "sv_AdjustAdid";

    /// <summary>
    /// 广告相关 - trial_num
    /// </summary>
    public const string If_Or_Spiny_Due= "sv_ad_trial_num";
    /// <summary>
    /// 看广告总次数
    /// </summary>
    public const string If_Worry_Or_Due= "sv_total_ad_num";
    /// <summary>
    /// 生命值
    /// </summary>
    public const string If_Indoor_Bad= "sv_energy_key";
    /// <summary>
    /// 生命值
    /// </summary>
    public const string If_Tall_Caste_Bad= "sv_time_stamp_key";
    /// <summary>
    /// 船等级
    /// </summary>
    public const string If_Sort_Cheep= "sv_ship_level";
    /// <summary>
    /// 当前等级经验
    /// </summary>
    public const string If_Sort_Fox= "sv_ship_exp";

    /// <summary>
    ///  总共杀的鱼
    /// </summary>
    public const string If_Worry_Associate= "sv_total_fishcount";

    /// <summary>
    ///  总共小游戏次数
    /// </summary>
    public const string If_Worry_Pheromone= "sv_total_smallgame";
     /// <summary>
    ///  总共Ferver次数
    /// </summary>
    public const string If_Worry_Printmaker= "sv_total_fervertime";
    /// <summary>
    /// Boss 鱼到达中心点引导是否已触发
    /// </summary>
    public const string If_guide_Mode_Juggle_Mesh= "sv_guide_boss_center_done";
    /// <summary>
    /// Ferver 过场结束后的首次引导是否已触发
    /// </summary>
    public const string If_Hover_Decade_Ridge_Mesh= "sv_guide_ferver_first_done";

/// <summary>
    ///click auto shoot toggle
    /// </summary>
    public const string If_Aside_Fort_Nurse_Mildly= "sv_click_auto_shoot_toggle";
    #endregion

    #region 监听发送的消息

    /// <summary>
    /// 有窗口打开
    /// </summary>
    public static string mg_WindowMark= "mg_WindowOpen";
    /// <summary>
    /// 窗口关闭
    /// </summary>
    public static string Of_SubwayBlood= "mg_WindowClose";
    /// <summary>
    /// 关卡结算时传值
    /// </summary>
    public static string Of_It_Physiological= "mg_ui_levelcomplete";
    /// <summary>
    /// 增加金币
    /// </summary>
    public static string Of_It_Distant= "mg_ui_addgold";
    /// <summary>
    /// 增加钻石/现金
    /// </summary>
    public static string Of_It_Sociably= "mg_ui_addtoken";
    /// <summary>
    /// 增加amazon
    /// </summary>
    public static string Of_It_Streetcar= "mg_ui_addamazon";

    /// <summary>
    /// 游戏暂停/继续
    /// </summary>
    public static string Of_ClanPicture= "mg_GameSuspend";

    /// <summary>
    /// 游戏资源数量变化
    /// </summary>
    public static string Of_StarBorder_= "mg_ItemChange_";

    /// <summary>
    /// 活动状态变更
    /// </summary>
    public static string Of_CrescentEqualBorder_= "mg_ActivityStateChange_";

    /// <summary>
    /// 关卡最大等级变更
    /// </summary>
    public static string Of_GripeRoeGripeBorder= "mg_LevelMaxLevelChange";

    #endregion

    #region 动态加载资源的路径

    // 金币图片
    public static string Cell_TalkVice_Midway= "Art/Tex/UI/jiangli1";
    // 钻石图片
    public static string Cell_Farce_Midway_Coerce= "Art/Tex/UI/jiangli4";
    // 鱼预制体名称前缀（例如 NormalFish）
    public const string EaseSeniorLustTrader= "FullerEase";

    #endregion
}

