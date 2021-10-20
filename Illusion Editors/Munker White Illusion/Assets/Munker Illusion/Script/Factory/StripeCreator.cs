using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stripe
{
    public GameObject stripe_object;
    public Color stripe_color;
}

public class StripeCreator : MonoBehaviour
{
    //public bool ShowStripe = false;
    //public GameObject[] stripeElements;
    private const int SIZE = 2;
    public Stripe[] StripeElements = new Stripe[SIZE];
    [Range(0, 10)]
    public float AreaHeight = 5;
    [Range(0, 10)]
    public float AreaWidth = 5;
    public bool AdvanceSetting;
    [Range(1f, 10f)]
    public float Intensity = 1;
    [Range(0.1f, 0.3f)]
    public float StripeInterval = 0.1f;
    [Range(0.01f, 0.02f)]
    public float StripeWidth = 0.02f;
    [Range(0f, 1f)]
    public float Ratio = 0.5f;
    float colorAlpha = 1f;
    List<GameObject> currentStripe = new List<GameObject>();

    private void OnValidate()
    {
        if (StripeElements.Length != SIZE)
        {
            Debug.LogWarning("Don't change the 'stripeElements' field's array size!");
            System.Array.Resize(ref StripeElements, SIZE);
        }

        /*if (Application.isPlaying && GameObject.FindGameObjectWithTag("GameManager") == null)
        {
            Hide();
            Create();
        }
        */
        if (Application.isPlaying)
        {
            Hide();
            Create();

        }
    }


    public void Create()
    {
        //ShowStripe = true;
        if (!AdvanceSetting)
        {
            Ratio = 0.5f;
            StripeInterval = 0.3f - (float)(Intensity / 10) * 0.2f;
            StripeWidth = 0.02f - (float)(Intensity / 10) * 0.01f;
            Debug.Log("StripeInterval = " + StripeInterval);
            Debug.Log("StripeWidth = " + StripeWidth);
        }
        float half = AreaHeight * Ratio;
        int count = 0;
        Transform[] ts = GetComponentsInChildren<Transform>();
        if (currentStripe.Count == 0)
        {
            float i = transform.position.z - AreaHeight / 2;
            while (i < half - AreaHeight / 2)
            {
                count = (count + 1) % 2;
                GameObject stripe = Instantiate(StripeElements[count].stripe_object) as GameObject;
                stripe.transform.parent = gameObject.transform;
                stripe.transform.localScale = new Vector3(AreaWidth, StripeWidth, AreaHeight);

                Color newColor = StripeElements[count].stripe_color;
                newColor.a = this.colorAlpha;
                stripe.GetComponent<SpriteRenderer>().material.color = newColor;

                //stripe.GetComponent<SpriteRenderer>().size = new Vector2(25f, playAreaHeight / stripeNumber);
                if (count == 0)
                {
                    stripe.transform.position = new Vector3(transform.position.x, 5, i);
                }
                else
                {
                    stripe.transform.position = new Vector3(transform.position.x, 0.2f, i);
                }
                currentStripe.Add(stripe);
                i += (StripeWidth + StripeInterval);
            }
            while (i < AreaHeight / 2)
            {
                count = (count + 1) % 2;
                GameObject stripe = Instantiate(StripeElements[count].stripe_object) as GameObject;
                stripe.transform.parent = gameObject.transform;
                stripe.transform.localScale = new Vector3(AreaWidth, StripeWidth, AreaHeight);

                Color newColor = StripeElements[count].stripe_color;
                newColor.a = this.colorAlpha;
                stripe.GetComponent<SpriteRenderer>().material.color = StripeElements[count].stripe_color;
                //stripe.GetComponent<SpriteRenderer>().size = new Vector2(25f, (float)playAreaHeight / stripeNumber);
                if (count == 0)
                {
                    stripe.transform.position = new Vector3(transform.position.x, 0.2f, i);
                }
                else
                {
                    stripe.transform.position = new Vector3(transform.position.x, 5, i);
                }
                i += (StripeWidth + StripeInterval);
                currentStripe.Add(stripe);
            }
        }
    }

    public IEnumerator CreateTranspanrentSprite()
    {
        for (this.colorAlpha = 0f; this.colorAlpha <= 1f; this.colorAlpha += 0.01f)
        {
            Debug.Log("createTranspanrentSprite, this.colorAlpha = " + this.colorAlpha);
            Hide();
            Create();
            yield return new WaitForSeconds(0.1f);
        }
        yield return 0;
    }

    public void Hide()
    {
        foreach (GameObject child in currentStripe)
        {
            Destroy(child);
        }
        currentStripe.Clear();
        //Transform[] ts = GetComponentsInChildren<Transform>();
        //foreach (Transform child in ts)
        //{
        //    GameObject.Destroy(child);
        //}
    }
    private void OnDisable()
    {
        Hide();
    }
    private void OnApplicationQuit()
    {
        Hide();
    }
}
