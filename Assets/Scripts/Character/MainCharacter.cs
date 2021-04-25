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

	void Start()
	{
		lastLegalPosition = transform.position;
		prevPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		
	}
	

	public void Attack()
	{
		animator.SetBool("IsAttacking", true);
	}

	//set legal position if valid
	public void SetLastLegalPosition()
	{
		if(verticalSpeed>=0) lastLegalPosition = gameObject.transform.position;
	}

	//reset position (if player falls too fast)
	void resetPlayerPosition()
	{
		GameObject.Find("Follow Camera").GetComponent<Follow>().PausePan(1f);
		gameObject.GetComponent<HealthSystem>().TakeDamage(1);
		gameObject.GetComponent<CharacterController2D>().playerWon = false;
		StartCoroutine(resetPositionWithDelay(1f));
	}

	//set position and start iframes with delay
	IEnumerator resetPositionWithDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		transform.position = lastLegalPosition;
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
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
		if(verticalSpeed<-15) resetPlayerPosition();
		

		//take damage on fall
		if(prevVertical<0 && controller.m_Grounded) DealFallDamage();
	}

	void OnEnterCollision2D(Collider2D collisionInfo)
	{
		
	}

	void DealFallDamage()
	{
		//Debug.Log("Deal Fall Damage");
		if(prevVertical < -12) gameObject.GetComponent<HealthSystem>().TakeDamage(1);
	}
}
