using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FMOD.Studio;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    [Range(0.01f, 100.0f)]
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    [Header("Object Sounds")]

    [FMODUnity.EventRef]
    public string HurtEvent;
    [FMODUnity.EventRef]
    public string DeathEvent;    
    [FMODUnity.EventRef]
    public string HeartbeatEvent;

    private const string DIE_TRIGGER = "Die";
    Animator anim;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    private FMOD.Studio.EventInstance heartBeatEvent;
    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        heartBeatEvent = FMODUnity.RuntimeManager.CreateInstance(HeartbeatEvent);
        heartBeatEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, playerMovement.GetRigidbody()));
        heartBeatEvent.start();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        heartBeatEvent.setParameterValue("health", currentHealth / (float)startingHealth);
        heartBeatEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, playerMovement.GetRigidbody()));
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;


        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
        else
        {
            var hurtEvent = FMODUnity.RuntimeManager.CreateInstance(HurtEvent);
            hurtEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, playerMovement.GetRigidbody()));
            hurtEvent.start();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger(DIE_TRIGGER);
        var deathEvent = FMODUnity.RuntimeManager.CreateInstance(DeathEvent);
        deathEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, playerMovement.GetRigidbody()));
        deathEvent.start();
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }

    void OnDestroy()
    {
        heartBeatEvent.stop(STOP_MODE.IMMEDIATE);
    }
}
