using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu]

public class weapon : ScriptableObject
{
    #region customClassForSerializedFields
#if UNITY_EDITOR
    [CustomEditor(typeof(weapon))]
    public class weaponFieldEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            weapon wep = (weapon)target;

            //DrawWeapon(wep);
            //EditorGUILayout.Space();
            //DrawAmmo(wep);
            //DrawStats(wep);
            //DrawTypes(wep);
            //DrawSounds(wep);

            base.OnInspectorGUI();
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
            #endregion
        }

        static void DrawWeapon(weapon wep)
        {
            wep.model = EditorGUILayout.ObjectField("Model", wep.model, typeof(GameObject), false) as GameObject;
            wep.Crosshair = EditorGUILayout.ObjectField("Crosshiar", wep.Crosshair, typeof(GameObject), false) as GameObject;
        }

        static void DrawAmmo(weapon wep)
        {
            EditorGUILayout.LabelField("Ammo");
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            wep.ammo = EditorGUILayout.IntField("Ammo", wep.ammo);
            wep.magCap = EditorGUILayout.IntField("Magazine Capacity", wep.magCap);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            wep.ammoReserve = EditorGUILayout.IntField("Ammo Reserve", wep.ammoReserve);
            wep.ammoMax = EditorGUILayout.IntField("Ammo Max", wep.ammoMax);
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
        }
        static void DrawStats(weapon wep)
        {
            EditorGUILayout.LabelField("Stats");
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            wep.reloadTime = EditorGUILayout.FloatField("Reload Time", wep.reloadTime);
            wep.fireRate = EditorGUILayout.FloatField("Fire Rate", wep.fireRate);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            wep.ammo = EditorGUILayout.IntField("Damage", wep.ammo);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }
        static void DrawTypes(weapon wep)
        {
            EditorGUILayout.LabelField("Types");
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Shoot Action", GUILayout.MaxWidth(125));
            EditorGUILayout.EnumFlagsField(wep.shootAction);
            EditorGUILayout.EndHorizontal();
            //Draw Action Attributes

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Shoot Type", GUILayout.MaxWidth(125));
            EditorGUILayout.EnumFlagsField(wep.shootType);
            EditorGUILayout.EndHorizontal();
            //Draw Type Attributes
            if (wep.shootType == weapon.ShootType.Hitscan)
            {
                DrawHitscan(wep);
            }
            else if (wep.shootType == weapon.ShootType.HitscanSpread)
            {
                DrawHitscanSpread(wep);
            }
            else if (wep.shootType == weapon.ShootType.Projectile)
            {
                DrawProjectile(wep);
            }
            EditorGUI.indentLevel--;
        }
        static void DrawHitscan(weapon wep)
        {
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("Hitscan", GUILayout.MaxWidth(125));
            EditorGUI.indentLevel++;
            wep.wepEffect = EditorGUILayout.ObjectField("Hit Effect", wep.wepEffect, typeof(GameObject), false) as GameObject;
        }
        static void DrawHitscanSpread(weapon wep)
        {
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("Hitscan Spread", GUILayout.MaxWidth(125));
            EditorGUI.indentLevel++;
            wep.wepEffect = EditorGUILayout.ObjectField("Hit Effect", wep.wepEffect, typeof(GameObject), false) as GameObject;
        }
        static void DrawProjectile(weapon wep)
        {
            EditorGUI.indentLevel--;
            EditorGUILayout.LabelField("Projectile", GUILayout.MaxWidth(125));
            EditorGUI.indentLevel++;
            wep.wepEffect = EditorGUILayout.ObjectField("Projectile", wep.wepEffect, typeof(GameObject), false) as GameObject;
        }

        static void DrawSounds(weapon wep)
        {
            wep.soundsbool = EditorGUILayout.Foldout(wep.soundsbool, "Sounds", true);
            EditorGUI.indentLevel++;
            if (wep.soundsbool)
            {
                // GUN SHOT Sounds
                int gunsize = Mathf.Max(0, EditorGUILayout.IntField("Gunshots", wep.gunshot.Count));
                while (gunsize > wep.gunshot.Count)
                {
                    wep.gunshot.Add(null);
                }
                while (gunsize < wep.gunshot.Count)
                {
                    wep.gunshot.RemoveAt(wep.gunshot.Count - 1);
                }
                EditorGUI.indentLevel++;
                for (int i = 0; i < wep.gunshot.Count; i++)
                {
                    wep.gunshot[i] = EditorGUILayout.ObjectField("Clip " + i + 1, wep.gunshot[i], typeof(AudioClip), false) as AudioClip;
                }
                EditorGUI.indentLevel--;

                // OUT OF AMMO CLICKS
                int ooasize = Mathf.Max(0, EditorGUILayout.IntField("Out Of Ammo", wep.outofAmmo.Count));
                while (ooasize > wep.outofAmmo.Count)
                {
                    wep.outofAmmo.Add(null);
                }
                while (ooasize < wep.outofAmmo.Count)
                {
                    wep.outofAmmo.RemoveAt(wep.outofAmmo.Count - 1);
                }
                EditorGUI.indentLevel++;
                for (int i = 0; i < wep.outofAmmo.Count; i++)
                {
                    wep.outofAmmo[i] = EditorGUILayout.ObjectField("Clip " + i + 1, wep.outofAmmo[i], typeof(AudioClip), false) as AudioClip;
                }
                EditorGUI.indentLevel--;

                // Reloading Sounds
                int relsize = Mathf.Max(0, EditorGUILayout.IntField("Reload Sounds", wep.reloadSound.Count));
                while (relsize > wep.reloadSound.Count)
                {
                    wep.reloadSound.Add(null);
                }
                while (relsize < wep.outofAmmo.Count)
                {
                    wep.reloadSound.RemoveAt(wep.reloadSound.Count - 1);
                }
                EditorGUI.indentLevel++;
                for (int i = 0; i < wep.reloadSound.Count; i++)
                {
                    wep.reloadSound[i] = EditorGUILayout.ObjectField("Clip " + i + 1, wep.reloadSound[i], typeof(AudioClip), false) as AudioClip;
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
    }

#endif
    #endregion
    public enum ShootType { Hitscan, HitscanSpread, Projectile }
    public enum ShootAction { SemiAuto, FullAuto }

    #region Hiden elements
    ////[Header("----WepAttributes----")]
    //[HideInInspector] public GameObject model;
    //[HideInInspector] public GameObject wepEffect;
    //[HideInInspector] public GameObject Crosshair;
    ////[Header("----WepStats----")]
    //[HideInInspector] public int ammo;
    //[HideInInspector] public int ammoMax;
    //[HideInInspector] public int ammoReserve;
    //[HideInInspector] public int magCap;
    //[HideInInspector] public int damage;
    //[HideInInspector] public float fireRate;
    //[HideInInspector] public float reloadTime;
    //[HideInInspector] public ShootAction shootAction;
    //[HideInInspector] public ShootType shootType;
    ////[Header("----WepSounds----")]
    //[HideInInspector] public List<AudioClip> gunshot;
    //[HideInInspector] public List<AudioClip> outofAmmo;
    //[HideInInspector] public List<AudioClip> reloadSound;
    #endregion
    #region Normal Inspector
    [Header("----WepAttributes----")]
    public GameObject model;
    public GameObject wepEffect;
    public GameObject Crosshair;
    [Header("----WepStats----")]
    public int ammo;
    public int ammoMax;
    public int ammoReserve;
    public int magCap;
    public int damage;
    public float fireRate;
    public float reloadTime;
    public ShootAction shootAction;
    public ShootType shootType;
    public int spreadshots;
    public int spreadangle;
    //[Header("----WepSounds----")]
    public List<AudioClip> gunshot;
    public List<AudioClip> outofAmmo;
    public List<AudioClip> reloadSound;
    #endregion

    [Header("----WepStats----")]
    bool soundsbool = false;
}