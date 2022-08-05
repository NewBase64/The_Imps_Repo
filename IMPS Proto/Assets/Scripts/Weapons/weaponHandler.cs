using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHandler : MonoBehaviour
{
    [Header("----WepStats----")]
    [SerializeField] public int ammo;
    [SerializeField] public int ammoReserve;
    [SerializeField] public int magCap;
    [SerializeField] int ammoMax;
    [SerializeField] public int damage;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    [SerializeField] float shootType;
    [SerializeField] bool semiTautoF;
    [Header("----Dev----")]
    [SerializeField] public bool InfiniteAmmo;
    [SerializeField] public bool bottomlessClip;
    [Header("----Weapon----")]
    [SerializeField] GameObject noModel;
    [SerializeField] GameObject model;
    [SerializeField] GameObject wepEffect;
    //[SerializeField] GameObject wepFlash;    
    [SerializeField] GameObject wepSpawn;
    [Header("----Crosshairs----")]
    [SerializeField] public GameObject CurrCrosshair;
    [SerializeField] GameObject NoCrosshair;
    [SerializeField] GameObject Crosshair1;
    [SerializeField] GameObject Crosshair2;
    [SerializeField] GameObject Crosshair3;
    [HideInInspector] public bool catchMe = false;
    //[Header("----weapon vars----")]
    [HideInInspector] public weapon primary;
    [HideInInspector] public weapon secondary;
    weapon tempHolder;
    [HideInInspector] public weapon pubHolder;
    int primammo;
    int primammoRes;
    [HideInInspector] public int secammo;
    [HideInInspector] public int secammoRes;
    int holdammo;
    int holdammoRes;
    [HideInInspector] public int pubammo;
    [HideInInspector] public int pubammoRes;
    //bool prim;
    [Header("----Audio----")]
    public AudioSource audi;
    [SerializeField] AudioClip gunshot;
    [Range(0, 1)][SerializeField] float gunShotVol;
    [SerializeField] AudioClip[] outofAmmo;
    [Range(0, 1)][SerializeField] float outofammoVol;
    bool oOASound = false;
    [SerializeField] AudioClip[] reloadSound;
    [Range(0, 1)][SerializeField] float reloadVol;
    [Header("----Weapon Drop stuff----")]
    [SerializeField] GameObject wepPickup;

    bool canShoot = true;
    bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set defaults so no null references
        model = noModel;
        CurrCrosshair = NoCrosshair;

    }

    // Update is called once per frame
    void Update()
    {
        if (!gamemanager.instance.menuCurrentlyOpen) // if there is not a menu open
        {
            // shoot
            Shooting();

            //gamemanager.instance.updateAmmoCount(); // updates the UI for the ammo

            // Reload
            if (Input.GetButtonDown("Reload") && !reloading)
            {
                audi.PlayOneShot(reloadSound[Random.Range(0, reloadSound.Length)], reloadVol);
                StartCoroutine(Reload());
            }
            // switch weapon
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                SwitchWeapon();
                updateGunStats();
            }
            aim(); // aim
        }
    }

    void Shooting()
    {
        if (semiTautoF)                                  // if the weapon is semi auto
        {                                                //
            if (Input.GetMouseButtonDown(0) && canShoot) // shoot once
            {
                StartCoroutine(Shoot());
            }
        }
        else                                         // if the weapon is full auto
        {                                            //
            if (Input.GetMouseButton(0) && canShoot) // shoot while held
            {
                StartCoroutine(Shoot());
            }
        }
    }

    void aim()
    {
        if (Input.GetMouseButtonDown(1)) // pressing right mouse
        {
            //switch (GUN)
            //{
            //    case 1:
            //        pistol.transform.localPosition = pistolAimPos;
            //        break;
            //    case 2:
            //        rifle.transform.localPosition = rifleAimPos;
            //        break;
            //}
            gamemanager.instance.cameraScript.fov = 45;
            //wepFlash.transform.Translate(wepBarrel.transform.position);
        }
        else if (Input.GetMouseButtonUp(1)) // releasing right mouse
        {
            //switch (GUN)
            //{
            //    case 1:
            //        pistol.transform.localPosition = pistolStartPos;
            //        break;
            //    case 2:
            //        rifle.transform.localPosition = rifleStartPos;
            //        break;
            //}
            gamemanager.instance.cameraScript.fov = 90;
            //wepFlash.transform.Translate(wepBarrel.transform.position);
        }
    }

    IEnumerator Shoot()
    {
        if (ammo > 0 && !reloading) // if the player has ammo and is not currently reloading
        {
            canShoot = false; // don't let this code get called untill the weapon can shoot again
            if (!bottomlessClip) // Do not decrement ammo if bottomless clip
            {
                ammo--;
                gamemanager.instance.updateAmmoCount();
            }
            // play gunshot          
            audi.PlayOneShot(gunshot, gunShotVol);

            RaycastHit hit;                                                                          //
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))//
            {                                                                                        //
                Instantiate(wepEffect, hit.point, wepEffect.transform.rotation);                     //
                                                                                                     //
                if (hit.collider.GetComponent<IDamageable>() != null)                                //
                {                                                                                    //
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();               //  Hitscan weapons
                    if (hit.collider is SphereCollider)                                              //
                        damageable.takeDamage(damage * 100);                                         //
                    else                                                                             //
                        damageable.takeDamage(damage);                                               //
                }                                                                                    //
            }                                                                                        //

            //StartCoroutine(Flash());

            yield return new WaitForSeconds(fireRate); // wait
            canShoot = true; // allow the weapon to shoot agian
        }
        else if (primary != null) // if the player has a weapon
        {
            if (!oOASound) // if the sound is not playing already
            {              // play the sound
                oOASound = true;
                audi.PlayOneShot(outofAmmo[Random.Range(0, outofAmmo.Length)], outofammoVol);
                yield return new WaitForSeconds(fireRate);
                oOASound = false;
            }
        }
    }

    //IEnumerator Flash()
    //{
    //    wepFlash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
    //    wepFlash.SetActive(true);
    //    yield return new WaitForSeconds(0.05f);
    //    wepFlash.SetActive(false);
    //}

    IEnumerator Reload()
    {
        canShoot = false;
        reloading = true;
        ammoReserve += ammo;
        yield return new WaitForSeconds(reloadTime);
        if (InfiniteAmmo)
        {
            ammo = magCap;
        }
        else if (ammoReserve >= magCap)
        {
            ammoReserve -= magCap;
            ammo = magCap;
        }
        else if (ammoReserve < magCap)
        {
            ammo = ammoReserve;
            ammoReserve = 0;
        }

        canShoot = true;
        reloading = false;
        gamemanager.instance.updateAmmoCount();
    }

    public int GiveAmmo(int primsec, int addammo = 0)
    {
        // If not adding a specific amount of ammo
        if (addammo == 0)
        {            
            if (primsec == 1) // If adding to the primary weapon
            {
                if ((magCap * 2 + ammoReserve) > ammoMax)                
                    ammoReserve = ammoMax;                
                else                
                    ammoReserve += magCap * 2;               
            }
            else if (primsec == 2) // If adding to the secondary weapon
            {
                if ((magCap * 2 + secammoRes) > secondary.ammoMax)                
                    secammoRes = secondary.ammoMax;               
                else               
                    secammoRes += magCap * 2;                
            }
            else // Add to both
            {
                if ((magCap * 2 + ammoReserve) > ammoMax)                
                    ammoReserve = ammoMax;                
                else                
                    ammoReserve += magCap * 2;
                
                if ((magCap * 2 + secammoRes) > secondary.ammoMax)                
                    secammoRes = secondary.ammoMax;                
                else                
                    secammoRes += magCap * 2;                
            }
            return 0;
        }
        else // If adding a specific amount of ammo
        {
            if (primsec == 1) // If giving to the primary weapon
            {
                if ((addammo + ammoReserve) > ammoMax) // If ammo overflow
                {
                    int ammoback = ammoMax - ammoReserve; // get the amount of ammo needed to fill up 
                    ammoReserve = ammoMax; // set ammo to max
                    return ammoback; // tell the caller how much I took
                }
                else
                {
                    ammoReserve += addammo; // add ammo to primary
                    return 0;
                }
            }
            else // Giving to the secondary
            {
                if ((addammo + secammoRes) > secondary.ammoMax)
                {
                    int ammoback = secondary.ammoMax - secammoRes;
                    secammoRes = secondary.ammoMax;
                    return ammoback;
                }
                else
                {
                    secammoRes += addammo;
                    return 0;
                }
            }
        }
    }

    public void FillAmmo()
    {
        ammoReserve = ammoMax;
        secammoRes = secondary.ammoMax;
    }

    public void unArm()
    {
        primary = null;
        secondary = null;
        ChangeCrosshair();
    }

    void SwitchWeapon()
    {
        if (primary != null && secondary != null)
        {
            tempHolder = primary;
            holdammo = ammo;
            holdammoRes = ammoReserve;
            primary = secondary;
            ammo = secammo;
            ammoReserve = secammoRes;
            secondary = tempHolder;
            secammo = holdammo;
            secammoRes = holdammoRes;
            tempHolder = null;

        }
    }

    public void AddGun(weapon stats, int addammo, int addammoRes)
    {
        if (primary == null)
        {
            primary = stats;
            ammo = addammo;
            ammoReserve = addammoRes;
        }
        else if (secondary == null)
        {
            secondary = stats;
            secammo = addammo;
            secammoRes = addammoRes;
        }
        else
        {
            if (ammo != 0 || ammoReserve != 0)
            {
                catchMe = true;
                Instantiate(wepPickup, gamemanager.instance.mainCam.transform.position, gamemanager.instance.mainCam.transform.rotation);
                pubHolder = primary;
                pubammo = ammo;
                pubammoRes = ammoReserve;
            }
            primary = stats;
            ammo = addammo;
            ammoReserve = addammoRes;
        }
        updateGunStats();
    }

    public void updateGunStats()
    {
        if (InfiniteAmmo || bottomlessClip)
        {
            ammoMax = primary.ammoMax;
            ammoReserve = primary.ammoMax;
            ammo = primary.magCap;
            magCap = primary.magCap;
        }
        else
        {
            ammoMax = primary.ammoMax;
            magCap = primary.magCap;
        }
        damage = primary.damage;
        fireRate = primary.fireRate;
        reloadTime = primary.reloadTime;
        semiTautoF = primary.semiTautoF;
        shootType = primary.shootType;
        model = primary.model;
        wepEffect = primary.wepEffect;
        gunshot = primary.gunshot;
        gunShotVol = primary.gunShotVol;
        reloadSound = primary.reloadSound;
        reloadVol = primary.reloadVol;
        outofAmmo = primary.outofAmmo;
        outofammoVol = primary.outofammoVol;
        ChangeCrosshair();
        gamemanager.instance.updateAmmoCount();
    }

    public void ChangeCrosshair()
    {
        if (primary != null)
        {
            switch (primary.Crosshair)
            {
                case 0:
                    CurrCrosshair.SetActive(false);
                    CurrCrosshair = NoCrosshair;
                    CurrCrosshair.SetActive(true);
                    break;
                case 1:
                    CurrCrosshair.SetActive(false);
                    CurrCrosshair = Crosshair1;
                    CurrCrosshair.SetActive(true);
                    break;
                case 2:
                    CurrCrosshair.SetActive(false);
                    CurrCrosshair = Crosshair2;
                    CurrCrosshair.SetActive(true);
                    break;
                case 3:
                    CurrCrosshair.SetActive(false);
                    CurrCrosshair = Crosshair3;
                    CurrCrosshair.SetActive(true);
                    break;
            }
        }
        else
        {
            CurrCrosshair.SetActive(false);
            CurrCrosshair = NoCrosshair;
            CurrCrosshair.SetActive(true);
        }
    }
}