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
    [Header("----Testing----")]
    public bool Armed;

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
        Hands();
        pistolStartPos = new Vector3(-0.488f, 0.735f, 0.705f);
        rifleStartPos = new Vector3(0.044f, 1.139f, 0.154f);
        pistolAimPos = new Vector3(-0.566f, 0.794f, 0.705f);
        rifleAimPos = new Vector3(-0.033f, 1.197f, 0.154f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Pickup"))
        //{
        //    if (GUN != 2)
        //    {
        //        Pickup(2);
        //    }
        //    else
        //        Pickup(1);
        //}

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
        }
    }

    IEnumerator Shoot()
    {
        if (ammo > 0)
        {
            canShoot = false;
            ammo--;

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                Instantiate(wepEffect, hit.point, wepEffect.transform.rotation);

                if (hit.collider.GetComponent<IDamageable>() != null)
                {
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                    if (hit.collider is SphereCollider)
                        damageable.takeDamage(damage * 2);
                    else
                        damageable.takeDamage(damage);
                }
            }

            StartCoroutine(Flash());

            yield return new WaitForSeconds(fireRate);
            canShoot = true;
        }
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
        yield return new WaitForSeconds(reloadTime);
        ammo = magCap;
        canShoot = true;
        reloading = false;
    }

    public void Hands()
    {
        magCap = 0;
        damage = 0;
        fireRate = 0.5f;
        reloadTime = 0;
        semiTautoF = false;
        ammo = magCap;
    }
    public void Pistol()
    {
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
