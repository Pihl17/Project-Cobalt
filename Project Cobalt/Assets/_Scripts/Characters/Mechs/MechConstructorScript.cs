using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class MechConstructorScript : MonoBehaviour
{

    public GameObject mechPrefab;

    public void ConstructMech(Vector3 pos, Quaternion rot) {
        CombatMech mech = Instantiate(mechPrefab, pos, rot).GetComponent<CombatMech>();


        if (!WeaponSelection.instance) {
            Debug.LogError("Lacking WeaponSelection instance", this);
            return;
        }
        Vector3[] weaponPos = new Vector3[] {mech.mechConfig.GunLocation, mech.mechConfig.HeavyLocation, mech.mechConfig.ArtilleryLocation};
        Weapon[] mechWeapons = new Weapon[3];
        for (int i = 0; i < weaponPos.Length; i++) {
            mechWeapons[i] = Instantiate(WeaponSelection.instance.choosenWeapons[i], mech.transform.position + weaponPos[i], mech.transform.rotation, mech.transform);
        }
        mech.SetWeapons(mechWeapons);

        ConstructAmmoDisplays(mech, mechWeapons);
    }

    void ConstructAmmoDisplays(CombatMech mech, Weapon[] mechWeapons) {
        if (mech.gameObject.GetComponentInChildren<PlayerAmmoDisplay>())
            mech.gameObject.GetComponentInChildren<PlayerAmmoDisplay>().SetWeaponsToTrack(mechWeapons);
    }

}
