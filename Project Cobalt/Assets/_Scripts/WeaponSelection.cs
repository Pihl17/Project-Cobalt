using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class WeaponSelection : MonoBehaviour
{

    public static WeaponSelection instance;
    public Weapon[] choosenWeapons = new Weapon[3];


    void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
