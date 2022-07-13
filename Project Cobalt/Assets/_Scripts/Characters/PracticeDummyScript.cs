using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeDummyScript : MonoBehaviour, IDestructible
{

    public PracticeDummyConfig config;
    
    public event HealthChangeEvent OnHealthChanged;
	public event DestroyedEvent OnDestroy;

    float health;

    MeshRenderer render;
    Collider col;
    Vector3 spawnPoint;

	public void Damage(float amount) {
        health = Mathf.Clamp(health - amount, 0, 10);
        OnHealthChanged?.Invoke(health, amount);
        if (health <= 0)
            OnDestroy?.Invoke();
    }

    void Die() {
        StartCoroutine(Respawn());
    }

	// Start is called before the first frame update
	void Start() {
        health = config.MaxHealth;
        OnDestroy += Die;
        render = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        spawnPoint = transform.position;
    }

    IEnumerator Respawn() {
        render.enabled = false;
        col.enabled = false;
        transform.position = new Vector3(-9999, -9999, -9999);
        yield return new WaitForSeconds(config.RespawnTime);
        render.enabled = true;
        col.enabled = true;
        health = config.MaxHealth;
        transform.position = spawnPoint;
    }

}
