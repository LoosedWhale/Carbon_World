using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject DialougePanel;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool canMove = true;

    void Start()
    {
        // Cache components for better performance
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Get raw input values
            GetMovementInput();

            // Set animator parameters based on raw input
            UpdateAnimatorParameters();

            // Attempt to move in the primary and secondary directions
            bool success = TryMove(movementInput);

            // If movement failed, try to move in the X direction
            if (!success)
                success = TryMove(new Vector2(movementInput.x, 0));

            // If movement in the X direction failed, try to move in the Y direction
            if (!success)
                TryMove(new Vector2(0, movementInput.y));

            // Set direction of sprite to movement direction
            UpdateSpriteDirection();
        }
    }

    private void GetMovementInput()
    {
        // Get raw input values
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    private void UpdateAnimatorParameters()
    {
        // Set animator parameters based on raw input
        // Ändrar inte om ingen knapp är intryckt
        if(!(movementInput.x == 0 && movementInput.y == 0)){
            animator.SetFloat("Horizontal", movementInput.x);
            animator.SetFloat("Vertical", movementInput.y);
        }
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            // If there are no collisions, move the body
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
        }

        return false;
    }

    private void UpdateSpriteDirection()
    {
        // Set direction of sprite to movement direction
        spriteRenderer.flipX = animator.GetFloat("Horizontal") < 0;
    }

    void OnMove(InputValue movementValue)
    {
        // Get the movement input from the input system
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        if(DialougePanel.activeInHierarchy){
                print("no");
            }
        else{
        // Trigger the sword attack animation
        animator.SetTrigger("swordAttack");
        }

    }

    public void SwordAttack()
    {
        // Lock movement while attacking
        LockMovement();

        // Trigger the sword attack based on sprite direction
        if (spriteRenderer.flipX)
            swordAttack.AttackLeft();
        else
            swordAttack.AttackRight();
    }

    public void EndSwordAttack()
    {
        // Unlock movement when sword attack ends
        UnlockMovement();

        // Stop the sword attack
        swordAttack.StopAttack();
    }

    private void LockMovement()
    {
        canMove = false;
    }

    private void UnlockMovement()
    {
        canMove = true;
    }





}

public static class Stats {
    public static int fireballKills = 0;
    public static int pickupCounter = 0;
    public static int trashCounter = 0;
    public static int keyCounter = 0;

}