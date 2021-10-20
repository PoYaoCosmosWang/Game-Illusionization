using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheater : MonoBehaviour
{
    public GoldCoinCreator coinCreator;
    public StripeCreator stripeCreator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            coinCreator.Create(0, 2); // yellow up
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            coinCreator.Create(1, -1.5f); // blue down
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            coinCreator.Create(2, 2); // orange up
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            coinCreator.Create(3, -2.5f); // purple down
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            coinCreator.Create(4, -1.5f); // red down
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            stripeCreator.Create();
        }
    }
}
