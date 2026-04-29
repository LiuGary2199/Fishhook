using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrinyAwesome : TireStability<BrinyAwesome>
{
    public void DaleBriny(string info)
    {
        UIAwesome.AgeFletcher().DaleUIHobby(nameof(Briny), info);
    }
}
