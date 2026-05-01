using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Kamera")]
    public Transform target; // Nanti objek Player ditarik ke sini

    [Header("Pengaturan Kamera")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f; // Makin kecil makin lambat ngikutinnya
    public Vector3 offset = new Vector3(0f, 3f, -10f); // Z harus -10 biar kamera ga masuk ke tanah

    // Pakai LateUpdate biar kamera gerak SETELAH player gerak (mencegah stutter/patah-patah)
    void LateUpdate()
    {
        if (target != null)
        {
            // Posisi yang dituju kamera (posisi player + jarak offset)
            Vector3 desiredPosition = target.position + offset;
            
            // Efek transisi halus (Lerp) dari posisi kamera sekarang ke posisi tujuan
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // Terapkan posisi barunya ke kamera
            transform.position = smoothedPosition;
        }
    }
}