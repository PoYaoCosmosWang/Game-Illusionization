using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if(player!=null)//可能已經被踩掉了
        {
            SendMessageUpwards("CheckPlayerCollision",player);
        }
    }
}
