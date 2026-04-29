using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IronControl : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("mask")]    public RectTransform Nose;
[UnityEngine.Serialization.FormerlySerializedAs("mypageview")]    public TautHurt Contractor;
    private void Awake()
    {
        Contractor.ToTautBorder = Counteract;
    }

    void Counteract(int index)
    {
        if (index >= this.transform.childCount) return;
        Vector3 pos= this.transform.GetChild(index).GetComponent<RectTransform>().position;
        Nose.GetComponent<RectTransform>().position = pos;
    }
}
