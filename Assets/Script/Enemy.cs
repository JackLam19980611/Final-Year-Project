using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{   
    public int hp, damage;
    private Animator anim;
    private bool deathAnimationGetPlayed;
    private Collider2D coll;
    public GameObject floatDamage;
    public Rigidbody2D rB;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        hp = 70;
        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0) 
        {   
            if (!deathAnimationGetPlayed)
            {
                Deathanimation();
                deathAnimationGetPlayed = true;
            }
        }
    }

    public void MonsterTakeDamage(int playerDamage) 
    {   
        hp -= playerDamage;
        GameObject fD = Instantiate(floatDamage, transform.position, Quaternion.identity) as GameObject;
        fD.transform.GetChild(0).GetComponent<TMP_Text>().text = playerDamage.ToString();
    }

    void Deathanimation() 
    {
        anim.SetTrigger("death");
        coll.enabled = false;
    }

    void DestroyEnemy() 
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {   
            if (Player.instance.isParrying) 
            {
                Player.instance.parrySuccessful = true;
                if (rB != null) 
                {
                    if (transform.position.x < Player.instance.transform.position.x)
                    {
                        rB.velocity = new Vector2(-1*600*Time.fixedDeltaTime, rB.velocity.y);
                    }
                    else 
                    {
                        rB.velocity = new Vector2(1*600*Time.fixedDeltaTime, rB.velocity.y);
                    }
                }
            }
            else 
            {
                Player.instance.PlayerTakeDamage(damage);
            }
        }
    }    
}
