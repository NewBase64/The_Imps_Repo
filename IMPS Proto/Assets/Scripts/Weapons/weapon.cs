using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class weapon : ScriptableObject
{
    [Header("----WepStats----")]
    [Range(1, 1000)][SerializeField] public int ammo;
    [Range(1, 1000)][SerializeField] public int ammoMax;
    [Range(1, 1000)][SerializeField] public int ammoReserve;
    [Range(1, 200)][SerializeField] public int magCap;
    [Range(1, 200)][SerializeField] public int damage;
    [Range(0.1f, 10)][SerializeField] public float fireRate;
    [Range(0.25f, 10)][SerializeField] public float reloadTime;
    [SerializeField] public bool semiTautoF;
    [Range(1,3)][SerializeField] public int shootType;
    [Header("----WepAttributes----")]
    [SerializeField] public GameObject model;
    [SerializeField] public GameObject wepEffect;
    [SerializeField] public int Crosshair;
    [Header("----WepSounds----")]
    [SerializeField] public AudioClip[] gunshot;
    [Range(0, 1)][SerializeField] public float gunShotVol;
    [SerializeField] public AudioClip[] outofAmmo;
    [Range(0, 1)][SerializeField] public float outofammoVol;
    [SerializeField] public AudioClip[] reloadSound;
    [Range(0, 1)][SerializeField] public float reloadVol;
}
