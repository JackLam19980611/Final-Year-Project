using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    public static SoundManager instance;
    public AudioSource aS;
    public AudioClip attackAudio, deathAudio, jumpAudio, dodgeAudio, parryAudio, fallAttackAudio, sheathSwordAudio, drawSwordAudio;
    // Start is called before the first frame update
    
    void Awake() 
    {
        instance = this;
    }
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackAudio() 
    {   
        aS.clip = attackAudio;
        aS.Play();
    }

    public void DeathAudio() 
    {
        aS.clip = deathAudio;
        aS.Play();
    }

    public void JumpAudio() 
    {
        aS.clip = jumpAudio;
        aS.Play();
    }

    public void ParryAudio()
    {
        aS.clip = parryAudio;
        aS.Play();
    }

    public void DodgeAudio() 
    {
        aS.clip = dodgeAudio;
        aS.Play();
    }

    public void FallAttackAudio()
    {
        aS.clip = fallAttackAudio;
        aS.Play();
    }

    public void SheathSwordAudio()
    {
        aS.clip = sheathSwordAudio;
        aS.Play();
    }   

    public void DrawSwordAudio() 
    {
        aS.clip = drawSwordAudio;
        aS.Play();
    }
}
