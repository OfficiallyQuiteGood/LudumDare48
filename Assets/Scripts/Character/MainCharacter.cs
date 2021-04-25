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

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;

		//animator.SetFloat("VerticalSpeed", verticalMove);
		animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalMove));

		//get vertical speed
		prevVertical = verticalSpeed;
		verticalSpeed = (transform.position - prevPos).y*50;
		prevPos = transform.position;

		//set verticalSpeed;;
		//fall speed limit
		if(verticalSpeed<-20) gameObject.GetComponent<HealthSystem>().TakeDamage(3);
		animator.SetFloat("VerticalSpeed", verticalSpeed);

		//take damage on fall
		if(prevVertical<0 && controller.m_Grounded) DealFallDamage();
	}

	void OnEnterCollision2D(Collider2D collisionInfo)
	{
		
	}

	void DealFallDamage()
	{
		//Debug.Log("Deal Fall Damage");
		if(prevVertical < -15) gameObject.GetComponent<HealthSystem>().TakeDamage(2);
		else if(prevVertical < -12) gameObject.GetComponent<HealthSystem>().TakeDamage(1);
	}
}
