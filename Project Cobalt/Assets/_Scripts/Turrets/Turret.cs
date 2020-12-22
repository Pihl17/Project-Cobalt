using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IDestructible
{
	public TurretConfig configFile;
	[SerializeField] protected Team team = Team.Neutral;

	public event DestroyedEvent OnDestroy;
	public event DamagedEvent OnDamaged;

	float health;
	float destroyedTimer;
	bool destroyed;
	Vector3 alivePos;

	protected virtual void Initialisation() {
		health = configFile.MaxHealth;
		alivePos = transform.position;
	}

	// Start is called before the first frame update
	void Start()
    {
		Initialisation();
	}

    // Update is called once per frame
    void Update()
    {
		CheckForRespawn();
    }

	public bool Targetable(Team otherTeam) {
		return team != otherTeam;
	}

	public void Damage(float amount) {
		health = Mathf.Max(health - amount, 0);
		if (OnDamaged != null)
			OnDamaged.Invoke(health, amount);
		if (health <= 0)
			Die();
	}

	void Die() {
		ScoreManager.ChangeScore(team == Team.Blue ? Team.Red : Team.Blue, configFile.DestroyReward);
		OnDestroy?.Invoke();
		Destroy();
	}

	protected virtual void Destroy() {
		destroyed = true;
		destroyedTimer = 0;
		transform.position = GlobalVariables.StorePos;
		//GameObject.Destroy(gameObject);
	}

	void CheckForRespawn() {
		if (destroyed) {
			destroyedTimer += Time.deltaTime;
			if (destroyedTimer >= configFile.DestroyedTime)
				Respawn();
		}
	}

	protected virtual void Respawn() {
		destroyed = false;
		health = configFile.MaxHealth;
		transform.position = alivePos;
	}

	protected void Fire(Vector3 dir) {
		throw new System.NotImplementedException();
	}

	protected void Aim(Vector3 target) {
		throw new System.NotImplementedException();
	}

	protected void AimingAtTarget(Vector3 target) {
		throw new System.NotImplementedException();
	}

}
