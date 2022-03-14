using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{

    public List<string> hurtTagList = new List<string>(new string[] { "burn" });
    public int lifePoints = 5;
    public int MAX_LIFE_POINTS = 5;
    [Range (0,100)]
    public float ANGRY_VELOCITY = 30;
    public float COOLDOWN_TIME;
    public float coolDown;
    public float destroyTime;

    private readonly bool isHurt = false; 
    private Vector2 lastPosition;  

    void Start()
    { 
        lifePoints = MAX_LIFE_POINTS;
        coolDown = COOLDOWN_TIME; 
        lastPosition = transform.position; 
    } 

    private void InvertScale()
    {
        if (lastPosition.x - transform.position.x < -0.01f )
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);

        else if (lastPosition.x - transform.position.x > 0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);

        lastPosition = transform.position;
    } 

    string CheckHurtList(GameObject other)
    {
        foreach (var element in hurtTagList)
        {
            if (element.Equals(other.gameObject.tag))
                return element;
        } 

        return null;
    }

  
    private void OnTriggerStay2D(Collider2D other)
    {
        if(coolDown >= COOLDOWN_TIME)
            GetHurt(other.gameObject);

    }
 
    private void GetHurt(GameObject other)
    {
        if (GetComponent<ControlSimpleAnim>().AnimationHasFinished("Die") && lifePoints > 0 && CheckHurtList(other) != null)
        { 
            GetComponent<Animator>().Play("Damage");
            lifePoints--;  
            coolDown = 0;

            if(GetComponent<MoveToRB>() != null)
                GetComponent<MoveToRB>().velocity = ANGRY_VELOCITY;

            if(GetComponent<InstantiateAtRuntime>() != null)
                GetComponent<InstantiateAtRuntime>().enabled = true;

            if (other.gameObject.transform.parent != null )
            { 
                if(other.gameObject.GetComponent<ControlAnimations>() != null)
                { 
                    if (!other.gameObject.GetComponentInParent<ControlAnimations>().AnimationHasFinished("Punch") && other.gameObject.transform.parent.name.Equals("Gnome"))
                        lifePoints--; 
                } 
            } 
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    { 
        if (coolDown >= COOLDOWN_TIME)
            GetHurt(other.gameObject); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    { 
        if (coolDown >= COOLDOWN_TIME)
            GetHurt(other.gameObject); 
    }

    private void Update()
    { 
        InvertScale();
        if (coolDown < COOLDOWN_TIME)
            coolDown += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (lifePoints == -1)
        {
            GetComponent<ControlSimpleAnim>().enabled = false;

            if (GetComponent<InstantiateAtRuntime>() != null)
                GetComponent<InstantiateAtRuntime>().enabled = false;

            GetComponentInChildren<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; 

            gameObject.tag = "Untagged"; 
            Destroy(this.gameObject, destroyTime); 
        } 
    }

}
