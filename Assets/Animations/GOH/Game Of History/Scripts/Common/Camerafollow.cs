 
using UnityEngine;

public class Camerafollow : MonoBehaviour {

    public Transform target;
    public GameObject player;
    public bool disable;

    private float smoothSpeed=0.4f; 
    public Vector2 OffsetVector;
    public Vector2 offsetUI;

    public float MIN_SIZE;
    public float MAX_SIZE;
    
    public float MAX_OFFSET_X;
    public float MAX_OFFSET_Y;
    public float MIN_OFFSET_X;
    public float MIN_OFFSET_Y;
    private const int GROUND = 72; 
    private bool PlayerRight;

    private float closertimer = 0.0f;
    private float furthertimer = 0.0f;

    private float initialSize;

    private void Start()
    {
        Cursor.visible = false; 

        initialSize = GetComponent<Camera>().orthographicSize;
    }
    void ChangeSize(float FinalSize,float speed)
    {
        transform.GetComponent<Camera>().orthographicSize = Mathf.SmoothStep(transform.GetComponent<Camera>().orthographicSize, FinalSize, speed* Time.deltaTime);
    }

    void ChangeUIScale()
    {

        var xy = GetComponent<Camera>().orthographicSize / initialSize;
        foreach (Transform child in transform)
        {
            child.localScale = new Vector2(xy, xy);
        }
        transform.GetChild(0).position = (Vector2)transform.position + new Vector2(GetComponent<Camera>().orthographicSize * offsetUI.x, GetComponent<Camera>().orthographicSize * offsetUI.y);
 
    }

    void ChangeOffset(float offset_x, float offset_y, float speed)
    {

        OffsetVector.y = Mathf.SmoothStep(OffsetVector.y, offset_y, Time.deltaTime * speed );
        OffsetVector.x = Mathf.SmoothStep(OffsetVector.x, offset_x, Time.deltaTime);
    }
 
    private void Update()
    {

      

            Vector2 desiredPosition = new Vector2(target.position.x, target.position.y) + OffsetVector;
            transform.position = new Vector2(Mathf.SmoothStep(transform.position.x, desiredPosition.x, smoothSpeed), Mathf.SmoothStep(transform.position.y, desiredPosition.y,smoothSpeed)); // camerafollow  
            if (disable == false)
            { 
                if (Mathf.Round(transform.GetComponent<Camera>().orthographicSize) > MIN_SIZE && player.GetComponent<Rigidbody2D>().velocity.magnitude < 1f)//get closer
                {
                    furthertimer = 0.0f;
                    closertimer = closertimer + 0.002f;

                    ChangeSize(MIN_SIZE, closertimer);
                    ChangeOffset(MIN_OFFSET_X, MIN_OFFSET_Y, closertimer);
                }


                else if (transform.GetComponent<Camera>().orthographicSize < MAX_SIZE && player.GetComponent<Rigidbody2D>().velocity.magnitude > 1f)//get further
                {
                    closertimer = 0.0f;
                    furthertimer = furthertimer + 0.002f;

                    ChangeSize(MAX_SIZE, furthertimer);
                    ChangeOffset(MAX_OFFSET_X, MAX_OFFSET_Y, furthertimer);
                }

 
            ChangeUIScale();

        }

    }

}