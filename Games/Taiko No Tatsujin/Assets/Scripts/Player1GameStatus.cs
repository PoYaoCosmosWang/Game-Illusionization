using UnityEngine;

public static class Player1GameStatus
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
    public static int maxCombo = 0;
    public static int goodCount = 0;
    public static int okCount = 0;
    public static int badCount = 0;
    public static int score = 0;

    public static string state = "";

    public static KeyCode leftKaKey = KeyCode.A;
    public static KeyCode leftDonKey = KeyCode.S;
    public static KeyCode rightDonKey = KeyCode.D;
    public static KeyCode rightKaKey = KeyCode.F;

    public static void UpdateOnHit(){
        if(isGood){
            score += 20;
            combo += 1;
            goodCount += 1;
            state = "GOOD";
        }else if(isOk){
            score += 5;
            combo += 1;
            okCount += 1;
            state = "OK";
        }else if(isBad){
            score += 1;
            combo += 1;
            badCount += 1;
            state = "BAD";
        }else{
            combo = 0;
            state = "";
        }

        if (combo > maxCombo){
            maxCombo = combo;
        }
        // Debug.Log("score " + score + "\tcombo " + combo);
    }

    public static void UpdateOnMiss(){
        state = "";
        combo = 0;
        Debug.Log("Miss");
    }

    public static void Reset(){
        hasStarted = false;

        isGood = false;
        isOk = false;
        isBad = false;

        isBigDon = false;
        isSmallDon = false;
        isBigKa = false;
        isSmallKa = false;

        combo = 0;
        maxCombo = 0;
        goodCount = 0;
        okCount = 0;
        badCount = 0;
        score = 0;

        state = "";
    }
}
