using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class weapon : ScriptableObject
{
    [Header("----WepStats----")]
    [SerializeField] public int ammo;
    [SerializeField] public int magCap;
    [SerializeField] public int mags;
    [SerializeField] public int damage;
    [SerializeField] public float fireRate;
    [SerializeField] public float reloadTime;
    [SerializeField] public bool semiTautoF;
    [Header("----WepAttributes----")]
    [SerializeField] public GameObject model;
    [SerializeField] public GameObject wepEffect;
    [SerializeField] public int Crosshair;
    [Header("----WepSounds----")]
    [SerializeField] AudioClip[] gunshot;
    [Range(0, 1)][SerializeField] float gunShotVol;
    [SerializeField] AudioClip[] outofAmmo;
    [Range(0, 1)][SerializeField] float outofammoVol;
    [SerializeField] AudioClip[] reloadSound;
    [Range(0, 1)][SerializeField] float reloadVol;
}
