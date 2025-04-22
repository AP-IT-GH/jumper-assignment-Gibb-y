using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        float height = Random.Range(0.2f, 1f);
        transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, height / 2, transform.position.z);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.localPosition.z > 8)
            Destroy(gameObject);
    }
}
