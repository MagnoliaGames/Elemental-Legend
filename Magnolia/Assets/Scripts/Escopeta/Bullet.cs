using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float normalization;
    private Vector3 normalizedOrientation;
    private Rigidbody rb;

    public Vector3 direction;
    public float speed;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        normalization = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        normalizedOrientation = new Vector3(direction.x / normalization, direction.y / normalization);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(transform.forward.x, transform.forward.y * normalizedOrientation.x, transform.forward.z) * speed;
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            other.GetComponent<EnemyHealth>().health -= damage;
            Destroy(gameObject);
        }
        if (other.CompareTag("Piso"))
        {
            Destroy(gameObject);
        }
    }
}
