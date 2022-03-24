using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public static PlayerHitBox instance;
    [SerializeField] Collider2D hitBox;
    [SerializeField] int knockBackDistance;
    //public Rigidbody2D HBRB;
    // Start is called before the first frame update

    void Awake() 
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HBRB = Player.instance.rB;
    }

    public void ActivateHitBox() 
    {
        hitBox.enabled = true;
    }

    public void InActivateHitBox() 
    {
        hitBox.enabled = false;
    }

    /*private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy") 
        {   
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (Player.instance.isParrying) 
            {
                Player.instance.parrySuccessful = true;
                if (enemy.rB != null) 
                {
                    if (enemy.transform.position.x < Player.instance.transform.position.x)
                    {
                        enemy.rB.velocity = new Vector2(-1*knockBackDistance*Time.fixedDeltaTime, enemy.rB.velocity.y);
                    }
                    else 
                    {
                        enemy.rB.velocity = new Vector2(1*knockBackDistance*Time.fixedDeltaTime, enemy.rB.velocity.y);
                    }
                }
            }
            else 
            {
                Player.instance.PlayerTakeDamage(enemy.damage);
            }
        }
    }*/
}
