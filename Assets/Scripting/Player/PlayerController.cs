using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    private InputSystem_Actions inputActions;

    private AudioSource footstepAudioSource;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new InputSystem_Actions();
        
        footstepAudioSource = gameObject.AddComponent<AudioSource>();
        footstepAudioSource.loop = true;
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        inputActions.Enable();
        animator.speed = 0f;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        UpdateAnimation(moveInput);
        if (!footstepAudioSource.isPlaying)
        {
            footstepAudioSource.clip = Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y)
                ? SoundManager.Instance.footstepHorizontal
                : SoundManager.Instance.footstepVertical;
            footstepAudioSource.Play();
        }
        animator.speed = 1f;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        UpdateAnimation(moveInput);
        footstepAudioSource.Stop();
        animator.speed = 0f;
    }
    
    private void UpdateAnimation(Vector2 direction)
    {
        bool isMoving = direction != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                animator.SetFloat("Vertical", 0);
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
            }
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
