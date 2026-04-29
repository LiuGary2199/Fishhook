using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoveWould : ShedUIHobby
{
[UnityEngine.Serialization.FormerlySerializedAs("BtnClose")]    public Button LadBlood;
[UnityEngine.Serialization.FormerlySerializedAs("TaskItemParent")]    public Transform LoveStarFemale;
    
    private List<LoveStar> taskPeach= new List<LoveStar>();
    
    private Transform mTalkVigor;

    private void Awake()
    {
        BarelyIon.ToJobberLoveAge += OnGetTask;
        //AddUIEvent(AEventType.UpdateTaskProgress, InitTaskItems);
        BarelyIon.ToJobberLoveCareless += CapeLovePeach;
        LadBlood.onClick.AddListener(() =>
        {ChileElk.AgeFletcher().WifeMisery(ChileSick.UIMusic.Sound_UIButton);
            BloodUIJazz(GetType().Name);
        });
        taskPeach = CliffLovePeach();
    }
    private void OnGetTask()
    {
        //AGoldFlayCtrl.GoldFlayAnim(mGoldTrans.gameObject, transform, transform.position, mGoldTrans.position);
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        CapeLovePeach();
    }

    private void OnDestroy()
    {
        BarelyIon.ToJobberLoveAge -= OnGetTask;
        BarelyIon.ToJobberLoveCareless -= CapeLovePeach;
    }

    private void CapeLovePeach()
    {
        var serverTasks = TedSlumElk.instance != null ? TedSlumElk.instance.ClanGush?.task_list : null;
        for (int i = 0; i < taskPeach.Count; i++)
        {
            var taskId = i + 1;
            var progress = LoveManual.AgeLoveCareless(taskId);
            var taskData = AgeLoveGushMeWe(serverTasks, taskId);
            var total = taskData != null ? Mathf.Max(1, taskData.target) : 1;
            var gold = taskData != null ? Mathf.Max(0, taskData.rewardGold) : 0;
            taskPeach[i].Cape(taskId, progress, total, gold);
        }
    }

    private static TaskData AgeLoveGushMeWe(List<TaskData> serverTasks, int taskId)
    {
        if (serverTasks == null || serverTasks.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < serverTasks.Count; i++)
        {
            var task = serverTasks[i];
            if (task != null && task.taskId == taskId)
            {
                return task;
            }
        }

        return null;
    }

    private List<LoveStar> CliffLovePeach()
    {
        var taskPeach= new List<LoveStar>();
        for (int i = 0; i < LoveStarFemale.childCount; i++)
        {
            var taskItem = LoveStarFemale.GetChild(i).GetComponent<LoveStar>();
            taskPeach.Add(taskItem);
        }
        return taskPeach;
    }
}