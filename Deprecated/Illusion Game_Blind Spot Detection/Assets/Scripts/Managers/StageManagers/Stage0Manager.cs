using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0Manager : BaseManager
{
    [SerializeField]
    private Vector3 playerDestnation;
    private NavigationManager naviManager;
    // Start is called before the first frame update
    void Start()
    {
        naviManager = GetComponentInChildren<NavigationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Go()
    {
        base.Go();
        naviManager.StartNavigation(playerDestnation);
    }
    
    private void NavigationEnd()
    {
        StageComplete();
    }
}
