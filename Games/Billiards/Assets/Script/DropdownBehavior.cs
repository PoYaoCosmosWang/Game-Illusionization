using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownBehavior : MonoBehaviour
{
    public GameObject gameManager;
    private Game gameManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        DropdownOptionSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate {
            DropdownOptionSelected(dropdown);
        });
    }

    void DropdownOptionSelected(Dropdown dropdown)
    {
        gameManagerScript = gameManager.GetComponent<Game>();
        int value = dropdown.value;
        if(value == 0)
        {
            gameManagerScript.useIllusion = true;
        }
        else
        {
            gameManagerScript.useIllusion = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
