using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TargetType
{
    Scene,
    UGUI
}
public enum LayoutType
{
    Sprite_First_Weight,
    Sprite_First_Height,
    Screen_First_Weight,
    Screen_First_Height,
    Bottom,
    Top,
    Left,
    Right
}
public enum RunTime
{
    Awake,
    Start,
    None
}
public class PikeSolemn : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("Target_Type")]    public TargetType Layout_Sick;
[UnityEngine.Serialization.FormerlySerializedAs("Layout_Type")]    public LayoutType Solemn_Sick;
[UnityEngine.Serialization.FormerlySerializedAs("Run_Time")]    public RunTime Eke_Duty;
[UnityEngine.Serialization.FormerlySerializedAs("Layout_Number")]    public float Solemn_Clause;
    private void Awake()
    {
        if (Eke_Duty == RunTime.Awake)
        {
            layoutExcite();
        }
    }
    private void Start()
    {
        if (Eke_Duty == RunTime.Start)
        {
            layoutExcite();
        }
    }

    public void layoutExcite()
    {
        if (Solemn_Sick == LayoutType.Sprite_First_Weight)
        {
            if (Layout_Sick == TargetType.UGUI)
            {

                float Wispy= Screen.width / Solemn_Clause;
                //GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.width / w * h);
                transform.localScale = new Vector3(Wispy, Wispy, Wispy);
            }
        }
        if (Solemn_Sick == LayoutType.Screen_First_Weight)
        {
            if (Layout_Sick == TargetType.Scene)
            {
                float Wispy= AgeBureauGush.AgeFletcher().RunRumbleEnure() / Solemn_Clause;
                transform.localScale = transform.localScale * Wispy;
            }
        }
        
        if (Solemn_Sick == LayoutType.Bottom)
        {
            if (Layout_Sick == TargetType.Scene)
            {
                float screen_bottom_y = AgeBureauGush.AgeFletcher().RunRumbleCanopy() / -2;
                screen_bottom_y += (Solemn_Clause + (AgeBureauGush.AgeFletcher().RunMidwayFuel(gameObject).y / 2f));
                transform.position = new Vector3(transform.position.x, screen_bottom_y, transform.position.y);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
