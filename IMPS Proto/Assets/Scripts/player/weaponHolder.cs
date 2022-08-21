using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHolder : MonoBehaviour
{
    [SerializeField] GameObject MainHolder;
    [Header("----Orig Points----")]
    [SerializeField] GameObject PistolOrig;
    [SerializeField] GameObject RifleOrig;
    [SerializeField] GameObject RocketOrig;
    [SerializeField] GameObject ShotgunOrig;
    [SerializeField] GameObject ShotgunPumpOrig;
    [SerializeField] GameObject ShotgunBarrelsOrig;
    [Header("----ADS Points----")]            // 
    [SerializeField] GameObject PistolADS;    // 
    [SerializeField] GameObject RifleADS;     // unused 4 now
    [SerializeField] GameObject RocketADS;    // 
    [SerializeField] GameObject ShotgunADS;   // 
    [Header("----Shoot Points----")]
    [SerializeField] GameObject PistolShoot;
    [SerializeField] GameObject RifleShoot;
    [SerializeField] GameObject RocketShoot;
    [SerializeField] GameObject ShotgunShoot;
    [SerializeField] GameObject ShotgunPumpPumped;
    [SerializeField] GameObject ShotgunBarrelRotated;
    [Header("----Reload Points----")]
    [SerializeField] GameObject PistolReloadPoint;
    [SerializeField] GameObject RifleReloadPoint;
    [SerializeField] GameObject RocketReloadPoint;
    [SerializeField] GameObject ShotgunReloadPoint;
    [Header("----Models----")]
    [SerializeField] GameObject CurrWep;
    [SerializeField] GameObject Pistol;
    [SerializeField] GameObject Rifle;
    [SerializeField] GameObject Rocket;
    [SerializeField] GameObject Shotgun;
    [SerializeField] GameObject shotgunPump;
    [SerializeField] GameObject shotgunBarrels;
    [Header("----Weapons----")]
    [SerializeField] weapon wepPistol;
    [SerializeField] weapon wepRifle;
    [SerializeField] weapon wepRocket;
    [SerializeField] weapon wepShotgun;
    [Header("----Weapon Sway----")]
    [SerializeField] float swayMult;
    [SerializeField] float swaySmooth;
    [Header("----Audio----")]
    [SerializeField] AudioClip shotgunPumpSound;

    //float shooting;
    bool shot = false;
    float shootCurr = 0, shootTarget = 0;
    float reloadCurr = 0, reloadTarget = 1;

    private void Update()
    {
        WeaponSway();
    }

    void WeaponSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMult;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMult;

        Quaternion rotX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targRot = rotX * rotY;

        MainHolder.transform.localRotation = Quaternion.Slerp(MainHolder.transform.localRotation, targRot, swaySmooth * Time.deltaTime);
    }

    public void GiveWeapon(weapon wep)
    {
        shootCurr = 0;
        if (CurrWep != null)
            CurrWep.SetActive(false);

        if (wep == wepPistol)
        {
            CurrWep = Pistol;
            CurrWep.SetActive(true);
        }
        if (wep == wepRifle)
        {
            CurrWep = Rifle;
            CurrWep.SetActive(true);
        }
        if (wep == wepShotgun)
        {
            CurrWep = Shotgun;
            CurrWep.SetActive(true);
        }
        if (wep == wepRocket)
        {
            CurrWep = Rocket;
            CurrWep.SetActive(true);
        }
    }

    public void Shooting()
    {
        shootCurr = 1;

        if (CurrWep == Pistol)
            StartCoroutine(PistolShot());

        if (CurrWep == Rifle)
            StartCoroutine(RifleShot());

        if (CurrWep == Shotgun)
            StartCoroutine(ShotgunShot());

        if (CurrWep == Rocket)
            StartCoroutine(RocketShot());
    }

    IEnumerator PistolShot()
    {
        shot = true;
        while (shot)
        {
            if (shootCurr == 0 && shot)
                shot = false;

            float tim = wepPistol.fireRate;
            Lerperator(Pistol, PistolOrig, PistolShoot, shootCurr);
            shootCurr = Mathf.MoveTowards(shootCurr, shootTarget, (tim / (tim * tim)) * Time.deltaTime);

            yield return null;
        }
    }
    IEnumerator RifleShot()
    {
        shot = true;
        while (shot)
        {
            if (shootCurr == 0 && shot)
            {
                shot = false;
            }
            float tim = wepRifle.fireRate;
            Lerperator(Rifle, RifleOrig, RifleShoot, shootCurr);
            shootCurr = Mathf.MoveTowards(shootCurr, shootTarget, (tim / (tim * tim)) * Time.deltaTime);

            yield return null;
        }
    }
    IEnumerator ShotgunShot()
    {
        shot = true;
        shootCurr = 0;
        shootTarget = 1;
        while (shot)
        {
            if (shootCurr == 1 && shot)
            {
                shot = false;
                shootTarget = 0;
            }

            float tim = wepShotgun.fireRate / 16;
            Lerperator(Shotgun, ShotgunOrig, ShotgunShoot, shootCurr);
            shootCurr = Mathf.MoveTowards(shootCurr, shootTarget, (tim / (tim * tim)) * Time.deltaTime);

            yield return null;
        }
        yield return new WaitForSeconds((wepShotgun.fireRate / 16) * 7);
        shot = true;
        StartCoroutine(ShotgunPumper());
        StartCoroutine(ShotgunBarrelSpin());
        while (shot)
        {
            if (shootCurr == 0 && shot)
                shot = false;

            float tim = wepShotgun.fireRate / 2;
            Lerperator(Shotgun, ShotgunOrig, ShotgunShoot, shootCurr);
            shootCurr = Mathf.MoveTowards(shootCurr, shootTarget, (tim / (tim * tim)) * Time.deltaTime);

            yield return null;
        }
    }
    IEnumerator ShotgunPumper()
    {
        bool pumping = true;
        float pumpcurr = 0, pumptarg = 1;
        gamemanager.instance.playerScript.audi.PlayOneShot(shotgunPumpSound);
        while (pumping)
        {
            if (pumpcurr == 1)
                pumptarg = 0;
            float tim = wepShotgun.fireRate / 4;
            Lerperator(shotgunPump, ShotgunPumpOrig, ShotgunPumpPumped, pumpcurr);
            pumpcurr = Mathf.MoveTowards(pumpcurr, pumptarg, (tim / (tim * tim)) * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator ShotgunBarrelSpin()
    {
        bool spinning = true;
        float spincurr = 0, spintarg = 1;
        while (spinning)
        {
            float tim = wepShotgun.fireRate / 4;
            Lerperator(shotgunBarrels, ShotgunBarrelsOrig, ShotgunBarrelRotated, spincurr);
            spincurr = Mathf.MoveTowards(spincurr, spintarg, (tim / (tim * tim)) * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator RocketShot()
    {
        shot = true;
        while (shot)
        {
            if (shootCurr == 0 && shot)
                shot = false;

            float tim = wepRocket.fireRate;
            Lerperator(Rocket, RocketOrig, RocketShoot, shootCurr);
            shootCurr = Mathf.MoveTowards(shootCurr, shootTarget, (tim / (tim * tim)) * Time.deltaTime);

            yield return null;
        }
    }

    public void Reloadstart()
    {
        if (CurrWep == Pistol)
            StartCoroutine(PistolReload());
        if (CurrWep == Rifle)
            StartCoroutine(RifleReload());
        if (CurrWep == Shotgun)
            StartCoroutine(ShotgunReload());
        if (CurrWep == Rocket)
            StartCoroutine(RocketReload());
    }

    IEnumerator PistolReload()
    {
        bool reloading = true;
        while (reloading)
        {
            if (reloadCurr == 1)
            {
                reloadTarget = 0;
                gamemanager.instance.playerScript.audi.PlayOneShot(gamemanager.instance.weaponHandler.reloadSound[Random.Range(0, gamemanager.instance.weaponHandler.reloadSound.Count)], gamemanager.instance.weaponHandler.reloadVol);
            }

            float tim = wepPistol.reloadTime / 2;
            Lerperator(Pistol, PistolOrig, PistolReloadPoint, reloadCurr);
            reloadCurr = Mathf.MoveTowards(reloadCurr, reloadTarget, (tim / (tim * tim)) * Time.deltaTime);

            if (reloadCurr == 0)
            {
                reloadTarget = 1;
                reloading = false;
            }

            yield return null;
        }
    }
    IEnumerator RifleReload()
    {
        bool reloading = true;
        while (reloading)
        {
            if (reloadCurr == 1)
            {
                reloadTarget = 0;
                gamemanager.instance.playerScript.audi.PlayOneShot(gamemanager.instance.weaponHandler.reloadSound[Random.Range(0, gamemanager.instance.weaponHandler.reloadSound.Count)], gamemanager.instance.weaponHandler.reloadVol);
            }

            float tim = wepRifle.reloadTime / 2;
            Lerperator(Rifle, RifleOrig, RifleReloadPoint, reloadCurr);
            reloadCurr = Mathf.MoveTowards(reloadCurr, reloadTarget, (tim / (tim * tim)) * Time.deltaTime);

            if (reloadCurr == 0)
            {
                reloadTarget = 1;
                reloading = false;
            }

            yield return null;
        }
    }
    IEnumerator ShotgunReload()
    {
        bool reloading = true;
        while (reloading)
        {
            if (reloadCurr == 1)
            {
                reloadTarget = 0;
                gamemanager.instance.playerScript.audi.PlayOneShot(gamemanager.instance.weaponHandler.reloadSound[Random.Range(0, gamemanager.instance.weaponHandler.reloadSound.Count)], gamemanager.instance.weaponHandler.reloadVol);
            }

            float tim = wepShotgun.reloadTime / 2;
            Lerperator(Shotgun, ShotgunOrig, ShotgunReloadPoint, reloadCurr);
            reloadCurr = Mathf.MoveTowards(reloadCurr, reloadTarget, (tim / (tim * tim)) * Time.deltaTime);

            if (reloadCurr == 0)
            {
                reloadTarget = 1;
                reloading = false;
            }

            yield return null;
        }
    }
    IEnumerator RocketReload()
    {
        bool reloading = true;
        while (reloading)
        {
            if (reloadCurr == 1)
            {
                reloadTarget = 0;
                gamemanager.instance.playerScript.audi.PlayOneShot(gamemanager.instance.weaponHandler.reloadSound[Random.Range(0, gamemanager.instance.weaponHandler.reloadSound.Count)], gamemanager.instance.weaponHandler.reloadVol);
            }

            float tim = wepRocket.reloadTime / 2;
            Lerperator(Rocket, RocketOrig, RocketReloadPoint, reloadCurr);
            reloadCurr = Mathf.MoveTowards(reloadCurr, reloadTarget, (tim / (tim * tim)) * Time.deltaTime);

            if (reloadCurr == 0)
            {
                reloadTarget = 1;
                reloading = false;
            }

            yield return null;
        }
    }

    private void Lerperator(GameObject obj, GameObject orig, GameObject dest, float pos)
    {
        obj.transform.position = Vector3.Lerp(orig.transform.position, dest.transform.position, pos);
        obj.transform.rotation = Quaternion.Lerp(orig.transform.rotation, dest.transform.rotation, pos);
    }
}
