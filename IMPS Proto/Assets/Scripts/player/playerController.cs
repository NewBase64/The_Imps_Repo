using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour, IDamageable
{
    [Header("----Components----")]
    [SerializeField] CharacterController controller;

    [Header("----Player Attributes----")]
    [Range(1, 200)][SerializeField] public int HP;
    [Range(1, 50)][SerializeField] float playerSpeed;
    [Range(-10, 10)][SerializeField] float sprintMult;
    [Range(1, 50)][SerializeField] float jumpHeight;
    [Range(1, 100)][SerializeField] float gravityValue;
    [Range(1, 10)][SerializeField] int numjumps;

    [Header("----Physics----")]
    public Vector3 pushback = Vector3.zero;
    [SerializeField] int pushResolve;

    [Header("----Audio----")]
    public AudioSource audi;
    [SerializeField] AudioClip[] footsep;
    [Range(0, 1)][SerializeField] float footstepVolume;
    bool footstepplaying = false;
    [SerializeField] AudioClip[] playerHurt;
    [Range(0, 1)][SerializeField] float PlayerhurtVol;
    [SerializeField] AudioClip[] jumpsound;
    [Range(0, 1)][SerializeField] float jumpVol;
   
    //movement vars
    float playerSpeedOrig;
    [HideInInspector] public int HPOrig;
    [HideInInspector] public bool jetpack;
    Vector3 playerSpawnPosition;
    int timesJumped = 0;
    private Vector3 playerVelocity;
    Vector3 move;

    //bools
    public Shield shield;
    bool isSprinting = false;
    public bool slowed = false;
    bool invulnerable = false;
    public bool takingDamage = false;

    // weapon pickup vars
    [SerializeField] bool pickingUp;
    [SerializeField] weapon stats;
    [SerializeField] GameObject obj;

    void Start()
    {
        playerSpeedOrig = playerSpeed;
        HPOrig = HP;
        playerSpawnPosition = transform.position;
    }

    void Update()
    {
        if (!gamemanager.instance.paused)
        {
            pushback = Vector3.Lerp(pushback, Vector3.zero, Time.deltaTime * pushResolve);

            movePlayer();
        }
    }
    
    private void movePlayer()
    {
        // Get the inputs from unity's input system
        move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));

        // Add our move vector into the character controller move
        controller.Move(move * playerSpeed * Time.deltaTime);

        // play footstep audio
        StartCoroutine(playFootsteps());

        // Jump if needed
        Jump();

        // Sprint if needed
        Sprint();
        pushback = Vector3.zero;

    }
    void Jump()
    {
        // Add gravity
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move((playerVelocity + pushback) * Time.deltaTime);

        // Change the height position of the player
        if (Input.GetButtonDown("Jump") && (timesJumped < numjumps))
        {
            audi.PlayOneShot(jumpsound[Random.Range(0, jumpsound.Length)], jumpVol);
            playerVelocity.y += jumpHeight;
            timesJumped++;
        }

        // If the player jumps and hit's their head, they will bounce off the surface
        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            playerVelocity.y -= jumpHeight;
        }

        // If the player touches the ground, reset teh velocity and jump counter
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }
    }
    void Sprint()
    {
        // Change move speed while sprinting
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            playerSpeed = playerSpeed * sprintMult;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            playerSpeed = playerSpeedOrig;
        }
    }

    public void Invulnerable()
    {
        if (invulnerable)
            invulnerable = false;
        else
            invulnerable = true;
    }

    public void GiveHP(int health)
    {
        if((health + HP) > HPOrig)
        {
            HP = HPOrig;
        }
        else
        {
            HP += health;
        }

        updatePlayerHp();
    }

    public void respawn()
    {
        HP = HPOrig;
        controller.enabled = false;
        transform.position = playerSpawnPosition;
        controller.enabled = true;
        updatePlayerHp();
        pushback = Vector3.zero;
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        audi.PlayOneShot(playerHurt[Random.Range(0, playerHurt.Length)], PlayerhurtVol);
        updatePlayerHp();
        StartCoroutine(damageFlash());
        if (HP <= 0)
        {
            gamemanager.instance.playerDead();
        }
    }
    public void updatePlayerHp()
    {
        gamemanager.instance.HPBar.fillAmount = (float)HP / (float)HPOrig;
    }

    public void updateShieldHp()
    {
        gamemanager.instance.ShieldBar.fillAmount = (float)shield.shieldCurrentHp / (float)shield.shieldHp;
    }

    IEnumerator damageFlash()
    {
        gamemanager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gamemanager.instance.playerDamageFlash.SetActive(false);
    }

    // Slows the player for a certain amount time by a certain percentage
    public IEnumerator slowPlayer(int time, float slowMultiplier = 0)
    {
        float playerSpeedOrig2 = playerSpeedOrig;

        playerSpeedOrig *= slowMultiplier;
        if (isSprinting)
        {
            playerSpeed *= slowMultiplier;
        }
        else
        {
            playerSpeed = playerSpeedOrig;
        }
        slowed = true;

        yield return new WaitForSeconds(time);

        playerSpeedOrig = playerSpeedOrig2;
        playerSpeed = playerSpeedOrig;
        slowed = false;

    }

    IEnumerator playFootsteps()
    {
        if (controller.isGrounded && move.normalized.magnitude > 0 && !footstepplaying)
        {
            footstepplaying = true;
            audi.PlayOneShot(footsep[Random.Range(0, footsep.Length)], footstepVolume);

            if (isSprinting)
                yield return new WaitForSeconds(0.3f);
            else if (slowed)
                yield return new WaitForSeconds(0.5f);
            else
                yield return new WaitForSeconds(0.4f);
            footstepplaying = false;
        }
    }
}
