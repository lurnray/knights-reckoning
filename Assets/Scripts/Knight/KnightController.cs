using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    // Audio clips for actions
    public AudioClip jumpClip;
    public AudioClip duckClip;
    public AudioClip hitClip;
    public AudioClip swordAttackClip;

    private Rigidbody rb;
    private Animator animator;
    private bool isDucking = false;
    
    // Reference to the sword's collider
    private Collider swordCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        // Find and assign the sword collider
        swordCollider = transform.Find("Sword").GetComponent<Collider>();
    }

    void Update()
    {
        // Handle movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Move the character
        if (moveDirection.magnitude >= 0.1f)
        {
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);

            // Move the knight
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Rotate the knight to face movement direction
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AudioManager.Instance.PlaySound(jumpClip);
        }

        // Duck
        if (Input.GetKeyDown(KeyCode.S) && !isDucking)
        {
            StartDucking();
        }
        else if (Input.GetKeyUp(KeyCode.S) && isDucking)
        {
            StopDucking();
        }

        // Sword Attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwordAttack();
        }
    }

    void StartDucking()
    {
        isDucking = true;
        animator.SetBool("IsDucking", true);
        AudioManager.Instance.PlaySound(duckClip);
    }

    void StopDucking()
    {
        isDucking = false;
        animator.SetBool("IsDucking", false);
    }

    void SwordAttack()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");
        AudioManager.Instance.PlaySound(swordAttackClip);

        // Detect enemies within the sword's range
        Collider[] hitEnemies = Physics.OverlapBox(swordCollider.bounds.center, swordCollider.bounds.extents, swordCollider.transform.rotation);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Destroy(enemy.gameObject); // Destroy the enemy on hit
            }
        }
    }

    // Check if the knight is grounded to enable jumping
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
}
