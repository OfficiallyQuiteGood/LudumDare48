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
		animator.SetBool("NotGrounded", jump);
	}
}
