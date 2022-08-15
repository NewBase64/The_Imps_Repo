using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour, IDamageable
{
    #region Organized_Fields
    [Header("----Components----")]
    [SerializeField] CharacterController controller;

    [Header("----Player Attributes----")]
    [Range(1, 200)] [SerializeField] public int HP;
    [Range(1, 50)] [SerializeField] float playerSpeed;
    [Range(-10, 10)] [SerializeField] float sprintMult;
    [Range(1, 50)] [SerializeField] float jumpHeight;
    [Range(1, 100)] [SerializeField] float gravityValue;
    [Range(1, 10)] [SerializeField] int numjumps;

    [Header("----Physics----")]
    public Vector3 pushback = Vector3.zero;
    [SerializeField] int pushResolve;

    [Header("----Audio----")]
    public AudioSource audi;
    [SerializeField] AudioClip[] footsep;
    [Range(0, 1)] [SerializeField] float footstepVolume;
    bool footstepplaying = false;
    [SerializeField] AudioClip[] playerHurt;
    [Range(0, 1)] [SerializeField] float PlayerhurtVol;
    [SerializeField] AudioClip[] jumpsound;
    [Range(0, 1)] [SerializeField] float jumpVol;
    [Range(0, 1)] [SerializeField] AudioClip[] JetBoots;
    [Range(0, 1)] [SerializeField] float JetBootsVolume;
    bool JetBootsPlaying;

    [Header("----Crouch and Wall Running----")]
    public Transform orientation;

    CapsuleCollider Collider;
    [SerializeField] float minimumJumpHeight = 1.5f;
    public float slidespeed = 10f;
    [SerializeField] float distanceOfWall = 0.3f;
    [SerializeField] float RunUp = 10f;
    [SerializeField] float WallJumpForce;
    [SerializeField] float camTiltAngle;
    [SerializeField] float tiltTime;

    [SerializeField] public float wallRunSpeed;
    [SerializeField] public float Tilt { get; private set; }
    //[SerializeField] public LayerMask whatwall;
    #endregion
    #region non_serialized_values_
    float playerSpeedOrig;
    public int HPOrig;
    float lerpHp;
    Vector3 playerSpawnPosition;
    float origCapsuleHeight;
    public float reducedCapsHeight;
    bool isSliding;
    bool isSprinting = false;
    //private bool isWallRunning=false;
    bool invulnerable = false;
    [SerializeField] public bool jetpack;
    float jumpheightOrig;
    int timesJumped = 0;
    private Vector3 playerVelocity;
    Vector3 move;
    public bool slowed = false;
    public bool takingDamage = false;
    bool walljumping = true;
    public float walljumpdelay;
    private bool _wallLeft, _wallRight, _wallFront, _wallBack;
    private bool _wallLefta = true, _wallRighta = true, _wallFronta = true, _wallBacka = true;
    private RaycastHit _leftWallHit, _rightWallHit, _wallFrontHit, _wallBackHit;
    public Shield shield;
    public GameObject cam;
    float t = 0;
    [SerializeField] float lerpSpeed;
    #endregion

    void Start()
    {
        audi = AudioManager.instance.sfx;

        playerSpeedOrig = playerSpeed;
        HPOrig = HP;
        lerpHp = HP;
        playerSpawnPosition = transform.position;

        Collider = GetComponent<CapsuleCollider>();

        origCapsuleHeight = Collider.height;
        jumpheightOrig = jumpHeight;

        cam = gamemanager.instance.mainCam;
    }
    #region Player_Functions
    void Update()
    {      
        if (!gamemanager.instance.paused)
        {
            pushback = Vector3.Lerp(pushback, Vector3.zero, Time.deltaTime * pushResolve);
            movePlayer();
        }

        if (Input.GetButtonDown("Crouch"))
            Sliding();
        else if (Input.GetButtonUp("Crouch"))
            GoUp();
        CheckForWall();

        if (jetpack && !isSliding)
        {
            if (CanWallRun() && walljumping)
            {
                if (_wallLeft)                
                    StartWall();                
                else if (_wallRight)                
                    StartWall();                
                else if (_wallFront)                
                    StartWall();                
                else if (_wallBack)                
                    StartWall();                
                else                
                    StopWall();                
            }
            else           
                StopWall();
        }
        updatePlayerHp();
    }

    void StartWall()
    {
        if (_wallLeft)
        {
            Tilt = Mathf.Lerp(Tilt, -camTiltAngle, tiltTime * Time.deltaTime * 100);
            cam.transform.Rotate(0, 0, Tilt);
        }

        else if (_wallRight)
        {
            Tilt = Mathf.Lerp(Tilt, camTiltAngle, tiltTime * Time.deltaTime * 100);
            cam.transform.localRotation = Quaternion.Euler(0, 0, Tilt);
        }

        if (Input.GetButton("Jump"))
        {
            if (_wallLeft && _wallLefta)
            {
                _wallLefta = false;
                Vector3 wallJumpDirect = transform.up * RunUp + _leftWallHit.normal;
                playerVelocity = wallJumpDirect * WallJumpForce;

                audi.PlayOneShot(JetBoots[Random.Range(0, JetBoots.Length)], JetBootsVolume);
                _wallRighta = true;
                _wallFronta = true;
                _wallBacka = true;
                StartCoroutine(WallJumpDelay());
            }
            if (_wallRight && _wallRighta)
            {
                _wallRighta = false;
                Vector3 wallRunJumpDirection = transform.up * RunUp + _rightWallHit.normal;
                playerVelocity = wallRunJumpDirection * WallJumpForce;
                //-Giga I commented this out because I kept getting an error and I needed to test out my level 
                audi.PlayOneShot(JetBoots[Random.Range(0, JetBoots.Length)], JetBootsVolume);
                _wallLefta = true;
                _wallFronta = true;
                _wallBacka = true;
                StartCoroutine(WallJumpDelay());
            }
            if (_wallFront && _wallFronta)
            {
                _wallFronta = false;
                Vector3 wallRunJumpDirection = transform.up * RunUp + _wallFrontHit.normal;
                playerVelocity = wallRunJumpDirection * WallJumpForce;
                //-Giga I commented this out because I kept getting an error and I needed to test out my level 
                audi.PlayOneShot(JetBoots[Random.Range(0, JetBoots.Length)], JetBootsVolume);
                _wallRighta = true;
                _wallLefta = true;
                _wallBacka = true;
                StartCoroutine(WallJumpDelay());
            }
            if (_wallBack && _wallBacka)
            {
                _wallBacka = false;
                Vector3 wallRunJumpDirection = transform.up * RunUp + _wallBackHit.normal;
                playerVelocity = wallRunJumpDirection * WallJumpForce;
                //-Giga I commented this out because I kept getting an error and I needed to test out my level 
                audi.PlayOneShot(JetBoots[Random.Range(0, JetBoots.Length)], JetBootsVolume);
                _wallRighta = true;
                _wallFronta = true;
                _wallLefta = true;
                StartCoroutine(WallJumpDelay());
            }
        }
    }
    void StopWall()
    {
        // if player hit's thier head
        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
            playerVelocity.y = 0;
     
        if (controller.isGrounded)
        {
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }
        Tilt = Mathf.Lerp(Tilt, 0, tiltTime * Time.deltaTime);
        cam.transform.Rotate(0, 0, Tilt);
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
        if (!controller.isGrounded)
        {
            // Add gravity
            playerVelocity.y -= gravityValue * Time.deltaTime;
            controller.Move((playerVelocity + pushback) * Time.deltaTime);
        }

        // Change the height position of the player
        if (Input.GetButtonDown("Jump") && (timesJumped < numjumps))
        {
            if (timesJumped == 0) // players first jump
               audi.PlayOneShot(jumpsound[Random.Range(0, jumpsound.Length)], jumpVol); 

            playerVelocity.y = 0;            // turn off gravity
            playerVelocity.y += jumpHeight;  // jump
            timesJumped++;                   // increment times jumped
        }

        // If the player jumps and hit's their head, they will bounce off the surface
        if ((controller.collisionFlags & CollisionFlags.Above) != 0)        
            playerVelocity.y = 0;
        
        // If the player touches the ground, reset teh velocity and jump counter
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;         // turn off gravity
            timesJumped = 0;               // reset number of jumps
            jumpHeight = jumpheightOrig;   // reset jump height
        }

        // JETPACK
        if (jetpack)
        {
            numjumps = 2;
            //if (timesJumped == 2)           
            //    jumpHeight = jumpHeight * 2;           
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
        if ((health + HP) > HPOrig)        
            HP = HPOrig;        
        else        
            HP += health;
        updatePlayerHp();
    }

    public void respawn()
    {
        HP = HPOrig;
        controller.enabled = false;
        transform.position = RoomManager.instance.checkpoint.transform.position;
        controller.enabled = true;
        updatePlayerHp();
        gamemanager.instance.weaponHandler.GiveAmmo(0);
        gamemanager.instance.updateAmmoCount();
        pushback = Vector3.zero;
    }

    public void Teleport()
    {
        controller.enabled = false;
        transform.position = playerSpawnPosition;
        controller.enabled = true;
        pushback = Vector3.zero;
    }

    public void takeDamage(int dmg)
    {
        if (shield != null && shield.isActive)
        {
            audi.PlayOneShot(AudioManager.instance.shieldDink);
            shield.takeDamage(dmg);
        }
        else
        {
            HP -= dmg;
            audi.PlayOneShot(playerHurt[Random.Range(0, playerHurt.Length)], PlayerhurtVol);
            //updatePlayerHp();
            t = 0;
            StartCoroutine(damageFlash());
        }
        if (HP <= 0)       
            gamemanager.instance.playerDead();        
    }
    public void updatePlayerHp()
    {
        if (lerpHp != HP)
        {
            lerpHp = Mathf.Lerp(lerpHp, HP, t);
            t += lerpSpeed * Time.deltaTime;
        }

        gamemanager.instance.HPBar.fillAmount = lerpHp / HPOrig;
        //gamemanager.instance.HPBar.fillAmount = (float)HP / (float)HPOrig;
    }

    public void updateShieldHp()
    {
        if(shield.shieldLerpHp != shield.shieldCurrentHp)
        {
            shield.shieldLerpHp = Mathf.Lerp(shield.shieldLerpHp, shield.shieldCurrentHp, shield.t);
            shield.t += shield.lerpSpeed * Time.deltaTime;
        }
        gamemanager.instance.ShieldBar.fillAmount = shield.shieldLerpHp / shield.shieldHp;
        //gamemanager.instance.ShieldBar.fillAmount = (float)shield.shieldCurrentHp / (float)shield.shieldHp;
    }
    private void Sliding()
    {
        isSliding = true;
        Collider.height = Mathf.Lerp(Collider.height, reducedCapsHeight, 2);
        controller.height = Mathf.Lerp(controller.height, reducedCapsHeight, 2);
        playerSpeed = playerSpeedOrig / 2;

        gamemanager.instance.cameraScript.crouch();
    }

    private void GoUp()
    {
        isSliding = false;
        playerSpeed = playerSpeedOrig;
        Collider.height = Mathf.Lerp(Collider.height, origCapsuleHeight, 1);
        controller.height = Mathf.Lerp(-controller.height, origCapsuleHeight, 1);
        gamemanager.instance.cameraScript.goUP();
    }

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }
    void CheckForWall()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), orientation.forward);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), -orientation.forward);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), orientation.right);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), -orientation.right);
        _wallLeft = Physics.Raycast(transform.position + new Vector3(0, 1, 0), -orientation.right, out _leftWallHit, distanceOfWall);
        _wallRight = Physics.Raycast(transform.position + new Vector3(0, 1, 0), orientation.right, out _rightWallHit, distanceOfWall);
        _wallFront = Physics.Raycast(transform.position + new Vector3(0, 1, 0), orientation.forward, out _wallFrontHit, distanceOfWall);
        _wallBack = Physics.Raycast(transform.position + new Vector3(0, 1, 0), -orientation.forward, out _wallBackHit, distanceOfWall);
    }
    #endregion

    #region IEnums
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
            else
                yield return new WaitForSeconds(0.4f);
            footstepplaying = false;
        }
    }

    IEnumerator WallJumpDelay()
    {
        walljumping = false;
        yield return new WaitForSeconds(walljumpdelay);
        walljumping = true;
    }
    #endregion
}