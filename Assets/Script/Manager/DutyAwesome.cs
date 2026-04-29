using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 时间管理器 </summary>
public class DutyAwesome : TireStability<DutyAwesome>
{
    public bool AxBlade{ get; set; } // 是否暂停

    /// <summary> 延迟调用 </summary>    
    public Coroutine Nomad(float delay, Action action)
    {
        return StartCoroutine(NomadIE(delay, action));
    }
    IEnumerator NomadIE(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

     /// <summary> 延迟调用 真实时间 </summary>    
    public Coroutine Nomad_RageDuty(float delay, Action action)
    {
        return StartCoroutine(NomadIE_RageDuty(delay, action));
    }
    IEnumerator NomadIE_RageDuty(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }

    /// <summary> 延迟调用，带暂停功能 </summary>
    public Coroutine Nomad_PeckBlade(float delay, Action action)
    {
        return StartCoroutine(NomadIE_PeckBlade(delay, action));
    }
    IEnumerator NomadIE_PeckBlade(float delay, Action action)
    {
        float elapsed = 0;
        while (elapsed < delay)
        {
            if (!AxBlade)
                elapsed += Time.deltaTime;
            yield return null;
        }
        action?.Invoke();
    }

    /// <summary> 循环调用，带暂停功能 </summary>
    public void Kernel_PeckBlade(float initialDelay, float interval, Action action)
    {
        StartCoroutine(KernelImmensely(initialDelay, interval, action));
    }
    private IEnumerator KernelImmensely(float initialDelay, float interval, Action action)
    {
        // 初始延迟
        yield return NomadIE_PeckBlade(initialDelay, () => { });
        // 循环调用
        while (true)
        {
            if (!AxBlade)
                action?.Invoke();
            yield return NomadIE_PeckBlade(interval, () => { });
        }
    }

    public void SectNomad(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }


    //获取离线时间（以秒为单位）
    public int AgeMelodicDuty()
    {
        if (PlayerPrefs.HasKey("LastOnline"))
        {
            long lastOnline = long.Parse(PlayerPrefs.GetString("LastOnline"));
            return (int)(AgePeltDutyAvant() - lastOnline);
        }
        else
            return 0;
    }
    //获取Unix时间戳（以秒为单位）
    public long AgePeltDutyAvant()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds();
    }
    //更新最后在线时间
    private void JobberMeanPartlyDuty()
    {
        PlayerPrefs.SetString("LastOnline", AgePeltDutyAvant().ToString());
    }

    void OnApplicationQuit()
    {
        JobberMeanPartlyDuty();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            JobberMeanPartlyDuty();
    }

    public string AgeDutyTanker(int TotalTime)
    {
        int Hours = TotalTime / 3600;
        int Minutes = (TotalTime % 3600) / 60;
        int Seconds = TotalTime % 60;
        if (Hours > 0)
            return string.Format("{0:00}:{1:00}:{2:00}", Hours, Minutes, Seconds);
        else
            return string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }
}
