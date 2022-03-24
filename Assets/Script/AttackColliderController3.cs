using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderController3 : SupAttackColliderController
{
    public static AttackColliderController3 instance;
    
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

    public void ActivateAttack3() 
    {
        attackCollider.enabled = true;
    }

    public void InactivateAttack3() 
    {
        attackCollider.enabled = false;
    }

    protected new void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy") 
        {
            other.gameObject.GetComponent<Enemy>().MonsterTakeDamage((int)(playerDamage*1.5));
        }
    }
}