using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Briny : ShedUIHobby
{
[UnityEngine.Serialization.FormerlySerializedAs("ToastText")]    public Text ToastWelt;

    

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);

        ToastWelt.text = uiFormParams.ToString();
        StartCoroutine(nameof(FortBloodBriny));
    }

    private IEnumerator FortBloodBriny()
    {
        yield return new WaitForSeconds(2);
        BloodUIJazz(GetType().Name);
    }

}
