using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelManager : MonoBehaviour
{
    public static SelectLevelManager instance;
    // Start is called before the first frame update
    public bool isSakai = false;
    public string prefixFileName = "MapInfo/Sakai";
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    public void SelectLevel(int level)
    {
        switch (level)
        {
            case 1:
                isSakai = false;
                prefixFileName = "MapInfo/Tutorial";
                print("train");
                break;
            case 2:
                isSakai = false;
                prefixFileName = "MapInfo/Normal";
                print("no illusion");
                break;
            case 3:
                isSakai = true;
                prefixFileName = "MapInfo/Sakai";
                print("with illusion");
                break;
            default:
                break;

        }

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
