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
	public Vector3 lastPosition;

	void Start()
	{
		
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

	public void SetLastPosition()
	{
		if(verticalSpeed>=0) lastPosition = gameObject.transform.position;
	}

	void resetPlayerPosition()
	{
		gameObject.GetComponent<HealthSystem>().TakeDamage(1);
		transform.position = lastPosition;
	}

	void FixedUpdate ()
	{
		//if(controller.m_Grounded) SetLastPosition();
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;

		//animator.SetFloat("VerticalSpeed", verticalMove);
		animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalMove));

		//get vertical speed
		prevVertical = verticalSpeed;
		verticalSpeed = (transform.position - prevPos).y*50;
		prevPos = transform.position;
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
