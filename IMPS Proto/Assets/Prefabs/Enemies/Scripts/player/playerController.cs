using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour, IDamageable
{
    #region Organized_Fields
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

    [Header("----Crouch and Wall Running----")]
    public Transform orientation;
    [SerializeField] Rigidbody rigid;
    CapsuleCollider Collider;
    [SerializeField] float minimumJumpHeight = 1.5f;
    [SerializeField] float slidespeed = 10f;
    [SerializeField] float distanceOfWall = 0.3f;
    [SerializeField] float RunUp = 10f;
    [SerializeField] float WallJumpForce;
    [SerializeField] float camTiltAngle;
    [SerializeField] float tiltTime;

    [SerializeField] public float wallRunSpeed;
    [SerializeField] public float Tilt { get; private set; }
    [SerializeField] public LayerMask whatwall;





    #endregion





    #region non_serialized_values_
    float playerSpeedOrig;
    public int HPOrig;
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
    private bool _wallLeft, _wallRight, _wallFront,_wallBack;

    private RaycastHit _leftWallHit, _rightWallHit, _wallFrontHit, _wallBackHit;
    public Shield shield;
    public GameObject cam;






    #endregion




    void Start()
    {
        playerSpeedOrig = playerSpeed;
        HPOrig = HP;
        playerSpawnPosition = transform.position;

        Collider = GetComponent<CapsuleCollider>();

        origCapsuleHeight = Collider.height;
        jumpheightOrig = jumpHeight;
        rigid = GetComponent<Rigidbody>();

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

        if (jetpack) { 
        if (CanWallRun())
        {

            if (_wallLeft)
            {
                //Debug.Log(_wallLeft);
                StartWall();
            }
            else if (_wallRight)
            {
                //Debug.Log(_wallRight);
                StartWall();

            }
            else if(_wallFront)
            {
                StartWall();

            }
            else if(_wallBack)
            {
                StartWall();

            }
            else
            {

                StopWall();

            }
        }
        else
        {
            StopWall();
        }

}
    }

    void StartWall()
    {
        //Debug.Log("StartWall hit");
        rigid.AddForce(orientation.forward * wallRunSpeed, ForceMode.Acceleration);
        if (_wallLeft)
        {

            Tilt = Mathf.Lerp(Tilt, -camTiltAngle, tiltTime * Time.deltaTime *100);
            cam.transform.Rotate(0, 0, Tilt);
            //Debug.Log(Tilt);
            //Debug.Log(cam.transform.rotation);
        }

        else if (_wallRight)
        {
            Tilt = Mathf.Lerp(Tilt, camTiltAngle, tiltTime * Time.deltaTime *100);
            cam.transform.localRotation = Quaternion.Euler(0, 0, Tilt);
            //Debug.Log(Tilt);
            //Debug.Log(cam.transform.rotation);
        }

        if (Input.GetButton("Jump"))
        {
            if (_wallLeft)
            {
                Vector3 wallJumpDirect = transform.up * RunUp + _leftWallHit.normal;
                //rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
                //rigid.AddForce(wallJumpDirect * WallJumpForce * 70, ForceMode.Force);
                playerVelocity = wallJumpDirect * WallJumpForce;
               

            }
            if (_wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up * RunUp + _rightWallHit.normal;
                //rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
                //rigid.AddForce(wallRunJumpDirection * WallJumpForce * 70, ForceMode.Force);
                playerVelocity = wallRunJumpDirection * WallJumpForce;
                
            }
            if (_wallFront)
            {
                Vector3 wallRunJumpDirection =transform.up * RunUp + _wallFrontHit.normal;
                playerVelocity=wallRunJumpDirection * WallJumpForce;


            }
            if (_wallBack)
            {
                Vector3 wallRunJumpDirection = transform.up * RunUp + _wallBackHit.normal;
                playerVelocity = wallRunJumpDirection * WallJumpForce;


            }
        }
    }
    void StopWall()
    {
        if (controller.isGrounded)
        {
            playerVelocity.x = 0;
            playerVelocity.z= 0;

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
        // Add gravity
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move((playerVelocity + pushback) * Time.deltaTime);

        // Change the height position of the player
        if (Input.GetButtonDown("Jump") && (timesJumped < numjumps))
        {
            if (timesJumped == 0)
            { audi.PlayOneShot(jumpsound[Random.Range(0, jumpsound.Length)], jumpVol); }

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
            jumpHeight = jumpheightOrig;
        }
        if(jetpack)
        {
            numjumps = 2;
            if(timesJumped==2)
            {
                jumpHeight = jumpHeight *2;


            }



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
        if (shield != null && shield.isActive)
        {
            shield.takeDamage(dmg);
        }
        else
        {
            HP -= dmg;
            audi.PlayOneShot(playerHurt[Random.Range(0, playerHurt.Length)], PlayerhurtVol);
            updatePlayerHp();
            StartCoroutine(damageFlash());
        }
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
    private void Sliding()
    {
        Collider.height = reducedCapsHeight;
        controller.height = reducedCapsHeight;
        playerSpeed = playerSpeedOrig / 2;
        gamemanager.instance.cameraScript.crouch();
        rigid.AddForce(transform.forward * slidespeed, ForceMode.VelocityChange);
    }

    private void GoUp()
    {
        playerSpeed = playerSpeedOrig;
        Collider.height = origCapsuleHeight;
        controller.height = origCapsuleHeight;
        gamemanager.instance.cameraScript.goUP();
    }

    bool CanWallRun()
    {
        //Debug.Log("CanWall");
        return !Physics.Raycast(transform.position , Vector3.down, minimumJumpHeight);
    }
    void CheckForWall()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), orientation.forward);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), -orientation.forward);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), orientation.right);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), -orientation.right);


        _wallLeft = Physics.Raycast(transform.position+ new Vector3(0,1,0), -orientation.right, out _leftWallHit, distanceOfWall, whatwall);
        _wallRight = Physics.Raycast(transform.position + new Vector3(0, 1, 0), orientation.right, out _rightWallHit, distanceOfWall, whatwall);
        _wallFront = Physics.Raycast(transform.position + new Vector3(0, 1, 0), orientation.forward, out _wallFrontHit, distanceOfWall, whatwall);
        _wallBack = Physics.Raycast(transform.position + new Vector3(0, 1, 0), -orientation.forward, out _wallBackHit, distanceOfWall, whatwall);
        
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
    #endregion
}