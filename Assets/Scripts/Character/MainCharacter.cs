using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
public CharacterController2D controller;

	public Animator animator;
	public float Health;
	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
	}

	public void TakeDamage()
	{

	}

	public void FillHeart()
	{

	}

	public void Attack()
	{

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
