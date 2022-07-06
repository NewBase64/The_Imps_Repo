using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour, IDamagable
{
    [Header("Components")]
    [SerializeField] CharacterController controller;

    [Header("-----------------")]
    [Header("Player Attributes")]
    [Range(1, 20)][SerializeField] int HP;
    [Range(3, 15)][SerializeField] float playerSpeed;
    [Range(.5f, 10)][SerializeField] int sprintMult;
    [Range(5, 20)][SerializeField] float jumpHeight;
    [Range(5, 40)][SerializeField] float gravityValue;
    [Range(1, 3)][SerializeField] int jumps;

    [Header("-------------------")]
    [Header("Player Weapon Stats")]
    [Range(0.0001f, 3)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int weaponDamage;

    [Header("-------")]
    [Header("Effects")]
    [SerializeField] GameObject hitEffectSpark;
    [SerializeField] GameObject muzzleFlash;

    bool isSprinting = false;
    float playerSpeedOrig;
    int HPOrig;
    int timesJumped;
    private Vector3 playerVelocity;
    Vector3 move;
    bool canShoot = true;
    Vector3 PlayerSpawnPosition;

    private void Start()
    {
        playerSpeedOrig = playerSpeed;
        HPOrig = HP;
        PlayerSpawnPosition = transform.position;
    }

    void Update()
    {
        if (!gamemanager.instance.paused)
        {
            movePlayer();
            sprint();
            StartCoroutine(shoot());
        }
    }

    private void movePlayer()
    {
        if((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            playerVelocity.y -= jumpHeight;
        }
        
        // if we land reset the player velocity and the jump counter
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }

        // get the input form unity's input system
        move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));

        // add our move vector inot the character controller move
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && (timesJumped < jumps))
        {
            playerVelocity.y += jumpHeight;
            timesJumped++;
        }

        // add Gravity
        playerVelocity.y -= gravityValue * Time.deltaTime;

        // Add gravity back into the character controller
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void sprint()
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

    IEnumerator shoot()
    {
        if (Input.GetButton("Shoot") && canShoot)
        {
            canShoot = false;

            RaycastHit hit;


            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                Instantiate(hitEffectSpark, hit.point, hitEffectSpark.transform.rotation);

                if (hit.collider.GetComponent<IDamagable>() != null)
                {
                    IDamagable isDamageable = hit.collider.GetComponent<IDamagable>();
                    if(hit.collider is SphereCollider)
                        isDamageable.takeDamage(weaponDamage * 20);
                    else
                        isDamageable.takeDamage(weaponDamage);

                }
            }

            muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            muzzleFlash.SetActive(false);

            yield return new WaitForSeconds(shootRate);
            canShoot = true;
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        updatePlayerHP();

        StartCoroutine(damageflash());

        if (HP <= 0)
        {
            gamemanager.instance.playerDead();
        }
    }

    IEnumerator damageflash()
    {
        gamemanager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gamemanager.instance.playerDamageFlash.SetActive(false);
    }

    public void giveHP(int amount)
    {
        if(HP < HPOrig)
            HP += amount;

        if(HP > HPOrig)
            HP = HPOrig;

        updatePlayerHP();
    }

    void updatePlayerHP()
    {
        gamemanager.instance.HPBar.fillAmount = (float)HP / (float)HPOrig;
    }

    public void respawn()
    {
        HP = HPOrig;
        updatePlayerHP();
        controller.enabled = false;
        transform.position = PlayerSpawnPosition;
        controller.enabled = true;
    }
}