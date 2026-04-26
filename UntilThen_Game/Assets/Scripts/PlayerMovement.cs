using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool canMove = false; // Awalnya false karena mulai dari Main Menu

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Mendengarkan sinyal dari GameManager (Observer Pattern)
    void OnEnable()
    {
        GameManager.OnGameStart += EnableMovement;
        GameManager.OnGamePaused += ToggleMovement;
        GameManager.OnReturnToMenu += DisableMovement;
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= EnableMovement;
        GameManager.OnGamePaused -= ToggleMovement;
        GameManager.OnReturnToMenu -= DisableMovement;
    }

    private void EnableMovement() => canMove = true;
    private void DisableMovement() => canMove = false;
    private void ToggleMovement(bool isPaused) => canMove = !isPaused;

    void Update()
    {
        // Kalau lagi di menu atau di-pause, input pergerakan dimatikan
        if (!canMove) 
        {
            movement = Vector2.zero;
            return;
        }

        // Ambil input A/D atau Panah Kiri/Kanan
        movement.x = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // Menggerakkan karakter
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }
}