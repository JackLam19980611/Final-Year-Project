using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderController1 : SupAttackColliderController
{
    public static AttackColliderController1 instance;
    
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

    public void ActivateAttack1() 
    {
        attackCollider.enabled = true;
        GameObject parent = transform.parent.gameObject;
        Debug.Log(parent.transform.position.x);
    }

    public void InactivateAttack1() 
    {
        attackCollider.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D other) 
    {
        base.OnTriggerEnter2D(other);  
    }
}
