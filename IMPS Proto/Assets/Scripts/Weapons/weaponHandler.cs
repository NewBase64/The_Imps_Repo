using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHandler : MonoBehaviour
{
    [Header("----WepStats----")]
    [SerializeField] bool GUN = false;
    [SerializeField] int ammo;
    [SerializeField] int magCap;
    [SerializeField] int mags;
    [SerializeField] int damage;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    [SerializeField] bool semiTautoF;
    [Header("----Weapon----")]
    [SerializeField] GameObject noModel;   
    [SerializeField] GameObject model;
    [SerializeField] GameObject wepEffect;
    //[SerializeField] GameObject wepFlash;    
    //[SerializeField] GameObject wepBarrel;
    weapon primary;
    weapon secondary;
    bool prim;
    [Header("----Audio----")]
    public AudioSource audi;
    [SerializeField] AudioClip[] gunshot;
    [Range(0, 1)][SerializeField] float gunShotVol;
    bool lazer = false;
    [SerializeField] AudioClip[] lazershot;
    [Range(0, 1)][SerializeField] float lazerShotVol;
    [SerializeField] AudioClip[] outofAmmo;
    [Range(0, 1)][SerializeField] float outofammoVol;
    [SerializeField] AudioClip[] reloadSound;
    [Range(0, 1)][SerializeField] float reloadVol;
    //[Header("----Testing----")]
    //public bool Armed;

    bool canShoot = true;
    bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        model = noModel;
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
            if (Input.GetButtonDown("Mouse ScrollWheel"))
            {
                prim = !prim;
                updateGunStats();
            }
            //aim();
        }
    }

    public void unArm()
    {
        canShoot = false;
        primary = null;
        secondary = null;
    }

    //void aim()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        switch (GUN)
    //        {
    //            case 1:
    //                pistol.transform.localPosition = pistolAimPos;
    //                break;
    //            case 2:
    //                rifle.transform.localPosition = rifleAimPos;
    //                break;
    //        }
    //        gamemanager.instance.cameraScript.fov = 45;
    //        wepFlash.transform.Translate(wepBarrel.transform.position);
    //    }
    //    else if (Input.GetMouseButtonUp(1))
    //    {
    //        switch (GUN)
    //        {
    //            case 1:
    //                pistol.transform.localPosition = pistolStartPos;
    //                break;
    //            case 2:
    //                rifle.transform.localPosition = rifleStartPos;
    //                break;
    //        }
    //        gamemanager.instance.cameraScript.fov = 90;
    //        wepFlash.transform.Translate(wepBarrel.transform.position);
    //    }
    //}

    IEnumerator Shoot()
    {
        if (ammo > 0)
        {
            canShoot = false;
            ammo--;

            if(lazer)
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

            StartCoroutine(Flash());

            yield return new WaitForSeconds(fireRate);
            canShoot = true;
        }
        else
        {
            audi.PlayOneShot(outofAmmo[Random.Range(0, outofAmmo.Length)], outofammoVol);
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
        yield return new WaitForSeconds(reloadTime);
        ammo = magCap;
        canShoot = true;
        reloading = false;
    }

    public void AddGun(weapon stats)
    {
        if (primary == null)
        {
            primary = stats;
            updateGunStats();
        }
        else if (secondary == null)
        {
            secondary = stats;
            prim = false;
            updateGunStats();
        }
    }

    public void updateGunStats()
    {
        if (prim && primary != null)
        {
            ammo = primary.ammo;
            magCap = primary.magCap;
            mags = primary.mags;
            damage = primary.damage;
            fireRate = primary.fireRate;
            reloadTime = primary.reloadTime;
            semiTautoF = primary.semiTautoF;
            model = primary.model;
            wepEffect = primary.wepEffect;
            gamemanager.instance.ChangeCrosshair(primary.Crosshair);
        }
        else if (!prim && secondary != null)
        {
            ammo = secondary.ammo;
            magCap = secondary.magCap;
            mags = secondary.mags;
            damage = secondary.damage;
            fireRate = secondary.fireRate;
            reloadTime = secondary.reloadTime;
            semiTautoF = secondary.semiTautoF;
            model = secondary.model;
            wepEffect = secondary.wepEffect;
            gamemanager.instance.ChangeCrosshair(secondary.Crosshair);
        }
        else
        {
            ammo = 0;
            magCap = 0;
            mags = 0;
            damage = 0;
            fireRate = 0;
            reloadTime = 0;
            model = noModel;
        }
    }
}