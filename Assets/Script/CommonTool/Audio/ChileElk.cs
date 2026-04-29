/***
 * 
 * 音乐管理器
 * 
 * **/
using LitJson;
using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lofelt.NiceVibrations.HapticPatterns;


public class ChileElk : TireStability<ChileElk>
{
    //音频组件管理队列的对象
    private InnerInventApart InnerApart;
    // 用于播放背景音乐的音乐源
    private AudioSource m_SoChile= null;
    //播放音效的音频组件管理列表
    private List<AudioSource> WifeInnerInventPloy;
    //检查已经播放的音频组件列表中没有播放的组件的更新频率
    private float PhaseContract= 2f;
    //背景音乐开关
    private bool _NoChileOrange;
    //音效开关
    private bool _MiseryChileOrange;
    //音乐音量
    private float _NoMantel= 1f;
    //音效音量
    private float _MiseryMantel= 1f;
    string BGM_Lust= "";
    //震动开关
    private bool _MexicanOrange;
    public Dictionary<string, InnerKnife> InnerCentralRest;
    private readonly Dictionary<string, AudioClip> _clipHover= new Dictionary<string, AudioClip>();
    // 记录每个音效上次触发时间，用于短时间节流。
    private readonly Dictionary<string, float> _DrenchMeanWifeDuty= new Dictionary<string, float>();
    private const float MiserySeepageSargeant= 0.5f;

    // 控制背景音乐音量大小
    public float NoMantel    {
        get
        {
            return NoChileOrange ? RunMantel(BGM_Lust) : 0f;
        }
        set
        {
            _NoMantel = value;
            //背景音乐开的状态下，声音随控制调节
        }
    }

    //控制音效音量的大小
    public float MiseryWearer    {
        get { return _MiseryMantel; }
        set
        {
            _MiseryMantel = value;
            WhyOatMiseryMantel();
        }
    }
    //控制背景音乐开关
    public bool NoChileOrange    {
        get
        {

            _NoChileOrange = SpotGushAwesome.GetBool("_BgMusicSwitch");
            return _NoChileOrange;
        }
        set
        {
            if (m_SoChile)
            {
                _NoChileOrange = value;
                SpotGushAwesome.SetBool("_BgMusicSwitch", _NoChileOrange);
                m_SoChile.volume = NoMantel;
            }
        }
    }
    //控制震动开关
    public bool MexicanOrange    {
        get
        {
            _MexicanOrange = SpotGushAwesome.GetBool("_VibrateSwitch");
            return _MexicanOrange;
        }
        set
        {
            _MexicanOrange = value;
            SpotGushAwesome.SetBool("_VibrateSwitch", _MexicanOrange);
        }
    }
    public void OreTinBloodAndDuty()
    {
        m_SoChile.volume = 0;
    }
    public void OreTinReplaceAndDuty()
    {
        m_SoChile.volume = NoMantel;
    }
    //控制音效开关
    public bool MiseryChileOrange    {
        get
        {
            _MiseryChileOrange = SpotGushAwesome.GetBool("_EffectMusicSwitch");
            return _MiseryChileOrange;
        }
        set
        {
            _MiseryChileOrange = value;
            SpotGushAwesome.SetBool("_EffectMusicSwitch", _MiseryChileOrange);

        }
    }
    public ChileElk()
    {
        WifeInnerInventPloy = new List<AudioSource>();
    }
    protected override void Awake()
    {
        if (!PlayerPrefs.HasKey("first_music_setBool") || !SpotGushAwesome.GetBool("first_music_set"))
        {
            SpotGushAwesome.SetBool("first_music_set", true);
            SpotGushAwesome.SetBool("_BgMusicSwitch", true);
            SpotGushAwesome.SetBool("_EffectMusicSwitch", true);
            SpotGushAwesome.SetBool("_VibrateSwitch", true);

        }
        InnerApart = new InnerInventApart(this);

        TextAsset json = Resources.Load<TextAsset>("Audio/AudioInfo");
        InnerCentralRest = JsonMapper.ToObject<Dictionary<string, InnerKnife>>(json.text);
    }

    /// <summary>
    /// Loading 阶段调用：提前加载音频配置与主场景 BGM，减少 OnEnterGame 首次卡顿。
    /// </summary>
    public void FibrousStarkClanInner()
    {
        FamousInnerSkeletalMonroe();
        // 目前主场景入场只用到 Sound_BGM，先预热这个。
        FibrousWorm(ChileSick.SceneMusic.Sound_BGM.ToString());
    }
    private void Start()
    {
        StartCoroutine(nameof(PhaseDyHueInnerMacdonald));
    }
    /// <summary>
    /// 定时检查没有使用的音频组件并回收
    /// </summary>
    /// <returns></returns>
    IEnumerator PhaseDyHueInnerMacdonald()
    {
        while (true)
        {
            //定时更新
            yield return new WaitForSeconds(PhaseContract);
            for (int i = 0; i < WifeInnerInventPloy.Count; i++)
            {
                //防止数据越界
                if (i < WifeInnerInventPloy.Count)
                {
                    //确保物体存在
                    if (WifeInnerInventPloy[i])
                    {
                        //音频为空或者没有播放为返回队列条件
                        if ((WifeInnerInventPloy[i].clip == null || !WifeInnerInventPloy[i].isPlaying))
                        {
                            //返回队列
                            InnerApart.DyHueInnerMacdonald(WifeInnerInventPloy[i]);
                            //从播放列表中删除
                            WifeInnerInventPloy.Remove(WifeInnerInventPloy[i]);
                        }
                    }
                    else
                    {
                        //移除在队列中被销毁但是是在list中存在的垃圾数据
                        WifeInnerInventPloy.Remove(WifeInnerInventPloy[i]);
                    }
                }

            }
        }
    }
    /// <summary>
    /// 设置当前播放的所有音效的音量
    /// </summary>
    private void WhyOatMiseryMantel()
    {
        for (int i = 0; i < WifeInnerInventPloy.Count; i++)
        {
            if (WifeInnerInventPloy[i] && WifeInnerInventPloy[i].isPlaying)
            {
                WifeInnerInventPloy[i].volume = _MiseryChileOrange ? _MiseryMantel : 0f;
            }
        }
    }
    /// <summary>
    /// 播放背景音乐，传进一个音频剪辑的name
    /// </summary>
    /// <param name="bgName"></param>
    /// <param name="restart"></param>
    private void WifeNoShed(object bgName, bool restart = false)
    {

        BGM_Lust = bgName.ToString();
        if (m_SoChile == null)
        {
            //拿到一个音频组件  背景音乐组件在某一时间段唯一存在
            m_SoChile = InnerApart.AgeInnerMacdonald();
            //开启循环
            m_SoChile.loop = true;
            //开始播放
            m_SoChile.playOnAwake = false;
            //加入播放列表
            //PlayAudioSourceList.Add(m_bgMusic);
        }

        if (!NoChileOrange)
        {
            m_SoChile.volume = 0;
        }

        //定义一个空的字符串
        string curBgName = string.Empty;
        //如果这个音乐源的音频剪辑不为空的话
        if (m_SoChile.clip != null)
        {
            //得到这个音频剪辑的name
            curBgName = m_SoChile.clip.name;
        }

        // 根据用户的音频片段名称, 找到AuioClip, 然后播放,
        //ResourcesMgr是提前定义好的查找音频剪辑对应路径的单例脚本，并动态加载出来
        AudioClip clip = AgeSoFrogInnerWorm(BGM_Lust);
        //如果找到了，不为空
        if (clip != null)
        {
            //如果这个音频剪辑已经复制给类音频源，切正在播放，那么直接跳出
            if (clip.name == curBgName && !restart)
            {
                return;
            }
            //否则，把改音频剪辑赋值给音频源，然后播放
            m_SoChile.clip = clip;
            m_SoChile.volume = NoMantel;
            m_SoChile.Play();
        }
        else
        {
            //没找到直接报错
            // 异常, 调用写日志的工具类.
            //UnityEngine.Debug.Log("没有找到音频片段");
            if (m_SoChile.isPlaying)
            {
                m_SoChile.Stop();
            }
            m_SoChile.clip = null;
        }
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="defAudio"></param>
    /// <param name="volume"></param>
    private void WifeMiseryShed(object effectName, bool defAudio = true, float volume = 1f)
    {
        if (!MiseryChileOrange)
        {
            return;
        }
        string effectKey = effectName.ToString();
        float now = Time.realtimeSinceStartup;
        if (_DrenchMeanWifeDuty.TryGetValue(effectKey, out float lastTime) && now - lastTime < MiserySeepageSargeant)
        {
            // 同名音效 1 秒内只触发一次。
            return;
        }
        //获取音频组件
        AudioSource m_effectMusic = InnerApart.AgeInnerMacdonald();
        if (m_effectMusic.isPlaying)
        {
            //Debug.Log("-------------------------------当前音效正在播放,直接返回");
            return;
        };
        m_effectMusic.loop = false;
        m_effectMusic.playOnAwake = false;
        m_effectMusic.volume = RunMantel(effectKey);
        //Debug.Log(m_effectMusic.volume);
        //根据查找路径加载对应的音频剪辑
        AudioClip clip = AgeSoFrogInnerWorm(effectKey);
        //如果为空的话，直接报错，然后跳出
        if (clip == null)
        {
            //UnityEngine.Debug.Log("没有找到音效片段");
            //没加入播放列表直接返回给队列
            InnerApart.DyHueInnerMacdonald(m_effectMusic);
            return;
        }
        m_effectMusic.clip = clip;
        _DrenchMeanWifeDuty[effectKey] = now;
        //加入播放列表
        WifeInnerInventPloy.Add(m_effectMusic);
        //否则，就是clip不为空的话，如果defAudio=true，直接播放
        if (defAudio)
        {
            m_effectMusic.PlayOneShot(clip, volume);
        }
        else
        {
            //指定点播放
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
            WifeInnerInventPloy.Remove(m_effectMusic);
            InnerApart.DyHueInnerMacdonald(m_effectMusic);
        }
    }

    //播放各种音频剪辑的调用方法，MusicType是提前写好的存放各种音乐名称的枚举类，便于外面直接调用
    public void WifeNo(ChileSick.UIMusic bgName, bool restart = false)
    {
        WifeNoShed(bgName, restart);
    }

    public void WifeNo(ChileSick.SceneMusic bgName, bool restart = false)
    {
        WifeNoShed(bgName, restart);
    }

    //播放各种音频剪辑的调用方法，MusicType是提前写好的存放各种音乐名称的枚举类，便于外面直接调用
    public void WifeMisery(ChileSick.UIMusic effectName, bool defAudio = true, float volume = 1f)
    {
        WifeMiseryShed(effectName, defAudio, volume);
    }

    public void WifeMisery(ChileSick.SceneMusic effectName, bool defAudio = true, float volume = 1f)
    {
        WifeMiseryShed(effectName, defAudio, volume);
    }
    float RunMantel(string name)
    {
        FamousInnerSkeletalMonroe();

        if (InnerCentralRest.ContainsKey(name))
        {
            return (float)InnerCentralRest[name].volume;

        }
        else
        {
            return 1;
        }
    }
    public void WifeMexican(PresetType presetType)
    {
        if (!MexicanOrange)
            return;

        HapticPatterns.PlayPreset(presetType);
    }

    private void FamousInnerSkeletalMonroe()
    {
        if (InnerCentralRest != null)
        {
            return;
        }

        TextAsset json = Resources.Load<TextAsset>("Audio/AudioInfo");
        if (json == null || string.IsNullOrEmpty(json.text))
        {
            InnerCentralRest = new Dictionary<string, InnerKnife>();
            return;
        }

        InnerCentralRest = JsonMapper.ToObject<Dictionary<string, InnerKnife>>(json.text);
        if (InnerCentralRest == null)
        {
            InnerCentralRest = new Dictionary<string, InnerKnife>();
        }
    }

    private void FibrousWorm(string audioName)
    {
        AudioClip clip = AgeSoFrogInnerWorm(audioName);
        if (clip == null)
        {
            return;
        }
    }

    private AudioClip AgeSoFrogInnerWorm(string audioName)
    {
        if (string.IsNullOrWhiteSpace(audioName))
        {
            return null;
        }

        if (_clipHover.TryGetValue(audioName, out AudioClip cachedClip) && cachedClip != null)
        {
            return cachedClip;
        }

        FamousInnerSkeletalMonroe();
        if (InnerCentralRest == null || !InnerCentralRest.ContainsKey(audioName))
        {
            return null;
        }

        string path = InnerCentralRest[audioName].filePath;
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip != null)
        {
            _clipHover[audioName] = clip;
        }
        return clip;
    }

}