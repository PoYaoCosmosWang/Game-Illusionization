using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //使用UnityEngine.AI

public static class Utility 
{
    //利用navigation將人走到對的位置
    //destnation: 要走到的位置
    //errorDistance: 多少誤差內是可以接受的
    //return 是否走到終點
    public static bool GoTo(NavMeshAgent navMesh, Vector3 destnation, float errorDistance = 2f)
    {
        Vector3 nowPosition = navMesh.transform.position;
        if(Vector3.Distance(nowPosition,destnation)<errorDistance) 
        {
            navMesh.updatePosition = false;
            //已經走到終點了
            return true;
        }
        navMesh.updatePosition = true;
        navMesh.SetDestination(destnation);
        return false;
    }


    //以同高度看向target
    public static void LookAtWithoutYOffest(Transform user,Vector3 target)
    {
        Vector3 newTarget = new Vector3(target.x,user.position.y,target.z);
        //Debug.Log(user.name + " look at " + target);
        user.LookAt(newTarget);
        
    }

 
    public static RaycastHit[] ConeCastAll(this Physics physics, Vector3 origin, float maxRadius, Vector3 direction, float maxDistance, float coneAngle)
    {
        RaycastHit[] sphereCastHits = Physics.SphereCastAll(origin - new Vector3(0,0,maxRadius), maxRadius, direction, maxDistance);
        List<RaycastHit> coneCastHitList = new List<RaycastHit>();
        
        if (sphereCastHits.Length > 0)
        {
            for (int i = 0; i < sphereCastHits.Length; i++)
            {
                sphereCastHits[i].collider.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);
                Vector3 hitPoint = sphereCastHits[i].point;
                Vector3 directionToHit = hitPoint - origin;
                float angleToHit = Vector3.Angle(direction, directionToHit);

                if (angleToHit < coneAngle)
                {
                    coneCastHitList.Add(sphereCastHits[i]);
                }
            }
        }

        RaycastHit[] coneCastHits = new RaycastHit[coneCastHitList.Count];
        coneCastHits = coneCastHitList.ToArray();

        return coneCastHits;
    }

    public static IEnumerator Timer(float totalTime, System.Action<float> callback, System.Action actionEnd){
        float timer = 0f;
        callback(timer);
        while (timer < totalTime){
            yield return null;
            timer = timer + Time.deltaTime;
            callback(timer);
        }
        callback(totalTime);
        actionEnd();
    }


}
