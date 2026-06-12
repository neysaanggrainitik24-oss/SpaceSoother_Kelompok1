using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 1.8f;

    private float tileSizeY;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        tileSizeY = GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
        transform.position = startPosition + Vector3.down * newPosition;
    }
}