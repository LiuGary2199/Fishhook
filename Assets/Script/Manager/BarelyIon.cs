using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class BarelyIon 
{
    public static Action ToEtchEven{ get; set; }
    public static Action ToEtchEase{ get; set; }
    /// <summary>新版穿刺钩子碰鱼时触发，用于碰鱼减速效果</summary>
    public static Action<int> ToSparseDownFadEase{ get; set; }

    public static Action<bool> ToDownSeedyJoltEqual{ get; set; }
    /// <summary>
    /// 碰鱼减速：传入当前“慢效果值”，0 表示无减速。
    /// 注意：慢效果值的具体换算（例如换算为速度倍率）由订阅方决定。
    /// </summary>
    public static Action<float> ToDownEaseFadJoltEqual{ get; set; }

    // 鱼受击动画播放完毕后，请求回收（由 UIEaseBergBureau 执行回收/入池）
    public static Action<UIEaseDeluge> ToEasePromoteTopsoil{ get; set; }

    /// <summary>
    /// 鱼被致命击杀、即将回收前发出；参数为鱼身在屏幕/UI 下的世界坐标中心 + 鱼类别（用于按类别播放特效）。
    /// </summary>
    public static Action<Vector3, UIFishCategory> OnEaseLiquidMistCivicCompress;

    public delegate bool FishDeathCloseupDelayHandler(UIEaseDeluge fish);

    /// <summary>
    /// 致命击杀时由 Canvas 特写等系统注册；返回 true 表示延迟回收，之后由特写方调用 <see cref="UIEaseDeluge.ExecutePendingLethalRecycleAfterCloseup"/>。
    /// 同时只能有一个实现（后注册的覆盖前者）。
    /// </summary>
    public static FishDeathCloseupDelayHandler SunNomadEaseAloftFitRumbleGazelle;
    /// <summary>非鱼来源：任务/奖励等「界面加金币」表现入口（与击杀鱼飞金币区分）。</summary>
    public static Action<Transform, int> ToDewJuicy{ get; set; }

    /// <summary>击杀鱼掉落金币：MoteWould 使用 cash 对象池 + FishGoldMove。</summary>
    public static Action<Transform, int> ToEaseDewJuicy{ get; set; }

    /// <summary>击杀钻石鱼：MoteWould 使用 diamond 对象池 + FishDiamondMove。</summary>
    public static Action<Transform, int> ToEaseDewLinkage{ get; set; }

    // ===== 新增体力相关事件（核心）=====
    // 体力变化事件：参数（当前体力，剩余恢复时间，最大体力）
    public static Action<int, float, int> ToSchoolPursuit;

    // 船升级经验变化：参数（当前等级，当前等级经验，升级所需经验）
    public static Action<int, int, int> ToPermElkPursuit;

    // 船升级完成：参数（旧等级，新等级，本次提升等级数）
    public static Action<int, int, int> ToPermGripePursuit;

    // 船可升级状态变化：参数（是否可升级，待升级次数）
    public static Action<bool, int> ToPermErectusEqualPursuit;

    // 请求弹出船升级界面（如满经验首次触发）
    public static Action ToPermErectusSlimePromote;

    // 游戏模式切换：用于鱼池刷新、UI过渡动画等
    public static Action<GameType> ToClanSickPursuit;


    // 模式切换前后事件：可用于播放切换动画
    public static Action<GameType, GameType> ToClanSickStrongholdPromote;

    // 手动点击发射按钮（用于关闭自动射击等联动）
    public static Action ToSuburbDownPastManageShyness;

    // 全局玩法暂停状态变化（true=暂停，false=恢复）
    public static Action<bool> ToSparselyBladeEqualPursuit;


    // Ferver 进度变化（普通模式下累计）：参数（当前击杀进度，触发所需击杀数）
    public static Action<int, int> ToEntireCarelessPursuit;

    // Ferver 倒计时变化：参数（剩余秒数，总秒数）
    public static Action<float, float> ToEntireTruckTearPursuit;

    // 请求进入 FerverTime（用于先播放过渡动画）
    public static Action ToEntireStarkStrongholdPromote;
    // 进入 FerverTime 过渡中段预清场请求（由动画关键帧触发）
    public static Action ToEntirePreMaizePromote;

    // ===== 主页转盘事件 =====
    // 按子项索引请求旋转（索引从 0 开始）
    public static Action<int> ToMoteSexHeadIDSmilePromote;

    // 按角度请求旋转（单位：度，0~360 会自动归一化）
    public static Action<float> ToMoteSexHeadIDCargoPromote;

    // 按可控概率请求旋转（由 MoteSexStump 内部概率配置抽取目标奖励并落点）
    public static Action ToMoteSexHeadMeDistinctionPromote;

    // 主页转盘旋转结束：返回命中索引
    public static Action<int> ToMoteSexHeadDormancy;
    // 主页转盘旋转奖励结果：奖励类型 + 数量
    public static Action<RewardType, int> ToMoteSexLessonImporter;

    // 请求生成一条 Boss 鱼（由 UIEaseDeluge 监听并转发给 UIEaseBergBureau 生成）
    public static Action ToAlikeKierEasePromote;
    // Boss 生成准备完成（dir, spawnX, spawnY, warnSpineDuration, warnPostDelay），可用于预告 UI。
    public static Action<int, float, float, float, float> ToKierAlikeNotation;
    // Boss 预警流程全部结束（Spine 与箭头都关闭），可用于正式出鱼。
    public static Action ToKierMessDormancy;
    // Boss 即将最后一次折返：可用于弹出“即将消失”提示。
    public static Action ToKierStripCavityOnstage;

    /// <summary>ClanAwesome 在 SetHookFired(true) 时发出；用于复位「本发 Boss 特写已请求」等按发次计的状态（旧版钩子等）。</summary>
    public static Action ToDownBeamCompile;

    // ===== 随机小游戏调度相关 =====
    // 随机小游戏结束时触发；用于通知调度器继续计时
    public static Action ToSewageClanDormancy;

    // Boss 专用特写触发框命中（由钩子碰撞按「每发一次」发出，NationUIRumbleGazelle 监听）
    public static Action<UIEaseDeluge> ToKierGazelleSeepagePromote;
    
    // Boss 鱼游动到屏幕中心（x=0）时触发引导：主骨骼 Transform + 喵骨骼 Transform（可空）
    public static Action<Transform, Transform> ToKierEaseWorthGovernCruelPromote;

    // Closeup 期间钩子/鱼叉移动速度倍率（由 NationUIRumbleGazelle 触发，DownLivelihood 监听）
    public static Action<float> ToGazelleDownPreenNavigation;

    // Closeup 期间鱼群游动速度倍率（由 NationUIRumbleGazelle 触发，UIEaseBergBureau 监听）
    public static Action<float> ToGazelleEasePreenNavigation;

    
    public static Action ToEntireDutyDiscWalker;
    public static Action ToStarkClan;

    public static Action ToJobberLoveCareless;
    public static Action ToJobberLoveAge;

}

