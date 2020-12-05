using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons {

	public class ChargableBlast : Weapon
	{
    
		bool charging;
		float chargeStart;
        //float chargeDuration = 1;
        //int[] damage = new int[]{5, 10};
        //float[] blastSize = new float[]{1,1.5f};
        Material blastVisual;

		public ChargableBlast() {
            configFile = Resources.Load<WeaponConfig>("WeaponConfigs/ChargableBlastConfig");
        }

        float GetChargeLevel() {
            return Mathf.Clamp((Time.time - chargeStart) / configFile.FloatValue[ValueName.ChargeTime], 0, 1);
        }

		public override void Fire(WeaponFireContext context) {
            if (!blastVisual)
            {
                GameObject blastVisualObject = GameObject.Instantiate(configFile.InstantiatableObjects[0], context.userTrans.position + context.userTrans.forward * (0.6f + configFile.Range * configFile.FloatValue[ValueName.RangeMultiplier]), context.userTrans.rotation, context.userTrans);
                blastVisualObject.transform.Rotate(new Vector3(90,0,0));
                blastVisualObject.transform.localScale = Vector3.one * configFile.Range * configFile.FloatValue[ValueName.RangeMultiplier] * 2;
                blastVisual = blastVisualObject.GetComponent<Renderer>().material;
				blastVisual.SetFloat("_MinRange", 1/configFile.FloatValue[ValueName.RangeMultiplier]);
            }
            if (ReadyToUse())
            {
                if (context.triggerPhase == InputActionPhase.Started)
                {
                    blastVisual.SetFloat("_ChargeProcent", GetChargeLevel());
                }
                else if (context.triggerPhase == InputActionPhase.Performed)
                {

                    chargeStart = Time.time;
                    charging = true;

                }
                else if (context.triggerPhase == InputActionPhase.Canceled)
                {

                    if (!charging)
                        return;

                    float chargeLevel = GetChargeLevel();
                    float curBlastSize = Mathf.Lerp(configFile.Range, configFile.Range * configFile.FloatValue[ValueName.RangeMultiplier], chargeLevel);

                    // This block of code is for debugging only TODO: Replace with a visual effect
                    Vector3 blastCenter = context.userTrans.position + context.userTrans.forward * (0.6f + curBlastSize);
                    Vector3 rightDir = new Vector3(context.userTrans.forward.z, 0, -context.userTrans.forward.x);
                    Debug.DrawLine(context.userTrans.position, blastCenter, Color.blue, 1.0f);
                    Debug.DrawLine(blastCenter + (-context.userTrans.forward - rightDir) * curBlastSize, blastCenter + (context.userTrans.forward - rightDir) * curBlastSize, Color.blue, 1.0f);
                    Debug.DrawLine(blastCenter + (context.userTrans.forward - rightDir) * curBlastSize, blastCenter + (context.userTrans.forward + rightDir) * curBlastSize, Color.blue, 1.0f);
                    Debug.DrawLine(blastCenter + (context.userTrans.forward + rightDir) * curBlastSize, blastCenter + (-context.userTrans.forward + rightDir) * curBlastSize, Color.blue, 1.0f);
                    Debug.DrawLine(blastCenter + (-context.userTrans.forward + rightDir) * curBlastSize, blastCenter + (-context.userTrans.forward - rightDir) * curBlastSize, Color.blue, 1.0f);


                    Collider[] colliders = Physics.OverlapBox(context.userTrans.position + context.userTrans.forward * (0.6f + curBlastSize), Vector3.one * curBlastSize);

                    for (int i = 0; i < colliders.Length; i++)
                    {
						ApplyDamageToEnemy(colliders[i], Mathf.Lerp(configFile.Damage, configFile.Damage * configFile.FloatValue[ValueName.DamageMultiplier], chargeLevel));
						/*if (colliders[i].GetComponent<DestructableScript>())
                        {
                            colliders[i].GetComponent<DestructableScript>().Damage(Mathf.Lerp(configFile.Damage, configFile.Damage * configFile.FloatValue[ValueName.DamageMultiplier], chargeLevel));
                        }*/
                    }

                    blastVisual.SetFloat("_ChargeProcent", 0.0f);
					blastVisual.SetFloat("_ChargeProcentWhenFired", GetChargeLevel());
					blastVisual.SetFloat("_FireTime", Time.timeSinceLevelLoad);
                    charging = false;
                    cooldownTimer = 0;
                }
            }
		}

	}
}