using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Abilities
{

    public enum ValueName { None, ExplosionRadius, MinCurvatureHeight, ChargeTime, DamageMultiplier, RangeMultiplier }

    [CreateAssetMenu(fileName = "AbilityConfig", menuName = "ScriptableObject/AbilityConfig", order = 2)]
    public class AbilityConfig : ScriptableObject//, ISerializationCallbackReceiver
    {
        [Header("Common Variables")]
        [SerializeField] string abilityName = "Unnamed";
        public string Name { get { return abilityName; } }

        [SerializeField] float cooldown = 1;
        public float Cooldown { get { return cooldown; } }
        public float FireRate { get { return 1 / cooldown; } }

        [Header("Ability Specific")]
        [SerializeField] float damage = 1;
        public float Damage { get { return damage; } }

        [SerializeField] float range = 10;
        public float Range { get { return range; } }

        [SerializeField] GameObject[] instantiatableObjects = new GameObject[0];
        public GameObject[] InstantiatableObjects { get { return instantiatableObjects; } }


        [SerializeField, HideInInspector] SerializedDictionary<ValueName, float> floatValue = new SerializedDictionary<ValueName, float>();
        public Dictionary<ValueName, float> FloatValue { get { return floatValue; } set { floatValue = (SerializedDictionary<ValueName, float>)value; } } 


        public enum DamageTime { PerHit, PerSecond }
        [Header("For Display info")]
        [SerializeField] DamageTime damageTime = DamageTime.PerHit;
        public DamageTime GetDamageTime { get { return damageTime; } }






        /*[Serializable]
        struct CustomKVP<T> {
            public string key;
            public T value;
            public CustomKVP(string _key, T _value) {
                key = _key;
                value = _value;
            }
        }
        [SerializeField] List<CustomKVP<float>> customFloatValues = new List<CustomKVP<float>>();

        public void OnAfterDeserialize() {
            floatValue = new Dictionary<string, float>();
            for (int i = 0; i < customFloatValues.Count; i++)
            {
                string keyName = customFloatValues[i].key;
                while (floatValue.ContainsKey(keyName)) {
                    keyName = keyName + " copy";
                }
                floatValue.Add(keyName, customFloatValues[i].value);
            }
        }

        public void OnBeforeSerialize() {
            customFloatValues.Clear();
            foreach (var kvp in floatValue) {
                customFloatValues.Add(new CustomKVP<float>(kvp.Key, kvp.Value));
            }
        }*/




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