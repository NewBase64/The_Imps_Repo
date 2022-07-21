using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHandler : MonoBehaviour
{
    [Header("----WepStats----")]
    [SerializeField] int ammo;
    [SerializeField] int magCap;
    [SerializeField] int damage;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    [SerializeField] bool semiTautoF;
    [Header("----Weapon----")]
    [SerializeField] int GUN;
    [SerializeField] GameObject hands;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject rifle;
    [SerializeField] GameObject weapon;
    [Header("----WepEffects----")]
    [SerializeField] GameObject pistolEffect;
    [SerializeField] GameObject rifleEffect;
    [SerializeField] GameObject wepEffect;
    [Header("----WepMuzFlash----")]
    [SerializeField] GameObject pistolFlash;
    [SerializeField] GameObject rifleFlash;
    [SerializeField] GameObject wepFlash;
    [Header("----Wepbarrels----")]
    [SerializeField] GameObject pistolBarrel;
    [SerializeField] GameObject rifleBarrel;
    [SerializeField] GameObject wepBarrel;
    [Header("----Crosshairs----")]
    [SerializeField] public GameObject CurrCrosshair;
    [SerializeField] GameObject NoCrosshair;
    [SerializeField] GameObject Crosshair1;
    [SerializeField] GameObject Crosshair2;
    [SerializeField] GameObject Crosshair3;
    [Header("----Audio----")]
    public AudioSource audi;
    [SerializeField] AudioClip[] gunshot;
    [Range(0, 1)][SerializeField] float gunshotVolume;
    [SerializeField] AudioClip[] outofammo;
    [Range(0, 1)][SerializeField] float outofammoVol;
    [SerializeField] AudioClip[] reload;
    [Range(0, 1)][SerializeField] float reloadVol;
    //[Header("----Testing----")]
    //public bool Armed;

    Vector3 pistolStartPos;
    Vector3 rifleStartPos;
    Vector3 pistolAimPos;
    Vector3 rifleAimPos;

    bool canShoot = true;
    bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        weapon = hands;
        wepFlash = pistolFlash;
        wepBarrel = pistolBarrel;
        CurrCrosshair = NoCrosshair;
        Hands();
        pistolStartPos = new Vector3(-0.488f, 0.735f, 0.705f);
        rifleStartPos = new Vector3(0.044f, 1.139f, 0.154f);
        pistolAimPos = new Vector3(-0.566f, 0.794f, 0.705f);
        rifleAimPos = new Vector3(-0.033f, 1.197f, 0.154f);
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
                StartCoroutine(Reload());
            }
            aim();
        }
    }

    public void unArm()
    {
        weapon.SetActive(false);
        canShoot = false;
        weapon = hands;
        Hands();
    }

    void aim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch (GUN)
            {
                case 1:
                    pistol.transform.localPosition = pistolAimPos;
                    break;
                case 2:
                    rifle.transform.localPosition = rifleAimPos;
                    break;
            }
            gamemanager.instance.cameraScript.fov = 45;
            wepFlash.transform.Translate(wepBarrel.transform.position);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            switch (GUN)
            {
                case 1:
                    pistol.transform.localPosition = pistolStartPos;
                    break;
                case 2:
                    rifle.transform.localPosition = rifleStartPos;
                    break;
            }
            gamemanager.instance.cameraScript.fov = 90;
            wepFlash.transform.Translate(wepBarrel.transform.position);
        }
    }

    IEnumerator Shoot()
    {
        if (ammo > 0)
        {
            canShoot = false;
            audi.PlayOneShot(gunshot[Random.Range(0, gunshot.Length)], gunshotVolume);
            ammo--;

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
            audi.PlayOneShot(outofammo[Random.Range(0, outofammo.Length)], outofammoVol);
    }

    IEnumerator Flash()
    {
        wepFlash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
        wepFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        wepFlash.SetActive(false);
    }

    IEnumerator Reload()
    {
        canShoot = false;
        reloading = true;
        audi.PlayOneShot(reload[Random.Range(0, reload.Length)], reloadVol);
        yield return new WaitForSeconds(reloadTime);
        ammo = magCap;
        canShoot = true;
        reloading = false;
    }

    public void Hands()
    {
        CurrCrosshair.SetActive(false);
        CurrCrosshair = NoCrosshair;
        CurrCrosshair.SetActive(true);
        weapon.SetActive(false);
        magCap = 0;
        damage = 0;
        fireRate = 0.5f;
        reloadTime = 0;
        semiTautoF = false;
        ammo = magCap;
    }
    public void Pistol()
    {
        CurrCrosshair.SetActive(false);
        CurrCrosshair = Crosshair1;
        CurrCrosshair.SetActive(true);
        weapon.SetActive(false);
        wepFlash.SetActive(false);
        GUN = 1;
        weapon = pistol;
        wepEffect = pistolEffect;
        wepFlash = pistolFlash;
        weapon.SetActive(true);
        magCap = 8;
        damage = 2;
        fireRate = 0.1f;
        reloadTime = 1;
        semiTautoF = true;
        ammo = magCap;
    }
    public void Rifle()
    {
        CurrCrosshair.SetActive(false);
        CurrCrosshair = Crosshair2;
        CurrCrosshair.SetActive(true);
        weapon.SetActive(false);
        wepFlash.SetActive(false);
        GUN = 2;
        weapon = rifle;
        wepEffect = rifleEffect;
        wepFlash = rifleFlash;
        weapon.SetActive(true);
        magCap = 32;
        damage = 1;
        fireRate = 0.1f;
        reloadTime = 1;
        semiTautoF = false;
        ammo = magCap;
    }
}
