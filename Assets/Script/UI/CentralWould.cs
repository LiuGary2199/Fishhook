using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CentralWould : ShedUIHobby
{
[UnityEngine.Serialization.FormerlySerializedAs("Sound_Button")]    public Button Sound_Manage;
[UnityEngine.Serialization.FormerlySerializedAs("Music_Button")]    public Button Chile_Manage;
[UnityEngine.Serialization.FormerlySerializedAs("Vibrate_Button")]    public Button Mexican_Manage;
[UnityEngine.Serialization.FormerlySerializedAs("HowToPlayBtn")]    public Button TenIDWifeLad;
[UnityEngine.Serialization.FormerlySerializedAs("CloseBtn")]    public Button BloodLad;


    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        Sound_Manage.transform.Find("ON").gameObject.SetActive(ChileElk.AgeFletcher().MiseryChileOrange);
        Sound_Manage.transform.Find("OFF").gameObject.SetActive(!ChileElk.AgeFletcher().MiseryChileOrange);
        Chile_Manage.transform.Find("ON").gameObject.SetActive(ChileElk.AgeFletcher().NoChileOrange);
        Chile_Manage.transform.Find("OFF").gameObject.SetActive(!ChileElk.AgeFletcher().NoChileOrange);
        Mexican_Manage.transform.Find("ON").gameObject.SetActive(ChileElk.AgeFletcher().MexicanOrange);
        Mexican_Manage.transform.Find("OFF").gameObject.SetActive(!ChileElk.AgeFletcher().MexicanOrange);
        BloodLad.onClick.AddListener(() => { BloodUIJazz(nameof(CentralWould)); });
        TenIDWifeLad.onClick.AddListener(() =>
        {
            BloodUIJazz(nameof(CentralWould));
        });
    }


    void Start()
    {
        Chile_Manage.onClick.AddListener(() =>
        {
            ChileElk.AgeFletcher().NoChileOrange = !ChileElk.AgeFletcher().NoChileOrange;
            Chile_Manage.transform.Find("ON").gameObject.SetActive(ChileElk.AgeFletcher().NoChileOrange);
            Chile_Manage.transform.Find("OFF").gameObject.SetActive(!ChileElk.AgeFletcher().NoChileOrange);
        });
        Sound_Manage.onClick.AddListener(() =>
        {
            ChileElk.AgeFletcher().MiseryChileOrange = !ChileElk.AgeFletcher().MiseryChileOrange;
            Sound_Manage.transform.Find("ON").gameObject.SetActive(ChileElk.AgeFletcher().MiseryChileOrange);
            Sound_Manage.transform.Find("OFF").gameObject.SetActive(!ChileElk.AgeFletcher().MiseryChileOrange);
        });
        Mexican_Manage.onClick.AddListener(() =>
        {
            ChileElk.AgeFletcher().MexicanOrange = !ChileElk.AgeFletcher().MexicanOrange;
            Mexican_Manage.transform.Find("ON").gameObject.SetActive(ChileElk.AgeFletcher().MexicanOrange);
            Mexican_Manage.transform.Find("OFF").gameObject.SetActive(!ChileElk.AgeFletcher().MexicanOrange);
        });
    }

}
