using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    public float destroyTime = 0.6f;
    public AudioClip explosionSFX;

    void Start()
    {
        // Mainkan suara ledakan
        AudioSource.PlayClipAtPoint(
            explosionSFX,
            transform.position
        );

        // Hancurkan objek ledakan
        Destroy(gameObject, destroyTime);
    }
}