using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class weaponHandler : MonoBehaviour
{
    #region customClassForSerializedFields
#if UNITY_EDITOR
    [CustomEditor(typeof(weaponHandler))]
    [System.Serializable]
    public class weaponHandlerFieldEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            weaponHandler wepHand = (weaponHandler)target;

            //EditorGUILayout.Space(); // delete me later
            //DrawWeapon(wepHand);
            //DrawGrenades(wepHand);
            //DrawSounds(wepHand);
            //DrawDev(wepHand);
            //DrawDropping(wepHand);
            //DrawFlash(wepHand);
            #region ListExample
            /*           
            List <GameObject> list = wepHand.listname;
            int size = mathf.Max(0, EditorGUILayout.IntField("Size", list.Count));            
            while(size > list.count)
            {
                list.Add(null);
            }
            while (size < list.count)
            {
                list.RemoveAt(list.Count - 1);
            }
            for (int i = 0; i < list.count; i++)
            {
                list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(GameObject, true) as GameObject;
            }
            */

            base.OnInspectorGUI();
            #endregion
        }

        static void DrawWeapon(weaponHandler wepHand)
        {
            wepHand.weaponsbool = EditorGUILayout.Foldout(wepHand.weaponsbool, "Weapons", true);
            if (wepHand.weaponsbool)
            {
                EditorGUI.indentLevel++;
                wepHand.secondary = EditorGUILayout.ObjectField("Secondary", wepHand.secondary, typeof(weapon), false) as weapon;
                if (wepHand.secondary != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    wepHand.secammo = EditorGUILayout.IntField("Secondary Ammo", wepHand.secammo);
                    wepHand.secammoRes = EditorGUILayout.IntField("Ammo Reserve", wepHand.secammoRes);
                    EditorGUILayout.EndHorizontal();
                }
                wepHand.primary = EditorGUILayout.ObjectField("Primary", wepHand.primary, typeof(weapon), false) as weapon;
                if (wepHand.primary != null)
                {
                    DrawAmmo(wepHand);
                    DrawStats(wepHand);
                    DrawTypes(wepHand);
                }
                EditorGUI.indentLevel--;
            }
        }
        static void DrawAmmo(weaponHandler wepHand)
        {
            EditorGUILayout.LabelField("Ammo");
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            wepHand.ammo = EditorGUILayout.IntField("Ammo", wepHand.ammo);
            wepHand.magCap = EditorGUILayout.IntField("Magazine Capacity", wepHand.magCap);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            wepHand.ammoReserve = EditorGUILayout.IntField("Ammo Reserve", wepHand.ammoReserve);
            wepHand.ammoMax = EditorGUILayout.IntField("Ammo Max", wepHand.ammoMax);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Can Shoot", GUILayout.Width(127));
            wepHand.canShoot = EditorGUILayout.Toggle(wepHand.canShoot, GUILayout.Width(20));
            EditorGUILayout.LabelField("Reloading", GUILayout.Width(89));
            wepHand.reloading = EditorGUILayout.Toggle(wepHand.reloading, GUILayout.Width(20));
            EditorGUILayout.LabelField("Out Of Ammo", GUILayout.Width(106));
            wepHand.oOAmmo = EditorGUILayout.Toggle(wepHand.oOAmmo);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }
        static void DrawStats(weaponHandler wepHand)
        {
            EditorGUILayout.LabelField("Stats");
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            wepHand.reloadTime = EditorGUILayout.FloatField("Reload Time", wepHand.reloadTime);
            wepHand.fireRate = EditorGUILayout.FloatField("Fire Rate", wepHand.fireRate);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            wepHand.damage = EditorGUILayout.IntField("Damage", wepHand.damage);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }
        static void DrawTypes(weaponHandler wepHand)
        {
            EditorGUILayout.LabelField("Types");
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Shoot Action", GUILayout.MaxWidth(125));
            EditorGUILayout.EnumFlagsField(wepHand.shootAction);
            EditorGUILayout.EndHorizontal();
            //Draw Action Attributes
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Shoot Type", GUILayout.MaxWidth(125));
            EditorGUILayout.EnumFlagsField(wepHand.shootType);
            EditorGUILayout.EndHorizontal();
            //Draw Type Attributes
            EditorGUI.indentLevel--;
        }
        static void DrawGrenades(weaponHandler wepHand)
        {
            wepHand.grenadesbool = EditorGUILayout.Foldout(wepHand.grenadesbool, "Grenades", true);
            EditorGUI.indentLevel++;
            if (wepHand.grenadesbool)
            {
                wepHand.grenades = EditorGUILayout.IntField("Grenades", wepHand.grenades);
                wepHand.maxGrenades = EditorGUILayout.IntField("Max Grenades", wepHand.maxGrenades);
                wepHand.playerGrenade = EditorGUILayout.ObjectField("Grenade Object", wepHand.playerGrenade, typeof(GameObject), true) as GameObject;
            }
            EditorGUI.indentLevel--;
        }
        static void DrawSounds(weaponHandler wepHand)
        {
            wepHand.soundsbool = EditorGUILayout.Foldout(wepHand.soundsbool, "Sounds", true);
            EditorGUI.indentLevel++;
            if (wepHand.soundsbool)
            {
                wepHand.audi = EditorGUILayout.ObjectField("Audiop Source", wepHand.audi, typeof(AudioSource), true) as AudioSource;
                int gunsize = Mathf.Max(0, EditorGUILayout.IntField("Gunshots", wepHand.gunshot.Count));
                while (gunsize > wepHand.gunshot.Count)
                {
                    wepHand.gunshot.Add(null);
                }
                while (gunsize < wepHand.gunshot.Count)
                {
                    wepHand.gunshot.RemoveAt(wepHand.gunshot.Count - 1);
                }
                EditorGUI.indentLevel++;
                for (int i = 0; i < wepHand.gunshot.Count; i++)
                {
                    wepHand.gunshot[i] = EditorGUILayout.ObjectField("Clip " + i + 1, wepHand.gunshot[i], typeof(AudioClip), true) as AudioClip;
                }
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel++;
                EditorGUILayout.Slider("Volume", wepHand.gunShotVol, 0, 1);
                EditorGUI.indentLevel--;

                int ooasize = Mathf.Max(0, EditorGUILayout.IntField("Out Of Ammo", wepHand.outofAmmo.Count));
                while (ooasize > wepHand.outofAmmo.Count)
                {
                    wepHand.outofAmmo.Add(null);
                }
                while (ooasize < wepHand.outofAmmo.Count)
                {
                    wepHand.outofAmmo.RemoveAt(wepHand.outofAmmo.Count - 1);
                }
                EditorGUI.indentLevel++;
                for (int i = 0; i < wepHand.outofAmmo.Count; i++)
                {
                    wepHand.outofAmmo[i] = EditorGUILayout.ObjectField("Clip " + i + 1, wepHand.outofAmmo[i], typeof(AudioClip), true) as AudioClip;
                }
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel++;
                EditorGUILayout.Slider("Volume", wepHand.outofammoVol, 0, 1);
                EditorGUI.indentLevel--;

                int relsize = Mathf.Max(0, EditorGUILayout.IntField("Reload", wepHand.reloadSound.Count));
                while (relsize > wepHand.reloadSound.Count)
                {
                    wepHand.reloadSound.Add(null);
                }
                while (relsize < wepHand.reloadSound.Count)
                {
                    wepHand.reloadSound.RemoveAt(wepHand.reloadSound.Count - 1);
                }
                EditorGUI.indentLevel++;
                for (int i = 0; i < wepHand.reloadSound.Count; i++)
                {
                    wepHand.reloadSound[i] = EditorGUILayout.ObjectField("Clip " + i + 1, wepHand.reloadSound[i], typeof(AudioClip), true) as AudioClip;
                }
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel++;
                EditorGUILayout.Slider("Volume", wepHand.reloadVol, 0, 1);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        static void DrawDev(weaponHandler wepHand)
        {
            wepHand.devbool = EditorGUILayout.Foldout(wepHand.devbool, "Developer Items", true);
            if (wepHand.devbool)
            {
                EditorGUI.indentLevel++;
                wepHand.InfiniteAmmo = EditorGUILayout.Toggle("Infinite Ammo", wepHand.InfiniteAmmo);
                wepHand.bottomlessClip = EditorGUILayout.Toggle("Bottomless Clip", wepHand.bottomlessClip);
                wepHand.unlimitedGrenades = EditorGUILayout.Toggle("Unlimited Grenades", wepHand.unlimitedGrenades);
                EditorGUI.indentLevel--;
            }
        }
        static void DrawDropping(weaponHandler wepHand)
        {
            wepHand.dropbool = EditorGUILayout.Foldout(wepHand.dropbool, "Weapon Droping stuff", true);
            if (wepHand.dropbool)
            {
                EditorGUI.indentLevel++;
                wepHand.wepPickup = EditorGUILayout.ObjectField("Weapon Pickup", wepHand.wepPickup, typeof(GameObject), true) as GameObject;
                EditorGUI.indentLevel--;
            }
        }
        static void DrawFlash(weaponHandler wepHand)
        {
            wepHand.flashbool = EditorGUILayout.Foldout(wepHand.flashbool, "Flash Variables", true);
            if (wepHand.flashbool)
            {
                EditorGUI.indentLevel++;
                wepHand.catchMe = EditorGUILayout.Toggle("Catch Me", wepHand.catchMe);
                EditorGUILayout.BeginHorizontal();
                wepHand.pubammo = EditorGUILayout.IntField("Public Ammo", wepHand.pubammo);
                wepHand.pubammoRes = EditorGUILayout.IntField("Public Ammo Reserve", wepHand.pubammoRes);
                EditorGUILayout.EndHorizontal();
                wepHand.pubHolder = EditorGUILayout.ObjectField("Public Weapon Holder", wepHand.pubHolder, typeof(weapon), false) as weapon;
                EditorGUI.indentLevel--;
            }
        }
    }
#endif
    #endregion
    #region Declarations
    [Header("----Weapons----")]
    [SerializeField] weapon primary;
    [SerializeField] int ammo;
    [SerializeField] int ammoReserve;
    [SerializeField] int magCap;
    [SerializeField] int ammoMax;
    [SerializeField] int damage;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    [SerializeField] weapon.ShootAction shootAction;
    [SerializeField] weapon.ShootType shootType;
    [SerializeField] int spreadshots;
    [SerializeField] int spreadangle;

    [SerializeField] bool canShoot = true;
    [SerializeField] bool reloading = false;
    [SerializeField] bool oOAmmo = false;

    [SerializeField] weapon secondary;
    [SerializeField] int secammo;
    [SerializeField] int secammoRes;

    [Header("----Holder objs----")]
    [SerializeField] GameObject wepEffect;
    //[HideInInspector] GameObject wepFlash;
    //[HideInInspector] GameObject wepSpawn; // Game object to dictate where player holds the weapon
    [SerializeField] weapon tempHolder;
    [SerializeField] int holdammo;
    [SerializeField] int holdammoRes;

    [Header("----Grenades----")]
    [SerializeField] int grenades;
    [SerializeField] int maxGrenades;
    [SerializeField] GameObject playerGrenade;
    [SerializeField] GameObject nonGrenade = null;

    [Header("----Audio----")]
    [SerializeField] AudioSource audi;
    [SerializeField] List<AudioClip> gunshot;
    [SerializeField] float gunShotVol;
    [SerializeField] List<AudioClip> outofAmmo;
    [SerializeField] float outofammoVol;
    [SerializeField] List<AudioClip> reloadSound;
    [SerializeField] float reloadVol;

    [Header("----Dev----")]
    [SerializeField] bool InfiniteAmmo;
    [SerializeField] bool bottomlessClip;
    [SerializeField] bool unlimitedGrenades;

    [Header("----Weapon Drop stuff----")]
    [SerializeField] GameObject wepPickup;

    [Header("----Flash Vars----")]
    [HideInInspector] public bool catchMe = false;
    [HideInInspector] public int pubammo;
    [HideInInspector] public int pubammoRes;
    [HideInInspector] public weapon pubHolder;

    [Header("----Crosshairs----")]
    [SerializeField] public GameObject CurrCrosshair;
    [SerializeField] GameObject NoCrosshair;

    //[Header("----Editor Bools----")]
    [HideInInspector][SerializeField] bool weaponsbool;
    [HideInInspector][SerializeField] bool grenadesbool;
    [HideInInspector][SerializeField] bool soundsbool;
    [HideInInspector][SerializeField] bool devbool;
    [HideInInspector][SerializeField] bool dropbool;
    [HideInInspector][SerializeField] bool flashbool;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Set defaults so no null references
        CurrCrosshair = NoCrosshair;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamemanager.instance.menuCurrentlyOpen) // if there is not a menu open
        {
            // shoot
            Shooting();

            // Reload
            if (Input.GetButtonDown("Reload") && !reloading)
            {
                audi.PlayOneShot(reloadSound[Random.Range(0, gunshot.Count)], reloadVol);
                StartCoroutine(Reload());
            }

            // switch weapon
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                SwitchWeapon();
                updateGunStats();
            }

            // aim
            aim();
            //grenade
            Grenade();
        }
    }

    #region Privates
    void Shooting()
    {
        if (shootAction == weapon.ShootAction.SemiAuto)
        {
            if (Input.GetMouseButtonDown(0) && canShoot) // shoot once
            {
                StartCoroutine(Shoot());
            }
        }
        else if (shootAction == weapon.ShootAction.FullAuto)
        {
            if (Input.GetMouseButton(0) && canShoot) // shoot while held
            {
                StartCoroutine(Shoot());
            }
        }
    }
    void Grenade()
    {
        if (Input.GetButtonDown("Grenade") && !reloading)
        {
            if (!unlimitedGrenades)
            {
                if (grenades != 0)
                {
                    grenades--;
                    nonGrenade = Instantiate(playerGrenade, gamemanager.instance.mainCam.transform.position + gamemanager.instance.mainCam.transform.forward, gamemanager.instance.mainCam.transform.rotation);
                    grenade gren = nonGrenade.GetComponent<grenade>();
                    gren.direction = true;
                }
            }
            else
            {
                nonGrenade = Instantiate(playerGrenade, gamemanager.instance.mainCam.transform.position + gamemanager.instance.mainCam.transform.forward, gamemanager.instance.mainCam.transform.rotation);
                grenade gren = nonGrenade.GetComponent<grenade>();
                gren.direction = true;
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
    void updateGunStats()
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
        shootAction = primary.shootAction;
        shootType = primary.shootType;
        wepEffect = primary.wepEffect;
        spreadshots = primary.spreadshots;
        spreadangle = primary.spreadangle;
        gunshot = primary.gunshot;
        reloadSound = primary.reloadSound;
        outofAmmo = primary.outofAmmo;
        CurrCrosshair.SetActive(false);
        CurrCrosshair = primary.Crosshair;
        CurrCrosshair.SetActive(true);
        gamemanager.instance.updateAmmoCount();
    }
    #endregion

    #region Publics
    public int GiveAmmo(int primsec, int addammo = 0)
    {
        int ammoback = 0;
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

        }
        else // If adding a specific amount of ammo
        {
            if (primsec == 1) // If giving to the primary weapon
            {
                if ((addammo + ammoReserve) > ammoMax) // If ammo overflow
                {
                    ammoback = ammoMax - ammoReserve; // get the amount of ammo needed to fill up 
                    ammoReserve = ammoMax; // set ammo to max                    
                }
                else
                    ammoReserve += addammo; // add ammo to primary                                   
            }
            else // Giving to the secondary
            {
                if ((addammo + secammoRes) > secondary.ammoMax)
                {
                    ammoback = secondary.ammoMax - secammoRes;
                    secammoRes = secondary.ammoMax;
                    return ammoback;
                }
                else
                    secammoRes += addammo;
            }
        }
        if (ammoback != 0)
        {
            return ammoback;
        }
        return 0;
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
                Instantiate(wepPickup, gamemanager.instance.mainCam.transform.position + gamemanager.instance.mainCam.transform.forward, gamemanager.instance.mainCam.transform.rotation);
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
    public void FillAmmo()
    {
        ammoReserve = ammoMax;
        secammoRes = secondary.ammoMax;
    }
    public void unArm()
    {
        primary = null;
        secondary = null;
        CurrCrosshair.SetActive(false);
        CurrCrosshair = NoCrosshair;
        CurrCrosshair.SetActive(true);
    }
    #endregion

    #region IEnumerators
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
            audi.PlayOneShot(gunshot[Random.Range(0, gunshot.Count)], gunShotVol);

            //
            if (shootType == weapon.ShootType.Hitscan)
            {
                RaycastHit hit;
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
            }                                                                                            //
            else if (shootType == weapon.ShootType.HitscanSpread)
            {
                RaycastHit hit;
                for (int i = 0; i < spreadshots; i++)                                                    //
                {                                                                                        //
                    float angle = spreadangle / 10;                                                      //
                    float min = 0.5f - angle;                                                            //
                    float max = 0.5f + angle;                                                            //
                    float x = Random.Range(min, max);                                                    //
                    float y = Random.Range(min, max);                                                    //
                    if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(x, y, 0)), out hit))  //
                    {                                                                                    //
                        Instantiate(wepEffect, hit.point, wepEffect.transform.rotation);                 //
                                                                                                         //
                        if (hit.collider.GetComponent<IDamageable>() != null)                            //
                        {                                                                                //
                            IDamageable damageable = hit.collider.GetComponent<IDamageable>();           //  Shotgun weapons
                            if (hit.collider is SphereCollider)                                          //
                                damageable.takeDamage(damage * 100);                                     //
                            else                                                                         //
                                damageable.takeDamage(damage);                                           //
                        }                                                                                //
                    }                                                                                    //
                }
            }
            else if (shootType == weapon.ShootType.Projectile)
            {

            }

            //StartCoroutine(Flash());

            yield return new WaitForSeconds(fireRate); // wait
            canShoot = true; // allow the weapon to shoot agian
        }
        else if (primary != null) // if the player has a weapon
        {
            if (!oOAmmo) // if the sound is not playing already
            {              // play the sound
                oOAmmo = true;
                audi.PlayOneShot(outofAmmo[Random.Range(0, gunshot.Count)], outofammoVol);
                yield return new WaitForSeconds(fireRate);
                oOAmmo = false;
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
    #endregion

    #region Getters
    public int GetAmmo()
    {
        return ammo;
    }
    public int GetSecondaryAmmo()
    {
        return secammo;
    }
    public int GetAmmoReserve()
    {
        return ammoReserve;
    }
    public int GetSecondaryAmmoReserve()
    {
        return secammoRes;
    }
    public weapon GetPrimary()
    {
        return primary;
    }
    public weapon GetSecondary()
    {
        return secondary;
    }
    #endregion
}