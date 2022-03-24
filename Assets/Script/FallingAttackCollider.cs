using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAttackCollider : SupAttackColliderController
{
    public static FallingAttackCollider instance;
    public int knockBackDistance;
    
    void Awake() 
    {
        instance = this;
    }
    // Start is called before the first frame update
    protected override void Start()
    {   
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateFallingAttack() 
    {
        attackCollider.enabled = true;
    }

    public void InactivateFallingAttack() 
    {
        attackCollider.enabled = false;
    }

    protected new void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.MonsterTakeDamage((int)(playerDamage*2));
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
    }
}
