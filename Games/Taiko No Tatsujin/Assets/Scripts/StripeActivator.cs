using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripeActivator : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sr;
    bool withIllusion = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            withIllusion = !withIllusion;
            StartCoroutine(UpdateStripe());
        }
    }

    IEnumerator UpdateStripe()
    {
        if (withIllusion)
        {
            while (sr.material.GetFloat("_Opacity") < 1.0f)
            {
                sr.material.SetFloat("_Opacity", sr.material.GetFloat("_Opacity") + 0.03f);
                yield return null;
            }
        }
        else
        {
            while (sr.material.GetFloat("_Opacity") > 0.0f)
            {
                sr.material.SetFloat("_Opacity", sr.material.GetFloat("_Opacity") - 0.03f);
                yield return null;
            }
        }
    }
}
