using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rB;
    public CapsuleCollider2D bodyCollider;
    public BoxCollider2D feetCollider;
    public static Player instance;
    [SerializeField] BoxCollider2D  crouchCollider, climbCollider;
    private LayerMask ground, wall;
    public float horizontalMove, rBGravity;
    public float speed, jumpingForce, runningSpeedAdjustment, originalSpeed;
    public Animator anim;
    public bool runGetPress, attackGetPress, jumpGetPress, downGetPress, downGetRelease, dodgeGetPress, parryGetPress,climbGetPress;
    public bool isMoving, isRunning, isAttacking, isCrouching, isJumping, isFalling, isClimbing, isFallingAttack, isDodging, isParrying, isGrabbing;
    public static int currentHP = 100, totalHP = 100;
    public int pDamage = 20, test = 0;
    public bool canMove = true, airAttacked, parrySuccessful, canParry= true;
    public GameObject JumpEffect, FallEffect;
    private bool fallingAttack; 
    public bool canJump = true, changeSide = true, GetPlay;
    public int dodgeSpeed;
    public Joystick joystick;
    Transform trans;
    public bool attackButton, grabButton;
    private SpriteRenderer sR;
    private Color originalColor;
    public float flashtime = 0.15f;
    private bool greenBox, redBox;
    public float redXOffset=0.32f, redYOffset=0.5f, redXSize=0.5f, redYSize=0.1f, greenXOffset=0.32f, greenYOffset=0.35f, greenXSize=0.5f, greenYSize=0.01f;
    public AudioSource walkingAudio, runningAudio, landingAudio, getHurtAudio;


    // Start is called before the first frame update
    
    void Awake() 
    {
        instance = this;
    }
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        feetCollider = transform.Find("VirtualBoxFeet").gameObject.GetComponent<BoxCollider2D>();
        climbCollider = transform.Find("VirtualBoxClimb").gameObject.GetComponent<BoxCollider2D>();
        ground = LayerMask.GetMask("Ground");
        wall = LayerMask.GetMask("Wall");
        originalSpeed = speed;
        rBGravity = rB.gravityScale;
        sR = GetComponent<SpriteRenderer>();
        originalColor = sR.color;
    }

    // Update is called once per frame
    void Update()
    {   
        greenBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (greenXOffset*transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXSize, greenYSize), 0f, ground); 
        redBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (redXOffset*transform.localScale.x), transform.position.y + redYOffset), new Vector2(redXSize, redYSize), 0f, ground); 
        JoystickChecking(); 
        if (joystick.Vertical < -0.7f)  
        {   
            downGetPress = true;
        }
        else if (joystick.Vertical>= 0) 
        {
            downGetPress = false;
            downGetRelease = true;
        }
        if (climbGetPress) 
        {
            isClimbing = !isClimbing;
            climbGetPress = false;
        }
        if ( downGetPress && attackButton && (anim.GetBool("jumping") || anim.GetBool("falling"))) 
        {
            fallingAttack = true;
        }
        Parry();
        Attack();
        AirAttacking();
        Grab();
    }

    void FixedUpdate() 
    {   
        if (!isDodging)
        {
            Movement();
        }
        Running();
        SwitchJumpingAnimation();
        Jumping();
        Crouch();
        Climb();
        FallingAttack();
        Dodge();
    }

    void JoystickChecking() 
    {
        if(joystick.Horizontal <0)
            horizontalMove = -1;
        else if(joystick.Horizontal>0)
            horizontalMove = 1;
        else
            horizontalMove = 0;
    }

    public void JumpButton() 
    {
        if (canJump)
            jumpGetPress = true;
    }
    
    public void DodgeButton()
    {   
        if(!isDodging && feetCollider.IsTouchingLayers(ground))
            dodgeGetPress = true;
    }

    public void ParryButton()
    {
        if (!isDodging && !isAttacking && !anim.GetBool("jumping") && !anim.GetBool("falling") && feetCollider.IsTouchingLayers(ground) && !isFallingAttack && !isClimbing && !isCrouching)
            parryGetPress = true;
    }

    public void AttackButton() 
    {
        attackButton = true;
    }

    public void ClimbButton() 
    {   
        if (climbCollider.IsTouchingLayers(wall) && (anim.GetBool("jumping") || anim.GetBool("falling") || anim.GetBool("climb")))
            climbGetPress = true;
    }

    public void GrabButtonDown() 
    {
        grabButton = true;
    }

    public void GrabButtonUp() 
    {
        grabButton = false;
    }
    
    void Movement() 
    {       
        if (canMove && feetCollider.IsTouchingLayers(ground) && !fallingAttack && !isDodging)
        {   
            rB.velocity = new Vector2(speed*horizontalMove*Time.fixedDeltaTime, rB.velocity.y);
            if (joystick.Horizontal!=0 && (joystick.Horizontal < 0.5f || joystick.Horizontal >-0.5f )&& !isAttacking)
            {   
                anim.SetBool("walking", true);
                isMoving = true;
                isRunning = false;
            }
            else
            {
                anim.SetBool("walking", false);
                isMoving = false;
            }
        }
        if (horizontalMove != 0 && !isAttacking && !anim.GetBool("jumping") && !anim.GetBool("falling") && !anim.GetBool("climb") && changeSide) 
        {
            transform.localScale = new Vector3(horizontalMove, transform.localScale.y, transform.localScale.z);
        }
    }

    void Running() 
    {   
        if (joystick.Horizontal!=0 && (joystick.Horizontal > 0.5f || joystick.Horizontal <-0.5f )&&!downGetPress && feetCollider.IsTouchingLayers(ground) && !isAttacking) 
        {   
            isRunning = true;
            anim.SetBool("walking", false);
            anim.SetBool("isRunning", true);
        }
        else if ((horizontalMove == 0) || isAttacking) 
        {   
            isRunning = false;
        }
    }

    void Jumping() 
    {
        if (jumpGetPress && !anim.GetBool("crouch") && feetCollider.IsTouchingLayers(ground) && !isAttacking && !isDodging || jumpGetPress && anim.GetBool("climb") && !isAttacking && !isDodging) 
        {   
            JumpEffect.transform.localScale = new Vector3(transform.localScale.x, JumpEffect.transform.localScale.y, JumpEffect.transform.localScale.z);
            Instantiate(JumpEffect, new Vector3(transform.position.x, transform.position.y-1, transform.position.z), Quaternion.identity);
            rB.velocity = new Vector2(speed*horizontalMove*Time.fixedDeltaTime, jumpingForce*Time.fixedDeltaTime);
            anim.SetBool("jumping", true);
            jumpGetPress = false;
            isJumping = true;   
            isClimbing = false;
            GetPlay = false;
            SoundManager.instance.JumpAudio();
        }
    }

    void SwitchJumpingAnimation() 
    {
        if (anim.GetBool("jumping")) 
        {
            if (rB.velocity.y <= 0)
            {   
                isJumping = false;
                anim.SetBool("jumping", false); // hasn't use statemachine
                anim.SetBool("falling", true);  // hasn't use statemachine
                isFalling = true;
            }
            jumpGetPress = false;
        }
        else if (feetCollider.IsTouchingLayers(ground)) 
        {   
            airAttacked = false;
            anim.SetBool("falling", false); // hasn't use statemachine
            isFalling = false;
            if (GetPlay == false && !isGrabbing)
            {   
                FallEffect.transform.localScale = new Vector3(transform.localScale.x, FallEffect.transform.localScale.y, FallEffect.transform.localScale.z); 
                Instantiate(FallEffect, new Vector3(transform.position.x, transform.position.y-1, transform.position.z), Quaternion.identity);
                GetPlay = true;
                Player.instance.landingAudio.Play();
            }
        }
        if (!feetCollider.IsTouchingLayers(ground) && rB.velocity.y < 0) 
        {
            anim.SetBool("falling", true); // hasn't use statemachine
            isFalling = true;
            jumpGetPress = false;
        }
        if (anim.GetBool("falling")) 
        {
            jumpGetPress = false;
            GetPlay = false;
        }
    }

    void Attack() 
    {
        if (attackButton && !isAttacking && !anim.GetBool("crouch") && !anim.GetBool("jumping") && !anim.GetBool("falling"))
        {
            isAttacking = true;
            attackButton = false;
        }
    }

    void FallingAttack() 
    {
        if (fallingAttack) 
        {
            anim.Play("FallAttack");
            fallingAttack = false;
            isFallingAttack = true;
        }
    }

    void AirAttacking() 
    {
        if (attackButton && (anim.GetBool("jumping") || anim.GetBool("falling")) && !airAttacked)
        {   
            airAttacked = true;
            anim.Play("AirAttack");
            attackButton = false;
            SoundManager.instance.AttackAudio();
        }

    }

    public void PlayerTakeDamage(int mDamage) 
    {
        currentHP -= mDamage;
        FlashEffect(flashtime);
        getHurtAudio.Play();
        if (currentHP <= 0) 
        {
            currentHP = 0;
            Death();
        }
    }

    void Crouch() 
    {
        if (downGetPress && !anim.GetBool("jumping") && !anim.GetBool("falling") && feetCollider.IsTouchingLayers(ground) && !isFallingAttack && !isDodging) 
        {   
            rB.velocity = new Vector2(0, rB.velocity.y);
            anim.SetBool("isRunning", false);
            anim.SetBool("crouch", true);
            isCrouching = true;
            canMove = false;
            jumpGetPress = false;
            bodyCollider.enabled = false;
            crouchCollider.enabled = true;
        }
        else if (downGetRelease && anim.GetBool("crouch") && !isDodging) 
        {   
            isCrouching = false;
            canMove = true;
            anim.SetBool("crouch", false);
            downGetRelease = false;
            bodyCollider.enabled = true;
            crouchCollider.enabled = false;
        }
    }

    void Climb() 
    {   
        if (isClimbing) 
        {
            anim.SetBool("climb", true);
            anim.SetBool("falling", false);
            anim.SetBool("walking",false);
        }
        else 
        {
            anim.SetBool("climb", false);
        }
    }

    void Dodge() 
    {   
        if (dodgeGetPress && canMove) 
        {
            rB.velocity = new Vector2(dodgeSpeed*transform.localScale.x*Time.fixedDeltaTime, rB.velocity.y);
            anim.Play("Dodge");
            isDodging = true;
            dodgeGetPress = false;  
        }
    }

    void Parry() 
    {
        if (parryGetPress && canParry) 
        {
            isParrying = true;
            anim.Play("Parry1");
            parryGetPress = false;
        }
    }

    void Death() 
    {
        anim.SetTrigger("death"); 
        bodyCollider.enabled = false;
        crouchCollider.enabled = false;
        feetCollider.enabled = false;
        climbCollider.enabled = false;
        SoundManager.instance.DeathAudio();
        GetComponent<Player>().enabled = false;
    }

    void FlashEffect(float effectTime) 
    {
        sR.color = new Color(0.65f,0.65f,0.65f,1);
        Invoke("ResetColor", effectTime);
    }

    void ResetColor() 
    {
        sR.color = originalColor;
    }

    void Grab() 
    {   
        if( grabButton && greenBox && !redBox && !isGrabbing && !isClimbing)
        {
            isGrabbing = true;
            anim.Play("Grab");
            grabButton = false;
        }
    }

    void ActiveCollider() 
    {
        bodyCollider.enabled = true;
    }
    void ActiveAttackCollider1() 
    {
        AttackColliderController1.instance.ActivateAttack1();
    }

    void InactiveAttackCollider1() 
    {
        AttackColliderController1.instance.InactivateAttack1();
    }

    void ActiveAttackCollider2() 
    {
        AttackColliderController2.instance.ActivateAttack2();
    }

    void InactiveAttackCollider2() 
    {
        AttackColliderController2.instance.InactivateAttack2();
    } 

    void ActiveAttackCollider3() 
    {
        AttackColliderController3.instance.ActivateAttack3();
    }

    void InactiveAttackCollider3() 
    {
        AttackColliderController3.instance.InactivateAttack3();
    }

    void ActiveFallingAttackCollider() 
    {
        FallingAttackCollider.instance.ActivateFallingAttack();
    }

    void InactiveFallingAttackCollider() 
    {
        FallingAttackCollider.instance.InactivateFallingAttack();
    }

    void ActiveAirAttack() 
    {
        AirAttack.instance.ActivateAirAttack();
    }

    void InActiveAirAttack() 
    {
        AirAttack.instance.InActivateAirAttack();
    }

    public void ActivePlayerHitBox() 
    {
        bodyCollider.enabled = true;
    }

    public void InActivePlayerHitBox() 
    {
        bodyCollider.enabled = false;
    }
    public bool CheckInput() 
    {

        if ((joystick.Horizontal!=0 || downGetPress || jumpGetPress))
        {
            return true;
        }
        else 
        {
            return false;
        } 
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (redXOffset*transform.localScale.x), transform.position.y + redYOffset), new Vector2(redXSize, redYSize));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (greenXOffset*transform.localScale.x), transform.position.y + greenYOffset), new Vector2(greenXSize, greenYSize));
    }  
}