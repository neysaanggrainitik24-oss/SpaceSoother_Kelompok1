using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    public float destroyTime = 0.6f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}