using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//player 如何呈現出移動，以及確認有沒有辦法移動和實際位移player
public class PlayerController : MonoBehaviour
{
    public bool isActive = true;
    private bool isRolling = false;

    public float speed;


    //0:down
    private readonly Vector2[] directions = new Vector2[]
    {
        /*new Vector2(0,-1), //down
        new Vector2(-1,0), //left
        new Vector2(1,0),//right
        new Vector2(0,1)//up
        */
        new Vector2(-1,0), //down
        new Vector2(0,-1),//left
        new Vector2(0,1), //right
        new Vector2(1,0),//up
    };
    private Vector3[] rotationPositions;

    //設定了就變成start position
    [SerializeField]
    private Vector2 positionOnMap;
    [SerializeField]
    private Map map;
    // Start is called before the first frame update
    private void Awake()
    {
        
        Bounds bound = GetComponent<BoxCollider>().bounds;
        rotationPositions = new Vector3[]
        {
            /*
            new Vector3(0, -bound.size.y / 2, -bound.size.z / 2), //down
            new Vector3(-bound.size.x / 2, -bound.size.y / 2, 0),//left
            new Vector3(bound.size.x / 2, -bound.size.y / 2, 0),//right
            new Vector3(0, -bound.size.y / 2, bound.size.z / 2) // up
            */
            new Vector3(-bound.size.x / 2, -bound.size.y / 2, 0),//down
            new Vector3(0, -bound.size.y / 2, bound.size.z / 2), // left
            new Vector3(0, -bound.size.y / 2, -bound.size.z / 2), //right
            new Vector3(bound.size.x / 2, -bound.size.y / 2, 0),//up
        };

    }
    private void Start()
    {
        Reset();
    }
    private void OnEnable()
    {
        Reset();
    }
    private void Reset()
    {
        isRolling = false;
        transform.position = new Vector3(positionOnMap.x, 0.5f, map.height-1 - positionOnMap.y);
        transform.rotation = Quaternion.identity;
    }
    //move animation
    private IEnumerator MoveCoroutine(Vector3 positionToRotation)
    {
        isRolling = true;
        float angle = 0;
        Vector3 point = transform.position + positionToRotation;
        Vector3 axis = Vector3.Cross(Vector3.up, positionToRotation).normalized;

        while (angle < 90f)
        {
            float angleSpeed = Time.deltaTime + speed;
            transform.RotateAround(point, axis, angleSpeed);
            angle += angleSpeed;
            yield return null;
        }
        transform.RotateAround(point, axis, 90 - angle);
        isRolling = false;
    }

    //透過0.5x +1.5y +1.5來得出0123
    private float DirectionMapping(Vector2 input)
    {
        float calculate = 0.5f * input[0] + 1.5f * input[1] + 1.5f;
        return calculate;
    }

    //用player input去access，對應到Move
    public void OnMove(InputValue value)
    {
        
        //Debug.Log(value.Get<Vector2>());
        Vector2 input = value.Get<Vector2>();
        float mappingDir = DirectionMapping(input);
        if(mappingDir==1.5f) // 0,0，不用動
        {
            return;
        }

        Move((int)mappingDir);
        
    }

    //真正移動的地方
    public void Move(int dir)
    {
        Vector2 nexPosition = positionOnMap + directions[dir];
        if ( !isActive ||isRolling || !map.IsValid(nexPosition) )
        {
            return;
        }

        Vector3 dirVector = rotationPositions[dir];
        positionOnMap = nexPosition;
        StartCoroutine(MoveCoroutine(dirVector));
    }

}
