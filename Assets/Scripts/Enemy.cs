using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	#region Public Variables
	public int Health;
	public ParticleSystem bloodParticle;
	public ParticleSystem blooGib;
	public float secsUntilDestroy;
	public GameObject enemyProjectile;
	public Transform firingPoint;
	public bool stationary;
	public float enemyBulletSpeed;
	public float timeBetweenAttacks;
	[Space(5)]
	public float cautionRange = 40f;
	public float alertedRange = 20f;
	public float attackRange = 10f;
	public float roamingSpeed = 5f;
	public float chaseSpeed = 10f;
	public Transform[] roamingPoints;
	[Range(1.0f, 0.1f)]
	public float agonyChance;
	public float agonyRevivalTime;
	public int playerCoolonDefeat = 5;

	[Space(5)]
	[Header("States")]
	public bool idle;
	public bool caution;
	public bool chase;
	public bool attack;
	public bool agony;

	[Space(5)]
	[Header("Audio")]
	public AudioClip ambientSound;
	[Range(1.0f, 0.1f)]
	public float ambientPlayChance;
	public float ambientCooldownSec;
	public AudioClip alertSound;
	public AudioClip shootSound;
	public AudioClip painSound;
	public AudioClip deathSound;
	public bool passive = false;
	#endregion

	#region Private
	private SpriteRenderer render;
	private bool canPlayAmbient;
	private bool notplayedalertsound = true;
	private bool alreadyAttacked;
	private int currentRoamPoint;
	private AudioSource audios;
	private Transform player;
	private NavMeshAgent nav;
	private Rigidbody rb;
	private Animator anim;
	private LayerMask ground = 1 << 6;
	private LayerMask playerMask = 1 << 3;
	private Color normalColor;
	private bool addOnce = false;
	AudioSource deathAudio;
	#endregion

	private void Start()
	{
		player = Player.instance.transform;
		rb = GetComponent<Rigidbody>();
		nav = GetComponent<NavMeshAgent>();
		audios = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		render = GetComponent<SpriteRenderer>();

		normalColor = render.material.GetColor("_Flash");

		deathAudio = GameObject.Find("EnemyDeathSound").GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (!passive)
		{
			caution = Physics.CheckSphere(transform.position, cautionRange, playerMask);
			chase = Physics.CheckSphere(transform.position, alertedRange, playerMask);
			attack = Physics.CheckSphere(transform.position, attackRange, playerMask);

			if (idle || stationary)
			{
				transform.position = this.transform.position;
				anim.SetBool("idle", true);
				anim.SetBool("walk", false);
				anim.SetBool("aim", false);
				anim.SetBool("shoot", false);
			}

			if (caution && !chase && !attack && !agony && !stationary)
			{
				Patrolling();
				anim.SetBool("idle", false);
				anim.SetBool("walk", true);
				anim.SetBool("aim", false);
				anim.SetBool("shoot", false);
			}

			if (caution && chase && !attack && !agony && !stationary)
			{
				ChasePlayer();
				anim.SetBool("idle", false);
				anim.SetBool("walk", true);
				anim.SetBool("aim", false);
				anim.SetBool("shoot", false);
			}

			if (caution && chase && attack && !agony)
			{
				AttackPlayer();
				anim.SetBool("idle", false);
				anim.SetBool("walk", false);
				anim.SetBool("aim", true);
				anim.SetBool("shoot", false);
			}

			if (agony)
			{
				anim.SetBool("idle", false);
				anim.SetBool("walk", false);
				anim.SetBool("aim", false);
				anim.SetBool("shoot", false);
				anim.SetBool("agony", true);
				InAgony();
			}
		}
	}

	private void Patrolling()
	{
		if (ambientSound != null)
		{
			if (Random.value > ambientPlayChance && canPlayAmbient)
			{
				audios.pitch = Random.Range(0.9f, 1.1f);
				audios.clip = ambientSound;
				audios.Play();
				canPlayAmbient = false;
				Invoke(nameof(canPlayAmbient), ambientCooldownSec);
			}
		}

		if (roamingPoints != null)
		{
			nav.SetDestination(roamingPoints[currentRoamPoint].position);
			for (int i = 0; i < roamingPoints.Length; i++)
			{
				if (Vector3.Distance(transform.position, roamingPoints[currentRoamPoint].position) < 1f)
				{
					currentRoamPoint++;
					if (currentRoamPoint > roamingPoints.Length - 1)
					{
						currentRoamPoint = 0;
					}
				}
			}
		}
		else
		{
			nav.SetDestination(transform.position);
		}
	}

	private void ChasePlayer()
	{
		nav.SetDestination(player.position);
		nav.speed = chaseSpeed;
		if (notplayedalertsound)
		{
			if (alertSound != null)
			{
				audios.pitch = Random.Range(0.9f, 1.1f);
				audios.clip = alertSound;
				audios.Play();
				notplayedalertsound = false;
			}
		}
	}

	private void AttackPlayer()
	{
		nav.SetDestination(transform.position);
		Vector3 target = new Vector3(player.transform.position.x,
			transform.position.y,
			player.transform.position.z);
		transform.LookAt(target);

		if (!alreadyAttacked)
		{
			alreadyAttacked = true;
			Invoke(nameof(Shoot), timeBetweenAttacks);
		}

	}

	private void Shoot()
	{
		anim.SetBool("shoot", true);
		Invoke(nameof(ResetAnim), 1);
		alreadyAttacked = false;
		if (!agony)
		{
			Rigidbody rb = Instantiate(enemyProjectile, firingPoint.position, this.transform.rotation).GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * enemyBulletSpeed, ForceMode.Impulse);
			if (shootSound != null)
			{
				audios.pitch = Random.Range(0.9f, 1.1f);
				audios.clip = shootSound;
				audios.Play();
			}
		}
	}

	private void ResetAnim()
	{
		anim.SetBool("shoot", false);
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;
		if (Random.value > agonyChance)
		{
			agony = true;
		}
		if (Health <= 0)
		{
			StartCoroutine(DeathEvent());
		}
		if (painSound != null)
		{
			audios.pitch = Random.Range(0.9f, 1.1f);
			audios.clip = painSound;
			audios.Play();
		}
		render.material.SetColor("_Flash", Color.white);

		Invoke(nameof(ResetColor), 0.1f);
	}

	public void BloodEffect(RaycastHit hit)
	{
		Instantiate(bloodParticle, hit.transform.position, Quaternion.identity);
	}

	private void ResetColor()
	{

		render.material.SetColor("_Flash", normalColor);
	}

	private IEnumerator DeathEvent()
	{
		if (deathSound != null)
		{
			deathAudio.pitch = Random.Range(0.9f, 1.1f);
			deathAudio.clip = deathSound;
			deathAudio.Play();
		}
		GameManager.Instance.enemiesKilled++;

		if (!addOnce)
		{
			AddPlayerCool();
		}
		Instantiate(blooGib, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(secsUntilDestroy);
		Destroy(gameObject);
	}

	private void AddPlayerCool()
	{
		Player.instance.cool += playerCoolonDefeat;
		addOnce = true;
	}

	private void InAgony()
	{
		nav.SetDestination(transform.position);
		transform.LookAt(player);
		Invoke(nameof(AgonyRevival), agonyRevivalTime);
	}

	private void AgonyRevival()
	{
		anim.SetBool("agony", false);
		agony = false;
	}

	private void ResetAmbientSound()
	{
		canPlayAmbient = true;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, alertedRange);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, cautionRange);
	}
}