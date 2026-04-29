using System;
using UnityEngine;

public class LoveManual
{
    public const int LoveWe_1= 1;
    public const int LoveWe_2= 2;
    public const int LoveWe_3= 3;
    public const int LoveWe_4= 4;
    
    /// <summary>
    /// 获取任务进度
    /// </summary>
    /// <returns></returns>
    public static int AgeLoveCareless(int taskId)
    {
        var progress = 0;
        var dateTime = DateTime.Now.ToString("yyyy/MM/dd");
        switch (taskId)
        {
            case LoveWe_1:
                progress = PlayerPrefs.GetInt($"TaskProgress_{dateTime}_{LoveWe_1}", 0);
                break;
            case LoveWe_2:
                progress = PlayerPrefs.GetInt($"TaskProgress_{dateTime}_{LoveWe_2}", 0);
                break;
            case LoveWe_3:
                progress = PlayerPrefs.GetInt($"TaskProgress_{dateTime}_{LoveWe_3}", 0);
                break;
            case LoveWe_4:
                progress = PlayerPrefs.GetInt($"TaskProgress_{dateTime}_{LoveWe_4}", 0);
                break;
        }

        return progress;
    }

    public static void WhyLoveCareless(int taskId, int progress)
    {
        var dateTime = DateTime.Now.ToString("yyyy/MM/dd");
        switch (taskId)
        {
            case LoveWe_1:
                PlayerPrefs.SetInt($"TaskProgress_{dateTime}_{LoveWe_1}", progress);
                break;
            case LoveWe_2:
                PlayerPrefs.SetInt($"TaskProgress_{dateTime}_{LoveWe_2}", progress);
                break;
            case LoveWe_3:
                PlayerPrefs.SetInt($"TaskProgress_{dateTime}_{LoveWe_3}", progress);
                break;
            case LoveWe_4:
                PlayerPrefs.SetInt($"TaskProgress_{dateTime}_{LoveWe_4}", progress);
                break;
        }
        PlayerPrefs.Save();
        BarelyIon.ToJobberLoveCareless?.Invoke();
        //AEventModule.Send(AEventType.UpdateTaskProgress);
    }

    public static void DewLoveCareless(int taskId, int progress)
    {
        var currentProgress = AgeLoveCareless(taskId);
        WhyLoveCareless(taskId, currentProgress + progress);
    }
    
    public static TaskStatus AgeLoveImpose(int taskId)
    {
        var taskStatus = (TaskStatus)PlayerPrefs.GetInt($"ATaskStatus_{taskId}", (int)TaskStatus.Incomplete);
        return taskStatus;
    }
    
    public static void WhyLoveImpose(int taskId, TaskStatus taskStatus)
    {
        PlayerPrefs.SetInt($"ATaskStatus_{taskId}", (int)taskStatus);
    }
}