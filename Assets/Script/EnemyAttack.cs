using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{   
    GameObject parent;
    Rigidbody2D parentRB;
    Enemy parentScript; // for access the attribute in script
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() 
    {
        parent = this.transform.parent.gameObject;
        if (parent.GetComponent<Rigidbody2D>() != null)
        {
            parentRB = parent.GetComponent<Rigidbody2D>();
        }
        parentScript = this.transform.parent.GetComponent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") 
        {   
            if (Player.instance.isParrying) 
            {
                Player.instance.parrySuccessful = true;
                if (parentRB != null) 
                {   
                    if (transform.position.x < Player.instance.transform.position.x)
                    {
                        parentRB.velocity = new Vector2(-1*600*Time.fixedDeltaTime, parentRB.velocity.y);
                    }
                    else 
                    {
                        parentRB.velocity = new Vector2(1*600*Time.fixedDeltaTime, parentRB.velocity.y);
                    }
                }
            }
            else 
            {   
                Player.instance.PlayerTakeDamage(parentScript.damage);
            }
        }   
    }
}
