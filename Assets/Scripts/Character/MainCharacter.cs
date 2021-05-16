using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
	public CharacterController2D controller;
	public Animator animator;
	public float runSpeed = 40f;
	float horizontalMove = 0f;
	bool jump = false;

	public float verticalSpeed;
	Vector3 prevPos;
	float prevVertical = 0.0f;
	//legal position for respawn
	public Vector3 lastLegalPosition;
	bool canSetPosition = true;
	CheckPoint checkPoint;

	// Audio Clips
    public AudioSource audioSource;
    //0
    public AudioClip[] deathNoises;
    //1
    public AudioClip[] attackNoises;
    //2
    public AudioClip[] moveNoises;
    //3
    public AudioClip[] damageNoises;
    public List<AudioClip[]> noisePacks;
	public AudioClip[] jumpNoises;
	public AudioClip[] ropeStick;
	public AudioClip[] ropeSound;
	public AudioClip[] healthPickup;
    protected bool[] canPlay;

	void Start()
	{
		// set initial spawn loc
		WorldSettings.SetSpawnLocation(gameObject.transform.position);

		lastLegalPosition = transform.position;
		prevPos = transform.position;

		
		noisePacks = new List<AudioClip[]>();
        noisePacks.Add(deathNoises);
        noisePacks.Add(attackNoises);
        noisePacks.Add(moveNoises);
        noisePacks.Add(damageNoises);
		noisePacks.Add(jumpNoises);
		noisePacks.Add(ropeStick);
		noisePacks.Add(ropeSound);
		noisePacks.Add(healthPickup);

		
        canPlay = new bool[noisePacks.Count];
        for(int i = 0; i<canPlay.Length; i++)
        {
            canPlay[i] = true;
        }
	}
	
	// Update is called once per frame
	// Player
	void Update () {
		if(canSetPosition)
		{
			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
			// play running noise
			if(Mathf.Abs(horizontalMove)>0 && controller.m_Grounded) playNoise(2,0.3f);

			if (Input.GetButtonDown("Jump"))
			{
				if(controller.m_Grounded) playNoise(4,0);
				jump = true;
			}
		}

		
	}

	public void playNoise(int ind, float delay)
    {
        StartCoroutine(playNoiseOnDelay(ind, delay));
    }

	public void playNoise(int ind)
	{
		if(canPlay[ind])
        {
            canPlay[ind] = false;
            AudioClip[] noisePack = noisePacks[ind];
            audioSource.PlayOneShot(noisePack[Random.Range(0, noisePack.Length)]);
            canPlay[ind] = true;
        }
	}

    public IEnumerator playNoiseOnDelay(int ind, float delay)
    {
        if(canPlay[ind])
        {
            canPlay[ind] = false;
            AudioClip[] noisePack = noisePacks[ind];
            audioSource.PlayOneShot(noisePack[Random.Range(0, noisePack.Length)]);
            yield return new WaitForSeconds(delay);
            canPlay[ind] = true;
        }
    }
	

	public void Attack()
	{
		StartCoroutine(AttackDelay());

	}

	IEnumerator AttackDelay()
	{
		SetAnimatorAttribute("IsAttacking", true);
		yield return new WaitForSeconds(0.25f);
		SetAnimatorAttribute("IsAttacking", false);
	}

	//set legal position if valid
	public void SetLastLegalPosition()
	{
		if(verticalSpeed>=0 && canSetPosition) lastLegalPosition = gameObject.transform.position;
	}

	public void SetLastLegalPosition(Vector3 position)
	{
		if(verticalSpeed>=0 && canSetPosition) lastLegalPosition = position;
	}
	

	//reset position (if player falls too fast)
	public void resetPlayerPosition(float delay)
	{
		if(gameObject.GetComponent<HealthSystem>().GetHealth()<=0) return;

		gameObject.GetComponent<HealthSystem>().TakeDamageReset(1, delay);

		//dont reset position if dead
		if(gameObject.GetComponent<HealthSystem>().GetHealth()>0)
		{
			GameObject.Find("Follow Camera").GetComponent<Follow>().PausePan(delay);
			gameObject.GetComponent<CharacterController2D>().playerWon = false;
			StartCoroutine(resetPositionWithDelay(delay));
		}
	}

	//set position and start iframes with delay
	IEnumerator resetPositionWithDelay(float delay)
	{
		canSetPosition = false;
		transform.position = lastLegalPosition;
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		
		// add extra delay to input
		yield return new WaitForSeconds(delay);
		
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(0.1f);
		canSetPosition = true;
	}

	void FixedUpdate ()
	{
		// Move our character
		if(canSetPosition) controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;

		

		//get vertical speed
		prevVertical = verticalSpeed;
		verticalSpeed = (transform.position - prevPos).y*50;
		prevPos = transform.position;

		//set animator params
		animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalMove));
		animator.SetFloat("VerticalSpeed", verticalSpeed);

		//set verticalSpeed;;
		//fall speed limit
		//if(verticalSpeed<-15) resetPlayerPosition(1f);
		

		//take damage on fall
		//if(prevVertical<0 && controller.m_Grounded) DealFallDamage();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		//Debug.Log("PLAYER COLLISION: "+collider);
		if (collider.transform.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Projectile projectile = collider.transform.gameObject.GetComponent<Projectile>();
            if (projectile)
            {
                gameObject.GetComponent<HealthSystem>().TakeDamage(1);
            }
        }
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		Collider2D collider = collision.collider;
		//Debug.Log("PLAYER COLLISION: "+collider);
		if (collider.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collider.transform.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                gameObject.GetComponent<HealthSystem>().TakeDamage(1);
            }
        }

		if (collider.transform.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Projectile projectile = collider.transform.gameObject.GetComponent<Projectile>();
            if (projectile)
            {
                gameObject.GetComponent<HealthSystem>().TakeDamage(1);
            }
        }

		if (collider.transform.gameObject.layer == LayerMask.NameToLayer("Hazards"))
        {
            resetPlayerPosition(1f);
        }
	}

	public void SetAnimatorAttribute(string parameterName, bool val)
	{
		if(animator.GetBool(parameterName) != val)
        {
            animator.SetBool(parameterName, val);
        }
	}

	void DealFallDamage()
	{
		//Debug.Log("Deal Fall Damage");
		if(prevVertical < -12) gameObject.GetComponent<HealthSystem>().TakeDamage(1);
	}

	public void setCheckPoint(CheckPoint cp)
	{
		this.checkPoint = cp;
	}

	public void loadCheckPoint()
	{
		if(checkPoint != null)
		{
			gameObject.GetComponent<HealthSystem>().HealHealth(3);
			gameObject.transform.position = checkPoint.transform.position;
		}
		else
		{
			gameObject.GetComponent<HealthSystem>().HealHealth(3);
			gameObject.transform.position = WorldSettings.GetSpawnLocation();
		}
	}

	
}
