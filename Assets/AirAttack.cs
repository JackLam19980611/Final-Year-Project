using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttack : SupAttackColliderController
{
    public static AirAttack instance;
    // Start is called before the first frame update
    void Awake() 
    {
        instance = this;
    }
    protected override void Start()
    {   
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateAirAttack() 
    {
        attackCollider.enabled = true;
    }

    public void InActivateAirAttack() 
    {
        attackCollider.enabled = false;
    }

    protected new void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy") 
        {   
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.MonsterTakeDamage((int)(playerDamage*1.2));
        }
    }
}
