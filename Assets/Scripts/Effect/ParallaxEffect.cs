using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    [SerializeField] Transform target; // The object Cinemachine follows
    [SerializeField] float parallaxEffect = 0.5f; // Speed of the parallax effect
    [SerializeField] float moveSpeed = 0.75f; // Speed at which the target moves

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move the target to simulate player movement
        target.position += Vector3.right * moveSpeed * Time.deltaTime;

        // Calculate background movement based on the target's position
        float distance = target.position.x * parallaxEffect;
        float movement = target.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // Infinite scrolling logic
        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
