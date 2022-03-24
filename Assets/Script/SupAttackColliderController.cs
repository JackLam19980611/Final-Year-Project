using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupAttackColliderController : MonoBehaviour
{   
    protected PolygonCollider2D attackCollider;
    protected int playerDamage;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        attackCollider = GetComponent<PolygonCollider2D>();
        playerDamage = Player.instance.pDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy") 
        {
            other.gameObject.GetComponent<Enemy>().MonsterTakeDamage(playerDamage);
        }
    }
}
