using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PerceivedMotion
{ 
    Vertical,
    Horizontal,
    Custom,
}


public class MotionOrganizationCreator : MonoBehaviour
{
    public List<GameObject> SurroundingObjects;
    public float SurroundingGroupRadius;

    public GameObject CenterObject;

    [Header("Perceieved Motion")]
    public PerceivedMotion CenterObjectPerceivedMotion = PerceivedMotion.Vertical;

    [Space]
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]  
    public int CenterClockwise = -1;
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]  
    public Vector3 CenterCenter = new Vector3(0, 0, 0);
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]
    public float CenterRotatingSpeed = 180f;
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]
    public float CenterRotatingRadius = 1f;
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]
    public float CenterInitialAngle = 0f;
    [Space]
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]
    public int SurroundingClockwise = 1;
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]
    public Vector3 SurroundingCenter = new Vector3(0, 1, 0);
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]
    public float SurroundingRotatingSpeed = 180f;
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]
    public float SurroundingRotatingRadius = 1f;
    [DrawIf("CenterObjectPerceivedMotion", PerceivedMotion.Custom)]
    public float SurroundingInitialAngle = 180f;

    public GameObject TrailParticleSystem;

    public void SpawnGroup(Vector3 position)
    {
        GameObject relativeRoot = new GameObject();
        relativeRoot.name = "Relative Motion Object Group";
        relativeRoot.transform.position = position;

        // Center object initialization
        GameObject center = Instantiate(CenterObject, relativeRoot.transform);
        center.name = "Center";
        Move centerMove = center.AddComponent<Move>();
        centerMove.Center = CenterCenter;
        centerMove.ClockWise = CenterClockwise;
        centerMove.RotatingSpeed =  CenterRotatingSpeed;
        centerMove.radius = CenterRotatingRadius;
        centerMove.angle = CenterInitialAngle;

        GameObject ps1 =  Instantiate(TrailParticleSystem, center.transform);
        centerMove.Trail = ps1.GetComponent<ParticleSystem>();


        // Surrounding objects initialization
        GameObject SurroundGroup = new GameObject();
        SurroundGroup.transform.parent = relativeRoot.transform;
        SurroundGroup.name = "Surrounding Group";
        Move surroundMove = SurroundGroup.AddComponent<Move>();
        surroundMove.Center =SurroundingCenter;
        surroundMove.ClockWise = SurroundingClockwise;
        surroundMove.RotatingSpeed = SurroundingRotatingSpeed;
        surroundMove.radius = SurroundingRotatingRadius;
        surroundMove.angle = SurroundingInitialAngle;

        for (int i = 0; i < SurroundingObjects.Count; ++i)
        { 
            GameObject surroundObj = Instantiate(SurroundingObjects[i], SurroundGroup.transform);
            float angle = 360 / SurroundingObjects.Count * i;
            surroundObj.transform.localPosition = new Vector3(SurroundingGroupRadius * Mathf.Cos(angle / 360f * 2f * Mathf.PI), SurroundingGroupRadius * Mathf.Sin(angle / 360f * 2f * Mathf.PI), 0);
        }
        GameObject ps2 = Instantiate(TrailParticleSystem, SurroundGroup.transform);
        surroundMove.Trail = ps2.GetComponent<ParticleSystem>();

    }
    void initialize()
    {
        if (CenterObjectPerceivedMotion == PerceivedMotion.Vertical)
        {
            CenterClockwise = -1;
            CenterCenter = new Vector3(0, 0, 0);
            CenterRotatingSpeed = 200f;
            CenterRotatingRadius = 1f;
            CenterInitialAngle = 0f;

            SurroundingClockwise = 1;
            SurroundingCenter = new Vector3(0, 0, 0);
            SurroundingRotatingSpeed = 200;
            SurroundingRotatingRadius = 1f;
            SurroundingInitialAngle = 0f;
        }

        else if (CenterObjectPerceivedMotion == PerceivedMotion.Horizontal)
        {
            CenterClockwise = -1;
            CenterCenter = new Vector3(0, 0, 0);
            CenterRotatingSpeed = 200f;
            CenterRotatingRadius = 1f;
            CenterInitialAngle = 180f;

            SurroundingClockwise = 1;
            SurroundingCenter = new Vector3(0, 0, 0);
            SurroundingRotatingSpeed = 200f;
            SurroundingRotatingRadius = 1f;
            SurroundingInitialAngle = 0f;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        initialize();

        SpawnGroup(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
