using UnityEngine;

public static class Player2GameStatus
{
    public static bool hasStarted = false;

    public static bool isGood = false;
    public static bool isOk = false;
    public static bool isBad = false;

    public static bool isBigDon = false;
    public static bool isSmallDon = false;
    public static bool isBigKa = false;
    public static bool isSmallKa = false;

    public static int combo = 0;
    public static int score = 0;

    public static string state = "";

    public static KeyCode leftKaKey = KeyCode.J;
    public static KeyCode leftDonKey = KeyCode.K;
    public static KeyCode rightDonKey = KeyCode.L;
    public static KeyCode rightKaKey = KeyCode.Semicolon;

    public static void UpdateOnHit(){
        if(isGood){
            score += 20;
            combo += 1;
            state = "GOOD";
        }else if(isOk){
            score += 5;
            combo += 1;
            state = "OK";
        }else if(isBad){
            score += 1;
            combo += 1;
            state = "BAD";
        }else{
            state = "";
        }
        Debug.Log("score " + score + "\tcombo " + combo);
    }

    public static void UpdateOnMiss(){
        state = "";
        combo = 0;
        Debug.Log("Miss");
    }
}
