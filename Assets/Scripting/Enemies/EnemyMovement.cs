using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private LayerMask obstacleLayer;

    private Vector2 currentDirection;
    private readonly Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ChooseRandomDirection();
    }

    private void Update()
    {
        transform.Translate(currentDirection * speed * Time.deltaTime);
        if (IsDirectionBlocked())
        {
            ChooseRandomDirection();
        }
    }

    private bool IsDirectionBlocked()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, 0.5f, obstacleLayer);
        return hit.collider != null;
    }

    private void ChooseRandomDirection()
    {
        List<Vector2> validDirections = new List<Vector2>();
        foreach (var direction in possibleDirections)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, obstacleLayer);
            if (hit.collider == null)
            {
                validDirections.Add(direction);
            }
        }
        if (validDirections.Count == 0) return;
        currentDirection = validDirections[Random.Range(0, validDirections.Count)];
        UpdateAnimation(currentDirection);
    }

    private void UpdateAnimation(Vector2 direction)
    {
        animator.SetBool("IsMovingRight", false);
        animator.SetBool("IsMovingLeft", false);
        animator.SetBool("IsMovingUp", false);
        animator.SetBool("IsMovingDown", false);

        if (direction == Vector2.right)
        {
            animator.SetBool("IsMovingRight", true);
        }
        else if (direction == Vector2.left)
        {
            animator.SetBool("IsMovingLeft", true);
        }
        else if (direction == Vector2.up)
        {
            animator.SetBool("IsMovingUp", true);
        }
        else if (direction == Vector2.down)
        {
            animator.SetBool("IsMovingDown", true);
        }
    }
    
    // [SerializeField] private float speed = 1f; // מהירות תנועה
    // [SerializeField] private bool moveHorizontally = true; // האם לנוע אופקית או אנכית
    // private Vector2 direction; // כיוון התנועה
    // private Animator animator;
    //
    // private void Start()
    // {
    //     animator = GetComponent<Animator>();
    //     direction = moveHorizontally ? Vector2.right : Vector2.up;
    //     animator.SetBool("IsMovingRight", true);
    //     animator.SetBool("IsMovingLeft", false);
    //     UpdateAnimation(direction);
    // }
    //
    // private void Update()
    // {
    //     transform.Translate(direction * speed * Time.deltaTime);
    // }
    //
    // //private void OnTriggerEnter2D(Collider2D collision)
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Wall"))
    //     {
    //         direction *= -1;
    //         UpdateAnimation(direction);
    //     }
    // }
    //
    // private void UpdateAnimation(Vector2 direction)
    // {
    //     if (direction.x > 0)
    //     {
    //         animator.SetBool("IsMovingRight", true);
    //         animator.SetBool("IsMovingLeft", false);
    //     }
    //     else if (direction.x < 0)
    //     {
    //         animator.SetBool("IsMovingRight", false);
    //         animator.SetBool("IsMovingLeft", true);
    //     }
    //     else if (direction.y != 0)
    //     {
    //         animator.SetBool("IsMovingRight", false);
    //         animator.SetBool("IsMovingLeft", false);
    //     }
    // }
}
