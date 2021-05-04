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
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			playNoise(4,0);
			jump = true;
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

	//reset position (if player falls too fast)
	public void resetPlayerPosition(float delay)
	{
		GameObject.Find("Follow Camera").GetComponent<Follow>().PausePan(delay);
		gameObject.GetComponent<HealthSystem>().TakeDamage(1);
		gameObject.GetComponent<CharacterController2D>().playerWon = false;
		StartCoroutine(resetPositionWithDelay(0.6f));
	}

	//set position and start iframes with delay
	IEnumerator resetPositionWithDelay(float delay)
	{
		canSetPosition = false;
		yield return new WaitForSeconds(delay);
		transform.position = lastLegalPosition;
		canSetPosition = true;
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		if(Mathf.Abs(horizontalMove)>0 && controller.m_Grounded) playNoise(2,0.3f);
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
		if(verticalSpeed<-15) resetPlayerPosition(1f);
		

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
	}
}
