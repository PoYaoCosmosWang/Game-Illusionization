using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //public static BattleManager instance;

    #region battle parameters
    private int totalWaves; //超過waves就代表完成了
    private List<int> enemiesNumInWaves;//每批敵人有幾個，都解決完才到下一批

    private int currentWave;
    private int currentEnemiesNum; //現在數到第幾隻了


    private int _currentDeadEnemiesNum;
    public int CurrentDeadEnemiesNum
    {
        get
        {
            return _currentDeadEnemiesNum;
        }
        set
        {
            _currentDeadEnemiesNum = value;
            CheckWaveEnd();
        }
    }

    #endregion

    

    //enemies
    [SerializeField]
    private GameObject[] terroristInstance;
    [SerializeField]
    private GameObject[] policeInstance;


    private BattleData battleData;
    // Start is called before the first frame update

    
    void Start()
    {

        //ResetBattleInfo("BattleData/" + fileName);
        
    }

    //先load battle file ，再開始戰鬥
    public void StartBattle(string fileName)
    {
        LoadBattleData(fileName);
        SpawnPoliceWave();
        SpawnNextEnemiesWave();

    }

    private void LoadBattleData(string fileName)
    {
        //battleData = Resources.Load(fileName, typeof(BattleData)) as BattleData; //用於scriptable object

        //用prefab
        battleData = Resources.Load<GameObject>("BattleData/"+fileName).GetComponent<BattleData>();
        enemiesNumInWaves = new List<int>();
        totalWaves = 0;
        int s = 0;
        for (int i = 0; i < battleData.terroristData.Length; ++i)
        {
            if (battleData.terroristData[i].time == totalWaves)//如果還是現在這波，此波敵人數++
            {
                s++;
            }
            else
            {
                //換到下一波了
                enemiesNumInWaves.Add(s);
                totalWaves++;
                s = 1;
            }
        }
        enemiesNumInWaves.Add(s);
        totalWaves++;
        currentEnemiesNum = 0;
        currentWave = 0;
    }

    private bool CheckBattleEnd()
    {
        if (currentWave >= totalWaves)
        {
            SendMessageUpwards("BattleEnd");
            //this.enabled = false;
            return true;
        }
        return false;
    }

    //current dead enemies num增加時呼叫
    private void CheckWaveEnd()
    {
        //死完了，下一波
        if(CurrentDeadEnemiesNum >= enemiesNumInWaves[currentWave])
        {
            currentWave++;
            //先檢查是否結束了
            if(CheckBattleEnd())
            {
                return;
            }

            SpawnNextEnemiesWave();
        }
    }

    private void SpawnPoliceWave()
    {
        for(int i=0;i<battleData.policeData.Length;++i)
        {
            SpawnPolice(battleData.policeData[i]);
        }
    }

    private void SpawnNextEnemiesWave()
    {
        //此波最後一位敵人 = currentNum + 此波數量 -1
        int nextEnemiesNum = currentEnemiesNum + enemiesNumInWaves[currentWave];
        for(int i=currentEnemiesNum;i<nextEnemiesNum;++i) //for迴圈把超出1的lastnum修正了
        {
            SpawnTerrorist(battleData.terroristData[i]);
        }
        //同時清除所有死亡人數
        _currentDeadEnemiesNum = 0;
        currentEnemiesNum = nextEnemiesNum;
    }


    private Character SpawnCharacter( CharacterData ch, GameObject instance )
    {
        //FIXME
        GameObject cha = Instantiate(instance,ch.originPosition,Quaternion.identity);
        Character c = cha.GetComponent<Character>();
        c.Initialize(ch.navigationalPosition);
        return c;
        //c.dieEvent.AddListener(EnemyDie);
    }
    
    private void SpawnTerrorist(CharacterData ch)
    {
        Character c = SpawnCharacter(ch,terroristInstance[ch.prefabIndex]);
        c.dieEvent.AddListener(EnemyDie);
    }
    private void SpawnPolice(CharacterData ch)
    {
        SpawnCharacter(ch,policeInstance[ch.prefabIndex]);
    }

    public void EnemyDie()
    {
        CurrentDeadEnemiesNum++;
    }

  
}
