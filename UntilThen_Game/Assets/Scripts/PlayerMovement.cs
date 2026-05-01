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
        // 1. CEK DULU: Lagi di Menu, di-Pause, ATAU lagi buka HP?
        // Kalau iya, matikan pergerakan dan hentikan eksekusi kode di bawahnya
        if (!canMove || PhoneManager.Instance.phonePanel.activeSelf) 
        {
            movement = Vector2.zero;
            return;
        }

        // 2. AMBIL INPUT: A/D atau Panah Kiri/Kanan
        movement.x = Input.GetAxisRaw("Horizontal");

        // 3. BALIK BADAN OTOMATIS: Flip satu badan penuh (beserta baju & rambut)
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(5f, 5f, 1f); // Nengok Kanan ukuran 2.5
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-5f, 5f, 1f); // Nengok Kiri ukuran 2.5
        }
    }

    void FixedUpdate()
    {
        // 4. EKSEKUSI JALAN: Menggunakan standar Unity 6
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }
}