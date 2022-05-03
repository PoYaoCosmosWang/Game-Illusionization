using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateController : BaseStateController
{
    private int layerMask; //偵測敵人的mask

    private bool foundTarget;
    //Initialize
    private void OnEnable()
    {
        
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //FIXME
        //layerMask = character.layerMask;
        
        foundTarget = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(foundTarget)
        {
            return;
        }
        Sprite sp = null;//= GetOneEnemy();
        //TODO
        if(sp!=null)
        {
            Debug.Log("find " + sp.name);
            foundTarget = true;
            character.Target = sp;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Debug.Log("move");
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}


    protected Sprite GetOneEnemy()
    {
        List<Sprite> sprites = SearchEnemies();

        if(sprites.Count==0)//no enemy
        {
            return null;
        }
        List<float> probs = CreateAttackEnemiesProbabilities(sprites);
        int enemyIndex = RandomOneEnemy(probs);
        return sprites[enemyIndex];
    }

    //TODO
    private List<Sprite> SearchEnemies()
    {
        //偵測附近的敵人
        Collider[] hitColliders = Physics.OverlapSphere(character.transform.position, character.searchEnemyRadius, layerMask);
        List<Sprite> enemies = new List<Sprite>();
        foreach (Collider c in hitColliders)
        {
            enemies.Add(c.GetComponent<Sprite>());
        }
        
        return enemies;
    }

    //攻擊每個敵人的累進機率，要跟random 一起用

    protected virtual List<float> CreateAttackEnemiesProbabilities(List<Sprite> sprites)
    {
        List<float> probs = new List<float>();
        int sum = 0;
        
        //算出累進數值
        for(int i=0;i<sprites.Count;++i)
        {
            sum++;
            probs.Add(sum);
        }

        //同除總量變為機率
        for(int i =0;i<sprites.Count;++i)
        {
            probs[i] /= sum;
            
        }

        return probs;
    }

    //TODO   
    //透過機率選出一個enemy(第幾個enemy)
    private int RandomOneEnemy(List<float> probabilities)
    {
        float rand = Random.Range(0f, 1f);
        int i = 0;
        while(rand>probabilities[i])
        {
            ++i;
        }
        return i;
    }
}
