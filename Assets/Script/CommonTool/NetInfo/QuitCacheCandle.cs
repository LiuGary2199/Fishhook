using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
public class QuitCacheCandle : TireStability<QuitCacheCandle>
{
    public string version = "1.2";
    public string ClanCode= TedSlumElk.instance.ClanCode;
    //channel
#if UNITY_IOS
    private string Lifeway = "AppStore";
#elif UNITY_ANDROID
    private string Lifeway= "GooglePlay";
#else
    private string Lifeway = "GooglePlay";
#endif


    private void OnApplicationPause(bool pause)
    {
        QuitCacheCandle.AgeFletcher().sendClanCareless();
    }

    public Text Moon;

    protected override void Awake()
    {
        base.Awake();

        version = Application.version;
        StartCoroutine(nameof(FortExplore));
    }
    IEnumerator FortExplore()
    {
        while (true)
        {
            yield return new WaitForSeconds(120f);
            QuitCacheCandle.AgeFletcher().sendClanCareless();
        }
    }
    private void Start()
    {
        if (SpotGushAwesome.GetInt("event_day") != DateTime.Now.Day && SpotGushAwesome.GetString("user_servers_id").Length != 0)
        {
            SpotGushAwesome.SetInt("event_day", DateTime.Now.Day);
        }
    }
    public void HornOxBullCache(string event_id)
    {
        HornCache(event_id);
    }
    public void sendClanCareless(List<string> valueList = null)
    {
        if (SpotGushAwesome.GetDouble(CMillet.If_LegitimateTalkVice) == 0)
        {
            SpotGushAwesome.SetDouble(CMillet.If_LegitimateTalkVice, SpotGushAwesome.GetDouble(CMillet.If_SeedVice));
        }
        if (SpotGushAwesome.GetDouble(CMillet.If_LegitimateFarce) == 0)
        {
            SpotGushAwesome.SetDouble(CMillet.If_LegitimateFarce, SpotGushAwesome.GetDouble(CMillet.If_Farce));
        }
        if (valueList == null)
        {
            valueList = new List<string>() {
                SpotGushAwesome.GetInt(CMillet.If_Sort_Cheep).ToString(),//船等级
                SpotGushAwesome.GetInt(CMillet.If_LegitimateTalkVice).ToString(),//累计现金
                SpotGushAwesome.GetInt(CMillet.If_Worry_Associate).ToString(),
                SpotGushAwesome.GetInt(CMillet.If_Worry_Pheromone).ToString(),
                SpotGushAwesome.GetInt(CMillet.If_Worry_Printmaker).ToString(),
                SpotGushAwesome.GetInt(CMillet.If_Aside_Fort_Nurse_Mildly).ToString(),
                //SpotGushAwesome.GetInt(SlotConfig.sv_SlotSpinCount).ToString()
            };
        }

        if (SpotGushAwesome.GetString(CMillet.If_CajunBottomWe) == null)
        {
            return;
        }
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("gameCode", ClanCode);
        wwwForm.AddField("userId", SpotGushAwesome.GetString(CMillet.If_CajunBottomWe));

        wwwForm.AddField("gameVersion", version);

        wwwForm.AddField("channel", Lifeway);

        for (int i = 0; i < valueList.Count; i++)
        {
            wwwForm.AddField("resource" + (i + 1), valueList[i]);
        }



        StartCoroutine(HornQuit(TedSlumElk.instance.ShedPeg + "/api/client/game_progress", wwwForm,
        (error) =>
        {
            Debug.Log(error);
        },
        (message) =>
        {
            Debug.Log(message);
        }));
    }
    public void HornCache(string event_id, string p1 = null, string p2 = null, string p3 = null)
    {
        if (Moon != null)
        {
            if (int.Parse(event_id) < 9100 && int.Parse(event_id) >= 9000)
            {
                if (p1 == null)
                {
                    p1 = "";
                }
                Moon.text += "\n" + DateTime.Now.ToString() + "id:" + event_id + "  p1:" + p1;
            }
        }
        if (SpotGushAwesome.GetString(CMillet.If_CajunBottomWe) == null)
        {
            TedSlumElk.instance.Imply();
            return;
        }
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("gameCode", ClanCode);
        wwwForm.AddField("userId", SpotGushAwesome.GetString(CMillet.If_CajunBottomWe));
        //Debug.Log("userId:" + SpotGushAwesome.GetString(CMillet.sv_LocalServerId));
        wwwForm.AddField("version", version);
        //Debug.Log("version:" + version);
        wwwForm.AddField("channel", Lifeway);
        //Debug.Log("channel:" + channal);
        wwwForm.AddField("operateId", event_id);
        Debug.Log("operateId:" + event_id);


        if (p1 != null)
        {
            wwwForm.AddField("params1", p1);
        }
        if (p2 != null)
        {
            wwwForm.AddField("params2", p2);
        }
        if (p3 != null)
        {
            wwwForm.AddField("params3", p3);
        }
        StartCoroutine(HornQuit(TedSlumElk.instance.ShedPeg + "/api/client/log", wwwForm,
        (error) =>
        {
            Debug.Log(error);
        },
        (message) =>
        {
            Debug.Log(message);
        }));
    }
    IEnumerator HornQuit(string _url, WWWForm wwwForm, Action<string> fail, Action<string> success)
    {
        //Debug.Log(SerializeDictionaryToJsonString(dic));
        using UnityWebRequest request = UnityWebRequest.Post(_url, wwwForm);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isNetworkError)
        {
            fail(request.error);
            NutPromote();
            request.Dispose();
        }
        else
        {
            success(request.downloadHandler.text);
            NutPromote();
            request.Dispose();
        }
    }
    private void NutPromote()
    {
        StopCoroutine(nameof(HornQuit));
    }


}