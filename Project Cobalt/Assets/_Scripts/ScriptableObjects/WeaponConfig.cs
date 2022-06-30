using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Weapons {

    public enum ValueName { None, ExplosionRadius, MinCurvatureHeight, SeekForce, MaxVelocity, ChargeTime, DamageMultiplier, RangeMultiplier }

    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObject/WeaponConfig", order = 4)]
    public class WeaponConfig : ScriptableObject
    {
        [Header("Common Variables")]
        [SerializeField] string abilityName = "Unnamed";
        public string Name { get { return abilityName; } }

        [SerializeField] float cooldown = 1;
        public float Cooldown { get { return cooldown; } }
        public float FireRate { get { return 1 / cooldown; } }

        [Header("Weapon Specifications")]
        [SerializeField] Vector3 localFirePoint = new Vector3(0f, 0f, 1.0f);
        public Vector3 LocalFirePoint { get { return localFirePoint; } }

        [SerializeField] float damage = 1;
        public float Damage { get { return damage; } }

        [SerializeField] float range = 10;
        public float Range { get { return range; } }

		[SerializeField] float velocity = 5f;
		public float Velocity { get { return velocity; } }

        [SerializeField] GameObject[] instantiatableObjects = new GameObject[0];
        public GameObject[] InstantiatableObjects { get { return instantiatableObjects; } }


		[Serializable]
		public struct SoundEffect {
			public AudioClip clip;
			[Range(0,1)]public float volume;
		}

		[SerializeField] SoundEffect[] audioEffects = new SoundEffect[0];
		public SoundEffect[] AudioEffects { get { return audioEffects; } }

        [SerializeField, HideInInspector] SerializedDictionary<ValueName, float> floatValue = new SerializedDictionary<ValueName, float>();
        public Dictionary<ValueName, float> FloatValue { get { return floatValue; } set { floatValue = (SerializedDictionary<ValueName, float>)value; } }


		[Header("Upgrade Specifications")]
		[SerializeField] int upgradeMaxAmmo = 10;
		public int UpgradeMaxAmmo { get { return upgradeMaxAmmo; } }

		[SerializeField] float upgradeDamageMultiplier = 1;
		public float UpgradeDamageMultiplier { get { return upgradeDamageMultiplier; } }


        public enum DamageTime { PerHit, PerSecond }
        [Header("For Display info")]
        [SerializeField] DamageTime damageTime = DamageTime.PerHit;
        public DamageTime GetDamageTime { get { return damageTime; } }

    }


    [Serializable]
    public struct CustomKVP<T, V>
    {
        public T key;
        public V value;
        public CustomKVP(T _key, V _value)
        {
            key = _key;
            value = _value;
        }
    }

    [Serializable]
    public class SerializedDictionary<TKey, TVal> : Dictionary<TKey, TVal>, ISerializationCallbackReceiver
    {


        [SerializeField] List<CustomKVP<TKey, TVal>> keyValuePairs = new List<CustomKVP<TKey, TVal>>();

        public void OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                this.Add(keyValuePairs[i].key, keyValuePairs[i].value);
            }
        }

        public void OnBeforeSerialize()
        {
            keyValuePairs.Clear();
            foreach (var kvp in this)
            {
                keyValuePairs.Add(new CustomKVP<TKey, TVal>(kvp.Key, kvp.Value));
            }
        }
    }
}