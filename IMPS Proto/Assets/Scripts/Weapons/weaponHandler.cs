using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHandler : MonoBehaviour
{
    [Header("----WepStats----")]
    [SerializeField] int ammoMax;
    [SerializeField] public int ammoReserve;
    [SerializeField] public int ammo;
    [SerializeField] int magCap;
    [SerializeField] public int damage;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    [SerializeField] bool semiTautoF;
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
    [Header("----Weapon Drop stuff----")]
    [SerializeField] GameObject wepPickup;
    [SerializeField] GameObject cam;
    public bool catchMe = false;
    //[Header("----weapon vars----")]
    public weapon primary;
    public weapon secondary;
    public weapon tempHolder;
    //bool prim;
    [Header("----Audio----")]
    public AudioSource audi;
    [SerializeField] AudioClip[] gunshot;
    [Range(0, 1)][SerializeField] float gunShotVol;
    bool lazer = false;
    [SerializeField] AudioClip[] lazershot;
    [Range(0, 1)][SerializeField] float lazerShotVol;
    [SerializeField] AudioClip[] outofAmmo;
    [Range(0, 1)][SerializeField] float outofammoVol;
    bool oOASound = false;
    [SerializeField] AudioClip[] reloadSound;
    [Range(0, 1)][SerializeField] float reloadVol;
    //[Header("----Testing----")]
    //public bool Armed;
    [SerializeField] public bool bottomlessClip;

    bool canShoot = true;
    bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set defaults so no null references
        model = noModel;
        CurrCrosshair = NoCrosshair;
        //prim = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamemanager.instance.menuCurrentlyOpen)
        {
            if (semiTautoF)
            {
                if (Input.GetMouseButtonDown(0) && canShoot)
                {
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                if (Input.GetMouseButton(0) && canShoot)
                {
                    StartCoroutine(Shoot());
                }
            }
            if (Input.GetButtonDown("Reload") && !reloading)
            {
                audi.PlayOneShot(reloadSound[Random.Range(0, reloadSound.Length)], reloadVol);
                StartCoroutine(Reload());
            }
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                SwitchWeapon();
                updateGunStats();
            }
            aim();
        }
    }

    void SwitchWeapon()
    {
        if (secondary != null)
        {
            tempHolder = primary;
            primary = secondary;
            secondary = tempHolder;
            tempHolder = null;
        }
    }

    public void unArm()
    {
        //canShoot = false;
        primary = null;
        secondary = null;
        primary.ammoMax = 0;
        primary.ammo = 0;
        primary.MagCap = 0;
        primary.damage = 0;
        primary.fireRate = 0;
        primary.reloadTime = 0;
        primary.model = noModel;
        primary.Crosshair = 0;
        ChangeCrosshair();
    }

    void aim()
    {
        if (Input.GetMouseButtonDown(1))
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
        else if (Input.GetMouseButtonUp(1))
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
        if (ammo > 0 && !reloading)
        {
            canShoot = false;
            if (!bottomlessClip)
            {
                ammo--;
                primary.ammo = ammo;
                //updateammo();
            }
            if (lazer)
                audi.PlayOneShot(lazershot[Random.Range(0, lazershot.Length)], lazerShotVol);
            else
                audi.PlayOneShot(gunshot[Random.Range(0, gunshot.Length)], gunShotVol);

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                Instantiate(wepEffect, hit.point, wepEffect.transform.rotation);

                if (hit.collider.GetComponent<IDamageable>() != null)
                {
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                    if (hit.collider is SphereCollider)
                        damageable.takeDamage(damage * 100);
                    else
                        damageable.takeDamage(damage);
                }
            }

            //StartCoroutine(Flash());

            yield return new WaitForSeconds(fireRate);
            canShoot = true;
        }
        else if (primary != null)
        {
            if (!oOASound)
            {
                oOASound = true;
                audi.PlayOneShot(outofAmmo[Random.Range(0, outofAmmo.Length)], outofammoVol);
                yield return new WaitForSeconds(fireRate);
                oOASound = false;
            }
        }
    }

    IEnumerator Flash()
    {
        //wepFlash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
        //wepFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        //wepFlash.SetActive(false);
    }

    IEnumerator Reload()
    {
        canShoot = false;
        reloading = true;
        ammoReserve += ammo;
        yield return new WaitForSeconds(reloadTime);
        if (ammoReserve >= magCap)
        {
            ammoReserve -= magCap;
            ammo = magCap;
            primary.ammoReserve = ammoReserve;
            primary.ammo = ammo;
        }
        else if (ammoReserve < magCap)
        {
            ammo = ammoReserve;
            ammoReserve = 0;
            primary.ammoReserve = ammoReserve;
            primary.ammo = ammo;
        }
        canShoot = true;
        reloading = false;
        //updateammo();
    }

    public void GiveAmmo(int addedAmmo, weapon gun = null)
    {
        if ((addedAmmo + ammo) > ammoMax)
        {
            ammo = ammoMax;
        }
        else
        {
            ammo += addedAmmo;
        }
    }

    public void AddGun(weapon stats)
    {
        if (primary == null)
        {
            primary = stats;            
        }
        else if (secondary == null)
        {
            secondary = primary;
            primary = stats;
            //prim = false;            
        }
        else
        {
            catchMe = true;
            Instantiate(wepPickup, cam.transform.position, cam.transform.rotation);
            tempHolder = primary;
            primary = stats;     
        }
        updateGunStats();
    }

    public void updateGunStats()
    {        
        ammoMax = primary.ammoMax;
        ammoReserve = primary.ammoReserve;
        ammo = primary.ammo;
        magCap = primary.MagCap;
        damage = primary.damage;
        fireRate = primary.fireRate;
        reloadTime = primary.reloadTime;
        semiTautoF = primary.semiTautoF;
        lazer = primary.Lazer;
        model = primary.model;
        wepEffect = primary.wepEffect;
        ChangeCrosshair();
        //updateammo();
    }

    public void ChangeCrosshair()
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
}