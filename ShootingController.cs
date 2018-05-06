using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour {

	public float g_damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float g_timeBetweenBullets = 0.15f;        // The time between each shot.
	public float g_range = 10f;                      // The distance the gun can fire.

	float g_timer;                                    // A timer to determine when to fire.
	Ray g_shootRay;                                   // A ray from the gun end forwards.
	RaycastHit g_shootHit;                            // A raycast hit to get information about what was hit.
	int g_shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	ParticleSystem g_gunParticles;                    // Reference to the particle system.
	LineRenderer g_gunLine;                           // Reference to the line renderer.
	AudioSource g_gunAudio;                           // Reference to the audio source.
	Light g_gunLight;                                 // Reference to the light component.
	float g_effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

	PlayerController g_playerScript;

	void Start ()
	{
		// Create a layer mask for the Shootable layer.
		g_shootableMask = LayerMask.GetMask ("Default");

		// Set up the references.
		g_gunParticles = GetComponent<ParticleSystem> ();
		g_gunLine = GetComponent <LineRenderer> ();
		g_gunAudio = GetComponent<AudioSource> ();
		g_gunLight = GetComponent<Light> ();

		g_playerScript = transform.parent.gameObject.GetComponent<PlayerController>();
	}

	void Update ()
	{
		// Add the time since Update was last called to the timer.
		g_timer += Time.deltaTime;

		// If the Fire1 button is being press and it's time to fire...
		if(Input.GetButton ("Fire1") && g_timer >= g_timeBetweenBullets)
		{
			// ... shoot the gun.
			Shoot ();
		}

		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(g_timer >= g_timeBetweenBullets * g_effectsDisplayTime)
		{
			// ... disable the effects.
			DisableEffects ();
		}
	}


	/*************************************************************************************************************/
	/*												FONCTIONS		  											 */
	/*************************************************************************************************************/


	public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		g_gunLine.enabled = false;
		g_gunLight.enabled = false;
	}

	void Shoot ()
	{
		// Reset the timer.
		g_timer = 0f;

		// Play the gun shot audioclip.
		g_gunAudio.Play ();

		// Enable the light.
		g_gunLight.enabled = true;

		// Stop the particles from playing if they were, then start the particles.
		g_gunParticles.Stop ();
		g_gunParticles.Play ();

		// Enable the line renderer and set it's first position to be the end of the gun.
		g_gunLine.enabled = true;
		g_gunLine.SetPosition (0, transform.position);

		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		g_shootRay.origin = transform.position;
		g_shootRay.direction = transform.forward;

		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (g_shootRay, out g_shootHit, g_range, g_shootableMask))
		{
			// Try and find an EnemyHealth script on the gameobject hit.
			EnemyController enemyHealth = g_shootHit.collider.GetComponent <EnemyController> ();

			// If the EnemyHealth component exist...
			if(enemyHealth != null)
			{
				// ... the enemy should take damage.
				enemyHealth.getHit (g_damagePerShot);
				if(enemyHealth.g_isDead){
					g_playerScript.g_scorePoints += enemyHealth.g_scoreProvided;
				}
			}

			// Set the second position of the line renderer to the point the raycast hit.
			g_gunLine.SetPosition (1, g_shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else
		{
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			g_gunLine.SetPosition (1, g_shootRay.origin + g_shootRay.direction * g_range);
		}
	}
}
