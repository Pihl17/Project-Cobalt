using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Practice Dummy Config", menuName = "ScriptableObject/Practice Dummy Config", order = 0)]
public class PracticeDummyConfig : ScriptableObject
{

    [SerializeField] float maxHealth = 1;
    public float MaxHealth { get { return maxHealth; } }
    [SerializeField] float respawnTime = 1;
    public float RespawnTime { get { return respawnTime; } }

}
