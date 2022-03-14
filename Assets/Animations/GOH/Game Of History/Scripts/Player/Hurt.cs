using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Hurt : MonoBehaviour
{
    public int lifePoints = 5;
    public int MAX_LIFE_POINTS = 5;
    private readonly bool isHurt = false;
    public float MAX_COOL_DOWN = 1.4f;
    public float coolDown;
    public GameObject heart; 
    public float heartDistance = 5;
    public List <GameObject> heartList;
    public Transform cameraPosition;
      

    public List<string> hurtTagList = new List<string>(new string[] {"burn"});
    public const float INVENCIBILTY_TIME = 1.2f;
    float hitTime = INVENCIBILTY_TIME;


 

    void Start()
    {
        coolDown = MAX_COOL_DOWN; 
        lifePoints = MAX_LIFE_POINTS;   
        heartList = new List<GameObject>();  

        AddHearts();
        InstantiateHearts(); 
    } 

    void AddHearts()
    {
        for (int i = 0; i < MAX_LIFE_POINTS; i++)
        {
            heartList.Add(heart);
        }
    }

    void InstantiateHearts()
    {       
        for (int i = 0; i < heartList.Count; i++)
        {          
            Instantiate(heartList[i], new Vector2(cameraPosition.GetChild(0).position.x + heartDistance * i, cameraPosition.GetChild(0).position.y), Quaternion.identity, cameraPosition.GetChild(0)); 
        }
    }

    void DestroyHearts()
    {
        for (int i = 0; i < cameraPosition.GetChild(0).childCount; i++)
        {
            if(cameraPosition.GetChild(0).GetChild(i).CompareTag("UIHeart"))
                Destroy(cameraPosition.GetChild(0).GetChild(i).gameObject);
        }
    } 
    string checkHurtList(GameObject other)
    {
        foreach (var element in hurtTagList)
        {
            if (element.Equals(other.gameObject.tag))
                return other.gameObject.tag;
        } 
        return null;
    }

    private void GetHurt(GameObject other)
    {
        if (coolDown > MAX_COOL_DOWN -Time.deltaTime && GetComponent<ControlAnimations>().AnimationHasFinished("Damage") && lifePoints > 0 && checkHurtList(other) != null  )
        {

             if(!transform.tag.Equals("godmode") || transform.tag.Equals("godmode") && checkHurtList(other) == "burn")
            {
                GetComponent<Animator>().Play("Damage");
                lifePoints--;
                Destroy(cameraPosition.GetChild(0).GetChild(lifePoints+1).gameObject);
                heartList.RemoveAt(lifePoints);
                coolDown = 0;
            } 
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
 
        GetHurt(other.gameObject);
        

    }
    private void OnCollisionStay2D(Collision2D other)
    {
 
        GetHurt(other.gameObject);

    }
    private void OnTriggerStay2D(Collider2D other)
    {
 
        GetHurt(other.gameObject);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "heart")
        {
            Destroy(other.gameObject);
            lifePoints++;
            heartList.Add(heart);
            DestroyHearts();
            InstantiateHearts();
        }
        GetHurt(other.gameObject);

    }
 
    private void InvencibilityFrames()
    {
        if ((!GetComponent<ControlAnimations>().AnimationHasFinished("Punch") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= .5f || !GetComponent<ControlAnimations>().AnimationHasFinished("Kick")) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= .2f)
        {
            transform.tag = "Player";  
        }
        if ((!GetComponent<ControlAnimations>().AnimationHasFinished("Punch") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > .3f || !GetComponent<ControlAnimations>().AnimationHasFinished("Kick")) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > .2f)
        {
            hitTime = INVENCIBILTY_TIME; 
 
            transform.tag = "godmode";
            transform.GetChild(0).GetComponentInChildren<BoxCollider2D>().enabled = true;
 
           
        }
        
        else if (GetComponent<ControlAnimations>().AnimationHasFinished("Punch") && GetComponent<ControlAnimations>().AnimationHasFinished("Kick"))
        {
            if (hitTime < 0.01f)
            {
                hitTime = INVENCIBILTY_TIME;

                transform.tag = "Player";
                 
            } 


            transform.GetChild(0).GetComponentInChildren<BoxCollider2D>().enabled = false;
        }

        if(transform.tag == "godmode")
            hitTime -= Time.deltaTime;

    }

    private void LateUpdate()
    {

        if (lifePoints == -1)
        {
            GetComponent<ControlAnimations>().enabled = false;
            GetComponent<Jump>().JumpVelocity = 0;
            GetComponent<Move>().horizontal = 0;
            GetComponent<InvertScale>().enabled = false;

            GetComponent<Move>().enabled = false;
            GetComponent<InvertScale>().enabled = false;
            GetComponent<Climb>().enabled = false;
            GetComponent<Dash>().enabled = false;

            GetComponent<Duck>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            if (GetComponent<ControlAnimations>().isGrounded)            
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            


            transform.gameObject.layer = LayerMask.NameToLayer("Ghost");
        }

    }

    private void Update()
    {
        InvencibilityFrames();

 
        if (Input.GetButtonDown("Submit") || transform.position.y < -999)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        
        


        if (coolDown < MAX_COOL_DOWN)
        { 
            GetComponent<SpriteRenderer>().color = new Color(1,1,1, 0.8f);
            
            coolDown += Time.deltaTime; 

        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
 
    }




}
